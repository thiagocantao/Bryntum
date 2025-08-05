function Valida(s, e) {
    var valorInicial = parseFloat(txtValorInicial.GetText().replace(',','.'));
    var valorFinal = parseFloat(txtValorFinal.GetText().replace(',', '.'));
    var isValid = valorFinal > valorInicial;
    if (!isValid) {
        e.errorText = traducao.CadastroFaixasTolerancia_o_valor_final_deve_ser_maior_que_o_valor_inicial;
        e.isValid = false;
    }
}