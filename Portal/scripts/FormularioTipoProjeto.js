// JScript File

var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";

function onClick_btnSalvar()
{
    if (window.SalvarCamposFormulario)
        SalvarCamposFormulario();
}

function onClick_btnCancelar()
{
    lbDisponiveisStatus.ClearItems();
    lbSelecionadosStatus.ClearItems();
    
    gvTiposProjetos.PerformCallback("EXCROWS");
    pcDados.Hide();
    return true;
}

function hfStatus_onEndCallback() {
    if (hfStatus.Get("StatusSalvar") == "1") {
        window.top.mostraMensagem(traducao.FormularioTipoProjeto_dados_gravados_com_sucesso_, 'sucesso', false, false, null);
        onClick_btnCancelar();
    }
    else if (hfStatus.Get("StatusSalvar") == "0") {
        mensagemErro = hfStatus.Get("ErroSalvar");
        window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
    }
}

function SalvarCamposFormulario()
{
    hfStatus.PerformCallback("Editar");
    return false;
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoModeloFormulario;NomeFormulario', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{
    if( null != valores)
    {
        var codigoModeloFormulario = valores[0];
        txtNomeFormulario.cpCodigoModeloFormulario = codigoModeloFormulario;
        txtNomeFormulario.SetText(valores[1]);
        
        lblSelecaoStatus.SetText(null);
        lbDisponiveisStatus.ClearItems();
        lbSelecionadosStatus.ClearItems();
        
        if (null != codigoModeloFormulario)
            gvTiposProjetos.PerformCallback("POPFRM_" + codigoModeloFormulario);
            
    }
}

function pcDados_OnPopup(s,e)
{
    var sWidth;
    var sHeight;
    sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
    sHeight = Math.max(0, document.documentElement.clientHeight) - 70;
    s.SetSize(sWidth, sHeight);
    s.UpdatePosition();

    hfStatus.Clear();
    OnGridFocusedRowChanged(gvFormularios, true) 
}

function gvTiposProjetos_FocusedRowChanged(s,e)
{
    var rowIndex = s.GetFocusedRowIndex();
    if ( -1 < rowIndex )
        s.GetRowValues(rowIndex, 'CodigoTipoProjeto;TipoProjeto', preencheListBoxesTela);
}

function preencheListBoxesTela(valores)
{
    if( null != valores)
    {
        var codigoModeloFormulario = txtNomeFormulario.cpCodigoModeloFormulario
        var codigoTipoProjeto = valores[0];
        
        if ( (null != codigoModeloFormulario) && (null != codigoTipoProjeto) )
        {
            lbDisponiveisStatus.cpCodigoTipoProjeto = codigoTipoProjeto;
            lbSelecionadosStatus.cpCodigoTipoProjeto = codigoTipoProjeto;
            lblSelecaoStatus.SetText(valores[1])

            // se ainda não tiver buscado os status para o tipo de projeto em questão
            if ( false == recoveryListBoxItemsFromMemory(codigoTipoProjeto) )
            {   
                var parametro = "POPLBX_" + codigoModeloFormulario + __cwf_delimitadorValores + codigoTipoProjeto;
                
                // busca os status da base de dados
                lbDisponiveisStatus.PerformCallback(parametro);
                lbSelecionadosStatus.PerformCallback(parametro);
            }
        }            
    }
}

function recoveryListBoxItemsFromMemory(codigoTipoProjeto)
{
    var preenchidos = false;
    var auxArray = [["Disp_", lbDisponiveisStatus], ["Sel_", lbSelecionadosStatus]];
    var idLista, listBox, listaAsString, listaStatus, temp;
    
    for (var i= 0; i<2; i++)
    {
        idLista = auxArray[i][0] + codigoTipoProjeto + __cwf_delimitadorValores;
        listBox = auxArray[i][1];
        
        if (hfStatus.Contains(idLista))
        {
            listaAsString = hfStatus.Get(idLista);
            listaStatus = listaAsString.split(__cwf_delimitadorElementoLista);
            listaStatus.sort();
            
            listBox.BeginUpdate();
            listBox.ClearItems();
            for(j=0; j<listaStatus.length; j++) 
            { 
                if (listaStatus[j].length>0)
                {
                   temp = listaStatus[j].split(__cwf_delimitadorValores); 
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
    btnAddAll.SetEnabled(lbDisponiveisStatus.GetItemCount() > 0);
    btnAddSel.SetEnabled(lbDisponiveisStatus.GetSelectedItem() != null);

    btnRemoveAll.SetEnabled(lbSelecionadosStatus.GetItemCount() > 0);
    btnRemoveSel.SetEnabled(lbSelecionadosStatus.GetSelectedItem() != null);
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

/// <summary>
/// função chamada ao fim de um processamento de callback da grid Tipos de Projeto.
/// Se o callback foi em função de uma exclusão de linhas, exclui os status selecionados 
/// </summary>
/// </remarks>
/// <param name="s" type=objeto>componentes DevExpress que gerou o evento.</param>
/// <param name="e" type=objeto>objeto com informações relacionadas ao evento.</param>
/// <returns>void</returns>
function gvTipoProjetos_onEndCallback(s,e)
{
    // cpCodigoTipoProjetoDeletado é atribuído na exclusão do registro da grid
    var tipoProjeto = gvTiposProjetos.cpCodigoTipoProjetoDeletado;
    if (tipoProjeto)
    {
        hfStatus.Remove("Disp_" + tipoProjeto + __cwf_delimitadorValores);
        hfStatus.Remove("Sel_" + tipoProjeto + __cwf_delimitadorValores);
        gvTiposProjetos.cpCodigoTipoProjetoDeletado = null;
        
        if (gvTiposProjetos.GetVisibleRowsOnPage()>0)
            gvTiposProjetos.SetFocusedRowIndex(0);
        else
        {
            var codigoModeloFormulario = -1;
            var codigoTipoProjeto = -1;
            var parametro = "POPLBX_" + codigoModeloFormulario + __cwf_delimitadorValores + codigoTipoProjeto;
            
            // busca os status da base de dados
            lbDisponiveisStatus.PerformCallback(parametro);
            lbSelecionadosStatus.PerformCallback(parametro);
        } // if (gvTiposProjetos.GetVisibleRowsOnPage()>0)
    } 
}

function posSalvarComSucesso()
{
    mostraPopupMensagemGravacao(traducao.FormularioTipoProjeto_dados_gravados_com_sucesso_);
}

//-------
function mostraPopupMensagemGravacao(acao)
{
    if (acao != null && acao != '') {
        lblAcaoGravacao.SetText(acao);
        pcMensagemGravacao.Show();
        setTimeout('fechaTelaEdicao();', 1500);
    }
}

function fechaTelaEdicao()
{
    pcMensagemGravacao.Hide();
    onClick_btnCancelar()
}    

/// <summary>
/// função para excluir as linhas de uma grid
/// </summary>
/// <param name="div" type="ASPxClientGridView">Objeto grid cujas linhas serão excluídas</param>
/// <returns>void</returns>
function deleteGridRows(grid)
{
    var count = grid.GetVisibleRowsOnPage();
    
    for ( var i=0; i<count; i++)
        grid.DeleteRow(0);
}

//--****
