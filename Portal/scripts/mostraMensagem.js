    var eventoOKMsg = null;
    function remove(myIdAlert) {
        $("#" + $(myIdAlert).data("id")).remove();
        redimenciona();
    }
    function redimenciona() {
        try {
            var scrollheight = $("body")[0].clientHeight + 10;
            window.parent.document.getElementById("wfFormularios").style = "height:" + scrollheight + "px;";
        } catch (e) {
        }
        
    }

    function eventOk(myIdAlert) {
        remove(myIdAlert);
        console.log();
        eventoOKMsg();

    }
    //tipo: success, info, warning, danger
// topElement: opcional.ID do elemento. A janela de mensagem vai se posicionar em cima deste elemento. Se não, ela será o primeiro elemento do body.
    function mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout, tipo, topElement) {
        var myIdAlert = "alert1";
        $("#" + myIdAlert).remove();
        var alertMsg = "<div id='" + myIdAlert + "' class='alert alert-" + tipo + " text-center alert-msg'>";
        if (!mostraBtnOK && !mostraBtnCancelar) {
            alertMsg += "<a class='close' data-id='" + myIdAlert + "' onclick='remove(this);'>×</a>  ";
        }
        alertMsg += textoMsg + "<div class='content-btn' >";
        if (mostraBtnCancelar) {
            alertMsg += "<button data-id='" + myIdAlert + "' onclick='remove(this);' class='btn btn-default btn-cancel'>Cancelar</button>";
        }
        if (mostraBtnOK) {
            alertMsg += "<button data-id='" + myIdAlert + "' class='btn btn-info btn-ok' onclick='eventOk(this);' >Ok</button>";
        }
        alertMsg += "</div>" +
        "</div>";
        if (topElement != null) {
            $(alertMsg).insertBefore("#" + topElement);
        } else {
            $(alertMsg).insertBefore("#content-btns-formulario");
            //$("body").prepend($(alertMsg)); 
        }

        $('html, body').animate({
            scrollTop: $("#" + myIdAlert).offset().top
        }, 500);
        

        if (mostraBtnOK) {
            if (eventoOK != null) {
                eventoOKMsg = eventoOK;
            }
        }
        if (!mostraBtnOK && !mostraBtnCancelar) {
            $(".content-btn").remove();
        }
        redimenciona();
        if (timeout) {
            setTimeout(function () {
                fechaMensagem(myIdAlert);
            }, timeout);
        }

    }
    function fechaMensagem(myIdAlert) {
        $("#" + myIdAlert).remove();
        redimenciona();
    }
    $(function () {
        jQuery(document).ready(function ($) {
            //mostraMensagem("Esto es una prueba.", null, false, false, function () { funcObj['prueba']() }, 0, "warning");
        });

            
        });
