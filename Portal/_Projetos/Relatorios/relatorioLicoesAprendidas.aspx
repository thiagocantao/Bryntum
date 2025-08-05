<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relatorioLicoesAprendidas.aspx.cs" Inherits="_Projetos_Relatorios_relatorioLicoesAprendidas" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxxr:reportviewer id="viewer" runat="server" ClientInstanceName="viewer"></dxxr:reportviewer>
    
    </div>
    </form>
</body>
</html>
