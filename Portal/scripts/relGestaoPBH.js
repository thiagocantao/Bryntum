function novaDescricao() {
    timedCount(0);
}

function timedCount(tempo) {
    if (tempo == 3) {
        tempo = 0;
        tlDados.PerformCallback();
    }
    else {
        tempo++;
        t = setTimeout("timedCount(" + tempo + ")", 1000);
    }
}