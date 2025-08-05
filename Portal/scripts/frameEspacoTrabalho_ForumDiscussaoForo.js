// JScript File

/*-------------------------------------------------
<summary>
Função que executa ciertas ações antes de terminar o callback do TreeList.
</summary>
<return></return>
<Parameters>
s: Componente TreeList.
e: Propiedade do TreeList.
</Parameters>
-------------------------------------------------*/
function onEnd_CallbackTl(s, e)
{
    if(s.cp_OperacaoOk == 'novo' || s.cp_OperacaoOk == 'comentar')
    {
        limparCampos();
        lblReForum.SetText(s.cp_ReForum);
        document.getElementById('spanRe').innerHTML = s.cp_TemaForum; //('ctl00_AreaTrabalho_pcDadosComentario_pnCallbackComentarios_spanRe').innerHTML = s.cp_TemaForum;
        txtCodigoForum.SetText(s.cp_CodigoForum);
        txtCodigoComentario.SetText(s.cp_CodigoComentario);
        lblPublicadoPor.SetText('por: ' + s.cp_Responsavel);
        
        pcDadosComentario.Show()
        //pnCallbackComentario.PerformCallback('recargar');
    }
    else if(s.cp_OperacaoOk == 'salvar')
    {
        //parent.pcDadosComentario.Hide();
        pnCallbackComentario.PerformCallback('recargar');
        
    }
    else if(s.cp_OperacaoOk == 'excluir')
    {
        pnCallbackComentario.PerformCallback('recargar');
    }
    
    s.cp_OperacaoOk = '';
}

/*-------------------------------------------------
<summary>
Função que limpa os campos do formulario de inserir comentario no forum.
</summary>
<return></return>
<Parameters></Parameters>
-------------------------------------------------*/
function limparCampos()
{
    memoComentario.SetText("");
    txtTituloComentario.SetText("");
}
     
/*-------------------------------------------------
<summary>
Função executa ação do Botão 'Salvar' do popup 'Novo Comentario'
</summary>
<return></return>
<Parameters>
s: Componente Popup.
e: Propiedade do Popup.
</Parameters>
-------------------------------------------------*/
function onClick_btnSalvarPanel(s, e)
{
    tlComentarioForum.PerformCallback('salvar');
}

/*-------------------------------------------------
<summary>
Função que inicia a ação ao faser click no link do comentarios da treeList.
permitiendo fazer comentarios ao comentarios ja feitos.
</summary>
<return></return>
<Parameters></Parameters>
-------------------------------------------------*/
function abrePopUp(codigoComentario){
	tlComentarioForum.PerformCallback('comentar;' + codigoComentario);
}

/*-------------------------------------------------
<summary>
Função que exclue um comentario.
</summary>
<return></return>
<Parameters>codigoComentario: o id do comentario necesario para fazer a esclução.</Parameters>
-------------------------------------------------*/
function onClick_exluiComenatario(codigoComentario)
{
    if (confirm(traducao.frameEspacoTrabalho_ForumDiscussaoForo_deseja_realmente_excluir_o_registro_))
        tlComentarioForum.PerformCallback("excluir;" + codigoComentario);
}

/*-------------------------------------------------
<summary>
Função que executa ciertas ações antes de terminar o callback do PanelCallback 'callbackComentario'.
</summary>
<return></return>
<Parameters>
s: Componente PanelCallback.
e: Propiedade do PanelCallback.
</Parameters>
-------------------------------------------------*/
function onEnd_CallbackComentario(s, e)
{
    if(s.cp_OperacaoFail == '')
    {
        tlComentarioForum.PerformCallback('recargar');
        pcDadosComentario.Hide();
    }
}
