// JScript File
var somenteLeituraExecucao = "N";
var frmAnexos = '';
var atualizarURLAnexos = '';
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
    // Função responsável por preparar os campos do formulário para receber um novo registro
    hfGeral.Set("codigoEvento", "");
    
    //[TabA] - Reunião
    txtAssunto.SetText("");
    txtHoraInicio.SetText("");
    txtHoraTermino.SetText("");
    txtAssunto.SetText("");
    
    
    memoLocal.SetText("");
    //memoPauta.SetHtml("");

    ddlResponsavelEvento.SetValue(null);
    ddlResponsavelEvento.SetText("");
    ddlResponsavelEvento.SetSelectedIndex(-1);
    //ddlResponsavelEvento.filterStrategy.FilteringBackspace();
    ddlResponsavelEvento.PerformCallback();

    ddlTipoEvento.SetValue(null);
    ddlTipoEvento.SetText("");

    ddlInicioPrevisto.SetValue(null);//SetValue("");//new Date());
    ddlInicioPrevisto.SetText("");//SetValue("");//new Date());

    ddlTerminoPrevisto.SetValue(null);//SetValue("");//new Date());
    ddlTerminoPrevisto.SetText("");//SetValue("");//new Date());

    //[TabB] - Participantes
    //[TabC] - Projetos
            
    if(window.TipoOperacao && "Incluir" == TipoOperacao)
    {
        hfGeral.Set("CodigosSelecionados", "");   
        gvProjetos.PerformCallback("-1");
        txtAssunto.SetFocus();
    }
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetSelectedFieldValues('CodigoEvento;DescricaoResumida;NomeResponsavel;InicioPrevisto;inicioPrevistoData;inicioPrevistoHora;TerminoPrevisto;TerminoPrevistoData;TerminoPrevistoHora;InicioReal;InicioRealData;InicioRealHora;TerminoReal;TerminoRealData;TerminoRealHora;CodigoTipoAssociacao;CodigoObjetoAssociado;LocalEvento;Pauta;ResumoEvento;CodigoTipoEvento;CodigoObjetoAssociado;CodigoResponsavelEvento', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values)
{
    try
    {
        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        
        /*
        0- CodigoEvento;                8- TerminoPrevistoHora;     16- CodigoObjetoAssociado;
        1- DescricaoResumida;           9- InicioReal;              17- LocalEvento;
        2- CodigoResponsavelEvento;     10- InicioRealData;         18- Pauta;
        3- InicioPrevisto;              11- InicioRealHora;         19- ResumoEvento;
        4- inicioPrevistoData;          12- TerminoReal;            20- CodigoTipoEvento;
        5- inicioPrevistoHora;          13- TerminoRealData;        21- CodigoObjetoAssociado
        6- TerminoPrevisto;             14- TerminoRealHora;
        7- TerminoPrevistoData;         15- CodigoTipoAssociacao;
        */
        LimpaCamposFormulario();
        
        if (values[0]) {
            var codigoEvento = (values[0][0] != null ? values[0][0] : "");

            hfGeral.Set("codigoEvento", codigoEvento);

            var horaInicio = (values[0][5] != null ? values[0][5].substring(0, 5) : "");
            var horaTermino = (values[0][8] != null ? values[0][8].substring(0, 5) : "");

            txtAssunto.SetText((values[0][1] != null ? values[0][1] : ""));
            memoLocal.SetText((values[0][17] != null ? values[0][17] : ""));

            (values[0][22] == null ? ddlResponsavelEvento.SetValue(null) : ddlResponsavelEvento.SetText(values[0][22]));
            (values[0][2] == null ? ddlResponsavelEvento.SetText("") : ddlResponsavelEvento.SetText(values[0][2]));
            (values[0][20] == null ? ddlTipoEvento.SetText("") : ddlTipoEvento.SetValue(values[0][20]));

            (values[0][3] == null ? ddlInicioPrevisto.SetText("") : ddlInicioPrevisto.SetValue(values[0][3]));
            txtHoraInicio.SetText(horaInicio);
            (values[0][6] == null ? ddlTerminoPrevisto.SetText("") : ddlTerminoPrevisto.SetValue(values[0][6]));
            txtHoraTermino.SetText(horaTermino);

            if (window.memoPauta)
                memoPauta.SetHtml((values[0][18] != null ? values[0][18] : ""));

            //funções transferidas para o evento tabControl.activepagechanged do cliente
            //por motivos de performance.
            desabilitaHabilitaComponentes();
            lbDisponiveis.PerformCallback(codigoEvento);
            lbSelecionados.PerformCallback(codigoEvento);
            gvProjetos.PerformCallback(codigoEvento);

        }
        else {
        }
    } catch (e) {
    }
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
    pcMensagemPauta.Hide();
    if (gvDados.GetVisibleRowsOnPage() > 0 )
        onClick_btnCancelar();    
}

// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------
function novaReuniao()
{
    btnSalvar1.SetVisible(true);
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
    desabilitaHabilitaComponentes();
    var parametro = "-1";  
    hfGeral.Set("CodigosSelecionados", "");   
    lbDisponiveis.PerformCallback(parametro);
    lbSelecionados.PerformCallback(parametro);
    gvProjetos.PerformCallback(parametro);    
    if (hfGeral.Get("TipoOperacao") == "Incluir") {
        LimpaCamposFormulario();
    }
}

function desabilitaHabilitaComponentes()
{
    try {
    //var tipoOperacaoSel = TipoOperacao; hfGeral.Get("TipoOperacao");
    var OperacaoSel = hfGeral.Get("TipoOperacao").toString();
    var BoolEnabled = (OperacaoSel == "Editar" || OperacaoSel == "Incluir") ? true : false;
    
    if ("Incluir" == OperacaoSel || "Consultar" == OperacaoSel) {
        btnEnviarPauta.SetVisible(false);
    }
    else {
        btnEnviarPauta.SetVisible(true);
    }
        
	/*if("Incluir" == OperacaoSel)
	    btnNovaReuniao.SetVisible(false);
    else
        btnNovaReuniao.SetVisible(true);*/
        
    txtAssunto.SetEnabled(BoolEnabled);
    txtHoraInicio.SetEnabled(BoolEnabled);
    txtHoraTermino.SetEnabled(BoolEnabled);
    memoPauta.SetEnabled(BoolEnabled);
    memoLocal.SetEnabled(BoolEnabled);    
    ddlResponsavelEvento.SetEnabled(BoolEnabled);
    ddlTipoEvento.SetEnabled(BoolEnabled);
    ddlInicioPrevisto.SetEnabled(BoolEnabled);
    ddlTerminoPrevisto.SetEnabled(BoolEnabled);

    lbDisponiveis.SetEnabled(BoolEnabled);
    lbSelecionados.SetEnabled(BoolEnabled);
    lbGrupos.SetEnabled(BoolEnabled);
        verificaEnvioPauta();
    if("Consultar" == OperacaoSel)
    {
        btnADD.SetEnabled(false);
        btnADDTodos.SetEnabled(false);
        btnRMV.SetEnabled(false);
        btnRMVTodos.SetEnabled(false);
    }
    }catch(e){}
}

function podeMudarAba(s, e) {

    if (hfGeral.Get("TipoOperacao") == "Incluir") {
        if (e.tab.index == 3) {
            window.top.mostraMensagem(traducao.reunioes_para_ter_acesso_a_op__o + " \"" + e.tab.GetText() + "\" " + traducao.reunioes___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + tabControl.GetTab(0).GetText() + "\"", traducao.reunioes_atencao, true, false, null);
            window.setTimeout(function () {
                tabControl.SetActiveTab(tabControl.GetTabByName("TabA"));
            }, 500);
            return false;
        }
    }

    if (e.tab.name == "tabAnexos")
        getValoresAnexo();
    return false;

}

function getValoresAnexo() {
    var OperacaoSel = hfGeral.Get("TipoOperacao").toString();
    var codigoEvento = hfGeral.Get("codigoEvento");
    var altura = hfGeral.Get("alturaTela");
    var readOnly = (OperacaoSel == "Editar" || OperacaoSel == "Incluir") ? 'N' : 'S';
    hfGeral.Set("TipoOperacao", "Editar");
    atualizarURLAnexos = 'S';
    frmAnexos = '../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=RE&ID=' + codigoEvento + '&RO=' + readOnly + '&ALT=' + altura + '&TO=' + hfGeral.Get("TipoOperacao").toString() + '&Popup=S'; // Parâmetro POPUP=1 deve ser usado quando os anexos forem aparecer dentro da popup em um formulário de edição, geralmente dentro de uma aba. Esse parâmetro faz com que o botão Fechar não seja exibido e elimina as margens (padding) laterais.
    document.getElementById('frmAnexosReuniao').src = frmAnexos;
    document.getElementById('frmAnexosReuniao').height = 380;

}

function getValoresPlanoAcao()
{
    var codigoEvento = hfGeral.Get("codigoEvento");
    if (window.hfGeralToDoList)
    {
        hfGeral.Set("TipoOperacao", TipoOperacao);
        hfGeral.Set("codigoObjetoAssociado", codigoEvento );
        hfGeralToDoList.Set("codigoObjetoAssociado", codigoEvento );
        gvToDoList.PerformCallback("Popular");
    }
    else
        window.top.mostraMensagem(traducao.reuniaoTecnicaPlanejamento_n_o_foi_poss_vel_encontrar_o_componente_todolist, 'Atencao', true, false, null);
}

//------------------------------------------------------------funções relacionadas com a ListBox
var delimitador = ";";

function UpdateButtons() 
{
    try
    { 
            btnADD.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbDisponiveis.GetSelectedItem() != null);
            btnADDTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbDisponiveis.GetItemCount() > 0 );
            btnRMV.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbSelecionados.GetSelectedItem() != null);
            btnRMVTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbSelecionados.GetItemCount() > 0);
    }catch(e){}
}

function capturaCodigosInteressados()
{
    var CodigosProjetosSelecionados = "";
    for (var i = 0; i < lbSelecionados.GetItemCount(); i++) 
    {
        CodigosProjetosSelecionados += lbSelecionados.GetItem(i).value + ";";
    }
    hfGeral.Set("CodigosSelecionados", CodigosProjetosSelecionados);
}
//--------------------***********




function verificarDadosPreenchidos()
{
    var mensagemError = "";
    var retorno = true;
    var numError = 0;
    //------------Obtendo data e hora actual
    var momentoActual = new Date();
    var horaActual    = momentoActual.getHours() + ':' + momentoActual.getMinutes();
	var arrayHoraAgora = horaActual.split(':');

	var meuDataAtual  = (momentoActual.getMonth() + 1) + '/' + momentoActual.getDate() + '/' + momentoActual.getFullYear();
	var dataHoje 	  = Date.parse(meuDataAtual);

	var arrayHoraEstInicio = txtHoraInicio.GetText().split(':');
	var arrayHoraEstFinal  = txtHoraTermino.GetText().split(':');
   
    
    
    //------------- ***
    
    if(txtAssunto.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_o_assunto_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if(ddlResponsavelEvento.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_o_respons_vel_pela_reuni_o_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if(ddlTipoEvento.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_o_tipo_de_reuni_o_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if(ddlInicioPrevisto.GetValue() == null)
    {
        mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_a_data_de_in_cio_da_reuni_o_deve_ser_informada_ + "\n";
        retorno = false;
    }

    if(ddlTerminoPrevisto.GetValue() == null)
    {
        mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_a_data_de_t_rmino_da_reuni_o_deve_ser_informada_ + "\n";
        retorno = false;
    }

    if(memoLocal.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_a_descri__o_do_local_da_reuni_o_deve_ser_informada_ + "\n";
        retorno = false;
    }

    if(window.memoPauta)
    {
        if(memoPauta.GetHtml() == "")
        {
            mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_a_pauta_da_reuni_o_deve_ser_informada_ + "\n";
            retorno = false;
        }
    }

//----------- PAUTA

    if(arrayHoraEstInicio[0] > arrayHoraEstFinal[0])
    {
        mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_a_hora_de_in_cio_indicada_n_o_pode_ser_superior___hora_de_t_rmino_ + "\n";
        retorno = false;
    } 

    if(arrayHoraEstInicio[0] == arrayHoraEstFinal[0])
    {
        if(arrayHoraEstInicio[1] > arrayHoraEstFinal[1])
        {
            mensagemError += ++numError + ") " + traducao.reuniaoTecnicaPlanejamento_a_hora_de_in_cio_indicada_n_o_pode_ser_superior___hora_de_t_rmino_ + "\n";
            retorno = false;
        }
    } 


	if (!retorno)
	    window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);

	return retorno;
}

function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}

var popup = null;

function abreExecucao(valor)
{    
    var tipoReuniao = gvDados.cp_tipoReuniao;

    if(popup != null && !popup.closed)
    {
        if(confirm(traducao.reuniaoTecnicaPlanejamento_j__existe_uma_reuni_o_aberta_para_execu__o__ao_abrir_uma_nova_janela_todos_os_dados_ser_o_perdidos__deseja_continuar_))
        {   
            popup = window.open("./reuniaoTecnicaRealizacao.aspx?COR=" + valor[0][0] + '&TR=' + tipoReuniao + "&RO=" + somenteLeituraExecucao, "frameReuniao", "menubar=no, scrollbars=1");
            if(popup != null)
                popup.focus();
        }
    }
    else
    {
        popup = window.open("./reuniaoTecnicaRealizacao.aspx?COR=" + valor[0][0] + '&TR=' + tipoReuniao + "&RO=" + somenteLeituraExecucao, "frameReuniao", "menubar=no, scrollbars=1");
        if(popup != null)
            popup.focus();     
    }
}

function mostraDivAtualizando(acao) {
    lblAcaoGravacao.SetText(acao);
    pcUsuarioIncluido.Show();

    //setTimeout('fechaTelaEdicao();', 10);
}

function fechaTelaEdicao() {
    pcUsuarioIncluido.Hide();
}

function enviaPautaDeReuniao()
{
    tipoEnvio = "EnviarPauta";
    pnCallback.PerformCallback(tipoEnvio);
}

function SalvaEEnviaPauta() {
    capturaCodigosInteressados();
    if (verificarDadosPreenchidos()) {
        tipoEnvio = "EnviarPauta";
        pnCallback.PerformCallback(tipoEnvio);
    }
}

function verificaEnvioPauta() {
    //var tipoOperacaoSel = TipoOperacao; hfGeral.Get("TipoOperacao");
    var OperacaoSel = hfGeral.Get("TipoOperacao").toString();

    if (OperacaoSel == "Editar") {
        if (tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1) {
            var dataInicioPrevisto = new Date(ddlInicioPrevisto.GetValue());
            var dataHoje = new Date();

            if (dataHoje > dataInicioPrevisto) {
                btnEnviarPauta.SetEnabled(false);
                var buttonElement = btnEnviarPauta.GetMainElement();
                buttonElement.title = traducao.reunioes_o_envio_da_pauta_s___permitido_para_reuni_es_com_data_futura;
            }
            else {
                btnEnviarPauta.SetEnabled(true);
                var buttonElement1 = btnEnviarPauta.GetMainElement();
                buttonElement1.title = '';
            }
        }
        else {
            btnEnviarPauta.SetEnabled(true);
            var buttonElement1 = btnEnviarPauta.GetMainElement();
            buttonElement1.title = '';
        }
    }
}