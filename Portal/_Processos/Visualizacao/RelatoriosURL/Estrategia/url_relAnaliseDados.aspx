<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relAnaliseDados.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Estrategia_url_relAnaliseDados"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
</head>
<body style="margin: 10px">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr style="height: 26px">
                <td valign="middle" style="padding-bottom: 5px">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr style="height: 26px">
                            <td valign="middle" style="padding-left: 10px">&nbsp;</td>
                            <td align="center" style="width: 120px; padding-right: 5px; padding-left: 5px">
                                <dxe:ASPxRadioButtonList ID="rbOpcao" runat="server" ClientInstanceName="rbOpcao"
                                    RepeatDirection="Horizontal" SelectedIndex="0">
                                    <Items>
                                        <dxe:ListEditItem Text="Mapa" Value="M" Selected="True"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Dado" Value="D"></dxe:ListEditItem>
                                    </Items>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetValue() == 'M')
	{
		ddlMapa.SetVisible(true);
		ddlDado.SetVisible(false);
	}
	else
	{
		ddlMapa.SetVisible(false);
		ddlDado.SetVisible(true);
	}	
}"></ClientSideEvents>
                                    <Paddings Padding="0px"></Paddings>
                                </dxe:ASPxRadioButtonList>
                            </td>
                            <td align="left" style="width: 320px">
                                <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa" ValueType="System.String" Width="100%">
                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Valor);
}"></ClientSideEvents>
                                </dxe:ASPxComboBox>
                                <dxe:ASPxComboBox ID="ddlDado" runat="server" ClientInstanceName="ddlDado" ClientVisible="False"
                                    ValueType="System.String" Width="100%">
                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Valor);
}"></ClientSideEvents>
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                    Text="Desempenho:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;"></td>
                            <td align="left" style="display: none; width: 8px; height: 26px;"></td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel40" runat="server"
                                    Text="Ano:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;"></td>
                            <td align="left" style="display: none; width: 8px; height: 26px;"></td>
                            <td align="right" style="display: none; width: 8px; height: 26px;"></td>
                            <td align="left" style="display: none; width: 8px; height: 26px;"></td>
                            <td align="left" style="display: none; width: 8px; height: 26px;"></td>
                            <td align="left" style="width: 80px; padding-left: 8px; height: 26px; padding-right: 5px;">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server"
                                    Text="Selecionar" Width="100%" AutoPostBack="False">
                                    <Paddings Padding="0px"></Paddings>
                                    <ClientSideEvents Click="function(s, e) {
	pvgDadosIndicador.PerformCallback();
}"></ClientSideEvents>
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>

                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgDadosIndicador"
                                                    ID="ASPxPivotGridExporter1">
                                                </dxpgwx:ASPxPivotGridExporter>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table runat="server" id="tbBotoes" cellpadding="0" cellspacing="0"
            style="width: 100%">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server" style="padding: 3px"
                    valign="top">
                    <table cellpadding="0" cellspacing="0" style="height: 22px">
                        <tr>
                            <td style="padding-right: 10px;">
                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                    ClientInstanceName="menu"
                                    ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                    OnInit="menu_Init">
                                    <Paddings Padding="0px" />
                                    <Items>
                                        <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                            <Image Url="~/imagens/botoes/incluirReg02.png">
                                            </Image>
                                        </dxm:MenuItem>
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
                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                    ClientVisible="False">
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
                                        <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                            <Items>
                                                <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                    <Image IconID="save_save_16x16">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout"
                                                    Name="btnRestaurarLayout">
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
        <div id="Div2" runat="server"
            style="overflow: auto; height: <%=alturaTabela %>; width: 100%;">
            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
            </dxhf:ASPxHiddenField>
            <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgDadosIndicador"
                Width="100%" ID="pvgDadosIndicador" OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort"
                OnCustomSummary="pvgDadosIndicador_CustomSummary" OnFieldAreaIndexChanged="pvgDadosIndicador_FieldAreaIndexChanged"
                OnFieldFilterChanged="pvgDadosIndicador_FieldFilterChanged" OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged"
                OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText" OnFieldAreaChanged="pvgDadosIndicador_FieldAreaChanged">
                <Fields>
                    <dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" AreaIndex="0" Caption="Unidade" UseNativeFormat="True">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="Dado" ID="fldDado" AllowedAreas="RowArea, ColumnArea"
                        Area="RowArea" AreaIndex="1" Caption="Dado" UseNativeFormat="True">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="Mes" ID="fldMes" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="ColumnArea" AreaIndex="1" Caption="M&#234;s" SortMode="Custom"
                        UseNativeFormat="True">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="Valor" ID="fldValor" AllowedAreas="DataArea" Area="DataArea"
                        AreaIndex="0" Caption="Valor" SummaryType="Custom" CellFormat-FormatType="Numeric"
                        TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Numeric"
                        ValueFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" UseNativeFormat="True">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="Ano" ID="fldAno" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="ColumnArea" AreaIndex="0" Caption="Ano" UseNativeFormat="True">
                    </dxwpg:PivotGridField>
                </Fields>
                <OptionsPager Visible="False">
                </OptionsPager>
                <OptionsFilter ShowOnlyAvailableItems="True" />
            </dxwpg:ASPxPivotGrid>
        </div>
    </form>
</body>
</html>
