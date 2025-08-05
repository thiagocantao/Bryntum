<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="IndicadoresOperacional.aspx.cs" Inherits="_Projetos_Administracao_IndicadoresOperacional" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <!--check -->
    <style>
        
        html td.dxe {
            padding: 0px !important;
        }
    </style>
    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
        Width="100%" OnCallback="pnCallback_Callback">
        <PanelCollection>
            <dxp:PanelContent runat="server">
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoIndicador"
                    AutoGenerateColumns="False" Width="100%"
                    ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnBeforeColumnSortingGrouping="gvDados_BeforeColumnSortingGrouping"
                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                    <ClientSideEvents
                        CustomButtonClick="function(s, e) 
{
	onClick_CustomButtomGrid(s, e);
}
"></ClientSideEvents>
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="130px" VisibleIndex="0">
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
                                <table>
                                    <tr>
                                        <td align="center">
                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                ClientInstanceName="menu"
                                                ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                OnInit="menu_Init">
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
                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                ClientVisible="False">
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
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Name="CodigoIndicador"
                            Visible="False" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Caption="Nome Indicador"
                            VisibleIndex="2">
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeMedida" Visible="False"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="GlossarioIndicador" Visible="False"
                            VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Polaridade" Visible="False"
                            VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="FormulaIndicador" Visible="False"
                            VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavel" Visible="False"
                            VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo de Indicador" FieldName="DescTipoIndicador"
                            GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending"
                            VisibleIndex="9">
                            <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo Indicador" FieldName="TipoIndicador"
                            ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoFuncaoAgrupamentoMeta" Name="CodigoFuncaoAgrupamentoMeta"
                            Visible="False" VisibleIndex="11">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="IndicaCriterio" Name="IndicaCriterio" Caption="IndicaCriterio"
                            Visible="False" VisibleIndex="12">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="PermitirAlteracaoCampos" FieldName="PermitirAlteracaoCampos"
                            Name="PermitirAlteracaoCampos" ShowInCustomizationForm="True" Visible="False"
                            VisibleIndex="13">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ResponsavelObjeto" FieldName="ResponsavelObjeto"
                            Name="ResponsavelObjeto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" AllowDragDrop="False"></SettingsBehavior>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True" ShowFilterRow="True"
                        ShowHeaderFilterBlankItems="False"></Settings>
                    <SettingsText></SettingsText>
                    <Paddings Padding="10px" />
                </dxwgv:ASPxGridView>
            </dxp:PanelContent>
        </PanelCollection>
        <ClientSideEvents EndCallback="function(s, e) {
	local_onEnd_pnCallback(s,e);
}"></ClientSideEvents>
    </dxcp:ASPxCallbackPanel>

    <asp:SqlDataSource runat="server" ID="dsResponsavel"></asp:SqlDataSource>
    <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>


    <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ExportSelectedRowsOnly="false" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        <Styles>
            <GroupFooter Font-Bold="True"></GroupFooter>

            <Title Font-Bold="True"></Title>
        </Styles>
    </dxcp:ASPxGridViewExporter>


    <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True" ClientInstanceName="pcOperMsg" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False" Width="270px" ID="pcOperMsg">
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="" align="center"></td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxcp:ASPxImage>


                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxcp:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxcp:ASPxLabel>


                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>


    <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" ID="pcDados" PopupAlignCorrection="Disabled" PopupVerticalOffset="50">
        <ClientSideEvents Closing="function(s, e) {
	LimpaCamposFormulario();
	TabControl.SetActiveTab(TabControl.GetTabByName(&#39;TabDetalhe&#39;));
}"
            CloseUp="function(s, e) {
	//ddlResponsavelIndicador.SetValue(null);
	//ddlResponsavelIndicador.SetText(&quot;&quot;);	
	//ddlResponsavelIndicador.PerformCallback();
}"
            Shown="function(s, e) {
	var tab = TabControl.GetTab(0);
	TabControl.SetActiveTab(tab);


}"
            Init="function(s, e) {
	TabControl.ActiveTabIndex = 0;
	
}"></ClientSideEvents>

        <ContentStyle>
            <Paddings Padding="5px"></Paddings>
        </ContentStyle>

        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxcp:ASPxPageControl runat="server" ShowTabs="False" ActiveTabIndex="0" ClientInstanceName="TabControl" Width="800px" ID="TabControl">
                                    <TabPages>
                                        <dxcp:TabPage Name="TabDetalhe" Text="Indicador">
                                            <ContentCollection>
                                                <dxcp:ContentControl runat="server">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <fieldset style="padding: 0px 0px 0px 5px">
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblTipoIndicador">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 115px">
                                                                                        <dxcp:ASPxLabel runat="server" Text="Tipo de Indicador:" ClientInstanceName="lblTipoIndicador" ID="lblTipoIndicador"></dxcp:ASPxLabel>

                                                                                    </td>
                                                                                    <td style="width: 5px"></td>
                                                                                    <td valign="top">
                                                                                        <dxcp:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" ClientInstanceName="rblTipoIndicador" Width="275px" ID="rblTipoIndicador">
                                                                                            <Items>
                                                                                                <dxcp:ListEditItem Text="Desempenho" Value="D"></dxcp:ListEditItem>
                                                                                                <dxcp:ListEditItem Text="Operacional" Value="O"></dxcp:ListEditItem>
                                                                                            </Items>

                                                                                            <Border BorderStyle="None"></Border>

                                                                                            <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                                                        </dxcp:ASPxRadioButtonList>

                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </fieldset>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 400px; padding-right: 10px;">
                                                                                    <dxcp:ASPxLabel runat="server" Text="Indicador:" ClientInstanceName="lblIndicador" ID="lblIndicador"></dxcp:ASPxLabel>

                                                                                </td>
                                                                                <td>
                                                                                    <dxcp:ASPxLabel runat="server" Text=" Agrupamento da Meta:" ClientInstanceName="lblAgrupamentoDaMeta" ID="lblAgrupamentoDaMeta"></dxcp:ASPxLabel>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 5px;">
                                                                                    <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="99" ClientInstanceName="txtIndicador" ID="txtIndicador">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxcp:ASPxTextBox>

                                                                                </td>
                                                                                <td>
                                                                                    <dxcp:ASPxComboBox runat="server" ValueType="System.Int32" Width="100%" ClientInstanceName="cmbAgrupamentoDaMeta" ID="cmbAgrupamentoDaMeta">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxcp:ASPxComboBox>

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
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 290px; padding-right: 10px;">
                                                                                    <dxcp:ASPxLabel runat="server" Text="Unidade de Medida:" ID="lblUnidadeMedida"></dxcp:ASPxLabel>

                                                                                </td>
                                                                                <td style="width: 100px; padding-right: 10px;">
                                                                                    <dxcp:ASPxLabel runat="server" Text="Casas Decimais:" ID="lblCasasDecimais"></dxcp:ASPxLabel>

                                                                                </td>
                                                                                <td>
                                                                                    <dxcp:ASPxLabel runat="server" Text="Polaridade:" ID="lblPolaridade"></dxcp:ASPxLabel>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 5px;">
                                                                                    <dxcp:ASPxComboBox runat="server" ValueType="System.Int32" Width="100%" ClientInstanceName="ddlUnidadeMedida" ID="ddlUnidadeMedida">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxcp:ASPxComboBox>

                                                                                </td>
                                                                                <td style="padding-right: 5px;">
                                                                                    <dxcp:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.Int32" Width="100%" ClientInstanceName="ddlCasasDecimais" ID="ddlCasasDecimais">
                                                                                        <Items>
                                                                                            <dxcp:ListEditItem Selected="True" Text="0 (Zero)" Value="0"></dxcp:ListEditItem>
                                                                                            <dxcp:ListEditItem Text="1 (Um)" Value="1"></dxcp:ListEditItem>
                                                                                            <dxcp:ListEditItem Text="2 (Duas)" Value="2"></dxcp:ListEditItem>
                                                                                        </Items>

                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxcp:ASPxComboBox>

                                                                                </td>
                                                                                <td style="padding-right: 5px;">
                                                                                    <dxcp:ASPxComboBox runat="server" SelectedIndex="0" Width="100%" ClientInstanceName="ddlPolaridade" ID="ddlPolaridade">
                                                                                        <Items>
                                                                                            <dxcp:ListEditItem Selected="True" Text="Quanto maior o valor, MELHOR" Value="POS"></dxcp:ListEditItem>
                                                                                            <dxcp:ListEditItem Text="Quanto maior o valor, PIOR" Value="NEG"></dxcp:ListEditItem>
                                                                                        </Items>

                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxcp:ASPxComboBox>

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
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxcp:ASPxLabel runat="server" Text="Respons&#225;vel pelo Indicador:" ID="lblResponsavelIndicador"></dxcp:ASPxLabel>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-bottom: 10px;">
                                                                                    <dxcp:ASPxComboBox runat="server" DropDownStyle="DropDown" ValueType="System.Int32" NullValueItemDisplayText="{0}" EnableCallbackMode="True" TextFormatString="{0}" Width="100%" ClientInstanceName="ddlResponsavelIndicador" ID="ddlResponsavelIndicador" OnItemRequestedByValue="ddlResponsavelIndicador_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavelIndicador_ItemsRequestedByFilterCondition">
                                                                                        <Columns>
                                                                                            <dxcp:ListBoxColumn FieldName="NomeUsuario" Width="200px" Caption="Nome"></dxcp:ListBoxColumn>
                                                                                            <dxcp:ListBoxColumn FieldName="EMail" Width="180px" Caption="Email"></dxcp:ListBoxColumn>
                                                                                        </Columns>

                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxcp:ASPxComboBox>

                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td id="tdDadoCriterioIndicador">
                                                                    <fieldset style="padding: 0px 0px 0px 5px;">
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxcp:ASPxLabel runat="server" Text="Crit&#233;rio:" ClientInstanceName="lblCriterioIndicador" ID="lblCriterioIndicador"></dxcp:ASPxLabel>

                                                                                    </td>
                                                                                    <td style="width: 5px"></td>
                                                                                    <td valign="top">
                                                                                        <dxcp:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" ClientInstanceName="rblCriterio" ID="rblCriterio">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	var value = rblCriterio.GetValue();
	if(value == &quot;A&quot;)
	{
		cmbAgrupamentoDaMeta.SetEnabled(true);
	}
	else
	{
		cmbAgrupamentoDaMeta.SetEnabled(false);
		cmbAgrupamentoDaMeta.SetSelectedIndex(-1);
	}
}"></ClientSideEvents>
                                                                                            <Items>
                                                                                                <dxcp:ListEditItem Text="Acumulado" Value="A"></dxcp:ListEditItem>
                                                                                                <dxcp:ListEditItem Text="Status" Value="S"></dxcp:ListEditItem>
                                                                                            </Items>

                                                                                            <Border BorderStyle="None"></Border>

                                                                                            <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                                                        </dxcp:ASPxRadioButtonList>

                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </fieldset>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxcp:ASPxLabel runat="server" Text="Gloss&#225;rio:" ID="lblGlossario"></dxcp:ASPxLabel>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="heGlossario" Width="100%" Height="133px" ID="heGlossario">
                                                                        <Toolbars>
                                                                            <dxhe:HtmlEditorToolbar>
                                                                                <Items>
                                                                                    <dxhe:ToolbarCutButton></dxhe:ToolbarCutButton>
                                                                                    <dxhe:ToolbarCopyButton></dxhe:ToolbarCopyButton>
                                                                                    <dxhe:ToolbarPasteButton></dxhe:ToolbarPasteButton>
                                                                                    <dxhe:ToolbarPasteFromWordButton></dxhe:ToolbarPasteFromWordButton>
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
                                                                                    <dxhe:ToolbarInsertImageDialogButton></dxhe:ToolbarInsertImageDialogButton>
                                                                                    <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False"></dxhe:ToolbarCheckSpellingButton>
                                                                                    <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                                        <Items>
                                                                                            <dxhe:ToolbarInsertTableDialogButton Text="Insert Table..." ToolTip="Insert Table..." BeginGroup="True"></dxhe:ToolbarInsertTableDialogButton>
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
                                                                                    <dxhe:ToolbarFullscreenButton></dxhe:ToolbarFullscreenButton>
                                                                                </Items>
                                                                            </dxhe:HtmlEditorToolbar>
                                                                            <dxhe:HtmlEditorToolbar>
                                                                                <Items>
                                                                                    <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                                        <Items>
                                                                                            <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                                            <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                                            <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                                            <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                                            <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                                            <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                                            <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                                            <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                                            <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                                        </Items>
                                                                                    </dxhe:ToolbarParagraphFormattingEdit>
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
                                                                                    <dxhe:ToolbarJustifyFullButton></dxhe:ToolbarJustifyFullButton>
                                                                                    <dxhe:ToolbarInsertOrderedListButton BeginGroup="True"></dxhe:ToolbarInsertOrderedListButton>
                                                                                    <dxhe:ToolbarInsertUnorderedListButton></dxhe:ToolbarInsertUnorderedListButton>
                                                                                    <dxhe:ToolbarBackColorButton BeginGroup="True"></dxhe:ToolbarBackColorButton>
                                                                                    <dxhe:ToolbarFontColorButton></dxhe:ToolbarFontColorButton>
                                                                                </Items>
                                                                            </dxhe:HtmlEditorToolbar>
                                                                        </Toolbars>

                                                                        <Settings AllowHtmlView="False" AllowPreview="False"></Settings>

                                                                        <SettingsHtmlEditing>
                                                                            <PasteFiltering Attributes="class"></PasteFiltering>
                                                                        </SettingsHtmlEditing>

                                                                        <StylesToolbars>
                                                                            <BarDockControl Wrap="True"></BarDockControl>

                                                                            <Toolbar>
                                                                                <SeparatorPaddings Padding="0px" PaddingLeft="1px"></SeparatorPaddings>
                                                                            </Toolbar>

                                                                            <ToolbarItem>
                                                                                <Paddings PaddingLeft="1px" PaddingRight="1px"></Paddings>
                                                                            </ToolbarItem>
                                                                        </StylesToolbars>
                                                                    </dxhe:ASPxHtmlEditor>

                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxcp:ContentControl>
                                            </ContentCollection>
                                        </dxcp:TabPage>
                                    </TabPages>

                                    <ClientSideEvents ActiveTabChanged="function(s, e) {
	var tab = TabControl.GetActiveTab();
	var nomeIndicador = txtIndicador.GetText();

	if(TipoOperacao == &#39;Incluir&#39;)
	{
	    if(e.tab.name == &#39;TabDado&#39;)
	    {
	        TabControl.SetActiveTab(TabControl.tabs[0]);
        }
    }
	else
	{
	    if(e.tab.name==&#39;TabDado&#39;)
	    {
	        lblCaptionIndicador.SetText(nomeIndicador);
        }
    }

}"
                                        Init="function(s, e) {

}"></ClientSideEvents>

                                    <ContentStyle>
                                        <Paddings Padding="3px" PaddingLeft="3px" PaddingRight="3px"></Paddings>
                                    </ContentStyle>
                                </dxcp:ASPxPageControl>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table cellspacing="0" cellpadding="4" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 90px">
                                                <dxcp:ASPxButton runat="server" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100%" ID="btnSalvar">
                                                    <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                </dxcp:ASPxButton>

                                            </td>
                                            <td align="right"></td>
                                            <td style="width: 90px" align="right">
                                                <dxcp:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%" ID="btnFechar">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
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

</asp:Content>
