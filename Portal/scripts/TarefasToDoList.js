// JScript File
var codigoStatus = "";
function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario()
{
    var tOperacao = ""

    try {// Função responsável por preparar os campos do formulário para receber um novo registro

        txtDescricaoTarefaBanco.SetText("");
        ddlResponsavel.SetValue(null);
        ddlPrioridade.SetValue("B");
        ddlStatusTarefa.SetValue(null);
        ddlInicioPrevisto.SetValue(null);
        ddlTerminoPrevisto.SetValue(null);
        txtCustoPrevistoBanco.SetText("");
        txtCustoRealBanco.SetText("");
        ddlInicioReal.SetValue(null);
        ddlTerminoReal.SetValue(null);
        txtEsforcoPrevistoBanco.SetText("");
        txtEsforcoReal.SetText("");
        mmAnotacoesBanco.SetText("");
        txtOrigemTarefaBanco.SetText("");
    }catch(e){}
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetSelectedFieldValues('CodigoTarefa;DescricaoTarefa;InicioPrevisto;TerminoPrevisto;InicioReal;TerminoReal;CodigoUsuarioResponsavel;NomeUsuarioResponsavel;PercentualConcluido;Anotacoes;CodigoStatusTarefa;DescricaoStatusTarefa;EsforcoPrevisto;EsforcoReal;CustoPrevisto;CustoReal;Prioridade;DescricaoOrigem', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(valores)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    //este comando ja esta sendo chamado no custombuttonclick da grid
    //LimpaCamposFormulario();

    var values = valores[0];

    try {
        codigoStatus = values[10];
        txtDescricaoTarefaBanco.SetText((values[1] != null ? values[1] : ""));
        ddlResponsavel.SetValue((values[6] != null ? values[6] : ""));
        ddlPrioridade.SetValue((values[16] != null ? values[16] : ""));
        ddlStatusTarefa.SetValue((values[10] != null ? values[10] : ""));
        ddlInicioPrevisto.SetValue((values[2] != null ? values[2] : ""));
        ddlTerminoPrevisto.SetValue((values[3] != null ? values[3] : ""));
        txtCustoPrevistoBanco.SetText((values[14] != null ? values[14].toString().replace('.', ',') : ""));
        txtCustoRealBanco.SetText((values[15] != null ? values[15].toString().replace('.', ',') : ""));
        (values[4] == null ? ddlInicioReal.SetText("") : ddlInicioReal.SetValue(values[4]));
        (values[5] == null ? ddlTerminoReal.SetText("") : ddlTerminoReal.SetValue(values[5]));
        txtEsforcoPrevistoBanco.SetText((values[12] != null ? values[12].toString() : ""));
        txtEsforcoReal.SetText((values[13] != null ? values[13].toString() : ""));
        mmAnotacoesBanco.SetText((values[9] != null ? values[9] : ""));
        ddlResponsavel.SetText((values[7] != null ? values[7] : ""));
        txtOrigemTarefaBanco.SetText((values[17] != null ? values[17] : ""));
        desabilitaHabilitaComponentes();
      }catch(e){}
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0 )
        onClick_btnCancelar();
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = ((hfGeral.Get("TipoOperacao") == "Editar" && codigoStatus.toString() != "2" && codigoStatus.toString() != "3") || hfGeral.Get("TipoOperacao") == "Incluir") ? true : false;

    txtDescricaoTarefaBanco.SetEnabled(BoolEnabled);
    ddlResponsavel.SetEnabled(BoolEnabled);
    ddlPrioridade.SetEnabled(BoolEnabled);
    ddlStatusTarefa.SetEnabled(hfGeral.Get("TipoOperacao") == "Editar" || hfGeral.Get("TipoOperacao") == "Incluir");
    ddlInicioPrevisto.SetEnabled(BoolEnabled);
    ddlTerminoPrevisto.SetEnabled(BoolEnabled);
    txtCustoPrevistoBanco.SetEnabled(BoolEnabled);
    txtCustoRealBanco.SetEnabled(BoolEnabled);
    ddlInicioReal.SetEnabled(BoolEnabled);
    ddlTerminoReal.SetEnabled(BoolEnabled);
    txtEsforcoPrevistoBanco.SetEnabled(BoolEnabled);
    txtEsforcoReal.SetEnabled(BoolEnabled);
    mmAnotacoesBanco.SetEnabled(BoolEnabled);
}

function showDetalhe()
{
    OnGridFocusedRowChanged(gvDados, true);
    btnSalvar.SetVisible(false);
    hfGeral.Set("TipoOperacao", "Consultar");
    pcDados.Show();
}

function verificarDadosPreenchidos() {
    var mensagemError = "";
    var retorno = true;
    var numError = 0;
    var status = ddlStatusTarefa.GetValue();

    //----------obtendo a data atual, início real e término real
    var dataAtual = new Date();
    var meuDataAtual = (dataAtual.getMonth() + 1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
    var dataHoje = Date.parse(meuDataAtual);

    var dataTermino;
    var dataInicio;
    var dataTerminoPrevisto;
    var dataInicioPrevisto;

    if (ddlInicioPrevisto.GetValue() != null) {
        var dataInicioPrevisto = new Date(ddlInicioPrevisto.GetValue());
        var meuDataInicio = (dataInicioPrevisto.getMonth() + 1) + '/' + dataInicioPrevisto.getDate() + '/' + dataInicioPrevisto.getFullYear();
        dataInicioPrevisto = Date.parse(meuDataInicio);
    } else {
        dataInicioPrevisto = null;
    }

    if (ddlTerminoPrevisto.GetValue() != null) {
        var dataTerminoPrevisto = new Date(ddlTerminoPrevisto.GetValue());
        var meuDataTermino = (dataTerminoPrevisto.getMonth() + 1) + '/' + dataTerminoPrevisto.getDate() + '/' + dataTerminoPrevisto.getFullYear();
        dataTerminoPrevisto = Date.parse(meuDataTermino);
    } else {
        dataTerminoPrevisto = null;
    }

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

    if (txtDescricaoTarefaBanco.GetText() == "") {
        mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_descri__o_da_tarefa_deve_ser_informada_ + "\n";
        retorno = false;
    }
    if (ddlResponsavel.GetValue() == null) {
        mensagemError += ++numError + ") " + traducao.TarefasToDoList_o_respons_vel_deve_ser_informado_ + "\n";
        retorno = false;
    }
    if (ddlPrioridade.GetValue() == null) {
        mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_prioridade_deve_ser_informada_ + "\n";
        retorno = false;
    }
    if (ddlStatusTarefa.GetValue() == null) {
        mensagemError += ++numError + ") " + traducao.TarefasToDoList_o_status_deve_ser_informado_ + "\n";
        retorno = false;
    }
    if (ddlInicioPrevisto.GetValue() == null) {
        mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_data_de_in_cio_previsto_deve_ser_informada_ + "\n";
        retorno = false;
    }
    if (ddlTerminoPrevisto.GetValue() == null) {
        mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_data_de_t_rmino_previsto_deve_ser_informada_ + "\n";
        retorno = false;
    }

    if ((dataTerminoPrevisto != null)) {
        if (dataInicioPrevisto != null) {
            if (dataInicioPrevisto > dataTerminoPrevisto) {
                mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_data_de_t_rmino_previsto_n_o_pode_ser_menor_que_a_data_in_cio_previsto_ + "\n";
                retorno = false;
            }
        }

    }

    if ((dataInicio != null)) {
        if (dataInicio > dataHoje) {
            mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_data_de_in_cio_real_n_o_pode_ser_maior_que_a_data_atual_ + "\n";
            retorno = false;
        }
        if (status == "4") {
            mensagemError += ++numError + ") " + traducao.TarefasToDoList_A_Data_De_Inicio_Real_Nao_Pode_Ser_Informada_Para_Este_Status_De_Tarefa_ + "\n";
            retorno = false;
        }
    } else {
        if (status == "2" || status == "1") {
            mensagemError += ++numError + ") " + traducao.TarefasToDoList_A_Data_De_Inicio_Real_Deve_Ser_Informada_Para_Este_Status_De_Tarefa_ + "\n";
            retorno = false;
        }
    }

    if ((dataTermino != null)) {
        if (status == "4" || status == "1") {
            mensagemError += ++numError + ") " + traducao.TarefasToDoList_A_Data_De_Termino_Real_Nao_Pode_Ser_Informada_Para_Este_Status_De_Tarefa_ + "\n";
            retorno = false;
        }
        if (status != "2") {
            mensagemError += ++numError + ") " + traducao.TarefasToDoList_o_status_da_tarefa_deve_ser_igual_a__conclu_do__se_o_t_rmino_real_estiver_informado_ + "\n";
            retorno = false;
        }
        if (dataInicio != null) {
            if (dataInicio > dataTermino) {
                mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_data_de_t_rmino_real_n_o_pode_ser_menor_que_a_data_in_cio_real_ + "\n";
                retorno = false;
            }
        }
        if (dataInicio == null) {
            mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_data_de_in_cio_real_deve_ser_informada_se_o_t_rmino_real_foi_preenchido_ + "\n";
            retorno = false;
        }
        if (dataTermino > dataHoje) {
            mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_data_de_t_rmino_real_n_o_pode_ser_maior_que_a_data_atual_ + "\n";
            retorno = false;
        }

    }
    else {
        if (status == "2") {
            mensagemError += ++numError + ") " + traducao.TarefasToDoList_a_tarefa_s__poder__ter_status_igual_a_conclu_do_se_o_t_rmino_real_for_informado_ + "\n";
            retorno = false;
        }
        if (dataInicio == null) {
            if (txtEsforcoReal.GetText() != null && parseFloat(txtEsforcoReal.GetValue().replace(',', '.')) != 0 && txtEsforcoReal.GetText() != "") {
                mensagemError += ++numError + ") " + traducao.TarefasToDoList_s__indicar_esfor_o_real_se_o_in_cio_real_estiver_preenchido + "!\n";
                retorno = false;
            }

            if (txtCustoRealBanco.GetValue() != null && parseFloat(txtCustoRealBanco.GetValue().replace(',', '.')) != 0 && txtCustoRealBanco.GetText() != "") {
                mensagemError += ++numError + ") " + traducao.TarefasToDoList_s__indicar_custo_real_se_o_in_cio_real_estiver_preenchido_ + "\n";
                retorno = false;
            }
        }
    }

    if (parseFloat(txtEsforcoReal.GetValue().replace(',', '.')) != 0 && txtEsforcoReal.GetText() != "" && status == "4") {
        mensagemError += ++numError + ") " + traducao.TarefasToDoList_O_Esforco_Real_Nao_Pode_Ser_Informado_Para_Este_Status_De_Tarefa_ + "\n";
        retorno = false;
    }

    if (parseFloat(txtCustoRealBanco.GetValue().replace(',', '.')) != 0 && txtCustoRealBanco.GetText() != "" && status == "4") {
        mensagemError += ++numError + ") " + traducao.TarefasToDoList_O_Custo_Real_Nao_Pode_Ser_Informada_Para_Este_Status_De_Tarefa_ + "\n";
        retorno = false;
    }

    if (!retorno)
        window.top.mostraMensagem(mensagemError, traducao.TarefasToDoList_atencao, true, false, null);

    return retorno;
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
