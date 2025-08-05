<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjetivoEstrategico_Estrategias.aspx.cs" Inherits="_Estrategias_objetivoEstrategico_ObjetivoEstrategico_Estrategias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <%--            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server"  CssClass="campo-label" Text="Mapa Estratégico:"></asp:Label></td>
                            <td>
                            </td>
                            <td style="width: 209px">
                                <asp:Label ID="lblPerspectiva" CssClass="campo-label" runat="server" 
                                    Text="Perspectiva:"></asp:Label></td>
                            <td>
                            </td>
                            <td style="width: 220px">
                                <asp:Label ID="Label6" CssClass="campo-label" runat="server"  Text="Tema:"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa" 
                                    ReadOnly="True" Width="100%">
                                    <ReadOnlyStyle BackColor="#E0E0E0">
                                    </ReadOnlyStyle>
                                </dxe:ASPxTextBox>
                            </td>
                            <td>
                            </td>
                            <td style="width: 220px">
                                <dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                                     ReadOnly="True" Width="100%">
                                    <ReadOnlyStyle BackColor="#E0E0E0">
                                    </ReadOnlyStyle>
                                </dxe:ASPxTextBox>
                            </td>
                            <td>
                            </td>
                            <td style="width: 220px">
                                <dxe:ASPxTextBox ID="txtTema" runat="server" ClientInstanceName="txtTema" 
                                    ReadOnly="True" Width="100%">
                                    <ReadOnlyStyle BackColor="#E0E0E0">
                                    </ReadOnlyStyle>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="display: none;">
                                <table style="width: 100%" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblObjetivoEstrategico" runat="server" CssClass="campo-label" Text="<%$ Resources:traducao, objetivo_estrat_gico_ %>"></asp:Label></td>
                                            <td
                                                style="width: 10px"></td>
                                            <td style="width: 280px">
                                                <asp:Label ID="Label3" runat="server" CssClass="campo-label" Text="<%$ Resources:traducao, respons_vel_ %>"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtObjetivoEstrategico">
                                                    <ReadOnlyStyle BackColor="#E0E0E0">
                                                    </ReadOnlyStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td
                                                style="width: 280px">
                                                <dxe:ASPxTextBox ID="txtResponsavel" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtResponsavel">
                                                    <ReadOnlyStyle BackColor="#E0E0E0">
                                                    </ReadOnlyStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                    Width="100%" OnCallback="pnCallback_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoLinhaAtuacao" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                                                <ClientSideEvents FocusedRowChanged="function(s, e) {
    OnGridFocusedRowChanged(s);
}"
                                                    CustomButtonClick="function(s, e) {
	clickButtonCustom(s,e);
}"></ClientSideEvents>
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="acao" Width="110px" Caption="A&#231;&#227;o" VisibleIndex="0">
                                                        <CustomButtons>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Visibility="Invisible" Text="Incluir">
                                                                <Image Url="~/imagens/botoes/incluirReg02.png"></Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                                <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                                <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                                                <Image Url="~/imagens/botoes/pFormulario.png"></Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>

                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
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
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoLinhaAtuacao" Name="DescricaoLinhaAtuacao" Caption="Descri&#231;&#227;o" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="TextoLinhaAtuacao" Name="TextoLinhaAtuacao" Caption="Texto" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
                                                </Columns>

                                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                                <Settings ShowHeaderFilterBlankItems="False" VerticalScrollBarMode="Visible"></Settings>

                                            </dxwgv:ASPxGridView>
                                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupVerticalOffset="-100" ShowCloseButton="False" Width="700px" ID="pcDados">
                                                <ContentStyle>
                                                    <Paddings PaddingBottom="1px"></Paddings>
                                                </ContentStyle>

                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                <ContentCollection>
                                                    <dxpc:PopupControlContentControl runat="server">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o:" ClientInstanceName="lblDescricao" ID="lblDescricao"></dxe:ASPxLabel>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%"
                                                                            ClientInstanceName="txtDescricao"
                                                                            ID="txtDescricao" MaxLength="250">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                        </dxe:ASPxTextBox>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Detalhamento:" ClientInstanceName="lblDetalhamento" ID="lblDetalhamento"></dxe:ASPxLabel>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="htmlTexto" Width="690px" Height="270px" ID="htmlTexto">
                                                                            <Toolbars>
                                                                                <dxhe:HtmlEditorToolbar>
                                                                                    <Items>
                                                                                        <dxhe:ToolbarCutButton></dxhe:ToolbarCutButton>
                                                                                        <dxhe:ToolbarCopyButton></dxhe:ToolbarCopyButton>
                                                                                        <dxhe:ToolbarPasteButton></dxhe:ToolbarPasteButton>
                                                                                        <dxhe:ToolbarPasteFromWordButton></dxhe:ToolbarPasteFromWordButton>
                                                                                        <dxhe:ToolbarUndoButton BeginGroup="True"></dxhe:ToolbarUndoButton>
                                                                                        <dxhe:ToolbarRedoButton></dxhe:ToolbarRedoButton>
                                                                                        <dxhe:ToolbarRemoveFormatButton BeginGroup="True"></dxhe:ToolbarRemoveFormatButton>
                                                                                        <dxhe:ToolbarSuperscriptButton BeginGroup="True"></dxhe:ToolbarSuperscriptButton>
                                                                                        <dxhe:ToolbarSubscriptButton></dxhe:ToolbarSubscriptButton>
                                                                                        <dxhe:ToolbarInsertOrderedListButton BeginGroup="True"></dxhe:ToolbarInsertOrderedListButton>
                                                                                        <dxhe:ToolbarInsertUnorderedListButton></dxhe:ToolbarInsertUnorderedListButton>
                                                                                        <dxhe:ToolbarIndentButton BeginGroup="True"></dxhe:ToolbarIndentButton>
                                                                                        <dxhe:ToolbarOutdentButton></dxhe:ToolbarOutdentButton>
                                                                                        <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True"></dxhe:ToolbarInsertLinkDialogButton>
                                                                                        <dxhe:ToolbarUnlinkButton></dxhe:ToolbarUnlinkButton>
                                                                                        <dxhe:ToolbarInsertImageDialogButton></dxhe:ToolbarInsertImageDialogButton>
                                                                                        <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False"></dxhe:ToolbarCheckSpellingButton>
                                                                                        <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                                            <Items>
                                                                                                <dxhe:ToolbarInsertTableDialogButton ViewStyle="ImageAndText" BeginGroup="True"></dxhe:ToolbarInsertTableDialogButton>
                                                                                                <dxhe:ToolbarTablePropertiesDialogButton BeginGroup="True"></dxhe:ToolbarTablePropertiesDialogButton>
                                                                                                <dxhe:ToolbarTableRowPropertiesDialogButton></dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                                <dxhe:ToolbarTableColumnPropertiesDialogButton></dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                                <dxhe:ToolbarTableCellPropertiesDialogButton></dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                                <dxhe:ToolbarInsertTableRowAboveButton BeginGroup="True"></dxhe:ToolbarInsertTableRowAboveButton>
                                                                                                <dxhe:ToolbarInsertTableRowBelowButton></dxhe:ToolbarInsertTableRowBelowButton>
                                                                                                <dxhe:ToolbarInsertTableColumnToLeftButton></dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                                <dxhe:ToolbarInsertTableColumnToRightButton></dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                                <dxhe:ToolbarSplitTableCellHorizontallyButton BeginGroup="True"></dxhe:ToolbarSplitTableCellHorizontallyButton>
                                                                                                <dxhe:ToolbarSplitTableCellVerticallyButton></dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                                <dxhe:ToolbarMergeTableCellRightButton></dxhe:ToolbarMergeTableCellRightButton>
                                                                                                <dxhe:ToolbarMergeTableCellDownButton></dxhe:ToolbarMergeTableCellDownButton>
                                                                                                <dxhe:ToolbarDeleteTableButton BeginGroup="True"></dxhe:ToolbarDeleteTableButton>
                                                                                                <dxhe:ToolbarDeleteTableRowButton></dxhe:ToolbarDeleteTableRowButton>
                                                                                                <dxhe:ToolbarDeleteTableColumnButton></dxhe:ToolbarDeleteTableColumnButton>
                                                                                                <dxhe:ToolbarFullscreenButton></dxhe:ToolbarFullscreenButton>
                                                                                            </Items>
                                                                                        </dxhe:ToolbarTableOperationsDropDownButton>
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
                                                                                        <dxhe:ToolbarBoldButton BeginGroup="True"></dxhe:ToolbarBoldButton>
                                                                                        <dxhe:ToolbarItalicButton></dxhe:ToolbarItalicButton>
                                                                                        <dxhe:ToolbarUnderlineButton></dxhe:ToolbarUnderlineButton>
                                                                                        <dxhe:ToolbarStrikethroughButton></dxhe:ToolbarStrikethroughButton>
                                                                                        <dxhe:ToolbarJustifyLeftButton BeginGroup="True"></dxhe:ToolbarJustifyLeftButton>
                                                                                        <dxhe:ToolbarJustifyCenterButton></dxhe:ToolbarJustifyCenterButton>
                                                                                        <dxhe:ToolbarJustifyRightButton></dxhe:ToolbarJustifyRightButton>
                                                                                        <dxhe:ToolbarJustifyFullButton></dxhe:ToolbarJustifyFullButton>
                                                                                        <dxhe:ToolbarBackColorButton BeginGroup="True"></dxhe:ToolbarBackColorButton>
                                                                                        <dxhe:ToolbarFontColorButton></dxhe:ToolbarFontColorButton>
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
                                                                                <InsertLinkDialog>
                                                                                    <SettingsDocumentSelector>
                                                                                        <CommonSettings AllowedFileExtensions=".rtf, .pdf, .doc, .docx, .odt, .txt, .xls, .xlsx, .ods, .ppt, .pptx, .odp" />
                                                                                    </SettingsDocumentSelector>
                                                                                </InsertLinkDialog>
                                                                            </SettingsDialogs>
                                                                        </dxhe:ASPxHtmlEditor>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 5px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 100px">
                                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100%" ID="btnSalvar" AutoPostBack="False">
                                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                                        </dxe:ASPxButton>

                                                                                    </td>
                                                                                    <td style="width: 10px"></td>
                                                                                    <td style="width: 100px">
                                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%" ID="btnFechar">
                                                                                            <ClientSideEvents Click="function(s, e) {
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
                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                                GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
                                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                                <ContentCollection>
                                                    <dxpc:PopupControlContentControl runat="server">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="" align="center"></td>
                                                                    <td style="width: 70px" align="center" rowspan="3">
                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>

                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxpc:PopupControlContentControl>
                                                </ContentCollection>
                                            </dxpc:ASPxPopupControl>
                                        </dxp:PanelContent>
                                    </PanelCollection>

                                    <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();	

	if('Incluir' == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.ObjetivoEstrategico_Estrategias_estrat_gia_inclu_da_com_sucesso_);
    else if('Editar' == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.ObjetivoEstrategico_Estrategias_dados_gravados_com_sucesso_);
    else if('Excluir' == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.ObjetivoEstrategico_Estrategias_estrat_gia_exclu_da_com_sucesso_);
}"></ClientSideEvents>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
