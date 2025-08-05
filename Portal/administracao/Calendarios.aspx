<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="Calendarios.aspx.cs" Inherits="administracao_Calendarios"
    Title="Portal da Estratégia" %>


<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server"
                                Text="Cadastro de Calendários Corporativos">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td id="ConteudoPrincipal">
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoCalendario"
                    AutoGenerateColumns="False" Width="100%"
                    ID="gvDados" OnCustomCallback="gvDados_CustomCallback">
                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                    <Styles>
                        <GroupPanel>
                        </GroupPanel>
                    </Styles>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_MSG != &quot;&quot;)
	{
		window.top.mostraMensagem(s.cp_MSG, 'Atencao', true, false, null);
	}
}"></ClientSideEvents>
                    <SettingsCommandButton>
                        <EditButton>
                            <Image ToolTip="Editar">
                            </Image>
                        </EditButton>
                        <DeleteButton>
                            <Image ToolTip="Excluir">
                            </Image>
                        </DeleteButton>
                    </SettingsCommandButton>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoCalendario" Width="180px" Caption=" "
                            VisibleIndex="0">
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False"
                                AllowHeaderFilter="False" AllowSort="False" />
                            <DataItemTemplate>
                                <%# getBotoes(Eval("CodigoCalendario").ToString(), Eval("IndicaCalendarioPadrao").ToString())  %>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                ClientInstanceName="menu"
                                                ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                OnInit="menu_Init">
                                                <Paddings Padding="0px" />
                                                <Items>
                                                    <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                        <Items>
                                                            <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                <Image Url="~/imagens/menuExportacao/xls.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                ClientVisible="False">
                                                                <Image Url="~/imagens/menuExportacao/html.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                        </Items>
                                                        <Image Url="~/imagens/botoes/btnDownload.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                        <Items>
                                                            <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                <Image IconID="save_save_16x16">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                <Image IconID="actions_reset_16x16">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                        </Items>
                                                        <Image Url="~/imagens/botoes/layout.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                </Items>
                                                <ItemStyle Cursor="pointer">
                                                    <HoverStyle>
                                                        <border borderstyle="None" />
                                                    </HoverStyle>
                                                    <Paddings Padding="0px" />
                                                </ItemStyle>
                                                <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                    <SelectedStyle>
                                                        <border borderstyle="None" />
                                                    </SelectedStyle>
                                                </SubMenuItemStyle>
                                                <Border BorderStyle="None" />
                                            </dxm:ASPxMenu>
                                        </td>
                                    </tr>
                                </table>

                            </HeaderTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoCalendario" Caption="Calend&#225;rio"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings VerticalScrollBarMode="Visible"></Settings>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
    </table>

    <dxpc:ASPxPopupControl ID="pcNovoCalendario" runat="server"
        HeaderText="Novo Calendário" Width="600px" ClientInstanceName="pcNovoCalendario"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowCloseButton="False" ShowFooter="true" Height="165px">
        <FooterTemplate>
            <table style="width:100%">
                <tr>
                    <td align="right">
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Salvar" Width="100px" Theme="MaterialCompact"
                                            ID="btnSalvar" ClientInstanceName="btnSalvar">
                                            <ClientSideEvents Click="function(s, e) {
	pcNovoCalendario.Hide();
	callback.PerformCallback();
}"></ClientSideEvents>
                                            <Paddings Padding="0px"></Paddings>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Fechar" Width="100px" Theme="MaterialCompact"
                                            ID="btnFechar" ClientInstanceName="btnFechar">
                                            <ClientSideEvents Click="function(s, e) {
	pcNovoCalendario.Hide();
}"></ClientSideEvents>
                                            <Paddings Padding="0px"></Paddings>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </FooterTemplate>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <div class="rolagem-tab">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tbody>
                            <tr>
                                <td rowspan="1">
                                    <dxe:ASPxLabel runat="server" Text="Calend&#225;rio Base:"
                                        ID="ASPxLabel1">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%"
                                        ID="ddlCalendarioBase">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	gvDados.PerformCallback();
	abreEdicao(s.cp_NovoCodigo);
}" />
    </dxcb:ASPxCallback>

    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
        ID="ASPxGridViewExporter1"
        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        <Styles>
            <Default></Default>

            <Header></Header>

            <Cell></Cell>

            <GroupFooter Font-Bold="True"></GroupFooter>

            <Title Font-Bold="True"></Title>
        </Styles>
    </dxwgv:ASPxGridViewExporter>

</asp:Content>
