function insereNovaDemanda(tipo) {
    window.location.href = hfGeral.Get(tipo);
}

function abreNovoFluxoEVT(codigoProjeto, iniciaisFluxo) {
    if (confirm(traducao.ListaDemandas_deseja_iniciar_o_fluxo_de_abertura_do_projeto_)) {
        callbackEVT.PerformCallback(codigoProjeto + ";" + iniciaisFluxo);
    }
}

function iniciaAberturaProjeto() {
    ddlProjetoAbertura.SetValue(null);
    pcAbertura.Show();
}

function OnContextMenu(s, e) {
    if (e.objectType == 'header') {
        colName = s.GetColumn(e.index).fieldName;
        headerMenu.GetItemByName('HideColumn').SetEnabled((colName == null || colName == 'CodigoDemanda' ? false : true));
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