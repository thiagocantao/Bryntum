<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="wfRenderizaExterno.aspx.cs" Inherits="listaObras" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
<iframe frameborder="0" id="frameListaObras" style="border: none;" scrolling="auto" src="wfRenderizaFormulario.aspx<%=parametrosURL %>"
        width="100%" height="<%=alturaTela %>"></iframe>
</asp:Content>



