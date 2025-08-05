<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_003.aspx.cs" Inherits="grafico_003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table>
            <tr>
                <td>
                    <dxrp:ASPxRoundPanel ID="pRiscos" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue"  HeaderText="Desempenho Físico do Projeto"
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="420px">
                        <ContentPaddings Padding="1px" PaddingTop="2px" />
                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                        <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                ForeColor="#404040">
                            <Paddings Padding="1px" PaddingLeft="3px" PaddingTop="0px" />
                            <BorderLeft BorderStyle="None" />
                            <BorderRight BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                        </HeaderStyle>
                        <HeaderContent>
                            <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/1031923202/HeaderContent.png"
                    Repeat="RepeatX" VerticalPosition="bottom" />
                        </HeaderContent>
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="False"
                                                    Text="Probabilidade">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" style="width: <%=alturaGrafico %>px; height: <%=alturaGrafico %>px">
                                                                         <tr>
                                                                            <td style="background-color: #ffffba">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_3_1" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ff8181">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_3_2" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ff484a">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_3_3" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="background-color: #b8ffba">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_2_1" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ffff6f">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_2_2" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ff8181">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_2_3" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="background-color: #8cff8c">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_1_1" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #b8ffba">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_1_2" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ffffba">
                                                                                <dxe:ASPxHyperLink ID="hlRisco_1_3" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="writing-mode:tb-rl;">
                                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="False"
                                                                Text="Impacto">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <HeaderTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left" style="height: 18px">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Riscos">
                                            </dxe:ASPxLabel>
                                            &nbsp;</td>
                                        <td align="right" style="height: 18px">                                            
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                    </dxrp:ASPxRoundPanel>
                </td>
                <td style="width: 5px">
                </td>
                <td>
                    <dxrp:ASPxRoundPanel ID="pQuestoes" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue"  HeaderText="Desempenho Físico do Projeto"
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="420px">
                        <ContentPaddings Padding="1px" PaddingTop="2px" />
                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                        <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                ForeColor="#404040">
                            <Paddings Padding="1px" PaddingLeft="3px" PaddingTop="0px" />
                            <BorderLeft BorderStyle="None" />
                            <BorderRight BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                        </HeaderStyle>
                        <HeaderContent>
                            <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/1031923202/HeaderContent.png"
                    Repeat="RepeatX" VerticalPosition="bottom" />
                        </HeaderContent>
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel26" runat="server" Font-Bold="False"
                                                    Text="Urg&#234;ncia">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                     <table cellpadding="0" cellspacing="0" style="width: <%=alturaGrafico %>px; height: <%=alturaGrafico %>px">
                                                                         <tr>
                                                                            <td style="background-color: #ffffba">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_3_1" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ff8181">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_3_2" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ff484a">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_3_3" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="background-color: #b8ffba">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_2_1" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ffff6f">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_2_2" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ff8181">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_2_3" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="background-color: #8cff8c">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_1_1" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #b8ffba">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_1_2" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                            <td style="background-color: #ffffba">
                                                                                <dxe:ASPxHyperLink ID="hlQuestao_1_3" runat="server" Font-Bold="True"
                                                                            Text="0">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 20px; writing-mode:tb-rl;">
                                                                    <dxe:ASPxLabel ID="ASPxLabel33" runat="server" Font-Bold="False"
                                                                Text="Prioridade">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
