var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";

function local_onEnd_pnCallback(s, e) {
    if ("Compartilhar" == s.cp_LastOperation) {
        if (hfGeral.Contains("StatusSalvar")) {
            var status = hfGeral.Get("StatusSalvar");
            if (status != "1") {
                var mensagem = hfGeral.Get("ErroSalvar");
                pnlImpedimentos.SetVisible(true);
                lblGravacao2.SetText(mensagem);
                pcMensagemGravacao.Show();
            }
            else {
                //Caso não tenha excepção, mostra o mensagem de sucesso.
                mostraMensagemOperacao(traducao.ConfiguracoesStatusReport_dados_gravados_com_sucesso_);
            }
        }
    }
}

function onClick_CustomButtomGrid(s, e) {
    e.processOnServer = false;
    gvDados.SetFocusedRowIndex(e.visibleIndex);

    if (e.buttonID == 'btnCompartilhar')
        btnCompartilhar_click(gvDados);
}

function btnCompartilhar_click(grid) {
    // limpa o hidden field com a lista dos projetos 
    //hfProjetos.Clear();
    hfGeral.Set("TipoOperacao", "Compartilhar");
    TipoOperacao = "Compartilhar";
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoModeloStatusReport;DescricaoModeloStatusReport;IniciaisModeloControladoSistema;IndicaIncluiGraficoReceita', mostraPcCompartilharModeloStatusReport);
}

function mostraPcCompartilharModeloStatusReport(valores) {
    if (null != valores) {
        txtNomeModelo.SetText((valores[1] != null ? valores[1] : ""));
        var checkBoxVisivel = valores[2] == 'BLTQ' || valores[2] == 'BLT_AE_UN';
        var indicaIncluiGraficoReceita = valores[3] == 'S';
        var CodigoModeloStatusReport = valores[0] != null ? valores[0] : "";
        cbIncluirGrafico.SetVisible(checkBoxVisivel);
        cbIncluirGrafico.SetChecked(indicaIncluiGraficoReceita);
        callback_lbItensDisponiveis.PerformCallback(CodigoModeloStatusReport);
        callback_lblItensSelecionados.PerformCallback(CodigoModeloStatusReport);
        /*
        cbIncluirGrafico.SetChecked(hfGeral.Get("IndicaIncluiGraficoReceita"));        
        hfGeral.Set("IndicaUnidade", false);
        hfGeral.Set("IndicaIncluiGraficoReceita", false);
        */
    }
    hfGeral.Clear();   
    pcCompartilharIndicador.Show();
}

function habilitaBotoesListBoxes() {
    var habilita = window.btnSalvarCompartilhar && btnSalvarCompartilhar.GetVisible();
    btnAddAll.SetEnabled(habilita && lbItensDisponiveis.GetItemCount() > 0);
    btnAddSel.SetEnabled(habilita && lbItensDisponiveis.GetSelectedItem() != null);

    btnRemoveAll.SetEnabled(habilita && lbItensSelecionados.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(habilita && lbItensSelecionados.GetSelectedItem() != null);
}

function onClick_btnSalvarCompartilhar() {
    if (cbIncluirGrafico.GetVisible()) {
        gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoModeloStatusReport;IndicaIncluiGraficoReceita', function (valores) {
            var valorOriginal = valores[1] == 'S';
            var valor = cbIncluirGrafico.GetChecked();
            if (xor(valorOriginal, valor))
                callback.PerformCallback(valor + ";" + valores[0]);
        });
    }
    hfGeral.Set("StatusSalvar", "0");
    callback1.PerformCallback("Compartilhar");
}

function xor(a, b) {
    return (a || b) && !(a && b);
}

function mostraMensagemOperacao(acao) {
    window.top.mostraMensagem(acao, 'sucesso', false, false, null);
}

function setListBoxItemsInMemory(listBox, inicial) {
    if ((null != listBox) && (null != inicial) && (null != listBox.cpCodigoModeloStatusReport)) {

        var strConteudo = "";
        var idLista = inicial + listBox.cpCodigoModeloStatusReport + __cwf_delimitadorValores;
        var nQtdItems = listBox.GetItemCount();
        var item;

        for (var i = 0; i < nQtdItems; i++) {
            item = listBox.GetItem(i);
            strConteudo = strConteudo + item.text + __cwf_delimitadorValores + item.value + __cwf_delimitadorElementoLista;
        }

        if (0 < strConteudo.length)
            strConteudo = strConteudo.substr(0, strConteudo.length - 1);

        // grava a string no hiddenField
        hfProjetos.Set(idLista, strConteudo);
    }
}



function UpdateButtons() {

    btnAddSel.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbItensDisponiveis.GetSelectedItem() != null);
    btnAddAll.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbItensDisponiveis.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbItensSelecionados.GetSelectedItem() != null);
    btnRemoveAll.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbItensSelecionados.GetItemCount() > 0);
    capturaCodigosSelecionados();
}

function capturaCodigosSelecionados() {
    var CodigosSelecionados = "";
    for (var i = 0; i < lbItensSelecionados.GetItemCount(); i++) {
        CodigosSelecionados += lbItensSelecionados.GetItem(i).value + __cwf_delimitadorValores;
    }
    hfGeral.Set("CodigosSelecionados", CodigosSelecionados);    
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
