<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relAnaliseProcessosReprojetados.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_relAnaliseProcessosReprojetados"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 10px">
    <form id="form1" runat="server">

        <table>
            <tr>
                <td>
                    <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgDadosProjeto"
                        OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                    </dxpgwx:ASPxPivotGridExporter>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
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
        <div id="Div1" style="overflow: auto; height: <%=alturaTabela %>; width: 100%">

            <dxwpg:ASPxPivotGrid ID="pvgDadosProjeto" runat="server" ClientInstanceName="pvgDadosProjeto"
                Width="100%" OnCustomSummary="pvgDadosIndicador_CustomSummary"
                OnCustomCellStyle="pvgDadosProjeto_CustomCellStyle" EnableViewState="False"
                OnCustomCellDisplayText="pvgDadosProjeto_CustomCellDisplayText" ClientIDMode="AutoID"
                OnCustomFilterPopupItems="pvgDadosProjeto_CustomFilterPopupItems">
                <Fields>
                    <dxwpg:PivotGridField ID="f_NomeProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" Caption="Projeto" FieldName="NomeProjeto" AreaIndex="0">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_SiglaUnidade" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" Caption="Unidade de Neg&#243;cio" FieldName="SiglaUnidade" Width="40"
                        Visible="False" AreaIndex="2">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_NomeGerenteUnidade" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" Caption="Gerente da Unidade" FieldName="NomeGerenteUnidade" Visible="False"
                        AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_NomeGerenteProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" AreaIndex="0" Caption="Gerente do Projeto" FieldName="NomeGerenteProjeto"
                        Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_StatusProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" Caption="Status do Projeto" FieldName="StatusProjeto" AreaIndex="1"
                        Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_FaseProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="ColumnArea" Caption="Fase do Projeto" FieldName="FaseProjeto" Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_CustoReal" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Real"
                        FieldName="CustoReal" AreaIndex="2" Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_CustoPrevistoTotal" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Despesa Prevista Total" FieldName="CustoPrevistoTotal" AreaIndex="0"
                        Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_CustoPrevistoData" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Despesa Prevista At&#233; a Data" FieldName="CustoPrevistoData" Visible="False"
                        AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_Custo" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Tend&#234;ncia"
                        FieldName="Custo" AreaIndex="0" Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_CustoHE" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa (Horas Extras)"
                        FieldName="CustoHE" AreaIndex="3" Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_TRabalhoReal" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Trabalho Real" FieldName="TRabalhoReal" AreaIndex="3" CellFormat-FormatType="Numeric"
                        Visible="False" CellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatString="#,##0,00"
                        GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0,00"
                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0,00"
                        TotalValueFormat-FormatType="Numeric">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_TrabalhoPrevistoTotal" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Trabalho Previsto Total" FieldName="TrabalhoPrevistoTotal" Visible="False"
                        CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0,00"
                        GrandTotalCellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatType="Numeric"
                        TotalCellFormat-FormatString="#,##0,00" TotalCellFormat-FormatType="Numeric"
                        TotalValueFormat-FormatString="#,##0,00" TotalValueFormat-FormatType="Numeric">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_TrabalhoPrevistoData" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Trabalho Previsto At&#233; a Data" FieldName="TrabalhoPrevistoData"
                        Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0,00"
                        GrandTotalCellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatType="Numeric"
                        TotalCellFormat-FormatString="#,##0,00" TotalCellFormat-FormatType="Numeric"
                        TotalValueFormat-FormatString="#,##0,00" TotalValueFormat-FormatType="Numeric">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_Trabalho" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Tend&#234;ncia"
                        FieldName="Trabalho" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3"
                        CellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatString="#,##0,00"
                        GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0,00"
                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0,00"
                        TotalValueFormat-FormatType="Numeric">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_TrabalhoHE" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho (Horas Extras)"
                        FieldName="TrabalhoHE" AreaIndex="3" CellFormat-FormatType="Numeric" Visible="False"
                        CellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatString="#,##0,00"
                        GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0,00"
                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0,00"
                        TotalValueFormat-FormatType="Numeric">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_ValorPlanejado" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Valor Planejado" FieldName="ValorPlanejado" Visible="False" AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_ValorAgregado" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Valor Agregado" FieldName="ValorAgregado" Visible="False" AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_ReceitaRealTotal" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Receita Real" FieldName="ReceitaRealTotal" Visible="False" AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_ReceitaPrevistaTotal" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Receita Prevista Total" FieldName="ReceitaPrevistaTotal" Visible="False"
                        AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_ReceitaPrevistaData" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Receita Prevista At&#233; a Data" FieldName="ReceitaPrevistaData" Visible="False"
                        AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_Receita" AllowedAreas="DataArea" Area="DataArea" Caption="Receita"
                        FieldName="Receita" Visible="False" AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_EscoreCriterios" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Escore Crit&#233;rios" FieldName="EscoreCriterios" Visible="False" CellFormat-FormatType="Numeric"
                        AreaIndex="3" SummaryType="Average" CellFormat-FormatString="#,##0.00" GrandTotalCellFormat-FormatString="#,##0.00"
                        GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.00"
                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.00"
                        TotalValueFormat-FormatType="Numeric">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_EscoreRiscos" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Escore Riscos" FieldName="EscoreRiscos" Visible="False" CellFormat-FormatType="Numeric"
                        AreaIndex="3" SummaryType="Average" CellFormat-FormatString="#,##0.00" GrandTotalCellFormat-FormatString="#,##0.00"
                        GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.00"
                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.00"
                        TotalValueFormat-FormatType="Numeric">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_VariacaoTrabalho" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Varia&#231;&#227;o do Trabalho" FieldName="VariacaoTrabalho" Visible="False"
                        AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_VariacaoCusto" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Varia&#231;&#227;o do Custo" FieldName="VariacaoCusto" AreaIndex="0"
                        CellFormat-FormatString="{0:C2}" CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:C2}"
                        GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:C2}"
                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:C2}" TotalValueFormat-FormatType="Numeric"
                        Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_VariacaoReceita" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Varia&#231;&#227;o da Receita" FieldName="VariacaoReceita" Visible="False"
                        AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="f_EstimativaCustoConcluir" AllowedAreas="DataArea" Area="DataArea"
                        Caption="Despesa ao Concluir (EAC)" FieldName="EstimativaCustoConcluir" Visible="False"
                        AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="field4" AllowedAreas="DataArea" Area="DataArea" AreaIndex="4"
                        Caption="&#205;ndice de Desempenho do Prazo" CellFormat-FormatString="#,##0.0%"
                        CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0.0%"
                        GrandTotalCellFormat-FormatType="Numeric" SummaryType="Custom" TotalCellFormat-FormatString="#,##0.0%"
                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0%"
                        TotalValueFormat-FormatType="Numeric" UnboundExpression="[ValorAgregado] / [ValorPlanejado]"
                        UnboundFieldName="IndiceDesempenhoPrazo" UnboundType="Decimal" Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="field5" AllowedAreas="DataArea" Area="DataArea" Caption="&#205;ndice de Desempenho do Custo"
                        CellFormat-FormatString="#,##0.0%" CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0.0%"
                        GrandTotalCellFormat-FormatType="Numeric" SummaryType="Custom" TotalCellFormat-FormatString="#,##0.0%"
                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0%"
                        TotalValueFormat-FormatType="Numeric" UnboundExpression="[ValorAgregado] / [CustoReal]"
                        UnboundFieldName="IndiceDesempenhoCusto" UnboundType="Decimal" Visible="False"
                        AreaIndex="3">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldCorGeral" Area="DataArea" Caption="Geral" FieldName="CorGeral"
                        AllowedAreas="DataArea" AreaIndex="0" CellFormat-FormatString="&lt;img alt='' src='../../../../imagens/{0}Menor.gif' /&gt;"
                        CellFormat-FormatType="Custom" ExportBestFit="False" SummaryType="Custom" ValueFormat-FormatString="&lt;img alt='' src='../../../../imagens/{0}Menor.gif' /&gt;"
                        ValueFormat-FormatType="Custom" Width="20">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldCorFisico" Area="DataArea" Caption="Físico" FieldName="CorFisico"
                        AllowedAreas="DataArea" AreaIndex="1" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;"
                        CellFormat-FormatType="Custom" ExportBestFit="False" SummaryType="Custom" ValueFormat-FormatString="&lt;img alt='' src='../../../../imagens/{0}Menor.gif' /&gt;"
                        ValueFormat-FormatType="Custom" Width="20">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldCorFinanceiro" Area="DataArea" Caption="Financ." FieldName="CorFinanceiro"
                        AllowedAreas="DataArea" AreaIndex="2" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;"
                        CellFormat-FormatType="Custom" ExportBestFit="False" SummaryType="Custom" ValueFormat-FormatString="&lt;img alt='' src='../../../../imagens/{0}Menor.gif' /&gt;"
                        ValueFormat-FormatType="Custom" Width="20">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldPercentualPrevistoRealizacao" Area="DataArea" Caption="Percentual Previsto"
                        FieldName="PercentualPrevistoRealizacao" AllowedAreas="DataArea" AreaIndex="3"
                        CellFormat-FormatString="p" CellFormat-FormatType="Numeric" SummaryType="Average">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldPercentualRealizacao" Area="DataArea" Caption="Percentual Realizado"
                        FieldName="PercentualRealizacao" AllowedAreas="DataArea" AreaIndex="4" CellFormat-FormatString="p"
                        CellFormat-FormatType="Numeric" SummaryType="Average">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldUltimaAtualizacao" Area="RowArea" Caption="Última Atualização"
                        FieldName="UltimaAtualizacao" AllowedAreas="RowArea" AreaIndex="1" Width="37">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldRiscosAtivos" Area="DataArea" Caption="Riscos Ativos"
                        FieldName="RiscosAtivos" AllowedAreas="DataArea" AreaIndex="5" Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldProblemasAtivos" Area="DataArea" Caption="Problemas Ativos"
                        FieldName="ProblemasAtivos" AllowedAreas="DataArea" AreaIndex="5" Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldAtrasosCronograma" Area="DataArea" Caption="Atrasos no Cronograma"
                        FieldName="AtrasosCronograma" AllowedAreas="DataArea" AreaIndex="5">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldEntregasRealizadas" Area="DataArea" Caption="Entregas Realizadas"
                        FieldName="EntregasRealizadas" AllowedAreas="DataArea" AreaIndex="6">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldEntregasAtrasadas" Area="DataArea" Caption="Entregas Atrasadas"
                        FieldName="EntregasAtrasadas" AllowedAreas="DataArea" AreaIndex="7">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldProximasEntregas" Area="DataArea" Caption="Entregas nos próx. 30 dias"
                        FieldName="ProximasEntregas" AllowedAreas="DataArea" AreaIndex="8" Visible="False">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldTipoProjeto" Area="RowArea" AreaIndex="2" FieldName="TipoProjeto"
                        Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField ID="fieldUnidadeAtendimento" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" AreaIndex="2" Caption="Unidade de Atendimento" FieldName="NomeUnidadeAtendimento"
                        Visible="False">
                    </dxwpg:PivotGridField>
                </Fields>
                <OptionsCustomization CustomizationWindowWidth="280" />
                <OptionsPager Visible="False">
                </OptionsPager>
                <OptionsFilter ShowOnlyAvailableItems="True" />
                <Styles>
                    <HeaderStyle />
                    <AreaStyle>
                    </AreaStyle>
                    <FilterAreaStyle>
                    </FilterAreaStyle>
                    <CellStyle>
                    </CellStyle>
                    <FieldValueStyle>
                    </FieldValueStyle>
                    <GrandTotalCellStyle>
                    </GrandTotalCellStyle>
                    <CustomTotalCellStyle>
                    </CustomTotalCellStyle>
                    <MenuItemStyle />
                    <MenuStyle />
                    <CustomizationFieldsStyle>
                    </CustomizationFieldsStyle>
                    <CustomizationFieldsHeaderStyle>
                    </CustomizationFieldsHeaderStyle>
                    <LoadingPanel>
                    </LoadingPanel>
                </Styles>
                <OptionsLoadingPanel Text="Carregando&amp;hellip;">
                    <Style></Style>
                </OptionsLoadingPanel>
                <StylesEditors>
                    <DropDownWindow>
                    </DropDownWindow>
                </StylesEditors>
            </dxwpg:ASPxPivotGrid>
        </div>
    </form>
</body>
</html>
