// JScript File
var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";


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

//-------------------------------------------------------------------------------
//Function haciendo referencia a los CustomButtons de gvDados.
//-------------------------------------------------------------------------------
function btnNovo_Click() {
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
    hfGeral.Set("TipoOperacao", "Incluir");
    TipoOperacao = "Incluir";
}

function btnEditar_Click() {
    pcDados.Show();
    onClickBarraNavegacao("Editar", gvDados, pcDados);
    hfGeral.Set("TipoOperacao", "Editar");
    TipoOperacao = "Editar";
}

function btnExcluir_Click() {
    onClickBarraNavegacao("Excluir", gvDados, pcDados);
}

function onClick_CustomButtomGrid(s, e) {
    e.processOnServer = false;
    gvDados.SetFocusedRowIndex(e.visibleIndex);
    btnSalvar1.SetVisible(true);
    if (e.buttonID == 'btnNovo')
        btnNovo_Click();
    else if (e.buttonID == 'btnCompartilhar')
        btnCompartilhar_click(gvDados);
    else if (e.buttonID == 'btnEditar')
        btnEditar_Click();
    else if (e.buttonID == 'btnExcluir')
        btnExcluir_Click();
    else if (e.buttonID == 'btnDetalhesCustom')
        btnDetalhes_Click();
}

function btnDetalhes_Click() {
    pcDados.Show();
    OnGridFocusedRowChanged(gvDados, true);
    btnSalvar1.SetVisible(false);
    hfGeral.Set("TipoOperacao", "Consultar");
    TipoOperacao = "Consultar";
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
    if (txtIndicador.GetText() == "") {
        contador++;
        mensagemErro_ValidaCamposFormulario += "\n" + contador + " - " + traducao.IndicadoresOperacional_o_indicador_deve_ser_informado;
    }

    if (ddlResponsavelIndicador.GetValue() == null || isNaN(ddlResponsavelIndicador.GetValue())) {
        contador++;
        mensagemErro_ValidaCamposFormulario += "\n" + contador + " - " + traducao.IndicadoresOperacional_o_respons_vel_pelo_indicador_n_o_foi_informado_ou_est__com_valor_inv_lido_;
    }


    if (rblCriterio.GetValue() == "A" && cmbAgrupamentoDaMeta.GetText() == "") {
        contador++;
        mensagemErro_ValidaCamposFormulario += "\n" + contador + " - " + traducao.IndicadoresOperacional_o_agrupamento_da_meta_deve_ser_informado;
    }
    if (rblTipoIndicador.GetValue() == null) {
        contador++;
        mensagemErro_ValidaCamposFormulario += "\n" + contador + " - " + traducao.IndicadoresOperacional_o_tipo_de_indicador_deve_ser_informado_;
    }
        


    //Ao momento de validar a formula, tenho qeu olhear que o modo de logeo da entidad nao tenha
    //ao modoLancamentoResultadoProjeto como 'Indicador'.

    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario() {
    var tOperacao = ""

    try {
        //Aba Indicador Operacional.
        txtIndicador.SetText("");
        cmbAgrupamentoDaMeta.SetText("");
        ddlUnidadeMedida.SetValue(1);
        ddlCasasDecimais.SetValue(0);
        ddlPolaridade.SetValue("POS");
        ddlResponsavelIndicador.SetText("");
        if (window.heGlossario)
            heGlossario.SetHtml("");
        //Aba Dado Indicador Operacional.

        desabilitaHabilitaComponentes();
        HabilitaDeshabilitaComponentes();

        rblCriterio.SetValue("A");
        rblTipoIndicador.SetValue("");
        pcDados.AdjustSize();
    } catch (e) {
    }
}

function OnGridFocusedRowChanged(grid) {
    if ((window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoIndicador;NomeIndicador;CodigoUnidadeMedida;GlossarioIndicador;CasasDecimais;Polaridade;TipoIndicador;CodigoUsuarioResponsavel;CodigoFuncaoAgrupamentoMeta;IndicaCriterio;PermitirAlteracaoCampos;ResponsavelObjeto', MontaCamposFormulario);
    }
}

//-------------------------------------------------------------------------------
/* Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    
0 - CodigoIndicador;        6 - TipoIndicador;               
1 - NomeIndicador;          7 - CodigoUsuarioResponsavel;                 
2 - CodigoUnidadeMedida;    8 - CodigoFuncaoAgrupamentoMeta;       
3 - GlossarioIndicador;     9 - IndicaCriterio;          
4 - CasasDecimais;          10- PermitirAlteracaoCampos;    
5 - Polaridade;             11- ResponsavelObjeto  
                                          
*/
//-------------------------------------------------------------------------------
function MontaCamposFormulario(values) {
    LimpaCamposFormulario();

    //var modoLancamentoResultadoProjeto = hfGeral.Get("modoLancamentoResultadoProjeto").toString();

    if (values) {
        var codigoIndicador = values[0];
        hfGeral.Set("hfCodigoIndicador", codigoIndicador);
        //Aba Indicador Operacional
        txtIndicador.SetText((values[1] != null ? values[1] : ""));
        (values[8] == null ? cmbAgrupamentoDaMeta.SetText("") : cmbAgrupamentoDaMeta.SetValue(values[8]));
        (values[2] == null ? ddlUnidadeMedida.SetText("") : ddlUnidadeMedida.SetValue(values[2]));
        (values[4] == null ? ddlCasasDecimais.SetValue("0") : ddlCasasDecimais.SetValue(values[4]));
        (values[5] == null ? ddlPolaridade.SetText("") : ddlPolaridade.SetValue(values[5]));
        (values[7] == null ? ddlResponsavelIndicador.SetText("") : ddlResponsavelIndicador.SetValue(values[7]));
        var nomeResp = values[11];
        (values[11] == null ? ddlResponsavelIndicador.SetText("") : ddlResponsavelIndicador.SetText(nomeResp));
        if (window.heGlossario)
            heGlossario.SetHtml((values[3] != null ? values[3] : ""));
        var criterio = (values[9] != null ? values[9] : "A");
        var tipoIndicador = (values[6] != null ? values[6] : "");
        rblCriterio.SetValue(criterio);
        rblTipoIndicador.SetValue(tipoIndicador);

        if ("Incluir" == TipoOperacao || "Editar" == TipoOperacao) {
            var permitirAlteracaoCampos = true;
            txtIndicador.SetEnabled(permitirAlteracaoCampos);
            cmbAgrupamentoDaMeta.SetEnabled(permitirAlteracaoCampos && criterio == "A");
            ddlUnidadeMedida.SetEnabled(permitirAlteracaoCampos);
            ddlCasasDecimais.SetEnabled(permitirAlteracaoCampos);
            ddlPolaridade.SetEnabled(permitirAlteracaoCampos);
            rblCriterio.SetEnabled(permitirAlteracaoCampos);
            rblTipoIndicador.SetEnabled(permitirAlteracaoCampos);
            if ("Editar" == TipoOperacao) 
                rblTipoIndicador.SetEnabled(true);
        }

        //        if ("Indicador" == modoLancamentoResultadoProjeto)
        //            rblCriterio.SetValue(values[11] != null ? "A" : "S");

        pcDados.AdjustSize();
    }
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
}

// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// -------------------------------------------------------------------------------


function replaceAll(origem, antigo, novo) {
    var teste = 0;
    while (teste == 0) {
        if (origem.indexOf(antigo) >= 0) {
            origem = origem.replace(antigo, novo);
        }
        else
            teste = 1;
    }
    return origem;
}


/*-----------------------------------------------------------------------------------------
Função de perparar a tela de edição para o usuario cadastre/edite/consulte um indicador.

O cuidado qeu tem que ter ao preparar a tela de detalhes, e que tem qeu saver com qual 
modoLancamentoResultadoProjeto inicio a entidade, ja que sendo de tipo 'Indicador', não podera
mecher con os Dados do indicador cadastrado.

'indicador' :: TabControl.GetTabByName("TabDado").SetVisible(false);
---------------------------------------------------------------------------------------oK*/
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

    if (tOperacao == "Incluir") {
        var tab = TabControl.GetTab(0);
        TabControl.SetActiveTab(tab);
    }
}

function desabilitaHabilitaComponentes() {
    if ("Incluir" == TipoOperacao || "Editar" == TipoOperacao) {
        txtIndicador.SetEnabled(true);
        cmbAgrupamentoDaMeta.SetEnabled(true);
        ddlUnidadeMedida.SetEnabled(true);
        ddlCasasDecimais.SetEnabled(true);
        ddlPolaridade.SetEnabled(true);
        ddlResponsavelIndicador.SetEnabled(true);
        //Aba Dado Indicador Operacional.
        rblCriterio.SetEnabled(true);
        rblTipoIndicador.SetEnabled(true);
        if ("Editar" == TipoOperacao)
            rblTipoIndicador.SetEnabled(true);
    }
    else {
        txtIndicador.SetEnabled(false);
        cmbAgrupamentoDaMeta.SetEnabled(false);
        ddlUnidadeMedida.SetEnabled(false);
        ddlCasasDecimais.SetEnabled(false);
        ddlPolaridade.SetEnabled(false);
        ddlResponsavelIndicador.SetEnabled(false);
        //Aba Dado Indicador Operacional.
        rblCriterio.SetEnabled(false);
        rblTipoIndicador.SetEnabled(false);
    }
}

/*------------------------------------------------------------------------------------
A ação 'Compartilhar', pode generar uma excepçao no momento de alterar as listas
do projecto que compartilhan o indicador. Caso que tenha excepção se listaram
quais projetos e quais excepçõe tem.
A mensagem de erro se encontra no componente hfGeral.Get('ErrorSalvar').
-----------------------------------------------------------------------------------oK*/
function local_onEnd_pnCallback(s, e) {
    if (window.onEnd_pnCallback)
        onEnd_pnCallback();

    if ("Incluir" == s.cp_OperacaoOk)
        mostraMensagemOperacao(traducao.IndicadoresOperacional_indicador_inclu_do_com_sucesso_);
    else if ("Editar" == s.cp_OperacaoOk)
        mostraMensagemOperacao(traducao.IndicadoresOperacional_dados_gravados_com_sucesso_);
    else if ("Compartilhar" == s.cp_OperacaoOk)
        mostraMensagemOperacao(traducao.IndicadoresOperacional_dados_gravados_com_sucesso_);
    else if ("Excluir" == s.cp_OperacaoOk)
        gvDados.PerformCallback(); //byAlejandro
}

/*------------------------------------------------------------------------------------
Função utilzada para mostrar mensaje temporal na tela, indicando o sucesso da ação.
-----------------------------------------------------------------------------------oK*/
function mostraMensagemOperacao(acao) {
    lblAcaoGravacao.SetText(acao);
    pcOperMsg.Show();
    setTimeout('fechaTelaEdicao();', 1500);
}

function fechaTelaEdicao() {
    pcOperMsg.Hide();
}

function abreNovoIndicador() {
    btnSalvar1.SetVisible(true);
    onClickBarraNavegacao('Incluir', gvDados, pcDados);
    TipoOperacao = 'Incluir';
}