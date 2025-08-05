function mostraPopupLancamentoFinanceiro(valores) {
    var codigoLancamentoFinanceiro = valores[0];
    var iniciaisControleLancamento = valores[1];
    var url = window.top.pcModal.cp_Path + "_projetos/administracao/LancamentosFinanceirosConvenio.aspx";
    url += "?clf=" + codigoLancamentoFinanceiro;
    url += "&tipo=" + iniciaisControleLancamento;
    window.top.showModal(url, "Pagamento", screen.width - 200, 500, atualizaGrid, null);
}

function atualizaGrid() {
    pnCallback.PerformCallback("atualizaGrid");
}