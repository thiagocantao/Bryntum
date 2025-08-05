
function OnGridFocusedRowChanged(s) 
{
        //Recebe os valores dos campos de acordo com a linha selecionada. O resultado é passado para a função montaCampo
        try
        {            
            s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoCategoria;DescricaoCategoria;', montaCampos);
        }
        catch(e)
        {}
}

function montaCampos(valores)
{ 
    try
    {    
       var codigoCategoria = (valores[0] == null) ? "" : valores[0].toString();
       var descricaoCategoria =(valores[1] == null) ? "" : valores[1].toString();  
           
       txtCategoria.SetText(descricaoCategoria);
       
       hfGeral.Set("hfCodigo",codigoCategoria);     
          
          
       disparaCallback();
       }catch(e){}
}

function disparaCallback()
{
    try{setTimeout("painelCallback.PerformCallback('navegar')", 10);}catch(e){}   
}
    
function UpdateButtons() 
{
    try
    {
        btnADD.SetEnabled(lbDisponiveis.GetSelectedItem() != null);
        btnADDTodos.SetEnabled(lbDisponiveis.GetItemCount() > 0);
        btnRMV.SetEnabled(lbSelecionados.GetSelectedItem() != null);
        btnRMVTodos.SetEnabled(lbSelecionados.GetItemCount() > 0);
        btnADDRiscos.SetEnabled(lbDisponiveisRiscos.GetSelectedItem() != null);
        btnADDTodosRiscos.SetEnabled(lbDisponiveisRiscos.GetItemCount() > 0 );
        btnRMVRiscos.SetEnabled(lbSelecionadosRiscos.GetSelectedItem() != null);
        btnRMVTodosRiscos.SetEnabled(lbSelecionadosRiscos.GetItemCount() > 0);
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
     
     function limpaGridSelecionadosRiscos()
     {
        try
        {
            lbSelecionadosRiscos.BeginUpdate();
            
             var itemCount = lbSelecionadosRiscos.GetItemCount();
             for (var i = 0; i < itemCount; i++) 
             {
                 lbSelecionadosRiscos.RemoveItem(0);
             }
             lbSelecionadosRiscos.EndUpdate();
         }catch(e){}
     }
               
     function iniciaGridSelecionados()
     {
        try
        {
            limpaGridSelecionados();
            lbSelecionados.BeginUpdate();
             var itemCount = lbDisponiveis.GetItemCount();
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
     
     function iniciaGridSelecionadosRiscos()
     {
        try
        {
            limpaGridSelecionadosRiscos();
            lbSelecionadosRiscos.BeginUpdate();
             var itemCount = lbDisponiveisRiscos.GetItemCount();
             for (var i = 0; i < parseInt(hfCountRiscos.Get("QuantidadeSelecionados")); i++) 
             {
                 var item = lbDisponiveisRiscos.GetItem(0);
                 lbSelecionadosRiscos.InsertItem(GetCurrentIndex(item.value, lbSelecionadosRiscos), item.text, item.value);
                 lbDisponiveisRiscos.RemoveItem(0);
             }
             lbSelecionadosRiscos.EndUpdate();
             UpdateButtons();             
         }catch(e){}
     }
     
     function MoveItems(lb1, lb2) 
     {
        try
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
         }catch(e){}
    }    
    
    function MoveSelectedItem(lb1, lb2) 
    {
        try
        {
            var item = lb1.GetSelectedItem();
            if (item != null) 
            {
                lb2.InsertItem(GetCurrentIndex(item.value, lb2), item.text, item.value);
                lb1.RemoveItem(item.index);
            }
            UpdateButtons();
            }catch(e){}
   }
        
        function SelectOption() 
        {
            MoveSelectedItem(lbDisponiveis, lbSelecionados);
        }
        
        function SelectOptionRiscos() 
        {
            MoveSelectedItem(lbDisponiveisRiscos, lbSelecionadosRiscos);
        }
        
        function SelectAllOptions() 
        {
            MoveItems(lbDisponiveis, lbSelecionados);
        }
        
        function SelectAllOptionsRiscos() 
        {
            MoveItems(lbDisponiveisRiscos, lbSelecionadosRiscos);
        }
        
        function UnselectOption() 
        {
            MoveSelectedItem(lbSelecionados, lbDisponiveis);
        }
        
        function UnselectOptionRiscos() 
        {
            MoveSelectedItem(lbSelecionadosRiscos, lbDisponiveisRiscos);
        }
        
        function UnselectAllOptions() 
        {
            MoveItems(lbSelecionados, lbDisponiveis);
        }
        
         function UnselectAllOptionsRiscos() 
        {
            MoveItems(lbSelecionadosRiscos, lbDisponiveisRiscos);
        }
        
        function GetPrimaryIndex(value)
        {
            var options = GetPrimaryOptions();
            for(var i = 0; i < options.length; i++)
                if(options[i] == value)
                    return i;
        }
        
        function GetPrimaryIndexRiscos(value)
        {
            var options = GetPrimaryOptionsRiscos();
            for(var i = 0; i < options.length; i++)
                if(options[i] == value)
                    return i;
        }
        
        function GetCurrentIndex(value, lbDestination)
        {
            var options = GetPrimaryOptions();
            for(var i = (GetPrimaryIndex(value) - 1); i >= 0; i--)
            {
                var neighborIndex = GetItemIndexByValue(options[i], lbDestination);
                if( neighborIndex != -1)
                    return neighborIndex + 1;
            }
            return 0;
        }
        
        function GetCurrentIndexRiscos(value, lbDestination)
        {
            var options = GetPrimaryOptionsRiscos();
            for(var i = (GetPrimaryIndex(value) - 1); i >= 0; i--)
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
        function GetPrimaryOptions()
        {
            return hiddenField.Get("options");
        }
        
        function GetPrimaryOptionsRiscos()
        {
            return hiddenField.Get("optionsRiscos");
        }
        
        
        function alteraValor(s, e, numeroLinha, numeroColuna)
        {
            var nome = s.name.substring(60);
	        //var numeroLinha = nome.substring(0, nome.indexOf('_'));
	        //var numeroColuna = nome.substring(nome.indexOf('_') + 1, nome.lastIndexOf('_'));
	        var novoValor = 1;
	        var numeroLinhas = gridMatriz.GetVisibleRowsOnPage();
	        var valorTotal = ((numeroLinhas * numeroLinhas) / 2) + (numeroLinhas / 2);
	        	       
	        hfValores.Set('Criterio_' + numeroLinha + '_' + numeroColuna, s.GetText());
	        	        
	        if(s.GetText() == 1)
	        {	
		        novoValor = 0;
            }          
            document.getElementById('cell_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1)).innerText = novoValor;
            hfValores.Set('Criterio_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1), novoValor);
	            
        	
	        calculaPeso();
        }
        
         function alteraValorRiscos(s, e, numeroLinha, numeroColuna)
        {
            var nome = s.name;
	        //var numeroLinha = nome.substring(0, nome.indexOf('_'));
	        //var numeroColuna = nome.substring(nome.indexOf('_') + 1, nome.lastIndexOf('_'));
	        var novoValor = 1;
	        var numeroLinhas = gridMatrizRiscos.GetVisibleRowsOnPage();
	        var valorTotal = ((numeroLinhas * numeroLinhas) / 2) + (numeroLinhas / 2);
	        	       
	        hfValoresRiscos.Set('Risco_' + numeroLinha + '_' + numeroColuna, s.GetText());
	        	        
	        if(s.GetText() == 1)
	        {	
		        novoValor = 0;
            }          
            document.getElementById('cellRiscos_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1)).innerText = novoValor;
            hfValoresRiscos.Set('Risco_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1), novoValor);
	            
        	
	        calculaPesoRiscos();
        }
        
        function calculaPeso()
        {
	        var novoValor = 1;
	        var numeroLinhas = gridMatriz.GetVisibleRowsOnPage();
	        var valorTotal = ((numeroLinhas * numeroLinhas) / 2) + (numeroLinhas / 2);
	        
            for(i = 0; i < numeroLinhas; i++)
	        {
	            var valorLinha = 0;
	             for(j = 0; j < numeroLinhas; j++)
	            {
	                if(i < j)
	                {
	                       valorLinha = valorLinha + parseInt(document.getElementById('txt_' + i + '_' + (j + 1) + '_Raw').value);	
        	        }
	                else
	                {
	                   
    	                valorLinha = valorLinha + parseInt(document.getElementById('cell_' + i + '_' + (j + 1)).innerText);          	              	                	                
    	            }
	            } 
	            document.getElementById('cell_' + i + '_' + (numeroLinhas + 1)).innerText = parseInt((valorLinha / valorTotal) * 100) + "%";        	    
	        }
        }
        
        function calculaPesoRiscos()
        {
	        var novoValor = 1;
	        var numeroLinhas = gridMatrizRiscos.GetVisibleRowsOnPage();
	        var valorTotal = ((numeroLinhas * numeroLinhas) / 2) + (numeroLinhas / 2);
	        
            for(i = 0; i < numeroLinhas; i++)
	        {
	            var valorLinha = 0;
	             for(j = 0; j < numeroLinhas; j++)
	            {
	                if(i < j)
	                {
	                       valorLinha = valorLinha + parseInt(document.getElementById('txtRiscos_' + i + '_' + (j + 1) + '_Raw').value);	
        	        }
	                else
	                {
	                   
    	                valorLinha = valorLinha + parseInt(document.getElementById('cellRiscos_' + i + '_' + (j + 1)).innerText);          	              	                	                
    	            }
	            } 
	            document.getElementById('cellRiscos_' + i + '_' + (numeroLinhas + 1)).innerText = parseInt((valorLinha / valorTotal) * 100) + "%";        	    
	        }
        }