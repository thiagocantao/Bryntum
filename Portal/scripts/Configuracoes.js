// JScript File

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

function validaCamposFormulario() {   // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var conta = 1;
    if (txtAno.GetText() == "" || (txtAno.GetText().toString().length < 4)) {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.Configuracoes_ano___inv_lido_ou_n_o_foi_informado_ + "\r\n";
    }
    if (ddlEditavel.GetValue() == null || ddlEditavel.GetValue() == "") {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.Configuracoes_op__o_de_ano_edit_vel_deve_ser_informado_ + "\r\n";
    }
    if (ddlTipoEdicao.GetValue() == null || ddlTipoEdicao.GetValue() == "") {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.Configuracoes_Periodicidade_deve_ser_informada + "\r\n";
    }
    if (ddlAnoAtivo.GetValue() == null || ddlAnoAtivo.GetValue() == "") {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.Configuracoes_op__o_de_ano_ativo_deve_ser_informado_ + "\r\n";
    }
    if (ddlMetaEditavel.GetValue() == null || ddlMetaEditavel.GetValue() == "") {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.Configuracoes_op__o_de_meta_edit_vel_deve_ser_informado_ + "\r\n";
    }
    if (ddlResultadoEditavel.GetValue() == null || ddlResultadoEditavel.GetValue() == "") {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.Configuracoes_op__o_de_resultado_edit_vel_deve_ser_informado_ + "\r\n";
    }
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario() {
    txtAno.SetText("");
    ddlEditavel.SetValue("");
    ddlTipoEdicao.SetValue("");

    ddlAnoAtivo.SetValue("");
    ddlMetaEditavel.SetValue("");
    ddlResultadoEditavel.SetValue("");

    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'Ano;CodigoEntidade;IndicaAnoPeriodoEditavel;IndicaTipoDetalheEdicao;IndicaAnoAtivo;IndicaMetaEditavel;IndicaResultadoEditavel;CodigoPeriodicidade;', MontaCamposFormulario);
}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    var Ano = values[0].toString();
    var IndicaEditavel = values[2].toString();
    var IndicaTipoEdicao = values[3].toString();

    var IndicaAnoAtivo = values[4].toString();
    var IndicaMetaEditavel = values[5].toString();
    var IndicaResultadoEditavel = values[6].toString();
    var CodigoPeriodicidade = values[7].toString();

    txtAno.SetText(Ano);
    ddlEditavel.SetValue(IndicaEditavel);
    ddlTipoEdicao.SetValue(IndicaTipoEdicao);

    ddlTipoEdicao.SetValue(CodigoPeriodicidade);
    ddlTipoEdicao.SetText(IndicaTipoEdicao);

    ddlAnoAtivo.SetValue(IndicaAnoAtivo);
    ddlMetaEditavel.SetValue(IndicaMetaEditavel);
    ddlResultadoEditavel.SetValue(IndicaResultadoEditavel);
}


// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    onClick_btnCancelar();
}


// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    //var BoolEnabled = hfGeral.Get("TipoOperacao") == "Editar" ? true : false;

    if ("Editar" == TipoOperacao || "Consultar" == TipoOperacao || "" == TipoOperacao)
        txtAno.SetEnabled(false);
    else
        txtAno.SetEnabled(true);
    if ("Consultar" == TipoOperacao || "" == TipoOperacao) {
        ddlEditavel.SetEnabled(false);
        ddlTipoEdicao.SetEnabled(false);

        ddlAnoAtivo.SetEnabled(false);
        ddlMetaEditavel.SetEnabled(false);
        ddlResultadoEditavel.SetEnabled(false);
    }
    else {
        ddlEditavel.SetEnabled(true);
        ddlTipoEdicao.SetEnabled(true);

        ddlAnoAtivo.SetEnabled(true);
        ddlMetaEditavel.SetEnabled(true);
        ddlResultadoEditavel.SetEnabled(true);
    }
}

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}