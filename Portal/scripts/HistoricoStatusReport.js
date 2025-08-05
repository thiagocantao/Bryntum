var codStatusReport;
var abreRelatorio = 'N';
var popUp;

var camposVisiveis = new Array();

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
    if (maxLength != 0 && text.length > maxLength) {
        textAreaElement.value = text.substr(0, maxLength);
    }
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function onClick_CustomButtomGrid(s, e) {
    e.processOnServer = false;

    gvDados.SetFocusedRowIndex(e.visibleIndex);

    if (e.buttonID == 'btnVisualizar') {
        btnVisualizar_Click();
        e.processOnServer = true;
    }
    else if (e.buttonID == 'btnEditar')
        btnEditar_Click(gvDados);
    else if (e.buttonID == 'btnPublicar')
        btnPublicar_Click();
    else if (e.buttonID == 'btnEnviar')
        btnEnviar_Click();
    else if (e.buttonID == 'btnDestaque')
        btnDestaque_Click();
    else if (e.buttonID == 'btnExcluir')
        btnExcluir_Click();
}

function btnExcluir_Click(indexLinha) {
    gvDados.SetFocusedRowIndex(indexLinha);
    onClickBarraNavegacao("Excluir", gvDados, pcDados);
    gvDados.Refresh();
}

function ExcluirRegistroSelecionado() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function MontaAnalise(valor) {
    var antes = htmlComentariosGerais.GetHtml();
    htmlComentariosGerais.SetHtml(antes + '<br>' + valor)
}


function btnEditar_Click(indexLinha) {

    gvDados.SetFocusedRowIndex(indexLinha);

    TipoOperacao = "Editar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    abreRelatorio = 'S';
    habilitaModoEdicaoBarraNavegacao(true, gvDados);
    OnGridFocusedRowChanged(gvDados, true);  // true é para forçar a chamada da função MontaCampos
}


function btnDestaque_Click(indexLinha) {

    gvDados.SetFocusedRowIndex(indexLinha);

    TipoOperacao = "EditarDestaque";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    abreRelatorio = 'N';
    habilitaModoEdicaoBarraNavegacao(true, gvDados);
    OnGridFocusedRowChangedDestaque(gvDados, true);  // true é para forçar a chamada da função MontaCampos
    popup.Show();
}

/*
0 - CodigoStatusReport;
1 - NomeRelatorio;
2 - DataGeracao;
3 - MostraComentarioGeral;
4 - MostraComentarioFisico;
5 - MostraComentarioFinanceiro;
6 - MostraComentarioRisco;
7 - MostraComentarioQuestao;
8 - MostraComentarioMeta;
9  - ComentarioGeral;
10 - ComentarioFisico;
11 - ComentarioFinanceiro;
12 - ComentarioRisco;
13 - ComentarioQuestao;
14 - ComentarioMeta;
15 - MostraComentarioPlanoAcao;
16 - ComentarioPlanoAcao;
17 - IniciaisModeloControladoSistema;
18 - CodigoAnalisePerformance;
19 - IndicaRegistroEditavel;
20 - DestaquesMes
*/
function MontaCamposFormulario(valores) {
    codStatusReport = valores[0];
    var iniciaisModeloControladoSistema = valores[17];

    if (iniciaisModeloControladoSistema == null || iniciaisModeloControladoSistema == "") {
        txtNomeRelatorio.SetText((valores[1] != null ? valores[1] : ""));
        deDataGeracao.SetValue(valores[2]);
        if (valores[3] == "S") {
            var text = (valores[9] != null ? valores[9] : "");
            document.getElementById("ComentariosGerais").style.display = "";
            htmlComentariosGerais.SetHtml(text);
            hfGeral.Set("IndicaRegistroEditavel", valores[19]);
        }
        else
            document.getElementById("ComentariosGerais").style.display = "none";
        if (valores[4] == "S") {
            var text = (valores[10] != null ? valores[10] : "");
            document.getElementById("AnaliseDesempenhoFisico").style.display = "";
            txtAnaliseDesempenhoFisico.SetText(text);
            txtAnaliseDesempenhoFisico.Validate();
        }
        else
            document.getElementById("AnaliseDesempenhoFisico").style.display = "none";
        if (valores[5] == "S") {
            var text = (valores[11] != null ? valores[11] : "");
            document.getElementById("AnaliseDesempenhoFinanceiro").style.display = "";
            txtAnaliseDesempenhoFinanceiro.SetText(text);
            txtAnaliseDesempenhoFinanceiro.Validate();
        }
        else
            document.getElementById("AnaliseDesempenhoFinanceiro").style.display = "none";
        if (valores[6] == "S") {
            var text = (valores[12] != null ? valores[12] : "");
            document.getElementById("AnaliseRiscos").style.display = "";
            txtAnaliseRiscos.SetText(text);
            txtAnaliseRiscos.Validate();
        }
        else
            document.getElementById("AnaliseRiscos").style.display = "none";
        if (valores[7] == "S") {
            var text = (valores[13] != null ? valores[13] : "");
            document.getElementById("AnaliseQuestoes").style.display = "";
            txtAnaliseQuestoes.SetText(text);
            txtAnaliseQuestoes.Validate();
        }
        else
            document.getElementById("AnaliseQuestoes").style.display = "none";
        if (valores[8] == "S") {
            var text = (valores[14] != null ? valores[14] : "");
            document.getElementById("AnaliseMetas").style.display = "";
            txtAnaliseMetas.SetText(text);
            txtAnaliseMetas.Validate();
        }
        else
            document.getElementById("AnaliseMetas").style.display = "none";
        if (valores[15] == "S") {
            var text = (valores[16] != null ? valores[16] : "");
            document.getElementById("ComentarioPlanoAcao").style.display = "";
            txtComentarioPlanoAcao.SetText(text);
            txtComentarioPlanoAcao.Validate();
        }
        else
            document.getElementById("ComentarioPlanoAcao").style.display = "none";
        //    if(valores[3]!="S" && valores[4]=="S" && valores[5]=="S" && valores[6]=="S" && valores[7]=="S" && valores[8]=="S" && valores[15]=="S")
        //        btnSalvar.SetVisible(false);

        pcDados.Show();
        pcDados.AdjustSize();
        if (abreRelatorio == 'S') {
            popUp = window.open("popupRelStatusReport.aspx?podeEditar=N&PodePublicar=N&codigoStatusReport=" + codStatusReport, "form", 'resizable=yes,menubar=no,scrollbars=yes,toolbar=no,width=800,height=' + (screen.height - 190));
            abreRelatorio = 'N';
        }
    }
    else {
        //        var width = screen.width > 1000? 750: screen.width - 250;
        var height = screen.height - 300;
        var varOpener = this;
        var url = iniciaisModeloControladoSistema == 'BLT_RAPU' ?
            window.top.pcModal.cp_Path + "_Projetos/Boletim/analisesRelatorioAcompanhamentoProjetosUnidade.aspx?podeEditar=" + btnProximo.cp_PodeEditar + "&codStatusReport=" + codStatusReport + "&iniciais=" + iniciaisModeloControladoSistema :
            window.top.pcModal.cp_Path + "_Projetos/Boletim/analisesCriticasProjetos.aspx?podeEditar=" + btnProximo.cp_PodeEditar + "&codStatusReport=" + codStatusReport + "&iniciais=" + iniciaisModeloControladoSistema + "&altura=" + height;
        var cont = window.top.document.location.pathname.split("/").length - 3;
        //        var ajustPath = "";
        //        for (var i = 0; i < cont; i++) {
        //            ajustPath = "../" + ajustPath;
        //        }
        //        url = ajustPath + url;
        var modal = window.top.showModal(url, traducao.HistoricoStatusReport_boletim_de_status, 1000, height, closePopup, varOpener);
        if (abreRelatorio == 'S') {
            popUp = window.open("popupRelProgramas.aspx?podeEditar=N&PodePublicar=N&codStatusReport=" + codStatusReport + "&iniciais=" + iniciaisModeloControladoSistema, "frmRelatorio", 'resizable=yes,menubar=no,scrollbars=yes,toolbar=no,width=800,height=' + (screen.height - 190));
            abreRelatorio = 'N';
        }
    }
}


/*
0 - CodigoStatusReport;
1 - NomeRelatorio;
2 - DataGeracao;
3 - MostraComentarioGeral;
4 - MostraComentarioFisico;
5 - MostraComentarioFinanceiro;
6 - MostraComentarioRisco;
7 - MostraComentarioQuestao;
8 - MostraComentarioMeta;
9  - ComentarioGeral;
10 - ComentarioFisico;
11 - ComentarioFinanceiro;
12 - ComentarioRisco;
13 - ComentarioQuestao;
14 - ComentarioMeta;
15 - MostraComentarioPlanoAcao;
16 - ComentarioPlanoAcao;
17 - IniciaisModeloControladoSistema;
18 - CodigoAnalisePerformance;
19 - IndicaRegistroEditavel;
20 - DestaquesMes
*/
function MontaCamposFormularioDestaque(valores) {
    codStatusReport = valores[0];
    var text = (valores[20] != null ? valores[20] : "");
    htmlDestaqueEdit.SetHtml(text);


}

function closePopup(retorno) {
    try {
        popUp.close();
        onClick_btnCancelar();
    } catch (e) { }
}

function btnVisualizar_Click() {

    hfGeral.Set("TipoOperacao", "Visualizar");
    TipoOperacao = "Visualizar";
    pnCallback.PerformCallback(TipoOperacao);


    //gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoStatusReport', mostraPopupStatusReport);
    //hfGeral.Set("TipoOperacao", "Visualizar");
    //TipoOperacao = "Visualizar";
    // pnCallback.PerformCallback(TipoOperacao);
}

function OnPopup(ComentarioPlanoAcao) {
    var analise = "";
    analise = htmlDestaqueEdit.GetHtml();
    //se 'analise' from igual a 'null' é atribuido a ela uma string vazia para evitar a ocorrencia de um erro
    if (analise == null)
        analise = "";
    htmlDestaqueEdit.SetHtml(analise);
}

function mostraPopupStatusReport(codigoStatusReport) {
    var varOpener = this;
    window.top.showModal("popupRelStatusReport.aspx?podeEditar=" + btnProximo.cp_PodeEditar + "&codigoStatusReport=" + codigoStatusReport, traducao.HistoricoStatusReport_relat_rio_de_status, 900, screen.height - 240, funcaoPosModal, varOpener);
}

function funcaoPosModal(retorno) {
    if (retorno == 'S')
        gvDados.PerformCallback();
}

function btnPublicar_Click() {
    if (confirm(traducao.HistoricoStatusReport_deseja_realmente_publicar_o_relat_rio_)) {
        hfGeral.Set("TipoOperacao", "Publicar");
        TipoOperacao = "Publicar";
        pnCallback.PerformCallback(TipoOperacao);
    }
}

function btnEnviar_Click() {
    if (confirm(traducao.HistoricoStatusReport_deseja_realmente_enviar_o_relat_rio_aos_destinat_rios_)) {
        hfGeral.Set("StatusSalvar", "0");
        hfGeral.Set("TipoOperacao", "Enviar");
        TipoOperacao = "Enviar";
        pnCallback.PerformCallback(TipoOperacao);
    }
}

function LimpaCamposFormulario() {
    txtNomeRelatorio.SetText("");
    deDataGeracao.SetValue(null);
    htmlComentariosGerais.SetHtml("");
    txtComentarioPlanoAcao.SetText("");
    txtComentarioPlanoAcao.Validate();
    txtAnaliseDesempenhoFisico.SetText("");
    txtAnaliseDesempenhoFisico.Validate();
    txtAnaliseDesempenhoFinanceiro.SetText("");
    txtAnaliseDesempenhoFinanceiro.Validate();
    txtAnaliseRiscos.SetText("");
    txtAnaliseRiscos.Validate();
    txtAnaliseQuestoes.SetText("");
    txtAnaliseQuestoes.Validate();
    txtAnaliseMetas.SetText("");
    txtAnaliseMetas.Validate();
}

function SalvarCamposFormulario() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);

    return false;
}

function onClick_BtnProximo(s, e, podeEditar) {
    if (Valida()) {

        try {
            popUp.close();
        } catch (e) { }

        hfGeral.Set("StatusSalvar", "0");
        //gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoStatusReport', SetCodigoStatusReport);
        setTimeout('PreviewStatusReport();', 3000);
        pnCallback.PerformCallback("Proximo");

        return false;
    }
}

function Valida() {
    if (document.getElementById("ComentariosGerais").style.display != "none" && htmlComentariosGerais.GetHtml() == "") {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_todos_os_campos_devem_ser_preenchidos_, 'atencao', true, false, null);
        return false;
    }
    if (document.getElementById("ComentarioPlanoAcao").style.display != "none" && txtComentarioPlanoAcao.GetText() == "") {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_todos_os_campos_devem_ser_preenchidos_, 'atencao', true, false, null);
        return false;
    }
    if (document.getElementById("AnaliseDesempenhoFisico").style.display != "none" && txtAnaliseDesempenhoFisico.GetText() == "") {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_todos_os_campos_devem_ser_preenchidos_, 'atencao', true, false, null);
        return false;
    }
    if (document.getElementById("AnaliseDesempenhoFinanceiro").style.display != "none" && txtAnaliseDesempenhoFinanceiro.GetText() == "") {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_todos_os_campos_devem_ser_preenchidos_, 'atencao', true, false, null);
        return false;
    }
    if (document.getElementById("AnaliseRiscos").style.display != "none" && txtAnaliseRiscos.GetText() == "") {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_todos_os_campos_devem_ser_preenchidos_, 'atencao', true, false, null);
        return false;
    }
    if (document.getElementById("AnaliseQuestoes").style.display != "none" && txtAnaliseQuestoes.GetText() == "") {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_todos_os_campos_devem_ser_preenchidos_, 'atencao', true, false, null);
        return false;
    }
    if (document.getElementById("AnaliseMetas").style.display != "none" && txtAnaliseMetas.GetText() == "") {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_todos_os_campos_devem_ser_preenchidos_, 'atencao', true, false, null);
        return false;
    }

    return true;
}

function PreviewStatusReport() {
    mostraPopupStatusReport(codStatusReport);
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
    if (window.onEnd_pnCallback)
        onEnd_pnCallback();

    var indicaRegistroEditavel = GetHiddenFieldValue("IndicaRegistroEditavel");
    //htmlComentariosGerais.SetEnabled(indicaRegistroEditavel == "S");
    var focusedRowIndex = GetHiddenFieldValue("FocusedRowIndex");
    gvDados.SetFocusedRowIndex(focusedRowIndex);

    if (s.cp_OperacaoOk != undefined && s.cp_OperacaoOk != null && s.cp_OperacaoOk != "") {
        if ("Enviar" == s.cp_OperacaoOk)
            mostraMensagemOperacao(traducao.HistoricoStatusReport_envio_realizado_com_sucesso_);
        else if ("Publicar" == s.cp_OperacaoOk)
            mostraMensagemOperacao(traducao.HistoricoStatusReport_publica__o_realizada_com_sucesso_);
        else if ("Editar" == s.cp_OperacaoOk)
            mostraMensagemOperacao(traducao.HistoricoStatusReport_dados_gravados_com_sucesso_);
        else if ("GerarRelatorio" == s.cp_OperacaoOk) {
            mostraMensagemOperacao(traducao.HistoricoStatusReport_relat_rio_gerado_com_sucesso_);
            pcSelecaoModeloStatusReport.Hide();
        }
        else if ("Excluir" == s.cp_OperacaoOk)
            mostraMensagemOperacao(traducao.HistoricoStatusReport_relat_rio_exclu_do_com_sucesso_);
        else if ("SalvarDestaque" == s.cp_OperacaoOk) {
            mostraMensagemOperacao(traducao.HistoricoStatusReport_dados_de_destaque_do_m_s_gravados_com_sucesso_);
            popup.Hide();
            onClick_btnCancelar();
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

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos) {
        var focusedRowIndex = gvDados.GetFocusedRowIndex();
        hfGeral.Set("FocusedRowIndex", focusedRowIndex);
        gvDados.GetRowValues(focusedRowIndex, 'CodigoStatusReport;NomeRelatorio;DataGeracao;MostraComentarioGeral;MostraComentarioFisico;MostraComentarioFinanceiro;MostraComentarioRisco;MostraComentarioQuestao;MostraComentarioMeta;ComentarioGeral;ComentarioFisico;ComentarioFinanceiro;ComentarioRisco;ComentarioQuestao;ComentarioMeta;MostraComentarioPlanoAcao;ComentarioPlanoAcao;IniciaisModeloControladoSistema;CodigoAnalisePerformance;IndicaRegistroEditavel;DestaquesMes;', MontaCamposFormulario);
    }
    else if (camposVisiveis != undefined && camposVisiveis.length > 0) {
        var indice = 0;
        for (indice = 0; indice < camposVisiveis.length - 1; indice++) {
            document.getElementById(camposVisiveis[indice]).style.display = "";
        }
        var focusedRowIndex = camposVisiveis[camposVisiveis.length - 1];
        gvDados.SetFocusedRowIndex(focusedRowIndex);
        camposVisiveis = new Array();
    }
}

function OnGridFocusedRowChangedDestaque(grid, forcarMontaCampos) {
    if (forcarMontaCampos) {
        var focusedRowIndex = gvDados.GetFocusedRowIndex();
        hfGeral.Set("FocusedRowIndex", focusedRowIndex);
        gvDados.GetRowValues(focusedRowIndex, 'CodigoStatusReport;NomeRelatorio;DataGeracao;MostraComentarioGeral;MostraComentarioFisico;MostraComentarioFinanceiro;MostraComentarioRisco;MostraComentarioQuestao;MostraComentarioMeta;ComentarioGeral;ComentarioFisico;ComentarioFinanceiro;ComentarioRisco;ComentarioQuestao;ComentarioMeta;MostraComentarioPlanoAcao;ComentarioPlanoAcao;IniciaisModeloControladoSistema;CodigoAnalisePerformance;IndicaRegistroEditavel;DestaquesMes;', MontaCamposFormularioDestaque);
    }
    else if (camposVisiveis != undefined && camposVisiveis.length > 0) {
        var indice = 0;
        for (indice = 0; indice < camposVisiveis.length - 1; indice++) {
            document.getElementById(camposVisiveis[indice]).style.display = "";
        }
        var focusedRowIndex = camposVisiveis[camposVisiveis.length - 1];
        gvDados.SetFocusedRowIndex(focusedRowIndex);
        camposVisiveis = new Array();
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
        window.top.mostraMensagem(traducao.HistoricoStatusReport_selecione_um_modelo_de_relat_rio_de_status, 'Atencao', true, false, null);
    else
    {
        window.top.mostraMensagem(traducao.HistoricoStatusReport_deseja_gerar_o_relat_rio_, 'confirmacao', true, true, confirmaGerarRelatorio);
    }
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
    armazenaDadosNecessarios();
    return false;
}

function onClick_BtnSalvarDestaque(s, e) {
    pnCallback.PerformCallback('SalvarDestaque');
    popup.Hide();

}

function posSalvarComSucesso() {
    if (pnCallback.cp_OperacaoOk == 'Proximo')
        onClick_btnCancelar();
}

function armazenaDadosNecessarios() {
    var cont = 0;

    if (document.getElementById("ComentariosGerais").style.display != "none") {
        camposVisiveis[cont] = 'ComentariosGerais';
        cont++;
    }
    if (document.getElementById("ComentarioPlanoAcao").style.display != "none") {
        camposVisiveis[cont] = 'ComentarioPlanoAcao';
        cont++;
    }
    if (document.getElementById("AnaliseDesempenhoFisico").style.display != "none") {
        camposVisiveis[cont] = 'AnaliseDesempenhoFisico';
        cont++;
    }
    if (document.getElementById("AnaliseDesempenhoFinanceiro").style.display != "none") {
        camposVisiveis[cont] = 'AnaliseDesempenhoFinanceiro';
        cont++;
    }
    if (document.getElementById("AnaliseRiscos").style.display != "none") {
        camposVisiveis[cont] = 'AnaliseRiscos';
        cont++;
    }
    if (document.getElementById("AnaliseQuestoes").style.display != "none") {
        camposVisiveis[cont] = 'AnaliseQuestoes';
        cont++;
    }
    if (document.getElementById("AnaliseMetas").style.display != "none") {
        camposVisiveis[cont] = 'AnaliseMetas';
        cont++;
    }
    camposVisiveis[cont] = gvDados.GetFocusedRowIndex();
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 40;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}