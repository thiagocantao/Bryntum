<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="SemAcesso.aspx.cs"
    Inherits="_Projetos_DadosProjeto_indexResumoProjeto" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" 
                                ClientInstanceName="lblTituloTela" Font-Bold="True"
                                 Text="Acesso Restrito">
                            </dxe:ASPxLabel>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <center>
                    <dxe:ASPxLabel ID="lblMensagem" runat="server" ClientInstanceName="lblMensagem" Font-Bold="True" Font-Italic="True"></dxe:ASPxLabel>
                </center>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <center>
                    <span style="color: #cc0000; font-size: 28px;"><strong><%= Resources.traducao.acesso_n_o_autorizado_ %></strong></span>
                </center>
            </td>
        </tr>
    </table>
</asp:Content>
