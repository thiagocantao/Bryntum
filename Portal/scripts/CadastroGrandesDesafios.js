// JScript File

function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario()
{
    //var tOperacao = ""

    try
    {// Função responsável por preparar os campos do formulário para receber um novo registro
        hfGeral.Set("codigoGrandeDesafio", "");
        hfGeral.Set("MetaPeriodo", "");
        hfGeral.Set("memGrandeDesafio", "");
        checkAtivo.SetChecked(true);


        txtMetaPeriodoEstrategico.SetText("");
        memGrandeDesafio.SetText("");

    }catch(e){}
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'MetaPeriodoEstrategico;DescricaoGrandeDesafio;CodigoGrandeDesafio;IndicaGrandeDesafioAtivo;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    var delimitador = "¥";

    try {
        txtMetaPeriodoEstrategico.SetText(values[0] != null ? values[0] : "");
        memGrandeDesafio.SetText(values[1] != null ? values[1] : "");
        hfGeral.Set("codigoGrandeDesafio", (values[2] != null ? values[2] : "-1"));

        var CodigoGrandeDesafio = (values[2] != null ? values[2] : "-1");
        var parametro = CodigoGrandeDesafio;
        var indicaGrandeDesafioAtivo = values[3];

        if (indicaGrandeDesafioAtivo == 'S')
            checkAtivo.SetValue(true);
        else
            checkAtivo.SetValue(false);      

        lblCantCarater.SetText(memGrandeDesafio.GetText().toString().length);

        desabilitaHabilitaComponentes();
    } catch (e) { }
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

function verificarDadosPreenchidos()
{
    var retorno = true;


    var tipoPapel = memGrandeDesafio.GetText();
    if("" == tipoPapel)
    {
        retorno = false;
        window.top.mostraMensagem(traducao.CadastroGrandesDesafios_o_campo_descri__o_grande_desafio_deve_ser_informado_, 'erro', true, false, null);
    }
    //--- alguma coisa de testing
	return retorno;
}



function desabilitaHabilitaComponentes()
{
    var tipoOperacao = hfGeral.Get("TipoOperacao").toString();
	var BoolEnabled = false; 
    if(("Incluir" == tipoOperacao) || ("Editar" == tipoOperacao))
        BoolEnabled = true;
    
    //--- alguma coisa de testing

    txtMetaPeriodoEstrategico.SetEnabled(BoolEnabled);
    memGrandeDesafio.SetEnabled(BoolEnabled);
    checkAtivo.SetEnabled(BoolEnabled);
    
    
}

var retornoTela = null;
         


function NovoGrandeDesafio()
{
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
    desabilitaHabilitaComponentes();
    var parametro = "-1"; 

}

//------------------------------------------------------------funções relacionadas com a ListBox
var delimitador = ";";

function UpdateButtons() 
{
    btnADD.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaProjetos.GetSelectedItem() != null);
    btnADDTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaProjetos.GetItemCount() > 0 );
    btnRMV.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaSelecionados.GetSelectedItem() != null);
    btnRMVTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaSelecionados.GetItemCount() > 0);
    capturaCodigosProjetosSelecionados();
}



function OnGridFocusedRowChangedPopup(grid) {
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoGrandeDesafio;MetaPeriodoEstrategico;DescricaoGrandeDesafio;IndicaGrandeDesafioAtivo;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor) {
    var idObjeto = (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var tituloMapa = "";

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = 900;
    var window_height = 590;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;
    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=CP&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa, traducao.CadastroGrandesDesafios_permiss_es, 920, 585, '', null);
}

function setMaxLength(textAreaElement, length) {
    textAreaElement.maxlength = length;
    ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
    ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
}

function onKeyUpOrChange(evt) {
    processTextAreaText(ASPxClientUtils.GetEventSource(evt));
}

function processTextAreaText(textAreaElement) {
    var maxLength = textAreaElement.maxlength;
    var text = textAreaElement.value;
    var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
    if (maxLength != 0 && text.length > maxLength)
        textAreaElement.value = text.substr(0, maxLength);
    else
        lblCantCarater.SetText(text.length);
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}