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

    if (Trim(txtDescricao.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.dotacoesOrcamentarias_a_dota__o_or_ament_ria_deve_ser_informada_;
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
    txtDescricao.SetText("");

    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'Dotacao;DataExclusao;CodigoEntidade;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var codigo = (values[0] != null ? values[0] : "");
    var descricao = (values[1] != null ? values[1] : "");

    txtDescricao.SetText(codigo);
    txtDescricao.SetValue(codigo);
    hfGeral.Set("txtDescricao", codigo);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function trataMensagemErro(TipoOperacao, mensagemErro) {
    if (TipoOperacao == "Excluir") {
        if (mensagemErro.indexOf('REFERENCE') >= 0)
            return traducao.dotacoesOrcamentarias_o_registro_n_o_pode_ser_exclu_do__pois_existem_mais_de_uma_vers_o_relacionada_a_esse_arquivo_;
        else if (mensagemErro.indexOf('REFERENCE') >= 0)
            return traducao.dotacoesOrcamentarias_o_registro_n_o_pode_ser_exclu_do__pois_est__sendo_utilizado_por_outra_tela_;
        else return mensagemErro;
    }
    else if (TipoOperacao == "Incluir") {
        if (mensagemErro.indexOf('chave duplicada') >= 0)
            return traducao.dotacoesOrcamentarias_o_registro_n_o_pode_ser_inclu_do__pois_o_mesmo_j__existe_na_base_de_dados_;
        else return mensagemErro;
    }
    else
        return "";
}

function desabilitaHabilitaComponentes() {
    txtDescricao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

function OnGridFocusedRowChanged1(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'Dotacao;DataExclusao;CodigoEntidade;', excluirIncluirRegistro);
    }
}
function excluirIncluirRegistro(values) {
    var codigo = (values[0] != null ? values[0] : "");
    var dataExclusao = (values[1] != null ? values[1] : "");
    if (dataExclusao == "") {
    onClickBarraNavegacao("Excluir", gvDados, pcDados);
    }
    else {
        //onClickBarraNavegacao("Restaurar", gvDados, pcDados);
        var confirmacao = window.confirm(traducao.dotacoesOrcamentarias_deseja_realmente_restaurar_o_registro_selecionado_);
        if (confirmacao == true) {
            pnCallback.PerformCallback("Restaurar");
        }        
    }

}
