
function abreDados(tipo, indexLinha) 
{
    if(tipo == 'D')
    {
        gvDados.GetRowValues(indexLinha, 'CodigoAtribuicao;TipoTarefa;', abreDatelhes);
    }    
}

function abreDatelhes(values)
{
    if(values[1].toString() == 'P')
    {   
        var urlDetalhes = "";
        
        if(gvDados.cp_UtilizaMSProject == "S")
            urlDetalhes = './DetalhesTSMSProject.aspx?CA=' + values[0];
        else
            urlDetalhes = '../Tarefas/DetalhesTS.aspx?CA=' + values[0];
        
        window.top.showModal(urlDetalhes, traducao.TarefasAtualizacao_detalhes, 1000, 590, finalizaEdicao, null);  
    }
    else
        window.top.showModal('../Tarefas/DetalhesTDL.aspx?CTDL=' + values[0], traducao.TarefasAtualizacao_detalhes, 1000, 460, finalizaEdicao, null);    
}

function finalizaEdicao(retorno)
{
    gvDados.PerformCallback();
}

function verificaBotoes(s) {
    var currentRow = s.GetFocusedRowIndex(), isRowSelect = !!s.GetVisibleRowsOnPage() && !s.IsGroupRow(currentRow);

    menuGvDados.GetItemByName("btnEditar").SetEnabled(isRowSelect);
    menuGvDados.GetItemByName("btnExcluir").SetEnabled(isRowSelect);
    s.Focus();
}

function grid_ContextMenu(s, e) {
    if (e.objectType == "header")
        pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
}

function abreTarefasEquipe() {
    window.top.showModal('./SelecaoTarefasEquipe.aspx', traducao.TarefasAtualizacao_tarefas_da_equipe, 1000, 460, finalizaEdicao, null); 
}

function enviaTarefasParaAprovacao()
{
    callBack.PerformCallback();
}