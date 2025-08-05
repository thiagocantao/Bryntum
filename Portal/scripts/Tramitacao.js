var indicaEdicao;

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
   mensagemErro_ValidaCamposFormulario = "";
   var numAux = 0;
    var mensagem = "";

    if (isNaN(ddlResponsavel.GetValue()) || ddlResponsavel.GetValue() == null)
    {
        numAux++;
        mensagem += "\n" + numAux + ") O responsável deve ser informado corretamente!";
    }

    if (ddlPrazo.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Tramitacao_o_prazo_deve_ser_informado_;
    }

    if (Trim(txtAssunto.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Tramitacao_o_assunto_deve_ser_informado_;
    }

    if (Trim(mmObjeto.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.Tramitacao_a_mensagem_deve_ser_informada_;
    }

    if(mensagem != "")
    {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }
    
    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    ddlResponsavel.SetValue(null);
    ddlResponsavel.SetText("");
    ddlPrazo.SetValue(null);
    txtAssunto.SetText(mmObjeto.cp_TextoAssunto);
    mmObjeto.SetText(mmObjeto.cp_TextoCorpo);

    desabilitaHabilitaComponentes();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoModeloFormulario;TituloFormulario;DescricaoStatus;DataSolicitacaoTramitacao;DataPrevistaConclusao;Responsavel;CodigoResponsavel;AssuntoMensagemNotificacao;TextoMensagemNotificacao', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values)
{
        
    var codigoModelo        = (values[0] != null ? values[0] : "");
    var tituloFormuLario    = (values[1] != null ? values[1] : "");
    var descricaoStatus     = (values[2] != null ? values[2] : "");
    var dataSolicitacao     = (values[3] != null ? values[3] : null);
    var dataConclusao       = (values[4] != null ? values[4] : null);
    var nomeResponsavel     = (values[5] != null ? values[5] : "");
    var codigoResponsavel   = (values[6] != null ? values[6] : null);
    var assunto             = (values[7] != null ? values[7] : mmObjeto.cp_TextoAssunto);
    var mensagem            = (values[8] != null ? values[8] : mmObjeto.cp_TextoCorpo);

    ddlResponsavel.SetValue(codigoResponsavel);
    ddlResponsavel.SetText(nomeResponsavel);
    ddlPrazo.SetValue(dataConclusao);
    txtAssunto.SetText(assunto);
    mmObjeto.SetText(mensagem);

    if (dataSolicitacao == null)
        indicaEdicao = 'N';
    else
        indicaEdicao = 'S';

    if (indicaEdicao == 'S' && TipoOperacao == "Incluir")
        TipoOperacao = 'Editar';
    
    gvFormulariosBloqueados.PerformCallback(TipoOperacao);

    desabilitaHabilitaComponentes();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {    
    ddlResponsavel.SetEnabled(window.TipoOperacao && TipoOperacao == "Incluir");
    ddlPrazo.SetEnabled(window.TipoOperacao && TipoOperacao == "Incluir");
    txtAssunto.SetEnabled(window.TipoOperacao && TipoOperacao == "Incluir");
    mmObjeto.SetEnabled(window.TipoOperacao && TipoOperacao == "Incluir");
    btnSalvar.SetVisible(window.TipoOperacao && TipoOperacao == "Incluir");

}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
    
    if ('Incluir' == pnCallback.cp_OperacaoOk) {

        TipoOperacao = 'Incluir';
        hfGeral.Set("TipoOperacao", "Incluir");
        OnGridFocusedRowChanged(gvDados, true);
        pcDados.Show();
    }
}

function gravaInstanciaWf() {
    try {
        window.parent.executaCallbackWF();
    } catch (e) { }
}

function eventoPosSalvar(codigoInstancia) {
    try {
        hfGeral.Set('CIWF', codigoInstancia);
        window.parent.parent.hfGeralWorkflow.Set('CodigoInstanciaWf', codigoInstancia);
        if (window.onClick_btnSalvar)
            onClick_btnSalvar();       
    } catch (e) {
    }
}