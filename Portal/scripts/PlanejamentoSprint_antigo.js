var valorPercentualAntigo = null;

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

    if (ddlRecurso.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") O recurso deve ser informado.";
    }
    if (ddlPapelRecurso.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") O papel do recurso deve ser informado.";
    }
    if (txtPercentualDedicacao.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") O percentual de dedicação deve ser informado.";
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
    ddlRecurso.SetValue(null);
    ddlPapelRecurso.SetValue(null);
    txtPercentualDedicacao.SetText("");
    callbackCusto.cp_HorasDedicadas = "";
    txtCusto.SetText("");
    txtReceita.SetText("");
    txtRecurso.SetText("");
    txtHorasDedicadas.SetText("");
    valorPercentualAntigo = null;

    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoRecursoCorporativo;CodigoTipoPapelRecursoIteracao;PercentualAlocacao;CustoUnitario;ReceitaUnitaria;NomeRecurso;HorasDedicadas', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var codigoRecurso = (values[0] != null ? values[0] : null);
    var codigoPapelRecurso = (values[1] != null ? values[1] : null);
    var percentualAlocacao = (values[2] != null ? values[2] : "");
    var custo = (values[3] != null ? values[3] : "");
    var receita = (values[4] != null ? values[4] : "");
    var nomeRecurso = (values[5] != null ? values[5] : "");
    var horasDedicadas = (values[6] != null ? values[6] : "");
    valorPercentualAntigo = (values[2] != null ? values[2] : null);

    ddlRecurso.SetValue(codigoRecurso);
    ddlPapelRecurso.SetValue(codigoPapelRecurso);
    txtPercentualDedicacao.SetValue(percentualAlocacao);
    txtCusto.SetValue(custo);
    txtReceita.SetText(receita);
    txtRecurso.SetText(nomeRecurso);
    txtHorasDedicadas.SetValue(horasDedicadas);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    if (TipoOperacao == "Incluir") {
        txtRecurso.SetVisible(false);
        ddlRecurso.SetVisible(true);
    } else {
        txtRecurso.SetVisible(true);
        ddlRecurso.SetVisible(false);
    }
    ddlRecurso.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlPapelRecurso.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtPercentualDedicacao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    //txtCusto.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtReceita.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

//ABA DE ITENS
function validaCamposFormularioItem() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (txtEsforcoPrevisto.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") A estimativa deve ser informada.";
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridItemFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'TituloItem;Importancia;EsforcoPrevisto;Complexidade;DetalheItem;', MontaCamposFormularioItem);
    }
}

function MontaCamposFormularioItem(values) {

    var item = (values[0] != null ? values[0] : '');
    var importancia = (values[1] != null ? values[1] : '');
    var esforcoPrevisto = (values[2] != null ? values[2] : "");
    var complexidade = (values[3] != null ? values[3] : "");
    var detalhesItem = (values[4] != null ? values[4] : "");

    txtTituloItem.SetText(item);
    txtImportancia.SetValue(importancia);
    txtEsforcoPrevisto.SetValue(esforcoPrevisto);
    ddlComplexidade.SetValue(complexidade);
    mmDescricaoItem.SetText(detalhesItem);
}

function abreSelecaoTarefas() {
    window.top.showModal('./SelecaoItensPlanejamentoSprint_antigo.aspx?CP=' + gvItens.cp_CP, 'Selecionar Itens', 1000, 460, finalizaEdicao, null);
}

function finalizaEdicao(retorno) {
    gvItens.PerformCallback();
    callbackDadosPlanejamento.PerformCallback();
}

function abreTarefas(codigoItem, somenteLeitura) {
    window.top.showModal('./TarefasItemBacklog_antigo.aspx?CodigoItem=' + codigoItem + "&RO=" + somenteLeitura, 'Tarefas', 1000, 430, finalizaEdicao, null);
}

function calculaHoras()
{
    var possuiHoras = (callbackCusto.cp_HorasDedicadas != null && callbackCusto.cp_HorasDedicadas != '');
    var possuiPercentual = (txtPercentualDedicacao.GetValue() != null);

    if (TipoOperacao == "Editar") {
        if (valorPercentualAntigo != 0)
            txtHorasDedicadas.SetValue(txtPercentualDedicacao.GetValue() * txtHorasDedicadas.GetValue() / valorPercentualAntigo);
        else
            txtHorasDedicadas.SetValue(0);

        valorPercentualAntigo = txtPercentualDedicacao.GetValue();
    } else {
        if (possuiHoras && possuiPercentual)
            txtHorasDedicadas.SetValue(parseFloat(callbackCusto.cp_HorasDedicadas) * txtPercentualDedicacao.GetValue() / 100);
        else
            txtHorasDedicadas.SetValue(null);
    }
}