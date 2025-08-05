/*
 * MUDANÇAS:
 * 
 * 24/03/2011 :: Alejandro : Adaptação para permissões. [OB_Ass_IN], [OB_Ass_PR].
 * 29/03/2011 :: Alejandro : Adaptação para permissões. [IN_CnsDtl], [PR_Acs], [OB_CnsDtl].
 */
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

public partial class _Estrategias_detalhesObjetivoEstrategico : System.Web.UI.Page
{
    dados cDados;
    DataTable dt;

    public int codigoUnidadeSelecionada = 0;
    public int codigoUnidadeLogada = 0;
    private int idUsuarioLogado = 0;
    private int codigoTema = 0;
    private int alturaPrincipal;
    int codigoMapa = 0;
    
    public string alturaTabela;
    public string alturaLista;
    private string resolucaoCliente;
    
    public bool permissaoMapa = false;
    public bool podeCnsDtl = false;

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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CT"] != null && Request.QueryString["CT"].ToString() != "")
            codigoTema = int.Parse(Request.QueryString["CT"].ToString());
        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMapa = int.Parse(Request.QueryString["CM"].ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        permissaoMapa = true; // cDados.verificaPermissaoOE(codigoObjetivoEstrategico, codigoUnidadeSelecionada);
        getPermissoesTela();

        if (!podeCnsDtl)
            cDados.RedirecionaParaTelaSemAcesso(this);

        //setea la tela
        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();

        carregaGridObjetivos();

        if (!IsPostBack)
        {
            carregaCampos();
        }
    }

    #region GRIDVIEW

    private void carregaGridObjetivos()
    {
        if (codigoUnidadeLogada == codigoUnidadeSelecionada)
        {

            DataSet dsOE = cDados.getTreeMapaEstrategico(codigoMapa.ToString(), codigoUnidadeSelecionada.ToString(), idUsuarioLogado, "AND CodigoPai = " + codigoTema);

            if (cDados.DataSetOk(dsOE))
            {
                gridOE.DataSource = dsOE;
                gridOE.DataBind();
            }
        }
    }

    #endregion

    #region VARIOS

    private void carregaCampos()
    {
        dt = cDados.getTemaEstrategico(codigoTema, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtTema.Text = dt.Rows[0]["Tema"].ToString();
        }
        else
        {
            txtPerspectiva.Text = "";
            txtMapa.Text = "";
            txtTema.Text = "";
        }
    }

    private void defineAlturaTela()
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = alturaPrincipal - 255;
        gridOE.Settings.VerticalScrollableHeight = altura;
    }

    private void getPermissoesTela()
    {
        ////Procurar permissão para visualizar Ações Sugeridas.
        //DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoUnidadeLogada, codigoTema, idObjetoPai, "OB", "OB_CnsDtl");
        //if (cDados.DataSetOk(ds))
        //{
        //    podeCnsDtl = int.Parse(ds.Tables[0].Rows[0]["OB_CnsDtl"].ToString()) > 0;
        //}
        podeCnsDtl = true;
    }

    #endregion

    public string getDescricaoOE()
    {
        return string.Format(@"<a href='../objetivoestrategico/indexResumoObjetivo.aspx?COE={0}&{2}' target='_top'>{1}</a>", Eval("Codigo").ToString(), Eval("Descricao").ToString(), Request.QueryString.ToString());
    }
}