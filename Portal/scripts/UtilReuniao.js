$(document).ready(function () {
    window.top.teste = 'abc';
    console.log(window.top.teste);
});

function AjustaFonte(ajuste) {
    var funcao = function () {
        var anterior = $(this).css("font-size");
        anterior = anterior.replace("px", "");
        var novo = parseInt(anterior) + ajuste;
        if (novo < 10)
            novo = 10;
        else if (novo > 100)
            novo = 100;
        $(this).css("font-size", +novo + "px");
    }
    $(document).children().first().find(".dxgv, .dxnb-ghtext, .legendas, textarea, *[class*='dxeBase']").each(funcao);
    $(document).children().first().find("td[class*='dxgvHeader']").each(function () {
        $(this).find('td').each(funcao);
    });
}


//            $(window.frames[0].window.frames[0].window.document).children().first().find(".dxgv, .dxnb-ghtext, .dxeBase, textarea").each(funcao);
//            $(window.frames[0].window.frames[0].window.document).children().first().find("td[class*='dxgvHeader']").each(function () {
//                $(this).find('td').each(funcao);
//            });