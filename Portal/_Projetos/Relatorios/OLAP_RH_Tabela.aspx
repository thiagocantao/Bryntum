<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="OLAP_RH_Tabela.aspx.cs" Inherits="_OLAP_RH_Tabela" Title="Portal da Estratégia" %>

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
                            <td valign="middle" style="height: 26px; padding-left: 10px;">
                                <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Recursos (Demanda x Disponibilidade RH)"></asp:Label>
                            </td>
                            <td valign="middle" style="padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Período:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxDateEdit ID="dteInicio" runat="server" Font-Overline="False"
                                    Width="105px" AllowNull="False" ClientInstanceName="dteInicio">
                                    <CalendarProperties>
                                        <DayHeaderStyle  />
                                        <WeekNumberStyle >
                                        </WeekNumberStyle>
                                        <DayStyle  />
                                        <DaySelectedStyle >
                                        </DaySelectedStyle>
                                        <DayOtherMonthStyle >
                                        </DayOtherMonthStyle>
                                        <DayWeekendStyle >
                                        </DayWeekendStyle>
                                        <DayOutOfRangeStyle >
                                        </DayOutOfRangeStyle>
                                        <TodayStyle >
                                        </TodayStyle>
                                        <HeaderStyle  />
                                        <FooterStyle  />
                                        <FastNavStyle >
                                        </FastNavStyle>
                                        <FastNavMonthAreaStyle >
                                        </FastNavMonthAreaStyle>
                                        <FastNavYearAreaStyle >
                                        </FastNavYearAreaStyle>
                                        <FastNavMonthStyle >
                                        </FastNavMonthStyle>
                                        <FastNavYearStyle >
                                            <SelectedStyle >
                                            </SelectedStyle>
                                            <HoverStyle >
                                            </HoverStyle>
                                        </FastNavYearStyle>
                                        <FastNavFooterStyle >
                                        </FastNavFooterStyle>
                                        <FocusedStyle >
                                        </FocusedStyle>
                                        <Style >
                                            
                                        </Style>
                                    </CalendarProperties>
                                    <MaskHintStyle >
                                    </MaskHintStyle>
                                    <FocusedStyle >
                                    </FocusedStyle>
                                </dxe:ASPxDateEdit>
                            </td>
                            <td style="width: 10px; padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="a" Width="10px">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxDateEdit ID="dteFim" runat="server" 
                                    Font-Strikeout="False" Width="105px" AllowNull="False" ClientInstanceName="dteFim">
                                    <CalendarProperties>
                                        <DayHeaderStyle  />
                                        <WeekNumberStyle >
                                        </WeekNumberStyle>
                                        <DayStyle  />
                                        <DaySelectedStyle >
                                        </DaySelectedStyle>
                                        <DayOtherMonthStyle >
                                        </DayOtherMonthStyle>
                                        <DayWeekendStyle >
                                        </DayWeekendStyle>
                                        <DayOutOfRangeStyle >
                                        </DayOutOfRangeStyle>
                                        <TodayStyle >
                                        </TodayStyle>
                                        <ButtonStyle >
                                        </ButtonStyle>
                                        <HeaderStyle  />
                                        <FooterStyle  />
                                        <FastNavStyle >
                                        </FastNavStyle>
                                        <FastNavMonthAreaStyle >
                                        </FastNavMonthAreaStyle>
                                        <FastNavYearAreaStyle >
                                        </FastNavYearAreaStyle>
                                        <FastNavMonthStyle >
                                            <SelectedStyle >
                                            </SelectedStyle>
                                        </FastNavMonthStyle>
                                        <FastNavYearStyle >
                                        </FastNavYearStyle>
                                        <FastNavFooterStyle >
                                        </FastNavFooterStyle>
                                        <ReadOnlyStyle >
                                        </ReadOnlyStyle>
                                        <FocusedStyle >
                                        </FocusedStyle>
                                        <InvalidStyle >
                                        </InvalidStyle>
                                        <Style >
                                            
                                        </Style>
                                    </CalendarProperties>
                                    <MaskHintStyle >
                                    </MaskHintStyle>
                                    <ClientSideEvents ButtonClick="function(s, e) {
btnSelecionar.SetEnabled(true);
}" ValueChanged="function(s, e) {
	btnSelecionar.SetEnabled(true);
}" />
                                    <NullTextStyle >
                                    </NullTextStyle>
                                    <ReadOnlyStyle >
                                    </ReadOnlyStyle>
                                    <FocusedStyle >
                                    </FocusedStyle>
                                    <DisabledStyle >
                                    </DisabledStyle>
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 85px; height: 26px;" valign="middle">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                    AutoPostBack="False" Width="85px" ClientInstanceName="btnSelecionar">
                                    <ClientSideEvents Click="function(s, e) {
	//debugger
    var retorno = true;

 	var dataInicial = new Date(dteInicio.GetValue());
 	var dataFinal =  new Date(dteFim.GetValue());

	var anoInicial = dataInicial.getFullYear();//1992
	var anoFinal = dataFinal.getFullYear();//1992

	var diferencaAno = anoFinal - anoInicial;

	var mesInicial = dataInicial.getMonth();// de 0 a 11
	var mesFinal = dataFinal.getMonth();// de 0 a 11

	var diferencaMes = mesFinal - mesInicial;

	var diaInicial = dataInicial.getDate();
	var diaFinal = dataFinal.getDate();

	var diferencaDias = diaFinal - diaInicial;

	if(diferencaAno == 0)
	{
		if(diferencaMes &lt;= 1)
		{
			if(diferencaDias &lt;= 40)
            {
				retorno = true;
			}
			else
			{
				retorno = false;
			}
		}
		else
		{
			retorno = false;
		}
	}
	else
	{
		retorno = false;
	}
	if(retorno == true)
	{
		grid.PerformCallback(&quot;PopularGrid&quot;);
	}
	else
	{
		window.top.mostraMensagem(&quot;Somente podem ser consultados intervalos menores ou iguais a 40 dias.&quot;, 'Atencao', true, false, null);
	}	
}"></ClientSideEvents>
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid"
                        OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell" OnCustomExportFieldValue="ASPxPivotGridExporter1_CustomExportFieldValue"
                        OnCustomExportHeader="ASPxPivotGridExporter1_CustomExportHeader">
                    </dxpgwx:ASPxPivotGridExporter>
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
                                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                </dxhf:ASPxHiddenField>
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
                             
                            Width="99%"  OnCustomFieldSort="grid_CustomFieldSort"
                            OnCustomCallback="grid_CustomCallback" CustomizationFieldsLeft="200" CustomizationFieldsTop="200"
                            OnCustomCellDisplayText="grid_CustomCellDisplayText" ClientIDMode="AutoID">
                            <Fields>
                                <dxwpg:PivotGridField FieldName="SiglaUnidadeNegocio" ID="fieldSiglaUnidadeNegocio"
                                    Area="RowArea" AreaIndex="0" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                    Caption="Unidade" Options-AllowDrag="False" Options-AllowDragInCustomizationForm="False"
                                    Options-AllowExpand="False" Options-AllowFilter="False" Options-AllowSort="False"
                                    Options-AllowSortBySummary="False" Options-ShowInCustomizationForm="False">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" Area="RowArea"
                                    AreaIndex="1" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Projeto">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="DescricaoStatus" ID="fieldDescricaoStatus"
                                    AreaIndex="4" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                    Caption="Status">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="NomeRecurso" ID="fieldNomeRecurso" Area="RowArea"
                                    AreaIndex="0" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Recurso"
                                    ExpandedInFieldsGroup="False">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Trabalho" ID="fieldTrabalho" Area="DataArea" AreaIndex="0"
                                    AllowedAreas="DataArea" Caption="Trabalho (h)" CellFormat-FormatString="#,###.#"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Capacidade" ID="fieldCapacidade" Area="DataArea"
                                    AreaIndex="1" AllowedAreas="DataArea" Caption="Capacidade (h)" CellFormat-FormatString="{0:n2}"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n2}" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="{0:n2}" GrandTotalCellFormat-FormatType="Numeric"
                                    ExpandedInFieldsGroup="False" TotalValueFormat-FormatString="{0:n2}" TotalValueFormat-FormatType="Numeric"
                                    ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Ano" ID="fieldAno" AreaIndex="0" 
                                    AllowedAreas="RowArea, ColumnArea, FilterArea" UnboundFieldName="field"
                                    Caption="Ano">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Mes" ID="fieldMes" AreaIndex="1" 
                                    AllowedAreas="RowArea, ColumnArea, FilterArea" UnboundFieldName="field1"
                                    Caption="M&#234;s" SortMode="Custom">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="DataReferencia" ID="field2" AreaIndex="2" 
                                    AllowedAreas="RowArea, ColumnArea, FilterArea" GroupInterval="Date"
                                    UnboundFieldName="field2" Caption="Data">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Disponibilidade" ID="fieldDisponibilidade" Area="DataArea"
                                    AreaIndex="2" AllowedAreas="DataArea" Caption="Disponibilidade (h)" CellFormat-FormatString="#,###.#"
                                    CellFormat-FormatType="Custom" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Custom"
                                    GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Custom">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TrabalhoLB" ID="field4" Area="DataArea" AreaIndex="3"
                                    Visible="False" AllowedAreas="DataArea" Caption="Trabalho Previsto (h)" CellFormat-FormatString="#,###.#"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TrabalhoReal" ID="field5" Area="DataArea" AreaIndex="3"
                                    Visible="False" AllowedAreas="DataArea" Caption="Trabalho Realizado (h)" CellFormat-FormatString="#,###.#"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Semana" ID="fieldSemana" Area="ColumnArea" AreaIndex="0"
                                    AllowedAreas="RowArea, ColumnArea, FilterArea" UnboundFieldName="field3" Caption="Semana"
                                    SortMode="Custom">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldAnotacoes" AllowedAreas="RowArea, FilterArea" AreaIndex="5"
                                    Caption="Conhecimentos, Habilidades e Competências" FieldName="Anotacoes">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldNomeUnidadeNegocio" 
                                    AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="3" 
                                    Caption="Nome Unidade Negocio" FieldName="NomeUnidadeNegocio">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldNomeEntidade" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                    Area="RowArea" AreaIndex="2" Caption="Nome Entidade" FieldName="NomeEntidade"
                                    Visible="False">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldCustoHora" AllowedAreas="RowArea" Area="RowArea" Caption="Despesa Unitária (R$)"
                                    CellFormat-FormatString="{0:n2}" CellFormat-FormatType="Numeric" FieldName="CustoHora"
                                    GrandTotalCellFormat-FormatString="{0:n2}" GrandTotalCellFormat-FormatType="Numeric"
                                    TotalCellFormat-FormatString="{0:n2}" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}"
                                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric"
                                    Visible="False" AreaIndex="1">
                                    <CellStyle >
                                    </CellStyle>
                                </dxwpg:PivotGridField>
                            </Fields>
                            <Styles>
                                <HeaderStyle ></HeaderStyle>
                                <AreaStyle >
                                </AreaStyle>
                                <FilterAreaStyle >
                                </FilterAreaStyle>
                                <DataAreaStyle >
                                </DataAreaStyle>
                                <ColumnAreaStyle >
                                </ColumnAreaStyle>
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
                                <FilterItemsAreaStyle >
                                </FilterItemsAreaStyle>
                                <FilterItemStyle >
                                </FilterItemStyle>
                                <FilterButtonStyle >
                                </FilterButtonStyle>
                                <FilterButtonPanelStyle >
                                </FilterButtonPanelStyle>
                                <MenuItemStyle  />
                                <MenuStyle  />
                                <LoadingPanel >
                                </LoadingPanel>
                            </Styles>
                            <Prefilter CriteriaString="Contains([fieldAnotacoes], '')" />
                            <OptionsCustomization CustomizationWindowHeight="350" CustomizationWindowWidth="200">
                            </OptionsCustomization>
                            <OptionsLoadingPanel Text=" ">
                                <Style >
                                </Style>
                            </OptionsLoadingPanel>
                            <OptionsPager EllipsisMode="None" RowsPerPage="15" Position="Bottom">
                            </OptionsPager>
                            <StylesPager>
                                <Pager >
                                </Pager>
                                <PageSizeItem >
                                </PageSizeItem>
                                <Summary >
                                </Summary>
                            </StylesPager>
                            <StylesPrint/>
                            <StylesEditors>
                                <Style >
                                </Style>
                            </StylesEditors>
                            <StylesFilterControl>
                                <Table >
                                </Table>
                                <PropertyName >
                                </PropertyName>
                                <GroupType >
                                </GroupType>
                                <Operation >
                                </Operation>
                                <ImageButton >
                                </ImageButton>
                            </StylesFilterControl>
                        </dxwpg:ASPxPivotGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
