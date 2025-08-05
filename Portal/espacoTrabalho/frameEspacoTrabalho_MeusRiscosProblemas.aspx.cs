//Revisado
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

public partial class espacoTrabalho_frameEspacoTrabalho_MeusRisgosProblemas : System.Web.UI.Page
{

    dados cDados;

    private string idUsuarioLogado;
    private string CodigoEntidade;
    public string alturaTabela;
    public string telaInicial = "";
    string tipoTela = "R", corStatus = "";

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

        if (Request.QueryString["TipoTela"] != null && Request.QueryString["TipoTela"].ToString() != "")
            tipoTela = Request.QueryString["TipoTela"].ToString();

        if (Request.QueryString["Cor"] != null && Request.QueryString["Cor"].ToString() != "")
            corStatus = Request.QueryString["Cor"].ToString();

        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();  //usuario logado.
        CodigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();   //entidad logada.
        this.Title = cDados.getNomeSistema();
    }
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            defineTelaInicial();
        }

        alturaTabela = getAlturaTela() + "px";

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "CAD_" + tipoTela, "ENT", -1, Resources.traducao.frameEspacoTrabalho_MeusRiscosProblemas_adicionar_aos_favoritos);
        }
    }

    #region VARIOS

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 95).ToString();
    }

    private void defineTelaInicial()
    {
        string paramCor = "";

        if (corStatus != "")
        {
            paramCor = "&Cor=" + corStatus;
        }

        if (tipoTela == "R")
        {
            telaInicial = "./" + "meusRiscos.aspx?TT=R&IDUsuario=" + idUsuarioLogado + "&tp=Riscos";
        }
        else
        {
            telaInicial = "./" + "meusProblemas.aspx?TT=Q&IDUsuario=" + idUsuarioLogado + "&tp=" + lblTituloTela.Text;
        }

        telaInicial += paramCor;
    }

    #endregion
}
