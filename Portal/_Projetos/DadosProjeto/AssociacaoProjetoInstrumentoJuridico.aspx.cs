using DevExpress.Web;
using DevExpress.Web.Data;
using System;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_AssociacaoProjetoInstrumentoJuridico : System.Web.UI.Page
{
    private int codigoProjeto;
    private int codigoUsuarioLogado;

    dados cDados;
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";

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
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
    }

    protected void gvDados_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cpCodigoProjeto"] = codigoProjeto;
    }

    protected void gvDados_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        var valorMaximo = (decimal)gvDados.GetRowValues(e.VisibleIndex, "ValorMaximo");
        var valorProjeto = (decimal)e.NewValues["ValorProjeto"];
        if (valorProjeto > valorMaximo)
            e.RowError = string.Format("O 'Valor Projeto' não pode ser superior à {0:N2}", valorMaximo);
    }

    protected void gvDados_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName.Equals("ValorProjeto"))
        {
            var editor = (ASPxSpinEdit)e.Editor;
            editor.MaxValue = decimal.MaxValue;
            editor.MinValue = 0.01M;
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;
        int altura = (alturaPrincipal - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 180;
        gvDados.Width = new Unit("100%");
    }
}