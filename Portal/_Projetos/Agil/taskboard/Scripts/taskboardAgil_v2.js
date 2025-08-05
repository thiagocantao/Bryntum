$(document).ready(function () {
    // traducao
    traducao.kanban_Meses = JSON.parse(traducao.kanban_Meses);

    var baseUrl = document.querySelector('#baseUrl').innerHTML;
    var codigoProjeto = document.querySelector('#codigoProjeto').innerHTML;
    var dashboardID = document.querySelector('#dashboardID').innerHTML;
    var delayPesquisaTexto = 1000;
    var mostrarSprintsEncerradas = false;
    var ordenacao = (document.getElementById('rbOrdemSprints_ValueInput') !== null) ? document.getElementById('rbOrdemSprints_ValueInput').value : 'R';
    var pesquisaTexto = '';
    var timerPesquisaTexto;

    ['input', 'propertychange'].map(function (evento) {
        document.querySelector('#pesquisaTexto').addEventListener(evento, function (e) {
            clearTimeout(timerPesquisaTexto);
            timerPesquisaTexto = setTimeout(function () { kanbanPesquisaTexto(); }, delayPesquisaTexto);
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


    $("#rbOrdemSprints").click(function () {
        $(this).trigger('classChanged');
    });
    $('#rbOrdemSprints').on('classChanged', function () {
        ordenacao = document.querySelector('#rbOrdemSprints_ValueInput').value;
        kanbanAgilCarregaSprintsDoProjeto();
    });

    $("#checkMostrarSprintsEncerradas").click(function () {
        $(this).trigger('classChanged');
    });
    $('#checkMostrarSprintsEncerradas').on('classChanged', function () {
        mostrarSprintsEncerradas = $('#checkMostrarSprintsEncerradas_S_D').hasClass('dxWeb_edtCheckBoxChecked_MaterialCompact');
        kanbanAgilCarregaSprintsDoProjeto();
    });

    function excluiItemBacklog(codigoItem, codigoIteracao) {
        $.ajax({
            data: 'codigoItem=' + encodeURI(codigoItem) + '&codigoIteracao=' + encodeURI(codigoIteracao),
            url: './agil-taskboard-service-v2.asmx/excluir-item-backlog-da-iteracao',
            type: 'POST',
            beforeSend: function () { lpLoading.Show() }
        }).done(function (data, textStatus, jqXHR) {
            var divBacklog = document.querySelector('div[data-codigoiteracao="0"] > div:not(:first-child) > div > div > ul');
            var itemARemover = divBacklog.querySelector('li[data-codigoitem="' + codigoItem + '"]');
            divBacklog.removeChild(itemARemover);
            lpLoading.Hide();
            
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });

    }

    function excluiSprint(codigoProjetoIteracao, codigoIteracao) {
        $.ajax({
            data: 'codigoProjeto=' + encodeURI(codigoProjetoIteracao) + '&codigoIteracao=' + encodeURI(codigoIteracao) + '&codigoProjetoPai=' + encodeURI(codigoProjeto),
            url: './agil-taskboard-service-v2.asmx/excluir-sprint',
            type: 'POST',
            beforeSend: function () { lpLoading.Show() }
        }).done(function (data, textStatus, jqXHR) {
            var mensagemErro = JSON.parse(data.firstElementChild.innerHTML);
            if (mensagemErro !== '') {
                window.top.mostraMensagem(mensagemErro, 'atencao', true, false, null);
            }
            else {
                kanbanAgilCarregaSprintsDoProjeto();
            }
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });

    }

    function htmlDecode(value) {
        return $("<textarea/>").html(value).text();
    }

    /* HTML Encode versão jQuery (Segurança contra ataque XSS) */
    function htmlEncode(value) {
        return $('<div/>').text(value).html();
    }

    function insereBotaoFloatAddSprintNoKanban() {

        var aTAG = document.createElement('a');

        aTAG.setAttribute('href', '#');
        aTAG.classList.add('float');
        aTAG.addEventListener("click", function (e) {
            var sUrl = '../DadosProjeto/popupSprint.aspx?CPA=' + codigoProjeto + '&CI=-1&RO=N';
            if (document.fullscreen == true) {
                document.exitFullscreen();
            }

            setTimeout(function () {
                window.top.showModalComFooter(sUrl, 'Nova Sprint', null, null, kanbanAgilCarregaSprintsDoProjeto, null);
            }, 1000);


        });

        var iTAG = document.createElement('i');
        iTAG.classList.add('fa');
        iTAG.classList.add('fa-plus');
        iTAG.classList.add('my-float');


        aTAG.appendChild(iTAG);
        var divBotaoAddSprint = document.querySelector('#divBotaoAddSprint');
        divBotaoAddSprint.setAttribute('title', 'Incluir Sprint');
        divBotaoAddSprint.appendChild(aTAG);


    }
    function insereBotaoFloatAjuda() {

        var divBotaoAjuda = document.createElement('div');
        divBotaoAjuda.setAttribute('id', 'divBotaoAjuda');
        var aTAG = document.createElement('a');

        aTAG.setAttribute('href', '#');
        aTAG.classList.add('float');
        aTAG.setAttribute('title', 'Quer adicionar item no Sprint?');
        aTAG.setAttribute('style', 'cursor:pointer;bottom:100px');

        aTAG.addEventListener("click", function (e) {
            var sUrl = window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/popupAdicionaItemSprint.aspx';
            if (document.fullscreen == true) {
                document.exitFullscreen();
            }
            setTimeout(function () {
                window.top.showModal(sUrl, 'Quer adicionar item no Sprint?', 450, null, null, null);
            }, 1000);

        });

        var iTAG = document.createElement('i');
        iTAG.classList.add('fa');
        iTAG.classList.add('fa-question');
        iTAG.classList.add('fa-inverse')
        iTAG.classList.add('my-float');

        aTAG.appendChild(iTAG);
        var divAjuda = document.querySelector('#divBotaoAjuda');
        divAjuda.setAttribute('title', 'Quer adicionar item no Sprint?');
        divAjuda.appendChild(aTAG);
    }

    function kanbanAgilArrastaESoltaItemBacklog(codigoItemMovimentado, codigoIteracaoOrigem, codigoIteracaoDestino) {
        //alert('codigoItemMovimentado=' + codigoItemMovimentado + ', codigoIteracaoOrigem=' + codigoIteracaoOrigem + ', codigoIteracaoDestino=' + codigoIteracaoDestino);
        $.ajax({
            data: 'codigoItemMovimentado=' + encodeURI(codigoItemMovimentado) + '&codigoIteracaoOrigem=' + encodeURI(codigoIteracaoOrigem) + '&codigoIteracaoDestino=' + encodeURI(codigoIteracaoDestino),
            url: './agil-taskboard-service-v2.asmx/salvar-item-de-backlog-na-iteracao',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {
            //int codigoProjeto, int codigoIteracao
            $.ajax({
                data: 'codigoItem=' + encodeURI(codigoItemMovimentado),
                url: './agil-taskboard-service-v2.asmx/obter-detalhes-item',
                type: 'POST'
            }).done(function (data, textStatus, jqXHR) {
                var jsonItens = JSON.parse(data.firstElementChild.innerHTML);
                var ulAAtualizar = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoDestino + '"]  > div:not(:first-child) > div > div > ul');
                liASubstituir = ulAAtualizar.querySelector('li[data-codigoitem="' + codigoItemMovimentado + '"]');

                var tagStatus = liASubstituir.querySelector('.kanban-agil-tarefa-tag-status');
                tagStatus.innerHTML = jsonItens[0].DescricaoTipoStatusItem;

                var estiloItem = '';
                if (jsonItens[0].PercentualConcluido === 100) {
                    estiloItem = 'background-color: #87cefa';//azul claro
                }
                else {
                    estiloItem = 'background-color:#b8e4b2';//verde claro
                }
                tagStatus.setAttribute('style', estiloItem);

                atualizaQuantidadeDeItensCabecalho(codigoIteracaoOrigem, codigoIteracaoDestino);
                atualizaPercentualAlocacaoSprintCabecalho(codigoIteracaoOrigem, codigoIteracaoDestino);
                atualizarMenuContextoSprint(codigoIteracaoOrigem, codigoIteracaoDestino);
            }).fail(function (jqXHR, textStatus, msg) {
                mostraErroEEscondeLoading(jqXHR);
            });
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });
    }

    function atualizarMenuContextoSprint(codigoIteracaoOrigem, codigoIteracaoDestino) {
        var selecionaColunaOrigem = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoOrigem + '"]');
        var selecionaColunaDestino = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoDestino + '"]');

        var ulList_origem = document.querySelectorAll('div[data-codigoiteracao="' + codigoIteracaoOrigem + '"] > div:not(:first-child) > div > div >  ul > li');

        var linhasDaTabelaOrigem = selecionaColunaOrigem.querySelectorAll('.dropdown-menu > table > tr > td > a');
        var linhasDaTabelaDestino = selecionaColunaDestino.querySelectorAll('.dropdown-menu > table > tr > td > a');
        if (ulList_origem.length == 0) {
            for (var i = 0; i < linhasDaTabelaOrigem.length; i++) {
                if (linhasDaTabelaOrigem[i].getAttribute("title") == 'Excluir Sprint') {
                    linhasDaTabelaOrigem[i].parentElement.parentElement.style.visibility = 'visible';
                }
            }
        }
        for (var i = 0; i < linhasDaTabelaDestino.length; i++) {
            if (linhasDaTabelaDestino[i].getAttribute("title") == 'Excluir Sprint') {
                linhasDaTabelaDestino[i].parentElement.parentElement.style.visibility = 'collapse';
            }
        }
    }

    function atualizaQuantidadeDeItensCabecalho(codigoIteracaoOrigem, codigoIteracaoDestino) {
        var ulList_origem = document.querySelectorAll('div[data-codigoiteracao="' + codigoIteracaoOrigem + '"] > div:not(:first-child) > div > div >  ul > li');
        //console.log(ulList_origem);

        var ulList_destino = document.querySelectorAll('div[data-codigoiteracao="' + codigoIteracaoDestino + '"] > div:not(:first-child) > div > div >  ul > li');
        //console.log(ulList_destino);


        var blocoQuantidadeItensOrigem = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoOrigem + '"]  >  div > div:not(:first-child)');
        if (codigoIteracaoOrigem == 0) {
            blocoQuantidadeItensOrigem = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoOrigem + '"]  >  div ');
            blocoQuantidadeItensOrigem = blocoQuantidadeItensOrigem.querySelector('.quantidade-itens-backlog');
        }
        if (ulList_origem.length == 0) {
            blocoQuantidadeItensOrigem.innerHTML = 'Nenhum item';
        }
        else if (ulList_origem.lenght == 1) {
            blocoQuantidadeItensOrigem.innerHTML = 'um item';
        }
        else {
            blocoQuantidadeItensOrigem.innerHTML = ulList_origem.length + ' itens';
        }


        var blocoQuantidadeItensDestino = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoDestino + '"]  >  div > div:not(:first-child)');
        if (codigoIteracaoDestino == 0) {
            blocoQuantidadeItensDestino = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoDestino + '"]  >  div ');
            blocoQuantidadeItensDestino = blocoQuantidadeItensDestino.querySelector('.quantidade-itens-backlog');
        }


        if (ulList_destino.length == 0) {
            blocoQuantidadeItensDestino.innerHTML = 'Nenhum item';
        }
        else if (ulList_destino.lenght == 1) {
            blocoQuantidadeItensDestino.innerHTML = 'um item';
        }
        else {
            blocoQuantidadeItensDestino.innerHTML = ulList_destino.length + ' itens';
        }
    }

    function atualizaPercentualAlocacaoSprintCabecalho(codigoIteracaoOrigem, codigoIteracaoDestino) {
        $.ajax({
            data: 'codigoIteracao=' + encodeURI(codigoIteracaoOrigem),
            url: './agil-taskboard-service-v2.asmx/obter-percentual-alocacao-sprint',
            type: 'POST'
        }).done(function (data2, textStatus, jqXHR) {
            
            var jsonItens = JSON.parse(data2.firstElementChild.innerHTML)[0];
            //console.log(jsonItens);
            var divQuantidadeItens = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoOrigem + '"] > div');
            divQuantidadeItens = divQuantidadeItens.querySelector('#spanPercentualOcupacaoSprint');
            //console.log(divQuantidadeItens);
            divQuantidadeItens.innerHTML = jsonItens.alocacao;
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });

        $.ajax({
            data: 'codigoIteracao=' + encodeURI(codigoIteracaoDestino),
            url: './agil-taskboard-service-v2.asmx/obter-percentual-alocacao-sprint',
            type: 'POST'
        }).done(function (data2, textStatus, jqXHR) {
            
            var jsonItens = JSON.parse(data2.firstElementChild.innerHTML)[0];
            var divQuantidadeItens = document.querySelector('div[data-codigoiteracao="' + codigoIteracaoDestino + '"] > div');
            divQuantidadeItens = divQuantidadeItens.querySelector('#spanPercentualOcupacaoSprint');
            //console.log(divQuantidadeItens);
            divQuantidadeItens.innerHTML = jsonItens.alocacao;
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });
    }


    function kanbanAgilCarregaSprintsDoProjeto() {
        //public string ObterSprints(int codigoProjeto, string ordenacao, bool mostrarSprintsEncerradas)
        document.querySelector('#kanban').innerHTML = "";
        //alert('codigoProjeto=' + encodeURI(codigoProjeto) + '&ordenacao=' + ordenacao);
        $.ajax({
            data: 'codigoProjeto=' + encodeURI(codigoProjeto) + '&ordenacao=' + ordenacao + '&mostrarSprintsEncerradas=' + mostrarSprintsEncerradas,
            url: './agil-taskboard-service-v2.asmx/obter-sprints',
            type: 'POST',
            beforeSend: function () { lpLoading.Show() }
        }).done(function (data, textStatus, jqXHR) {

            
            document.querySelector('#kanban').innerHTML = '';

            var title = 'Backlog';
            var sprints = JSON.parse(data.firstElementChild.innerHTML);
            var quantidadeItensSptrint = 0;

            var divAriaLabelled = document.createElement('div');
            divAriaLabelled.setAttribute('aria-labelledby', 'kanbanAgilMenuBacklog');
            divAriaLabelled.classList.add('dropdown-menu');

            var iPLUS = document.createElement('i');
            iPLUS.classList.add('fas');
            iPLUS.classList.add('fa-plus');
            iPLUS.setAttribute('id', 'iplus1');
            iPLUS.setAttribute('style', 'cursor:pointer');

            var iIMPORT = document.createElement('i');
            iIMPORT.classList.add('fas');
            iIMPORT.classList.add('fa-edit');
            iIMPORT.setAttribute('id', 'iimportar');
            iIMPORT.setAttribute('style', 'cursor:pointer');

            var aDropDownItemIncluiBacklog = document.createElement('a');
            aDropDownItemIncluiBacklog.classList.add('dropdown-item');
            aDropDownItemIncluiBacklog.setAttribute('href', 'javascript:void(0);');
            aDropDownItemIncluiBacklog.setAttribute('title', 'Incluir Backlog');
            aDropDownItemIncluiBacklog.innerHTML = 'Incluir Backlog';
            aDropDownItemIncluiBacklog.addEventListener("click", function (e) {
                var sUrl = "../DadosProjeto/popupItensBacklog.aspx?CI=0&CIS=0&IDProjeto=" + codigoProjeto + '&acao=kanbanAgilIncluiTarefaBacklogNaoPlanejada';
                if (document.fullscreen == true) {
                    document.exitFullscreen();
                }
                setTimeout(function () {
                    var retorno = function (codigoItem) {
                        if ((codigoItem != null) && (codigoItem != "")) {
                            $.ajax({
                                data: 'codigoItem=' + encodeURI(codigoItem),
                                url: './agil-taskboard-service-v2.asmx/obter-detalhes-item',
                                type: 'POST',
                                beforeSend: function () { lpLoading.Show() }
                            }).done(function (data, textStatus, jqXHR) {
                                
                                var item = JSON.parse(data.firstElementChild.innerHTML)[0];
                                var divBacklog = document.querySelector('div[data-codigoiteracao="0"] > div:not(:first-child) > div > div > ul');
                                divBacklog.appendChild(kanbanAgilItemBacklog(item));
                                lpLoading.Hide();
                            }).fail(function (xhr, status, error) {
                                mostraErroEEscondeLoading(xhr);
                            });
                        }
                    };
                    window.top.showModalComFooter(sUrl, 'Backlog', null, null, retorno, null);
                }, 1000);
            });

            var aDropDownItemImportar = document.createElement('a');
            aDropDownItemImportar.classList.add('dropdown-item');
            aDropDownItemImportar.setAttribute('href', 'javascript:void(0);');
            aDropDownItemImportar.setAttribute('title', 'Importar Itens do Cronograma');
            aDropDownItemImportar.innerHTML = 'Importar';
            aDropDownItemImportar.addEventListener("click", function (e) {
                var sUrl = "../Agil/ImportarTarefasParaItemBacklog.aspx?IDProjeto=" + codigoProjeto;
                if (document.fullscreen == true) {
                    document.exitFullscreen();
                }
                setTimeout(function () {
                    window.top.showModal(sUrl, 'Importação do Cronograma', null, null, kanbanAgilCarregaSprintsDoProjeto, null);
                }, 1000);
            });

            divAriaLabelled.appendChild(aDropDownItemIncluiBacklog);
            divAriaLabelled.appendChild(aDropDownItemImportar);

            var divMais = document.createElement('div');
            divMais.setAttribute('id', 'kanbanAgilIncluiTarefaBacklogNaoPlanejadaMenu');

            var divKanbanAgilTarefaMenu = document.createElement('div');
            divKanbanAgilTarefaMenu.classList.add('kanban-agil-tarefa-menu-backlog');
            //divKanbanAgilTarefaMenu.classList.toggle('dropdown');
            divKanbanAgilTarefaMenu.setAttribute('id', 'kanbanAgilMenuBacklog');
            divKanbanAgilTarefaMenu.setAttribute('data-toggle', 'dropdown');


            var iBARS = document.createElement('i');
            iBARS.classList.add('fas');
            iBARS.classList.add('fa-plus');

            divKanbanAgilTarefaMenu.appendChild(iBARS);
            divMais.appendChild(divKanbanAgilTarefaMenu);
            divMais.appendChild(divAriaLabelled);


            //<img src="images/btn-incluir-rapido-item-backlog.png" />

            var imagemIncluiRapido = document.createElement('img');
            imagemIncluiRapido.setAttribute('src', 'images/btn-incluir-rapido-item-backlog.png');
            imagemIncluiRapido.setAttribute('title', 'Inclusão Rápida de Backlog');
            imagemIncluiRapido.setAttribute('style', 'cursor:pointer');
            imagemIncluiRapido.setAttribute('id', 'imagemIncluiRapido');

            var divEsquerda = document.createElement('div');
            divEsquerda.classList.add('div-do-campo-de-inclusao-em-massa');
            divEsquerda.innerHTML = '<input class="form-control" id="campoincluirapidobacklog" autocomplete="off" maxlength="200" placeholder="Digite aqui para incluir novo item" style="border-top-left-radius: 3;border-bottom-left-radius: 3;font-size: 0.78em; padding-left: 5px;padding-right: 5px" type="text">';

            var divDireita = document.createElement('div');
            divDireita.setAttribute('style', 'flex-grow:1;text-align:right;padding-right:5px;');


            $.ajax({
                data: 'codigoProjeto=' + encodeURI(codigoProjeto),
                url: './agil-taskboard-service-v2.asmx/obter-permissao-manter-sprints',
                type: 'POST',
                beforeSend: function () { lpLoading.Show() }
            }).done(function (data, textStatus, jqXHR) {
                
                var item = JSON.parse(data.firstElementChild.innerHTML)[0];
                var campo = document.querySelector('#campoincluirapidobacklog');
                if (item.Permissao === true) {
                    divDireita.appendChild(divMais);

                    campo.setAttribute('style', 'display:block');
                }
                else {
                    campo.setAttribute('style', 'display:none');
                }
                lpLoading.Hide();
            }).fail(function (jqXHR, textStatus, msg) {
                mostraErroEEscondeLoading(jqXHR);
            });

            var divQuantidadeItens = document.createElement('div');
            //divQuantidadeItens.innerHTML = '12*itens';
            divQuantidadeItens.classList.add('quantidade-itens-backlog');
            divQuantidadeItens.setAttribute('id', 'divQuantidadeItensBacklog');
            divQuantidadeItens.setAttribute('style', 'height:20px');
            var divContainer = document.createElement('div');
            divContainer.classList.add('cabecalho-raia-backlog');
            divContainer.appendChild(divEsquerda);
            //divContainer.appendChild(imagemIncluiRapido);
            divContainer.appendChild(divDireita);
            divContainer.appendChild(divQuantidadeItens);

            var div2 = document.createElement('div');
            div2.setAttribute('data-codigoprojetoiteracao', codigoProjeto);
            div2.setAttribute('data-codigoiteracao', '0');

            div2.appendChild(divContainer);
            div2.setAttribute('title', title);

            html = '';

            var div = document.createElement('div');
            div.classList.add('kanban-agil-raias-sprint');
            div.appendChild(div2);

            document.querySelector('#kanban').appendChild(div);
            document.querySelector('#campoincluirapidobacklog').addEventListener('keypress', function (e) {
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
                        url: './agil-taskboard-service-v2.asmx/incluir-backlog-rapidamente',
                        type: 'POST'
                    }).done(function (data2, textStatus, jqXHR) {
                        
                        var item = JSON.parse(data2.firstElementChild.innerHTML)[0];
                        var divBacklog = document.querySelector('div[data-codigoiteracao="0"] > div:not(:first-child) > div > div > ul');
                        divBacklog.appendChild(kanbanAgilItemBacklog(item));
                        document.querySelector('#campoincluirapidobacklog').value = '';
                    }).fail(function (jqXHR, textStatus, msg) {
                        mostraErroEEscondeLoading(jqXHR);
                    });
                }
            });
            for (var i = 0; i < sprints.length; i++) {

                var sprint = sprints[i];
                var div3 = document.createElement('div');
                div3.setAttribute('data-codigoprojetoiteracao', sprint.CodigoProjetoIteracao);
                div3.setAttribute('data-codigoiteracao', sprint.CodigoIteracao);
                div3.setAttribute('data-indicasprintencerrada', (sprint.IniciaisStatus === 'SPRINTENCERRADA') ? 'S' : 'N');
                div3.setAttribute('data-indexsprint', sprint.seq);
                div3.setAttribute('data-nomesprint', sprint.TituloSprint);

                //Permissão para a sprint
                div3.setAttribute('data-permiteincluiritemsprint', sprint.PermissaoIncluirItensSprint);
                div3.setAttribute('data-permiteexcluiritemsprint', sprint.PermissaoExcluirItensSprint);

                var link1 = baseUrl + "/_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + sprint.CodigoProjetoIteracao + "&NomeProjeto=" + sprint.TituloSprint;
                var aTAG = document.createElement('a');
                aTAG.setAttribute("href", link1);
                aTAG.setAttribute("target", "_self");
                aTAG.setAttribute('id', 'aTAG_link_' + sprint.CodigoProjetoIteracao);
                aTAG.setAttribute('style', 'color: #ffffff');

                aTAG.innerHTML = sprint.TituloSprint;
                aTAG.addEventListener("click", function (e) {
                    var atag = document.getElementById(e.target.id);
                    var href = atag.getAttribute('href');
                    window.open(href, '_parent');
                });
                aTAG.addEventListener('mouseover', function (e) {
                    e.currentTarget.setAttribute('style', 'color:#0000FF');
                });

                aTAG.addEventListener('mouseout', function (e) {
                    e.currentTarget.setAttribute('style', 'color:#ffffff');
                });

                var fonte = document.createElement('div');
                fonte.classList.add('sprint-titulo');
                fonte.classList.add("text-nowrap");
                fonte.classList.add("text-truncate");
                fonte.setAttribute('title', sprint.TituloSprint);
                fonte.appendChild(aTAG);

                var divContainer1 = document.createElement('div');
                divContainer1.classList.add('cabecalho-raia-sprint');
                if (sprint.IniciaisStatus === 'SPRINTENCERRADA') {
                    divContainer1.classList.add('sprint-encerrada');
                }

                var divContainerSuperiorDireita = document.createElement('div');
                divContainerSuperiorDireita.setAttribute('style', 'display:flex;flex-direction:row-reverse;cursor:pointer');

                var iconeFileChart = document.createElement('i');
                iconeFileChart.classList.add('fas');
                iconeFileChart.classList.add('fa-chart-area');
                iconeFileChart.setAttribute('style', 'cursor:pointer');
                iconeFileChart.title = 'Números da Sprint';
                iconeFileChart.setAttribute('id', 'id_iconeFileChart_' + sprint.CodigoProjetoIteracao);
                iconeFileChart.setAttribute('data-codigoprojetoiteracao', sprint.CodigoProjetoIteracao);
                iconeFileChart.setAttribute('data-titulosprint', sprint.TituloSprint);

                iconeFileChart.addEventListener('click', function (e) {

                    var itag = document.getElementById(e.target.id);
                    var codigoProjetoIteracao = itag.getAttribute('data-codigoprojetoiteracao');
                    var sUrl = window.top.pcModal.cp_Path + '_Dashboard/VisualizadorDashboard.aspx?id=' + dashboardID + '&CodProjeto=' + codigoProjetoIteracao;
                    var sHeaderTitulo = itag.getAttribute('data-titulosprint');
                    var sWidth = null;
                    var sHeight = null;
                    var sFuncaoPosModal = kanbanAgilCarregaSprintsDoProjeto;
                    var objParam = null;
                    if (document.fullscreen == true) {
                        document.exitFullscreen();
                    }
                    setTimeout(function () {
                        window.top.showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam);
                    }, 1000);


                });

                var iconeUsers = document.createElement('a');
                iconeUsers.innerHTML = 'Editar Equipe';
                iconeUsers.title = 'Editar Equipe';
                iconeUsers.setAttribute('data-codigoprojetoiteracao', sprint.CodigoProjetoIteracao);
                iconeUsers.setAttribute('id', 'id_iconeUsers_' + sprint.CodigoProjetoIteracao);
                iconeUsers.setAttribute('style', 'padding-right:5px;flex-grow:1;cursor:pointer');
                iconeUsers.addEventListener('click', function (e) {

                    var iutag = document.getElementById(e.target.id);
                    iutag.getAttribute('data-codigoprojetoiteracao');

                    var codigoProjetoIteracao = iutag.getAttribute('data-codigoprojetoiteracao');
                    var sUrl = window.top.pcModal.cp_Path + '_Projetos/Agil/EquipeProjetoAgil.aspx?cp=' + codigoProjetoIteracao;
                    var sHeaderTitulo = 'Editar Equipe';
                    var sWidth = null;
                    var sHeight = null;
                    var sFuncaoPosModal = kanbanAgilCarregaSprintsDoProjeto;
                    var objParam = null;
                    if (document.fullscreen == true) {
                        document.exitFullscreen();
                    }

                    setTimeout(function () {
                        window.top.showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam);
                    }, 1000);
                });

                var divLinhaSuperior = document.createElement('div');
                divLinhaSuperior.classList.add('sprint-titulo-icone-dash-tres-pontos');

                var divLinhaDoMeio = document.createElement('div');
                divLinhaDoMeio.classList.add('sprint-titulo-quantidade-itens');
                divLinhaDoMeio.innerHTML = sprint.QuantidadeItens;
                divLinhaDoMeio.setAttribute('id', 'divLinhaDoMeio');

                var divLinhaInferior = document.createElement('div');
                var iTAG = document.createElement('i');
                iTAG.classList.add('fa');
                iTAG.classList.add('fa-calendar');
                iTAG.setAttribute('aria-hidden', 'true');
                iTAG.setAttribute('style', 'flex-grow:1');

                var spanInicioTerminoSprt = document.createElement('span');
                spanInicioTerminoSprt.innerHTML = sprint.Inicio + ' a ' + sprint.Termino;
                spanInicioTerminoSprt.setAttribute('style', 'flex-grow:15');


                var divContainer_spanPercentualOcupacaoSprint = document.createElement('span');
                divContainer_spanPercentualOcupacaoSprint.innerHTML = sprint.OcupacaoSprint;
                divContainer_spanPercentualOcupacaoSprint.setAttribute('style', 'padding-right:3px');
                divContainer_spanPercentualOcupacaoSprint.setAttribute('id', 'spanPercentualOcupacaoSprint');

                var divContainer_spanInicioTerminoSprt = document.createElement('div');
                divContainer_spanInicioTerminoSprt.appendChild(iTAG);
                divContainer_spanInicioTerminoSprt.appendChild(spanInicioTerminoSprt);
                divContainer_spanInicioTerminoSprt.appendChild(divContainer_spanPercentualOcupacaoSprint);
                divContainer_spanInicioTerminoSprt.classList.add('sprint-titulo-inicio-termino-ocupacao');

                divLinhaInferior.appendChild(divContainer_spanInicioTerminoSprt);

                var iEditTAG = document.createElement('a');

                iEditTAG.innerHTML = 'Alterar Sprint';
                iEditTAG.setAttribute('data-codigoprojetoiteracao', sprint.CodigoProjetoIteracao);
                iEditTAG.setAttribute('data-codigoiteracao', sprint.CodigoIteracao);

                iEditTAG.setAttribute('id', 'id_iEditTAG_' + sprint.CodigoIteracao);
                iEditTAG.setAttribute('title', 'Alterar Sprint');
                iEditTAG.setAttribute('style', 'cursor:pointer');

                iEditTAG.addEventListener("click", function (e) {
                    var iEditTag1 = document.getElementById(e.target.id);
                    var codigoProjetoIteracao = iEditTag1.getAttribute('data-codigoprojetoiteracao');
                    var sUrl = '../DadosProjeto/popupSprint.aspx?CPA=' + codigoProjeto + '&CPI=' + codigoProjetoIteracao + '&RO=N';
                    if (document.fullscreen == true) {
                        document.exitFullscreen();
                    }
                    setTimeout(function () {
                        window.top.showModalComFooter(sUrl, 'Alterar Sprint', null, null, kanbanAgilCarregaSprintsDoProjeto, null);
                    }, 1000);

                });

                var divContainer_iEditTAG = document.createElement('div');
                divContainer_iEditTAG.classList.add('sprint-titulo-icone-dashboard-numeros-sprint');
                divContainer_iEditTAG.appendChild(iconeFileChart);

                var iDeleteTAG = document.createElement('a');
                iDeleteTAG.innerHTML = 'Excluir Sprint';
                iDeleteTAG.setAttribute('id', 'id_iDeleteTAG_' + sprint.CodigoIteracao);
                iDeleteTAG.setAttribute('data-codigoiteracao', sprint.CodigoIteracao);
                iDeleteTAG.setAttribute('data-codigoprojetoiteracao', sprint.CodigoProjetoIteracao);
                iDeleteTAG.setAttribute('title', 'Excluir Sprint');
                iDeleteTAG.setAttribute('style', 'cursor:pointer');

                iDeleteTAG.addEventListener("click", function (e) {
                    var iDeleteTAG1 = document.getElementById(e.target.id);
                    var codigoProjetoIteracao = iDeleteTAG1.getAttribute('data-codigoprojetoiteracao');
                    var codigoIteracao = iDeleteTAG1.getAttribute('data-codigoiteracao');

                    var funcObj = { funcaoClickOK: function () { excluiSprint(codigoProjetoIteracao, codigoIteracao); } };
                    window.top.mostraConfirmacao('Deseja realmente excluir a sprint?', function () { funcObj['funcaoClickOK'](); }, null);
                });


                var divMenuSanduiche = document.createElement('div');
                divMenuSanduiche.classList.add('kanban-agil-tarefa-menu-sprint');
                divMenuSanduiche.setAttribute('data-toggle', 'dropdown');
                divMenuSanduiche.innerHTML = '<i class="fas fa-ellipsis-h"></i>';
                divMenuSanduiche.setAttribute('id', 'menusanduichesprint_' + sprint.CodigoIteracao);
                divMenuSanduiche.setAttribute('title', 'Opções da Sprint');

                var divAcionadaPeloMenuSanduiche = document.createElement('div');
                divAcionadaPeloMenuSanduiche.setAttribute('style', 'width:75px');
                divAcionadaPeloMenuSanduiche.setAttribute('aria-labelledby', 'menusanduichesprint_' + sprint.CodigoIteracao);
                divAcionadaPeloMenuSanduiche.classList.add('dropdown-menu');

                var tabela = document.createElement('table');

                var linha_edit = document.createElement('tr');
                var coluna_edit = document.createElement('td');
                coluna_edit.classList.add('dropdown-item');
                coluna_edit.appendChild(iEditTAG);
                linha_edit.appendChild(coluna_edit);

                var linha_delete = document.createElement('tr');
                var coluna_delete = document.createElement('td');
                coluna_delete.classList.add('dropdown-item');
                coluna_delete.appendChild(iDeleteTAG);
                linha_delete.appendChild(coluna_delete);

                var linha_users = document.createElement('tr');
                var coluna_users = document.createElement('td');
                coluna_users.classList.add('dropdown-item');
                coluna_users.appendChild(iconeUsers);
                linha_users.appendChild(coluna_users);
                if (document.getElementById("permiteAlterarSprint").innerHTML == "True") {
                    tabela.appendChild(linha_edit);
                }

                var iteracoesCode = $("#iteracoesCode").val();
                var ar = iteracoesCode.split(',');
                tabela.appendChild(linha_delete);
                linha_delete.style.visibility = "collapse";
                if (document.getElementById("permiteExcluirSprint").innerHTML == "True" && sprint.PossuiItemAssociado == 0) {
                    linha_delete.style.visibility = "visible";
                }

                if (document.getElementById("permiteManterEquipe").innerHTML == "True") {
                    tabela.appendChild(linha_users);
                }
                divAcionadaPeloMenuSanduiche.appendChild(tabela);

                var divBotaoAjuda = document.createElement('div');
                divBotaoAjuda.setAttribute('id', 'divBotaoAjuda');
                var aTAG = document.createElement('a');

                aTAG.setAttribute('title', 'Ajuda');
                aTAG.setAttribute('style', 'cursor:pointer; margin:15px;bottom:0');

                aTAG.addEventListener("click", function (e) {
                    var sUrl = window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/popupAdicionaItemSprint.aspx';
                    if (document.fullscreen == true) {
                        document.exitFullscreen();
                    }
                    setTimeout(function () {
                        window.top.showModal(sUrl, 'Ajuda', 450, null, null, null);
                    }, 1000);

                });

                var iTAG = document.createElement('i');
                iTAG.classList.add('fa');
                iTAG.classList.add('fa-question-circle');
                iTAG.classList.add('fa-2x');
                iTAG.classList.add('fa-inverse')
                iTAG.classList.add('my-float');

                aTAG.appendChild(iTAG);

                divBotaoAjuda.appendChild(aTAG);

                divLinhaSuperior.appendChild(fonte);
                divLinhaSuperior.appendChild(divContainer_iEditTAG);
                divLinhaSuperior.appendChild(divMenuSanduiche);

                divLinhaSuperior.appendChild(divAcionadaPeloMenuSanduiche);

                divContainer1.appendChild(divLinhaSuperior);
                divContainer1.appendChild(divLinhaDoMeio);
                divContainer1.appendChild(divLinhaInferior);

                div3.appendChild(divContainer1);
                div.appendChild(div3);
            }

            obterItensDeBacklogDaTela();
            var qtdItem = document.getElementById('divLinhaDoMeio').innerHTML;
            if (qtdItem == 'Nenhum item') {
                window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/popupAdicionaItemSprint.aspx', 'Ajuda', 450, null, null, null);
            }
            lpLoading.Hide();
        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });

    }

    function obterItensDeBacklogDaTela() {
        $.ajax({
            data: 'codigoProjeto=' + encodeURI(codigoProjeto) + '&pesquisaTexto=' + pesquisaTexto,
            url: './agil-taskboard-service-v2.asmx/obter-itens-backlog',
            type: 'POST'
        }).done(function (data2, textStatus, jqXHR) {
            
            var jsonItens = JSON.parse(data2.firstElementChild.innerHTML);
            var selecionaTodasAsColunas = document.querySelectorAll('div[data-codigoiteracao]');//para cada coluna que não tiver um ul, colocar                

            for (var i = 0; i < selecionaTodasAsColunas.length; i++) {
                var divSelecionada = selecionaTodasAsColunas[i];
                var divContainerUL = document.createElement('div');
                var ul = document.createElement('ul');
                ul.classList.add('kanban-agil');



                for (var k = 0; k < jsonItens.length; k++) {
                    var item = jsonItens[k];//de acordo com o código a que o ítem pertença será selecionada uma div especifica para ele
                    if (item.CodigoIteracao == divSelecionada.getAttribute('data-codigoiteracao')) {
                        ul.appendChild(kanbanAgilItemBacklog(item));
                    }
                }
                var sHeight = Math.max(0, document.documentElement.clientHeight) - 135;
                //ul.setAttribute('style', 'height:' + sHeight + 'px');
                divContainerUL.appendChild(ul);
                divSelecionada.appendChild(divContainerUL);
                divContainerUL.setAttribute('style', 'height:' + sHeight + 'px');
            }
            customizaBarrasDeRolagemEstilizadas();
            customizaEfeitosDeArrastarESoltarCards();
            atualizaQuantidadeItensBacklog();

            if ($('.kanban-agil').length === 0)
                $('.kanban-agil-raias-sprint').first().after($('<div class="no-data-to-display">Não há dados a serem exibidos</div>'));

            var sHeight = Math.max(0, document.documentElement.clientHeight);
            var divDoKanbanSprint = document.querySelector('.kanban-agil-raias-sprint');
            divDoKanbanSprint.setAttribute('style', 'height:' + (sHeight - 65) + 'px');
            lpLoading.Hide();

        }).fail(function (jqXHR, textStatus, msg) {
            mostraErroEEscondeLoading(jqXHR);
        });
    }

    function customizaEfeitosDeArrastarESoltarCards() {
        $('div[data-codigoiteracao] > div:not(:first-child) > div > div > ul').sortable({
            //connectWith: '.kanban-agil',
            connectWith: 'div[data-codigoiteracao] > div:not(:first-child) > div > div > ul',
            placeholder: 'kanban-agil-tarefa-sombra',
            opacity: 0.7,
            //disabled: !sprint.IndicaUsuarioLogadoMembroEquipe,
            beforeStop: function (event, ui) {
                //var classeDeBloqueio = ui.item.get(0).querySelector('.kanban-agil-tarefa-cabecalho-titulo-bloqueio');
                //if (classeDeBloqueio !== null) {
                //    $(this).sortable('cancel');
                //}

                var destino = ui.item.get(0);
                var codigoIteracaoDestino = destino.parentElement.parentElement.parentElement.parentElement.parentElement.getAttribute('data-codigoiteracao');
                var percentualConcluido = destino.getAttribute('data-percentualconcluido');
                var nomeSprintDestino = destino.parentElement.parentElement.parentElement.parentElement.parentElement.getAttribute('data-nomesprint');
                var indicaSprintEncerrada = destino.parentElement.parentElement.parentElement.parentElement.parentElement.getAttribute('data-indicasprintencerrada');

                var permiteIncluirItemSprint = destino.parentElement.parentElement.parentElement.parentElement.parentElement.getAttribute('data-permiteincluiritemsprint');//Se vier null significa que o Destino é Backlog

                //- Não permitir arrastar para a raia "Backlog" quando o percentual concluído for maior que 0% e verifica permissão de inclusão e exclusão de itens das sprints
                
                if ((parseInt(codigoIteracaoDestino) === 0 && parseInt(percentualConcluido) > 0) || indicaSprintEncerrada === 'S'
                    || permiteIncluirItemSprint == "false") {

                    if ((parseInt(codigoIteracaoDestino) === 0 && parseInt(percentualConcluido) > 0) == true) {
                        window.top.mostraMensagem('Este item não pode ser movido para cá porque ele já foi iniciado na Sprint.', 'atencao', true, false, null);
                    }
                    if (indicaSprintEncerrada === 'S') {
                        window.top.mostraMensagem('Este item não pode ser movido para cá porque esta sprint já está encerrada.', 'atencao', true, false, null);
                    }

                    if (permiteIncluirItemSprint == "false") {
                        window.top.mostraMensagem('Usuário não tem permissão para incluir itens na Sprint: ' + nomeSprintDestino, 'atencao', true, false, null);
                    }

                    $(this).sortable('cancel');
                }


            },
            sort: function (event, ui) {
                $('.kanban-agil-tarefa-sombra').height(ui.item.height());
            },
            receive: function (event, ui) {

                var item = ui.item.get(0);
                var origem = ui.sender.get(0);
                var destino = ui.item.get(0);
                var codigoIteracaoOrigem = origem.parentElement.parentElement.parentElement.parentElement.getAttribute('data-codigoiteracao');
                var codigoIteracaoDestino = destino.parentElement.parentElement.parentElement.parentElement.parentElement.getAttribute('data-codigoiteracao');
                var codigoItemMovimentado = item.getAttribute('data-codigoitem');
                var nomeSprintOrigem = origem.parentElement.parentElement.parentElement.parentElement.getAttribute('data-nomesprint');
                // Este setTimeout() é importante pois evita que dois itens sejam movidos simultaneamente por causa de algum "eventual" problema de sincronismo entre o Sortable do jQuery UI e o Ajax que atrasará a execução final do evento "receive". Precisa ser melhor compreendido.
                //console.log(item);
                //console.log(origem);
                //console.log(destino);

                var indicaSprintEncerrada = origem.parentElement.parentElement.parentElement.parentElement.getAttribute('data-indicasprintencerrada');
                var codigoiteracaoOrigem = origem.parentElement.parentElement.parentElement.parentElement.getAttribute('data-codigoiteracao');
                //console.log(origem.parentElement.parentElement.parentElement.parentElement);

                var permiteExcluirItemSprint = origem.parentElement.parentElement.parentElement.parentElement.getAttribute('data-permiteexcluiritemsprint');//Se vier null significa que o Destino é Backlog

                if (indicaSprintEncerrada === 'S' || permiteExcluirItemSprint == "false") {
                    if (indicaSprintEncerrada === 'S') {
                        window.top.mostraMensagem('Este item não pode ser retirado desta sprint porque ela já está encerrada.', 'atencao', true, false, null);
                    }

                    if (permiteExcluirItemSprint == "false") {
                        window.top.mostraMensagem('Usuário não tem permissão para excluir itens da Sprint: ' + nomeSprintOrigem, 'atencao', true, false, null);
                    }

                    var divBacklog = document.querySelector('div[data-codigoiteracao="' + codigoiteracaoOrigem + '"] > div:not(:first-child) > div > div > ul');
                    divBacklog.appendChild(item);

                }
                else {
                    if (!Number.isNaN(codigoIteracaoOrigem) && !Number.isNaN(codigoIteracaoDestino) && !Number.isNaN(codigoItemMovimentado)) {
                        setTimeout(function () {
                            kanbanAgilArrastaESoltaItemBacklog(codigoItemMovimentado, codigoIteracaoOrigem, codigoIteracaoDestino);//kanbanAgilArrastaESoltaItemBacklog(item, origem, destino);
                        }, 500);
                    }
                }
            }
        }).disableSelection();
    }

    function customizaBarrasDeRolagemEstilizadas() {
        //.kanban-agil-raias > h2:not(:first-child)
        $("div[data-codigoiteracao] > div:not(:first-child)").mCustomScrollbar({
            scrollbarPosition: "inside",
            setHeight: false,
            setWidth: '335px',
            autoHideScrollbar: true,
            alwaysShowScrollbar: 0,
            scrollInertia: 0,
            theme: "rounded-dark",
            autoDraggerLength: true,
            scrollButtons: { enable: true }
        });
    }

    function atualizaQuantidadeItensBacklog() {
        var ulList = document.querySelectorAll('div[data-codigoiteracao="0"] > div:not(:first-child) > div > div >  ul > li');

        var divQuantidade = document.getElementById('divQuantidadeItensBacklog');
        if (ulList.length == 0) {
            divQuantidade.innerHTML = 'Nenhum item';
        }
        else if (ulList.lenght == 1) {
            divQuantidade.innerHTML = 'um item';
        }
        else {
            divQuantidade.innerHTML = ulList.length + ' itens';
        }
    }

    async function kanbanAgilEditaTarefa(item, acao) {
        var codigoItem = item.getAttribute('data-codigoitem');
        var codigoItemSuperior = item.getAttribute('data-codigoitemsuperior');
        var url = '';
        var alturaTela = 480;//parseInt(document.querySelector('#alturaTela').innerHTML, 10) - 200;
        switch (acao) {
            case 'kanbanAgilEditaTarefa':
            case 'kanbanAgilEditaTarefaBacklog':
                {
                    var idprojeto = item.parentElement.parentElement.parentElement.parentElement.parentElement.getAttribute('data-codigoprojetoiteracao');
                    url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/popupItensBacklog.aspx?CI=" + encodeURI(codigoItem) + "&CIS=" + encodeURI(codigoItemSuperior) + "&acao=" + encodeURI(acao) + "&IDProjeto=" + encodeURI(idprojeto) + "&ALT=" + (screen.height - 200).toString() + "&tela=taskboardAgil.js";
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
        }

        var retorno = function (codigoItem) {
            if ((codigoItem != null) && (codigoItem != "")) {
                $.ajax({
                    data: 'codigoItem=' + encodeURI(codigoItem),
                    url: './agil-taskboard-service-v2.asmx/obter-detalhes-item',
                    type: 'POST'
                }).done(function (data, textStatus, jqXHR) {
                    
                    var novoItem = JSON.parse(data.firstElementChild.innerHTML)[0];

                    switch (acao) {
                        case 'kanbanAgilEditaTarefa':
                        case 'kanbanAgilEditaTarefaBacklog':
                            {

                                var divIteracao = document.querySelector('div[data-codigoiteracao="' + novoItem.CodigoIteracao + '"] > div:not(:first-child) > div > div > ul');
                                var itemASubstituir = divIteracao.querySelector('li[data-codigoitem="' + novoItem.CodigoItem + '"]');
                                divIteracao.replaceChild(kanbanAgilItemBacklog(novoItem), itemASubstituir);
                                break;
                            }
                    }
                }).fail(function (xhr, status, error) {
                    mostraErroEEscondeLoading(xhr);
                });
            }
            else {
                if (acao === 'kanbanAgilAssociaTarefaBacklogNaoPlanejada' || acao === 'kanbanAgilIncluiTarefaBacklogNaoPlanejada') {

                    kanbanAgilCarregaSprintsDoProjeto();
                }
            }
        };
        if (document.fullscreen == true) {
            document.exitFullscreen();
        }
        setTimeout(function () {
            window.top.showModalComFooter(url, traducao.taskboardAgil_edi__o, null, null, retorno, null);
        }, 1000);



    }

    function kanbanAgilItemBacklog(item) {

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


        html += (item.IndicaBloqueioItem == 'S') ? '<div class="kanban-agil-tarefa-cabecalho-titulo-bloqueio">' : '<div class="kanban-agil-tarefa-cabecalho-titulo">';


        html += '<div title="' + htmlEncode(item.CodigoItem + ' ' + htmlDecode(item.TituloItem)) + '" ">' + htmlDecode(item.TituloItem) + '</div>';
        html += '</div>';

        //if (sprint.IndicaUsuarioLogadoMembroEquipe) {
        if (document.getElementById("permiteManterItensBacklog").innerHTML == "True" || document.getElementById("permiteExcluirItensBacklog").innerHTML == "True") {
            html += '<div class="kanban-agil-tarefa-menu" data-toggle="dropdown" id="kanbanAgilMenu">';
            html += '<i class="fas fa-ellipsis-h" style="display:none"></i>';
            html += '</div>';
        }
        html += '<div aria-labelledby="kanbanAgilMenu" class="dropdown-menu">';
        if (document.getElementById("permiteManterItensBacklog").innerHTML == "True") {
            html += '<a class="dropdown-item kanban-agil-tarefa-menu-editar-item-backlog" href="javascript:void(0);" title="Editar Item"><i class="fas fa-edit"></i> Editar Item</a>';
        }
        if (item.CodigoIteracao == 0 && document.getElementById("permiteExcluirItensBacklog").innerHTML == "True") {
            html += '<a class="dropdown-item kanban-agil-tarefa-menu-excluir-item-backlog" href="javascript:void(0);" title="Excluir Item"><i class="fas fa-trash"></i> Excluir Item</a>';
        }

        html += '</div>';
        //}

        html += (item.IniciaisUsuarioRecurso !== null) ? '<div class="kanban-agil-tarefa-usuario">' : '<div class="kanban-agil-tarefa-usuario-float-right">';
        if (item.IniciaisUsuarioRecurso !== null) {
            html += '<div class="kanban-agil-tarefa-usuario-sigla">';
            html += '<span title="' + htmlEncode(item.IniciaisUsuarioRecurso) + '">' + htmlEncode(item.IniciaisUsuarioRecurso) + '</span>';
            html += '</div>';
            html += '<div class="kanban-agil-tarefa-usuario-nome text-nowrap text-truncate" title="' + htmlEncode(item.NomeUsuarioRecurso) + '">' + htmlEncode(item.NomeUsuarioRecurso) + '</div>';
        }

        var htmlIconeTemporario = '';
        html += htmlIconeTemporario;

        concluido = item.PercentualConcluido.toFixed(2).toString();
        concluido = concluido.substr(0, concluido.length - 3);

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

        if (item.IndicaBloqueioItem.toString().trim() == 'S') {
            html += '<div style="padding:0px 5px;"><img src="../../../imagens/agil_impedimento.png" title="Item com impedimento - Não é possível movê-lo entre raias enquanto estiver com impedimento!"></div>';
        }

        if (item.IniciaisTipoClassificacaoItemControladoSistema == 'PROBLEMA') {
            html += '<div style="padding:0px 5px;"><img src="../../../imagens/agil_bug.png" title="' + item.DescricaoTipoClassificacaoItem + '"></div>';
        }

        html += '</div>';
        //}
        html += '</div>';

        html += '<ul class="kanban-agil-tarefa-tags">';

        if (item.Tag1 != null) {
            html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate" title="' + htmlEncode(item.Tag1) + '">' + htmlEncode(item.Tag1) + '</li>';
        }
        if (item.Tag2 != null) {
            html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate" title="' + htmlEncode(item.Tag2) + '">' + htmlEncode(item.Tag2) + '</li>';
        }
        if (item.Tag3 != null) {
            html += '<li class="kanban-agil-tarefa-tag text-nowrap text-truncate" title="' + htmlEncode(item.Tag3) + '">' + htmlEncode(item.Tag3) + '</li>';
        }

        var estiloItem = '""';
        //alert(item.PercentualConcluido);
        if (item.PercentualConcluido === 100) {
            estiloItem = '"background-color: #87cefa"';//azul claro
        }
        else {
            estiloItem = '"background-color:#b8e4b2"';//verde claro
        }
        // Se %concluído for 100%, Colocar em fundo azul, verde quando estiver fazendo (%concluído > 0% e < 100%.
        if (item.DescricaoTipoStatusItem != null) {
            html += '<li class="kanban-agil-tarefa-tag-status text-nowrap text-truncate" style=' + estiloItem + ' title="' + htmlEncode(item.DescricaoTipoStatusItem) + '">' + htmlEncode(item.DescricaoTipoStatusItem) + '</li>';
        }
        html += '</ul>';
        html += '</div>';
        //alert(html);
        var li = document.createElement('li');
        li.setAttribute('id', item.CodigoItem);
        li.setAttribute('data-codigoitem', item.CodigoItem);
        li.setAttribute('data-codigoitemsuperior', item.CodigoItemSuperior);
        li.setAttribute('data-codigoitemespelho', item.CodigoItemEspelho);
        li.setAttribute('data-percentualconcluido', item.PercentualConcluido);

        li.innerHTML = html;
        //li.classList.add('kanban-agil-tarefa');
        if (item.IndicaItemNaoPlanejado == 'S') {
            li.classList.add('kanban-agil-tarefa-nao-planejada');
        }
        if (item.IndicaBloqueioItem == 'S') {
            li.classList.add('kanban-agil-tarefa-bloqueada');
        }

        //if (sprint.IndicaUsuarioLogadoMembroEquipe) {

        li.addEventListener('dblclick', function (e) {
            kanbanAgilEditaTarefa(this, 'kanbanAgilEditaTarefaBacklog');
        });
        var botaoEditar = li.querySelector('.kanban-agil-tarefa-menu-editar-item-backlog');
        if (botaoEditar != null) {
            botaoEditar.addEventListener('click', function (e) {
                kanbanAgilEditaTarefa(this.parentElement.parentElement.parentElement, 'kanbanAgilEditaTarefaBacklog');
            });
        }
        var botaoExcluir = li.querySelector('.kanban-agil-tarefa-menu-excluir-item-backlog');
        if (botaoExcluir != null) {
            botaoExcluir.addEventListener('click', function (e) {
                var codigoItem = this.parentElement.parentElement.parentElement.getAttribute('data-codigoitem');
                var codigoIteracao = this.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.getAttribute('data-codigoiteracao');

                var funcObj = { funcaoClickOK: function () { excluiItemBacklog(codigoItem, codigoIteracao); } };
                window.top.mostraConfirmacao('O item e todas as suas tarefas serão excluídos. Confirma?', function () { funcObj['funcaoClickOK'](); }, null);
            });
        }
        li.addEventListener('mouseover', function (e) {
            var objetoEllipsis = e.currentTarget.querySelector('.kanban-agil-tarefa > .kanban-agil-tarefa-menu > i');
            if (objetoEllipsis != undefined) {
                objetoEllipsis.setAttribute('style', 'display:block');
            }
        });

        li.addEventListener('mouseout', function (e) {
            var objetoEllipsis = e.currentTarget.querySelector('.kanban-agil-tarefa > .kanban-agil-tarefa-menu > i');
            if (objetoEllipsis != undefined) {
                objetoEllipsis.setAttribute('style', 'display:none');
            }
        });

        //}

        return (li);
    }

    function kanbanPesquisaTexto() {
        pesquisaTexto = document.querySelector('#pesquisaTexto').value.trim();

        kanbanAgilCarregaSprintsDoProjeto();
        $('#pesquisaTexto').focus().select();
    }

    /* Versão simplificada da sprintf() usada para substituir variáveis em uma string utilizando chaves: {0}, {1}, ... */
    function sprintf() {
        var text = arguments[0];
        for (var i = 1; i < arguments.length; i++) {
            text = text.replace('{' + (i - 1).toString() + '}', arguments[i]);
        }
        return (text);
    }
    /*
     * ########################
     * ### Início da página ###
     * ########################
    */
    // Carrega as informações da sprint atual.
    kanbanAgilCarregaSprintsDoProjeto();
    
    if (document.getElementById("permiteIncluirNovaSprint").innerHTML == "True") {
        insereBotaoFloatAjuda();
        insereBotaoFloatAddSprintNoKanban();
    }

    //});
});

function mostraErroEEscondeLoading(jqXHR) {
    
    var mensagemFinal = 'Falha ao obter os dados. Mensagem original: ' + jqXHR.responseText;
    window.top.mostraMensagem(mensagemFinal, 'atencao', true, false, null);
}
