function validaCamposFormulario() {

    var countMsg = 1;
    var mensagemErro_ValidaCamposFormulario = "";

    //if (dtInicio.GetValue() == null) {
    //    mensagemErro_ValidaCamposFormulario += countMsg++ + ") Campo data de início deve ser informado!\n";
    //}
    //if (dtTermino.GetValue() == null) {
    //    mensagemErro_ValidaCamposFormulario += countMsg++ + ") Campo data de término deve ser informado!";
    //}
    if (dtInicio.GetValue() != null && dtTermino.GetValue() != null) {

        var dataAtual = new Date();
        var meuDataAtual = (dataAtual.getMonth() + 1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
        var dataHoje = Date.parse(meuDataAtual);

        var dataInicio = new Date(dtInicio.GetValue());
        var meuDataInicio = (dataInicio.getMonth() + 1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
        dataInicio = Date.parse(meuDataInicio);


        var dataTermino = new Date(dtTermino.GetValue());
        var meuDataTermino = (dataTermino.getMonth() + 1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
        dataTermino = Date.parse(meuDataTermino);

        /*if (dataInicio < dataHoje) {
            mensagemErro_ValidaCamposFormulario = "A Data de Início não pode ser anterior à data atual!\n";
        }
        if (dataTermino < dataHoje) {
            mensagemErro_ValidaCamposFormulario = "A Data de Término não pode ser anterior à data atual!\n";
        }*/
        if (dataInicio > dataTermino) {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.RelatorioSOE_senar_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n";
        }
        if (ddlOrigem.GetValue() == null) {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.RelatorioSOE_senar_a_origem_deve_ser_informada_ + "\n";
        }
    }
    return mensagemErro_ValidaCamposFormulario;
}