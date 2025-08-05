
function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    //------------Obtendo data e hora actual
    var dataInicio = new Date(ddlInicioDeVigencia.GetValue());
    var dataInicioP = (dataInicio.getMonth() + 1) + "/" + dataInicio.getDate() + "/" + dataInicio.getFullYear();
    var dataInicioC = Date.parse(dataInicioP);

    var dataTermino = new Date(ddlTerminoDeVigencia.GetValue());
    var dataTerminoP = (dataTermino.getMonth() + 1) + "/" + dataTermino.getDate() + "/" + dataTermino.getFullYear();
    var dataTerminoC = Date.parse(dataTerminoP);

    if (ddlTipoContrato.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_o_instrumento_deve_ser_informado_;
    }
    if (hfGeral.Get("UsaNumeracaoAutomatica") == "N" && txtNumeroContrato.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_o_n_mero_do_contrato_deve_ser_informado_;
    }
    if (ddlSituacao.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_situa__o_deve_ser_informada_;
    }
    if (ddlRazaoSocial.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_raz_o_social_deve_ser_informada_;
    }
    if (mmObjeto.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_descri__o_do_objeto_deve_ser_informada_;
    }
    if (ddlUnidadeGestora.GetValue() == null || ddlUnidadeGestora.GetValue() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_unidade_gestora_deve_ser_informada_;
        //ddlInteresado.Focus();
    }
    if (ddlMunicipio.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_o_munic_pio_deve_ser_informado_;
    }
    if (ddlsegmento.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_o_segmento_deve_ser_informado_;
    }
    if (ddlSituacao.GetValue() != "P" && (ddlInicioDeVigencia.GetValue() == null || ddlInicioDeVigencia.GetText() == "")) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_da_os_deve_ser_informada_;
    }
    if (ddlSituacao.GetValue() != "P" && (ddlTerminoDeVigencia.GetValue() == null || ddlTerminoDeVigencia.GetText() == "")) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_de_t_rmino_deve_ser_informada_;
    }
    if (ddlSituacao.GetValue() != "P" && ((ddlInicioDeVigencia.GetValue() != null) && (ddlTerminoDeVigencia.GetValue() != null))) {
        if (dataInicioC > dataTerminoC) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_da_os_n_o_pode_ser_maior_que_a_data_de_t_rmino_;
            retorno = false;
        }
    }
    if (ddlSituacao.GetValue() != "P" && (ddlAssinatura.GetValue() == null || ddlAssinatura.GetText() == "")) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_de_assinatura_deve_ser_informada_;
    }
    if (ddlCriterioReajuste.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_o_crit_rio_de_reajuste_deve_ser_informado_;
    }
    if (ddlTipoContratacao.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_o_tipo_de_contrata__o_deve_ser_informado_;
    }
    if (ddlOrigem.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_origem_deve_ser_informada_;
    }
    if (ddlFonte.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_fonte_deve_ser_informada_;
    }
    if (ddlCriterioMedicao.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_o_crit_rio_de_medi__o_deve_ser_informado_;
    }
    if (ddlGestorContrato.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_o_gestor_deve_ser_informado_;
    }
    //customizações 09/2012


    if (ddlDataInicioGarantia.GetValue() != null && ddlDataInicioGarantia.GetText() != "") {
        if (ddlDataInicioGarantia.GetValue() < ddlInicioDeVigencia.GetValue()) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_de_in_cio_da_garantia_n_o_pode_ser_menor_que_a_data_de_in_cio_de_vig_ncia_do_contrato_;
        }
        //        if (ddlDataInicioGarantia.GetValue() > ddlTerminoDeVigencia.GetValue()) {
        //            numAux++;
        //            mensagem += "\n" + numAux + ") A data de Início da garantia não pode ser maior que a data de Término de vigência do contrato.";
        //        }
        if (ddlDataTerminoGarantia.GetValue() == null || ddlDataTerminoGarantia.GetText() == "") {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_de_t_rmino_da_garantia_deve_ser_informada_;
        }
    }

    if (ddlDataTerminoGarantia.GetValue() != null && ddlDataTerminoGarantia.GetText() != "") {
        if (ddlDataTerminoGarantia.GetValue() < ddlInicioDeVigencia.GetValue()) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_de_t_rmino_da_garantia_n_o_pode_ser_menor_que_a_data_de_in_cio_de_vig_ncia_do_contrato_;
        }
        if (ddlDataTerminoGarantia.GetValue() < ddlDataInicioGarantia.GetValue()) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_de_t_rmino_da_garantia_n_o_pode_ser_menor_que_a_data_de_in_cio_da_garantia_;
        }
        if (ddlDataInicioGarantia.GetValue() == null || ddlDataInicioGarantia.GetText() == "") {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_a_data_de_in_cio_da_garantia_deve_ser_informada_;
        }
    }

    if (ddlRetencaoGarantia.GetValue() == "GR" && (txtPercGarantia.GetText() == "0,0000" || txtPercGarantia.GetText() == "" || txtPercGarantia.GetText() == null)) {
        numAux++;
        mensagem += "\n" + numAux + ") % " + traducao.DetalhesContratosExtendido_garantia_deve_ser_informada_;
    }

    if (ddlRetencaoGarantia.GetValue() == "CF" && (txtValorGarantia.GetText() == "0,00" || txtValorGarantia.GetText() == "" || txtValorGarantia.GetText() == null)) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.DetalhesContratosExtendido_valor_garantia_deve_ser_informada_;
    }




    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = traducao.DetalhesContratosExtendido_alguns_dados_s_o_de_preenchimento_obrigat_rio_ + "\n\n" + mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco

    callbackSalvar.PerformCallback();

    return false;
}

function mostraCampoGarantia(s, e) {
    if (s.GetValue() == 'CF') {
        document.getElementById('tdlblValorGarantia').style.display = '';
        document.getElementById('tdValorGarantia').style.display = '';
        document.getElementById('tdlblPercGarantia').style.display = 'none';
        document.getElementById('tdPercGarantia').style.display = 'none';
    } else if (s.GetValue() == 'GR') {
        document.getElementById('tdlblPercGarantia').style.display = '';
        document.getElementById('tdPercGarantia').style.display = '';
        document.getElementById('tdlblValorGarantia').style.display = 'none';
        document.getElementById('tdValorGarantia').style.display = 'none';
    } else {
        document.getElementById('tdlblValorGarantia').style.display = 'none';
        document.getElementById('tdValorGarantia').style.display = 'none';
        document.getElementById('tdlblPercGarantia').style.display = 'none';
        document.getElementById('tdPercGarantia').style.display = 'none';
    }
}

function mostraMsgSub() {

    window.top.mostraMensagem(traducao.DetalhesContratosExtendido_voc__est__alterando_a_op__o_permitir_subcontrata__o_para_n_o_ + ' \n\n' + traducao.DetalhesContratosExtendido_quando_salvar__se_houver_registro_de_sub_contratadas_para_este_contrato_os_mesmos_ser_o_excluidos_, 'Atencao', true, false, null);
}



//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    //if (callbackSalvar.cp_Status != null && callbackSalvar.cp_Status == "1")
    //    window.parent.gvDados.PerformCallback('A');
    atualizaAlturaDosFrames();
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
    else {
        if (textAreaElement.name.indexOf("mmObservacoes") >= 0)
            lblCantCaraterOb.SetText(text.length);
        else
            lblCantCarater.SetText(text.length);
    }
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function onInit_mmObjeto(s, e) {
    try { return setMaxLength(s.GetInputElement(), 4000); }
    catch (e) { }
}

function onInit_mmObservacoes(s, e) {
    try { return setMaxLength(s.GetInputElement(), 1000); }
    catch (e) { }
}

function onInit_mmEncerramento(s, e) {
    try { return setMaxLength(s.GetInputElement(), 2000); }
    catch (e) { }
}


function carregaDadosFornecedor(municipioFornecedor, contatoFornecedor) {
    txtGestorContratada.SetText(contatoFornecedor);
    txtOrigemContratada.SetText(municipioFornecedor);
}

function novaRazaoSocial() {
    window.top.showModal2('../Administracao/frmCadastroPessoa.aspx', traducao.DetalhesContratosExtendido_nova_raz_o_social, 900, 435, funcaoPosModal, null);
}

function funcaoPosModal(valor) {
    if (valor != null && valor != '')
        ddlRazaoSocial.PerformCallback(valor.toString());
}

function atualizaAbas() {

    if (tabControl.GetActiveTabIndex() == 1) {
        if (atualizarURLFornecedor == '') {
            document.getElementById('frmFornecedor').src = tabControl.cp_TabFornecedor + '&CP=' + ddlRazaoSocial.GetValue();
            document.getElementById('frmFornecedor2').src = tabControl.cp_TabFornecedor2;
            atualizarURLFornecedor = 'N';
        }
    }
    else if (tabControl.GetActiveTabIndex() == 2) {
        if (atualizarURLFornecedor2 == '') {
            document.getElementById('frmFornecedor3').src = tabControl.cp_TabFornecedor3;
            atualizarURLFornecedor2 = 'N';
        }
    }
    else if (tabControl.GetActiveTabIndex() == 3) {
        //        if (atualizarURLPrevisao == '') {
        document.getElementById('frmPrevisao').src = tabControl.cp_TabPrevisao;
        //            atualizarURLPrevisao = 'N';
        //        }
    }
    else if (tabControl.GetActiveTabIndex() == 4) {
        if (atualizarURLParcelas == '') {
            document.getElementById('frmParcelas').src = tabControl.cp_TabParcelas;
            atualizarURLParcelas = 'N';
        }
    }

    else if (tabControl.GetActiveTabIndex() == 5) {
        if (atualizarURLAnexos == '') {
            document.getElementById('frmAnexos').src = tabControl.cp_TabAnexos;
            atualizarURLAnexos = 'N';
        }
    }
    else if (tabControl.GetActiveTabIndex() == 6) {
        if (atualizarURLAditivos == '') {
            document.getElementById('frmAditivos').src = tabControl.cp_TabAditivos;
            atualizarURLAditivos = 'N';
        }
    }

    else if (tabControl.GetActiveTabIndex() == 7) {
        if (atualizarURLComentarios == '') {
            document.getElementById('frmComentarios').src = tabControl.cp_TabComentarios;
            atualizarURLComentarios = 'N';
        }
    }
    else if (tabControl.GetActiveTabIndex() == 8) {
        //if (atualizarURLAcessorios == '') {
        document.getElementById('frmAcessorios').src = tabControl.cp_TabAcessorios;
        //    atualizarURLAcessorios = 'N';
        //}
    }

    else if (tabControl.GetActiveTabIndex() == 9) {
        //if (atualizarURLReajuste == '') {
        document.getElementById('frmReajuste').src = tabControl.cp_TabReajuste;
        //    atualizarURLReajuste = 'N';
        //}
    }
}

function fechaFrame() {
    var fakeLink = document.createElement("a");

    if (typeof (fakeLink.click) == 'undefined')
        location.href = '../../branco.htm'; // sends referrer in FF, not in IE 
    else {
        fakeLink.href = '../../branco.htm';
        document.body.appendChild(fakeLink);
        fakeLink.click(); // click() method defined in IE only 
    }

    window.parent.pcDados.Hide();
}
