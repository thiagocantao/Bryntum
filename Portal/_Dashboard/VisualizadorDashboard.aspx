<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizadorDashboard.aspx.cs" Inherits="_Dashboard_VisualizadorDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .dx-texteditor-input {
            padding: 2px 4px 4px;
            
        }

        .dx-dashboard-combobox-filter-item {
            padding: 3px;
            
        }

        .dx-dashboard-item-header-text {
            padding: 0 0 0 10px;
            
        }

        .dx-dashboard-item-header {
            height: auto;
            
        }

        tr > td > span > a {
            text-decoration:none !important;
            color:#232323 !important;
           
        }
    </style>
    <script type="text/javascript" language="javascript" src="../scripts/jquery.ultima.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript">
        function ItemWidgetCreatedHandle(s, e) {
            var grid = e.GetWidget();
            if (grid.element)
                grid.element()[0].style.fontSize = "8pt";
        }
        $(document).ready(function () {
            $("td.dx-dashboard-splitter-pane>div[data-layout-item-name]").css('height', 'auto');
            $('.dx-dashboard-item.dx-dashboard-combobox-filter-item').css('height', 'auto');
            $('.dx-dashboard-splitter-layout-place').css('height', 'auto');
        });
    </script>
</head>
<body style="margin: 0;">
    <form id="form1" runat="server">
        <div>
            <dx:ASPxDashboard ID="dashboardViewer" runat="server"
                OnConfigureDataConnection="dashboardViewer_ConfigureDataConnection1"
                AllowExecutingCustomSql="True" WorkingMode="ViewerOnly"
                AllowExportDashboardItems="True" ClientInstanceName="dashboardViewer" Width="100%" Height="600px"  OnCustomParameters="dashboardViewer_CustomParameters" OnDashboardLoading="dashboardViewer_DashboardLoading1">
                <ClientSideEvents Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);
	s.SetHeight(height - 25);
}" ItemWidgetCreated="ItemWidgetCreatedHandle" />
            </dx:ASPxDashboard>
        </div>
    </form>
</body>
</html>
