var comando = "";
function validaCamposFormulario() {
    var contador = 0;
    var mensagemErro_ValidaCamposFormulario = "";

    if (ddlProjeto.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += contador++ + ") " + traducao.adm_PesoItensCurva_UHE_projeto_deve_ser_escolhido + "\r\n";
    }
    if (ddlCategoria.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += contador++ + ") " + traducao.adm_PesoItensCurva_UHE_categoria_deve_ser_escolhida + "\r\n";
    }
    return mensagemErro_ValidaCamposFormulario;
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoProjeto;CodigoCategoria;NomeProjeto;DescricaoCategoria;Peso;', MontaCamposFormulario);
}

function LimpaCamposFormulario() {
    desabilitaHabilitaComponentes();
    ddlProjeto.SetValue(null);
    ddlCategoria.SetValue(null);
    spnPeso.SetValue(null);

}

function desabilitaHabilitaComponentes() {

    ddlProjeto.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlCategoria.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    spnPeso.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    
}

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallback
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function MontaCamposFormulario(values) {
    LimpaCamposFormulario();

    //CodigoProjeto; CodigoCategoria; NomeProjeto; DescricaoCategoria; Peso;

    var CodigoProjeto = (values[0] != null ? values[0] : "");
    var CodigoCategoria = (values[1] != null ? values[1] : "");
    var NomeProjeto = (values[2] != null ? values[2] : "");
    var DescricaoCategoria = (values[3] != null ? values[3] : "");
    var Peso = (values[4] != null ? values[4] : "");


    ddlProjeto.SetValue(CodigoProjeto);
    ddlProjeto.SetText(NomeProjeto);

    ddlCategoria.SetValue(CodigoCategoria);
    ddlCategoria.SetText(DescricaoCategoria);

    spnPeso.SetValue(Peso);	

    desabilitaHabilitaComponentes();
}

