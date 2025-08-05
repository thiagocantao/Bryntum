using DevExpress.Web;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;

public partial class Relatorios_OutrosRelatorios_RelatorioSenarDocs : System.Web.UI.Page
{
    dados cDados;

    int codigoEntidade;
    int codigoUsuarioLogado;
    DsListaProcessos ds;
    int codigoLista;
    int codigoListaUsuario;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));

        Session["ce"] = codigoEntidade;
        Session["cu"] = codigoUsuarioLogado;

        sdsNiveis.ConnectionString = cDados.classeDados.getStringConexao();
        sdsProcessos.ConnectionString = cDados.classeDados.getStringConexao();
        sdsRegistrosIntegracao.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

       

        if (!(IsPostBack || IsPostBack))
        {
            cDados.aplicaEstiloVisual(this);
            var dataAtual = DateTime.Now;
            deTerminoPeriodo.Date = dataAtual;
            deInicioPeriodo.Date = dataAtual.AddDays(-7);
        }
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        ds = new DsListaProcessos();
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.exportaTreeList(exporter, parameter);
        }
    }

    protected void menu_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        ASPxMenu menu = (sender as ASPxMenu);

        #region EXPORTAÇÃO

        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");

        bool exportaOLAPTodosFormatos = false;

        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
        {
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        }

        DevExpress.Web.MenuItem btnExportar = menu.Items.FindByName("btnExportar");

        btnExportar.ClientVisible = true;

        if (!exportaOLAPTodosFormatos)
        {
            btnExportar.Items.Clear();
            btnExportar.Image.Url = "~/imagens/botoes/btnExcel.png";
            btnExportar.ToolTip = Resources.traducao.VisualizacaoGrid_exportar_para_xlsx;
        }
        else
        {
            btnExportar.ToolTip = Resources.traducao.exportar;
            btnExportar.Items.FindByName("btnXLS").ToolTip = Resources.traducao.exportar_para_xls;
        }

        #endregion

        #region JS

        menu.ClientSideEvents.ItemClick =
            String.Format(@"function(s, e){{ 
	e.processOnServer = false;
	if(e.item.name != 'btnLayout'){{e.processOnServer = true;}}	
}}");

        #endregion
    }

    protected void sdsNiveis_Selecting(object sender, System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs e)
    {
        var p = e.Command.Parameters;
    }

    protected void sdsRegistrosIntegracao_Selecting(object sender, System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs e)
    {
        var p = e.Command.Parameters;
    }

    protected void sdsRegistrosIntegracao_Selected(object sender, System.Web.UI.WebControls.SqlDataSourceStatusEventArgs e)
    {
        var p = e.Command.Parameters;
    }

    protected void sdsNiveis_Selected(object sender, System.Web.UI.WebControls.SqlDataSourceStatusEventArgs e)
    {
        var p = e.Command.Parameters;
    }

    protected void treeList_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
    {
        treeList.DataBind();
    }

    protected void cmbNiveisDetalhes_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        DataTable dt = ((DataView)sdsNiveis.Select(DataSourceSelectArguments.Empty)).Table;
        cmbNiveisDetalhes.DataSource = dt;
        cmbNiveisDetalhes.DataBindItems();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            if (!dr.IsNull("IndicaNivelDetalhePadrao") && dr.Field<string>("IndicaNivelDetalhePadrao").Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                cmbNiveisDetalhes.SelectedIndex = i;
                break;
            }
        }
    }

    protected void treeList_HtmlRowPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventArgs e)
    {
        if (e.RowKind == DevExpress.Web.ASPxTreeList.TreeListRowKind.Data)
        {
            var node = treeList.FindNodeByKeyValue(e.NodeKey);
            var mensagem = node.GetValue("MensagemFalhaProcessamento") as string;
            if (!string.IsNullOrWhiteSpace(mensagem))
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}