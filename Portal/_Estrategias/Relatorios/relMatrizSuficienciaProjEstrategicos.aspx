<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relMatrizSuficienciaProjEstrategicos.aspx.cs" Inherits="_Estrategias_Relatorios_relMatrizSuficienciaProjEstrategicos"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    &nbsp;<div>
        <table>
            <tr style="height: 26px;
                width: 100%">
                <td align="left" valign="middle" style="padding-left: 10px">
                    <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                        Font-Overline="False" Font-Strikeout="False"
                        Text="Matriz de Suficiência de Projetos Estratégicos"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" Width="100%" ClientInstanceName="pnCallbackDados"
                    OnCallback="pnCallbackDados_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 5px; padding-top: 10px">
                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                            </dxhf:ASPxHiddenField>
                                            <table cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 425px">
                                                            <dxe:ASPxLabel runat="server" Text="Mapa Estrat&#233;gico:"
                                                                ID="ASPxLabel1">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="width: 108px">
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 425px">
                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="417px" Height="22px"
                                                                ClientInstanceName="ddlMapa"  ID="ddlMapa">
                                                                <ItemStyle Wrap="True" >
                                                                    <Paddings Padding="0px" />
                                                                </ItemStyle>
                                                                <Paddings Padding="0px"></Paddings>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 108px">
                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar" Width="100px"
                                                                Height="22px"  ID="btnSelecionar">
                                                                <ClientSideEvents Click="function(s, e) {
	pnCallbackDados.PerformCallback();
}"></ClientSideEvents>
                                                                <Paddings Padding="0px"></Paddings>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 205px">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Height="22px" ClientInstanceName="ddlExporta"
                                                                                                 ID="ddlExporta">
                                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxComboBox>
                                                                                        </td>
                                                                                        <td style="padding-left: 2px">
                                                                                            <dxcp:ASPxCallbackPanel runat="server"  
                                                                                                ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
                                                                                                <PanelCollection>
                                                                                                    <dxp:PanelContent runat="server">
                                                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                                                                            Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao">
                                                                                                        </dxe:ASPxImage>
                                                                                                    </dxp:PanelContent>
                                                                                                </PanelCollection>
                                                                                            </dxcp:ASPxCallbackPanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" Text="Exportar" Height="22px"
                                                                                ID="Aspxbutton1" OnClick="btnExcel_Click">
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 10px; padding-left: 10px">
                                            <dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgMapaEntregas" ID="ASPxPivotGridExporter1"
                                                OnCustomExportCell="ASPxPivotGridExporter1_CustomExportCell">
                                            </dxpgwx:ASPxPivotGridExporter>
                                            <div id="Div1" runat="server">
                                                <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgMapaEntregas"
                                                    Width="99%"  ID="pvgMapaEntregas" OnCustomCellStyle="pvgMapaEntregas_CustomCellStyle">
                                                    <Fields>
                                                        <dxwpg:PivotGridField FieldName="NomeProjeto" ID="fieldNomeProjeto" AllowedAreas="RowArea, ColumnArea"
                                                            Area="ColumnArea" AreaIndex="0" Caption="Nome do Projeto" EmptyCellText="Nenhum"
                                                            EmptyValueText="NA">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="LiderProjeto" ID="fieldLiderProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                            Area="ColumnArea" AreaIndex="0" Visible="False" Caption="L&#237;der do Projeto"
                                                            EmptyCellText="Nenhum" EmptyValueText="NA">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="PercentualConcluido" ID="fieldPercentualConcluido"
                                                            AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Visible="False" Caption="Percentual Conclu&#237;do"
                                                            EmptyValueText="NA" TotalsVisibility="None" CellFormat-FormatString="#,##0.0%"
                                                            CellFormat-FormatType="Numeric">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="ObjetivoEstrategico" ID="fieldObjetivoEstrategico"
                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Caption="Objetivo Estrategico"
                                                            TotalsVisibility="None">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="PossuiObjetivoAssociado" ID="fieldPossuiObjetivoAssociado"
                                                            AllowedAreas="DataArea" Area="DataArea" AreaIndex="0" Caption="Possui Objetivo Associado"
                                                            TotalCellFormat-FormatType="Numeric">
                                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="Perspectiva" ID="fieldPerspectiva" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                            Area="RowArea" AreaIndex="0" Caption="Perspectiva" TotalsVisibility="None">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="StatusProjeto" ID="fieldStatusProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                            Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Status do Projeto" EmptyCellText="Nenhum"
                                                            EmptyValueText="NA">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="FaseProjeto" ID="fieldFaseProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                            Area="ColumnArea" AreaIndex="0" Visible="False" Caption="Fase do Projeto" EmptyCellText="Nenhuma"
                                                            EmptyValueText="NA">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="UnidadeNegocio" ID="fieldUnidadeNegocio" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                            Area="ColumnArea" AreaIndex="1" Visible="False" Caption="Unidade de Neg&#243;cio"
                                                            EmptyCellText="Nenhuma" EmptyValueText="NA" TotalsVisibility="None">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="Tema" ID="fieldTema" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                            Area="RowArea" AreaIndex="1" Visible="False" Caption="Tema">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="ResponsavelTema" ID="fieldResponsavelTema" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                            Area="RowArea" AreaIndex="1" Visible="False" Caption="Respons&#225;vel Tema">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                        <dxwpg:PivotGridField FieldName="ResponsavelObjetivo" ID="fieldResponsavelObjetivo"
                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" Visible="False"
                                                            Caption="Respons&#225;vel Objetivo">
                                                            <CellStyle >
                                                            </CellStyle>
                                                            <HeaderStyle ></HeaderStyle>
                                                            <ValueStyle >
                                                            </ValueStyle>
                                                            <ValueTotalStyle >
                                                            </ValueTotalStyle>
                                                        </dxwpg:PivotGridField>
                                                    </Fields>
                                                    <OptionsView ShowColumnTotals="False" ShowRowTotals="False" ShowColumnGrandTotals="False"
                                                        ShowRowGrandTotals="False"></OptionsView>
                                                    <OptionsPager RowsPerPage="15" Position="Bottom" Visible="False">
                                                        <Summary AllPagesText="P&#225;ginas: {0} - {1} ({2} Registros)" Text="P&#225;ginas: {0} - {1} ({2} Registros)">
                                                        </Summary>
                                                    </OptionsPager>
                                                    <Styles>
                                                        <FilterAreaStyle >
                                                        </FilterAreaStyle>
                                                        <MenuStyle ></MenuStyle>
                                                    </Styles>
                                                </dxwpg:ASPxPivotGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>
