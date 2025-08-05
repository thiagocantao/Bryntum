function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (window.pcDados)
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'ano;meta', MontaCamposFormulario);
}

function LimpaCamposFormulario()
{
    spAno.SetValue(null);
    spValorMeta.SetValue(null);
   
}


function MontaCamposFormulario(valores) 
{
    var ano = (valores[0] == null) ? "" : valores[0].toString();
    var valorMeta = (valores[1] == null) ? "" : valores[1].toString();
    spAno.SetValue(ano);
    spValorMeta.SetValue(valorMeta);
}

function validaCamposFormulario() {

    mensagemErro_ValidaCamposFormulario = "";
    var conta = 0;
    if (spAno.GetText() == "") {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.seriesMeta_o_campo_ano_deve_ser_informado_ + "\n";
        spAno.Focus();
    }
    if (spValorMeta.GetText() == "") {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.seriesMeta_o_campo_valor_da_meta_deve_ser_informado_ + "\n";
        spValorMeta.Focus();
    }
    return mensagemErro_ValidaCamposFormulario;
}

function onClick_btnSalvar() {
    if (window.validaCamposFormulario) {
        if (validaCamposFormulario() != "") {
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'Atencao', true, false, null);
            return false;
        }
        else {
            pnCallback.PerformCallback();
        }
    }
}

function SalvarCamposFormulario() {
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    hfGeral.Set("RowFocusChanged", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}



function onEnd_pnCallbackLocal(s, e) {
    if ("Incluir" == s.cp_OperacaoOk) {
        mostraDivSalvoPublicado(traducao.seriesMeta_dados_inclu_dos_com_sucesso_);
    }
    else if ("Editar" == s.cp_OperacaoOk) {
        mostraDivSalvoPublicado(traducao.seriesMeta_dados_gravados_com_sucesso_);
    }
    else if ("Excluir" == s.cp_OperacaoOk) {
        mostraDivSalvoPublicado(traducao.seriesMeta_dados_exclu_dos_com_sucesso_);
    }

}


function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
    // oculta também a tela de perfis, caso esteja visível.
    //pcPerfis.Hide();
}


function ExcluirRegistroSelecionado() {
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    hfGeral.Set("RowFocusChanged", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}
