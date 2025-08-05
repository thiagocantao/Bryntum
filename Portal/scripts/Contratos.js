//======================================
//      AÇÕES COM O BANCO DE DADOS.
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function ExcluirRegistroSelecionado()
{
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

//======================================
//      VALIDATION'S.
//--------------------------------------

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onValidation_NumeroAditivo(s, e)
{
    var parcela = txtNumeroParcela.GetText();
    var parcelas = hfGeral.Get("ListaDeParcelas");
    var tipoOperacionEmParcela = hfGeral.Get("TipoOperacaoEmParcela");
    var verificar = false;
    
    if(tipoOperacionEmParcela != "Editar")
    {
        if(parcelas && parcelas.length > 0)
        {
            var listaParcelas = parcelas.split(";");
            for ( var i in listaParcelas)
            {
                //if(e.value == listaParcelas[i])
                if((e.value + parcela) == listaParcelas[i])
                    verificar = true;
            }
        }
        if(verificar)
        {
    	    e.isValid = false;
	        e.errorText = 'Parcela já cadastrada!';
        }
        else
	        e.isValid = true;
	}
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onValidation_NumeroParcela(s, e)
{
    var aditivo = txtNumeroAditivo.GetText();
    var parcelas = hfGeral.Get("ListaDeParcelas");
    var tipoOperacionEmParcela = hfGeral.Get("TipoOperacaoEmParcela");
    var verificar = false;
    
    if(tipoOperacionEmParcela != "Editar")
    {
	    if (isNaN(e.value) || parseInt(e.value) == 0){
		    e.isValid = false;
		    e.errorText = 'Campo obrigatório!';
	    } else {
	        if(parcelas && parcelas.length > 0)
	        {
	            var listaParcelas = parcelas.split(";");
	            for ( var i in listaParcelas)
                {
                    //if(e.value == listaParcelas[i])
                    if((aditivo + e.value) == listaParcelas[i])
                        verificar = true;
                }
            }
            if(verificar)
            {
        	    e.isValid = false;
		        e.errorText = traducao.Contratos_parcela_ou_aditivo_j__cadastrada_;
            }
            else
		        e.isValid = true;
	    }
	}
}


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onValidation_ValorPrevisto(s, e)
{
	if(e.value == null || parseInt(e.value) == 0){
		e.isValid = false;
		e.errorText = traducao.Contratos_campo_obrigat_rio_;
	} else {
		e.isValid = true;
	}
}

function onValidation_DataVencimento(s, e)
{
    var valueData = (e.value == null   || parseInt(e.value) == 0);
    
    if(valueData){
		e.isValid = false;
		e.errorText = traducao.Contratos_campo_obrigat_rio_;
	} else {
		e.isValid = true;
	}
}

function onValidation_DataPagamentoGvParcela(s, e)
{
    var test        = txtValorPagoGvParcela.GetText();
    var valorPago   = (test == null || parseInt(test) == 0);
    var value       = (e.value == null   || parseInt(e.value) == 0);
    
    if(valorPago && !value)
    {
		e.isValid = false;
		e.errorText = traducao.Contratos_campo_valor_pago_deve_ser_preenchido_;
	} else {
		onValidation_PosDataPagamento();
		e.isValid = true;
	}
}
	
function onValidation_ValorPagoGvParcela(s, e)
{
    var test            = ddlDataPagamento.GetValue();
    var dataPagamento   = (test == null || parseInt(test) == 0);
    var value           = (e.value == null   || parseInt(e.value) == 0);
    
    if(dataPagamento && !value){
		e.isValid = false;
		e.errorText = traducao.Contratos_campo_data_de_pagamento_deve_ser_preenchido_;
	} else {
	    onValidation_PosValorPago();
		e.isValid = true;
	}
}

function onValidation_PosDataPagamento()
{
    var value = (txtValorPagoGvParcela.GetValue() == null) || (parseInt(txtValorPagoGvParcela.GetValue()) == 0);
    
    if(!value){
		txtValorPagoGvParcela.SetIsValid(true);
	}
}

function onValidation_PosValorPago()
{
    var value = (ddlDataPagamento.GetValue() == null) || (parseInt(ddlDataPagamento.GetValue()) == 0);
    
    if(!value){
		ddlDataPagamento.SetIsValid(true);
	}
}


//======================================
//      MANIPULAÇÃO DOS CLICK'S (EVENTOS JAVASCRIPT'S).
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onClick_NovoContrato(){
    TipoOperacao = "Incluir";
    onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
    
    hfGeral.Set("CodigoContrato", "-1");
    gvParcelas.PerformCallback("CARREGAR");
}


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onClick_CustomButtomGridParcelas(s, e)
{
    var tipoOperacaoParcela = hfGeral.Get("TipoOperacaoParcelas");
     
     if((e.buttonID == "btnDetalheCustom"))
     {	
        try
        {
            if(tipoOperacaoParcela != "Consultar")
                hfGeral.Set("TipoOperacaoParcelas", "ConsultarEdicao");
            gvParcelas.StartEditRow(gvParcelas.GetFocusedRowIndex());
        }
        catch(e){}
     }	
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onClick_Cancelar(s, e)
{
    pcDados.Hide();
    callbackGeral.PerformCallback("CerrarSession");
    hfGeral.Set("ListaDeParcelas", "");
    hfGeral.Set("TipoOperacaoEmParcela", "");
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onClick_Salvar(s, e)
{
    var mensagemErro = verificarDadosPreenchidos(); 
    if(mensagemErro =="")
    {
        if (window.onClick_btnSalvar)
            onClick_btnSalvar();
    }
    else
    {
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
        e.processOnServer = false;
    }
}



//======================================
//      ESTADOS NOS COMPONENTES.
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function desabilitaHabilitaComponentes()
{
	var BoolEnabled = hfGeral.Get("TipoOperacao") != "Consultar";
   
    txtNumeroContrato.SetEnabled(BoolEnabled);
    ddlTipoContrato.SetEnabled(BoolEnabled);
    ddlSituacao.SetEnabled(BoolEnabled);
    ddlModalidadAquisicao.SetEnabled(BoolEnabled);
    txtFornecedor.SetEnabled(BoolEnabled);
    mmObjeto.SetEnabled(BoolEnabled);
    ddlProjetos.SetEnabled(BoolEnabled);
    ddlUnidadeGestora.SetEnabled(BoolEnabled);
    ddlInicioDeVigencia.SetEnabled(BoolEnabled);
    ddlTerminoDeVigencia.SetEnabled(BoolEnabled);
    txtResponsavel.SetEnabled(BoolEnabled);
    txtSAP.SetEnabled(BoolEnabled);
    mmObservacoes.SetEnabled(BoolEnabled);
    ddlFontePagadora.SetEnabled(BoolEnabled);
    txtValorDoContrato.SetEnabled(BoolEnabled);
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function verificarDadosPreenchidos()
{
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
    
    if(txtNumeroContrato.GetText() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_o_n_mero_do_contrato_deve_ser_informado_;
        //ddlInteresado.Focus();
    }
    if(ddlTipoContrato.GetValue() == null || ddlTipoContrato.GetText() == "Selecionar..." || ddlTipoContrato.GetValue() == 0 )
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_o_tipo_de_contrato_deve_ser_informada_;
        //ddlInteresado.Focus();
    }
    if(ddlModalidadAquisicao.GetValue() == null || ddlModalidadAquisicao.GetValue() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_a_modalidade_de_aquisi__o_deve_ser_informada_;
        //ddlInteresado.Focus();
    }
    if(txtFornecedor.GetText() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_o_fornecedor_deve_ser_informado_;
        //ddlInteresado.Focus();
    }
    if(mmObjeto.GetText() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_a_descri__o_do_objeto_deve_ser_informada_;
        //ddlInteresado.Focus();
    }
    if(ddlProjetos.GetValue() == null || ddlProjetos.GetValue() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_o_projeto_deve_ser_informado_;
        //ddlInteresado.Focus();
    }
    if(ddlUnidadeGestora.GetValue() == null || ddlUnidadeGestora.GetValue() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_a_unidade_gestora_deve_ser_informada_;
        //ddlInteresado.Focus();
    }
    if(ddlFontePagadora.GetValue() == null || ddlFontePagadora.GetValue() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_a_fonte_pagadora_deve_ser_informada_;
        //ddlInteresado.Focus();
    }
    if(ddlInicioDeVigencia.GetValue() == null || ddlInicioDeVigencia.GetText() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_a_data_de_in_cio_de_vig_ncia_deve_ser_informada_;
        //ddlInteresado.Focus();
    }
    if((ddlInicioDeVigencia.GetValue() != null) && (ddlTerminoDeVigencia.GetValue() != null))
    {
        if(dataInicioC > dataTerminoC)
        {
            mensagem += "\n" + numAux + ") " + traducao.Contratos_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n";
            retorno = false;
        }
    }
    if(txtResponsavel.GetText() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Contratos_o_respons_vel_deve_ser_informado_;
        //ddlInteresado.Focus();
    }
    if(mensagem != "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.Contratos_alguns_dados_s_o_de_preenchimento_obrigat_rio_ + "\n\n" + mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}



//======================================
//      PROCURAR USUARIO (LOV.ASPX)
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function buscaNomeBD(objeto)
{
    hfGeral.Set("CodigoResponsavel","");
    nome = objeto.GetText();
    if (nome!="")
        callbackResponsavel.PerformCallback();
    else
        mostraLov();    
}

var retornoPopUp = null;

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function mostraLovResponsavel(param)
{         
    var where = hfGeral.Get("ComandoWhere");               
    
    if(window.showModalDialog)
    {
        retornoPopUp = window.showModalDialog('../../lov.aspx?tab=usuario&val=codigoUsuario&nom=nomeUsuario&whe=' + where + '&ord=nomeUsuario&Pes=' + txtResponsavel.GetText(), '', 'resizable:0; dialogWidth:520px; dialogHeight:465px; status:no; menubar:no;');
        atualizaDadosLov();
    }
    else
    {
        window.open('../../lov.aspx?tab=usuario&val=codigoUsuario&nom=nomeUsuario&whe=' + where + '&ord=nomeUsuario&Pes=' + txtResponsavel.GetText(), 'LOV', 'resizable=0,width=520,height=465,status=no,menubar=no');
    }        
    
}

function atualizaDadosLov()
{    
    if (retornoPopUp && retornoPopUp != "")
    {
        var aRetorno = retornoPopUp.split(';');
        hfGeral.Set("CodigoResponsavel", aRetorno[0]);
        txtResponsavel.SetText(aRetorno[1]); 
    }
    else
    {
        txtResponsavel.SetText(""); 
        hfGeral.Set("CodigoResponsavel", "");
    }
}


//==============================================
//      TRATAMENTO DOS REGISTRO DA DIV GVDADOS
//----------------------------------------------


/*-------------------------------------------------
<summary>colocar los campos em branco o limpos, en momento de inserir un nuevo dado.</summary>
-------------------------------------------------*/
function LimpaCamposFormulario()
{
    var tOperacao = ""
    var CantUnidadesGestora = 0

    try
    {// Função responsável por preparar os campos do formulário para receber um novo registro
        txtNumeroContrato.SetText("");
        ddlTipoContrato.SetValue("0");
        ddlSituacao.SetValue("A");
        ddlModalidadAquisicao.SetValue("");
        txtFornecedor.SetText("");
        mmObjeto.SetText("");
        ddlProjetos.SetSelectedIndex(0);
        
        if(window.hfGeral && hfGeral.Contains("CantUnidadesGestora"))
            CantUnidadesGestora = hfGeral.Get("CantUnidadesGestora");
            
        ddlUnidadeGestora.SetValue(null); 
        txtResponsavel.SetText("");
        ddlFontePagadora.SetText("");
        ddlInicioDeVigencia.SetValue(null);
        ddlTerminoDeVigencia.SetValue(null);
        txtSAP.SetText("");
        mmObservacoes.SetText("");
        txtValorDoContrato.SetText("0");
    }catch(e){}
}


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
            grid.GetRowValues(grid.GetFocusedRowIndex(),'NomeUsuario;NomeUnidadeNegocio;NomeFonte;DescricaoTipoAquisicao;CodigoContrato;NomeProjeto;NumeroContrato;StatusContrato;Fornecedor;DataInicio;DataTermino;CodigoUsuarioResponsavel;DescricaoObjetoContrato;CodigoTipoAquisicao;CodigoUnidadeNegocio;CodigoFonteRecursosFinanceiros;Observacao;NumeroContratoSAP;CodigoProjeto;CodigoTipoContrato;ValorContrato;', MontaCamposFormulario);
    }
}

/*------------------------------------------------
 Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    0- NomeUsuario;                 8- Fornecedor;                          16- Observacao;
    1- NomeUnidadeNegocio;          9- DataInicio;                          17- NumeroContratoSAP;
    2- NomeFonte;                   10- DataTermino;                        18- CodigoProjeto
    3- DescricaoTipoAquisicao;      11- CodigoUsuarioResponsavel;           19- CodigoTipoContrato
    4- CodigoContrato;              12- DescricaoObjetoContrato;            20- ValorContrato
    5- NomeProjeto;                 13- CodigoTipoAquisicao;
    6- NumeroContrato;              14- CodigoUnidadeNegocio;
    7- StatusContrato;              15- CodigoFonteRecursosFinanceiros;
-------------------------------------------------*/
function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    
        var parametros      = "";
        
        //------------Obtemgo os valores da grid.
        
        var codigoContrato  = (values[4]  != null ? values[4]  : "");
        var numeroContrato  = (values[6]  != null ? values[6]  : "");
        var situacao        = (values[7]  != null ? values[7]  : "");
        var aquisicao       = (values[13] != null ? values[13] : "");
        var fornecedor      = (values[8]  != null ? values[8]  : "");
        var unidadeGestora  = (values[14] != null ? values[14] : "");
        var responsavel     = (values[0]  != null ? values[0]  : "");
        var descricaoObj    = (values[12] != null ? values[12] : "");
        var fonte           = (values[2]  != null ? values[2]  : "");
        var observacoes     = (values[16] != null ? values[16] : "");
        var ValorContrato   = values[20];
        var CodigoFonteRecursosFinanceiros  = (values[15]  != null ? values[15]  : "");
        var codigoUsuarioResponsavel        = (values[11]  != null ? values[11]  : "");
        var NumeroContratoSAP               = (values[17]  != null ? values[17]  : "");
        var codigoProjeto                   = (values[18]  != null ? values[18]  : "0");
        var codigoTipoContrato              = (values[19]  != null ? values[19]  : "0");
        var NomeUnidadeNegocio              = (values[1]   != null ? values[1]  : "");

        //----------prencho os objetos.
        txtNumeroContrato.SetText(numeroContrato);
        ddlTipoContrato.SetValue(codigoTipoContrato);
        ddlSituacao.SetValue(situacao);
        ddlModalidadAquisicao.SetValue(aquisicao);
        txtFornecedor.SetText(fornecedor);
        mmObjeto.SetText(descricaoObj);
        ddlProjetos.SetValue(codigoProjeto);
        
        try
        {
        if(TipoOperacao == "Consultar")
            ddlUnidadeGestora.SetText(NomeUnidadeNegocio);
        else 
            ddlUnidadeGestora.SetValue(unidadeGestora);
        }catch(e){}
        
        if(values[9] == null || values[9] == "")
            ddlInicioDeVigencia.SetText("");
        else
            ddlInicioDeVigencia.SetValue(values[9]);
        if(values[10] == null || values[10] == "")
            ddlTerminoDeVigencia.SetText("");
        else
            ddlTerminoDeVigencia.SetValue(values[10]);
        txtResponsavel.SetText(responsavel);
        txtSAP.SetText(NumeroContratoSAP);
        mmObservacoes.SetText(observacoes);
        (ValorContrato == null || ValorContrato.toString() == "" ? txtValorDoContrato.SetText("0") : txtValorDoContrato.SetText(ValorContrato.toString().replace('.', ',')));
        
        ddlFontePagadora.SetValue(CodigoFonteRecursosFinanceiros);
       
        if(window.hfGeral && hfGeral.Contains("TipoOperacao"))
            hfGeral.Set("TipoOperacao", TipoOperacao);
        if(window.hfGeral && hfGeral.Contains("CodigoResponsavel"))
            hfGeral.Set("CodigoResponsavel", codigoUsuarioResponsavel);
        if(window.hfGeral && hfGeral.Contains("CodigoContrato"))
            hfGeral.Set("CodigoContrato", codigoContrato);
        hfAnexos.Set("IDObjetoAssociado",codigoContrato);
        if(codigoContrato != "")
            gvParcelas.PerformCallback("CARREGAR");
            
        __isEdicao = false;      
     
}



//======================================
//      CALLBACK'S
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onEnd_pnCallbackLocal(s, e)
{
    if("Incluir" == s.cp_OperacaoOk)
    {
        mostraPopupMensagemGravacao(traducao.Contratos_dados_inclu_dos_com_sucesso_);
        TipoOperacao = "Editar";
        hfGeral.Set("TipoOperacao", TipoOperacao);
        hfGeral.Set("ListaDeParcelas", "");
        OnGridFocusedRowChanged(gvDados);
    }
    else if("Editar" == s.cp_OperacaoOk)
        mostraPopupMensagemGravacao(traducao.Contratos_dados_alterados_com_sucesso_);	    
    else if("Excluir" == s.cp_OperacaoOk)
        mostraPopupMensagemGravacao(traducao.Contratos_dados_excluidos_com_sucesso_);	  
            
    //callbackGeral.PerformCallback("CerrarSession");
    
    //if(s.cp_lovCodigoResponsavel != "-1")
	//{
    //  hfGeral.Set("lovMostrarPopPup", s.cp_lovMostrarPopPup);
	//	hfGeral.Set("lovCodigoResponsavel", s.cp_lovCodigoResponsavel);
	//}
}


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onEnd_CallbackResponsavel(s, e)
{
	if(s.cp_Codigo != "")
	{
		hfGeral.Set("CodigoResponsavel", s.cp_Codigo);
		txtResponsavel.SetText(s.cp_Nome);
	}
	else
	{
		mostraLovResponsavel(s.cp_Where);
	}
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onEnd_CallbackGvParcelas(s, e)
{
    if(window.hfGeral && hfGeral.Contains("ListaDeParcelas"))
    {
        var listaDeParcelas = hfGeral.Get("ListaDeParcelas");
        //if(listaDeParcelas == "")
        hfGeral.Set("ListaDeParcelas", s.cp_parcelas);
            
	    var tipoOperacao = hfGeral.Get("TipoOperacaoParcelas");
        if(tipoOperacao == "ConsultarEdicao")
		    hfGeral.Set("TipoOperacaoParcelas", "");
		    
        hfGeral.Set("TipoOperacaoEmParcela", s.cp_tipoOperacaoEmParcela);
    }
    else
    {
        hfGeral.Set("ListaDeParcelas", s.cp_parcelas);
        
        var tipoOperacao = hfGeral.Get("TipoOperacaoParcelas");
        if(tipoOperacao == "ConsultarEdicao")
		    hfGeral.Set("TipoOperacaoParcelas", "");
    }
    if(s.cp_Edicao == "OK")
        __isEdicao = true;
}



//======================================
//      DIV'S
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function mostraPopupMensagemGravacao(acao)
{
    lblAcaoGravacao.SetText(acao);
    pcUsuarioIncluido.Show();
    setTimeout ('fechaTelaEdicao();', 1500);
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function fechaTelaEdicao()
{
    pcUsuarioIncluido.Hide();
    //onClick_btnCancelar()
}



//======================================
//      FUNÇÕES VARIAS
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function podeMudarAba(s, e)
{
     var codigoContrato = parseFloat(hfGeral.Get("CodigoContrato"));
     btnSalvar.SetVisible(TipoOperacao != "Consultar"? true:false);
     btnCancelar.SetText(traducao.Contratos_fechar);
    
    if (e.tab.index == 0)
    {
        return false;
    }
        
    if(e.tab.index == 2)
    {
        if (isNaN(codigoContrato) || codigoContrato.length <= 0 || parseFloat(codigoContrato)<0)
        {
            window.top.mostraMensagem(traducao.Contratos_para_ter_acesso_a_op__o + " \"" + e.tab.GetText() + "\" " + traducao.Contratos___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + tabControl.GetTab(0).GetText() + "\"", 'Atencao', true, false, null);
             return true;
        }
        else
        {
            if(__isEdicao)
            {
                window.top.mostraMensagem(traducao.Contratos_para_ter_acesso_a_op__o + " \"" + e.tab.GetText() + "\" " + traducao.Contratos___obrigat_rio_salvar_as_informa__es_alteradas_da_op_oes + " \"" + tabControl.GetTab(0).GetText() + "\" e \"" + tabControl.GetTab(1).GetText() + "\"", 'Atencao', true, false, null);
                return true;
            }
            else
            {
                pnCallback1.PerformCallback("Listar");
                btnCancelar.SetText("Fechar");
                btnSalvar.SetVisible(false);
                return false;
            }
        }  
    }

//    if(e.tab.index == 3)
//    {
//    	e.processOnServer = false;
//	    onClick_Salvar(s, e);
//    }
    
    if (isNaN(codigoContrato) || codigoContrato.length <= 0 || parseFloat(codigoContrato)<0)
    {
        window.top.mostraMensagem(traducao.Contratos_para_ter_acesso___op__o + " \"" + e.tab.GetText() + "\", " + traducao.Contratos___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + tabControl.GetTab(0).GetText() + "\"", 'Atencao', true, false, null);
        return true;
    }

    if(!window.TipoOperacao)
        TipoOperacao = "Consultar";

    return false;
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function onValidation_DataPagamento(s, e)
{
    var dataPagamento = new Date(s.GetValue());
    //var dataPagamentoP = (dataPagamento.getMonth() + 1) + "/" + dataPagamento.getDate() + "/" + dataPagamento.getFullYear();
    //var dataPagamentoC = Date.parse(dataPagamentoP);
    var dataAtual = new Date
    
    if(dataPagamento > dataAtual)
    {
		e.isValid = false;
		e.errorText = traducao.Contratos_a_data_de_pagamento_deve_ser_menor_ou_igual___data_atual_;
	} else {
		e.isValid = true;
	}
}


//======================================
//      CONTROLE DE CANTIDADE DE CARACTERES DIGITADOS.
//--------------------------------------

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
    try{ return setMaxLength(s.GetInputElement(), 500); }
    catch(e){}
}

function onInit_mmObservacoes(s, e)
{
    try{ return setMaxLength(s.GetInputElement(), 500); }
    catch(e){}
}


/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function OnGridFocusedRowChangedPopup(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoContrato;NomeFonte;DescricaoTipoAquisicao;NumeroContrato;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor)
{
    var idObjeto     =  (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "") +  " [" + (valor[2] != null ? valor[2] : "") + "] - Nº: " + (valor[3] != null ? valor[3] : "");
    var tituloMapa   = "";
   
    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width    = 900;
    var window_height   = 590;
    var newfeatures     = 'scrollbars=no,resizable=no';
    var window_top      = (screen.height-window_height)/2;
    var window_left     = (screen.width-window_width)/2;

    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=CT&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa, traducao.Contratos_permiss_es, 920, 585, '', null);
}


