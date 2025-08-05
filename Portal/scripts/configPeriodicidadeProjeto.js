var comando;
// JScript File
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
    spAno.SetValue(null);
    ckbAnoAtivo.SetValue("N");
    ckbAnoPeriodoEditavel.SetValue("N");
    ckbMetaEditavel.SetValue("N");
    ckbResultadoEditavel.SetValue("N");
    ddlTipoPeriodicidade.SetValue(null);

    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        //                                              0;             1;                       2;                 3;                      4;                                    5;                        6;
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'Ano;IndicaAnoAtivo;IndicaAnoPeriodoEditavel;IndicaMetaEditavel;IndicaResultadoEditavel;CodigoPeriodicidadeValoresFinanceiros;DescricaoPeriodicidade_PT;', MontaCamposFormulario);
    }

}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    var Ano = (values[0] != null ? values[0].toString() : "");
    var IndicaAnoAtivo = (values[1] != null ? values[1].toString() : "");
    var IndicaAnoPeriodoEditavel = (values[2] != null ? values[2].toString() : "");
    var IndicaMetaEditavel = (values[3] != null ? values[3].toString() : "");
    var IndicaResultadoEditavel = (values[4] != null ? values[4].toString() : "");
    var CodigoPeriodicidadeValoresFinanceiros = (values[5] != null ? values[5].toString() : "");
    var DescricaoPeriodicidade_PT = (values[6] != null ? values[6].toString() : "");

    spAno.SetValue(Ano);
    ckbAnoAtivo.SetValue(IndicaAnoAtivo);
    ckbAnoPeriodoEditavel.SetValue(IndicaAnoPeriodoEditavel);
    ckbMetaEditavel.SetValue(IndicaMetaEditavel);
    ckbResultadoEditavel.SetValue(IndicaResultadoEditavel);

    ddlTipoPeriodicidade.SetValue(CodigoPeriodicidadeValoresFinanceiros);
    ddlTipoPeriodicidade.SetText(DescricaoPeriodicidade_PT);
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 1;

    var meuData = new Date();
    var meuAnoAtual = meuData.getFullYear();

    var anoNovo = spAno.GetText();
    var anos = hfGeral.Get('hfAnos').toString();

    if (anoNovo.replace("_", "").length < 4)
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") Ano Inválido!\n";

    if ("Editar" != hfGeral.Get("TipoOperacao").toString())
        if (("" != anos) & (anos.indexOf(anoNovo) >= 0))
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") O Ano digitado não pode ser igual aos já existentes! \n";

    if (ddlTipoPeriodicidade.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") O tipo de periodicidade deve ser informado! \n";
    }
    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes() {
    var tipoOperacao = TipoOperacao; //hfGeral.Get("TipoOperacao").toString();

    if (("Consultar" == tipoOperacao) || ("" == tipoOperacao)) {
        spAno.SetEnabled(false);
        ckbAnoAtivo.SetEnabled(false);
        ckbAnoPeriodoEditavel.SetEnabled(false);
        ckbResultadoEditavel.SetEnabled(false);
        ckbMetaEditavel.SetEnabled(false);
        ddlTipoPeriodicidade.SetEnabled(false);
    }
    if (("Incluir" == tipoOperacao)) {
        spAno.SetEnabled(true);
        ckbAnoAtivo.SetEnabled(true);
        ckbAnoPeriodoEditavel.SetEnabled(true);
        ckbMetaEditavel.SetEnabled(true);
        ckbResultadoEditavel.SetEnabled(true);
        ddlTipoPeriodicidade.SetEnabled(true);
    }
    if (("Editar" == tipoOperacao)) {
        spAno.SetEnabled(false);
        ckbAnoAtivo.SetEnabled(true);
        ckbAnoPeriodoEditavel.SetEnabled(true);
        ckbMetaEditavel.SetEnabled(true);
        ckbResultadoEditavel.SetEnabled(true);
        ddlTipoPeriodicidade.SetEnabled(true);
    }
}//merge iel bahia