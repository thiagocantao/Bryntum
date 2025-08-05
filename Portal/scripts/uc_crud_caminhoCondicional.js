// "DataSource" responsável por armazenar os dados. Será um array de array.
var arrayRegistrosGridExpressoes = []; // Estrutura: codigoExpressaoCorrente, expressaoExtenso, expressaoAvaliada, codigoEtapaDestino, nomeEtapaDestino
var indiceCorrenteArrayRegistroGridExpressoes;// Número do índice correspondente ao registro que está sendo editado
var modoOperacaoObjeto = "";
var codigoObjetoCaminhoCondicional = -1; // id do objeto "caminho condicional"

// array utilizado para armazernar os códigos e nomes dos campos que podem ser utilizados na expressão
var arrayCampos_DePara = [];
var modoOperacaoDesignerCondicao = "";
var expressaoAvaliada = "";

// retorna o índice correspondente ao codigoExpressao informado no parametro. 
function getIndiceArrayRegistroGridExpressoes(CodigoChave) {
    for (var i = 0; i < arrayRegistrosGridExpressoes.length; i++) {
        var temp = arrayRegistrosGridExpressoes[i][0]; // a Posição Zero é o código da expressao
        if (temp == CodigoChave) {
            return i; // retorna o indice do array
        }
    }
    return -1; // -1 para indicar que o código não foi encontrado
}

function pcDvCaminhoCondicionalShow(modo, idObjeto) {
    modoOperacaoObjeto = modo;

    // preenche o combo "cmbEtapaOrigemDvCaminhoCondicional" com as etapas disponíveis
    preencheCombosComTipoElementos(cmbEtapaOrigemDvCaminhoCondicional, __wf_cTipoElementoEtapa, "");

    // se é um novo elemento
    if (modo == "Inc") {
        // na inclusão, o código sempre será -1
        codigoObjetoCaminhoCondicional = -1;
        // Se não tinha etapas disponíveis, avisa e sai sem mostrar a tela 
        if (cmbEtapaOrigemDvCaminhoCondicional.GetItemCount() <= 2)
        {
            window.top.mostraMensagem(traducao.uc_crud_caminhoCondicional_para_utilizar_esta_op__o___necess_rio_j__ter_inclu_do_no_fluxo_no_m_nimo_tr_s_etapas__origem__caminho_alternativo_e_caminhos_condicionais__, 'Atencao', true, false, null);
            return;
        }

        txtEtapaOrigemLabelOpcao.SetText("");

        // vai no servidor para criar o datasource e popular/limpar a grid de expressões.
        arrayRegistrosGridExpressoes = [];
        hf.Set('arrayRegistrosGridExpressoes', arrayRegistrosGridExpressoes);
        gv_Condicoes.PerformCallback(arrayRegistrosGridExpressoes);

        // mostra a div para a seleção da etapa de origem
        dvSelecaoEtapa.SetVisible(true);
        // esconde a div para o cadastro de condições que só pode aparecer depois que a etapa de origem for selecionada.
        dvGridCondicoes.SetVisible(false);
    }
    else // alteração de um elemento existente
    {
        codigoObjetoCaminhoCondicional = idObjeto;
        // esconde a div para a seleção da etapa de origem, pois a etapa já está selecionada
        dvSelecaoEtapa.SetVisible(false);
        // mosta a div para o cadastro de condições
        dvGridCondicoes.SetVisible(true);
        // carrega as expressões associadas ao objeto que está sendo editado
        getInformacoesObjetoSelecionadoApartirDoXML(idObjeto);
    }

    // mostra o popupControl 
    pcDvCaminhoCondicional.Show();
}

function getInformacoesObjetoSelecionadoApartirDoXML(idObjeto)
{
    //var nodeObjetox = getEtapaChildNodeByActivityId(__wf_nlElementos, idObjeto);
    var nodeObjeto = getChildNodeById(__wf_nlElementos, idObjeto);

    //nodeObjeto.childNodes['0']
    var nodeExpressoes = nodeObjeto.childNodes['0'];//nodeObjeto.getElementsByTagName("expressoes");
    var idEtapaOrigem = nodeExpressoes.getAttribute('idEtapaOrigem');
    var nomeOpcao = nodeExpressoes.getAttribute('nomeOpcao');
    var idEtapaPadrao = nodeExpressoes.getAttribute('idEtapaPadrao');

    cmbEtapaOrigemDvCaminhoCondicional.SetValue(idEtapaOrigem);
    txtEtapaOrigemDvCaminhoCondicional.SetText(cmbEtapaOrigemDvCaminhoCondicional.GetText());
    txtEtapaOrigemLabelOpcao.SetText(nomeOpcao);

    preparaTela_CaminhoCondicional_ParaUtilizarEtapaSelecionada();

    cmbEtapaDestinoPadrao.SetValue(idEtapaPadrao);

    // popula o combo de etapas de destino. A etapa de origem não pode participar da lista de destino
    //preencheCombosComTipoElementos(cmbEtapaDestinoDvCaminhoCondicional, __wf_cTipoElementoEtapa, cmbEtapaOrigemDvCaminhoCondicional.GetText());


    // obtem os expressões associadas a etapa de origem
    var expressao = nodeObjeto.getElementsByTagName("expressao");

    arrayRegistrosGridExpressoes = [];
    for (var i = 0; i < expressao.length; i++) {
        node = expressao[i];
        var registroCorrente = [];
        registroCorrente[0] = parseInt(node.getAttribute('id'));
        registroCorrente[1] = node.getAttribute('extenso');
        registroCorrente[2] = node.getAttribute('avaliada');
        registroCorrente[3] = parseInt(node.getAttribute('idEtapaDestino'));
        // obtem o nome da etapa de destino. "Miau". Optei por ler direto do combo "cmbEtapaDestinoDvCaminhoCondicional" que já está populado.
        cmbEtapaDestinoDvCaminhoCondicional.SetValue(registroCorrente[3]);
        registroCorrente[4] = cmbEtapaDestinoDvCaminhoCondicional.GetText();

        arrayRegistrosGridExpressoes.push(registroCorrente);
    }

    // vai no servidor para criar o datasource e popular a grid.
    hf.Set('arrayRegistrosGridExpressoes', arrayRegistrosGridExpressoes);
    gv_Condicoes.PerformCallback(arrayRegistrosGridExpressoes);
}

function atualizaArrayCamposCondicao(listaCampos) {
    for (i = 0; i < listaCampos.length; i++)
    {
        var arrayCampos = listaCampos[i].split(';');
        var descricaoCampo = arrayCampos[0];
        var codigoCampo = arrayCampos[1];
        if (arrayCampos_DePara.indexOf(codigoCampo + ";" + descricaoCampo) == -1)
            arrayCampos_DePara.push(codigoCampo + ";" + descricaoCampo);
    }
}

function salvaCaminhoCondicional()
{
    // verifica se todas as condições inseridas apontam para etapas diferentes da etapa padrão
    var codigoEtapaPadrao = cmbEtapaDestinoPadrao.GetValue();
    for (var i = 0; i < arrayRegistrosGridExpressoes.length; i++) {
        var codigoEtapaDestinoNaExpressao = arrayRegistrosGridExpressoes[i][3];
        if (codigoEtapaDestinoNaExpressao == codigoEtapaPadrao) {
            window.top.mostraMensagem(traducao.uc_crud_caminhoCondicional_a_ + (i + 1) + traducao.uc_crud_caminhoCondicional___express_o_direciona_o_fluxo_para_a_etapa_de_caminho_alternativo__altere_este_registro_para_que_direcione_para_outra_etapa_ou_altere_a_etapa_de_caminho_alternativo_, 'Atencao', true, false, null);
            return false;
        }
    }

    if (modoOperacaoObjeto == "Inc") {
        insereObjetoXML(__wf_xmlDocWorkflow, traducao.uc_crud_caminhoCondicional_caminho_condicional);
    }
    else if (modoOperacaoObjeto == "Edt") {
        removeElementoFromXml(codigoObjetoCaminhoCondicional);
        insereObjetoXML(__wf_xmlDocWorkflow, traducao.uc_crud_caminhoCondicional_caminho_condicional);
    }
    else
        return false;

    return true;
}

function preparaTela_CaminhoCondicional_ParaUtilizarEtapaSelecionada()
{
    // se não selecionou a etapa, sai sem fazer nada
    if (cmbEtapaOrigemDvCaminhoCondicional.GetSelectedIndex() == -1)
        return false;

    var texto = cmbEtapaOrigemDvCaminhoCondicional.GetText();
    txtEtapaOrigemDvCaminhoCondicional.SetText(texto);

    // popula o combo de etapas de destino PADRÃO. A etapa de origem não pode participar da lista de destino
    preencheCombosComTipoElementos(cmbEtapaDestinoPadrao, __wf_cTipoElementoEtapa, texto);

    // popula o combo de etapas de destino ALTERNATIVO. A etapa de origem não pode participar da lista de destino
    preencheCombosComTipoElementos(cmbEtapaDestinoDvCaminhoCondicional, __wf_cTipoElementoEtapa, texto);

    dvSelecaoEtapa.SetVisible(false);
    dvGridCondicoes.SetVisible(true);
    pcDvCaminhoCondicional.UpdatePosition();

    // obtem o "ID" correspondente a etapa de origem
    var idEtapa = cmbEtapaOrigemDvCaminhoCondicional.GetValue();
    // obtem o "Node" correspondente a etapa de origem
   // var nodeEtapaOrigemx = getEtapaChildNodeByActivityId(__wf_nlElementos, idEtapa);
    var nodeEtapaOrigem = getChildNodeById(__wf_nlElementos, idEtapa);
    
    // obtem os formulários utilizados na etapa de origem
    var formularios = nodeEtapaOrigem.getElementsByTagName("formulario");

    //
    var listaNomeECodigoFormulario = "";

    // Popula a lista de formulários utilizados na etapa de origem
    lstFormularios.ClearItems();
    for (var i = 0; i < formularios.length; i++) {
        node = formularios[i];
        lstFormularios.AddItem(node.getAttribute('title'), node.getAttribute('id'));
        if (listaNomeECodigoFormulario == "") {
            listaNomeECodigoFormulario = node.getAttribute('title') + "|" + node.getAttribute('id');
        } else {
            listaNomeECodigoFormulario = listaNomeECodigoFormulario + "," + node.getAttribute('title') + "|" + node.getAttribute('id');
        }
    }

    // Envia para o servidor "filtrar" a lista, retirando os formulários que não são válidos para esta operação
    callbackForms.PerformCallback(listaNomeECodigoFormulario);
}

function preparaTelaParaNovaCondicao()
{
    modoOperacaoDesignerCondicao = 'Inc';
    txtEdidorCondicao.SetText("");
    cmbEtapaDestinoDvCaminhoCondicional.SetSelectedIndex(-1);
    pcDesignerCondicoes.Show();
}

// função chamada ao clicar na opção "Editar/Caminho condicional"
function recebeCamposEditaCaminhoCondicional(id)
{
    // na edição, o código do objeto será enviado por parametro
    pcDvCaminhoCondicionalShow('Edt', id);
}

function preparaTelaParaEditarCondicaoExistente(CodigoExpressao) {
    modoOperacaoDesignerCondicao = 'Edt';
    // busca o índice do registro corrente
    indiceCorrenteArrayRegistroGridExpressoes = getIndiceArrayRegistroGridExpressoes(CodigoExpressao);
    var registroCorrente = arrayRegistrosGridExpressoes[indiceCorrenteArrayRegistroGridExpressoes];
    if (registroCorrente != null) {
        txtEdidorCondicao.SetText(registroCorrente[1]);
        cmbEtapaDestinoDvCaminhoCondicional.SetValue(registroCorrente[3]);
        pcDesignerCondicoes.Show();
    }
}

function constroiDeParaCodigoCampo(stringSimulaDeParaCodigoCampo)
{
    // O Callback do "lstFormularios" retornou uma string que deverá ser transformada no Array "De-Para"
    arrayCampos_DePara = stringSimulaDeParaCodigoCampo.split("|");
}

// Inclui o conteudo passado como parametro dentro do editor de condições
function incluiCampoEditorCondicao(Codigo, Conteudo, tipo) {
    // se é um campo, precisa ser composto com o nome do formulário e "[.]"
    if (tipo == "CAMPO") {
        // obtem o nome do formulário para compor o título do campo
        var nomeFormulario = lstFormularios.GetSelectedItem().text;
        Conteudo = "[" + nomeFormulario + "." + Conteudo + "]";

        // se o campo não existe na lista de depara
        if (arrayCampos_DePara.indexOf(Codigo + ";" + Conteudo) == -1)
            arrayCampos_DePara.push(Codigo + ";" + Conteudo);
    }

    // obtem a posição do cursor e insere o título do campo
    var el = txtEdidorCondicao.GetInputElement();
    var start = el.selectionStart;
    var end = el.selectionEnd;
    var text = el.value;
    var before = text.substring(0, start);
    var after = text.substring(end, text.length);
    el.value = before + Conteudo + after;
    //  reposiciona o cursor dentro do campo "memo"
    el.selectionStart = el.selectionEnd = start + Conteudo.length;
}

function getCodigoCampoPelaDescricaoComposta(descricaoComposta)
{
    for (var i = 0; i < arrayCampos_DePara.length; i++) {
        var temp = arrayCampos_DePara[i].split(';');
        if (temp[1] == descricaoComposta) {
            return temp[0];
        }
    }
    return -1;
}

async function validaExpressaoCondicional()
{
    var expressao = txtEdidorCondicao.GetText();
    var condicaoInvalida = false;
    var existeExpressaoCampoAssociado = false;
    var trechoExpressaoComCampo = "";
    var codigoWorkflow;
    var codigoFluxo
    
    // as quantidade de "[", "]" e "!" devem ser iguais e maior que zero para que exista campos na expressão
    var qtde_AbreCampo = expressao.split("[").length - 1;
    var qtde_FechaCampo = expressao.split("]").length - 1;
    var qtde_SeparaCampo = expressao.split(".").length - 1;

    if (qtde_AbreCampo > 0 || qtde_FechaCampo > 0) {
        // se a quantidade de caracteres é diferente, inválido
        if (qtde_AbreCampo != qtde_FechaCampo)
            condicaoInvalida = true;
    }

    // se existe campo
    if (qtde_AbreCampo > 0 && condicaoInvalida == false)
    {
        var campos = [];
        var expTemp = expressao;
        for (campo = 0; campo < qtde_AbreCampo; campo++)
        {
            //Separa um possível trecho da expressão que contem um campo associado
            trechoExpressaoComCampo = expTemp.substring(expTemp.indexOf("["), expTemp.indexOf("]") + 1);
            //Se dentro do trecho selecionado existe um "." então significa que existe um campo associado
            if (trechoExpressaoComCampo != "" && trechoExpressaoComCampo.indexOf(".") >= 0) {
                campos[campo] = expTemp.substring(expTemp.indexOf("[") + 1, expTemp.indexOf("]"));
                expTemp = expTemp.replace("[" + campos[campo] + "]", "1");
                existeExpressaoCampoAssociado = true;
            } else {
                //Se caiu aqui então existe "[" e "]" na expressão, porém não tem campo associado e pode ser uma chamada de função
                //Neste caso vamos submeter a expressão para análise no SQL Server
                expTemp = expTemp.replace(trechoExpressaoComCampo, " ");
            }

        }

        //Se existe expressão com campo associado então valida
        if (existeExpressaoCampoAssociado) {
            // se após a troca dos campos, sobrou algum dos caracteres "[.]", tem algo errado.
            if (expTemp.indexOf("[") >= 0 || expTemp.indexOf(".") >= 0 || expTemp.indexOf("]") >= 0) {
                condicaoInvalida = true;
            }
        }

        // reescreve a expressão, trocando os nomes pelos códigos dos campos
        for (campo = 0; campo < campos.length; campo++)
        {
            if (campos[campo] != undefined) {
                var codigoCampo = getCodigoCampoPelaDescricaoComposta("[" + campos[campo] + "]");
                if (codigoCampo == -1) {
                    condicaoInvalida = true;
                }
                expressao = expressao.replace(campos[campo], codigoCampo);
            }

        }

        // guarda a exoressao avaliada para ser utilizada pelos metodos "insereNovoRegistro()" e "atualizaRegistro()" 
        expressaoAvaliada = expressao;

    }
    //Esta função chama o processo de verificação da expressão pelo ajax no servidor
    async function processoVerificacao() {
        try {
            return await verificaExpressaoCondicional(expressao, codigoFluxo, codigoWorkflow);
        } catch (error) {
            console.error("Erro:", error);
        }
    }

    //Se existe condição inválida significa que a sintaxe das expressões com campos associados está errada e nem chama a validação via SQL Server
    if (condicaoInvalida == true) {
        window.top.mostraMensagem('Expressão condicional não é válida!', 'atencao', true, false, null);
    } else {
        codigoWorkflow = document.getElementById("hfCodigoWorkflow").value;
        codigoFluxo = document.getElementById("hfCodigoFluxo").value;
        expressaoAvaliada = expressao;
        processoVerificacao();
    }
}

async function verificaExpressaoCondicional(expressao, codigoFluxo, codigoWorkflow) {

    var expressaoValida = false;

    var dataIn = {
        expressao: encodeURI(expressao),
        codigoFluxo: encodeURI(codigoFluxo),
        codigoWorkflow: encodeURI(codigoWorkflow)
    };
    try {
        var data = await $.ajax({
            url: '../workflow/wsWorkflows.asmx/valida-expressao-caminho-condicional',
            data: dataIn,
            type: 'POST'
        });

        if (data != '') {
            expressaoValida = !!(data) && !!(data.firstElementChild)
                && (data.firstElementChild.innerHTML === 'true');
            if (expressaoValida == false)
                window.top.mostraMensagem('Expressão condicional não é válida!', 'atencao', true, false, null);
            else
                salvaExpressaoCondicional();
        } else {
            console.log("Não houve retorno do servidor para a rota: valida-expressao-caminho-condicional")
        }

    } catch (error) {
        throw new Error("Erro na resposta do webservice");
    }
}

function salvaExpressaoCondicional() {
    if (modoOperacaoDesignerCondicao == "Inc")
        insereNovoRegistro();
    else
        atualizaRegistro();

    hf.Set('arrayRegistrosGridExpressoes', arrayRegistrosGridExpressoes);
    gv_Condicoes.PerformCallback(arrayRegistrosGridExpressoes);

    modoOperacaoDesignerCondicao = '';
    pcDesignerCondicoes.Hide();
}

function insereNovoRegistro() {
    // obtem o código para o novo registro
    var codigoExpressao = 1;
    if (arrayRegistrosGridExpressoes.length > 0)
        codigoExpressao = parseInt(arrayRegistrosGridExpressoes[arrayRegistrosGridExpressoes.length - 1][0]) + 1;

    var expressaoExtenso = txtEdidorCondicao.GetText();
    var codigoEtapaDestino = cmbEtapaDestinoDvCaminhoCondicional.GetValue();
    var nomeEtapaDestino = cmbEtapaDestinoDvCaminhoCondicional.GetText();
    var novoRegistro = [codigoExpressao, expressaoExtenso, expressaoAvaliada, codigoEtapaDestino, nomeEtapaDestino];
    arrayRegistrosGridExpressoes.push(novoRegistro);
}

function atualizaRegistro()
{
    // busca o registro que está sendo atualizado a partir do índice que foi obtido no momento que clicou no botão "btnEditar"
    var registroCorrente = arrayRegistrosGridExpressoes[indiceCorrenteArrayRegistroGridExpressoes];
    registroCorrente[1] = txtEdidorCondicao.GetText();
    registroCorrente[2] = expressaoAvaliada;
    registroCorrente[3] = cmbEtapaDestinoDvCaminhoCondicional.GetValue();
    registroCorrente[4] = cmbEtapaDestinoDvCaminhoCondicional.GetText();
}

function excluiRegistro(CodigoExpressao)
{
    if (confirm(traducao.uc_crud_caminhoCondicional_deseja_excluir_a_condi__o_selecionada_) == false)
        return;

    // busca o registro que deve ser excluído
    var indiceParaExclusao = getIndiceArrayRegistroGridExpressoes(CodigoExpressao);
    var registro = arrayRegistrosGridExpressoes[indiceParaExclusao];

    // remove o registro
    arrayRegistrosGridExpressoes.splice(indiceParaExclusao, 1);

    // atualiza a grid
    hf.Set('arrayRegistrosGridExpressoes', arrayRegistrosGridExpressoes);
    gv_Condicoes.PerformCallback(arrayRegistrosGridExpressoes);
}

// ----------------------------------------------------------------------------------------------------------------------------------
// Funções que sugiro serem colocadas no arquivo "WorkflowCharts.js"
function getElementosWF()
{
    /// <summary>
    /// função para retornar um array com todos os elementos disponíveis
    /// </summary>
    /// <returns type="array">array</returns>

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

    return linhas;
}

function preencheCombosComTipoElementos(comboParam, tipoElementoParam, textoNoAddParam) {
    /// <summary>
    /// função para preencher um combobox com os tipos de elementos informados no parametro
    /// </summary>
    /// <returns type="void">void</returns>

    comboParam.ClearItems(); // limpa os itens do combo box
    comboParam.SetSelectedIndex(-1); // 

    var linhas = getElementosWF();
    var qtdeElementos = 0;
    // adiciona os itens
    for (var i = 0; i < linhas.length; i++) {
        // Só adiciona os elementos do tipo informado 
        if (tipoElementoParam == linhas[i][2]) {
            if (linhas[i][1] != textoNoAddParam) {
                comboParam.AddItem(linhas[i][1], linhas[i][0]);
                qtdeElementos++;
            }
        }
        
    } // for (avr i

    return qtdeElementos;
}

function insereObjetoXML(xmlDoc, toolText) {

    var ds = xmlDoc.getElementsByTagName("dataSet");
    var Elementos = ds[0].getElementsByTagName("set");
    var elementId = getNextElementId(Elementos);
    var name = txtEtapaOrigemLabelOpcao.GetText();

    var ne = xmlDoc.createElement("set");
    ne.setAttribute("shape", "RECTANGLE");
    ne.setAttribute("id", elementId);
    ne.setAttribute("name", name);
    ne.setAttribute("toolText", toolText);
    ne.setAttribute("x", 5);
    ne.setAttribute("y", 95);
    ne.setAttribute("width", 26);
    ne.setAttribute("height", 26);
    ne.setAttribute("imageNode", "1");
    ne.setAttribute("imageURL", "../imagens/Workflow/condicional.png");
    ne.setAttribute("imageWidth", "25");
    ne.setAttribute("imageHeight", "25");
    ne.setAttribute("imageAlign", "MIDDLE");
    ne.setAttribute("labelAlign", "BOTTOM");
    //ne.setAttribute("color", "CCFFCC");
    ne.setAttribute("tipoElemento", __wf_cTipoElementoCondicao);
    ne.setAttribute("etapaInicial", "0");
    ne.setAttribute("grupoWorkflow", "0");
    ne.setAttribute("idElementoInicioFluxo", "-1");
    ne.setAttribute("ocultaBotoesAcao", "1");
    ne.setAttribute("codigoSubfluxo", "0");
    ds[0].appendChild(ne);

    // Insere as tags "expressoes/expressao" 
    var expressoes = __wf_xmlDocWorkflow.createElement("expressoes");
    expressoes.setAttribute("idEtapaOrigem", cmbEtapaOrigemDvCaminhoCondicional.GetValue());
    expressoes.setAttribute("nomeOpcao", txtEtapaOrigemLabelOpcao.GetText());
    expressoes.setAttribute("idEtapaPadrao", cmbEtapaDestinoPadrao.GetValue());
    for (var i = 0; i < arrayRegistrosGridExpressoes.length; i++) {
        expressao = __wf_xmlDocWorkflow.createElement("expressao");
        expressao.setAttribute("id", arrayRegistrosGridExpressoes[i][0]); //codigoExpressaoCorrente
        expressao.setAttribute("extenso", arrayRegistrosGridExpressoes[i][1]); //expressaoExtenso 
        expressao.setAttribute("avaliada", arrayRegistrosGridExpressoes[i][2]); //expressaoAvaliada 
        expressao.setAttribute("idEtapaDestino", arrayRegistrosGridExpressoes[i][3]); //codigoEtapaDestino
        expressoes.appendChild(expressao);
    }
    ne.appendChild(expressoes);

    //----- insere os conectores;
    var connector = new xConnector();

    // Conector de Origem
    // --------------------------------------------------------------
    connector.acao = txtEtapaOrigemLabelOpcao.GetText();
    connector.from = cmbEtapaOrigemDvCaminhoCondicional.GetValue();
    connector.to = elementId;
    addConnectorInXml(connector, false);
    atualizaXmlComInfoDoObjetoConnector(xmlDoc, connector);

    // conectores de destino as expressões informadas. Será um para cada condição
    var i;

    for (i = 0; i < arrayRegistrosGridExpressoes.length; i++) {
        codigoEtapaDestino = arrayRegistrosGridExpressoes[i][3]; // a Posição três é o código da etapa de destino
        
        connector.acao = traducao.uc_crud_caminhoCondicional_condi____o_ + arrayRegistrosGridExpressoes[i][0];
        connector.from = elementId;
        connector.to = codigoEtapaDestino;
        connector.condition = arrayRegistrosGridExpressoes[i][1];
        connector.decodedCondition = arrayRegistrosGridExpressoes[i][2];
        connector.conditionIndex = arrayRegistrosGridExpressoes[i][0];
        addConnectorInXml(connector, false);
        atualizaXmlComInfoDoObjetoConnector(xmlDoc, connector);
    }

    // Conector de destino para a Etapa Padrão
    // --------------------------------------------------------------
    connector.acao = traducao.uc_crud_caminhoCondicional_caminho_alternativo;
    connector.from = elementId;
    connector.to = cmbEtapaDestinoPadrao.GetValue();
    connector.condition = traducao.uc_crud_caminhoCondicional_caminho_alternativo;
    connector.decodedCondition = "else";
    connector.conditionIndex = ++i;
    addConnectorInXml(connector, false);
    atualizaXmlComInfoDoObjetoConnector(xmlDoc, connector);

    atualizaXmlDoGrafico(xmlDoc);
}

function atualizaXmlComInfoDoObjetoConnector(xmlDoc, connector) {
    // ---------------------------------------------------------------------------  //
    // cria o nó de atributos estendidos da atividade //
    var ds = null, sets = null, ne = null;

    var ds = xmlDoc.getElementsByTagName("dataSet");

    if (null != ds)
        sets = xmlDoc.getElementsByTagName("set");

    if (null != sets)
        ne = getChildNodeById(sets, connector.from);

    if (null != ne) {
        var acoes = getChildNodeByName(ne.childNodes, "acoes");
        if (null == acoes)
            acoes = xmlDoc.createElement("acoes");

        var acao = obtemXmlAcaoConectorAtualizado(acoes, connector);
        acoes.appendChild(acao);
        ne.appendChild(acoes);
    }
}
