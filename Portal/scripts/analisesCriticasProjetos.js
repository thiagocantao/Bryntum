String.prototype.stripHTML = function () { return this.replace(/<.*?>/g, ''); }

function onClick_BtnAvancar(s, e) {
    if (Valida()) {
        try {
            popUp.close();
        } catch (e) { }

        var args = 'Avancar';
        pnCallback.PerformCallback(args);
        window.setTimeout("mostraPopupBoletim()", 3000);

        return false;
    }
}

function mostraPopupBoletim() {
    window.top.fechaModal();
    var varOpener = this;

    //    var variaveis = '?podeEditar=' + btnAvancar.cp_podeEditar + '&codStatusReport=' + btnAvancar.cp_CodigoStatusReport;
    //    var nomeArquivo = 'popupRelProgramas.aspx';
    //    var path = '';
    //    if(window.top.document.location.pathname.indexOf('Boletim') != -1);
    //        path = '../DadosProjeto/';
    //    var url = path + nomeArquivo + variaveis;
    var iniciais = btnAvancar.cp_iniciais;

                                             
    var url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/popupRelProgramas.aspx?podeEditar=" + btnAvancar.cp_podeEditar + "&codStatusReport=" + btnAvancar.cp_CodigoStatusReport + "&iniciais=" + iniciais;
//    var cont = window.top.document.location.pathname.split("/").length - 3;
//    var ajustPath = "";
//    for (var i = 0; i < cont; i++) {
//        ajustPath = "../" + ajustPath;
//    }
//    url = ajustPath + url;
//chamando igual está chamando na tela de status report
   window.top.showModal(url, traducao.analisesCriticasProjetos_boletim, 800, (screen.height - 190), null, varOpener);
}

function Valida() {
    return true;
}


 
function onClick_BtnSalvar(s, e) {
    var htmlGerado = htmlAnaliseEdit.GetHtml();
    htmlGerado = htmlGerado.stripHTML();

    //if (htmlAnaliseEdit.GetHtml().length <= 8000) {
    if (htmlGerado.length <= 8000) {
        pnCallback.PerformCallback('Salvar');
        popup.Hide();
    }
    else
        window.top.mostraMensagem(traducao.analisesCriticasProjetos_a_an_lise_deve_ter_menos_de_8000_caracteres_, 'erro', true, false, null);
}

function local_onEnd_pnCallback(s, e) {
    if (window.onEnd_pnCallback)
        onEnd_pnCallback();

    if ("Salvar" == s.cp_OperacaoOk)
        window.top.mostraMensagem(traducao.analisesCriticasProjetos_altera__es_salvas_com_sucesso_, 'sucesso', false, false, null);
}

function OnPopup(comentarioGeral) {
    var analise = "";
    if (hfGeral.Get("TipoRegistroEmEdicao") == "pai")
        analise = htmlAnalise.GetHtml();
    else
        analise = comentarioGeral;
    //se 'analise' from igual a 'null' é atribuido a ela uma string vazia para evitar a ocorrencia de um erro
    if (analise == null)
        analise = "";
    htmlAnaliseEdit.SetHtml(analise);
}

//function SetMaxLength(memo, maxLength) {
//    if (!memo)
//        return;
//    if (typeof (maxLength) != "undefined" && maxLength >= 0) {
//        memo.maxLength = maxLength;
//        memo.maxLengthTimerToken = window.setInterval(function () {
//            var text = memo.GetText();
//            if (text && text.length > memo.maxLength)
//                memo.SetText(text.substr(0, memo.maxLength));
//        }, 10);
//    } else if (memo.maxLengthTimerToken) {
//        window.clearInterval(memo.maxLengthTimerToken);
//        delete memo.maxLengthTimerToken;
//        delete memo.maxLength;
//    }
//}

//function imposeMaxLength(Object, MaxLen) {
//    return (Object.GetText().length <= MaxLen);
//}


function setMaxLength(textAreaElement, length) {
    textAreaElement.maxlength = length;
    ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
    ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
}

function onKeyUpOrChange(evt) {
    processTextAreaText(ASPxClientUtils.GetEventSource(evt));
}

function processTextAreaText(textAreaElement) {
    var maxLength = textAreaElement.maxlength;
    var text = textAreaElement.value;
    var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
    if (maxLength != 0 && text.length > maxLength)
        textAreaElement.value = text.substr(0, maxLength);
    else {
        var id = textAreaElement.id;
        if (id.indexOf('AnaliseGeral') > -1)
            lblContCaracterEdit.SetText(text.length);
        else
            document.getElementById('lblContador').innerText = text.length;
    }
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}