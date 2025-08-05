/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
var TipoEdicao = 'C';
var codigoContratoGlobal = -1;
function abrePermissoes(codigoContrato, numeroContrato) {
    var idObjeto = (codigoContrato != null ? codigoContrato : "-1");
    var tituloObjeto = traducao.ListaContratosExtendido_contrato_n___ + (numeroContrato != null ? numeroContrato : "");
    var tituloMapa = "";

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = screen.width - 100;
    var window_height = screen.height - 200;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;

    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=CT&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa + '&AlturaGrid=' + (window_height - 110), traducao.ListaContratosExtendido_permiss_es, null, null, '', null);
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
    else {
        if (textAreaElement.name.indexOf("mmObservacoes") >= 0)
            lblCantCaraterOb.SetText(text.length);
        else
            lblCantCarater.SetText(text.length);
    }
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function onInit_mmObjeto(s, e) {
    try { return setMaxLength(s.GetInputElement(), 4000); }
    catch (e) { }
}

function onInit_mmObservacoes(s, e) {
    try { return setMaxLength(s.GetInputElement(), 500); }
    catch (e) { }
}

function onInit_mmEncerramento(s, e) {
    try { return setMaxLength(s.GetInputElement(), 2000); }
    catch (e) { }
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}

function abreNovo() {
 }

function novoRegistro() {
    TipoEdicao = 'I';
    var codigoProjeto = gvDados.cp_CodigoProjeto;
    var larguraDisponivel = screen.availWidth;
    var alturaDisponivel = screen.availHeight;
    //document.getElementById('frmDetalhes').src = 
    //frmDetalhes.location = 'DetalhesContratoEstendido.aspx?CodigoContrato=-1&ID=' + codigoProjeto + '&RO=N&Alt=' + alturaFrames;
    var url1 = window.top.pcModal.cp_Path + '_Projetos/Administracao/DetalhesContratoEstendido.aspx?CodigoContrato=-1&ID=' + codigoProjeto + '&RO=N&Alt=' + alturaDisponivel + '&Larg=' + larguraDisponivel + '&PodeAlterarProjeto=' + gvDados.cp_PodeAlterarProjeto;
    window.top.showModal(url1, 'Detalhes', (larguraDisponivel - 100), (alturaDisponivel - 300), carregaGrid, null);
}

function limpaFrame() {
    frmDetalhes.location = "ListaContratoEstendido.aspx";
    pcDados.Hide();
}

function abreDetalhes(codigoContrato, codigoProjeto, somenteLeitura) {
    TipoEdicao = (somenteLeitura == 'S') ? 'C' : 'E';

    var larguraDisponivel = screen.availWidth;
    var alturaDisponivel = screen.availHeight;


    var url1 = window.top.pcModal.cp_Path + '_Projetos/Administracao/DetalhesContratoEstendido.aspx?CodigoContrato=' + codigoContrato + '&ID=' + codigoProjeto + '&RO=' + somenteLeitura + '&Alt=' + alturaDisponivel + '&Larg=' + larguraDisponivel + '&PodeAlterarProjeto=' + gvDados.cp_PodeAlterarProjeto;
    window.top.showModal(url1, 'Detalhes', null, null, carregaGrid, null);
}

function carregaGrid() {
    gvDados.Refresh();
}

function excluiContrato(codigoContrato) {
    codigoContratoGlobal = codigoContrato;
    window.top.mostraMensagem(traducao.ListaContratosExtendido_deseja_excluir_o_contrato_selecionado_, 'confirmacao', true, true, executaExclusaoContrato);
    
}

function executaExclusaoContrato() {
    gvDados.PerformCallback(codigoContratoGlobal);
}