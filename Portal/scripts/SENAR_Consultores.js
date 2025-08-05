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

    if (ddlUF.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.SENAR_Consultores_a_regional_deve_ser_informada_;
    }

    if (Trim(txtNome.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.SENAR_Consultores_o_nome_completo_deve_ser_informado_;
    }

    cpf = txtCPF.GetText();
    cpf = replaceAll(cpf, '.', '');
    cpf = replaceAll(cpf, '-', '');

    if (Trim(cpf) != "" && cpf != null && valida_cpf(cpf) == false) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.SENAR_Consultores_o_cpf_informado___inv_lido_;
    }

    if (txtEmail.GetText() != "" && validateEmail(txtEmail.GetText()) == false) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.SENAR_Consultores_o_email_informado___inv_lido_;
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
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
    ddlUF.SetValue(null);
    txtFuncao.SetText('');
    txtNome.SetText('');
    txtCPF.SetText('');
    txtTelefone.SetText('');
    txtEmail.SetText('');
    
    desabilitaHabilitaComponentes();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetSelectedFieldValues('SiglaUF;Funcao;NomeConsultor;CPF;Telefone;Email;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(valores)
{
    var values = valores[0];

    desabilitaHabilitaComponentes();
    
    var siglaUF     = (values[0] != null ? values[0]  : "");
    var funcao      = (values[1] != null ? values[1] : "");
    var nome        = (values[2] != null ? values[2] : "");
    var cpf         = (values[3] != null ? values[3] : "");
    var telefone    = (values[4] != null ? values[4] : "");
    var email       = (values[5] != null ? values[5] : "");

    ddlUF.SetValue(siglaUF);
    txtFuncao.SetText(funcao);
    txtNome.SetText(nome);
    txtCPF.SetText(cpf);
    txtTelefone.SetText(telefone);
    txtEmail.SetText(email);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    ddlUF.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtFuncao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtNome.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCPF.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtTelefone.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtEmail.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
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

function validateEmail(elementValue) {
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(elementValue);
}

function replaceAll(origem, antigo, novo) {
    var teste = 0;
    while (teste == 0) {
        if (origem.indexOf(antigo) >= 0) {
            origem = origem.replace(antigo, novo);
        }
        else
            teste = 1;
    }
    return origem;
}