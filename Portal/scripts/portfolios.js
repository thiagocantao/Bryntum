// JScript File  
function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var conta = 1;
    if (txtPortfolio.GetText() == "") {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.portfolios_a_descri__o_do_portf_lio_deve_ser_informada_ + "\n";
        txtPortfolio.Focus();
    }
    if (ddlGerente.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.portfolios_o_gerente_do_portf_lio_deve_ser_informado_ + "\n";
        ddlGerente.Focus();
    }
    if (ddlStatus.GetValue() == null) {
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.portfolios_o_status_do_portf_lio_deve_ser_informado_ + "\n";
        ddlStatus.Focus();
    }
    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario() {
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function LimpaCamposFormulario() {
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtPortfolio.SetText("");
    ddlGerente.SetValue("");
    ddlStatus.SetValue("");
    ddlPortfolioSuperior.SetValue("");
    ddlCarteira.SetValue("");
    callbackPortfolioSuperior.PerformCallback("");
}

function desabilitaHabilitaComponentes() {
    txtPortfolio.SetEnabled(TipoOperacao != "Consultar");
    ddlGerente.SetEnabled(TipoOperacao != "Consultar");
    ddlStatus.SetEnabled(TipoOperacao != "Consultar");
    //ddlPortfolioSuperior.SetEnabled(TipoOperacao != "Consultar");
    ddlCarteira.SetEnabled(TipoOperacao != "Consultar");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    /*,p.CodigoCarteiraAssociada
                  ,cart.NomeCarteira*/
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoPortfolio;DescricaoPortfolio;CodigoStatusPortfolio;CodigoPortfolioSuperior;CodigoUsuarioGerente;UsuarioGerente;CodigoCarteiraAssociada;NomeCarteira;PortfolioSuperior;', MontaCamposFormulario);
}

function MontaCamposFormulario(valores) {
    //LimpaCamposFormulario();

    var CodigoPortfolio = (valores[0] == null) ? "" : valores[0].toString();
    var DescricaoPortfolio = (valores[1] == null) ? "" : valores[1].toString();
    var Status = (valores[2] == null) ? "" : valores[2].toString();
    var PortfolioSuperior = (valores[3] == null) ? "" : valores[3].toString();
    var UsuarioGerente = (valores[4] == null) ? "" : valores[4].toString();
    var nomeResp = (valores[5] == null) ? "" : valores[5].toString();
    //6 e 7
    var CodigoCarteiraAssociada = (valores[6] == null) ? "" : valores[6].toString();
    var NomeCarteira = (valores[7] == null) ? "" : valores[7].toString();
    var descricaoPortfolioSuperior = (valores[8] == null) ? "" : valores[8].toString();

    txtPortfolio.SetText(DescricaoPortfolio);
    ddlGerente.SetValue(UsuarioGerente);
    ddlGerente.SetText(nomeResp)
    ddlStatus.SetValue(Status);
    ddlPortfolioSuperior.SetValue(PortfolioSuperior);
    ddlPortfolioSuperior.SetText(descricaoPortfolioSuperior);

    ddlCarteira.SetValue(CodigoCarteiraAssociada);
    ddlCarteira.SetText(NomeCarteira);
    
    callbackPortfolioSuperior.PerformCallback(CodigoPortfolio + '|' + (TipoOperacao == "" ? "Consultar" : TipoOperacao));
}

function posSalvarComSucesso() {
    mostraPopupMensagemGravacao(traducao.portfolios_portf_lio_gravado_com_sucesso_);
}

//-------
function mostraPopupMensagemGravacao(acao) {
    lblAcaoGravacao.SetText(acao);
    pcMensagemGravacao.Show();
    setTimeout('fechaTelaEdicao();', 1500);
}

function fechaTelaEdicao() {
    pcMensagemGravacao.Hide();
    onClick_btnCancelar();
}

function trataMensagemErro(TipoOperacao, mensagemErro) {
    if (TipoOperacao == "Excluir") {
        if (mensagemErro.indexOf('REFERENCE') >= 0)
            return traducao.portfolios_j__existe_projeto_associado_a_este_portf_lio__a_exclus_o_n_o_pode_ser_realizada_;
    }
    if (TipoOperacao == "Incluir") {
        return traducao.portfolios_ocorreu_uma_refer_ncia_c_clica__o_portf_lio_superior_n_o___v_lido_pois_est__acessando_outro_portf_lio_na_mesma_hierarquia_;
    }
    if (TipoOperacao == "Editar") {
        return traducao.portfolios_ocorreu_uma_refer_ncia_circular_onde_o___portf_lio_superior___informado_est__acessando_outro_portf_lio_na_mesma_hierarquia_;
    }
    else
        return "";
}