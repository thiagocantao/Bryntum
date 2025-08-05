// JScript File
// ---- Provavelmente não será necessário alterar as duas próximas funções
function SalvarCamposFormulario() {
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}


// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario() {
    pnCallbackPopup.PerformCallback();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoParametro_PT;TipoDadoParametro;Valor;IndicaControladoSistema;', MontaCamposFormulario);
}

function MontaCamposFormulario(values) {

    var DescricaoParametro_PT = (values[0] != null ? values[0] : "");
    var TipoDadoParametro = (values[1] != null ? values[1] : "");
    var Valor = (values[2] != null ? values[2] : "");
    var IndicaControladoSistema = trim((values[3] != null ? values[3] : ""));

    memoDescricao.SetText(DescricaoParametro_PT);
    txtValorTXT.SetText(Valor);

    if (IndicaControladoSistema == "S") {
        txtValorTXT.SetEnabled(false);
        btnSalvar.SetVisible(false);
    }
    else {
        txtValorTXT.SetEnabled(true);
        btnSalvar.SetVisible(true);
    }
    pcDados.AdjustSize();
}


// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    onClick_btnCancelar();
}

