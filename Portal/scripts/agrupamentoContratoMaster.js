function validaCamposFormulario() {

    var contador = 0
    var mensagemErro_ValidaCamposFormulario = "";
    if (isNaN(txtCCBM.GetText()) || txtCCBM.GetText() == "") {
        contador++;
        mensagemErro_ValidaCamposFormulario += contador + ") " + traducao.agrupamentoContratoMaster_campo_ccbm_n_o_est__preenchido_com_valor_decimal_v_lido + "\n";
    }
    if (isNaN(txtIMPSA.GetText()) || txtIMPSA.GetText() == "") {
        contador++;
        mensagemErro_ValidaCamposFormulario += contador + ") " + traducao.agrupamentoContratoMaster_campo_impsa_n_o_est__preenchido_com_valor_decimal_v_lido + "\n";
    }
    if (isNaN(txtEPBM.GetText()) || txtEPBM.GetText() == "") {
        contador++;
        mensagemErro_ValidaCamposFormulario += contador + ") " + traducao.agrupamentoContratoMaster_campo_epbm_n_o_est__preenchido_com_valor_decimal_v_lido + "\n";
    }
    if (isNaN(txtELM.GetText()) || txtELM.GetText() == "") {
        contador++;
        mensagemErro_ValidaCamposFormulario += contador + ") " + traducao.agrupamentoContratoMaster_campo_elm_n_o_est__preenchido_com_valor_decimal_v_lido + "\n";
    }
    return mensagemErro_ValidaCamposFormulario;
}

function posSalvarComSucesso(mensagemResultante) {
    if (mensagemResultante == "") {
        mostraPopupMensagemGravacao(traducao.agrupamentoContratoMaster_dados_gravados_com_sucesso_);
    }
    else {
        mostraPopupMensagemGravacao(mensagemResultante);
    }    
}

//-------
function mostraPopupMensagemGravacao(acao) {
    lblAcaoGravacao.SetText(acao);
    pcUsuarioIncluido.Show();
    setTimeout('fechaTelaEdicao();', 1500);
}

function fechaTelaEdicao() {
    pcUsuarioIncluido.Hide();
}