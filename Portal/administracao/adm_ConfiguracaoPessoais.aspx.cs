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
using System.Xml;

public partial class administracao_adm_ConfiguracaoPessoais : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado = 0;
    private int codigoEntidade = 0;

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

        
        
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        defineLabelCarteira();
        HeaderOnTela();
        carregaComboTelaInicial();
        cDados.defineConfiguracoesComboCarteiras(ddlVisaoInicial, IsPostBack, idUsuarioLogado, codigoEntidade, true);
        populaComboEntidad();

        if (!IsPostBack)
        {
            populaComboMapa();

            cargarDadosTela();


            bool usarPainelInstitucional = true;

            DataSet dsParametros = cDados.getParametrosSistema("usarDashboardInstitucional");
            //tituloPaginsweb
            DataSet dsTituloPaginasWeb = cDados.getParametrosSistema("tituloPaginasWeb");
            if (cDados.DataSetOk(dsTituloPaginasWeb) && cDados.DataTableOk(dsTituloPaginasWeb.Tables[0]))
            {
                string titulo = dsTituloPaginasWeb.Tables[0].Rows[0]["tituloPaginasWeb"].ToString();
                ckbEmailSemanal.Text = Resources.traducao.receber_email_semanal_de_minhas_atividades_registradas_no  + " " + titulo;
            }

            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
                usarPainelInstitucional = dsParametros.Tables[0].Rows[0]["usarDashboardInstitucional"].ToString() == "S";
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        if (!IsPostBack)
        {
            DataSet ds = cDados.getDefinicaoEntidade(codigoEntidade);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                rpEntidade.HeaderText = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
                ((ListBoxColumn)ddlEntidade.Columns[1]).Caption = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();

            }
            DataSet ds1 = cDados.getDefinicaoUnidade(codigoEntidade);
            if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
            {
                rblPorfolioPadrao.Items[1].Text = ds1.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            }
        }
        cDados.aplicaEstiloVisual(Page);
    }

    private void carregaComboTelaInicial()
    {
        DataSet dsTela = cDados.getListaTelasIniciais(idUsuarioLogado, codigoEntidade, "");

        if (cDados.DataSetOk(dsTela))
        {
            ddlTelaInicial.DataSource = dsTela;
            ddlTelaInicial.TextField = "NomeObjetoMenu_PT";
            ddlTelaInicial.ValueField = "Iniciais";
            ddlTelaInicial.DataBind();
        }
    }

    #region VARIOS

    private void defineLabelCarteira()
    {
        //Tratamento de Pronome para Masculino e Feminino em PT e em inglês utiliza-se o "The"
        if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
        {
            rpVisaoInicial.HeaderText = "Default View*";
        }
        else
        {
            DataSet dsParametro = cDados.getParametrosSistema("labelCarteiras", "labelCarteirasPlural");
            string label = Resources.traducao.adm_ConfiguracaoPessoais_carteira;
            if ((cDados.DataSetOk(dsParametro)) && (cDados.DataTableOk(dsParametro.Tables[0])))
            {
                label = dsParametro.Tables[0].Rows[0]["labelCarteiras"].ToString();
            }
            rpVisaoInicial.HeaderText = label + " Inicial*";
        }
    }

    private void HeaderOnTela()
    {

        //Setea o Encabecado da página
        //string comando = string.Format(@"<script type='text/javascript'>logoVisible('none');</script>");
        //this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/custom.css"" rel=""stylesheet""/>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_ConfiguracaoPessoais.js""></script>"));
        this.TH(this.TS("adm_ConfiguracaoPessoais"));
    }

    protected void cmbModeloVisual_SelectedIndexChanged(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(Page, cmbModeloVisual.Value.ToString());
    }

    #endregion

    #region GET_DADOS

    protected void cargarDadosTela()
    {
        DataSet ds = getConfiguracaoPessoais(codigoEntidade.ToString(), idUsuarioLogado.ToString());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string estiloVisual = ds.Tables[0].Rows[0]["IDEstiloVisual"].ToString();
            string entidadePadrao = ds.Tables[0].Rows[0]["CodigoEntidadeAcessoPadrao"].ToString();
            string mapaPadrao = ds.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"].ToString();
            string painelBordo = ds.Tables[0].Rows[0]["IndicaDashboardPadrao"].ToString();
            string bordaArredondada = ds.Tables[0].Rows[0]["IndicaGraficoBordaArredondada"].ToString();
            string gradienteGrafico = ds.Tables[0].Rows[0]["IndicaGraficoComGradiente"].ToString();
            string tabelaPaginacao = ds.Tables[0].Rows[0]["IndicaTabelaComPaginacao"].ToString();
            string padraoPortfolio = ds.Tables[0].Rows[0]["IndicaTipoPortfolioPadrao"].ToString();
            string habilitaEmailsSemanais = ds.Tables[0].Rows[0]["IndicaRecebimentoEmailAtividadesSemana"].ToString();
            string ParametroVerificacaoAtrasoRelatorioAtividade = ds.Tables[0].Rows[0]["ParametroVerificacaoAtrasoRelatorioAtividade"].ToString();
            string urlTelaInicialMobileUsuario = ds.Tables[0].Rows[0]["urlTelaInicialMobileUsuario"].ToString();
            cmbModeloVisual.Value = estiloVisual;
            ddlEntidade.Value = entidadePadrao;
            ListEditItem li = ddlEntidade.Items.FindByValue(int.Parse(entidadePadrao));
            ddlEntidade.SelectedItem = li;

            if (ddlMapaEstrategico.Items.FindByValue(mapaPadrao) != null)
                ddlMapaEstrategico.Value = mapaPadrao;
            else
                ddlMapaEstrategico.Value = "-1";

            if ("" != painelBordo)
                ddlTelaInicial.Value = painelBordo;
            if ("" != padraoPortfolio)
                rblPorfolioPadrao.Value = padraoPortfolio;

            if ("" != ParametroVerificacaoAtrasoRelatorioAtividade)
                rblAtrasoAtividades.Value = ParametroVerificacaoAtrasoRelatorioAtividade;

            if ("" != bordaArredondada)
            {
                if (bordaArredondada == "S")
                    ckBordasArredondadas.Checked = true;
                else
                    ckBordasArredondadas.Checked = false;
            }
            if ("" != gradienteGrafico)
            {
                if (gradienteGrafico == "S")
                    ckAplicarGradiente.Checked = true;
                else
                    ckAplicarGradiente.Checked = false;
            }
            if ("" != tabelaPaginacao)
            {
                if (tabelaPaginacao == "S")
                    ckTabelasComPaginacao.Checked = true;
                else
                    ckTabelasComPaginacao.Checked = false;
            }

            txtTelaInicialMobile.Text = urlTelaInicialMobileUsuario;

            if (habilitaEmailsSemanais == "S")
                ckbEmailSemanal.Checked = true;
            else
                ckbEmailSemanal.Checked = false;

        }
    }

    public DataSet getConfiguracaoPessoais(string codigoEntidade, string codigoUsuario)
    {
        string comandoSQL = string.Format(@"
                SELECT 
                    IDEstiloVisual,
                    CodigoEntidadeAcessoPadrao,
                    CodigoMapaEstrategicoPadrao,
                    IndicaDashboardPadrao,
                    IndicaGraficoBordaArredondada,
                    IndicaGraficoComGradiente,
                    IndicaTabelaComPaginacao,
                    IndicaTipoPortfolioPadrao,
                    IndicaRecebimentoEmailAtividadesSemana,
                    ParametroVerificacaoAtrasoRelatorioAtividade,
                    UrlTelaInicialMobileUsuario
                FROM {0}.{1}.usuario AS u
                    INNER JOIN {0}.{1}.UsuarioUnidadeNegocio AS uun
                        ON u.CodigoUsuario = uun.CodigoUsuario
                WHERE u.CodigoUsuario = {2}
                    AND uun.CodigoUnidadeNegocio = {3}
                ", cDados.getDbName(),cDados.getDbOwner(), codigoUsuario, codigoEntidade);
        return cDados.getDataSet(comandoSQL);
    }

    protected void populaComboMapa()
    {
        DataSet ds = cDados.getMapaEstrategicoComImagemConfiguracaoPessoais(codigoEntidade.ToString(), idUsuarioLogado.ToString());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlMapaEstrategico.DataSource = ds.Tables[0];
            ddlMapaEstrategico.ValueField = "CodigoMapaEstrategico";
            ddlMapaEstrategico.TextField = "TituloMapaEstrategico"; 
            ddlMapaEstrategico.DataBind();
        }

        ListEditItem lei = new ListEditItem(" ", "-1");
        ddlMapaEstrategico.Items.Insert(0, lei);
    }

    protected void populaComboEntidad()
    {
        string where = string.Format(" AND UsuarioUnidadeNegocio.CodigoUsuario = {0}",idUsuarioLogado);
        DataSet ds = cDados.getEntidadesUsuario(idUsuarioLogado, where); // getEntidadeConfiguracaoPessoais(idUsuarioLogado.ToString());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlEntidade.DataSource = ds.Tables[0];
            ddlEntidade.ValueField = "CodigoUnidadeNegocio";
            ddlEntidade.TextField = "NomeUnidadeNegocio";
            ddlEntidade.DataBind();
        }
    }

    #endregion

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        //Aqui estou declarando e inicializando as variaveis que irei usar
        DataSet ds;
        string msg = "";

        try
        {
            string temaVisual = cmbModeloVisual.Value.ToString();
            string entidadPadrao = ddlEntidade.Value.ToString();
            string mapaPadrao = (ddlMapaEstrategico.Value != null && ddlMapaEstrategico.Value.ToString() != "-1") ? ddlMapaEstrategico.Value.ToString() : "";
            string painelBordo = ddlTelaInicial.Value.ToString();
            string portfolioPadrao = (rblPorfolioPadrao.Value == null ? "" : rblPorfolioPadrao.Value.ToString());
            string bordasArredondadas = ckBordasArredondadas.Checked ? "S" : "N";
            string gradientesGraficos = ckAplicarGradiente.Checked ? "S" : "N";
            string tabelaPaginacao = ckTabelasComPaginacao.Checked ? "S" : "N";
            string carteiraPadrao = ddlVisaoInicial.SelectedIndex == -1 ? "NULL" : ddlVisaoInicial.Value.ToString();
            string habilitaEmailSemanal = ckbEmailSemanal.Checked ? "S" : "N";
            string ParametroVerificacaoAtrasoRelatorioAtividade = (rblAtrasoAtividades.Value == null ? "" : rblAtrasoAtividades.Value.ToString());
            string urlTelaInicialMobileUsuario = txtTelaInicialMobile.Text.Trim();

            ds = atualizaConfiguracaoPessoais(temaVisual, entidadPadrao, mapaPadrao,
                                                     painelBordo, bordasArredondadas, gradientesGraficos,
                                                     tabelaPaginacao, idUsuarioLogado.ToString(), portfolioPadrao, carteiraPadrao,
                                                     habilitaEmailSemanal, ParametroVerificacaoAtrasoRelatorioAtividade, urlTelaInicialMobileUsuario,
                                                     ref msg);

            cDados.setInfoSistema("IDEstiloVisual", temaVisual);

            if ((mapaPadrao != "") && (mapaPadrao != "-1"))
            {
                cDados.setInfoSistema("CodigoMapaEstrategicoInicial", mapaPadrao);
            }

            string caminho = Session["NomeArquivoNavegacao"] + "";

            string telaIniciaUsuario = "";

            DataSet dsURL = cDados.getURLTelaInicialUsuario(codigoEntidade.ToString(), idUsuarioLogado.ToString());

            if (cDados.DataSetOk(dsURL) && cDados.DataTableOk(dsURL.Tables[0]))
            {
                telaIniciaUsuario = dsURL.Tables[0].Rows[0]["TelaInicial"].ToString().Replace("~/", "");
            }

            if (Session["NomeArquivoNavegacao"] != null && caminho != "")
            {
                try
                {
                    XmlDocument doc = new XmlDocument();

                    doc.Load(caminho);

                    int i = 0;

                    XmlNode no = doc.SelectSingleNode(String.Format("/caminho/N[id={0}]", i));

                    if (no.SelectSingleNode("./nome").InnerText.Contains("Painel"))
                    {
                        no.SelectSingleNode("./url").InnerText = telaIniciaUsuario;
                        no.SelectSingleNode("./nome").InnerText = ddlTelaInicial.Text;
                    }

                    doc.Save(caminho);
                }
                catch
                {
                    Response.RedirectLocation = cDados.getPathSistema() + "po_autentica.aspx";
                }
            }            

            if(ddlVisaoInicial.SelectedIndex != -1)
                cDados.setInfoSistema("CodigoCarteira", carteiraPadrao);

            return msg;
        }
        catch (Exception ex)
        {
            //gvDados.ClientVisible = false;
            return ex.Message;
        }
    }

    public DataSet atualizaConfiguracaoPessoais(string estiloTela, string entidadPadrao, string mapaPadrao,
                                                string bordoPadrao, string graficoBorda, string graficoGradiente,
                                                string tabelaPaginacao, string codigoUsuario, string portfolioPadrao,
                                                string carteiraPadrao, string habilitaEmailSemanal,
                                                string ParametroVerificacaoAtrasoRelatorioAtividade,string urlTelaInicialMobileUsuario,
                                                ref string msg)
    {
        try
        {
            string comandoSQL = string.Format(@"
                UPDATE {0}.{1}.usuario
                SET
                    IDEstiloVisual                  ='{2}',
                    CodigoEntidadeAcessoPadrao      ={3},
                    CodigoMapaEstrategicoPadrao     ={4},
                    IndicaDashboardPadrao           ='{5}',
                    IndicaGraficoBordaArredondada   ='{6}',
                    IndicaGraficoComGradiente       ='{7}',
                    IndicaTabelaComPaginacao        ='{8}',
                    IndicaTipoPortfolioPadrao       ='{10}',
                    CodigoCarteiraPadrao            ={11},
                    IndicaRecebimentoEmailAtividadesSemana = '{12}',
                    ParametroVerificacaoAtrasoRelatorioAtividade = '{13}',
                    UrlTelaInicialMobileUsuario = {14}
                WHERE CodigoUsuario = {9}
        ", cDados.getDbName(), cDados.getDbOwner(), estiloTela,
                entidadPadrao == "" ? "NULL" : "'" + entidadPadrao + "'",
                mapaPadrao == "" ? "NULL" : "'" + mapaPadrao + "'",
                bordoPadrao,
                graficoBorda, graficoGradiente, tabelaPaginacao, codigoUsuario, portfolioPadrao, carteiraPadrao, habilitaEmailSemanal, ParametroVerificacaoAtrasoRelatorioAtividade,
                string.IsNullOrEmpty(urlTelaInicialMobileUsuario) == true ? "null" : "'" + urlTelaInicialMobileUsuario.ToString().Trim() + "'");

            msg = Resources.traducao.adm_ConfiguracaoPessoais_configura__es_salvas_com_sucesso_;

            return cDados.getDataSet(comandoSQL);
        }
        catch
        {
            msg = Resources.traducao.adm_ConfiguracaoPessoais_erro_ao_salvar_as_configura__es_;
            return null;
        }
    }


    protected void callbackGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackGeralConfig.JSProperties["cp_msg"] = persisteEdicaoRegistro();

        int nivel = 0;
        if (!string.IsNullOrWhiteSpace(Request.QueryString["NivelNavegacao"]))
            nivel = int.Parse(Request.QueryString["NivelNavegacao"]);
        cDados.excluiNiveisAbaixo(nivel);
        cDados.insereNivelPrerencia(0);
        Master.geraRastroSite();
    }
}