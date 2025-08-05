// JScript File
//document.onkeyup = ProcessKeyPress;
    function OnGridFocusedRowChanged(s) 
    {
        //Recebe os valores dos campos de acordo com a linha selecionada. O resultado é passado para a função montaCampo
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoUsuario;NomeUsuario;TipoAutenticacao;EMail;TelefoneContato1;TelefoneContato2;Observacoes;CodigoUnidadePrimeiraInclusao;CodigoEntidade;listaPrimeiraUnidadeUsuario;usuarioAtivo;gerente', montaCampos);
       
    }
    
    function montaCampos(valores)
    {          
        var codigoUsuario = (valores[0] == null)? "": valores[0].toString();
        var nomeUsuario =(valores[1] == null)? "": valores[1].toString();
        var tipoAutenticacao = (valores[2] == null)? "": valores[2].toString();
        var email =(valores[3] == null)? "": valores[3].toString();
        var telefoneContato1 = (valores[4] == null)? "": valores[4].toString();
        var telefoneContato2 = (valores[5] == null)? "": valores[5].toString();
        var observacoes = (valores[6] == null)? "": valores[6].toString();
        var codigoUnidadePrimeiraInclusao = (valores[7] == null)? "": valores[7].toString();
        var codigoEntidade = (valores[8] == null)? "": valores[8].toString();
        
        var listaPrimeiraUnidadeUsuario = (valores[9] == null)? "": valores[9].toString();
        var usuarioAtivo = (valores[10] == null)? "": valores[10].toString();
        var gerente = (valores[11] == null)? "": valores[11].toString();
        
        txtNomeUsuario.SetText(nomeUsuario);
        txtEmail.SetText(email);
        txtFoneContato1.SetText(telefoneContato1);
        txtFoneContato2.SetText(telefoneContato2);
        memObservacoes.SetText(observacoes);
        
        cmbTipoAutentica.SetValue(tipoAutenticacao);
        
        //painel de acesso a unidades do usuario
        //cmbUnidades1.SetValue(listaPrimeiraUnidadeUsuario);
        ckbUsuarioAtivo.SetChecked((usuarioAtivo == "S")?true:false);
        ckbUsuarioGerente.SetChecked((gerente == "S")?true:false);
            
       cmbUnidades1.SetValue(codigoUnidadePrimeiraInclusao);
       
        hfGeral.Set("hfCodigoUsuario",codigoUsuario);
        if(codigoUnidadePrimeiraInclusao == codigoEntidade)
        {
            imgEdicao.SetVisible(true);
            imgExclusao.SetVisible(true);
        }
        else
        {
            imgEdicao.SetVisible(false);
            imgExclusao.SetVisible(false);
        }
        
    }
   
    function SelectOption() 
    {
        MoveSelectedItem(lbDisponiveis, lbSelecionados);
    }
    function SelectAllOptions() 
    {
       MoveItems(lbDisponiveis, lbSelecionados);
    }
    function UnselectOption() 
    {
       MoveSelectedItem(lbSelecionados, lbDisponiveis);
    }
    function UnselectAllOptions() 
    {
       MoveItems(lbSelecionados, lbDisponiveis);
    }
        
    function MoveItems(lb1, lb2) 
     {
         lb2.BeginUpdate();
         var itemCount = lb1.GetItemCount();
         
         for (var i = 0; i < itemCount; i++) 
         {
             var item = lb1.GetItem(0);
             lb2.InsertItem(GetCurrentIndex(item.value, lb2), item.text, item.value);
             lb1.RemoveItem(0);
         }
         lb2.EndUpdate();
         UpdateButtons();         
    }    
    
    function GetCurrentIndex(value, lbDestination)
    {
        var options = GetPrimaryOptions();
        for(var i = (GetPrimaryIndex(value) - 1) ; i >= 0; i--)
        {
            var neighborIndex = GetItemIndexByValue(options[i], lbDestination);
            if( neighborIndex != -1)
                return neighborIndex + 1;
        }
        return 0;
    }
        
     function GetItemIndexByValue(value, listBox)
    {
        var itemCount = listBox.GetItemCount();
        for (var i = 0; i < itemCount; i++)
           if(listBox.GetItem(i).value == value)
                return i;
        return -1;
    }
    
         function GetPrimaryIndex(value)
        {
            var options = GetPrimaryOptions();
            for(var i = 0; i < options.length; i++)
                if(options[i] == value)
                    return i;
        }
        
        function GetPrimaryOptions()
        {
            return hiddenField.Get("options");
        }
    
    function MoveSelectedItem(lb1, lb2) 
    {
            var item = lb1.GetSelectedItem();
            
            if (item != null) 
            {
                lb2.InsertItem(GetCurrentIndex(item.value, lb2), item.text, item.value);
                lb1.RemoveItem(item.index);
            }
            UpdateButtons();
   }
    
    
    function UpdateButtons() 
     {
            try
            {
                btnADD.SetEnabled(lbDisponiveis.GetSelectedItem() != null && lbSelecionados.GetItemCount() < 6);
                btnADDTodos.SetEnabled(lbDisponiveis.GetItemCount() > 0 && lbSelecionados.GetItemCount() < 6);
                btnRMV.SetEnabled(lbSelecionados.GetSelectedItem() != null);
                btnRMVTodos.SetEnabled(lbSelecionados.GetItemCount() > 0);
            }catch(e){}
     }
     
     function limpaGridSelecionados()
     {
        try
        {
            lbSelecionados.BeginUpdate();
            
             var itemCount = lbSelecionados.GetItemCount();
             for (var i = 0; i < itemCount; i++) 
             {
                 lbSelecionados.RemoveItem(0);
             }
             lbSelecionados.EndUpdate();
         }catch(e){}
     }
     
     function iniciaGridSelecionados()
     {
        try
        {           
            limpaGridSelecionados();
            lbSelecionados.BeginUpdate();
             for (var i = 0; i < parseInt(hfCount.Get("QuantidadeSelecionados")); i++) 
             {
                 var item = lbDisponiveis.GetItem(0);
                 lbSelecionados.InsertItem(GetCurrentIndex(item.value, lbSelecionados), item.text, item.value);
                 lbDisponiveis.RemoveItem(0);
             }
             lbSelecionados.EndUpdate();
             UpdateButtons();
         }catch(e){}
     }
          
     function validaCadastro()
     {
        var retorno = true;
        if(txtNomeUsuario.GetText() == "")
        {
            window.top.mostraMensagem(traducao.cadastroUsuarios_nome_do_usu_rio_deve_ser_informado_, 'erro', true, false, null);
            txtNomeUsuario.Focus();
            retorno = false;
            return false;
        }
        if(txtEmail.GetText() == "")
        {
            window.top.mostraMensagem(traducao.cadastroUsuarios_email_do_usu_rio_ser_informado_, 'erro', true, false, null);
            txtEmail.Focus();
            retorno = false;
            return false;
        }
           return retorno;
     }