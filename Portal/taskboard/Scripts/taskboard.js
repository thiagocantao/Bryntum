if (String.prototype.ano == null) {

    String.prototype.ano = function (idiomaPadrao) {

        var separador = this.replace(/[0-9]/gi, '').substr(0, 1);
        var data = this.split(separador);
        return data[2];
    }

}

if (String.prototype.mes == null) {

    String.prototype.mes = function (idiomaPadrao) {
        var separador = this.replace(/[0-9]/gi, '').substr(0, 1);
        var data = this.split(separador);
        var i = 1;
        if (idiomaPadrao !== true) {
            if (traducao.geral_formato_data_js == 'MM/DD/YYYY') // Idioma "en" English
            {
                i = 0;
            }
        }
        return (data[i]);
    }

}

if (String.prototype.dia == null) {

    String.prototype.dia = function (idiomaPadrao) {
        var separador = this.replace(/[0-9]/gi, '').substr(0, 1);
        var data = this.split(separador);
        var i = 0;
        if (idiomaPadrao !== true) {
            if (traducao.geral_formato_data_js == 'MM/DD/YYYY') // Idioma "en" English
            {
                i = 1;
            }
        }
        return (data[i]);
    }

}

$(document).ready(function () {
    //alert(hfGeral.Get('indicaTarefasAtrasadasURL'));
    $('#atrasadasTodo').prop('checked', hfGeral.Get('indicaTarefasAtrasadasURL') == 'S' ? true : false);
    $('#atrasadasDoing').prop('checked', hfGeral.Get('indicaTarefasAtrasadasURL') == 'S' ? true : false);
    var idioma = document.querySelector('#idioma').innerHTML;
    moment.locale(idioma.toLocaleLowerCase());
    if (idioma == 'pt-BR') {
        $.fn.datepicker.defaults.language = idioma;
    }
    // traducao
    traducao.kanban_Meses = JSON.parse(traducao.kanban_Meses);
    traducao.kanban_Status = JSON.parse(traducao.kanban_Status);
    traducao.kanban_StatusAprovacao = JSON.parse(traducao.kanban_StatusAprovacao);
    traducao.kanban_MomentLocale = JSON.parse(traducao.kanban_MomentLocale);

    // pesquisaModoExibicaoCards
    var pesquisaModoExibicaoCards = Cookies.get('Brisk_Kanban_pesquisaModoExibicaoCards');
    if ((pesquisaModoExibicaoCards == null) || ((pesquisaModoExibicaoCards != 'C') && (pesquisaModoExibicaoCards != 'S'))) {
        pesquisaModoExibicaoCards = 'S'; // S = Simples, C = Completo
    }
    switch (pesquisaModoExibicaoCards) {
        case 'S':
            {
                document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').classList.add('btn-color-active');
                document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').classList.remove('btn-color-disable');
                document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').classList.remove('btn-color-active');
                document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').classList.add('btn-color-disable');
                break;
            }
        case 'C':
            {
                document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').classList.add('btn-color-active');
                document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').classList.remove('btn-color-disable');
                document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').classList.remove('btn-color-active');
                document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').classList.add('btn-color-disable');
                break;
            }
    }

    // pesquisaDataInicio
    var pesquisaDataInicio = Cookies.get('Brisk_Kanban_pesquisaDataInicio');
    if (pesquisaDataInicio == null) {
        //pesquisaDataInicio = moment().subtract(30, 'd');
    }
    else {
        /*
        pesquisaDataInicio = moment(pesquisaDataInicio, 'YYYY-MM-DD');
        if (!pesquisaDataInicio.isValid()) {
            pesquisaDataInicio = moment().subtract(30, 'd');
        }
        */
        pesquisaDataInicio = null;
    }

    // pesquisaDataFim
    var pesquisaDataFim = Cookies.get('Brisk_Kanban_pesquisaDataFim');
    if (pesquisaDataFim == null) {
        //pesquisaDataFim = moment();
    }
    else {
        /*
        pesquisaDataFim = moment(pesquisaDataFim, 'YYYY-MM-DD');
        if (!pesquisaDataFim.isValid()) {
            pesquisaDataFim = moment();
        }
        */
        pesquisaDataFim = null;
    }

    // pesquisaData
    if ((pesquisaDataInicio == null) || (pesquisaDataFim == null)) {
        $('#pesquisaData').val('Data de Início - Data de Fim');
    }
    else {
        $('#pesquisaData').val(pesquisaDataInicio.format(traducao.geral_formato_data_js) + ' - ' + pesquisaDataFim.format(traducao.geral_formato_data_js));
    }

    // pesquisaTexto
    var pesquisaTexto = '';

    // ordenacaoToDo
    var ordenacaoTodo = '';

    // ordenacaoDoing
    var ordenacaoDoing = '';

    // ordenacaoDone
    var ordenacaoDone = '';

    document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').addEventListener('click', function (e) {
        document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').classList.add('btn-color-active');
        document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').classList.remove('btn-color-disable');
        document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').classList.remove('btn-color-active');
        document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').classList.add('btn-color-disable');
        pesquisaModoExibicaoCards = 'S';
        Cookies.set('Brisk_Kanban_pesquisaModoExibicaoCards', pesquisaModoExibicaoCards);
        var dataPagina = parseInt($('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina'), 10);
        if (dataPagina > 0) {
            $('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina', 1);
        }
        kanbanCarregaCards();
    });

    document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').addEventListener('click', function (e) {
        document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').classList.add('btn-color-active');
        document.querySelector('#botaoPesquisaModoExibicaoCardsCompleto').classList.remove('btn-color-disable');
        document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').classList.remove('btn-color-active');
        document.querySelector('#botaoPesquisaModoExibicaoCardsSimples').classList.add('btn-color-disable');
        pesquisaModoExibicaoCards = 'C';
        Cookies.set('Brisk_Kanban_pesquisaModoExibicaoCards', pesquisaModoExibicaoCards);
        var dataPagina = parseInt($('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina'), 10);
        if (dataPagina > 0) {
            $('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina', 1);
        }
        kanbanCarregaCards();
    });

    document.querySelector('#ordenacaoTodo').addEventListener('change', function (e) {
        ordenacaoTodo = document.querySelector('#ordenacaoTodo').value;
        kanbanCarregaCards('todo');
    });

    document.querySelector('#ordenacaoDoing').addEventListener('change', function (e) {
        ordenacaoDoing = document.querySelector('#ordenacaoDoing').value;
        kanbanCarregaCards('doing');
    });

    document.querySelector('#ordenacaoDone').addEventListener('change', function (e) {
        ordenacaoDone = document.querySelector('#ordenacaoDone').value;
        var dataPagina = parseInt($('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina'), 10);
        if (dataPagina > 0) {
            $('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina', 1);
        }
        kanbanCarregaCards('done');
    });

    if ((pesquisaDataInicio == null) || (pesquisaDataFim == null)) {
        document.querySelector('#pesquisaDataInicio').placeholder = 'Data de Início';
    }
    else {
        document.querySelector('#pesquisaDataInicio').value = moment(pesquisaDataInicio).format('L');
    }

    if ((pesquisaDataInicio == null) || (pesquisaDataFim == null)) {
        document.querySelector('#pesquisaDataFim').placeholder = 'Data de Fim';
    }
    else {
        document.querySelector('#pesquisaDataFim').value = moment(pesquisaDataFim).format('L');
    }

    document.querySelector('#botaoPesquisaDatas').addEventListener('click', function (e) {
        if (!((document.querySelector('#pesquisaDataInicio').value == '') && (document.querySelector('#pesquisaDataFim').value == ''))) {
            if (document.querySelector('#pesquisaDataInicio').value == '') {
                alert('A data de início não foi preenchida.');
                return;
            }
            if (document.querySelector('#pesquisaDataFim').value == '') {
                alert('A data de fim não foi preenchida.');
                return;
            }
        }
        var dataInicio = moment(document.querySelector('#pesquisaDataInicio').value, traducao.geral_formato_data_js);
        var dataFim = moment(document.querySelector('#pesquisaDataFim').value, traducao.geral_formato_data_js);
        if (dataInicio > dataFim) {
            alert('A data de início não pode ser maior do que a data de fim.');
            return;
        }
        pesquisaDataInicio = dataInicio;
        pesquisaDataFim = dataFim;
        Cookies.set('Brisk_Kanban_pesquisaDataInicio', dataInicio.format('YYYY-MM-DD'));
        Cookies.set('Brisk_Kanban_pesquisaDataFim', dataFim.format('YYYY-MM-DD'));
        $('#pesquisaData').val(dataInicio.format(traducao.geral_formato_data_js) + ' a ' + dataFim.format(traducao.geral_formato_data_js));
        var dataPagina = parseInt($('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina'), 10);
        if (dataPagina > 0) {
            $('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina', 1);
        }
        kanbanCarregaCards();
    });

    document.querySelector('#botaoLimpaDatas').addEventListener('click', function (e) {
        pesquisaDataInicio = null;
        pesquisaDataFim = null;
        $('#pesquisaData').val('Data de Início - Data de Fim');
        $('#pesquisaDataInicio').val('Data de Início');
        $('#pesquisaDataFim').val('Data de Fim');
        var dataPagina = parseInt($('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina'), 10);
        if (dataPagina > 0) {
            $('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina', 1);
        }
        kanbanCarregaCards();
    });

    $('#pesquisaDataInicio').datepicker({
        autoclose: true,
        todayBtn: true
    }).on('changeDate', function (e) {
    });

    $('#pesquisaDataFim').datepicker({
        autoclose: true,
        todayBtn: true
    }).on('changeDate', function (e) {
    });

    $('#pesquisaData').daterangepicker({
        endDate: pesquisaDataFim,
        linkedCalendars: false,
        locale: traducao.kanban_MomentLocale,
        startDate: pesquisaDataInicio,
        showDropdowns: true
    }, function (dataInicio, dataFim, legenda) {
        pesquisaDataInicio = dataInicio;
        pesquisaDataFim = dataFim;
        Cookies.set('Brisk_Kanban_pesquisaDataInicio', dataInicio.format('YYYY-MM-DD'));
        Cookies.set('Brisk_Kanban_pesquisaDataFim', dataFim.format('YYYY-MM-DD'));
        $('#pesquisaData').val(dataInicio.format(traducao.geral_formato_data_js) + ' a ' + dataFim.format(traducao.geral_formato_data_js));
        var dataPagina = parseInt($('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina'), 10);
        if (dataPagina > 0) {
            $('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina', 1);
        }
        kanbanCarregaCards();
    });

    $('.kanban-coluna:nth-child(3) > .kanban-lista').on('scroll', function (e) {
        var dataPagina = parseInt($(this).data('pagina'), 10);
        if (dataPagina > 0) {
            var element = event.target;
            if (element.scrollHeight - element.scrollTop === element.clientHeight) {
                dataPagina++;
                kanbanCarregaCards('done', dataPagina);
                $(this).data('pagina', dataPagina);
            }
        }
    });

    var timerPesquisaTexto;
    var delayPesquisaTexto = 1000;

    ['input', 'propertychange'].map(function (evento) {

        $('.form-search i.fa-search').removeClass('hidden');
        $('.form-search i.fa-times').addClass('hidden');

        document.querySelector('#pesquisaTexto').addEventListener(evento, function (e) {
            clearTimeout(timerPesquisaTexto);
            timerPesquisaTexto = setTimeout(function () { kanbanPesquisaTexto() }, delayPesquisaTexto);
        });
    });

    document.querySelector('#pesquisaTexto').addEventListener('keypress', function (e) {

        $('.form-search i.fa-search').removeClass('hidden');
        $('.form-search i.fa-times').addClass('hidden');

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

    $('.form-search > i.fa-times').on('click', function (e) {

        var input = $('.form-search > #pesquisaTexto');

        input.val('');

        kanbanPesquisaTexto();

        $('.form-search > i.fa-search').removeClass('hidden');
        $('.form-search > i.fa-times').addClass('hidden');

        if ($(window).width() > 1024) {
            input.focus().select();
        }
        else {
            input.trigger('blur');
        }
    });

    function kanbanPesquisaTexto() {
        pesquisaTexto = document.querySelector('#pesquisaTexto').value.trim();
        var dataPagina = parseInt($('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina'), 10);
        if (dataPagina > 0) {
            $('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina', 1);
        }
        kanbanCarregaCards();
        $('#pesquisaTexto').focus().select();

        $('.form-search > i.fa-search').addClass('hidden');
        $('.form-search > i.fa-times').removeClass('hidden');
    }

    function kanbanCarregaCards(status, paginacao, complemento) {



        carregando();
        switch (status) {
            case 'todo':
            case 'doing':
            case 'done':
                {
                    var arrayStatus = [status];
                    $('#kanban #' + status).closest('.kanban-coluna').find('.kanban-coluna-cabecalho-contador').text('');
                    break;
                }
            default:
                {
                    var arrayStatus = ['todo', 'doing', 'done'];
                    $('#kanban .kanban-coluna-cabecalho-contador').text('');
                }
        }
        var cont = 0;
        arrayStatus.forEach(function (status, indice, array) {
            if (paginacao === undefined) {
                pagina = 0;
            }
            else
            {
                pagina = paginacao;
            }
            var dataPagina = parseInt($('.kanban-coluna:nth-child(3) > .kanban-lista').data('pagina'), 10);
            if (status == 'done')
            {
                if ((dataPagina == 0) || (paginacao === undefined) || (paginacao == 0))
                {
                    document.querySelector('#' + status).innerHTML = '';
                }
                if (dataPagina == 0)
                {
                    pagina = 0;
                }
                else
                {
                    if (paginacao === undefined)
                    {
                        pagina = 1;
                    }
                }
            }
            else {
                document.querySelector('#' + status).innerHTML = '';
            }
            switch (status) {
                case 'todo':
                    {
                        var pesquisaExibirCardsAtrasados = $('#atrasadasTodo').is(':checked');
                        var ordenacao = ordenacaoTodo;
                        break;
                    }
                case 'doing':
                    {
                        var pesquisaExibirCardsAtrasados = $('#atrasadasDoing').is(':checked');
                        var ordenacao = ordenacaoDoing;
                        break;
                    }
                case 'done':
                    {
                        var pesquisaExibirCardsAtrasados = false;
                        var ordenacao = ordenacaoDone;
                        break;
                    }
                default:
                    {
                        var pesquisaExibirCardsAtrasados = false;
                        var ordenacao = '';
                    }
            }
            if (ordenacao == '') {
                ordenacao = 'prioridade';
            }

            var parametros = '';
            parametros += 'status=' + status;
            parametros += '&pesquisaExibirCardsAtrasados=' + encodeURI(pesquisaExibirCardsAtrasados ? '1' : '0');
            if ((pesquisaDataInicio == null) || (pesquisaDataFim == null)) {
                parametros += '&pesquisaDataInicio=';
                parametros += '&pesquisaDataFim=';
            }
            else {
                parametros += '&pesquisaDataInicio=' + encodeURI(pesquisaDataInicio.format('YYYY/MM/DD'));
                parametros += '&pesquisaDataFim=' + encodeURI(pesquisaDataFim.format('YYYY/MM/DD'));
            }
            parametros += '&pesquisaTexto=' + encodeURI(pesquisaTexto);
            parametros += '&ordenacao=' + encodeURI(ordenacao);
            parametros += '&pagina=' + encodeURI(pagina);
            $('#kanban #' + status).closest('.kanban-coluna').find('.kanban-coluna-cabecalho-contador').text('');
            $('#kanban #' + status).closest('.kanban-coluna').find('.kanban-coluna-cabecalho-carregando').removeClass('hidden');

            $.ajax({
                data: parametros,
                url: './taskboard-service.asmx/obter-tarefas-pesquisa-ordenacao',
                type: 'POST'
            }).done(function (data, textStatus, jqXHR) {
                $('#kanban #' + status).closest('.kanban-coluna').find('.kanban-coluna-cabecalho-carregando').addClass('hidden');
                if (
                    ($('#kanban #todo').closest('.kanban-coluna').find('.kanban-coluna-cabecalho-carregando').hasClass('hidden')) &&
                    ($('#kanban #doing').closest('.kanban-coluna').find('.kanban-coluna-cabecalho-carregando').hasClass('hidden')) &&
                    ($('#kanban #done').closest('.kanban-coluna').find('.kanban-coluna-cabecalho-carregando').hasClass('hidden'))
                )
                {
                    carregando(false);
                }
                var itens = getDataItems(data);
                if (status == 'done') {
                    if (paginacao === undefined) {
                        if (itens.length == 0) {
                            $('#kanban #' + status).data('total', itens.length);
                        }
                        else {
                            $('#kanban #' + status).data('total', itens[0]['TotalTarefasConcluidas']);
                        }
                    }
                }
                kanbanAdicionaCards(status, itens, complemento);
            });
        });
    }

    function kanbanAdicionaCards(status, itens, complemento) {
        if ((status == 'done') && (complemento !== undefined) && (complemento === true)) {
            var c = $('#' + status).children().length;
            if (c < 10) {
                for (var i = 9; i >= c; i--) {
                    var item = itens[i];
                    var card = kanbanCard(item);
                    document.querySelector('#' + status).appendChild(card);
                }
                kanbanAtualizaTotalCards(status);
            }
        }
        else {
            for (var i = 0; i < itens.length; i++) {
                var item = itens[i];
                var card = kanbanCard(item);
                document.querySelector('#' + status).appendChild(card);
            }
            kanbanAtualizaTotalCards(status);
        }
    }

    function kanbanAtualizaCard(status, id, callback) {
        carregando();
        $.ajax({
            data: 'status=' + status + '&codigoAtribuicao=' + id,
            url: './taskboard-service.asmx/salvar-tarefa-novo',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {
            var item = getDataItems(data)[0];
            var card = document.getElementById(id);
            var novoCard = kanbanCard(item);
            if (card.parentElement.getAttribute('id') == item.Status) {
                card.parentElement.replaceChild(novoCard, card);
                kanbanAtualizaTotalCards();
            }
            else {
                var titulo = item.Descricao;
                var nomeStatusAprovacao = '';
                if (card.querySelectorAll('div.kanban-item-situacao').length > 0) {
                    [].forEach.call(card.querySelectorAll('div.kanban-item-situacao'), function (itemSituacao) {
                        var sigla = itemSituacao.getAttribute('sigla').toString();
                        if (sigla != 'AT') {
                            nomeStatusAprovacao = kanbanNomeStatusAprovacao(sigla, item.Status);
                        }
                    });
                }
                var nomeStatus = kanbanNomeStatus(item.Status);
                setTimeout(function () {
                    $('#kanbanModalMensagem').html(sprintf(traducao.kanban_MensagemAlteracaoStatusAprovacao, titulo, nomeStatusAprovacao, nomeStatus));
                    $('#kanbanModal').modal();
                    $('#kanbanModal').off('hidden.bs.modal');
                    $('#kanbanModal').on('hidden.bs.modal', function (e) {
                        $(card).fadeOut('slow', function () {
                            $(card).remove();
                            $('#' + item.Status).prepend(novoCard).fadeIn('slow');
                            if (item.Status == 'done') {
                                var total = $('#' + item.Status).data('total');
                                total++;
                                $('#' + item.Status).data('total', total);
                            }
                            else if (status == 'done') {
                                var total = $('#' + status).data('total');
                                total--;
                                $('#' + status).data('total', total);
                            }
                        });
                        //location.reload();
                    });
                }, 1000);
            }
            if (callback !== undefined) {
                callback();
            }
            kanbanAtualizaTotalCards();
            carregando(false);
        });
    }

    function kanbanEditaCard(status, id, idprojeto) {

        $.ajax({
           /* data: 'codigoAtribuicao=' + id,*/
            url: './taskboard-service.asmx/obter-parametro-usa-historico-oc',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {

            var urlEditaCard = (parseInt(id, 10) < 0) ? '../espacoTrabalho/DetalhesTDL.aspx?CTDL=' + id : '../espacoTrabalho/DetalhesTS.aspx?CA=' + id;

            var indicaTelaHistoricoOutrosCustos = JSON.parse(data.firstElementChild.innerHTML)[0].Column1;
            if ((indicaTelaHistoricoOutrosCustos == true) && (parseInt(id, 10) >= 0)) {

                urlEditaCard = '../espacoTrabalho/DetalhesTSHistoricoOC.aspx?CA=' + id + '&CP=' + idprojeto;
            }
            if (status == 'done') {
                urlEditaCard += '&B=S';
            }
            var funcaoEditaCard = function () {
                carregando();
                $.ajax({
                    data: 'codigoAtribuicao=' + id,
                    url: './taskboard-service.asmx/obter-detalhes-tarefa',
                    type: 'POST'
                }).done(function (data, textStatus, jqXHR) {
                    var item = getDataItems(data)[0];
                    var card = document.getElementById(id);
                    var novoCard = kanbanCard(item);
                    if (status == item.Status) {
                        card.parentElement.replaceChild(novoCard, card);
                        kanbanAtualizaTotalCards();
                    }
                    else {
                        setTimeout(function () {
                            $('#kanbanModalMensagem').html(sprintf(traducao.kanban_MensagemAlteracaoStatus, kanbanNomeStatus(item.Status)));
                            $('#kanbanModal').modal();
                            $('#kanbanModal').off('hidden.bs.modal');
                            $('#kanbanModal').on('hidden.bs.modal', function (e) {
                                $(card).fadeOut('slow', function () {
                                    $(card).remove();
                                    $('#' + item.Status).prepend(novoCard).fadeIn('slow');


                                    if (item.Status == 'done') {





                                        var total = $('#' + item.Status).data('total');
                                        total++;
                                        $('#' + item.Status).data('total', total);
                                    }
                                    else if (status == 'done') {
                                        var total = $('#' + status).data('total');
                                        total--;
                                        $('#' + status).data('total', total);
                                        if ($('#' + status).children().length < 10) {
                                            kanbanCarregaCards(status, 1, true);
                                        }
                                    }
                                    kanbanAtualizaTotalCards();
                                });
                                //location.reload();
                            });
                        }, 1000);
                    }
                    carregando(false);
                });
            };
            //alert(urlEditaCard);
            window.top.showModalComFooter(urlEditaCard, traducao.kanban_Edicao, null, null, funcaoEditaCard, null);

            //alert(data);

        }).fail(function (jqXHR, textStatus, msg) {

        });


        
    }

    function kanbanAtualizaTotalCards(status) {
        switch (status) {
            case 'todo':
            case 'doing':
            case 'done':
                {
                    var arrayStatus = [status];
                    break;
                }
            default:
                {
                    var arrayStatus = ['todo', 'doing', 'done'];
                }
        }
        arrayStatus.forEach(function (status, indice, array) {
            $('#' + status).each(function () {
                if (status == 'done') {
                    var total = $(this).data('total');
                }
                else {
                    var total = $(this).children().length;
                }
                $(this).closest('.col').find('.kanban-coluna-cabecalho-contador').text(' (' + total + ((total == 1) ? ' ' + traducao.kanban_Item : ' ' + traducao.kanban_Itens) + ')');
            });
        });
        kanbanAtualizaMensagem();
    }

    function kanbanAtualizaMensagem() {
        var totalTarefasNaoConcluidas = $('#todo').children().length;
        var totalTarefas = 0;
        totalTarefas += $('#todo').children().length;
        totalTarefas += $('#doing').children().length;
        totalTarefas += parseInt($('#done').data('total'), 10);
        var mensagem = traducao.kanban_MensagemExibicao.replace('{0}', totalTarefasNaoConcluidas).replace('{1}', totalTarefas).replace('{2}', "Ok");
        $('#popKanban').attr('data-content', mensagem);
        /*
        $('p.kanban-mensagem').html(mensagem);
        $('p.kanban-mensagem').removeClass('hide');
        */
    }

    function kanbanCard(item) {

        var html = '';

        var cor = kanbanCor(item.Status);

        var nomeStatusAprovacao = kanbanNomeStatusAprovacao(item.StatusAprovacao, item.Status);

        if (item.InicioReal == null) {
            var diaInicio = htmlEncode(item.Inicio.dia(true));
            var mesInicio = htmlEncode(traducao.kanban_Meses[parseInt(item.Inicio.mes(true), 10) - 1]);
            var anoInicio = htmlEncode(item.Inicio.ano(true));
            var inicio = 'Início Previsto: ' + item.Inicio;
        }
        else {
            var diaInicio = htmlEncode(item.InicioReal.dia(true));
            var mesInicio = htmlEncode(traducao.kanban_Meses[parseInt(item.InicioReal.mes(true), 10) - 1]);
            var anoInicio = htmlEncode(item.InicioReal.ano(true));
            var inicio = 'Início Real: ' + item.InicioReal;
        }

        if (item.TerminoReal == null) {
            var diaTermino = htmlEncode(item.Termino.dia(true));
            var mesTermino = htmlEncode(traducao.kanban_Meses[parseInt(item.Termino.mes(true), 10) - 1]);
            var anoTermino = htmlEncode(item.Termino.ano(true));
            var termino = 'Término Previsto: ' + item.Termino;
        }
        else {
            var diaTermino = htmlEncode(item.TerminoReal.dia(true));
            var mesTermino = htmlEncode(traducao.kanban_Meses[parseInt(item.TerminoReal.mes(true), 10) - 1]);
            var anoTermino = htmlEncode(item.TerminoReal.ano(true));
            var termino = 'Término Real: ' + item.TerminoReal;
        }

        // kanban-item-dados
        html += '<div class="kanban-item-dados">';

        // kanban-item-dados-periodo
        html += '<div class="kanban-item-dados-periodo">';

        html += '<div class="kanban-item-dados-periodo-data-inicio" title="' + htmlEncode(inicio) + '">';
        html += '<span class="kanban-item-dados-periodo-data-inicio-dia">' + htmlEncode(diaInicio) + '</span>';
        html += '<span class="kanban-item-dados-periodo-data-inicio-mes">' + htmlEncode(mesInicio) + '</span>';
        html += '</div>';

        html += '<div class="kanban-item-dados-periodo-data-termino" title="' + htmlEncode(termino) + '">';
        html += '<span class="kanban-item-dados-periodo-data-termino-dia">' + htmlEncode(diaTermino) + '</span>';
        html += '<span class="kanban-item-dados-periodo-data-termino-mes">' + htmlEncode(mesTermino) + '</span>';
        html += '</div>';

        html += '</div>'; // kanban-item-dados-periodo

        // kanban-item-dados-detalhes
        html += '<div class="kanban-item-dados-detalhes">';

        html += '<div class="kanban-item-dados-detalhes-titulo kanban-item-dados-detalhes-titulo-' + ((pesquisaModoExibicaoCards == 'C') ? 'completo' : 'simples') + '">' + htmlEncode(item.Descricao) + '</div>';
        if (pesquisaModoExibicaoCards == 'C') // Card Completo
        {
            html += '<div class="kanban-item-dados-detalhes-titulo-separador"></div>';
            html += '<div class="kanban-item-dados-detalhes-categoria">' + htmlEncode(item.DescricaoObjetoSuperior) + '</div>';
        }

        html += '</div>'; // kanban-item-dados-detalhes

        html += '</div>'; // kanban-item-dados


        // kanban-item-indicadores
        html += '<div class="kanban-item-indicadores">';

        // kanban-item-indicadores-situacoes
        html += '<div class="kanban-item-indicadores-situacoes">';
        if (item.IndicaAtrasada) {
            html += '<div class="kanban-item-indicadores-situacoes-situacao kanban-cor-vermelho" sigla="AT">';
            html += '<i class="far fa-dot-circle ico-circle"></i>';
            html += htmlEncode(traducao.kanban_StatusAprovacao['AT']);
            html += '</div>';
        }
        if (nomeStatusAprovacao != "") {
            html += '<div class="kanban-item-indicadores-situacoes-situacao kanban-cor-' + cor + '-claro" sigla="' + item.StatusAprovacao + '">';
            html += '<i class="far fa-dot-circle ico-circle"></i>';
            html += htmlEncode(nomeStatusAprovacao);
            html += '</div>';
        }
        html += '</div>'; // kanban-item-indicadores-situacoes

        // kanban-item-indicadores-opcoes
        html += '<div class="kanban-item-indicadores-opcoes">';
        html += '<i class="fas fa-pen-square kanban-item-indicadores-opcoes-editar kanban-cor-' + cor + '" title="' + traducao.kanban_EditarTarefa + '"></i>';
        if (item.Status == 'done')
        {
            if ((item.StatusAprovacao == 'AP') || (item._CodigoAtribuicao < 0)) {
                html += '<i class="fas fa-archive kanban-item-indicadores-opcoes-arquivar kanban-cor-' + cor + '" title="' + traducao.kanban_ArquivarTarefa + '"></i>';
            }
        }

        html += '</div>'; // kanban-item-indicadores-opcoes

        html += '</div>'; // kanban-item-indicadores

        if (item._CodigoAtribuicao < 0) {
            html += '<i class="fas fa-check-square kanban-icone-todo"></i>';
        }
        else if (item._CodigoAtribuicao > 0) {
            html += '<i class="fas fa-calendar-alt kanban-icone-schedule"></i>';
        }

        var li = document.createElement('li');
        li.setAttribute('id', item._CodigoAtribuicao);
        li.setAttribute('id-projeto', item.CodigoProjeto);
        li.innerHTML = html;
        li.classList.add('kanban-item');
        if (item._CodigoAtribuicao < 0) {
            li.classList.add('kanban-item-todo');
        }
        else if(item._CodigoAtribuicao > 0) {
            li.classList.add('kanban-item-schedule');
        }
        if (item.StatusAprovacao == 'AP') {
            if (item.CodigoAprovador == callbackSession.cp_IDUsuario) {
                li.classList.remove('bloqueado');
            }
            else {
                li.classList.add('bloqueado');
            }
        }
        else if ((item.StatusAprovacao == 'EA') || (item.StatusAprovacao == 'ER')) {
            li.classList.add('bloqueado');
        }
        else {
            li.classList.remove('bloqueado');
        }

        /*
        li.addEventListener('click', function (e) {
            var status = this.parentElement.getAttribute('id');
            var id = this.getAttribute('id');
            kanbanEditaCard(status, id);
        });
        */

       

        li.querySelector('.kanban-item-indicadores-opcoes-editar').addEventListener('click', function (e) {
            var status = this.parentElement.parentElement.parentElement.parentElement.getAttribute('id');
            var id = this.parentElement.parentElement.parentElement.getAttribute('id');
            var idprojeto = this.parentElement.parentElement.parentElement.getAttribute('id-projeto');

            kanbanEditaCard(status, id, idprojeto);
        });

        if (item.Status == 'done') {
            if ((item.StatusAprovacao == 'AP') || (item._CodigoAtribuicao < 0)) {
                li.querySelector('.kanban-item-indicadores-opcoes-arquivar').addEventListener('click', function (e) {
                    var status = this.parentElement.parentElement.parentElement.parentElement.getAttribute('id');
                    var id = this.parentElement.parentElement.parentElement.getAttribute('id');
                    $('#kanbanModalArquivar').data('id', id);
                    $('#kanbanModalArquivarCarregando').addClass('hidden');
                    $('#kanbanModalArquivamentoSucesso').addClass('hidden');
                    $('#kanbanModalArquivarErro').addClass('hidden');
                    $('#kanbanModalArquivarSim').attr('disabled', false);
                    $('#kanbanModalArquivarNao').attr('disabled', false);
                    $('#kanbanModalArquivar').modal({
                        backdrop: 'static'
                    });
                });
            }
        }

        li.addEventListener('dblclick', function (e) {
            var status = this.parentElement.getAttribute('id');
            var id = this.getAttribute('id');
            var idprojeto = this.getAttribute('id-projeto');
            kanbanEditaCard(status, id, idprojeto);
        });

        return (li);
    }

    /* Esta rotina merece/precisa ser revisada na questão de adicionar/remover os divs com/sem status. */
    function kanbanAtualizaStatusCards() {



        alert('kanbanAtualizaStatusCards');


        var status = 'done';
        var parametros = '';
        parametros += 'status=' + status;
        parametros += '&pesquisaExibirCardsAtrasados=0';
        if ((pesquisaDataInicio == null) || (pesquisaDataFim == null)) {
            parametros += '&pesquisaDataInicio=';
            parametros += '&pesquisaDataFim=';
        }
        else {
            parametros += '&pesquisaDataInicio=' + encodeURI(pesquisaDataInicio.format('YYYY/MM/DD'));
            parametros += '&pesquisaDataFim=' + encodeURI(pesquisaDataFim.format('YYYY/MM/DD'));
        }
        parametros += '&pesquisaTexto=' + encodeURI(pesquisaTexto);
        parametros += '&ordenacao=' + encodeURI(ordenacaoTodo);
        $.ajax({
            data: parametros,
            url: './taskboard-service.asmx/obter-tarefas-pesquisa-ordenacao',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {
            kanbanMensagemAtualizandoStatusCards(traducao.kanban_MensagemAtualizando);
            setTimeout(function () {
                kanbanMensagemAtualizaStatusCards(sprintf(traducao.kanban_MensagemAtualizacao, intervalAtualizaStatusCards));
            }, 3000);
            var itens = getDataItems(data);
            for (var i = 0; i < itens.length; i++) {
                var item = itens[i];
                var cor = kanbanCor(item.Status);
                var nomeStatusAprovacao = kanbanNomeStatusAprovacao(item.StatusAprovacao, item.Status);
                [].forEach.call(document.querySelectorAll('ul#' + item.Status + ' > li.kanban-item'), function (card) {
                    if (card.getAttribute('id').toString() == item._CodigoAtribuicao.toString()) {
                        if (card.querySelectorAll('div.kanban-item-indicadores-situacoes-situacao').length == 0) {
                            if (nomeStatusAprovacao != '') {
                                var div = document.createElement('div');
                                div.classList.add('kanban-item-indicadores-situacoes-situacao');
                                div.classList.add('kanban-cor-' + cor + '-claro');
                                div.setAttribute('sigla', htmlEncode(item.StatusAprovacao));
                                div.innerHTML = '<i class="far fa-dot-circle ico-circle"></i>' + htmlEncode(nomeStatusAprovacao);
                                if (item.StatusAprovacao == 'AP') {
                                    if (item.CodigoAprovador != callbackSession.cp_IDUsuario) {
                                        div.classList.add('bloqueado');
                                    }
                                }
                                else if ((item.StatusAprovacao == 'EA') || (item.StatusAprovacao == 'ER')) {
                                    div.classList.add('bloqueado');
                                }
                                else if (item.StatusAprovacao == 'RP') {
                                    kanbanAtualizaCard('todo', item._CodigoAtribuicao);
                                }
                                card.querySelector('div.kanban-item-indicadores-situacoes').appendChild(div);
                            }
                            if (item.IndicaAtrasada) {
                                var div = document.createElement('div');
                                div.classList.add('kanban-item-indicadores-situacoes-situacao');
                                div.classList.add('kanban-cor-vermelho');
                                div.setAttribute('sigla', 'AT');
                                div.innerHTML = '<i class="far fa-dot-circle ico-circle"></i>' + htmlEncode(traducao.atrasada);
                                card.querySelector('div.kanban-item-indicadores-situacoes').appendChild(div);
                            }
                        }
                        else {
                            var possuiStatusAprovacao = false;
                            var possuiStatusAtrasada = false;
                            [].forEach.call(card.querySelectorAll('div.kanban-item-indicadores-situacoes-situacao'), function (itemSituacao) {
                                if (itemSituacao.getAttribute('sigla').toString() == 'AT') {
                                    possuiStatusAtrasada = true;
                                    if (!item.IndicaAtrasada) {
                                        itemSituacao.remove();
                                    }
                                }
                                else {
                                    possuiStatusAprovacao = true;
                                    if (item.StatusAprovacao != itemSituacao.getAttribute('sigla')) {
                                        itemSituacao.setAttribute('sigla', item.StatusAprovacao);
                                        itemSituacao.innerHTML = nomeStatusAprovacao;
                                        itemSituacao.classList.add('pisca');
                                        if (item.StatusAprovacao == 'AP') {
                                            if (item.CodigoAprovador == callbackSession.cp_IDUsuario) {
                                                itemSituacao.classList.remove('bloqueado');
                                            }
                                            else {
                                                itemSituacao.classList.add('bloqueado');
                                            }
                                        }
                                        else if ((item.StatusAprovacao == 'EA') || (item.StatusAprovacao == 'ER')) {
                                            itemSituacao.classList.add('bloqueado');
                                        }
                                        else if (item.StatusAprovacao == 'RP') {
                                            kanbanAtualizaCard('todo', item._CodigoAtribuicao);
                                        }
                                        setTimeout(function () {
                                            itemSituacao.classList.remove('pisca');
                                        }, 2000);
                                    }
                                }
                            });
                            if ((item.IndicaAtrasada) && (!possuiStatusAtrasada)) {
                                var div = document.createElement('div');
                                div.classList.add('kanban-item-indicadores-situacoes-situacao');
                                div.classList.add('kanban-cor-vermelho');
                                div.setAttribute('sigla', 'AT');
                                div.innerHTML = '<i class="far fa-dot-circle ico-circle"></i>' + htmlEncode(traducao.atrasada);
                                card.querySelector('div.kanban-item-indicadores-situacoes').appendChild(div);
                            }
                            if ((nomeStatusAprovacao != '') && (!possuiStatusAprovacao)) {
                                var div = document.createElement('div');
                                div.classList.add('kanban-item-indicadores-situacoes-situacao');
                                div.classList.add('kanban-cor-' + cor + '-claro');
                                div.setAttribute('sigla', htmlEncode(item.StatusAprovacao));
                                div.innerHTML = '<i class="far fa-dot-circle ico-circle"></i>' + htmlEncode(nomeStatusAprovacao);
                                card.querySelector('div.kanban-item-indicadores-situacoes').appendChild(div);
                            }
                        }
                    }
                });
            }
        });
    }

    function kanbanMensagemAtualizaStatusCards(mensagem) {
        document.querySelector('.loader').style.display = 'none';
        document.querySelector('.kanban-aviso').innerHTML = mensagem;
        document.querySelector('.kanban-aviso').style.backgroundColor = '#ffffff';
        document.querySelector('.kanban-aviso').style.color = '#575757';
        document.querySelector('.kanban-aviso').classList.remove('hide');
    }

    function kanbanMensagemAtualizandoStatusCards(mensagem) {
        document.querySelector('.loader').style.display = 'block';
        document.querySelector('.kanban-aviso').innerHTML = mensagem;
        document.querySelector('.kanban-aviso').style.backgroundColor = '#ffffff';
        document.querySelector('.kanban-aviso').style.color = '#575757';
        document.querySelector('.kanban-aviso').classList.remove('hide');
    }

    function kanbanNomeStatus(status) {
        if (traducao.kanban_Status[status] == null) {
            return ('');
        }
        return (traducao.kanban_Status[status]);
    }

    function kanbanNomeStatusAprovacao(statusAprovacao, status) {
        if (traducao.kanban_StatusAprovacao[statusAprovacao] == null) {
            return ('');
        }
        // O status "Pendente de Envio" é o status padrão de uma tarefa de cronograma recém criada no Taskes
        // No Kanban esse status não deve ser exibido.
        if (statusAprovacao == 'PE') {
            return ('');
        }
        return (traducao.kanban_StatusAprovacao[statusAprovacao]);
    }

    function kanbanCor(status) {
        var cores = {
            'todo': 'verde',
            'doing': 'laranja',
            'done': 'azul'
        };
        if (cores[status] == null) {
            return ('verde');
        }
        return (cores[status]);
    }

    // Drag and Drop do jQuery UI.
    $('#todo, #doing, #done').sortable({
        cancel: 'ul#done > li.bloqueado',
        connectWith: '#todo, #doing, #done',
        helper: 'clone', /* BUG: https://forum.jquery.com/topic/jquery-ui-sortable-triggers-a-click-in-firefox-15 */
        placeholder: 'kanban-item-sombra',
        opacity: 0.7,
        beforeStop: function (ev, ui) {
            var status = ui.item.get(0).parentElement.getAttribute('id');
            if (($(ui.item).find('[sigla="AP"]').length > 0) && (ui.item.get(0).getAttribute('data-status') == 'done') && (status != 'done') && ($(ui.item).hasClass('bloqueado'))) {
                $(this).sortable('cancel');
            }
            else if (($(ui.item).find('[sigla="EA"]').length > 0) && (ui.item.get(0).getAttribute('data-status') == 'done') && (status != 'done')) {
                $(this).sortable('cancel');
            }
            else if (($(ui.item).find('[sigla="ER"]').length > 0) && (ui.item.get(0).getAttribute('data-status') == 'done') && (status != 'done')) {
                $(this).sortable('cancel');
            }
        },
        stop: function (event, ui) {
        },
        start: function (event, ui) {
            ui.item.get(0).setAttribute('data-status', ui.item.get(0).parentElement.getAttribute('id'));
        },
        receive: function (event, ui) {
            carregando();
            var status = ui.item.get(0).parentElement.getAttribute('id');
            var statusInicial = ui.item.get(0).getAttribute('data-status');
            if (status == 'done') {
                var total = $('#' + status).data('total');
                total++;
                $('#' + status).data('total', total);
            }
            else if (statusInicial == 'done') {
                var total = $('#' + statusInicial).data('total');
                total--;
                $('#' + statusInicial).data('total', total);
            }
            kanbanAtualizaTotalCards();
            var id = ui.item.get(0).getAttribute('id');
            ui.item.get(0).classList.remove('bloqueado');
            // Este setTimeout() é importante pois evita que dois itens sejam movidos simultaneamente por causa de algum "eventual" problema de sincronismo entre o Sortable do jQuery UI e o Ajax que atrasará a execução final do evento "receive". Precisa ser melhor compreendido.
            setTimeout(function () {
                if (statusInicial == 'done') {
                    kanbanAtualizaCard(status, id, function () {
                        if ($('#' + statusInicial).children().length < 10) {
                            kanbanCarregaCards(statusInicial, 1, true);
                        }
                        else {
                            kanbanAtualizaCard(status, id);
                        }
                    });
                }
                else {
                    kanbanAtualizaCard(status, id);
                }
            }, 500);
        },
        sort: function (event, ui) {
            $('.kanban-item-sombra').height(ui.item.outerHeight());
        }
    }).disableSelection();

    $('#atrasadasTodo').on('click', function (e) {
        kanbanCarregaCards('todo');
    });

    $('#atrasadasDoing').on('click', function (e) {
        kanbanCarregaCards('doing');
    });

    $('#popKanban').on('mouseleave', function (e) {
        $(this).popover('hide');
    });

    /* Hack para o EcmaScript */
    function insertAfter(newNode, referenceNode) {
        referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
    }

    /* HTML Encode versão jQuery */
    function htmlEncode(value) {
        return $('<div/>').text(value).html();
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

    //var intervalAtualizaStatusCards = 2; // Expresso em minutos (Default: 2 (minutos)).

    //kanbanMensagemAtualizaStatusCards(sprintf(traducao.kanban_MensagemAtualizacao, intervalAtualizaStatusCards));

    // Carrega os cards de cada uma das "raias" ou status do Kanban.
    kanbanCarregaCards();

    // Inicia um temporizador para, de 2 em 2 minutos, atualizar os status dos cards do Kanban.
    /**
     * Guilherme Cruz - 18/02/2019 - Estou desativando esta função, pois ao implantar o Lazy Load nas raias,
     * passa a ser inviável a obtenção das tarefas que estão renderizadas na tela de modo a atualizar seus
     * respectivos status.
     */
    /*
    var timer = setInterval(function () {
        kanbanAtualizaStatusCards();
    }, intervalAtualizaStatusCards * 60000);
    */

    function carregando(desligar) {
        $('#popKanban').addClass('hidden');
        if (desligar === false) {
            $('#kanban_carregando').addClass('hidden');
            $('#popKanban').removeClass('hidden');
        }
        else {
            $('#kanban_carregando').width($('#kanban').width());
            $('#kanban_carregando').height($('#kanban').height());
            $('#kanban_carregando').removeClass('hidden');
        }
    }

    function getDataItems(data, returnAsJson) {

        if ((returnAsJson === undefined) || (returnAsJson === null)) {
            returnAsJson = true;
        }

        var itens = null;
        var objeto = null;

        if (data.firstElementChild !== undefined) {
            objeto = data.firstElementChild;
        }
        else if (data.firstChild !== undefined) {
            objeto = data.firstChild;
        }
        else if (data.children !== undefined) {
            if (data.children.constructor === Array) {
                if (data.children.length > 0) {
                    objeto = data.children[0];
                }
            }
        }

        if (objeto !== null) {
            if (objeto.innerHTML !== undefined) {
                itens = objeto.innerHTML;
            }
            else if (objeto.textContent !== undefined) {
                itens = objeto.textContent;
            }
        }

        if (returnAsJson) {
            if (itens !== null) {
                itens = JSON.parse(itens);
            }
        }

        return itens;
    }

    $('#kanbanModalArquivarSim').on('click', function (e) {
        $('#kanbanModalArquivarCarregando').removeClass('hidden');
        $('#kanbanModalArquivarSim').attr('disabled', true);
        $('#kanbanModalArquivarNao').attr('disabled', true);
        var id = $('#kanbanModalArquivar').data('id');
        var card = document.getElementById(id);
        $.ajax({
            data: 'status=done&codigoAtribuicao=' + id,
            url: './taskboard-service.asmx/arquivar-tarefa',
            type: 'POST'
        }).done(function (data, textStatus, jqXHR) {
            $('#kanbanModalArquivarCarregando').addClass('hidden');
            $('#kanbanModalArquivamentoSucesso').removeClass('hidden');
            setTimeout(function () {
                $(card).remove();
                $('#kanbanModalArquivar').modal('hide');
                $('#done').data('total', $('#done').data('total') - 1);
                kanbanAtualizaTotalCards('done');
            }, 2000);
        }).fail(function (data) {
            $('#kanbanModalArquivarCarregando').addClass('hidden');
            $('#kanbanModalArquivamentoSucesso').addClass('hidden');
            $('#kanbanModalArquivarErro').removeClass('hidden');
            $('#kanbanModalArquivarSim').attr('disabled', false);
            $('#kanbanModalArquivarNao').attr('disabled', false);
        });
    });

});
