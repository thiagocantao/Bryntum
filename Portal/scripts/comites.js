// JScript File
function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    var mensagemErro_ValidaCamposFormulario = "";    
        
    if(txtNomeGrupo.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.comites_nome_do_grupo_deve_ser_informado_;
    }
    return mensagemErro_ValidaCamposFormulario;
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
    txtNomeGrupo.SetText("");
    ddlPortfolios.SetValue("");
    lbSelecionados.ClearItems();
    
    if (TipoOperacao=="Incluir")
        pnListBox.PerformCallback("POP_-1");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoGrupoPortfolio;DescricaoGrupo;CodigoPortfolio;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    txtNomeGrupo.SetText((values[1]==null) ? "" : values[1]);
    //hfGeral.Set("hfCodigoComite",(values[0]==null) ? "" : values[0]);
    pnListBox.PerformCallback("POP_" + values[0]);
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
            if(edicaoAtiva == 'S')
            {
                btnADDTodos.SetClientVisible(true);
                btnADD.SetClientVisible(true);
                btnRMV.SetClientVisible(true);
                btnRMVTodos.SetClientVisible(true);
                
                btnADDTodos.SetEnabled(lbDisponiveis.GetItemCount() > 0);
                btnADD.SetEnabled(lbDisponiveis.GetSelectedItem() != null);
                btnRMV.SetEnabled(lbSelecionados.GetSelectedItem() != null);
                btnRMVTodos.SetEnabled(lbSelecionados.GetItemCount() > 0);
            }
            else
            {
                btnADDTodos.SetClientVisible(false);
                btnADD.SetClientVisible(false);
                btnRMV.SetClientVisible(false);
                btnRMVTodos.SetClientVisible(false);
            }
            
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
 //metodo iniciagridselecionados é chamado no evento cliente
 //onInit do listbox lbDisponiveis
 //ele pega todos os items da grid selecionados e remove item por item ate a
 //grid selecionados estiver vazia
 //depois sao inseridos na lbSelecionados os items da lbDisponiveis até a quantidade
 
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
      
