var comando;

function SalvarCamposFormulario() {
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}


function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    OnGridFocusedRowChanged(gvDados, false);
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function validaCamposFormulario() {

    mensagemErro_ValidaCamposFormulario = "";
    var contador = 1;
    if (spnNumero.GetText() == "") {
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_Acoes_n_mero_da_a__o_deve_ser_informada_ + "\n";
    }
    if (trim(txtAcao.GetText()) == "") {
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_Acoes_descri__o_da_a__o_deve_ser_informada_ + "\n";
    }
    if (ddlPrograma.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_Acoes_descri__o_do_programa_deve_ser_informada_ + "\n";
    }
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario() {
    spnNumero.SetValue(null);
    txtAcao.SetText("");
    ddlPrograma.SetValue(null);
}


function MontaCamposFormulario(values) {
    var valores = values[0];

    var CodigoAcao = (valores[0] == null) ? "" : valores[0].toString();
    var CodigoEntidade = (valores[1] == null) ? "" : valores[1].toString();
    var CodigoPrograma = (valores[2] == null) ? "" : valores[2].toString();
    var NomePrograma = (valores[3] == null) ? "" : valores[3].toString();
    var NumeroAcao = (valores[4] == null) ? "" : valores[4].toString();
    var NomeAcao = (valores[5] == null) ? "" : valores[5].toString();
    var DataExclusao = (valores[6] == null) ? "" : valores[6].toString();

    spnNumero.SetText(NumeroAcao);
    txtAcao.SetValue(NomeAcao);
    ddlPrograma.SetValue(CodigoPrograma);
    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetSelectedFieldValues('CodigoAcao;CodigoEntidade;CodigoPrograma;NomePrograma;NumeroAcao;NomeAcao;DataExclusao;', MontaCamposFormulario);
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;
    spnNumero.SetEnabled(BoolEnabled);
    txtAcao.SetEnabled(BoolEnabled);
    ddlPrograma.SetEnabled(BoolEnabled);
}
