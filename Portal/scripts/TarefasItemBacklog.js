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

    if (Trim(txtTituloTarefa.GetText()) == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.TarefasItemBacklog_o_da_tarefa_deve_ser_informado_;
    }
    if (txtEsforcoPrevisto.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.TarefasItemBacklog_a_estimativa_deve_ser_informada_;
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

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario()
{
    callbackPopupItemBacklog.PerformCallback(gvDados.cpCodigoItem + '|' + 'incluirTarefasItem|' + gvDados.cpCodigoProjeto + '|' + gvDados.cpCodigoProjetoAgil);
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'TituloItem;EsforcoPrevisto;DetalheItem', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(codigoItem)
{
    callbackPopupItemBacklog.PerformCallback(codigoItem + '|editarTarefasItem' + '|' + gvDados.cpCodigoProjeto + '|' + gvDados.cpCodigoProjetoAgil);
}

function MontaCamposFormularioRO(codigoItem) {
    callbackPopupItemBacklog.PerformCallback(codigoItem + '|editarTarefasItemRO' + '|' + gvDados.cpCodigoProjeto + '|' + gvDados.cpCodigoProjetoAgil);
}




// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    txtTituloTarefa.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtEsforcoPrevisto.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    mmDetalhes.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
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

function finalizaEdicao() {
    gvDados.PerformCallback();
}