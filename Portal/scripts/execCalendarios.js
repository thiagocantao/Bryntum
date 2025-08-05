// JScript File
function validarHorario(hsInicio, hsFinal) {
    var vacioHs1 = false;
    var vacioHs2 = false;

    //testar qeu tenha prenchido os dados.
    if (hsInicio.GetText() == '__:__' || hsInicio.GetText() == ':' || hsInicio.GetText() == '') {
        vacioHs1 = true;
    }
    if (hsFinal.GetText() == '__:__' || hsFinal.GetText() == ':' || hsFinal.GetText() == '') {
        vacioHs2 = true;
    }

    if ((vacioHs1 != vacioHs2) && (vacioHs1 == true || vacioHs2 == true)) {

        return false; //um de los horarios esta vacio.
    }

    //testar qeu horario inicio seja menor ao horario final.
    var hora1 = hsInicio.GetText().split(':');
    var hora2 = hsFinal.GetText().split(':');

    //Obtener horas y minutos (hora 1)
    var hh1 = parseInt(hora1[0], 10);
    var mm1 = parseInt(hora1[1], 10);

    //Obtener horas y minutos (hora 2)
    var hh2 = parseInt(hora2[0], 10);
    var mm2 = parseInt(hora2[1], 10);

    if (hh1 > hh2) {

        return false; // hora inicio maior a hora final.
    }
    else if (hh1 == hh2) {
        if (mm1 > mm2) {

            return false; // horas iguis, mais minutos do inicio maior ao minutos final.
        }
    }
    return true
}

function validarTurno(hsTerminoAnteriorT, hsInicioProximoT) {
    //isNaN(123) devolverá False
    //isNaN("prueba") devolverá True

    //Agora vejo que o inicio do siguente turno possiu o horario igual o maior.
    var hora1 = hsTerminoAnteriorT.GetText().split(':');
    var hora2 = hsInicioProximoT.GetText().split(':');

    //Obtener horas y minutos (hora 1)
    var hh1 = parseInt(hora1[0], 10);
    var mm1 = parseInt(hora1[1], 10);

    //Obtener horas y minutos (hora 2)
    var hh2 = parseInt(hora2[0], 10);
    var mm2 = parseInt(hora2[1], 10);

    //si termino de turno anterio vacio, e inicio proximo turno prenchido, ERRO.
    if (!isNaN(hh2)) {
        if (isNaN(hh1)) {

            return false;
        }
    }

    if (hh1 > hh2) {

        return false; // hora termino de turno anterior maior a hora inicio do proximo turno.
    }
    else if (hh1 == hh2) {
        if (mm1 > mm2) {

            return false; // horas iguais, mais minutos do termino de turno anterior maior ao minutos inicio proximo turno.
        }
    }

    return true;
}

function onDateChange_txtDe(s, e) {
    var dataDe = new Date(s.GetValue());
    var dataAte = new Date(txtAte.GetValue());
    txtAte.SetMinDate(s.GetValue());

    if (dataDe > dataAte) {
        txtAte.SetValue(dataDe);
    }

    var diferencia = txtAte.GetDate() - s.GetDate();
    var cantDias = ((diferencia / 60000) / 60) / 24;

    if (cantDias < 7) {
        var numDia = s.GetDate().getDay();

        for (i = 0; i < numDia; i++) {
            var nomeTr = 'tr' + i;
            fila = document.getElementById(nomeTr);
            fila.style.display = 'none';
        }

        var numDiaFim = txtAte.GetDate().getDay();
        if (numDiaFim <= 6) {
            for (j = numDiaFim; j <= 6; j++) {
                var nomeTr = 'tr' + i;
                fila = document.getElementById(nomeTr);
                fila.style.display = 'none';
            }
        }
    }
}

function verificarTurnoPequenhos(horaInicio, horaFim) {
    var hora1 = horaInicio.GetText().split(':');
    var hora2 = horaFim.GetText().split(':');

    //Obtener horas y minutos (hora 1)
    var hh1 = parseInt(hora1[0], 10);
    var mm1 = parseInt(hora1[1], 10);

    //Obtener horas y minutos (hora 2)
    var hh2 = parseInt(hora2[0], 10);
    var mm2 = parseInt(hora2[1], 10);

    if (!isNaN(hh1)) {
        if (hh1 == hh2) {
            trocarFondo(horaInicio.name, true);
            trocarFondo(horaFim.name, true);
        }
        else {
            trocarFondo(horaInicio.name, false);
            trocarFondo(horaFim.name, false);
        }
    }
}


function trocarFondo(idObjeto, value) {
    if (value)
        document.getElementById(idObjeto).style.backgroundColor = "#FF9933";
    else
        document.getElementById(idObjeto).style.backgroundColor = "#FFFFFF";
}

function funcaoCallbackSalvar() {
    if (ASPxClientEdit.ValidateGroup('MKE')) {
        callback.PerformCallback();
    }
    else {
        window.top.mostraMensagem(traducao.execCalendarios_existem_pend_ncias_a_serem_corrigidas__n_n__descri__o_da_exce__o_deve_ser_informada__n__hor_rio_dentro_de_um_turno_tem_que_ser_preenchido__in_cio___fim__n__a_hora_de_in_cio_n_o_pode_ser_maior_que_a_hora_de_t_rmino_dentro_de_um_mesmo_turno_n__hora_de_in_cio_de_um_turno_significa_prencher_o_hor_rio_de_t_rmino_do_turno_anterior__n__hora_de_t_rmino_de_um_turno_n_o_pode_ser_maior_que_o_in_cio_do_pr_ximo_turno_, traducao.execCalendarios_atencao, true, false, null);
    }
}

function funcaoCallbackFechar() {
    window.parent.fechaEdicao();
}