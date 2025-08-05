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

    if (txtNomePessoa.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.SENAR_Contratos_o_nome_do_fornecedor_deve_ser_informado_;
    }
    if (rbTipoPessoa.GetValue() == "J" && txtCNPJ.GetText() != "" && txtCNPJ.GetValue() != null) {
        if (valida_cnpj(txtCNPJ.GetValue()) == false) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.SENAR_Contratos_o_cnpj_informado___inv_lido_;
        }
    }
    if (rbTipoPessoa.GetValue() == "F" && txtCPF.GetText() != "" && txtCPF.GetValue() != null) {
        if (valida_cpf(txtCPF.GetValue()) == false) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.SENAR_Contratos_o_cpf_informado___inv_lido_;
        }
    }

    if (txtEmail.GetText() != "") {
        if (validateEmail(txtEmail.GetText()) == false) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.SENAR_Contratos_o_email_informado___inv_lido_;
        }
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    if (gvContratos.batchEditApi.HasChanges())
        gvContratos.UpdateEdit();
    else
        pnCallback.PerformCallback(TipoOperacao);

    return false;
}

function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario()
{
    txtNomePessoa.SetText("");
    txtNumeroProcesso.SetText("");
    rbTipoPessoa.SetValue("J");
    alteraTipoPessoa("J");
    txtCNPJ.SetText("");
    txtCPF.SetText("");

    txtTelefone.SetText("");
    txtNomeContato.SetText("");

    txtInformacoesContato.SetText("");
    txtInformacoesContato.Validate();

    txtEmail.SetText("");
    
    desabilitaHabilitaComponentes();

    gvContratos.PerformCallback();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetSelectedFieldValues('CodigoFornecedorABC;OutrasInformacoes;NomeContato;NomeFornecedor;NumeroDocPessoa;TelefonePessoa;TipoPessoa;Email;NumeroProcesso', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(valores)
{
    var values = valores[0];
    var codigoPessoa = (values[0] != null ? values[0] : "");
    var informacaoContato = (values[1] != null ? values[1] : "");
    var nomeContato = (values[2] != null ? values[2] : "");
    var nomePessoa = (values[3] != null ? values[3] : "");
    var numeroCNPJCPF = (values[4] != null ? values[4] : "");
    var telefonePessoa = (values[5] != null ? values[5] : "");
    var tipoPessoa = (values[6] != null ? values[6] : "");
    var email = (values[7] != null ? values[7] : "");
    var numeroProcesso = (values[8] != null ? values[8] : "");

    txtNomePessoa.SetText(nomePessoa);
    rbTipoPessoa.SetValue(tipoPessoa);
    alteraTipoPessoa(tipoPessoa);

    if (tipoPessoa == 'J')
        txtCNPJ.SetText(numeroCNPJCPF);
    else
        txtCPF.SetText(numeroCNPJCPF);

    txtTelefone.SetText(telefonePessoa);
    txtNomeContato.SetText(nomeContato);
    txtInformacoesContato.SetText(informacaoContato);
    txtEmail.SetText(email);
    txtNumeroProcesso.SetText(numeroProcesso);

    gvContratos.PerformCallback();

    desabilitaHabilitaComponentes();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    txtNomePessoa.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    rbTipoPessoa.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCNPJ.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCPF.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtTelefone.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtNomeContato.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtInformacoesContato.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtEmail.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtNumeroProcesso.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    btnSalvar.SetVisible(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
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

function novaLinha() {
    gvContratos.AddNewRow();
}

var visibleIndex = 0;
var indexNovo = 0;

function onStartEdit(s, e) {
    visibleIndex = e.visibleIndex;
    e.cancel = true;
    pcDadosContrato.Show();
}
function onShown(s, e) {
    txtNumeroContrato.SetValue(gvContratos.batchEditApi.GetCellValue(visibleIndex, "NumeroContrato"));
    deInicio.SetValue(gvContratos.batchEditApi.GetCellValue(visibleIndex, "DataInicioVigencia"));
    deTermino.SetValue(gvContratos.batchEditApi.GetCellValue(visibleIndex, "DataTerminoVigencia"));
    txtValorGlobal.SetValue(gvContratos.batchEditApi.GetCellValue(visibleIndex, "ValorGlobalContrato"));
}
function onAcceptClick(s, e) {
    gvContratos.batchEditApi.SetCellValue(visibleIndex, "NumeroContrato", txtNumeroContrato.GetValue());
    gvContratos.batchEditApi.SetCellValue(visibleIndex, "DataInicioVigencia", deInicio.GetValue());
    gvContratos.batchEditApi.SetCellValue(visibleIndex, "DataTerminoVigencia", deTermino.GetValue());
    gvContratos.batchEditApi.SetCellValue(visibleIndex, "ValorGlobalContrato", txtValorGlobal.GetValue());
    pcDadosContrato.Hide();
}
function onCloseButtonClick(s, e) {
    if (visibleIndex <= -1 && gvContratos.batchEditApi.GetCellValue(visibleIndex, "CodigoContrato") == null)
        gvContratos.batchEditApi.ResetChanges(visibleIndex);

    pcDadosContrato.Hide();
}