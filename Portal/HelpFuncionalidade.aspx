<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelpFuncionalidade.aspx.cs" Inherits="HelpFuncionalidade" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;height:100%">
    
        <dxhe:ASPxHtmlEditor ID="htmlVisualizaHelp" runat="server" ClientInstanceName="htmlVisualizaHelp" ActiveView="Preview">
            <ClientSideEvents Init="function(s, e) {

var diferencaAltura = (window.outerHeight - window.innerHeight);
var diferencaLargura =  (window.outerWidth - window.innerWidth);
if(diferencaLargura == 0)
{
diferencaLargura = 50;
}
var altura = (window.innerHeight - (diferencaAltura));
var largura = (window.innerWidth - (diferencaLargura));	

s.SetHeight(window.innerHeight - 60);
s.SetWidth(window.innerWidth - 25);
}" />
            <Settings AllowDesignView="False" AllowHtmlView="False" AllowContextMenu="False" />
        </dxhe:ASPxHtmlEditor>
    
        <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: right; padding-top: 10px; padding-right: 15px;">
            <tr>
                <td style="width: 100%;">
                    &nbsp;
                </td>
                <td>
                    <dxcp:ASPxButton ID="btnFechar" runat="server" Text="Fechar" AutoPostBack="False" ClientInstanceName="btnFechar"  Width="100px">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                    </dxcp:ASPxButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
