<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_021.aspx.cs" Inherits="grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
<%--    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>--%>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>

    <style type="text/css">
        .style1 {
            width: 129px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center">
            <dxrp:ASPxRoundPanel ID="painel" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho"
                Width="100%" CornerRadius="1px" ClientInstanceName="painel">
                <ContentPaddings Padding="0px" />
                <HeaderStyle Font-Bold="False" BackColor="#EBEBEB" Height="27px">
                    <Paddings PaddingBottom="2px" PaddingTop="2px" />
                </HeaderStyle>
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div id="chartdiv"></div>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
                <HeaderTemplate>
                    <table>
                        <tbody>
                            <tr>
                                <td align="left" class="style1">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                        Text="<%$ Resources:traducao, curva_s_f_sica %>">
                                    </dxe:ASPxLabel>
                                    &nbsp;</td>
                                <td align="right">
                                    <img id="imagemZoom" align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:abrePopupZoom()"
                                        src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                            </tr>
                        </tbody>
                    </table>
                </HeaderTemplate>
            </dxrp:ASPxRoundPanel>
        </div>
        <script type="text/javascript">
            function abrePopupZoom() {
                var sUrl = '../../_Projetos/DadosProjeto/graficos/grafico_021.aspx?FRM=' + painel.cpFRM + '&IDProjeto=' + painel.cpIDProjeto + '&TipoTela=' + painel.cpTipoTela + '&Financeiro=' + painel.cpFinanceiro + '&ehzoom=S';
                if (painel.cpZoom != 'S') {
                    window.top.showModal(sUrl, 'ZOOM', null, null, null, null);
                }                
            }
            function getGraficoZoom(tipoGrafico, largura, altura, paramJSON, paramDiv) {
                FusionCharts.ready(function () {
                    var cSatScoreChart = new FusionCharts({
                        type: tipoGrafico,
                        renderAt: paramDiv,
                        width: largura,
                        height: altura,
                        dataFormat: 'json',
                        dataSource: paramJSON
                    }).render();
                });
            }
            document.getElementById('imagemZoom').style.display = painel.cpZoom != 'S' ? 'block' : 'none';
            var sHeight = Math.max(0, document.documentElement.clientHeight) - 25;
            var sWidth = Math.max(0, document.documentElement.clientWidth) - 25;
            if (painel.cpZoom == 'S')
            {
                sHeight -= 25;
                sWidth -= 25;
            }            
            getGraficoZoom('scrollline2d', sWidth, sHeight, '<%=grafico21_jsonzoom %>', 'chartdiv');
        </script>
    </form>
</body>
</html>
