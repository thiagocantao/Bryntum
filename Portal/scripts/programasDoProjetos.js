// JScript File
var parametro = '-1';
var comando;
function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    //var tOperacao = ""
    try {// Função responsável por preparar os campos do formulário para receber um novo registro
        hfGeral.Set("CodigoUnidadeNegocio", "");
        hfGeral.Set("CodigoGerenteProjeto", "");
        hfGeral.Set("CodigoMSProject", "");
        hfGeral.Set("CodigoProjeto", "");

        txtNomePrograma.SetText("");
        ddlGerentePrograma.SetValue(null);
        ddlGerentePrograma.SetText("");
        //ddlUnidade.SetText("");
        ddlUnidadeNegocio.SetValue(null);
        //treeList.SetFocusedNodeKey("");
        parametro = "-1";

        var tipoOperacao = hfGeral.Get("TipoOperacao").toString();
        if(tipoOperacao === "Incluir")
            gvProjetos.PerformCallback(parametro);

    } catch (e) {
        var erro = e;
    }
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (grid.GetFocusedRowIndex() !== -1) {
        if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'NomeProjeto;SiglaUnidadeNegocio;NomeGerente;CodigoProjeto;CodigoMSProject;CodigoGerenteProjeto;CodigoUnidadeNegocio;NomeUnidadeNegocio', MontaCamposFormulario);
        }
    }
}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    //LimpaCamposFormulario();
    var delimitador = "¥";
    try {
        txtNomePrograma.SetText(values[0] !== null ? values[0] : "");
        ddlGerentePrograma.SetValue(values[5] !== null ? values[5] : "");
        (values[2] === null ? ddlGerentePrograma.SetText("") : ddlGerentePrograma.SetText(values[2]));
        //treeList.SetFocusedNodeKey(values[6] != null ? values[6] + '' : "");
        //ddlUnidade.SetText(values[7] != null ? values[7] + '' : "");
        ddlUnidadeNegocio.SetValue(values[6]);
        hfGeral.Set("CodigoGerenteProjeto", (values[5] !== null ? values[5] : ""));
        hfGeral.Set("CodigoMSProject", (values[4] !== null ? values[4] : "-1"));
        hfGeral.Set("CodigoProjeto", (values[3] !== null ? values[3] : "-1"));

        var codigoPrograma = (values[3] !== null ? values[3] : "-1");
        var CodigoUnidadeNegocio = (values[6] !== null ? values[6] : "-1");
        parametro = codigoPrograma; // + delimitador + CodigoUnidadeNegocio;

        hfGeral.Set("hfCodigoUnidadeTree", CodigoUnidadeNegocio);

        gvProjetos.PerformCallback(parametro);

        desabilitaHabilitaComponentes();
    } catch (e) {
        var erro = e;
    }
}

function MontaCallbackPesos(values) {
    gvPesos.PerformCallback(values);
}
// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0)
        onClick_btnCancelar();
}

function verificarDadosPreenchidos() {
    var retorno = true;

    var numAux = 0;
    var mensagem = "";
    
    if (txtNomePrograma.GetText() === "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.programasDoProjetos_o_nome_do_programa_deve_ser_informado_;
    }
    if (ddlGerentePrograma.GetValue() === null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.programasDoProjetos_o_gerente_deve_ser_informado_;
    }
    if (ddlUnidadeNegocio.GetValue() === null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.programasDoProjetos_a_unidade_deve_ser_informada_;
    }
    if (mensagem !== "") {
        retorno = false;
        window.top.mostraMensagem(mensagem, 'Atencao', true, false, null);
    }

    return retorno;
}

function desabilitaHabilitaComponentes() {
    var tipoOperacao = hfGeral.Get("TipoOperacao").toString();
    var BoolEnabled = false;
    if (("Incluir" === tipoOperacao) || ("Editar" === tipoOperacao))
        BoolEnabled = true;

    //--- alguma coisa de testing

    txtNomePrograma.SetEnabled(BoolEnabled);
    ddlGerentePrograma.SetEnabled(BoolEnabled);
    ddlUnidadeNegocio.SetEnabled(BoolEnabled);

    if ("Incluir" === tipoOperacao)
        document.getElementById('tdProjToProg').style.display = '';
    else
        document.getElementById('tdProjToProg').style.display = 'none';

    gvProjetos.SetEnabled(BoolEnabled);

}

var retornoTela = null;

function abreSelecaoProjeto() {
    window.top.showModal("./ListagemDeProjetos.aspx", traducao.programasDoProjetos_selecionar_projeto, 840, 400, processaRetornoTela, null);
}

function processaRetornoTela(retornoTela) {
    if (retornoTela !== null && retornoTela !== '') {
        hfGeral.Set("CodigoProjeto", retornoTela[0]);
        txtNomePrograma.SetText(retornoTela[2]);
        ddlGerentePrograma.SetValue(retornoTela[3]);
        alert(retornoTela[0]);
        gvProjetos.PerformCallback(retornoTela[0]);

        TipoOperacao = "Editar";
        hfGeral.Set("TipoOperacao", TipoOperacao);
    }
}

function novoPrograma() {
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
    desabilitaHabilitaComponentes();
    parametro = "-1";

    gvProjetos.PerformCallback(parametro);
}

////------------------------------------------------------------funções relacionadas com a TreeList
//function OnTreeListFocusedNodeChanged() {
//    treeList.GetNodeValues(treeList.GetFocusedNodeKey(), 'CodigoUnidadeNegocio;', pegaCodigo);
//}

function pegaCodigo(codigo) {
    hfGeral.Set("hfCodigoUnidadeTree", codigo);
}

function ClearSelection() {
    //treeList.SetFocusedNodeKey("");
    UpdateControls(null, "");
}
function UpdateSelection() {
    var employeeName = "";
    //var focusedNodeKey = treeList.GetFocusedNodeKey();
    //if (focusedNodeKey != "")
    //    employeeName = treeList.cpEmployeeNames[focusedNodeKey];
    UpdateControls(focusedNodeKey, employeeName);
}
function UpdateControls(key, text) {
    //ddlUnidade.SetText(text);
    alert('update antes: ' + key);
    ddlUnidadeNegocio.SetValue(key);
    alert('update antes: ' + key);
    //pegaCodigo(key);
    //ddlUnidade.HideDropDown();
    //UpdateButtons();
}
function UpdateButtons() {
    //btnSelecionar.SetEnabled(treeList.GetFocusedNodeKey() != "");
}
function OnDropDown() {
    //treeList.SetFocusedNodeKey(ddlUnidade.GetKeyValue());
    //treeList.MakeNodeVisible(treeList.GetFocusedNodeKey());
}

function onIndexChanged(s,e)
{
    if (e.visibleIndex > -1) {
        var selecionados = hfGeral.Get('Selecionados');

        if (e.isSelected) {
            selecionados += s.GetRowKey(e.visibleIndex) + ';';
        }
        else {
            selecionados = selecionados.replace(';' + s.GetRowKey(e.visibleIndex) + ';', ';');
        }

        hfGeral.Set('Selecionados', selecionados);
    }
    else if (e.isAllRecordsOnPage)
    {
        if (e.isSelected) {
            hfGeral.Set('Selecionados', 'Todos');
        }
        else {
            hfGeral.Set('Selecionados', ';');
        }
    }
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 100;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}

function editarPrograma(codigoProjeto) {
    var sUrl = window.top.pcModal.cp_Path + '_Projetos/Administracao/popupProgramasDoProjeto.aspx?CP=' + codigoProjeto  + '&RO=N';
    window.top.showModal(sUrl, 'Detalhes', null, null, atualizaGrid, null);
}


function visualizarPrograma(codigoProjeto) {

    var sUrl = window.top.pcModal.cp_Path + '_Projetos/Administracao/popupProgramasDoProjeto.aspx?CP=' + codigoProjeto + '&RO=S';
    window.top.showModal(sUrl, 'Detalhes', null, null, atualizaGrid, null);
}

function excluirPrograma(codigoProjeto) {
    callbackTela.PerformCallback(codigoProjeto);
}

function atualizaGrid() {
    gvDados.Refresh();
}