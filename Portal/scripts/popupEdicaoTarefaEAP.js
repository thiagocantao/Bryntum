// JScript File
var mensagemErro_ValidaCamposFormulario = "";
function validaCamposFormulario()
{
    mensagemErro_ValidaCamposFormulario = "";
    
    if(!validaData())
    {
        mensagemErro_ValidaCamposFormulario =  traducao.popupEdicaoTarefaEAP_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n";
    }
    else if (txtTarefa.GetText().trim() === "") {
        mensagemErro_ValidaCamposFormulario = "O nome do item deve ser informado!\n";
    }
    else if (txtTarefa.GetText().trim() === "") {
        mensagemErro_ValidaCamposFormulario = "O nome do item deve ser informado!\n";
    }
    else if(txtTrabalho.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.popupEdicaoTarefaEAP_o_trabalho_deve_ser_informado_ + "\n"; 
    }
    else if(txtCusto.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.popupEdicaoTarefaEAP_o_custo_deve_ser_informado_ + "\n"; 
    }
    return mensagemErro_ValidaCamposFormulario;
}

function validaUsuario()
{
    return (hfGeral.Contains("lovCodigoResponsavel") && hfGeral.Get("lovCodigoResponsavel").toString() != "");
}

function validaData()
{
    var dataInicio = null;
    var dataTermino = null;
    
    if(dteInicio.GetValue() != null)
    {
       dataInicio 	  = new Date(dteInicio.GetValue());
	   var meuDataInicio = (dataInicio.getMonth() + 1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
	   dataInicio  	  = Date.parse(meuDataInicio);
    }
    if(dteTermino.GetValue() != null)
    {
       	dataTermino 	= new Date(dteTermino.GetValue());
	    var meuDataTermino 	= (dataTermino.getMonth() + 1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
	    dataTermino		    = Date.parse(meuDataTermino);
    }
    if(dataInicio != null && dataTermino != null)
    {
         if(dataInicio > dataTermino)
            return false;
        else
            return true;
    }
    else
       return true;
}

function mostraPopupMensagemGravacao(acao)
    {
        lblAcaoGravacao.SetText(acao);
        pcMensagemGravacao.Show();
        setTimeout ('fechaTelaEdicao();', 1500);
    }

    function fechaTelaEdicao()
    {
        pcMensagemGravacao.Hide();
        window.top.fechaModal2();
        
    }
    
    function setMaxLength(textAreaElement, length) {
    textAreaElement.maxlength = length;
    ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
    ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
}

function onKeyUpOrChange(evt) 
{
    processTextAreaText(ASPxClientUtils.GetEventSource(evt));
}

function processTextAreaText(textAreaElement) {
    var maxLength = textAreaElement.maxlength;
    var text = textAreaElement.value;
    var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
    if (maxLength != 0 && text.length > maxLength) 
        textAreaElement.value = text.substr(0, maxLength);
    else
        lblCantCarater.SetText(text.length);
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}
