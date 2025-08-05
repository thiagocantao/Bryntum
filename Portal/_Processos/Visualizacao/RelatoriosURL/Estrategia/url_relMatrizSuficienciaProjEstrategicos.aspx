<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relMatrizSuficienciaProjEstrategicos.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Estrategia_url_relMatrizSuficienciaProjEstrategicos"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
</head>
<body style="margin: 10px">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>

                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="padding-bottom: 5px;" align="right">
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                    </dxhf:ASPxHiddenField>
                                    <table cellspacing="0" cellpadding="0">
                                        <tbody align="left">
                                            <tr>
                                                <td style="width: 425px">
                                                    <dxe:ASPxLabel runat="server" Text="Mapa:" ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 90px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 425px">
                                                    <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="417px" Height="22px"
                                                        ClientInstanceName="ddlMapa" ID="ddlMapa">
                                                        <ItemStyle Wrap="True">
                                                            <Paddings Padding="0px" />
                                                        </ItemStyle>
                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td style="width: 90px">
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar" Width="100%"
                                                        Height="22px" ID="btnSelecionar">
                                                        <ClientSideEvents Click="function(s, e) {
	pvgMapaEntregas.PerformCallback();
}"></ClientSideEvents>
                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgMapaEntregas" ID="ASPxPivotGridExporter1"
                                        OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                                    </dxpgwx:ASPxPivotGridExporter>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
            style="width: 100%">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server" style="padding: 3px" valign="top">
                    <table cellpadding="0" cellspacing="0" style="height: 22px">
                        <tr>
                            <td style="padding-right: 10px;">
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
        <div id="Div1" runat="server" style="width: 100%">
            <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgMapaEntregas"
                Width="100%" ID="pvgMapaEntregas"
                OnCustomCellStyle="pvgMapaEntregas_CustomCellStyle" ClientIDMode="AutoID">
                <Fields>
                    <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" AllowedAreas="RowArea, ColumnArea"
                        Area="ColumnArea" AreaIndex="0" Caption="Nome do Projeto" EmptyCellText="Nenhum"
                        EmptyValueText="NA">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="LiderProjeto" ID="fieldLiderProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="ColumnArea" AreaIndex="0" Visible="False" Caption="L&#237;der do Projeto"
                        EmptyCellText="Nenhum" EmptyValueText="NA">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="PercentualConcluido" ID="fieldPercentualConcluido"
                        AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Percentual Conclu&#237;do"
                        EmptyValueText="NA" TotalsVisibility="None" CellFormat-FormatString="#,##0.0%"
                        CellFormat-FormatType="Numeric">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="ObjetivoEstrategico" ID="fieldObjetivoEstrategico"
                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Caption="Objetivo Estrategico"
                        TotalsVisibility="None">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="PossuiObjetivoAssociado" ID="fieldPossuiObjetivoAssociado"
                        AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Caption="Possui Objetivo Associado"
                        TotalCellFormat-FormatType="Numeric">
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="Perspectiva" ID="fieldPerspectiva" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" AreaIndex="0" Caption="Perspectiva" TotalsVisibility="None">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="StatusProjeto" ID="fieldStatusProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Status do Projeto" EmptyCellText="Nenhum"
                        EmptyValueText="NA">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="FaseProjeto" ID="fieldFaseProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Fase do Projeto" EmptyCellText="Nenhuma"
                        EmptyValueText="NA">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="UnidadeNegocio" ID="fieldUnidadeNegocio" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="ColumnArea" AreaIndex="1" Visible="False" Caption="Unidade de Neg&#243;cio"
                        EmptyCellText="Nenhuma" EmptyValueText="NA" TotalsVisibility="None">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="Tema" ID="fieldTema" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" AreaIndex="1" Visible="False" Caption="Tema">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="ResponsavelTema" ID="fieldResponsavelTema" AllowedAreas="RowArea, ColumnArea, FilterArea"
                        Area="RowArea" AreaIndex="1" Visible="False" Caption="Respons&#225;vel Tema">
                    </dxwpg:PivotGridField>
                    <dxwpg:PivotGridField FieldName="ResponsavelObjetivo" ID="fieldResponsavelObjetivo"
                        AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Visible="False"
                        Caption="Respons&#225;vel Objetivo">
                    </dxwpg:PivotGridField>
                    <dxpgwx:PivotGridField ID="fieldTipoProjeto" AreaIndex="0"
                        Caption="Tipo Projeto" FieldName="TipoProjeto">
                    </dxpgwx:PivotGridField>
                </Fields>
                <OptionsView ShowColumnTotals="False" ShowRowTotals="False" ShowColumnGrandTotals="False"
                    ShowRowGrandTotals="False"></OptionsView>
                <OptionsPager RowsPerPage="15" Position="Bottom" Visible="False">
                    <Summary AllPagesText="P&#225;ginas: {0} - {1} ({2} Registros)" Text="P&#225;ginas: {0} - {1} ({2} Registros)"></Summary>
                </OptionsPager>
                <OptionsFilter ShowOnlyAvailableItems="True" />
            </dxwpg:ASPxPivotGrid>
        </div>
    </form>
</body>
</html>
