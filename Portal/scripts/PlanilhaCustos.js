var codigoRecursoParam = null;

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
   mensagemErro_ValidaCamposFormulario = "";
   var numAux = 0;
    var mensagem = "";

    if (ddlItem.GetValue() == null)
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.PlanilhaCustos_o_item_deve_ser_informado_;
    }

    if (txtQuantidade.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.PlanilhaCustos_a_quantidade_deve_ser_informada_;
    }

    if (txtValorUnitario.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.PlanilhaCustos_o_valor_unit_rio_deve_ser_informado_;
    }
    
    if (ddlFonte.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.PlanilhaCustos_a_fonte_de_recursos_deve_ser_informada_;
    }
    
    var valorAnoCorrente = txtValorRequeridoAnoCorrente.GetValue() == null ? 0 : txtValorRequeridoAnoCorrente.GetValue();
    var valorAnoSeguinte = txtValorRequeridoAnoSeguinte.GetValue() == null ? 0 : txtValorRequeridoAnoSeguinte.GetValue();
    var valorAnoSeguinte2 = txtValorRequeridoAnoSeguinte2.GetValue() == null ? 0 : txtValorRequeridoAnoSeguinte2.GetValue();
    var valorTotal = txtValorTotal.GetValue() == null ? 0 : txtValorTotal.GetValue();
    var anoCorrente = txtValorRequeridoAnoCorrente.cp_AnoCorrente;

    if (parseFloat((valorAnoCorrente + valorAnoSeguinte + valorAnoSeguinte2).toFixed(2)) > parseFloat(valorTotal.toFixed(2))) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.PlanilhaCustos_a_soma_dos_valores_anuais_n_o_deve_ser_superior_ao_valor_total_;
    }

    if(mensagem != "")
    {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    hfGeral.Set("Critica", "");
    return mensagemErro_ValidaCamposFormulario;
}
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
    pnCallback.PerformCallback("Excluir");
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    ddlItem.SetValue(null);
    txtQuantidade.SetValue(null);
    txtValorUnitario.SetValue(null);
    txtValorTotal.SetValue(null);
    txtValorRequeridoAnoCorrente.SetValue(null);
    txtValorRequeridoAnoSeguinte.SetValue(null);
    txtValorRequeridoAnoSeguinte2.SetValue(null);
    lblDescricaoItem.SetText('');
    ckContratar.SetChecked(false);
    ddlFonte.SetValue(null);
    txtDotacao.SetValue(null);
    txtComentario.SetText('');
    document.getElementById('tdAjuda').style.display = 'none';
    lblCantCaraterOb.SetText(txtComentario.GetText().length);
    desabilitaHabilitaComponentes();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoItemRecurso;QuantidadeOrcada;ValorUnitario;ValorTotal;ValorRequeridoAnoCorrente;ValorRequeridoAnoSeguinte;CodigoFonteRecursosFinanceiros;DotacaoOrcamentaria;IndicaContratarItem;UnidadeMedida;DetalheItemRecurso;ValorRequeridoPosAnoSeguinte;DescricaoItemOrcamentoProjeto;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values)
{
    desabilitaHabilitaComponentes();

    var codigoItemRecurso               = (values[0] != null ? values[0] : "");
    var quantidadeOrcada                = values[1];
    var valorUnitario                   = values[2];
    var valorTotal                      = values[3];
    var valorRequeridoAnoCorrente       = values[4];
    var valorRequeridoAnoSeguinte       = values[5];
    var codigoFonteRecursosFinanceiros  = (values[6] != null ? values[6] : "");
    var dotacaoOrcamentaria             = (values[7] != null ? values[7] : "");
    var indicaContratarItem             = (values[8] != null ? values[8] : "");
    var unidadeMedida                   = (values[9] != null ? values[9] : "");
    var detalheItem                     = (values[10] != null ? values[10] : "");
    var valorRequeridoAnoSeguinte2      = values[11];
    var comentario                      = values[12];

    ddlItem.SetValue(codigoItemRecurso);
    txtQuantidade.SetValue(quantidadeOrcada);
    txtValorUnitario.SetValue(valorUnitario);
    txtValorTotal.SetValue(valorTotal);
    txtValorRequeridoAnoCorrente.SetValue(valorRequeridoAnoCorrente);
    txtValorRequeridoAnoSeguinte.SetValue(valorRequeridoAnoSeguinte);
    txtValorRequeridoAnoSeguinte2.SetValue(valorRequeridoAnoSeguinte2);
    //txtUnidadeMedida.SetText(unidadeMedida);
    ckContratar.SetChecked(indicaContratarItem == "S");
    ddlFonte.SetValue(codigoFonteRecursosFinanceiros);
    txtDotacao.SetValue(dotacaoOrcamentaria);
    txtComentario.SetText(comentario);

    lblCantCaraterOb.SetText(txtComentario.GetText().length);

    if (detalheItem != null && detalheItem != '') {
        document.getElementById('tdAjuda').style.display = '';
        lblDescricaoItem.SetText(detalheItem);
    }
    else {
        lblDescricaoItem.SetText('');
        document.getElementById('tdAjuda').style.display = 'none';
    }
}

function calculaTotal() {
    if (txtQuantidade.GetValue() == null || txtValorUnitario.GetValue() == null)
        txtValorTotal.SetValue(null);
    else {
        var valorTotal = txtQuantidade.GetValue() * txtValorUnitario.GetValue();
        txtValorTotal.SetValue(valorTotal);
    }
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    ddlItem.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtQuantidade.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValorUnitario.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValorRequeridoAnoCorrente.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValorRequeridoAnoSeguinte.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValorRequeridoAnoSeguinte2.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    //txtUnidadeMedida.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ckContratar.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlFonte.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtDotacao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtComentario.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}
function callbackPopupPlanilhaCustos(valores) {
    
    var CodigoItemRecurso = (valores[0] == null) ? "" : valores[0].toString();//CIR
    var QuantidadeOrcada = (valores[1] == null) ? "" : valores[1].toString();//QO
    var ValorUnitario = (valores[2] == null) ? "" : valores[2].toString();//VU
    var ValorTotal = (valores[3] == null) ? "" : valores[3].toString();//VT
    var ValorRequeridoAnoCorrente = (valores[4] == null) ? "" : valores[4].toString();//VRAC
    var ValorRequeridoAnoSeguinte = (valores[5] == null) ? "" : valores[5].toString();//VRAS
    var CodigoFonteRecursosFinanceiros = (valores[6] == null) ? "" : valores[6].toString();//CFRF
    var DotacaoOrcamentaria = (valores[7] == null) ? "" : valores[7].toString();//DO
    var IndicaContratarItem = (valores[8] == null) ? "" : valores[8].toString();//ICI
    var UnidadeMedida = (valores[9] == null) ? "" : valores[9].toString();//UM
    var DetalheItemRecurso = (valores[10] == null) ? "" : valores[10].toString();//DIR
    var ValorRequeridoPosAnoSeguinte = (valores[11] == null) ? "" : valores[11].toString();//VRPAS
    var DescricaoItemOrcamentoProjeto = (valores[12] == null) ? "" : valores[12].toString();//DIOP
    var CodigoItemOrcamento = (valores[13] == null) ? "" : valores[13].toString();//CIO

    var surl = window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/popupPlanilhaCustos.aspx';

    var altura = screen.availHeight - 350;
    var largura = screen.availWidth - 100;

    surl += '?CIR=' + CodigoItemRecurso;//CIR
    surl += '&QO=' + QuantidadeOrcada;//QO
    surl += '&VU=' + ValorUnitario;//VU
    surl += '&VT=' + ValorTotal;//VT
    surl += '&VRAC=' + ValorRequeridoAnoCorrente;//VRAC
    surl += '&VRAS=' + ValorRequeridoAnoSeguinte;//VRAS
    surl += '&CFRF=' + CodigoFonteRecursosFinanceiros;//CFRF
    surl += '&DO=' + DotacaoOrcamentaria;//DO
    surl += '&ICI=' + IndicaContratarItem;//ICI
    surl += '&UM=' + UnidadeMedida;//UM
    surl += '&DIR=' + DetalheItemRecurso;//DIR
    surl += '&VRPAS=' + ValorRequeridoPosAnoSeguinte;//VRPAS
    surl += '&DIOP=' + DescricaoItemOrcamentoProjeto;//DIOP
    surl += '&CIO=' + CodigoItemOrcamento;//CIO
    surl += '&AC=' + gvDados.cp_anoCorrente;
    surl += '&CP=' + gvDados.cp_CodigoProjeto;
    surl += '&CWF=' + gvDados.cp_CodigoWorkflow;
    surl += '&CLB=' + gvDados.cp_CodigoLB;
    surl += '&TO=' + TipoOperacao;
    surl += '&ALT=' + altura;
    window.top.showModal(surl, 'Planilha de Custos', largura, altura, atualizaGrid, null);
}

function atualizaGrid() {
    gvDados.Refresh();
}

function novoRegistro() {
    var surl = window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/popupPlanilhaCustos.aspx';
    var altura = screen.availHeight - 350;
    var largura = screen.availWidth - 100;
    surl += '?CIO=-1';
    surl += '&AC=' + gvDados.cp_anoCorrente;
    surl += '&CP=' + gvDados.cp_CodigoProjeto;
    surl += '&CWF=' + gvDados.cp_CodigoWorkflow;
    surl += '&CLB=' + gvDados.cp_CodigoLB;
    surl += '&ALT=' + altura;


    //alert(valores[0]);
    window.top.showModal(surl, 'Planilha de Custos', largura, altura, atualizaGrid, null);
}

function onKeyUpOrChange(evt) {
    processTextAreaText(ASPxClientUtils.GetEventSource(evt));
}

function processTextAreaText(textAreaElement) {
    var maxLength = textAreaElement.maxlength;
    var text = textAreaElement.value;
    var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
    if (maxLength != 0 && text.length > maxLength)
        textAreaElement.value = text.substr(0, maxLength);
    else {
        lblCantCaraterOb.SetText(text.length);
    }
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function onInit_txtComentario(s, e) {
    try { return setMaxLength(s.GetInputElement(), 2000); }
    catch (e) { }
}


function setMaxLength(textAreaElement, length) {
    textAreaElement.maxlength = length;
    ASPxClientUtils.AttachEventToElement(textAreaElement, "keyup", createEventHandler("onKeyUpOrChange"));
    ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
}