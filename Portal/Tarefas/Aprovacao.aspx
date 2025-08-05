<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="Aprovacao.aspx.cs" Inherits="Tarefas_Aprovacao" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                    Text="Aprovação de Tarefas">
                </dxe:ASPxLabel>
            </td>
            <td valign="middle" style="padding-left: 10px" align="right">
                            <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server" 
                                ClientInstanceName="txtPesquisa"  
                                NullText="Pesquisar por palavra chave..." Width="400px">
                                <ClientSideEvents ButtonClick="function(s, e) {
	callback.PerformCallback(s.GetText());
}" />
                                <Buttons>
                                    <dxe:EditButton>
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Search" 
                                                HottrackedCssClass="Sprite_SearchHover" PressedCssClass="Sprite_SearchHover" />
                                        </Image>
                                    </dxe:EditButton>
                                </Buttons>
                                <ButtonStyle CssClass="MailMenuSearchBoxButton" />
                            </dxe:ASPxButtonEdit>
            </td>
        </tr>
    </table>
     <iframe ID="frmAprovacao" frameborder="0" height="<%=alturaTabela %>" name="frmAprovacao" scrolling="no" 
                    src="FrameAprovacao.aspx" width="100%"></iframe> 
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" 
        oncallback="callback_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	document.getElementById('frmAprovacao').src = 'FrameAprovacao.aspx?PalavraChave=' + s.cp_Param;
}" />
    </dxcb:ASPxCallback>
</asp:Content>

