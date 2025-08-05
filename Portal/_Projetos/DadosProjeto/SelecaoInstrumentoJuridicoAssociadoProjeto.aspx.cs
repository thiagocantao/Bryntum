using System;
using System.Globalization;
using System.Linq;

public partial class _Projetos_DadosProjeto_SelecaoInstrumentoJuridicoAssociadoProjeto : System.Web.UI.Page
{
    private int codigoProjeto;
    private int codigoUsuarioLogado;

    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = string.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }
        codigoProjeto = int.Parse(Request.QueryString["cp"]);
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        Session["cul"] = codigoUsuarioLogado;
        cDados.aplicaEstiloVisual(this);
        dataSource.ConnectionString = cDados.ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        var codigos = e.Parameter.Split(',');
        foreach (var cod in codigos)
        {
            int codigo_IJ = -1;
            if (int.TryParse(cod, out codigo_IJ) == true)
            {
                var valorDisponivel = string.Format(CultureInfo.InvariantCulture, "{0}", gvDados.GetRowValuesByKeyValue(codigo_IJ, "ValorDisponivel"));
                dataSource.InsertParameters["ValorDisponivel"].DefaultValue = valorDisponivel;
                dataSource.InsertParameters["CodigoIJ"].DefaultValue = cod;
                var result = dataSource.Insert();
            }
            
        }
    }
}