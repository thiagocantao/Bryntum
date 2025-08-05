
var tipoEdicao = '';

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}

function editaTarefa() {
    tipoEdicao = 'E';
    gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoAlertaTarefa;NomeTarefa;', getCodigoAssociacaoTarefas);
}

function consultaTarefa() {
    tipoEdicao = 'C';
    gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoAlertaTarefa;NomeTarefa;', getCodigoAssociacaoTarefas);
}

function excluiTarefa() {
    window.top.mostraMensagem(traducao.ListaTarefasAlerta_deseja_remover_a_associa__o_selecionada_, 'confirmacao', true, true, removeAssociacao);
}

function removeAssociacao() {
    gvDados.PerformCallback('X');
}

function getCodigoAssociacaoTarefas(values) {

    var codigoAlertaTarefa = values[0];
    var NomeTarefa = values[1];
    chamaPopUp(tipoEdicao, codigoAlertaTarefa, NomeTarefa);
}

function chamaPopUp(tipoEdicaoParam, codigoAlertaTarefa, NomeTarefa) {
    var somenteLeitura = tipoEdicaoParam == 'C' ? 'S' : 'N';

    var url = './AssociacaoTarefasAlerta.aspx?CAT=' + codigoAlertaTarefa + '&RO=' + somenteLeitura + "&CP=" + gvDados.cp_CodigoProjeto + "&CA=" + gvDados.cp_CodigoAlerta + "&TE=" + tipoEdicaoParam + "&NT=" + NomeTarefa;
    frmModal.location.href = url;
    pcModal.Show();

}

function fechaModal() {
    pcModal.Hide();
}
