<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="wfEngine.aspx.cs"
    Inherits="wfEngine" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <iframe frameborder="0" id="frameWF" style="border: none;" scrolling="auto" src="wfEngineInterno.aspx<%=parametrosURL %>"
        width="100%" height="<%=alturaTela %>"></iframe>
</asp:Content>
