/// <summary>
/// A��o a realizar ao fazer foco na filas da grid gvMetas
/// </summary>
/// <param name="sender">Objeto ASPxGridView</param>
/// <param name="e">propiedades geral</param>
var idBotao = "";
function gvMetas_FocusedRowChanged(s,e) {
    var rowIndex = s.GetFocusedRowIndex();
    if ( -1 < rowIndex )
        s.GetRowValues(rowIndex, 'CodigoMetaOperacional', preencheGridPeriodo);
}


function preencheGridPeriodo(valores) {

    if (null != valores) {
        if (window.hfGeral)
            hfGeral.Set('CodigoMeta', valores[0]);
        try {
            gvPeriodo.PerformCallback('RecargaPeriodo');
        } catch (e) { }
    }
}

/// <summary>
/// A��o a realizar ao fazer foco na filas da grid gvPeriodo
/// </summary>
/// <param name="sender">Objeto ASPxGridView</param>
/// <param name="e">propiedades geral</param>
function gvPeriodo_FocusedRowChanged(s, e) {
    var rowIndex = s.GetFocusedRowIndex();
    if (-1 < rowIndex)
        s.GetRowValues(rowIndex, 'CodigoProjeto;CodigoIndicador;Ano;Mes;Periodo;MetaMes;ResultadoMes;Desempenho;', preencheDetalhePeriodo);
}

function preencheDetalhePeriodo(valores) {
    if (null != valores) {
        var CodigoProjeto = valores[0];
        var CodigoIndicador = valores[1];
        var Ano = valores[2];
        var Mes = valores[3];
        var periodo = valores[4];
        var MetaMes = valores[5];
        var ResultadoMes = valores[6];
        var Desempenho = valores[7];

        hfGeral.Set('Ano', Ano);
        hfGeral.Set('Mes', Mes);
    }
}

/// <summary>
/// A��o a realizar ao fazer click nos diferentes CustomButtom da grid gvPeriodo
/// pasou como parametro ao callback os id do CustomButton feito click.
///             "btnCustomNovo" - "btnCustomEdit" - "btnCustomExcluir"
/// </summary>
/// <param name="sender">Objeto ASPxGridView</param>
/// <param name="e">Par�metros</param>
function OnClick_CustomGvPeriodo(s, e) {
    if (e.buttonID == "btnCustomExcluir") {
        idBotao = e.buttonID;
        window.top.mostraMensagem(traducao.AnaliseDasMetas_deseja_realmente_excluir_o_registro_, 'confirmacao', true, true, excluiRegistro);
    }

    else
        pnCallbackDetalhe.PerformCallback(e.buttonID);
}

function excluiRegistro() {
    gvPeriodo.PerformCallback(idBotao);
}

function OnClick_NovoDetalhe()
{
    window.top.mostraMensagem(traducao.AnaliseDasMetas_novo_detalhe, 'Atencao', true, false, null);
}


function End_pnCallbackDados()
{
	if(hfAcao.Get('acao') != 'Excluir')
		pcDados.Show();
	if(hfAcao.Get('acao') == 'Salvar')
		pcDados.Hide();
}

function On_ClickCancelar(s, e)
{
	e.processOnServer = false;
    pcDados.Hide();
}

function On_ClickSalvar(s, e)
{
	e.processOnServer = false;
	if(mmAnalise.isValid)
	{
	    hfAcao.Set('analise', mmAnalise.GetText());
	    hfAcao.Set('recomendacoes', mmRecomendacoes.GetText());
	    pnCallbackDetalhe.PerformCallback('Salvar');
    }
}