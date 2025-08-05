var comando = "";
function MontaCamposFormulario(valor) {

    var codigoModeloFormulario = valor;
    var url = './EditaIniciaisFormularios_popup.aspx?CMF=' + codigoModeloFormulario;
    url += '&alt=' + 490;
    url += '&larg=' + 900;
    window.top.showModal(url, "Iniciais", 900, 490, '', null);
}

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}
