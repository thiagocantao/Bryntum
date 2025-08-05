function insereNovaDemanda(tipo) {
    window.location.href = hfGeral.Get(tipo);
}

function OnContextMenu(s, e) {
    if (e.objectType == 'header') {
        colName = s.GetColumn(e.index).fieldName;
        headerMenu.GetItemByName('HideColumn').SetEnabled((colName == null || colName == 'CodigoProjeto' ? false : true));
        headerMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }
}

function OnItemClick(s, e) {
    if (e.item.name == 'HideColumn') {
        gvDados.PerformCallback(colName);
        colName = null;
    }
    else {
        if (gvDados.IsCustomizationWindowVisible())
            gvDados.HideCustomizationWindow();
        else
            gvDados.ShowCustomizationWindow();
    }
}