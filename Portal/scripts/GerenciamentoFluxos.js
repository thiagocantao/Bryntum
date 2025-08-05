var codigoWf = -1;

function tratarSimbolos(s, e)
{
    if (e.htmlEvent.keyCode == 39 || e.htmlEvent.keyCode == 192)
    {
        e.htmlEvent.keyCode=180; 
        return e.htmlEvent.keyCode;
    }
}


function abreEdicaoEtapas(s, e) {
    var codigo = s.GetRowKey(e.visibleIndex);
    var url = './GerenciamentoFluxosEtapas.aspx?CWF=' + codigoWf + '&id=' + codigo;

    window.top.showModal(url, traducao.GerenciamentoFluxos_configura__o_etapa, 850, 450, function (e) { gvEtapas.PerformCallback() }, null);
}

function abreEdicaoConectores(s, e) {
    var chaves = s.GetRowKey(e.visibleIndex).split('|');
    
    var url = './GerenciamentoFluxosConectores.aspx?CWF=' + codigoWf + '&idEtapaOrigem=' + chaves[0] + '&idEtapaDestino=' + chaves[1];

    window.top.showModal(url, traducao.GerenciamentoFluxos_configura__o_conector, 850, 520, function (e) { gvConectores.PerformCallback() }, null);
}

