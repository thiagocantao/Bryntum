$(document).ready(function () {
    var largura = $(window).width();
    var altura = $(window).height();
    var velocidade = $(':input[name="tempoTransicao"]').val();
    var numero = $("#galeriaImagens img").first().index();
    var proximo = numero + 1;
    var tempo;
    var alturaLargura2 = 25;
    var alturaTopoRoda1 = 50;
    var alturaTopoRoda2 = 80;
    $.esconderMenu = function () {
//        $("#conteudoRodapeGaleria, #conteudoTopoGaleria").animate({ 'opacity': 0.1 }, 'fast', function () {
//            $("#rodapeGaleria, #topoGaleria").animate({ 'height': 0, 'opacity': 0.1 }, 'fast');
//        });
        //$(".menuGaleria img").hide('fast');
    };
    $.mostrarMenu = function () {
//        $("#topoGaleria").animate({ 'height': alturaTopoRoda1, 'opacity': 0.6 }, 'slow');
//        $("#rodapeGaleria").animate({ 'height': alturaTopoRoda2, 'opacity': 0.6 }, 'slow', function () {
//            $("#conteudoRodapeGaleria, #conteudoTopoGaleria").animate({ 'opacity': 1 }, 'slow');
//        });
        //$(".menuGaleria img").show('slow');
    };
    $.posicaoMenu = function () {
        var yTopo2 = 0;
        var yRodape2 = altura - alturaTopoRoda2;
        var xDireita2 = largura - (alturaLargura2 * 3);
        var xEsquerda2 = alturaLargura2;
        var yDE2 = (altura - alturaLargura2) / 2;
        var xCentro2 = (largura - alturaLargura2) / 2
        $("#topoGaleria").css({ 'top': yTopo2, 'width': largura });
        $("#direitaGaleria").css({ 'top': yDE2, 'left': xDireita2 });
        $("#esquerdaGaleria").css({ 'top': yDE2, 'left': xEsquerda2, 'height': alturaLargura2, 'width': alturaLargura2 });
        $("#rodapeGaleria").css({ 'top': yRodape2 });
        $("#centroGaleria").css({ 'top': yDE2, 'left': xCentro2 });

    };
    $("#palco").mouseenter(function () {
        $.mostrarMenu();
    });
    $("#palco").mouseleave(function () {
        $.esconderMenu();
    });
    $.rodarImagem = function () {
        $("#galeriaImagens img").eq(proximo).css({ 'opacity': 1, 'z-index': 9, '-moz-opacity': 1, 'filter': 'alpha(opacity=100)' });
        $("#galeriaImagens img").eq(numero).animate({ 'opacity': 0, 'z-index': 0, '-moz-opacity': 0, 'filter': 'alpha(opacity=0)' }, 'slow');
        $("#galeriaImagens img").delay(500).eq(proximo).animate({ 'opacity': 1, 'z-index': 99, '-moz-opacity': 1, 'filter': 'alpha(opacity=100)' }, 'slow');
        $("#conteudoRodapeGaleria").html($("#galeriaImagens img").eq(numero).attr("alt"));
        $("#conteudoTopoGaleria").html($("#galeriaImagens img").eq(numero).attr("title"));
        if (numero >= $("#galeriaImagens img").last().index()) {
            numero = $("#galeriaImagens img").first().index();
            proximo = numero + 1;
        }
        else {
            numero = numero + 1;
            proximo = numero == $("#galeriaImagens img").last().index() ? $("#galeriaImagens img").first().index() : (numero + 1);
        }
    };

    $("#galeriaImagens img").css({ 'width': largura, 'height': altura });

    $("#palco:hidden, #galeriaImagens:hidden").show('slow').animate({ opacity: 1, width: largura, height: altura }, 'slow', function () {
        $("#galeriaImagens img").first().css({ 'opacity': 1, 'z-index': 99 });
        $.posicaoMenu();
        $.getScript("imagens/jquery.timer.js", function () {
            tempo = $.timer($.rodarImagem, velocidade, true);
        });
    });
    $(".menuGaleria img").mouseover(function () {
        $.mostrarMenu();
    });
    $("#centroGaleria").click(function () {
        $.playPause();
    });
    $.playPause = function () {
        if ($("#centroGaleria img").attr("alt") == "Pausar") {
            $("#centroGaleria img").attr("alt", "Iniciar");
            $("#centroGaleria img").attr("title", "Iniciar apresentação");
            $("#centroGaleria img").attr("src", "imagens/play.png");
            tempo.pause();
        } else {
            $("#centroGaleria img").attr("alt", "Pausar");
            $("#centroGaleria img").attr("title", "Pausar apresentação");
            $("#centroGaleria img").attr("src", "imagens/pause.png");
            tempo.play();
        }
    };
    $("#direitaGaleria").click(function () {
        if ($("#centroGaleria img").attr("alt") == "Pausar") {

            $.playPause();
        }
        $.rodarImagem();

    });
    $("#esquerdaGaleria").click(function () {
        if ($("#centroGaleria img").attr("alt") == "Pausar") {
            $.playPause();
        }
        if (numero == $("#galeriaImagens img").first().index()) {
            proximo = $("#galeriaImagens img").last().index();
        }
        else {
            proximo = numero - 1;
        }
        $.rodarImagem();

    });
});