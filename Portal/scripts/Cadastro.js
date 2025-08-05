// JScript File
   //document.onkeyup = ProcessKeyPress;
    
    function ProcessKeyPress()
    {   
      try
      {   
     //37 esquerda, 39 direita 
     //backspace no modo de consulta deve retornar falso
     if(event.keyCode == 8 && hfGeral.Get("hfModoEdicao").toString() == "C")
        return false;
        
     if(hfGeral.Get("hfModoEdicao").toString() == "C")
     {  
          var currentIndex = grid.GetFocusedRowIndex();
          
          if (event.keyCode == 40||event.keyCode == 39)
          {
              if (currentIndex == grid.GetVisibleRowsOnPage() - 1)
              {
                    grid.SetFocusedRowIndex(currentIndex);
              } 
              else
              {
                    grid.SetFocusedRowIndex(currentIndex+1);
              }
          }
          if (event.keyCode == 38||event.keyCode == 37)
          {
              if (currentIndex == 0)
              {
                    return;
              } 
              else
              {
                    grid.SetFocusedRowIndex(currentIndex-1);
              }
          }
       }
       }
       catch(e){}
    }

    function mudaLinhaSelecionada(objGrid, numeroNovaLinha, retorno)
    {
        if(numeroNovaLinha == 0)
        {
            numeroNovaLinha = objGrid.visibleStartIndex;
        }
        if(numeroNovaLinha >= 0)
        {
           try{ objGrid.SetFocusedRowIndex(numeroNovaLinha);}catch(e){}
        }
    }
    
    function habilitaEdicao(objTab, tipoEdicao, retorno)
    {       

        if(tipoEdicao == 'NOVO')
        {
           hfGeral.Set("hfModoEdicao", "I");
        }
        else
        { 
            hfGeral.Set("hfModoEdicao", "E");
        }
        objTab.SetActiveTab(objTab.GetTabByName('TabE'));        
        document.getElementById('tbBotoesEdicao').style.display = 'block';
        
        objTab.GetTabByName('TabC').SetEnabled(false);
        objTab.GetTabByName('TabE').SetEnabled(true);
        
        try{imgPrimeiro.SetImageUrl(pastaImagens +'/Navegacao/btnPrimeiroDes.png');}catch(e){}
        try{imgAnterior.SetImageUrl(pastaImagens +'/Navegacao/btnAnteriorDes.png');}catch(e){}
        try{imgProximo.SetImageUrl(pastaImagens +'/Navegacao/btnProximoDes.png');}catch(e){}
        try{imgUltimo.SetImageUrl(pastaImagens +'/Navegacao/btnUltimoDes.png');}catch(e){}
        try{imgNovo.SetImageUrl(pastaImagens +'/Navegacao/btnInserirDes.png');}catch(e){}
        try{imgEdicao.SetImageUrl(pastaImagens +'/Navegacao/btnEditarDes.png');}catch(e){}
        try{imgExclusao.SetImageUrl(pastaImagens +'/Navegacao/btnExcluirDes.png');}catch(e){}
        
        try{imgPrimeiro.SetEnabled(false);}catch(e){}
        try{imgAnterior.SetEnabled(false);}catch(e){}
        try{imgProximo.SetEnabled(false);}catch(e){}
        try{imgUltimo.SetEnabled(false);}catch(e){}
        try{imgNovo.SetEnabled(false);}catch(e){}
        try{imgEdicao.SetEnabled(false);}catch(e){}
        try{imgExclusao.SetEnabled(false);}catch(e){}
        
   } 
    
    
    function desabilitaEdicao(objTab)
    {

        objTab.SetActiveTab(tabControl.GetTabByName('TabC'));
        OnGridFocusedRowChanged();
        if(document.getElementById('tbBotoesEdicao') != null)
        {
            document.getElementById('tbBotoesEdicao').style.display = 'none';
        }            
        
        hfGeral.Set("hfModoEdicao", "C");
         
        objTab.GetTabByName('TabC').SetEnabled(true);
         
        try{imgNovo.SetImageUrl(pastaImagens + '/Navegacao/btnInserir.png');}catch(e){}      
                      
        if(grid.GetFocusedRowIndex() >= 0)
        {                                    
            try{imgPrimeiro.SetImageUrl(pastaImagens +'/Navegacao/btnPrimeiro.PNG');}catch(e){}
            try{imgAnterior.SetImageUrl(pastaImagens +'/Navegacao/btnAnterior.png');}catch(e){}
            try{imgProximo.SetImageUrl(pastaImagens +'/Navegacao/btnProximo.png');}catch(e){}
            try{imgUltimo.SetImageUrl(pastaImagens +'/Navegacao/btnUltimo.png');}catch(e){}
            try{imgEdicao.SetImageUrl(pastaImagens +'/Navegacao/btnEditar.png');}catch(e){}
            try{imgExclusao.SetImageUrl(pastaImagens +'/Navegacao/btnExcluir.png');}catch(e){}
                        
            try{imgPrimeiro.SetEnabled(true);}catch(e){}
            try{imgAnterior.SetEnabled(true);}catch(e){}
            try{imgProximo.SetEnabled(true);}catch(e){}
            try{imgUltimo.SetEnabled(true);}catch(e){}
            try{imgNovo.SetEnabled(true);}catch(e){}
            try{imgEdicao.SetEnabled(true);}catch(e){}
            try{imgExclusao.SetEnabled(true);}catch(e){}
        }
        else
        {
            objTab.GetTabByName('TabE').SetEnabled(false);
            
            try{imgPrimeiro.SetImageUrl(pastaImagens +'/Navegacao/btnPrimeiroDes.png');}catch(e){}
            try{imgAnterior.SetImageUrl(pastaImagens +'/Navegacao/btnAnteriorDes.png');}catch(e){}
            try{imgProximo.SetImageUrl(pastaImagens +'/Navegacao/btnProximoDes.png');}catch(e){}
            try{imgUltimo.SetImageUrl(pastaImagens +'/Navegacao/btnUltimoDes.png');}catch(e){}
            try{imgEdicao.SetImageUrl(pastaImagens +'/Navegacao/btnEditarDes.png');}catch(e){}
            try{imgExclusao.SetImageUrl(pastaImagens +'/Navegacao/btnExcluirDes.png');}catch(e){}
                        
            try{imgPrimeiro.SetEnabled(false);}catch(e){}
            try{imgAnterior.SetEnabled(false);}catch(e){}
            try{imgProximo.SetEnabled(false);}catch(e){}
            try{imgUltimo.SetEnabled(false);}catch(e){}
            try{imgNovo.SetEnabled(true);}catch(e){}
            try{imgEdicao.SetEnabled(false);}catch(e){}
            try{imgExclusao.SetEnabled(false);}catch(e){}
            
        }
        
    }     
    
function confirmarExclusao(objGrid) {
    if (objGrid.GetVisibleRowsOnPage() > 0) {
        return confirm("Confirma exclusão?");
    }
    else {
        return false;
    }
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 40;
    gvProjetosDependenciaPai.SetHeight(height);
    gvProjetosDependenciaFilho.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}


