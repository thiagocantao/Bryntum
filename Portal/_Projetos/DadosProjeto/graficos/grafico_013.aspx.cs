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
using System.Drawing;

public partial class _Projetos_DadosProjeto_graficos_grafico_013 : System.Web.UI.Page
{
    dados cDados;
    int codigoProjeto = 0;
    int codigoUnidadeNegocio = 0;
    public int alturaTela;
    private string modoTela = "";

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

        codigoUnidadeNegocio = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

        if (Request.QueryString["ModoTela"] != null && Request.QueryString["ModoTela"].ToString() != "")
            modoTela = Request.QueryString["ModoTela"].ToString();

        DataSet dsGrafico = cDados.getDesempenhoFisicoProjeto(codigoProjeto, "");
                
        carregaGridTarefas();

        cDados.aplicaEstiloVisual(this);

        cDados.defineAlturaFrames(this, alturaTela + 75);

        gridProjetos.Settings.ShowFilterRow = true;
        gridProjetos.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gridProjetos.SettingsLoadingPanel.Mode = DevExpress.Web.GridViewLoadingPanelMode.Disabled;

        ((GridViewDataTextColumn)gridProjetos.Columns["TipoTarefa"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataTextColumn)gridProjetos.Columns["Realizado"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataTextColumn)gridProjetos.Columns["Tendência"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataTextColumn)gridProjetos.Columns["Previsto"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataTextColumn)gridProjetos.Columns["Marco"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataTextColumn)gridProjetos.Columns["col_Status"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
    }

    private void carregaGridTarefas()
    {
        DataSet ds = cDados.getMarcosTarefasClienteDescricaoTipoTarefa(codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gridProjetos.DataSource = ds;

            gridProjetos.DataBind();
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstMarPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "LstMarPrj", "Marcos do Projeto", this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        e.XlsExportNativeFormat = DevExpress.Utils.DefaultBoolean.False;
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
        if (e.Column.Name == "col_Status" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;

            if (e.Value.ToString().Equals("VermelhoOk"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Equals("VerdeOk"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Equals("Verde"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Equals("Laranja"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Orange;
            }
            else if (e.Value.ToString().Equals("Vermelho"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Equals("Amarelo"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().Equals("Branco"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
        }
    }
}
