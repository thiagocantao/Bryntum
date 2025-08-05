<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_OlapFluxoCaixa.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_OlapFluxoCaixa" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        function atualizaDados() {
            grid.PerformCallback();
        }
    </script>
    <style type="text/css">

        .btn_inicialMaiuscula {
            text-transform: capitalize !important;
        }

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

        .style1 {
            height: 5px;
        }

        .style2 {
            width: 99%;
        }
    </style>
</head>
<body style="margin-top: 0">
    <form id="form1" runat="server">
        <div>
            <table>

                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td align="right">
                                    <table>

                                        <tr>


                                            <td style="width: 95px; padding-bottom: 0px;" align="left">
                                                <dxe:ASPxComboBox ID="ddlCenario" runat="server" ClientInstanceName="ddlCenario"
                                                    SelectedIndex="0" ValueType="System.String"
                                                    Width="100px">
                                                    <Items>
                                                        <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, cen_rio_1 %>" Value="1" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao,cen_rio_2 %>" Value="2" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao,cen_rio_3 %>" Value="3" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao,cen_rio_4 %>" Value="4" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao,cen_rio_5 %>" Value="5" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao,cen_rio_6 %>" Value="6" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao,cen_rio_7 %>" Value="7" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao,cen_rio_8 %>" Value="8" />
                                                        <dxe:ListEditItem Text="<%$ Resources:traducao,cen_rio_9 %>" Value="9" />
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td style="padding-left: 10px;">
                                                <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False" CssClass="btn_inicialMaiuscula"
                                                    Text="Selecionar" Width="90px">
                                                    <Paddings Padding="0px" />
                                                    <ClientSideEvents Click="function(s, e) 
        {
	        grid.PerformCallback('AtualizarVC');
        }" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="overflow: auto; height: <%=alturaTela %>; width: <%=larguraTela %>">
                            <table runat="server" id="tbBotoes" cellpadding="0" cellspacing="0"
                                style="width: 99%">
                                <tr runat="server">
                                    <td runat="server"
                                        valign="top">
                                        <table cellpadding="0" cellspacing="0" style="height: 22px">
                                            <tr>
                                                <td>
                                                    <dxm:ASPxMenu runat="server" ClientInstanceName="menu" ItemSpacing="5px"
                                                        BackColor="Transparent" ID="menu" OnInit="menu_Init"
                                                        OnItemClick="menu_ItemClick">
                                                        <Paddings Padding="0px"></Paddings>
                                                        <Items>
                                                            <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                <Items>
                                                                    <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                        <Image Url="~/imagens/menuExportacao/xls.png"></Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                        <Image Url="~/imagens/menuExportacao/pdf.png"></Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                        <Image Url="~/imagens/menuExportacao/rtf.png"></Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                        <Image Url="~/imagens/menuExportacao/html.png"></Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                        <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                        </Image>
                                                                    </dxm:MenuItem>
                                                                </Items>

                                                                <Image Url="~/imagens/botoes/btnDownload.png"></Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem ClientVisible="False" Name="btnLayout" Text="" ToolTip="Layout">
                                                                <Items>
                                                                    <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                        <Image IconID="save_save_16x16"></Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                        <Image IconID="actions_reset_16x16"></Image>
                                                                    </dxm:MenuItem>
                                                                </Items>

                                                                <Image Url="~/imagens/botoes/layout.png"></Image>
                                                            </dxm:MenuItem>
                                                        </Items>

                                                        <ItemStyle Cursor="pointer">
                                                            <HoverStyle>
                                                                <border borderstyle="None"></border>
                                                            </HoverStyle>

                                                            <Paddings Padding="0px"></Paddings>
                                                        </ItemStyle>

                                                        <SubMenuItemStyle Cursor="pointer" BackColor="White">
                                                            <SelectedStyle>
                                                                <border borderstyle="None"></border>
                                                            </SelectedStyle>
                                                        </SubMenuItemStyle>

                                                        <Border BorderStyle="None"></Border>
                                                    </dxm:ASPxMenu>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>

                            <dxwpg:ASPxPivotGrid ID="grid" runat="server" ClientInstanceName="grid"
                                Width="99%" OnCustomCallback="grid_CustomCallback" ClientIDMode="AutoID" OnCustomFieldSort="grid_CustomFieldSort">
                                <Fields>
                                    <dxwpg:PivotGridField ID="colNomeProjeto" AllowedAreas="RowArea, ColumnArea" Area="RowArea" AreaIndex="2"
                                        FieldName="NomeProjeto" Caption="Projeto">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="colCategoria" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        AreaIndex="0" FieldName="DescricaoCategoria" Caption="Categoria">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="colAno" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="2"
                                        FieldName="Ano" Visible="False" Caption="Ano">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="colPeriodo" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="0"
                                        FieldName="Periodo" Caption="Per&#237;odo" Area="ColumnArea" SortMode="Custom">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="colDespesa" AllowedAreas="DataArea" Area="DataArea"
                                        AreaIndex="0" FieldName="Despesa"
                                        ValueFormat-FormatString="n2" ValueFormat-FormatType="Numeric" Caption="Despesa" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="colReceita" AllowedAreas="DataArea" Area="DataArea"
                                        AreaIndex="0" FieldName="Receita"
                                        ValueFormat-FormatString="n2" ValueFormat-FormatType="Numeric" Caption="Receita" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field1" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0"
                                        Caption="Saldo" UnboundExpression="[Receita] - [Despesa]" UnboundFieldName="saldo"
                                        UnboundType="Decimal">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field" AllowedAreas="RowArea, ColumnArea" Area="RowArea"
                                        AreaIndex="1" Caption="Status" FieldName="DescricaoStatus">
                                    </dxwpg:PivotGridField>
                                </Fields>
                                <OptionsCustomization CustomizationWindowHeight="300" CustomizationWindowWidth="200" />
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
                                    <GrandTotalCellStyle>
                                    </GrandTotalCellStyle>
                                    <FilterButtonPanelStyle>
                                    </FilterButtonPanelStyle>
                                </Styles>
                            </dxwpg:ASPxPivotGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server"
            ASPxPivotGridID="grid">
        </dxpgwx:ASPxPivotGridExporter>
    </form>
</body>
</html>
