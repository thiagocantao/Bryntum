// JScript File

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

function MontaCamposFormulario(valores) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();
    var values = valores[0];
    if (values) {
        var codigoToDoList = values[0];
        var codigoResponsavel = values[1];
        var nomeToDoList = values[2];
        var porcentagem = values[3];
        var status = values[4];

        hfGeral.Set("codigoToDoList", (codigoToDoList != null ? codigoToDoList : ""));

        if (codigoResponsavel != null) {
            ddlResponsavel.SetValue(codigoResponsavel.toString());
            hfGeral.Set("codRespSelecionado", codigoResponsavel);
        }
        else {
            ddlResponsavel.SetValue(-1);
            hfGeral.Set("codRespSelecionado", -1);
        }
        txtDescricaoPlanoAcao.SetText((nomeToDoList != null ? nomeToDoList : ""));
        txtPorcentajeConcluido.SetText((porcentagem != null ? porcentagem : ""));
        imgStatus.SetImageUrl("../../imagens/" + status + ".gif");

        if (window.hfGeralToDoList) {
            if (window.hfGeral && hfGeral.Contains('codigoObjetoAssociado'))
                hfGeralToDoList.Set('codigoObjetoAssociado', hfGeral.Get('codigoObjetoAssociado'));
        }
        desabilitaHabilitaComponentes();
        gvToDoList.PerformCallback("Popular"); // faz um callback para preencher o ToDoList, e mostrá-lo no endCallBack
    }
}

function verificarDadosPreenchidos() {
    var mensagemError = "";
    var retorno = true;
    var numError = 0;

    if (txtDescricaoPlanoAcao.GetText() == "") {
        mensagemError += ++numError + ") " + traducao.ObjetivoEstrategico_ToDoList_a_descri__o_do_plano_de_a__o_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if (ddlResponsavel.GetText() == "") {
        mensagemError += ++numError + ") " + traducao.ObjetivoEstrategico_ToDoList_o_respons_vel_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if (!retorno)
        window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);

    return retorno;
}

function LimpaCamposFormulario() {
    txtDescricaoPlanoAcao.SetText("");
    ddlResponsavel.SetText("");
    txtPorcentajeConcluido.SetText("");
    imgStatus.SetImageUrl("../../imagens/Branco.gif");
}

function desabilitaHabilitaComponentes() {
    var BoolEnabled = hfGeral.Get("TipoOperacao") == "Consultar" ? false : true;
    btnSalvar.SetVisible(BoolEnabled);
    txtDescricaoPlanoAcao.SetEnabled(BoolEnabled);
    ddlResponsavel.SetEnabled(BoolEnabled);

}

function Click_NovaAcaoSugerida() {
    LimpaCamposFormulario();
    hfGeral.Set('TipoOperacao', 'Incluir');
    TipoOperacao = 'Incluir';
    desabilitaHabilitaComponentes();

    hfGeral.Set("codigoToDoList", -1); // valor usado durante o callback 

    if (window.hfGeralToDoList) {
        if (window.hfGeral && hfGeral.Contains('codigoObjetoAssociado'))
            hfGeralToDoList.Set('codigoObjetoAssociado', hfGeral.Get('codigoObjetoAssociado'));
    }
    gvToDoList.PerformCallback("ShowToDoList"); // faz um callback para preencher o todo list;
}

function Click_Salvar(s, e) {
    e.processOnServer = false;

    if (verificarDadosPreenchidos()) {
        if (window.onClick_btnSalvar)
            onClick_btnSalvar();
    }
    else
        return false;
}

function posSalvarComSucesso() {
    if (TipoOperacao === 'Excluir')
        window.top.mostraMensagem(traducao.ObjetivoEstrategico_ToDoList_MensagemRegistroExcluidoComSucesso, 'Sucesso', false, false, null);
    else if (TipoOperacao === 'Editar')
        window.top.mostraMensagem(traducao.ObjetivoEstrategico_ToDoList_MensagemRegistroSalvoComSucesso, 'Sucesso', false, false, null);
    onClick_btnCancelar();
}