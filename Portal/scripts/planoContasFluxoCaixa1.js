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
        mensagem += "\n" + numAux + ") " + traducao.planoContasFluxoCaixa1_a_descri__o_do_plano_de_contas_deve_ser_informada_;
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
    //desabilitaHabilitaComponentes();
    ddlContaSuperior.PerformCallback("");
}

function OnFocusedNodeChanged(treeList) {
    treeList.GetNodeValues(treeList.GetFocusedNodeKey(), 'CodigoConta;DescricaoConta;CodigoContaSuperior;CodigoEntidade;IndicaContaAnalitica;CodigoReservadoGrupoConta;Diretoria;Departamento;TipoConta', MontaCamposFormularioTreeList);
}

function abrePopUp(conta, modo) {
    //debugger
    hfGeral.Set('TipoOperacao', modo);
    rblTipoConta.SetEnabled(true);
    txtDescricaoConta.SetEnabled(true);
    txtCodigoReservado.SetEnabled(true);
    //ddlContaSuperior.SetEnabled(true);
    if (modo == 'Visualizar') {

        rblTipoConta.SetEnabled(false);
        txtDescricaoConta.SetEnabled(false);
        txtCodigoReservado.SetEnabled(false);
        //ddlContaSuperior.SetEnabled(false);
        pcDados.Show();
    }
    if (modo == 'Incluir') {

        LimpaCamposFormulario();
        pcDados.Show();
    }
    if (modo == 'Editar') {
        pcDados.Show();
    }
    if (modo == 'Cancelar') {
        pcDados.Hide();
    }
    if (modo == 'Excluir') {
        window.top.mostraMensagem(traducao.planoContasFluxoCaixa1_deseja_realmente_excluir_o_registro_selecionado_, 'confirmacao', true, true, excluiPlanoContas);
    }
}

function excluiPlanoContas() {
    pnCallback.PerformCallback("Excluir");
}

function MontaCamposFormularioTreeList(values) {

    desabilitaHabilitaComponentes();
    //          0              1                    3             4                     5                       6                 7           8    
    //CodigoConta;DescricaoConta;CodigoContaSuperior;CodigoEntidade;IndicaContaAnalitica;CodigoReservadoGrupoConta;Diretoria;Departamento;TipoConta;
    var codigoConta = (values[0] != null ? values[0] : "");
    var descricaoConta = (values[1] != null ? values[1] : "");
    var codigoContaSuperior = (values[3] != null ? values[3] : "");
    var codigoReservadoGrupoConta = (values[5] != null ? values[5] : "");
    var tipoConta = (values[8] != null ? values[8] : "");
    txtDescricaoConta.SetText(descricaoConta);
    txtCodigoReservado.SetText(codigoReservadoGrupoConta);
    ddlContaSuperior.SetValue(codigoContaSuperior);
    if (tipoConta != 'DP' && tipoConta != 'DI')
        rblTipoConta.SetValue('CT');
    else
        rblTipoConta.SetValue(tipoConta);
    hfGeral.Set("codigoContaSuperior", codigoContaSuperior);
    ddlContaSuperior.PerformCallback(codigoConta);

}
// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    rblTipoConta.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtDescricaoConta.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtCodigoReservado.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    //ddlContaSuperior.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}


function onClick_btnCancelar() {
    //habilitaModoEdicaoBarraNavegacao(false, gvDados);
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
        window.top.mostraMensagem(traducao.planoContasFluxoCaixa1_o_m_todo_n_o_foi_implementado_, 'Atencao', true, false, null);
    }
}

function onEnd_pnCallback() {
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
        mensagemErro = hfGeral.Get("ErroSalvar");

        if (hfGeral.Get("TipoOperacao") == "Excluir") {
            // se existe um tratamento de erro especifico da opçao que está sendo executada
            if (window.trataMensagemErro)
                mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
            else // caso contrário, usa o tratamento padrão
            {
                // se for erro de Chave Estrangeira (FK)
                if (mensagemErro != null && mensagemErro.indexOf('REFERENCE') >= 0)
                    mensagemErro = traducao.planoContasFluxoCaixa1_o_registro_n_o_pode_ser_exclu_do_pois_est__sendo_utilizado_por_outro;
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
        tlCentroCusto.PerformCallback();
    }
    else {
        tempo++;
        t = setTimeout("timedCount(" + tempo + ")", 1000);
    }
}


//---------------------------ordenação-------------------------------------

function ordenarItems() {
    var listaContaSuperiorTXT = new Array();
    var listaContaSuperiorVAL = new Array();


    for (var i = 0; i < ddlContaSuperior.GetItemCount(); i++) {
        var item = ddlContaSuperior.GetItem(i);
        listaContaSuperiorTXT[i] = item.text;
    }

    if (listaContaSuperiorTXT) {
        listaContaSuperiorTXT.sort();
        insereVetorOrdenado(listaContaSuperiorTXT, ddlContaSuperior);
    }


}

function insereVetorOrdenado(vetorArrayTXT, objCombobox) {

    for (var i = 0; i < vetorArrayTXT.length; i++) {
        /*a chave é o texto e o valor e o codigo*/
        hfOrdena.Set(vetorArrayTXT[i], buscaValorObjListBox(vetorArrayTXT[i], objCombobox));
    }

    objCombobox.BeginUpdate();
    var itemCount = objCombobox.GetItemCount();
    for (var i = 0; i < itemCount; i++) {
        objCombobox.RemoveItem(0);
    }

    for (var i = 0; i < vetorArrayTXT.length; i++) {
        /*o texto mostrado é o vetor ordenado e os codigos sao buscados apartir dos textos*/
        objCombobox.AddItem(vetorArrayTXT[i], hfOrdena.Get(vetorArrayTXT[i]));
    }
    objCombobox.EndUpdate();
    hfOrdena.Clear();
}

function buscaValorObjListBox(valor, objListBox) {
    var itemCount = objListBox.GetItemCount();
    var item1 = null;
    var retorno = "";
    for (var i = 0; i < itemCount; i++) {
        item1 = objListBox.GetItem(i);
        if (item1.text == valor) {
            retorno = item1.value;
        }
    }
    return retorno;

}