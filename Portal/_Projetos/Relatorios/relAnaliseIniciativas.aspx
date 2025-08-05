<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relAnaliseIniciativas.aspx.cs" Inherits="_Projetos_Relatorios_relAnaliseIniciativas" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
<div>
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
                                    Text="Análise de Iniciativas"></asp:Label></td>
                            <td align="right" style="width: 110px; height: 26px;">
                                <dxe:aspxlabel id="ASPxLabel4" runat="server" 
                                    text="Mapa Estratégico:">
                </dxe:aspxlabel>
                            </td>
                            <td align="right" style="width: 190px; height: 26px;">
                                <dxe:aspxcombobox id="ddlMapa" runat="server" clientinstancename="ddlMapa"
                                    valuetype="System.String" width="420px">
                </dxe:aspxcombobox>
                            </td>
                            <td align="right" style="width: 8px; height: 26px;">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:aspxlabel id="ASPxLabel3" runat="server" 
                                    text="Desempenho:">
                </dxe:aspxlabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:aspxlabel id="ASPxLabel40" runat="server" 
                                    text="Ano:">
                </dxe:aspxlabel>
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
                            <td align="left" style="width: 80px; height: 26px;">
                                <dxe:aspxbutton id="btnSelecionar" runat="server" 
                                    text="Selecionar" onclick="btnSelecionar_Click">
                    <Paddings Padding="0px" />
                </dxe:aspxbutton>
                            </td>
                            <td style="width: 5px; height: 26px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
                            <dxpgwx:ASPxPivotGridExporter id="ASPxPivotGridExporter1" runat="server" aspxpivotgridid="pvgDadosIndicador" oncustomexportcell="ASPxPivotGridExporter1_CustomExportCell"></dxpgwx:ASPxPivotGridExporter>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
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
                                                        OnCallback="pnImage_Callback" Width="23px" Height="22px" HideContentOnCallback="False"  >
                                                        <PanelCollection>
                                                            <dxp:PanelContent runat="server">
                                                                <dxe:ASPxImage ID="imgExportacao" runat="server" ClientInstanceName="imgExportacao"
                                                                    Width="20px" Height="20px" ImageUrl="~/imagens/menuExportacao/iconoExcel.png">
                                                                </dxe:ASPxImage>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </dxcp:ASPxCallbackPanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                            <dxe:aspxbutton id="btnExcel" runat="server" 
                                text="Exportar" OnClick="btnExcel_Click">
                                    <Paddings Padding="0px" />
                                </dxe:aspxbutton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; height: 5px;" valign="top">
                        </td>
                        <td valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td colspan="1" valign="top">
                            <div id="Div2" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                            <dxwpg:aspxpivotgrid id="pvgDadosIndicador" runat="server" clientinstancename="pvgDadosIndicador"
                                oncustomcelldisplaytext="pvgDadosIndicador_CustomCellDisplayText" oncustomfieldsort="pvgDadosIndicador_CustomFieldSort"
                                oncustomsummary="pvgDadosIndicador_CustomSummary" onfieldvisiblechanged="pvgDadosIndicador_FieldVisibleChanged"
                                width="100%" ><Fields>
<dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" Area="RowArea" AreaIndex="0" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Unidade" UseNativeFormat="True">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Periodo" ID="fldPeriodo" Area="RowArea" AreaIndex="1" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Per&#237;odo" SortMode="Custom" UseNativeFormat="True">
<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Ano" ID="fldAno" Area="ColumnArea" AreaIndex="0" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Ano" UseNativeFormat="True">
<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Indicador" ID="fldIndicador" Area="RowArea" AreaIndex="1" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Indicador">
<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Meta" ID="fldMeta" Area="DataArea" AreaIndex="0" Visible="False" AllowedAreas="DataArea" Caption="Meta" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Resultado" ID="fldResultado" Area="DataArea" AreaIndex="0" Visible="False" AllowedAreas="DataArea" Caption="Resultado" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
<CellStyle ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="MetaAcumulada" ID="fldMetaAcumulada" Area="DataArea" AreaIndex="0" AllowedAreas="DataArea" Caption="Meta Acumulada" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
<CellStyle ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="ResultadoAcumulado" ID="fldResultadoAcumulado" Area="DataArea" AreaIndex="1" AllowedAreas="DataArea" Caption="Resultado Acumulado" SummaryType="Custom" CellFormat-FormatType="Numeric" UseNativeFormat="True">
<CellStyle Font-Bold="False" ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DesempenhoAcumulado" ID="fldDesempenhoAcumulado" Width="20" ExportBestFit="False" Area="DataArea" AreaIndex="2" AllowedAreas="DataArea" Caption="Status Acumulado" SummaryType="Custom" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" CellFormat-FormatType="Custom" ValueFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" ValueFormat-FormatType="Custom" KPIGraphic="TrafficLights">
<CellStyle HorizontalAlign="Center" ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Desempenho" ID="fldDesempenho" Width="20" ExportBestFit="False" Area="DataArea" AreaIndex="3" Visible="False" AllowedAreas="DataArea" Caption="Status" SummaryType="Custom" CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" CellFormat-FormatType="Custom" ValueFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" ValueFormat-FormatType="Custom">
<CellStyle ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
</Fields>

<Styles>
<HeaderStyle ></HeaderStyle>

<AreaStyle ></AreaStyle>

<CellStyle ></CellStyle>

<TotalCellStyle ></TotalCellStyle>

<GrandTotalCellStyle ></GrandTotalCellStyle>

<CustomTotalCellStyle ></CustomTotalCellStyle>

<FilterWindowStyle ></FilterWindowStyle>

<MenuStyle ></MenuStyle>

<CustomizationFieldsStyle ></CustomizationFieldsStyle>

<CustomizationFieldsContentStyle ></CustomizationFieldsContentStyle>

<CustomizationFieldsHeaderStyle ></CustomizationFieldsHeaderStyle>
</Styles>

<OptionsView ShowColumnTotals="False" ShowRowTotals="False" ShowColumnGrandTotals="False" ShowRowGrandTotals="False" ></OptionsView>

<OptionsPager Visible="False"></OptionsPager>

<StylesEditors>
<DropDownWindow ></DropDownWindow>
</StylesEditors>
</dxwpg:aspxpivotgrid>
</div>
                            </td>
                    </tr>
                </table>
                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                </dxhf:ASPxHiddenField>
            </td>
        </tr>
    </table>
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
</asp:Content>


