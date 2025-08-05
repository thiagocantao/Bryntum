<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_001.aspx.cs" Inherits="_Projetos_Agil_graficos_grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../../../scripts/CDIS.js" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center">
            <table cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td align="right">
                            <img style="cursor: pointer" onclick="javascript:f_zoomGrafico('../zoom.aspx', '', 'Flashs/Pie2D.swf', '<%=grafico_xmlzoom %>', '0')"
                                alt="Visualizar gráfico em modo ampliado" src="../../../imagens/zoom.PNG" align="right" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div id="chartdiv" align="center">
                            </div>
                            <script type="text/javascript">
                                //debugger
                                getGrafico('<%=grafico_swf %>', "grafico001", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
                            </script>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
