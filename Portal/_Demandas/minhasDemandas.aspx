<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="minhasDemandas.aspx.cs" Inherits="_Demandas_minhasDemandas" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" EnableViewState="false"
    runat="Server">
    <div enableviewstate="false">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);">
            <tr style="height: 26px">
                <td valign="middle" style="padding-left: 10px">
                    <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True" 
                        Font-Overline="False" Font-Strikeout="False" Text="Minhas Demandas"
                        EnableViewState="False"></asp:Label>
                </td>
                <td align="left" valign="middle">
                </td>
            </tr>
        </table>
        <!-- Painel arrendondado lista de projetos -->
    </div>
    <table cellpadding="0" cellspacing="0" enableviewstate="false">
        <tr>
            <td align="right" style="width: 5px; height: 10px">
            </td>
            <td align="right" style="height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
                <dxwgv:ASPxGridView ID="gvDemandas" runat="server" 
                    Width="100%" AutoGenerateColumns="False" ClientInstanceName="gvDemandas" KeyFieldName="CodigoDemanda"
                    OnAutoFilterCellEditorInitialize="gvDemandas_AutoFilterCellEditorInitialize">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="100px">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton Text="Visualizar Fluxo Graficamente" ID="btnFluxo">
                                    <Image ToolTip="Visualizar Fluxo Graficamente" Url="~/imagens/botoes/fluxos.PNG">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton Text="Excluir" ID="btnExcluir">
                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton Text="Visualizar Detalhes" ID="btnDetalhes">
                                    <Image ToolTip="Visualizar Detalhes" Url="~/imagens/botoes/pFormulario.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <HeaderTemplate>
                                <%# string.Format(@"<table><tr><td align=""left"">{0}</td></tr></table>", (podeIncluirDemandas) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Incluir Novo Registro"" onclick=""incluiNovaDemanda()"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Incluir Novo Registro"" style=""cursor: default;""/>")%>
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Assunto" FieldName="Assunto" VisibleIndex="2">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="T&#237;tulo" FieldName="Titulo" VisibleIndex="3"
                            Width="250px">
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                            <FilterCellStyle >
                            </FilterCellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="4"
                            Width="150px">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo" FieldName="Tipo" VisibleIndex="5" Width="100px">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Urg&#234;ncia" FieldName="Urgencia" VisibleIndex="6"
                            Width="80px">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Previs&#227;o" FieldName="Previsao" VisibleIndex="7"
                            Width="75px">
                            <Settings AllowAutoFilter="False" />
                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Styles>
                        <Header >
                        </Header>
                        <Row >
                        </Row>
                        <FilterRow >
                        </FilterRow>
                        <Cell >
                        </Cell>
                        <HeaderPanel >
                        </HeaderPanel>
                        <FilterCell >
                        </FilterCell>
                        <FilterBar >
                        </FilterBar>
                        <FilterRowMenu >
                        </FilterRowMenu>
                        <FilterRowMenuItem >
                        </FilterRowMenuItem>
                    </Styles>
                    <StylesPopup>
                        <FilterBuilder>
                            <Header >
                            </Header>
                        </FilterBuilder>
                    </StylesPopup>
                    <StylesFilterControl>
                        <Table >
                        </Table>
                        <Value >
                        </Value>
                    </StylesFilterControl>
                    <ClientSideEvents CustomButtonClick="function(s, e) {
	if(e.buttonID == 'btnDetalhes')
		s.GetRowValues(e.visibleIndex,'CodigoDemanda;', detalhesDemanda);
	
}" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
