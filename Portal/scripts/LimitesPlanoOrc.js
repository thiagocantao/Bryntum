
var mensagemErro_ValidaCamposFormulario;
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoPlano;CodigoLimite;NomeLimite;SiglaUnidadeMedida;CodigoUnidadeMedida;ValorMinimo;ValorMaximo;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(valores) {
    var CodigoPlano = (valores[0] == null) ? "" : valores[0].toString();
    var CodigoLimite = (valores[1] == null) ? "" : valores[1].toString();
    var NomeLimite = (valores[2] == null) ? "" : valores[2].toString();
    var SiglaUnidadeMedida = (valores[3] == null) ? "" : valores[3].toString();
    var CodigoUnidadeMedida = (valores[4] == null) ? "" : valores[4].toString();
    var ValorMinimo = (valores[5] == null) ? "" : valores[5].toString();
    var ValorMaximo = (valores[6] == null) ? "" : valores[6].toString();

    ddlLimites.SetValue(CodigoLimite);
    ddlLimites.SetText(NomeLimite);
    spnValorMinimo.SetValue(ValorMinimo);
    spnValorMaximo.SetValue(ValorMaximo);

    desabilitaHabilitaComponentes();

    pnUnidadeMedida.PerformCallback(CodigoLimite + '|' + TipoOperacao);
}

function LimpaCamposFormulario()
{
    ddlLimites.SetValue(null);
    ddlLimites.SetText("");
    spnValorMinimo.SetValue(null);
    spnValorMaximo.SetValue(null);
    pnUnidadeMedida.PerformCallback("-1|" + TipoOperacao);

}

function validaCamposFormulario()
{
    mensagemErro_ValidaCamposFormulario = "";
    var contador = 1;

    if (ddlLimites.GetValue() == null)
    {
        mensagemErro_ValidaCamposFormulario += contador++ + ") " + traducao.LimitesPlanoOrc_o_limite_deve_ser_escolhido_ + "<br/>";
    }

    var valorMinimo = spnValorMinimo.GetValue();
    var valorMaximo = spnValorMaximo.GetValue();
    if (valorMaximo == null && valorMinimo == null) {
        mensagemErro_ValidaCamposFormulario += contador++ + ") " + traducao.LimitesPlanoOrc_o_preenchimento_de_pelo_menos_um_valor___obrigat_rio_m_ximo_e_ou_m_nimo__ + "<br/>";
    }
    else if (valorMaximo != null && valorMinimo != null) {
        if (valorMaximo < valorMinimo) {
            mensagemErro_ValidaCamposFormulario += contador++ + ") " + traducao.LimitesPlanoOrc_valor_m_ximo_n_o_pode_ser_menor_que_o_valor_m_nimo_ + "<br/>";
        }
    }

    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes() {
    //var tipoOperacaoSel = TipoOperacao; hfGeral.Get("TipoOperacao");
    var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;
    ddlLimites.SetEnabled(BoolEnabled);
    ddlLimites.SetEnabled(BoolEnabled);
    spnValorMinimo.SetEnabled(BoolEnabled);
    spnValorMaximo.SetEnabled(BoolEnabled);
    //pnUnidadeMedida.PerformCallback("-2");
}

function ExcluirRegistroSelecionado() {

    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function SalvarCamposFormulario() {

    gvDados.PerformCallback(TipoOperacao);
    return false;
}

