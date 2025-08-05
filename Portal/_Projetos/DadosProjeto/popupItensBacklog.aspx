<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupItensBacklog.aspx.cs" Inherits="_Projetos_DadosProjeto_popupItensBacklog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Ítens</title>
    <style type="text/css">
        #btnCopiar {
            font-family: FontAwesome;
            border-style: solid;
            height: 35px;
            border-width: thin;
            border-color: darkgray;
            border-radius: 2px;
        }

        .style2 {
            width: 100%;
        }

        .style3 {
            width: 377px;
        }

        .auto-style1 {
            height: 33px;
        }

        .Resize textarea {
            resize: both;
        }

        .auto-style2 {
            width: 120px;
        }

        .auto-style3 {
            width: 300px;
        }

        .auto-style4 {
            width: 171px;
            height: 14px;
        }

        .auto-style5 {
            height: 14px;
        }
    </style>
    <script src="../../scripts/clipboard.min.js"></script>
    <script>
        function setFrameSource() {
            const urlParams = new URLSearchParams(window.location.search);
            const codigoItem = urlParams.get('CI');
            const frame = document.getElementById('frmComentarios');
            const acao = urlParams.get('acao');
            if (!(frame.src))
                frame.src = '../Agil/Comentarios.aspx?ini=IB&co=' + codigoItem + '&acao=' + acao;
        }

        function setFrameHistoricoSource() {
            const urlParams = new URLSearchParams(window.location.search);
            const codigoItem = urlParams.get('CI');
            const frame = document.getElementById('frmHistorico');
            if (!(frame.src))
                frame.src = '../Agil/agil_HistoricoItem.aspx?CI=' + codigoItem;
        }

        function funcaoCallbackSalvar()
        {
            
            var acao = btnSalvar.cpAcao;
            var msgValidacao = ValidaCamposFormulario();
            if (msgValidacao == '') {
                window.top.dadosAlteradosNoPopup = false;
                callbackTela.PerformCallback(acao);
            }
            else {
                window.top.mostraMensagem(msgValidacao, 'atencao', true, false, null);
            }
        }

        function funcaoCallbackFechar() {
            //e.processOnServer = false;
            callbackBotaoFechar.PerformCallback();
        }

        function funcaoCallbackAdicionarComentario() {
            window.frames['frmComentarios'].contentWindow.funcaoAdicionarComentario();
        }

    </script>
</head>
<body style="margin: 0">
    <input id="retorno_popup" name="retorno_popup" runat="server" type="hidden" value="" />
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="style2" id="tabelaPrincipal">
                <tr>
                    <td>
                        <dxcp:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl" Width="100%" ID="tabControl" OnLoad="tabControl_Load">
                            <TabPages>
                                <dxcp:TabPage Name="tabP" Text="Principal">
                                    <ContentCollection>
                                        <dxcp:ContentControl runat="server">
                                            <div id="divPrincipalPopup" runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: auto">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxcp:ASPxTokenBox runat="server" ItemValueType="System.String" ShowDropDownOnFocus="Never" Tokens="" TextSeparator="|" AllowMouseWheel="True" NullText="TAGS do item de backlog" EnableCallbackMode="True" HorizontalAlign="Left" Width="100%" Height="22px" ClientInstanceName="tagBox" EnableClientSideAPI="True" ToolTip="&#201; permitido cadastrar no m&#225;ximo at&#233; 3 tags, com at&#233; 40 caracteres cada uma." ID="tagBox" NullTextDisplayMode="UnfocusedAndFocused">
                                                                                    <ClientSideEvents TokensChanged="function(s, e) {
var colecaoDeTokens = s.GetTokenCollection();
if(colecaoDeTokens.length &gt; 3 )
      {
             s.RemoveToken(colecaoDeTokens.length  - 1);
     }
}"
                                                                                        Validation="function(s, e) {
     var textoExtraido = removerAcentosECaracteresEspeciais(s.GetText());
      var&nbsp;arrayTokens = textoExtraido.split('|');
      var contador = 0;
      var textoResultante = '';
        for(contador = 0; contador &lt; arrayTokens.length; contador++ )
      {
               var strTemp = arrayTokens[contador];
               strTemp = strTemp.substring(0, 40) + '|';
              textoResultante += strTemp; 
      }
      textoResultante  = textoResultante.substring(0, textoResultante.length - 1);
     s.SetText(textoResultante);
     e.isValid = true;
}"
                                                                                        KeyPress="function(s, e) {
	if(e.htmlEvent.keyCode == 13)
	{
		e.processOnServer = false; 
                                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
	}
}" LostFocus="function(s, e) {
		if(e.htmlEvent.keyCode == 13)
	{
		e.processOnServer = false; 
                                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
	}

}" ValueChanged="function(s, e) {
window.top.dadosAlteradosNoPopup = true;
}"></ClientSideEvents>

                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                        <RegularExpression ErrorText=""></RegularExpression>
                                                                                    </ValidationSettings>

                                                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </ReadOnlyStyle>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTokenBox>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>

                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblTitulo" Text="ID:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxtv:ASPxLabel ID="lblTitulo6" runat="server" ClientInstanceName="lblTitulo" Text="Título: *">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 85px">
                                                                            <dxtv:ASPxTextBox ID="txtCodigoItem" runat="server" ClientInstanceName="txtCodigoItem" ReadOnly="true" Width="100%">
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxtv:ASPxTextBox ID="txtTituloItem" runat="server" ClientInstanceName="txtTituloItem" MaxLength="150" Width="100%">
                                                                                <ClientSideEvents TextChanged="function(s, e) {
   window.top.dadosAlteradosNoPopup = true;
}" />
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxTextBox>
                                                                        </td>
                                                                        <td style="width: 25px">
                                                                            <button id="btnCopiar" type="button" title="Copiar o código e o título" data-clipboard-text="Just because you can doesn't mean you should — clipboard.js">
                                                                                &#xf0c5
                                                                            </button>


                                                                        </td>
                                                                    </tr>
                                                                </table>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 5px">
                                                                <dxtv:ASPxLabel ID="lblTitulo7" runat="server" ClientInstanceName="lblTitulo" Text="Detalhes / História:">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxhe:ASPxHtmlEditor ID="txtDetalheItem" runat="server" Theme="MaterialCompact" Width="100%" ClientInstanceName="txtDetalheItem">
                                                                    <ClientSideEvents HtmlChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
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
                                                                </dxhe:ASPxHtmlEditor>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 5px; padding-bottom: 5px;">
                                                                <table cellpadding="0" cellspacing="0" class="style2">
                                                                    <tr>
                                                                        <td style="width: 16.67%">
                                                                            <dxcp:ASPxLabel runat="server" Text="Esforço Previsto: " ClientInstanceName="lblTitulo" ID="lblTitulo8"></dxcp:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 16.67%">
                                                                            <dxcp:ASPxLabel runat="server" Text="Import&#226;ncia: *" ClientInstanceName="lblTitulo14" ID="lblTitulo14"></dxcp:ASPxLabel>

                                                                        </td>
                                                                        <td style="width: 16.67%">
                                                                            <dxcp:ASPxLabel runat="server" Text="% Conclu&#237;do:" ClientInstanceName="lblTitulo15" ID="lblTitulo15"></dxcp:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px; width: 16.67%;">
                                                                            <dxcp:ASPxSpinEdit runat="server" DecimalPlaces="2" Width="100%" DisplayFormatString="n2" ClientInstanceName="txtEsforco" ID="txtEsforco" MaxLength="11" MaxValue="99999999.99" Number="1">
                                                                                <SpinButtons ShowIncrementButtons="False"></SpinButtons>

                                                                                <ClientSideEvents NumberChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />

                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxcp:ASPxSpinEdit>

                                                                        </td>
                                                                        <td style="padding-right: 5px">
                                                                            <dxcp:ASPxSpinEdit runat="server" NumberType="Integer" Width="100%" DisplayFormatString="n0" ClientInstanceName="txtImportancia" ID="txtImportancia" MaxLength="5" MaxValue="32766" Number="1">
                                                                                <SpinButtons ShowIncrementButtons="False"></SpinButtons>

                                                                                <ClientSideEvents NumberChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />

                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxcp:ASPxSpinEdit>

                                                                        </td>
                                                                        <td>
                                                                            <dxcp:ASPxTextBox runat="server" DecimalPlaces="2" Width="100%" ClientInstanceName="txtPercentualConcluido" ClientEnabled="False" ID="txtPercentualConcluido">

                                                                                <ClientSideEvents TextChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />

                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxcp:ASPxTextBox>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="tdInfoSecundarias" style="padding-bottom: 5px;">
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td style="padding-top: 5px; width: 16.67%;">
                                                                            <dxtv:ASPxLabel ID="lblTitulo18" runat="server" ClientInstanceName="lblTitulo18" Text="Data Alvo:">
                                                                            </dxtv:ASPxLabel>

                                                                        </td>
                                                                        <td style="padding-top: 5px; width: 16.67%;">
                                                                            <dxtv:ASPxLabel ID="lblTitulo12" runat="server" ClientInstanceName="lblTitulo12" Text="Classificação:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>

                                                                        <td style="padding-top: 5px; width: 16.67%;">
                                                                            <dxtv:ASPxLabel ID="lblTitulo17" runat="server" ClientInstanceName="lblTitulo17" Text="Responsável:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px; padding-bottom: 10px;" class="auto-style1">
                                                                            <dxtv:ASPxDateEdit ID="dtDataAlvo" runat="server" ClientInstanceName="dtDataAlvo" PopupVerticalAlign="TopSides" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                            </dxtv:ASPxDateEdit>

                                                                        </td>
                                                                        <td style="padding-right: 5px; padding-bottom: 10px; width: 16.67%;">
                                                                            <dxtv:ASPxComboBox ID="ddlClassificacao" runat="server" ClientInstanceName="ddlClassificacao" Width="100%">
                                                                                <ItemStyle Wrap="True" />
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxComboBox>
                                                                        </td>
                                                                        <td style="padding-right: 5px; padding-bottom: 10px; width: 16.67%;">
                                                                            <dxtv:ASPxComboBox ID="ddlRecurso" runat="server" ClientInstanceName="ddlRecurso" TextField="NomeRecurso" ValueField="CodigoRecursoCorporativo" ValueType="System.Int32" Width="100%">
                                                                                <ItemStyle Wrap="True" />
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                                <ClientSideEvents Init="function(s, e) {
                                                                                     hfRecursoSelecionadoInit.PerformCallback();}" ValueChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />
                                                                            </dxtv:ASPxComboBox>
                                                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfRecursoSelecionadoInit" ID="hfRecursoSelecionadoInit"
                                                                                OnCustomCallback="hfRecursoSelecionadoInit_CustomCallback">
                                                                            </dxhf:ASPxHiddenField>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </dxcp:ContentControl>
                                    </ContentCollection>
                                </dxcp:TabPage>
                                <dxtv:TabPage Name="tabFinanceiro" Text="Avançado">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="divFinanceiro" runat="server">
                                                <table class="style2">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table class="style2">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 50%">
                                                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table align="left" cellpadding="0" cellspacing="0" class="auto-style2">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td style="width: 100px">
                                                                                                            <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Text="Custo Previsto:">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxImage ID="imgAjudaCustoPrevisto0" runat="server" ImageUrl="../../imagens/questao.gif" ToolTip="Informar aqui o custo fixo previsto relacionado a este item">
                                                                                                            </dxtv:ASPxImage>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxSpinEdit ID="spnCustoPrevisto" runat="server" ClientInstanceName="spnCustoPrevisto" DisplayFormatString="c2" Number="0">
                                                                                                <SpinButtons ClientVisible="False">
                                                                                                </SpinButtons>
                                                                                                <ClientSideEvents NumberChanged="function(s, e) {
		window.top.dadosAlteradosNoPopup = true;
}" />
                                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </ReadOnlyStyle>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxtv:ASPxSpinEdit>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Comentários sobre o custo:">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxMemo ID="memoComentarioCusto" runat="server" AutoResizeWithContainer="True" ClientInstanceName="memoComentarioCusto" Height="185px" Width="100%">
                                                                                                <ClientSideEvents TextChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />
                                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                                </ValidationSettings>
                                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </ReadOnlyStyle>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxtv:ASPxMemo>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxLabel ID="lblContadorMemoComentarioCusto" runat="server" ClientInstanceName="lblContadorMemoComentarioCusto" Font-Bold="True" ForeColor="#999999" Text="0 de 2000">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0" class="style2">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table cellpadding="0" cellspacing="0" class="auto-style2">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel7" runat="server" Text="Receita Prevista:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxImage ID="imgAjudaReceitaPrevista0" runat="server" ClientInstanceName="imgAjudaReceitaPrevista" ImageUrl="../../imagens/questao.gif" ToolTip="Informar aqui qual é a receita prevista após a entrega deste item">
                                                                                                        </dxtv:ASPxImage>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxSpinEdit ID="spnReceitaPrevista" runat="server" ClientInstanceName="spnReceitaPrevista" DisplayFormatString="c2" Number="0">
                                                                                                <SpinButtons ClientVisible="False">
                                                                                                </SpinButtons>
                                                                                                <ClientSideEvents NumberChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />
                                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </ReadOnlyStyle>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxtv:ASPxSpinEdit>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Comentários sobre a receita:">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxMemo ID="memoComentarioReceita" runat="server" AutoResizeWithContainer="True" ClientInstanceName="memoComentarioReceita" Height="185px" Width="100%">
                                                                                                <ClientSideEvents TextChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />
                                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                                </ValidationSettings>
                                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </ReadOnlyStyle>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxtv:ASPxMemo>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxLabel ID="lblContadorMemoComentarioReceita" runat="server" ClientInstanceName="lblContadorMemoComentarioReceita" Font-Bold="True" ForeColor="#999999" Text="0 de 2000">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" class="auto-style3">
                                                                    <tr>
                                                                        <td class="auto-style4">
                                                                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Text="Pacote de Trabalho Associado:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style5">
                                                                            <dxtv:ASPxImage ID="imgAjudaPacoteTrabalhoAssociado" runat="server" ClientInstanceName="imgAjudaPacoteTrabalhoAssociado" ImageUrl="../../imagens/questao.gif" ToolTip="Se estiver usando uma EAP de entregas de projeto, escolha aqui o pacote de trabalho no qual está vinculado o item.">
                                                                            </dxtv:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxComboBox ID="ddlPacoteTrabalhoAssociado" runat="server" ClientInstanceName="ddlPacoteTrabalhoAssociado" Width="100%">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	window.top.dadosAlteradosNoPopup = true;
}" />
                                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </ReadOnlyStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Text="Checklist" ToolTip="Checklist" Name="tabChecklist">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="divCheckList" runat="server">
                                                <iframe id="frmCheckList" src="" frameborder="0" height="100%" width="100%"></iframe>
                                            </div>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Text="Comentários" ToolTip="Comentários" Name="tabComentarios">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="divComentarios" runat="server">
                                                <iframe id="frmComentarios" style="height: 100%; width: 100%; overflow: auto; border: none;"></iframe>
                                            </div>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Name="tabHistorico" Text="Histórico">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="divHistorico" runat="server">
                                                <iframe id="frmHistorico" style="height: 100%; width: 100%; overflow: auto; border: none;"></iframe>
                                            </div>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Name="tabLinks" Text="Links">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="divLinks" runat="server">
                                                <iframe id="frmLinks" style="height: 100%; width: 100%; overflow: auto; border: none;"></iframe>
                                            </div>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxcp:TabPage Name="tabA" Text="Anexos">
                                    <ContentCollection>
                                        <dxcp:ContentControl runat="server">
                                            <div id="divAnexos" runat="server">
                                                <iframe id="frmAnexos" frameborder="0" height="100%" scrolling="auto" src="" runat="server"
                                                    width="100%"></iframe>
                                            </div>
                                        </dxcp:ContentControl>
                                    </ContentCollection>
                                </dxcp:TabPage>
                            </TabPages>

                            <ClientSideEvents ActiveTabChanging="function(s, e) {
       var mostraBotaoSalvar = true;
       var mostraBotaoAdicionarComentarios = false;
       if(e.tab.name ==  'tabA')
       {               
                mostraBotaoSalvar = false;                
               document.getElementById('tabControl_frmAnexos').src = s.cpFrameAnexos;                          
       }
       else if (e.tab.name == 'tabChecklist')
       {
                mostraBotaoSalvar = false;
               document.getElementById('frmCheckList').src = s.cpFrameChecklist;
       }
       else if(e.tab.name == 'tabHistorico')
       {
               mostraBotaoSalvar = false;
              document.getElementById('frmHistorico').src = s.cpFrameHistorico;
       }
       else if(e.tab.name == 'tabLinks')
       {
               mostraBotaoSalvar = false;
                   document.getElementById('frmLinks').src = s.cpFrameLinks;
       }
        else if(e.tab.name == 'tabComentarios')
       {
              mostraBotaoSalvar = false;
              mostraBotaoAdicionarComentarios = true;
              
              setFrameSource();
        }

       window.top.SetBotaoAdicionarComentariosVisivel(mostraBotaoAdicionarComentarios);
       window.top.SetBotaoSalvarVisivel(mostraBotaoSalvar);
}"
                                Init="function(s, e) {
	var altura = Math.max(0, document.documentElement.clientHeight) - 325;
                                txtDetalheItem.SetHeight(altura < 125 ? altura + 130 : altura);
       window.top.SetBotaoSalvarVisivel(true);
                                window.top.SetBotaoAdicionarComentariosVisivel(false);
}"
                                ActiveTabChanged="function(s, e) {
	var altura = Math.max(0, document.documentElement.clientHeight) - 325;
                txtDetalheItem.SetHeight(altura &lt; 125 ? altura + 130 : altura);
                memoComentarioCusto.SetHeight(altura + 100);
                memoComentarioReceita.SetHeight(altura + 100);
}"></ClientSideEvents>

                            <ContentStyle>
                                <Paddings Padding="0px"></Paddings>
                            </ContentStyle>
                        </dxcp:ASPxPageControl>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table>
                            <tr>
                                <td align="left">
                                    <dxtv:ASPxLabel ID="lblDescricaoSprint" runat="server" ClientInstanceName="lblDescricaoSprint" Font-Italic="True">
                                    </dxtv:ASPxLabel>

                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" style="display: none">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE" Width="100px" ID="btnSalvar" EnableClientSideAPI="True">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
                 var acao = s.cpAcao;
                 var msgValidacao = ValidaCamposFormulario();
                  if(msgValidacao == '')
                  {
                          window.top.dadosAlteradosNoPopup = false;                                  
                          callbackTela.PerformCallback(acao);
                  }
                  else
                  {
                         window.top.mostraMensagem(msgValidacao , 'atencao', true, false, null);
                  }
}"></ClientSideEvents>
                                                    </dxcp:ASPxButton>

                                                </td>
                                                <td>
                                                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100px" ID="btnFechar">
                                                        <ClientSideEvents Click="function(s, e) {
e.processOnServer = false;
callbackBotaoFechar.PerformCallback();
}"></ClientSideEvents>
                                                    </dxcp:ASPxButton>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>
        <dxtv:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
            <ClientSideEvents EndCallback="function(s, e){
         if(s.cpSucesso != '')
        {
                    var codigo = s.cpCodigo;
                    document.getElementById('retorno_popup').value = codigo;
                    if(window.top.pcModal2.GetVisible() == true &amp;&amp; window.top.pcModal.GetVisible() == true)
                    {
                       window.top.fechaModal2();
                    }
                    else
                    {
                          if(window.top.pcModal.GetVisible() == true)
                          {
                                 window.top.fechaModal();
                          }
                    }
                    funcaoCallbackFechar();
                    window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null);
        }
        else
       {
                 if(s.cpErro != '')
                 {
                             window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
                 }
       }
}"></ClientSideEvents>
        </dxtv:ASPxCallback>
        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>
        <dxwtl:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server" TreeListID="tlDados" OnRenderBrick="ASPxTreeListExporter1_RenderBrick">
            <Settings ExpandAllNodes="True" ExportAllPages="True">
                <PageSettings PaperKind="A4">
                </PageSettings>
            </Settings>
            <Styles>
                <Header Font-Bold="True" Font-Names="roboto">
                </Header>
                <Cell Font-Names="roboto">
                </Cell>
            </Styles>
        </dxwtl:ASPxTreeListExporter>
        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfAreaTransferencia" ID="hfAreaTransferencia"></dxcp:ASPxHiddenField>
        <dxtv:ASPxCallback ID="callbackBotaoFechar" runat="server" ClientInstanceName="callbackBotaoFechar" OnCallback="callbackBotaoFechar_Callback">
            <ClientSideEvents EndCallback="function(s, e) 
{
                var codigo = s.cpCodigo;                 
	e.processOnServer = false;
                if(window.top.pcModal2.GetVisible() == true &amp;&amp; window.top.pcModalComFooter.GetVisible() == true)
                {
                           document.getElementById('retorno_popup').value = codigo;
                           window.top.fechaModal2();
                 }
                 else
                 {
                           if(window.top.pcModalComFooter.GetVisible() == true)
                          {
                                     if (window.top.dadosAlteradosNoPopup == true) 
                                     {
                                                  window.top.mostraConfirmacao('Descartar as alterações?', function () { window.top.dadosAlteradosNoPopup = false; window.top.fechaModalComFooter(); }, null);
                                      }
                                     else 
                                    {
                                                  window.top.fechaModalComFooter();
                                     }
                             }
                    }
}"></ClientSideEvents>
        </dxtv:ASPxCallback>
    </form>
    <script type="text/javascript">
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 65;
        document.getElementById("tabControl_divPrincipalPopup").style.height = sHeight + "px";
        document.getElementById("tabControl_divPrincipalPopup").style.overflow = "auto";

        document.getElementById("tabControl_frmAnexos").style.height = sHeight + "px";
        document.getElementById("tabControl_divCheckList").style.height = sHeight + "px";
        document.getElementById("tabControl_divFinanceiro").style.height = sHeight + "px";
        document.getElementById("tabControl_divFinanceiro").style.overflow = "auto";
        document.getElementById("tabControl_divComentarios").style.height = sHeight + "px";
        document.getElementById("tabControl_divHistorico").style.height = sHeight + "px";
        document.getElementById("tabControl_divLinks").style.height = sHeight + "px";


        document.querySelector('#btnCopiar').addEventListener('click', function (e) {

            const urlParams = new URLSearchParams(window.location.search);
            const codigoItem = urlParams.get('CI');

            var conteudoTexto = document.getElementById('tabControl_txtTituloItem_I').value;
            var objetoBotaoCopiar = document.getElementById('btnCopiar');
            objetoBotaoCopiar.setAttribute('data-clipboard-text', codigoItem + ' - ' + conteudoTexto);
        });

        var objetoCopia = new ClipboardJS('#btnCopiar');
        objetoCopia.on('success', function (e) {
            //console.info('Action:', e.action);
            //console.info('Text:', 'e.text quiser aser um perr ksdjfksd hkfjsh');
            //console.info('Trigger:', e.trigger);

            window.top.mostraMensagem('Copiado!', 'sucesso', false, false, null, 1000);
            e.clearSelection();
        });

        objetoCopia.on('error', function (e) {
            //console.error('Action:', e.action);
            //console.error('Trigger:', e.trigger);
        });



    </script>
</body>
</html>
