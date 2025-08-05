
function executaAberturaFluxo() {
    callbackAberturaFluxo.PerformCallback();
}

var myObject = null;
var posExecutar = null;
var urlModal = "";
var cancelaFechamentoPopUp = 'N';
var linkImg = "";
var retornoModal = null;
var refreshinterval = 540;
var starttime;
var nowtime;
var reloadseconds = 0;
var secondssinceloaded = 0;

var posExecutar2 = null;
var urlModal2 = "";
var cancelaFechamentoPopUp2 = 'N';
var retornoModal2 = null;

function starttime() {
    starttime = new Date();
    starttime = starttime.getTime();
    countdown();
}

function countdown() {
    nowtime = new Date();
    nowtime = nowtime.getTime();
    secondssinceloaded = (nowtime - starttime) / 1000;
    reloadseconds = Math.round(refreshinterval - secondssinceloaded);
    if (refreshinterval >= secondssinceloaded) {
        var timer = setTimeout("countdown()", 10);

    }
    else {
        clearTimeout(timer);
        callbackSession.PerformCallback();
    }
}
window.onload = starttime;

function encerraSessao() {

}

function inicializaSessao() {

}

function replaceAll(origem, antigo, novo) {
    var teste = 0;
    while (teste == 0) {
        if (origem.indexOf(antigo) >= 0) {
            origem = origem.replace(antigo, novo);
        }
        else
            teste = 1;
    }
    return origem;
}

var eventoOKMsg = null;
var eventoCancelarMsg = null;

function mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) {
    if (!(timeout)) {
        timeout = 1500;
    }
    lpAguardeMasterPage.Hide();
    if (nomeImagem != null && nomeImagem != '')
        imgApresentacaoAcao.SetImageUrl(pcModal.cp_Path + 'imagens/' + nomeImagem + '.png');
    else
        imgApresentacaoAcao.SetVisible(false);

    textoMsg = replaceAll(textoMsg, '\n', '<br/>');

    lblMensagemApresentacaoAcao.SetText(textoMsg);
    btnOkApresentacaoAcao.SetVisible(mostraBtnOK);
    btnCancelarApresentacaoAcao.SetVisible(mostraBtnCancelar);
    pcApresentacaoAcao.Show();
    eventoOKMsg = eventoOK;
    eventoCancelarMsg = null;

    if (!mostraBtnOK && !mostraBtnCancelar) {
        setTimeout('fechaMensagem();', timeout);
    }
}

function mostraConfirmacao(textoMsg, eventoOK, eventoCancelar) {

    imgApresentacaoAcao.SetImageUrl(pcModal.cp_Path + 'imagens/confirmacao.png');

    textoMsg = replaceAll(textoMsg, '\n', '<br/>');

    lblMensagemApresentacaoAcao.SetText(textoMsg);
    btnOkApresentacaoAcao.SetVisible(true);
    btnCancelarApresentacaoAcao.SetVisible(true);
    pcApresentacaoAcao.Show();
    eventoOKMsg = eventoOK;
    eventoCancelarMsg = eventoCancelar;
}

function fechaMensagem() {
    lblMensagemApresentacaoAcao.SetText('');

    pcApresentacaoAcao.AdjustSize();
    pcApresentacaoAcao.Hide();
}

function getPathUrl() {
    return pcModal.cp_Path;
}

function gotoURL(url, target) {
    if (pcModal.cp_Path != null && pcModal.cp_Path != "" && pcModal.cp_Path != 'undefined') {
        var fakeLink = document.createElement("a");

        fakeLink.target = target;

        if (typeof (fakeLink.click) == 'undefined')
            location.href = pcModal.cp_Path + url; // sends referrer in FF, not in IE 
        else {
            fakeLink.href = pcModal.cp_Path + url;
            document.body.appendChild(fakeLink);
            fakeLink.click(); // click() method defined in IE only 
        }
    }

}


function atualizaTela() {
    window.location.reload();
}

function mostraSobre(caminho) {
    showModal(caminho, "Sobre", 532, 330, "", null);
}

function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
    if (parseInt(sHeight) < 535)
        sHeight = parseInt(sHeight) + 20;
    sWidth = sWidth <= 400 ? 900 : sWidth;

    myObject = objParam;
    posExecutar = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

    pcModal.SetWidth(sWidth);
    pcModal.SetWidth(sWidth);
    pcModal.SetHeight(sHeight + 50);
    pcModal.SetContentUrl(sUrl);
    //setTimeout ('alteraUrlModal();', 0);            
    pcModal.SetHeaderText(sHeaderTitulo);
    pcModal.Show();

}

function atualizaURLModal(sUrl) {
    urlModal = sUrl;
    pcModal.SetContentUrl(sUrl);
}

function fechaModal() {
    pcModal.Hide();
}

function fechaModalStatusReport() {
    pcStatusReportCarteira.Hide();
}

function resetaModal() {
    posExecutar = null;
    pcModal.SetContentUrl(pcModal.cp_Path + "branco.htm");
    pcModal.SetHeaderText("");
    retornoModal = null;
}


function mudaLogoLD(urlLogo) {
    if (urlLogo != '') {
        linkImg = "S";
        imgLogoUnidade.mainElement.src = pcModal.cp_Path + urlLogo;
        imgLogoUnidade.SetVisible(true);
    } else {
        document.getElementById('tdImgLD').style.cursor = '';
        imgLogoUnidade.mainElement.title = '';
        linkImg = "N";
        imgLogoUnidade.SetVisible(false);
        imgLogoUnidade.mainElement.src = '';

    }
}

function eventoClickImagem(s, e) {
    if (linkImg == "S") {
        if (e.htmlEvent.offsetY > 7 && e.htmlEvent.offsetY < 14 && e.htmlEvent.offsetX > 68 && e.htmlEvent.offsetX < 88) {
            if (window.abreVisao1) {
                abreVisao1();
            }
        }
        else if (e.htmlEvent.offsetY > 40 && e.htmlEvent.offsetY < 50 && e.htmlEvent.offsetX > 15 && e.htmlEvent.offsetX < 35) {
            if (window.abreVisao5) {
                abreVisao5();
            }
        }
        else if (e.htmlEvent.offsetY > 40 && e.htmlEvent.offsetY < 50 && e.htmlEvent.offsetX > 43 && e.htmlEvent.offsetX < 61) {
            if (window.abreVisao6) {
                abreVisao6();
            }
        }
        else if (e.htmlEvent.offsetY > 40 && e.htmlEvent.offsetY < 50 && e.htmlEvent.offsetX > 68 && e.htmlEvent.offsetX < 88) {
            if (window.abreVisao7) {
                abreVisao7();
            }
        }
        else if (e.htmlEvent.offsetY > 40 && e.htmlEvent.offsetY < 50 && e.htmlEvent.offsetX > 94 && e.htmlEvent.offsetX < 114) {
            if (window.abreVisao8) {
                abreVisao8();
            }
        }
        else if (e.htmlEvent.offsetY > 40 && e.htmlEvent.offsetY < 50 && e.htmlEvent.offsetX > 120 && e.htmlEvent.offsetX < 140) {
            if (window.abreVisao9) {
                abreVisao9();
            }
        }
    }
}

function moveMouseImagem(event, obj) {
    if (linkImg == "S") {
        if (event.offsetY > 7 && event.offsetY < 14 && event.offsetX > 68 && event.offsetX < 88 && hfPermissoes.Get("EAP") == 'S') {
            obj.style.cursor = "pointer";
            imgLogoUnidade.mainElement.title = 'UHE Belo Monte';
        }
        else if (event.offsetY > 40 && event.offsetY < 50 && event.offsetX > 15 && event.offsetX < 35 && hfPermissoes.Get("Pimental") == 'S') {
            obj.style.cursor = "pointer";
            imgLogoUnidade.mainElement.title = 'Sítio Pimental';
        }
        else if (event.offsetY > 40 && event.offsetY < 50 && event.offsetX > 43 && event.offsetX < 61 && hfPermissoes.Get("BeloMonte") == 'S') {
            obj.style.cursor = "pointer";
            imgLogoUnidade.mainElement.title = 'Sítio Belo Monte';
        }
        else if (event.offsetY > 40 && event.offsetY < 50 && event.offsetX > 68 && event.offsetX < 88 && hfPermissoes.Get("Infra") == 'S') {
            obj.style.cursor = "pointer";
            imgLogoUnidade.mainElement.title = 'Sítio Infraestrutura';
        }
        else if (event.offsetY > 40 && event.offsetY < 50 && event.offsetX > 94 && event.offsetX < 114 && hfPermissoes.Get("Derivacao") == 'S') {
            obj.style.cursor = "pointer";
            imgLogoUnidade.mainElement.title = 'Sítio Canais de Derivação, Transposição e Enchimento';
        }
        else if (event.offsetY > 40 && event.offsetY < 50 && event.offsetX > 120 && event.offsetX < 140 && hfPermissoes.Get("Reservatorios") == 'S') {
            obj.style.cursor = "pointer";
            imgLogoUnidade.mainElement.title = 'Sítio Diques';
        }
        else {
            obj.style.cursor = "";
            imgLogoUnidade.mainElement.title = '';
        }
    }
    else {
        obj.style.cursor = "";
        imgLogoUnidade.mainElement.title = '';
    }
}


function showModal2(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal) {
    if (parseInt(sHeight) < 535)
        sHeight = parseInt(sHeight) + 20;
    sWidth = sWidth <= 400 ? 900 : sWidth;
    posExecutar2 = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

    pcModal2.SetWidth(sWidth);
    pcModal2.SetHeight(sHeight);
    pcModal2.SetContentUrl(sUrl);
    //setTimeout ('alteraUrlModal();', 0);            
    pcModal2.SetHeaderText(sHeaderTitulo);
    pcModal2.Show();

}

function fechaModal2() {
    //pcModal2.SetContentUrl(pcModal.cp_Path + "branco.htm");
    pcModal2.Hide();
}

function resetaModal2() {

    posExecutar2 = null;
    pcModal2.SetContentUrl(pcModal.cp_Path + "branco.htm");
    pcModal2.SetHeaderText("");
    retornoModal2 = null;
}
var painelAtual;
function showModal3(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {

    if (pcModal.GetVisible()) {
        showModal2(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal);
        painelAtual = pcModal2;
    }
    else {
        showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam);
        painelAtual = pcModal;
    }

}

function fechaModal3() {
    //painelAtual.SetContentUrl(pcModal.cp_Path + "branco.htm");
    painelAtual.Hide();
}


function atualizaGrid() {
    gvDados.PerformCallback();
}