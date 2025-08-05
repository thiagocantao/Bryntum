<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_Biblioteca.aspx.cs"
    Inherits="espacoTrabalho_frameEspacoTrabalho_Biblioteca" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div id="ConteudoPrincipal">
    <iframe frameborder="0" id="frameAnexosBiblioteca" style="border: none;" scrolling="auto" src="frameEspacoTrabalho_BibliotecaInterno.aspx<%=parametrosURL %>"
        width="100%" height="<%=alturaTela %>"></iframe>
    </div>
</asp:Content>

