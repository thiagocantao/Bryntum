function btnSalvarPropOcupante_Click(s, e) {
    var msg = ValidaCamposPropOcupante();
    if (msg == "") {
        callback.PerformCallback("");
        existeConteudoCampoAlterado = false;
    }
    else {
        window.top.mostraMensagem(msg, 'Atencao', true, false, null);
    }
}

function habilitaPainelConjuge(sim) {
    if (sim == false) {
        txtNomeConjuge.SetText('');
        dtNascimentoConjuge.SetValue(null);
        ddlNacionalidadeConjuge.SetValue(null);
        txtPaisConjuge.SetText('');
        ddlUFConjuge.SetValue(null);
        ddlMunicipioConjuge.SetValue(null);
        txtProfissaoConjuge.SetText('');
        txtCPFConjuge.SetText('');
        //ckbSabeAssinarConjuge.SetEnabled(sim);
        ddlTipoDocumentoConjuge.SetValue(null);
        txtNumeroDocumentoConjuge.SetText('');
        txtOrgaoExpeditorConjuge.SetText('');
        txtNomePaiConjuge.SetText('');
        txtNomeMaeConjuge.SetText('');
        ddlEstadoCivilConjuge.SetValue(null);
        txtCertidaoConjuge.SetText('');
        dtEmissaoConjuge.SetValue(null);
        txtLivroConjuge.SetText('');
        txtFolhasConjuge.SetText('');
        txtCartorioConjuge.SetText('');
    }
    txtNomeConjuge.SetEnabled(sim);
    dtNascimentoConjuge.SetEnabled(sim);
    ddlNacionalidadeConjuge.SetEnabled(sim);
    txtPaisConjuge.SetEnabled(sim);
    ddlUFConjuge.SetEnabled(sim);
    ddlMunicipioConjuge.SetEnabled(sim);
    txtProfissaoConjuge.SetEnabled(sim);
    txtCPFConjuge.SetEnabled(sim);
    //ckbSabeAssinarConjuge.SetEnabled(sim);
    ddlTipoDocumentoConjuge.SetEnabled(sim);
    txtNumeroDocumentoConjuge.SetEnabled(sim);
    txtOrgaoExpeditorConjuge.SetEnabled(sim);
    txtNomePaiConjuge.SetEnabled(sim);
    txtNomeMaeConjuge.SetEnabled(sim);
    ddlEstadoCivilConjuge.SetEnabled(sim);
    txtCertidaoConjuge.SetEnabled(sim);
    dtEmissaoConjuge.SetEnabled(sim);
    txtLivroConjuge.SetEnabled(sim);
    txtFolhasConjuge.SetEnabled(sim);
    txtCartorioConjuge.SetEnabled(sim);
}

function ValidaCamposPropOcupante() {
    var msg = ""

    var estCivilPessoa = rblIndicaEstadoCivilPessoa.GetValue();
    if (txtNome.GetText() == "") {
        msg += traducao.frmProprietarioOcupante_o_campo__nome__deve_ser_informado_ + "\n";
    }
    if (dtNascimento.GetValue() == null) {
        msg += traducao.frmProprietarioOcupante_o_campo__data_de_nascimento__deve_ser_informado_ + "\n";
    }
    if (estCivilPessoa == null || estCivilPessoa == "") {
        msg += traducao.frmProprietarioOcupante_o_campo__estado_civil__deve_ser_informado_ + "\n";
    }
    else {
        var eSepOuDivOuSoltOuViuvo = (estCivilPessoa == 'D' || estCivilPessoa == 'S' || estCivilPessoa == 'SJ' || estCivilPessoa == 'V');
        if (!eSepOuDivOuSoltOuViuvo && txtNomeConjuge.GetText() == "") {
            msg += traducao.frmProprietarioOcupante_o_campo__nome_do_conjuge__deve_ser_informado_ + "\n";
        }
    }
    if (txtCPF.GetText() != "") {
        if (valida_cpf(txtCPF.GetText()) == false) {
            msg += traducao.frmProprietarioOcupante_o_campo__cpf____inv_lido_ + "\n";        
        }
    }
    if (txtCPFConjuge.GetText() != "") {
        if (valida_cpf(txtCPFConjuge.GetText()) == false) {
            msg += traducao.frmProprietarioOcupante_o_campo__cpf_do_conjuge____inv_lido_ + "\n";
        }
    }
    
    
    //IndicaNacionalidadePessoa
    if (ddlIndicaNacionalidade.GetValue() == "") {
        msg += traducao.frmProprietarioOcupante_o_campo__nacionalidade__deve_ser_informado_ + "\n";
    }
    



    
    //IndicaNacionalidadeConjuge

    if (ddlNacionalidadeConjuge.GetValue() == "") {
        msg += traducao.frmProprietarioOcupante_o_campo__nacionalidade_do_conjuge__deve_ser_informado_ + "\n";
    }
    return msg;
}

function valida_cpf(cpf) {
    var numeros, digitos, soma, i, resultado, digitos_iguais;
    digitos_iguais = 1;
    if (cpf.length < 11)
        return false;
    for (i = 0; i < cpf.length - 1; i++)
        if (cpf.charAt(i) != cpf.charAt(i + 1)) {
            digitos_iguais = 0;
            break;
        }
    if (!digitos_iguais) {
        numeros = cpf.substring(0, 9);
        digitos = cpf.substring(9);
        soma = 0;
        for (i = 10; i > 1; i--)
            soma += numeros.charAt(10 - i) * i;
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;
        numeros = cpf.substring(0, 10);
        soma = 0;
        for (i = 11; i > 1; i--)
            soma += numeros.charAt(11 - i) * i;
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;
        return true;
    }
    else
        return false;
}

function ProcessaResultadoCallback(s, e) {
    var result = e.result;
    if (result && result.length && result.length > 0) {
        if (result.substring(0, 1) == "I") {
            var activeTabIndex = pageControl.GetActiveTabIndex();
            //window.location = "./propostaDeIniciativa_003.aspx?CP=" + result.substring(1) + "&tab=" + activeTabIndex;
        }
        window.top.mostraMensagem(traducao.frmProprietarioOcupante_altera__es_salvas_com_sucesso_, 'sucesso', false, false, null);
    }
}