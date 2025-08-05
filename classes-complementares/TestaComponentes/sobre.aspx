<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sobre.aspx.cs" Inherits="sobre" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dxhe" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dxwsc" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        window.name="modal";
        var existeConteudoCampoAlterado = false;
    </script>    
    
        <script type="text/javascript">
        function anexo()
        {
            var retorno = window.showModalDialog('formulario_Anexo.aspx', '', 'resizable:0; dialogWidth:620px; dialogHeight:250px; status:no; menubar=no;');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" target="modal">
        <dxwgv:aspxgridviewexporter id="ASPxGridViewExporter1" runat="server" gridviewid="ASPxGridView1"></dxwgv:aspxgridviewexporter>
    </form>
</body>
</html>
