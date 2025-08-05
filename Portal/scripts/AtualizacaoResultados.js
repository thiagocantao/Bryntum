
var valorAtualDado = null;



function OnGridFocusedRowChanged(grid) {
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoIndicador;NomeIndicador;formulaPorExtenso;Polaridade;NomeUsuario;SiglaUnidadeMedida;DescricaoPeriodicidade_PT;CodigoUsuarioResponsavel;Permissoes;CasasDecimais;CodigoUnidadeNegocio', MontaCamposFormulario);
}

var urlMetas = '';

function MontaCamposFormulario(valores) {
    try {
       var codigoIndicador = valores[0];
        var indicador       = valores[1];
        var formula         = valores[2];
        var polaridade      = valores[3];
        var nomeResponsavel = valores[4];
        var unidadeMedida   = valores[5];
        var periodicidade   = valores[6];
        var codigoResponsavel = valores[7];
        var Permissoes = (valores[8] == null ? 0 : valores[8]);
        var casasDecimais = (valores[9] == null ? 0 : valores[9]);
        var codigoUnidade = valores[10];

        var aux = (Permissoes) ? "S" : "N";
        hfGeral.Set("PermissaoLinha", aux);
        txtIndicador.SetText(indicador);
        txtFormula.SetText(formula);

        urlMetas = '../indicador/editaResultados.aspx?COIN=' + codigoIndicador + '&Permissao=' + aux + '&CasasDecimais=' + casasDecimais + '&CUN=' + codigoUnidade + '&Orig=1' + '&tipo=atualizacao-resultado&Popup=S';
        
        if(tabEdicao.activeTabIndex == 1)
            document.getElementById('frmMetas').src = urlMetas;
        
        //generar o mensagem para polaridade.
        if(polaridade == "POS"){
            txtPolaridade.SetText(traducao.AtualizacaoResultados_quanto_maior__melhor);
        }
        if(polaridade == "NEG"){
            txtPolaridade.SetText(traducao.AtualizacaoResultados_quanto_maior__pior);
        }
        txtResponsavel.SetText(nomeResponsavel);
        txtUnidadeMedida.SetText(unidadeMedida);
        txtPeriodicidade.SetText(periodicidade);
        
	    ddlAnos.PerformCallback();
    }catch(e){}
}

function LimpaCampos() {
    try {        
        
        txtIndicador.SetText('');
        txtFormula.SetText('');

        urlMetas = '';

        if (tabEdicao.activeTabIndex == 1)
            document.getElementById('frmMetas').src = urlMetas;

        txtPolaridade.SetText('');       
        txtResponsavel.SetText('');
        txtUnidadeMedida.SetText('');
        txtPeriodicidade.SetText('');
    } catch (e) { }
}



// --------------------------------------------------------------------------------
// Escreva aqui as novas funçÃµes que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------
function mostraPopupMensagemGravacao(acao)
{
    lblAcaoGravacao.SetText(acao);
    pcMensagemGravacao.Show();
    setTimeout ('fechaTelaEdicao();', 1500);
}

function fechaTelaEdicao()
{
    pcMensagemGravacao.Hide();
    pcResponsavel.Hide();
}

function verificaValorMaximoMinimo(s, e)
{
    if (valorAtualDado != null && valorAtualDado != "") {
        if (parseFloat(valorAtualDado) < parseFloat(s.GetMinValue())) {

            window.top.mostraMensagem(traducao.AtualizacaoResultados_o_valor___menor_que_o_permitido_para_o_dado__valor_m_nimo__ + s.GetMinValue(), 'erro', true, false, null);
        }
        else {
            if (parseFloat(valorAtualDado) > parseFloat(s.GetMaxValue())) {

                window.top.mostraMensagem(traducao.AtualizacaoResultados_o_valor___maior_que_o_permitido_para_o_dado__valor_m_ximo__ + s.GetMaxValue(), 'erro', true, false, null);
            }
        }
    }
    valorAtualDado = null;
}