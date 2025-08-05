function abreEdicaoPlano(codigoPlano)
{
    window.top.showModal2('EdicaoPlano.aspx?CP=' + codigoPlano, traducao.listaPPA_plano_plurianual, 700, 300, function (e) { gvDados.PerformCallback() });
}