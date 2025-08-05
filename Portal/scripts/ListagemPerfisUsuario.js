/*
 * Criação: 05/04/2011
 * Autor  : Alejandro
 
 MODIFICAÇÕES
 
 08/04/2011 :: Alejandro : Alterações de funções para compatibilidade com Mozilla (Versão: 3.6.7)
 
 */
// JScript File

var TipoOperacao = "Consultar";

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

function onClickDetalheAcesso(grid, e) {
    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    //btnSalvar.SetVisible(false);
    btnSalvarAux = document.getElementById("pnCallback_pcPermissoes_TPCFm1_btnSalvar");   //btnSalvar.SetVisible(true);
    btnSalvarAux.style.visibility = 'hidden';
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'HerdaPermissoesObjetoSuperior;Perfis;IniciaisTipoObjeto;CodigoObjeto;CodigoObjetoPai', MostraGridPermissaoDetalhe);
}

function onClickEditarAcesso(grid, e) {
    TipoOperacao = "Editar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    pnCallbackDetalhe.PerformCallback();
}

/*---------------------------------------------------------------------------
Problema de execução de Mozilla com o metodo [btnSalvar.SetVisible()], quiças seja por nao ter alcançe ao componente
fazendo referença a que o id fica composto por a profundidade dos outros componentes que o contem.

Obs. : (08/04/2011) Propiedade style.visibility > 'hidden' (invisivel) ou 'visible' 
---------------------------------------------------------------------------*/
function onClickPersonalizarPermissoes(s, e) {
    TipoOperacao = "Editar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    btnSalvarAux = document.getElementById("pnCallback_pcPermissoes_TPCFm1_btnSalvar");   //btnSalvar.SetVisible(true);
    btnSalvarAux.style.visibility = 'visible';
    gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'HerdaPermissoesObjetoSuperior;Perfis;IniciaisTipoObjeto;CodigoObjeto;CodigoObjetoPai', MostraGridPermissaoDetalhe);
}

function onClickExcluirAcesso(s, e) {
    TipoOperacao = "Excluir";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    if (confirm('Deseja realmente excluir o registro?')) {
        TipoOperacao = "Excluir";
        hfGeral.Set("TipoOperacao", TipoOperacao);
        ExcluirRegistroSelecionado();
    }
}

/*---------------------------------------------------------------------------
    Descrição: 
    
    HerdaPermissoesObjetoSuperior   = 0
    Perfis                          = 1
    IniciaisTipoObjeto              = 2
    
    Obs.:(08/04/2011) Problema de execução de Mozilla com o metodo [lbListaPerfisSelecionados.mainElement.innerText.toString()]
         quiças seja por nao ter alcançe ao componente fazendo referença a que o id fica composto por a profundidade dos
         outros componentes que o contem.

        > innerText retorna o conteudo da lista, debería se tratar a formatação para marcar mas estetico
          a separação da lista.
---------------------------------------------------------------------------*/
function MostraGridPermissaoDetalhe(values) {
    var herda;
    var perfis;
    var iniciaisTO = (values[2] != null ? values[2] : "");
    var codigoObjeto = (values[3] != null ? values[3] : "-1");
    var codigoObjetoPai = (values[4] != null ? values[4] : "0");

    // quando for mostrar a grid em uma "Edição", os valores virão da tela anterior
    if (TipoOperacao == "Editar") {
        herda = checkHerdarPermissoes.GetChecked() ? 'S' : 'N';
        //perfis  = lbListaPerfisSelecionados.mainElement.innerText.toString();
        var lbListaPerfisSelecionadosAux = document.getElementById("pnCallback_pcDados_pnCallbackDetalhe_lbListaPerfisSelecionados");
        perfis = lbListaPerfisSelecionadosAux.innerText;
    }
    else {
        herda = (values[0] != null ? values[0] : "");
        perfis = (values[1] != null ? values[1] : "");
    }

    if (window.hfGeral && hfGeral.Contains("HerdaPermissoes")) hfGeral.Set("HerdaPermissoes", herda);
    if (window.hfGeral && hfGeral.Contains("TipoOperacao")) hfGeral.Set("TipoOperacao", TipoOperacao);
    if (window.hfGeral && hfGeral.Contains("NomeUsuarioInteressado")) lblCaptionInteressado.SetText(hfGeral.Get("NomeUsuarioInteressado"));
    if (window.hfGeral && hfGeral.Contains("iniciaisTO")) hfGeral.Set("iniciaisTO", iniciaisTO);
    if (window.hfGeral && hfGeral.Contains("CodigoObjetoAssociado")) hfGeral.Set("CodigoObjetoAssociado", codigoObjeto);
    if (window.hfGeral && hfGeral.Contains("CodigoObjetoPai")) hfGeral.Set("CodigoObjetoPai", codigoObjetoPai);

    lblCaptionPerfil.SetText(perfis);
    lblCaptionHerda.SetText(herda == 'S' ? 'Sim' : 'Não');

    if (TipoOperacao == "Editar")
        pcPermissoes.Show();
    else {
        if (pcPermissoes.GetVisible())
            CallBackGvPermissoes(iniciaisTO) // na hora de mostrar os detalhes, o item menu é o mesmo do tipo de objeto
        else
            pcPermissoes.Show();
    }
}

/*-------------------------------------------------
Descrição: colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
-------------------------------------------------*/
function LimpaCamposFormulario() {
    var tOperacao = ""

    try {// Função responsável por preparar os campos do formulário para receber um novo registro
        if (window.hfGeral && hfGeral.Contains("CodigoObjetoAssociado")) hfGeral.Set("CodigoObjetoAssociado", "-1");
        if (window.hfGeral && hfGeral.Contains("CodigoUsuarioPermissao")) hfGeral.Set("CodigoUsuarioPermissao", "-1");
        if (window.hfGeral && hfGeral.Contains("HerdaPermissoes")) hfGeral.Set("HerdaPermissoes", "");
        if (window.hfGeral && hfGeral.Contains("CodigosPerfisSelecionados")) hfGeral.Set("CodigosPerfisSelecionados", "-1");

        checkHerdarPermissoes.SetChecked(false);
        lblCaptionInteressado.SetText("");
        lblCaptionPerfil.SetText("");
        lblCaptionHerda.SetText("");

        if (TipoOperacao != "Incluir") callbackGeral.PerformCallback("CerrarSession");
    } catch (e) { }
}

/*-------------------------------------------------
<summary>
</summary>
-------------------------------------------------*/
function onClick_Cancelar(s, e) {
    pcDados.Hide();
    callbackGeral.PerformCallback("CerrarSession");
}

/*-------------------------------------------------
<summary>
Evento que se dispara ao fazer click no menu nbAssociacao.
Utiliza o nome do item do menu, para enviarlo ao callback da grid gvPermissoes
para recargar las permissões correspondientes.
</summary>
-------------------------------------------------*/
function onClick_MenuPermissao(s, e) {
    e.processOnServer = false;

    var textoItem = getIniciaisAssociacaoFromTextoMenu(e.item.name);
    CallBackGvPermissoes(textoItem);
}

function CallBackGvPermissoes(itemMenu) {
    if (window.hfGeral && window.hfGeral.Contains('itemMenu'))
        hfGeral.Set('itemMenu', itemMenu);

    gvPermissoes.PerformCallback("");
}

function getIniciaisAssociacaoFromTextoMenu(textoMenu) {
    /* EN UN ST PR ME PP TM OB IN CT */
    var retorno = "";

    if (textoMenu == "mnEntidade") retorno = "EN";
    else if (textoMenu == "mnUnidades") retorno = "UN";
    else if (textoMenu == "mnEstrategia") retorno = "ST";
    else if (textoMenu == "mnProjeto") retorno = "PR";
    else if (textoMenu == "mnMapas") retorno = "ME";
    else if (textoMenu == "mnPerspectiva") retorno = "PP";
    else if (textoMenu == "mnTema") retorno = "TM";
    else if (textoMenu == "mnObjetivo") retorno = "OB";
    else if (textoMenu == "mnIndicador") retorno = "IN";
    else if (textoMenu == "mnContrato") retorno = "CT";
    else if (textoMenu == "mnDemandaComplexa") retorno = "DC";
    else if (textoMenu == "mnDemandaSimple") retorno = "DS";
    else if (textoMenu == "mnProcesso") retorno = "PC";
    else if (textoMenu == "mnEquipe") retorno = "EQ";

    return retorno;
}

function verificarDadosPreenchidos() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    return mensagemErro_ValidaCamposFormulario;
}

//------------------------------------------------------------ funções relacionadas com a ListBox
var delimitadorLocal = ";";

function UpdateButtons() {
    btnADD.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaPerfisDisponivel.GetSelectedItem() != null);
    btnADDTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaPerfisDisponivel.GetItemCount() > 0);
    btnRMV.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaPerfisSelecionados.GetSelectedItem() != null);
    btnRMVTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaPerfisSelecionados.GetItemCount() > 0);
    capturaCodigosProjetosSelecionados();
}

function capturaCodigosProjetosSelecionados() {
    var CodigosPerfisSelecionados = "";

    for (var i = 0; i < lbListaPerfisSelecionados.GetItemCount(); i++) {
        CodigosPerfisSelecionados += lbListaPerfisSelecionados.GetItem(i).value + delimitadorLocal;
    }
    hfGeral.Set("CodigosPerfisSelecionados", CodigosPerfisSelecionados);

    if (lbListaPerfisSelecionados.GetItemCount() > 0) {
        btnSeleccionarPermissao.SetEnabled(true);
        btnSalvarPerfis.SetEnabled(true);
    }
    else {
        btnSeleccionarPermissao.SetEnabled(false);
        btnSalvarPerfis.SetEnabled(false);
    }
}

function onValueChanged_lbListaPerfisSelecionados(s, e) {
    if (s.GetItemCount() > 0) {
        btnSeleccionarPermissao.SetEnabled(true);
        btnSalvarPerfis.SetEnabled(true);
    }
    else {
        btnSeleccionarPermissao.SetEnabled(false);
        btnSalvarPerfis.SetEnabled(false);
    }
}

//------------------------------------------------------------ CHECKBOX PERMISSÕES
function clicaConceder(codigo, valor, op) {
    var checkConcedido = document.getElementById('CheckConcedido');
    var checkExtensivel = document.getElementById('CheckDelegavel');
    var checkNegar = document.getElementById('CheckNegado');

    if (op == "C") { }
    else if (op == "D") { }
    else if (op == "N") { }

    callbackConceder.PerformCallback(codigo + ';' + valor + ';' + op);
}

function clicaRestrincao(valor) {
    gvDados.PerformCallback(valor);
}

function mostraPopupMensagemGravacao(acao) {
    window.top.mostraMensagem(acao, 'sucesso', false, false, null);
}

function onClick_Cancelar() {
    habilitaModoEdicaoBarraNavegacao(false, gvDados);
    pcDados.Hide();
    pcPermissoes.Hide();
    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    return true;
}