<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="VisualizadorDashboardComMaster.aspx.cs" Inherits="_Dashboard_VisualizadorDashboardComMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <iframe id="frameConteudo" runat="server" style="width: 99%; height: 1000px; border: none;"></iframe>

    <script type="text/javascript" language="javascript" src="../scripts/jquery.ultima.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var height = Math.max(0, document.documentElement.clientHeight);
            $('#frameConteudo').height(height - 70);
        });
    </script>
</asp:Content>

