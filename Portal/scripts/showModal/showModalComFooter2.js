var posExecutarComFooter2 = null;
var retornoModalComFooter2 = null;

function showModalComFooter2(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
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
    posExecutarComFooter2 = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

    pcModalComFooter2.SetWidth(sWidth);
    pcModalComFooter2.SetWidth(sWidth);
    pcModalComFooter2.SetHeight(sHeight + 50);
    pcModalComFooter2.SetContentUrl(sUrl);
    //setTimeout ('alteraUrlModal();', 0);     
    document.getElementById('divTextoCabecalhoComFooter2').innerText = sHeaderTitulo;
    //pcModal.SetHeaderText(sHeaderTitulo);

    pcModalComFooter2.Show();

}

function resetaModalComFooter2() {
    posExecutarComFooter2 = null;
    pcModalComFooter2.SetContentUrl(pcModalComFooter2.cp_Path + "branco.htm");
    pcModalComFooter2.SetHeaderText("");
    retornoModalComFooter2 = null;
}

function fechaModalComFooter2(returnValue = 'OK') {
    returnValueFormulario_cs = returnValue;
    pcModalComFooter2.Hide();
}
