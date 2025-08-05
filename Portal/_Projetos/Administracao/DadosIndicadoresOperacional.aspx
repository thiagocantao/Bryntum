<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="DadosIndicadoresOperacional.aspx.cs" Inherits="_Projetos_Administracao_DadosIndicadoresOperacional" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);">
        <tr style="height: 26px">
            <td valign="middle" style="height: 26px; padding-left: 10px;">
                <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False" Text="Dados de Indicadores de Projeto"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td></td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoDado"
                                AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) 
{
	gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
	 }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
	 }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
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
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderTemplate>
                                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo Indicador Operacional"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>")%>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoDado" Caption="Descri&#231;&#227;o"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoDado" Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="GlossarioDado" Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeMedida" Visible="False" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoFuncaoAgrupamentoDado" Visible="False"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Codigo Reservado" FieldName="CodigoReservado"
                                        Visible="False" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords">
                                    <AllButton>
                                        <Image Width="27px">
                                        </Image>
                                    </AllButton>
                                    <FirstPageButton>
                                        <Image Width="23px">
                                        </Image>
                                    </FirstPageButton>
                                    <LastPageButton>
                                        <Image Width="23px">
                                        </Image>
                                    </LastPageButton>
                                    <NextPageButton>
                                        <Image Width="19px">
                                        </Image>
                                    </NextPageButton>
                                    <PrevPageButton>
                                        <Image Width="19px">
                                        </Image>
                                    </PrevPageButton>
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible"></Settings>
                                <SettingsText></SettingsText>
                                <Images>
                                    <CollapsedButton Width="15px">
                                    </CollapsedButton>
                                    <ExpandedButton Width="15px">
                                    </ExpandedButton>
                                    <DetailCollapsedButton Width="15px">
                                    </DetailCollapsedButton>
                                    <DetailExpandedButton Width="15px">
                                    </DetailExpandedButton>
                                    <HeaderFilter Width="19px">
                                    </HeaderFilter>
                                    <HeaderActiveFilter Width="19px">
                                    </HeaderActiveFilter>
                                    <HeaderSortDown Width="7px">
                                    </HeaderSortDown>
                                    <HeaderSortUp Width="7px">
                                    </HeaderSortUp>
                                    <FilterRowButton Width="13px">
                                    </FilterRowButton>
                                    <WindowResizer Width="13px">
                                    </WindowResizer>
                                </Images>
                                <ImagesEditors>
                                    <CalendarFastNavPrevYear Width="19px">
                                    </CalendarFastNavPrevYear>
                                    <CalendarFastNavNextYear Width="19px">
                                    </CalendarFastNavNextYear>
                                    <DropDownEditDropDown Width="9px">
                                    </DropDownEditDropDown>
                                    <SpinEditIncrement Width="7px">
                                    </SpinEditIncrement>
                                    <SpinEditDecrement Width="7px">
                                    </SpinEditDecrement>
                                    <SpinEditLargeIncrement Width="7px">
                                    </SpinEditLargeIncrement>
                                    <SpinEditLargeDecrement Width="7px">
                                    </SpinEditLargeDecrement>
                                </ImagesEditors>
                            </dxwgv:ASPxGridView>
                            <!-- POPUPCONTROL: pcDados -->
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="720px"
                                ID="pcDados">
                                <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>
                                <ContentStyle>
                                    <Paddings PaddingTop="5px" PaddingBottom="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table id="tbtbtb" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 176px">
                                                                    <dxe:ASPxLabel runat="server" Text="C&#243;digo Reservado:" ClientInstanceName="lblUnidadeDeMedida"
                                                                        Font-Strikeout="False" ID="ASPxLabel2">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top">
                                                                    <dxe:ASPxLabel runat="server" Text="Dado:" ClientInstanceName="lblDado"
                                                                        Font-Strikeout="False" ID="lblDado">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 176px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 95%">
                                                                        <tr>
                                                                            <td style="width: 149px">
                                                                                <dxe:ASPxTextBox runat="server" Width="97%" MaxLength="20" ClientInstanceName="txtCodigoReservado"
                                                                                    Font-Strikeout="False" ID="txtCodigoReservado">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    ImageUrl="~/imagens/ajuda.png" ToolTip="C&#243;digo utilizado para interface com outros sistemas da institui&#231;&#227;o">
                                                                                    <ClientSideEvents Click="function(s, e) {
	pcAjuda.Show();
}" />
                                                                                    <ClientSideEvents Click="function(s, e) {
	pcAjuda.Show();
}"></ClientSideEvents>
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td valign="top">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="99" ClientInstanceName="txtDado"
                                                                        Font-Strikeout="False" ID="txtDado">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 166px;">
                                                                        <dxe:ASPxLabel runat="server" Text="Unidade de Medida:" ClientInstanceName="lblUnidadeDeMedida"
                                                                            Font-Strikeout="False" ID="lblUnidadeDeMedida">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 10px; height: 16px;"></td>
                                                                    <td style="width: 166px;">
                                                                        <dxe:ASPxLabel runat="server" Text="Casas Decimais:" ClientInstanceName="lblCasasDecimais"
                                                                            Font-Strikeout="False" ID="lblCasasDecimais">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 10px;"></td>
                                                                    <td style="width: 323px;">
                                                                        <dxe:ASPxLabel runat="server" Text="Agrupamento do Dado:" ClientInstanceName="lblAgrupamentoDoDado"
                                                                            Width="135px" Font-Strikeout="False" ID="lblAgrupamentoDoDado">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbUnidadeDeMedida"
                                                                            ID="cmbUnidadeDeMedida">
                                                                            <DropDownButton>
                                                                                <Image Width="9px">
                                                                                </Image>
                                                                            </DropDownButton>
                                                                            <ButtonEditEllipsisImage Width="12px">
                                                                            </ButtonEditEllipsisImage>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbCasasDecimais"
                                                                            ID="cmbCasasDecimais">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="0 (Zero)" Value="0"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="1 (Uma)" Value="1"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="2 (Duas)" Value="2"></dxe:ListEditItem>
                                                                            </Items>
                                                                            <DropDownButton>
                                                                                <Image Width="9px">
                                                                                </Image>
                                                                            </DropDownButton>
                                                                            <ButtonEditEllipsisImage Width="12px">
                                                                            </ButtonEditEllipsisImage>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="width: 323px">
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbAgrupamentoDoDado"
                                                                            ID="cmbAgrupamentoDoDado">
                                                                            <DropDownButton>
                                                                                <Image Width="9px">
                                                                                </Image>
                                                                            </DropDownButton>
                                                                            <ButtonEditEllipsisImage Width="12px">
                                                                            </ButtonEditEllipsisImage>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td valign="middle" align="center">
                                                                        <img style="cursor: pointer" title="Mensagem para ajuda!" src="../../imagens/ajuda.png"
                                                                            alt="ajuda" />
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Gloss&#225;rio:" ClientInstanceName="lblGlossario"
                                                            ID="lblGlossario">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="heGlossario" Width="690px"
                                                            ID="heGlossario">
                                                            <Toolbars>
                                                                <dxhe:HtmlEditorToolbar>
                                                                    <Items>
                                                                        <dxhe:ToolbarCutButton>
                                                                        </dxhe:ToolbarCutButton>
                                                                        <dxhe:ToolbarCopyButton>
                                                                        </dxhe:ToolbarCopyButton>
                                                                        <dxhe:ToolbarPasteButton>
                                                                        </dxhe:ToolbarPasteButton>
                                                                        <dxhe:ToolbarPasteFromWordButton>
                                                                        </dxhe:ToolbarPasteFromWordButton>
                                                                        <dxhe:ToolbarUndoButton BeginGroup="True">
                                                                        </dxhe:ToolbarUndoButton>
                                                                        <dxhe:ToolbarRedoButton>
                                                                        </dxhe:ToolbarRedoButton>
                                                                        <dxhe:ToolbarRemoveFormatButton BeginGroup="True">
                                                                        </dxhe:ToolbarRemoveFormatButton>
                                                                        <dxhe:ToolbarSuperscriptButton BeginGroup="True">
                                                                        </dxhe:ToolbarSuperscriptButton>
                                                                        <dxhe:ToolbarSubscriptButton>
                                                                        </dxhe:ToolbarSubscriptButton>
                                                                        <dxhe:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                        </dxhe:ToolbarInsertOrderedListButton>
                                                                        <dxhe:ToolbarInsertUnorderedListButton>
                                                                        </dxhe:ToolbarInsertUnorderedListButton>
                                                                        <dxhe:ToolbarIndentButton BeginGroup="True">
                                                                        </dxhe:ToolbarIndentButton>
                                                                        <dxhe:ToolbarOutdentButton>
                                                                        </dxhe:ToolbarOutdentButton>
                                                                        <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                        </dxhe:ToolbarInsertLinkDialogButton>
                                                                        <dxhe:ToolbarUnlinkButton>
                                                                        </dxhe:ToolbarUnlinkButton>
                                                                        <dxhe:ToolbarInsertImageDialogButton>
                                                                        </dxhe:ToolbarInsertImageDialogButton>
                                                                        <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarCheckSpellingButton>
                                                                        <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                            <Items>
                                                                                <dxhe:ToolbarInsertTableDialogButton BeginGroup="True" ViewStyle="ImageAndText">
                                                                                </dxhe:ToolbarInsertTableDialogButton>
                                                                                <dxhe:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                                </dxhe:ToolbarTablePropertiesDialogButton>
                                                                                <dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                </dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                <dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                </dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                <dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                </dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                <dxhe:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                                </dxhe:ToolbarInsertTableRowAboveButton>
                                                                                <dxhe:ToolbarInsertTableRowBelowButton>
                                                                                </dxhe:ToolbarInsertTableRowBelowButton>
                                                                                <dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                </dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                <dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                </dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                <dxhe:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                                </dxhe:ToolbarSplitTableCellHorizontallyButton>
                                                                                <dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                </dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                <dxhe:ToolbarMergeTableCellRightButton>
                                                                                </dxhe:ToolbarMergeTableCellRightButton>
                                                                                <dxhe:ToolbarMergeTableCellDownButton>
                                                                                </dxhe:ToolbarMergeTableCellDownButton>
                                                                                <dxhe:ToolbarDeleteTableButton BeginGroup="True">
                                                                                </dxhe:ToolbarDeleteTableButton>
                                                                                <dxhe:ToolbarDeleteTableRowButton>
                                                                                </dxhe:ToolbarDeleteTableRowButton>
                                                                                <dxhe:ToolbarDeleteTableColumnButton>
                                                                                </dxhe:ToolbarDeleteTableColumnButton>
                                                                            </Items>
                                                                        </dxhe:ToolbarTableOperationsDropDownButton>
                                                                        <dxhe:ToolbarFullscreenButton>
                                                                        </dxhe:ToolbarFullscreenButton>
                                                                    </Items>
                                                                </dxhe:HtmlEditorToolbar>
                                                                <dxhe:HtmlEditorToolbar>
                                                                    <Items>
                                                                        <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                            <Items>
                                                                                <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                            </Items>
                                                                        </dxhe:ToolbarParagraphFormattingEdit>
                                                                        <dxhe:ToolbarFontNameEdit>
                                                                            <Items>
                                                                                <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                                            </Items>
                                                                        </dxhe:ToolbarFontNameEdit>
                                                                        <dxhe:ToolbarFontSizeEdit>
                                                                            <Items>
                                                                                <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                                                <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                                            </Items>
                                                                        </dxhe:ToolbarFontSizeEdit>
                                                                        <dxhe:ToolbarBoldButton BeginGroup="True">
                                                                        </dxhe:ToolbarBoldButton>
                                                                        <dxhe:ToolbarItalicButton>
                                                                        </dxhe:ToolbarItalicButton>
                                                                        <dxhe:ToolbarUnderlineButton>
                                                                        </dxhe:ToolbarUnderlineButton>
                                                                        <dxhe:ToolbarStrikethroughButton>
                                                                        </dxhe:ToolbarStrikethroughButton>
                                                                        <dxhe:ToolbarJustifyLeftButton BeginGroup="True">
                                                                        </dxhe:ToolbarJustifyLeftButton>
                                                                        <dxhe:ToolbarJustifyCenterButton>
                                                                        </dxhe:ToolbarJustifyCenterButton>
                                                                        <dxhe:ToolbarJustifyRightButton>
                                                                        </dxhe:ToolbarJustifyRightButton>
                                                                        <dxhe:ToolbarJustifyFullButton>
                                                                        </dxhe:ToolbarJustifyFullButton>
                                                                        <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                                        </dxhe:ToolbarBackColorButton>
                                                                        <dxhe:ToolbarFontColorButton>
                                                                        </dxhe:ToolbarFontColorButton>
                                                                    </Items>
                                                                </dxhe:HtmlEditorToolbar>
                                                            </Toolbars>
                                                            <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                            <SettingsDialogs>
                                                                <InsertImageDialog>
                                                                    <SettingsImageSelector>
                                                                        <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                                    </SettingsImageSelector>
                                                                </InsertImageDialog>
                                                            </SettingsDialogs>
                                                        </dxhe:ASPxHtmlEditor>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px"
                                                                            ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    if (window.onClick_btnSalvar)
	                {
	                    onClick_btnSalvar();
	                }
                }"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"
                                                                            ID="btnFechar">
                                                                            <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    if (window.onClick_btnCancelar)
	                {
                       onClick_btnCancelar();
	                   if(gvDados.GetVisibleRowsOnPage() == 0)
		                {
			                desabilitaBarraNavegacao(true);
		                }
		                else
		                {
			                desabilitaBarraNavegacao(false);
		                }
	                }
                	
                }"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
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
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="" align="center"></td>
                                                    <td style="width: 70px" align="center" rowspan="3">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                            ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                            ID="lblAcaoGravacao">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxpc:ASPxPopupControl ID="pcAjuda" runat="server" AllowDragging="True" ClientInstanceName="pcAjuda"
                                CloseAction="CloseButton" Font-Size="10pt" HeaderText="Ajuda"
                                Modal="True" PopupElementID="imgAjuda" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                Width="293px">
                                <HeaderStyle Font-Bold="True" />
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        Código utilizado para interface com outros sistemas da instituição.
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();

	if(gvDados.GetVisibleRowsOnPage() == 0)
	{
		desabilitaBarraNavegacao(true);
	}
	else
	{
		desabilitaBarraNavegacao(false);
	}

	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Dados inclu&#237;do com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
