<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameEspacoTrabalho.aspx.cs"
    Inherits="espacoTrabalho_frameEspacoTrabalho" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
                <table border="0" bordercolor="red" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 120px; border-right: gainsboro 1px solid; padding-left: 10px" valign="top">
                            <iframe name="menuLateral" src="frameEspacoTrabalho_MenuEsquerdo.aspx" width="120px"
                                scrolling="no" frameborder="0" style="height: <%=alturaTabela %>"></iframe>
                        </td>
                        <td style="width: 0px">
                        </td>
                        <td valign="top">
                            <iframe name="framePrincipal" src="frameEspacoTrabalho_CaixaEntrada.aspx" width="100%"
                                scrolling="auto" frameborder="0" style="height: <%=alturaTabela %>"></iframe>
                        </td>
                    </tr>
                   
                </table>
</asp:Content>
