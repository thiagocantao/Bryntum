<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ex_004.aspx.cs" Inherits="_VisaoExecutiva_Graficos_ex_004" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function abreMensagensNovas()
        {
            window.top.showModal("../Mensagens/MensagensPainelBordo.aspx?EsconderBotaoNovo=S", 'Mensagens', 1000, 600, atualizaTela, null);  
        }
        
        function atualizaTela(lParam)
        {
            window.location.reload();
        }
        
        function abreMensagensAbertas()
        {
            window.top.showModal("../Mensagens/MensagensPainelBordo.aspx?EsconderBotaoNovo=S", 'Mensagens', 1000, 600, atualizaTela, null);    
        }
        
        function abreMensagensRespondidas()
        {
            window.top.showModal("../Mensagens/MensagensPainelBordo.aspx?EsconderBotaoNovo=S", 'Mensagens', 1000, 600, atualizaTela, null);  
        }
    
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <dxrp:ASPxRoundPanel id="ASPxRoundPanel1" runat="server" ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="235px" Height="215px" HeaderText="Mensagens" CssPostfix="PlasticBlue" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" BackColor="White">
            <ContentPaddings Padding="1px"  />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px"  />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None"  />
                <BorderRight BorderStyle="None"  />
                <BorderBottom BorderStyle="None"  />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/219597904/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left"  />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                        <tr>
                            <td align="left" style="width: 60px">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/MensagemNova.png">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkMensagensNovas" runat="server"  EncodeHtml="False" Font-Bold="False">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 60px; padding-left: 3px;">
                                <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/calendarioDash.PNG">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkReunioes" runat="server"  EncodeHtml="False">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="padding-left: 3px; width: 60px">
                                <dxe:ASPxImage ID="ASPxImage4" runat="server" ImageUrl="~/imagens/aprovacoes.PNG">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkFluxos" runat="server" EncodeHtml="False"
                                   >
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </dxrp:ASPxRoundPanel>
    </div>
    </form>
</body>
</html>
