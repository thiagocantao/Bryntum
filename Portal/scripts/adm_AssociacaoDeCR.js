var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";

function mostraPopupSelecao(valores) {
    var codigoProjeto = valores[0];
    var ano = valores[1];
    var nomeProjeto = valores[2];
    window.top.showModal("popup_adm_AssociacaoDeCR.aspx?CP=" + codigoProjeto + "&ano=" + ano, "Associar -- " + nomeProjeto, null, null, atualizaGrid, null);

}

function habilitaBotoesListBoxes() {
    btnAddAll.SetEnabled(crDisponiveis.GetItemCount() > 0);
    btnAddSel.SetEnabled(crDisponiveis.GetSelectedItem() != null);

    btnRemoveAll.SetEnabled(crSelecionados.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(crSelecionados.GetSelectedItem() != null);
}


function setListBoxItemsInMemory(listBox, inicial) {
    if ((null != listBox) && (null != inicial)) {

        var strConteudo = "";
        var idLista = inicial +  __cwf_delimitadorValores;
        var nQtdItems = listBox.GetItemCount();
        var item;

        for (var i = 0; i < nQtdItems; i++) {
            item = listBox.GetItem(i);
            strConteudo = strConteudo + item.text + __cwf_delimitadorValores + item.value + __cwf_delimitadorElementoLista;
        }

        if (0 < strConteudo.length)
            strConteudo = strConteudo.substr(0, strConteudo.length - 1);

        // grava a string no hiddenField
         hfGeral.Set(idLista, strConteudo);
    }
}

function atualizaGrid() {
    gvDados.PerformCallback();
}