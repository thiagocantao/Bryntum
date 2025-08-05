<%@ Page Language="C#" AutoEventWireup="true" CodeFile="periodo_002.aspx.cs" Inherits="_Estrategias_indicador_graficos_periodo_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <%--    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>--%>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>
    <script type="text/javascript">

        function zoomGrafico() {

            var w = ASPxClientUtils.GetDocumentClientWidth();
            var h = ASPxClientUtils.GetDocumentClientHeight();

            var sUrl = window.top.pcModal.cp_Path + '_Estrategias/indicador/graficos/periodo_002.aspx?' + hfGeral.Get("urlAtual") + '&w=' + w + '&h=' + h;
            //alert(sUrl);
            var sHeaderTitulo = 'Zoom';
            var sWidth = null;
            var sHeight = null;
            var sFuncaoPosModal = null;
            var objParam = null;

            window.top.showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam);
        }

    </script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center; padding-top: 3px">

            <dxrp:ASPxRoundPanel ID="pGrafico1" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                CssPostfix="PlasticBlue" HeaderText="Comparação"
                ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%">
                <ContentPaddings Padding="0px" PaddingTop="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" />
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

                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>

                    </dxp:PanelContent>
                </PanelCollection>
                <HeaderTemplate>
                    <table>
                        <tbody>
                            <tr>
                                <td align="left">
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:traducao, periodo_002_meta_x_resultado %>">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo"
                                                    SelectedIndex="0" Width="130px"
                                                    AutoPostBack="True">
                                                    <Items>
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, periodo_002_acumulado %>" Value="A" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, periodo_002_status %>" Value="S" />
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <img align="right" alt="<%# Resources.traducao.periodo_002_visualizar_gr_fico_em_modo_ampliado %>" id="imagemClicaZoom" onclick="javascript:zoomGrafico();"
                                        src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                            </tr>
                        </tbody>
                    </table>
                </HeaderTemplate>
            </dxrp:ASPxRoundPanel>

        </div>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
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
