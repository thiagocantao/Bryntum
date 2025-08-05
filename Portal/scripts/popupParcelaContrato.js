function validaCamposFormulario() {
    var mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 1;

    if (spNumeroAditivo.GetValue() == null || spNumeroAditivo.GetValue() <= -1) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.popupParcelaContrato_o_numero_do_aditivo_deve_ser_informado_e_deve_ser_um_numero_maior_que__1_ + "\n";
    }
    if (spNumeroParcela.GetValue() == null || spNumeroParcela.GetValue() <= -1) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.popupParcelaContrato_o_numero_da_parcela_deve_ser_informado_e_deve_ser_um_n_mero_maior_que__1_ + "\n";
    }
    if (dtVencimento.GetValue() == null || dtVencimento.GetText() == "") {
        var aliasVencimento = labelVencimentoParcelaContrato.GetText();
        aliasVencimento = aliasVencimento.replace(":", "");
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.popupParcelaContrato_o_campo_ + " \"" + aliasVencimento + "\" " + traducao.popupParcelaContrato_deve_ser_informado_ + "\n";
    }
    if (spValorPrevisto.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.popupParcelaContrato_o_valor_previsto_deve_ser_informado_ + "\n";
    }
    if (spValorPago.GetValue() > 0.0) {
        if (dtPagamento.GetValue() == null || dtPagamento.GetText() == "") {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.popupParcelaContrato_o_campo____data_de_pagamento___deve_ser_informado__ + "\n";
        }
    }
    if (spRetencao.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.popupParcelaContrato_o_valor_da_reten__o_deve_ser_informado_ + "\n";
    }
    return mensagemErro_ValidaCamposFormulario;
}

function AbrePopupIniciativasAssociadasParcela(codigoContrato, numeroAditivoContrato, numeroParcela) {
    var url = window.top.pcModal.cp_Path + '_Projetos/Administracao/pda_AssociacaoParcelaContratoIniciativa.aspx?cc=' + codigoContrato + "&nac=" + numeroAditivoContrato + "&np=" + numeroParcela;
    window.top.showModal2(url, 'Associar iniciativas Ã  parcela', 900, 450, AtualizaQuantidadeIniciativasAssociadas);
    window.top.myObject = { contrato: txtNumeroContrato.GetText(), numAditivo: numeroAditivoContrato, numParcela: numeroParcela, valor: parseFloat(spValorPrevisto.GetValue()) };
}

function AtualizaQuantidadeIniciativasAssociadas(retorno) {
    if (retorno) {
        linkIniciativasAssociadasParcela.SetText(retorno + traducao.popupParcelaContrato__iniciativa_s__associada_s____parcela);
    }
    window.top.myObject = null;
}   