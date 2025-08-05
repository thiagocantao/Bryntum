function MontaCampos(values) {

    pcDetalhesMedicao.SetHeaderText('Contrato: ' + values[1]);
    /*lbFornecedor.SetText(values[3]);
    lbObjeto.SetText(values[4].substring(0,150));
    lbValor.SetText(number_format(values[5], 2, ',', '.'));
    lbDataInicio.SetText(values[6]);
    lbDataTermino.SetText(values[7]);
    lbValorMedidoNoMes.SetText(number_format(values[10], 2, ',', '.'));
    lbValorMedidoAteMes.SetText(number_format(values[11], 2, ',', '.'));
    lbSaldo.SetText(number_format( (values[5]-values[11]-values[10]) , 2, ',', '.'));*/

    txtFornecedor.SetText(values[3]);
    txtObjeto.SetText(values[4].substring(0, 150));
    txtValor.SetText(number_format(values[5], 2, ',', '.'));
    txtDataInicio.SetText(values[6]);
    txtDataTermino.SetText(values[7]);
    txtValorMedidoNoMes.SetText(number_format(values[10], 2, ',', '.'));
    txtValorMedidoAteMes.SetText(number_format(values[11], 2, ',', '.'));
    txtSaldo.SetText(number_format((values[5] - values[11] - values[10]), 2, ',', '.'));

}

function ShowPcMudaStatus(values) {
   // pcMudaStatus.SetHeaderText('Contrato: ' + values[1] + '   -   Medição: ' + values[2]);
}

function number_format(number, decimals, dec_point, thousands_sep) {
    var n = number, c = isNaN(decimals = Math.abs(decimals)) ? 2 : decimals;
    var d = dec_point == undefined ? "," : dec_point;
    var t = thousands_sep == undefined ? "." : thousands_sep, s = n < 0 ? "-" : "";
    var i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
}

//----------- Mensagem modificação com sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (callBackSalvar.cp_Status == "1") {
        pcMudaStatus.Hide();
        gvMedicao.PerformCallback();
    }
    
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}


function getToolTip(valor, elemento) {
    pcToolTip.SetPopupElementID(elemento);
    lblToolTip.SetText(valor);
    pcToolTip.Show();
}

function escondeToolTip() {
    pcToolTip.Hide();
}

function abreAnexos() {
    var urlAnexos = "../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=MD&ID=" + pnGeral.cp_CodigoMedicao + "&ALT=370";

    window.top.showModal(urlAnexos, traducao.ListaMedicoes_anexos_da_medi__o, 900, 420, "", null);
}

function abreMedicao(codigoMedicao) {
    
    var urlMedicao = "./detalhesMedicao.aspx?CodigoMedicao=" + codigoMedicao;

    window.top.showModal(urlMedicao, traducao.ListaMedicoes_medi__o, screen.width - 30, screen.height - 230, atualizaGvMedicao, null);
}

function atualizaGvMedicao(lParam) {
    if (lParam == 'S') {
        gvMedicao.PerformCallback();
    }
}

   