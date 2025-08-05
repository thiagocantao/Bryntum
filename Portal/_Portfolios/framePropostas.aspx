<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="framePropostas.aspx.cs" Inherits="_Portfolios_framePropostas" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloSelecao" runat="server" ClientInstanceName="lblTituloSelecao"
                                Font-Bold="False" >
                            </dxe:ASPxLabel>
                            &nbsp;-
                            <dxe:ASPxLabel ID="lblTituloProposta" runat="server" ClientInstanceName="lblTituloProposta"
                                Font-Bold="False" >
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border ="0" bordercolor = "red" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 180px; border-right: gainsboro 1px solid;" valign="top">
                            <iframe name="menuLateral" src="frameProposta_MenuEsquerdo.aspx" width="180px" scrolling="no"
                                frameborder="0" style="height: <%=alturaTabela %>"></iframe>
                        </td>
                        <td style="width: 5px">&nbsp;
                        </td>
                        <td valign="top">
                            <iframe name="framePrincipal" src="frameProposta_Resumo.aspx" width="100%" scrolling="auto"
                                frameborder="0" style="height: <%=alturaTabela %>"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

