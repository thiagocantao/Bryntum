<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="GestaoPlanosInvestimentoTIC.aspx.cs" Inherits="_Projetos_Administracao_GestaoPlanosInvestimentoTIC" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0"
        width: 100%">
        <tr>
            <td style="height: 26px; padding-left: 10px;" valign="middle">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela"
                    Font-Bold="True"  Text="Gestão dos Planos de Investimento de TIC">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
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
                &nbsp;
            </td>
            <td align="right">
                <table cellpadding="0" cellspacing="0" class="headerGrid">
                    <tr>
                        <td align="right" style="padding-right: 2px">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="Plano de Investimento:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 470px">
                            <dxe:ASPxComboBox ID="ddlPlanoInvestimento" runat="server" ClientInstanceName="ddlPlanoInvestimento"
                                 Width="100%" TextFormatString="{0}">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback();
}" />
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback();
}"></ClientSideEvents>
                                <Columns>
                                    <dxe:ListBoxColumn Caption="Plano de Investimento" FieldName="DescricaoPlanoInvestimento"
                                        Width="300px" />
                                    <dxe:ListBoxColumn Caption="Ano" FieldName="Ano" Width="100px" />
                                    <dxe:ListBoxColumn Caption="Status" FieldName="DescricaoStatusPlanoInvestimento"
                                        Width="300px" />
                                </Columns>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
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
                &nbsp;
            </td>
            <td>
                <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="gvDados"
                    Width="100%" AutoGenerateColumns="False" KeyFieldName="CodigoProjeto"
                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnCommandButtonInitialize="gvDados_CommandButtonInitialize"
                    OnCustomCallback="gvDados_CustomCallback">
                    <SettingsText CustomizationWindowCaption="Colunas Disponíveis" />
                    <ClientSideEvents EndCallback="function(s, e) {
	btnPublicar.SetText(s.cp_TextoBotao);

	lblValor.SetText(s.cp_DescricaoLabel);

	if(s.cp_StatusPlanoInvestimento == 5)
		btnPublicar.SetVisible(false);
	else
		btnPublicar.SetVisible(true);

	if(s.cp_Msg != '')
		mostraDivSalvoPublicado(s.cp_Msg);	

	if(s.cp_AtualizaPlanoInvestimento == 'S')
		ddlPlanoInvestimento.PerformCallback();
}" Init="function(s, e) {
	btnPublicar.SetText(s.cp_TextoBotao);

	lblValor.SetText(s.cp_DescricaoLabel);

	if(s.cp_StatusPlanoInvestimento == 5)
		btnPublicar.SetVisible(false);
	else
		btnPublicar.SetVisible(true);
}" />
                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_StatusPlanoInvestimento == 5)
		btnPublicar.SetVisible(false);
	else
		btnPublicar.SetVisible(true);

	btnPublicar.SetText(s.cp_TextoBotao);

	lblValor.SetText(s.cp_DescricaoLabel);

	if(s.cp_Msg != '')
		mostraDivSalvoPublicado(s.cp_Msg);	

	if(s.cp_AtualizaPlanoInvestimento == 'S')
		ddlPlanoInvestimento.PerformCallback();
}" Init="function(s, e) {
	if(s.cp_StatusPlanoInvestimento == 5)
		btnPublicar.SetVisible(false);
	else
		btnPublicar.SetVisible(true);

	btnPublicar.SetText(s.cp_TextoBotao);

	lblValor.SetText(s.cp_DescricaoLabel);
}" ContextMenu="OnContextMenu"></ClientSideEvents>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorSolicitado" ShowInColumn="ValorSolicitado"
                            ShowInGroupFooterColumn="ValorSolicitado" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="LOA" ShowInColumn="LOA" ShowInGroupFooterColumn="LOA"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="AjustadoCGTIC" ShowInColumn="AjustadoCGTIC"
                            ShowInGroupFooterColumn="AjustadoCGTIC" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorRemanejado" ShowInColumn="ValorRemanejado"
                            ShowInGroupFooterColumn="ValorRemanejado" SummaryType="Sum" />
                    </TotalSummary>
                    <GroupSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorSolicitado" ShowInGroupFooterColumn="ValorSolicitado"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="LOA" ShowInGroupFooterColumn="LOA"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="AjustadoCGTIC" ShowInGroupFooterColumn="AjustadoCGTIC"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorRemanejado" ShowInGroupFooterColumn="ValorRemanejado"
                            SummaryType="Sum" />
                    </GroupSummary>
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowSelectCheckbox="True"
                            VisibleIndex="1" Width="35px" AllowDragDrop="False">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <%# (podeExcluir) ? @"<img src=""../../imagens/botoes/excluirReg02.png"" title=""Marcar como não selecionado LOA"" onclick=""abreExclusao();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/excluirRegDes.png"" title="""" style=""cursor: default;""/>"%>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Width="90px" Caption=" " VisibleIndex="0" FieldName="CodigoProjeto"
                            Name="CodigoProjeto" ExportWidth="10">
                            <Settings AllowAutoFilter="False" AllowGroup="False" AllowAutoFilterTextInputTimer="False"
                                AllowHeaderFilter="False" AllowSort="False" />
                            <Settings AllowAutoFilter="False" AllowGroup="False" AllowDragDrop="False"></Settings>
                            <FilterCellStyle>
                                <BorderRight BorderStyle="None" />
                                <BorderRight BorderStyle="None"></BorderRight>
                            </FilterCellStyle>
                            <DataItemTemplate>
                                <%# getBotoes() %>
                            </DataItemTemplate>
                            <CellStyle>
                                <BorderRight BorderStyle="None" />
                                <BorderRight BorderStyle="None"></BorderRight>
                            </CellStyle>
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
                                                        <Border BorderStyle="None" />
                                                    </HoverStyle>
                                                    <Paddings Padding="0px" />
                                                </ItemStyle>
                                                <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                    <SelectedStyle>
                                                        <Border BorderStyle="None" />
                                                    </SelectedStyle>
                                                </SubMenuItemStyle>
                                                <Border BorderStyle="None" />
                                            </dxm:ASPxMenu>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="2"
                            Width="200px" ExportWidth="250">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Nome do Projeto" FieldName="NomeProjeto" VisibleIndex="3"
                            Width="250px" ExportWidth="300">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Unidade" FieldName="NomeUnidade" VisibleIndex="4"
                            Width="220px" ExportWidth="280">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Órgão" FieldName="NomeOrgao" VisibleIndex="5"
                            Width="220px" ExportWidth="280">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="RK" FieldName="RK" VisibleIndex="6" Width="35px"
                            ExportWidth="60">
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <Settings AllowAutoFilter="False" AllowGroup="False"></Settings>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pontuação" FieldName="Pontuacao" VisibleIndex="7"
                            Width="80px" ExportWidth="110">
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <Settings AllowAutoFilter="False" AllowGroup="False"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Global (R$)" FieldName="ValorGlobal"
                            VisibleIndex="8" Width="140px">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <Settings AllowAutoFilter="False" AllowGroup="False"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ValorSolicitado" ExportWidth="160" Width="135px"
                            Caption="Valor Solicitado (R$)" VisibleIndex="9">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False"></Settings>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ajustado CGTIC (R$)" FieldName="AjustadoCGTIC"
                            VisibleIndex="10" Width="140px" ExportWidth="160">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <Settings AllowAutoFilter="False" AllowGroup="False"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="LOA (R$)" FieldName="LOA" VisibleIndex="11"
                            Width="135px" ExportWidth="160">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <Settings AllowAutoFilter="False" AllowGroup="False"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Acumulado (R$)" FieldName="ValorAcumulado"
                            VisibleIndex="12" Width="150px" ExportWidth="180">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <Settings AllowAutoFilter="False" AllowGroup="False"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Reprogramação (R$)" FieldName="ValorRemanejado"
                            VisibleIndex="13" Width="170px">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" />
                            <Settings AllowAutoFilter="False" AllowGroup="False"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Natureza" FieldName="Natureza" VisibleIndex="14"
                            Width="150px" ExportWidth="220">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="FonteRecurso" VisibleIndex="15" Caption="Fonte de Recursos"
                            ExportWidth="220" Width="150px">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ContratoConvenioOrigem" VisibleIndex="16"
                            Caption="Contrato/Convênio de Origem" ExportWidth="300" Width="250px">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoStatus" Name="CodigoStatus" ShowInCustomizationForm="False"
                            Visible="False" VisibleIndex="23">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Código Programa PPAG" FieldName="CodigoProgramaPPAG"
                            VisibleIndex="17" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Nome Programa PPAG" FieldName="NomeProgramaPPAG"
                            VisibleIndex="18" Width="250px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Código Ação PPAG" FieldName="CodigoAcaoPPAG"
                            VisibleIndex="19" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Nome Ação PPAG" FieldName="NomeAcaoPPAG" VisibleIndex="20"
                            Width="250px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Código Sub Ação PPAG" FieldName="CodigoSubAcaoPPAG"
                            VisibleIndex="21" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Nome Sub Ação PPAG" FieldName="NomeSubAcaoPPAG"
                            VisibleIndex="22" Width="250px">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" EnableCustomizationWindow="True">
                    </SettingsBehavior>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True" ShowGroupFooter="VisibleAlways"
                        ShowHeaderFilterBlankItems="False" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                    <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" ShowGroupPanel="True"
                        ShowFooter="True" ShowGroupFooter="VisibleAlways" VerticalScrollBarMode="Visible"
                        HorizontalScrollBarMode="Visible"></Settings>
                    <SettingsText CustomizationWindowCaption="Colunas Dispon&#237;veis"></SettingsText>
                    <SettingsPopup>
                        <CustomizationWindow Height="200px" Width="200px" HorizontalAlign="LeftSides" VerticalAlign="TopSides" />
                        <CustomizationWindow Width="200px" Height="200px" HorizontalAlign="LeftSides" VerticalAlign="TopSides">
                        </CustomizationWindow>
                    </SettingsPopup>
                </dxwgv:ASPxGridView>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 10px;">
            </td>
            <td style="height: 10px">
            </td>
            <td style="width: 10px; height: 10px;">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right">
                <dxe:ASPxButton ID="btnPublicar" runat="server" AutoPostBack="False" ClientInstanceName="btnPublicar"
                    CssClass="NaoQuebraTexto"  Text="Publicar">
                    <ClientSideEvents Click="function(s, e) {
                        var sentenca = 'Deseja ' + s.GetText() + '?';
                    window.top.mostraMensagem(sentenca, 'confirmacao', true, true, publicar);	
		
}" />
                    <Paddings Padding="2px" PaddingLeft="3px" PaddingRight="3px" />
                </dxe:ASPxButton>
                <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                    OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                </dxwgv:ASPxGridViewExporter>
                <dxm:ASPxPopupMenu ID="headerMenu" runat="server" ClientInstanceName="headerMenu"
                    >
                    <Items>
                        <dxm:MenuItem Text="Ocultar Coluna" Name="HideColumn">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="Mostrar/Ocultar Colunas Disponíveis" Name="ShowHideList">
                        </dxm:MenuItem>
                    </Items>
                    <ClientSideEvents ItemClick="OnItemClick" />
                </dxm:ASPxPopupMenu>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados"
        HeaderText="Plano de Investimento" Width="900px" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ClientSideEvents CloseUp="function(s, e) {
	mmObjeto.SetText('');
	txtValor.SetText('');
	txtProjeto.SetText('');
}" />
        <ClientSideEvents CloseUp="function(s, e) {
	mmObjeto.SetText('');
	txtValor.SetText('');
	txtProjeto.SetText('');
 	lblCantCarater.SetText('0');
}"></ClientSideEvents>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                    <tr>
                        <td id="tdDetalhesProjeto">
                            <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="lblProjeto" runat="server" 
                                            Text="Projeto:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td class="linhaEdicaoPeriodo">
                                        <dxe:ASPxLabel ID="lblValor" runat="server" 
                                            Text="Valor:" ClientInstanceName="lblValor">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtProjeto" runat="server" ClientEnabled="False" ClientInstanceName="txtProjeto"
                                            Width="100%" >
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td class="linhaEdicaoPeriodo">
                                        <dxe:ASPxSpinEdit ID="txtValor" runat="server" AllowMouseWheel="False" ClientInstanceName="txtValor"
                                            DecimalPlaces="2"  Width="100%" DisplayFormatString="n2">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 5px; height: 10px;">
                                    </td>
                                    <td class="linhaEdicaoPeriodo" style="height: 10px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel37" runat="server" ClientInstanceName="lblNumeroContrato"
                                 Text="Comentários:">
                            </dxe:ASPxLabel>
                            <dxe:ASPxLabel ID="lblCantCarater0" runat="server" ClientInstanceName="lblCantCarater"
                                 ForeColor="Silver" Text="0">
                            </dxe:ASPxLabel>
                            <dxe:ASPxLabel ID="lblDe251" runat="server" ClientInstanceName="lblDe250" EncodeHtml="False"
                                 ForeColor="Silver" Text="&amp;nbsp;de 4000">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxMemo ID="mmObjeto" runat="server" ClientInstanceName="mmObjeto"
                                Rows="12" Width="100%">
                                <ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e);
}" ValueChanged="function(s, e) {
	
}" />
                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" Init="function(s, e) {
	onInit_mmObjeto(s, e);
}"></ClientSideEvents>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table border="0" cellpadding="0" cellspacing="0" >
                                <tbody>
                                    <tr>
                                        <td id="tdBtnSalvar" align="right" style="padding-right: 5px; padding-left: 5px;">
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                                Text="Salvar" Width="90px" AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	executaAcao();
}" />
                                                <Paddings Padding="0px" />
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	executaAcao();
}"></ClientSideEvents>
                                                <Paddings Padding="0px"></Paddings>
                                            </dxe:ASPxButton>
                                        </td>
                                        <td align="right" style="width: 100px;">
                                            <dxe:ASPxButton ID="btnCancelar" runat="server" ClientInstanceName="btnCancelar"
                                                CommandArgument="btnCancelar"  Text="Fechar"
                                                Width="90px" AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {
	pcDados.Hide();
}" />
                                                <Paddings Padding="0px" />
                                                <ClientSideEvents Click="function(s, e) {
	pcDados.Hide();
}"></ClientSideEvents>
                                                <Paddings Padding="0px"></Paddings>
                                            </dxe:ASPxButton>
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
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
        ShowHeader="False"  ID="pcUsuarioIncluido"
        Width="400px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tbody>
                        <tr>
                            <td style="" align="center">
                            </td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                    ClientInstanceName="imgSalvar" ID="imgSalvar">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
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
</asp:Content>
