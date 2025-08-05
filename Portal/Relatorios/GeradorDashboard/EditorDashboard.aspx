<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="EditorDashboard.aspx.cs" Inherits="Relatorios_GeradorDashboard_EditorDashboard" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="AreaTrabalho" runat="server">
    <iframe runat="server" ID="frameEditorDashboard" src="frame_EditorDashboard.aspx" style="border:none; width:100%"></iframe>
</asp:Content>

