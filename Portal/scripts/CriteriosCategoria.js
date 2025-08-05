function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    if(txtCategoria.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.CriteriosCategoria_o_nome_da_categoria_deve_ser_informado_;
        txtCategoria.Focus();
    }    
    else if(txtSigla.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.CriteriosCategoria_a_sigla_da_categoria_deve_ser_informada_;
        txtSigla.Focus();
    }    

    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{
    txtCategoria.SetEnabled(TipoOperacao != "Consultar");
    txtSigla.SetEnabled(TipoOperacao != "Consultar");
    lbDisponiveis.SetEnabled(TipoOperacao != "Consultar");
    lbSelecionados.SetEnabled(TipoOperacao != "Consultar");
    lbDisponiveisRiscos.SetEnabled(TipoOperacao != "Consultar");
    lbSelecionadosRiscos.SetEnabled(TipoOperacao != "Consultar");
}

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

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtCategoria.SetText("");
    txtSigla.SetText("");
    
    if (TipoOperacao=="Incluir")
    {
        pn_ListBox_Criterios.PerformCallback("POP_-1");
        pn_ListBox_Riscos.PerformCallback("POP_-1");
    }

//        setTimeout("pnCallback.PerformCallback('clickInserir')", 10);
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoCategoria;DescricaoCategoria;SiglaCategoria;', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    var codigoCategoria = (valores[0] == null) ? "" : valores[0].toString();
    var descricaoCategoria =(valores[1] == null) ? "" : valores[1].toString();  
    var siglaCategoria =(valores[2] == null) ? "" : valores[2].toString();  
       
    txtCategoria.SetText(descricaoCategoria);
    txtSigla.SetText(siglaCategoria);
             
    pn_ListBox_Criterios.PerformCallback("POP_" + codigoCategoria);          
    pn_ListBox_Riscos.PerformCallback("POP_" + codigoCategoria);  
   
   //disparaCallback();
}


/*
function disparaCallback()
{
    try{setTimeout("pnCallback.PerformCallback('navegar')", 10);}catch(e){}   
}
*/  

function UpdateButtons() 
{

        try
        {
            btnADD.SetEnabled(           TipoOperacao !="" && TipoOperacao !="Consultar" && lbDisponiveis.GetSelectedItem() != null);
            btnADDTodos.SetEnabled(      TipoOperacao !="" && TipoOperacao !="Consultar" && lbDisponiveis.GetItemCount() > 0);
            btnRMV.SetEnabled(           TipoOperacao !="" && TipoOperacao !="Consultar" && lbSelecionados.GetSelectedItem() != null);
            btnRMVTodos.SetEnabled(      TipoOperacao !="" && TipoOperacao !="Consultar" && lbSelecionados.GetItemCount() > 0);
            btnADDRiscos.SetEnabled(     TipoOperacao !="" && TipoOperacao !="Consultar" && lbDisponiveisRiscos.GetSelectedItem() != null);
            btnADDTodosRiscos.SetEnabled(TipoOperacao !="" && TipoOperacao !="Consultar" && lbDisponiveisRiscos.GetItemCount() > 0 );
            btnRMVRiscos.SetEnabled(     TipoOperacao !="" && TipoOperacao !="Consultar" && lbSelecionadosRiscos.GetSelectedItem() != null);
            btnRMVTodosRiscos.SetEnabled(TipoOperacao !="" && TipoOperacao !="Consultar" && lbSelecionadosRiscos.GetItemCount() > 0);
        }catch(e){}
   
}

var delimitador = "¥";
function capturaCodigosCriteriosSelecionados()
{
    var CodigosCriteriosSelecionados = "";
    var TextosCriteriosSelecionados = "";
    for (var i = 0; i < lbSelecionados.GetItemCount(); i++) 
    {
        CodigosCriteriosSelecionados += lbSelecionados.GetItem(i).value + delimitador;
        TextosCriteriosSelecionados += lbSelecionados.GetItem(i).text + delimitador;
    }
    hfCriteriosSelecionados.Set("CodigosCriteriosSelecionados", CodigosCriteriosSelecionados);
    hfCriteriosSelecionados.Set("TextosCriteriosSelecionados", TextosCriteriosSelecionados);
}

function capturaCodigosRiscosSelecionados()
{
    var CodigosRiscosSelecionados = "";
    var TextosRiscosSelecionados = "";
    for (var i = 0; i < lbSelecionadosRiscos.GetItemCount(); i++) 
    {
        CodigosRiscosSelecionados += lbSelecionadosRiscos.GetItem(i).value + delimitador;
        TextosRiscosSelecionados += lbSelecionadosRiscos.GetItem(i).text + delimitador;
    }
    hfRiscosSelecionados.Set("CodigosRiscosSelecionados", CodigosRiscosSelecionados);
    hfRiscosSelecionados.Set("TextosRiscosSelecionados", TextosRiscosSelecionados);
}
       
        
function alteraValor(s, e, numeroLinha, numeroColuna)
{
   try
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
    
    }catch(e){}
}

 function alteraValorRiscos(s, e, numeroLinha, numeroColuna)
{   
    try
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
    }catch(e){}
}

function calculaPeso()
{
   try
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
    }catch(e){}
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



