<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfEngineInternoMobile.aspx.cs" Inherits="wfEngineInternoMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap-3.3.7/css/bootstrap.min.css" />
    <script type="text/javascript" src="./scripts/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="./scripts/mostraMensagem.js"></script>
    <script type="text/javascript" src="./scripts/alertModal.js"></script>
    <script type="text/javascript" src="./scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="~/Content/reset.css"/>
    <link rel="stylesheet" type="text/css" href="~/Content/mobile/style.css"/>
    <link rel="stylesheet" type="text/css" href="~/Content/mobile/alertModal.css"/>
    <link rel="stylesheet" type="text/css" href="~/Content/mostraMensagem.css"/>
    <link rel="stylesheet" type="text/css" href="~/Content/mobile/wfEngineInternoMobile.css"/>
    <link rel="stylesheet" type="text/css" href="~/Content/FontAwesome/font-awesome.min.css"/>
    <script type="text/javascript" src="./scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        var data = {
            type: "FullScreen",//para identificar o evento na app
            value: true //tamanho do documento
        };
        if (localStorage.getItem("originMsg")) {
            parent.window.postMessage(data, localStorage.getItem("originMsg"));
        }
        
        function displayMessage(evt) {//recebe o evento da app
            //verifica que ação tomar
            //neste caso está pedindo o tamanho do documento para adaptar o tamanho do frame pra fullcreen
            if (evt.data.type == 'origin') {
                var data = {
                    type: "FullScreen",//tipo do evento 
                    value: true //tamanho do documento
                };
                localStorage.setItem("originMsg", evt.origin);
                parent.window.postMessage(data, evt.origin);
            }


        }
        //escuta o evento da app
        if (window.addEventListener) {
            window.removeEventListener("message", displayMessage, false);
            window.addEventListener("message", displayMessage, false);
        }
        else {
            window.attachEvent("onmessage", displayMessage);
        }

        var itemActive = { tab: { index: null, name: null } };

        function CompareTabs (tab1, tab2)
        {
            return (tab1.index === tab2.index);
        }
        function GetActiveTab() {
            return itemActive.tab;
        }

        function SetActiveTab(tab, novoItem) {
            itemActive = novoItem;

            $('a.active').removeClass("active");
            tab.addClass("active");
        }

        function RedimencionaPageControl() {
            if (typeof $('iframe[name=wfFormularios]').contents().find("body")[0] != "undefined") {
                $('iframe[name=wfFormularios]').height($('iframe[name=wfFormularios]').contents().find("body")[0].clientHeight + 20);
            }
            
            
        }

        jQuery(document).ready(function ($) {


            
            $('iframe[name=wfFormularios]').on('load', function () {
                console.log("load");
                RedimencionaPageControl();
                $(this).contents().find("body").bind("DOMSubtreeModified", function () {
                });
                $(this).contents().find("body").find(".cdis-memo").each(function () {
                    $(this).closest('div[class^="form-group"]').css("width", "100%");
                });
                $(this).contents().find("body").find(".cdis-htmleditor").each(function () {
                    $(this).closest('div[class^="form-group"]').css("width", "100%");
                });
                
            });
            var mainHeader = $('.cd-secondary-nav');

            

            $('iframe[name=wfFormularios]').on("miEvt", RedimencionaPageControl());

            mainHeader.on('click', 'a', function (e) {
                var TabDestino = $(this);
                var thisItem = {
                    tab: {
                        index: $("li").index(TabDestino.parent()),
                        name: TabDestino.data("name")
                    }
                };
                var existeAlteracao = false;
                existeAlteracao = window.frames['wfFormularios'].existeConteudoCampoAlterado;

                if (existeAlteracao && mudaAba == false) {
                    var textoMsg = 'As últimas informações que você digitou ainda não foram salvas. </br>Deseja mudar de aba e perder essas informações?';

                    var funcObj = { MudaTab: function ( tab, thisItem) { mudaAba = true; SetActiveTab(tab, thisItem) } }

                    mostraMensagem(textoMsg, 'confirmacao', true, true, function () { funcObj['MudaTab']( TabDestino, thisItem) }, 0 , "info");

                } else {
                    SetActiveTab(TabDestino, thisItem)
                }
                    
                mudaAba = false;
                selecionaFormulario(null, thisItem);
            });
        });



        var frmCriteriosPendente = '';

        function mostraBotoes() {
            frmCriteriosPendente = hfFormCriterio.Get('frmCriteriosPendente');
                if (window.pnBotoes)
                    window.pnBotoes.SetVisible(true);
        }
    </script>
</head>
<body onload='mostraBotoes()' style="margin:0px;">
    <form id="form1" runat="server">
    <dxhf:ASPxHiddenField ID="hfFormCriterio" runat="server" 
        ClientInstanceName="hfFormCriterio">
    </dxhf:ASPxHiddenField>
    
    <dxcp:ASPxCallback ID="callbackParecer" runat="server" 
        ClientInstanceName="callbackParecer" oncallback="callbackParecer_Callback">
        <ClientSideEvents EndCallback="function(s, e) {

	if(s.cp_Msg == '')
	{
		var nomeAcao =  s.cp_NA;
		if (true == podeAvancarFluxo(s.cp_CWF, s.cp_CE, s.cp_CodigoAcao , s.cp_ICD, nomeAcao))
			pnWorkflow.PerformCallback(s.cp_CodigoAcao);
	}
	else
	{
		mostraMensagem(s.cp_Msg, 'Atencao', true, false, null, 0 , 'warning');
	}
}" />
    </dxcp:ASPxCallback>
    
    </form>
</body
</html>
