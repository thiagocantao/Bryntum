<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="listaProcessos.aspx.cs"
    Inherits="_Portfolios_listaProcessos" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle" style="padding-left: 10px">
                                <dxe:ASPxLabel ID="lbTituloTela" runat="server" Font-Bold="True" 
                                                Text="Fluxos de Processos">
                                               </dxe:ASPxLabel>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
    <iframe frameborder="0" id="frameWF" style="border: none;" scrolling="auto" src="listaProcessosInterno.aspx<%=parametrosURL %>"
        width="100%" height="<%=alturaTela %>"></iframe>
</asp:Content>
