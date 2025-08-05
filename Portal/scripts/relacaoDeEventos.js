function validaData() {
    var retorno = "";
    if (dteInicio.GetValue() != null && dteTermino.GetValue() != null) {
        
        var dataAtual = new Date();
        var meuDataAtual = (dataAtual.getMonth() + 1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
        var dataHoje = Date.parse(meuDataAtual);

        var dataInicio = new Date(dteInicio.GetValue());
        var meuDataInicio = (dataInicio.getMonth() + 1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
        dataInicio = Date.parse(meuDataInicio);


        var dataTermino = new Date(dteTermino.GetValue());
        var meuDataTermino = (dataTermino.getMonth() + 1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
        dataTermino = Date.parse(meuDataTermino);

        if (dataInicio > dataTermino) {
            retorno = traducao.relacaoDeEventos_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n";
        }
        return retorno;
    }
    return retorno;
}

function validaCampos() {
    var msg = "";

}


