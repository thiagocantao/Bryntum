<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getResolucaoCliente.aspx.cs" Inherits="getResolucaoCliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <title>Brisk PPM</title>
</head>
<body>
    <input id="urlMobile" name="urlMobile" type="hidden" value="<%=urlMobile %>" />
    <form id="form1" runat="server">
    <dxhf:ASPxHiddenField ID="ASPxHiddenField1" ClientInstanceName="hfRes" runat="server">
    </dxhf:ASPxHiddenField>
    </form>
    <script src="./scripts/getResolucaoCliente.js"></script>
</body>
</html>