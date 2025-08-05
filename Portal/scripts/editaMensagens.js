// JScript File

var  mensagemErro_ValidaCamposFormulario = "";
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
            mensagemErro_ValidaCamposFormulario = traducao.editaMensagens_campo_mensagem_deve_ser_informado_;
        }
    }
    
    if(btnResponder.GetClientVisible() == true)
    {
        //o usuario vai responder a mensagem, validar somente campo responder
        if(txtResposta.GetText() == "")
        {
            mensagemErro_ValidaCamposFormulario = traducao.editaMensagens_campo_resposta_deve_ser_informado_;
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
   txtMensagem.SetText("");
   txtResposta.SetText("");
   
}

         
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(),'CodigoMensagem;CodigoUsuarioInclusao;Mensagem;Resposta;DataResposta;DataInclusao;IndicaRespostaNecessaria;CodigoObjetoAssociado;DataLimiteResposta;UsuarioInclusao;DataInclusao;NomeUsuarioResposta;NomeProjeto;ExcluiMensagem;EditaMensagem;EditaResposta;', MontaCamposFormulario);  
    }
}

function MontaCamposFormulario(values)
{
   if(txtMensagem.GetInputElement()!=null)
   {
        var msg = (values[2] == null) ? "" : values[2];
        txtMensagem.SetText(msg);
        hfGeral.Set("hfCodigoMensagem",values[0]);
        txtResposta.SetText(values[3]);
   
        var idUsuarioInclusao = values[1];
        var respNecessaria = values[6];
        var podeEditarMsg = values[14];
        var podeEditarResposta = values[15];
        var dataLimite = values[8];
        //pnEditaMensagens.PerformCallback(podeEditarMsg);
       
        verificaAcessosUsuario(idUsuarioInclusao,respNecessaria,podeEditarMsg,podeEditarResposta,dataLimite);
   }
   
}

function verificaAcessosUsuario(idUsuarioInclusao,respNecessaria,podeEditarMsg,podeEditarResposta,dataLimite)
{
    var idUsuarioLogado = hfGeral.Get("hfIdUsuarioLogado");

    if(idUsuarioLogado == idUsuarioInclusao)
    {
        /*verificar se é o usuário que incluiu a mensagem. 
         Se for, HABILITAR o campo MENSAGEM e DESABILITAR o campo RESPOSTA.*/
        pnBotaoResponder.PerformCallback("Salvar");
    }
    else
    {
        /*Se não for o usuário que incluiu a mensagem, 
        a mensagem estiver marcada com RESPOSTA NECESSÁRIA 
        e o usuário TIVER PERMISSÃO para RESPONDER MENSAGEM, 
        desabilitar o campo MENSAGEM e HABILITAR o campo RESPOSTA.*/
        pnBotaoResponder.PerformCallback("Responder");
    }

}

function validaMensagem()
{
    var retorno = "";
    if(ckbRespondeMsg.GetChecked()==true && dtePrazo.GetText() == "")
    {
        retorno = traducao.editaMensagens_prazo_para_resposta_deve_ser_informado;
    }
    return retorno;
}


function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 125;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}