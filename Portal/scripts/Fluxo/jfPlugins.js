;(function($) {
    ///Plugin para ciração da tabela/grid com valores enviados
    $.jfCriarTabela = function (opcoes) {
        /*
        
        Opções:
        classeTabela = String com o nome da classe para a tabela toda
        ativarLinhaZebrada = Boolean ativar a linha zebrada
        classeLinhaZebrada = String com nome da classe para a linha zebrada
        cliqueLinha = Function para a linha clicada
        duploCliqueLinha = Function para clique duplo na linha
        cliqueCelula = Function para clique em celula
        duploCliqueCelula = Function para duplo clique em celula
        classeLinhasTopo = String com nome da classe para linha do cabecalho
        div = Object Div Jquery do destino a ser inserido a tabela
        titulos = Object Array com os nomes dos titulos da tabela
        valores = Object Array com os valores da linha para cada coluna de acordo com o cabecalho
        classeLinhaClicada = String com o nome da classe para a linha clicada
        ativarLinhaClicada = Boolean para ativar amostra de linha clicada
        ativarCelulaClicada = Boolean para ativar amostra da celula clicada
        classeCelulaClicada = String com o nome da Classe para a celula clicada
        tituloTabela = String com o titulo da tabela
        */
        if (typeof opcoes["titulos"] == "object" && opcoes["titulos"].length > 0 && typeof opcoes["div"] == "object") {
            //Classes
            var classeLinhaClicada = typeof opcoes["classeLinhaClicada"] == "string" ? opcoes["classeLinhaClicada"] : "jfTabelaValoresCampoListaClicado";
            var classeLinhaZebrada = typeof opcoes["classeLinhaZebrada"] == "string" ? opcoes["classeLinhaZebrada"] : "jfTabelaValoresCampoLista";
            var classeLinhaTopo = typeof opcoes["classeLinhasTopo"] == "string" ? opcoes["classeLinhasTopo"] : "jfTabelaValoresCampoCabecalho";
            var classeCelulaClicada = typeof opcoes["classeCelulaClicada"] == "string" ? opcoes["classeCelulaClicada"] : "jfTabelaValoresCampoCelulaClicado";
            var classeTabela = typeof opcoes["classeTabela"] == "string"?opcoes["classeTabela"]:"jfTabelaValoresCampo";

            var nomeTabela = "tabelaValoresCampo_" + opcoes["div"].attr("id");
            var tabela = document.createElement('table');
            tabela.className = classeTabela;
            tabela.id = nomeTabela;
            if(opcoes["div"].find("#" + nomeTabela).size() > 0)
                $("#" + nomeTabela).remove();
            opcoes["div"].append(tabela);
            $(tabela).data("opcoes", opcoes);
            $(tabela).data("tipo", "JfTabela");
            var topo = document.createElement('thead');
            var linhaTopo = document.createElement('tr');
            var linhaTitulo = document.createElement('tr');
            var celulaTitulo = document.createElement('td');
            celulaTitulo.className = "jfCelulaTituloTabela";
            celulaTitulo.setAttribute("colspan", opcoes["titulos"].length);
            $(celulaTitulo).text(typeof opcoes["tituloTabela"]=="string"?opcoes["tituloTabela"]:"");
            $(linhaTitulo).append(celulaTitulo);
            linhaTopo.className = classeLinhaTopo;
            $(topo).append(linhaTitulo);
            $(topo).append(linhaTopo);
            for (var i = 0; i < opcoes["titulos"].length; i++) {
                var celulaTopo = document.createElement('td');
                $(celulaTopo).text(opcoes["titulos"][i]);
                $(linhaTopo).append(celulaTopo);
            }

            $(tabela).append(topo);
            var corpo = document.createElement('tbody');
            var rodape = document.createElement('tfoot');
            var linhaRodape = document.createElement('tr');
            var celulaRodape = document.createElement('td');
            celulaRodape.setAttribute("colspan", opcoes["titulos"].length);
            $(linhaRodape).append(celulaRodape);
            $(rodape).append(linhaRodape);
            $(tabela).append(corpo);
            $(tabela).append(rodape);
            if (typeof opcoes["valores"] == "object" && opcoes["valores"].length > 0) {
                for (var v = 0; v < opcoes["valores"].length; v++) {
                    var linha = document.createElement("tr");
                    $(linha).data("index", v);
                    for (var vv = 0; vv < opcoes["valores"][v].length; vv++) {
                        var celula = document.createElement("td");
                        $(celula).text(opcoes["valores"][v][vv]);
                        $(celula).data("index", vv);
                        $(linha).append(celula);
                    }
                    $(corpo).append(linha);
                }
                if (typeof opcoes["ativarLinhaZebrada"] == "boolean" && opcoes["ativarLinhaZebrada"]) {
                    $("#" + tabela.id + " tbody tr:odd").addClass(classeLinhaZebrada);
                }
                $("#" + tabela.id + " tbody tr").click(function () {
                    if (typeof opcoes["ativarLinhaClicada"] == "boolean" && opcoes["ativarLinhaClicada"]) {
                        $("#" + tabela.id + " ." + classeLinhaClicada).removeClass(classeLinhaClicada);
                        $(this).addClass(classeLinhaClicada);
                    }
                    $(tabela).data("cliqueLinha", $(this).data("index"));
                    if (typeof opcoes["cliqueLinha"] == "function")
                        opcoes["cliqueLinha"]($(this), $(this).data("index"));
                });
                $("#" + tabela.id + " tbody tr").dblclick(function () {
                    if (typeof opcoes["duploCliqueLinha"] == "function")
                        opcoes["duploCliqueLinha"]($(this), $(this).data("index"));
                    $(tabela).data("linhaDuploClique", $(this).data("index"));
                });
                $("#" + tabela.id + " tbody td").click(function () {
                    if (typeof opcoes["ativarCelulaClicada"] == "boolean" && opcoes["ativarCelulaClicada"]) {
                        $("#" + tabela.id + " ." + classeCelulaClicada).removeClass(classeCelulaClicada);
                        $(this).addClass(classeCelulaClicada);
                    }
                    if (typeof opcoes["cliqueCelula"] == "function")
                        opcoes["cliqueCelula"]($(this), $(this).data("index"));
                    $(tabela).data("cliqueCelula", $(this).data("index"));
                });
                $("#" + tabela.id + " tbody td").dblclick(function () {
                    if (typeof opcoes["duploCliqueCelula"] == "function")
                        opcoes["duploCliqueCelula"]($(this), $(this).data("index"));
                    $(tabela).data("duploCliqueCelula", $(this).data("index"));
                });

            }
            opcoes["div"].data("tabela", $(tabela));
        }
        
        return $(tabela);
    };
    ///Plugin de elemento tabela para adicionar uma nova linha
    $.fn.jfAdicionarTabela = function (opcoes) {
        if (typeof this.data("tipo") != "undefined" && this.data("tipo") == "JfTabela") {
            var opcoesTabela = this.data("opcoes");
            var tabela = this;
            opcoesTabela["valores"].push(opcoes.valores);
            tabela.removeData();
            $.jfCriarTabela(opcoesTabela);
        }
        else {
            console.log("Elemento não é do tipo JfTabela!");
        }
    };
    ///Plugin de elemento tabela para remover uma linha
    $.fn.jfRemoverTabela = function () {
        if (typeof this.data("tipo") != "undefined" && this.data("tipo") == "JfTabela") {
            if(typeof this.data("cliqueLinha") == "undefined")
            {
                $.jfAlerta({
                    mensagem: "Não foi selecionado a linha a ser excluída.",
                    tipo:"alertaConfirmado",
                    titulo:"Selecione o valor a ser excluído!"
                });
            }
            else
            {
                var opcoes = this.data("opcoes");
                var tabela = this;
                if(opcoes["valores"].length > 0)
                {
                    var numero  = Number(this.data("cliqueLinha"));
                    $.jfAlerta({
                        mensagem:"Deseja realmente excluir a linha selecionada ?",
                        tipo:"confirmar",
                        titulo:"Confirmar exclusão!",
                        botaoConfirmar:function(){
                            opcoes["valores"].splice(numero, 1);
                            tabela.removeData();
                            $.jfCriarTabela(opcoes);
                        }
                    });

                    
                }
            }
        }
        else {
            console.log("Elemento não é do tipo JfTabela!");
        }
    };
    ///Plugin de elemento tabela para alterar uma linha
    $.fn.jfAlterarTabela = function (opcoes) {
        if (typeof this.data("tipo") != "undefined" && this.data("tipo") == "JfTabela") {
            var opcoesTabela = this.data("opcoes");
            var tabela = this;
            var numero  = Number(this.data("cliqueLinha"));
            opcoesTabela["valores"][numero] = opcoes["valores"];
            tabela.removeData();
            $.jfCriarTabela(opcoesTabela);
        }
        else {
            console.log("Elemento não é do tipo JfTabela!");
        }
    };
    ///Plugin de elemento tabela para retornar os valores de linha
    $.fn.jfValoresTabela = function () {
        if (typeof this.data("tipo") != "undefined" && this.data("tipo") == "JfTabela") 
        {
            var opcoes = this.data("opcoes");
            if(opcoes["valores"].length > 0 && typeof this.data("cliqueLinha") != "undefined")
            {
                var numero  = Number(this.data("cliqueLinha"));
                return opcoes["valores"][numero];
            }
            else
            {
                $.jfAlerta({
                    mensagem: "Não foi selecionado a linha a ser editada.",
                    tipo:"alertaConfirmado",
                    titulo:"Selecione o valor a ser editado!"
                });
                return false;
            }
        }
        else {
            console.log("Elemento não é do tipo JfTabela!");
        }
    };
    ///Retorna a distancia em px entre dois objetos
    $.jfDistanciaPontos = function(a, b)
	{
		var pa = $.jfPosicoesPonto(a); 
		var pb = $.jfPosicoesPonto(b);
        var novos = $.jfCalculaXYPontos(pa, pb);
		var v1 = Math.abs(novos.bx - novos.ax);
		var v2 = Math.abs(novos.by - novos.ay);
		v1 = Math.abs(v1 * v1);
		v2 = Math.abs(v2 * v2);
		return Math.sqrt(v1 + v2);
	};
    ///Retorna as coordenadas do ponto médio entre os dois objetos
	 $.jfPontoMedioPontos = function(a, b)
	{
		var pa = $.jfPosicoesPonto(a); 
		var pb = $.jfPosicoesPonto(b);
        var novos = $.jfCalculaXYPontos(pa, pb);
		var v1 = Math.abs((novos.bx + novos.ax) / 2);
		var v2 = Math.abs((novos.by + novos.ay) / 2);
		return {x:v1, y:v2};
	};
    ///Retorna o grau de um objeto para o outro
	 $.jfGrausPontos = function(a, b)
	{
		var pa = $.jfPosicoesPonto(a); 
		var pb = $.jfPosicoesPonto(b);
        var novos = $.jfCalculaXYPontos(pa, pb);
		var x = novos.bx - novos.ax;
		var y = novos.by - novos.ay;
		var etan = Math.atan2(y, x);
		var valor = etan * 180 / (Math.PI);
		return valor;
	};
    ///Retorna os valores e coordenadas do objeto informado
	 $.jfPosicoesPonto = function(obj)
	{
		var ox = obj.position().left;
		var oy = obj.position().top;
		var oh = obj.height();
		var ow = obj.width();
		return {x:ox, y:oy, h:oh, w:ow};
	};
    ///Cálculo os coordenadas de x e y de acordo com a posição entre os objetos
     $.jfCalculaXYPontos = function(atual, anterior)
	{
		var xAnterior = anterior.x + (anterior.w / 2);
		var yAnterior = anterior.y + (anterior.h / 2);
		var larguraAnterior = anterior.x + anterior.w;
		var alturaAnterior = anterior.y + anterior.h;
		var xAtual = atual.x + (atual.w / 2);
		var yAtual = atual.y + (atual.h / 2);
		var larguraAtual = atual.x + atual.w;
		var alturaAtual = atual.y + atual.h;
		var hor, ver, ladoAnterior, ladoAtual, xAnteriorNovo, yAnteriorNovo, xAtualNovo, yAtualNovo;
		var espaco = 4;
        
		if ((anterior.x - espaco) > larguraAtual)
		{
			hor = -1;
		}
		else if ((larguraAnterior + espaco) < atual.x)
		{
			hor = 1;
		}
        else
        {
            hor = 0;
        }
		if (anterior.y > alturaAtual)
		{
			ver = 1;
		}
		else if (alturaAnterior < atual.y)
		{
			ver = -1;
		}
        else
        {
            ver = 0;
        }
		var validacao = hor + "/" + ver;
		switch (validacao)
		{
			case "0/0" :
				ladoAnterior = "Centro";
				ladoAtual = "Centro";
				xAnteriorNovo = xAnterior;
				yAnteriorNovo = yAnterior;
				xAtualNovo = xAtual;
				yAtualNovo = yAtual;
				break;
			case "-1/-1" :
			case "0/-1" :
			case "1/-1" :
				ladoAnterior = "Inferior";
				ladoAtual = "Superior";
				xAnteriorNovo = xAnterior;
				yAnteriorNovo = anterior.y + anterior.h;
				xAtualNovo = xAtual;
				yAtualNovo = atual.y;
				break;
			case "1/0" :
				ladoAnterior = "Esquerda";
				ladoAtual = "Direita";
				xAnteriorNovo = anterior.x + anterior.w;
				yAnteriorNovo = yAnterior;
				xAtualNovo = atual.x;
				yAtualNovo = yAtual;
				break;
			case "-1/1" :
			case "0/1" :
			case "1/1" :
				ladoAnterior = "Superior";
				ladoAtual = "Inferior";
				xAnteriorNovo = xAnterior;
				yAnteriorNovo = anterior.y;
				xAtualNovo = xAtual;
				yAtualNovo = (atual.y + atual.h);
				break;
			case "-1/0" :
				ladoAnterior = "Direita";
				ladoAtual = "Esquerda";
				xAnteriorNovo = anterior.x;
				yAnteriorNovo = yAnterior;
				xAtualNovo = (atual.x + atual.w);
				yAtualNovo = yAtual;
				break;
		}
		return {ax:xAtualNovo, ay:yAtualNovo, bx:xAnteriorNovo, by:yAnteriorNovo};
	};
    ///Aplica a rotação e posicionamento entre dois objetos
    $.jfLigarConector = function(linha, a, b, animar, interno)
	{
		var valor = $.jfGrausPontos(a, b);
		var distancia = $.jfDistanciaPontos(a, b);
		var pontoMedio = $.jfPontoMedioPontos(a, b);
		pontoMedio["xReal"] = pontoMedio.x - (distancia / 2);
		if(animar)
		{
		    linha.css({
			    left:pontoMedio.xReal + "px"
			    , top:pontoMedio.y + "px"
		    }).css({
				"transform":"rotate(" + valor + "deg)"
				, "-ms-transform":"rotate(" + valor + "deg)"
				, "-webkit-transform":"rotate(" + valor + "deg)"
		    }).css({
			    width:distancia + "px"
			    }).fadeIn(400);
		}
		else
		{
			linha.css({
				left:pontoMedio.xReal + "px"
				, top:pontoMedio.y + "px"
			}).css({
				width:distancia + "px"
			}).css({
				"transform":"rotate(" + valor + "deg)"
				, "-ms-transform":"rotate(" + valor + "deg)"
				, "-webkit-transform":"rotate(" + valor + "deg)"
			});
		}
        if(typeof interno != "undefined")
        {
            var valorInterno = valor * -1;
            interno.css({
				"transform":"rotate(" + valorInterno + "deg)"
				, "-ms-transform":"rotate(" + valorInterno + "deg)"
				, "-webkit-transform":"rotate(" + valorInterno + "deg)"
			});
        }
	};
    ///Janela alerta
    $.jfAlerta = function(opcoes)
    {
        /*
        mensagem = String com o texto a ser apresentado.
        observacao = String com o texto a ser apresentado como observação.
        titulo = String com o texto do título do alerta;
        tipo = String com o tipo de aviso a ser mostrado (confirmar, alertaConfirmado, alertaAviso)
        botaoConfirmar = Function com a função para o botão confirmar
        botaoCancelar = Function com o função para o botão cancelar
        botaoEntendido = Function com a função para o botão Entendido
        div = Object destino para adicionar o alerta
        aberto = Function com a função para quando abrir o aviso
        fechado = Function com a função para quando fechar o aviso
        classeAlerta = String com o nome da classe para a janela de alerta
        tempoAviso = Number com o tempo em que o aviso ficara em exibição
        botoes = Object botoões adicionais para o alerta.
        redimencionar = Boolean especifica a ativar redimecionamento do dialog
        mostrarModal = Boolean especifica ´habilitado para modal;
        mover = Boolean habilita ou desabilita mover a janela
        posicao = Object posição da janela
        */
        if(typeof opcoes["mensagem"] == "string" && typeof opcoes["tipo"] == "string")
        {
            var div = document.createElement('div');
            var botoes = typeof opcoes["botoes"] == "object" ? opcoes["botoes"] : new Object();
            var destino = typeof opcoes["div"] == "object"?opcoes["div"]:$('body');
            var tempoAviso = typeof opcoes["tempoAviso"] == "number"?opcoes["tempoAviso"]:3000;
            var mostrarModal = opcoes["tipo"] == "alertaAviso"?false:true;
            mostrarModal = typeof opcoes["mostrarModal"] == "boolean"?opcoes["mostrarModal"]:mostrarModal;
            var redimencionar = typeof opcoes["redimencionar"] == "boolean" ? opcoes["redimencionar"] : false;
            var mover = typeof opcoes["mover"] == "boolean"?opcoes["mover"]:false;
            var alturaMaxima, larguraMaxima;
            var posicao = typeof opcoes["posicao"] == "object"?opcoes["posicao"]:{ my: "center", at: "center", of: window };
            if(destino[0].tagName == "BODY")
            {
                alturaMaxima = $(document).height() - 20;
                larguraMaxima = $(document).width() / 2 - 20;
            }
            else
            {
                alturaMaxima = destino.height() - 50;
                larguraMaxima = destino.width() - 50;
            }
            div.title = typeof opcoes["titulo"] == "string"?opcoes["titulo"]:"Alerta!";
            div.id = "jfJanelaAlerta";
            div.className = typeof opcoes["classeAlerta"] == "string"?opcoes["classeAlerta"]:"jfJanelaAlerta";
            
            if($('body').find('[id="' + div.id + '"]:visible').size() > 0 && $("#" + div.id).children().size() > 0)
            {
                if($("#" + div.id).dialog( "isOpen" ))
                {
                    $("#" + div.id).dialog( "close" );
                    $("#" + div.id).dialog( "destroy" );
                    
                }
                $("#" + div.id).empty();
                $("#" + div.id).remove();
            }
            destino.append(div);
            var texto = document.createElement('div');
            var obs = document.createElement('p');
            var botoesPadrao;
            switch(opcoes["tipo"])
            {
                case "confirmar":
                    botoesPadrao = {
                        "Confirmar":function(){
                            typeof opcoes["botaoConfirmar"] == "function" ? opcoes["botaoConfirmar"]("confirmado") : function(){};
                            $(div).dialog( "close" );
                            $(div).dialog( "destroy" );
                            $(div).empty();
                            $(div).remove();
                        },
                        "Cancelar":function()
                        {
                            typeof opcoes["botaoCancelar"] == "function" ? opcoes["botaoCancelar"]("cancelado") : function(){};
                            $(div).dialog( "close" );
                            $(div).dialog( "destroy" );
                            $(div).empty();
                            $(div).remove();
                        }
                    };
                break;
                case "alertaConfirmado":
                    botoesPadrao = {
                        "Entendido":function()
                        {
                            typeof opcoes["botaoEntendido"] == "function" ? opcoes["botaoEntendido"]("entendido") : function(){};
                            $(div).dialog( "close" );
                            $(div).dialog( "destroy" );
                            $(div).empty();
                            $(div).remove();
                        }
                    };
                break;
            }
            for(b in botoesPadrao)
            {
                botoes[b] = botoesPadrao[b];
            }
            $(div).append(texto);
            $(div).append(obs);
            $(div).dialog({
                open: function(){
                    typeof opcoes["aberto"] == "function" ? opcoes["aberto"]("aberto") : function(){};
                    $(texto).text(opcoes["mensagem"]);
                    if(typeof opcoes["observacao"]=="string")
                        $(obs).text("Observação: " + opcoes["observacao"]);
                    if(opcoes["tipo"] == "alertaAviso")
                    {
                        setTimeout(function () {
                            if($('body').find(div).size() > 0)
                            {
                                $(div).dialog( "close" );
                                $(div).empty();
                                $(div).dialog( "destroy" );
                                $(div).remove();
                            }
                        }, tempoAviso);
                    }
                },
                autoOpen: true,
                modal: mostrarModal,
                resizable: redimencionar,
                position: posicao,
                closeText: 'Fechar',
                draggable: mover,
                show: 'fade',
                minWidth: 600,
                maxWidth: larguraMaxima,
                maxHeight: alturaMaxima,
                dialogClass: "janelaDialogo",
                close: function(){
                    typeof opcoes["fechado"] == "function" ? opcoes["fechado"]("fechado") : function(){};
                },
                buttons: botoes
            }); 

        }

    };

    $.fn.jfValorCampo = function(opcoes)
    {
        /*
        valor = Variavel com o valor do campo em questão
        colocar = Boolean especifica se coloca o valor
        retirar = Boolean ativa a retirada do valor
        retornoBooleanNumerico = Boolean ativa o retorno de valores boolean em formato de int 1 ou 0
        */
        switch(this[0].tagName)
        {
            case "SELECT":
                if(typeof opcoes["colocar"] == "boolean" && opcoes["colocar"])
                    this.find('option[value="' + opcoes["valor"] + '"]').attr("selected", "selected");
                if(typeof opcoes["retirar"] == "boolean" && opcoes["retirar"])
                    return this.find('option[selected="selected"]').attr("value");
            break;
            case "INPUT":
                if(typeof this.attr("type") == "undefined")
                {
                    if(typeof opcoes["colocar"] == "boolean" && opcoes["colocar"])
                        this.val(opcoes["valor"]);
                    if(typeof opcoes["retirar"] == "boolean" && opcoes["retirar"])
                        return this.val();
                }
                else if(this.attr("type") == "checkbox")
                {
                    if(typeof opcoes["colocar"] == "boolean" && opcoes["colocar"])
                    {
                        var marcar = "";
                        if(typeof opcoes["valor"] == "string")
                        {
                            if(opcoes["valor"] == "false" || opcoes["valor"] == "False" || opcoes["valor"] == "off" || opcoes["valor"] == "0")
                                marcar = "";
                            else if(opcoes["valor"] == "true" || opcoes["valor"] == "True" || opcoes["valor"] == "on" || opcoes["valor"] == "1")
                                marcar = "checked";
                        }
                        else if(typeof opcoes["valor"] == "boolean")
                            marcar = opcoes["valor"]==true?"checked":"";
                        else if(typeof opcoes["valor"] == "number")
                            marcar = opcoes["valor"]==1?"checked":"";
                        if(typeof marcar == "string" && marcar.length > 0)
                            this.attr("checked", marcar);
                    }
                    if(typeof opcoes["retirar"] == "boolean" && opcoes["retirar"])
                    {
                        if(typeof opcoes["retornoBooleanNumerico"] == "boolean" && opcoes["retornoBooleanNumerico"])
                            return this.attr("checked")=="checked"?1:0;
                        else
                            return this.attr("checked")=="checked"?true:false;
                    }
                }
            break;
        }
    };



        
})(jQuery);