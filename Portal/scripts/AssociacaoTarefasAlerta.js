
//------------------------------------------------------------ funções relacionadas com a ListBox
var delimitadorLocal = ";";

function UpdateButtons() {
    var rOnly = ddlTarefas.cp_RO;

    btnADD.SetEnabled(rOnly == "N" && lbListaDisponiveis.GetSelectedItem() != null);
    btnADDTodos.SetEnabled(rOnly == "N" && lbListaDisponiveis.GetItemCount() > 0);
    btnRMV.SetEnabled(rOnly == "N" && lbListaSelecionados.GetSelectedItem() != null);
    btnRMVTodos.SetEnabled(rOnly == "N" && lbListaSelecionados.GetItemCount() > 0);
    capturaCodigosProjetosSelecionados();
}

function capturaCodigosProjetosSelecionados() {
    var CodigosPerfisSelecionados = "";
    for (var i = 0; i < lbListaSelecionados.GetItemCount(); i++) {
        CodigosPerfisSelecionados += lbListaSelecionados.GetItem(i).value + delimitadorLocal;
    }
    hfGeral.Set("CodigosDestinatariosSelecionados", CodigosPerfisSelecionados);
}

function validaCampos() {
    var mensagemError = "";
    var retorno = true;
    var numError = 0;



    if (ddlTarefas.GetText() == "" || ddlTarefas.GetValue() == null) {
        mensagemError += ++numError + ") " + traducao.AssociacaoTarefasAlerta_escolha_a_tarefa_a_ser_associada__n;
        retorno = false;
    }

    if (lbListaSelecionados.GetItemCount() == 0) {
        mensagemError += ++numError + ") " + traducao.AssociacaoTarefasAlerta_escolha_pelo_menos_um_usu_rio_para_receber_o_alerta__n;
        retorno = false;
    }    

    if (!retorno)
        window.top.mostraMensagem(mensagemError, 'erro', true, false, null);

    return retorno;
}