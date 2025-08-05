 // JScript File
function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    if(gridCR.GetVisibleRowsOnPage() == 0 && TipoOperacao=="Editar")
    {
        mensagemErro_ValidaCamposFormulario = traducao.Indicador_CR_SMD_nenhum_cr_foi_informado_;
    }    

    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtIndicador.SetText("");
    desabilitaHabilitaComponentes();
    if (TipoOperacao=="Incluir")
        gridCR.PerformCallback(TipoOperacao);    
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'NomeIndicador;CodigoIndicador;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    txtIndicador.SetText(values[0].toString());
    desabilitaHabilitaComponentes();
    gridCR.PerformCallback(TipoOperacao + "_" + values[1].toString());                 
}

function desabilitaHabilitaComponentes()
{
    txtIndicador.SetEnabled(false);
}

function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
    if (gridCR.GetVisibleRowsOnPage() > 0 )
        onClick_btnCancelar();    
}

