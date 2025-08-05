/*
 OBSERVAÇÕES
 
 12/01/2011 : by Alejandro : alteração do método 'private void criaMenuProjetos(){...}' consultando a permissão
                            disponivel do usuario.
 */
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Drawing;
using System.Web.Hosting;
using System.Xml;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public partial class cdis : System.Web.UI.MasterPage
{
    #region Variáveis

    public dados cDados;

    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;

    private string labelQuestoes = "Questões";
    private string labelToDoList = "To Do List";

    public string mostrarOpAgenda;
    public bool montarMenuPortfolio;
    public bool montarMenuProjeto;
    public bool montarMenuDemandas;
    public bool montarMenuEstrategia;
    public bool montarMenuOrcamento;
    public bool montarMenuFinanceiro;
    public bool montarMenuAdministracao;
    public bool montarMenuEspacoTrabalho;
    public bool mostrarMenuProcessos;
    public string mostrarAcaoFavoritos = "none";
    public string imagemFundoCentro = "", imagemFundoEsquerdo = "", imagemFundoTopo = "";

    private string urlProcessosExternos = "";
    private string urlSistemaOrcamento = "";

    string lblEspacoTrabalho = "";
    string lblEstrategia = "";
    string lblProjetos = "";
    string lblDemandas = "";
    string lblAdministracao = "";
    string lblFinanceiro = "";
    string distanciaMenuLabels = "75px";
    public string posicaoMenu = "center";
    bool utilizaLogoLadoDireito = false;
    private string bloqueiaAbas = System.Configuration.ConfigurationManager.AppSettings["bloqueiaAbas"] != null ? System.Configuration.ConfigurationManager.AppSettings["bloqueiaAbas"].ToString() : "N";
    public string scriptJs = "";
    bool isMobile = false;

    #endregion

    /* Experimento Bootstrap */
    public bool paginaFrame = false;

    public string baseUrl;

    public string idioma;

    public string logo = "";

    public string banner = "";

    public string menu = "";
    public string novoMenu = "";
    public string menuMobile = "";
    public string menuAdministracao = "";
    public string novoMenuAdministracao = "";
    public string menuMobileAdministracao = "";
    private string linkMenuAlterarEntidade = "";
    private string linkMenuAlterarSenha = "";
    private string linkMenuGerenciarPreferencias = "";
    private string linkMenuOrganizarFavoritos = "";
    public string menuAlterarEntidade = "";
    public string menuAlterarSenha = "";
    public string menuGerenciarPreferencias = "";
    public string menuOrganizarFavoritos = "";
    public bool permiteFavoritos = false;
    public string favoritos = "";
    public string novoFavoritos = "";
    public string entidades = "";
    public string usuario = "";
    public string rastro = "";
    public string sobre = "";
    public string labelCarteira = "Carteira";
    public string carteira = "";
    public bool existemMensagens = false;
    public bool existemNotificacoes = false;
    public bool existemTarefas = false;
    public string mensagens = "";
    public string notificacoes = "";
    public string tarefas = "";
    string NomeRepetido = "";
    public string hintNotificacoes = "";
    public string hintMensagens = "";
    public string hintTarefas = "";
    public int quantidadeDiasAlertaContratosVencimento = 60;
    public string definicaoQuestao = "";
    public string definicaoQuestaoPlural = "";
    public string definicaoQuestaoSingular = "";
    public string labelCriticaSingular = "";
    public string labelCriticaPlural = "";
    public string urlWSnewBriskBase = "";

    protected void Page_Init(object sender, EventArgs e)
    {


        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        idioma = cultureInfo.ToString();

        this.TH(this.TS("geral", "menu", "novaCdis", "mensagemErro", "barraNavegacao"));

        #region Page_Init

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        try
        {
            cDados = CdadosUtil.GetCdados(null);
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }
        bool indicaHttpsViaProxyReverso = false;
        DataSet dsParam1 = cDados.getParametrosSistema("httpsViaProxyReverso");
        if (cDados.DataSetOk(dsParam1) && cDados.DataTableOk(dsParam1.Tables[0]))
        {
            indicaHttpsViaProxyReverso = dsParam1.Tables[0].Rows[0]["httpsViaProxyReverso"].ToString().ToUpper() == "S";
        }

        if (indicaHttpsViaProxyReverso == true)
        {
            baseUrl = "https://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
        }
        else
        {

            bool indicaHttps = false;
            DataSet dsParam = cDados.getParametrosSistema("utilizaProtocoloHttps");
            if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            {
                indicaHttps = dsParam.Tables[0].Rows[0]["utilizaProtocoloHttps"].ToString().ToUpper() == "S";
            }

            /* Experimento Bootstrap */
            if (indicaHttps == true)
            {
                baseUrl = "https://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
            }
            else
            {
                baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
            }
        }

        //Busca a variavel de sessão que foi inicializada na login
        urlWSnewBriskBase = Session["urlWSnewBriskBase"]+"";
        //Se não tiver na sessão busca novamente no parâmetros do sistema
        if (urlWSnewBriskBase == "") {
            DataSet dsParam2 = cDados.getParametrosSistema("urlWSnewBriskBase");
            if (cDados.DataSetOk(dsParam2) && cDados.DataTableOk(dsParam2.Tables[0]))
            {
                urlWSnewBriskBase = dsParam2.Tables[0].Rows[0]["urlWSnewBriskBase"].ToString();
            }
            else
            {
                urlWSnewBriskBase = "http://localhost:50977";
                //urlWSnewBriskBase = "https://ws.desenv.briskppm.com.br/brisk-bsc-ptbr";
            }
            Session["urlWSnewBriskBase"] = urlWSnewBriskBase;
        }

        hfSignalR.Set("urlWSnewBriskBase", urlWSnewBriskBase);

        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        hfSignalR.Set("CodigoEntidade", codigoEntidadeLogada);
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        callbackSession.JSProperties["cp_Usuario"] = cDados.getInfoSistema("NomeUsuarioLogado").ToString();
        callbackSession.JSProperties["cp_IDUsuario"] = cDados.getInfoSistema("IDUsuarioLogado").ToString();

        #endregion

        logo = @"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8/5+hHgAHggJ/PchI7wAAAABJRU5ErkJggg=="; // One pixel transparent PNG

        linkMenuAlterarEntidade = @"<li><a href=""#"" title=""" + this.T("menu_alterar_entidade") + @""" onclick=""showModal('{1}', '" + this.T("menu_alterar_entidade") + @"', 620, null, '', null);"">" + this.T("menu_alterar_entidade") + "</a></li>";
        linkMenuAlterarSenha = @"<li><a href=""{0}"" title=""" + this.T("menu_alterar_senha") + @""" > " + this.T("menu_alterar_senha") + "</a></li>";
        linkMenuGerenciarPreferencias = @"<li><a href=""{0}"" title=""" + this.T("menu_gerenciar_preferencias") + @""" > " + this.T("menu_gerenciar_preferencias") + "</a></li>";
        linkMenuOrganizarFavoritos = @"<li><a href=""{0}"" title=""" + this.T("menu_organizar_favoritos") + @""" > " + this.T("menu_organizar_favoritos") + "</a></li>";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //Valida a aparição do botão de fluxos para mobile
        if (!Request.Browser.IsMobileDevice)
        {
            menuPrincipalPassoFluxo.Visible = false;
        }
        else
        {
            menuPrincipalPassoFluxo.Visible = true;
        }

        /* Experimento Bootstrap */
        Head();

        #region Page_Load

        formMaster.Action = Request.RawUrl;

        //isMobile = cDados.isMobileBrowser();
        if (!IsPostBack && !Page.IsCallback)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //// isso é para impedir que o site seja aberto em duas abas. Se já existe sessão, a chamada tem de ser originada em uma url do sistema
            if (cDados.getInfoSistema("temSessaoPortal") != null)
            {
                //    // se não existe url anterior é pq esta tentando abrir uma aba nova... não pode.
                if (Request.UrlReferrer == null && bloqueiaAbas == "S")
                {
                    Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
                    //Response.Redirect("~/branco.htm");
                    Response.End();
                }
            }

            cDados.setInfoSistema("temSessaoPortal", "S");
        }
        //imgGlossario.JSProperties["cp_CodigoGlossario"] = cDados.getCodigoGlossarioTela(this.Page);
        pcModal.JSProperties["cp_Path"] = cDados.getPathSistema();
        pcModalComFooter.JSProperties["cp_Path"] = cDados.getPathSistema();
        pcModalComFooter2.JSProperties["cp_Path"] = cDados.getPathSistema();
        pcFormularioDinamico.JSProperties["cp_Path"] = cDados.getPathSistema();

        configuraIntervaloAtualizaNotificacoes();

        string primeiroNome = cDados.getInfoSistema("NomeUsuarioLogado").ToString();
        string siglaUnidade = " ";

        if (cDados.getInfoSistema("NomeUsuarioLogado").ToString().IndexOf(" ") != -1)
            primeiroNome = cDados.getInfoSistema("NomeUsuarioLogado").ToString().Substring(0, cDados.getInfoSistema("NomeUsuarioLogado").ToString().IndexOf(" "));


        //// Para a tela de seleçao de entidades, não pode mostrar a sigla, pois o usuário ainda não definiu com qual entidade deseja trabalhar
        object opcao = cDados.getInfoSistema("Opcao");
        if (opcao == null)
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }
        if (opcao.ToString() != "se")
        {
            // busca a(s) entidade(s) do usuário logado
            DataSet dsAux = cDados.getEntidadesUsuario(codigoUsuarioLogado, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuarioLogado.ToString());
            int QtdeEntidades = dsAux.Tables[0].Rows.Count;

            if (QtdeEntidades > 1) // o usuário tiver acesso a mais de uma entidade
                siglaUnidade = string.Format(@" (<a href='#' style='cursor:pointer;' title='Alterar Entidade' id='linkEntidades' onclick='showModal(""{1}"", ""Alterar Entidade"", 620, 400, """", null);'>{0}</a>)"
                    , cDados.getInfoSistema("SiglaUnidadeNegocio").ToString()
                    , cDados.getPathSistema() + "popUpSelecaoEntidade.aspx");
        }

        //lblBemVindo.Text = Resources.traducao.ol_ + ", " + primeiroNome + siglaUnidade + "&nbsp;";

        DataSet ds = cDados.getURLTelaInicialUsuario(codigoEntidadeLogada.ToString(), codigoUsuarioLogado.ToString());

        string urlDestino = "";

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            urlDestino = ds.Tables[0].Rows[0]["TelaInicial"].ToString().Replace("~/", "");
        }

        string eventoHome = string.Format(@"gotoURL('{0}', '_self');", urlDestino);

        //string baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/') + "/";
        string homeIconeUrl = baseUrl + "imagens/principal/Home_Menu.PNG";
        //imgHome.ClientSideEvents.Click = "function(s, e) { " + eventoHome + "}";

        if (!Page.IsCallback)
        {
            montaMenu();
        }

        if (!IsPostBack)
        {

            cDados.aplicaEstiloVisual(this);
        }

        geraFavoritos();

        if (!Page.IsCallback)
        {


            quantidadeDiasAlertaContratosVencimento = 60;

            definicaoQuestao = "Questão";
            definicaoQuestaoPlural = "questões ativas";
            definicaoQuestaoSingular = "questão ativa";
            labelCriticaSingular = "crítica";
            labelCriticaPlural = "críticas";
            DataSet dsParametros = cDados.getParametrosSistema("urlProcessosExternos", "UrlOrcamento", "mostrarMenuComIcones", "distanciaMenuTopo", "posicaoMenu", "utilizaLogoUnidade", "TempoMaximoSessaoUsuario", "QuantidadeDiasAlertaContratosVencimento", "labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

            string mostrarMenuComIcones = "N";
            distanciaMenuLabels = "55" + "px";

            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
            {
                mostrarMenuComIcones = dsParametros.Tables[0].Rows[0]["mostrarMenuComIcones"].ToString() == "N" ? "N" : "S";
                distanciaMenuLabels = dsParametros.Tables[0].Rows[0]["distanciaMenuTopo"].ToString() == "" ? "55px" : dsParametros.Tables[0].Rows[0]["distanciaMenuTopo"].ToString() + "px";
                urlProcessosExternos = dsParametros.Tables[0].Rows[0]["urlProcessosExternos"].ToString();
                urlSistemaOrcamento = dsParametros.Tables[0].Rows[0]["UrlOrcamento"].ToString();
                utilizaLogoLadoDireito = dsParametros.Tables[0].Rows[0]["utilizaLogoUnidade"] + "" == "S";
                quantidadeDiasAlertaContratosVencimento = int.Parse(dsParametros.Tables[0].Rows[0]["QuantidadeDiasAlertaContratosVencimento"].ToString());
                if (dsParametros.Tables[0].Rows[0]["TempoMaximoSessaoUsuario"].ToString() != "")
                {
                    int tempo = int.Parse(dsParametros.Tables[0].Rows[0]["TempoMaximoSessaoUsuario"].ToString());
                    Session.Timeout = tempo;
                }


                string genero = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";
                definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
                definicaoQuestaoPlural = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
                definicaoQuestaoSingular = string.Format(@"{0} ativ{1}", definicaoQuestao, genero == "M" ? "o" : "a");
                labelCriticaSingular = string.Format(@"crític{0}", genero == "M" ? "o" : "a");
                labelCriticaPlural = string.Format(@"crític{0}", genero == "M" ? "os" : "as");



            }

            if (mostrarMenuComIcones == "S" || isMobile)
                criaMenuHTMLIcones();
            else
            {
                posicaoMenu = dsParametros.Tables[0].Rows[0]["posicaoMenu"].ToString();
                imagemFundoEsquerdo = cDados.getPathSistema() + "espacoCliente/fundoEsquerdo.png";
                imagemFundoCentro = cDados.getPathSistema() + "espacoCliente/fundoCentro.png";
                imagemFundoTopo = cDados.getPathSistema() + "espacoCliente/fundoTopo.png";

                criaMenuHTMLLabels();
            }

            //imgLogo.Value = null;
            //imgLogo.DataBind();

            defineLogo();
            defineLogoUnidade();

            //cDados.defineConfiguracoesComboCarteiras(ddlVisaoInicial, IsPostBack, codigoUsuarioLogado, codigoEntidadeLogada, false);
            defineImgStatusReport();
            defineLabelCarteira();

            mostrarCarteiras();

            DefineConfiguracoesPeriodo();
        }
        Page.Title = cDados.getNomeSistema();

        #endregion

        /* Experimento Bootstrap */
        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(this);
        }
        if (!paginaFrame)
        {
            Logo();
            Banner();
            MenuBootstrap4xMegadrop(false);
            MenuBootstrap4xMegadrop(true);
            Favoritos();
            Entidades();
            Rastro();
            Sobre();
            Usuario();
            Carteira();
            //Alertas();
        }

        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString(), out largura, out altura);
        if (largura <= 800)
        {

            lblMensagemApresentacaoAcao.Wrap = DevExpress.Utils.DefaultBoolean.True;
        }
        else
        {
            lblMensagemApresentacaoAcao.Wrap = DevExpress.Utils.DefaultBoolean.False;
        }

        string email = cDados.getUsuarios(" and CodigoUsuario = " + codigoUsuarioLogado.ToString()).Tables[0].Rows[0]["EMail"].ToString();
        hfSignalR.Set("EmailUsuario", email);
        hfSignalR.Set("QuantidadeDiasAlertaContratosVencimento", quantidadeDiasAlertaContratosVencimento);

        hfSignalR.Set("definicaoQuestao", definicaoQuestao);
        hfSignalR.Set("definicaoQuestaoPlural", definicaoQuestaoPlural);
        hfSignalR.Set("definicaoQuestaoSingular", definicaoQuestaoSingular);
        hfSignalR.Set("labelCriticaSingular", labelCriticaSingular);
        hfSignalR.Set("labelCriticaPlural", labelCriticaPlural);
        hfSignalR.Set("baseUrl", baseUrl);
        hfSignalR.Set("TokenAcessoNewBrisk", Session["TokenAcessoNewBrisk"]);
        hfSignalR.Set("CodigoEntidadeUsuarioLogado", codigoEntidadeLogada);
        montaIconesDeNotificacoes();
        configuraURLDoLinkDasNotificacoesIgualAoMenuPrincipal();
    }

    private void montaIconesDeNotificacoes()
    {
        if (Session["notificacoes"] == null || Session["notificacoes"] + "" == "")
            return;
        string minhasNotificacoes = Session["notificacoes"].ToString();

        Notificacoes notificacoes = JsonConvert.DeserializeObject<Notificacoes>(minhasNotificacoes);

        string existeNotificacaoMensagem = "nav-notification";
        if (notificacoes.indicaIconeMensagemLigado == false)
        {
            existeNotificacaoMensagem = "nav-notification-gray";
        }

        string existeNotificacao = "nav-notification-gray";
        if (notificacoes.indicaIconeRiscoS1Ligado == true ||
            notificacoes.indicaIconePndWfLigado == true ||
            notificacoes.indicaIconeReuniaoLigado == true ||
            notificacoes.indicaIconeContratoLigado == true ||
            notificacoes.indicaIconeIndicadorLigado == true ||
            notificacoes.indicaIconeAprovTrfLigado == true)
        {
            existeNotificacao = "nav-notification";
        }

        string existeNotificacaoTarefa = "nav-notification";
        if (notificacoes.indicaIconeTarefaS1Ligado == false)
        {
            existeNotificacaoTarefa = "nav-notification-gray";
        }

        menuPrincipalExistemMensagens.Attributes.Add("class", existeNotificacaoMensagem);
        menuPrincipalExistemMensagens.InnerHtml = string.Format(@"<span id=""bolinhaNotificacaoMensagem"" class=""estiloBolinhasDeNotificacoes"">{0}</span>", notificacoes.qtdMensagem > 99 ? "+99" : notificacoes.qtdMensagem.ToString());

        int somaNotificacoes = 0;


        somaNotificacoes += notificacoes.qtdPndWf;
        somaNotificacoes += notificacoes.qtdRiscoS1;
        somaNotificacoes += notificacoes.qtdContrato;
        somaNotificacoes += notificacoes.qtdQuestaoS1;
        somaNotificacoes += notificacoes.qtdReuniao;
        somaNotificacoes += notificacoes.qtdIndicador;
        somaNotificacoes += notificacoes.qtdAprovTrf;

        menuPrincipalExistemNotificacoes.Attributes.Add("class", existeNotificacao);
        menuPrincipalExistemNotificacoes.InnerHtml = string.Format(@"<span id=""bolinhaNotificacaoNotificacoes"" class=""estiloBolinhasDeNotificacoes"">{0}</span>", somaNotificacoes > 99 ? "+99": somaNotificacoes.ToString());

        menuPrincipalExistemTarefas.Attributes.Add("class", existeNotificacaoTarefa);
        menuPrincipalExistemTarefas.InnerHtml = string.Format(@"<span id=""bolinhaNotificacaoTarefas"" class=""estiloBolinhasDeNotificacoes"">{0}</span>", notificacoes.qtdTarefaS1 > 99 ? "+99" : notificacoes.qtdTarefaS1.ToString());

        if(bolinhaNotificacaoTarefas.InnerText.Trim() != "")
        {
            iconeMensagens.Attributes.Add("data-toggle", "dropdown");
            iconeMensagens.Attributes.Add("href", "#");
            iconeMensagens.Attributes.Add("title", this.T("menu_mensagens"));            

            iconeNotificacoes.Attributes.Add("data-toggle", "dropdown");
            iconeNotificacoes.Attributes.Add("href", "#");
            iconeNotificacoes.Attributes.Add("title", this.T("menu_notificacoes"));

            iconeTarefas.Attributes.Add("data-toggle", "dropdown");
            iconeTarefas.Attributes.Add("href", "#");
            iconeTarefas.Attributes.Add("title", this.T("menu_tarefas"));
        }
    }

    private void configuraURLDoLinkDasNotificacoesIgualAoMenuPrincipal()
    {
        DataSet dsLinkAprovacoes = cDados.getDataSet("SELECT UrlObjetoMenu FROM OBJETOMENU WHERE INICIAIS = 'APROV'");
        if (cDados.DataSetOk(dsLinkAprovacoes) && cDados.DataTableOk(dsLinkAprovacoes.Tables[0]))
        {
            hfLinkAprovacoes.Set("valor", dsLinkAprovacoes.Tables[0].Rows[0]["UrlObjetoMenu"].ToString().Replace("~/", ""));
        }
    }

    private void configuraIntervaloAtualizaNotificacoes()
    {
        int quantidadeSegundosAtualizaNotificacoes = 600;
        DataSet dsAtualiza = cDados.getParametrosSistema(codigoEntidadeLogada, "IntervaloAtualizaNotificacoesEmSegundos");
        bool retorno = int.TryParse(dsAtualiza.Tables[0].Rows[0]["IntervaloAtualizaNotificacoesEmSegundos"].ToString(), out quantidadeSegundosAtualizaNotificacoes);
        if (!retorno)
        {
            quantidadeSegundosAtualizaNotificacoes = 600;
        }
        pcModal.JSProperties["cp_IntervaloAtualizacaoNotificacoes"] = quantidadeSegundosAtualizaNotificacoes * 1000;


        TimeSpan t = TimeSpan.FromSeconds(quantidadeSegundosAtualizaNotificacoes);
        bool existeHora = false;
        bool existeMinuto = false;

        string sufixo_concatenado = "";

        if (t.Hours > 0)
        {
            existeHora = true;
            if (t.Hours == 1)
            {
                sufixo_concatenado += t.Hours + " " + Resources.traducao.hora;
            }
            else
            {
                sufixo_concatenado += t.Hours + " " + Resources.traducao.horas;
            }
        }
        if (t.Minutes > 0)
        {
            existeMinuto = true;
            if (t.Minutes == 1)
            {
                sufixo_concatenado += ((existeHora == true) ? ", " : "") + t.Minutes + " " + Resources.traducao.minuto + " ";
            }
            else
            {
                sufixo_concatenado += ((existeHora == true) ? ", " : "") + t.Minutes + " " + Resources.traducao.minutos + " ";
            }
        }

        if (t.Seconds > 0)
        {
            if (t.Seconds == 1)
            {
                sufixo_concatenado += ((existeMinuto == true) ? Resources.traducao.e + " " : "") + t.Seconds + " " + Resources.traducao.segundo;
            }
            else
            {
                sufixo_concatenado += ((existeMinuto == true) ? Resources.traducao.e + " " : "") + t.Seconds + " " + Resources.traducao.segundos;
            }
        }

        hintNotificacoes = Resources.traducao.menu_notifica__es_notifica__es__os_n_meros_s_o_atualizados_ao_acionar_a_logomarca_no_canto_superior_esquerdo_da_tela_e_tamb_m_por_configura__o_de_par_metro_a_cada_ + " " + sufixo_concatenado;
        hintMensagens = Resources.traducao.menu_notifica__es_mensagens__os_n_meros_s_o_atualizados_ao_acionar_a_logomarca_no_canto_superior_esquerdo_da_tela_e_tamb_m_por_configura__o_de_par_metro_a_cada_ + " " + sufixo_concatenado;
        hintTarefas = Resources.traducao.menu_notifica__es_tarefas__os_n_meros_s_o_atualizados_ao_acionar_a_logomarca_no_canto_superior_esquerdo_da_tela_e_tamb_m_por_configura__o_de_par_metro_a_cada_ + " " + sufixo_concatenado;
    }

    /* Experimento Bootstrap */
    private void Head()
    {
        HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/bootstrap/v4.1.3/css/bootstrap.min.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/fonts/fontawesome/v5.0.12/css/fontawesome-all.min.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/fonts/material-icons/v3.0.1/material-icons.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/fonts/Linearicons/v1.0.0/Linearicons.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        //HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/css/custom.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/css/menu.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js""></script>", baseUrl)));
        HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/bootstrap/v4.1.3/js/bootstrap.bundle.min.js""></script>", baseUrl)));
        //HeaderContent.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/js/custom.js""></script>", baseUrl)));
        FooterContent.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/js/menu.js""></script>", baseUrl)));
        //FooterContent.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}scripts/dist/browser/signalr.js""></script>", baseUrl)));

        HeadContentScriptsShowModal.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/scripts/dist/browser/signalr.js""></script>", baseUrl)));
        HeadContentScriptsShowModal.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/scripts/HubNotification.js""></script>", baseUrl)));

        HeadContentScriptsShowModal.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/scripts/showModal/showModal.js""></script>", baseUrl)));
        HeadContentScriptsShowModal.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/scripts/showModal/showModal2.js""></script>", baseUrl)));
        HeadContentScriptsShowModal.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/scripts/showModal/showModalComFooter.js""></script>", baseUrl)));
        HeadContentScriptsShowModal.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/scripts/showModal/showModalComFooter2.js""></script>", baseUrl)));
        HeadContentScriptsShowModal.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/scripts/showModal/showModalFormulario.js""></script>", baseUrl)));
        HeadContentScriptsShowModal.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/scripts/showModal/mostraMensagem.js""></script>", baseUrl)));

    }

    /* Experimento Bootstrap */
    protected string Html(string html)
    {
        return (HttpUtility.HtmlEncode(html));
    }

    /* Experimento Bootstrap */
    protected string Link(string link)
    {
        if (link.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase) || link.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
        {
            return (link);
        }
        if (link.Substring(0, 2) == "~/")
        {
            link = link.Substring(1);
        }
        else
        {
            link = "/" + link.TrimStart('/');
        }
        link = baseUrl + link;
        return (link);
    }

    protected string Url(string url)
    {
        return (Server.UrlEncode(url.ToString()));
    }

    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }

    /* Experimento Bootstrap */
    protected void Logo()
    {
        try
        {
            DataSet ds = cDados.getLogoEntidade(codigoEntidadeLogada, "");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0]["LogoUnidadeNegocio"] != null && dt.Rows[0]["LogoUnidadeNegocio"].ToString() != "")
                {
                    logo = "data:image/png;base64," + Convert.ToBase64String((byte[])dt.Rows[0]["LogoUnidadeNegocio"]);
                }
            }
        }
        catch (Exception ex)
        {
            string msgErro = ex.Message;
        }
    }

    protected void Banner()
    {
        banner = string.Format(@"{0}/Bootstrap/images/BannerBrisk.png", baseUrl);
    }

    /* Experimento Bootstrap */
    protected void MenuBootstrap4xMegadrop(bool administracao = false)
    {
        if (administracao)
        {
            if (cDados.getInfoSistema("novoMenuAdministracao") != null)
            {
                novoMenuAdministracao = cDados.getInfoSistema("novoMenuAdministracao").ToString();
                return;
            }
        }
        else
        {
            if (cDados.getInfoSistema("novoMenu") != null)
            {
                novoMenu = cDados.getInfoSistema("novoMenu").ToString();
                if (cDados.getInfoSistema("menuAlterarSenha") != null)
                {
                    menuAlterarSenha = cDados.getInfoSistema("menuAlterarSenha").ToString();
                }
                if (cDados.getInfoSistema("menuGerenciarPreferencias") != null)
                {
                    menuGerenciarPreferencias = cDados.getInfoSistema("menuGerenciarPreferencias").ToString();
                }
                if (cDados.getInfoSistema("menuOrganizarFavoritos") != null)
                {
                    menuOrganizarFavoritos = cDados.getInfoSistema("menuOrganizarFavoritos").ToString();
                }
                return;
            }
        }

        int nivel;
        string hierarquia;
        string nome;
        string iniciais;
        string url;
        string icone;
        string cor;
        string novoHtml = "";
        string possuiMenuInferior = "";

        int proximoNivel;

        int inseridos = 0;

        string comandoSQL;

        /*
         * Quando o idioma de contexto for passado para o SQL Server no ato da conexão, nas Classes Complementares, onde as PROCs e FUNCTIONs
         * irão pegar esse idioma de contexto, estas linhas não serão mais necessárias.
        */
        switch (idioma.ToLower().Substring(0, 2))
        {
            case "pt":
                {
                    comandoSQL = "SET CONTEXT_INFO 1";
                    break;
                }
            case "en":
                {
                    comandoSQL = "SET CONTEXT_INFO 2";
                    break;
                }
            case "es":
                {
                    comandoSQL = "SET CONTEXT_INFO 3";
                    break;
                }
            default:
                {
                    comandoSQL = "SET CONTEXT_INFO 1";
                    break;
                }
        }

        int registrosAfetados = 0;
        cDados.execSQL(comandoSQL, ref registrosAfetados);

        comandoSQL = string.Format(@"
            EXEC [dbo].[p_brk_Menu_hierarquia] {0}, {1}, {2}, {3};
        ", codigoEntidadeLogada, codigoUsuarioLogado, 1, "NULL");

        try
        {
            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                /* Obtém a identificação (prefixo) do menu Administração para que seja possível, a partir desse prefixo,
                 * remover do dataset os itens de Administração quando o menu a ser exibido não seja o menu Administração,
                 * ou manter no dataset os itens de menu de Administração quando o menu a ser exibido seja o menu de Administração.
                 * Há no C# uma forma mais inteligente e otimizada de fazer isso por meio de subqueries em datasets.
                 */
                string hierarquiaAdministracao = @"";
                string hierarquiaAdministracaoPonto = @"";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Iniciais"].ToString().TrimStart().TrimEnd() == "PR_ADM")
                    {
                        hierarquiaAdministracao = ds.Tables[0].Rows[i]["_Hierarquia"].ToString();
                        hierarquiaAdministracaoPonto = string.Concat(hierarquiaAdministracao, @".");
                        break;
                    }
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    hierarquia = ds.Tables[0].Rows[i]["_Hierarquia"].ToString();

                    if (administracao)
                    {
                        if (hierarquiaAdministracao == "")
                        {
                            ds.Tables[0].Rows[i].Delete();
                            ds.AcceptChanges();
                            i--;
                        }
                        else
                        {
                            bool inclui = false;
                            if (hierarquia == hierarquiaAdministracao)
                            {
                                inclui = true;
                            }
                            else if ((hierarquia.Length > 2) && (hierarquia.Substring(0, 2) == hierarquiaAdministracaoPonto))
                            {
                                inclui = true;
                            }
                            if (!inclui)
                            {
                                ds.Tables[0].Rows[i].Delete();
                                ds.AcceptChanges();
                                i--;
                            }
                        }
                    }
                    else
                    {
                        if (hierarquiaAdministracao != "")
                        {
                            bool inclui = true;
                            if (hierarquia == hierarquiaAdministracao)
                            {
                                inclui = false;
                            }
                            else if ((hierarquia.Length > 2) && (hierarquia.Substring(0, 2) == hierarquiaAdministracaoPonto))
                            {
                                inclui = false;
                            }
                            if (!inclui)
                            {
                                ds.Tables[0].Rows[i].Delete();
                                ds.AcceptChanges();
                                i--;
                            }
                        }
                    }
                }

                // Remove os menus de primeiro e segundo nível caso eles não tenham menus filho.
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    nivel = (int)ds.Tables[0].Rows[i]["_Nivel"];
                    possuiMenuInferior = ds.Tables[0].Rows[i]["PossuiMenuInferior"].ToString();
                    if (((nivel == 1) || (nivel == 2)) && (possuiMenuInferior == "N"))
                    {
                        ds.Tables[0].Rows[i].Delete();
                        ds.AcceptChanges();
                        i--;
                    }
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    nivel = (int)ds.Tables[0].Rows[i]["_Nivel"];
                    hierarquia = ds.Tables[0].Rows[i]["_Hierarquia"].ToString();
                    nome = ds.Tables[0].Rows[i]["NomeObjetoMenu"].ToString();
                    iniciais = ds.Tables[0].Rows[i]["Iniciais"].ToString().TrimStart().TrimEnd();
                    url = ds.Tables[0].Rows[i]["URLObjetoMenu"].ToString();
                    url += ((url.Contains("?")) ? "&" : "?") + "TITULO=" + Url(nome);
                    if (iniciais == "PNL_GE")
                    {
                        // Não habilitar: A rotina cDados.getURLTelaInicialUsuario() referencia uma função que não existe no banco de dados.
                        /*
                        DataSet ds2 = cDados.getURLTelaInicialUsuario(codigoEntidadeLogada.ToString(), codigoUsuarioLogado.ToString());
                        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                        {
                            url = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                        }
                        */
                    }
                    if (iniciais == "EMSOS") // Emissão de ordem de serviço.
                    {
                        // busca o código do workflow para criar uma nova Demanda.
                        int codigoFluxo, codigoWorkflow;
                        cDados.getCodigoWfAtualFluxoEmissaoOS(codigoEntidadeLogada, out codigoFluxo, out codigoWorkflow);
                        if ((codigoFluxo != 0) && (codigoWorkflow != 0))
                        {
                            url = "~/wfEngine.aspx?CF=" + codigoFluxo.ToString() + "&CW=" + codigoWorkflow.ToString();
                        }
                    }
                    url = url.Replace("@UrlApp", Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.AppRelativeCurrentExecutionFilePath.Replace("~/", ""))));
                    url = url.Replace("¥ENT", codigoEntidadeLogada.ToString());
                    icone = ds.Tables[0].Rows[i]["IconeObjetoMenu"].ToString();
                    cor = ds.Tables[0].Rows[i]["CorObjetoMenu"].ToString();
                    if (cor == "")
                    {
                        cor = "green";
                    }

                    switch (nivel)
                    {
                        case 1:
                            {
                                if (!administracao)
                                {
                                    novoHtml += string.Format(@"<li class=""dropdown-submenu brisk-menu-{0}"">", cor);
                                    novoHtml += string.Format(@"<a aria-expanded=""false"" aria-haspopup=""true"" class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"" role=""button"" title=""{0}"">{0}</a>", Html(nome));
                                    novoHtml += @"<div class=""dropdown-menu"" role=""menu"">";
                                    novoHtml += @"<div class=""card-columns"">";
                                }

                                break;
                            }
                        case 2:
                            {
                                novoHtml += @"<div class=""card"">";
                                novoHtml += string.Format(@"<div class=""card-header"" title=""{0}"">{0}</div>", Html(nome));
                                novoHtml += @"<div class=""card-body"">";
                                novoHtml += @"<ul>";

                                break;
                            }
                        case 3:
                            {
                                novoHtml += @"<li>";
                                novoHtml += string.Format(@"<a href=""{0}"" title=""{1}"">{1}</a>",
                                    Link(url),
                                    Html(nome));
                                novoHtml += @"</li>";

                                if (!administracao)
                                {
                                    if (menuAlterarSenha == "")
                                    {
                                        if (url.IndexOf(@"alterarSenhaViaOpMenu.aspx") >= 0)
                                        {
                                            menuAlterarSenha = string.Format(linkMenuAlterarSenha, Link(url));
                                        }
                                    }
                                    if (menuGerenciarPreferencias == "")
                                    {
                                        if (url.IndexOf(@"administracao/adm_ConfiguracaoPessoais.aspx") >= 0)
                                        {
                                            menuGerenciarPreferencias = string.Format(linkMenuGerenciarPreferencias, Link(url));
                                        }
                                    }
                                    if (menuOrganizarFavoritos == "")
                                    {
                                        if (url.IndexOf(@"espacoTrabalho/frameEspacoTrabalho_Favoritos.aspx") >= 0)
                                        {
                                            menuOrganizarFavoritos = string.Format(linkMenuOrganizarFavoritos, Link(url));
                                        }
                                    }
                                }

                                break;
                            }
                    }
                    inseridos++;

                    /* Todos os itens de menu de nível 1 têm filhos (nível 2), e todos os menus de nível 2 têm filhos (nível 3), pois isso foi garantido na rotina acima. */
                    if ((i + 1) < ds.Tables[0].Rows.Count)
                    {
                        proximoNivel = (int)ds.Tables[0].Rows[i + 1]["_Nivel"];
                        /* O menu de nível 3 pode descer para um menu de nível 2 ou de nível 1. Se for de nível 1, algumas tags a mais têm de ser fechadas */
                        if ((nivel == 3) && (proximoNivel < nivel))
                        {
                            novoHtml += @"</ul>";
                            novoHtml += @"</div>";
                            novoHtml += @"</div>";
                            if (proximoNivel == 1)
                            {
                                if (!administracao)
                                {
                                    novoHtml += @"</div>";
                                    novoHtml += @"</div>";
                                    novoHtml += @"</li>";
                                }
                            }
                        }
                    }
                    else
                    {
                        /* Aqui só existe a hipótese de ser um menu de nível 3, já que não há menus de níveis 1 ou 2 sem menus filho (nível 3). */
                        if (nivel == 3)
                        {
                            novoHtml += @"</ul>";
                            novoHtml += @"</div>";
                            novoHtml += @"</div>";
                            if (!administracao)
                            {
                                novoHtml += @"</div>";
                                novoHtml += @"</div>";
                                novoHtml += @"</li>";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string msgErro = ex.Message;
        }
        if (administracao)
        {
            novoMenuAdministracao = novoHtml;
            cDados.setInfoSistema("novoMenuAdministracao", novoMenuAdministracao);
        }
        else
        {
            novoMenu = novoHtml;
            cDados.setInfoSistema("novoMenu", novoMenu);
            cDados.setInfoSistema("menuAlterarSenha", menuAlterarSenha);
            cDados.setInfoSistema("menuGerenciarPreferencias", menuGerenciarPreferencias);
            cDados.setInfoSistema("menuOrganizarFavoritos", menuOrganizarFavoritos);
        }
    }

    /* Experimento Bootstrap */
    private void Favoritos()
    {
        favoritos = "";
        novoFavoritos = "";

        try
        {
            DataSet ds = cDados.getFavoritosUsuario(codigoEntidadeLogada, codigoUsuarioLogado);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    favoritos += string.Format(@"<a class=""dropdown-item item-fav"" href=""{0}"" title=""{1}""><i class=""fas fa-star ico-yellow""></i> {1}</a>", Link(row["URL"].ToString()), Html(row["NomeLinkFavorito"].ToString()));
                    novoFavoritos += string.Format(@"<a class=""dropdown-item"" href=""{0}"" title=""{1}""><i class=""fas fa-star gold""></i> {1}</a>", Link(row["URL"].ToString()), Html(row["NomeLinkFavorito"].ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            string msgErro = ex.Message;
        }
    }

    /* Experimento Bootstrap */
    private void Entidades()
    {
        entidades = "";

        try
        {
            if (cDados.getInfoSistema("Opcao").ToString() != "se")
            {
                DataSet ds = cDados.getEntidadesUsuario(codigoUsuarioLogado, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuarioLogado.ToString());
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    if (ds.Tables[0].Rows.Count > 1) // O usuário tem acesso a mais de uma entidade?
                    {
                        menuAlterarEntidade = string.Format(linkMenuAlterarEntidade
                            , cDados.getInfoSistema("SiglaUnidadeNegocio").ToString()
                            , cDados.getPathSistema() + "popUpSelecaoEntidade.aspx");
                    }
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        entidades += @"<li>";
                        entidades += string.Format(@"<a href=""{0}"" title=""{1}"">{1}</a>", Link("inicio.aspx?SUN=" + row["SiglaUnidadeNegocio"].ToString() + "&CE=" + row["CodigoUnidadeNegocio"].ToString()), Html(row["NomeUnidadeNegocio"].ToString()));
                        entidades += @"</li>";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string msgErro = ex.Message;
        }
    }

    /* Experimento Bootstrap */
    private void Rastro()
    {
        string caminho = Session["NomeArquivoNavegacao"] + "";
        if (Session["NomeArquivoNavegacao"] != null && caminho != "")
        {
            try
            {
                //rastro = String.Format(@"<li class=""breadcrumb-item""><a href=""{0}"">" + this.T("menu_inicio") + "</a></li>"
                //                    , Link("index.aspx"));
                XmlDocument doc = new XmlDocument();
                doc.Load(caminho);
                for (int i = 0; i < doc.ChildNodes[1].ChildNodes.Count; i++)
                {
                    XmlNode no = doc.SelectSingleNode(String.Format(@"/caminho/N[id={0}]", i));
                    if (no != null)
                    {
                        //Tenta Verificar o XML se Tem Caminho Repetido como por Exemplo Dois Níveis de BradCrumps onde um é a Página de preferência HOME
                        //e A outra é a mesma página de referência na contagem dos níveis, trada em momento de Impressão sem alterar as definições de níveis da página.
                        //Caso Contrário Segue o fluxo normal.
                        if (NomeRepetido == no.SelectSingleNode("./nome").InnerText.ToString())
                        {
                            NomeRepetido = no.SelectSingleNode("./nome").InnerText.ToString();
                        }
                        else
                        {
                            //Caso haja mais níveis de Link Percorre e insere nos links o nome e os RequestQueryString Passados pela Página no momento de execução.
                            if ((i + 1) < doc.ChildNodes[1].ChildNodes.Count)
                            {
                                if (i > 0)
                                {
                                    rastro += @"<span>&gt;</span>";
                                }
                                rastro += String.Format(@"<a href=""{0}{2}"" title=""{1}"">{1}</a>"
                                    , Link(no.SelectSingleNode("./url").InnerText.ToString())
                                    , Html(no.SelectSingleNode("./nome").InnerText.ToString())
                                    , String.IsNullOrEmpty(Html(no.SelectSingleNode("./parametros").InnerText.ToString())) ? "" : "?" + Html(no.SelectSingleNode("./parametros").InnerText.ToString()));
                            }
                            else
                            {
                                if (i > 0)
                                {
                                    rastro += @"<span>&gt;</span>";
                                }
                                rastro += String.Format(@"{0}"
                                , String.IsNullOrEmpty(Html(no.SelectSingleNode("./nome").InnerText.ToString())) ? "" : Html(no.SelectSingleNode("./nome").InnerText.ToString()));

                            }
                            NomeRepetido = no.SelectSingleNode("./nome").InnerText.ToString();
                        }
                    }
                }
            }
            catch
            {
                Response.RedirectLocation = cDados.getPathSistema() + "po_autentica.aspx";
            }
        }
        else
        {
            string nomeArquivo = "/ArquivosTemporarios/xml_Caminho" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioLogado + ".xml"; ;
            string urlDestino = "";
            string nomeTela = "";
            DataSet ds = cDados.getURLTelaInicialUsuario(codigoEntidadeLogada.ToString(), codigoUsuarioLogado.ToString());
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                //Pega o Nome da Tela Inicial
                var dsTelaUrlPadrao = cDados.GetNomeTelaUrlPadraoUsuarioIdioma(codigoUsuarioLogado.ToString(), codigoEntidadeLogada.ToString());

                urlDestino = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                nomeTela = dsTelaUrlPadrao.Tables[0].Rows[0]["NomeObjetoMenu"].ToString();
            }
            string xml = string.Format(@"<caminho>
	                                            <N>
		                                            <id>0</id>
                                                    <nivel>0</nivel>
		                                            <url>{0}</url>
		                                            <nome>{1}</nome>
		                                            <parametros></parametros>
	                                            </N>
                                            </caminho>"
                                            , urlDestino.Replace("~/", "")
                                            , nomeTela);
            Session["NomeArquivoNavegacao"] = Request.PhysicalApplicationPath + nomeArquivo;
            cDados.escreveXML(xml, nomeArquivo);
        }
    }

    /* Experimento Bootstrap */
    private void Sobre()
    {
        Version versao = Global.Version;
        DateTime dataVersao = versao.GetVerionDate();
        Version versaoDevExpress = typeof(ASPxGridView).Assembly.GetName(true).Version;

        sobre = "";
        sobre += string.Format(@"<p class=""copyright"">&copy; 2009-{0} CDIS Informática Ltda.</p>", DateTime.Today.Year);
        sobre += string.Format(@"<p class=""versao"">{0}: {1}</p>", this.T("vers_o"), versao.ToString());
        sobre += string.Format(@"<p class=""data"">{0}: {1}</p>", this.T("data"), dataVersao.ToString("g"));
        //sobre += @"<ul class=""footer-sobre"">";
        //sobre += string.Format(@"<li>Copyright © 2009-{0} CDIS Informática LTDA.</li>", DateTime.Now.Year);
        /*
        sobre += string.Format(@"<li>{1}: {0}</li>", versao.ToString(), Resources.traducao.vers_o);
        sobre += string.Format(@"<li>{1}: {0}</li>", versaoDevExpress.ToString(), Resources.traducao.vers_o_do_devexpress);
        sobre += string.Format(@"<li>{1}: {0}</li>", dataVersao.ToString("g"), "Data");
        DataSet ds = cDados.getParametrosSistema("nomeEmpresa");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            sobre += string.Format(@"<li>{1}: {0}</li>", ds.Tables[0].Rows[0]["nomeEmpresa"].ToString(), Resources.traducao.licenciado_para);
        }
        else
        {
            sobre += string.Format(@"<li>{1}: {0}</li>", "CDIS Informática LTDA", Resources.traducao.licenciado_para);
        }
        */
        //sobre += "</ul>";
        //sobre += string.Format(@"<a class=""more-information"" href=""#"" onclick=""mostraSobre('{0}');"">" + this.T("menu_mais_informacoes") + "</a>", Link("/sobre.aspx"));
    }

    /* Experimento Bootstrap */
    private void Usuario()
    {
        usuario = cDados.getInfoSistema("NomeUsuarioLogado").ToString();
        if (usuario.IndexOf(" ") != -1)
        {
            usuario = usuario.Substring(0, usuario.IndexOf(" "));
        }
        string siglaEntidade = cDados.getInfoSistema("SiglaUnidadeNegocio").ToString();
        if (siglaEntidade.Length > 0)
        {
            usuario += ", <strong>" + siglaEntidade + "</strong>";
        }
    }

    /* Experimento Bootstrap */
    private void Carteira()
    {
        /* Esta rotina foi copiada de 3 funções sobre carteira, e precisa ser revisada, pois aparenta ser redundante. */
        /* Não encontrei utilidade no objeto imgStatusReport. Verificar a relevância no projeto. */
        DataSet ds = cDados.getParametrosSistema("labelCarteiras", "labelCarteirasPlural");
        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            labelCarteira = ds.Tables[0].Rows[0]["labelCarteiras"].ToString();
        }

        int codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
        bool possuiStatusReport = cDados.verificaCarteiraPossuiStatusReport(codigoCarteira);
        imgStatusReport.Enabled = possuiStatusReport;
        if (possuiStatusReport == true)
        {
            imgStatusReport.ImageUrl = "~/imagens/botoes/btnPDF.png";
            imgStatusReport.ToolTip = "Imprimir Último Boletim de Status Publicado";
        }
        else
        {
            imgStatusReport.ImageUrl = "~/imagens/botoes/btnPDFDes.png";
            imgStatusReport.ToolTip = "Nenhum Boletim de Status Publicado";
        }

        string relativePath = cDados.getPathSistema() + string.Format(@"_Projetos/DadosProjeto/HistoricoStatusReport.aspx?idObjeto={0}&tp=CP", codigoCarteira);
        imgHistoricoStatusReport.JSProperties["cp_RelativePath"] = relativePath;

        cDados.defineConfiguracoesComboCarteiras(ddlVisaoInicial, IsPostBack, codigoUsuarioLogado, codigoEntidadeLogada, false);

        bool exibirCarteira = cDados.mostraComboCarteirasDeProjetosTela(Request.AppRelativeCurrentExecutionFilePath.ToString());
        //lblCarteira.ClientVisible = exibirCarteira;
        ddlVisaoInicial.ClientVisible = exibirCarteira;
        divDDLVisaoInicial.Visible = exibirCarteira;
        if (exibirCarteira)
        {
            int.TryParse(ddlVisaoInicial.Value.ToString(), out codigoCarteira);
            bool possuiBoletimStatus = cDados.indicaCarteiraPossuiBoletimStatus(codigoCarteira);
            imgStatusReport.Visible = possuiBoletimStatus;
            imgHistoricoStatusReport.ClientVisible = possuiBoletimStatus;
        }
        else
        {
            imgStatusReport.Visible = false;
            imgHistoricoStatusReport.ClientVisible = false;
        }
    }

    /* Experimento Bootstrap */
    private void AlertasOld()
    {
        mensagens = "";
        notificacoes = "";
        tarefas = "";

        string texto;

        DataSet ds;
        DataTable dt;

        float total, atrasados;

        // an_003.aspx.cs

        ds = cDados.getAtualizacoesPainelAnalista(codigoEntidadeLogada, codigoUsuarioLogado, "", 'L', "AND TerminoRealInformado IS NULL");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            dt = ds.Tables[0];
            float tarefasTotal, tarefasAtrasadas, indicadoresTotal, indicadoresAtrasados, contratosTotal, contratosAtrasados, toDoListTotal, toDoListAtrasadas;
            tarefasTotal = float.Parse(dt.Rows[0]["Total"].ToString());
            tarefasAtrasadas = float.Parse(dt.Rows[0]["Atrasados"].ToString());
            indicadoresTotal = float.Parse(dt.Rows[1]["Total"].ToString());
            indicadoresAtrasados = float.Parse(dt.Rows[1]["Atrasados"].ToString());
            contratosTotal = float.Parse(dt.Rows[2]["Total"].ToString());
            contratosAtrasados = float.Parse(dt.Rows[2]["Atrasados"].ToString());
            toDoListTotal = float.Parse(dt.Rows[3]["Total"].ToString());
            toDoListAtrasadas = float.Parse(dt.Rows[3]["Atrasados"].ToString());

            // Tarefas (ToDoList)

            /*
            texto = "";
            total = tarefasTotal;
            atrasados = tarefasAtrasadas;
            ds = cDados.getParametrosSistema(codigoEntidadeLogada, "ExibeTarefas_an003");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                if (ds.Tables[0].Rows[0]["ExibeTarefas_an003"].ToString() == "S")
                {
                    DateTime data = DateTime.Now.AddDays(10);
                    total += toDoListTotal;
                    atrasados += toDoListAtrasadas;
                    if (total == 0)
                    {
                        //tarefas += string.Format(@"<li class=""dropdown-item item-icons"">Você não tem tarefas.</li>", data);
                    }
                    else
                    {
                        existemTarefas = true;
                        switch ((int)total)
                        {
                            case 1:
                                tarefas += string.Format(@"<li class=""dropdown-item item-icons"">Você tem <a href='#' data-menu-bootstrap-total-tarefas-geral=""1"" onclick='abreTarefas003(""N"");'>1</a> <a class='SF'>tarefa não concluída", data);
                                break;
                            default:
                                tarefas += string.Format(@"<li class=""dropdown-item item-icons"">Você tem <a href='#' data-menu-bootstrap-total-tarefas-geral=""{0}"" onclick='abreTarefas003(""N"");'>{0:n0}</a> <a class='SF'>tarefas não concluídas", total, data);
                                break;
                        }
                        if (atrasados == 0)
                        {
                            tarefas += ".</a>";
                        }
                        else if (atrasados == 1)
                        {
                            tarefas += string.Format(@"<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreTarefas003(""A"");'>1</a> <a class='SF'>atrasada. </a>");
                        }
                        else if (atrasados > 0)
                        {
                            tarefas += string.Format(@"<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreTarefas003(""A"");'>{0:n0}</a> <a class='SF'>atrasadas. </a>", atrasados);
                        }
                        tarefas += "</li>";
                    }
                }
                else
                {
                    tarefas += string.Format(@"<li class=""dropdown-item item-icons"">Você não tem tarefas a serem acessadas/atualizadas por meio deste painel.</li>");
                }
            }
            */

            texto = "";
            total = tarefasTotal;
            atrasados = tarefasAtrasadas;
            ds = cDados.getParametrosSistema(codigoEntidadeLogada, "ExibeTarefas_an003");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                if (ds.Tables[0].Rows[0]["ExibeTarefas_an003"].ToString() == "S")
                {
                    DateTime data = DateTime.Now.AddDays(10);
                    total += toDoListTotal;
                    atrasados += toDoListAtrasadas;
                    if (total == 0)
                    {
                        //menuPrincipalExistemTarefas.Visible = false;
                        //texto = this.T("menu_tarefas_nao_existem_tarefas");
                    }
                    else
                    {
                        //menuPrincipalExistemTarefas.Visible = true;
                        existemTarefas = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = this.T("menu_tarefas_voce_tem_n_tarefa");
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = this.T("menu_tarefas_voce_tem_n_tarefa_sendo_n_atrasado");
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = this.T("menu_tarefas_voce_tem_n_tarefa_sendo_n_atrasados");
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = this.T("menu_tarefas_voce_tem_n_tarefas");
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = this.T("menu_tarefas_voce_tem_n_tarefas_sendo_n_atrasado");
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = this.T("menu_tarefas_voce_tem_n_tarefas_sendo_n_atrasados");
                        }
                    }
                }
                else
                {
                    texto = this.T("menu_tarefas_acesso_negado");
                }
                if (texto != "")
                {
                    tarefas += string.Format(@"<li>{0}</li>", string.Format(texto,
                        string.Format(total.ToString(), "n0"),
                        string.Format(atrasados.ToString(), "n0"))
                    );
                }
            }

            // Indicadores

            /*
            total = indicadoresTotal;
            atrasados = indicadoresAtrasados;
            if ((int)total == 0)
            {
                //notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Não há pendências de atualização de indicadores.</li>");
            }
            else
            {
                existemNotificacoes = true;
                switch ((int)total)
                {
                    case 1:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons""> Há <a href='#' onclick='abreIndicadores003(""N"");'>1</a> <a class='SF'>pendência de atualização de indicador</a>");
                        break;
                    default:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons""> Há <a href='#' onclick='abreIndicadores003(""N"");'>{0:n0}</a> <a class='SF'>pendências de atualização de indicadores</a>", total);
                        break;
                }
                if (atrasados == 1)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo que </a><a href='#' style='Color:#5E585C' onclick='abreIndicadores003(""A"");'>1</a> <a class='SF'>está atrasado</a>");
                }
                else if (atrasados > 0)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo que </a><a href='#' style='Color:#5E585C' onclick='abreIndicadores003(""A"");'>{0:n0}</a> <a class='SF'>estão atrasados</a>", atrasados);
                }
                notificacoes += ".</li>";
            }
            */

            texto = "";
            total = indicadoresTotal;
            atrasados = indicadoresAtrasados;
            if ((int)total == 0)
            {
                existemNotificacoes = false;
                //texto = this.T("menu_notificacoes_nao_existem_indicadores");
            }
            else
            {
                existemNotificacoes = true;
                if ((total == 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_indicador");
                }
                else if ((total == 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_indicador_sendo_n_atrasado");
                }
                else if ((total == 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_indicador_sendo_n_atrasados");
                }
                else if ((total > 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_indicadores");
                }
                else if ((total > 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_indicadores_sendo_n_atrasado");
                }
                else if ((total > 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_indicadores_sendo_n_atrasados");
                }
            }
            if (texto != "")
            {
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(total.ToString(), "n0"),
                    string.Format(atrasados.ToString(), "n0"))
                );
            }

            // Parcelas de Contrato

            /*
            total = contratosTotal;
            atrasados = contratosAtrasados;
            double diasParcelas = 0;
            ds = cDados.getParametrosSistema(codigoEntidadeLogada, "diasParcelasVencendo");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["diasParcelasVencendo"].ToString() != "")
            {
                diasParcelas = double.Parse(ds.Tables[0].Rows[0]["diasParcelasVencendo"].ToString());
            }
            DateTime dataVencimento = DateTime.Now.AddDays(diasParcelas);
            if ((int)total == 0)
            {
                //notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Não há parcelas de contrato vencendo até {0:dd/MM/yyyy}.</li>", dataVencimento);
            }
            else
            {
                existemNotificacoes = true;
                switch ((int)total)
                {
                    case 1:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Há <a href='#' onclick='abreContratos003(""N"");'>1</a> <a class='SF'>parcela de contrato vencendo até {0:dd/MM/yyyy}</a>", dataVencimento);
                        break;
                    default:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Há <a href='#' onclick='abreContratos003(""N"");'>{0:n0}</a> <a class='SF'>parcelas de contrato vencendo até {1:dd/MM/yyyy}</a>", total, dataVencimento);
                        break;
                }
                if (atrasados == 1)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreContratos003(""A"");'>1</a> <a class='SF'>atrasada</a>");
                }
                else if (atrasados > 0)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreContratos003(""A"");'>{0:n0}</a> <a class='SF'>atrasadas</a>", atrasados);
                }
                notificacoes += ".</li>";
            }
            */

            texto = "";
            total = contratosTotal;
            atrasados = contratosAtrasados;
            double diasParcelas = 0;
            ds = cDados.getParametrosSistema(codigoEntidadeLogada, "diasParcelasVencendo");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["diasParcelasVencendo"].ToString() != "")
            {
                diasParcelas = double.Parse(ds.Tables[0].Rows[0]["diasParcelasVencendo"].ToString());
            }
            DateTime dataVencimento = DateTime.Now.AddDays(diasParcelas);
            if ((int)total == 0)
            {
                //texto = this.T("menu_notificacoes_nao_existem_parcelas_contratos");
            }
            else
            {
                existemNotificacoes = true;
                if ((total == 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_parcela_contrato");
                }
                else if ((total == 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_parcela_contrato_sendo_n_atrasado");
                }
                else if ((total == 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_parcela_contrato_sendo_n_atrasados");
                }
                else if ((total > 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_parcelas_contratos");
                }
                else if ((total > 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_parcelas_contratos_sendo_n_atrasado");
                }
                else if ((total > 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_parcelas_contratos_sendo_n_atrasados");
                }
            }
            if (texto != "")
            {
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(dataVencimento.ToString(), this.T("geral_formato_data_csharp")),
                    string.Format(total.ToString(), "n0"),
                    string.Format(atrasados.ToString(), "n0"))
                );
            }
        }

        // an_004.aspx.cs

        string formatoTelaPendenciaWorkflows = "";

        ds = cDados.getParametrosSistema(codigoEntidadeLogada, "formatoTelaPendenciaWorkflows");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["formatoTelaPendenciaWorkflows"].ToString() == "SGDA")
        {
            formatoTelaPendenciaWorkflows = "SGDA";
        }
        ds = cDados.getAprovacoesPainelAnalista(codigoEntidadeLogada, codigoUsuarioLogado, "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            dt = ds.Tables[0];
            float tarefasRecursosTotal, workflowTotal, workflowAtrasados;
            tarefasRecursosTotal = float.Parse(dt.Rows[0]["Total"].ToString());
            workflowTotal = float.Parse(dt.Rows[1]["Total"].ToString());
            workflowAtrasados = float.Parse(dt.Rows[1]["Atrasados"].ToString());

            // Tarefas de Recursos

            /*
            total = tarefasRecursosTotal;
            if ((int)total == 0)
            {
                //tarefas += string.Format(@"<li class=""dropdown-item item-icons"">Não há tarefas de seus recursos para aprovação.</li>");
            }
            else
            {
                existemTarefas = true;
                switch ((int)total)
                {
                    case 1:
                        tarefas += string.Format(@"<li class=""dropdown-item item-icons"">Seus recursos enviaram <a href='#' onclick='abreTarefas004();'>1</a> <a class='SF'>tarefa para aprovação</a>.</li>");
                        break;
                    default:
                        tarefas += string.Format(@"<li class=""dropdown-item item-icons"">Seus recursos enviaram <a href='#' onclick='abreTarefas004();'>{0:n0}</a> <a class='SF'>tarefas para aprovação</a>.</li>", total);
                        break;
                }
            }
            */

            texto = "";
            total = tarefasRecursosTotal;
            if ((int)total == 0)
            {
                //texto = this.T("menu_tarefas_nao_existem_tarefas_recursos");
            }
            else
            {
                existemTarefas = true;
                if (total == 1)
                {
                    texto = this.T("menu_tarefas_voce_tem_n_tarefa_recurso");
                }
                else if (total > 1)
                {
                    texto = this.T("menu_tarefas_voce_tem_n_tarefas_recursos");
                }
            }
            if (texto != "")
            {
                tarefas += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(total.ToString(), "n0"))
                );
            }

            // Pendências de Workflows

            /*
            total = workflowTotal;
            atrasados = workflowAtrasados;
            if ((int)total == 0)
            {
                //notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você não tem pendências de workflow.</li>");
            }
            else
            {
                existemNotificacoes = true;
                switch ((int)total)
                {
                    case 1:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você tem <a href='#' onclick='abreWorkflow004(""N"",""{0}"", this);'>1</a> <a class='SF'>pendência de workflow</a>", formatoTelaPendenciaWorkflows);
                        break;
                    default:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você tem <a href='#' onclick='abreWorkflow004(""N"",""{1}"", this);'>{0:n0}</a> <a class='SF'>pendências de workflow</a>", total, formatoTelaPendenciaWorkflows);
                        break;
                }
                if (atrasados == 1)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo que </a><a href='#' style='Color:#5E585C'  onclick='abreWorkflow004(""A"",""{0}"", this);'>1</a> <a class='SF'>está com prazo vencido</a>", formatoTelaPendenciaWorkflows);
                }
                else if (atrasados > 0)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo que </a><a href='#' style='Color:#5E585C'  onclick='abreWorkflow004(""A"",""{1}"", this);'>{0:n0}</a> <a class='SF'>estão com prazo vencido</a>", atrasados, formatoTelaPendenciaWorkflows);
                }
                notificacoes += ".</li>";
            }
            */

            texto = "";
            total = workflowTotal;
            atrasados = workflowAtrasados;
            if ((int)total == 0)
            {
                //texto = this.T("menu_notificacoes_nao_existem_pendencias_workflow");
            }
            else
            {
                existemNotificacoes = true;
                if ((total == 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_pendencia_workflow");
                }
                else if ((total == 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_pendencia_workflow_sendo_n_atrasado");
                }
                else if ((total == 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_pendencia_workflow_sendo_n_atrasados");
                }
                else if ((total > 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_pendencias_workflow");
                }
                else if ((total > 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_pendencias_workflow_sendo_n_atrasado");
                }
                else if ((total > 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_pendencias_workflow_sendo_n_atrasados");
                }
            }
            if (texto != "")
            {
                /*
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(@"<a href='#' onclick='abreWorkflow004(""N"",""{0}"", this);'>", formatoTelaPendenciaWorkflows) + string.Format(total.ToString(), "n0") + "</a>",
                    string.Format(@"<a href='#' onclick='abreWorkflow004(""A"",""{0}"", this);'>", formatoTelaPendenciaWorkflows) + string.Format(atrasados.ToString(), "n0") + "</a>"));
                */
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(total.ToString(), "n0"),
                    string.Format(atrasados.ToString(), "n0"),
                    formatoTelaPendenciaWorkflows)
                );
            }

        }

        // an_005.aspx.cs

        int quantidadeDiasAlertaContratos = 60;

        ds = cDados.getParametrosSistema("QuantidadeDiasAlertaContratosVencimento");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["QuantidadeDiasAlertaContratosVencimento"] + "" != "")
        {
            quantidadeDiasAlertaContratos = int.Parse(ds.Tables[0].Rows[0]["QuantidadeDiasAlertaContratosVencimento"] + "");
        }
        ds = cDados.getGestaoPainelAnalista(codigoEntidadeLogada, codigoUsuarioLogado, quantidadeDiasAlertaContratos, "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            dt = ds.Tables[0];
            float riscosTotal, riscosAtrasados, issuesTotal, issuesAtrasadas, contratosTotal, contratosVencidos;
            riscosTotal = float.Parse(dt.Rows[0]["Total"].ToString());
            riscosAtrasados = float.Parse(dt.Rows[0]["Atrasados"].ToString());
            issuesTotal = float.Parse(dt.Rows[1]["Total"].ToString());
            issuesAtrasadas = float.Parse(dt.Rows[1]["Atrasados"].ToString());
            contratosTotal = float.Parse(dt.Rows[2]["Total"].ToString());
            contratosVencidos = float.Parse(dt.Rows[2]["Atrasados"].ToString());

            // Riscos

            /*
            total = riscosTotal;
            atrasados = riscosAtrasados;
            if ((int)total == 0)
            {
                //notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você não possui riscos ativos de sua responsabilidade.</li>");
            }
            else
            {
                existemNotificacoes = true;
                switch ((int)total)
                {
                    case 1:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você é responsável por <a href='#' onclick='abreRiscos005(""N"");'>1</a> <a class='SF'>risco ativo</a>");
                        break;
                    default:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você é responsável por <a href='#' onclick='abreRiscos005(""N"");'>{0:n0}</a> <a class='SF'>riscos ativos</a>", total);
                        break;
                }
                if (atrasados == 1)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreRiscos005(""A"");'>1</a> <a class='SF'>crítico</a>");
                }
                else if (atrasados > 0)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreRiscos005(""A"");'>{0:n0}</a> <a class='SF'>críticos</a>", atrasados);
                }
                notificacoes += ".</li>";
            }
            */

            texto = "";
            total = riscosTotal;
            atrasados = riscosAtrasados;
            if ((int)total == 0)
            {
                //texto = this.T("menu_notificacoes_nao_existem_riscos");
            }
            else
            {
                existemNotificacoes = true;
                if ((total == 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_risco");
                }
                else if ((total == 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_risco_sendo_n_atrasado");
                }
                else if ((total == 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_risco_sendo_n_atrasados");
                }
                else if ((total > 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_riscos");
                }
                else if ((total > 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_riscos_sendo_n_atrasado");
                }
                else if ((total > 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_riscos_sendo_n_atrasados");
                }
            }
            if (texto != "")
            {
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(total.ToString(), "n0"),
                    string.Format(atrasados.ToString(), "n0"))
                );
            }

            // Questões

            /*
            total = issuesTotal;
            atrasados = issuesAtrasadas;
            string definicaoQuestao = "Questão", definicaoQuestaoPlural = "questões ativas", definicaoQuestaoSingular = "questão ativa";
            string labelCriticaSingular = "crítica", labelCriticaPlural = "críticas";
            DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");
            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
            {
                string genero = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";
                definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
                definicaoQuestaoPlural = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
                definicaoQuestaoSingular = string.Format(@"{0} ativ{1}", definicaoQuestao, genero == "M" ? "o" : "a");
                labelCriticaSingular = string.Format(@"crític{0}", genero == "M" ? "o" : "a");
                labelCriticaPlural = string.Format(@"crític{0}", genero == "M" ? "os" : "as");
            }
            if ((int)total == 0)
            {
                //notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você não possui {0} de sua responsabilidade.</li>", definicaoQuestaoPlural.ToLower());
            }
            else
            {
                existemNotificacoes = true;
                switch ((int)total)
                {
                    case 1:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você é responsável por <a href='#' onclick='abreIssues005(""N"");'>1</a> <a class='SF'>{0}</a>", definicaoQuestaoSingular.ToLower());
                        break;
                    default:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Você é responsável por <a href='#' onclick='abreIssues005(""N"");'>{0:n0}</a> <a class='SF'>{1}</a>", total, definicaoQuestaoPlural.ToLower());
                        break;
                }
                if (atrasados == 1)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreIssues005(""A"");'>1</a> <a class='SF'>{0}</a>", labelCriticaSingular.ToLower());
                }
                else if (atrasados > 0)
                {
                    notificacoes += string.Format(@"<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreIssues005(""A"");'>{0:n0}</a> <a class='SF'>{1}</a>", atrasados, labelCriticaPlural.ToLower());
                }
                notificacoes += ".</li>";
            }
            */

            texto = "";
            total = issuesTotal;
            atrasados = issuesAtrasadas;
            string definicaoQuestao = "Questão", definicaoQuestaoPlural = "questões ativas", definicaoQuestaoSingular = "questão ativa";
            string labelCriticaSingular = "crítica", labelCriticaPlural = "críticas";
            DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");
            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
            {
                string genero = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";
                definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
                definicaoQuestaoPlural = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
                definicaoQuestaoSingular = string.Format(@"{0} ativ{1}", definicaoQuestao, genero == "M" ? "o" : "a");
                labelCriticaSingular = string.Format(@"crític{0}", genero == "M" ? "o" : "a");
                labelCriticaPlural = string.Format(@"crític{0}", genero == "M" ? "os" : "as");
            }
            if ((int)total == 0)
            {
                //texto = this.T("menu_notificacoes_nao_existem_questoes");
            }
            else
            {
                existemNotificacoes = true;
                if ((total == 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_questao");
                }
                else if ((total == 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_questao_sendo_n_atrasado");
                }
                else if ((total == 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_questao_sendo_n_atrasados");
                }
                else if ((total > 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_questoes");
                }
                else if ((total > 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_questoes_sendo_n_atrasado");
                }
                else if ((total > 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_questoes_sendo_n_atrasados");
                }
            }
            if (texto != "")
            {
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(total.ToString(), "n0"),
                    string.Format(atrasados.ToString(), "n0"))
                );
            }

            // Contratos

            /*
            total = contratosTotal;
            atrasados = contratosVencidos;
            string varLigacao = "e";
            string varUsaContratoEstendido = "N";
            ds = cDados.getParametrosSistema(codigoEntidadeLogada, "UtilizaContratosExtendidos");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["UtilizaContratosExtendidos"].ToString() == "S")
            {
                varUsaContratoEstendido = "S";
            }
            if ((int)total == 0)
            {
                //notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Não há contratos de sua responsabilidade com vencimento nos próximos {0} dias.</li>", quantidadeDiasAlertaContratos);
                varLigacao = ", mas"; // Muito estranho isso estar aqui, pois não será usado.
            }
            else
            {
                existemNotificacoes = true;
                switch ((int)total)
                {
                    case 1:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Há <a href='#' onclick='abreContratos005(""N"", {0}, ""{1}"");'>1</a> <a class='SF'>contrato de sua responsabilidade com vencimento nos próximos {0} dias</a>", quantidadeDiasAlertaContratos, varUsaContratoEstendido);
                        break;
                    default:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-icons"">Há <a href='#' onclick='abreContratos005(""N"", {1}, ""{2}"");'>{0:n0}</a> <a class='SF'>contratos de sua responsabilidade com vencimento nos próximos {1} dias</a>", total, quantidadeDiasAlertaContratos, varUsaContratoEstendido);
                        break;
                }
                if (atrasados == 1)
                {
                    notificacoes += string.Format(@"<a class='SF'> {0} </a><a href='#' style='Color:#5E585C' onclick='abreContratos005(""A"", 0, ""{1}"");'>1</a> <a class='SF'> de sua responsabilidade está vencido</a>", varLigacao, varUsaContratoEstendido);
                }
                else if (atrasados > 0)
                {
                    notificacoes += string.Format(@"<a class='SF'> {1} </a><a href='#' style='Color:red' onclick='abreContratos005(""A"", 0, ""{2}"");'>{0:n0}</a> <a class='SF'> de sua responsabilidade estão vencidos</a>", atrasados, varLigacao, varUsaContratoEstendido);
                }
                notificacoes += ".</li>";
            }
            */

            texto = "";
            total = contratosTotal;
            atrasados = contratosVencidos;
            string varLigacao = "e";
            string varUsaContratoEstendido = "N";
            ds = cDados.getParametrosSistema(codigoEntidadeLogada, "UtilizaContratosExtendidos");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["UtilizaContratosExtendidos"].ToString() == "S")
            {
                varUsaContratoEstendido = "S";
            }
            if ((int)total == 0)
            {
                //texto = this.T("menu_notificacoes_nao_existem_contratos");
            }
            else
            {
                existemNotificacoes = true;
                if ((total == 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_contrato");
                }
                else if ((total == 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_contrato_sendo_n_atrasado");
                }
                else if ((total == 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_contrato_sendo_n_atrasados");
                }
                else if ((total > 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_contratos");
                }
                else if ((total > 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_contratos_sendo_n_atrasado");
                }
                else if ((total > 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_contratos_sendo_n_atrasados");
                }
            }
            if (texto != "")
            {
                /*
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    quantidadeDiasAlertaContratos,
                    string.Format(@"<a href='#' onclick='abreContratos005(""N"", {0}, ""{1}"");'>", quantidadeDiasAlertaContratos, varUsaContratoEstendido) + string.Format(total.ToString(), "n0") + "</a>",
                    string.Format(@"<a href='#' onclick='abreContratos005(""A"", 0, ""{0}"");'>", varUsaContratoEstendido) + string.Format(atrasados.ToString(), "n0") + "</a>")
                );
                */
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    quantidadeDiasAlertaContratos,
                    string.Format(total.ToString(), "n0"),
                    string.Format(atrasados.ToString(), "n0"),
                    varUsaContratoEstendido)
                );
            }
        }

        // an_006.aspx.cs

        ds = cDados.getComunicacaoPainelAnalista(codigoEntidadeLogada, codigoUsuarioLogado, "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            dt = ds.Tables[0];
            float compromissosTotal, compromissosProximosDias, mensagensTotal, mensagensAtrasadas;
            compromissosTotal = float.Parse(dt.Rows[0]["Total"].ToString());
            compromissosProximosDias = float.Parse(dt.Rows[0]["Atrasados"].ToString());
            mensagensTotal = float.Parse(dt.Rows[1]["Total"].ToString());
            mensagensAtrasadas = float.Parse(dt.Rows[1]["Atrasados"].ToString());

            // Compromissos

            /*
            total = compromissosTotal;
            atrasados = compromissosProximosDias;
            if ((int)total == 0)
            {
                //notificacoes += string.Format(@"<li class=""dropdown-item item-fav"">Não há compromissos futuros na agenda.</li>");
            }
            else
            {
                existemNotificacoes = true;
                switch ((int)total)
                {
                    case 1:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-fav"">Há <a href='#' onclick='abreCompromissos006(""N"")'>1</a> <a class='SF'>compromisso futuro na agenda</a>");
                        break;
                    default:
                        notificacoes += string.Format(@"<li class=""dropdown-item item-fav"">Há <a href='#' onclick='abreCompromissos006(""N"");'>{0:n0}</a> <a class='SF'>compromissos futuros na agenda</a>", total);
                        break;
                }
                if (atrasados > 0)
                {
                    notificacoes += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:#5E585C' onclick='abreCompromissos006(\"A\")'>{0:n0}</a> <a class='SF'>para os próximos 10 dias</a>", atrasados);
                }
                notificacoes += ".</li>";
            }
            */

            texto = "";
            total = compromissosTotal;
            atrasados = compromissosProximosDias;
            if ((int)total == 0)
            {
                //texto = this.T("menu_notificacoes_nao_existem_compromissos");
            }
            else
            {
                existemNotificacoes = true;
                if ((total == 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_compromisso");
                }
                else if ((total == 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_compromisso_sendo_n_atrasado");
                }
                else if ((total == 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_compromisso_sendo_n_atrasados");
                }
                else if ((total > 1) && (atrasados == 0))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_compromissos");
                }
                else if ((total > 1) && (atrasados == 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_compromissos_sendo_n_atrasado");
                }
                else if ((total > 1) && (atrasados > 1))
                {
                    texto = this.T("menu_notificacoes_voce_tem_n_compromissos_sendo_n_atrasados");
                }
            }
            if (texto != "")
            {
                /*
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    @"<a href='#' onclick='abreCompromissos006(""N"")'>" + string.Format(total.ToString(), "n0") + "</a>",
                    @"<a href='#' onclick='abreCompromissos006(""A"")'>" + string.Format(atrasados.ToString(), "n0") + "</a>")
                );
                */
                notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(total.ToString(), "n0"),
                    string.Format(atrasados.ToString(), "n0"),
                    10)
                );
            }

            // Mensagens

            /*
            total = mensagensTotal;
            atrasados = mensagensAtrasadas;
            callbackPontoVermelhoExistemMensagens.ClientVisible = false;
            if ((int)total == 0)
            {
                //mensagens += string.Format(@"<li class=""dropdown-item item-fav"">Você não tem mensagens.</li>");
            }
            else
            {
                existemMensagens = true;
                callbackPontoVermelhoExistemMensagens.ClientVisible = true;
                switch ((int)total)
                {
                    case 1:
                        mensagens += string.Format(@"<li class=""dropdown-item item-icons"">Você tem <a href='#' onclick='abreMensagensNovas006(""N"");'>1</a> <a class='SF'>mensagem não lida</a>");
                        break;
                    default:
                        mensagens += string.Format(@"<li class=""dropdown-item item-icons"">Você tem <a href='#' onclick='abreMensagensNovas006(""N"");'>{0:n0}</a> <a class='SF'>mensagens não lidas</a>", total);
                        break;
                }
                mensagens += ".</li>";
            }
            */

            texto = "";
            total = mensagensTotal;
            atrasados = mensagensAtrasadas;
            //callbackPontoVermelhoExistemMensagens.ClientVisible = false;
            if ((int)total == 0)
            {
                //menuPrincipalExistemMensagens.Style.Add("display", "none");
                //menuPrincipalExistemMensagens.Visible = false;


                //texto = this.T("menu_mensagens_nao_existem_mensagens");
            }
            else
            {
                //menuPrincipalExistemMensagens.Style.Add("display", "");
                //menuPrincipalExistemMensagens.Visible = true;
                existemMensagens = true;
                if ((total == 1) && (atrasados == 0))
                {
                    texto = this.T("menu_mensagens_voce_tem_n_mensagem");
                }
                else if ((total == 1) && (atrasados == 1))
                {
                    texto = this.T("menu_mensagens_voce_tem_n_mensagem_sendo_n_atrasado");
                }
                else if ((total == 1) && (atrasados > 1))
                {
                    texto = this.T("menu_mensagens_voce_tem_n_mensagem_sendo_n_atrasados");
                }
                else if ((total > 1) && (atrasados == 0))
                {
                    texto = this.T("menu_mensagens_voce_tem_n_mensagens");
                }
                else if ((total > 1) && (atrasados == 1))
                {
                    texto = this.T("menu_mensagens_voce_tem_n_mensagens_sendo_n_atrasado");
                }
                else if ((total > 1) && (atrasados > 1))
                {
                    texto = this.T("menu_mensagens_voce_tem_n_mensagens_sendo_n_atrasados");
                }
            }
            if (texto != "")
            {
                mensagens += string.Format(@"<li>{0}</li>", string.Format(texto,
                    string.Format(total.ToString(), "n0"))
                );
            }


        }

        //menuPrincipalExistemNotificacoes.Visible = existemNotificacoes;

    }

    #region Definição da Logo

    public void defineLogo()
    {
        //Trouce o dados do banco
        DataSet ds = cDados.getLogoEntidade(codigoEntidadeLogada, "");

        //imgLogo.BinaryStorageMode = DevExpress.Web.BinaryStorageMode.Session;

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows[0]["LogoUnidadeNegocio"] != null && dt.Rows[0]["LogoUnidadeNegocio"].ToString() != "")
            {
                //imgLogo.ContentBytes = (byte[])dt.Rows[0]["LogoUnidadeNegocio"];
            }
        }
    }

    public void defineLogoUnidade()
    {
        if (utilizaLogoLadoDireito)
        {
            //Trouce o dados do banco
            DataSet ds = cDados.getLogoUnidade(codigoUsuarioLogado, codigoEntidadeLogada);

            //imgLogoUnidade.BinaryStorageMode = DevExpress.Web.BinaryStorageMode.Session;

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0]["LogoUnidadeNegocio"] != null && dt.Rows[0]["LogoUnidadeNegocio"].ToString() != "")
                {
                    //imgLogoUnidade.ContentBytes = (byte[])dt.Rows[0]["LogoUnidadeNegocio"];
                }
            }
        }
        else
        {
            //imgLogoUnidade.EmptyImage.Url = "";
            //imgLogoUnidade.ClientVisible = false;
        }
    }

    #endregion

    #region Criação de Menus

    private void montaMenu()
    {
        limpaComponentes();

        DataSet ds = cDados.getParametrosSistema("labelQuestoes", "labelToDoList");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            labelQuestoes = ds.Tables[0].Rows[0]["labelQuestoes"].ToString();
            labelToDoList = ds.Tables[0].Rows[0]["labelToDoList"].ToString();
        }

        DataSet dsMenu = cDados.getMenuAcessoUsuario(codigoEntidadeLogada, codigoUsuarioLogado, "");

        carregaMenuPrincipal(dsMenu);

        espacoTrabalho_Nav001.Groups.Clear();
        espacoTrabalho_Nav002.Groups.Clear();
        espacoTrabalho_Nav003.Groups.Clear();
        estrategia_Nav001.Groups.Clear();
        estrategia_Nav002.Groups.Clear();
        estrategia_Nav003.Groups.Clear();
        projetos_Nav001.Groups.Clear();
        projetos_Nav002.Groups.Clear();
        projetos_Nav003.Groups.Clear();
        demandas_Nav001.Groups.Clear();
        demandas_Nav002.Groups.Clear();
        demandas_Nav003.Groups.Clear();
        administracao_Nav001.Groups.Clear();
        administracao_Nav002.Groups.Clear();
        administracao_Nav003.Groups.Clear();
        financeiro_Nav001.Groups.Clear();
        financeiro_Nav002.Groups.Clear();
        financeiro_Nav003.Groups.Clear();

        if (lblEspacoTrabalho != "")
        {
            criaMenuEspacoTrabalho(dsMenu.Tables[0]);
        }

        if (lblEstrategia != "")
        {
            criaMenuEstrategia(dsMenu.Tables[0]);
        }

        if (lblProjetos != "")
        {
            criaMenuProjetos(dsMenu.Tables[0]);
        }

        if (lblDemandas != "")
        {
            criaMenuDemandas(dsMenu.Tables[0]);
        }

        if (lblFinanceiro != "")
        {
            criaMenuFinanceiro(dsMenu.Tables[0]);
        }

        if (lblAdministracao != "")
        {
            criaMenuAdministracao(dsMenu.Tables[0]);
        }

        montarMenuEspacoTrabalho = lblEspacoTrabalho != "";
        montarMenuEstrategia = lblEstrategia != "";
        montarMenuProjeto = lblProjetos != "";
        montarMenuDemandas = lblDemandas != "";
        montarMenuFinanceiro = lblFinanceiro != "";
        montarMenuAdministracao = lblAdministracao != "";
    }

    private void carregaMenuPrincipal(DataSet ds)
    {
        lblEspacoTrabalho = "";
        lblEstrategia = "";
        lblProjetos = "";
        lblDemandas = "";
        lblFinanceiro = "";
        lblAdministracao = "";

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dtMenu = ds.Tables[0];

            DataRow[] dr = dtMenu.Select("Iniciais = 'PR_ESP'");

            lblEspacoTrabalho = (dr.Length > 0) ? dr[0]["NomeMenu"].ToString() : "";

            dr = dtMenu.Select("Iniciais = 'PR_EST'");

            lblEstrategia = (dr.Length > 0) ? dr[0]["NomeMenu"].ToString() : "";

            dr = dtMenu.Select("Iniciais = 'PR_PRJ'");

            lblProjetos = (dr.Length > 0) ? dr[0]["NomeMenu"].ToString() : "";

            dr = dtMenu.Select("Iniciais = 'PR_DEM'");

            lblDemandas = (dr.Length > 0) ? dr[0]["NomeMenu"].ToString() : "";

            dr = dtMenu.Select("Iniciais = 'PR_FIN'");

            lblFinanceiro = (dr.Length > 0) ? dr[0]["NomeMenu"].ToString() : "";

            dr = dtMenu.Select("Iniciais = 'PR_ADM'");

            lblAdministracao = (dr.Length > 0) ? dr[0]["NomeMenu"].ToString() : "";
        }
    }

    private void constroiSubMenus(DataRow[] dr, DataTable dtMenu, ASPxNavBar bar01, ASPxNavBar bar02, ASPxNavBar bar03, ref string lblMenu)
    {
        bool possuiItem = false;

        for (int i = 0; i < dr.Length; i++)
        {
            int codigoObjetoSubMenu = int.Parse(dr[i]["CodigoObjetoMenu"].ToString());

            DataRow[] drSubMenu = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoSubMenu, "OrdemObjeto");

            NavBarGroup subMenu = new NavBarGroup();

            if (i == 0)
            {
                subMenu = bar01.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 1)
            {
                subMenu = bar02.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 2)
            {
                subMenu = bar03.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 3)
            {
                subMenu = dr.Length == 4 ? bar02.Groups.Add(dr[i]["NomeMenu"].ToString()) : bar01.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 4)
            {
                subMenu = dr.Length == 5 ? bar03.Groups.Add(dr[i]["NomeMenu"].ToString()) : bar02.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 5)
            {
                subMenu = bar03.Groups.Add(dr[i]["NomeMenu"].ToString());
            }

            foreach (DataRow drItem in drSubMenu)
            {
                string urlMenu = drItem["URLObjetoMenu"].ToString();

                urlMenu += (urlMenu.Contains("?") ? "&" : "?") + "TITULO=" + Server.UrlEncode(drItem["NomeMenu"].ToString());

                //urlMenu += urlMenu.Contains("?") ? ("&TITULO=" + drItem["NomeMenu"].ToString()) : ("?TITULO=" + drItem["NomeMenu"].ToString());

                if (drItem["Iniciais"].ToString() == "PNL_GE")
                {
                    DataSet ds = cDados.getURLTelaInicialUsuario(codigoEntidadeLogada.ToString(), codigoUsuarioLogado.ToString());

                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        urlMenu = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                    }
                }
                else if (drItem["Iniciais"].ToString() == "EMSOS ") // emissão de ordem de serviço
                {
                    // busca o código do workflow para criar uma nova Demanda
                    int codigoFluxo, codigoWorkflow;

                    cDados.getCodigoWfAtualFluxoEmissaoOS(codigoEntidadeLogada, out codigoFluxo, out codigoWorkflow);

                    if ((codigoFluxo != 0) && (codigoWorkflow != 0))
                        urlMenu = "~/wfEngine.aspx?CF=" + codigoFluxo.ToString() + "&CW=" + codigoWorkflow.ToString();
                }

                urlMenu = urlMenu.Replace("@UrlApp", Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.AppRelativeCurrentExecutionFilePath.Replace("~/", ""))));

                subMenu.Items.Add(drItem["NomeMenu"].ToString(), null, null, urlMenu.Replace("¥ENT", codigoEntidadeLogada.ToString()));

                if (possuiItem == false)
                    possuiItem = true;
            }
        }

        if (possuiItem == false)
            lblMenu = "";
    }

    private void constroiSubMenusMobile(DataRow[] dr, DataTable dtMenu, ASPxNavBar bar01, ASPxNavBar bar02, ASPxNavBar bar03, ref string lblMenu)
    {
        bool possuiItem = false;

        bar01.ShowExpandButtons = true;
        bar01.AllowExpanding = true;
        bar01.AutoCollapse = true;
        bar01.Width = new Unit(bar01.Width.Value * 3);

        bar01.GroupHeaderStyle.Font.Size = new FontUnit("17pt");
        bar01.ItemStyle.Font.Size = new FontUnit("16pt");

        bar02.ClientVisible = false;
        bar03.ClientVisible = false;

        for (int i = 0; i < dr.Length; i++)
        {
            int codigoObjetoSubMenu = int.Parse(dr[i]["CodigoObjetoMenu"].ToString());

            DataRow[] drSubMenu = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoSubMenu, "OrdemObjeto");

            NavBarGroup subMenu = new NavBarGroup();

            subMenu = bar01.Groups.Add(dr[i]["NomeMenu"].ToString());

            foreach (DataRow drItem in drSubMenu)
            {
                string urlMenu = drItem["URLObjetoMenu"].ToString();

                if (drItem["Iniciais"].ToString() == "PNL_GE")
                {
                    DataSet ds = cDados.getURLTelaInicialUsuario(codigoEntidadeLogada.ToString(), codigoUsuarioLogado.ToString());

                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        urlMenu = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                    }
                }
                else if (drItem["Iniciais"].ToString() == "EMSOS ") // emissão de ordem de serviço
                {
                    // busca o código do workflow para criar uma nova Demanda
                    int codigoFluxo, codigoWorkflow;

                    cDados.getCodigoWfAtualFluxoEmissaoOS(codigoEntidadeLogada, out codigoFluxo, out codigoWorkflow);

                    if ((codigoFluxo != 0) && (codigoWorkflow != 0))
                        urlMenu = "~/wfEngine.aspx?CF=" + codigoFluxo.ToString() + "&CW=" + codigoWorkflow.ToString();
                }

                //urlMenu = urlMenu.Replace("@UrlApp", Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("~/")));

                subMenu.Items.Add(drItem["NomeMenu"].ToString(), null, null, urlMenu.Replace("¥ENT", codigoEntidadeLogada.ToString()));

                if (possuiItem == false)
                    possuiItem = true;
            }
        }

        if (possuiItem == false)
            lblMenu = "";
    }

    private void criaMenuEspacoTrabalho(DataTable dtMenu)
    {
        DataRow[] dr = dtMenu.Select("Iniciais = 'PR_ESP'");

        if (dr.Length == 0)
        {
            lblEspacoTrabalho = "";
            return;
        }

        int codigoObjetoMenu = int.Parse(dr[0]["CodigoObjetoMenu"].ToString());

        dr = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoMenu, "OrdemObjeto");

        if (isMobile)
        {
            constroiSubMenusMobile(dr, dtMenu, espacoTrabalho_Nav001, espacoTrabalho_Nav002, espacoTrabalho_Nav003, ref lblEspacoTrabalho);
        }
        else
        {
            constroiSubMenus(dr, dtMenu, espacoTrabalho_Nav001, espacoTrabalho_Nav002, espacoTrabalho_Nav003, ref lblEspacoTrabalho);
        }
    }

    private void criaMenuEstrategia(DataTable dtMenu)
    {
        DataRow[] dr = dtMenu.Select("Iniciais = 'PR_EST'");

        int codigoObjetoMenu = int.Parse(dr[0]["CodigoObjetoMenu"].ToString());

        dr = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoMenu, "OrdemObjeto");

        if (isMobile)
        {
            constroiSubMenusMobile(dr, dtMenu, estrategia_Nav001, estrategia_Nav002, estrategia_Nav003, ref lblEstrategia);
        }
        else
        {
            constroiSubMenus(dr, dtMenu, estrategia_Nav001, estrategia_Nav002, estrategia_Nav003, ref lblEstrategia);
        }
    }

    private void criaMenuProjetos(DataTable dtMenu)
    {
        DataRow[] dr = dtMenu.Select("Iniciais = 'PR_PRJ'");

        int codigoObjetoMenu = int.Parse(dr[0]["CodigoObjetoMenu"].ToString());

        dr = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoMenu, "OrdemObjeto");

        if (isMobile)
        {
            constroiSubMenusMobile(dr, dtMenu, projetos_Nav001, projetos_Nav002, projetos_Nav003, ref lblProjetos);
        }
        else
        {
            constroiSubMenus(dr, dtMenu, projetos_Nav001, projetos_Nav002, projetos_Nav003, ref lblProjetos);
        }
    }

    private void criaMenuDemandas(DataTable dtMenu)
    {
        DataRow[] dr = dtMenu.Select("Iniciais = 'PR_DEM'");

        int codigoObjetoMenu = int.Parse(dr[0]["CodigoObjetoMenu"].ToString());

        dr = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoMenu, "OrdemObjeto");

        if (isMobile)
        {
            constroiSubMenusMobile(dr, dtMenu, demandas_Nav001, demandas_Nav002, demandas_Nav003, ref lblDemandas);
        }
        else
        {
            constroiSubMenus(dr, dtMenu, demandas_Nav001, demandas_Nav002, demandas_Nav003, ref lblDemandas);
        }

    }

    private void criaMenuFinanceiro(DataTable dtMenu)
    {
        DataRow[] dr = dtMenu.Select("Iniciais = 'PR_FIN'");

        int codigoObjetoMenu = int.Parse(dr[0]["CodigoObjetoMenu"].ToString());

        dr = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoMenu, "OrdemObjeto");

        if (isMobile)
        {
            constroiSubMenusMobile(dr, dtMenu, financeiro_Nav001, financeiro_Nav002, financeiro_Nav003, ref lblFinanceiro);
        }
        else
        {
            constroiSubMenus(dr, dtMenu, financeiro_Nav001, financeiro_Nav002, financeiro_Nav003, ref lblFinanceiro);
        }
    }

    private void criaMenuAdministracao(DataTable dtMenu)
    {
        DataRow[] dr = dtMenu.Select("Iniciais = 'PR_ADM'");

        int codigoObjetoMenu = int.Parse(dr[0]["CodigoObjetoMenu"].ToString());

        dr = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoMenu, "OrdemObjeto");

        if (isMobile)
        {
            constroiSubMenusMobile(dr, dtMenu, administracao_Nav001, administracao_Nav002, administracao_Nav003, ref lblAdministracao);
        }
        else
        {
            constroiSubMenus(dr, dtMenu, administracao_Nav001, administracao_Nav002, administracao_Nav003, ref lblAdministracao);
        }


    }

    private void criaMenuHTMLIcones()
    {
        string fonte = "";
        int tamanho = 60;

        if (isMobile)
        {
            fonte = "font-size:15pt";
            tamanho = 110;
        }

        string menuHTML = string.Format(@"<table style=""{0}"" cellpadding=""0"" cellspacing=""0"" >
                            <tr>", fonte, tamanho);

        string menuHTMLSegundaLinha = "<tr>";

        //Home ou Início
        menuHTML += string.Format(@"
                        <td style=""width: {0}px; cursor:pointer"" align=""center"" valign=""bottom"">
                        </td>
                        ", tamanho + 20);

        //string baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/') + "/";
        string homeIconeUrl = baseUrl + "imagens/principal/Home_Menu.PNG";
        menuHTMLSegundaLinha += string.Format(@"
                        <td alt=""Início"" style=""width: {0}px;"" align=""center"" valign=""top"" onclick=""window.top.gotoURL('index.aspx', '_self');"">
                            <img title=""Minha Página Inicial"" class=""dxeImage_MaterialCompact"" id=""imgHome"" onclick=""window.top.gotoURL('index.aspx', '_self');"" src=""{1}"" alt="""" style=""cursor:pointer;"">
                        </td>", tamanho + 20, homeIconeUrl);

        //Espaço de Trabalho
        if (montarMenuEspacoTrabalho)
        {
            menuHTML += string.Format(@"
                        <td style=""width: {1}px; cursor:pointer"" align=""center"" valign=""bottom"">
                            <img alt=""{2}"" src=""{0}imagens/principal/EspacoTrabalho_Menu.PNG"" style=""cursor:pointer"" id=""imgEspacoTrabalho"" onclick=""if(window.pcEspacoTrabalho)pcEspacoTrabalho.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" />
                        </td>
                        ", cDados.getPathSistema()
                         , tamanho + 20
                         , lblEspacoTrabalho);

            menuHTMLSegundaLinha += string.Format(@"
                        <td alt=""{0}"" style=""width: {1}px;"" align=""center"" valign=""top"" id=""tdEspacoTrabalho"">
                            {0}
                        </td>", lblEspacoTrabalho, tamanho + 20);
        }
        if (montarMenuEstrategia)
        {
            menuHTML += string.Format(@"
                        <td alt=""{2}"" style=""width: {1}px; cursor:pointer"" align=""center"" valign=""bottom"">
                            <img src=""{0}imagens/principal/Estrategia_Menu.PNG"" style=""cursor:pointer"" id=""imgEstrategia"" onclick=""{3}"" />
                        </td>
                        ", cDados.getPathSistema()
                         , tamanho + 20
                         , lblEstrategia
                         , "try{pcEstrategia.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));}catch(e){}");

            menuHTMLSegundaLinha += string.Format(@"
                        <td alt=""{0}"" style=""width: {1}px;"" align=""center"" valign=""top"" onclick=""{2}"" id=""tdEstrategia"">
                            {0}
                        </td>", lblEstrategia
                         , tamanho + 20, "try{pcEstrategia.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));}catch(e){}");
        }
        if (montarMenuProjeto)
        {
            menuHTML += string.Format(@"                        
                        <td alt=""{2}"" style=""width: {1}px; cursor:pointer"" align=""center"" valign=""bottom"">
                            <img src=""{0}imagens/principal/Portfolio_Menu.PNG"" style=""cursor:pointer"" id=""imgProjetos"" onclick=""if(window.pcProjetos)pcProjetos.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" />
                        </td>
                        ", cDados.getPathSistema()
                         , tamanho + 20
                         , lblProjetos);

            menuHTMLSegundaLinha += string.Format(@"
                        <td alt=""{0}"" style=""width: {1}px;"" align=""center"" valign=""top"" id=""tdProjetos"">
                            {0}
                        </td>", lblProjetos
                         , tamanho + 20);
        }
        if (montarMenuDemandas)
        {
            menuHTML += string.Format(@"                        
                        <td alt=""{2}"" style=""width: {1}px; cursor:pointer"" align=""center"" valign=""bottom"">
                            <img src=""{0}imagens/principal/Demandas_Menu.PNG"" style=""cursor:pointer"" id=""imgDemandas"" onclick=""if(window.pcDemandas)pcDemandas.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" />
                        </td>
                        ", cDados.getPathSistema()
                         , tamanho + 20
                         , lblDemandas);

            menuHTMLSegundaLinha += string.Format(@"
                        <td alt=""{0}"" style=""width: {1}px;"" align=""center"" valign=""top"" id=""tdDemandas"">
                            {0}
                        </td>", lblDemandas
                         , tamanho + 20);
        }
        if (montarMenuFinanceiro)
        {
            menuHTML += string.Format(@"
                        <td alt=""{2}"" style=""width: {1}px; cursor:pointer"" align=""center"" valign=""bottom"">
                            <img src=""{0}imagens/principal/Financeiro_Menu.PNG"" style=""cursor:pointer"" id=""imgFinanceiro"" onclick=""if(window.pcFinanceiro)pcFinanceiro.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" />
                        </td>
                        ", cDados.getPathSistema()
                         , tamanho + 30
                         , lblFinanceiro);

            menuHTMLSegundaLinha += string.Format(@"
                        <td alt=""{0}"" style=""width: {1}px;"" align=""center"" valign=""top"" id=""tdFinanceiro"">
                            {0}
                        </td>", lblFinanceiro
                         , tamanho + 30);
        }
        if (montarMenuAdministracao)
        {
            menuHTML += string.Format(@"
                        <td alt=""{2}"" style=""width: {1}px; cursor:pointer"" align=""center"" valign=""bottom"">
                            <img src=""{0}imagens/principal/Administracao_Menu.PNG"" style=""cursor:pointer"" id=""imgAdministracao"" onclick=""if(window.pcAdministracao)pcAdministracao.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" />
                        </td>
                        ", cDados.getPathSistema()
                         , tamanho + 30
                         , lblAdministracao);

            menuHTMLSegundaLinha += string.Format(@"
                        <td alt=""{0}"" style=""width: {1}px;"" align=""center"" valign=""top"" id=""tdAdministracao"">
                            {0}
                        </td>", lblAdministracao
                         , tamanho + 30);
        }

        menuHTML += string.Format(@"
                        <td alt=""{2}"" valign=""bottom"" align=""center"" style=""width: {1}px; cursor:pointer"">
                            <img src=""{0}imagens/principal/Ajuda_Menu.PNG"" style=""cursor:pointer"" id=""imgSobre"" onclick=""mostraSobre('{0}sobre.aspx');"" />
                        </td>
                        <td alt=""{3}"" valign=""bottom"" align=""center"" style=""width: {1}px; cursor:pointer"">
                            <img src=""{0}imagens/principal/Sair_Menu.PNG"" style=""cursor:pointer"" id=""imgSair"" onclick=""window.top.gotoURL('selecionaOpcao.aspx?op=sa', '_self');"" />
                        </td>
                    </tr>
                    ", cDados.getPathSistema()
                         , tamanho, Resources.traducao.sobre, Resources.traducao.sair_menu);

        menuHTMLSegundaLinha += string.Format(@"
                        <td alt=""{2}""  valign=""top"" align=""center"" onclick=""mostraSobre('{1}sobre.aspx');"" style=""width: {0}px"">
                            {2}
                        </td>
                        <td alt=""{3}"" valign=""top"" align=""center"" onclick=""window.top.gotoURL('selecionaOpcao.aspx?op=sa', '_self');"" style=""width: {0}px"">
                            {3}
                        </td>
                    </tr>
                   </table>"
                         , tamanho, cDados.getPathSistema(), Resources.traducao.sobre, Resources.traducao.sair_menu);

        //spanMenu.InnerHtml = menuHTML + menuHTMLSegundaLinha;

    }

    private void criaMenuHTMLLabels()
    {
        string menuHTML = string.Format(@"<table style=""padding-top:{0}"" cellpadding=""0"" cellspacing=""0"" >
                            <tr>", distanciaMenuLabels);


        //Espaço de Trabalho
        if (montarMenuEspacoTrabalho)
        {
            menuHTML += string.Format(@"
                        <td valign=""bottom""><table cellpadding=""0"" cellspacing=""0"" id=""imgEspacoTrabalho"" onclick=""if(window.pcEspacoTrabalho)pcEspacoTrabalho.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" style=""cursor:pointer;"">
                                <tr><td id=""tdEspacoTrabalho"">{0}</td><td><img src=""{1}imagens/principal/setaBaixoMenu.png"" /></td><td style=""width: 10px""></td></tr></table></td>
                        ", lblEspacoTrabalho
                         , cDados.getPathSistema());


        }
        if (montarMenuEstrategia)
        {
            menuHTML += string.Format(@"
                        <td valign=""bottom""><table cellpadding=""0"" cellspacing=""0"" id=""imgEstrategia"" onclick=""if(window.pcEstrategia)pcEstrategia.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" style=""cursor:pointer;"">
                                <tr><td id=""tdEstrategia"">{0}</td><td><img src=""{1}imagens/principal/setaBaixoMenu.png"" /></td><td style=""width: 10px""></td></tr></table></td>
                        ", lblEstrategia
                         , cDados.getPathSistema());


        }
        if (montarMenuProjeto)
        {
            menuHTML += string.Format(@"                        
                        <td valign=""bottom""><table cellpadding=""0"" cellspacing=""0"" id=""imgProjetos"" onclick=""if(window.pcProjetos)pcProjetos.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" style=""cursor:pointer;"">
                                <tr><td id=""tdProjetos"">{0}</td><td><img src=""{1}imagens/principal/setaBaixoMenu.png"" /></td><td style=""width: 10px""></td></tr></table></td>
                        ", lblProjetos
                         , cDados.getPathSistema());


        }
        if (montarMenuDemandas)
        {
            menuHTML += string.Format(@"                        
                        <td valign=""bottom""><table cellpadding=""0"" cellspacing=""0"" id=""imgDemandas"" onclick=""if(window.pcDemandas)pcDemandas.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" style=""cursor:pointer;"">
                                <tr><td id=""tdDemandas"">{0}</td><td><img src=""{1}imagens/principal/setaBaixoMenu.png"" /></td><td style=""width: 10px""></td></tr></table></td>
                        ", lblDemandas
                         , cDados.getPathSistema());


        }
        if (montarMenuFinanceiro)
        {
            menuHTML += string.Format(@"
                        <td valign=""bottom""><table cellpadding=""0"" cellspacing=""0"" id=""imgFinanceiro"" onclick=""if(window.pcFinanceiro)pcFinanceiro.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" style=""cursor:pointer;"">
                                <tr><td id=""tdFinanceiro"">{0}</td><td><img src=""{1}imagens/principal/setaBaixoMenu.png"" /></td><td style=""width: 10px""></td></tr></table></td>
                        ", lblFinanceiro
                         , cDados.getPathSistema());


        }

        if (urlSistemaOrcamento.Trim() != "")
        {
            menuHTML += string.Format(@"
                        <td valign=""bottom""><table cellpadding=""0"" cellspacing=""0"" id=""imgOrcamento"" onclick=""window.open('{0}', 'orc');"" style=""cursor:pointer;"">
                                <tr><td id=""tdOrcamento"">Orçamento</td><td style=""width: 10px""></td></tr></table></td>
                        ", urlSistemaOrcamento
                         , cDados.getPathSistema());
        }

        if (urlProcessosExternos.Trim() != "")
        {
            menuHTML += string.Format(@"
                        <td valign=""bottom""><table cellpadding=""0"" cellspacing=""0"" id=""imgProcessos"" onclick=""window.open('{0}', 'proc');"" style=""cursor:pointer;"">
                                <tr><td id=""tdProcessos"">Processos</td> <td style=""width: 10px""></td></tr></table></td>
                        ", urlProcessosExternos
                         , cDados.getPathSistema());
        }

        if (montarMenuAdministracao)
        {
            menuHTML += string.Format(@"
                        <td valign=""bottom""><table cellpadding=""0"" cellspacing=""0"" id=""imgAdministracao"" onclick=""if(window.pcAdministracao)pcAdministracao.ShowAtPos(ASPxClientUtils.GetEventX(event) / 2, ASPxClientUtils.GetEventY(event));"" style=""cursor:pointer;"">
                                <tr><td id=""tdAdministracao"">{0}</td><td><img src=""{1}imagens/principal/setaBaixoMenu.png"" /></td><td style=""width: 10px""></td></tr></table></td>
                        ", lblAdministracao
                         , cDados.getPathSistema());


        }

        menuHTML += string.Format(@"
                        <td valign=""bottom"" id=""imgSobre"" onclick=""mostraSobre('{0}sobre.aspx');"" style=""cursor:pointer;"">Sobre</td><td style=""width: 10px""></td>
                        <td valign=""bottom"" id=""imgSair"" onclick=""window.top.gotoURL('selecionaOpcao.aspx?op=sa', '_top');"" style=""cursor:pointer;"">Sair</td></tr></table>
                    ", cDados.getPathSistema());


        //spanMenu.InnerHtml = menuHTML;

    }

    #endregion

    #region Permissões

    private void limpaComponentes()
    {
        estrategia_Nav001.Groups.Clear();
        estrategia_Nav002.Groups.Clear();
        estrategia_Nav003.Groups.Clear();
    }


    #endregion

    #region Gerencia o Rastro

    public void geraRastroSite()
    {
        string caminho = Session["NomeArquivoNavegacao"] + "";
        string urlDestino = "";
        string txtCaminho = "";
        string nomeTela = "";
        //Se já existe um arquivo XML Temporário do rastro em: \Portal\ArquivosTemporarios Ler o arquivo caso contrário cria um arquivo novo.
        if (Session["NomeArquivoNavegacao"] != null && caminho != "")
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(caminho);

                int i = 0;



                for (i = 0; i < doc.ChildNodes[1].ChildNodes.Count; i++)
                {
                    XmlNode no = doc.SelectSingleNode(String.Format(@"/caminho/N[id={0}]", i));
                    if (no != null)
                    {

                        txtCaminho += String.Format(@" <span class=""sep-breadcrumb""></span> <a style=""font-size:7pt;"" href=""{0}"">{1}</a>"
                        , cDados.getPathSistema() + no.SelectSingleNode("./url").InnerText.ToString()
                        , no.SelectSingleNode("./nome").InnerText.ToString()
                        , String.IsNullOrEmpty(no.SelectSingleNode("./parametros").InnerText.ToString()) ? "" : "?" + no.SelectSingleNode("./parametros").InnerText.ToString());
                        nomeTela = no.SelectSingleNode("./nome").InnerText.ToString();
                    }
                }
                //lblCaminho.Text = String.Format(@"<a href=""" + cDados.getPathSistema() + @"index.aspx"">{0}</a>", Resources.traducao.in_cio_rastro) + txtCaminho;
            }
            catch
            {
                Response.RedirectLocation = cDados.getPathSistema() + "po_autentica.aspx";
            }
        }
        else
        {


            //Pega o Nome da Tela Inicial
            var dsTelaUrlPadrao = cDados.GetNomeTelaUrlPadraoUsuarioIdioma(codigoUsuarioLogado.ToString(), codigoEntidadeLogada.ToString());
            nomeTela = dsTelaUrlPadrao.Tables[0].Rows[0]["NomeObjetoMenu"].ToString();

            string nomeArquivo = "/ArquivosTemporarios/xml_Caminho" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioLogado + ".xml"; ;

            var ds = cDados.getURLTelaInicialUsuario(codigoEntidadeLogada.ToString(), codigoUsuarioLogado.ToString());

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                urlDestino = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                nomeTela = dsTelaUrlPadrao.Tables[0].Rows[0]["NomeObjetoMenu"].ToString();
            }

            string xml = string.Format(@"<caminho>
	                                            <N>
		                                            <id>0</id>
                                                    <nivel>0</nivel>
		                                            <url>{0}</url>
		                                            <nome>{1}</nome>
		                                            <parametros></parametros>
	                                            </N>
                                            </caminho>", urlDestino.Replace("~/", ""), nomeTela);

            Session["NomeArquivoNavegacao"] = Request.PhysicalApplicationPath + nomeArquivo;

            cDados.escreveXML(xml, nomeArquivo);
        }
        string comandoSQL = string.Format(@"

            declare @in_datetime as DateTime
            set @in_datetime = GETDATE()
            EXEC p_RegistraLogAcessoFuncionalidade  {0}, 
             @in_datetime, 
             {1}, 
            'Funcionalidade', 
			 '{2}' ", codigoUsuarioLogado, codigoEntidadeLogada, nomeTela);
        int registrosAfetados = 0;
        cDados.execSQL(comandoSQL, ref registrosAfetados);
    }
    #endregion


    #region Gerencia os Favoritos

    private void geraFavoritos()
    {
        nbFavoritos.Groups[0].Items.Clear();

        DataSet dsFavoritos = cDados.getFavoritosUsuario(codigoEntidadeLogada, codigoUsuarioLogado);
        bool possuiItem = false;

        if (cDados.DataSetOk(dsFavoritos) && cDados.DataTableOk(dsFavoritos.Tables[0]))
        {
            foreach (DataRow drItem in dsFavoritos.Tables[0].Rows)
            {
                string urlMenu = drItem["URL"].ToString();

                nbFavoritos.Groups[0].Items.Add(drItem["NomeLinkFavorito"].ToString(), null, null, urlMenu);

                if (possuiItem == false)
                    possuiItem = true;
            }
        }

        if (possuiItem)
        {
            pcFavoritos.PopupElementID = "imgFavoritos";
        }
        else
        {
            //imgFavoritos.ToolTip = Resources.traducao.voc__n_o_possui_favoritos;
            pcFavoritos.PopupElementID = "";
        }
    }

    public void verificaAcaoFavoritos(bool mostrarIcone, string nomeTela, string iniciaisMenu, string iniciaisTipoObjeto, int codigoObjetoAssociado, string toolTip)
    {
        permiteFavoritos = mostrarIcone;
        if (mostrarIcone)
        {
            //imgAcaoFavoritos.ClientVisible = true;

            if (codigoObjetoAssociado == -1)
                codigoObjetoAssociado = codigoEntidadeLogada;

            bool existeFavorito = cDados.verificaExistenciaFavoritosUsuario(codigoEntidadeLogada, codigoUsuarioLogado, iniciaisMenu, iniciaisTipoObjeto, codigoObjetoAssociado);

            if (existeFavorito)
            {
                hfFavoritos.Set("TipoEdicaoFavorito", "E");

                menuPrincipalFavoritosOpcoes.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosOpcoes
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "hidden" })
                    .ToArray()
                ));
                menuPrincipalFavoritosAdicionar.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosAdicionar
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "hidden" })
                    .Concat(new string[] { "hidden" })
                    .ToArray()
                ));
                menuPrincipalFavoritosRemover.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosRemover
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "hidden" })
                    .ToArray()
                ));


                //estrelaFavoritos.Attributes["class"] = "fas fa-star ico-yellowFav";

                /*
                estrelaFavoritos.Attributes.Add("class", String.Join(" ", estrelaFavoritos
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "fas" })
                    .Concat(new string[] { "fas" })
                    .ToArray()
                ));
                estrelaFavoritos.Attributes.Add("class", String.Join(" ", estrelaFavoritos
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "far" })
                    .ToArray()
                ));
                */
                //imgAcaoFavoritos.ToolTip = "Remover dos Favoritos";
                //imgAcaoFavoritos.ImageUrl = "~/imagens/principal/favoritos_RMV.png";
                //imgAcaoFavoritos.ClientSideEvents.Click = @"function(s, e) {callbackFavoritos.PerformCallback();}";
            }
            else
            {
                hfFavoritos.Set("TipoEdicaoFavorito", "I");

                menuPrincipalFavoritosOpcoes.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosOpcoes
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "hidden" })
                    .ToArray()
                ));
                menuPrincipalFavoritosAdicionar.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosAdicionar
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "hidden" })
                    .ToArray()
                ));
                menuPrincipalFavoritosRemover.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosRemover
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "hidden" })
                    .Concat(new string[] { "hidden" })
                    .ToArray()
                ));

                //estrelaFavoritos.Attributes["class"] = "far fa-star ico-yellowFav";

                /*
                estrelaFavoritos.Attributes.Add("class", String.Join(" ", estrelaFavoritos
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "far" })
                    .Concat(new string[] { "far" })
                    .ToArray()
                ));
                estrelaFavoritos.Attributes.Add("class", String.Join(" ", estrelaFavoritos
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "fas" })
                    .ToArray()
                ));
                */
                //imgAcaoFavoritos.ToolTip = toolTip;
                //imgAcaoFavoritos.ImageUrl = "~/imagens/principal/favoritos_ADD.png";
                //imgAcaoFavoritos.ClientSideEvents.Click = "function(s, e) {try{pcNovoFavorito.Show();}catch(e){}}";
            }

            txtNomeFavorito.Text = nomeTela.Length > 100 ? nomeTela.Substring(0, 95) + "..." : nomeTela.ToString();
            hfFavoritos.Set("NomeTelaReferencia", nomeTela.Length > 100 ? nomeTela.Substring(0, 95) + "..." : nomeTela);
            hfFavoritos.Set("URL", Request.AppRelativeCurrentExecutionFilePath.ToString() + "?" + Request.QueryString.ToString());
            hfFavoritos.Set("IniciaisMenu", iniciaisMenu);
            hfFavoritos.Set("IniciaisTipoObjeto", iniciaisTipoObjeto);
            hfFavoritos.Set("CodigoObjetoAssociado", codigoObjetoAssociado);
            mostrarAcaoFavoritos = "block";
        }
        else
        {
            //imgAcaoFavoritos.ClientVisible = false;
            mostrarAcaoFavoritos = "none";

            //estrelaFavoritos.Attributes["class"] = "far fa-star disabled";

            menuPrincipalFavoritosOpcoes.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosOpcoes
                .Attributes["class"]
                .Split(' ')
                .Except(new string[] { "", "hidden" })
                .Concat(new string[] { "hidden" })
                .ToArray()
            ));
            menuPrincipalFavoritosAdicionar.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosAdicionar
                .Attributes["class"]
                .Split(' ')
                .Except(new string[] { "", "hidden" })
                .Concat(new string[] { "hidden" })
                .ToArray()
            ));
            menuPrincipalFavoritosRemover.Attributes.Add("class", String.Join(" ", menuPrincipalFavoritosRemover
                .Attributes["class"]
                .Split(' ')
                .Except(new string[] { "", "hidden" })
                .Concat(new string[] { "hidden" })
                .ToArray()
            ));

        }
    }

    protected void pnCallbackFavoritos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string msg = "";
        string tipoEdicao = hfFavoritos.Get("TipoEdicaoFavorito") + "";
        string nomeFavorito = txtNomeFavorito.Text;
        string nomeReferencia = hfFavoritos.Get("NomeTelaReferencia") + ""; ;
        string url = hfFavoritos.Get("URL") + "";
        string iniciaisMenu = hfFavoritos.Get("IniciaisMenu") + "";
        string iniciaisTipoObjeto = hfFavoritos.Get("IniciaisTipoObjeto") + "";
        int codigoObjetoAssociado = hfFavoritos.Get("CodigoObjetoAssociado") + "" == "" || hfFavoritos.Get("CodigoObjetoAssociado") + "" == "-1" ? codigoEntidadeLogada : int.Parse(hfFavoritos.Get("CodigoObjetoAssociado") + "");
        bool result = false;

        if (tipoEdicao == "I")
            result = cDados.incluiFavoritoUsuario(codigoEntidadeLogada, codigoUsuarioLogado, nomeFavorito, nomeReferencia, url, iniciaisMenu, iniciaisTipoObjeto, codigoObjetoAssociado, ref msg);
        else
            result = cDados.excluiFavoritoUsuario(codigoEntidadeLogada, codigoUsuarioLogado, iniciaisMenu, iniciaisTipoObjeto, codigoObjetoAssociado, ref msg);


        pnCallbackFavoritos.JSProperties["cp_URL"] = url.Substring(2);
    }

    protected void callbackPontoVermelhoExistemMensagens_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        //existemMensagens = false;
        //mensagens = "";
        //Alertas();

        //if (!existemMensagens)
        //{
        //    //spanPontoVermelhoExistemMensagens.Style.Add("display", "none");
        //    //menuPrincipalExistemMensagens.Style.Add("display", "none");
        //}
        //else
        //{
        //    //spanPontoVermelhoExistemMensagens.Style.Add("display", "");
        //    //menuPrincipalExistemMensagens.Style.Add("display", "");
        //}
        //((ASPxCallbackPanel)(sender)).JSProperties["cp_mensagens"] = mensagens;


    }

    #endregion

    #region Gerencia as Carteiras

    private void defineImgStatusReport()
    {
        int codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
        bool possuiStatusReport = cDados.verificaCarteiraPossuiStatusReport(codigoCarteira);
        imgStatusReport.Enabled = possuiStatusReport;

        if (possuiStatusReport == true)
        {
            imgStatusReport.ImageUrl = "~/imagens/botoes/btnPDF.png";
            imgStatusReport.ToolTip = "Imprimir Último Boletim de Status Publicado";
        }
        else
        {
            imgStatusReport.ImageUrl = "~/imagens/botoes/btnPDFDes.png";
            imgStatusReport.ToolTip = "Nenhum Boletim de Status Publicado";
        }

        string relativePath = cDados.getPathSistema() + string.Format("_Projetos/DadosProjeto/HistoricoStatusReport.aspx?idObjeto={0}&tp=CP", codigoCarteira);
        imgHistoricoStatusReport.JSProperties["cp_RelativePath"] = relativePath;
    }

    private void defineLabelCarteira()
    {
        DataSet dsParametro = cDados.getParametrosSistema("labelCarteiras", "labelCarteirasPlural");
        string label = "Carteira";

        if ((cDados.DataSetOk(dsParametro)) && (cDados.DataTableOk(dsParametro.Tables[0])))
        {
            label = dsParametro.Tables[0].Rows[0]["labelCarteiras"].ToString();
        }

        //lblCarteira.Text = label.TrimEnd() + ":";
        //lblCarteira.Text = label.TrimEnd();

    }

    private void mostrarCarteiras()
    {
        bool mostrar = cDados.mostraComboCarteirasDeProjetosTela(Request.AppRelativeCurrentExecutionFilePath.ToString());
        //lblCarteira.ClientVisible = mostrar;
        ddlVisaoInicial.ClientVisible = mostrar;

        if (mostrar)
        {
            int codigoCarteira;
            int.TryParse((ddlVisaoInicial.Value != null) ? ddlVisaoInicial.Value.ToString() : "-1", out codigoCarteira);
            bool possuiBoletimStatus = cDados.indicaCarteiraPossuiBoletimStatus(codigoCarteira);
            imgStatusReport.Visible = possuiBoletimStatus;
            imgHistoricoStatusReport.ClientVisible = possuiBoletimStatus;
        }
        else
        {
            imgStatusReport.Visible = false;
            imgHistoricoStatusReport.ClientVisible = false;
        }
    }

    protected void callbackCarteiras_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        cDados.setInfoSistema("CodigosProjetosUsuario", null);

        string param = e.Parameter.ToLower();
        switch (param)
        {
            case "down":
                int codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
                DownloadStatusReportCarteira(codigoCarteira);
                break;
            default:
                cDados.setInfoSistema("CodigoCarteira", ddlVisaoInicial.Value.ToString());
                break;
        }
    }

    private void DownloadStatusReportCarteira(int codigoCarteira)
    {
        string comandoSql = string.Format(@"
DECLARE @CodigoCarteira INT
	SET @CodigoCarteira = {2}

 SELECT sr.ConteudoStatusReport
   FROM {0}.{1}.StatusReport sr 
  WHERE sr.CodigoTipoAssociacaoObjeto = dbo.f_GetCodigoTipoAssociacao('CP')
    AND sr.CodigoObjeto = @CodigoCarteira
    AND sr.ConteudoStatusReport IS NOT NULL
    AND sr.DataExclusao IS NULL
  ORDER BY
		sr.DataGeracao DESC"
            , cDados.getDbName(), cDados.getDbOwner(), codigoCarteira);

        DataSet ds = cDados.getDataSet(comandoSql);

        Byte[] byteArray = (Byte[])ds.Tables[0].Rows[0]["ConteudoStatusReport"];
        // Limpa todo conteúdo de saída do buffer stream
        Response.Clear();
        // Adicionar um cabeçalho HTTP para o fluxo de saída que especifica o nome de arquivo padrão
        // para o diálogo de download do navegador
        Response.AddHeader("Content-Disposition", "attachment; filename=Status_Report.pdf");
        // Adicionar um cabeçalho HTTP para o fluxo de saída que contém o comprimento do conteúdo (Tamanho do Arquivo). 
        // Isso permite que o browser saiba o quanto os dados estão sendo transferidos
        Response.AddHeader("Content-Length", byteArray.Length.ToString());
        // Define o HTTP MIME type do fluxo de saída
        Response.ContentType = "application/octet-stream";
        // Gravar os dados para o cliente.
        Response.BinaryWrite(byteArray);
        Response.Flush();
        Response.End();
    }

    protected void imgStatusReport_Click(object sender, ImageClickEventArgs e)
    {
        int codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
        DownloadStatusReportCarteira(codigoCarteira);
    }

    #endregion

    #region Gerencia o Período

    private void DefineConfiguracoesPeriodo()
    {
        DataTable dtParametro = cDados.getParametrosSistema("utilizaPeriodoUsuario").Tables[0];
        if (dtParametro.Rows.Count > 0 && dtParametro.Rows[0]["utilizaPeriodoUsuario"].ToString().ToUpper().Equals("S"))
        {
            //imgPeriodo.ClientVisible = true;
            string comandoSql = string.Format(@"SELECT * FROM UsuarioPeriodo WHERE CodigoUsuario = {0}", codigoUsuarioLogado);
            DataTable dtUsuarioPeriodo = cDados.getDataSet(comandoSql).Tables[0];
            if (dtUsuarioPeriodo.Rows.Count > 0)
            {
                DataRow dr = dtUsuarioPeriodo.Rows[0];
                object dataInicioPeriodo = dr["DataInicioPeriodo"];
                object dataFimPeriodo = dr["DataFimPeriodo"];
                deDataInicial.Value = dataInicioPeriodo;
                deDataFinal.Value = dataFimPeriodo;
            }
            else
            {
                deDataInicial.Value = null;
                deDataFinal.Value = null;
            }
        }
        else
        {
            //imgPeriodo.ClientVisible = false;
        }
    }

    protected void callbackPeriodo_Callback(object source, CallbackEventArgs e)
    {
        string comandoSql;
        int registrosAfetados = 0;
        string acao = e.Parameter;
        switch (acao)
        {
            case "limpar":
                #region Comando SQL
                comandoSql = string.Format(@"
DECLARE @CodigoUsuario int
    SET @CodigoUsuario = {0}
 DELETE FROM [UsuarioPeriodo] WHERE [CodigoUsuario] = @CodigoUsuario"
                    , codigoUsuarioLogado);
                #endregion

                cDados.execSQL(comandoSql, ref registrosAfetados);
                break;
            case "salvar":
                #region Comando SQL
                comandoSql = string.Format(@"
DECLARE @CodigoUsuario int,
        @DataInicioPeriodo datetime,
        @DataFimPeriodo datetime
        
    SET @CodigoUsuario = {0}
    SET @DataInicioPeriodo = {1}
    SET @DataFimPeriodo = {2}
            
IF EXISTS (SELECT 1 FROM [UsuarioPeriodo] WHERE [CodigoUsuario] = @CodigoUsuario)
BEGIN
    UPDATE [UsuarioPeriodo]
       SET [DataInicioPeriodo] = @DataInicioPeriodo
          ,[DataFimPeriodo] = @DataFimPeriodo
     WHERE [CodigoUsuario] = @CodigoUsuario
END
ELSE
BEGIN
    INSERT INTO [UsuarioPeriodo]
               ([CodigoUsuario]
               ,[DataInicioPeriodo]
               ,[DataFimPeriodo])
         VALUES
               (@CodigoUsuario
               ,@DataInicioPeriodo
               ,@DataFimPeriodo)
END"
                            , codigoUsuarioLogado
                            , deDataInicial.Value is DateTime ? string.Format(@"CONVERT(datetime, '{0:d}' + ' 00:00:00:000', 103)", deDataInicial.Value) : "NULL"
                            , deDataFinal.Value is DateTime ? string.Format(@"CONVERT(datetime, '{0:d}' + ' 23:00:00:000', 103)", deDataFinal.Value) : "NULL");
                #endregion

                cDados.execSQL(comandoSql, ref registrosAfetados);
                break;
            default:
                break;
        }
    }

    protected void callbackNotificacoes_Callback(object source, CallbackEventArgs e)
    {
        string notificacoes = e.Parameter;

        Session["notificacoes"] = notificacoes;

    }

    protected void callbackDesbloqueio_Callback(object source, CallbackEventArgs e)
    {
        int regAf = 0;
        cDados.execSQL("UPDATE Formulario SET CodigoUsuarioCheckOut = null, DataCheckOut = null, DataCheckIn = GetDate() WHERE CodigoFormulario = " + e.Parameter, ref regAf);
    }

    #endregion

    #region Gerencia o Glossário

    protected void imgGlossario_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

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

        ((ASPxImage)sender).ClientVisible = false;
        DataSet ds = cDados.getParametrosSistema("utilizaGlossario");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) && !string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][0].ToString()))
            {
                if (ds.Tables[0].Rows[0][0].ToString().ToLower().Trim() == "s")
                {
                    ((ASPxImage)sender).ClientVisible = true;
                }
            }
        }
    }

    #endregion

    protected void callbackLimpaSessao_Callback(object source, CallbackEventArgs e)
    {
        Session.Remove("notificacoes");
    }
}
public class Notificacoes
{
    public int codigoUsuario { get; set; }
    public int codigoEntidade { get; set; }    
    public string eMail { get; set; }

    public int qtdRiscoS1 { get; set; }
    public int qtdRiscoS1Destaque { get; set; }
    public bool indicaIconeRiscoS1Ligado { get; set; }
    
    public int qtdRiscoS2 { get; set; }
    public int qtdRiscoS2Destaque { get; set; }
    public bool indicaIconeRiscoS2Ligado { get; set; }
    
    public int qtdQuestaoS1 { get; set; }
    public int qtdQuestaoS1Destaque { get; set; }
    public bool indicaIconeQuestaoS1Ligado { get; set; }
    
    public int qtdIndicador { get; set; }
    public int qtdIndicadorDestaque { get; set; }
    public bool indicaIconeIndicadorLigado { get; set; }
    
    public int qtdReuniao { get; set; }
    public int qtdReuniaoDestaque { get; set; }
    public bool indicaIconeReuniaoLigado { get; set; }
    
    public int qtdTarefaS1 { get; set; }
    public int qtdTarefaS1Destaque { get; set; }
    public bool indicaIconeTarefaS1Ligado { get; set; }
   
    public int qtdTarefaS2 { get; set; }
    public int qtdTarefaS2Destaque { get; set; }
    public bool indicaIconeTarefaS2Ligado { get; set; }

    public int qtdContrato { get; set; }
    public int qtdContratoDestaque { get; set; }
    public bool indicaIconeContratoLigado { get; set; }

    public int qtdPndFin { get; set; }
    public int qtdPndFinDestaque { get; set; }
    public bool indicaIconePndFinLigado { get; set; }

    public int qtdAprovTrf { get; set; }
    public int qtdAprovTrfDestaque { get; set; }
    public bool indicaIconeAprovTrfLigado { get; set; }

    public int qtdPndWf { get; set; }
    public int qtdPndWfDestaque { get; set; }
    public bool indicaIconePndWfLigado { get; set; }

    public int qtdMensagem { get; set; }
    public int qtdMensagemDestaque { get; set; }
    public bool indicaIconeMensagemLigado { get; set; }

}