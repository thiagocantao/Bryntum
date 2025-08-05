<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseDados.aspx.cs" Inherits="_relAnaliseDados" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle">
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                 Font-Overline="False" Font-Strikeout="False"
                                Text="Análise de Dados"></asp:Label>
                        </td>
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
                            <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa" 
                                ValueType="System.String" Width="100%">
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
                        <td align="left" style="display: none; width: 8px; height: 26px;">
                        </td>
                        <td align="left" style="display: none; width: 8px; height: 26px;">
                        </td>
                        <td align="right" style="display: none; width: 8px; height: 26px;">
                            <dxe:ASPxLabel ID="ASPxLabel40" runat="server" 
                                Text="Ano:">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="left" style="display: none; width: 8px; height: 26px;">
                        </td>
                        <td align="left" style="display: none; width: 8px; height: 26px;">
                        </td>
                        <td align="right" style="display: none; width: 8px; height: 26px;">
                        </td>
                        <td align="left" style="display: none; width: 8px; height: 26px;">
                        </td>
                        <td align="left" style="display: none; width: 8px; height: 26px;">
                        </td>
                        <td align="left" style="width: 80px; padding-left: 8px; height: 26px; padding-right: 5px;">
                            <dxe:ASPxButton ID="btnSelecionar" runat="server" 
                                Text="Selecionar" Width="100%" AutoPostBack="False">
                                <Paddings Padding="0px"></Paddings>
                                <ClientSideEvents Click="function(s, e) {
	pnCallbackDados.PerformCallback();
}"></ClientSideEvents>
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="padding-right: 10px; padding-left: 10px">
                            <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" ClientInstanceName="pnCallbackDados"
                                OnCallback="pnCallbackDados_Callback" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="padding-bottom: 5px; padding-top: 10px">
                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" ClientInstanceName="ddlExporta"
                                                                             ID="ddlExporta">
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set('tipoArquivo', s.GetValue());
}"></ClientSideEvents>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td style="padding-left: 2px">
                                                                        <dxcp:ASPxCallbackPanel runat="server"  
                                                                            ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
                                                                            <PanelCollection>
                                                                                <dxp:PanelContent runat="server">
                                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                                                        Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao">
                                                                                    </dxe:ASPxImage>
                                                                                </dxp:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxcp:ASPxCallbackPanel>
                                                                    </td>
                                                                    <td style="padding-left: 10px">
                                                                        <dxe:ASPxButton runat="server" Text="Exportar" 
                                                                            ID="Aspxbutton1" OnClick="btnExcel_Click">
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
                                                <tr>
                                                    <td>
                                                        <dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgDadosIndicador"
                                                            ID="ASPxPivotGridExporter1">
                                                        </dxpgwx:ASPxPivotGridExporter>
                                                        <div id="Div2" runat="server"
                                                            style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>;">
                                                            <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgDadosIndicador"
                                                                 Width="100%"
                                                                ID="pvgDadosIndicador" OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort"
                                                                OnCustomSummary="pvgDadosIndicador_CustomSummary" OnFieldAreaIndexChanged="pvgDadosIndicador_FieldAreaIndexChanged"
                                                                OnFieldFilterChanged="pvgDadosIndicador_FieldFilterChanged" OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged"
                                                                OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText" OnFieldAreaChanged="pvgDadosIndicador_FieldAreaChanged">
                                                                <Fields>
                                                                    <dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                        Area="RowArea" AreaIndex="0" Caption="Unidade" UseNativeFormat="True">
                                                                        <CellStyle >
                                                                        </CellStyle>
                                                                        <HeaderStyle ></HeaderStyle>
                                                                        <ValueStyle >
                                                                        </ValueStyle>
                                                                        <ValueTotalStyle >
                                                                        </ValueTotalStyle>
                                                                    </dxwpg:PivotGridField>
                                                                    <dxwpg:PivotGridField FieldName="Dado" ID="fldDado" AllowedAreas="RowArea, ColumnArea"
                                                                        Area="RowArea" AreaIndex="1" Caption="Dado" UseNativeFormat="True">
                                                                        <CellStyle >
                                                                        </CellStyle>
                                                                        <HeaderStyle ></HeaderStyle>
                                                                        <ValueStyle >
                                                                        </ValueStyle>
                                                                        <ValueTotalStyle >
                                                                        </ValueTotalStyle>
                                                                    </dxwpg:PivotGridField>
                                                                    <dxwpg:PivotGridField FieldName="Mes" ID="fldMes" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                        Area="ColumnArea" AreaIndex="1" Caption="M&#234;s" SortMode="Custom"
                                                                        UseNativeFormat="True">
                                                                        <CellStyle >
                                                                        </CellStyle>
                                                                        <HeaderStyle ></HeaderStyle>
                                                                        <ValueStyle >
                                                                        </ValueStyle>
                                                                        <ValueTotalStyle >
                                                                        </ValueTotalStyle>
                                                                    </dxwpg:PivotGridField>
                                                                    <dxwpg:PivotGridField FieldName="Valor" ID="fldValor" AllowedAreas="DataArea" Area="DataArea"
                                                                        AreaIndex="0" Caption="Valor" SummaryType="Custom" CellFormat-FormatType="Numeric"
                                                                        TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Numeric"
                                                                        ValueFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" UseNativeFormat="True">
                                                                        <CellStyle >
                                                                        </CellStyle>
                                                                        <HeaderStyle ></HeaderStyle>
                                                                        <ValueStyle >
                                                                        </ValueStyle>
                                                                        <ValueTotalStyle >
                                                                        </ValueTotalStyle>
                                                                    </dxwpg:PivotGridField>
                                                                    <dxwpg:PivotGridField FieldName="Ano" ID="fldAno" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                        Area="ColumnArea" AreaIndex="0" Caption="Ano" UseNativeFormat="True">
                                                                        <CellStyle >
                                                                        </CellStyle>
                                                                        <HeaderStyle ></HeaderStyle>
                                                                        <ValueStyle >
                                                                        </ValueStyle>
                                                                        <ValueTotalStyle >
                                                                        </ValueTotalStyle>
                                                                    </dxwpg:PivotGridField>
                                                                </Fields>
                                                                <OptionsView HorizontalScrollBarMode="Visible"></OptionsView>
                                                                <OptionsPager Visible="False">
                                                                </OptionsPager>
                                                                <Styles>
                                                                    <AreaStyle >
                                                                    </AreaStyle>
                                                                    <MenuItemStyle ></MenuItemStyle>
                                                                    <MenuStyle ></MenuStyle>
                                                                </Styles>
                                                            </dxwpg:ASPxPivotGrid>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxcp:ASPxCallbackPanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
