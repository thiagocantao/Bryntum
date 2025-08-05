using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Xml;

public partial class administracao_adm_ConfiguracaoPessoais_Popup : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado = 0;
    private int idUsuarioSelecionado = 0;
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        this.Title = cDados.getNomeSistema();
    }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        idUsuarioSelecionado = int.Parse(string.IsNullOrEmpty(Request.QueryString["US"]) ? "-1" : Request.QueryString["US"]);
        
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        defineLabelCarteira();
        HeaderOnTela();
        carregaComboTelaInicial();
        cDados.defineConfiguracoesComboCarteiras(ddlVisaoInicial, IsPostBack, idUsuarioSelecionado, codigoEntidade, true);
        populaComboEntidad();

        if (!IsPostBack)
        {
            populaComboMapa();

            cargarDadosTela();
        }

        bool usarPainelInstitucional = true;

        DataSet dsParametros = cDados.getParametrosSistema("usarDashboardInstitucional");
        //tituloPaginsweb
        DataSet dsTituloPaginasWeb = cDados.getParametrosSistema("tituloPaginasWeb");
        if (cDados.DataSetOk(dsTituloPaginasWeb) && cDados.DataTableOk(dsTituloPaginasWeb.Tables[0]))
        {
            string titulo = dsTituloPaginasWeb.Tables[0].Rows[0]["tituloPaginasWeb"].ToString();
            ckbEmailSemanal.Text = Resources.traducao.receber_email_semanal_de_minhas_atividades_registradas_no + " " + titulo;
        }

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
            usarPainelInstitucional = dsParametros.Tables[0].Rows[0]["usarDashboardInstitucional"].ToString() == "S";


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
        cDados.aplicaEstiloVisual(this.Page);
    }
    private void carregaComboTelaInicial()
    {
        DataSet dsTela = cDados.getListaTelasIniciais(idUsuarioSelecionado, codigoEntidade, "");

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
        DataSet dsParametro = cDados.getParametrosSistema("labelCarteiras", "labelCarteirasPlural");
        string label = "Carteira";

        if ((cDados.DataSetOk(dsParametro)) && (cDados.DataTableOk(dsParametro.Tables[0])))
        {
            label = dsParametro.Tables[0].Rows[0]["labelCarteiras"].ToString();
        }

        rpVisaoInicial.HeaderText = label + " Inicial";

    }

    private void HeaderOnTela()
    {

        //Setea o Encabecado da página
        //string comando = string.Format(@"<script type='text/javascript'>logoVisible('none');</script>");
        //this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);

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
        DataSet ds = getConfiguracaoPessoais(codigoEntidade.ToString(), idUsuarioSelecionado.ToString());

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

            cmbModeloVisual.Value = estiloVisual;
            ddlEntidade.Value = entidadePadrao;
            ListEditItem li = ddlEntidade.Items.FindByValue(int.Parse(entidadePadrao));
            ddlEntidade.SelectedItem = li;

            if (ddlMapaEstrategico.Items.FindByValue(mapaPadrao) != null)
                ddlMapaEstrategico.Value = mapaPadrao;

            if ("" != painelBordo)
                ddlTelaInicial.Value = painelBordo;
            if ("" != padraoPortfolio)
                rblPorfolioPadrao.Value = padraoPortfolio;

            if ("" != ParametroVerificacaoAtrasoRelatorioAtividade)
                rblAtrasoAtividades.Value = ParametroVerificacaoAtrasoRelatorioAtividade;

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
                    ParametroVerificacaoAtrasoRelatorioAtividade
                FROM {0}.{1}.usuario AS u
                    INNER JOIN {0}.{1}.UsuarioUnidadeNegocio AS uun
                        ON u.CodigoUsuario = uun.CodigoUsuario
                WHERE u.CodigoUsuario = {2}
                    AND uun.CodigoUnidadeNegocio = {3}
                ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade);
        return cDados.getDataSet(comandoSQL);
    }

    protected void populaComboMapa()
    {
        DataSet ds = cDados.getMapaEstrategicoConfiguracaoPessoais(codigoEntidade.ToString(), idUsuarioSelecionado.ToString());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlMapaEstrategico.DataSource = ds.Tables[0];
            ddlMapaEstrategico.ValueField = "CodigoMapaEstrategico";
            ddlMapaEstrategico.TextField = "TituloMapaEstrategico";
            ddlMapaEstrategico.DataBind();
        }
    }

    protected void populaComboEntidad()
    {
        string where = string.Format(" AND UsuarioUnidadeNegocio.CodigoUsuario = {0}", idUsuarioSelecionado);
        DataSet ds = cDados.getEntidadesUsuario(idUsuarioSelecionado, where); // getEntidadeConfiguracaoPessoais(idUsuarioSelecionado.ToString());
        ddlEntidade.ToolTip = "Não é possível salvar pois este usuário não possui permissão de acesso a nenhuma entidade";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            callbackGeralConfig.JSProperties["cp_IndicaPermissaoAcessoEntidade"] = "S";
            ddlEntidade.DataSource = ds.Tables[0];
            ddlEntidade.ValueField = "CodigoUnidadeNegocio";
            ddlEntidade.TextField = "NomeUnidadeNegocio";
            ddlEntidade.DataBind();
            ddlEntidade.ToolTip = "";
        }
        else
        {
            callbackGeralConfig.JSProperties["cp_IndicaPermissaoAcessoEntidade"] = "N";
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
            string mapaPadrao = ddlMapaEstrategico.Value == null ? "" : ddlMapaEstrategico.Value.ToString();
            string painelBordo = ddlTelaInicial.Value.ToString();
            string portfolioPadrao = (rblPorfolioPadrao.Value == null ? "" : rblPorfolioPadrao.Value.ToString());
            string carteiraPadrao = ddlVisaoInicial.SelectedIndex == -1 ? "NULL" : ddlVisaoInicial.Value.ToString();
            string habilitaEmailSemanal = ckbEmailSemanal.Checked ? "S" : "N";
            string ParametroVerificacaoAtrasoRelatorioAtividade = (rblAtrasoAtividades.Value == null ? "" : rblAtrasoAtividades.Value.ToString());

            ds = atualizaConfiguracaoPessoais(temaVisual, entidadPadrao, mapaPadrao,
                                                     painelBordo, "N", "N",
                                                     "N", idUsuarioSelecionado.ToString(), portfolioPadrao, carteiraPadrao,
                                                     habilitaEmailSemanal, ParametroVerificacaoAtrasoRelatorioAtividade,
                                                     ref msg);

            cDados.setInfoSistema("IDEstiloVisual", temaVisual);

            string caminho = Session["NomeArquivoNavegacao"] + "";

            string telaIniciaUsuario = "";

            DataSet dsURL = cDados.getURLTelaInicialUsuario(codigoEntidade.ToString(), idUsuarioSelecionado.ToString());

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

            if (ddlVisaoInicial.SelectedIndex != -1)
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
                                                string ParametroVerificacaoAtrasoRelatorioAtividade,
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
                    ParametroVerificacaoAtrasoRelatorioAtividade = '{13}'  
                WHERE CodigoUsuario = {9}
        ", cDados.getDbName(), cDados.getDbOwner(), estiloTela,
                entidadPadrao == "" ? "NULL" : "'" + entidadPadrao + "'",
                mapaPadrao == "" ? "NULL" : "'" + mapaPadrao + "'",
                bordoPadrao,
                graficoBorda, graficoGradiente, tabelaPaginacao, codigoUsuario, portfolioPadrao, carteiraPadrao, habilitaEmailSemanal, ParametroVerificacaoAtrasoRelatorioAtividade);

            msg = "Configurações salvas com sucesso!";

            return cDados.getDataSet(comandoSQL);
        }
        catch
        {
            msg = "Erro ao salvar as configurações!";
            return null;
        }
    }


    protected void callbackGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackGeralConfig.JSProperties["cp_msg"] = persisteEdicaoRegistro();
        //Response.Redirect("~/administracao/adm_ConfiguracaoPessoais.aspx");
    }

}