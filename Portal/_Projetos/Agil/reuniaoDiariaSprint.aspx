<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reuniaoDiariaSprint.aspx.cs" Inherits="_Projetos_Agil_reuniaoDiariaSprint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">
        function validaCamposFormulario() {
            var retorno = "";
            var countMsg = 1;

            if (txtHoraInicioReal.GetValue() == null || txtHoraInicioReal.GetText() == "") {
                retorno += countMsg++ + ") O horário de início da reunião deve ser informado. \n";
            }
            if (txtHoraTerminoReal.GetValue() == null || txtHoraTerminoReal.GetText() == "") {
                retorno += countMsg++ + ") O horário de término da reunião deve ser informado. \n";
            }

            if (txtHoraInicioReal.GetValue() == "00:00" || txtHoraInicioReal.GetText() == "00:00") {
                retorno += countMsg++ + ") O horário de início da reunião deve ser informado. \n";
            }
            if (txtHoraTerminoReal.GetValue() == "00:00" || txtHoraTerminoReal.GetText() == "00:00") {
                retorno += countMsg++ + ") O horário de término da reunião deve ser informado. \n";
            }

            if (memoLocal.GetValue() == null || memoLocal.GetText() == "") {
                retorno += countMsg++ + ") O local da reunião deve ser informado. \n";
            }
            if (txtHoraTerminoReal.GetValue() == txtHoraInicioReal.GetValue()) {
                retorno += countMsg++ + ") " + traducao.reuniaoDiariaSprint_o_hor_rio_de_in_cio_e_t_rmino_n_o_devem_ser_iguais_ + "\n";
            }
            return retorno;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" style="width:100%">

        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" style="width:100%">
                        <tbody>
                            <tr>
                                <td>

                                    <table cellpadding="0" cellspacing="0" class="dxhe-dialogControlsWrapper">
                                        <tr>
                                            <td style="width: 225px">
                                                <dxcp:ASPxLabel ID="ASPxLabel5" runat="server" Text="Selecione a data de término da reunião:">
                                                </dxcp:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxcp:ASPxDateEdit ID="dateEditDataReuniao" runat="server" ClientInstanceName="dateEditDataReuniao" OnCalendarDayCellPrepared="ASPxDateEdit1_CalendarDayCellPrepared" AllowNull="False">
                                                    <CalendarProperties ShowClearButton="False">
                                                    </CalendarProperties>
                                                    <ClientSideEvents ValueChanged="function(s, e) {
lpLoading.Show();
callbackAtualizaTela.PerformCallback(s.GetText());
}" DropDown="function(s, e) {
         tabControl1.SetActiveTabIndex(0);
}" Init="function(s, e) {
         lpLoading.Show();
         callbackAtualizaTela.PerformCallback(s.GetText());
}" />
                                                </dxcp:ASPxDateEdit>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxcp:ASPxPageControl ID="tabControl1" runat="server" ActiveTabIndex="0" Width="100%" ClientInstanceName="tabControl1">
                                        <TabPages>
                                            <dxtv:TabPage Name="tabAta" Text="Ata" ToolTip="Ata" >
                                                <ContentCollection>
                                                    <dxtv:ContentControl runat="server">
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" style="width: 100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="padding-top: 0px;" valign="bottom">
                                                                                    <asp:Label runat="server" CssClass="campo-label" Text="Local:" ID="lblLocal"></asp:Label>
                                                                                </td>
                                                                                <td style="padding-top: 0px" valign="bottom">
                                                                                    <asp:Label runat="server" CssClass="campo-label" Text="In&#237;cio:" ID="lblInicio"></asp:Label>
                                                                                </td>
                                                                                <td style="padding-top: 0px" valign="bottom">
                                                                                    <asp:Label runat="server" CssClass="campo-label" Text="T&#233;rmino:" ID="lblTermino"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 5px;">
                                                                                    <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="50"
                                                                                        ClientInstanceName="memoLocal"
                                                                                        ID="memoLocal">
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                                                                            ValidationGroup="MKE">
                                                                                            <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxcp:ASPxTextBox>
                                                                                </td>
                                                                                <td style="padding-right: 5px; width: 70px">
                                                                                    <dxcp:ASPxTextBox runat="server" Width="70px"
                                                                                        ClientInstanceName="txtHoraInicioReal"
                                                                                        ID="txtHoraInicioReal">
                                                                                        <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxcp:ASPxTextBox>
                                                                                </td>
                                                                                <td style="width: 70px">
                                                                                    <dxcp:ASPxTextBox runat="server" Width="70px"
                                                                                        ClientInstanceName="txtHoraTerminoReal"
                                                                                        ID="txtHoraTerminoReal">
                                                                                        <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxcp:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAta" runat="server" CssClass="campo-label" Text="Ata:"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxhe:ASPxHtmlEditor ID="memoAta" runat="server" ClientInstanceName="memoAta" EnableTheming="True" Width="100%">
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
                                                                                    <dxhe:ToolbarInsertImageDialogButton Visible="False">
                                                                                    </dxhe:ToolbarInsertImageDialogButton>
                                                                                    <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False">
                                                                                    </dxhe:ToolbarCheckSpellingButton>
                                                                                    <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
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
                                                                                    <dxhe:ToolbarFullscreenButton>
                                                                                    </dxhe:ToolbarFullscreenButton>
                                                                                </Items>
                                                                            </dxhe:HtmlEditorToolbar>
                                                                            <dxhe:HtmlEditorToolbar>
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
                                                                                    <dxhe:ToolbarJustifyFullButton>
                                                                                    </dxhe:ToolbarJustifyFullButton>
                                                                                    <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                                                    </dxhe:ToolbarBackColorButton>
                                                                                    <dxhe:ToolbarFontColorButton>
                                                                                    </dxhe:ToolbarFontColorButton>
                                                                                </Items>
                                                                            </dxhe:HtmlEditorToolbar>
                                                                        </Toolbars>
                                                                        <Settings AllowHtmlView="False" AllowPreview="False">
                                                                        </Settings>
                                                                        <SettingsHtmlEditing>
                                                                            <PasteFiltering Attributes="class" />
                                                                        </SettingsHtmlEditing>
                                                                        <StylesToolbars>
                                                                            <ToolbarItem>
                                                                                <Paddings Padding="1px" />
                                                                            </ToolbarItem>
                                                                        </StylesToolbars>
                                                                    </dxhe:ASPxHtmlEditor>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dxtv:ContentControl>
                                                </ContentCollection>
                                            </dxtv:TabPage>
                                            <dxtv:TabPage Name="tabParticipantes" Text="Participantes" ToolTip="Participantes">
                                                <ContentCollection>
                                                    <dxtv:ContentControl runat="server">
                                                        <dxtv:ASPxGridView ID="gvParticipantesReuniao" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvParticipantesReuniao" KeyFieldName="CodigoUsuario" OnCustomCallback="gvParticipantesReuniao_CustomCallback" Width="100%">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	//var checado = s.cp_checado;
	//document.getElementById('checkTodosClientesAssociados').checked = checado;	
}"
                                                                Init="function(s, e) {
    //var numLinhasSelecionadas = s.GetSelectedRowCount();
    //var linhasPagina = s.GetVisibleRowsOnPage();
    //if(numLinhasSelecionadas == linhasPagina)
//	{
//		document.getElementById('checkTodosClientesAssociados').checked = true;
//	}
//	else{
//		document.getElementById('checkTodosClientesAssociados').checked = false;
//	}
                s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 157);
}"
                                                                SelectionChanged="function(s, e) {
    //var numLinhasSelecionadas = s.GetSelectedRowCount();
    //var linhasPagina = s.GetVisibleRowsOnPage();
    //if(numLinhasSelecionadas == linhasPagina)
   //	{
   //		document.getElementById('checkTodosClientesAssociados').checked = true;
   //	}
  //	else{
  //		document.getElementById('checkTodosClientesAssociados').checked = false;
//	}
}" />
                                                            <Templates>
                                                                <TitlePanel>
                                                                    <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Participantes da Reunião de Sprint">
                                                                    </dxtv:ASPxLabel>
                                                                </TitlePanel>
                                                            </Templates>
                                                            <SettingsPager Mode="ShowAllRecords" PageSize="100">
                                                            </SettingsPager>
                                                            <Settings HorizontalScrollBarMode="Auto" ShowFilterRow="True" VerticalScrollableHeight="250" VerticalScrollBarMode="Visible" />
                                                            <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AllowSelectByRowClick="True" AllowSort="False" ConfirmDelete="True" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                                                            <SettingsText CommandClearFilter="Limpar filtro" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn Caption="todos" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="50px">
<%--                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <input onclick="gvParticipantesReuniao.PerformCallback(this.checked.toString());" id="checkTodosClientesAssociados" title="Marcar/Desmarcar Todos" type="checkbox" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>--%>
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="CodigoUsuario" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Participante" FieldName="NomeUsuario" ShowInCustomizationForm="True" VisibleIndex="3" Width="100%">
                                                                    <Settings AllowSort="False" />
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Email" FieldName="EMail" ShowInCustomizationForm="True" VisibleIndex="5" Width="300px">
                                                                </dxtv:GridViewDataTextColumn>
                                                            </Columns>
                                                            <Paddings Padding="0px" />
                                                        </dxtv:ASPxGridView>
                                                    </dxtv:ContentControl>
                                                </ContentCollection>
                                            </dxtv:TabPage>
                                        </TabPages>
                                        <ClientSideEvents Init="function(s, e) {
	memoAta.SetHeight(Math.max(0, document.documentElement.clientHeight) - 245);
}" ActiveTabChanging="function(s, e) {
	if(e.tab.name ==  'tabParticipantes')
       {
            e.cancel = (hfGeral.Get('CodigoEvento') ==  undefined);	
       }
}" ActiveTabChanged="function(s, e) {
	if(e.tab.name ==  'tabParticipantes')
       {
            gvParticipantesReuniao.PerformCallback(hfGeral.Get('CodigoEvento'));
       }
}"/>
                                        <Paddings Padding="0px" />
                                    </dxcp:ASPxPageControl>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table cellpadding="0" cellspacing="0" style="padding-top: 5px;">
                        <tr>
                            <td style="width: 110px; padding-right: 5px">
                                <dxcp:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                    Text="Salvar" Width="100%"
                                    AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
//debugger   
var erros = validaCamposFormulario();   
   if( erros != &quot;&quot;)
    {
    	window.top.mostraMensagem(erros, 'atencao', true, false, null);
                e.processOnServer = false;
	}	
	else
	{
    		lpLoading.Show();		
                                callbackGeral.PerformCallback();
	}
}" />
                                </dxcp:ASPxButton>
                            </td>
                            <td style="width: 110px">
                                <dxcp:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"
                                    Text="Fechar" Width="100%">
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                </dxcp:ASPxButton>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
        <dxlp:ASPxLoadingPanel ID="lpLoading" runat="server"
            ClientInstanceName="lpLoading"
            Height="73px" HorizontalAlign="Center" Text="Carregando&amp;hellip;"
            VerticalAlign="Middle" Width="180px">
        </dxlp:ASPxLoadingPanel>
        <dxcb:ASPxCallback ID="callbackGeral" runat="server" ClientInstanceName="callbackGeral"
            OnCallback="callbackGeral_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
      lpLoading.Hide();
      if(s.cp_mensagemErro != null &amp;&amp; s.cp_mensagemErro != &quot;&quot;)
      {
                 window.top.mostraMensagem(s.cp_mensagemErro, 'atencao', true, false, null);	
      }
       else
      {
                window.top.mostraMensagem(s.cp_mensagemSucesso, 'sucesso', false, false, null);
                window.top.fechaModal();
      }
}" />
        </dxcb:ASPxCallback>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
        <dxcb:ASPxCallback ID="callbackAtualizaTela" runat="server" ClientInstanceName="callbackAtualizaTela"
            OnCallback="callbackAtualizaTela_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
 memoLocal.SetText(s.cp_memoLocal);
 txtHoraInicioReal.SetText(s.cp_txtHoraInicioReal);
 txtHoraTerminoReal.SetText(s.cp_txtHoraTerminoReal);
 memoAta.SetHtml(s.cp_memoAta);
hfGeral.Set('CodigoEvento', s.cp_CodigoEvento);
lpLoading.Hide();
}" />
        </dxcb:ASPxCallback>
        <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

    </form>
</body>
</html>
