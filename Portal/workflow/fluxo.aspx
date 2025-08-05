<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fluxo.aspx.cs" Inherits="workflow_fluxo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="Stylesheet" href="../estilos/fluxo.css" type="text/css" />
    <link rel="Stylesheet" href="../estilos/jquery.ui.ultima.css" type="text/css" />
    <link rel="Stylesheet" href="../estilos/jfPlugins.css" type="text/css" />
    <script type="text/javascript" language="javascript" src="../scripts/jquery.ultima.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/Fluxo/jfPlugins.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/jquery.timer.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/Fluxo/funcoesBanco.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/Fluxo/objetos.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/Fluxo/padrao.js"></script>
    <title>.::Fluxo::.</title>
</head>
<body>
<div id="painelBotao" class="janelaDialogo" title="Opções do objeto"></div>
<div id="painelBotaoEdicao" class="janelaDialogo" title="Adição/Edição"></div>
    <form id="form1" runat="server" >
    <div id="area" class="padroes">
        
        <div id="menu" class="padroes padroesBorda padroesSombra">
            <div id="menuDireito" class="padroes">
                <img id="menuSalvar" alt="Salvar fluxo atual" title="Salva o fluxo atual" class="botaoMenuTopo" src="../imagens/Fluxo/salvar.png" />
                <img id="menuPublicar" alt="Publicar fluxo atual" title="Publicar fluxo atual" class="botaoMenuTopo" src="../imagens/Fluxo/publicar.png" />
            </div>
        </div>
        <div class="divisor"></div>
        <div id="palco" class="padroes padroesBorda padroesSombra">
            <div id="conteudo" class="padroes padroesBorda"></div>
            <div id="menuAbrirPainel">
                <img id="menuPainel" alt="Abrir/Fechar propriedades do objeto selecionado" title="Abrir/Fechar propriedades do objeto selecionado" class="botaoMenu" src="../imagens/Fluxo/voltar.png" />
            </div>
            <div id="painel" class="padroes padroesBorda painel"></div>
            <div id="menuObjeto" class="padroes padroesBorda padroesSombra" >
                <img id="menuAdicionar" alt="Adicionar novo objeto" title="Adicionar objeto" src="../imagens/Fluxo/adicionar.gif" />
                <img id="menuAlterar" alt="Alterar as propriedades do objeto" title="Alterar propriedades" src="../imagens/Fluxo/editar.gif" />
                <img id="menuConector" alt="Alterar a origem e destino do objeto" title="Alterar origem/destino" src="../imagens/Fluxo/origemDestino.gif" />
                <img id="menuExcluir" alt="Excluir objeto" title="Excluir objeto" src="../imagens/Fluxo/excluir.gif" />
            </div>
            <div id="menuEsquerdo" class="padroes padroesBorda padroesSombra">
                <img id="menuAddEtapa" alt="Adicionar etapa" title="Adicionar Etapa" class="botaoMenu" src="../imagens/Fluxo/etapaNormal.png" />
                <img id="menuAddTempo" alt="Adicionar tempo" title="Adicionar Tempo" class="botaoMenu" src="../imagens/Fluxo/tempo.png" />
                <img id="menuAddConector" alt="Adicionar conector" title="Adicionar Conector" class="botaoMenu" src="../imagens/Fluxo/conexao.png" />
                <img id="menuAddJuncao" alt="Adicionar junção" title="Adicionar Junção" class="botaoMenu" src="../imagens/Fluxo/juncoesNovo.png" />
                <img id="menuAddFim" alt="Adicionar fim" title="Adicionar Fim" class="botaoMenu" src="../imagens/Fluxo/fim.png" />
            </div>
            <div id="menuPalco">
                <img id="menuLente" alt="Lente de aumento" title="Lente de aumento" class="botaoMenu" src="../imagens/Fluxo/zoom.png" />
                <img id="menuVisao" alt="Mudar visão" title="Mudar visão" class="botaoMenu" src="../imagens/Fluxo/telaInteira.png" />
            </div>
        </div>
        
    </div>
    
    </form>
    
</body>
</html>
