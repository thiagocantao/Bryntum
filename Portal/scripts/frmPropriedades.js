function btnSalvar_Click(s, e) {
    var msg = ValidaCampos();
    if (msg == "") {
        callback.PerformCallback("");
        existeConteudoCampoAlterado = false;
    }
    else {
        window.top.mostraMensagem(msg, 'Atencao', true, false, null);
    }
}

function abrejanela(codigos) {
    var url = '../Administracao/frmPropriedade.aspx?CI=';
    url = url + codigos[0];
    url = url + '&CP=' + codigos[1];
    url = url + '&RO=N';
    window.top.showModal(url, 'Proprietário/ocupante', 720, 450);
}

function abrejanelaPessoaImovel(codigos,largura,altura) {
    var url = '../Administracao/frmProprietarioOcupante.aspx?CP=';
    url = url + codigos[0];
    url = url + '&CPI=' + codigos[1];

    if (largura == null) {
        largura = hfGeral.Get("larguraTela");
        altura = hfGeral.Get("alturaTela");
    }

    largura -= 100;
    altura -= 100;

    window.top.showModal(url, traducao.frmPropriedades_novo_propriet_rio_ocupante, largura, altura, atualizaPosPopup, null);
    //window.open(url, 'Ocupantes', 'menubar=no;titlebar=no;scrollbars=yes', true);
}

function atualizaPosPopup(valor) {
    gvProprietarioOcupante.PerformCallback();
}
 
function retornaValor(codigoImovel) {
    return codigoImovel;
}

//para funcionar o label mostrador de quantos caracteres faltam para terminar o texto
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
    else
        lblCantCarater.SetText(text.length);
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}
