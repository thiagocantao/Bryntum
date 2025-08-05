var ddlAtual;
var myObjectLocal = null;
var posExecutarLocal = null;
var urlModalLocal = "";
var retornoModalLocal = null;

function processaClickBotao(idBotao, codigoWorkflow, codigoInstanciaWf, codigoEtapa, ocorrencia, codigoFluxo) {
    var url = '';
    var titulo = '';
    var largura = screen.width - 10;
    var altura = screen.height - 175;

    if (idBotao == 'G') {
        url = window.top.pcModal.cp_Path + '_Portfolios/GraficoProcessoInterno.aspx?CW=' + codigoWorkflow + '&CI=' + codigoInstanciaWf + '&CF=' + codigoFluxo + '&CP=' + -1 + '&Altura=' + (altura - 190) + '&Largura=' + (largura - 50);
        titulo = 'Gráfico';
    }
    else {
        url = window.top.pcModal.cp_Path + 'wfEngineInterno.aspx?CW=' + codigoWorkflow + '&CI=' + codigoInstanciaWf + '&CE=' + codigoEtapa + '&CS=' + ocorrencia + '&Altura=' + (altura - 190) + '&Largura=' + (largura - 50);
        titulo = 'Etapa';
    }

    abreTela(url, titulo, largura, altura);

}

function abreTela(url, titulo, largura, altura) {
    window.top.showModal2(url + '&PND=S', titulo, largura, altura, atualizaTela, null);
}

function atualizaTela(param) {
    if (param == 'S') {
        gvDados.PerformCallback();
    }
}

function grid_ContextMenu(s, e) {
    if (e.objectType == "header")
        pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
}

function resizeText(incremento) {
    if (document.body.style.fontSize == "") {
        document.body.style.fontSize = "8pt";
    }
    document.body.style.fontSize = parseFloat(document.body.style.fontSize) + incremento + "pt";

    var componentes = ASPxClientControl.GetControlCollection().elements;

    for (i = 0; i < componentes.length; i++)
        componentes.mainElement.style.fontSize = parseFloat(componentes.mainElement.style.fontSize) + incremento + "pt";
}
function onClick(s, e) {
    var codigos = '';
    var qtdSelecionados = gvDados.GetSelectedKeysOnPage().length;

    if (qtdSelecionados == 0) {
        window.top.mostraMensagem(traducao.AssinaturaOficios_nenhum_registro_foi_selecionado_, 'erro', true, false, null);
    } else if (btnAssinar.cp_utilizaAssinaturaDigitalOficio == "N") {
        if (window.confirm('Deseja realizar a assinatura dos ofícios selecionados?'))
            callback.PerformCallback('prosseguirSemAssinaturaDigital');
    } else {
        for (var i = 0; i < qtdSelecionados; i++) {
            var chaves = gvDados.GetSelectedKeysOnPage()[i].split('|');
            var cod = chaves[0];
            if (cod == null || cod == undefined)
                cod = -1;
            codigos += cod.toString();
            if (i + 1 < qtdSelecionados)
                codigos += ',';
        }
        //window.top.showModal(window.top.pcModal.cp_Path + '_CertificadoDigital/assinaturaMultiplosFluxos.aspx?codigos=' + codigos + '&CD=' + gvDados.cp_CD, 'Assinar Ofícios Selecionados', 600, 280, funcaoPosModal, null);
        window.top.showModal(window.top.pcModal.cp_Path + '_CertificadoDigital/AssinaturaDigitalArquivosOficios.aspx?PassaFluxo=S&codigos=' + codigos + '&CD=' + gvDados.cp_CD, 'Assinar Ofícios Selecionados', 600, 280, funcaoPosModal, null);
    }
}

function funcaoPosModal(x) {
    if (x == 'OK') {
        window.top.mostraMensagem(traducao.AssinaturaOficios_assinatura_realizada_com_sucesso_, 'sucesso', false, false, null);
        gvDados.PerformCallback();
    }
    else if (x == 'ERRO') {

    }
    else if (x == 'CANCELAR' || x == '' || x == null) {
        callback.PerformCallback('cancelar');
    }
}

function OnSelectionChanged(s, e) {
    var values = s.cp_ArrayOficios;
    if (e.isChangedOnServer) {
        var cont = 0;
        var codigos = [];
        for (var i = 0; i < values.length; i++) {
            if (s.IsRowSelectedOnPage(i)) {
                var codigoOficio = values[i][0];
                var oficioAssinado = values[i][1];
                if (oficioAssinado == "Sim")
                    codigos[cont++] = codigoOficio;
            }
        }
        if (codigos.length > 0)
            s.UnselectRowsByKey(codigos);
    }
    else {
        var oficioAssinado = values[e.visibleIndex][1];
        if (e.isSelected && oficioAssinado == "Sim")
            s.UnselectRows(e.visibleIndex);
    }
}