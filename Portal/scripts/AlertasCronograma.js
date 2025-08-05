// JScript File

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
    var tOperacao = ""

    try {// Função responsável por preparar os campos do formulário para receber um novo registro

        txtNomeRegra.SetText("");
        ck001.SetChecked(false);
        ck002.SetChecked(false);
        ck003.SetChecked(false);
        ck004.SetChecked(false);
        ck005.SetChecked(false);
        limpaCampos001();
        limpaCampos002();
        limpaCampos003();
        limpaCampos004();
        limpaCampos005();
        
    }catch(e){}
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(),'DescricaoAlerta;DiasAntecedenciaInicio1;DiasIntervaloRecorrenciaInicio2;DiasAntecedenciaInicio2;DiasIntervaloRecorrenciaInicio3;DiasIntervaloRecorrenciaTermino;DiasIntervaloRecorrenciaAtraso;MensagemAlertaInicio1;MensagemAlertaInicio2;MensagemAlertaInicio3;MensagemAlertaTermino;MensagemAlertaAtraso;IndicaAlertaInicio1;IndicaAlertaInicio2;IndicaAlertaInicio3;IndicaAlertaTermino;IndicaAlertaAtraso', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values)
{
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    try
    {
        txtNomeRegra.SetText((values[0] != null ? values[0] : ""));
        txt001.SetText((values[1] != null ? values[1] : ""));
        txt002.SetText((values[2] != null ? values[2] : ""));
        txt003.SetText((values[3] != null ? values[3] : ""));
        txt004.SetText((values[4] != null ? values[4] : ""));
        txt005.SetText((values[5] != null ? values[5] : ""));
        txt006.SetText((values[6] != null ? values[6] : ""));
        txtDescricao001.SetText((values[7] != null ? values[7] : ""));
        txtDescricao002.SetText((values[8] != null ? values[8] : ""));
        txtDescricao003.SetText((values[9] != null ? values[9] : ""));
        txtDescricao004.SetText((values[10] != null ? values[10] : ""));
        txtDescricao005.SetText((values[11] != null ? values[11] : ""));
        ck001.SetChecked((values[12] == 'S' ? true : false));
        ck002.SetChecked((values[13] == 'S' ? true : false));
        ck003.SetChecked((values[14] == 'S' ? true : false));
        ck004.SetChecked((values[15] == 'S' ? true : false));
        ck005.SetChecked((values[16] == 'S' ? true : false));
        
      }catch(e){}
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0 )
        onClick_btnCancelar();
}

function desabilitaHabilitaComponentes()
{
    var BoolEnabled = hfGeral.Get("TipoOperacao") == "Editar" || hfGeral.Get("TipoOperacao") == "Incluir" ? true : false;

    txtNomeRegra.SetEnabled(BoolEnabled);
    ck001.SetEnabled(BoolEnabled);
    ck004.SetEnabled(BoolEnabled);
    ck005.SetEnabled(BoolEnabled);
    habilitaAlerta001();
    habilitaAlerta004();
    habilitaAlerta005();
}

function showDetalhe()
{
    OnGridFocusedRowChanged(gvDados, true);
    btnSalvar.SetVisible(false);
    hfGeral.Set("TipoOperacao", "Consultar");
    pcDados.Show();
}

function verificarDadosPreenchidos() {
    var mensagemError = "";
    var retorno = true;
    var numError = 0;

    if (txtNomeRegra.GetText() == "") {
        mensagemError += ++numError + ") " + traducao.AlertasCronograma_o_nome_do_alerta_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if (ck001.GetChecked()) {

        if (txt001.GetText() == "" || txt001.GetText() == null) {
            mensagemError += ++numError + ") " + traducao.AlertasCronograma_informe_quantos_dias_antes_que_a_tarefa_se_inicie_dever__ser_enviado_o_alerta_ + "\n";
            retorno = false;
        }
    }

    if (ck002.GetChecked()) {

        if (txt002.GetText() == "" || txt002.GetText() == null) {
            mensagemError += ++numError + ") " + traducao.AlertasCronograma_informe_a_recorr_ncia_em_dias_que_o_alerta_dever__ser_enviado_antes_que_a_tarefa_se_inicie_ + "\n";
            retorno = false;
        }

        if (txt003.GetText() == "" || txt003.GetText() == null) {
            mensagemError += ++numError + ") " + traducao.AlertasCronograma_pend_ncia_no_item_2__informe_quantos_dias_antes_que_a_tarefa_se_inicie_dever__ser_parado_de_enviar_o_alerta_recorrente_ + "\n";
            retorno = false;
        } else {
            if (txt001.GetText() != "" && txt001.GetText() != null && txt001.GetValue() <= txt003.GetValue()) {
                mensagemError += ++numError + ") " + traducao.AlertasCronograma_a_antecend_ncia_do_item_2_n_o_pode_ser_maior_ou_igual___antecend_ncia_do_item_1_ + "\n";
                retorno = false;
            }

            if (txt002.GetText() != "" && txt002.GetText() != null && txt002.GetValue() >= txt003.GetValue()) {
                mensagemError += ++numError + ") " + traducao.AlertasCronograma_pend_ncia_no_item_2__a_recorr_ncia_n_o_pode_ser_maior_ou_igual___antecend_ncia_ + "\n";
                retorno = false;
            }
        }

    }

    if (ck003.GetChecked()) {

        if (txt004.GetText() == "" || txt004.GetText() == null) {
            mensagemError += ++numError + ") " + traducao.AlertasCronograma_pend_ncia_no_item_3__informe_a_recorr_ncia_em_dias_que_o_alerta_dever__ser_enviado_antes_que_a_tarefa_se_inicie_ + "\n";
            retorno = false;
        }
        else {
            if (txt003.GetText() != "" && txt003.GetText() != null && txt003.GetValue() <= txt004.GetValue()) {
                mensagemError += ++numError + ") " + traducao.AlertasCronograma_a_recorr_ncia_do_item_3_n_o_pode_ser_maior_ou_igual___antecend_ncia_do_item_2_ + "\n";
                retorno = false;
            }
        }
    }    
    

    if (ck004.GetChecked()) {

        if (txt005.GetText() == "" || txt005.GetText() == null) {
            mensagemError += ++numError + ") " + traducao.AlertasCronograma_pend_ncia_no_item_4__informe_a_recorr_ncia_em_dias_que_o_alerta_dever__ser_enviado_antes_que_a_tarefa_se_encerre_ + "\n";
            retorno = false;
        }
    }

    if (ck005.GetChecked()) {

        if (txt006.GetText() == "" || txt006.GetText() == null) {
            mensagemError += ++numError + ") " + traducao.AlertasCronograma_pend_ncia_no_item_5__informe_a_recorr_ncia_em_dias_que_o_alerta_dever__ser_enviado_caso_a_tarefa_esteja_atrasada_ + "\n";
            retorno = false;
        }
    }

    if (!retorno)
        window.top.mostraMensagem(mensagemError, 'erro', true, false, null);

    return retorno;
}

function habilitaAlerta001() {

    habilita = ck001.GetEnabled() && ck001.GetChecked();

    txt001.SetEnabled(habilita);
    txtDescricao001.SetEnabled(habilita);    
    habilitaAlerta002(habilita);
    habilitaAlerta003(habilita);

    if (habilita) {
        document.getElementById('tb002').style.backgroundColor = '#FFFFFF';
        document.getElementById('tb003').style.backgroundColor = '#FFFFFF';
        document.getElementById('tdSeparador').style.backgroundColor = '#FFFFFF';
    }
    else {
        document.getElementById('tb002').style.backgroundColor = '#EBEBEB';
        document.getElementById('tb003').style.backgroundColor = '#EBEBEB';
        document.getElementById('tdSeparador').style.backgroundColor = '#EBEBEB';
    }

}

function habilitaAlerta002(habilitaCK) {
    ck002.SetEnabled(habilitaCK);
    habilitaCampos = ck002.GetEnabled() && ck002.GetChecked();
    txt002.SetEnabled(habilitaCampos);
    txt003.SetEnabled(habilitaCampos);
    txtDescricao002.SetEnabled(habilitaCampos);    
}

function habilitaAlerta003(habilitaCK) {
    ck003.SetEnabled(habilitaCK);
    habilitaCampos = ck003.GetEnabled() && ck003.GetChecked();    
    txt004.SetEnabled(habilitaCampos);
    txtDescricao003.SetEnabled(habilitaCampos);
}

function habilitaAlerta004() {
    habilitaCampos = ck004.GetEnabled() && ck004.GetChecked();
    txt005.SetEnabled(habilitaCampos);
    txtDescricao004.SetEnabled(habilitaCampos);
}

function habilitaAlerta005() {
    habilitaCampos = ck005.GetEnabled() && ck005.GetChecked();
    txt006.SetEnabled(habilitaCampos);
    txtDescricao005.SetEnabled(habilitaCampos);
}

function limpaCampos001() {
    txt001.SetText("");
    txtDescricao001.SetText("");
}

function limpaCampos002() {
    txt002.SetText("");
    txt003.SetText("");
    txtDescricao002.SetText("");

    if (ck001.GetChecked() == false)
        ck002.SetChecked(false);
}

function limpaCampos003() 
{
    txt004.SetText("");
    txtDescricao003.SetText("");

    if(ck001.GetChecked() == false)
        ck003.SetChecked(false);
}

function limpaCampos004() {
    txt005.SetText("");
    txtDescricao004.SetText("");
}

function limpaCampos005() {
    txt006.SetText("");
    txtDescricao005.SetText("");
}

function clickAssociacaoTarefas() {
    gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoAlerta;', abreAssociacaoTarefas);
}

function abreAssociacaoTarefas(values) {

    var codigoAlerta = values[0];

    var url = './ListaTarefasAlertas.aspx?CA=' + codigoAlerta + '&CP=' + gvDados.cp_CodigoProjeto;

    window.top.showModal(url, traducao.AlertasCronograma_lista_de_tarefas_associadas, 900, 490, '', null);
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 15;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
