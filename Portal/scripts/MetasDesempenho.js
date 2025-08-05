var podeFecharEdicao = 'S';
//// JScript File
//// ---- Provavelmente não será necessário alterar as duas próximas funções
//function SalvarCamposFormulario()
//{
//    return false;
//}

//function ExcluirRegistroSelecionado()
//{
//    return false;
//}


//// **************************************************************************************
//// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
//// **************************************************************************************

//function validaCamposFormulario()
//{
//    // Esta função tem que retornar uma string.
//    // "" se todas as validações estiverem OK
//    // "<erro>" indicando o que deve ser corrigido
//    mensagemErro_ValidaCamposFormulario = "";
//    
//    return mensagemErro_ValidaCamposFormulario;
//}
var valores = '';
var anos = '';
var meses = '';

function getUltima(s, e, indexColuna) {
    var valorAtual = s.GetValue() == null ? '0' : s.GetValue().toString();

    if (valorAtual == '') {
        txtMetaInformada.SetText('');
    } else {

        valores[indexColuna] = valorAtual;

        var valor = getLstArray(valores);

        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    }

    var rowIndex = gvMetas.GetFocusedRowIndex();
    var codigo = gvMetas.GetRowKey(rowIndex);
    if (codigo != null) {
        setTimeout('gvMetas.cp_Valores_' + codigo + ' = atualizaArray(valores)', 1);
    }
}

function getSoma(s, e, indexColuna) {
    var valorAtual = s.GetValue() == null ? '0' : s.GetValue().toString();

    if (valorAtual == '') {
        txtMetaInformada.SetText('');
    } else {

        valores[indexColuna] = valorAtual;

        var valor = getSumArray(valores);

        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    }

    var rowIndex = gvMetas.GetFocusedRowIndex();
    var codigo = gvMetas.GetRowKey(rowIndex);
    if (codigo != null) {
        setTimeout("atualizaArray(valores, gvMetas.cp_Valores_' + codigo + ')", 1);
    }
}

function getMinimo(s, e, indexColuna) {
    var valorAtual = s.GetValue() == null ? '0' : s.GetValue().toString();

    if (valorAtual == '') {
        txtMetaInformada.SetText('');
    } else {

        valores[indexColuna] = valorAtual;

        var valor = getMinArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    }

    var rowIndex = gvMetas.GetFocusedRowIndex();
    var codigo = gvMetas.GetRowKey(rowIndex);
    if (codigo != null) {
        setTimeout("atualizaArray(valores, gvMetas.cp_Valores_' + codigo + ')", 1);
    }
}

function getMaximo(s, e, indexColuna) {
    var valorAtual = s.GetValue() == null ? '0' : s.GetValue().toString();

    if (valorAtual == '') {
        txtMetaInformada.SetText('');
    } else {

        valores[indexColuna] = valorAtual;

        var valor = getMaxArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    }

    var rowIndex = gvMetas.GetFocusedRowIndex();
    var codigo = gvMetas.GetRowKey(rowIndex);
    if (codigo != null) {
        setTimeout("atualizaArray(valores, gvMetas.cp_Valores_' + codigo + ')", 1);
    }
}

function getMedia(s, e, indexColuna) {
    var valorAtual = s.GetValue() == null ? '0' : s.GetValue().toString();

    if (valorAtual == '') {
        txtMetaInformada.SetText('');
    } else {

        valores[indexColuna] = valorAtual;

        var valor = getAvgArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    }

    var rowIndex = gvMetas.GetFocusedRowIndex();
    var codigo = gvMetas.GetRowKey(rowIndex);
    if (codigo != null) {
        setTimeout("atualizaArray(valores, gvMetas.cp_Valores_' + codigo + ')", 1);
    }
}

function inicializaVariaveis(tipoOperacao) {
    var rowIndex = gvMetas.GetFocusedRowIndex();
    var codigo = gvMetas.GetRowKey(rowIndex);
    if (codigo != null) {
        setTimeout('setaVariaveisArray(gvMetas.cp_Valores_' + codigo + ', gvMetas.cp_Anos_' + codigo + ', gvMetas.cp_Meses_' + codigo + ', "' + tipoOperacao + '")', 1);
    }
}

function atualizaArray(valores) {
    var strArray = '';

    for (i = 0; i < valores.length; i++) {
        strArray += valores[i] + ';';
    }

    return strArray;
}

function setaVariaveisArray(valoresParam, anosParam, mesesParam, tipoOperacao) {
    valores = valoresParam.toString().split(';');
    anos = anosParam.toString().split(';');
    meses = mesesParam.toString().split(';');
    atualizaValoresMeta(tipoOperacao);
}

function atualizaValoresMeta(tipoOperacao) {
    if (tipoOperacao == 'SUM') {
        var valor = getSumArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    } else if (tipoOperacao == 'AVG') {
        var valor = getAvgArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    } else if (tipoOperacao == 'MIN') {
        var valor = getMinArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    } else if (tipoOperacao == 'MAX') {
        var valor = getMaxArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    } else if (tipoOperacao == 'STT') {
        var valor = getLstArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    } else {
        var valor = getSumArray(valores);
        if (isNaN(valor) || valor == null)
            txtMetaInformada.SetText('');
        else
            txtMetaInformada.SetText(valor.toString().replace('.', ','));
    }

}

function getLstArray(array) {
    var ultimo;

    for (i = array.length; i >= 0; i--) {
        if (array[i] != null && array[i] != "") {
            ultimo = parseFloat(array[i].toString());
            break;
        }
    }

    return ultimo;
}

function getMaxArray(array) {
    var maximo;

    for (i = 0; i < array.length; i++) {
        if (array[i] != null && array[i] != "") {
            if (maximo == null || parseFloat(array[i].toString()) > maximo)
                maximo = parseFloat(array[i].toString());
        }
    }

    return maximo;
}

function getMinArray(array) {
    var minimo;

    for (i = 0; i < array.length; i++) {
        if (array[i] != null && array[i] != "") {
            if (minimo == null || parseFloat(array[i].toString()) < minimo)
                minimo = parseFloat(array[i].toString());
        }
    }

    return minimo;
}

function getAvgArray(array) {
    var count = 0;
    var soma = 0;

    for (i = 0; i < array.length; i++) {
        if (array[i] != null && array[i] != "") {
            soma += parseFloat(array[i].toString());
            count++;
        }
    }

    return count == 0 ? null : soma / count;
}

function getSumArray(array) {
    var count = 0;
    var soma = 0;

    for (i = 0; i < array.length; i++) {
        if (array[i] != null && array[i] != "" && isNaN(array[i]) == false) {
            soma += parseFloat(array[i].toString());
            count++;
        }
    }

    return count == 0 ? null : soma;
}

//function getValoresArray(valoresArray1, valoresArray2) {

//    for (i = 0; i < valoresArray1.length; i++) {
//        valoresArray1[i] = valoresArray2[i + 1].value;
//    }

//}

function LimpaCamposFormulario()
{
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
   if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 
                'CodigoIndicador;NomeIndicador;formulaPorExtenso;Polaridade;NomeUsuario;SiglaUnidadeMedida;DescricaoPeriodicidade_PT;CodigoUsuarioResponsavel;Meta;Permissoes;CasasDecimais;CodigoUnidadeNegocio', MontaCamposFormulario);
   
}

var urlMetas = '';

function MontaCamposFormulario(valores)
{
    try{
    var codigoIndicador     = valores[0];
    var indicador           = valores[1];
    var formula             = valores[2];
    var polaridade          = valores[3];
    var nomeResponsavel     = valores[4];
    var unidadeMedida       = valores[5];
    var periodicidade       = valores[6];
    var codigoResponsavel   = valores[7];
    var meta = (valores[8] == null ? "" : valores[8]);
    var Permissoes = (valores[9] == null ? 0 : valores[9]);
    var casasDecimais = (valores[10] == null ? 0 : valores[10]);
    var codigoUnidade = valores[11];

    var aux = (Permissoes) ? "S" : "N";
    hfGeral.Set("PermissaoLinha", aux);
    hfGeral.Set("CodigoIndicador", codigoIndicador);

    urlMetas = '../indicador/editaMetas.aspx?COIN=' + codigoIndicador + '&Permissao=' + aux + '&CasasDecimais=' + casasDecimais + '&CUN=' + codigoUnidade + '&tipo=meta-desempenho' + '&Popup=S';
    
    if(tabEdicao.activeTabIndex == 1)
        document.getElementById('frmMetas').src = urlMetas;
    
    txtIndicador.SetText(indicador);
    txtFormula.SetText(formula);
    txtMeta.SetText(meta);
    
    //generar o mensagem para polaridade.
    if(polaridade == "POS"){
        txtPolaridade.SetText(traducao.MetasDesempenho_quanto_maior__melhor);
    }
    if(polaridade == "NEG"){
        txtPolaridade.SetText(traducao.MetasDesempenho_quanto_maior__pior);
    }
    
    txtResponsavel.SetText(nomeResponsavel);
    txtUnidadeMedida.SetText(unidadeMedida);
    txtPeriodicidade.SetText(periodicidade);
    
    imgEditarMeta.SetEnabled(aux == "S");
    if(aux=="N") imgEditarMeta.SetImageUrl(pastaImagens + "/botoes/editarRegDes.png");
    else imgEditarMeta.SetImageUrl(pastaImagens + "/botoes/editarReg02.PNG");

    }catch(e){}          
}


// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
//function posSalvarComSucesso()
//{   
//}


// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
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

/*-------------------------------------------------------------------
    Función: OnClick_ImagemCancelar(s, e)
    Parámetro: .
    retorno: void.
    Descripción: Cancela la ação de edição da meta do indicador.
                 Los botoes se encuentra agrupados em uma tabela, com
                 las celdas indicada pelo um id. O que faiz a função e
                 mudar seu visualização (style.display) segundo esteja
                 editando o visualizando.
-------------------------------------------------------------------*/
function OnClick_ImagemCancelar(s,e)
{
	var filaEditar = document.getElementById("tdEditarMeta");
	var filaAcao = document.getElementById("tdAcaoMeta");

	filaEditar.style.display = "";
	filaAcao.style.display = "none";

	txtMeta.SetEnabled(false);

	OnGridFocusedRowChanged(gvDados, true);
}