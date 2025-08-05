<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="atendimentoDemandas.aspx.cs" Inherits="_Demandas_atendimentoDemandas"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" EnableViewState="false"
    runat="Server">
    <div enableviewstate="false">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);">
            <tr style="height: 26px">
                <td valign="middle" style="padding-left: 10px">
                    <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True" 
                        Font-Overline="False" Font-Strikeout="False" Text="Lista de Demandas"
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
                    Width="100%" AutoGenerateColumns="False" OnAutoFilterCellEditorInitialize="gvDemandas_AutoFilterCellEditorInitialize">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="100px">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton Text="Visualizar Fluxo Graficamente">
                                    <Image ToolTip="Visualizar Fluxo Graficamente" Url="~/imagens/botoes/fluxos.PNG">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton Text="Excluir">
                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton Text="Visualizar Detalhes">
                                    <Image ToolTip="Visualizar Detalhes" Url="~/imagens/botoes/pFormulario.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Demanda" VisibleIndex="1" Width="280px" FieldName="Titulo">
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                            <FilterCellStyle >
                            </FilterCellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Atraso Demanda" VisibleIndex="0" Width="110px"
                            FieldName="AtrasoDemanda">
                            <Settings AllowAutoFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Atraso Etapa" VisibleIndex="2" Width="85px"
                            FieldName="AtrasoEtapa">
                            <Settings AllowAutoFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Demandante" FieldName="Demandante" VisibleIndex="4"
                            Width="200px">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Respons&#225;vel" FieldName="Responsavel"
                            VisibleIndex="4" Width="200px">
                            <Settings AllowAutoFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Assunto" VisibleIndex="3" FieldName="Assunto"
                            Width="220px">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="4" Width="100px" FieldName="Status">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo" VisibleIndex="5" Width="100px" FieldName="Tipo">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Urg&#234;ncia" VisibleIndex="6" Width="85px"
                            FieldName="Urgencia">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Previs&#227;o" VisibleIndex="7" Width="80px"
                            FieldName="Previsao">
                            <Settings AllowAutoFilter="False" />
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
