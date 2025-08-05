<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="historicoProcesso.aspx.cs" Inherits="_Portfolios_historicoProcesso" Title="Portal da Estratégia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <iframe id="frameInterno" style="border: none;" scrolling="auto" src="historicoProcessoInterno.aspx<%=parametrosURL %>"
        width="100%" height="<%=alturaTela %>"></iframe>
</asp:Content>

