window.name = "lov";

function fecharPopUp(valor) {
    if (valor == "") {
        if (window.showModalDialog)
            window.returnValue = valor;
        else if (window.opener != null && window.opener.retornoPopUp != null)
            window.opener.retornoPopUp = valor;
        try{
            window.top.fechaModal();
        }
        catch(e){
            window.close();
        }
        
        
    }
    else {
        if (gvResultado.GetFocusedRowIndex() >= 0) {
            gvResultado.GetRowValues(gvResultado.GetFocusedRowIndex(), 'ColunaValor;ColunaNome', MontaCamposFormulario);
        }
    }
}

function MontaCamposFormulario(valores) {
    var ColunaValor = (valores[0] == null) ? "" : valores[0].toString();
    var ColunaNome = (valores[1] == null) ? "" : valores[1].toString();

    if (window.showModalDialog)
        window.returnValue = ColunaValor + ";" + ColunaNome;
    else if (window.opener != null && window.opener.retornoPopUp != null) {
        window.opener.retornoPopUp = ColunaValor + ";" + ColunaNome;
    }
    window.top.myObject = ColunaValor + ";" + ColunaNome;
    try{
        window.top.fechaModal();
    } catch (e) {
        window.close();
    }
    

}

function atualizaTelaPai() {
    if (window.showModalDialog == null && window.opener.atualizaDadosLov)
        window.opener.atualizaDadosLov();
}