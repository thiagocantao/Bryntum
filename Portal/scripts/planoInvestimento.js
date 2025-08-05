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

    if (Trim(txtDescricaoPlanoInvestimento.GetText()) == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.planoInvestimento_a_descri__o_do_plano_de_investimento_deve_ser_informada_;
    }

    if (validaAno() != "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + validaAno();
    }
    if (validaData() != "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + validaData();
    }

    if (ddlStatus.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.planoInvestimento_o_status_deve_ser_informado_;
    }

    if (mensagem != "") {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function validaAno() {
    //ano deve ser maior ou igual ao ano anterior
    var retorno = "";
    var anoInt = 0;
    var anoInt = txtAno.GetValue();
    var dataAtual = new Date();
    var anoAtual = dataAtual.getFullYear();
    var anoAnterior = anoAtual - 1;

    if (anoInt >= anoAnterior) {
        retorno = "";
    }
    else {
        retorno = traducao.planoInvestimento_o_ano_informado_deve_ser_maior_ou_igual_ao_ano_anterior;
    }
    return retorno;
}

function validaData() {
    var retorno = "";
    if (dteInicio.GetValue() != null && dteFinal.GetValue() != null) {

        var dataInicio = new Date(dteInicio.GetValue());
        var meuDataInicio = (dataInicio.getMonth() + 1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
        dataInicio = Date.parse(meuDataInicio);

        var dataTermino = new Date(dteFinal.GetValue());
        var meuDataTermino = (dataTermino.getMonth() + 1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
        dataTermino = Date.parse(meuDataTermino);

        if (dataInicio > dataTermino) {
            retorno = traducao.planoInvestimento_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_;
        }
        else {
            retorno = "";
        }
    }
    else {

        if (dteInicio.GetValue() == null && dteFinal.GetValue() == null) {
            retorno = traducao.planoInvestimento_a_data_de_in_cio_e_a_data_de_t_rmino_devem_ser_informadas_;
        }
        else if (dteInicio.GetValue() == null) {
            retorno = traducao.planoInvestimento_a_data_de_in_cio_deve_ser_informada_;
        }
        else if (dteFinal.GetValue() == null) {
            retorno = traducao.planoInvestimento_a_data_de_t_rmino_deve_ser_informada_;
        }
    }
    return retorno;
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

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {

    txtDescricaoPlanoInvestimento.SetText('');
    txtAno.SetText('');
    dteInicio.SetValue(null);
    dteFinal.SetValue(null);
    ddlStatus.SetSelectedIndex(0);
    desabilitaHabilitaComponentes();
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoPlanoInvestimento;DescricaoPlanoInvestimento;Ano;DataInicioInclusaoProjeto;DataFinalInclusaoProjetos;CodigoStatusPlanoInvestimento;DescricaoStatusPlanoInvestimento;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    desabilitaHabilitaComponentes();

    var codigoPlanoInvestimento;
    var descricaoPlanoInvestimento;
    var ano;
    var dataInicioInclusaoProjeto;
    var dataFinalInclusaoProjetos;
    var codigoStatusPlanoInvestimento;
    var descricaoStatusPlanoInvestimento;


    codigoPlanoInvestimento = (values[0] != null ? values[0] : "");
    descricaoPlanoInvestimento = (values[1] != null ? values[1] : "");
    ano = (values[2] != null ? values[2] : "");
    dataInicioInclusaoProjeto = (values[3] != null ? values[3] : "");
    dataFinalInclusaoProjetos = (values[4] != null ? values[4] : "");
    codigoStatusPlanoInvestimento = (values[5] != null ? values[5] : "");
    descricaoStatusPlanoInvestimento = (values[6] != null ? values[6] : "");

    txtDescricaoPlanoInvestimento.SetText(descricaoPlanoInvestimento);
    txtAno.SetText(ano);
    dteInicio.SetValue(dataInicioInclusaoProjeto);
    dteFinal.SetValue(dataFinalInclusaoProjetos);
    ddlStatus.SetValue(codigoStatusPlanoInvestimento);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    txtDescricaoPlanoInvestimento.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtAno.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    dteInicio.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    dteFinal.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlStatus.SetEnabled(false/*window.TipoOperacao && TipoOperacao != "Consultar"*/);
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}