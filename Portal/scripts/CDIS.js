function desbloquearProjeto() {
    gvDados.PerformCallback();
}

function getGraficoZoom(tipoGrafico, largura, altura, paramJSON, paramDiv) {
    FusionCharts.ready(function () {
        var cSatScoreChart = new FusionCharts({
            type: tipoGrafico,
            renderAt: paramDiv,
            width: largura,
            height: altura,
            dataFormat: 'json',
            dataSource: paramJSON
        }).render();
    });
}
// mostrar o gráfico em uma janela exclusiva
function f_zoomGrafico(caminho, Titulo, graficoSWF, tipo) {
    window.top.showModal(caminho + "?TIT=" + Titulo + "&Tipo=" + tipo + "&SWF=" + graficoSWF, Titulo, null, null, '', null);
}

// mostrar os gráficos de bullets em uma janela exclusiva
function f_zoomBullets(caminho, titulo, grafico002_1_jsonzoom, grafico002_2_jsonzoom, grafico002_3_jsonzoom) {
    window.top.showModal(caminho + "?XML1=" + "" + "&XML2=" + "" + "&XML3=" + "" + "&grafico002_1_jsonzoom=" + grafico002_1_jsonzoom + "" + "&grafico002_2_jsonzoom=" + grafico002_2_jsonzoom + "" + "&grafico002_3_jsonzoom=" + grafico002_3_jsonzoom + "" , titulo, null, null, '', null);
}