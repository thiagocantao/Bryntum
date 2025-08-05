function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (Trim(txtTipoDeQuestao.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.cadastroTiposQuestoes_o_tipo_de_quest_o_deve_ser_informado_;
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    txtTipoDeQuestao.SetText("");
    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoTipoRiscoQuestao;DescricaoTipoRiscoQuestao;IndicaControladoSistema;IndicaRiscoQuestao;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {

    var codigoTipoRiscoQuestao = (values[0] != null ? values[0] : "");
    var descricaoTipoRiscoQuestao = (values[1] != null ? values[1] : "");
    var indicaControladoSistema = (values[2] != null ? values[2] : "");
    var indicaRiscoQuestao = (values[3] != null ? values[3] : "");

    txtTipoDeQuestao.SetText(descricaoTipoRiscoQuestao);
    desabilitaHabilitaComponentes();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtTipoDeQuestao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");

}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}