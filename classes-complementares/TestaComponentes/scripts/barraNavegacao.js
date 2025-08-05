// JScript File
function onClickBarraNavegacao(idBotao, grid, popup)
{
    if (!window.hfGeral)
        alert("O componente \"hfGeral\" não foi encontrado! \nEste componente deve ser colocado dentro do componente \"pnCallback\"");
        
    if (!window.OnGridFocusedRowChanged)
        alert("O método \"OnGridFocusedRowChanged\" não foi implementado!");
        
    // alert(idBotao);
    // se entrou no modo de edição 
    if (idBotao=="Incluir" || idBotao=="Editar" )
    {
        if (! window.LimpaCamposFormulario)
            alert("O método \"LimpaCamposFormulario\" não foi implementado!");
        else if (!window.MontaCamposFormulario)
            alert("O método \"MontaCamposFormulario\" não foi implementado!");
        else
        {
            habilitaModoEdicaoBarraNavegacao(true, grid);
            TipoOperacao = idBotao;
            if (idBotao=="Incluir")
                LimpaCamposFormulario();
            if (idBotao=="Editar")
                OnGridFocusedRowChanged(grid, true)
            if (popup!=null)
                popup.Show();
            btnSalvar.SetVisible(true);
        }
    }
    else if(idBotao=="Excluir")
    {
        if (window.ExcluirRegistroSelecionado && grid.GetVisibleRowsOnPage() > 0)
        {
            if (confirm('Deseja realmente excluir o registro?'))
            {
                TipoOperacao = idBotao;
                ExcluirRegistroSelecionado();
            }
        }
        else 
            alert("O método \"ExcluirRegistroSelecionado\" não foi implementado!");
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
            alert(mensagemErro_ValidaCamposFormulario);
            return false;
        }
    }
    
    if (window.SalvarCamposFormulario)
    {
        if (SalvarCamposFormulario())
        {
            pcDados.Hide();
            habilitaModoEdicaoBarraNavegacao(false, gvDados);
            return true;
        }
    }
    else
    {
        alert("O método não foi implementado!");
    }
}

function onClick_btnCancelar()
{
    habilitaModoEdicaoBarraNavegacao(false, gvDados);
    pcDados.Hide();
    return true;
}

function onEnd_pnCallback()
{
    if (hfGeral.Get("StatusSalvar")=="1")
        onClick_btnCancelar();        
    else
        alert(hfGeral.Get("ErroSalvar"));
}