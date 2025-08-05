function validaCamposFormulario() {
    var contador = 0;
    var mensagem = "";

    if (trim(txtLocal.GetText()) == "") {
        contador++;
        mensagem += "\n" + contador + ") " + traducao.configSprint_o_local_deve_ser_informado_ + "\n";
    }
    if (txtHoraInicioReal.GetValue() == null || txtHoraInicioReal.GetText() == "") {
        contador++;
        mensagem += contador + ") " + traducao.configSprint_o_hor_rio_de_in_cio_da_reuni_o_deve_ser_informado_ + "\n";
    }
    if (txtHoraTerminoReal.GetValue() == null || txtHoraTerminoReal.GetText() == "") {
        contador++;
        mensagem += contador + ") " + traducao.configSprint_o_hor_rio_de_t_rmino_da_reuni_o_deve_ser_informado_ + "\n";
    }
    if (txtHoraTerminoReal.GetValue() == txtHoraInicioReal.GetValue()) {
        contador++
        mensagem += contador + ") " + traducao.configSprint_o_hor_rio_de_in_cio_e_t_rmino_n_o_devem_ser_iguais_ + "\n";
    }
    if (rblTipoGrafico.GetValue() == null) {
        contador++
        mensagem += contador + ") " + traducao.configSprint_o_tipo_de_gr_fico_deve_ser_informado_ + "\n";
    }
    

    if (mensagem == "") {
        var valido = false;
        var array_HoraInicio = txtHoraInicioReal.GetValue().toString().split(':');
        var array_HoraFim = txtHoraTerminoReal.GetValue().toString().split(':');

        var hora_Inicio = array_HoraInicio[0];
        var minuto_Inicio = array_HoraInicio[1];

        var hora_Fim = array_HoraFim[0];
        var minuto_Fim = array_HoraFim[1];
        if (hora_Inicio <= hora_Fim) {
            if (hora_Inicio == hora_Fim) {
                if (minuto_Inicio < minuto_Fim) {
                    valido = true;
                }
            }
            else if (hora_Inicio < hora_Fim) {
                valido = true;
            }
        }
        if (valido == false) {
            contador++;
            mensagem += contador + ") " + traducao.configSprint_o_hor_rio_de_in_cio___maior_que_o_hor_rio_de_t_rmino_ + "\n";
        }
    }
    return mensagem;
}
