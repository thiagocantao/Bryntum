<%@ Page Language="C#" AutoEventWireup="true" CodeFile="md_006.aspx.cs" Inherits="_Dashboard_VisaoCorporativa_md_006" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="White"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" HeaderText="Despesa/Receita por Categoria"
            Height="215px" Width="235px" ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/219597904/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left" />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height:<%=alturaTabela %>">
                        <tr>
                            <td align="left" style="width: 51px">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/aprovacoes.PNG">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkAprovacoes" runat="server" >
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 51px">
                                <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/calendarioDash.PNG">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkReunioes" runat="server" >
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 51px">
                                <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/AtualizarIndicador.PNG">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkIndicadores" runat="server" >
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 51px">
                                <dxe:ASPxImage ID="ASPxImage4" runat="server" ImageUrl="~/imagens/avisos.PNG">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="hlAvisos" runat="server" >
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                 <table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><tbody><tr><td align=left style="height: 17px"><dxe:ASPxLabel id="ASPxLabel1" runat="server" Text="Outras Informações" >
                            </dxe:ASPxLabel>  &nbsp;&nbsp;</td><td align=right style="height: 17px"></td></tr></tbody></table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
    
    </div>
    </form>
</body>
</html>
