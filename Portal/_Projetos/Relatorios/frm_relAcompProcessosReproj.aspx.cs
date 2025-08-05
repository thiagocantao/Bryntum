using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.XtraReports.UI;

public partial class _Projetos_Relatorios_frm_relAcompProcessosReproj : System.Web.UI.Page
{

    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    int codUnidade = -1;
    int ano = -1;
    public bool podeIncluir = false;
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "RelAcProcReprMA");
        }

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (int.TryParse(Request.QueryString["UN"], out codUnidade) == true)
            codUnidade = int.Parse(Request.QueryString["UN"]);

        if (int.TryParse(Request.QueryString["ANO"], out ano) == true)
            ano = int.Parse(Request.QueryString["ANO"]);


        rel_AcompProcessosReprojPrincipal relatorio = new rel_AcompProcessosReprojPrincipal(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codUnidade, ano);


        relatorio.CreateDocument();

        DevExpress.XtraPrinting.Page[] paginarelatorio = relatorio.Pages.ToArray();

        for (int i = 0; i < paginarelatorio.Length && (codUnidade != -1); i++)
        {
            relatorio.Pages.Add(paginarelatorio[i]);
        }

        relSubreportProjetos relSubreportProjetos1 = new relSubreportProjetos(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codUnidade, ano);

        relSubreportProjetos1.CreateDocument();

        rel_AcompProcessosReproj rel_AcompProcessosReproj1 = new rel_AcompProcessosReproj(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codUnidade, ano);

        rel_AcompProcessosReproj1.CreateDocument();

        //relAcompProcReproj_relSubreportResultadoMeta relSubreportResultadoMeta1 = new relAcompProcReproj_relSubreportResultadoMeta(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codUnidade, ano);

        //relSubreportResultadoMeta1.CreateDocument();

        rel_SubResultadosMesAMes rel_SubResultadosMesAMes1 = new rel_SubResultadosMesAMes(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codUnidade, ano);

        rel_SubResultadosMesAMes1.CreateDocument();


        DevExpress.XtraPrinting.Page[] paginaSubreportProjetos1 = relSubreportProjetos1.Pages.ToArray();
        DevExpress.XtraPrinting.Page[] paginarel_AcompProcessosReproj1 = rel_AcompProcessosReproj1.Pages.ToArray();
        //DevExpress.XtraPrinting.Page[] paginarelSubreportResultadoMeta1 = relSubreportResultadoMeta1.Pages.ToArray();
        DevExpress.XtraPrinting.Page[] paginarel_SubResultadosMesAMes1 = rel_SubResultadosMesAMes1.Pages.ToArray();



        for (int i = 0; i < paginaSubreportProjetos1.Length && (codUnidade != -1); i++)
        {
            relatorio.Pages.Add(paginaSubreportProjetos1[i]);
        }

        
        for (int i = 0; i < paginarel_SubResultadosMesAMes1.Length && (codUnidade != -1); i++)
        {
            relatorio.Pages.Add(paginarel_SubResultadosMesAMes1[i]);
        }

        for (int i = 0; i < paginarel_AcompProcessosReproj1.Length && (codUnidade != -1); i++)
        {
            relatorio.Pages.Add(paginarel_AcompProcessosReproj1[i]);
        }

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        largura = largura - (int)ReportViewer1.Width.Value;
        largura = largura / 2;
        ReportViewer1.Height = new Unit((alturaPrincipal - 220) + "px");
        ReportViewer1.Report = relatorio;
        ReportViewer1.WritePdfTo(this.Response);
        

    }
}