// Custom namespace
var modal = {};
var alertEventoOK = null;
modal.hide = function () {
    $('#overlay').fadeOut();
    $('.dialog').fadeOut();
};

function alertRemove() {
    modal.hide();
    $(modal.id).remove();
    if (alertEventoOK != null) {
        alertEventoOK();
    }
    else
        console.log(traducao.alertModal_evento_passado_para_alert___nulo_);
    //return false;
}

function mostraAlert(textoMsg, _eventoOK, timeout) {
    var idAlert = "overlay";
    modal.id = "#myAlertModal";
    $(modal.id).remove();
    var alertMsg = '<div id="' + idAlert + '">';
        alertMsg += '<div id="screen"></div>';
        alertMsg += '<div id="myAlertModal" class="dialog modal">';
            alertMsg += '<div class="label-dialog"><img class="alert-img" src="./imagens/movel/portalicon.png" ></div>';
                    alertMsg += '<div class="body-dialog">';
                    alertMsg += '<p>'+textoMsg+'</p>';
                alertMsg += '</div>';
                alertMsg += '<div class="ok-dialog" onclick="alertRemove();" ><i class="fa fa-check-circle" aria-hidden="true"></i></div>';
        alertMsg += '</div>';
    alertMsg += '</div>';
    $("body").prepend($(alertMsg));
    // Prevent dialog closure when clicking the body of the dialog (overrides closing on clicking overlay)
    $('.dialog').click(function () {
        event.stopPropagation();
    });
    if (_eventoOK != null) {
        alertEventoOK = _eventoOK;
    }
    $('#overlay').fadeIn();
    $(modal.id).fadeIn();
    if (timeout !== undefined) {
        setTimeout(function () {
            $(modal.id).fadeOut();
        }, timeout);
    }
}