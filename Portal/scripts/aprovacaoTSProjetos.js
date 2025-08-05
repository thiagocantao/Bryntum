var chave;

function abreTelaMudancaStatus(chavePrimaria, statusTarefa, comentarios, comentariosRecurso) {
    chave = chavePrimaria;

    while (comentarios.indexOf("¥") != -1) {
        comentarios = comentarios.replace("¥", "\n");
    }
    while (comentariosRecurso.indexOf("¥") != -1) {
        comentariosRecurso = comentariosRecurso.replace("¥", "\n");
    }
    txtComentarios.SetText(comentarios);
    txtComentariosRecurso.SetText(comentariosRecurso);
    ddlStatusAssunto.SetValue(statusTarefa);

    pcAprovacao.Show();
}

function mudaStatus() {
    var valorChave = chave + '.' + ddlStatusAssunto.GetValue();
    callbackStatus.PerformCallback(valorChave);
}

function selecionaTodos(valor) {
    gvDados.SelectAllRowsOnPage(valor);
}