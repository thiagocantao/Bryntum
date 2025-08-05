<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_OLAP_Contratos.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_OLAP_Contratos"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 5px" valign="top"></td>
                                <td valign="top">
                                    <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgContratos">
                                    </dxpgwx:ASPxPivotGridExporter>
                                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                    </dxhf:ASPxHiddenField>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top"></td>
                                <td colspan="1" valign="top">
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
                                        <dxwpg:ASPxPivotGrid ID="pvgContratos" runat="server" ClientInstanceName="pvgContratos"
                                            Width="100%"
                                            OnCustomFieldSort="pvgContratos_CustomFieldSort"
                                            OnCustomSummary="pvgContratos_CustomSummary" ClientIDMode="AutoID">
                                            <Fields>
                                                <dxwpg:PivotGridField FieldName="NomeUnidadeNegocio" ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" Caption="Unidade">
                                                    <CellStyle></CellStyle>

                                                    <HeaderStyle></HeaderStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="NomeProjeto" ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Caption="Projeto/Demanda" EmptyCellText="Sem Projeto" EmptyValueText="Sem Projeto">
                                                    <CellStyle></CellStyle>

                                                    <HeaderStyle></HeaderStyle>

                                                    <ValueStyle></ValueStyle>

                                                    <ValueTotalStyle></ValueTotalStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="NumeroContrato" ID="field2" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="3" Caption="N&#250;mero Contrato" EmptyValueText="&lt;Sem Projeto&gt;">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="TipoAquisicao" ID="field3" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="4" Caption="Modalidade Aquisi&#231;&#227;o">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="NomeFornecedor" ID="field4" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Caption="Fornecedor">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="Status" ID="fldStatus" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Caption="Status">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="ValorPrevisto" ID="field6" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" RunningTotal="True" Caption="Valor Previsto Parcela">
                                                    <CellStyle></CellStyle>

                                                    <HeaderStyle></HeaderStyle>

                                                    <ValueStyle></ValueStyle>

                                                    <ValueTotalStyle></ValueTotalStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="ValorPago" ID="field7" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1" RunningTotal="True" Caption="Valor Pago Parcela" TotalsVisibility="None">
                                                    <CellStyle></CellStyle>

                                                    <HeaderStyle></HeaderStyle>

                                                    <ValueStyle></ValueStyle>

                                                    <ValueTotalStyle></ValueTotalStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="NumeroParcela" ID="field8" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="Parcela">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="DataVencimento" ID="field9" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Data Vencimento" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="DataPagamento" ID="field10" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" Visible="False" Caption="Data Pagamento" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="DataInicioVigencia" ID="field11" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="In&#237;cio Vig&#234;ncia" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="DataTerminoVigencia" ID="field12" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="T&#233;rmino Vig&#234;ncia" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="Objeto" ID="field13" AllowedAreas="RowArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="Objeto">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="HistoricoParcela" ID="field14" AllowedAreas="RowArea" Area="RowArea" AreaIndex="5" Visible="False" Caption="Hist&#243;rico">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="PessoaResponsavel" ID="field15" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="7" Visible="False" Caption="Respons&#225;vel">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="MesVencimento" ID="fldMesVencimento" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Visible="False" Caption="M&#234;s Vencimento" SortMode="Custom">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="AnoVencimento" ID="field17" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Visible="False" Caption="Ano Vencimento">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="MesPagamento" ID="fldMesPagamento" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Visible="False" Caption="M&#234;s Pagamento" SortMode="Custom">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="AnoPagamento" ID="field19" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" Visible="False" Caption="Ano Pagamento">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="SituacaoParcela" ID="field16" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="0" Caption="Situa&#231;&#227;o Parcelas">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="TipoContrato" ID="fieldTipoContrato" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="1" Caption="Situa&#231;&#227;o Contrato">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="NumeroAditivoContrato" ID="fieldNumeroAditivoContrato" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Visible="False" Caption="Aditivo">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="ValorContrato" ID="fieldValorContrato" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2" Visible="False" Caption="Valor Contrato" SummaryType="Custom">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>

                                                    <ValueTotalStyle></ValueTotalStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="ValorRestante" ID="fieldValorRestante" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2" Visible="False" Caption="Valor Restante Contrato" SummaryType="Custom">
                                                    <CellStyle></CellStyle>

                                                    <ValueStyle></ValueStyle>

                                                    <ValueTotalStyle></ValueTotalStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="SiglaUnidadeNegocio" ID="fieldSiglaUnidadeNegocio" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="Sigla Un"></dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="DescricaoTipoContrato" ID="fieldDescricaoTipoContrato" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Visible="False" Caption="Tipo Contrato"></dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="FonteRecursosFinanceiros" ID="fieldFonteRecursosFinanceiros" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Visible="False" Caption="Fonte Recursos"></dxwpg:PivotGridField>
                                                <dxpgwx:PivotGridField ID="fieldNomePrograma" AllowedAreas="RowArea, FilterArea" Area="RowArea" AreaIndex="1" FieldName="NomePrograma">
                                                </dxpgwx:PivotGridField>
                                            </Fields>

                                            <OptionsFilter ShowOnlyAvailableItems="True" />

                                            <Styles>
                                                <HeaderStyle></HeaderStyle>

                                                <AreaStyle></AreaStyle>

                                                <CellStyle></CellStyle>

                                                <TotalCellStyle></TotalCellStyle>

                                                <GrandTotalCellStyle></GrandTotalCellStyle>

                                                <CustomTotalCellStyle></CustomTotalCellStyle>

                                                <FilterWindowStyle></FilterWindowStyle>

                                                <MenuStyle></MenuStyle>

                                                <CustomizationFieldsStyle></CustomizationFieldsStyle>

                                                <CustomizationFieldsContentStyle></CustomizationFieldsContentStyle>

                                                <CustomizationFieldsHeaderStyle></CustomizationFieldsHeaderStyle>
                                            </Styles>

                                            <OptionsPager Visible="False"></OptionsPager>

                                            <StylesEditors>
                                                <DropDownWindow></DropDownWindow>
                                            </StylesEditors>
                                        </dxwpg:ASPxPivotGrid>

                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td valign="top"></td>
                                <td valign="middle">&nbsp;
                                <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False" Text="S/I = Sem Informação"></asp:Label>
                                </td>
                                <td valign="top"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
