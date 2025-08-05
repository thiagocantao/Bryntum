var comando;
var TipoOperacao;

function montaCamposFormulario(valores) {
    //                                                      0;        1;               2;             3;                      4;
    //s.GetRowValues(s.GetFocusedRowIndex(), 'TituloDashboard;Descricao;IniciaisControle;TipoAssociacao;DescricaoTipoAssociacao;', montaCamposFormulario);
    var TituloDashboard = (valores[0] != null) ? valores[0] : "";
    var Descricao = (valores[1] != null) ? valores[1] : "";
    var IniciaisControle = (valores[2] != null) ? valores[2] : "";
    var TipoAssociacao = (valores[3] != null) ? valores[3] : "";

    txtTituloDashboard.SetText(TituloDashboard);
    memoDescricao.SetText(Descricao);
    txtIniciaisControle.SetText(IniciaisControle);
    comboTipoAssociacao.SetValue(TipoAssociacao);
    pcDados.Show();

}

function limpaCamposFormulario() {

    txtTituloDashboard.SetText("");
    memoDescricao.SetText("");
    txtIniciaisControle.SetText("");
    comboTipoAssociacao.SetValue(null);
    comboTipoAssociacao.SetText("");
    pcDados.Show();
}

function acessarDashboard(chaveGuid) {
    window.location.href = "./EditorDashboard.aspx?id=" + chaveGuid;

}

function excluirDashboard(chaveGuid) {
    gvDados.PerformCallback("Excluir|" + chaveGuid);
}

function validaCamposFormulario() {

    var mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;

    if (txtTituloDashboard.GetText() == "")
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") O título do dashboard deve ser preenchido!\n";

    return mensagemErro_ValidaCamposFormulario;
}