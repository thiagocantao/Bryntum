<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="OLAP_Demandas.aspx.cs" Inherits="_Projetos_VisaoDemandas_OLAP_Demandas" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height:26px">
                <td valign="middle" style="width: 738px">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 99%">
                        <tr>
                            <td style="height: 26px; padding-left: 10px;" valign="middle">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Alocação (TimeSheet)"></asp:Label></td>
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
                            <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgDemandas">
                            </dxpgwx:ASPxPivotGridExporter>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td id="tabelaFiltros" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:<%=larguraTabela %>" id="tabelaFiltros">
                                <tr>
                                    <td align="left">
                                        <table>
                                            <tr>
                            <td align="right" style="padding-right: 5px; height: 26px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Período:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" style="padding-right: 5px; width: 105px; height: 26px" valign="middle">
                                <dxe:ASPxDateEdit ID="dteInicio" runat="server" AllowNull="False" ClientInstanceName="dteInicio"
                                    Font-Overline="False" Width="105px" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="right" style="padding-right: 5px; width: 10px; height: 26px">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="a" Width="10px">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" style="padding-right: 5px; width: 105px; height: 26px" valign="middle">
                                <dxe:ASPxDateEdit ID="dteFim" runat="server" AllowNull="False" ClientInstanceName="dteFim"
                                     Font-Strikeout="False" Width="105px" DisplayFormatString="dd/MM/yyyy">
                                    <ClientSideEvents ButtonClick="function(s, e) {
btnSelecionar.SetEnabled(true);
}" ValueChanged="function(s, e) {
	btnSelecionar.SetEnabled(true);
}" />
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 85px; height: 26px" valign="middle">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" ClientInstanceName="btnSelecionar"
                                     Text="Selecionar" Width="85px" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
    if(dteInicio.GetValue()!= null &amp;&amp; dteFim.GetValue()  != null)
    {
        var dataInicio 	  = new Date(dteInicio.GetValue());
		var meuDataInicio = (dataInicio.getMonth() +1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
		dataInicio  	  = Date.parse(meuDataInicio);
		
		
		var dataTermino 	= new Date(dteFim.GetValue());
		var meuDataTermino 	= (dataTermino.getMonth() +1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
		dataTermino		    = Date.parse(meuDataTermino);

		if(dataInicio &gt; dataTermino)
        {
			e.ProcessOnServer = false;            
			window.top.mostraMensagem(&quot;A Data de Início não pode ser maior que a Data de Término!&quot;, 'Atencao', true, false, null);
        }
		else
		{
			pvgDemandas.PerformCallback(&quot;preenche&quot;);
		}    
    }
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right">
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
                                <dxwpg:ASPxPivotGrid ID="pvgDemandas" runat="server" ClientInstanceName="pvgDemandas"
                                     Width="100%" OnCustomFieldSort="pvgDemandas_CustomFieldSort" OnCustomCallback="pvgDemandas_CustomCallback"><Fields>
<dxwpg:PivotGridField FieldName="Unidade" ID="fldUnidade" Area="RowArea" AreaIndex="0" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Unidade">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="IndicaProjetoDemanda" ID="field1" Area="RowArea" AreaIndex="0" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Tipo de A&#231;&#227;o">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Status" ID="field2" Area="RowArea" AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Status">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="NomeProjetoDemanda" ID="field3" Area="RowArea" AreaIndex="2" Visible="False" Caption="Nome da A&#231;&#227;o">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="NomeRecurso" ID="field4" Area="RowArea" AreaIndex="2" Visible="False" Caption="Nome do Recurso">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Ano" ID="fldAno" Area="ColumnArea" AreaIndex="0" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Ano">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Mes" ID="fldMes" Area="ColumnArea" AreaIndex="1" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="M&#234;s" SortMode="Custom">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Alocacao" ID="field9" Area="DataArea" AreaIndex="0" AllowedAreas="DataArea" Caption="Aloca&#231;&#227;o (horas)" CellFormat-FormatString="#,##0.0" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0.0" GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="#,##0.0" ValueFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0" TotalValueFormat-FormatType="Numeric">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
    <ValueTotalStyle >
    </ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="TipoAtividade" ID="field6" Area="RowArea" AreaIndex="0" Caption="Tipo de Atividade">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
    <ValueTotalStyle >
    </ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Dia" ID="field7" Area="ColumnArea" AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Dia" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
    <ValueTotalStyle >
    </ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Semana" ID="fldSemana" Area="ColumnArea" AreaIndex="2" Visible="False" AllowedAreas="RowArea, ColumnArea, FilterArea" Caption="Semana" SortMode="Custom">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
    <ValueTotalStyle >
    </ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Alocacao" ID="field8" Area="DataArea" AreaIndex="1" AllowedAreas="DataArea" Caption="Percentual" SummaryDisplayType="PercentOfColumn" CellFormat-FormatString="#,##0.0%;#,##0.0%;" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#,##0.0%;#,##0.0%;" TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0.0%;#,##0.0%;" GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="#,##0.0%;#,##0.0%;" ValueFormat-FormatType="Numeric" TotalValueFormat-FormatString="#,##0.0%;#,##0.0%;" TotalValueFormat-FormatType="Numeric">
    <CellStyle >
    </CellStyle>
    <HeaderStyle  />
    <ValueStyle >
    </ValueStyle>
    <ValueTotalStyle >
    </ValueTotalStyle>
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
                  
 
   
     
    
  
  
   
 

</asp:Content>

