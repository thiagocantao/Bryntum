<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_identidadeOrganizacional.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Estrategia_url_identidadeOrganizacional"
    Title="Portal da Estratégia" %>

<%@ Register Assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <style type="text/css">
        .style1 {
            width: 369px;
        }
    </style>
    <script type="text/javascript">
        function onItemClick(s, e) {
            if (e.item.name == 'SaveToDisk' || e.item.name == 'SaveToWindow') {
                var value = reportToolbar.GetItemTemplateControl('SaveFormat').GetValue();
                value = value == null ? "" : value.toLowerCase();
                if (value == 'ppt' || value == 'pptx' || value == 'doc' || value == 'docx') {
                    e.processOnServer = false;
                    callback.PerformCallback(value);
                }
            }
        }

        function OnCallbackComplete(s, e) {
            if (e.result)
                window.location = e.result;
        }

        function init(s) {
            var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
            var createFrameElement = s.printHelper.createFrameElement;

            s.printHelper.createFrameElement = function (name) {
                var frameElement = createFrameElement.call(this, name);
                if (isChrome) {
                    frameElement.addEventListener("load", function (e) {
                        if (frameElement.contentDocument.contentType !== "text/html")
                            frameElement.contentWindow.print();
                    });
                }
                return frameElement;
            }
        }

    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td valign="middle" align="left" style="padding-left: 10px; padding-right: 10px; padding-top: 5px;">
                        <dxcp:ASPxCallbackPanel ID="pnRelatorio" runat="server" ClientInstanceName="pnRelatorio"
                            OnCallback="pnRelatorio_Callback" Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%; padding-bottom: 5px;" cellspacing="0" cellpadding="0"
                                                        border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="bottom" class="style1">
                                                                    <dxxr:ReportToolbar runat="server" ShowDefaultButtons="False" ReportViewer="<%# ReportViewer1 %>"
                                                                        ID="ReportToolbar1" ClientInstanceName="reportToolbar" Width="100%">
                                                                        <Items>
                                                                            <dxxr:ReportToolbarButton Enabled="True" ItemKind="FirstPage" />
                                                                            <dxxr:ReportToolbarButton Enabled="True" ItemKind="PreviousPage" />
                                                                            <dxxr:ReportToolbarLabel ItemKind="PageLabel" Text="Página"></dxxr:ReportToolbarLabel>
                                                                            <dxxr:ReportToolbarComboBox Width="65px" ItemKind="PageNumber">
                                                                            </dxxr:ReportToolbarComboBox>
                                                                            <dxxr:ReportToolbarLabel ItemKind="OfLabel"></dxxr:ReportToolbarLabel>
                                                                            <dxxr:ReportToolbarTextBox ItemKind="PageCount"></dxxr:ReportToolbarTextBox>
                                                                            <dxxr:ReportToolbarButton ItemKind="NextPage"></dxxr:ReportToolbarButton>
                                                                            <dxxr:ReportToolbarButton ItemKind="LastPage"></dxxr:ReportToolbarButton>
                                                                            <dxxr:ReportToolbarSeparator />
                                                                            <dxxr:ReportToolbarButton ItemKind="PrintReport" />
                                                                            <dxxr:ReportToolbarButton ItemKind="SaveToDisk" />
                                                                            <dxxr:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                                                                <Elements>
                                                                                    <dxxr:ListElement Value="pdf" Text="PDF" />
                                                                                    <dxxr:ListElement Text="XLS" Value="xls" />
                                                                                    <dxxr:ListElement Text="XLSX" Value="xlsx" />
                                                                                    <dxxr:ListElement Text="DOC" Value="doc" />
                                                                                    <dxxr:ListElement Text="DOCX" Value="DOCX" />
                                                                                    <dxxr:ListElement Text="PPT" Value="ppt" />
                                                                                    <dxxr:ListElement Text="PPTX" Value="pptx" />
                                                                                </Elements>
                                                                            </dxxr:ReportToolbarComboBox>
                                                                        </Items>
                                                                        <ClientSideEvents ItemClick="function(s, e) {
	onItemClick(s, e);
}" />
                                                                        <Styles>
                                                                            <LabelStyle>
                                                                                <Margins MarginLeft="3px" MarginRight="3px"></Margins>
                                                                            </LabelStyle>
                                                                        </Styles>
                                                                    </dxxr:ReportToolbar>
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td valign="bottom">
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="left" valign="bottom">
                                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblTitulo"
                                                                                    Font-Bold="False" Text="Mapa:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="bottom">
                                                                                <dxe:ASPxComboBox ID="ddlMapa" runat="server" AutoPostBack="True"
                                                                                    ClientInstanceName="ddlMapa"
                                                                                    IncrementalFilteringMode="Contains" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
//	hfGeral.Set(&quot;CodigoMapa&quot;,s.GetSelectedItem().value);

	e.processOnServer = false;
	var option = s.GetSelectedItem().value;
	var nome = s.GetSelectedItem().text;

	var parametro = option + &quot;|&quot; + nome;
	
	hfGeral.Set('CodigoMapaSelecionado', option );
    hfGeral.Set('NomeMapaSelecionado', nome );
	
	pnRelatorio.PerformCallback(parametro);
}" />
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <table cellpadding="0" cellspacing="0"
                                                        style="width: 100%; padding-left: 5px; padding-right: 5px;">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0"
                                                                    style="border-style: none; width: 100%; height: 100%;" width="98%">
                                                                    <tr>
                                                                        <td style="vertical-align: middle; text-align: center; padding-top: 5px;"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="vertical-align: middle;">
                                                                            <dxxr:ReportViewer ID="ReportViewer1" runat="server" AutoSize="False"
                                                                                ClientInstanceName="ReportViewer1" Width="100%">
                                                                                <ClientSideEvents PageLoad="init" />
                                                                                <Paddings PaddingBottom="0px" PaddingLeft="5px" PaddingRight="0px"
                                                                                    PaddingTop="0px" />
                                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </dxxr:ReportViewer>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
            </table>
        </div>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	OnCallbackComplete(s, e);
}" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
