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
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************
    
function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;
        
    if(txtNome.GetText() == "")
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.assuntosDemandas_campo_assunto_deve_ser_informado_ + "\n";        
        
    if(ddlResponsavel.GetValue() == null)
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.assuntosDemandas_campo_respons_vel_deve_ser_informado_ + "\n";   
        
    if(ddlFluxo.GetValue() == null)
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.assuntosDemandas_campo_fluxo_associado_deve_ser_informado_ + "\n";   
    
    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{
	var BoolEnabled = TipoOperacao != "Consultar";
   
    txtNome.SetEnabled(BoolEnabled);
    ddlResponsavel.SetEnabled(BoolEnabled);
    ddlFluxo.SetEnabled(BoolEnabled);
    ddlProjeto.SetEnabled(BoolEnabled);        
    txtObservacoes.SetEnabled(BoolEnabled);
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtNome.SetText("");
    ddlResponsavel.SetValue(null);
    ddlFluxo.SetValue(null);
    ddlProjeto.SetValue(-1);        
    txtObservacoes.SetText("");   
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    //Recebe os valores dos campos de acordo com a linha selecionada. O resultado é passado para a função montaCampo
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
         grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoAssuntoDemanda;DescricaoAssuntoDemanda;CodigoGerente;CodigoFluxo;CodigoProjetoAssociado;Comentario', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{ 
    LimpaCamposFormulario();
    
    if(valores)
    {
        var codigoAssunto  = valores[0];
        var assunto    = valores[1];
        var codigoGerente = valores[2];
        var codigoFluxo = valores[3];
        var codigoProjetoAssociado = valores[4];
        var comentario = valores[5];
                        
        txtNome.SetText(assunto);
        ddlResponsavel.SetValue(codigoGerente == null ? null : codigoGerente.toString());
        ddlFluxo.SetValue(codigoFluxo == null ? null : codigoFluxo.toString());
        ddlProjeto.SetValue(codigoProjetoAssociado == null ? -1 : codigoProjetoAssociado.toString());        
        txtObservacoes.SetText(comentario == null ? "" : comentario.toString());   
    }
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

//-----------
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao()
{
    onClick_btnCancelar();
}

/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function OnGridFocusedRowChangedPopup(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoAssuntoDemanda;DescricaoAssuntoDemanda;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor)
{
    var idObjeto     =  (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var tituloMapa   = "";
   
    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width    = 900;
    var window_height   = 590;
    var newfeatures     = 'scrollbars=no,resizable=no';
    var window_top      = (screen.height-window_height)/2;
    var window_left     = (screen.width-window_width)/2;
    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=AD&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa, traducao.assuntosDemandas_permiss_es, 920, 585, '', null);
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
    var maxLength    = textAreaElement.maxlength;
    var text         = textAreaElement.value;
    var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
    
    if (maxLength != 0 && text.length > maxLength) 
        textAreaElement.value = text.substr(0, maxLength);
    else
        lblCantCarater.SetText(text.length);
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

function onClick_CustomIncluir()
{
    TipoOperacao = "Incluir";
    desabilitaHabilitaComponentes();
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
}