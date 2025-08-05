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
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function onInit_mmObjeto(s, e) {
    try { return setMaxLength(s.GetInputElement(), 4000); }
    catch (e) { }
}

function setInformacaoConsultor(s, e) {
    var nomeConsultorColumn = s.GetColumnByField('NomeConsultor');

    if (!e.rowValues.hasOwnProperty(nomeConsultorColumn.index))
        return;
    var cellInfo = e.rowValues[nomeConsultorColumn.index];
    if (ddlConsultor.GetSelectedIndex() > -1 || cellInfo.text != ddlConsultor.GetText()) {
        cellInfo.value = ddlConsultor.GetValue();
        cellInfo.text = ddlConsultor.GetText();
        hfValores.Set("Cod_" + s.GetRowKey(e.visibleIndex), ddlConsultor.GetValue());
        ddlConsultor.SetValue(null);
    }
    else
        e.cancel = true;
}

function validaEdicao(s, e) {

}