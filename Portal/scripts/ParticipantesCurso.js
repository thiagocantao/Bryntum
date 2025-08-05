function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
   mensagemErro_ValidaCamposFormulario = "";
   var numAux = 0;
    var mensagem = "";

    if (Trim(txtParticipante.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ParticipantesCurso_o_nome_do_participante_deve_ser_informado_;
    }

    if (txtCPF.GetValue() != null && valida_cpf(txtCPF.GetValue()) == false) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ParticipantesCurso_o_cpf_informado___inv_lido;
    }

    if (txtEmail.isValid == false) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ParticipantesCurso_o_email_informado___inv_lido_;
    }
    
    if (txtCNPJ.GetValue() != null && valida_cnpj(txtCNPJ.GetValue()) == false) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ParticipantesCurso_o_cnpj_informado___inv_lido_;
    }
    
    if(mensagem != "")
    {
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

function ExcluirTodosRegistros() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    window.top.mostraMensagem(traducao.ParticipantesCurso_deseja_realmente_excluir_todos_os_registros_, 'confirmacao', true, true, ExecutaExcluirTodosRegistros);
}

function ExecutaExcluirTodosRegistros() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback("ExcluirTodos");
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario()
{
    txtParticipante.SetText("");
    txtEmpresaSindicato.SetText("");
    txtSetor.SetValue(null);
    txtCargo.SetText("");
    txtEmail.SetText("");
    txtCPF.SetText("");
    txtCNPJ.SetText("");
    
    desabilitaHabilitaComponentes();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'NomeParticipante;NomeEmpresaSindicato;Cargo;eMail;SegmentoIndustria;CPF;CNPJ', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values)
{
    desabilitaHabilitaComponentes();
    
    var nomeParticipante = (values[0] != null ? values[0]  : "");
    var empresaSindicato = (values[1] != null ? values[1] : "");
    var cargo = (values[2] != null ? values[2] : "");
    var email = (values[3] != null ? values[3] : "");
    var setor = (values[4] != null ? values[4] : "");
    var cpf = (values[5] != null ? values[5] : "");
    var cnpj = (values[6] != null ? values[6] : "");
    txtParticipante.SetText(nomeParticipante);
    txtEmpresaSindicato.SetText(empresaSindicato);
    txtSetor.SetText(setor);
    txtCargo.SetText(cargo);
    txtEmail.SetText(email);
    txtCPF.SetText(cpf);
    txtCNPJ.SetText(cnpj);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    txtParticipante.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtEmpresaSindicato.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtSetor.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCargo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtEmail.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCPF.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCNPJ.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
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

    cpf = getSomenteNumeros(cpf);

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

    cnpj = getSomenteNumeros(cnpj);

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

function getSomenteNumeros(valor)
{
    if (valor == null)
        return '';

    exp = /\-|\.|\/|\(|\)| /g
    return valor.toString().replace(exp, "");
}

function formataCampo(s) {
    var boleanoMascara;

    campoSoNumeros = getSomenteNumeros(s.GetValue());

    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.length;;
    var Mascara = '';

    if (TamanhoMascara == 11) {
        Mascara = '000.000.000-00';
    } else if (TamanhoMascara == 14) {
        Mascara = '00.000.000/0000-00';
    }
    else
        return false;

    for (i = 0; i <= TamanhoMascara; i++) {
        boleanoMascara = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
                                                || (Mascara.charAt(i) == "/"))
        boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(")
                                                || (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))
        if (boleanoMascara) {
            NovoValorCampo += Mascara.charAt(i);
            TamanhoMascara++;
        } else {
            NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
            posicaoCampo++;
        }
    }
    s.SetValue(NovoValorCampo);
    return true;
}