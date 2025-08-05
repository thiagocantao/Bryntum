<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfEngineInterno.aspx.cs" Inherits="wfEngineInterno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="./scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        var frmCriteriosPendente = '';

        function mostraBotoes() {
            frmCriteriosPendente = hfFormCriterio.Get('frmCriteriosPendente');
                if (window.pnBotoes)
                    window.pnBotoes.SetVisible(true);
        }
    </script>
    <link href="estilos/custom.css" rel="stylesheet" />
</head>
<body onload='mostraBotoes()' style="margin:0px; overflow: auto;">
    <form id="form1" runat="server">
    <dxhf:ASPxHiddenField ID="hfFormCriterio" runat="server" 
        ClientInstanceName="hfFormCriterio">
    </dxhf:ASPxHiddenField>
    
    <dxcp:ASPxCallback ID="callbackParecer" runat="server" 
        ClientInstanceName="callbackParecer" oncallback="callbackParecer_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	window.top.lpAguardeMasterPage.Hide();

	if(s.cp_Msg == '')
	{
		var nomeAcao =  s.cp_NA;
		if (true == podeAvancarFluxo(s.cp_CWF, s.cp_CE, s.cp_CodigoAcao , s.cp_ICD, nomeAcao))
			pnWorkflow.PerformCallback(s.cp_CodigoAcao);
	}
	else
	{
		window.top.mostraMensagem(s.cp_Msg, 'Atencao', true, false, null);
	}
}" BeginCallback="function(s, e) {
window.top.lpAguardeMasterPage.Show();	
}" />
    </dxcp:ASPxCallback>
    
    </form>
</body>
</html>
