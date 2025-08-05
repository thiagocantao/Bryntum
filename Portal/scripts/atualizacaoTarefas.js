
function abreDados(tipo, indexLinha) 
{
    if(tipo == 'D')
    {
        gvDados.GetRowValues(indexLinha, 'CodigoAtribuicao;TipoTarefa;', abreDatelhes);
    }    
}

function abreDatelhes(values)
{
    if(values[0][1].toString() == 'P')
    {   
        var urlDetalhes = "";
        
        if(gvDados.cp_UtilizaMSProject == "S")
            urlDetalhes = './DetalhesTSMSProject.aspx?CA=' + values[0][0]; 
        else
            urlDetalhes = './DetalhesTS.aspx?CA=' + values[0][0];
        
        window.top.showModal(urlDetalhes, 'Detalhes', 810, 550, finalizaEdicao, null);  
    }
    else
        window.top.showModal('./DetalhesTDL.aspx?CTDL=' + values[0][0], traducao.atualizacaoTarefas_detalhes, 980, 420, finalizaEdicao, null);    
}

function finalizaEdicao(retorno)
{
    if(retorno == 'S')
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

function enviaTarefasParaAprovacao() {
    callBack.PerformCallback();
}