using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;

public partial class _Projetos_DadosProjeto_cni_resumoProjeto : System.Web.UI.Page
{
    private int larguraTela;
    private int alturaTela;

    private int _codigoProjeto;

    public int CodigoProjeto
    {
        get { return _codigoProjeto; }
        set
        {
            _codigoProjeto = value;
        }
    }

    private string _nomeProjeto;
    /// <summary>
    /// </summary>
    /// 
    public string NomeProjeto
    {
        get { return _nomeProjeto; }
        set
        {
            _nomeProjeto = value;
        }
    }

    private dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
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
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
            CodigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

        if (Request.QueryString["NomeProjeto"] != null && Request.QueryString["NomeProjeto"].ToString() != "")
            NomeProjeto = Request.QueryString["NomeProjeto"].ToString();

        carregaResumoProjetos(CodigoProjeto);        
        
        if (!IsPostBack)
        {
            int nivel = 2;

            if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
                nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, NomeProjeto, "CNI_PRJ", "EST", CodigoProjeto, "Adicionar Projeto aos Favoritos");
        }

        carregaAcoesProjeto(CodigoProjeto);

        defineAlturaTela();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        cDados.getLarguraAlturaTela(resolucaoCliente, out larguraTela, out alturaTela);

        //divGeral.Style.Add("overflow-y", "scroll");
        //divGeral.Style.Add("height", "40%");

       // divGeral.Style.Add("height", "40%" /*getAlturaDePercentualAMenosDesejado(alturaTela, 60) + "px"*/);
        divGeral.Style.Add("padding-top", "5px");
    }

    private void carregaResumoProjetos(int CodigoProjeto)
    {
        DataSet dsResumoProjetosTemp = new DataSet();

        string comandoSQL = string.Format(
        @"DECLARE @RC int
          DECLARE @CodigoProjeto int
          set @CodigoProjeto = {2}
          EXECUTE @RC = {0}.{1}.p_cni_getDetalhesProjetoEstrategico @CodigoProjeto", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);

        dsResumoProjetosTemp = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(dsResumoProjetosTemp) && cDados.DataTableOk(dsResumoProjetosTemp.Tables[0]))
        {
            txtNomeProjeto.Text = dsResumoProjetosTemp.Tables[0].Rows[0]["NomeProjeto"].ToString();
            txtUnidadeResponsavel.Text = dsResumoProjetosTemp.Tables[0].Rows[0]["UnidadeResponsavelProjeto"].ToString();
            txtGerenteUnidade.Text = dsResumoProjetosTemp.Tables[0].Rows[0]["GerenteUnidade"].ToString();
            memObjetivoProjeto.Text = dsResumoProjetosTemp.Tables[0].Rows[0]["ObjetivoProjeto"].ToString();
            memCronograma.Text = dsResumoProjetosTemp.Tables[0].Rows[0]["CronogramaBasico"].ToString();
            memProdFinalResEsperados.Text = dsResumoProjetosTemp.Tables[0].Rows[0]["ProdutoFinal"].ToString();

            ASPxHyperLink1.Text = "Acessar Projeto";
            ASPxHyperLink1.NavigateUrl = string.Format(@"../../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}&NomeProjeto={1}", CodigoProjeto, NomeProjeto);
        }
        lblTituloTela.Text = "Projeto: " + NomeProjeto;
    }

    private void carregaAcoesProjeto(int CodigoProjeto)
    {
        DataSet dsAcaoTemp = new DataSet();

        string comandoSQL = string.Format(@"
        SELECT asu.DescricaoAcao
          FROM {0}.{1}.AcoesSugeridas asu INNER JOIN
			   {0}.{1}.ProjetoAcoesSugeridas pas ON (pas.CodigoAcaoSugerido = asu.CodigoAcaoSugerida
												 AND pas.CodigoProjeto = {2})
         WHERE asu.DataDesativacao IS NULL
         ORDER BY asu.DescricaoAcao", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);
        dsAcaoTemp = cDados.getDataSet(comandoSQL);

        gvAcao.DataSource = dsAcaoTemp;
        gvAcao.DataBind();
    }
}
