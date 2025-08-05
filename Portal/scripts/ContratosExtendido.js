
var bPodeAlterarNumero = 'N';
//var bUsaNumeracaoAutomatica = 'N';
var indicaObra = 'N';
var frmParcelasContrato = '';
var atualizarURLParcelas = '';
var frmAnexosContrato = '';
var atualizarURLAnexos = '';
var frmAditivosContrato = '';
var atualizarURLAditivos = '';
var frmPrevisaoFinanceira = '';
var atualizarURLPrevisao = '';
var frmComentariosContrato = '';
var atualizarURLComentarios = '';
var atualizarURLReajuste = '';
var atualizarURLAcessorios = '';
var frmReajuste = '';
var statusContrato = '';
var qtdAditivos = 0;
var vValorOriginal = 0;
var vValorContrato = 0;
var vValorPago = 0;
var dtContratoAditivo = '';
var dtContratoOriginal = '';
var permissoes = 0;                    



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

    if (ddlTipoContrato.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_o_instrumento_deve_ser_informado_;
    }
    if (hfGeral.Get("UsaNumeracaoAutomatica") == "N" && txtNumeroContrato.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_o_n_mero_do_contrato_deve_ser_informado_;
    }
    if (ddlSituacao.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_situa__o_deve_ser_informada_;
    }
    if (ddlRazaoSocial.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_raz_o_social_deve_ser_informada_;
    }
    if (mmObjeto.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_descri__o_do_objeto_deve_ser_informada_;
    }
    if (ddlUnidadeGestora.GetValue() == null || ddlUnidadeGestora.GetValue() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_unidade_gestora_deve_ser_informada_;
        //ddlInteresado.Focus();
    }
    if (ddlMunicipio.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_o_munic_pio_deve_ser_informado_;
    }
    if (ddlsegmento.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_o_segmento_deve_ser_informado_;
    }
    if (ddlSituacao.GetValue() != "P" && (ddlInicioDeVigencia.GetValue() == null || ddlInicioDeVigencia.GetText() == "")) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_data_da_os_deve_ser_informada_;
    }
    if (ddlSituacao.GetValue() != "P" && (ddlTerminoDeVigencia.GetValue() == null || ddlTerminoDeVigencia.GetText() == "")) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_data_de_t_rmino_deve_ser_informada_;
    }
    if (ddlSituacao.GetValue() != "P" && ((ddlInicioDeVigencia.GetValue() != null) && (ddlTerminoDeVigencia.GetValue() != null))) {
        if (dataInicioC > dataTerminoC) {
            numAux++;
            mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_data_da_os_n_o_pode_ser_maior_que_a_data_de_t_rmino_;
            retorno = false;
        }
    }
    if (ddlSituacao.GetValue() != "P" && (ddlAssinatura.GetValue() == null || ddlAssinatura.GetText() == "")) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_data_de_assinatura_deve_ser_informada_;
    }
    if (ddlCriterioReajuste.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_o_crit_rio_de_reajuste_deve_ser_informado_;
    }
    if (ddlTipoContratacao.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_o_tipo_de_contrata__o_deve_ser_informado_;
    }
    if (ddlOrigem.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_origem_deve_ser_informada_;
    }
    if (ddlFonte.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_a_fonte_deve_ser_informada_;
    }
    if (ddlCriterioMedicao.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_o_crit_rio_de_medi__o_deve_ser_informado_;
    }
    if (ddlGestorContrato.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.ContratosExtendido_o_gestor_deve_ser_informado_;
    }
    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = traducao.ContratosExtendido_alguns_dados_s_o_de_preenchimento_obrigat_rio_ + "\n\n" + mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
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

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario()
{
    var tOperacao = "";

    try
    {// Função responsável por preparar os campos do formulário para receber um novo registro
        txtNumeroContrato.SetText("");  
        ddlTipoContrato.SetValue(null);
        ddlSituacao.SetValue(null);
        ddlRazaoSocial.SetValue(null);
        mmObjeto.SetText("");
        ddlUnidadeGestora.SetValue(null); 
        ddlMunicipio.SetValue(null);
        ddlsegmento.SetValue(null);
        ddlInicioDeVigencia.SetValue(null);
        ddlTerminoDeVigencia.SetValue(null);
        ddlTerminoComAditivo.SetValue(null);
        ddlAssinatura.SetValue(null);
        txtValorDoContrato.SetText("");
        txtValorComAditivo.SetText("");
        ddlDataBase.SetValue(null);
        ddlCriterioReajuste.SetValue(null);
        ddlTipoContratacao.SetValue(null);
        ddlOrigem.SetValue(null);
        ddlFonte.SetValue(null);
        ddlCriterioMedicao.SetValue(null);
        ddlGestorContrato.SetValue(null);
        txtGestorContratada.SetText(""); 
        txtNumeroTrabalhadores.SetText(""); 
        txtOrigemContratada.SetText("");
        mmObservacoes.SetText("");
        ddlProjetos.SetSelectedIndex(0);
        txtnumeroInterno2.SetValue("");
        txtnumeroInterno3.SetValue("");
        indicaObra = 'N';
        statusContrato = '';
        vValorContrato = 0;
        vValorOriginal = 0;
        dtContratoAditivo = '';
        dtContratoOriginal = '';
        bPodeAlterarNumero = 'N';
        //bUsaNumeracaoAutomatica = 'N';
        numeroInterno2 = '';
        numeroInterno3 = '';
        permissoes = 0;

        if (hfGeral.Get("labelNumeroInterno2") == "") {
            document.getElementById('tdLabelNumeroInterno2').style.display = 'none';
            document.getElementById('tdNumeroInterno2').style.display = 'none';
        } else {
            document.getElementById('tdLabelNumeroInterno2').style.display = '';
            document.getElementById('tdNumeroInterno2').style.display = '';

        }
        if (hfGeral.Get("labelNumeroInterno3") == "") {
            document.getElementById('tdLabelNumeroInterno3').style.display = 'none';
            document.getElementById('tdNumeroInterno3').style.display = 'none';
        } else {
            document.getElementById('tdLabelNumeroInterno3').style.display = '';
            document.getElementById('tdNumeroInterno3').style.display = '';
        }


    }catch(e){}
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoContrato;CodigoTipoContrato;NumeroContrato;StatusContrato;CodigoStatusProjeto;CodigoPessoaContratada;DescricaoObjetoContrato;CodigoMunicipioObra;CodigoSegmentoObra;DataInicio;DataTermino;ValorContrato;DataBaseReajuste;CodigoCriterioReajusteContrato;CodigoTipoServico;CodigoOrigemObra;CodigoFonteRecursosFinanceiros;CodigoCriterioMedicaoContrato;CodigoUsuarioResponsavel;NomeContato;NomeMunicipio;NumeroTrabalhadoresDiretos;Observacao;CodigoProjeto;IndicaObra;DataAssinatura;ValorContratoOriginal;DataTerminoOriginal;CodigoUnidadeNegocio;QuantidadeAditivos;PodeAlterarNumero;Permissoes;NumeroInterno2;NumeroInterno3;GestorContrato;NomeMunicipioOrigem;UsaNumeracaoAutomatica;NomeProjeto', MontaCamposFormulario);
    }
}
/*------------------------------------------------
 Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    0- CodigoContrato;              8- CodigosegmentoContrato;            16- CodigoFonteRecursosFinanceiros;
    1- CodigoTipoContrato;          9- DataInicio;                          17- CodigoCriterioMedicaoContrato;
    2- NumeroContrato;              10- DataTermino;                        18- CodigoUsuarioResponsavel
    3- StatusContrato;              11- ValorContrato;                      19- NomeContato
    4- CodigoStatusProjeto;         12- DataBaseReajuste;                   20- NomeMunicipio
    5- CodigoPessoaContratada;      13- CodigoCriterioReajusteContrato;     21- NumeroTrabalhadoresDiretos
    6- DescricaoObjetoContrato;     14- CodigoTipoServicoContrato;          22- Observacao
    7- CodigoMunicipioObra;         15- CodigoOrigemContrato;               25- DataAssinatura
-------------------------------------------------*/
function MontaCamposFormulario(values)
{
        
        var codigoContrato                  = values[0];
        var codigoTipoContrato              = values[1];
        var numeroContrato                  = values[2];
        statusContrato                      = values[3];
        var codigoStatusProjeto             = values[4];
        var codigoPessoaContratada          = values[5];
        var descricaoObjetoContrato         = values[6];
        var codigoMunicipioObra             = values[7];
        var codigosegmentoContrato          = values[8];
        var dataInicio                      = values[9];
        var dataTermino                     = values[10];
        var valorContrato                   = values[11];
        var dataBaseReajuste                = values[12];
        var codigoCriterioReajusteContrato  = values[13];
        var codigoTipoServicoContrato       = values[14];
        var codigoOrigemContrato            = values[15];
        var codigoFonteRecursosFinanceiros  = values[16];
        var codigoCriterioMedicaoContrato   = values[17];
        var codigoUsuarioResponsavel        = values[18];
        var nomeContato                     = values[19];
        var nomeMunicipio                   = values[35];//20
        var numeroTrabalhadoresDiretos      = values[21];
        var observacao                      = values[22];
        var codigoProjeto                   = values[23];
        indicaObra                          = values[24];
        var dataAssinatura                  = values[25];
        var valorOriginal                   = values[26];
        var terminoOriginal                 = values[27];
        var codigoUnidade                   = values[28];
        qtdAditivos                         = values[29];
        vValorContrato                      = values[11];
        vValorOriginal                      = values[26];
        dtContratoAditivo                   = values[10]+"";
        dtContratoOriginal = values[27] + "";
        bPodeAlterarNumero = values[30] == true || values[30] == 1 ? "S" : "N";
        //bUsaNumeracaoAutomatica = values[36] == true || values[36] == "S" ? "S" : "N";
        permissoes                      = values[31];
        var numeroInterno2 = values[32];
        var numeroInterno3 = values[33];
        var nomeResp = values[34];
        var nomeProjeto = values[37] == null ? "" : values[37];
        ddlTipoContrato.SetValue(codigoTipoContrato);
        txtNumeroContrato.SetText(numeroContrato);
        ddlSituacao.SetValue(statusContrato);
        ddlRazaoSocial.SetValue(codigoPessoaContratada.toString());
        mmObjeto.SetText(descricaoObjetoContrato);
        ddlMunicipio.SetValue(codigoMunicipioObra);
        ddlsegmento.SetValue(codigosegmentoContrato);
        ddlUnidadeGestora.SetValue(codigoUnidade);
        txtnumeroInterno2.SetValue(numeroInterno2);
        txtnumeroInterno3.SetValue(numeroInterno3);
        
        if(dataInicio != "")
            ddlInicioDeVigencia.SetValue(dataInicio);

        if (terminoOriginal != "")
            ddlTerminoDeVigencia.SetValue(terminoOriginal);

        if (dataTermino != "" && dtContratoAditivo != dtContratoOriginal)
            ddlTerminoComAditivo.SetValue(dataTermino);
        else
            ddlTerminoComAditivo.SetValue(null);

        (valorContrato == null || valorContrato.toString() == "" || valorOriginal == valorContrato ? txtValorComAditivo.SetText("") : txtValorComAditivo.SetText(valorContrato.toString().replace('.', ',')));
        (valorOriginal == null || valorOriginal.toString() == "" ? txtValorDoContrato.SetText("0") : txtValorDoContrato.SetText(valorOriginal.toString().replace('.', ',')));
        ddlDataBase.SetValue(dataBaseReajuste);
        ddlCriterioReajuste.SetValue(codigoCriterioReajusteContrato);
        ddlTipoContratacao.SetValue(codigoTipoServicoContrato);
        ddlOrigem.SetValue(codigoOrigemContrato);
        ddlFonte.SetValue(codigoFonteRecursosFinanceiros);
        ddlCriterioMedicao.SetValue(codigoCriterioMedicaoContrato);
        ddlGestorContrato.SetValue(codigoUsuarioResponsavel.toString());
        (codigoUsuarioResponsavel == null ? ddlGestorContrato.SetText("") : ddlGestorContrato.SetText(nomeResp));
        txtGestorContratada.SetText(nomeContato);
        txtNumeroTrabalhadores.SetText(numeroTrabalhadoresDiretos + "");
        txtOrigemContratada.SetText(nomeMunicipio);
        mmObservacoes.SetText(observacao);



        if(dataAssinatura != "")
            ddlAssinatura.SetValue(dataAssinatura);

        if (codigoProjeto != null && codigoProjeto != "")
            ddlProjetos.SetValue(codigoProjeto);
        else
            ddlProjetos.SetValue("0");

        if (nomeProjeto != "")
            ddlProjetos.SetText(nomeProjeto);
        
        var readOnly = TipoOperacao == "Consultar" ? 'S' : 'N';
        
        atualizarURLParcelas = 'S';

        frmParcelasContrato = './frmParcelasContratos.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&IVG=' + ddlInicioDeVigencia.GetText() + '&ALT=' + gvDados.cp_Altura;
        
        if(pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 1)
         document.getElementById('frmParcelas').src = frmParcelasContrato;
         
         atualizarURLAnexos = 'S';

         frmAnexosContrato = '../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=CT&ID=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=' + (parseInt(gvDados.cp_Altura) - 25);
        
        if(pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 2)
            document.getElementById('frmAnexos').src = frmAnexosContrato;

        atualizarURLAditivos = 'S';

        frmAditivosContrato = './frmAditivosContrato.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=' + gvDados.cp_Altura;

        if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 3 && TipoOperacao == "Consultar")
            document.getElementById('frmAditivos').src = frmAditivosContrato;

        atualizarURLPrevisao = 'S';

        frmAditivosContrato = './frmPrevisaoFinanceira.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=' + gvDados.cp_Altura;

        if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 3 && TipoOperacao == "Consultar")
            document.getElementById('frmPrevisao').src = frmPrevisaoFinanceira;

        atualizarURLComentarios = 'S';

        frmComentariosContrato = './frmComentariosContrato.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=' + gvDados.cp_Altura;

        atualizarURLReajuste = 'S';

        frmReajuste = './frmIndiceReajusteContrato.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=' + gvDados.cp_Altura;
        document.getElementById('frmReajuste').src = frmReajuste;

        atualizarURLAcessorios = 'S';

        frmAcessorios = "./frmAcessorioCalculoPagamentoContrato.aspx?CC=" + codigoContrato + "&RO=" + readOnly + "&ALT=" + alturaFrames + '&TO=' + TipoOperacao;// './frmIndiceReajusteContrato.aspx?CC=' + codigoContrato + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=' + gvDados.cp_Altura;
        document.getElementById('frmAcessorios').src = frmAcessorios;


        if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 3 && TipoOperacao == "Consultar")
            document.getElementById('frmComentarios').src = frmComentariosContrato;

        if (codigoProjeto != null && codigoProjeto != "" && ddlProjetos.cp_MostraLink == "S") {
            document.getElementById('tdLinkProjeto').style.display = '';
            lkProjeto.SetNavigateUrl('../../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=' + codigoProjeto + '&NomeProjeto=' + ddlProjetos.GetText());
        }
        else {
            document.getElementById('tdLinkProjeto').style.display = 'none';
            lkProjeto.SetNavigateUrl("");
        }

        if (hfGeral.Get("labelNumeroInterno2") == "") {
            document.getElementById('tdLabelNumeroInterno2').style.display = 'none';
            document.getElementById('tdNumeroInterno2').style.display = 'none';
        } else {
            document.getElementById('tdLabelNumeroInterno2').style.display = '';
            document.getElementById('tdNumeroInterno2').style.display = '';
           
        }
        if (hfGeral.Get("labelNumeroInterno3") == "") {
            document.getElementById('tdLabelNumeroInterno3').style.display = 'none';
            document.getElementById('tdNumeroInterno3').style.display = 'none';
        } else {
            document.getElementById('tdLabelNumeroInterno3').style.display = '';
            document.getElementById('tdNumeroInterno3').style.display = '';
        }

        desabilitaHabilitaComponentes();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    var BoolEnabled = window.TipoOperacao && TipoOperacao != "Consultar" && statusContrato != "I";
   
    ddlTipoContrato.SetEnabled(BoolEnabled);
    if (hfGeral.Get("UsaNumeracaoAutomatica")  == "S") 
       txtNumeroContrato.SetEnabled( bPodeAlterarNumero == "S" && TipoOperacao != "Consultar");
    else 
       txtNumeroContrato.SetEnabled( TipoOperacao != "Consultar");
    ddlSituacao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlRazaoSocial.SetEnabled(BoolEnabled);
    mmObjeto.SetEnabled(BoolEnabled);
    ddlUnidadeGestora.SetEnabled(BoolEnabled);
    ddlMunicipio.SetEnabled(BoolEnabled);
    ddlsegmento.SetEnabled(BoolEnabled);
    ddlInicioDeVigencia.SetEnabled(BoolEnabled);
    ddlTerminoDeVigencia.SetEnabled(BoolEnabled && dtContratoAditivo == dtContratoOriginal);
    ddlAssinatura.SetEnabled(BoolEnabled);
    txtValorDoContrato.SetEnabled(BoolEnabled && vValorOriginal == vValorContrato );
    ddlDataBase.SetEnabled(BoolEnabled);
    ddlCriterioReajuste.SetEnabled(BoolEnabled);
    ddlTipoContratacao.SetEnabled(BoolEnabled && indicaObra != 'S');
    ddlOrigem.SetEnabled(BoolEnabled);
    ddlFonte.SetEnabled(BoolEnabled);
    ddlCriterioMedicao.SetEnabled(BoolEnabled);
    ddlGestorContrato.SetEnabled(BoolEnabled);
    txtGestorContratada.SetEnabled(false);
    txtNumeroTrabalhadores.SetEnabled(BoolEnabled);
    txtOrigemContratada.SetEnabled(false);
    mmObservacoes.SetEnabled(BoolEnabled);
    ddlProjetos.SetEnabled(BoolEnabled && indicaObra != 'S');
    txtnumeroInterno2.SetEnabled(BoolEnabled);
    txtnumeroInterno3.SetEnabled(BoolEnabled);

    if (BoolEnabled)
        document.getElementById('tdLinkProjeto').style.display = 'none';

    

}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
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
    else
    {
        if(textAreaElement.name.indexOf("mmObservacoes")>=0)
            lblCantCaraterOb.SetText(text.length);
        else
            lblCantCarater.SetText(text.length);
    }
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function onInit_mmObjeto(s, e)
{
    try{ return setMaxLength(s.GetInputElement(), 4000); }
    catch(e){}
}

function onInit_mmObservacoes(s, e)
{
    try{ return setMaxLength(s.GetInputElement(), 500); }
    catch(e){}
}

function onInit_mmEncerramento(s, e) {
    try { return setMaxLength(s.GetInputElement(), 2000); }
    catch (e) { }
}

function podeMudarAba(s, e)
{    
    if (e.tab.index == 0)
    {
        return false;
    }
    else if(TipoOperacao == 'Incluir')
    {
        window.top.mostraMensagem(traducao.ContratosExtendido_para_ter_acesso_a_op__o + " \"" + e.tab.GetText() + "\" " + traducao.ContratosExtendido___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + tabControl.GetTab(0).GetText() + "\"", 'Atencao', true, false, null);
        return true;
    }

    return false;
}

/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function OnGridFocusedRowChangedPopup(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoContrato;NumeroContrato;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor)
{
    var idObjeto     =  (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = "Contrato Nº: " + (valor[1] != null ? valor[1] : "");
    var tituloMapa   = "";
   
    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width    = 900;
    var window_height   = 590;
    var newfeatures     = 'scrollbars=no,resizable=no';
    var window_top      = (screen.height-window_height)/2;
    var window_left     = (screen.width-window_width)/2;

    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=CT&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa, traducao.ContratosExtendido_permiss_es, 920, 585, '', null);
}

function onClick_NovoContrato() {

    document.getElementById('tdBtnSalvar').style.display = '';
    document.getElementById('tdBtnImprimir').style.display = 'none';
    tabControl.SetActiveTabIndex(0);
    TipoOperacao = "Incluir";
    hfGeral.Set("TipoOperacao", "Incluir");
    ddlUnidadeGestora.PerformCallback("");
    onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);    
    desabilitaHabilitaComponentes();
}

function carregaDadosFornecedor(municipioFornecedor, contatoFornecedor)
{
     txtGestorContratada.SetText(contatoFornecedor); 
     txtOrigemContratada.SetText(municipioFornecedor); 
}

function novaRazaoSocial()
{
    window.top.showModal('../Administracao/frmCadastroPessoa.aspx', 'Nova Razão Social', 900, 435, funcaoPosModal, null);
}

function funcaoPosModal(valor)
{
    if(valor != null && valor != '')
        ddlRazaoSocial.PerformCallback(valor.toString());
}

function onClick_btnSalvar_MSR() {
    gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoContrato', MontaCamposFormularioRelatorio);
    }

function MontaCamposFormularioRelatorio(codigoContrato) {

        popUp = window.open("../DadosProjeto/popupRelContratos.aspx?codigoContrato=" + codigoContrato, "form", 'resizable=yes,menubar=no,scrollbars=yes,toolbar=no,width=800,height=' + (screen.height - 200));
}
