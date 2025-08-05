var __cwf_delimitadorValores = "$";
var __cwf_delimitadorElementoLista = "¢";
var comando;

function onClick_btnSalvar() {
    if (window.validaCamposFormulario) {
        if (validaCamposFormulario() != "") {
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'Atencao', true, false, null);
            return false;
        }
    }

    if (window.SalvarCamposFormulario)
        SalvarCamposFormulario();
}

function onClick_btnCancelar() {
    pcDados.Hide();
    return true;
}

function onEnd_pnCallback() {
    if (hfGeral.Get("StatusSalvar") == "1") {
        if (window.posSalvarComSucesso)
            window.posSalvarComSucesso();
        else
            onClick_btnCancelar();
    }
    else if (hfGeral.Get("StatusSalvar") == "0") {
        mensagemErro = hfGeral.Get("ErroSalvar");
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
    }
}

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";

    return mensagemErro_ValidaCamposFormulario;
}

function SalvarCamposFormulario() {
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallback
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback("Editar");
    return false;
}

function LimpaCamposFormulario() {
    //txtNomeMapa.cpCodigoMapa = -1;
    //txtNomeMapa.SetText("");
    //-------------------------------
    lblNomeMapa.cpCodigoMapa = -1;
    lblNomeMapa.SetText("");

    gvEntidades.PerformCallback("POPMAP_-1");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetSelectedFieldValues('CodigoMapaEstrategico;TituloMapaEstrategico;IndicaMapaEstrategicoAtivo;DataInicioVersaoMapaEstrategico;VersaoMapaEstrategico;DataTerminoVersaoMapaEstrategico;CodigoUnidadeNegocio;', MontaCamposFormulario);
}

function MontaCamposFormulario(values) {
    /*
    0: CodigoMapaEstrategico,                   4: VersaoMapaEstrategico, 
    1: TituloMapaEstrategico,                   5: DataTerminoVersaoMapaEstrategico,
    2: IndicaMapaEstrategicoAtivo,              6: CodigoUnidadeNegocio
    3: DataInicioVersaoMapaEstrategico, 
                     
    */

    var valores = values[0];

    try {
        var codigoMapa = valores[0];
        var tituloMapa = (valores[1] != null ? valores[1] : "");
        var mapaAtivo = valores[2];
        var codigoUnidade = valores[6];

        txtMapaTitulo.SetText(tituloMapa);
        ddlMapaAtivo.SetValue(mapaAtivo);
        ddlUnidadeNegocio.SetValue(codigoUnidade);

        //txtNomeMapa.cpCodigoMapa = codigoMapa;
        //txtNomeMapa.SetText(tituloMapa);
        //-------------------------------------
        lblNomeMapa.cpCodigoMapa = codigoMapa;
        lblNomeMapa.SetText(tituloMapa);

        gvEntidades.SetFocusedRowIndex(-1);

        if (null != codigoMapa) {
            gvEntidades.PerformCallback("POPMAP_" + codigoMapa);
        }
        hfGeral.Set("CodigoMapa", codigoMapa);
        hfGeral.Set("NomeMapa", tituloMapa);

    } catch (e) { }
}

function pcDados_OnPopup(s, e) {
    OnGridFocusedRowChanged(gvMapa, true)
}

function posSalvarComSucesso() {
    mostraPopupMensagemGravacao(traducao.MapaEstrategico_permiss_es_gravadas_com_sucesso_);
}

/*-------------------------------------------------
<summary>
Mostra um popup comm um mensagem pasado como parâmetro.
Sua funçõe em conjunto con 'fechaTelaEdicao()' que fechara a popup.
</summary>
<Parameters>acao: Mensagem ao ser prenchido no popup.</Parameters>
-------------------------------------------------*/
function mostraPopupMensagemGravacao(acao) {
    window.top.mostraMensagem(acao, 'sucesso', false, false, null);

}

function fechaTelaEdicao() {
    pcMensagemGravacao.Hide();
    onClick_btnCancelar();
}



function onClickNovoMapa() {
    window.top.mostraMensagem(traducao.MapaEstrategico_novo_mapa, 'Atencao', true, false, null);
}


function onClick_CustomButtomGvDados(s, e) {
    e.processOnServer = false;
    gvMapa.SelectRowOnPage(gvMapa.GetFocusedRowIndex(), true);
    if ('btnCompartilhar' == e.buttonID) pcDados.Show();
    else if ('btnDisenhoMapa' == e.buttonID) gvMapa.GetSelectedFieldValues('CodigoMapaEstrategico;TituloMapaEstrategico;IndicaMapaCarregado', PrepararLinkMapaEstrategicoFlash);
    else if ('btnPermissoes' == e.buttonID) OnGridFocusedRowChangedPopup(gvMapa);
}

var myArgument = new Object();

function PrepararLinkMapaEstrategicoFlash(values) {
    var valores = values[0];

    var codigoMapa = valores[0] != null ? valores[0] : "-1"; //hfGeral.Contains('CodigoMapa') ? hfGeral.Get("CodigoMapa") : '-1';
    var nomeMapa = valores[1] != null ? valores[1] : "";  //hfGeral.Contains('NomeMapa') ? hfGeral.Get("NomeMapa") : '';
    var indicaMapaCarregado = valores[2] != null ? valores[2] : "N";

    var Thiswidth;
    var Thisheight;

    if (navigator.appName.indexOf("Microsoft") != -1) {
        Thiswidth = document.body.clientWidth - 20;
        Thisheight = document.documentElement.clientHeight - 60;
    }
    else {
        Thiswidth = window.innerWidth - 20;
        Thisheight = window.innerHeight - 65;
    }

    var codigoUsuario = hfGeral.Contains('CodigoUsuario') ? hfGeral.Get("CodigoUsuario") : '-1';
    var codigoEntidade = hfGeral.Contains('CodigoUnidade') ? hfGeral.Get("CodigoUnidade") : '-1';
    var link = '../mapa/ConfiguracaoNovoMapaEstrategico.aspx?cm=' + codigoMapa + '&CU=' + codigoUsuario + '&CE=' + codigoEntidade + '&NMP=' + nomeMapa;


    myArgument.param1 = nomeMapa;

    if (indicaMapaCarregado == "S")
        window.location.href = "../mapa/ConfiguracaoNovoMapaEstrategico.aspx?cm=" + codigoMapa;
    else {
        //window.top.showModal(link + '&Altura=' + (screen.height + 80), traducao.MapaEstrategico_mapa_estrat_gico, null, null, atualizaTela, myArgument);
        window.location.href = link;
    }
        
}

function atualizaTela(retorno) {
    gvMapa.PerformCallback();
}


//---------------------------------------------------------------------------
//METODOS RELACIOANDO CON A GRID MAPA ESTRATEGICO (gvMapa)
//===========================================================================

/*-------------------------------------------------
<summary>
</summary>
-------------------------------------------------*/
function onClick_btnSalvarMapa() {
    if (window.validaCamposFormulario) {
        if (validaCamposFormulario() != "") {
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'Atencao', true, false, null);
            return false;
        }
    }

    if (window.SalvarCamposFormulario) {
        if (SalvarCamposFormulario()) {
            pcDados.Hide();
            habilitaModoEdicaoBarraNavegacao(false, gvDados);
            return true;
        }
    }
    else {
        window.top.mostraMensagem(traducao.MapaEstrategico_o_m_todo_n_o_foi_implementado_, 'Atencao', true, false, null);
    }
}

/*-------------------------------------------------
<summary>
Ler o registro actual da gridView.
</summary>
<Parameters>
grid: ID da grid ao ler.
forcarMontaCampos: booleana. En seu estado true, com certeça chama ao função 'MontaCamposFormularioMapa(valores)'.
</Parameters>
-------------------------------------------------*/
function OnGridFocusedRowChangedMapa(grid, forcarMontaCampos) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetSelectedFieldValues('CodigoMapaEstrategico;TituloMapaEstrategico;IndicaMapaEstrategicoAtivo;DataInicioVersaoMapaEstrategico;VersaoMapaEstrategico;DataTerminoVersaoMapaEstrategico;CodigoUnidadeNegocio;', MontaCamposFormularioMapa);
}

/*-------------------------------------------------
<summary>
Preenche os objetos qeu contem o popup utilizado para editar um registro da gridView.
</summary>
<Parameters>valores: Array de String.</Parameters>
-------------------------------------------------*/
function MontaCamposFormularioMapa(values) {
    /*
    0: CodigoMapaEstrategico,                   4: VersaoMapaEstrategico, 
    1: TituloMapaEstrategico,                   5: DataTerminoVersaoMapaEstrategico,
    2: IndicaMapaEstrategicoAtivo,              6: CodigoUnidadeNegocio
    3: DataInicioVersaoMapaEstrategico, 
                     
    */

    var valores = values[0];

    if (null != valores) {
        var codigoMapa = valores[0];
        //txtNomeMapa.cpCodigoMapa = codigoMapa;
        //txtNomeMapa.SetText(valores[1]);
        //--------------------------------------
        lblNomeMapa.cpCodigoMapa = codigoMapa;
        lblNomeMapa.SetText(valores[1]);

        gvEntidades.SetFocusedRowIndex(-1);

        if (null != codigoMapa) {
            gvEntidades.PerformCallback("POPMAP_" + codigoMapa);
        }
        hfGeral.Set("CodigoMapa", codigoMapa);
    }
}

/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function OnGridFocusedRowChangedPopup(grid) {
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    grid.GetSelectedFieldValues('CodigoMapaEstrategico;TituloMapaEstrategico;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valores) {
    var valor = valores[0];
    var idObjeto = (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var tituloMapa = "";

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = 900;
    var window_height = 590;
    var newfeatures = 'scrollbars=no,resizable=yes';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;
    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=ME&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa + "&AlturaGrid=480", traducao.MapaEstrategico_permiss_es, 920, 585, '', null);

}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 110;
    gvMapa.SetHeight(height);
}