// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
//grid de GrupoMenuTipoProjeto
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    //if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoGrupoMenuTipoProjeto;DescricaoGrupoMenuTipoProjeto;SequenciaGrupoMenuTipoProjeto;IndicaGrupoVisivel', MontaCamposFormulario);
    //}
}


function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();
    //DescricaoGrupoMenuTipoProjeto;SequenciaGrupoMenuTipoProjeto;IndicaGrupoVisivel;CodigoGrupoMenuTipoProjeto
    var descricaoGrupoMenuTipoProjeto = (values[1] != null ? values[1] : "");
    var sequenciaMenuTipoProjeto = (values[2] != null ? values[2] : "");
    var grupoAtivo = (values[3] != null ? values[3] : "");
    var codigoGrupoMenuSelecionado = (values[0] != null ? values[0] : "");
    txtGrupo.SetText(descricaoGrupoMenuTipoProjeto);
    txtOrdem.SetText(sequenciaMenuTipoProjeto);
    ckGrupoAtivo.SetValue((grupoAtivo == "S") ? true : false);
    gvMenuTipoProjeto.PerformCallback();
}

function ddlTipoProjetoSelectedIndexChanged() {

    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    var codTipoProjetoSelecionado = ddlTipoProjeto.GetValue();
    hfGeral.Set("codigoTipoProjetoSelecionado", codTipoProjetoSelecionado);
    pnCallback.PerformCallback('filtraGrupos');
}

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

//funcao exclusiva da pcdados GrupoMenuTipoProjeto
function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (Trim(txtGrupo.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMenusObjetos_descri__o_do_grupo_deve_ser_informado_;
    }
    if (Trim(txtOrdem.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMenusObjetos_ordem_do_grupo_deve_ser_informada_;
    }
    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

//funcao exclusiva da pcdados GrupoMenuTipoProjeto
function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}
//funcao exclusiva da pcdados GrupoMenuTipoProjeto
function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");     pnCallback.PerformCallback(TipoOperacao);
    return false;
}
// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

//funcao exclusiva da pcdados GrupoMenuTipoProjeto
function LimpaCamposFormulario() {
    txtGrupo.SetText('');
    txtOrdem.SetText('');
    ckGrupoAtivo.SetValue(true);
    desabilitaHabilitaComponentes();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {

    txtGrupo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtOrdem.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");

    ckGrupoAtivo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO') > 0)
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else if (acao.toUpperCase().indexOf('VEL INCLUIR O GRUPO DE MENU') > 0)
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao() {
    onClick_btnCancelar();
}

function ddlOpcaoMenuSelectedIndexChanged()
{
    var codigoOpcaoDeMenuSelecionado = ddlOpcaoDeMenu.GetValue();
    hfGeral.Set("codigoOpcaoDeMenuSelecionado", codigoOpcaoDeMenuSelecionado);
}