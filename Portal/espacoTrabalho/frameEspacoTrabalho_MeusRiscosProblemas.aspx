<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_MeusRiscosProblemas.aspx.cs"
    Inherits="espacoTrabalho_frameEspacoTrabalho_MeusRisgosProblemas" Title="Meus Risgos/Problemas" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <%--TABLE PRINCIPAL --%>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="height:26px">
           <td valign="middle" style="padding-left: 10px">
           <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                        Text="Riscos / Problemas"></dxe:ASPxLabel>
    </td></tr>
    </table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <%--TABLE CORPO--%>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td valign="top">
                <iframe frameborder="0" name="framePrincipal" scrolling="no" src="<%=telaInicial %>"
                                style="height: <%=alturaTabela %>; margin:0" width="100%" marginheight="0" id="frmDadosUsuario">
                </iframe>
                <dxlp:ASPxLoadingPanel ID="lpAtualizando" runat="server" ClientInstanceName="lpAtualizando"
                     Modal="True" Text="Atualizando&hellip;">
                </dxlp:ASPxLoadingPanel>
                <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral">
                </dxhf:aspxhiddenfield>
            </td>
        </tr>
    </table>
</asp:Content>