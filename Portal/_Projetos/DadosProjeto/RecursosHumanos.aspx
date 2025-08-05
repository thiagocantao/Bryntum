<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecursosHumanos.aspx.cs" Inherits="_Projetos_DadosProjeto_RecursosHumanos" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script language="javascript" type="text/javascript">
        function reziseDiv() {
            if (window.parent.sp_Tela) {
                pnDiv.SetWidth(window.parent.sp_Tela.GetPane(1).lastWidth - 50);
            }
        }
    </script>
    <style type="text/css">
        .dxmMenu {
            font: 12px Tahoma, Geneva, sans-serif;
            color: black;
            background-color: #F0F0F0;
            border: 1px solid #A8A8A8;
            padding: 2px;
        }

        .dxmMenuItemWithImage {
            white-space: nowrap;
        }

        .dxmMenuItemWithImage {
            padding: 4px 8px 5px;
        }

        .dxmMenuItemSpacing {
            width: 2px;
        }
    </style>
</head>
<body style="overflow: visible; margin: 0" onload="reziseDiv()">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="width: 5px; height: 5px"></td>
                <td>
                    <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server"
                        ASPxPivotGridID="pvgRecursosHumanos"
                        OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                    </dxpgwx:ASPxPivotGridExporter>
                </td>
            </tr>
            <tr>
                <td style="width: 5px; height: 5px"></td>
                <td>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                </td>
            </tr>
            <tr>
                <td style="width: 5px"></td>
                <td>

                    <dxp:ASPxPanel ID="pnDiv" ClientInstanceName="pnDiv" runat="server" Width="100%" ScrollBars="Auto">
                        <PanelCollection>
                            <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
                                    style="width: 100%">
                                    <tr runat="server">
                                        <td runat="server" style="padding: 3px" valign="top">
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
                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
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
                                                                <dxm:MenuItem ClientVisible="False" Name="btnLayout" Text="" ToolTip="Layout">
                                                                    <Items>
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
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
                                                    <td style="width: 30px;">
                                                        <dxe:ASPxImage ID="imgGraficos" runat="server"
                                                            ImageUrl="~/imagens/botoes/btnCurvaS.png"
                                                            ToolTip="Gráfico de Recursos Humanos do Projeto">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td style="width: 45px; display: none;"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <dxwpg:ASPxPivotGrid ID="pvgRecursosHumanos" runat="server"
                                    ClientIDMode="AutoID" ClientInstanceName="pvgRecursosHumanos"
                                    OnCustomCellStyle="pvgRecursosHumanos_CustomCellStyle"
                                    OnCustomFieldSort="pvgRecursosHumanos_CustomFieldSort"
                                    OnFieldAreaChanged="pvgRecursosHumanos_FieldAreaChanged"
                                    OnFieldAreaIndexChanged="pvgRecursosHumanos_FieldAreaIndexChanged"
                                    OnFieldVisibleChanged="pvgRecursosHumanos_FieldVisibleChanged" Width="100%">
                                    <Fields>
                                        <dxwpg:PivotGridField ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="0" Caption="Nome do recurso" FieldName="NomeRecurso"
                                            GroupIndex="0" InnerGroupIndex="0">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field1"
                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0"
                                            Caption="Ano" FieldName="Ano" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field2" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="0" Caption="Quantidade" CellFormat-FormatString="{0:n1}"
                                            CellFormat-FormatType="Custom" FieldName="Trabalho"
                                            GrandTotalCellFormat-FormatString="{0:n1}"
                                            GrandTotalCellFormat-FormatType="Custom" ValueFormat-FormatType="Custom">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field3" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="1" Caption="Quantidade Real" CellFormat-FormatString="{0:n1}"
                                            CellFormat-FormatType="Custom" FieldName="TrabalhoReal"
                                            GrandTotalCellFormat-FormatString="{0:n1}"
                                            GrandTotalCellFormat-FormatType="Custom">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field4" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="2" Caption="Quantidade Prevista" CellFormat-FormatString="{0:n1}"
                                            CellFormat-FormatType="Custom" FieldName="TrabalhoLB"
                                            GrandTotalCellFormat-FormatString="{0:n1}"
                                            GrandTotalCellFormat-FormatType="Custom">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field5" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Variação da Quantidade" CellFormat-FormatString="{0:n1}"
                                            CellFormat-FormatType="Custom" FieldName="VariacaoTrabalho"
                                            GrandTotalCellFormat-FormatString="{0:n1}"
                                            GrandTotalCellFormat-FormatType="Custom">
                                            <CellStyle ForeColor="Black">
                                            </CellStyle>
                                            <ValueStyle ForeColor="Black">
                                            </ValueStyle>
                                            <ValueTotalStyle ForeColor="Black">
                                            </ValueTotalStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDespesa" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Despesa" CellFormat-FormatString="{0:n2}"
                                            CellFormat-FormatType="Custom" FieldName="Custo"
                                            GrandTotalCellFormat-FormatString="{0:n1}"
                                            GrandTotalCellFormat-FormatType="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDespesaReal" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Despesa Real" CellFormat-FormatString="{0:n2}"
                                            CellFormat-FormatType="Custom" FieldName="CustoReal"
                                            GrandTotalCellFormat-FormatString="{0:n2}"
                                            GrandTotalCellFormat-FormatType="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDespesaPrevista" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Despesa Prevista" CellFormat-FormatString="{0:n2}"
                                            CellFormat-FormatType="Custom" FieldName="CustoLB"
                                            GrandTotalCellFormat-FormatString="{0:n2}"
                                            GrandTotalCellFormat-FormatType="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDespesaHoraExtra" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Despesa Hora Extra" CellFormat-FormatString="{0:n2}"
                                            CellFormat-FormatType="Custom" FieldName="CustoHE"
                                            GrandTotalCellFormat-FormatString="{0:n2}"
                                            GrandTotalCellFormat-FormatType="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDespesaRestante" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Despesa Restante" CellFormat-FormatString="{0:n2}"
                                            CellFormat-FormatType="Custom" FieldName="CustoRestante"
                                            GrandTotalCellFormat-FormatString="{0:n2}"
                                            GrandTotalCellFormat-FormatType="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldVariacaoDaDespesa" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="4" Caption="Variação da Despesa" CellFormat-FormatString="{0:n2}"
                                            CellFormat-FormatType="Custom" FieldName="VariacaoCusto"
                                            GrandTotalCellFormat-FormatString="{0:n2}"
                                            GrandTotalCellFormat-FormatType="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field12"
                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1"
                                            Caption="Nome Tarefa" FieldName="NomeTarefa" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field13" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Quantidade Restante" CellFormat-FormatString="{0:n1}"
                                            CellFormat-FormatType="Custom" FieldName="TrabalhoRestante"
                                            GrandTotalCellFormat-FormatString="{0:n1}"
                                            GrandTotalCellFormat-FormatType="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field14"
                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" Caption="Mês"
                                            FieldName="Mes" SortMode="Custom" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="field15"
                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0"
                                            Caption="Data" CellFormat-FormatString="{0:dd/MM/yyyy}"
                                            CellFormat-FormatType="DateTime" FieldName="Data"
                                            ValueFormat-FormatString="{0:dd/MM/yyyy}" ValueFormat-FormatType="DateTime"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                    </Fields>
                                    <OptionsPager Position="Top" RowsPerPage="9" ShowDefaultImages="False"
                                        Visible="False">
                                        <Summary AllPagesText="Páginas: {0} - {1} ({2} itens)"
                                            Text="Página {0} de {1} ({2} itens)" />
                                    </OptionsPager>
                                    <Styles>
                                        <HeaderStyle />
                                        <AreaStyle>
                                        </AreaStyle>
                                        <FilterAreaStyle>
                                        </FilterAreaStyle>
                                        <FieldValueStyle>
                                        </FieldValueStyle>
                                        <FieldValueTotalStyle>
                                        </FieldValueTotalStyle>
                                        <FieldValueGrandTotalStyle>
                                        </FieldValueGrandTotalStyle>
                                        <CellStyle>
                                        </CellStyle>
                                        <CustomTotalCellStyle>
                                        </CustomTotalCellStyle>
                                        <CustomizationFieldsStyle>
                                        </CustomizationFieldsStyle>
                                        <CustomizationFieldsCloseButtonStyle>
                                        </CustomizationFieldsCloseButtonStyle>
                                    </Styles>
                                    <Groups>
                                        <dxwpg:PivotGridWebGroup ShowNewValues="True" />
                                    </Groups>
                                </dxwpg:ASPxPivotGrid>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxp:ASPxPanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

