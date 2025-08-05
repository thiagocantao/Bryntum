<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseIndicadores.aspx.cs" Inherits="_Projetos_Relatorios_relAnaliseIndicadores"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr style="height:26px">
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle" style="padding-left: 10px">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Indicadores de Projetos"></asp:Label>
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Desempenho:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel40" runat="server" 
                                    Text="Ano:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="height: 26px; width: 120px;">
                                &nbsp;
                            </td>
                            <td style="width: 5px; height: 26px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
                            <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgDadosIndicador"
                                OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                            </dxpgwx:ASPxPivotGridExporter>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" 
                                style="width: 100%; padding-bottom: 5px;">
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
                        <td valign="top">
                        </td>
                        <td colspan="1" valign="top" >
                            
                            
                            
                            <div id="divPivotGrid" runat="server">
                            <dxwpg:ASPxPivotGrid ID="pvgDadosIndicador" runat="server" ClientInstanceName="pvgDadosIndicador"
                                OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText" OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort"
                                OnCustomSummary="pvgDadosIndicador_CustomSummary" OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged"
                                Width="100%"  ClientIDMode="AutoID" EncodeHtml="False">
                                <Fields>
                                    <dxwpg:PivotGridField FieldName="Periodo" ID="fldPeriodo" Area="RowArea" AreaIndex="2"
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Per&#237;odo" 
                                        SortMode="Custom" Options-AllowSort="False" Options-AllowSortBySummary="True">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Meta" ID="fldMeta" Area="DataArea" 
                                        AreaIndex="0" AllowedAreas="DataArea" Caption="Meta Período" SummaryType="Custom"
                                        CellFormat-FormatType="Numeric" UseNativeFormat="True" Width="350">
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Resultado" ID="fldResultado" Area="DataArea" 
                                        AreaIndex="1" AllowedAreas="DataArea" Caption="Resultado Período" SummaryType="Custom"
                                        CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                        <CellStyle HorizontalAlign="Right" >
                                        </CellStyle>
                                        <HeaderStyle ></HeaderStyle>
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Desempenho" ID="fldDesempenho" Width="90" ExportBestFit="False"
                                        Area="DataArea" AreaIndex="2" AllowedAreas="DataArea" Caption="Status Período"
                                        SummaryType="Custom" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;"
                                        CellFormat-FormatType="Custom" ValueFormat-FormatString="Custom &quot;&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;&quot;"
                                        ValueFormat-FormatType="Custom" UseNativeFormat="True">
                                        <CellStyle HorizontalAlign="Center" >
                                        </CellStyle>
                                        <HeaderStyle ></HeaderStyle>
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Indicador" ID="fldIndicador" Area="RowArea" AreaIndex="2"
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Indicador" Visible="False">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Projeto" ID="fldProjeto" Area="RowArea" AreaIndex="0"
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Projeto">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="MetaAcumulada" ID="fldMetaAcumulada" Area="DataArea"
                                        AreaIndex="3" AllowedAreas="DataArea" Caption="Meta Acumulada" SummaryType="Custom"
                                        CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="ResultadoAcumulado" ID="fldResultadoAcumulado" Area="DataArea"
                                        AreaIndex="4" AllowedAreas="DataArea" Caption="Resultado Acumulado" SummaryType="Custom"
                                        CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="DesempenhoAcumulado" ID="fldDesempenhoAcumulado"
                                        Width="90" ExportBestFit="False" Area="DataArea" AreaIndex="5" AllowedAreas="DataArea"
                                        Caption="Status Acumulado" SummaryType="Custom" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;"
                                        CellFormat-FormatType="Custom" ValueFormat-FormatString="Custom &quot;&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;&quot;"
                                        ValueFormat-FormatType="Custom" UseNativeFormat="True" 
                                        KPIGraphic="TrafficLights">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="StatusProjeto" ID="fldStatusProjeto" Area="RowArea"
                                        AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Caption="Status Projeto">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="GerenteProjeto" ID="fldGerenteProjeto" Area="RowArea"
                                        AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Caption="Gerente">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" Area="RowArea" AreaIndex="2"
                                        Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Unidade">
                                        <ValueStyle >
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldMetaDescritiva" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="1" Caption="Meta" FieldName="MetaDescritiva">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fldPrograma" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="RowArea" AreaIndex="2" Caption="Programa" FieldName="Programa" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fldAno" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="ColumnArea" AreaIndex="0" Caption="Ano" FieldName="Ano">
                                    </dxwpg:PivotGridField>
                                </Fields>
                                <OptionsPager RowsPerPage="15">
                                </OptionsPager>
                                <Styles>
                                    <HeaderStyle ></HeaderStyle>
                                    <AreaStyle >
                                    </AreaStyle>
                                    <DataAreaStyle >
                                    </DataAreaStyle>
                                    <ColumnAreaStyle >
                                    </ColumnAreaStyle>
                                    <RowAreaStyle >
                                    </RowAreaStyle>
                                    <CellStyle >
                                    </CellStyle>
                                    <TotalCellStyle >
                                    </TotalCellStyle>
                                    <GrandTotalCellStyle >
                                    </GrandTotalCellStyle>
                                    <CustomTotalCellStyle >
                                    </CustomTotalCellStyle>
                                    <FilterWindowStyle >
                                    </FilterWindowStyle>
                                    <MenuStyle ></MenuStyle>
                                    <CustomizationFieldsStyle >
                                    </CustomizationFieldsStyle>
                                    <CustomizationFieldsContentStyle >
                                    </CustomizationFieldsContentStyle>
                                    <CustomizationFieldsHeaderStyle >
                                    </CustomizationFieldsHeaderStyle>
                                </Styles>
                                <OptionsView ShowColumnTotals="False" ShowRowTotals="False" ShowColumnGrandTotals="False"
                                    ShowRowGrandTotals="False" HeaderHeightOffset="1" HeaderWidthOffset="1"></OptionsView>
                                <StylesPager>
                                    <Pager >
                                    </Pager>
                                </StylesPager>
                                <StylesEditors>
                                    <DropDownWindow >
                                    </DropDownWindow>
                                </StylesEditors>
                            </dxwpg:ASPxPivotGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
