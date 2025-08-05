<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Financeiro.aspx.cs" Inherits="_Projetos_DadosProjeto_Financeiro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Financeiro - Projeto</title>

    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script language="javascript" type="text/javascript">
        function reziseDiv() {
            if (window.parent.sp_Tela) {
                pnDiv.SetWidth(window.parent.sp_Tela.GetPane(1).lastWidth - 15);
            }
        }
    </script>
    <style type="text/css">
        .style1 {
            width: 41px;
        }

        .dxmMenu,
        .dxmVerticalMenu {
            font: 12px Tahoma, Geneva, sans-serif;
            color: black;
            background-color: #F0F0F0;
            border: 1px solid #A8A8A8;
            padding: 2px;
        }

        .dxmMenuItem,
        .dxmMenuItemWithImage {
            padding: 4px 8px 5px;
        }

        .dxmMenuItem,
        .dxmMenuItemWithImage,
        .dxmMenuItemWithPopOutImage,
        .dxmMenuItemWithImageWithPopOutImage,
        .dxmVerticalMenuItem,
        .dxmVerticalMenuItemWithImage,
        .dxmVerticalMenuItemWithPopOutImage,
        .dxmVerticalMenuItemWithImageWithPopOutImage,
        .dxmVerticalMenuRtlItem,
        .dxmVerticalMenuRtlItemWithImage,
        .dxmVerticalMenuRtlItemWithPopOutImage,
        .dxmVerticalMenuRtlItemWithImageWithPopOutImage,
        .dxmMenuLargeItem,
        .dxmMenuLargeItemWithImage,
        .dxmMenuLargeItemWithPopOutImage,
        .dxmMenuLargeItemWithImageWithPopOutImage,
        .dxmVerticalMenuLargeItem,
        .dxmVerticalMenuLargeItemWithImage,
        .dxmVerticalMenuLargeItemWithPopOutImage,
        .dxmVerticalMenuLargeItemWithImageWithPopOutImage,
        .dxmVerticalMenuLargeRtlItem,
        .dxmVerticalMenuLargeRtlItemWithImage,
        .dxmVerticalMenuLargeRtlItemWithPopOutImage,
        .dxmVerticalMenuLargeRtlItemWithImageWithPopOutImage {
            white-space: nowrap;
        }

        .dxmMenuItemSpacing,
        .dxmMenuLargeItemSpacing,
        .dxmMenuItemSeparatorSpacing,
        .dxmMenuLargeItemSeparatorSpacing {
            width: 2px;
        }
    </style>
</head>
<body style="overflow: hidden; margin: 0" onload="reziseDiv()">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td style="width: 5px; height: 5px"></td>
                    <td style="height: 10px"></td>
                </tr>
                <tr>
                    <td style="width: 5px">&nbsp;</td>
                    <td>
                        <table cellpadding="0" cellspacing="0" runat="server" id="tbBotoes0"
                            style="width: 100%">
                            <tr>
                                <td valign="top">&nbsp;</td>
                                <td align="right" class="style1">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                        Text="Tipo:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" style="width: 88px; padding-right: 20px;">
                                    <dxe:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo"
                                        SelectedIndex="0" Width="100px">
                                        <Items>
                                            <dxe:ListEditItem Selected="True" Text="Despesa" Value="D" />
                                            <dxe:ListEditItem Text="Receita" Value="R" />
                                        </Items>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pvgFinanceiro.PerformCallback();
}" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5px"></td>
                    <td>
                        <dxp:ASPxPanel ID="pnDiv" ClientInstanceName="pnDiv" runat="server" Width="100%" ScrollBars="Auto">
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
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
                                                        <td style="width: 30px">
                                                            <dxe:ASPxImage ID="imgGraficos" runat="server"
                                                                ImageUrl="~/imagens/botoes/btnCurvaS.png" ToolTip="Curva S de Orçamento do Projeto">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td style="width: 45px"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <dxwpg:ASPxPivotGrid ID="pvgFinanceiro" runat="server"
                                        ClientInstanceName="pvgFinanceiro"
                                        OnCustomCellStyle="pvgFinanceiro_CustomCellStyle"
                                        OnFieldAreaChanged="pvgFinanceiro_FieldAreaChanged"
                                        OnFieldAreaIndexChanged="pvgFinanceiro_FieldAreaIndexChanged"
                                        OnFieldVisibleChanged="pvgFinanceiro_FieldVisibleChanged"
                                        OnCustomCallback="pvgFinanceiro_CustomCallback" Width="100%"
                                        OnCustomFieldSort="pvgFinanceiro_CustomFieldSort" ClientIDMode="AutoID">
                                        <Fields>
                                            <dxwpg:PivotGridField ID="campoAno" Area="ColumnArea" AreaIndex="0" Caption="Ano"
                                                FieldName="Ano" AllowedAreas="RowArea, ColumnArea, FilterArea">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoMes" Area="RowArea" AreaIndex="1" Caption="M&#234;s"
                                                FieldName="Mes" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                SortMode="Custom">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoGrupo" AreaIndex="1" Caption="Grupo"
                                                FieldName="Grupo" AllowedAreas="RowArea, ColumnArea, FilterArea">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoConta" Area="RowArea" AreaIndex="0"
                                                Caption="Conta" FieldName="Conta"
                                                AllowedAreas="RowArea, ColumnArea, FilterArea">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoDespesaReceita" AreaIndex="0" FieldName="DespesaReceita" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Despesa/Receita">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoValor" Area="DataArea"
                                                Caption="Valor Tend&#234;ncia" CellFormat-FormatString="{0:c2}"
                                                CellFormat-FormatType="Custom" FieldName="Valor" GrandTotalCellFormat-FormatString="{0:c2}"
                                                GrandTotalCellFormat-FormatType="Custom" AllowedAreas="DataArea"
                                                Visible="False" AreaIndex="2">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoValorLB" Area="DataArea" AreaIndex="0" Caption="Valor Previsto"
                                                CellFormat-FormatString="{0:c2}" CellFormat-FormatType="Custom" FieldName="ValorLB"
                                                GrandTotalCellFormat-FormatString="{0:c2}"
                                                GrandTotalCellFormat-FormatType="Custom" AllowedAreas="DataArea">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoValorReal" Area="DataArea" AreaIndex="1" Caption="Valor Real"
                                                CellFormat-FormatString="{0:c2}" CellFormat-FormatType="Custom" FieldName="ValorReal"
                                                GrandTotalCellFormat-FormatString="{0:c2}"
                                                GrandTotalCellFormat-FormatType="Custom" AllowedAreas="DataArea">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoValorHE" Area="DataArea" Caption="Valor Horas Extras"
                                                CellFormat-FormatString="{0:c2}" CellFormat-FormatType="Custom" FieldName="ValorHE" AllowedAreas="DataArea" GrandTotalCellFormat-FormatString="{0:c2}" GrandTotalCellFormat-FormatType="Custom" Visible="False" AreaIndex="3">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoVariacaoValor" Area="DataArea" Caption="Varia&#231;&#227;o"
                                                CellFormat-FormatString="{0:c2}" CellFormat-FormatType="Custom" FieldName="VariacaoValor"
                                                GrandTotalCellFormat-FormatString="{0:c2}"
                                                GrandTotalCellFormat-FormatType="Custom" AllowedAreas="DataArea"
                                                Visible="False" AreaIndex="2">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoValorRestante" Area="DataArea" Caption="Valor Restante"
                                                CellFormat-FormatString="{0:c2}" CellFormat-FormatType="Custom" FieldName="ValorRestante"
                                                GrandTotalCellFormat-FormatString="{0:c2}" GrandTotalCellFormat-FormatType="Custom" AllowedAreas="DataArea" Visible="False" AreaIndex="3">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="fieldNomeCR"
                                                AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2"
                                                Caption="CR" FieldName="NomeCR" Visible="False">
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="campoValorPrevistoData" AllowedAreas="DataArea"
                                                Area="DataArea" AreaIndex="2" Caption="Valor Previsto até a Data"
                                                CellFormat-FormatString="{0:c2}" CellFormat-FormatType="Custom"
                                                FieldName="ValorPrevistoData" GrandTotalCellFormat-FormatString="{0:c2}"
                                                GrandTotalCellFormat-FormatType="Custom">
                                            </dxwpg:PivotGridField>
                                        </Fields>
                                        <Groups>
                                            <dxwpg:PivotGridWebGroup />
                                        </Groups>
                                        <OptionsView HorizontalScrollBarMode="Visible" />
                                        <OptionsPager Position="Top" RowsPerPage="9" ShowDefaultImages="False" Visible="False">
                                            <Summary AllPagesText="P&#225;ginas: {0} - {1} ({2} &#237;tens)" Text="P&#225;gina {0} de {1} ({2} &#237;tens)" />
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
                                    </dxwpg:ASPxPivotGrid>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                    </td>
                </tr>
            </table>
        </div>
        <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgFinanceiro"></dxpgwx:ASPxPivotGridExporter>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
    <script type="text/javascript" language="javascript">
        var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
        if (isIE8) {
            document.forms[0].style.overflow = "hidden";
        }
        else {
            document.forms[0].style.position = "relative";
            document.forms[0].style.overflow = "hidden";
        }

    </script>
</body>
</html>
