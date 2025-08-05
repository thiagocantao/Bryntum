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

public partial class _Projetos_DadosProjeto_indexResumoProjeto : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;
    private string nomePlano = "";
    private int codigoEntidadeLogada;
    private int codigoUsuarioResponsavel;

    public string telaInicial = "";
    public string alturaTabela;
    public int codigoPlano = 0;
    string nomeTelaInicial = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        imgAjudaGlossarioTipoProjeto.JSProperties["cp_CodigoGlossario"] = "-1";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        dbOwner = cDados.getDbOwner();
        dbName = cDados.getDbName();

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

        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"">var textoItem;</script>"));

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoPlano = int.Parse(Request.QueryString["CP"].ToString());
        }
        if (Request.QueryString["NP"] != null && Request.QueryString["NP"].ToString() != "")
        {
            nomePlano = Server.UrlDecode(Request.QueryString["NP"].ToString());
        }

        hfGeral.Set("hfNomePlano", nomePlano);

        alturaTabela = getAlturaTela() + "px";

        if (!IsPostBack)
        {
            carregaMenuLateral();
        }

        if (!IsPostBack)
        {
            defineTelaInicial();

            int nivel = 2;

            if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
                nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, nomePlano, "DT_PLN", "PLPLU", codigoPlano, "Adicionar Plano aos Favoritos");
        }
        this.Title = cDados.getNomeSistema();
        cDados.aplicaEstiloVisual(sp_Tela);
        cDados.aplicaEstiloVisual(nvbMenuProjeto);
    }

    private void defineTelaInicial()
    {
        telaInicial = "";

        if (nvbMenuProjeto.Groups.Count > 0)
        {
            nomeTelaInicial = "";
            
                for (int i = 0; i < nvbMenuProjeto.Groups.Count; i++)
                {
                    if (nvbMenuProjeto.Groups[i].Items.Count > 0 && nvbMenuProjeto.Groups[i].ClientVisible == true)
                    {
                        telaInicial = cDados.getPathSistema() + nvbMenuProjeto.Groups[i].Items[0].NavigateUrl.ToString().Replace("~/", "");
                        nomeTelaInicial = nvbMenuProjeto.Groups[i].Items[0].Text;
                        break;
                    }
                }   

            sp_Tela.Panes[1].ContentUrl = telaInicial;

            lblTituloTela.Text = nomePlano + " - " + nomeTelaInicial;
        }

        if (telaInicial == "" && nomeTelaInicial == "")
            cDados.RedirecionaParaTelaSemAcesso(this);
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        sp_Tela.Height = alturaTela - 135;
        return (alturaTela - 140).ToString();
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        sp_Tela.Panes[1].Size = new Unit(largura - 165);
        return largura - 140;
    }

    protected void nvbMenuProjeto_ItemClick(object source, NavBarItemEventArgs e)
    {

    }

    #region Menu Lateral

    private void carregaMenuLateral()
    {
        DataSet ds = cDados.getMenuObjeto(codigoUsuarioResponsavel.ToString(), codigoPlano.ToString(), "PL", cDados.getInfoSistema("CodigoEntidade").ToString());

        nvbMenuProjeto.Groups.Clear();

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {


            DataTable drGrupos = ds.Tables[0].DefaultView.ToTable(true, "NomeGrupo");

            int index = 0;

            foreach (DataRow drG in drGrupos.Rows)
            {
                NavBarGroup nbg = new NavBarGroup(drG["NomeGrupo"].ToString(), "G" + index.ToString());
                nvbMenuProjeto.Groups.Add(nbg);

                DataRow[] drItens = ds.Tables[0].Select("NomeGrupo = '" + drG["NomeGrupo"] + "'", "OrdemObjeto");

                foreach (DataRow drI in drItens)
                {
                    string alturaFrameForms = getAlturaTela();
                    int largura = getLarguraTela();
                    string larguraFrameForms = largura.ToString();
                    string idItem = "op_" + "G" + index.ToString() + "_" + drI["CodigoObjetoMenu"];
                    string urlItem = drI["URLObjetoMenu"].ToString();

                    urlItem = urlItem.Replace("@CP", codigoPlano.ToString());
                    urlItem = urlItem.Replace("@CodigoPortfolio", getCodigoPortfolio().ToString());

                    urlItem = urlItem.Replace("@Altura", alturaFrameForms.ToString()).Replace("@Largura", larguraFrameForms.ToString());

                    if (cDados.podeEditarPPA(codigoPlano, codigoEntidadeLogada, codigoUsuarioResponsavel) == false)
                        urlItem += "&RO=S";

                    nbg.Items.Add(drI["NomeMenu"].ToString(), idItem, "", urlItem, "framePrincipal");
                }

                if (nbg.Items.Count == 0)
                    nbg.ClientVisible = false;
            }
        }

        nvbMenuProjeto.AutoCollapse = true;
    }

    private int getCodigoPortfolio()
    {
        int codigoPortfolio = -1;

        string comandoSQL = string.Format(@"
        SELECT ISNULL(CodigoPortfolioAssociado, -1) AS CodigoPortfolio
          FROM dbo.Plano
         WHERE CodigoPlano = {0}", codigoPlano);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            codigoPortfolio = int.Parse(ds.Tables[0].Rows[0]["CodigoPortfolio"].ToString());

        cDados.setInfoSistema("CodigoPortfolio", codigoPortfolio);

        return codigoPortfolio;
    }

    #endregion

    protected void callbackMenu_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaMenuLateral();
    }

    protected void cbImagemAjuda_Callback(object sender, CallbackEventArgsBase e)
    {
        string retornoCodigoGlossario = "";
        string url = e.Parameter;
        string[] urlTratada = url.Substring(0, url.IndexOf('?')).Split('/');
        string urlTratada1 = "";
        for (int i = 3; i < urlTratada.Length; i++)
        {
            urlTratada1 += "\\" + urlTratada[i];
        }
        retornoCodigoGlossario = cDados.getCodigoGlossarioTela(urlTratada1);

        //retorno
        imgAjudaGlossarioTipoProjeto.JSProperties["cp_CodigoGlossario"] = retornoCodigoGlossario;
    }

    protected void imgAjudaGlossarioTipoProjeto_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        ((ASPxImage)sender).ClientVisible = false;
        DataSet ds = cDados.getParametrosSistema("utilizaGlossario");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) && !string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][0].ToString()))
            {
                if(ds.Tables[0].Rows[0][0].ToString().ToLower().Trim() == "s")
                {
                    ((ASPxImage)sender).ClientVisible = true;
                }
            }                
        }
    }
}

