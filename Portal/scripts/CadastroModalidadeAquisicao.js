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

    if (Trim(txtDescricaoModalidadeAquisicao.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroModalidadeAquisicao_a_descri__o_da_modalidade_de_aquisi__o_deve_ser_informada_;
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
    txtDescricaoModalidadeAquisicao.SetText("");

    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'Codigo;Descricao;CodigoEntidade', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    LimpaCamposFormulario();
    desabilitaHabilitaComponentes();

    var codigo = (values[0] != null ? values[0] : "");
    var descricao = (values[1] != null ? values[1] : "");
    var codigoEntidade = (values[1] != null ? values[1] : "");

    txtDescricaoModalidadeAquisicao.SetText(descricao);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtDescricaoModalidadeAquisicao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao() {
    onClick_btnCancelar();
}


function OnClick_CustomButtons(s, e) {

    s.SetFocusedRowIndex(e.visibleIndex);
    e.processOnServer = false;
    if (e.buttonID == "btnEditar") {
        TipoOperacao = "Editar";
        hfGeral.Set("TipoOperacao", TipoOperacao);
        OnGridFocusedRowChanged(gvDados);
        onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
    }
    else if (e.buttonID == "btnExcluir") {
        onClickBarraNavegacao("Excluir", gvDados, pcDados);
    }
    else if (e.buttonID == "btnDetalhesCustom") {
        TipoOperacao = "Consultar";
        hfGeral.Set("TipoOperacao", TipoOperacao);
        OnGridFocusedRowChanged(gvDados, true);
        //Ocultar botones.
        pcDados.Show();
    }
}