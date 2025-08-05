<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseProcessosObras.aspx.cs" Inherits="_Processos_Relatorios_relAnaliseProcessosObras"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-right: 20px;
            padding-left: 10px">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 99%">
                        <tr style="height:26px">
                            <td valign="middle" style="height: 26px">
                                &nbsp;
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Processos de Obras"></asp:Label>
                            </td>
                            <td valign="middle" style="padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Grupo do Fluxo:" Visible="True">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 400px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxComboBox ID="ddlGrupoFluxo" runat="server" ClientInstanceName="ddlGrupoFluxo"
                                     Width="100%" IncrementalFilteringDelay="250"
                                    IncrementalFilteringMode="Contains">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                      <ClientSideEvents ButtonClick="function(s, e) {
btnSelecionar.SetEnabled(true);
}" ValueChanged="function(s, e) {
	btnSelecionar.SetEnabled(true);
}" />
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="left" style="width: 85px; height: 26px;" valign="middle">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                    AutoPostBack="False" Width="85px" 
                                    ClientInstanceName="btnSelecionar" ClientEnabled="False">
                                    <Paddings Padding="0px"></Paddings>
                                    <ClientSideEvents Click="function(s, e) {
	grid.PerformCallback(&quot;PopularGrid&quot;);
}"></ClientSideEvents>
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 205px">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlExporta" runat="server" ClientInstanceName="ddlExporta"
                                                 ValueType="System.String">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td style="padding-left: 2px">
                                            <dxcp:ASPxCallbackPanel ID="pnImage" runat="server" ClientInstanceName="pnImage"
                                                Height="22px" HideContentOnCallback="False" OnCallback="pnImage_Callback" 
                                                 Width="23px">
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <dxe:ASPxImage ID="imgExportacao" runat="server" ClientInstanceName="imgExportacao"
                                                            Height="20px" ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px">
                                                        </dxe:ASPxImage>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxcp:ASPxCallbackPanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="Aspxbutton1" runat="server" 
                                    OnClick="btnExcel_Click" Text="Exportar">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                                <dxhf:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid">
                                </dxpgwx:ASPxPivotGridExporter>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="Div1" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                        <dxwpg:ASPxPivotGrid ID="grid" runat="server" ClientInstanceName="grid"
                            OnCustomCallback="grid_CustomCallback" CustomizationFieldsLeft="200" CustomizationFieldsTop="200"
                            OnAfterPerformCallback="grid_AfterPerformCallback" Width="100%" SummaryText="Média">
                            <OptionsCustomization CustomizationWindowWidth="210" CustomizationWindowHeight="280">
                            </OptionsCustomization>
                            <ClientSideEvents BeginCallback="function(s, e) {
	hfGeral.Set('FoiMechido', '');
	if('S' == s.cp_Alterado)
		hfGeral.Set('FoiMechido', 'S');
}"></ClientSideEvents>
                            <Fields>
                                <dxwpg:PivotGridField FieldName="NomeProcesso" ID="fieldNomeProcesso" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="2" Caption="Nome Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TempoProcessoDias" ID="fieldTempoProcessoDias" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="0" Caption="Tempo Processo Dias" GrandTotalText="Tempo M&#233;dio"
                                    SummaryType="Average" CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric"
                                    TotalCellFormat-FormatString="{0:n1}" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                    GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                    TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TempoProcessoHoras" ID="fieldTempoProcessoHoras"
                                    AllowedAreas="DataArea" Visible="False" Caption="Tempo Processo Horas" SummaryType="Average"
                                    CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n1}"
                                    TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                    GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                    TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="StatusProcesso" ID="fieldStatusProcesso" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="0" Caption="Status Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="InicioProcesso" ID="fieldInicioProcesso" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="3" Visible="False" Caption="Inicio Processo" CellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                    ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                    TotalValueFormat-FormatType="DateTime">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TerminoProcesso" ID="fieldTerminoProcesso" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="2" Visible="False" Caption="Termino Processo" CellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                    ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                    TotalValueFormat-FormatType="DateTime">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="2" Visible="False" Caption="Nome Projeto">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="SegmentoObra" ID="fieldSegmentoObra" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="3" Caption="Segmento Obra">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Versao" ID="fieldVersao" Area="RowArea" AreaIndex="3"
                                    Visible="False" Caption="Versao">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TipoProcesso" ID="fieldTipoProcesso" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="1" Caption="Tipo Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TipoProjeto" ID="fieldTipoProjeto" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Tipo Projeto">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="AtrasoDias" ID="fieldAtrasoDias" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="1" Caption="Atraso Dias" SummaryType="Average" CellFormat-FormatString="{0:n1}"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n1}" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="{0:n1}" GrandTotalCellFormat-FormatType="Numeric"
                                    ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n1}"
                                    TotalValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="AtrasoHoras" ID="fieldAtrasoHoras" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="2" Visible="False" Caption="Atraso Horas" SummaryType="Average"
                                    CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n1}"
                                    TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                    GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                    TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="EtapaAtual" ID="fieldEtapaAtual" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Etapa Atual">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="AnoInicioProcesso" ID="fieldAnoInicioProcesso" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Ano Inicio Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="MesInicioProcesso" ID="fieldMesInicioProcesso" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Mes Inicio Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="AnoTerminoProcesso" ID="fieldAnoTerminoProcesso"
                                    AllowedAreas="RowArea, ColumnArea" Area="RowArea" AreaIndex="4" Visible="False"
                                    Caption="Ano Termino Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="MesTerminoProcesso" ID="fieldMesTerminoProcesso"
                                    AllowedAreas="RowArea, ColumnArea" Area="RowArea" AreaIndex="4" Visible="False"
                                    Caption="Mes Termino Processo">
                                </dxwpg:PivotGridField>
                            </Fields>
                            <Styles>
                                <HeaderStyle ></HeaderStyle>
                                <AreaStyle >
                                </AreaStyle>
                                <FilterAreaStyle >
                                </FilterAreaStyle>
                                <FieldValueStyle >
                                </FieldValueStyle>
                                <FieldValueTotalStyle >
                                </FieldValueTotalStyle>
                                <FieldValueGrandTotalStyle >
                                </FieldValueGrandTotalStyle>
                                <CellStyle >
                                </CellStyle>
                                <GrandTotalCellStyle >
                                </GrandTotalCellStyle>
                                <FilterWindowStyle >
                                </FilterWindowStyle>
                                <FilterButtonPanelStyle >
                                </FilterButtonPanelStyle>
                                <MenuItemStyle ></MenuItemStyle>
                                <MenuStyle ></MenuStyle>
                                <CustomizationFieldsContentStyle >
                                </CustomizationFieldsContentStyle>
                            </Styles>
                            <OptionsLoadingPanel Text="Carregando...">
                            </OptionsLoadingPanel>
                            <OptionsPager EllipsisMode="None">
                            </OptionsPager>
                        </dxwpg:ASPxPivotGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
</asp:Content>
