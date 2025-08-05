// JScript File

function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    //LimpaCamposFormulario();
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    var posErro = 0;
    mensagemErro_ValidaCamposFormulario = "";
    
    if(txtDado.GetText() == "")
        mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.DadosIndicadoresOperacional_campo_dado___de_preenchimento_obrigat_rio_;
    
    if(cmbUnidadeDeMedida.GetSelectedIndex() == -1)
        mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.DadosIndicadoresOperacional_selecione_um_item_no_campo_unidade_de_medida_;
    
    if(cmbCasasDecimais.GetSelectedIndex() == -1)
        mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.DadosIndicadoresOperacional_selecione_um_item_no_campo_casas_decimais_;
    
    if(cmbAgrupamentoDoDado.GetSelectedIndex() == -1)
        mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.DadosIndicadoresOperacional_selecione_um_item_no_campo_agrupamento_do_dado_;
    
    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{
    txtDado.SetEnabled(TipoOperacao != "Consultar");
    cmbUnidadeDeMedida.SetEnabled(TipoOperacao != "Consultar");
    cmbCasasDecimais.SetEnabled(TipoOperacao != "Consultar");
    cmbAgrupamentoDoDado.SetEnabled(TipoOperacao != "Consultar");
    //heGlossario.SetEnabled(TipoOperacao != "Consultar");
    txtCodigoReservado.SetEnabled(TipoOperacao != "Consultar");
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registr
    
    txtDado.SetText("");
    cmbUnidadeDeMedida.SetSelectedIndex(-1);
    cmbCasasDecimais.SetSelectedIndex(-1);
    cmbAgrupamentoDoDado.SetSelectedIndex(-1);
    heGlossario.SetHtml("");
    txtCodigoReservado.SetText("");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if(forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
         grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoDado;GlossarioDado;CodigoUnidadeMedida;CasasDecimais;CodigoFuncaoAgrupamentoDado;CodigoReservado', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    
    txtDado.SetText(values[0]);
        
    if(window.heGlossario)
        heGlossario.SetHtml((values[1] != null ? values[1]  : ""));

    cmbUnidadeDeMedida.SetValue(values[2]);
    cmbCasasDecimais.SetValue(values[3]);
    cmbAgrupamentoDoDado.SetValue(values[4]);
    txtCodigoReservado.SetText(values[5]);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
//    if (gridDescricao.GetVisibleRowsOnPage() > 0 )
//        onClick_btnCancelar();    
}

// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------

function onloadDesabilitaBarraNavegacao()
{
     if(gvDados.GetVisibleRowsOnPage() == 0)
		{
			desabilitaBarraNavegacao(true);
		}
		else
		{
			desabilitaBarraNavegacao(false);
		}
}

function desabilitaBarraNavegacao(habilitar)
{
    if (habilitar==true)
    {        
        try{imgBtnFormulario.SetImageUrl(PathImagesUrl +'/pFormularioDisabled.png');}catch(e){}
        try{imgBtnPrimeiro.SetImageUrl(PathImagesUrl +'/pFirstDisabled.png');}catch(e){}
        try{imgBtnAnterior.SetImageUrl(PathImagesUrl +'/pPrevDisabled.png');}catch(e){}
        try{imgBtnProximo.SetImageUrl(PathImagesUrl +'/pNextDisabled.png');}catch(e){}
        try{imgBtnUltimo.SetImageUrl(PathImagesUrl +'/pLastDisabled.png');}catch(e){}
        try{imgBtnIncluir.SetImageUrl(PathImagesUrl +'/pIncluir.png');}catch(e){}
        try{imgBtnEditar.SetImageUrl(PathImagesUrl +'/pEditarDisabled.png');}catch(e){}
        try{imgBtnExcluir.SetImageUrl(PathImagesUrl +'/pExcluirDisabled.png');}catch(e){}
        try{imgBtnSalvar.SetImageUrl(PathImagesUrl +'/pSalvarDisabled.png');}catch(e){}
        try{imgBtnCancelar.SetImageUrl(PathImagesUrl +'/pCancelarDisabled.png');}catch(e){}
    }
    else
    {
        try{imgBtnFormulario.SetImageUrl(PathImagesUrl +'/pFormulario.png');}catch(e){}
        try{imgBtnPrimeiro.SetImageUrl(PathImagesUrl +'/pFirst.png');}catch(e){}
        try{imgBtnAnterior.SetImageUrl(PathImagesUrl +'/pPrev.png');}catch(e){}
        try{imgBtnProximo.SetImageUrl(PathImagesUrl +'/pNext.png');}catch(e){}
        try{imgBtnUltimo.SetImageUrl(PathImagesUrl +'/pLast.png');}catch(e){}
        try{imgBtnIncluir.SetImageUrl(PathImagesUrl +'/pIncluir.png');}catch(e){}
        try{imgBtnEditar.SetImageUrl(PathImagesUrl +'/pEditar.png');}catch(e){}
        try{imgBtnExcluir.SetImageUrl(PathImagesUrl +'/pExcluir.png');}catch(e){}
        try{imgBtnSalvar.SetImageUrl(PathImagesUrl +'/pSalvarDisabled.png');}catch(e){}
        try{imgBtnCancelar.SetImageUrl(PathImagesUrl +'/pCancelarDisabled.png');}catch(e){}
        TipoOperacao = "";
    }
    
    try{imgBtnFormulario.SetEnabled(habilitar==false);}catch(e){}
    try{imgBtnPrimeiro.SetEnabled(habilitar==false);}catch(e){}
    try{imgBtnAnterior.SetEnabled(habilitar==false);}catch(e){}
    try{imgBtnProximo.SetEnabled(habilitar==false);}catch(e){}
    try{imgBtnUltimo.SetEnabled(habilitar==false);}catch(e){}
    try{imgBtnIncluir.SetEnabled(true);}catch(e){}
    try{imgBtnEditar.SetEnabled(habilitar==false);}catch(e){}
    try{imgBtnExcluir.SetEnabled(habilitar==false);}catch(e){}
    try{imgBtnSalvar.SetEnabled(false);}catch(e){}
    try{imgBtnCancelar.SetEnabled(false);}catch(e){}
}

//-------------------- DIV salvar
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar()
} 
//**** fim DIV salvar