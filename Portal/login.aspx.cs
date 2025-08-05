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
using System.Web.Hosting;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Threading;
using System.Net;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Collections.Generic;
using DevExpress.Web;
using Cdis.Brisk.Service.Services.Usuario;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.Infra.Core.Extensions;
using System.Threading.Tasks;


public partial class login : BasePageBrisk
{
    public string baseUrl;
    dados cDados;
    public string linhaDataNascimento = "none";
    bool utilizaAutenticacaoExterna = false;
    string utilizaAutenticacaoExterna_TIPO = "";
    string utilizaAutenticacaoExterna_DOMINIO = "";
    string utilizaAutenticacaoExterna_ServidorLDAP_AD = "";
    string autenticacaoExterna_PortaLDAP = "";
    string autenticacaoExterna_PortaLDAPSSL = "";
    string autenticacaoExterna_IndicaSSL = "";
    int codigoUsuarioExterno = 0;
    bool apagaArquivosTemporariosViaThread = true; // este é o caminho correto
    bool apagaArquivosTemporariosViaLoginUsuario = false; // este é alternativo - PBH
    string chave = "";
    string nomeUsuario = "";
    string IDEstiloVisual = "";
    public bool isShowMsg = false;
    public string briskMsg = "";
    public string briskTipoMsg = "";
    public string jsonTraducao;
    public bool isRedefinirSenha = false;
    public string urlWSnewBriskBase;
    protected void Page_Init(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        //Valia o Browser para verificar se está nos browsers padrão do Brisk
        HttpBrowserCapabilities bc = Request.Browser;
        if (!(bc.Browser == "Chrome" || bc.Browser == "Firefox" || bc.Browser == "MicrosoftEdge" || bc.Browser == "IE" || bc.Browser == "InternetExplorer" | (Request.Browser.IsMobileDevice)))
        {
            string respostaBrowser = Resources.traducao.login_o_sistema___incompat_vel_para_a_vers_o_de_browser__por_favor__instalar_chrome__firefox_ou_microsoft_edge_;
            Response.Write("<script>alert('" + respostaBrowser + "')</script>");
        }
    }

    public void SetInvisibleDivAuth()
    {
        divAuth.Attributes.Add("style", "display:none");
        divEsqueciSenha.Attributes.Add("style", "display:none");        
        divRedefinirSenha.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        List<string> listTraducaoItem = new List<string>()
        {
            "Vazia",
            "Curta_Demais",
            "Fraca",
            "Razoavel",
            "Forte"
        };
        jsonTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeof(Resources.traducao), listTraducaoItem).ToJson();

        txtUsuario.NullText = Resources.traducao.usu_rio;
        txtSenha.NullText = Resources.traducao.senha;
        btnAcessar.Text = Resources.traducao.entrar;
        btnAlterar.Text = Resources.traducao.alterar_senha;
        hplEsqueciSenha.Text = Resources.traducao.Esqueceu_A_Senha;
        ltrEsqueceuSenha.Text = Resources.traducao.Esqueceu_A_Senha + " ?";
        txtEmailEsqueciSenha.NullText = Resources.traducao.email;
        btnEnviarEsqueciSenha.Text = Resources.traducao.enviar;
        btnCancelarEsqueciSenha.Text = Resources.traducao.cancelar;
        lbMessagePassword.Text = Resources.traducao.Vazia;
        ltrTituloRedefinirSenha.Text = Resources.traducao.redefinir_senha;
        btnCancelarRedefinirSenha.Text = Resources.traducao.cancelar;
        txtConfirmaNovaSenha.NullText = Resources.traducao.senha;
        btnSalvarNovaSenha.Text = Resources.traducao.salvar;
        
        SetInvisibleDivAuth();
        divAuth.Attributes.Add("style", "display:block");
        cDados = CdadosUtil.GetCdados(null);    

        if (Session["Brisk:TipoMsg"] != null)
        {
            briskMsg = Session["Brisk:Msg"].ToString();
            briskTipoMsg = Session["Brisk:TipoMsg"].ToString();
            isShowMsg = true;
            Session["Brisk:Msg"] = null;
            Session["Brisk:TipoMsg"] = null;
            MostrarMensagem(briskMsg, briskTipoMsg);
        }

        if (Request.QueryString["key"] != null)
        {
            if (Session["Brisk:SolicitarNovaSenha"] == null)
            {
                chave = Request.QueryString["key"].ToString();
                bool isChaveValida = UowApplication.UowService.GetUowService<UsuarioEsqueceuSenhaService>().GetIsChaveValida(chave);
                if (isChaveValida)
                {
                    SetInvisibleDivAuth();
                    divRedefinirSenha.Visible = true;
                    divRedefinirSenha.Attributes.Add("style", "display:block");
                    Session["Brisk:SolicitarNovaSenha"] = null;
                    isRedefinirSenha = true;
                }
                else
                {
                    MostrarMensagem(Resources.traducao.Solicite_Nova_Chave, "Atencao");
                    Session["Brisk:SolicitarNovaSenha"] = true;
                }

                cDados.aplicaEstiloVisual(Page, "MaterialCompact");
            }
            else
            {
                SetInvisibleDivAuth();
                divEsqueciSenha.Attributes.Add("style", "display:block");
                Session["Brisk:SolicitarNovaSenha"] = null;
            }
        }
        else
        {
            //divAuth.Visible = true;
            divAuth.Attributes.Add("style", "display:block");
            //   ASPxPopupControlRedefineSenha.ShowOnPageLoad = true;
            if (!IsPostBack)
            {
                Session["Brisk:TentativaAcesso:Qtd"] = null;
            }
            this.TH(this.TS("login", "avisoDesconexao"));



            cDados.setInfoSistema("novoMenu", null);
            cDados.setInfoSistema("menuAlterarSenha", null);
            cDados.setInfoSistema("menuGerenciarPreferencias", null);
            cDados.setInfoSistema("menuOrganizarFavoritos", null);
            cDados.setInfoSistema("novoMenuAdministracao", null);

            pcApresentacaoAcao.JSProperties["cp_Path"] = cDados.getPathSistema();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //se tiver sido passado na url o parâmetro el (entrada login), anota na seção.
            if (Request.QueryString["el"] != null && Request.QueryString["el"].ToString() != "")
            {
                Session["modoLoginFNDE"] = Request.QueryString["el"].ToString();
            }
            if (!Page.IsCallback)
            {
                if (cDados.getInfoSistema("ResolucaoCliente") == null)
                {
                    cDados.setInfoSistema("Origem", "Login");
                    Session["Brisk:TentativaAcesso:Qtd"] = null;
                    Response.Redirect("~/index.aspx?" + Request.QueryString);
                }
            }

            string tituloPaginasWEB = System.Configuration.ConfigurationManager.AppSettings["nomeSistema"].ToString();
            lblLicenciado.Text = string.Format("{0}: {1}", Resources.traducao.licenciado_para, System.Configuration.ConfigurationManager.AppSettings["nomeEmpresa"].ToString());

            txtUsuario.Focus();
            painelLogin.HeaderText = "";

            // define o título da página
            Page.Title = "Brisk PPM";
            cDados.aplicaEstiloVisual(Page, "MaterialCompact");

            DataSet dsAutenticacaoExterna = cDados.getParametrosSistema(-1, "utilizaAutenticacaoExterna", "utilizaAutenticacaoExterna_TIPO", "utilizaAutenticacaoExterna_DOMINIO", "utilizaAutenticacaoExterna_ServidorLDAP_AD", "utilizaLogoUnidade", "utilizaAutenticacaoExternaOutroBanco", "urlTelaLogOff", "apagaArquivosTemporariosViaThread", "apagaArquivosTemporariosViaLoginUsuario", "ldapPort", "ldapPortSSL", "LdapSSL", "urlWSnewBriskBase");

            if (cDados.DataSetOk(dsAutenticacaoExterna) && cDados.DataTableOk(dsAutenticacaoExterna.Tables[0]))
            {
                //lê os valores retornados para alguns parametros
                utilizaAutenticacaoExterna_TIPO = dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna_TIPO"] + "";
                utilizaAutenticacaoExterna_DOMINIO = dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna_DOMINIO"] + "";
                utilizaAutenticacaoExterna_ServidorLDAP_AD = dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna_ServidorLDAP_AD"] + "";
                autenticacaoExterna_PortaLDAP = dsAutenticacaoExterna.Tables[0].Rows[0]["ldapPort"] + "";
                autenticacaoExterna_PortaLDAPSSL = dsAutenticacaoExterna.Tables[0].Rows[0]["ldapPortSSL"] + "";
                autenticacaoExterna_IndicaSSL = dsAutenticacaoExterna.Tables[0].Rows[0]["LdapSSL"] + "";

                if (dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna"] + "" == "S" ||          // Usado apenas pelo FNDE
                    dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna_TIPO"] + "" != "")        // Usado apenas por PBH
                {
                    utilizaAutenticacaoExterna = true;
                    btnAlterar.ClientVisible = false;
                    btnLoginIntegrado.ClientVisible = false;
                    //imgHelpUsuario.ClientVisible = false;

                    // se a autenticação externa está configurada e o TIPO está vazio, então significa que trata-se da autenticação FNDE.
                    // Neste caso, será redirecionado para a tela quando é feito o logoff do sistema, uma vez a nova autenticação FNDE é feita em tela fora do sistema
                    // 23/10/2014 -> Incluído nessa regra a verificação do parâmetro URL igAP
                    if ((utilizaAutenticacaoExterna == true) && (utilizaAutenticacaoExterna_TIPO == ""))
                    {
                        string urlTelaLogOff = dsAutenticacaoExterna.Tables[0].Rows[0]["urlTelaLogOff"] + "";
                        if (urlTelaLogOff != "")
                        {
                            if (Session["modoLoginFNDE"] == null || Session["modoLoginFNDE"].ToString() != "2")
                                Response.Redirect(urlTelaLogOff);
                        }
                    }
                }

                if (dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaLogoUnidade"] + "" == "S")
                {
                    //imgLogoLD.ClientVisible = true;
                }

                /* Em PBH, a tentativa de excluir os arquivos temporários via Thread provoca um erro de permissão
                 * para contornar, foram criados os parametros "apagaArquivosTemporariosViaThread" e "apagaArquivosTemporariosViaLoginUsuario"
                 * se os dois parametros tiverem os valores iguais a "N", os arquivos NUNCA serão apagados
                 * se o parametro "apagaArquivosTemporariosViaThread" é "S", será utilizado o método tradicional - Thread
                 * se o parametro "apagaArquivosTemporariosViaLoginUsuario" é "S", os arquivos serão exluídos por um loop sempre que algum usuário autenticar no sistema 
                 * ===================================================================================================================================================== */
                if (dsAutenticacaoExterna.Tables[0].Rows[0]["apagaArquivosTemporariosViaThread"] + "" == "N")
                {
                    apagaArquivosTemporariosViaThread = false;

                    if (dsAutenticacaoExterna.Tables[0].Rows[0]["apagaArquivosTemporariosViaLoginUsuario"] + "" == "S")
                    {
                        apagaArquivosTemporariosViaLoginUsuario = true;
                    }
                }
                urlWSnewBriskBase = dsAutenticacaoExterna.Tables[0].Rows[0]["urlWSnewBriskBase"] + "";

                //if (System.Diagnostics.Debugger.IsAttached)
                //{
                //    urlWSnewBriskBase = "";
                //}


                if (urlWSnewBriskBase != "")
                {
                    urlWSnewBriskBase = dsAutenticacaoExterna.Tables[0].Rows[0]["urlWSnewBriskBase"] + "";
                }
                else
                {
                    urlWSnewBriskBase = "http://localhost:50977";
                    //urlWSnewBriskBase = "https://ws.desenv.briskppm.com.br/brisk-bsc-ptbr";
                }

                hfGeral.Set("urlWSnewBriskBase", urlWSnewBriskBase);
                Session["urlWSnewBriskBase"] = urlWSnewBriskBase;
            }
        }


    }

    private void SetVisibleCaptcha()
    {
        if (Session["Brisk:TentativaAcesso:Qtd"] == null)
        {
            Session["Brisk:TentativaAcesso:Qtd"] = 1;
        }
        else
        {
            Session["Brisk:TentativaAcesso:Qtd"] = int.Parse(Session["Brisk:TentativaAcesso:Qtd"].ToString()) + 1;
        }
        Captcha.Visible = (Session["Brisk:TentativaAcesso:Qtd"] == null)
                      ? false
                      : int.Parse(Session["Brisk:TentativaAcesso:Qtd"].ToString()) >= 3;
    }

    protected void callbackSenha_Callback(object sender, CallbackEventArgsBase e)
    {
        callbackSenha.JSProperties["cp_codeAutorizacao"] = "";
        callbackSenha.JSProperties["cp_urlBase"] = "";
        callbackSenha.JSProperties["cp_Erro"] = "";
        callbackSenha.JSProperties["cp_captcha"] = "";
        


        cDados.setInfoSistema("IDUsuarioLogado", null);
        cDados.setInfoSistema("PerfilAdministrador", null);

        ViewState["Senha"] = txtSenha.Text;

        txtUsuario.Text = txtUsuario.Text.Replace("'", "");
        SetVisibleCaptcha();

        callbackSenha.JSProperties["cp_captcha"] = Captcha.Visible;

        if (txtUsuario.Text != "" && txtSenha.Text != "")
        {
            autenticarUsuario(txtUsuario.Text, txtSenha.Text, sender);
        }
        else
        {
            string aviso = "";
            if (txtUsuario.Text == "" && txtSenha.Text == "")
            {
                aviso = this.T("login_usuario_senha_nao_informados");
            }
            else if (txtUsuario.Text == "")
            {
                aviso = Resources.traducao.login_nome_de_usu_rio_deve_ser_informado;
            }
            else
            {
                aviso = Resources.traducao.login_senha_deve_ser_informada;
            }
            lblErro.Visible = true;
            callbackSenha.JSProperties["cp_Erro"] = lblErro.Text = aviso;
        }
    }

        protected void btnEntrar_Click(object sender, EventArgs e)
    {
        cDados.setInfoSistema("IDUsuarioLogado", null);
        cDados.setInfoSistema("PerfilAdministrador", null);

        ViewState["Senha"] = txtSenha.Text;

        txtUsuario.Text = txtUsuario.Text.Replace("'", "");
        SetVisibleCaptcha();
        if (txtUsuario.Text != "" && txtSenha.Text != "")
        {           
            autenticarUsuario(txtUsuario.Text, txtSenha.Text);
        }
        else
        {
            string aviso = "";
            if (txtUsuario.Text == "" && txtSenha.Text == "")
            {
                aviso = this.T("login_usuario_senha_nao_informados");
            }
            else if (txtUsuario.Text == "")
            {
                aviso = Resources.traducao.login_nome_de_usu_rio_deve_ser_informado;
            }
            else
            {
                aviso = Resources.traducao.login_senha_deve_ser_informada;
            }
            lblErro.Visible = true;
            lblErro.Text = aviso;
        }
    }

    private void autenticarUsuario(string usuario, string senha, object sender = null)
    {
        int qtdTentativaAcesso = int.Parse(Session["Brisk:TentativaAcesso:Qtd"].ToString());
        if ((qtdTentativaAcesso >= 3 && Captcha.IsValid) || (qtdTentativaAcesso < 3))
        {
            int senhaCriptografa;
            int codigoUsuario = -1;
            //string tipoAutenticacaoUsuario = "";
            string usuarioDeveAlterarSenha = "";

            /* Em 06/03/14, decidimos (Antonio/Geter/Ericsson) alterar o processo de autenticação, incluindo o modelo "AP"
             * -----------------------------------------------------------------------------------------------------------
             * Processo de autenticação pela tela login.aspx:
             *     => 1º Tenta autenticar no sistema (AS), se não conseguir, tenta autenticar utilizando as funções LDAP ou AD (AP)
             */

            // --------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 1º Tenta autenticar no Sistema - Tipo "AS"
            // --------------------------------------------------------------------------------------------------------------------------------------------------------------
            // Tenta autenticar gerando a senha pelo metodo do .NET Framework - GetHashCode()
            senhaCriptografa = senha.GetHashCode();
            codigoUsuario = cDados.getAutenticacaoUsuario(usuario, senhaCriptografa, "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

            // se não conseguiu autenticar, tenta pelo metodo desenvolvido pela CDIS - ObtemCodigoHash()
            if (codigoUsuario == 0)
            {
                senhaCriptografa = cDados.ObtemCodigoHash(senha);               
                codigoUsuario = cDados.getAutenticacaoUsuario(usuario, senhaCriptografa, "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);
            }

            // --------------------------------------------------------------------------------------------------------------------------------------------------------------
            // ------ FIM Autenticação "AS"
            // --------------------------------------------------------------------------------------------------------------------------------------------------------------

            // --------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 2º se NÃO conseguiu autenticar no sistema, tenta autenticar no IIS - Tipo "AI"
            // --------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (codigoUsuario == 0)
            {
                // Para autenticar no IIS, precisa ter o nome do dominio
                if (usuario.IndexOf('\\') > 0)
                {
                    string usuarioIIS = usuario;

                    // Antes de irmos no AD, vamos verificar se o usuário está cadastrado no banco de dados com este tipo de autenticação
                    int codigoUsuarioTemp = cDados.getAutenticacaoUsuario(usuario, 0, "AI", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

                    // se está cadastrado com o tipo de autenticação "AI", tenta autenticá-lo no AD
                    if (codigoUsuarioTemp > 0)
                    {
                        string mensagemErroAutenticacao = "(AD) " + Resources.traducao.login_n_o_foi_poss_vel_autenticar_o_usu_rio_;
                        // se não conseguiu autenticar...
                        if (!autenticacaoExternaServidorLDAP_AD("AD", usuarioIIS, senha, out codigoUsuario, out mensagemErroAutenticacao))
                        {
                            if (mensagemErroAutenticacao != "")
                            {
                                ((ASPxCallback)sender).JSProperties["cp_Erro"] = lblErro.Text = mensagemErroAutenticacao;
                                return;
                            }
                        }
                    }
                }
            }

            // --------------------------------------------------------------------------------------------------------------------------------------------------------------
            // ------ FIM Autenticação "AI"
            // --------------------------------------------------------------------------------------------------------------------------------------------------------------



            // --------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 3º se NÃO conseguiu autenticar no sistema, tenta autenticar em um sistema próprio (LDAP/AD/Outros)
            // --------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (codigoUsuario == 0)
            {
                string mensagemErroAutenticacao = "";

                // se o parametro que informa o tipo de autenticação externa não foi informado
                if (utilizaAutenticacaoExterna_TIPO == "")
                    mensagemErroAutenticacao = Resources.traducao.login_n_o_foi_poss_vel_autenticar_as_credenciais_informadas_;// "Não foi possível identificar o modelo de autenticação a ser utilizado.";
                else
                {
                    // Se conseguiu autenticar no ldap/ad
                    if (autenticacaoExternaServidorLDAP_AD(utilizaAutenticacaoExterna_TIPO, usuario, senha, out codigoUsuario, out mensagemErroAutenticacao))
                    {
                        // se conseguiu autenticar... busca outras informações do usuário
                        if (codigoUsuario > 0)
                            cDados.getDadosAutenticacaoUsuarioExterno(codigoUsuario, out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);
                    }
                }

                if (mensagemErroAutenticacao != "")
                {
                    ((ASPxCallbackPanel)sender).JSProperties["cp_Erro"] = lblErro.Text = mensagemErroAutenticacao;
                    UowApplication.UowService.GetUowService<LogAcessoUsuarioService>().SalvarAcessoUsuarioPorEmail(usuario);
                    return;
                }

            }

            // se NÃO conseguiu autenticar, avisa o usuário e sai
            if (codigoUsuario <= 0)
            {
                ((ASPxCallbackPanel)sender).JSProperties["cp_Erro"] = lblErro.Text = Resources.traducao.login_as_informa__es_digitadas_n_o_est_o_corretas;
                UowApplication.UowService.GetUowService<LogAcessoUsuarioService>().SalvarAcessoUsuarioPorEmail(usuario);
                return;
            }

            // daqui para baixo, o usuário está autenticado.
            // ---------------------------------------------

            // se o usuário precisa mudar a senha, avisa e sai.
            if (usuarioDeveAlterarSenha == "S")
            {
                ((ASPxCallbackPanel)sender).JSProperties["cp_Erro"] = lblErro.Text = Resources.traducao.login_sua_senha_expirou_e_deve_ser_alterada__clique_no_bot_o__alterar_senha__para_fazer_a_mudan_a_;
                return;
            }

            // agora está tudo certo. Usuário autenticado e com senha válida
            // -------------------------------------------------------------

            // lê as informações do usuário
            cDados.setInfoSistema("IDUsuarioLogado", codigoUsuario);
            cDados.setInfoSistema("IDEstiloVisual", IDEstiloVisual);
            cDados.setInfoSistema("NomeUsuarioLogado", nomeUsuario);
            Session["NomeUsuario"] = nomeUsuario;
            Session["RemoteIPUsuario"] = Request.UserHostAddress;

            // busca a(s) entidade(s) do usuário logado
            DataSet ds = cDados.getEntidadesUsuario(codigoUsuario, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuario);
            int QtdeEntidades = ds.Tables[0].Rows.Count;
            // determina qual tela será a inicial;
            int codigoEntidade = -1;
            if (QtdeEntidades == 0) // o usuário não tem acesso a nenhuma entidade
                codigoEntidade = 0;
            else if (QtdeEntidades == 1) // o usuário não tem acesso a apenas uma entidade
            {
                codigoEntidade = (ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString() != "") ? int.Parse(ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString()) : 0;
                cDados.setInfoSistema("SiglaUnidadeNegocio", ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString());
            }

            // se o usuário tem acesso a uma ou mais entidades, volta para Index e continua a aplicação
            if (QtdeEntidades >= 1)
            {
                cDados.setInfoSistema("CodigoEntidade", codigoEntidade);

                if (codigoEntidade != -1)
                    cDados.setInfoSistema("CodigoCarteira", cDados.getCodigoCarteiraPadraoUsuario(codigoUsuario, codigoEntidade).ToString());

                DataSet dsPmt = cDados.getParametrosSistema("nomeEmpresa", "tituloPaginasWEB");
                if (cDados.DataSetOk(dsPmt) && cDados.DataTableOk(dsPmt.Tables[0]))
                {
                    lblLicenciado.Text = this.T("login_licenciado_para") + ": " + dsPmt.Tables[0].Rows[0]["nomeEmpresa"].ToString();
                    Page.Title = "Brisk PPM";
                }

                if (cDados.PerfilAdministrador(codigoUsuario, codigoEntidade))
                    cDados.setInfoSistema("PerfilAdministrador", 1);

                // se é para apagar os arquivos temporarios - Normalmente esse é o caminho correto. 
                if (apagaArquivosTemporariosViaThread)
                {
                    try
                    {
                        //Apaga os arquivos temporários
                        Thread thr = new Thread(new ThreadStart(cDados.apagaArquivosTemporarios));

                        thr.Name = Thread.CurrentThread.GetHashCode().ToString();

                        thr.Start();
                    }
                    catch
                    { }
                }
                // se não é para apagar via Thread, verifica se pode apagar via login (PBH)
                else if (apagaArquivosTemporariosViaLoginUsuario)
                {
                    cDados.apagaArquivosTemporarios();
                }

                DataSet dsMapaDefaultUsuario = cDados.getCodigoMapaEstrategicoPadraoUsuario(codigoUsuario);
                if (cDados.DataSetOk(dsMapaDefaultUsuario) && cDados.DataTableOk(dsMapaDefaultUsuario.Tables[0]))
                {
                    cDados.setInfoSistema("CodigoMapaEstrategicoInicial", dsMapaDefaultUsuario.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"].ToString());
                }
                Session["Brisk:TentativaAcesso:Qtd"] = null;
                Session["TokenAcessoNewBrisk"] = null;

                string CodigoAutorizacaoBrisk1 = string.Empty;

                try
                {
                    CodigoAutorizacaoBrisk1 = geraTokenAutorizacaoNewBrisk();

                    if (String.IsNullOrEmpty(CodigoAutorizacaoBrisk1)) {
                        throw new Exception();
                    }

                    Session["CodigoAutorizacaoBrisk1"] = CodigoAutorizacaoBrisk1;
                    if (sender != null)
                    {
                        ((ASPxCallbackPanel)sender).JSProperties["cp_urlBase"] = Session["urlWSnewBriskBase"];
                    }
                }
                catch (Exception)
                {
                    ((ASPxCallbackPanel)sender).JSProperties["cp_Erro"] = lblErro.Text = Resources.traducao.login_falha_ao_obter_c_digo_interno_de_conex_o_mudan_a_;                    
                }

                if (string.IsNullOrEmpty(CodigoAutorizacaoBrisk1) == false)
                {
                    try
                    {
                        // login_falha_validacao_credenciais
                        var accessToenNBR = obtemAccessTokenNBR(CodigoAutorizacaoBrisk1);
                        Session["TokenAcessoNewBrisk"] = accessToenNBR;
                        if (sender != null)
                        {
                            ((ASPxCallbackPanel)sender).JSProperties["cp_tokenAcessoNewBrisk"] = accessToenNBR;
                        }
                    }
                    catch (HttpException exHttp)
                    {
                        ((ASPxCallbackPanel)sender).JSProperties["cp_Erro"] = lblErro.Text = Resources.traducao.login_falha_validacao_credenciais.Replace("{{0}}", exHttp.GetHttpCode().ToString() + " autenticar-usuario-sso");
                    }
                    catch (Exception)
                    {
                        ((ASPxCallbackPanel)sender).JSProperties["cp_Erro"] = lblErro.Text = Resources.traducao.login_falha_validacao_credenciais.Replace("{{0}}", "004");
                    }
                }
                
            }
        }
        else
        {
            UowApplication.UowService.GetUowService<LogAcessoUsuarioService>().SalvarAcessoUsuarioPorEmail(usuario);
            Captcha.Visible = true;
            Captcha.IsValid = qtdTentativaAcesso == 3;

            ((ASPxCallbackPanel)sender).JSProperties["cp_captcha"] = Captcha.Visible;
        }
    }

    public string geraTokenAutorizacaoNewBrisk()
    {
        var retorno = "";
        string comandoSQL = "";

        Guid tokenGuid = Guid.NewGuid();
        //x.ToString()
        //Gerar o token, Acionar a proc para gravar o token e ao final gravar o token na variavel de sessão.
        //            string comandoSQL = @"
        int idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        comandoSQL = string.Format(@"
            DECLARE @RC int
            DECLARE @siglaFederacaoIdentidade varchar(30)
            DECLARE @codigoAutorizacaoAcesso varchar(500)
            DECLARE @tokenAcesso varchar(4000)
            DECLARE @CodigoUsuario int
            DECLARE @codigoControleToken bigint

           set @siglaFederacaoIdentidade = 'brisk_em'
           set @codigoAutorizacaoAcesso = '{0}'
           set @tokenAcesso = null
           set @CodigoUsuario = {1}

           EXECUTE @RC = [dbo].[p_brk_rw_registraTokenUsuarioFederacaoIdentidade]
               @siglaFederacaoIdentidade
              ,@codigoAutorizacaoAcesso
              ,@tokenAcesso
              ,@CodigoUsuario
              ,@codigoControleToken OUTPUT
          
        SELECT @codigoAutorizacaoAcesso as codigoAutorizacaoAcesso, @codigoControleToken as codigoControleToken", tokenGuid.ToString(), idUsuarioLogado);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0]["codigoAutorizacaoAcesso"].ToString();
        }

        return retorno;

    }
    private class dadosTokenJsonNBR
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public object user { get; set; }
    }

    private class retornoJsonNBR
    {
        public bool success { get; set; }
        public dadosTokenJsonNBR data { get; set; }
        public object errors { get; set; }
    }

    public string obtemAccessTokenNBR(string codigoAutorizacao)
    {
        string token_nbr = string.Empty;

        var urlWsNbr = Session["urlWSnewBriskBase"].ToString();
        var requestUrl = urlWsNbr + "/api/v1/autenticar-usuario-sso";
        var _request = (HttpWebRequest)WebRequest.Create(requestUrl);

        _request.Method = "POST";
        _request.Accept = "application/json";
        _request.Headers.Add("cod-entidade-contexto", "0");
        _request.ContentType = "application/json-patch+json";

        var requestData = new
        {
            siglaFederacaoIdentidade = "brisk_em",
            idSessaoFederacaoIdentidade = codigoAutorizacao,
            uriProcessamentoLogin = urlWsNbr
        };

        // Converta o objeto em JSON
        var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

        using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
        {
            streamWriter.Write(jsonRequest);
            streamWriter.Flush();
        }

        var httpResponse = (HttpWebResponse)_request.GetResponse();
        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
            string responseStr;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseStr = streamReader.ReadToEnd();
                // Aqui você pode tratar o retorno, se necessário.
            }

            var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<retornoJsonNBR>(responseStr);
            token_nbr = responseData.data.access_token;
        }

        return token_nbr;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["Brisk:TentativaAcesso:Qtd"] = null;
        Response.Redirect("et_alteraSenha.aspx");
    }

    protected void btnLoginIntegrado_Click(object sender, EventArgs e)
    {
        Session["Brisk:TentativaAcesso:Qtd"] = null;
        Response.Redirect("po_autentica.aspx?" + Request.QueryString);
    }

    public string EncodeTo64(string toEncode)
    {
        byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
        string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
        return returnValue;
    }

    private string inverter(string Texto)
    {
        //Cria a partir do texto original um array de char
        char[] ArrayChar = Texto.ToCharArray();
        //Com o array criado invertemos a ordem do mesmo
        Array.Reverse(ArrayChar);
        //Agora basta criarmos uma nova String, ja com o array invertido.
        return new string(ArrayChar);
    }

    private bool autenticacaoExternaServidorLDAP_AD(string tipo, string usuario, string senha, out int codigoUsuario, out string mensagemErroAutenticacao)
    {
        codigoUsuario = -1;
        mensagemErroAutenticacao = "";
        int portaLDAP = 0;
        bool indicaSSL = false;

        string comandoSql = "Select codigoUsuario, IDEstiloVisual, NomeUsuario from usuario where ";

        if (tipo == "LDAP")
        {
            // posição do caracter "@" no campo nome do usuário.
            int posicaoArrobaNoCampoUsuario = usuario.IndexOf('@');

            string username = "uid=" + (posicaoArrobaNoCampoUsuario >= 0 ? usuario.Substring(0, posicaoArrobaNoCampoUsuario) : usuario) + ",";

            // se tem contrabarra no nome do usuário, o complemento não será utilizado, pois será feito com AD
            if (usuario.IndexOf('\\') > 0)
                username = usuario;

            // seleciona as unidades organizacionais de pesquisas
            string comandoSQL_LDAP_AD = "SELECT valor FROM ParametroConfiguracaoSistema WHERE Parametro LIKE 'LDAP_AD_DN%' ORDER BY Parametro";
            DataSet dsLDAP_AD = cDados.getDataSet(comandoSQL_LDAP_AD);
            bool autenticadoComSucesso = false;
            foreach (DataRow dr in dsLDAP_AD.Tables[0].Rows)
            {
                string complemento = dr["valor"].ToString().Trim();
                if (complemento == "")
                    continue;

                if (usuario.IndexOf('\\') > 0)
                    complemento = "";


                string usuarioLDAP = username + complemento;
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + utilizaAutenticacaoExterna_ServidorLDAP_AD,
                                                           usuarioLDAP,
                                                           senha, AuthenticationTypes.ServerBind);
                try
                {
                    Object obj = entry.NativeObject;
                    // se chegou aqui é por ter autenticado no LDAP com sucesso. Não precisa continuar.
                    autenticadoComSucesso = true;
                    mensagemErroAutenticacao = "";
                    break;
                }
                catch (Exception ex)
                {
                    mensagemErroAutenticacao += " DN = " + usuarioLDAP + ". Erro: " + ex.Message;
                }
            }

            // se deu erro em todas as tentativas de autenticação, mostra a mensagem e sai.
            if (!autenticadoComSucesso)
            {
                mensagemErroAutenticacao = this.T("login_erro_autenticacao_ldap") + "\n";// O Ricardo/Simone pediu para retirar a mensagem-->  +mensagemErroAutenticacao;
                return false;
            }

            // sucesso ao autenticar o usuário no ldap/ad. Agora vamos verificar se ele existe no banco do portal.
            // se não é usuário de domínio (não tem contrabarra)
            if (usuario.IndexOf('\\') < 0)
            {
                // se não digitou o caracter "@", vamos incluí-lo
                if (posicaoArrobaNoCampoUsuario < 0)
                    usuario += "@" + utilizaAutenticacaoExterna_DOMINIO;
                comandoSql += string.Format(" email = '{0}' and dataExclusao is null", usuario);
            }
            else // usuário de domínio, vamos pesquisar com o campo contaWindows
            {
                comandoSql += string.Format(" ContaWindows = '{0}' and dataExclusao is null", usuario);
            }
        }
        else if (tipo == "AD")
        {
            try
            {
                indicaSSL = (autenticacaoExterna_IndicaSSL.Equals("0") || autenticacaoExterna_IndicaSSL.Equals("N") || autenticacaoExterna_IndicaSSL.Equals("n")) ? false : true;
                portaLDAP = indicaSSL ? int.Parse(autenticacaoExterna_PortaLDAPSSL) : int.Parse(autenticacaoExterna_PortaLDAP);

                LdapConnection ldc = new LdapConnection(new LdapDirectoryIdentifier(utilizaAutenticacaoExterna_ServidorLDAP_AD, portaLDAP, true, false));

                NetworkCredential ncon = new NetworkCredential(usuario, senha);//, "cdis");
                ldc.Credential = ncon;
                ldc.AuthType = AuthType.Basic;
                ldc.SessionOptions.SecureSocketLayer = indicaSSL;
                ldc.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback((con, cer) => true);
                ldc.Bind(ncon);
            }
            catch (Exception ex)
            {
                mensagemErroAutenticacao = this.T("login_erro_autenticacao_ad") + " " + ex.Message;
                return false;
            }
            comandoSql += string.Format(" ContaWindows = '{0}' and dataExclusao is null", usuario);
        }
        else
        {
            mensagemErroAutenticacao = "(I) " + this.T("login_autenticacao_desconhecida");
            return false;
        }

        try
        {
            DataSet ds = cDados.getDataSet(comandoSql);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoUsuario = int.Parse(ds.Tables[0].Rows[0]["codigoUsuario"].ToString());
                nomeUsuario = ds.Tables[0].Rows[0]["nomeUsuario"].ToString();
                IDEstiloVisual = ds.Tables[0].Rows[0]["IDEstiloVisual"].ToString();
                mensagemErroAutenticacao = "";
                return true;
            }

            mensagemErroAutenticacao = string.Format(this.T("login_usuario_autenticado_ldap_ad_sem_cadastro_sistema"), usuario);
            return false;
        }
        catch (Exception ex)
        {
            mensagemErroAutenticacao = this.T("login_erro_autenticacao_sistema") + " " + ex.Message;
            return false;
        }
    }

    private string getDadosUnidade(string codigoUnidade)
    {
        string retorno = "";
        DataSet ds = cDados.getUnidade(" AND CodigoUnidadeNegocio = " + codigoUnidade);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString() + " (" + ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString() + ")";
        }

        return retorno;
    }

    private string geraSenha()
    {
        string numero = "";
        Random objeto = new Random();
        for (int i = 0; i < 6; i++)
            numero += objeto.Next(0, 9).ToString();
        return numero;
    }

    // ACG - Os métodos abaixo ainda não foram apagados para esperar os testes de autenticação
    private bool xx___apague_em_2015__autenticacaoExternaServidorLDAP_AD(string usuario, string senha)
    {
        string comandoSql = "Select codigoUsuario, IDEstiloVisual, NomeUsuario from usuario where ";

        if (utilizaAutenticacaoExterna_TIPO == "LDAP")
        {
            // posição do caracter "@" no campo nome do usuário.
            int posicaoArrobaNoCampoUsuario = usuario.IndexOf('@');

            string username = "uid=" + (posicaoArrobaNoCampoUsuario >= 0 ? usuario.Substring(0, posicaoArrobaNoCampoUsuario) : usuario) + ",";

            // se tem contrabarra no nome do usuário, o complemento não será utilizado, pois será feito com AD
            if (usuario.IndexOf('\\') > 0)
                username = usuario;

            // seleciona as unidades organizacionais de pesquisas
            string comandoSQL_LDAP_AD = "SELECT valor FROM ParametroConfiguracaoSistema WHERE Parametro LIKE 'LDAP_AD_DN%' ORDER BY Parametro";
            DataSet dsLDAP_AD = cDados.getDataSet(comandoSQL_LDAP_AD);
            bool autenticadoComSucesso = false;
            string mensagemErroAutenticacao = "";
            foreach (DataRow dr in dsLDAP_AD.Tables[0].Rows)
            {
                string complemento = dr["valor"].ToString().Trim();
                if (complemento == "")
                    continue;

                if (usuario.IndexOf('\\') > 0)
                    complemento = "";


                string usuarioLDAP = username + complemento;
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + utilizaAutenticacaoExterna_ServidorLDAP_AD,
                                                           usuarioLDAP,
                                                           senha, AuthenticationTypes.ServerBind);
                try
                {
                    Object obj = entry.NativeObject;
                    // se chegou aqui é por ter autenticado no LDAP com sucesso. Não precisa continuar.
                    autenticadoComSucesso = true;
                    break;
                }
                catch (Exception ex)
                {
                    mensagemErroAutenticacao += " DN = " + usuarioLDAP + ". Erro: " + ex.Message;
                }
            }

            // se deu erro em todas as tentativas de autenticação, mostra a mensagem e sai.
            if (!autenticadoComSucesso)
            {
                mensagemErroAutenticacao = this.T("login_erro_autenticacao_ldap") + "\n";// O ricardo/Simone pediu para retirar a mensagem-->  +mensagemErroAutenticacao;
                lblErro.Text = mensagemErroAutenticacao;
                return false;
            }

            // sucesso ao autenticar o usuário no ldap/ad. Agora vamos verificar se ele existe no banco do portal.
            // se não é usuário de domínio (não tem contrabarra)
            if (usuario.IndexOf('\\') < 0)
            {
                // se não digitou o caracter "@", vamos incluí-lo
                if (posicaoArrobaNoCampoUsuario < 0)
                    usuario += "@" + utilizaAutenticacaoExterna_DOMINIO;
                comandoSql += string.Format(" email = '{0}' and dataExclusao is null", usuario);
            }
            else // usuário de domínio, vamos pesquisar com o campo contaWindows
            {
                comandoSql += string.Format(" ContaWindows = '{0}' and dataExclusao is null", usuario);
            }
        }
        else if (utilizaAutenticacaoExterna_TIPO == "AD")
        {
            try
            {
                LdapConnection ldc = new LdapConnection(new LdapDirectoryIdentifier(utilizaAutenticacaoExterna_ServidorLDAP_AD, 389, true, false));

                NetworkCredential ncon = new NetworkCredential(usuario, senha);//, "cdis");
                ldc.Credential = ncon;
                ldc.AuthType = AuthType.Basic;
                ldc.Bind(ncon);

            }
            catch (Exception ex)
            {
                lblErro.Text = this.T("login_erro_autenticacao_ad") + " " + ex.Message;
                return false;
            }
            comandoSql += string.Format(" ContaWindows = '{0}' and dataExclusao is null", usuario);
        }

        try
        {
            DataSet ds = cDados.getDataSet(comandoSql);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoUsuarioExterno = int.Parse(ds.Tables[0].Rows[0]["codigoUsuario"].ToString());
                nomeUsuario = ds.Tables[0].Rows[0]["nomeUsuario"].ToString();
                IDEstiloVisual = ds.Tables[0].Rows[0]["IDEstiloVisual"].ToString();
                return true;
            }

            lblErro.Text = string.Format(this.T("login_usuario_autenticado_ldap_ad_sem_cadastro_sistema"), usuario);
            return false;
        }
        catch (Exception ex)
        {
            lblErro.Text = this.T("login_erro_autenticacao_sistema") + " " + ex.Message;
            return false;
        }
    }
    private bool xx___autenticacaoExternaOutroBanco(string nomeUsuario, string senha)
    {
        // =================================================================================================================
        // ATENÇÃO: Esta função esta funcionando EXCLUSIVAMENTE para atender a autenticação da PRODABEL que é feita no MySQL
        // =================================================================================================================
        string Servidor = "";
        string Porta = "";
        string Banco = "";
        string UsuarioAcessoBanco = "";
        string SenhaAcessoBanco = "";
        string Query = "";

        // busca os parametros de acesso ao banco de dados externo. Este parametros devem estar associados a entidade 1
        string comandoSql = string.Format(
         @"SELECT parametro, valor 
                FROM ParametroConfiguracaoSistema 
               WHERE CodigoEntidade = 1
                 AND Parametro like 'AutenticaoExternaBD_%'
               ORDER BY Parametro");

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds))
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row["parametro"] + "" == "AutenticaoExternaBD_Servidor")
                    Servidor = row["valor"].ToString();
                else if (row["parametro"] + "" == "AutenticaoExternaBD_Porta")
                    Porta = row["valor"].ToString();
                else if (row["parametro"] + "" == "AutenticaoExternaBD_Banco")
                    Banco = row["valor"].ToString();
                else if (row["parametro"] + "" == "AutenticaoExternaBD_Usuario")
                    UsuarioAcessoBanco = row["valor"].ToString();
                else if (row["parametro"] + "" == "AutenticaoExternaBD_Senha")
                    SenhaAcessoBanco = row["valor"].ToString();
                else if (row["parametro"] + "" == "AutenticaoExternaBD_Query")
                    Query = row["valor"].ToString();
            }
        }

        // tem de existir no mínimo 6 parametros para autenticação externa
        if (!cDados.DataSetOk(ds) || ds.Tables[0].Rows.Count < 6)
        {
            lblErro.Text = this.T("login_parametros_autenticacao_externa_nao_definidos");
            return false;
        }

        // verifica se o parametro query retornou valor
        if (Query == "")
        {
            lblErro.Text = string.Format(this.T("login_parametro_nao_foi_definido"), "\"AutenticaoExternaBD_Query\"");
            return false;
        }

        // monta a string de conexao
        string connStr = string.Format("server={0};port={1};user={2};password={3};database={4}", Servidor, Porta, UsuarioAcessoBanco, SenhaAcessoBanco, Banco);
        using (MySql.Data.MySqlClient.MySqlConnection bdConn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
        {
            // a senha da prodabel é invertida depois de criptografada
            string senhaCripto = senha;
            senhaCripto = EncodeTo64(senhaCripto);
            senhaCripto = inverter(senhaCripto);

            string nomeUsuarioExterno = nomeUsuario;
            if (nomeUsuarioExterno.IndexOf('@') > 0)
                nomeUsuarioExterno = nomeUsuarioExterno.Substring(0, nomeUsuarioExterno.IndexOf('@'));

            // monta a query para verificar a existência do usuário
            Query = string.Format(Query, nomeUsuarioExterno, senhaCripto);

            //tenta abrir uma conexão com o mySql
            try
            {
                bdConn.Open();
                MySql.Data.MySqlClient.MySqlCommand oCmd = new MySql.Data.MySqlClient.MySqlCommand(Query, bdConn);
                oCmd.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;

                // Cria um DataAdapter, com base no comando SQL fornecido.
                MySql.Data.MySqlClient.MySqlDataAdapter dataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(oCmd);
                DataSet bdDataSet = new DataSet();

                // Preenche  o DataSet, com as informações contidas no dataAdapter.
                dataAdapter.Fill(bdDataSet, "rs");

                // se retornou algum registro... só pode retornar 1 registro
                DataTable dt = bdDataSet.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    lblErro.Text = this.T("login_usuario_nao_encontrado");
                    return false;
                }

                // se retornou algum registro... só pode retornar 1 registro
                if (dt.Rows.Count > 1)
                {
                    lblErro.Text = this.T("login_banco_dados_retornando_varios_registros");
                    return false;
                }

                bdConn.Close();
            }
            catch (Exception ex)
            {
                lblErro.Text = this.T("login_erro_conexao_banco_dados_autenticacao") + ": " + ex.Message;
                return false;
            }
            finally
            {
                bdConn.Dispose();
            }
        }

        // sucesso ao autenticar o usuário no banco externo. Agora vamos verificar se ele existe no banco do portal.
        comandoSql = string.Format("Select codigoUsuario from usuario where email = '{0}' and dataExclusao is null", nomeUsuario);
        ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoUsuarioExterno = int.Parse(ds.Tables[0].Rows[0]["codigoUsuario"].ToString());
            return true;
        }

        lblErro.Text = this.T("login_usuario_autenticado_externo_sem_cadastro_sistema");
        return false;
    }
    private int xx___autenticacaoIntegradaViaIIS(string usuario, string senha, string dominio)
    {
        try
        {
            string url_IIS = Request.Url.AbsoluteUri.Replace("login", "autenticacaoIntegradaViaAplicacaoExterna");
            WebRequest request = WebRequest.Create(url_IIS);
            request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            //request.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            request.Credentials = new NetworkCredential(usuario, senha, dominio);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string str = reader.ReadLine();
            while (str != null)
            {
                if (str.IndexOf("lblRetornoIIS") >= 0)
                {
                    string retornoIIS = str.Substring(str.IndexOf("lblRetornoIIS") + 15);
                    retornoIIS = retornoIIS.Substring(0, retornoIIS.IndexOf('<'));
                    string[] aRetornoIIS = retornoIIS.Split(';');
                    if (aRetornoIIS[0] == "OKCDIS")
                    {
                        return int.Parse(aRetornoIIS[1]);
                    }
                }
                str = reader.ReadLine();
            }
            return -1;
        }
        catch
        {
            //MessageBox.Show(ex.Message);
            return -1;
        }
    }



    protected void btnOkApresentacaoAcao_Click(object sender, EventArgs e)
    {

    }

    protected void btnEnviarEsqueciSenha_Click(object sender, EventArgs e)
    {
        string urlEsqueciSenha = baseUrl + "/login.aspx?key=";
        ResultRequestDomain result = UowApplication.UowService.GetUowService<UsuarioEsqueceuSenhaService>().SolicitarEsqueciSenha(txtEmailEsqueciSenha.Text, urlEsqueciSenha);

        string msg = "";    
        string nomeImg = "";
        if (result.IsSuccess)
        {
            msg = Resources.traducao.Solicitacao_Enviada_Verifique_Seu_Email;
            nomeImg = "sucesso";
        }
        else
        {
            //if (result.IsException)
            //{
            //    throw new Exception(result.Message);
            //}
            msg = result.IsException ? result.Message : Resources.traducao.login_erro_autenticacao_sistema;
            nomeImg = result.IsException ? "erro" : "Atencao";
        }

        Session["Brisk:Msg"] = msg;
        Session["Brisk:TipoMsg"] = nomeImg;
        Page.Response.Redirect("login.aspx", true);
    }

    protected void btnSalvarNovaSenha_Click(object sender, EventArgs e)
    {
        if (txtNovaSenha.Text == txtConfirmaNovaSenha.Text)
        {
            int hashSenha = cDados.ObtemCodigoHash(txtNovaSenha.Text);
            ResultRequestDomain result = UowApplication.UowService.GetUowService<UsuarioService>().SalvarNovaSenha(chave, hashSenha);
            string msg = "";
            string nomeImg = "";
            if (result.IsSuccess)
            {
                msg = Resources.traducao.opera__o_realizada;
                nomeImg = "sucesso";
            }
            else
            {
                if (result.IsException)
                {
                    throw new Exception(result.Message);
                }
                msg = result.IsException ? result.Message : Resources.traducao.usu_rio_sem_acesso;
                nomeImg = result.IsException ? "erro" : "Atencao";
            }

            Session["Brisk:Msg"] = msg;
            Session["Brisk:TipoMsg"] = nomeImg;
            Page.Response.Redirect("login.aspx", true);
        }
        else
        {
            string msg = Resources.traducao.As_Senhas_Nao_Coincidem;
            MostrarMensagem(msg, "Atencao");
        }
    }

    public void MostrarMensagem(string msg, string nomeImg)
    {
        imgApresentacaoAcao.ImageUrl = cDados.getPathSistema() + "imagens/" + nomeImg + ".png";
        imgApresentacaoAcao.Visible = true;
        lblMensagemApresentacaoAcao.Text = msg;
        btnOkApresentacaoAcao.Visible = true;
        btnCancelarApresentacaoAcao.Visible = false;
        pcApresentacaoAcao.ShowOnPageLoad = true;
    } 
}

