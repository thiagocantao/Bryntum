
$(document).ready(function () {

    // idioma
    var idioma = document.querySelector('#idioma').innerHTML;

    // é Sprint
    var ehSprint = document.querySelector('#ehSprint').innerHTML;

    // traducao
    traducao.kanban_Meses = JSON.parse(traducao.kanban_Meses);

    var codigoProjeto = document.querySelector('#codigoProjeto').innerHTML;

    var sprint;

    // pesquisaTexto
    var pesquisaTexto = '';

    var timerPesquisaTexto;
    var delayPesquisaTexto = 1000;

    var primeiraRaia = {};
    var ultimaRaia = {};

    var canvasFiltraKanbanPorBugs = document.getElementById("canvasFiltraKanbanPorBugs");
    var original_canvasFiltraKanbanPorBugs = new MarvinImage();
    var image_canvasFiltraKanbanPorBugs;

    original_canvasFiltraKanbanPorBugs.load("../../../imagens/agil_bug.png", function () {
        image_canvasFiltraKanbanPorBugs = this.clone();
        Marvin.grayScale(this, image_canvasFiltraKanbanPorBugs);
        image_canvasFiltraKanbanPorBugs.draw(canvasFiltraKanbanPorBugs);
    });

    var clicouNoFiltroPorBug = false;

    var canvasFiltraKanbanPorImpedimentos = document.getElementById("canvasFiltraKanbanPorImpedimentos");
    var original_canvasFiltraKanbanPorImpedimentos = new MarvinImage();
    var image_canvasFiltraKanbanPorImpedimentos;

    original_canvasFiltraKanbanPorImpedimentos.load("../../../imagens/agil_impedimento.png", function () {
        image_canvasFiltraKanbanPorImpedimentos = this.clone();
        Marvin.grayScale(this, image_canvasFiltraKanbanPorImpedimentos);
        image_canvasFiltraKanbanPorImpedimentos.draw(canvasFiltraKanbanPorImpedimentos);
    });

    var clicouNoFiltroPorImpedimentos = false;


    var canvasFiltraKanbanPorUsuarioLogado = document.getElementById("canvasFiltraKanbanPorUsuarioLogado");
    var original_canvasFiltraKanbanPorUsuarioLogado = new MarvinImage();
    var image_canvasFiltraKanbanPorUsuarioLogado;

    original_canvasFiltraKanbanPorUsuarioLogado.load("images/user-icon.png", function () {
        image_canvasFiltraKanbanPorUsuarioLogado = this.clone();
        Marvin.grayScale(this, image_canvasFiltraKanbanPorUsuarioLogado);
        image_canvasFiltraKanbanPorUsuarioLogado.draw(canvasFiltraKanbanPorUsuarioLogado);
    });

    var clicouNoFiltroPorUsuarioLogado = false;

    var codigoStatusItemSelecionado = -1;

    ['input', 'propertychange'].map(function (evento) {
        document.querySelector('#pesquisaTexto').addEventListener(evento, function (e) {
            clearTimeout(timerPesquisaTexto);
            timerPesquisaTexto = setTimeout(function () { kanbanPesquisaTexto() }, delayPesquisaTexto);
        });
    });

    document.querySelector('#pesquisaTexto').addEventListener('keypress', function (e) {
        if (e.which == 13) {
            clearTimeout(timerPesquisaTexto);
            kanbanPesquisaTexto();
            e.preventDefault();
        }
        else {
            clearTimeout(timerPesquisaTexto);
            timerPesquisaTexto = setTimeout(function () { kanbanPesquisaTexto() }, delayPesquisaTexto);
        }
    });



    function kanbanPesquisaTexto() {
        pesquisaTexto = document.querySelector('#pesquisaTexto').value.trim();
        kanbanAgilCarregaRaias();
        $('#pesquisaTexto').focus().select();
    }

    function kanbanAgilCarregaSprint(carregaRaias) {
        if (carregaRaias == null) {
            carregaRaias = true;
        }
        $.ajax({
            data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto),
            url: './agil-taskboard-service.asmx/obter-informacoes-gerais',
            type: 'POST',
            beforeSend: function () { window.top.lpAguardeMasterPage.Show(); }
        }).done(function (data, textStatus, jqXHR) {
            sprint = JSON.parse(data.firstElementChild.innerHTML)[0];

            // Carrega a data de iní­cio e fim da Sprint.
            var encerrada = (sprint.IniciaisStatus === 'SPRINTENCERRADA') ? ' - Encerrada em ' + sprint.TerminoReal : '';
            var dataSprint = sprint.Inicio + ' a ' + sprint.Termino + encerrada;
            document.querySelector('#kanban-agil-inicio-termino-sprint').innerHTML = dataSprint;
            //document.querySelector('.kanban-agil-data-sprint > span:nth-child(1)').innerHTML = sprint.NomeSprint;

            configuraBotoesDoMenuBarsEsquerdo(sprint);

            $('#kanban-agil-cabecalho-id').removeClass('kanban-agil-cabecalho-indica-item-bloqueado');
            if (sprint.IndicaQueTemItemBloqueado === true) {

                $('#kanban-agil-cabecalho-id').addClass('kanban-agil-cabecalho-indica-item-bloqueado');
            }
            // Carrega as raias ou colunas do Kanban.
            if (carregaRaias) {
                kanbanAgilCarregaRaias();
            }
            else {
                window.top.lpAguardeMasterPage.Hide();
            }

        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });




    }

    function configuraBotoesDoMenuBarsEsquerdo(sprint) {

        var botaoOff = document.querySelector('#icoEncerrarSprint');
        botaoOff.setAttribute('style', 'visibility:visible');
        if (sprint.IniciaisStatus === 'SPRINTENCERRADA') {
            botaoOff.setAttribute('style', 'display:none');
        }
        if (ehSprint == 'False') {
            botaoOff.setAttribute('style', 'display:none');
        }
        if (sprint.Permissao === true) {
            document.addEventListener('click', function (e) {
                if ((e.target) && (e.target.id == 'icoEncerrarSprint')) {
                    var el = document.getElementById("icoEncerrarSprint");
                    //el.click();
                    //e.preventDefault();
                    //alert('clique ocorrei');
                    if (document.fullscreenElement) {
                        document.exitFullscreen();
                        setTimeout(function () {
                            window.top.mostraConfirmacao('Ao encerrar a Sprint, todos os itens não finalizados serão movidos para o backlog do projeto\t\n e nenhuma alteração poderá mais ser realizada nesta Sprint. \t\nEsta operação não poderá ser desfeita! Confirma o encerramento da Sprint?', eventoOkParaEncerrarSprint, eventoCancelarParaEncerrarSprint);
                        }, 300);
                    }
                    else {
                        window.top.mostraConfirmacao('Ao encerrar a Sprint, todos os itens não finalizados serão movidos para o backlog do projeto\t\n e nenhuma alteração poderá mais ser realizada nesta Sprint. \t\nEsta operação não poderá ser desfeita! Confirma o encerramento da Sprint?', eventoOkParaEncerrarSprint, eventoCancelarParaEncerrarSprint);
                    }
                }
            });
        }
        else {
            botaoOff.setAttribute('style', 'visibility:visible;color:darkgray');
            botaoOff.setAttribute('title', 'Voce não tem permissão para encerrar uma sprint');
        }

        var botaoConfigurarRaias = document.querySelector('#icoConfigurarRaias');
        if (sprint.PermissaoManterRaias === true) {
            document.addEventListener('click', function (e) {
                if ((e.target) && (e.target.id == 'icoConfigurarRaias')) {
                    var el = document.getElementById("icoConfigurarRaias");
                    //el.click();
                    //e.preventDefault();
                    if (document.fullscreen)
                        document.exitFullscreen();
                    var url = window.top.pcModal.cp_Path + '_Projetos/Agil/CadastroRaias.aspx?CP=' + codigoProjeto;
                    window.top.showModal(url, 'Raias', null, null, '', null);
                }
            });
        }
        else {
            botaoConfigurarRaias.setAttribute('style', 'visibility:visible;color:darkgray');
            botaoConfigurarRaias.setAttribute('title', 'Você não tem permissão para configurar uma raia');
        }


        var botaoEditarEquipe = document.querySelector('#icoEditarEquipe');
        if (sprint.PermissaoManterEquipeAgil === true) {
            document.addEventListener('click', function (e) {
                if ((e.target) && (e.target.id == 'icoEditarEquipe')) {
                    var el = document.getElementById("icoEditarEquipe");
                    //el.click();
                    //e.preventDefault();
                    if (document.fullscreen)
                        document.exitFullscreen();
                    var url = window.top.pcModal.cp_Path + '_Projetos/Agil/EquipeProjetoAgil.aspx?CP=' + codigoProjeto;
                    window.top.showModal(url, 'Equipe', null, null, '', null);
                }
            });
        }
        else {
            botaoEditarEquipe.setAttribute('style', 'visibility:visible;color:darkgray');
            botaoEditarEquipe.setAttribute('title', 'Você não tem permissão para configurar uma equipe ágil');
        }

        var botaoBurnDown = document.querySelector('#icoBurndown');
        if (sprint.PermissaoVisualizarPainelSprint === true) {
            document.addEventListener('click', function (e) {
                if ((e.target) && (e.target.id == 'icoBurndown')) {
                    var el = document.getElementById("imgBurndown");
                    el.click();
                    e.preventDefault();
                }
            });
        }
        else {
            botaoBurnDown.setAttribute('style', 'visibility:visible;color:darkgray');
            botaoBurnDown.setAttribute('title', 'Você não tem permissão para visualizar gráfico de evolução');
        }


        configuraSePermiteAssociarItens();


        document.addEventListener('click', function (e) {
            if ((e.target) && (e.target.id == 'icoRefreshKanban')) {
                window.location.reload();
            }
        });
    }

    function configuraSePermiteManterItens() {
        var campo = document.querySelector('#campoincluirapidobacklog');
        var campoMais = document.querySelector('#dropdownAdicionarItemBacklog');
        if (sprint.PermissaoIncluirItensSprint === true) {
            //style = ""
            if (campoMais != undefined) {
                campoMais.setAttribute('style', 'visibility:visible');
                campo.setAttribute('style', 'border-top-left-radius: 3; border-bottom-left-radius: 3;font-size:0.72em;padding-left:5px;padding-right:5px;visibility:visible');
            }

            document.addEventListener('click', function (e) {
                if ((e.target) && (e.target.id === 'dropdownAdicionarItemBacklog')) {
                    kanbanAgilEditaTarefa(e.target.parentElement.parentElement.parentElement, 'kanbanAgilIncluiTarefaBacklogNaoPlanejada');
                }
            });
        }
        else {
            campoMais.setAttribute('style', 'visibility:hidden');
            campo.setAttribute('style', 'border-top-left-radius: 3; border-bottom-left-radius: 3;font-size:0.72em;padding-left:5px;padding-right:5px;visibility:hidden');
        }
    }

    function kanbanAgilCarregaRaias() {

        document.querySelector('#kanban').innerHTML = "";

        $.ajax({
            data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto),
            url: './agil-taskboard-service.asmx/obter-raias',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {
            document.querySelector('#kanban').innerHTML = '';
            var div = document.createElement('div');
            div.classList.add('kanban-agil-raias');

            var h2 = document.createElement('h2');
            h2.setAttribute('data-codigoitem', '0');
            h2.setAttribute('data-codigoitemsuperior', '0');
            var title = 'Backlog';
            var raias = JSON.parse(data.firstElementChild.innerHTML);
            h2.innerHTML = ' <font style="font-size:14px;margin-right: 3px;">Backlog</font> <font style="color:black;font-weight:regular;font-size:14px; margin-right: 5px;">(' + raias[0].QuantidadeItensBacklogNaIteracao + ') </font><input class="form-control" id="campoincluirapidobacklog" autocomplete="off" maxlength="200" placeholder="Digite aqui para incluir novo item" style="border-top-left-radius: 3; border-bottom-left-radius: 3;font-size:0.72em;padding-left:5px;padding-right:5px" type="text">';
            h2.setAttribute('title', title);

            html = '';

            if (sprint.IndicaUsuarioLogadoMembroEquipe && sprint.IniciaisStatus !== 'SPRINTENCERRADA') {
                html += '<div class="float-right">';
                html += '<div id="kanbanAgilIncluiTarefaBacklogNaoPlanejadaMenu" title="Incluir Backlog">';
                html += '<i  id="dropdownAdicionarItemBacklog" class="fas fa-plus" style="cursor: pointer;"></i>';
                html += '</div>';
                //html += '<div aria-labelledby="kanbanAgilIncluiTarefaBacklogNaoPlanejadaMenu" class="dropdown-menu">';
                //html += '<div>';
                //html += '<a id="dropdownAdicionarItemBacklog" href="javascript:void(0);" title="Adicionar Item"><i class="fas fa-plus-square"></i></a>';
                //html += '</div>';
                //html += '</div>';

                var el = document.createElement('div');
                el.innerHTML = html;
                var iplus = el.children[0];
                h2.appendChild(iplus);

                //configuraSePermiteAssociarItens();
            }

            div.appendChild(h2);

            primeiraRaia = raias.find(element => element.PercentualConcluido === 0);
            ultimaRaia = raias.find(element => element.PercentualConcluido === 100);

            configuraCoresEQuantidadeDeItensNaRaia(raias, div);

            document.querySelector('#kanban').appendChild(div);
            $.ajax({
                data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=0' + '&pesquisaTexto=' + encodeURI(pesquisaTexto) + (clicouNoFiltroPorBug === true ? '&indicaProblema=S' : '&indicaProblema=N') + (clicouNoFiltroPorImpedimentos === true ? '&indicaBloqueioItem=S' : '&indicaBloqueioItem=N') + '&codigoStatusItem=' + codigoStatusItemSelecionado + (clicouNoFiltroPorUsuarioLogado === true ? '&indicaFiltroPorUsuarioLogado=S' : '&indicaFiltroPorUsuarioLogado=N'),
                url: './agil-taskboard-service.asmx/obter-tarefas',
                type: 'POST'
            }).done(function (data, textStatus, jqXHR) {

                var itens = JSON.parse(data.firstElementChild.innerHTML);


                var existeContainercards = document.querySelector('#divContainerCards');
                if (existeContainercards !== null) {
                    return;
                }

                var divContainerCards = document.createElement('div');
                divContainerCards.setAttribute('id', 'divContainerCards');
                var sHeight = Math.max(0, document.documentElement.clientHeight) - 135;
                divContainerCards.setAttribute('style', 'height:' + sHeight + 'px;overflow-y: initial;width: max-content;');

                // Adiciona todos os backlogs e respectivas raias.
                for (var i = 0; i < itens.length; i++) {

                    var item = itens[i];

                    var backlog = item.Dados;

                    var div = document.createElement('div');
                    div.classList.add('kanban-agil');
                    div.setAttribute('data-codigoitem', backlog.CodigoItem);


                    // <ul> do Backlog
                    var ul = document.createElement('ul');
                    ul.appendChild(kanbanAgilTarefa(backlog, true));
                    div.appendChild(ul);

                    for (var j = 0; j < raias.length; j++) {

                        var raia = raias[j];

                        // <ul> da Raia
                        ul = document.createElement('ul');
                        ul.setAttribute('data-codigoraia', raia.CodigoRaia);
                        ul.setAttribute('data-percentualconcluido', raia.PercentualConcluido);
                        div.appendChild(ul);

                    }
                    divContainerCards.appendChild(div);
                }

                var containerKanban = document.querySelector('#kanban');
                var inseriuContainercards = containerKanban.querySelector('.kanban-agil');
                if (inseriuContainercards == null) {
                    document.querySelector('#kanban').appendChild(divContainerCards);
                }


                for (var i = 0; i < itens.length; i++) {
                    var item = itens[i];
                    var backlog = item.Dados;
                    var possuiItensFilhos = backlog.QuantidadeItensFilhos > 0;
                    kanbanAgilRaiasArrastarSoltar(backlog.CodigoItem, possuiItensFilhos);
                }

                // Adiciona todas as tarefas às respectivas raias <ul> dos respectivos backlogs.
                for (var i = 0; i < itens.length; i++) {

                    var item = itens[i];

                    for (var j = 0; j < item.Tarefas.length; j++) {

                        var tarefa = item.Tarefas[j];

                        // Seleciona o <div> do backlog da tarefa.
                        var div = document.querySelector('div[data-codigoitem="' + tarefa.CodigoItemSuperior + '"]');
                        // Seleciona a <ul> da raia do backlog da tarefa.
                        if (div !== null) {
                            var ul = div.querySelector('ul[data-codigoraia="' + tarefa.CodigoRaia + '"]');
                            if (ul !== null) {
                                ul.appendChild(kanbanAgilTarefa(tarefa));
                            }
                        }

                    }
                }
                window.top.lpAguardeMasterPage.Hide();
                if ($('.kanban-agil').length === 0)
                    $('.kanban-agil-raias').first().after($('<div class="no-data-to-display">Não há dados a serem exibidos</div>'));
            }).fail(function (jqXHR, textStatus, msg) {
                mostraErroEEscondeLoading(jqXHR);
            });
            configuraCampoDeInclusaoEmMassa(sprint);
            configuraSePermiteManterItens();
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });


    }

    function configuraCoresEQuantidadeDeItensNaRaia(raias, div) {
        for (var i = 0; i < raias.length; i++) {

            var raia = raias[i];

            var h2 = document.createElement('h2');
            h2.setAttribute('data-codigoraia', raia.CodigoRaia);
            h2.style.backgroundColor = raia.CorCabecalho;

            h2.innerHTML = '<font style="font-size:14px">' + raia.NomeRaia + ' (' + raia.QuantidadeItensNaRaia + ')</font>';
            if ((raia.QuantidadeMaximaItensRaia !== null) &&
                (raia.QuantidadeItensNaRaia > raia.QuantidadeMaximaItensRaia)) {
                h2.innerHTML = raia.NomeRaia + '<font style="color:red;font-weight:bold;font-size:14px"> (' + raia.QuantidadeItensNaRaia + ')</font>';
                h2.setAttribute('title', 'Esta raia excedeu o número máximo de tarefas previstas');
            }
            h2.setAttribute('title', raia.NomeRaia);
            var span = document.createElement('span');
            span.innerHTML = raia.PercentualConcluido + '%';

            h2.appendChild(span);

            div.appendChild(h2);

        }
    }

    function configuraSePermiteAssociarItens() {
        var podeAssociarItens = false;
        $.ajax({
            data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto),
            url: './agil-taskboard-service.asmx/verifica-pode-associar-itens',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {
            podeAssociarItens = !!(data)
                && !!(data.firstElementChild)
                && (data.firstElementChild.innerHTML === 'true');
            if (!podeAssociarItens) {
                var botaoSetaPBaixo1 = document.querySelector('#dropdownAssociarItemBacklog');
                if (ehSprint == 'True') {
                    botaoSetaPBaixo1.setAttribute('style', 'visibility:visible;color:darkgray;cursor:default');
                    botaoSetaPBaixo1.setAttribute('title', 'Não existem itens a serem atualizados.');
                }
                else {
                    botaoSetaPBaixo1.setAttribute('style', 'display:none');
                }

            }
            else {
                $.ajax({
                    data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto),
                    url: './agil-taskboard-service.asmx/verifica-pode-associar-itens2',
                    type: 'POST'
                }).done(function (data, textStatus, jqXHR) {
                    podeAssociarItens = (data.firstElementChild.innerHTML === 'true');
                    if (!podeAssociarItens) {
                        var botaoSetaPBaixo = document.querySelector('#dropdownAssociarItemBacklog');
                        if (ehSprint == 'True') {
                            botaoSetaPBaixo.setAttribute('style', 'visibility:visible;color:darkgray;cursor:default');
                            botaoSetaPBaixo.setAttribute('title', 'Voce não tem permissão para associar itens de backlog.');
                        }
                        else {
                            botaoSetaPBaixo.setAttribute('style', 'display:none');
                        }
                    }
                    else {
                        document.addEventListener('click', function (e) {
                            if ((e.target) && (e.target.id === 'dropdownAssociarItemBacklog')) {
                                kanbanAgilEditaTarefa(e.target.parentElement.parentElement.parentElement, 'kanbanAgilAssociaTarefaBacklogNaoPlanejada');
                            }
                        });
                    }
                });
            }
        });
    }

    function configuraCampoDeInclusaoEmMassa(sprint) {
        var campoIncluiRapido = document.getElementById('campoincluirapidobacklog');
        campoIncluiRapido.addEventListener('keypress', function (e) {
            //alert(e.which);
            if (e.which == 39) {
                e.preventDefault();
                return false;
            }
            if (e.which == 13) {//se teclou enter
                var tituloItemBacklog = document.querySelector('#campoincluirapidobacklog').value;
                //alert('clicou na imagem o backlog é:' + descricaoItemBacklog);
                if (tituloItemBacklog.trim() === '') {
                    e.preventDefault();
                    return false;
                }
                $.ajax({
                    data: 'codigoProjeto=' + encodeURI(codigoProjeto) + '&tituloItem=' + tituloItemBacklog,
                    url: './agil-taskboard-service.asmx/incluir-backlog-rapidamente',
                    type: 'POST'
                }).done(function (data2, textStatus, jqXHR) {
                    var item = JSON.parse(data2.firstElementChild.innerHTML)[0];
                    //var divBacklog = document.querySelector('div[data-codigoiteracao="0"] > div:not(:first-child) > div > div > ul');
                    //divBacklog.appendChild(kanbanAgilItemBacklog(item));
                    document.querySelector('#campoincluirapidobacklog').value = '';
                    //console.log(item);
                    var kanban1 = document.querySelector('#divContainerCards');

                    var divAEncaixarNoKanban = document.createElement('div');
                    divAEncaixarNoKanban.classList.add('kanban-agil');
                    divAEncaixarNoKanban.setAttribute('data-codigoitem', item.CodigoItem);

                    var ulAEncaixarNaDiv = document.createElement('ul');
                    ulAEncaixarNaDiv.classList.add('ui-sortable');
                    ulAEncaixarNaDiv.appendChild(kanbanAgilTarefa(item, true));
                    divAEncaixarNoKanban.appendChild(ulAEncaixarNaDiv);
                    $.ajax({
                        data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto),
                        url: './agil-taskboard-service.asmx/obter-raias',
                        type: 'POST'
                    }).done(function (data3, textStatus, jqXHR) {
                        var raias = JSON.parse(data3.firstElementChild.innerHTML);
                        console.log(raias);
                        for (var i = 0; i < raias.length; i++) {
                            //<ul data-codigoraia="73" data-percentualconcluido="0" class="ui-sortable"></ul>
                            var ulRaia = document.createElement('ul');
                            ulRaia.setAttribute('data-codigoraia', raias[i].CodigoRaia);
                            ulRaia.setAttribute('data-percentualconcluido', raias[i].PercentualConcluido);
                            ulRaia.classList.add('ui-sortable');
                            divAEncaixarNoKanban.appendChild(ulRaia);
                        }
                        kanban1.appendChild(divAEncaixarNoKanban);

                        var elemNodataDisplay = document.querySelector('.no-data-to-display');
                        if (elemNodataDisplay != null)
                            elemNodataDisplay.remove();

                        var divRaias = document.querySelector('.kanban-agil-raias > h2 > font:not(:first-child)');
                        var divListaBacklog = document.querySelectorAll('.kanban-agil');
                        divRaias.innerHTML = '(' + divListaBacklog.length + ')';

                        //var possuiItensFilhos = item.QuantidadeItensFilhos > 0;
                        kanbanAgilRaiasArrastarSoltar(item.CodigoItem, false);
                    }).fail(function (jqXHR, textStatus, msg) {
                        mostraErroEEscondeLoading(jqXHR);
                    });

                }).fail(function (jqXHR, textStatus, msg) {
                    mostraErroEEscondeLoading(jqXHR);
                });
            }
        });
        if (sprint.IniciaisStatus === 'SPRINTENCERRADA') {
            campoIncluiRapido.style.visibility = 'hidden';
        }
        else {
            campoIncluiRapido.style.visibility = 'visible';
        }
    }

    function kanbanAgilTarefa(item, backlog) {

        if (backlog == null) {
            backlog = false;
        }
        var html = '';
        var data = item.DataAlvo;
        var dataAlvoObjetoJavascript = null;
        if (data != undefined) {
            dataAlvoObjetoJavascript = new Date(data.substr(6, 4), (data.substr(3, 2) - 1), data.substr(0, 2));//28/04/2020
        }

        if (data == null) {

            data = '';
        }
        else {
            data = data.substr(0, 2) + ' ' + traducao.kanban_Meses[parseInt(data.substr(3, 2), 10) - 1] + ' ' + data.substr(-4);
        }

        html += '<div class="kanban-agil-tarefa">';

        anexo = '';
        if (item.PossuiAnexo == 1) {
            if (backlog) {
                anexo += '<i class="fas fa-paperclip" title="Este item possui anexos"></i> ';
            } else {
                anexo += '<i class="fas fa-paperclip" title="Esta tarefa possui anexos"></i> ';
            }
        }

        var htmlIndicaComentario = '';
        if ((item.QuantComentario) && (item.QuantComentario > 0)) {
            if (backlog) {
                htmlIndicaComentario += '<i class="far fa-comment" title="Este item possui comentários"></i> ';
            } else {
                htmlIndicaComentario += '<i class="far fa-comment" title="Esta tarefa possui comentários"></i> ';
            }
        }

        html += (item.IndicaBloqueioItem == 'S') ? '<div class="kanban-agil-tarefa-cabecalho-titulo-bloqueio">' : '<div class="kanban-agil-tarefa-cabecalho-titulo">';


        html += '<div title="' + htmlEncode(item.CodigoItem + ' ' + htmlDecode(item.TituloItem)) + '">' + htmlDecode(item.TituloItem) + '</div>';
        html += '</div>';
        //alert(sprint.PermissaoIncluirTarefasItensBacklog + '< - incluir // Excluir ->  ' + sprint.PermissaoExcluirTarefasItensBacklog);

        if (sprint.IndicaUsuarioLogadoMembroEquipe) {
            html += '<div class="kanban-agil-tarefa-menu" data-toggle="dropdown" id="kanbanAgilMenu">';
            html += '<i class="fas fa-ellipsis-h" style="display:none"></i>';
            html += '</div>';
            html += '<div aria-labelledby="kanbanAgilMenu" class="dropdown-menu">';
            if (backlog) {
                if (item.CodigoItemEspelho === true) {
                    if (sprint.PermissaoAlterarItensSprint === true) {
                        html += '<a class="dropdown-item kanban-agil-tarefa-menu-editar-item-backlog" href="javascript:void(0);" title="Editar Item"><i class="fas fa-edit"></i> Editar Item</a>';
                    }
                    if (sprint.PermissaoExcluirItensSprint === true) {
                        html += '<a class="dropdown-item kanban-agil-tarefa-menu-excluir-item-backlog" href="javascript:void(0);" title="Excluir Item"><i class="fas fa-trash"></i> Excluir Item</a>';
                    }
                }
                else {
                    if ((sprint.PermissaoIncluirItensSprint === true) || (sprint.PermissaoIncluirTarefasItensBacklog == true)) {
                        html += '<a class="dropdown-item kanban-agil-tarefa-menu-adicionar-tarefa" href="javascript:void(0);" title="Adicionar Tarefa"><i class="fas fa-plus-square"></i> Adicionar Tarefa</a>';
                    }
                    if (sprint.PermissaoAlterarItensSprint === true) {
                        html += '<a class="dropdown-item kanban-agil-tarefa-menu-editar-item-backlog" href="javascript:void(0);" title="Editar Item"><i class="fas fa-edit"></i> Editar Item</a>';
                    }
                    if (sprint.PermissaoExcluirItensSprint === true) {
                        html += '<a class="dropdown-item kanban-agil-tarefa-menu-excluir-item-backlog" href="javascript:void(0);" title="Excluir Item"><i class="fas fa-trash"></i> Excluir Item</a>';
                    }
                }

            }
            else {
                if ((sprint.PermissaoAlterarItensSprint === true) || (sprint.PermissaoIncluirTarefasItensBacklog === true)) {
                    html += '<a class="dropdown-item kanban-agil-tarefa-menu-editar-tarefa" href="javascript:void(0);" title="Editar Tarefa"><i class="fas fa-edit"></i> Editar Tarefa</a>';
                }
                if ((sprint.PermissaoExcluirItensSprint === true) || (sprint.PermissaoExcluirTarefasItensBacklog === true)) {
                    html += '<a class="dropdown-item kanban-agil-tarefa-menu-excluir-tarefa" href="javascript:void(0);" title="Excluir Tarefa"><i class="fas fa-trash"></i> Excluir Tarefa</a>';
                }
                if (item.IndicaBloqueioItem === 'S') {
                    html += '<a class="dropdown-item kanban-agil-tarefa-menu-registra-impedimento" href="javascript:void(0);" title="Remover Impedimento"><i class="fas fa-trash"></i> Remover Impedimento</a>';
                }
                else {
                    html += '<a class="dropdown-item kanban-agil-tarefa-menu-registra-impedimento" href="javascript:void(0);" title="Registrar Impedimento"><i class="fas fa-trash"></i> Registrar Impedimento</a>';
                }
            }
            html += '</div>';
        }

        html += (item.IniciaisUsuarioRecurso !== null) ? '<div class="kanban-agil-tarefa-usuario">' : '<div class="kanban-agil-tarefa-usuario-float-right">';
        if (item.IniciaisUsuarioRecurso !== null) {
            html += '<div class="kanban-agil-tarefa-usuario-sigla">';
            html += '<span title="' + htmlEncode(item.IniciaisUsuarioRecurso) + '">' + htmlEncode(item.IniciaisUsuarioRecurso) + '</span>';
            html += '</div>';
            html += '<div class="kanban-agil-tarefa-usuario-nome text-nowrap text-truncate" title="' + htmlEncode(item.NomeUsuarioRecurso) + '">' + htmlEncode(item.NomeUsuarioRecurso) + '</div>';
        }
        var concluido = "0";

        var htmlIconeTemporario = '';
        html += htmlIconeTemporario;
        if (backlog) {
            concluido = item.PercentualConcluido.toFixed(2).toString();
            concluido = concluido.substr(0, concluido.length - 3);
            html += '<div class="kanban-agil-tarefa-usuario-detalhes-concluido" title="Concluí­do: ' + htmlEncode(concluido) + '%"><span> ' + htmlEncode(concluido) + '%</span></div>';

        } else {
            concluido = item.PercentualConcluido.toFixed(2).toString();
            if (concluido.substr(-3) == '.00') {
                concluido = concluido.substr(0, concluido.length - 3);
            }

            html += '<div class="kanban-agil-tarefa-usuario-detalhes-concluido" title="Concluí­do: ' + htmlEncode(concluido) + '%"><span> ' + htmlEncode(concluido) + '%</span></div>';
        }

        html += '</div>';





        //if (data != '' || anexo != '' || htmlIndicaComentario !== '' || item.IniciaisTipoClassificacaoItemControladoSistema == 'PROBLEMA' || item.IndicaBloqueioItem.toString().trim() == 'S') {

        html += '<div class="kanban-agil-tarefa-detalhes">';
        if (data != '') {
            var atrasada = '';
            if (concluido < 100) {
                var dataAtual = new Date();
                if ((dataAlvoObjetoJavascript != null) && (dataAtual > dataAlvoObjetoJavascript)) {
                    if (dataAtual.getFullYear() === dataAlvoObjetoJavascript.getFullYear() &&
                        dataAtual.getMonth() === dataAlvoObjetoJavascript.getMonth() &&
                        dataAtual.getDate() === dataAlvoObjetoJavascript.getDate()) {
                        atrasada = '';
                    }
                    else {
                        atrasada = 'atrasada';
                    }
                }
            }
            html += '<div class="kanban-agil-tarefa-detalhes-data ' + atrasada + '" title="Data Alvo: ' + htmlEncode(data) + '"><span>Data Alvo</span><span><i class="far fa-calendar-alt"></i> ' + htmlEncode(data) + '</span></div>';
        }
        html += '<div style="width:100%; display:flex;flex-direction:row;justify-content: flex-end;">';

        if (htmlIndicaComentario) {
            html += '<div style="padding:0px 5px;">';
            html += htmlIndicaComentario;
            html += '</div>';
        }

        if (anexo) {
            html += '<div style="padding:0px 5px;">';
            html += anexo;
            html += '</div>';
        }

        if (item.IndicaBloqueioItem.toString().trim() == 'S') {
            html += '<div style="padding:0px 5px;"><img src="../../../imagens/agil_impedimento.png" title="Item com impedimento - Não é possível movê-lo entre raias enquanto estiver com impedimento!"></div>';
        }

        if (item.IniciaisTipoClassificacaoItemControladoSistema == 'PROBLEMA') {
            html += '<div style="padding:0px 5px;"><img src="../../../imagens/agil_bug.png" title="' + item.DescricaoTipoClassificacaoItem + '"></div>';
        }

        html += '</div>';
        //}
        html += '</div>';


        if (!backlog) {

        }
        html += '<ul class="kanban-agil-tarefa-tags">';



        if ((item.Tag1 == null) && (item.Tag2 == null) && (item.Tag3 == null)) {
            if (item.QuantChecklistTotal > 0) {
                html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate">';
                html += '<font color="#7d807e">[' + item.QuantChecklistConcluido + '/' + item.QuantChecklistTotal + ']</font>';
                html += '</li>';
            }
        }
        else {
            if (item.Tag1 != null) {
                html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate" title="' + htmlEncode(item.Tag1) + '">' + htmlEncode(item.Tag1) + '</li>';
            }
            if (item.Tag2 != null) {
                html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate" title="' + htmlEncode(item.Tag2) + '">' + htmlEncode(item.Tag2) + '</li>';
            }
            if (item.Tag3 != null) {
                html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate" title="' + htmlEncode(item.Tag3) + '">' + htmlEncode(item.Tag3) + '</li>';
            }
            if (item.QuantChecklistTotal > 0) {
                html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate">';
                html += '<font color="#7d807e">[' + item.QuantChecklistConcluido + '/' + item.QuantChecklistTotal + ']</font>';
                html += '</li>';
            }
        }
        html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate" style="background-color: #515E6E !important;">';

        var codigoItemFormatado = 'ID-' + item.CodigoItem.toString().padStart(4, '0');
        html += '<font color="#FFFFFF" style="font-weight: bold" title="' + codigoItemFormatado + '">' + codigoItemFormatado + '</font>';
        html += '</li>';

        html += '</ul>';
        html += '</div>';
        //alert(html);
        var li = document.createElement('li');
        li.setAttribute('id', item.CodigoItem);
        li.setAttribute('data-codigoitem', item.CodigoItem);
        li.setAttribute('data-codigoitemsuperior', item.CodigoItemSuperior);
        li.setAttribute('data-codigoitemespelho', item.CodigoItemEspelho);
        li.setAttribute('data-indicabloqueioitem', item.IndicaBloqueioItem);
        li.innerHTML = html;
        li.classList.add('kanban-agil-tarefa');
        if (item.IndicaItemNaoPlanejado == 'S') {
            li.classList.add('kanban-agil-tarefa-nao-planejada');
        }
        if (item.IndicaBloqueioItem == 'S') {
            li.classList.add('kanban-agil-tarefa-bloqueada');
        }

        if (sprint.IndicaUsuarioLogadoMembroEquipe) {
            li.addEventListener('mouseover', function (e) {
                var objetoEllipsis = e.currentTarget.querySelector('.kanban-agil-tarefa > .kanban-agil-tarefa-menu > i');
                if (objetoEllipsis != undefined) {
                    if (!(sprint.IniciaisStatus === 'SPRINTENCERRADA'))
                        objetoEllipsis.setAttribute('style', 'display:block');
                }

            });

            li.addEventListener('mouseout', function (e) {
                var objetoEllipsis = e.currentTarget.querySelector('.kanban-agil-tarefa > .kanban-agil-tarefa-menu > i');
                if (objetoEllipsis != undefined) {
                    objetoEllipsis.setAttribute('style', 'display:none');
                }

            });
            if (backlog) {
                li.addEventListener('dblclick', function (e) {
                    kanbanAgilEditaTarefa(this, 'kanbanAgilEditaTarefaBacklog');
                });
                var botaoAdicionar = li.querySelector('.kanban-agil-tarefa-menu-adicionar-tarefa');
                if (botaoAdicionar != null) {
                    botaoAdicionar.addEventListener('click', function (e) {
                        kanbanAgilEditaTarefa(this.parentElement.parentElement.parentElement, 'kanbanAgilIncluiTarefa');
                    });
                }
                var botaoEditar = li.querySelector('.kanban-agil-tarefa-menu-editar-item-backlog');
                if (botaoEditar != null) {
                    botaoEditar.addEventListener('click', function (e) {
                        kanbanAgilEditaTarefa(this.parentElement.parentElement.parentElement, 'kanbanAgilEditaTarefaBacklog');
                    });
                }
                var botaoExcluir = li.querySelector('.kanban-agil-tarefa-menu-excluir-item-backlog');
                if (botaoExcluir != null) {
                    botaoExcluir.addEventListener('click', function (e) {
                        if (document.fullscreen)
                            document.exitFullscreen();
                        kanbanAgilModal('kanbanAgilExcluirItemBacklog', 'O item e todas as suas tarefas serão  excluí­dos. Confirma?', this.parentElement.parentElement.parentElement.getAttribute('data-codigoitem'));
                    });
                }
            }
            else {
                li.addEventListener('dblclick', function (e) {
                    kanbanAgilEditaTarefa(this, 'kanbanAgilEditaTarefa');
                });
                var botaoEditar = li.querySelector('.kanban-agil-tarefa-menu-editar-tarefa');
                if (botaoEditar != null) {
                    botaoEditar.addEventListener('click', function (e) {
                        kanbanAgilEditaTarefa(this.parentElement.parentElement.parentElement, 'kanbanAgilEditaTarefa');
                    });
                }
                var botaoExcluir = li.querySelector('.kanban-agil-tarefa-menu-excluir-tarefa');
                if (botaoExcluir != null) {
                    botaoExcluir.addEventListener('click', function (e) {
                        if (document.fullscreen)
                            document.exitFullscreen();
                        kanbanAgilModal('kanbanAgilExcluirTarefa', 'A tarefa será excluí­da. Confirma?', this.parentElement.parentElement.parentElement.getAttribute('data-codigoitem'));
                    });
                }
                var botaoRegistrarImpedimento = li.querySelector('.kanban-agil-tarefa-menu-registra-impedimento');
                if (botaoRegistrarImpedimento != null) {
                    botaoRegistrarImpedimento.addEventListener('click', function (e) {
                        kanbanAgilEditaTarefa(this.parentElement.parentElement.parentElement, 'kanbanAgilRegistraImpedimento');
                    });
                }
            }
        }
        return (li);
    }

    function kanbanAgilRaiasArrastarSoltar(codigoItem, possuiItensFilhos) {
        // Drag and Drop do jQuery UI.        
        $('div[data-codigoitem="' + codigoItem + '"] > ul[data-codigoraia]').sortable({
            connectWith: 'div[data-codigoitem="' + codigoItem + '"] > ul[data-codigoraia]',
            placeholder: 'kanban-agil-tarefa-sombra',
            opacity: 0.7,
            disabled: ((!sprint.IndicaUsuarioLogadoMembroEquipe) || (sprint.IniciaisStatus === 'SPRINTENCERRADA')),
            beforeStop: function (event, ui) {
                /*
                var status = ui.item.get(0).parentNode.getAttribute('id');
                var id = ui.item.get(0).getAttribute('id');
                if (($(ui.item).find('[sigla="AP"]').length > 0) && (status != 'done')) {
                    $(this).sortable('cancel');
                }
                */
                document.getElementById('kanban').style.overflowY = 'initial';

                var classeDeBloqueio = ui.item.get(0).querySelector('.kanban-agil-tarefa-cabecalho-titulo-bloqueio');
                if (classeDeBloqueio !== null) {
                    $(this).sortable('cancel');
                }

            },
            start: function (event, ui) {
                document.getElementById('kanban').style.overflowY = 'clip';
            },
            stop: function (event, ui) {
                document.getElementById('kanban').style.overflowY = 'initial';
            },
            receive: function (event, ui) {

                var item = ui.item.get(0);
                var origem = ui.sender.get(0).getAttribute('data-codigoraia');
                var destino = ui.item.get(0).parentElement.getAttribute('data-codigoraia');
                // Este setTimeout() é importante pois evita que dois itens sejam movidos simultaneamente por causa de algum "eventual" problema de sincronismo entre o Sortable do jQuery UI e o Ajax que atrasará a execução final do evento "receive". Precisa ser melhor compreendido.
                if (origem) {
                    setTimeout(function () {
                        kanbanAgilAtualizaTarefa(item, origem, destino);
                    }, 500);
                }
                document.getElementById('kanban').style.overflowY = 'initial';
            },
            sort: function (event, ui) {
                $('.kanban-agil-tarefa-sombra').height(ui.item.height());
                document.getElementById('kanban').style.overflowY = 'clip';
            }
        }).disableSelection();

        var permiteArrastarItem = (sprint.IndicaUsuarioLogadoMembroEquipe) && !(possuiItensFilhos);
        $('div[data-codigoitem="' + codigoItem + '"]>ul:not([data-codigoraia])').sortable({
            connectWith: 'div[data-codigoitem="' + codigoItem + '"] > ul[data-codigoraia="' + primeiraRaia.CodigoRaia + '"]',
            placeholder: 'kanban-agil-tarefa-sombra',
            opacity: 0.7,
            disabled: !permiteArrastarItem,
            sort: function (event, ui) {
                $('.kanban-agil-tarefa-sombra').height(ui.item.height());
                document.getElementById('kanban').style.overflowY = 'clip';
            },
            stop: function (entent, ui) {

                document.getElementById('kanban').style.overflowY = 'initial';

                var that = this;
                if (that === ui.item[0].parentElement) return;

                $(that).sortable('cancel');
                $.ajax({
                    data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=' + encodeURI(codigoItem) + '&codigoRaia=' + primeiraRaia.CodigoRaia,
                    url: './agil-taskboard-service.asmx/criar-tarefa-espelho',
                    type: 'POST'
                }).done(function (data, textStatus, jqXHR) {
                    $(that).sortable('destroy');
                    var novoItem = JSON.parse(data.firstElementChild.innerHTML)[0];
                    novoItem.TituloItem = htmlDecode(novoItem.TituloItem);
                    that.parentElement.querySelector('ul:nth-child(2)').insertBefore(kanbanAgilTarefa(novoItem), that.parentElement.querySelector('ul:nth-child(2)').firstElementChild);
                    $.ajax({
                        data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=' + novoItem.CodigoItemSuperior,
                        url: './agil-taskboard-service.asmx/obter-detalhes-tarefa',
                        type: 'POST'
                    }).done(function (data, textStatus, jqXHR) {
                        var itens = JSON.parse(data.firstElementChild.innerHTML);
                        for (var i = 0; i < itens.length; i++) {
                            var novoItemBacklog = JSON.parse(data.firstElementChild.innerHTML)[0];
                            var itemBacklog = that.parentElement.querySelector('ul:first-child').querySelector('li[id="' + novoItem.CodigoItemSuperior + '"]');
                            itemBacklog.parentElement.replaceChild(kanbanAgilTarefa(novoItemBacklog, true), itemBacklog);
                            break;
                        }

                        obterRaias();

                    }).fail(function (jqXHR, textStatus, msg) {
                        mostraErroEEscondeLoading(jqXHR);
                    });
                }).fail(function (jqXHR, textStatus, msg) {
                    mostraErroEEscondeLoading(jqXHR);
                });
            }
        }).disableSelection();
    }

    function obterRaias() {

        $.ajax({
            data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto),
            url: './agil-taskboard-service.asmx/obter-raias',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {
            //console.log(data);
            var itens = JSON.parse(data.firstElementChild.innerHTML);
            for (var i = 0; i < itens.length; i++) {
                //console.log(itens[i]);
                var kanban = document.querySelector('#kanban');
                var kanbanRaia = kanban.querySelector('.kanban-agil-raias').querySelector('h2[data-codigoraia="' + itens[i].CodigoRaia + '"]');
                kanbanRaia.innerHTML = '<font style="font-size:14px">' + itens[i].NomeRaia + ' (' + itens[i].QuantidadeItensNaRaia + ')</font>' + '<span>' + itens[i].PercentualConcluido + '%</span>';
                if ((itens[i].QuantidadeMaximaItensRaia !== null) &&
                    (itens[i].QuantidadeItensNaRaia > itens[i].QuantidadeMaximaItensRaia)) {
                    kanbanRaia.innerHTML = itens[i].NomeRaia + '<font style="color:red;font-weight:bold;font-size:14px"> (' + itens[i].QuantidadeItensNaRaia + ') </font>' + '<span>' + itens[i].PercentualConcluido + '%</span>';
                }
            }
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });
    }

    function kanbanAgilAtualizaTarefa(item, origem, destino) {
        var codigoItem = item.getAttribute('id');
        $.ajax({
            data: 'codigoItem=' + encodeURI(codigoItem) + '&origem=' + encodeURI(origem) + '&destino=' + encodeURI(destino),
            url: './agil-taskboard-service.asmx/salvar-tarefa',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {
            var codigoItemBacklog = item.parentElement.parentElement.getAttribute('data-codigoitem');
            var itemBacklog = item.parentElement.parentElement.querySelector('ul:first-child').querySelector('li[id="' + codigoItemBacklog + '"]');
            var novoItem = JSON.parse(data.firstElementChild.innerHTML)[0];
            item.parentElement.replaceChild(kanbanAgilTarefa(novoItem), item);
            // Atualiza o percentual concluí­do do item de backlog correspondente
            $.ajax({
                data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=' + codigoItemBacklog,
                url: './agil-taskboard-service.asmx/obter-detalhes-tarefa',
                type: 'POST'
            }).done(function (data, textStatus, jqXHR) {
                var itens = JSON.parse(data.firstElementChild.innerHTML);
                for (var i = 0; i < itens.length; i++) {
                    var novoItemBacklog = JSON.parse(data.firstElementChild.innerHTML)[0];
                    itemBacklog.parentElement.replaceChild(kanbanAgilTarefa(novoItemBacklog, true), itemBacklog);
                    break;
                }
                obterRaias();
            }).fail(function (jqXHR, textStatus, msg) {
                mostraErroEEscondeLoading(jqXHR);
            });
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });
    }

    async function kanbanAgilEditaTarefa(item, acao) {
        if (document.fullscreen)
            await document.exitFullscreen();
        var codigoItem = item.getAttribute('data-codigoitem');
        var codigoItemSuperior = item.getAttribute('data-codigoitemsuperior');
        var indicaBloqueioItem = item.getAttribute('data-indicabloqueioitem');
        var url = '';
        var alturaTela = 480;//parseInt(document.querySelector('#alturaTela').innerHTML, 10) - 200;
        switch (acao) {
            case 'kanbanAgilEditaTarefa':
            case 'kanbanAgilEditaTarefaBacklog':
                {
                    url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/popupItensBacklog.aspx?CI=" + encodeURI(codigoItem) + "&CIS=" + encodeURI(codigoItemSuperior) + "&acao=" + encodeURI(acao) + "&IDProjeto=" + encodeURI(codigoProjeto) + "&ALT=" + (screen.height - 200).toString() + "&tela=taskboardAgil.js";
                    break;
                }
            case 'kanbanAgilAssociaTarefaBacklogNaoPlanejada':
                {
                    url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/popupAssociaItensBacklog.aspx?CP=" + encodeURI(codigoProjeto) + "&ALT=" + alturaTela;
                    break;
                }
            case 'kanbanAgilIncluiTarefaBacklogNaoPlanejada':
                {
                    url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/popupItensBacklog.aspx?CI=" + encodeURI(codigoItem) + "&CIS=" + encodeURI(codigoItemSuperior) + "&acao=" + encodeURI(acao) + "&IDProjeto=" + encodeURI(codigoProjeto) + "&ALT=" + (screen.height - 200).toString() + "&tela=taskboardAgil.js";
                    break;
                }
            case 'kanbanAgilIncluiTarefa':
                {
                    if (item.parentElement.parentElement.querySelector('ul:nth-child(2)') == null) {
                        alert('Este Kanban não possui raias. Adicione ao menos uma raia para poder adicionar tarefas.');
                        return;
                    }
                    var codigoRaia = item.parentElement.parentElement.querySelector('ul:nth-child(2)').getAttribute('data-codigoraia');
                    url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/popupItensBacklog.aspx?CI=" + encodeURI(codigoItem) + "&CIS=" + encodeURI(codigoItemSuperior) + "&acao=" + encodeURI(acao) + "&IDProjeto=" + encodeURI(codigoProjeto) + "&CR=" + encodeURI(codigoRaia) + "&ALT=" + (screen.height - 200).toString() + "&tela=taskboardAgil.js";
                    break;
                }
            case 'kanbanAgilRegistraImpedimento':
                {
                    //alert('cliclou em kanbanAgilRegistraImpedimento');//../Agil/Comentarios.aspx?ini=IB&co=' + codigoItem;
                    url = window.top.pcModal.cp_Path + "_Projetos/Agil/Comentarios.aspx?ini=IB&co=" + encodeURI(codigoItem) + "&bloqueio=" + ((indicaBloqueioItem === 'N') ? 'S' : 'N');
                    break;
                }

        }

        var larguraTela = 900;

        var retorno = function (codigoItem) {
            if ((codigoItem != null) && (codigoItem != "")) {
                $.ajax({
                    data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=' + encodeURI(codigoItem),
                    url: './agil-taskboard-service.asmx/obter-detalhes-tarefa',
                    type: 'POST'
                }).done(function (data, textStatus, jqXHR) {

                    switch (acao) {
                        case 'kanbanAgilEditaTarefa':
                        case 'kanbanAgilEditaTarefaBacklog':
                        case 'kanbanAgilRegistraImpedimento':
                            {
                                var novoItem = JSON.parse(data.firstElementChild.innerHTML)[0];
                                var indicaBacklog = acao === 'kanbanAgilEditaTarefaBacklog';
                                item.parentElement.replaceChild(kanbanAgilTarefa(novoItem, indicaBacklog), item);
                                var codigoItemAtualizar = 0;
                                if (indicaBacklog) {
                                    if (novoItem.CodigoItemEspelho) {
                                        codigoItemAtualizar = novoItem.CodigoItemEspelho;
                                    }
                                }
                                else {
                                    if ($('div[data-codigoitem="' + novoItem.CodigoItemSuperior + '"]>ul:not([data-codigoraia])>li[data-codigoitemespelho="' + codigoItem + '"').length > 0) {
                                        codigoItemAtualizar = novoItem.CodigoItemSuperior;
                                    }
                                }
                                if (codigoItemAtualizar !== 0) {
                                    $.ajax({
                                        data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=' + codigoItemAtualizar,
                                        url: './agil-taskboard-service.asmx/obter-detalhes-tarefa',
                                        type: 'POST'
                                    }).done(function (data, textStatus, jqXHR) {
                                        var novoItemAtualizar = JSON.parse(data.firstElementChild.innerHTML)[0];
                                        var itemAtualizar = document.getElementById(codigoItemAtualizar.toString());
                                        itemAtualizar.parentElement.replaceChild(kanbanAgilTarefa(novoItemAtualizar, !indicaBacklog), itemAtualizar);
                                    });
                                }
                                kanbanAgilCarregaSprint(false);
                                break;
                            }
                        case 'kanbanAgilIncluiTarefa':
                            {
                                var novoItem = JSON.parse(data.firstElementChild.innerHTML)[0];
                                item.parentElement.parentElement.querySelector('ul:nth-child(2)').insertBefore(kanbanAgilTarefa(novoItem), item.parentElement.parentElement.querySelector('ul:nth-child(2)').firstElementChild);
                                $.ajax({
                                    data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=' + novoItem.CodigoItemSuperior,
                                    url: './agil-taskboard-service.asmx/obter-detalhes-tarefa',
                                    type: 'POST'
                                }).done(function (dados, textStatus, jqXHR) {
                                    //debugger
                                    var itens = JSON.parse(dados.firstElementChild.innerHTML);
                                    if (itens.length > 0) {
                                        var concluido = '0';
                                        concluido = itens[0].PercentualConcluido.toFixed(2).toString();
                                        concluido = concluido.substr(0, concluido.length - 3);
                                        item.parentElement.parentElement.querySelector('.kanban-agil-tarefa-usuario-detalhes-concluido span').innerHTML = htmlEncode(concluido) + '%';
                                    }
                                    obterRaias();
                                }).fail(function (jqXHR, textStatus, msg) {
                                    mostraErroEEscondeLoading(jqXHR);
                                });
                                break;
                            }
                        case 'kanbanAgilAssociaTarefaBacklogNaoPlanejada':
                            {
                                kanbanAgilCarregaRaias();
                                break;
                            }
                        case 'kanbanAgilIncluiTarefaBacklogNaoPlanejada':
                            {
                                kanbanAgilCarregaRaias();
                                break;
                            }
                    }
                }).fail(function (xhr, status, error) {
                    mostraErroEEscondeLoading(xhr);
                });
            }
            else {
                if (acao === 'kanbanAgilAssociaTarefaBacklogNaoPlanejada' || acao === 'kanbanAgilIncluiTarefaBacklogNaoPlanejada') {
                    kanbanAgilCarregaRaias();
                }
            }
        };
        if (acao === 'kanbanAgilRegistraImpedimento') {
            var n = url.search("bloqueio=");
            var resposta = url.substring(n + "bloqueio=".length, (n + "bloqueio=".length) + 1);
            if (resposta === 'S') {
                window.top.showModal(url, 'Associar um comentário ao impedimento', null, null, retorno, null);
            }
            else {
                window.top.showModal(url, 'Associar um comentário ao remover um impedimento', null, null, retorno, null);
            }
        }
        else {
            window.top.showModalComFooter(url, traducao.taskboardAgil_edi__o, null, null, retorno, null);
        }

    }

    /* HTML Encode versão jQuery (Segurança contra ataque XSS) */
    function htmlEncode(value) {
        return $('<div/>').text(value).html();
    }

    function htmlDecode(value) {
        return $("<textarea/>").html(value).text();
    }

    /* Versão simplificada da sprintf() usada para substituir variáveis em uma string utilizando chaves: {0}, {1}, ... */
    function sprintf() {
        var text = arguments[0];
        for (var i = 1; i < arguments.length; i++) {
            text = text.replace('{' + (i - 1).toString() + '}', arguments[i]);
        }
        return (text);
    }

    function kanbanAgilModal(acao, mensagem, valor) {

        var funcObj = null;
        if (acao == 'kanbanAgilExcluirItemBacklog') {
            funcObj = {
                funcaoClickOK: function (s, e) {
                    var codigoItem = valor;
                    $.ajax({
                        data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=' + encodeURI(codigoItem),
                        url: './agil-taskboard-service.asmx/excluir-item-backlog',
                        type: 'POST'
                    }).done(function (data, textStatus, jqXHR) {
                        window.top.mostraMensagem('O item e todas as suas tarefas foram excluí­dos com sucesso.', 'sucesso', false, false, null, 3000);
                        kanbanAgilCarregaRaias();
                    }).fail(function (xhr, status, error) {
                        window.top.mostraMensagem('Ocorreu um erro e o item de backog não pôde ser excluí­do.', 'erro', true, false, null, 3000);
                    });
                }
            }
        }
        else if (acao == 'kanbanAgilExcluirTarefa') {
            funcObj = {
                funcaoClickOK: function (s, e) {
                    var codigoItem = valor;
                    $.ajax({
                        data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto) + '&codigoItem=' + encodeURI(codigoItem),
                        url: './agil-taskboard-service.asmx/excluir-item-backlog',
                        type: 'POST'
                    }).done(function (data, textStatus, jqXHR) {
                        window.top.mostraMensagem('A tarefa foi excluí­da com sucesso.', 'sucesso', false, false, null, 3000);
                        kanbanAgilCarregaRaias();

                    }).fail(function (xhr, status, error) {
                        window.top.mostraMensagem('Ocorre um erro e a tarefa não pôde ser excluí­da.', 'erro', true, false, null, 3000);
                    });
                }
            }
        }
        window.top.mostraConfirmacao(mensagem, function () { funcObj['funcaoClickOK']() }, null);
    }

    /*
     * ########################
     * ### Iní­cio da página ###
     * ########################
    */
    // Carrega as data de inÃ­cio e fim da Sprint do Kanban.
    kanbanAgilCarregaSprint();

    $.ajax({
        url: './agil-taskboard-service.asmx/obter-status-itens',
        type: 'POST'
    }).done(function (data, textStatus, jqXHR) {
        var statuses = JSON.parse(data.firstElementChild.innerHTML);
        var comboFiltro = document.getElementById('comboFiltrarStatus');

        comboFiltro.innerHTML = "<option value='-1' selected>Todos</option>";

        for (var i = 0; i < statuses.length; i++) {
            var opcao = document.createElement('option');
            opcao.setAttribute('value', statuses[i].CodigoTipoStatusItem);
            opcao.innerHTML = statuses[i].TituloStatusItem;
            comboFiltro.appendChild(opcao);
        }
        comboFiltro.addEventListener('change', function (e) { codigoStatusItemSelecionado = this.value; kanbanAgilCarregaRaias(); });
    }).fail(function (jqXHR, textStatus, msg) {
        mostraErroEEscondeLoading(jqXHR);
    });

    canvasFiltraKanbanPorBugs.addEventListener('click', function (e) {
        if (clicouNoFiltroPorBug === true) {
            clicouNoFiltroPorBug = false;
            image_canvasFiltraKanbanPorBugs = original_canvasFiltraKanbanPorBugs.clone();
            Marvin.grayScale(original_canvasFiltraKanbanPorBugs, image_canvasFiltraKanbanPorBugs);
            image_canvasFiltraKanbanPorBugs.draw(this);
            this.setAttribute('title', 'Filtrar Problemas');
        }
        else {
            clicouNoFiltroPorBug = true;
            image_canvasFiltraKanbanPorBugs = original_canvasFiltraKanbanPorBugs.clone();
            image_canvasFiltraKanbanPorBugs.draw(this);
            this.setAttribute('title', 'Remover filtro de problemas');
        }
        kanbanAgilCarregaRaias();
    });

    canvasFiltraKanbanPorImpedimentos.addEventListener('click', function (e) {
        if (clicouNoFiltroPorImpedimentos === true) {
            clicouNoFiltroPorImpedimentos = false;
            image_canvasFiltraKanbanPorImpedimentos = original_canvasFiltraKanbanPorImpedimentos.clone();
            Marvin.grayScale(original_canvasFiltraKanbanPorImpedimentos, image_canvasFiltraKanbanPorImpedimentos);
            image_canvasFiltraKanbanPorImpedimentos.draw(this);
            this.setAttribute('title', 'Filtrar Impedimentos');
        }
        else {
            clicouNoFiltroPorImpedimentos = true;
            image_canvasFiltraKanbanPorImpedimentos = original_canvasFiltraKanbanPorImpedimentos.clone();
            image_canvasFiltraKanbanPorImpedimentos.draw(this);
            this.setAttribute('title', 'Remover filtro de impedimentos');
        }
        kanbanAgilCarregaRaias();
    });


    canvasFiltraKanbanPorUsuarioLogado.addEventListener('click', function (e) {
        if (clicouNoFiltroPorUsuarioLogado === true) {
            clicouNoFiltroPorUsuarioLogado = false;
            image_canvasFiltraKanbanPorUsuarioLogado = original_canvasFiltraKanbanPorUsuarioLogado.clone();
            Marvin.grayScale(original_canvasFiltraKanbanPorUsuarioLogado, image_canvasFiltraKanbanPorUsuarioLogado);
            image_canvasFiltraKanbanPorUsuarioLogado.draw(this);
            //this.setAttribute('title', 'Filtrar Por Usuario Logado');
        }
        else {
            clicouNoFiltroPorUsuarioLogado = true;
            image_canvasFiltraKanbanPorUsuarioLogado = original_canvasFiltraKanbanPorUsuarioLogado.clone();
            image_canvasFiltraKanbanPorUsuarioLogado.draw(this);
            //this.setAttribute('title', 'Remover filtro Por Usuario Logado');
        }
        kanbanAgilCarregaRaias();
    });

});

function mostraErroEEscondeLoading(jqXHR) {
    window.top.lpAguardeMasterPage.Hide();
    var mensagemFinal = 'Falha ao obter os dados. Mensagem original: ' + jqXHR.responseText;
    window.top.mostraMensagem(mensagemFinal, 'atencao', true, false, null);
}