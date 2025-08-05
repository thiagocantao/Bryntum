using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class _CertificadoDigital_assinaturaMultiplosFluxos : System.Web.UI.Page
{
    dados cDados;
    string codigos;
    int codigoEntidade;
    int idUsuarioLogado;

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
        idUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));

        codigos = Request.QueryString["codigos"];
        if (!IsPostBack && !string.IsNullOrWhiteSpace(codigos))
        {
            long codigoOperacao = cDados.RegistraOperacaoCritica("ASSNOFICIOS", idUsuarioLogado, codigoEntidade);
            cDados.RegistraPassoOperacaoCritica(codigoOperacao, "Escopo do processo", string.Format("Ofícios: {0}", codigos));
            cDados.RegistraPassoOperacaoCritica(codigoOperacao, "Configuracao Applet", Request.IsSecureConnection ? "https" : "http");
            cDados.setInfoSistema("CodigoOperacaoCritica", codigoOperacao);
            ConfiguraApplet();
        }

        string comandoSQL = string.Format(@"
        SELECT Texto FROM TextoPadraoSistema WHERE IniciaisTexto = 'AssinatDigital'");

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            lblTextoInformacao.Text = ds.Tables[0].Rows[0]["Texto"].ToString();
    }

    private void ConfiguraApplet()
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoWorkflow, 
					 CodigoInstanciaWf,
                     CodigoOficio
                FROM f_pbh_GetOficiosAssinar('{1}', {2}, {3})
               WHERE CodigoOficio IN ({0})", codigos, Request.QueryString["CD"], idUsuarioLogado, codigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds != null)
        {
            string arquivosConteudoApplettml = "";
            string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy"); //Verificar o formato da data com a ESEC
            DataTable dtParametros = cDados.getParametrosSistema("indicaAmbienteHttps").Tables[0];
            bool indicaAmbienteHttps;
            if (dtParametros.Rows.Count > 0)
                indicaAmbienteHttps = (dtParametros.Rows[0]["indicaAmbienteHttps"] as string) == "S";
            else
                indicaAmbienteHttps = Request.IsSecureConnection;
            string url = indicaAmbienteHttps ? 
                "https://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath :
                Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
            byte idContArquivo = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int codigoWorkflow = row.Field<int>("CodigoWorkflow");
                int codigoInstanciaWf = row.Field<int>("CodigoInstanciaWf");
                int codigoOficio = Convert.ToInt32(row["CodigoOficio"]);

                int regAf = 0;

                string comandoSQLProc = string.Format(@"EXEC p_pbh_AssinaOficio {0}, {1}, {2}, 'N'", codigoWorkflow, codigoInstanciaWf, idUsuarioLogado);

                cDados.execSQL(comandoSQLProc, ref regAf);

                comandoSQL = string.Format(
            @"SELECT CodigoFormularioAssinar
                FROM f_pbh_GetOficiosAssinar('{1}', {2}, {3})
               WHERE CodigoOficio  = {0}", codigoOficio, Request.QueryString["CD"], idUsuarioLogado, codigoEntidade);

                DataSet ds2 = cDados.getDataSet(comandoSQL);

                DataRow rowCF = ds2.Tables[0].Rows[0];

                int codigoFormularioAssinatura = rowCF["CodigoFormularioAssinar"] + "" == "" ? -1 : int.Parse(rowCF["CodigoFormularioAssinar"].ToString());


                string nomeArquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "_CertificadoDigital\\TempOrigem\\" + string.Format("CF{0}_{1}_{2}.pdf", codigoOficio, idUsuarioLogado, DateTime.Now.ToString("yyyyMMddHHmmss"));
                string comite = Request.QueryString["CD"];
                relOficioDemanda rel = new relOficioDemanda(codigoWorkflow, codigoInstanciaWf, idUsuarioLogado, comite);
                rel.Parameters["pComiteDeliberacao"].Value = comite;
                rel.Parameters["pMostraChave"].Value = "S";
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

            string paginaSalvaArquivoP7s = string.Format("{0}/_CertificadoDigital/salvaArquivoP7s.aspx?PassaFluxo=S&ce={1}&cu={2}", url, codigoEntidade, idUsuarioLogado);

            string indicaAmbienteTesteAssinaturaDigital = cDados.getParametrosSistema(codigoEntidade, "indicaAmbienteTesteAssinaturaDigital").Tables[0].Rows[0]["indicaAmbienteTesteAssinaturaDigital"] as string;
            string configuracoesPolitcasApplet = indicaAmbienteTesteAssinaturaDigital.ToUpper() == "N" ?
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
    }

    protected void cbExecutaAcaoServidor_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string[] parametros = e.Parameter.Split(';');
        string acao = parametros[0];
        string valor = parametros.Length == 2 ? parametros[1] : string.Empty;
        switch (acao)
        {
            case "efetiva":
                long codigoOperacao = (long)cDados.getInfoSistema("CodigoOperacaoCritica");
                cDados.setInfoSistema("CodigoOperacaoCritica", null);
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
                                e.Result = "Não foi possível concluir o processo de assinatura";
                                cDados.RegistraFalhaOperacaoCritica(codigoOperacao, "ErrVerific", e.Result);
                                return;
                            }
                        }
                    }
                }
                cDados.FinalizaOperacaoCritica(codigoOperacao);
                break;
        }
    }
}