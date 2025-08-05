function abreEdicao(codigoCalendario)
{
    window.top.showModalComFooter('frm_Calendarios.aspx?TA=EN&CC=' + codigoCalendario, traducao.calendarios_cadastro_de_calend_rios, null, null, finalizaEdicao);  
}

function abreInsercao()
{
    pcNovoCalendario.Show();
}

function finalizaEdicao()
{
    gvDados.PerformCallback();
}