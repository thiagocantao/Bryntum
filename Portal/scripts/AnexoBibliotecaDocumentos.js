// JScript File
var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";

function OnFocusedNodeChanged(treeList) 
{
    try
    {
        treeList.GetNodeValues(treeList.GetFocusedNodeKey(), 'CodigoAnexo;Nome;IndicaPasta;DataInclusao;NomeUsuario;DescricaoAnexo;CodigoPastaSuperior;NomePastaSuperior;IndicaControladoSistema;', MontaCamposFormularioX);
    }
    catch(e){}
}
function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallback
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback("Editar");
    return false;
}

function MontaCamposFormularioX(values) {
    hfGeral.Set("CodigoAnexo", values[0] != null ? values[0] : "");
    // registra as informações da pasta superior
    hfGeral.Set("CodigoPastaSuperior", "-1");
    hfGeral.Set("NomePastaSuperior", "");
    hfGeral.Set("IndicaPasta", values[2] != null ? values[2] : "");
    txtNome.SetText(values[1] != null ? values[1] : "");
    lblIndicaDocumentoPasta.SetText(hfGeral.Get("IndicaPasta").toString() == "S" ? "Pasta:" : "Arquivo:");
    if (values[6] != null) {
        hfGeral.Set("CodigoPastaSuperior", values[6]);
        hfGeral.Set("NomePastaSuperior", values[7]);
    }

    if (null != values) {
        pnCallbackBotoes.PerformCallback("setaControles");
        preencheListBoxesTela(values);
    }

    imgExcluirArquivoPasta.SetEnabled((values[8] + "" != "S"));
    imgEditarArquivoPasta.SetEnabled((values[8] + "" != "S"));
}

function pcDados_OnPopup(s,e)
{
    // limpa o hidden field com a lista das unidades
    //OnFocusedNodeChanged(tlAnexos);
    hfUnidades.Clear();
    
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

function abrePopUp(origem, modoOperacao)
{
    var codigoPastaSuperior = "";
    var codigoPastaAtual = "";
    var codigoAnexo = "";
    
    if (hfGeral.Contains("CodigoPastaSuperior") && hfGeral.Get("CodigoPastaSuperior") != "-1")
        codigoPastaSuperior = hfGeral.Get("CodigoPastaSuperior");
    if (hfGeral.Contains("IndicaPasta") && hfGeral.Get("IndicaPasta") == "S")
        codigoPastaAtual = hfGeral.Get("CodigoAnexo");
    
    if (hfGeral.Contains("IndicaPasta") && hfGeral.Get("IndicaPasta") == "N")
        codigoAnexo = hfGeral.Get("CodigoAnexo");
    
    if('IncluirRaiz' == modoOperacao)
    {
        codigoPastaSuperior = -1;
        codigoPastaAtual = -1;
        modoOperacao = "Incluir";
    }

    window.showModal(tlAnexos.cp_Path + "/AnexosProjeto_PopUp.aspx?OL=S&TA=" + hfGeral.Get("IniciaisTipoAssociacao") + "&ID=" + hfGeral.Get("IDObjetoAssociado") + "&O=" + origem + "&CPS=" + codigoPastaSuperior + "&CPA=" + codigoPastaAtual + "&MO=" + modoOperacao + "&CA=" + codigoAnexo, traducao.AnexoBibliotecaDocumentos_anexos, 950, 510, executaPosPopUp, window);

}

function abreOpcoesLink() {
    var codigoPastaSuperior = "";
    var codigoPastaAtual = "";
    var codigoAnexo = "";

    if (hfGeral.Contains("CodigoPastaSuperior") && hfGeral.Get("CodigoPastaSuperior") != "-1")
        codigoPastaSuperior = hfGeral.Get("CodigoPastaSuperior");
    if (hfGeral.Contains("IndicaPasta") && hfGeral.Get("IndicaPasta") == "S")
        codigoPastaAtual = hfGeral.Get("CodigoAnexo");

    window.showModal(tlAnexos.cp_Path + "/AnexosCompartilhamento.aspx?TA=" + hfGeral.Get("IniciaisTipoAssociacao") + "&ID=" + hfGeral.Get("IDObjetoAssociado") + "&O=Arquivo&CPS=" + codigoPastaSuperior + "&CPA=" + codigoPastaAtual, 'Anexos', screen.width - 60, 490, executaPosPopUp, window);

}

function executaPosPopUp(lParam)
{
    pnCallback.PerformCallback();
}

function mostraPopupMensagemGravacao(acao) {
    if (acao != "") {
        lblAcaoGravacao.SetText(acao);
        pcMensagemGravacao.Show();
    }
    setTimeout('fechaTelaEdicao();', 2000);
}

    function fechaTelaEdicao()
    {
        pcMensagemGravacao.Hide();
        pcDados.Hide();
    }
    
   function trataMensagemErro(TipoOperacao, mensagemErro)
   {
       if (TipoOperacao == "Excluir") {
           if (!mensagemErro.indexOf(traducao.AnexoBibliotecaDocumentos_houve_um_erro_ao_efetivar_a_exclus_o_do_anexo_para_o_projeto) && mensagemErro.indexOf('REFERENCE') >= 0 && mensagemErro.indexOf('anexoVersao') >= 0)
               return traducao.AnexoBibliotecaDocumentos_o_registro_n_o_pode_ser_exclu_do__pois_existem_mais_de_uma_vers_o_relacionada_a_esse_arquivo_;
           else if (!mensagemErro.indexOf(traducao.AnexoBibliotecaDocumentos_houve_um_erro_ao_efetivar_a_exclus_o_do_anexo_para_o_projeto) && mensagemErro.indexOf('REFERENCE') >= 0)
               return traducao.AnexoBibliotecaDocumentos_o_registro_n_o_pode_ser_exclu_do__pois_est__sendo_utilizado_por_outra_tela;
           else return mensagemErro.replace(traducao.AnexoBibliotecaDocumentos_bando_de_dados, traducao.AnexoBibliotecaDocumentos_banco_de_dados);
       }
       else
           return "";
   }