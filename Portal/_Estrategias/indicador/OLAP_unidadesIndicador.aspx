<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OLAP_unidadesIndicador.aspx.cs"
    Inherits="_Estrategias_indicador_OLAP_unidadesIndicador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>

    <title>Untitled Page</title>
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

        .auto-style1 {
            height: 5px;
        }
    </style>
</head>
<body class="body">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr style="height: 26px">
                    <td valign="middle" style="height: 26px">
                        <table>
                            <tr>
                                <td>&nbsp;<dxe:ASPxLabel ID="lblTitulo" CssClass="lblTituloTela" runat="server" ClientInstanceName="lblTituloSelecao"
                                    Font-Bold="True" Text="Unidades do Indicador">
                                </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 5px" valign="top"></td>
                            <td valign="top">
                                <table cellpadding="0" cellspacing="0" class="formulario" width="100%">
                                    <tr>
                                        <td id="tdObjetivoMapa" runat="server">
                                            <table cellpadding="0" cellspacing="0" width="100%" class="headerGrid formulario-colunas">
                                                <tr class="formulario-linha">
                                                    <%--                                                    <td class="formulario-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                            Text="Mapa Estratégico:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="formulario-labe">
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                            Text="Objetivo Estratégico:">
                                                        </dxe:ASPxLabel>
                                                    </td>--%>
                                                    <td class="formulario-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                            Text="Indicador:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <%--                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientEnabled="False"
                                                            Width="100%">
                                                            <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" ClientEnabled="False"
                                                            Width="100%">
                                                            <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxTextBox>
                                                    </td>--%>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientEnabled="False"
                                                            Width="100%">
                                                            <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgDadosIndicador"
                                                OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                                            </dxpgwx:ASPxPivotGridExporter>
                                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                            </dxhf:ASPxHiddenField>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px" valign="top"></td>
                            <td valign="top"></td>
                        </tr>
                        <tr>
                            <td style="width: 5px" valign="top"></td>
                            <td valign="top">
                                <div id="Div2" style="overflow: auto; height: <%=alturaDivGrid %>; width: <%=larguraDivGrid %>">
                                    <table runat="server" id="tbBotoes" cellpadding="0" cellspacing="0"
                                        style="width: 100%">
                                        <tr runat="server">
                                            <td runat="server" style="padding: 3px"
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

                                    <dxwpg:ASPxPivotGrid ID="pvgDadosIndicador" runat="server" ClientInstanceName="pvgDadosIndicador"
                                        OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText" OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort"
                                        OnCustomSummary="pvgDadosIndicador_CustomSummary" OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged"
                                        Width="100%" ClientIDMode="AutoID" OnHtmlCellPrepared="pvgDadosIndicador_HtmlCellPrepared" OnHtmlFieldValuePrepared="pvgDadosIndicador_HtmlFieldValuePrepared">
                                        <Fields>
                                            <dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" Caption="Unidade" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="MesPorExtenso" ID="fldMes" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Visible="False" Caption="M&#234;s" SortMode="Custom" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="Ano" ID="fldAno" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0" Caption="Ano" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="Indicador" ID="fldIndicador" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Visible="False" Caption="Indicador">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="Meta" ID="fldMeta" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Meta" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="Resultado" ID="fldResultado" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Resultado" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="MetaAcumulada" ID="fldMetaAcumulada" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Caption="Meta Acumulada" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="ResultadoAcumulado" ID="fldResultadoAcumulado" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1" Caption="Resultado Acumulado" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="DesempenhoAcumulado" ID="fldDesempenhoAcumulado" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2" Caption="Status Acumulado" SummaryType="Custom" KPIGraphic="TrafficLights">
                                                <CellStyle HorizontalAlign="Center"></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="Desempenho" ID="fldDesempenho" AllowedAreas="DataArea" Area="DataArea" AreaIndex="3" Visible="False" Caption="Status" SummaryType="Custom">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxpgwx:PivotGridField ID="fieldDecimais" AllowedAreas="RowArea" Area="RowArea" AreaIndex="1" Caption="Decimais" FieldName="Decimais" Visible="False">
                                            </dxpgwx:PivotGridField>
                                            <dxpgwx:PivotGridField ID="fieldMedida" AllowedAreas="RowArea" Area="RowArea" AreaIndex="1" FieldName="Medida" Visible="False">
                                            </dxpgwx:PivotGridField>
                                        </Fields>

                                        <Styles>
                                            <HeaderStyle ForeColor="#575757"></HeaderStyle>

                                            <AreaStyle></AreaStyle>

                                            <FilterAreaStyle></FilterAreaStyle>

                                            <DataAreaStyle>
                                            </DataAreaStyle>
                                            <ColumnAreaStyle>
                                            </ColumnAreaStyle>
                                            <FieldValueStyle>
                                            </FieldValueStyle>
                                            <FieldValueTotalStyle>
                                            </FieldValueTotalStyle>
                                            <FieldValueGrandTotalStyle>
                                            </FieldValueGrandTotalStyle>

                                            <CellStyle></CellStyle>

                                            <TotalCellStyle></TotalCellStyle>

                                            <GrandTotalCellStyle></GrandTotalCellStyle>

                                            <CustomTotalCellStyle></CustomTotalCellStyle>

                                            <FilterWindowStyle></FilterWindowStyle>

                                            <FilterItemStyle></FilterItemStyle>

                                            <MenuItemStyle></MenuItemStyle>

                                            <CustomizationFieldsHeaderStyle></CustomizationFieldsHeaderStyle>
                                        </Styles>

                                        <OptionsView ShowColumnTotals="False" ShowRowTotals="False" ShowColumnGrandTotals="False" ShowRowGrandTotals="False"></OptionsView>

                                        <OptionsPager Visible="False"></OptionsPager>
                                    </dxwpg:ASPxPivotGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
<script type="text/javascript" language="javascript">
    var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
    if (isIE8) {
        //document.getElementById('indicador_desktop')
        document.forms[0].style.overflow = "hidden";
    }
    else {
        document.forms[0].style.position = "relative";
        document.forms[0].style.overflow = "hidden";
    }

</script>
</html>
