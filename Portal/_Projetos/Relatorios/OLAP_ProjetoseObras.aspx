<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="OLAP_ProjetoseObras.aspx.cs" Inherits="_Projetos_Relatorios_OLAP_ProjetoseObras"
    Title="Portal da Estratégia" %>

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
                            <td valign="middle">
                                &nbsp;
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Projetos e Obras"></asp:Label>
                            </td>
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
                                &nbsp;
                            </td>
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
                        <td style="width: 5px" valign="top">
                        </td>
                        <td valign="top">
                            <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" 
                                ASPxPivotGridID="pvgContratos" oncustomexportfieldvalue="ASPxPivotGridExporter1_CustomExportFieldValue"
                                >
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
                                                            <dxp:PanelContent ID="PanelContent1" runat="server">
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
                        <td style="width: 10px; height: 3px" valign="top">
                        </td>
                        <td style="height: 3px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td colspan="1" valign="top">
                            <div id="Div2" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                                <dxwpg:ASPxPivotGrid ID="pvgContratos" runat="server" ClientInstanceName="pvgContratos"
                                     Width="98%" OnCustomFieldSort="pvgContratos_CustomFieldSort"
                                    OnCustomSummary="pvgContratos_CustomSummary" ClientIDMode="AutoID">
                                    <Fields>
                                        <dxwpg:PivotGridField FieldName="NomeProjeto" ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="1" Caption="Projeto" EmptyCellText="Sem Projeto" EmptyValueText="Sem Projeto">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                            <ValueTotalStyle >
                                            </ValueTotalStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="NumeroContrato" ID="field2" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="2" Caption="Número do Contrato" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <HeaderStyle ></HeaderStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="TipoAquisicao" ID="field3" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Caption="Modalidade Aquisi&#231;&#227;o" Visible="False" Options-ShowInCustomizationForm="False"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="NomeFornecedor" ID="field4" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="2" Caption="Razão Social" EmptyCellText="S/I" EmptyValueText="S/I"
                                            Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="Status" ID="fldStatus" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            AreaIndex="0" Caption="Situação do Contrato" Area="RowArea" EmptyCellText="S/I"
                                            EmptyValueText="S/I" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="ValorPrevisto" ID="field6" AllowedAreas="DataArea"
                                            Area="DataArea" AreaIndex="0" Caption="Valor Medido" RunningTotal="True" Visible="False"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <HeaderStyle  />
                                            <ValueStyle >
                                            </ValueStyle>
                                            <ValueTotalStyle >
                                            </ValueTotalStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="ValorPago" ID="field7" AllowedAreas="FilterArea, DataArea"
                                            Area="DataArea" AreaIndex="0" RunningTotal="True" Caption="Valor Pago" TotalsVisibility="None"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <HeaderStyle ></HeaderStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                            <ValueTotalStyle >
                                            </ValueTotalStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="NumeroParcela" ID="field8" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="6" Visible="False" Caption="Parcela" EmptyCellText="S/I"
                                            EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="DataVencimento" ID="field9" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Data Vencimento" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime" EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="DataPagamento" ID="field10" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="ColumnArea" Visible="False" Caption="Data Pagamento" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime" EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="DataInicioVigencia" ID="field11" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="6" Visible="False" Caption="Assinatura" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime" EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="DataTerminoVigencia" ID="field12" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="6" Visible="False" Caption="Término Contrato" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime" EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="Objeto" ID="field13" AllowedAreas="RowArea" Area="RowArea"
                                            AreaIndex="6" Visible="False" Caption="Objeto" EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="HistoricoParcela" ID="field14" AllowedAreas="RowArea"
                                            Area="RowArea" AreaIndex="5" Visible="False" Caption="Hist&#243;rico" EmptyCellText="S/I"
                                            EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="PessoaResponsavel" ID="field15" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="7" Visible="False" Caption="Gestor" EmptyCellText="S/I"
                                            EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="MesVencimento" ID="fldMesVencimento" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="2" Visible="False" Caption="M&#234;s Vencimento" SortMode="Custom"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="AnoVencimento" ID="field17" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="2" Visible="False" Caption="Ano Vencimento" EmptyCellText="S/I"
                                            EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="MesPagamento" ID="fldMesPagamento" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="1" Visible="False" Caption="M&#234;s Pagamento" SortMode="Custom"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="AnoPagamento" ID="field19" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="0" Visible="False" Caption="Ano Pagamento" EmptyCellText="S/I"
                                            EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="SituacaoParcela" ID="field16" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            AreaIndex="0" Caption="Situa&#231;&#227;o Parcelas" Visible="False" EmptyCellText="S/I"
                                            EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="NumeroAditivoContrato" ID="fieldNumeroAditivoContrato"
                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" Visible="False"
                                            Caption="Aditivo" EmptyCellText="S/I" EmptyValueText="S/I" AreaIndex="8">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="ValorContrato" ID="fieldValorContrato" AllowedAreas="FilterArea, DataArea"
                                            Area="DataArea" AreaIndex="0" Caption="Valor do Contrato" SummaryType="Custom"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                            <ValueTotalStyle >
                                            </ValueTotalStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="ValorRestante" ID="fieldValorRestante" AllowedAreas="FilterArea, DataArea"
                                            Area="DataArea" AreaIndex="1" Caption="Saldo Contratual" SummaryType="Custom"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                            <ValueTotalStyle >
                                            </ValueTotalStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="DescricaoTipoContrato" ID="fieldDescricaoTipoContrato"
                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Visible="False"
                                            Caption="Instrumento" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="FonteRecursosFinanceiros" ID="fieldFonteRecursosFinanceiros"
                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" Visible="False"
                                            Caption="Fonte" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldMunicipio" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="0" Caption="Município" FieldName="Municipio" EmptyCellText="S/I"
                                            EmptyValueText="S/I" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldUF" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="UF" FieldName="UF" Visible="False" EmptyCellText="S/I"
                                            EmptyValueText="S/I" AreaIndex="8">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldSegmento" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="0" Caption="Segmento" FieldName="Segmento" EmptyCellText="S/I"
                                            EmptyValueText="S/I" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTipoServico" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="0" Caption="Tipo de Contratacao" FieldName="TipoServico"
                                            EmptyCellText="S/I" EmptyValueText="S/I" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldOrigem" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="Origem" FieldName="Origem" Visible="False" EmptyCellText="S/I"
                                            EmptyValueText="S/I" AreaIndex="8">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTrabalhadoresDiretos" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="Trabalhadores" FieldName="TrabalhadoresDiretos" Visible="False"
                                            EmptyCellText="S/I" EmptyValueText="S/I" AreaIndex="8">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDataBaseReajuste" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="6" Caption="Data-Base" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" FieldName="DataBaseReajuste" GrandTotalCellFormat-FormatString="dd/MM/yyyy"
                                            GrandTotalCellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy"
                                            TotalCellFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy"
                                            ValueFormat-FormatType="DateTime" Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldCriterioReajuste" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="6" Caption="Critério de Reajuste" FieldName="CriterioReajuste"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldCriterioMedicao" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="6" Caption="Critério de Medição" FieldName="CriterioMedicao"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldContatoContratada" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="6" Caption="Gestor da Contratada" FieldName="ContatoContratada"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDescricaoStatus" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="0" Caption="Status do Projeto" FieldName="DescricaoStatus"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldOrigemFornecedor" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="6" Caption="Origem Fornecedor" FieldName="OrigemFornecedor"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldNomeFantasia" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="4" Caption="Nome Fantasia" FieldName="NomeFantasia"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldValorOriginal" AllowedAreas="FilterArea, DataArea"
                                            Area="DataArea" Caption="Valor Original Contrato" 
                                            FieldName="ValorOriginal" SummaryType="Custom"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I" AreaIndex="1">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDataTerminoOriginal" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="4" Caption="Término Original" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" FieldName="DataTerminoOriginal" GrandTotalCellFormat-FormatString="dd/MM/yyyy"
                                            GrandTotalCellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy"
                                            TotalCellFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy"
                                            ValueFormat-FormatType="DateTime" Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldValorAditado" AllowedAreas="FilterArea, DataArea"
                                            Area="DataArea" Caption="Valor Aditado" FieldName="ValorAditado" SummaryType="Custom"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I" AreaIndex="1">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTerminoPrevisto" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="6" Caption="Término Previsto" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" ExpandedInFieldsGroup="False" FieldName="TerminoPrevisto"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" Visible="False"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTerminoReprogramado" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="6" Caption="Término Reprogramado" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" FieldName="TerminoReprogramado" GrandTotalCellFormat-FormatString="dd/MM/yyyy"
                                            GrandTotalCellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy"
                                            TotalCellFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy"
                                            ValueFormat-FormatType="DateTime" Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldAnoAssinatura" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="5" Caption="Ano Assinatura" FieldName="AnoAssinatura"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldMesAssinatura" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="5" Caption="Mês Assinatura" FieldName="MesAssinatura"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldAnoTermino" AllowedAreas="RowArea, ColumnArea" Area="RowArea"
                                            AreaIndex="5" Caption="Ano Término" FieldName="AnoTermino" Visible="False" EmptyCellText="S/I"
                                            EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldMesTermino" AllowedAreas="RowArea, ColumnArea" Area="RowArea"
                                            AreaIndex="6" Caption="Mês Término" FieldName="MesTermino" Visible="False" EmptyCellText="S/I"
                                            EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTemAditivo" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="5" Caption="Tem Aditivo" FieldName="TemAditivo" Visible="False"
                                            EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldQtdAditivo" AllowedAreas="FilterArea, DataArea" Area="DataArea"
                                            AreaIndex="2" Caption="Qtd. Aditivos" FieldName="QtdAditivo" SummaryType="Custom"
                                            Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="f_SiglaUnidade" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="Sigla Unidade" FieldName="SiglaUnidade" AreaIndex="1"
                                            Width="40" Visible="False" EmptyCellText="S/I" EmptyValueText="S/I">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldNumeroInterno2" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="DD" FieldName="NumeroInterno2" Visible="False" EmptyCellText="S/I"
                                            EmptyValueText="S/I" AreaIndex="8">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldNumeroInterno3" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="RD" FieldName="NumeroInterno3" Visible="False" EmptyCellText="S/I"
                                            EmptyValueText="S/I" AreaIndex="8">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDataUltimaAlteracao" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="3" Caption="Última Atualização do Projeto " CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" EmptyCellText="S/I" EmptyValueText="S/I" FieldName="DataUltimaAlteracao"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime">
                                            <CellStyle >
                                            </CellStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldPercentualRealizacao" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="2" Caption="% Físico Concluído " CellFormat-FormatString="p"
                                            CellFormat-FormatType="Numeric" EmptyCellText="S/I" EmptyValueText="S/I" FieldName="PercentualRealizacao"
                                            ValueFormat-FormatString="p" ValueFormat-FormatType="Numeric">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTipoProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" Caption="Tipo de Projeto" FieldName="TipoProjeto" 
                                            Visible="False" AreaIndex="8">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTerminoPrevistoObraPBA" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" Caption="Término Pactuado" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" EmptyCellText="S/I" EmptyValueText="S/I" FieldName="TerminoPrevistoObraPBA"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" 
                                            Visible="False" AreaIndex="8">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTerminoRepactuado" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="4" Caption="Término Repactuado" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" EmptyCellText="S/I" EmptyValueText="S/I" FieldName="TerminoRepactuado"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldAnoTerminoPactuado" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="4" Caption="Ano Término Pactuado" FieldName="AnoTerminoPactuado"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldAnoTerminoRepactuado" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="4" Caption="Ano Término Repactuado" FieldName="AnoTerminoRepactuado"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldDataOSExterna" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="4" Caption="Data OS Externa" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" EmptyCellText="S/I" EmptyValueText="S/I" FieldName="DataOSExterna"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            TotalValueFormat-FormatString="dd/MM/yyyy" TotalValueFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldNumeroOSInterna" AllowedAreas="RowArea, ColumnArea"
                                            Area="RowArea" AreaIndex="4" Caption="Número da OS Interna" FieldName="NumeroOSInterna"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldFisicoPrevisto" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" Caption="% Físico Previsto" CellFormat-FormatString="p" CellFormat-FormatType="Numeric"
                                            EmptyCellText="S/I" EmptyValueText="S/I" FieldName="FisicoPrevisto" TotalCellFormat-FormatString="p"
                                            TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="p" TotalValueFormat-FormatType="Numeric"
                                            ValueFormat-FormatString="p" ValueFormat-FormatType="Numeric" 
                                            Visible="False" AreaIndex="8">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldetapacontratacao" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="4" Caption="Etapa Atual de Contratação" FieldName="etapacontratacao"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldstatusetapacontratacao" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="4" Caption="Status Atual de Contratação" FieldName="statusetapacontratacao"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldInicioPrevisto" 
                                            AllowedAreas="RowArea, FilterArea" Area="RowArea" AreaIndex="4" 
                                            Caption="Início Previsto" FieldName="InicioPrevisto" 
                                            TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" 
                                            TotalValueFormat-FormatString="dd/MM/yyyy" 
                                            TotalValueFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" 
                                            ValueFormat-FormatType="DateTime" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldInicioReprogramado" 
                                            AllowedAreas="RowArea, FilterArea" Area="RowArea" AreaIndex="5" 
                                            Caption="Início Reprogramado" FieldName="InicioReprogramado" 
                                            TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" 
                                            TotalValueFormat-FormatString="dd/MM/yyyy" 
                                            TotalValueFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" 
                                            ValueFormat-FormatType="DateTime" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldInicioReal" AllowedAreas="RowArea, FilterArea" 
                                            Area="RowArea" AreaIndex="6" Caption="Início Real" FieldName="InicioReal" 
                                            TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime" 
                                            TotalValueFormat-FormatString="dd/MM/yyyy" 
                                            TotalValueFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" 
                                            ValueFormat-FormatType="DateTime" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTerminoReal" Area="RowArea" AreaIndex="7" 
                                            Caption="Término Real" FieldName="TerminoReal" 
                                            AllowedAreas="RowArea, FilterArea" TotalCellFormat-FormatString="dd/MM/yyyy" 
                                            TotalCellFormat-FormatType="DateTime" 
                                            TotalValueFormat-FormatString="dd/MM/yyyy" 
                                            TotalValueFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy" 
                                            ValueFormat-FormatType="DateTime" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldCorStatusProjeto" 
                                            AllowedAreas="RowArea, FilterArea" Area="RowArea" AreaIndex="4" 
                                            Caption="Desempenho" FieldName="CorStatusProjeto" 
                                            CellFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" 
                                            CellFormat-FormatType="Custom" 
                                            ValueFormat-FormatString="&lt;img alt='' src='../../imagens/{0}Menor.gif' /&gt;" 
                                            ValueFormat-FormatType="Custom">
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </CellStyle>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ValueStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </ValueStyle>
                                            <ValueTotalStyle HorizontalAlign="Center">
                                            </ValueTotalStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldNomeUnidadeNegocio" 
                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="5" 
                                            Caption="Nome Unidade" FieldName="NomeUnidadeNegocio">
                                        </dxwpg:PivotGridField>
                                    </Fields>
                                    <OptionsDataField AreaIndex="0" />
                                </dxwpg:ASPxPivotGrid>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="middle">
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False" Text="S/I = Sem Informação"></asp:Label>
                        </td>
                        <td valign="top">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
