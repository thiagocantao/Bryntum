<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frame_EditorDashboard.aspx.cs" Inherits="Relatorios_GeradorDashboard_frame_EditorDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var comando = '';
        function onBeforeRender(sender) {
            var control = sender.GetDashboardControl();
            var customMenuItem = new DevExpress.Dashboard.Designer.DashboardMenuItem('customItem', 'Sair', 0, 20, function sairDashboard() {/*location.href =*/window.open('CadastroDashboard.aspx','_parent');/*callbackVoltar.PerformCallback();*/ });
            control.findExtension('toolbox').addMenuItem(customMenuItem);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
 <div style="position: absolute; left: 80px; right: 0; top: 0; bottom: 30px;display:none">
        <table cellpadding="0" cellspacing="0" class="dxeBinImgCPnlSys">
            <tr>
                
                <td style="width: 100px">
                    <dxcp:ASPxButton ID="btnAlternar" ClientInstanceName="btnAlternar" runat="server" Text="Designer" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
	            var workingMode = dashboardDesigner.GetWorkingMode();
	            if (workingMode == 'designer') {
		            dashboardDesigner.SwitchToViewer();
		            s.SetText('Designer');
                }
                else {
		            dashboardDesigner.SwitchToDesigner();
		            s.SetText('View');
                }
            }"
                            Init="function(s, e) {
	var workingMode = dashboardDesigner.GetWorkingMode();
	            if (workingMode == 'designer') {
		            dashboardDesigner.SwitchToViewer();
		            s.SetText('Designer');
                }
                else {
		            dashboardDesigner.SwitchToDesigner();
		            s.SetText('View');
                }
}"/>
                    </dxcp:ASPxButton>
                </td>
                <td style="padding-left: 5px">
                    <dx:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/botoes/voltarparalista.png" ShowLoadingImage="True"  ToolTip="Voltar para o cadastro de dashboard" Cursor="pointer">
                        <ClientSideEvents Click="function(s, e) {
//window.location = &quot;CadastroDashboard.aspx&quot;; 
                            callbackVoltar.PerformCallback();
                            
}" />
                    </dx:ASPxImage>

                </td>
            </tr>
        </table>
    </div>
    <div style="position: absolute; left: 0; right: 0; top: 0px; bottom: 0;">
        <dx:ASPxDashboard ID="dashboardDesigner"
            ClientInstanceName="dashboardDesigner"
            runat="server"
            OnConfigureDataConnection="ASPxDashboard1_ConfigureDataConnection"
            AllowCreateNewDashboard="False"
            AllowOpenDashboard="False"
            WorkingMode="Viewer"
            AllowExecutingCustomSql="True" DashboardId="" DashboardState="" DisableHttpHandlerValidation="False" Height="850px" Width="100%">
            <ClientSideEvents BeforeRender="onBeforeRender"></ClientSideEvents>

            <BackendOptions Uri=""></BackendOptions>
        </dx:ASPxDashboard>
    </div>
    </form>
</body>
</html>
