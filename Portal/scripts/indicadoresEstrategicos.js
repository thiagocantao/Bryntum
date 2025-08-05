// JScript File
var PossuiMetaResultado = 'N';


// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function validaCamposFormulario()
{   // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var contador = 1;
    if (txtIndicador.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadoresEstrategicos_o_indicador_deve_ser_informado_ + "\n";

    if(cmbAgrupamentoDaMeta.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadoresEstrategicos_o_agrupamento_da_meta_deve_ser_informado_ + "\n";

    if (ddlUnidadeMedida.GetText() == "") {
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadoresEstrategicos_a_unidade_de_medida_deve_ser_informada_ + "\n";
    }

    if (ddlPeriodicidade.GetText() == "") {
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadoresEstrategicos_a_periodicidade_do_indicador_deve_ser_informada_ + "\n";
    }

    if (ddlUnidadeNegocio.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadoresEstrategicos_a_ + ddlUnidadeNegocio.cp_NomeUnidade + traducao.indicadoresEstrategicos__deve_ser_informada_ + "\n";

    if (ddlResponsavelIndicador.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadoresEstrategicos_o_responsavel_pelo_indicador_deve_ser_informado_ + "\n";

    if (ddlResponsavelResultado.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadoresEstrategicos_o_responsavel_pela_atualiza__o_do_indicador_deve_ser_informado_ + "\n";

    if(!validaFormula())
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadoresEstrategicos_a_f_rmula_do_indicador_deve_ser_informada_ + "\n";

    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{    
    txtIndicador.SetEnabled(TipoOperacao != "Consultar");
    txtFonte.SetEnabled(TipoOperacao != "Consultar");
    txtFormulaIndicador.SetEnabled(TipoOperacao != "Consultar");
    txtResultadoFormula.SetEnabled(TipoOperacao != "Consultar");
    txtCodigoReservado.SetEnabled(TipoOperacao != "Consultar");
    txtLimite.SetEnabled(TipoOperacao != "Consultar");
    
    txtD1.SetEnabled(TipoOperacao != "Consultar");
    txtA1.SetEnabled(TipoOperacao != "Consultar");
    txtD2.SetEnabled(TipoOperacao != "Consultar");
    txtA2.SetEnabled(TipoOperacao != "Consultar");
    txtD3.SetEnabled(TipoOperacao != "Consultar");
    txtA3.SetEnabled(TipoOperacao != "Consultar");

    ddlResponsavelIndicador.SetEnabled(TipoOperacao != "Consultar");
    ddlUnidadeNegocio.SetEnabled(TipoOperacao == "Incluir" || (TipoOperacao == 'Editar' && PossuiMetaResultado == 'N'));
    ddlResponsavelResultado.SetEnabled(TipoOperacao != "Consultar");
    ddlUnidadeMedida.SetEnabled(TipoOperacao != "Consultar");
    ddlCasasDecimais.SetEnabled(TipoOperacao != "Consultar");
    ddlPolaridade.SetEnabled(TipoOperacao != "Consultar");
    ddlPeriodicidade.SetEnabled(TipoOperacao != "Consultar");
    ddlResponsavelIndicador.SetEnabled(TipoOperacao != "Consultar");
    cmbAgrupamentoDaMeta.SetEnabled(TipoOperacao != "Consultar");
    cbCheckResultante.SetEnabled(TipoOperacao != "Consultar");
    rbCriterio.SetEnabled(TipoOperacao != "Consultar");

    ddlInicioVigencia.SetEnabled(TipoOperacao != "Consultar");
    ddlTerminoVigencia.SetEnabled(TipoOperacao != "Consultar");
    cbVigencia.SetEnabled(TipoOperacao != "Consultar");
    
    ddlC4.SetEnabled(TipoOperacao != "Consultar");
    
    heGlossario.SetEnabled(TipoOperacao != "Consultar");
}

function LimpaCamposFormulario()
{
    var tOperacao = ""

    try
    {
        PossuiMetaResultado = 'N';
        txtCodigoReservado.SetText("");
        txtIndicador.SetText("");
        txtFonte.SetText("");
        txtFormulaIndicador.SetText("");
        txtResultadoFormula.SetText("");
        ddlUnidadeNegocio.SetText("");
        ddlResponsavelIndicador.SetText("");
        ddlResponsavelResultado.SetText("");
        ddlUnidadeMedida.SetValue(1);
        ddlCasasDecimais.SetValue(0);
        ddlPolaridade.SetValue("POS");
        ddlPeriodicidade.SetValue(1);
        ddlResponsavelIndicador.SetText("");
        cmbAgrupamentoDaMeta.SetText("");
        rbCriterio.SetValue('S');
        txtLimite.SetValue("0");
        
        txtD1.SetValue("0");
        txtA1.SetValue("0");
        txtD2.SetValue("0");
        txtA2.SetValue("0");
        txtD3.SetValue("0");
        txtA3.SetValue("0");
        txtD4.SetValue("0");
        txtA4.SetValue("0");

        ddlInicioVigencia.SetValue(null);
        ddlTerminoVigencia.SetValue(null);
        verificaVigencia();
        
        if(window.heGlossario)
            heGlossario.SetHtml("");

        if(window.cbCheckResultante)
            cbCheckResultante.Checked = false; //cbCheckResultante.SetChecked(false);   
            
        lblCaptionIndicador.SetText("");

        if(TipoOperacao != null)
        {
            hfGeral.Set("TipoOperacao", TipoOperacao);
            hfGeral.Set("BotaoGvDado", TipoOperacao);
        }
        else
        {
            hfGeral.Set("TipoOperacao", "Consultar");
            hfGeral.Set("BotaoGvDado", "Consultar");
        }

        tOperacao = hfGeral.Get("TipoOperacao").toString();
        
        TabControl.GetTabByName("TabDado").SetVisible(tOperacao != 'Incluir');
        btnFaixaDeTolerancia.SetVisible(tOperacao != 'Incluir');
        
        
        if(tOperacao=="Incluir")//if(TipoOperacao=="Incluir")
        {
            hfGeral.Set("modoGridDados", "Incluir");
            var tab = TabControl.GetTab(0); //posiciono na TAB 0 (Detalhe).
            TabControl.SetActiveTab(tab);   
        }
       
        gridDadosIndicador.PerformCallback(tOperacao);
        
    }catch(e){}
}

function insereNovoDado() //---[ metodos do Dados
{            
    gridDadosIndicador.GetEditor("CodigoDado").PerformCallback();
}

function validaFormula()//---[ metodos da formula
{
    var tipoOperacao = hfGeral.Get("TipoOperacao").toString();

    var qtdeDados = gridDadosIndicador.GetVisibleRowsOnPage();
    var formulaValida = true;

    if (qtdeDados > 0 && tipoOperacao != 'Incluir') {
        executaFormula();
        var formula = txtFormulaIndicador.GetText();
        var resultadoFormula = txtResultadoFormula.GetText();
        var formulaValida = (formula != "" && resultadoFormula != "erro" && resultadoFormula != "infinity");
        //hfGeral.Set("FormulaIndicador", "");
    }
    else {
        txtFormulaIndicador.SetText("");
        hfGeral.Set("FormulaIndicador", "");
    }
    return formulaValida;
}

function executaFormula() {
    var nomeGrid = gridDadosIndicador.name + "_cell";
    var qtdeLinhas = gridDadosIndicador.GetVisibleRowsOnPage();
    var formula = txtFormulaIndicador.GetText();


    formula = formula.toUpperCase();
    FormulaIndicador = formula.toUpperCase();

    FormulaIndicador = replaceAll(FormulaIndicador, '.', '');
    FormulaIndicador = replaceAll(FormulaIndicador, ',', '.');

    formula = replaceAll(formula, '.', '');
    formula = replaceAll(formula, ',', '.');

    for (i = qtdeLinhas; i > 0; i--) {
        var celula = 'C' + i;
        if (formula.indexOf(celula) >= 0) {

            var valorCelula = "";
            var txtValorDado = document.getElementById(nomeGrid + (i - 1) + "_6_txtValorDado_" + (i - 1) + "_I");
            if (txtValorDado != null) {
                valorCelula = document.getElementById(nomeGrid + (i - 1) + "_6_txtValorDado_" + (i - 1) + "_I").value;

                valorCelula = replaceAll(valorCelula, '.', '');
                valorCelula = replaceAll(valorCelula, ',', '.');

                if (valorCelula.indexOf('.') < 0)
                    valorCelula += '.00';

                if (valorCelula == "") {
                    valorCelula = 1;
                    txtValorDado.value = valorCelula;
                    //document.getElementById(nomeGrid + (i-1)+"_4_txtValorDado_I").value = valorCelula;
                }
                //formula = formula.replace(/microsoft/gi, valorCelula));
                formula = replaceAll(formula, celula, valorCelula);
                valorDado = gridDadosIndicador.keys[(i - 1)]; //(i-1)
                valorDado = valorDado.substr(valorDado.indexOf('|') + 1);

                valorDado = replaceAll(valorDado, '.', '');
                valorDado = replaceAll(valorDado, ',', '.');

                var codigoComponente = parseInt(valorDado);

                if (codigoComponente > 0) {
                    FormulaIndicador = replaceAll(FormulaIndicador, celula, "[" + codigoComponente + "]");
                }
                else {
                    codigoComponente = (codigoComponente * -1);
                    FormulaIndicador = replaceAll(FormulaIndicador, celula, "[I" + codigoComponente + "]");
                }
            }
        }
    }
    //var resultado = eval(formula);
    var resultado = calculate(formula);
    if (resultado == "erro") {
        txtResultadoFormula.SetText("erro");
        window.top.mostraMensagem(traducao.indicadoresEstrategicos_a_f_rmula_digitada_cont_m_erros_, 'Atencao', true, false, null);
        return false;
    }

    callbackCalc.PerformCallback(formula);
    hfGeral.Set("FormulaIndicador", FormulaIndicador);
    return true;
}

function calculate(equation) {
    var answer = 'erro';
    var str = new RegExp("^[0-9+\\/\\-*\\(). ]*$");
    if (equation.match(str)) {
        try {
            answer = equation != '' ? eval(equation) : '0';
        }
        catch (e) {
        }
    }
    return answer;
}


function replaceAll(origem, antigo, novo) {

    var teste = 0;
    while (teste == 0) {
        if (origem.indexOf(antigo) >= 0) {
            origem = origem.replace(antigo, novo);
        }
        else
            teste = 1;
    }

    return origem;
}

//-------------------- DIV salvar
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}
//**** fim DIV salvar



function verificaCamposFaixaTolerancia(combo, txt1, txt2, txtAnterior)
{   
    if(txtAnterior != null && parseFloat(txtAnterior.GetValue().toString().replace(',', '.')) != 0)
        txt1.SetText((parseFloat(txtAnterior.GetValue().toString().replace(',', '.')) + 0.01).toString().replace('.', ','));
    
    txt1.SetEnabled(false);
    if(combo.GetValue() == "S")
    {
        txt1.SetText("");
        txt2.SetText("");
        txt2.SetEnabled(false);
    }
    else
    {
        txt2.SetEnabled(hfGeral.Get("TipoOperacao").toString() != "Consultar");
    }
}

function verificaVigencia() {
    var inicio = ddlInicioVigencia.GetValue();
    var termino = ddlTerminoVigencia.GetValue();

    if (inicio == null && termino == null) {
        cbVigencia.SetEnabled(false);
        cbVigencia.SetChecked(false);
    }
    else {
        cbVigencia.SetEnabled(TipoOperacao != "Consultar");
    }
}