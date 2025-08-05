<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnaliseAcompanhamentoProjeto.aspx.cs"
    Inherits="_Projetos_DadosProjeto_AnaliseAcompanhamentoProjeto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .Resize textarea {
            resize: both;
        }
        /*
        @media (max-height: 768px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 300px;
            }
        }
        @media (min-height: 769px) and (max-height: 800px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 400px;
            }
        }
        @media (min-height: 801px) and (max-height: 960px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 550px;
            }
        }
        @media (min-height: 961px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 650px;
            }
        }
        */
    </style>
    <script type="text/javascript">
        function AddNewRow() {
            hfGeral.Set('SomenteLeitura', 'N');
            gvDados.AddNewRow();
        }

        function OnStartEdit() {
            var somenteLeitura = hfGeral.Get('SomenteLeitura');
            var tdBtnSalvar = document.getElementById('tdBtnSalvar');
            if (somenteLeitura == 'S') {
                htmlExecucaoFisicaProjeto.SetEnabled(false);
                htmlExecucaoFinanceiraProjeto.SetEnabled(false);
                btnSalvar.SetVisible(false);
                //tdBtnSalvar.style.diplay = 'none';
            }
            else if (somenteLeitura == 'N') {
                htmlExecucaoFisicaProjeto.SetEnabled(true);
                htmlExecucaoFinanceiraProjeto.SetEnabled(true);
                btnSalvar.SetVisible(true);
                //tdBtnSalvar.style.diplay = '';
            }
            hfGeral.Set('SomenteLeitura', null);
        }

        function OnCustomButtonClick(s, e) {
            gvDados.SetFocusedRowIndex(e.visibleIndex);
            if (e.buttonID == "btnNovo") {
                hfGeral.Set('SomenteLeitura', 'N');
                s.AddNewRow();
            }
            else if (e.buttonID == "btnEditar") {
                hfGeral.Set('SomenteLeitura', 'N');
                s.StartEditRow(e.visibleIndex);
            }
            else if (e.buttonID == "btnExcluir") {
                window.top.mostraMensagem('Deseja excluir o registro?', 'confirmacao', true, true, excluiAnalise);
            }
            else if (e.buttonID == "btnDetalhesCustom") {
                hfGeral.Set('SomenteLeitura', 'S');
                s.StartEditRow(e.visibleIndex);
            }
        }

        function excluiAnalise() {
            gvDados.DeleteRow(gvDados.GetFocusedRowIndex());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                            KeyFieldName="CodigoAnalisePerformance"
                            OnCustomButtonInitialize="gvDados_CustomButtonInitialize" Width="100%"
                            DataSourceID="dataSource" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                            <ClientSideEvents CustomButtonClick="function(s, e) 
{
	OnCustomButtonClick(s, e);
}
"
                                Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);
    height = height - 130;
 	//s.SetHeight(height);
}" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                    Width="100px">
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
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td align="center">
                                                    <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                        ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
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
                                                                    <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
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
                                                            <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
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
                                <dxwgv:GridViewDataDateColumn Caption="Data" ExportWidth="120" FieldName="DataAnalisePerformance"
                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="115px">
                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                    </PropertiesDateEdit>
                                    <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Responsável" ExportWidth="240" FieldName="colResponsavel"
                                    ShowInCustomizationForm="True" UnboundExpression="IsNull(UsuarioAlteracao, UsuarioInclusao)"
                                    UnboundType="String" VisibleIndex="2" Width="200px">
                                    <Settings AutoFilterCondition="Contains" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="CodigoAnalisePerformance" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Visible="False">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataMemoColumn Caption="Execução Física do Projeto" FieldName="PrincipaisResultados"
                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                    <PropertiesMemoEdit Rows="16">
                                        <Style CssClass="Resize">
                    </Style>
                                    </PropertiesMemoEdit>
                                    <EditFormSettings Caption="Execução Física do Projeto" CaptionLocation="Top" Visible="True" />
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataMemoColumn>
                                <dxwgv:GridViewDataMemoColumn Caption="Execução Financeira do Projeto" FieldName="PontosAtencao"
                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                    <PropertiesMemoEdit Rows="16">
                                        <Style CssClass="Resize">
                    </Style>
                                    </PropertiesMemoEdit>
                                    <EditFormSettings Caption="Execução Financeira do Projeto" CaptionLocation="Top" Visible="True" />
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataMemoColumn>
                            </Columns>
                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                            </SettingsPager>
                            <SettingsEditing Mode="PopupEditForm" />
                            <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible" />
                            <SettingsText PopupEditFormCaption=" " />
                            <SettingsPopup>
                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                    Width="800px" />
                            </SettingsPopup>
                            <Templates>
                                <EditForm>
                                    <div runat="server" id="DivDoEditFormTemplate" class="rolagem-tab">
                                        <table style="height: 100%; width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="padding-top: 5px; padding-bottom: 5px">
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                            Text="Execução Física do Projeto: ">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="padding-top: 5px; padding-bottom: 5px;">
                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                            Text="Execução Financeira do Projeto: ">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-bottom: 10px; width: 50%;">
                                                        <dxhe:ASPxHtmlEditor ID="htmlExecucaoFisicaProjeto" runat="server" Theme="MaterialCompact"
                                                            ClientInstanceName="htmlExecucaoFisicaProjeto" Height="<%#alturaMemoEdit%>" Width="100%" Html='<%# Bind("PrincipaisResultados") %>'>
                                                            <Toolbars>
                                                                <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
                                                                    <Items>
                                                                        <dxhe:ToolbarCutButton Visible="False">
                                                                        </dxhe:ToolbarCutButton>
                                                                        <dxhe:ToolbarCopyButton Visible="False">
                                                                        </dxhe:ToolbarCopyButton>
                                                                        <dxhe:ToolbarPasteButton Visible="False">
                                                                        </dxhe:ToolbarPasteButton>
                                                                        <dxhe:ToolbarPasteFromWordButton Visible="False">
                                                                        </dxhe:ToolbarPasteFromWordButton>
                                                                        <dxhe:ToolbarUndoButton BeginGroup="True">
                                                                        </dxhe:ToolbarUndoButton>
                                                                        <dxhe:ToolbarRedoButton>
                                                                        </dxhe:ToolbarRedoButton>
                                                                        <dxhe:ToolbarRemoveFormatButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarRemoveFormatButton>
                                                                        <dxhe:ToolbarSuperscriptButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarSuperscriptButton>
                                                                        <dxhe:ToolbarSubscriptButton Visible="False">
                                                                        </dxhe:ToolbarSubscriptButton>
                                                                        <dxhe:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                        </dxhe:ToolbarInsertOrderedListButton>
                                                                        <dxhe:ToolbarInsertUnorderedListButton>
                                                                        </dxhe:ToolbarInsertUnorderedListButton>
                                                                        <dxhe:ToolbarIndentButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarIndentButton>
                                                                        <dxhe:ToolbarOutdentButton Visible="False">
                                                                        </dxhe:ToolbarOutdentButton>
                                                                        <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarInsertLinkDialogButton>
                                                                        <dxhe:ToolbarUnlinkButton Visible="False">
                                                                        </dxhe:ToolbarUnlinkButton>
                                                                        <dxhe:ToolbarInsertImageDialogButton Visible="False">
                                                                        </dxhe:ToolbarInsertImageDialogButton>
                                                                        <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True" Visible="False">
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
                                                                        <dxhe:ToolbarFullscreenButton BeginGroup="True">
                                                                        </dxhe:ToolbarFullscreenButton>
                                                                    </Items>
                                                                </dxhe:HtmlEditorToolbar>
                                                                <dxhe:HtmlEditorToolbar Name="StandardToolbar2">
                                                                    <Items>
                                                                        <dxhe:ToolbarParagraphFormattingEdit Visible="False" Width="120px">
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
                                                                        <dxhe:ToolbarFontNameEdit Visible="False">
                                                                            <Items>
                                                                                <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                                <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                                <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                                <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                                <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                                <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                                <dxhe:ToolbarListEditItem Text="Segoe UI" Value="Segoe UI" />
                                                                                <dxhe:ToolbarListEditItem Text="Calibri" Value="Calibri" />
                                                                            </Items>
                                                                        </dxhe:ToolbarFontNameEdit>
                                                                        <dxhe:ToolbarFontSizeEdit>
                                                                            <Items>
                                                                                <dxhe:ToolbarListEditItem Text="2 (11pt)" Value="2" />
                                                                                <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                            </Items>
                                                                        </dxhe:ToolbarFontSizeEdit>
                                                                        <dxhe:ToolbarBoldButton BeginGroup="True">
                                                                        </dxhe:ToolbarBoldButton>
                                                                        <dxhe:ToolbarItalicButton>
                                                                        </dxhe:ToolbarItalicButton>
                                                                        <dxhe:ToolbarUnderlineButton>
                                                                        </dxhe:ToolbarUnderlineButton>
                                                                        <dxhe:ToolbarStrikethroughButton Visible="False">
                                                                        </dxhe:ToolbarStrikethroughButton>
                                                                        <dxhe:ToolbarJustifyLeftButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarJustifyLeftButton>
                                                                        <dxhe:ToolbarJustifyCenterButton Visible="False">
                                                                        </dxhe:ToolbarJustifyCenterButton>
                                                                        <dxhe:ToolbarJustifyRightButton Visible="False">
                                                                        </dxhe:ToolbarJustifyRightButton>
                                                                        <dxhe:ToolbarBackColorButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarBackColorButton>
                                                                        <dxhe:ToolbarFontColorButton Visible="False">
                                                                        </dxhe:ToolbarFontColorButton>
                                                                    </Items>
                                                                </dxhe:HtmlEditorToolbar>
                                                            </Toolbars>
                                                            <Settings AllowHtmlView="False" AllowPreview="False">
                                                            </Settings>
                                                            <SettingsLoadingPanel Text="Carregando" />
                                                            <SettingsValidation ErrorText="O conteúdo HTML é inválido">
                                                            </SettingsValidation>
                                                        </dxhe:ASPxHtmlEditor>
                                                    </td>
                                                    <td style="padding-bottom: 10px; width: 50%;">
                                                        <dxhe:ASPxHtmlEditor ID="htmlExecucaoFinanceiraProjeto" Theme="MaterialCompact" runat="server" Html='<%# Bind("PontosAtencao") %>'
                                                            ClientInstanceName="htmlExecucaoFinanceiraProjeto" Height="<%#alturaMemoEdit%>" Width="100%">
                                                            <Toolbars>
                                                                <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
                                                                    <Items>
                                                                        <dxhe:ToolbarCutButton Visible="False">
                                                                        </dxhe:ToolbarCutButton>
                                                                        <dxhe:ToolbarCopyButton Visible="False">
                                                                        </dxhe:ToolbarCopyButton>
                                                                        <dxhe:ToolbarPasteButton Visible="False">
                                                                        </dxhe:ToolbarPasteButton>
                                                                        <dxhe:ToolbarPasteFromWordButton Visible="False">
                                                                        </dxhe:ToolbarPasteFromWordButton>
                                                                        <dxhe:ToolbarUndoButton BeginGroup="True">
                                                                        </dxhe:ToolbarUndoButton>
                                                                        <dxhe:ToolbarRedoButton>
                                                                        </dxhe:ToolbarRedoButton>
                                                                        <dxhe:ToolbarRemoveFormatButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarRemoveFormatButton>
                                                                        <dxhe:ToolbarSuperscriptButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarSuperscriptButton>
                                                                        <dxhe:ToolbarSubscriptButton Visible="False">
                                                                        </dxhe:ToolbarSubscriptButton>
                                                                        <dxhe:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                        </dxhe:ToolbarInsertOrderedListButton>
                                                                        <dxhe:ToolbarInsertUnorderedListButton>
                                                                        </dxhe:ToolbarInsertUnorderedListButton>
                                                                        <dxhe:ToolbarIndentButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarIndentButton>
                                                                        <dxhe:ToolbarOutdentButton Visible="False">
                                                                        </dxhe:ToolbarOutdentButton>
                                                                        <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarInsertLinkDialogButton>
                                                                        <dxhe:ToolbarUnlinkButton Visible="False">
                                                                        </dxhe:ToolbarUnlinkButton>
                                                                        <dxhe:ToolbarInsertImageDialogButton Visible="False">
                                                                        </dxhe:ToolbarInsertImageDialogButton>
                                                                        <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True" Visible="False">
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
                                                                        <dxhe:ToolbarFullscreenButton BeginGroup="True">
                                                                        </dxhe:ToolbarFullscreenButton>
                                                                    </Items>
                                                                </dxhe:HtmlEditorToolbar>
                                                                <dxhe:HtmlEditorToolbar Name="StandardToolbar2">
                                                                    <Items>
                                                                        <dxhe:ToolbarParagraphFormattingEdit Visible="False" Width="120px">
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
                                                                        <dxhe:ToolbarFontNameEdit Visible="False">
                                                                            <Items>
                                                                                <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                                <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                                <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                                <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                                <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                                <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                                <dxhe:ToolbarListEditItem Text="Segoe UI" Value="Segoe UI" />
                                                                                <dxhe:ToolbarListEditItem Selected="True" Text="Calibri" Value="Calibri" />
                                                                            </Items>
                                                                        </dxhe:ToolbarFontNameEdit>
                                                                        <dxhe:ToolbarFontSizeEdit>
                                                                            <Items>
                                                                                <dxhe:ToolbarListEditItem Text="2 (11pt)" Value="2" />
                                                                                <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                            </Items>
                                                                        </dxhe:ToolbarFontSizeEdit>
                                                                        <dxhe:ToolbarBoldButton BeginGroup="True">
                                                                        </dxhe:ToolbarBoldButton>
                                                                        <dxhe:ToolbarItalicButton>
                                                                        </dxhe:ToolbarItalicButton>
                                                                        <dxhe:ToolbarUnderlineButton>
                                                                        </dxhe:ToolbarUnderlineButton>
                                                                        <dxhe:ToolbarStrikethroughButton Visible="False">
                                                                        </dxhe:ToolbarStrikethroughButton>
                                                                        <dxhe:ToolbarJustifyLeftButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarJustifyLeftButton>
                                                                        <dxhe:ToolbarJustifyCenterButton Visible="False">
                                                                        </dxhe:ToolbarJustifyCenterButton>
                                                                        <dxhe:ToolbarJustifyRightButton Visible="False">
                                                                        </dxhe:ToolbarJustifyRightButton>
                                                                        <dxhe:ToolbarBackColorButton BeginGroup="True" Visible="False">
                                                                        </dxhe:ToolbarBackColorButton>
                                                                        <dxhe:ToolbarFontColorButton Visible="False">
                                                                        </dxhe:ToolbarFontColorButton>
                                                                    </Items>
                                                                </dxhe:HtmlEditorToolbar>
                                                            </Toolbars>
                                                            <Settings AllowHtmlView="False" AllowPreview="False">
                                                            </Settings>
                                                            <SettingsLoadingPanel Text="Carregando" />
                                                            <SettingsValidation ErrorText="O conteúdo HTML é inválido">
                                                            </SettingsValidation>
                                                            <ClientSideEvents Init="function(s, e) {
	OnStartEdit();
}" />
                                                        </dxhe:ASPxHtmlEditor>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <table align="right" class="formulario-botoes" id="tblBotoes">
                                        <tr>
                                            <td class="formulario-botao" id="tdBtnSalvar">
                                                <dxe:ASPxButton ID="btnSalvar" CssFilePath="~/AppThemes/MaterialCompact/{0}/styles.css" CssPostfix="MaterialCompact" runat="server"
                                                    Text="Salvar" Width="100px" ClientInstanceName="btnSalvar"
                                                    AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	//aspxGVScheduleCommand('pnCallback_gvDados',['UpdateEdit']);
	gvDados.UpdateEdit();
}" />
                                                </dxe:ASPxButton>
                                                <%--<dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" runat="server"
                                                            ReplacementType="EditFormUpdateButton" />--%>
                                            </td>
                                            <td class="formulario-botao" id="tdBtnFechar">
                                                <dxe:ASPxButton ID="btnCancelar" CssFilePath="~/AppThemes/MaterialCompact/{0}/styles.css" CssPostfix="MaterialCompact" runat="server"
                                                    Text="Fechar" Width="100px" ClientInstanceName="btnCancelar"
                                                    AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	//aspxGVScheduleCommand('pnCallback_gvDados',['CancelEdit']);
	gvDados.CancelEdit();
}" />
                                                </dxe:ASPxButton>
                                                <%--<dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" runat="server"
                                                        ReplacementType="EditFormCancelButton" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </EditForm>
                            </Templates>
                        </dxwgv:ASPxGridView>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </div>
        <asp:SqlDataSource ID="dataSource" runat="server" DeleteCommand="UPDATE AnalisePerformance
        SET DataExclusao = GETDATE(),
               CodigoUsuarioExclusao = @CodigoUsuario
 WHERE CodigoAnalisePerformance = @CodigoAnalisePerformance"
            InsertCommand=" SELECT @CodigoTipoAssociacao = CodigoTipoAssociacao FROM TipoAssociacao WHERE IniciaisTipoAssociacao = 'PR'

 INSERT INTO AnalisePerformance(
        CodigoObjetoAssociado, 
        CodigoTipoAssociacao, 
        DataAnalisePerformance, 
        PrincipaisResultados, 
        PontosAtencao,
        CodigoUsuarioInclusao, 
        DataInclusao,
        IndicaRegistroEditavel)
 VALUES(
        @CodigoObjeto, 
        @CodigoTipoAssociacao, 
        GETDATE(), 
        @PrincipaisResultados, 
        @PontosAtencao,
        @CodigoUsuario, 
        GETDATE(), 
        'S')
        
    SET @CodigoAnalisePerformance = @@IDENTITY
            
 SELECT TOP 1 
        @CodigoStatusReport = sr.CodigoStatusReport
   FROM StatusReport sr 
  WHERE sr.CodigoObjeto = @CodigoObjeto
    AND sr.CodigoTipoAssociacaoObjeto = @CodigoTipoAssociacao
  ORDER BY 
        DataGeracao DESC
        
 UPDATE StatusReport 
    SET CodigoAnalisePerformance = @CodigoAnalisePerformance 
  WHERE CodigoStatusReport = @CodigoStatusReport
    AND DataExclusao IS NULL
    AND CodigoAnalisePerformance IS NULL
    AND DataPublicacao IS NULL"
            SelectCommand=" SELECT app.*, 
        ui.NomeUsuario As UsuarioInclusao,  
        ua.NomeUsuario As UsuarioAlteracao, 
        (CASE WHEN EXISTS(SELECT 1 
                            FROM StatusReport sr 
                           WHERE sr.CodigoAnalisePerformance = app.CodigoAnalisePerformance 
                             AND sr.CodigoObjeto = @CodigoObjeto 
                             AND sr.CodigoTipoAssociacaoObjeto = (SELECT CodigoTipoAssociacao FROM TipoAssociacao WHERE IniciaisTipoAssociacao = 'PR') 
                             AND sr.DataExclusao IS NULL) THEN 'S' 
              ELSE 'N' END) AS ExisteVinculoStatusReport
  FROM              [AnalisePerformance]    AS app 
        LEFT JOIN   [Usuario]               AS ui   ON app.CodigoUsuarioInclusao = ui.CodigoUsuario
        LEFT JOIN   [Usuario]               AS ua   ON app.CodigoUsuarioUltimaAlteracao = ua.CodigoUsuario
  WHERE CodigoObjetoAssociado   = @CodigoObjeto
    AND CodigoTipoAssociacao    = (SELECT CodigoTipoAssociacao FROM TipoAssociacao WHERE IniciaisTipoAssociacao = 'PR')
    AND app.DataExclusao is null
  ORDER BY 
        app.DataAnalisePerformance DESC"
            UpdateCommand=" UPDATE AnalisePerformance
    SET DataAnalisePerformance          = GETDATE(), 
        PrincipaisResultados            = @PrincipaisResultados, 
        PontosAtencao                   = @PontosAtencao, 
        CodigoUsuarioUltimaAlteracao    = @CodigoUsuario, 
        DataUltimaAlteracao             = GETDATE()
  WHERE CodigoAnalisePerformance        = @CodigoAnalisePerformance"
            OnUpdating="dataSource_Updating">
            <DeleteParameters>
                <asp:Parameter Name="CodigoAnalisePerformance" Type="Int32" />
                <asp:SessionParameter Name="CodigoUsuario" SessionField="cul" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="CodigoTipoAssociacao" />
                <asp:QueryStringParameter Name="CodigoObjeto" QueryStringField="IDProjeto" />
                <asp:Parameter Name="PrincipaisResultados" />
                <asp:Parameter Name="PontosAtencao" />
                <asp:SessionParameter Name="CodigoUsuario" SessionField="cul" />
                <asp:Parameter Name="CodigoAnalisePerformance" />
                <asp:Parameter Name="CodigoStatusReport" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="CodigoObjeto" QueryStringField="IDProjeto" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="PrincipaisResultados" />
                <asp:Parameter Name="PontosAtencao" />
                <asp:SessionParameter Name="CodigoUsuario" SessionField="cul" />
                <asp:Parameter Name="CodigoAnalisePerformance" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfDadosSessao" ID="hfDadosSessao">
        </dxhf:ASPxHiddenField>
        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
            Landscape="True" ExportEmptyDetailGrid="True" ID="ASPxGridViewExporter1"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default>
                </Default>
                <Header>
                </Header>
                <Cell>
                </Cell>
                <Footer>
                </Footer>
                <GroupFooter>
                </GroupFooter>
                <GroupRow>
                </GroupRow>
                <Title></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
