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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Drawing;
using DevExpress.XtraPivotGrid;
using DevExpress.Web;

public partial class _Portfolios_frameSelecaoBalanceamento_OlapRecursos : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;
    public string larguraTela = "", alturaTela = "";
    public string larguraGrafico = "", alturaGrafico = "";

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
        // =========================== Verifica se a sessão existe INICIO ========================
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

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            if (cDados.getInfoSistema("Cenario") != null)
                ddlCenario.Value = cDados.getInfoSistema("Cenario").ToString();
        }

        carregaComboRecursos();
        carregaComboCategorias();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);

            cDados.setInfoSistema("Cenario", ddlCenario.Value.ToString());

            if (cDados.getInfoSistema("Categoria") != null)
                ddlCategoria.Value = cDados.getInfoSistema("Categoria");

            cDados.setInfoSistema("Categoria", ddlCategoria.Value);

            if (cDados.getInfoSistema("CodigoRH") != null)
                ddlRecurso.Value = cDados.getInfoSistema("CodigoRH").ToString();

            cDados.setInfoSistema("CodigoRH", ddlRecurso.Value.ToString());
        }

        imgModoVisao.Style.Add("cursor", "pointer");
        lblGantt.Style.Add("cursor", "pointer");
        
        defineLarguraTela();

    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = (largura - 30).ToString() + "px";

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 140).ToString() + "px";
        
    }    

    private void carregaComboRecursos()
    {
        string where = " AND IndicaCenario" + ddlCenario.Value + " = 'S'";

        DataSet dsRecursos = cDados.getRecursosSelecaoBalanceamento(codigoEntidadeUsuarioResponsavel, where, "");

        ddlRecurso.DataSource = dsRecursos;

        ddlRecurso.TextField = "DescricaoGrupo";

        ddlRecurso.ValueField = "CodigoGrupoRecurso";

        ddlRecurso.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

        ddlRecurso.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlRecurso.SelectedIndex = 0;
    }

    protected void callBackVC_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        cDados.setInfoSistema("Cenario", ddlCenario.Value.ToString());
        cDados.setInfoSistema("CodigoRH", ddlRecurso.Value.ToString());
        cDados.setInfoSistema("Categoria", ddlCategoria.Value);
    }

    private void carregaComboCategorias()
    {
        DataSet ds = cDados.getCategoria(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);

        ddlCategoria.DataSource = ds;

        ddlCategoria.TextField = "SiglaCategoria";

        ddlCategoria.ValueField = "CodigoCategoria";
        
        DataRow dr = ds.Tables[0].NewRow();

        dr["DescricaoCategoria"] = Resources.traducao.todas;
        dr["CodigoCategoria"] = -1;
        dr["SiglaCategoria"] = Resources.traducao.todas;
        ds.Tables[0].Rows.InsertAt(dr, 0);
        ddlCategoria.DataBind();

        if (!IsPostBack)
            ddlCategoria.SelectedIndex = 0;
    }

    protected void ddlRecurso_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter.ToString() == "A")
        {
            ddlRecurso.SelectedIndex = 0;
        }
    }

}
