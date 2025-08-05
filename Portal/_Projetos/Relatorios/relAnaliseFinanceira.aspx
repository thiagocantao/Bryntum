<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relAnaliseFinanceira.aspx.cs"
    Inherits="_Projetos_Relatorios_relAnaliseFinanceira" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
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
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise Financeira de Projetos"></asp:Label></td>
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
                            &nbsp;</td>
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
                                oncustomfieldsort="pvgDadosProjeto_CustomFieldSort" ClientIDMode="AutoID">
                                <Fields>
                                    <dxwpg:PivotGridField ID="f_NomeProjeto" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        Caption="Projeto" FieldName="NomeProjeto" AreaIndex="2">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_NomeUnidadeNegocio" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        Caption="Unidade de Neg&#243;cio" FieldName="NomeUnidadeNegocio" 
                                        AreaIndex="0" Width="40">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CodigoProjeto" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" 
                                        Caption="Código do projeto" FieldName="_CodigoProjeto" Visible="False" 
                                        AreaIndex="0" Options-AllowDragInCustomizationForm="False" 
                                        Options-ShowInCustomizationForm="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Ano" AllowedAreas="RowArea, ColumnArea, FilterArea" 
                                        Caption="Ano" FieldName="_Ano" AreaIndex="0">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Mes" AllowedAreas="RowArea, ColumnArea, FilterArea" 
                                        Caption="Mês" FieldName="Mes" SortMode="Custom" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoReal" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa Real"
                                        FieldName="CustoReal" AreaIndex="1">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoPrevisto" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Despesa Prevista"
                                        FieldName="CustoPrevisto" AreaIndex="0">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Custo" AllowedAreas="DataArea" Area="DataArea" Caption="Despesa"
                                        FieldName="Custo" Visible="False" AreaIndex="2">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaReal" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Receita Real"
                                        FieldName="ReceitaReal" Visible="False" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaPrevista" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Receita Prevista"
                                        FieldName="ReceitaPrevista" Visible="False" AreaIndex="2">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_Receita" AllowedAreas="DataArea" Area="DataArea" Caption="Receita"
                                        FieldName="Receita" Visible="False" AreaIndex="4">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ValorPlanejadoCusto" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Valor Planejado"
                                        FieldName="ValorPlanejadoCusto" AreaIndex="2" 
                                        CellFormat-FormatString="{0:C2}" CellFormat-FormatType="Numeric" 
                                        GrandTotalCellFormat-FormatString="{0:C2}" 
                                        GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:C2}" 
                                        TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:C2}" 
                                        TotalValueFormat-FormatType="Numeric" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ValorAgregadoCusto" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Valor Agregado"
                                        FieldName="ValorAgregadoCusto" AreaIndex="2" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_NomeEntidade" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" Caption="Nome Entidade"
                                        FieldName="NomeEntidade" Visible="False" AreaIndex="5">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_StatusProjeto" 
                                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" 
                                        Caption="Status Projeto" FieldName="StatusProjeto">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field" AllowedAreas="DataArea" Area="DataArea" 
                                        AreaIndex="2" Caption="Variação Despesa" 
                                        UnboundExpression=" ([ValorAgregadoCusto] - [CustoReal])" 
                                        UnboundType="Decimal" Visible="False" UnboundFieldName="f_VariacaoDespesa">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldCodigoReservadoCR" AllowedAreas="RowArea, FilterArea" 
                                        Caption="CR" FieldName="CodigoReservadoCR" Area="RowArea" AreaIndex="3">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldDescricaoGrupoConta" 
                                        AllowedAreas="RowArea, FilterArea" Area="RowArea" AreaIndex="4" Caption="Grupo" 
                                        FieldName="DescricaoGrupoConta">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldDescricaoConta" 
                                        AllowedAreas="RowArea, FilterArea" Area="RowArea" AreaIndex="5" Caption="Conta" 
                                        FieldName="DescricaoConta">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_CustoPrevistoData" AllowedAreas="DataArea" 
                                        Area="DataArea" AreaIndex="2" Caption="Despesa Prevista até a Data" 
                                        FieldName="CustoPrevistoData">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="f_ReceitaPrevistaData" AllowedAreas="DataArea" 
                                        Area="DataArea" Caption="Receita Prevista até a Data" 
                                        FieldName="ReceitaPrevistaData" Visible="False" AreaIndex="2">
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
</asp:Content>
