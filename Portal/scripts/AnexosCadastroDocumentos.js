// JScript File
function OnFocusedNodeChanged(treeList) 
{
    treeList.GetNodeValues(treeList.GetFocusedNodeKey(), 'CodigoAnexo;Nome;IndicaPasta;DataInclusao;NomeUsuario;DescricaoAnexo;CodigoPastaSuperior;NomePastaSuperior', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
    hfGeral.Set("CodigoAnexo", values[0] != null ? values[0] : "");
    // registra as informações da pasta superior
    hfGeral.Set("CodigoPastaSuperior", "-1");
    hfGeral.Set("NomePastaSuperior", "");
    hfGeral.Set("IndicaPasta", values[2] != null ? values[2] : "");
    if (values[6] != null)
    {
        hfGeral.Set("CodigoPastaSuperior", values[6]);
        hfGeral.Set("NomePastaSuperior", values[7]);
    }
}

function abrePopUp(origem, modoOperacao)
{
    var codigoPastaSuperior = "";
    var codigoPastaAtual = "";
    var codigoAnexo = "";
    
    if (hfGeral.Contains("CodigoPastaSuperior") && hfGeral.Get("CodigoPastaSuperior") != "-1")
        codigoPastaSuperior = hfGeral.Get("CodigoPastaSuperior");
    if (hfGeral.Contains("IndicaPasta") && hfGeral.Get("IndicaPasta") == "S")
        codigoPastaAtual = hfGeral.Get("CodigoAnexo");
    
    if (hfGeral.Contains("IndicaPasta") && hfGeral.Get("IndicaPasta") == "N")
        codigoAnexo = hfGeral.Get("CodigoAnexo");
    
    if('IncluirRaiz' == modoOperacao)
    {
        codigoPastaSuperior = -1;
        codigoPastaAtual = -1;
        modoOperacao = "Incluir";
    }

    window.top.showModal("../espacoTrabalho/AnexosProjeto_PopUp.aspx?TA=" + hfGeral.Get("IniciaisTipoAssociacao") + "&ID=" + hfGeral.Get("IDObjetoAssociado") + "&O=" + origem + "&CPS=" + codigoPastaSuperior + "&CPA=" + codigoPastaAtual + "&MO=" + modoOperacao + "&CA=" + codigoAnexo, traducao.AnexosCadastroDocumentos_anexos, 650, 280, executaPosPopUp, window);
                
}

function executaPosPopUp(lParam)
{
    if(lParam == 'OK')
        pnCallback.PerformCallback("Listar");   
}