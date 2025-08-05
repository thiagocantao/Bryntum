// JScript File
var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
var isIE7 = (window.navigator.userAgent.indexOf("MSIE 7.0") > 0);
var isFirefox = (window.navigator.userAgent.indexOf("Mozilla") >= 0);
function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    if(txtAssunto.GetValue() == null)
    {
        mensagemErro_ValidaCamposFormulario = traducao.CadastroAvisos_campo_assunto_deve_ser_informado_;
    }
    else if(dteInicio.GetValue()== null)
    {
        mensagemErro_ValidaCamposFormulario = traducao.CadastroAvisos_campo_data_de_in_cio_deve_ser_informado_;
    }
    else if(dteTermino.GetValue()  == null)
    {
        mensagemErro_ValidaCamposFormulario = traducao.CadastroAvisos_campo_data_de_t_rmino_deve_ser_informado_;
    }  
    else if(ddlTipoDestinatario.GetValue() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.CadastroAvisos_campo_tipo_de_destinat_rio_deve_ser_informado_;
    }
    else if(txtAviso.GetValue() == " " || txtAviso.GetValue() == "" || txtAviso.GetValue() == null)
    {
        mensagemErro_ValidaCamposFormulario = traducao.CadastroAvisos_campo_aviso_deve_ser_informado_;
    }
    
    if(dteInicio.GetValue()!= null && dteTermino.GetValue()  != null)
    {
        
        var dataAtual 	 = new Date();
	    var meuDataAtual = (dataAtual.getMonth() +1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
	    var dataHoje 	 = Date.parse(meuDataAtual);
        
        var dataInicio 	  = new Date(dteInicio.GetValue());
		var meuDataInicio = (dataInicio.getMonth() +1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
		dataInicio  	  = Date.parse(meuDataInicio);
		
		
		var dataTermino 	= new Date(dteTermino.GetValue());
		var meuDataTermino 	= (dataTermino.getMonth() +1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
		dataTermino		    = Date.parse(meuDataTermino);
		
		if("Incluir" == TipoOperacao)
		{
		    if( dataInicio < dataHoje)
		    {
                mensagemErro_ValidaCamposFormulario = traducao.CadastroAvisos_a_data_de_in_cio_n_o_pode_ser_anterior___data_atual_ + "\n";
		    }
		    if( dataTermino < dataHoje)
		    {
                mensagemErro_ValidaCamposFormulario = traducao.CadastroAvisos_a_data_de_t_rmino_n_o_pode_ser_anterior___data_atual_ + "\n";
		    }
		    if(dataInicio > dataTermino)
            {
                mensagemErro_ValidaCamposFormulario = traducao.CadastroAvisos_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n";
            }    
		}
		
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
        try
        {
        txtAssunto.SetText("");
        dteInicio.SetText("");
        dteTermino.SetText("");
        txtAviso.SetText("");
        ddlTipoDestinatario.SetValue("TD");
        rdPainel.SetVisible(false);
        
        dteInicio.SetEnabled(true);
        dteTermino.SetEnabled(true);
    
        document.getElementById('pnDestinatario').style.height="0px";
        document.getElementById('txtAviso').style.height="247px";
        lblCantCarater.SetText("0");
        }catch(e){}
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
   if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    /*                                                      0;      1;    2;         3;          4;           5;                    6;             7;               8;*/
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoAviso;Assunto;Aviso;DataInicio;DataTermino;DataInclusao;CodigoUsuarioInclusao;CodigoEntidade;tipoDestinatario;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    txtAssunto.SetText((values[1] == null) ? "" : values[1]);
   
   if(values[3] == null)
   {
        dteInicio.SetText("");    
   }
   else
   {
        dteInicio.SetValue(values[3]);
   }
   
   if(values[4] == null)
   {
        dteTermino.SetText("");    
   }
   else
   {
        dteTermino.SetValue(values[4]);
   }

    if(TipoOperacao == "Editar" || TipoOperacao == "Consultar")
   {
   
        dteInicio.SetEnabled(false);
        dteTermino.SetEnabled(false);
   }
   else if (TipoOperacao == "Incluir")
   {
        dteInicio.SetEnabled(true);
        dteTermino.SetEnabled(true);
   }

   ddlTipoDestinatario.SetValue((values[8] == null) ? "TD" : values[8]);

   txtAviso.SetText((values[2] == null) ? "" : values[2]);
 
   
   hfGeral.Set("CodigoAviso",(values[0]==null) ? " " : values[0]); 
     
   pnDestinatario.PerformCallback("popula");
   
   if(ddlTipoDestinatario.GetValue() ==null || ddlTipoDestinatario.GetValue()=="TD")
   {
        document.getElementById('pnDestinatario').style.height="0px";
        document.getElementById('txtAviso').style.height="247px";
    }
    else
    {
        document.getElementById('pnDestinatario').style.height="155px";
        document.getElementById('txtAviso').style.height="95px";

    }
     lblCantCarater.SetText(txtAviso.GetText().toString().length);
}

function desabilitaHabilitaComponentes()
{
    txtAssunto.SetEnabled(TipoOperacao != "Consultar");
    dteInicio.readOnly = (TipoOperacao == "Consultar");
    dteTermino.readOnly = (TipoOperacao == "Consultar");
    ddlTipoDestinatario.SetEnabled(TipoOperacao != "Consultar");
    txtAviso.SetEnabled(TipoOperacao != "Consultar");
    lblCantCarater.SetVisible(TipoOperacao != "Consultar");
    lblDe2000.SetVisible(TipoOperacao != "Consultar");
}

//=========  dos list box ==============
    function SelectOption() 
    {
       MoveSelectedItem(lbDisponiveis, lbSelecionados);
       ordenarItems();
    }
    function SelectAllOptions() 
    {
        MoveItems(lbDisponiveis, lbSelecionados);
        ordenarItems();
    }
    function UnselectOption() 
    {
        MoveSelectedItem(lbSelecionados, lbDisponiveis);
        ordenarItems();
    }
    function UnselectAllOptions() 
    {
        MoveItems(lbSelecionados, lbDisponiveis);
        ordenarItems();
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
          btnADDTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbDisponiveis.GetItemCount() > 0);
          btnADD.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbDisponiveis.GetSelectedItem() != null);
          btnRMV.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbSelecionados.GetSelectedItem() != null);
          btnRMVTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbSelecionados.GetItemCount() > 0);
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
     
     function ordenarItems()
     {
          var listaSelecionadosTXT = new Array();
          var listaSelecionadosVAL = new Array();
          
          var listaDisponiveisTXT = new Array();
          var listaDisponiveisVAL = new Array();
          
          for(var i = 0; i < lbSelecionados.GetItemCount() ; i++)
          {
                 var item = lbSelecionados.GetItem(i);
                 listaSelecionadosTXT[i] = item.text;
          }
          for(var i = 0; i < lbDisponiveis.GetItemCount() ; i++)
          {
                 var item = lbDisponiveis.GetItem(i);
                 listaDisponiveisTXT[i] = item.text;
          }
          if (listaSelecionadosTXT)
          {
                listaSelecionadosTXT.sort();
                insereVetorOrdenado(listaSelecionadosTXT,lbSelecionados);
          }
          if (listaDisponiveisTXT)
          {
                listaDisponiveisTXT.sort();
                insereVetorOrdenado(listaDisponiveisTXT,lbDisponiveis);
          }

     }
     
     function buscaValorObjListBox(valor,objListBox)
     {
        var itemCount = objListBox.GetItemCount();
        var item1 = null;
        var retorno = "";
        for (var i = 0; i < itemCount; i++) 
        {
            item1 = objListBox.GetItem(i);
            if(item1.text == valor)
            {
                retorno = item1.value;
            }
        }
        return retorno;
         
     }
     
     function ajustaLarguras()
     {
        if(isIE8)
        {
            var objListboxDisp = document.getElementById("lbDisponiveis_D");               
            var objListboxSelec = document.getElementById("lbSelecionados_D");
            if(objListboxDisp != null)
                objListboxDisp.style.width = "295px";
            if(objListboxSelec != null)
                objListboxSelec.style.width = "276px";
        }
     }
     
     function insereVetorOrdenado(vetorArrayTXT, objListBox)
     {
        
        for(var i=0;i<vetorArrayTXT.length;i++)
        {
            /*a chave é o texto e o valor e o codigo*/
            hfOrdena.Set(vetorArrayTXT[i],buscaValorObjListBox(vetorArrayTXT[i],objListBox));
        }

        objListBox.BeginUpdate();
        var itemCount = objListBox.GetItemCount();
        for (var i = 0; i < itemCount; i++) 
        {
             objListBox.RemoveItem(0);
        }
         
        for (var i = 0; i < vetorArrayTXT.length; i++)
        {
             /*o texto mostrado é o vetor ordenado e os codigos sao buscados apartir dos textos*/
             objListBox.AddItem(vetorArrayTXT[i], hfOrdena.Get(vetorArrayTXT[i]));
        }
        objListBox.EndUpdate();
        hfOrdena.Clear();
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

    function posSalvarComSucesso()
    {
        mostraPopupMensagemGravacao(traducao.CadastroAvisos_aviso_gravado_com_sucesso_);
    }

    //-------
    function mostraPopupMensagemGravacao(acao)
    {
        lblAcaoGravacao.SetText(acao);
        pcMensagemGravacao.Show();
        setTimeout ('fechaTelaEdicao();', 1500);
    }

    function fechaTelaEdicao()
    {
        pcMensagemGravacao.Hide();
        onClick_btnCancelar()
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
    else
        lblCantCarater.SetText(text.length);
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}
