// JScript File
// --- Desenho do mapa estrategico.
// ---------------------------------------
var orgCursor=null;   // The original Cursor (mouse) Style so we can restore it
var dragOK=false;     // True if we're allowed to move the element under mouse
var dragXoffset=0;    // How much we've moved the element on the horozontal
var dragYoffset=0;    // How much we've moved the element on the verticle
var objetoPai = null  // Registra o objeto container do que está sendo movimentado.
var idObjetoSelecionado = -1; // Indica o ID correspondente ao objeto "idObjetoJS" utilizado no hfGeral

function dragHandler(e)
{
    var htype='-moz-grabbing';
    if (e == null) { e = window.event; htype='move';} 
    var target = e.target != null ? e.target : e.srcElement;
    // se o evento partiu de um textbox, saia sem fazer nada
    if (target.type == "text" )
    {
        // se não tem objeto selecionado, avisa o usuário para selecionar um
        if (selObj==null)
            window.top.mostraMensagem(traducao.drag_selecione_um_objeto_na_janela_da_esquerda_, 'Atencao', true, false, null);
        return 
    }

    if (target.className.substring(0,4)!="move")
        target = getElementMove(target);

    if (target == null)
        return;

    selObj=target;//objetoSelecionado;
    
    // verifica se o objeto selecionado (objetivo/tema) tem restrição de movimento
    objetoPai = getObjetoPai(target);
    
    orgCursor=target.style.cursor;
    if (target.className.substring(0,4)=="move" || target.className=="moveableTO") 
    {
        // se for Titulo/Conteudo do objetivo, tem que pegar a Div Externa.
        if (target.className=="moveableTO")
        {
            target = target.parentNode; // A div externa é o pai das outras duas
            selObj = target;
        }
        target.style.cursor=htype;
        
        dragOK=true;
        dragXoffset=e.clientX-parseInt(target.style.left);
        dragYoffset=e.clientY-parseInt(target.style.top);
        document.onmousemove=moveHandler;
        document.onmouseup=cleanup;
        //return false;
        ajustaCaixaPropriedadesObjetoSelecionado();
    }
}

function moveHandler(e)
{
    if (e == null) { e = window.event } 
    if (e.button<=1&&dragOK)
    {
        // se o objeto selecionado tem pai, tem que limitar o movimento na área do pai.
        if (objetoPai != null)
        {
            limiteDireito = objetoPai.offsetWidth - 4;
            limiteInferior = objetoPai.offsetHeight - 8;
        }
        else
        {
            limiteDireito = screen.availWidth ;
            limiteInferior = screen.availHeight;
        }
        
        selObj.style.left=e.clientX-dragXoffset+'px';
        selObj.style.top=e.clientY-dragYoffset+'px';
        
        if ( e.clientX-dragXoffset <0 )
            selObj.style.left = "0px";
        
        if ( e.clientY-dragYoffset <0 )
            selObj.style.top = "0px";  
            
        if ( e.clientX-dragXoffset + selObj.offsetWidth > limiteDireito)
            selObj.style.left = (limiteDireito - selObj.offsetWidth) + "px";   
            
        if ( e.clientY-dragYoffset + selObj.offsetHeight > limiteInferior)
            selObj.style.top = (limiteInferior - selObj.offsetHeight) + "px";   
            
        return false;
    }
}

function cleanup(e) 
{
    document.onmousemove=null;
    document.onmouseup=null;
    selObj.style.cursor=orgCursor;
    dragOK=false;
    objetoPai = null;
    ajustaCaixaPropriedadesObjetoSelecionado();
}

function getElementMove(startIn)
{
    var elemento = startIn;
    var controle = 1;
    var classe = "";
    while (elemento!=null)
    {
        if (elemento.className != null)
            classe = elemento.className.substring(0,4);
        if (classe=="move")
            return elemento;
        elemento = elemento.parentNode; 
        if (controle>15)
        {
            
            break;
        }
        controle ++;
    }
}

function getObjetoPai(filho)
{
    // só tem pai os objetos "Objetivo, Tema e Semaforo";
    if (filho.id.substring(0,3) == "Obj" || filho.id.substring(0,4) == "Tema" || filho.id.substring(0,3) == "Sem" )
    {
        var nomePai = (filho.id.substring(0,3) == "Obj" || filho.id.substring(0,3) == "Sem" )?filho.id.substring(3):filho.id.substring(4);
        if (filho.id.substring(0,3) != "Sem" )
            nomePai = nomePai.substring(0, nomePai.indexOf("_"));
        var pai = document.getElementById(nomePai);
        return pai;
    }
    else
        return null;
}

// --------------------------------------- EVENTOS ---------------------
function selecionaObjeto(obj)
{
    idObjetoSelecionado = retiraSelecaoTodosObjetos();
    obj.style.border = "#ef0 1px solid";
}

function retiraSelecaoTodosObjetos()
{
    var maxID = hfGeral.Get("maxID"); 
    var idRetorno = -1;
    for (id = 0; id < maxID; id++)
    {
        if (hfGeral.Contains("o" + id + "_n"))
        {
            var objeto = hfGeral.Get("o" + id + "_n");
            document.getElementById(objeto).style.border = "none";
            
            // se o objeto atual é igual ao objeto selecionado
            if (objeto == selObj.id)
                idRetorno = id;
        }
    }
    return idRetorno;
}

function ajustaCaixaPropriedadesObjetoSelecionado()
{
    if ( selObj==null || selObj.id == "")
        return;
        
    var nomeObjeto = selObj.id;
    var tituloObjeto = "";
    var textoObjeto = "";
    var corFundoObjeto = "";
    var corFonteObjeto = "";
    var corBordaObjeto = "";
    var esquerdaObjeto = 0;
    var superiorObjeto = 0;
    var larguraObjeto  = 0;
    var alturaObjeto   = 0;
    
    if ( nomeObjeto.substr(0,3)== "Img")
    {
        esquerdaObjeto = selObj.offsetLeft - 1;
        superiorObjeto = selObj.offsetTop - 1;
        larguraObjeto = selObj.offsetWidth;
        alturaObjeto = selObj.offsetHeight;
    }
    else
    {
        if (document.getElementById("Titulo"+nomeObjeto) != null)
            tituloObjeto = document.getElementById("Titulo"+nomeObjeto).innerHTML;

        // tema não tem conteúdo
       // if (nomeObjeto.substr(0,4) != "Tema")
            textoObjeto = document.getElementById("Texto"+nomeObjeto).innerHTML;
            
        corFundoObjeto = document.getElementById("tb"+nomeObjeto).style.background;
        corFonteObjeto = document.getElementById("tb"+nomeObjeto).style.color;
        corBordaObjeto = document.getElementById("tb"+nomeObjeto).style.borderColor;
        esquerdaObjeto = selObj.offsetLeft - 1;
        superiorObjeto = selObj.offsetTop - 1;
        larguraObjeto  = selObj.offsetWidth;
        alturaObjeto   = document.getElementById("tb"+nomeObjeto).offsetHeight;
    }
    
    txtObjeto.SetText(tituloObjeto);    
    txtTexto.SetText(textoObjeto);    
    txtCorFundo.SetText(corFundoObjeto);        
    txtCorFonte.SetText(corFonteObjeto);        
    txtCorBorda.SetText(corBordaObjeto);        
    txtEsquerda.SetText(esquerdaObjeto);
    txtSuperior.SetText(superiorObjeto);
    txtLargura.SetText(larguraObjeto);
    txtAltura.SetText(alturaObjeto);
}

function ajustaPropriedadeObjetoSelecionado(valor, tipo)
{
    if ( selObj==null || selObj.id == "")
        return;
        
    var nomeObjeto = selObj.id;
    var tipoObj = nomeObjeto.substr(0,3);
    
    if ("ESLA".indexOf(tipo) >=0 && valor != parseInt(valor))
    {
        window.top.mostraMensagem(traducao.drag_o_valor_informado_n_o___um_n_mero_v_lido, 'erro', true, false, null);
        return;
    }

    
    if (tipo == "T")
        document.getElementById("Texto"+nomeObjeto).innerHTML = valor;
    else if (tipo == "E")
    {
        if (valor < 2)
            valor = 2;
        else if (valor > 700)
            valor = 700;
            
        txtEsquerda.SetText(valor);
        document.getElementById(nomeObjeto).style.left = valor+"px";
    }
    else if (tipo == "S")
    {
        if (valor < 2)
            valor = 2;
        else if (valor > 640)
            valor = 640;
            
        txtSuperior.SetText(valor);
        document.getElementById(nomeObjeto).style.top = valor+"px";        
    }
    else if (tipo == "L")
    {
        // apenas para imagens
        if (tipoObj == "Img")
        {
            if (valor < 10)
                valor = 10;
            else if (valor > 400)
                valor = 400;
        }
        else
        {
            if (valor < 20)
                valor = 20;
            else if (valor > 780)
                valor = 780;
         }   
        txtLargura.SetText(valor);
        document.getElementById(nomeObjeto).style.width = (valor -2 )+"px";        
    }
    else if (tipo == "A")
    {
        if (tipoObj != "Tem" && tipoObj != "Img")
        {
            if (valor < 20)
                valor = 20;
            else if (valor > 300)
                valor = 300;
        }   
        // apenas para temas
        else if (tipoObj == "Tem")
        {
            if (valor < 20)
                valor = 20;
            else if (valor > 80)
                valor = 80;
        }
        // apenas para imagens
        else if (tipoObj == "Img")
        {
            if (valor < 10)
                valor = 10;
            else if (valor > 400)
                valor = 400;
        }
        
        txtAltura.SetText(valor);
        if (tipoObj != "Img")
            document.getElementById("tb"+nomeObjeto).style.height = valor+"px";
        else
            document.getElementById(nomeObjeto).style.height = valor+"px";     
    }
    else if (tipo == "CFu"  && tipoObj != "Img") // Cor do Fundo
    {
        if (tipoObj != "Tem") // tema não tem bordas arredondadas
        {
            // borda superior
            document.getElementById("b2s"+nomeObjeto).style.background = valor; 
            document.getElementById("b3s"+nomeObjeto).style.background = valor; 
            document.getElementById("b4s"+nomeObjeto).style.background = valor; 

            // borda inferior
            document.getElementById("b2i"+nomeObjeto).style.background = valor; 
            document.getElementById("b3i"+nomeObjeto).style.background = valor; 
            document.getElementById("b4i"+nomeObjeto).style.background = valor; 
        }
        document.getElementById("tb"+nomeObjeto).style.background = valor;        
    }    
    else if (tipo == "CFo"  && tipoObj != "Img") // Cor da fonte
    {
        document.getElementById("tb"+nomeObjeto).style.color = valor;        
    }    
    else if (tipo == "CBo"  && tipoObj != "Img") // Cor da borda
    {
        if (tipoObj != "Tem") // tema não tem bordas arredondadas
        {
            // borda superior
            document.getElementById("b1s"+nomeObjeto).style.background = valor; 
            document.getElementById("b2s"+nomeObjeto).style.borderColor = valor; 
            document.getElementById("b3s"+nomeObjeto).style.borderColor = valor; 
            document.getElementById("b4s"+nomeObjeto).style.borderColor = valor; 

            // borda inferior
            document.getElementById("b1i"+nomeObjeto).style.background = valor; 
            document.getElementById("b2i"+nomeObjeto).style.borderColor = valor; 
            document.getElementById("b3i"+nomeObjeto).style.borderColor = valor; 
            document.getElementById("b4i"+nomeObjeto).style.borderColor = valor; 
        }        
        // bordas laterais
        document.getElementById("tb"+nomeObjeto).style.borderColor = valor;  
    }           
}

// Retorna o ID correspondente ao objeto "idObjetoJS" utilizado no hfGeral
function getIdObjeto(nomeObjeto)
{
    var maxID = hfGeral.Get("maxID"); 
    var idRetorno = -1;
    for (id = 0; id < maxID; id++)
    {
        if (hfGeral.Contains("o" + id + "_n"))
        {
            var objeto = hfGeral.Get("o" + id + "_n");
            
            // se o objeto atual é igual ao objeto selecionado
            if (objeto == nomeObjeto)
            {
                idRetorno = id;
                break;
            }
        }
    }
    return idRetorno;
}

function removeElemento(origem)
{
    var tecla = (event.which) ? event.which : event.keyCode;
    if (tecla==46 || origem == 'botaoDel') // DEL
    {
        if ( selObj==null || selObj.id == "")
            return;

        // os unicos objetos que podem ser excluídos são: Objetivos, temas e imagens
        var nomeObjeto = selObj.id;
        var tipoObj = nomeObjeto.substr(0,3);
        if (tipoObj != 'Obj' && tipoObj != 'Tem' && tipoObj != 'Img')
            return;
    

        var idObjetoJS = getIdObjeto(selObj.id);
        // marca que o objeto foi excluido
        hfGeral.Set("o" + idObjetoJS + "_x", "S");
        
        // o elemente não pode ser removido fisicamente, apenas ficará oculto.
        selObj.style.cursor = "default";
        selObj.style.display="none";

        // Se fosse para remover o elemento definitivamente do cliente, usaria as linhas abaixo
        //var pai = selObj.parentNode;
        //pai.removeChild(selObj);
    }
}

function insereObjeto(tipo)
{
    if (tipo=='P')
    {
        // ultimo objeto inserido
        var idObjetoJS = hfGeral.Get("maxID");
        var corPadrao = "#FFFFFF";
        var novoObjeto = criarObjetivoTema(idObjetoJS, tipo, null, true, 0, 0, 500, 150, corPadrao );
        hfGeral.Add("o" + idObjetoJS + "_c", "");
        hfGeral.Add("o" + idObjetoJS + "_n", novoObjeto.id);
        hfGeral.Set("maxID", ++idObjetoJS);
    }
    else if (tipo=='O' || tipo == 'T')
    {
        var perspectiva = getPerspectivaSelecionada();
        // se for objetivo ou tema, tem que ter perspectiva selecionada
        if (perspectiva== null)
        {
            window.top.mostraMensagem(traducao.drag_selecione_uma_perspectiva_na_janela_da_esquerda_, 'Atencao', true, false, null);
            return;
        }
        
        // insere um objeto (objetivo/Tema) na perspectiva selecionada
        var largura = 200;
        var altura = 20;
        var corPadrao = "#cecece";
        if (tipo=="T")
        {
            bordasArrendondadas = false;
            altura = 20;
            corPadrao = "#f3ff85";
        }
        else if (tipo=="O")
        {
            bordasArrendondadas = true;
            altura = 60;
            corPadrao = "#ffe0a1";
        }
        // busca o codigo da perspectiva pai
        var codigoObjetoEstrategiaSuperior = hfGeral.Get(perspectiva.id);
        
        // ultimo objeto inserido
        var idObjetoJS = hfGeral.Get("maxID");
        
        var novoObjeto = criarObjetivoTema(idObjetoJS, tipo, perspectiva, bordasArrendondadas, 10, 50, largura, altura, corPadrao );
        hfGeral.Add("o" + idObjetoJS + "_c", "");
        hfGeral.Add("o" + idObjetoJS + "_n", novoObjeto.id);
        hfGeral.Add("o" + idObjetoJS + "_p", codigoObjetoEstrategiaSuperior);
        hfGeral.Set("maxID", ++idObjetoJS);
    }
    else
    {
        // ultimo objeto inserido
        var idObjetoJS = hfGeral.Get("maxID");
        
        var novoObjeto = criarSeta(idObjetoJS, tipo, 100, 100, 100, 20);
        hfGeral.Add("o" + idObjetoJS + "_c", "");
        hfGeral.Add("o" + idObjetoJS + "_n", novoObjeto.id);
        hfGeral.Set("maxID", ++idObjetoJS);
    }
}

function getPerspectivaSelecionada()
{
    // se não tem objeto selecionado
    if (selObj==null)
        return null;
    
    // verifica se o objeto é perspectiva
    var idAtual = selObj.id.substring(0,1);
    if (idAtual != "P")
        return null;

    return selObj
}
  
function criarSeta(sequencial, tipoObjeto, esquerda, topo, largura, altura)
{
    var nomeObjeto = "";
    nomeObjeto = "Img" + tipoObjeto + "_" + (sequencial);
    var conteudoObjetoPai = document.getElementById("dvDesktop");
     var alturaMinima = "30px";
     
    // criar o objeto Imagem
    var imgObjeto = document.createElement("img");
    imgObjeto.setAttribute('id', nomeObjeto);
    imgObjeto.className    = "moveable bordabox";
    //imgObjeto.style.minHeight = alturaMinima;
    imgObjeto.onclick    = function() { selecionaObjeto(this); };
    
    if (tipoObjeto == "SC")
    {
        imgObjeto.src = "../../imagens/wizardMapaEstrategico/SetaCima.png";
    }
    else if (tipoObjeto == "SB")
    {
         imgObjeto.src = "../../imagens/wizardMapaEstrategico/SetaBaixo.png";
    }
    else if (tipoObjeto == "SE")
    {
         imgObjeto.src = "../../imagens/wizardMapaEstrategico/SetaEsq.png";
    }
    else if (tipoObjeto == "SD")
    {
         imgObjeto.src = "../../imagens/wizardMapaEstrategico/SetaDir.png";
    }
    else if (tipoObjeto == "SFC")
    {
        largura = 10;
        altura = 30;
        imgObjeto.src = "../../imagens/wizardMapaEstrategico/SetaFinaCima.png";
    }
    else if (tipoObjeto == "SFB")
    {
        largura = 10;
        altura = 30;
        imgObjeto.src = "../../imagens/wizardMapaEstrategico/SetaFinaBaixo.png";
    }
    else if (tipoObjeto == "SFE")
    {
         largura = 30;
         altura = 10;
         imgObjeto.src = "../../imagens/wizardMapaEstrategico/SetaFinaEsq.png";
    }
    else if (tipoObjeto == "SFD")
    {
         largura = 30;
         altura = 10;
         imgObjeto.src = "../../imagens/wizardMapaEstrategico/SetaFinaDir.png";
    }
      
    imgObjeto.style.left   = esquerda + "px";
    imgObjeto.style.top    = topo     + "px";
    imgObjeto.style.width  = largura  + "px";
    imgObjeto.style.height = altura + "px";
         
    conteudoObjetoPai.appendChild(imgObjeto);
    return imgObjeto;
}  

function criarObjetivoTema(sequencial, tipoObjeto, objetoPai, comBordasArredondadas, esquerda, topo, largura, altura, backgroundColor)
{
    var nomeObjeto = "";
    
    var conteudoObjetoPai;
    var alturaMinima = "30px";
    if (tipoObjeto=="P")
    {
        conteudoObjetoPai = document.getElementById("dvDesktop");
        nomeObjeto = "Psp" + (sequencial);
        altura -= 8;
    }
    else if (tipoObjeto=="O")
    {
        nomeObjeto = "Obj" + objetoPai.id + "_" + (sequencial);
        altura -= 8;
    }
    else if (tipoObjeto=="T")
    {
        nomeObjeto = "Tema" + objetoPai.id + "_" + (sequencial);
        alturaMinima = "15px";
    }
        
    topo += novoObjetivoTopo;
    esquerda += novoObjetivoEsquerda;
    
    // os objetivos e os temas são criados sempre dentro das perspectivas
    if (tipoObjeto=="O" || tipoObjeto=="T" )
    {
        conteudoObjetoPai = document.getElementById("Conteudo"+objetoPai.id);
        novoObjetivoTopo += 10;
        novoObjetivoEsquerda += 10;
    }
        
    var divObjeto = document.createElement("div");
    divObjeto.setAttribute('id', nomeObjeto);
    divObjeto.className    = "moveable bordabox";
    divObjeto.style.left   = esquerda + "px";
    divObjeto.style.top    = topo     + "px";
    divObjeto.style.width  = largura  + "px";
    divObjeto.style.height = altura + "px";
    divObjeto.style.zIndex = 1000;
    divObjeto.style.minHeight = alturaMinima;
    divObjeto.onclick    = function() { selecionaObjeto(this); };
    
    var b1s = null; var b2s = null; var b3s = null; var b4s = null;
    var b1i = null; var b2i = null; var b3i = null; var b4i = null;

    if (comBordasArredondadas)
    {
        b1s = document.createElement("b"); b1s.className = "b1"; b1s.id = "b1s"+nomeObjeto;
        b2s = document.createElement("b"); b2s.className = "b2"; b2s.id = "b2s"+nomeObjeto;
        b3s = document.createElement("b"); b3s.className = "b3"; b3s.id = "b3s"+nomeObjeto;
        b4s = document.createElement("b"); b4s.className = "b4"; b4s.id = "b4s"+nomeObjeto;

        b1i = document.createElement("b"); b1i.className = "b1"; b1i.id = "b1i"+nomeObjeto;
        b2i = document.createElement("b"); b2i.className = "b2"; b2i.id = "b2i"+nomeObjeto;
        b3i = document.createElement("b"); b3i.className = "b3"; b3i.id = "b3i"+nomeObjeto;
        b4i = document.createElement("b"); b4i.className = "b4"; b4i.id = "b4i"+nomeObjeto;
    }  
      
    var tableObjeto = document.createElement("TABLE");
    tableObjeto.setAttribute('id', "tb"+nomeObjeto);
    tableObjeto.setAttribute('cellpadding', 0);
    tableObjeto.setAttribute('cellspacing', 0);
    (comBordasArredondadas)? tableObjeto.className = "conteudo" : tableObjeto.className = "conteudoBordaQuad";
    tableObjeto.boder=2;
    tableObjeto.style.width ="100%";
    tableObjeto.style.height = altura + "px";
    tableObjeto.style.background = backgroundColor;//"#cecece";
    tableObjeto.style.borderColor = "#999999";
    tableObjeto.style.color = "#000000";
    
    var tbody = document.createElement("tbody");
    
    // Titulo do objeto
    // ---------------------------------------------
    var trTitulo = document.createElement("TR"); 
    var tdTitulo = document.createElement("TD"); 
    tdTitulo.setAttribute('id', 'Titulo'+nomeObjeto);
    tdTitulo.valign = "top";
    
    trTitulo.appendChild(tdTitulo);
    tbody.appendChild(trTitulo);
    
    // Conteudo do objeto
    // ---------------------------------------------    
    var trConteudo = document.createElement("TR"); 
    var tdConteudo = document.createElement("TD"); 
    tdConteudo.setAttribute('id', 'Conteudo'+nomeObjeto);
    tdConteudo.align = "center";
    var spanTexto = document.createElement("SPAN"); 
    spanTexto.setAttribute('id', 'Texto'+nomeObjeto);
    var textoConteudo = document.createTextNode("");
    if (tipoObjeto == "O")
        textoConteudo = document.createTextNode("[Descrição do Objetivo]");
    else if (tipoObjeto == "T")
        textoConteudo = document.createTextNode("[Tema]");
        
    spanTexto.appendChild(textoConteudo);
    tdConteudo.appendChild(spanTexto);
    trConteudo.appendChild(tdConteudo);
    tbody.appendChild(trConteudo);

    tableObjeto.appendChild(tbody);

    
    if (comBordasArredondadas)
    {
        divObjeto.appendChild(b1s);
        divObjeto.appendChild(b2s);
        divObjeto.appendChild(b3s);
        divObjeto.appendChild(b4s);
    }
    
    divObjeto.appendChild(tableObjeto);

    if (comBordasArredondadas)
    {
        divObjeto.appendChild(b4i);
        divObjeto.appendChild(b3i);
        divObjeto.appendChild(b2i);
        divObjeto.appendChild(b1i);
    }
    
    conteudoObjetoPai.appendChild(divObjeto);
    //conteudoObjetoPai.innerHtml;
    return divObjeto;
}
    
function salvarMapa()
{
    var maxID = hfGeral.Get("maxID"); 
    for (id = 0; id < maxID; id++)
    {
        if (hfGeral.Contains("o" + id + "_n"))
        {
            var objeto = hfGeral.Get("o" + id + "_n");
            hfGeral.Set("o" + id + "_i", getInformacoesObjeto(objeto));
        }
    }
    
    return true;
}

function getInformacoesObjeto(objeto)
{
    var tituloObjeto = "";
    var textoObjeto = "";

    var corFundoObjeto = "";
    var corBordaObjeto = "";
    var corFonteObjeto = "";
    var esquerdaObjeto = 0;
    var superiorObjeto = 0;
    var larguraObjeto  = 0;
    var alturaObjeto   = 0;

    if (objeto.substr(0,3) == "Img")
    {
        var idTemp = document.getElementById(objeto).id;
        tituloObjeto = idTemp.substr(0, idTemp.indexOf("_"));
        esquerdaObjeto = document.getElementById(objeto).offsetLeft;
        superiorObjeto = document.getElementById(objeto).offsetTop;
        larguraObjeto = document.getElementById(objeto).offsetWidth;
        alturaObjeto = document.getElementById(objeto).offsetHeight;
    }
    else
    {
        if (document.getElementById("Titulo"+objeto) != null)
            tituloObjeto = document.getElementById("Titulo"+objeto).innerHTML;
            
        textoObjeto = document.getElementById("Texto"+objeto).innerHTML;
        corFundoObjeto = document.getElementById("tb"+objeto).style.background;
        corBordaObjeto = document.getElementById("tb"+objeto).style.borderColor;
        corFonteObjeto = document.getElementById("tb"+objeto).style.color;        
        esquerdaObjeto = document.getElementById(objeto).offsetLeft;
        superiorObjeto = document.getElementById(objeto).offsetTop;
        larguraObjeto  = document.getElementById(objeto).offsetWidth;
        alturaObjeto   = document.getElementById("tb"+objeto).offsetHeight;
    }
    var info = tituloObjeto+'¥'+textoObjeto+'¥'+esquerdaObjeto+'¥'+superiorObjeto+'¥'+larguraObjeto+'¥'+alturaObjeto+'¥'+corFundoObjeto+'¥'+corBordaObjeto+'¥'+corFonteObjeto;

    return info;
}

function onEnd_hfCallback()
{
    if (hfGeral.Get("StatusSalvar")=="1")
    {
        window.top.mostraMensagem(traducao.drag_as_informa__es_foram_salvas_, 'sucesso', false, false, null);
    }
    else if (hfGeral.Get("StatusSalvar")=="0")
    {
        window.top.mostraMensagem(hfGeral.Get("ErroSalvar"), 'erro', true, false, null);
    }
}

