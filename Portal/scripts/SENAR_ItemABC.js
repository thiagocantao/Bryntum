var comando;
function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
   var mensagemErro_ValidaCamposFormulario = "";

    var numAux = 0;
    var mensagem = "";

    if (txtDescricao.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.SENAR_ItemABC_descri__o_deve_ser_informada_;
    }
    if (rbTipo.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.SENAR_ItemABC_tipo_deve_ser_informado_;
    }
    if (spnValor.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.SENAR_ItemABC_valor_deve_ser_informado_;
    }
    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {

    txtDescricao.SetText("");
    rbTipo.SetValue();
    spnValor.SetValue();
    ckbAtivo.SetChecked(false);

    desabilitaHabilitaComponentes();
}

function desabilitaHabilitaComponentes() {
    txtDescricao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    rbTipo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    spnValor.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ckbAtivo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoItem;DescricaoItem;ValorUnitarioItem;TipoItem;IndicaItemAtivo;IniciaisItem;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var CodigoItem = (values[0] != null ? values[0] : "");
    var DescricaoItem = (values[1] != null ? values[1] : "");
    var ValorUnitarioItem = (values[2] != null ? values[2] : "");
    var TipoItem = (values[3] != null ? values[3] : "");
    var IndicaItemAtivo = (values[4] != null ? values[4] : "");
    var IniciaisItem = (values[5] != null ? values[5] : "");

    txtDescricao.SetText(DescricaoItem);
    rbTipo.SetValue(TipoItem);
    spnValor.SetValue(ValorUnitarioItem);
    ckbAtivo.SetChecked(IndicaItemAtivo == "S");
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}