var CodigoCampoSelecionado = -1;

function mostraTela_CampoCalculado(CodigoCampo)
{
    CodigoCampoSelecionado = CodigoCampo;
}

function validarFormulaDigitada()//---[ metodos da formula
{
    var qtdeDados = gvd_CAL_CamposCalculaveis.GetVisibleRowsOnPage();
    var formulaValida = true;
    if (qtdeDados > 0) {
        executarFormulaDigitada();
        var formula = txt_CAL_Formula.GetText();
        var resultadoFormula = txt_CAL_ResultadoFormula.GetText();
        var formulaValida = (formula != "" && resultadoFormula != "erro" && resultadoFormula != "infinity");
    }
    else {
        txt_CAL_Formula.SetText("");
    }
    return formulaValida;
}

  
function executarFormulaDigitada() {
    var nomeGrid = gvd_CAL_CamposCalculaveis.name + "_cell";
    var qtdeLinhas = gvd_CAL_CamposCalculaveis.GetVisibleRowsOnPage();
    var formula = txt_CAL_Formula.GetText();
    formula = formula.toUpperCase();
    FormulaIndicador = formula.toUpperCase();
    for (i = qtdeLinhas; i > 0; i--) {
        var celula = 'B' + i;
        if (formula.indexOf(celula) >= 0) {
            var nomeCampo = nomeGrid + (i - 1) + "_3_" + "txtValorCampo_" + (i - 1) + "_I";
            var valorCelula = "";
            var txtValorCampo = document.getElementById(nomeCampo);
            if (txtValorCampo != null) {
                valorCelula = txtValorCampo.value.replace(',', '.');
                if (valorCelula == "") {
                    valorCelula = 1;
                    txtValorCampo.value = valorCelula;
                }
                formula = replaceAll(formula, celula, valorCelula);
                valorDado = gvd_CAL_CamposCalculaveis.keys[(i - 1)]; //(i-1)
                valorDado = valorDado.substr(valorDado.indexOf('|') + 1);
                FormulaIndicador = replaceAll(FormulaIndicador, celula, "[" + valorDado + "]");
            }
        }
    }
    //var resultado = eval(formula);
    var resultado = calculate(formula);
    if (resultado == "erro") {
        txt_CAL_ResultadoFormula.SetText("erro");
        window.top.mostraMensagem(traducao.uc_cfg_campoCalculado_a_f_rmula_digitada_cont_m_erros_, 'Atencao', true, false, null);
        return false;
    }
    else
        hf.Set("CAL_Formula", FormulaIndicador);

    txt_CAL_ResultadoFormula.SetValue(resultado.toString().replace('.', ','));
}