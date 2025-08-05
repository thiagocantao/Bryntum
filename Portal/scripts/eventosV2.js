var propriedadesVisivel = false; // informa se a janela de propriedades está ou não aberta
var qtdeObjetivos = 0 ; // usada para identificar e contar os objetivos
var divSelecionada = null; // registra qual a div está selecinada "visao/missao/perspectiva/objetivo/tema" 
var objetivoSelecionado = null;
var objetoSelecionado = null;
var perspectivaAtual = null;
var novoObjetivoTopo = 0;     // é utiliza ao criar um novo objetivo
var novoObjetivoEsquerda = 0; // é utiliza ao criar um novo objetivo
var qtdeObjeto = 0 ; // usada para nomear os objetos

function seleciona(obj)
{
    divSelecionada = obj;
    deseleciona(document.getElementById("Conteudo"));
    deseleciona(document.getElementById("ConteudoPerspectiva1"));
    deseleciona(document.getElementById("ConteudoPerspectiva2"));
    deseleciona(document.getElementById("ConteudoPerspectiva3"));
    deseleciona(document.getElementById("ConteudoPerspectiva4"));
    var tdx = document.getElementById(obj.id);
    tdx.style.border = "#ef0 1px solid";
}

function deseleciona(objeto)
{
    objetoSelecionado = null;
    if (objeto != null)
    {
        var acrescimo = 0;
        var filhos = objeto.childNodes;
           
        for (i=0;i<filhos.length;i++)
        {
            if (filhos[i].nodeType == 1)
                filhos[i].style.border = "none";
        }
    }
}    

function perspectivaSelecionada()
{
    var perspectivaAtual = divSelecionada;
    if (perspectivaAtual!=null)
    {
        var idAtual = perspectivaAtual.id.substring(0,1);
        if (idAtual != "P")
            perspectivaAtual = null;
    }
    if (perspectivaAtual==null)
        window.top.mostraMensagem(traducao.eventosV2_selecione_uma_perspectiva, 'Atencao', true, false, null);

    return perspectivaAtual
}

function novoObjetivoTema(tipo)
{
    // busca a perspectiva selecionada
    perspectivaAtual = perspectivaSelecionada();
    
    // se não existir perspectiva selecionada... sai.
    if (perspectivaAtual==null)
        return;
    
    var largura = 200;
    var altura = 20;
    if (tipo=="T")
    {
        bordasArrendondadas = false;
        altura = 20;
    }
    else
    {
        bordasArrendondadas = true;
        altura = 60;
    }
    var corPadrao = "#cecece";
    if (tipo=="O")
        corPadrao = "#ffe0a1";
    else if (tipo=="T")
        corPadrao = "#f3ff85";
        
    // insere um objeto (objetivo/Tema) na perspectiva selecionada
    var novoObjeto = getObjeto(tipo, perspectivaAtual, bordasArrendondadas, 0, 0, largura, altura );
    mudaCor( novoObjeto.id, corPadrao );
    
    // se o objeto criado foi um objetivo, vamos inserir um sinalizado
    if (tipo=="O")
    {
        var esquerda = largura - 27;
        var topo     = 3;
    
        var nomeObjeto = "Sem" + novoObjeto.id;//.substring(3);
        var imagem = document.createElement("img");
        imagem.setAttribute('id', nomeObjeto);
        imagem.className    = "moveable";
        imagem.style.left   = esquerda + "px";
        imagem.style.top    = topo     + "px";
        imagem.src="../../imagens/verde.gif";
        novoObjeto.appendChild(imagem);
    }
}

function removeElemento(elemento)
{
   
    var tecla = (event.which) ? event.which : event.keyCode;
    if (tecla==46)
    {
        // o elemente não pode ser removido fisicamente, apenas ficará oculto.
        elemento.style.cursor = "default";
        elemento.style.display="none";

        // Se fosse para remover o elemento definitivamente do cliente, usaria as linhas abaixo
        //var pai = elemento.parentNode;
        //pai.removeChild(elemento);
    }
}

function getObjeto(tipoObjeto, objetoPai, comBordasArredondadas, esquerda, topo, largura, altura)
{
    var nomeObjeto = "";
    
    var conteudoObjetoPai = document.getElementById("Conteudo");
    var alturaMinima = "30px";
    if (tipoObjeto=="M")
        nomeObjeto = "Missao";
    else if (tipoObjeto=="V")
        nomeObjeto = "Visao";
    else if (tipoObjeto=="P")
        nomeObjeto = "Perspectiva"+(qtdeObjeto++);
    else if (tipoObjeto=="O")
    {
        nomeObjeto = "Obj" + objetoPai.id + "_" + (qtdeObjeto++);
        altura -= 8;
    }
    else if (tipoObjeto=="T")
    {
        nomeObjeto = "Tema" + objetoPai.id + "_" + (qtdeObjeto++);
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
    divObjeto.style.minHeight = alturaMinima;
    divObjeto.onclick    = function() { objetoSelecionado = this; seleciona(this); };
    divObjeto.onkeydown  = function() { removeElemento(this); };
    divObjeto.ondblclick = function() { abrirPropriedade(this, tipoObjeto); };
    
    var b1s = null; var b2s = null; var b3s = null; var b4s = null;
    var b1i = null; var b2i = null; var b3i = null; var b4i = null;

    if (comBordasArredondadas)
    {
        b1s = document.createElement("b"); b1s.className = "b1";
        b2s = document.createElement("b"); b2s.className = "b2";
        b3s = document.createElement("b"); b3s.className = "b3";
        b4s = document.createElement("b"); b4s.className = "b4";

        b1i = document.createElement("b"); b1i.className = "b1";
        b2i = document.createElement("b"); b2i.className = "b2";
        b3i = document.createElement("b"); b3i.className = "b3";
        b4i = document.createElement("b"); b4i.className = "b4";
    }  
      
    var tableObjeto = document.createElement("TABLE");
    tableObjeto.setAttribute('id', "tb"+nomeObjeto);
    tableObjeto.setAttribute('cellpadding', 0);
    tableObjeto.setAttribute('cellspacing', 0);
    (comBordasArredondadas)? tableObjeto.className = "conteudo" : tableObjeto.className = "conteudoBordaQuad";
    tableObjeto.boder=2;
    tableObjeto.style.width ="100%";
    tableObjeto.style.height = altura + "px";
    
    var tbody = document.createElement("tbody");
    
    // Titulo do objeto
    // ---------------------------------------------
    var trTitulo = document.createElement("TR"); 
    var tdTitulo = document.createElement("TD"); 
    tdTitulo.setAttribute('id', 'Titulo'+nomeObjeto);
    tdTitulo.valign = "top";
    
    if (tipoObjeto=="T")
    {
        var textoTitulo = document.createTextNode("[Tema]");
        tdTitulo.appendChild(textoTitulo);
    }
        
    trTitulo.appendChild(tdTitulo);
    tbody.appendChild(trTitulo);
    
    // Tema não tem conteúdo, todos os outros sim.
    if (tipoObjeto != "T")
    {
        // Conteudo do objeto
        // ---------------------------------------------    
        var trConteudo = document.createElement("TR"); 
        var tdConteudo = document.createElement("TD"); 
        tdConteudo.setAttribute('id', 'Conteudo'+nomeObjeto);
        tdConteudo.align = "center";
        var spanTexto = document.createElement("SPAN"); 
        spanTexto.setAttribute('id', 'Texto'+nomeObjeto);
        var textoConteudo = document.createTextNode("[Descrição]");
        spanTexto.appendChild(textoConteudo);
        tdConteudo.appendChild(spanTexto);
        trConteudo.appendChild(tdConteudo);
        tbody.appendChild(trConteudo);
    }

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
    conteudoObjetoPai.innerHtml;
    return divObjeto;
}

// janela de propriedades
function aplicarPropriedades() 
{
    var txtTitulo   = document.getElementById("txtTitulo"  ).value;
    var txtTexto    = document.getElementById("txtTexto"   ).value;
    var txtAltura   = document.getElementById("txtAltura"  ).value;
    var txtLargura  = document.getElementById("txtLargura" ).value;
    var txtTopo     = document.getElementById("txtTopo"    ).value;
    var txtEsquerda = document.getElementById("txtEsquerda").value;
    var txtCor      = document.getElementById("txtCor"     ).value;
    var nomeObjeto  = document.getElementById("txtID"      ).value;

    document.getElementById("Titulo"+nomeObjeto).innerHTML = txtTitulo;
    document.getElementById("tb"+nomeObjeto).style.height = txtAltura + "px";
    document.getElementById(nomeObjeto).style.height = txtAltura + "px";
    document.getElementById(nomeObjeto).style.width = txtLargura + "px";
    document.getElementById(nomeObjeto).style.top = txtTopo + "px";
    document.getElementById(nomeObjeto).style.left = txtEsquerda + "px";     
    
    if (document.getElementById("Texto"+nomeObjeto)!=null)
        document.getElementById("Texto"+nomeObjeto).innerHTML = txtTexto;
    
    mudaCor(nomeObjeto, txtCor);   
 }

// janela de propriedades
function fecharPropriedades(salvar) 
{
    var fundo1 = document.getElementById("fundo1");
    fundo2.style.visibility = "hidden";
    fundo1.style.display = "none";
    if (salvar)
    {
        aplicarPropriedades();
    }
    // registra que a janela foi fechada
    propriedadesVisivel = false; 
 }
 
 
function abrirPropriedade(objeto, tipo)
{ 
    //Se a janela já está aberta, encerra esta chamada ao procedimento
    if (propriedadesVisivel)
        return;
        
    novoObjetivoTopo     = 0;
    novoObjetivoEsquerda = 0;
    
    var btnInserirObjetivo = document.getElementById('btnInserirObjetivo');
    var btnInserirTema     = document.getElementById('btnInserirTema');
    btnInserirObjetivo.style.display="none";
    btnInserirTema.style.display="none";
    
    // tipo do objeto que abriu a janela de propriedades
    var txtTipoObjeto = document.getElementById("txtTipoObjeto");

    // linha da propriedade Titulo    
    var tr_titulo = document.getElementById("tr_Titulo");
    // linha da propriedade Texto
    var tr_texto = document.getElementById("tr_Texto");

    // Propriedades configuráveis
    var txtTitulo   = document.getElementById("txtTitulo"  );
    var txtTexto    = document.getElementById("txtTexto"   );
    var txtAltura   = document.getElementById("txtAltura"  );
    var txtLargura  = document.getElementById("txtLargura" );
    var txtTopo     = document.getElementById("txtTopo"    );
    var txtEsquerda = document.getElementById("txtEsquerda");
    var txtCor      = document.getElementById("txtCor"     );
    var txtID       = document.getElementById("txtID"      );
    
    if (tipo == 'V')
        txtTipoObjeto.value = "Visão";
    else if (tipo == 'M')
        txtTipoObjeto.value = "Missão";
    else if (tipo == 'P')
    {
        txtTipoObjeto.value = "Perspectiva";
        btnInserirObjetivo.style.display="";
        btnInserirTema.style.display="";
    }
    else if (tipo == 'O')
        txtTipoObjeto.value = "Objetivo";
    else if (tipo == 'T')
        txtTipoObjeto.value = "Tema";

    // dados do objeto
    var nomeObjeto = objeto.id;
    var _titulo = document.getElementById("Titulo"+nomeObjeto).innerHTML;
    // tema não tem texto
    var _texto = (tipo != 'T')?document.getElementById("Texto"+nomeObjeto).innerHTML:"";
    var _altura = document.getElementById("tb"+nomeObjeto).offsetHeight;
    var _largura = objeto.offsetWidth;
    var _topo = objeto.offsetTop;
    var _esquerda = objeto.offsetLeft;
    var _cor = document.getElementById("tb"+nomeObjeto).style.background;

    (tipo == "O")?tr_titulo.style.display="none":tr_titulo.style.display="";
    (tipo == "T")?tr_texto.style.display="none":tr_texto.style.display="";

    txtTitulo.value   = _titulo;
    txtTexto.value    = _texto;
    txtAltura.value   = _altura;
    txtLargura.value  = _largura;
    txtTopo.value     = _topo;
    txtEsquerda.value = _esquerda;
    txtCor.value      = _cor;
    txtCor.style.background = _cor;
    txtID.value       = nomeObjeto;

    var fundo1 = document.getElementById("fundo1");
    fundo1.style.top = "0px";
    fundo1.style.left = "0px";
    fundo1.style.width = document.body.clientWidth;
    fundo1.style.height = document.body.clientHeight;
    fundo1.style.display = "block";
    var fundo2 = document.getElementById("fundo2");
    fundo2.style.top = (document.body.clientHeight/2)- (fundo2.offsetHeight/2);
    fundo2.style.left = (document.body.clientWidth/2)-(fundo2.offsetWidth/2);
    fundo2.style.visibility = "visible";
    
    // registra que a janela foi aberta
    propriedadesVisivel = true; 
 }

function mudaCor(nomeObjeto, novaCor)
{
    var objeto = document.getElementById(nomeObjeto);
    x=objeto.childNodes;
    for (i=0;i<x.length;i++)
    {
        if(x[i].className != "b1")
            x[i].style.background = novaCor; 
    } 
}