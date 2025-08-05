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

public partial class _Estrategias_wizard_menuMapa_IndexMapa : System.Web.UI.Page
{
    public string telaInicial = "PerspectivasMapa.aspx?Tit=Perspectivas";
    public string alturaTabela;
    public string nomeTela = Resources.traducao.IndexMapa_perspectivas_de_mapas_estrat_gicos;

    private int idUsuarioLogado;
    private int idEntidadeLogada;
    private int codigoMapaDaEmpresa = -1;
    
    dados cDados;

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

        idEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        alturaTabela = getAlturaTela() + "px";
        getMapaDaEmpresa(idEntidadeLogada);
        telaInicial += "&CM=" + codigoMapaDaEmpresa;

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            nomeTela = cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "IDXMPA", "EST", -1, Resources.traducao.adicionar_aos_favoritos);
        }

        nomeTela = Server.UrlEncode(nomeTela);

        if(!IsPostBack)
            lblTituloTela.Text += " - Perspectivas";

        cDados.aplicaEstiloVisual(this);
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 131).ToString();
    }

    private void getMapaDaEmpresa(int codigoEntidadLogada)
    {
        DataTable dt = cDados.getMapaDaEntidade(codigoEntidadLogada).Tables[0];
        if (cDados.DataTableOk(dt))
        {
            if(dt.Rows[0]["CodigoMapaDaEntidade"].ToString() != "")
                codigoMapaDaEmpresa = int.Parse(dt.Rows[0]["CodigoMapaDaEntidade"].ToString());

        }
    }
}
