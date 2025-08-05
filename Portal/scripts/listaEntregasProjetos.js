// ---- Provavelmente não será necessário alterar as duas próximas funções
var teste;
function SalvarCamposFormulario() {
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";

    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario() {

    // Função responsável por preparar os campos do formulário para receber um novo registro
    dteData.SetValue(null);
    txtIncluidaPor.SetText("");
    txtTipo.SetText("");
    txtAssunto.SetText("");
    txtProjeto.SetText("");
    txtLicao.SetText("");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
//    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
//        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoProjeto;NomeTarefa;TerminoLB;TerminoReal;Situacao', MontaCamposFormulario);
}

function MontaCamposFormulario(values) {
    //    if (window.TipoOperacao &&  TipoOperacao == "Incluir")
    //        return;

    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente

    LimpaCamposFormulario();



    if (values[1] == null)
        dteData.SetText("");
    else
        dteData.SetValue(values[1]);

    txtIncluidaPor.SetText(values[6] != null ? values[6].toString() : "");

    txtTipo.SetText(values[2] != null ? values[2].toString() : "");

    txtAssunto.SetText(values[3] != null ? values[3].toString() : "");

    txtProjeto.SetText(values[4] != null ? values[4].toString() : "");

    txtLicao.SetText(values[5] != null ? values[5].toString() : "");
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    /*
    if (TipoOperacao == "Editar")
    {
    btnRelatorioRisco.SetClientVisible(true);
    window.top.mostraMensagem("As informações foram salvas com sucesso.", 'Atencao', false, false, null);
        
    }
    */
}


function relListaEntregasProjetos(codigoProjeto, where) {
    var strUrl = "../Relatorios/popupRelListaEntregasProjetos.aspx?";
    strUrl += "&CP=" + codigoProjeto;
    var now = new Date();
    var dia = now.getDate();
    var mes = now.getMonth();
    var horas = now.getHours();
    var minutes = now.getMinutes();

    if (dia > 0 && dia < 10) {
        dia = '0' + dia;
    }
    if (mes > 0 && mes < 10) {
        mes = '0' + mes;
    }
    if (horas > 0 && horas < 10) {
        horas = '0' + horas;
    }
    if (minutes > 0 && minutes < 10) {
        minutes = '0' + minutes;
    }

    var dataFormatada = dia + "/" + mes + "/" + now.getFullYear() + " " + horas + ":" + minutes;
    strUrl += "&DT=" + dataFormatada;
    strUrl += "&WH=" + where;
    window.top.showModal(strUrl, traducao.listaEntregasProjetos_lista_de_entregas, screen.width - 60, screen.height - 250, '', null);
}
