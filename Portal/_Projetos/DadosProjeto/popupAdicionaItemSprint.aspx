<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupAdicionaItemSprint.aspx.cs" Inherits="_Projetos_DadosProjeto_popupAdicionaItemSprint
    " %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2 {
            width: 87%;
            margin-left:20px;
            margin-right:auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="../../imagens/BRISK.jpg" class="style2" />
        </div>
        <div style="padding-left:22px">
            <dxcp:ASPxButton ID="btn" runat="server" ClientInstanceName="btn" EnableClientSideAPI="True" Text="Ok, Ciente" Width="100px" align="center">
                <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
            </dxcp:ASPxButton>
        </div>

    </form>
</body>
</html>
