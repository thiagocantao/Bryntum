<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="index.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Here, is OK. When the ASPxButton "Export to XLS" was clicked, the file dialog is open.<br />
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server">
        </dxwgv:ASPxGridView>
    
    </div>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
        <dxe:ASPxButton ID="ASPxButton1" runat="server" OnClick="ASPxButton1_Click" Text="Export to XLS">
        </dxe:ASPxButton>
        <hr />
        <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="Open ShowModalDialog">
            <ClientSideEvents Click="function(s, e) {
	window.showModalDialog(&quot;dialog.aspx&quot;, &quot;dialogWidth:800px; dialogHeight:460px; status:no; menubar=no;&quot;);
}" />
        </dxe:ASPxButton>
    </form>
</body>
</html>
