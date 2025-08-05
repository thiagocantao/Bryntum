<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usuariosLogadosFrm.aspx.cs" Inherits="usuariosLogadosFrm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="scripts/jquery-3.1.1.min.js"></script>
    <%--<script src="scripts/json2.js"></script>--%>
    <script src="scripts/jquery.signalR.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
</head>
<body>
    <script type="text/javascript">

        $(function () {
            // Declare a proxy to reference the hub. 
            //var chatHub = $.connection.chatHub;

            //registerClientMethods(chatHub);

            //// Start Hub
            //$.connection.hub.start().done(function () {

            //    chatHub.server.connect(callbackSession.cp_Usuario, callbackSession.cp_IDUsuario);

            //});
        });


        function registerClientMethods(chatHub) {

            try {
                // Calls when user successfully logged in
                chatHub.client.onConnected = function (id, userName, allUsers, messages) {
                    console.log(id + ' --onConnected-- ' + userName)
                }

                // On New User Connected
                chatHub.client.onNewUserConnected = function (id, name) {
                    console.log(id + ' --onNewUserConnected-- ' + name);

                }
                // On User Disconnected
                chatHub.client.onUserDisconnected = function (id, userName, allUsers) {
                    console.log(id + ' --onUserDisconnected-- ' + userName);
                }
            } catch (e) { }
        }
    </script>
    <form id="form1" runat="server">
        <dxcb:ASPxCallback ID="callbackSession" runat="server"
            ClientInstanceName="callbackSession">
            <ClientSideEvents EndCallback="function(s, e) {
	            starttime = new Date();
                starttime = starttime.getTime();
                countdown();
}" />
        </dxcb:ASPxCallback>
        <div>
        </div>
    </form>
</body>
</html>
