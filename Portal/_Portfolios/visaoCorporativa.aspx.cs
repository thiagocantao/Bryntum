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

public partial class _Portfolios_visaoCorporativa : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    int codigoEntidadeUsuarioResponsavel;

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

        int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "EN_AcsPnlPtf");
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        verifiacaVisao();
        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"">var statusVC = 1;</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/vcPortfolio.js""></script>"));
        this.TH(this.TS("vcPortfolio"));


        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        defineLarguraTela();
    }

    private void verifiacaVisao()
    {
        if (Request.QueryString["CC"] != null)
        {
            rbTipoVisao.Value = "C";
            lblFiltro.Text = "Categoria:";
            ddlCategoria.ClientVisible = true;
            ddlUnidade.ClientVisible = false;
            ddlGeral.ClientVisible = false;
        }

        carregaComboCategorias();
        carregaComboPortfolios();
        carregaComboUnidades();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 151).ToString() + "px";        
    }

    private void carregaComboPortfolios()
    {
        DataSet dsPortfolios = cDados.getPortfolios(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(dsPortfolios))
        {
            ddlGeral.DataSource = dsPortfolios;

            ddlGeral.TextField = "DescricaoPortfolio";

            ddlGeral.ValueField = "CodigoPortfolio";

            ddlGeral.DataBind();

            if (!IsPostBack && cDados.DataTableOk(dsPortfolios.Tables[0]))
                ddlGeral.SelectedIndex = 0;
        }

        cDados.setInfoSistema("CodigoPortfolio", ddlGeral.Items.Count > 0 ? ddlGeral.Value.ToString() : "-1");
    }

    private void carregaComboCategorias()
    {
        DataSet dsCategorias = cDados.getCategoria(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);

        if (cDados.DataSetOk(dsCategorias))
        {
            ddlCategoria.DataSource = dsCategorias;

            ddlCategoria.TextField = "DescricaoCategoria";

            ddlCategoria.ValueField = "CodigoCategoria";

            ddlCategoria.DataBind();

            if (!IsPostBack && cDados.DataTableOk(dsCategorias.Tables[0]))
                ddlCategoria.SelectedIndex = 0;
        }
        

        if (!IsPostBack && Request.QueryString["CC"] != null)
        {
            cDados.setInfoSistema("CodigoCategoria", Request.QueryString["CC"].ToString());
            ddlCategoria.Value = cDados.getInfoSistema("CodigoCategoria").ToString();
        }

        cDados.setInfoSistema("CodigoCategoria", ddlCategoria.Items.Count > 0 ? ddlCategoria.Value.ToString() : "-1");
    }

    private void carregaComboUnidades()
    {
        DataSet dsUnidades = cDados.getUnidade(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);

        if (cDados.DataSetOk(dsUnidades))
        {
            ddlUnidade.DataSource = dsUnidades;

            ddlUnidade.TextField = "SiglaUnidadeNegocio";

            ddlUnidade.ValueField = "CodigoUnidadeNegocio";

            ddlUnidade.DataBind();

            if (!IsPostBack && cDados.DataTableOk(dsUnidades.Tables[0]))
                ddlUnidade.SelectedIndex = 0;
        }
        
       cDados.setInfoSistema("CodigoUnidade", ddlUnidade.Items.Count > 0 ? ddlUnidade.Value.ToString() : "-1");        
    }

    protected void callBackVC_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        
    }   
}
