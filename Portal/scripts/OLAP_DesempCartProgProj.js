function abreCurvaS(valores) {
    var codigoObjeto = valores[0];
    var tipoObjeto = valores[1];
    var descricao = valores[2];
    var codigoUnidade = ddlUnidade.GetValue() == null ? -1 : ddlUnidade.GetValue();
    
    var tituloGrafico = "Curva S - " + descricao;
    window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Relatorios/GraficoDesempCartProgProj.aspx?Altura=430&Largura=' + (screen.width - 90) + '&Tipo=' + tipoObjeto + '&CG=' + codigoObjeto + '&UN=' + codigoUnidade, tituloGrafico, screen.width - 55, 490, "", null);
}

function OnGridFocusedRowChanged(grid) {
    if (grid.GetFocusedNodeKey() != null)
        grid.GetNodeValues(grid.GetFocusedNodeKey(), 'CodigoItem;TipoItem;Descricao;', abreCurvaS);
    else
        window.top.mostraMensagem(traducao.OLAP_DesempCartProgProj_nenhuma_linha_foi_selecionada_para_a_apresenta__o_da_curva_s, 'Atencao', true, false, null);
}