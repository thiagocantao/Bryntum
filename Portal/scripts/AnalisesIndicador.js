function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'Analise;Recomendacoes;DataAnalise;Responsavel;CodigoCorStatusObjetoAssociado', MontaCamposFormulario);
    }
}

function LimpaCamposFormulario()
{
    txtAnalise.SetText("");
    txtRecomendacoes.SetText("");
    lblInclusao.SetText("");
    ddlStatus.SetValue(null);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    if(values)
    {
        var analise          = values[0];
        var recomendacoes    = values[1];
        var dataAnalise      = values[2];
        var responsavel      = values[3];
        var corStatus        = values[4];
        
        txtAnalise.SetText(analise);
        txtRecomendacoes.SetText(recomendacoes);
        ddlStatus.SetValue(corStatus);
        var strInclusao = "";
        var strAlteracao = "";

        strInclusao = (responsavel != '' && dataAnalise != '') ? "Feito por " + responsavel + " em " + dataAnalise : "";

        lblInclusao.SetText(strInclusao);
    }
}

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

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";
        
    if (Trim(txtAnalise.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.AnalisesIndicador_as_tend_ncias_devem_ser_informadas_;
    }
    if (Trim(txtRecomendacoes.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.AnalisesIndicador_a_agenda_deve_ser_informada_;
    }
    if (ddlStatus.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.AnalisesIndicador_o_status_deve_ser_informado_;
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao() {
    onClick_btnCancelar();
}


