<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vm_019.aspx.cs" Inherits="_VisaoMaster_Graficos_vm_019" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 10px;
        }
.dxbButton
{
	color: #000000;
	font: normal 12px Tahoma;
	vertical-align: middle;
	border: 1px solid #7F7F7F;
	padding: 1px;
	cursor: pointer;
}
        </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        

                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                               <div id="chartdiv" align="center">
        </div></td>
                        </tr>
                        <tr>
                <td align="left" valign="top">
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" EncodeHtml="false" 
                         
                        Text="Fonte: Gerência de Informações Gerenciais">
                    </dxe:ASPxLabel>
                </td>
            </tr>
                        <tr>
                            <td class="style2">
                                </td>
                        </tr>
                        <tr>
                            <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharGeral"
                                         Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                            </td>
                        </tr>
        </table>

                    <script type="text/javascript">
                        getGrafico('<%=grafico1_swf %>', "grafico001", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico1_xml %>', 'chartdiv');
                    </script>
        </div>
    </form>
</body>
</html>
