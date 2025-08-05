// JScript File
var __cwf_delimitadorValores = "ֆ";
var __cwf_delimitadorElementoLista = "¢";


// esta função chama o método no servidor responsável por persistir as informações no banco
// o método será chamado por meio do objeto pnCallBack.

function SalvarCamposFormulario() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);

    return false;
}

// esta função chama o método no servidor responsável por excluir o registro selecionado
// o método será chamado por meio do objeto pnCallBack.
function ExcluirRegistroSelecionado() {
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function LimpaCamposFormulario() {
    txtNomePerfil.SetText("");
    lblTipoPerfil.SetText("");

    //desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoPerfilWf;perfil;tipo;indTipoGrupo', MontaCamposFormulario);
}

function MontaCamposFormulario(values) {
    LimpaCamposFormulario();
    var codigoPerfil;
    var parametro;
    if (values) {

        codigoPerfil = values[0];
        hfGeral.Set("CodigoPerfilWf", codigoPerfil);
        txtNomePerfil.SetText((values[1] != null ? values[1] : ""));
        lblTipoPerfil.SetText((values[3] != null ? traducao.adm_CadastroDePerfis_tipo + ": " + values[3] : ""));

        if (values[2] == "RP") {
            document.getElementById("linhaSelecaoUsuarios").style.display = 'none';
            pcDados.UpdatePosition();
            lbItensDisponiveis.SetVisible(false);
            lbItensSelecionados.SetVisible(false);
            btnAddAll.SetEnabled(false);
            btnAddSel.SetEnabled(false);
            btnRemoveAll.SetEnabled(false);
            btnRemoveSel.SetEnabled(false);
            lbItensDisponiveis.ClearItems();
            lbItensSelecionados.ClearItems();
            lpLoading.Hide();
        } else {
            lbItensDisponiveis.SetVisible(true);
            lbItensSelecionados.SetVisible(true);
            document.getElementById("linhaSelecaoUsuarios").style.display = 'block';
            pcDados.UpdatePosition();
        }

        if ((null != codigoPerfil) && (values[2] != "RP")) {
            parametro = "POPLBX_" + codigoPerfil;

            lbItensDisponiveis.PerformCallback(parametro);
            lbItensSelecionados.PerformCallback(parametro);
        }
    }
    else {
        codigoPerfil = -1;
        hfGeral.Set("CodigoPerfilWf", codigoPerfil);
        parametro = "POPLBX_" + codigoPerfil;

        lbItensDisponiveis.PerformCallback(parametro);
        lbItensSelecionados.PerformCallback(parametro);
    }
}

function desabilitaHabilitaComponentes() {
    //var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;

    if ("Incluir" == TipoOperacao || "Editar" == TipoOperacao) {
        txtNomePerfil.SetEnabled(true);
    }
    else {
        txtNomePerfil.SetEnabled(false);
    }
}

/*-----------------------------------------------------------------------------
Function: cerrarPcDado()
Action: Cierra el popup pcDados, enviando un mensaje de sucesso.

-------------------------------------------------------------------------------*/
function cerrarPcDado(operacao) {
    if ("Incluir" == operacao)
        mostraDivSalvoPublicado(traducao.adm_CadastroDePerfis_perfil_inclu_do_com_sucesso_);
    else if ("Editar" == operacao)
        mostraDivSalvoPublicado(traducao.adm_CadastroDePerfis_dados_gravados_com_sucesso_);
    else if ("Excluir" == operacao)
        mostraDivSalvoPublicado(traducao.adm_CadastroDePerfis_perfil_exclu_do_com_sucesso_);
}

/*-----------------------------------------------------------------------------
Function: onClickNovoPerfil()
Action: Novo Registro de perfis.

-------------------------------------------------------------------------------*/
function onClickNovoPerfil() {
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
    hfGeral.Set("TipoOperacao", "Incluir");
    TipoOperacao = "Incluir";

    //desabilitaHabilitaComponentes();
    lblTipoPerfil.SetText(traducao.adm_CadastroDePerfis_pessoas_espec_ficas);

    var codigoPerfil = -1;
    hfGeral.Set("CodigoPerfilWf", codigoPerfil);
    var parametro = "POPLBX_" + codigoPerfil;

    lbItensDisponiveis.PerformCallback(parametro);
    lbItensSelecionados.PerformCallback(parametro);
}

/*-----------------------------------------------------------------------------
Function: endCallback_aspxCallback()
Action: Procesa el resultado de los devuelto por aspxCallback().

-------------------------------------------------------------------------------*/
function endCallback_aspxCallback(cp_ExistenciaOk) {

}

function verificarDadosPreenchidos() {
    var retorno = true;

    //--- Verificar que nao seja seleccionado no modo 'Inserir' ao 'gerente do Projeto'
    var tipoPapel = txtNomePerfil.GetText();
    if ("" == tipoPapel) {
        retorno = false;
        window.top.mostraMensagem(traducao.adm_CadastroDePerfis_o_nome_do_perfil_deve_ser_informado_, 'Atencao', true, false, null);
    }
    //--- alguma coisa de testing
    return retorno;
}

function pcDados_OnPopup(s, e) {
    // limpa o hidden field com a lista de status
    desabilitaHabilitaComponentes();
    hfUsuarios.Clear();
}

function pcDados_OnCloseUp(s, e) {
    // limpa a tela para que a próxima vez que mostrar
    // evitar o efeito de ver os dados desaparecendo e reaparecendo na tela
    lbItensDisponiveis.ClearItems();
    lbItensSelecionados.ClearItems();
}


/*-----------------------------------------------------------------------------
Function: mostraDivSalvoPublicado(acao) - fechaTelaEdicao()
Action: manipula a Div que mostra o mensajes de manipulação com os registros.

-------------------------------------------------------------------------------*/
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao() {
    onClick_btnCancelar()
}

function setListBoxItemsInMemory(listBox, inicial) {
    if ((null != listBox) && (null != inicial)) {

        var strConteudo = "";
        var idLista = inicial;
        var nQtdItems = listBox.GetItemCount();
        var item;

        for (var i = 0; i < nQtdItems; i++) {
            item = listBox.GetItem(i);
            strConteudo = strConteudo + item.text + __cwf_delimitadorValores + item.value + __cwf_delimitadorElementoLista;
        }

        if (0 < strConteudo.length)
            strConteudo = strConteudo.substr(0, strConteudo.length - 1);

        // grava a string no hiddenField
        hfUsuarios.Set(idLista, strConteudo);
    }
}

function habilitaBotoesListBoxes() {
    btnAddAll.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbItensDisponiveis.GetItemCount() > 0);
    btnAddSel.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbItensDisponiveis.GetSelectedItem() != null);

    btnRemoveAll.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbItensSelecionados.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbItensSelecionados.GetSelectedItem() != null);
}