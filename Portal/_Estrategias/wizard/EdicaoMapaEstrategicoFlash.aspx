<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdicaoMapaEstrategicoFlash.aspx.cs" Inherits="_Estrategias_wizard_EdicaoMapaEstrategicoFlash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <base target="_self" />
    <script language="javascript" type="text/javascript">
        function fechaJanela() {
            //window.top.retornoModal = hfGeral.Get("codigoEAP");
            //window.top.fechaModal();
        }

        function mudaTituloTela() {
            //var argumentoTela;
            //argumentoTela = window.top.myObject.param1;

            //document.title = "Mapa Estratégico - " + argumentoTela;
            //lblModoVisualizacao.SetText(argumentoTela); // + window.dialogArguments.param2; // + " (VISUALIZAÇÃO)";
        }
    </script>
</head>
<%--onbeforeunload = "trataSaidaTela();" --%>
<body onload="mudaTituloTela();" onunload="fechaJanela();">
    <form id="form1" runat="server">


        <dxcp:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/FimDoAdobeFlashPlayer.png" ShowLoadingImage="true"></dxcp:ASPxImage>

    </form>
</body>
</html>
