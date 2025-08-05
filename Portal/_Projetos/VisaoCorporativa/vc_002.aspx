<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_002.aspx.cs" Inherits="_Projetos_VisaoCorporativa_vc_002" %>

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
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho" Width="210px" CornerRadius="1px">

            <ContentPaddings Padding="1px" />
            <HeaderStyle Font-Bold="False"  
                BackColor="#EBEBEB" Height="31px">
            </HeaderStyle>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <div id="chartdiv" align="center">
                    </div>

                    <script type="text/javascript">
                          getGrafico('<%=grafico_swf %>', "grafico001", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
                    </script>

                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table style="WIDTH: 100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left">
                            <dxe:ASPxLabel id="ASPxLabel1" runat="server" Text="Projetos - Desempenho">
                            </dxe:ASPxLabel>  
                        </td>
                        <td align="right">
                            <img style="cursor: pointer" onclick="javascript:f_zoomGrafico('../zoom.aspx', '', 'Flashs/Pie2D.swf', '<%=grafico_xmlzoom %>', '0')" alt="Visualizar gráfico em modo ampliado" src="../../imagens/zoom.PNG" />
                        </td>
                    </tr>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
       </div>
    </form>
</body>
</html>
