// JScript File
function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    if (txtIdiomaPt.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = "A descrição da unidade no idioma Português deve ser informado.";
        txtIdiomaPt.Focus();
    }
    else if (txtIdiomaEs.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = "A descrição da unidade no idioma Espanhol deve ser informado.";
        txtIdiomaEs.Focus();
    }
    else if (txtIdiomaEn.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = "A descrição da unidade no idioma Inglês deve ser informado.";
        txtIdiomaEn.Focus();
    }
    else if (txtSigla.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = "A sigla da unidade deve ser informada.";
        txtSigla.Focus();
    }
    else if (txtFator.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = "O fator multiplicador deve ser informado.";
        txtFator.Focus();
    }
        
    return mensagemErro_ValidaCamposFormulario;
}

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
    return false;
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtIdiomaPt.SetText("");
    txtIdiomaEs.SetText("");
    txtIdiomaEn.SetText("");
    txtSigla.SetText("");
    txtFator.SetText("");
}

function OnGridFocusedRowChanged(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoUnidadeMedida_PT;DescricaoUnidadeMedida_EN;DescricaoUnidadeMedida_ES;SiglaUnidadeMedida;FatorMultiplicador;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    txtIdiomaPt.SetText(values[0]);
    txtIdiomaEs.SetText(values[1]);
    txtIdiomaEn.SetText(values[2]);
    txtSigla.SetText(values[3]);
    txtFator.SetText(values[4]);
}