var codigoObjeto = '';
var iniciaisAssociacao = '';
var nomeSitio = '';
var idPaginaAtual = '';

function abreFotosSitio() {
    window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/GaleriaFotos.aspx?NumeroFotos=999&CR=' + iniciaisAssociacao, "Últimas Fotos - " + nomeSitio, 565, 490, "", null);
}

function abreSequenciaEvolutiva() {
    window.top.showModal(window.top.pcModal.cp_Path + 'espacoTrabalho/galeriaFotos.aspx?CR=' + iniciaisAssociacao, "Últimas Fotos - " + nomeSitio, 900, 550, "", null);
}

function abreCurvaSSitio() {
    var codigoArea = -1;
    var nomeArea = '';

    if (ddlArea.GetVisible()) {
        codigoArea = ddlArea.GetValue();
        nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
    }
    else if (ddlArea2.GetVisible()) {
        codigoArea = ddlArea2.GetValue();
        nomeArea = ddlArea2.GetValue() == "-1" ? "" : ddlArea2.GetText();
    }

    var tituloGrafico = nomeSitio == "" ? traducao.PainelGerenciamento_curva_s : traducao.PainelGerenciamento_curva_s___ + nomeSitio + "(" + nomeArea + ")";
    window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/vm_010.aspx?Altura=430&Largura=930&CR=' + iniciaisAssociacao + '&CA=' + codigoArea, tituloGrafico, 980, 490, "", null);
}

function abreGaugeSitio() {
    var codigoArea = -1;
    var nomeArea = '';

    if (ddlArea.GetVisible()) {
        codigoArea = ddlArea.GetValue();
        nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
    }
    else if (ddlArea2.GetVisible()) {
        codigoArea = ddlArea2.GetValue();
        nomeArea = ddlArea2.GetValue() == "-1" ? "" : ddlArea2.GetText();
    }

    var tituloGrafico = nomeSitio == "" ? traducao.PainelGerenciamento_desempenho_econ_mico : traducao.PainelGerenciamento_desempenho_econ_mico___ + nomeSitio + "(" + nomeArea + ")";
    window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/vm_019.aspx?Altura=430&Largura=930&CR=' + iniciaisAssociacao + '&CA=' + codigoArea, tituloGrafico, 980, 490, "", null);
}

function selecionaMenu(indexMenu) { 
    setTimeout('abreVisao' + indexMenu + '();', 10);
}

function defineAjuda(texto) {
    imgAjuda.SetVisible(false);
}

function abreVisao0() {
    if (hfPermissoes.Get("GestaoEmpreendimento") == "S") {
        ddlArea2.SetVisible(true);
        ddlArea.SetVisible(false);
        var nomeArea = ddlArea2.GetValue() == "-1" ? "" : ddlArea2.GetText();
        imgAnterior.SetVisible(false);
        imgProximo.SetVisible(false);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        imgSequenciaEvolutiva.SetVisible(false);
        verificaLabelImagensVisivel();
        document.getElementById('tdArea').style.display = '';
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoPresidencia.aspx?CA=' + ddlArea2.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao1() {
    if (hfPermissoes.Get("EAP") == "S") {
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(false);
        imgProximo.SetVisible(false);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        imgSequenciaEvolutiva.SetVisible(false);
        verificaLabelImagensVisivel();
        //window.top.mudaLogoLD('./imagens/ne/UHE_01.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/painelGerencial.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao2() {
    if (hfPermissoes.Get("VisaoGlobal") == "S") {
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(true);
        imgProximo.SetVisible(true);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        imgSequenciaEvolutiva.SetVisible(false);
        verificaLabelImagensVisivel();
        //window.top.mudaLogoLD('./imagens/ne/UHE_01.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_00.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao3() {
    if (hfPermissoes.Get("PainelDesempenho") == "S") {
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(true);
        imgProximo.SetVisible(true);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        imgSequenciaEvolutiva.SetVisible(false);
        verificaLabelImagensVisivel();
        //window.top.mudaLogoLD('./imagens/ne/UHE_01.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_01.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao4() {
    if (hfPermissoes.Get("VisaoCusto") == "S") {
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(true);
        imgProximo.SetVisible(true);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        imgSequenciaEvolutiva.SetVisible(false);
        verificaLabelImagensVisivel();
        //window.top.mudaLogoLD('./imagens/ne/UHE_08.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_08.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao5() {
    if (hfPermissoes.Get("Pimental") == "S") {
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(true);
        imgProximo.SetVisible(true);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        verificaImagemEvolucaPimentalVisivel();
        verificaLabelImagensVisivel();
        //window.top.mudaLogoLD('./imagens/ne/UHE_02.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_02.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao6() {
    if (hfPermissoes.Get("BeloMonte") == "S") {
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(true);
        imgProximo.SetVisible(true);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        verificaImagemEvolucaoBeloMonteVisivel();
        verificaLabelImagensVisivel();
        //window.top.mudaLogoLD('./imagens/ne/UHE_03.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_03.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao7() {
    if (hfPermissoes.Get("Infra") == "S") {
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(true);
        imgProximo.SetVisible(true);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        imgSequenciaEvolutiva.SetVisible(false);
        verificaLabelImagensVisivel();
        //window.top.mudaLogoLD('./imagens/ne/UHE_04.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_04.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao8() {
    if (hfPermissoes.Get("Derivacao") == "S") {
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(true);
        imgProximo.SetVisible(true);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(imgFotosEscavacao.cp_MostraImagemEscavacao == 'S');
        imgSequenciaEvolutiva.SetVisible(false);
        verificaLabelImagensVisivel();    
        //window.top.mudaLogoLD('./imagens/ne/UHE_05.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_05.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
    }
}

function abreVisao9() {
    if (hfPermissoes.Get("Reservatorios") == "S") {
        ddlArea2.SetVisible(false);
        ddlArea.SetVisible(true);
        var nomeArea = ddlArea.GetValue() == "-1" ? traducao.PainelGerenciamento_obras_civis_e_fornecimento_e_montagem : ddlArea.GetText();
        document.getElementById('tdArea').style.display = '';
        imgAnterior.SetVisible(true);
        imgProximo.SetVisible(true);
        imgAjuda.SetVisible(false);
        imgFotosEscavacao.SetVisible(false);
        imgSequenciaEvolutiva.SetVisible(false);
        verificaLabelImagensVisivel();
        //window.top.mudaLogoLD('./imagens/ne/UHE_06.png');
        window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_06.aspx?CA=' + ddlArea.GetValue() + "&NA=" + nomeArea, 'frmVC');
        
    }
}

function verificaLabelImagensVisivel() {
    lblImagens.SetVisible(imgFotosEscavacao.GetVisible() || imgFotos.GetVisible() || imgSequenciaEvolutiva.GetVisible());
}

function verificaImagemEscavacaoVisivel() {
    imgFotosEscavacao.SetVisible(imgFotosEscavacao.cp_MostraImagemEscavacao == 'S');
}

function verificaImagemEvolucaPimentalVisivel() {
    imgSequenciaEvolutiva.SetVisible(imgSequenciaEvolutiva.cp_MostraImagemEvolucaoPimental == 'S');
}

function verificaImagemEvolucaoBeloMonteVisivel() {
    imgSequenciaEvolutiva.SetVisible(imgSequenciaEvolutiva.cp_MostraImagemEvolucaoBeloMonte == 'S');
}

function getPaginaPorId(indexSomatorio) {
    if (hfPermissoes.Get('Urls') == '')
        return;

    var indexPaginaAtual;
    var indexPaginaDestino;

    var urlsPaginas = hfPermissoes.Get('Urls').split(';');

    for (var i = 0; i < urlsPaginas.length; i++) {
        if (urlsPaginas[i] == idPaginaAtual) {
            indexPaginaAtual = i;
            break;
        }
    }

    indexPaginaDestino = indexPaginaAtual + indexSomatorio;

    if (indexPaginaDestino >= 0 && indexPaginaDestino < urlsPaginas.length && urlsPaginas[indexPaginaDestino] != '')
        selecionaMenu(urlsPaginas[indexPaginaDestino]);
}