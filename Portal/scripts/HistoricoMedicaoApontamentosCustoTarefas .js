var codStatusReport;
var abreRelatorio = 'N';
var popUp;

var camposVisiveis = new Array();

function ValidaPreenchimento() {
    var valorComboMes = ddlMes.GetValue();
    var textoAno = txtAno.GetValue();

    if (valorComboMes == null) {
        return "Selecione o mês de referência";
    } else if (textoAno == null) {
        return "Digite o ano de referência";
    } else if (textoAno < 1900) {
        return "Digite um ano válido."
    }

    return "";
}

function onClick_CustomButtomGrid(s, e) {

    e.processOnServer = false;

    gvDados.SetFocusedRowIndex(e.visibleIndex);

    if (e.buttonID == 'btnDownLoad') {
        btnVisualizar_Click();
        e.processOnServer = true;
    }
    else if (e.buttonID == 'btnExcluir')
        btnExcluir_Click();
}

function btnExcluir_Click() {

    //gvDados.SetFocusedRowIndex(indexLinha);
    onClickBarraNavegacao("Excluir", gvDados, null);
    gvDados.Refresh();
}

function ExcluirRegistroSelecionado() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function btnVisualizar_Click(indexLinha) {

    gvDados.SetFocusedRowIndex(indexLinha);
    hfGeral.Set("TipoOperacao", "Visualizar");
    TipoOperacao = "Visualizar";
    pnCallback.PerformCallback(TipoOperacao);
}

function funcaoPosModal(retorno) {
    if (retorno == 'S')
        gvDados.PerformCallback();
}

function SalvarCamposFormulario() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);

    return false;
}


function GetHiddenFieldValue(key) {
    var value = null;
    if (hfGeral.Contains(key)) {
        value = hfGeral.Get(key);
        hfGeral.Remove(key);
    }
    return value;
}

function local_onEnd_pnCallback(s, e) {
    if (s.cp_OperacaoOk != undefined && s.cp_OperacaoOk != null && s.cp_OperacaoOk != "") {
        if ("GerarRelatorio" == s.cp_OperacaoOk) {
            mostraMensagemOperacao(traducao.HistoricoStatusReport_relat_rio_gerado_com_sucesso_);
            pcSelecaoModeloStatusReport.Hide();
        }
        else if ("Excluir" == s.cp_OperacaoOk) {
            mostraMensagemOperacao(traducao.HistoricoStatusReport_relat_rio_exclu_do_com_sucesso_);
        }
        gvDados.Refresh();
    } else {
        mostraMensagemErroOperacao(hfGeral.Get("ErroSalvar"));
    }
}

function mostraMensagemOperacao(acao) {
    window.top.mostraMensagem(acao, 'sucesso', false, false, null);
}

function mostraMensagemErroOperacao(acao) {
    window.top.mostraMensagem(acao, 'erro', true, false, null);
}

function onClick_btnCancelar_MSR() {
    habilitaModoEdicaoBarraNavegacao(false, gvDados);
    pcSelecaoModeloStatusReport.Hide();
    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    return true;
}

function onClick_btnSalvar_MSR() {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_deseja_gerar_o_relat_rio_, 'confirmacao', true, true, confirmaGerarRelatorio);
}

function confirmaGerarRelatorio() {
    var tipoOperacao = "GerarRelatorio";
    hfGeral.Set("TipoOperacao", tipoOperacao);
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(tipoOperacao);
    gvDados.Refresh();    
}

function onClick_btnSalvar() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback("Editar");
    return false;
}


function posSalvarComSucesso() {
    if (pnCallback.cp_OperacaoOk == 'Proximo')
        onClick_btnCancelar();
}

function LimpaCamposFormulario() {

}
function MontaCamposFormulario() {

}

function OnGridFocusedRowChanged() {

}