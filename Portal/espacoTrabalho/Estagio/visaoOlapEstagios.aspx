<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoOlapEstagios.aspx.cs" Inherits="_DashBoard_VisaoCorporativa_OLAP_Estagios1" Title="Portal da Estratégia" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>  
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script> 
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center;">
    </div>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
                            <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgEstagios">
                            </dxpgwx:ASPxPivotGridExporter>
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
                                        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                        </dxhf:ASPxHiddenField>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; height: 14px" valign="top">
                        </td>
                        <td valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td colspan="1" valign="top">
                          <div id="Div2" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                                <dxwpg:ASPxPivotGrid ID="pvgEstagios" runat="server" ClientInstanceName="pvgEstagios"
                                     Width="100%" OnCustomCallback="pvgEstagios_CustomCallback" onfieldareachanged="pvgEstagios_FieldAreaChanged" onfieldareaindexchanged="pvgEstagios_FieldAreaIndexChanged" onfieldfilterchanged="pvgEstagios_FieldFilterChanged" onfieldvisiblechanged="pvgEstagios_FieldVisibleChanged" oncustomfieldsort="pvgEstagios_CustomFieldSort"><Fields>
<dxwpg:PivotGridField FieldName="Regiao" ID="fldRegiao" GroupIndex="1" InnerGroupIndex="0" Area="RowArea" AreaIndex="0" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Regi&#227;o"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="SiglaUF" ID="fldSiglaUF" GroupIndex="1" InnerGroupIndex="1" Area="RowArea" AreaIndex="1" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="UF"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="NomeIndicador" ID="fldTopico" GroupIndex="0" InnerGroupIndex="0" Area="ColumnArea" AreaIndex="0" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="T&#243;pico">
<CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ValueStyle HorizontalAlign="Center"></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DescricaoDado" ID="fldDado" GroupIndex="0" InnerGroupIndex="1" Area="ColumnArea" AreaIndex="1" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Dado"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="ValorDado" ID="fldValorDado" Area="DataArea" AreaIndex="0" AllowedAreas="DataArea" Caption="Valor" CellFormat-FormatString="#,###" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,###" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,###" GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="#,###" ValueFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,###" TotalValueFormat-FormatType="Numeric"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Ano" ID="fldAno" Area="RowArea" AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Ano"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Mes" ID="fldMes" Area="RowArea" AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="M&#234;s"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Trimestre" ID="fldTrimestre" Area="RowArea" AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Trimestre"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Semestre" ID="fldSemestre" Area="RowArea" AreaIndex="3" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Semestre"></dxwpg:PivotGridField>
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

<OptionsView ></OptionsView>

<Groups>
<dxwpg:PivotGridWebGroup Caption="Dado"></dxwpg:PivotGridWebGroup>
<dxwpg:PivotGridWebGroup Caption="regi&#227;o"></dxwpg:PivotGridWebGroup>
</Groups>

<OptionsPager Visible="False"></OptionsPager>

<StylesEditors>
<DropDownWindow ></DropDownWindow>
</StylesEditors>
</dxwpg:ASPxPivotGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
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
                  
 </form>
 </body>
 </html>

