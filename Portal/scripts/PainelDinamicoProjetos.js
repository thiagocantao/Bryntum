var timeOut = null;
var pagina = 1;

function executaEventoFecharConfiguracoes() {
    if(timeOut != null)
        clearTimeout(timeOut);

    if (ASPxClientEdit.ValidateGroup('MKE', true)) {
        pcConfiguracoes.Hide();
        mudaPainel1(gvDados.GetFocusedRowIndex());
        callBack.PerformCallback();
    }
}

function mudaPainel1(index) {

    if ( (index<0) || (index >= gvDados.GetVisibleRowsOnPage()) )
        index = 0;

    pagina = 1;

    var codigoProjeto = gvDados.GetRowKey(index);

    document.getElementById("frmVC").src = 'Frm1_PainelDinamicoProjetos.aspx?CP=' + codigoProjeto;

    timeOut = setTimeout('mudaPainel2(' + index.toString() + ');', txtTempo.GetValue() * 1000);
}

function mudaPainel2(index) {
    
    var codigoProjeto = gvDados.GetRowKey(index);

    document.getElementById("frmVC").src = 'Frm2_PainelDinamicoProjetos.aspx?CP=' + codigoProjeto + '&CC=' + ddlVisaoInicial.GetValue() + '&Page=' + pagina;
        
    if(pagina >= parseInt(callBack.cp_NumeroPaginas))
        timeOut = setTimeout('mudaPainel1(' + (index + 1).toString() + ');', txtTempo.GetValue() * 1000);
    else
        timeOut = setTimeout('mudaPainel2(' + index.toString() + ');', txtTempo.GetValue() * 1000);

    pagina++;
}

function vrfHabilitacaoBtnOk() {
    btnOk.SetEnabled((gvDados.GetVisibleRowsOnPage()>0));
    return;
}
