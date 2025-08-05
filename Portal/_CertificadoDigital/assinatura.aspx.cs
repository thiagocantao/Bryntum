using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using Cdis.IntegraCertillion.com.certillion.labs;
using Cdis.IntegraCertillion;

public partial class _CertificadoDigital_assinatura : System.Web.UI.Page
{
    dados cDados;
    int idUsuarioLogado;
    int codigoEntidade;

    private int CodigoWorkflow = -1;
    private int? CodigoInstanciaWf = null;
    private int? CodigoEtapaAtual = null;
    private int? OcorrenciaAtual = null;
    //teste de build e commit
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

    protected void Page_Load(object sender, EventArgs e)
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
        // Usuário logado
        idUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        // parametros da URL
        if (Request.QueryString["CW"] != null)
            CodigoWorkflow = int.Parse(Request.QueryString["CW"].ToString());
        if (Request.QueryString["CIWF"] != null)
            CodigoInstanciaWf = int.Parse(Request.QueryString["CIWF"].ToString());
        if (Request.QueryString["CEWF"] != null)
            CodigoEtapaAtual = int.Parse(Request.QueryString["CEWF"].ToString());
        if (Request.QueryString["COWF"] != null)
            OcorrenciaAtual = int.Parse(Request.QueryString["COWF"].ToString());

        if (!IsPostBack)
        {
            CodigoOperacao = cDados.RegistraOperacaoCritica("ASSNOFICIOS", idUsuarioLogado, codigoEntidade);
            cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Escopo do processo", string.Format("Parâmetros da URL: {0}", Request.QueryString));
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

    private BatchSignatureRespTypeV2 RealizarRequisicaoAssinatura()
    {
        List<string> arquivosAssinatura = new List<string>();
        string comandoSQL;

        #region Comando SQL

        comandoSQL = string.Format(
    @"DECLARE @CodigoUsuario int
              DECLARE @RetornoExecucaoProc int
              DECLARE @ParametroSaidaProc varchar(5000)
              DECLARE @maxCodigoFormulario int 
               SELECT @maxCodigoFormulario = max(CodigoFormularioAssinatura) from Log_FormularioAssinatura
                  SET @CodigoUsuario = {4}
 
              Exec @RetornoExecucaoProc = p_GeraXMLFormulariosEtapa {0}, {1}, {2}, {3}, @CodigoUsuario, @ParametroSaidaProc output
               
              SELECT CodigoFormularioAssinatura, CodigoFormulario 
                FROM Log_FormularioAssinatura 
               WHERE CodigoFormularioAssinatura > @maxCodigoFormulario
                 AND CodigoUsuario = @CodigoUsuario
              
             ", CodigoWorkflow, CodigoInstanciaWf, CodigoEtapaAtual, OcorrenciaAtual, idUsuarioLogado);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSQL);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            int codigoFormulario = Convert.ToInt32(row["CodigoFormulario"]);
            int codigoFormularioAssinatura = Convert.ToInt32(row["CodigoFormularioAssinatura"]);
            string nomeArquivo = GerarArquivoImpressaoFormulario(codigoFormulario, codigoFormularioAssinatura);
            arquivosAssinatura.Add(nomeArquivo);
        }

        return EnviaRequisicaoAssinaturaArquivos(arquivosAssinatura.ToArray());
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

    private string GerarArquivoImpressaoFormulario(int codigoFormulario, int codigoFormularioAssinatura)
    {
        string nomeArquivo = ObtemNomeArquivoImpressaoFormulario(codigoFormulario);
        var rel = new rel_ImpressaoFormularios(codigoFormulario, codigoFormularioAssinatura);
        rel.CreateDocument();
        rel.ExportToPdf(nomeArquivo);
        return nomeArquivo;
    }

    private string ObtemNomeArquivoImpressaoFormulario(int codigoFormulario)
    {
        string caminhoDiretorio = ObtemCaminhoDiretorioArquivosAssinaturaDigital();
        string nomeArquivo = string.Format("CF{0}_{1}_{2:yyyyMMddHHmmss}.pdf", codigoFormulario, idUsuarioLogado, DateTime.Now);
        string caminhoArquivo = Path.Combine(caminhoDiretorio, nomeArquivo);

        return caminhoArquivo;
    }

    private string ObtemCaminhoDiretorioArquivosAssinaturaDigital()
    {
        return Path.Combine(Request.ServerVariables["APPL_PHYSICAL_PATH"], "_CertificadoDigital", "TempOrigem");
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter == "assinar")
        {
            #region assinar
            try
            {
                BatchSignatureRespTypeV2 respostaRequisicaoAssinatura = RealizarRequisicaoAssinatura();
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
        }
        else
        {
            #region verificar
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
                        CodigoOperacao = null;
                    }
                    else
                    {
                        e.Result = string.Format("Assinatura digital do documento não realizada. '{0}'",
                            statusVerificacaoAssinatura.StatusDetail.Replace("'", "''"));
                    }
                    ApagaArquivosTemporariosRelatorioOfiocio();
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, e.Result.Replace("'", "''"), ex.ToString().Replace("'", "''"));
            }
            #endregion
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
                cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Efetiva assinatura digital e processa ação workflow", contexto);
                cDados.ProcessaAcaoWorkflow(idUsuarioLogado.ToString(), codigoWorkflow, codigoInstanciaWf, sequenciaOcorrenciaEtapaWf, codigoEtapaWf, true, out msgError);
            }
            if (string.IsNullOrEmpty(msgError))
                cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Arquivo assinado", "CFA:" + codigoFormularioAssinatura);
            else
                cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, "Falha ao passar de etapa", msgError);
        }
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

    private BatchSignatureStatusRespType VerificaoSituacaoAssinaturaDigitalArquivo(long idTransacao)
    {
        Integrador integrador = ObtemInstanciaIntegradorCertillion();
        BatchSignatureStatusRespType respostaVerificacaoAssinatura = integrador.
            ObtemResultadosRequisicaoAssinaturaDigitalArquivos(idTransacao);
        return respostaVerificacaoAssinatura;
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

    private void RegistraStatusOperacao(string descricao, StatusType status)
    {
        var informacaoContexto = string.Format("{0}: {1}",
            status.StatusMessage.Replace("'", "''"),
            status.StatusDetail.Replace("'", "''"));
        cDados.RegistraPassoOperacaoCritica(CodigoOperacao.Value, descricao, informacaoContexto);
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

    /*
	protected void cbExecutaAcaoServidor_Callback(object source, DevExpress.Web.CallbackEventArgs e)
	{
		string[] parametros = e.Parameter.Split(';');
		string acao = parametros[0];
		string valor = parametros.Length == 2 ? parametros[1] : string.Empty;
		switch (acao)
		{
			case "assina":
				// provisório - Apenas para simular a chamada do ws da esec.

				// chama a procedure para criar os XML´s dos formulários
				string comandoSQL = string.Format(
					@"DECLARE @CodigoUsuario int
		              DECLARE @RetornoExecucaoProc int
		              DECLARE @ParametroSaidaProc varchar(5000)
		              DECLARE @maxCodigoFormulario int 
		               SELECT @maxCodigoFormulario = max(CodigoFormularioAssinatura) from Log_FormularioAssinatura
		                  SET @CodigoUsuario = {4}
		 
		              Exec @RetornoExecucaoProc = p_GeraXMLFormulariosEtapa {0}, {1}, {2}, {3}, @CodigoUsuario, @ParametroSaidaProc output
		               
		              SELECT CodigoFormularioAssinatura, CodigoFormulario 
		                FROM Log_FormularioAssinatura 
		               WHERE CodigoFormularioAssinatura > @maxCodigoFormulario
		                 AND CodigoUsuario = @CodigoUsuario
		              
		             ", CodigoWorkflow, CodigoInstanciaWf, CodigoEtapaAtual, OcorrenciaAtual, idUsuarioLogado);

				DataSet ds = cDados.getDataSet(comandoSQL);
				if (ds != null)
				{
					string connectionString = cDados.classeDados.getStringConexao();
					using (var conn = new SqlConnection(connectionString))
					{
						conn.Open();
						try
						{
							foreach (DataRow row in ds.Tables[0].Rows)
							{
								int codigoFormulario = Convert.ToInt32(row["CodigoFormulario"]);
								int codigoFormularioAssinatura = row.Field<int>("CodigoFormularioAssinatura");
								int retorno = ExecutaComandoAssinatura(conn, codigoFormulario, codigoFormularioAssinatura);
								if (retorno > 0)
								{
									//Sucesso
								}
								else
								{
									//Falha
									throw new Exception("Código retorno: " + retorno);
								}
							}
						}
						catch
						{
							e.Result = "Operação cancelada!\n\nFalha no processo de assinatura.";
						}
						finally
						{
							conn.Close();
						}
					}
				}
				break;
			case "efetiva":
                long codigoOperacao = (long)cDados.getInfoSistema("CodigoOperacaoCritica");
                foreach (var strCodigo in valor.Split('|'))
				{
					int codigo;
					if (int.TryParse(strCodigo, out codigo))
					{
						string comandoSql = @"SELECT CASE WHEN l.ImagemFormulario IS NOT NULL AND l.BinarioAssinatura IS NOT NULL THEN 'S' ELSE 'N' END IndicaAssinado FROM Log_FormularioAssinatura l WHERE CodigoFormularioAssinatura = " + codigo;
						DataSet dsTemp = cDados.getDataSet(comandoSql);
						if (dsTemp.Tables[0].Rows.Count > 0)
						{
							string indicaAssinado = dsTemp.Tables[0].Rows[0]["IndicaAssinado"] as string;
							if (indicaAssinado != "S")
							{
								e.Result = "Falha no processo de assinatura";
                                cDados.RegistraFalhaOperacaoCritica(codigoOperacao, "ErrVerific", e.Result);
                                break;
							}
						}
					}
				}
                //int qtdeRegistrosAfetados = EfetivaAssinaturaDigitalFormulario();
                //e.Result = qtdeRegistrosAfetados.ToString();
                cDados.FinalizaOperacaoCritica(codigoOperacao);
                break;
		}
	}

	private static int ExecutaComandoAssinatura(SqlConnection conn, int codigoFormulario, int codigoFormularioAssinatura)
	{
		byte[] conteudoArquivo = ObtemBytesConteudoArquivo(
			codigoFormulario, codigoFormularioAssinatura);

		var command = new SqlCommand();
		var pAssinatura = new SqlParameter();
		var pImagemFormulario = new SqlParameter();
		var pCodigoFormularioAssinatura = new SqlParameter();
		var pCodigoResultadoAssinatura = new SqlParameter();
		var pDescricaoErroAssinatura = new SqlParameter();
		var pDataAssinatura = new SqlParameter();
		var pValorRetorno = new SqlParameter();

		command.CommandText = "p_GravaAssinaturaDigitalFormulario";
		command.CommandType = CommandType.StoredProcedure;
		command.Connection = conn;

		#region Configura parâmetros

		//pCodigoFormularioAssinatura
		pCodigoFormularioAssinatura.ParameterName = "@in_codigoFormularioAssinatura";
		pCodigoFormularioAssinatura.Value = codigoFormularioAssinatura;
		//pCodigoResultadoAssinatura
		pCodigoResultadoAssinatura.ParameterName = "@in_codigoResultadoAssinatura";
		pCodigoResultadoAssinatura.Value = 1;
		//pDescricaoErroAssinatura
		pDescricaoErroAssinatura.ParameterName = "@in_descricaoErroAssinatura";
		pDescricaoErroAssinatura.Value = string.Empty;
		//pDataAssinatura
		pDataAssinatura.ParameterName = "@in_dataAssinatura";
		pDataAssinatura.Value = DateTime.Now;
		//pImagemFormulario
		pImagemFormulario.ParameterName = "@in_imagemFormulario";
		pImagemFormulario.SqlDbType = SqlDbType.VarBinary;
		pImagemFormulario.Size = conteudoArquivo.Length;
		pImagemFormulario.Value = conteudoArquivo;
		//pAssinatura
		pAssinatura.ParameterName = "@in_binarioAssinatura";
		pAssinatura.SqlDbType = SqlDbType.VarBinary;
		pAssinatura.Size = 0;
		pAssinatura.Value = new byte[0];
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

	private static byte[] ObtemBytesConteudoArquivo(int codigoFormulario, int codigoFormularioAssinatura)
	{
		MemoryStream stream;
		using (stream = new MemoryStream())
		{
			rel_ImpressaoFormularios rel = new rel_ImpressaoFormularios(
				codigoFormulario, codigoFormularioAssinatura);
			rel.CreateDocument();
			rel.ExportToPdf(stream);
			stream.Position = 0;
		}
		byte[] conteudoArquivo = stream.ToArray();
		return conteudoArquivo;
	}

	private bool SalvaXmlEmDisco()
	{
		// chama a procedure para criar os XML´s dos formulários
		string comandoSQL = string.Format(
			@"DECLARE @CodigoUsuario int
              DECLARE @RetornoExecucaoProc int
              DECLARE @ParametroSaidaProc varchar(5000)
              DECLARE @maxCodigoFormulario int 
               SELECT @maxCodigoFormulario = max(CodigoFormularioAssinatura) from Log_FormularioAssinatura
                  SET @CodigoUsuario = {4}
 
              Exec @RetornoExecucaoProc = p_GeraXMLFormulariosEtapa {0}, {1}, {2}, {3}, @CodigoUsuario, @ParametroSaidaProc output
               
              SELECT CodigoFormularioAssinatura, CodigoFormulario 
                FROM Log_FormularioAssinatura 
               WHERE CodigoFormularioAssinatura > @maxCodigoFormulario
                 AND CodigoUsuario = @CodigoUsuario
              
             ", CodigoWorkflow, CodigoInstanciaWf, CodigoEtapaAtual, OcorrenciaAtual, idUsuarioLogado);

		DataSet ds = cDados.getDataSet(comandoSQL);
		if (ds != null)
		{
			string arquivosConteudoApplettml = "";
			string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy"); //Verificar o formato da data com a ESEC
			string url = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
			byte idContArquivo = 0;
			foreach (DataRow row in ds.Tables[0].Rows)
			{
				int codigoFormulario = Convert.ToInt32(row["CodigoFormulario"]);
				int codigoFormularioAssinatura = row.Field<int>("CodigoFormularioAssinatura");
				//string conteudo = row.Field<string>("XMLFormulario");
				string nomeArquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "_CertificadoDigital\\TempOrigem\\" + string.Format("CF{0}_{1}_{2}.pdf", codigoFormulario, idUsuarioLogado, DateTime.Now.ToString("yyyyMMddHHmmss"));
				//File.WriteAllText(nomeArquivo, conteudo);
				rel_ImpressaoFormularios rel = new rel_ImpressaoFormularios(
				codigoFormulario, codigoFormularioAssinatura);
				rel.CreateDocument();
				rel.ExportToPdf(nomeArquivo);

				arquivosConteudoApplettml += string.Format(
				@"<param name='Arquivo.{0}' value='{1}'/>
                  <param name='Data Envio.{0}' value='{2}'/>
                  <param name='url.{0}' value= '{3}/_CertificadoDigital/TempOrigem/{1}'/> 
                  <param name='ID.{0}' value='{4}'/>

                 ", idContArquivo++, Path.GetFileName(nomeArquivo), dataEnvio, url, codigoFormularioAssinatura);
			}
			string cpf = cDados.getDataSet("SELECT CPF FROM Usuario WHERE CodigoUsuario = " + idUsuarioLogado).Tables[0].Rows[0]["CPF"] as string;//"64268688412";
			if (string.IsNullOrWhiteSpace(cpf))
				cpf = "0";
			string paginaSalvaArquivoP7s = string.Format("{0}/_CertificadoDigital/salvaArquivoP7s.aspx?PassaFluxo=N&ce={1}&cu={2}", url, codigoEntidade, idUsuarioLogado);

			string indicaAmbienteTesteAssinaturaDigital = cDados.getParametrosSistema(codigoEntidade, "indicaAmbienteTesteAssinaturaDigital").Tables[0].Rows[0]["indicaAmbienteTesteAssinaturaDigital"] as string;
			string configuracoesPolitcasApplet = (indicaAmbienteTesteAssinaturaDigital ?? "").ToUpper() == "N" ?
@"<param name='policyURL' value='http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_1.der' />
                        <param name='policyURLInSignature' value='http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_1.der' />
                        <param name='usePolicy' value='true' />
                        <param name='envelopeType' value='cades' />
                        <param name='globalField.0' value='usePolicy=true' />
                        <param name='globalField.1' value='policy=2.16.76.1.7.1.1.2.1' />" :
@"<param name='policyURL' value='http://www.esec.com.br/calab2/pa/pa_raweb.dat' />
                        <param name='policyURLInSignature' value='http://www.esec.com.br/calab2/pa/pa_raweb.dat' />
                        <param name='usePolicy' value='true' />
                        <param name='envelopeType' value='cades' />
                        <param name='globalField.0' value='usePolicy=true' />
                        <param name='globalField.1' value='policy=2.16.76.1.7.1.1.2.2' />";

			string conteudoAppletHtml = string.Format(
							@"
                    <applet code='br/com/esec/signapplet/DefaultSignApplet.class' archive='sdk-web.jar' width='1' height='1'>
                        <param name='cache_version' value='1.6.5.11'/>
                        <param name='cache_archive' value='sdk-web.jar'/>
                        <param name='sdk-base-version' value='1.6.5.11'/>
                        <param name='viewGui' value='false'/>
                        <param name='mode' value='1'/>
                        <param name='separate_jvm' value='true'/>
                        <param name='userid' value='sdk-web'/>
                        <param name='jspServer' value='{0}'/>
                        <param name='autoCommit' value='true'/>
                        <param name='nextURL' value='NO_FORWARD'/>
                        <param name='colCount' value='3'/>
                        <param name='encodedFileParam' value='ENCDATA'/>
                        <param name='encodedFileCount' value='QTYDATA'/>
                        <param name='encodedFileId' value='IDDATA'/>
                        <param name='recipientNameParam' value='RECIPIENT_NAME'/>
                        <param name='config.type' value='local'/>
                        <param name='detachedSignature' value='true'/>
                        <param name='userCPF' value ='{2}' />

                        <param name='colName.0' value='Arquivo'/>
                        <param name='colAlias.0' value='#arquivo'/>
                        <param name='colName.1' value='Data Envio'/>
                        <param name='colAlias.1' value='dataEnv'/>
                        <param name='colName.2' value='ID'/>
                        <param name='colAlias.2' value='#idArq'/>
                        {1}
                        <param name='signFunction' value='true'/>
                        <param name='encryptFunction' value='false'/>
                        <param name='checkLibs' value='true'/>
                        <param name='digestAlgorithm' value='SHA256'/>
                        <param name='generateLog' value='true'/>
                        <param name='signingAlgorithm' value='SHA256withRSA' />
						{3}
                    </applet> ",
							   paginaSalvaArquivoP7s,
							   arquivosConteudoApplettml,
							   cpf,
							   configuracoesPolitcasApplet);

			applet.InnerHtml = conteudoAppletHtml;
		}

		return false;
	}

	private int EfetivaAssinaturaDigitalFormulario()
	{
		string connectionString = cDados.classeDados.getStringConexao();
		SqlConnection conn = new SqlConnection(connectionString);
		SqlCommand command = new SqlCommand();
		conn.Open();

		command.CommandText = "p_efetivaAssinaturaDigitalFormulario";
		command.CommandType = CommandType.StoredProcedure;
		command.Connection = conn;

		command.Parameters.Add(new SqlParameter("@in_codigoWorkflow", CodigoWorkflow));
		command.Parameters.Add(new SqlParameter("@in_codigoEtapaWf", CodigoEtapaAtual));
		command.Parameters.Add(new SqlParameter("@in_codigoInstanciaWf", CodigoInstanciaWf));
		command.Parameters.Add(new SqlParameter("@in_sequenciaOcorrenciaEtapaWf", OcorrenciaAtual));

		return command.ExecuteNonQuery();
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string acao = e.Parameter;
        long codigoOperacao = (long)cDados.getInfoSistema("CodigoOperacaoCritica");
        cDados.setInfoSistema("CodigoOperacaoCritica", null);
        switch (acao)
        {
            case "cancelar":
                cDados.RegistraFalhaOperacaoCritica(codigoOperacao, "Operação cancelada", "Operação cancelada pelo usuário");
                break;
            default:
                break;
        }
    }
    */
}