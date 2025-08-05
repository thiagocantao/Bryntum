<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseIndicadoresNovo.aspx.cs" Inherits="_Projetos_Relatorios_relAnaliseIndicadoresNovo"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div style="font-family: Verdana; font-size: 8pt;">
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height:26px">
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle" style="padding-left: 10px">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Names="Verdana" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                                    Text="Análise de Indicadores de Projetos"></asp:Label>
                            </td>
                         </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" 
                                style="width: 100%; padding-bottom: 5px; padding-top: 5px; padding-left: 10px;">
                                <tr>
                                    <td style="width: 205px">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxComboBox ID="ddlExporta" runat="server" ClientInstanceName="ddlExporta"
                                                        Font-Names="Verdana" Font-Size="8pt" ValueType="System.String">
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
                                        <dxe:ASPxButton ID="Aspxbutton1" runat="server" Font-Names="Verdana" Font-Size="8pt"
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
                </table>
            </td>
        </tr>
    </table>
    <div style="padding: 10px"><dxwpg:ASPxPivotGrid ID="pvgDadosIndicador" runat="server" ClientInstanceName="pvgDadosIndicador"
                                OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText" OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort"
                                OnCustomSummary="pvgDadosIndicador_CustomSummary" OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged"
                                Width="100%" Font-Names="Verdana" Font-Size="8pt" ClientIDMode="AutoID" EncodeHtml="False">
                                <Fields>
                                    <dxwpg:PivotGridField FieldName="Periodo" ID="fldPeriodo" Area="RowArea" AreaIndex="2"
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Per&#237;odo" 
                                        SortMode="Custom" Options-AllowSort="False" Options-AllowSortBySummary="True">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Meta" ID="fldMeta" Area="DataArea" 
                                        AreaIndex="0" AllowedAreas="DataArea" Caption="Meta Período" SummaryType="Custom"
                                        CellFormat-FormatType="Numeric" UseNativeFormat="True" Width="350">
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Resultado" ID="fldResultado" Area="DataArea" 
                                        AreaIndex="1" AllowedAreas="DataArea" Caption="Resultado Período" SummaryType="Custom"
                                        CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                        <CellStyle HorizontalAlign="Right" Font-Names="Verdana" Font-Size="8pt">
                                        </CellStyle>
                                        <HeaderStyle Font-Names="Verdana" Font-Size="8pt"></HeaderStyle>
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Desempenho" ID="fldDesempenho" Width="90" ExportBestFit="False"
                                        Area="DataArea" AreaIndex="2" AllowedAreas="DataArea" Caption="Status Período"
                                        SummaryType="Custom" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;"
                                        CellFormat-FormatType="Custom" ValueFormat-FormatString="Custom &quot;&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;&quot;"
                                        ValueFormat-FormatType="Custom" UseNativeFormat="True">
                                        <CellStyle HorizontalAlign="Center" Font-Names="Verdana" Font-Size="8pt">
                                        </CellStyle>
                                        <HeaderStyle Font-Names="Verdana" Font-Size="8pt"></HeaderStyle>
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Indicador" ID="fldIndicador" Area="RowArea" AreaIndex="2"
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Indicador" Visible="False">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Projeto" ID="fldProjeto" Area="RowArea" AreaIndex="0"
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Projeto">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="MetaAcumulada" ID="fldMetaAcumulada" Area="DataArea"
                                        AreaIndex="3" AllowedAreas="DataArea" Caption="Meta Acumulada" SummaryType="Custom"
                                        CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="ResultadoAcumulado" ID="fldResultadoAcumulado" Area="DataArea"
                                        AreaIndex="4" AllowedAreas="DataArea" Caption="Resultado Acumulado" SummaryType="Custom"
                                        CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="DesempenhoAcumulado" ID="fldDesempenhoAcumulado"
                                        Width="90" ExportBestFit="False" Area="DataArea" AreaIndex="5" AllowedAreas="DataArea"
                                        Caption="Status Acumulado" SummaryType="Custom" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;"
                                        CellFormat-FormatType="Custom" ValueFormat-FormatString="Custom &quot;&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;&quot;"
                                        ValueFormat-FormatType="Custom" UseNativeFormat="True" 
                                        KPIGraphic="TrafficLights">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="StatusProjeto" ID="fldStatusProjeto" Area="RowArea"
                                        AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Caption="Status Projeto">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="GerenteProjeto" ID="fldGerenteProjeto" Area="RowArea"
                                        AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Caption="Gerente">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" Area="RowArea" AreaIndex="2"
                                        Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Unidade">
                                        <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                        </ValueStyle>
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldMetaDescritiva" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="1" Caption="Meta" FieldName="MetaDescritiva">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fldAno" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="ColumnArea" AreaIndex="0" Caption="Ano" FieldName="Ano">
                                    </dxwpg:PivotGridField>
                                    <dxpgwx:PivotGridField ID="fieldPrograma" AllowedAreas="RowArea, FilterArea" Area="RowArea" AreaIndex="0" FieldName="Programa" Visible="False">
                                    </dxpgwx:PivotGridField>
                                    <dxpgwx:PivotGridField ID="fieldNomeFocoEstrategico" AllowedAreas="RowArea, FilterArea" AreaIndex="0" Caption="Foco" FieldName="NomeFocoEstrategico">
                                    </dxpgwx:PivotGridField>
                                    <dxpgwx:PivotGridField ID="fieldDescricaoDirecionadorEstrategico" AllowedAreas="RowArea, FilterArea" AreaIndex="1" Caption="Direcionador" FieldName="DescricaoDirecionadorEstrategico">
                                    </dxpgwx:PivotGridField>
                                    <dxpgwx:PivotGridField ID="fieldDescricaoDesafio" AllowedAreas="RowArea, FilterArea" AreaIndex="2" Caption="Grande Desafio" FieldName="DescricaoDesafio">
                                    </dxpgwx:PivotGridField>
                                    <dxpgwx:PivotGridField ID="fieldTipoProjeto" AllowedAreas="RowArea, FilterArea" AreaIndex="3" Caption="Tipo" FieldName="TipoProjeto">
                                    </dxpgwx:PivotGridField>
                                </Fields>
                                <OptionsPager RowsPerPage="15" Position="Bottom">
                                </OptionsPager>
                                <Styles>
                                    <HeaderStyle Font-Names="Verdana" Font-Size="8pt"></HeaderStyle>
                                    <AreaStyle Font-Names="Verdana" Font-Size="8pt">
                                    </AreaStyle>
                                    <DataAreaStyle Font-Names="Verdana" Font-Size="8pt">
                                    </DataAreaStyle>
                                    <ColumnAreaStyle Font-Names="Verdana" Font-Size="8pt">
                                    </ColumnAreaStyle>
                                    <RowAreaStyle Font-Names="Verdana" Font-Size="8pt">
                                    </RowAreaStyle>
                                    <CellStyle Font-Names="Verdana" Font-Size="8pt">
                                    </CellStyle>
                                    <TotalCellStyle Font-Names="Verdana" Font-Size="8pt">
                                    </TotalCellStyle>
                                    <GrandTotalCellStyle Font-Names="Verdana" Font-Size="8pt">
                                    </GrandTotalCellStyle>
                                    <CustomTotalCellStyle Font-Names="Verdana" Font-Size="8pt">
                                    </CustomTotalCellStyle>
                                    <FilterWindowStyle Font-Names="Verdana" Font-Size="8pt">
                                    </FilterWindowStyle>
                                    <MenuStyle Font-Names="Verdana" Font-Size="8pt"></MenuStyle>
                                    <CustomizationFieldsStyle Font-Names="Verdana" Font-Size="8pt">
                                    </CustomizationFieldsStyle>
                                    <CustomizationFieldsContentStyle Font-Names="Verdana" Font-Size="8pt">
                                    </CustomizationFieldsContentStyle>
                                    <CustomizationFieldsHeaderStyle Font-Names="Verdana" Font-Size="8pt">
                                    </CustomizationFieldsHeaderStyle>
                                </Styles>
                                <OptionsView ShowColumnTotals="False" ShowRowTotals="False" ShowColumnGrandTotals="False"
                                    ShowRowGrandTotals="False" HeaderHeightOffset="1" HeaderWidthOffset="1"></OptionsView>
                                <StylesPager>
                                    <Pager Font-Names="Verdana" Font-Size="8pt">
                                    </Pager>
                                </StylesPager>
                                <StylesEditors>
                                    <DropDownWindow Font-Names="Verdana" Font-Size="8pt">
                                    </DropDownWindow>
                                </StylesEditors>
                            </dxwpg:ASPxPivotGrid></div>
    <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgDadosIndicador"
                                OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                            </dxpgwx:ASPxPivotGridExporter>
    
</asp:Content>
