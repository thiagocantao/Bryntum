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
using System.Web.Hosting;
using System.IO;
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraReports.Web.Localization;

public partial class _Portfolios_Relatorios_relProjetosObjetivos : System.Web.UI.Page
{
    dados cDados;

    public string alturaDivGrid = "";
    public string larguraDivGrid = "";

    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codigoUsuarioLogado = 0;

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

        int codUnidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        defineAlturaTela();
        if (!Page.IsPostBack)
        {
            hfGeral.Set("CodigoUnidade", "-1");
            hfGeral.Set("CodigoStatus", "-1");
            populaddlUnidade();
            populaddlStatus();
        }
        relProjetosObjetivos rel = new relProjetosObjetivos();
        string where = "";
        hfGeral.Set("CodigoUnidade", "-1");
        hfGeral.Set("CodigoStatus", "-1");
        if (ddlUnidade.SelectedItem != null && ddlUnidade.SelectedItem.Text != "Todas")
        {
            where = " AND un.CodigoUnidadeNegocio = " + ddlUnidade.SelectedItem.Value;
            hfGeral.Set("CodigoUnidade", ddlUnidade.SelectedItem.Value);
        }

        if (ddlStatus.SelectedItem != null && ddlStatus.SelectedItem.Text != "Todos")
        {
            where += " AND s.CodigoStatus = " + ddlStatus.SelectedItem.Value;
            hfGeral.Set("CodigoStatus", ddlStatus.SelectedItem.Value);
        }


        rel.DataSource = cDados.getRelProjetosPropostas(codigoEntidadeUsuarioResponsavel, codigoUsuarioLogado, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), where).Tables[0];

        ReportViewer1.Report = rel;
        ASPxReportsResLocalizer.Activate();
    }


    //nao esquecer de colocar essas funcoes no cdados quando estiver pronto
    private void populaddlUnidade()
    {
        DataSet ds = cDados.getUnidadePropostaProjeto(codigoEntidadeUsuarioResponsavel, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), codigoUsuarioLogado);
        ddlUnidade.ValueField = "CodigoUnidadeNegocio";
        ddlUnidade.TextField = "SiglaUnidadeNegocio";
        ddlUnidade.DataSource = ds.Tables[0];
        ddlUnidade.DataBind();
        ddlUnidade.Items.Insert(0, new ListEditItem(Resources.traducao.todos, "-1"));
        ddlUnidade.SelectedIndex = 0;
      
    }

    private void populaddlStatus()
    {
        ddlStatus.ValueField = "CodigoStatus";
        ddlStatus.TextField = "DescricaoStatus";
        ddlStatus.DataSource = cDados.getStatusRelPropostaProjetos();
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, new ListEditItem(Resources.traducao.todos, "-1"));
        ddlStatus.SelectedIndex = 0;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaDivGrid = (alturaPrincipal - 215) + "px";
        larguraDivGrid = (larguraPrincipal - 195) + "px";

    }

    public class ASPxReportsResLocalizer : DevExpress.XtraReports.Web.Localization.ASPxReportsResLocalizer
    {
        public static void Activate()
        {
            ASPxReportsResLocalizer localizer = new ASPxReportsResLocalizer();
            DefaultActiveLocalizerProvider<ASPxReportsStringId> provider = new DefaultActiveLocalizerProvider<ASPxReportsStringId>(localizer);
            ASPxReportsResLocalizer.SetActiveLocalizerProvider(provider);

        }
        public override string GetLocalizedString(ASPxReportsStringId id)
        {
            switch (id)
            {
                case ASPxReportsStringId.SearchDialog_Cancel: return "Cancelar";
                case ASPxReportsStringId.SearchDialog_FindNext: return "Próximo";
                case ASPxReportsStringId.SearchDialog_Header: return "Buscar";
                case ASPxReportsStringId.SearchDialog_Up: return "Para Cima";
                case ASPxReportsStringId.SearchDialog_Down: return "Para Baixo";
                case ASPxReportsStringId.SearchDialog_WholeWord: return "Palavra Inteira";
                case ASPxReportsStringId.SearchDialog_FindWhat: return "Palavra";
                case ASPxReportsStringId.SearchDialog_Case: return "Maiúsculas";
                case ASPxReportsStringId.SearchDialog_Finished: return "A Busca Retornou Sem Resultados";


                case ASPxReportsStringId.ToolBarItemText_FirstPage: return "Primeira Página";
                case ASPxReportsStringId.ToolBarItemText_LastPage: return "Última Página";
                case ASPxReportsStringId.ToolBarItemText_NextPage: return "Próxima Página";
                case ASPxReportsStringId.ToolBarItemText_OfLabel: return "de";
                case ASPxReportsStringId.ToolBarItemText_PreviousPage: return "Página Anterior";
                case ASPxReportsStringId.ToolBarItemText_PrintPage: return "Imprimir a Página Atual";
                case ASPxReportsStringId.ToolBarItemText_PrintReport: return "Imprimir o Relatório";
                case ASPxReportsStringId.ToolBarItemText_Search: return "Mostra a Janela de Busca";
                case ASPxReportsStringId.ToolBarItemText_SaveToWindow: return "Exportar o Relatório e Mostrar em Uma Nova Janela";

                default: return base.GetLocalizedString(id);
            }
        }
    }
        
}
