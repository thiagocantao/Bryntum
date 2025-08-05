function incluiNovoProjeto() {
    var url = window.top.pcModal.cp_Path + '_Estrategias/mapa/CadastroProjeto.aspx';

    window.top.showModal(url, traducao.ListaProjetosMapa_inclus_o_de_projeto, screen.width - 20, 730, atualizaGrid, null);
}

function atualizaGrid(s) {
    gvDados.PerformCallback();
}
