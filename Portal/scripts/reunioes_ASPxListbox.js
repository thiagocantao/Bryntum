// JScript File
var delimitador = "¥";

function buscaNoArray(arr, obj) {
    var retorno = false;
    var i = 0;
    for (i = 0; i < arr.length; i++) {
        if (arr[i] == obj) {
            retorno = true;
        }
    }
    return retorno;
}

function minhaOrdenacao (a, b) {
    if (a.toLowerCase() < b.toLowerCase()) return -1;
    if (a.toLowerCase() > b.toLowerCase()) return 1;
    return 0;
}


function lb_moveItem(lbOrigem, lbDestino) {
    var selectedIndices = lbOrigem.GetSelectedIndices();
    if (selectedIndices != null) {
        var item;
        arrayItens = new Array();

        for (var i = 0; i < lbDestino.GetItemCount(); i++)
            arrayItens.push(lbDestino.GetItem(i).text + delimitador + lbDestino.GetItem(i).value);

        for (var i = 0; i < selectedIndices.length; i++) {
            item = lbOrigem.GetItem(selectedIndices[i]);
            if (null != item) {
                arrayItens.push(item.text + delimitador + item.value);
            }
        }

        lbOrigem.UnselectIndices(selectedIndices);

        arrayItens.sort(minhaOrdenacao);

        lbDestino.BeginUpdate();
        lbDestino.ClearItems();
        for (i = 0; i < arrayItens.length; i++) {
            temp = arrayItens[i].split(delimitador);
            lbDestino.AddItem(temp[0], temp[1]);
        }
        lbDestino.EndUpdate();

        selectedIndices.sort(ordenaArrayNumericamente); // ordena para garantir que irá excluir os 'últimos' primeiro
        lbOrigem.BeginUpdate();
        for (var i = selectedIndices.length - 1; i >= 0; i--) {
            lbOrigem.RemoveItem(selectedIndices[i]);
        }
        lbOrigem.EndUpdate();
    }
}

function ordenaArrayNumericamente(valor1, valor2) {
    return valor1 - valor2;
}

function lb_moveTodosItens(lbOrigem, lbDestino) {
    if (lbOrigem.GetItemCount() > 0) {
        arrayItens = new Array();

        for (var i = 0; i < lbOrigem.GetItemCount(); i++)
            arrayItens.push(lbOrigem.GetItem(i).text + delimitador + lbOrigem.GetItem(i).value);

        for (var i = 0; i < lbDestino.GetItemCount(); i++)
            arrayItens.push(lbDestino.GetItem(i).text + delimitador + lbDestino.GetItem(i).value);

        arrayItens.sort(minhaOrdenacao);

        lbDestino.BeginUpdate();
        lbDestino.ClearItems();
        for (i = 0; i < arrayItens.length; i++) {
            temp = arrayItens[i].split(delimitador);
            lbDestino.AddItem(temp[0], temp[1])
        }
        lbDestino.EndUpdate();

        lbOrigem.ClearItems();
    }
}       
