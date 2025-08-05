<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relAnaliseExecucaoFluxos.aspx.cs" Inherits="_Processos_Relatorios_relAnaliseExecucaoFluxos" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-right: 20px;
            padding-left: 10px">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 99%">
                        <tr style="height:26px">
                            <td valign="middle" style="height: 26px">
                                &nbsp;
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Execução de Fluxos"></asp:Label></td>
                            <td valign="middle" style="padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Período:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxDateEdit ID="dteInicio" runat="server" Font-Overline="False"
                                    Width="105px" AllowNull="False" ClientInstanceName="dteInicio">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td style="width: 10px; padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="a" Width="10px">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxDateEdit ID="dteFim" runat="server" 
                                    Font-Strikeout="False" Width="105px" AllowNull="False" ClientInstanceName="dteFim">
                                    <ClientSideEvents ButtonClick="function(s, e) {
btnSelecionar.SetEnabled(true);
}" ValueChanged="function(s, e) {
	btnSelecionar.SetEnabled(true);
}" />
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 85px; height: 26px;" valign="middle">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                    AutoPostBack="False" Width="85px" ClientInstanceName="btnSelecionar">
<Paddings Padding="0px"></Paddings>

<ClientSideEvents Click="function(s, e) {
	grid.PerformCallback(&quot;PopularGrid&quot;);
}"></ClientSideEvents>
</dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 205px">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlExporta" runat="server" ClientInstanceName="ddlExporta"
                                                 ValueType="System.String">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td style="padding-left: 2px">
                                            <dxcp:ASPxCallbackPanel ID="pnImage" runat="server" ClientInstanceName="pnImage"
                                                Height="22px" HideContentOnCallback="False" OnCallback="pnImage_Callback" 
                                                 Width="23px">
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <dxe:ASPxImage ID="imgExportacao" runat="server" ClientInstanceName="imgExportacao"
                                                            Height="20px" ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px">
                                                        </dxe:ASPxImage>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxcp:ASPxCallbackPanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="Aspxbutton1" runat="server" 
                                    OnClick="btnExcel_Click" Text="Exportar">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                                <dxhf:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid">
                                </dxpgwx:ASPxPivotGridExporter>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
<div id="Div1" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                        <dxwpg:ASPxPivotGrid ID="grid" runat="server" ClientInstanceName="grid"
                            OnCustomFieldSort="grid_CustomFieldSort"
                            OnCustomCallback="grid_CustomCallback" CustomizationFieldsLeft="200" CustomizationFieldsTop="200" onafterperformcallback="grid_AfterPerformCallback" width="100%">
<OptionsCustomization CustomizationWindowWidth="210" CustomizationWindowHeight="280"></OptionsCustomization>

<ClientSideEvents BeginCallback="function(s, e) {
	hfGeral.Set('FoiMechido', '');
	if('S' == s.cp_Alterado)
		hfGeral.Set('FoiMechido', 'S');
}"></ClientSideEvents>
<Fields>
<dxwpg:PivotGridField FieldName="Fluxo" ID="campoFluxo" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" Caption="Fluxo"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Versao" ID="campoVersao" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Visible="False" Caption="Vers&#227;o"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Instancia" ID="campoInstancia" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Caption="Processo"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Projeto" ID="campoProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Visible="False" Caption="Projeto"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Etapa" ID="campoEtapa" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Caption="Etapa" ExpandedInFieldsGroup="False"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="FinalizadorEtapa" ID="campoFinalizadorEtapa" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Visible="False" Caption="Finalizador de Etapa"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="StatusProcesso" ID="campoStatusProcesso" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="0" Caption="Status do Processo"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="TempoDiasEtapa" ID="campoTempoDiasProcesso" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Tempo M&#233;dio  (d)" SummaryType="Average" CellFormat-FormatString="#,###.#" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,###.#" TotalValueFormat-FormatType="Numeric"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="TempoHorasEtapa" ID="campoTempoHorasProcesso" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Tempo M&#233;dio (h)" SummaryType="Average" CellFormat-FormatString="#,###.#" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,###.#" TotalValueFormat-FormatType="Numeric"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DescricaoTempoProcesso" ID="campoDescricaoTempoProcesso" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Tempo do Processo" SortMode="Custom"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="TempoDiasEtapa" ID="campoTempoDiasEtapa" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Tempo Total (d)" CellFormat-FormatString="#,###.#" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,###.#" TotalValueFormat-FormatType="Numeric"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="TempoHorasEtapa" ID="campoTempoHorasEtapa" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Tempo Total (h)" CellFormat-FormatString="#,###.#" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,###.#" TotalValueFormat-FormatType="Numeric"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="AtrasoDiasEtapa" ID="campoAtrasoDiasEtapa" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Atraso da Etapa (Dias)"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="AtrasoHorasEtapa" ID="campoAtrasoHorasEtapa" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Atraso da Etapa (Horas)"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DescricaoAtrasoEtapa" ID="campoAtrasoEtapa" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0" Caption="Atraso da Etapa" SortMode="Custom"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Etapa" ID="campoQtdEtapas" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Caption="N&#186; Etapas" SummaryType="Count"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="TipoEtapa" ID="campoTipoEtapa" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="1" Caption="Tipo Etapa"></dxwpg:PivotGridField>
</Fields>

<Styles>
<HeaderStyle ></HeaderStyle>

<AreaStyle ></AreaStyle>

<FilterAreaStyle ></FilterAreaStyle>

<FieldValueStyle ></FieldValueStyle>

<FieldValueTotalStyle ></FieldValueTotalStyle>

<FieldValueGrandTotalStyle ></FieldValueGrandTotalStyle>

<CellStyle ></CellStyle>

<GrandTotalCellStyle ></GrandTotalCellStyle>

<FilterWindowStyle ></FilterWindowStyle>

<FilterButtonPanelStyle ></FilterButtonPanelStyle>

<MenuItemStyle ></MenuItemStyle>

<MenuStyle ></MenuStyle>

<CustomizationFieldsContentStyle ></CustomizationFieldsContentStyle>
</Styles>

<OptionsLoadingPanel Text=" "></OptionsLoadingPanel>

<OptionsPager EllipsisMode="None"></OptionsPager>
</dxwpg:ASPxPivotGrid>
                        &nbsp;&nbsp;&nbsp;</div>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" language="javascript">
       var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
       if(isIE8)
       {
            document.forms[0].style.overflow = "hidden";
       }
       else
       {
            document.forms[0].style.position = "relative";
            document.forms[0].style.overflow = "hidden";       
       }

    </script>

    <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral"></dxhf:aspxhiddenfield>
</asp:Content>

