// JScript File
var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";

/*-------------------------------------------------
<summary>
Função que retorna a listagem seleccionada.
</summary>
<return></return>
<Parameters>
s: Componente RadioButtonList.
e: Propiedade do RadioBurronList.
</Parameters>
-------------------------------------------------*/
function onSelected_Listagem(s, e)
{
    var option = s.GetValue();
    pnCallbackNb.PerformCallback(option);
}

/*-------------------------------------------------
<summary></summary>
<return></return>
<Parameters>
grid: Componente ASPxgridview.
forcarMontaCampos: boolean. Determina si executa o nao a função para carregar os campos da tela.
</Parameters>
-------------------------------------------------*/
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoParametro_PT;TipoDadoParametro;Valor;', MontaCamposFormulario);
}

/*-------------------------------------------------
<summary></summary>
<return></return>
<Parameters>
values: datos proveniente da grid, para carregar os campos da tela.
</Parameters>
-------------------------------------------------*/
function MontaCamposFormulario(values)
{

}

/*-------------------------------------------------
<summary>Ação ao finalizar o callback do NavBar</summary>
<return></return>
<Parameters>
</Parameters>
-------------------------------------------------*/
function onEnd_CallbackNb(s, e)
{
//	if(s.cp_OperacaoOk == "")
//	{
//	    //document.getElementById("tdSimForo").style.display = "";
//	    imgIncluirPrimerForum.SetVisible(true);
//	    lblMesagemSimForum.SetVisible(true);
//	    btnNovoForum.SetVisible(false);
//    }
//	else
//	{
//	    //document.getElementById("tdSimForo").style.display = "none";
//	    imgIncluirPrimerForum.SetVisible(false);
//	    lblMesagemSimForum.SetVisible(false);
//	    btnNovoForum.SetVisible(true);
//    }
}

/*-------------------------------------------------
<summary>Ação ao finalizar o callback do NavBar</summary>
<return></return>
<Parameters>
</Parameters>
-------------------------------------------------*/
function onClick_Novo()
{
    hfPanel.Set("acaoPanel", "novo");
    var parametro = "novo";
    pnCallbackPanel.PerformCallback(parametro)
}

/*-------------------------------------------------
<summary>Ação ao finalizar o callback do NavBar</summary>
<return></return>
<Parameters>
</Parameters>
-------------------------------------------------*/
function onClick_Editar(codigoForum)
{
    hfPanel.Set("acaoPanel", "editar");
    var parametro = "editar;" + codigoForum;
    pnCallbackPanel.PerformCallback(parametro)
}

/*-------------------------------------------------
<summary>Ação ao finalizar o callback do NavBar</summary>
<return></return>
<Parameters>
</Parameters>
-------------------------------------------------*/
function onClick_Excluir(codigoForum)
{
    if (confirm(traducao.frameEspacoTrabalho_ForumDiscussao_deseja_realmente_excluir_o_registro_))
    {
        hfPanel.Set("acaoPanel", "excluir");
        var parametro = "excluir;" + codigoForum;
        pnCallbackPanel.PerformCallback(parametro)
    }
}

/*-------------------------------------------------
<summary>Executa ação referento ao botão 'Salvar' do panel</summary>
<return></return>
<Parameters>
</Parameters>
-------------------------------------------------*/
function onClick_btnSalvarPanel()
{
    var acao = "";
    
    if(window.hfPanel)
    {
        //acao = hfPanel.Get("acaoPanel");
        pnCallbackPanel.PerformCallback("salvar");
    }
}

function onEnd_CallbackPanel(s, e)
{
    if (s.cp_OperacaoPanelOk == "editar")
        pcDados.Show();
    else if (s.cp_OperacaoPanelOk == "novo")
        pcDados.Show();
    else 
    {
        if ((hfGeral.Get("mensagemErro") != "undefined") && (hfGeral.Get("mensagemErro") != "") && (hfGeral.Get("mensagemErro") != null)) 
        {
            msgErro = hfGeral.Get("mensagemErro").toString().substring(0, 1000);
            window.top.mostraMensagem(msgErro, 'erro', true, false, null);
            hfGeral.Set("mensagemErro", "");
            //Depois que a mensagem de erro foi mostrada, o usuário deve ter inalteradas suas unidades disponiveis e selecionadas
            //listboxes com unidades disponiveis está sumindo com os itens depois que o erro é mostrado, a de selecionados permanece
            //pnCallbackPanel.PerformCallback("erro");
            pcDados.Show();

        }
        else 
        {
            pcDados.Hide();
        }
    }
}

function habilitaBotoesListBoxes()
{
    btnAddAll.SetEnabled(lbDisponiveisEntidades.GetItemCount() > 0);
    btnAddSel.SetEnabled(lbDisponiveisEntidades.GetSelectedItem() != null);

    btnRemoveAll.SetEnabled(lbSelecionadosEntidades.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(lbSelecionadosEntidades.GetSelectedItem() != null);
}

function setListBoxItemsInMemory(listBox, inicial)
{
    if( (null != listBox) && (null != inicial) && (null != listBox.cpCodigoTipoProjeto) )
    {
        var strConteudo = "";
        var idLista = inicial + listBox.cpCodigoTipoProjeto + __cwf_delimitadorValores;
        var nQtdItems = listBox.GetItemCount();
        var item;
        
        for( var i=0; i<nQtdItems; i++)
        {
            item = listBox.GetItem(i);
            strConteudo = strConteudo + item.text + __cwf_delimitadorValores + item.value + __cwf_delimitadorElementoLista; 
        }
        
        if (0 < strConteudo.length)
            strConteudo = strConteudo.substr(0,strConteudo.length-1);

        // grava a string no hiddenField
        hfStatus.Set(idLista, strConteudo);
    }        
}


function capturaCodigosInteressados()
{
    var CodigosProjetosSelecionados = "";
    for (var i = 0; i < lbSelecionadosEntidades.GetItemCount(); i++) 
    {
        CodigosProjetosSelecionados += lbSelecionadosEntidades.GetItem(i).value + __cwf_delimitadorValores;
    }
    hfStatus.Set("CodigosSelecionados", CodigosProjetosSelecionados);
}

/*-------------------------------------------------
<summary>Executa ação referento ao botão 'Salvar' do panel</summary>
<return></return>
<Parameters>
</Parameters>
-------------------------------------------------*/
function onEnd_CallbackComentarios(s, e)
{
    var filtroListagem = rblListaForuns.GetValue();
    pnCallbackNb.PerformCallback(filtroListagem);
    pcDadosComentario.Hide();
}

/******************************************************************************
Area NavBar
******************************************************************************/
function onExpandedChange_NB(s, e)
{
	if(e.group.GetExpanded())
	{
		document.getElementById('frm_' + e.group.name).src = 'frameEspacoTrabalho_ForumDiscussaoForo.aspx?CF=' + e.group.name;
	}
}