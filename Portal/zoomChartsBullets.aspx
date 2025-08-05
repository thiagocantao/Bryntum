<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zoomChartsBullets.aspx.cs" Inherits="zoomBullets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="scripts/CDIS.js"></script>
    <script type="text/javascript" src="scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>

    <script type="text/javascript" language="javascript">
       
    function redimensiona(altura){
        document.getElementById('tbExterna').style.height=altura;
        }    
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 25;
        var sWidth = Math.max(0, document.documentElement.clientWidth) - 25;
        if ('<%=mostrarEsforco %>' != 'N') {
            getGraficoZoom('hbullet', sWidth, sHeight, '<%=grafico1_jsonzoom %>', 'chartdiv1');
        }
        if ('<%=mostrarDespesa %>' != 'N') {
            getGraficoZoom('hbullet', sWidth, sHeight, '<%=grafico2_jsonzoom %>', 'chartdiv2');
        }
        if ('<%=mostrarReceita %>' != 'N') {
            getGraficoZoom('hbullet', sWidth, sHeight, '<%=grafico3_jsonzoom %>', 'chartdiv3');
        }
    </script>

</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div id="Externo" style="width: 100%; height: 100%;">
            <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0"
                id="tbExterna">
                <tr>
                    <td style="width: 100%;" valign="middle" id="menu">
                        <table border="0" cellpadding="0" cellspacing="0" 
                            style="width: 100%; display: none;">
                            <tr>
                                <td style="height: 22px;" align="right">
                                    &nbsp;</td>
                                <td style="width: 50px; height: 22px;">
                                    &nbsp;<asp:Label ID="lblSair" Style="cursor: pointer;" runat="server" Text=" Sair" onClick="javascript:window.parent.fechaModal();" ></asp:Label></td>
                            </tr>
                        </table>
                                    <table border="0" cellpadding="0" cellspacing="3" style="width: 99%" id="tbGraficoPrint">
                                <tr>
                                    <td align="center">
                                    <asp:Label ID="lbl_TituloGrafico" runat="server" Font-Bold="False" Font-Size="11pt"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   <td>                                    
                                     <!-- Gráfico -->
                                     <div id="chartdiv1" align="center" style="width:100%"></div>                                                               
                                   </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px">
                                    </td>
                                </tr>                                
                                <tr>
                                   <td>                                    
                                     <!-- Gráfico -->
                                     <div id="chartdiv2" align="center" style="width:100%"></div>
                                                              
                                  </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                   <td>                                    
                                     <!-- Gráfico -->
                                     <div id="chartdiv3" align="center" style="width:100%"></div>
                                                                      
                                  </td>
                                </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>