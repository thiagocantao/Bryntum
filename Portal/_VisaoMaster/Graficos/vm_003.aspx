<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vm_003.aspx.cs" Inherits="_VisaoMaster_Graficos_vm_003" %>

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
        <dxrp:ASPxRoundPanel ID="pDesempenhoFisico" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue"  HeaderText="Desempenho Físico do Projeto"
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="420px">
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
                     <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <div id="chartdiv" align="center">
                    </div>

                    <script type="text/javascript">
                        getGrafico('<%=grafico1_swf %>', "grafico001", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico1_xml %>', 'chartdiv');
                    </script></td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" 
                         
                        Text="Fonte: Superintendência de Planejamento">
                    </dxe:ASPxLabel>
                </td>
            </tr>
        </table>
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table title="<%=toolTip %>" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td align="left">
                               <%=grafico1_titulo %>
                                &nbsp;</td>
                            <td align="right">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../zoom.aspx', 'Desempenho Físico', 'Flashs/AngularGauge.swf', '<%=grafico1_xmlzoom %>', '0')"
                                    src="../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                        </tr>
                    </tbody>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
