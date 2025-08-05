<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_arquivos_visaocronogramaxml.aspx.cs" Inherits="administracao_adm_arquivos_visaocronogramaxml" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <asp:Panel ID="Panel1" runat="server" Width="100%">
        <dxe:ASPxLabel ID="lblNomeProjeto" runat="server" Text="">
        </dxe:ASPxLabel>
        <br />
        <table>
            <tbody>
                <tr>
                    <td style="padding-right: 10px">
                        <dxe:ASPxComboBox ID="cbListaCronogramas" runat="server" ValueType="System.String"
                            Width="200px" DropDownRows="20">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnLerCronograma" runat="server" Text="Ler" Width="40px" OnClick="btnLerCronograma_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <dxtc:ASPxPageControl ID="pcCronograma" runat="server" ActiveTabIndex="1" ClientInstanceName="pcCronograma"
        Width="98%">
        <TabPages>
            <dxtc:TabPage Name="tpTarefas" Text="Tarefas">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dxwtl:ASPxTreeList ID="tlTarefas" runat="server" Height="500px" Width="100%">
                        </dxwtl:ASPxTreeList>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Name="tpInfo" Text="Informações">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                        <dxe:ASPxComboBox ID="cbInformacoes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbInformacoes_SelectedIndexChanged"
                            Width="200px" DropDownRows="20">
                        </dxe:ASPxComboBox>
                        <dxwgv:ASPxGridView ID="gvInformacoes" runat="server">
                            <SettingsPager PageSize="100">
                            </SettingsPager>
                        </dxwgv:ASPxGridView>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
        </TabPages>
    </dxtc:ASPxPageControl>
</asp:Content>
