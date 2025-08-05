using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.Web;

public partial class _Projetos_DadosProjeto_popupRelContratos : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    public string larguraTela = "";
    private int codigoContratoReport;
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
        // =========================== Verifica se a sessão existe FIM ========================

        if (!int.TryParse(Request.QueryString["codigoContrato"], out codigoContratoReport))
            codigoContratoReport = -1;

        defineAlturaTela();
        this.Title = cDados.getNomeSistema();


        ReportViewer1.Report = new rel_Contrato(codigoContratoReport);
        ReportViewer1.WritePdfTo(Response);
    }

    private ReportToolbarItem GetBtnByName(string name)
    {
        foreach (ReportToolbarItem item in ReportToolbar1.Items)
        {
            if (item.Name == name)
                return item;
        }
        return null;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        // Calcula a largura da tela
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTela = (alturaPrincipal - 40).ToString() + "px";
        larguraTela = (larguraPrincipal - 90).ToString() + "px";
    }



    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {

    }
}