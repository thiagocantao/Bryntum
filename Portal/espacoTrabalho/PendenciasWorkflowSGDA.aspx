<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="PendenciasWorkflowSGDA.aspx.cs" Inherits="espacoTrabalho_PendenciasWorkflowSGDA" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" 
        style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif); width: 100%">
        <tr style="height:26px">
            <td valign="middle">
                &nbsp; &nbsp;<dxtv:ASPxLabel ID="lblTituloTela" runat="server" 
                    ClientInstanceName="lblTituloTela" Font-Bold="True" 
                    Text="Minhas Pendências de Workflow">
                </dxtv:ASPxLabel>
            </td>
        </tr>
    </table>
    <iframe frameborder="0" id="framePwf" style="border: none;" scrolling="auto" src="<%=url %>"
        width="100%" height="<%=alturaTela %>"></iframe>
    </asp:Content>

