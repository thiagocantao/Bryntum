// JScript File
   
    
function validaCadastro()
{
    if(txtCriterio.GetText() == "")
    {
        window.top.mostraMensagem(traducao.criteriosSelecao_campo_crit_rio_deve_ser_informado_, 'Atencao', true, false, null);
        return false;
    }
    else
    {
        return true;
    }
}

function OnGridFocusedRowChanged() 
{
        //Recebe os valores dos campos de acordo com a linha selecionada. O resultado é passado para a função montaCampo
         try{
         grid.GetRowValues(grid.GetFocusedRowIndex(), 'DescricaoCriterioSelecao;CodigoCriterioSelecao;', montaCampos);
         }catch(e){}
}

function montaCampos(valores)
    { 
          
          var descricaoCriterioSelecao = (valores[0] == null) ? "" : valores[0].toString();

          txtCriterio.SetText(descricaoCriterioSelecao);
          
          painelCallback.PerformCallback('focuGrid'); 
        
           
    }


