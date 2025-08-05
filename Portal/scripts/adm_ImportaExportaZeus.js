function ConfirmaExportacao(s, e) {
    if (confirm(traducao.adm_ImportaExportaZeus_aten__o___ + "\n\n" + traducao.adm_ImportaExportaZeus_confirma_execu__o_do_procedimento_de_exportar_dados_para_o_zeus_ + "\n")) {
        window.parent.parent.hfGeral.Set('podeExportar', 'S');
        pnLoading.Show();
    }
    else
        window.parent.parent.hfGeral.Set('podeExportar', 'N');
}

function ConfirmaImportacao(s, e) {
    if (confirm(traducao.adm_ImportaExportaZeus_aten__o___ + "\n\n" + traducao.adm_ImportaExportaZeus_confirma_execu__o_do_procedimento_de_importar_os_dados_do_zeus_ + "\n")) {
        window.parent.parent.hfGeral.Set('podeImportar', 'S');
        pnLoading.Show();
    }
    else
        window.parent.parent.hfGeral.Set('podeImportar', 'N');
}
function ConfirmaGerarArquivo(s, e) {
    if (confirm(traducao.adm_ImportaExportaZeus_aten__o___ + "\n\n" + traducao.adm_ImportaExportaZeus_confirma_execu__o_do_procedimento_de_gerar_arquivo_do_cronograma_or_ament_rio_ + "\n")) {
        window.parent.parent.hfGeral.Set('podeGerarArquivo', 'S');
        pnLoading.Show();
    }
    else
        window.parent.parent.hfGeral.Set('podeGerarArquivo', 'N');
}

