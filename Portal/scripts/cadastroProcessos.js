var existeConteudoCampoAlterado = false;

function conteudoCampoAlterado() 
{
    existeConteudoCampoAlterado = true;
}

// JScript File

//---------------------------
function validaCampos()
{
    var retorno = true;
    var nomeProjeto = txtNomeProjeto.GetText();
    var unidadeNegocio = ddlUnidadeNegocio.GetText();
    var gerenteProjeto = ddlGerenteProjeto.GetText();
    
    var mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;
    
    if("" == nomeProjeto)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProcessos_o_nome_do_processo_deve_ser_informado_ + "\n";
        retorno = false;
    }
        
    if("" == unidadeNegocio)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProcessos_a_unidade_de_negocio_deve_ser_informado_ + "\n";
        retorno = false;
    }
    
    if("" == gerenteProjeto)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProcessos_o_respons_vel_deve_ser_informado_ + "\n";
        retorno = false;
    }  
    if(rbQuemAtualiza.GetValue() == null)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProcessos_informe_quem_atualizar__o_projeto_ + "\n";
         retorno = false;
    }

    if(rbTipoAprovacao.GetValue() == null)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProcessos_o_tipo_de_aprova__o_deve_ser_informado_ + "\n";
         retorno = false;
    }
       
    existeConteudoCampoAlterado = false;
    
    if("" != mensagemErro_ValidaCamposFormulario)
        window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'erro', true, false, null);
    
    return retorno;
}

function mudaNomeProjeto(nomeProjeto)
{
    parent.lblTituloTela.SetText(nomeProjeto + ' - ' + traducao.cadastroProcessos_caracteriza__o);
}