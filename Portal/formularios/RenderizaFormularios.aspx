<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RenderizaFormularios.aspx.cs"
    Inherits="formularios_RenderizaFormularios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
</head>
<body style="margin:1px">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfMostraDivPublicar" runat="server" />
        <asp:HiddenField ID="hfPublicarFormulario" runat="server" />
        <asp:Panel ID="pnExterno" runat="server" Style="text-align: left" Width="100%">
        </asp:Panel>
    </form>
</body>
</html>
