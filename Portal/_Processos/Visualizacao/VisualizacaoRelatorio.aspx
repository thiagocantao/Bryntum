<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizacaoRelatorio.aspx.cs" Inherits="_Processos_Visualizacao_VisualizacaoRelatorio" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">
        function onToolbarItemClick(s, e) {
            var itemName = e.item.name;
            switch (itemName) {
                case "costumizar":
                    var id = documentViewer.cp_IdRelatorio;
                    window.location = '../../Relatorios/DesenhoRelatorio.aspx?custom=S&id=' + id;
                    break;
                case "restaurar":
                    if (confirm(traducao.VisualizacaoRelatorio_deseja_restaurar_layout_original_relatorio + '\n\n' + traducao.VisualizacaoRelatorio_voce_perdera_customizacoes_realizadas + '\n\n' + traducao.VisualizacaoRelatorio_deseja_continuar))
                        callback.PerformCallback("restaurar");
                    break;
                default:
                    break;
            }
        }

        function onCallbackComplete(s, e) {
            window.location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dxxr:ASPxDocumentViewer ID="documentViewer" ClientInstanceName="documentViewer" runat="server" Width="100%" OnCustomJSProperties="documentViewer_CustomJSProperties">
            <ToolbarItems>
                <dxxr:ReportToolbarButton IconID="miscellaneous_design_16x16" Name="costumizar" ToolTip="<% $Resources:traducao, VisualizacaoRelatorio_customizar_layout_relatorio %>" />
                <dxxr:ReportToolbarButton IconID="actions_reset_16x16" Name="restaurar" ToolTip="<% $Resources:traducao, VisualizacaoRelatorio_restaurar %>" />
                <dxxr:ReportToolbarSeparator />
                <dxxr:ReportToolbarButton ItemKind="Search" />
                <dxxr:ReportToolbarSeparator />
                <dxxr:ReportToolbarButton ItemKind="PrintReport" />
                <dxxr:ReportToolbarButton ItemKind="PrintPage" />
                <dxxr:ReportToolbarSeparator />
                <dxxr:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                <dxxr:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                <dxxr:ReportToolbarLabel ItemKind="PageLabel" />
                <dxxr:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                </dxxr:ReportToolbarComboBox>
                <dxxr:ReportToolbarLabel ItemKind="OfLabel" />
                <dxxr:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                <dxxr:ReportToolbarButton ItemKind="NextPage" />
                <dxxr:ReportToolbarButton ItemKind="LastPage" />
                <dxxr:ReportToolbarSeparator />
                <dxxr:ReportToolbarButton ItemKind="SaveToDisk" />
                <dxxr:ReportToolbarButton ItemKind="SaveToWindow" />
                <dxxr:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                    <Elements>
                        <dxxr:ListElement Value="pdf" />
                        <dxxr:ListElement Value="xls" />
                        <dxxr:ListElement Value="xlsx" />
                        <dxxr:ListElement Value="rtf" />
                        <dxxr:ListElement Value="mht" />
                        <dxxr:ListElement Value="html" />
                        <dxxr:ListElement Value="txt" />
                        <dxxr:ListElement Value="csv" />
                        <dxxr:ListElement Value="png" />
                    </Elements>
                </dxxr:ReportToolbarComboBox>
            </ToolbarItems>
            <ClientSideEvents ToolbarItemClick="onToolbarItemClick" />
        </dxxr:ASPxDocumentViewer>
    
    </div>
        <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="onCallbackComplete" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
