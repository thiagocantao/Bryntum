/// 
/// Data Creação: 1 de Fevereiro 2011
///-----------------------------------------


function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
   if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoContrato;NumeroAditivoContrato;NumeroParcela;ValorPrevisto;DataVencimento;DataPagamento;ValorPago;HistoricoParcela;NumeroContrato;Fornecedor;Objeto', MontaCamposFormulario);
}

function MontaCamposFormulario(valores) {
    try {
        var CodigoContrato = valores[0];
        var NumeroAditivoContrato = valores[1];
        var NumeroParcela = valores[2];
        var ValorPrevisto = valores[3];
        var DataVencimento = valores[4];
        var DataPagamento = valores[5];
        var ValorPago = valores[6];
        var HistoricoParcela = valores[7];
        var NumeroContrato = valores[8];
        var Fornecedor = valores[9];
        var Objeto = valores[10];

        txtNumeroContrato.SetText((NumeroContrato != null ? NumeroContrato.toString() : ""));
        txtNomeFornecedor.SetText((Fornecedor != null ? Fornecedor.toString() : ""));
        mmObjeto.SetText((Objeto != null ? Objeto.toString() : ""));

        txtAditivo.SetText((NumeroAditivoContrato != null ? NumeroAditivoContrato.toString() : "0"));
        txtParcela.SetText((NumeroParcela != null ? NumeroParcela.toString() : "0"));
        txtValorPrevisto.SetText((ValorPrevisto != null ? ValorPrevisto.toString().replace('.', ',') : "0"));
        txtValorPago.SetText((ValorPago != null ? ValorPago.toString().replace('.', ',') : "0"));
        mmHistorico.SetText((HistoricoParcela != null ? HistoricoParcela.toString() : ""));

        if (DataVencimento == null)
            deDataVencimento.SetText("");
        else
            deDataVencimento.SetValue(DataVencimento);
        if (DataPagamento == null)
            deDataPagamento.SetText("");
        else
            deDataPagamento.SetValue(DataPagamento);

        hfGeral.Set("KeyGrid", CodigoContrato + ";" + NumeroAditivoContrato + ";" + NumeroParcela);
    }
    catch (e) { }
}

///------------------------------------
/// Função responsável por preparar os 
/// campos do formulário para receber 
/// um novo registro
///
function LimpaCamposFormulario() {
    try {
        txtAditivo.SetText("");
        txtParcela.SetText("");
        txtValorPrevisto.SetText("0");
        deDataVencimento.SetText("");
        txtValorPago.SetText("0");
        deDataPagamento.SetText("");
        mmHistorico.SetText("");
    }
    catch (e) { }
}

///------------------------------------
/// funções manipulação do pcDadosIncluido.
/// popup indica ação do sistema.
///
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}


/// --------------------------------------------
/// para funcionar o label mostrador de quantos 
/// caracteres faltam para terminar o texto
///
function setMaxLength(textAreaElement, length) {
    textAreaElement.maxlength = length;
    ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
    ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
}

function onKeyUpOrChange(evt) {
    processTextAreaText(ASPxClientUtils.GetEventSource(evt));
}

function processTextAreaText(textAreaElement) {
    var maxLength = textAreaElement.maxlength;
    var text = textAreaElement.value;
    var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
    if (maxLength != 0 && text.length > maxLength)
        textAreaElement.value = text.substr(0, maxLength);
    else
        lblCantCarater.SetText(text.length);
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function mostraPopup(valor) {
    //function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {

    var url = window.top.pcModal.cp_Path + "_projetos/administracao/LancamentosFinanceirosConvenio.aspx";
    url += "?clf=" + valor;
    url += "&tipo=LPV";
    window.top.showModal(url, "Parcelas", screen.width - 200, 500, atualizaGrid, null);
}

function atualizaGrid() {
    gvDados.Refresh();
}