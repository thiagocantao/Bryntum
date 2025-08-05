<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemasMapa.aspx.cs" Inherits="_Estrategias_wizard_menuMapa_TemasMapa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <%--<style type="text/css">
        .auto-style1 {
            height: 13px;
        }
    </style>--%>
    <link href="../../../estilos/custom.css" rel="stylesheet" />
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div id="ConteudoPrincipalFrame">
            <!-- TABLE CONTEUDO -->
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="padding-right: 10px; padding-left: 10px; padding-top: 10px;">
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%" OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <div id="divGrid" style="visibility: hidden">
                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoObjetoEstrategia;CodigoMapaEstrategico;CodigoVersaoMapaEstrategico"
                                            AutoGenerateColumns="False" Width="100%"
                                            ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                            OnCustomErrorText="gvDados_CustomErrorText">
                                            <ClientSideEvents CustomButtonClick="function(s, e) {
	e.processOnServer = false;

    if(e.buttonID == 'btnEditarCustom')
    {
        TipoOperacao = 'Editar';
        desabilitaHabilitaComponentes();
		onClickBarraNavegacao('Editar', gvDados, pcDados);
		hfGeral.Set('TipoOperacao', 'Editar');
    }
    else if(e.buttonID == 'btnExcluirCustom')   onClickBarraNavegacao('Excluir', gvDados, pcDados);
    else if(e.buttonID == 'btnPermissaoCustom')	onGridFocusedRowChangedPopup(gvDados);
}"  Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="110px" VisibleIndex="0">
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnPermissaoCustom" Text="Permiss&#245;es">
                                                            <Image Url="~/imagens/Perfis/Perfil_Permissoes.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                    <HeaderTemplate>
                                                        <table>
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
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoEstrategia" Name="CodigoObjetoEstrategia"
                                                    Caption="CodigoObjetoEstrategia" Visible="False" VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoMapaEstrategico" Name="CodigoMapaEstrategico"
                                                    Caption="CodigoMapaEstrategico" Visible="False" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoVersaoMapaEstrategico" Name="CodigoVersaoMapaEstrategico"
                                                    Caption="CodigoVersaoMapaEstrategico" Visible="False" VisibleIndex="3">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TituloMapaEstrategico" GroupIndex="0" SortIndex="0"
                                                    SortOrder="Ascending" Name="TituloMapaEstrategico" Width="300px" Caption="Mapa"
                                                    VisibleIndex="4">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="GlossarioObjeto" Name="GlossarioObjeto"
                                                    Caption="GlossarioObjeto" Visible="False" VisibleIndex="6">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavelObjeto" Name="CodigoResponsavelObjeto"
                                                    Caption="CodigoResponsavelObjeto" Visible="False" VisibleIndex="7">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoEstrategiaSuperior" Name="CodigoObjetoEstrategiaSuperior"
                                                    Caption="CodigoObjetoEstrategiaSuperior" Visible="False" VisibleIndex="8">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TituloMapaEstrategico" Name="TituloMapaEstrategico"
                                                    Caption="TituloMapaEstrategico" Visible="False" VisibleIndex="9">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Responsável" Name="ResponsavelObjeto" ShowInCustomizationForm="True"
                                                    VisibleIndex="12" FieldName="ResponsavelObjeto">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Título do Tema" FieldName="TituloObjetoEstrategia"
                                                    ShowInCustomizationForm="True" VisibleIndex="10">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Descrição do Tema" FieldName="DescricaoObjetoEstrategia"
                                                    ShowInCustomizationForm="True" VisibleIndex="11">
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible"></Settings>
                                            <SettingsText></SettingsText>
                                        </dxwgv:ASPxGridView>
                                    </div>
                                    <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                        CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="800px"
                                        ID="pcDados" PopupVerticalOffset="-20" AllowResize="True" Height="300px">
                                        <ClientSideEvents Closing="function(s, e) {
	LimpaCamposFormulario();
}"
                                            CloseUp="function(s, e) {	
	ddlResponsavel.SetValue(null);
	ddlResponsavel.SetText(&quot;&quot;);	
	ddlResponsavel.PerformCallback();
}"></ClientSideEvents>
                                        <ContentStyle>
                                            <Paddings Padding="5px"></Paddings>
                                        </ContentStyle>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%">
                                                                <TabPages>
                                                                    <dxtv:TabPage Name="tabDetalhes" Text="Detalhes">
                                                                        <ContentCollection>
                                                                            <dxtv:ContentControl runat="server">
                                                                                <div id="divDetalhes" runat="server" style="height: 250px">
                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxtv:ASPxLabel ID="lblMapaEstrategico0" runat="server" ClientInstanceName="lblMapaEstrategico" Text="Mapa Estratégico: *">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxtv:ASPxComboBox ID="ddlMapaEstrategico" runat="server" ClientInstanceName="ddlMapaEstrategico" Width="100%">
                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
    //ddlPerspectiva.PerformCallback(s.GetValue());
       callbackPerspectiva.PerformCallback(s.GetValue());
}" />
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxtv:ASPxLabel ID="lblPerspectiva0" runat="server" ClientInstanceName="lblPerspectiva" Text="Perspectiva: *">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                    <dxtv:ASPxCallbackPanel ID="callbackPerspectiva0" runat="server" ClientInstanceName="callbackPerspectiva" OnCallback="callbackPerspectiva_Callback" Width="100%">
                                                                                                        <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" />
                                                                                                        <PanelCollection>
                                                                                                            <dxtv:PanelContent runat="server">
                                                                                                                <dxtv:ASPxComboBox ID="ddlPerspectiva" runat="server" ClientInstanceName="ddlPerspectiva" OnCallback="ddlPerspectiva_Callback" ShowImageInEditBox="True" Width="100%">
                                                                                                                    <ClientSideEvents EndCallback="function(s, e) {
    if(s.cp_ValueEdicao !=&quot;&quot;)
        ddlPerspectiva.SetValue(s.cp_ValueEdicao);
}"
                                                                                                                        ValueChanged="function(s, e) {
    hfGeral.Set(&quot;idPerspectiva&quot;, s.GetValue());
}" />
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxtv:ASPxComboBox>
                                                                                                            </dxtv:PanelContent>
                                                                                                        </PanelCollection>
                                                                                                    </dxtv:ASPxCallbackPanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <!-- FECHA PESPECTIVA -->
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <dxtv:ASPxLabel ID="lblTema0" runat="server" ClientInstanceName="lblTema" Text="Título Tema: *">
                                                                                                                </dxtv:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblTema" Text="Descrição Tema: *">
                                                                                                                </dxtv:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxtv:ASPxLabel ID="lblResponsavel0" runat="server" ClientInstanceName="lblResponsavel" Text="Responsável:">
                                                                                                                </dxtv:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <dxtv:ASPxTextBox ID="txtTituloTema" runat="server" ClientInstanceName="txtTituloTema" MaxLength="100" Width="100%">
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxtv:ASPxTextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxtv:ASPxTextBox ID="txtDescricaoTema" runat="server" ClientInstanceName="txtDescricaoTema" MaxLength="500" Width="100%">
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxtv:ASPxTextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxtv:ASPxComboBox ID="ddlResponsavel" runat="server"
                                                                                                                    ClientInstanceName="ddlResponsavel"
                                                                                                                    ValueType="System.Int32"
                                                                                                                    EnableCallbackMode="True"
                                                                                                                    OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                                                                                                    OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                                                                                                    TextFormatString="{0}" Width="100%"
                                                                                                                    CallbackPageSize="80" DropDownRows="10" DropDownHeight="150px" IncrementalFilteringMode="Contains">
                                                                                                                    <Columns>
                                                                                                                        <dxtv:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="300px" />
                                                                                                                        <dxtv:ListBoxColumn Caption="Email" FieldName="EMail" Width="200px" />
                                                                                                                    </Columns>
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                    <SettingsLoadingPanel Text="Carregando;"></SettingsLoadingPanel>
                                                                                                                </dxtv:ASPxComboBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </dxtv:ContentControl>
                                                                        </ContentCollection>
                                                                    </dxtv:TabPage>
                                                                    <dxtv:TabPage Name="tabGlossario" Text="Glossário">
                                                                        <ContentCollection>
                                                                            <dxtv:ContentControl runat="server">
                                                                                <div id="divGlossario" runat="server">
                                                                                    <dxhe:ASPxHtmlEditor ID="heGlossario" runat="server" ClientInstanceName="heGlossario" EnableTheming="True" Width="100%" Height="250px">
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
                                                                                            <PasteFiltering Attributes="class"></PasteFiltering>
                                                                                        </SettingsHtmlEditing>
                                                                                    </dxhe:ASPxHtmlEditor>
                                                                                </div>
                                                                            </dxtv:ContentControl>
                                                                        </ContentCollection>
                                                                    </dxtv:TabPage>
                                                                </TabPages>
                                                                <Paddings Padding="0px" />
                                                                <BorderTop BorderWidth="0px" />
                                                            </dxtv:ASPxPageControl>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxtv:ASPxButton ID="btnSalvar0" runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px">
                                                                                <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if (window.onClick_btnSalvar)
        onClick_btnSalvar();
}" />
                                                                                <Paddings Padding="0px" />
                                                                            </dxtv:ASPxButton>
                                                                        </td>
                                                                        <td style="padding-left: 10px">
                                                                            <dxtv:ASPxButton ID="btnCancelar0" runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="90px">
                                                                                <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                                <Paddings Padding="0px" />
                                                                            </dxtv:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                    </dxhf:ASPxHiddenField>
                                    <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
                                    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                                        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();

	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.TemasMapa_tema_inclu_ido_com_sucesso_);
	else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.TemasMapa_tema_alterado_com_sucesso_);
	else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.TemasMapa_tema_exclu_do_com_sucesso_);

}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
