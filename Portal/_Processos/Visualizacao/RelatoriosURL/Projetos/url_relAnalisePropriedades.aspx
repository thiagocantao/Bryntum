<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relAnalisePropriedades.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_relAnalisePropriedades"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <dxwgv:ASPxGridViewExporter ID="GridViewExporter1" runat="server" 
                    GridViewID="gvPropriedades" onrenderbrick="GridViewExporter1_RenderBrick">
                </dxwgv:ASPxGridViewExporter>
                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                </dxhf:ASPxHiddenField>
            </td>
        </tr>
        <tr>
            <td style="height: 3px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td >
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding-left: 5px;">
                    <tr>
                        <td style="width: 208px">
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="Div1" runat="server" style="padding-left: 5px; padding-right: 5px; padding-top: 3px;
                    overflow: auto; position: inherit; display: inherit;">
                    <dxtc:ASPxPageControl ID="pgControlPropriedades" runat="server" ActiveTabIndex="0"
                         Width="100%" ClientInstanceName="pgControlPropriedades">
                        <TabPages>
                            <dxtc:TabPage Text="Propriedades">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxwgv:ASPxGridView ID="gvPropriedades" runat="server" AutoGenerateColumns="False"
                                            ClientInstanceName="gvPropriedades"  KeyFieldName="SequencialImovel"
                                            Width="100%">
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem DisplayFormat="Área Total:{0:n4}" FieldName="AreaTotal" ShowInColumn="Área Total (ha)"
                                                    SummaryType="Sum" ValueDisplayFormat="Área Total:{0:n4}" ShowInGroupFooterColumn="Área Total (ha)" />
                                                <dxwgv:ASPxSummaryItem DisplayFormat="Área Atingida:{0:n4}" ShowInColumn="Área Atingida (ha)"
                                                    ShowInGroupFooterColumn="Área Atingida (ha)" SummaryType="Sum" ValueDisplayFormat="Área Atingida:{0:n4}"
                                                    FieldName="AreaAtingida" />
                                                <dxwgv:ASPxSummaryItem DisplayFormat="Quantidade: {0}" ShowInColumn="SEQ" ShowInGroupFooterColumn="SEQ"
                                                    SummaryType="Count" ValueDisplayFormat="Quantidade: {0}" />
                                                <dxwgv:ASPxSummaryItem DisplayFormat="Área Averbada:{0:n4}" FieldName="AreaAverbada"
                                                    ShowInColumn="Área Averbada (ha)" ShowInGroupFooterColumn="Área Averbada (ha)"
                                                    SummaryType="Sum" ValueDisplayFormat="Área Averbada:{0:n4}" />
                                                <dxwgv:ASPxSummaryItem DisplayFormat="Área Registrada:{0:n4}" FieldName="AreaRegistrada"
                                                    ShowInColumn="Área Registrada (ha)" ShowInGroupFooterColumn="Área Registrada (ha)"
                                                    ValueDisplayFormat="Área Registrada:{0:n4}" SummaryType="Sum" />
                                            </TotalSummary>
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Caption="SEQ" FieldName="SequencialImovel" ShowInCustomizationForm="True"
                                                    VisibleIndex="0">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Localização" FieldName="Localizacao" ShowInCustomizationForm="True"
                                                    VisibleIndex="1" Width="300px">
                                                    <PropertiesTextEdit>
                                                        <Style >
                                                            
                                                        </Style>
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AllowSort="True" AutoFilterCondition="Contains" />
                                                    <EditCellStyle >
                                                    </EditCellStyle>
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                    <HeaderStyle  />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Município" FieldName="NomeMunicipio" ShowInCustomizationForm="True"
                                                    VisibleIndex="2">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AllowSort="True" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Distrito" FieldName="Distrito" ShowInCustomizationForm="True"
                                                    VisibleIndex="3">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AllowSort="True" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Comarca" FieldName="Comarca" ShowInCustomizationForm="True"
                                                    VisibleIndex="4">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AllowSort="True" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Região" FieldName="NomeRegiao" ShowInCustomizationForm="True"
                                                    VisibleIndex="5">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Identificação Fundiária" FieldName="IdentificacaoFundiaria"
                                                    ShowInCustomizationForm="True" VisibleIndex="6">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AllowSort="True" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Coordenadas" FieldName="Coordenadas" ShowInCustomizationForm="True"
                                                    VisibleIndex="7">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AllowSort="False" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Área Total (ha)" FieldName="AreaTotal" ShowInCustomizationForm="True"
                                                    VisibleIndex="8" Width="250px">
                                                    <PropertiesTextEdit DisplayFormatString="n4">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" AllowAutoFilter="False" AllowHeaderFilter="False" AllowSort="True" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Área Atingida (ha)" FieldName="AreaAtingida"
                                                    ShowInCustomizationForm="True" VisibleIndex="9" Width="250px">
                                                    <PropertiesTextEdit DisplayFormatString="n4">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" AllowAutoFilter="False" AllowHeaderFilter="False" AllowSort="True" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Espólio" FieldName="IndicaEspolio" ShowInCustomizationForm="True"
                                                    VisibleIndex="10">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                    <DataItemTemplate>
                                                        <%# (Eval("IndicaEspolio").ToString() == "S")? "Sim" : "Não"%>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Inventariante" FieldName="NomeInventariante"
                                                    ShowInCustomizationForm="True" VisibleIndex="11">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AllowSort="True" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Juízo" FieldName="JuizoEspolio" ShowInCustomizationForm="True"
                                                    VisibleIndex="12">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Cartório Espólio" FieldName="CartorioEspolio"
                                                    ShowInCustomizationForm="True" VisibleIndex="13">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Advogado (Espólio)" FieldName="NomeAdvogadoEspolio"
                                                    ShowInCustomizationForm="True" VisibleIndex="14">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Endereço Advogado" FieldName="EnderecoAdvogadoEspolio"
                                                    ShowInCustomizationForm="True" VisibleIndex="15">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Telefone (Advogado)" FieldName="TelefoneAdvogadoEspolio"
                                                    ShowInCustomizationForm="True" VisibleIndex="16">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Dados Última Declaração ITR" FieldName="DadosUltimaDeclaracaoITR"
                                                    ShowInCustomizationForm="True" VisibleIndex="17">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Área Averbada (ha)" FieldName="AreaAverbada"
                                                    ShowInCustomizationForm="True" VisibleIndex="18" Width="250px">
                                                    <PropertiesTextEdit DisplayFormatString="n4">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="False" AllowAutoFilter="False" AllowHeaderFilter="False" AllowSort="True" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Área Registrada (ha)" FieldName="AreaRegistrada"
                                                    ShowInCustomizationForm="True" VisibleIndex="19" Width="250px">
                                                    <PropertiesTextEdit DisplayFormatString="n4">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" AllowAutoFilter="False" AllowHeaderFilter="False" AllowSort="True" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" ShowInCustomizationForm="True"
                                                    VisibleIndex="20" Width="300px">
                                                    <Settings AllowGroup="True" AllowHeaderFilter="False" AllowSort="True" AutoFilterCondition="Contains" />
                                                    <FilterCellStyle >
                                                    </FilterCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True" HorizontalScrollBarMode="Visible"
                                                ShowFooter="True" ShowFilterRow="True" ShowHeaderFilterBlankItems="False" />
                                            <Paddings Padding="0px" />
                                            <Styles>
                                                <FilterRow >
                                                </FilterRow>
                                                <FilterCell >
                                                </FilterCell>
                                            </Styles>
                                        </dxwgv:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Text="Ocupantes e Proprietários">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxwgv:ASPxGridView ID="gvOcupantes" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvOcupantes"
                                             KeyFieldName="SequencialImovel" Width="99%">
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem DisplayFormat="Quantidade:{0}" FieldName="SequencialImovel"
                                                    ShowInColumn="SEQ" ShowInGroupFooterColumn="SEQ" SummaryType="Count" ValueDisplayFormat="Quantidade:{0}" />
                                                <dxwgv:ASPxSummaryItem DisplayFormat="Área Total:{0:n4}" FieldName="AreaTotal" ShowInColumn="Área Total (ha)"
                                                    ShowInGroupFooterColumn="Área Total (ha)" SummaryType="Sum" ValueDisplayFormat="Área Total:{0:n4}" />
                                                <dxwgv:ASPxSummaryItem DisplayFormat="Área Atingida:{0:n4}" FieldName="AreaAtingida"
                                                    ShowInColumn="Área Atingida (ha)" ShowInGroupFooterColumn="Área Atingida (ha)"
                                                    SummaryType="Sum" ValueDisplayFormat="Área Atingida:{0}" />
                                            </TotalSummary>
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Caption="SEQ" FieldName="SequencialImovel" ShowInCustomizationForm="True"
                                                    VisibleIndex="0" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Localização" FieldName="Localizacao" ShowInCustomizationForm="True"
                                                    VisibleIndex="1">
                                                    <Settings AllowGroup="True" AllowAutoFilter="True" AllowSort="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Município" FieldName="NomeMunicipio" ShowInCustomizationForm="True"
                                                    VisibleIndex="2">
                                                    <Settings AllowGroup="True" AllowAutoFilter="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Distrito" FieldName="Distrito" ShowInCustomizationForm="True"
                                                    VisibleIndex="3">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Comarca" FieldName="Comarca" ShowInCustomizationForm="True"
                                                    VisibleIndex="4">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Região" FieldName="NomeRegiao" ShowInCustomizationForm="True"
                                                    VisibleIndex="5">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Identificação Fundiária" FieldName="IdentificacaoFundiaria"
                                                    ShowInCustomizationForm="True" VisibleIndex="6">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Coordenadas" FieldName="Coordenadas" ShowInCustomizationForm="True"
                                                    VisibleIndex="7">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Área Total (ha)" FieldName="AreaTotal" ShowInCustomizationForm="True"
                                                    VisibleIndex="8" Width="250px">
                                                    <PropertiesTextEdit DisplayFormatString="n4">
                                                        <Style >
                                                            
                                                        </Style>
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="False" AllowAutoFilter="False" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Área Atingida (ha)" FieldName="AreaAtingida"
                                                    ShowInCustomizationForm="True" VisibleIndex="9" Width="250px">
                                                    <PropertiesTextEdit DisplayFormatString="n4">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="False" AllowAutoFilter="False" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Espólio" FieldName="IndicaEspolio" ShowInCustomizationForm="True"
                                                    VisibleIndex="10">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                    <DataItemTemplate>
                                                        <%# (Eval("IndicaEspolio") + "" == "S") ? "Sim":"Não" %>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Inventariante" FieldName="NomeInventariante"
                                                    ShowInCustomizationForm="True" VisibleIndex="11">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Juízo" FieldName="JuizoEspolio" ShowInCustomizationForm="True"
                                                    VisibleIndex="12">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Cartório (Espólio)" FieldName="CartorioEspolio"
                                                    ShowInCustomizationForm="True" VisibleIndex="13">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Advogado (Espólio)" FieldName="NomeAdvogadoEspolio"
                                                    ShowInCustomizationForm="True" VisibleIndex="14">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Endereço Advogado" FieldName="EnderecoAdvogadoEspolio"
                                                    ShowInCustomizationForm="True" VisibleIndex="15">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Telefone Advogado" FieldName="TelefoneAdvogadoEspolio"
                                                    ShowInCustomizationForm="True" VisibleIndex="16">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Dados Última Declaração ITR" FieldName="DadosUltimaDeclaracaoITR"
                                                    ShowInCustomizationForm="True" VisibleIndex="17">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" ShowInCustomizationForm="True"
                                                    VisibleIndex="18">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Pessoa" FieldName="NomePessoa" ShowInCustomizationForm="True"
                                                    VisibleIndex="19">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Data de Nasc." FieldName="DataNascimentoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="20">
                                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Nacionalidade" FieldName="NacionalidadePessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="21">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="País" FieldName="NomePaisPessoa" ShowInCustomizationForm="True"
                                                    VisibleIndex="22">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Naturalidade" FieldName="NomeMunicipio" ShowInCustomizationForm="True"
                                                    VisibleIndex="23">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Profissão" FieldName="ProfissaoPessoa" ShowInCustomizationForm="True"
                                                    VisibleIndex="24">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="CPF" FieldName="NumeroCPFCNPJPessoa" ShowInCustomizationForm="True"
                                                    VisibleIndex="25">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Sabe Assinar?" FieldName="PessoaSabeAssinar"
                                                    ShowInCustomizationForm="True" VisibleIndex="26">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Tipo de Documento" FieldName="TipoDocumentoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="27">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Núm. Documento" FieldName="NumeroDocumentoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="28">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Órgão Expedidor Doc." FieldName="OrgaoExpedidorDocumentoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="29">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Nome do Pai" FieldName="NomePaiPessoa" ShowInCustomizationForm="True"
                                                    VisibleIndex="30">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Nome da Mãe" FieldName="NomeMaePessoa" ShowInCustomizationForm="True"
                                                    VisibleIndex="31">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Estado Civil" FieldName="EstadoCivilPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="32">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Regime Separação" FieldName="RegimeSeparacaoBensPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="33">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Certidão Estado Civil" FieldName="CertidaoEstadoCivilPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="34">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Livro Estado Civil" FieldName="LivroCertidaoEstadoCivilPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="35">
                                                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Folha Estado Civil" FieldName="FolhaCertidaoEstadoCivilPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="36">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Emissão Doc. Estado Civil" FieldName="EmissaoCertidaoEstadoCivilPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="37">
                                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Cartório Estado Civil" FieldName="NomeCartorioCertidaoEstadoCivilPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="38">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Autos Separação" FieldName="AutosSeparacaoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="39">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Data Separação" FieldName="DataSeparacaoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="40">
                                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Juízo Separação" FieldName="JuizoSeparacaoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="41">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Data União Estável" FieldName="DataUniaoEstavel"
                                                    ShowInCustomizationForm="True" VisibleIndex="42">
                                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Certidão Óbito" FieldName="pesImov.FolhaCertidaoViuvoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="43">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Livro Certidão Óbito" FieldName="LivroCertidaoViuvoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="44">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Escritura Pacto Antenupcial" FieldName="IndicaEscrituraRegistroPactoAnteNupcialPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="45">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Número Pacto Antenupcial" FieldName="NumeroPactoAnteNupcialPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="46">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Folha Pacto Antenupcial" FieldName="FolhaRegistroPactoAnteNupcialPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="47">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Livro Pacto Antenupcial" FieldName="LivroRegistroPactoAnteNupcialPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="48">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Cartório Pacto Antenupcial" FieldName="NomeCartorioRegistroPactoAnteNupcialPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="49">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Endereço Residencial" FieldName="EnderecoResidencialPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="50">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Número Endereço Residencial" FieldName="NumeroEnderecoResidencialPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="51">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Município Residencial" FieldName="NomeMunicipioResPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="52">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Telefone" FieldName="TelefonePessoa" ShowInCustomizationForm="True"
                                                    VisibleIndex="53">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Tempo Ocupação Imóvel" FieldName="TempoOcupacaoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="54">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Nome Cônjuge" FieldName="NomeConjuge" ShowInCustomizationForm="True"
                                                    VisibleIndex="55">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="DataNascimentoConjuge" ShowInCustomizationForm="True"
                                                    VisibleIndex="56">
                                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Nacionalidade Cônjuge" FieldName="NacionalidadeConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="57">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="País Conjuge" FieldName="NomePaisConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="58">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Naturalidade Cônjuge" FieldName="NomeMunicipio"
                                                    ShowInCustomizationForm="True" VisibleIndex="59">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Profissão Cônjuge" FieldName="ProfissaoConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="60">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="CPF Cônjuge" FieldName="NumeroCPFCNPJConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="61">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Cônjuge Sabe Assinar?" FieldName="ConjugeSabeAssinar"
                                                    ShowInCustomizationForm="True" VisibleIndex="62">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Tipo Doc. Cônjuge" FieldName="TipoDocumentoConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="63">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Número Doc. Cônjuge" FieldName="NumeroDocumentoConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="64">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Órgão Expedidor Doc. Cônjuge" FieldName="OrgaoExpedidorDocumentoConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="65">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Nome do Pai Cônjuge" FieldName="NomePaiConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="66">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Nome da Mãe Cônjuge" FieldName="NomeMaeConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="67">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Estado Civil Cônjuge" FieldName="IndicaEstadoCivilConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="68">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Certidão Estado Civil Cônjuge" FieldName="CertidaoEstadoCivilConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="69">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Livro Estado Civil Cônjuge" FieldName="LivroCertidaoEstadoCivilConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="70">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Folha Estado Civil Cônjuge" FieldName="FolhaCertidaoEstadoCivilConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="71">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Emissão Doc. Estado Civil Cônjuge" FieldName="EmissaoCertidaoEstadoCivilConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="72">
                                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Cartório Estado Civil Cônjuge" FieldName="NomeCartorioCertidaoEstadoCivilConjuge"
                                                    ShowInCustomizationForm="True" VisibleIndex="73">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Proprietário?" FieldName="Proprietario" ShowInCustomizationForm="True"
                                                    VisibleIndex="74">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Ocupante?" FieldName="Ocupante" ShowInCustomizationForm="True"
                                                    VisibleIndex="75">
                                                    <Settings AllowGroup="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True" HorizontalScrollBarMode="Visible"
                                                ShowFilterRow="True" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                                        </dxwgv:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                    </dxtc:ASPxPageControl>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
