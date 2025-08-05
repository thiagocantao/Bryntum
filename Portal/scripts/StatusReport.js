var __cwf_delimitadorValores = "ֆ";
var __cwf_delimitadorElementoLista = "¢";
var rowIndexGlobal = -1;

function excluiAssociacaoModeloStatusReport() {
    gvAssociacaoModelos.DeleteRow(rowIndexGlobal);
}
// esta função chama o método no servidor responsável por persistir as informações no banco
// o método será chamado por meio do objeto pnCallBack.

function SalvarCamposFormulario() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);

    return false;
}

// esta função chama o método no servidor responsável por excluir o registro selecionado
// o método será chamado por meio do objeto pnCallBack.
function ExcluirRegistroSelecionado() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    //gvDados.PerformCallback(); //byAlejandro
    return false;
}

// esta função chama o método no servidor responsável por compartilhar o registro selecionado
// o método será chamado por meio do objeto pnCallbackMensagem.
function onClick_btnSalvarCompartilhar() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallbackMensagem.PerformCallback("Compartilhar");
}

function btnNovo_Click() {
    onClickBarraNavegacao("Incluir", gvDados, pcDados, false);
    hfGeral.Set("TipoOperacao", "Incluir");
    TipoOperacao = "Incluir";
}

function btnEditar_Click() {
    onClickBarraNavegacao("Editar", gvDados, pcDados, false);
    hfGeral.Set("TipoOperacao", "Editar");
    TipoOperacao = "Editar";
}

function btnExcluir_Click() {
    onClickBarraNavegacao("Excluir", gvDados, pcDados);
}

function btnCompartilhar_click(grid) {
    // limpa o hidden field com a lista dos projetos 
    hfObjetos.Clear();
    hfGeral.Set("TipoOperacao", "Compartilhar");
    TipoOperacao = "Compartilhar";
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoModeloStatusReport;DescricaoModeloStatusReport', mostraPcCompartilharModeloStatusReport);
}

function onClick_CustomButtomGrid(s, e) {
    e.processOnServer = false;
    gvDados.SetFocusedRowIndex(e.visibleIndex);

    if (e.buttonID == 'btnNovo')
        btnNovo_Click();
    else if (e.buttonID == 'btnCompartilhar')
        btnCompartilhar_click(gvDados);
    else if (e.buttonID == 'btnEditar') {
        gvDados.GetRowValues(e.visibleIndex, 'IniciaisModeloControladoSistema', ConfiguraVisualizacaoItensSelecao);
        btnEditar_Click();
    }
    else if (e.buttonID == 'btnExcluir')
        btnExcluir_Click();
    else if (e.buttonID == 'btnDetalhesCustom')
        btnDetalhes_Click();
}

function mostraPcCompartilharModeloStatusReport(valores) {
    if (null != valores) {
        var descricaoModeloSR = valores[1] != null ? valores[1] : "";
        txtNomeModeloSR.SetText(descricaoModeloSR);
        txtNomeModelo_Associacao.SetText(descricaoModeloSR);
        gvAssociacaoModelos.PerformCallback(valores[0]);
    }
    pcCompartilharModelos.Show();
}

function btnDetalhes_Click() {
    OnGridFocusedRowChanged(gvDados, true);
    btnSalvar.SetVisible(false);
    hfGeral.Set("TipoOperacao", "Consultar");
    TipoOperacao = "Consultar";
    pcDados.Show();
}

//-------------------------------------------------------------------------------
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
//-------------------------------------------------------------------------------

// Esta função tem que retornar uma string.
//          ""  ::  se todas as validações estiverem OK
//    "<erro>"  ::  indicando o que deve ser corrigido
function validaCamposFormulario() {
    mensagemErro_ValidaCamposFormulario = "";
    var contador = 0;
    if (txtNomeNovoModeloRelatorio.GetText() == "") {
        contador++;
        mensagemErro_ValidaCamposFormulario += "\n " + contador + " - " + traducao.StatusReport_o_nome_do_modelo_de_status_report_deve_ser_informado;
    }


    if (ddlPeriodicidade.GetText() == "") {
        contador++;
        mensagemErro_ValidaCamposFormulario += "\n " + contador + " - " + traducao.StatusReport_a_periodicidade_deve_ser_informada;
    }

    //a espera deve ser menor que a quantidade de dias da periodicidade
    ValidaToleranciaPeriodicidade(contador);

    return mensagemErro_ValidaCamposFormulario;
}

function ValidaToleranciaPeriodicidade(contador) {
    var toleranciaPeriodicidade = parseInt(txtEspera.GetText());
    var periodo = ddlPeriodicidade.GetText();
    var intervaloDias = 0;

    if (periodo == traducao.StatusReport_di_ria) {
        intervaloDias = 1;
    }
    else if (periodo == traducao.StatusReport_semanal) {
        intervaloDias = 7;
    }
    else if (periodo == traducao.StatusReport_quinzenal) {
        intervaloDias = 13;
    }
    else if (periodo == traducao.StatusReport_mensal) {
        intervaloDias = 28;
    }
    else if (periodo == traducao.StatusReport_bimestral) {
        intervaloDias = 59;
    }
    else if (periodo == traducao.StatusReport_trimestral) {
        intervaloDias = 89;
    }
    else if (periodo == traducao.StatusReport_semestral) {
        intervaloDias = 180;
    }
    else if (periodo == traducao.StatusReport_anual) {
        intervaloDias = 360;
    }

    if (toleranciaPeriodicidade >= intervaloDias) {
        if (contador != null && contador != undefined) {
            contador++;
        }
        else {
            contador = 1;
        }
        mensagemErro_ValidaCamposFormulario += "\n" + contador + " - " + traducao.StatusReport_aten__o__o_valor_informado_no_campo__espera___d____deve_ser_menor_do_que_a_quantidade_de_dias_da_periodicidade_escolhida_;
    }

}

function LimpaCamposFormulario() {
    var tOperacao = ""

    try {
        txtNomeNovoModeloRelatorio.SetText("");
        txtEspera.SetText("");
        ddlPeriodicidade.SetValue(1);

        ceAnaliseValorAgregado.SetChecked(false);
        ceComentarFinanceiro.SetChecked(false);
        ceComentarFisico.SetChecked(false);
        ceComentarioGeral.SetChecked(false);
        cePlanoAcaoGeral.SetChecked(false);
        ceComentarMetasResultados.SetChecked(false);
        ceComentarQuestoes.SetChecked(false);
        ceComentarRiscos.SetChecked(false);
        ceContratos.SetChecked(false);
        ceDetalhesCusto.SetChecked(false);
        ceDetalhesReceita.SetChecked(false);
        ceInformacoesCusto.SetChecked(false);
        //ceGraficoDesempenhoCusto.SetChecked(false);
        //ceGraficoDesempenhoFisico.SetChecked(false);
        //ceGraficoDesempenhoReceita.SetChecked(false);
        ceListaDesempenhoRecursos.SetChecked(false);
        ceListaPendencias.SetChecked(false);
        ceMarcosAtrasados.SetChecked(false);
        ceMarcosConcluidos.SetChecked(false);
        ceMarcosProximoPeriodo.SetChecked(false);
        ceMetasResultados.SetChecked(false);
        ceQuestoesAtivas.SetChecked(false);
        ceQuestoesResolvidas.SetChecked(false);
        ceRiscosAtivos.SetChecked(false);
        ceRiscosEliminados.SetChecked(false);
        ceTarefasAtrasadas.SetChecked(false);
        ceTarefasConcluidasPeriodo.SetChecked(false);
        ceTarefasProximoPeriodo.SetChecked(false);

        if (window.heGlossario)
            heGlossario.SetHtml("");

        desabilitaHabilitaComponentes();
        HabilitaDeshabilitaComponentes();

        lbItensDisponiveis.ClearItems();
        lbItensSelecionados.ClearItems();

        //pcDados.AdjustSize();
    } catch (e) {
    }
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoModeloStatusReport;DescricaoModeloStatusReport;CodigoEntidade;CodigoPeriodicidade;DescricaoPeriodicidade_PT;ListaTarefasAtrasadas;ListaTarefasConcluidas;ListaTarefasFuturas;ListaMarcosConcluidos;ListaMarcosAtrasados;ListaMarcosFuturos;GraficoDesempenhoFisico;ListaRH;GraficoDesempenhoCusto;ListaContasCusto;GraficoDesempenhoReceita;ListaContasReceita;AnaliseValorAgregado;ListaRiscosAtivos;ListaRiscosEliminados;ListaQuestoesAtivas;ListaQuestoesResolvidas;ListaMetasResultados;ListaPendenciasToDoList;ComentarioGeral;ComentarioFisico;ComentarioFinanceiro;ComentarioRisco;ComentarioQuestao;ComentarioMeta;ListaContratos;ComentarioPlanoAcao;ToleranciaPeriodicidade;ListaEntregas;IniciaisModeloControladoSistema', MontaCamposFormulario);
    }
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'IniciaisModeloControladoSistema', mostrarOpcaoBoletimQuinzenal);
}

function mostrarOpcaoBoletimQuinzenal(iniciaisModeloControladoSistema) {
    RecomporVisualizacaoOpcoes();
    var opcoes = new Array();
    switch (iniciaisModeloControladoSistema) {
        case "BLTQ":
            opcoes = hfGeral.Get("Associar_RAP").split(",");
            break;
        case "BLT_AE_UN":
            opcoes = hfGeral.Get("Associar_RPU").split(",");
            break;
        case "BLT_AE_VI":
            opcoes = hfGeral.Get("Associar_BAE").split(",");
            break;
        case "GRF_BOLHAS":
            opcoes = hfGeral.Get("Associar_GRF_BOLHA").split(",");
            break;
        case "BLT_RAPU":
            opcoes = hfGeral.Get("Associar_RAPU").split(",");
            break;
        case "PADRAONOVO":
            opcoes = hfGeral.Get("Associar_SR_Novo").split(",");
            break;
        case "SR_MDL0007":
            opcoes = hfGeral.Get("Associar_SR_MDL0007").split(",");
            break;
        case "SR_PPJ01":
            opcoes = hfGeral.Get("Associar_SR_PPJ01").split(",");
            break;
        default:
            opcoes = hfGeral.Get("Associar_SR").split(",");
            break;
    }
    for (var cont = 0; cont < opcoes.length; cont++) {
        var opcao = opcoes[cont].toLowerCase();
        var element = document.getElementById(opcao);
        if (element != null)
            element.style.display = "";
    }
}

function RecomporVisualizacaoOpcoes() {
    document.getElementById("pr").style.display = "none";
    document.getElementById("en").style.display = "none";
    document.getElementById("un").style.display = "none";
    document.getElementById("cp").style.display = "none";
}

//-------------------------------------------------------------------------------
/* Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    
0 - CodigoModeloStatusReport;       6 - ListaTarefasConcluidas;     12 - ListaRH;                   18 - ListaRiscosAtivos;          24 - ComentarioGeral;
1 - DescricaoModeloStatusReport;    7 - ListaTarefasFuturas;        13 - GraficoDesempenhoCusto;    19 - ListaRiscosEliminados;      25 - ComentarioFisico;
2 - CodigoEntidade;                 8 - ListaMarcosConcluidos;      14 - ListaContasCusto;          20 - ListaQuestoesAtivas;        26 - ComentarioFinanceiro;
3 - CodigoPeriodicidade;            9 - ListaMarcosAtrasados;       15 - GraficoDesempenhoReceita;  21 - ListaQuestoesResolvidas;    27 - ComentarioRisco;
4 - DescricaoPeriodicidade_PT;      10- ListaMarcosFuturos;         16 - ListaContasReceita;        22 - ListaMetasResultados;       28 - ComentarioQuestao;
5 - ListaTarefasAtrasadas;          11 - GraficoDesempenhoFisico;   17 - AnaliseValorAgregado;      23 - ListaPendenciasToDoList;    29 - ComentarioMeta;
30 - ListaContratos
31 - ComentarioPlanoAcao
32 - ToleranciaPeriodicidade
33 - ListaEntregas
34 - IniciaisModeloControladoSistema
*/
//-------------------------------------------------------------------------------
function MontaCamposFormulario(values) {
    LimpaCamposFormulario();

    if (values) {
        var codigoModeloStatusReport = values[0];
        var IniciaisModeloControladoSistema = values[34];
        hfGeral.Set("hfCodigoModeloStatusReport", codigoModeloStatusReport);
        txtNomeNovoModeloRelatorio.SetText((values[1] != null ? values[1] : ""));
        txtEspera.SetText((values[32] != null ? values[32] + '' : "0"));

        (values[3] == null ? ddlPeriodicidade.SetText("") : ddlPeriodicidade.SetValue(values[3]));
        ceRiscosAtivos.SetChecked(true);

        ceAnaliseValorAgregado.SetChecked((values[17] == "S"));
        ceComentarFinanceiro.SetChecked((values[26] == "S"));
        ceComentarFisico.SetChecked((values[25] == "S"));
        ceComentarioGeral.SetChecked((values[24] == "S"));
        cePlanoAcaoGeral.SetChecked((values[31] == "S"));
        ceComentarMetasResultados.SetChecked((values[29] == "S"));
        ceComentarQuestoes.SetChecked((values[28] == "S"));
        ceComentarRiscos.SetChecked((values[27] == "S"));
        ceContratos.SetChecked((values[30] == "S"));
        if (IniciaisModeloControladoSistema == 'PADRAONOVO')
            ceInformacoesCusto.SetChecked((values[14] == "S"));
        else
            ceDetalhesCusto.SetChecked((values[14] == "S"));
        ceDetalhesReceita.SetChecked((values[16] == "S"));
        //ceGraficoDesempenhoCusto.SetChecked((values[13] == "S"));
        //ceGraficoDesempenhoFisico.SetChecked((values[11] == "S"));
        //ceGraficoDesempenhoReceita.SetChecked((values[15] == "S"));
        ceListaDesempenhoRecursos.SetChecked((values[12] == "S"));
        ceListaPendencias.SetChecked((values[23] == "S"));
        ceMarcosAtrasados.SetChecked((values[9] == "S"));
        ceMarcosConcluidos.SetChecked((values[8] == "S"));
        ceMarcosProximoPeriodo.SetChecked((values[10] == "S"));
        ceMetasResultados.SetChecked((values[22] == "S"));
        ceQuestoesAtivas.SetChecked((values[20] == "S"));
        ceQuestoesResolvidas.SetChecked((values[21] == "S"));
        ceRiscosAtivos.SetChecked((values[18] == "S"));
        ceRiscosEliminados.SetChecked((values[19] == "S"));
        ceTarefasAtrasadas.SetChecked((values[5] == "S"));
        ceTarefasConcluidasPeriodo.SetChecked((values[6] == "S"));
        ceTarefasProximoPeriodo.SetChecked((values[7] == "S"));
        ceListaEntregas.SetChecked((values[33] == "S"));

        var mostrar =
            IniciaisModeloControladoSistema == null ||
            IniciaisModeloControladoSistema == "" || 
            IniciaisModeloControladoSistema == "PADRAONOVO";
        ocultarExibirCheckBoxes(mostrar);
    }
}

function ocultarExibirCheckBoxes(mostrar) {
    var displayStyle = mostrar ? "" : "none";
    if (document.getElementById("CheckBox").style.display != displayStyle) {
        //pcDados.Hide();
        document.getElementById("CheckBox").style.display = displayStyle;
        pcDados.AdjustSize();
        pcDados.SetWidth(730);
    }
    pcDados.Show();
}

function HabilitaDeshabilitaComponentes() {

    if (TipoOperacao != null) {
        hfGeral.Set("TipoOperacao", TipoOperacao);
        hfGeral.Set("BotaoGvDado", TipoOperacao);
    }
    else {
        hfGeral.Set("TipoOperacao", "Consultar");
        hfGeral.Set("BotaoGvDado", "Consultar");
    }

    tOperacao = hfGeral.Get("TipoOperacao").toString();

    if (tOperacao == "Incluir")//if(TipoOperacao=="Incluir")
    {
        hfGeral.Set("modoGridDados", "Incluir");
    }
}

function desabilitaHabilitaComponentes() {
    if ("Incluir" == TipoOperacao || "Editar" == TipoOperacao) {
        ceAnaliseValorAgregado.SetEnabled(true);
        ceComentarFinanceiro.SetEnabled(true);
        ceComentarFisico.SetEnabled(true);
        ceComentarioGeral.SetEnabled(true);
        cePlanoAcaoGeral.SetEnabled(true);
        ceComentarMetasResultados.SetEnabled(true);
        ceComentarQuestoes.SetEnabled(true);
        ceComentarRiscos.SetEnabled(true);
        ceContratos.SetEnabled(true);
        ceDetalhesCusto.SetEnabled(true);
        ceInformacoesCusto.SetEnabled(true);
        ceDetalhesReceita.SetEnabled(true);
        //ceGraficoDesempenhoCusto.SetEnabled(true);
        //ceGraficoDesempenhoFisico.SetEnabled(true);
        //ceGraficoDesempenhoReceita.SetEnabled(true);
        ceListaDesempenhoRecursos.SetEnabled(true);
        ceListaPendencias.SetEnabled(true);
        ceMarcosAtrasados.SetEnabled(true);
        ceMarcosConcluidos.SetEnabled(true);
        ceMarcosProximoPeriodo.SetEnabled(true);
        ceMetasResultados.SetEnabled(true);
        ceQuestoesAtivas.SetEnabled(true);
        ceQuestoesResolvidas.SetEnabled(true);
        ceRiscosAtivos.SetEnabled(true);
        ceRiscosEliminados.SetEnabled(true);
        ceTarefasAtrasadas.SetEnabled(true);
        ceTarefasConcluidasPeriodo.SetEnabled(true);
        ceTarefasProximoPeriodo.SetEnabled(true);
    }
    else {
        ceAnaliseValorAgregado.SetEnabled(false);
        ceComentarFinanceiro.SetEnabled(false);
        ceComentarFisico.SetEnabled(false);
        ceComentarioGeral.SetEnabled(false);
        cePlanoAcaoGeral.SetEnabled(false);
        ceComentarMetasResultados.SetEnabled(false);
        ceComentarQuestoes.SetEnabled(false);
        ceComentarRiscos.SetEnabled(false);
        ceContratos.SetEnabled(false);
        ceDetalhesCusto.SetEnabled(false);
        ceInformacoesCusto.SetEnabled(false);
        ceDetalhesReceita.SetEnabled(false);
        //ceGraficoDesempenhoCusto.SetEnabled(false);
        //ceGraficoDesempenhoFisico.SetEnabled(false);
        //ceGraficoDesempenhoReceita.SetEnabled(false);
        ceListaDesempenhoRecursos.SetEnabled(false);
        ceListaPendencias.SetEnabled(false);
        ceMarcosAtrasados.SetEnabled(false);
        ceMarcosConcluidos.SetEnabled(false);
        ceMarcosProximoPeriodo.SetEnabled(false);
        ceMetasResultados.SetEnabled(false);
        ceQuestoesAtivas.SetEnabled(false);
        ceQuestoesResolvidas.SetEnabled(false);
        ceRiscosAtivos.SetEnabled(false);
        ceRiscosEliminados.SetEnabled(false);
        ceTarefasAtrasadas.SetEnabled(false);
        ceTarefasConcluidasPeriodo.SetEnabled(false);
        ceTarefasProximoPeriodo.SetEnabled(false);
    }
}

function habilitaBotoesListBoxes() {
    var habilita = window.btnSalvarCompartilhar && btnSalvarCompartilhar.GetVisible();
    btnAddAll.SetEnabled(habilita && lbItensDisponiveis.GetItemCount() > 0);
    btnAddSel.SetEnabled(habilita && lbItensDisponiveis.GetSelectedItem() != null);

    btnRemoveAll.SetEnabled(habilita && lbItensSelecionados.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(habilita && lbItensSelecionados.GetSelectedItem() != null);
}

function setListBoxItemsInMemory(listBox, inicial) {
    if ((null != listBox) && (null != inicial)) {

        var strConteudo = "";
        var idLista = inicial;
        var nQtdItems = listBox.GetItemCount();
        var item;

        for (var i = 0; i < nQtdItems; i++) {
            item = listBox.GetItem(i);
            strConteudo = strConteudo + item.text + __cwf_delimitadorValores + item.value + __cwf_delimitadorElementoLista;
        }

        if (0 < strConteudo.length)
            strConteudo = strConteudo.substr(0, strConteudo.length - 1);

        // grava a string no hiddenField
        hfObjetos.Set(idLista, strConteudo);
    }
}

function preencheListBoxesTela(codigoModeloStatusReport, tipoObjeto) {
    if (null != codigoModeloStatusReport) {
        lbItensDisponiveis.ClearItems();
        lbItensSelecionados.ClearItems();

        if ((null != codigoModeloStatusReport)) {
            var parametro = "POPLBX_" + tipoObjeto + codigoModeloStatusReport;

            // busca os projetos da base de dados
            lbItensDisponiveis.PerformCallback(parametro);
            lbItensSelecionados.PerformCallback(parametro);
        }
    }
}

/*------------------------------------------------------------------------------------
A ação 'Compartilhar', pode generar uma excepçao no momento de alterar as listas
do projecto que compartilhan o indicador. Caso que tenha excepção se listaram
quais projetos e quais excepçõe tem.
A mensagem de erro se encontra no componente hfGeral.Get('ErrorSalvar').
-----------------------------------------------------------------------------------oK*/
function local_onEnd_pnCallback(s, e) {
    if ("Compartilhar" == s.cp_LastOperation) {
        if (hfGeral.Contains("StatusSalvar")) {
            var status = hfGeral.Get("StatusSalvar");
            if (status != "1") {
                var mensagem = hfGeral.Get("ErroSalvar");
                window.top.mostraMensagem(mensagem, 'erro', true, false, null);
            }
            else {
                //Caso não tenha excepção, mostra o mensagem de sucesso.
                window.top.mostraMensagem(traducao.StatusReport_dados_gravados_com_sucesso_, 'sucesso', false, false, null);
            }
        }
    }
    else {
        if (window.onEnd_pnCallback)
            onEnd_pnCallback();

        if ("Incluir" == s.cp_OperacaoOk)
            window.top.mostraMensagem(traducao.StatusReport_modelo_de_status_report_inclu_do_com_sucesso_, 'sucesso', false, false, null);
        else if ("Editar" == s.cp_OperacaoOk)
            window.top.mostraMensagem(traducao.StatusReport_dados_gravados_com_sucesso_, 'sucesso', false, false, null);
        else if ("Compartilhar" == s.cp_OperacaoOk)
            window.top.mostraMensagem(traducao.StatusReport_dados_gravados_com_sucesso_, 'sucesso', false, false, null);
        else if ("Excluir" == s.cp_OperacaoOk)
            gvDados.PerformCallback(); //byAlejandro
    }
}

/*------------------------------------------------------------------------------------
Função utilzada para mostrar mensaje temporal na tela, indicando o sucesso da ação.
-----------------------------------------------------------------------------------oK*/

function mostrarAssociacaoModelosSR(tipoObjeto, descricaoTipoObjeto) {
    var codigoModeloStatusReport = gvDados.GetRowKey(gvDados.GetFocusedRowIndex());
    txtTipoObjeto.SetText(descricaoTipoObjeto);
    preencheListBoxesTela(codigoModeloStatusReport, tipoObjeto);
    pcAssociacao.Show();
}

function gvAssociacaoModelos_CustomButtonClick(s, e) {
    e.processOnServer = false;
    if (e.buttonID == "btnExcluirAssociacaoModelo") {
        rowIndexGlobal = e.visibleIndex;
        window.top.mostraMensagem(traducao.StatusReport_deseja_excluir_a_associa__o_do_modelo_ao_objeto_, 'confirmacao', true, true, excluiAssociacaoModeloStatusReport);
    }
}

function ConfiguraVisualizacaoItensSelecao(tipo) {
    var indicaPadrao = tipo == null || tipo == '' || !(tipo.toLowerCase().indexOf('novo') > -1);

    var mostrar =
            tipo == "" ||
            tipo == null ||
            tipo == undefined ||
            tipo.toLowerCase().indexOf("padrao") > -1;
    ocultarExibirCheckBoxes(mostrar);

    if (indicaPadrao) {
        ceListaEntregas.SetChecked(false);
    }
    else {
        ceComentarFisico.SetChecked(false);
        ceComentarRiscos.SetChecked(false);
        ceComentarQuestoes.SetChecked(false);
        ceComentarFinanceiro.SetChecked(false);
        ceComentarMetasResultados.SetChecked(false);
        ceListaPendencias.SetChecked(false);
        cePlanoAcaoGeral.SetChecked(false);
        ceAnaliseValorAgregado.SetChecked(false);
        ceDetalhesCusto.SetChecked(false);
        ceDetalhesReceita.SetChecked(false);
    }

    ceComentarFisico.SetVisible(indicaPadrao);
    ceComentarRiscos.SetVisible(indicaPadrao);
    ceComentarQuestoes.SetVisible(indicaPadrao);
    ceComentarFinanceiro.SetVisible(indicaPadrao);
    ceComentarMetasResultados.SetVisible(indicaPadrao);
    ceListaPendencias.SetVisible(indicaPadrao);
    cePlanoAcaoGeral.SetVisible(indicaPadrao);
    ceAnaliseValorAgregado.SetVisible(indicaPadrao);
    ceDetalhesCusto.SetVisible(indicaPadrao);
    ceDetalhesReceita.SetVisible(indicaPadrao);
    lblFinanceiro.SetVisible(indicaPadrao);
    ceListaEntregas.SetVisible(!indicaPadrao);
    ceInformacoesCusto.SetVisible(!indicaPadrao);
}

function AbreFormularioNovoSR(tipo) {
    pcNovoStatusReport.Hide();

    ConfiguraVisualizacaoItensSelecao(tipo);

    onClickBarraNavegacao('Incluir', gvDados, pcDados);
    TipoOperacao = 'Incluir';
    hfGeral.Set("TipoSR", tipo);
}