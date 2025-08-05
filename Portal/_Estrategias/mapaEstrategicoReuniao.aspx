<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mapaEstrategicoReuniao.aspx.cs"
    Inherits="_Estrategias_mapaEstrategicoReuniao" Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <title>Untitled Page</title>   
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
            </td>
            <td style="width: 2px; height: 2px">
            </td>
            <td valign="top">
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblMapa" runat="server" 
                    Text="Mapa:">
                </dxe:ASPxLabel>
            </td>
            <td style="width: 2px">
            </td>
            <td valign="top">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="width: 2px; height: 5px">
            </td>
            <td valign="top">
            </td>
        </tr>
        <tr>
            <td>
            <div style="height: <%= alturaDivMapa %>px;width:97%;overflow:auto;">
            <div id="divObjet">
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" id="WBSComponent" width="974px" height="825px" codebase="http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab">
    <param name="allowScriptAccess" value="sameDomain" />
    <param name="wmode" value="transparent" />
    <param name="movie" value="../flashs/mapaEstrategicoView.swf?caminhoWs=<%=webServicePath%>&codMapa=<%=codigoMapa %>&codEntidade=<%=codigoEntidadeMapa %>&codUsuario=<%=codigoUsuario %>" />
    <param name="quality" value="high" />
    <param name="bgcolor" value="#fffff4" />
    <embed src="../flashs/mapaEstrategicoView.swf?caminhoWs=<%=webServicePath%>&codMapa=<%=codigoMapa %>&codEntidade=<%=codigoEntidadeMapa %>&codUsuario=<%=codigoUsuario %>"
        wmode = "transparent"
        quality="high" 
        bgcolor="#fffff4" 
        width="100%" height="98%" 
        name="mapaEstrategicoView" 
        align="middle" 
	    play="true" 
	    loop="false" 
        type="application/x-shockwave-flash" 
        pluginspage="http://www.adobe.com/go/getflashplayer">
    </embed>
    </object>
</div>
            </div>
            </td>
            <td style="width: 2px">
            </td>
            <td valign="top">
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblEntidadeDiferente" runat="server" ClientInstanceName="lblEntidadeDiferente"
                    Font-Bold="True" Font-Italic="True" ForeColor="Red">
                </dxe:ASPxLabel>
            </td>
            <td style="width: 2px">
            </td>
            <td valign="top">
            </td>
        </tr>
    </table>
</div>
</form>
</body>
</html>
