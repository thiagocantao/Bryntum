// JScript File
function inicioModo(){
    var texto = txtCampoMemo.GetText();
    if (text==""){
        hfGeral.Set("hfModoEdicao", "I");
    } else {
        hfGeral.Set("hfModoEdicao", "E");
    }
}

function validar(){
    if(txtCampoMemo.GetText() == "")
    {
        window.top.mostraMensagem(traducao.descricaoSimples_informe_a_descri__o_desejada_, 'Atencao', true, false, null);
        return false;
    }
    
    return true;
}

function onClick_ButtomSalvar(s, e)
{
    e.processOnServer = false;
    if(validar())
	    Callback.PerformCallback(txtCampoMemo.GetText());
}


