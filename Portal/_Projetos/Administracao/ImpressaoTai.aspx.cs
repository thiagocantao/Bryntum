using System;
using System.Web.UI;
using DevExpress.XtraReports.UI;

public partial class _Projetos_Administracao_ImpressaoTai : Page
{
    dados cDados;
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        XtraReport report;
        Int32 codigoProjeto = Int32.Parse(Request.QueryString["CP"]);
        String modeloTai = Request.QueryString["ModeloTai"].ToLower();
        String origem =  Request.QueryString["Origem"];
        Int32 codigoFormulario = 0;

        switch (modeloTai)
        {
            case "tai":
                report = new relImpressaoTai(codigoProjeto);
                break;
            case "tai001":
                report = new relImpressaoTai_001(codigoProjeto);
                break;
            case "tai003":
                report = null;
                break;
            case "tai004":
                report = new relImpressaoTai_004(codigoProjeto);
                if (origem == "S")
                    ((relImpressaoTai_004)report).pTitulo.Value = "Plano de Projeto";
                break;
            case "tai005":
                report = null;
                break;
            case "tai007":
                report = new relImpressaoTai_007(codigoProjeto);
                if (origem == "S")
                    ((relImpressaoTai_007)report).pTitulo.Value = "Plano de Projeto";
                break;
			case "tai008":
				report = new relImpressaoTai_008(codigoProjeto);
                if (origem == "S")
					((relImpressaoTai_008)report).pTitulo.Value = "Plano de Projeto";
				break;
            default:
                report = null;
                break;
        }

        string cf = Request.QueryString["CF"];
        if (!string.IsNullOrEmpty(cf) && int.TryParse(cf, out codigoFormulario) && codigoFormulario != -1)
        {
            rel_ImpressaoFormularios relCF = new rel_ImpressaoFormularios(codigoFormulario);
            report.CreateDocument();
            relCF.CreateDocument();
            report.Pages.AddRange(relCF.Pages);

        }
        ReportViewer1.Report = report;
        defineAlturaTela(resolucaoCliente);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 180);
        divRelatorio.Style.Add("height", (alturaPrincipal) + "px");
        divRelatorio.Style.Add("overflow", "auto");
        divToolBox.Style.Add("overflow", "auto");
        divToolBox.Style.Add("height", "40px");



    }
}