<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="IndicadoresOperacionalCompleto.aspx.cs" Inherits="_Projetos_Administracao_IndicadoresOperacional" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); width: 100%">
        <tr style="height: 26px">
            <td style="height: 26px" valign="middle">&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                    Font-Overline="False"
                    Font-Strikeout="False" Text="Indicadores de Projeto"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 10px; height: 10px;"></td>
            <td style="height: 10px"></td>
            <td style="width: 10px; height: 10px;"></td>
        </tr>
        <tr>
            <td style="width: 10px;"></td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoIndicador"
                                                AutoGenerateColumns="False" Width="100%"
                                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnBeforeColumnSortingGrouping="gvDados_BeforeColumnSortingGrouping"
                                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                                                <ClientSideEvents FocusedRowChanged="function(s, e) {
     OnGridFocusedRowChanged(s);
}"
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
                                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                                                            <Image IconID="save_save_16x16">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout"
                                                                                            Name="btnRestaurarLayout">
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
                                                        Visible="False" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Caption="Nome Indicador"
                                                        VisibleIndex="1">
                                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeMedida" Visible="False" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="GlossarioIndicador" Visible="False" VisibleIndex="3">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False" VisibleIndex="4">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Polaridade" Visible="False" VisibleIndex="5">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="FormulaIndicador" Visible="False" VisibleIndex="6">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavel" Visible="False"
                                                        VisibleIndex="7">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Tipo de Indicador" FieldName="DescTipoIndicador"
                                                        GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending"
                                                        VisibleIndex="8">
                                                        <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AllowDragDrop="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Tipo Indicador" FieldName="TipoIndicador"
                                                        ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoFuncaoAgrupamentoMeta" Name="CodigoFuncaoAgrupamentoMeta"
                                                        Visible="False" VisibleIndex="15">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaCriterio" Name="IndicaCriterio" Caption="IndicaCriterio"
                                                        Visible="False" VisibleIndex="16">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="PermitirAlteracaoCampos" FieldName="PermitirAlteracaoCampos"
                                                        Name="PermitirAlteracaoCampos" ShowInCustomizationForm="True" Visible="False"
                                                        VisibleIndex="17">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="ResponsavelObjeto" FieldName="ResponsavelObjeto"
                                                        Name="ResponsavelObjeto" ShowInCustomizationForm="True" Visible="False"
                                                        VisibleIndex="18">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeResponsavel"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaMetaDesdobravel"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoGrupoIndicador"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeAvaliado"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaApuracaoViaResultado"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" AllowDragDrop="False"></SettingsBehavior>
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True" ShowFilterRow="True"
                                                    ShowHeaderFilterBlankItems="False"></Settings>
                                                <SettingsText></SettingsText>
                                            </dxwgv:ASPxGridView>
                                            <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackMensagem" Width="640px"
                                                ID="pnCallbackMensagem" OnCallback="pnCallbackMensagem_Callback">
                                                <ClientSideEvents EndCallback="function(s, e) {
	local_onEnd_pnCallback(s,e);
}"></ClientSideEvents>
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                        </dxhf:ASPxHiddenField>
                                                        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                                            GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
                                                        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcOperMsg" HeaderText="Incluir a Entidad Atual"
                                                            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                                            ShowCloseButton="False" ShowHeader="False" Width="270px"
                                                            ID="pcOperMsg">
                                                            <ContentCollection>
                                                                <dxpc:PopupControlContentControl runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="" align="center"></td>
                                                                                <td style="width: 70px" align="center" rowspan="3">
                                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                                                        ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                                                        ID="lblAcaoGravacao">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxpc:PopupControlContentControl>
                                                            </ContentCollection>
                                                        </dxpc:ASPxPopupControl>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxcp:ASPxCallbackPanel>
                                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                                ID="pcDados">
                                                <ClientSideEvents Closing="function(s, e) {
	LimpaCamposFormulario();
	TabControl.SetActiveTab(TabControl.GetTabByName('TabDetalhe'));
}"
                                                    Shown="function(s, e) {
	var tab = TabControl.GetTab(0);
	TabControl.SetActiveTab(tab);


}"
                                                    Init="function(s, e) {
	TabControl.ActiveTabIndex = 0;
	
}"
                                                    CloseUp="function(s, e) {
	ddlResponsavelIndicador.SetValue(null);
	ddlResponsavelIndicador.SetText(&quot;&quot;);	
	ddlResponsavelIndicador.PerformCallback();
}"></ClientSideEvents>
                                                <ContentStyle>
                                                    <Paddings Padding="5px"></Paddings>
                                                </ContentStyle>
                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                <ContentCollection>
                                                    <dxpc:PopupControlContentControl runat="server">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="TabControl"
                                                                            Width="880px" ID="TabControl"
                                                                            Height="445px">
                                                                            <TabPages>
                                                                                <dxtc:TabPage Name="TabDetalhe" Text="Indicador">
                                                                                    <ContentCollection>
                                                                                        <dxw:ContentControl runat="server">
                                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <dxe:ASPxLabel runat="server" Text="Indicador:" ClientInstanceName="lblIndicador"
                                                                                                                                ID="lblIndicador">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 220px;">
                                                                                                                            <dxe:ASPxLabel ID="lblResponsavelIndicador" runat="server"
                                                                                                                                Text="Responsável pelo Indicador:">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="99" ClientInstanceName="txtIndicador"
                                                                                                                                ID="txtIndicador">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxe:ASPxTextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 220px;">
                                                                                                                            <dxe:ASPxComboBox ID="ddlResponsavelIndicador" runat="server"
                                                                                                                                ClientInstanceName="ddlResponsavelIndicador" DropDownStyle="DropDown"
                                                                                                                                EnableCallbackMode="True"
                                                                                                                                IncrementalFilteringMode="Contains"
                                                                                                                                OnItemRequestedByValue="ddlResponsavelIndicador_ItemRequestedByValue"
                                                                                                                                OnItemsRequestedByFilterCondition="ddlResponsavelIndicador_ItemsRequestedByFilterCondition"
                                                                                                                                TextFormatString="{0}" ValueType="System.Int32" Width="100%">
                                                                                                                                <Columns>
                                                                                                                                    <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                                                                                                                                    <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="180px" />
                                                                                                                                </Columns>
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxe:ASPxComboBox>
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
                                                                                                                        <td style="width: 180px; padding-right: 10px;">
                                                                                                                            <dxe:ASPxLabel runat="server" Text=" Agrupamento da Meta:"
                                                                                                                                ID="lblAgrupamentoDaMeta" ClientInstanceName="lblAgrupamentoDaMeta">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 149px; padding-right: 10px;">
                                                                                                                            <dxe:ASPxLabel ID="lblUnidadeMedida" runat="server"
                                                                                                                                Text="Unidade de Medida:">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 110px; padding-right: 10px;">
                                                                                                                            <dxe:ASPxLabel runat="server" Text="Casas Decimais:"
                                                                                                                                ID="lblCasasDecimais">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <dxe:ASPxLabel runat="server" Text="Polaridade:"
                                                                                                                                ID="lblPolaridade">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="padding-right: 10px; width: 180px;">
                                                                                                                            <dxe:ASPxComboBox ID="cmbAgrupamentoDaMeta" runat="server"
                                                                                                                                ClientInstanceName="cmbAgrupamentoDaMeta"
                                                                                                                                ValueType="System.Int32" Width="100%">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxe:ASPxComboBox>
                                                                                                                        </td>
                                                                                                                        <td style="padding-right: 10px; width: 149px;">
                                                                                                                            <dxe:ASPxComboBox ID="ddlUnidadeMedida" runat="server"
                                                                                                                                ClientInstanceName="ddlUnidadeMedida"
                                                                                                                                ValueType="System.Int32" Width="100%">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxe:ASPxComboBox>
                                                                                                                        </td>
                                                                                                                        <td style="padding-right: 10px; width: 110px;">
                                                                                                                            <dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.Int32" Width="100%"
                                                                                                                                ClientInstanceName="ddlCasasDecimais" ID="ddlCasasDecimais">
                                                                                                                                <Items>
                                                                                                                                    <dxe:ListEditItem Text="0 (Zero)" Value="0" Selected="True"></dxe:ListEditItem>
                                                                                                                                    <dxe:ListEditItem Text="1 (Um)" Value="1"></dxe:ListEditItem>
                                                                                                                                    <dxe:ListEditItem Text="2 (Duas)" Value="2"></dxe:ListEditItem>
                                                                                                                                </Items>
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxe:ASPxComboBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.String" Width="100%"
                                                                                                                                ClientInstanceName="ddlPolaridade" ID="ddlPolaridade">
                                                                                                                                <Items>
                                                                                                                                    <dxe:ListEditItem Text="Quanto maior o valor, MELHOR" Value="POS" Selected="True"></dxe:ListEditItem>
                                                                                                                                    <dxe:ListEditItem Text="Quanto maior o valor, PIOR" Value="NEG"></dxe:ListEditItem>
                                                                                                                                </Items>
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxe:ASPxComboBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 10px">&nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td style="width: 280px">
                                                                                                                        <dxe:ASPxLabel ID="lbl00" runat="server"
                                                                                                                            Text="Grupo:">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="width: 150px">
                                                                                                                        <dxe:ASPxLabel ID="lbl1" runat="server"
                                                                                                                            Text="Unidade Responsável:">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxLabel ID="lbl2" runat="server"
                                                                                                                            Text="Avaliado:">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="padding-right: 10px; width: 280px;">
                                                                                                                        <dxe:ASPxComboBox ID="ddlGrupo" runat="server" ClientInstanceName="ddlGrupo"
                                                                                                                            ValueType="System.Int32" Width="100%">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-right: 10px; width: 150px;">
                                                                                                                        <dxe:ASPxComboBox ID="ddlUnidadeGestora" runat="server"
                                                                                                                            ClientInstanceName="ddlUnidadeGestora"
                                                                                                                            TextField="NomeUnidadeNegocio" TextFormatString="{0}"
                                                                                                                            ValueField="CodigoUnidadeNegocio" Width="100%">
                                                                                                                            <Columns>
                                                                                                                                <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio"
                                                                                                                                    Name="SiglaUnidadeNegocio" Width="60px" />
                                                                                                                                <dxe:ListBoxColumn Caption="Unidade" FieldName="NomeUnidadeNegocio"
                                                                                                                                    Name="NomeUnidadeNegocio" Width="400px" />
                                                                                                                            </Columns>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxTextBox ID="txtAvaliado" runat="server"
                                                                                                                            ClientInstanceName="txtAvaliado"
                                                                                                                            MaxLength="250" Width="100%">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td id="tdDadoCriterioIndicador">
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td id="tblTipoIndicador" style="padding-right: 10px">
                                                                                                                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server"
                                                                                                                            HeaderText="Tipo de Indicador" Width="100%" CornerRadius="0px">
                                                                                                                            <ContentPaddings Padding="0px" />
                                                                                                                            <PanelCollection>
                                                                                                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                                                                    <dxe:ASPxRadioButtonList ID="rblTipoIndicador" runat="server"
                                                                                                                                        ClientInstanceName="rblTipoIndicador"
                                                                                                                                        RepeatDirection="Horizontal" Width="275px">
                                                                                                                                        <Paddings Padding="2px" />
                                                                                                                                        <Items>
                                                                                                                                            <dxe:ListEditItem Text="Desempenho" Value="D" />
                                                                                                                                            <dxe:ListEditItem Text="Operacional" Value="O" />
                                                                                                                                        </Items>
                                                                                                                                        <Border BorderStyle="None" />
                                                                                                                                        <DisabledStyle ForeColor="Black">
                                                                                                                                        </DisabledStyle>
                                                                                                                                    </dxe:ASPxRadioButtonList>
                                                                                                                                </dxp:PanelContent>
                                                                                                                            </PanelCollection>
                                                                                                                        </dxrp:ASPxRoundPanel>
                                                                                                                    </td>
                                                                                                                    <td id="tdDadoCriterioIndicador2">
                                                                                                                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server"
                                                                                                                            HeaderText="Critério" Width="100%" CornerRadius="0px">
                                                                                                                            <ContentPaddings Padding="0px" />
                                                                                                                            <PanelCollection>
                                                                                                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                                                                    <dxe:ASPxRadioButtonList ID="rblCriterio" runat="server"
                                                                                                                                        ClientInstanceName="rblCriterio"
                                                                                                                                        RepeatDirection="Horizontal" Width="275px">
                                                                                                                                        <Paddings Padding="2px" />
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
}" />
                                                                                                                                        <Items>
                                                                                                                                            <dxe:ListEditItem Text="Acumulado" Value="A" />
                                                                                                                                            <dxe:ListEditItem Text="Status" Value="S" />
                                                                                                                                        </Items>
                                                                                                                                        <Border BorderStyle="None" />
                                                                                                                                        <DisabledStyle ForeColor="Black">
                                                                                                                                        </DisabledStyle>
                                                                                                                                    </dxe:ASPxRadioButtonList>
                                                                                                                                </dxp:PanelContent>
                                                                                                                            </PanelCollection>
                                                                                                                        </dxrp:ASPxRoundPanel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td style="width: 170px">
                                                                                                                        <dxe:ASPxCheckBox ID="ckMetaDesdobravel" runat="server" CheckState="Unchecked"
                                                                                                                            ClientInstanceName="ckMetaDesdobravel"
                                                                                                                            Text="Meta desdobrável?">
                                                                                                                            <DisabledStyle ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox ID="ckDesempenho" runat="server" CheckState="Unchecked"
                                                                                                                            ClientInstanceName="ckDesempenho"
                                                                                                                            Text="O desempenho é verificado comparando diretamente a meta com o resultado?">
                                                                                                                            <DisabledStyle ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 8px"></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel ID="lblGlossario" runat="server"
                                                                                                                Text="Glossário:">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxMemo ID="heGlossario" runat="server" ClientInstanceName="heGlossario"
                                                                                                                Rows="13" Width="100%">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxMemo>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </dxw:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dxtc:TabPage>
                                                                                <dxtc:TabPage Name="tbMetrica" Text="Métrica">
                                                                                    <ContentCollection>
                                                                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                                            <dxwgv:ASPxGridView ID="gvMetrica" runat="server" AutoGenerateColumns="False"
                                                                                                ClientInstanceName="gvMetrica"
                                                                                                KeyFieldName="CodigoFaixa" OnRowDeleting="gvMetrica_RowDeleting"
                                                                                                OnRowInserting="gvMetrica_RowInserting" OnRowUpdating="gvMetrica_RowUpdating"
                                                                                                Width="100%">
                                                                                                <ClientSideEvents CustomButtonClick="
" />
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" "
                                                                                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="110px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                                        <HeaderTemplate>
                                                                                                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""center"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""gvMetrica.AddNewRow();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Incluir"" style=""cursor: default;""/>")%>
                                                                                                        </HeaderTemplate>
                                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                                    <dxwgv:GridViewDataSpinEditColumn Caption="De" FieldName="ValorLimiteInferior"
                                                                                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="170px">
                                                                                                        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="g">
                                                                                                            <ValidationSettings>
                                                                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesSpinEdit>
                                                                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Até" FieldName="ValorLimiteSuperior"
                                                                                                        ShowInCustomizationForm="True" VisibleIndex="2" Width="170px">
                                                                                                        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="g">
                                                                                                            <ValidationSettings>
                                                                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesSpinEdit>
                                                                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Cor" FieldName="CorDesempenho"
                                                                                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                                        <PropertiesComboBox>
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Azul" Value="Azul" />
                                                                                                                <dxe:ListEditItem Text="Verde" Value="Verde" />
                                                                                                                <dxe:ListEditItem Text="Amarelo" Value="Amarelo" />
                                                                                                                <dxe:ListEditItem Text="Vermelho" Value="Vermelho" />
                                                                                                            </Items>
                                                                                                            <ValidationSettings>
                                                                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesComboBox>
                                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                                </Columns>
                                                                                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                                </SettingsPager>
                                                                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3" />
                                                                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="370" />
                                                                                                <SettingsPopup>
                                                                                                    <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter"
                                                                                                        Width="500px" />
                                                                                                </SettingsPopup>
                                                                                                <Styles>
                                                                                                    <EditForm>
                                                                                                    </EditForm>
                                                                                                    <EditFormCell>
                                                                                                    </EditFormCell>
                                                                                                </Styles>
                                                                                            </dxwgv:ASPxGridView>
                                                                                        </dxw:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dxtc:TabPage>
                                                                            </TabPages>
                                                                            <ContentStyle>
                                                                                <Paddings Padding="3px" PaddingLeft="3px" PaddingRight="3px"></Paddings>
                                                                            </ContentStyle>
                                                                        </dxtc:ASPxPageControl>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="padding-top: 3px">
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 90px">
                                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100%"
                                                                                            ID="btnSalvar">
                                                                                            <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                    <td align="right"></td>
                                                                                    <td style="width: 90px" align="right">
                                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%"
                                                                                            ID="btnFechar">
                                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxpc:PopupControlContentControl>
                                                </ContentCollection>
                                            </dxpc:ASPxPopupControl>
                                            <!-- ASPxPOPUPCONTROL : Dados salvos -->
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	local_onEnd_pnCallback(s,e);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td></td>
        </tr>
    </table>
    <asp:SqlDataSource runat="server" ID="dsResponsavel"></asp:SqlDataSource>
</asp:Content>
