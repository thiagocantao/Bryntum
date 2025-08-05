<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relAnaliseIndicadores.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_relAnaliseIndicadores"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 10px">
    <form id="form1" runat="server">
        <div style="width: 100%">
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td valign="top">
                                    <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgDadosIndicador"
                                        OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                                    </dxpgwx:ASPxPivotGridExporter>
                                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                    </dxhf:ASPxHiddenField>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" valign="top"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
                style="width: 100%">
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server" style="padding: 3px" valign="top">
                        <table cellpadding="0" cellspacing="0" style="height: 22px">
                            <tr>
                                <td>
                                    <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                        ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init"
                                        OnItemClick="menu_ItemClick">
                                        <Paddings Padding="0px" />
                                        <Items>
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
                                                    <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML"
                                                        ToolTip="Exportar para HTML">
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
                                            <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                <Items>
                                                    <dxm:MenuItem Name="btnSalvarLayout" Text="Salvar" ToolTip="Salvar Layout">
                                                        <Image IconID="save_save_16x16">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnRestaurarLayout" Text="Restaurar"
                                                        ToolTip="Restaurar Layout">
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
                    </td>
                </tr>
            </table>
            <div id="divPivotGrid" runat="server" style="width: 100%; overflow: auto">

                <dxwpg:ASPxPivotGrid ID="pvgDadosIndicador" runat="server" ClientInstanceName="pvgDadosIndicador"
                    OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText" OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort"
                    OnCustomSummary="pvgDadosIndicador_CustomSummary" OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged"
                    Width="100%" ClientIDMode="AutoID" OnHtmlCellPrepared="pvgDadosIndicador_HtmlCellPrepared" OnHtmlFieldValuePrepared="pvgDadosIndicador_HtmlFieldValuePrepared">
                    <Fields>
                        <dxwpg:PivotGridField FieldName="Periodo" ID="fldPeriodo" Area="RowArea" AreaIndex="2"
                            AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Per&#237;odo" SortMode="Custom"
                            Options-AllowSort="False" Options-AllowSortBySummary="True">
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="Meta" ID="fldMeta" Area="DataArea" AreaIndex="0"
                            AllowedAreas="DataArea" Caption="Meta Período" SummaryType="Custom" CellFormat-FormatType="Numeric"
                            UseNativeFormat="True" Width="350">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="Resultado" ID="fldResultado" Area="DataArea" AreaIndex="1"
                            AllowedAreas="DataArea" Caption="Resultado Período" SummaryType="Custom" CellFormat-FormatType="Numeric"
                            UseNativeFormat="True">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <HeaderStyle></HeaderStyle>
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="Desempenho" ID="fldDesempenho" Width="90" ExportBestFit="False"
                            Area="DataArea" AreaIndex="2" AllowedAreas="DataArea" Caption="Status Período"
                            SummaryType="Custom" UseNativeFormat="True">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderStyle></HeaderStyle>
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="Indicador" ID="fldIndicador" Area="RowArea" AreaIndex="2"
                            AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Indicador" Visible="False">
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="Projeto" ID="fldProjeto" Area="RowArea" AreaIndex="0"
                            AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Projeto">
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="MetaAcumulada" ID="fldMetaAcumulada" Area="DataArea"
                            AreaIndex="3" AllowedAreas="DataArea" Caption="Meta Acumulada" SummaryType="Custom"
                            CellFormat-FormatType="Numeric" UseNativeFormat="True">
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="ResultadoAcumulado" ID="fldResultadoAcumulado" Area="DataArea"
                            AreaIndex="4" AllowedAreas="DataArea" Caption="Resultado Acumulado" SummaryType="Custom"
                            CellFormat-FormatType="Numeric" UseNativeFormat="True">
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="DesempenhoAcumulado" ID="fldDesempenhoAcumulado"
                            Width="90" ExportBestFit="False" Area="DataArea" AreaIndex="5" AllowedAreas="DataArea"
                            Caption="Status Acumulado" SummaryType="Custom" UseNativeFormat="True"
                            KPIGraphic="TrafficLights">
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="StatusProjeto" ID="fldStatusProjeto" Area="RowArea"
                            AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                            Caption="Status Projeto">
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="GerenteProjeto" ID="fldGerenteProjeto" Area="RowArea"
                            AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                            Caption="Gerente">
                            <ValueStyle>
                            </ValueStyle>
                        </dxwpg:PivotGridField>
                        <dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" Area="RowArea" AreaIndex="2"
                            Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Unidade">
                            <ValueStyle>
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
                    <OptionsFilter ShowOnlyAvailableItems="True" />
                    <Styles>
                        <HeaderStyle></HeaderStyle>
                        <AreaStyle>
                        </AreaStyle>
                        <DataAreaStyle>
                        </DataAreaStyle>
                        <ColumnAreaStyle>
                        </ColumnAreaStyle>
                        <RowAreaStyle>
                        </RowAreaStyle>
                        <CellStyle>
                        </CellStyle>
                        <TotalCellStyle>
                        </TotalCellStyle>
                        <GrandTotalCellStyle>
                        </GrandTotalCellStyle>
                        <CustomTotalCellStyle>
                        </CustomTotalCellStyle>
                        <FilterWindowStyle>
                        </FilterWindowStyle>
                        <MenuStyle></MenuStyle>
                        <CustomizationFieldsStyle>
                        </CustomizationFieldsStyle>
                        <CustomizationFieldsContentStyle>
                        </CustomizationFieldsContentStyle>
                        <CustomizationFieldsHeaderStyle>
                        </CustomizationFieldsHeaderStyle>
                    </Styles>
                    <OptionsView ShowColumnTotals="False" ShowRowTotals="False" ShowColumnGrandTotals="False"
                        ShowRowGrandTotals="False" HeaderHeightOffset="1" HeaderWidthOffset="1"></OptionsView>
                    <StylesPager>
                        <Pager>
                        </Pager>
                    </StylesPager>
                    <StylesEditors>
                        <DropDownWindow>
                        </DropDownWindow>
                    </StylesEditors>
                </dxwpg:ASPxPivotGrid>
            </div>
        </div>
    </form>
</body>
</html>
