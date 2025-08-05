// JScript File
// ---- Provavelmente não será necessário alterar as duas próximas funções
function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
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
    mensagemErro_ValidaCamposFormulario = "";    
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario()
{
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoParametro_PT;TipoDadoParametro;Valor;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    memoDescricao.SetText(values[0].toString());
    
    var tipoDado = values[1].toString();
    
    txtValorTXT.SetVisible(tipoDado == 'TXT');
    txtValorINT.SetVisible(tipoDado == 'INT');
    ddlValorMES.SetVisible(tipoDado == 'MES');
    rbValorBOL.SetVisible(tipoDado == 'BOL' || tipoDado == 'LOG');
    ddlCOR.SetVisible(tipoDado == 'COR');
    
    switch(tipoDado)
    {
        case 'TXT': txtValorTXT.SetText(values[2].toString());
        break;
        case 'INT': txtValorINT.SetText(values[2].toString());
        break;
        case 'MES': ddlValorMES.SetValue(values[2].toString());
        break;
        case 'BOL': rbValorBOL.SetValue(values[2].toString());
        break;
        case 'LOG': rbValorBOL.SetValue(values[2].toString());
        break;
        case 'COR': ddlCOR.SetText(values[2].toString());
        break;
    }
}


// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    onClick_btnCancelar();    
}


// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------

