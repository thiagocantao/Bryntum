<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mt_003.aspx.cs" Inherits="_Portfolios_VisaoMetas_mt_003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function renderizaGrafico()
        {
            vchart.render(document.getElementById('chartdiv'));
        }
        
        function abreAnalise(ano, mes, codigoMeta)
        {
            parent.callback.PerformCallback(ano + ';' + mes + ';' + codigoMeta);
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                <div id="chartdiv" align="center">
                        </div>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtAnalise" runat="server" ClientEnabled="False" Rows="4" Width="100%" ClientInstanceName="txtAnalise">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <dxe:ASPxLabel ID="lblAnalise" runat="server" Font-Italic="True">
                    </dxe:ASPxLabel>
                    &nbsp;</td>
            </tr>
        </table>
        

                    <script type="text/javascript">                          
                          getGrafico('<%=grafico_swf %>', "grafico001", '100%', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
                    </script>                
        </div>
    </form>
</body>
</html>
