// JScript File

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

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(),'CodigoLinhaAtuacao;DescricaoLinhaAtuacao;TextoLinhaAtuacao;DataInclusao;CodigoUsuarioInclusao;DataDesativacao;CodigoUsuarioDesativacao;CodigoTipoAssociacao;CodigoObjetoAssociado;', MontaCamposFormulario);
    }

}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente

    LimpaCamposFormulario();

    if(values)
    {
        hfGeral.Set("CodigoLinhaAtuacao", (values[0] != null ? values[0] : ""));
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

function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}

function clickButtonCustom(s,e)
{
     if(e.buttonID == "btnEditarCustom")
     {
		hfGeral.Set("TipoOperacao", "Editar");
		onClickBarraNavegacao("Editar", gvDados, pcDados);
		desabilitaHabilitaComponentes();
     }
     
     else if(e.buttonID == "btnExcluirCustom")
     {
		onClickBarraNavegacao("Excluir", gvDados, pcDados);
     }
     
     else if(e.buttonID == "btnFormularioCustom")
     {	
		OnGridFocusedRowChanged(gvDados, true);
		hfGeral.Set("TipoOperacao", "Consultar");
        desabilitaHabilitaComponentes();
		pcDados.Show();
     }
}

function validaCamposFormulario() {
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 1;

    if (txtDescricao.GetText() == null || trim(txtDescricao.GetText()) == "")
        mensagemErro_ValidaCamposFormulario += countMsg++ + ")" +  traducao.ObjetivoEstrategico_Estrategias_descri__o_deve_ser_informada + "\n";
        
    return mensagemErro_ValidaCamposFormulario;
}



