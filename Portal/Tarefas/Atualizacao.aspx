<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="Atualizacao.aspx.cs" Inherits="Tarefas_Aprovacao" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                    Text="Atualização de Tarefas">
                </dxe:ASPxLabel>
            </td>
            <td valign="middle" style="padding-left: 10px" align="right">
                            <table>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxe:ASPxCheckBox ID="ckAtrasadas" runat="server" CheckState="Unchecked" 
                                            ClientInstanceName="ckAtrasadas"  
                                            Text="Somente atrasadas">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
	callback.PerformCallback();
}" Init="function(s, e) {
	var atrasadas = s.GetChecked() ? 'S' : 'N';
	document.getElementById('frmAtualizacao').src = 'FrameAtualizacao.aspx?Atrasadas=' + atrasadas;	
}" />
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td>
                            <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server" 
                                ClientInstanceName="txtPesquisa"  
                                NullText="Pesquisar por palavra chave..." Width="350px">
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
            </td>
        </tr>
    </table>
     <iframe ID="frmAtualizacao" frameborder="0" height="<%=alturaTabela %>" name="frmAtualizacao" scrolling="no" 
                    src="" width="100%"></iframe> 
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" 
        oncallback="callback_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	var atrasadas = ckAtrasadas.GetChecked() ? 'S' : 'N';
	document.getElementById('frmAtualizacao').src = 'FrameAtualizacao.aspx?PalavraChave=' + s.cp_Param + '&amp;Atrasadas=' + atrasadas;	
}" BeginCallback="function(s, e) {
	lpCarregando.Show();
}" />
    </dxcb:ASPxCallback>
    <dxlp:ASPxLoadingPanel ID="lpCarregando" runat="server" 
        ClientInstanceName="lpCarregando" Modal="True">
    </dxlp:ASPxLoadingPanel>
</asp:Content>

