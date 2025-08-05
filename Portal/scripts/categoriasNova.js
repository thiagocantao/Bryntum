function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    if(txtCategoria.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.categoriasNova_o_nome_da_categoria_deve_ser_informado_;
        txtCategoria.Focus();
    }    
    else if(txtSigla.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.categoriasNova_a_sigla_da_categoria_deve_ser_informada_;
        txtSigla.Focus();
    }    

    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{
    txtCategoria.SetEnabled(TipoOperacao != "Consultar");
    txtSigla.SetEnabled(TipoOperacao != "Consultar");    
}

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

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtCategoria.SetText("");
    txtSigla.SetText("");
    
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoCategoria;DescricaoCategoria;SiglaCategoria;', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    var codigoCategoria = (valores[0] == null) ? "" : valores[0].toString();
    var descricaoCategoria =(valores[1] == null) ? "" : valores[1].toString();  
    var siglaCategoria =(valores[2] == null) ? "" : valores[2].toString();  
       
    txtCategoria.SetText(descricaoCategoria);
    txtSigla.SetText(siglaCategoria);
}

function abreMatrizCategoria(codigoCategoria)
{
    window.top.showModal('matrizCategoria.aspx?CC=' + codigoCategoria + '&alt=900&larg=560', 'Matriz de Priorização de Projetos', null, null, '', null);
}