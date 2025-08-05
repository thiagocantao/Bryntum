<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndicadoresEstrategicosFrm.aspx.cs" Inherits="_Estrategias_wizard_IndicadoresEstrategicosFrm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 105px;
        }

        .auto-style2 {
            width: 120px;
        }

        .auto-style3 {
            width: 155px;
        }

        .auto-style4 {
            width: 39px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">

        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="padding-bottom: 10px">
                    <dxcp:ASPxPageControl ID="TabControl" runat="server" ActiveTabIndex="0" Width="100%" ClientInstanceName="TabControl">
                        <TabPages>
                            <dxtv:TabPage Name="TabPageDetalhes" Text="Detalhes">
                                <ContentCollection>
                                    <dxtv:ContentControl runat="server">
                                        <div  runat="server" id="divTab1">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="padding-bottom: 10px">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 156px">
                                                                <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" ClientInstanceName="lblIndicador" Text="Código Reservado:" ToolTip="Código utilizado para interface com outros sistemas da instituição.">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td style="width: 400px">
                                                                <dxtv:ASPxLabel ID="lblIndicador" runat="server" ClientInstanceName="lblIndicador" Text="Indicador: *">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td></td>
                                                            <td>
                                                                <dxtv:ASPxLabel ID="lblAgrupamentoDaMeta" runat="server" ClientInstanceName="lblAgrupamentoDaMeta" Text="Agrupamento da Meta: *">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 156px">
                                                                <dxtv:ASPxTextBox ID="txtCodigoReservado" runat="server" ClientInstanceName="txtCodigoReservado" MaxLength="256" Width="95%" ToolTip="Código utilizado para interface com outros sistemas da instituição.">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxTextBox>
                                                            </td>
                                                            <td style="width: 400px">
                                                                <dxtv:ASPxTextBox ID="txtIndicador" runat="server" ClientInstanceName="txtIndicador" MaxLength="99" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxTextBox>
                                                            </td>
                                                            <td></td>
                                                            <td>
                                                                <dxtv:ASPxComboBox ID="cmbAgrupamentoDaMeta" runat="server" ClientInstanceName="cmbAgrupamentoDaMeta" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 10px">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td class="auto-style3">
                                                                <dxtv:ASPxLabel ID="lblUnidadeMedida" runat="server" Text="Unidade de Medida: *">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td></td>
                                                            <td style="width: 120px">
                                                                <dxtv:ASPxLabel ID="lblCasasDecimais" runat="server" Text="Casas Decimais:">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td></td>
                                                            <td>
                                                                <dxtv:ASPxLabel ID="lblPolaridade" runat="server" Text="Polaridade:">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td></td>
                                                            <td class="auto-style2">
                                                                <dxtv:ASPxLabel ID="lblPeriodicidade" runat="server" Text="Periodicidade: *">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td></td>
                                                            <td class="auto-style1"></td>
                                                            <td class="auto-style4">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style3">
                                                                <dxtv:ASPxComboBox ID="ddlUnidadeMedida" runat="server" ClientInstanceName="ddlUnidadeMedida" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                            <td></td>
                                                            <td style="width: 120px">
                                                                <dxtv:ASPxComboBox ID="ddlCasasDecimais" runat="server" ClientInstanceName="ddlCasasDecimais" SelectedIndex="0" Width="100%">
                                                                    <Items>
                                                                        <dxtv:ListEditItem Selected="True" Text="0 (Zero)" Value="0" />
                                                                        <dxtv:ListEditItem Text="1 (Um)" Value="1" />
                                                                        <dxtv:ListEditItem Text="2 (Duas)" Value="2" />
                                                                        <dxtv:ListEditItem Text="3 (Três)" Value="3" />
                                                                        <dxtv:ListEditItem Text="4 (Quatro)" Value="4" />
                                                                        <dxtv:ListEditItem Text="5 (Cinco)" Value="5" />
                                                                        <dxtv:ListEditItem Text="6 (Seis)" Value="6" />
                                                                    </Items>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                            <td></td>
                                                            <td>
                                                                <dxtv:ASPxComboBox ID="ddlPolaridade" runat="server" ClientInstanceName="ddlPolaridade" SelectedIndex="0" Width="100%">
                                                                    <Items>
                                                                        <dxtv:ListEditItem Selected="True" Text="Quanto maior o valor, MELHOR" Value="POS" />
                                                                        <dxtv:ListEditItem Text="Quanto maior o valor, PIOR" Value="NEG" />
                                                                    </Items>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                            <td></td>
                                                            <td class="auto-style2">
                                                                <dxtv:ASPxComboBox ID="ddlPeriodicidade" runat="server" ClientInstanceName="ddlPeriodicidade" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                            <td></td>
                                                            <td class="auto-style1">
                                                                <dxtv:ASPxCheckBox ID="cbCheckResultante" runat="server" CheckState="Unchecked" ClientInstanceName="cbCheckResultante" Text="Resultante?">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxCheckBox>
                                                            </td>
                                                            <td class="auto-style4">
                                                                <dxtv:ASPxButton ID="btnFaixaDeTolerancia" runat="server" ClientInstanceName="btnFaixaDeTolerancia" Width="100%">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcFaixaDeTolerancia.Show();
}" />
                                                                    <Image Height="18px" ToolTip="Editar Faixa de Tolerância" Url="~/imagens/botoes/FaixaDeTolerancia.png">
                                                                    </Image>
                                                                    <Paddings Padding="0px" />
                                                                </dxtv:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 10px">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxLabel ID="lblFonte" runat="server" Text="Fonte:">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td style="width: 10px;"></td>
                                                            <td style="width: 160px;">
                                                                <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" Text="Critério:">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxTextBox ID="txtFonte" runat="server" ClientInstanceName="txtFonte" MaxLength="59" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxTextBox>
                                                            </td>
                                                            <td></td>
                                                            <td style="width: 160px">
                                                                <dxtv:ASPxRadioButtonList ID="rbCriterio" runat="server" ClientInstanceName="rbCriterio" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%">
                                                                    <Paddings Padding="0px" />
                                                                    <Items>
                                                                        <dxtv:ListEditItem Selected="True" Text="Status" Value="S" />
                                                                        <dxtv:ListEditItem Text="Acumulado" Value="A" />
                                                                    </Items>
                                                                    <DisabledStyle ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxRadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 10px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxLabel ID="lblUnidade" runat="server" ClientInstanceName="lblUnidade" Text="Unidade de Negócio: *">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td style="width: 210px">
                                                                <dxtv:ASPxLabel ID="lblResponsavelIndicador" runat="server" Text="Responsável pelo Indicador: *">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td style="width: 244px">
                                                                <dxtv:ASPxLabel ID="lblResponsavelIndicador0" runat="server" Text="Responsável Atualização do Resultado: *">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxComboBox ID="ddlUnidadeNegocio" runat="server" ClientInstanceName="ddlUnidadeNegocio" TextFormatString="{1}" Width="100%">
                                                                    <Columns>
                                                                        <dxtv:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio" Width="140px" />
                                                                        <dxtv:ListBoxColumn Caption="Nome" FieldName="NomeUnidadeNegocio" Width="300px" />
                                                                    </Columns>
                                                                    <SettingsLoadingPanel Text="Carregando;" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                            <td style="padding-right: 10px; width: 210px;">
                                                                <dxtv:ASPxComboBox ID="ddlResponsavelIndicador" runat="server" ClientInstanceName="ddlResponsavelIndicador" EnableCallbackMode="True" OnItemRequestedByValue="ddlResponsavelIndicador_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavelIndicador_ItemsRequestedByFilterCondition" ValueType="System.Int32" Width="100%">
                                                                    <ClientSideEvents Init="function(s, e) {
	if (s.cp_Codigo != &quot;&quot;)
{
            		s.SetValue(s.cp_Codigo);
		s.SetText(s.cp_Descricao);
	}
}" />
                                                                    <Columns>
                                                                        <dxtv:ListBoxColumn Caption="Nome" Width="300px" />
                                                                        <dxtv:ListBoxColumn Caption="Email" Width="200px" />
                                                                    </Columns>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                            <td style="width: 244px">
                                                                <dxtv:ASPxComboBox ID="ddlResponsavelResultado" runat="server" ClientInstanceName="ddlResponsavelResultado" EnableCallbackMode="True" OnItemRequestedByValue="ddlResponsavelIndicador_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavelIndicador_ItemsRequestedByFilterCondition" ValueType="System.Int32" Width="100%">
                                                                    <ClientSideEvents Init="function(s, e) {
	if (s.cp_Codigo != &quot;&quot;)
{
            		s.SetValue(s.cp_Codigo);
		s.SetText(s.cp_Descricao);
	}
}
" />
                                                                    <Columns>
                                                                        <dxtv:ListBoxColumn Caption="Nome" Width="300px" />
                                                                        <dxtv:ListBoxColumn Caption="Email" Width="200px" />
                                                                    </Columns>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 10px">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="padding-right: 10px">
                                                                <dxtv:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Vigência" Height="70px" Width="100%">
                                                                    <ContentPaddings Padding="3px" />
                                                                    <PanelCollection>
                                                                        <dxtv:PanelContent runat="server">
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="width: 125px">
                                                                                        <dxtv:ASPxLabel ID="lblInicioVigencia" runat="server" Text="Início:">
                                                                                        </dxtv:ASPxLabel>
                                                                                    </td>
                                                                                    <td style="width: 125px">
                                                                                        <dxtv:ASPxLabel ID="lblInicioVigencia0" runat="server" Text="Término:">
                                                                                        </dxtv:ASPxLabel>
                                                                                    </td>
                                                                                    <td>&nbsp; </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 125px; padding-right: 10px">
                                                                                        <dxtv:ASPxDateEdit ID="ddlInicioVigencia" runat="server" ClientInstanceName="ddlInicioVigencia" DisplayFormatString="{0:dd/MM/yyyy}" Width="100%">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	verificaVigencia();
}" />
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxtv:ASPxDateEdit>
                                                                                    </td>
                                                                                    <td style="width: 125px; padding-right: 10px">
                                                                                        <dxtv:ASPxDateEdit ID="ddlTerminoVigencia" runat="server" ClientInstanceName="ddlTerminoVigencia" DisplayFormatString="{0:dd/MM/yyyy}" Width="100%">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	verificaVigencia();
}" />
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxtv:ASPxDateEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxtv:ASPxCheckBox ID="cbVigencia" runat="server" CheckState="Unchecked" ClientInstanceName="cbVigencia" Text="Acompanhar as metas do indicador somente no período">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxtv:ASPxCheckBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </dxtv:PanelContent>
                                                                    </PanelCollection>
                                                                </dxtv:ASPxRoundPanel>
                                                            </td>
                                                            <td>
                                                                <dxtv:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="O Indicador deverá ser atualizado em quantos dias após o fechamento do período?" Height="70px" Width="100%">
                                                                    <ContentPaddings Padding="3px" />
                                                                    <PanelCollection>
                                                                        <dxtv:PanelContent runat="server">
                                                                            <dxtv:ASPxTextBox ID="txtLimite" runat="server" ClientInstanceName="txtLimite" HorizontalAlign="Right" Width="80px">
                                                                                <MaskSettings Mask="&lt;1..9999&gt;" PromptChar=" " />
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxTextBox>
                                                                        </dxtv:PanelContent>
                                                                    </PanelCollection>
                                                                </dxtv:ASPxRoundPanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblGlossario" runat="server" Text="Glossário:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxhe:ASPxHtmlEditor ID="heGlossario" runat="server" ClientInstanceName="heGlossario" Height="203px" Width="100%">
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
                                                        <StylesToolbars>
                                                            <BarDockControl Wrap="True">
                                                            </BarDockControl>
                                                            <Toolbar>
                                                                <SeparatorPaddings Padding="0px" PaddingLeft="1px" />
                                                            </Toolbar>
                                                            <ToolbarItem>
                                                                <Paddings PaddingLeft="1px" PaddingRight="1px" />
                                                            </ToolbarItem>
                                                        </StylesToolbars>
                                                    </dxhe:ASPxHtmlEditor>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" ClientInstanceName="lblAjuda" Font-Bold="False" Font-Italic="False" ForeColor="#404040">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                            </div>
                                    </dxtv:ContentControl>
                                </ContentCollection>
                            </dxtv:TabPage>
                            <dxtv:TabPage Text="Componentes da Fórmula" Name="TabDado">
                                <ContentCollection>
                                    <dxtv:ContentControl runat="server">
                                        <div runat="server" id="divTab2">
                                         <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <dxtv:ASPxGridView ID="gridDadosIndicador" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridDadosIndicador" EnableRowsCache="False" EnableViewState="False" KeyFieldName="CodigoDado" OnCellEditorInitialize="gridDadosIndicador_CellEditorInitialize" OnRowDeleting="gridDadosIndicador_RowDeleting" OnRowInserting="gridDadosIndicador_RowInserting" OnRowUpdating="gridDadosIndicador_RowUpdating" Width="100%">

                                                            <Templates>
                                                                <FooterRow>
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <dxtv:ASPxImage ID="ASPxImage1" runat="server" Height="16px" ImageUrl="~/imagens/dado.png">
                                                                                    </dxtv:ASPxImage>
                                                                                </td>
                                                                                <td align="left" style="padding-left: 3px; padding-right: 10px;">
                                                                                    <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Dado">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <dxtv:ASPxImage ID="ASPxImage2" runat="server" Height="16px" ImageUrl="~/imagens/indicadoresMenor.png">
                                                                                    </dxtv:ASPxImage>
                                                                                </td>
                                                                                <td align="left" style="padding-left: 3px; padding-right: 10px">
                                                                                    <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Indicador">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </FooterRow>
                                                            </Templates>
                                                            <SettingsPager Mode="ShowAllRecords" PageSize="4" Visible="False">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm">
                                                            </SettingsEditing>
                                                            <Settings ShowFooter="True" ShowGroupButtons="False" VerticalScrollableHeight="400" VerticalScrollBarMode="Visible" />
                                                            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                                            <SettingsPopup>
                                                                <EditForm AllowResize="True" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="400px" />
                                                            </SettingsPopup>
                                                            <SettingsText ConfirmDelete="Confirmar exclusão?" PopupEditFormCaption="Componentes" />
                                                            <StylesPopup>
                                                                <EditForm>
                                                                    <MainArea HorizontalAlign="Left">
                                                                    </MainArea>
                                                                </EditForm>
                                                            </StylesPopup>
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="Ações" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <HeaderTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <dxtv:ASPxMenu ID="menu1" runat="server" BackColor="Transparent" ClientInstanceName="menu1" ItemSpacing="5px" OnInit="menu_Init1" OnItemClick="menu_ItemClick">
                                                                                        <Paddings Padding="0px" />
                                                                                        <Items>
                                                                                            <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                                </Image>
                                                                                            </dxtv:MenuItem>
                                                                                            <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                                <Items>
                                                                                                    <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                    <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                    <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                    <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                                                        <Image Url="~/imagens/menuExportacao/html.png">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                </Items>
                                                                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                                </Image>
                                                                                            </dxtv:MenuItem>
                                                                                            <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                                                                <Items>
                                                                                                    <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                                        <Image IconID="save_save_16x16">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                    <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                                        <Image IconID="actions_reset_16x16">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                </Items>
                                                                                                <Image Url="~/imagens/botoes/layout.png">
                                                                                                </Image>
                                                                                            </dxtv:MenuItem>
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
                                                                                    </dxtv:ASPxMenu>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataTextColumn Caption=" #" FieldName="Sequencia" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="30px">
                                                                    <EditFormSettings Visible="False" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Tipo" FieldName="CodigoDado" ShowInCustomizationForm="True" VisibleIndex="2" Width="50px">
                                                                    <EditFormSettings Visible="False" />
                                                                    <DataItemTemplate>
                                                                        <%# (int.Parse(Eval("CodigoDado").ToString()) > 0) ? "<img src='../../imagens/dado.png' />" : "<img src='../../imagens/indicadoresMenor.png' />" %>
                                                                    </DataItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="A (Componente)" FieldName="DescricaoDado" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                    <EditFormSettings Visible="False" />
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataComboBoxColumn Caption="A (Componente)" FieldName="CodigoDado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3" Width="280px">
                                                                    <PropertiesComboBox ImageUrlField="UrlTipoComponente" TextField="DescricaoDado" ValueField="CodigoDado" ValueType="System.Int32" Width="280px">
                                                                        <ClientSideEvents Init="function(s, e) {
	hfGeral.Set(&quot;NovoCodigoDado&quot;, s.GetValue());
}"
                                                                            SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;NovoCodigoDado&quot;, s.GetValue());
}"
                                                                            Validation="function(s, e) {	
    if(s.GetValue() == null)
	{
		e.isValid = false;
		e.errorText = 'Campo Obrigatório!';
	}
}" />
                                                                        <ValidationSettings CausesValidation="True" Display="Dynamic" ValidationGroup="MKE">
                                                                            <RequiredField IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings CaptionLocation="Top" Visible="True" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </dxtv:GridViewDataComboBoxColumn>
                                                                <dxtv:GridViewDataComboBoxColumn Caption="B (Função)" FieldName="CodigoFuncaoDadoIndicador" ShowInCustomizationForm="True" VisibleIndex="5" Width="120px">
                                                                    <PropertiesComboBox DataSourceID="dsFuncao" TextField="NomeFuncao" ValueField="CodigoFuncao" ValueType="System.Int32">
                                                                        <ClientSideEvents Init="function(s, e) {
	hfGeral.Set(&quot;NovoCodigoFuncao&quot;, s.GetValue());
}"
                                                                            SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;NovoCodigoFuncao&quot;, s.GetValue());
}" />
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxtv:GridViewDataComboBoxColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="C (Valor)" FieldName="valorDado" ShowInCustomizationForm="True" VisibleIndex="6" Width="90px">
                                                                    <EditFormSettings Visible="False" />
                                                                    <DataItemTemplate>
                                                                        <dxtv:ASPxTextBox ID="txtValorDado" runat="server" ClientInstanceName="txtValorDado" HorizontalAlign="Right" Width="60px">
                                                                        </dxtv:ASPxTextBox>
                                                                    </DataItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="CodigoIndicador" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="DescricaoDado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="NomeFuncao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                                </dxtv:GridViewDataTextColumn>
                                                            </Columns>
                                                            <Styles>
                                                                <Table BackColor="White">
                                                                </Table>
                                                                <EmptyDataRow BackColor="White">
                                                                </EmptyDataRow>
                                                                <TitlePanel BackColor="White">
                                                                </TitlePanel>
                                                            </Styles>
                                                        </dxtv:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td id="tdDetalhes" style="width: 5px;">&nbsp; </td>
                                                                    <td style="padding-top: 20px;">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <dxtv:ASPxLabel ID="lblFormula" runat="server" ClientInstanceName="lblFormula" Text="Fórmula ">
                                                                                        </dxtv:ASPxLabel>
                                                                                        <dxtv:ASPxLabel ID="lblExemplo2" runat="server" ClientInstanceName="lblExemplo2" Font-Italic="True" ForeColor="Teal" Text="  exemplo (0,2 * C1 + C2) :">
                                                                                        </dxtv:ASPxLabel>
                                                                                    </td>
                                                                                    <td style="width: 42px"></td>
                                                                                    <td style="width: 80px"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxtv:ASPxTextBox ID="txtFormulaIndicador" runat="server" ClientInstanceName="txtFormulaIndicador" MaxLength="999" Width="100%">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxtv:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center" style="padding-left: 3px; padding-right: 3px;">
                                                                                        <dxtv:ASPxButton ID="btnEqual" runat="server" Text="=" Width="100%">
                                                                                            <ClientSideEvents Click="function(s, e) {
	                executaFormula();
	                e.processOnServer = false;
                }" />
                                                                                            <Paddings Padding="0px" />
                                                                                        </dxtv:ASPxButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxtv:ASPxTextBox ID="txtResultadoFormula" runat="server" ClientInstanceName="txtResultadoFormula" HorizontalAlign="Center" Width="100%">
                                                                                        </dxtv:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
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
                                                    <td align="left">
                                                        <dxtv:ASPxPopupControl ID="popupNovoDado" runat="server" ClientInstanceName="popupNovoDado" HeaderText="Novo Dado" Height="124px" Modal="True" PopupElementID="popupDado" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="600px">
                                                            <ClientSideEvents Shown="function(s, e) {
        txtNome.SetText(&quot;&quot;);
    }" />
                                                            <ContentStyle>
                                                                <Paddings Padding="5px" />
                                                            </ContentStyle>
                                                            <HeaderStyle Font-Bold="True" />
                                                            <ContentCollection>
                                                                <dxtv:PopupControlContentControl runat="server">
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxLabel ID="lblNome" runat="server" ClientInstanceName="lblNome" CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" Text="Nome do Dado:">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxTextBox ID="txtNome" runat="server" ClientInstanceName="txtNome" MaxLength="100" Width="100%">
                                                                                        <ValidationSettings CausesValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="PPR">
                                                                                            <ErrorImage Height="14px">
                                                                                            </ErrorImage>
                                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxtv:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table>
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxtv:ASPxButton ID="btnSalvarNovoDado" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarPP" CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass" Text="Salvar" ValidationGroup="PPR" Width="90px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
                    if(txtNome.GetValue() != &quot;&quot;) 
                    {	
                        insereNovoDado();
                        popupNovoDado.Hide();
                     }
                }" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dxtv:ASPxButton>
                                                                                                </td>
                                                                                                <td></td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxButton ID="btnCancelaNovoDado" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelaPP" CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass" Text="Cancelar" Width="90px">
                                                                                                        <ClientSideEvents Click="function(s, e) { popupNovoDado.Hide(); }" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dxtv:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxtv:PopupControlContentControl>
                                                            </ContentCollection>
                                                        </dxtv:ASPxPopupControl>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                         </div>
                                    </dxtv:ContentControl>
                                </ContentCollection>
                            </dxtv:TabPage>
                        </TabPages>
                        <ClientSideEvents ActiveTabChanging="function(s, e) {
	var tab = TabControl.GetActiveTab();

	if(TipoOperacao == 'Incluir')
	{
	    if(e.tab.name=='TabDado')
	    {
	        TabControl.SetActiveTab(TabControl.tabs[0]);
       	     }
   	 }	
}
" />
                        <ContentStyle>
                            <Paddings Padding="4px" />
                        </ContentStyle>
                    </dxcp:ASPxPageControl>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE" Width="90px" ID="btnSalvar" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {	
	var valido = true;
	
	if(hfGeral.Get(&#39;TipoOperacao&#39;).toString() == &#39;Incluir&#39;)
		valido = true;
	else
	{
	if(parseFloat(txtD1.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) &gt; parseFloat(txtA1.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)))
	{
		txtA1.SetIsValid(false);
		txtA1.Validate();
		valido = false;
	}

	if(parseFloat(txtD2.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) &gt; parseFloat(txtA2.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)))
	{
		txtA2.SetIsValid(false);
		txtA2.Validate();
		valido = false;
	}

	if(parseFloat(txtD3.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) &gt; parseFloat(txtA3.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)))
	{
		txtA3.SetIsValid(false);
		txtA3.Validate();
		valido = false;
	}

	if((ddlC4.GetValue() != &#39;S&#39;) &amp;&amp; (parseFloat(txtD4.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) &gt; parseFloat(txtA4.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;))))
	{
		txtA4.SetIsValid(false);
		txtA4.Validate();
		valido = false;
	}
	}
	
    if(valido == true)
	{   
        var msgForm = validaCamposFormulario();
        if (msgForm != '')
        {
            window.top.mostraMensagem(msgForm, 'atencao', true, false, null);            
        }else
        {
		    pnCallback2.PerformCallback();
        }
	}
	else
	{
		window.top.mostraMensagem(&#39;Faixa de Toler&#226;ncia Inv&#225;lida&#39;, &#39;atencao&#39;, true, false, null);	
	}
	e.processOnServer = false;
}"></ClientSideEvents>
                                        <Paddings Padding="0px"></Paddings>
                                    </dxcp:ASPxButton>
                                </td>
                                <td align="right"></td>
                                <td align="right">
                                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    window.top.fechaModal2();
}"></ClientSideEvents>
                                        <Paddings Padding="0px"></Paddings>
                                    </dxcp:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsFuncao" runat="server" ConnectionString="" SelectCommand="SELECT CodigoFuncao, NomeFuncao FROM TipoFuncaoDado UNION SELECT 0 AS Expr1, 'STATUS' AS Expr2 ORDER BY NomeFuncao"></asp:SqlDataSource>

        <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ExportSelectedRowsOnly="false" ID="ASPxGridViewExporter1">
            <Styles>
                <Default></Default>

                <Header></Header>

                <Cell></Cell>

                <GroupFooter Font-Bold="True"></GroupFooter>

                <Title Font-Bold="True"></Title>
            </Styles>
        </dxcp:ASPxGridViewExporter>

        <dxcp:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents ControlsInitialized="function(s, e) {	
	desabilitaHabilitaComponentes();
}" />
        </dxcp:ASPxGlobalEvents>

        <dxcp:ASPxCallback ID="callbackCalc" runat="server" ClientInstanceName="callbackCalc" OnCallback="callbackCalc_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	txtResultadoFormula.SetText(replaceAll(s.cp_Valor, '.', ','));
}" />
        </dxcp:ASPxCallback>

        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>

        <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True" ClientInstanceName="pcFaixaDeTolerancia" HeaderText="Faixa de Toler&#226;ncia" ShowCloseButton="False" ID="pcFaixaDeTolerancia">
            <ClientSideEvents Shown="function(s, e) {
	if (hfGeral.Get(&quot;TipoOperacao&quot;).toString() == &#39;Editar&#39;)
    	btnSalvarFaixaDeTolerancia.SetVisible(true);
	else
		btnSalvarFaixaDeTolerancia.SetVisible(false);

	pnCallbackFaixaTolerancia.PerformCallback();
}"></ClientSideEvents>

            <ContentStyle HorizontalAlign="Left"></ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackFaixaTolerancia" Width="100%" ID="pnCallbackFaixaTolerancia" OnCallback="pnCallbackFaixaTolerancia_Callback">
                                        <ClientSideEvents EndCallback="function(s, e) {
	//desabilitaHabilitaComponentes();
	//pcFaixaDeTolerancia.Show();
	if(s.cp_FallaEditada == &quot;OK&quot;)
	{
		mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
		pcFaixaDeTolerancia.Hide();
	}
}"></ClientSideEvents>
                                        <PanelCollection>
                                            <dxcp:PanelContent runat="server">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top" align="left">
                                                                <table cellspacing="0" cellpadding="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxcp:ASPxLabel runat="server" Text="Cor:" ID="lblCor"></dxcp:ASPxLabel>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxLabel runat="server" Text="De:" ID="lblDe"></dxcp:ASPxLabel>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxLabel runat="server" Text="At&#233;:" ID="lblAte"></dxcp:ASPxLabel>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxcp:ASPxComboBox runat="server" SelectedIndex="0" Width="95px" ClientInstanceName="ddlC1" ClientEnabled="False" BackColor="#EBEBEB" ID="ddlC1">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD1, txtA1, null);
	txtD1.SetFocus();
}"
                                                                                        Init="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD1, txtA1, null);
}"></ClientSideEvents>
                                                                                    <Items>
                                                                                        <dxcp:ListEditItem Selected="True" Text="Vermelho" Value="Vermelho"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Amarelo" Value="Amarelo"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Verde" Value="Verde"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Azul" Value="Azul"></dxcp:ListEditItem>
                                                                                    </Items>

                                                                                    <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxComboBox>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtD1" ID="txtD1">
                                                                                    <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtA1" ID="txtA1">
                                                                                    <ClientSideEvents TextChanged="function(s, e) {
	if(ddlC2.GetValue() != &#39;S&#39; &amp;&amp; parseFloat(s.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) != 0)
		txtD2.SetText((parseFloat(s.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) + 0.01).toString().replace(&#39;.&#39;, &#39;,&#39;));
}"
                                                                                        Validation="function(s, e) {
	if(parseFloat(txtD1.GetValue()) &gt; parseFloat(s.GetValue()))
		e.isValid = false;
}"></ClientSideEvents>

                                                                                    <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Valor da Faixa Superior Menor que o Valor da Faixa Inferior"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxcp:ASPxComboBox runat="server" SelectedIndex="1" Width="95px" ClientInstanceName="ddlC2" ClientEnabled="False" BackColor="#EBEBEB" ID="ddlC2">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(ddlC1.GetValue() == &#39;S&#39;)
	{
		s.SetValue(&#39;S&#39;);
		window.top.mostraMensagem(&#39;Escolha a Faixa de Toler&#226;ncia Acima para Continuar!&#39;, &#39;atencao&#39;, true, false, null);	
	}
	else
	{
		verificaCamposFaixaTolerancia(s, txtD2, txtA2, txtA1);
		txtD2.SetFocus();
	}
}"
                                                                                        Init="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD2, txtA2, txtA1);
}"></ClientSideEvents>
                                                                                    <Items>
                                                                                        <dxcp:ListEditItem Text="Vermelho" Value="Vermelho"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Selected="True" Text="Amarelo" Value="Amarelo"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Verde" Value="Verde"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Azul" Value="Azul"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Sem Faixa" Value="S"></dxcp:ListEditItem>
                                                                                    </Items>

                                                                                    <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxComboBox>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtD2" ID="txtD2">
                                                                                    <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtA2" ID="txtA2">
                                                                                    <ClientSideEvents TextChanged="function(s, e) {
	if(ddlC3.GetValue() != &#39;S&#39; &amp;&amp; parseFloat(s.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) != 0)
		txtD3.SetText((parseFloat(s.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) + 0.01).toString().replace(&#39;.&#39;, &#39;,&#39;));
}"
                                                                                        Validation="function(s, e) {
	if(parseFloat(txtD2.GetValue()) &gt; parseFloat(s.GetValue()))
		e.isValid = false;
}"></ClientSideEvents>

                                                                                    <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Valor da Faixa Superior Menor que o Valor da Faixa Inferior" ValidationGroup="MKE"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxcp:ASPxComboBox runat="server" SelectedIndex="2" Width="95px" ClientInstanceName="ddlC3" ClientEnabled="False" BackColor="#EBEBEB" ID="ddlC3">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(ddlC1.GetValue() == &#39;S&#39; || ddlC2.GetValue() == &#39;S&#39;)
	{
		s.SetValue(&#39;S&#39;);
		window.top.mostraMensagem(&#39;Escolha as Faixas de Toler&#226;ncia Acima para Continuar!&#39;, &#39;atencao&#39;, true, false, null);		
	}
	else
	{
		verificaCamposFaixaTolerancia(s, txtD3, txtA3, txtA2);
		txtD1.SetFocus();
	}
}"
                                                                                        Init="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD3, txtA3, txtA2);
}"></ClientSideEvents>
                                                                                    <Items>
                                                                                        <dxcp:ListEditItem Text="Vermelho" Value="Vermelho"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Amarelo" Value="Amarelo"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Selected="True" Text="Verde" Value="Verde"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Azul" Value="Azul"></dxcp:ListEditItem>
                                                                                    </Items>

                                                                                    <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxComboBox>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtD3" ID="txtD3">
                                                                                    <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtA3" ID="txtA3">
                                                                                    <ClientSideEvents TextChanged="function(s, e) {
	if(ddlC4.GetValue() != &quot;S&quot; &amp;&amp; parseFloat(s.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) != 0)
		txtD4.SetText((parseFloat(s.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) + 0.01).toString().replace(&#39;.&#39;, &#39;,&#39;));
}"
                                                                                        Validation="function(s, e) {
	if(parseFloat(txtD3.GetValue()) &gt; parseFloat(s.GetValue()))
		e.isValid = false;
}"></ClientSideEvents>

                                                                                    <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Valor da Faixa Superior Menor que o Valor da Faixa Inferior"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxcp:ASPxComboBox runat="server" SelectedIndex="4" Width="95px" ClientInstanceName="ddlC4" ID="ddlC4">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(ddlC1.GetValue() == &#39;S&#39; || ddlC2.GetValue() == &#39;S&#39; || ddlC3.GetValue() == &#39;S&#39;)
	{
		s.SetValue(&#39;S&#39;);
		window.top.mostraMensagem(&#39;Escolha as Faixas de Toler&#226;ncia Acima para Continuar!&#39;, &#39;atencao&#39;, true, false, null);		
	}
	else
	{
		verificaCamposFaixaTolerancia(s, txtD4, txtA4, txtA3);
		txtD1.SetFocus();
	}
}"
                                                                                        Init="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD4, txtA4, txtA3);
}"></ClientSideEvents>
                                                                                    <Items>
                                                                                        <dxcp:ListEditItem Text="Vermelho" Value="Vermelho"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Amarelo" Value="Amarelo"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Verde" Value="Verde"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Text="Azul" Value="Azul"></dxcp:ListEditItem>
                                                                                        <dxcp:ListEditItem Selected="True" Text="Sem Faixa" Value="S"></dxcp:ListEditItem>
                                                                                    </Items>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxComboBox>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtD4" ID="txtD4">
                                                                                    <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtA4" ID="txtA4">
                                                                                    <ClientSideEvents Validation="function(s, e) {
	if(parseFloat(txtD4.GetValue()) &gt; parseFloat(s.GetValue()))
		e.isValid = false;
}"></ClientSideEvents>

                                                                                    <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Valor da Faixa Superior Menor que o Valor da Faixa Inferior"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 10px" valign="top">
                                                                <dxcp:ASPxLabel runat="server" Text="(*) " Font-Bold="True" Font-Italic="False" ForeColor="#404040" ID="ASPxLabel2"></dxcp:ASPxLabel>

                                                                <dxcp:ASPxLabel runat="server" Text="O desempenho considera o resultado do indicador em rela&#231;&#227;o &#224; sua meta" Font-Bold="False" Font-Italic="False" ForeColor="#404040" ID="ASPxLabel10"></dxcp:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </dxcp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>

                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px" align="right">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnSalvarFaixaDeTolerancia" Text="Salvar" Width="90px" ID="btnSalvarFaixaDeTolerancia">
                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
	var valido = true;

	if(parseFloat(txtD1.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) &gt; parseFloat(txtA1.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)))
	{
		txtA1.SetIsValid(false);
		txtA1.Validate();
		valido = false;
	}
	if(parseFloat(txtD2.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) &gt; parseFloat(txtA2.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)))
	{
		txtA2.SetIsValid(false);
		txtA2.Validate();
		valido = false;
	}
	if(parseFloat(txtD3.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) &gt; parseFloat(txtA3.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)))
	{
		txtA3.SetIsValid(false);
		txtA3.Validate();
		valido = false;
	}
	if((ddlC4.GetValue() != &#39;S&#39;) &amp;&amp; (parseFloat(txtD4.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;)) &gt; parseFloat(txtA4.GetValue().toString().replace(&#39;,&#39;, &#39;.&#39;))))
	{
		txtA4.SetIsValid(false);
		txtA4.Validate();
		valido = false;
	}
	
    if(valido == true)
		pnCallbackFaixaTolerancia.PerformCallback(&#39;Salvar&#39;);
	else
		window.top.mostraMensagem(&#39;Faixa de Toler&#226;ncia Inv&#225;lida&#39;, &#39;atencao&#39;, true, false, null);	
}"></ClientSideEvents>

                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxcp:ASPxButton>

                                                </td>
                                                <td style="padding-left: 10px" align="right">
                                                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="ASPxButton3">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcFaixaDeTolerancia.Hide();
}"></ClientSideEvents>

                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxcp:ASPxButton>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>

        <dxcp:ASPxPopupControl runat="server" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" ClientInstanceName="pcMensagemGravacao" EnableClientSideAPI="True" HeaderText="Mensagem" ShowCloseButton="False" ShowShadow="False" Width="721px" ID="pcMensagemGravacao">
            <HeaderImage Url="~/imagens/alertAmarelho.png"></HeaderImage>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxcp:ASPxLabel runat="server" Text="Aten&#231;&#227;o:" ClientInstanceName="lblAtencao" Font-Bold="True" ID="lblAtencao"></dxcp:ASPxLabel>

                                    &nbsp;<dxcp:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblMensagemError" ID="lblMensagemError"></dxcp:ASPxLabel>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                        </tbody>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>
        <dxcp:ASPxCallbackPanel ID="pnCallback2" runat="server" ClientInstanceName="pnCallback2"
            Width="100%" OnCallback="pnCallback_Callback" HideContentOnCallback="True" ClientVisible="False">
            <PanelCollection>
                <dxp:PanelContent runat="server">
                </dxp:PanelContent>
            </PanelCollection>
            <SettingsLoadingPanel Enabled="False" />
            <ClientSideEvents EndCallback="function(s, e) {                            

    
        var status = s.cp_StatusSalvar;
        if (status != &quot;1&quot;)
        {
            var mensagem = s.cp_ErroSalvar;
            window.top.mostraMensagem(mensagem, 'erro', true, false, null);
        }
        else
        {
                           
	        if(&quot;Incluir&quot; == s.cp_OperacaoOk)
                {
		            mostraDivSalvoPublicado(&quot;Indicador inclu&#237;do com sucesso!&quot;);
                    hfGeral.Set('COIN', s.cp_COIN);
                    hfGeral.Set('TipoOperacao', s.cp_TipoOperacao);
                    hfGeral.Set('modoGridDados', s.cp_modoGridDados);
                    //window.top.fechaModal2();
                }
            else if(&quot;Editar&quot; == s.cp_OperacaoOk){
		        mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
                //window.top.fechaModal2();
                }
	        else if(&quot;EditarCompartilhar&quot; == s.cp_OperacaoOk)
		        mostraDivSalvoPublicado(&quot;Indicador compartilhado com sucesso!&quot;);

            if(s.cp_AtualizaDados == 'S')
                      gridDadosIndicador.PerformCallback();
        }
    
}
"></ClientSideEvents>
        </dxcp:ASPxCallbackPanel>
    </form>
</body>
</html>
