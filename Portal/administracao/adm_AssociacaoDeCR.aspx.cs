using DevExpress.Web;
using System;
using System.Data;
using System.Data.SqlClient;

public partial class administracao_adm_AssociacaoDeCR : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
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
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(Page);
        Header.Controls.Add(cDados.getLiteral(@"<script src=""../scripts/adm_AssociacaoDeCR.js""></script>"));

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Associação de CR", "ASSOCR", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }

        carregaGrid();
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"exec [dbo].[p_Sescoop_GetProjetosEntidade] {0}", codigoEntidadeLogada);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "ListaCarteiras")
        {
            e.Cell.Text = e.GetValue("ListaCarteiras").ToString();
        }
    }

    protected void menu_ItemClick1(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AssoCR");
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "CadRecCorp", "Associação", this);
    }

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
        else
        {
            //e.TextValue = @"='<p> ""&A2&"".</p><b> ""&B1&"" </b><ul><li> ""&SUBSTITUTE(B2,CHAR(10),"" </li><li> "")&"" </li></ul><b> ""&C1&"" </b><ul><li> ""&SUBSTITUTE(C2,CHAR(10),"" </li><li> "")&"" < / li >< / ul >' ";
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGrid();
    }

    private DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters GetMsSqlConnectionParameters()
    {
        var builder = new SqlConnectionStringBuilder(cDados.ConnectionString);

        var msSqlConnectionParameters = new DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters();
        msSqlConnectionParameters.AuthorizationType = DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.SqlServer;
        msSqlConnectionParameters.DatabaseName = builder.InitialCatalog;
        msSqlConnectionParameters.Password = builder.Password;
        msSqlConnectionParameters.ServerName = builder.DataSource;
        msSqlConnectionParameters.UserName = builder.UserID;
        return msSqlConnectionParameters;
    }

    protected void ASPxWebDocumentViewer1_Load(object sender, EventArgs e)
    {
        var report = new rel_AssociacaoDeCR();
        report.pCodigoEntidade.Value = codigoEntidadeLogada;
        report.sqlDataSource1.ConnectionParameters = GetMsSqlConnectionParameters();
        ASPxWebDocumentViewer1.OpenReport(report);
    }
}