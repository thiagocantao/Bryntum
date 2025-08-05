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

    if (Trim(txtNomeConta.GetText()) == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.contaDesembolsoFinanceiro_a_descri__o_da_conta_deve_ser_informada_;
    }
    if (Trim(txtNomeGrupo.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.contaDesembolsoFinanceiro_o_grupo_deve_ser_informado_;
    }
    if (ddlCategoria.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.contaDesembolsoFinanceiro_a__rea_deve_ser_informada_;
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

function LimpaCamposFormulario()
{
    txtNomeConta.SetText("");
    txtNomeGrupo.SetText("");
    ddlCategoria.SetValue(null);
    ddlCategoria.SetText("")
    ckbFinanciavel.SetChecked(true);
    
    desabilitaHabilitaComponentes();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(),'DescricaoConta;IndicaInvestimentoFinanciavel;GrupoConta;CodigoEntidade;CodigoCategoria;CodigoConta;DescricaoCategoria', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values)
{
    desabilitaHabilitaComponentes();


    var DescricaoConta = (values[0] != null ? values[0] : ""); ;
    var IndicaInvestimentoFinanciavel = (values[1] != null ? values[1] : ""); ;
    var GrupoConta = (values[2] != null ? values[2] : ""); ;
    var CodigoEntidade = (values[3] != null ? values[3] : ""); ;
    var CodigoCategoria = (values[4] != null ? values[4] : ""); ;
    var CodigoConta = (values[5] != null ? values[5] : ""); ;
    var DescricaoCategoria = (values[6] != null ? values[6] : ""); ;

    txtNomeConta.SetText(DescricaoConta);
    txtNomeGrupo.SetText(GrupoConta);
    ddlCategoria.SetValue(CodigoCategoria);
    ddlCategoria.SetText(DescricaoCategoria)
    ckbFinanciavel.SetChecked(IndicaInvestimentoFinanciavel == 'S' ? true : false);

}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    txtNomeConta.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtNomeGrupo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlCategoria.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlCategoria.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ckbFinanciavel.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}