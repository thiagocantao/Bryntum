<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RiscoQuestaoSelecionada.aspx.cs" Inherits="_Projetos_Relatorios_RiscoQuestaoSelecionada" %>

<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Risco/Questão Selecionada</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow:hidden">
        <dxxr:ReportToolbar ID="ReportToolbar1" runat="server" ClientInstanceName="ReportToolbar1"
             ReportViewer="<%# viewer %>" ShowDefaultButtons="False">
            <Items>
                <dxxr:ReportToolbarButton ItemKind="PrintReport" />
                <dxxr:ReportToolbarButton ItemKind="SaveToDisk" />
                <dxxr:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                    <Elements>
                        <dxxr:ListElement Value="pdf" />
                        <dxxr:ListElement Value="xls" />
                        <dxxr:ListElement Value="xlsx" />
                        <dxxr:ListElement Value="rtf" />
                        <dxxr:ListElement Value="mht" />
                        <dxxr:ListElement Value="txt" />
                        <dxxr:ListElement Value="csv" />
                        <dxxr:ListElement Value="png" />
                    </Elements>
                </dxxr:ReportToolbarComboBox>
            </Items>
            <Styles>
                <LabelStyle>
                    <Margins MarginLeft="3px" MarginRight="3px" />
                </LabelStyle>
            </Styles>
        </dxxr:ReportToolbar>
        <br />
        <div style="overflow: auto; width: 900px; height: 550px;overflow:auto">
            <table border="0" cellpadding="0" cellspacing="0" style="padding-left:10px">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
            <dxxr:ReportViewer ID="viewer" runat="server" ClientInstanceName="viewer">
            </dxxr:ReportViewer>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
