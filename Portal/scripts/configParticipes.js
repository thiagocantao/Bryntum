var comando;
var TipoOperacao;

function SalvarCamposFormulario() {
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);

    //LimpaCamposFormulario();
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************


function LimpaCamposFormulario() {
    // Função responsável por preparar os campos do formulário para receber um novo registr
    ddlParticipe.SetValue(null);
    ddlPapel.SetValue(null);
    ckbAtivo.SetChecked(true);
    spinLimite.SetValue(null);
    pnDdlParticipe.PerformCallback("-1|S");
    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    //debugger
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (window.pcDados) {
        //                                      0;         1;                   2;                      3;                   4;           5;                    6;                  7;                           8;                         9;
        //gr.GetSelectedFieldValues('CodigoPessoa;NomePessoa;CodigoPapelParticipe;DescricaoPapelParticipe;IndicaParticipeAtivo;DataInclusao;CodigoUsuarioInclusao;DataUltimaAlteracao;CodigoUsuarioUltimaAlteracao;PercentualLimiteUsoRecurso;', MontaCamposFormulario);
        grid.GetSelectedFieldValues('CodigoPessoa;NomePessoa;CodigoPapelParticipe;DescricaoPapelParticipe;IndicaParticipeAtivo;DataInclusao;CodigoUsuarioInclusao;DataUltimaAlteracao;CodigoUsuarioUltimaAlteracao;PercentualLimiteUsoRecurso;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    //ddlParticipe.SetValue(null);
    ddlPapel.SetValue(null);
    ckbAtivo.SetChecked(true);
    desabilitaHabilitaComponentes();

    var valores = values[0];

    var CodigoPessoa = (valores[0] != null ? valores[0].toString() : "");
    var NomePessoa = (valores[1] != null ? valores[1].toString() : "");

    var CodigoPapelParticipe = (valores[2] != null ? valores[2].toString() : "");
    var DescricaoPapelParticipe = (valores[3] != null ? valores[3].toString() : "");

    var IndicaParticipeAtivo = (valores[4] != null ? valores[4].toString() : "");
    var DataInclusao = (valores[5] != null ? valores[5].toString() : "");
    var CodigoUsuarioInclusao = (valores[6] != null ? valores[6].toString() : "");
    var DataUltimaAlteracao = (valores[7] != null ? valores[7].toString() : "");
    var CodigoUsuarioUltimaAlteracao = (valores[8] != null ? valores[8].toString() : "");
    var PercentualLimiteUsoRecurso = (valores[9] != null ? valores[9].toString() : "");

    ddlParticipe.SetValue(CodigoPessoa);
    ddlParticipe.SetText(NomePessoa);

    pnDdlParticipe.PerformCallback(CodigoPessoa + '|N');

    ddlPapel.SetValue(CodigoPapelParticipe);
    ddlPapel.SetText(DescricaoPapelParticipe);

    ckbAtivo.SetChecked(IndicaParticipeAtivo == "S");
    spinLimite.SetValue(PercentualLimiteUsoRecurso);

}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 1;

    if (ddlParticipe.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") O Partícipe deve ser informado! \n";
    }
    if (ddlPapel.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") O Papel deve ser informado!\n";
    }
    if (spinLimite.GetValue() != null) {
        var percentual = parseInt(spinLimite.GetText());

        if ((percentual < 1) || (percentual > 100)) {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") O % limite deve estar entre 1 e 100%!\n";
        }


    }
    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;
    ddlPapel.SetEnabled(BoolEnabled);
    ckbAtivo.SetEnabled(BoolEnabled);
    spinLimite.SetEnabled(BoolEnabled);
}