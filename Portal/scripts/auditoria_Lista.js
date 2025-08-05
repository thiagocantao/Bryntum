function mostraPopupAuditoria(codigo, operacao) {
    var url = 'auditoria_Insercao.aspx';
    if (operacao == 'U') {
        url = 'auditoria_Atualizacao.aspx';
    }
    url += '?ID=' + codigo;
    var height = Math.max(0, document.documentElement.clientHeight) - 140;
    var width =  Math.max(0, document.documentElement.clientWidth) - 45;

    window.top.showModal(url, 'Auditoria', width, height, '', null);
}