function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
   mensagemErro_ValidaCamposFormulario = "";
   var numAux = 0;
    var mensagem = "";

    if (Trim(txtAquisicao.GetText()) == "")
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroAquisicoes_a_descri__o_do_item_deve_ser_informada_;
  
    }
    if (ddlResponsavel.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroAquisicoes_o_respons_vel_deve_ser_informado_;
    }

    if (spnValorPrevisto.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroAquisicoes_o_valor_previsto___obrigat_rio_;
    }

    if (spnValorRealizado.GetEnabled() == true && (spnValorRealizado.GetValue() == null) || (spnValorRealizado.GetValue() == "")) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroAquisicoes_o_percentual_contratado___obrigat_rio_;
    }
    if(ddlDataPrevista.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.CadastroAquisicoes_o_prazo_m_ximo_para_contrata__o_deve_ser_informado_;
    }
    
    if(mensagem != "")
    {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }
    
    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario()
{
    ddlConta.SetValue(null);
    txtAquisicao.SetText("");
    ddlDataPrevista.SetValue(null);
    spnValorPrevisto.SetText("");
    spnValorRealizado.SetText("");
    ddlResponsavel.SetText("");
    txtGrupoAquisicao.SetText("");
    desabilitaHabilitaComponentes();
    spnValorRealizado.SetEnabled(true);
    ddlContratado.SetValue(1);
}

function onClick_btnSalvar() {
    if (window.validaCamposFormulario) {
        if (validaCamposFormulario() != "") {
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'erro', true, false, null);
            return false;
        }
        else {
            pnCallback.PerformCallback();
        }
    }
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {


        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoAquisicao;Item;DataPrevista;ValorPrevisto;PercentualContratado;CodigoResponsavel;NomeUsuario;GrupoAquisicao;Status;CodigoConta;', MontaCamposFormulario);
    
    }
}

function MontaCamposFormulario(values)
{
    var codigoAquisicao = (values[0] != null ? values[0] : "");
    var item = (values[1] != null ? values[1] : "");
    var dataPrevista = (values[2] != null ? values[2] : "");
    var valorPrevisto = (values[3] != null ? values[3] : "");
    var PercentualContratado = (values[4] != null ? values[4] : "");
    var codigoResponsavel = (values[5] != null ? values[5] : "");
    var nomeUsuario = (values[6] != null ? values[6] : "");
    var grupoAquisicao = (values[7] != null ? values[7] : "");
    var status = (values[8] != null ? values[8] : "");
    var codigoConta = (values[9] != null ? values[9] : "");

    txtAquisicao.SetText(item);
    ddlDataPrevista.SetValue((dataPrevista != "") ? dataPrevista : null);
    spnValorPrevisto.SetValue(valorPrevisto);
    spnValorRealizado.SetValue(PercentualContratado * 100);
    ddlResponsavel.SetValue(codigoResponsavel);
    ddlResponsavel.SetText(nomeUsuario);
    txtGrupoAquisicao.SetText(grupoAquisicao);
    ddlContratado.SetValue(status);//1-> sim; 2 -> Não 3-> Parcial
    ddlConta.SetValue(codigoConta);
    if (status == "Não") {
        spnValorRealizado.SetEnabled(false);
        spnValorRealizado.SetText("");
    }
//    else {
//        spnValorRealizado.SetEnabled(true);
//    }
    desabilitaHabilitaComponentes();
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    txtAquisicao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlDataPrevista.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    spnValorPrevisto.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    spnValorRealizado.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && ddlContratado.GetValue() != "Não");
    ddlResponsavel.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    txtGrupoAquisicao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlContratado.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
    ddlConta.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao()
{
    onClick_btnCancelar();
}