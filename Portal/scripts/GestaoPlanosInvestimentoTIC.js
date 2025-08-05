var tipoEdicao = '';
function publicar() {
    gvDados.PerformCallback('P');
}


function abreReativacao() {
    document.getElementById('tdDetalhesProjeto').style.display = 'none';
    tipoEdicao = 'R';
    pcDados.Show();
    
}

function abreEdicao(nomeProjeto) {
    document.getElementById('tdDetalhesProjeto').style.display = '';
    txtProjeto.SetText(nomeProjeto);
    tipoEdicao = 'E';
    pcDados.Show();
}

function abreExclusao() {
    if (gvDados.GetSelectedRowCount() == 0)
        window.top.mostraMensagem(traducao.GestaoPlanosInvestimentoTIC_nenhum_projeto_foi_selecionado_, 'Atencao', true, false, null);
    else {
        if (confirm(traducao.GestaoPlanosInvestimentoTIC_deseja_marcar_os_projetos_como_n_o_selecionados_para_loa_)) {
            document.getElementById('tdDetalhesProjeto').style.display = 'none';
            tipoEdicao = 'X';
            pcDados.Show();
        }
    }
}

function abreDetalhes(codigoProjeto, codigoPlanoInvestimento) {
    window.top.showModal('HistoricoPlanoInvestimento.aspx?CP=' + codigoProjeto + '&CPI=' + codigoPlanoInvestimento, "Histórico de Movimentos do Projeto", 850, 500, "", null);
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

function executaAcao() {
    if (tipoEdicao == 'R') {
        verificaReativacao();
    } else if (tipoEdicao == 'E') {
        verificaEdicao();
    } else if (tipoEdicao == 'X') {
        verificaExclusao();
    } 
}

function verificaReativacao() {
    if (validaCamposFormulario()) 
        gvDados.PerformCallback('R');
	
}

function verificaEdicao() {
    if (validaCamposFormulario())
        gvDados.PerformCallback('E');
}

function verificaExclusao() {
   if (validaCamposFormulario())  {
        gvDados.PerformCallback('X');
    }
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    var numAux = 0;
    var mensagem = "";

    if (Trim(mmObjeto.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.GestaoPlanosInvestimentoTIC_o_campo__coment_rios__deve_ser_informado_;
    }

    if (mensagem != "") {
        window.top.mostraMensagem(traducao.GestaoPlanosInvestimentoTIC_alguns_dados_s_o_de_preenchimento_obrigat_rio_ + "\n\n" + mensagem, 'Atencao', true, false, null);
        return false;
    }

    return true;
}

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO')) {
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
        pcDados.Hide();
    }
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    
}

function OnContextMenu(s, e) {
    if (e.objectType == 'header') {
        colName = s.GetColumn(e.index).fieldName;
        headerMenu.GetItemByName('HideColumn').SetEnabled((colName == null || colName == 'CodigoProjeto' || colName == 'CodigoStatus' ? false : true));
        headerMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }
}

function OnItemClick(s, e) {
    if (e.item.name == 'HideColumn') {
        gvDados.PerformCallback(colName);
        colName = null;
    }
    else {
        if (gvDados.IsCustomizationWindowVisible())
            gvDados.HideCustomizationWindow();
        else
            gvDados.ShowCustomizationWindow();
    }
}