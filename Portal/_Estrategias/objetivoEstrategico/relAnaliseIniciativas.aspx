<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relAnaliseIniciativas.aspx.cs" Inherits="_Estrategias_objetivoEstrategico_relAnaliseIniciativas" Title="Portal da Estratégia" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script type="text/javascript" language="javascript" src="../../scripts/AnexoObjetivoEstrategico.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>

    <title>Anexos do Objetivo Estratégico</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
<div>
    </div>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server"  Text="Mapa Estratégico:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td style="width: 209px">
                                                    <asp:Label ID="lblPerspectiva" runat="server" 
                                                        Text="Perspectiva:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td style="width: 220px">
                                                    <asp:Label ID="Label6" runat="server"  Text="Tema:"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa" 
                                                        ReadOnly="True" Width="100%">
                                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                                        </ReadOnlyStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 220px">
                                                    <dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                                                         ReadOnly="True" Width="100%">
                                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                                        </ReadOnlyStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 220px">
                                                    <dxe:ASPxTextBox ID="txtTema" runat="server" ClientInstanceName="txtTema" 
                                                        ReadOnly="True" Width="100%">
                                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                                        </ReadOnlyStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>
                                        <table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><tbody><tr><td><asp:Label id="lblObjetivoEstrategico" runat="server" Text="Objetivo Estratégico:"></asp:Label></td><td 
style="WIDTH: 10px"></td><td 
style="WIDTH: 280px"><asp:Label id="Label3" runat="server" Text="Responsável:" ></asp:Label></td></tr><tr><td><dxe:ASPxTextBox id="txtObjetivoEstrategico" runat="server"  Width="100%" ReadOnly="True" ClientInstanceName="txtObjetivoEstrategico">
                    <ReadOnlyStyle BackColor="#E0E0E0">
                    </ReadOnlyStyle>
                </dxe:ASPxTextBox> </td><td style="WIDTH: 10px"></td><td style="WIDTH: 280px"><dxe:ASPxTextBox id="txtResponsavel" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtResponsavel">
                                <ReadOnlyStyle BackColor="#E0E0E0">
                                </ReadOnlyStyle>
                            </dxe:ASPxTextBox> </td></tr></tbody></table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; height: 10px" valign="top">
                        </td>
                        <td valign="top">
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
                                                        OnCallback="pnImage_Callback" Width="23px" Height="22px">
                                                        <SettingsLoadingPanel Enabled="False" ShowImage="False" />
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
                            <dxpgwx:ASPxPivotGridExporter id="ASPxPivotGridExporter1" runat="server" aspxpivotgridid="pvgDadosIndicador" oncustomexportcell="ASPxPivotGridExporter1_CustomExportCell"></dxpgwx:ASPxPivotGridExporter>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; height: 10px;" valign="top">
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
                                width="100%" ><Fields>
                                    <dxwpg:PivotGridField ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea"
                                        AreaIndex="0" Caption="Perspectiva" FieldName="Perspectiva">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="RowArea" AreaIndex="1" Caption="Tema" FieldName="Tema">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field2" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="RowArea" AreaIndex="2" Caption="Objetivo Estrat&#233;gico" FieldName="objetivoEstrategico">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field3" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="RowArea" AreaIndex="3" Caption="Unidade de Neg&#243;cio" FieldName="unidadeNegocio"
                                        Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field4" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="RowArea" AreaIndex="3" Caption="Iniciativa" FieldName="Iniciativa" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field5" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                        Area="ColumnArea" Caption="In&#237;cio" FieldName="DataInicio" Visible="False" AreaIndex="0">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field6" AllowedAreas="RowArea, ColumnArea" Area="ColumnArea"
                                        AreaIndex="0" Caption="T&#233;rmino" FieldName="Termino">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field7" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1"
                                        Caption="Conclu&#237;do (%)">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field8" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2"
                                        Caption="Despesa Prevista">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field9" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0"
                                        Caption="Despesa Real">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field10" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1"
                                        Caption="Despesa Restante" Visible="False">
                                    </dxwpg:PivotGridField>
                                    <dxwpg:PivotGridField ID="field11" AllowedAreas="DataArea" Area="DataArea" Caption="Varia&#231;&#227;o da Despesa"
                                        Visible="False" AreaIndex="2">
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
</form>
</body>
</html>



