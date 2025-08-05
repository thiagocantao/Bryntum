<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="mapaEntregasProjetos.aspx.cs"
    Inherits="_Projetos_Relatorios_mapaEntregasProjetos" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%; height: 28px">
            <tr>
                <td style="padding-left: 10px">
                    <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                        Font-Overline="False" Font-Strikeout="False"
                        Text="Mapa de Entregas"></asp:Label></td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <dxcp:aspxcallbackpanel id="pnCallbackDados" runat="server" clientinstancename="pnCallbackDados"
                        oncallback="pnCallbackDados_Callback" width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="PADDING-RIGHT: 10px; PADDING-LEFT: 10px; PADDING-BOTTOM: 5px; PADDING-TOP: 10px"><table id="tabelaFiltros" cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><TR ><td style="WIDTH: 424px" align=left ><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0" ><TBODY><TR ><td style="WIDTH: 57px" valign="middle" ><dxe:ASPxLabel runat="server" Text="Per&#237;odo:"  ID="ASPxLabel1"  ></dxe:ASPxLabel>
 </td><td style="WIDTH: 120px" valign="middle" ><dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="110px" DisplayFormatString="dd/MM/yyyy"  ID="txtInicio"  >
<CalendarProperties>
<DayHeaderStyle ></DayHeaderStyle>

<DayStyle ></DayStyle>

<DayOtherMonthStyle ></DayOtherMonthStyle>

<DayWeekendStyle ></DayWeekendStyle>

<DayOutOfRangeStyle ></DayOutOfRangeStyle>

<ButtonStyle ></ButtonStyle>

<HeaderStyle ></HeaderStyle>

<FastNavMonthAreaStyle ></FastNavMonthAreaStyle>

<FastNavFooterStyle ></FastNavFooterStyle>
</CalendarProperties>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxDateEdit>
 </td><td style="WIDTH: 20px" valign="middle" ><dxe:ASPxLabel runat="server" Text="a"  ID="ASPxLabel2"  ></dxe:ASPxLabel>
 </td><td style="WIDTH: 120px" valign="middle" ><dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="110px" DisplayFormatString="dd/MM/yyyy"  ID="txtTermino"  >
<CalendarProperties>
<DayHeaderStyle ></DayHeaderStyle>

<DayStyle ></DayStyle>

<DayOtherMonthStyle ></DayOtherMonthStyle>

<DayWeekendStyle ></DayWeekendStyle>

<ButtonStyle ></ButtonStyle>

<HeaderStyle ></HeaderStyle>

<FooterStyle ></FooterStyle>
</CalendarProperties>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxDateEdit>
 </td><td style="WIDTH: 90px" valign="middle" ><dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar"  ID="btnSelecionar"  >
<ClientSideEvents Click="function(s, e) {
	pnCallbackDados.PerformCallback();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>
 </td><td style="WIDTH: 40px" valign="middle" ><dxe:ASPxImage runat="server" 
            ImageUrl="~/imagens/ajuda.png" ClientInstanceName="ImgHelp" Cursor="pointer" 
            ID="ImgHelp"  ></dxe:ASPxImage>
 </td></tr></tbody></table></td><td align=right ><table cellspacing="0" cellpadding="0" border="0" ><TBODY><TR ><td style="WIDTH: 205px" ><table cellspacing="0" cellpadding="0" border="0" ><TBODY><TR ><td ><dxe:ASPxComboBox runat="server" ValueType="System.String" ClientInstanceName="ddlExporta"  ID="ddlExporta"  >
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}"></ClientSideEvents>
</dxe:ASPxComboBox>
 </td><td style="PADDING-LEFT: 2px" ><dxcp:ASPxCallbackPanel runat="server"   ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage"   OnCallback="pnImage_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao" ></dxe:ASPxImage>

 </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>
 </td></tr></tbody></table></td><td ><dxe:ASPxButton runat="server" Text="Exportar"  ID="Aspxbutton1"   OnClick="btnExcel_Click">
<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>
 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"  ></dxhf:ASPxHiddenField>
 </td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td style="PADDING-RIGHT: 10px; PADDING-LEFT: 10px"><dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgMapaEntregas"  ID="ASPxPivotGridExporter1"  OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell"></dxpgwx:ASPxPivotGridExporter>
 <div id="Div1" style="overflow: auto; width: ">
 <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgMapaEntregas" Width="100%" 
                       ID="pvgMapaEntregas" 
                      OnCustomFieldSort="pvgMapaEntregas_CustomFieldSort" 
                      OnCustomCellStyle="pvgMapaEntregas_CustomCellStyle">
        <Fields>
        <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" Caption="Nome do Projeto" SortMode="None">
        <CellStyle Wrap="False"></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>
        </dxwpg:PivotGridField>
        <dxwpg:PivotGridField FieldName="Tarefa" ID="fieldTarefa" AllowedAreas="RowArea, ColumnArea, FilterArea" Options-AllowSort="True" Area="RowArea" AreaIndex="1" Caption="Entrega" SortBySummaryInfo-FieldName="DataPrevistaEntrega" SortMode="Custom">
        <CellStyle Wrap="False"></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>
        </dxwpg:PivotGridField>
        <dxwpg:PivotGridField FieldName="GerenteProjeto" ID="fieldGerenteProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea" Visible="False" Caption="Gerente do Projeto" SortMode="None">
        <CellStyle Wrap="False"></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>
        </dxwpg:PivotGridField>
        <dxwpg:PivotGridField FieldName="DataPrevistaEntrega" ID="fieldDataPrevistaEntrega" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0" Caption="Data da Entrega" SortMode="None" CellFormat-FormatString="{0:dd/MM/yyyy}" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime">
        <CellStyle Wrap="False"></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>
        </dxwpg:PivotGridField>
        <dxwpg:PivotGridField FieldName="SequenciaHistoricoEntrega" ID="fieldSequenciaHistoricoEntrega" AllowedAreas="DataArea" Options-ShowGrandTotal="False" Options-ShowTotals="False" Area="DataArea" AreaIndex="0" Caption="Hist&#243;rico" TotalsVisibility="None" SortMode="None" CellFormat-FormatString="{0:n0}" CellFormat-FormatType="Custom" ValueFormat-FormatString="n0" ValueFormat-FormatType="Numeric" KPIGraphic="None">
        <CellStyle Wrap="False" ></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>

        <ValueTotalStyle ></ValueTotalStyle>
        <CustomTotals>
        <dxwpg:PivotGridCustomTotal SummaryType="Count"></dxwpg:PivotGridCustomTotal>
        </CustomTotals>
        </dxwpg:PivotGridField>
        <dxwpg:PivotGridField FieldName="ValorFaturamento" ID="fieldValorFaturamento" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1" Visible="False" Caption="Valor do Faturamento" SortMode="None" CellFormat-FormatString="n2" CellFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Custom">
        <CellStyle Wrap="False"></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>
        </dxwpg:PivotGridField>
        <dxwpg:PivotGridField FieldName="Ano" ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Ano" SortMode="None">
        <CellStyle Wrap="False"></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>
        </dxwpg:PivotGridField>
        <dxwpg:PivotGridField FieldName="DataFormatacao" ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0" Visible="False" Caption="M&#234;s" SortMode="None" CellFormat-FormatString="{0:MMM/yyyy}" CellFormat-FormatType="DateTime" ValueFormat-FormatString="{0:MMM/yyyy}" ValueFormat-FormatType="DateTime">
        <CellStyle Wrap="False"></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>
        </dxwpg:PivotGridField>
        <dxwpg:PivotGridField FieldName="DataRealEntrega" ID="fieldDataRealEntrega" AllowedAreas="ColumnArea, FilterArea" Visible="False" Caption="Data Real da Entrega" SortMode="None" CellFormat-FormatString="{0:dd/MM/yyyy}" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime">
        <CellStyle Wrap="False"></CellStyle>

        <HeaderStyle ></HeaderStyle>

        <ValueStyle ></ValueStyle>
        </dxwpg:PivotGridField>
        </Fields>

        <OptionsCustomization AllowSort="False"></OptionsCustomization>

        <OptionsPager RowsPerPage="15" Position="Bottom" Visible="False">
        <Summary AllPagesText="P&#225;ginas: {0} - {1} ({2} Registros)" Text="P&#225;ginas: {0} - {1} ({2} Registros)"></Summary>
        </OptionsPager>

        <Styles>
        <HeaderStyle ></HeaderStyle>

        <AreaStyle ></AreaStyle>

        <CellStyle ></CellStyle>

        <TotalCellStyle ></TotalCellStyle>

        <CustomTotalCellStyle ></CustomTotalCellStyle>

        <MenuItemStyle ></MenuItemStyle>

        <MenuStyle ></MenuStyle>
        </Styles>
</dxwpg:ASPxPivotGrid>
</div>

<dxpc:ASPxPopupControl runat="server" AllowDragging="True" HeaderText="Legenda" PopupElementID="ImgHelp" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupVerticalOffset="15" Width="816px"  ID="PopupHelp" ><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><TBODY><tr><td style="BORDER-RIGHT: #fac19c 4px solid; BORDER-TOP: #fac19c 4px solid; BORDER-LEFT: #fac19c 4px solid; WIDTH: 30px; BORDER-BOTTOM: #fac19c 4px solid; HEIGHT: 25px; BACKGROUND-COLOR: #fac19c" align="center"><SPAN>1</SPAN></td><td style="HEIGHT: 25px">&nbsp;- <asp:Label runat="server" Text="Significa que foi previsto mas n&#227;o foi entregue nessa data. O n&#250;mero 1 significa que foi a primeira data pactuada com o cliente."  ID="Label1"></asp:Label>

 </td></tr><tr><td style="WIDTH: 30px; HEIGHT: 10px" align="center"></td>
 <td style="HEIGHT: 10px"></td></tr><tr>
 <td style="BORDER-RIGHT: #fac19c 4px solid; BORDER-TOP: #fac19c 4px solid; BORDER-LEFT: #fac19c 4px solid; WIDTH: 30px; BORDER-BOTTOM: #fac19c 4px solid; HEIGHT: 25px; BACKGROUND-COLOR: #fac19c" align="center"><SPAN>2</SPAN></td><td style="HEIGHT: 25px">&nbsp;- <asp:Label runat="server" Text="Significa que foi pactuado pela segunda vez e tamb&#233;m n&#227;o foi entregue na data."  ID="Label2"></asp:Label>

 </td></tr><tr><td style="WIDTH: 30px; HEIGHT: 10px" align="center"></td><td style="HEIGHT: 10px"></td></tr><tr><td style="BORDER-RIGHT: #b0c6ff 4px solid; BORDER-TOP: #b0c6ff 4px solid; BORDER-LEFT: #b0c6ff 4px solid; WIDTH: 30px; BORDER-BOTTOM: #b0c6ff 4px solid; HEIGHT: 25px; BACKGROUND-COLOR: #b0c6ff" align="center"><SPAN>3</SPAN></td><td style="HEIGHT: 25px">&nbsp;- <asp:Label runat="server" Text="Significa que foi pactuado pela terceira vez e foi cumprido na data"  ID="Label3"></asp:Label>

 </td></tr><tr><td style="WIDTH: 30px; HEIGHT: 10px" align="center"></td><td style="HEIGHT: 10px"></td></tr><tr><td style="BORDER-RIGHT: #acffb0 4px solid; BORDER-TOP: #acffb0 4px solid; BORDER-LEFT: #acffb0 4px solid; WIDTH: 30px; BORDER-BOTTOM: #acffb0 4px solid; HEIGHT: 25px; BACKGROUND-COLOR: #acffb0" align="center"><SPAN>2</SPAN></td><td style="HEIGHT: 25px">&nbsp;- <asp:Label runat="server" Text="Significa que foi pactuado pela segunda vez e ser&#225; entregue na data (informa&#231;&#227;o do status report)"  ID="Label4"></asp:Label>

 </td></tr><tr><td style="WIDTH: 30px; HEIGHT: 10px" align="center"></td><td style="HEIGHT: 10px"></td></tr><tr><td style="BORDER-RIGHT: #c6c6c6 4px solid; BORDER-TOP: #c6c6c6 4px solid; BORDER-LEFT: #c6c6c6 4px solid; WIDTH: 30px; BORDER-BOTTOM: #c6c6c6 4px solid; HEIGHT: 25px; BACKGROUND-COLOR: #acffb0" align="center"><SPAN>1</SPAN></td><td style="HEIGHT: 25px">&nbsp;- <asp:Label runat="server" Text="Moldura significa que &#233; um marco de faturamento - Verde significa que provavelmente vai se confirmar"  ID="Label5"></asp:Label>

 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
</td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxcp:aspxcallbackpanel>
                </td>
            </tr>
        </table>
</asp:Content>
