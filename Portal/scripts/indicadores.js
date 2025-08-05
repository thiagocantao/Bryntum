// JScript File
var PossuiMetaResultado = 'N';

function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function IncluirNovoRegistro()
{
    TipoOperacao = 'Incluir';
    hfGeral.Set('TipoOperacao', TipoOperacao);
    onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
    desabilitaHabilitaComponentes();
}

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
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadores_o_indicador_deve_ser_informado_ + "\n";

    if(cmbAgrupamentoDaMeta.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadores_agrupamento_da_meta_deve_ser_informado_ + "\n";
            
    if(ddlUnidadeNegocio.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadores_a_ + ddlUnidadeNegocio.cp_NomeUnidade + traducao.indicadores__deve_ser_informada_ + "\n";

    if (ddlResponsavelIndicador.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadores_o_responsavel_pelo_indicador_deve_ser_informado_ + "\n";

    if (ddlResponsavelResultado.GetText() == "")
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadores_o_responsavel_pela_atualiza__o_do_indicador_deve_ser_informado_ + "\n";

    if(!validaFormula())
        mensagemErro_ValidaCamposFormulario += (contador++).toString() + ") " + traducao.indicadores_a_f_rmula_do_indicador_deve_ser_informada_ + "\n";

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

function alteraResponsavelCombo(valores)
{
    var valor = valores[0];
    /* s.GetRowValues(s.GetFocusedRowIndex(), 'NomeUsuarioResponsavel;CodigoIndicador;CodigoReservado;NomeIndicador;CodigoResponsavelAtualizacaoIndicador;NomeUsuarioResponsavelResultado;CodigoResponsavel', alteraResponsavelCombo);
*/

    var NomeUsuarioResponsavel = (valor[0] == null) ? "" : valor[0].toString();
    var CodigoIndicador = (valor[1] == null) ? "" : valor[1].toString();
    var CodigoReservado = (valor[2] == null) ? "" : valor[2].toString();
    var NomeIndicador = (valor[3] == null) ? "" : valor[3].toString();
    var CodigoResponsavelAtualizacaoIndicador = (valor[4] == null) ? "" : valor[4].toString();
    var NomeUsuarioResponsavelResultado = (valor[5] == null) ? "" : valor[5].toString();
    var CodigoResponsavel = (valor[6] == null) ? "" : valor[6].toString();
    
    ddlResponsavel.SetValue(CodigoResponsavel);
    ddlResponsavel.SetText(NomeUsuarioResponsavel);

    hfGeral.Set("hfCodigoIndicador", CodigoIndicador);

    txtCodigoReservadoNovoResp.SetText(CodigoReservado);

    ddlResponsavelResultados2.SetValue(CodigoResponsavelAtualizacaoIndicador);
    ddlResponsavelResultados2.SetText(NomeUsuarioResponsavelResultado);
    
    pcResponsavel2.Show();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetSelectedFieldValues('CodigoResponsavel;CodigoIndicador;NomeIndicador;CodigoUnidadeMedida;GlossarioIndicador;CasasDecimais;Polaridade;FormulaIndicador;IndicaIndicadorCompartilhado;CodigoPeriodicidadeCalculo;FonteIndicador;CodigoResponsavel;FormulaFormatoCliente;IndicadorResultante;CodigoFuncaoAgrupamentoMeta;IndicaCriterio;LimiteAlertaEdicaoIndicador;CodigoReservado;NomeUsuarioResponsavel;CodigoResponsavelAtualizacaoIndicador;NomeUsuarioResponsavelResultado;CodigoUnidadeNegocio;DataInicioValidadeMeta;DataTerminoValidadeMeta;IndicaAcompanhamentoMetaVigencia;PossuiMetaResultado', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(valores)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    var values = valores[0];

    if(values)
    {
        var CodigoUsuario           = values[0];
        var CodigoIndicador         = values[1];
        var NomeIndicador           = values[2];
        var CodigoUnidadeMedida     = values[3];
        var GlossarioIndicador      = values[4];
        var CasasDecimais           = values[5];
        var Polaridade              = values[6];
        var FormulaIndicador        = values[7];
        var IndicaIndicadorCompartilhado    = values[8];
        var CodigoPeriodicidadeCalculo      = values[9];
        var FonteIndicador          = values[10];
        var CodigoUsuarioResponsavel= values[11];
        var FormulaFormatoCliente   = values[12];
        var IndicadorResultante     = values[13];
        var CodigoFuncaoAgrupamentoMeta     = values[14];
        var indicaCriterio = values[15];
        var limiteDias = values[16];
        var codigoReservado = values[17];
        var nomeResp = values[18];
        var CodigoUsuarioResponsavelResultado = values[19];
        var nomeRespResultado = values[20];
        var codigoUnidadeNegocio = values[21];
        var inicioVigencia = values[22];
        var terminoVigencia = values[23];
        var vigencia = values[24];
        PossuiMetaResultado = values[25];
        
        ddlUnidadeNegocio.SetEnabled(TipoOperacao == 'Editar' && PossuiMetaResultado == 'N');

        hfGeral.Set("hfCodigoUsuario", CodigoUsuario);
        hfGeral.Set("hfCodigoIndicador", CodigoIndicador);
        (NomeIndicador == null ? txtIndicador.SetText("") : txtIndicador.SetText(NomeIndicador));
        (NomeIndicador == null ? lblCaptionIndicador.SetText("") : lblCaptionIndicador.SetText(NomeIndicador));
        (CodigoUnidadeMedida == null ? ddlUnidadeMedida.SetText("") : ddlUnidadeMedida.SetValue(CodigoUnidadeMedida));
        (codigoUnidadeNegocio == null ? ddlUnidadeNegocio.SetText("") : ddlUnidadeNegocio.SetValue(codigoUnidadeNegocio));
        if(window.heGlossario)
            (GlossarioIndicador == null ? heGlossario.SetHtml("") : heGlossario.SetHtml(GlossarioIndicador));
        (CasasDecimais == null ? ddlCasasDecimais.SetText("") : ddlCasasDecimais.SetValue(CasasDecimais));
        (Polaridade == null ? ddlPolaridade.SetText("") : ddlPolaridade.SetValue(Polaridade));
        (CodigoPeriodicidadeCalculo == null ? ddlPeriodicidade.SetText("")  : ddlPeriodicidade.SetValue(CodigoPeriodicidadeCalculo));
        (FonteIndicador == null ? txtFonte.SetText("") : txtFonte.SetText(FonteIndicador));
        
        (CodigoUsuarioResponsavel == null ? ddlResponsavelIndicador.SetText("") : ddlResponsavelIndicador.SetValue(CodigoUsuarioResponsavel));
        (CodigoUsuarioResponsavel == null ? ddlResponsavelIndicador.SetText("") : ddlResponsavelIndicador.SetText(nomeResp));

        (CodigoUsuarioResponsavelResultado == null ? ddlResponsavelResultado.SetText("") : ddlResponsavelResultado.SetValue(CodigoUsuarioResponsavelResultado));
        (CodigoUsuarioResponsavelResultado == null ? ddlResponsavelResultado.SetText("") : ddlResponsavelResultado.SetText(nomeRespResultado));


        (CodigoFuncaoAgrupamentoMeta == null ? cmbAgrupamentoDaMeta.SetText("") : cmbAgrupamentoDaMeta.SetValue(CodigoFuncaoAgrupamentoMeta));

        (inicioVigencia == null ? ddlInicioVigencia.SetValue(null) : ddlInicioVigencia.SetValue(inicioVigencia));
        (terminoVigencia == null ? ddlTerminoVigencia.SetValue(null) : ddlTerminoVigencia.SetText(terminoVigencia));
        cbVigencia.SetChecked(vigencia == "S");
        verificaVigencia();

        if (FormulaFormatoCliente == null) {
            txtFormulaIndicador.SetText("");
        }
        else {
            FormulaFormatoCliente = replaceAll(FormulaFormatoCliente, ',', '');
            FormulaFormatoCliente = replaceAll(FormulaFormatoCliente, '.', ',');
            txtFormulaIndicador.SetText(FormulaFormatoCliente);
        }
        
        if (IndicadorResultante != null)
          cbCheckResultante.SetChecked(IndicadorResultante.toString() == "S");
        (limiteDias == null ? txtLimite.SetValue("0") : txtLimite.SetValue(limiteDias.toString())); 
        txtCodigoReservado.SetText(codigoReservado);
        if (indicaCriterio != null)
            rbCriterio.SetValue(indicaCriterio); 
        else
            rbCriterio.SetValue(null);

        pcDados.AdjustSize();
    }
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
//    if (TipoOperacao == "Editar")
    //        window.top.mostraMensagem("Agora você pode inserir os DADOS do Indicador.", 'Atencao', true, false, null);
    // se já incluiu alguma opção, feche a tela após salvar
//    if (gvDados.GetVisibleRowsOnPage() > 0 )
////        onClick_btnCancelar();    
//    {
//    //habilitaModoEdicaoBarraNavegacao(false, gvDados);
//    //pcDados.Hide();
//    TipoOperacao = "Consultar";
//    hfGeral.Set("TipoOperacao", TipoOperacao);
//    return true;
//    }
}

// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// -------------------------------------------------------------------------------


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
        window.top.mostraMensagem(traducao.indicadores_a_f_rmula_digitada_cont_m_erros_, 'Atencao', true, false, null);
        return false;
    }

    resultado = replaceAll(resultado.toString(), '.', ',');

    txtResultadoFormula.SetText(resultado);
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
function mostraDivSalvoPublicado(acao)
{
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


/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function OnGridFocusedRowChangedPopup(grid) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    grid.GetSelectedFieldValues('CodigoIndicador;NomeIndicador;CodigoUnidadeNegocio', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valores)
{
    var valor = valores[0];
    var idObjeto     =  (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var codigoObjPai = (valor[2] != null ? valor[2] : "0");
    var tituloMapa   = "";
   
    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width    = 900;
    var window_height   = 590;
    var newfeatures     = 'scrollbars=no,resizable=no';
    var window_top      = (screen.height-window_height)/2;
    var window_left     = (screen.width-window_width)/2;
    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=IN&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&COP=" + codigoObjPai + "&TME=" + tituloMapa + '&AlturaGrid=' + (window_height - 110), traducao.indicadores_permiss_es, null, null, '', null);
}

function abreCompartilhamentoIndicador(codigoIndicador) {
    window.top.showModal("./IndicadoresCompartilhamento.aspx?CodigoIndicador=" + codigoIndicador, traducao.indicadores_compartilhamento_do_indicador, null, null, '', null);
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

var indicaSomenteLeitura;

function abreDetalhes(codigoIndicador, somenteLeitura)
{
    if (codigoIndicador == -1) {
        var url = window.top.pcModal.cp_Path + '_Estrategias/wizard/IndicadoresEstrategicosFrm.aspx?RO=N&CI=-1&CUN=-1' + '&alt=' + (screen.availHeight - 200);
        window.top.showModal2(url + '&AT=' + (400).toString(), traducao.indicadores_detalhes_do_indicador, screen.width - 40, screen.availHeight - 200, function (e) { gvDados.PerformCallback('Atualizar') });
    } else {
        indicaSomenteLeitura = somenteLeitura;
        gvDados.SelectRowOnPage(gvDados.GetFocusedRowIndex(), true);
        gvDados.GetSelectedFieldValues('CodigoIndicador;CodigoUnidadeNegocio', abrePopupDetalhes); //getKeyGvDados);    
    }
}

function abrePopupDetalhes(valores)
{
    if (window.screen.availHeight > 768) {
        var alturaPopup = 810;
    }
    else {
        var alturaPopup = 620;
    }
    var valor = valores[0];
    var url = window.top.pcModal.cp_Path + '_Estrategias/wizard/IndicadoresEstrategicosFrm.aspx?RO=' + indicaSomenteLeitura + '&CI=' + valor[0] + '&CUN=' + valor[1];
    window.top.showModal2(url + '&alt=' + (screen.availHeight - 200).toString(), traducao.indicadores_detalhes_do_indicador, screen.width - 40, screen.availHeight - 200, function (e) { gvDados.PerformCallback('Atualizar') });
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 105;
    gvDados.SetHeight(height);
}