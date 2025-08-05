function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallback
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback("Editar");
    return false;
}
function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function LimpaCamposFormulario()
{
    txtAnalise.SetText("");
    txtAnalise.Validate();

    txtRecomendacoes.SetText("");
    txtRecomendacoes.Validate(); 
    
    dteUltimaAlteracao.SetValue(null); 
    txtIncluidoPor.SetText("");  
    txtAlteradoPor.SetText("");
    dteDataInclusao.SetValue(null); 
    hfGeral.Set("Ano", "");
    hfGeral.Set("Mes", "");
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDetalhesAnalise && pcDetalhesAnalise.IsVisible()))
    {
        //                                           0;      1;            2;           3;          4;                  5;                         6;  7;
        grid.GetRowValues(grid.GetFocusedRowIndex(),'Analise;Recomendacoes;DataInclusao;NomeUsuario;DataUltimaAlteracao;NomeUsuarioUltimaAlteracao;Ano;mes', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    if(values)
    {
        var analise          = values[0];
        var recomendacoes    = values[1];
        
        var dataInclusao     = values[2];
        var incluidoPor      = values[3];
        var ultimaAlteracao  = values[4];
        var alteradoPor      = values[5];
        var ano              = values[6];
        var mes              = values[7];

        txtAnalise.SetText(analise);
        txtAnalise.Validate();

        txtRecomendacoes.SetText(recomendacoes);
        txtRecomendacoes.Validate();

        dteDataInclusao.SetValue(dataInclusao); 
        txtIncluidoPor.SetText(incluidoPor);       
        
        dteUltimaAlteracao.SetValue(ultimaAlteracao); 
        txtAlteradoPor.SetText(alteradoPor);
        
        hfGeral.Set("Ano", ano);
        hfGeral.Set("Mes", mes);
       
    }
}

function onClick_CustomButomGrid(s, e)
{
	e.processOnServer = false;
    gvDados.SetFocusedRowIndex(e.visibleIndex);
	hfGeral.Set("TipoOperacao","");
	desabilitaHabilitaComponentes(true);
	
	if ( 'btnCustomInserir' == e.buttonID || 'btnCustomEditar' == e.buttonID)
	{
	    if ( 'btnCustomInserir' == e.buttonID)
	        desabilitaHabilitaComponentes(false);
	        
	    TipoOperacao = "Editar";
	    onClickBarraNavegacao("Editar", gvDados, pcDados);
	    hfGeral.Set("TipoOperacao", "Editar");
    }
}

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    if((txtAnalise.GetText() == null || trim(txtAnalise.GetText()) == "" ))
        mensagemErro_ValidaCamposFormulario = traducao.indicador_Analise_o_campo_an_lises_e_obrigat_rio_;
        
    return mensagemErro_ValidaCamposFormulario;
}

function onClick_btnExcluirAnalise(s, e)
{
	e.processOnServer = false;
    onClickBarraNavegacao("Excluir", gvDados, pcDados);
}

function desabilitaHabilitaComponentes(value)
{
    btnExcluir.SetEnabled(value);
}