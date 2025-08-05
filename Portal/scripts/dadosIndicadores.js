// JScript File
var nomeDadoEdicao = "";
var crDadoEdicao = "";

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
        mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_campo_dado___de_preenchimento_obrigat_rio_;
    else
    {
        if(hfGeral.Get("NomeDados") != "")
        {
            var listaNomes = hfGeral.Get("NomeDados").split('¥');
            var countNomes = 0;
            for(i = 0; i < listaNomes.length; i++)
            {
                if(listaNomes[i] == txtDado.GetText())
                    countNomes = 1;
            }
            
            if((TipoOperacao == "Incluir" && countNomes == 1) || (TipoOperacao == "Editar" && countNomes == 1 && nomeDadoEdicao != txtDado.GetText()))
                mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_j__existe_um_dado_com_este_nome_;
        }
    }
    if(txtCodigoReservado.GetText() != "")
    {
        if(hfGeral.Get("CRsDados") != "")
        {
            var listaNomes = hfGeral.Get("CRsDados").split('¥');
            var countNomes = 0;
            for(i = 0; i < listaNomes.length; i++)
            {
                if(listaNomes[i] == txtCodigoReservado.GetText())
                    countNomes = 1;
            }
            
            if((TipoOperacao == "Incluir" && countNomes == 1) || (TipoOperacao == "Editar" && countNomes == 1 && crDadoEdicao != txtCodigoReservado.GetText()))
                mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_j__existe_um_dado_com_este_c_digo_reservado_;
        }
    }
    
    if(cmbUnidadeDeMedida.GetSelectedIndex() == -1)
        mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_selecione_um_item_no_campo_unidade_de_medida_;
    
    if(cmbCasasDecimais.GetSelectedIndex() == -1)
        mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_selecione_um_item_no_campo_casas_decimais_;
    
    if(cmbAgrupamentoDoDado.GetSelectedIndex() == -1)
        mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_selecione_um_item_no_campo_agrupamento_do_dado_;
        
    var vlMax = txtValorMaximo.GetText();
    var vlMin = txtValorMinimo.GetText();    
     
    if(vlMax != "" && vlMin != "")
    {
        var controle = 'S';
        
        if(parseFloat(vlMin) + "" == "NaN" || parseFloat(vlMin) == null)
        {
            mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_valor_m_nimo_inv_lido_;
            controle = 'N';
        }
        
        if(parseFloat(vlMax) + "" == "NaN" || parseFloat(vlMax) == null)
        {
            mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_valor_m_ximo_inv_lido_;
            controle = 'N';
        }
        
        if(controle == 'S')
        {
            if(parseFloat(vlMax) < parseFloat(vlMin))
            {
                mensagemErro_ValidaCamposFormulario += "\n" + posErro++ + ") " + traducao.dadosIndicadores_o_valor_m_ximo_n_o_pode_ser_menor_que_o_valor_m_nimo_;                
            }
            
            txtValorMaximo.SetText(parseFloat(vlMax));
            txtValorMinimo.SetText(parseFloat(vlMin));
        }
    }
    
    return mensagemErro_ValidaCamposFormulario;
}


function desabilitaHabilitaComponentes()
{
    txtDado.SetEnabled(TipoOperacao != "Consultar");
    cmbUnidadeDeMedida.SetEnabled(TipoOperacao != "Consultar");
    cmbCasasDecimais.SetEnabled(TipoOperacao != "Consultar");
    cmbAgrupamentoDoDado.SetEnabled(TipoOperacao != "Consultar");
    txtValorMinimo.SetEnabled(TipoOperacao != "Consultar");
    txtValorMaximo.SetEnabled(TipoOperacao != "Consultar");
    txtCodigoReservado.SetEnabled(TipoOperacao != "Consultar");
    habilitaHtmlEditor();
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registr
    txtCodigoReservado.SetText("");
    txtDado.SetText("");
    cmbUnidadeDeMedida.SetSelectedIndex(-1);
    cmbCasasDecimais.SetSelectedIndex(-1);
    cmbAgrupamentoDoDado.SetSelectedIndex(-1);
    txtValorMinimo.SetText("");
    txtValorMaximo.SetText("");
    if (window.heGlossario)
        heGlossario.SetHtml("");
}

function habilitaHtmlEditor()
{
    try
    {
        document.getElementById('heGlossario_TC_T0').style.display = 'none';
        document.getElementById('heGlossario_TC_AT0').style.display = 'none';
        document.getElementById('heGlossario_TC_T1').style.display = 'none';
        document.getElementById('heGlossario_TC_AT1').style.display = 'none';
        
       if(TipoOperacao != "Consultar")
            heGlossario.ChangeActiveView('D');
        else
            heGlossario.ChangeActiveView('P'); 
   }catch(e)
   {
   }
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if(forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetSelectedFieldValues('DescricaoDado;GlossarioDado;CodigoUnidadeMedida;CasasDecimais;ValorMinimo;ValorMaximo;CodigoFuncaoAgrupamentoDado;CodigoReservado', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    var values = valores[0];

    nomeDadoEdicao = values[0];
    crDadoEdicao = values[7];
    
    txtDado.SetText(values[0]);

    if (window.heGlossario)
        (values[1] == null ? heGlossario.SetHtml("") : heGlossario.SetHtml(values[1]));

    cmbUnidadeDeMedida.SetValue(values[2]);
    cmbCasasDecimais.SetValue(values[3]);
    
    txtValorMinimo.SetText(values[4]);
    
    txtValorMaximo.SetText(values[5]); 

    cmbAgrupamentoDoDado.SetValue(values[6]);
    txtCodigoReservado.SetText(values[7]);
    pcDados.AdjustSize();
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


//-------------------- DIV salvar
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

/*-------------------------------------------------
<summary></summary>
<return></return>
<Parameters>
s: Componente gvDados.
e: Propiedade do gvDados.
</Parameters>
-------------------------------------------------*/
function onClick_CustomButtom(s, e)
{
	gvDados.SetFocusedRowIndex(e.visibleIndex);
	btnSalvar1.SetVisible(true);
	if (e.buttonID == "btnNovo")
     {
        TipoOperacao = "Incluir";
        habilitaHtmlEditor();
        onClickBarraNavegacao("Incluir", gvDados, pcDados);
		hfGeral.Set("TipoOperacao", "Incluir");
		
	 }
     if(e.buttonID == "btnEditar")
     {
        TipoOperacao = "Editar";
        habilitaHtmlEditor();
		onClickBarraNavegacao("Editar", gvDados, pcDados);
		hfGeral.Set("TipoOperacao", "Editar");
		
	 }
     else if(e.buttonID == "btnExcluir")
     {
		onClickBarraNavegacao("Excluir", gvDados, pcDados);
     }
     else if(e.buttonID == "btnDetalhesCustom")
     {	
        TipoOperacao = "Consultar";
        habilitaHtmlEditor();
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar1.SetVisible(false);
		hfGeral.Set("TipoOperacao", "Consultar");
		
		pcDados.Show();
     }
     else if(e.buttonID == "btnDadoCompartilhadoCustom")
     {
        selecionaDadoCompartilhado(gvDados)
        pcDadoCompartilhado.Show();
     }
}

function selecionaDadoCompartilhado(grid)
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    grid.GetSelectedFieldValues('CodigoDado;CodigoReservado', alteraDadoCompartilhado);
        
}

function alteraDadoCompartilhado(valores)
{
    var valor = valores[0];
    (valor[0] == null ? txtCodigoDadoCompartilhado.SetText("") : txtCodigoDadoCompartilhado.SetText(valor[0]));
    (valor[1] == null ? txtCodigoReservadoDadoComp.SetText("") : txtCodigoReservadoDadoComp.SetText(valor[1]));
}

/*-------------------------------------------------
<summary>
Ao finalizar o callback, e se tubo exito sua atividade, se mostrar uma tela de sucesso, 
e atualizará a grid de dados.</summary>
<return></return>
<Parameters>
s: Componente callback.
e: Propiedade do callback.
</Parameters>
-------------------------------------------------*/
function onEnd_CallbackResponsavel(s, e)
{
	if("SIM" == s.cp_OperacaoOk)
	{
	    pcDadoCompartilhado.Hide();
		mostraDivSalvoPublicado(traducao.dadosIndicadores_dados_gravados_com_sucesso_);
		gvDados.PerformCallback();
	}
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