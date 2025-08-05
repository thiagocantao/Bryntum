// JScript File
// ---- Provavelmente não será necessário alterar as duas próximas funções
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
    
    if (txtRisco.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.riscosPadroes_a_descri__o_do_risco_padr_o_deve_ser_informada_;
        txtRisco.Focus();
    }
    
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtRisco.SetText("");
    mmDescricaoImpactoAlto.SetText("");
    mmDescricaoImpactoMedio.SetText("");
    mmDescricaoImpactoBaixo.SetText("");
    mmDescricaoProbabilidadeAlta.SetText("");
    mmDescricaoProbabilidadeMedia.SetText("");
    mmDescricaoProbabilidadeBaixa.SetText("");
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoRiscoPadrao;DescricaoImpactoAlto;DescricaoImpactoMedio;DescricaoImpactoBaixo;DescricaoProbabilidadeAlta;DescricaoProbabilidadeMedia;DescricaoProbabilidadeBaixa', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    if(values)
    {
        txtRisco.SetText(values[0]!= null ? values[0] : "");
        mmDescricaoImpactoAlto.SetText(values[1]!= null ? values[1] : "");
        mmDescricaoImpactoMedio.SetText(values[2]!= null ? values[2] : "");
        mmDescricaoImpactoBaixo.SetText(values[3]!= null ? values[3] : "");
        mmDescricaoProbabilidadeAlta.SetText(values[4]!= null ? values[4] : "");
        mmDescricaoProbabilidadeMedia.SetText(values[5]!= null ? values[5] : "");
        mmDescricaoProbabilidadeBaixa.SetText(values[6]!= null ? values[6] : "");

        var numeroCaracteres = 0;
        numeroCaracteres = mmDescricaoImpactoAlto.GetText().length;
        (numeroCaracteres == 0) ? lblCantCaraterImpactoAlto.SetText('0') : lblCantCaraterImpactoAlto.SetText(numeroCaracteres);
        numeroCaracteres = mmDescricaoImpactoMedio.GetText().length;
        (numeroCaracteres == 0) ? lblCantCaraterImpactoMedio.SetText('0') : lblCantCaraterImpactoMedio.SetText(numeroCaracteres);
        numeroCaracteres = mmDescricaoImpactoBaixo.GetText().length;
        (numeroCaracteres == 0) ? lblCantCaraterImpactoBaixo.SetText('0') : lblCantCaraterImpactoBaixo.SetText(numeroCaracteres);
        numeroCaracteres = mmDescricaoProbabilidadeAlta.GetText().length;
        (numeroCaracteres == 0) ? lblCantCaraterProbabilidadeAlta.SetText('0') : lblCantCaraterProbabilidadeAlta.SetText(numeroCaracteres);
        numeroCaracteres = mmDescricaoProbabilidadeMedia.GetText().length;
        (numeroCaracteres == 0) ? lblCantCaraterProbabilidadeMedia.SetText('0') : lblCantCaraterProbabilidadeMedia.SetText(numeroCaracteres);
        numeroCaracteres = mmDescricaoProbabilidadeBaixa.GetText().length;
        (numeroCaracteres == 0) ? lblCantCaraterProbabilidadeBaixa.SetText('0') : lblCantCaraterProbabilidadeBaixa.SetText(numeroCaracteres);
    }
}

function desabilitaHabilitaComponentes()
{
    txtRisco.SetEnabled(TipoOperacao != "Consultar");
    mmDescricaoImpactoAlto.SetEnabled(TipoOperacao != "Consultar");
    mmDescricaoImpactoMedio.SetEnabled(TipoOperacao != "Consultar");
    mmDescricaoImpactoBaixo.SetEnabled(TipoOperacao != "Consultar");
    mmDescricaoProbabilidadeAlta.SetEnabled(TipoOperacao != "Consultar");
    mmDescricaoProbabilidadeMedia.SetEnabled(TipoOperacao != "Consultar");
    mmDescricaoProbabilidadeBaixa.SetEnabled(TipoOperacao != "Consultar");
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    onClick_btnCancelar();  
}


// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------
//-------
    function mostraPopupMensagemGravacao(acao)
    {
        lblAcaoGravacao.SetText(acao);
        pcMensagemGravacao.Show();
        setTimeout ('fechaTelaEdicao();', 1500);
    }

    function fechaTelaEdicao()
    {
        pcMensagemGravacao.Hide();
        onClick_btnCancelar()
    }
    
//======================================
//      AÇÕES DE CONTROLE DOS COMPONENTE DA TELA.
//--------------------------------------
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
    {
        if(textAreaElement.id =="mmDescricaoImpactoAlto_I")
            lblCantCaraterImpactoAlto.SetText(text.length);
        else if(textAreaElement.id =="mmDescricaoImpactoMedio_I")
            lblCantCaraterImpactoMedio.SetText(text.length);
        else if(textAreaElement.id =="mmDescricaoImpactoBaixo_I")
            lblCantCaraterImpactoBaixo.SetText(text.length);
        else if(textAreaElement.id =="mmDescricaoProbabilidadeAlta_I")
            lblCantCaraterProbabilidadeAlta.SetText(text.length);
        else if(textAreaElement.id =="mmDescricaoProbabilidadeMedia_I")
            lblCantCaraterProbabilidadeMedia.SetText(text.length);
        else if(textAreaElement.id =="mmDescricaoProbabilidadeBaixa_I")
            lblCantCaraterProbabilidadeBaixa.SetText(text.length);
    }
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}