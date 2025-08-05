function atualizaGrid() {
    gvDados.Refresh();
}
function excluirRegistroIntegracao(codigoRegistro) {
    var funcObj = {
        funcaoClickOK: function () {
            //alert('clicou em ok');
            lpLoading.Show();
            callbackExcluiRegistro.PerformCallback(codigoRegistro);
        }
    }
    var funcObj1 = {
        funcaoClickCancelar: function () {
            //alert('clicou em cancelar');
        }
    }
    window.top.mostraConfirmacao('Essa operação não poderá ser desfeita. Confirma a exclusão desse registro?', function () { funcObj['funcaoClickOK']() }, function () { funcObj1['funcaoClickCancelar']() });


    
}

function incluirDado() {
    var sUrl = window.top.pcModal.cp_Path + 'administracao/DadoWebService_popup.aspx?C=-1';
    window.top.showModal(sUrl, 'Detalhes - Dados WebService', 800, null, atualizaGrid, null);


}

function atualizaDadoIntegracao(IniciaisDadoWebService) {

    var funcObj = {
        funcaoClickOK: function () {
            //alert('clicou em ok');
            lpLoading.Show();
            callbackAtualizaDadoIntegracao.PerformCallback(IniciaisDadoWebService);
        }
    }
    var funcObj1 = {
        funcaoClickCancelar: function () {
            //alert('clicou em cancelar');
        }
    }
    window.top.mostraConfirmacao('Confirma a atualização dos dados dessa integração?', function () { funcObj['funcaoClickOK']() }, function () { funcObj1['funcaoClickCancelar']() });

}