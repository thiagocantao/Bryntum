<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_003.aspx.cs" Inherits="_Estrategias_indicador_graficos_grafico_003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>
    <script type="text/javascript" language="javascript">        

        function zoomGrafico() {

            var w = Math.max(0, document.documentElement.clientWidth);
            var h = Math.max(0, document.documentElement.clientHeight);

            var sUrl = window.top.pcModal.cp_Path + '_Estrategias/indicador/graficos/grafico_003.aspx?' + hfGeral.Get("urlAtual") + '&w=' + w + '&h=' + h;
            //alert(sUrl);
            

            var sHeaderTitulo = 'Zoom';
            var sWidth = null;
            var sHeight = null;
            var sFuncaoPosModal = null;
            var objParam = null;


            window.top.showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam);
        }
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center; width: 100%">
            <table style="width: 100%">
                <tr>
                    <td>
                        <dxrp:ASPxRoundPanel ID="pGrafico1" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                            CssPostfix="PlasticBlue" HeaderText="Comparação"
                            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%">
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
                                    <table style="width:100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                                </dxtv:ASPxHiddenField>
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
                                                    Text="<%$ Resources:traducao, grafico_003_desempenho_do_indicador %>">
                                                </dxe:ASPxLabel>
                                                &nbsp;</td>
                                            <td align="right">
                                                <img align="right" id="imagemClicaZoom" alt="<%# Resources.traducao.grafico_003_visualizar_gr_fico_em_modo_ampliado %>" onclick="javascript:zoomGrafico();"
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
        <script type="text/javascript">
            const urlParams = new URLSearchParams(window.location.search);
            const widthx = urlParams.get('w');
            if (widthx !== null) {
                document.getElementById('imagemClicaZoom').style.display = 'none';
            }
            else {
                document.getElementById('imagemClicaZoom').style.display = 'block';
            }
        </script>
    </form>
</body>
</html>
