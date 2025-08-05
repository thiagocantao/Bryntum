function LimpaCamposFormulario() {
//    txtMeta.SetText('');
//    rbTipo.SetValue(null);
//    txtDescricao.SetText('');
//    txtComentarios.SetText('');
//    ddlMapa.SetValue(null);
//    ddlAssociacao.PerformCallback();
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
   if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(),
                'CodigoIndicador;NomeIndicador;SiglaUnidadeMedida;Meta;Permissoes;CasasDecimais;Tipo;CodigoObjetoEstrategia;Descricao;Comentario;CodigoMapaEstrategico', MontaCamposFormulario);
   
}

var urlMetas = '';
var urlSerie = '';

function MontaCamposFormulario(valores)
{
    try{
    var codigoIndicador     = valores[0];
    var indicador           = valores[1];
    var unidadeMedida       = valores[2];
    var meta = (valores[3] == null ? "" : valores[3]);
    var Permissoes = (valores[4] == null ? 0 : valores[4]);
    var casasDecimais = (valores[5] == null ? 0 : valores[5]);
    var tipo = valores[6];
    var codigoObjetoAssociado = valores[7] == null ? -1 : valores[7];
    var descricao = valores[8];
    var comentario = valores[9];
    var codigoMapa = valores[10]; 
    
    var aux = (Permissoes) ? "S" : "N";
    hfGeral.Set("PermissaoLinha", aux);
    hfGeral.Set("CodigoIndicador", codigoIndicador);

    urlMetas = './editaMetas.aspx?CodigoIndicador=' + codigoIndicador + '&Permissao=' + aux + '&CasasDecimais=' + casasDecimais + '&Altura=275';
    urlSerie = './seriesMeta.aspx?CodigoIndicador=' + codigoIndicador + '&Permissao=' + aux + '&CasasDecimais=' + casasDecimais + '&Altura=265';
    
    if(tabEdicao.activeTabIndex == 1)
        document.getElementById('frmMetas').src = urlMetas;

    txtIndicador.SetText(indicador);
    txtMeta.SetText(meta);
    rbTipo.SetValue(tipo);
    txtDescricao.SetText(descricao);
    txtComentarios.SetText(comentario);
    ddlMapa.SetValue(codigoMapa);

    ddlAssociacao.PerformCallback(codigoObjetoAssociado);

    }catch(e){}          
}


// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
//function posSalvarComSucesso()
//{   
//}


//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (ddlAssociacao.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CNI_GestaoMetas_deve_ser_associada_uma_macrometa_ou_uma_micrometa_;
    }
    if (Trim(txtMeta.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CNI_GestaoMetas_a_meta_deve_ser_informada_;
    }
    if (Trim(txtDescricao.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CNI_GestaoMetas_a_descri__o_deve_ser_informada_;
    }
    if (Trim(txtComentarios.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CNI_GestaoMetas_o_coment_rio_deve_ser_informado_;
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}