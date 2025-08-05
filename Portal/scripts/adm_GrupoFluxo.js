function validaCamposFormulario() {

    mensagemErro_ValidaCamposFormulario = "";
    var contador = 1;
    if (txtDescricaoGrupoFluxo.GetText() == "") {
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.adm_GrupoFluxo_descri__o_do_grupo_de_fluxo_deve_ser_informado_ + "\n";
    }
    if (txtOrdemGrupoFluxo.GetValue() == null || txtOrdemGrupoFluxo.GetValue() == 0) {
        mensagemErro_ValidaCamposFormulario += (contador++) + " - " + traducao.adm_GrupoFluxo_ordem_do_grupo_de_fluxo_deve_ser_informado_;
    }
    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    desabilitaHabilitaComponentes();
    txtDescricaoGrupoFluxo.SetText("");
    txtOrdemGrupoFluxo.SetValue(null);
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoGrupoFluxo;DescricaoGrupoFluxo;OrdemGrupoMenu;IniciaisGrupoFluxo;CodigoEntidade;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {

    var IniciaisGrupoFluxo = (values[3] != null ? values[3] : "");

    desabilitaHabilitaComponentes(IniciaisGrupoFluxo);

    var CodigoGrupoFluxo = (values[0] != null ? values[0] : "");
    var DescricaoGrupoFluxo = (values[1] != null ? values[1] : "");
    var OrdemGrupoMenu = (values[2] != null ? values[2] : "");
    var CodigoEntidade = (values[4] != null ? values[4] : "");

    txtDescricaoGrupoFluxo.SetText(DescricaoGrupoFluxo);
    txtOrdemGrupoFluxo.SetValue(OrdemGrupoMenu);

}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes(iniciaisGRUPO) {
    var habilitado = window.TipoOperacao && TipoOperacao != "Consultar";
    txtDescricaoGrupoFluxo.SetEnabled(habilitado);
    txtOrdemGrupoFluxo.SetEnabled(habilitado);

    if (  ((iniciaisGRUPO != "") && (iniciaisGRUPO != undefined)) && TipoOperacao != "Consultar") {
        //se houver iniciais de grupo, deve-se permitir edição apenas da ordem do grupo
        txtOrdemGrupoFluxo.SetEnabled(true);
        txtDescricaoGrupoFluxo.SetEnabled(false);
    }
    
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao() {
    onClick_btnCancelar();
}

function trataMensagemErro(TipoOperacao, mensagemErro) {
    var strRetorno = "";
    if (TipoOperacao == "Excluir" && mensagemErro.indexOf('REFERENCE') >= 0) {
        strRetorno = traducao.adm_GrupoFluxo_este_grupo_n_o_pode_ser_exclu_do__porque_j__h__modelos_de_fluxos_associados_a_ele_;
    }
    else {
        strRetorno = "";
    }
    return strRetorno;

}