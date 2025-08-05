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

    if (Trim(txtTituloTarefa.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") O da tarefa deve ser informado.";
    }
    if (txtEsforcoPrevisto.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") A estimativa deve ser informada.";
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
    txtTituloTarefa.SetText("");
    txtEsforcoPrevisto.SetText("");
    mmDetalhes.SetText("");

    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'TituloItem;EsforcoPrevisto;DetalheItem', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var tituloTarefa = (values[0] != null ? values[0] : "");
    var esforcoPrevisto = (values[1] != null ? values[1] : "");
    var detalhes = (values[2] != null ? values[2] : "");

    txtTituloTarefa.SetText(tituloTarefa);
    txtEsforcoPrevisto.SetText(esforcoPrevisto);
    mmDetalhes.SetText(detalhes);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtTituloTarefa.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtEsforcoPrevisto.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    mmDetalhes.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}