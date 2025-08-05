var comando = "";

function mostraMensagemOperacao(acao) {
    lblAcaoGravacao.SetText(acao);
    pcMensagemGravacao.Show();
    setTimeout('fechaTelaEdicao();', 3000);
}

function fechaTelaEdicao() {
    pcMensagemGravacao.Hide();
}