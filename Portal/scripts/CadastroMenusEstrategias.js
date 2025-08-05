var comando;
function mostraMensagemOperacao(acao) {
    lblAcaoGravacao.SetText(acao);
    pcUsuarioIncluido.Show();
    setTimeout('fechaTelaEdicao();', 2500);
}

function fechaTelaEdicao() {
    pcUsuarioIncluido.Hide();
}

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario() {

    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (Trim(txtGrupo.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMenusEstrategias_a_descri__o_do_grupo_deve_ser_informada_;
    }

    if (spnOrdem.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMenusEstrategias_a_ordem_deve_ser_informada_;
    }

    if (ddlIndicaDisponivel.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMenusEstrategias_a_disponibilidade_deve_ser_informada_;
    }

    if (Trim(txtTituloMenu.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMenusEstrategias_o_t_tulo_deve_ser_informado_;
    }
    
    if (ddlOpcao.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMenusEstrategias_a_op__o_do__tem_de_menu_deve_ser_informada_;
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    txtGrupo.SetText("");
    spnOrdem.SetValue("");
    ddlIndicaDisponivel.SetValue(null);
    txtTituloMenu.SetText("");
    ddlOpcao.SetValue(null);

    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoMenu;DescricaoGrupoMenu;CodigoItemMenu;NomeItemMenu;SequenciaItemGrupoMenu;IniciaisObjeto;IndicaMenuDisponivel;CodigoEntidade;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var CodigoMenu = (values[0] != null ? values[0] : "");
    var DescricaoGrupoMenu = (values[1] != null ? values[1] : "");
    var CodigoItemMenu = (values[2] != null ? values[2] : "");
    var NomeItemMenu = (values[3] != null ? values[3] : "");
    var SequenciaItemGrupoMenu = (values[4] != null ? values[4] : "");
    var IniciaisObjeto = (values[5] != null ? values[5] : "");
    var IndicaMenuDisponivel = (values[6] != null ? values[6] : "");
    var CodigoEntidade = (values[7] != null ? values[7] : "");

    txtGrupo.SetText(DescricaoGrupoMenu);
    spnOrdem.SetValue(SequenciaItemGrupoMenu);
    ddlIndicaDisponivel.SetValue(IndicaMenuDisponivel);
    txtTituloMenu.SetText(NomeItemMenu);
    ddlOpcao.SetValue(CodigoItemMenu);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtGrupo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    spnOrdem.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlIndicaDisponivel.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtTituloMenu.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlOpcao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}