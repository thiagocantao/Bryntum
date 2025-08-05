function AbreSelecaoProdutosConsultor(codigoConsultor) {
    window.top.showModal("./SelecaoProdutosConsultor.aspx?cc=" + codigoConsultor, traducao.CadastroConsultores_selecionar_produtos_do_consultor, 750, 450, "", null);
}

function abreEdicao(codigoConsultor) {
    window.top.showModal("./DetalhesConsultor.aspx?cc=" + codigoConsultor, traducao.CadastroConsultores_detalhes_do_consultor, 960, 500, atualizaGrid, null);
}

function atualizaGrid(param) {
    gvDados.PerformCallback();
}