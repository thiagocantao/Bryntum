// Data: 29 / 07 / 2010.
// Author: Alejandro Fuentes


function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
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



function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(),'CodigoAcaoSugerida;DescricaoAcao;TextoAcaoSugerida;DataInclusao;CodigoUsuarioInclusao;DataDesativacao;CodigoUsuarioDesativacao;CodigoTipoAssociacao;CodigoObjetoAssociado;', MontaCamposFormulario);
    }

}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente

    LimpaCamposFormulario();

    if(values)
    {
        hfGeral.Set("codigoEvento", (values[0] != null ? values[0] : ""));
        txtDescricao.SetText((values[1] != null ? values[1]  : ""));
        if(window.htmlTexto)
            htmlTexto.SetHtml((values[2] != null ? values[2]  : ""));
    }
}

function Click_NovaAcaoSugerida()
{
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
    hfGeral.Get("TipoOperacao", "Incluir");
    desabilitaHabilitaComponentes();
}

function LimpaCamposFormulario()
{
    txtDescricao.SetText("");
    htmlTexto.SetHtml("");
}

function desabilitaHabilitaComponentes()
{
	var BoolEnabled = hfGeral.Get("TipoOperacao") == "Consultar" ? false : true ;
    btnSalvar.SetVisible(BoolEnabled);
	txtDescricao.SetEnabled(BoolEnabled);
    htmlTexto.SetEnabled(BoolEnabled);
}

function verificarDadosPreenchidos()
{
    var mensagemError = "";
    var retorno = true;
    var numError = 0;
    
    if(trim(txtDescricao.GetText()) == "")
    {
        mensagemError += ++numError + ") " + traducao.ObjetivoEstrategico_AcoesSugeridas_a_descri__o_deve_ser_informado_ + "\n";
            retorno = false;
    }

	if (!retorno)
	    window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);
	    
    return retorno;
}

function Click_Salvar(s,e)
{
	e.processOnServer = false;
	
	if(verificarDadosPreenchidos())
	{
		if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
	}
	else
		return false;
}

function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}