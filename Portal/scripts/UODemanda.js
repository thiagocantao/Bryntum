var command;

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
    spnUO.SetEnabled(true);
    spnUO.SetText("");
}

function desabilitaHabilitaComponentes() {
    spnUO.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}

function validaCamposFormulario() {
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 1;

  
    if (spnUO.GetText() == "")
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.UODemanda_u_o__deve_ser_informada_ + "  \n";
    if (spnUO.GetText().search("_") != -1) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.UODemanda_valor_inv_lido__dica__complete_com_zeros_a_esquerda_ + "  \n";
    }

    return mensagemErro_ValidaCamposFormulario;
}

function MontaCamposFormulario(valores) {

    var values = valores[0];


    var CodigoUO = (values[0] != null ? values[0] : "");
    var NumeroProtocolo = (values[1] != null ? values[1] : "");
    var DataInclusao = (values[2] != null ? values[2] : "");
    var CodigoUsuarioInclusao = (values[3] != null ? values[3] : "");
    var DataUltimaAlteracao = (values[4] != null ? values[4] : "");
    var CodigoUsuarioUltimaAlteracao = (values[5] != null ? values[5] : "");
    var DataExclusao = (values[6] != null ? values[6] : "");
    var CodigoUsuarioExclusao = (values[7] != null ? values[7] : "");

    spnUO.SetText(CodigoUO);



    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    if (window.pcDados)
        grid.GetSelectedFieldValues('CodigoUO;NumeroProtocolo;DataInclusao;CodigoUsuarioInclusao;DataUltimaAlteracao;CodigoUsuarioUltimaAlteracao;DataExclusao;CodigoUsuarioExclusao;', MontaCamposFormulario);
}
