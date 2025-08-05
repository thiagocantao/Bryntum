var ddlAtual;
var myObjectLocal = null;
var posExecutarLocal = null;
var urlModalLocal = "";
var retornoModalLocal = null;


function abreTela(url, titulo, largura, altura) {
    window.top.showModal2(url + '&PND=S', titulo, largura, altura, atualizaTela, null);
}

function atualizaTela(param) {
    if (param == 'S') {
        if(ddlAtual)
            ddlAtual.cp_PossuiPendencias = 'N';
        ASPxClientEdit.ValidateGroup('MKE', true);
    }
}

function atualizaGrid(param) {    
        gvDados.PerformCallback();
}

function grid_ContextMenu(s, e) {
    if (e.objectType == "header")
        pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
}

function resizeText(incremento) {
    if (document.body.style.fontSize == "") {
        document.body.style.fontSize = "8pt";
    }
    document.body.style.fontSize = parseFloat(document.body.style.fontSize) + incremento + "pt";

    var componentes = ASPxClientControl.GetControlCollection().elements;

    for (i = 0; i < componentes.length; i++)
        componentes.mainElement.style.fontSize = parseFloat(componentes.mainElement.style.fontSize) + incremento + "pt";
}
function onClick(s, e) {
    var codigos = '';
    var qtdSelecionados = gvDados.GetSelectedKeysOnPage().length;

    if (qtdSelecionados == 0) {
        window.top.mostraMensagem(traducao.PendenciasWorkflow_nenhum_registro_foi_selecionado_, 'Atencao', true, false, null);
    }else {
        
        for (var i = 0; i < qtdSelecionados; i++) {
            var chaves = gvDados.GetSelectedKeysOnPage()[i].split('|');
            var cod = chaves[5];
            if (cod == null || cod == undefined)
                cod = -1;
            codigos += cod.toString();
            if (i + 1 < qtdSelecionados)
                codigos += ',';
        }

        window.top.showModal(window.top.pcModal.cp_Path + '_CertificadoDigital/assinaturaMultiplosFluxos.aspx?codigos=' + codigos, 'Assinar Ofícios Selecionados', 600, 300, funcaoPosModal, null);
    }
}

function funcaoPosModal(x) {
    if (x == 'OK') {
        window.top.mostraMensagem(traducao.PendenciasWorkflow_assinatura_realizada_com_sucesso_, 'sucesso', false, false, null);
        gvDados.PerformCallback();
    }
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 15;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}