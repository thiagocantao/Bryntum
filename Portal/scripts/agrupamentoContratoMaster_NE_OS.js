var comando = "";
function validaCamposFormulario() {
    var contador = 0;
    var mensagemErro_ValidaCamposFormulario = "";
    if (txtDescricaoContrato.GetText() == null || txtDescricaoContrato.GetText() == "") {
        mensagemErro_ValidaCamposFormulario += contador++ + ") Descrição do contrato deve ser preenchido\r\n";
    }
    if (txtValorContratado.GetValue() == null || txtValorContratado.GetValue() == 0) {
        mensagemErro_ValidaCamposFormulario += contador++ + ") Valor Contratado deve ser preenchido\r\n";
    }        
    return mensagemErro_ValidaCamposFormulario;
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoContratoEspecial;DescricaoContrato;ValorContratado;ValorContratadoReaj;ValorRealizado;ValorRealizadoReaj', MontaCamposFormulario);
}

function LimpaCamposFormulario() {
    desabilitaHabilitaComponentes();
    txtDescricaoContrato.SetText("");
    txtValorContratado.SetText("");
    txtValorContratadoReaj.SetText("");
    txtValorRealizado.SetText("");
    txtValorRealizadoReaj.SetText("");
}

function desabilitaHabilitaComponentes() {
 
    txtDescricaoContrato.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValorContratado.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValorContratadoReaj.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValorRealizado.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtValorRealizadoReaj.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallback
    hfGeral.Set("StatusSalvar", "0");
    gvDados.PerformCallback(TipoOperacao);
    return false;
}

function MontaCamposFormulario(values) {
    LimpaCamposFormulario();    
    
    //CodigoContratoEspecial;DescricaoContrato;ValorContratado;ValorContratadoReaj;ValorRealizado;ValorRealizadoReaj
    var DescricaoContrato = (values[1] != null ? values[1] : "");
    var ValorContratado = (values[2] != null ? values[2] : "");
    var ValorContratadoReaj = (values[3] != null ? values[3] : "");
    var ValorRealizado = (values[4] != null ? values[4] : "");
    var ValorRealizadoReaj = (values[5] != null ? values[5] : "");
  
    txtDescricaoContrato.SetText(DescricaoContrato);
    
    desabilitaHabilitaComponentes();

    var habilita = ((window.TipoOperacao && TipoOperacao != "Consultar") ? "S" : "N");

    pncb_txtValorContratado.PerformCallback(ValorContratado + "|" + habilita);
    pncb_txtValorContratadoReaj.PerformCallback(ValorContratadoReaj + "|" + habilita);
    pncb_txtValorRealizado.PerformCallback(ValorRealizado + "|" + habilita);
    pncb_txtValorRealizadoReaj.PerformCallback(ValorRealizadoReaj + "|" + habilita);
    



}

