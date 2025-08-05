function OnFocusedNodeChanged(treeList) 
{
    try
    {
        treeList.GetNodeValues(treeList.GetFocusedNodeKey(), 'CodigoAnexo;Nome;IndicaPasta;DataInclusao;NomeUsuario;DescricaoAnexo;CodigoPastaSuperior;NomePastaSuperior', MontaCamposFormularioAnexos);
    }
    catch(e){}
}

function MontaCamposFormularioAnexos(values)
{
    hfAnexos.Set("CodigoAnexo", values[0] != null ? values[0] : "");
    // registra as informações da pasta superior
    hfAnexos.Set("CodigoPastaSuperior", "-1");
    hfAnexos.Set("NomePastaSuperior", "");
    hfAnexos.Set("IndicaPasta", values[2] != null ? values[2] : "");
    if (values[6] != null)
    {
        hfAnexos.Set("CodigoPastaSuperior", values[6]);
        hfAnexos.Set("NomePastaSuperior", values[7]);
    }
}

function abrePopUp(origem, modoOperacao)
{
    var codigoPastaSuperior = "";
    var codigoPastaAtual = "";
    var codigoAnexo = "";
    
    if (hfAnexos.Contains("CodigoPastaSuperior") && hfAnexos.Get("CodigoPastaSuperior") != "-1")
        codigoPastaSuperior = hfAnexos.Get("CodigoPastaSuperior");
    if (hfAnexos.Contains("IndicaPasta") && hfAnexos.Get("IndicaPasta") == "S")
        codigoPastaAtual = hfAnexos.Get("CodigoAnexo");
    
    if (hfAnexos.Contains("IndicaPasta") && hfAnexos.Get("IndicaPasta") == "N")
        codigoAnexo = hfAnexos.Get("CodigoAnexo");
    
    if('IncluirRaiz' == modoOperacao)
    {
        codigoPastaSuperior = -1;
        codigoPastaAtual = -1;
        modoOperacao = "Incluir";
    }

    window.top.showModal("../../espacoTrabalho/AnexosProjeto_PopUp.aspx?TA=CT&ID=" + hfAnexos.Get("IDObjetoAssociado") + "&O=" + origem + "&CPS=" + codigoPastaSuperior + "&CPA=" + codigoPastaAtual + "&MO=" + modoOperacao + "&CA=" + codigoAnexo, traducao.Anexos_Contratos_anexos, 650, 265, executaPosPopUp, null);
}            


function executaPosPopUp(lParam)
{
    if(lParam == 'OK')
        pnCallback1.PerformCallback("Listar");   
}