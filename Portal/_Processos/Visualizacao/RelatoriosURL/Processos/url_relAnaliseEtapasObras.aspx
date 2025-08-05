<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relAnaliseEtapasObras.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Processos_url_relAnaliseEtapasObras"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-right: 20px; padding-left: 10px">

                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="background-image: none; width: 99%">
                            <tr style="height: 26px">
                                <td valign="middle" style="height: 26px">&nbsp;</td>
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
}"
                                            ValueChanged="function(s, e) {
	btnSelecionar.SetEnabled(true);
}" />
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td align="left" style="width: 85px; height: 26px;" valign="middle">
                                    <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                        AutoPostBack="False" Width="85px" ClientInstanceName="btnSelecionar"
                                        Visible="False">
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
                        <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
                            style="width: 100%">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server" style="padding: 3px" valign="top">
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
                                                                <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML"
                                                                    ToolTip="Exportar para HTML">
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
                                                        <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                            <Items>
                                                                <dxm:MenuItem Name="btnSalvarLayout" Text="Salvar" ToolTip="Salvar Layout">
                                                                    <Image IconID="save_save_16x16">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnRestaurarLayout" Text="Restaurar"
                                                                    ToolTip="Restaurar Layout">
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
                        <div id="Div1" style="overflow: auto; height: <%=alturaTabela %>; width: 100%">

                            <dxwpg:ASPxPivotGrid ID="grid" runat="server"
                                ClientInstanceName="grid"
                                OnCustomCallback="grid_CustomCallback" CustomizationFieldsLeft="200" CustomizationFieldsTop="200"
                                OnAfterPerformCallback="grid_AfterPerformCallback" Width="100%" SummaryText="Média"
                                ClientIDMode="AutoID">
                                <OptionsCustomization CustomizationWindowWidth="210" CustomizationWindowHeight="280"></OptionsCustomization>
                                <ClientSideEvents BeginCallback="function(s, e) {
	hfGeral.Set('FoiMechido', '');
	if('S' == s.cp_Alterado)
		hfGeral.Set('FoiMechido', 'S');
}"></ClientSideEvents>
                                <Fields>
                                    <dxwpg:PivotGridField FieldName="NomeProcesso" ID="fieldNomeProcesso" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="2" Visible="False" Caption="Processo">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="TempoEtapaDias" ID="fieldTempoEtapaDias" AllowedAreas="DataArea"
                                        Area="DataArea" AreaIndex="0" Caption="Tempo Decorrido Etapa (Dias)" GrandTotalText="Tempo M&#233;dio"
                                        SummaryType="Average" CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric"
                                        TotalCellFormat-FormatString="{0:n1}" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                        GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                        TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="TempoEtapaHoras" ID="fieldTempoEtapaHoras" AllowedAreas="DataArea"
                                        Visible="False" Caption="Tempo Decorrido Etapa (Horas)" SummaryType="Average"
                                        CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n1}"
                                        TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                        GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                        TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="StatusProcesso" ID="fieldStatusProcesso" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="0" Caption="Status do Processo">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="InicioEtapa" ID="fieldInicioEtapa" AllowedAreas="RowArea"
                                        Area="RowArea" AreaIndex="3" Visible="False" Caption="Data de In&#237;cio da Etapa"
                                        CellFormat-FormatString="dd/MM/yyyy HH:mm" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                        TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                        GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                        ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                        TotalValueFormat-FormatType="DateTime">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="TerminoEtapa" ID="fieldTerminoEtapa" AllowedAreas="RowArea"
                                        Area="RowArea" AreaIndex="2" Visible="False" Caption="Data de T&#233;rmino da Etapa"
                                        CellFormat-FormatString="dd/MM/yyyy HH:mm" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                        TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy HH:mm"
                                        GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                        ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy HH:mm"
                                        TotalValueFormat-FormatType="DateTime">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" AllowedAreas="RowArea"
                                        Area="RowArea" AreaIndex="2" Visible="False" Caption="Projeto/Obra">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="SegmentoObra" ID="fieldSegmentoObra" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="2" Visible="False" Caption="Segmento da Obra">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="Versao" ID="fieldVersao" Area="RowArea" AreaIndex="3"
                                        Visible="False" Caption="Vers&#227;o do Fluxo">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="TipoProcesso" ID="fieldTipoProcesso" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="1" Caption="Tipo de Processo">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="TipoProjeto" ID="fieldTipoProjeto" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="4" Visible="False" Caption="Tipo de Projeto/Obra">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="AtrasoDias" ID="fieldAtrasoDias" AllowedAreas="DataArea"
                                        Area="DataArea" AreaIndex="1" Caption="Atraso Etapa (Dias)" SummaryType="Average"
                                        CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n1}"
                                        TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                        GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                        TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="AtrasoHoras" ID="fieldAtrasoHoras" AllowedAreas="DataArea"
                                        Area="DataArea" AreaIndex="2" Visible="False" Caption="Atraso Etapa (Horas)"
                                        SummaryType="Average" CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Numeric"
                                        TotalCellFormat-FormatString="{0:n1}" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="{0:n1}"
                                        GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Numeric"
                                        TotalValueFormat-FormatString="{0:n1}" TotalValueFormat-FormatType="Numeric">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="NomeEtapa" ID="fieldNomeEtapa" AllowedAreas="RowArea"
                                        Area="RowArea" AreaIndex="2" Caption="Nome da Etapa " Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="AnoInicioEtapa" ID="fieldAnoInicioEtapa" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="4" Visible="False" Caption="Ano Inicio Etapa">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="MesInicioEtapa" ID="fieldMesInicioEtapa" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="4" Visible="False" Caption="Mes Inicio Etapa">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="AnoTerminoEtapa" ID="fieldAnoTerminoEtapa" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="4" Visible="False" Caption="Ano Termino Etapa">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="MesTerminoEtapa" ID="fieldMesTerminoEtapa" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="4" Visible="False" Caption="Mes Termino Etapa">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="ResponsavelEtapa" ID="fieldResponsavelEtapa" AllowedAreas="RowArea"
                                        Area="RowArea" AreaIndex="4" Visible="False" Caption="Nome do Respons&#225;vel pela Etapa">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField FieldName="StatusEtapa" ID="fieldStatusEtapa" AllowedAreas="RowArea, ColumnArea"
                                        Area="RowArea" AreaIndex="2" Visible="False" Caption="Status da Etapa">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldnumeroContrato" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="RowArea" Caption="Numero Contrato" FieldName="numeroContrato" Visible="False"
                                        AreaIndex="2">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="fieldNomeEtapaReduzido" AllowedAreas="RowArea" Area="RowArea"
                                        Caption="Descrição Etapa Resumido" FieldName="NomeEtapaReduzido" AreaIndex="2">
                                    </dxwpg:PivotGridField>
                                </Fields>
                                <OptionsFilter ShowOnlyAvailableItems="True" />
                                <Styles>
                                    <HeaderStyle></HeaderStyle>
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
                                    <FilterWindowStyle>
                                    </FilterWindowStyle>
                                    <FilterButtonPanelStyle>
                                    </FilterButtonPanelStyle>
                                    <MenuItemStyle></MenuItemStyle>
                                    <MenuStyle></MenuStyle>
                                    <CustomizationFieldsContentStyle>
                                    </CustomizationFieldsContentStyle>
                                </Styles>
                                <OptionsLoadingPanel Text=" ">
                                </OptionsLoadingPanel>
                                <OptionsPager EllipsisMode="None">
                                </OptionsPager>
                            </dxwpg:ASPxPivotGrid>
                            &nbsp;&nbsp;&nbsp;
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxhf:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid"
            OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
        </dxpgwx:ASPxPivotGridExporter>
    </form>
</body>
</html>
