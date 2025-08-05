<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="alterarSenhaViaOpMenu.aspx.cs" Inherits="alterarSenhaViaOpMenu" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="/*background-image: url(imagens/titulo/back_Titulo_Desktop.gif);*/
                height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div align="center" style="margin-top:-35px;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td align="center">
                    <br />
                    <dxrp:ASPxRoundPanel ID="painelLogin" runat="server" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"  Font-Size="10pt" HeaderText="Redefinição de senha"
                        Height="182px" Width="410px" DefaultButton="btnAlterar"
                        GroupBoxCaptionOffsetX="6px" GroupBoxCaptionOffsetY="-19px" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" HorizontalAlign="Center">
                        
                        <ContentPaddings PaddingBottom="15px" PaddingLeft="7px" PaddingRight="11px" PaddingTop="10px">
                        </ContentPaddings>
                        
                         <HeaderImage Url="~/imagens/login/login_Acesso.gif" Height="">
                         </HeaderImage>

                         <HeaderStyle VerticalAlign="Middle"  Font-Size="13px" Font-Bold="true">
                            <Paddings PaddingBottom="6px" PaddingLeft="7px" PaddingRight="11px" PaddingTop="1px" />
                        </HeaderStyle>

                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table cellspacing="0" cellpadding="0">
                                    <tbody>
                                       <%-- <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <table style="width: 100%; text-align: center" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table <%--cellspacing="0" cellpadding="0"--%>>
                                                                    <tbody>
                                                                        <tr>
                                                                         <td align="left">
                                                                           <dxe:ASPxLabel runat="server" Height="22px" ClientInstanceName="lblUsuario" ID="lblUsuario"   
                                                                               CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                                                                            </dxe:ASPxLabel>
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
                                                                                <dxe:ASPxTextBox runat="server" MaxLength="20" Password="True" Height="22px"
                                                                                    ClientInstanceName="txtSenhaAtual" ID="txtSenhaAtual" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" 
                                                                                    NullText="Senha Atual" Width="370px"
                                                                                   >
                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                    </ValidationSettings>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                            <td style="font-weight: bold; width: 162px; height: 10px" valign="top" align="right">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 176px">
                                                                                                <dxe:ASPxTextBox runat="server" MaxLength="20" Password="True" Height="22px"
                                                                                                    ClientInstanceName="txtNovaSenha"  ID="txtNovaSenha" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" 
                                                                                                    NullText="Nova Senha" Width="370px">
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                            <td valign="middle">
                                                                                                <span id="spanHelp" style="display:none">
                                                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ClientInstanceName="imgHelpUsuario"
                                                                                                        ID="imgHelpUsuario" Style="cursor: pointer; margin: 0px 3px;">
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
                                                                                <dxe:ASPxTextBox runat="server" MaxLength="20" Password="True" Height="22px"
                                                                                    ClientInstanceName="txtConfNovaSenha"  ID="txtConfNovaSenha" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" NullText="Confirmar Nova Senha" Width="370px">
                                                                                            
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <table cellspacing="0" cellpadding="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <dxe:ASPxButton runat="server" EnableClientSideAPI="True" Text="Alterar senha" CssPostfix="PlasticBlue"
                                                                                    CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                                                                                    ID="btnAlterar" AutoPostBack="False" Width="100px">
                                                                                    <ClientSideEvents Click="function(s, e) 
{
pnCallbackErro.PerformCallback();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                                &nbsp;</td>
                                                                            <caption>
                                                                                &nbsp;</caption>
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
                                            <td align="left">
                                                <asp:Label runat="server" Text="<%# Resources.traducao.alterarSenhaViaOpMenu_licenciado_para_ %>" 
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
                                                <asp:Label runat="server" Text="<%# Resources.traducao.alterarSenhaViaOpMenu_aten__o_caps_lock_ligado_ %>"
                                                    ForeColor="Red" ID="Label5" Style="visibility: hidden"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 15px">
                                                <dxtv:ASPxCallbackPanel ID="pnCallbackErro" runat="server" 
                                                    ClientInstanceName="pnCallbackErro" OnCallback="pnCallbackErro_Callback" 
                                                    Width="100%">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg != '')
	{
		if(s.cp_status == 'ok')
		{
            			window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null, 2500);
			setTimeout(function() { window.open('login.aspx', '_self'); }, 3000);
		}
        	}
}
" />
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <asp:Label ID="lblErro" runat="server" Font-Bold="True" 
                                                                ForeColor="Red" Height="16px"></asp:Label>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxCallbackPanel>
                                                &nbsp;</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                    <dxpc:ASPxPopupControl ID="popupHelp" runat="server" ClientInstanceName="popupHelp"
                         HeaderText="" PopupElementID="spanHelp"
                        Modal="true" Width="416px" CloseAction="CloseButton">
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                A nova senha deverá ter entre 4 e 14 caracteres.
                                </dxpc:PopupControlContentControl>
                        </ContentCollection>
                        <HeaderImage Url="~/imagens/ajuda.png">
                        </HeaderImage>
                    </dxpc:ASPxPopupControl>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" 
                        ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style2 {
            height: 16px;
        }
    </style>
</asp:Content>

