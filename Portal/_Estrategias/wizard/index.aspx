<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="index.aspx.cs"
    Inherits="_Estrategias_wizard_index" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: <%=alturaTabela %>;">
        <tr>
            <td style="width: 192px" valign="top">
                <iframe id="mapa_menu" src="opcoes.aspx" width="100%" height="<%=alturaTabela %>"
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
