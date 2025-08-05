<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseEtapasObras.aspx.cs" Inherits="_Processos_Relatorios_relAnaliseEtapasObras"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-right: 20px;padding-top:5px;
            padding-left: 10px">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                        <tr style="height:26px">
                            <td valign="left" style="padding-right: 5px;padding-left:10px; height: 26px;width:100px" align="left">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Grupo do Fluxo:" Visible="True">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 400px; padding-right: 5px; height: 26px;" align="right" valign="left">
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
                            <td align="right" style="width: 85px; height: 26px;" valign="middle">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                    AutoPostBack="False" Width="85px" 
                                    ClientInstanceName="btnSelecionar" ClientEnabled="False">
                                    <Paddings Padding="0px"></Paddings>
                                    <ClientSideEvents Click="function(s, e) {
    hfGeral.Set(&quot;clickSelecionar&quot;, &quot;true&quot;)
	grid.PerformCallback(&quot;PopularGrid&quot;);
}"></ClientSideEvents>
                                </dxe:ASPxButton>
                            </td>
                            <td align="right" style="padding-right:30px">
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
                                        <td>
                                <dxe:ASPxButton ID="Aspxbutton1" runat="server" 
                                    OnClick="btnExcel_Click" Text="Exportar">
                                    <Paddings Padding="0px" />
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
                <td>
                    <div id="Div1" style="padding-left:10px; overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                        <dxwpg:ASPxPivotGrid ID="grid" runat="server" ClientInstanceName="grid"
                            OnCustomCallback="grid_CustomCallback" CustomizationFieldsLeft="200" CustomizationFieldsTop="200"
                            OnAfterPerformCallback="grid_AfterPerformCallback" Width="100%" SummaryText="Média"
                            ClientIDMode="AutoID">
                            <OptionsCustomization CustomizationWindowWidth="210" CustomizationWindowHeight="280">
                            </OptionsCustomization>
                            <ClientSideEvents BeginCallback="function(s, e) {
	hfGeral.Set('FoiMechido', '');
	if('S' == s.cp_Alterado)
		hfGeral.Set('FoiMechido', 'S');
}"></ClientSideEvents>
                            <Fields>
                                <dxwpg:PivotGridField FieldName="NomeProcesso" ID="fieldNomeProcesso" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="2" Visible="False" Caption="Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TempoEtapaDias" ID="fieldTempoEtapaDias" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="0" Caption="Tempo Decorrido Etapa (Dias)" GrandTotalText="Tempo M&#233;dio"
                                    SummaryType="Average" CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric"
                                    TotalCellFormat-FormatString="{0:n1}" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                    GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                    TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TempoEtapaHoras" ID="fieldTempoEtapaHoras" AllowedAreas="DataArea"
                                    Visible="False" Caption="Tempo Decorrido Etapa (Horas)" SummaryType="Average"
                                    CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n1}"
                                    TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                    GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                    TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="StatusProcesso" ID="fieldStatusProcesso" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="0" Caption="Status do Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="InicioEtapa" ID="fieldInicioEtapa" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="3" Visible="False" Caption="Data de In&#237;cio da Etapa"
                                    CellFormat-FormatString="dd/MM/yyyy HH:mm" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                    ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                    TotalValueFormat-FormatType="DateTime">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TerminoEtapa" ID="fieldTerminoEtapa" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="2" Visible="False" Caption="Data de T&#233;rmino da Etapa"
                                    CellFormat-FormatString="dd/MM/yyyy HH:mm" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                    GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                    ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                    TotalValueFormat-FormatType="DateTime">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="2" Visible="False" Caption="Projeto/Obra">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="SegmentoObra" ID="fieldSegmentoObra" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="2" Visible="False" Caption="Segmento da Obra">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Versao" ID="fieldVersao" Area="RowArea" AreaIndex="3"
                                    Visible="False" Caption="Vers&#227;o do Fluxo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TipoProcesso" ID="fieldTipoProcesso" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="1" Caption="Tipo de Processo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TipoProjeto" ID="fieldTipoProjeto" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Tipo de Projeto/Obra">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="AtrasoDias" ID="fieldAtrasoDias" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="1" Caption="Atraso Etapa (Dias)" SummaryType="Average"
                                    CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n1}"
                                    TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                    GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                    TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="AtrasoHoras" ID="fieldAtrasoHoras" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="2" Visible="False" Caption="Atraso Etapa (Horas)"
                                    SummaryType="Average" CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric"
                                    TotalCellFormat-FormatString="{0:n1}" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                    GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                    TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="NomeEtapa" ID="fieldNomeEtapa" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="2" Caption="Nome da Etapa " Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="AnoInicioEtapa" ID="fieldAnoInicioEtapa" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Ano Inicio Etapa">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="MesInicioEtapa" ID="fieldMesInicioEtapa" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Mes Inicio Etapa">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="AnoTerminoEtapa" ID="fieldAnoTerminoEtapa" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Ano Termino Etapa">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="MesTerminoEtapa" ID="fieldMesTerminoEtapa" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Mes Termino Etapa">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ResponsavelEtapa" ID="fieldResponsavelEtapa" AllowedAreas="RowArea"
                                    Area="RowArea" AreaIndex="4" Visible="False" Caption="Nome do Respons&#225;vel pela Etapa">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="StatusEtapa" ID="fieldStatusEtapa" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="2" Visible="False" Caption="Status da Etapa">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldnumeroContrato" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                    Area="RowArea" Caption="Numero Contrato" FieldName="numeroContrato" Visible="False"
                                    AreaIndex="2">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldNomeEtapaReduzido" AllowedAreas="RowArea" Area="RowArea"
                                    Caption="Descrição Etapa Resumido" FieldName="NomeEtapaReduzido" AreaIndex="2">
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
                                    <dxhf:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid"
                                    OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                                </dxpgwx:ASPxPivotGridExporter>

</asp:Content>
