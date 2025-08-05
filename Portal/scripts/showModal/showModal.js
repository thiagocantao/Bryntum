var posExecutar = null;
var retornoModal = null;

function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam, aspxLoadingPanel = null) {
    if (sWidth == null) {
        sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
    }
    if (sHeight == null) {
        sHeight = Math.max(0, document.documentElement.clientHeight) - 155;
    }


    if (parseInt(sHeight) < 535)
        sHeight = parseInt(sHeight) + 20;
    sWidth = sWidth <= 400 ? 900 : sWidth;

    myObject = objParam;
    posExecutar = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

    pcModal.SetWidth(sWidth);
    pcModal.SetWidth(sWidth);
    pcModal.SetHeight(sHeight + 50);
    pcModal.SetContentUrl(sUrl);
    //setTimeout ('alteraUrlModal();', 0);     
    document.getElementById('divTextoCabecalho').innerText = sHeaderTitulo;
    //pcModal.SetHeaderText(sHeaderTitulo);
    if (aspxLoadingPanel != null && aspxLoadingPanel != undefined) {
        aspxLoadingPanel.Hide();
    }
    pcModal.Show();

}

function fechaModal(returnValue = 'OK') {
    returnValueFormulario_cs = returnValue;
    pcModal.Hide();
}

function resetaModal() {
    posExecutar = null;
    pcModal.SetContentUrl(pcModal.cp_Path + "branco.htm");
    pcModal.SetHeaderText("");
    retornoModal = null;
}

function trataClickBotaoFecharPopupModal() {
    window.top.fechaModal();
}