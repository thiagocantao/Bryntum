<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_002.aspx.cs" Inherits="_Estrategias_indicador_graficos_grafico_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
            var vchartJS;
    </script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table>
            <tr>
                <td>
                    <dxrp:ASPxRoundPanel ID="pGrafico1" runat="server" BackColor="White" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"  HeaderText="Comparação"
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="410px">
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
                                <table>
                                    <tr>
                                        <td>
                                            <div id="chartdiv1" align="center"></div>

                                            <script type="text/javascript">
                                                getGrafico('<%=grafico1_swf %>', "grafico001", '400', '<%=alturaGrafico %>', '<%=grafico1_xml %>', 'chartdiv1');
                                            </script>

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
                                    Text="<%$ Resources:traducao, grafico_002_desempenho_do_indicador %>">
                                            </dxe:ASPxLabel>
                                            &nbsp;</td>
                                        <td align="right">
                                            <img align="right" alt="<%# Resources.traducao.grafico_002_visualizar_gr_fico_em_modo_ampliado %>" onclick="javascript:f_zoomGrafico('../../zoom.aspx', '', 'Flashs/AngularGauge.swf', '<%=grafico1_xmlzoom %>', '0')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr>
                <td style="height: 7px">
                </td>
            </tr>
            <tr>
                <td>
                    <dxrp:ASPxRoundPanel ID="pGrafico2" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue"  HeaderText="Comparação"
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="410px" ClientInstanceName="pGrafico2">
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
                                <table>
                                    <tr>
                                        <td>
                                            <div id="chartdiv2" align="center">
                                            </div>

                                            <script type="text/javascript">
                                                vchartJS = getGrafico('<%=grafico2_swf %>', "grafico002", '400', '<%=alturaGrafico %>', '<%=grafico2_xml %>', 'chartdiv2');
                                            </script>

                                        </td>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <HeaderTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left" style="width: 80px">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="<%$ Resources:traducao, grafico_002_compara__o %>">
                                            </dxe:ASPxLabel>
                                            </td>
                                        <td align="left" style="width: 60px">
                                            <dxe:ASPxRadioButton ID="rbEntidade" runat="server" Checked="True" 
                                                GroupName="Comparacao" Text="<%$ Resources:traducao, grafico_002_brasil %>">
                                                <ClientSideEvents CheckedChanged="function(s, e) {

	if(s.GetChecked())
	{
		vchartJS.setDataXML(pGrafico2.cp_grafico01);
		pGrafico2.cp_graficoZoom = pGrafico2.cp_graficoZoom1;
    }
}" />
                                            </dxe:ASPxRadioButton>
                                        </td>
                                        <td align="left">
                                            <dxe:ASPxRadioButton ID="rbRegiao" runat="server" 
                                                GroupName="Comparacao" Text="<%$ Resources:traducao, grafico_002_regi_o %>">
                                                <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		vchartJS.setDataXML(pGrafico2.cp_grafico02);
		pGrafico2.cp_graficoZoom = pGrafico2.cp_graficoZoom2;
	}
}" />
                                            </dxe:ASPxRadioButton>
                                        </td>
                                        <td align="right">
                                            <img align="right" alt="<%# Resources.traducao.grafico_002_visualizar_gr_fico_em_modo_ampliado %>" onclick="javascript:f_zoomGrafico('../../zoom.aspx', 'Comparação Indicador', 'Flashs/MSCombi2D.swf', pGrafico2.cp_graficoZoom, '0')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
