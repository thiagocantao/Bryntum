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

    if (Trim(txtDescricaoGrupo.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.cadastroGrupoIndicador_o_grupo_de_indicadores_ser_informado_;
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
    txtDescricaoGrupo.SetText(null);
    ckbIndicaGrupoOperacional.SetChecked(false);
    ckbIndicaGrupoEstrategico.SetChecked(false);
    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoGrupoIndicador;DescricaoGrupoIndicador;IndicaGrupoOperacional;IndicaGrupoEstrategico;IniciaisGrupoControladoSistema;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();
    //'CodigoGrupoIndicador;DescricaoGrupoIndicador;IndicaGrupoOperacional;IndicaGrupoEstrategico;IniciaisGrupoControladoSistema;
    var codigoGrupoIndicador = (values[0] != null ? values[0] : "");
    var descricaoGrupoIndicador = (values[1] != null ? values[1] : "");
    var indicaGrupoOperacional = (values[2] != null ? values[2] : "");
    var IndicaGrupoEstrategico = (values[3] != null ? values[3] : "");
    txtDescricaoGrupo.SetText(descricaoGrupoIndicador);
    ckbIndicaGrupoOperacional.SetChecked(indicaGrupoOperacional == 'S');
    ckbIndicaGrupoEstrategico.SetChecked(IndicaGrupoEstrategico == 'S');


}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtDescricaoGrupo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ckbIndicaGrupoOperacional.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ckbIndicaGrupoEstrategico.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    }
