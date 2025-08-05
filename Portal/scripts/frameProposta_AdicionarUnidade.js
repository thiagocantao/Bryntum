// JScript File

//---------------------------
function validaCampos()
{
    var retorno = true;
    var data1 = deInicioProposta.lastSuccessText;
    var data2 = deTerminoProposta.lastSuccessText;
    var associarCronograma = ddlAssoCroExistente.GetText();
    var unidadeNegocio = ddlUnidadeNegocio.GetText();
    var gerenteProjeto = ddlGerenteProjeto.GetText();
    
    var mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;
    
    if("" == unidadeNegocio)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.frameProposta_AdicionarUnidade_a_unidade_de_negocio_deve_ser_informado_ + "\n";
        retorno = false;
    }
    
    if("" == gerenteProjeto)
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.frameProposta_AdicionarUnidade_o_gerente_de_projeto_deve_ser_informado_ + "\n";
        retorno = false;
    }
        
    if("" == associarCronograma)
    {
        if ("" == data1)
        {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.frameProposta_AdicionarUnidade_a_data_in_cio_deve_ser_informado_ + "\n";
            retorno = false;
        }
        if ("" == data2)
        {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.frameProposta_AdicionarUnidade_a_data_t_rmino_deve_ser_informado_ + "\n";
            retorno = false;
        }
    }
    /*
    if (data1 == '' || data2 == '')
    {
        return false;
    }
    */

    if (data1 != '')
    {
        if( data2 != '')
        {
            var nova_data1 = parseInt(data1.split("/")[2].toString() + data1.split("/")[1].toString() + data1.split("/")[0].toString());
            var nova_data2 = parseInt(data2.split("/")[2].toString() + data2.split("/")[1].toString() + data2.split("/")[0].toString());
            if (nova_data2 < nova_data1)
            {
                mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.frameProposta_AdicionarUnidade_a_data_t_rmino_n_o_pode_ser_menor_ou_igual___data_in_cio_ + "\n";
                retorno = false;
            }
        }
    }
    existeConteudoCampoAlterado = false;
    
    if("" != mensagemErro_ValidaCamposFormulario)
        window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'erro', true, false, null);
    
    return retorno;
}


//---------------------------
function verificaSelecao(s)
{
    if(s.GetValue() == "ACE")
    {
        document.getElementById('CE1').style.display = 'block';
        document.getElementById('CE2').style.display = 'block';
        document.getElementById('NC1').style.display = 'none';
        document.getElementById('NC2').style.display = 'none';
        document.getElementById('CM1').style.display = 'none';
        document.getElementById('CM2').style.display = 'none';
    }
    else if(s.GetValue() == "NCB")
    {
        document.getElementById('CE1').style.display = 'none';
        document.getElementById('CE2').style.display = 'none';
        document.getElementById('NC1').style.display = 'none';
        document.getElementById('NC2').style.display = 'none';
        document.getElementById('CM1').style.display = 'none';
        document.getElementById('CM2').style.display = 'none';
    }
    else if(s.GetValue() == "NCM")
    {
        document.getElementById('CE1').style.display = 'none';
        document.getElementById('CE2').style.display = 'none';
        document.getElementById('NC1').style.display = 'none';
        document.getElementById('NC2').style.display = 'none';
        document.getElementById('CM1').style.display = 'block';
        document.getElementById('CM2').style.display = 'block';
    }
}