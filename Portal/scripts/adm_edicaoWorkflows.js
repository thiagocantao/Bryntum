// JScript File
var __wf_acaoWfObj = new xAcaoWf();
var selectionStart;
var selectionEnd;


/*-----------------------------------------------------------------------------
    Función: inicializarPropiedadesWF()
    Descripción: Prepara los datos que seran reflejados en le popup Propiedades.
------------------------------------------------------------------------------*/
function inicializarPropiedadesWF() {
    txtDescricaoVersaoWf.SetText(hfValoresTela.Get("descricaoVersao"));
    memObservacaoWf.SetText(hfValoresTela.Get("observacaoVersao"));
    txtAlturaWf.SetText(hfValoresTela.Get("AlturaDivFlash"));
    txtLarguraWf.SetText(hfValoresTela.Get("LarguraDivFlash"));
}

function insereUmTextoNoMemo(aspxMemo, textoAInserir, imagem) {

    var obj = aspxMemo.GetInputElement();
    obj.value = obj.value.substr(0, selectionStart) + textoAInserir + obj.value.substr(selectionEnd, obj.value.length);
    
    aspxMemo.SetCaretPosition(selectionStart + textoAInserir.length);
}

/*-----------------------------------------------------------------------------
    Función: rdm(id,widthD,heightD)
    Parámetro: id (tag Div), string indicando Width, string indicando Height.
    retorno: void.
    Descripción: Redimenciona el área de disenho del Workflow.
------------------------------------------------------------------------------*/
function rdm(widthD, heightD) {
    if (("" == widthD) || ("" == heightD) || (null == __wf_chartObj))
        return;

    // alterando diretamente as variáveis do objeto uma vez que a recriação do objeto provocava erro sem motivo aparente
    __wf_chartObj.width = widthD;
    __wf_chartObj.height = heightD;

    //    __wf_chartObj.variables['chartWidth'] = widthD;
    //    __wf_chartObj.variables['chartHeight'] = heightD;

    __wf_chartObj.render("divFlash");
}

/*-----------------------------------------------------------------------------
    Función: aplicarConfiguracaoWf()
    retorno: void.
    Descripción: Aplica configuración selecionada por el usuario.
------------------------------------------------------------------------------*/
function aplicarConfiguracaoWf() {
    var widthWf, heightWf;
    widthWf = txtLarguraWf.GetText();
    heightWf = txtAlturaWf.GetText();

    lblDescricaoDaVersaoWf.SetText(txtDescricaoVersaoWf.GetText());
    hfValoresTela.Set("descricaoVersao", txtDescricaoVersaoWf.GetText());
    hfValoresTela.Set("observacaoVersao", memObservacaoWf.GetText());
    hfValoresTela.Set("LarguraDivFlash", widthWf);
    hfValoresTela.Set("AlturaDivFlash", heightWf);

    // redimensiona área de desenho com as novas dimensões
    rdm(widthWf, heightWf);
}

/*-----------------------------------------------------------------------------
    Función: setMaxLength(textAreaElement, length), onKeyUpOrChange(evt),
             processTextAreaText(textAreaElement), createEventHandler(funcName)
    Parámetro: Obj Memo, cadena de texto.
    retorno: void.
    Descripción: Controla la cantidad de caracteres digitados en un objeto de 
                 tipo Memo.
------------------------------------------------------------------------------*/
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
    else
        lblCantCarater.SetText(text.length);
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function SomenteNumero(e) {
    var tecla = (window.event) ? event.keyCode : e.which;
    if ((tecla > 47 && tecla < 58)) return true;
    else {
        if (tecla != 8) return false;
        else return true;
    }
}

function renderizaFlash() {
    var alturaTela = document.documentElement.offsetHeight;
    document.getElementById('divFlash').style.height = ((alturaTela - 220) + 'px');
}


/* ========================================================================
    SESION AÇÃO FLUXO (INICIO / CANCELAMENTO)

*/

/// <summary>
/// Função para criar um novo objeto que terá os dados informados na tela de edição  de notificação de início e cancelamento do fluxo. 
/// </summary>
/// <returns type="void"></returns>
function xAcaoWf() {
    this.acao = "";
    this.assuntoNotificacao1 = "";
    this.textoNotificacao1 = "";
    this.assuntoNotificacao2 = "";
    this.textoNotificacao2 = "";

    // declara matriz para guardar as informações dos grupos que serão notificados por cancelamento ou início de fluxo
    this.notificationGroups = new Array();

    // declara matriz para guardar as informações sobre as ações automáticas a serem executadas no início ou cancelamento do fluxo
    this.automaticActions = new Array();

    this.temAcoesAutomaticas = false;
}

/// <summary>
/// função para limpar os campos da divAcaWf.
/// </summary>
/// <returns type="void">void</returns>
function limpaCamposDivAcaoWf() {
    var val = "";

    lbWorkflow_acaoWf.SetValue(obtemIdentificaoWorkflow());
    document.getElementById("mmTexto1_wf").value = val;
    document.getElementById("txtAssunto1_wf").value = val;
    document.getElementById("mmTexto2_wf").value = val;
    document.getElementById("txtAssunto2_wf").value = val;
    return;
}

/// <summary>
/// faz as validações iniciais do preenchimento da divAcaoWf.
/// </summary>
/// <remarks>
/// Esta verifica se o preenchimento da divAcaoWf está ok.
/// (Os campos obrigatórios foram preenchidos, etc)
/// </remarks>
/// <returns type="boolean">Se as informações iniciais da Div estão condizentes, retorna true. Caso contrário, false.</returns>
function validacaoInicialDivAcaoWf() {
    var bRet;

    __wf_acaoWfObj.textoNotificacao1 = document.getElementById("mmTexto1_wf").value;
    __wf_acaoWfObj.assuntoNotificacao1 = document.getElementById("txtAssunto1_wf").value;
    __wf_acaoWfObj.textoNotificacao2 = document.getElementById("mmTexto2_wf").value;
    __wf_acaoWfObj.assuntoNotificacao2 = document.getElementById("txtAssunto2_wf").value;
    // valida se os campos textos e assuntos foram preenchidos corretamente
    bRet = validaTextoAssunto(__wf_acaoWfObj);

    return bRet;
}

/// <summary>
/// Trata o 'click' no botão OK da div Ação Fluxo. 
/// </summary>
/// <remarks>
///  Atualiza as informações do XML referente ás notificações e ações automáticas devido a início ou cancelamento de fluxo.
/// </remarks>
/// <returns type="void"></returns>
function onOkDivAcaoWfClick(s, e) {
    var bRet = false;
    e.processOnServer = false;  // por enquanto, processar apenas no cliente.
    // se a validação inicial estiver ok 
    if (true == validacaoInicialDivAcaoWf()) {

        // chama a função para verificar se os grupos foram informados corretamente
        // esta função chamará a função para processar o preenchimento da divAcao, caso tudo esteja OK.
        gv_GruposNotificados_wf.GetPageRowValues("CodigoPerfilWf;NomePerfilWf;Mensagem", validaGruposNotif_wf);
    }

    return;
}

/// <summary>
/// Função para validar se ao informar os grupos que receberão notificações não foi esquecido 
/// de informar os textos das mensagens correspondentes
/// </summary>
/// <remarks>
///  Assume que a variável global "__wf_acaoWfObj" já contém os valores dos campos informados na tela. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de grupos da div AcaoWf
/// </param>
/// <returns type="void">void</returns>
function validaGruposNotif_wf(valores) {
    var bRet = true, bMensagemAcao = false, bMensagemAcomp = false;
    var cTipoMsg = "";
    
    for (var i = 0; i < valores.length; i++) {
        cTipoMsg = valores[i][2];
        if (traducao.adm_edicaoWorkflows_a__o == cTipoMsg)
            bMensagemAcao = true;
        else if (traducao.adm_edicaoWorkflows_acompanhamento == cTipoMsg)
            bMensagemAcomp = true;
    }

    if (true == bRet) {
        if (("" == __wf_acaoWfObj.textoNotificacao1) && (true == bMensagemAcao)) {
            window.top.mostraMensagem(traducao.adm_edicaoWorkflows_aten__o____foram_informados_grupos_para_receberem_a__mensagem_a__o___mas_n_o_foi_informado_o_texto_da_notifica__o_, 'Atencao', true, false, null);
            bRet = false;
        }
        else if (("" !== __wf_acaoWfObj.textoNotificacao1) && (false == bMensagemAcao)) {
            window.top.mostraMensagem(traducao.adm_edicaoWorkflows_aten__o__foi_informado_o_texto_da__mensagem_a__o__mas_n_o_foi_indicado_nenhum_grupo_para_receber_esta_mensagem_, 'Atencao', true, false, null);
            bRet = false;
        }
    } // if (true == bRet)

    if (true == bRet) {
        if (("" == __wf_acaoWfObj.textoNotificacao2) && (true == bMensagemAcomp)) {
            window.top.mostraMensagem(traducao.adm_edicaoWorkflows_aten__o____foram_informados_grupos_para_receberem_a__mensagem_acompanhamento___mas_n_o_foi_informado_o_texto_da_notifica__o_, 'Atencao', true, false, null);
            bRet = false;
        }
        else if (("" !== __wf_acaoWfObj.textoNotificacao2) && (false == bMensagemAcomp)) {
            window.top.mostraMensagem(traducao.adm_edicaoWorkflows_aten__o__foi_informado_o_texto_da__mensagem_acompanhamento__mas_n_o_foi_indicado_nenhum_grupo_para_receber_esta_mensagem_, 'Atencao', true, false, null);
            bRet = false;
        }
    } // if (true == bRet)
    //--------------------------------------------------------------------------------------------------

    if (true == bRet) {
        // processa o preenchimento da div gravando os dados no XML
        processaPreenchimentoDivAcaoWf();
    }

    return;
}


/// <summary>
/// função para processar o preenchimento da DivAcao
/// </summary>
/// <remarks>
/// Esta função assume que as informações entradas na DivAcao já estão todas VALIDADAS.
/// </remarks>
/// <returns>void</returns>
function processaPreenchimentoDivAcaoWf() {
    //personalizo o tipo da ação que se esta atualizando (Início ou Cancelamento).
    //Iso recupero do label, preenchido no momento de cargar a DivAcaoWf.
    var acao = lblAcaoWf.GetText();
    if ("cancelado." == acao)
        acao = "Cancelamento";
    else if ("iniciado." == acao)
        acao = "Início";
    __wf_acaoWfObj.acao = acao

    if (gv_Acoes_wf.GetVisibleRowsOnPage() > 0)
        __wf_acaoWfObj.temAcoesAutomaticas = true;
    else
        __wf_acaoWfObj.temAcoesAutomaticas = false;

    // oculta a div e inclui, no xml, as demais informações fornecidas nas grids;
    divAcaoWf.Hide();
    processaInformacoesGridsDivAcaoWf();
}

/// <summary>
/// Solicita, ao servidor, as informações das grids, incluindo cada uma no xml.
/// </summary>
/// <returns type="void">void</returns>
function processaInformacoesGridsDivAcaoWf() {
    var campos = "CodigoPerfilWf;NomePerfilWf;Mensagem";
    // solicita inicialmente as informações sobre a grid de grupos notificados. 
    gv_GruposNotificados_wf.GetPageRowValues(campos, recebeCamposGridGruposDivAcaoWf);
}

/// <summary>
/// Função para receber, do servidor, os valores dos campos da grid de Grupos Notificados da DivAcaoWf 
/// e gravá-los no XML.
/// </summary>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de Grupos Notificados da div AcaoWf
/// </param>
/// <returns type="void">void</returns>
function recebeCamposGridGruposDivAcaoWf(valores) {
    var i, j;
    
    // remove os elementos antigos do objeto
    if (0 < __wf_acaoWfObj.notificationGroups.length)
        __wf_acaoWfObj.notificationGroups.splice(0, __wf_acaoWfObj.notificationGroups.length);

    for (i = 0; i < valores.length; i++)
        __wf_acaoWfObj.notificationGroups.push(valores[i]);

    atualizaXmlComGruposDaDivAcaoWf();

    // solicita informações sobre a grid de ações automáticas. A função recebeCamposGridAcoesDivAcaoWf 
    // irá gravar as informações no xml assim que as receber.

    var campos = "Nome;CodigoAcaoAutomaticaWf";

    gv_Acoes_wf.GetPageRowValues(campos, recebeCamposGridAcoesDivAcaoWf);
}

/// <summary>
/// Função para receber os valores dos campos da grid de ações da DivAcaoWf
/// </summary>
/// <remarks>
/// Após receber e gravar os valores da grid de ações, habilita o botão salvar da tela 
/// que foi desabilitado pela função que trata o click do botão ok da div (onOkDivAcaoWfClick(s, e)).
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de grupos de ações da div Etapa
/// <param>
/// <returns type="void">void</returns>
function recebeCamposGridAcoesDivAcaoWf(valores) {
    var i, j;

    // remove os elementos antigos do objeto
    if (0 < __wf_acaoWfObj.automaticActions.length)
        __wf_acaoWfObj.automaticActions.splice(0, __wf_acaoWfObj.automaticActions.length);

    for (i = 0; i < valores.length; i++)
        __wf_acaoWfObj.automaticActions.push(valores[i]);

    atualizaXmlComAutomaticActionsDaDivAcaoWf();
}

/// <summary>
/// Função para incluir no XML dados das ações automáticas da grid 'Ações' da divAcaoWf 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. 
/// Assume que o objeto __wf_acaoWfObj contém os dados a serem incluídos e a objeto __wf_xmlDocWorkflow
/// contém o xml com as informações. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de 'Ações' da div AcaoWf
/// </param>
/// <returns type="void">void</returns>
function atualizaXmlComAutomaticActionsDaDivAcaoWf() {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");
    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (sets != null)
        ne = getChildNodeById(sets, "0");

    //if ((null != ne) && (0 < __wf_acaoWfObj.automaticActions.length)) {
    if (null != ne) {
        var acoes = getChildNodeByName(ne.childNodes, "acoes");
        if (null == acoes)
            acoes = __wf_xmlDocWorkflow.createElement("acoes");

        var acao = obtemXmlAcaoInicioAtualizado(acoes, __wf_acaoWfObj);

        var actions = getChildNodeByName(acao.childNodes, "acoesAutomaticas");
        if (null != actions)
            acao.removeChild(actions);

        actions = __wf_xmlDocWorkflow.createElement("acoesAutomaticas");
        var action;
        for (var i = 0; i < __wf_acaoWfObj.automaticActions.length; i++) {
            action = __wf_xmlDocWorkflow.createElement("acaoAutomatica");
            action.setAttribute("id", __wf_acaoWfObj.automaticActions[i][__wf_cNumColDivCntAutoActions_id]);
            action.setAttribute("name", __wf_acaoWfObj.automaticActions[i][__wf_cNumColDivCntAutoActions_name]);
            actions.appendChild(action);
        }
        acao.appendChild(actions);
        acoes.appendChild(acao);
        ne.appendChild(acoes);
        atualizaHiddenField(__wf_xmlDocWorkflow);
    }
}


/// <summary>
/// Função para incluir no XML dados da grid 'Grupos Notificados' da divAcaoWf 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. 
/// Assume que o objeto __wf_acaoWfObj contém os dados a serem incluídos e a objeto __wf_xmlDocWorkflow
/// contém o xml com as informações. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de 'grupos notificados' da div AcaoWf
/// </param>
/// <returns type="void">void</returns>
function atualizaXmlComGruposDaDivAcaoWf() {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");

    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (null != sets)
        ne = getChildNodeById(sets, "0"); //Etapa Inicial

    if (null != ne) {
        var acoes = getChildNodeByName(ne.childNodes, "acoes");
        if (null == acoes)
            acoes = __wf_xmlDocWorkflow.createElement("acoes");

        var acao = obtemXmlAcaoInicioAtualizado(acoes, __wf_acaoWfObj);

        var groups = getChildNodeByName(acao.childNodes, "gruposNotificados");
        if (null != groups)
            acao.removeChild(groups);

        groups = __wf_xmlDocWorkflow.createElement("gruposNotificados");
        var group;
        for (var i = 0; i < __wf_acaoWfObj.notificationGroups.length; i++) {
            group = __wf_xmlDocWorkflow.createElement("grupo");
            group.setAttribute("id", __wf_acaoWfObj.notificationGroups[i][__wf_cNumColDivCntGroups_id]);
            group.setAttribute("name", __wf_acaoWfObj.notificationGroups[i][__wf_cNumColDivCntGroups_name]);
            group.setAttribute("msgBox", __wf_acaoWfObj.notificationGroups[i][__wf_cNumColDivCntGroups_msgBox]);
            groups.appendChild(group);
        }
        acao.appendChild(groups);
        acoes.appendChild(acao);
        ne.appendChild(acoes);
        atualizaHiddenField(__wf_xmlDocWorkflow);
    }
}

/// <summary>
/// Função para devolver um "nó" XML contendo as informações sobre a ação especificada pelo objeto xAcaoWf
/// passado como parâmetro.
/// </summary>
/// <remarks>
/// Esta função devolve um "nó" XML contendo as informações sobre a ação. Atenção!!! Esta função não atualiza o XML, 
/// apenas devolve o nó. 
/// </remarks>
/// <param name="acoes" type="DOM XML">Objeto DOM no qual serão incluídas as informações.</param>
/// <param name="connector" type="objeto">Objeto do tipo xConnector contendo as informações</param>
/// <returns type="DOM XML">Retorna a ação void</returns>
function obtemXmlAcaoInicioAtualizado(acoes, objAcao) {
    var actionType;

    if (__wf_acaoWfObj.acao == "Cancelamento")
        actionType = "C";
    else
        actionType = "I";

    var acao = getChildNodeById(acoes.childNodes, objAcao.acao);
    if (null == acao)
        acao = __wf_xmlDocWorkflow.createElement("acao");

    acao.setAttribute("id", objAcao.acao);
    acao.setAttribute("nextStageId", "");
    acao.setAttribute("to", "");
    acao.setAttribute("actionType", actionType);

    //Assunto da notificação1
    var assunto = getChildNodeByName(acao.childNodes, "assuntoNotificacao");
    var conteudo;

    if (null != assunto)
        acao.removeChild(assunto);
    assunto = __wf_xmlDocWorkflow.createElement("assuntoNotificacao");

    if ((null != objAcao.assuntoNotificacao1) && ("" != objAcao.assuntoNotificacao1))
        conteudo = __wf_xmlDocWorkflow.createTextNode(objAcao.assuntoNotificacao1);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    assunto.appendChild(conteudo);
    acao.appendChild(assunto);

    //Texto da notificação1
    var texto = getChildNodeByName(acao.childNodes, "textoNotificacao");
    if (null != texto)
        acao.removeChild(texto);
    texto = __wf_xmlDocWorkflow.createElement("textoNotificacao");

    if ((null != objAcao.textoNotificacao1) && ("" !== objAcao.textoNotificacao1))
        conteudo = __wf_xmlDocWorkflow.createTextNode(objAcao.textoNotificacao1);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    texto.appendChild(conteudo);
    acao.appendChild(texto);

    //Assunto da notificação2
    assunto = getChildNodeByName(acao.childNodes, "assuntoNotificacao2");

    if (null != assunto)
        acao.removeChild(assunto);
    assunto = __wf_xmlDocWorkflow.createElement("assuntoNotificacao2");

    if ((null != objAcao.assuntoNotificacao2) && ("" != objAcao.assuntoNotificacao2))
        conteudo = __wf_xmlDocWorkflow.createTextNode(objAcao.assuntoNotificacao2);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    assunto.appendChild(conteudo);
    acao.appendChild(assunto);

    //Texto da notificação2
    texto = getChildNodeByName(acao.childNodes, "textoNotificacao2");
    if (null != texto)
        acao.removeChild(texto);
    texto = __wf_xmlDocWorkflow.createElement("textoNotificacao2");

    if ((null != objAcao.textoNotificacao2) && ("" !== objAcao.textoNotificacao2))
        conteudo = __wf_xmlDocWorkflow.createTextNode(objAcao.textoNotificacao2);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    texto.appendChild(conteudo);
    acao.appendChild(texto);

    return acao;
}

function tratarSimbolos(s, e) {
    if (e.htmlEvent.keyCode == 39 || e.htmlEvent.keyCode == 192) {
        e.htmlEvent.keyCode = 180;
        return e.htmlEvent.keyCode;
    }
}

function processaClickIncluirGridParametros() {
    var numLinhas = gv_Acionamentos.GetVisibleRowsOnPage();
    if (numLinhas == 0 || gv_Acionamentos.GetFocusedRowIndex() < 0) {
        window.top.mostraMensagem('Primeiro selecione um item na grid de Acionamentos!', 'atencao', true, false, null);
    }
    else {
        gv_ParametrosAcionamentos.AddNewRow();
    }
}