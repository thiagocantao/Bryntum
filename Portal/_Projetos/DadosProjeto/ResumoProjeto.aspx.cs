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
using System.IO;
using System.Globalization;
using System.Drawing;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_ResumoProjeto : System.Web.UI.Page
{
    dados cDados;

    public int codigoProjeto = 0;
    public int larguraGrafico = 400;
    public int alturaGraficos = 166;
    public string grafico1 = "grafico_001", grafico2 = "grafico_002", grafico3 = "grafico_003", grafico4 = "grafico_004", grafico5 = "grafico_005", grafico6 = "", urlGrafico06 = "";
    double diasAtualizacao = -1;
    public string tipoTela = "P";
    public string paramFin = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/FusionCharts.js?v=1""></script>"));
        this.TH(this.TS("FusionCharts"));
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        if (Request.QueryString["TipoTela"] != null && Request.QueryString["TipoTela"].ToString() != "")
        {
            tipoTela = Request.QueryString["TipoTela"].ToString();
        }

        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_CnsStt");
        }
        DataSet dsParamMostrarFiltroFinanceiroProjetos = cDados.getParametrosSistema(codigoEntidade, "mostrarFiltroFinanceiroProjetos");
        if(cDados.DataSetOk(dsParamMostrarFiltroFinanceiroProjetos) && 
            cDados.DataTableOk(dsParamMostrarFiltroFinanceiroProjetos.Tables[0]) && 
            dsParamMostrarFiltroFinanceiroProjetos.Tables[0].Rows[0]["mostrarFiltroFinanceiroProjetos"].ToString() == "S")
        {
            paramFin = "&Financeiro=" + (Request.QueryString["Financeiro"] + "" == "A" ? DateTime.Now.Year : -1);
        }       

        DataSet dsParametro = cDados.getParametrosSistema("projeto_Grafico001", "projeto_Grafico002", "projeto_Grafico003", "projeto_Grafico004", "projeto_Grafico005", "projeto_Grafico006", "diasAtualizacaoProjeto", "mostraRankingCarteira");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            grafico1 = dsParametro.Tables[0].Rows[0]["projeto_Grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["projeto_Grafico001"] + "";
            grafico2 = dsParametro.Tables[0].Rows[0]["projeto_Grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["projeto_Grafico002"] + "";
            grafico3 = dsParametro.Tables[0].Rows[0]["projeto_Grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["projeto_Grafico003"] + "";
            grafico4 = dsParametro.Tables[0].Rows[0]["projeto_Grafico004"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["projeto_Grafico004"] + "";
            grafico5 = dsParametro.Tables[0].Rows[0]["projeto_Grafico005"] + "" == "" ? grafico5 : dsParametro.Tables[0].Rows[0]["projeto_Grafico005"] + "";
            grafico6 = (dsParametro.Tables[0].Rows[0]["projeto_Grafico006"] + "").Trim() == "" ? grafico6 : dsParametro.Tables[0].Rows[0]["projeto_Grafico006"] + "";

            if (grafico6 != "")
            {
                urlGrafico06 = "./graficos/" + grafico6 + ".aspx?FRM=frm06&IDProjeto=" + codigoProjeto + "&TipoTela=" + tipoTela + paramFin;
            }

            if (dsParametro.Tables[0].Rows[0]["diasAtualizacaoProjeto"] + "" != "")
            {
                diasAtualizacao = double.Parse(dsParametro.Tables[0].Rows[0]["diasAtualizacaoProjeto"] + "");
            }

            if (dsParametro.Tables[0].Rows[0]["mostraRankingCarteira"] + "" != "S")
            {
                trRanking.Style.Add("display", "none");
            }
            else
            {
                string comandoSQL = string.Format(@"SELECT ISNULL(dbo.f_GetRankingProjetoCarteira({0}, {1}), '--') AS Ranking", codigoProjeto, codigoEntidade);

                DataSet dsRanking = cDados.getDataSet(comandoSQL);

                if (cDados.DataSetOk(dsRanking) && cDados.DataTableOk(dsRanking.Tables[0]))
                    lblRanking.Text = dsRanking.Tables[0].Rows[0]["Ranking"].ToString();

                comandoSQL = string.Format(@"SELECT ISNULL(dbo.f_GetCarteiraPrincipalProjeto({0}), '--') AS Carteira", codigoProjeto);

                DataSet dsCarteira = cDados.getDataSet(comandoSQL);

                if (cDados.DataSetOk(dsCarteira) && cDados.DataTableOk(dsCarteira.Tables[0]))
                    lblCarteira.Text = dsCarteira.Tables[0].Rows[0]["Carteira"].ToString();
            }
        }

        defineTamanhoObjetos();
        preencheCampos();
        getLabelsBancoDados();

        if (tipoTela == "D")
        {
            //lblTituloCategoria.Visible = false;
            //lblCategoria.Visible = false;
            lblTituloGer.Text = "Responsável:";
        }

        string estiloFooter = "dxgvControl dxgvGroupPanel";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

        tbBotoes.Attributes.Add("class", estiloFooter);

        tbBotoes.Style.Add("padding", "3px");

        tbBotoes.Style.Add("border-collapse", "collapse");

        tbBotoes.Style.Add("border-bottom", "none");
    }

    //Preenche os numeros do projeto
    private void preencheCampos()
    {
        DataSet dsDados = cDados.getDadosGeraisProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            //Preenche os campos de texto de Dados do Projeto
            lblUnidadeNegocio.Text = dt.Rows[0]["Unidade"] + "" == "" ? "---" : dt.Rows[0]["Unidade"].ToString();
            lblInicio.Text = dt.Rows[0]["InicioReprogramado"] + "" == "" ? "---" : ((DateTime)dt.Rows[0]["InicioReprogramado"]).ToShortDateString();
            lblTermino.Text = dt.Rows[0]["TerminoReprogramado"] + "" == "" ? "---" : ((DateTime)dt.Rows[0]["TerminoReprogramado"]).ToShortDateString();
            lblGerente.Text = dt.Rows[0]["Gerente"] + "" == "" ? "---" : dt.Rows[0]["Gerente"].ToString();
            //lblCategoria.Text = dt.Rows[0]["SiglaCategoria"] + "" == "" ? "---" : dt.Rows[0]["SiglaCategoria"].ToString();

            if (dt.Rows[0]["UltimaAtualizacao"] + "" == "")
            {
                lblAtualizacao.Text = string.Format("{0} {1}", Resources.traducao.atualiza__o_, "-- /--/----");

            }
            else
            {
                lblAtualizacao.Text = string.Format("{0} {1}", Resources.traducao.atualiza__o_, ((DateTime)dt.Rows[0]["UltimaAtualizacao"]).ToShortDateString());
            }

            if (dt.Rows[0]["DataUltimaAtualizacao"] != null && dt.Rows[0]["DataUltimaAtualizacao"].ToString() != "" && ((DateTime)dt.Rows[0]["DataUltimaAtualizacao"]).AddDays(diasAtualizacao) < DateTime.Now)
                lblAtualizacao.ForeColor = Color.Red;

            formataLinkDataAtualizacao();
        }
    }

    private void formataLinkDataAtualizacao()
    {
        DataSet dsDados = cDados.getItensProjetoAtualizacaoMonitorada(codigoProjeto, "");
        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            if (dsDados.Tables[0].Rows.Count > 1)
            {
                lblAtualizacao.Style.Add("cursor", "pointer");
                lblAtualizacao.Font.Underline = true;
                lblAtualizacao.ClientSideEvents.Click = "function(s,e) { pcEventosAtlPrj.Show(); }"; // Esse aqui funciona!!!
                //lblAtualizacao.SetClientSideEventHandler("click", "pcEventosAtlPrj.Show();");
                //lblAtualizacao.Attributes.Add("onclick", "pcEventosAtlPrj.Show();");
                gvEventosAtlPrj.Settings.VerticalScrollableHeight = 250;
                gvEventosAtlPrj.Settings.ShowFilterRow = false;
                gvEventosAtlPrj.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
                gvEventosAtlPrj.SettingsLoadingPanel.Mode = DevExpress.Web.GridViewLoadingPanelMode.Disabled;
                gvEventosAtlPrj.DataSource = dsDados;
                gvEventosAtlPrj.DataBind();
            }
        }
    }

    private void getLabelsBancoDados()
    {
        DataSet ds = cDados.getParametrosSistema("labelGerente");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string labelGerente = ds.Tables[0].Rows[0]["labelGerente"] + "" != "" ? ds.Tables[0].Rows[0]["labelGerente"] + "" : "Gerente";

            lblTituloGer.Text = labelGerente + ":";
        }
    }

    private void defineTamanhoObjetos()
    {




        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        int larguraPaineis = ((largura) / 2 - 105);
        int alturaPaineis = altura - 217;

        larguraGrafico = larguraPaineis;

        alturaGraficos = (altura - 202) / 2;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "FormAtlPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "FormAtlPrj", "Atualização Projeto", this);
    }

    #endregion    
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }
}


