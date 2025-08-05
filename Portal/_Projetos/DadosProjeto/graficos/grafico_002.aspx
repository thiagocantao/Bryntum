<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_002.aspx.cs" Inherits="grafico_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho"
            Width="100%" CornerRadius="1px">
            <ContentPaddings Padding="0px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB" Height="27px">
            <Paddings PaddingBottom="2px" PaddingTop="2px" />
            </HeaderStyle>
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table align="center" border="0" cellpadding="0" cellspacing="0">                                            
                                            <tr>
                                                <td>
                                                    <div id="chartdiv1" align="center">
                                                        </div>
                                                </td>
                                            </tr>                                            
                                            <tr>
                                                <td>
                                                    <div id="chartdiv2" align="center">
                                                    </div>
                                                </td>
                                            </tr>                                            
                                            <tr>
                                                <td>
                                                    <div id="chartdiv3" align="center">
                                                    </div>
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
                                                        Text="Desempenho Geral">
                                                    </dxe:ASPxLabel>
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomBullets('../../zoomChartsBullets.aspx', 'Desempenho Geral' ,'grafico002_1_jsonzoom','grafico002_2_jsonzoom','grafico002_3_jsonzoom')"
                                src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </HeaderTemplate>
                            </dxrp:ASPxRoundPanel></div>
         <script type="text/javascript">
             var sHeight = Math.max(0, document.documentElement.clientHeight) - 25;
             var sWidth = Math.max(0, document.documentElement.clientWidth) - 25;
             getGraficoZoom('hbullet', sWidth, sHeight, '<%=grafico002_1_jsonzoom %>', 'chartdiv1')
             getGraficoZoom('hbullet', sWidth, sHeight, '<%=grafico002_2_jsonzoom %>', 'chartdiv2')
             getGraficoZoom('hbullet', sWidth, sHeight, '<%=grafico002_3_jsonzoom %>', 'chartdiv3')

         </script>
    </form>
</body>
</html>