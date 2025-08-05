/*---------------------------------------------------
 cadastroEntidade.js
Author:  Alejandro Fuentes
Date: 17 / 11 / 2009
-----------------------------------------------------*/
                  
function OnGridFocusedRowChanged(s)
{
    try
    {
        s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoUnidadeNegocio;SiglaUnidadeNegocio;NomeUnidadeNegocio;CodigoUsuarioGerente;SiglaUF;IndicaUnidadeNegocioAtiva;Observacoes;NomeUsuario;Email;TelefoneContato1;', montaCampos); 
    }
   catch(e)   {    /* ERRO...*/   } 
}
                  
function montaCampos(valores)
{
    //debugger
   var CodigoUnidade = (valores[0] == null) ? "" : valores[0].toString();
   var SiglaUnidadeNegocio = (valores[1] == null) ? "" : valores[1].toString();
   var NomeUnidadeNegocio = (valores[2] == null) ? "" : valores[2].toString();
   var CodigoUsuarioGerente = (valores[3] == null) ? "" : valores[3].toString();
   var SiglaUF = (valores[4] == null) ? "" : valores[4].toString();
   var IndicaUnidadeNegocioAtiva = (valores[5] == null) ? "" : valores[5].toString();
   var Observacoes = (valores[6] == null) ? "" : valores[6].toString();
   var NomeUsuario = (valores[7] == null) ? "" : valores[7].toString();
   var Email = (valores[8] == null) ? "" : valores[8].toString();
   var TelefoneContato1 = (valores[9] == null) ? "" : valores[9].toString();
   

   txtSigla.SetText(SiglaUnidadeNegocio);
   txtEntidade.SetText(NomeUnidadeNegocio);
   comboUF.SetValue(SiglaUF);
   //comboUF.SetText(SiglaUF);
   
   if(IndicaUnidadeNegocioAtiva == "S")
   {
        ckbEntidadAtiva.checked = true; 
   }
   else
   {
        ckbEntidadAtiva.checked = false; 
   }
   
   txtNome.SetText(NomeUsuario);
   txtEmail.SetText(Email);
   txtFone.SetText(TelefoneContato1);
   memObservacoes.SetText(Observacoes);
    hfGeral.Set("hfCodigoUnidadeNegocio",CodigoUnidade);
}


//-------------------------------------------------------------------------------------------
// Valida dados digitados pelo usuario, antes de ser inserido/atualizado.
    function validaCadastro()
    {
        //validação da Entidad 
        if(txtSigla.GetText() == "")
        {
            window.top.mostraMensagem(traducao.cadastroEntidade_a_sigla_da_entidade_deve_ser_informado, 'erro', true, false, null);
            txtSigla.Focus();
            return false;
        }        
        if(txtEntidade.GetText() == "")
        {
            window.top.mostraMensagem(traducao.cadastroEntidade_nome_da_entidade_deve_ser_informado, 'erro', true, false, null);
            txtEntidade.Focus();
            return false;
        }
        
        //validação do Usuario
        if(txtNome.GetText() == "")
        {
            window.top.mostraMensagem(traducao.cadastroEntidade_nome_do_usu_rio_deve_ser_informado, 'erro', true, false, null);
            txtNome.Focus();
            return false;
        }
        if(txtEmail.GetText() == "")
        {
            window.top.mostraMensagem(traducao.cadastroEntidade_email_do_usu_rio_deve_ser_informado, 'erro', true, false, null);
            txtEmail.Focus();
            return false;
        }
        return true;
    }
   
   /*-------------------------------------------------------------------------------------------
     setea a tabela tbAcao que contem ao girAcoes, si sera visivel o nao segundo o parametro
     estado:    block = visivel.
                        none = nao visivel.
   */ 
    function logoVisible(estado)
    {
        if (document.getElementById('tdImagem')!=null)
            document.getElementById('tdImagem').style.display = estado;
            
        if (document.getElementById('tbUploadLogo')!=null)
            document.getElementById('tbUploadLogo').style.display = estado;
    } 
   
    