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
using DevExpress.Web;
using System.Collections.Specialized;
using System.IO;
using DevExpress.XtraPrinting;
using System.Text;
using DevExpress.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using System.Linq;
using System.Drawing;

public partial class _Portfolios_frameSelecaoBalanceamento_Resumo : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;

    int codigoPortfolio = 0;
    private int codigoUsuarioLogado = 0;

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================

        if(Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
            codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
        else if (cDados.getInfoSistema("CodigoPortfolio") != null)
            codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());

        cDados.aplicaEstiloVisual(Page);
        gvProjetos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvProjetos.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control;
        defineAlturaTela();

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());


        cDados.setaDefinicoesBotoesInserirExportarSemTemplate(menu0, false, "", false, true, true, "LstSelBal", "Seleção e Balanceamento", this, gvProjetos);

        carregaComboAnos();
        carregaGrid();

        string estiloFooter = "dxgvControl dxgvGroupPanel";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

        tbBotoes.Attributes.Add("class", estiloFooter);
        tbBotoes.Style.Add("padding", "3px");
        tbBotoes.Style.Add("border-collapse", "collapse");
        tbBotoes.Style.Add("border-bottom", "none");
        cDados.traduzControles(this);
        gvProjetos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

    }

    private void carregaComboAnos()
    {
        string where = " AND IndicaAnoPeriodoEditavel = 'S'";
        DataSet dsAnos = cDados.getPeriodoAnalisePortfolio(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(dsAnos))
        {
            ddlAno.DataSource = dsAnos;

            ddlAno.TextField = "Ano";

            ddlAno.ValueField = "Ano";

            ddlAno.DataBind();

            ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

            ddlAno.Items.Insert(0, lei);

            if (!IsPostBack)
                ddlAno.SelectedIndex = 0;
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        //gvProjetos.Settings.VerticalScrollableHeight = (alturaPrincipal - 485);

    }

    protected void checkCriterio1_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio2_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio3_CheckedChanged(object sender, EventArgs e)
    {
       string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio4_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio5_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio6_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio7_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio8_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio9_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }
    
    private void carregaGrid()
    {

        if (ddlAno.Items.Count > 0)
        {
            string where = "";

            DataSet dsGrid = cDados.getProjetosPorCriterio(int.Parse(ddlAno.Value.ToString()), codigoPortfolio, codigoEntidadeUsuarioResponsavel, where);

            if (cDados.DataSetOk(dsGrid))
            {
                gvProjetos.DataSource = dsGrid;

                gvProjetos.DataBind();
            }
        }
        DataSet dsParametro = cDados.getParametrosSistema("labelDespesa");
        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            gvProjetos.Columns["Custo"].Caption = string.Format("{0}", dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString());
        }
    }

    protected void gvProjetos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {        
        if (e.Parameters.Contains(';'))
        {
            string cenario = "IndicaCenario" + e.Parameters.Substring(0, 1);
            int codigoProposta = int.Parse(e.Parameters.Substring(2));
            string selecionado = "N";
            if (codigoProposta < 0)
            {
                codigoProposta = codigoProposta * -1;
                selecionado = "S";
            }

            ListDictionary table = new ListDictionary();

            table.Add(cenario, selecionado);

            cDados.update("Projeto", table, " CodigoProjeto = " + codigoProposta);

        }
        else if (e.Parameters != string.Empty && e.Parameters != "Atualizar")
        {
            (sender as ASPxGridView).Columns[e.Parameters].Visible = false;
            (sender as ASPxGridView).Columns[e.Parameters].ShowInCustomizationForm = true;
        }
        
        carregaGrid();
        
    }

    protected void gvProjetos_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGrid();
    }

    public string getCenarioMarcado(string idCheck, string marcado, string parametroCall, string codigoProjeto)
    {
        string checado = "";

        if (marcado.ToUpper() == "S")
            checado = "checked='true'";
        else
            codigoProjeto = "-" + codigoProjeto;

        parametroCall = parametroCall + codigoProjeto;

        string disabled = "";

        //if (Request.QueryString["RO"] + "" == "S")
        disabled = "disabled='disabled'";

        return string.Format(@"<input id=""{0}"" type=""checkbox"" {1} onclick=""clickCB('{2}');"" {3}/>", idCheck, checado, parametroCall, disabled);
    }       

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenuSemTemplate((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstSelBal", gvProjetos);
        }            
    }

    protected void menu_Init(object sender, EventArgs e)
    {

    }

    #endregion
    
    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        if (e.RowType == GridViewRowType.Header)
        {
            if (e.Text == "&nbsp;")
            {
                e.Text = e.Text.Replace("&nbsp;", "");
            }
        }

        if (e.Column != null)
        {
            if (e.Column.ToolTip == "Desempenho" && e.Value != null)
            {
                Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.BrickStyle.Font = fontex;
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
                e.Text = "l";
                e.TextValue = "l";

                if (e.Value.ToString().Contains("Vermelho"))
                {

                    e.BrickStyle.ForeColor = Color.Red;
                }
                else if (e.Value.ToString().Contains("Amarelo"))
                {

                    e.BrickStyle.ForeColor = Color.Yellow;
                }
                else if (e.Value.ToString().Contains("Branco"))
                {

                    e.BrickStyle.ForeColor = Color.WhiteSmoke;
                }
                else if (e.Value.ToString().Contains("Verde"))
                {
                    e.BrickStyle.ForeColor = Color.Green;
                }
            }
            if (e.Column.Name == "IndicaCenario1" || e.Column.Name == "IndicaCenario2" || e.Column.Name == "IndicaCenario3" ||
                e.Column.Name == "IndicaCenario4" || e.Column.Name == "IndicaCenario5" || e.Column.Name == "IndicaCenario6" ||
                e.Column.Name == "IndicaCenario7" || e.Column.Name == "IndicaCenario8" || e.Column.Name == "IndicaCenario9")
            {

                if (e.Value != null && e.RowType == GridViewRowType.Data)
                {
                    Font fontex = new Font("Wingdings 2", 13, FontStyle.Bold, GraphicsUnit.Point);
                    e.BrickStyle.Font = fontex;
                    e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;

                    if (e.Value.ToString().Contains("S"))
                    {
                        e.Text = "R";
                        e.TextValue = "R";
                    }
                    else
                    {
                        e.Text = "£";
                        e.TextValue = "£";
                    }
                }
            }
        }
    }

    public string constroiLinkAbreCategoria( string IndicaItalico, string Categoria, string _CodigoCategoria)
    {
        string cor = IndicaItalico == "S" ? "#339900" : "#000000";
        string funcao = string.IsNullOrEmpty(_CodigoCategoria) ? "void(0)" : "abreMatrizCategoria(" + _CodigoCategoria + ");";

        string retorno = string.Format(@"<span style = 'color:{0}'>", cor);
        retorno += string.Format(@"<a href='#' style = 'color:{0}' onclick='{0}'>{1}</a>", funcao, Categoria);
        retorno += "</span>";
        return retorno;
       
    }
}
