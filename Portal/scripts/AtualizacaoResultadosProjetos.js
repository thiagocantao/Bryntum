function OnGridFocusedRowChanged(grid, indexLinha) {    
        grid.GetRowValues(indexLinha, 'CodigoMetaOperacional;CodigoIndicador;NomeIndicador;SiglaUnidadeMedida;CodigoPeriodicidade;CasasDecimais;CodigoProjeto', abreAtualizacao);
}

function abreAtualizacao(valores) {
    var codigoMeta = valores[0];
    var codigoIndicador = valores[1];
    var indicador = valores[2];
    var unidadeMedida = valores[3];
    var periodicidade = valores[4];
    var casasDecimais = (valores[5] == null ? 0 : valores[5]);
    var codigoProjeto = valores[6];

    urlMetas = './DadosProjeto/FrmResultadosIndicador.aspx?CodigoMeta=' + codigoMeta + '&CodigoIndicador=' + codigoIndicador + '&CasasDecimais=' + casasDecimais + '&SiglaUnidadeMedida=' + unidadeMedida + '&NomeIndicador=' + indicador + "&Periodicidade=" + periodicidade + "&CodigoProjeto=" + codigoProjeto;

    window.top.showModal(urlMetas, traducao.AtualizacaoResultadosProjetos_atualiza__o_do_indicador, screen.width - 40, 365, funcaoPosModal, null);
}

function funcaoPosModal() {
    gvDados.PerformCallback('Atualizar');
}
