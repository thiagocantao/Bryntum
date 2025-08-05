 // JScript File
function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    if(txtCriterio.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.criteriosSelecaoNova_o_nome_do_crit_rio_deve_ser_informado_;
        txtCriterio.Focus();
    }
    else if(gridDescricao.GetVisibleRowsOnPage() == 0 && TipoOperacao=="Editar")
    {
        mensagemErro_ValidaCamposFormulario = traducao.criteriosSelecaoNova_as_op__es_do_crit_rio_devem_ser_cadastradas_;
    }    

    return mensagemErro_ValidaCamposFormulario;
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
    txtCriterio.SetText("");
    desabilitaHabilitaComponentes();
    if (TipoOperacao=="Incluir")
        gridDescricao.PerformCallback(TipoOperacao);    
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoCriterioSelecao;CodigoCriterioSelecao;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    //var descricaoCriterioSelecao = (values[0] == null) ? "" : values[0].toString();
    txtCriterio.SetText(values[0].toString());
    desabilitaHabilitaComponentes();
    gridDescricao.PerformCallback(TipoOperacao + "_" + values[1].toString());                 
}

function desabilitaHabilitaComponentes()
{
    txtCriterio.SetEnabled(TipoOperacao != "Consultar");
}

function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
    if (gridDescricao.GetVisibleRowsOnPage() > 0 )
        onClick_btnCancelar();    
}

