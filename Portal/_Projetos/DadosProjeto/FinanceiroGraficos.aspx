<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinanceiroGraficos.aspx.cs" Inherits="_Projetos_DadosProjeto_RecursosHumanos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>
    <title>Untitled Page</title>
</head>
<body style="margin: 0">
    <div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width: 99%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="border-right: #e0e0e0 1px solid; border-top: #e0e0e0 1px solid; border-left: #e0e0e0 1px solid; border-bottom: #e0e0e0 1px solid; height: 22px">
                                        <tr>
                                            <td style="width: 30px">
                                                <dxe:ASPxImage ID="imgGraficos" runat="server" ImageUrl="~/imagens/olap.PNG" ToolTip="Tabela de Orçamento do Projeto">
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
                                <td align="right" style="width: 50px">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                        Text="Tipo:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="left" style="width: 90px">
                                    <dxe:ASPxComboBox ID="ddlTipo" runat="server" AutoPostBack="True" ClientInstanceName="ddlTipo"
                                        OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"
                                        SelectedIndex="0" Width="100px">
                                        <Items>
                                            <dxe:ListEditItem Selected="True" Text="Despesa" Value="D" />
                                            <dxe:ListEditItem Text="Receita" Value="R" />
                                        </Items>
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <dxrp:ASPxRoundPanel ID="pRH" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                            CssPostfix="PlasticBlue" HeaderText="Desempenho Físico do Projeto"
                            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%">
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
                                    <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                    </dxcp:ASPxHiddenField>
                                    <div id="chart-container">
                                    </div>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <HeaderTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                    Text="Orçamento">
                                                </dxe:ASPxLabel>
                                                &nbsp;</td>
                                           <%-- <td align="right">
                                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoomChart2.aspx', '', 'Pie2D', 'graficoFinanceiro_jsonzoom')"
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
            <script type="text/javascript">

                var sHeight = Math.max(0, document.documentElement.clientHeight) - 25;
                var sWidth = Math.max(0, document.documentElement.clientWidth) - 25;

                FusionCharts.ready(function () {
                    var revenueChart = new FusionCharts({
                        type: 'pie2d',
                        renderAt: 'chart-container',
                        width: sWidth,
                        height: sHeight,
                        dataFormat: 'json',
                        dataSource: { <%=graficoFinanceiro_jsonzoom%> }

                    }).render();
                });

            </script>
        </form>
    </div>

</body>
</html>

