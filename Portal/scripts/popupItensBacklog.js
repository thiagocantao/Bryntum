// JScript File
var urlFrmAnexosContrato = '';
var atualizarURLAnexos = '';
var comando = '';
var map = {
    "â": "a", "Â": "A", "à": "a", "À": "A", "á": "a", "Á": "A", "ã": "a", "Ã": "A",
    "ê": "e", "Ê": "E", "è": "e", "È": "E", "é": "e", "É": "E",
    "î": "i", "Î": "I", "ì": "i", "Ì": "I", "í": "i", "Í": "I",
    "õ": "o", "Õ": "O", "ô": "o", "Ô": "O", "ò": "o", "Ò": "O", "ó": "o", "Ó": "O",
    "ü": "u", "Ü": "U", "û": "u", "Û": "U", "ú": "u", "Ú": "U", "ù": "u", "Ù": "U", "ç": "c", "Ç": "C"
};

function removerAcentosECaracteresEspeciais(s) {
    var resultadoMapeamento = s.replace(/[\W\[\] ]/g, function (a) { return map[a] || a });
    var retorno1 = resultadoMapeamento.replace(/(([\{\}\,\.\%\*\=\&\¨\:\(\)\!\?\'´\´\`\~\^])+)/g, '');
    return retorno1;
}

function excluiItemBacklog(valor) {
    callbackTela.PerformCallback("Excluir");
}

function incluiItemBacklog() {
    hfGeral.Set("TipoOperacao", "Incluir");
    desabilitaHabilitaComponentes('N');

    LimpaCamposFormulario();
    pcDados.Show();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    // se já incluiu alguma opção, feche a tela após salvar
    pcDados.Hide();
}

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

function podeMudarAba(s, e) {
    if (e.tab.index == 0) {
        return false;
    }
    else if (hfGeral.Get("TipoOperacao") == 'Incluir') {
        window.top.mostraMensagem(traducao.popupItensBacklog_para_ter_acesso_a_op__o + " \"" + e.tab.GetText() + "\" " + traducao.popupItensBacklog___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + tabControl.GetTab(0).GetText() + "\"", 'Atencao', true, false, null);
        return true;
    }

    return false;
}

function ValidaCamposFormulario() {
    var mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;

    if (txtTituloItem.GetText().toString().trim() == "") {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") Título do item deve ser informado. \n";
    }
    //if (txtEsforco.GetText() == "") {
    //    mensagemErro_ValidaCamposFormulario += countMsg++ + ") Estimativa do item deve ser informada. \n";
    //}
    if (txtImportancia.GetText().toString().trim() == "" || txtImportancia.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") Importância deve ser informada. \n";
    }

    return mensagemErro_ValidaCamposFormulario;

}