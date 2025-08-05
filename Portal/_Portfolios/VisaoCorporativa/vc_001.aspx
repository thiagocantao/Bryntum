<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_001.aspx.cs" Inherits="_Portfolios_VisaoCorporativa_vc_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" HeaderText="Desempenho Geral"
            Height="215px" Width="210px" ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/612423700/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left" />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server"><table>
                    <tr >
                        <td>
                            <div id="vchartdiv1" align="center">
                            </div>

                            <script type="text/javascript">
                                if ('<%=mostrarEsforco %>' != 'N')
                                    getGrafico('<%=grafico1_swf %>', "grafico001", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico1_xml %>', 'vchartdiv1');
                            </script>

                        </td>
                    </tr>
                    <tr >
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <div id="chartdiv2" align="center">
                            </div>

                            <script type="text/javascript">
                                if ('<%=mostrarDespesa %>' != 'N')
                                    getGrafico('<%=grafico1_swf %>', "grafico002", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico2_xml %>', 'chartdiv2');
                            </script>

                        </td>
                    </tr>
                    <tr >
                        <td style="height: 9px">
                        </td>
                    </tr>
                    <tr >
                        <td>
                            <div id="chartdiv3" align="center">
                            </div>

                            <script type="text/javascript">
                                    if ('<%=mostrarReceita %>' != 'N')
                                       getGrafico('<%=grafico3_swf %>', "grafico003", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico3_xml %>', 'chartdiv3');
                            </script>

                        </td>
                    </tr>
                    <tr>
                        <td />
                    </tr>
                </table>
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table>
                    <tr>
                        <td align="left">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Desempenho Geral">
                            </dxe:ASPxLabel>
                            &nbsp;</td>
                        <td align="right">
                            <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomBullets('../zoomBullets.aspx', 'Desempenho Geral' ,'HBullet.swf', '<%=grafico1_xmlzoom %>', 'HBullet.swf', '<%=grafico2_xmlzoom %>', 'HBullet.swf', '<%=grafico3_xmlzoom %>')"
                                src="../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                    </tr>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
    
    </div>
    </form>
</body>
</html>
