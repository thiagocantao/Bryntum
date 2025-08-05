function onClick_CustomButtomGrid(s, e) {
    e.processOnServer = false;

    gvDados.SetFocusedRowIndex(e.visibleIndex);

    if (e.buttonID == 'btnVisualizar') {
        btnVisualizar_Click();
        e.processOnServer = true;
    }
}

function onClick_btnCancelar_MSR() {
    habilitaModoEdicaoBarraNavegacao(false, gvDados);
    pcSelecaoModeloStatusReport.Hide();
    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    return true;
}

function onClick_btnSalvar_MSR() {
    if (ddlModeloStatusReport.GetSelectedIndex() == -1)
        window.top.mostraMensagem(traducao.HistoricoStatusReportCarteira_selecone_um_modelo_de_relat_rio_de_status, 'Atencao', true, false, null);
    else if (confirm(traducao.HistoricoStatusReportCarteira_deseja_gerar_o_relat_rio_)) {
        TipoOperacao = "GerarRelatorio";
        hfGeral.Set("TipoOperacao", TipoOperacao);
        hfGeral.Set("StatusSalvar", "0");
        pnCallback.PerformCallback(TipoOperacao);
        gvDados.Refresh();
    }
}

function local_onEnd_pnCallback(s, e) {
    if ("GerarRelatorio" == s.cp_OperacaoOk) {
        mostraMensagemOperacao(traducao.HistoricoStatusReportCarteira_relat_rio_gerado_com_sucesso_);
        pcSelecaoModeloStatusReport.Hide();
    }
    else if ("Excluir" == s.cp_OperacaoOk)
        mostraMensagemOperacao(traducao.HistoricoStatusReportCarteira_relat_rio_exclu_do_com_sucesso_);
}

function mostraMensagemOperacao(acao) {
    lblAcaoGravacao.SetText(acao);
    pcOperMsg.Show();
    setTimeout('fechaTelaEdicao();', 1500);
}

function fechaTelaEdicao() {
    pcOperMsg.Hide();
}

function mostraPopupSelecaoModeloStatusReport() {
    pcSelecaoModeloStatusReport.Show();
}

function btnExcluir_Click(indexLinha) {
    gvDados.SetFocusedRowIndex(indexLinha);
    onClickBarraNavegacao("Excluir", gvDados, null);
    gvDados.Refresh();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
}

function ExcluirRegistroSelecionado() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

