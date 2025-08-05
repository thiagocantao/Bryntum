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

    if (ddlTema.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.PropostaAcao_o_tema_deve_ser_informado_;
    }
    
    if (Trim(txtPropostaAcao.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.PropostaAcao_a_proposta_de_a__o_deve_ser_informada_;
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
    txtPropostaAcao.SetText("");

    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoProposta;DescricaoProposta;DescricaoResumidaTemaMobilizacaoSindical;CodigoTemaMobilizacaoSindical', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var codigoTipoAtucao = (values[0] != null ? values[0] : "");
    var DescricaoTipoAtuacao = (values[1] != null ? values[1] : "");
    var codigoTema = (values[3] != null ? values[3] : "");
    var descricaoTema = (values[2] != null ? values[2] : "");

    txtPropostaAcao.SetText(DescricaoTipoAtuacao);
    ddlTema.SetValue(codigoTema);
    ddlTema.SetText(descricaoTema);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtPropostaAcao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlTema.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}
