function abreEdicaoObras(codigoObra, tipoObra) {
    var height = 335;
    switch (tipoObra) {
        case 'soci':
            height = 525;
            break;
        case 'plan':
            height = 425;
            break;
    }

    if (tipoObra == 'serv')
        abreEdicaoServicos(codigoObra);
    else
        window.top.showModal("./Administracao/CadastroObra.aspx?ChP=S&CP=" + codigoObra + "&TpO=" + tipoObra, "Cadastro de Obra", 900, height, funcaoPosModal, null);
}

function abreEdicaoServicos(codigoObra) {
    var height = 515;
    
    window.top.showModal("./Administracao/CadastroServicos.aspx?ChP=S&CP=" + codigoObra, "Cadastro de Serviço", 900, height, funcaoPosModal, null);
}

function funcaoPosModal(retorno) {
    if (retorno == 'S')
        gvDados.PerformCallback();
}

function insereNovaObra(tipoObra) {
    var codigoObra = -1;
    var height = 335;
    switch (tipoObra) {
        case 'soci':
            height = 530;
            break;
        case 'plan':
            height = 435;
            break;
    }
    pcTipoObras.Hide();
    window.top.showModal("./Administracao/CadastroObra.aspx?ChP=S&CP=" + codigoObra + "&TpO=" + tipoObra, "Cadastro de Obra", 900, height, funcaoPosModal, null);
}

function OnContextMenu(s, e) {
    if (e.objectType == 'header') {
        colName = s.GetColumn(e.index).fieldName;
        headerMenu.GetItemByName('HideColumn').SetEnabled((colName == null || colName == 'CodigoObra' ? false : true));
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

function grid_ContextMenu(s, e) {
    if (e.objectType == "header")
        pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
}

function SalvarConfiguracoesLayout() {
    callback.PerformCallback("save_layout");
}

function RestaurarConfiguracoesLayout() {
    var funcObj = { funcaoClickOK: function () { callback.PerformCallback("restore_layout"); } }
    window.top.mostraConfirmacao(traducao.ListaObras_deseja_restaurar_as_configura__es_originais_do_layout_da_consulta_, function () { funcObj['funcaoClickOK']() }, null);
}