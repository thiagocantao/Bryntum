<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfRenderizaFormularioMobile.aspx.cs"
    Inherits="wfRenderizaFormularioMobile" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="./scripts/CDIS.js" language="javascript"></script>
    
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap-3.3.7/css/bootstrap.min.css" />
    <script type="text/javascript" src="./scripts/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="./scripts/mostraMensagem.js"></script>
    <script type="text/javascript" src="./Content/bootstrap-3.3.7/js/bootstrap.min.js" ></script>
    <!--<link rel="stylesheet" type="text/css" href="~/Content/reset.css"/> -->
    <link rel="stylesheet" type="text/css" href="~/Content/mobile/style.css"/>
    <link rel="stylesheet" type="text/css" href="~/Content/mostraMensagem.css"/>
    <link rel="stylesheet" type="text/css" href="~/Content/mobile/wfRenderizaFormularioMobile.css"/>
    <script type="text/javascript">

        var existeConteudoCampoAlterado = false;
        var retornoModal = null;
        var retornoModalTexto = null;
        var retornoModalValor = null;
        var componenteLOV = null;

        function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, objParam) {
            if (parseInt(sHeight) < 535)
                sHeight = parseInt(sHeight) + 20;

            myObject = objParam;
            objFrmModal = document.getElementById('frmModal');

            pcModal.SetWidth(sWidth);
            objFrmModal.style.width = "100%";
            objFrmModal.style.height = sHeight + "px";
            urlModal = sUrl;
            pcModal.SetHeaderText(sHeaderTitulo);
            pcModal.Show();
        }

        function fechaModal() {
            pcModal.Hide();
        }

        function resetaModal() {
            objFrmModal = document.getElementById('frmModal');
            posExecutar = null;
            objFrmModal.src = "";
            pcModal.SetHeaderText("");
            retornoModal = null;
            retornoModalTexto = null;
            retornoModalValor = null;
        }

        function mostrarLov(s, e, codigoLista) {
            componenteLOV = s;
            var valor = s.GetValue() != null ? s.GetValue() : "";
            var texto = s.GetText();
            showModal("lovFormulario.aspx?CL=" + codigoLista + "&Value=" + valor + '&Text=' + texto + '&' + callbackWF.cp_QS, "Lista de valores", 680, 200, null);
        }

        function atribuiResultadoLov() {
            componenteLOV.ClearItems();
            componenteLOV.AddItem(retornoModalTexto, retornoModalValor);
            componenteLOV.SetSelectedIndex(0);
        }


        function conteudoCampoAlterado() {
            existeConteudoCampoAlterado = true;
        }

        function executaCallbackWF() {
            if (window.parent.hfGeralWorkflow) {
                // copia o 'CodigoInstanciaWf' da frame Fluxo para a frame do formulário atual
                var CodigoInstanciaWf = window.parent.hfGeralWorkflow.Get('CodigoInstanciaWf');
                if (CodigoInstanciaWf.toString() == "-1")
                    callbackWF.PerformCallback();
            }
        }

        function fechaTelaPosSalvar() {
            if (callbackWF.cp_FechaModal == 'S')
                window.top.fechaModal();
        }

        function executaFuncaoTelaPai(codigoProjeto, codigoForm) {
            if (window.parent.funcaoPosSalvarFormulario)
                window.parent.funcaoPosSalvarFormulario(codigoProjeto, codigoForm);
        }

        function mudaVersao(urlFormulario) {
            window.open(urlFormulario, '_self');
        }

        function abreVersoes() {
            pcVersoes.Show();
        }

        function abreAssinaturas() {
            document.body.style.cursor = 'wait';
            callbackVerificacaoAssinatura.PerformCallback('');
            //pcAssinaturas.Show();
        }

        function ImprimeFormAssinado(param) {
            var codigoFormulario = -1;
            var parametros = param.split("&");
            for (var i = 0; i < parametros.length; i++) {
                var p = parametros[i].toLowerCase();
                if (p.substring(0, 3) == "cf=") {
                    codigoFormulario = parseInt(p.replace("cf=", ""));
                    break;
                }
            }
            document.body.style.cursor = 'wait';
            callbackVerificacaoAssinatura.PerformCallback(codigoFormulario);
        }

      /*  function desbloqueiaFormulario()
        {
            if (callbackVerificacaoAssinatura.cp_Desbloqueio == 'S') {
                window.top.callbackDesbloqueio.PerformCallback(callbackVerificacaoAssinatura.cp_CodigoFormulario);

                setTimeout(function () {
                    aguarde();
                }, 1000);
            }
        } */

    </script>
</head>
<body style="margin-left: 5px; margin-right: 5px; margin-bottom: 0px; margin-top: 0px;">
        <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                   <!-- <td class="style1">
                    </td> -->
                </tr>
                <tr>
                    <td>

                        <dxhf:ASPxHiddenField ID="hfSessao" runat="server" 
                            ClientInstanceName="hfSessao">
                        </dxhf:ASPxHiddenField>
                    </td>
                </tr>
            </table>
        </div>
        <dxpc:ASPxPopupControl ID="pcModal" runat="server" ClientInstanceName="pcModal"
            HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                    <iframe id="frmModal" name="frmModal" frameborder="0" style="overflow: auto; padding: 0px;
                        margin: 0px;"></iframe>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents PopUp="function(s, e) {
                            window.document.getElementById('frmModal').dialogArguments = myObject;
	                        document.getElementById('frmModal').src = urlModal;
                        }" Closing="function(s, e) {
                            if(retornoModal != null)
                                atribuiResultadoLov();
                             resetaModal();
                        }" />
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dxpc:ASPxPopupControl>
        </form>
    <dxcb:ASPxCallback ID="callbackWF" runat="server" ClientInstanceName="callbackWF"
        OnCallback="callbackWF_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	try
	{
		wfTela.eventoPosSalvar(s.cp_CodigoInstanciaWf);
	}catch(e){}
}" />
    </dxcb:ASPxCallback>
    
        
    
    <dxcb:ASPxCallback ID="callbackReload" runat="server" ClientInstanceName="callbackReload"
        OnCallback="callbackReload_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	
	try
	{
		wfTela.eventoPosSalvar(s.cp_CodigoInstanciaWf);
	}catch(e){}
	if(s.cp_URL != null &amp;&amp; s.cp_URL != '')
		window.parent.location = s.cp_URL;
}" BeginCallback="function(s, e) {
	//window.top.lpAguardeMasterPage.Show();
}" />
    </dxcb:ASPxCallback>



    <script type="text/javascript">
        var firstOpen = true;
        var opentab = false;
        $(function () {
            jQuery(document).ready(function ($) {
                $(window).resize(function () {
                    //redimenciona();
                });
                
                
                $('#pnExterno_pnFormulario_pcFormulario_TC a').stop().animate({ 'marginLeft': '-105px' }, 1000);
                $('a', $("#pnExterno_pnFormulario_pcFormulario_TC li").first().next()).trigger('mouseover');

                
                $('#pnExterno_pnFormulario_pcFormulario_TC').click(
                  function () {
                      closeFirstTab();
                  }
                );
                $('#pnExterno_pnFormulario_pcFormulario_CC').click(
                  function () {
                      closeFirstTab();
                  }
                );
            });

            function getDuplicateElement(originalElement) {
                var mClass = originalElement.attr("class").split(' ')[0];
                var mElement;
                if (mClass === "dxtc-activeTab") {
                    mElement = originalElement.prev();
                } else if (mClass === "dxtc-tab") {
                    mElement = originalElement.next();
                }

                return mElement;
            }

            function closeFirstTab() {
                if (firstOpen) {
                    firstOpen = false;
                    $('a', $("#pnExterno_pnFormulario_pcFormulario_TC li").first().next()).stop().animate({ 'marginLeft': '-105px' }, 200);
                    $('a', $("#pnExterno_pnFormulario_pcFormulario_TC li").first().next().next()).stop().animate({ 'marginLeft': '-105px' }, 200);
                    $("#pnExterno_pnFormulario_pcFormulario_TC li").first().next().animate({ width: "20px" }, 195);
                    //$("#pnExterno_pnFormulario_pcFormulario_TC li").first().next().next().animate({ width: "20px" }, 195);
                }
            }
            function resizeTabs() {
                $("#pnExterno_pnFormulario_pcFormulario_TC").find('li').each(function () {
                    var dElement = getDuplicateElement($(this));
                    $(this).animate({ width: "20px" }, 15);
                });
            }

            $('#pnExterno_pnFormulario_pcFormulario_TC > li').hover(
              function () {
                  if (opentab) {
                      closeFirstTab();
                  }
                  opentab = true;
                  var mClass = $(this).attr("class").split(' ')[0];
                  var dElement = getDuplicateElement($(this));
                  $(this).animate({ width: "120px" }, 195);
                  dElement.animate({ width: "120px" }, 195);
                  $('a', $(this)).stop().animate({ 'marginLeft': '-2px' }, 200);
                  $('a', dElement).stop().animate({ 'marginLeft': '-2px' }, 200);
              },
              function () {
                  var mClass = $(this).attr("class").split(' ')[0];
                  var dElement = getDuplicateElement($(this));
                  $('a', $(this)).stop().animate({ 'marginLeft': '-105px' }, 200);
                  $('a', dElement).stop().animate({ 'marginLeft': '-105px' }, 200);
                  $(this).animate({ width: "20px" }, 195);
                  dElement.animate({ width: "20px" }, 195);
              }
            );

            
        });

        
    </script>

    

    </body>
</html>
