
function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
   mensagemErro_ValidaCamposFormulario = "";
   var numAux = 0;
    var mensagem = "";
    
    if(txtNomeMunicipio.GetText() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMunicipiosObra_o_nome_do_munic_pio_deve_ser_informado_;
    }
    if(ddlUF.GetValue() == null)
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMunicipiosObra_a_uf_deve_ser_informada_;
    }
    if(txtSigla.GetText() == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroMunicipiosObra_a_sigla_do_munic_pio_deve_ser_informada_;
    }
    
    if(mensagem != "")
    {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }
    
    return mensagemErro_ValidaCamposFormulario;
}
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
    txtNomeMunicipio.SetText("");
    ddlUF.SetValue(null);
    txtSigla.SetText("");
    
    desabilitaHabilitaComponentes();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(),'CodigoMunicipio;NomeMunicipio;SiglaUF;SiglaMunicipio;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values)
{
    desabilitaHabilitaComponentes();
    
    var codigoMunicipio = (values[0] != null ? values[0]  : "");
    var nomeMunicipio = (values[1] != null ? values[1]  : "");
    var siglaUF = (values[2] != null ? values[2]  : "");
    var siglaMunicipio = (values[3] != null ? values[3]  : "");    
    
    txtNomeMunicipio.SetText(nomeMunicipio);
    ddlUF.SetValue(siglaUF);
    txtSigla.SetText(siglaMunicipio);    
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    txtNomeMunicipio.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlUF.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtSigla.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
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