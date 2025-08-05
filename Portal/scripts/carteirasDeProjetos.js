var mensagemErro_ValidaCamposFormulario;
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
    //var tOperacao = ""

    try {// Função responsável por preparar os campos do formulário para receber um novo registro
        hfGeral.Set("CodigoCarteira", "");
        hfGeral.Set("NomeCarteira", "");
        hfGeral.Set("memDescricaoCarteira", "");
        checkAtivo.SetChecked(true);

        txtNomeCarteiraADD.SetText("");
        memDescricaoCarteiraADD.SetText("");
        memDescricaoCarteiraADD.Validate();
        var parametro = "-1";
        //gvProjetos.PerformCallback(parametro);
        //var tipoOperacao = hfGeral.Get("TipoOperacao").toString();

        //if (tipoOperacao == "Incluir")
        //    gvProjetos.PerformCallback(parametro);

    } catch (e) { }
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDadosNovaCarteira && pcDadosNovaCarteira.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'NomeCarteira;DescricaoCarteira;CodigoCarteira;IniciaisCarteiraControladaSistema;CodigoObjeto;IndicaCarteiraAtiva;', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    try {
        txtNomeCarteiraADD.SetText(values[0] != null ? values[0] : "");
        memDescricaoCarteiraADD.SetText(values[1] != null ? values[1] : "");
        memDescricaoCarteiraADD.Validate();
        hfGeral.Set("CodigoCarteira", (values[2] != null ? values[2] : "-1"));

        var codigoCarteira = (values[2] != null ? values[2] : "-1");
        var parametro = codigoCarteira;
        var IndicaCarteiraAtiva = values[5];

        if (IndicaCarteiraAtiva == 'S')
            checkAtivoADD.SetValue(true);
        else
            checkAtivoADD.SetValue(false);

        gvProjetos.PerformCallback(parametro);

        desabilitaHabilitaComponentes();
    } catch (e) { }
}

//################ ASSOCIAÇÃO DO PROEJTO ######################
function OnGridFocusedRowChangedProjetos(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDadosProjeto && pcDadosProjeto.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'NomeCarteira;DescricaoCarteira;CodigoCarteira;IniciaisCarteiraControladaSistema;CodigoObjeto;IndicaCarteiraAtiva;', MontaCamposFormularioProjeto);
    }
}

function MontaCamposFormularioProjeto(values) {
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    LimpaCamposFormulario();

    try {


        txtNomeCarteira.SetText(values[0] != null ? values[0] : "");
        memDescricaoCarteira.SetText(values[1] != null ? values[1] : "");
        memDescricaoCarteira.Validate();
        hfGeral.Set("CodigoCarteira", (values[2] != null ? values[2] : "-1"));

        var codigoCarteira = (values[2] != null ? values[2] : "-1");
        var parametro = codigoCarteira;
        var IndicaCarteiraAtiva = values[5];

        if (IndicaCarteiraAtiva == 'S')
            checkAtivo.SetValue(true);
        else
            checkAtivo.SetValue(false);

        gvProjetos.PerformCallback(parametro);

        //desabilitaHabilitaComponentes();
    } catch (e) { alert('Deu Erro:' + e); }
}

function InserirNovaCarteira(grid) {
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'NomeCarteira;DescricaoCarteira;CodigoCarteira;IniciaisCarteiraControladaSistema;CodigoObjeto;IndicaCarteiraAtiva;', MontaCamposFormularioProjeto(grid));
    btnSalvar1.SetVisible(true);
    onClickBarraNavegacao("Incluir", gvProjetos, pcDadosProjeto);
    //desabilitaHabilitaComponentes();
}

function InserirProjetos(grid) {
    pcCheck.Show(); gvCheck.Refresh();
    pcDadosProjeto.Hide();

    //Seta a paginação da Grid para a Primeira psição

    //alert('007: Seta Page Incial');
    //gvCheck.indexOf = 0;

}

function ExcluirProjetos(grid) {
    pcExcluirCheck.Show(); gvExcluirCheck.Refresh();
    pcDadosProjeto.Hide();
}

//################ FIM ASSOCIAÇÃO DO PROEJTO ######################

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0)
        onClick_btnCancelar();
}

function validaCamposFormulario() {
    //--- Verificar que nao seja seleccionado no modo 'Inserir' ao 'gerente do Projeto'
    var tipoPapel = txtNomeCarteiraADD.GetText();
    mensagemErro_ValidaCamposFormulario = "";
    if ("" == tipoPapel) {
        mensagemErro_ValidaCamposFormulario = traducao.carteirasDeProjetos_o_campo_ + lblTitulo.cp_LabelCarteira + traducao.carteirasDeProjetos__deve_ser_informado_;
    }
    //--- alguma coisa de testing
    return mensagemErro_ValidaCamposFormulario;
}

function selecionarTodos(actVar) {
    for (i = 0; i < form1.length; i++) {
        if (form1.elements[i].type == "checkbox") {
            if (form1.elements[i].name.indexOf("checkAcoes") != -1) {
                form1.elements[i].checked = actVar;
            }
        }
    }
}

function desabilitaHabilitaComponentes() {
    var tipoOperacao = hfGeral.Get("TipoOperacao").toString();
    var BoolEnabled = false;
    if (("Incluir" == tipoOperacao) || ("Editar" == tipoOperacao))
        BoolEnabled = true;

    //--- alguma coisa de testing
    txtNomeCarteiraADD.SetEnabled(BoolEnabled);
    memDescricaoCarteiraADD.SetEnabled(BoolEnabled);
    checkAtivoADD.SetEnabled(BoolEnabled);
}

var retornoTela = null;

function novaCarteira() {
    btnSalvar1.SetVisible(true);
    onClickBarraNavegacao("Incluir", gvDados, pcDadosNovaCarteira);
    desabilitaHabilitaComponentes();
    pcDadosNovaCarteira.SetHeaderText('Inserir Nova Carteira');
}

function OnGridFocusedRowChangedPopup(grid) {
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoCarteira;NomeCarteira;DescricaoCarteira;;IniciaisCarteiraControladaSistema;CodigoObjeto;IndicaCarteiraAtiva;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor) {
    var idObjeto = (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var tituloMapa = "";

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = 900;
    var window_height = 590;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;
    window.top.showModal("../../_Estrategias/InteressadosObjeto.aspx?ITO=CP&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa, traducao.carteirasDeProjetos_permiss_es, 920, 470, '', null);
}