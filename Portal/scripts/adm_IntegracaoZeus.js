function ConfirmaGeraArquivos() {
    if (confirm("ATENÇÃO!!!\n\n" + traducao.adm_IntegracaoZeus_confirma_gera__o_do_arquivo_para_exportar_para_o_zeus_ + " \n")) {
        pnLoading.Show();
        return true;
    }
    else {
        return false;
    }
}

