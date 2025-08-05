<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesenhoRelatorio.aspx.cs" Inherits="Relatorios_DesenhoRelatorio" UICulture="pt-BR" %>

<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxReportDesigner ID="reportDesigner" runat="server" ClientInstanceName="reportDesigner" OnSaveReportLayout="reportDesigner_SaveReportLayout">
        </dx:ASPxReportDesigner>
    
    </div>
    </form>
</body>
</html>
