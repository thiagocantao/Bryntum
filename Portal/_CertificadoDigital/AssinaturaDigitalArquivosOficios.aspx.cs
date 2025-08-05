using Cdis.IntegraCertillion;
using Cdis.IntegraCertillion.com.certillion.labs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Data.SqlClient;

public partial class _CertificadoDigital_AssinaturaDigitalArquivosOficios : System.Web.UI.Page
{
    dados cDados;
    string codigos;
    int codigoEntidade;
    int idUsuarioLogado;
    string comiteDeliberacao;

    private long? CodigoOperacao
    {
        get
        {
            return (long)cDados.getInfoSistema("CodigoOperacaoCritica");
        }
        set
        {
            cDados.setInfoSistema("CodigoOperacaoCritica", value);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        idUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));

        codigos = Request.QueryString["codigos"];
        comiteDeliberacao = Request.QueryString["CD"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrWhiteSpace(codigos))
        {
            CodigoOperacao = cDados.RegistraOperacaoCritica("ASSNOFICIOS", idUsuarioLogado, codigoEntidade);
            cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Escopo do processo", string.Format("Ofícios: {0}", codigos));
            cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Configuração", Request.IsSecureConnection ? "https" : "http");
            cDados.setInfoSistema("CodigoOperacaoCritica", CodigoOperacao);
        }

        DefineTextoInformacao();
    }

    private void DefineTextoInformacao()
    {
        string comandoSQL = string.Format("SELECT Texto FROM TextoPadraoSistema WHERE IniciaisTexto = 'AssinatDigital'");

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            lblTextoInformacao.Text = ds.Tables[0].Rows[0]["Texto"].ToString();
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        switch (e.Parameter)
        {
            case "assinar":
                #region assinar
                try
                {
                    string[] arquivosAssinatura = ObtemArquivosAssinatura();
                    cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value,
                        "Inicia processo de requisição de assinatura", "Codigos:" + codigos);
                    BatchSignatureRespTypeV2 respostaRequisicaoAssinatura =
                        EnviaRequisicaoAssinaturaArquivos(arquivosAssinatura);
                    StatusType statusRequisicaoAssinatura = respostaRequisicaoAssinatura.Status;
                    RegistraStatusOperacao("Requisição assinatura", statusRequisicaoAssinatura);
                    if (statusRequisicaoAssinatura.StatusCode == 100)
                        e.Result = respostaRequisicaoAssinatura.TransactionId.ToString();
                    else
                        e.Result = string.Format("Assinatura digital do documento não realizada. '{0}'",
                            statusRequisicaoAssinatura.StatusDetail.Replace("'", "''"));
                }
                catch (Exception ex)
                {
                    const string mensagemErroUsuarioDesconhecido = "This enduser is unknown.";
                    if (ex.Message.Equals(mensagemErroUsuarioDesconhecido, StringComparison.InvariantCultureIgnoreCase))
                    {
                        string emailUsuario = ObtemIdentificacaoUsuario();
                        e.Result = string.Format("'{0}' não está cadastrado no serviço responsável pela assinatura digital dos arquivos.", emailUsuario);
                    }
                    else
                        e.Result = ex.Message;
                    cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, e.Result.Replace("'", "''"), ex.ToString().Replace("'", "''"));
                }
                #endregion
                break;
            default:
                #region default
                try
                {
                    var idTransacao = long.Parse(e.Parameter);
                    cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value,
                        "Inicia processo de verificação de assinatura", "ID transação:" + idTransacao);
                    BatchSignatureStatusRespType respostaVerificacaoAssinatura =
                        VerificaoSituacaoAssinaturaDigitalArquivo(idTransacao);
                    StatusType statusVerificacaoAssinatura = respostaVerificacaoAssinatura.Status;
                    RegistraStatusOperacao("Verificação assinatura", statusVerificacaoAssinatura);
                    if (statusVerificacaoAssinatura.StatusCode == 110)
                    {
                        e.Result = e.Parameter;
                    }
                    else
                    {
                        e.Result = string.Empty;
                        if (statusVerificacaoAssinatura.StatusCode == 100)
                        {
                            PersisteAssinaturaArquivos(respostaVerificacaoAssinatura);
                            cDados.FinalizaOperacaoCritica(CodigoOperacao.Value);
                            ApagaArquivosTemporariosRelatorioOfiocio();
                            CodigoOperacao = null;
                        }
                        else
                        {
                            e.Result = string.Format("Assinatura digital do documento não realizada. '{0}'",
                                statusVerificacaoAssinatura.StatusDetail.Replace("'", "''"));
                            ApagaArquivosTemporariosRelatorioOfiocio();
                        }
                    }
                }
                catch (Exception ex)
                {
                    e.Result = ex.Message;
                    cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, e.Result.Replace("'", "''"), ex.ToString().Replace("'", "''"));
                }
                #endregion
                break;
        }
    }

    private void PersisteAssinaturaArquivos(BatchSignatureStatusRespType respostaVerificacaoAssinatura)
    {
        foreach (var documentoAssinado in respostaVerificacaoAssinatura.DocumentSignatureStatus)
        {
            RegistraStatusOperacao("Status assinatura arquivo", documentoAssinado.Status);
            if (documentoAssinado.Status.StatusCode == 140)
            {
                cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value,
                    "Inicia processo de efetivação de assinatura do arquivo",
                    "ID documento:" + documentoAssinado.TransactionId);
                EfetivaAssinaturaDigitalArquivo(documentoAssinado.DocumentName, documentoAssinado.Signature);
            }
        }
    }

    private BatchSignatureStatusRespType VerificaoSituacaoAssinaturaDigitalArquivo(long idTransacao)
    {
        Integrador integrador = ObtemInstanciaIntegradorCertillion();
        BatchSignatureStatusRespType respostaVerificacaoAssinatura = integrador.
            ObtemResultadosRequisicaoAssinaturaDigitalArquivos(idTransacao);
        return respostaVerificacaoAssinatura;
    }

    private void RegistraStatusOperacao(string descricao, StatusType status)
    {
        var informacaoContexto = string.Format("{0}: {1}",
            status.StatusMessage.Replace("'", "''"),
            status.StatusDetail.Replace("'", "''"));
        cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, descricao, informacaoContexto);
    }

    private void EfetivaAssinaturaDigitalArquivo(string nomeArquivo, byte[] binarioAssinatura)
    {
        string[] info = nomeArquivo.Split('_');
        long codigoFormularioAssinatura = long.Parse(info[1]);

        string caminhoDiretorio = ObtemCaminhoDiretorioArquivosAssinaturaDigital();
        string caminhoArquivo = Path.Combine(caminhoDiretorio, nomeArquivo);
        byte[] binarioArquivo = File.ReadAllBytes(caminhoArquivo);
        int resultado = ExecutaComandoAssinatura(codigoFormularioAssinatura, binarioArquivo, binarioAssinatura, string.Empty, DateTime.Now);
        if (resultado == 1)
        {
            string msgError = string.Empty;

            if (Request.QueryString["PassaFluxo"] == "S")
            {
                string comandoSQL = string.Format(@"
                                              SELECT CodigoFormularioAssinatura, 
					                                 CodigoFormulario, 
					                                 CodigoWorkflow, 
					                                 CodigoInstanciaWf,
                                                     SequenciaOcorrenciaEtapaWf,
                                                     CodigoEtapaWf
                                                FROM Log_FormularioAssinatura 
                                               WHERE CodigoFormularioAssinatura  = {0}", codigoFormularioAssinatura);

                DataSet ds = cDados.getDataSet(comandoSQL);
                DataRow dr = ds.Tables[0].Rows[0];
                var codigoWorkflow = dr["CodigoWorkflow"].ToString();
                var codigoInstanciaWf = dr["CodigoInstanciaWf"].ToString();
                var sequenciaOcorrenciaEtapaWf = dr["SequenciaOcorrenciaEtapaWf"].ToString();
                var codigoEtapaWf = dr["CodigoEtapaWf"].ToString();
                var contexto = string.Format("cw={0};ci={1};so={2};ce={3}", 
                    codigoWorkflow, codigoInstanciaWf, sequenciaOcorrenciaEtapaWf, codigoEtapaWf);
                AssinaOficio(int.Parse(codigoWorkflow), int.Parse(codigoInstanciaWf));
                cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Efetiva assinatura digital e processa ação workflow", contexto);
                cDados.ProcessaAcaoWorkflow(idUsuarioLogado.ToString(), codigoWorkflow, codigoInstanciaWf, sequenciaOcorrenciaEtapaWf, codigoEtapaWf, true, out msgError);
            }
            if (string.IsNullOrEmpty(msgError))
                cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Arquivo assinado", "CFA:" + codigoFormularioAssinatura);
            else
                cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Falha ao passar de etapa", msgError);
        }
    }

    private void AssinaOficio(int codigoWorkflow, int codigoInstanciaWf)
    {
        var comandoSql = string.Format(@"
DECLARE @CodigoWorkflow int, 
        @CodigoInstanciaWf int, 
        @codigoUsuarioLogado int
    SET @CodigoWorkflow = {0}
    SET @CodigoInstanciaWf = {1}
    SET @codigoUsuarioLogado = {2}
EXEC p_pbh_AssinaOficio @CodigoWorkflow, @CodigoInstanciaWf, @codigoUsuarioLogado, 'S'",
        codigoWorkflow, codigoInstanciaWf, idUsuarioLogado);
        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    private string[] ObtemArquivosAssinatura()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @comiteDeliberacao VARCHAR(10),
        @codigoUsuarioLogado int,
        @codigoEntidade int,
        @efetivarAssinatura char(1)
    SET @comiteDeliberacao = '{0}'
    SET @codigoUsuarioLogado = {1}
    SET @codigoEntidade = {2}
    SET @efetivarAssinatura = '{3}'

DECLARE @CodigoWorkflow int, 
        @CodigoInstanciaWf int, 
        @CodigoOficio int

DECLARE @tbl_temp TABLE
(
        CodigoFormularioAssinar bigint, 
        CodigoWorkflow int, 
        CodigoInstanciaWf int, 
        CodigoOficio int
)

DECLARE db_cursor CURSOR FOR
 SELECT CodigoWorkflow,
        CodigoInstanciaWf,
        CodigoOficio
   FROM f_pbh_GetOficiosAssinar(@comiteDeliberacao, @codigoUsuarioLogado, @codigoEntidade)
  WHERE CodigoOficio IN ({4})

OPEN db_cursor
FETCH NEXT FROM db_cursor INTO @CodigoWorkflow, @CodigoInstanciaWf, @CodigoOficio

WHILE @@FETCH_STATUS = 0
BEGIN
    EXEC p_pbh_AssinaOficio @CodigoWorkflow, @CodigoInstanciaWf, @codigoUsuarioLogado, 'N'

     INSERT INTO @tbl_temp
     SELECT CodigoFormularioAssinar, 
            @CodigoWorkflow, 
            @CodigoInstanciaWf,
            @CodigoOficio
       FROM f_pbh_GetOficiosAssinar(@comiteDeliberacao, @codigoUsuarioLogado, @codigoEntidade)
      WHERE CodigoOficio  = @CodigoOficio

    FETCH NEXT FROM db_cursor INTO @CodigoWorkflow, @CodigoInstanciaWf, @CodigoOficio
END

CLOSE db_cursor
DEALLOCATE db_cursor

 SELECT * FROM @tbl_temp", comiteDeliberacao, idUsuarioLogado, codigoEntidade, "N", codigos);

        #endregion

        var listaArquivosAssinatura = new List<string>();
        var ds = cDados.getDataSet(comandoSql);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            int codigoOficio = row.Field<int>("CodigoOficio");
            int codigoWorkflow = row.Field<int>("CodigoWorkflow");
            int codigoInstanciaWf = row.Field<int>("CodigoInstanciaWf");
            long codigoFormularioAssinatura = row.IsNull("CodigoFormularioAssinar") ? -1 : row.Field<long>("CodigoFormularioAssinar");
            string caminhoArquivo = GeraArquivoRelatorioOficio(codigoOficio, codigoWorkflow, codigoInstanciaWf, codigoFormularioAssinatura);
            listaArquivosAssinatura.Add(caminhoArquivo);
        }
        return listaArquivosAssinatura.ToArray();
    }

    private void ApagaArquivosTemporariosRelatorioOfiocio(params string[] arquivosAssinatura)
    {
        if (arquivosAssinatura == null || arquivosAssinatura.Length == 0)
        {
            var caminhoDiretorio = ObtemCaminhoDiretorioArquivosAssinaturaDigital();
            arquivosAssinatura = Directory.GetFiles(caminhoDiretorio, "cf*.pdf");
        }

        foreach (var caminhoArquivo in arquivosAssinatura)
        {
            if (File.Exists(caminhoArquivo))
                File.Delete(caminhoArquivo);
        }
    }

    private BatchSignatureRespTypeV2 EnviaRequisicaoAssinaturaArquivos(string[] listaArquivosAssinatura)
    {
        Integrador integrador = ObtemInstanciaIntegradorCertillion();
        string identificacaoUsuario = ObtemIdentificacaoUsuario();
        string mensagemInformativa = ObtemMensagemInformativa();
        string[] arquivosAssinatura = listaArquivosAssinatura;
        BatchSignatureRespTypeV2 resposta = integrador.RealizaRequisicaoAssinaturaDigitalArquivos(
            identificacaoUsuario, mensagemInformativa, arquivosAssinatura);

        return resposta;
    }

    private Integrador ObtemInstanciaIntegradorCertillion()
    {
        var dr = cDados.getParametrosSistema(
            "SUCC_URLWebServiceUploadArquivosAssinados",
            "SUCC_URLWebServiceCertillion").Tables[0].Rows[0];
        var fileUploadServiceUrl = dr.Field<string>("SUCC_URLWebServiceUploadArquivosAssinados");
        var fileSigningServiceUrl = dr.Field<string>("SUCC_URLWebServiceCertillion");
        var integrador = new Integrador(fileUploadServiceUrl, fileSigningServiceUrl);
        return integrador;
    }

    private string ObtemMensagemInformativa()
    {
        var dr = cDados.getParametrosSistema("SUCC_TextoOrientacaoUsuarioAssinatura").Tables[0].Rows[0];
        var textoOrientacaoUsuarioAssinatura = dr.Field<string>("SUCC_TextoOrientacaoUsuarioAssinatura");
        return textoOrientacaoUsuarioAssinatura;
    }

    private string ObtemIdentificacaoUsuario()
    {
        var comandoSql = string.Format("SELECT EMail FROM Usuario WHERE CodigoUsuario = {0}", idUsuarioLogado);
        var dataRow = cDados.getDataSet(comandoSql).Tables[0].Rows[0];
        return dataRow.Field<string>("EMail");
    }

    private string GeraArquivoRelatorioOficio(int codigoOficio, int codigoWorkflow, int codigoInstanciaWf, long codigoFormularioAssinatura)
    {
        var dataHoraFormatada = DateTime.Now.ToString("yyyyMMddHHmmss");
        var nomeArquivo = string.Format("CF_{0}_{1}_{2}_{3}_{4}_{5}.pdf", codigoFormularioAssinatura, codigoOficio, codigoWorkflow, codigoInstanciaWf, idUsuarioLogado, dataHoraFormatada);
        var caminhoDiretorio = ObtemCaminhoDiretorioArquivosAssinaturaDigital();
        var caminhoArquivo = Path.Combine(caminhoDiretorio, nomeArquivo);
        var rel = new relOficioDemanda(codigoWorkflow, codigoInstanciaWf, idUsuarioLogado, comiteDeliberacao);
        rel.Parameters["pComiteDeliberacao"].Value = comiteDeliberacao;
        rel.Parameters["pMostraChave"].Value = "S";
        rel.CreateDocument();
        rel.ExportToPdf(caminhoArquivo);
        return caminhoArquivo;
    }

    private string ObtemCaminhoDiretorioArquivosAssinaturaDigital()
    {
        return Path.Combine(Request.ServerVariables["APPL_PHYSICAL_PATH"], "_CertificadoDigital", "TempOrigem");
    }

    protected void callback_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
    {
        e.Properties["cpIntervaloVerificacaoStatusAssinatura"] = ObtemIntervaloVerificacaoStatusAssinatura();
    }

    private object ObtemIntervaloVerificacaoStatusAssinatura()
    {
        var dr = cDados.getParametrosSistema("SUCC_IntervaloVerificacaoStatusAssinatura").Tables[0].Rows[0];
        var intervaloVerificacaoStatusAssinatura = dr.Field<string>("SUCC_IntervaloVerificacaoStatusAssinatura");
        return intervaloVerificacaoStatusAssinatura;
    }

    private int ExecutaComandoAssinatura(long codigoFormularioAssinatura, byte[] conteudoArquivo, byte[] binarioAssinatura, string descricaoErroAssinatura, DateTime dataAssinatura)
    {
        var resultado = (string.IsNullOrEmpty(descricaoErroAssinatura)) ? 1 : 0;
        var connectionString = cDados.classeDados.getStringConexao();
        var conn = new SqlConnection(connectionString);
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
}