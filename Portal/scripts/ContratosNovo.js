

var frmParcelasContrato = '';
var frmAditivosContrato = '';
var atualizarURLParcelas = '';
var frmAnexosContrato = '';
var frmItensContrato = '';
var frmLinksContrato = '';
var atualizarURLItens = '';
var atualizarURLAnexos = '';
var atualizarURLAditivos = '';
var atualizarURLLinks = '';
var mostraColunaIndicaRevisaoPrevia = false;
var codigoContratoGlobal;

function refreshGridContratos() {
    gvDados.PerformCallback();
}

function alteraLabelCliFor() {
    if (rbClienteFor.GetValue() == 'C')
        lblClienteFor.SetText(traducao.ContratosNovo_cliente + ":");
    else if (rbClienteFor.GetValue() == 'F')
        lblClienteFor.SetText(traducao.ContratosNovo_fornecedor + ":");
    else
        lblClienteFor.SetText("Concedente de convênio");
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    //------------Obtendo data e hora actual
    var dataInicio = new Date(ddlInicioDeVigencia.GetValue());
    var dataInicioP = (dataInicio.getMonth() + 1) + "/" + dataInicio.getDate() + "/" + dataInicio.getFullYear();
    var dataInicioC = Date.parse(dataInicioP);

    var dataTermino = new Date(ddlTerminoDeVigencia.GetValue());
    var dataTerminoP = (dataTermino.getMonth() + 1) + "/" + dataTermino.getDate() + "/" + dataTermino.getFullYear();
    var dataTerminoC = Date.parse(dataTerminoP);

    //OK
    if (txtNumeroContrato.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosNovo_o_n_mero_do_contrato_deve_ser_informado_;
    }
    //OK
    if (mmObjeto.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosNovo_a_descri__o_do_objeto_deve_ser_informada_;
    }
    //OK
    if (ddlUnidadeGestora.GetValue() == null || ddlUnidadeGestora.GetValue() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosNovo_a_unidade_gestora_deve_ser_informada_;
    }
    //OK
    if (ddlResponsavel.GetText() == "" || ddlResponsavel.GetText() == "Todos") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosNovo_o_respons_vel_deve_ser_informado_;
    }
    //OK
    if (ddlStatusComplementarContrato.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosNovo_o_status_do_contrato_deve_ser_informado_;
    }
    //OK
    if (ddlTipoContrato.GetValue() == null || ddlTipoContrato.GetText() == "Selecionar..." || ddlTipoContrato.GetValue() == 0) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosNovo_o_tipo_de_contrato_deve_ser_informada_;
    }

    if ((ddlInicioDeVigencia.GetValue() != null) && (ddlTerminoDeVigencia.GetValue() != null)) {
        if (dataInicioC > dataTerminoC) {
            mensagem += "\n" + numAux + ") " + traducao.ContratosNovo_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n";
            retorno = false;
        }
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = traducao.ContratosNovo_alguns_dados_s_o_de_preenchimento_obrigat_rio_ + "\n\n" + mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {

    if (mostraColunaIndicaRevisaoPrevia == true) {
        //document.getElementById("td_cabecalhoRevisaoPrevia").style.visibility = 'visible';
        //document.getElementById("td_ConteudoRevisaoPrevia").style.visibility = 'visible';
    }
    else {
        //document.getElementById("td_cabecalhoRevisaoPrevia").style.display = 'none';
        //document.getElementById("td_ConteudoRevisaoPrevia").style.display = 'none';
    }

    var tOperacao = ""
    var CantUnidadesGestora = 0

    try {// Função responsável por preparar os campos do formulário para receber um novo registro
        txtNumeroContrato.SetText("");
        ddlTipoContrato.SetValue("0");
        ddlSituacao.SetValue("A");
        ddlModalidadAquisicao.SetValue("");


        mmObjeto.SetText("");
        mmObjeto.Validate();

        ddlProjetos.SetSelectedIndex(0);

        if (window.hfGeral && hfGeral.Contains("CantUnidadesGestora"))
            CantUnidadesGestora = hfGeral.Get("CantUnidadesGestora");

        ddlUnidadeGestora.SetValue(null);
        ddlResponsavel.SetValue(null);
        ddlFontePagadora.SetText("");
        ddlInicioDeVigencia.SetValue(null);
        ddlTerminoDeVigencia.SetValue(null);
        rbClienteFor.SetValue(null);
        ddlRazaoSocial.SetValue(null);
        txtSAP.SetText("");

        mmObservacoes.SetText("");
        mmObservacoes.Validate();


        txtValorDoContrato.SetValue(0);
        txtValorDoContrato.SetReadOnly(false);
        //alteraLabelCliFor();

        spnValorComAditivo.SetValue(null);

        spnPagoAcumulado.SetValue(null);
        spnPrevistoAcumulado.SetValue(null);
        spnSaldo.SetValue(null);
        spnPrevistoAcumulado.SetValue(null);
        ddlStatusComplementarContrato.SetValue(null);

        

    } catch (e) { }
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    mostraColunaIndicaRevisaoPrevia = (grid.cp_MostraColunaIndicaRevisaoPrevia == "S");
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'NomeUsuario;NomeUnidadeNegocio;NomeFonte;DescricaoTipoAquisicao;CodigoContrato;NomeProjeto;NumeroContrato;StatusContrato;Fornecedor;DataInicio;DataTermino;CodigoUsuarioResponsavel;DescricaoObjetoContrato;CodigoTipoAquisicao;CodigoUnidadeNegocio;CodigoFonteRecursosFinanceiros;Observacao;NumeroContratoSAP;CodigoProjeto;CodigoTipoContrato;ValorContrato;CodigoPessoaContratada;TipoPessoa;TemParcelas;IndicaRevisaoPrevia;CodigoStatusComplementarContrato;DescricaoStatusComplementarContrato;ValorContratoOriginal', MontaCamposFormulario);
    }
}


/*------------------------------------------------
 Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    0- NomeUsuario;                 8- Fornecedor;                          16- Observacao;                25 - CodigoStatusComplementarContrato
    1- NomeUnidadeNegocio;          9- DataInicio;                          17- NumeroContratoSAP;         26 - DescricaoStatusComplementarContrato
    2- NomeFonte;                   10- DataTermino;                        18- CodigoProjeto              27 - ValorContratoOriginal
    3- DescricaoTipoAquisicao;      11- CodigoUsuarioResponsavel;           19- CodigoTipoContrato
    4- CodigoContrato;              12- DescricaoObjetoContrato;            20- ValorContrato
    5- NomeProjeto;                 13- CodigoTipoAquisicao;                21- CodigoPessoaContratada
    6- NumeroContrato;              14- CodigoUnidadeNegocio;               22- TipoPessoa
    7- StatusContrato;              15- CodigoFonteRecursosFinanceiros;     23- TemParcelas 
-------------------------------------------------*/
function MontaCamposFormulario(values) {
    var codigoContrato = (values[4] != null ? values[4] : "");
    var numeroContrato = (values[6] != null ? values[6] : "");
    var situacao = (values[7] != null ? values[7] : "");
    var aquisicao = (values[13] != null ? values[13] : "");
    var fornecedor = (values[21] != null ? values[21] : "");
    var descricaoFornecedor = (values[8] != null ? values[8] : "");
    var unidadeGestora = (values[14] != null ? values[14] : "");
    var responsavel = (values[0] != null ? values[0] : "");
    var descricaoObj = (values[12] != null ? values[12] : "");
    var fonte = (values[2] != null ? values[2] : "");
    var observacoes = (values[16] != null ? values[16] : "");
    var ValorContrato = values[20];
    var CodigoFonteRecursosFinanceiros = (values[15] != null ? values[15] : "");
    var codigoUsuarioResponsavel = (values[11] != null ? values[11] : "");
    var NumeroContratoSAP = (values[17] != null ? values[17] : "");
    var codigoProjeto = (values[18] != null ? values[18] : "0");
    var codigoTipoContrato = (values[19] != null ? values[19] : "0");
    var NomeUnidadeNegocio = (values[1] != null ? values[1] : "");
    var tipoPessoa = (values[22] != null ? values[22] : "");
    var temParcelas = (values[23] != null ? values[23] : "");
    var indicaRevisaoPrevia = (values[24] != null ? values[24] : "N");
    var CodigoStatusComplementarContrato = (values[25] != null ? values[25] : "");
    var DescricaoStatusComplementarContrato = (values[26] != null ? values[26] : "");
    var ValorContratoOriginal = (values[27] != null ? values[27] : "");

    desabilitaHabilitaComponentes(temParcelas);

    //----------prencho os objetos.
    txtNumeroContrato.SetText(numeroContrato);
    ddlTipoContrato.SetValue(codigoTipoContrato);
    ddlSituacao.SetValue(situacao);
    ddlModalidadAquisicao.SetValue(aquisicao);
    ddlRazaoSocial.SetValue(fornecedor);
    ddlRazaoSocial.SetText(descricaoFornecedor);

    rbClienteFor.SetValue(tipoPessoa);

    mmObjeto.SetText(descricaoObj);
    mmObjeto.Validate();

    ddlProjetos.SetValue(codigoProjeto);

    if (values[9] == null || values[9] == "")
        ddlInicioDeVigencia.SetText("");
    else
        ddlInicioDeVigencia.SetValue(values[9]);
    if (values[10] == null || values[10] == "")
        ddlTerminoDeVigencia.SetText("");
    else
        ddlTerminoDeVigencia.SetValue(values[10]);

    var readOnly = TipoOperacao == "Consultar" ? 'S' : 'N';

    atualizarURLItens = 'S';
    frmItensContrato = './frmItensContrato.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao;

    atualizarURLParcelas = 'S';
    frmParcelasContrato = './frmParcelasContratos.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&IVG=' + ddlInicioDeVigencia.GetText() + '&TP=' + tipoPessoa;

    atualizarURLAditivos = 'S';
    frmAditivosContrato = './frmAditivosContratoNovo.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&IVG=' + ddlInicioDeVigencia.GetText() + '&TP=' + tipoPessoa;

    atualizarURLAnexos = 'S';
    frmAnexosContrato = '../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=CT&ID=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=250' + '&Popup=S';

    atualizarURLLinks = 'S';
    frmLinksContrato = './frmLinksContrato.aspx?CC=' + codigoContrato + '&RO=' + readOnly;

    if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 1)
        document.getElementById('frmItens').src = frmItensContrato;
    if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 2)
        document.getElementById('frmParcelas').src = frmParcelasContrato;
    if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 3)
        document.getElementById('frmAditivos').src = frmAditivosContrato;
    if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 3)
        document.getElementById('frmLinks').src = frmLinksContrato;
    if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 5)
        document.getElementById('frmAnexos').src = frmAnexosContrato;

    try {
        if (TipoOperacao == "Consultar")
            ddlUnidadeGestora.SetText(NomeUnidadeNegocio);
        else
            ddlUnidadeGestora.SetValue(unidadeGestora);
    } catch (e) { }

    //var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;

    //document.getElementById('frmParcelas').width = sWidth;
    //document.getElementById('frmAditivos').width = sWidth;
    //document.getElementById('frmItens').width = sWidth;
    //document.getElementById('frmAnexos').width = sWidth;

    ddlResponsavel.SetValue(codigoUsuarioResponsavel);
    ddlResponsavel.SetText(responsavel);
    txtSAP.SetText(NumeroContratoSAP);

    mmObservacoes.SetText(observacoes);
    mmObservacoes.Validate();

    (ValorContratoOriginal == null || ValorContratoOriginal.toString() == "" ? txtValorDoContrato.SetValue(0) : txtValorDoContrato.SetValue(ValorContratoOriginal));

    txtValorDoContrato.SetReadOnly(true);

    ddlFontePagadora.SetValue(CodigoFonteRecursosFinanceiros);

    if (window.hfGeral && hfGeral.Contains("CodigoResponsavel"))
        hfGeral.Set("CodigoResponsavel", codigoUsuarioResponsavel);

    //mostraColunaIndicaRevisaoPrevia
    //if (mostraColunaIndicaRevisaoPrevia == true) {
    //    document.getElementById("td_cabecalhoRevisaoPrevia").style.visibility = 'visible';
    //    document.getElementById("td_ConteudoRevisaoPrevia").style.visibility = 'visible';
    //}
    //else {
    //    document.getElementById("td_cabecalhoRevisaoPrevia").style.display = 'none';
    //    document.getElementById("td_ConteudoRevisaoPrevia").style.display = 'none';
    //}

    ddlStatusComplementarContrato.SetValue(CodigoStatusComplementarContrato);
    ddlStatusComplementarContrato.SetText(DescricaoStatusComplementarContrato);


    cbIndicaRevisaoPrevia.SetValue(indicaRevisaoPrevia);
    //ddlRazaoSocial.PerformCallback(fornecedor);

    codigoContratoGlobal = codigoContrato;
    //alert(codigoContratoGlobal);

    pnSaldo.PerformCallback(codigoContrato);

    //alteraLabelCliFor();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes(temParcelas) {
    var BoolEnabled = window.TipoOperacao && TipoOperacao != "Consultar";
    txtNumeroContrato.SetEnabled(BoolEnabled);
    ddlTipoContrato.SetEnabled(BoolEnabled);
    ddlSituacao.SetEnabled(BoolEnabled);
    ddlModalidadAquisicao.SetEnabled(BoolEnabled);
    ddlRazaoSocial.SetEnabled(BoolEnabled && temParcelas == 0);
    mmObjeto.SetEnabled(BoolEnabled);
    ddlProjetos.SetEnabled(BoolEnabled);
    ddlUnidadeGestora.SetEnabled(BoolEnabled);
    ddlInicioDeVigencia.SetEnabled(BoolEnabled);
    ddlTerminoDeVigencia.SetEnabled(BoolEnabled);
    ddlResponsavel.SetEnabled(BoolEnabled);
    txtSAP.SetEnabled(BoolEnabled);
    mmObservacoes.SetEnabled(BoolEnabled);
    ddlFontePagadora.SetEnabled(BoolEnabled);
    rbClienteFor.SetEnabled(BoolEnabled && temParcelas == 0);
    cbIndicaRevisaoPrevia.SetEnabled(BoolEnabled);
    ddlStatusComplementarContrato.SetEnabled(BoolEnabled);
    //alteraLabelCliFor();
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}

function setMaxLength(textAreaElement, length) {
    textAreaElement.maxlength = length;
    ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
    ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
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
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function buscaNomeBD(objeto) {
    hfGeral.Set("CodigoResponsavel", "");
    nome = objeto.GetText();
    if (nome != "")
        callbackResponsavel.PerformCallback();
    else
        mostraLov();
}

var retornoPopUp = null;

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function mostraLovResponsavel(param) {
    var where = hfGeral.Get("ComandoWhere");

    if (window.showModalDialog) {
        retornoPopUp = window.showModalDialog('../../lov.aspx?tab=usuario&val=codigoUsuario&nom=nomeUsuario&whe=' + where + '&ord=nomeUsuario&Pes=' + txtResponsavel.GetText(), '', 'resizable:0; dialogWidth:520px; dialogHeight:465px; status:no; menubar:no;');
        atualizaDadosLov();
    }
    else {
        window.open('../../lov.aspx?tab=usuario&val=codigoUsuario&nom=nomeUsuario&whe=' + where + '&ord=nomeUsuario&Pes=' + txtResponsavel.GetText(), 'LOV', 'resizable=0,width=520,height=465,status=no,menubar=no');
    }

}

function atualizaDadosLov() {
    if (retornoPopUp && retornoPopUp != "") {
        var aRetorno = retornoPopUp.split(';');
        hfGeral.Set("CodigoResponsavel", aRetorno[0]);
        txtResponsavel.SetText(aRetorno[1]);
    }
    else {
        txtResponsavel.SetText("");
        hfGeral.Set("CodigoResponsavel", "");
    }
}

function onEnd_CallbackResponsavel(s, e) {
    if (s.cp_Codigo != "") {
        hfGeral.Set("CodigoResponsavel", s.cp_Codigo);
        txtResponsavel.SetText(s.cp_Nome);
    }
    else {
        mostraLovResponsavel(s.cp_Where);
    }
}

function podeMudarAba(s, e) {
    if (e.tab.index == 0) {
        return false;
    }
    else if (TipoOperacao == 'Incluir') {
        var msg = traducao.ContratosNovo_para_ter_acesso_a_op__o_ + "\"" + e.tab.GetText() + "\" " + traducao.ContratosNovo___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + tabControl.GetTab(0).GetText() + "\"";
        window.top.mostraMensagem(msg, 'atencao', true, false, null);
        return true;
    }

    return false;
}

/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function OnGridFocusedRowChangedPopup(grid) {
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoContrato;NomeFonte;DescricaoTipoAquisicao;NumeroContrato;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor) {
    var idObjeto = (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "") + " [" + (valor[2] != null ? valor[2] : "") + "] - Nº: " + (valor[3] != null ? valor[3] : "");
    var tituloMapa = "";

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = 900;
    var window_height = 590;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;

    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=CT&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa, traducao.ContratosNovo_interessados, null, null, '', null);
}

function onClick_NovoContrato() {
    tabControl.SetActiveTabIndex(0);
    TipoOperacao = "Incluir";
    onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
    desabilitaHabilitaComponentes(0);
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 10;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
