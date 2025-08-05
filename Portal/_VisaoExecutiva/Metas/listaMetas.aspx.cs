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

public partial class _VisaoExecutiva_Metas_listaMetas : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade = 0;
    private int idUsuarioLogado;
    public string alturaTela = "";
    public string paginaIncialMetas = "";
    string codigoResponsavel = "-1";

    string azul = "", verde = "", amarelo = "", vermelho = "", branco = "", mostrarFechados = "&Fechados=S";

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
        this.Title = cDados.getNomeSistema();
        if (!IsPostBack)
        {
            if (Request.QueryString["Azul"] != null && Request.QueryString["Azul"].ToString() != "")
                ckbAzul.Checked = Request.QueryString["Azul"].ToString() == "S";

            if (Request.QueryString["Verde"] != null && Request.QueryString["Verde"].ToString() != "")
                ckbVerde.Checked = Request.QueryString["Verde"].ToString() == "S";

            if (Request.QueryString["Amarelo"] != null && Request.QueryString["Amarelo"].ToString() != "")
                ckbAmarelo.Checked = Request.QueryString["Amarelo"].ToString() == "S";

            if (Request.QueryString["Vermelho"] != null && Request.QueryString["Vermelho"].ToString() != "")
                ckbVermelho.Checked = Request.QueryString["Vermelho"].ToString() == "S";

            if (Request.QueryString["Branco"] != null && Request.QueryString["Branco"].ToString() != "")
                ckbBranco.Checked = Request.QueryString["Branco"].ToString() == "S";

            if (Request.QueryString["Fechados"] != null && Request.QueryString["Fechados"].ToString() != "")
                mostrarFechados = "&Fechados=" + Request.QueryString["Fechados"].ToString();

            if (Request.QueryString["CR"] != null && Request.QueryString["CR"].ToString() != "")
                codigoResponsavel = Request.QueryString["CR"].ToString();
        }

        azul = ckbAzul.Checked ? "&Azul=S&" : "&Azul=N&";

        verde = ckbVerde.Checked ? "Verde=S&" : "Verde=N&";

        amarelo = ckbAmarelo.Checked ? "Amarelo=S&" : "Amarelo=N&";

        vermelho = ckbVermelho.Checked ? "Vermelho=S&" : "Vermelho=N&";

        branco = ckbBranco.Checked ? "Branco=S" : "Branco=N";

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_AcsPnlMta");
        }

        carregaComboResponsaveis();

        codigoResponsavel = ddlResponsavel.Value.ToString();
                
        cDados.aplicaEstiloVisual(this);

        defineLarguraTela();

        int nivel = 0;

        if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
            nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

        if (!IsPostBack)
        {
            lblTituloTela.Text = "Painel de Metas Estratégicas ";

            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "VISMET", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }

        paginaIncialMetas = "../../_Estrategias/VisaoMetas/visaoMetas_01.aspx?CR=" + codigoResponsavel + azul + verde + amarelo + vermelho + branco + mostrarFechados + "&NivelNavegacao=" + nivel;
        
    }

    private void carregaComboResponsaveis()
    {
        DataSet dsResponsaveis = cDados.getResponsaveisMetasUsuario(codigoEntidade, idUsuarioLogado, "");

        if (cDados.DataSetOk(dsResponsaveis))
        {
            ddlResponsavel.DataSource = dsResponsaveis;
            ddlResponsavel.TextField = "NomeUsuario";
            ddlResponsavel.ValueField = "CodigoUsuario";

            DataRow dr = dsResponsaveis.Tables[0].NewRow();
            dr["NomeUsuario"] = "Todos";
            dr["CodigoUsuario"] = "-1";
            dr["EMail"] = "Todos";
            dsResponsaveis.Tables[0].Rows.InsertAt(dr, 0);
            ddlResponsavel.DataBind();
        }
        if (!IsPostBack)
        {
            if (ddlResponsavel.Items.FindByValue(int.Parse(codigoResponsavel)) != null)
                ddlResponsavel.Value = int.Parse(codigoResponsavel);
            else
                ddlResponsavel.SelectedIndex = 0;
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 155).ToString() + "px";
    }  
}
