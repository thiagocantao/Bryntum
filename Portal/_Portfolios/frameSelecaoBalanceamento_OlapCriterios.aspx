<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_OlapCriterios.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_OlapCriterios" %>

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
            padding: 4px 8px 5px;
        }

        .dxmMenuItemWithImage {
            white-space: nowrap;
        }

        .dxmMenuItemSpacing {
            width: 2px;
        }

        .style1 {
            height: 5px;
        }
    </style>
</head>
<body style="margin-top: 0;">
    <form id="form1" runat="server">
        <div style="width:100%;overflow:auto">
            <table style="width:100%">

                <tr>
                    <td align="right">
                        <table>

                            <tr>
                                <td style="width: 95px; padding-bottom: 0px;" align="left">
                                    <dxe:ASPxComboBox ID="ddlCenario" runat="server" ClientInstanceName="ddlCenario"
                                        SelectedIndex="0" ValueType="System.String"
                                        Width="100px">
                                        <Items>
                                            <dxe:ListEditItem Selected="True" Text="Cen&#225;rio 1" Value="1" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 2" Value="2" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 3" Value="3" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 4" Value="4" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 5" Value="5" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 6" Value="6" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 7" Value="7" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 8" Value="8" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 9" Value="9" />
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
                <tr>
                    <td style="width:auto">
                        <div id="divGrid" style="overflow: auto;visibility:hidden;width:100%">
                            <table runat="server" id="tbBotoes" cellpadding="0" cellspacing="0"
                                style="width: 100%">
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
                                Width="100%" OnCustomCallback="grid_CustomCallback" 
                                OnCustomSummary="grid_CustomSummary">
                                <ClientSideEvents Init="function(s, e) {
                                    var height = Math.max(0, document.documentElement.clientHeight) - 200;
                                    s.SetHeight(height);
                                    document.getElementById('divGrid').style.visibility = '';
                                    }"/>
                                <Fields>
                                    <dxwpg:PivotGridField ID="colNomeProjeto" AllowedAreas="RowArea" Area="RowArea" AreaIndex="0"
                                        FieldName="NomeProjeto" Caption="Projeto">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="colCategoria" AllowedAreas="FilterArea"
                                        AreaIndex="0" FieldName="Categoria" Caption="Categoria" ExpandedInFieldsGroup="False" SummaryType="Average">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="colCriterio" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="1" FieldName="Criterio" Caption="Import&#226;ncia" Area="RowArea">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="colValorCriterio" AllowedAreas="DataArea" Area="DataArea"
                                        AreaIndex="0" CellFormat-FormatString="n2" CellFormat-FormatType="Numeric" FieldName="ValorCriterio"
                                        ValueFormat-FormatString="n2" ValueFormat-FormatType="Numeric" Caption="Valor Import&#226;ncia" SummaryType="Custom">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        Caption="Op&#231;&#227;o Import&#226;ncia" FieldName="OpcaoCriterio" Visible="False" AreaIndex="2">
                                    </dxwpg:PivotGridField>
                                </Fields>
                                <OptionsCustomization CustomizationWindowHeight="100" />
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
                                <OptionsLoadingPanel Text=" ">
                                </OptionsLoadingPanel>
                                <OptionsPager EllipsisMode="None">
                                </OptionsPager>
                                <OptionsView ShowColumnGrandTotals="False" ShowRowGrandTotals="False" />
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
