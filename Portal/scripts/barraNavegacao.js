// JScript File
function onClickBarraNavegacao(idBotao, grid, popup, exibirPopup)
{
    if (exibirPopup == null) {
        exibirPopup = true;
    }
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    if (!window.hfGeral)
        window.top.mostraMensagem(traducao.barraNavegacao_o_componente___hfgeral___n_o_foi_encontrado___neste_componente_deve_ser_colocado_dentro_do_componente___pncallback__, 'atencao', true, false, null);
        
    if (!window.OnGridFocusedRowChanged)
        window.top.mostraMensagem(traducao.barraNavegacao_o_m_todo___ongridfocusedrowchanged___n_o_foi_implementado_, 'atencao', true, false, null);

    hfGeral.Set("KeyFieldValue", grid.GetRowKey(grid.GetFocusedRowIndex()));

    
    
    // se entrou no modo de edição 
    if (idBotao=="Incluir" || idBotao=="Editar" )
    {
        if (! window.LimpaCamposFormulario)
            window.top.mostraMensagem(traducao.barraNavegacao_o_m_todo___limpacamposformulario___n_o_foi_implementado_, 'atencao', true, false, null);
        else if (!window.MontaCamposFormulario)
            window.top.mostraMensagem(traducao.barraNavegacao_o_m_todo___montacamposformulario___n_o_foi_implementado_, 'atencao', true, false, null);
        else
        {
            habilitaModoEdicaoBarraNavegacao(true, grid);
            TipoOperacao = idBotao;            
            hfGeral.Set("TipoOperacao", TipoOperacao);
            if (idBotao=="Incluir")
                LimpaCamposFormulario();
            if (idBotao=="Editar")
                OnGridFocusedRowChanged(grid, true) // true é para forçar a chamada da função MontaCampos
            if (popup != null)
                if (exibirPopup)
                    popup.Show();
            btnSalvar.SetVisible(true);
        }
    }
    else if(idBotao=="Excluir")
    {
        if (window.ExcluirRegistroSelecionado && grid.GetVisibleRowsOnPage() > 0)
        {
            TipoOperacao = idBotao;
            hfGeral.Set("TipoOperacao", TipoOperacao);
            window.top.mostraMensagem(traducao.barraNavegacao_deseja_realmente_excluir_o_registro_, 'confirmacao', true, true, ExcluirRegistroSelecionado);
        }
        else 
            window.top.mostraMensagem(traducao.barraNavegacao_o_m_todo___excluirregistroselecionado___n_o_foi_implementado_, 'atencao', true, false, null);
    }   
    else if(idBotao=="Primeiro")
      selecionaLinhaGrid(grid, popup, 0, false);
    else if(idBotao=="Anterior")
      selecionaLinhaGrid(grid, popup, grid.GetFocusedRowIndex() - 1, false);
    else if(idBotao=="Proximo")
      selecionaLinhaGrid(grid, popup, grid.GetFocusedRowIndex() + 1, false);
    else if(idBotao=="Ultimo")
      selecionaLinhaGrid(grid, popup, grid.visibleStartIndex + grid.GetVisibleRowsOnPage() - 1, false);
}

function selecionaLinhaGrid(grid, popup, numeroNovaLinha, retorno)
{
    if(numeroNovaLinha == 0)
    {
        numeroNovaLinha = grid.visibleStartIndex;
    }
    if(numeroNovaLinha >= 0)
    {
       try{ grid.SetFocusedRowIndex(numeroNovaLinha);}catch(e){}
    }
    // se o formulário estiver visível, executa montaCampos()
   // if (popup != null && popup.IsVisible())
    //{
   //     MontaCamposFormulario();
   // }
}

function ajustaVisualComRegistroGrid()
{
    if (gvDados.GetVisibleRowsOnPage()<=0) 
    {
      habilitarBotoesAcessoModoFormulario(false);
      habilitarBotoesNavegacao(false);
    }
}

function habilitarBotoesAcessoModoFormulario(habilitar, grid)
{
    // se o botão Primeiro não existir é pq a barra de navegação não existe.
    if (!window.imgBtnPrimeiro)
        return;

    if (habilitar==true && grid.GetVisibleRowsOnPage()>0)
    {        
        try{imgBtnFormulario.SetImageUrl(PathImagesUrl +'/pFormulario.png');}catch(e){}
        try{imgBtnEditar.SetImageUrl(PathImagesUrl +'/pEditar.png');}catch(e){}
        try{imgBtnExcluir.SetImageUrl(PathImagesUrl +'/pExcluir.png');}catch(e){}
        
        try{imgBtnFormulario.SetEnabled(true);}catch(e){}
        try{imgBtnEditar.SetEnabled(true);}catch(e){}
        try{imgBtnExcluir.SetEnabled(true);}catch(e){}
    }
    else
    {
        try{imgBtnFormulario.SetImageUrl(PathImagesUrl +'/pFormularioDisabled.png');}catch(e){}
        try{imgBtnEditar.SetImageUrl(PathImagesUrl +'/pEditarDisabled.png');}catch(e){}
        try{imgBtnExcluir.SetImageUrl(PathImagesUrl +'/pExcluirDisabled.png');}catch(e){}
        
        try{imgBtnFormulario.SetEnabled(false);}catch(e){}
        try{imgBtnEditar.SetEnabled(false);}catch(e){}
        try{imgBtnExcluir.SetEnabled(false);}catch(e){}
    }
}

function habilitarBotoesNavegacao(habilitar, grid)
{
    // se o botão Primeiro não existir é pq a barra de navegação não existe.
    if (!window.imgBtnPrimeiro)
        return;
        
    if (habilitar==true && grid.GetVisibleRowsOnPage()>0)
    {        
        try{imgBtnPrimeiro.SetImageUrl(PathImagesUrl +'/pFirst.png');}catch(e){}
        try{imgBtnAnterior.SetImageUrl(PathImagesUrl +'/pPrev.png');}catch(e){}
        try{imgBtnProximo.SetImageUrl(PathImagesUrl +'/pNext.png');}catch(e){}
        try{imgBtnUltimo.SetImageUrl(PathImagesUrl +'/pLast.png');}catch(e){}

        try{imgBtnPrimeiro.SetEnabled(true);}catch(e){}
        try{imgBtnAnterior.SetEnabled(true);}catch(e){}
        try{imgBtnProximo.SetEnabled(true);}catch(e){}
        try{imgBtnUltimo.SetEnabled(true);}catch(e){}
    }
    else
    {
        try{imgBtnPrimeiro.SetImageUrl(PathImagesUrl +'/pFirstDisabled.png');}catch(e){}
        try{imgBtnAnterior.SetImageUrl(PathImagesUrl +'/pPrevDisabled.png');}catch(e){}
        try{imgBtnProximo.SetImageUrl(PathImagesUrl +'/pNextDisabled.png');}catch(e){}
        try{imgBtnUltimo.SetImageUrl(PathImagesUrl +'/pLastDisabled.png');}catch(e){}
        
        try{imgBtnPrimeiro.SetEnabled(false);}catch(e){}
        try{imgBtnAnterior.SetEnabled(false);}catch(e){}
        try{imgBtnProximo.SetEnabled(false);}catch(e){}
        try{imgBtnUltimo.SetEnabled(false);}catch(e){}
    }
}

function habilitaModoEdicaoBarraNavegacao(habilitar, grid)
{
    // Se é para habilitar o modo de edição, temos que desabilitar alguns botões
    if (habilitar==true)
    {        
        // vamos desabilitar os botoes que dá acesso ao modo de edição - "F", "Editar" e "Excluir"
        habilitarBotoesAcessoModoFormulario(false, grid);
        
        // Desabilita os botões de navegação
        habilitarBotoesNavegacao(false, grid);

        // Desabilita o botão de inclusão        
        try{imgBtnIncluir.SetImageUrl(PathImagesUrl +'/pIncluirDisabled.png');}catch(e){}
    }
    else // voltou para o modo normal (tabela)
    {
        // vamos habilitar os botoes que dá acesso ao modo de edição - "F", "Editar" e "Excluir"
        habilitarBotoesAcessoModoFormulario(true, grid);

        // Habilita os botões de navegação
        habilitarBotoesNavegacao(true, grid);
        
        // Habilita o botão de inclusão
        try{imgBtnIncluir.SetImageUrl(PathImagesUrl +'/pIncluir.png');}catch(e){}
        
        // O tipo de operação deixa de ser Inclusão/Edição
        TipoOperacao = "";
    }
    
    // Habilitar/Desabilitar o botão Inclusão
    try{imgBtnIncluir.SetEnabled(habilitar==false);}catch(e){}

    // oculta a grid para evitar a mudança de registro 
    try{grid.SetVisible(habilitar==false);}catch(e){}
}

function onClick_btnSalvar()
{
    if (window.validaCamposFormulario)
    {
        if ( validaCamposFormulario() != "")
        {
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'atencao', true, false, null);
            return false;
        }
    }
    
    if (window.SalvarCamposFormulario)
    {        
        if (window.top.lpAguardeMasterPage.GetVisible() == false) {
            window.top.lpAguardeMasterPage.Show();
            setTimeout('window.top.lpAguardeMasterPage.Hide();', 15000);
            if (SalvarCamposFormulario()) {
                pcDados.Hide();
                habilitaModoEdicaoBarraNavegacao(false, gvDados);
                return true;
            }
        }
    }
    else
    {
        window.top.mostraMensagem(traducao.barraNavegacao_o_m_todo_n_o_foi_implementado_, 'atencao', true, false, null);
    }
}

function onClick_btnCancelar()
{
    habilitaModoEdicaoBarraNavegacao(false, gvDados);
    pcDados.Hide();
    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    return true;
}

function onEnd_pnCallback() {
    window.top.lpAguardeMasterPage.Hide();
    if (window.TipoOperacao) {
        if (hfGeral.Get("StatusSalvar") == "1") {
            if (TipoOperacao == "Incluir") {
                TipoOperacao = "Editar";
                hfGeral.Set("TipoOperacao", TipoOperacao);
            }
            if (window.posSalvarComSucesso)
                window.posSalvarComSucesso();
            else
                onClick_btnCancelar();
        }
        else if (hfGeral.Get("StatusSalvar") == "0") {
            mensagemErro = hfGeral.Get("ErroSalvar");

            if (TipoOperacao == "Excluir") {
                // se existe um tratamento de erro especifico da opçao que está sendo executada
                if (window.trataMensagemErro)
                    mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
                else // caso contrário, usa o tratamento padrão
                {
                    // se for erro de Chave Estrangeira (FK)
                    if (mensagemErro != null && mensagemErro.indexOf('REFERENCE') >= 0)
                        mensagemErro = traducao.barraNavegacao_o_registro_n_o_pode_ser_exclu_do_pois_est__sendo_utilizado_por_outro;
                }
            }
            else if (TipoOperacao == "Incluir") {
                // se existe um tratamento de erro especifico da opçao que está sendo executada
                if (window.trataMensagemErro)
                    mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
                else // caso contrário, usa o tratamento padrão
                {
                    // se for erro de Chave Estrangeira (FK)
                    if (mensagemErro != null && mensagemErro.indexOf('REFERENCE') >= 0)
                        mensagemErro = traducao.barraNavegacao_o_registro_n_o_pode_ser_inclu_do_pois_j__existe_no_banco_de_dados_;
                }
            }
            if (mensagemErro != "" && mensagemErro != undefined)
                 window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
        }
    }
}


/* Andre */
function habilitaTodos(sim)
    {
        var collection = ASPxClientControl.GetControlCollection();
        for (var key in collection.elements)
        {
            var control = collection.elements[key];
            if (control != null && ASPxIdent.IsASPxClientEdit(control))
                control.SetEnabled(sim);
            if (control != null && ASPxIdent.IsASPxClientButton(control))
            {
                var sempreHabilitado=/btnFechar/g;
                var sempreDesabilitado = /btnADD/g;
                var sempreDesabilitado1 = /btnRMV/g;
                
                var eHabilitado = sempreHabilitado.test(control.name.toString());
                var desabilitadoADD = sempreDesabilitado.test(control.name.toString());
                var desabilitadoRMV = sempreDesabilitado1.test(control.name.toString());
                
                if(eHabilitado)
                {
                    control.SetVisible(true);
                }
                else if(desabilitadoADD || desabilitadoRMV)
                {
                    control.SetVisible(sim);
                }
              
            }
        }  
    }
   