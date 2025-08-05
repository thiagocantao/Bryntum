// JScript File
var urlFrmAnexosContrato = '';
var atualizarURLAnexos = '';

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
    //debugger
    var tOperacao = ""

    try {// Função responsável por preparar os campos do formulário para receber um novo registro

        txtTituloItem.SetText("");
        txtDetalheItem.SetText("");
        txtEsforco.SetValue(null);
        ddlComplexidade.SetValue(null);
        txtImportancia.SetValue(null);
        ddlStatus.SetValue(null);
        ddlClassificacao.SetValue(null);
        lblItemNaoPlanejado.SetText("");
        txtPercentualConcluido.SetValue(null);

        ddlCliente.SetText("");
        ddlCliente.SetValue(null);

        ddlTipoAtividade.SetText("");
        ddlTipoAtividade.SetValue(null);

        spnReceitaPrevista.SetValue(null);

        txtTituloItem.SetEnabled(true);
        txtDetalheItem.SetEnabled(true);
        txtEsforco.SetEnabled(true);
        ddlComplexidade.SetEnabled(true);
        txtImportancia.SetEnabled(true);
        ddlStatus.SetEnabled(true);
        ddlClassificacao.SetEnabled(true);
        lblItemNaoPlanejado.SetEnabled(true);
        txtPercentualConcluido.SetEnabled(false);
        pnDescricaoSprint.PerformCallback("-1");
    } catch (e) { }
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        //                                                     0;         1;            2;                 3;          4;                      5;                   6;                            7;                           8;                    9;          10;                          11;                 12;                   13;                 14;            15;         16;          17;             18;                    19;                20;            21;               22;                               23;                    24;           25;         26;                       27;                          28;             29;
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoItem;TituloItem;CodigoProjeto;CodigoItemSuperior;DetalheItem;DescricaoTipoStatusItem;CodigoTipoStatusItem;DescricaoTipoClassificacaoItem;CodigoTipoClassificacaoItem;CodigoUsuarioInclusao;DataInclusao;CodigoUsuarioUltimaAlteracao;DataUltimaAlteracao;CodigoUsuarioExclusao;PercentualConcluido;CodigoIteracao;Importancia;Complexidade;EsforcoPrevisto;IndicaItemNaoPlanejado;IndicaBloqueioItem;CodigoWorkflow;CodigoInstanciaWF;CodigoCronogramaProjetoReferencia;CodigoTarefaReferencia;CodigoCliente;NomeCliente;CodigoTipoTarefaTimeSheet;DescricaoTipoTarefaTimeSheet;ReceitaPrevista;TituloStatusItem', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente

    txtTituloItem.SetText("");
    txtDetalheItem.SetText("");
    txtEsforco.SetValue(null);
    ddlComplexidade.SetValue(null);
    txtImportancia.SetValue(null);
    ddlStatus.SetValue(null);
    ddlClassificacao.SetValue(null);
    lblItemNaoPlanejado.SetText("");

    var CodigoItem = (values[0] != null ? values[0] : "");
    var TituloItem = (values[1] != null ? values[1] : "");
    var CodigoProjeto = (values[2] != null ? values[2] : "");
    var CodigoItemSuperior = (values[3] != null ? values[3] : "");
    var DetalheItem = (values[4] != null ? values[4] : "");
    var DescricaoTipoStatusItem = (values[5] != null ? values[5] : "");
    var CodigoTipoStatusItem = (values[6] != null ? values[6] : "");
    var DescricaoTipoClassificacaoItem = (values[7] != null ? values[7] : "");
    var CodigoTipoClassificacaoItem = (values[8] != null ? values[8] : "");
    var CodigoUsuarioInclusao = (values[9] != null ? values[9] : "");
    var DataInclusao = (values[10] != null ? values[10] : "");
    var CodigoUsuarioUltimaAlteracao = (values[11] != null ? values[11] : "");
    var DataUltimaAlteracao = (values[12] != null ? values[12] : "");
    var CodigoUsuarioExclusao = (values[13] != null ? values[13] : "");
    var PercentualConcluido = (values[14] != null ? values[14] : "");
    var CodigoIteracao = (values[15] != null ? values[15] : "");
    var Importancia = (values[16] != null ? values[16] : "");
    var Complexidade = (values[17] != null ? values[17] : "0");
    var EsforcoPrevisto = (values[18] != null ? values[18] : "");
    var IndicaItemNaoPlanejado = (values[19] != null ? values[19] : "");
    var IndicaBloqueioItem = (values[20] != null ? values[20] : "");
    var CodigoWorkflow = (values[21] != null ? values[21] : "");
    var CodigoInstanciaWf = (values[22] != null ? values[22] : "");
    var CodigoCronogramaProjetoReferencia = (values[23] != null ? values[23] : "");
    var CodigoTarefaReferencia = (values[24] != null ? values[24] : "");
    var CodigoCliente = (values[25] != null ? values[25] : "");
    var NomeCliente = (values[26] != null ? values[26] : "");
    var CodigoTipoTarefaTimeSheet = (values[27] != null ? values[27] : "");
    var DescricaoTipoTarefaTimeSheet = (values[28] != null ? values[28] : "");
    var ReceitaPrevista = (values[29] != null ? values[29] : "0");
    var TituloStatusItem = (values[30] != null ? values[30] : "");

    ddlCliente.SetValue(CodigoCliente);
    ddlCliente.SetText(NomeCliente);

    ddlTipoAtividade.SetValue(CodigoTipoTarefaTimeSheet);
    ddlTipoAtividade.SetText(DescricaoTipoTarefaTimeSheet);

    spnReceitaPrevista.SetValue(ReceitaPrevista);

    txtTituloItem.SetText(TituloItem);
    txtDetalheItem.SetText(DetalheItem);
    txtDetalheItem.Validate();
    txtEsforco.SetValue(EsforcoPrevisto);
    ddlComplexidade.SetValue(Complexidade);
    txtImportancia.SetValue(Importancia);

    ddlStatus.SetValue(CodigoTipoStatusItem);
    //ddlStatus.SetText(TituloStatusItem);

    txtPercentualConcluido.SetValue(PercentualConcluido);
    //ckbItemNaoPlanejado.SetValue(IndicaItemNaoPlanejado);

    var textoItemNaoPlanejado = (IndicaItemNaoPlanejado == "S" ? "Item Não Planejado" : "Item Planejado");
    lblItemNaoPlanejado.SetText(textoItemNaoPlanejado);

    ddlClassificacao.SetValue(CodigoTipoClassificacaoItem);

    atualizarURLAnexos = 'S';
    var readOnly = hfGeral.Get("TipoOperacao") == "Consultar" ? 'S' : 'N';

    desabilitaHabilitaComponentes(IndicaBloqueioItem);

    urlFrmAnexosContrato = '../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=IB&ID=' + CodigoItem + '&RO=' + readOnly + '&TO=' + hfGeral.Get("TipoOperacao");
    pnDescricaoSprint.PerformCallback(CodigoItem);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0)
        onClick_btnCancelar();
}

function desabilitaHabilitaComponentes(IndicaBloqueioItem) {
    var BoolEnabled = hfGeral.Get("TipoOperacao") == "Editar" || hfGeral.Get("TipoOperacao") == "Incluir" ? true : false;
    txtTituloItem.SetEnabled(BoolEnabled);
    txtDetalheItem.SetEnabled(BoolEnabled);
    txtEsforco.SetEnabled(BoolEnabled);
    ddlComplexidade.SetEnabled(BoolEnabled);
    txtImportancia.SetEnabled(BoolEnabled);
    ddlStatus.SetEnabled(BoolEnabled);
    ddlClassificacao.SetEnabled(BoolEnabled);

    ddlCliente.SetEnabled(BoolEnabled);
    ddlTipoAtividade.SetEnabled(BoolEnabled);
    spnReceitaPrevista.SetEnabled(BoolEnabled);

    //txtPercentualConcluido.SetEnabled(BoolEnabled);
}

function showDetalhe() {
    OnGridFocusedRowChanged(gvDados, true);
    btnSalvar.SetVisible(false);
    hfGeral.Set("TipoOperacao", "Consultar");
    pcDados.Show();
}

function verificarDadosPreenchidos() {
    var mensagemError = "";
    var retorno = true;
    var numError = 0;

    if (txtTituloItem.GetText() == "") {
        mensagemError += ++numError + ") O título do item deve ser informado!\n";
        retorno = false;
    }
    if (txtDetalheItem.GetText() == "") {
        mensagemError += ++numError + ") Os detalhes do item devem ser informados!\n";
        retorno = false;
    }
    if (txtEsforco.GetText() == "") {
        mensagemError += ++numError + ") O Esforço do item deve ser informado!\n";
        retorno = false;
    }

    if (ddlComplexidade.GetSelectedIndex() == -1) {
        mensagemError += ++numError + ") A complexidade do item deve ser informada!\n";
        retorno = false;
    }
    if (txtImportancia.GetText() == "") {
        mensagemError += ++numError + ") A importância do item deve ser informada!\n";
        retorno = false;
    }

    if (ddlStatus.GetText == "" && ddlClassificacao.cp_Visivel != 'N') {
        mensagemError += ++numError + ") O status do item deve ser informado!\n";
        retorno = false;
    }
    if (ddlClassificacao.GetSelectedIndex() == -1 && ddlClassificacao.cp_Visivel != 'N') {
        mensagemError += ++numError + ") A classificação do item deve ser informada!\n";
        retorno = false;
    }
    if (!retorno)
        window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);

    return retorno;
}

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

function podeMudarAba(s, e) {
    if (e.tab.index == 0) {
        return false;
    }
    else if (hfGeral.Get("TipoOperacao") == 'Incluir') {
        window.top.mostraMensagem("Para ter acesso a opção \"" + e.tab.GetText() + "\" é obrigatório salvar as informações da opção \"" + tabControl.GetTab(0).GetText() + "\"", 'Atencao', true, false, null);
        return true;
    }

    return false;
}

