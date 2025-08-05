<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_arquivos.aspx.cs" Inherits="administracao_adm_arquivos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <dxe:ASPxLabel ID="lblAviso" runat="server" Text="ASPxLabel" Visible="False" Style="margin: 20px;"
        Font-Bold="True" Font-Size="Larger">
    </dxe:ASPxLabel>
    <dxe:ASPxRadioButton ID="rgCronograma" runat="server" Checked="True" 
        ClientInstanceName="rgCronograma" GroupName="Arquivo" Text="Cronograma" 
        AutoPostBack="True">
    </dxe:ASPxRadioButton>
    <dxe:ASPxRadioButton ID="rgTemporarios" runat="server" GroupName="Arquivo" 
        ClientInstanceName="rgTemporarios" Text="Arquivos temporários" 
        AutoPostBack="True">
    </dxe:ASPxRadioButton>
    <asp:Panel ID="pnControles" runat="server">
        <table style="margin: 10px">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="CodigoCronogramaProjeto">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                                <dxe:ASPxTextBox ID="txtCodigoCronograma" runat="server" Width="300px" ClientInstanceName="txtCodigoCronograma">
                                    <ValidationSettings CausesValidation="True">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dxe:ASPxTextBox>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnProcurar" runat="server" Text="Procurar" ClientInstanceName="btnProcurar"
                                    OnClick="btnProcurar_Click">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lblQtdeArquivos" runat="server" Text="ASPxLabel" style="float:left;padding-right:100px;">
                    </dxe:ASPxLabel>
                    <dxe:ASPxButton ID="btnExcluir" runat="server" 
                        Text="Excluir arquivos selecionados" ClientInstanceName="btnExcluir" 
                        onclick="btnExcluir_Click" Width="200px">
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="gv_arquivos" runat="server" AutoGenerateColumns="False" 
                        oncustombuttoncallback="gv_arquivos_CustomButtonCallback">
                        <Columns>
                        
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" 
                                VisibleIndex="0">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhaProjeto">
                                        <Image Url="~/imagens/botoes/btnGantt.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn UnboundType="String" VisibleIndex="1">
                                <DataItemTemplate>
                                    <%# "<table><tr><td><a href='adm_arquivos.aspx?arq=" + Eval("Nome") + "'><img alt='Resumo do projeto' src='../imagens/anexo/download.png'  style='border:none;'/></a></td></tr></table>"%>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Arquivo" FieldName="Nome" 
                                VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tamanho" FieldName="Tamanho" 
                                VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tipo" FieldName="Tipo" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Modificado" FieldName="Modificado" 
                                VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager PageSize="30">
                        </SettingsPager>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
