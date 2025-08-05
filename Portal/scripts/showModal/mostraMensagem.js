var eventoOKMsg = null;
var eventoCancelarMsg = null;
function mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) {
    if (!(timeout)) {
        timeout = 1500;
    }
    lpAguardeMasterPage.Hide();
    if (nomeImagem != null && nomeImagem != '')
        imgApresentacaoAcao.SetImageUrl(pcModal.cp_Path + 'imagens/' + nomeImagem + '.png');
    else
        imgApresentacaoAcao.SetVisible(false);

    textoMsg = replaceAll(textoMsg, '\n', '<br/>');

    lblMensagemApresentacaoAcao.SetText(textoMsg);
    btnOkApresentacaoAcao.SetVisible(mostraBtnOK);
    btnCancelarApresentacaoAcao.SetVisible(mostraBtnCancelar);
    pcApresentacaoAcao.Show();
    eventoOKMsg = eventoOK;
    eventoCancelarMsg = null;

    if (!mostraBtnOK && !mostraBtnCancelar) {
        setTimeout('fechaMensagem();', timeout);
    }
}
function mostraConfirmacao(textoMsg, eventoOK, eventoCancelar) {

    imgApresentacaoAcao.SetImageUrl(pcModal.cp_Path + 'imagens/confirmacao.png');

    textoMsg = replaceAll(textoMsg, '\n', '<br/>');

    lblMensagemApresentacaoAcao.SetText(textoMsg);
    btnOkApresentacaoAcao.SetVisible(true);
    btnCancelarApresentacaoAcao.SetVisible(true);
    pcApresentacaoAcao.Show();
    eventoOKMsg = eventoOK;
    eventoCancelarMsg = eventoCancelar;
}
function fechaMensagem() {
    lblMensagemApresentacaoAcao.SetText('');

    pcApresentacaoAcao.AdjustSize();
    pcApresentacaoAcao.Hide();
}