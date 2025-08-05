using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CDIS;
using DevExpress.Web;

public partial class wfEngineInternoMobile : System.Web.UI.Page
{
    dados cDados;
    string resolucaoCliente;
    public string alturaFrameFluxo;
    public string alturaFrameForms;
    private int larguraTela;

    private string stringConexao = "";

    private int CodigoWorkflow = -1;
    private int? CodigoInstanciaWf = null;
    private int? CodigoEtapaAtual = null;
    private int? OcorrenciaAtual = null;
    private int? CodigoProjeto = null;
    private int? CodigoFluxo = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Duvida
        // como passar esses parametros
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        // Mobile 
        //Nao utiliza
        alturaFrameFluxo = "600";
        larguraTela = 800;

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
            cDados.setInfoSistema("CodigoProjeto", null);

        if (Request.QueryString["CW"] == null)
            return;


        if (Request.QueryString["SC"] != null)
            stringConexao = Request.QueryString["SC"].ToString();

        if (Request.QueryString["CF"] != null)
            CodigoFluxo = int.Parse(Request.QueryString["CF"].ToString());

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"] != "null" && Request.QueryString["CP"] + "" != "")
            CodigoProjeto = int.Parse(Request.QueryString["CP"].ToString());


        if (Request.QueryString["CW"] != null)
            CodigoWorkflow = int.Parse(Request.QueryString["CW"].ToString());
        if (Request.QueryString["CI"] != null)
            CodigoInstanciaWf = int.Parse(Request.QueryString["CI"].ToString());
        if (Request.QueryString["CS"] != null)
            OcorrenciaAtual = int.Parse(Request.QueryString["CS"].ToString());
        if (Request.QueryString["CE"] != null)
            CodigoEtapaAtual = int.Parse(Request.QueryString["CE"].ToString());

        // se tem instância e tem etapa
        string idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();

        int codigoUsuarioResponsavel = int.Parse(idUsuarioLogado);


        hfFormCriterio.Set("frmCriteriosPendente", "");

        if (CodigoWorkflow > 0)
        {
            // Mobile - Adaptar Responsive
            string CssFilePath = "~/App_Themes/Aqua/{0}/styles.css";
            string CssPostFix = "Aqua";

            string strAdd = "";

            string engine = "";
            engine = "&Engine=S&";

            string strReadOnly = "";

            if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
                strReadOnly = "&RO=S";

            string acessoEtapaInicial = "";

            if (Request.QueryString["AEI"] != null)
                acessoEtapaInicial = "&AEI=" + Request.QueryString["AEI"] + "&";
            /*
            if (Request.QueryString["Altura"] != null && Request.QueryString["Altura"].ToString() != "")
            {
                alturaFrameFluxo = Request.QueryString["Altura"].ToString();
                alturaFrameForms = (int.Parse(Request.QueryString["Altura"].ToString()) - 5).ToString();
            }

            if (Request.QueryString["Largura"] != null && Request.QueryString["Largura"].ToString() != "")
                larguraTela = int.Parse(Request.QueryString["Largura"].ToString());
                */

            string formularioEdicao = "wfRenderizaFormularioMobile.aspx?CWF=" + CodigoWorkflow + strReadOnly + engine + acessoEtapaInicial;
            //string formularioEdicao = "wfRenderizaFormulario.aspx?AT=" + alturaFrameForms + "&WSCR=" + (larguraTela) + "&AlturaWf=" + alturaFrameFluxo.ToString() + strAdd + "&CWF=" + CodigoWorkflow + strReadOnly + engine + acessoEtapaInicial;
            string urlDestinoAposExecutarAcao = "./espacoTrabalho/FrmListaPendenciasMobileWf.aspx";


            bool mostraBotoes = Request.QueryString["PND"] + "" != "S";

            WorkflowMobile myFluxo = new WorkflowMobile(cDados.classeDados, codigoUsuarioResponsavel, IsPostBack, CodigoWorkflow, CodigoInstanciaWf, CodigoEtapaAtual, OcorrenciaAtual, CodigoProjeto, this.Page, stringConexao);
            Control pnFluxo = myFluxo.ObtemPainelExecucaoEtapa(CssFilePath, CssPostFix, formularioEdicao, larguraTela - 15, int.Parse(alturaFrameFluxo), urlDestinoAposExecutarAcao, mostraBotoes);

            form1.Controls.Add(pnFluxo);

            //Se não tiver Etapa Atual nem adianta verifica se o formulário Critérios esta na etapa
            if (CodigoEtapaAtual != null)
            {

                string comandoSQL = string.Format(
                    @"SELECT 1
                        FROM
                        [dbo].[FormulariosEtapasWf] AS [fewf]
                        INNER JOIN [dbo].[ModeloFormulario] AS [mf] ON
                        ( mf.[CodigoModeloFormulario] = fewf.[CodigoModeloFormulario]
                        AND mf.[DataExclusao] IS NULL
                        AND mf.[IniciaisFormularioControladoSistema] = 'CRITERIOS' )
                        WHERE
                        fewf.[CodigoWorkflow] = {0}
                        AND fewf.[CodigoEtapaWf] = {1}
                        AND fewf.[PreenchimentoObrigatorio] = 1"
                    , CodigoWorkflow
                    , CodigoEtapaAtual);

                DataSet ds = cDados.getDataSet(comandoSQL);

                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    if (CodigoProjeto == null)
                        CodigoProjeto = cDados.getCodigoProjetoInstanciaFluxo(CodigoWorkflow, CodigoInstanciaWf.Value);

                    comandoSQL = string.Format(@"DECLARE @CodigoProjeto Int
      
                                                SET @CodigoProjeto = {0}

                                                SELECT COUNT(1) AS QTD
                                                  FROM dbo.[ProjetoCriterioSelecao] AS [pcs] 
                                                 WHERE pcs.CodigoProjeto = @CodigoProjeto", CodigoProjeto);

                    DataSet dsFormObr = cDados.getDataSet(comandoSQL);

                    if (cDados.DataSetOk(dsFormObr) && cDados.DataTableOk(dsFormObr.Tables[0]) && int.Parse(dsFormObr.Tables[0].Rows[0][0].ToString()) != 0)
                        hfFormCriterio.Set("frmCriteriosPendente", "");
                    else
                        hfFormCriterio.Set("frmCriteriosPendente", "Sim");
                }
            }
        }
    }


    protected void callbackParecer_Callback(object source, CallbackEventArgs e)
    {
        string codigoAcaoParam = e.Parameter.Split(';')[0];
        string codigoWfParam = e.Parameter.Split(';')[1];
        string codigoEtapaParam = e.Parameter.Split(';')[2];
        string indicaCertificacaoDigitalParam = e.Parameter.Split(';')[3];
        string nomeAcaoParam = e.Parameter.Split(';')[4];
        string codigoInstanciaParam = e.Parameter.Split(';')[5];
        string sequenciaEtapaParam = e.Parameter.Split(';')[6];
        int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        string possuiPendencia = "N";

        string comandoSQL = string.Format(@"
        SELECT dbo.f_wf_verificaPendenciaTramitacaoEtapaFluxo({0}, {1}, {2}, {3}, {4}) AS PossuiPendencias"
            , codigoWfParam
            , codigoInstanciaParam
            , codigoEtapaParam
            , sequenciaEtapaParam
            , codigoUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            possuiPendencia = ds.Tables[0].Rows[0]["PossuiPendencias"].ToString();


        callbackParecer.JSProperties["cp_Msg"] = possuiPendencia == "S" ? "O parecer da tramitação é obrigatório!" : "";
        callbackParecer.JSProperties["cp_CodigoAcao"] = codigoAcaoParam;
        callbackParecer.JSProperties["cp_CWF"] = codigoWfParam;
        callbackParecer.JSProperties["cp_CE"] = codigoEtapaParam;
        callbackParecer.JSProperties["cp_ICD"] = indicaCertificacaoDigitalParam;
        callbackParecer.JSProperties["cp_NA"] = nomeAcaoParam;
    }

}
