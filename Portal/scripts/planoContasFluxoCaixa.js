var comando;
var global_conta = '';
var global_modo = '';

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

    if (Trim(txtDescricaoConta.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.planoContasFluxoCaixa_a_descri__o_da_conta_deve_ser_informada_;
    }
    if (cbTipoConta.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.planoContasFluxoCaixa_o_tipo_de_conta_deve_ser_informado_;
    }
    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }
    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(hfGeral.Get("TipoOperacao"));
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(hfGeral.Get("ModoOperacao"));
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    txtDescricaoConta.SetText("");
    txtCodigoReservado.SetText("");
    cbTipoConta.SelectedIndex = -1;
    cbTipoConta.SetText("");
    checkIndicaContaAnalitica.SetChecked(false);
    //ddlContaSuperior.PerformCallback("");
}

function OnFocusedNodeChanged(treeList) {
    treeList.GetNodeValues(treeList.GetFocusedNodeKey(), 'CodigoConta;DescricaoConta;CodigoContaSuperior;CodigoEntidade;IndicaContaAnalitica;CodigoReservadoGrupoConta;Diretoria;Departamento;TipoConta', MontaCamposFormularioTreeList);
}
function MontaCamposFormularioTreeList(values) {

    //          0;             1;                  2;             3;                   4;                        5;        6;           7;       8    
    //CodigoConta;DescricaoConta;CodigoContaSuperior;CodigoEntidade;IndicaContaAnalitica;CodigoReservadoGrupoConta;Diretoria;Departamento;TipoConta;
    var codigoConta = (values[0] != null ? values[0] : "");
    var descricaoConta = (values[1] != null ? values[1] : "");
    var codigoContaSuperior = (values[3] != null ? values[3] : "");
    var codigoReservadoGrupoConta = (values[5] != null ? values[5] : "");
    var tipoConta = (values[8] != null ? values[8] : "");
    var IndicaContaAnalitica = (values[4] != null ? values[4] : "");
    txtDescricaoConta.SetText(descricaoConta);
    txtCodigoReservado.SetText(codigoReservadoGrupoConta);
    cbTipoConta.SetValue(tipoConta);
    checkIndicaContaAnalitica.SetChecked(IndicaContaAnalitica == 'S');
    hfGeral.Set("codigoContaSuperior", codigoContaSuperior);
    hfGeral.Set("codigoContaSelecionada", codigoConta);
}

function abrePopUp(conta, modo) {
    global_conta = conta;
    global_modo = modo;

    //debugger
    hfGeral.Set('TipoOperacao', modo);
    cbTipoConta.SetEnabled(true);
    txtDescricaoConta.SetEnabled(true);
    txtCodigoReservado.SetEnabled(true);    
    tlCentroCustos.GetSelectedNodeValues("DescricaoConta", performAbrePopUp);
}

function performAbrePopUp(valor) {
    var DescricaoConta = (valor != null ? valor : "");
    pcDados.SetHeaderText(DescricaoConta);

    if (global_modo == 'Visualizar') {

        cbTipoConta.SetEnabled(false);
        txtDescricaoConta.SetEnabled(false);
        txtCodigoReservado.SetEnabled(false);
        //ddlContaSuperior.SetEnabled(false);

        pcDados.Show();
    }
    if (global_modo == 'Incluir') {

        LimpaCamposFormulario();
        pcDados.Show();
    }
    if (global_modo == 'Editar') {
        pcDados.Show();
    }
    if (global_modo == 'Cancelar') {
        pcDados.Hide();
    }
    if (global_modo == 'Excluir') {
        window.top.mostraMensagem(traducao.planoContasFluxoCaixa_deseja_realmente_excluir_o_registro_selecionado_, 'confirmacao', true, true, excluiPlanoContas);
    }
}

function excluiPlanoContas() {
    pnCallback.PerformCallback("Excluir");
}




function onClick_btnCancelar() {
    pcDados.Hide();
    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    return true;
}


function onClick_btnSalvar() {
    if (window.validaCamposFormulario) {
        if (validaCamposFormulario() != "") {
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'Atencao', true, false, null);
            return false;
        }
    }

    if (window.SalvarCamposFormulario) {
        if (SalvarCamposFormulario()) {
            pcDados.Hide();
            habilitaModoEdicaoBarraNavegacao(false, gvDados);
            return true;
        }
    }
    else {
        window.top.mostraMensagem(traducao.planoContasFluxoCaixa_o_m_todo_n_o_foi_implementado_, 'Atencao', true, false, null);
    }
}

function onEnd_pnCallback(erro) {
    if (hfGeral.Get("StatusSalvar") == "1") {
        if (hfGeral.Get("TipoOperacao") == "Incluir") {
            hfGeral.Set("TipoOperacao", "Editar");
            //hfGeral.Set("TipoOperacao", TipoOperacao);
        }
        if (window.posSalvarComSucesso)
            window.posSalvarComSucesso();
        else
            onClick_btnCancelar();
    }
    else if (hfGeral.Get("StatusSalvar") == "0") {
        mensagemErro = erro;

        if (hfGeral.Get("TipoOperacao") == "Excluir") {
            // se existe um tratamento de erro especifico da opçao que está sendo executada
            if (window.trataMensagemErro)
                mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
            else // caso contrário, usa o tratamento padrão
            {
                // se for erro de Chave Estrangeira (FK)
                if (mensagemErro != null && mensagemErro.search('REFERENCE') >= 0)
                    mensagemErro = traducao.planoContasFluxoCaixa_o_registro_n_o_pode_ser_exclu_do_pois_est__sendo_utilizado_por_outro;
            }
        }
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
    }
}



function novaDescricao() {
    timedCount(0);
}

function timedCount(tempo) {
    if (tempo == 3) {
        tempo = 0;
        tlCentroCustos.PerformCallback();
    }
    else {
        tempo++;
        t = setTimeout("timedCount(" + tempo + ")", 1000);
    }
}