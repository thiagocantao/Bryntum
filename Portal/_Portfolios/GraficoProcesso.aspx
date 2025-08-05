<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="GraficoProcesso.aspx.cs"
    Inherits="_Portfolios_GraficoProcesso" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <iframe frameborder="0" id="frameInterno" style="border: none;" scrolling="auto" src="GraficoProcessoInterno.aspx<%=parametrosURL %>"
        width="100%" height="<%=alturaTela %>"></iframe>
</asp:Content>
