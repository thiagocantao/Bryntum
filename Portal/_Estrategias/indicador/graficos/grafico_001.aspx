<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_001.aspx.cs" Inherits="_Estrategias_indicador_graficos_grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../../FusionMapsJS/FusionMPS.js" language="javascript"></script>
     <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        function atualizaUnidade(codigoUnidade)
        {
           parent.alteraUnidade(codigoUnidade);
        }
        
        function abreGridUnidades(codigoUF)
        {
            parent.gvUnidades.PerformCallback(codigoUF);
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table>
            <tr>
                <td>
        <dxrp:ASPxRoundPanel ID="pDesempenhoFisico" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue"  HeaderText="Desempenho Físico do Projeto"
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="410px">
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
                                   getGraficoMapa('<%=grafico1_swf %>', "grafico001", '400', '<%=alturaGrafico %>', '<%=grafico1_xml %>', 'chartdiv1');
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
                                    Text="Desempenho do Indicador - Brasil">
                                </dxe:ASPxLabel>
                                &nbsp;</td>
                            <td align="right">
                                <img align="right" alt="<%# Resources.traducao.grafico_001_visualizar_gr_fico_em_modo_ampliado %>" onclick="javascript:f_zoomGrafico('../../zoomMapaBrasil.aspx', 'Desempenho do Indicador - Brasil', '', '<%=grafico1_xmlzoom %>', '0')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                        </tr>
                    </tbody>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <dxrp:ASPxRoundPanel ID="pGrafico1" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                        CssPostfix="PlasticBlue"  HeaderText="Comparação"
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="410px">
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
                                            <div id="chartdiv2" align="center">
                                            </div>

                                           <script type="text/javascript">
                                                getGrafico('<%=grafico2_swf %>', "grafico002", '400', '70', '<%=grafico2_xml %>', 'chartdiv2');
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
                                                Text="<%$ Resources:traducao, grafico_001_desempenho_geral %>">
                                            </dxe:ASPxLabel>
                                            &nbsp;</td>
                                        <td align="right">
                                            <img align="right" alt="<%# Resources.traducao.grafico_001_visualizar_gr_fico_em_modo_ampliado %>" onclick="javascript:f_zoomGrafico('../../zoom.aspx', '', 'Flashs/HLinearGauge.swf', '<%=grafico2_xmlzoom %>', '0')"
                                                src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
        </table>
        &nbsp; &nbsp;
        &nbsp;
        &nbsp;&nbsp;</div>
    </form>
</body>
</html>
