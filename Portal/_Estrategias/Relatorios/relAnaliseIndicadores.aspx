<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relAnaliseIndicadores.aspx.cs"
    Inherits="_relAnaliseIndicadores" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr style="height: 26px">
            <td valign="middle">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="height: 26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="<%# Resources.traducao.relAnaliseIndicadores_an_lise_de_indicadores %>"></asp:Label></td>
                        <td align="right" style="display: none; width: 8px; height: 26px;"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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

    <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" ClientInstanceName="pnCallbackDados"
        Width="100%" OnCallback="pnCallbackDados_Callback">
        <PanelCollection>
            <dxp:PanelContent runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 5px; padding-top: 10px">
                                <table id="tabelaFiltro" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 153px" align="center">
                                                                <dxe:ASPxCheckBox runat="server" Text="Valores Acumulados" ClientInstanceName="checkAcumulado" Width="147px" Height="25px" ID="checkAcumulado"></dxe:ASPxCheckBox>
                                                            </td>
                                                            <td align="center"></td>
                                                            <td align="left">
                                                                <dxe:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" SelectedIndex="0" ClientInstanceName="rbOpcao" Width="120px" Height="25px" ID="rbOpcao">
                                                                    <Paddings Padding="0px"></Paddings>

                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetValue() == &quot;U&quot;)
	{
		ddlUnidade.SetVisible(true);
		ddlIndicador.SetVisible(false);
	}
	else
	{
		ddlUnidade.SetVisible(false);
		ddlIndicador.SetVisible(true);
	}	
}"></ClientSideEvents>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Unidade" Value="U" Selected="True"></dxe:ListEditItem>
                                                                        <dxe:ListEditItem Text="Indicador" Value="I"></dxe:ListEditItem>
                                                                    </Items>
                                                                </dxe:ASPxRadioButtonList>
                                                            </td>
                                                            <td style="width: 5px" align="left"></td>
                                                            <td style="width: 256px" align="left">
                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="250px" Height="25px" ClientInstanceName="ddlIndicador" ClientVisible="False" ID="ddlIndicador">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Valor);
}"></ClientSideEvents>
                                                                </dxe:ASPxComboBox>
                                                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.String" TextFormatString="{0}" Width="250px" Height="25px" ClientInstanceName="ddlUnidade" ID="ddlUnidades">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Valor);
}"></ClientSideEvents>
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn FieldName="SiglaUnidadeNegocio" Width="200px" Caption="Sigla"></dxe:ListBoxColumn>
                                                                        <dxe:ListBoxColumn FieldName="NomeUnidadeNegocio" Width="350px" Caption="Unidade"></dxe:ListBoxColumn>
                                                                    </Columns>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td style="width: 10px" align="right"></td>
                                                            <td style="display: none; width: 8px; height: 61px" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="Desempenho:" ID="ASPxLabel3"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                            <td style="display: none; width: 8px; height: 61px" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="Ano:" ID="ASPxLabel40"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                            <td style="display: none; width: 8px; height: 61px" align="right"></td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left"></td>
                                                            <td style="width: 91px" align="left">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar" Width="84px" Height="25px" ID="btnSelecionar">
                                                                    <ClientSideEvents Click="function(s, e) {
	pnCallbackDados.PerformCallback('A');
}" />

                                                                    <ClientSideEvents Click="function(s, e) {
	pnCallbackDados.PerformCallback(&#39;A&#39;);
}"></ClientSideEvents>

                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                                            </td>
                                            <td align="right">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="172px" Height="25px" ClientInstanceName="ddlExporta" ID="ddlExporta">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}"></ClientSideEvents>

                                                                                    <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-left: 2px">
                                                                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
                                                                                    <PanelCollection>
                                                                                        <dxp:PanelContent runat="server">
                                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao"></dxe:ASPxImage>



                                                                                        </dxp:PanelContent>
                                                                                    </PanelCollection>
                                                                                </dxcp:ASPxCallbackPanel>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButton runat="server" Text="Exportar" Width="65px" Height="25px" ID="btnExcel" OnClick="btnExcel_Click">
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px">
                                <div id="Div2" runat="server" style="overflow: auto;">
                                    <dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgDadosIndicador" ID="ASPxPivotGridExporter1" OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell"></dxpgwx:ASPxPivotGridExporter>
                                    <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgDadosIndicador"
                                        Width="100%"
                                        ID="pvgDadosIndicador"
                                        OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText"
                                        OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort"
                                        OnCustomSummary="pvgDadosIndicador_CustomSummary"
                                        OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged"
                                        EnableRowsCache="False" EnableViewState="False" Height="100%" OnHtmlCellPrepared="pvgDadosIndicador_HtmlCellPrepared" OnHtmlFieldValuePrepared="pvgDadosIndicador_HtmlFieldValuePrepared">
                                        <Fields>
                                            <dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" AllowedAreas="RowArea" Options-AllowDrag="False" Area="RowArea" AreaIndex="0" Caption="Unidade" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="MesPorExtenso" ID="fldMes" AllowedAreas="ColumnArea" Area="ColumnArea" AreaIndex="1" Caption="M&#234;s" SortMode="Custom" UseNativeFormat="True">
                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="Ano" ID="fldAno" AllowedAreas="ColumnArea" Options-AllowDrag="False" Area="ColumnArea" AreaIndex="0" Caption="Ano" UseNativeFormat="True">
                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField FieldName="Indicador" ID="fldIndicador" AllowedAreas="RowArea" Options-AllowDrag="False" Area="RowArea" AreaIndex="1" Caption="Indicador">
                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="fldMeta" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Caption="Meta" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="fldResultado" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1" Caption="Resultado" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                            <dxwpg:PivotGridField ID="fldDesempenho" AllowedAreas="DataArea" Width="20" ExportBestFit="False" Area="DataArea" AreaIndex="2" Caption="Status" SummaryType="Custom" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" CellFormat-FormatType="Custom" ValueFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" ValueFormat-FormatType="Custom">
                                                <CellStyle></CellStyle>

                                                <HeaderStyle></HeaderStyle>

                                                <ValueStyle></ValueStyle>
                                            </dxwpg:PivotGridField>
                                        </Fields>

                                        <OptionsView ShowRowTotals="False" ShowColumnGrandTotals="False"
                                            ShowRowGrandTotals="False" ColumnTotalsLocation="Near"
                                            HorizontalScrollBarMode="Visible"></OptionsView>

                                        <OptionsPager Position="Bottom" RowsPerPage="7" NumericButtonCount="7">
                                        </OptionsPager>

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

                                        <StylesEditors>
                                            <DropDownWindow></DropDownWindow>
                                        </StylesEditors>
                                        <Border BorderStyle="None"></Border>
                                        <BorderLeft BorderColor="#dddddd" BorderStyle="Solid" BorderWidth="1px" />
                                        <BorderTop BorderColor="#dddddd" BorderStyle="Solid" BorderWidth="1px" />
                                        <BorderRight BorderStyle="Solid" BorderWidth="1px" />
                                        <BorderBottom BorderColor="#dddddd" BorderStyle="Solid" BorderWidth="1px" />

                                        <BorderLeft BorderColor="#dddddd" BorderStyle="Solid" BorderWidth="1px"></BorderLeft>

                                        <BorderTop BorderColor="#dddddd" BorderStyle="Solid" BorderWidth="1px"></BorderTop>

                                        <BorderRight BorderStyle="Solid" BorderWidth="1px"></BorderRight>

                                        <BorderBottom BorderColor="#dddddd" BorderStyle="Solid" BorderWidth="1px"></BorderBottom>
                                    </dxwpg:ASPxPivotGrid>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
</asp:Content>
