// JScript File
var codigoStatus = "";
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
    var tOperacao = ""

    try {// Função responsável por preparar os campos do formulário para receber um novo registro
        txtOrigemTarefaBanco.SetText("");
        txtDescricaoTarefaBanco.SetText("");
        txtInicioPrevistoBanco.SetText("");
        txtTerminoPrevistoBanco.SetText("");
        txtCodigoUsuarioResponsavelBanco.SetText("");
        txtEsforcoPrevistoBanco.SetText("");
        txtEstagioBanco.SetText("");
        txtCustoPrevistoBanco.SetText("");
        txtCustoRealBanco.SetText("");

        mmAnotacoesBanco.SetText("");

        ddlInicioReal.SetText("");
        ddlTerminoReal.SetText("");
        txtEsforcoReal.SetText("");
        ddlStatusTarefa.SetText("");
    } catch (e) { }
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoTarefa;DescricaoTarefa;InicioPrevisto;TerminoPrevisto;InicioReal;TerminoReal;CodigoUsuarioResponsavel;NomeUsuarioResponsavel;PercentualConcluido;Anotacoes;CodigoStatusTarefa;DescricaoStatusTarefa;EsforcoPrevisto;EsforcoReal;CustoPrevisto;CustoReal;Estagio;DescricaoOrigem;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    try {
        codigoStatus = values[10];
        txtOrigemTarefaBanco.SetText((values[17] != null ? values[17] : ""));
        txtDescricaoTarefaBanco.SetText((values[1] != null ? values[1] : ""));
        txtInicioPrevistoBanco.SetText((values[2] != null ? values[2] : ""));
        txtTerminoPrevistoBanco.SetText((values[3] != null ? values[3] : ""));
        txtCodigoUsuarioResponsavelBanco.SetText((values[7] != null ? values[7] : ""));
        txtEsforcoPrevistoBanco.SetText((values[12] != null ? values[12] : ""));
        txtEstagioBanco.SetText((values[16] != null ? values[16] : ""));
        txtCustoPrevistoBanco.SetText((values[14] != null ? values[14] : ""));
        txtCustoRealBanco.SetText((values[15] != null ? values[15] : ""));

        mmAnotacoesBanco.SetText((values[9] != null ? values[9] : ""));

        txtEsforcoReal.SetText((values[13] != null ? values[13] : ""));
        ddlStatusTarefa.SetValue((values[10] != null ? values[10] : ""));
        //ddlInicioReal.SetText((values[4] != null ? values[4]  : ""));
        //ddlTerminoReal.SetText((values[5] != null ? values[5]  : ""));
        (values[4] == null ? ddlInicioReal.SetText("") : ddlInicioReal.SetValue(values[4]));
        (values[5] == null ? ddlTerminoReal.SetText("") : ddlTerminoReal.SetValue(values[5]));

        desabilitaHabilitaComponentes();
    } catch (e) { }
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0)
        onClick_btnCancelar();
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = hfGeral.Get("TipoOperacao") == "Editar" && codigoStatus.toString() != "2" && codigoStatus.toString() != "3" ? true : false;

    mmAnotacoesBanco.SetEnabled(BoolEnabled);
    ddlInicioReal.SetEnabled(BoolEnabled);
    ddlTerminoReal.SetEnabled(BoolEnabled);
    txtEsforcoReal.SetEnabled(BoolEnabled);
    ddlStatusTarefa.SetEnabled(hfGeral.Get("TipoOperacao") == "Editar");
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
    var status = ddlStatusTarefa.GetText();

    //----------obtendo a data atual, início real e término real
    var dataAtual = new Date();
    var meuDataAtual = (dataAtual.getMonth() + 1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
    var dataHoje = Date.parse(meuDataAtual);

    var dataTermino;
    var dataInicio;

    if (ddlInicioReal.GetValue() != null) {
        var dataInicio = new Date(ddlInicioReal.GetValue());
        var meuDataInicio = (dataInicio.getMonth() + 1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
        dataInicio = Date.parse(meuDataInicio);
    } else {
        dataInicio = null;
    }

    if (ddlTerminoReal.GetValue() != null) {
        var dataTermino = new Date(ddlTerminoReal.GetValue());
        var meuDataTermino = (dataTermino.getMonth() + 1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
        dataTermino = Date.parse(meuDataTermino);
    } else {
        dataTermino = null;
    }


    if ((dataInicio != null)) {
        if (dataInicio > dataHoje) {
            mensagemError += ++numError + ") " + traducao.frameEspacoTrabalho_MinhaTarefas_a_data_de_in_cio_real_n_o_pode_ser_maior_a_data_atual_ + "\n";
            retorno = false;
        }
    }

    if ((dataTermino != null)) {
        if (status != "Concluída") {
            mensagemError += ++numError + ") " + traducao.frameEspacoTrabalho_MinhaTarefas_o_status_do_projeto_deve_ser_igual_a__conclu_do__se_o_t_rmino_real_estiver_informado_ + "\n";
            retorno = false;
        }
        if (dataInicio != null) {
            if (dataInicio > dataTermino) {
                mensagemError += ++numError + ") " + traducao.frameEspacoTrabalho_MinhaTarefas_a_data_de_t_rmino_real_n_o_pode_ser_menor_a_data_in_cio_real_ + "\n";
                retorno = false;
            }
        }
        if (dataInicio == null) {
            mensagemError += ++numError + ") " + traducao.frameEspacoTrabalho_MinhaTarefas_a_data_de_in_cio_real_deve_ser_informada_se_o_t_rmino_real_foi_preenchido__ + "\n";
            retorno = false;
        }
        if (dataTermino > dataHoje) {
            mensagemError += ++numError + ") " + traducao.frameEspacoTrabalho_MinhaTarefas_a_data_de_t_rmino_real_n_o_pode_ser_maior_que_a_data_atual_ + "\n";
            retorno = false;
        }

    }
    else {
        if (status == "Concluída") {
            mensagemError += ++numError + ") " + traducao.frameEspacoTrabalho_MinhaTarefas_o_projeto_s__poder__ter_status_igual_a_conclu_do_se_o_t_rmino_real_for_informado_ + "\n";
            retorno = false;
        }
        if (dataInicio == null) {
            if (txtEsforcoReal.GetText() != null && txtEsforcoReal.GetText() != "0" && txtEsforcoReal.GetText() != "") {
                mensagemError += ++numError + ") " + traducao.frameEspacoTrabalho_MinhaTarefas_s__indicar_esfor_o_real_si_in_cio_real_estiver_preenchido_ + "\n";
                retorno = false;
            }
        }
    }

    if (!retorno)
        window.top.mostraMensagem(mensagemError, 'erro', true, false, null);

    return retorno;
}