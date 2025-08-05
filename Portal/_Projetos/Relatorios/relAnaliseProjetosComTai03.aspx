<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relAnaliseProjetosComTai03.aspx.cs" Inherits="_Projetos_Relatorios_relAnaliseProjetosComTai03" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
     <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height:26px">
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle" style="padding-left: 10px">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Projetos"></asp:Label></td>
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
                        </tr>
                    </table>
                    <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgDadosProjeto" OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                    </dxpgwx:ASPxPivotGridExporter>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 10px; height: 14px" valign="top">
                        </td>
                        <td valign="top">
                        </td>
                        <td style="width: 10px; height: 14px">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
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
                                                            <dxp:PanelContent ID="PanelContent1" runat="server">
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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td style="height: 5px;" valign="top">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td colspan="1" valign="top">
                        <div id="Div1" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                            <dxwpg:ASPxPivotGrid ID="pvgDadosProjeto" runat="server" ClientInstanceName="pvgDadosProjeto"
                                Width="99%" OnCustomSummary="pvgDadosIndicador_CustomSummary" 
                                 
                                OnCustomCellStyle="pvgDadosProjeto_CustomCellStyle" 
                                oncustomcelldisplaytext="pvgDadosProjeto_CustomCellDisplayText" 
                                ClientIDMode="AutoID" 
                                oncustomfilterpopupitems="pvgDadosProjeto_CustomFilterPopupItems">
                                <Fields>
                                    <dxwpg:PivotGridField ID="f_NomeProjeto" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        Caption="Projeto" FieldName="NomeProjeto" AreaIndex="0">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_SiglaUnidade" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        Caption="Unidade de Neg&#243;cio" FieldName="SiglaUnidade" Width="40" 
                                        Visible="False" AreaIndex="2">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_NomeGerenteUnidade" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" Caption="Gerente da Unidade" FieldName="NomeGerenteUnidade" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_NomeGerenteProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        AreaIndex="0" Caption="Gerente do Projeto" FieldName="NomeGerenteProjeto" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_StatusProjeto" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" 
                                        Caption="Status do Projeto" FieldName="StatusProjeto" AreaIndex="1" 
                                        Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_FaseProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" Caption="Fase do Projeto" FieldName="FaseProjeto" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoReal" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Real"
                                        FieldName="CustoReal" AreaIndex="2" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoPrevistoTotal" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Despesa Prevista Total"
                                        FieldName="CustoPrevistoTotal" AreaIndex="0" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoPrevistoData" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Prevista At&#233; a Data"
                                        FieldName="CustoPrevistoData" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Custo" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Tend&#234;ncia"
                                        FieldName="Custo" AreaIndex="0" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoHE" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa (Horas Extras)"
                                        FieldName="CustoHE" AreaIndex="3" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_TRabalhoReal" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Real"
                                        FieldName="TRabalhoReal" AreaIndex="3" CellFormat-FormatType="Numeric" Visible="False" CellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0,00" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0,00" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_TrabalhoPrevistoTotal" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Previsto Total"
                                        FieldName="TrabalhoPrevistoTotal" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0,00" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0,00" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_TrabalhoPrevistoData" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Previsto At&#233; a Data"
                                        FieldName="TrabalhoPrevistoData" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0,00" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0,00" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Trabalho" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Tend&#234;ncia"
                                        FieldName="Trabalho" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0,00" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0,00" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_TrabalhoHE" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho (Horas Extras)"
                                        FieldName="TrabalhoHE" AreaIndex="3" CellFormat-FormatType="Numeric" Visible="False" CellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatString="#,##0,00" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0,00" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0,00" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ValorPlanejado" AllowedAreas="DataArea" Area="DataArea" Caption="Valor Planejado"
                                        FieldName="ValorPlanejado" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ValorAgregado" AllowedAreas="DataArea" Area="DataArea" Caption="Valor Agregado"
                                        FieldName="ValorAgregado" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaRealTotal" AllowedAreas="DataArea" Area="DataArea" Caption="Receita Real"
                                        FieldName="ReceitaRealTotal" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaPrevistaTotal" AllowedAreas="DataArea" Area="DataArea" Caption="Receita Prevista Total"
                                        FieldName="ReceitaPrevistaTotal" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaPrevistaData" AllowedAreas="DataArea" Area="DataArea" Caption="Receita Prevista At&#233; a Data"
                                        FieldName="ReceitaPrevistaData" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Receita" AllowedAreas="DataArea" Area="DataArea" Caption="Receita"
                                        FieldName="Receita" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_EscoreCriterios" AllowedAreas="DataArea" Area="DataArea" Caption="Escore Crit&#233;rios"
                                        FieldName="EscoreCriterios" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" SummaryType="Average" CellFormat-FormatString="#,##0.00" GrandTotalCellFormat-FormatString="#,##0.00" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.00" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.00" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_EscoreRiscos" AllowedAreas="DataArea" Area="DataArea" Caption="Escore Riscos"
                                        FieldName="EscoreRiscos" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" SummaryType="Average" CellFormat-FormatString="#,##0.00" GrandTotalCellFormat-FormatString="#,##0.00" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.00" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.00" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_VariacaoTrabalho" AllowedAreas="DataArea" Area="DataArea" Caption="Varia&#231;&#227;o do Trabalho"
                                        FieldName="VariacaoTrabalho" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_VariacaoCusto" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Varia&#231;&#227;o do Custo"
                                        FieldName="VariacaoCusto" AreaIndex="0" CellFormat-FormatString="{0:C2}" 
                                        CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:C2}" 
                                        GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:C2}" 
                                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:C2}" 
                                        TotalValueFormat-FormatType="Numeric" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_VariacaoReceita" AllowedAreas="DataArea" Area="DataArea" Caption="Varia&#231;&#227;o da Receita"
                                        FieldName="VariacaoReceita" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_EstimativaCustoConcluir" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa ao Concluir (EAC)"
                                        FieldName="EstimativaCustoConcluir" Visible="False" AreaIndex="3">
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
                                        UnboundFieldName="IndiceDesempenhoCusto" UnboundType="Decimal" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldCorGeral" Area="DataArea" 
                                        Caption="Geral" FieldName="CorGeral" AllowedAreas="DataArea" AreaIndex="0" 
                                        CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" 
                                        CellFormat-FormatType="Custom" ExportBestFit="False" SummaryType="Custom" 
                                        ValueFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" 
                                        ValueFormat-FormatType="Custom" Width="20">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldCorFisico" Area="DataArea" 
                                        Caption="Físico" FieldName="CorFisico" AllowedAreas="DataArea" 
                                        AreaIndex="1" 
                                        CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" 
                                        CellFormat-FormatType="Custom" ExportBestFit="False" SummaryType="Custom" 
                                        ValueFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" 
                                        ValueFormat-FormatType="Custom" Width="20">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldCorFinanceiro" Area="DataArea" 
                                        Caption="Financ." FieldName="CorFinanceiro" AllowedAreas="DataArea" 
                                        AreaIndex="2" 
                                        CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" 
                                        CellFormat-FormatType="Custom" ExportBestFit="False" SummaryType="Custom" 
                                        ValueFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" 
                                        ValueFormat-FormatType="Custom" Width="20">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldPercentualPrevistoRealizacao" Area="DataArea" 
                                        Caption="Percentual Previsto" FieldName="PercentualPrevistoRealizacao" 
                                        AllowedAreas="DataArea" AreaIndex="3" CellFormat-FormatString="p" 
                                        CellFormat-FormatType="Numeric" SummaryType="Average">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldPercentualRealizacao" Area="DataArea" 
                                        Caption="Percentual Realizado" FieldName="PercentualRealizacao" 
                                        AllowedAreas="DataArea" AreaIndex="4" CellFormat-FormatString="p" 
                                        CellFormat-FormatType="Numeric" SummaryType="Average">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldUltimaAtualizacao" Area="RowArea" 
                                        Caption="Última Atualização" FieldName="UltimaAtualizacao" 
                                        AllowedAreas="RowArea" AreaIndex="1" Width="40">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldRiscosAtivos" Area="DataArea" 
                                        Caption="Riscos Ativos" FieldName="RiscosAtivos" AllowedAreas="DataArea" 
                                        AreaIndex="5" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldProblemasAtivos" Area="DataArea" 
                                        Caption="Problemas Ativos" FieldName="ProblemasAtivos" 
                                        AllowedAreas="DataArea" AreaIndex="5" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldAtrasosCronograma" Area="DataArea" 
                                        Caption="Atrasos no Cronograma" FieldName="AtrasosCronograma" 
                                        AllowedAreas="DataArea" AreaIndex="5" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldEntregasRealizadas" Area="DataArea" 
                                        Caption="Entregas Realizadas" FieldName="EntregasRealizadas" 
                                        AllowedAreas="DataArea" AreaIndex="5" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldEntregasAtrasadas" Area="DataArea" 
                                        Caption="Entregas Atrasadas" FieldName="EntregasAtrasadas" 
                                        AllowedAreas="DataArea" AreaIndex="5" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldProximasEntregas" Area="DataArea" 
                                        Caption="Entregas nos próx. 30 dias" FieldName="ProximasEntregas" 
                                        AllowedAreas="DataArea" AreaIndex="5" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldTipoProjeto" Area="RowArea" AreaIndex="2" 
                                        FieldName="TipoProjeto" Visible="False" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldUnidadeAtendimento" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        AreaIndex="2" Caption="Unidade de Atendimento" 
                                        FieldName="NomeUnidadeAtendimento" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldDataInicio" AllowedAreas="RowArea" 
                                        Area="RowArea" Caption="Data de Início do Projeto" FieldName="DataInicio" 
                                        Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldDataTermino" AllowedAreas="RowArea" 
                                        Area="RowArea" Caption="Data de Término do Projeto" FieldName="DataTermino" 
                                        Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldValorContrapartida" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Valor das Contrapartidas" 
                                        FieldName="ValorContrapartida" Visible="False" AreaIndex="8">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldFontesFinanciamento" Area="RowArea" 
                                        Caption="Fontes de Financiamento" FieldName="FontesFinanciamento" 
                                        Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldIndicaLinhaFinanciamento" 
                                        AllowedAreas="RowArea, ColumnArea" Area="RowArea" 
                                        Caption="Linha de Financiamento" FieldName="IndicaLinhaFinanciamento" 
                                        Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldSetorIndustrial" AllowedAreas="RowArea" 
                                        Area="RowArea" Caption="Setor Industrial" FieldName="SetorIndustrial" 
                                        Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldPublicoAlvo" AllowedAreas="RowArea" 
                                        Area="RowArea" Caption="Cliente" FieldName="PublicoAlvo" Visible="False" 
                                        AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldObjetivoGeral" AllowedAreas="RowArea" 
                                        Area="RowArea" Caption="Objetivo do Projeto" FieldName="ObjetivoGeral" 
                                        Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldJustificativa" AllowedAreas="RowArea" 
                                        Area="RowArea" Caption="Justificativa" FieldName="Justificativa" 
                                        Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldEscopo" AllowedAreas="RowArea" Area="RowArea" 
                                        Caption="Escopo" FieldName="Escopo" Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                </Fields>
                                <OptionsView ShowRowGrandTotalHeader="False" />
                                <OptionsCustomization CustomizationWindowWidth="280" />
                                <OptionsPager Visible="False">
                                </OptionsPager>
                                <Styles>
                                    <HeaderStyle  />
                                    <AreaStyle >
                                    </AreaStyle>
                                    <FilterAreaStyle >
                                    </FilterAreaStyle>
                                    <CellStyle >
                                    </CellStyle>
                                    <FieldValueStyle >
                                    </FieldValueStyle>
                                    <GrandTotalCellStyle >
                                    </GrandTotalCellStyle>
                                    <CustomTotalCellStyle >
                                    </CustomTotalCellStyle>
                                    <MenuItemStyle  />
                                    <MenuStyle  />
                                    <CustomizationFieldsStyle >
                                    </CustomizationFieldsStyle>
                                    <CustomizationFieldsHeaderStyle >
                                    </CustomizationFieldsHeaderStyle>
                                    <LoadingPanel >
                                    </LoadingPanel>
                                </Styles>
                                <OptionsLoadingPanel Text="Carregando&amp;hellip;">
                                    <Style ></Style>
                                </OptionsLoadingPanel>
                                <StylesEditors>
                                    <DropDownWindow >
                                    </DropDownWindow>
                                </StylesEditors>
                            </dxwpg:ASPxPivotGrid>
                            </div>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

