<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="ListaContratos.aspx.cs" Inherits="_VisaoNE_ListaContratos" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%; height: 28px">
        <tr>
            <td align="left" style="padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False"
                    Text="Lista de Contratos"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 10px; height: 5px">
            </td>
            <td>
            </td>
            <td style="width: 10px; height: 5px">
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="right">
                <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
                    Landscape="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                </dxwgv:ASPxGridViewExporter>
                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                </dxhf:ASPxHiddenField>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxComboBox runat="server" ValueType="System.String" ClientInstanceName="ddlExporta"
                                     ID="ddlExporta">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set('tipoArquivo', s.GetValue());
}"></ClientSideEvents>
                                </dxe:ASPxComboBox>
                            </td>
                            <td style="padding-right: 3px; padding-left: 3px">
                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnImage" Width="23px"
                                    Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao">
                                            </dxe:ASPxImage>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                            <td style="padding-right: 3px; padding-left: 3px; width: 75px">
                                <dxe:ASPxButton runat="server" Text="Exportar" 
                                    ID="Aspxbutton1" OnClick="btnExcel_Click">
                                    <ClientSideEvents Click="function(s, e) 
{
	if(gvDados.pageRowCount == 0)
	{
		window.top.mostraMensagem(&quot;N&#227;o h&#225; Nenhuma informa&#231;&#227;o para exportar.&quot;, 'Atencao', true, false, null);
		e.processOnServer = false;	
	}
}"></ClientSideEvents>
                                    <Paddings Padding="0px"></Paddings>
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td align="right">
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 5px">
            </td>
            <td>
            </td>
            <td style="width: 10px; height: 5px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoContrato"
                    AutoGenerateColumns="False" Width="100%" 
                    ID="gvDados" EnableRowsCache="False" EnableViewState="False" OnAfterPerformCallback="gvDados_AfterPerformCallback"
                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnProcessColumnAutoFilter="gvDados_ProcessColumnAutoFilter">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Munic&#237;pio" FieldName="Municipio" GroupIndex="0"
                            Name="Municipio" SortIndex="0" SortOrder="Ascending" VisibleIndex="0" Width="300px">
                            <Settings AllowGroup="True" AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" GroupIndex="1"
                            SortIndex="1" SortOrder="Ascending" VisibleIndex="1" Width="145px">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Segmento" FieldName="Segmento" VisibleIndex="2"
                            Width="170px">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="Projeto" VisibleIndex="3"
                            Width="230px">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                            <DataItemTemplate>
                                <a href='../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=<%# Eval("CodigoProjeto")%>&NomeProjeto=<%# Eval("Projeto")%>'
                                    style="cursor: pointer">
                                    <%# Eval("Projeto") %>
                                </a>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Vigente Ano" FieldName="VigenteAno" VisibleIndex="4"
                            Width="110px">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Contrato" FieldName="ValorContrato"
                            VisibleIndex="5" Width="155px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="True" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Pago" FieldName="ValorPago" VisibleIndex="6"
                            Width="155px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="True" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Saldo" FieldName="Saldo" VisibleIndex="7"
                            Width="155px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="True" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="% Financeiro" FieldName="PercentualFinanceiro"
                            VisibleIndex="8" Width="95px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}%">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="True" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="% F&#237;sico" FieldName="PercentualFisico"
                            VisibleIndex="9" Width="95px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}%">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="True" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="In&#237;cio" FieldName="Inicio" VisibleIndex="10"
                            Width="120px">
                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:dd/MM/yyyy}"
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="T&#233;rmino" FieldName="Termino" VisibleIndex="11"
                            Width="120px">
                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:dd/MM/yyyy}"
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ano T&#233;rmino" FieldName="AnoTermino" VisibleIndex="12"
                            Width="110px">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Fornecedor" FieldName="Fornecedor" VisibleIndex="13"
                            Width="280px">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo de Obra" FieldName="TipoObra" VisibleIndex="14"
                            Width="160px">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Faixa de Valor" FieldName="FaixaValor" VisibleIndex="15"
                            Width="190px">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AutoExpandAllGroups="True">
                    </SettingsBehavior>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                        HorizontalScrollBarMode="Visible" ShowFooter="True" ShowHeaderFilterBlankItems="False">
                    </Settings>
                    <SettingsText CommandClearFilter="Limpar filtro"></SettingsText>
                    <StylesPopup>
                        <HeaderFilter>
                            <Content >
                            </Content>
                        </HeaderFilter>
                    </StylesPopup>
                </dxwgv:ASPxGridView>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
