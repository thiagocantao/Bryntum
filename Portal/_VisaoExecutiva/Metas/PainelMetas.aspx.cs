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
    string codigoUnidade = "-1";
    string codigoMapa = "-1";
    string codigoIndicador = "-1";
    bool setaMapaDefaul = true;

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

        cDados.setInfoSistema("CodigoMapa", "0");

        if (!IsPostBack)
        {
            if (Request.QueryString["Azul"] != null && Request.QueryString["Azul"].ToString() != "")
            {
                ckbAzul.Checked = Request.QueryString["Azul"].ToString() == "S";
                setaMapaDefaul = false;
            }
            if (Request.QueryString["Verde"] != null && Request.QueryString["Verde"].ToString() != "")
            {
                ckbVerde.Checked = Request.QueryString["Verde"].ToString() == "S";
                setaMapaDefaul = false;
            }
            if (Request.QueryString["Amarelo"] != null && Request.QueryString["Amarelo"].ToString() != "")
            {
                ckbAmarelo.Checked = Request.QueryString["Amarelo"].ToString() == "S";
                setaMapaDefaul = false;
            }
            if (Request.QueryString["Vermelho"] != null && Request.QueryString["Vermelho"].ToString() != "")
            {
                ckbVermelho.Checked = Request.QueryString["Vermelho"].ToString() == "S";
                setaMapaDefaul = false;
            }
            if (Request.QueryString["Branco"] != null && Request.QueryString["Branco"].ToString() != "")
            {
                ckbBranco.Checked = Request.QueryString["Branco"].ToString() == "S";
                setaMapaDefaul = false;
            }
            if (Request.QueryString["Fechados"] != null && Request.QueryString["Fechados"].ToString() != "")
                mostrarFechados = "&Fechados=" + Request.QueryString["Fechados"].ToString();

            if (Request.QueryString["CR"] != null && Request.QueryString["CR"].ToString() != "")
            {
                codigoResponsavel = Request.QueryString["CR"].ToString();
                setaMapaDefaul = false;
            }
        }

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidade, "vinculaMetasEstrategicasAMapasEmPaineis");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && setaMapaDefaul == false)
            setaMapaDefaul = dsParam.Tables[0].Rows[0]["vinculaMetasEstrategicasAMapasEmPaineis"].ToString() == "S";

        azul = ckbAzul.Checked ? "&Azul=S&" : "&Azul=N&";

        verde = ckbVerde.Checked ? "Verde=S&" : "Verde=N&";

        amarelo = ckbAmarelo.Checked ? "Amarelo=S&" : "Amarelo=N&";

        vermelho = ckbVermelho.Checked ? "Vermelho=S&" : "Vermelho=N&";

        branco = ckbBranco.Checked ? "Branco=S" : "Branco=N";
        
        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_AcsPnlMta");
        }

        carregaComboResponsaveis();
        carregaComboUnidades();
        carregaComboMapas();
        carregaComboIndicador();

        codigoResponsavel = ddlResponsavel.Value.ToString();
        codigoUnidade = ddlUnidadeNegocio.SelectedIndex == -1 ? "-1" : ddlUnidadeNegocio.Value.ToString();
        codigoMapa = ddlMapa.SelectedIndex == -1 ? "-1" : ddlMapa.Value.ToString();
        codigoIndicador = ddlIndicador.SelectedIndex == -1 ? "-1" : ddlIndicador.Value.ToString();
                
        cDados.aplicaEstiloVisual(this);

        defineLarguraTela();

        int nivel = 0;

        if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
            nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

        if (!IsPostBack)
        {
            lblTituloTela.Text = "<%# Resources.traducao.PainelMetas_painel_de_metas_estrat_gicas_ %>";

            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();            
        }

        Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "VISMET", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);

        string pesquisa = Server.UrlEncode(txtPesquisa.Text.TrimEnd());

        paginaIncialMetas = "../../_Estrategias/VisaoMetas/visaoMetas_02.aspx?CR=" + codigoResponsavel + azul + verde + amarelo + vermelho + branco + mostrarFechados + "&NivelNavegacao=" + nivel + "&CUN=" + codigoUnidade + "&CM=" + codigoMapa + "&CI=" + codigoIndicador + "&Meta=" + pesquisa;
        
        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidade);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblUnidade.Text = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString() + ":";
        }

        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../Content/sprite.css"" />"));
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

    private void carregaComboIndicador()
    {
        string comandoSQL = string.Format(@"
        SELECT i.CodigoIndicador, i.NomeIndicador 
          FROM {0}.{1}.Indicador i INNER JOIN
	           {0}.{1}.f_GetIndicadoresUsuario({2}, {3}, 'N') fiu ON fiu.CodigoIndicador = i.CodigoIndicador
         WHERE i.CodigoIndicador IN (SELECT iu.CodigoIndicador FROM {0}.{1}.IndicadorUnidade iu WHERE iu.DataExclusao IS NULL)
           AND i.DataExclusao IS NULL
         ORDER BY NomeIndicador", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlIndicador.DataSource = ds;
        ddlIndicador.TextField = "NomeIndicador";
        ddlIndicador.ValueField = "CodigoIndicador";
        DataRow dr = ds.Tables[0].NewRow();
        dr["NomeIndicador"] = "Todos";
        dr["CodigoIndicador"] = "-1";
        ds.Tables[0].Rows.InsertAt(dr, 0);
        ddlIndicador.DataBind();

        if (!IsPostBack)
            ddlIndicador.SelectedIndex = 0;
    }

    private void carregaComboUnidades()
    {
        string comandoSQL = string.Format(@"
SELECT un.CodigoUnidadeNegocio, un.SiglaUnidadeNegocio, un.NomeUnidadeNegocio
  FROM {0}.{1}.UnidadeNegocio un
 WHERE CodigoEntidade = {2}
   AND EXISTS(SELECT 1 FROM {0}.{1}.IndicadorUnidade iu WHERE iu.CodigoUnidadeNegocio = un.CodigoUnidadeNegocio AND iu.DataExclusao IS NULL AND iu.Meta IS NOT NULL)
 ORDER BY CodigoTipoUnidadeNegocio, NomeUnidadeNegocio", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade);

        DataSet dsUnidades = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(dsUnidades))
        {
            ddlUnidadeNegocio.DataSource = dsUnidades;
            ddlUnidadeNegocio.TextField = "SiglaUnidadeNegocio";
            ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";

            DataRow dr = dsUnidades.Tables[0].NewRow();
            dr["SiglaUnidadeNegocio"] = "Todas";
            dr["CodigoUnidadeNegocio"] = "-1";
            dr["NomeUnidadeNegocio"] = "Todas";
            dsUnidades.Tables[0].Rows.InsertAt(dr, 0);
            ddlUnidadeNegocio.DataBind();
        }
        if (!IsPostBack && ddlUnidadeNegocio.Items.Count > 0)
        {
            ddlUnidadeNegocio.SelectedIndex = 0;
        }
    }

    private void carregaComboMapas()
    {
        string comandoSQL = string.Format(@"
SELECT  me.CodigoMapaEstrategico, me.TituloMapaEstrategico
  FROM {0}.{1}.MapaEstrategico AS me INNER JOIN  
	   {0}.{1}.f_GetMapasEstrategicosUsuario({2}, {3}) AS f ON f.CodigoMapaEstrategico = me.CodigoMapaEstrategico INNER JOIN 
	   {0}.{1}.UnidadeNegocio AS un ON un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio 
 WHERE me.IndicaMapaEstrategicoAtivo = 'S'
   AND un.CodigoEntidade = {3}
 ORDER BY me.TituloMapaEstrategico", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade);

        DataSet dsMapas = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(dsMapas))
        {
            ddlMapa.DataSource = dsMapas;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";

            DataRow dr = dsMapas.Tables[0].NewRow();
            dr["TituloMapaEstrategico"] = "Todos";
            dr["CodigoMapaEstrategico"] = "-1";
            dsMapas.Tables[0].Rows.InsertAt(dr, 0);

            ddlMapa.DataBind();
        }
        if (!IsPostBack && ddlMapa.Items.Count > 0)
        {
            DataSet dsParametro = cDados.getMapaDefaultUsuario(idUsuarioLogado, "");

            if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]) && setaMapaDefaul)
            {
                string auxCodMapa = dsParametro.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"] + "";

                if (auxCodMapa != "" && ddlMapa.Items.FindByValue(auxCodMapa) != null)
                    ddlMapa.Value = auxCodMapa;
                else
                    ddlMapa.SelectedIndex = 0;
            }
            else
            {
                ddlMapa.SelectedIndex = 0;
            }
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 210).ToString() + "px";
    }  
}
