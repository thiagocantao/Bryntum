<%@ Page Language="C#" AutoEventWireup="true" CodeFile="po_autentica.aspx.cs" Inherits="po_autentica" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body scroll="no" style="margin: 0px; text-align: center" text="#00a">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td align="left" style="width: 100px">
                    <img src="imagens/titulo/LogoPortal.png" /></td>
            </tr>
<tr>
                <td align="left" background="imagens/titulo/back_Titulo_Desktop.gif" height="26px">
                    &nbsp;&nbsp;<asp:Label ID="lblIdentifique" runat="server" Font-Bold="True"
                        Font-Overline="False" Font-Strikeout="False" Text=""></asp:Label></td>
            </tr>            
            <tr>
                <td align="left" style="width: 100px; height: 50px">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <dxrp:ASPxRoundPanel ID="painelLogin" runat="server" BackColor="#F3F3F3" CssFilePath="~/App_Themes/RedLine/{0}/styles.css"
                        CssPostfix="RedLine" Font-Size="10pt" HeaderText="Usuário sem acesso" Width="400px">
                        <ContentPaddings PaddingBottom="12px" />
                        <Border BorderStyle="None" />
                        <HeaderStyle Font-Bold="True" Font-Size="14pt" HorizontalAlign="Center"
                            VerticalAlign="Middle">
                            <Border BorderStyle="None" />
                        </HeaderStyle>
                        <HeaderContent BackColor="#DADADA">
                            <BackgroundImage ImageUrl="~/App_Themes/RedLine/Web/rpHeaderContent.png" Repeat="RepeatX" VerticalPosition="bottom" />
                        </HeaderContent>
                        <PanelCollection>
                            <dxp:PanelContent runat="server"><asp:Label ID="lblUsuarioRede" runat="server" Font-Bold="True" 
                                    ForeColor="Red"></asp:Label>
 &nbsp;<br /><br /><asp:Label ID="lblErro" runat="server" Font-Bold="True"
     ForeColor="Red">No momento, você não é um usuário ativo em nenhuma unidade. Entre em contato com o administrador do sistema.</asp:Label>
 <br /><br />&nbsp;<dxe:ASPxButton ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Tentar acessar utilizando usu&#225;rio do sistema" ></dxe:ASPxButton>
 </dxp:PanelContent>
                        </PanelCollection>
                        <HeaderImage Url="~/imagens/login/login_Acesso.gif" />
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
