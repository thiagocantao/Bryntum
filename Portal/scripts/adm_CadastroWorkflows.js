var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";
var codigoFluxoSelecionado;

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    if(txtNomeFluxo.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.adm_CadastroWorkflows_o_nome_do_modelo_de_fluxo_deve_ser_informado_;
        txtNomeFluxo.Focus();
    }    
    else if(ddlStatusFluxo.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.adm_CadastroWorkflows_o_status_do_modelo_de_fluxo_deve_ser_informado_;
        ddlStatusFluxo.Focus();
    }    
    
    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallback
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallback
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function LimpaCamposFormulario() {
    codigoFluxoSelecionado = -1;
    txtNomeFluxo.cpCodigoFluxo = -1;
    txtNomeFluxo.SetText(null);
    txtDescricao.SetText(null);
    txtObservacao.SetText(null);
    ddlStatusFluxo.SetSelectedIndex(-1);
    ddlGrupoFluxo.SetSelectedIndex(-1);
    txtIniciaisFluxo.SetText(null);
    lblSelecaoStatus.SetText(null);
    lbDisponiveisStatus.ClearItems();
    lbSelecionadosStatus.ClearItems();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoFluxo;NomeFluxo;Descricao;Observacao;StatusFluxo;CodigoGrupoFluxo;IniciaisFluxo;', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{
    if( null != valores)
    {
        var codigoFluxo = valores[0];
        txtNomeFluxo.cpCodigoFluxo = codigoFluxo;
        txtNomeFluxo.SetText(valores[1] == null ? "" : valores[1].toString());
        txtDescricao.SetText(valores[2] == null ? "" : valores[2].toString());
        txtObservacao.SetText(valores[3] == null ? "" : valores[3].toString());
        ddlStatusFluxo.SetValue(valores[4] == null ? "" : valores[4].toString());
        ddlGrupoFluxo.SetValue(valores[5] == null ? "" : valores[5].toString());
        txtIniciaisFluxo.SetText(valores[6] == null ? "" : valores[6].toString());

        gvTiposProjetos.SetFocusedRowIndex(-1);
        codigoFluxoSelecionado = codigoFluxo;

        txtNomeFluxoAssociado.cpCodigoFluxo = codigoFluxo;
        txtNomeFluxoAssociado.SetText(valores[1]);

        lblSelecaoStatus.SetText(null);
        lbDisponiveisStatus.ClearItems();
        lbSelecionadosStatus.ClearItems();

        if (null != codigoFluxo) {
            gvTiposProjetos.PerformCallback("POPFRM_" + codigoFluxo);
            gvProjetosFluxo.PerformCallback(codigoFluxo);
        }
    }
}

function pcDados_OnPopup(s,e)
{


    var largura = Math.max(0, document.documentElement.clientWidth) - 100;
    var altura = Math.max(0, document.documentElement.clientHeight) - 155;

    s.SetWidth(largura);
    s.SetHeight(altura);

    txtObservacao.SetHeight(altura - 345);
    gvProjetosFluxo.SetHeight(altura - 165);
    s.UpdatePosition();

    desabilitaHabilitaComponentes();
}

function pcDados_OnCloseUp(s,e)
{
    
}


function desabilitaHabilitaComponentes()
{
    var situacao = TipoOperacao != "Consultar";
    txtNomeFluxo.SetEnabled(situacao);
    ddlStatusFluxo.SetEnabled(situacao);
    txtDescricao.SetEnabled(situacao);
    txtObservacao.SetEnabled(situacao);
    ddlGrupoFluxo.SetEnabled(situacao);
    txtIniciaisFluxo.SetEnabled(situacao);
}


//-----------
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao()
{
    onClick_btnCancelar()
}


//------------------- disparo de fluxos automatico
function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChangedDisparoFluxo(grid, forcarMontaCampos) {
    if (window.pcDadosDisparoFluxoProjetos && forcarMontaCampos) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoFluxo;CodigoProjeto;NomeProjeto;DataAtivacao;IdentificadorUsuarioAtivacao;DataDesativacao;IdentificadorUsuarioDesativacao;DataPrimeiraExecucao;CodigoPeriodicidade;DataProximaExecucao;indicaControlado;UsuarioAtivacao;UsuarioDesativacao;DescricaoPeriodicidade_PT;existeRegistro;', MontaCamposFormularioDisparoFluxo);
    }
}

function MontaCamposFormularioDisparoFluxo(values) {
    LimpaCamposFormularioDisparoFluxo();
    var CodigoFluxo = (values[0] != null ? values[0] : "");
    var CodigoProjeto = (values[1] != null ? values[1] : "");
    var NomeProjeto = (values[2] != null ? values[2] : "");
    var DataAtivacao = (values[3] != null ? values[3] : "");
    var IdentificadorUsuarioAtivacao = (values[4] != null ? values[4] : "");
    var DataDesativacao = (values[5] != null ? values[5] : "");
    var IdentificadorUsuarioDesativacao = (values[6] != null ? values[6] : "");
    var DataPrimeiraExecucao = (values[7] != null ? values[7] : "");
    var CodigoPeriodicidade = (values[8] != null ? values[8] : "");
    var DataProximaExecucao = (values[9] != null ? values[9] : "");
    var indicaControlado = (values[10] != null ? values[10] : "");
    var UsuarioAtivacao = (values[11] != null ? values[11] : "");
    var UsuarioDesativacao = (values[12] != null ? values[12] : "");
    var DescricaoPeriodicidade = (values[13] != null ? values[13] : "");
    var existeRegistro = (values[14] != null ? values[14] : "");

    cmbPeriodicidade.SetValue(CodigoPeriodicidade);
    cmbPeriodicidade.SetText(DescricaoPeriodicidade);
    dteInicio.SetValue(DataPrimeiraExecucao != "" ? DataPrimeiraExecucao : null);
    txtProximaExecucao.SetText(DataProximaExecucao);

    pcDadosDisparoFluxoProjetos.SetHeaderText("Projeto: " + NomeProjeto);

    hfGeral1.Set("CodigoFluxo", CodigoFluxo);
    hfGeral1.Set("CodigoProjeto", CodigoProjeto);
    hfGeral1.Set("ExisteRegistro", existeRegistro);
    hfGeral1.Set("codigoPeriodicidade", CodigoPeriodicidade);

    ckbIndicaControlado.SetChecked(Trim(indicaControlado) == "S");
    desabilitaHabilitaComponentesDisparoFluxo();

}

function onClickBarraNavegacaoDisparoFluxo(idBotao, grid, popup) {
    if (!window.hfGeral1)
        window.top.mostraMensagem(traducao.adm_CadastroWorkflows_o_componente___hfgeral___n__o_foi_encontrado___neste_componente_deve_ser_colocado_dentro_do_componente___pncallbackgvprojetosfluxo__, 'erro', true, false, null);

    if (!window.OnGridFocusedRowChangedDisparoFluxo)
        window.top.mostraMensagem(traducao.adm_CadastroWorkflows_o_m_todo___ongridfocusedrowchangeddisparofluxo___n_o_foi_implementado_, 'erro', true, false, null);

    hfGeral1.Set("KeyFieldValueDisparoFluxo", grid.GetRowKey(grid.GetFocusedRowIndex()));
//    var rowIndex = grid.GetFocusedRowIndex();
//    if ( -1 < rowIndex )
//        grid.GetRowValues(rowIndex, 'existeRegistro', preencheExisteRegistro);

     
    // window.top.mostraMensagem(idBotao, 'Atencao', true, false, null);
    // se entrou no modo de edição 
    if (idBotao == "Incluir" || idBotao == "Editar") {
        if (!window.LimpaCamposFormularioDisparoFluxo)
            window.top.mostraMensagem(traducao.adm_CadastroWorkflows_o_m_todo___limpacamposformulario___n_o_foi_implementado_, 'erro', true, false, null);
        else if (!window.MontaCamposFormularioDisparoFluxo)
            window.top.mostraMensagem(traducao.adm_CadastroWorkflows_o_m_todo___montacamposformulario___n_o_foi_implementado_, 'erro', true, false, null);
        else {
            TipoOperacao = idBotao;
            hfGeral1.Set("TipoOperacao", TipoOperacao);
            if (idBotao == "Incluir")
                LimpaCamposFormularioDisparoFluxo();
            if (idBotao == "Editar")
                OnGridFocusedRowChangedDisparoFluxo(grid, true);  // true é para forçar a chamada da função MontaCampos
            if (popup != null)
                popup.Show();
            btnSalvarDisparo.SetVisible(true);
        }
    }
    else if (idBotao == "Excluir") {
        if (window.ExcluirRegistroSelecionadoDisparoFluxo && grid.GetVisibleRowsOnPage() > 0) {
            if (confirm(traducao.adm_CadastroWorkflows_deseja_realmente_desativar_o_disparo_autom_tico_para_este_projeto_)) {
                TipoOperacao = idBotao;
                hfGeral1.Set("TipoOperacao", TipoOperacao);
                ExcluirRegistroSelecionadoDisparoFluxo();
            }
        }
        else
            window.top.mostraMensagem(traducao.adm_CadastroWorkflows_o_m_todo___excluirregistroselecionadodisparofluxo___n_o_foi_implementado_, 'erro', true, false, null);
    }
}

//function preencheExisteRegistro(valores) {

//    if (null != valores) {
//        if (window.hfGeral)
//            hfGeral1.Set("ExisteRegistro", valores[0]);
//    }
//}

function LimpaCamposFormularioDisparoFluxo() {
    cmbPeriodicidade.SetValue(null);
    dteInicio.SetValue(null);
    ckbIndicaControlado.SetChecked(false);
    desabilitaHabilitaComponentesDisparoFluxo();
    hfGeral1.Set("CodigoFluxo", "");
    hfGeral1.Set("CodigoProjeto", "");
    hfGeral1.Set("ExisteRegistro", "");
    hfGeral1.Set("codigoPeriodicidade", "");

}

function LimpaCamposDisparoFluxo() {
    if (!ckbIndicaControlado.GetChecked()) {
        cmbPeriodicidade.SetValue(null);
        dteInicio.SetValue(null);
        hfGeral1.Set("codigoPeriodicidade", "");
    }
    desabilitaHabilitaComponentesDisparoFluxo();
}
function onClickAlteraDisparo(s, e) {
    
    var aRetorno = cmbPeriodicidade.GetText().split(';');
    var soma = parseInt(aRetorno[1]);
    var primeiraExecucao = dteInicio.GetText(); //formato dd/mm/yyy
    var dataInicial = primeiraExecucao.substring(0, 10);
//    var horaInicial = primeiraExecucao.substring(11, 13);
    if (dataInicial == '') return;
//    if (parseInt(horaInicial) < 0 || parseInt(horaInicial) > 24 || horaInicial == NaN || horaInicial == '') return;
//    if (!primeiraExecucao.match('[0-9]{1,2}\/[0-9]{1,2}\/[0-9]{4}')) return;
    dataInicial = dataInicial.split('/');

    var date = new Date();
    date.setFullYear(dataInicial[2]);
    date.setMonth(dataInicial[1] - 1); // mes de 0 a 11
    date.setDate(dataInicial[0]);
//    date.setHours(horaInicial);
//    date.setMinutes(0);

    date.setDate(date.getDate() + soma); 
    
    txtProximaExecucao.SetText(date.toLocaleString());
    txtProximaExecucao.SetText(date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear());

    hfGeral1.Set("codigoPeriodicidade", cmbPeriodicidade.GetValue().toString());
}


function desabilitaHabilitaComponentesDisparoFluxo() {
    cmbPeriodicidade.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && ckbIndicaControlado.GetChecked());
    dteInicio.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && ckbIndicaControlado.GetChecked());
    ckbIndicaControlado.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}


function onClick_btnSalvarDisparo() {
    if (window.validaCamposFormularioDisparo) {
        if (validaCamposFormularioDisparo() != "") {
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormularioDisparo, 'erro', true, false, null);
            return false;
        }
        else {
            if (window.SalvarCamposFormularioDisparo) {
                if (SalvarCamposFormularioDisparo()) {
                    pcDisparo.Hide();
                    //habilitaModoEdicaoBarraNavegacao(false, gvProjetosFluxo);
                    return true;
                }
            }
            else {
                window.top.mostraMensagem(traducao.adm_CadastroWorkflows_o_m_todo_n_o_foi_implementado_, 'erro', true, false, null);
            }

        }
    }
}

function onClick_btnFecharDisparo() {
    //habilitaModoEdicaoBarraNavegacao(false, gvProjetosFluxo);
    pcDadosDisparoFluxoProjetos.Hide();

    var CodigoFluxo = (pnCallbackgvProjetosFluxo.cp_CodigoFluxo != null) ? pnCallbackgvProjetosFluxo.cp_CodigoFluxo : hfGeral1.Get("CodigoFluxo");

    gvProjetosFluxo.PerformCallback(CodigoFluxo);

    TipoOperacao = "Consultar";
    hfGeral1.Set("TipoOperacao", TipoOperacao);
    return true;
}

function validaCamposFormularioDisparo() {

    mensagemErro_ValidaCamposFormularioDisparo = "";
    var numAux = 0;
    var mensagem = "";

    if (dteInicio.GetValue() == null && dteInicio.GetText() == "" && ckbIndicaControlado.GetChecked()) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.adm_CadastroWorkflows_a_data_da_primeira_execu__o_deve_ser_informada_;
    }

    if (cmbPeriodicidade.GetValue() == null && cmbPeriodicidade.GetText() == "" && ckbIndicaControlado.GetChecked()) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.adm_CadastroWorkflows_a_periodicidade_de_execu__o_deve_ser_informada_;
    } 


    if (mensagem != "") {
        mensagemErro_ValidaCamposFormularioDisparo = mensagem;
    }

    return mensagemErro_ValidaCamposFormularioDisparo;
}

function SalvarCamposFormularioDisparo() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral1.Set("StatusSalvar", "0");
    pnCallbackgvProjetosFluxo.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionadoDisparoFluxo() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral1.Set("StatusSalvar", "0");
    pnCallbackgvProjetosFluxo.PerformCallback(TipoOperacao);
    return false;
}

function onEnd_pnCallbackDisparoFluxo() {
    if (window.TipoOperacao) {
        if (hfGeral1.Get("StatusSalvar") == "1") {
            if (TipoOperacao == "Incluir") {
                TipoOperacao = "Editar";
                hfGeral1.Set("TipoOperacao", TipoOperacao);
            }
            window.top.mostraMensagem(hfGeral1.Get("SucessoSalvar"), 'sucesso', false, false, null);
            if (window.posSalvarComSucesso)
                window.posSalvarComSucesso();
            else
                onClick_btnFecharDisparo();
        }
        else if (hfGeral1.Get("StatusSalvar") == "0") {
            mensagemErro = hfGeral1.Get("ErroSalvar");

            if (TipoOperacao == "Excluir") {
                // se existe um tratamento de erro especifico da opçao que está sendo executada
                if (window.trataMensagemErro)
                    mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
                else // caso contrário, usa o tratamento padrão
                {
                    // se for erro de Chave Estrangeira (FK)
                    if (mensagemErro != null && mensagemErro.indexOf('REFERENCE') >= 0)
                        mensagemErro = traducao.adm_CadastroWorkflows_o_registro_n_o_pode_ser_exclu_do_pois_est__sendo_utilizado_por_outro;
                }
            }
            else if (TipoOperacao == "Incluir") {
                // se existe um tratamento de erro especifico da opçao que está sendo executada
                if (window.trataMensagemErro)
                    mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
                else // caso contrário, usa o tratamento padrão
                {
                    // se for erro de Chave Estrangeira (FK)
                    if (mensagemErro != null && mensagemErro.indexOf('REFERENCE') >= 0)
                        mensagemErro = traducao.adm_CadastroWorkflows_o_registro_n_o_pode_ser_inclu_do_pois_j__existe_no_banco_de_dados_;
                }
            }
            if (mensagemErro != "" && mensagemErro != undefined)
                window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
        }
    }
}

//--------------------------------------bloco de scripts da copia do fluxo

function validaCamposCopiaFluxo() {
    //debugger
    var retorno = true;
    var mensagemAlert = "";

    if (ddlFluxoDestino.GetValue() == null || ddlFluxoDestino.GetValue() == -1) {
        mensagemAlert += traducao.adm_CadastroWorkflows_informe_o_workflow_de_destino_para_a_c__pia + "\n";
        retorno = false;
    }
    else {
        hfStatusCopiaFluxo.Set("CodigoFluxoDestino", ddlFluxoDestino.GetValue()); 
    }

    if (retorno == false) {
        window.top.mostraMensagem(mensagemAlert, 'erro', true, false, null);
    }
    return retorno;
}



function onClick_btnSalvarCopiaFluxo() {
    if (confirm(traducao.adm_CadastroWorkflows_confirma_a_c_pia_do_fluxo_selecionado_ + '\n\n' + traducao.adm_CadastroWorkflows_esta_opera__o_n_o_pode_ser_desfeita_)) {
        if (validaCamposCopiaFluxo()) {
            hfStatusCopiaFluxo.PerformCallback("Editar");
        }
    }

    return false;
}


function onClick_btnCancelarCopiaFluxo() {
    pcCopiaFluxo.Hide();
    return true;
}

function hfStatusCopiaFluxo_onEndCallback() {
    if (hfStatusCopiaFluxo.Get("StatusSalvar") == "1") {
        if (window.posSalvarComSucessoCopiaFluxo)
            window.posSalvarComSucessoCopiaFluxo();
        gvDados.PerformCallback("");
        onClick_btnCancelarCopiaFluxo();
    }
    else if (hfStatusCopiaFluxo.Get("StatusSalvar") == "0") {
        mensagemErro = hfStatusCopiaFluxo.Get("ErroSalvar");
        mostraPopupMensagemGravacaoCopiaFluxo(mensagemErro);

    }
}

function pcCopiaFluxo_OnPopup(s, e) {
    // limpa o hidden field com a lista de status
    //hfStatusCopiaFluxo.Clear();
    ddlFluxoDestino.SetSelectedIndex(-1);

}

function posSalvarComSucessoCopiaFluxo() {
    mostraPopupMensagemGravacaoCopiaFluxo(traducao.adm_CadastroWorkflows_dados_gravados_com_sucesso_);
}

//-------
function mostraPopupMensagemGravacaoCopiaFluxo(acao) {
    lblAcaoGravacao.SetText(acao);
    pcUsuarioIncluido.Show();
    setTimeout('fechaTelaEdicaoCopiaFluxo();', 4500);
}

function fechaTelaEdicaoCopiaFluxo() {
    pcUsuarioIncluido.Hide();
    onClick_btnCancelarCopiaFluxo()
}    


//fim do bloco da copia

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 100;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
