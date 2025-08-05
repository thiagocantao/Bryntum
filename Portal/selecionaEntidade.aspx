<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="selecionaEntidade.aspx.cs" Inherits="selecionaEntidade" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
        <table>
            <tr style="height: 10px;">
                <td style="width: 20px; height: 10px;">
                    <img src="imagens/spacer.gif" alt="" /></td>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td style="width: 20px;">
                    <img src="imagens/spacer.gif" alt="" /></td>
                <td>
                    <div id="divTelaInicial" style="height: <%=alturaLista %>;overflow: auto">
                        <asp:Panel ID="pnEntidades" runat="server" Width="90%" >
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
</asp:Content>

