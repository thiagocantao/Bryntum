using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;
using System.Data;

public partial class _CertificadoDigital_salvaArquivoP7s : System.Web.UI.Page
{
    NameValueCollection req;
    dados cDados;
    int codigoEntidade, codigoUsuario;

    public static bool AcceptAllCertifictions(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    private HttpWebRequest GetRequest()
    {
        ServicePointManager.ServerCertificateValidationCallback =
            new RemoteCertificateValidationCallback(AcceptAllCertifictions);
        string nomeParametro = "enderecoWebServiceAssinaturaDigital";
        DataSet dsParam = cDados.getParametrosSistema(codigoEntidade, nomeParametro);
        object valorParametro = dsParam.Tables[0].Rows[0][nomeParametro];
        string endrecoWebServiceAssinaturaDigital = valorParametro + "complement";
        //"https://179.124.38.222:8543/cdis/ws-sign/signature/complement";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endrecoWebServiceAssinaturaDigital);
        request.ContentType = "application/json; charset=UTF-8";
        request.Accept = "application/json";
        request.Method = "POST";
        return request;
    }

    private Resposta ObtemObjetoResposta(HttpWebRequest request)
    {
        string result;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
        {
            result = rdr.ReadToEnd();
        }
        return new JavaScriptSerializer().Deserialize<Resposta>(result);
    }

    private string ObtemStringRequisicao(byte[] pkcs7Encoded, byte[] conteudoArquivo, byte[] assinaturaAnterior, string policy)
    {
        string dataToBeSigned = Convert.ToBase64String(conteudoArquivo);
        string detachedSignature = Convert.ToBase64String(pkcs7Encoded);
        string oldDetachedSignature = assinaturaAnterior == null ? null : Convert.ToBase64String(assinaturaAnterior);
        var requestBody = new Requisicao
        {
            dataToBeSigned = dataToBeSigned,
            detachedSignature = detachedSignature,
            oldDetachedSignature = oldDetachedSignature,
            policyOID = policy
        };
        return new JavaScriptSerializer().Serialize(requestBody);
    }

    private void AssinaArquivo()
    {
        DateTime dataAssinatura = DateTime.MinValue;
        string mensagemErro = string.Empty;
        // Obtem os parametros enviados pelo cliente
        req = Request.Form;
        if (string.IsNullOrEmpty(req["QTYDATA"]))
        {
            RegistraLog("Ausência de informações", "campo QTYDATA faltando");
            throw new ServletException("campo QTYDATA faltando");
        }
        // Obtem o identificador do documento
        int docId;
        if (!int.TryParse(req["IDDATA"], out docId))
        {
            RegistraLog("Ausência de informações", "campo IDDATA faltando");
            throw new ServletException("campo IDDATA faltando");
        }

        string infoId;
        string paramName;
        string fileNameLabel;
        if (docId == -1)
        {
            infoId = "#idArq";
            paramName = "ENCDATA";
            fileNameLabel = "#arquivo";
        }
        else
        {
            infoId = "#idArq." + docId;
            paramName = "ENCDATA." + docId;
            fileNameLabel = "#arquivo." + docId;
        }
        int codigoFormularioAssinatura = int.Parse(req[infoId]);
        byte[] pkcs7Encoded = StringToByteArray(req[paramName]);
        string postedName = req[fileNameLabel];
        if (string.IsNullOrEmpty(postedName))
            postedName = "rawData.txt";
        string physicalPaty = Request.ServerVariables["APPL_PHYSICAL_PATH"];
        string nomeArquivoOrigem = Path.Combine(physicalPaty, "_CertificadoDigital", "TempOrigem", postedName);
        string nomeArquivoDestino = Path.Combine(physicalPaty, "_CertificadoDigital", "TempDestino", postedName + ".P7s");
        byte[] assinaturaAnterior = ObtemAssinaturaAnterior(codigoFormularioAssinatura);
        byte[] conteudoArquivo = File.ReadAllBytes(nomeArquivoOrigem);

        bool usePolicy = false;
        string policy = req["policy"];
        bool.TryParse(req["usePolicy"], out usePolicy);

        try
        {
            RegistraLog("Inicia processo de requisição de assinatura", "CFA:" + codigoFormularioAssinatura);

            string jsonRequest = ObtemStringRequisicao(pkcs7Encoded, conteudoArquivo, assinaturaAnterior, policy);
            HttpWebRequest request = GetRequest();
            using (Stream requestStream = request.GetRequestStream())
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(jsonRequest);
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
            }

            RegistraLog("Realiza requisição de assinatura", "CFA:" + codigoFormularioAssinatura);
            Resposta r = ObtemObjetoResposta(request);
            dataAssinatura = GetDateFromUnixEpoch(r.signingTime);
            pkcs7Encoded = Convert.FromBase64String(r.newerSignature);
        }
        catch (Exception ex)
        {
            RegistraLog("Exceção levantada durante o processo de requisição de assinatura do arquivo", ex.Message);
            mensagemErro = ex.Message;
        }

        int resultado = 0;
        if (string.IsNullOrEmpty(mensagemErro))
        {
            try
            {
                RegistraLog("Realiza gravação assinatura digital no banco de dados", "CFA:" + codigoFormularioAssinatura);
                resultado = ExecutaComandoAssinatura(codigoFormularioAssinatura, conteudoArquivo, pkcs7Encoded, mensagemErro, dataAssinatura);
            }
            catch (Exception ex)
            {
                RegistraLog("Exceção levantada durante o processo de gravação da assinatura digital no banco de dados", ex.Message);
                mensagemErro = ex.Message;
            }
        }
        Response.Clear();
        if (resultado == 1)
        {
            string msgError = "";

            if (Request.QueryString["PassaFluxo"] + "" == "S")
            {
                string comandoSQL = string.Format(
                @"SELECT CodigoFormularioAssinatura, 
					 CodigoFormulario, 
					 CodigoWorkflow, 
					 CodigoInstanciaWf,
                     SequenciaOcorrenciaEtapaWf,
                     CodigoEtapaWf
                FROM Log_FormularioAssinatura 
               WHERE CodigoFormularioAssinatura  = {0}", codigoFormularioAssinatura);

                DataSet ds = cDados.getDataSet(comandoSQL);
                DataRow dr = ds.Tables[0].Rows[0];

                RegistraLog("Efetiva assinatura digital e processa ação workflow", string.Format("cw={0};ci={1};so={2};ce={3}", dr["CodigoWorkflow"], dr["CodigoInstanciaWf"], dr["SequenciaOcorrenciaEtapaWf"], dr["CodigoEtapaWf"]));
                cDados.ProcessaAcaoWorkflow(codigoUsuario.ToString(), dr["CodigoWorkflow"].ToString(), dr["CodigoInstanciaWf"].ToString(), dr["SequenciaOcorrenciaEtapaWf"].ToString(), dr["CodigoEtapaWf"].ToString(), true, out msgError);
            }

            if (msgError == "")
            {
                RegistraLog("Arquivo assinado", "CFA:" + codigoFormularioAssinatura);
                Response.Write("Ok");
            }
            else
            {
                RegistraLog("Falha ao passar de etapa", msgError);
                Response.Write(string.Format("Status: Erro ao passar etapa - Mensagem: {0}", msgError));
            }
        }
        else
        {
            RegistraLog("Falha durante o processo de assinatura", "Status:" + resultado);
            Response.Write(string.Format("Status: {0} - Mensagem: {1}", resultado, mensagemErro));
        }
        Response.End();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        codigoEntidade = int.Parse(Request.QueryString["ce"]);
        codigoUsuario = int.Parse(Request.QueryString["cu"]);

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        AssinaArquivo();
    }

    private byte[] ObtemAssinaturaAnterior(int codigoFormularioAssinatura)
    {
        byte[] assinaturaAnterior = null;
        string comandoSql = string.Format(@"
DECLARE @CodigoFormularioAssinatura INT
    SET @CodigoFormularioAssinatura = {0}
    
DECLARE @CodigoFormularioAtual INT
    SET @CodigoFormularioAtual = (SELECT CodigoFormulario FROM Log_FormularioAssinatura WHERE CodigoFormularioAssinatura = @CodigoFormularioAssinatura)

SELECT BinarioAssinatura 
  FROM FormularioAssinatura
 WHERE CodigoFormulario = (SELECT CodigoFormulario FROM dbo.f_getAssinaturasFormulario(@CodigoFormularioAtual))", codigoFormularioAssinatura);

        DataTable dt = cDados.getDataSet(comandoSql).Tables[0];
        if (dt.Rows.Count > 0)
            assinaturaAnterior = dt.Rows[0]["BinarioAssinatura"] as byte[];

        return assinaturaAnterior;
    }

    private DateTime GetDateFromUnixEpoch(long unixEpochTime)
    {
        TimeSpan elapsedTime = TimeSpan.FromMilliseconds(unixEpochTime);
        DateTime unixInitilDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return TimeZoneInfo.ConvertTimeFromUtc(unixInitilDate.Add(elapsedTime), TimeZoneInfo.Local);
    }

    private int ExecutaComandoAssinatura(int codigoFormularioAssinatura, byte[] conteudoArquivo, byte[] binarioAssinatura, string descricaoErroAssinatura, DateTime dataAssinatura)
    {
        int resultado = (string.IsNullOrEmpty(descricaoErroAssinatura)) ? 1 : 0;
        string connectionString = cDados.classeDados.getStringConexao();
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        var command = new SqlCommand();
        var pAssinatura = new SqlParameter();
        var pImagemFormulario = new SqlParameter();
        var pCodigoFormularioAssinatura = new SqlParameter();
        var pCodigoResultadoAssinatura = new SqlParameter();
        var pDescricaoErroAssinatura = new SqlParameter();
        var pDataAssinatura = new SqlParameter();
        var pValorRetorno = new SqlParameter();

        command.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
        command.CommandText = "p_GravaAssinaturaDigitalFormulario";
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = conn;

        #region Configura parâmetros

        //pCodigoFormularioAssinatura
        pCodigoFormularioAssinatura.ParameterName = "@in_codigoFormularioAssinatura";
        pCodigoFormularioAssinatura.Value = codigoFormularioAssinatura;
        //pCodigoResultadoAssinatura
        pCodigoResultadoAssinatura.ParameterName = "@in_codigoResultadoAssinatura";
        pCodigoResultadoAssinatura.Value = resultado;
        //pDescricaoErroAssinatura
        pDescricaoErroAssinatura.ParameterName = "@in_descricaoErroAssinatura";
        pDescricaoErroAssinatura.Value = descricaoErroAssinatura;
        //pDataAssinatura
        pDataAssinatura.ParameterName = "@in_dataAssinatura";
        pDataAssinatura.Value = dataAssinatura;
        //pImagemFormulario
        pImagemFormulario.ParameterName = "@in_imagemFormulario";
        pImagemFormulario.SqlDbType = SqlDbType.VarBinary;
        pImagemFormulario.Size = conteudoArquivo.Length;
        pImagemFormulario.Value = conteudoArquivo;
        //pAssinatura
        pAssinatura.ParameterName = "@in_binarioAssinatura";
        pAssinatura.SqlDbType = SqlDbType.VarBinary;
        pAssinatura.Size = binarioAssinatura.Length;
        pAssinatura.Value = binarioAssinatura;
        //pValorRetorno
        pValorRetorno.Direction = ParameterDirection.ReturnValue;

        #endregion

        command.Parameters.Add(pCodigoFormularioAssinatura);
        command.Parameters.Add(pDataAssinatura);
        command.Parameters.Add(pAssinatura);
        command.Parameters.Add(pCodigoResultadoAssinatura);
        command.Parameters.Add(pDescricaoErroAssinatura);
        command.Parameters.Add(pImagemFormulario);
        command.Parameters.Add(pValorRetorno);

        command.ExecuteNonQuery();

        return (int)pValorRetorno.Value;
    }

    public byte[] StringToByteArray(String hex)
    {
        return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    }

    private void RegistraLog(string descricao, string contexto = null)
    {
        object codigoOperacao = cDados.getInfoSistema("CodigoOperacaoCritica");
        if (codigoOperacao is long)
            cDados.RegistraPassoOperacaoCritica((long)codigoOperacao, descricao, contexto);
    }

    class ServletException : ApplicationException
    {
        public ServletException(string msg)
            : base(msg)
        {

        }
    }

    public class Resposta
    {
        public Resposta()
        {
        }
        public string newerSignature { get; set; }
        public long signingTime { get; set; }
        public string cpf { get; set; }
        public string numeroSerial { get; set; }
        public long dataDeExpiracao { get; set; }
    }

    public class Requisicao
    {
        public Requisicao()
        {
        }
        public string dataToBeSignedHash { get; set; }
        public string dataToBeSigned { get; set; }
        public string detachedSignature { get; set; }
        public string oldDetachedSignature { get; set; }
        public string policyOID { get; set; }
    }
}