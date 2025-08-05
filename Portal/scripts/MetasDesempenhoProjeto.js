var tipoEdicao = '';
function LimpaCamposFormulario() {
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if ((window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(),
            'CodigoMetaOperacional;NomeIndicador;Agrupamento;Polaridade;NomeUsuario;SiglaUnidadeMedida;CodigoPeriodicidade;Meta;CasasDecimais;MetaNumerica;EditaPeriodicidade;CodigoIndicador;FonteIndicador;CodigoUsuarioResponsavelAtualizacao;UsuarioAtualizacao;DataInicioValidadeMeta;DataTerminoValidadeMeta;IndicaAcompanhaMetaVigencia', MontaCamposFormulario);

}

var urlMetas = '';

function MontaCamposFormulario(valores) {
    var codigoMeta = valores[0];
    var indicador = valores[1];
    var agrupamento = valores[2];
    var polaridade = valores[3];
    var nomeResponsavel = valores[4];
    var unidadeMedida = valores[5];
    var periodicidade = valores[6];
    var meta = (valores[7] == null ? "" : valores[7]);
    var casasDecimais = (valores[8] == null ? 0 : valores[8]);
    var valorMeta = (valores[9] == null ? "" : valores[9].toString().replace('.', ','));
    var codigoProjeto = gvDados.cp_CodigoProjeto;
    var editaPeriodicidade = valores[10];
    var codigoIndicador = valores[11];
    var FonteIndicador = valores[12];
    var CodigoUsuarioResponsavelResultado = valores[13];
    var nomeRespResultado = valores[14];
    var DataInicioValidadeMeta = valores[15];
    var DataTerminoValidadeMeta = valores[16];
    var IndicaAcompanhaMetaVigencia = valores[17];

    if (gvDados.GetVisibleRowsOnPage() == 1) {
        codigoMeta = gvDados.cp_codigoMeta;
        codigoIndicador = gvDados.cp_codigoIndicador;
        unidadeMedida = gvDados.cp_unidadeMedida;
        indicador = gvDados.cp_indicador;
        periodicidade = gvDados.cp_periodicidade;
        meta = gvDados.cp_meta;
    }

    urlMetas = './editaMetas.aspx?CodigoMeta=' + codigoMeta + '&CodigoIndicador=' + codigoIndicador + '&CasasDecimais=' + casasDecimais + "&Periodicidade=" + periodicidade + "&CodigoProjeto=" + codigoProjeto;

    if (tabEdicao.activeTabIndex == 1)
        document.getElementById('frmMetas').src = urlMetas;

    txtIndicador.SetText(indicador);
    txtAgrupamento.SetText(agrupamento);
    (CodigoUsuarioResponsavelResultado == null ? ddlResponsavelResultado.SetText("") : ddlResponsavelResultado.SetValue(CodigoUsuarioResponsavelResultado));
    (CodigoUsuarioResponsavelResultado == null ? ddlResponsavelResultado.SetText("") : ddlResponsavelResultado.SetText(nomeRespResultado));

    //generar o mensagem para polaridade.
    if (polaridade == "POS") {
        txtPolaridade.SetText(traducao.MetasDesempenhoProjeto_quanto_maior__melhor);
    }
    if (polaridade == "NEG") {
        txtPolaridade.SetText(traducao.MetasDesempenhoProjeto_quanto_maior__pior);
    }

    txtResponsavel.SetText(nomeResponsavel);
    txtUnidadeMedida.SetText(unidadeMedida);
    ddlPeriodicidadeCalculo.SetValue(periodicidade);
    txtMeta.SetText(meta);
    txtValorMeta.SetText(valorMeta);
    txtFonte.SetText(FonteIndicador);
    ddlInicioVigencia.SetValue(DataInicioValidadeMeta);
    ddlTerminoVigencia.SetValue(DataTerminoValidadeMeta);
    cbVigencia.SetValue(IndicaAcompanhaMetaVigencia);
    cbVigencia.SetEnabled(ddlInicioVigencia.GetValue() != null && ddlTerminoVigencia.GetValue() != null);
    verificaEdicaoPeriodicidade(editaPeriodicidade);
}

function verificaVigencia() {
    var inicio = ddlInicioVigencia.GetValue();
    var termino = ddlTerminoVigencia.GetValue();

    if (inicio == null && termino == null) {
        cbVigencia.SetEnabled(false);
        cbVigencia.SetChecked(false);
    }
    else {
        cbVigencia.SetEnabled(true);
        cbVigencia.SetChecked(true);
    }
}

function verificaEdicaoPeriodicidade(editaPeriodicidade) {
    if (editaPeriodicidade == 'S') {
        ddlPeriodicidadeCalculo.SetEnabled(true);
        document.getElementById('tdPeriodicidade').title = "";
    } else {
        ddlPeriodicidadeCalculo.SetEnabled(false);
        document.getElementById('tdPeriodicidade').title = traducao.MetasDesempenhoProjeto_o_campo_n_o_pode_ser_alterado__existem_lan_amentos_de_metas_e_ou_resultados_para_a_periodicidade_atual_;
    }
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

function foco(idElemento) {
    document.getElementById(idElemento).focus();
}

/*-------------------------------------------------------------------
    Función: lancarMensagemSistema(mensagem)
    Parámetro: cadena de texto.
    retorno: void.
    Descripción: Exibe panelControl com o texto como parâmetro.
                 O parâmetro e usado para exibir mensagens 
                 informativas ao usuário.
-------------------------------------------------------------------*/
function lancarMensagemSistema(mensagem) {
    lblMensagemVarios.SetText(mensagem);
    pcMensagemVarios.Show();
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
function OnClick_ImagemCancelar(s, e) {
    OnGridFocusedRowChanged(gvDados, true);
}

/*-------------------------------------------------------------------
    Función: OnClick_CustomEditarGvDado(s, e)
    Parámetro: .
    retorno: void.
    Descripción: Iniciar ações de edição da linha da grig gvDados.
-------------------------------------------------------------------*/
function OnClick_CustomEditarGvDado(s, e) {
    txtIndicador.SetVisible(true);
    ddlIndicador.SetVisible(false);
    ddlIndicador.SetValue(null);
    tabEdicao.GetTabByName('tbMetas').SetVisible(true);
    tipoEdicao = 'E';
    OnGridFocusedRowChanged(s, true);
    pcDados.Show();
}

var tempDeserializedItems;

function OnClick_CustomIncluirGvDado() {

    txtIndicador.SetVisible(false);
    ddlIndicador.SetVisible(true);
    try {
        if (tempDeserializedItems == null) {
            tempDeserializedItems = ddlIndicador.listBox.itemsManager.deserializedItems;
            ddlIndicador.SetValue(null);
            ddlIndicador.SetText("");
        } else {
            ddlIndicador.ClearItems();
            for (var i = 0; i < tempDeserializedItems.length; ++i) {
                var itemIndicador = { value: tempDeserializedItems[i].value, text: tempDeserializedItems[i].text };
                ddlIndicador.AddItem(itemIndicador.text, parseInt(itemIndicador.value));
            }
        }

    } catch (e) {
        console.log('Error - MetasDesempenhoProjeto.js - OnClick_CustomIncluirGvDado():', e);
    }
    tabEdicao.GetTabByName('tbMetas').SetVisible(false);
    tipoEdicao = 'I';
    limpaCampos();
    verificaEdicaoPeriodicidade("S");
    pcDados.Show();
}

function limpaCampos() {
    txtIndicador.SetText("");
    txtAgrupamento.SetText("");
    txtPolaridade.SetText("");
    txtResponsavel.SetText("");
    ddlResponsavelResultado.SetText("");
    txtUnidadeMedida.SetText("");
    ddlPeriodicidadeCalculo.SetValue(null);
    txtMeta.SetText("");
    txtValorMeta.SetText("");
    txtFonte.SetText("");
    ddlInicioVigencia.SetValue(null);
    ddlTerminoVigencia.SetValue(null);
    cbVigencia.SetEnabled(false);
    cbVigencia.SetValue("N");
}

function preencheCamposIndicador(s) {
    txtIndicador.SetText(s.cp_NomeIndicador);

    txtAgrupamento.SetText(s.cp_Agrupamento);

    txtFonte.SetText(s.cp_Fonte);

    ddlResponsavelResultado.SetValue(s.cp_CodigoResponsavelAtualizacao);
    ddlResponsavelResultado.SetText(s.cp_NomeResponsavelAtualizacao);

    //generar o mensagem para polaridade.
    if (s.cp_Polaridade == "POS") {
        txtPolaridade.SetText(traducao.MetasDesempenhoProjeto_quanto_maior__melhor);
    }
    if (s.cp_Polaridade == "NEG") {
        txtPolaridade.SetText(traducao.MetasDesempenhoProjeto_quanto_maior__pior);
    }
    else {
        txtPolaridade.SetText("");
    }

    txtResponsavel.SetText(s.cp_NomeUsuario);
    txtUnidadeMedida.SetText(s.cp_SiglaUnidadeMedida);
    ddlPeriodicidadeCalculo.SetValue(null);
    txtMeta.SetText("");
    txtValorMeta.SetText("");
}

function validaCampos() {
    mensagemErro_ValidaCamposFormulario = "";
    var numAux = 0;
    var mensagem = "";

    if (tipoEdicao == 'I' && ddlIndicador.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.MetasDesempenhoProjeto_o_indicador_deve_ser_informado_;
    }

    if (txtMeta.GetText() == "") {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.MetasDesempenhoProjeto_a_descri__o_da_meta_deve_ser_informada_;
    }

    if (ddlPeriodicidadeCalculo.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.MetasDesempenhoProjeto_a_periodicidade_da_meta_deve_ser_informada_;
    }
    if (ddlInicioVigencia.GetValue() != null && ddlTerminoVigencia.GetValue() != null) {
        var dataInicio = new Date(ddlInicioVigencia.GetValue());
        var dataTermino = new Date(ddlTerminoVigencia.GetValue());
        if (dataInicio > dataTermino) {
            mensagem += "\n" + numAux + ") " + traducao.MetasDesempenhoProjeto_a_data_de_in_cio_n_o_pode_ser_depois_da_data_de_t_rmino_ + "\n";
        }
    }
    if (mensagem != "") {
        window.top.mostraMensagem(mensagem, 'Atencao', true, false, null);
        return false;
    }

    return true;
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 60;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}