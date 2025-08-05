// JScript File
var comando = "";
// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    //debugger
    var tOperacao = ""

    try {// Função responsável por preparar os campos do formulário para receber um novo registro

        //debugger
        txtTitulo.SetText("");
        dtInicio.SetValue(null);
        dtTermino.SetValue(null);
        txtStatus.SetText("");

        ddlProjectOwner.SetValue(null);
        ddlProjectOwner.SetText("");

        ddlPacoteTrabalho.SetValue(null);
        ddlPacoteTrabalho.SetText("");

        txtObjetivos.SetText("");
        txtObjetivos.Validate();
        document.getElementById("tdCopiaRaiasRecursos").style.display = "block";

        desabilitaHabilitaComponentes();
        pnResponsavelScrumMaster.PerformCallback();
        pnPacoteTrabalho.PerformCallback();

    } catch (e) { }
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoIteracao;CodigoProjetoIteracao;Titulo;Inicio;Termino;Status;NomeProjectOwner;CodigoProjectOwner;NomePacoteTrabalho;CodigoPacoteTrabalho;Objetivos;FatorProdutividade;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente

    var CodigoIteracao = (values[0] != null ? values[0] : "");
    var CodigoProjetoIteracao = (values[1] != null ? values[1] : "");
    var Titulo = (values[2] != null ? values[2] : "");
    var Inicio = (values[3] != null ? values[3] : "");
    var Termino = (values[4] != null ? values[4] : "");
    var Status = (values[5] != null ? values[5] : "");
    var NomeProjectOwner = (values[6] != null ? values[6] : "");
    var CodigoProjectOwner = (values[7] != null ? values[7] : "");
    var NomePacoteTrabalho = (values[8] != null ? values[8] : "");
    var CodigoPacoteTrabalho = (values[9] != null ? values[9] : "");
    var Objetivos = (values[10] != null ? values[10] : "");
    //var FatorProdutividade = (values[11] != null ? values[11] : "");

    txtTitulo.SetText(Titulo);
    dtInicio.SetValue(Inicio);
    dtTermino.SetValue(Termino);

    txtStatus.SetText(Status);

    ddlProjectOwner.SetValue(CodigoProjectOwner);
    ddlProjectOwner.SetText(NomeProjectOwner);
    if (CodigoPacoteTrabalho != "") {
        ddlPacoteTrabalho.SetValue(CodigoPacoteTrabalho);
        ddlPacoteTrabalho.SetText(NomePacoteTrabalho);
    }
    else {
        ddlPacoteTrabalho.SetSelectedIndex(0);
        ddlPacoteTrabalho.SetValue(-1);
        //ddlPacoteTrabalho.SetText(NomePacoteTrabalho);
    }
    //callbackPlanoTrabalhoAssociado.PerformCallback(CodigoPacoteTrabalho);
    txtObjetivos.SetText(Objetivos);
    txtObjetivos.Validate();

    //FatorProdutividade = (FatorProdutividade * 100);
    //spnFatorProdutividade.SetValue(FatorProdutividade);
    document.getElementById("tdCopiaRaiasRecursos").style.display = "none";
    desabilitaHabilitaComponentes();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0)
        onClick_btnCancelar();
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = hfGeral.Get("TipoOperacao") == "Editar" || hfGeral.Get("TipoOperacao") == "Incluir" ? true : false;

    txtTitulo.SetEnabled(BoolEnabled);
    dtInicio.SetEnabled(BoolEnabled);
    dtTermino.SetEnabled(BoolEnabled);
    //txtStatus.SetEnabled(BoolEnabled);
    ddlProjectOwner.SetEnabled(BoolEnabled);
    ddlPacoteTrabalho.SetEnabled(BoolEnabled);
    txtObjetivos.SetEnabled(BoolEnabled);
    //spnFatorProdutividade.SetEnabled(BoolEnabled);

}

function verificarDadosPreenchidos() {
    var mensagemError = "";
    var retorno = true;
    var numError = 0;
    if (txtTitulo.GetReadOnly() == true) {
        mensagemError += ++numError + ") Este formulário é somente-leitura!\n";
        retorno = false;
    }
    if (txtTitulo.GetText().toString().trim() == "") {
        mensagemError += ++numError + ") O título do sprint deve ser informado!\n";
        retorno = false;
    }
    if (dtInicio.GetValue() != null && dtTermino.GetValue() != null) {

        var dataInicio = new Date(dtInicio.GetValue());
        var meuDataInicio = (dataInicio.getMonth() + 1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
        dataInicio = Date.parse(meuDataInicio);

        var dataTermino = new Date(dtTermino.GetValue());
        var meuDataTermino = (dataTermino.getMonth() + 1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
        dataTermino = Date.parse(meuDataTermino);

        if (dataInicio > dataTermino) {
            retorno = false;
            mensagemError += ++numError + ") A Data de Início não pode ser maior que a Data de Término!\n";
        }

    }


    if (dtInicio.GetValue() == null && dtTermino.GetValue() == null) {
        retorno = false;
        mensagemError += ++numError + ") A Data de Início e a Data de Término devem ser informadas.\n";
    }
    else if (dtTermino.GetValue() == null) {
        retorno = false;
        mensagemError += ++numError + ") A Data de Término deve ser informada.\n";
    }
    else if (dtInicio.GetValue() == null) {
        retorno = false;
        mensagemError += ++numError + ") A Data de Início deve ser informada.\n";
    }


    if (ddlProjectOwner.GetValue() == null) {
        mensagemError += ++numError + ") O responsável pelo sprint deve ser informado!\n";
        retorno = false;
    }

    if (!retorno)
        window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);

    return retorno;
}

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

function validaData() {
    var retorno = false;
    if (dtInicio.GetValue() != null && dtTermino.GetValue() != null) {

        var dataInicio = new Date(dtInicio.GetValue());
        var meuDataInicio = (dataInicio.getMonth() + 1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
        dataInicio = Date.parse(meuDataInicio);

        var dataTermino = new Date(dtTermino.GetValue());
        var meuDataTermino = (dataTermino.getMonth() + 1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
        dataTermino = Date.parse(meuDataTermino);

        if (dataInicio > dataTermino) {
            window.top.mostraMensagem("A Data de Início não pode ser maior que a Data de Término!\n", 'Atencao', true, false, null);
        }
        else {
            retorno = true;
        }
    }
    else {

        if (dtInicio.GetValue() == null && dtTermino.GetValue() == null) {
            window.top.mostraMensagem("A Data de Início e a Data de Término devem ser informadas.", 'Atencao', true, false, null);
        }
        if (dtTermino.GetValue() == null) {
            window.top.mostraMensagem("A Data de Início deve ser informada.", 'Atencao', true, false, null);
        }
        else if (dtTermino.GetValue() == null) {
            window.top.mostraMensagem("A Data de Término deve ser informada.", 'Atencao', true, false, null);
        }
    }
    return retorno;
}

function abrePopupSelecionarItensBacklog(valores) {

    var CodigoIteracao = valores[0];
    var CodigoProjetoIteracao = valores[1];

    var url = './popupAssociaItensBacklog.aspx?CI=' + CodigoIteracao + '&CPI=' + CodigoProjetoIteracao + '&CP=' + CodigoProjetoIteracao + '&ALT=' + 490;
    //function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
    window.top.showModal(url, "Associar Itens de Backlog", 900, 490, atualizaGrid, null);
}

function abrePopupSprint(valores) {
    var CodigoProjetoIteracao = valores[0];
    var CodigoIteracao = valores[1];

    var url = './popupSprint.aspx?CPI=' + CodigoProjetoIteracao + '&CI=' + CodigoIteracao + '&RO=N&ALT=' + 490;
    //function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
    window.top.showModal(url, "Editar Sprint", 900, 490, atualizaGrid, null);
}

function abrePopupSprintInclusao(codigoProjetoAgil) {
    var url = './popupSprint.aspx?CPA=' + codigoProjetoAgil + '&CI=-1&RO=N&ALT=' + 490;
    //function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
    window.top.showModal(url, "Incluir Sprint", 900, 490, atualizaGrid, null);
}

function abrePopupSprintReadOnly(valores) {
    var CodigoProjetoIteracao = valores[0];
    var CodigoIteracao = valores[1];

    var url = './popupSprint.aspx?CPI=' + CodigoProjetoIteracao + '&CI=' + CodigoIteracao + '&RO=S&ALT=' + 490;
    //function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
    window.top.showModal(url, "Visualizar Sprint", 900, 490, atualizaGrid, null);
}

function atualizaGrid() {
    gvDados.PerformCallback();
}

function funcaoCallbackSalvar() {
    if (verificarDadosPreenchidos()) {
        callbackTela.PerformCallback('salvar');
    }
}

function funcaoCallbackFechar() {
    window.top.fechaModalComFooter();
}