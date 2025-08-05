<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_018.aspx.cs" Inherits="grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>
  
    <style type="text/css">
        .style1
        {
            width: 129px;
        }
    </style>
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
                    <table>
                        <tr>
                            <td>
                                <div id="chartdiv" align="center">
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
                            <td align="left" class="style1">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Desempenho Físico">
                                </dxe:ASPxLabel>
                                &nbsp;</td>
                            <td align="left">
                                <dxe:ASPxRadioButtonList ID="rbGrafico" runat="server" 
                                     ItemSpacing="10px" 
                                    RepeatDirection="Horizontal" SelectedIndex="0" TextSpacing="2px" 
                                    AutoPostBack="True" onselectedindexchanged="rbGrafico_SelectedIndexChanged">
                                    <Paddings Padding="0px" />
                                    
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="Speedômetro" Value="S" />
                                        <dxe:ListEditItem Text="Curva S Física" Value="C" />
                                    </Items>
                                    <Border BorderStyle="None" />
                                </dxe:ASPxRadioButtonList>
                            </td>
                            <td align="right">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoomChart.aspx','','<%=tipoGrafico %>', 'grafico18')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                        </tr>
                    </tbody>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
       </div>
         
   <script type="text/javascript">
       var sHeight = Math.max(0, document.documentElement.clientHeight) - 25;
       var sWidth = Math.max(0, document.documentElement.clientWidth) - 25;
       getGraficoZoom('<%=tipoGrafico %>', sWidth, sHeight, '<%=grafico18_jsonzoom%>', 'chartdiv')
   </script> </form>
</body>
</html>
