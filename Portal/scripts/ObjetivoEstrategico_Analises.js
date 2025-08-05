var TipoOperacao;

function LimpaCamposFormulario()
{
    //debugger

    txtAnalise.SetEnabled(TipoOperacao != 'Visualizar');
    txtRecomendacoes.SetEnabled(TipoOperacao != 'Visualizar');
    dteUltimaAlteracao.SetEnabled(TipoOperacao != 'Visualizar');
    txtIncluidoPor.SetEnabled(TipoOperacao != 'Visualizar');
    txtAlteradoPor.SetEnabled(TipoOperacao != 'Visualizar');
    dteDataInclusao.SetEnabled(TipoOperacao != 'Visualizar');

    txtAnalise.SetText("");
    txtAnalise.Validate();
    txtRecomendacoes.SetText("");
    txtRecomendacoes.Validate();
    dteUltimaAlteracao.SetValue(null); 
    txtIncluidoPor.SetText("");  
    txtAlteradoPor.SetText("");
    dteDataInclusao.SetValue(null); 
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    //LimpaCamposFormulario();

    if(values)
    {
        var analise          = values[0];
        var recomendacoes    = values[1];
        var dataInclusao     = values[2];
        var incluidoPor      = values[3];
        var ultimaAlteracao  = values[4];
        var alteradoPor      = values[5];
        
        txtAnalise.SetText(analise);
        txtAnalise.Validate();
        txtRecomendacoes.SetText(recomendacoes);
        txtRecomendacoes.Validate();
        dteUltimaAlteracao.SetValue(ultimaAlteracao); 
        txtIncluidoPor.SetText(incluidoPor);  
        txtAlteradoPor.SetText(alteradoPor);
        dteDataInclusao.SetValue(dataInclusao); 

        txtAnalise.SetEnabled(TipoOperacao != 'Visualizar');
        txtRecomendacoes.SetEnabled(TipoOperacao != 'Visualizar');
        dteUltimaAlteracao.SetEnabled(TipoOperacao != 'Visualizar');
        txtIncluidoPor.SetEnabled(TipoOperacao != 'Visualizar');
        txtAlteradoPor.SetEnabled(TipoOperacao != 'Consultar');
        dteDataInclusao.SetEnabled(TipoOperacao != 'Consultar');
    }    
}

function validaCamposFormulario() {

    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    var mensagemErro = "";

    if (txtAnalise.GetText() == "") {
        mensagemErro = "O campo análise deverá ser preenchido.";
        txtAnalise.Focus();
    }

    return mensagemErro;
}


