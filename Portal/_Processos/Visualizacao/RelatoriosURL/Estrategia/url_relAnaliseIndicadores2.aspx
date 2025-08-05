<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relAnaliseIndicadores2.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Estrategia_url_relAnaliseIndicadores2"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 10px">
    <form id="form1" runat="server">
        <script language="javascript" type="text/javascript">

            function SalvarConfiguracoesLayout() {

                callback.PerformCallback("save_layout");
            }

            function RestaurarConfiguracoesLayout() {
                var funcObj = { funcaoClickOK: function () { callback.PerformCallback("restore_layout"); } }
                window.top.mostraConfirmacao('Deseja restaurar as configurações originais do layout da consulta?', function () { funcObj['funcaoClickOK']() }, null);
            }

        </script>


        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td>
                        <table id="tabelaFiltro" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 153px" align="center">
                                                        <dxe:ASPxCheckBox runat="server" Text="Valores Acumulados" ClientInstanceName="checkAcumulado"
                                                            Width="147px" Height="25px" ID="checkAcumulado"
                                                            Checked="True" CheckState="Checked">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td align="center"></td>
                                                    <td align="left">
                                                        <dxe:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" SelectedIndex="0"
                                                            ClientInstanceName="rbOpcao" Width="120px" Height="25px"
                                                            ID="rbOpcao">
                                                            <Paddings Padding="0px"></Paddings>
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetValue() == &quot;U&quot;)
	{
		ddlUnidade.SetVisible(true);
		ddlIndicador.SetVisible(false);
        ddlMapa.SetVisible(false);
	}
	else if(s.GetValue() == &quot;I&quot;)
	{
		ddlUnidade.SetVisible(false);
		ddlIndicador.SetVisible(true);
        ddlMapa.SetVisible(false);
	}
    else
	{
		ddlUnidade.SetVisible(false);
		ddlIndicador.SetVisible(false);
        ddlMapa.SetVisible(true);
	}	
}"></ClientSideEvents>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Mapa" Value="M" Selected="True" />
                                                                <dxe:ListEditItem Text="Unidade" Value="U"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Text="Indicador" Value="I"></dxe:ListEditItem>
                                                            </Items>
                                                        </dxe:ASPxRadioButtonList>
                                                    </td>
                                                    <td style="width: 5px" align="left"></td>
                                                    <td style="width: 256px" align="left">
                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="250px" Height="25px"
                                                            ClientInstanceName="ddlIndicador" ClientVisible="False"
                                                            ID="ddlIndicador">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Valor);
}"></ClientSideEvents>
                                                        </dxe:ASPxComboBox>
                                                        <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa" ClientVisible="False"
                                                            Height="25px" Width="250px">
                                                        </dxe:ASPxComboBox>
                                                        <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.String"
                                                            TextFormatString="{0}" Width="250px" Height="25px" ClientInstanceName="ddlUnidade"
                                                            ID="ddlUnidades" ClientVisible="False">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Valor);
}"></ClientSideEvents>
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="SiglaUnidadeNegocio" Width="200px" Caption="Sigla">
                                                                </dxe:ListBoxColumn>
                                                                <dxe:ListBoxColumn FieldName="NomeUnidadeNegocio" Width="350px" Caption="Unidade">
                                                                </dxe:ListBoxColumn>
                                                            </Columns>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td align="right"></td>
                                                    <td style="display: none; width: 8px; height: 61px" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="Desempenho:"
                                                            ID="ASPxLabel3">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                    <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                    <td style="display: none; width: 8px; height: 61px" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="Ano:" ID="ASPxLabel40">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                    <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                    <td style="display: none; width: 8px; height: 61px" align="right"></td>
                                                    <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                    <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                    <td style="width: 90px" align="left">
                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar" Width="100%"
                                                            Height="25px" ID="btnSelecionar">
                                                            <ClientSideEvents Click="function(s, e) {
	pvgDadosIndicador.PerformCallback(&#39;A&#39;);
}"></ClientSideEvents>
                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                        </dxhf:ASPxHiddenField>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 10px; padding-left: 10px"></td>
                </tr>
            </tbody>
        </table>
        <table runat="server" id="tbBotoes" cellpadding="0" cellspacing="0"
            style="width: 100%">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server" style="padding: 3px"
                    valign="top">
                    <table cellpadding="0" cellspacing="0" style="height: 22px">
                        <tr>
                            <td>
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
                                                <dxm:MenuItem Name="btnXLSX" Text="XLSX" ToolTip="Exportar para XLSX">
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
        <div id="Div2" runat="server" style="overflow: auto; width: 100%">

            <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgDadosIndicador"
                Width="100%" ID="pvgDadosIndicador" OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText"
                OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort" OnCustomSummary="pvgDadosIndicador_CustomSummary"
                OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged"
                OnCustomCallback="pvgDadosIndicador_CustomCallback" EncodeHtml="False">
                <Fields>
                </Fields>
                <OptionsView ShowColumnGrandTotals="False" ShowRowGrandTotals="False"
                    RowTotalsLocation="Tree"></OptionsView>
                <OptionsPager Position="Bottom">
                    <PageSizeItemSettings Caption="Itens/página:" Visible="True">
                    </PageSizeItemSettings>
                </OptionsPager>
                <Styles>
                    <HeaderStyle></HeaderStyle>
                    <AreaStyle>
                    </AreaStyle>
                    <FilterAreaStyle>
                    </FilterAreaStyle>
                    <DataAreaStyle>
                    </DataAreaStyle>
                    <RowAreaStyle>
                    </RowAreaStyle>
                    <FieldValueStyle>
                    </FieldValueStyle>
                    <FieldValueTotalStyle>
                    </FieldValueTotalStyle>
                    <FieldValueGrandTotalStyle>
                    </FieldValueGrandTotalStyle>
                    <CellStyle>
                    </CellStyle>
                    <TotalCellStyle>
                    </TotalCellStyle>
                    <GrandTotalCellStyle>
                    </GrandTotalCellStyle>
                    <CustomTotalCellStyle>
                    </CustomTotalCellStyle>
                    <FilterWindowStyle>
                    </FilterWindowStyle>
                    <FilterItemsAreaStyle>
                    </FilterItemsAreaStyle>
                    <FilterButtonStyle>
                    </FilterButtonStyle>
                    <FilterButtonPanelStyle>
                    </FilterButtonPanelStyle>
                    <MenuItemStyle>
                        <HoverStyle>
                        </HoverStyle>
                    </MenuItemStyle>
                    <MenuStyle></MenuStyle>
                    <CustomizationFieldsStyle>
                    </CustomizationFieldsStyle>
                    <CustomizationFieldsCloseButtonStyle>
                    </CustomizationFieldsCloseButtonStyle>
                    <CustomizationFieldsContentStyle>
                    </CustomizationFieldsContentStyle>
                    <CustomizationFieldsHeaderStyle>
                    </CustomizationFieldsHeaderStyle>
                </Styles>
                <StylesPager>
                    <Button>
                    </Button>
                    <DisabledButton>
                    </DisabledButton>
                    <CurrentPageNumber>
                    </CurrentPageNumber>
                    <PageNumber>
                    </PageNumber>
                    <PageSizeItem>
                    </PageSizeItem>
                    <Ellipsis>
                    </Ellipsis>
                </StylesPager>
                <StylesPrint Cell-Font="Verdana, 8.25pt" CustomTotalCell-Font="Verdana, 8.25pt" FieldHeader-Font="Verdana, 8.25pt"
                    FieldValue-Font="Verdana, 8.25pt" FieldValueGrandTotal-Font="Verdana, 8.25pt"
                    FieldValueTotal-Font="Verdana, 8.25pt" GrandTotalCell-Font="Verdana, 8.25pt"
                    Lines-Font="Verdana, 8.25pt" TotalCell-Font="Verdana, 8.25pt" />
                <StylesEditors>
                    <DropDownWindow>
                    </DropDownWindow>
                </StylesEditors>
            </dxwpg:ASPxPivotGrid>
        </div>
        <dxpgwx:ASPxPivotGridExporter ID="exporter" runat="server" OnCustomExportCell="exporter_CustomExportCell"
            ASPxPivotGridID="pvgDadosIndicador" OnCustomExportFieldValue="exporter_CustomExportFieldValue"
            OnCustomExportHeader="exporter_CustomExportHeader">
            <OptionsPrint>
                <PageSettings Landscape="True" PaperKind="A4" />
            </OptionsPrint>
        </dxpgwx:ASPxPivotGridExporter>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	if(e.parameter == &quot;export&quot;)
		window.location = &quot;../../ExportacaoDados.aspx?exportType=xls&amp;bInline=False&quot;;
	else if(e.parameter == &quot;restore_layout&quot;){
        window.location.reload(true);
    }
}" />
        </dxcb:ASPxCallback>
    </form>
</body>
</html>
