<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="forumPortal.aspx.cs"
    Inherits="espacoTrabalho_forumPortal" Title="FORVN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: <%=alturaTabela %>;">
        <tr>
            <td style="width: 205px" valign="top">
                <iframe id="mapa_menu" src="forumMenu.aspx" width="100%" height="<%=alturaTabela %>"
                    scrolling="no" frameborder="0" name="mapa_menu"></iframe>
            </td>
            <td valign="top">
                <iframe id="mapa_desktop" src="<%=telaInicial %>" width="100%"
                    height="<%=alturaTabela %>" scrolling="auto" frameborder="0" name="mapa_desktop">
                </iframe>
            </td>
        </tr>
    </table>
</asp:Content>