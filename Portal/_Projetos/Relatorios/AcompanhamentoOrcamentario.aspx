<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="AcompanhamentoOrcamentario.aspx.cs" Inherits="_Projetos_Relatorios_AcompanhamentoOrcamentario" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table class="headerGrid" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" 
                    style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); width: 100%">
                    <tr style="height:26px">
                        <td valign="middle">
                            <table border="0" cellpadding="0" cellspacing="0" 
                                style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif); width: 100%">
                                <tr style="height:26px">
                                    <td style="height: 26px;padding-left:10px">
                                       <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" 
                                                        Font-Bold="True" Font-Overline="False" 
                                                        Font-Strikeout="False" Text="Acompanhamento Orçamentário"></asp:Label>
                                    </td>
                                    <td align="right">
                                                                            <table cellspacing="0" cellpadding="0" 
                                            border="0" __designer:mapid="ee" align="right">
                                                                                <tbody __designer:mapid="ef">
                                                                                    <tr __designer:mapid="f0">
                                                                                        <td style="width: 116px" __designer:mapid="f1">
                                                                                            <table cellspacing="0" cellpadding="0" border="0" __designer:mapid="f2">
                                                                                                <tbody __designer:mapid="f3">
                                                                                                    <tr __designer:mapid="f4">
                                                                                                        <td __designer:mapid="f5">
                                                                                                            <dxe:ASPxComboBox runat="server" Width="70px" ClientInstanceName="ddlExporta" 
                                                                                                                 ID="ddlExporta">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}"></ClientSideEvents>
</dxe:ASPxComboBox>

                                                                                                        </td>
                                                                                                        <td style="padding-right: 3px; padding-left: 3px" __designer:mapid="f8">
                                                                                                            <dxcp:ASPxCallbackPanel runat="server"  
                                                                                                                 ClientInstanceName="pnImage" Width="23px" 
                                                                                                                Height="22px" ID="pnImage" OnCallback="pnImage_Callback"><PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                                                        <dxe:ASPxImage runat="server" 
                                                                                                                            ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px" Height="20px" 
                                                                                                                            ClientInstanceName="imgExportacao" ID="imgExportacao"></dxe:ASPxImage>

                                                                                                                    </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>

                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td __designer:mapid="fd">
                                                                                            <dxe:ASPxButton runat="server" Text="Exportar" 
                                                                                                ID="Aspxbutton1" OnClick="btnExcel_Click">
<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                    </td>
                                </tr>
                            </table>

                                                                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-Bottom: 0px; padding: 10px">
                <table cellpadding="0" cellspacing="0" class="headerGrid">
                    <tr>
                        <td>
                        <div id="divGrid" runat="server">
                                                            <dxpgwx:ASPxPivotGridExporter runat="server" 
                                ASPxPivotGridID="pvgAcompanhamentoOrcamentario" ID="ASPxPivotGridExporter1"></dxpgwx:ASPxPivotGridExporter>

                                                                <dxwpg:ASPxPivotGrid runat="server" 
                                ClientInstanceName="pvgAcompanhamentoOrcamentario" Width="100%" 
                                 ClientIDMode="AutoID" 
                                ID="pvgAcompanhamentoOrcamentario">

                                                                    <Fields>
                                                                        <dxwpg:PivotGridField ID="fieldAno" 
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" AreaIndex="0" 
                                                                            Caption="Ano" FieldName="Ano" ValueFormat-FormatType="Numeric">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldDespesaReceita" 
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" 
                                                                            Caption="Despesa/Receita" FieldName="DespesaReceita" Visible="False">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldNomeProjeto" 
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="0" 
                                                                            Caption="Nome do Projeto" FieldName="NomeProjeto">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldNomeAcao" 
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1" 
                                                                            Caption="Nome Ação" FieldName="NomeAcao">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldCONTADES" 
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="2" 
                                                                            Caption="Conta Orçamentária" FieldName="CONTA_DES">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldQuantidade" AllowedAreas="RowArea" 
                                                                            Area="RowArea" AreaIndex="3" Caption="Quantidade" FieldName="Quantidade">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldValorUnitario" 
                                                                            AllowedAreas="RowArea" Area="RowArea" AreaIndex="4" 
                                                                            Caption="Valor Unitário" CellFormat-FormatString="c2" 
                                                                            CellFormat-FormatType="Custom" FieldName="ValorUnitario" 
                                                                            GrandTotalCellFormat-FormatString="c2" GrandTotalCellFormat-FormatType="Custom" 
                                                                            TotalCellFormat-FormatString="c2" TotalCellFormat-FormatType="Custom" 
                                                                            TotalValueFormat-FormatString="c2" TotalValueFormat-FormatType="Custom" 
                                                                            ValueFormat-FormatString="c2" ValueFormat-FormatType="Custom">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldValorProposto" AllowedAreas="DataArea" 
                                                                            Area="DataArea" AreaIndex="0" Caption="Valor Proposto" 
                                                                            FieldName="ValorProposto">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldValorRealizado" AllowedAreas="DataArea" 
                                                                            Area="DataArea" AreaIndex="1" Caption="Valor Realizado" 
                                                                            FieldName="ValorRealizado">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldDisponibilidadeAtual" AllowedAreas="DataArea" 
                                                                            Area="DataArea" AreaIndex="2" Caption="Disponibilidade Atual" 
                                                                            FieldName="DisponibilidadeAtual">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldValorSuplemento" AllowedAreas="DataArea" 
                                                                            Area="DataArea" AreaIndex="3" Caption="Valor Suplemento" 
                                                                            FieldName="ValorSuplemento">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldValorTransposto" AllowedAreas="DataArea" 
                                                                            Area="DataArea" AreaIndex="4" Caption="Valor Transposto" 
                                                                            FieldName="ValorTransposto">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldDisponibilidadeReformulada" 
                                                                            AllowedAreas="DataArea" Area="DataArea" AreaIndex="7" 
                                                                            Caption="Disponibilidade Reformulada" 
                                                                            FieldName="DisponibilidadeReformulada">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldValorSuplementacaoOld" AllowedAreas="DataArea" 
                                                                            Area="DataArea" AreaIndex="5" Caption="Suplemento Anterior" 
                                                                            FieldName="ValorSuplementacao_Old">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fieldValorTransposicaoOld" AllowedAreas="DataArea" 
                                                                            Area="DataArea" AreaIndex="6" Caption="Transposto Anterior" 
                                                                            FieldName="ValorTransposicao_Old">
                                                                        </dxwpg:PivotGridField>
                                                                    </Fields>

<OptionsPager Visible="False"></OptionsPager>

<Styles>
<HeaderStyle ></HeaderStyle>

<AreaStyle ></AreaStyle>

<DataAreaStyle ></DataAreaStyle>

<ColumnAreaStyle ></ColumnAreaStyle>

<FieldValueStyle ></FieldValueStyle>

<CellStyle ></CellStyle>

<MenuItemStyle ></MenuItemStyle>

<MenuStyle ></MenuStyle>
</Styles>
</dxwpg:ASPxPivotGrid>
</div>

                                                            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

