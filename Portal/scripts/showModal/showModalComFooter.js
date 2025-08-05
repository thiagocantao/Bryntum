var posExecutarComFooter = null;
var retornoModalComFooter = null;

function showModalComFooter(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam, aspxLoadingPanel = null) {
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
    posExecutarComFooter = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

    pcModalComFooter.SetWidth(sWidth);
    pcModalComFooter.SetWidth(sWidth);
    pcModalComFooter.SetHeight(sHeight + 50);
    pcModalComFooter.SetContentUrl(sUrl);
    //setTimeout ('alteraUrlModal();', 0);     
    document.getElementById('divTextoCabecalhoComFooter').innerText = sHeaderTitulo;
    //pcModal.SetHeaderText(sHeaderTitulo);

    pcModalComFooter.Show();

}

function resetaModalComFooter() {
    posExecutarComFooter = null;
    pcModalComFooter.SetContentUrl(pcModalComFooter.cp_Path + "branco.htm");
    pcModalComFooter.SetHeaderText("");
    retornoModalComFooter = null;
}

function fechaModalComFooter(returnValue = 'OK') {
    returnValueFormulario_cs = returnValue;
    pcModalComFooter.Hide();
}
