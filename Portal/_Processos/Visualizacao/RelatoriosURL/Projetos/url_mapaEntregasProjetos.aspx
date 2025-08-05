<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_mapaEntregasProjetos.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_mapaEntregasProjetos"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 10px">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table id="tabelaFiltros" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td align="left">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td valign="middle">
                                                        <dxe:ASPxLabel runat="server" Text="Per&#237;odo:"
                                                            ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="middle">
                                                        <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                            Width="110px" DisplayFormatString="dd/MM/yyyy"
                                                            ID="txtInicio">
                                                            <CalendarProperties>
                                                                <DayHeaderStyle></DayHeaderStyle>
                                                                <DayStyle></DayStyle>
                                                                <DayOtherMonthStyle>
                                                                </DayOtherMonthStyle>
                                                                <DayWeekendStyle>
                                                                </DayWeekendStyle>
                                                                <DayOutOfRangeStyle>
                                                                </DayOutOfRangeStyle>
                                                                <ButtonStyle>
                                                                </ButtonStyle>
                                                                <HeaderStyle></HeaderStyle>
                                                                <FastNavMonthAreaStyle>
                                                                </FastNavMonthAreaStyle>
                                                                <FastNavFooterStyle>
                                                                </FastNavFooterStyle>
                                                            </CalendarProperties>
                                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                    <td style="width: 20px" valign="middle" align="center">
                                                        <dxe:ASPxLabel runat="server" Text="a" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="middle">
                                                        <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                            Width="110px" DisplayFormatString="dd/MM/yyyy"
                                                            ID="txtTermino">
                                                            <CalendarProperties>
                                                                <DayHeaderStyle></DayHeaderStyle>
                                                                <DayStyle></DayStyle>
                                                                <DayOtherMonthStyle>
                                                                </DayOtherMonthStyle>
                                                                <DayWeekendStyle>
                                                                </DayWeekendStyle>
                                                                <ButtonStyle>
                                                                </ButtonStyle>
                                                                <HeaderStyle></HeaderStyle>
                                                                <FooterStyle></FooterStyle>
                                                            </CalendarProperties>
                                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                    <td style="padding-left: 10px;" valign="middle">
                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar"
                                                            ID="btnSelecionar" Width="100px">
                                                            <ClientSideEvents Click="function(s, e) {
	pvgMapaEntregas.PerformCallback();
}" />
                                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td style="padding-left: 5px;" valign="middle">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ClientInstanceName="ImgHelp"
                                                            Cursor="pointer" ID="ImgHelp">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td colspan="1" valign="top">
                                        <table runat="server" id="tbBotoes" cellpadding="0" cellspacing="0">
                                            <tr id="Tr1" runat="server">
                                                <td id="Td1" runat="server" style="padding: 3px" valign="top">
                                                    <table cellpadding="0" cellspacing="0" style="height: 22px">
                                                        <tr>
                                                            <td>
                                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                                                <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout" Name="btnRestaurarLayout">
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
                                        <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgMapaEntregas" Width="100%"
                                            ID="pvgMapaEntregas" OnCustomFieldSort="pvgMapaEntregas_CustomFieldSort" OnCustomCellStyle="pvgMapaEntregas_CustomCellStyle"
                                            ClientIDMode="AutoID" OnCustomCallback="pvgMapaEntregas_CustomCallback"
                                            OnFieldValueDisplayText="pvgMapaEntregas_FieldValueDisplayText">
                                            <Fields>
                                                <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Area="RowArea" AreaIndex="0" Caption="Nome do Projeto" SortMode="None">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <ValueStyle>
                                                    </ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="NomePrograma" ID="fieldNomePrograma" AllowedAreas="RowArea, FilterArea"
                                                    Area="RowArea" AreaIndex="0" Caption="Programa" SortMode="None">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle Font-Names="Verdana" Font-Size="8pt"></HeaderStyle>
                                                    <ValueStyle Font-Names="Verdana" Font-Size="8pt">
                                                    </ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="Tarefa" ID="fieldTarefa" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Options-AllowSort="True" Area="RowArea" AreaIndex="1" Caption="Entrega" SortBySummaryInfo-FieldName="DataPrevistaEntrega"
                                                    SortMode="Custom">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <ValueStyle>
                                                    </ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="GerenteProjeto" ID="fieldGerenteProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Visible="False" Caption="Gerente do Projeto" SortMode="None" AreaIndex="0">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <ValueStyle>
                                                    </ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="SequenciaHistoricoEntrega" ID="fieldSequenciaHistoricoEntrega"
                                                    AllowedAreas="DataArea" Options-ShowGrandTotal="False" Options-ShowTotals="False"
                                                    Area="DataArea" AreaIndex="0" Caption="Hist&#243;rico" TotalsVisibility="None"
                                                    SortMode="None" CellFormat-FormatString="{0:n0}" CellFormat-FormatType="Custom"
                                                    ValueFormat-FormatString="n0" ValueFormat-FormatType="Numeric" KPIGraphic="None">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <ValueStyle>
                                                    </ValueStyle>
                                                    <ValueTotalStyle>
                                                    </ValueTotalStyle>
                                                    <CustomTotals>
                                                        <dxwpg:PivotGridCustomTotal SummaryType="Count"></dxwpg:PivotGridCustomTotal>
                                                    </CustomTotals>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="ValorFaturamento" ID="fieldValorFaturamento" AllowedAreas="DataArea"
                                                    Area="DataArea" AreaIndex="1" Visible="False" Caption="Valor do Faturamento"
                                                    SortMode="None" CellFormat-FormatString="n2" CellFormat-FormatType="Numeric"
                                                    ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Custom">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <ValueStyle>
                                                    </ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="Ano" ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Ano" SortMode="None">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <ValueStyle>
                                                    </ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="DataFormatacao" ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Area="ColumnArea" AreaIndex="0" Visible="False" Caption="M&#234;s" SortMode="None"
                                                    CellFormat-FormatString="{0:MMM/yyyy}" CellFormat-FormatType="DateTime" ValueFormat-FormatString="{0:MMM/yyyy}"
                                                    ValueFormat-FormatType="DateTime">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <ValueStyle>
                                                    </ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField FieldName="DataRealEntrega" ID="fieldDataRealEntrega" AllowedAreas="ColumnArea, FilterArea"
                                                    Caption="Data Real da Entrega" SortMode="None" CellFormat-FormatString="{0:dd/MM/yyyy}"
                                                    ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" AreaIndex="0">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <ValueStyle>
                                                    </ValueStyle>
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="fieldDataPrevistaEntrega" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Area="ColumnArea" AreaIndex="0" Caption="Data da Entrega" CellFormat-FormatString="{0:dd/MM/yyyy}"
                                                    FieldName="DataPrevistaEntrega" ValueFormat-FormatString="dd/MM/yyyy"
                                                    ValueFormat-FormatType="DateTime">
                                                </dxwpg:PivotGridField>
                                            </Fields>
                                            <OptionsCustomization AllowSort="False"></OptionsCustomization>
                                            <OptionsPager RowsPerPage="15" Position="Bottom" Visible="False">
                                                <Summary AllPagesText="P&#225;ginas: {0} - {1} ({2} Registros)" Text="P&#225;ginas: {0} - {1} ({2} Registros)"></Summary>
                                            </OptionsPager>
                                            <OptionsFilter ShowOnlyAvailableItems="True" />
                                            <Styles>
                                                <HeaderStyle></HeaderStyle>
                                                <AreaStyle>
                                                </AreaStyle>
                                                <CellStyle>
                                                </CellStyle>
                                                <TotalCellStyle>
                                                </TotalCellStyle>
                                                <CustomTotalCellStyle>
                                                </CustomTotalCellStyle>
                                                <MenuItemStyle></MenuItemStyle>
                                                <MenuStyle></MenuStyle>
                                            </Styles>
                                        </dxwpg:ASPxPivotGrid>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" HeaderText="Legenda" PopupElementID="ImgHelp"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupVerticalOffset="15"
                Width="816px" ID="PopupHelp">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <table cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="border-right: #fac19c 4px solid; border-top: #fac19c 4px solid; border-left: #fac19c 4px solid; width: 30px; border-bottom: #fac19c 4px solid; height: 25px; background-color: #fac19c"
                                        align="center">
                                        <span style="">1</span>
                                    </td>
                                    <td style="height: 25px">&nbsp;-
                                    <asp:Label runat="server" Text="Significa que foi previsto mas n&#227;o foi entregue nessa data. O n&#250;mero 1 significa que foi a primeira data pactuada com o cliente."
                                        ID="Label1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30px; height: 10px" align="center"></td>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="border-right: #fac19c 4px solid; border-top: #fac19c 4px solid; border-left: #fac19c 4px solid; width: 30px; border-bottom: #fac19c 4px solid; height: 25px; background-color: #fac19c"
                                        align="center">
                                        <span style="">2</span>
                                    </td>
                                    <td style="height: 25px">&nbsp;-
                                    <asp:Label runat="server" Text="Significa que foi pactuado pela segunda vez e tamb&#233;m n&#227;o foi entregue na data."
                                        ID="Label2"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30px; height: 10px" align="center"></td>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="border-right: #b0c6ff 4px solid; border-top: #b0c6ff 4px solid; border-left: #b0c6ff 4px solid; width: 30px; border-bottom: #b0c6ff 4px solid; height: 25px; background-color: #b0c6ff"
                                        align="center">
                                        <span style="">3</span>
                                    </td>
                                    <td style="height: 25px">&nbsp;-
                                    <asp:Label runat="server" Text="Significa que foi pactuado pela terceira vez e foi cumprido na data"
                                        ID="Label3"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30px; height: 10px" align="center"></td>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="border-right: #acffb0 4px solid; border-top: #acffb0 4px solid; border-left: #acffb0 4px solid; width: 30px; border-bottom: #acffb0 4px solid; height: 25px; background-color: #acffb0"
                                        align="center">
                                        <span style="">2</span>
                                    </td>
                                    <td style="height: 25px">&nbsp;-
                                    <asp:Label runat="server" Text="Significa que foi pactuado pela segunda vez e ser&#225; entregue na data (informa&#231;&#227;o do status report)"
                                        ID="Label4"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30px; height: 10px" align="center"></td>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="border-right: #c6c6c6 4px solid; border-top: #c6c6c6 4px solid; border-left: #c6c6c6 4px solid; width: 30px; border-bottom: #c6c6c6 4px solid; height: 25px; background-color: #acffb0"
                                        align="center">
                                        <span style="">1</span>
                                    </td>
                                    <td style="height: 25px">&nbsp;-
                                    <asp:Label runat="server" Text="Moldura significa que &#233; um marco de faturamento - Verde significa que provavelmente vai se confirmar"
                                        ID="Label5"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgMapaEntregas" ID="ASPxPivotGridExporter1"
                OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
            </dxpgwx:ASPxPivotGridExporter>
            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
            </dxhf:ASPxHiddenField>
        </div>
    </form>
</body>
</html>
