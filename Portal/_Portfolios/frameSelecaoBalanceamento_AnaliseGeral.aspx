<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_AnaliseGeral.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_AnaliseGeral" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript">
        function atualizaDados()
        {            
            pnCallback.PerformCallback();
        }
    </script>
</head>
<body style="margin-top:0; overflow:hidden">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td id='tdOlap'>
                <div style="overflow:auto; height:<%=alturaTela %>; width:<%=larguraTela %>">
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server"><dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral"></dxhf:ASPxHiddenField>
 <table>
     <tr>
         <td>
         </td>
     </tr>
     <tr>
         <td>
             <dxwpg:ASPxPivotGrid ID="pvgDadosProjeto" runat="server"
                                    ClientInstanceName="pvgDadosProjeto"
                                    OnCustomCellStyle="pvgDadosProjeto_CustomCellStyle" OnCustomSummary="pvgDadosProjeto_CustomSummary"
                                    Width="100%"><Fields>
<dxwpg:PivotGridField ID="f_NomeProjeto" AllowedAreas="RowArea, FilterArea" Area="RowArea"
                                            AreaIndex="2" Caption="Projeto" FieldName="NomeProjeto"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_Categoria" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="0" Caption="Categoria" FieldName="Categoria"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_SiglaUnidade" AllowedAreas="RowArea" Area="RowArea" AreaIndex="1"
                                            Caption="Unidade Neg&#243;cio" FieldName="SiglaUnidade" Width="40"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_NomeGerenteUnidade" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="1" Caption="Gerente Unidade" FieldName="NomeGerenteUnidade"
                                            Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_NomeGerenteProjeto" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="0" Caption="Gerente Projeto" FieldName="NomeGerenteProjeto"
                                            Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_StatusProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                            AreaIndex="3" Caption="Status do Projeto" FieldName="StatusProjeto"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_CustoReal" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0"
                                            Caption="Despesa Real" FieldName="CustoReal"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_CustoPrevistoTotal" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="1" Caption="Despesa Prevista Total" FieldName="CustoPrevistoTotal"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_CustoPrevistoData" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Despesa Prevista At&#233; Data" FieldName="CustoPrevistoData"
                                            Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_CustoHE" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1"
                                            Caption="Despesa Horas Extras" FieldName="CustoHE" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_TRabalhoReal" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="2" Caption="Trabalho Real" CellFormat-FormatString="#,##0.0" CellFormat-FormatType="Numeric"
                                            FieldName="TRabalhoReal" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0"
                                            TotalValueFormat-FormatType="Numeric" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_TrabalhoPrevistoTotal" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Trabalho Previsto Total" CellFormat-FormatString="#,##0.0"
                                            CellFormat-FormatType="Numeric" FieldName="TrabalhoPrevistoTotal" GrandTotalCellFormat-FormatString="#,##0.0"
                                            GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0"
                                            TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0"
                                            TotalValueFormat-FormatType="Numeric" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_TrabalhoPrevistoData" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Trabalho Previsto at&#233; a Data" CellFormat-FormatString="#,##0.0"
                                            CellFormat-FormatType="Numeric" FieldName="TrabalhoPrevistoData" GrandTotalCellFormat-FormatString="#,##0.0"
                                            GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0"
                                            TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0"
                                            TotalValueFormat-FormatType="Numeric" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_Trabalho" AllowedAreas="DataArea" Area="DataArea" AreaIndex="3"
                                            Caption="Trabalho Tend&#234;ncia" CellFormat-FormatString="#,##0.0" CellFormat-FormatType="Numeric"
                                            FieldName="Trabalho" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0"
                                            TotalValueFormat-FormatType="Numeric" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_TrabalhoHE" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2"
                                            Caption="Trabalho Horas Extras" CellFormat-FormatString="#,##0.0" CellFormat-FormatType="Numeric"
                                            FieldName="TrabalhoHE" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0"
                                            TotalValueFormat-FormatType="Numeric" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_ValorPlanejado" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Valor Planejado" FieldName="ValorPlanejado" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_ValorAgregado" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Valor Agregado" FieldName="ValorAgregado" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_ReceitaRealTotal" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Receita Real Total" FieldName="ReceitaRealTotal" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_ReceitaPrevistaTotal" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Receita Prevista Total" FieldName="ReceitaPrevistaTotal"
                                            Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_ReceitaPrevistaData" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Receita Prevista at&#233; a Data" FieldName="ReceitaPrevistaData"
                                            Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_EscoreCriterios" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Escore Criterios" CellFormat-FormatString="#,##0.0" CellFormat-FormatType="Numeric"
                                            FieldName="EscoreCriterios" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0"
                                            TotalValueFormat-FormatType="Numeric" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_EscoreRiscos" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Escore Riscos" CellFormat-FormatString="#,##0.0" CellFormat-FormatType="Numeric"
                                            FieldName="EscoreRiscos" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0"
                                            TotalValueFormat-FormatType="Numeric" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_VariacaoTrabalho" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Variacao Trabalho" FieldName="VariacaoTrabalho" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="f_EstimativaCustoConcluir" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Estimativa Despesa Concluir" FieldName="EstimativaCustoConcluir"
                                            Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="field4" AllowedAreas="DataArea" Area="DataArea" AreaIndex="3"
                                            Caption="&#205;ndice de Desempenho de Prazo" CellFormat-FormatString="#,##0.0%"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0.0%"
                                            GrandTotalCellFormat-FormatType="Numeric" SummaryType="Custom" TotalCellFormat-FormatString="#,##0.0%"
                                            TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0%"
                                            TotalValueFormat-FormatType="Numeric" UnboundExpression="[ValorAgregado] / [ValorPlanejado]"
                                            UnboundFieldName="IndiceDesempenhoPrazo" UnboundType="Decimal" Visible="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField ID="field5" AllowedAreas="DataArea" Area="DataArea" AreaIndex="3"
                                            Caption="&#205;ndice de Desempenho de Custo" CellFormat-FormatString="#,##0.0%"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0.0%"
                                            GrandTotalCellFormat-FormatType="Numeric" SummaryType="Custom" TotalCellFormat-FormatString="#,##0.0%"
                                            TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0%"
                                            TotalValueFormat-FormatType="Numeric" UnboundExpression="[ValorAgregado] / [CustoReal]"
                                            UnboundFieldName="IndiceDesempenhoCusto" UnboundType="Decimal" Visible="False"></dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                            AreaIndex="0" Caption="Associado a Portf&#243;lio" FieldName="ProjetoAssociadoPortfolio">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="3" Caption="Desempenho" FieldName="Desempenho" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field2" AllowedAreas="RowArea" Area="RowArea" AreaIndex="3"
                                            Caption="% Realizado" CellFormat-FormatString="{0:p0}" CellFormat-FormatType="Custom"
                                            FieldName="PercentualRealizacao" ValueFormat-FormatString="{0:p0}" ValueFormat-FormatType="Custom"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
</Fields>

<OptionsPager Visible="False"></OptionsPager>

<OptionsLoadingPanel Text="Carregando&amp;hellip;">
<Style ></Style>
</OptionsLoadingPanel>

<Styles>
<HeaderStyle  />

<AreaStyle ></AreaStyle>

<FilterAreaStyle ></FilterAreaStyle>

<FieldValueStyle ></FieldValueStyle>

<CellStyle ></CellStyle>

<GrandTotalCellStyle ></GrandTotalCellStyle>

<CustomTotalCellStyle ></CustomTotalCellStyle>

<MenuItemStyle  />

<MenuStyle  />

<CustomizationFieldsStyle ></CustomizationFieldsStyle>

<CustomizationFieldsHeaderStyle ></CustomizationFieldsHeaderStyle>

<LoadingPanel ></LoadingPanel>
</Styles>

<StylesEditors>
<DropDownWindow ></DropDownWindow>
</StylesEditors>
</dxwpg:ASPxPivotGrid>
         </td>
     </tr>
 </table>&nbsp; </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
	defineCaminhoGrafico();
}" />
                    </dxcp:ASPxCallbackPanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td id='tdGrafico' style="display: none" >
                    <iframe name="framePrincipal" src="frameProposta_GraficoRH.aspx?DefineAltura=S&CRH=-1" width="100%" scrolling="no"
                                frameborder="0" style="height: <%=alturaTela %>"></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>