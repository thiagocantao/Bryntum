<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnvioMensagens.aspx.cs" Inherits="_Default"
    EnableViewState="False" EnableSessionState="ReadOnly" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Mensagens</title>
    <script type="text/javascript">
        var myObject = null;
        var posExecutar = null;
        var urlModal = "";
        var retornoModal = null;
        var marcacaoAlta = 'N';

        function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
            if (parseInt(sHeight) < 535)
                sHeight = parseInt(sHeight) + 20;

            myObject = objParam;
            posExecutar = sFuncaoPosModal != "" ? sFuncaoPosModal : null;
            objFrmModal = document.getElementById('frmModal');

            pcModal.SetWidth(sWidth);
            objFrmModal.style.width = "100%";
            objFrmModal.style.height = sHeight + "px";
            urlModal = sUrl;
            //setTimeout ('alteraUrlModal();', 0);            
            pcModal.SetHeaderText(sHeaderTitulo);
            pcModal.Show();

        }

        function fechaModal() {
            pcModal.Hide();
        }

        function resetaModal() {
            objFrmModal = document.getElementById('frmModal');
            posExecutar = null;
            objFrmModal.src = "";
            pcModal.SetHeaderText("");
            retornoModal = null;
        }

    </script>
    <style type="text/css">
        .style1 {
            height: 8px;
        }

        .btn {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="width: 100%">
            <table cellpadding="0"
                cellspacing="0" class="headerGrid" width="100%">
                <tr>
                    <td style="padding-top: 8px; padding-bottom: 3px">
                        <dxe:ASPxButton runat="server" AutoPostBack="False"
                            ClientInstanceName="btnPara" Text="Para..." Width="65px"
                            ID="btnPara" CssClass="btn">
                            <ClientSideEvents Click="function(s, e) {
	abreDestinatariosPopUp();
}"></ClientSideEvents>

                            <Paddings Padding="0px"></Paddings>
                        </dxe:ASPxButton>

                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxMemo runat="server" Rows="4" Width="100%"
                            ClientInstanceName="txtDestinatarios" ClientEnabled="False"
                            ID="txtDestinatarios">
                            <ClientSideEvents KeyDown="function(s, e) {
                                                                       
	if(e.htmlEvent.keyCode != 46 &amp;&amp; e.htmlEvent.keyCode != 8)
		ASPxClientUtils.PreventEvent(e.htmlEvent);
}"></ClientSideEvents>

                            <DisabledStyle BackColor="#EBEBEB" ForeColor="#333333"></DisabledStyle>
                        </dxe:ASPxMemo>

                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfDestinatarios"
                            ID="hfDestinatarios">
                        </dxhf:ASPxHiddenField>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="Assunto:"
                                                    ID="ASPxLabel4">
                                                </dxe:ASPxLabel>

                                            </td>
                                            <td style="width: 200px">
                                                <dxe:ASPxLabel runat="server" Text="Categoria:"
                                                    ID="ASPxLabel8">
                                                </dxe:ASPxLabel>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="98%" ClientInstanceName="txtAssunto"
                                                    ID="txtAssunto">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxe:ASPxTextBox>

                                            </td>
                                            <td style="width: 200px">
                                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                                    Width="100%" ClientInstanceName="ddlCategoria"
                                                    ID="ddlCategoria">
                                                </dxe:ASPxComboBox>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="padding-left: 5px; width: 140px;">
                                    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" GroupName="Prioridade"
                                                    ClientInstanceName="btnAltaPrioridade" ImageSpacing="3px"
                                                    HorizontalAlign="Left" CausesValidation="False" Text="Alta Prioridade"
                                                    Width="100%" ID="btnAltaPrioridade" CssClass="btn">
                                                    <Image Url="~/imagens/vazio.PNG"></Image>
                                                    <Paddings Padding="0px"></Paddings>
                                                    <BorderBottom BorderStyle="None"></BorderBottom>
                                                    <ClientSideEvents Click="                                                                     
                                                                     function(s, e)
                                                                     {  
                                                                        if (marcacaoAlta == 'S') 
                                                                        {
                                                                              s.SetImageUrl('../imagens/vazio.PNG');
                                                                              marcacaoAlta = 'N';
                                                                              s.SetChecked(false);
                                                                        }
                                                                        else 
                                                                        {
                                                                              s.SetImageUrl('../imagens/selecionado.PNG');
                                                                              marcacaoAlta = 'S';
                                                                              s.SetChecked(true);
                                                                        }
                                                                     }" />
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
                    <td style="height: 4px">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="ClientMailEditor"
                            Width="100%" Height="225px"
                            ID="MailEditor">
                            <ClientSideEvents Init="MailDemo.ClientMailEditor_Init"></ClientSideEvents>

                            <StylesToolbars>
                                <Toolbar>
                                    <Paddings Padding="0px"></Paddings>
                                </Toolbar>

                                <ToolbarItem>
                                    <Paddings Padding="4px"></Paddings>
                                </ToolbarItem>
                            </StylesToolbars>
                            <Toolbars>
                                <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
                                    <Items>
                                        <dxhe:ToolbarCutButton></dxhe:ToolbarCutButton>
                                        <dxhe:ToolbarCopyButton></dxhe:ToolbarCopyButton>
                                        <dxhe:ToolbarPasteButton></dxhe:ToolbarPasteButton>
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
                                        <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                            <Items>
                                                <dxhe:ToolbarInsertTableDialogButton BeginGroup="True"></dxhe:ToolbarInsertTableDialogButton>
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
                                            </Items>
                                        </dxhe:ToolbarTableOperationsDropDownButton>
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
                                        <dxhe:ToolbarBackColorButton BeginGroup="True"></dxhe:ToolbarBackColorButton>
                                        <dxhe:ToolbarFontColorButton></dxhe:ToolbarFontColorButton>
                                        <dxhe:ToolbarFullscreenButton>
                                        </dxhe:ToolbarFullscreenButton>
                                    </Items>
                                </dxhe:HtmlEditorToolbar>
                            </Toolbars>

                            <Settings AllowInsertDirectImageUrls="False" AllowHtmlView="False" AllowPreview="False"></Settings>

                            <SettingsHtmlEditing EnterMode="BR"></SettingsHtmlEditing>
                            <SettingsDialogs>
                                <InsertImageDialog>
                                    <SettingsImageUpload UploadFolderUrlPath=""></SettingsImageUpload>
                                </InsertImageDialog>
                            </SettingsDialogs>
 

                            <Border BorderWidth="1px"></Border>
                        </dxhe:ASPxHtmlEditor>

                    </td>
                </tr>
                <tr>
                    <td class="style1"></td>
                </tr>
                <tr>
                    <td align="right">
                        <table>
                            <tr>
                                <td align="right">
                                    <dxe:ASPxButton ID="btnSalvar" runat="server"
                                        ClientInstanceName="ClientMailSendButton" Height="5px"
                                        Text="Salvar" Width="90px" CssClass="btn"
                                        AutoPostBack="False">
                                        <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                        <ClientSideEvents Click="MailDemo.ClientMailSendButton_Click" />
                                    </dxe:ASPxButton>
                                </td>
                                <td align="right"></td>
                                <td>
                                    <dxe:ASPxButton ID="btnCancelar" runat="server" ClientInstanceName="btnCancelar"
                                        Height="1px" Text="Fechar" Width="90px" CssClass="btn"
                                        AutoPostBack="False">
                                        <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                        <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	window.top.fechaModal();	
}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
            </dxhf:ASPxHiddenField>
            <dxcb:ASPxCallback ID="callbackSalvar" runat="server"
                ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
                <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg != '')
	{
		if(s.cp_Status == 'OK')
		{
                                                 window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
			 window.top.fechaModal();
		}
                               else if(s.cp_Status == 'ERR')
                               {
                                               window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);
                               }
	}
}" />
            </dxcb:ASPxCallback>
            <dxpc:ASPxPopupControl ID="pcModal" runat="server" ClientInstanceName="pcModal"
                HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                        <iframe id="frmModal" name="frmModal" frameborder="0" style="overflow: auto; padding: 0px; margin: 0px;"></iframe>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <ClientSideEvents CloseUp="function(s, e) {
            var retorno = '';
            
            if(retornoModal != null)
            {   
                retorno = retornoModal;
            }
            
            if(posExecutar != null)
               posExecutar(retorno);
                
            resetaModal();
}"
                    PopUp="function(s, e) {
    window.document.getElementById('frmModal').dialogArguments = myObject;
	document.getElementById('frmModal').src = urlModal;
}" />
                <ContentStyle>
                    <Paddings Padding="5px" />
                </ContentStyle>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
