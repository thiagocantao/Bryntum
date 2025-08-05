var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";

function onClick_btnSalvar()
{
    if (window.validaCamposFormulario)
    {
        if ( validaCamposFormulario() != "")
        {
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'erro', true, false, null);
            return false;
        }
    }
    
    if (window.SalvarCamposFormulario)
        SalvarCamposFormulario()
}

function onClick_btnCancelar()
{
    pcDados.Hide();
    return true;
}

function onEnd_pnCallback()
{
    if (hfGeral.Get("StatusSalvar")=="1")
    {
        if (window.posSalvarComSucesso)
            window.posSalvarComSucesso();
        else
            onClick_btnCancelar();
    }
    else if (hfGeral.Get("StatusSalvar")=="0")
    {
        mensagemErro = hfGeral.Get("ErroSalvar");
        window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
        
        if (window.posErroSalvar)
            window.posErroSalvar();
    }
}

function local_onEnd_pnCallback(s,e)
{
        if ( hfGeral.Contains("StatusSalvar") )
        {
            var status = hfGeral.Get("StatusSalvar");
            if (status != "1")
            {
                var mensagem = hfGeral.Get("ErroSalvar");
                pnlImpedimentos.SetVisible(true);
                lblMensagemError.SetText(mensagem);
                pcMensagemGravacao.Show();
            }
            else
            {
	            if (window.onEnd_pnCallback)
	                onEnd_pnCallback();
            }
        }
}

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallback
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback("Editar");
    return false;
}

function LimpaCamposFormulario()
{
    txtNomeIndicador.cpCodigoIndicador = -1;
    txtNomeIndicador.SetText(null);
    
    lbItensDisponiveis.ClearItems();
    lbItensSelecionados.ClearItems();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoIndicador;NomeIndicador', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{
    if( null != valores)
    {
        var codigoIndicador = valores[0];
        txtNomeIndicador.cpCodigoIndicador = codigoIndicador;
        txtNomeIndicador.SetText(valores[1]);
        preencheListBoxesTela(valores);
    }
}

function pcDados_OnPopup(s,e)
{
    // limpa o hidden field com a lista dos usuários
    hfUnidades.Clear();
    OnGridFocusedRowChanged(gvIndicadores, true) 
}


function preencheListBoxesTela(valores)
{
    if( null != valores)
    {
        var codigoIndicador = valores[0];
        
        lbItensDisponiveis.ClearItems();
        lbItensSelecionados.ClearItems();
        
        if ( (null != codigoIndicador) )
        {
            lbItensDisponiveis.cpCodigoIndicador = codigoIndicador;
            lbItensSelecionados.cpCodigoIndicador = codigoIndicador;
            // se ainda não tiver buscado os status para o tipo de projeto em questão
            if ( false == recoveryListBoxItemsFromMemory(codigoIndicador) )
            {   
                var parametro = "POPLBX_" + codigoIndicador + __cwf_delimitadorValores + "0";
                
                // busca os status da base de dados
                lbItensDisponiveis.PerformCallback(parametro);
                lbItensSelecionados.PerformCallback(parametro);
            }
        }            
    }
}

function recoveryListBoxItemsFromMemory(codigoIndicador)
{
    var preenchidos = false;
    var auxArray = [["Disp_", lbItensDisponiveis], ["Sel_", lbItensSelecionados]];
    var idLista, listBox, listaAsString, listaUnidades, temp;
    
    for (var i= 0; i<2; i++)
    {
        idLista = auxArray[i][0] + codigoIndicador + __cwf_delimitadorValores;
        listBox = auxArray[i][1];
        
        if (hfUnidades.Contains(idLista))
        {
            listaAsString = hfUnidades.Get(idLista);
    
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

function habilitaBotoesListBoxes()
{
    btnAddAll.SetEnabled(lbItensDisponiveis.GetItemCount() > 0);
    btnAddSel.SetEnabled(lbItensDisponiveis.GetSelectedItem() != null);

    btnRemoveAll.SetEnabled(lbItensSelecionados.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(lbItensSelecionados.GetSelectedItem() != null);

    //lbItensDisponiveis.UnselectAll();
    //lbItensSelecionados.UnselectAll();
}

function setListBoxItemsInMemory(listBox, inicial)
{
    if( (null != listBox) && (null != inicial) && (null != listBox.cpCodigoIndicador) )
    {
        
        var strConteudo = "";
        var idLista = inicial + listBox.cpCodigoIndicador + __cwf_delimitadorValores;
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
        hfUnidades.Set(idLista, strConteudo);
    }        
}

function posSalvarComSucesso()
{
    mostraPopupMensagemGravacao(traducao.compartilhamentoIndicador_dados_gravados_com_sucesso_);
}

function posErroSalvar()
{
    pnlImpedimentos.SetVisible(true);
   // pnlMensagemGravacao.SetVisible(false);
    pcMensagemGravacao.Show();
}

//-------
function mostraPopupMensagemGravacao(acao)
{
    //pnlImpedimentos.SetVisible(false);
    //pnlMensagemGravacao.SetVisible(true);
    lblAcaoGravacao.SetText(acao);
    pcOperMsg.Show();
    setTimeout ('fechaTelaEdicao();', 2500);
}

function fechaTelaEdicao()
{
    pcOperMsg.Hide();
    onClick_btnCancelar()
}    
//--****
