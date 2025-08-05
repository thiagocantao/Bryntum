<%@ Page Language="C#" AutoEventWireup="true" CodeFile="analisesRelatorioAcompanhamentoProjetosUnidade.aspx.cs"
    Inherits="_Projetos_Boletim_analisesRelatorioAcompanhamentoProjetosUnidade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
    .Resize textarea {
            resize: both;
        }
        .auto-style1 {
            width: 100%;
        }
        </style>
    <script type="text/javascript">
        function mostraPopupBoletim() {
            window.top.fechaModal();
            var varOpener = this;
            var iniciais = btnAvancar.cp_iniciais;
            var url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/popupRelProgramas.aspx?podeEditar=" + btnAvancar.cp_podeEditar + "&codStatusReport=" + btnAvancar.cp_CodigoStatusReport + "&iniciais=" + iniciais;
            window.top.showModal(url, 'Boletim', 900, (screen.height - 190), null, varOpener);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="auto-style1">
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Boletim">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxTextBox ID="txtBoletim" runat="server" ClientEnabled="False"
                        Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dxtv:ASPxGridView ID="gvDadosBoletim" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDadosBoletim" DataSourceID="dataSource"  KeyFieldName="CodigoProjeto" Width="100%" OnHtmlDataCellPrepared="gvDadosBoletim_HtmlDataCellPrepared">
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
        </SettingsEditing>
        <Settings VerticalScrollBarMode="Visible" />
        <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="800px" ShowCloseButton="true" />
        </SettingsPopup>
        <SettingsText CommandCancel="Cancelar" CommandUpdate="Salvar" PopupEditFormCaption="Boletim" />
        <StylesEditors>
            <Style>
            </Style>
            <Memo >
            </Memo>
        </StylesEditors>
        <Columns>
            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowEditButton="True" ShowInCustomizationForm="False" VisibleIndex="0" Width="50px">
            </dxtv:GridViewCommandColumn>
            <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="False" Visible="False" VisibleIndex="1">
                <EditFormSettings Visible="False" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn FieldName="CodigoStatusReport" ShowInCustomizationForm="False" Visible="False" VisibleIndex="2">
                <EditFormSettings Visible="False" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Projeto" FieldName="DescricaoObjeto" ShowInCustomizationForm="False" VisibleIndex="3">
                <EditFormSettings Visible="False" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataMemoColumn Caption="Execução Financeira do Projeto" FieldName="PontosAtencao" ShowInCustomizationForm="True" VisibleIndex="5">
                <PropertiesMemoEdit Rows="10">
                    <Style CssClass="Resize" Font-Size="10pt">
                    </Style>
                </PropertiesMemoEdit>
                <EditFormSettings Caption="Execução Financeira do Projeto" CaptionLocation="Top" Visible="True" />
                <EditItemTemplate>
                    <dxhe:ASPxHtmlEditor ID="htmlExecucaoFinanceiraProjeto" runat="server" ClientInstanceName="htmlExecucaoFinanceiraProjeto" Height="150px" Width="100%" OnInit="htmlExecucaoFinanceiraProjeto_Init" Font-Names="Calibri" Font-Size="11pt">
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
                    </dxhe:ASPxHtmlEditor>
                </EditItemTemplate>
            </dxtv:GridViewDataMemoColumn>
            <dxtv:GridViewDataMemoColumn Caption="Execução Física do Projeto" FieldName="PrincipaisResultados" ShowInCustomizationForm="True" VisibleIndex="4">
                <PropertiesMemoEdit Rows="10">
                    <Style CssClass="Resize" Font-Size="10pt">
                    </Style>
                </PropertiesMemoEdit>
                <EditFormSettings Caption="Execução Física do Projeto" CaptionLocation="Top" Visible="True" />
                <EditItemTemplate>
                    <dxhe:ASPxHtmlEditor ID="htmlExecucaoFisicaProjeto" runat="server" ClientInstanceName="htmlExecucaoFisicaProjeto" Height="150px" Width="100%" OnInit="htmlExecucaoFisicaProjeto_Init" Font-Names="Calibri" Font-Size="11pt">
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
                </EditItemTemplate>
            </dxtv:GridViewDataMemoColumn>
        </Columns>
    </dxtv:ASPxGridView>
                    <asp:SqlDataSource ID="dataSource" runat="server" SelectCommand=" SELECT sr.CodigoObjeto AS CodigoProjeto, 
        sr.CodigoStatusReport,
		(SELECT dbo.f_GetDescricaoOrigemAssociacaoObjeto(@CodigoEntidade, sr.CodigoTipoAssociacaoObjeto, null, sr.CodigoObjeto,0,null) ) AS DescricaoObjeto, 
        ap.IndicaRegistroEditavel, 
        ap.PrincipaisResultados,
        ap.PontosAtencao
   FROM StatusReport AS sr  LEFT JOIN
		AnalisePerformance ap ON ap.CodigoAnalisePerformance = sr.CodigoAnalisePerformance
  WHERE sr.CodigoStatusReportSuperior = @CodigoStatusReport
  ORDER BY 2" UpdateCommand="    SET @Data = GETDATE()
    
 SELECT @CodigoAnalisePerformace_Original = sr.CodigoAnalisePerformance,
        @CodigoObjetoAssociado = sr.CodigoObjeto,
        @CodigoTipoAssociacao = sr.CodigoTipoAssociacaoObjeto
   FROM StatusReport AS sr 
  WHERE sr.CodigoStatusReport = @CodigoStatusReport
        
IF(EXISTS(SELECT 1 
            FROM AnalisePerformance AS ap
           WHERE ap.CodigoAnalisePerformance = @CodigoAnalisePerformace_Original
             AND ap.IndicaRegistroEditavel = 'S'))
BEGIN
     UPDATE AnalisePerformance
        SET DataAnalisePerformance = @Data,
            PrincipaisResultados = @PrincipaisResultados,
            PontosAtencao = @PontosAtencao,
            CodigoUsuarioUltimaAlteracao = @CodigoUsuario,
            DataUltimaAlteracao = @Data
      WHERE CodigoAnalisePerformance = @CodigoAnalisePerformace_Original
END
ELSE
BEGIN
    INSERT INTO AnalisePerformance
           (CodigoObjetoAssociado
           ,CodigoTipoAssociacao
           ,DataAnalisePerformance
           ,PrincipaisResultados
           ,PontosAtencao
           ,CodigoUsuarioInclusao
           ,DataInclusao  
           ,IndicaRegistroEditavel)
     VALUES
           (@CodigoObjetoAssociado
           ,@CodigoTipoAssociacao
           ,@Data
           ,@PrincipaisResultados
           ,@PontosAtencao
           ,@CodigoUsuario
           ,@Data          
           ,'S')

    SET @CodigoAnalisePerformace = SCOPE_IDENTITY()

IF @CodigoAnalisePerformace_Original IS NULL
 UPDATE StatusReport
    SET CodigoAnalisePerformance = @CodigoAnalisePerformace
  WHERE CodigoAnalisePerformance IS NULL
    AND CodigoObjeto = @CodigoObjetoAssociado
    AND CodigoTipoAssociacaoObjeto = @CodigoTipoAssociacao
    AND DataPublicacao IS NULL
ELSE
 UPDATE StatusReport
    SET CodigoAnalisePerformance = @CodigoAnalisePerformace
  WHERE CodigoAnalisePerformance = @CodigoAnalisePerformace_Original
    AND DataPublicacao IS NULL
END

 UPDATE StatusReport
    SET DataUltimaAlteracao = @Data,
        CodigoUsuarioUltimaAlteracao = @CodigoUsuario
  WHERE CodigoStatusReport = @CodigoStatusReport" OnUpdating="dataSource_Updating">
                        <SelectParameters>
                            <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
                            <asp:QueryStringParameter Name="IniciaisModelo" QueryStringField="iniciais" />
                            <asp:QueryStringParameter Name="CodigoStatusReport" 
                                QueryStringField="codStatusReport" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:SessionParameter Name="CodigoUsuario" SessionField="cul" />
                            <asp:QueryStringParameter Name="CodigoStatusReport" 
                                QueryStringField="codStatusReport" />
                            <asp:Parameter Name="CodigoTipoAssociacao" />
                            <asp:Parameter Name="CodigoObjetoAssociado" />
                            <asp:Parameter Name="CodigoAnalisePerformace" />
                            <asp:Parameter Name="CodigoAnalisePerformace_Original" />
                            <asp:Parameter Name="PrincipaisResultados" />
                            <asp:Parameter Name="PontosAtencao" />
                            <asp:Parameter Name="Data" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
    
                </td>
            </tr>
            <tr>
                <td>
                    <table style="margin-left: auto">
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" 
                                                ClientInstanceName="btnSalvar" ClientVisible="False" 
                                                Text="Salvar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if (window.onClick_BtnSalvar)
		onClick_BtnSalvar(s, e);
}" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="padding-right: 10px">
                                            <dxe:ASPxButton ID="btnAvancar" runat="server" AutoPostBack="False" 
                                                ClientInstanceName="btnAvancar"  
                                                Text="Avançar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	mostraPopupBoletim();
}" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" 
                                                 Text="Fechar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                </td>
            </tr>
        </table>
        &nbsp;</div>
    </form>
</body>
</html>
