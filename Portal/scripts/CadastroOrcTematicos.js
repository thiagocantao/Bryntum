var comando;

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}


function LimpaCamposFormulario() {
    var parametro = "-1";
    gvDotacoes.PerformCallback(parametro);
    txtDescricaoOrcamento.SetEnabled(true);
    txtDescricaoOrcamento.SetText("");    
}

function desabilitaHabilitaComponentes() {
    txtDescricaoOrcamento.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}

function validaCamposFormulario() {
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 1;

    if (txtDescricaoOrcamento.GetText() == null || txtDescricaoOrcamento.GetText() == "")
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.CadastroOrcTematicos_descri__o_do_or_amento_tem_tico_deve_ser_informado_ + "\n";

    return mensagemErro_ValidaCamposFormulario;
}

function MontaCamposFormulario(valores) {
    //grid.GetSelectedFieldValues('CodigoOrcamentoTematico;DescricaoOrcamentoTematico;CodigoEntidade;', MontaCamposFormulario);
    var values = valores[0];

    var CodigoOrcamentoTematico = (values[0] != null ? values[0] : "");
    var DescricaoOrcamentoTematico = (values[1] != null ? values[1] : "");
    var CodigoEntidade = (values[2] != null ? values[2] : "");
    gvDotacoes.PerformCallback(CodigoOrcamentoTematico);
    txtDescricaoOrcamento.SetText(DescricaoOrcamentoTematico);
    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    if (window.pcDados)
        grid.GetSelectedFieldValues('CodigoOrcamentoTematico;DescricaoOrcamentoTematico;CodigoEntidade;', MontaCamposFormulario);
}
