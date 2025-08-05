// JScript File

function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    //LimpaCamposFormulario();
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************


function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registr
    
    txtAno.SetText("");
    
    ddlAnoAtivo.SetValue("N");
    ddlMetaEditavel.SetValue("N");
    ddlResultadoEditavel.SetValue("N");
    ddlTipoVisualizacao.SetValue("P");    
    desabilitaHabilitaComponentes();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if(forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'Ano;IndicaAnoAtivo;IndicaMetaEditavel;IndicaResultadoEditavel;IndicaTipoDetalheVisualizacao;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    
    txtAno.SetText((values[0] != null ? values[0].toString()  : ""));
    
    //(values[4] == null ? ddlInicioReal.SetText("")  : ddlInicioReal.SetValue(values[4]));
    (values[1] == null ? ddlAnoAtivo.SetValue("N")  : ddlAnoAtivo.SetValue(values[1]));
    (values[2] == null ? ddlMetaEditavel.SetValue("N")  : ddlMetaEditavel.SetValue(values[2]));
    (values[3] == null ? ddlResultadoEditavel.SetValue("N") : ddlResultadoEditavel.SetValue(values[3]));
    (values[4] == null ? ddlTipoVisualizacao.SetValue("P") : ddlTipoVisualizacao.SetValue(values[4]));
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0 )
        onClick_btnCancelar();    
}

function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;
    
	var meuData 	= new Date();
	var meuAnoAtual = meuData.getFullYear();

	var anoNovo = txtAno.GetText();
	var anos = hfGeral.Get('hfAnos').toString();
	
	if(anoNovo.replace("_", "").length < 4)
	    mensagemErro_ValidaCamposFormulario += "Ano Inválido!"; 
	
	if("Editar" != hfGeral.Get("TipoOperacao").toString())
	    if ( ("" != anos) & (anos.indexOf(anoNovo)>=0) )
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.PeriodoEstrategico_o_ano_digitado_n_o_pode_ser_igual_aos_j__existentes_ + "\n"; 
    
    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{
    var tipoOperacao = TipoOperacao; //hfGeral.Get("TipoOperacao").toString();
    
    if(("Consultar" == tipoOperacao) || ("" == tipoOperacao))
    {
        txtAno.SetEnabled(false);
        ddlAnoAtivo.SetEnabled(false);
        ddlMetaEditavel.SetEnabled(false);
        ddlResultadoEditavel.SetEnabled(false);
        ddlTipoVisualizacao.SetEnabled(false);
    }
    if(("Incluir" == tipoOperacao))
    {
        txtAno.SetEnabled(true);
        ddlAnoAtivo.SetEnabled(true);
        ddlMetaEditavel.SetEnabled(true);
        ddlResultadoEditavel.SetEnabled(true);
        ddlTipoVisualizacao.SetEnabled(true);
    }
    if(("Editar" == tipoOperacao))
    {
        txtAno.SetEnabled(false);
        ddlAnoAtivo.SetEnabled(true);
        ddlMetaEditavel.SetEnabled(true);
        ddlResultadoEditavel.SetEnabled(true);
        ddlTipoVisualizacao.SetEnabled(true);
    }
}

//-------------------- DIV salvar
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar()
} 
//**** fim DIV salvar


function OnEndCallback(s, e) {
    AdjustSize();
}
function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 100;
    gvDados.SetHeight(height);
}