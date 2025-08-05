<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dialog.aspx.cs" Inherits="dialog" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
    <script type="text/javascript">
        window.name="myForm";
    </script>
    
</head>
<body>

    <form id="form1" runat="server" target="myForm" >

    <div>
            Here, is not OK. When ASPxButton "Export to XLS" was clicked, nothing happens.<br />
                <br />
                I put the clause "target = 'myForm'" in the FORM tag, because without it was being opened
                another window.<br />
        <br />
                ...&nbsp;
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text='<form id="form1" runat="server" target="myForm" >'>
                </dxe:ASPxLabel>
                <br />
        <br />
        <dxwgv:aspxgridview id="ASPxGridView1" runat="server"></dxwgv:aspxgridview>
    
    </div>
        <dxwgv:aspxgridviewexporter id="ASPxGridViewExporter1" runat="server" gridviewid="ASPxGridView1"></dxwgv:aspxgridviewexporter>
        <dxe:aspxbutton id="ASPxButton1" runat="server" 
            text="ASPxButton">
        </dxe:aspxbutton>
    </form>
</body>
</html>
