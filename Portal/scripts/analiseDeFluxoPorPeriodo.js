
function MontaCamposFormulario(valores) {
    var codigoWorkflow = valores[1];
    var codInstanciaWf = valores[0];
    var nomeInstanciaWf = valores[2];

    var url1 = '../_Portfolios/Relatorios/popupRelResumoProcessos.aspx?';
    url1 += 'cwf=' + codigoWorkflow;
    url1 += '&ciwf=' + codInstanciaWf;
    url1 += '&niwf=' + nomeInstanciaWf;

    window.open(url1, 'form', 'resizable=1,width=1020px,height=700px,status=no,menubar=no');
}