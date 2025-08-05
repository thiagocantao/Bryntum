<%@ Page Language="C#" AutoEventWireup="true" CodeFile="galeriaFotos.aspx.cs" Inherits="espacoTrabalho_galeriaFotos" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>
    <link rel="Stylesheet" href="imagens/galeriaImagens.css" />
    <script type="text/javascript" src="imagens/galeriaImagens.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="tempoTransicao"  value="<%=tempoTransicao %>" />
    <div id="palco">
        <div id="galeriaImagens">
                <%=todasImagens%>
        </div>
        <div id="topoGaleria" class="menuGaleria">
            <div id="conteudoTopoGaleria">
            </div>
        </div>
        <div id="direitaGaleria" class="menuGaleria">
            <img alt="Próxima imagem" title="Próxima imagem" src="imagens/avancar.png" /></div>
        <div id="esquerdaGaleria" class="menuGaleria">
            <img alt="Imagem anterior" title="Imagem anterior" src="imagens/voltar.png" /></div>
        <div id="rodapeGaleria" class="menuGaleria">
            <div id="conteudoRodapeGaleria">
            </div>
        </div>
        <div id="centroGaleria" class="menuGaleria">
            <img alt="Pausar" title="Pausar apresentação" src="imagens/pause.png" /></div>
    </div>
    </form>
</body>
</html>
