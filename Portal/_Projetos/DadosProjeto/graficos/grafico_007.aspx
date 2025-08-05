<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_007.aspx.cs" Inherits="grafico_007" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
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
                                Desempenho dos Riscos e <%= tituloQuestoes %>
                                </td>
                            <td align="right">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoom.aspx', '', 'Flashs/StackedColumn2D.swf', '<%=grafico1_xmlzoom %>', '0')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                        </tr>
                    </tbody>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
