var comando_gvCampos = "";
function validaCamposTexto() {
    //debugger
    var retorno = true;
    var mensagemAlert = "";
    var qtdLinhas = parseInt(txt_TXT_Linhas.GetText());
    var qtdCaracteres = parseInt(txt_TXT_Tamanho.GetText());
    if (qtdLinhas > 10) {
        mensagemAlert += traducao.adm_CadastroFormularios_o_campo_texto_n_o_permite_mais_que_10_linhas + "\n";
        retorno = false;
    }
    if (qtdCaracteres > 4000) {
        mensagemAlert += traducao.adm_CadastroFormularios_o_campo_texto_n_o_permite_mais_que_4000_caracteres + "\n";
        retorno = false;
    }
    if (retorno == false) {
        window.top.mostraMensagem(mensagemAlert, 'erro', true, false, null);
    }
    return retorno;
}


function obtemValoresClientes(tipo) {
    if (tipo == "VAR") {
        hfControle.Set("VAR_tamanho", txt_Var_tamanho.GetText());
        hfControle.Set("VAR_mascara", txt_Var_mascara.GetText());
        hfControle.Set("VAR_padrao", txt_Var_padrao.GetText());
    }
    else if (tipo == "TXT") {
        hfControle.Set("TXT_tamanho", txt_TXT_Tamanho.GetText());
        hfControle.Set("TXT_linhas", txt_TXT_Linhas.GetText());
        hfControle.Set("TXT_padrao", txt_TXT_padrao.GetText());
        var TXT_Formatacao = -1
        if (rb_TXT_SemFormatacao.GetChecked())
            TXT_Formatacao = 0;
        else if (rb_TXT_FormatacaoSimples.GetChecked())
            TXT_Formatacao = 1;
        //else if (rb_TXT_FormatacaoAvancada.GetChecked())
        //    TXT_Formatacao = 2;
        hfControle.Set("TXT_formatacao", TXT_Formatacao);
    }
    else if (tipo == "NUM") {
        hfControle.Set("NUM_Minimo", txt_NUM_Minimo.GetText());
        hfControle.Set("NUM_Maximo", txt_NUM_Maximo.GetText());
        hfControle.Set("NUM_Precisao", ddl_NUM_Precisao.GetSelectedItem().value);
        hfControle.Set("NUM_Formato", ddl_NUM_Formato.GetSelectedItem().value);
        hfControle.Set("NUM_Agregacao", ddl_NUM_Agregacao.GetSelectedItem().value);
        hfControle.Set("NUM_padrao", txt_NUM_padrao.GetText());
    }
    else if (tipo == "LST") {
        hfControle.Set("LST_Opcoes", txt_LST_Opcoes.GetText());
        var LST_Formatacao = -1;
        if (rb_LST_Combo.GetChecked())
            LST_Formatacao = 0;
        else if (rb_LST_Radio.GetChecked())
            LST_Formatacao = 1;
        else if (rb_LST_Check.GetChecked())
            LST_Formatacao = 2;
        hfControle.Set("LST_Formatacao", LST_Formatacao);
        hfControle.Set("LST_Tamanho", txt_LST_tamanho.GetText());
        hfControle.Set("LST_padrao", txt_LST_padrao.GetText());
    }
    else if (tipo == "DAT") {
        var DAT_IncluirHora = "N";
        if (rb_DAT_Sim.GetChecked())
            DAT_IncluirHora = "S";

        var DAT_ValorInicial = "B";
        if (rb_DAT_Atual.GetChecked())
            DAT_ValorInicial = "A";

        hfControle.Set("DAT_IncluirHora", DAT_IncluirHora);
        hfControle.Set("DAT_ValorInicial", DAT_ValorInicial);
    }
    else if (tipo == "BOL") {
        hfControle.Set("BOL_TextoVerdadeiro", txt_BOL_TextoVerdadeiro.GetText());
        hfControle.Set("BOL_ValorVerdadeiro", txt_BOL_ValorVerdadeiro.GetText());
        hfControle.Set("BOL_TextoFalso", txt_BOL_TextoFalso.GetText());
        hfControle.Set("BOL_ValorFalso", txt_BOL_ValorFalso.GetText());
    }
    else if (tipo == "SUB") {
        hfControle.Set("SUB_CodigoFormulario", ddl_SUB_Formulario.GetSelectedItem().value);
    }
    else if (tipo == "CPD") {
        hfControle.Set("CPD_CampoPre", ddl_CPD_CampoPre.GetSelectedItem().value);
        hfControle.Set("CPD_Linhas", txt_CPD_Linhas.GetText());
        hfControle.Set("CPD_Tamanho", txt_CPD_tamanho.GetText());
    }
    else if (tipo == "LOO") {
        hfControle.Set("LOO_ListaPre", ddl_LOO_ListaPre.GetSelectedItem().value);
        hfControle.Set("LOO_Tamanho", txt_LOO_tamanho.GetText());
        var LOO_ApresentacaoLOV = "N"
        if (rb_LOO_Combo.GetChecked())
            LOO_ApresentacaoLOV = "N";
        else if (rb_LOO_Lov.GetChecked())
            LOO_ApresentacaoLOV = "S";
        hfControle.Set("LOO_ApresentacaoLOV", LOO_ApresentacaoLOV);
    }
    else if (tipo == "REF") {
        hfControle.Set("REF_ModeloFormulario", ddl_REF_ModeloFormulario.GetSelectedItem().value);
        hfControle.Set("REF_CampoFormulario", ddl_REF_CampoFormulario.GetSelectedItem().value);
        hfControle.Set("REF_SomenteLeitura", ddl_REF_SomenteLeitura.GetSelectedItem().value);
        hfControle.Set("REF_TituloExterno", ddl_REF_TituloExterno.GetSelectedItem().value);
        hfControle.Set("REF_TituloInterno", ddl_REF_TituloInterno.GetSelectedItem().value);
    }
    else if (tipo == "CAL") {
        hfControle.Set("CAL_Precisao", ddl_CAL_Precisao.GetSelectedItem().value);
        hfControle.Set("CAL_Formato", ddl_CAL_Formato.GetSelectedItem().value);
        hfControle.Set("CAL_Agregacao", ddl_CAL_Agregacao.GetSelectedItem().value);
        // o valor "CAL_Formula" será preenchido no final da função executaFormula
        hfControle.Set("CAL_Formula", txt_CAL_Formula.GetText());
    }
    else if (tipo == "VAO") {
        hfControle.Set("VAO_Codigo", ddl_VAO.GetSelectedItem().value);
    }
}

function validaFormula()//---[ metodos da formula
{
    var qtdeDados = gvd_CAL_CamposCalculaveis.GetVisibleRowsOnPage();
    var formulaValida = true;
    if (qtdeDados > 0) {
        executaFormula();
        var formula = txt_CAL_Formula.GetText();
        var resultadoFormula = txt_CAL_ResultadoFormula.GetText();
        formulaValida = (formula != "" && resultadoFormula != "erro" && resultadoFormula != "infinity");
    }
    else {
        txt_CAL_Formula.SetText("");
    }
    return formulaValida;
}

function executaFormula() {
    var nomeGrid = gvd_CAL_CamposCalculaveis.name + "_cell";
    var qtdeLinhas = gvd_CAL_CamposCalculaveis.GetVisibleRowsOnPage();
    var formula = txt_CAL_Formula.GetText();
    formula = formula.toUpperCase();
    FormulaIndicador = formula.toUpperCase();
    for (i = qtdeLinhas; i > 0; i--) {
        var celula = 'B' + i;
        if (formula.indexOf(celula) >= 0) {
            var numeroNomeGrid = nomeGrid.substring(nomeGrid.indexOf("CamposCalculaveis")).replace("CamposCalculaveis_", "").replace("_cell", "");
            var nomeCampo = nomeGrid + (i - 1) + "_3_" + numeroNomeGrid + "_txtValorCampo_" + (i - 1) + "_I";
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
        window.top.mostraMensagem(traducao.adm_CadastroFormularios_a_f_rmula_digitada_cont_m_erros_, 'erro', true, false, null);
        return false;
    }
    else
        hfControle.Set("CAL_Formula", FormulaIndicador);

    var precisao = ddl_CAL_Precisao.GetValue();
    var formato = ddl_CAL_Formato.GetValue();
    var indicaIdiomaPortugues = hfGeral.Get("indicaIdiomaPortugues");
    var simboloMoeda = indicaIdiomaPortugues ? "R$ " : "U$ ";
    var strPrefixoMoeda = (formato == "M") ? simboloMoeda : "";
    if (precisao >= 0 && precisao != undefined && precisao != null) {
        if (formato == "M") {
            //aqui não pode ocorrer arredondamento, deve ser truncamento
            resultado = truncateToDecimals(resultado, precisao);
        }
        else {
            //aqui ocorre arredondamento
            resultado = parseFloat(resultado.toFixed(precisao));
        }
        
    }
    txt_CAL_ResultadoFormula.SetValue(strPrefixoMoeda + resultado.toString().replace('.', traducao.geral_separador_decimal));
}

function calculate(equation) {
    var answer = 'erro';
    try {
        answer = equation != '' ? eval(equation) : '0';
    }
    catch (e) {
    }
    return answer;
}

//https://stackoverflow.com/questions/10808671/javascript-how-to-prevent-tofixed-from-rounding-off-decimal-numbers/44184500#44184500
function truncateToDecimals(num, dec = 2) {
    const calcDec = Math.pow(10, dec);
    return Math.trunc(num * calcDec) / calcDec;
}

function replaceAll(origem, antigo, novo) {
    return origem.split(antigo).join(novo);
}

    
