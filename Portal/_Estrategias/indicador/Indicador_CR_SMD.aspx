<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="Indicador_CR_SMD.aspx.cs" Inherits="_Estrategias_indicador_Indicador_CR_SMD" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Critérios de Seleção"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 10px; height: 10px"></td>
            <td style="height: 10px">&nbsp;
            </td>
            <td style="width: 10px; height: 10px;"></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoIndicador"
                                PreviewFieldName="CodigoIndicador" AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) 
{    
     gvDados.SetFocusedRowIndex(e.visibleIndex);
    
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
	 }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
        desabilitaHabilitaComponentes();
		pcDados.Show();
     }	
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir" Visibility="Invisible">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
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
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Caption="Indicador" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Caption="CodigoIndicador"
                                        Visible="False" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible"></Settings>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Relacionamento Centro de Resultado" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="600px"
                                ID="pcDados">
                                <CloseButtonImage Width="17px">
                                </CloseButtonImage>
                                <SizeGripImage Width="12px">
                                </SizeGripImage>
                                <ClientSideEvents CloseUp="function(s, e) {
     
    txtIndicador.SetText(&quot;&quot;);
  gridCR.PerformCallback(&quot;Limpar&quot;);

	
}" />
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        <dxp:ASPxPanel runat="server" Width="100%" ID="pnFormulario" Style="overflow: auto">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Indicador:"
                                                                                        ID="ASPxLabel1">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="60" ClientInstanceName="txtIndicador"
                                                                                        ID="txtIndicador">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gridCR" KeyFieldName="ID"
                                                                                        AutoGenerateColumns="False" Width="100%"
                                                                                        ID="gridCR" OnRowUpdating="gridCR_RowUpdating" OnRowInserting="gridCR_RowInserting"
                                                                                        OnRowDeleting="gridCR_RowDeleting" OnCustomCallback="gridCR_CustomCallback">
                                                                                        <ClientSideEvents EndCallback="function(s, e) {
   if (s.cp_msg != ''){
	if(s.cp_status == 'ok')
        window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
	else
		window.top.mostraMensagem(s.cp_msg, 'erro', true, false, null);
               s.cp_msg = '';
         }
}" />
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" Caption="Ac&#245;es"
                                                                                                ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true" ShowUpdateButton="true"
                                                                                                VisibleIndex="0">
                                                                                                <HeaderTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <dxm:ASPxMenu ID="menu1" runat="server" BackColor="Transparent" ClientInstanceName="menu1"
                                                                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick1" OnInit="menu_Init1">
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
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="CR" Width="50px" Caption="Centro de Resultado"
                                                                                                VisibleIndex="1" UnboundType="Decimal">
                                                                                                <PropertiesTextEdit>
                                                                                                    <ReadOnlyStyle BackColor="#E8E8E8" ForeColor="Black">
                                                                                                    </ReadOnlyStyle>
                                                                                                    <ValidationSettings ErrorText="Dado obrigat&#243;rio!" ErrorDisplayMode="None">
                                                                                                        <RequiredField ErrorText="Dado obrigat&#243;rio!"></RequiredField>
                                                                                                    </ValidationSettings>
                                                                                                    <Style></Style>
                                                                                                </PropertiesTextEdit>
                                                                                                <Settings AllowSort="False" AllowAutoFilter="True" AutoFilterCondition="Contains"
                                                                                                    FilterMode="DisplayText" />
                                                                                                <Settings AllowSort="False"></Settings>
                                                                                                <EditFormSettings VisibleIndex="0" CaptionLocation="Top" Caption="Centro de Resultado:"
                                                                                                    Visible="True"></EditFormSettings>
                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                <CellStyle HorizontalAlign="Center">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="CodigoIndicador" FieldName="CodigoIndicador"
                                                                                                Name="CodigoIndicador" ReadOnly="True" ShowInCustomizationForm="False" Visible="False"
                                                                                                VisibleIndex="2">
                                                                                                <EditFormSettings Visible="False" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" ReadOnly="True"
                                                                                                ShowInCustomizationForm="False" Visible="False" VisibleIndex="3">
                                                                                                <EditFormSettings Visible="False" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowSort="False" AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3">
                                                                                        </SettingsEditing>
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="600px" />
                                                                                        </SettingsPopup>
                                                                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="250" ShowFilterRow="True"></Settings>
                                                                                        <SettingsText Title=" &amp;nbsp;" ConfirmDelete="Excluir este Centro de Resultado?"
                                                                                            PopupEditFormCaption="Edi&#231;ao/Novo"></SettingsText>
                                                                                        <Styles>
                                                                                            <TitlePanel BackColor="White" ForeColor="Black">
                                                                                            </TitlePanel>
                                                                                        </Styles>
                                                                                        <Templates>
                                                                                            <FooterRow>
                                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="right">
                                                                                                            <%# gridCR.VisibleRowCount %>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </FooterRow>
                                                                                        </Templates>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 48px" align="right">
                                                        <table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr style="height: 35px">
                                                                    <td>
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                            Text="Salvar" Width="90px" ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="width: 70px" align="right">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="90px" ID="btnFechar">
                                                                            <ClientSideEvents Click="function(s, e) {
      gridCR.CancelEdit();
      e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                <Styles>
                                    <Default>
                                    </Default>
                                    <Header>
                                    </Header>
                                    <Cell>
                                    </Cell>
                                    <GroupFooter Font-Bold="True">
                                    </GroupFooter>
                                    <Title Font-Bold="True"></Title>
                                </Styles>
                            </dxwgv:ASPxGridViewExporter>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <SettingsLoadingPanel Text=" "></SettingsLoadingPanel>
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
