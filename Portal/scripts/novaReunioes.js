// <![CDATA[
var timerAtualizacao;
var mostrarAcompanhamento = false;
var planoDeAcaoAExcluir = "";
var planoDeAcaoPendenteAExcluir = "";

$(document).ready(function () {
    $(document).tooltip();
    carrgarTodoBanco();
    aplicarMudancas();
    timerAtualizacao = $.timer(function () {
        buscarAtualizacoesSistema();
    });
    fecharQuadrosMenu();
    timerAtualizacao.set({ time: 2000, autostart: false });
    timerAtualizacao.play(true);
   
    //    timerAtualizacao.pause();
    //    timerAtualizacao.stop(); 
});

function carrgarTodoBanco() {
    var dataIn = { valor: "atualizarSistema" };
    var valor = "";
    var endereco = enderecoBaseAtual();
    $.ajax({
        url: endereco + "/Servicos/Service.svc/carregarDadosBanco",
        data: dataIn,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if(data.d == undefined)
                    valor = data.toString();
                else
                    valor = data.d.toString();
                if (valor == "True") {
                    atualizarSistema("atualizarDados");
                }
                else
                    window.top.mostraMensagem(valor, 'Atencao', true, false, null);
            }
        },
        error: function (request, status, error) {
            avisoErroConexaoWebService();
            console.log(status.toString() + traducao.novaReunioes__na_resposta_do_webservice_de_banco_de_dados__c_digo___ + request.status + " Mensagem = " + request.statusText);
        }
    });
}

function aplicarMudancaFonte() {
    var valor = "";
    var adicionar = true;
    if (hfValores.Get("fontePadraoSistema") != undefined) {
        valor = hfValores.Get("fontePadraoSistema");
        if (hfValores.Get("fontePadraoSistema") == "")
            adicionar = false;
    }
    mudarFontes(valor, adicionar, false);
}

function mudarFontes(tamanho, adicionar, atualizar) {
    hfValores.Set("fontePadraoSistema", tamanho);
    if ($("#cpTitulo_lbTituloTipo").hasClass('tamanhoFontePadrao'))
        $("label[id!='cpTitulo_lbTituloItem'], .dxgv, .dxnb-ghtext, textarea, iframe").removeClass("tamanhoFontePadrao");
    if(adicionar)
        $("label[id!='cpTitulo_lbTituloItem'], .dxgv, .dxnb-ghtext, textarea, iframe").addClass("tamanhoFontePadrao");
    if(!adicionar && tamanho == "" && atualizar)
        document.location.reload();
    
    if(tamanho != "")
    {
        $(".tamanhoFontePadrao").each(function () {
            var anterior = $(this).css("font-size");
            anterior = anterior.replace("px", "");
            var novo = parseInt(tamanho);
            $(this).css("font-size", + novo + "px");
        });
        $("textarea").css({ 'padding-top': parseInt(tamanho) + 'px', 'padding-bottom': parseInt(tamanho) + 'px', 'line-height': parseInt(tamanho) + 'px' });
    }
}

function telaToda() {
    window.open("novaReunioes.aspx", "principal", "status=no, toolbar=no, menubar=no, location=no, fullscreen=1, scrolling=auto");
}



function gravarDeliberacao(valor) {
    if ($('#quadroDeliberacoes textarea').attr('readonly') == undefined) {
        valor += "§atualizarDeliberacaoAtual";
        var data = new Date();
        var tempoValor = data.getMinutes() + data.getSeconds();
        $(window).data("intervaloGravacaoDeliberacao", tempoValor);
        setTimeout(function () {
            var data2 = new Date();
            var tempoValor2 = data2.getMinutes() + data2.getSeconds();
            if (Number($(window).data("intervaloGravacaoDeliberacao")) <= (tempoValor2 - 3)) {
                //console.log("Deliberação gravada");
                callItens.PerformCallback(valor);
            }
        }, 3000);
        
        
    }
}


function converterValoresDatas(data, valor) {
    var novaData = new Date(valor);
    var retorno;
    if (data) {
        var dia = novaData.getDate().toString().length < 2 ? novaData.getDate() + 1 : novaData.getDate();
        var mes = novaData.getMonth().toString().length < 2 ? "0" + (novaData.getMonth() + 1) : novaData.getMonth() + 1;
        retorno = dia + "/" + mes + "/" + novaData.getFullYear();
    }
    else {
        var hora = novaData.getHours().toString().length < 2 ? "0" + novaData.getHours() : novaData.getHours();
        var minuto = novaData.getMinutes().toString().length < 2 ? "0" + novaData.getMinutes() : novaData.getMinutes();
        retorno = hora + ":" + minuto;
    }
    return retorno;
}

function gravarDataInicio(valorData, valorHora, nome) {
    var dados = converterValoresDatas(true, valorData) + '§' + converterValoresDatas(false, valorHora) + "§" + nome + "§atualizarValoresDataSelecionadas";
    hfValores.Set(nome, dados);
}


function mostrarEditorPlanos(result, userContext, methodName) {
    if (result == "true") {
        pcEditarPlanos.Show();

    }
}

function cliqueMenu(s, e) {
    switch (e.item.name) {
        case "menuItens":
            cpTodosItens.PerformCallback("atualizarDados");
            break;
        case "menuParticipantes":
            cpParticipantes.PerformCallback("atualizarDados");
            break;
        case "iniciar":
        case "finalizar":
            fecharQuadrosMenu();
            aplicarMudancas();
            if (confirm("Confirma " + e.item.name + " a reunião?"))
                atualizarSistema(e.item.name);
            break;
        case "proximo":
        case "anterior":
        case "visao":
            fecharQuadrosMenu();
            atualizarSistema(e.item.name);
            aplicarMudancas();
            break;
        case "fonteMonitor":
            mudarFontes("", false, true);
        break;
        case "fonteTv":
            mudarFontes("20", true, false);
            verificarComentarios();
            break;
        case "fonteProjetor":
            mudarFontes("28", true, false);
            verificarComentarios();
            break;
    }
}

function verificarComentarios() {
    if(quadrosPrincipais.groupsExpanding[4])
        cpComentariosItens.PerformCallback('atualizarDados');
}

function fecharQuadrosMenu() {
    for (var i = 0; i < quadrosPrincipais.groups.length; i++) {
        if (quadrosPrincipais.groupsExpanding[i] && i > 0) {
            quadrosPrincipais.SetExpandedInternal(i, false);
        }
        else if(i == 0) {
            quadrosPrincipais.SetExpandedInternal(i, true);
        }
    }
}


function overMenu(s, e) {
    var atualizar = "atualizarDados";
    switch (e.item.name) {
        case "menuItens":
            cpTodosItens.PerformCallback(atualizar);
            break;
        case "menuParticipantes":
            cpParticipantes.PerformCallback(atualizar);
            break;
    }
}

function atualizarSistema(tipo) {
    if (tipo == "atualizarDados")
        timerAtualizacao.pause();
    hfValores.Set("atualizarMenu", tipo);
    callItens.PerformCallback(tipo);
    aplicarMudancas();
}

function atualizarSistemaMenu() {
    if (hfValores.Get("atualizarMenu") == "iniciar" || hfValores.Get("atualizarMenu") == "finalizar" || hfValores.Get("atualizarMenu") == "atualizarDados") {
        cpMenu.PerformCallback('atualizarDados');
    }
}

function atualizarSistemaQuadros() {
    verificarVisibilidadeQuadros();
    if (hfValores.Get("atualizarMenu") == "iniciar" || hfValores.Get("atualizarMenu") == "finalizar" || hfValores.Get("atualizarMenu") == "atualizarDados") {
        cpMenu.PerformCallback('atualizarDados');
    }
    else {
        verificarQuadrosAbertos(quadrosPrincipais);
    }
    aplicarMudancas();
    if (hfValores.Get("atualizarMenu") == "atualizarDados")
        timerAtualizacao.play(true);
}

function retornoMenu() {
    if (hfValores.Get("atualizarMenu") == "iniciar" || hfValores.Get("atualizarMenu") == "finalizar" || hfValores.Get("atualizarMenu") == "atualizarDados") {
        verificarQuadrosAbertos(quadrosPrincipais);
        hfValores.Set("atualizarMenu", "fim");
    }
    aplicarMudancas();
}

function irDiretoItem(data) {
    cpTodosItens.PerformCallback(data);
    fecharQuadrosMenu();
    atualizarSistema("atualizar");
}

function atualizarBanco(valor) {
    if (valor != undefined && valor.indexOf("§") >= 0) {
        if (mostrarAcompanhamento)
            console.log("Sistema será atualizado.");
        fecharQuadrosMenu();
        if (valor.indexOf("dtItens") >= 0)
            atualizarPorQuadros("dtItens");
        if (valor.indexOf("dtParticipantes") >= 0)
            atualizarPorQuadros("dtParticipantes");
        if (valor.indexOf("dtComentarios") >= 0)
            cpComentariosItens.PerformCallback('marcarNovidade');
        if (valor.indexOf("dtAnexos") >= 0)
            cpAnexosItens.PerformCallback('marcarNovidade');
        if (valor.indexOf("dtStatus") >= 0) {
            aplicarMudancas();
        }
        verificarQuadrosAbertos(quadrosPrincipais);
    }
    else {
        if (mostrarAcompanhamento)
            console.log(traducao.novaReunioes_n_o_h__atualiza__o_para_o_sistema_);
    }
}

function cliqueQuadros(s, e) {
    if(!s.groupsExpanding[e.group.index])
        atualizarPorQuadros(e.group.name);
    verificarQuadrosAbertos(s);
}

function buscarAtualizacoesSistema() {
    if (mostrarAcompanhamento)
        console.log("Buscando atualização...");
    timerAtualizacao.pause();
    var dataIn = { valor: "atualizarSistema" };
    var valor = "";
    var endereco = enderecoBaseAtual();
    $.ajax({
        url: endereco + "/Servicos/Service.svc/verificarAtualizacoes",
        data: dataIn,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {
            if (mostrarAcompanhamento)
                console.log(traducao.novaReunioes_solicita__o_de_atualiza__o_realizada_com_ + status.toString());
            if (data != "") {
                if (data.d == undefined)
                    valor = data.toString();
                else
                    valor = data.d.toString();
                atualizarBanco(valor);
            }
        },
        error: function (request, status, error) {
            avisoErroConexaoWebService();
            console.log(status.toString() + traducao.novaReunioes__na_resposta_do_webservice_de_atualiza__o__c_digo___ + request.status + " Mensagem = " + request.statusText);
        }
    });
    timerAtualizacao.play();
}

function lerDadosTabela(tabela) {
    var dataIn = { tabela: tabela };
    var endereco = enderecoBaseAtual();
    $.ajax({
        url: endereco + "/Servicos/Service.svc/buscarTabela",
        data: dataIn,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var dados;
                if (data.d == undefined)
                    dados = jQuery.parseJSON(data);
                else
                    dados = jQuery.parseJSON(data.d);
                var quantidade = dados.rs.length;
                for (i = 0; i < quantidade; i++) {
                }
            }
        },
        error: function (request, status, error) {
            avisoErroConexaoWebService();
            console.log(status.toString() + traducao.novaReunioes__na_resposta_do_webservice_de_retornar_tabelas__c_digo___ + request.status + traducao.novaReunioes__mensagem___ + request.statusText);
        }
    });
}

function verificarQuadrosAbertos(quadro) {
    verificarVisibilidadeQuadros();
    for (var i = 0; i < quadro.groups.length; i++) {
        if (quadro.groupsExpanding[i] && quadro.groups[i].visible) {
            atualizarPorQuadros(quadro.groups[i].name);
        }
    }
}

function verificarVisibilidadeQuadros() {
    var quadro = quadrosPrincipais;
    var endereco = enderecoBaseAtual();
    var codigos;
    $.ajax({
        url: endereco + "/Servicos/Service.svc/itemEventoAtual",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (data.d == undefined)
                    codigos = data.toString();
                else
                    codigos = data.d.toString();
                codigos = codigos.split("§");
                var numero = Number(codigos[1]);
                for (var i = 0; i < quadro.groups.length; i++) 
                {
                    if ((quadro.groups[i].name == "dadosDeliberacoes"
                        || quadro.groups[i].name == "dadosPlanos"
                        || quadro.groups[i].name == "dadosAnexos")
                        && (numero < 1
                        )){
                        quadro.groups[i].SetVisible(false);
                    }
                    else 
                    {
                        if (numero !== -99 && numero < 1 && quadro.groups[i].name != "dadosItem")
                            quadro.groups[i].SetVisible(false);
                        else
                            quadro.groups[i].SetVisible(true);
                    }
                }

            }

        },
        error: function (request, status, error) {
            avisoErroConexaoWebService();
            console.log(status.toString() + traducao.novaReunioes__na_resposta_do_webservice_de_item_atual__c_digo___ + request.status + traducao.novaReunioes__mensagem___ + request.statusText);
        }
    });
    
}

function atualizarPorQuadros(nome) {
    switch (nome) {
        case "dadosItem":
            cpDadosItem.PerformCallback('atualizarDados');
            break;
        case "dadosDeliberacoes":
            cpDeliberacaoAtual.PerformCallback('atualizarDados');
            break;
        case "dadosPlanos":
            cpPlanosItens.PerformCallback('atualizarDados');
            break;
        case "dadosAnexos":
            cpAnexosItens.PerformCallback('atualizarDados');
            break;
        case "dadosComentarios":
            cpComentariosItens.PerformCallback('atualizarDados');
            break;
        case "dtItens":
            cpTodosItens.PerformCallback('atualizarDados');
            break;
        case "dtParticipantes":
            cpParticipantes.PerformCallback('atualizarDados');
            break;
    }
}

function enviarAnexo(s, e) {
    if (uploadAnexos.GetText(0) == "") {
        window.top.mostraMensagem(traducao.novaReunioes_por_favor_selecione_um_arquivo_para_anexar_, 'Atencao', true, false, null);
        return;
    }
    else if (inputAnexos.GetText(0) == "") {
        window.top.mostraMensagem(traducao.novaReunioes_por_favor_especificar_o_t_tulo_do_arquivo_, 'Atencao', true, false, null);
    }
    else {
        uploadAnexos.Upload();
        
    }
}

function enderecoBaseAtual() {
    var endereco = window.location.href.split('/');
    var numero = endereco.indexOf("Reunioes");
    endereco.splice(numero, 2);
    return endereco.join('/');
}

function permissoesUsuarios() {
    var modo;
    var moderador = false;
    var endereco = enderecoBaseAtual();
    $.ajax({
        url: endereco + "/Servicos/Service.svc/permissoesUsuario",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (data.d == undefined)
                    modo = data.toString();
                else
                    modo = data.d.toString();
                modo = modo.split("§");
                var gridPlanos = true;
                var gridParticipantes = true;
                var gridItens = true;
                var memoDeliberacao = true;
                moderador = modo[0] == "moderador";
                if (modo[1] == "iniciada") {
                    switch (modo[0]) {
                        case "participante":
                            //botões
                            botaoMarcarParticipantes.SetEnabled(false);
                            botaoAdicionarAnexos.SetEnabled(true);
                            botaoAdicionarComentario.SetEnabled(true);
                            //Colunas grids
                            gridPlanos = "none";
                            gridParticipantes = "none";
                            gridItens = "none";
                            //Memo
                            memoDeliberacao = "readonly";
                            break;
                        case "moderador":
                            //botões
                            botaoMarcarParticipantes.SetEnabled(true);
                            botaoAdicionarAnexos.SetEnabled(true);
                            botaoAdicionarComentario.SetEnabled(true);
                            //Colunas grids
                            gridPlanos = "block";
                            gridParticipantes = "block";
                            gridItens = "block";
                            //Memo
                            memoDeliberacao = "";
                            break;
                        case "assistente":
                            //botões
                            //botões
                            botaoMarcarParticipantes.SetEnabled(false);
                            botaoAdicionarAnexos.SetEnabled(true);
                            botaoAdicionarComentario.SetEnabled(true);
                            //Colunas grids
                            gridPlanos = "block";
                            gridParticipantes = "none";
                            gridItens = "none";
                            //Memo
                            memoDeliberacao = "";
                            break;
                        case "convidado":
                            //botões
                            botaoMarcarParticipantes.SetEnabled(false);
                            botaoAdicionarAnexos.SetEnabled(false);
                            botaoAdicionarComentario.SetEnabled(false);
                            //Colunas grids
                            gridPlanos = "none";
                            gridParticipantes = "none";
                            gridItens = "none";
                            //Memo
                            memoDeliberacao = "readonly";
                            break;
                    }
                }
                else {
                    //botões
                    botaoMarcarParticipantes.SetEnabled(false);
                    botaoAdicionarAnexos.SetEnabled(false);
                    botaoAdicionarComentario.SetEnabled(false);
                    //Colunas grids
                    gridPlanos = "none";
                    gridParticipantes = "none";
                    gridItens = "none";
                    //Memo
                    memoDeliberacao = "readonly";
                }
                $('td.classe_gvParticipantes_IndicaParticipantePresente').css('display', gridParticipantes);
                $('td.classe_gvTodosItens_IndicaItemTratado').css('display', gridItens);
                $('td.classe_gvPlanosPendentes_comandos').css('display', gridPlanos);
                $('td.classe_gvPlanos_colEditar').css('display', gridPlanos);
                if (memoDeliberacao == "readonly")
                    $('#quadroDeliberacoes textarea').attr('readonly', 'readonly');
                else
                    $('#quadroDeliberacoes textarea').removeAttr('readonly');
                if (moderador) {
                    mudarBotaoIniciar(modo[1]);
                }
                else {
                    mudarBotaoIniciar("naoModerador");
                }

                ///Aviso usuário
                if (modo.length == 4) {
                    if (modo[2] != "") {
                        lbAvisoUsuarioTv.SetText(modo[2]);
                        botaoAvisoUsuarioFechar.SetVisible(modo[3] == "True" ? true : false);
                        if (!pcAvisoUsuario.IsVisible())
                            pcAvisoUsuario.Show();
                    }
                }


            }
        },
        error: function (request, status, error) {
            avisoErroConexaoWebService();
            console.log(status.toString() + traducao.novaReunioes__na_resposta_do_webservice_de_permiss_o__c_digo___ + request.status + " Mensagem = " + request.statusText);
        }
    });
}

function avisoErroConexaoWebService() {
    if (sessionStorage.avisoErro == undefined || !Boolean(sessionStorage.avisoErro)) {
        window.top.mostraMensagem(traducao.novaReunioes_n_o_foi_realizado_a_conex_o_com_a_base_de_dados_, 'Atencao', true, false, null);
        sessionStorage.avisoErro = "true";
    }
}

function mudarBotaoIniciar(modo) {
    var memuBotaoIniciar = menuPrincipal.rootItem.GetItemByName("iniciar");
    switch(modo)
    {
        case "naoIniciada":
            memuBotaoIniciar.items[0].SetVisible(true);
            memuBotaoIniciar.items[1].SetVisible(false);
            memuBotaoIniciar.SetVisible(true);
            memuBotaoIniciar.SetImageUrl("../imagens/Reuniao/play.png");
            break;
        case "iniciada":
            memuBotaoIniciar.items[0].SetVisible(false);
            memuBotaoIniciar.items[1].SetVisible(true);
            memuBotaoIniciar.SetVisible(true);
            memuBotaoIniciar.SetImageUrl("../imagens/Reuniao/parar.png");
            break;
        case "finalizada":
        case "naoModerador":
            memuBotaoIniciar.items[0].SetVisible(false);
            memuBotaoIniciar.items[1].SetVisible(false);
            memuBotaoIniciar.SetVisible(false);
            break;
    }
}

function retornoQuadros() {
    aplicarMudancas();
}

function aplicarMudancas() {
    aplicarMudancaFonte();
    permissoesUsuarios();
}

function graficoMetaIndicadoresItem(codigo) {
    var data = new Date();
    var dataAtual = data.getFullYear() + "_" + data.getMonth() + "_" + data.getDate() + "_" + data.getHours() + "_" + data.getMinutes() + "_" + data.getSeconds() + "_" + data.getMilliseconds();
    var valores = codigoDadosIndicadoresMetaAtual.GetText();
    if(valores != undefined && valores.indexOf("§") >= 0)
    {
        var valor = valores.split("§");
        getGrafico(valor[0], "grafico_" + valor[4] + "_" + dataAtual, valor[1], valor[2], valor[3], valor[4]);
    }
    aplicarMudancas();
}

function graficoProjetoItem(valores) {
    var data = new Date();
    var dataAtual = data.getFullYear() + "_" + data.getMonth() + "_" + data.getDate() + "_" + data.getHours() + "_" + data.getMinutes() + "_" + data.getSeconds() + "_" + data.getMilliseconds();
    if (valores != undefined && valores.indexOf("§") >= 0) {
        var valor = valores.split("§");
        if (valor.length == 5) {
            $("#" + valor[4]).parent().show();
            $("#trLegendas_" + valor[4]).show();
            getGrafico(valor[0], "grafico_" + valor[4] + "_" + dataAtual, valor[1], valor[2], valor[3], valor[4]);
        }
        else {
            $("#" + valor[4]).parent().hide();
            $("#trLegendas_" + valor[4]).hide();
        }
    }
    aplicarMudancas();
}

function botaoCustomizadoGvPlanos(s, e) {
    switch (e.buttonID) {
        case "botaoEditar":
            pcEditarPlanos.Show();
            pcEditarPlanos.PerformCallback(e.visibleIndex);
            break;
        case "botaoHistoricoPlanos":
            cpPlanosItens.PerformCallback("atualizarDadosHistorico");
            break;
        case "botaoExcluir":
            planoDeAcaoAExcluir = e.visibleIndex + "§botaoExcluir";
            window.top.mostraMensagem(traducao.novaReunioes_deseja_realmente_excluir_o_plano_de_a__o_selecionado_, 'confirmacao', true, true, excluiPlanoAcaoSelecionado);
            break;
        case "":
            cpEditarPlanos.PerformCallback(e.visibleIndex);
            break;
    }
}

function excluiPlanoAcaoSelecionado() {
    cpPlanosItens.PerformCallback(planoDeAcaoAExcluir);
}

function botaoMostarHistoricoPlanos(s, e) {
    cpPlanosItens.PerformCallback('atualizarDadosHistorico');
}

function botaoCustomizadoGvPlanosPendentes(s, e) {
    switch (e.buttonID) {
        case "botaoEditarPendente":
            //gvPlanosPendentes.PerformCallback(e.visibleIndex + "§editar");
            pcEditarPlanosPendentes.Show();
            pcEditarPlanosPendentes.PerformCallback(e.visibleIndex + "§editar");
            break;
        case "botaoExcluirPendente":
            planoDeAcaoPendenteAExcluir = e.visibleIndex + "§excluir";
            window.top.mostraMensagem(traducao.novaReunioes_deseja_realmente_excluir_o_plano_de_a__o_pendente_selecionado_, 'confirmacao', true, true, excluiPlanoAcaoPendente);
            break;
    }
}

function excluiPlanoAcaoPendente() {
    gvPlanosPendentes.PerformCallback(planoDeAcaoPendenteAExcluir);
}

function retornoCallbackObjetos() {
    aplicarMudancas();
}


// ]]> 