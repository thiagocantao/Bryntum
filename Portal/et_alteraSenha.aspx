<%@ Page Language="C#" AutoEventWireup="true" CodeFile="et_alteraSenha.aspx.cs" Inherits="et_alteraSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="estilos/cdisEstilos.css" rel="stylesheet" />

    <link href="estilos/custom.css" rel="stylesheet" type="text/css" />
    <link href="Bootstrap/css/styleLogin.css" rel="stylesheet" />

    <!-- Style Bootstrap -->
    <link href="Bootstrap/vendor/bootstrap/v4.0.0/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Bootstrap/vendor/fontawesome/v5.0.12/css/fontawesome-all.min.css" rel="stylesheet" />
    <!-- Script Bootstrap -->
    <script src="Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js"></script>
    <script src="Bootstrap/vendor/bootstrap/v4.0.0/js/bootstrap.min.js"></script>

        <!-- Script -->
    <script type="text/javascript">
        $(function () {
            var rand = Math.floor(Math.random() * 5) + 0;
            switch (rand) {
                case 0:
                    $('#sidebar').addClass("sidebar-green");
                    $('#sidebarTitulo').html("Espaço de Trabalho");
                    $('form.login').css('border-top', '4px solid #79ACA5');

                    break;
                case 1:
                    $('#sidebar').addClass("sidebar-orange");
                    $('#sidebarTitulo').html("Estratégia");
                    $('form.login').css('border-top', '4px solid #C25127');

                    break;
                case 2:
                    $('#sidebar').addClass("sidebar-blue");
                    $('#sidebarTitulo').html("Projetos e Portfólios");
                    $('form.login').css('border-top', '4px solid #515E6E');
                    break;
                case 3:
                    $('#sidebar').addClass("sidebar-yellow");
                    $('#sidebarTitulo').html("Administração");
                    $('form.login').css('border-top', '4px solid #E2C162');
                    break;

                case 4:
                    $('#sidebar').addClass("sidebar-grey");
                    $('#sidebarTitulo').html("Mobilidade");
                    $('form.login').css('border-top', '4px solid #E2C162');
                    break;
            }

        });


    </script>
</head>
<body id="login" scroll="no" text-align: center">

    <div class="container-fluid">
    <div class="row" style="height:900px;">

              <div class="col-sm-4 d-sm-none d-md-block">
                <div id="sidebar">
                    <a class="logo" href="#">
                        <img src="Bootstrap/images/logo-brisk-login.png" class="img-fluid" alt="Logomarca Brisk" />
                    </a>

                    <div class="text-sub-title">
                        <a href="#" id="sidebarTitulo" title="Estratégia">Estratégia</a>
                    </div>

                </div>
            </div>

                   <div class="col-sm-8">
                <div class="box-form">
    <form id="form1" runat="server">
    <div class="centralizarAlterarSenha">

        <table class="login altSenhaPage">
            <tr>
                <td align="center">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 60px; display:none;">
                        <tr>
                            <td align="left" style="height:26px">
                                <table>
                                    <tr>
                                        <td style="text-align: center; height: 20px;" class="titulo">
                                            &nbsp;
                                            <asp:Label ID="lblIdentifique" runat="server"
                                                Font-Overline="False" Font-Strikeout="False"
                                                Text=" Por favor, digite os dados de sua nova senha"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <dxrp:ASPxRoundPanel ID="painelLogin" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                        CssPostfix="DevEx" HeaderText="" DefaultButton="btnAlterar"
                        GroupBoxCaptionOffsetX="6px" GroupBoxCaptionOffsetY="-19px" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" CssClass="textalignRight">
                        
                        <ContentPaddings PaddingBottom="10px" PaddingTop="10px">
                        </ContentPaddings>

                        <HeaderStyle VerticalAlign="Middle" Font-Size="13px">
                            <Paddings PaddingBottom="6px" PaddingLeft="7px" PaddingRight="11px" PaddingTop="1px" />
                        </HeaderStyle>

                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>

                                            <div style="margin-bottom:15px;">
	                                            <i class="fas fa-key loginLockAlt"></i><span class="loginTextLock">Alterar Senha</span>
                                            </div>
                                           </td>
                                        </tr>
                                         
                                        <tr>
                                            <td>
                                                <table style="width: 100%; text-align: left" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0">
                                                                    <tbody>
                                                                        <tr>

                                                                            <td align="left">
                                                                                <dxe:ASPxTextBox runat="server" Height="22px" ClientInstanceName="txtUsuario" CssClass="inputEmail"
                                                                                    ID="txtUsuario" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" 
                                                                                            NullText="Usuário" Width="370px">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 10px" valign="top" align="right">
                                                                            </td>
                                                                            <td style="height: 10px" align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>

                                                                            <td align="left">
                                                                                <dxe:ASPxTextBox runat="server"  MaxLength="20" Password="True" Height="22px" CssClass="inputEmail"
                                                                                    ClientInstanceName="txtSenhaAtual" ID="txtSenhaAtual" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" 
                                                                                            NullText="Senha Atual" Width="370px">
                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                    </ValidationSettings>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-weight: bold; width: 162px; height: 10px" valign="top" align="right">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
        

                                                                            <td align="left">
                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 176px">
                                                                                                <dxe:ASPxTextBox runat="server" MaxLength="20" Password="True" Height="22px" CssClass="inputEmail"
                                                                                                    ClientInstanceName="txtNovaSenha"  ID="txtNovaSenha" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" 
                                                                                            NullText="Nova Senha" Width="370px">
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                            <td valign="middle">
                                                                                                <span id="spanHelp">
                                                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ClientInstanceName="imgHelpUsuario"
                                                                                                        ID="imgHelpUsuario" Style="cursor: pointer; margin: -5px 0px 0px 15px;">
                                                                                                    </dxe:ASPxImage>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>


                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 10px" valign="top" align="right">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
   
                                                                            <td align="left">
                                                                                <dxe:ASPxTextBox runat="server" MaxLength="20" Password="True" Height="22px" CssClass="inputEmail"
                                                                                    ClientInstanceName="txtConfNovaSenha"  ID="txtConfNovaSenha" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" 
                                                                                            NullText="Confirmar Nova Senha" Width="370px">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%; padding: 20px 0px;" cellspacing="0" cellpadding="0">
                                                                    <tbody>
                                                                        <tr>
                                                                              <td>
                                                                                <dxe:ASPxButton runat="server" EnableClientSideAPI="True" Text="Alterar senha" CssPostfix="MaterialCompact"
                                                                                    CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" CssClass="btnEnterLoginAlterar"
                                                                                    ID="btnAlterar" AutoPostBack="False">
                                                                                     <Paddings Padding="0px"></Paddings>
                                                                                    <ClientSideEvents Click="function(s, e) 
                                                                                    {
                                                                                    pnCallbackErro.PerformCallback();
                                                                                    }"></ClientSideEvents>
                                                                              </dxe:ASPxButton>
                                                                            </td>

                                                                            

                                                                        </tr>

                                                                        <tr>
                                                                              <td>
                                                                                <dxe:ASPxButton runat="server" Text="Tela de Login" CssPostfix="MaterialCompact"
                                                                                    CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"  CssClass="btnEnterLoginAlterar"
                                                                                     ID="btnCancelar" OnClick="btnCancelar_Click"
                                                                                    >
                                                                                     <Paddings Padding="0px"></Paddings>
                                                                                    <ClientSideEvents Click="function(s, e) 
{

}" CheckedChanged="function(s, e) 
{
	
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="tituloLicenciado" style="display:none;">
                                                <asp:Label runat="server" Text="Licenciado para:" 
                                                    ID="lblLicenciado"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label runat="server" Font-Bold="True"  ID="lblLicenca"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 15px">
                                                <asp:Label runat="server" Text="Aten&#231;&#227;o! CAPS LOCK Ligado."
                                                    ForeColor="Red" ID="Label5" Style="visibility: hidden"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 15px">
                                                &nbsp;<dxtv:ASPxCallbackPanel ID="pnCallbackErro" runat="server" ClientInstanceName="pnCallbackErro"
                                                    OnCallback="pnCallbackErro_Callback" Width="100%">
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <asp:Label ID="lblErro" runat="server" Font-Bold="True" 
                                                                ForeColor="Red" Height="12px"></asp:Label>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                    <dxpc:ASPxPopupControl ID="popupHelp" runat="server" ClientInstanceName="popupHelp"
                         HeaderText="" PopupElementID="spanHelp" CssClass="alignPopUp"
                        Modal="true" Width="416px" CloseAction="CloseButton" PopupHorizontalAlign="Center" PopupVerticalAlign="TopSides">
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                A nova senha deverá ter entre 4 e 14 caracteres.</dxpc:PopupControlContentControl>
                        </ContentCollection>
                        <HeaderImage Url="~/imagens/ajuda.png">
                        </HeaderImage>
                    </dxpc:ASPxPopupControl>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                </td>
            </tr>


        </table>
    </div>
    </form>
                                   </div>


            </div>

    </div>
                            <div class="d-flex flex-row-reverse loginFooter">

                        <div style="font-size: .82rem;font-family: 'Roboto Regular', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif;padding-left: 1rem;">Licenciado para: CDIS Informática Ltda.</div>
                        <span style="font-size: .82rem;font-family: 'Roboto Regular', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif; border-right: 1px dotted #585254; padding-right: 1rem; cursor: pointer;">
                            <a href="#" onClick="window.open('./_Public/Anexos.aspx', '_self')">Pasta pública</a></span>
                        <div style="padding-right: 1rem;"> <dxcp:ASPxImage ID="ASPxImage2" runat="server" Cursor="pointer" ImageUrl="~/imagens/anexo/pasta.png" ShowLoadingImage="True" ToolTip="Acessar pastas públicas">
		                        <ClientSideEvents Click="function(s, e) {
		                            window.open('./_Public/Anexos.aspx', '_self');
		                            }" />
		                    </dxcp:ASPxImage>
                            
                         

                        </div>
                        
                    </div>
    </div>


</body>
</html>
