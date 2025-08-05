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

public partial class wfEngineInterno : System.Web.UI.Page
{
    dados cDados;
    string resolucaoCliente;
    public string alturaFrameFluxo;
    public string alturaFrameForms;
    private int larguraTela;

    private int CodigoWorkflow = -1;
    private int? CodigoInstanciaWf = null;
    private int? CodigoEtapaAtual = null;
    private int? OcorrenciaAtual = null;
    private int? CodigoProjeto = null;
    private int? CodigoFluxo = null;
    private string targetDirecionamento = "_self";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        if (!IsPostBack)
            cDados.setInfoSistema("CodigoProjeto", null);

        if (Request.QueryString["CW"] == null)
            return;

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
        
        //Gambis para contornar problema do IE 8 de PBH
        if (cDados.getInfoSistema("ResolucaoCliente") == null && cDados.getInfoSistema("ResolucaoCliente2") != null)
            cDados.setInfoSistema("ResolucaoCliente", cDados.getInfoSistema("ResolucaoCliente2").ToString());


        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        alturaFrameFluxo = (int.Parse(getAlturaTela()) - 20).ToString();
        larguraTela = getLarguraTela() + 5;

        if (Request.QueryString["PND"] != null)
            larguraTela = larguraTela - 20;

        if (Request.QueryString["PND"] != null)
        {
            int altura;
            if (int.TryParse(alturaFrameFluxo, out altura))
            {
                alturaFrameFluxo = (altura + 20 + 70).ToString();
            }
        }
        else if (((CodigoFluxo > 0) && (CodigoProjeto > 0)) || Request.QueryString["TL"] + "" == "CHI")
        {
            int altura;
            if (int.TryParse(alturaFrameFluxo, out altura))
            {                
                alturaFrameFluxo = (altura + 20 - 20).ToString();
            }
        }

        alturaFrameForms = alturaFrameFluxo;

        //frmCriteriosPendente será verificado na classe Workflow.cs
        hfFormCriterio.Set("frmCriteriosPendente", "");

        if (CodigoWorkflow > 0)
        {
            string CssFilePath = "~/App_Themes/MaterialCompact/{0}/styles.css";
            string CssPostFix = "MaterialCompact";
            string strAdd = "";

            string engine = "";

            // se a chamada é via tela de projeto, passa a largura da tela para desenhar o workflow
            //if (((CodigoFluxo > 0) && (CodigoProjeto > 0)) || Request.QueryString["TL"] + "" == "CHI")
            //{
            //    strAdd = "&WSCR=" + larguraTela.ToString() + "&CFX=" + CodigoFluxo;
            //    //engine = "&";
            //}
            //else
            //{
            engine = "&Engine=S&";
            //}

            string strReadOnly = "";

            if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
                strReadOnly = "&RO=S";

            string acessoEtapaInicial = "";

            if (Request.QueryString["AEI"] != null)
                acessoEtapaInicial = "&AEI=" + Request.QueryString["AEI"] + "&";

            //Seta Altura para formulários dinâmicos
            //if (Request.QueryString["Altura"] != null && Request.QueryString["Altura"].ToString() != "")
            //{
            //    alturaFrameFluxo = (int.Parse(Request.QueryString["Altura"].ToString()) - 70).ToString();
            //    alturaFrameForms = (int.Parse(Request.QueryString["Altura"].ToString()) - 5).ToString();
            //}

            if (Request.QueryString["Largura"] != null && Request.QueryString["Largura"].ToString() != "")
                larguraTela = int.Parse(Request.QueryString["Largura"].ToString());

            string popup = "";

            if (Request.QueryString["Popup"] != null)
                popup = "&Popup=" + Request.QueryString["Popup"] + "&";

            string formularioEdicao;
            if (Request.QueryString["ModuloMenuLDP"] != null)
                formularioEdicao = "wfRenderizaFormulario.aspx?AT=" + alturaFrameForms + "&WSCR=" + (larguraTela) + strAdd + "&CWF=" + CodigoWorkflow + strReadOnly + engine + acessoEtapaInicial + popup + "&AlturaWf=" + alturaFrameFluxo.ToString() + "&ModuloMenuLDP=" + Request.QueryString["ModuloMenuLDP"] + "&";
            else if (Request.QueryString["Demandas"] != null)
                formularioEdicao = "wfRenderizaFormulario.aspx?AT=" + alturaFrameForms + "&WSCR=" + (larguraTela) + strAdd + "&CWF=" + CodigoWorkflow + strReadOnly + engine + acessoEtapaInicial + popup + "&AlturaWf=" + alturaFrameFluxo.ToString() + "&Demandas=" + Request.QueryString["Demandas"] + "&";
            else if (Request.QueryString["PlanosInvestimento"] != null)
                formularioEdicao = "wfRenderizaFormulario.aspx?AT=" + alturaFrameForms + "&WSCR=" + (larguraTela) + strAdd + "&CWF=" + CodigoWorkflow + strReadOnly + engine + acessoEtapaInicial + popup + "&AlturaWf=" + alturaFrameFluxo.ToString() + "&PlanosInvestimento=" + Request.QueryString["PlanosInvestimento"] + "&";
            else
                formularioEdicao = "wfRenderizaFormulario.aspx?AT=" + alturaFrameForms + "&WSCR=" + (larguraTela) + "&AlturaWf=" + alturaFrameFluxo.ToString() + strAdd + "&CWF=" + CodigoWorkflow + strReadOnly + engine + acessoEtapaInicial + popup;
            string urlDestinoAposExecutarAcao;

            string paramCHI = Request.QueryString["TL"] + "" != "" ? "&TL=" + Request.QueryString["TL"] : "";

            if ((CodigoFluxo > 0) && (CodigoProjeto > 0))
                urlDestinoAposExecutarAcao = "./_Portfolios/listaProcessosInterno.aspx?CF=" + CodigoFluxo + "&CP=" + CodigoProjeto + "&CW=" + CodigoWorkflow + strReadOnly + acessoEtapaInicial + paramCHI;
            else
                urlDestinoAposExecutarAcao = "./_Portfolios/listaProcessosInterno.aspx" + paramCHI.Replace('&', '?');

            if (Request.QueryString["Obras"] != null && Request.QueryString["Obras"].ToString() == "S")
                urlDestinoAposExecutarAcao = "./_VisaoNE/FrmListaObras.aspx";

            if (Request.QueryString["Demandas"] != null && Request.QueryString["Demandas"].ToString() == "S")
                urlDestinoAposExecutarAcao = "./_Demandas/FrmListaDemandas.aspx";

            if (Request.QueryString["PlanosInvestimento"] != null && Request.QueryString["PlanosInvestimento"].ToString() == "S")
            {
                urlDestinoAposExecutarAcao = "./_Demandas/ListaPlanosInvestimento.aspx";
                targetDirecionamento = "_top";
            }

            if (!string.IsNullOrWhiteSpace(Request.QueryString["ModuloMenuLDP"]))
            {
                string cmm = Request.QueryString["ModuloMenuLDP"];
                if (cmm.ToLower().Equals("todos"))
                    urlDestinoAposExecutarAcao = "./_Processos/Visualizacao/index.aspx";
                else
                    urlDestinoAposExecutarAcao = String.Format("./_Processos/Visualizacao/index.aspx?cmm={0}", cmm);
                targetDirecionamento = "_top";
            }

            bool mostraBotoes = Request.QueryString["PND"] + "" != "S";
            bool indicaPopup = Request.QueryString["Popup"] + "" == "S";


            Workflow myFluxo = new Workflow(cDados.classeDados, codigoUsuarioResponsavel, IsPostBack, CodigoWorkflow, CodigoInstanciaWf, CodigoEtapaAtual, OcorrenciaAtual, CodigoProjeto, this.Page);
            Control pnFluxo = myFluxo.ObtemPainelExecucaoEtapa(CssFilePath, CssPostFix, formularioEdicao, larguraTela - 15, int.Parse(alturaFrameFluxo) - 30, urlDestinoAposExecutarAcao, targetDirecionamento, mostraBotoes, indicaPopup);

            form1.Controls.Add(pnFluxo);

            //Se não tiver Etapa Atual nem adianta verifica se o formulário Critérios esta na etapa
            if(CodigoEtapaAtual != null) { 

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

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);

            string script = @"<script type='text/Javascript' language='Javascript'>
                                    if(window.top.lpAguardeMasterPage)
                                        window.top.lpAguardeMasterPage.Hide();                                  
                                 </script>";
            ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
        }
    }

    private string getAlturaTela()
    {
        int constanteRedudora = 260;//260
        int alturaTela = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - constanteRedudora).ToString();
    }

    private int getLarguraTela()
    {
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 10;
        if (((CodigoFluxo > 0) && (CodigoProjeto > 0)) || Request.QueryString["TL"] + "" == "CHI")
        {
            largura -= 190;
        }
        return largura;
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
