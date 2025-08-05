// JScript File
var mensagemErro_ValidaCamposFormulario = "";

function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    
    mensagemErro_ValidaCamposFormulario = "";
    if(btnSalvar.GetClientVisible() == true)
    {
        //o usuario vai salvar a mensagem, validar somente campo salvar
        if(txtMensagem.GetText() == "")
         {
            mensagemErro_ValidaCamposFormulario = traducao.frameEspacoTrabalho_CaixaEntrada_campo_mensagem_deve_ser_informado_;
        }
    }
    
    if(btnResponder.GetClientVisible() == true)
    {
        //o usuario vai responder a mensagem, validar somente campo responder
        if(txtResposta.GetText() == "")
        {
            mensagemErro_ValidaCamposFormulario = traducao.frameEspacoTrabalho_CaixaEntrada_campo_resposta_deve_ser_informado_;
        }
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

function linkLerMensagem(CodigoCaixaMensagem, CodigoTipoAssociacao, CodigoObjetoAssociado, CodigoProjeto, CodigoMensage, executaLink)
{
    if ( (null != executaLink) && ("S" == executaLink) )
    {
        var tipoAssociacaoWf = hfGeral.Get("tipoAssociacaoWf");
        var tipoAssociacaoMsg = hfGeral.Get("tipoAssociacaoMsg");
        
        hfGeral.Set("CodigoMensagem", CodigoCaixaMensagem);
        hfGeral.Set("CodigoTipoAssociacao", CodigoTipoAssociacao);
        hfGeral.Set("CodigoObjetoAssociado", CodigoObjetoAssociado);
        hfGeral.Set("CodigoProjeto", CodigoProjeto);
        hfGeral.Set("CodigoDoMensagem", CodigoMensage);

        if(CodigoTipoAssociacao == tipoAssociacaoWf)
        {
            pnCallback.PerformCallback();
        }
        else if(CodigoTipoAssociacao == tipoAssociacaoMsg)
        {
           //pcDados.ShowWindow();
           pnPcDados.PerformCallback("preencherDados");
        }
    }
}

function executaUrlWfEngine()
{
    if (hfGeral.Contains("urlWfEngine"))
    {
        urlWfEngine = hfGeral.Get("urlWfEngine");
        if (urlWfEngine != "")
            window.top.gotoURL(urlWfEngine, '_parent');
    }
}


