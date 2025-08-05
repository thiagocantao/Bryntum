<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" CodeFile="EdicaoEAP.aspx.cs" Inherits="_Projetos_DadosProjeto_EdicaoEAP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
<base target="_self" />
<script language="javascript" type="text/javascript">
      

    function mudaTituloTela()
    {
        document.title = "EAP - " + window.top.myObject.param1; // + window.dialogArguments.param2; // + " (VISUALIZAÇÃO)";
    }       
</script>
    <script language="javascript" type="text/javascript" src="../../scripts/EdicaoEAP.js?v1"></script>
</head>
<%--onbeforeunload = "trataSaidaTela();" --%>
<body  onload = "trataEntradaTela(); mudaTituloTela();" >
    <form id="form1" runat="server">
    
<div id="divObjet">
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"
    id="WBSComponent" width="100%" height="<%=alturaObject %>"
    codebase="http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab">
    <param name="movie" value="../../flashs/WBSComponent.swf" />
    <param name="quality" value="high" />
    <param name="bgcolor" value="#ffffff" />
    <param name="allowScriptAccess" value="sameDomain" />
    <param name="FlashVars" value="projectID=<%= codigoEAP %>&doubleClickURL=<%=Server.UrlEncode(doubleClipTarea)%>&WBSComponentServiceWSDL=<%=webServicePath%>"/>
    <param name="wmode" value="transparent" />
    <param name="width" value="<%=largoObject %>" />
    <param name="height" value="<%=alturaObjectFlash %>" />
    <embed src="../../flashs/WBSComponent.swf" 
        wmode = "transparent"
        quality="high" 
        bgcolor="#ffffff" 
        width="<%=largoObject %>" 
        height="<%=alturaObjectFlash %>" 
        name="WBSComponent" 
        align="middle" 
        swLiveConnect="true"
        FlashVars="projectID=<%= codigoEAP %>&doubleClickURL=<%=Server.UrlEncode(doubleClipTarea)%>&WBSComponentServiceWSDL=<%=webServicePath%>"
	    play="true" 
	    loop="false" 
	    quality="high" 
	    allowScriptAccess="sameDomain" 
        type="application/x-shockwave-flash" 
        pluginspage="http://www.adobe.com/go/getflashplayer">
    </embed>
    </object>
</div>
                       
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr><td style="padding-left: 10px; height: 30px; margin-bottom: 10px; padding-right: 10px;" valign="middle">
            <dxe:aspxlabel id="lblModoVisualizacao" runat="server" clientinstancename="lblModoVisualizacao"
                           font-bold="True"  forecolor="#C0C000" 
                           backcolor="Olive" width="100%"></dxe:aspxlabel>
        </td></tr>
    </table>
    <dxcb:aspxcallback id="callbackGeral" runat="server" clientinstancename="callbackGeral"
                       oncallback="callbackGeral_Callback"></dxcb:aspxcallback>

    <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral"></dxhf:aspxhiddenfield>


    </form>
    <script language="javascript" type="text/javascript">
        try
        {
            window.top.retornoModal = hfGeral.Get("codigoEAP");
        }catch(e)
        {
        }
    </script>
</body>

</html>
