<%@ Page Language="C#" AutoEventWireup="true" CodeFile="avisoDesconexao.aspx.cs"  MasterPageFile="~/novaCdis.master"  Inherits="avisoDesconexao" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><asp:Literal runat="server" Text="Desconexão Automática" /></title>
    <script type="text/javascript" src="./scripts/CDIS.js" language="javascript"></script>
    <meta name="viewport" content="width=device-width, user-scalable=no">

    
</head>--%>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
        <div class="box-erro">
            <span><asp:Literal id="objetoMensagemErroInatividade" runat="server" Text="Você foi desconectado automaticamente."> </asp:Literal> <a href="<%=caminho %>" target="_top"><asp:Literal runat="server" Text="<%$ Resources:traducao, erroInatividade_clique_aqui%>"></asp:Literal></a></span>
        </div>
</asp:Content>
