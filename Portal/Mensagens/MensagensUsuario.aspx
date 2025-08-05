<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MensagensUsuario.aspx.cs" Inherits="_VisaoExecutiva_MensagensExecutivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self" />
    <title>Mensagens</title>
    <script type="text/javascript" language="javascript">
        function OnGridFocusedRowChanged(grid) 
        {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'Mensagem;Assunto', MontaCampos);
        }
        
        function MontaCampos(values)
        {
//            txtMensagem.SetText(values[0]);
//            txtResposta.SetText("");
//            txtAssunto.SetText(values[1]);
        }
        
        function abreLeitura()
        {
//            pcDados.Show();
//            txtResposta.SetEnabled(false);
//            btnSalvar.SetVisible(false);
        }
        
        function abreEdicao()
        {
//            pcDados.Show();
//            txtResposta.SetEnabled(true);
//            btnSalvar.SetVisible(true);
        }        
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="gvMensagens" runat="server" AutoGenerateColumns="False"
                        ClientInstanceName="gvMensagens"  KeyFieldName="CodigoMensagem" OnHtmlDataCellPrepared="gvMensagens_HtmlDataCellPrepared" OnCustomButtonInitialize="gvMensagens_CustomButtonInitialize" Width="100%">
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <SettingsText ConfirmDelete="Deseja Realmente Excluir a Associa&#231;&#227;o?"  />
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" 
                                Width="50px">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnLer" Text="Ler">
                                        <Image Url="~/imagens/botoes/lerMensagem.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Objeto Associado"
                                VisibleIndex="6">
                            </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Width="80px" Caption="Prioridade" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Remetente" FieldName="NomeUsuario" VisibleIndex="3"
                                Width="180px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Data Envio" FieldName="DataInclusao" VisibleIndex="1"
                                Width="90px">
<PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesTextEdit>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Assunto" FieldName="Assunto"
                                VisibleIndex="2" Width="230px">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
	if(e.buttonID == 'btnLer')
		abreLeitura();	
	else
		abreEdicao();
}" />
                        <Styles>
                            <CommandColumnItem>
                                <Paddings PaddingRight="2px" />
                            </CommandColumnItem>
                        </Styles>
                    </dxwgv:ASPxGridView>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharGeral"
                                         Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                </td>
                <td>
                </td>
            </tr>
            </table>
    
    </div>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_MSG != &quot;&quot;)
	{
		window.top.mostraMensagem(s.cp_MSG, 'sucesso', false, false, null);
		pcDados.Hide();
		window.top.retornoModal = 'S';	
	}
	else if(s.cp_Erro != &quot;&quot;)
	{
		window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
		pcDados.Hide();
		window.top.retornoModal = 'S';
	}

}" />
        </dxcb:ASPxCallback>
        
                                        <dxpc:ASPxPopupControl ID="MailEditorPopup" 
        runat="server" AllowDragging="True" 
                                            
        ClientInstanceName="ClientMailEditorPopup" CloseAction="CloseButton" 
                                            PopupAnimationType="None" 
        HeaderText="Mensagem" Modal="True" 
                                            OnWindowCallback="MailEditorPopup_WindowCallback" 
                                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        ShowFooter="True" Width="960px" 
                                            >
                                            <ContentStyle>
                                                <Paddings Padding="0px" PaddingLeft="2px" PaddingRight="2px" />
                                            </ContentStyle>
                                            <FooterTemplate>
                                                <dxe:ASPxButton ID="MailCancelButton" runat="server" AutoPostBack="False" 
                                                    CssClass="MailPopupButton" Text="Cancelar" 
                                                    Width="100px">
                                                    <ClientSideEvents Click="" />
                                                    <Paddings Padding="0px" />
                                                </dxe:ASPxButton>
                                                <dxe:ASPxButton ID="MailSendButton" runat="server" AutoPostBack="False" 
                                                    ClientInstanceName="ClientMailSendButton" CssClass="MailPopupButton" 
                                                    Text="Enviar"  Width="100px">
                                                    <ClientSideEvents Click="" />
                                                    <Paddings Padding="0px" />
                                                </dxe:ASPxButton>
                                                <div class="clear">
                                                </div>
                                            </FooterTemplate>
                                            <ContentCollection>
                                                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                        <tr>
                                                            <td style="padding-top: 8px; padding-bottom: 3px">
                                                                <dxe:ASPxButton ID="btnPara" runat="server" AutoPostBack="False" 
                                                                    ClientInstanceName="btnPara"  
                                                                    Text="Para..." Width="65px">
                                                                    <ClientSideEvents Click="function(s, e) {
	abreDestinatarios();
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxMemo ID="txtDestinatarios" runat="server" 
                                                                    ClientInstanceName="txtDestinatarios"  
                                                                    Rows="3" Width="100%" ClientEnabled="False">
                                                                    <ClientSideEvents KeyDown="function(s, e) {
                                                                       
	if(e.htmlEvent.keyCode != 46 &amp;&amp; e.htmlEvent.keyCode != 8)
		ASPxClientUtils.PreventEvent(e.htmlEvent);
}" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="#333333">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxMemo>
                                                                <dxhf:ASPxHiddenField ID="hfDestinatarios" runat="server" 
                                                                    ClientInstanceName="hfDestinatarios">
                                                                </dxhf:ASPxHiddenField>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 4px">
                                                                <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                                                            Text="Assunto:">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="txtAssunto" runat="server" ClientInstanceName="txtAssunto" 
                                                                                             Width="100%">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 150px;">
                                                                            <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxButton ID="btnAltaPrioridade" runat="server" 
                                                                                            Text="Alta Prioridade" Width="100%" AutoPostBack="False" 
                                                                                            ClientInstanceName="btnAltaPrioridade" HorizontalAlign="Left" 
                                                                                            ImageSpacing="3px" CausesValidation="False" GroupName="Prioridade">
                                                                                            <Image Url="~/imagens/PrioridadeAlta.png">
                                                                                            </Image>
                                                                                            <Paddings Padding="0px" />
                                                                                            <BorderBottom BorderStyle="None" />
                                                                                        </dxe:ASPxButton> 
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxButton ID="btnBaixaPrioridade" runat="server" AutoPostBack="False" Checked="false"
                                                                                            ClientInstanceName="btnBaixaPrioridade"  
                                                                                            HorizontalAlign="Left" ImageSpacing="3px" Text="Baixa Prioridade" 
                                                                                            Width="100%" CausesValidation="False" GroupName="Prioridade">
                                                                                            <Image Url="~/imagens/PrioridadeBaixa.png">
                                                                                            </Image>
                                                                                            <Paddings Padding="0px" />
                                                                                            <CheckedStyle BackColor="#FFCC66">
                                                                                            </CheckedStyle>
                                                                                            <BorderTop BorderStyle="None" />
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 4px">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxhe:ASPxHtmlEditor ID="MailEditor" runat="server" 
                                                                    ClientInstanceName="ClientMailEditor"  
                                                                    Height="250px" Width="100%">
                                                                    <ClientSideEvents Init="MailDemo.ClientMailEditor_Init" />
                                                                    <StylesToolbars>
                                                                        <Toolbar>
                                                                            <Paddings Padding="0px" />
                                                                        </Toolbar>
                                                                        <ToolbarItem>
                                                                            <Paddings Padding="4px" />
                                                                        </ToolbarItem>
                                                                    </StylesToolbars>
                                                                    <Toolbars>
                                                                        <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
                                                                            <Items>
                                                                                <dxhe:ToolbarCutButton>
                                                                                </dxhe:ToolbarCutButton>
                                                                                <dxhe:ToolbarCopyButton>
                                                                                </dxhe:ToolbarCopyButton>
                                                                                <dxhe:ToolbarPasteButton>
                                                                                </dxhe:ToolbarPasteButton>
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
                                                                                <dxhe:ToolbarFontNameEdit>
                                                                                    <Items>
                                                                                        <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                                        <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                                        <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                                        <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                                        <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                                        <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
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
                                                                                <dxhe:ToolbarFullscreenButton>
                                                                                </dxhe:ToolbarFullscreenButton>
                                                                            </Items>
                                                                        </dxhe:HtmlEditorToolbar>
                                                                       
                                                                    </Toolbars>
                                                                    <Settings AllowHtmlView="False" AllowPreview="False" 
                                                                        AllowInsertDirectImageUrls="False" />
                                                                    <SettingsHtmlEditing EnterMode="BR" />
                                                                    <Border BorderWidth="0px" />
                                                                </dxhe:ASPxHtmlEditor>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxpc:PopupControlContentControl>
                                            </ContentCollection>
                                        </dxpc:ASPxPopupControl>        
    </form>
</body>
</html>
