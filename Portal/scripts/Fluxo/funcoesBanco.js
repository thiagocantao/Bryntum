var atual = $("#" + $(window).data("atual"));
var anterior = $("#" + $(window).data("anterior"));

///função que especifica o item atual e o anterior
function especificarAtualAnterior(atual) {
    if ($(window).data("atual") != atual.attr("id")) {
        if ($(window).data("atual") != undefined)
        {
            $('.objetoSelecionado').removeClass("objetoSelecionado");
            $(window).data("anterior", $(window).data("atual"));
        }
        atual.addClass("objetoSelecionado");
        $(window).data("atual", atual.attr("id"));
    }
}
///Verifica as regras para adicionar ou executar uma ação
function verificaRegrasAcoes(tipo)
{
    var retorno = true;
    var atual = $("#" + $(window).data("atual"));
    var anterior = $("#" + $(window).data("anterior"));
    var origens, destinos;
    if(typeof tipo != "undefined")
    {
        switch(tipo)
        {
            case "conector":
            if($(window).data("atual") == undefined && $(window).data("anterior") == undefined && $(window).data("atual") == $(window).data("anterior"))
            {
                $.jfAlerta({
                    mensagem: "Não foi possível adicionar o conector, clique primeiramente no origem e depois clique no destino do conector.",
                    tipo: "alertaConfirmado",
                    titulo: "Operação incorreta!"
                });
                retorno = false;
            }
            if(atual.data("origem") != undefined && atual.data("origem").indexOf(anterior.attr("id")) >= 0)
            {
                $.jfAlerta({
                    mensagem: "Não foi possível adicionar o conecotr, pois já existe um conector no objeto de origem e objeto de destino escolhidos.",
                    tipo: "alertaConfirmado",
                    titulo: "Operação incorreta!"
                });
                retorno = false;
            }
            break;
        }
    }
    if(typeof $(window).data("atual") != "undefined" && typeof atual != "undefined")
    {
        switch(atual.data("tipo"))
        {
            case "fim":
                retorno = false;
            break;
            case "inicio":
                if(typeof $("#objetoInicio").data("destino") != "undefined")
                    retorno = false;
                if(tipo != "etapa")
                    retorno = false;
            break;
        }
    }
    if(typeof $(window).data("anterior") != "undefined" && typeof anterior != "undefined")
    {
        switch(anterior.data("tipo"))
        {
            case "":

                break;        
        }
    }
    return retorno;
}
function abrirJanelaEdicaoBotao(tipo, valores)
{
    var botaoId = $(window).data("botaoPainelClicado");
    var botao = $("#" + botaoId);
    var janela = $( "#painelBotaoEdicao" );
    var nomeExterno = botao.data("nomeExterno");
    janela.attr("title", tipo);
    janela.dialog( "open" );
    janela.data("tipo", tipo);
    adicionarCamposObjetoBotao(botaoId, janela, valores);
}
    
///
function abrirJanelaListaBotao()
{
    $( "#painelBotao" ).dialog({
        autoOpen: false,
        modal: true,
        resizable: true,
        closeText: 'Fechar',
        draggable: true,
        show: 'fade',
        minWidth: 700,
        dialogClass: "janelaDialogo",
        buttons: {
            "Adicionar":function(){
                abrirJanelaEdicaoBotao("Novo");
            },
            "Editar":function()
            {
                if(typeof $( "#painelBotao" ).data("tabela") == "object")
                {
                    var valores = $( "#painelBotao" ).data("tabela").jfValoresTabela();
                    if(typeof valores == "object")
                        abrirJanelaEdicaoBotao("Editar", valores);
                }
            },
            "Remover":function()
            {
                if(typeof $("#painelBotao").data("tabela") === "object")
                    $( "#painelBotao" ).data("tabela").jfRemoverTabela();
            },
            "Cancelar":function()
            {
                $(this).empty();
                $(this).dialog( "destroy" );
            }
        }
    });
}
///
function mostrarPainel(somenteFechar, somenteAbrir) {
    var larguraArea = $(window).width();
    var larguraPalco = larguraArea - 30;
    var leftPainel = larguraPalco + 20;
    var novoleftPainel = leftPainel - $("#painel").width() - 22;
    var retorno = false;
    if ($("#painel").position().left < leftPainel) {
        if(typeof somenteAbrir == "undefined" || !somenteAbrir)
        {
            var db = $("#" + $(window).data("atual"));
            $('.objetoSelecionadoDbc').removeClass("objetoSelecionadoDbc");
            $('.objetoSelecionado').removeClass("objetoSelecionado");
            $("#painel").animate({ 'left': leftPainel }, 'fast', function () {
                if ($(window).data("atual") != undefined) {
                    if (!db.hasClass("objetoSelecionado"))
                        db.addClass("objetoSelecionado");
                }
                retorno = false;
                if ($(this).children().length > 0)
                    $(this).empty();

                //Girar botão
                $("#menuPainel").css({
                    "transform": "rotate(" + 0 + "deg)"
				    , "-ms-transform": "rotate(" + 0 + "deg)"
				    , "-webkit-transform": "rotate(" + 0 + "deg)"
                });
            });  
        }  
    }
    else {
        if(typeof somenteFechar == "undefined" || !somenteFechar)
        {
            $("#painel").animate({ 'left': novoleftPainel }, 'slow', function () {
                var db = $("#" + $(window).data("atual"));
                $('.objetoSelecionado').removeClass("objetoSelecionado");
                if (!db.hasClass("objetoSelecionadoDbc"))
                    db.addClass("objetoSelecionadoDbc");
                adicionaCamposObjeto();
                retorno = true;
                //Girar botão
                $("#menuPainel").css({
                    "transform": "rotate(" + 180 + "deg)"
				    , "-ms-transform": "rotate(" + 180 + "deg)"
				    , "-webkit-transform": "rotate(" + 180 + "deg)"
                });
                esconderAdicionais();
            });
        }
    }
    return retorno;
};
///
function mostrarIniciais() {
    $('img').css('display', 'inline').fadeIn(1500, function () {
            tamanhosAreas();
    });
};
///
function tamanhosAreas() {
    var alturaArea = $(window).height();
    var larguraArea = $(window).width();
    var alturaPalco = alturaArea - 35;
    var larguraPalco = larguraArea - 30;
    var leftPainel = larguraPalco + 20;
    $("#area").css({ 'height': alturaArea, 'width': larguraArea });
    $("#menuAbrirPainel").css({ 'left': larguraPalco - 30 });
    $("#menuPalco").css({ 'left': larguraPalco - 80, 'top': alturaPalco - 45 });
    $("#palco").animate({ 'width': larguraPalco, 'height': alturaPalco }, 'slow');
    $("#painel").css({ 'width': 400, 'height': alturaPalco, 'left': leftPainel }, 'slow');
    
    ///Controle conteúdo
    var cl = $("#conteudo").width();
    var ca = $("#conteudo").height();
    var intervalo = 20;
    if(ca >= alturaPalco || cl >= (larguraPalco - intervalo))
    {
        $("#conteudo").draggable('enable');
        $("#conteudo").css("cursor", "pointer");
    }
    else
    {
        $("#conteudo").css({'left': 0, 'top':0, 'cursor':'auto'});
        $("#conteudo").draggable('disable');
    }
};
///
function valorGet(name) {
    var url = window.location.search.replace("?", "");
    var itens = url.split("&");

    for (n in itens) {
        if (itens[n].match(name)) {
            return decodeURIComponent(itens[n].replace(name + "=", ""));
        }
    }
    return null;
}
///Verifica as linhas ligas ao objeeto atual e realiza a atualização
function atualizarLinhas(objeto)
{
    var todos = retornaTodosConectores(objeto);
    if(todos != undefined)
        origemDestinoConector(todos, objeto);
}
///
function retornaTodosConectores(objeto) {
    var destinos, origens;
    var todos = Array();
    if (objeto.data("conectorDestino") != undefined && objeto.data("conectorDestino") != "") {
        destinos = objeto.data("conectorDestino").split("§");
    }
    if (objeto.data("conectorOrigem") != undefined && objeto.data("conectorOrigem") != "") {
        origens = objeto.data("conectorOrigem").split("§");
    }
    if (origens != undefined)
        todos = todos.concat(origens);
    if (destinos != undefined)
        todos = todos.concat(destinos);
    return todos;
}
///Verificar e aplica a busca e atualização dos conectores do objeto informado
function origemDestinoConector(conectores, objeto) {
    var quantidade = conectores != null && conectores != undefined?conectores.length:0;
    var conector, objetoTipo, atual, anterior;
    for(var i in conectores)
    {
        conector = $("#" + conectores[i]);
        if(conector.data("origem") == objeto.attr("id") || conector.data("destino") == objeto.attr("id"))
        {
            anterior = $("#" + conector.data("origem"));
            atual = $("#" + conector.data("destino"));
            if(conector.find('p[id^="texto_"]').size() > 0)
                $.jfLigarConector(conector, atual, anterior, false, conector.find('p[id^="texto_"]'));
            else
                $.jfLigarConector(conector, atual, anterior, false);
        }
    }
            
}  
///
function buscarValoresTabelaBotaoClicado(botao, janela)
{
    var tipo = retornarTipoBotaoCampo(botao.attr("id"));
    var campos = $(window).data("nomesGrupos")[tipo].split("/");
    var nomesExternos = new Array();
    var botaoR = botao;
    var objeto = $("#" + $(window).data("atual"));
    for(var i in campos)
    {
        nomesExternos.push($(window).data("nomesCamposGrupos")[campos[i]][1].replace(":", ""));
    }
    return $.jfCriarTabela({
        div:janela
        , titulos:nomesExternos
        , valores:objeto.data(botaoR.attr("id"))
        , ativarLinhaClicada:true
        , ativarLinhaZebrada: true
        , tituloTabela: "Lista de " + botao.data("nomeExterno")
    });
}
///Salvar alterações ou novo dados
function salvarDadosNovosEditados(botao, tipoJanela, valoresRecebido, tabela)
{
    var tipo = retornarTipoBotaoCampo(botao);
    var campos = $(window).data("nomesGrupos")[tipo].split("/");
    var valores = new Array();
    for(var i in campos)
    {
        var nome = "janela§campo§" + $(window).data("nomesCamposGrupos")[campos[i]][0];
        var valor = valoresRecebido[nome];
        valores.push(valor);
    }
    if(tipoJanela == "Novo")
    {
        tabela.jfAdicionarTabela({valores:valores});
    }
    else if(tipoJanela == "Editar")
    {
        tabela.jfAlterarTabela({valores:valores});
    }
}
///Adiciona os campos no dialogo do botão
function adicionarCamposObjetoBotao(botao, janela, valores)
{
    var tipo = retornarTipoBotaoCampo(botao);
    var campos = $(window).data("nomesGrupos")[tipo].split("/");
    criarCamposPainel(campos, janela, "janela§", valores);
}
///
function retornarTipoBotaoCampo(id)
{
    var tipo;
    switch(id)
    {
        case "botaoAcessos":
            tipo = "janelaAcessos";
        break;
		case "botaoAcoesA":
            tipo = "janelaAcoes";
        break;
        case "botaoAcompanhamentoM":
            tipo = "janelaMensagens";
        break;
        case "botaoFormularios":
            tipo = "janelaFormularios";
        break;
        case "botaoPerfisN":
            tipo = "janelaPerfis";
        break;
		case "botaoTextoD":
            tipo = "janelaMensagens";
        break;
    }
    return tipo;
}
///Adiciona os campos utlizados por cada tipo de objeto
function adicionaCamposObjeto()
{
    var p = $("#painel");
    var atual = $("#" + $(window).data("atual"));
    var campos = $(window).data("nomesGrupos")[atual.data("tipo")].split("/");
    criarCamposPainel(campos, p, "");
}
///
function criarCamposPainel(campos, recipiente, prefixoR, valores)
{
    var lista = document.createElement('ul');
    var seta = document.createElement('span');
    var p = recipiente;
    var atual = $("#" + $(window).data("atual"));
    var id = atual.attr("id");
    var campo, divCampo, nome, tipo;
    if(p.children().length > 0)
        p.empty();
    lista.id = prefixoR + "lista" + id;
    lista.className = "listaPainel";
    lista.style.width = (p.width() - 18) + "px";
    p.append(lista);
    $(lista).sortable();
    $(lista).disableSelection();
    var prefixo = prefixoR == "janela§" ? "janela§campo§" : "";
    for(var i in campos)
    {
        nome = $(window).data("nomesCamposGrupos")[campos[i]][0];
        var item = document.createElement('li');
        var label = document.createElement('label');
        var valor = atual.data(nome) == undefined?"":atual.data(nome);
        if(valor == "" && typeof valores == "object")
            valor = valores[i];
        item.className = "ui-state-default";
        item.id = prefixo + "item" + nome;
        $(lista).append(item);
        tipo = nome.substring(0, 5);
        campo = retornaTipoCampoPainel(tipo);
        campo.name = campo.id = prefixo + nome;
        campo.style.width = (p.width() - 52) + "px";
        campo.className = "camposPainel";
        label.setAttribute("for", nome);
        label.id = prefixo + "label" + nome;
        label.innerHTML = $(window).data("nomesCamposGrupos")[campos[i]][1];
        var nomeExterno = $(window).data("nomesCamposGrupos")[campos[i]][1];
        if(tipo != "botao")
        {
            $(item).append(label);
            $(item).append("<br>");
        }
        $(item).append(campo);
        if(tipo == "combo")
        {
                valoresCombosCamposPainel($(campo), nome, valor);
        }
        else
        {
            switch(tipo)
            {
                case "botao":
                    $(campo).text(nomeExterno.replace(":", ""));
                break;
                case "check":
                    $(campo).val(nomeExterno.replace(":", ""));
                break;
            }
            $(campo).jfValorCampo({valor:valor, colocar:true});
                    
        }
        $(item).fadeIn("slow");
        $(campo).data("nomeExterno", nomeExterno.replace(":", ""));
                
    }
    var nomeFocus = $("#" + prefixo + $(window).data("nomesCamposGrupos")[campos[0]][0]);
    nomeFocus.focus();  
}
///Retorna o tipo de elemento que será criado
function retornaTipoCampoPainel(tipo)
{
    var campo;
    switch(tipo)
    {
        case "botao":
            campo = document.createElement('button');
            campo.setAttribute('type', 'button');
        break;
        case "caixa":
            campo = document.createElement('textarea');
        break;
        case "texto":
            campo = document.createElement('input');
        break;
        case "numer":
            campo = document.createElement('input');
        break;
        case "combo":
            campo = document.createElement('select');
        break;
        case "check":
            campo = document.createElement('input');
            campo.setAttribute('type', 'checkbox');
        break;
    }
    return campo;
}
///
function valoresCombosCamposPainel(campo, nome, valor)
{
    var dataIn = { codigoWorkflow: '743' };
    var nomeRecebido = nome;
    var campoRecebido = campo;
    var valorRecebido = valor;
    $.ajax({
        url: "../Servicos/Service.svc/carregarCombosFluxo",
        data: dataIn,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var dados = jQuery.parseJSON(data.d);
                switch(nomeRecebido)
                {
                    case "comboAcaoA":
                        var quantidade = dados.AcoesAutomaticasWf.length;
                        if(quantidade > 0)
                        {
                            for (i = 0; i < quantidade; i++) {
                                campoRecebido.append(new Option(dados.AcoesAutomaticasWf[i].nome, dados.AcoesAutomaticasWf[i].codigo));
                            }
                        }
                    break;
                    case "comboAcessos":
                        campoRecebido.append(new Option('Ação', 'Ação'));
                        campoRecebido.append(new Option('Consulta', 'Consulta'));
                    break;
                    case "comboEtapa":
                        var quantidade = Number($(window).data("quantidadeEtapas"));
                        for(var i = 0;i<quantidade;i++)
                        {
                            var obj = $("#objetoEtapa" + i);
                            campoRecebido.append(new Option(obj.data("titulo"), obj.attr("id")));
                        }
                    break;
                    case "comboFormulario":
                        var quantidade = dados.ModeloFormulario.length;
                        if(quantidade > 0)
                        {
                            for (i = 0; i < quantidade; i++) {
                                campoRecebido.append(new Option(dados.ModeloFormulario[i].nome, dados.ModeloFormulario[i].codigo));
                            }
                        }
                    break;
                    case "comboPerfil":
                        var quantidade = dados.PerfisWf.length;
                        if(quantidade > 0)
                        {
                            for (i = 0; i < quantidade; i++) {
                                campoRecebido.append(new Option(dados.PerfisWf[i].nome, dados.PerfisWf[i].codigo));
                            }
                        }
                    break;
                    case "comboPrazoM":
                        campoRecebido.append(new Option('Ínicio do Fluxo', 'IFLX'));
                        campoRecebido.append(new Option('Ìnicio da Etapa', 'IETP'));
                    break;
                    case "comboPrazoTipo":
                        campoRecebido.append(new Option('Horas', 'horas'));
                        campoRecebido.append(new Option('Dias', 'dias'));
                        campoRecebido.append(new Option('Dias úteis', 'diasU'));
                        campoRecebido.append(new Option('Semanas', 'semanas'));
                        campoRecebido.append(new Option('Meses', 'meses'));
                    break;
                    case "comboTipoM":
                        campoRecebido.append(new Option('Ações', 'A'));
                        campoRecebido.append(new Option('Acompanhamento', 'C'));
                    break;
                }
                $(campoRecebido).jfValorCampo({valor:valorRecebido, colocar:true});
                return true;
            }
            return false;
        },
        error: function (request, status, error) {
            $.jfAlerta({
                mensagem: "Não foi realizado a conexão com a base de dados.",
                tipo: "alertaConfirmado",
                titulo: "Sem conexão!"
            });
            console.log(status.toString() + " na resposta do webservice de retornar tabelas: Código = " + request.status + " Mensagem = " + request.statusText);
            return false;
        }
    });
    return false;
}
///
function excluirObjetoFluxo() {
    var atual = $("#" + $(window).data("atual"));
    var mensagem, conectores, observacao, nomeConector;
    mostrarPainel(true);
    if (atual.data("tipo") != "inicio") {
        if (atual.data("tipo") != "conector") {
            conectores = retornaTodosConectores(atual);
            nomeConector = conectores == null && conectores == undefined && conectores.length <= 1 ? "conector" : "conectores";
        }
        if (conectores != undefined && conectores != null && conectores.length > 0) {
            mensagem = "O objeto selecionado possuí " + conectores.length + " " + nomeConector + ", deseja realmente excluir o objeto selecionado ?";
            observacao = "A exclusão do objeto acarretará na exclusão de todos os conectores ligados a ele.";
        }
        else {
            mensagem = "Deseja realmente excluir o objeto selecionado ?";
        }
        $.jfAlerta({
            mensagem: mensagem,
            observacao: observacao,
            tipo: "confirmar",
            titulo: "Confirmar exclusão!",
            aberto: function () {
                $('.objetoSelecionado').removeClass("objetoSelecionado");
                atual.addClass("objetoSelecionadoExclusao");
            },
            botaoConfirmar: function () {
                removerReferenciasDataObjeto(atual);
                if (conectores != undefined && conectores != null & conectores.length > 0) {
                    for (var c in conectores) {
                        var conector = $("#" + conectores[c]);
                        removerReferenciasDataObjeto(conector);
                        conector.remove();
                    }
                }
                atual.remove();
                $('.objetoSelecionadoExclusao').removeClass("objetoSelecionadoExclusao");
                atual.addClass("objetoSelecionado");
            },
            botaoCancelar: function () {
                $('.objetoSelecionadoExclusao').removeClass("objetoSelecionadoExclusao");
                atual.addClass("objetoSelecionado");
            }
        });
    }
    else {
        $.jfAlerta({
            mensagem: "O objeto selecionado não pode ser excluído.",
            tipo: "alertaConfirmado",
            titulo: "Operação inválida!"
        });
    }
}
///
function removerReferenciasDataObjeto(atual, apenasLimpar) {
    if (atual.data("tipo") == "conector") {
        var origens = atual.data("origem") != undefined ? atual.data("origem").split("§") : new Array();
        var destinos = atual.data("destino") != undefined ? atual.data("destino").split("§") : new Array();
        if (origens.length > 0) {
            for (var o in origens) {
                var origem = $("#" + origens[o]);
                var oDestinos = origem.data("conectorDestino") != undefined ? origem.data("conectorDestino").split("§") : new Array();
                if (oDestinos.length > 0 && origem.data("conectorDestino").indexOf(atual.attr("id")) >= 0) {
                    oDestinos.splice(origem.data("conectorDestino").indexOf(atual.attr("id")), 1);
                    origem.data("conectorDestino", oDestinos.join("§"));
                }
            }
        }
        if (destinos.length > 0) {
            for (var d in destinos) {
                var destino = $("#" + destinos[d]);
                var dOrigens = destino.data("conectorOrigem") != undefined ? destino.data("conectorOrigem").split("§") : new Array();
                if (dOrigens.length > 0 && destino.data("conectorOrigem").indexOf(atual.attr("id")) >= 0) {
                    dOrigens.splice(destino.data("conectorOrigem").indexOf(atual.attr("id")), 1);
                    destino.data("conectorOrigem", dOrigens.join("§"));
                }
            }
        }
    }
    else {
        var origens = atual.data("origem") != undefined ? atual.data("origem").split("§") : new Array();
        var destinos = atual.data("destino") != undefined ? atual.data("destino").split("§") : new Array();
        if (origens.length > 0) {
            for (var o in origens) {
                var origem = $("#" + origens[o]);
                var oDestinos = origem.data("destino") != undefined ? origem.data("destino").split("§") : new Array();
                if (oDestinos.length > 0 && origem.data("destino").indexOf(atual.attr("id")) >= 0) {
                    oDestinos.splice(origem.data("destino").indexOf(atual.attr("id")), 1);
                    origem.data("destino", oDestinos.join("§"));
                }
            }
        }
        if (destinos.length > 0) {
            for (var d in destinos) {
                var destino = $("#" + destinos[d]);
                var dOrigens = destino.data("origem") != undefined ? destino.data("origem").split("§") : new Array();
                if (dOrigens.length > 0 && destino.data("origem").indexOf(atual.attr("id")) >= 0) {
                    dOrigens.splice(destino.data("origem").indexOf(atual.attr("id")), 1);
                    destino.data("destino", dOrigens.join("§"));
                }
            }
        }
    }
    if (apenasLimpar == undefined || !apenasLimpar) {
        var numero = $(window).data("nomesObjetos").indexOf(atual.attr("id"));
        if (numero >= 0)
            $(window).data("nomesObjetos").splice(numero, 1);
    }
}
///
function salvarFluxo() {
    console.log("Salvar");
}
///
function geraNovoObjeto(tipo, id) {
    if ($(window).data("menuAtivo")) {
        if (verificaRegrasAcoes(tipo)) {
            $(window).data("menuAtivo", false);
            adicionarObjeto(tipo);
            if (tipo != "conector") {
                setTimeout(function () {
                    adicionarObjeto("conector");
                }, 200);
            }
        }
        setTimeout(function () {
            if (id != $("#" + $(window).data("atual")).attr("id")) {
                $(window).data("menuAtivo", true);
                mostrarPainel(false, true);
            }
        }, 200);
    }
}
///
function valoresTabelaObjetosAtuais() {
    var retorno = new Array();
    var valores = new Array();
    retorno.push(new Array("Objetos nome", "Tipo", "Sequência"));
    var objetos = new $(window).data("nomesObjetos");
    for(o in objetos)
    {
        var objeto = $("#" + objetos[o]);
        if (objeto.data("tipo") != "conector" && objeto.data("tipo") != "inicio") {
            if (objeto.data("tipo") == "etapa")
                valores.push(new Array(objeto.data("titulo"), objeto.data("tipoExterno"), (Number(o) + 1) + "º"));
            else
                valores.push(new Array(objeto.data("textoDescricao"), objeto.data("tipoExterno"), (Number(o) + 1) + "º"));
        }
    }
    retorno.push(valores);
    return retorno;
}
///Verifica se ã opção publicar estará ativa ou não
function verificarPublicacaoFluxo() {
    $("#menuPublicar").hide();
}
///Exibe o menu do objeto
function mostrarMenuObjeto(alvo) {
    var menu = $("#menuObjeto");
    var pa = $("#conteudo");
    var posPa = pa.position();
    var pos = alvo.position();
    var esq = pos.left + posPa.left + alvo.width() + 5;
    $("#menuEsquerdo:visible").hide();
    menu.css({ 'left': esq, 'top': pos.top + posPa.top });
    menu.show();
    opcoesMenuObjeto();
}
///Exibe o menu do objeto
function mostrarMenuObjetoAdicionar(alvo) {
    var menu = $("#menuObjeto");
    var ad = $("#menuEsquerdo");
    var pos = menu.position();
    var esq = pos.left + menu.width() + 5;
    ad.css({ 'left': esq, 'top': pos.top });
    ad.show();
    opcoesMenuObjetoAdicionar();
}
///Controle de o que deverá ser mostrado no menu objeto
function opcoesMenuObjeto() {
    var atual = $("#" + $(window).data("atual"));
    $("#menuObjeto img").show();
    switch(atual.data("tipo")) 
    {
        case "conector":
            $("#menuObjeto #menuAdicionar").hide();
            break;
        case "inicio":
            if(atual.data("destino") != undefined)
                $("#menuObjeto #menuAdicionar").hide();
            $("#menuObjeto #menuConector").hide();
            $("#menuObjeto #menuExcluir").hide();
            break;
        case "fim":
            $("#menuObjeto #menuAdicionar").hide();
            $("#menuObjeto #menuConector").hide();
            break;
        default:
            $("#menuObjeto #menuConector").hide();
            break;
    }

}
///Controle de o que deverá ser mostrado no meu objeto adicionar
function opcoesMenuObjetoAdicionar() {
    var atual = $("#" + $(window).data("atual"));
    $("#menuEsquerdo img").show();
}
///Mostrar opções de alteração de origem e destino connector
function mostrarAlteracaoOrigemDestino(objeto) {
    if ($(window).data("trocarDestinoOrigemConector") == undefined) {
        $.jfAlerta({
            mensagem: "Escolha qual deseja alterar.",
            tipo: "confirmar",
            titulo: "Origem ou Destino!",
            redimencionar: true,
            mover: true,
            mostrarModal: false,
            posicao: {
                my: "left",
                at: "right",
                of: objeto
            },
            aberto: function () {
                $('.objetoSelecionado').removeClass("objetoSelecionado");
                if (!objeto.hasClass("objetoSelecionadoDbc"))
                    objeto.addClass("objetoSelecionadoDbc");
            },
            fechado: function () {
                $('.objetoSelecionadoDbc').removeClass("objetoSelecionadoDbc");
                if (!objeto.hasClass("objetoSelecionado"))
                    objeto.addClass("objetoSelecionado");
            },
            botoes: {
                "Origem": function () {
                    var valores = valoresTabelaObjetosAtuais();
                    var tabela = $.jfCriarTabela({
                        titulos: valores[0],
                        valores: valores[1],
                        ativarLinhaClicada: true,
                        tituloTabela: "Objeto do Fluxo para Origem",
                        div: $("#jfJanelaAlerta"),
                        cliqueLinha: function () {
                            var numero = Number(tabela.jfValoresTabela()[2].replace("º", "")) - 1;
                            objeto.data("novaOrigem", $(window).data("nomesObjetos")[numero]);
                        }
                    });
                },
                "Destino": function () {
                    var valores = valoresTabelaObjetosAtuais();
                    var tabela = $.jfCriarTabela({
                        titulos: valores[0],
                        valores: valores[1],
                        ativarLinhaClicada: true,
                        tituloTabela: "Objeto do Fluxo para Destino",
                        div: $("#jfJanelaAlerta"),
                        cliqueLinha: function () {
                            var numero = Number(tabela.jfValoresTabela()[2].replace("º", "")) - 1;
                            objeto.data("novoDestino", $(window).data("nomesObjetos")[numero]);
                        }
                    });
                }
            },
            botaoCancelar: function () {
                if (objeto.data("novaOrigem") != undefined)
                    objeto.removeData("novaOrigem");
                if (objeto.data("novoDestino") != undefined)
                    objeto.removeData("novoDestino");
            },
            botaoConfirmar: function () {
                var nOrigem, nDestino;
                if (objeto.data("novaOrigem") != undefined) {
                    nOrigem = objeto.data("novaOrigem");
                    objeto.removeData("novaOrigem");
                }
                else {
                    nOrigem = objeto.data("origem");
                }
                if (objeto.data("novoDestino") != undefined) {
                    nDestino = objeto.data("novoDestino");
                    objeto.removeData("novoDestino");
                }
                else {
                    nDestino = objeto.data("destino");
                }
                if (nOrigem != nDestino) {
                    removerReferenciasDataObjeto(objeto, true);
                    var atual = $("#" + nDestino);
                    var anterior = $("#" + nOrigem);
                    var interno = objeto.find('p[id^="texto_"]').size() > 0 ? objeto.find('p[id^="texto_"]') : undefined;
                    gravarLigacoesConectorAtualAnterior(objeto, atual, anterior);
                    $.jfLigarConector(objeto, atual, anterior, true, interno);
                }
            }
        });
    }
}
///Esconde opções adicionais (retorna as opções padrão de inicio)
function esconderAdicionais() {
    $("#menuEsquerdo:visible").hide();
    $("#menuObjeto:visible").hide();
}
///Ajusta o conteudo do palco entre o modo encaixado no canto e o modo visão total
function mudarModoVisaoConteudo() {
    var pa = $("#palco");
    var co = $("#conteudo");
    if (($(window).data("modoAmpliacao") == undefined || $(window).data("modoAmpliacao") == "normal") && ($(window).data("modoVisao") == undefined || $(window).data("modoVisao") == "normal")) {
        $(window).data("modoVisao", "total");
        var direitaSobra = co.width() - pa.width();
        var baixoSobra = co.height() - pa.height();
        var xy, porcentagem, escala;
        var esquerda = 0;
        var cima = 0;
        if (direitaSobra > baixoSobra) {
            porcentagem = (direitaSobra * 100) / pa.width();
            escala = (1 * porcentagem) / 100
            xy = 1 - escala;
            esquerda = (direitaSobra / 2) * -1;
            cima = (baixoSobra / 2) * -1;
        }
        if (baixoSobra > direitaSobra) {
            porcentagem = (baixoSobra * 100) / pa.height();
            escala = (1 * porcentagem) / 100
            xy = 1 - escala;
            esquerda = (direitaSobra / 2) * -1;
            cima = (baixoSobra / 2) * -1;
        }
        co.css({
            'left': esquerda,
            'top': cima,
            'transform': 'scale(' + xy + ',' + xy + ')',
            '-ms-transform': 'scale(' + xy + ',' + xy + ')',
            '-webkit-transform': 'scale(' + xy + ',' + xy + ')'
        });
        $.jfAlerta({
            mensagem: "Modo de visão total do conteúdo.",
            tipo: "alertaAviso",
            titulo: "Visão total!"
        });
    }
    else 
    {
        co.css({
            'left':0,
            'top':0,
            'transform': 'scale(1,1)',
            '-ms-transform': 'scale(1,1)',
            '-webkit-transform': 'scale(1,1)'
        });
        $(window).data("modoVisao", "normal");
        $(window).data("modoAmpliacao", "normal");
        $.jfAlerta({
            mensagem: "Modo de visão normal.",
            tipo: "alertaAviso",
            titulo: "Visão normal!"
        });
    }
}
///Ampliar o conteudo do palco em três tamanhos diferentes
function ampliarConteudoPalco() {
    var pa = $("#palco");
    var co = $("#conteudo");
    var paPo = pa.position();
    var coPo = co.position();
    var xx = (pa.width() - co.width()) / 2;
    var yy = (pa.height() - co.height()) / 2;
    var maior = (pa.width() < co.width()) || (pa.height() < co.height());
    if ($(window).data("modoAmpliacao") == undefined || $(window).data("modoAmpliacao") == "normal") {
        $("#conteudo").draggable('enable');
        co.css({
            'left': xx,
            'top': yy,
            'transform': 'scale(1.5,1.5)',
            '-ms-transform': 'scale(1.5,1.5)',
            '-webkit-transform': 'scale(1.5,1.5)'
        });
        $(window).data("modoAmpliacao", "tipo1");
        $.jfAlerta({
            mensagem: "Aumento de 50% do conteúdo.",
            tipo: "alertaAviso",
            titulo: "Aumento 50%!"
        });
    }
    else {
        switch ($(window).data("modoAmpliacao"))
        {
            case "tipo1":
                $("#conteudo").draggable('enable');
                co.css({
                    'left': xx,
                    'top': yy,
                    'transform': 'scale(2,2)',
                    '-ms-transform': 'scale(2,2)',
                    '-webkit-transform': 'scale(2,2)'
                });
                $(window).data("modoAmpliacao", "tipo2");
                $.jfAlerta({
                    mensagem: "Aumento de 100% do conteúdo.",
                    tipo: "alertaAviso",
                    titulo: "Aumento 100%!"
                });
                break;
            case "tipo2":
                $("#conteudo").draggable('enable');
                co.css({
                    'left': xx,
                    'top': yy,
                    'transform': 'scale(2.5,2.5)',
                    '-ms-transform': 'scale(2.5,2.5)',
                    '-webkit-transform': 'scale(2.5,2.5)'
                });
                $(window).data("modoAmpliacao", "tipo3");
                $.jfAlerta({
                    mensagem: "Aumento de 150% do conteúdo.",
                    tipo: "alertaAviso",
                    titulo: "Aumento 150%!"
                });
                break;
            case "tipo3":
                if (!maior)
                    $("#conteudo").draggable('disable');
                co.css({
                    'left': 0,
                    'top': 0,
                    'transform': 'scale(1,1)',
                    '-ms-transform': 'scale(1,1)',
                    '-webkit-transform': 'scale(1,1)'
                });
                $(window).data("modoAmpliacao", "normal");
                $(window).data("modoVisao", "normal");
                $.jfAlerta({
                    mensagem: "Tamanho normal do conteúdo.",
                    tipo: "alertaAviso",
                    titulo: "Sem aumento!"
                });
                break;
        }
    }
}