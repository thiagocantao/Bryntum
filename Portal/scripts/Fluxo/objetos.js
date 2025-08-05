var tiposObjetosFluxo = [
		"inicio"
	, "etapa"
	, "conector"
	, "tempo"
	, "juncao"
	, "desjuncao"
	, "fim"
];

///
function iniciarFluxo() {
    var novo = true;
    if ($(window).data("codigoWorkflow") == null || $(window).data("tipoWorkflow") == null || ($(window).data("codigoWorkflow") == "0" && $(window).data("tipoWorkflow") == "0")) {
        novo = true;
    }
    else
        novo = false;
    $(window).data("fluxoNovo", novo);
    if (!novo) {
        var dataIn = { codigoWorkflow: $(window).data("codigoWorkflow") };
        $.ajax({
            url: "../Servicos/Service.svc/carregarFluxo",
            data: dataIn,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    var xml = $.parseXML(data.d);
                    criarObjetos(xml);
                }
            },
            error: function (request, status, error) {
                $.jfAlerta({
                    mensagem: "Não foi realizado a conexão com a base de dados, para carregar os objetos do fluxo.",
                    tipo: "alertaConfirmado",
                    titulo: "Sem conexão!"
                });
                console.log(status.toString() + " na resposta do webservice de retornar o fluxo: Código = " + request.status + " Mensagem = " + request.statusText);
                return;
            }
        });
    }
    else {
        adicionarObjeto("inicio");
    }
}
///Função para criar o objeto(div) para ser adicionado posteriormente no palco(div)
function criarObjeto (x, y, tipo, titulo, tituloIterno)
    {
    var id, classe, tituloPadrao, textoInterno, tituloIternoPadrao, idTipo, tipoExterno;
    //Controles de quantidades de cada tipo de objeto no palco
    var quantidadeEtapas = $(window).data("quantidadeEtapas")==undefined?0:$(window).data("quantidadeEtapas");
    var quantidadeConectores = $(window).data("quantidadeConectores")==undefined?0:$(window).data("quantidadeConectores");
    var quantidadeTempos = $(window).data("quantidadeTempos")==undefined?0:$(window).data("quantidadeTempos");
    var quantidadeJuncoes = $(window).data("quantidadeJuncoes")==undefined?0:$(window).data("quantidadeJuncoes");
    var quantidadeDesjuncoes = $(window).data("quantidadeDesjuncoes")==undefined?0:$(window).data("quantidadeDesjuncoes");
    var quantidadeFins = $(window).data("quantidadeFins") == undefined ? 0 : $(window).data("quantidadeFins");
    switch(tipo)
    {
        case "novaInicio":
        case "inicio":
            classe = "objetoInicio";
            id = "objetoInicio";
            idTipo = 0;
            tituloPadrao = "Início";
            tipoExterno = "Início";
            break;
        case "novaEtapa":
        case "etapa":
            classe = "objetoEtapa";
            id = "objetoEtapa" + quantidadeEtapas;
            idTipo = quantidadeEtapas;
            tituloPadrao = "Etapa do fluxo";
            tituloIternoPadrao = "Nome da etapa";
            $(window).data("quantidadeEtapas", $(window).data("quantidadeEtapas") == undefined?1:Number($(window).data("quantidadeEtapas")) + 1);
            textoInterno = document.createElement('p');
            tipoExterno = "Etapa";
        break;
        case "novaConector":
        case "conector":
            classe = "objetoConector";
            id = "objetoConector" + quantidadeConectores;
            idTipo = quantidadeConectores;
            tituloPadrao = "Enviar";
            tituloIternoPadrao = "Título do enviar";
            $(window).data("quantidadeConectores", $(window).data("quantidadeConectores") == undefined?1:Number($(window).data("quantidadeConectores")) + 1);
            textoInterno = document.createElement('p');
            tipoExterno = "Conector";
        break;
        case "novaTempo":
        case "tempo":
            classe = "objetoTempo";
            id = "objetoTempo" + quantidadeTempos;
            idTipo = quantidadeTempos;
            tituloPadrao = "Temporizador do fluxo";
            $(window).data("quantidadeTempos", $(window).data("quantidadeTempos") == undefined ? 1 : Number($(window).data("quantidadeTempos")) + 1);
            tipoExterno = "Temporizador";
        break;
        case "novaJuncao":
        case "juncao":
            classe = "objetoJuncao";
            id = "objetoJuncao" + quantidadeJuncoes;
            idTipo = quantidadeJuncoes;
            tituloPadrao = "Junção do fluxo";
            $(window).data("quantidadeJuncoes", $(window).data("quantidadeJuncoes") == undefined ? 1 : Number($(window).data("quantidadeJuncoes")) + 1);
            tipoExterno = "Junção";
        break;
        case "novaDesjuncao":
        case "desjuncao":
            classe = "objetoDesjuncao";
            id = "objetoDesjuncao" + quantidadeDesjuncoes;
            idTipo = quantidadeDesjuncoes;
            tituloPadrao = "Desjunção do fluxo";
            $(window).data("quantidadeDesjuncoes", $(window).data("quantidadeDesjuncoes") == undefined ? 1 : Number($(window).data("quantidadeDesjuncoes")) + 1);
            tipoExterno = "Desjunção";
        break;
        case "novaFim":
        case "fim":
            classe = "objetoFim";
            id = "objetoFim" + quantidadeFins;
            idTipo = quantidadeFins;
            tituloPadrao = "Fim do fluxo";
            $(window).data("quantidadeFins", $(window).data("quantidadeFins") == undefined ? 1 : Number($(window).data("quantidadeFins")) + 1);
            tipoExterno = "Fim";
        break;
    }
            
    var pixel = document.createElement('div');
    pixel.id = id;
    pixel.className = "objetos " + classe;
    pixel.setAttribute("tabindex", "0");
    if(tipo == "inicio" || tipo == "novaInicio")
        pixel.title = tituloPadrao;
    else
        pixel.title = titulo==undefined?tituloPadrao:titulo;
        
    pixel.style.left = x + 'px';
    pixel.style.top = y + 'px';
    $("#conteudo").append(pixel);
    if ($(window).data("nomesObjetos") == undefined)
        $(window).data("nomesObjetos", new Array());
    $(window).data("nomesObjetos").push(pixel.id);
    if(typeof textoInterno != "undefined" && titulo != "")
    {
        textoInterno.id = "texto_" + pixel.id;
        $(pixel).append(textoInterno);
        $(textoInterno).html(tituloIterno==undefined?tituloIternoPadrao:tituloIterno);
    }
    if(tipo != "novaConector")
    {
        especificarAtualAnterior($("#" + pixel.id));
    }
    else if(tipo == "novaConector")
    {
        var seta = document.createElement('img');
        seta.id = "seta" + id;
        seta.src = "../imagens/Fluxo/setaConectorL.png";
        $(pixel).append(seta);
    }
    //Datas*************
    if (tipo.indexOf('nova') >= 0) {
        $(pixel).data("tipo", tipo.substring(4, tipo.length).toLowerCase());
    }
    else {
        $(pixel).data("tipo", tipo);
    }
    $(pixel).data("titulo", titulo == undefined ? tituloPadrao : titulo);
    $(pixel).data("idTipo", idTipo);
    $(pixel).data("tipoExterno", tipoExterno);
    //***********

    $(pixel).fadeIn("fast");
    return $(pixel);
}

    ///Função para adicionar o objeto no palco(div)
function adicionarObjeto(tipo){
    var x = 0;
    var y = 0;
    var nome, objeto;
    var atual = $("#" + $(window).data("atual"));
    var anterior = $("#" + $(window).data("anterior"));
    if($(window).data("atual") != undefined)
    {
        x = $("#" + $(window).data("atual")).position().left + $("#" + $(window).data("atual")).width() + 10;
        y = $("#" + $(window).data("atual")).position().top + $("#" + $(window).data("atual")).height() + 10;;
    }
    switch(tipo)
    {
        case "inicio":
            objeto = criarObjeto(x, y, "novaInicio");
            var altura = ($("#conteudo").height() / 2) - ($("#objetoInicio").height() / 2);
            $("#objetoInicio").animate({ "top": altura + "px" });
            $(window).data("objetoInicial", "true");
        break;
        case "etapa":
            objeto = criarObjeto(x, y, "novaEtapa");
        break;
        case "conector":
            objeto = criarObjeto(x, y, "novaConector");
            if (objeto.find('p[id^="texto_"]').size() > 0) {
                $.jfLigarConector(objeto, atual, anterior, true, objeto.find('p[id^="texto_"]'));
            }
            else {
                $.jfLigarConector(objeto, atual, anterior, true);
            }
            gravarLigacoesConectorAtualAnterior(objeto, atual, anterior);
        break;
        case "tempo":
            objeto = criarObjeto(x, y, "novaTempo");
        break;
        case "juncao":
            objeto = criarObjeto(x, y, "novaJuncao");
        break;
        case "desjuncao":
            objeto = criarObjeto(x, y, "novaDesjuncao");
        break;
        case "fim":
            objeto = criarObjeto(x, y, "novaFim");
        break;
    }
}

function gravarLigacoesConectorAtualAnterior(objeto, atual, anterior)
{
    nome = objeto.attr("id");
    objeto.data("destino", atual.attr("id"));
    objeto.data("origem", anterior.attr("id"));
    if (atual.data("origem") == undefined || atual.data("origem") == "")
        atual.data("origem", anterior.attr("id"));
    else {
        if (atual.data("origem").indexOf(anterior.attr("id")) < 0)
            atual.data("origem", atual.data("origem") + "§" + anterior.attr("id"));
    }

    if (anterior.data("destino") == undefined || anterior.data("destino") == "")
        anterior.data("destino", atual.attr("id"));
    else {
        if (anterior.data("destino").indexOf(atual.attr("id")) < 0)
            anterior.data("destino", anterior.data("destino") + "§" + atual.attr("id"));
    }

    if (atual.data("conectorOrigem") == undefined || atual.data("conectorOrigem") == "")
        atual.data("conectorOrigem", nome);
    else {
        if (atual.data("conectorOrigem").indexOf(nome) < 0)
            atual.data("conectorOrigem", atual.data("conectorOrigem") + "§" + nome);
    }

    if (anterior.data("conectorDestino") == undefined || anterior.data("conectorDestino") == "")
        anterior.data("conectorDestino", nome);
    else {
        if(anterior.data("conectorDestino").indexOf(nome) < 0)
            anterior.data("conectorDestino", anterior.data("conectorDestino") + "§" + nome);
    }

        
}

function criarObjetos(xml){
    var objeto;
    var baseX = $("#conteudo").width();
    var baseY = $("#conteudo").height();
    var idNome = new Array(); 
    $(xml).find('set').each(function(){
        var numeroTipo = Number($(this).attr('tipoElemento'));
        var x = (baseX * Number($(this).attr('x'))) / 100;
        var y = (baseY * (100 - Number($(this).attr('y')))) / 100;
        objeto = criarObjeto(x, y, tiposObjetosFluxo[numeroTipo], $(this).attr('name'));
        idNome[$(this).attr('id')] = objeto.attr("id");
        //Guardando valores
        objeto.data("idXml", $(this).attr('id'));
        //Valores padrões
        var largura = Number($(xml).find('workflows').attr('width'));
        var altura = Number($(xml).find('workflows').attr('height'));
        $("#conteudo").width(largura);
        $("#conteudo").height(altura);
        var tipoObjeto = tiposObjetosFluxo[Number($(this).attr('tipoElemento'))];
        switch(tipoObjeto)
        {
            case "inicio":
                objeto.data("textoDescricaoA", $(xml).find('dadosVersao').attr('descricaoAbreviada'));
                objeto.data("textoDescricao", $(xml).find('dadosVersao').attr('descricao'));
                objeto.data("versaoXml", $(xml).find('dadosVersao').attr('versaoXml'));
                var inicioFluxo = $(this).find('acao[id="Início"]');
                objeto.data("caixaAssuntoM", inicioFluxo.find('assuntoNotificacao').text());
                objeto.data("caixaTextoM", inicioFluxo.find('textoNotificacao').text());
                objeto.data("caixaAssuntoM2", inicioFluxo.find('assuntoNotificacao2').text());
                objeto.data("caixaTextoM2", inicioFluxo.find('textoNotificacao2').text());
                //Grupo acesso
                var valoresPerfis = new Array();
                inicioFluxo.find('grupo').each(function(){
                    var valorP = [$(this).attr("id"), $(this).attr("msgBox")];
                    valoresPerfis.push(valorP);
                });
                objeto.data("botaoPerfisN", valoresPerfis); 
                //Ações automáticas
                var valoresAcoes = new Array();
                inicioFluxo.find('acaoAutomatica').each(function(){
                    var valorA = [$(this).attr("id")];
                    valoresAcoes.push(valorA);
                });
                objeto.data("botaoAcoesA", valoresAcoes);  
                break;
            case "etapa":
                objeto.data("textoNomeA", $(this).attr('name'));
                objeto.data("textoDescricaoA", $(this).attr('toolText'));
                objeto.data("textoDescricao", $(this).find('descricao').text());
                objeto.data("comboPrazoM", $(this).find('prazoPrevisto').attr('timeoutOffset'));
                objeto.data("comboPrazoTipo", $(this).find('prazoPrevisto').attr('timeoutUnit'));
                objeto.data("numericoPrazoQ", $(this).find('prazoPrevisto').attr('timeoutValue'));
                //Valores visual
                objeto.attr("title", $(this).attr('toolText'));
                if(objeto.find('p[id^="texto_"]').size() > 0)
                    objeto.find('p[id^="texto_"]').html($(this).attr('name'));
                //Grupo acesso
                var valoresPerfis = new Array();
                $(this).find('gruposComAcesso').find('grupo').each(function(){
                    var valorP = [$(this).attr("id"), $(this).attr("accessType")];
                    valoresPerfis.push(valorP);
                });
                objeto.data("botaoAcessos", valoresPerfis); 
                //Ações formulários
                var valoresFormulario = new Array();
                $(this).find('formulario').each(function(){
                    var valorF = [$(this).attr("id"), $(this).attr("title"), $(this).attr("newOnEachOcurrence"), $(this).attr("required"), $(this).attr("readOnly")];
                    valoresFormulario.push(valorF);
                });
                objeto.data("botaoFormularios", valoresFormulario);
                break;
		    case "tempo":
                var numeroOrigem = $(xml).find('connector[to="' + $(this).attr("id") + '"]').attr("from");
                var numeroDestino = $(xml).find('connector[from="' + $(this).attr("id") + '"]').attr("to");
                var tempo = $(xml).find('set[id="' + numeroOrigem + '"]').find('acao[id="timer"][actionType="T"][nextStageId="' + numeroDestino + '"]');
                objeto.data("numericoPrazoQ", tempo.attr("timeoutValue"));
                objeto.data("comboPrazoTipo", tempo.attr("timeoutUnit"));
                objeto.data("caixaAssuntoM", tempo.find("assuntoNotificacao").text());
                objeto.data("caixaTextoM", tempo.find("textoNotificacao").text());
                objeto.data("caixaAssuntoM2", tempo.find("assuntoNotificacao2").text());
                objeto.data("caixaTextoM2", tempo.find("textoNotificacao2").text());
                //Grupo acesso
                var valoresPerfis = new Array();
                tempo.find('grupo').each(function(){
                    var valorP = [$(this).attr("id"), $(this).attr("msgBox")];
                    valoresPerfis.push(valorP);
                });
                objeto.data("botaoPerfisN", valoresPerfis); 
                //Ações automáticas
                var valoresAcoes = new Array();
                tempo.find('acaoAutomatica').each(function(){
                    var valorA = [$(this).attr("id")];
                    valoresAcoes.push(valorA);
                });
                objeto.data("botaoAcoesA", valoresAcoes); 
                break;
		    case "juncao":
		    case "desjuncao":
		    case "fim":
                objeto.data("textoDescricao", $(this).attr('toolText'));
                objeto.attr("title", $(this).attr('toolText'));
                break;
        }
    });
    $(xml).find('connector').each(function () {
        var objeto = criarObjeto(100, 100, "novaConector", $(this).attr('label'));
        var atual = $("#" + idNome[$(this).attr('to')]);
        var anterior = $("#" + idNome[$(this).attr('from')]);
        if (objeto.find('p[id^="texto_"]').size() > 0) {
            $.jfLigarConector(objeto, atual, anterior, false, objeto.find('p[id^="texto_"]'));
        }
        else {
            $.jfLigarConector(objeto, atual, anterior, false);
        }
        gravarLigacoesConectorAtualAnterior(objeto, atual, anterior);
        //Valores
        var numeroOrigem = $(this).attr("from");
        var numeroDestino = $(this).attr("to");
        var label = $(this).attr("label");
        var linha = $(xml).find('set[id="' + numeroOrigem + '"]').find('acao[id="' + label + '"][actionType="U"][nextStageId="' + numeroDestino + '"]');
        objeto.data("textoTitulo", $(this).attr("label"));
        objeto.data("caixaAssuntoM", linha.find("assuntoNotificacao").text());
        objeto.data("caixaTextoM", linha.find("textoNotificacao").text());
        objeto.data("caixaAssuntoM2", linha.find("assuntoNotificacao2").text());
        objeto.data("caixaTextoM2", linha.find("textoNotificacao2").text());
        if ($(this).attr("label") != "") {
            if (!objeto.hasClass("objetoConectorAcao"))
                objeto.addClass("objetoConectorAcao");
        }
        else {
            if (objeto.hasClass("objetoConectorAcao"))
                objeto.removeClass("objetoConectorAcao");
        }

        //Grupo acesso
        var valoresPerfis = new Array();

        linha.find('grupo').each(function () {
            var valorP = [$(this).attr("id"), $(this).attr("msgBox")];
            valoresPerfis.push(valorP);
        });
        objeto.data("botaoPerfisN", valoresPerfis);
        //Ações automáticas
        var valoresAcoes = new Array();
        linha.find('acaoAutomatica').each(function () {
            var valorA = [$(this).attr("id")];
            valoresAcoes.push(valorA);
        });
        objeto.data("botaoAcoesA", valoresAcoes);
    });
    $(window).data("idNomeXml", idNome);

}



