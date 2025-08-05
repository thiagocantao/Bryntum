<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_020.aspx.cs" Inherits="grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>
    <script language="javascript" type="text/javascript">
        function abreDesempenhoFinanceiro(codigoProjeto) {
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/graficos/grafico_019.aspx?Altura=430&Largura=930&CP=' + codigoProjeto, "Curva S Financeira", 980, 490, "", null);
        }
    </script>
    <style type="text/css">


.dxeRadioButtonList
{
	border: 1px Solid #9F9F9F;
}

.dxeRadioButtonList
{
    cursor: default;
}

.dxeRadioButtonList
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxeIRadioButton
{
	margin: 1px;
	display: inline-block;
	vertical-align: middle;
}
        
.dxeIRadioButton
{
    cursor: default;
}

.dxeIRadioButton
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho"
            Width="100%" CornerRadius="1px">
            <ContentPaddings Padding="0px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB" Height="27px">
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
                            <td align="left">
                                <dxe:ASPxRadioButtonList ID="rbGrafico" runat="server" AutoPostBack="True" 
                                     ItemSpacing="10px" 
                                    onselectedindexchanged="rbGrafico_SelectedIndexChanged" 
                                    RepeatDirection="Horizontal" SelectedIndex="0" TextSpacing="2px">
                                    <Paddings Padding="0px" />
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="Desempenho Geral" Value="D" />
                                        <dxe:ListEditItem Text="Curva S Econômica" Value="C" />
                                    </Items>
                                    <Border BorderStyle="None" />
                                </dxe:ASPxRadioButtonList>
                            </td>
                            <td align="left">
                                &nbsp;</td>
                            <td align="right">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoomChart.aspx', '<%=grafico1_titulo %>', '<%=tipoGrafico %>', 'grafico20')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                        </tr>
                    </tbody>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        </div>
         <script type="text/javascript">
             var sHeight = Math.max(0, document.documentElement.clientHeight) - 25;
             var sWidth = Math.max(0, document.documentElement.clientWidth) - 25;
             getGraficoZoom('<%=tipoGrafico %>', sWidth, sHeight, '<%=grafico1_jsonzoom %>', 'chartdiv')
         </script>
    </form>
</body>
</html>
