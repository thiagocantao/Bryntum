<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vm_009.aspx.cs" Inherits="_VisaoMaster_Graficos_vm_009" %>

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
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" HeaderText="Projetos por Desempenho e Categoria"
            Height="215px" Width="235px"  ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/671673645/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left" />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                     <table>
                    <tr >
                        <td>
                            <div id="vchartdiv1" align="center">
                            </div>

                            <script type="text/javascript">
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
                                    getGrafico('<%=grafico2_swf %>', "grafico002", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico2_xml %>', 'chartdiv2');
                            </script>

                        </td>
                    </tr>                    
                </table>
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><TBODY><tr><td align=left><dxe:ASPxLabel id="ASPxLabel1" runat="server" Text="Desempenho Físico e Econômico">
                            </dxe:ASPxLabel>  </td><td align=right><IMG style="CURSOR: pointer" 
                    onclick="javascript:f_zoomBullets('../zoomBullets.aspx', 'Desempenho Físico e Econômico' ,'HBullet.swf', '<%=grafico1_xmlzoom %>', 'HBullet.swf', '<%=grafico2_xmlzoom %>', 'HBullet.swf', '')" 
                    alt="Visualizar gráfico em modo ampliado" src="../../imagens/zoom.PNG" 
                    align=right 
                    /></td></tr></tbody></table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
