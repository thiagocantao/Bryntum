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

function desabilitaHabilitaComponentes()
{
 
        txtAssunto.SetEnabled("Consultar" != TipoOperacao)
    
    
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
    
    if (txtAssunto.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.assuntosLicoesAprendidas_a_descri__o_do_assunto_deve_ser_informada_;
        txtAssunto.Focus();
    }
    
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtAssunto.SetText("");
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoAssuntoLicaoAprendida', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    txtAssunto.SetText(values);
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