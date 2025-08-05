<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="identidadeOrganizacional.aspx.cs" Inherits="_Estrategias_Relatorios_identidadeOrganizacional" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function onItemClick(s, e) {
            if (e.item.name == 'SaveToDisk' || e.item.name == 'SaveToWindow') {
                var value = reportToolbar.GetItemTemplateControl('SaveFormat').GetValue();
                value = value == null ? "" : value.toLowerCase();
                if (value == 'ppt' || value == 'pptx') {
                    e.processOnServer = false;
                    callback.PerformCallback(value);
                }
            }
        }

        function OnCallbackComplete(s, e) {
            window.location = e.result;
        }
    </script>
</asp:Content>
<asp:Content id="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0"
            width="100%">
            <tr style="height:26px">
                <td valign="top" align="right">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
                        width: 100%">
                        <tr>
                            <td align="right" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); height: 26px">
                                <table>
                                    <tr>
                                        <td align="left" valign="middle" style="width: 379px; padding-left: 10px;">
                                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" 
                                                ClientInstanceName="lblTituloTela" Font-Bold="True"
                         Text="Planejamento Estratégico">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="middle">
                                            <dxe:ASPxLabel runat="server" Text="Mapa:" ClientInstanceName="lblTitulo" Font-Bold="False"  ID="ASPxLabel1"></dxe:ASPxLabel>

                                        </td>
                                        <td align="left" valign="middle" style="width: 350px">
                                            <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                                                Width="100%" AutoPostBack="True" ClientInstanceName="ddlMapa" 
                                                 ID="ddlMapa">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
//	hfGeral.Set(&quot;CodigoMapa&quot;,s.GetSelectedItem().value);

	e.processOnServer = false;
	var option = s.GetSelectedItem().value;
	var nome = s.GetSelectedItem().text;

	var parametro = option + &quot;|&quot; + nome;
	
	hfGeral.Set(&#39;CodigoMapaSelecionado&#39;, option );
    hfGeral.Set(&#39;NomeMapaSelecionado&#39;, nome );
	
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
            <tr style="height:26px">
                <td valign="middle" align="left" style="padding-left: 10px; padding-right: 10px; padding-top: 5px;">
                    <dxcp:aspxcallbackpanel id="pnRelatorio" runat="server" clientinstancename="pnRelatorio"
                        oncallback="pnRelatorio_Callback" width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0" ><tbody>
    <TR ><td ><table style="WIDTH: 100%; padding-bottom: 5px;" cellspacing="0" 
            cellpadding="0" border="0" ><tbody><TR ><td style="WIDTH: 350px" >
            <dx:ReportToolbar runat="server" ShowDefaultButtons="False" 
                ReportViewer="<%# ReportViewer1 %>" ID="ReportToolbar1" ClientInstanceName="reportToolbar"  ><Items>
<dx:ReportToolbarButton ItemKind="Search"></dx:ReportToolbarButton>
<dx:ReportToolbarSeparator></dx:ReportToolbarSeparator>
<dx:ReportToolbarButton ItemKind="PrintReport"></dx:ReportToolbarButton>
<dx:ReportToolbarButton ItemKind="PrintPage"></dx:ReportToolbarButton>
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                    <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
<dx:ReportToolbarLabel ItemKind="PageLabel"></dx:ReportToolbarLabel>
<dx:ReportToolbarComboBox Width="65px" ItemKind="PageNumber"></dx:ReportToolbarComboBox>
<dx:ReportToolbarLabel ItemKind="OfLabel"></dx:ReportToolbarLabel>
<dx:ReportToolbarTextBox ItemKind="PageCount"></dx:ReportToolbarTextBox>
<dx:ReportToolbarButton ItemKind="NextPage"></dx:ReportToolbarButton>
<dx:ReportToolbarButton ItemKind="LastPage"></dx:ReportToolbarButton>
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                    <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                    <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                        <elements>
                            <dx:ListElement Value="pdf" />
                            <dx:ListElement Text="PPT" Value="ppt" />
                            <dx:ListElement Text="PPTX" Value="pptx" />
                        </elements>
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
        </td>
        <td >
            &nbsp;</td></tr></tbody></table></td></tr><TR >
        <td style="border-style: solid; border-width: thin; OVERFLOW: auto; PADDING-TOP: 5px; background-color: #DDDAD5;" 
            align="center" ><dx:ReportViewer runat="server" 
                ClientInstanceName="ReportViewer1" AutoSize="False" Width="800px" Height="100%" 
                ID="ReportViewer1" style="overflow: scroll">
<Paddings PaddingLeft="5px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                <border bordercolor="Black" borderstyle="Solid" borderwidth="1px"></border>
</dx:ReportViewer>
 </td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxcp:aspxcallbackpanel>
                    
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