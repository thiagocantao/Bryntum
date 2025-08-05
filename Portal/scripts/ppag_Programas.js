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
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_Programas_n_mero_do_programa_deve_ser_informado_ + "\n";
    }
    if (trim(txtPrograma.GetText()) == "" ){
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_Programas_descri__o_do_programa_deve_ser_informado_;
    }
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario() {
    spnNumero.SetValue(null);
    txtPrograma.SetText("");
}


function MontaCamposFormulario(values) {
    var valores = values[0];
    var CodigoPrograma = (valores[0] == null) ? "" : valores[0].toString();
    var CodigoEntidade = (valores[1] == null) ? "" : valores[1].toString();
    var NumeroPrograma = (valores[2] == null) ? "" : valores[2].toString();
    var NomePrograma = (valores[3] == null) ? "" : valores[3].toString();
    var DataExclusao = (valores[4] == null) ? "" : valores[4].toString();

    spnNumero.SetText(NumeroPrograma);
    txtPrograma.SetValue(NomePrograma);

    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetSelectedFieldValues('CodigoPrograma;CodigoEntidade;NumeroPrograma;NomePrograma;DataExclusao', MontaCamposFormulario);
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;
    spnNumero.SetEnabled(BoolEnabled);
    txtPrograma.SetEnabled(BoolEnabled);
}
