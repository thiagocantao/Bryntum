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

public partial class _VisaoNE_VisaoContratos_visaoCorporativa : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    int codigoEntidade;

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
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"">var statusVC = 1;</script>"));
        // =========================== Verifica se a sessão existe FIM ========================

        int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_AcsPnlGesCtt");
            cDados.aplicaEstiloVisual(Page);
        } 
              
        defineLarguraTela();
                
        if (!IsPostBack)
        {
            carregaAnos();
            carregaTiposContratacao();
            carregaFontesContratacao();
            cDados.excluiNiveisAbaixo(0);
            cDados.insereNivel(0, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "PNL_NECTR", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);

            DataSet ds = cDados.getParametrosSistema(codigoEntidade, "mostraFiltroFonteGestaoContratos");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["mostraFiltroFonteGestaoContratos"].ToString() == "S")
            {
                lblFonte.ClientVisible = true;
                ddlFonte.ClientVisible = true;
            }
            else
            {
                lblFonte.ClientVisible = false;
                ddlFonte.ClientVisible = false;
            }
        }
        this.Title = cDados.getNomeSistema();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 200).ToString() + "px";
    }

    private void carregaAnos()
    {
        DataSet ds = cDados.getAnosContratos(codigoEntidade, "");
        
        ddlAno.DataSource = ds;
        ddlAno.TextField = "AnoContrato";
        ddlAno.ValueField = "AnoContrato";
        ddlAno.DataBind();

        //ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

        //ddlAno.Items.Insert(0, lei);

        if (cDados.DataSetOk(ds) && ddlAno.Items.Count > 0 && !IsPostBack)
        {

            if (cDados.getInfoSistema("AnoPainelContrato") != null)
                ddlAno.Value = cDados.getInfoSistema("AnoPainelContrato").ToString();
            else
            {
                if (ddlAno.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
                    ddlAno.Value = DateTime.Now.Year.ToString();
                else
                    ddlAno.SelectedIndex = 0;
            }
        }


        cDados.setInfoSistema("AnoPainelContrato", ddlAno.SelectedIndex == -1 ? "-1" : ddlAno.Value.ToString());

    }

    private void carregaTiposContratacao()
    {
        DataSet ds = cDados.getTipoServico(codigoEntidade, "");

        ddlTipoContratacao.DataSource = ds;
        ddlTipoContratacao.TextField = "DescricaoTipoServico";
        ddlTipoContratacao.ValueField = "CodigoTipoServico";
        ddlTipoContratacao.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");
        ddlTipoContratacao.Items.Insert(0, lei);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && !IsPostBack)
        {

            if (cDados.getInfoSistema("TipoContratacao") != null)
                ddlTipoContratacao.Value = cDados.getInfoSistema("TipoContratacao").ToString();
            else
            {
                ddlTipoContratacao.SelectedIndex = 0;
            }
        }


        cDados.setInfoSistema("TipoContratacao", ddlTipoContratacao.SelectedIndex == -1 ? "-1" : ddlTipoContratacao.Value.ToString());

    }

    private void carregaFontesContratacao()
    {
        DataSet ds = cDados.getFontesRecursosFinanceiros(codigoEntidade, "");

        ddlFonte.DataSource = ds;
        ddlFonte.TextField = "NomeFonte";
        ddlFonte.ValueField = "CodigoFonteRecursosFinanceiros";
        ddlFonte.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");
        ddlFonte.Items.Insert(0, lei);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && !IsPostBack)
        {

            if (cDados.getInfoSistema("CodigoFonte") != null)
                ddlFonte.Value = cDados.getInfoSistema("CodigoFonte").ToString();
            else
            {
                ddlFonte.SelectedIndex = 0;
            }
        }


        cDados.setInfoSistema("CodigoFonte", ddlFonte.SelectedIndex == -1 ? "-1" : ddlFonte.Value.ToString());

    }

    protected void callbackVC_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        carregaAnos();
        carregaTiposContratacao();
        carregaFontesContratacao();
    }
}
