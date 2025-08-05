function validaCampos() {
    var retorno = false;
    var mensagemErro = "";
    if (ddlMapa.GetValue() == null || trim(ddlMapa.GetText().toString())== "") {
        mensagemErro = traducao.relGestaoEstrategia_o_mapa_estrat_gico_n_o_foi_selecionado_;
    }
    else {
        retorno = true;
    }
    if (!retorno) {
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
    }
    else {
        lpLoading.Show();
        cbPrint.PerformCallback();
    }
    return retorno;
}
