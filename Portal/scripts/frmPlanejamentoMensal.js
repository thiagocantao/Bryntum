function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
   mensagemErro_ValidaCamposFormulario = "";
   var numAux = 0;
    var mensagem = "";

    if (ddlAcao.GetValue() == null)
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_a__o_deve_ser_informada_;
    }

    if (ddlInstrutor.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_o_instrutor_respons_vel_deve_ser_informado_;
    }

    if (ddlMunicipio.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_o_munic_pio_deve_ser_informado_;
    }

    if (ddlInicio.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_data_de_in_cio_deve_ser_informada_;
    }

    if (ddlTermino.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_data_de_t_rmino_deve_ser_informada_;
    }

    if (ddlInicio.GetDate() > ddlTermino.GetDate()) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_data_de_inicio_n_o_deve_ser_maior_que_a_data_de_t_rmino_;
    }

    if (gvDados.cpExibirColunas) {
        if (ddlInicioReal.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_data_de_in_cio_real_deve_ser_informada_;
        }

        if (ddlTerminoReal.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_data_de_t_rmino_real_deve_ser_informada_;
        }

        if (ddlInicioReal.GetValue() > new Date()) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_data_de_in_cio_real_n_o_deve_ser_maior_que_a_data_atual_;
        }

        if (ddlTerminoReal.GetValue() > new Date()) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_data_de_t_rmino_real_n_o_deve_ser_maior_que_a_data_atual_;
        }

        if (ddlInicioReal.GetDate() > ddlTerminoReal.GetDate()) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.frmPlanejamentoMensal_a_data_de_inicio_real_n_o_deve_ser_maior_que_a_data_de_t_rmino_real_;
        }
    }
    
    if(mensagem != "")
    {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }
    
    return mensagemErro_ValidaCamposFormulario;
}
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
    ddlAcao.SetValue(null);
    ddlInstrutor.SetValue(null);
    ddlMunicipio.SetValue(null);
    ddlInicio.SetValue(null);
    ddlTermino.SetValue(null);
    txtValor.SetValue(null);
    cbAcaoExecutada.SetValue('S');
    ddlInicioReal.SetValue(null);
    ddlTerminoReal.SetValue(null);
    
    desabilitaHabilitaComponentes();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetSelectedFieldValues('CodigoItemAcao;CodigoConsultor;CodigoMunicipio;Inicio;Termino;Valor;AcaoExecutada;DataInicioReal;DataTerminoReal;AcaoPlanejada', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(valores)
{
    var values = valores[0];
    desabilitaHabilitaComponentes();
    
    var acao = values[0];
    var consultor = values[1];
    var municipio = values[2];
    var inicio = values[3];
    var termino = values[4];
    var valor = values[5];
    var acaoExecutada = (values[6] == 'S' || values[6] == 's');
    var inicioReal = values[7];
    var terminoReal = values[8];
    var acaoPlanejada = (values[9] == 'S' || values[9] == 's');

    ddlAcao.SetValue(acao);
    ddlInstrutor.SetValue(consultor);
    ddlMunicipio.SetValue(municipio);
    ddlInicio.SetValue(inicio);
    ddlTermino.SetValue(termino);
    txtValor.SetValue(valor);
    ddlInicioReal.SetValue(inicioReal);
    ddlTerminoReal.SetValue(terminoReal);

    //cbAcaoExecutada.SetChecked(acaoExecutada);
    if (gvDados.cpExibirColunas)
        controlaCamposHabilitadorPrestacaoContas(acaoPlanejada);
}

function controlaCamposHabilitadorPrestacaoContas(acaoPlanejada) {
    ddlInstrutor.SetEnabled(!acaoPlanejada);
    ddlMunicipio.SetEnabled(!acaoPlanejada);
    ddlAcao.SetEnabled(!acaoPlanejada);
    ddlInicio.SetEnabled(!acaoPlanejada);
    ddlTermino.SetEnabled(!acaoPlanejada);

    ddlInicioReal.SetEnabled(acaoPlanejada);
    ddlTerminoReal.SetEnabled(acaoPlanejada);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    var habilitado = window.TipoOperacao && TipoOperacao != "Consultar";

    ddlAcao.SetEnabled(habilitado);
    ddlInstrutor.SetEnabled(habilitado);
    ddlMunicipio.SetEnabled(habilitado);
    ddlInicio.SetEnabled(habilitado);
    ddlTermino.SetEnabled(habilitado);
    txtValor.SetEnabled(false);
    cbAcaoExecutada.SetEnabled(habilitado);
    ddlInicioReal.SetEnabled(habilitado);
    ddlTerminoReal.SetEnabled(habilitado);

    if (habilitado && gvDados.cpExibirColunas)
        controlaCamposHabilitadorPrestacaoContas(false);
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

function IncluirPrestacaoContas() {
    popupMenu.Show();
}

function popupMenu_ItemClick(s, e) {
    switch (e.item.name) {
        case 'itemNovaAcaoNaoPlanejada':
            onClickBarraNavegacao('Incluir', gvDados, pcDados);
            break;
        case 'itemRegistrarAcoesPlanejadas':
            var cw = gvDados.cpCodigoWorkflow;
            var ci = gvDados.cpCodigoInstancia;
            var callbackFunc = function () {
                gvDados.Refresh();
                window.parent.atualizaValor();
            }
            //Bug 595: [SENAR] - [Homologação] - Fluxos com formulário de realização mensal
            window.top.showModal(window.top.pcModal.cp_Path + 'TelasClientes/frmSelecaoAcoesPlanejadasPrestacaoContas.aspx?cw=' + cw + '&ci=' + ci, 'Selecionar ações planejadas', screen.width - 100, window.top.innerHeight - 150, callbackFunc);
            break;
    }
}