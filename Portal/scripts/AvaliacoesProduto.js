function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}

function abreQuestoes(codigoAvaliacao, codigoEdicaoAvaliacao, somenteLeitura) {
    var url = window.top.pcModal.cp_Path + '_PDA/QuestoesAvaliacao.aspx?CodigoProduto=' + gvDados.cp_CodigoProduto + '&CodigoAvaliacao=' + codigoAvaliacao +'&CodigoEdicaoAvaliacao=' + codigoEdicaoAvaliacao + 
                '&IndicaAvaliador=' + rbTipoAvaliador.GetValue() + '&Altura=' + (screen.height - 320) + '&RO=' + somenteLeitura + '&CWF=' + gvDados.cp_CWF + '&CIWF=' + gvDados.cp_CIWF;
    window.top.showModal(url, "Avaliação Curso", 1000, screen.height - 240, funcaoPosModal, null);
}

function funcaoPosModal(valor) {
        gvDados.PerformCallback();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {

}