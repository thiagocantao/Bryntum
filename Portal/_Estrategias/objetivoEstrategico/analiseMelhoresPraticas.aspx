<%@ Page Language="C#" AutoEventWireup="true" CodeFile="analiseMelhoresPraticas.aspx.cs" Inherits="_Estrategias_objetivoEstrategico_analiseMelhoresPraticas" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/AnexoObjetivoEstrategico.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>

    <title>Anexos do Objetivo Estratégico</title>
    <style type="text/css">
        .style1 {
            width: 10px;
        }

        .style2 {
            height: 10px;
            width: 10px;
        }

        .style3 {
            height: 19px;
            width: 10px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td valign="top" class="style1"></td>
                            <td valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <%--                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server"  CssClass="campo-label" Text="Mapa Estratégico:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td style="width: 209px">
                                                    <asp:Label ID="lblPerspectiva" CssClass="campo-label" runat="server" 
                                                        Text="Perspectiva:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td style="width: 220px">
                                                    <asp:Label ID="Label6" CssClass="campo-label" runat="server"  Text="Tema:"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa" 
                                                        ReadOnly="True" Width="100%">
                                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                                        </ReadOnlyStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 220px">
                                                    <dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                                                         ReadOnly="True" Width="100%">
                                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                                        </ReadOnlyStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 220px">
                                                    <dxe:ASPxTextBox ID="txtTema" runat="server" ClientInstanceName="txtTema" 
                                                        ReadOnly="True" Width="100%">
                                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                                        </ReadOnlyStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="width: 5px; height: 5px">
                                    </td>
                                </tr>                                --%>
                                    <tr>
                                        <td>
                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblObjetivoEstrategico" CssClass="campo-label" runat="server" Text="Objetivo Estratégico:"></asp:Label></td>
                                                        <td
                                                            style="width: 10px"></td>
                                                        <td style="width: 280px">
                                                            <asp:Label ID="Label3" CssClass="campo-label" runat="server" Text="Responsável:"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtObjetivoEstrategico">
                                                                <ReadOnlyStyle BackColor="#E0E0E0">
                                                                </ReadOnlyStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10px"></td>
                                                        <td
                                                            style="width: 280px">
                                                            <dxe:ASPxTextBox ID="txtResponsavel" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtResponsavel">
                                                                <ReadOnlyStyle BackColor="#E0E0E0">
                                                                </ReadOnlyStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td style="width: 5px"></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="style1"></td>
                        </tr>
                        <tr>
                            <td valign="top" class="style2"></td>
                            <td valign="top"></td>
                            <td class="style1"></td>
                        </tr>
                        <tr>
                            <td valign="top" class="style3"></td>
                            <td colspan="1" valign="top">
                                <div id="digGrid" style="overflow: auto; width: <%=larguraTabela %>; height: <%=alturaTabela %>">
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

                                    <dxwpg:ASPxPivotGrid ID="gvMelhoresPraticas" runat="server" ClientInstanceName="gvMelhoresPraticas"
                                        Width="100%">
                                        <Fields>
                                            <dxwpg:PivotGridField ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                                AreaIndex="0" Caption="Unidade" FieldName="Unidade">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                Area="RowArea" AreaIndex="1" Caption="Nome do Indicador" FieldName="Indicador">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="field2" Area="RowArea" Caption="Status Indicador"
                                                FieldName="Desempenho" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="3" ExportBestFit="False" Options-AllowExpand="False" TotalsVisibility="None" Width="70">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                                <ValueStyle HorizontalAlign="Center">
                                                </ValueStyle>
                                                <ValueTotalStyle HorizontalAlign="Center">
                                                </ValueTotalStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="field3" Area="ColumnArea" AreaIndex="0" Caption="Utiliza Pr&#225;tica Sugerida"
                                                FieldName="AcaoSugerida1" AllowedAreas="RowArea, ColumnArea, FilterArea" ExportBestFit="False" Width="60">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="field4" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                Area="RowArea" AreaIndex="2" Caption="Iniciativa" FieldName="Projeto">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="field5" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0"
                                                Caption="Quantidade de Melhores Pr&#225;ticas" CellFormat-FormatString="#.###" CellFormat-FormatType="Numeric"
                                                FieldName="Quantidade" GrandTotalCellFormat-FormatString="#.###" GrandTotalCellFormat-FormatType="Numeric"
                                                TotalCellFormat-FormatString="#.###" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#.###"
                                                TotalValueFormat-FormatType="Numeric" ExportBestFit="False" Width="60">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="field6" AllowedAreas="RowArea" Area="RowArea" AreaIndex="3"
                                                Caption="A&#231;&#227;o Sugerida" FieldName="AcaoSugerida" Visible="False" Width="220">
                                            </dxwpg:PivotGridField>
                                        </Fields>
                                        <Styles>
                                            <HeaderStyle ForeColor="#575757" />
                                            <AreaStyle>
                                            </AreaStyle>
                                            <FilterAreaStyle>
                                            </FilterAreaStyle>
                                            <FieldValueStyle>
                                            </FieldValueStyle>
                                            <CellStyle>
                                            </CellStyle>
                                            <GrandTotalCellStyle>
                                            </GrandTotalCellStyle>
                                            <CustomTotalCellStyle>
                                            </CustomTotalCellStyle>
                                            <MenuItemStyle />
                                            <MenuStyle />
                                            <CustomizationFieldsStyle>
                                            </CustomizationFieldsStyle>
                                            <CustomizationFieldsHeaderStyle>
                                            </CustomizationFieldsHeaderStyle>
                                            <LoadingPanel>
                                            </LoadingPanel>
                                        </Styles>
                                        <OptionsLoadingPanel Text="Carregando&amp;hellip;">
                                            <Style></Style>
                                        </OptionsLoadingPanel>
                                        <OptionsPager Visible="False">
                                        </OptionsPager>
                                        <StylesEditors>
                                            <DropDownWindow>
                                            </DropDownWindow>
                                        </StylesEditors>
                                    </dxwpg:ASPxPivotGrid>
                                </div>
                            </td>
                            <td class="style3"></td>
                        </tr>
                    </table>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                    <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter2" runat="server" ASPxPivotGridID="gvMelhoresPraticas">
                        <OptionsPrint>
                            <PageSettings Landscape="True" Margins="30, 30, 30, 30" PaperKind="A4" />
                        </OptionsPrint>
                    </dxpgwx:ASPxPivotGridExporter>
                </td>
            </tr>
        </table>

        <script language="javascript" type="text/javascript">
            var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
            if (isIE8) {
                document.forms[0].style.overflow = "hidden";
            }
            else {
                document.forms[0].style.position = "relative";
                document.forms[0].style.overflow = "hidden";
            }

        </script>
    </form>
</body>
</html>


