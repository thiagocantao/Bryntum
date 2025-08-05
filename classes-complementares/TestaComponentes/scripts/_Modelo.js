// JScript File
function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
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
    txtNome.SetText("");
    cmbSexo.SetText("");
    txtEndereco.SetText("");
    txtComplemento.SetText("");
    txtBairro.SetText("");
    txtCidade.SetText("");
    cmbUF.SetText("");
    txtFoneFixo.SetText("");
    txtFoneCelular.SetText("");
    cmbDataNascimento.SetText("");
    cmbDataAdmissao.SetText("");
    txtObservacao.SetText("");
    txtIdade.SetText("");
    txtSalario.SetText("");
}

function OnGridFocusedRowChanged(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'Codigo;Nome;Sexo;Endereco;Complemento;Bairro;Cidade;Uf;TelefoneFixo;TelefoneCelular;DataNascimento;DataAdmissao;Observacoes;Idade;Salario;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    txtNome.SetText(values[1]);
    cmbSexo.SetText(values[2]);
    txtEndereco.SetText(values[3]);
    txtComplemento.SetText(values[4]);
    txtBairro.SetText(values[5]);
    txtCidade.SetText(values[6]);
    cmbUF.SetText(values[7]);
    txtFoneFixo.SetText(values[8]);
    txtFoneCelular.SetText(values[9]);
    cmbDataNascimento.SetValue(values[10]);
    cmbDataAdmissao.SetValue(values[11]);
    txtObservacao.SetText(values[12]);
    txtIdade.SetText(values[13]);
    txtSalario.SetText(values[14]);    
}