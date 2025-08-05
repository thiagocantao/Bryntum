var codigoRecursoParam = null;

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (ddlProjeto.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.EmpenhoFinanceiro_o_projeto_deve_ser_informado_;
    }

    if (rbDespesaReceita.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.EmpenhoFinanceiro_selecione__despesa__ou__receita__;
    }

    if (ddlEmpenhadoEm.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.EmpenhoFinanceiro_a_data_em_que_foi_empenhado_deve_ser_informada_;
    }

    if (txtValor.GetValue() == null || Trim(txtValor.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.EmpenhoFinanceiro_o_valor_deve_ser_informado_;
    }
    if (hfGeral.Get("Critica") == "N") {
        mensagem = "";
    }
    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }
    hfGeral.Set("Critica", "");
    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {

    if (ddlProjeto.cp_RO != "S")
        ddlProjeto.SetValue(null);

    ddlTarefa.SetValue(null);
    ddlRecurso.SetValue(null);
    rbDespesaReceita.SetValue("D");
    ddlConta.SetValue(null);
    //ddlFontePagadora.SetValue(null);
    ddlRazaoSocial.SetValue(null);
    ddlEmissaoDoc.SetValue(null);
    txtNumeroDoc.SetText("");
    ddlEmpenhadoEm.SetValue(null);
    ddlPrevistoPara.SetValue(null);
    txtComentariosEmpenho.SetText("");
    txtComentariosEmpenho.Validate();
    txtValor.SetText("");
    dtPagoEm.SetValue(null);
    mmPagamento.SetText("");
    mmPagamento.Validate();
    rbStatusEmpenho.SetValue("P");
    lblInclusao.SetText("");
    lblAprovacao.SetText("");
    lblAprovacao2.SetText("");
    verificaDespesaReceita();


    ddlConta.PerformCallback("");

    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && (pcDados.IsVisible() || pcAprovacao.IsVisible()))) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoLancamentoFinanceiro;CodigoProjeto;IndicaDespesaReceita;DataEmpenho;ValorEmpenhado;DataPagamentoRecebimento;ValorPagamentoRecebimento;CodigoPessoaEmitente;NumeroDocFiscal;DataEmissaoDocFiscal;CodigoConta;IndicaAprovacaoReprovacao;DataPrevistaPagamentoRecebimento;HistoricoEmpenho;HistoricoPagamento;CodigoTarefa;CodigoRecursoProjeto;DataInclusao;UsuarioInclusao;DataAprovacaoReprovacao;UsuarioAprovacao;HistoricoAprovacaoReprovacao;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var CodigoLancamentoFinanceiro = (values[0] != null ? values[0] : "");
    var CodigoProjeto = (values[1] != null ? values[1] : "");
    var IndicaDespesaReceita = (values[2] != null ? values[2] : "");
    var DataEmpenho = (values[3] != null ? values[3] : "");
    var ValorEmpenhado = (values[4] != null ? values[4] : "");
    var DataPagamentoRecebimento = (values[5] != null ? values[5] : "");
    var ValorPagamentoRecebimento = (values[6] != null ? values[6] : "");
    var CodigoPessoaEmitente = (values[7] != null ? values[7] : "");
    var NumeroDocFiscal = (values[8] != null ? values[8] : "");
    var DataEmissaoDocFiscal = (values[9] != null ? values[9] : "");
    var CodigoConta = (values[10] != null ? values[10] : "");
    var IndicaAprovacaoReprovacao = (values[11] != null ? values[11] : "");
    var DataPrevistaPagamentoRecebimento = (values[12] != null ? values[12] : "");
    var HistoricoEmpenho = (values[13] != null ? values[13] : "");
    var HistoricoPagamento = (values[14] != null ? values[14] : "");
    var CodigoTarefa = (values[15] != null ? values[15] : "");
    var CodigoRecursoProjeto = (values[16] != null ? values[16] : "");
    var DataInclusao = (values[17] != null ? values[17] : "");
    var UsuarioInclusao = (values[18] != null ? values[18] : "");
    var DataAprovacaoReprovacao = (values[19] != null ? values[19] : "");
    var UsuarioAprovacao = (values[20] != null ? values[20] : "");
    var HistoricoAprovacaoReprovacao = (values[21] != null ? values[21] : "");
    //var CodigoFonteRecursosFinanceiros = (values[22] != null ? values[22] : "");

    codigoRecursoParam = CodigoRecursoProjeto;

    if (ddlProjeto.cp_RO != "S")
        ddlProjeto.SetValue(CodigoProjeto);

    rbDespesaReceita.SetValue(IndicaDespesaReceita);
    ddlRazaoSocial.SetValue(CodigoPessoaEmitente);
    //ddlFontePagadora.SetValue(CodigoFonteRecursosFinanceiros);
    ddlEmissaoDoc.SetValue(DataEmissaoDocFiscal == "" ? null : DataEmissaoDocFiscal);
    txtNumeroDoc.SetText(NumeroDocFiscal);
    ddlEmpenhadoEm.SetValue(DataEmpenho);
    ddlPrevistoPara.SetValue(DataPrevistaPagamentoRecebimento == "" ? null : DataPrevistaPagamentoRecebimento);
    dtPagoEm.SetValue(DataPagamentoRecebimento == "" ? null : DataPagamentoRecebimento);
    txtValor.SetValue(ValorEmpenhado);

    mmPagamento.SetText(HistoricoPagamento);
    mmPagamento.Validate();

    txtComentariosEmpenho.SetText(HistoricoEmpenho);
    txtComentariosEmpenho.Validate();

    mmAprovacao.SetText(HistoricoAprovacaoReprovacao);
    mmAprovacao.Validate();

    rbStatusEmpenho.SetValue(IndicaAprovacaoReprovacao);

    lblInclusao.SetText("Incluído em " + DataInclusao + " por " + UsuarioInclusao);

    if (IndicaAprovacaoReprovacao == "A") {
        lblAprovacao.SetText("Aprovado em " + DataAprovacaoReprovacao + " por " + UsuarioAprovacao);
        lblAprovacao2.SetText("Aprovado em " + DataAprovacaoReprovacao + " por " + UsuarioAprovacao);
    }
    else if (IndicaAprovacaoReprovacao == "R") {
        lblAprovacao.SetText("Reprovado em " + DataAprovacaoReprovacao + " por " + UsuarioAprovacao);
        lblAprovacao2.SetText("Reprovado em " + DataAprovacaoReprovacao + " por " + UsuarioAprovacao);
    }
    else {

        lblAprovacao.SetText("");
        lblAprovacao2.SetText("");
    }

    verificaDespesaReceita();




    ddlConta.PerformCallback(CodigoConta);
    ddlTarefa.PerformCallback(CodigoTarefa);
}

function verificaDespesaReceita() {
    if (rbDespesaReceita.GetValue() == "D") {//saida é fornecedor
        lblRazaoSocial.SetText("Fornecedor:");
        lblPagoEmRecebidoEm.SetText("Pago em:");
    }
    else {
        lblRazaoSocial.SetText("Cliente:"); //entrada e cliente
        lblPagoEmRecebidoEm.SetText("Recebido em:");
    }
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    //    txtRamoAtividade.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlProjeto.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && ddlProjeto.cp_RO != "S");
    ddlTarefa.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlRecurso.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    rbDespesaReceita.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlConta.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    //ddlFontePagadora.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlRazaoSocial.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlEmissaoDoc.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtNumeroDoc.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlEmpenhadoEm.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlPrevistoPara.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtComentariosEmpenho.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValor.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    btnSalvar.SetVisible(window.TipoOperacao && TipoOperacao != "Consultar");
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

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
}

function atualizaGrid() {
    gvDados.Refresh();
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function mostraPopupLancamentoFinanceiro(valores) {
    //'CodigoLancamentoFinanceiro;IniciaisControleLancamento;CodigoProjeto;
    var codigoLancamentoFinanceiro = valores[0];
    var iniciaisControleLancamento = valores[1];
    var codigoProjeto = valores[2];
    var url = window.top.pcModal.cp_Path + "_projetos/administracao/LancamentosFinanceirosConvenio.aspx";
    url += "?clf=" + codigoLancamentoFinanceiro;
    url += "&tipo=" + iniciaisControleLancamento;
    url += "&cp=" + codigoProjeto;
    window.top.showModal(url, "Pagamento", screen.width - 200, 500, atualizaGrid, null);
}