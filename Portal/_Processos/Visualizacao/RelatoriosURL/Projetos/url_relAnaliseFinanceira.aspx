<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relAnaliseFinanceira.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_relAnaliseFinanceira"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    
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
                                    </td>
                                </tr>
                            </table>
                            &nbsp;
                        <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgDadosProjeto"
                        OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                    </dxpgwx:ASPxPivotGridExporter>
                                        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                        </dxhf:ASPxHiddenField>
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
                                    Width="98%" OnCustomSummary="pvgDadosIndicador_CustomSummary"
                                    OnCustomCellStyle="pvgDadosProjeto_CustomCellStyle" EnableViewState="False"
                                    OnCustomFieldSort="pvgDadosProjeto_CustomFieldSort" ClientIDMode="AutoID">
                                    <Fields>
                                        <dxwpg:PivotGridField ID="f_NomeProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="Projeto" FieldName="NomeProjeto" AreaIndex="2">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_NomeUnidadeNegocio" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="Unidade de Neg&#243;cio" FieldName="NomeUnidadeNegocio"
                                            AreaIndex="0" Width="40">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_CodigoProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="Código do projeto" FieldName="_CodigoProjeto" Visible="False"
                                            AreaIndex="0" Options-AllowDragInCustomizationForm="False" Options-ShowInCustomizationForm="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_Ano" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea"
                                            Caption="Ano" FieldName="_Ano" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_Mes" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea"
                                            Caption="Mês" FieldName="Mes" SortMode="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_CustoReal" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Real"
                                            FieldName="CustoReal" AreaIndex="1">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_CustoPrevisto" AllowedAreas="DataArea" Area="DataArea"
                                            Caption="Despesa Prevista" FieldName="CustoPrevisto" AreaIndex="0">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_Custo" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa"
                                            FieldName="Custo" Visible="False" AreaIndex="2">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_ReceitaReal" AllowedAreas="DataArea" Area="DataArea"
                                            Caption="Receita Real" FieldName="ReceitaReal" Visible="False" AreaIndex="3">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_ReceitaPrevista" AllowedAreas="DataArea" Area="DataArea"
                                            Caption="Receita Prevista" FieldName="ReceitaPrevista" Visible="False" AreaIndex="2">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_Receita" AllowedAreas="DataArea" Area="DataArea" Caption="Receita"
                                            FieldName="Receita" Visible="False" AreaIndex="4">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_ValorPlanejadoCusto" AllowedAreas="DataArea" Area="DataArea"
                                            Caption="Valor Planejado" FieldName="ValorPlanejadoCusto" AreaIndex="2" CellFormat-FormatString="{0:C2}"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:C2}" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="{0:C2}" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:C2}"
                                            TotalValueFormat-FormatType="Numeric" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_ValorAgregadoCusto" AllowedAreas="DataArea" Area="DataArea"
                                            Caption="Valor Agregado" FieldName="ValorAgregadoCusto" AreaIndex="2" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_NomeEntidade" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="Nome Entidade" FieldName="NomeEntidade" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_StatusProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="1" Caption="Status Projeto" FieldName="StatusProjeto">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2"
                                            Caption="Variação Custo" UnboundExpression=" ([ValorAgregadoCusto] - [CustoReal])"
                                            UnboundType="Decimal" Visible="False" UnboundFieldName="field">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldCodigoReservadoCR" AllowedAreas="RowArea, FilterArea"
                                            Caption="CR" FieldName="CodigoReservadoCR" Area="RowArea" AreaIndex="3">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDescricaoGrupoConta" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="4" Caption="Grupo" FieldName="DescricaoGrupoConta">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDescricaoConta" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="5" Caption="Conta" FieldName="DescricaoConta">
                                        </dxwpg:PivotGridField>
                                        <dxpgwx:PivotGridField ID="fieldNomePrograma" AllowedAreas="RowArea, FilterArea" Area="RowArea" AreaIndex="2" Caption="Programa" FieldName="NomePrograma" Visible="False">
                                        </dxpgwx:PivotGridField>
                                    </Fields>
                                    <OptionsPager Visible="False">
                                    </OptionsPager>
                                    <OptionsFilter ShowOnlyAvailableItems="True" />
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
    </form>
</body>
</html>
