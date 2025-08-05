function abrePopupPermissoes(codigoContratoPortal) {
    var idObjeto = (codigoContratoPortal != null ? codigoContratoPortal : "-1");
    var tituloObjeto = "Contrato Nº: " + (codigoContratoPortal != null ? codigoContratoPortal : "");

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = screen.width - 100;
    var window_height = screen.height - 200;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;
    window.top.showModal(window.top.pcModal.cp_Path + "_Estrategias/InteressadosObjeto.aspx?ITO=CT&COE=" + idObjeto + "&TOE=" + tituloObjeto + '&AlturaGrid=' + (window_height - 110), traducao.succ_MenuPrincipal_permiss_es, window_width, window_height, '', null);
}

function abrePopupInstrumentosJuridicos(valores) {
    //CodigoInstrumentoJuridico;NumeroInstrumentoJuridico', abrePopupInstrumentosJuridicos)
    var CodigoInstrumentoJuridico = valores[0];
    var NumeroInstrumentoJuridico = valores[1];
    
    var idObjeto = (CodigoInstrumentoJuridico != null ? CodigoInstrumentoJuridico : "-1");
    var tituloObjeto = "Instrumento N&ordm;: " + (NumeroInstrumentoJuridico != null ? NumeroInstrumentoJuridico : "");

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = screen.width - 100;
    var window_height = screen.availHeight - 225;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;
    window.top.showModal(window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/succ_Projetos_popup.aspx?CI=" + CodigoInstrumentoJuridico + '&AlturaGrid=' + (window_height - 110), tituloObjeto, window_width, window_height, '', null);
}