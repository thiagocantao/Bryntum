 var myObject = new Object();
function abreTelaNovaMensagem()
{
    var codProj = hfGeral.Get('hfCodigoProjeto');
	var nomeProj = hfGeral.Get('hfNomeProjeto');


    
     myObject.nomeProjeto = nomeProj;

     window.top.showModal("../../Mensagens/EnvioMensagens.aspx?CO=" + codProj + "&TA=DC", traducao.indexResumoDemanda_nova_mensagem___ + nomeProj, 950, 510, "", myObject);
}

function funcaoPosModal(retorno)
{
	cbkGeral.PerformCallback(retorno);
}