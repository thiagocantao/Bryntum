var comando;
var TipoOperacao;

function SalvarCamposFormulario() {
    gvDados.PerformCallback(TipoOperacao);

}

function OnGridFocusedRowChanged(s) {
    //Recebe os valores dos campos de acordo com a linha selecionada. O resultado é passado para a função montaCampo
    s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoGlossarioAjuda;TituloGlossarioAjuda;TituloFuncionalidade;CodigoFuncionalidade;DetalhesGlossarioAjuda;', MontaCamposFormulario);

}
function MontaCamposFormulario(valores) {

    var CodigoGlossarioAjuda = (valores[0] == null) ? "" : valores[0].toString();
    var TituloGlossarioAjuda = (valores[1] == null) ? "" : valores[1].toString();
    var TituloFuncionalidade = (valores[2] == null) ? "" : valores[2].toString();
    var CodigoFuncionalidade = (valores[3] == null) ? "" : valores[3].toString();
    var DetalhesGlossarioAjuda = (valores[4] == null) ? "" : valores[4].toString();
    
    txtTituloAjuda.SetText(TituloGlossarioAjuda);
    ddlFuncionalidade.SetValue(CodigoFuncionalidade);
    ddlFuncionalidade.SetText(TituloFuncionalidade);
    htmlDetalhesAjuda.SetHtml(DetalhesGlossarioAjuda);

    desabilitaHabilitaComponentes();


}

function LimpaCamposFormulario()
{
    txtTituloAjuda.SetText("");
    ddlFuncionalidade.SetValue(null);
    ddlFuncionalidade.SetText("");
    htmlDetalhesAjuda.SetHtml("");

    desabilitaHabilitaComponentes();
}

function validaCamposFormulario()
{
    var countMsg = 0;
    mensagemErro_ValidaCamposFormulario = "";

    if (txtTituloAjuda.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.Ajudas_o_t_tulo_da_ajuda_deve_ser_informado_ + "\n";
    }
    if (ddlFuncionalidade.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.Ajudas_a_funcionalidade_deve_ser_escolhida_ + "\n";
    }

    if (htmlDetalhesAjuda.GetHtml().toString().length == 0) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.Ajudas_o_detalhe_da_ajuda_deve_ser_preenchido_ + "\n";
    }
    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;

    txtTituloAjuda.SetEnabled(BoolEnabled);
    ddlFuncionalidade.SetEnabled(BoolEnabled);
    ddlFuncionalidade.SetEnabled(BoolEnabled);
    htmlDetalhesAjuda.SetEnabled(BoolEnabled);
}

function ExcluirRegistroSelecionado() {
    gvDados.PerformCallback("Excluir");
}

function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    onClick_btnCancelar();
}
