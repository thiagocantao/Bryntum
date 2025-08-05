<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_023.aspx.cs" Inherits="grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
  
    <style type="text/css">

.dxeBase
{
	font: 12px Tahoma, Geneva, sans-serif;
}
        .style1
        {
            height: 4px;
            width: 4px;
            border-width: 0px;
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
                                            <div id="chartdiv1" align="center">
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
                                                Text="Curva S de Alocação de Recursos">
                                            </dxe:ASPxLabel>
                                            &nbsp;</td>
                                        <td align="right" style="width: 20px">
                                            <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoom.aspx', '', 'Flashs/MSLine.swf', '<%=grafico_xmlzoom %>', '0')"
                                                src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                    </dxrp:ASPxRoundPanel>                               
       </div>
    </form>
</body>
</html>
