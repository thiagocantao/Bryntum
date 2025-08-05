<%@ Page Language="C#" AutoEventWireup="true" CodeFile="atualizaComentario.aspx.cs" Inherits="_Projetos_Agil_atualizaComentario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 100%;
            float: right;
        }

        .iniciaisMaiusculas {
            text-transform: capitalize !important
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxhe:ASPxHtmlEditor ID="htmlEditaComentario" runat="server" Theme="MaterialCompact" Width="100%" ClientInstanceName="htmlEditaComentario">
                <ClientSideEvents Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 45;
       s.SetHeight(sHeight);
}" />
                <Toolbars>
                    <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
                        <Items>
                            <dxhe:ToolbarCutButton AdaptivePriority="2">
                            </dxhe:ToolbarCutButton>
                            <dxhe:ToolbarCopyButton AdaptivePriority="2">
                            </dxhe:ToolbarCopyButton>
                            <dxhe:ToolbarPasteButton AdaptivePriority="2">
                            </dxhe:ToolbarPasteButton>
                            <dxhe:ToolbarPasteFromWordButton AdaptivePriority="2" Visible="False">
                            </dxhe:ToolbarPasteFromWordButton>
                            <dxhe:ToolbarUndoButton AdaptivePriority="1" BeginGroup="True">
                            </dxhe:ToolbarUndoButton>
                            <dxhe:ToolbarRedoButton AdaptivePriority="1">
                            </dxhe:ToolbarRedoButton>
                            <dxhe:ToolbarRemoveFormatButton AdaptivePriority="2" BeginGroup="True" Visible="False">
                            </dxhe:ToolbarRemoveFormatButton>
                            <dxhe:ToolbarSuperscriptButton AdaptivePriority="1" BeginGroup="True">
                            </dxhe:ToolbarSuperscriptButton>
                            <dxhe:ToolbarSubscriptButton AdaptivePriority="1">
                            </dxhe:ToolbarSubscriptButton>
                            <dxhe:ToolbarInsertOrderedListButton AdaptivePriority="1" BeginGroup="True">
                            </dxhe:ToolbarInsertOrderedListButton>
                            <dxhe:ToolbarInsertUnorderedListButton AdaptivePriority="1">
                            </dxhe:ToolbarInsertUnorderedListButton>
                            <dxhe:ToolbarIndentButton AdaptivePriority="2" BeginGroup="True">
                            </dxhe:ToolbarIndentButton>
                            <dxhe:ToolbarOutdentButton AdaptivePriority="2">
                            </dxhe:ToolbarOutdentButton>
                            <dxhe:ToolbarInsertLinkDialogButton AdaptivePriority="1" BeginGroup="True" Visible="False">
                            </dxhe:ToolbarInsertLinkDialogButton>
                            <dxhe:ToolbarUnlinkButton AdaptivePriority="1" Visible="False">
                            </dxhe:ToolbarUnlinkButton>
                            <dxhe:ToolbarInsertImageDialogButton AdaptivePriority="1" Visible="False">
                            </dxhe:ToolbarInsertImageDialogButton>
                            <dxhe:ToolbarTableOperationsDropDownButton AdaptivePriority="2" BeginGroup="True" Visible="False">
                                <Items>
                                    <dxhe:ToolbarInsertTableDialogButton BeginGroup="True" Text="Insert Table..." ToolTip="Insert Table...">
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
                            <dxhe:ToolbarFindAndReplaceDialogButton AdaptivePriority="2" BeginGroup="True">
                            </dxhe:ToolbarFindAndReplaceDialogButton>
                            <dxhe:ToolbarFullscreenButton AdaptivePriority="1" BeginGroup="True">
                            </dxhe:ToolbarFullscreenButton>
                        </Items>
                    </dxhe:HtmlEditorToolbar>
                    <dxhe:HtmlEditorToolbar Name="StandardToolbar2">
                        <Items>
                            <dxhe:ToolbarParagraphFormattingEdit AdaptivePriority="2" Width="120px">
                                <Items>
                                    <dxhe:ToolbarListEditItem Text="Normal" Value="p" />
                                    <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                    <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                    <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                    <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                    <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                    <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                    <dxhe:ToolbarListEditItem Text="Address" Value="address" />
                                    <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                </Items>
                            </dxhe:ToolbarParagraphFormattingEdit>
                            <dxhe:ToolbarFontNameEdit AdaptivePriority="2">
                                <Items>
                                    <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                    <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                    <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                    <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                                    <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                    <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
                                    <dxhe:ToolbarListEditItem Text="Segoe UI" Value="Segoe UI" />
                                </Items>
                            </dxhe:ToolbarFontNameEdit>
                            <dxhe:ToolbarFontSizeEdit AdaptivePriority="2">
                                <Items>
                                    <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                    <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                    <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                    <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                    <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                    <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                    <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                </Items>
                            </dxhe:ToolbarFontSizeEdit>
                            <dxhe:ToolbarBoldButton AdaptivePriority="1" BeginGroup="True">
                            </dxhe:ToolbarBoldButton>
                            <dxhe:ToolbarItalicButton AdaptivePriority="1">
                            </dxhe:ToolbarItalicButton>
                            <dxhe:ToolbarUnderlineButton AdaptivePriority="1">
                            </dxhe:ToolbarUnderlineButton>
                            <dxhe:ToolbarStrikethroughButton AdaptivePriority="1">
                            </dxhe:ToolbarStrikethroughButton>
                            <dxhe:ToolbarJustifyLeftButton AdaptivePriority="1" BeginGroup="True">
                            </dxhe:ToolbarJustifyLeftButton>
                            <dxhe:ToolbarJustifyCenterButton AdaptivePriority="1">
                            </dxhe:ToolbarJustifyCenterButton>
                            <dxhe:ToolbarJustifyRightButton AdaptivePriority="1">
                            </dxhe:ToolbarJustifyRightButton>
                            <dxhe:ToolbarBackColorButton AdaptivePriority="1" BeginGroup="True">
                            </dxhe:ToolbarBackColorButton>
                            <dxhe:ToolbarFontColorButton AdaptivePriority="1">
                            </dxhe:ToolbarFontColorButton>
                        </Items>
                    </dxhe:HtmlEditorToolbar>
                </Toolbars>
                <Settings AllowHtmlView="False" AllowPreview="False">
                </Settings>
                <SettingsHtmlEditing>
                    <PasteFiltering Attributes="class"></PasteFiltering>
                </SettingsHtmlEditing>
                <SettingsValidation ErrorText="Nenhum comentário foi informado" ValidationGroup="grp_comment">
                    <RequiredField ErrorText="Nenhum comentário foi informado" IsRequired="True" />
                </SettingsValidation>
            </dxhe:ASPxHtmlEditor>
        </div>
        <table class="auto-style3">
            <tr>
                <td>
                    <dxcp:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
       if(s.cpErro != '')
      {
        window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
      }
      else if (s.cpSucesso != '')
      {
                window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3000);
      }
}" />
                    </dxcp:ASPxCallback>
                </td>
                <td style="width: 100px; padding-right: 5px">
                    <dxcp:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100%" AutoPostBack="False" CssClass="iniciaisMaiusculas">
                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer  = false;
               callbackTela.PerformCallback();
}" />
                    </dxcp:ASPxButton>
                </td>
                <td style="width: 100px">
                    <dxcp:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%" AutoPostBack="False" CssClass="iniciaisMaiusculas">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal2();
}" />
                    </dxcp:ASPxButton>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
