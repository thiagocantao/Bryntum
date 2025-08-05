<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="LDAP.aspx.cs" Inherits="administracao_LDAP" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Parâmetros e Configurações - PBH"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td></td>
            <td style="height: 10px"></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoParametro"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                OnCustomErrorText="gvDados_CustomErrorText">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) 
{
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
        }
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="95px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="GrupoParametro" GroupIndex="0"
                                        SortIndex="0" SortOrder="Ascending" Width="200px" Caption="Grupo"
                                        VisibleIndex="1">
                                        <Settings AllowGroup="True" AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoParametro_PT"
                                        Caption="Par&#226;metro" VisibleIndex="2">
                                        <Settings AllowGroup="False" AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoValor" Caption="Valor"
                                        VisibleIndex="5">
                                        <Settings AllowGroup="False" AllowAutoFilter="False"></Settings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TipoDadoParametro"
                                        Name="TipoDadoParametro" Width="200px" Caption="Tipo" Visible="False"
                                        VisibleIndex="3">
                                        <Settings AllowGroup="False"></Settings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Valor" Visible="False" VisibleIndex="4">
                                        <Settings AllowGroup="False"></Settings>
                                    </dxwgv:GridViewDataTextColumn>

                                    <dxtv:GridViewDataTextColumn Caption="IndicaControladoSistema"
                                        FieldName="IndicaControladoSistema" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="6">
                                    </dxtv:GridViewDataTextColumn>

                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>
                                <Styles>
                                    <GroupPanel>
                                    </GroupPanel>
                                </Styles>
                            </dxwgv:ASPxGridView>

                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="730px"
                                ID="pcDados">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <dxtv:ASPxCallbackPanel ID="pnCallbackPopup" runat="server" Width="100%"
                                            ClientInstanceName="pnCallbackPopup" OnCallback="pnCallbackPopup_Callback">
                                            <PanelCollection>
                                                <dxtv:PanelContent ID="PanelContent3" runat="server">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxtv:ASPxPanel ID="pnFormulario2" runat="server" Style="overflow: auto"
                                                                        Width="100%">
                                                                        <PanelCollection>
                                                                            <dxtv:PanelContent runat="server">
                                                                                <table cellpadding="0" cellspacing="0" style="width: 710px">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span style="">
                                                                                                    <asp:Label ID="lblValor" runat="server" Text="Valor:"></asp:Label>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxTextBox ID="txtValorTXT" runat="server"
                                                                                                    ClientInstanceName="txtValorTXT"
                                                                                                    MaxLength="250" Width="100%">
                                                                                                    <ValidationSettings>
                                                                                                        <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                                        </ErrorImage>
                                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                        </ErrorFrameStyle>
                                                                                                    </ValidationSettings>
                                                                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </ReadOnlyStyle>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxTextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </dxtv:PanelContent>
                                                                        </PanelCollection>
                                                                    </dxtv:ASPxPanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Label ID="lblDescricao" runat="server" Text="Parâmetro:"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxtv:ASPxMemo ID="memoDescricao" runat="server"
                                                                        ClientInstanceName="memoDescricao" EnableClientSideAPI="True"
                                                                        ReadOnly="True" Rows="5" Width="100%">
                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </ReadOnlyStyle>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxtv:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <table id="tblSalvarFechar" border="0" cellpadding="0" cellspacing="0">
                                                                        <tbody>
                                                                            <tr style="height: 35px">
                                                                                <td>
                                                                                    <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnSalvar"
                                                                                        Text="Salvar" Width="90px">
                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                                        <Paddings Padding="0px" />
                                                                                    </dxtv:ASPxButton>
                                                                                </td>
                                                                                <td></td>
                                                                                <td align="right">
                                                                                    <dxtv:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnFechar"
                                                                                        Text="Fechar" Width="90px">
                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                                        <Paddings Padding="0px" />
                                                                                    </dxtv:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxtv:PanelContent>
                                            </PanelCollection>
                                        </dxtv:ASPxCallbackPanel>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                                PaperKind="A4" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                <Styles>
                                    <Header Font-Bold="True">
                                    </Header>
                                    <Cell>
                                    </Cell>
                                    <GroupRow Font-Bold="True">
                                    </GroupRow>
                                </Styles>
                            </dxwgv:ASPxGridViewExporter>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
