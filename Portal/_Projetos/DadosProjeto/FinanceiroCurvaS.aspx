<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinanceiroCurvaS.aspx.cs" Inherits="_Projetos_DadosProjeto_RecursosHumanos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
</head>
<body style="margin: 10; margin-bottom: 0">
    <div>
        <form id="form1" runat="server">
            <table>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="border-right: #e0e0e0 1px solid; border-top: #e0e0e0 1px solid; border-left: #e0e0e0 1px solid; border-bottom: #e0e0e0 1px solid; height: 22px">
                            <tr>
                                <td style="width: 30px">
                                    <dxe:ASPxImage ID="imgGraficos" runat="server" ImageUrl="~/imagens/olap.PNG" ToolTip="Tabela de Recursos Humanos do Projeto">
                                    </dxe:ASPxImage>
                                </td>
                                <td style="width: 45px">
                                    <dxe:ASPxLabel ID="lblGrafico" runat="server"
                                        Text="Tabela">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td align="center">
                        <dxrp:ASPxRoundPanel ID="pRH" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                            CssPostfix="PlasticBlue" HeaderText="Desempenho Físico do Projeto"
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
                                                <div id="chartdiv1" align="center">
                                                </div>

                                                <script type="text/javascript">
                                               getGrafico('<%=grafico1_swf %>', "grafico001", '100%', '<%=alturaGrafico %>', '<%=grafico1_xml %>', 'chartdiv1');
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <HeaderTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                    Text="Curva S Financeira">
                                                </dxe:ASPxLabel>
                                                &nbsp;</td>
<%--                                            <td align="right" style="width: 20px">
                                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoom.aspx', '', 'Flashs/MSLine.swf', '<%=grafico1_xmlzoom %>', '0')"
                                                    src="../../imagens/zoom.PNG" style="cursor: pointer" />
                                            </td>--%>
                                        </tr>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>

