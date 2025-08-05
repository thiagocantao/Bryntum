// JScript File
var urlFrmAnexosContrato = '';
var atualizarURLAnexos = '';
var comando = '';
var map = {
    "â": "a", "Â": "A", "à": "a", "À": "A", "á": "a", "Á": "A", "ã": "a", "Ã": "A",
    "ê": "e", "Ê": "E", "è": "e", "È": "E", "é": "e", "É": "E",
    "î": "i", "Î": "I", "ì": "i", "Ì": "I", "í": "i", "Í": "I",
    "õ": "o", "Õ": "O", "ô": "o", "Ô": "O", "ò": "o", "Ò": "O", "ó": "o", "Ó": "O",
    "ü": "u", "Ü": "U", "û": "u", "Û": "U", "ú": "u", "Ú": "U", "ù": "u", "Ù": "U", "ç": "c", "Ç": "C"
};

function removerAcentos(s)
{
    return s.replace(/[\W\[\] ]/g, function (a) { return map[a] || a })
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



function atualizaItensBacklog(valores)
{
    hfGeral.Set("TipoOperacao", "Editar");
    CodigoItem = (valores[0] != null ? valores[0] : "");    
    callbackPopupItemBacklog.PerformCallback(CodigoItem + '|' + 'editar|' + tlDados.cp_CodigoProjeto);
}

function getDescricaoItemSuperior(CodigoItem)
{
    callbackDescricaoIBSuperior.PerformCallback(CodigoItem);
}

function excluiItemBacklog(valores) {
    var CodigoItem = valores[0];
    var IndicaSeItemBacklogTemTarefaAssociada = valores[1];


    var mensagemConfirmacao = '';


    var funcObj = {
        funcaoClickOK: function (cod) {
            callbackTela.PerformCallback("Excluir|" + cod);//s.GetRowValues(s.GetFocusedRowIndex(), "CodigoItem;IndicaSeItemBacklogTemTarefaAssociada;", excluiItemBacklog);
        }
    };


    if (IndicaSeItemBacklogTemTarefaAssociada == null)
    {
        mensagemConfirmacao = traducao.ItensBacklog_deseja_realmente_excluir_o_item_de_backlog;
    }
    else
    {
        mensagemConfirmacao = 'Todas as tarefas associadas a este ítem serão excluídas,\n Deseja realmente excluir o ítem de backlog?';
    }
        
    window.top.mostraConfirmacao(mensagemConfirmacao, function () { funcObj['funcaoClickOK'](CodigoItem) }, null);    
}

function incluiItemBacklog() {
    hfGeral.Set("TipoOperacao", "Incluir");
    callbackPopupItemBacklog.PerformCallback(tlDados.cpCodigoItemSelecionado + '|incluir|' + tlDados.cp_CodigoProjeto);
}




// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo


// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
    pcDados.Hide();
}

function desabilitaHabilitaComponentes(IndicaBloqueioItem) {
    var BoolEnabled = hfGeral.Get("TipoOperacao") == "Editar" || hfGeral.Get("TipoOperacao") == "Incluir" ? true : false;
    txtTituloItem.SetEnabled(BoolEnabled);
    txtDetalheItem.SetEnabled(BoolEnabled);
    txtEsforco.SetEnabled(BoolEnabled);
    ddlComplexidade.SetEnabled(BoolEnabled);
    txtImportancia.SetEnabled(BoolEnabled);
    ddlStatus.SetEnabled(BoolEnabled);
    ddlClassificacao.SetEnabled(BoolEnabled);

    ddlRecurso.SetEnabled(BoolEnabled);
    dtDataAlvo.SetEnabled(BoolEnabled);
    spnReceitaPrevista.SetEnabled(BoolEnabled);
    tagBox.SetEnabled(BoolEnabled);
    //txtPercentualConcluido.SetEnabled(BoolEnabled);
    btnSalvar.SetVisible(BoolEnabled);

}

function showDetalhe()
{
    OnGridFocusedRowChanged(gvDados, true);
    btnSalvar.SetVisible(false);
    hfGeral.Set("TipoOperacao", "Consultar");
    pcDados.Show();
}

function verificarDadosPreenchidos() {
    var mensagemError = "";
    var retorno = true;
    var numError = 0;
    
    if (txtTituloItem.GetText() == "") {
        mensagemError += ++numError + ") O título do item deve ser informado!\n";
        retorno = false;
    }
    if (txtDetalheItem.GetText() == "") {
        mensagemError += ++numError + ") Os detalhes do item devem ser informados!\n";
        retorno = false;
    }
    if (txtEsforco.GetText() == "") {
        mensagemError += ++numError + ") O Esforço do item deve ser informado!\n";
        retorno = false;
    }

   if (ddlComplexidade.GetSelectedIndex() == -1)
   {
        mensagemError += ++numError + ") A complexidade do item deve ser informada!\n";
        retorno = false;
   }
    if (txtImportancia.GetText() == "")
    {
       mensagemError += ++numError + ") A importância do item deve ser informada!\n";
        retorno = false;
    }

    if (ddlStatus.GetSelectedIndex() == -1 && ddlClassificacao.cp_Visivel != 'N') {
        mensagemError += ++numError + ") O status do item deve ser informado!\n";
        retorno = false;
    }
    if (!retorno)
        window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);

    return retorno;
}

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

function podeMudarAba(s, e) {
    if (e.tab.index == 0) {
        return false;
    }
    else if (hfGeral.Get("TipoOperacao") == 'Incluir') {
        window.top.mostraMensagem("Para ter acesso a opção \"" + e.tab.GetText() + "\" é obrigatório salvar as informações da opção \"" + tabControl.GetTab(0).GetText() + "\"", 'Atencao', true, false, null);
        return true;
    }

    return false;
}


function atualizaGridItensBacklog() {
    tlDados.Refresh();
}

function verifcaSeTemTarefasAssociadas(codigoItem)
{
    //alert('passou pela função verifcaSeTemTarefasAssociadas codigo : ' + codigoItem);
    callbackVerificaSeTemTarefasAssociadas.PerformCallback(codigoItem);
}