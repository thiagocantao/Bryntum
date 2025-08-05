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

    if (Trim(txtUnidade.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.unidadesDeMedida_a_unidade_deve_ser_informada_;
    }

    if (Trim(txtSigla.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.unidadesDeMedida_a_sigla_deve_ser_informada_;
    }

    if (spnFatorMultiplicador.GetValue() == 0 || spnFatorMultiplicador.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.unidadesDeMedida_o_fator_multiplicador_deve_ser_informado_;
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
    txtUnidade.SetText("");
    txtSigla.SetText("");
    spnFatorMultiplicador.SetValue(null);
    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoUnidadeMedida;DescricaoUnidadeMedida_PT;DescricaoUnidadeMedida_EN;DescricaoUnidadeMedida_ES;SiglaUnidadeMedida;FatorMultiplicador;IndicaControladoSistema;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {

    var codigoUnidadeMedida = (values[0] != null ? values[0] : "");
    var descricaoUnidadeMedida_PT = (values[1] != null ? values[1] : "");
    var descricaoUnidadeMedida_EN = (values[2] != null ? values[2] : "");
    var descricaoUnidadeMedida_ES = (values[3] != null ? values[3] : "");
    var siglaUnidadeMedida = (values[4] != null ? values[4] : "");
    var fatorMultiplicador = (values[5] != null ? values[5] : "");
    var indicaControladoSistema = (values[6] != null ? values[6] : "");

    txtUnidade.SetText(descricaoUnidadeMedida_PT);
    txtSigla.SetText(siglaUnidadeMedida);
    spnFatorMultiplicador.SetValue(fatorMultiplicador);
    desabilitaHabilitaComponentes();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtUnidade.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtSigla.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    spnFatorMultiplicador.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}