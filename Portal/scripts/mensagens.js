// JScript File
var delimitadorLocal = ";";

function validaMensagem()
{

    var retorno = "";
    if(txtMensagem.GetText() == "")
    {
        retorno = traducao.mensagens_mensagem_deve_ser_informada;
    }
    if(ckbRespondeMsg.GetChecked()==true && dtePrazo.GetText() == "")
    {
        retorno = traducao.mensagens_prazo_deve_ser_informado;
    }
    else
    {
        if(dtePrazo.GetValue() != null)
        {
            var dataAtual 	 = new Date();
	        var meuDataAtual = (dataAtual.getMonth() +1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
	        var dataHoje 	 = Date.parse(meuDataAtual);

	        var dataPrazo = new Date(dtePrazo.GetValue());
            
            if(dataPrazo < dataHoje)
            {
                 retorno = traducao.mensagens_prazo_n_o_deve_ser_anterior_a_data_atual;
            }
        }  
    }
    
    if(lbSelecionados.GetItemCount() == 0)
    {
        retorno = traducao.mensagens_pelo_menos_um_destinat_rio_deve_ser_escolhido;
    }
    return retorno;
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
        btnADDTodos.SetEnabled(lbDisponiveis.GetItemCount() > 0);
        btnADD.SetEnabled(lbDisponiveis.GetSelectedItem() != null);
        btnRMV.SetEnabled(lbSelecionados.GetSelectedItem() != null);
        btnRMVTodos.SetEnabled(lbSelecionados.GetItemCount() > 0);
        capturaCodigosDestinatariosSelecionados();
    }catch(e){}
 }
 
 function capturaCodigosDestinatariosSelecionados()
{
    var CodigosPerfisSelecionados = "";
    for (var i = 0; i < lbSelecionados.GetItemCount(); i++) 
    {
        CodigosPerfisSelecionados += lbSelecionados.GetItem(i).value + delimitadorLocal;
    }
    hfGeral.Set("CodigosDestinatariosSelecionados", CodigosPerfisSelecionados);
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
      
//para funcionar o label mostrador de quantos caracteres faltam para terminar o texto
function setMaxLength(textAreaElement, length) {
    textAreaElement.maxlength = length;
    ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
    ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
}

function onKeyUpOrChange(evt) {
    processTextAreaText(ASPxClientUtils.GetEventSource(evt));
}

function processTextAreaText(textAreaElement) {
    var maxLength = textAreaElement.maxlength;
    var text = textAreaElement.value;
    var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
    if (maxLength != 0 && text.length > maxLength) 
        textAreaElement.value = text.substr(0, maxLength);
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}