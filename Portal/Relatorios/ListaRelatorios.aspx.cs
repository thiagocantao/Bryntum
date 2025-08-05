using DevExpress.Web;
using System;
using System.Collections.Specialized;

public partial class Relatorios_ListaRelatorios : System.Web.UI.Page
{
    private dados cDados;

    private string ConnectionString
    {
        get
        {
            return cDados.classeDados.getStringConexao();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
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
        //cDados.aplicaEstiloVisual(this);
        DefineStringsConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvRelatorios_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["IDRelatorio"] = Guid.NewGuid();
    }

    private void DefineStringsConexao()
    {
        dataSource.ConnectionString = ConnectionString;
    }
}