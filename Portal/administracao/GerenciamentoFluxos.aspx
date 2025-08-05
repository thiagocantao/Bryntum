<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="GerenciamentoFluxos.aspx.cs" Inherits="administracao_GerenciamentoFluxos" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            height: 10px;
        }
        .auto-style3 {
            height: 10px;
            width: 10px;
        }
        .auto-style4 {
            width: 10px;
        }
        .auto-style5 {
            width: 50px;
        }
        .auto-style6 {
            width: 361px;
        }
        .auto-style7 {
            width: 56px;
        }
        .auto-style8 {
            width: 90px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px"><span id="Span2" runat="server"></span></td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <table cellspacing="0" class="auto-style1">
                                <tr>
                                    <td>
                            <dxe:ASPxLabel id="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Gerenciamento de Fluxos">
                            </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" class="auto-style5">
                                        <dxcp:ASPxLabel ID="ASPxLabel3" runat="server"  Text="Fluxo:">
                                        </dxcp:ASPxLabel>
                                    </td>
                                    <td class="auto-style6">
                                        <dxcp:ASPxComboBox ID="ddlFluxo" runat="server" ClientInstanceName="ddlFluxo"  Width="100%">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlVersao.PerformCallback();
}" />
                                        </dxcp:ASPxComboBox>
                                    </td>
                                    <td align="right" class="auto-style7">
                                        <dxcp:ASPxLabel ID="ASPxLabel4" runat="server"  Text="Versão:">
                                        </dxcp:ASPxLabel>
                                    </td>
                                    <td class="auto-style8" style="padding-right: 10px">
                                        <dxcp:ASPxComboBox ID="ddlVersao" runat="server" ClientInstanceName="ddlVersao"  ValueType="System.String" Width="100%">
                                        </dxcp:ASPxComboBox>
                                    </td>
                                    <td class="auto-style8" style="padding-right: 10px">
                                        <dxcp:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False"  Text="Selecionar" Width="100%">
                                            <ClientSideEvents Click="function(s, e) {
	gvEtapas.SetFocusedRowIndex(-1);
gvConectores.SetFocusedRowIndex(-1);
	codigoWf = ddlVersao.GetValue();
	gvEtapas.PerformCallback();
	gvConectores.PerformCallback();
}" />
                                            <Paddings Padding="0px" />
                                        </dxcp:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="0" class="auto-style1">
        <tr>
            <td class="auto-style3"></td>
            <td class="auto-style2"></td>
            <td class="auto-style3"></td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>

 <dxcp:ASPxGridView runat="server" ClientInstanceName="gvEtapas" KeyFieldName="id" AutoGenerateColumns="False" Width="100%"  ID="gvEtapas">
<ClientSideEvents CustomButtonClick="function(s, e) 
{
    abreEdicaoEtapas(s, e);
}
" FocusedRowChanged="function(s, e) {
	 gvConectores.PerformCallback();
}"></ClientSideEvents>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings ShowFilterRow="True" VerticalScrollBarMode="Visible"></Settings>

<SettingsBehavior AllowFocusedRow="True" AllowSort="False" ConfirmDelete="True"></SettingsBehavior>
     <SettingsText ConfirmDelete="Deseja excluir a etapa?" />
<Columns>
<dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="40px" VisibleIndex="0" Caption=" "><CustomButtons>
<dxcp:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxcp:GridViewCommandColumnCustomButton>
</CustomButtons>
</dxcp:GridViewCommandColumn>
<dxcp:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" Name="Name" Caption="Nome Abreviado" VisibleIndex="2">
<Settings AutoFilterCondition="Contains"></Settings>
</dxcp:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Descrição Resumida" FieldName="toolText" VisibleIndex="3">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Prazo Previsto" FieldName="prazoPrevisto" VisibleIndex="4" Width="95px">
        <Settings AllowAutoFilter="False" />
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Acesso Tipo &quot;Ação&quot;" FieldName="nomeGrupoAcessoAcao" VisibleIndex="5" Width="220px">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Acesso Tipo &quot;Consulta&quot;" FieldName="nomeGrupoAcessoConsulta" VisibleIndex="6" Width="220px">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="ID" FieldName="id" VisibleIndex="1" Width="40px">
        <Settings AllowAutoFilter="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
</Columns>
</dxcp:ASPxGridView>

            </td>
            <td class="auto-style4">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3"></td>
            <td class="auto-style2"></td>
            <td class="auto-style3"></td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>

 <dxcp:ASPxGridView runat="server" ClientInstanceName="gvConectores" KeyFieldName="idEtapa;idEtapaDestino" AutoGenerateColumns="False" Width="100%"  ID="gvConectores" OnHtmlDataCellPrepared="gvConectores_HtmlDataCellPrepared">
<ClientSideEvents CustomButtonClick="function(s, e) 
{
     abreEdicaoConectores(s, e);
}
"></ClientSideEvents>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings ShowFilterRow="True" VerticalScrollBarMode="Visible"></Settings>

<SettingsBehavior AllowFocusedRow="True" AllowSort="False"></SettingsBehavior>
<Columns>
<dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="40px" VisibleIndex="0" Caption=" "><CustomButtons>
<dxcp:GridViewCommandColumnCustomButton ID="btnEditar0" Text="Editar">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxcp:GridViewCommandColumnCustomButton>
</CustomButtons>
</dxcp:GridViewCommandColumn>
<dxcp:GridViewDataTextColumn FieldName="textoBotao" ShowInCustomizationForm="True" Caption="Botão de Ação " VisibleIndex="2">
<Settings AutoFilterCondition="Contains"></Settings>
</dxcp:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Mensagem Tipo &quot;Ação&quot;" FieldName="textoNotificacaoAcao" VisibleIndex="3">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Notificados Tipo &quot;Ação&quot;" FieldName="nomeGrupoNotificadoAcao" VisibleIndex="4">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Mensagem Tipo &quot;Acompanhamento&quot;" FieldName="textoNotificacaoAcompanhamento" VisibleIndex="5">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Notificados Tipo &quot;Acompanhamento&quot;" FieldName="nomeGrupoNotificadoAcompanhamento" VisibleIndex="6">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Conector" FieldName="idEtapa" VisibleIndex="1" Width="140px">
        <Settings AllowAutoFilter="False" FilterMode="DisplayText" />
        <DataItemTemplate>
            <%# getEtapasConector() %>
        </DataItemTemplate>
        <CellStyle HorizontalAlign="Left">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
</Columns>
</dxcp:ASPxGridView>

            </td>
            <td class="auto-style4">&nbsp;</td>
        </tr>
    </table>
</asp:Content>

