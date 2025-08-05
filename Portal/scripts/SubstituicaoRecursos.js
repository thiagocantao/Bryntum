//----------- Mensagem modificação con sucesso..!!!
function substituir() {
    gvDados.PerformCallback('SBS');
}

function verificaSeHabilitaBotaoSubstituir() {

    if (gvDados.GetSelectedRowCount() > 0
        && !isNaN(parseInt(ddlUsuarioOrigem.GetValue()))
        && !isNaN(parseInt(ddlUsuarioDestino.GetValue()))) {

        btSubstituir.SetEnabled(true);
    }
    else {
        btSubstituir.SetEnabled(false);
    }
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 230;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}