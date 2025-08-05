function onClick_CustomIncluirConta() {
    LimpaCamposFormulario();
    hfGeral.Set("TipoOperacao", "Incluir");
    desabilitaHabilitaComponentes();
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
    TipoOperacao = "Incluir";
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    //não esquecer de colocar a latitude e a longitude na função:  grid.GetRowValues
    //Recebe os valores dos campos de acordo com a linha selecionada. O resultado é passado para a função montaCampo
    
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'EntradaSaida;DescricaoConta;CodigoContaSuperior;TipoConta;CodigoReservadoGrupoConta;IndicaContaAnalitica;CodigoConta;CodigoContaSuperior;DescricaoContaSuperior;', MontaCamposFormulario);
}

function LimpaCamposFormulario() {
    
    ckbContaAnalitica.SetValue("N");
    txtDescricaoConta.SetText("");
    txtCodigoReservado.SetText("");
    ddlTipoConta.SetSelectedIndex(-1);
    ddlTipoConta.SetText("");
    ddlContaSuperior.SetSelectedIndex(-1);
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = hfGeral.Get("TipoOperacao") != "Consultar";

    rblEntradaSaida.SetEnabled(BoolEnabled);
    ckbContaAnalitica.SetEnabled(BoolEnabled);
    txtDescricaoConta.SetEnabled(BoolEnabled);
    txtCodigoReservado.SetEnabled(BoolEnabled);
    ddlTipoConta.SetEnabled(BoolEnabled);
    ddlContaSuperior.SetEnabled(BoolEnabled);
}

function MontaCamposFormulario(valores) {
    LimpaCamposFormulario();

    if (valores) {

        var EntradaSaida = valores[0];
        var DescricaoConta = valores[1];
        var CodigoContaSuperior = valores[2];
        var TipoConta = valores[3];
        var CodigoReservadoGrupoConta = valores[4];
        var IndicaContaAnalitica = valores[5];
        var CodigoConta = valores[6];
        var DescricaoContaSuperior = valores[8];

        hfGeral.Set("CodigoConta", CodigoConta);

        rblEntradaSaida.SetValue(EntradaSaida);
        ckbContaAnalitica.SetValue(IndicaContaAnalitica);
        txtDescricaoConta.SetText(DescricaoConta);
        txtCodigoReservado.SetText(CodigoReservadoGrupoConta);
        ddlTipoConta.SetValue(TipoConta);
        ddlContaSuperior.SetValue(CodigoContaSuperior);
        ddlContaSuperior.SetText(DescricaoContaSuperior);
    }
    desabilitaHabilitaComponentes();
}

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}


function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (trim(txtDescricaoConta.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.planoContas_descri__o_da_conta_deve_ser_informada_;
    }
    if (ddlTipoConta.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.planoContas_tipo_da_conta_deve_ser_informada_;
    }
    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}