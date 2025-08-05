<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="indicadorObjetivo.aspx.cs" Inherits="_Estrategias_wizard_indicadorObjetivo" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Objetivos Estratégicos X Indicadores"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="padding-top: 5px; padding-right: 5px; padding-left: 5px;">
                <dxrp:ASPxRoundPanel ID="rpFiltro" runat="server" ClientInstanceName="rpFiltro"
                    HeaderText="Filtro" Width="780px">
                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel runat="server" Text="Mapa Estrat&#233;gico:"
                                                ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlMapa"
                                                 ID="ddlMapa">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback('Atualizar');
}"></ClientSideEvents>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 5px; padding-left: 5px; padding-top: 5px">
                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                    KeyFieldName="CodigoIndicador" Width="100%" 
                    OnRowDeleting="gvDados_RowDeleting" OnCellEditorInitialize="gvDados_CellEditorInitialize"
                    OnRowInserting="gvDados_RowInserting" OnCommandButtonInitialize="gvDados_CommandButtonInitialize"
                    OnHtmlRowCreated="gvDados_HtmlRowCreated">
                    <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
                    </SettingsEditing>
                    <SettingsPopup>
                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                            AllowResize="True" Width="580px" />
                    </SettingsPopup>
                    <SettingsText ConfirmDelete="Deseja realmente excluir o registro?" PopupEditFormCaption="Objetivos Estrat&#233;gicos X Indicadores">
                    </SettingsText>
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="10%" Caption=" " VisibleIndex="0"
                            ShowDeleteButton="true">
                            <HeaderTemplate>
                                <%# string.Format(@"<table style='width:100%'><tr><td align='left'><img src='../../imagens/botoes/incluirReg02.png' title='Novo' onclick='gvDados.AddNewRow();' style='cursor: pointer;'/></td></tr></table>")%>
                            </HeaderTemplate>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoObjetoEstrategia" Width="45%" Caption="Objetivo Estrat&#233;gico"
                            VisibleIndex="1">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Width="45%" Caption="Indicador"
                            VisibleIndex="2">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoEstrategia" Visible="False"
                            VisibleIndex="3">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn ReadOnly="True" Name="mapa" Caption="Mapa Estrat&#233;gico"
                            Visible="False" VisibleIndex="3">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle BackColor="#E0E0E0">
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" VisibleIndex="0" CaptionLocation="Top" Caption="Mapa Estrat&#233;gico:">
                            </EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="DescricaoObjetoEstrategia" Name="objetivo"
                            Caption="Objetivo Estrat&#233;gico" Visible="False" VisibleIndex="3">
                            <PropertiesComboBox ValueType="System.String">
                                <ClientSideEvents Validation="function(s, e) {
	if(e.value == null)
	{
		e.isValid = false;
		e.errorText = 'Campo Obrigat&#243;rio';
	}
	else
		e.isValid = true;
}"></ClientSideEvents>
                                <ValidationSettings ErrorText="Campo Obrigat&#243;rio" ValidationGroup="MKE">
                                    <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" VisibleIndex="1" CaptionLocation="Top" Caption="Objetivo Estrat&#233;gico:">
                            </EditFormSettings>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="NomeIndicador" Name="indicador" Caption="Indicador"
                            Visible="False" VisibleIndex="3">
                            <PropertiesComboBox ValueType="System.String">
                                <ClientSideEvents Validation="function(s, e) {
	if(e.value == null)
	{
		e.isValid = false;
		e.errorText = 'Campo Obrigat&#243;rio';
	}
	else
		e.isValid = true;
}"></ClientSideEvents>
                                <ValidationSettings ErrorText="Campo Obrigat&#243;rio" ValidationGroup="MKE">
                                    <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" VisibleIndex="2" CaptionLocation="Top" Caption="Indicador:">
                            </EditFormSettings>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Name="CodigoProjeto" Visible="False"
                            VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings VerticalScrollBarMode="Visible"></Settings>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 5px; padding-left: 5px; padding-top: 5px">
                <dxrp:ASPxRoundPanel ID="pLegenda" runat="server" 
                    HeaderText="Legenda" HorizontalAlign="Center" Width="300px">
                    <ContentPaddings Padding="1px"></ContentPaddings>
                    <HeaderStyle Font-Bold="True">
                        <Paddings Padding="1px" PaddingLeft="3px"></Paddings>
                    </HeaderStyle>
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 20px">
                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/excluirRegDes.png" ID="ASPxImage1">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <span style="">
                                                <asp:Label runat="server" Text="Indicador associado a iniciativas no objetivo."
                                                    Font-Size="7pt" ID="Label1" EnableViewState="False"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
            </td>
        </tr>
    </table>
</asp:Content>
