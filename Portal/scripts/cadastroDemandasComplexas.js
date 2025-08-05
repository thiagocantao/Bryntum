// JScript File

var existeConteudoCampoAlterado = false;

function onValueChange_Objects(s, e)
{
    existeConteudoCampoAlterado = true;
}

function validaCamposFormulario() {
   var mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;

    if (trim(txtTitulo.GetText()) == "")
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroDemandasComplexas_t_tulo_deve_ser_informado_ + "\n";

    if (txtInicio.GetValue() == null)
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroDemandasComplexas_in_cio_deve_ser_informado_ + "\n";

    if (txtTermino.GetValue() == null)
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroDemandasComplexas_t_rmino_deve_ser_informado_ + "\n";

    if (ddlUnidade.GetValue() == null)
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroDemandasComplexas_unidade_deve_ser_informada_ + "\n";

    return mensagemErro_ValidaCamposFormulario;
}