var comando;
function abrePopupChaveAcesso(valores) {
    var sHeight = Math.max(0, document.documentElement.clientHeight) - 190;
    var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
    window.top.showModal(window.top.pcModal.cp_Path + "administracao/popupChaveAcesso.aspx?C=" + valores[0] + '&CI=' + valores[1], 'Geração de chave de acesso', 800, 330, executaPosPopUp, null);
    //alert('chamou a função abrePopupChaveAcesso');
}

function abrePopupSelecionarOpcoes(valores) {
    var sHeight = Math.max(0, document.documentElement.clientHeight) - 190;
    var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
    window.top.showModal(window.top.pcModal.cp_Path + "administracao/popupSelecionarOpcoesChaveAcesso.aspx?C=" + valores[0], 'Opções', sWidth, sHeight, executaPosPopUp, null);
    //alert('chamou a função abrePopupSelecionarOpcoes');
}

function credenciarUsuario(codigoUsuario, clientId, marcado) {
    
    if (marcado == true) {
        lpLoading.Show();
        callbackCredencia.PerformCallback(codigoUsuario + '|' + clientId);
    }
    else {

        var funcObj = {
            funcaoClickOK: function () {
                callbackDescredencia.PerformCallback(codigoUsuario);
            }
        }
        var funcObj1 = {
            funcaoClickCancelar: function () {
                var check1 = document.getElementById('check_' + codigoUsuario);
                check1.checked = true;
            }
        }
        window.top.mostraConfirmacao('Este procedimento vai descredenciar o usuário, Confirma?', function () { funcObj['funcaoClickOK']() }, function () { funcObj1['funcaoClickCancelar']() });


        
    }
}
function executaPosPopUp() {
    gvDados.Refresh();
}