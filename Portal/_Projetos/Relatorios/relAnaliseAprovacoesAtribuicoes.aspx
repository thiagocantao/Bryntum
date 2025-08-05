<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseAprovacoesAtribuicoes.aspx.cs" Inherits="_Projetos_Relatorios_relAnaliseAprovacoesAtribuicoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table id="tabelaFiltros" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td align="right">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url('../../imagens/titulo/back_Titulo.gif');
        width: 100%">
        <tr>
            <td align="left" style="background-image: url('../../imagens/titulo/back_Titulo_Desktop.gif');
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel id="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Análise de Atribuições e Aprovações" 
                                ClientInstanceName="lblTituloTela"></dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                <table cellpadding="0" cellspacing="0" style="width: 99%">
                    <tr>
                        <td align="left" valign="middle">
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                    </dxhf:ASPxHiddenField>
                                                    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
                                                        GridViewID="gvDados" 
                                        onrenderbrick="ASPxGridViewExporter1_RenderBrick">
                                                        <Styles>
                                                            <Header  Wrap="True">
                                                            </Header>
                                                            <Cell >
                                                            </Cell>
                                                        </Styles>
                                                    </dxwgv:ASPxGridViewExporter>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                    <table cellspacing="0" cellpadding="0" border="0" style="paddi">
                        <tbody>
                            <tr>
                                <td style="height: 5px;" width="100%">
                                </td>
                                <td style="width: 116px; height: 5px;">
                                </td>
                                <td align="right">
                                </td>
                            </tr>
                            <tr>
                                <td width="100%">
                                </td>
                                <td style="width: 116px">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="right">
                                                    <dxe:ASPxComboBox runat="server" Width="70px" ClientInstanceName="ddlExporta"
                                                        ID="ddlExporta">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}"></ClientSideEvents>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td style="padding-right: 3px; padding-left: 3px">
                                                    <dxcp:ASPxCallbackPanel runat="server"  
                                                        ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
                                                        <PanelCollection>
                                                            <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
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
                                <td align="right">
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
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 10px">
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="NomeRecurso"
                        AutoGenerateColumns="False" EnableRowsCache="False" Width="99%"
                        ID="gvDados" EnableViewState="False">
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="130px"
                                VisibleIndex="0" Visible="False">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Mostrar Detalhes">
                                        <Image Url="~/imagens/botoes/pFormulario.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto"
                                Caption="Projeto" VisibleIndex="1" Width="250px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Status do Projeto" VisibleIndex="2" 
                                FieldName="StatusProjeto" Width="100px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Recurso" FieldName="NomeRecurso" 
                                VisibleIndex="4" Width="180px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Aprovador" 
                                FieldName="ResponsavelAprovacao" VisibleIndex="5" Width="180px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Status da Atribuição" 
                                FieldName="StatusAtribuicao" VisibleIndex="6" Width="100px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Solicitação" FieldName="DataSolicitacao" 
                                VisibleIndex="7" Width="105px">
                                <PropertiesDateEdit DisplayFormatString="">
                                </PropertiesDateEdit>
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Aprovação" FieldName="DataAprovacao" 
                                VisibleIndex="8" Width="105px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Mês Solicitação" 
                                FieldName="mesSolicitacao" VisibleIndex="9" Width="100px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ano Solicitação" 
                                FieldName="anoSolicitacao" VisibleIndex="10" Width="95px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Mês Aprovação" FieldName="mesAprovacao" 
                                VisibleIndex="11" Width="100px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ano Aprovação" FieldName="anoAprovacao" 
                                VisibleIndex="12" Width="95px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tarefa" FieldName="NomeTarefa" 
                                VisibleIndex="3" Width="200px">
                                <Settings AllowAutoFilter="True" AllowGroup="True" />
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                        <SettingsPager PageSize="100" Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True" 
                            ShowFilterRow="True" HorizontalScrollBarMode="Visible"></Settings>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
