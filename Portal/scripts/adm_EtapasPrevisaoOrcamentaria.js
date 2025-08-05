var TipoOperacao;


function mostraPopup(values) {

   
    var CodigoPrevisao = (values[0] != null ? values[0] : "");
    var DescricaoPrevisao = (values[1] != null ? values[1] : "");
    var AnoOrcamento = (values[2] != null ? values[2] : "");
    var Observacao = (values[3] != null ? values[3] : "");
    var DescricaoStatusPrevisaoFluxoCaixa = (values[4] != null ? values[4] : "");
    var MesInicioBloqueio = (values[5] != null ? values[5] : "");
    var MesTerminoBloqueio = (values[6] != null ? values[6] : "");
    var InicioPeriodoElaboracao = (values[7] != null ? values[7] : "");
    var TerminoPeriodoElaboracao = (values[8] != null ? values[8] : "");
    var CodigoStatusPrevisaoFluxoCaixa = (values[9] != null ? values[9] : "");
    var PodeExcluir = (values[10] != null ? values[10] : "");
    var PodeAlterarStatus = (values[11] != null ? values[11] : "");
    var PodeIgualarPrevistoRealizado = (values[12] != null ? values[12] : "");
    var CodigoNovoStatusPermitido = (values[13] != null ? values[13] : "");
    var CodigoStatusAnteriorPermitido = (values[14] != null ? values[14] : "");

    txtPrevisao.SetText(DescricaoPrevisao);
    spnAno.SetValue(AnoOrcamento);
    memoObservacoes.SetText(Observacao);
    ddlMesInicioBloqueio.SetValue(MesInicioBloqueio);
    ddlMesTerminoBloqueio.SetValue(MesTerminoBloqueio);
    ddlDataInicioElaboracao.SetText(InicioPeriodoElaboracao);
    ddlDataTerminoElaboracao.SetText(TerminoPeriodoElaboracao);
    ddlStatus.SetValue(CodigoStatusPrevisaoFluxoCaixa);
    pnCallbackStatus.PerformCallback(CodigoStatusPrevisaoFluxoCaixa + '|' + TipoOperacao);

    desabilitaHabilitaComponentes();

    pcDados.Show();
  
}

function desabilitaHabilitaComponentes()
{
    if (TipoOperacao === 'Consultar') {
        ddlStatus.SetEnabled(false);
        txtPrevisao.SetEnabled(false);
        spnAno.SetEnabled(false);
        memoObservacoes.SetEnabled(false);
        ddlMesInicioBloqueio.SetEnabled(false);
        ddlMesTerminoBloqueio.SetEnabled(false);
        ddlDataInicioElaboracao.SetEnabled(false);
        ddlDataTerminoElaboracao.SetEnabled(false);
        ddlStatus.SetEnabled(false);
        btnSalvar.SetEnabled(false);
    }
    else {
        ddlStatus.SetEnabled(true);
        txtPrevisao.SetEnabled(true);
        spnAno.SetEnabled(true);
        memoObservacoes.SetEnabled(true);
        ddlMesInicioBloqueio.SetEnabled(true);
        ddlMesTerminoBloqueio.SetEnabled(true);
        ddlDataInicioElaboracao.SetEnabled(true);
        ddlDataTerminoElaboracao.SetEnabled(true);
        ddlStatus.SetEnabled(true);
        btnSalvar.SetEnabled(true);
    }

}

function abreNovoRegistro() {
    desabilitaHabilitaComponentes();
    txtPrevisao.SetText('');
    spnAno.SetValue(null);
    memoObservacoes.SetText('');
    ddlMesInicioBloqueio.SetValue(null);
    ddlMesTerminoBloqueio.SetValue(null);
    ddlDataInicioElaboracao.SetValue(null);
    ddlDataTerminoElaboracao.SetValue(null);
    ddlStatus.SetValue(null);
    btnSalvar1.SetVisible(true);
    pnCallbackStatus.PerformCallback('-1|Incluir');
    pcDados.Show();
}



function verificarDadosPreenchidos() {
    var mensagemError = '';
    var numError = 0;
    var retorno = true;
    if (trim(txtPrevisao.GetText()) == "") {
        mensagemError += ++numError + ") O campo Previsão deve ser preenchido\n";
        retorno = false;
    }
    if (spnAno.GetValue() == null) {
        mensagemError += ++numError + ") O campo Ano deve ser preenchido\n";
        retorno = false;
    }


    if (ddlDataInicioElaboracao.GetValue() == null) {
        mensagemError += ++numError + ") O campo início elaboração deve ser preenchido\n";
        retorno = false;
    }

    if (ddlDataTerminoElaboracao.GetValue() == null) {
        mensagemError += ++numError + ") O campo término elaboração deve ser preenchido\n";
        retorno = false;
    }

    if (ddlStatus.GetValue() == null) {
        mensagemError += ++numError + ") O campo status deve ser preenchido\n";
        retorno = false;
    }
    if (!retorno) {
        window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);
    }
    return retorno;

}


function ExcluirRegistroSelecionado() {
    callbackSalvar.PerformCallback(TipoOperacao);
}

function igualaPrevistoRealizado() {
    callbackSalvar.PerformCallback(TipoOperacao);
}
