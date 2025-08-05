// JScript File
function validaCampos()
{
    if(txtTitulo.GetText() == "")
    {
        window.top.mostraMensagem(traducao.PropostaResumo_campo_titulo_precisa_ser_preenchido_, 'Atencao', true, false, null);
        return false;
    }
    else
    {
        return true;
    }
}
