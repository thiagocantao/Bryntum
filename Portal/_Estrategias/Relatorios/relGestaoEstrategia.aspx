<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relGestaoEstrategia.aspx.cs" Inherits="_Estrategias_Relatorios_relGestaoEstrategia"
    Title="Portal da Estratégia" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
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
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style="height: 26px">
                <td valign="top" align="right">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
                        width: 100%">
                        <tr>
                            <td align="right" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                height: 26px">
                                <table>
                                    <tr>
                                        <td align="left" valign="middle" style="padding-left: 10px">
                                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela"
                                                Font-Bold="True"  Text="Relatório de Gestão da Estratégia">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="middle">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                                                 Text="Mapa:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" style="width: 450px; padding-right: 5px;" valign="middle">
                                            <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa"
                                                Width="445px">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                e.processOnServer = false;
	                                                //hfGeral.Set(&quot;CodigoMapa&quot;,s.GetSelectedItem().value);
	                                                var option = s.GetSelectedItem().value;
	                                                var nome = s.GetSelectedItem().text;

	                                                var parametro = option + &quot;|&quot; + nome;
	
	                                                hfGeral.Set('CodigoMapaSelecionado', option );
                                                    hfGeral.Set('NomeMapaSelecionado', nome );
	
	                                                pnRelatorio.PerformCallback(parametro);
                                                }"></ClientSideEvents>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 26px">
                <td valign="middle" align="left" style="padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <table style="width: 100%; padding-bottom: 5px;" cellspacing="0" cellpadding="0"
                                        border="0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 354px">
                                                    <dx:ReportToolbar runat="server" ReportViewer="<%# ReportViewer1 %>"
                                                        Width="485px" ID="ReportToolbar2" ClientInstanceName="reportToolbar">
                                                        <Items>
                                                            <dx:ReportToolbarButton ItemKind="Search"></dx:ReportToolbarButton>
                                                            <dx:ReportToolbarSeparator></dx:ReportToolbarSeparator>
                                                            <dx:ReportToolbarButton ItemKind="PrintReport"></dx:ReportToolbarButton>
                                                            <dx:ReportToolbarButton ItemKind="PrintPage"></dx:ReportToolbarButton>
                                                            <dx:ReportToolbarSeparator />
                                                            <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                                            <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                                            <dx:ReportToolbarLabel ItemKind="PageLabel" Text="Página"></dx:ReportToolbarLabel>
                                                            <dx:ReportToolbarComboBox Width="65px" ItemKind="PageNumber">
                                                            </dx:ReportToolbarComboBox>
                                                            <dx:ReportToolbarLabel ItemKind="OfLabel"></dx:ReportToolbarLabel>
                                                            <dx:ReportToolbarTextBox ItemKind="PageCount"></dx:ReportToolbarTextBox>
                                                            <dx:ReportToolbarButton ItemKind="NextPage"></dx:ReportToolbarButton>
                                                            <dx:ReportToolbarButton ItemKind="LastPage"></dx:ReportToolbarButton>
                                                            <dx:ReportToolbarSeparator />
                                                            <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                                                            <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                                                            <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                                                <Elements>
                                                                    <dx:ListElement Value="pdf" Text="PDF" />
                                                                    <dx:ListElement Text="XLS" Value="xls" />
                                                                    <dx:ListElement Text="XLSX" Value="xlsx" />
                                                                    <dx:ListElement Text="DOC" Value="doc" />
                                                                    <dx:ListElement Text="DOCX" Value="DOCX" />
                                                                    <dx:ListElement Text="PPT" Value="ppt" />
                                                                    <dx:ListElement Text="PPTX" Value="pptx" />
                                                                </Elements>
                                                            </dx:ReportToolbarComboBox>
                                                        </Items>
                                                        <clientsideevents itemclick="function(s, e) {
	onItemClick(s, e);
}" />
                                                        <Styles>
                                                            <LabelStyle>
                                                                <Margins MarginLeft="3px" MarginRight="3px"></Margins>
                                                            </LabelStyle>
                                                        </Styles>
                                                    </dx:ReportToolbar>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="width: 100%; padding-left: 5px; padding-right: 5px;">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="border-style: solid; border-width: thin;
                                    border-color: #000000; width: 100%; height: 100%; background-color: #DDDAD5;">
                                    <tr>
                                        <td style="vertical-align: middle; text-align: center">
                                        </td>
                                        <td style="vertical-align: middle; text-align: center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tdRelatorio" runat="server" style="vertical-align: middle; text-align: center">
                                            &nbsp;
                                        </td>
                                        <td style="vertical-align: middle; text-align: center">
                                            <dxcp:ASPxCallbackPanel ID="pnRelatorio" runat="server" ClientInstanceName="pnRelatorio"
                                                OnCallback="pnRelatorio_Callback">
                                                <PanelCollection>
                                                    <dxp:PanelContent ID="PanelContent1" runat="server">
                                                        <dx:ReportViewer ID="ReportViewer1" runat="server" AutoSize="False" ClientInstanceName="ReportViewer1"
                                                            Width="820px">
                                                            <Paddings PaddingBottom="0px" PaddingLeft="5px" PaddingRight="0px" PaddingTop="0px" />
                                                            <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                        </dx:ReportViewer>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxcp:ASPxCallbackPanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
</asp:Content>
