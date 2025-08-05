<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_Relatorio.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_Relatorio" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        function atualizaDados()
        {            
            window.location.reload();
        }
    </script>
</head>
<body style="margin-top:0; overflow:hidden" class="body">
    <form id="form1" runat="server">
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <br />
                    <dxxr:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False"  ReportViewer="<%# ReportViewer1 %>">
                        <Items>
                            <dxxr:ReportToolbarButton ItemKind='Search' />
                            <dxxr:ReportToolbarSeparator />
                            <dxxr:ReportToolbarButton ItemKind='PrintReport' />
                            <dxxr:ReportToolbarButton ItemKind='PrintPage' />
                            <dxxr:ReportToolbarSeparator />
                            <dxxr:ReportToolbarButton Enabled='False' ItemKind='FirstPage' />
                            <dxxr:ReportToolbarButton Enabled='False' ItemKind='PreviousPage' />
                            <dxxr:ReportToolbarLabel ItemKind='PageLabel' />
                            <dxxr:ReportToolbarComboBox ItemKind='PageNumber' Width='65px'>
                            </dxxr:ReportToolbarComboBox>
                            <dxxr:ReportToolbarLabel ItemKind='OfLabel' />
                            <dxxr:ReportToolbarTextBox IsReadOnly='True' ItemKind='PageCount' />
                            <dxxr:ReportToolbarButton ItemKind='NextPage' />
                            <dxxr:ReportToolbarButton ItemKind='LastPage' />
                            <dxxr:ReportToolbarSeparator />
                            <dxxr:ReportToolbarButton ItemKind='SaveToDisk' />
                            <dxxr:ReportToolbarButton ItemKind='SaveToWindow' />
                            <dxxr:ReportToolbarComboBox ItemKind='SaveFormat' Width='70px'>
                                <Elements>
                                    <dxxr:ListElement Value='pdf' />
                                    <dxxr:ListElement Value='xls' />
                                    <dxxr:ListElement Value='xlsx' />
                                    <dxxr:ListElement Value='rtf' />
                                    <dxxr:ListElement Value='mht' />
                                    <dxxr:ListElement Value='txt' />
                                    <dxxr:ListElement Value='csv' />
                                    <dxxr:ListElement Value='png' />
                                </Elements>
                            </dxxr:ReportToolbarComboBox>
                        </Items>
                        <Styles>
                            <LabelStyle>
                                <Margins MarginLeft='3px' MarginRight='3px' />
                            </LabelStyle>
                        </Styles>
                    </dxxr:ReportToolbar>
        &nbsp;
        <div style="width: <%=larguraTela %>; height: <%=alturaTela%>;overflow:auto">
                                       <dxxr:ReportViewer ID="ReportViewer1" runat="server">
                    </dxxr:ReportViewer>
        </div>
    </form>
</body>
</html>
