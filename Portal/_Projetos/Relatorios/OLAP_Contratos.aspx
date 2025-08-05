<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="OLAP_Contratos.aspx.cs" Inherits="_Projetos_Relatorios_OLAP_Contratos" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
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
                                    Text="Análise de Contratos de Projetos"></asp:Label></td>
                            <td align="right" style="display: none; width: 8px; height: 26px">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Desempenho:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px">
                                <dxe:ASPxLabel ID="ASPxLabel40" runat="server" 
                                    Text="Ano:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px">
                            </td>
                            <td align="left" style="width: 120px; height: 26px">
                                &nbsp;</td>
                            <td style="width: 5px; height: 26px">
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
                            <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvgContratos">
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
                                <dxwpg:ASPxPivotGrid ID="pvgContratos" runat="server" ClientInstanceName="pvgContratos"
                                     Width="100%" 
                                    OnCustomFieldSort="pvgContratos_CustomFieldSort" 
                                    oncustomsummary="pvgContratos_CustomSummary" ClientIDMode="AutoID"><Fields>
<dxwpg:PivotGridField FieldName="NomeUnidadeNegocio" ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" Caption="Unidade">
<CellStyle ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="NomeProjeto" ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Caption="Projeto/Demanda" EmptyCellText="Sem Projeto" EmptyValueText="Sem Projeto">
<CellStyle ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>

<ValueTotalStyle ></ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="NumeroContrato" ID="field2" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Caption="N&#250;mero Contrato" EmptyValueText="&lt;Sem Projeto&gt;">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="TipoAquisicao" ID="field3" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="3" Caption="Modalidade Aquisi&#231;&#227;o">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="NomeFornecedor" ID="field4" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="4" Caption="Fornecedor">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Status" ID="fldStatus" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Caption="Status">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="ValorPrevisto" ID="field6" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" RunningTotal="True" Caption="Valor Previsto Parcela">
<CellStyle ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>

<ValueTotalStyle ></ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="ValorPago" ID="field7" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1" RunningTotal="True" Caption="Valor Pago Parcela" TotalsVisibility="None">
<CellStyle ></CellStyle>

<HeaderStyle ></HeaderStyle>

<ValueStyle ></ValueStyle>

<ValueTotalStyle ></ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="NumeroParcela" ID="field8" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="Parcela">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DataVencimento" ID="field9" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Data Vencimento" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DataPagamento" ID="field10" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" Visible="False" Caption="Data Pagamento" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DataInicioVigencia" ID="field11" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="In&#237;cio Vig&#234;ncia" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DataTerminoVigencia" ID="field12" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="T&#233;rmino Vig&#234;ncia" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="Objeto" ID="field13" AllowedAreas="RowArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="Objeto">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="HistoricoParcela" ID="field14" AllowedAreas="RowArea" Area="RowArea" AreaIndex="5" Visible="False" Caption="Hist&#243;rico">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="PessoaResponsavel" ID="field15" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="7" Visible="False" Caption="Respons&#225;vel">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="MesVencimento" ID="fldMesVencimento" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Visible="False" Caption="M&#234;s Vencimento" SortMode="Custom">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="AnoVencimento" ID="field17" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" Visible="False" Caption="Ano Vencimento">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="MesPagamento" ID="fldMesPagamento" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Visible="False" Caption="M&#234;s Pagamento" SortMode="Custom">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="AnoPagamento" ID="field19" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" Visible="False" Caption="Ano Pagamento">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="SituacaoParcela" ID="field16" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="0" Caption="Situa&#231;&#227;o Parcelas">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="TipoContrato" ID="fieldTipoContrato" AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="1" Caption="Situa&#231;&#227;o Contrato">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="NumeroAditivoContrato" ID="fieldNumeroAditivoContrato" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Visible="False" Caption="Aditivo">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="ValorContrato" ID="fieldValorContrato" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2" Visible="False" Caption="Valor Contrato" SummaryType="Custom">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>

<ValueTotalStyle ></ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="ValorRestante" ID="fieldValorRestante" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2" Visible="False" Caption="Valor Restante Contrato" SummaryType="Custom">
<CellStyle ></CellStyle>

<ValueStyle ></ValueStyle>

<ValueTotalStyle ></ValueTotalStyle>
</dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="SiglaUnidadeNegocio" ID="fieldSiglaUnidadeNegocio" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="6" Visible="False" Caption="Sigla Un"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="DescricaoTipoContrato" ID="fieldDescricaoTipoContrato" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Visible="False" Caption="Tipo Contrato"></dxwpg:PivotGridField>
<dxwpg:PivotGridField FieldName="FonteRecursosFinanceiros" ID="fieldFonteRecursosFinanceiros" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Visible="False" Caption="Fonte Recursos"></dxwpg:PivotGridField>
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