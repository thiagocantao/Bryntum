var timedOutMsg;
var tipoEdicao;
var marcacaoAlta = 'N';
var marcacaoBaixa = 'N';

var MailDemo = {

    PendingCallbacks: {},

    DoCallback: function (sender, callback) {
        if (sender.InCallback()) {
            MailDemo.PendingCallbacks[sender.name] = callback;
            sender.EndCallback.RemoveHandler(MailDemo.DoEndCallback);
            sender.EndCallback.AddHandler(MailDemo.DoEndCallback);
        } else {
            callback();
        }
    },

    DoEndCallback: function (s, e) {
        var pendingCallback = MailDemo.PendingCallbacks[s.name];
        if (pendingCallback) {
            pendingCallback();
            delete MailDemo.PendingCallbacks[s.name];
        }
    },

    ClientMailEditor_Init: function (s, e) {
        window.setTimeout(function () { s.Focus(); }, 0);
        ClientMailSendButton.SetEnabled(true);
    },

    ClientMailSendButton_Click: function (s, e) {
        if (trim(txtDestinatarios.GetText()) == '') {
            window.top.mostraMensagem(traducao.EnvioMensagens_nenhum_destinat_rio_foi_selecionado_, 'erro', true, false, null);
            return;
        }

        callbackSalvar.PerformCallback(tipoEdicao);
    }
};

function adicionaEmail(valor) {
    var email = valor[0];

    txtUsuarios.SetText(txtUsuarios.GetText() + email + '; ');
}

function removeEmail(valor) {
    var email = valor[0];

    txtUsuarios.SetText(txtUsuarios.GetText().replace(email + '; ', ''));
}

function verificaMarcacaoAlta() {
    if (marcacaoAlta == 'S') {
        btnAltaPrioridade.SetChecked(false);
        marcacaoAlta = 'N';
    }
    else {
        marcacaoAlta = 'S';
        marcacaoBaixa = 'N';
    }
}

function verificaMarcacaoBaixa() {
    if (marcacaoBaixa == 'S') {
        btnBaixaPrioridade.SetChecked(false);
        marcacaoBaixa = 'N';
    }
    else {
        marcacaoBaixa = 'S';
        marcacaoAlta = 'N';
    }
}

function abreDestinatariosPopUp() {
    showModal('ListaUsuariosMensagem.aspx?Popup=S', traducao.EnvioMensagens_selecionar_destinat_rios, 820, 460, "", null);
}

function selecionaTexto() {
    if (txtDestinatarios.GetText() != '') {
        var textoMemo = txtDestinatarios.GetText();
        var elemento = txtDestinatarios.GetInputElement();

        var indexInicio = _aspxGetSelectionInfo(elemento).startPos;
        var indexFim = _aspxGetSelectionInfo(elemento).endPos;
        var indexNovoInicio = 0;
        var indexNovoFim = textoMemo.length - 1;
        var i = 0;
        for (i = indexInicio; i >= 0; i--) {
            if (textoMemo.charAt(i) == ';') {
                indexNovoInicio = i + 1;
                break;
            }
        }

        for (i = indexFim; i < textoMemo.length; i++) {
            if (textoMemo.charAt(i) == ';') {
                indexNovoFim = i + 1;
                break;
            }
        }

        txtDestinatarios.SetSelection(indexNovoInicio, indexNovoFim, true);;
    }
}

function trim(str) {
return str.replace(/^\s+|\s+$/g,"");
}

