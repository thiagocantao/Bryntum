var command;

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    if (window.pcDados)//tcPais.CodigoTarefa,tcp1.NomeTarefa, tcPais.CodigoPais, p.NomePais 
        grid.GetSelectedFieldValues('CodigoTarefa;NomeTarefa;', MontaCamposFormulario);
}

function MontaCamposFormulario(valores) {
    
    var values = valores[0];

    var CodigoTarefa = (values[0] != null ? values[0] : "");
    var NomeTarefa = (values[1] != null ? values[1] : "");

    pcDados.SetHeaderText(traducao.AssociacaoTarefasPaises_pa_ses_associados___tarefa__ + NomeTarefa);
    
    gvPaises.PerformCallback();
}

function LimpaCamposFormulario() {

}

function clickLinkQuantidade(chave) {
    gvDados.SetFocusedRowIndex(gvDados.GetFocusedRowIndex());
    hfGeral.Set('TipoOperacao', 'Editar');
    onClickBarraNavegacao('Editar', gvDados, pcDados);
    gvPaises.PerformCallback();
}