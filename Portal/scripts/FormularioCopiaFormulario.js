// JScript File
function validaCamposCopiaFormulario() {
    var mensagemAlert = "";
    if (txtNomeFormularioCopia.GetText().trim() == "") {
        mensagemAlert += traducao.FormularioCopiaFormulario_o_campo_nome_do_novo_formul_rio_deve_ser_preenchido_ + "\n";
    }
    return mensagemAlert;
}



function onClick_btnSalvarCopiaFormulario() {
    var valida = validaCamposCopiaFormulario();

    if (valida == "") {
        window.top.mostraMensagem(traducao.FormularioCopiaFormulario_confirma_a_c_pia_do_formul_rio_selecionado_, 'confirmacao', true, true, copiaFormulario);
    }
    else {
        window.top.mostraMensagem(valida, 'erro', true, false, null);
    }
}
    
function copiaFormulario() {
    hfStatusCopiaFormulario.PerformCallback("Editar");
}

function onClick_btnCancelarCopiaFormulario()
{
    pcCopiaFormulario.Hide();
    return true;
}

function hfStatusCopiaFormulario_onEndCallback()
{
    if (hfStatusCopiaFormulario.Get("StatusSalvar") == "1") {
        if (window.posSalvarComSucessoCopiaFormulario)
            window.posSalvarComSucessoCopiaFormulario();
        //else
        gvFormularios.PerformCallback("");
        onClick_btnCancelarCopiaFormulario();
    }
    else if (hfStatusCopiaFormulario.Get("StatusSalvar") == "0") {
        mensagemErro = hfStatusCopiaFormulario.Get("ErroSalvar");
        window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
        //mostraPopupMensagemGravacaoCopiaFormulario(mensagemErro);

    }
}

function pcCopiaFormulario_OnPopup(s, e)
{
    // limpa o hidden field com a lista de status
    hfStatus.Clear();
    txtNomeFormularioCopia.SetText("");

}

function posSalvarComSucessoCopiaFormulario()
{
    window.top.mostraMensagem(traducao.FormularioCopiaFormulario_dados_gravados_com_sucesso_, 'sucesso', false, false, null);
    onClick_btnCancelarCopiaFormulario();
    //mostraPopupMensagemGravacaoCopiaFormulario('Dados gravados com sucesso!');
}

//-------
function mostraPopupMensagemGravacaoCopiaFormulario(acao)
{
    lblAcaoGravacao.SetText(acao);
    pcMensagemGravacao.Show();
    setTimeout('fechaTelaEdicaoCopiaFormulario();', 4500);
}

function fechaTelaEdicaoCopiaFormulario() {
    pcMensagemGravacao.Hide();
    onClick_btnCancelarCopiaFormulario()
}    



//--****
