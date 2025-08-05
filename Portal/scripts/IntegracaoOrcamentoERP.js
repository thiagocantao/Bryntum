// JScript File

//======================================
//      BOTOES
//--------------------------------------

/*-------------------------------------------------
<summary>
Esta função trata o click feito no botón editar da grid pricipal 'gvDados'.
O botón qeu e um customButtom da grid, chama ao objeto 'pnCallbackPopup'
enviando como parâmetro indicando que vai excluir o registro.
</summary>
-------------------------------------------------*/
function onClick_CustomButtomGvDados(s, e)
{
     if(e.buttonID == "btnExcluirCustom")
      if (confirm(traducao.IntegracaoOrcamentoERP_deseja_realmente_excluir_o_registro_))
		pnCallbackPopup.PerformCallback("excluirCR");
}

/*-------------------------------------------------
<summary>
Esta função trata o click feito no botón 'salvarCR', ubicado no popup
'pcDados', chama ao objeto 'pnCallbackPopup', enviando como parâmetro
qeu o dado prenchido no formulario vai ser salvo no banco.
</summary>
-------------------------------------------------*/
function onClick_btnSalvar()
{
     pnCallbackPopup.PerformCallback("salvarCR");   
}

/*-------------------------------------------------
<summary>
Esta função trata o click feito no botón 'novo', ubicado na grid
'gvDados', chama ao objeto 'pnCallbackPopup', enviando como parâmetro
qeu inicie o proceso de inserir um novo dado.
</summary>
-------------------------------------------------*/
function novaIntegracao()
{
    pnCallbackPopup.PerformCallback("iniciarPopup");
}


//======================================
//      CALBACK'S
//--------------------------------------


/*-------------------------------------------------
<summary>
Esta função trata o fim do callback 'CallbackPopup', segundo os botoes qeu forom
feitos click, fechara aquela opção indicando mensajes, visualizando ou ocultado
objetos.
</summary>
-------------------------------------------------*/
function onEnd_CallbackPopup(s, e)
{
	if("iniciarPopup" == s.cp_OperacaoOk)
		pcDados.Show();
	else if("salvarCR" == s.cp_OperacaoOk)
	{
	    pcDados.Hide();
	    gvDados.PerformCallback();
	    mostraDivSalvoPublicado(traducao.IntegracaoOrcamentoERP_dados_gravados_com_sucesso_)
	}
	else if("excluirCR" == s.cp_OperacaoOk)
	{
	    gvDados.PerformCallback();
	    mostraDivSalvoPublicado(traducao.IntegracaoOrcamentoERP_registro_exclu_do_com_sucesso_)
	}
}

/*-------------------------------------------------
<summary>
Esta função trata o fim do callback 'CallbackCR', ao recaragar o comboBox
'ddlListagemCR', ele reinicia o control de mensaje de codigoCr utilizado
asim como do botão 'SalvarCR' (do objeto panel 'pcDados')
</summary>
-------------------------------------------------*/
function onEnd_CallbackCR(s, e)
{
    pnCallbackEstadoCR.PerformCallback("-1");
}


//======================================
//      COMBOBOX'S
//--------------------------------------


/*-------------------------------------------------
<summary>
Esta função trata o momento de seleccionar um novo elemento do
comboBox 'ddlMovimientoOrcamentario', llamando ao objeto callback
'pncallbackCR'.
O que vai fazer o objeto callback, e carregar um outro combo o
'ddlListagemCR', usando como parâmetro o codigo otido no
'ddlMovimientoOrcamentario'
</summary>
-------------------------------------------------*/
function onChanged_ddlMovimientoOrcamentario(s, e)
{
    var codigoInteressado = ddlMovimientoOrcamentario.GetValue();
	pnCallbackCR.PerformCallback(codigoInteressado);
}

/*-------------------------------------------------
<summary>
Esta função trata o momento de seleccionar um novo elemento do
comboBox 'ddlListagemCR', llamando ao objeto callback 'pncallbackEstadoCR'.
O que vai fazer o objeto callback, e verificar que o CR não exista ja 
em algúm projeto da entidade logada.
</summary>
-------------------------------------------------*/
function onChanged_ddlListagemCR(s, e)
{
	if(window.hfGeral && window.hfGeral.Contains('codigoCR'))
		window.hfGeral.Set('codigoCR', s.GetValue());
	pnCallbackEstadoCR.PerformCallback(s.GetValue());
}


//======================================
//      VARIOS
//--------------------------------------


function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}