function validaData() {
    var retorno = false;
    if (dteDe.GetValue() != null && dteAte.GetValue() != null) {

        var dataInicio = new Date(dteDe.GetValue());
        var meuDataInicio = (dataInicio.getMonth() + 1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
        dataInicio = Date.parse(meuDataInicio);

        var dataTermino = new Date(dteAte.GetValue());
        var meuDataTermino = (dataTermino.getMonth() + 1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
        dataTermino = Date.parse(meuDataTermino);

        if (dataInicio > dataTermino) {
            window.top.mostraMensagem(traducao.rel_AnaliseDePerformance_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n", 'Atencao', true, false, null);
        }
        else {
            retorno = true;
        }
    }
    else {

        if (dteDe.GetValue() == null && dteAte.GetValue() == null) {
            window.top.mostraMensagem(traducao.rel_AnaliseDePerformance_a_data_de_in_cio_e_a_data_de_t_rmino_devem_ser_informadas_, 'Atencao', true, false, null);
        }
        if (dteDe.GetValue() == null) {
            window.top.mostraMensagem(traducao.rel_AnaliseDePerformance_a_data_de_in_cio_deve_ser_informada_, 'Atencao', true, false, null);
        }
        else if (dteAte.GetValue() == null) {
            window.top.mostraMensagem(traducao.rel_AnaliseDePerformance_a_data_de_t_rmino_deve_ser_informada_, 'Atencao', true, false, null);
        }
    }
    return retorno;
}