/*
NOTAS:cbSolicitarAssinaturaDigital

12/10/2010: Mudança by Alejandro
Implementação da função 'frv_formulariosEtapasOk()'.
Função que realiza uma revisão da relação entre Formulários e Etapas às quais está relacionada.
*/
var comandoGridAcionamentos;
var isIE = (window.navigator.userAgent.indexOf("MSIE") > 0);

// número da versão de workflow atual do sistema
var __wf_versaoFormatoXmlAtual = "001.1.029";

// variáveis globais para guardar as informações sobres os elementos sendo incluídos/editados
var __wf_activObj = new xActivity('0');
var __wf_subprcObj = new xSubprocess('0');
var __wf_timerObj = new xTimer();
var __wf_connectorObj = new xConnector();

var __wf_chartObj; // variável global do tipo FusionChart para conter uma referência ao chart sendo apresentado na tela
var __wf_xmlDocWorkflow; // variável global do tipo DOM XML para conter o XML de trabalho do gráfico do workflow
var __wf_nlElementos; // variável global do tipo DOM XML para conter a lista de nós referentes aos elementos no gráfico
var __wf_nlConectores; // variável global do tipo DOM XML para conter a lista de nós referentes aos elementos no gráfico
//var __wf_nlAcaoWf; // variável global do tipo DOM XML para conter a lista do nós referentes aos elementos no gráfico.

// constantes usadas no script
var __wf_cIdElementoInicio = "0";   // identificador do elemento gráfico 'início' do workflow

var __wf_cTipoElementoInicio = "0";
var __wf_cTipoElementoEtapa = "1";
var __wf_cTipoElementoConector = "2";
var __wf_cTipoElementoTimer = "3";
var __wf_cTipoElementoDisjuncao = "4";
var __wf_cTipoElementoJuncao = "5";
var __wf_cTipoElementoFim = "6";
var __wf_cTipoElementoCondicao = "7";
var __wf_cTipoElementoSubprocesso = "8";

var __wf_cNumColDivEtpForms_id = 0;
var __wf_cNumColDivEtpForms_name = 1;
var __wf_cNumColDivEtpForms_title = 2;
var __wf_cNumColDivEtpForms_readOnly = 3;
var __wf_cNumColDivEtpForms_required = 4;
var __wf_cNumColDivEtpForms_newOnEachOcurrence = 5;
var __wf_cNumColDivEtpForms_requerAssinaturaDigital = 6;        // CERTDIG
var __wf_cNumColDivEtpForms_ordemFormularioEtapa = 7;        // OrdemForm
var __wf_cNumColDivEtpForms_originalStageId = 8;

var __wf_cNumColDivEtpGroups_id = 0;
var __wf_cNumColDivEtpGroups_name = 1;
var __wf_cNumColDivEtpGroups_accessType = 2;

var __wf_cNumColDivCntGroups_id = 0;
var __wf_cNumColDivCntGroups_name = 1;
var __wf_cNumColDivCntGroups_msgBox = 2;

var __wf_cNumColDivCntAutoActions_name = 0;
var __wf_cNumColDivCntAutoActions_id = 1;

// outras variáveis globais auxiliares
var __wf_bFormTitleChanged = false; // controlar se o título de um formulário sendo incluído na divEtapa foi alterado pelo usuário

/// <summary>
/// Variável para conter o texto que se deve adicionar ao início dos nomes dos elementos na tela. 
/// Técnica passada pelo Antônio para uso nas funções getElementById() em telas que ora estarão dentro de 
/// uma master page, ora estarão fora.
/// Estar variável deve ter seu valor atribuído num script na tela que irá incluir este arquivo de script
/// </summary>
var __frameName = ""; // controlar se o título de um formulário sendo incluído na divEtapa foi alterado pelo usuário

/// <summary>
/// Função que o FusionCharts chama após o gráfico ter sido renderizado
/// </summary>
/// <param name="DOMId" type="object">Id do gráfico que foi renderizado</param>
/// <returns type="void"></returns>
function FC_Rendered(DOMId) {
    if (DOMId == "nodeChart") {
        //setXmlComponente();
        ocultaDivEdicaoWorkflow("DivDisenhando"); // by alejandro
        return;
    }
}


/// <summary>
/// função para tratar o click em um dos botões para adicionar elementos no XML
/// </summary>
/// <param name="tipoElemento" type=string>Código do tipo de elemento para o qual foi feito o click</param>
/// <returns type="boolean">Retorna falso, sempre -> para que o processamento não produza o reload na tela</returns>
function trataClickNewElementButton(tipoElemento) {
    // se não for possível manipular o XML, já retorna sem fazer nada;
    if (false == xmlWorkflowOk())
        return false;

    // se clicou para incluir uma etapa
    if (__wf_cTipoElementoEtapa == tipoElemento) {
        preparaExibicaoDivEtapa("I");
        divEtapa.Show();
    }
    else if (__wf_cTipoElementoConector == tipoElemento) {
        limpaCamposDivConector('I');
        preparaExibicaoDivConector();
        divConector.Show();
    }
    else if (__wf_cTipoElementoTimer == tipoElemento) {
        limpaCamposDivTimer();
        cmbEtapaOrigem_tmr.cpTimerID = getNextElementId(__wf_nlElementos);
        divTimer.Show();
    }
    else if ((__wf_cTipoElementoDisjuncao == tipoElemento)
        || (__wf_cTipoElementoJuncao == tipoElemento)
        || (__wf_cTipoElementoFim == tipoElemento)) {
        zeraJSProperties_pnlCbkDisJunFim(tipoElemento);
        preparaExibicaoDivDisJunEnd(tipoElemento);
        mostraDivEdicaoWorkflow("divDescricao");
    }
    if (__wf_cTipoElementoSubprocesso == tipoElemento) {
        preparaExibicaoDivSubprocesso("I");
        divSubprocesso.Show();
    }
    else if (__wf_cTipoElementoCondicao == tipoElemento) {
        // função para incluir um novo objeto "CaminhoCondicional". Na inclusão, o código sempre será "-1"
        pcDvCaminhoCondicionalShow("Inc", -1); // ACG: 20/11/2015 - A função está no arquivo "uc_crud_caminhoCondicional.js"
    }

    return false;
}

// ------------------------------------------------------------------------------------------------- //
// ------                                                                                     ------ //
// ------ Funções para inclusão de nova atividade                                             ------ //
// ------                                                                                     ------ //
// ------------------------------------------------------------------------------------------------- //

/// <summary>
/// Função para criar um novo objeto do tipo etapa. 
/// </summary>
/// <param name="id" type="integer">Id no gráfico para o objeto em questão.</param>
/// <returns type="void"></returns>
function xActivity(id) {
    this.elementId = id;
    this.activityId = 0;
    this.name = "";
    this.shortDescription = "";
    this.description = "";
    this.inicial = false;
    this.ocultaBotoes = false;
    this.valorPrazoPrevisto = "";
    this.unidadePrazoPrevisto = "";
    this.referenciaPrazoPrevisto = "";
    this.codigoSubfluxo = 0;

    // declara matriz para guardar as informações dos grupos que serão usados na etapa
    this.forms = new Array();

    // declara matriz para guardar as informações dos grupos que terão acessos à etapa
    this.accessGroups = new Array();
}

function limpaCamposDivEtapa() {
    /// <summary>
    /// função para limpar os campos da div etapa. usada ao mostrar a div para o usuário.
    /// </summary>
    /// <returns type="void">void</returns>

    var val = null;

    edtNomeAbreviado_etp.SetValue(val);
    edtDescricaoResumida_etp.SetValue(val);
    mmDescricao_etp.SetValue(val);
    txtQtdTempo.SetValue(val);
    ddlUnidadeTempo.SetSelectedIndex(-1);
    ddlReferenciaTempo.SetSelectedIndex(-1);
    cbOcultaBotoes.SetChecked(false);
    return;
}

function obtemIdentificaoWorkflow() {
    var nomeFluxo = hfValoresTela.Get("nomeFluxo");
    var versao = hfValoresTela.Get("versaoWorkflow");
    if (null == nomeFluxo)
        nomeFluxo = "";
    if (null == versao)
        versao = "";

    return nomeFluxo + " - " + traducao.WorkflowCharts_vers__o + ": " + versao;
}


/// <summary>
/// função para tratar o evento de inicialização do dropDownList de etapas de origens na divConector
/// </summary>
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function ddlEtapaOrigem_etpInit(s, e) {
    __wf_bFormTitleChanged = false;
    preencheComboEtapaOrigem_etp(s, e);
}

/// <summary>
/// função para preencher o combobox de etapa de origem ao incluir um novo formulário na div Etapa.
/// </summary>
/// <remarks>
/// no momento do preenchimento, se o nome da etapa corresponder ao nome no edtNomeAbreviado_etp,
/// não inclui esta etapa no combo por se tratar da etapa em edição no momento.
/// </remarks>
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function preencheComboEtapaOrigem_etp(s, e) {
    var j = 0;
    var linhas = new Array();

    // variáveis para atualizar o texto mostrado no combobox;
    var value, text = "";

    var elName;
    var elId = new Object();
    var tipo;

    var col = s.listBox;
    col.ClearItems(); // limpa os itens do combo box

    value = s.GetValue();


    // adiciona a linha em branco
    elName = "";
    elId = "0";
    var selectedIndex = col.AddItem(elName, elId); // inicialmente, o índice selecionado será o da etapa 'em branco'

    for (var i = 0; i < __wf_nlElementos.length; i++) {
        if (__wf_nlElementos[i].nodeType != 1)
            continue;

        tipo = obtemTipoElemento(__wf_nlElementos[i]);
        if ((__wf_cTipoElementoEtapa == tipo)) {
            elName = getElementIdName(__wf_nlElementos[i]);
            elId = __wf_nlElementos[i].getAttribute("id");

            if ((null != elName) && (null != elId)) {
                if (edtNomeAbreviado_etp.GetValue() != getNomeEtapaSemId(elName)) {
                    linhas[j] = new Array();
                    linhas[j][0] = elId;
                    linhas[j][1] = elName;
                    j++;
                }
            } // if (null != ...
        } // if (true == ...
    } // for i

    // ordena a matriz antes de adicionar ao drop down list;
    if (0 < linhas.length)
        linhas.sort(sortArrayEtapas);

    // adiciona os demais itens
    for (var i = 0; i < linhas.length; i++) {
        // se estiver adicionando o item que está registrado atualmente no combobox
        // atualiza a variável selectedIndex para posicionar o combo na linha correta
        if (value == linhas[i][0]) {
            selectedIndex = col.AddItem(linhas[i][1], linhas[i][0]);
            text = linhas[i][1];
        }
        else
            col.AddItem(linhas[i][1], linhas[i][0]);
    }

    s.SetText(text);

    //    col.SelectIndex(selectedIndex);
    col.SetSelectedIndex(selectedIndex);
}


/// <summary>
/// função usada no processamento de sortear uma matriz. 
/// </summary>
/// <remarks>
/// Esta função deve ser passada como parâmetro para o método 'sort' de uma matriz quando se deseja 
/// ordernar uma matriz multidimensional pela segunda coluna
/// </remarks>
/// <param name="item1" type=Object>uma das linhas da matriz para se comparar</param>
/// <param name="item2" type=Object>outra linha da matriz a se comparar</param>
/// <returns>Caso as duas linhas sejam iguais, retorna 0, se a primeira linha passada 
/// é para ser ordenada após a segunda, retorna 1, e na situação inversa, retorna -1</returns>
function sortArrayBySecondColumn(item1, item2) {
    if (item1[1] == item2[1])
        return 0;
    else if (item1[1] > item2[1])
        return 1;
    else
        return -1;
}


/// <summary>
/// função usada no processamento de sortear uma matriz de ETAPAS. 
/// </summary>
/// <remarks>
/// Esta função deve ser passada como parâmetro para o método 'sort' para se ordenar uma matriz de etapas
/// Por matriz de etapa entende-se uma matriz com no mínimo duas dimensões e que com a 2ª dimensão 
/// contendo a identificação da etapa ("XXX. ABCDEFG...", onde XXX é o número da etapa)
/// </remarks>
/// <param name="item1" type=Object>uma das linhas da matriz de ETAPAS para se comparar</param>
/// <param name="item2" type=Object>outra linha da matriz de ETAPAS a se comparar</param>
/// <returns>Caso as duas linhas sejam iguais, retorna 0, se a primeira linha passada 
/// é para ser ordenada após a segunda, retorna 1, e na situação inversa, retorna -1</returns>
function sortArrayEtapas(item1, item2) {
    var id1, id2;
    id1 = getEtapaIdFromEtapaName(item1[1]);
    id2 = getEtapaIdFromEtapaName(item2[1]);

    // se alguns dos elementos não tiver ID (não for uma etapa), faz a comparação textual
    if ((null == id1) || (null == id2)) {
        if (item1[1] == item2[1])
            return 0;
        else if (item1[1] > item2[1])
            return 1;
        else
            return -1;
    }  // if ( (null == id1) || (null == id2) )
    else { // se os dois elementos são etapas, ordena pelos IDs
        id1 = parseInt(id1);
        id2 = parseInt(id2);

        if (id1 == id2)
            return 0;
        else if (id1 > id2)
            return 1;
        else
            return -1;

    }
}

/// <summary>
/// função para prepara a div etapa para ser exibida.
/// </summary>
/// <remarks>
/// No momento da exibição da div etapa é necessário fazer alguns ajustes na div que depende 
/// se a div está sendo mostrada para a inclusão de uma nova etapa ou para a edição de uma
/// etapa já existente
/// Assume que os dados a serem mostrados já estão nos controles dentro da div.
/// </remarks>
/// <param name="acao" type=string>Letra indicando se a exibição é para uma inclusão ou para uma 
/// edição de uma etapa. Letas I ou E</param>
/// <returns type="void">void</returns>
function preparaExibicaoDivEtapa(acao) {
    lbWorkflow_etp.SetValue(obtemIdentificaoWorkflow());
    //lbWorkflow_etp.HeaderText.SetValue("Informações da Etapa - Modelo de Fluxo: " + obtemIdentificaoWorkflow());
    // se for uma inclusão 
    if ("I" == acao) {
        limpaCamposDivEtapa();

        __wf_activObj.elementId = getNextElementId(__wf_nlElementos);
        __wf_activObj.activityId = getNextActivityId(__wf_nlElementos);

        edtIdEtapa_etp.SetValue(__wf_activObj.activityId.toString());
    }

    // define o estado do checkBox Etapa Inicial de acordo com a ação (inclusão ou edição)
    defineEstadoCbEtapaInicial(acao);

}

/// <summary>
/// função para definir qual será o estado do checkBox 'EtapaInicial' ao mostrar a divEtapa.
/// </summary>
/// <remarks>
/// A definição do estado do checkbox segue a seguinte regra:
/// Propriedade Enabled: 
///    Estará habilitada caso a etapa sendo editada seja a etapa inicial ou ainda não exista nenhuma etapa
///    definida como inicial
/// Propriedade Checked: 
///    O campo estará marcado se esta for a etapa inicial, e desmarcado caso contrário;
///
///  Assume que o ID da etapa sendo editada está gravado no controle edtIdEtapa_etp
/// </remarks>
/// <returns type="void">void</returns>
function defineEstadoCbEtapaInicial(acao) {
    var idEtapa = edtIdEtapa_etp.GetValue();

    // se for a etapa inicial, marca e habilita o checkbox
    if (true == ehEtapaInicial(idEtapa)) {
        cbEtapaInicial_etp.SetEnabled(true);
        cbEtapaInicial_etp.SetChecked(true);
    }
    // se não for a etapa inicial e já existir a etapa inicial no fluxo
    /// desmarca e desabilita o checkbox
    else if (true == existeEtapaInicial()) {
        cbEtapaInicial_etp.SetChecked(false);
        cbEtapaInicial_etp.SetEnabled(false);
    }
    // se NÃO for a etapa inicial, mas ainda NÃO houver uma etapa inicial
    else {
        // habilita o check box e, se for uma inclusão de etapa, marca-o sugerindo que 
        // a próxima etapa sendo incluída seja a etapa inicial. Caso seja uma edição, 
        // deixa o checkbox sem marcar.
        cbEtapaInicial_etp.SetEnabled(true);

        if ("I" == acao)
            cbEtapaInicial_etp.SetChecked(true);
        else
            cbEtapaInicial_etp.SetChecked(false);
    }

    return;
}

/// <summary>
/// função para verificar no XML se a etapa com o id passado no parâmetro <paramref name="idEtapa"> é a etapa inicial do workflow.
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para oo gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="idEtapa" type="string">ID da etapa que se deseja verificar se é a etapa inicial.</param>
/// <returns type="boolean">Se a etapa em questão for a etapa inicial, retorna true. 
///  Caso a etapa não conste no XML ou não tenha sido possível verificar a existência, retorna false.</returns>
function ehEtapaInicial(idEtapa) {
    if (null == idEtapa)
        return false;

    var bRet = false;
    var node = obtemXmlNodeEtapaInicial();
    var id;

    if (null != node) {
        id = getEtapaIdFromEtapaName(node.getAttribute("name"));
        if ((null != id) && (id == idEtapa))
            bRet = true;
    }
    return bRet;
}

/// <summary>
/// função para verificar se já existe no XML uma etapa definida como etapa inicial do fluxo.
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para oo gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <returns type="boolean">Se foi verificada a existência de uma etapa inicial no XML, retorna true. 
///  Caso não conste uma etapa inicial no XML ou não tenha sido possível verificar, retorna false.</returns>
function existeEtapaInicial() {
    var bRet = false;
    var node = obtemXmlNodeEtapaInicial();

    if (null != node)
        bRet = true;

    return bRet;
}


/// <summary>
/// função para devolver o XmlNode referente à etapa inicial do XML.
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// <remarks>
/// <returns type="XML Node">O nó referente à etapa inicial caso tenha sido possível localizá-lo no XML. (Objeto DOM).</returns>
function obtemXmlNodeEtapaInicial() {
    var node = null;

    if (null != __wf_nlElementos) {
        var inicial;
        var i;

        for (i = 0; i < __wf_nlElementos.length; i++) {
            if (__wf_nlElementos[i].nodeType != 1)
                continue;

            inicial = __wf_nlElementos[i].getAttribute("etapaInicial");
            if ((null != inicial) && ("1" == inicial))
                break;
        }

        // se o loop foi interrompido antes de varrer todos os nós, é sinal que localizou o nó procurado
        if (i < __wf_nlElementos.length)
            node = __wf_nlElementos[i];
    }

    return node;
}


/// <summary>
/// função para tratar a marcação e desmarcação de 'read only' na inclusão de formulários 
/// na etapa
/// </summary>
/// <remarks>
/// Ao marcar readOnly num formulário, deve-se desativar as opções 'Obrigatório' e 'Novo em cada Ocorrência', 
/// que só voltam a ficar disponíveis ao desmarcar readOnly
/// </remarks
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function onCbReadOnly_frmEtp__Checked(s, e) {
    if (true == s.GetChecked()) {
        cbRequired_frmEtp.SetChecked(false);
        cbRequired_frmEtp.SetEnabled(false);
        cbNovoCadaOcorrencia_frmEtp.SetChecked(false);
        cbNovoCadaOcorrencia_frmEtp.SetEnabled(false);
    }
    else {
        cbRequired_frmEtp.SetEnabled(true);
        cbNovoCadaOcorrencia_frmEtp.SetEnabled(true);
    }
    e.processOnServer = false; // registro no parâmetro que não é para processar nada no server
}

/// <summary>
/// função para tratar a marcação e desmarcação da opção 'novo em cada ocorrência' na inclusão de formulários 
/// na etapa
/// </summary>
/// <remarks>
/// Ao marcar 'Novo em Cada Ocorrência' num formulário, deve-se desativar a opção para a escolha 
/// da etapa de origem do formulário que só volta a ficar disponível ao desmarcar 'Novo em Cada Ocorrência' 
/// </remarks
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function onCbNovoCadaOcorrencia_frmEtp__Checked(s, e) {
    if (true == s.GetChecked()) {
        ddlEtapaOrigem_etp.SetValue("0"); // coloca a seleção na linha com espaço em branco;
        ddlEtapaOrigem_etp.SetEnabled(false);
    }
    else {
        ddlEtapaOrigem_etp.SetEnabled(true);
    }
    e.processOnServer = false; // registro no parâmetro que não é para processar nada no server
}

/// <summary>
/// Trata o 'click' no botão OK da div Etapa. 
/// </summary>
/// <remarks>
/// Realiza as consistências necessárias e, se tudo certo, inclui no gráfico a etapa cadastrada.
/// Após incluir no gráfico, solicita ao servidor as informações adicionais preenchidas nas grids da div.
/// A função que recebe as informações das grids as inclui no xml assim que as recebe.
/// </remarks>
/// <param name="s" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
/// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
function onOkDivEtapaClick(s, e) {
    var bRet = false;
    e.processOnServer = false;  // por enquanto, não processar no server

    if (preenchimentoDivEtapaOK()) {
        __wf_activObj.activityId = edtIdEtapa_etp.GetValue();
        __wf_activObj.name = edtNomeAbreviado_etp.GetValue();
        __wf_activObj.shortDescription = edtDescricaoResumida_etp.GetValue();
        __wf_activObj.inicial = cbEtapaInicial_etp.GetChecked();
        __wf_activObj.ocultaBotoes = cbOcultaBotoes.GetChecked();
        __wf_activObj.valorPrazoPrevisto = txtQtdTempo.GetValue();
        __wf_activObj.unidadePrazoPrevisto = ddlUnidadeTempo.GetValue();
        __wf_activObj.referenciaPrazoPrevisto = ddlReferenciaTempo.GetValue();
        __wf_activObj.description = mmDescricao_etp.GetValue();

        // tratamento para o caso de não ter informado
        if (null == __wf_activObj.description)
            __wf_activObj.description = "";

        if (true == addActivityNodeInXml(__wf_xmlDocWorkflow, __wf_activObj, __wf_cTipoElementoEtapa)) {
            var wCnn = new xConnector();
            wCnn.from = '0';
            wCnn.to = __wf_activObj.elementId;
            wCnn.acao = "";
            if (true == __wf_activObj.inicial)
                addConnectorInXml(wCnn, true);
            else
                removeConnectorFromXml(wCnn);

            // desabilita o botão 'Salvar' e oculta a div. o botão salvar será reabilitado após 
            // o cliente ter recebido os dados das grids e os gravado no xml.
            //            btnSalvar.SetEnabled(false);

            divEtapa.Hide();

            // inclui as demais informações fornecidas nas grids no xml;
            processaInformacoesGridsDivEtapa();
            bRet = true;

        }
        else  // se não deu certa a inclusão da etapa
        {
            bRet = false;
            window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_da_nova_atividade_ + " (0004)", 'erro', true, false, null);
        }
    }

    return bRet;
}

function preenchimentoDivEtapaOK() {
    /// <summary>
    /// função para validar se o preenchimento da divEtapa está ok.
    /// (Todos os campos obrigatórios foram preenchidos? Não está incluindo uma etapa em duplicidade?
    /// </summary>
    /// <returns type="boolean">Se tudo deu certo no preenchimento da Div, retorna true. Caso contrário, false.</returns>
    var bRet = true;
    var idEtapa = edtIdEtapa_etp.GetValue()
    var NomeEtapa = edtNomeAbreviado_etp.GetValue();
    var DescricaoEtapa = edtDescricaoResumida_etp.GetValue();
    var quantidadeTempo = txtQtdTempo.GetValue();
    var unidadeTempo = ddlUnidadeTempo.GetValue();
    var referenciaTempo = ddlReferenciaTempo.GetValue();
    var chkEtapaInicial = cbEtapaInicial_etp.GetChecked();
    
    if (true == bRet) {
        if ((null == NomeEtapa) || (0 == NomeEtapa.length)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_o_campo__nome_abreviado____obrigat_rio_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        if ((null == DescricaoEtapa) || (0 == DescricaoEtapa.length)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_o_campo__descri__o_resumida____obrigat_rio_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        if (("" == quantidadeTempo) || ("" == unidadeTempo) || ("" == referenciaTempo) ||
            (null == quantidadeTempo) || (null == unidadeTempo) || (null == referenciaTempo)) {
            window.top.mostraMensagem(traducao.WorkflowCharts___preciso_informar_o_prazo_previsto_para_a_conclus_o_da_etapa_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        // se a etapa já existe e tiver ID diferente da atual (retorno 1), não deixa continuar
        if (1 == etapaJaCadastradaNoXml(idEtapa, NomeEtapa)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_j__existe_uma_etapa_com_o_nome_informado_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        var count = gvFormularios_etp.GetVisibleRowsOnPage();
        if (0 == count) {
            window.top.mostraMensagem(traducao.WorkflowCharts___preciso_informar_pelo_menos_um_formul_rio_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        var count = gv_PessoasAcessos_etp.GetVisibleRowsOnPage();
        if (0 == count) {
            window.top.mostraMensagem(traducao.WorkflowCharts___preciso_informar_pelo_menos_um_grupo_de_pessoas_que_acessar__esta_etapa_, 'atencao', true, false, null);
            bRet = false;
        }
    }
    if (true == bRet) {
        if (chkEtapaInicial == true && frv_existeTimerLigadoAEtapa (idEtapa, NomeEtapa) == true) {
            window.top.mostraMensagem('Uma etapa que tenha um timer associado, não pode ser configurada como a etapa inicial do fluxo', 'atencao', true, false, null);
            bRet = false;
        }
    }

    return bRet;
}

/// <summary>
/// Verifica se já existe no XML uma a etapa com o mesmo nome da que está sendo informada na divEtapa.
/// </summary>
/// <remarks>
/// Função que verifica se já existe no XML uma etapa com nome e id passados como parâmetros. 
/// Retorna 0 caso não exista nenhuma etapa com o nome passado como parâmetro.
/// Retorna 1 caso exista a etapa passado como parâmetro mas com ID diferente do ID passado como parâmetro
/// Retorna 2 caso exista a etapa e com o mesmo ID.
/// 
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="idEtapa" type="string">ID da etapa que se deseja verificar a existência no XML.</param>
/// <param name="NomeEtapa" type="string">Nome da etapa que se deseja verificar a existência no XML.</param>
/// <returns type="int">
/// Retorna 0 caso não exista nenhuma etapa com o nome passado como parâmetro.
/// Retorna 1 caso exista a etapa passado como parâmetro mas com ID diferente do ID passado como parâmetro
/// Retorna 2 caso exista a etapa e com o mesmo ID.
/// </returns>
function etapaJaCadastradaNoXml(idEtapa, NomeEtapa) {

    var nRet = 0;
    var auxName, wName, wId;

    // retorna que não existe caso não tenha pasado nomeEtapa
    if (null == NomeEtapa)
        return bRet;

    for (var i = 0; i < __wf_nlElementos.length; i++) {
        if (__wf_nlElementos[i].nodeType != 1)
            continue;

        tipo = obtemTipoElemento(__wf_nlElementos[i]);
        if ((__wf_cTipoElementoEtapa == tipo)) {
            auxName = __wf_nlElementos[i].getAttribute("name");
            wName = getNomeEtapaSemId(auxName);

            if ((null != wName) && (wName == NomeEtapa)) {
                wId = getEtapaIdFromEtapaName(auxName);
                if (wId == idEtapa)
                    nRet = 2;
                else
                    nRet = 1;
                break;
            }
        }
    }
    return nRet;
}

/// <summary>
/// Inclui uma nova atividade no XML que será enviado para o gráfico.
/// </summary>
/// <param name="xmlDoc" type="DOM XML">Objeto DOM no qual serão incluídas as informações da nova atividade.</param>
/// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
function addActivityNodeInXml(xmlDoc, activ, tipoElemento) {

    insertActivityNode(xmlDoc, activ, tipoElemento);
    atualizaXmlDoGrafico(xmlDoc);
    return true;
}

function getNextActivityId(nodes) {
    /// <summary>
    /// Devolve o próximo ID de uma atividade. O ID devolvido por esta função não pode ser usadao como ID do 
    /// elemento gráfico, apenas como índice para nomear a nova atividade sendo inserida.
    /// .</summary>
    /// <param name="nodes" type="XML Nodes">Coleção, no formato XML, que contém os nós a serem varridos para 
    /// identificação do próximo ID da atividade.</param>
    /// <returns type="integer">ID da próxima atividade.</returns>

    var lastId = 0;
    var id;
    var name;

    for (i = 0; i < nodes.length; i++) {
        if (true == ehElementoDoTipo(nodes[i], __wf_cTipoElementoEtapa)) {
            name = nodes[i].getAttribute("name");
            if ((null != name) && (name.length > 0)) {
                var pos = name.indexOf(".");

                if (pos > 0) {
                    id = parseInt(name.substring(0, pos));
                    if (id > lastId)
                        lastId = id;
                }
            }
        }
    }

    lastId++;
    return lastId;
}

/// <summary>
/// Solicita, ao servidor, as informações das grids, incluindo cada uma no xml.
/// </summary>
/// <returns type="void">void</returns>
function processaInformacoesGridsDivEtapa() {
    var campos = "CodigoModeloFormulario;NomeFormulario;TituloFormulario;TipoAcessoFormulario;PreenchimentoObrigatorio;NovoCadaOcorrencia;RequerAssinaturaDigital;OrdemFormularioEtapa;CodigoEtapaWfOrigem"; // CERTDIG      // OrdemForm

    // solicita inicialmente as informações sobre a grid de formulários. A função recebeCamposGridFormsDivEtapa
    /// irá solicitar as outras informações assim que receber estas primeiras.
    gvFormularios_etp.GetPageRowValues(campos, recebeCamposGridFormsDivEtapa);
}

/// <summary>
/// Função para receber, do servidor, os valores dos campos da grid de formulários da DivEtapa
/// e gravá-los no XML. 
/// </summary>
/// <remarks>
/// Após receber e gravar os valores da grid de formulários, solicita os valores da grid de grupos.
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de formuários da div Etapa
/// </param>
/// <returns type="void">void</returns>
function recebeCamposGridFormsDivEtapa(valores) {
    var i, j;

    // remove os elementos antigos do objeto
    if (0 < __wf_activObj.forms.length)
        __wf_activObj.forms.splice(0, __wf_activObj.forms.length);

    for (i = 0; i < valores.length; i++)
        __wf_activObj.forms.push(valores[i]);

    // solicita informações sobre a grid de acessos. A função recebeCamposGridAcessosDivEtapa 
    // irá gravar as informações no xml assim que as receber.

    var campos = "CodigoPerfilWf;NomePerfilWf;TipoAcesso";
    gv_PessoasAcessos_etp.GetPageRowValues(campos, recebeCamposGridAcessosDivEtapa);
    return;
}

/// <summary>
/// Função para receber os valores dos campos da grid de grupos de acessos da DivEtapa. 
/// </summary>
/// <remarks>
/// Após receber e gravar os valores da grid de grupos de acessos, habilita o botão salvar da tela 
/// que foi desabilitado pela função que trata o click do botão ok da div (onOkDivEtapaClick(s, e)).
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de grupos de acessos da div Etapa
/// <param>
/// <returns type="void">void</returns>
function recebeCamposGridAcessosDivEtapa(valores) {
    var i, j;

    // remove os elementos antigos do objeto
    if (0 < __wf_activObj.accessGroups.length)
        __wf_activObj.accessGroups.splice(0, __wf_activObj.accessGroups.length);

    for (i = 0; i < valores.length; i++)
        __wf_activObj.accessGroups.push(valores[i]);

    remontaXmlEtapa(__wf_activObj);
}

/// <summary>
/// Função para incluir no XML dados dos formulários informados na  grid 'Formulários' da divEtapa 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. Além das informações da grid, a função 
/// inclui também  a descrição da etapa.
/// Assume que o objeto __wf_activObj contém os dados a serem incluídos e a objeto __wf_xmlDocWorkflow
/// contém o xml com as informações. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de formuários da div Etapa
/// </param>
/// <returns type="void">void</returns>
function remontaXmlEtapa(activ) {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");
    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (sets != null)
        ne = getEtapaChildNodeByActivityId(sets, activ.activityId);

    if (null != ne) {
        var descr = getChildNodeByName(ne.childNodes, "descricao");
        if (null != descr)
            ne.removeChild(descr);

        descr = __wf_xmlDocWorkflow.createElement("descricao");

        var conteudo;

        if (null != descr) {
            conteudo = __wf_xmlDocWorkflow.createTextNode(activ.description);

            descr.appendChild(conteudo);
            ne.appendChild(descr);
        }

        var prazo = getChildNodeByName(ne.childNodes, "prazoPrevisto");
        if (null != prazo)
            ne.removeChild(prazo);

        prazo = __wf_xmlDocWorkflow.createElement("prazoPrevisto");

        if (null != prazo) {
            prazo.setAttribute("timeoutValue", activ.valorPrazoPrevisto);
            prazo.setAttribute("timeoutUnit", activ.unidadePrazoPrevisto);
            prazo.setAttribute("timeoutOffset", activ.referenciaPrazoPrevisto);

            ne.appendChild(prazo);
        }

        if ((null != ne) && (0 < activ.forms.length)) {
            var forms = getChildNodeByName(ne.childNodes, "formularios");
            if (null != forms)
                ne.removeChild(forms);

            forms = __wf_xmlDocWorkflow.createElement("formularios");
            var form;
            var checked;
            var valXmlChecked;

            for (var i = 0; i < activ.forms.length; i++) {
                form = __wf_xmlDocWorkflow.createElement("formulario");
                form.setAttribute("id", activ.forms[i][__wf_cNumColDivEtpForms_id]);

                checked = activ.forms[i][__wf_cNumColDivEtpForms_readOnly];
                if (true == checked)
                    valXmlChecked = "1";
                else
                    valXmlChecked = "0";

                form.setAttribute("readOnly", valXmlChecked);

                checked = activ.forms[i][__wf_cNumColDivEtpForms_required];
                if (true == checked)
                    valXmlChecked = "1";
                else
                    valXmlChecked = "0";

                form.setAttribute("required", valXmlChecked);

                checked = activ.forms[i][__wf_cNumColDivEtpForms_newOnEachOcurrence];
                if (true == checked)
                    valXmlChecked = "1";
                else
                    valXmlChecked = "0";

                form.setAttribute("newOnEachOcurrence", valXmlChecked);

                checked = activ.forms[i][__wf_cNumColDivEtpForms_requerAssinaturaDigital];              // CERTDIG
                if (true == checked)
                    valXmlChecked = "1";
                else
                    valXmlChecked = "0";

                form.setAttribute("requerAssinaturaDigital", valXmlChecked);

                if (null == activ.forms[i][__wf_cNumColDivEtpForms_originalStageId])
                    form.setAttribute("originalStageId", "");
                else
                    form.setAttribute("originalStageId", activ.forms[i][__wf_cNumColDivEtpForms_originalStageId]);


                if (null == activ.forms[i][__wf_cNumColDivEtpForms_ordemFormularioEtapa])
                    form.setAttribute("ordemFormularioEtapa", "");
                else
                    form.setAttribute("ordemFormularioEtapa", activ.forms[i][__wf_cNumColDivEtpForms_ordemFormularioEtapa]);


                form.setAttribute("title", activ.forms[i][__wf_cNumColDivEtpForms_title]);
                form.setAttribute("name", activ.forms[i][__wf_cNumColDivEtpForms_name]);
                forms.appendChild(form);
            }
            ne.appendChild(forms);
        }
        if ((null != ne) && (0 < activ.accessGroups.length)) {
            var groups = getChildNodeByName(ne.childNodes, "gruposComAcesso");
            if (null != groups)
                ne.removeChild(groups);

            groups = __wf_xmlDocWorkflow.createElement("gruposComAcesso");
            var group;
            for (var i = 0; i < activ.accessGroups.length; i++) {
                group = __wf_xmlDocWorkflow.createElement("grupo");
                group.setAttribute("id", activ.accessGroups[i][__wf_cNumColDivEtpGroups_id]);
                group.setAttribute("name", activ.accessGroups[i][__wf_cNumColDivEtpGroups_name]);
                group.setAttribute("accessType", activ.accessGroups[i][__wf_cNumColDivEtpGroups_accessType]);
                groups.appendChild(group);
            }
            ne.appendChild(groups);
        }
        atualizaHiddenField(__wf_xmlDocWorkflow);
    }
}


/// <summary>
/// Função para incluir no XML dados dos grupos de acessos grid 'Pessoas com Acessos' da divEtapa 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. 
/// Assume que o objeto __wf_activObj contém os dados a serem incluídos e a objeto __wf_xmlDocWorkflow
/// contém o xml com as informações. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de 'pessoas com acessos' da div Etapa
/// </param>
/// <returns type="void">void</returns>
function atualizaXmlComAccessGroupsDaDivEtapa() {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");
    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (sets != null)
        ne = getEtapaChildNodeByActivityId(sets, __wf_activObj.activityId);


}

/// <summary>
/// Definição do objeto size de um 'set' do xml 
/// </summary>
/// <returns type="void">void</returns>
function __wf_setSize() {
    this.width = 0;
    this.height = 0
}


/// <summary>
/// Insere no XML uma atividade. 
/// .</summary>
/// <param name="activ" type="objeto">Objeto atividade a ser inserido no XML.</param>
/// <returns type="void">void</returns>
function insertActivityNode(xmlDoc, activ, tipoElemento) {
    var ds = xmlDoc.getElementsByTagName("dataSet");
    var name = activ.activityId + ". " + activ.name;
    var toolText = activ.shortDescription;
    var inicial = activ.inicial ? "1" : "0";
    var ocultaBotoes = activ.ocultaBotoes ? "1" : "0";

    var size = new __wf_setSize();
    getActicitySize(name, size, activ.codigoSubfluxo > 0);

    var ne;
    // procura pela etapa no xml
    ne = getEtapaChildNodeByActivityId(__wf_nlElementos, activ.activityId);

    // se não foi encontrada a etapa
    if (null == ne) {

        // ---------------------------------------------------------------------------  //
        // cria o nó XML referente ao elemento gráfico  //
        var ne = xmlDoc.createElement("set");
        ne.setAttribute("shape", "RECTANGLE");
        ne.setAttribute("id", activ.elementId);
        ne.setAttribute("name", name);
        ne.setAttribute("toolText", toolText);
        ne.setAttribute("x", 5);
        ne.setAttribute("y", 95);
        ne.setAttribute("width", size.width);
        ne.setAttribute("height", size.height);
        ne.setAttribute("color", "FFBC79");
        ne.setAttribute("tipoElemento", tipoElemento);
        ne.setAttribute("etapaInicial", inicial);
        ne.setAttribute("grupoWorkflow", "0");
        ne.setAttribute("idElementoInicioFluxo", "-1");
        ne.setAttribute("ocultaBotoesAcao", ocultaBotoes);
        ne.setAttribute("codigoSubfluxo", activ.codigoSubfluxo);
        if (activ.codigoSubfluxo > 0) {
            ne.setAttribute("imageNode", "1");
            ne.setAttribute("imageURL", "../imagens/Workflow/subprocessopequeno.png");
            ne.setAttribute("imageWidth", "28");
            ne.setAttribute("imageHeight", "20");
            ne.setAttribute("imageAlign", "BOTTOM");
        }
        // ---------------------------------------------------------------------------  //
    }
    else {
        activ.elementId = ne.getAttribute("id"); // atribui o id do elemento ao atributo da variável global
        ne.setAttribute("name", name);
        ne.setAttribute("toolText", toolText);
        ne.setAttribute("width", size.width);
        ne.setAttribute("height", size.height);
        ne.setAttribute("etapaInicial", inicial);
        ne.setAttribute("ocultaBotoesAcao", ocultaBotoes);
        ne.setAttribute("codigoSubfluxo", activ.codigoSubfluxo);
    }

    ds[0].appendChild(ne);
}

/// <summary>
/// função para calcular a largura e a altura que deverá ter, no gráfico de workflow, o elemento do
/// conjunto "set" do gráfico.
/// atividade/etapa.
/// </summary>
/// <remarks>
/// caso o parâmetro <paramref name="name"> não contenha um nome, será registrado no parâmetro <paramref name="size"> 
/// o tamanho padrão para um elemento "set".
/// </remarks>
/// <param name="name" type="string">O nome informado para a atividade.</param>
/// <param name="size" type="objeto">Objeto para conter o tamanho da figura.</param>
/// <returns type="void"></returns>
function getActicitySize(name, size, subprocesso) {
    var pixelByChar = 9;
    var minWidth = 64;
    var minHeight = 32;
    var tamPrimeiraPalavra = getLengthFirstActivityWord(name);
    var tamMaiorPalavra = getLengthMajorWord(name);
    var tamFrase = name.length;

    if (tamPrimeiraPalavra > 15) {
        if (tamFrase > tamPrimeiraPalavra)
            size.width = minWidth + 46;
        else
            size.width = minWidth + 30;
    }
    else if (tamPrimeiraPalavra > 13) {
        if (tamFrase > tamPrimeiraPalavra)
            size.width = minWidth + 26;
        else
            size.width = minWidth + 10;
    }
    else if ((tamPrimeiraPalavra > 10) && (tamFrase > tamPrimeiraPalavra) & (tamFrase < 24))
        size.width = minWidth + 10
    else if (tamFrase >= 24)
        size.width = minWidth + 26;
    else if (tamFrase > 22)
        size.width = minWidth + 20;
    else if (tamFrase > 20)
        size.width = minWidth + 10;
    else
        size.width = minWidth;

    if ((tamFrase * pixelByChar / size.width) > 3)
        size.height = minHeight + 10;
    else
        size.height = minHeight;

    if (subprocesso)
        size.height = size.height + 20;

    return
}

function getLengthFirstActivityWord(str) {
    /// <summary>
    /// função para devolver o tamanho da primeira palavra usada para nomear uma atividade. 
    /// Inclui no tamanho a numeração da etapa que acaba por fazer parte do nome da etapa.
    /// .</summary>
    /// <param name="str" type="string">O nome informado para a atividade.</param>
    /// <returns type="integer">A quantidade de caracteres que possui a primeira palavra usada para 
    /// nomear a atividade/etapa.</returns>
    var s = str.split(" ", 2);
    if (s.length > 1)
        return s[0].length + s[1].length + 1;
    else
        return 0;
}

/// <summary>
/// função para mostrar uma div na tela de edição de workflow
/// </summary>
/// <returns type="void">void</returns>
function mostraDivEdicaoWorkflow(nomeDiv) {

    var fundo = $el("__wf_divFundo");
    var div = $el(nomeDiv);

    var posXaux = 200;

    if ((null == div) || (null == fundo))
        return;

    var tela;
    if (document.documentElement)
        tela = document.documentElement;
    else
        tela = document.body;

    fundo.style.top = "0px";
    fundo.style.left = "0px";
    fundo.style.width = tela.clientWidth;
    fundo.style.height = tela.clientHeight;
    fundo.style.display = "block";

    if ("divEtapa" == nomeDiv)
        posXaux = 360;
    else if ("divConector" == nomeDiv)
        posXaux = 110;
    else if ("divTimer" == nomeDiv)
        posXaux = 390;
    else if ("divDescricao" == nomeDiv)
        posXaux = 220;
    else if ("divGridEdicaoElementos" == nomeDiv)
        posXaux = 280;
    else if ("divAcaoWf" == nomeDiv)
        posXaux = 390;

    var alturaDiv = div.style.height.toString().replace("px", "");
    var topDiv = (screen.availHeight - alturaDiv) / 2 - posXaux;
    div.style.top = topDiv + "px";

    var larguraDiv = div.style.width.toString().replace("px", "");
    var leftDiv = (tela.clientWidth - larguraDiv) / 2;
    div.style.left = leftDiv + "px";


    div.style.display = "block";

    if ("divEtapa" == nomeDiv) {
        // deixa sempre o foco no campo Nome abreviado e a aba formulários sendo mostrada
        tcDivEtapa.SetActiveTab(tcDivEtapa.GetTab(0));
        edtNomeAbreviado_etp.Focus();
    }
    else if ("divConector" == nomeDiv) {
        tcDivConector.SetActiveTab(tcDivConector.GetTab(0));
        cmbEtapaOrigem_cnt.Focus();
    }
    else if ("divTimer" == nomeDiv) {
        tcDivTimer.SetActiveTab(tcDivTimer.GetTab(0));
        cmbEtapaOrigem_tmr.Focus();
    }
    else if ("divDescricao" == nomeDiv) {
        txtDescricaoDivDescricao.Focus();
    }
    else if ("divAcaoWf" == nomeDiv) {
        //...
    }

    return true;
}

/// <summary>
/// Devolve o nó na lista nós <paramref name="nodes"/> que se referir à etapa indicada em <paramref name="etapaId"/>.
/// </summary>
/// <param name="nodes" type="XML Nodes">Coleção, no formato XML, que contém os nós.</param>
/// <param name="etapaId" type="string">Id da etapa que será usado para localizar o nó procurado.</param>
/// <returns type="XML Node">O nó localizado. (Objeto DOM).</returns>
function getEtapaChildNodeByActivityId(nodes, etapaId) {
    var node = null;

    if (null != nodes) {
        var name, id;
        var i;

        for (i = 0; i < nodes.length; i++) {
            if (nodes[i].nodeType != 1)
                continue;

            tipo = obtemTipoElemento(__wf_nlElementos[i]);
            if ((__wf_cTipoElementoEtapa == tipo)) {
                name = nodes[i].getAttribute("name");
                if (null != name) {
                    id = getEtapaIdFromEtapaName(name);
                    if ((null != id) && (id == etapaId))
                        break;
                }
            }
        }

        // se o loop foi interrompido antes de varrer todos os nós, é sinal que localizou o nó procurado
        if (i < nodes.length)
            node = nodes[i];
    }

    return node;
}

/// <summary>
/// Devolve o id de uma etapa contido na propriedade 'name' de uma etapa
/// </summary>
/// <remarks>
/// A propriedade 'name' de uma etapa no XML está sempre no formato "N. XXXXXXX", onde N é o ID da etapa
/// e XXX o nome
/// </remarks>
/// <param name="name" type="string">Propriedade 'name' da etapa</param>
/// <returns type="string">O ID da etapa, ou null caso não seja possível determinar o ID da etapa</returns>
function getEtapaIdFromEtapaName(name) {
    var id = null;
    if (null != name) {
        var pos = name.indexOf(".");
        if (-1 == pos) {
            if ("Início" == name)
                id = 0;
        }
        else {
            id = name.substring(0, pos)
        }
    }
    return id;
}

/// <summary>
/// Devolve o nome de uma etapa retirando o id da etapa que eventualmente esteja compondo o nome da etapa
/// </summary>
/// <remarks>
/// A propriedade 'name' de uma etapa no XML está sempre no formato "N. XXXXXXX", onde N é o ID da etapa
/// e XXX o nome. Esta função devolve a parte XXXXXX
/// </remarks>
/// <param name="name" type="string">Propriedade 'name' da etapa</param>
/// <returns type="string">O nome da etapa</returns>
function getNomeEtapaSemId(name) {
    var nomeEtapa = name;
    if (null != name) {
        var pos = name.indexOf(".");

        if ((pos >= 0) && (pos < (name.length - 2)))
            nomeEtapa = name.substr(pos + 2)
    }
    return nomeEtapa;
}

/// <summary>
/// remove um elemento gráfico do XML, removendo também os elementos 'connector' com 
/// ligações ao elementos.
/// </summary>
/// <remarks>
/// Por elemento gráfico, entende-se elemento dentro do nó "dataSet" do gráfico do xml.
/// Esta função apenas retira os nós XML's 'connector' que se ligam ao elemento e aos conec
/// </remarks>
/// <param name="elementID" type="string">id do elemento gráfico a ser excluído.</param>
/// <returns type="boolean">Se deu certo a exclusão do elemento, retorna true. Caso contrário, false.</returns>
function removeElementoFromXml(elementID) {
    // exclui as conexões do elemento
    excluiConexoesElementoGrafico(elementID);

    // exclui o próprio elemento
    excluiElementoDoXml(elementID);

    // atualiza gráfico na tela
    atualizaXmlDoGrafico(__wf_xmlDocWorkflow);
    return true;
}

/// <summary>
/// Exclui as conexões de um elemento gráfico, atualizando todas as informações do XML.
/// </summary>
/// <remarks>
/// Localiza cada conexão do elementos e as exclui, atualizando as informações no XML.
/// </remarks>
/// <param name="idElemento" type="string">id do elemento gráfico cujas conexões serão excluídas.</param>
/// <returns type="void"></returns>
function excluiConexoesElementoGrafico(idElemento) {
    var to, from;
    var setaInicio, setaFim;
    var label;
    var connector = new xConnector();

    // varre a lista de conexões para excluir as conexões do elemento em questão
    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        from = __wf_nlConectores[i].getAttribute("from");
        to = __wf_nlConectores[i].getAttribute("to");

        if ((null != from) && (null != to) && ((from == idElemento) || (to == idElemento))) {
            // se a ligação estiver saindo do elemento, irá verificar se é o destino é um timer para excluí-lo
            if (from == idElemento) {
                var ne = getChildNodeById(__wf_nlElementos, to);

                // se o elemento 'to' for do tipo timer
                if (ehElementoDoTipo(ne, __wf_cTipoElementoTimer)) {
                    var auxTo = getFirstToID(to);
                    if (null != auxTo) {
                        __wf_timerObj.elementId = to;
                        __wf_timerObj.from = idElemento;
                        __wf_timerObj.to = auxTo;

                        removeTimerFromXml(__wf_timerObj);

                        i = 0; // recomeça o loop pois a variável __wf_nlConectores foi alterada;
                        continue;
                    } // if (null != auxTo)
                } // if ( ehElementoDoTipo(ne, __wf_cTipoElementoTimer) )
            } // if( from == idElemento )

            setaInicio = __wf_nlConectores[i].getAttribute("arrowAtStart");
            if (null == setaInicio)
                setaInicio = "0";

            setaFim = __wf_nlConectores[i].getAttribute("arrowAtEnd");
            if (null == setaFim)
                setaFim = "0";

            label = null;
            if (((from == idElemento) && ("1" == setaInicio)) ||
                ((to == idElemento) && ("1" == setaFim)))
                label = __wf_nlConectores[i].getAttribute("label");

            if ((null != label) && ("1" == setaInicio) && ("1" == setaFim))
                label = decompoeLabelAcao(idElemento, from, to, label);


            if (null == label)
                connector.acao = "";
            else
                connector.acao = label;  // assume "" quando não tiver label

            if ("1" == setaFim) {
                connector.from = from;
                connector.to = to;
            } else {
                connector.from = to;
                connector.to = from;
            }

            // retira a conexão e atualiza o XML, incluindo a variável __wf_nlConectores
            removeConnectorFromXml(connector);
            i = 0; // recomeça o loop pois a variável __wf_nlConectores foi alterada;
        } // if ( (null != from) && ...
    } // for (var i=0; i<__wf_nlConectores; i++)

    return;
}


// ------------------------------------------------------------------------------------------------- //
// ------ fim de bloco ~(Funções para inclusão de nova etapa)                                ------ //
// ------------------------------------------------------------------------------------------------- //


// ------------------------------------------------------------------------------------------------- //
// ------                                                                                     ------ //
// ------ Funções para inclusão de novo Subprocesso                                           ------ //
// ------                                                                                     ------ //
// ------------------------------------------------------------------------------------------------- //

/// <summary>
/// Função para criar um novo objeto do tipo subprocesso. 
/// </summary>
/// <param name="id" type="integer">Id no gráfico para o objeto em questão.</param>
/// <returns type="void"></returns>
function xSubprocess(id) {
    this.elementId = id;
    this.activityId = 0;
    this.name = "";
    this.shortDescription = "";
    this.description = "";
    this.inicial = false;
    this.ocultaBotoes = false;
    this.valorPrazoPrevisto = "";
    this.unidadePrazoPrevisto = "";
    this.referenciaPrazoPrevisto = "";
    this.codigoSubfluxo = 0;

    // declara matriz para guardar as informações dos grupos que serão usados na etapa
    this.forms = new Array();

    // declara matriz para guardar as informações dos grupos que terão acessos à etapa
    this.accessGroups = new Array();
}

/// <summary>
/// função para limpar os campos da div subprocesso. usada ao mostrar a div para o usuário.
/// </summary>
/// <returns type="void">void</returns>
function limpaCamposDivSubprocesso() {

    var val = null;

    edtNomeAbreviado_sub.SetValue(val);
    edtDescricaoResumida_sub.SetValue(val);
    mmDescricao_sub.SetValue(val);
    cmbFluxos_sub.SetSelectedIndex(-1);
    return;
}

/// <summary>
/// função para prepara a div subprocesso para ser exibida.
/// </summary>
/// <remarks>
/// No momento da exibição da div subprocesso é necessário fazer alguns ajustes na div que depende 
/// se a div está sendo mostrada para a inclusão de um novo subprocesso ou para a edição de um
/// subprocesso já existente
/// Assume que os dados a serem mostrados já estão nos controles dentro da div.
/// </remarks>
/// <param name="acao" type=string>Letra indicando se a exibição é para uma inclusão ou para uma 
/// edição de uma etapa. Letas I ou E</param>
/// <returns type="void">void</returns>
function preparaExibicaoDivSubprocesso(acao) {
    lbWorkflow_sub.SetValue(obtemIdentificaoWorkflow());

    if ("I" == acao) {
        limpaCamposDivSubprocesso();

        __wf_subprcObj.elementId = getNextElementId(__wf_nlElementos);
        __wf_subprcObj.activityId = getNextActivityId(__wf_nlElementos);

        edtIdEtapa_sub.SetValue(__wf_subprcObj.activityId.toString());
    }
}

/// <summary>
/// Trata o 'click' no botão OK da div subprocesso. 
/// </summary>
/// <remarks>
/// Realiza as consistências necessárias e, se tudo certo, inclui no gráfico  o subprocesso cadastrado.
/// </remarks>
/// <param name="s" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
/// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
function onOkDivSubprocessoClick(s, e) {
    var bRet = false;
    e.processOnServer = false;  // por enquanto, não processar no server

    if (preenchimentoDivSubprocessoOK()) {
        __wf_subprcObj.activityId = edtIdEtapa_sub.GetValue();
        __wf_subprcObj.name = edtNomeAbreviado_sub.GetValue();
        __wf_subprcObj.shortDescription = edtDescricaoResumida_sub.GetValue();
        __wf_subprcObj.codigoSubfluxo = cmbFluxos_sub.GetValue();
        __wf_subprcObj.description = mmDescricao_sub.GetValue();
        __wf_subprcObj.ocultaBotoes = "1";

        // tratamento para o caso de não ter informado
        if (null == __wf_subprcObj.description)
            __wf_subprcObj.description = "";

        if (true == addActivityNodeInXml(__wf_xmlDocWorkflow, __wf_subprcObj, __wf_cTipoElementoEtapa)) {
            var wCnn = new xConnector();
            wCnn.from = '0';
            wCnn.to = __wf_subprcObj.elementId;
            wCnn.acao = "";
            removeConnectorFromXml(wCnn);

            // desabilita o botão 'Salvar' e oculta a div. o botão salvar será reabilitado após 
            // o cliente ter recebido os dados das grids e os gravado no xml.
            //            btnSalvar.SetEnabled(false);

            divSubprocesso.Hide();

            // remonta o xml do subprocesso
            remontaXmlEtapa(__wf_subprcObj);
            bRet = true;

        }
        else  // se não deu certa a inclusão da etapa
        {
            bRet = false;
            window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_do_novo_subprocesso_ + " (0004)", 'erro', true, false, null);
        }
    }

    return bRet;
}

/// <summary>
/// função para validar se o preenchimento da divSubprocesso está ok.
/// (Todos os campos obrigatórios foram preenchidos? Não está incluindo uma etapa em duplicidade?
/// </summary>
/// <returns type="boolean">Se tudo deu certo no preenchimento da Div, retorna true. Caso contrário, false.</returns>
function preenchimentoDivSubprocessoOK() {
    var bRet = true;
    var idEtapa = edtIdEtapa_sub.GetValue()
    var NomeEtapa = edtNomeAbreviado_sub.GetValue();
    var DescricaoEtapa = edtDescricaoResumida_sub.GetValue();
    var codigoSubfluxo = cmbFluxos_sub.GetValue();

    if (true == bRet) {
        if ((null == NomeEtapa) || (0 == NomeEtapa.length)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_o_campo__nome_abreviado____obrigat_rio_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        if ((null == DescricaoEtapa) || (0 == DescricaoEtapa.length)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_o_campo__descri__o_resumida____obrigat_rio_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        if ((null == codigoSubfluxo) || (0 == codigoSubfluxo.length)) {
            window.top.mostraMensagem(traducao.WorkflowCharts___preciso_selecionar_o_fluxo_a_ser_executado_como_subprocesso_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        // se a etapa já existe e tiver ID diferente da atual (retorno 1), não deixa continuar
        if (1 == etapaJaCadastradaNoXml(idEtapa, NomeEtapa)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_j__existe_uma_etapa_com_o_nome_informado_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    return bRet;
}

// ------------------------------------------------------------------------------------------------- //
// ------ fim de bloco ~(Funções para inclusão de novo Subprocesso)                           ------ //
// ------------------------------------------------------------------------------------------------- //

// ------------------------------------------------------------------------------------------------- //
// ------                                                                                     ------ //
// ------ Funções para inclusão de novo conector                                              ------ //
// ------                                                                                     ------ //
// ------------------------------------------------------------------------------------------------- //

function xConnector() {
    /// <summary>
    /// Função para criar um novo objeto conector. 
    /// </summary>
    /// <returns type="void"></returns>
    this.from = "";
    this.to = "";
    this.acao = "";
    this.assuntoNotificacao1 = "";
    this.textoNotificacao1 = "";
    this.assuntoNotificacao2 = "";
    this.textoNotificacao2 = "";

    // declara matriz para guardar as informações dos grupos que serão notificados pela ação do conector
    this.notificationGroups = new Array();

    // declara matriz para guardar as informações sobre as ações automáticas do conector
    this.automaticActions = new Array();

    // declara matriz para guardar as informações sobre os acionamentos de API
    this.acionamentosAPI = new Array();

    // declara matriz para guardar as informações sobre os parâmetros de acionamentos de API
    this.parametrosAcionamentosAPI = new Array();

    this.temAcoesAutomaticas = false;
    this.solicitaAssinaturaDigital = false;
    this.corBotao = "";
    this.iconeBotao = "";
    this.condition = "";
    this.decodedCondition = "";
    this.conditionIndex = "";
}

/// <summary>
/// função para prepara a div Conector para ser exibida.
/// </summary>
/// <remarks>
/// No momento da exibição da div conector é necessário fazer alguns ajustes na div que depende 
/// se a div está sendo mostrada para a inclusão de um novo conector ou para a edição de um
/// conector já existente
/// Assume que os dados a serem mostrados já estão nos controles dentro da div.
/// </remarks>
/// <param name="acao" type=string>Letra indicando se a exibição é para uma inclusão ou para uma 
/// edição de um conector. Letas I ou E</param>
/// <returns type="void">Se tudo deu certo, retorna true. Caso contrário, false</returns>
function preparaExibicaoDivConector(acao) {
    SetaInteratividadeCamposDivConector();
    return true;
}

/// <summary>
/// função para atribuir as propriedades de interatividade para os campos da div conector
/// </summary>
/// <remarks>
/// Com base nas informações se a No momento da exibição da div conector é necessário fazer alguns ajustes na div que depende 
/// se a div está sendo mostrada para a inclusão de um novo conector ou para a edição de um
/// conector já existente
/// Assume que os dados a serem mostrados já estão nos controles dentro da div.
/// </remarks>
/// <param name="acao" type=string>Letra indicando se a exibição é para uma inclusão ou para uma 
/// edição de um conector. Letas I ou E</param>
/// <returns type="void">void</returns>
function SetaInteratividadeCamposDivConector() {
    var fromID = cmbEtapaOrigem_cnt.GetValue();
    var bPodeTerOpcao, bPodeTerAcao;
    var div = $el("divConector");
    var tdOpt = $el("tdOpcoes");

    if (null == fromID) {
        bPodeTerOpcao = true;
        bPodeTerAcao = true;
    }
    else {
        var ehSubprocesso;
        var elemento = getChildNodeById(__wf_nlElementos, fromID);
        var tipo = obtemTipoElemento(elemento);

        if (__wf_cTipoElementoEtapa == tipo)
            ehSubprocesso = f_ehElementoSubProcesso(elemento);
        else
            ehSubprocesso = false;

        var qtdFormsRequerAssinaturaDigital = frv_qtdFormulariosRequerAssinatura(null, fromID);
        if (qtdFormsRequerAssinaturaDigital === 0) {
            tdSolicAssinaturaDigital.hidden = "hidden";
            cbSolicitarAssinaturaDigital.visible = false;
        } else if (hfValoresTela.Get("utilizaAssinaturaDigitalFormularios") == "S") {
            tdSolicAssinaturaDigital.hidden = "";
            cbSolicitarAssinaturaDigital.visible = true;
        }

        if ((__wf_cTipoElementoJuncao == tipo) || (__wf_cTipoElementoDisjuncao == tipo) || (__wf_cTipoElementoSubprocesso == tipo) || (true == ehSubprocesso))
            bPodeTerOpcao = false;
        else
            bPodeTerOpcao = true;

        if (__wf_cTipoElementoDisjuncao == tipo)
            bPodeTerAcao = false;
        else
            bPodeTerAcao = true;
    }

    //-- by alejandro 25/02/2010
    //    if (!bPodeTerOpcao)
    //        edtAcao_cnt.SetValue(null);
    var height;
    if (bPodeTerAcao)
        height = "500px";
    else
        height = "125px";

    //    div.style.height = height;
    tdOpt.style.display = bPodeTerOpcao ? "block" : "none";
    paDadosAuxConector.SetVisible(bPodeTerAcao);
    tcDivConector.SetActiveTabIndex(0);
    pcTipoMensagemAcao.SetActiveTabIndex(0);
    if (gv_Acionamentos.GetVisibleRowsOnPage() > 0) {
        tabelaAcionamentosAPI.style.display = 'none';
    }
    else {
        tabelaAcionamentosAPI.style.display = 'block';
    }
    //---------------------------------------fim by alejandro 25/02/2010
}


function limpaCamposDivConector(tipoEdicao) {
    /// <summary>
    /// função para limpar os campos da div etapa. usada ao mostrar a div para o usuário.
    /// </summary>
    /// <returns type="void">void</returns>

    if (tipoEdicao == 'I') {
        gv_Acoes_cnt.PerformCallback('LimpaGrid');
        gv_GruposNotificados_cnt.PerformCallback('LimpaGrid');
        gv_ParametrosAcionamentos.PerformCallback('LimpaGrid');
        gv_Acionamentos.PerformCallback('LimpaGrid');
    }

    var val = "";

    lbWorkflow_cnt.SetValue(obtemIdentificaoWorkflow());
    cmbEtapaOrigem_cnt.ClearItems(); // limpa os itens do combo box
    cmbEtapaOrigem_cnt.SetSelectedIndex(-1);
    cmbEtapaOrigem_cnt.cpOriginalFrom = ""; // propriedade adicionada para guardar o valor anterior

    cmbEtapaDestino_cnt.ClearItems(); // limpa os itens do combo box
    cmbEtapaDestino_cnt.SetSelectedIndex(-1);
    cmbEtapaDestino_cnt.cpOriginalTo = ""; // propriedade adicionada para guardar o valor anterior

    edtAcao_cnt.SetValue(val); // limpa campo ação.
    edtAcao_cnt.cpOriginalAction = ""; // propriedade adicionada para guardar o valor anterior
    cbSolicitarAssinaturaDigital.SetChecked(false);

    //by Alejandro--------------------------------------------------------------------------------------------
    document.getElementById("txtAssunto1_cnt").value = val;
    document.getElementById("mmTexto1_cnt").value = val;
    //------------------------------------------
    document.getElementById("txtAssunto2_cnt").value = val;
    document.getElementById("mmTexto2_cnt").value = val;

    // Esconde o check box [cbSolicitaAssinaturaDigital]
    tdSolicAssinaturaDigital.hidden = "hidden";
    cbSolicitarAssinaturaDigital.visible = false;

    preencheCombosDivConector();

    return;
}

/// <summary>
/// função para preencher os combosBoxes da div Conector.
/// </summary>
/// <returns type="void">void</returns>
function preencheCombosDivConector() {

    var elName;
    var elId = new Object();
    var tipo;

    var tipos = new Array();
    tipos[0] = __wf_cTipoElementoEtapa;
    tipos[1] = __wf_cTipoElementoDisjuncao;
    tipos[2] = __wf_cTipoElementoJuncao;
    tipos[3] = __wf_cTipoElementoFim;

    var linhas = new Array();
    var k = 0;

    for (var j = 0; j < tipos.length; j++) {
        for (var i = 0; i < __wf_nlElementos.length; i++) {
            if (__wf_nlElementos[i].nodeType != 1)
                continue;

            tipo = obtemTipoElemento(__wf_nlElementos[i]);
            if ((true == ehElementoManualmenteConectavel(__wf_nlElementos[i]))
                && (tipos[j] == tipo)) {
                elName = getElementIdName(__wf_nlElementos[i]);
                elId = __wf_nlElementos[i].getAttribute("id");

                if ((null != elName) && (null != elId)) {
                    linhas[k] = new Array();
                    linhas[k][0] = elId;
                    linhas[k][1] = elName;
                    linhas[k][2] = tipo;
                    k++;
                } // if (null != ...
            } // if (true == ...
        } // for i
    } // for j

    // ordena a matriz antes de adicionar ao drop down list;
    if (0 < linhas.length)
        linhas.sort(sortArrayEtapas);

    // adiciona os demais itens
    for (var i = 0; i < linhas.length; i++) {
        // os elementos fins não podem constar como origem de uma conexão. 
        if (__wf_cTipoElementoFim != linhas[i][2])
            cmbEtapaOrigem_cnt.AddItem(linhas[i][1], linhas[i][0]);

        cmbEtapaDestino_cnt.AddItem(linhas[i][1], linhas[i][0]);
    } // for 
}


/// <summary>
/// Trata o 'click' no botão OK da div Conector. 
/// </summary>
/// <remarks>
/// Realiza as consistências necessárias e, se tudo certo, inclui no gráfico a etapa cadastrada.
/// Após incluir no gráfico, solicita ao servidor as informações adicionais preenchidas nas grids da div.
/// A função que recebe as informações das grids as inclui no xml assim que as recebe.
/// </remarks>
/// <param name="s" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
/// <param name="e" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
/// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
function onOkDivConectorClick(s, e) {
    lpLoading.Show();
    e.processOnServer = false;  // por enquanto, não processar no server
    var tipoElementoOrigem = null;
    if (validacaoInicialDivConector(tipoElementoOrigem)) {
        // se for uma disjunção, já chama a função que irá processar o preenchimento da divConector
        if (__wf_cTipoElementoDisjuncao == tipoElementoOrigem) {
            processaPreenchimentoDivConector();
        }
        else {
            // se não for uma disjunção, chama a função para verificar se os grupos foram informados corretamente, que chamará a função para processar o preenchimento da divConector
            gv_GruposNotificados_cnt.GetPageRowValues("CodigoPerfilWf;NomePerfilWf;Mensagem", /// <summary>
                function validaGruposNotif_Cnt(valores) {

                    var bRet = true, bMensagemAcao = false, bMensagemAcomp = false;
                    var cTipoMsg = "";

                    for (var i = 0; i < valores.length; i++) {
                        cTipoMsg = valores[i][2];
                        if (traducao.WorkflowCharts_a__o == cTipoMsg)
                            bMensagemAcao = true;
                        else if (traducao.WorkflowCharts_acompanhamento == cTipoMsg)
                            bMensagemAcomp = true;
                    }

                    if (true == bRet) {
                        if (("" == __wf_connectorObj.textoNotificacao1) && (true == bMensagemAcao)) {
                            lpLoading.Hide();
                            window.top.mostraMensagem(traducao.WorkflowCharts_foram_informados_grupos_para_receberem_a__mensagem_a__o___mas_n_o_foi_informado_o_texto_da_notifica__o_, 'atencao', true, false, null);
                            bRet = false;
                        }
                        else if (("" !== __wf_connectorObj.textoNotificacao1) && (false == bMensagemAcao)) {
                            lpLoading.Hide();
                            window.top.mostraMensagem(traducao.WorkflowCharts_foi_informado_o_texto_da__mensagem_a__o__mas_n_o_foi_indicado_nenhum_grupo_para_receber_esta_mensagem_, 'atencao', true, false, null);
                            bRet = false;
                        }
                    } // if (true == bRet)

                    if (true == bRet) {
                        if (("" == __wf_connectorObj.textoNotificacao2) && (true == bMensagemAcomp)) {
                            lpLoading.Hide();
                            window.top.mostraMensagem(traducao.WorkflowCharts_foram_informados_grupos_para_receberem_a__mensagem_acompanhamento___mas_n_o_foi_informado_o_texto_da_notifica__o_, 'atencao', true, false, null);
                            bRet = false;
                        }
                        else if (("" !== __wf_connectorObj.textoNotificacao2) && (false == bMensagemAcomp)) {
                            lpLoading.Hide();
                            window.top.mostraMensagem(traducao.WorkflowCharts_aten__o__foi_informado_o_texto_da__mensagem_acompanhamento__mas_n_o_foi_indicado_nenhum_grupo_para_receber_esta_mensagem_, 'atencao', true, false, null);
                            bRet = false;
                        }
                    } // if (true == bRet)
                    //--------------------------------------------------------------------------------------------------

                    if (true == bRet) {
                        // processa o preenchimento da div gravando os dados no XML
                        processaPreenchimentoDivConector();
                    }

                    return;
                });
        }

    }
    return;
}

/// <summary>
/// faz as validações iniciais do preenchimento da divConector.
/// </summary>
/// <remarks>
/// Esta verifica se o preenchimento da divConector está ok.
/// (Os campos obrigatórios foram preenchidos? Não está incluindo um conector duplicidade?, etc)
/// </remarks>
/// <returns type="boolean">Se as informações iniciais da Div estão condizentes, retorna true. Caso contrário, false.</returns>
function validacaoInicialDivConector(tipoElementoOrigem) {
    var bRet = true;

    var from = cmbEtapaOrigem_cnt.GetValue();
    var to = cmbEtapaDestino_cnt.GetValue();

    // quando editando a conexão inicio => xxx, o cmbEtapaOrigem não contém a etapa Início, então 
    // o componente cmbEtapaOrigem_cnt devolve como GetValue o próprio texto e não o código
    if (("Início" == from))
        from = "0";

    if (true == bRet) {
        if (("" == from) || (null == from) || ("" == to) || (null == to)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_os_campos__etapa_origem__e__etapa_destino__s_o_obrigat_rios_, 'atencao', true, false, null);
            bRet = false;
        }
    } // if (true == bRet)

    if (true == bRet) {
        __wf_connectorObj.from = from;
        __wf_connectorObj.to = to;

        if (__wf_connectorObj.from == __wf_connectorObj.to) {
            window.top.mostraMensagem(traducao.WorkflowCharts_as_etapas_de_origem_e_destino_n_o_podem_ser_a_mesma_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        // verificando se está conectando duas etapas já conectadas;

        if ("0" == from) // se a edição é do conector Início => xxx, deixa passar
            bRet = true;

        // se for uma edição, e não se alterou as etapas sendo ligadas, é lógico que já existirá no xml 
        // o registro desta conexão. Neste caso, deixa passar
        else if ((cmbEtapaOrigem_cnt.cpOriginalFrom == __wf_connectorObj.from) &&
            (cmbEtapaDestino_cnt.cpOriginalTo == __wf_connectorObj.to))
            bRet = true;
        else if (true == conexaoExistente(__wf_connectorObj)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_j__existe_um_conector_ligando_estas_duas_etapas__favor_verificar_as_etapas_selecionadas_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        __wf_connectorObj.acao = edtAcao_cnt.GetValue();
        tipoElementoOrigem = obtemTipoElemento(getChildNodeById(__wf_nlElementos, __wf_connectorObj.from));

        // ação pode não ser informada pelo usuário
        if (null == __wf_connectorObj.acao)
            __wf_connectorObj.acao = "";

        if (__wf_cTipoElementoEtapa == tipoElementoOrigem) {
            if ("" == __wf_connectorObj.acao) {
                if (false == f_ehElementoSubProcesso(getChildNodeById(__wf_nlElementos, __wf_connectorObj.from))) {
                    window.top.mostraMensagem(traducao.WorkflowCharts___preciso_indicar_qual_a_op__o_que_far__a_conex_o_entre_a_etapa_origem_e_destino_, 'atencao', true, false, null);
                    bRet = false;
                }
            } // if ( "" == __wf_connectorObj.acao)
        } // if ( __wf_cTipoElementoEtapa == tipoElementoOrigem )
        else {

            if ("0" != from)
                __wf_connectorObj.acao = "";

            // nenhuma ação ou notificação pode ser feita quando o fluxo sai de uma disjunção
            if (__wf_cTipoElementoDisjuncao == tipoElementoOrigem) {
                deleteGridRows(gv_GruposNotificados_cnt);
                deleteGridRows(gv_Acoes_cnt);
                deleteGridRows(gv_Acionamentos);
                //by Alejandro 02/03/2010----------------------------------------------------------------
                document.getElementById("mmTexto1_cnt").value = null;
                document.getElementById("txtAssunto1_cnt").value = null;
                //----------------------------------------------------------
                document.getElementById("mmTexto2_cnt").value = null;
                document.getElementById("txtAssunto2_cnt").value = null;

            } // if ( __wf_cTipoElementoDisjuncao == tipoElementoOrigem )
        } // else ( __wf_cTipoElementoEtapa == tipoElementoOrigem )
    }

    if (true == bRet) {
        if ("" !== __wf_connectorObj.acao) {
            if (__wf_connectorObj.acao.indexOf("/") >= 0) {
                window.top.mostraMensagem(traducao.WorkflowCharts_o_nome_usado_para_a_a__o_n_o_pode_conter_o_caracter + " '/'.", 'atencao', true, false, null);
                bRet = false;
            }
            // se foi informada uma ação e não é a mesma da anterior
            else if (("" !== __wf_connectorObj.acao) && (
                (cmbEtapaOrigem_cnt.cpOriginalFrom !== __wf_connectorObj.from) ||
                (edtAcao_cnt.cpOriginalAction.toLowerCase() !== __wf_connectorObj.acao.toLowerCase()))) {
                // se já existir a ação, não permite a alteração
                if (true == acaoExistente(__wf_connectorObj.from, __wf_connectorObj.acao)) {
                    window.top.mostraMensagem(traducao.WorkflowCharts_j__existe_uma_a__o + " '" + __wf_connectorObj.acao + "'" + traducao.WorkflowCharts__na_etapa_origem_, 'atencao', true, false, null);
                    bRet = false;
                }
            }
        } // if( "" != __wf_connectorObj.acao )
    }

    if (true == bRet) {
        // se não for dijunção, valida os campos textos e assuntos
        if (__wf_cTipoElementoDisjuncao !== tipoElementoOrigem) {

            __wf_connectorObj.textoNotificacao1 = document.getElementById("mmTexto1_cnt").value
            __wf_connectorObj.assuntoNotificacao1 = document.getElementById("txtAssunto1_cnt").value;
            __wf_connectorObj.textoNotificacao2 = document.getElementById("mmTexto2_cnt").value
            __wf_connectorObj.assuntoNotificacao2 = document.getElementById("txtAssunto2_cnt").value;
            // valida se os campos textos e assuntos foram preenchidos corretamente
            bRet = validaTextoAssunto(__wf_connectorObj);
        } // if ( __wf_cTipoElementoDisjuncao != tipoElementoOrigem )
    } // if (true == bRet)
    if (true == bRet) {
        if (__wf_cTipoElementoDisjuncao !== tipoElementoOrigem) {

            __wf_connectorObj.corBotao = ceCorBotao_cnt.GetValue();
            if (null == __wf_connectorObj.corBotao)
                __wf_connectorObj.corBotao = "";

            __wf_connectorObj.iconeBotao = cmbIconeBotao_cnt.GetValue();
            if (null == __wf_connectorObj.iconeBotao)
                __wf_connectorObj.iconeBotao = "";
        }
    }
    if (false == bRet) {
        lpLoading.Hide();
    }
    return bRet;
}

/// <summary>
/// valida se os campos Textos e Assuntos das notificações foram preenchidos corretamente.
/// </summary>
/// <remarks>
/// Assume que os atributos referentes aos textos e assuntos já estão preenchidos no objNotif.
/// </remarks>
/// <returns type="boolean">Se os campos foram preenchidos corretamente, retorna true. Caso contrário, false.</returns>
function validaTextoAssunto(objNotif) {
    var bRet = true;
    //------------------------------------------------------------------------------------------------------------

    if (true == bRet) {
        if (("" == objNotif.textoNotificacao1) && ("" != objNotif.assuntoNotificacao1) ||
            ("" != objNotif.textoNotificacao1) && ("" == objNotif.assuntoNotificacao1))
        //--------------------------------------------------------------------------------------------
        {
            window.top.mostraMensagem(traducao.WorkflowCharts_para_configurar_um_envio_de_uma__mensagem_a__o___os_dois_campos_s_o_obrigrat_rios___assunto__e__texto__, 'atencao', true, false, null);
            bRet = false;
        } // if ((null == mmTexto1Conector) || ...
    }

    if (true == bRet) {
        if (("" == objNotif.textoNotificacao2) && ("" != objNotif.assuntoNotificacao2) ||
            ("" != objNotif.textoNotificacao2) && ("" == objNotif.assuntoNotificacao2))
        //--------------------------------------------------------------------------------------------
        {
            window.top.mostraMensagem(traducao.WorkflowCharts_para_configurar_um_envio_de_uma__mensagem_acompanhamento___os_dois_campos_s_o_obrigrat_rios___assunto__e__texto__, 'atencao', true, false, null);
            bRet = false;
        } // if ((null == mmTexto2Conector) || ...
    }
    // zera as variáveis, caso tenha havido algum problema na validação.
    if (false == bRet) {
        objNotif.textoNotificacao1 = "";
        objNotif.assuntoNotificacao1 = "";
        objNotif.textoNotificacao2 = "";
        objNotif.assuntoNotificacao2 = "";
    }
    return bRet;
}



/// <summary>
/// função para processar o preenchimento da DivConector
/// </summary>
/// <remarks>
/// Esta função assume que as informações entradas na divConector já estão todas VALIDADAS.
/// </remarks>
/// <returns>void</returns>
function processaPreenchimentoDivConector() {
    // remove conector anterior, caso se trata de uma edição
    if ("" != cmbEtapaOrigem_cnt.cpOriginalFrom) {
        var auxConn = new xConnector();

        auxConn.from = cmbEtapaOrigem_cnt.cpOriginalFrom;
        auxConn.to = cmbEtapaDestino_cnt.cpOriginalTo;
        auxConn.acao = edtAcao_cnt.cpOriginalAction;

        removeConnectorFromXml(auxConn);
    }

    if (gv_Acoes_cnt.GetVisibleRowsOnPage() > 0)
        __wf_connectorObj.temAcoesAutomaticas = true;
    else
        __wf_connectorObj.temAcoesAutomaticas = false;

    __wf_connectorObj.solicitaAssinaturaDigital = cbSolicitarAssinaturaDigital.GetChecked();


    if (false == addConnectorInXml(__wf_connectorObj, true)) {
        bRet = false;
        window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_do_conector_ + " (0005)", 'erro', true, false, null);
    }
    else {

        // solicita inicialmente as informações sobre a grid de Grupos notificados. 
        gv_GruposNotificados_cnt.GetPageRowValues("CodigoPerfilWf;NomePerfilWf;Mensagem",
            function recebeCamposGridGruposDivConector(valores) {

                var i, j;

                // remove os elementos antigos do objeto
                if (0 < __wf_connectorObj.notificationGroups.length)
                    __wf_connectorObj.notificationGroups.splice(0, __wf_connectorObj.notificationGroups.length);

                for (i = 0; i < valores.length; i++)
                    __wf_connectorObj.notificationGroups.push(valores[i]);

                atualizaXmlComGruposDaDivConector();

                // solicita informações sobre a grid de ações automáticas. A função recebeCamposGridAcoesDivConector 
                // irá gravar as informações no xml assim que as receber.
                gv_Acoes_cnt.GetPageRowValues("Nome;CodigoAcaoAutomaticaWf", /// <summary>
                    function recebeCamposGridAcoesDivConector(valores) {
                        var i, j;
                        // remove os elementos antigos do objeto
                        if (0 < __wf_connectorObj.automaticActions.length)
                            __wf_connectorObj.automaticActions.splice(0, __wf_connectorObj.automaticActions.length);

                        for (i = 0; i < valores.length; i++)
                            __wf_connectorObj.automaticActions.push(valores[i]);

                        atualizaXmlComAutomaticActionsDaDivConector(__wf_connectorObj);
                        gv_Acionamentos.GetPageRowValues("Id;webServiceURL;enabled;activationSequence", function recebeCamposGridAcionamentosDivConector(valores) {
                            var i, j;
                            // remove os elementos antigos do objeto
                            if (0 < __wf_connectorObj.acionamentosAPI.length)
                                __wf_connectorObj.acionamentosAPI.splice(0, __wf_connectorObj.acionamentosAPI.length);

                            for (i = 0; i < valores.length; i++)
                                __wf_connectorObj.acionamentosAPI.push(valores[i]);

                            gv_ParametrosAcionamentos.GetPageRowValues("Id;parameter;dataType;httpPart;sendNull;value", function recebeCamposGridParametrosAcionamentos(valores) {
                                var i, j;
                                // remove os elementos antigos do objeto
                                if (0 < __wf_connectorObj.parametrosAcionamentosAPI.length)
                                    __wf_connectorObj.parametrosAcionamentosAPI.splice(0, __wf_connectorObj.parametrosAcionamentosAPI.length);

                                for (i = 0; i < valores.length; i++)
                                    __wf_connectorObj.parametrosAcionamentosAPI.push(valores[i]);
                                atualizaXmlComAcionamentosDaDivConector(__wf_connectorObj);
                                divConector.Hide();
                                lpLoading.Hide();
                            });
                        });
                    });


            });

    }
    return;
}


/// <summary>
/// função para excluir as linhas de uma grid
/// </summary>
/// <param name="div" type="ASPxClientGridView">Objeto grid cujas linhas serão excluídas</param>
/// <returns>void</returns>
function deleteGridRows(grid) {
    var count = grid.GetVisibleRowsOnPage();

    for (var i = 0; i < count; i++)
        grid.DeleteRow(0);
}


/// <summary>
/// função para verificar se já existe no XML um conector cadastrado ligando as etapas em questão.
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </summary>
/// <param name="connector" type="objeto">Objeto conector a ser verificad.</param>
/// <returns type="boolean">Se foi verificada a existência da conector no XML, retorna true. 
///  Caso o conector não conste no XML ou não tenha sido possível verificar a existência, retorna false.
/// </returns>
function conexaoExistente(connector) {
    var bRet = false;
    auxConector = conectorExistente(connector, true);

    if (null != auxConector)
        bRet = true

    return bRet;
}

/// <summary>
/// função para verificar se já existe no XML a ação que está sendo cadastrada.
/// verifica se para o id passado como parâemtro já existe um conector de nome igual ao parâmetro nomeAcao.
/// </summary>
/// <param name="elementId" type="string">ID do elemento gráfico para o qual se deseja verificar se existe ação cadastrada.</param>
/// <param name="nomeAcao" type="string">Nome da ação a verificar.</param>
/// <returns type="boolean">Se foi verificada a existência de uma ação já cadastrada, retorna true. 
///  Caso não exista a ação ou não tenha sido possível verificar a existência, retorna false.
/// </returns>
function acaoExistente(elementId, nomeAcao) {
    var to, from;
    var setaInicio, setaFim;
    var label;

    var bExiste = false;

    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        from = __wf_nlConectores[i].getAttribute("from");
        to = __wf_nlConectores[i].getAttribute("to");

        if ((null != from) && (null != to) && ((from == elementId) || (to == elementId))) {
            setaInicio = __wf_nlConectores[i].getAttribute("arrowAtStart");
            if (null == setaInicio)
                setaInicio = "0";

            setaFim = __wf_nlConectores[i].getAttribute("arrowAtEnd");
            if (null == setaFim)
                setaFim = "0";

            label = null;
            if ((from == elementId) && ("1" == setaFim))
                label = __wf_nlConectores[i].getAttribute("label");
            else if ((to == elementId) && ("1" == setaInicio))
                label = __wf_nlConectores[i].getAttribute("label");

            if ((null != label) && ("1" == setaInicio) && ("1" == setaFim))
                label = decompoeLabelAcao(elementId, from, to, label);

            if ((null != label) && (label.toLowerCase() == nomeAcao.toLowerCase())) {
                bExiste = true;
                break;
            } // if ( (null != label) && ...
        } // if ( (null != from) && ...
    } // for (var i=0; i<__wf_nlConectores; i++)

    return bExiste;
}

/// <summary>
/// função para decompor o label de um conector quando este label refere-se a uma conexão de via dupla.
/// se o elemento em questão é o de origem (id == from), retorna a primeira parte do label, em caso contrário,
/// retorna a segunda parte. 
/// </summary>
/// <param name="id" type="string">id do elemento para o qual está se analisando o label da conexão.</param>
/// <param name="from" type="string">id do elemento 'from' da conexão.</param>
/// <param name="to" type="string">id do elemento 'to' da conexão.</param>
/// <returns type="string">retorna o label decomposto.</returns>
function decompoeLabelAcao(id, from, to, label) {
    if ((null == id) || (null == from) || (null == to) || (null == label))
        return label;

    var newLabel;
    var posBar;

    posBar = label.indexOf("/");

    if (id == from)
        newLabel = label.substring(0, posBar);
    else if (id == to)
        newLabel = label.substring(posBar + 1);

    return newLabel;
}


/// <summary>
/// Inclui um novo conector no XML que será enviado para o gráfico.
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </summary>
/// <param name="connector" type="objeto">Objeto conector a ser inserido no XML.</param>
/// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
function addConnectorInXml(connector, atualizaXml) {
    atualizaXmlConexaoElementos(connector.from, connector.to);
    insertConnector(connector);

    if (atualizaXml) {
        atualizaXmlDoGrafico(__wf_xmlDocWorkflow);
    }
    return true;
}


/// <summary>
/// Exclui um conector do XML, atualizando todas as informações do XML.
/// </summary>
/// <remarks>
/// Exclui um conector do XML e atualizando as seguintes informações:
/// 1. Atributos do conector no XML dentro do nó <connectors>
///    ora exclui um elemento desse nó, ora atualiza suas informações
/// 2. Retira as informações das ações no XML caso o conector tenha gerado ação na etapa;
/// 3. Atualiza as informações de criação de fluxo/subfluxo no nó <workflows>
/// 
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="connector" type="objeto">Objeto conector a ser excluído do XML.</param>
/// <returns type="boolean">Se deu certo a exclusão do conector do xml, retorna true. Caso contrário, false.</returns>
function removeConnectorFromXml(connector) {
    retiraActionNodeOfConnector(connector);
    atualizaXmlDesconexaoElementos(connector.from, connector.to);
    deleteConnector(connector);
    atualizaXmlDoGrafico(__wf_xmlDocWorkflow);
    return true;
}


/// <summary>
/// Retira do XML as informações sobre a ação a que se refere um conector.
/// </summary>
/// <remarks>
/// Localiza o 'nó' da etapa em que estão as informações sobre o conector e retira desse 
/// 'nó' as informações sobre a ação referente ao conector em questão.
/// esta função é usada ao se retirar um conector do desenho do workflow.
/// 
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="connector" type="objeto">Objeto conector 'sendo retirado' do XML.</param>
function retiraActionNodeOfConnector(connector) {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null, acoes = null, acao = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");

    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (null != sets)
        ne = getChildNodeById(sets, connector.from);

    if (null != ne)
        acoes = getChildNodeByName(ne.childNodes, "acoes");

    if (null != acoes)
        acao = getChildNodeById(acoes.childNodes, connector.acao);

    if (null != acao)
        acoes.removeChild(acao);
}



/// <summary>
/// Atualiza o XML em função da criação de uma conexão entre os elementos
/// </summary>
/// <remarks>
/// Atualiza o XML com as seguintes ações:
/// 1. Registra atributos específicos das etapas que estão sendo conectadas (etapaInicial, grupoWorkflow e idElementoInicioFluxo)
/// 2. Adiciona "elementos" no "nó" <workflows> do XML, caso a conexão esteja saindo de um elemento gráfico do tipo 'início' ou 'disjunção'
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="from" type="string">ID do objeto de origem da conexão.</param>
/// <param name="to" type="string">ID do objeto de destino da conexão.</param>
/// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
function atualizaXmlConexaoElementos(from, to) {
    var tipoElementoOrigem, tipoElementoDestino; // tipos dos elementos de origem e destino 
    var neOrigem, neDestino;  // "nós" XML's referentes aos elementos de origem e destino
    var grupoWf; // grupo de Workflow
    var idElemInicioFluxo; // id do elemento que inicia o fluxo do objeto sendo conectado
    var indEtapaInicial;

    //Teste Alejandro 5/06/2010
    if ("Cancelamento" == from) from = "0";

    neOrigem = getChildNodeById(__wf_nlElementos, from);
    neDestino = getChildNodeById(__wf_nlElementos, to);

    tipoElementoOrigem = obtemTipoElemento(neOrigem);
    tipoElementoDestino = obtemTipoElemento(neDestino);

    grupoWf = neOrigem.getAttribute("grupoWorkflow");

    // se a origem for uma junção (fechamento de subworkflow
    if (__wf_cTipoElementoJuncao == tipoElementoOrigem) {
        // pega a disjunção que iniciou o fluxo (atributo idElementoInicioFluxo do objeto 'from')
        idElemInicioFluxo = neOrigem.getAttribute("idElementoInicioFluxo");
        var neDisjuncao = getChildNodeById(__wf_nlElementos, idElemInicioFluxo);
        indEtapaInicial = '0';
        if (null == neDisjuncao) {
            idElemInicioFluxo = '';
            grupoWf = '';
        }
        else {   // o id do elemento início fluxo do elemento 'to' será o mesmo id de elemento de início de fluxo da disjunção que iniciou o fluxo da junção
            idElemInicioFluxo = neDisjuncao.getAttribute("idElementoInicioFluxo");
            grupoWf = neDisjuncao.getAttribute("grupoWorkflow");
        }
    }
    // se a origem for um 'Início' ou uma 'Disjunção'    
    else if ((__wf_cTipoElementoInicio == tipoElementoOrigem) || (__wf_cTipoElementoDisjuncao == tipoElementoOrigem)) {
        idElemInicioFluxo = from;
        grupoWf = addWfGroupIdInXml(to, from, grupoWf);
        if ((__wf_cTipoElementoInicio == tipoElementoOrigem)) {
            indEtapaInicial = '1';
        }
        else if ((__wf_cTipoElementoDisjuncao == tipoElementoOrigem)) {
            indEtapaInicial = '2';
        }
    }
    else {
        idElemInicioFluxo = neOrigem.getAttribute("idElementoInicioFluxo");
        indEtapaInicial = '0';
    }

    neDestino.setAttribute("etapaInicial", indEtapaInicial);
    neDestino.setAttribute("grupoWorkflow", grupoWf);
    neDestino.setAttribute("idElementoInicioFluxo", idElemInicioFluxo);
}

/// <summary>
/// Atualiza o XML em função da eliminação de uma conexão entre elementos
/// </summary>
/// <remarks>
/// verifica se a ligação desfeita era a ligação que dava início a um fluxo/subfluxo, e, em caso positivo, 
/// retira o "nó" <workflows> do XML referente à ligação em questão
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="from" type="string">ID do objeto de origem da conexão.</param>
/// <param name="to" type="string">ID do objeto de destino da conexão.</param>
/// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
function atualizaXmlDesconexaoElementos(from, to) {
    // por se tratar de uma primeira implementação, chama apenas a função que retira o "nó" do XML, uma vez
    // só existirá nó para ser retirado caso os dois elementos envolvidos davam início a um fluxo/subfluxo.
    removeWfGroupIdInXml(to, from);
}

/// <summary>
/// Adiciona, se necessários, um novo grupo de workflow no XML.
/// retorna o ID do grupo adicionado ou encontrado
/// </summary>
/// <remarks>
/// Antes de adicionar o grupo no xml, a função verifica se já não existe um fluxo para as informações
/// em questão: idEtapaInicialSubWorkflow, etapaIniciadora, workflowPai
/// Assume que a variável global '__wf_nlElementos' contém os "nós" XML's referentes aos elementos do XML do gráfico.
/// </remarks>
/// <param name="idEtapaInicialSubWorkflow" type="string">Id da primeira etapa no SubWorkflow.</param>
/// <param name="etapaIniciadora" type="string">Id da etapa do workflow pai que inicia o subworkflow.</param>
/// <param name="workflowPai" type="string">id do workflow/subworkflow que será o pai do subworkflow sendo incluído.</param>
/// <returns type="string">retorna o id do subworkflow incluído no xml</returns>
function addWfGroupIdInXml(idEtapaInicialSubWorkflow, etapaIniciadora, workflowPai) {
    var nextWfGroupId;

    nextWfGroupId = getWfGroupIdInXml(idEtapaInicialSubWorkflow, etapaIniciadora, workflowPai);

    if (null == nextWfGroupId) {
        nextWfGroupId = getNextWfGroupId(__wf_nlElementos); // obtém o próximo id de grupo de workflow

        var ds = __wf_xmlDocWorkflow.getElementsByTagName("workflows");
        var ne = __wf_xmlDocWorkflow.createElement("subworkflow");

        ne.setAttribute("id", nextWfGroupId);
        ne.setAttribute("etapaInicialSubWorkflow", idEtapaInicialSubWorkflow);
        ne.setAttribute("workflowPai", workflowPai);
        ne.setAttribute("etapaIniciadora", etapaIniciadora);
        ds[0].appendChild(ne);
    }

    return nextWfGroupId;
}


/// <summary>
/// Remove o grupo de workflow no XML. 
/// </summary>
/// <remarks>
/// Esta função é usada na exclusão de uma conexão, quando a origem desta conexão é um elemento que inicia 
/// um fluxo/subfluxo (elemento início ou disjunção)
/// </remarks>
/// <param name="idEtapaInicialSubWorkflow" type="string">Id da primeira etapa no SubWorkflow.</param>
/// <param name="etapaIniciadora" type="string">Id da etapa do workflow pai que inicia o subworkflow.</param>
/// <returns type="void"></returns>
function removeWfGroupIdInXml(idEtapaInicialSubWorkflow, etapaIniciadora) {
    var id = null;
    var ds = __wf_xmlDocWorkflow.getElementsByTagName("workflows");
    var nlChildren = ds[0].childNodes;
    var idEtapaInicial, idEtapaIniciadora;

    if (null != nlChildren) {
        for (var j = 0; j < nlChildren.length; j++) {
            if (nlChildren[j].nodeType != 1)
                continue;

            idEtapaInicial = nlChildren[j].getAttribute("etapaInicialSubWorkflow");
            idEtapaIniciadora = nlChildren[j].getAttribute("etapaIniciadora");

            if ((null != idEtapaInicial) && (null != idEtapaIniciadora)) {
                if ((idEtapaInicialSubWorkflow == idEtapaInicial) &&
                    (etapaIniciadora == idEtapaIniciadora)) {
                    ds[0].removeChild(nlChildren[j]);
                    break;
                }
            }
        } // for j < nlChildren.length ...
    } // if (null != nlChildren)

    return
}


/// <summary>
/// retorna o id de um grupo de workflow que se tiver as informações passadas como parâmetro
/// (idEtapaInicialSubWorkflow, etapaIniciadora, workflowPai)
/// </summary>
/// <remarks>
/// Antes de adicionar o grupo no xml, a função verifica se já não existe um fluxo para as informações
/// em questão: idEtapaInicialSubWorkflow, etapaIniciadora, workflowPai
/// Assume que a variável global '__wf_nlElementos' contém os "nós" XML's referentes aos elementos do XML do gráfico.
/// </remarks>
/// <param name="idEtapaInicialSubWorkflow" type="string">Id da primeira etapa no SubWorkflow.</param>
/// <param name="etapaIniciadora" type="string">Id da etapa do workflow pai que inicia o subworkflow.</param>
/// <param name="workflowPai" type="string">id do workflow/subworkflow que será o pai do subworkflow sendo incluído.</param>
/// <returns type="string">retorna o id do subworkflow incluído no xml</returns>
function getWfGroupIdInXml(idEtapaInicialSubWorkflow, etapaIniciadora, workflowPai) {
    var id = null;
    var ds = __wf_xmlDocWorkflow.getElementsByTagName("workflows");
    var nlChildren = ds[0].childNodes;
    var idEtapaInicial, idEtapaIniciadora, idWorkflowPai;

    if (null != nlChildren) {
        for (var j = 0; j < nlChildren.length; j++) {
            if (nlChildren[j].nodeType != 1)
                continue;

            idEtapaInicial = nlChildren[j].getAttribute("etapaInicialSubWorkflow");
            idWorkflowPai = nlChildren[j].getAttribute("workflowPai");
            idEtapaIniciadora = nlChildren[j].getAttribute("etapaIniciadora");

            if ((null != idEtapaInicial) && (null != idEtapaIniciadora) && (null != idWorkflowPai)) {
                if ((idEtapaInicialSubWorkflow == idEtapaInicial) &&
                    (etapaIniciadora == idEtapaIniciadora) && (workflowPai == idWorkflowPai)) {
                    id = nlChildren[j].getAttribute("id");
                    break;
                }
            }
        } // for j < nlChildren.length ...
    } // if (null != nlChildren)

    return id;
}

/// <summary>
/// Devolve o próximo ID do próximo grupo de workflow a ser incluído no xml
/// </summary>
/// <param name="nodes" type="XML Nodes">Coleção, no formato XML, que contém os nós a serem varridos para 
/// identificação do próximo ID que deve ser usado.</param>
/// <returns type="string">ID do próximo grupo de workflow.</returns>
function getNextWfGroupId(nodes) {
    var lastId = 0;
    var id;

    for (i = 0; i < nodes.length; i++) {
        if (nodes[i].nodeType != 1)
            continue;

        id = parseInt(nodes[i].getAttribute("grupoWorkflow"));

        if ((null != id) && (id > lastId))
            lastId = id
    }

    lastId++;
    return lastId.toString();
}


function insertConnector(connector) {
    /// <summary>
    /// Insere no XML um conector. 
    /// Assume que a variável global '__wf_nlConectores' contém os "nós" XML's referentes aos conectores do XML do gráfico.
    /// </summary>
    /// <param name="connector" type="objeto">Objeto conector a ser inserido no XML.</param>
    /// <returns type="void">void</returns>

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("connectors");
    var auxConector = conectorExistente(connector, false);

    // se já existir um conector ligando as duas etapas, atualiza os dados do conector
    if (null != auxConector)
        updateConnectorDataDueToInsert(auxConector, connector);
    else {
        var ne = __wf_xmlDocWorkflow.createElement("connector");
        ne.setAttribute("from", connector.from);
        ne.setAttribute("to", connector.to);
        ne.setAttribute("label", connector.acao);
        ne.setAttribute("arrowAtStart", "0");
        ne.setAttribute("arrowAtEnd", "1");
        ne.setAttribute("color", "33C1FE");
        ne.setAttribute("strength", "0.5");
        ne.setAttribute("alpha", "100");

        if (connector.temAcoesAutomaticas == true)
            ne.setAttribute("dashed", "1");
        else
            ne.setAttribute("dashed", "0");

        ds[0].appendChild(ne);
    } // else
}

/// <summary>
/// agapa um conector do XML 
/// </summary>
/// <param name="connector" type="objeto">Objeto conector a ser inserido no XML.</param>
/// <returns type="void">void</returns>
function deleteConnector(connector) {
    var auxConector = conectorExistente(connector, false);

    // se existir um conector ligando os dois elementos gráficos indicados no objeto connector
    if (null != auxConector) {
        updateConnectorDataDueToDelete(auxConector, connector);
    }
}

function conectorExistente(connector, consideraSentido) {
    /// <summary>
    /// Verifica no XML se já existe um conetor entre as etapas from e to do conector passado como parâmetro. 
    /// Se consideraSentido for indicado, a função só retorna o conector caso o sentido seja o mesmo indicado na
    /// variável parâmetro.
    /// .</summary>
    /// <param name="connector" type="objeto">Objeto conector a ser verificado.</param>
    /// <param name="consideraSentido" type="boolean">Valor que indicará se o sentido deve ser ou não considerado.</param>
    /// <returns type="elementNode XML">o nó do elemento conector encontrado, ou null caso não seja encontrado.</returns>

    var idFrom, idTo;
    var retConector;
    var setaInicio, setaFim;

    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        idFrom = __wf_nlConectores[i].getAttribute("from");
        idTo = __wf_nlConectores[i].getAttribute("to");
        if ((null != idFrom) && (null != idTo)) {
            if ((idFrom == connector.from) && (idTo == connector.to)) {
                if (true == consideraSentido) {
                    setaFim = __wf_nlConectores[i].getAttribute("arrowAtEnd");
                    if ((null != setaFim) & ("1" == setaFim))
                        retConector = __wf_nlConectores[i];
                } // if
                else
                    retConector = __wf_nlConectores[i];

                // parando o for por que no XML só trata a 1ª linha para uma conexão entre dos elementos 
                break;
            } // if ( (idFrom == idEtapaOrigem ...
            else if ((idTo == connector.from) && (idFrom == connector.to)) {
                if (true == consideraSentido) {
                    setaInicio = __wf_nlConectores[i].getAttribute("arrowAtStart");
                    if ((null != setaInicio) & ("1" == setaInicio))
                        retConector = __wf_nlConectores[i];
                } // if
                else
                    retConector = __wf_nlConectores[i];
                // parando o for por que no XML só trata a 1ª linha para uma conexão entre dos elementos     
                break;
            } // else if ( (idTo == connector.from ...
        } // if ( (null != idFrom ...
    } // for

    return retConector;
}

/// <summary>
/// função para atualizar o XML de um conector em função da exclusão de uma conexão.
/// </summary>
/// <param name="toUpdateConnector" type="DOM XML Node">Nó do XML referente ao conector a ser atualizado.</param>
/// <param name="objConnector" type="objeto">Objeto conector a ser verificado.</param>
/// <returns type="elementNode XML">o nó do elemento conector encontrado, ou null caso não seja encontrado.</returns>
function updateConnectorDataDueToDelete(toUpdateConnector, objConnector) {
    var idFrom = toUpdateConnector.getAttribute("from");
    var idTo = toUpdateConnector.getAttribute("to");
    var label, setaInicio, setaFim;
    var pos = -1;
    var newLabel = null;

    if ((null != idFrom) && (null != idTo)) {
        label = toUpdateConnector.getAttribute("label");
        if (null != label)
            pos = label.indexOf("/");

        if ((idFrom == objConnector.from) && (idTo == objConnector.to)) {
            toUpdateConnector.setAttribute("arrowAtEnd", "0");

            if ((pos > -1) && (label.length > pos + 1)) {
                newLabel = label.substr(pos + 1);
            }
        }
        else if ((idTo == objConnector.from) && (idFrom == objConnector.to)) {
            toUpdateConnector.setAttribute("arrowAtStart", "0");

            if (pos > -1) {
                newLabel = label.substring(0, pos);
            }
        } // else if ( (idTo == connector.from ...

        setaInicio = toUpdateConnector.getAttribute("arrowAtStart");
        setaFim = toUpdateConnector.getAttribute("arrowAtEnd");

        if (("0" == setaInicio) && ("0" == setaFim)) {
            toUpdateConnector.parentNode.removeChild(toUpdateConnector);
        }
        else if (null != newLabel)
            toUpdateConnector.setAttribute("label", newLabel);

    } // if ( (null != idFrom ...
}


/// <summary>
/// função para atualizar o XML de um conector. Esta função é usada quando já existe uma conexão entre duas etapas
/// e o usuário cria uma segunda conexão para as mesmas duas etapas no sentido inverso ao existente.
/// .</summary>
/// <param name="toUpdateConnector" type="DOM XML Node">Nó do XML referente ao conector a ser atualizado.</param>
/// <param name="objConnector" type="objeto">Objeto conector a ser verificado.</param>
/// <returns>void</returns>
function updateConnectorDataDueToInsert(toUpdateConnector, objConnector) {
    var idFrom = toUpdateConnector.getAttribute("from");
    var idTo = toUpdateConnector.getAttribute("to");
    var label1 = "";
    var label2 = "";
    var conector = "";

    if ((null != idFrom) && (null != idTo)) {
        if ((idFrom == objConnector.from) && (idTo == objConnector.to)) {
            toUpdateConnector.setAttribute("arrowAtEnd", "1");

            label2 = toUpdateConnector.getAttribute("label");
            label1 = objConnector.acao;
        } // if ( (idFrom == objConnector.from) 

        if ((idTo == objConnector.from) && (idFrom == objConnector.to)) {
            toUpdateConnector.setAttribute("arrowAtStart", "1");

            label1 = toUpdateConnector.getAttribute("label");
            label2 = objConnector.acao;
        } // else if ( (idTo == connector.from ...

        if (null == label1)
            label1 = "";

        if (null == label2)
            label2 = "";

        if (("" == label1) & ("" == label2))
            conector = "";
        else
            conector = "/";

        label = label1 + conector + label2;

        toUpdateConnector.setAttribute("label", label);

    } // if ( (null != idFrom ...
}

/// <summary>
/// Solicita, ao servidor, as informações das grids da div conector, incluindo cada uma no xml.
/// </summary>
/// <returns type="void">void</returns>
function processaInformacoesGridsDivConector() {
    var campos = "CodigoPerfilWf;NomePerfilWf;Mensagem";

    // solicita inicialmente as informações sobre a grid de formulários. 
    gv_GruposNotificados_cnt.GetPageRowValues(campos, recebeCamposGridGruposDivConector);
}






/// <summary>
/// Função para incluir no XML dados da grid 'Grupos Notificados' da divConector 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. 
/// Assume que o objeto __wf_activObj contém os dados a serem incluídos e a objeto __wf_xmlDocWorkflow
/// contém o xml com as informações. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de 'grupos notificados' da div Conector
/// </param>
/// <returns type="void">void</returns>
function atualizaXmlComGruposDaDivConector() {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");

    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (null != sets)
        ne = getChildNodeById(sets, __wf_connectorObj.from);

    if (null != ne) {
        var acoes = getChildNodeByName(ne.childNodes, "acoes");
        if (null == acoes)
            acoes = __wf_xmlDocWorkflow.createElement("acoes");

        var acao = obtemXmlAcaoConectorAtualizado(acoes, __wf_connectorObj);

        var groups = getChildNodeByName(acao.childNodes, "gruposNotificados");
        if (null != groups)
            acao.removeChild(groups);

        groups = __wf_xmlDocWorkflow.createElement("gruposNotificados");

        var group;
        for (var i = 0; i < __wf_connectorObj.notificationGroups.length; i++) {
            group = __wf_xmlDocWorkflow.createElement("grupo");
            group.setAttribute("id", __wf_connectorObj.notificationGroups[i][__wf_cNumColDivCntGroups_id]);
            group.setAttribute("name", __wf_connectorObj.notificationGroups[i][__wf_cNumColDivCntGroups_name]);
            group.setAttribute("msgBox", __wf_connectorObj.notificationGroups[i][__wf_cNumColDivCntGroups_msgBox]);
            groups.appendChild(group);
        }
        acao.appendChild(groups);
        acoes.appendChild(acao);
        ne.appendChild(acoes);
        atualizaHiddenField(__wf_xmlDocWorkflow);
    }
}


/// <summary>
/// Função para devolver um "nó" XML contendo as informações sobre a ação especificada pelo objeto xConnector 
/// passado como parâmetro.
/// </summary>
/// <remarks>
/// Esta função devolve um "nó" XML contendo as informações sobre a ação. Atenção!!! Esta função não atualiza o XML, 
/// apenas devolve o nó. 
/// </remarks>
/// <param name="acoes" type="DOM XML">Objeto DOM no qual serão incluídas as informações.</param>
/// <param name="connector" type="objeto">Objeto do tipo xConnector contendo as informações</param>
/// <returns type="DOM XML">Retorna a ação void</returns>
function obtemXmlAcaoConectorAtualizado(acoes, connector) {
    var tipoElementoOrigem, tipoElementoDestino, nextStageId, actionType;

    var acao = getChildNodeById(acoes.childNodes, connector.acao);
    if (null == acao)
        acao = __wf_xmlDocWorkflow.createElement("acao");

    tipoElementoOrigem = obtemTipoElemento(getChildNodeById(__wf_nlElementos, connector.from));
    tipoElementoDestino = obtemTipoElemento(getChildNodeById(__wf_nlElementos, connector.to));

    if ((__wf_cTipoElementoEtapa == tipoElementoDestino) || (__wf_cTipoElementoDisjuncao == tipoElementoDestino) || (__wf_cTipoElementoCondicao == tipoElementoDestino))
        nextStageId = connector.to;
    else
        nextStageId = "";

    if (__wf_cTipoElementoCondicao == tipoElementoOrigem)
        actionType = "D";
    else
        actionType = "U";


    acao.setAttribute("id", connector.acao);
    acao.setAttribute("nextStageId", nextStageId);
    acao.setAttribute("to", connector.to);
    acao.setAttribute("actionType", actionType);
    acao.setAttribute("corBotao", connector.corBotao);
    acao.setAttribute("iconeBotao", connector.iconeBotao);
    acao.setAttribute("decodedCondition", connector.decodedCondition);
    acao.setAttribute("condition", connector.condition);
    acao.setAttribute("conditionIndex", connector.conditionIndex);

    if (connector.solicitaAssinaturaDigital == true)
        acao.setAttribute("solicitaAssinaturaDigital", "1");
    else
        acao.setAttribute("solicitaAssinaturaDigital", "0");

    //Assunto da notificação1
    var assunto = getChildNodeByName(acao.childNodes, "assuntoNotificacao");
    var conteudo;

    if (null != assunto)
        acao.removeChild(assunto);
    assunto = __wf_xmlDocWorkflow.createElement("assuntoNotificacao");

    if ((null != connector.assuntoNotificacao1) && ("" != connector.assuntoNotificacao1))
        conteudo = __wf_xmlDocWorkflow.createTextNode(connector.assuntoNotificacao1);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    assunto.appendChild(conteudo);
    acao.appendChild(assunto);

    //Texto da notificação1
    var texto = getChildNodeByName(acao.childNodes, "textoNotificacao");
    if (null != texto)
        acao.removeChild(texto);
    texto = __wf_xmlDocWorkflow.createElement("textoNotificacao");

    if ((null != connector.textoNotificacao1) && ("" !== connector.textoNotificacao1))
        conteudo = __wf_xmlDocWorkflow.createTextNode(connector.textoNotificacao1);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    texto.appendChild(conteudo);
    acao.appendChild(texto);

    //Assunto da notificação2
    assunto = getChildNodeByName(acao.childNodes, "assuntoNotificacao2");

    if (null != assunto)
        acao.removeChild(assunto);
    assunto = __wf_xmlDocWorkflow.createElement("assuntoNotificacao2");

    if ((null != connector.assuntoNotificacao2) && ("" != connector.assuntoNotificacao2))
        conteudo = __wf_xmlDocWorkflow.createTextNode(connector.assuntoNotificacao2);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    assunto.appendChild(conteudo);
    acao.appendChild(assunto);

    //Texto da notificação2
    texto = getChildNodeByName(acao.childNodes, "textoNotificacao2");
    if (null != texto)
        acao.removeChild(texto);
    texto = __wf_xmlDocWorkflow.createElement("textoNotificacao2");

    if ((null != connector.textoNotificacao2) && ("" !== connector.textoNotificacao2))
        conteudo = __wf_xmlDocWorkflow.createTextNode(connector.textoNotificacao2);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    texto.appendChild(conteudo);
    acao.appendChild(texto);

    return acao;
}

/// <summary>
/// Função para incluir no XML dados das ações automáticas da grid 'Ações' da divConector 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. 
/// Assume que objeto __wf_xmlDocWorkflow contém o xml com as informações. 
/// </remarks>
/// <param name="connector" type="object">Objeto contendo as informações sobre o connector 
/// da grid de 'Ações' da div Conector
/// </param>
/// <returns type="void">void</returns>
function atualizaXmlComAcionamentosDaDivConector(connector) {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;
    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");
    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (sets != null)
        ne = getChildNodeById(sets, connector.from);

    if ((null != ne) && (0 < connector.acionamentosAPI.length)) {
        var acoes = getChildNodeByName(ne.childNodes, "acoes");
        if (null == acoes)
            acoes = __wf_xmlDocWorkflow.createElement("acoes");

        var acao = obtemXmlAcaoConectorAtualizado(acoes, connector);

        var acionamentosAPIs = getChildNodeByName(acao.childNodes, "acionamentosAPIs");
        if (null != acionamentosAPIs)
            acao.removeChild(acionamentosAPIs);

        acionamentosAPIs = __wf_xmlDocWorkflow.createElement("acionamentosAPIs");
        var acionamentoAPI;

        for (var i = 0; i < connector.acionamentosAPI.length; i++) {
            acionamentoAPI = __wf_xmlDocWorkflow.createElement("acionamentoAPI");
            acionamentoAPI.setAttribute("Id", connector.acionamentosAPI[i][0]);
            acionamentoAPI.setAttribute("webServiceURL", connector.acionamentosAPI[i][1]);
            acionamentoAPI.setAttribute("enabled", connector.acionamentosAPI[i][2]);
            acionamentoAPI.setAttribute("activationSequence", connector.acionamentosAPI[i][3]);

            var parametrosAPI = __wf_xmlDocWorkflow.createElement("parametrosAPI");
            var parametroAPI;
            var existemParametros = false;
            //Id;parameter;dataType;httpPart;sendNull;value
            for (var j = 0; j < connector.parametrosAcionamentosAPI.length; j++) {
                if (connector.parametrosAcionamentosAPI[j][0] == connector.acionamentosAPI[i][0]) {
                    existemParametros = true;
                    parametroAPI = __wf_xmlDocWorkflow.createElement("parametroAPI");
                    parametroAPI.setAttribute("parameter", connector.parametrosAcionamentosAPI[j][1]);
                    parametroAPI.setAttribute("dataType", connector.parametrosAcionamentosAPI[j][2]);
                    parametroAPI.setAttribute("httpPart", connector.parametrosAcionamentosAPI[j][3]);
                    parametroAPI.setAttribute("sendNull", connector.parametrosAcionamentosAPI[j][4]);
                    parametroAPI.setAttribute("value", connector.parametrosAcionamentosAPI[j][5]);
                    parametrosAPI.appendChild(parametroAPI);
                }
            }

            if (existemParametros) {
                acionamentoAPI.appendChild(parametrosAPI);
            }

            acionamentosAPIs.appendChild(acionamentoAPI);
        }
        acao.appendChild(acionamentosAPIs);

        acoes.appendChild(acao);
        ne.appendChild(acoes);
        atualizaHiddenField(__wf_xmlDocWorkflow);
    }


}
/// <summary>
/// Função para incluir no XML dados das ações automáticas da grid 'Ações' da divConector 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. 
/// Assume que objeto __wf_xmlDocWorkflow contém o xml com as informações. 
/// </remarks>
/// <param name="connector" type="object">Objeto contendo as informações sobre o connector 
/// da grid de 'Ações' da div Conector
/// </param>
/// <returns type="void">void</returns>
function atualizaXmlComAutomaticActionsDaDivConector(connector) {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");
    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (sets != null)
        ne = getChildNodeById(sets, connector.from);

    if ((null != ne) && (0 < connector.automaticActions.length)) {
        var acoes = getChildNodeByName(ne.childNodes, "acoes");
        if (null == acoes)
            acoes = __wf_xmlDocWorkflow.createElement("acoes");

        var acao = obtemXmlAcaoConectorAtualizado(acoes, connector);

        var actions = getChildNodeByName(acao.childNodes, "acoesAutomaticas");
        if (null != actions)
            acao.removeChild(actions);

        actions = __wf_xmlDocWorkflow.createElement("acoesAutomaticas");
        var action;
        for (var i = 0; i < connector.automaticActions.length; i++) {
            action = __wf_xmlDocWorkflow.createElement("acaoAutomatica");
            action.setAttribute("id", connector.automaticActions[i][__wf_cNumColDivCntAutoActions_id]);
            action.setAttribute("name", connector.automaticActions[i][__wf_cNumColDivCntAutoActions_name]);
            actions.appendChild(action);
        }
        acao.appendChild(actions);
        acoes.appendChild(acao);
        ne.appendChild(acoes);
        atualizaHiddenField(__wf_xmlDocWorkflow);
    }
}

// ------------------------------------------------------------------------------------------------- //
// ------ fim de bloco ~(Funções para inclusão de novo conector)                              ------ //
// ------------------------------------------------------------------------------------------------- //

// ------------------------------------------------------------------------------------------------- //
// ------                                                                                     ------ //
// ------ Funções para inclusão de novo TIMER                                                 ------ //
// ------                                                                                     ------ //
// ------------------------------------------------------------------------------------------------- //

function xTimer() {
    /// <summary>
    /// Função para criar um novo objeto timer. 
    /// </summary>
    /// <returns type="void"></returns>
    this.elementId = "";
    this.from = "";
    this.to = "";
    this.fromName = "";
    this.qtdeTempo = 0;
    this.unidade = "";
    this.assuntoNotificacao = "";
    this.textoNotificacao = "";
    this.assuntoNotificacao2 = "";
    this.textoNotificacao2 = "";

    // declara matriz para guardar as informações dos grupos que serão notificados pela ação do timer
    this.notificationGroups = new Array();

    // declara matriz para guardar as informações sobre as ações automáticas do timer
    this.automaticActions = new Array();
}

function limpaCamposDivTimer() {
    /// <summary>
    /// função para limpar os campos da div de timer. usada ao mostrar a div para o usuário.
    /// </summary>
    /// <returns type="void">void</returns>

    var val = "";

    lbWorkflow_tmr.SetValue(obtemIdentificaoWorkflow());

    cmbEtapaOrigem_tmr.cpTimerID = "";
    cmbEtapaOrigem_tmr.cpOriginalTimerID = "";

    cmbEtapaOrigem_tmr.ClearItems(); // limpa os itens do combo box
    cmbEtapaOrigem_tmr.SetSelectedIndex(-1); // 
    cmbEtapaOrigem_tmr.cpOriginalFrom = "";

    cmbEtapaDestino_tmr.ClearItems(); // limpa os itens do combo box
    cmbEtapaDestino_tmr.SetSelectedIndex(-1); // 
    cmbEtapaDestino_tmr.cpOriginalTo = "";

    cmbUnidadeTempo_tmr.SetSelectedIndex(-1); // 
    edtQtdTempo_tmr.SetValue(val);

    document.getElementById("mmTexto1_tmr").value = val;
    document.getElementById("txtAssunto1_tmr").value = val;
    document.getElementById("mmTexto2_tmr").value = val;
    document.getElementById("txtAssunto2_tmr").value = val;

    preencheCombosDivTimer();

    return;
}

function preencheCombosDivTimer() {
    /// <summary>
    /// função para preencher os combosBoxes da div Timer.
    /// </summary>
    /// <returns type="void">void</returns>

    var elName;
    var elId = new Object();
    var tipo;

    var tipos = new Array();
    tipos[0] = __wf_cTipoElementoEtapa;
    tipos[1] = __wf_cTipoElementoDisjuncao;
    tipos[2] = __wf_cTipoElementoJuncao;
    tipos[3] = __wf_cTipoElementoFim;

    var linhas = new Array();
    var k = 0;

    for (var j = 0; j < tipos.length; j++) {
        for (var i = 0; i < __wf_nlElementos.length; i++) {
            if (__wf_nlElementos[i].nodeType != 1)
                continue;

            tipo = obtemTipoElemento(__wf_nlElementos[i]);
            if ((true == ehElementoManualmenteConectavel(__wf_nlElementos[i]))
                && (tipos[j] == tipo)) {

                elName = getElementIdName(__wf_nlElementos[i]);
                elId = __wf_nlElementos[i].getAttribute("id");

                if ((null != elName) && (null != elId)) {
                    linhas[k] = new Array();
                    linhas[k][0] = elId;
                    linhas[k][1] = elName;
                    linhas[k][2] = tipo;
                    if (__wf_cTipoElementoEtapa == tipo) {                       
                        linhas[k][3] = f_ehElementoSubProcesso(__wf_nlElementos[i]);
                        linhas[k][4] = f_ehElementoEtapaInicial(__wf_nlElementos[i]);
                    }                        
                    else
                        linhas[k][3] = false;
                    k++;
                } // if (null != ...
            } // if (true == ...
        } // for i
    } // for j


    // ordena a matriz antes de adicionar ao drop down list;
    if (0 < linhas.length)
        linhas.sort(sortArrayEtapas);

    // adiciona os demais itens
    for (var i = 0; i < linhas.length; i++) {
        // os elementos fins, junção e disjunção não podem constar como origem de uma timer. 
        //não podem constar também elementos do tipo: "Etapa Inicial"
        if ((__wf_cTipoElementoEtapa == linhas[i][2]) && (linhas[i][3] == false) && (linhas[i][4] == false)) {
            cmbEtapaOrigem_tmr.AddItem(linhas[i][1], linhas[i][0]);
        }
        cmbEtapaDestino_tmr.AddItem(linhas[i][1], linhas[i][0]);
    } // for (var i
}

/// <summary>
///  Função para verificar se o tipo de elemento do parâmetro 'elementNode' é uma etapa inicial.
///  A verificação é feita apenas verificando o atributo etapaInicial.
/// </summary>
function f_ehElementoEtapaInicial(elementNode) {
    var ehEtapaInicial = elementNode.getAttribute("etapaInicial");
    if (ehEtapaInicial != undefined && ehEtapaInicial == "1")
        return true;
    else
        return false;
}

/// <summary>
/// Trata o 'click' no botão OK da div Timer. Realiza as consistências necessárias e inclui um novo timer no workflow
/// no gráfico.
/// </summary>
/// <returns type="void"></returns>
function onOkDivTimerClick(s, e) {
    if (true == validacaoInicialDivTimer()) {

        // chama a função para verificar se os grupos foram informados corretamente
        // esta função chamará a função para processar o preenchimento da divTimer, caso tudo esteja OK.
        gv_GruposNotificados_tmr.GetPageRowValues("CodigoPerfilWf;NomePerfilWf;Mensagem", validaGruposNotif_tmr);
    }
    return;
}

/// <summary>
/// função para fazer as validações iniciais nas informações preenchidas na divTimer.
/// (Todos os campos obrigatórios foram preenchidos? Não está incluindo um timer em duplicidade?, etc)
/// </summary>
/// <returns type="boolean">Se as validações iniciais foram bem sucedidas, retorna true. Caso contrário, false.</returns>
function validacaoInicialDivTimer() {
    var bRet = true;

    var idxOrigem = cmbEtapaOrigem_tmr.GetSelectedIndex();
    var idxDestino = cmbEtapaDestino_tmr.GetSelectedIndex();

    if (true == bRet) {
        if ((-1 == idxOrigem) || (-1 == idxDestino)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_os_campos__etapa_do_timer__e__nova_etapa__s_o_obrigat_rios_, 'atencao', true, false, null);
            bRet = false;
        }
    }  // if (true == bRet)

    if (true == bRet) {
        __wf_timerObj.elementId = cmbEtapaOrigem_tmr.cpTimerID;
        __wf_timerObj.from = cmbEtapaOrigem_tmr.GetValue();
        __wf_timerObj.to = cmbEtapaDestino_tmr.GetValue();

        if (__wf_timerObj.from == __wf_timerObj.to) {
            window.top.mostraMensagem(traducao.WorkflowCharts_a_etapa_do_timer_e_a_nova_etapa_n_o_podem_ser_a_mesma_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        // verificando se já não existe um timer na etapa de origem;

        // se for uma edição, e não se alterou a etapa de origem, é lógico que já existirá no xml 
        // o registro desta conexão. Neste caso, deixa passar.

        if (cmbEtapaOrigem_tmr.cpOriginalFrom == __wf_timerObj.from)
            bRet = true;
        else if (true == existeTimerNaEtapa(__wf_timerObj)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_j__existe_um_timer_para_a_etapa_de_origem__n_o___poss_vel_adicionar_mais_de_um_timer_para_uma_etapa_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        __wf_timerObj.qtdeTempo = edtQtdTempo_tmr.GetValue();

        if ((null == __wf_timerObj.qtdeTempo) || (0 == __wf_timerObj.qtdeTempo.length)) {
            window.top.mostraMensagem(traducao.WorkflowCharts___preciso_informar_a_quantidade_de_tempo_de_espera_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        __wf_timerObj.unidade = cmbUnidadeTempo_tmr.GetText();

        if ((null == __wf_timerObj.unidade) || (0 == __wf_timerObj.unidade.length)) {
            window.top.mostraMensagem(traducao.WorkflowCharts___preciso_informar_a_unidade_de_tempo_de_espera_, 'atencao', true, false, null);
            bRet = false;
        }
    }

    if (true == bRet) {
        __wf_timerObj.textoNotificacao1 = document.getElementById("mmTexto1_tmr").value;
        __wf_timerObj.assuntoNotificacao1 = document.getElementById("txtAssunto1_tmr").value;
        __wf_timerObj.textoNotificacao2 = document.getElementById("mmTexto2_tmr").value;
        __wf_timerObj.assuntoNotificacao2 = document.getElementById("txtAssunto2_tmr").value;
        // valida se os campos textos e assuntos foram preenchidos corretamente
        bRet = validaTextoAssunto(__wf_timerObj);
    } // if (true == bRet)

    if (true == bRet)
        __wf_timerObj.fromName = getNomeEtapaSemId(cmbEtapaOrigem_tmr.GetText());

    return bRet;
}

/// <summary>
/// Função para validar se ao informar os grupos que receberão notificações não foi esquecido 
/// de informar os textos das mensagens correspondentes
/// </summary>
/// <remarks>
///  Assume que a variável global "__wf_timerObj" já contém os valores dos campos informados na tela. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de grupos da div Timer
/// </param>
/// <returns type="void">void</returns>
function validaGruposNotif_tmr(valores) {
    var bRet = true, bMensagemAcao = false, bMensagemAcomp = false;
    var cTipoMsg = "";

    for (var i = 0; i < valores.length; i++) {
        cTipoMsg = valores[i][2];
        if (traducao.WorkflowCharts_a__o == cTipoMsg)
            bMensagemAcao = true;
        else if (traducao.WorkflowCharts_acompanhamento == cTipoMsg)
            bMensagemAcomp = true;
    }

    if (true == bRet) {
        if (("" == __wf_timerObj.textoNotificacao1) && (true == bMensagemAcao)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_foram_informados_grupos_para_receberem_a__mensagem_a__o___mas_n_o_foi_informado_o_texto_da_notifica__o_, 'atencao', true, false, null);
            bRet = false;
        }
        else if (("" !== __wf_timerObj.textoNotificacao1) && (false == bMensagemAcao)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_foi_informado_o_texto_da__mensagem_a__o__mas_n_o_foi_indicado_nenhum_grupo_para_receber_esta_mensagem_, 'atencao', true, false, null);
            bRet = false;
        }
    } // if (true == bRet)

    if (true == bRet) {
        if (("" == __wf_timerObj.textoNotificacao2) && (true == bMensagemAcomp)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_foram_informados_grupos_para_receberem_a__mensagem_acompanhamento___mas_n_o_foi_informado_o_texto_da_notifica__o_, 'atencao', true, false, null);
            bRet = false;
        }
        else if (("" !== __wf_timerObj.textoNotificacao2) && (false == bMensagemAcomp)) {
            window.top.mostraMensagem(traducao.WorkflowCharts_foi_informado_o_texto_da__mensagem_acompanhamento__mas_n_o_foi_indicado_nenhum_grupo_para_receber_esta_mensagem_, 'atencao', true, false, null);
            bRet = false;
        }
    } // if (true == bRet)
    //--------------------------------------------------------------------------------------------------

    if (true == bRet) {
        // processa o preenchimento da div gravando os dados no XML
        processaPreenchimentoDivTimer();
    }

    return;
}

/// <summary>
/// função para processar o preenchimento da DivConector
/// </summary>
/// <remarks>
/// Esta função assume que as informações entradas na divConector já estão todas VALIDADAS.
/// </remarks>
/// <returns>void</returns>
function processaPreenchimentoDivTimer() {
    // remove o timer anterior, caso se trata de uma edição
    if ("" != cmbEtapaOrigem_tmr.cpOriginalTimerID) {
        var auxTimer = new xTimer();
        auxTimer.elementId = cmbEtapaOrigem_tmr.cpOriginalTimerID;
        auxTimer.from = cmbEtapaOrigem_tmr.cpOriginalFrom;
        auxTimer.to = cmbEtapaDestino_tmr.cpOriginalTo;
        removeTimerFromXml(auxTimer);
    }

    if (false == addTimerInXml(__wf_timerObj)) {
        window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_de_novo_timer_ + " (0005)", 'erro', true, false, null);
    }
    else {
        // desabilita o botão 'Salvar' e oculta a div. o botão salvar será reabilitado após 
        // o cliente ter recebido os dados das grids e os gravado no xml.
        // btnSalvar.SetEnabled(false);
        divTimer.Hide();

        // inclui, no xml, as demais informações fornecidas nas grids;
        processaInformacoesGridsDivTimer();
    }
}

/// <summary>
/// função para verificar se já existe no XML um timer cadastrado para a etapa em questão.
/// </summary>
/// <remarks>
/// A função verifica se existe um timer já cadastrado para a etapa de origem do 
/// objeto <paramref name="timer"> passado como parâmetro
/// A função só retorna a indicação positiva da prévia existência de um timer caso o id do timer encontrado
/// seja diferente do id do timer passado como parâmetro
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="timer" type="objeto">Objeto timer cuja prévia existência deseja ser verificada.</param>
/// <returns type="boolean">Se foi verificada a existência do timer no XML, retorna true. 
///  Caso o timer não conste no XML ou não tenha sido possível verificar a existência, retorna false.
/// </returns>
function existeTimerNaEtapa(timer) {
    var bRet = false;
    var idTimer;
    var connector = new xConnector();

    for (var i = 0; i < __wf_nlElementos.length; i++) {
        if (ehElementoDoTipo(__wf_nlElementos[i], __wf_cTipoElementoTimer)) {
            idTimer = __wf_nlElementos[i].getAttribute("id");
            connector.from = timer.from;
            connector.to = idTimer;

            if (true == conexaoExistente(connector)) {
                bRet = true;
                break;
            } // if
        } // if (ehElementoDoTipo ...
    } // for 

    return bRet;
}

/// <summary>
/// Inclui um novo timer no XML que será enviado para o gráfico.
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="timer" type="objeto">Objeto timer a ser inserido no XML.</param>
/// <returns type="boolean">Se deu certo a inclusão da novo timer no xml, retorna true. Caso contrário, false.</returns>
function addTimerInXml(timer) {

    insertTimer(timer);
    atualizaXmlDoGrafico(__wf_xmlDocWorkflow);
    return true;
}

/// <summary>
/// Insere no XML um timer. 
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <param name="timer" type="objeto">Objeto timer a ser inserido no XML.</param>
/// <returns type="void">void</returns>
function insertTimer(timer) {

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");
    var toolText = timer.qtdeTempo + " " + timer.unidade + " " + traducao.WorkflowCharts_ap_s_entrar_em + " [" + timer.fromName + "]";

    var ne;
    // procura pelo timer no xml
    ne = getChildNodeById(__wf_nlElementos, timer.elementId);

    // se não foi encontrada a etapa
    if (null == ne) {
        var ne = __wf_xmlDocWorkflow.createElement("set");
        ne.setAttribute("shape", "RECTANGLE");
        ne.setAttribute("id", timer.elementId);
        ne.setAttribute("name", "");
        ne.setAttribute("toolText", toolText);
        ne.setAttribute("x", 5);
        ne.setAttribute("y", 95);
        ne.setAttribute("width", 25);
        ne.setAttribute("height", 28);
        // ne.setAttribute("color", "FFBC79");
        ne.setAttribute("imageNode", "1");
        ne.setAttribute("imageURL", "../imagens/Workflow/clockEdicao.png");
        ne.setAttribute("imageWidth", "24");
        ne.setAttribute("imageHeight", "27");
        ne.setAttribute("imageAlign", "MIDDLE");
        ne.setAttribute("plotBorderAlpha", "0");
        ne.setAttribute("alpha", "0");
        ne.setAttribute("tipoElemento", __wf_cTipoElementoTimer);
    }
    else {
        ne.setAttribute("toolText", toolText);
    }

    ds[0].appendChild(ne);

    // ------------------------------------------//
    /// ----- insere os dois conectores no xml;
    var connector = new xConnector();
    connector.acao = "";

    connector.from = timer.from;
    connector.to = timer.elementId;

    // indica que tem timer 
    if (gv_Acoes_tmr.GetVisibleRowsOnPage() > 0)
        connector.temAcoesAutomaticas = true;

    insertConnector(connector);
    connector.from = timer.elementId;
    connector.to = timer.to;

    insertConnector(connector);
    // ------------------------------------------//
}


/// <summary>
/// Solicita, ao servidor, as informações das grids da div Timer, incluindo cada uma no xml.
/// </summary>
/// <returns type="void">void</returns>
function processaInformacoesGridsDivTimer() {
    var campos = "CodigoPerfilWf;NomePerfilWf;Mensagem";

    // solicita inicialmente as informações sobre a grid de formulários. 
    gv_GruposNotificados_tmr.GetPageRowValues(campos, recebeCamposGridGruposDivTimer);
}

/// <summary>
/// Função para receber, do servidor, os valores dos campos da grid de grupos notificados da DivTimer
/// e gravá-los no XML. 
/// </summary>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de grupos da div Timer
/// </param>
/// <returns type="void">void</returns>
function recebeCamposGridGruposDivTimer(valores) {
    var i, j;

    // remove os elementos antigos do objeto
    if (0 < __wf_timerObj.notificationGroups.length)
        __wf_timerObj.notificationGroups.splice(0, __wf_timerObj.notificationGroups.length);

    for (i = 0; i < valores.length; i++)
        __wf_timerObj.notificationGroups.push(valores[i]);

    atualizaXmlComGruposDaDivTimer();

    // solicita informações sobre a grid de ações automáticas. A função recebeCamposGridAcoesDivTimer 
    // irá gravar as informações no xml assim que as receber.

    var campos = "Nome;CodigoAcaoAutomaticaWf";
    gv_Acoes_tmr.GetPageRowValues(campos, recebeCamposGridAcoesDivTimer);
}


/// <summary>
/// Função para receber os valores dos campos da grid de ações da DivTimer
/// </summary>
/// <remarks>
/// Após receber e gravar os valores da grid de ações, habilita o botão salvar da tela 
/// que foi desabilitado pela função que trata o click do botão ok da div (onOkDivTimerClick(s, e)).
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de grupos de ações da div Etapa
/// <param>
/// <returns type="void">void</returns>
function recebeCamposGridAcoesDivTimer(valores) {
    var i, j;

    // remove os elementos antigos do objeto
    if (0 < __wf_timerObj.automaticActions.length)
        __wf_timerObj.automaticActions.splice(0, __wf_timerObj.automaticActions.length);

    for (i = 0; i < valores.length; i++)
        __wf_timerObj.automaticActions.push(valores[i]);

    atualizaXmlComAutomaticActionsDaDivTimer();
}

/// <summary>
/// Função para incluir no XML dados da grid 'Grupos Notificados' da DivTimer 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. 
/// Assume que o objeto __wf_activObj contém os dados a serem incluídos e a objeto __wf_xmlDocWorkflow
/// contém o xml com as informações. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de 'grupos notificados' da div Timer
/// </param>
/// <returns type="void">void</returns>
function atualizaXmlComGruposDaDivTimer() {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");

    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (null != sets)
        ne = getChildNodeById(sets, __wf_timerObj.from);

    if (null != ne) {
        var acoes = getChildNodeByName(ne.childNodes, "acoes");
        if (null == acoes)
            acoes = __wf_xmlDocWorkflow.createElement("acoes");

        var acao = obtemXmlAcaoTimerAtualizado(acoes, __wf_timerObj);

        var groups = getChildNodeByName(acao.childNodes, "gruposNotificados");
        if (null != groups)
            acao.removeChild(groups);

        groups = __wf_xmlDocWorkflow.createElement("gruposNotificados");

        var group;
        for (var i = 0; i < __wf_timerObj.notificationGroups.length; i++) {
            group = __wf_xmlDocWorkflow.createElement("grupo");
            group.setAttribute("id", __wf_timerObj.notificationGroups[i][__wf_cNumColDivCntGroups_id]);
            group.setAttribute("name", __wf_timerObj.notificationGroups[i][__wf_cNumColDivCntGroups_name]);
            group.setAttribute("msgBox", __wf_timerObj.notificationGroups[i][__wf_cNumColDivCntGroups_msgBox]);
            groups.appendChild(group);
        }
        acao.appendChild(groups);
        acoes.appendChild(acao);
        ne.appendChild(acoes);
        atualizaHiddenField(__wf_xmlDocWorkflow);
    }
}

/// <summary>
/// Função para devolver um "nó" XML contendo as informações sobre a ação especificada pelo objeto xTimer 
/// passado como parâmetro.
/// </summary>
/// <remarks>
/// Esta função devolve um "nó" XML contendo as informações sobre a ação. Atenção!!! Esta função não atualiza o XML, 
/// apenas devolve o nó. 
/// </remarks>
/// <param name="acoes" type="DOM XML">Objeto DOM no qual serão incluídas as informações.</param>
/// <param name="timer" type="objeto">Objeto do tipo xTimer contendo as informações</param>
/// <returns type="DOM XML">Retorna a ação void</returns>
function obtemXmlAcaoTimerAtualizado(acoes, timer) {
    var tipoElementoDestino, nextStageId;

    var acao = getChildNodeById(acoes.childNodes, "timer");  // localiza uma ação de id 'timer'
    if (null == acao)
        acao = __wf_xmlDocWorkflow.createElement("acao");

    tipoElementoDestino = obtemTipoElemento(getChildNodeById(__wf_nlElementos, timer.to));

    if ((__wf_cTipoElementoEtapa == tipoElementoDestino) || (__wf_cTipoElementoDisjuncao == tipoElementoDestino))
        nextStageId = timer.to;
    else
        nextStageId = "";

    acao.setAttribute("id", "timer");
    acao.setAttribute("nextStageId", nextStageId);
    acao.setAttribute("to", timer.to);
    acao.setAttribute("actionType", "T");
    acao.setAttribute("timeoutValue", timer.qtdeTempo);
    acao.setAttribute("timeoutUnit", timer.unidade);

    //Assunto da notificação1
    var assunto = getChildNodeByName(acao.childNodes, "assuntoNotificacao");
    var conteudo;

    if (null != assunto)
        acao.removeChild(assunto);
    assunto = __wf_xmlDocWorkflow.createElement("assuntoNotificacao");

    if ((null != timer.assuntoNotificacao1) && ("" != timer.assuntoNotificacao1))
        conteudo = __wf_xmlDocWorkflow.createTextNode(timer.assuntoNotificacao1);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    assunto.appendChild(conteudo);
    acao.appendChild(assunto);

    //texto da notificação1
    var textoEmail = getChildNodeByName(acao.childNodes, "textoNotificacao");
    if (null != textoEmail)
        acao.removeChild(textoEmail);

    textoEmail = __wf_xmlDocWorkflow.createElement("textoNotificacao");

    if ((null != timer.textoNotificacao1) && ("" != timer.textoNotificacao1))
        conteudo = __wf_xmlDocWorkflow.createTextNode(timer.textoNotificacao1);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    textoEmail.appendChild(conteudo);
    acao.appendChild(textoEmail);

    //Assunto da notificação2
    assunto = getChildNodeByName(acao.childNodes, "assuntoNotificacao2");

    if (null != assunto)
        acao.removeChild(assunto);
    assunto = __wf_xmlDocWorkflow.createElement("assuntoNotificacao2");

    if ((null != timer.assuntoNotificacao2) && ("" != timer.assuntoNotificacao2))
        conteudo = __wf_xmlDocWorkflow.createTextNode(timer.assuntoNotificacao2);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    assunto.appendChild(conteudo);
    acao.appendChild(assunto);

    //texto da notificação2
    textoEmail = getChildNodeByName(acao.childNodes, "textoNotificacao2");
    if (null != textoEmail)
        acao.removeChild(textoEmail);

    textoEmail = __wf_xmlDocWorkflow.createElement("textoNotificacao2");

    if ((null != timer.textoNotificacao2) && ("" != timer.textoNotificacao2))
        conteudo = __wf_xmlDocWorkflow.createTextNode(timer.textoNotificacao2);
    else
        conteudo = __wf_xmlDocWorkflow.createTextNode("");

    textoEmail.appendChild(conteudo);
    acao.appendChild(textoEmail);
    return acao;
}


/// <summary>
/// Função para incluir no XML dados das ações automáticas da grid 'Ações' da divTimer 
/// </summary>
/// <remarks>
/// As informações são incluídas no objeto __wf_xmlDocWorkflow. 
/// Assume que o objeto __wf_timerObj contém os dados a serem incluídos e a objeto __wf_xmlDocWorkflow
/// contém o xml com as informações. 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos de cada linha 
/// da grid de 'Ações' da div Timer
/// </param>
/// <returns type="void">void</returns>
function atualizaXmlComAutomaticActionsDaDivTimer() {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");
    if (null != ds)
        sets = __wf_xmlDocWorkflow.getElementsByTagName("set");

    if (sets != null)
        ne = getChildNodeById(sets, __wf_timerObj.from);

    if ((null != ne) && (0 < __wf_timerObj.automaticActions.length)) {
        var acoes = getChildNodeByName(ne.childNodes, "acoes");
        if (null == acoes)
            acoes = __wf_xmlDocWorkflow.createElement("acoes");

        var acao = obtemXmlAcaoTimerAtualizado(acoes, __wf_timerObj);

        var actions = getChildNodeByName(acao.childNodes, "acoesAutomaticas");
        if (null != actions)
            acao.removeChild(actions);

        actions = __wf_xmlDocWorkflow.createElement("acoesAutomaticas");
        var action;
        for (var i = 0; i < __wf_timerObj.automaticActions.length; i++) {
            action = __wf_xmlDocWorkflow.createElement("acaoAutomatica");
            action.setAttribute("id", __wf_timerObj.automaticActions[i][__wf_cNumColDivCntAutoActions_id]);
            action.setAttribute("name", __wf_timerObj.automaticActions[i][__wf_cNumColDivCntAutoActions_name]);
            actions.appendChild(action);
        }
        acao.appendChild(actions);
        acoes.appendChild(acao);
        ne.appendChild(acoes);
        atualizaHiddenField(__wf_xmlDocWorkflow);
    }
}

/// <summary>
/// Exclui um timer do XML, atualizando todas as informações do XML.
/// </summary>
/// <param name="timer" type="objeto">Objeto timer a ser excluído do XML.</param>
/// <returns type="boolean">Se deu certo a exclusão do timer do xml, retorna true. Caso contrário, false.</returns>
function removeTimerFromXml(timer) {
    var connector = new xConnector();
    if ((null != timer.elementId) & (null != timer.from) & (null != timer.to)) {
        // remove o conector ligado à etapa de origem             
        connector.from = timer.from;
        connector.to = timer.elementId;
        connector.acao = "timer";
        removeConnectorFromXml(connector);

        // remove o conector ligado à etapa de destino
        connector.from = timer.elementId;
        connector.to = timer.to;
        connector.acao = "";
        removeConnectorFromXml(connector);

        // remove o elemento gráfico do xml;
        excluiElementoDoXml(timer.elementId);
        atualizaXmlDoGrafico(__wf_xmlDocWorkflow);
    }
    return;
}


/// <summary>
/// Retorna o ID do elemento 'destino' da primeira ligação encontrada onde o parâmetro <paramref name="id">
/// conste como a 'origem' da ligação.
/// </summary>
/// <param name="id" type="string">ID do elemento para o qual se quer o ID de destino.</param>
/// <returns type="string">Se encontrou, retorna o ID do elemento destino da primeira ligação que encontrar.
/// Caso contrário, retorna null.
/// </returns>
function getFirstToID(id) {
    var to, from;
    var setaInicio, setaFim;
    var toID = null;

    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        from = __wf_nlConectores[i].getAttribute("from");
        to = __wf_nlConectores[i].getAttribute("to");

        if ((null != from) && (null != to) && ((from == id) || (to == id))) {
            setaInicio = __wf_nlConectores[i].getAttribute("arrowAtStart");
            if (null == setaInicio)
                setaInicio = "0";

            setaFim = __wf_nlConectores[i].getAttribute("arrowAtEnd");
            if (null == setaFim)
                setaFim = "0";

            // se a ligação está saindo do elemento
            if ((from == id) && ("1" == setaFim))
                toID = to;
            else if ((to == id) && ("1" == setaInicio))
                toID = from;

            // se encontrou, pára de percorrer
            if (null != toID)
                break;
        } // if ( (null != from) && ...
    } // for (var i=0; i<__wf_nlConectores; i++)

    return toID;
}

/// <summary>
/// Retorna o ID do elemento 'origem' da primeira ligação encontrada onde o parâmetro <paramref name="id">
/// conste como o 'destino' da ligação.
/// </summary>
/// <param name="id" type="string">ID do elemento para o qual se quer o ID de origem da ligação.</param>
/// <returns type="string">Se encontrou, retorna o ID do elemento de origem da primeira ligação que encontrar.
/// Caso contrário, retorna null.
/// </returns>
function getFirstFromID(id) {
    var to, from;
    var setaInicio, setaFim;
    var fromID = null;

    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        from = __wf_nlConectores[i].getAttribute("from");
        to = __wf_nlConectores[i].getAttribute("to");

        if ((null != from) && (null != to) && ((from == id) || (to == id))) {
            setaInicio = __wf_nlConectores[i].getAttribute("arrowAtStart");
            if (null == setaInicio)
                setaInicio = "0";

            setaFim = __wf_nlConectores[i].getAttribute("arrowAtEnd");
            if (null == setaFim)
                setaFim = "0";

            // se a ligação está chegando ao elemento
            if ((to == id) && ("1" == setaFim))
                fromID = from;
            if ((from == id) && ("1" == setaInicio))
                fromID = to;

            // se encontrou, pára de percorrer
            if (null != fromID)
                break;
        } // if ( (null != from) && ...
    } // for (var i=0; i<__wf_nlConectores; i++)

    return fromID;
}

// ------------------------------------------------------------------------------------------------- //
// ------ fim de bloco ~(Funções para inclusão de novo timer)                                ------ //
// ------------------------------------------------------------------------------------------------- //


// ------------------------------------------------------------------------------------------------- //
// ------                                                                                     ------ //
// ------ Funções para inclusão de um novo elemento de fim                                    ------ //
// ------                                                                                     ------ //
// ------------------------------------------------------------------------------------------------- //

function preparaExibicaoDivDisJunEnd(tipoElemento) {
    pnlCnkDisJunFim.cpElementType = tipoElemento;
    var txtTipo = pnlCnkDisJunFim.cpElementName;
    var captionID = pnlCnkDisJunFim.cpCaptionID;
    var caption = pnlCnkDisJunFim.cpElementCaption;


    if ("" == captionID) {
        captionID = getNextCaptionId(__wf_nlElementos, tipoElemento);
        pnlCnkDisJunFim.cpCaptionID = captionID;
    }

    lblCaptionTipoElemento.SetValue(txtTipo + " " + captionID);
    txtDescricaoDivDescricao.SetValue(caption);
}


/// <summary>
/// Zera as propriedades customizadas do panelCallback DisJunFim.
/// </summary>
/// <param name="tipoElemento" type="string">Código do tipo do elemento a atribuir na propriedade [cpElementType]</param>
/// <returns>void</returns>
function zeraJSProperties_pnlCbkDisJunFim(tipoElemento) {
    pnlCnkDisJunFim.cpElementType = tipoElemento;
    pnlCnkDisJunFim.cpCaptionID = "";
    pnlCnkDisJunFim.cpElementID = "";
    pnlCnkDisJunFim.cpElementCaption = "";

    var elementName = "";

    if (__wf_cTipoElementoFim == tipoElemento)
        elementName = traducao.WorkflowCharts_fim;
    else if (__wf_cTipoElementoJuncao == tipoElemento)
        elementName = "Junção";
    else if (__wf_cTipoElementoDisjuncao == tipoElemento)
        elementName = "Disjunção";

    pnlCnkDisJunFim.cpElementName = elementName;

    return;
}

/// <summary>
/// Trata o 'click' no botão Ok da divxxx
/// </summary>
/// <param name="s" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
/// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
function onOkDisJunEndClick() {
    var bRet = true;

    if (false == xmlWorkflowOk())
        return false;

    var tipoElemento = pnlCnkDisJunFim.cpElementType;
    var txtTipo = pnlCnkDisJunFim.cpElementName;
    var captionID = pnlCnkDisJunFim.cpCaptionID;
    var elementID = pnlCnkDisJunFim.cpElementID;
    var descricao = txtDescricaoDivDescricao.GetValue();

    if (null == descricao)
        descricao = "";

    if ("" == elementID)
        elementID = getNextElementId(__wf_nlElementos);

    var toolText = txtTipo + " " + captionID + ((descricao != "") ? (" - " + descricao) : "");


    if (__wf_cTipoElementoJuncao == tipoElemento) {
        if (false == addJunctionNodeInXml(__wf_xmlDocWorkflow, elementID, captionID, toolText)) {
            bRet = false;
            window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_do_elemento_gr_fico_ + " (0008)", 'erro', true, false, null);
        }
    }
    else if (__wf_cTipoElementoDisjuncao == tipoElemento) {
        if (false == addDisjunctionNodeInXml(__wf_xmlDocWorkflow, elementID, captionID, toolText)) {
            bRet = false;
            window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_do_elemento_gr_fico_ = " (0007)", 'erro', true, false, null);
        }
    }
    else if (__wf_cTipoElementoFim == tipoElemento) {
        if (false == addEndNodeInXml(__wf_xmlDocWorkflow, elementID, captionID, toolText)) {
            bRet = false;
            window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_de_elemento_gr_fico_ + " (0006)", 'erro', true, false, null);
        }
    }
    return bRet;
}

function addEndNodeInXml(xmlDoc, elementId, endId, toolText) {
    /// <summary>
    /// Inclui um novo elemento de fim no no XML que será enviado para o gráfico.
    /// </summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM no qual serão incluídas as informações do novo elemento de fim.</param>
    /// <param name="elementId" type="integer">id do objeto gráfico no XML.</param>
    /// <param name="endId" type="integer">id do objeto 'fim' no XML.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>

    insertEndNote(xmlDoc, elementId, endId, toolText);
    atualizaXmlDoGrafico(xmlDoc);
    return true;
}


function insertEndNote(xmlDoc, elementId, endId, toolText) {
    /// <summary>
    /// insere no XML o novo elemento de fim.
    /// </summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM no qual serão incluídas as informações do novo elemento de fim.</param>
    /// <param name="elementId" type="integer">id do objeto gráfico no XML.</param>
    /// <param name="endId" type="integer">id do objeto 'fim' no XML.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>

    var ds = xmlDoc.getElementsByTagName("dataSet");
    var name = endId.toString();
    var toolText = toolText;

    var ne;
    // procura pela elemento no xml
    ne = getChildNodeById(__wf_nlElementos, elementId);

    // se não foi encontrada a etapa
    if (null == ne) {
        ne = xmlDoc.createElement("set");
        ne.setAttribute("shape", "POLYGON");
        ne.setAttribute("id", elementId);
        ne.setAttribute("name", name);
        ne.setAttribute("toolText", toolText);
        ne.setAttribute("x", 5);
        ne.setAttribute("y", 95);
        ne.setAttribute("numSides", "8");
        ne.setAttribute("radius", "10");
        ne.setAttribute("color", "33CC66");
        ne.setAttribute("tipoElemento", __wf_cTipoElementoFim);
        ds[0].appendChild(ne);
    }
    else
        ne.setAttribute("toolText", toolText);

    return true;
}

// ------------------------------------------------------------------------------------------------- //
// ------ fim de bloco ~(Funções para inclusão de novo elemento de fim)                       ------ //
// ------------------------------------------------------------------------------------------------- //



// ------------------------------------------------------------------------------------------------- //
// ------                                                                                     ------ //
// ------ Funções para inclusão de um novo elemento disjunção                                 ------ //
// ------                                                                                     ------ //
// ------------------------------------------------------------------------------------------------- //

function ajusteExibicaoDivDescricaoDisjunction(acao) {
    if (false == xmlWorkflowOk())
        return false;

    hfValoresTela.Set("DivDescricao", "D");
    var caption = "Disjunção " + getNextCaptionId(__wf_nlElementos, __wf_cTipoElementoDisjuncao);

    lblCaptionTipoElemento.SetValue(caption);
    txtDescricaoDivDescricao.SetValue("");

    //caso que seja edição. Gueter fazera os cambios pertinentes;
}


function onNewDisjunctionClick(captiom) {
    /// <summary>
    /// Trata o 'click' no botão Novo elemtno junção.
    /// </summary>
    /// <param name="s" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
    var bRet = true;

    if (false == xmlWorkflowOk())
        return false;

    var elementId = getNextElementId(__wf_nlElementos);
    var captionId = getNextCaptionId(__wf_nlElementos, __wf_cTipoElementoDisjuncao);
    var toolText = "Disjunção " + captionId + ((captiom != "") ? (" - " + captiom) : "");

    if (false == addDisjunctionNodeInXml(__wf_xmlDocWorkflow, elementId, captionId, toolText)) {
        bRet = false;
        window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_do_elemento_gr_fico_ + " (0007)", 'erro', true, false, null);
    }
    return bRet;
}

function addDisjunctionNodeInXml(xmlDoc, elementId, junctionId, toolText) {
    /// <summary>
    /// Inclui um novo elemento de fim no no XML que será enviado para o gráfico.
    /// </summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM no qual serão incluídas as informações do novo elemento de fim.</param>
    /// <param name="elementId" type="integer">id do objeto gráfico no XML.</param>
    /// <param name="junctionId" type="integer">id do objeto 'fim' no XML.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>

    insertDisjunctionNote(xmlDoc, elementId, junctionId, toolText);
    atualizaXmlDoGrafico(xmlDoc);
    return true;
}


function insertDisjunctionNote(xmlDoc, elementId, junctionId, toolText) {
    /// <summary>
    /// insere no XML o novo elemento de fim.
    /// </summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM no qual serão incluídas as informações do novo elemento de fim.</param>
    /// <param name="elementId" type="integer">id do objeto gráfico no XML.</param>
    /// <param name="junctionId" type="integer">id do objeto 'fim' no XML.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>

    var ds = xmlDoc.getElementsByTagName("dataSet");
    var name = junctionId.toString();
    //var toolText = 'Disjunção ' + name;

    var ne;
    // procura pela elemento no xml
    ne = getChildNodeById(__wf_nlElementos, elementId);

    // se não foi encontrada a etapa
    if (null == ne) {
        ne = xmlDoc.createElement("set");
        ne.setAttribute("shape", "RECTANGLE");
        ne.setAttribute("id", elementId);
        ne.setAttribute("name", name);
        ne.setAttribute("toolText", toolText);
        ne.setAttribute("x", 5);
        ne.setAttribute("y", 95);
        ne.setAttribute("width", 24);
        ne.setAttribute("height", 40);
        ne.setAttribute("imageNode", "1");
        ne.setAttribute("imageURL", "../imagens/Workflow/disjunction003.png");
        ne.setAttribute("imageWidth", "23");
        ne.setAttribute("imageHeight", "23");
        ne.setAttribute("imageAlign", "TOP");
        ne.setAttribute("labelAlign", "BOTTOM");
        ne.setAttribute("color", "33C1FE");
        ne.setAttribute("tipoElemento", __wf_cTipoElementoDisjuncao);

        ds[0].appendChild(ne);
    }
    else
        ne.setAttribute("toolText", toolText);
}

// ------------------------------------------------------------------------------------------------- //
// ------ fim de bloco ~(Funções para inclusão de novo elemento junção)                       ------ //
// ------------------------------------------------------------------------------------------------- //



// ------------------------------------------------------------------------------------------------- //
// ------                                                                                     ------ //
// ------ Funções para inclusão de um novo elemento junção                                    ------ //
// ------                                                                                     ------ //
// ------------------------------------------------------------------------------------------------- //

function ajusteExibicaoDivDescricaoJunction(acao) {
    if (false == xmlWorkflowOk())
        return false;

    hfValoresTela.Set("DivDescricao", "J");
    var caption = "Junção " + getNextCaptionId(__wf_nlElementos, __wf_cTipoElementoDisjuncao);

    lblCaptionTipoElemento.SetValue(caption);
    txtDescricaoDivDescricao.SetValue("");

    //caso que seja edição. Gueter fazera os cambios pertinentes;
}

function onNewJunctionClick(captiom) {
    /// <summary>
    /// Trata o 'click' no botão Novo elemtno junção.
    /// </summary>
    /// <param name="s" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>
    var bRet = true;

    if (false == xmlWorkflowOk())
        return false;

    var elementId = getNextElementId(__wf_nlElementos);
    var captionId = getNextCaptionId(__wf_nlElementos, __wf_cTipoElementoJuncao);
    var toolText = "Junção " + captionId + ((captiom != "") ? (" - " + captiom) : "");

    if (false == addJunctionNodeInXml(__wf_xmlDocWorkflow, elementId, captionId, toolText)) {
        bRet = false;
        window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_durante_a_inclus_o_do_elemento_gr_fico_ + " (0008)", 'erro', true, false, null);
    }
    return bRet;
}

function addJunctionNodeInXml(xmlDoc, elementId, junctionId, toolText) {
    /// <summary>
    /// Inclui um novo elemento de fim no no XML que será enviado para o gráfico.
    /// </summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM no qual serão incluídas as informações do novo elemento de fim.</param>
    /// <param name="elementId" type="integer">id do objeto gráfico no XML.</param>
    /// <param name="junctionId" type="integer">id do objeto 'fim' no XML.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>

    insertJunctionNote(xmlDoc, elementId, junctionId, toolText);
    atualizaXmlDoGrafico(xmlDoc);
    return true;
}


function insertJunctionNote(xmlDoc, elementId, junctionId, toolText) {
    /// <summary>
    /// insere no XML o novo elemento de fim.
    /// </summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM no qual serão incluídas as informações do novo elemento de fim.</param>
    /// <param name="elementId" type="integer">id do objeto gráfico no XML.</param>
    /// <param name="junctionId" type="integer">id do objeto 'fim' no XML.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>

    var ds = xmlDoc.getElementsByTagName("dataSet");
    var name = junctionId.toString();
    //var toolText = 'Junção ' + name;

    var ne;
    // procura pela elemento no xml
    ne = getChildNodeById(__wf_nlElementos, elementId);

    // se não foi encontrada a etapa
    if (null == ne) {
        ne = xmlDoc.createElement("set");
        ne.setAttribute("shape", "RECTANGLE");
        ne.setAttribute("id", elementId);
        ne.setAttribute("name", name);
        ne.setAttribute("toolText", toolText);
        ne.setAttribute("x", 5);
        ne.setAttribute("y", 95);
        ne.setAttribute("width", 24);
        ne.setAttribute("height", 40);
        ne.setAttribute("imageNode", "1");
        ne.setAttribute("imageURL", "../imagens/Workflow/junction024.png");
        ne.setAttribute("imageWidth", "23");
        ne.setAttribute("imageHeight", "23");
        ne.setAttribute("imageAlign", "TOP");
        ne.setAttribute("labelAlign", "BOTTOM");
        ne.setAttribute("color", "CCFFCC");
        ne.setAttribute("tipoElemento", __wf_cTipoElementoJuncao);

        ds[0].appendChild(ne);
    }
    else
        ne.setAttribute("toolText", toolText);
}

// ------------------------------------------------------------------------------------------------- //
// ------ fim de bloco ~(Funções para inclusão de novo elemento junção)                       ------ //
// ------------------------------------------------------------------------------------------------- //

function getNextCaptionId(nodes, elementType) {
    /// <summary>
    /// Devolve o próximo ID de uma elemento que tem o id no seu atributo name (junção, disjunção, fim de fluxo). O ID devolvido por esta função não pode ser usadao como ID do 
    /// elemento gráfico, apenas como índice para nomear o elmento gráfico.
    /// .</summary>
    /// <param name="elementType" type="string">tipo com o qual será comparado o tipo do elemento gráfico.</param>
    /// <param name="nodes" type="XML Nodes">Coleção, no formato XML, que contém os nós a serem varridos para 
    /// identificação do próximo ID de fim.</param>
    /// <returns type="integer">ID do elemento de fim.</returns>

    var lastId = 0;
    var id;
    var name;

    for (var i = 0; i < nodes.length; i++) {
        if (true == ehElementoDoTipo(nodes[i], elementType)) {
            name = nodes[i].getAttribute("name");
            if ((null != name) && (name.length > 0)) {
                id = parseInt(name);
                if (id > lastId)
                    lastId = id;
            }
        }
    }

    lastId++;
    return lastId;
}

function xmlWorkflowOk() {
    /// <summary>
    /// Função para verificar se está tudo ok com o XML que a tela irá trabalhar para inserir elementos 
    /// gráficos de workflow
    /// </summary>
    /// <returns type="boolean">Retorna true se o xml estiver ok. Caso contrário, false.</returns>

    // atribui o XML à variável global __wf_xmlDocWorkflow
    __wf_xmlDocWorkflow = getWorkXmlDoc();

    if (null == __wf_xmlDocWorkflow) {
        window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0001)", 'erro', true, false, null);
        return false;
    }

    // atribui a lista de nós à variável global
    __wf_nlElementos = __wf_xmlDocWorkflow.getElementsByTagName("set");
    if (null == __wf_nlElementos) {
        window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0002)", 'erro', true, false, null);
        return false;
    }

    // atribui a lista de nós à variável global
    __wf_nlConectores = __wf_xmlDocWorkflow.getElementsByTagName("connector");
    if (null == __wf_nlConectores) {
        window.top.mostraMensagem(traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0003)", 'erro', true, false, null);
        return false;
    }

    if (window.pcResumoWf && pcResumoWf.GetVisible())
        tlResumoWf.PerformCallback();

    return true;
}

function ehElementoDoTipo(elementNode, elementType) {
    /// <summary>
    /// função para identificar se um elemento gráfico é um de um determinado tipo.
    /// .</summary>
    /// <param name="elementNode" type="XML Node">nó XML referente ao elemento gráfico.</param>
    /// <param name="elementType" type="string">tipo com o qual será comparado o tipo do elemento gráfico.</param>
    /// <returns type="boolean">Se for possível verificar que o elemento se refere ao tipo em questão, 
    ///  retorna true. Caso contrário, retorna false.</returns>
    var bRet = false;
    var indTipoElemento = obtemTipoElemento(elementNode);

    if ((null != indTipoElemento) && (elementType == indTipoElemento))
        bRet = true;

    return bRet;
}

function ehElementoManualmenteConectavel(elementNode) {
    /// <summary>
    /// função para identificar se um elemento gráfico é um elemento que se conecta manualmente.
    /// .</summary>
    /// <param name="elementNode" type="XML Node">nó XML referente ao elemento gráfico.</param>
    /// <returns type="boolean">Se for possível verificar que o elemento é manualmente conectável, 
    ///  retorna true. Caso contrário, retorna false.</returns>
    var bRet = false;
    var indTipoElemento = obtemTipoElemento(elementNode);

    if ((null != indTipoElemento) && (__wf_cTipoElementoTimer != indTipoElemento) && (__wf_cTipoElementoInicio != indTipoElemento))
        bRet = true;

    return bRet;
}

/// <summary>
/// função para devolver o tipo de elemento do parâmetro 'elementNode'.
/// .</summary>
/// <param name="elementNode" type="XML Node">nó XML referente ao elemento gráfico.</param>
/// <returns type="string">Retorna o tipo de elemento ou null, caso o elemento não tenha o atributo 'tipoElemento'.</returns>
function obtemTipoElemento(elementNode) {
    if ((null == elementNode) || (elementNode.nodeType != 1))
        return null;
    else
        return elementNode.getAttribute("tipoElemento");
}

/// <summary>
///  Função para verificar se o tipo de elemento do parâmetro 'elementNode' é um subprocesso.
///  A verificação é feita apenas verificando o atributo codigoSubFluxo.
/// .</summary>
/// <param name="elementNode" type="XML Node">nó XML referente ao elemento gráfico.</param>
/// <returns type="string">Retorna o tipo de elemento ou null, caso o elemento não tenha o atributo 'tipoElemento'.</returns>
function f_ehElementoSubProcesso(elementNode) {
    if ((null == elementNode) || (elementNode.nodeType != 1))
        return false;
    else {
        var codigoSubfluxo = elementNode.getAttribute("codigoSubfluxo");
        if (codigoSubfluxo > 0)
            return true;
        else
            return false;
    }
}


function getLengthMajorWord(str) {
    /// <summary>
    /// função para devolver o tamanho da maior palavra numa frase.
    /// .</summary>
    /// <param name="str" type="string">a frase a ser verificada.</param>
    /// <returns type="integer">A quantidade de caracteres que possui a maior palavra na frase.</returns>
    var s = str.split(" ");
    var maior = 0;

    for (var i = 0; i < s.length; i++) {
        if (s[i].length > maior)
            maior = s[i].length;
    }
    return maior;
}


function getWorkXmlDoc() {
    /// <summary>
    /// Devolve um objeto DOM XML contendo o XML de trabalho -> XML a usar para incluir elementos gráficos e 
    /// atualizar o componente flash. Devolve NULL em caso de erro.
    /// </summary>
    /// <returns type="DOM XML">Retorna o objeto. Em caso de falha, retorna NULL.</returns>

    var xmlDocOrigem = getXmlDocDaOrigem();
    var xmlDocGraf = null;
    var xmlDocWork = null;
    if (null != xmlDocOrigem) {
        xmlDocGraf = getXmlDocDoGrafico()
    }

    if ((null != xmlDocOrigem) && (null != xmlDocGraf)) {
        xmlDocWork = mesclaXMLs(xmlDocOrigem, xmlDocGraf);
    }

    return xmlDocWork;
}

function getXmlDocDaOrigem() {
    /// <summary>
    /// Devolve um objeto DOM XML contendo o XML original -> não processado pelo gráfico do flash.
    /// </summary>
    /// <returns type="DOM XML">Retorna o objeto. Em caso de falha, retorna NULL.</returns>

    // obtém o XML de origem do hidden field de nome fXML. 
    var xmlDoc = null;

    var xml = hfValoresTela.Get("XMLWF");

    if ((null != xml) && (xml.length > 0))
        xmlDoc = xmlDocFromString(xml);

    return xmlDoc;
}

function getXmlDocDoGrafico() {
    /// <summary>
    /// Devolve um objeto DOM XML contendo o XML que está 'dentro' do componente flash no momento. Devolve NULL em caso de erro.
    /// </summary>
    /// <returns type="DOM XML">Retorna o objeto. Em caso de falha, retorna NULL.</returns>

    return xmlDocFromString(GetXmlDoGrafico());
}

function xmlDocFromString(str) {
    /// <summary>
    /// Devolve um objeto DOM XML dado um conteúdo XML dentro de uma string. Devolve NULL em caso de erro.
    /// </summary>
    /// <param name="str" type="string">Conteúdo do XML.</param>
    /// <returns type="DOM XML">Objeto DOM XML contendo o xml passado como parâmetro.</returns>
    var xmlDoc;
    if (null == str)
        return null;
    else {

        if (window.DOMParser) {
            parser = new DOMParser();
            xmlDoc = parser.parseFromString(str, "text/xml");
        }
        else {
            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = "false";
            xmlDoc.loadXML(str);
        }
        return xmlDoc;
    }
}

/// <summary>
/// Devolve a STRING contendo o XML usado no momento pelo gráfico do flash. 
/// </summary>
/// <returns type="string">String com o XML sobre o qual o gráfico flash está trabalhando.</returns>
function GetXmlDoGrafico() {
    var auxChart = getChartFromId("nodeChart");
    if (null == auxChart)
        return null;
    else {
        var xmlRtn = auxChart.getXMLData();
        return xmlRtn;
    }
}

function mesclaXMLs(xmlDocOrigem, xmlDocGraf) {
    /// <summary>
    /// Devolve um objeto DOM XML resultante da mesclagem dos dois objetos DOMs passados como parâmetro.
    /// O processo de mesclagem é feito especificamente para atender às necessidades do gráfico do workflow.
    /// </summary>
    /// <returns type="DOM XML">Objeto DOM XML contendo o xml mesclado.</returns>

    // atualiza as posições dos elementos gráficos que o usuário possa eventualmente ter arrastado na tela
    updateNodePositions(xmlDocOrigem, xmlDocGraf);
    return xmlDocOrigem;
}

function updateNodePositions(xmlDocOrigem, xmlDocGraf) {
    /// <summary>
    /// Devolve o parâmetro xmlDocOrigem com os atributos "x" e "y" de cada um dos elementos atualizados pelos valores
    /// dos elementos constantes no xmlDocGraf
    /// </summary>
    /// <returns type="DOM XML">Objeto DOM XML com os elementos atualizados.</returns>
    var setsOrigem = xmlDocOrigem.getElementsByTagName("set");
    var setsGraf = xmlDocGraf.getElementsByTagName("set");

    if (null != setsGraf) {
        var id, x, y;
        var wSet;

        for (i = 0; i < setsGraf.length; i++) {
            if (setsGraf[i].nodeType != 1)
                continue;

            id = setsGraf[i].getAttribute("id");
            x = setsGraf[i].getAttribute("x");
            y = setsGraf[i].getAttribute("y");

            wSet = getChildNodeById(setsOrigem, id)

            if (null != wSet) {
                wSet.setAttribute("x", x);
                wSet.setAttribute("y", y);
            }
        }
    }
}

function getChildNodeById(nodes, idToFind) {
    /// <summary>
    /// Devolve o primeiro nó na coleção 'nodes' que tenha um atributo 'id' que coincida com 
    /// o valor passado no parâmetro 'idToFind'
    /// .</summary>
    /// <param name="nodes" type="XML Nodes">Coleção, no formato XML, que contém os nós.</param>
    /// <param name="idToFind" type="string">valor do ID que será usado para localizar o nó procurado.</param>
    /// <returns type="XML Node">O nó localizado. (Objeto DOM).</returns>

    var node = null;

    if (null != nodes) {
        var nodeId;
        var i;

        for (i = 0; i < nodes.length; i++) {
            if (nodes[i].nodeType != 1)
                continue;

            nodeId = nodes[i].getAttribute("id");
            if (nodeId == idToFind)
                break;
        }

        // se o loop foi interrompido antes de varrer todos os nós, é sinal que localizou o nó procurado
        if (i < nodes.length)
            node = nodes[i];
    }

    return node;
}

/// <summary>
/// Devolve o primeiro nó na coleção 'nodes' que tenha o nome que coincida com 
/// o valor passado no parâmetro <paramref name="nameToFind"/>
/// .</summary>
/// <param name="nodes" type="XML Nodes">Coleção, no formato XML, que contém os nós.</param>
/// <param name="nameToFind" type="string">Nome que será usado para localizar o nó procurado.</param>
/// <returns type="XML Node">O nó localizado. (Objeto DOM).</returns>
function getChildNodeByName(nodes, nameToFind) {
    var node = null;

    if (null != nodes) {
        var name;
        var i;

        for (i = 0; i < nodes.length; i++) {
            name = nodes[i].nodeName;
            if (name == nameToFind)
                break;
        }

        // se o loop foi interrompido antes de varrer todos os nós, é sinal que localizou o nó procurado
        if (i < nodes.length)
            node = nodes[i];
    }

    return node;
}

/// <summary>
/// Devolve o próximo ID a ser usado para incluir, no gráfico, os elementos do tipo atividade, timer, início e fim
/// .</summary>
/// <param name="nodes" type="XML Nodes">Coleção, no formato XML, que contém os nós a serem varridos para 
/// identificação do próximo ID que deve ser usado.</param>
/// <returns type="string">ID do próximo elemento gráfico.</returns>
function getNextElementId(nodes) {

    var lastId = 0;
    var id;

    for (i = 0; i < nodes.length; i++) {
        if (nodes[i].nodeType != 1)
            continue;

        id = parseInt(nodes[i].getAttribute("id"));

        if ((null != id) && (id > lastId))
            lastId = id
    }

    lastId++;
    return lastId.toString();
}

function atualizaXmlDoGrafico(xmlDoc) {
    /// <summary>
    /// Atualiza o XML do gráfico, fazendo com que as alterações sejam 'enxergadas' 
    /// .</summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM XML contendo o XML a ser enviado ao gráfico flash.</param>
    /// <returns type="boolean">Se deu certo a atualização do gráfico, retorna true. Caso contrário, false.</returns>

    // atualiza o campo hidden field para que o código no servidor trabalhe com o xml atualizado.
    atualizaHiddenField(xmlDoc);

    // atualiza o xml do componente de desenho
    setXmlComponente();

    // atualiza as variáveis globais com o novo xml
    xmlWorkflowOk();
}

function atualizaHiddenField(xmlDoc) {
    /// <summary>
    /// Atualiza o hidden field da tela com o XML atual.
    /// .</summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM XML contendo o XML atualizado.</param>
    /// <returns type="boolean">Se deu certo a atualização do hidden field, retorna true. Caso contrário, false.</returns>

    var strXml;
    if (xmlDoc.xml) {
        strXml = xmlDoc.xml; // code for IE
    }
    else {
        strXml = (new XMLSerializer()).serializeToString(xmlDoc);
    }

    hfValoresTela.Set("XMLWF", strXml);

    return true;
}

function setTextToControl(control, text) {
    /// <summary>
    /// Atribui um texto a um controle. Antes de atribuir, verifica em qual propriedade irá atribuir, uma vez
    /// que cada browser trabalha com um tipo de propriedade diferente.
    /// </summary>
    /// <param name="control" type="DOM Object">Objeto DOM cuja propriedade 'texto' será alterada.</param>
    /// <param name="text" type="string">string contendo o texto a atribuir.</param>
    /// <returns type="boolean">Se deu certo a atribuição, retorna true. Caso contrário, false.</returns>

    var bRet = true;
    if ((null == control) || (null == text))
        return false;
    else {
        if (isIE)
            control.value = text;
        else
            control.textContent = text;
    }

    return bRet;
}

function getTextFromControl(control) {
    /// <summary>
    /// Devolve o texto de um controle. Antes de devolver, verifica em qual propriedade está o texto, uma vez
    /// que cada browser trabalha com um tipo de propriedade diferente.
    /// </summary>
    /// <param name="control" type="DOM Object">Objeto DOM cuja propriedade 'texto' será alterada.</param>
    /// <returns type="string">string com o texto do controle. Em caso de erro, retorna null.</returns>

    var text = null;
    if (null != control) {
        if (isIE)
            text = control.value;
        else
            text = control.textContent;
    }

    return text;
}


function setXmlComponente() {
    /// <summary>
    /// Busca o XML no hidden field hfValoresTela e o atribui ao componente flash. 
    /// Assume que haverá uma referência para o gráfico na  variável __wf_chartObj;
    /// </summary>
    /// <returns type="boolean">Se deu certo a atualização do hidden field, retorna true. Caso contrário, false.</returns>

    var strXml = hfValoresTela.Get("XMLWF");

    if ((null != strXml) && (strXml.length > 0)) {
        if (null != __wf_chartObj) {
            __wf_chartObj.setDataXML(strXml)
            return true;
        }
    }
}

function xmlFromXmlDoc(xmlDoc) {
    /// <summary>
    /// Devolve o XML do xmlDoc passado como parâmetro.
    /// </summary>
    /// <param name="xmlDoc" type="DOM XML">Objeto DOM do qual será extraído o XML.</param>
    /// <returns type="string">O XML do xmlDoc como string</returns>

    var strXml;
    if (null != xmlDoc) {
        if (window.XMLHttpRequest) {
            strXml = xmlDoc.xml; // code for IE
        }
        else {
            strXml = (new XMLSerializer()).serializeToString(xmlDoc);
        }
    }

    return strXml;
}

function onBtnSalvarClick(s, e) {
    /// <summary>
    /// Trata o 'click' no botão salvar na tela de workflow. 
    /// </summary>
    /// <param name="s" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
    /// <param name="e" type="xxx">parâmetro do evento click dos componentes DevExpress.</param>
    /// <returns type="boolean">Se deu certo a inclusão da nova atividade no xml, retorna true. Caso contrário, false.</returns>

    // se está tudo ok com o XML, 
    if (true == xmlWorkflowOk()) {   // atualiza o hidden field para gravar xml atualizado;

        atualizaHiddenField(__wf_xmlDocWorkflow);
        e.processOnServer = true;
    }
    else
        e.processOnServer = false;

    return e.processOnServer;
}


/// <summary>
/// função para devolver o nome de identificação de um elemento no gráfico. Se o elemento for uma etapa, 
/// devolve o atributo 'name', caso contrário, devolve o atributo 'tooltext'.
/// </summary>
/// <param name="elementNode" type="elementNode XML">Nó referente ao elemento do qual se deseja o nome.</param>
/// <returns type="string">O nome de identificação do elemento gráfico.</returns>
function getElementIdName(elementNode) {
    if ((null == elementNode) || (elementNode.nodeType != 1))
        return null;

    var tipo = obtemTipoElemento(elementNode);
    var elName;


    if (__wf_cTipoElementoEtapa == tipo)
        elName = elementNode.getAttribute("name");
    else
        elName = elementNode.getAttribute("toolText");

    return elName;
}

/// <summary>
/// Função para tratar o click no botão 'Publicar' do workflow. 
/// </summary>
/// <remarks>
/// Antes de 'autorizar' o processamento de publicar o servidor (e.processOnServer = true), esta função
/// valida toda a construção do workflow e coloca o xml do gráfico no formato final
/// </remarks>
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
var executaConfirmacao = true;
function onBtnPublicarXml_Click(s, e) {
    e.processOnServer = false;

    if (true == xmlWorkflowOk() && (true == normalizaXmlWorkflow()) &&
        (true == validaConstrucaoWorkflow()) && (true == validaEntradaDadosWf())) {

        if (executaConfirmacao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    executaConfirmacao = false;
                    s.DoItemClick(e.item.indexPath, false, null);
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_ao_publicar_uma_vers_o__esta_passa_a_ser_a_vers_o_atual_para_este_modelo_de_fluxo__confirma_a_publica__o_, function () { funcObj['funcaoClickOK'](s, e) }, null);
        }
        else {
            atualizaHiddenField(__wf_xmlDocWorkflow);
            executaConfirmacao = true;
            e.processOnServer = true;
        }
    }

    return e.processOnServer;
}

/// <summary>
/// Função para gravar informações adicionais no XML, através da análise das conexões dos elementos. 
/// </summary>
/// <remarks>
/// normalizar o xml do workflow. Por normalizar, entende-se registrar corretamente 
/// a seção 'workflows' no xml e gravar os atributos adicionais para cada elemento gráfico, 
/// tais como 'grupoWorkflow' e 'idElementoInicioFluxo'.
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <returns type="boolean">Se tudo deu certo na normalização, retorna true. Caso contrário, false.</returns>
function normalizaXmlWorkflow() {
    // limpa o nó 'workflows' do xml.
    clearWorkflowsNode();

    // limpa o conteúdo do atributo grupoWorkflow dos elementos gráficos
    clearWorkflowGroupAttribute();

    // reprocessa cada ligação de elementos começando do elemento início
    processaConexoesElementos(__wf_cIdElementoInicio, __wf_cIdElementoInicio);

    var bOk = true;
    return bOk;
}

/// <summary>
/// Função para limpar o conteúdo do "nó" <workflows> nos xml.
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <returns type="boolean">Se tudo deu certo na 'limpeza', retorna true. Caso contrário, false.</returns>
function clearWorkflowsNode() {
    var bOk = true;
    var sWf = __wf_xmlDocWorkflow.getElementsByTagName("workflows")[0];
    __wf_xmlDocWorkflow.documentElement.removeChild(sWf);
    var ds = __wf_xmlDocWorkflow.createElement("workflows");
    ds.setAttribute("xmlVersion", __wf_versaoFormatoXmlAtual);
    __wf_xmlDocWorkflow.documentElement.appendChild(ds);
    return bOk;
}


/// <summary>
/// Função para reprocessar as conexões dos elementos gráficos do xml. 
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <returns type="boolean">Se tudo deu certo no reprocessamento, retorna true. Caso contrário, false.</returns>
function processaConexoesElementos(elementoId, originalId) {
    var bOk = true;

    var to, from;
    var setaInicio, setaFim;
    var fromId, toId;
    var ne, grupoWf;

    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        from = __wf_nlConectores[i].getAttribute("from");
        to = __wf_nlConectores[i].getAttribute("to");

        setaInicio = __wf_nlConectores[i].getAttribute("arrowAtStart");
        if (null == setaInicio)
            setaInicio = "0";

        setaFim = __wf_nlConectores[i].getAttribute("arrowAtEnd");
        if (null == setaFim)
            setaFim = "0";

        fromId = null;
        toId = null;

        if ((null != from) && (null != to)) {
            if ((from == elementoId) && ("1" == setaFim)) {
                fromId = from;
                toId = to;
            }
            else if ((to == elementoId) && ("1" == setaInicio)) {
                fromId = to;
                toId = from;
            }
        } // if ( (null != from) && ...

        // se for uma conexão 'saindo' do elemento em questão e não estiver indo para o originalId -> que é um das condições de paradas
        if ((null != fromId) && (fromId == elementoId) && (toId != originalId)) {
            // verifica se o destino já não tem o atributo grupoWorkflow
            ne = getChildNodeById(__wf_nlElementos, toId);
            if (null != ne) {
                grupoWf = ne.getAttribute("grupoWorkflow");
                if ((null != grupoWf) && ("0" == grupoWf)) {
                    // caso o grupo de workflow do destino ainda esteja com o valor "0", atualiza os atributos adicionais e 
                    // passa a processar as ligações do destino;
                    atualizaXmlConexaoElementos(fromId, toId);
                    processaConexoesElementos(toId, originalId)
                } // if ( (null!=grupoWf) && ...
            } // if (null != ne)
        }  // if ( (null!=fromId) && ...
    } // for (var i=0; i<__wf_nlConectores; i++)

    return bOk;
}



/// <summary>
/// Função para limpar os atributos "nó" <workflows> nos xml.
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_nlElementos' contém os nós XML dos elementos gráficos do workflow 
/// mostrado na tela.
/// </remarks>
/// <returns type="boolean">Se tudo deu certo na 'limpeza', retorna true. Caso contrário, false.</returns>
function clearWorkflowGroupAttribute() {
    var bOk = true;

    for (var i = 0; i < __wf_nlElementos.length; i++) {
        if (__wf_nlElementos[i].nodeType != 1)
            continue;

        __wf_nlElementos[i].setAttribute("grupoWorkflow", "0");
    }

    return bOk;
}



/// <summary>
/// Verificar se as informações entradas para a composição do workflow estão ok.
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <returns type="boolean">Se as informações estão ok, retorna true. Caso contrário, false.</returns>
function validaEntradaDadosWf() {
    var bOk;

    bOk = validaInformacoesEtapas();
    return bOk;
}

/// <summary>
/// Verificar se as informações entradas para as etapas estão ok.
/// </summary>
/// <remarks>
/// Verifica se cada etapa tem pelo menos um grupo com acesso para 'Ação'
/// verifica se cada etapa tem pelo menos uma ação que seja do tipo 'u' -> ação de usuário;
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <returns type="boolean">Se as informações estão ok, retorna true. Caso contrário, false.</returns>
function validaInformacoesEtapas() {
    var bOk = true;
    var ne, groups, group, acoes, acao;
    var comAcesso, accessType;
    var comAcao, actionType;

    for (var i = 0; i < __wf_nlElementos.length; i++) {
        ne = __wf_nlElementos[i];

        if ((true == ehElementoDoTipo(ne, __wf_cTipoElementoEtapa)) && (false == f_ehElementoSubProcesso(__wf_nlElementos[i]))) {

            if (true == bOk) {
                comAcesso = false;

                groups = getChildNodeByName(ne.childNodes, "gruposComAcesso");
                if (null != groups) {
                    for (var j = 0; j < groups.childNodes.length; j++) {
                        group = groups.childNodes[j];
                        if (group.nodeType != 1)
                            continue;

                        accessType = group.getAttribute("accessType");

                        if (traducao.WorkflowCharts_a__o == accessType) {
                            comAcesso = true;
                            break;
                        } // if ( "Ação" == accessType) 
                    } // for (var j=0; j<groups.childNodes.length; j++)
                } // if(null != groups) - by Alejandro 9/07/2010
                if (false == comAcesso) {
                    var msg = traducao.WorkflowCharts_aten__o____n_o_foi_informado_nenhum_grupo_com_acesso_de_a__o_para_a_etapa + " [" + ne.getAttribute("name") + "].";
                    window.top.mostraMensagem(msg, 'atencao', true, false, null);
                    bOk = false;
                } // if (false == comAcao)
            } // if ( true == bOk )

            if (true == bOk) {

                comAcao = false;
                acoes = getChildNodeByName(ne.childNodes, "acoes");
                for (var j = 0; j < acoes.childNodes.length; j++) {
                    acao = acoes.childNodes[j];
                    if (acao.nodeType != 1)
                        continue;

                    actionType = acao.getAttribute("actionType");

                    if ("U" == actionType) {
                        comAcao = true;
                        break;
                    } // if ( "Ação" == accessType) 
                } // for (var j=0; j<groups.childNodes.length; j++)

                if (false == comAcao) {
                    var msg = traducao.WorkflowCharts_a_etapa + " [" + ne.getAttribute("name") + "] " + traducao.WorkflowCharts_n_o_possui_nenhuma_op__o_onde_as_pessoas_possam_interagir_;
                    window.top.mostraMensagem(msg, 'atencao', true, false, null);
                    bOk = false;
                } // if (false == comAcao)
            } // if ( true == bOk )

            // se foi encontrado algum erro, já interrompe o processamento            
            if (false == bOk)
                break;
        } // if( true == ehElementoDoTipo(ne, __wf_cTipoElementoEtapa) )
    } // for (var i = 0; i < __wf_nlElementos.length; i++)

    return bOk;
}


/// <summary>
/// Verificar se o 'desenho' montado do work flow, está coerente, de acordo com as regras gráficas
/// constantes dos requisitos de workflow.
/// </summary>
/// <remarks>
/// Assume que a variável global '__wf_xmlDocWorkflow' contém o XML 'de trabalho' para o gráfico de workflow 
/// mostrado na tela.
/// </remarks>
/// <returns type="boolean">Se o desenho está ok, retorna true. Caso contrário, false.</returns>
function validaConstrucaoWorkflow() {
    var bOk = true;
    if (false == frv_existeElementoDoTipo(__wf_cTipoElementoFim)) {
        window.top.mostraMensagem(traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__elemento_fim_n_o_encontrado_, 'atencao', true, false, null);
        bOk = false;
    }

    if (true == bOk)
        bOk = frv_todosElementosConectados();

    if (true == bOk)
        bOk = !frv_existeConexoesForaDoFluxo();

    if (true == bOk)
        bOk = !frv_existeTimerLigadoASubprocesso();

    if (true == bOk)
        bOk = !frv_existeTimerLigadoAEtapaInicial();

    if (true == bOk)
        bOk = !frv_existeDisjuncaoLigadoAJuncao();

    if (true == bOk)
        bOk = frv_ligacoesAosElementosFinsOk();

    if (true == bOk)
        bOk = frv_formulariosEtapasOk();


    return bOk;
}

/// <summary>
/// Verifica se existe referência entre formulários e etapas.
/// </summary>
/// <returns type="boolean">Se existir, retorna true. Caso contrário, false.</returns>
function frv_formulariosEtapasOk() {
    var bOk = true;

    for (var i = 0; i < __wf_nlElementos.length; i++) {
        if (__wf_nlElementos[i].nodeType != 1)
            continue;

        elName = getElementIdName(__wf_nlElementos[i]);
        qtdFormsRequerAssinatura = frv_qtdFormulariosRequerAssinatura(__wf_nlElementos[i], -1);
        qtdAcoesSolicitaAssinatura = frv_qtdAcoesSolicitaAssinatura(__wf_nlElementos[i]);

        if (qtdFormsRequerAssinatura > 0 && 0 === qtdAcoesSolicitaAssinatura) {
            bOk = false;
            window.top.mostraMensagem(traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__existe_m__formul_rio_s__que_requer_em__assinatura_digital_na_etapa__ + elName + traducao.WorkflowCharts___e_nenhum_conector_solicita_assinatura_digital_, 'atencao', true, false, null);
            break;
        } else if (0 === qtdFormsRequerAssinatura && qtdAcoesSolicitaAssinatura > 0) {
            bOk = false;
            window.top.mostraMensagem(traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__existe_m__a__o__es__que_solicita_m__assinatura_digital_na_etapa__ + elName + traducao.WorkflowCharts___mas_nenhum_formul_rio_desta_etada_requer_assinatura_digital_, 'atencao', true, false, null);
            break;
        }


        formularios = getChildNodeByName(__wf_nlElementos[i].childNodes, "formularios");
        if (formularios != null) {
            for (var j = 0; j < formularios.childNodes.length; j++) {
                if (formularios.childNodes[j].nodeType != 1)
                    continue;

                id = formularios.childNodes[j].getAttribute('originalStageId');
                etapaNode = getChildNodeById(__wf_nlElementos, id);

                if (etapaNode == null) // -> problema!!!!
                {
                    bOk = false;
                    window.top.mostraMensagem(traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__os_formul_rios_da_etapa__ + elName + traducao.WorkflowCharts___referem_se_a_etapas_que_n_o_mais_existem_no_modelo_, 'atencao', true, false, null);
                    break; // sai do for(var j = 0 ...
                }
            }
            if (!bOk)
                break; //sai do for(var i = 0 ...
        }
    }
    return bOk;
}


/// <summary>
/// Conta a quantidade de formulários da etapa que possuem o atributo "RequerAssinaturaDigital" marcado como SIM  ("1")
/// </summary>
/// <returns type="int">Retorna a quantidade de formulários com este atributo ligado.</returns>
function frv_qtdFormulariosRequerAssinatura(__wf_Etapa, nCodEtapa) {
    var qtdFormsRequerAssinatura = 0;

    if (nCodEtapa > -1) {
        for (var i = 0; i < __wf_nlElementos.length; i++) {
            if ((__wf_nlElementos[i].nodeType === 1) && (__wf_nlElementos[i].getAttribute("id") === nCodEtapa)) {
                __wf_Etapa = __wf_nlElementos[i];
                break;
            }
        }
    }


    if (__wf_Etapa != null) {
        formularios = getChildNodeByName(__wf_Etapa.childNodes, "formularios");
        if (formularios != null) {
            for (var j = 0; j < formularios.childNodes.length; j++) {
                if (formularios.childNodes[j].nodeType != 1)
                    continue;

                requerAssinatura = formularios.childNodes[j].getAttribute('requerAssinaturaDigital');
                if ("1" === requerAssinatura)
                    qtdFormsRequerAssinatura++;

            }
        }
    }
    return qtdFormsRequerAssinatura;
}


/// <summary>
/// Conta a quantidade de Ações (conectores) da etapa que possuem o atributo "SolicitaAssinaturaDigital" marcado como SIM  ("1")
/// </summary>
/// <returns type="int">Retorna a quantidade de ações com este atributo ligado.</returns>
function frv_qtdAcoesSolicitaAssinatura(__wf_Etapa) {
    var qtdAcoesSolicitaAssinatura = 0;

    acoes = getChildNodeByName(__wf_Etapa.childNodes, "acoes");
    if (acoes != null) {
        for (var j = 0; j < acoes.childNodes.length; j++) {
            if (acoes.childNodes[j].nodeType != 1)
                continue;

            solicitaAssinatura = acoes.childNodes[j].getAttribute('solicitaAssinaturaDigital');
            if ("1" === solicitaAssinatura)
                qtdAcoesSolicitaAssinatura++;

        }
    }
    return qtdAcoesSolicitaAssinatura;
}





/// <summary>
/// Verificar se existe algum elemento do tipo FIM no gráfico.
/// </summary>
/// <returns type="boolean">Se existir, retorna true. Caso contrário, false.</returns>
function frv_existeElementoDoTipo(tipo) {
    var bExiste = false;

    for (var i = 0; i < __wf_nlElementos.length; i++) {
        if (true == ehElementoDoTipo(__wf_nlElementos[i], tipo)) {
            bExiste = true;
            break;
        } // if (true == ehElementoDoTipo ...
    } // for (var i...

    return bExiste;
}

/// <summary>
/// Função para criar um novo objeto ligações. 
/// </summary>
/// <returns type="void"></returns>
function xLigacoes() {
    this.entradas = 0;
    this.saidas = 0;
}

/// <summary>
/// Verificar se o todos os elementos do gráfico estão 'devidamente' conectados conforme a regra:
/// 1. Os eleme
/// constantes dos requisitos de workflow.
/// Assume que a variável __wf_
/// </summary>
/// <returns type="boolean">Se o desenho está ok, retorna true. Caso contrário, false.</returns>
function frv_todosElementosConectados() {
    var id;
    var bOk = true;
    var tipoElemento;
    var elName;
    var ligacoes = new xLigacoes();

    for (var i = 0; i < __wf_nlElementos.length; i++) {
        if (__wf_nlElementos[i].nodeType != 1)
            continue;

        tipoElemento = obtemTipoElemento(__wf_nlElementos[i]);
        id = __wf_nlElementos[i].getAttribute("id");
        if (null != id) {
            obtemQuantidadeLigacoesElementos(id, ligacoes);

            if (__wf_cTipoElementoInicio == tipoElemento)
                bOk = ((0 == ligacoes.entradas) && (1 == ligacoes.saidas));
            else if (__wf_cTipoElementoFim == tipoElemento)
                bOk = ((ligacoes.entradas > 0) && (0 == ligacoes.saidas));
            else if (__wf_cTipoElementoEtapa == tipoElemento) {
                if (f_ehElementoSubProcesso(__wf_nlElementos[i]) == true)
                    bOk = ((ligacoes.entradas > 0) && (ligacoes.saidas == 1));
                else
                    bOk = ((ligacoes.entradas > 0) && (ligacoes.saidas > 0));
            }
            else if (__wf_cTipoElementoTimer == tipoElemento)
                bOk = ((ligacoes.entradas == 1) && (ligacoes.saidas == 1));
            else if (__wf_cTipoElementoDisjuncao == tipoElemento)
                bOk = ((ligacoes.entradas > 0) && (ligacoes.saidas > 1));
            else if (__wf_cTipoElementoJuncao == tipoElemento)
                bOk = ((ligacoes.entradas > 1) && (ligacoes.saidas == 1));
            else if (__wf_cTipoElementoCondicao == tipoElemento)
                bOk = ((ligacoes.entradas == 1) && (ligacoes.saidas > 1));

            if (false == bOk) { // se há algum problema com as conexões do um elemento 
                // se for etapa, pega pelo nome, caso contrário, pelo toolText
                elName = getElementIdName(__wf_nlElementos[i]);
                window.top.mostraMensagem(traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__as_conex_es_do_item__ + elName + traducao.WorkflowCharts___est_o_inconsistentes_, 'atencao', true, false, null);
                break; // sai do for;
            } // if (false == bOK)
        } // (null != id)

    } // for (var i...

    return bOk;
}

/// <summary>
/// Função para obter a quantidade de ligações que possui um elemento. Registra na variável 'ligacoes' 
/// as quantidades de ligações de entrada e saida.
/// </summary>
/// <param name="elementoId" type="string">id do elemento que deseja verificar.</param>
/// <returns type="boolean">Se o elemento tem as ligações de entrada e saída, retorna true. Caso contrário, false.</returns>
function obtemQuantidadeLigacoesElementos(elementoId, ligacoes) {
    var bOk = true;
    var from;
    var to;
    var setaInicio;
    var setaFim;

    ligacoes.entradas = 0;
    ligacoes.saidas = 0;

    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        from = __wf_nlConectores[i].getAttribute("from");
        to = __wf_nlConectores[i].getAttribute("to");

        setaInicio = __wf_nlConectores[i].getAttribute("arrowAtStart");
        if (null == setaInicio)
            setaInicio = "0";

        setaFim = __wf_nlConectores[i].getAttribute("arrowAtEnd");
        if (null == setaFim)
            setaFim = "0";

        if ((null != from) && (null != to)) {
            if (from == elementoId) {
                if ("1" == setaInicio)
                    ligacoes.entradas++;

                if ("1" == setaFim)
                    ligacoes.saidas++;

            } // if (from == elementoId)
            if (to == elementoId) {
                if ("1" == setaInicio)
                    ligacoes.saidas++;

                if ("1" == setaFim)
                    ligacoes.entradas++;
            } // if (to == elementoId)
        } // if ( (null != from) && ...
    } // for (var i=0; i<__wf_nlConectores; i++)

    return;
}

/// <summary>
/// Verificar se existe as ligações aos elementos 'fins' são todas 'pertinentes'
/// </summary>
/// <remarks>
/// Não é permitido ligar a um elemento fim os seguintes elementos:
/// 1. O elemento 'Início'
/// 2. Os elementos do tipo 'disjunção'
/// 3. Qualquer elementos dentro de um subfluxo
/// </remarks>
/// <returns type="boolean">Se existir, retorna true. Caso contrário, false.</returns>
function frv_ligacoesAosElementosFinsOk() {
    var bLigacoesOk;

    var msgErro = null;

    var from, to;
    var fromNode;
    var idInicioFluxo_from;
    var fromType, toType;
    var elName;

    // varre as conexões verificando, dentre as conexões com os elementos 'fins', se alguma conexão é imprópria
    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        if (null == msgErro) {
            to = __wf_nlConectores[i].getAttribute("to");

            if (null == to)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.1)";
        } // if (null == msgErro)

        if (null == msgErro) {
            toType = obtemTipoElemento(getChildNodeById(__wf_nlElementos, to));

            if (null == toType)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.2)";
        } // if (null == msgErro)

        if (null == msgErro) {
            if (__wf_cTipoElementoFim == toType) {
                from = __wf_nlConectores[i].getAttribute("from");
                if (null == from)
                    msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.3)";

                if (null == msgErro) {
                    fromNode = getChildNodeById(__wf_nlElementos, from);
                    if (null == fromNode)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.4)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    fromType = obtemTipoElemento(fromNode);
                    if (null == fromType)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.5)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    // para conexões cujo 'from' é um timer, o 'from' a considerar é o 'from' do timer e
                    // não o timer em si
                    if (__wf_cTipoElementoTimer == fromType)
                        from = getFirstFromID(from);

                    if (null == from)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.6)";

                    if (null == msgErro) {
                        fromNode = getChildNodeById(__wf_nlElementos, from);
                        if (null == fromNode)
                            msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.7)";
                    } // if (null == msgErro)

                    if (null == msgErro) {
                        fromType = obtemTipoElemento(fromNode);
                        if (null == fromType)
                            msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.9)";
                    } // if (null == msgErro)

                } // if (null == msgErro)

                if (null == msgErro) {
                    idInicioFluxo_from = fromNode.getAttribute("idElementoInicioFluxo");

                    if (null == idInicioFluxo_from)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0030.10)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    elName = getElementIdName(fromNode);
                    if (null == elName)
                        elName = "";

                    // O id do elemento que inicia o fluxo de quem está ligado ao elemento fim tem que ser '0' -> id do elemento 'início'
                    if ("0" == idInicioFluxo_from) {
                        // não se pode conectar início ou disjunção a um elemento 'Fim'
                        if ((__wf_cTipoElementoInicio == fromType) || (__wf_cTipoElementoDisjuncao == fromType))
                            msgErro = traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__o_item__ + elName + traducao.WorkflowCharts___n_o_pode_ser_conectado_a_um__fim__;
                    } // if ("0" == idInicioFluxo_from)
                    else {
                        msgErro = traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__o_item__ + elName +
                            traducao.WorkflowCharts____por_estar_dentro_de_um_fluxo_iniciado_por_uma_disjun__o__n_o_pode_ser_conectado_a_um__fim__;
                    } // else ("0" == idInicioFluxo_from)
                } // if (null == msgErro)
            } // if (__wf_cTipoElementoFim == toType)
        } // if (null == msgErro) 

        // sai do loop caso tenha dado algum erro
        if (null != msgErro)
            break;
    } // for (var i...

    if (null == msgErro)
        bLigacoesOk = true;
    else {
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);
        bLigacoesOk = false;
    }

    return bLigacoesOk;
}

/// <summary>
/// Verificar se existe alguma disjunção ligad diretamente a uma junção
/// </summary>
/// <returns type="boolean">Se existir, retorna true. Caso contrário, false.</returns>
function frv_existeDisjuncaoLigadoAJuncao() {
    // se não existir elemento disjunção, não há como haver elementos com inícios diferentes;
    if (false == frv_existeElementoDoTipo(__wf_cTipoElementoDisjuncao))
        return false;

    var bExiste;

    var msgErro = null;

    var from, to;
    var fromNode, toNode;
    var fromType, toType;
    var elNameFrom, elNameTo;

    // varre as conexões verificando, dentre as conexões com os elementos 'fins', se alguma conexão é imprópria
    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        if (null == msgErro) {
            to = __wf_nlConectores[i].getAttribute("to");

            if (null == to)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0031.1)";
        } // if (null == msgErro)

        if (null == msgErro) {
            toNode = getChildNodeById(__wf_nlElementos, to);
            if (null == toNode)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0031.2)";

        } // if (null == msgErro)

        if (null == msgErro) {
            toType = obtemTipoElemento(toNode);

            if (null == toType)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0031.3)";
        } // if (null == msgErro)

        if (null == msgErro) {
            if (__wf_cTipoElementoJuncao == toType) {
                from = __wf_nlConectores[i].getAttribute("from");
                if (null == from)
                    msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0031.4)";

                if (null == msgErro) {
                    fromNode = getChildNodeById(__wf_nlElementos, from);
                    if (null == fromNode)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0031.5)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    fromType = obtemTipoElemento(fromNode);
                    if (null == fromType)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0031.6)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    elNameFrom = getElementIdName(fromNode);
                    if (null == elNameFrom)
                        elNameFrom = "";

                    elNameTo = getElementIdName(toNode);
                    if (null == elNameTo)
                        elNameTo = "";

                    // não se pode conectar disjunção a um elemento 'junção'
                    if (__wf_cTipoElementoDisjuncao == fromType)
                        msgErro = traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__o_item__ + elNameFrom + traducao.WorkflowCharts___n_o_pode_ser_conectado_ao_item__ + elNameTo + "'.";
                } // if (null == msgErro)
            } // if (__wf_cTipoElementoFim == toType)
        } // if (null == msgErro) 

        // sai do loop caso tenha dado algum erro
        if (null != msgErro)
            break;
    } // for (var i...

    if (null == msgErro)
        bExiste = false;
    else {
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);
        bExiste = true;
    }

    return bExiste;
}


/// <summary>
/// Verificar se existe algum timer ligado diretamente a um subprocesso
/// </summary>
/// <returns type="boolean">Se existir, retorna true. Caso contrário, false.</returns>
function frv_existeTimerLigadoASubprocesso() {
    // se não existir elemento disjunção, não há como haver elementos com inícios diferentes;
    if (false == frv_existeElementoDoTipo(__wf_cTipoElementoTimer))
        return false;

    var bExiste;

    var msgErro = null;

    var from, to;
    var fromNode, toNode;
    var fromType, toType;
    var elNameFrom, elNameTo;

    // varre as conexões verificando, dentre as conexões com os elementos 'fins', se alguma conexão é imprópria
    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        if (null == msgErro) {
            to = __wf_nlConectores[i].getAttribute("to");

            if (null == to)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.1)";
        } // if (null == msgErro)

        if (null == msgErro) {
            toNode = getChildNodeById(__wf_nlElementos, to);
            if (null == toNode)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.2)";

        } // if (null == msgErro)

        if (null == msgErro) {
            toType = obtemTipoElemento(toNode);

            if (null == toType)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.3)";
        } // if (null == msgErro)

        if (null == msgErro) {
            if (__wf_cTipoElementoTimer == toType) {
                from = __wf_nlConectores[i].getAttribute("from");
                if (null == from)
                    msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.4)";

                if (null == msgErro) {
                    fromNode = getChildNodeById(__wf_nlElementos, from);
                    if (null == fromNode)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.5)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    fromType = obtemTipoElemento(fromNode);
                    if (null == fromType)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.6)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    elNameFrom = getElementIdName(fromNode);
                    if (null == elNameFrom)
                        elNameFrom = "";

                    elNameTo = getElementIdName(toNode);
                    if (null == elNameTo)
                        elNameTo = "";

                    // não se pode colocar timer num subprocesso
                    if (__wf_cTipoElementoEtapa == fromType) {
                        if (f_ehElementoSubProcesso(toNode) == true) {
                            msgErro = traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__o_item__ + elNameFrom + traducao.WorkflowCharts___n_o_pode_ser_conectado_ao_item__ + elNameTo + "'.";
                        }
                    }// if (__wf_cTipoElementoDisjuncao == 
                } // if (null == msgErro)
            } // if (__wf_cTipoElementoFim == toType)
        } // if (null == msgErro) 

        // sai do loop caso tenha dado algum erro
        if (null != msgErro)
            break;
    } // for (var i...

    if (null == msgErro)
        bExiste = false;
    else {
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);
        bExiste = true;
    }

    return bExiste;
}

/// <summary>
/// Verificar se existe algum timer ligado diretamente a um subprocesso
/// </summary>
/// <returns type="boolean">Se existir, retorna true. Caso contrário, false.</returns>
function frv_existeTimerLigadoAEtapaInicial() {
    // se não existir elemento Timer, não há como haver elementos com inícios diferentes;
    if (false == frv_existeElementoDoTipo(__wf_cTipoElementoTimer))
        return false;
    var bExiste;

    var msgErro = null;

    var from, to;
    var fromNode, toNode;
    var fromType, toType;
    var elNameFrom, elNameTo;

    // varre as conexões verificando, dentre as conexões com os elementos 'fins', se alguma conexão é imprópria
    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (null == msgErro) {
            to = __wf_nlConectores[i].getAttribute("to");

            if (null == to)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.1)";
        } // if (null == msgErro)

        if (null == msgErro) {
            toNode = getChildNodeById(__wf_nlElementos, to);
            if (null == toNode)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.2)";

        } // if (null == msgErro)

        if (null == msgErro) {
            toType = obtemTipoElemento(toNode);

            if (null == toType)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.3)";
        } // if (null == msgErro)

        if (null == msgErro) {
            if (__wf_cTipoElementoTimer == toType) {
                from = __wf_nlConectores[i].getAttribute("from");
                if (null == from)
                    msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.4)";

                if (null == msgErro) {
                    fromNode = getChildNodeById(__wf_nlElementos, from);
                    if (null == fromNode)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.5)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    fromType = obtemTipoElemento(fromNode);
                    if (null == fromType)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.6)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    elNameFrom = getElementIdName(fromNode);
                    if (null == elNameFrom)
                        elNameFrom = "";

                    elNameTo = getElementIdName(toNode);
                    if (null == elNameTo)
                        elNameTo = "";

                    // não se pode colocar timer numa etapa inicial
                    if (__wf_cTipoElementoEtapa == fromType) {
                        if (f_ehElementoEtapaInicial(fromNode) == true) {
                            msgErro = 'Uma etapa que tenha um timer associado, não pode ser configurada como a etapa inicial do fluxo.';
                        }
                    }// if (__wf_cTipoElementoDisjuncao == 
                } // if (null == msgErro)
            } // if (__wf_cTipoElementoFim == toType)
        } // if (null == msgErro) 

        // sai do loop caso tenha dado algum erro
        if (null != msgErro)
            break;
    } // for (var i...

    if (null == msgErro)
        bExiste = false;
    else {
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);
        bExiste = true;
    }

    return bExiste;
}


/// <summary>
/// Verificar se existe algum timer ligado diretamente a uma etapa e esta etapa seja um timer.
/// </summary>
/// <returns type="boolean">Se existir, retorna true. Caso contrário, false.</returns>
function frv_existeTimerLigadoAEtapa(idEtapa, NomeEtapa) {

    // se não existir elemento Timer, não há como haver elementos com inícios diferentes;
    if (false == frv_existeElementoDoTipo(__wf_cTipoElementoTimer))
        return false;
    var bExiste;

    var msgErro = null;

    var from, to;
    var fromNode, toNode;
    var fromType, toType;
    var elNameFrom, elNameTo;

    // varre as conexões verificando, dentre as conexões com os elementos 'fins', se alguma conexão é imprópria
    for (var i = 0; i < __wf_nlConectores.length; i++) {
        //if (__wf_nlConectores[i].nodeType != 1)
        //    continue;

        if (null == msgErro) {
            to = __wf_nlConectores[i].getAttribute("to");

            if (null == to)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.1)";
        } // if (null == msgErro)

        if (null == msgErro) {
            toNode = getChildNodeById(__wf_nlElementos, to);
            if (null == toNode)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.2)";

        } // if (null == msgErro)

        if (null == msgErro) {
            toType = obtemTipoElemento(toNode);

            if (null == toType)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.3)";
        } // if (null == msgErro)

        if (null == msgErro) {
            if (__wf_cTipoElementoTimer == toType) {
                from = __wf_nlConectores[i].getAttribute("from");
                if (null == from)
                    msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.4)";

                if (null == msgErro) {
                    fromNode = getChildNodeById(__wf_nlElementos, from);
                    if (null == fromNode)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.5)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    fromType = obtemTipoElemento(fromNode);
                    if (null == fromType)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0032.6)";
                } // if (null == msgErro)

                if (null == msgErro) {
                    elNameFrom = getElementIdName(fromNode);
                    if (null == elNameFrom)
                        elNameFrom = "";

                    elNameTo = getElementIdName(toNode);
                    if (null == elNameTo)
                        elNameTo = "";

                    // não se pode colocar timer numa etapa inicial
                    if (__wf_cTipoElementoEtapa == fromType) {
                        if (elNameFrom == idEtapa.toString() + '. ' + NomeEtapa)
                            msgErro = 'Uma etapa que tenha um timer associado, não pode ser configurada como a etapa inicial do fluxo.';
                    }// if (__wf_cTipoElementoDisjuncao ==
                } // if (null == msgErro)
            } // if (__wf_cTipoElementoFim == toType)
        } // if (null == msgErro) 

        // sai do loop caso tenha dado algum erro
        if (null != msgErro)
            break;
    } // for (var i...

    if (null == msgErro)
        bExiste = false;
    else {
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);
        bExiste = true;
    }
    return bExiste;

}
/// <summary>
/// Verifica se existe conexões ligando elementos que estão em fluxos diferentes.
/// </summary>
/// <remarks>
/// </remarks>
/// <returns type="boolean">Se existir, retorna true. Caso contrário, false.</returns>
/// último código de erro interno desta função: 20.13
function frv_existeConexoesForaDoFluxo() {
    // se não existir elemento disjunção, não há como haver elementos com inícios diferentes;
    if (false == frv_existeElementoDoTipo(__wf_cTipoElementoDisjuncao))
        return false;

    var bExiste = false;
    var msgErro = null;
    var from, to;
    var elNameTo;
    var fromNode, toNode;
    var fromType, toType;

    /// <summary>
    /// IDs a comparar para o elemento de origem e destino da conexão
    /// De acordo com os tipos do elementos conectados, serão comparados informações diferentes.
    /// Entre etapas, compara-se o atributo 'grupoWorkflow' de cada uma, se o destino for uma junção, 
    /// compara-se o atributo 'idElementoInicioFluxo' de cada um...
    /// </summary>
    var fromCompareID, toCompareID;


    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        if (null == msgErro) {
            from = __wf_nlConectores[i].getAttribute("from");
            if (null == from)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.1)";
        } // if (null == msgErro)

        if (null == msgErro) {
            fromNode = getChildNodeById(__wf_nlElementos, from);
            if (null == fromNode)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.2)";
        } // if (null == msgErro)

        if (null == msgErro) {
            fromType = obtemTipoElemento(fromNode);
            if (null == fromType)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.3)";
        } // if (null == msgErro)

        if (null == msgErro) {
            // conexões cujo from é um timer não são avalidadas. Tratando-se de conexões 
            // do elemento Timer, são avaliadas apenas as conexões onde o timer é o destino, e, 
            // nestes casos, o destino considerado será o destino do timer e não o timer em si
            if (__wf_cTipoElementoTimer == fromType)
                continue;
        } // if (null == msgErro)

        if (null == msgErro) {
            to = __wf_nlConectores[i].getAttribute("to");
            if (null == to)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.4)";
        } // if (null == msgErro)

        if (null == msgErro) {
            toNode = getChildNodeById(__wf_nlElementos, to);
            if (null == fromNode)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.5)";
        } // if (null == msgErro)

        if (null == msgErro) {
            toType = obtemTipoElemento(toNode);
            if (null == fromType)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.6)";
        } // if (null == msgErro)

        if (null == msgErro) {
            // para conexões cujo 'to' é um timer, o 'to' a considerar é o 'to' do timer e
            // não o timer em si
            if (__wf_cTipoElementoTimer == toType)
                to = getFirstToID(to);

            if (null == to)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.11)";

            if (null == msgErro) {
                toNode = getChildNodeById(__wf_nlElementos, to);
                if (null == fromNode)
                    msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.12)";
            } // if (null == msgErro)

            if (null == msgErro) {
                toType = obtemTipoElemento(toNode);
                if (null == fromType)
                    msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.13)";
            } // if (null == msgErro)

        } // if (null == msgErro)

        if (null == msgErro) {
            if ((__wf_cTipoElementoInicio == fromType) || (__wf_cTipoElementoDisjuncao == fromType)) {
                // se a origem for o início ou uma disjunção, considera que a ligação é válida
                // pois a ligação oriunda desses elementos sempre iniciam fluxos
                fromCompareID = "1";
                toCompareID = "1";
            } // if ( (__wf_cTipoElementoInicio == fromType) || (__wf_cTipoElementoDisjuncao == fromType) )
            else if (__wf_cTipoElementoJuncao == toType) {
                // se o destino for uma junção, serão comparados os atributos 'idElementoInicioFluxo' 
                // de cada um (origem e destino)
                fromCompareID = fromNode.getAttribute("idElementoInicioFluxo");
                toCompareID = toNode.getAttribute("idElementoInicioFluxo");
            } // else if (__wf_cTipoElementoJuncao == toType)
            else if (__wf_cTipoElementoJuncao == fromType) {
                // se a origem for uma junção (obviamente, desde que o destino não seja uma junção, pois neste caso, 
                // entraria no if acima), serão comparados os atributos 'grupoWorkflow', porém o valor deste atributo 
                // para a junção (origem obviamente) será o atributo da disjunção que inicia o fluxo em que está a junção

                var auxId = fromNode.getAttribute("idElementoInicioFluxo");
                if (null == auxId)
                    msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.7)";
                else {
                    var auxNode = getChildNodeById(__wf_nlElementos, auxId);
                    if (null == auxNode)
                        msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.8)";
                    else {
                        fromCompareID = auxNode.getAttribute("grupoWorkflow");
                        toCompareID = toNode.getAttribute("grupoWorkflow");
                    }
                } // else if (null == auxId)
            } // else if (__wf_cTipoElementoJuncao == fromType) 
            else {
                // se não for nenhuma das situações especiais acima, compara 0 atributo 'grupoWorkflow' 
                // da origem e destino
                fromCompareID = fromNode.getAttribute("grupoWorkflow");
                toCompareID = toNode.getAttribute("grupoWorkflow");
            } // else
        } // if (null == msgErro)

        if (null == msgErro) {
            if (null == fromCompareID)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.9)";
        } // if (null == msgErro)

        if (null == msgErro) {
            if (null == toCompareID)
                msgErro = traducao.WorkflowCharts_erro_interno_do_aplicativo_ + " (0020.10)";
        } // if (null == msgErro)

        if (null == msgErro) {
            if (fromCompareID != toCompareID) {
                elNameTo = getElementIdName(toNode);
                if (null == elNameTo)
                    elNameTo = "";

                msgErro = traducao.WorkflowCharts_falha_na_verifica__o_do_modelo_de_fluxo__as_conex_es_ao_item__ + elNameTo + traducao.WorkflowCharts___est_o_incorretas_;
            } // (fromCompareID != toCompareID)
        } // if (null == msgErro)

        // sai do loop caso tenha dado algum erro
        if (null != msgErro)
            break;
    } // for (var i...

    if (null == msgErro)
        bExiste = false;
    else {
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);
        bExiste = true;
    }

    return bExiste;
}

/// <summary>
/// Função para obter o id do elemento de início do ID passado como parâmetro.
/// São considerados elementos de início junções, disjunções e o próprio elemento início.
/// Será devolvido o primeiro 'anterior' no fluxo, partindo do elemento passado como parâmetro (ID)
/// </summary>
/// <param name="elementoId" type="string">id do elemento cujo elemento de início deseja-se obter.</param>
/// <returns type="string">ID do elemento início encontrado. Null caso nenhum elemento de início tenha sido encontrado.</returns>
function obtemIdElementoInicio(elementoId, originalId) {
    var to, from;
    var setaInicio, setaFim;
    var fromId;
    var inicioId = null;
    var tipo;

    // se o elemento em questão é o elemento início do fluxo, retorna o próprio ID
    if (true == ehElementoDoTipo(getChildNodeById(__wf_nlElementos, elementoId), __wf_cTipoElementoInicio))
        return elementoId;

    for (var i = 0; i < __wf_nlConectores.length; i++) {
        if (__wf_nlConectores[i].nodeType != 1)
            continue;

        from = __wf_nlConectores[i].getAttribute("from");
        to = __wf_nlConectores[i].getAttribute("to");

        setaInicio = __wf_nlConectores[i].getAttribute("arrowAtStart");
        if (null == setaInicio)
            setaInicio = "0";

        setaFim = __wf_nlConectores[i].getAttribute("arrowAtEnd");
        if (null == setaFim)
            setaFim = "0";

        fromId = null;

        if ((null != from) && (null != to)) {
            if ((from == elementoId) && ("1" == setaInicio))
                fromId = to;
            else if ((to == elementoId) && ("1" == setaFim))
                fromId = from;
        } // if ( (null != from) && ...

        if ((null != fromId) && (fromId != originalId)) {
            tipo = obtemTipoElemento(getChildNodeById(__wf_nlElementos, fromId));
            if ((__wf_cTipoElementoInicio == tipo) || (__wf_cTipoElementoJuncao == tipo) ||
                (__wf_cTipoElementoDisjuncao == tipo))
                inicioId = fromId;
            else
                inicioId = obtemIdElementoInicio(fromId, originalId);
        } // if ( (null!=fromId) && ...

        if (null != inicioId)
            break;
    } // for (var i=0; i<__wf_nlConectores; i++)

    return inicioId;
}

/// <summary>
/// função para tratar o evento de inicialização da coluna da grid de formulários na divEtapa. 
/// A coluna em questão é exatamente a coluna que apresenta um DropDownList para a escolha dos formulários.
/// </summary>
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function OnCmbEtapaOrigem_cntSelectedIndexChanged(s, e) {

    var qtdFormsRequerAssinaturaDigital = frv_qtdFormulariosRequerAssinatura(null, s.GetValue());
    if (qtdFormsRequerAssinaturaDigital === 0) {
        tdSolicAssinaturaDigital.hidden = "hidden";
        cbSolicitarAssinaturaDigital.visible = false;
    } else if (hfValoresTela.Get("utilizaAssinaturaDigitalFormularios") == "S") {
        tdSolicAssinaturaDigital.hidden = "";
        cbSolicitarAssinaturaDigital.visible = true;
    }

    SetaInteratividadeCamposDivConector();
    e.processOnServer = false;
    return;
}

function ocultaDivEdicaoWorkflow(divId) {
    /// <summary>
    /// função para ocultar uma div
    /// </summary>
    /// <param name="divId" type="string">id da div a ocultar.</param>
    /// <returns type="boolean">Se tudo deu certo ao esconder a div, retorna true. Caso contrário, false.</returns>
    var bRet = false;
    var div = $el(divId);
    if (null != div) {
        div.style.display = 'none';

        var div2 = $el("__wf_divFundo");
        if (null != div2) {
            div2.style.display = 'none';
            bRet = true;
        }
    }
    return bRet;
}

function obtemObjetoXMLHttpRequest() {
    /// <summary>
    /// função para devolver um objeto XMLHTTPRequest
    /// </summary>
    /// <returns type="XMLHttpRequest Object">Retorna o objeto XMLHttpRequest.</returns>

    var xmlObj;
    try {
        xmlObj = new ActiveXObject("Microsoft.XMLHTTP");
    }
    catch (e) {
        try {
            xmlObj = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (ex) {
            try {
                xmlObj = new XMLHttpRequest();
            }
            catch (exc) {
                window.top.mostraMensagem(traducao.WorkflowCharts_falha_no_acesso_das_fun__es_do_browser__, 'atencao', true, false, null);
                xmlObj = null;
            }
        }
    }
    return (xmlObj);
}


// ------------------------------------------------------------------------------------------------- //
// ------                                                                                     ------ //
// ------ Funções para edição dos elementos do gráfico                                        ------ //
// ------                                                                                     ------ //
// ------------------------------------------------------------------------------------------------- //
function $el(id) {
    return document.getElementById(id);
}
//window.onload=window.onresize=window.onscroll=function(){
//var data=getWindowData();
//$el('dadosBasicosWorkFlow').style.left=data[0]/2+data[2]-parseInt($el('pp').style.width)/2+'px';
//$el('dadosBasicosWorkFlow').style.top=data[1]/2+data[3]-parseInt($el('pp').style.height)/2+'px';
//}
///---end by Alejandro



function mostraDivLoading() {
    var div = $el('divLoading');
    if (null != div)
        div.style.display = 'block';
}

function ocultaDivLoading() {
    var div = $el('divLoading');
    if (null != div)
        div.style.display = 'none';
}




/// <summary>
/// função para apresentar a div que mostra ação realizada no workflow (salvo, publicado).
/// </summary>
/// <remarks>
/// Apresenta a div e já agenda a sua 'ocultação' em 3 segundos.
/// </remarks>
/// <param name="titulo" type=string>Título da div (nome da aplicação, p. ex.)</param>
/// <param name="acao" type=string>Texto indicando a ação executada, p. ex.: Workflow salvo com sucesso!.</param>
/// <returns>void</returns>
function mostraDivSalvoPublicado(titulo, acao) {
    window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    setTimeout('ocultaDivSalvoPublicado()', 1500);
}

function ocultaDivSalvoPublicado() {


    // se ação escolhida foi publicar e deu certo a publicação, fecha a tela
    if (("publicar" == pnlCbkEdicaoElementos.cpAcaoEscolhida) &&
        ("1" == pnlCbkEdicaoElementos.cpAcaoExecutadaComSucesso)) {
        window.top.gotoURL('administracao/adm_CadastroWorkflows.aspx', '_self');
    }
}


/// <summary>
/// função para anota na variável que o título de um formulário foi digitado pelo usuário para que o 
/// aplicativo deixe de sugerir o título a partir deste momento.
/// </summary>
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function onColFormTitle_etpTextChanged(s, e) {
    __wf_bFormTitleChanged = true;
}

/// <summary>
/// função para tratar sugerir o título do formulário quando selecionando um formulário na div etapa
/// sugere apenas se o título ainda não foi mudado ou caso tenha sido 'retirada a digitação'
/// </summary>
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function onColFormCode_etpSelectedIndexChanged(s, e) {
    if ((false == __wf_bFormTitleChanged) || (null == colFormTitle_etp.GetValue()) || ('' == colFormTitle_etp.GetValue())) {
        var sFormTitle = colFormCode_etp.GetText();
        if (null != sFormTitle) {
            var tam = sFormTitle.length;
            if (tam > 0) {
                var newTitle;
                // limita a 50 que é o tamanho na base de dados para o título do relatório !!!!
                if (tam > 50)
                    newTitle = sFormTitle.substring(0, 49);
                else
                    newTitle = sFormTitle

                colFormTitle_etp.SetValue(newTitle);
            } // if (tam>0) 
        } // if (null != sFormTitle)
    } // if ( (false == __wf_bFormTitleChanged) || ...
}


/// <summary>
/// Função para receber, do servidor, os valores dos campos da grid de edição de elementos 
/// quando se tratando de exclusão de um conector
/// </summary>
/// <remarks>
/// Recebe os valores dos campos da grid de edição de elementos e chama a função que vai excluir 
/// o conector e as suas informações no XML 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos da linha em questão da gvEdicaoElementos
/// </param>
/// <returns type="void">void</returns>
function recebeCamposExclusaoConector(valores) {
    var acao = valores[0];
    var from = valores[1]
    var to = valores[2];
    var connector = new xConnector();

    if ((null != acao) & (null != from) & (null != to)) {
        if (false == xmlWorkflowOk())
            return;

        connector.from = from;
        connector.to = to;
        connector.acao = acao;
        removeConnectorFromXml(connector);
    }
    return;
}


/// <summary>
/// Função para receber, do servidor, os valores dos campos da grid de edição de elementos 
/// quando se tratando de exclusão de um timer
/// </summary>
/// <remarks>
/// Recebe os valores dos campos da grid de edição de elementos e chama a função que vai excluir 
/// o timer e as suas informações no XML 
/// </remarks>
/// <param name="valores" type="array">Matriz contendo os valores dos campos da linha em questão da gvEdicaoElementos
/// </param>
/// <returns type="void">void</returns>
function recebeCamposExclusaoTimer(valores) {
    var timerID = valores[0];
    var from = valores[1]
    var to = valores[2];


    if ((null != timerID) & (null != from) & (null != to)) {
        if (false == xmlWorkflowOk())
            return;

        __wf_timerObj.elementId = timerID;
        __wf_timerObj.from = from;
        __wf_timerObj.to = to;

        removeTimerFromXml(__wf_timerObj);
    }
}

/// <summary>
/// Função para receber, do servidor, os valores dos campos da grid de edição de elementos 
/// quando se tratando de exclusão de elementos genéricos (fins, junções, disjunções)
/// </summary>
/// <remarks>
/// Recebe os valores dos campos da grid de edição de elementos e chama a função que vai excluir  
/// o elemento e as suas informações no XML 
/// </remarks>
/// <param name="idElemento" type="string">ID do elemento a ser excluído.</param>
/// <returns type="void">void</returns>
function recebeCamposExclusaoElementos(idElemento) {
    if (null != idElemento) {
        if (false == xmlWorkflowOk())
            return;

        removeElementoFromXml(idElemento);
    }
    return;
}

/// <summary>
/// Exclui um elemento gráfico do XML.
/// </summary>
/// <remarks>
/// Por elemento gráfico, entende-se elemento dentro do nó "dataSet" do gráfico do xml
/// </remarks>
/// <param name="elementID" type="string">id do elemento gráfico a ser excluído.</param>
/// <returns type="boolean">Se deu certo a exclusão do elemento, retorna true. Caso contrário, false.</returns>
function excluiElementoDoXml(elementID) {
    var ds = __wf_xmlDocWorkflow.getElementsByTagName("dataSet");
    var ne = getChildNodeById(__wf_nlElementos, elementID);
    var bRet = false;

    if (null != ne) {
        // exclui o nó referente à etapa;
        ds[0].removeChild(ne);
        bRet = true;
    }

    return bRet;
}

/// <summary>
/// função chamada no evento endCallBack do painel pnlCbkEdicaoElementos 
/// </summary>
/// <remarks>
/// oculta a div 'loading' e mostra a div indicando a ação escolhida.
/// </remarks
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function onPnlCbkEdicaoElementos_EndCallback(s, e) {
    // a propriedade customizada cpAcaoEscolhida é definiado no processamento do callBack do painel
    if ("salvar" == pnlCbkEdicaoElementos.cpAcaoEscolhida) {
        ocultaDivLoading();
        if ("1" == pnlCbkEdicaoElementos.cpAcaoExecutadaComSucesso)
            mostraDivSalvoPublicado("...", traducao.WorkflowCharts_vers_o_gravada_com_sucesso_);
    }
    else if ("publicar" == pnlCbkEdicaoElementos.cpAcaoEscolhida) {
        ocultaDivLoading();

        if ("1" == pnlCbkEdicaoElementos.cpAcaoExecutadaComSucesso)
            mostraDivSalvoPublicado("...", traducao.WorkflowCharts_vers_o_publicada_com_sucesso_);
    }
}

function x() {
    var exportType = 'PDF';

    __wf_chartObj.exportChart({ exportFormat: exportType });
}
