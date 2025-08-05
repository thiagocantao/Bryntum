
function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (txtNomeFantasia.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroFornecedores_o_nome_fantasia_deve_ser_informado_;
    }
    if (rbTipoPessoa.GetValue() == "J" && txtCNPJ.GetText() != "" && txtCNPJ.GetValue() != null) {
        if (valida_cnpj(txtCNPJ.GetValue()) == false) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.CadastroFornecedores_o_cnpj_informado___inv_lido_;
        }
    }
    if (rbTipoPessoa.GetValue() == "F" && txtCPF.GetText() != "" && txtCPF.GetValue() != null) {
        if (valida_cpf(txtCPF.GetValue()) == false) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.CadastroFornecedores_o_cpf_informado___inv_lido_;
        }
    }
    if (ddlMunicipio.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroFornecedores_o_municipio_deve_ser_informado_;
    }
    if (txtEmail.GetText() != "") {
        if (validateEmail(txtEmail.GetText()) == false) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.CadastroFornecedores_o_email_informado___inv_lido_;
        }
    }

    if (ckbCliente.GetChecked() == false && ckbFornecedor.GetChecked() == false && ckbParticipe.GetChecked() == false) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.E_preciso_indicar_os_papeis_da_pessoa_Cliente_Fornecedor_ou_Participe;
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
    txtNomePessoa.SetText("");
    txtNomeFantasia.SetText("");
    rbTipoPessoa.SetValue("J");
    alteraTipoPessoa("J");
    txtCNPJ.SetText("");
    txtCPF.SetText("");
    ckbCliente.SetChecked(false);
    ckbFornecedor.SetChecked(false);
    ckbParticipe.SetChecked(false);
    txtEnderecoPessoa.SetText("");
    txtEnderecoPessoa.Validate();

    ddlUF.SetValue(null);
    ddlMunicipio.SetValue(null);
    txtTelefone.SetText("");
    txtNomeContato.SetText("");

    txtInformacoesContato.SetText("");
    txtInformacoesContato.Validate();

    ddlRamoAtividade.SetValue(null);
    txtEmail.SetText("");

    txtComentarios.SetText("");
    txtComentarios.Validate();

    ddlMunicipio.PerformCallback();
    desabilitaHabilitaComponentes();

}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoPessoa;CodigoMunicipioEnderecoPessoa;EnderecoPessoa;InformacaoContato;NomeContato;NomePessoa;NumeroCNPJCPF;TelefonePessoa;TipoPessoa;CodigoRamoAtividade;Email;Comentarios;NomeFantasia;SiglaUF;IndicaCliente;IndicaFornecedor;TemContratoCliente;TemContratoFornecedor;IndicaParticipe;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var codigoPessoa = (values[0] != null ? values[0] : "");
    var codigoMunicipioEnderecoPessoa = (values[1] != null ? values[1] : "");
    var enderecoPessoa = (values[2] != null ? values[2] : "");
    var informacaoContato = (values[3] != null ? values[3] : "");
    var nomeContato = (values[4] != null ? values[4] : "");
    var nomePessoa = (values[5] != null ? values[5] : "");
    var numeroCNPJCPF = (values[6] != null ? values[6] : "");
    var telefonePessoa = (values[7] != null ? values[7] : "");
    var tipoPessoa = (values[8] != null ? values[8] : "");
    var codigoRamoAtividade = (values[9] != null ? values[9] : null);
    var email = (values[10] != null ? values[10] : "");
    var comentarios = (values[11] != null ? values[11] : "");
    var nomeFantasia = (values[12] != null ? values[12] : "");
    var siglaUF = (values[13] != null ? values[13] : "");

    txtNomePessoa.SetText(nomePessoa);
    txtNomeFantasia.SetText(nomeFantasia);
    rbTipoPessoa.SetValue(tipoPessoa);
    ckbCliente.SetChecked(values[14].toString() == "S");
    ckbFornecedor.SetChecked(values[15].toString() == "S");
    ckbParticipe.SetChecked(values[18].toString() == "S");
    alteraTipoPessoa(tipoPessoa);

    ckbCliente.SetEnabled(values[16].toString() == "N");
    ckbFornecedor.SetEnabled(values[17].toString() == "N");

    if (tipoPessoa == 'J')
        txtCNPJ.SetText(numeroCNPJCPF);
    else
        txtCPF.SetText(numeroCNPJCPF);

    ddlUF.SetValue(siglaUF);

    txtEnderecoPessoa.SetText(enderecoPessoa);
    txtEnderecoPessoa.Validate();

    txtTelefone.SetText(telefonePessoa);
    txtNomeContato.SetText(nomeContato);

    txtInformacoesContato.SetText(informacaoContato);
    txtInformacoesContato.Validate();

    ddlRamoAtividade.SetValue(codigoRamoAtividade);
    txtEmail.SetText(email);

    txtComentarios.SetText(comentarios);
    txtComentarios.Validate();

    ddlMunicipio.PerformCallback(codigoMunicipioEnderecoPessoa);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtNomePessoa.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtNomeFantasia.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    rbTipoPessoa.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCNPJ.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCPF.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtEnderecoPessoa.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlUF.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlMunicipio.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtTelefone.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtNomeContato.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtInformacoesContato.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlRamoAtividade.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtEmail.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtComentarios.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ckbCliente.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ckbFornecedor.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ckbParticipe.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao() {
    onClick_btnCancelar();
}

function alteraTipoPessoa(valor) {
    if (valor == 'J') {
        lblTipoPessoa.SetText('CNPJ:');
        txtCNPJ.SetVisible(true);
        txtCPF.SetVisible(false);
    }
    else {
        lblTipoPessoa.SetText('CPF:');
        txtCNPJ.SetVisible(false);
        txtCPF.SetVisible(true);
    }
}

function validateEmail(elementValue) {
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(elementValue);
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

function valida_cnpj(cnpj) {
    var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
    digitos_iguais = 1;
    if (cnpj.length < 14 && cnpj.length < 15)
        return false;
    for (i = 0; i < cnpj.length - 1; i++)
        if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
            digitos_iguais = 0;
            break;
        }
    if (!digitos_iguais) {
        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;
        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;
        return true;
    }
    else
        return false;
} 