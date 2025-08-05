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
using System.Collections.Specialized;
using System.Web.Hosting;
using System.IO;

public partial class _Portfolios_frameSelecaoBalanceamento : System.Web.UI.Page
{
    public string alturaTabela;
    public dados cDados;
    int codigoEntidadeUsuarioResponsavel;

    int codigoUsuarioResponsavel;

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



        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "PO_Bal");
        }

        tcOpcoes.Tabs.FindByName("Publicacao").ClientVisible = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "PO_Pub");

        carregaComboPortfolios();

        btnAtualizar.Style.Add("cursor", "pointer");

        if (!IsPostBack)
        {
            cDados.setInfoSistema("CodigoPortfolio", ddlPortfolio.Items.Count > 0 ? ddlPortfolio.Value.ToString() : "-1");
        }

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        alturaTabela = getAlturaTela() + "px";

        defineMenuAbas();

        if (!IsPostBack)
        {
            tcOpcoes.ActiveTabIndex = 0;
        }

        carregaGrid();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "SELBAL", "POR", -1, Resources.traducao.adicionar_aos_favoritos);
        }
        this.Title = cDados.getNomeSistema();
        cDados.traduzControles(this);

        //todo: Task 3222: [Tradução] - #1 - Ocultar Relatórios PDFs quando o idioma é diferente de PT-BR
        btnRelatorioRisco.ClientVisible = System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
    }

    private void defineMenuAbas()
    {
        tcOpcoes.Tabs[0].NavigateUrl = "frameSelecaoBalanceamento_Resumo.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        tcOpcoes.Tabs[1].NavigateUrl = "frameSelecaoBalanceamento_AnaliseGeral.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        tcOpcoes.Tabs[2].NavigateUrl = "frameSelecaoBalanceamento_AnaliseGrafica.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        tcOpcoes.Tabs[3].NavigateUrl = "frameSelecaoBalanceamento_OlapFluxoCaixa.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        tcOpcoes.Tabs[4].NavigateUrl = "frameSelecaoBalanceamento_OlapCriterios.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        tcOpcoes.Tabs[5].NavigateUrl = "frameSelecaoBalanceamento_OlapComplexidade.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        tcOpcoes.Tabs[6].NavigateUrl = "frameSelecaoBalanceamento_OlapRecursos.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        tcOpcoes.Tabs[7].NavigateUrl = "frameSelecaoBalanceamento_Simulacao.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        tcOpcoes.Tabs[8].NavigateUrl = "frameSelecaoBalanceamento_Publicacao.aspx?CodigoPortfolio=" + (ddlPortfolio.Value == null ? "-1" : ddlPortfolio.Value.ToString());
        //tcOpcoes.Tabs[8].NavigateUrl = "frameSelecaoBalanceamento_Relatorio.aspx";

        DataSet ds = cDados.getParametrosSistema("controlaRHPortfolio");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            tcOpcoes.Tabs[5].ClientVisible = ds.Tables[0].Rows[0]["controlaRHPortfolio"] + "" == "S" ? true : false;
       

    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 240).ToString();
    }

    private void carregaComboPortfolios()
    {
        DataSet dsPortfolios = cDados.getPortfolios(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(dsPortfolios))
        {
            ddlPortfolio.DataSource = dsPortfolios;

            ddlPortfolio.TextField = "DescricaoPortfolio";

            ddlPortfolio.ValueField = "CodigoPortfolio";

            ddlPortfolio.DataBind();

            if (cDados.DataTableOk(dsPortfolios.Tables[0]))
            {
                if (!IsPostBack)
                    ddlPortfolio.SelectedIndex = 0;

                DataRow[] dr = dsPortfolios.Tables[0].Select("CodigoPortfolio = " + ddlPortfolio.Value);

                tcOpcoes.Tabs.FindByName("Publicacao").ClientVisible = dr[0]["CarteiraAssociada"].ToString() == "-1";
            }
        }
    }

   

    private string getNomePortfolioAtual(int codPortfolio)
    {
        string nomePortfolioAtual = "";
        DataSet ds1 = cDados.getPortfolios(codigoEntidadeUsuarioResponsavel, " AND p.CodigoPortfolio = " + codPortfolio.ToString());

        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            nomePortfolioAtual = ds1.Tables[0].Rows[0]["DescricaoPortfolio"].ToString();
        }
        return nomePortfolioAtual;
    }

    protected void checkCriterio1_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio2_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio4_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio5_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio6_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio7_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio8_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    protected void checkCriterio9_CheckedChanged(object sender, EventArgs e)
    {
        string codigoProjeto = ((ASPxCheckBox)sender).Value.ToString();
    }

    private void carregaGrid()
    {
        int portfolio = -1;
        if (ddlPortfolio.Items.Count > 0)
        {
            portfolio = int.Parse(ddlPortfolio.Value.ToString());
        }
        string where = "";

        DataSet dsGrid = cDados.getProjetosPorCriterio(-1, portfolio, codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(dsGrid))
        {
            gvProjetos.DataSource = dsGrid;

            gvProjetos.DataBind();           
        }

    }

    protected void gvProjetos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters != Resources.traducao.frameSelecaoBalanceamento_atualizar)
        {
            string cenario = "IndicaCenario" + e.Parameters.Substring(0, 1);
            int codigoProposta = int.Parse(e.Parameters.Substring(2));
            string selecionado = "N";
            if (codigoProposta < 0)
            {
                codigoProposta = codigoProposta * -1;
                selecionado = "S";
            }

            ListDictionary table = new ListDictionary();

            table.Add(cenario, selecionado);

            cDados.update("Projeto", table, " CodigoProjeto = " + codigoProposta);
        }

        carregaGrid();
    }
    protected void gvProjetos_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGrid();
    }

    public string getCenarioMarcado(string idCheck, string marcado, string parametroCall, string codigoProjeto)
    {
        string checado = "";

        if (marcado.ToUpper() == "S")
            checado = "checked='true'";
        else
            codigoProjeto = "-" + codigoProjeto;

        parametroCall = parametroCall + codigoProjeto;

        return string.Format(@"<input id=""{0}"" type=""checkbox"" {1} onclick=""gvProjetos.PerformCallback('{2}');"" />", idCheck, checado, parametroCall);
    }


    protected void callBackVC_Callback1(object sender, CallbackEventArgsBase e)
    {
        callBackVC.JSProperties["cp_Funcao"] = e.Parameter;
        if (e.Parameter == "AtualizarVC")
        {
            cDados.setInfoSistema("CodigoPortfolio", ddlPortfolio.Items.Count > 0 ? ddlPortfolio.Value.ToString() : "-1");
        }
        else if (e.Parameter == "export")
        {
            string montaNomeArquivo = "";
            byte[] vetorBytes = null;
            DataSet ds = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                if (ds.Tables[0].Rows[0]["LogoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["LogoUnidadeNegocio"].ToString() != "")
                {
                    vetorBytes = (byte[])ds.Tables[0].Rows[0]["LogoUnidadeNegocio"];
                }
            }

            ASPxBinaryImage image1 = new ASPxBinaryImage();
            try
            {
                image1.ContentBytes = vetorBytes;

                if (image1.ContentBytes != null)
                {
                    montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "/ArquivosTemporarios/" + "logo" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim(' ') + ".png";
                    FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                    fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch
            {

            }

            string nomeUsuarioLogado = "";

            DataSet dsUsuario = cDados.getUsuarios(" and u.[CodigoUsuario] = " + codigoUsuarioResponsavel.ToString());
            if (cDados.DataSetOk(dsUsuario) && cDados.DataTableOk(dsUsuario.Tables[0]))
            {
                nomeUsuarioLogado = dsUsuario.Tables[0].Rows[0]["NomeUsuario"].ToString();
            }

            int codigoPortfolio = 0;
            if (Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
                codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
            else if (cDados.getInfoSistema("CodigoPortfolio") != null)
                codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());
            rel_SelecaoBalanceamentoPortfolio relatorio;
            relatorio = new rel_SelecaoBalanceamentoPortfolio(codigoEntidadeUsuarioResponsavel);
            relatorio.Parameters["pcodigoPortfolio"].Value = codigoPortfolio;
            relatorio.Parameters["pAno"].Value = "-1";
            relatorio.Parameters["pCaminhoArquivo"].Value = montaNomeArquivo;
            relatorio.Parameters["pCodigoEntidade"].Value = codigoEntidadeUsuarioResponsavel;
            relatorio.Parameters["pNomeUsuarioLogado"].Value = nomeUsuarioLogado;
            relatorio.Parameters["pNomePortfolio"].Value = getNomePortfolioAtual(codigoPortfolio);

            MemoryStream stream = new MemoryStream();
            relatorio.ExportToPdf(stream);
            Session["exportStream"] = stream;

        }
        }
    }
