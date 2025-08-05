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
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_SubAcoes_n_mero_da_sub_a__o_deve_ser_informada_ + "\n";
    }
    if (ddlPrograma.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_SubAcoes_programa_deve_ser_informado_ + "\n";
    }
    if (ddlAcao.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_SubAcoes_a__o_deve_ser_informada_ + "\n";
    }
    if (trim(txtSubAcao.GetText()) == "") {
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.ppag_SubAcoes_descri__o_da_sub_a__o_deve_ser_informada_ + "\n";
    }

    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario() {
    spnNumero.SetValue(null);
    ddlPrograma.SetValue(null);
    ddlAcao.SetValue(null);
    txtSubAcao.SetText("");
    
}


function MontaCamposFormulario(values) {
    var valores = values[0];

    var CodigoSubAcao = (valores[0] == null) ? "" : valores[0].toString();
    var CodigoEntidade = (valores[1] == null) ? "" : valores[1].toString();
    var CodigoAcao = (valores[2] == null) ? "" : valores[2].toString();
    var NomeAcao = (valores[3] == null) ? "" : valores[3].toString();
    var NumeroSubAcao = (valores[4] == null) ? "" : valores[4].toString();
    var NomeSubAcao = (valores[5] == null) ? "" : valores[5].toString();
    var DataExclusao = (valores[6] == null) ? "" : valores[6].toString();
    var NomePrograma = (valores[7] == null) ? "" : valores[7].toString();;
    var CodigoPrograma = (valores[8] == null) ? "" : valores[8].toString();

    spnNumero.SetValue(NumeroSubAcao);

    ddlPrograma.SetValue(CodigoPrograma);

    ddlAcao.SetValue(CodigoAcao);

    txtSubAcao.SetText(NomeSubAcao);

    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetSelectedFieldValues('CodigoSubAcao;CodigoEntidade;CodigoAcao;NomeAcao;NumeroSubAcao;NomeSubAcao;DataExclusao;NomePrograma;CodigoPrograma', MontaCamposFormulario);
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;

    spnNumero.SetEnabled(BoolEnabled);
    ddlPrograma.SetEnabled(BoolEnabled);
    ddlAcao.SetEnabled(BoolEnabled);
    txtSubAcao.SetEnabled(BoolEnabled);
   
}
