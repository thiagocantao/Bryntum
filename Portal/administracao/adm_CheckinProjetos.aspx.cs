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

public partial class administracao_adm_CheckinProjetos : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;

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
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this);
        
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaGrid();
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        ((GridViewDataTextColumn)gvDados.Columns["DataCheckoutCronograma"]).Settings.ShowFilterRowMenu = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataTextColumn)gvDados.Columns["DataCheckoutCronograma"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        this.TH(this.TS("adm_CheckinProjetos"));
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void populaGrid()
    {
        DataSet ds = cDados.getCronogramasComCheckoutComPermissao(CodigoEntidade,idUsuarioLogado, "");
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 185;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        int codProjeto = (int)gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjeto");
        string CodigoCronogramaProjeto = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoCronogramaProjeto") + "";
        // ACG: 04/10/2015 - O segundo parametro é para desbloquear cronogramas de replanejamento
        cDados.atualizaCronogramaCheckin(codProjeto, CodigoCronogramaProjeto);
        populaGrid();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "DesblCrono");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "DesblCrono", lblTituloTela.Text, this);
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
        if (e.Column.Name == "DataCheckoutCronograma")
        {
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        }
    }
    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        bool podeDesbloquearCronograma = ( (gvDados.GetRowValues(e.VisibleIndex, "Permissao") != null) && gvDados.GetRowValues(e.VisibleIndex, "Permissao").ToString() == "S") ? true : false;

        if (e.ButtonID == "btnCheckin")
        {
            if (podeDesbloquearCronograma)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/btnDesbloquearDes.png";
            }
        }
    }
}
