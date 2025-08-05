
function showModalFormulario(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
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
    posExecutarFormularioDinamico = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

    pcFormularioDinamico.SetWidth(sWidth);
    pcFormularioDinamico.SetWidth(sWidth);
    pcFormularioDinamico.SetHeight(sHeight + 50);
    pcFormularioDinamico.SetContentUrl(sUrl);
    //setTimeout ('alteraUrlModal();', 0);     
    document.getElementById('divTextoCabecalhoComFooter').innerText = sHeaderTitulo;
    //pcFormularioDinamico.SetHeaderText(sHeaderTitulo);

    pcFormularioDinamico.Show();

}


function resetaModalFormularioDinamico() {
    posExecutarFormularioDinamico = null;
    pcFormularioDinamico.SetContentUrl(pcFormularioDinamico.cp_Path + "branco.htm");
    pcFormularioDinamico.SetHeaderText("");
    retornoModalComFooter = null;
}

function fechaModalFormularioDinamico(returnValue = 'OK', statusSalvar) {
    returnValueFormulario_cs = returnValue;
    statusSalvarFormularioDinamico = statusSalvar;
    pcFormularioDinamico.Hide();
}

function processa_fechaModalFormularioDinamico() {
    window.returnValue = 'Cancel';
    if (pcFormularioDinamico.GetContentIFrameWindow().hfGeralFormulario != undefined && pcFormularioDinamico.GetContentIFrameWindow().hfGeralFormulario.Contains('StatusSalvar') && pcFormularioDinamico.GetContentIFrameWindow().hfGeralFormulario.Get('StatusSalvar') == '1') {
        window.returnValue = 'OK';
    }
    if (pcFormularioDinamico.GetContentIFrameWindow().hfGeralFormulario != undefined)
        window.top.fechaModalFormularioDinamico(window.returnValue, pcFormularioDinamico.GetContentIFrameWindow().hfGeralFormulario.Get('StatusSalvar'));
    else
        window.top.fechaModalFormularioDinamico(window.returnValue, 0);
}