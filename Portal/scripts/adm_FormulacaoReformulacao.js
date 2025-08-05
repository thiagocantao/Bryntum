
function ConfirmaInicioDesbloqueio() {
    if (confirm(traducao.adm_FormulacaoReformulacao_aten__o___ + "\n\n" + traducao.adm_FormulacaoReformulacao_confirma_execu__o_do_procedimento_de_in_cio_do_desbloqueio_de_projetos_ + "\n")) {
        window.parent.parent.hfGeral.Set('podeIniciarDesbloqueio', 'S');
        mostraEscondeElementos("Desbloqueio");
    }
    else
        window.parent.parent.hfGeral.Set('podeIniciarDesbloqueio', 'N');
}

function ConfirmaLimpa() {
    if (confirm(traducao.adm_FormulacaoReformulacao_aten__o___ + "\n\n" + traducao.adm_FormulacaoReformulacao_confirma_exclus_o_de_dados_da__ltima_reformula__o_efetuada_ + "\n")) 
    {
        LoadingPanel.Show();
        return true;
    }
    else {
        return false;
    }
}



function ConfirmaReformulacao() {
    if (confirm(traducao.adm_FormulacaoReformulacao_aten__o___ + "\n\n" + traducao.adm_FormulacaoReformulacao_confirma_gera__o_de_arquivos_e_in_cio_da_reformula__o_ + "\n"))
    {
        LoadingPanel.Show();
        return true;
    }
    else
    {
        return false;
    }
}

function mostraEscondeElementos(acaoParam) {

    lblMostraMensagem.SetVisible(false);
    if (acaoParam == "Desbloqueio") {
        gvDados.SetVisible(true);
        gvDadosGeraArquivo.SetVisible(false);

    }
    if (acaoParam == "Reformulação") {
        gvDadosGeraArquivo.SetVisible(true);
        gvDados.SetVisible(false);
    }
    
    //bool mostra = (mostraGrid == "S") ? true : false;
    //    document.getElementById("tdcomboMes").style.display = "none";
    //    document.getElementById("tdLblMes").style.display = "none";

    //   

    //    }
    //    else if (acaoParam == "Desbloqueio") {
    //        gvDados.SetVisible(true);
    //        gvDadosGeraArquivo.SetVisible(false);
    //        AspxbuttonInicioReformulacao.SetVisible(true);
    //        AspxbuttonReformulacao.SetVisible(false);
    //    } else if (acaoParam == "Término") {
    //        gvDados.SetVisible(false);
    //        gvDadosGeraArquivo.SetVisible(false);
    //        AspxbuttonInicioReformulacao.SetVisible(true);
    //        AspxbuttonReformulacao.SetVisible(false);
    //    } else if (acaoParam == "Tudo") {
    //        gvDadosGeraArquivo.SetVisible(false);
    //        AspxbuttonReformulacao.SetVisible(false);
    //        AspxbuttonInicioReformulacao.SetVisible(true);
    //        gvDados.SetVisible(false);
    //        gvDados.SetVisible(false);
    //        gvDadosGeraArquivo.SetVisible(false);
    //    }
}


       