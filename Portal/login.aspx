<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v19.1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta name="viewport" content="width=device-width,initial-scale=1" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <link href="estilos/custom.css" rel="stylesheet" type="text/css" />
    <link href="Bootstrap/css/styleLogin.css" rel="stylesheet" />

    <!-- Style Bootstrap -->
    <link href="Bootstrap/vendor/bootstrap/v4.0.0/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Bootstrap/vendor/fontawesome/v5.0.12/css/fontawesome-all.min.css" rel="stylesheet" />

    <!-- Script Bootstrap -->
    <script src="Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js"></script>
    <script src="Bootstrap/vendor/bootstrap/v4.0.0/js/bootstrap.min.js"></script>
    <script src="/Scripts/dist/browser/signalr.js"></script>
    <script src="/Scripts/HubNotification.js"></script>


    <script type="text/javascript" language="javascript">   

        function replaceAll(origem, antigo, novo) {
            var teste = 0;
            while (teste == 0) {
                if (origem.indexOf(antigo) >= 0) {
                    origem = origem.replace(antigo, novo);
                }
                else
                    teste = 1;
            }
            return origem;
        }

        var eventoOKMsg = null;

        function mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK) {

            if (nomeImagem != null && nomeImagem != '')
                imgApresentacaoAcao.SetImageUrl(pcApresentacaoAcao.cp_Path + 'imagens/' + nomeImagem + '.png');
            else
                imgApresentacaoAcao.SetVisible(false);

            //textoMsg = replaceAll(textoMsg, '\n', '<br/>');

            lblMensagemApresentacaoAcao.SetText(textoMsg);
            btnOkApresentacaoAcao.SetVisible(mostraBtnOK);
            btnCancelarApresentacaoAcao.SetVisible(mostraBtnCancelar);
            pcApresentacaoAcao.Show();
            eventoOKMsg = eventoOK;

            if (!mostraBtnOK && !mostraBtnCancelar) {
                setTimeout('fechaMensagem();', 3500);
            }
        }

        function fechaMensagem() {
            lblMensagemApresentacaoAcao.SetText('');

            pcApresentacaoAcao.AdjustSize();
            pcApresentacaoAcao.Hide();
        }
        function somenteNumero(e, aceitaPontoOuVirgula) {

            var tecla = (window.event) ? event.keyCode : e.which;
            if ((tecla > 47 && tecla < 58)) {
                return true;
            }
            else if (aceitaPontoOuVirgula && ((tecla == 46) || (tecla == 44))) {
                return true;
            }
            else {
                if (tecla != 8) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        var connSignalRnewBrisk = null;
        var OnConnectedSignalRnewBrisk;

        function realizaConexaoSignalRLogin(token) {
            if (connSignalRnewBrisk) {
                // Já existe uma conexão estabelecida, não é necessário criar uma nova
                return;
            }

            sessionStorage.setItem('isLogin', 'S');
            
            const queryParams = `?login=${true}` ;


            connSignalRnewBrisk = new signalR.HubConnectionBuilder()
                .withUrl(`${urlWSnewBriskBase}/hubs/notification/${queryParams}`, { accessTokenFactory: async () => token })
                .configureLogging(signalR.LogLevel.Information)
                .build();

            let messageReceived = false;

            async function start() {
                try {
                    await connSignalRnewBrisk.start();
                    console.log("SignalR Connected on realizaConexaoSignalR().");
                } catch (err) {
                    lpLoading.Hide();
                    console.log(err);
                    setTimeout(start, 5000);
                }
            };

            connSignalRnewBrisk.on("licenseIssue_sce", function (userNameEconnectionId) {
                connSignalRnewBrisk.stop();
                connSignalRnewBrisk = null;
                localStorage.clear();
                document.getElementById('painelLogin_lblErro').innerText = traducao.avisoDesconexao_todas_as_licen_as_contratadas_para_a_sua_entidade_padr_o_j__est_o_em_uso_no_momento__tente_novamente_mais_tarde_ou_entre_em_contato_com_o_administrador_do_sistema;
                lpLoading.Hide();
                messageReceived = true; // Marcar que uma mensagem foi recebida

            });

            connSignalRnewBrisk.on("SuccessConnected", function (userNameEconnectionId) {
                connSignalRnewBrisk.stop();
                connSignalRnewBrisk = null;
                window.location.href = "index.aspx";
                messageReceived = true; // Marcar que uma mensagem foi recebida

            });

            start();

                
                const timeoutDuration = 20000; // 20 segundos
                const timeoutId = setTimeout(() => {
                if (!messageReceived) {
                    connSignalRnewBrisk.stop();
                    connSignalRnewBrisk = null;
                    document.getElementById('painelLogin_lblErro').innerText = traducao.login_falha_validacao_credenciais.replace('{{0}}', '003');;
                    lpLoading.Hide();
                }
            }, timeoutDuration);
        }


        function conectaSignalRLogin(tokenAcessoNewBrisk) {
            try {
                var token = getTokenNBRFromMemory(tokenAcessoNewBrisk);
                realizaConexaoSignalRLogin(token);
            }
            catch (err) {
                lpLoading.Hide();
                document.getElementById('painelLogin_lblErro').innerText = traducao.login_falha_validacao_credenciais.replace('{{0}}', '005');
                console.log(err);
            }
        }
    </script>


    <title>Brisk PPM</title>
    <style type="text/css">        
          
        .auto-style1 {
            height: 40px;
        }

        .negativeBar {
            background-color: #E8E8E8;
        }

        .pwdBlankBar .positiveBar {
            width: 0%;
        }

        .pwdBlankBar .negativeBar {
            width: 100%;
        }

        .pwdWeakBar .positiveBar {
            background-color: Red;
            width: 30%;
        }

        .pwdWeakBar .negativeBar {
            width: 70%;
        }

        .pwdFairBar .positiveBar {
            background-color: #FFCC33;
            width: 65%;
        }

        .pwdFairBar .negativeBar {
            width: 35%;
        }

        .pwdStrengthBar .positiveBar {
            background-color: Green;
            width: 100%;
        }

        .pwdStrengthBar .negativeBar {
            width: 0%;
        }
    </style>
    <link rel="icon" href="imagens/Brisk-Ico.ico" />
    <!--[if IE]>
    <style type="text/css">

        input[type='text'],
        input[type='password'],
        input[type='number'],
        select
        {
            min-height: 22px !important;
        }

    </style>
<![endif]-->

    <style type="text/css">
        input[type='text'],
        input[type='password'],
        input[type='number'],
        select {
            min-height: 22px !important;
        }
    </style>

    <!-- Script -->
    <script type="text/javascript" src="scripts/custom/util/linqtoo.min.js"></script>
    <script type="text/javascript" src="scripts/HubNotification.js"></script>

    <script type="text/javascript">


        var minPwdLength = 3;
        var strongPwdLength = 6;

       

        function ControlarBtnSalvarNovaSenha() {
            if (getPassValid()) {
                btnSalvarNovaSenha.SetEnabled(true);
            } else {
                btnSalvarNovaSenha.SetEnabled(false);
            }
        }

        function UpdateIndicator() {
            ControlarBtnSalvarNovaSenha();
            var strength = GetPasswordStrength(txtNovaSenha.GetText());

            var className;
            var message;
            if (strength == -1) {
                className = 'pwdBlankBar';
                message = getTraducao('Vazia');
            } else if (strength == 0) {
                className = 'pwdBlankBar';
                message = getTraducao('Curta_Demais');
            } else if (strength <= 0.4) {
                className = 'pwdWeakBar';
                message = getTraducao('Fraca');
            } else if (strength <= 0.8) {
                className = 'pwdFairBar';
                message = getTraducao('Razoavel');
            } else {
                className = 'pwdStrengthBar';
                message = getTraducao('Forte');
            }

            // update css and message
            var bar = document.getElementById("PasswordStrengthBar");
            bar.className = className;
            lbMessagePassword.SetValue(message);
        }
        function GetPasswordStrength(password) {
            if (password.length == 0) return -1;
            if (password.length < minPwdLength) return 0;

            var rate = 0;
            if (password.length >= strongPwdLength) rate++;
            if (password.match(/[0-9]/)) rate++;
            if (password.match(/[a-z]/)) rate++;
            if (password.match(/[A-Z]/)) rate++;
            if (password.match(/[!,@,#,$,%,^,&,*,?,_,~,\-,(,),\s,\[,\],+,=,\,,<,>,:,;]/)) rate++;
            return rate / 5;
        }

        function getPassValid() {
            return (txtNovaSenha.GetValue() == txtConfirmaNovaSenha.GetValue()) && (txtNovaSenha.GetValue() != null && txtNovaSenha.GetValue().length >= 6);
        }

        function isInvalidEmail(email) {            
            var pattern = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;                        
            return (email.match(pattern));
        }
        
        function verifyEmailEsqueciSenha() {            
            if (isInvalidEmail(txtEmailEsqueciSenha.GetText())) {
                btnEnviarEsqueciSenha.SetEnabled(true);
            } else {
                btnEnviarEsqueciSenha.SetEnabled(false);
            }
        }

        function ConfirmedPassword_OnValidation(s, e) {
            ControlarBtnSalvarNovaSenha();
        }

        $(function () {
            var rand = Math.floor(Math.random() * 5) + 0;
            switch (rand) {
                case 0:
                    $('#sidebar').addClass("sidebar-green");
                    $('#sidebarTitulo').html(traducao.login_espa_o_de_trabalho);
                    $('form.login').css('border-top', '4px solid #79ACA5');

                    break;
                case 1:
                    $('#sidebar').addClass("sidebar-orange");
                    $('#sidebarTitulo').html(traducao.login_estrat_gia);
                    $('form.login').css('border-top', '4px solid #C25127');

                    break;
                case 2:
                    $('#sidebar').addClass("sidebar-blue");
                    $('#sidebarTitulo').html(traducao.login_projetos_e_portf_lios);
                    $('form.login').css('border-top', '4px solid #515E6E');
                    break;
                case 3:
                    $('#sidebar').addClass("sidebar-yellow");
                    $('#sidebarTitulo').html(traducao.login_administra__o);
                    $('form.login').css('border-top', '4px solid #E2C162');
                    break;

                case 4:
                    $('#sidebar').addClass("sidebar-grey");
                    $('#sidebarTitulo').html(traducao.login_mobilidade);
                    $('form.login').css('border-top', '4px solid #E2C162');
                    break;
            }

        });

        window.onload = function () {
            // Desabilita o controle ASPxCaptcha
            sessionStorage.removeItem('listaNotificacoes');
        };
    </script>

</head>
<body id="login">
    <div class="container-fluid">
        <div class="row responsive-row">
            <div class="col-sm-4 responsive-960">
                <div id="sidebar">
                    <a class="logo" href="#">
                        <img src="Bootstrap/images/logo-brisk-login.png" class="img-fluid logoBrisk" alt="<%# Resources.traducao.login_logomarca_brisk %>" />
                    </a>

                    <div class="text-sub-title">
                        <a href="#" id="sidebarTitulo" title="<%# Resources.traducao.login_estrat_gia %>"><asp:Literal runat="server" Text="<%$ Resources:traducao, login_estrat_gia %>" /></a>
                    </div>

                </div>
            </div>

            <div class="col-sm-8 responsive-medium responsive-larger">

                <div class="logoBrisk-responsive">
                    <a class="logo" href="#">
                        <img src="Bootstrap/images/logo-brisk-login.png" class="img-fluid logoBrisk" alt="<%# Resources.traducao.login_logomarca_brisk %>" />
                    </a>
                </div>

                <div>
                    <form id="form1" runat="server" enableviewstate="false">
                        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral"></dxhf:ASPxHiddenField>

                        <dx:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
                        </dx:ASPxLoadingPanel>

                        <table cellpadding="0" cellspacing="0" height="100%" id="conteudoLogin" width="100%">


                            <%--                        <a class="logo-541" href="#">
                                <img src="Bootstrap/images/logo-brisk-login.png" class="img-fluid logoBrisk" alt="<%# Resources.traducao.login_logomarca_brisk %>" />
                            </a>--%>

                            <tr>
                                <td align="center" valign="middle">
                                    <dxrp:ASPxRoundPanel ID="painelLogin" CssClass="login" runat="server"
                                        DefaultButton="btnAcessar" HeaderText="">
                                        <ContentPaddings />

                                        <HeaderStyle VerticalAlign="Middle">

                                            <Paddings />
                                        </HeaderStyle>
                                        <PanelCollection>

                                            <dxp:PanelContent ID="PanelContent1" runat="server">


                                                <div id="divAuth" runat="server">
                                                    <table cellpadding="0" cellspacing="0" width="93%">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" Text="<%# Resources.traducao.login_aten__231___227_o__caps_lock_ligado_ %>"
                                                                        ForeColor="Red" ID="aviso_caps_lock" Style="visibility: hidden"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="alert-erro" Visible="true"
                                                                        ID="lblErro"></asp:Label>

                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>

                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>

                                                            <tr>
                                                                <td>
                                                                    <div id="cadeadoLogin">
                                                                        <%--                                                                    <dxe:ASPxImage ID="ASPxImageCadeadoLogin" runat="server"
                                                                        ClientInstanceName="imgCadeadoLogin" ImageUrl="~/imagens/login/cadeado-login.png">
                                                                    </dxe:ASPxImage>--%>


                                                                        <div style="margin-left:2rem;">


                                                                            <svg class="svgFont" aria-hidden="true" data-prefix="fas" data-icon="lock" class="svg-inline--fa fa-lock fa-w-14" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                                                                                <path fill="currentColor" d="M400 224h-24v-72C376 68.2 307.8 0 224 0S72 68.2 72 152v72H48c-26.5 0-48 21.5-48 48v192c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48V272c0-26.5-21.5-48-48-48zm-104 0H152v-72c0-39.7 32.3-72 72-72s72 32.3 72 72v72z"></path>

                                                                            </svg>




                                                                            <span class="loginTextLock"><asp:Literal runat="server" Text="<%$ Resources:traducao, login_autenticacao %>" /></span>
                                                                        </div>


                                                                    </div>



                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tbody>

                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" id="formularioLogin" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <dxe:ASPxTextBox ID="txtUsuario" runat="server" ClientInstanceName="txtUsuario" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                                                                    Width="100%" CssClass="inputEmail">
                                                                                                                </dxe:ASPxTextBox>
                                                                                                            </td>
                                                                                                            <%--                                                                                                        <td>
                                                                                                            <span id="spanhelp">
                                                                                                                <dxe:ASPxImage ID="imgHelpUsuario" runat="server"
                                                                                                                    ClientInstanceName="imgHelpUsuario" ImageUrl="~/imagens/login/info-login.png">
                                                                                                                </dxe:ASPxImage>
                                                                                                            </span>
                                                                                                        </td>--%>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <dxe:ASPxTextBox ID="txtSenha" runat="server" ClientInstanceName="txtSenha" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                                                                    Width="100%" Password="True" CssClass="inputPassword" UseValueAsPassword="true">
                                                                                                                </dxe:ASPxTextBox>
                                                                                                            </td>
                                                                                                            <td></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <dx:ASPxCallbackPanel runat="server" ClientInstanceName="callbackSenha" ID="callbackSenha" OnCallback="callbackSenha_Callback">
                                                                                                        <ClientSideEvents EndCallback="function(s, e) {

                                                                                      document.getElementById('painelLogin_lblErro').innerText = s.cp_Erro;
                                                                                       if(s.cp_tokenAcessoNewBrisk == undefined || s.cp_tokenAcessoNewBrisk == null || s.cp_tokenAcessoNewBrisk == '')
                                                                                       {
                                                                                             lpLoading.Hide();
                                                                                       }                                                                                                            
                                                                                       else 
                                                                                       {
                                                                                            urlWSnewBriskBase = s.cp_urlBase;
                                                                                            conectaSignalRLogin(s.cp_tokenAcessoNewBrisk);
                                                                                       }
                                                                                      
                                                                                }"></ClientSideEvents>
                                                                                                        <PanelCollection>
                                                                                                            <dxcp:PanelContent runat="server">
                                                                                                                <dx:ASPxCaptcha
                                                                                                                    ID="Captcha"
                                                                                                                    runat="server"
                                                                                                                    CharacterSet="abcdefhjklmnpqrstuvxyz23456789"
                                                                                                                    CodeLength="6"
                                                                                                                    Visible="False"
                                                                                                                    TextBox-Position="Bottom" ClientInstanceName="captcha">

                                                                                                                    <TextBox Position="Bottom"></TextBox>

                                                                                                                    <ChallengeImage
                                                                                                                        BackgroundColor="#4c7724"
                                                                                                                        ForegroundColor="#FFFFFF"
                                                                                                                        BorderWidth="0"
                                                                                                                        FontFamily="Georgia">
                                                                                                                    </ChallengeImage>
                                                                                                                    <ValidationSettings SetFocusOnError="true" ErrorDisplayMode="Text" />
                                                                                                                </dx:ASPxCaptcha>
                                                                                                            </dxcp:PanelContent>
                                                                                                        </PanelCollection>
                                                                                                    </dx:ASPxCallbackPanel>
                                                                                                </td>
                                                                                            </tr>

                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td align="center">

                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="false" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                                                        ID="btnAcessar"  CssClass="btnEnterLogin ">
                                                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                                        console.log('acionou o botao entrar');
                                                                                                            lpLoading.Show();

callbackSenha.PerformCallback();                                                               
                                                             var urlWSnewBriskBase = hfGeral.Get('urlWSnewBriskBase');
                                                             localStorage.setItem('BRISK-USER', txtUsuario.lastChangedValue);

}" />
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>

                                                                                                <td align="center" style="display: none;">
                                                                                                    <dxe:ASPxButton runat="server" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                                                        ID="btnAlterar" OnClick="Button2_Click">
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td align="center" style="display: none;">
                                                                                                    <dxe:ASPxButton runat="server" Text="Login Integrado" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                                                        ID="btnLoginIntegrado" OnClick="btnLoginIntegrado_Click">
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" id="esqueciSenhaLogin" width="100%">
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td class="loginBloco">

                                                                                            </td>
                                                                                            <td class="loginBloco">
                                                                                                <dxe:ASPxHyperLink ID="hplEsqueciSenha" runat="server" Cursor="pointer"
                                                                                                    CssClass="text-grey">
                                                                                                    <ClientSideEvents Click="function(s, e) {	
                                                                                                    $('#painelLogin_divAuth').fadeOut(function(){
                                                                                                        $('#painelLogin_divEsqueciSenha').fadeIn();                                                                                                    
                                                                                                    });
                                                                                                    
}" />
                                                                                                </dxe:ASPxHyperLink>


                                                                                            </td>
                                                                                            <td class="loginBloco">
                                                                                                <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Cursor="pointer"
                                                                                                    Text="Login integrado" CssClass="text-green">
                                                                                                    <ClientSideEvents Click="function(s, e) {
	window.location.href = './po_autentica.aspx?' + window.location.search;
}" />
                                                                                                </dxe:ASPxHyperLink>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>




                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="display: none;">
                                                                    <asp:Label runat="server" Text="<%# Resources.traducao.login_licenciado_para_ %>" ID="lblLicenciado"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Label runat="server" ID="lblLicenca"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td class="rodape" style="display: none;">
                                                                    <dxcp:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" ImageUrl="~/imagens/anexo/pasta.png" ShowLoadingImage="True" ToolTip="Acessar pastas públicas">
                                                                        <ClientSideEvents Click="function(s, e) {
	                window.open('./_Public/Anexos.aspx', '_self');
                }" />
                                                                    </dxcp:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>

                                                <div id="divEsqueciSenha" runat="server">
                                                    <div style="margin-left:1.4rem;padding-bottom: .5rem;">                                                                                                                                    
				                                           <span class="loginTextLock"><asp:Literal ID="ltrEsqueceuSenha" runat="server" /></span>                                                                        
                                                     </div>

                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <div style="padding-bottom: .5rem;">
                                                                    <dxe:ASPxTextBox
                                                                        ID="txtEmailEsqueciSenha"
                                                                        runat="server"
                                                                        Width="100%"
                                                                        CssClass="inputEmail"
                                                                        ClientInstanceName="txtEmailEsqueciSenha"
                                                                        CssPostfix="MaterialCompact"
                                                                        CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                                                                        <ClientSideEvents  KeyUp="verifyEmailEsqueciSenha" />
                                                                    </dxe:ASPxTextBox>
                                                                </div>

                                                            </td>
                                                        </tr>
                                                        <tr >
                                                            <td>
                                                                <div style="padding-bottom: .5rem;">
                                                                    <dxe:ASPxButton runat="server" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                        ID="btnEnviarEsqueciSenha" ClientInstanceName="btnEnviarEsqueciSenha" OnClick="btnEnviarEsqueciSenha_Click" CssClass="btnEnterLogin ">
                                                                        <Paddings Padding="0px"></Paddings>
                                                                    </dxe:ASPxButton>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr style="margin-left:1.4rem;">
                                                            <td>
                                                                <dxe:ASPxButton runat="server" AutoPostBack="false" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                    ID="btnCancelarEsqueciSenha" CssClass="btnEnterLogin btnLoginCancelarCor">
                                                                    <ClientSideEvents Click="function(s, e) {	                                                                                
                                                                                $('#painelLogin_divEsqueciSenha').fadeOut(function(){
                                                                                    txtEmailEsqueciSenha.SetText('');
                                                                                    $('#painelLogin_divAuth').fadeIn();
                                                                                });                                                                                                                                                                                                                                                                        
                                                                              }" />
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                                <div id="divRedefinirSenha" runat="server">
                                                    <div style="margin-left:1.4rem;padding-bottom: .5rem;">                                                                                                                                    
				                                           <span class="loginTextLock"><asp:Literal ID="ltrTituloRedefinirSenha" runat="server" /></span>
                                                    </div>

                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <div>
                                                                    <dxe:ASPxTextBox
                                                                        ID="txtNovaSenha"
                                                                        runat="server"
                                                                        ClientInstanceName="txtNovaSenha"
                                                                        CssPostfix="MaterialCompact"
                                                                        CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                        Width="100%"
                                                                        Password="True"
                                                                        CssClass="inputEmail">
                                                                        <ClientSideEvents KeyUp="UpdateIndicator" TextChanged="UpdateIndicator" />
                                                                    </dxe:ASPxTextBox>

                                                                    <div class="inputEmail">
                                                                        <dx:ASPxLabel ID="lbMessagePassword" ClientInstanceName="lbMessagePassword" runat="server">
                                                                        </dx:ASPxLabel>
                                                                        <table id="PasswordStrengthBar" class="pwdBlankBar" style="height:10px;" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td id="PositiveBar" class="positiveBar"></td>
                                                                                    <td id="NegativeBar" class="negativeBar"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxTextBox
                                                                    ID="txtConfirmaNovaSenha"
                                                                    runat="server"
                                                                    ClientInstanceName="txtConfirmaNovaSenha"
                                                                    CssPostfix="MaterialCompact"
                                                                    CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                    Width="100%"
                                                                    Password="True"
                                                                    CssClass="inputPassword">
                                                                    <ClientSideEvents KeyUp="ConfirmedPassword_OnValidation" />
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <div style="padding-bottom: .5rem;">
                                                                    <dxe:ASPxButton
                                                                        runat="server"
                                                                        CssPostfix="MaterialCompact"
                                                                        CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                        ID="btnSalvarNovaSenha"
                                                                        ClientInstanceName="btnSalvarNovaSenha"
                                                                        OnClick="btnSalvarNovaSenha_Click"
                                                                        CssClass="btnEnterLogin">
                                                                    </dxe:ASPxButton>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr style="margin-left:1.4rem;">
                                                            <td>
                                                                <dxe:ASPxButton runat="server" AutoPostBack="false" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                                    ID="btnCancelarRedefinirSenha" CssClass="btnEnterLogin btnLoginCancelarCor">
                                                                    <ClientSideEvents Click="function(s, e) {	   
                                                                                window.location.href = './login.aspx'                                                                                                                                                                                                                                                                       
                                                                              }" />
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <%--  --%>
                                            </dxp:PanelContent>

                                        </PanelCollection>
                                    </dxrp:ASPxRoundPanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">

                                    <dxpc:ASPxPopupControl ID="popupHelpUsuario" runat="server" ClientInstanceName="popupHelpUsuario"
                                        Width="460px" HeaderText="" PopupElementID="spanhelp"
                                        PopupHorizontalAlign="Center" PopupHorizontalOffset="100" PopupVerticalAlign="Middle" PopupVerticalOffset="-60"
                                        Modal="True">
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, o_usu_rio_a_ser_informado_dever__corresponder_ao_email_completo %>" />
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                        <HeaderImage Url="~/imagens/ajuda.png"></HeaderImage>
                                    </dxpc:ASPxPopupControl>


                                    <dxtv:ASPxPopupControl ID="pcApresentacaoAcao" runat="server" ClientInstanceName="pcApresentacaoAcao" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Modal="True" CloseAction="None" Width="460px">
                                        <ClientSideEvents Closing="function(s, e) {
	lblMensagemApresentacaoAcao.SetText('');
}" />
                                        <ContentCollection>
                                            <dxtv:PopupControlContentControl runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td class="auto-style1">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxtv:ASPxImage ID="imgApresentacaoAcao" runat="server" ClientInstanceName="imgApresentacaoAcao" ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png" Height="40px" CssClass="mr-10">
                                                                            </dxtv:ASPxImage>
                                                                        </td>
                                                                        <td>
                                                                            <dxtv:ASPxLabel ID="lblMensagemApresentacaoAcao" runat="server" ClientInstanceName="lblMensagemApresentacaoAcao" EncodeHtml="False" Wrap="False">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>

                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxtv:ASPxButton ID="btnOkApresentacaoAcao" runat="server" AutoPostBack="False" CssClass="ml-6" ClientInstanceName="btnOkApresentacaoAcao" Text="OK"  OnClick="btnOkApresentacaoAcao_Click" HorizontalAlign="center">
                                                                            <ClientSideEvents Click="function(s, e) {
	if(eventoOKMsg != null &amp;&amp; eventoOKMsg != '')
		eventoOKMsg();
	fechaMensagem();
}" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxButton ID="btnCancelarApresentacaoAcao" runat="server" AutoPostBack="False" Text="Cancelar"  ClientInstanceName="btnCancelarApresentacaoAcao">
                                                                            <ClientSideEvents Click="function(s, e) {
	fechaMensagem();
}" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxtv:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxtv:ASPxPopupControl>

                                </td>
                            </tr>
                        </table>
                    </form>
                </div>


                <div class="loginFooter">

                    <div class="titleLicenciado">
                        <asp:Literal runat="server" Text="<%$ Resources:traducao, login_licenciado_para_ %>" /> CDIS Informática Ltda
                    </div>
                    <div class="titlePastaPublica">
                        <a href="#" onClick="window.open('./_Public/Anexos.aspx', '_self')"><asp:Literal runat="server" Text="<%$ Resources:traducao, login_pasta_p_blica %>" /></a>

                    </div>
                    <div class="imgPasta">
                        <dxcp:ASPxImage ID="ASPxImage2" runat="server" Cursor="pointer" ImageUrl="~/imagens/anexo/pasta.png" ShowLoadingImage="True" ToolTip="Acessar pastas públicas">
                            <ClientSideEvents Click="function(s, e) {
		                            window.open('./_Public/Anexos.aspx', '_self');
		                            }" />
                        </dxcp:ASPxImage>



                    </div>

                </div>

            </div>
        </div>
    </div>


    <script type="text/javascript">
        var jsonTraducao = JSON.parse('<%=jsonTraducao%>');

        var getTraducao = function (nameKey) {
            if (jsonTraducao == null) {
                return "No translation";
            } else {
                var itemTraducao = jsonTraducao.Where(function (item) { return item.Key == nameKey; }).First().Text;
                return itemTraducao;
            }
        }

        var isRedefinirSenha = <%=isRedefinirSenha.ToString().ToLower()%>;
        if (isRedefinirSenha) {
            btnSalvarNovaSenha.SetEnabled(false);
        }

        btnEnviarEsqueciSenha.SetEnabled(false);        
        sessionStorage.removeItem("taNBR");

        sessionStorage.removeItem('listaNotificacoes');
    </script>
</body>
</html>
