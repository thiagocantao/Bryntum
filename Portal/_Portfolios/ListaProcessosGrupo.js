function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 160;
    sp_Tela.SetHeight(height);
    //nvbMenuFluxos.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}