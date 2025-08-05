function enviaParaAprovacao()
{
    callBack.PerformCallback();
}   

function abreComentarios(comentarios, data, codigoProjeto, comentariosRecurso, codigoTipoTarefaTimeSheet) {
    while (comentarios.indexOf("¥") != -1) {
        comentarios = comentarios.replace("¥", "\n");
    }
    while (comentariosRecurso.indexOf("¥") != -1) {
        comentariosRecurso = comentariosRecurso.replace("¥", "\n");
    }
    hfData.Set('CodigoProjeto', codigoProjeto);
    hfData.Set('Data', data);
    hfData.Set('Erro', '');
    hfData.Set('codigoTipoTarefaTimeSheet', codigoTipoTarefaTimeSheet);
    txtComentarioAprovador.SetText(comentarios);
    txtComentarioRecurso.SetText(comentariosRecurso);
    pcAprovacao.Show();
}

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