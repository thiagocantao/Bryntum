var parametro = '-1';
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

    if (Trim(txtNomeGrupo.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.grupoParticipantesEvento_o_nome_do_grupo_deve_ser_informado_;
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
    txtNomeGrupo.SetText("");
    hfGeral.Set("Selecionados", "");
    //lbDisponiveis.PerformCallback("-1");
    //lbSelecionados.PerformCallback("-1");

    desabilitaHabilitaComponentes();
    parametro = "-1";
    var tipoOperacao = hfGeral.Get("TipoOperacao").toString();
    if (tipoOperacao == "Incluir")
        gvParticipantesEventos.PerformCallback(parametro);
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoGrupoParticipantes;DescricaoGrupoParticipantes;CodigoEntidade;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var codigoGrupoParticipantes = (values[0] != null ? values[0] : "");
    var descricaoGrupoParticipantes = (values[1] != null ? values[1] : "");
    var CodigoEntidade = (values[2] != null ? values[2] : "");

    parametro = codigoGrupoParticipantes;

    txtNomeGrupo.SetText(descricaoGrupoParticipantes);

    gvParticipantesEventos.PerformCallback(parametro);

    //lbDisponiveis.PerformCallback(codigoGrupoParticipantes);
    //lbSelecionados.PerformCallback(codigoGrupoParticipantes);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtNomeGrupo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    //lbDisponiveis.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    //lbSelecionados.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}
