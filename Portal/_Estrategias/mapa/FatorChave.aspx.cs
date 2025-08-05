using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class _Estrategias_mapa_FatorChave : System.Web.UI.Page
{
    int codigoFator = 0;
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    private string alturaFrames = "";

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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        defineAlturaTela(resolucaoCliente);

        if (Request.QueryString["Cod"] != null && Request.QueryString["Cod"].ToString() != "")
            codigoFator = int.Parse(Request.QueryString["Cod"].ToString());

        if (!IsPostBack && Request.QueryString["tabIndex"] + "" != "")
            tabControl.ActiveTabIndex = int.Parse(Request.QueryString["tabIndex"].ToString());

        adicionaFrameTab(0, "frmMacrometa", "./Meta.aspx?cod=" + codigoFator + "&INI=PP");
        adicionaFrameTab(1, "frmQuadroSintese", "./QuadroSintese.aspx?cod=" + codigoFator + "&INI=PP");

        carregaDadosFatorChave();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "FTCH", "EST", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }

    private void carregaDadosFatorChave()
    {
        string comandoSQL = string.Format(@"SELECT TituloObjetoEstrategia, CorFundoObjetoEstrategia 
                                              FROM {0}.{1}.ObjetoEstrategia 
                                             WHERE CodigoObjetoEstrategia = {2}"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , codigoFator);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblTituloTela.Text = "FATOR CHAVE DE COMPETITIVIDADE: " + ds.Tables[0].Rows[0]["TituloObjetoEstrategia"].ToString().ToUpper();

            string corFonte = "#000000";

            if (ds.Tables[0].Rows[0]["CorFundoObjetoEstrategia"].ToString() != "")
                corFonte = ds.Tables[0].Rows[0]["CorFundoObjetoEstrategia"].ToString();

            corFonte = corFonte == "#FFFFFF" ? "" : corFonte;

            lblTituloTela.ForeColor = Color.FromName(corFonte);
        }
    }
    
    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 166);

        if (Request.QueryString["Alt"] != null && Request.QueryString["Alt"].ToString() != "")
            alturaFrames = Request.QueryString["Alt"].ToString();
        else
            alturaFrames = (alturaPrincipal).ToString();
    }

    private void adicionaFrameTab(int tabIndex, string nomeFrame, string url)
    {
        Literal controle;

        tabControl.TabPages[tabIndex].Controls.Clear();

        string frm = string.Format(@"<iframe id=""{0}"" name=""{0}"" frameborder=""0"" height=""{1}px"" 
                                        src=""{2}"" 
                                            width=""100%""></iframe>", nomeFrame
                                                                      , alturaFrames
                                                                      , url);

        controle = cDados.getLiteral(frm);

        tabControl.TabPages[tabIndex].Controls.Add(controle);         
    }
}
