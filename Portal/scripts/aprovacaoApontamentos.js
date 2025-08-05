
var comando = '';
function abreComentarios(indexLinha)
{
    lpLoad.Show();
    gvDados.SelectRowOnPage(gvDados.GetFocusedRowIndex());
    gvDados.GetSelectedFieldValues('CodigoAtribuicao;', abreDatelhes);
        
}

function abreDatelhes(values)
{    
    window.top.showModal('./ComentariosTarefa.aspx?CA=' + values[0][0], traducao.aprovacaoTarefas_coment_rios, 760, 420, '', null, lpLoad);
}

function selecionaTodos(valor)
{
    gvDados.SelectAllRowsOnPage(valor);
}

function abreAnexos(indexLinha) {
    lpLoad.Show();
    gvDados.SelectRowOnPage(gvDados.GetFocusedRowIndex());
    gvDados.GetSelectedFieldValues('CodigoAtribuicao;', abreListaAnexos);
}

function abreListaAnexos(values) {
    var urlAnexo = "../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=AT&ID=" + values[0][0] + "&ALT=382";
    window.top.showModal(urlAnexo, traducao.aprovacaoTarefas_anexos, 860, 460, '', null, lpLoad);
}

function validaSelecaoGrid() {
    return gvDados.GetSelectedKeysOnPage().length > 0;
}


function abreTelaMeusApontamentosAprovados() {
    var urlHistoricoApontamentos = "../espacoTrabalho/popupApontamentosTarefasAprovados.aspx";
    window.top.showModalComFooter(urlHistoricoApontamentos, 'Histórico de Apontamentos', null, null, '', null, lpLoad);
}
function funcaoCallbackFechar() {
    window.top.fechaModalComFooter();
}