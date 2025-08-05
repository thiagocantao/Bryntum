/* Bootstrap Menu does not close when clicking in an iframe */

$(window).on('blur', function () {
    $('#menuPrincipal > ul > li.dropdown').removeClass('show');
    $('#menuPrincipal > ul > li.dropdown > [data-toggle="dropdown"]').attr('aria-expanded', 'false');
    $('#menuPrincipal > ul > li.dropdown .dropdown-menu').removeClass('show');
});

/* #################### */
/* Função geral do Menu */
/* #################### */


$('#menuPrincipalFavoritos').on('shown.bs.dropdown', function (e) {
    if ($(window).width() >= 992) {
        setTimeout(function () {
            $('#menuPrincipalFavoritosOpcoes').hide();
            $('#menuPrincipalFavoritosOpcoes').show("slow", function () { });
        }, 0);
    }
});



    $('#menuPrincipalModulos').on('shown.bs.dropdown', function (e) {
        if ($(window).width() >= 992) {
            setTimeout(function () {
                $('#pesquisaModulos').focus();
            }, 500);
        }
    });

    $('#menuPrincipalAdministracao').on('shown.bs.dropdown', function (e) {
        if ($(window).width() >= 992) {
            setTimeout(function () {
                $('#pesquisaAdministracao').focus();
            }, 500);
        }
    });

$('#menuPrincipal .dropdown-menu .dropdown-toggle').on('click', function (e) {

    if (!$(this).next().hasClass('show')) {
        $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
    }

    var $dropdownMenu = $(this).next(".dropdown-menu");
    $dropdownMenu.toggleClass('show');

    $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
        $('#menuPrincipal .dropdown-submenu .show').removeClass("show");
    });

    return false;
});

$('#menuPrincipal a[href], a.navbar-brand').on('click', function (e) {
    if ($(this).attr('href') == '#') {
        e.preventDefault();
    }
    else {
        $('.loading').css('display', 'block');
    }
});

/* ############### */
/* Função da Busca */
/* ############### */

var timerSearchModulos;
var intervalSearchModulos = 1000; // 1 segundo

var timerSearchAdministracao;
var intervalSearchAdministracao = 1000; // 1 segundo

function seo(input, separator) {
    if (separator == null) {
        separator = "-";
    }
    input = input.replace(/^\s+|\s+$/g, '');
    input = input.toLowerCase();
    var from = 'ÀÁÂÃÄÅàáâãäåÒÓÔÕÕÖØòóôõöøÈÉÊËèéêëðÇçÐÌÍÎÏìíîïÙÚÛÜùúûüÑñŠšŸÿýŽž';
    var to = "AAAAAAaaaaaaOOOOOOOooooooEEEEeeeeeCcDIIIIiiiiUUUUuuuuNnSsYyyZz";
    for (var i = 0, l = from.length; i < l; i++) {
        input = input.replace(new RegExp(from.charAt(i), "g"), to.charAt(i));
    }
    input = input.replace(/[^a-z0-9 -]/g, "").replace(/\s+/g, separator).replace(/-+/g, separator);
    return (input);
}

function search(input)
{
    var searchResults = input.closest('.dropdown-menu').find('.nav-search-results');
    var searchPattern = input.val().trim();
    if (searchPattern == "") {
        return;
    }
    var html = '';
    input.closest('.dropdown-menu').find('.nav-submenu').find('a').each(function (i, a) {
        var href = $(a).prop("href");
        var text = $(a).text();
        var regex = new RegExp(searchPattern, "gi");
        var seoRegex = new RegExp(seo(searchPattern), "gi");
        if ((text.match(regex) !== null) || (seo(text).match(seoRegex) !== null)) {
            if ($(a).attr('href') != '#')
            {
                html += '<li>' + $(this).clone().get(0).outerHTML + '</li>';
            }
        }
    });
    if (html == '')
    {
        html = '<li>' + traducao.menu_pesquisa_nenhum_resultado_encontrado + '</li>';
    }
    searchResults.find('.nav-search-results-content').html(html);
    searchResults.find('.nav-search-results-content').find('a[href]').on('click', function(e) {
        $('.loading').css('display', 'block');
    });
}

$('#menuPrincipal #pesquisaModulos').on('keypress', function(e) {
    clearTimeout(timerSearchModulos);
    if (e.which == 13)
    {
        e.preventDefault();
        var input = $(this);
        input.closest('.dropdown-menu').find('.nav-search-results').find('.nav-search-results-content').html('');
        input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'none');
        input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'block');
        if (input.val().trim() != '')
        {
            search(input);
            input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'block');
            input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'none');
            if ($(window).width() > 1024)
            {
                input.focus().select();
            }
            else
            {
                input.trigger('blur');
            }
        }
    }
    else
    {
        timerSearchModulos = setTimeout(function () {
            search(input);
            input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'block');
            input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'none');
            if ($(window).width() > 1024)
            {
                input.focus().select();
            }
            else
            {
                input.trigger('blur');
            }
        }, intervalSearchModulos);
    }
});

$('#menuPrincipal #pesquisaModulos').on('input propertychange', function (e) {
    clearTimeout(timerSearchModulos);
    var input = $(this);
    input.closest('.dropdown-menu').find('.nav-search-results').find('.nav-search-results-content').html('');
    input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'none');
    input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'block');
    if (input.val().trim() != '')
    {
        timerSearchModulos = setTimeout(function () {
            search(input);
            input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'block');
            input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'none');
            if ($(window).width() > 1024)
            {
                input.focus().select();
            }
            else
            {
                input.trigger('blur');
            }
        }, intervalSearchModulos);
    }
});

$('#menuPrincipal #pesquisaAdministracao').on('keypress', function(e) {
    clearTimeout(timerSearchAdministracao);
    if (e.which == 13)
    {
        e.preventDefault();
        var input = $(this);
        input.closest('.dropdown-menu').find('.nav-search-results').find('.nav-search-results-content').html('');
        input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'none');
        input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'block');
        if (input.val().trim() != '')
        {
            search(input);
            input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'block');
            input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'none');
            if ($(window).width() > 1024)
            {
                input.focus().select();
            }
            else
            {
                input.trigger('blur');
            }
        }
    }
    else
    {
        timerSearchAdministracao = setTimeout(function () {
            search(input);
            input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'block');
            input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'none');
            if ($(window).width() > 1024)
            {
                input.focus().select();
            }
            else
            {
                input.trigger('blur');
            }
        }, intervalSearchAdministracao);
    }
});

$('#menuPrincipal #pesquisaAdministracao').on('input propertychange', function (e) {
    clearTimeout(timerSearchAdministracao);
    var input = $(this);
    input.closest('.dropdown-menu').find('.nav-search-results').find('.nav-search-results-content').html('');
    input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'none');
    input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'block');
    if (input.val().trim() != '')
    {
        timerSearchAdministracao = setTimeout(function () {
            search(input);
            input.closest('.dropdown-menu').find('.nav-search-results').css('display', 'block');
            input.closest('.dropdown-menu').find('.nav-submenu').css('display', 'none');
            if ($(window).width() > 1024)
            {
                input.focus().select();
            }
            else
            {
                input.trigger('blur');
            }
        }, intervalSearchAdministracao);
    }
});

$('#menuPrincipal .nav-search-results').on('click', function(e) {
    if ($(e.target).prop('tagName') == 'A')
    {
        $(this).closest('.dropdown-menu').find('.nav-search').find('input[type="text"]').val('');
        $(this).closest('.dropdown-menu').find('.nav-search-results-content').html('');
        $(this).closest('.dropdown-menu').find('.nav-search-results').css('display', 'none');
        $(this).closest('.dropdown-menu').find('.nav-submenu').css('display', 'block');
        $(this).closest('.dropdown-menu').find('.nav-search').find('input[type="text"]').select();
        e.stopPropagation();
    }
    else
    {
        e.preventDefault();
        e.stopPropagation();
    }
});

$('#menuPrincipal .nav-search-results-title > i').on('click', function(e) {
    e.preventDefault();
    e.stopPropagation();
    $(this).closest('.dropdown-menu').find('.nav-search').find('input[type="text"]').val('');
    $(this).closest('.dropdown-menu').find('.nav-search-results-content').html('');
    $(this).closest('.dropdown-menu').find('.nav-search-results').css('display', 'none');
    $(this).closest('.dropdown-menu').find('.nav-submenu').css('display', 'block');
});

/* #################### */
/* Função dos Favoritos */
/* #################### */

$("#menuPrincipalFavoritosAdicionar").on("click", function (e) {
    e.preventDefault();
    try {
        pcNovoFavorito.Show();
    }
    catch (e) {
    }
});

$("#menuPrincipalFavoritosRemover").on("click", function (e) {
    e.preventDefault();
    try {
        callbackFavoritos.PerformCallback();
    }
    catch (e) {
    }
});

/* ############################################# */
/* Funções das mensagens, notificações e tarefas */
/* ############################################# */

var timerMensagens = setInterval(function () {
    /*callbackPontoVermelhoExistemMensagens.PerformCallback();*/
}, 120000);

function abreUrl(url, target) {
    var link = document.createElement('a');
    url = jQuery("body").attr("data-baseUrl") + "/" + url;
    link.href = url;
    if ((target != null) && (target != "")) {
        link.target = target;
    }
    document.body.appendChild(link);
    link.click();
}

function abreTarefas003(statusParam) {
    var url = 'taskboard/TaskboardWrap.aspx';
    $('#menuPrincipal a').each(function () {
        var a = $(this);
        var i = a.prop('href').indexOf('taskboard/TaskboardWrap.aspx');
        if (i >= 0) {
            if (!(a.prop('href').indexOf('&') > 0 ||
                  a.prop('href').indexOf('#') > 0)) {
                url = a.prop('href').substr(i);
                return false;
            }
 
        }
    });
    if (statusParam == 'A') {
        if (url.indexOf('?') >= 0) {
            url += '&';
        }
        else {
            url += '?';
        }
        url += 'Atrasadas=S';
    }
    abreUrl(url, '_top');
}

function abreIndicadores003(statusParam) {
    if (statusParam == "A")
        abreUrl('_Estrategias/wizard/atualizacaoResultados.aspx?DiasVencimento=10&Atrasados=S&Filtro=NomeUsuario', '_top');
    else
        abreUrl('_Estrategias/wizard/atualizacaoResultados.aspx?DiasVencimento=10&Filtro=NomeUsuario', '_top');
}

function abreContratos003(statusParam) {
    if (statusParam == "A")
        abreUrl('_Projetos/Administracao/ParcelasContrato.aspx?Atrasados=S&Filtro=NomeUsuario', '_top');
    else
        abreUrl('_Projetos/Administracao/ParcelasContrato.aspx?Vencendo=S&Filtro=NomeUsuario', '_top');
}

function abreToDoList003(statusParam) {
    if (statusParam == "A")
        abreUrl('espacoTrabalho/frameEspacoTrabalho_MinhaTarefas.aspx?Estagio=Atrasada', '_top');
    else
        abreUrl('espacoTrabalho/frameEspacoTrabalho_MinhaTarefas.aspx?Estagio=', '_top');
}

function abreTarefas004() {
    abreUrl(hfLinkAprovacoes.Get("valor"), '_top');
}

function abreWorkflow004(statusParam, origem) {
    if (origem == "SGDA") {
        if (statusParam == "A")
            abreUrl("espacoTrabalho/PendenciasWorkflowSGDA.aspx?IP=S&ATR=S&PND=S", '_top');
        else
            abreUrl("espacoTrabalho/PendenciasWorkflowSGDA.aspx?IP=S&ATR=N&PND=S", '_top');
    }
    else {
        if (statusParam == "A")
            abreUrl("espacoTrabalho/PendenciasWorkflow.aspx?IP=S&ATR=S&PND=S", '_top');
        else
            abreUrl("espacoTrabalho/PendenciasWorkflow.aspx?IP=S&ATR=N&PND=S", '_top');
    }
}

function abreRiscos005(statusParam) {
    if (statusParam == "A")
        abreUrl('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?Cor=Vermelho&TipoTela=R', '_top');
    else
        abreUrl('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?TipoTela=R', '_top');
}

function abreIssues005(statusParam) {
    if (statusParam == "A")
        abreUrl('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?Cor=Vermelho&TipoTela=Q', '_top');
    else
        abreUrl('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?TipoTela=Q', '_top');
}

function abreContratos005(statusParam, qtdDias, varUsaContratoEstendido) {
    if (varUsaContratoEstendido == "S") {
        if (statusParam == "A")
            abreUrl('_Projetos/Administracao/ListaContratosEstendidos.aspx?Vencidos=S&ApenasMeusContratos=S', '_top');
        else
            abreUrl('_Projetos/Administracao/ListaContratosEstendidos.aspx?DiasVencimento=' + qtdDias + '&ApenasMeusContratos=S', '_top');
    }
    else {
        if (statusParam == "A")
            abreUrl('_Projetos/Administracao/Contratos.aspx?Vencidos=S&ApenasMeusContratos=S', '_top');
        else
            abreUrl('_Projetos/Administracao/Contratos.aspx?DiasVencimento=' + qtdDias + '&ApenasMeusContratos=S', '_top');
    }
}

function abreCompromissos006(statusParam) {
    if (statusParam == "A")
        abreUrl('espacoTrabalho/frameEspacoTrabalho_Agenda.aspx', '_top');
    else
        abreUrl('espacoTrabalho/frameEspacoTrabalho_Agenda.aspx', '_top');
}

function atualizaTela006(lParam) {
    window.location.reload();
}

function abreMensagensNovas006(statusParam) {
    var url = $("body").attr("data-baseUrl") + "/" + "Mensagens/MensagensPainelBordo.aspx?EsconderBotaoNovo=S";
    window.top.showModal(url, traducao.menu_mensagens, (parseInt(window.screen.width) - 80), (parseInt(window.screen.height) - 250), null);

    fechaMenu();
}

function fechaMenu()
{
    var teste = $('#pcModal_PW-1').attr('style');
    //menuPrincipal
    $('#pcModal_PW-1').attr({ 'style': (teste + 'z-index: 300000;') });
}

function abreRespostas006(statusParam) {
    if (statusParam == "A") {
        var url = $("body").attr("data-baseUrl") + "/" + "_VisaoExecutiva/MensagensEnviadasNaoRespondidasExecutivo.aspx?Status=A";
        window.top.showModal(url, traducao.custom_mensagens, 960, 550, '', null);
    }
    else {
        var url = $("body").attr("data-baseUrl") + "/" + "_VisaoExecutiva/MensagensEnviadasNaoRespondidasExecutivo.aspx";
        window.top.showModal(url, traducao.custom_mensagens, 960, 550, '', null);
    }
}

/* ##################################### */
/* ### Funções específicas do Portal ### */
/* ##################################### */

function openSidenav(id) {
    document.getElementById(id).style.width = "445px";
}
function closeSidenav(id) {
    document.getElementById(id).style.width = "0";
}
