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

    window.top.showModal("../../espacoTrabalho/AnexosProjeto_PopUp.aspx?TA=" + hfGeral.Get("IniciaisTipoAssociacao") + "&ID=" + hfGeral.Get("IDObjetoAssociado") + "&O=" + origem + "&CPS=" + codigoPastaSuperior + "&CPA=" + codigoPastaAtual + "&MO=" + modoOperacao + "&CA=" + codigoAnexo, traducao.AnexoIndicador_anexos, 650, 265, executaPosPopUp, null);
}       


function executaPosPopUp(lParam)
{
    if(lParam == 'OK')
        pnCallback.PerformCallback("Listar");   
}

/*function mostraNovaPasta(Tipo)
{
    // se o registro selecionado tem pasta Selecionada
    if (hfGeral.Contains("CodigoPastaSuperior") && hfGeral.Get("CodigoPastaSuperior") != "-1")
    {
        rbPastaSuperior.SetVisible(true);
        rbPastaSuperior.SetText("A partir da pasta \"" + hfGeral.Get("NomePastaSuperior") + "\"" );
        rbPastaSuperior.SetValue(hfGeral.Get("CodigoPastaSuperior"));
    }
    else
        rbPastaSuperior.SetVisible(false);

    // se o registro selecionado é uma pasta
    if (lblTipo.GetText() == "Pasta")
    {
        rbPastaSelecionada.SetVisible(true);
        rbPastaSelecionada.SetText("A partir da pasta \"" + lblNomeArquivo.GetText() + "\"" );
        rbPastaSelecionada.SetValue(hfGeral.Get("CodigoAnexo"));
    }
    else
        rbPastaSelecionada.SetVisible(false);
        
    // se o registro selecionado não possui pasta superior e não é uma pasta, a única opção é "pasta raiz"
    if (rbPastaSuperior.GetVisible() == false && rbPastaSelecionada.GetVisible() == false)
        rbPastaRaiz.SetChecked(true);
    
    if (Tipo == 'Incluir')
    {
        rbPastaRaiz.SetEnabled(true);
        rbPastaSelecionada.SetEnabled(true);  
        rbPastaSuperior.SetEnabled(true); 
        txtNomePasta.SetText("");
        txtDescricaoNovaPasta.SetText("");
    }
    else
    {
        rbPastaRaiz.SetEnabled(false);
        rbPastaSelecionada.SetEnabled(false); 
        rbPastaSuperior.SetEnabled(false); 
    }
    pcNovaPasta.Show();
    txtNomePasta.Focus();
}


function mostraNovoAnexo(Tipo)
{
    // se o registro selecionado tem pasta Selecionada
    if (hfGeral.Contains("CodigoPastaSuperior") && hfGeral.Get("CodigoPastaSuperior") != "-1")
    {
        rbAnexoPastaSuperior.SetVisible(true);
        rbAnexoPastaSuperior.SetText("A partir da pasta \"" + hfGeral.Get("NomePastaSuperior") + "\"" );
        rbAnexoPastaSuperior.SetValue(hfGeral.Get("CodigoPastaSuperior"));
    }
    else
        rbAnexoPastaSuperior.SetVisible(false);

    // se o registro selecionado é uma pasta
    if (lblTipo.GetText() == "Pasta")
    {
        rbAnexoPastaSelecionada.SetVisible(true);
        rbAnexoPastaSelecionada.SetText("A partir da pasta \"" + lblNomeArquivo.GetText() + "\"" );
        rbAnexoPastaSelecionada.SetValue(hfGeral.Get("CodigoAnexo"));
    }
    else
        rbAnexoPastaSelecionada.SetVisible(false);
        
    // se o registro selecionado não possui pasta superior e não é uma pasta, a única opção é "pasta raiz"
    if (rbAnexoPastaSuperior.GetVisible() == false && rbAnexoPastaSelecionada.GetVisible() == false)
        rbAnexoPastaRaiz.SetChecked(true);
    
    if (Tipo == 'Incluir')
    {
        rbAnexoPastaRaiz.SetEnabled(true);
        rbAnexoPastaSelecionada.SetEnabled(true);  
        rbAnexoPastaSuperior.SetEnabled(true); 
        txtDescricaoNovoAnexo.SetText("");
    }
    else
    {
        rbAnexoPastaRaiz.SetEnabled(false);
        rbAnexoPastaSelecionada.SetEnabled(false); 
        rbAnexoPastaSuperior.SetEnabled(false); 
    }
    pcNovoAnexo.Show();
}

function validaNovaPasta()
{
    mensagemErro_ValidaCamposFormulario = "";
    if (txtNomePasta.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario = "O campo \"Nome da Pasta\" deve ser informado.";
        txtNomePasta.Focus();
    }
    else if (rbPastaRaiz.GetChecked() == false && rbPastaSelecionada.GetChecked() == false && rbPastaSuperior.GetChecked() == false)
    {
        mensagemErro_ValidaCamposFormulario = "O local para a criação da pasta deve ser informado.";
    }
    
    return mensagemErro_ValidaCamposFormulario;
}


function validaNovoAnexo()
{
    mensagemErro_ValidaCamposFormulario = "";
    if(document.getElementById("pnCallbackNovoAnexo_pcNovoAnexo_fluArquivo").value == "")
    {
        mensagemErro_ValidaCamposFormulario = "Selecione um Arquivo para ser anexado";
    }
    else if (rbAnexoPastaRaiz.GetChecked() == false && rbAnexoPastaSelecionada.GetChecked() == false && rbAnexoPastaSuperior.GetChecked() == false)
    {
        mensagemErro_ValidaCamposFormulario = "A pasta destino do anexo deve ser informada.";
    }
    
    return mensagemErro_ValidaCamposFormulario;
}

function onEnd_pnCallback(s)
{
    if (s.cp_StatusSalvar=="1")
    {
        txtNomePasta.SetText("");
        txtDescricaoNovaPasta.SetText("");
        txtDescricaoNovoAnexo.SetText("");
        pcNovaPasta.Hide();
    }
    else if (s.cp_StatusSalvar=="0")
    {
        window.top.mostraMensagem(s.cp_ErroSalvar, 'erro', true, false, null);
    }
}
*/
