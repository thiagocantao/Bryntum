<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relProjetosPortfolios.aspx.cs" Inherits="_Portfolios_Relatorios_relProjetosPortfolios" Title="Portal da Estratégia" %>


<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height:26px">
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle">
                                &nbsp;
                                <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Projetos de Portfólios"></asp:Label></td>
                            <td align="right" style="width: 110px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Portfólio:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" style="width: 190px; height: 26px;">
                                <dxe:ASPxComboBox ID="ddlPortfolios" runat="server" ClientInstanceName="ddlPortfolios"
                                    ValueType="System.String" Width="390px">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) 
{
	btnSelecionar.SetEnabled(true);	
}" />
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="right" style="width: 8px; height: 26px;">
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
                            <td align="left" style="width: 80px; height: 26px;">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" 
                                    Text="Selecionar" ClientEnabled="False" ClientInstanceName="btnSelecionar">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="width: 5px; height: 26px;">
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
                        <td valign="top">
                        </td>
                        <td valign="top">
                        </td>
                        <td>
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
                                Width="100%"   OnCustomCellStyle="pvgDadosProjeto_CustomCellStyle" OnCustomSummary="pvgDadosProjeto_CustomSummary">
                                <Fields>
                                    <dxwpg:PivotGridField ID="f_NomeProjeto" AllowedAreas="RowArea, FilterArea" Area="RowArea"
                                        Caption="Projeto" FieldName="NomeProjeto" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Categoria" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        Caption="Categoria" FieldName="Categoria" AreaIndex="0">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_SiglaUnidade" AllowedAreas="RowArea" Area="RowArea"
                                        Caption="Sigla" FieldName="SiglaUnidade" Visible="False" AreaIndex="2" Width="40">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_NomeGerenteUnidade" AllowedAreas="RowArea, FilterArea" Area="RowArea" Caption="Gerente Unidade" FieldName="NomeGerenteUnidade" Visible="False" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_NomeGerenteProjeto" AllowedAreas="RowArea, FilterArea" Area="RowArea"
                                        AreaIndex="0" Caption="Gerente Projeto" FieldName="NomeGerenteProjeto" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_StatusProjeto" AllowedAreas="ColumnArea" Area="ColumnArea" Caption="Status do Projeto" FieldName="StatusProjeto" Visible="False" AreaIndex="0">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_FaseProjeto" AllowedAreas="ColumnArea" Area="ColumnArea" Caption="Fase do Projeto" FieldName="FaseProjeto" Visible="False" AreaIndex="0">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoReal" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Real"
                                        FieldName="CustoReal" AreaIndex="0">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoPrevistoTotal" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Prevista Total"
                                        FieldName="CustoPrevistoTotal" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoPrevistoData" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Prevista At&#233; Data"
                                        FieldName="CustoPrevistoData" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Custo" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Tend&#234;ncia"
                                        FieldName="Custo" AreaIndex="2">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoHE" AllowedAreas="DataArea" Area="DataArea" Caption="Despesas Horas Extras"
                                        FieldName="CustoHE" AreaIndex="1" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_TRabalhoReal" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Real"
                                        FieldName="TRabalhoReal" AreaIndex="2" CellFormat-FormatType="Numeric" CellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0" TotalValueFormat-FormatType="Numeric" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_TrabalhoPrevistoTotal" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Previsto Total"
                                        FieldName="TrabalhoPrevistoTotal" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_TrabalhoPrevistoData" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Previsto at&#233; a Data"
                                        FieldName="TrabalhoPrevistoData" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Trabalho" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Tend&#234;ncia"
                                        FieldName="Trabalho" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_TrabalhoHE" AllowedAreas="DataArea" Area="DataArea" Caption="Trabalho Horas Extras"
                                        FieldName="TrabalhoHE" AreaIndex="2" CellFormat-FormatType="Numeric" CellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0" TotalValueFormat-FormatType="Numeric" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ValorPlanejado" AllowedAreas="DataArea" Area="DataArea" Caption="Valor Planejado"
                                        FieldName="ValorPlanejado" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ValorAgregado" AllowedAreas="DataArea" Area="DataArea" Caption="Valor Agregado"
                                        FieldName="ValorAgregado" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaRealTotal" AllowedAreas="DataArea" Area="DataArea" Caption="Receita Real Total"
                                        FieldName="ReceitaRealTotal" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaPrevistaTotal" AllowedAreas="DataArea" Area="DataArea" Caption="Receita Prevista Total"
                                        FieldName="ReceitaPrevistaTotal" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaPrevistaData" AllowedAreas="DataArea" Area="DataArea" Caption="Receita Prevista at&#233; a Data"
                                        FieldName="ReceitaPrevistaData" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Receita" AllowedAreas="DataArea" Area="DataArea" Caption="Receita"
                                        FieldName="Receita" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_EscoreCriterios" AllowedAreas="DataArea" Area="DataArea" Caption="Escore Criterios"
                                        FieldName="EscoreCriterios" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_EscoreRiscos" AllowedAreas="DataArea" Area="DataArea" Caption="Escore Riscos"
                                        FieldName="EscoreRiscos" Visible="False" CellFormat-FormatType="Numeric" AreaIndex="3" CellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_VariacaoTrabalho" AllowedAreas="DataArea" Area="DataArea" Caption="Variacao Trabalho"
                                        FieldName="VariacaoTrabalho" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_VariacaoCusto" AllowedAreas="DataArea" Area="DataArea" Caption="Varia&#231;&#227;o da Despesa"
                                        FieldName="VariacaoCusto" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_VariacaoReceita" AllowedAreas="DataArea" Area="DataArea" Caption="Varia&#231;&#227;o Receita"
                                        FieldName="VariacaoReceita" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_EstimativaCustoConcluir" AllowedAreas="DataArea" Area="DataArea" Caption="Estimativa Despesa Concluir"
                                        FieldName="EstimativaCustoConcluir" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field4" AllowedAreas="DataArea" Area="DataArea" AreaIndex="3"
                                        Caption="&#205;ndice de Desempenho de Prazo" CellFormat-FormatString="#,##0.0%"
                                        CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0.0%"
                                        GrandTotalCellFormat-FormatType="Numeric" SummaryType="Custom" TotalCellFormat-FormatString="#,##0.0%"
                                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0%"
                                        TotalValueFormat-FormatType="Numeric" UnboundExpression="[ValorAgregado] / [ValorPlanejado]"
                                        UnboundFieldName="IndiceDesempenhoPrazo" UnboundType="Decimal" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field5" AllowedAreas="DataArea" Area="DataArea" AreaIndex="3"
                                        Caption="&#205;ndice de Desempenho de Despesa" CellFormat-FormatString="#,##0.0%"
                                        CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0.0%"
                                        GrandTotalCellFormat-FormatType="Numeric" SummaryType="Custom" TotalCellFormat-FormatString="#,##0.0%"
                                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0%"
                                        TotalValueFormat-FormatType="Numeric" UnboundExpression="[ValorAgregado] / [CustoReal]"
                                        UnboundFieldName="IndiceDesempenhoCusto" UnboundType="Decimal" Visible="False">
                                    </dxwpg:PivotGridField>
                                </Fields>
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
       <script type="text/javascript" language="javascript">
       var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
       if(isIE8)
       {
            document.forms[0].style.overflow = "hidden";
       }
       else
       {
            document.forms[0].style.position = "relative";
            document.forms[0].style.overflow = "hidden";       
       }

    </script>
</asp:Content>

