<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="principalIndicador.aspx.cs" Inherits="_Estrategias_indicador_principalIndicador" Title="Portal da Estratégia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table>
        <tr>
            <td align="right"  style="height: 25px;" valign="middle">
                <div id="segundoMenu">
                    <%= segundoMenu%>
                 </div>
            </td>
        </tr>
        <tr>
            <td>
                <iframe id="frameIndicador" style="border: none;" scrolling="auto" src="resumoIndicador.aspx?COIN=<%=codigoIndicador %>" width="100%" height="<%=alturaTela %>"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>

