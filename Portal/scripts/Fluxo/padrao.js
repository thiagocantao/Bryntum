$(document).ready(function () {
    var objetos;
    //Valores
    $(window).data("codigoWorkflow", valorGet("cW"));
    $(window).data("codigoFluxo", valorGet("cF"));
    $(window).data("versaoFluxo", valorGet("vF"));
    $(window).data("tipoWorkflow", valorGet("tW"));
    $(window).data("menuAtivo", true);
    ///Nomes grupos ********************
    var nomesGrupos = new Array();
    nomesGrupos["acaoFluxo"] = "23/22/4/1/2";
    nomesGrupos["conector"] = "26/4/1/6/7/27/28";
    nomesGrupos["juncao"] = "22";
    nomesGrupos["desjuncao"] = "22";
    nomesGrupos["etapa"] = "25/23/22/21/16/17/3/0";
    nomesGrupos["fim"] = "22";
    nomesGrupos["inicio"] = "23/22/6/7/27/28/4/1";
    nomesGrupos["tempo"] = "21/17/4/1/6/7/27/28";
    nomesGrupos["janelaAcessos"] = "15/12";
    nomesGrupos["janelaAcoes"] = "11";
    nomesGrupos["janelaFormularios"] = "14/26/10/9/8";
    nomesGrupos["janelaPerfis"] = "15/18";
    nomesGrupos["janelaMensagens"] = "6/7/27/28";
    $(window).data("nomesGrupos", nomesGrupos);

    var nomesCamposGrupos = new Array();
    nomesCamposGrupos["0"] = ["botaoAcessos", "Acessos:"];
    nomesCamposGrupos["1"] = ["botaoAcoesA", "Ações automáticas:"];
    nomesCamposGrupos["2"] = ["botaoAcompanhamentoM", "Acompanhamento:"];
    nomesCamposGrupos["3"] = ["botaoFormularios", "Formulários:"];
    nomesCamposGrupos["4"] = ["botaoPerfisN", "Perfis:"];
    nomesCamposGrupos["5"] = ["botaoTextoD", "Texto adicional:"];
    nomesCamposGrupos["6"] = ["caixaAssuntoM", "Assunto:"];
    nomesCamposGrupos["7"] = ["caixaTextoM", "Mensagem:"];
    nomesCamposGrupos["8"] = ["checkNovoO", "Novo:"];
    nomesCamposGrupos["9"] = ["checkObrigatorio", "Obrigatório:"];
    nomesCamposGrupos["10"] = ["checkSomenteL", "Somente leitura:"];
    nomesCamposGrupos["11"] = ["comboAcaoA", "Ações automáticas:"];
    nomesCamposGrupos["12"] = ["comboAcessos", "Acessos:"];
    nomesCamposGrupos["13"] = ["comboEtapa", "Etapas:"];
    nomesCamposGrupos["14"] = ["comboFormulario", "Formulários:"];
    nomesCamposGrupos["15"] = ["comboPerfil", "Perfis:"];
    nomesCamposGrupos["16"] = ["comboPrazoM", "Prazo:"];
    nomesCamposGrupos["17"] = ["comboPrazoTipo", "Tipo do prazo:"];
    nomesCamposGrupos["18"] = ["comboTipoM", "Tipo:"];
    nomesCamposGrupos["19"] = ["numericoAltura", "Altura:"];
    nomesCamposGrupos["20"] = ["numericoLargura", "Largura:"];
    nomesCamposGrupos["21"] = ["numericoPrazoQ", "Prazo:"];
    nomesCamposGrupos["22"] = ["textoDescricao", "Descrição:"];
    nomesCamposGrupos["23"] = ["textoDescricaoA", "Descrição abreviada:"];
    nomesCamposGrupos["24"] = ["textoId", "ID:"];
    nomesCamposGrupos["25"] = ["textoNomeA", "Nome abreviado:"];
    nomesCamposGrupos["26"] = ["textoTitulo", "Título:"];
    nomesCamposGrupos["27"] = ["caixaAssuntoM2", "Assunto acompanhamento:"];
    nomesCamposGrupos["28"] = ["caixaTextoM2", "Mensagem acompanhamento:"];
    $(window).data("nomesCamposGrupos", nomesCamposGrupos);
    ////Fim nomes grupos ***********

    $(document).tooltip();
    $("#conteudo").draggable({
        start: function (event, ui) {
            esconderAdicionais();
        }
    });
    verificarPublicacaoFluxo();
    ///Eventos dos objetos
    ///
    $("#conteudo").resizable({
        start: function(event, ui){
            esconderAdicionais();
        },
        stop: function(event, ui){
            var objeto = $(this);
            $(window).data("alturaConteudo", objeto.height());
            $(window).data("larguraConteudo", objeto.width());
        },
    });
    ///
    $(window).resize(function () {
        tamanhosAreas();
    });
    ///
    $(document).on("dblclick", '.objetos', function (e) {
        var objeto = $(this);
        $(window).data("objetoDbClique", $(this).attr("id"));
        especificarAtualAnterior(objeto);
        mostrarPainel();
    });
    ///
    $("#painel").mouseleave(function (e) {
        var intervalo = 5;
        var ax = $(this).position().left + intervalo;
        var bx = ax + $(this).width() - (intervalo * 2);
        var ay = $(this).position().top + intervalo;
        var by = ay + $(this).height() - (intervalo * 2);
        var x = e.pageX;
        var y = e.pageY;
        if((x > ax && x < bx && y > ay && y < by))
            mostrarPainel(true);
    });
    ///
    $("#conteudo").click(function(){
        esconderAdicionais();
        mostrarPainel(true);
    });
    ///
    $("#menu").mouseenter(function(){
        mostrarPainel(true);
        esconderAdicionais();
    });
    ///
    $("#menuPalco img").click(function(){
        var tipo = "";
        var idAtual = $(this).attr("id");
        var idObjetoAtual = $("#" + $(window).data("atual")).attr("id");
        esconderAdicionais();
        switch(idAtual)
        {
            case "menuLente":
                ampliarConteudoPalco();
            break;
            case "menuVisao":
                mudarModoVisaoConteudo();
            break;
        }
    });
    ///
    $("#menuAbrirPainel img").click(function(e){
        if(e.shiftKey)
        {

        }
        mostrarPainel();
    });
    ///
    $("#menuEsquerdo img").click(function(e){
        var tipo = "";
        var idAtual = $(this).attr("id");
        var idObjetoAtual = $("#" + $(window).data("atual")).attr("id");
        $(this).parent().hide();
        $("#menuObjeto").hide();
        switch(idAtual)
        {
            case "menuAddEtapa":
                tipo = "etapa";
                break;
            case "menuAddConector":
                tipo = "conector";
                break;
            case "menuAddTempo":
                tipo = "tempo";
                break;
            case "menuAddJuncao":
                tipo = "juncao";
                break;
            case "menuAddFim":
                tipo = "fim";
                break;
        }
        geraNovoObjeto(tipo, idObjetoAtual);
    });
    ///
    $("#menuObjeto img").click(function(){
        var idAtual = $(this).attr("id");
        var objeto = $(this);
        var atual = $("#" + $(window).data("atual"));
        $("#menuEsquerdo:visible").hide();
        switch(idAtual)
        {
            case "menuAdicionar":
                mostrarMenuObjetoAdicionar(objeto);
            break;
            case "menuAlterar":
                mostrarPainel();
                $(this).parent().hide();
            break;
            case "menuConector":
                if(atual.data("tipo") == "conector")
                    mostrarAlteracaoOrigemDestino(atual);
                $(this).parent().hide();
            break;
            case "menuExcluir":
                excluirObjetoFluxo();
                $(this).parent().hide();
            break;
        }
    });
    ///
    $("#menu div img").click(function (e) {
        var idAtual = $(this).attr("id");
        switch (idAtual) {
            case "menuPublicar":
                console.log("publicar");
                break;
            case "menuSalvar":
                console.log("salvar");
                verificarPublicacaoFluxo();
                break;
        }
    });
    ///controle de palco ***************************************************************************
    $("#conteudo").droppable({
        out: function(event, ui) {
        //console.log( "saiu do palco" );
        },
        over: function(event, ui) {
        //console.log( "entrou no palco" );
        },
    });

    ///controle de objetos ***************************************************************************
    $(document).on("click", '.objetos', function(e){
        var objeto = $(this);
        especificarAtualAnterior(objeto);
        if(objeto.attr("id") != $(window).data("atual"))
            mostrarPainel(true);
        if(e.shiftKey && objeto.data("tipo") == "conector")
        {
            mostrarAlteracaoOrigemDestino(objeto);
        }else
        {
            mostrarMenuObjeto(objeto);
        }
    });
    ///Controle de atalhos do teclado do sistema
    $(window).keydown(function(event){
        var atual = $("#" + $(window).data("atual"));
        var id = atual.attr("id");
        var nomeObjeto = "objetoEtapa";
        var velocidade = 10;
        if(event.shiftKey)
        {
            esconderAdicionais();
            switch(event.keyCode)
            {
                case 46://delete
                    excluirObjetoFluxo(atual);
                break;
                case 13://enter
                    mostrarPainel();
                break;
                case 80://p
                    var numero = Number($(window).data("nomesObjetos").indexOf(id));
                    if(numero < ($(window).data("nomesObjetos").length - 1))
                        numero++;
                    else
                        numero = 0;
                    especificarAtualAnterior($("#" + $(window).data("nomesObjetos")[numero]));
                break;
                case 65://a
                    var numero = Number($(window).data("nomesObjetos").indexOf(id));
                    if(numero > 0)
                        numero--;
                    else
                        numero = ($(window).data("nomesObjetos").length - 1);
                    especificarAtualAnterior($("#" + $(window).data("nomesObjetos")[numero]));
                break;
                case 37://seta esquerda
                    if(atual.position().left >= velocidade && atual.data("tipo") != "conector")
                    {
                        atual.css("left", atual.position().left - velocidade);
                        atualizarLinhas(atual);
                    }
                break;
                case 38://seta cima
                    if(atual.position().top >= velocidade && atual.data("tipo") != "conector")
                    {
                        atual.css("top", atual.position().top - velocidade);
                        atualizarLinhas(atual);
                    }
                break;
                case 39://seta direita
                    if((atual.position().left + atual.width()) < ($("#conteudo").width() - velocidade) && atual.data("tipo") != "conector")
                    {
                        atual.css("left", atual.position().left + velocidade);
                        atualizarLinhas(atual);
                    }
                break;
                case 40://seta baixo
                    if((atual.position().top + atual.height()) < ($("#conteudo").height() - velocidade) && atual.data("tipo") != "conector")
                    {
                        atual.css("top", atual.position().top + velocidade);
                        atualizarLinhas(atual);
                    }
                break;
                case 69://e
                    geraNovoObjeto("etapa", id);
                break;
                case 67://c
                    geraNovoObjeto("conector", id);
                break;
                case 84://t
                    geraNovoObjeto("tempo", id);
                break;
                case 74://j
                    geraNovoObjeto("juncao", id);
                break;
                case 70://f
                    geraNovoObjeto("fim", id);
                break;
                case 90://z
                    cancelarAlteracoes();
                break;
            }
        }
    });
    ///
    $(document).on("mouseover", '.objetos:not(".objetoConector")', function (event) {
        if($(this).data("isResizable") == undefined)
        {
            $(this).resizable({
                start: function(event, ui){
                    esconderAdicionais();
                    var objeto = $(this);
                    especificarAtualAnterior(objeto);
                },
            });
            $(this).data("isResizable", true);
        }
        if($(this).data("isDraggable") == undefined)
        {
            $(this).draggable({
                start: function (event, ui) {
                    esconderAdicionais();
                    var objeto = $(this);
                    especificarAtualAnterior(objeto);
                },
                stop: function (event, ui) {
                    //console.log($(this).attr("id") + " parado");
                },
                drag: function(event, ui){
                    atualizarLinhas($(this));
                },
            });
            $(this).data("isDraggable", true);
        }
    });
    ///
    $(document).on("change", '.camposPainel', function (event) {
        $("#" + $(window).data("atual")).data($(this).attr("id"), $(this).val());
        var tipo = $("#" + $(window).data("atual")).data("tipo");
        if($(this)[0].tagName == "INPUT" && $(this).attr("type") == "checkbox")
        {
            if(typeof $(this).attr("checked") == "undefined")
                $(this).attr("checked", "checked");
            else if($(this).attr("checked") == "checked")
                $(this).removeAttr("checked");
        }
        var objeto = $("#" + $(window).data("atual"));
        switch($(this).attr("id"))
        {
            case "textoNomeA":
                if(tipo != "etapa")
                    $("#" + $(window).data("atual")).attr("title", $(this).val());
                else if(tipo == "etapa")
                {
                    if($("#" + $(window).data("atual")).find('p[id^="texto_"]').size() > 0)
                        $("#" + $(window).data("atual")).find('p[id^="texto_"]').html($(this).val());
                }
            break;
            case "textoTitulo":
                if($(this).val() != "")
                {
                    if(!objeto.hasClass("objetoConectorAcao"))
                        objeto.addClass("objetoConectorAcao");
                }
                else
                {
                    if(objeto.hasClass("objetoConectorAcao"))
                        objeto.removeClass("objetoConectorAcao");
                }
                if(tipo == "conector")
                {
                    if($("#" + $(window).data("atual")).find('p[id^="texto_"]').size() > 0)
                        $("#" + $(window).data("atual")).find('p[id^="texto_"]').html($(this).val());
                }
            break;
            case "textoDescricao":
                $("#" + $(window).data("atual")).attr("title", $(this).val());
            break;
            case "textoDescricaoA":
                if(tipo == "etapa")
                    $("#" + $(window).data("atual")).attr("title", $(this).val());
            break;
        }
    });
    ///
    $(document).on("click", '.painel button', function (event) {
        var botaoId = $(this).attr("id");
        var botao = $(this);
        var janela = $( "#painelBotao" );
        var nomeExterno = botao.data("nomeExterno");
        $(window).data("botaoPainelClicado", botaoId);
        janela.attr("title", nomeExterno);
        abrirJanelaListaBotao(janela);
        janela.dialog( "open" );
        if(janela.children().length > 0)
                    janela.empty();
        buscarValoresTabelaBotaoClicado(botao, janela);
    
    });

    $("#painelBotaoEdicao").dialog({
        autoOpen: false,
        modal: true,
        resizable: false,
        minWidth: 700,
        closeText: 'Fechar',
        draggable: true,
        show: 'fade',
        buttons: {
            "Salvar":function(){
                var tipo = $( this ).data("tipo");
                var botaoId = $(window).data("botaoPainelClicado");
                var valores = new Array();
                var tabela = $( "#painelBotao" ).data("tabela");
                $(this).find('[id^="janela§campo§"]').each(function(){
                    if($(this)[0].tagName == "INPUT" && $(this).attr("type") == "checkbox")
                    {
                        valores[$(this).attr("id")] = $(this).attr("checked") == "checked"?"1":"0";
                    }
                    else
                        valores[$(this).attr("id")] = $(this).val();
                });
                
                salvarDadosNovosEditados(botaoId, tipo, valores, tabela);
                $( this ).dialog( "close" );
            },
            "Cancelar":function()
            {
                $( this ).dialog( "close" );
            }
        },
        close: function() {
            $(this).empty();
        }
    });
 
 

    ///Fim eventos do objetos
    tamanhosAreas();

});

$(window).load(function () {
  iniciarFluxo();
});







