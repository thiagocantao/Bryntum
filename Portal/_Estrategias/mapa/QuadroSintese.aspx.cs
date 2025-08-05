using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Estrategias_mapa_Macrometa : System.Web.UI.Page
{
    int codigoFator = 0;
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    private string iniciaisObjeto = "PP";
    public string estiloFooter = "dxtlFooter";
    
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

        if (Request.QueryString["INI"] != null && Request.QueryString["INI"].ToString() != "")
            iniciaisObjeto = Request.QueryString["INI"].ToString();

        defineAlturaTela(resolucaoCliente);

        if (Request.QueryString["Cod"] != null && Request.QueryString["Cod"].ToString() != "")
            codigoFator = int.Parse(Request.QueryString["Cod"].ToString());

        carregaMapaEstrategicoArvore();

        string cssPostfix = "", cssPath = "";

        cDados.aplicaEstiloVisual(Page);
        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter += "_" + cssPostfix;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        tlMapaEstrategico.Settings.ScrollableHeight = (altura - 230);
    }

    private void carregaMapaEstrategicoArvore()
    {
        //----------------------------------------------------[Em Arvore]
        DataSet ds = cDados.getQuadroSinteseFatorChave(codigoFator, codigoUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds))
        {
            tlMapaEstrategico.DataSource = ds.Tables[0];
            tlMapaEstrategico.DataBind();
            tlMapaEstrategico.ExpandToLevel(2);
        }
    }

    public string getDescricao()
    {
        string tipoObjeto = Eval("TipoObjeto").ToString();
        string descricao = Eval("Descricao").ToString();
        string codigoLink = Eval("CodigoLink").ToString();
        string cor = Eval("Cor").ToString().Trim();
        bool permissao = (bool)Eval("Permissao");
        string retornoDescricao = "";

        if (tipoObjeto == "TEM")
        {
            retornoDescricao = string.Format(@"<table><tr><td><img src='../../imagens/mapaEstrategico/TemaCombo.png' /></td><td>{0}</td></table>", descricao);
        }
        else if (tipoObjeto == "OBJ")
        {
            retornoDescricao = string.Format(@"<table><tr><td><img src='../../imagens/mapaEstrategico/Objetivo.png' /></td><td>{0}</td></table>", descricao);
        }
        else if (tipoObjeto == "ATR")
        {
            retornoDescricao = string.Format(@"<table><tr><td><img src='../../imagens/mapaEstrategico/Acao.png' /></td><td>{0}</td></table>", descricao);
        }
        else if (tipoObjeto == "IND")
        {
            if(permissao)
            {
                retornoDescricao = string.Format(@"<table><tr><td><img src='../../imagens/mapaEstrategico/indicador.png' /></td><td style=""width:30px"" align=""center""><img src=""../../imagens/{3}.gif""/></td/><td><a href='./Micrometa.aspx?Cod={1}&INI=OB&CF={2}' target='_top'>{0}</a></td></table>", descricao
                    , codigoLink == "" ? "-1" : codigoLink
                    , codigoFator
                    , cor);
            }
            else
                retornoDescricao = string.Format(@"<table><tr><td><img src='../../imagens/mapaEstrategico/indicador.png' /></td><td>{0}</td></table>", descricao);
        }
        else if (tipoObjeto == "INI")
        {
            if(permissao)
            {
                string linkProjeto = Eval("IndicaProjetoCarteiraPrioritaria").ToString() == "S" ? "indexResumoProjeto" : "cni_ResumoProjeto";

                retornoDescricao = string.Format(@"<table><tr><td><img src='../../imagens/mapaEstrategico/projeto.png' /></td><td><a href='../../_Projetos/DadosProjeto/" + linkProjeto + ".aspx?IDProjeto={1}&NomeProjeto={0}&NivelNavegacao=2' target='_top'>{0}</a></td></table>", descricao
                    , codigoLink == "" ? "-1" : codigoLink);
            }
            else
                retornoDescricao = string.Format(@"<table><tr><td><img src='../../imagens/mapaEstrategico/projeto.png' /></td><td>{0}</td></table>", descricao);
        }
        else
        {
            return descricao;
        }

        return retornoDescricao;
    }
}