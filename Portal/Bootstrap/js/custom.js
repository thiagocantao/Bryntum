function openNav() {
    document.getElementById("mySidenav").style.width = "70%";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

function openNavPerfil() {
    document.getElementById("mySidenavPerfil").style.width = "445px";
}

function closeNavPerfil() {
    document.getElementById("mySidenavPerfil").style.width = "0";
}

function openLegend() {
    document.getElementById("mySidenavLegend").style.width = "445px";
}

function closeLegend() {
    document.getElementById("mySidenavLegend").style.width = "0";
}

function openMenuMobile() {
    document.getElementById("myMenuMobile").style.width = "100%";
}

function closeMenuMobile() {
    document.getElementById("myMenuMobile").style.width = "0";
}


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

var timerFiltro;
var delayFiltro = 1000;

var timerFiltroMobile;
var delayFiltroMobile = 1000;

$(document).ready(function () {

    $('#home').on('click', function () {
        $('div.panel').animate({
            'width': 'toggle'
        }, 1000);
    });

    function filtro() {
        $('div#filtroMenuResultado').hide();
        var pesquisa = $('#filtroMenu').val().trim();
        if (pesquisa == "") {
            return;
        }
        var html = '';
        html += '<h5>' + traducao.menu_pesquisa_resultados + '</h5>';
        html += '<ul>';
        $('nav#menuPrincipal ul.menu, div#mySidenav').find('h3 + ul > li a').each(function (i, elemento) {
            var link = $(this).prop("href");
            var texto = $(this).text();
            var regex = new RegExp(pesquisa, "gi");
            var seoRegex = new RegExp(seo(pesquisa), "gi");
            if ((texto.match(regex) !== null) || (seo(texto).match(seoRegex) !== null)) {
                html += '<li>' + $(this).clone().get(0).outerHTML + '</li>';
            }
        });
        html += '</ul>';
        $('div#filtroMenuResultado').html(html);
        $('div#filtroMenuResultado').show();
        $('#filtroMenu').focus().select();
    }

    if ($('#filtroMenu').length > 0) {
        filtro();
    }

    $('#filtroMenu').on('input propertychange', function (e) {
        $('div#filtroMenuResultado').hide();
        clearTimeout(timerFiltro);
        timerFiltro = setTimeout(function () { filtro() }, delayFiltro);
    });

    $('#filtroMenu').on('keypress', function (e) {
        if (e.which == 13) {
            clearTimeout(timerFiltro);
            filtro();
            e.preventDefault();
        }
        else {
            clearTimeout(timerFiltro);
            timerFiltro = setTimeout(function () { filtro() }, delayFiltro);
        }
    });

    if ($('#filtroMenuMobile').length > 0) {
        filtroMobile();
    }

    function filtroMobile() {
        $('#filtroMenuMobileResultado').hide();
        var pesquisa = $('#filtroMenuMobile').val().trim();
        if (pesquisa == "") {
            return;
        }
        var html = '';
        html += '<h5>' + traducao.menu_pesquisa_resultados + '</h5>';
        html += '<ul>';
        $('nav#menuPrincipal ul.menu, div#mySidenav').find('h3 + ul > li a').each(function (i, elemento) {
            var link = $(this).prop("href");
            var texto = $(this).text();
            var regex = new RegExp(pesquisa, "gi");
            var seoRegex = new RegExp(seo(pesquisa), "gi");
            if ((texto.match(regex) !== null) || (seo(texto).match(seoRegex) !== null)) {
                html += '<li>' + $(this).clone().get(0).outerHTML + '</li>';
            }
        });
        html += '</ul>';
        $('div#filtroMenuMobileResultado').html(html);
        $('div#filtroMenuMobileResultado').show();
        $('#filtroMenuMobile').focus().select();
    }

    $('#filtroMenuMobile').on('input propertychange', function (e) {
        $('div#filtroMenuMobileResultado').hide();
        clearTimeout(timerFiltroMobile);
        timerFiltroMobile = setTimeout(function () { filtroMobile() }, delayFiltroMobile);
    });

    $('#filtroMenuMobile').on('keypress', function (e) {
        if (e.which == 13) {
            clearTimeout(timerFiltroMobile);
            filtroMobile();
            e.preventDefault();
        }
        else {
            clearTimeout(timerFiltroMobile);
            timerFiltroMobile = setTimeout(function () { filtroMobile() }, delayFiltroMobile);
        }
    });

    $("div[aria-labelledby=\"navbarDropdown\"] div.filtro-menu i.ico-close").on("click", function (e) {
        e.stopPropagation();
        $('#filtroMenu').val('');
        $('div#filtroMenuResultado').hide();
        clearTimeout(timerFiltro);
    });

    $("#myMenuMobile div.filtro-menu i.ico-close").on("click", function (e) {
        e.stopPropagation();
        $('#filtroMenuMobile').val('');
        $('div#filtroMenuMobileResultado').hide();
        clearTimeout(timerFiltroMobile);
    });

    $("div[aria-labelledby=\"navbarDropdown\"] > ul.menu > li").on("click", function (e) {
        //e.stopPropagation();
    });

    $("div[aria-labelledby=\"navbarDropdown\"] > ul.menu > li > a[href=\"#\"]").on("click", function (e) {
        //e.preventDefault();
    });

    $("i#estrelaFavoritos").on("click", function (e) {
        if ($(this).hasClass("disabled")) {
            return;
        }
        if ($(this).hasClass("far")) {
            try {
                pcNovoFavorito.Show();
            }
            catch (e) {
            }
        }
        else if ($(this).hasClass("fas")) {
            callbackFavoritos.PerformCallback();
        }
    });

    $('.click').click(function () {
        if ($('span').hasClass("fa-star")) {
            $('.click').removeClass('active')
            setTimeout(function () {
                $('.click').removeClass('active-2')
            }, 30)
            $('.click').removeClass('active-3')
            setTimeout(function () {
                $('span').removeClass('fa-star')
                $('span').addClass('fa-star-o')
            }, 15)
        } else {
            $('.click').addClass('active')
            $('.click').addClass('active-2')
            setTimeout(function () {
                $('span').addClass('fa-star')
                $('span').removeClass('fa-star-o')
            }, 150)
            setTimeout(function () {
                $('.click').addClass('active-3')
            }, 150)
            $('.info').addClass('info-tog')
            setTimeout(function () {
                $('.info').removeClass('info-tog')
            }, 1000)
        }
    });

    $('#navbarDropdown + .dropdown-menu').on('click', function (e) {
        //e.preventDefault();
        //e.stopPropagation();
    });

});

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
    if (statusParam == "A") {
        abreUrl('taskboard/TaskboardWrap.aspx?Atrasadas=S', '_top');
        //abreUrl('espacoTrabalho/frameEspacoTrabalho_TimeSheet.aspx?Atrasadas=S', '_top');
    }
    else {
        abreUrl('taskboard/TaskboardWrap.aspx', '_top');
        //abreUrl('espacoTrabalho/frameEspacoTrabalho_TimeSheet.aspx', '_top');
    }
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
    abreUrl("espacoTrabalho/frameEspacoTrabalho_AprovacoesTarefas.aspx", '_top');
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
    window.top.showModal(url, traducao.custom_mensagens, (parseInt(window.screen.width) - 80), (parseInt(window.screen.height) - 250), null);

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

if ($("#spanPontoVermelhoExistemMensagens").length > 0) {
    var timerMensagens = setInterval(function () {
        /*callbackPontoVermelhoExistemMensagens.PerformCallback();*/
    }, 60000);
}

$(function () {
    $('.closeMenu').click(function (event) {
        $('#navbarDropdown').dropdown('toggle');
        event.preventDefault();
    });

});

