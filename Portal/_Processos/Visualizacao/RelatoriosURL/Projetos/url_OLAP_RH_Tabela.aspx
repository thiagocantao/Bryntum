<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_OLAP_RH_Tabela.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_OLAP_RH_Tabela"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid"
            OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell" OnCustomExportFieldValue="ASPxPivotGridExporter1_CustomExportFieldValue"
            OnCustomExportHeader="ASPxPivotGridExporter1_CustomExportHeader">
        </dxpgwx:ASPxPivotGridExporter>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-right: 10px; padding-left: 10px;">
                <tr>
                    <td align="right">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:450px;padding-top:5px;padding-bottom:5px">
                            <tr style="height: 26px">
                                <td valign="middle" style="padding-right: 5px;">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                        Text="Período:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 120px; padding-right: 5px; height: 26px;" align="right">
                                    <dxe:ASPxDateEdit ID="dteInicio" runat="server" Font-Overline="False"
                                        Width="100%" AllowNull="False" ClientInstanceName="dteInicio">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td style="width: 10px; padding-right: 5px;">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                        Text="a" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 120px; padding-right: 5px;" align="right">
                                    <dxe:ASPxDateEdit ID="dteFim" runat="server"
                                        Font-Strikeout="False" Width="100%" AllowNull="False" ClientInstanceName="dteFim">
                                        <ClientSideEvents ButtonClick="function(s, e) {
btnSelecionar.SetEnabled(true);
}"
                                            ValueChanged="function(s, e) {
	btnSelecionar.SetEnabled(true);
}" />
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td align="left" style="width: 100px; height: 26px;">
                                    <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                        AutoPostBack="False" Width="100%"
                                        ClientInstanceName="btnSelecionar">
                                        <ClientSideEvents Click="function(s, e) {
	grid.PerformCallback(&quot;PopularGrid&quot;);
}"></ClientSideEvents>
                                        <Paddings Padding="0px" />
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
                                                                <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<% $Resources:traducao, url_OLAP_RH_Tabela_exportar_para_XLS %>">
                                                                    <Image Url="~/imagens/menuExportacao/xls.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<% $Resources:traducao, url_OLAP_RH_Tabela_exportar_para_PDF %>">
                                                                    <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<% $Resources:traducao, url_OLAP_RH_Tabela_exportar_para_RTF %>">
                                                                    <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML"
                                                                    ToolTip="<% $Resources:traducao, url_OLAP_RH_Tabela_exportar_para_HTML %>">
                                                                    <Image Url="~/imagens/menuExportacao/html.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Text="CSV" ToolTip="<% $Resources:traducao, url_OLAP_RH_Tabela_exportar_para_CSV %>">
                                                                    <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                            </Items>
                                                            <Image Url="~/imagens/botoes/btnDownload.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                            <Items>
                                                                <dxm:MenuItem Name="btnSalvarLayout" Text="<% $Resources:traducao, url_OLAP_RH_Tabela_salvar %>" ToolTip="<% $Resources:traducao, url_OLAP_RH_Tabela_salvar_layout %>">
                                                                    <Image IconID="save_save_16x16">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnRestaurarLayout" Text="<% $Resources:traducao, url_OLAP_RH_Tabela_restaurar %>"
                                                                    ToolTip="<% $Resources:traducao, url_OLAP_RH_Tabela_restaurar_layout %>">
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
                        <dxwpg:ASPxPivotGrid ID="grid" runat="server" ClientInstanceName="grid" OnCustomCellStyle="grid_CustomCellStyle" OnCustomFieldSort="grid_CustomFieldSort"
                            OnCustomCallback="grid_CustomCallback" CustomizationFieldsLeft="200" CustomizationFieldsTop="200"
                            OnCustomCellDisplayText="grid_CustomCellDisplayText" ClientIDMode="AutoID" OnInit="grid_Init">
                            <Fields>
                                <dxwpg:PivotGridField FieldName="SiglaUnidadeNegocio" ID="fieldSiglaUnidadeNegocio"
                                    Area="RowArea" AreaIndex="0" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                    Caption="Unidade" Options-AllowDrag="False" Options-AllowDragInCustomizationForm="False"
                                    Options-AllowExpand="False" Options-AllowFilter="False" Options-AllowSort="False"
                                    Options-AllowSortBySummary="False" Options-ShowInCustomizationForm="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" Area="RowArea"
                                    AreaIndex="1" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Projeto">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="DescricaoStatus" ID="fieldDescricaoStatus" Area="RowArea"
                                    AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                    Caption="Status">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="NomeRecurso" ID="fieldNomeRecurso" Area="RowArea"
                                    AreaIndex="0" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Recurso"
                                    ExpandedInFieldsGroup="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Trabalho" ID="fieldTrabalho" Area="DataArea" AreaIndex="0"
                                    AllowedAreas="DataArea" Caption="Trabalho (h)" CellFormat-FormatString="#,###.#"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Capacidade" ID="fieldCapacidade" Area="DataArea"
                                    AreaIndex="1" AllowedAreas="DataArea" Caption="Capacidade (h)" CellFormat-FormatString="{0:n2}"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n2}" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="{0:n2}" GrandTotalCellFormat-FormatType="Numeric"
                                    ExpandedInFieldsGroup="False" TotalValueFormat-FormatString="{0:n2}" TotalValueFormat-FormatType="Numeric"
                                    ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Ano" ID="fieldAno" Area="RowArea" AreaIndex="1"
                                    Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" UnboundFieldName="field"
                                    Caption="Ano">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Mes" ID="fieldMes" Area="RowArea" AreaIndex="2"
                                    Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" UnboundFieldName="field1"
                                    Caption="M&#234;s" SortMode="Custom">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="DataReferencia" ID="field2" Area="RowArea" AreaIndex="1"
                                    Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" GroupInterval="Date"
                                    UnboundFieldName="field2" Caption="Data">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Disponibilidade" ID="fieldDisponibilidade" Area="DataArea"
                                    AreaIndex="2" AllowedAreas="DataArea" Caption="Disponibilidade (h)" CellFormat-FormatString="#,###.#"
                                    CellFormat-FormatType="Custom" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Custom"
                                    GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Custom">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TrabalhoLB" ID="field4" Area="DataArea" AreaIndex="3"
                                    Visible="False" AllowedAreas="DataArea" Caption="Trabalho Previsto (h)" CellFormat-FormatString="#,###.#"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="TrabalhoReal" ID="field5" Area="DataArea" AreaIndex="3"
                                    Visible="False" AllowedAreas="DataArea" Caption="Trabalho Realizado (h)" CellFormat-FormatString="#,###.#"
                                    CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###.#" TotalCellFormat-FormatType="Numeric"
                                    GrandTotalCellFormat-FormatString="#,###.#" GrandTotalCellFormat-FormatType="Numeric">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField FieldName="Semana" ID="fieldSemana" Area="ColumnArea" AreaIndex="0"
                                    AllowedAreas="RowArea, ColumnArea, FilterArea" UnboundFieldName="field3" Caption="Semana"
                                    SortMode="Custom">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldAnotacoes" AllowedAreas="RowArea" Area="RowArea" AreaIndex="2"
                                    Caption="Conhecimentos, Habilidades e Competências" FieldName="Anotacoes" Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldNomeUnidadeNegocio" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                    Area="RowArea" AreaIndex="2" Caption="Nome Unidade Negocio" FieldName="NomeUnidadeNegocio"
                                    Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldNomeEntidade" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                    Area="RowArea" AreaIndex="2" Caption="Nome Entidade" FieldName="NomeEntidade"
                                    Visible="False">
                                </dxwpg:PivotGridField>
                                <dxwpg:PivotGridField ID="fieldCustoHora" AllowedAreas="RowArea" Area="RowArea" Caption="Despesa Unitária (R$)"
                                    CellFormat-FormatString="{0:n2}" CellFormat-FormatType="Numeric" FieldName="CustoHora"
                                    GrandTotalCellFormat-FormatString="{0:n2}" GrandTotalCellFormat-FormatType="Numeric"
                                    TotalCellFormat-FormatString="{0:n2}" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}"
                                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric"
                                    Visible="False">
                                </dxwpg:PivotGridField>
                            </Fields>
                            <OptionsFilter ShowOnlyAvailableItems="True" />
                            <OptionsCustomization CustomizationWindowHeight="350" CustomizationWindowWidth="200"></OptionsCustomization>
                            <OptionsLoadingPanel Text=" ">
                            </OptionsLoadingPanel>
                            <OptionsPager EllipsisMode="None">
                            </OptionsPager>
                            <OptionsView HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                        </dxwpg:ASPxPivotGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
