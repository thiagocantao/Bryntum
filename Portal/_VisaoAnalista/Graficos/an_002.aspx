<%@ Page Language="C#" AutoEventWireup="true" CodeFile="an_002.aspx.cs" Inherits="_VisaoAnalista_Graficos_an_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/custom_frame.css" rel="stylesheet" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center">
            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho" Width="210px" CornerRadius="1px">
                <ContentPaddings Padding="1px" />
                <HeaderStyle Font-Bold="False" BackColor="#EBEBEB" Height="31px"></HeaderStyle>
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
                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Meus Projetos">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right">
                                    <img style="cursor: pointer"
                                        onclick="javascript:f_zoomGrafico('../zoom.aspx', '', 'Flashs/Pie2D.swf', '<%=grafico_xmlzoom %>', '0')"
                                        align="right" /></td>
                            </tr>
                        </tbody>
                    </table>
                </HeaderTemplate>
            </dxrp:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
