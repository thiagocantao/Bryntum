// JScript File
// ---- Provavelmente não será necessário alterar as duas próximas funções
function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function desabilitaHabilitaComponentes()
{
    txtDescricaoTipoTarefa.SetEnabled("Consultar" != TipoOperacao)
}

function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    if (txtDescricaoTipoTarefa.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.tipoTarefasTimeSheet_a_descri__o_do_tipo_de_tarefa_deve_ser_informada_;
        txtDescricaoTipoTarefa.Focus();
    }
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtDescricaoTipoTarefa.SetText("");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoTipoTarefaTimeSheet', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    txtDescricaoTipoTarefa.SetText(values);
}

function posSalvarComSucesso()
{
    onClick_btnCancelar();  
}

var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";

function preencheListBoxesTela(codigoProjeto)
{
    if( null != codigoProjeto)
    {
        lbItensDisponiveis.ClearItems();
        lbItensSelecionados.ClearItems();
        
        if ( null != codigoProjeto )
        {
            lbItensDisponiveis.cpCodigoProjeto = codigoProjeto[0];
            lbItensSelecionados.cpCodigoProjeto = codigoProjeto[0];
            // se ainda não tiver buscado os status para o tipo de projeto em questão
            if ( false == recoveryListBoxItemsFromMemory(codigoProjeto[0]) )
            {   
                var parametro = "POPLBX_" + codigoProjeto[0] + __cwf_delimitadorValores + "0";
                
                // busca os status da base de dados
                lbItensDisponiveis.PerformCallback(parametro);
                lbItensSelecionados.PerformCallback(parametro);
            }
        }            
    }
}

function recoveryListBoxItemsFromMemory(codigoProjeto)
{
    var preenchidos = false;
    var auxArray = [["Disp_", lbItensDisponiveis], ["Sel_", lbItensSelecionados]];
    var idLista, listBox, listaAsString, listaUnidades, temp;
    
    for (var i= 0; i<2; i++)
    {
        idLista = auxArray[i][0] + codigoProjeto + __cwf_delimitadorValores;
        listBox = auxArray[i][1];
        
        if (hfCompartilhaAssuntos.Contains(idLista))
        {
            listaAsString = hfCompartilhaAssuntos.Get(idLista);
            listaUnidades = listaAsString.split(__cwf_delimitadorElementoLista);
            listaUnidades.sort();
            
            listBox.BeginUpdate();
            listBox.ClearItems();
            for(j=0; j<listaUnidades.length; j++) 
            { 
                if (listaUnidades[j].length>0)
                {
                   temp = listaUnidades[j].split(__cwf_delimitadorValores); 
                   listBox.AddItem(temp[0], temp[1])
               }
            } 
            listBox.EndUpdate();
            preenchidos = true;
        }
    } // for (var i= 0; i<2; i++)
    
    if (preenchidos)
        habilitaBotoesListBoxes();
        
    return preenchidos;
}

function setListBoxItemsInMemory(listBox, inicial)
{
    if( (null != listBox) && (null != inicial) && (null != listBox.cpCodigoProjeto) )
    {
        
        var strConteudo = "";
        var idLista = inicial + listBox.cpCodigoProjeto + __cwf_delimitadorValores;
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
        hfCompartilhaAssuntos.Set(idLista, strConteudo);
    }        
}

function habilitaBotoesListBoxes()
{

    var habilita = window.btnSalvarCompartilhar && btnSalvarCompartilhar.GetVisible();
    btnAddAll.SetEnabled(habilita && lbItensDisponiveis.GetItemCount() > 0);
    btnAddSel.SetEnabled(habilita && lbItensDisponiveis.GetSelectedItem() != null);

    btnRemoveAll.SetEnabled(habilita && lbItensSelecionados.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(habilita && lbItensSelecionados.GetSelectedItem() != null);

    //lbItensDisponiveis.UnselectAll();
    //lbItensSelecionados.UnselectAll();
}

function hfCompartilhaAssuntos_onEndCallback()
{
    if ( hfCompartilhaAssuntos.Contains("StatusSalvar") )
    {
        var status = hfCompartilhaAssuntos.Get("StatusSalvar");
        
        if (status == "1")
            window.top.mostraMensagem(traducao.tipoTarefasTimeSheet_tipo_de_atividade_compartilhada_com_sucesso_, 'sucesso', false, false, null);
        else
        {
            mensagemErro = hfCompartilhaAssuntos.Get("ErroSalvar");
            window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
        }
    }
}

function OnClick_CustomButtons(s, e)
{
	gvDados.SetFocusedRowIndex(e.visibleIndex);	
	e.processOnServer = false;
	
	if(e.buttonID == "btnAssociaProjetos")
	{
		onBtnEditarTipoTarefaTimeSheet_Click(s,e);
	}
}

function pcCompartilharAssuntos_onBtnSalvar()
{
    hfCompartilhaAssuntos.PerformCallback("SalvarTiposTarefaTimeSheet");
}

function onBtnEditarTipoTarefaTimeSheet_Click(grid, e)
{
    hfCompartilhaAssuntos.Clear();
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoTipoTarefaTimeSheet;DescricaoTipoTarefaTimeSheet', mostraPcCompartilharAssuntos);
}

function mostraPcCompartilharAssuntos(valores)
{
    if( null != valores)
    {
        txtDescricaoTipoTarefa_pc.SetText(valores[1]);
        preencheListBoxesTela(valores);
    }
    pcCompartilharAssuntos.Show();
}

function trataMensagemErro(TipoOperacao, mensagemErro)
{
    return mensagemErro;
}