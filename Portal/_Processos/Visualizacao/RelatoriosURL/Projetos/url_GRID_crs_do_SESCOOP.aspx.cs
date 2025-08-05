using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_GRID_crs_do_SESCOOP : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;
    int codigoEntidade = 0;

    public string alturaTabela = "";
    public string larguraTabela = "";

    public bool exportaOLAPTodosFormatos = false;

    private List<string> Meses = new List<string>();
    private string tipoConsulta = ""; //Tipo de consulta a ser efetuada 'C'liente ou 'F'ornecedor

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        //cDados.aplicaEstiloVisual(this);
        gvDados.Theme = "MaterialCompact";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            populaComboAnos();

        }
        if (this.IsPostBack || this.IsCallback)
        {
            populaGrid();
            gvDados.ClientVisible = true;
        }
        else
        {
            gvDados.ClientVisible = false;
        }
        
    }

    private void populaComboAnos()
    {
        string strCombo = string.Format(@"SELECT Ano, IndicaSelecionado FROM [dbo].[f_Sescoop_GetAnosCRs] ({0})", codigoEntidade);
        DataSet dscombo = cDados.getDataSet(strCombo);

        comboAnos.TextField = "Ano";
        comboAnos.ValueField = "Ano";
        comboAnos.DataSource = dscombo;
        comboAnos.DataBind();

        if(comboAnos.Items.Count > 0)
        {
            comboAnos.SelectedIndex = 0;
        }
        comboTrimestre.SelectedIndex = 0;
    }

    private void populaGrid()
    {
        string comandoSQL = string.Format(@"
        dbo.p_SESCOOP_ExportaCSVTCU {0}, {1}, {2}, '{3}'       
", codigoEntidade, comboAnos.Value, comboTrimestre.Value, radioReceitaDespesa.Value);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();

        if(radioReceitaDespesa.Value.ToString() == "R")
        {
            ((GridViewDataTextColumn)gvDados.Columns["REALIZADO"]).Caption = "ARRECADADO";
            ((GridViewDataTextColumn)gvDados.Columns["RETIFICADO"]).Visible = false;
            ((GridViewDataTextColumn)gvDados.Columns["SUPLEMENTADO"]).Visible = false;
            ((GridViewDataTextColumn)gvDados.Columns["TRANSPOSTO"]).Visible = false;
        }
        else
        {
            ((GridViewDataTextColumn)gvDados.Columns["REALIZADO"]).Caption = "REALIZADO";
            ((GridViewDataTextColumn)gvDados.Columns["RETIFICADO"]).Visible = true;
            ((GridViewDataTextColumn)gvDados.Columns["SUPLEMENTADO"]).Visible = true;
            ((GridViewDataTextColumn)gvDados.Columns["TRANSPOSTO"]).Visible = true;
        }

    }


    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "CSV" : e.Item.Text;
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenu(menu, "CSV", ASPxGridViewExporter1, "CrSesc");
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportarSemTemplate((sender as ASPxMenu), false, "", false, true, false, "CrSesc", "CRs do SESCOOP", this, gvDados);

        DevExpress.Web.MenuItem btnExportar = menu.Items.FindByName("btnExportar");

        

        if (!exportaOLAPTodosFormatos)
        {
            btnExportar.Items.Clear();
            btnExportar.Image.Url = "~/imagens/menuExportacao/iconoCSV.png";
            btnExportar.ToolTip = "Exportar para CSV";
        }
        btnExportar.ClientVisible = (this.IsPostBack || this.IsCallback);
    }

    #endregion
}