<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="InvolucroRelatorio.aspx.cs" Inherits="Relatorios_InvolucroRelatorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../scripts/jquery.ultima.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var height = Math.max(0, document.documentElement.clientHeight);
            height = height - 100;
            $('#frame').height(height);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <iframe id="frame" src="ListaRelatorios.aspx" style="border: none; width: 100%;"></iframe>
</asp:Content>

