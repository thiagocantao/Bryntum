function incluiNovaDemanda()
{   
    window.top.showModal('./Administracao/cadastroDemanda.aspx', traducao.MinhasDemandas_inclus_o_de_demanda, 750, 400, executaPosFechar, null);
}

function detalhesDemanda(values)
{   
    window.top.showModal('./Administracao/cadastroDemanda.aspx?RO=S&CD=' + values[0], traducao.MinhasDemandas_detalhes_de_demanda, 750, 400, "", null);
}

function executaPosFechar(lParam)
{
    gvDemandas.PerformCallback(); 
}