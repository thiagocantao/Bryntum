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

    if (Trim(txtTipoDeProjeto.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.cadastroTiposProjetos_o_tipo_de_projeto_deve_ser_informado_;
    }

    if (ddlCalendarioBase.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.cadastroTiposProjetos_CalendarioBase_deve_ser_informado_;
    }

    if (ddlCalendarioBase.GetValue() == '-1') {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.cadastroTiposProjetos_CalendarioBase_deve_ser_informado_;
    }

    if (txtAtraso.GetNumber() < 0) {
        if (!txtAtraso.GetNumber() == null) {
            var nullpode = ""; 
        }
        else {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.cadastroTiposProjetos_ToleranciaMaiorQZero;
        }
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
    txtTipoDeProjeto.SetText("");
    txtAtraso.SetText("");
    ddlCalendarioBase.SetSelectedIndex(0);
    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoTipoProjeto;TipoProjeto;IndicaControladoSistema;IndicaTipoProjeto;CodigoTipoAssociacao;DescricaoTipoAssociacao;CodigoCalendarioPadrao;DescricaoCalendario;ToleranciaInicioLBProjeto;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {

    var CodigoTipoProjeto = (values[0] != null ? values[0] : "");
    var TipoProjeto = (values[1] != null ? values[1] : "");
    var IndicaControladoSistema = (values[2] != null ? values[2] : "");
    var IndicaTipoProjeto = (values[3] != null ? values[3] : "");
    var CodigoTipoAssociacao = (values[4] != null ? values[4] : "");
    var DescricaoTipoAssociacao = (values[5] != null ? values[5] : "");
    var CodigoCalendarioPadrao = (values[6] != null ? values[6] : "-1");
    var DescricaoCalendario = (values[7] != null ? values[7] : "");
    var ToleranciaInicioLBProjeto = (values[8] != null ? values[8] : "");

    txtTipoDeProjeto.SetText(TipoProjeto);
    txtAtraso.SetText(ToleranciaInicioLBProjeto)
    ddlCalendarioBase.SetValue(CodigoCalendarioPadrao);

    desabilitaHabilitaComponentes(IndicaControladoSistema);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes(IndicaControladoSistema) {
    txtTipoDeProjeto.SetEnabled((window.TipoOperacao && TipoOperacao != "Consultar" && IndicaControladoSistema == "Não") || window.TipoOperacao &&  TipoOperacao == "Incluir");
    txtAtraso.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar"  );
    ddlCalendarioBase.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");

}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}