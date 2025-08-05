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
    var tipoProjeto = ddlTipoProjeto.GetText();
    
    var mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;
    
    if("" == nomeProjeto)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProjetos_o_nome_do_projeto_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if ("" == tipoProjeto) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProjetos_o_tipo_do_projeto_deve_ser_informado_ + "\n";
        retorno = false;
    }
        
    if("" == unidadeNegocio)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProjetos_a_ + hfGeral.Get("definicaoUnidade").toString() + traducao.cadastroProjetos__deve_ser_informada_ + "\n";
        retorno = false;
    }
    
    if("" == gerenteProjeto)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProjetos_o_gerente_de_projeto_deve_ser_informado_ + "\n";
        retorno = false;
    }  
    if (isNaN(ddlGerenteProjeto.GetValue()) == true)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProjetos_gerente_de_projetos_informado_n_o_existe_na_base_de_dados_ + "\n";
        retorno = false;
    }
    if(rbQuemAtualiza.GetValue() == null)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProjetos_informe_quem_atualizar__o_projeto_ + "\n";
         retorno = false;
    }
    if(rbTipoAprovacao.GetValue() == null)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroProjetos_o_tipo_de_aprova__o_deve_ser_informado_ + "\n";
         retorno = false;
    }
       
    existeConteudoCampoAlterado = false;
    
    if("" != mensagemErro_ValidaCamposFormulario)
        window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'atencao', true, false, null);
    
    return retorno;
}

function mudaNomeProjeto(nomeProjeto)
{
    parent.lblTituloTela.SetText(nomeProjeto + ' - ' + traducao.cadastroProjetos_caracteriza__o);
}


