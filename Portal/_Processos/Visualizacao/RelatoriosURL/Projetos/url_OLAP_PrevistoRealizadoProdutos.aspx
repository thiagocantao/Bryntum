<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_OLAP_PrevistoRealizadoProdutos.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_OLAP_PrevistoRealizadoProdutos"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-right: 20px;
            padding-left: 10px">
            <tr>
                <td style="padding-top: 0px; background-image:none);
                    width: 98%; height: 26px;">
                    <table>
                        <tr style="height:26px">
                            <td valign="middle" style="padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Período:" Visible="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxDateEdit ID="dteInicio" runat="server" Font-Overline="False"
                                    Width="105px" AllowNull="False" ClientInstanceName="dteInicio"
                                    Visible="False">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td style="width: 10px; padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="a" Width="10px" Visible="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxDateEdit ID="dteFim" runat="server" 
                                    Font-Strikeout="False" Width="105px" AllowNull="False" ClientInstanceName="dteFim"
                                    Visible="False">
                                    <ClientSideEvents ButtonClick="function(s, e) {
btnSelecionar.SetEnabled(true);
}" ValueChanged="function(s, e) {
	btnSelecionar.SetEnabled(true);
}" />
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 105px; height: 26px;" valign="middle">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                    AutoPostBack="False" Width="100px" ClientInstanceName="btnSelecionar"
                                    Visible="False">
                                    <Paddings Padding="0px"></Paddings>
                                    <ClientSideEvents Click="function(s, e) {
	grid.PerformCallback(&quot;PopularGrid&quot;);
}"></ClientSideEvents>
                                </dxe:ASPxButton>
                            </td>
<td>
                    <table border="0" cellpadding="0" cellspacing="0" 
                        width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right">
                                            <dxe:ASPxComboBox ID="ddlExporta" runat="server" ClientInstanceName="ddlExporta"
                                                 ValueType="System.String">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td style="padding-left: 2px" width="10">
                                            <dxcp:ASPxCallbackPanel ID="pnImage" runat="server" ClientInstanceName="pnImage"
                                                Height="22px" HideContentOnCallback="False" OnCallback="pnImage_Callback" 
                                                 Width="23px">
                                                <PanelCollection>
                                                    <dxp:PanelContent ID="PanelContent1" runat="server">
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
                            <td align="right" width="100%">
                                <dxe:ASPxButton ID="Aspxbutton1" runat="server" 
                                    OnClick="btnExcel_Click" Text="Exportar" Width="100px">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td width="10">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ClientInstanceName="ImgHelp"
                                    Cursor="pointer" ID="ImgHelp">
                                </dxe:ASPxImage>
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
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                                <dxhf:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid">
                                </dxpgwx:ASPxPivotGridExporter>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="Div1" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                        <dxwpg:ASPxPivotGrid ID="grid" runat="server"  
                             ClientInstanceName="grid"
                            OnCustomCallback="grid_CustomCallback" CustomizationFieldsLeft="200" CustomizationFieldsTop="200"
                            OnAfterPerformCallback="grid_AfterPerformCallback" Width="100%" SummaryText="Total"
                            ClientIDMode="AutoID">
                            <OptionsCustomization CustomizationWindowWidth="210" CustomizationWindowHeight="280">
                            </OptionsCustomization>
                            <ClientSideEvents BeginCallback="function(s, e) {
	hfGeral.Set('FoiMechido', '');
	if('S' == s.cp_Alterado)
		hfGeral.Set('FoiMechido', 'S');
}"></ClientSideEvents>
                            <Fields>
                                <dxwpg:PivotGridField FieldName="GrupoProduto_" ID="fieldGrupoProduto" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="2" Caption="Grupo">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="DescricaoEdital" ID="fieldDescricaoEdital" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="2" Caption="Edital" Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="SiglaUF" ID="fieldSiglaUF" AllowedAreas="RowArea, ColumnArea"
                                    Visible="False" Caption="UF" Area="RowArea" AreaIndex="2">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="NomeFederacao" ID="fieldNomeFederacao" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="2" Caption="Federação" Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="SiglaFederacao" ID="fieldSiglaFederacao" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="0" Caption="Sigla">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Produto" ID="fieldProduto" AllowedAreas="RowArea, ColumnArea"
                                    Area="RowArea" AreaIndex="1" Caption="Produto">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="QuantidadePrevista" ID="fieldQuantidadePrevista"
                                    AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Caption="Quantidade Prevista"
                                    CellFormat-FormatString="N2" CellFormat-FormatType="Custom">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="QuantidadeReal" ID="fieldQuantidadeReal" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="2" Caption="Quantidade Realizada" CellFormat-FormatString="N2"
                                    CellFormat-FormatType="Custom">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="QuantidadeRevisada" ID="fieldQuantidadeRevisada"
                                    Area="DataArea" AreaIndex="1" Caption="Quantidade Revisada" AllowedAreas="DataArea"
                                    CellFormat-FormatString="N2" CellFormat-FormatType="Custom">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="QuantidadeRestante" ID="fieldQuantidadeRestante"
                                    AllowedAreas="DataArea" Area="DataArea" AreaIndex="3" Caption="Quantidade Restante"
                                    CellFormat-FormatString="N2" CellFormat-FormatType="Custom">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ValorPrevistoCNI" ID="fieldValorPrevistoCNI" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="0" Visible="False" Caption="Valor Previsto CNI">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ValorPrevistoFederacao" ID="fieldValorPrevistoFederacao"
                                    AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Caption="Valor Previsto Federação"
                                    Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ValorRealCNI" ID="fieldValorRealCNI" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="0" Visible="False" Caption="Valor Realizado CNI">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ValorRealFederacao" ID="fieldValorRealFederacao"
                                    AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Valor Realizado Federação">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ValorRevisadoCNI" ID="fieldValorRevisadoCNI" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="0" Visible="False" Caption="Valor Revisado CNI">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ValorRevisadoFederacao" ID="fieldValorRevisadoFederacao"
                                    AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Valor Revisado Federação">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ValorPrevistoTotal" ID="fieldValorPrevistoTotal"
                                    AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Valor Previsto Total">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="ValorRealTotal" ID="fieldValorRealTotal" AllowedAreas="DataArea"
                                    Area="DataArea" AreaIndex="0" Visible="False" Caption="Valor Realizado Total">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldValorRevisadoTotal" AllowedAreas="DataArea" Area="DataArea"
                                    AreaIndex="0" Caption="Valor Revisado Total" FieldName="ValorRevisadoTotal" Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldSaldoCNI" AllowedAreas="DataArea" Area="DataArea"
                                    AreaIndex="0" Caption="Saldo CNI" FieldName="SaldoCNI" Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldSaldoFederacao" AllowedAreas="DataArea" Area="DataArea"
                                    AreaIndex="0" Caption="Saldo Federação" FieldName="SaldoFederacao" Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldSaldoTotal" AllowedAreas="DataArea" Area="DataArea"
                                    AreaIndex="0" Caption="Saldo Total" FieldName="SaldoTotal" Visible="False">
                                </dxwpg:PivotGridField>
                            </Fields>
                            <OptionsFilter ShowOnlyAvailableItems="True" />
                            <Styles>
                                <HeaderStyle ></HeaderStyle>
                                <AreaStyle >
                                </AreaStyle>
                                <FilterAreaStyle >
                                </FilterAreaStyle>
                                <FieldValueStyle >
                                </FieldValueStyle>
                                <FieldValueTotalStyle >
                                </FieldValueTotalStyle>
                                <FieldValueGrandTotalStyle >
                                </FieldValueGrandTotalStyle>
                                <CellStyle >
                                </CellStyle>
                                <GrandTotalCellStyle >
                                </GrandTotalCellStyle>
                                <FilterWindowStyle >
                                </FilterWindowStyle>
                                <FilterButtonPanelStyle >
                                </FilterButtonPanelStyle>
                                <MenuItemStyle ></MenuItemStyle>
                                <MenuStyle ></MenuStyle>
                                <CustomizationFieldsContentStyle >
                                </CustomizationFieldsContentStyle>
                            </Styles>
                            <OptionsLoadingPanel Text=" ">
                            </OptionsLoadingPanel>
                            <OptionsPager EllipsisMode="None">
                            </OptionsPager>
                        </dxwpg:ASPxPivotGrid>
                        &nbsp;&nbsp;&nbsp;</div>
                </td>
            </tr>
        </table>
    </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxpc:ASPxPopupControl runat="server" PopupVerticalOffset="5" PopupElementID="ImgHelp"
        AllowDragging="True" HeaderText="Ajuda" Width="295px" 
        ID="PopupHelp">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Este relatório não inclui os recursos repassados para apoio à contratação de Recursos Humanos e para apoio à divulgação dos projetos ao publico alvo."
                                     ID="Label1" Width="100%"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
