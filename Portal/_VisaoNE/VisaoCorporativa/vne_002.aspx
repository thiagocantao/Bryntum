<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vne_002.aspx.cs" Inherits="_VisaoNE_VisaoCorporativa_vne_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue" HeaderText="Desempenho" Height="215px" ImageFolder="~/App_Themes/PlasticBlue/{0}/"
            Width="210px">
            <ContentPaddings Padding="1px" />
            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" >
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png"
                    Repeat="RepeatX" VerticalPosition="bottom" />
            </HeaderContent>
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
                <table>
                    <tbody>
                        <tr>
                           <td align="left">
                                <%# getTitulo()%>
                                </td>
                            <td align="right">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../zoom.aspx', '', 'Flashs/MSColumn2D.swf', '<%=grafico_xmlzoom %>', '0')"
                                    src="../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                        </tr>
                    </tbody>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
