<%@ Page Language="C#" AutoEventWireup="true" CodeFile="analisesCriticasProjetos.aspx.cs"
    Inherits="_Projetos_Boletim_analisesCriticasProjetos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="divHtmlPopUp" style="overflow-y: scroll; height: auto;">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>



    <script type="text/javascript" language="javascript">
        var execucaoSalvar;
        var textoFocus = '';
        function salvarAutomatico() {
            execucaoSalvar = setTimeout(function () { if (textoFocus != htmlAnaliseEdit.GetHtml()) { gvDadosBoletim.PerformCallback('S'); } else salvarAutomatico(); }, 30000);
        }
        function cancelaSalvarAutomatico() {
            clearTimeout(execucaoSalvar);
        }
    </script>
    <title></title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            height: 10px;
        }

        .style3 {
            height: 10px;
            width: 10px;
        }

        .style4 {
            width: 10px;
        }
    </style>
</head>
<body onload="window.top.pcModal.SetHeaderText('<%=tituloModal %>');" style="margin: 0px;">
    <form id="form1" runat="server">
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            OnCallback="pnCallback_Callback" Width="100%">
            <ClientSideEvents EndCallback="function(s, e) {
	local_onEnd_pnCallback(s,e);
}" />
            <PanelCollection>
                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <div>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="style3">&nbsp;
                                </td>
                                <td class="style2"></td>
                                <td class="style3">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">&nbsp;
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                        Text="Boletim">
                                    </dxe:ASPxLabel>
                                </td>
                                <td class="style4">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">&nbsp;
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtBoletim" runat="server" ClientEnabled="False" Width="100%">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style4">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px" class="style4">&nbsp;
                                </td>
                                <td style="padding-top: 10px">
                                    <div>
                                        <dxwgv:ASPxGridView ID="gvDadosBoletim" runat="server" AutoGenerateColumns="False"
                                            ClientInstanceName="gvDadosBoletim" KeyFieldName="CodigoProjeto"
                                            OnCommandButtonInitialize="gvDadosBoletim_CommandButtonInitialize" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                            OnCustomCallback="gvDadosBoletim_CustomCallback" OnHtmlDataCellPrepared="gvDadosBoletim_HtmlDataCellPrepared"
                                            OnRowUpdating="gvDadosBoletim_RowUpdating" Width="100%">
                                            <ClientSideEvents CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;btnEditar&quot;)
	{
		hfGeral.Set(&quot;TipoRegistroEmEdicao&quot;, &quot;filhos&quot;);
		popup.Show(); $('#divHtmlPopUp').attr('style', 'overflow-y: scroll; height: auto; color:red');
	}
}"
                                                EndCallback="function(s, e) {
	if(s.cp_Salvar == 'S')
	{
	
		textoFocus = htmlAnaliseEdit.GetHtml();
		if(hfGeral.Get('TipoRegistroEmEdicao') != 'filhos') 
			htmlAnalise.SetHtml(s.cp_Html);
        if(s.cp_Msg != '')
        {
            if(s.cp_status == 'ok')
                window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);
        }

    	salvarAutomatico();
	}
}" />
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowInCustomizationForm="False"
                                                    VisibleIndex="0" Width="50px">
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                            <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
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
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="False"
                                                    Visible="False" VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoStatusReport" ShowInCustomizationForm="False"
                                                    Visible="False" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="DescricaoObjeto" ShowInCustomizationForm="False"
                                                    VisibleIndex="3" Width="40%">
                                                    <EditFormSettings Visible="False" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataMemoColumn Caption="Análise Crítica" FieldName="ComentarioGeral"
                                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                                    <PropertiesMemoEdit ClientInstanceName="memoAnaliseCritica" Rows="25">
                                                        <ClientSideEvents Init="function(s, e) {
    document.getElementById('lblContador').innerText = s.GetText().length;
	return setMaxLength(s.GetInputElement(), 8000);
}" />
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings Caption="Análise Crítica: &lt;span style=&quot;color:Silver&quot;&gt;&lt;span id='lblContador'&gt;0&lt;/span&gt; de 8000&lt;/span&gt;"
                                                        CaptionLocation="Top" VisibleIndex="0" />
                                                </dxwgv:GridViewDataMemoColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                    AllowResize="True" Width="800px" />
                                            </SettingsPopup>
                                            <Settings VerticalScrollBarMode="Visible" />
                                            <SettingsText CommandCancel="Cancelar" CommandUpdate="Salvar" PopupEditFormCaption="Boletim" />
                                        </dxwgv:ASPxGridView>
                                    </div>
                                </td>
                                <td class="style4" style="padding-top: 10px">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">&nbsp;
                                </td>
                                <td>
                                    <div id="divAnaliseGeral" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="width: 50px" valign="middle">
                                                    <dxe:ASPxImage ID="btnEdicaoAnaliseGeral" runat="server" ClientInstanceName="btnEdicaoAnaliseGeral"
                                                        Cursor="pointer" ImageUrl="~/imagens/botoes/editarReg02.PNG">
                                                        <ClientSideEvents Click="function(s, e) {
	hfGeral.Set(&quot;TipoRegistroEmEdicao&quot;, &quot;pai&quot;);
	popup.Show();
}" />
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                    Text="Análise Geral: ">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxhe:ASPxHtmlEditor ID="htmlAnalise" runat="server" ActiveView="Preview" ClientInstanceName="htmlAnalise"
                                                                    Height="60px" Width="100%">
                                                                    <Settings AllowDesignView="False" AllowHtmlView="False" AllowInsertDirectImageUrls="False" />
                                                                    <SettingsLoadingPanel Text="Carregando" />
                                                                    <SettingsValidation ErrorText="O conteúdo HTML é inválido">
                                                                    </SettingsValidation>
                                                                </dxhe:ASPxHtmlEditor>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td class="style4">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-top: 10px" class="style4">&nbsp;
                                </td>
                                <td align="right" style="padding-top: 5px">
                                    <table cellpadding="0" cellspacing="0" style="margin-bottom: 15px">
                                        <tr>
                                            <td style="padding-right: 10px">
                                                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                    ClientVisible="False" Text="Salvar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if (window.onClick_BtnSalvar)
		onClick_BtnSalvar(s, e); $('#divHtmlPopUp').attr('style', 'overflow-y: scroll; height: auto; color:red');
}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td style="padding-right: 10px">
                                                <dxe:ASPxButton ID="btnAvancar" runat="server" AutoPostBack="False" ClientInstanceName="btnAvancar"
                                                    Text="Avançar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if (window.onClick_BtnAvancar)
		onClick_BtnAvancar(s, e);
}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" Text="Fechar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" class="style4" style="padding-top: 10px">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </dxp:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
        <dxpc:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton"
            HeaderText="Boletim" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" Width="800px" AllowDragging="True">
            <ClientSideEvents PopUp="function(s, e) {
	gvDadosBoletim.GetRowValues(
		gvDadosBoletim.GetFocusedRowIndex(), 
		&quot;ComentarioGeral&quot;,
		OnPopup);
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table width="100%">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                Text="Análise: ">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxhe:ASPxHtmlEditor ID="htmlAnaliseEdit" Height="300" runat="server" ClientInstanceName="htmlAnaliseEdit"
                                    Width="100%">
                                    <ClientSideEvents GotFocus="function(s, e) {
    cancelaSalvarAutomatico();
	textoFocus = s.GetHtml();
}" />
                                    <Toolbars>
                                        <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
                                            <Items>
                                                <dxhe:ToolbarCutButton>
                                                </dxhe:ToolbarCutButton>
                                                <dxhe:ToolbarCopyButton>
                                                </dxhe:ToolbarCopyButton>
                                                <dxhe:ToolbarPasteButton>
                                                </dxhe:ToolbarPasteButton>
                                                <dxhe:ToolbarPasteFromWordButton Visible="False">
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
                                                <dxhe:ToolbarInsertImageDialogButton Visible="False">
                                                </dxhe:ToolbarInsertImageDialogButton>
                                                <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                    <Items>
                                                        <dxhe:ToolbarInsertTableDialogButton BeginGroup="True">
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
                                                <dxhe:ToolbarParagraphFormattingEdit Width="120px">
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
                                                <dxhe:ToolbarFontNameEdit>
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
                                                <dxhe:ToolbarFontSizeEdit>
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
                                                <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                </dxhe:ToolbarBackColorButton>
                                                <dxhe:ToolbarFontColorButton>
                                                </dxhe:ToolbarFontColorButton>
                                            </Items>
                                        </dxhe:HtmlEditorToolbar>
                                    </Toolbars>
                                    <Settings AllowHtmlView="False" AllowPreview="False" />
                                    <SettingsLoadingPanel Text="Carregando" />
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
                                    <SettingsValidation ErrorText="O conteúdo HTML é inválido">
                                    </SettingsValidation>
                                </dxhe:ASPxHtmlEditor>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/salvar.gif">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if (window.onClick_BtnSalvar){
		onClick_BtnSalvar(s, e);
	}
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <dxe:ASPxImage ID="ASPxImage2" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/cancelar.gif">
                                                <ClientSideEvents Click="function(s, e) {
	popup.Hide();
	htmlAnaliseEdit.SetHtml(htmlAnalise.GetHtml());
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxhf:ASPxHiddenField ID="hfDadosSessao" runat="server">
        </dxhf:ASPxHiddenField>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        </dxwgv:ASPxGridViewExporter>
    </form>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".dxpc-closeBtn").on('click', function (event) {
                $("#divHtmlPopUp").attr("style", "overflow-y: scroll; height: auto; color:red");
            });
        });
    </script>
</body>



</html>
