<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseRisco.aspx.cs" Inherits="_Projetos_Relatorios_relAnaliseRisco" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="height: 26px; padding-left: 10px;">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Análise de Risco"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" 
                    ASPxPivotGridID="grid">
                </dxpgwx:ASPxPivotGridExporter>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 205px; padding-left: 5px;">
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
            <td>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 5px">
                <div id="Div1" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                    <dxwpg:ASPxPivotGrid ID="grid" runat="server" ClientInstanceName="grid"
                        Width="100%"
                        Height="100%" ClientIDMode="AutoID">
                        <OptionsData AutoExpandGroups="True" />
                        <Fields>
                            <dxwpg:PivotGridField ID="fieldDescricao" AllowedAreas="RowArea" Area="RowArea" AreaIndex="1"
                                Caption="Risco" FieldName="Descricao">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldDetalhe" AllowedAreas="RowArea" Area="RowArea" Caption="Detalhes do Risco"
                                FieldName="Detalhe" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldProbabilidade" AllowedAreas="RowArea, ColumnArea"
                                Area="RowArea" AreaIndex="2" Caption="Probabilidade" FieldName="Probabilidade">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldImpacto" AllowedAreas="RowArea, ColumnArea" Area="RowArea"
                                AreaIndex="3" Caption="Impacto" FieldName="Impacto">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldTipo" AllowedAreas="RowArea, ColumnArea" Area="RowArea"
                                Caption="Tipo de Risco" FieldName="Tipo" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldResponsavel" AllowedAreas="RowArea" Area="RowArea"
                                AreaIndex="5" Caption="Nome do Responsável" FieldName="Responsavel">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldStatus" AllowedAreas="RowArea, ColumnArea" Area="ColumnArea"
                                AreaIndex="0" Caption="Status do Risco" FieldName="Status">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldDataLimiteResolucao" AllowedAreas="RowArea, ColumnArea"
                                Area="RowArea" Caption="Limite Resolução" FieldName="DataLimiteResolucao" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldAnoLimiteResolucao" AllowedAreas="RowArea, ColumnArea"
                                Area="RowArea" Caption="Ano Limite Resolução" FieldName="AnoLimiteResolucao"
                                Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldMesLimiteResolucao" AllowedAreas="RowArea, ColumnArea"
                                Area="RowArea" Caption="Mês Limite Resolução" FieldName="MesLimiteResolucao"
                                Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldConsequencia" AllowedAreas="RowArea" Area="RowArea"
                                Caption="Consequência" FieldName="Consequencia" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldTratamento" AllowedAreas="RowArea" Area="RowArea"
                                Caption="Tratamento" FieldName="Tratamento" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldProjeto" AllowedAreas="RowArea" Area="RowArea" AreaIndex="0"
                                Caption="Nome do Projeto" FieldName="Projeto">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldStatusProjeto" AllowedAreas="RowArea" Area="RowArea"
                                AreaIndex="4" Caption="Status do Projeto" FieldName="StatusProjeto">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldTipoProjeto" AllowedAreas="RowArea" Area="RowArea"
                                Caption="Tipo Projeto" FieldName="TipoProjeto" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldUnidadeNegocio" AllowedAreas="RowArea" Area="RowArea"
                                Caption="Unidade de Negócio" FieldName="UnidadeNegocio" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldDataEliminacao" AllowedAreas="RowArea, ColumnArea"
                                Area="RowArea" Caption="Data de Eliminação" FieldName="DataEliminacao" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldAnoEliminacao" AllowedAreas="RowArea, ColumnArea"
                                Area="RowArea" Caption="Ano da Eliminação" FieldName="AnoEliminacao" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldMesEliminacao" AllowedAreas="RowArea, ColumnArea"
                                Area="RowArea" Caption="Mês da Eliminação" FieldName="MesEliminacao" Visible="False">
                            </dxwpg:PivotGridField>
                            <dxwpg:PivotGridField ID="fieldQuantidade" AllowedAreas="DataArea" Area="DataArea"
                                AreaIndex="0" Caption="Quantidade" FieldName="Quantidade">
                            </dxwpg:PivotGridField>
                        </Fields>
                        <OptionsCustomization CustomizationWindowHeight="350" CustomizationWindowWidth="200">
                        </OptionsCustomization>
                        <OptionsLoadingPanel Text=" ">
                        </OptionsLoadingPanel>
                        <OptionsPager EllipsisMode="None">
                        </OptionsPager>
                    </dxwpg:ASPxPivotGrid>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
