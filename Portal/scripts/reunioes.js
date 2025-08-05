// JScript File
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
    hfGeral.Set("CodigoEvento", "");
    
    //[TabA] - Reunião
    txtAssunto.SetText("");
    txtHoraInicio.SetText("");
    txtHoraTermino.SetText("");
    
    memoLocal.SetText("");
    try {
        memoPauta.SetHtml("");
    } catch (e) {
        console.log(e.message);
    }
    
    ddlResponsavelEvento.SetText("");
    ddlTipoEvento.SetText("");
    
    ddlInicioPrevisto.SetValue(null);//SetValue("");//new Date());
    ddlTerminoPrevisto.SetValue(null); //SetValue("");//new Date());
    
    ddlInicioPrevisto.SetText(""); //SetValue("");//new Date());
    ddlTerminoPrevisto.SetText(""); //SetValue("");//new Date());
    
    //[TabB] - Participantes
    //[TabC] - Projetos
    //[TabD] - Ata
    txtHoraInicioAta.SetText("");
    txtHoraTerminoAta.SetText("");
    
    ddlInicioReal.SetText("");//ddlInicioReal.SetValue(null);//new Date());
    ddlTerminoReal.SetText(""); //ddlTerminoReal.SetValue(null);//new Date());
    ddlInicioReal.SetValue(null); //SetValue("");//new Date());
    ddlTerminoReal.SetValue(null); //SetValue("");//new Date());
    
    try {
        memoAta.SetHtml("");
    } catch (e) {
        console.log(e.message);
    }
    //[TabE] - Tarefas - grid criado por Antonio.
    if(window.TipoOperacao)
        if("Incluir" == TipoOperacao)
            hfGeral.Set("CodigosSelecionados", "");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos)
    {
        grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
        grid.GetSelectedFieldValues('CodigoEvento;DescricaoResumida;CodigoResponsavelEvento;InicioPrevisto;inicioPrevistoData;inicioPrevistoHora;TerminoPrevisto;TerminoPrevistoData;TerminoPrevistoHora;InicioReal;InicioRealData;InicioRealHora;TerminoReal;TerminoRealData;TerminoRealHora;CodigoTipoAssociacao;CodigoObjetoAssociado;LocalEvento;Pauta;ResumoEvento;CodigoTipoEvento;CodigoObjetoAssociado;', MontaCamposFormulario);
    }
    //LimpaCamposFormulario();
}

function MontaCamposFormulario(values)
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

        var url = '../../Reunioes/reunioes1_popup.aspx';
        url += '?idProjeto=' + valor[1];
        url += '&CR=' + valor[0];
        url += '&RO=' + somenteLeitura;
        url += '&TO=' + hfGeral.Get("TipoOperacao").toString();
        popup = window.top.showModal(url, traducao.reunioes_reuni_es, 500, 400, atualizaPosPopup, null);
             
        //lpCarregando.Show();
        //btnEnviarPauta.SetEnabled(false);
        //var buttonElement = btnEnviarPauta.GetMainElement();
        //buttonElement.title = traducao.reunioes_o_envio_da_pauta_s___permitido_para_reuni_es_com_data_futura;
        //btnSalvar.SetEnabled(false);
        //var readOnly = hfGeral.Get("TipoOperacao").toString() == "Consultar" ? 'S' : 'N';
        //btnImprimir.SetVisible(hfGeral.Get("TipoOperacao").toString() == "Consultar");
        //var codigoEvento = (values[0][0] != null ? values[0][0] : "");
        //hfGeral.Set("CodigoEvento", codigoEvento);
        //var altura = pcDados.GetContentHeight() - 115;

        //atualizarURLAnexos = 'S';
        //frmAnexos = '../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=RE&ID=' + codigoEvento + '&RO=' + readOnly + '&ALT=' + (altura + 30) + '&TO=' + hfGeral.Get("TipoOperacao").toString();

        //if (pcDados.GetVisible() && tabControl.GetActiveTabIndex() == 4) {
        //    document.getElementById('frmAnexosReuniao').src = frmAnexos;
        //}

        //document.getElementById('frmAnexosReuniao').height = altura + 48;

        //var horaInicio = (values[0][5] != null ? values[0][5].substring(0, 5) : "");
        //var horaTermino = (values[0][8] != null ? values[0][8].substring(0, 5) : "");
        //var horaInicioAta = (values[0][11] != null ? values[0][11].substring(0, 5) : "");
        //var horaTerminoAta = (values[0][14] != null ? values[0][14].substring(0, 5) : "");

        //txtAssunto.SetText((values[0][1] != null ? values[0][1] : ""));
        //memoLocal.SetText((values[0][17] != null ? values[0][17] : ""));

        //(values[0][2] == null ? ddlResponsavelEvento.SetText("") : ddlResponsavelEvento.SetValue(values[0][2]));
        //(values[0][20] == null ? ddlTipoEvento.SetText("") : ddlTipoEvento.SetValue(values[0][20]));

        //(values[0][3] == null ? ddlInicioPrevisto.SetText("") : ddlInicioPrevisto.SetValue(values[0][3]));
        //txtHoraInicio.SetText(horaInicio);
        //(values[0][6] == null ? ddlTerminoPrevisto.SetText("") : ddlTerminoPrevisto.SetValue(values[0][6]));
        //txtHoraTermino.SetText(horaTermino);

        //(values[0][9] == null ? ddlInicioReal.SetValue(null) : ddlInicioReal.SetValue(values[0][9]));
        //txtHoraInicioAta.SetText(horaInicioAta);
        //(values[0][12] == null ? ddlTerminoReal.SetValue(null) : ddlTerminoReal.SetValue(values[0][12]));
        //txtHoraTerminoAta.SetText(horaTerminoAta);
        //hfGeral.Set("CodigoEventoAtual", (values[0][0] != null ? values[0][0] : "-1"));
        //if (window.memoAta)
        //    memoAta.SetHtml((values[0][19] != null ? values[0][19] : ""));
        //if (window.memoPauta)
        //    memoPauta.SetHtml((values[0][18] != null ? values[0][18] : ""));

        ////hfGeral.Set("codigoObjetoAssociado", codigoEvento);
        //if (tabControl.GetActiveTab().name == "TabD")
        //    getValoresPlanoAcao();     
            
        //if (ddlTerminoReal.GetValue() != null && ddlTerminoReal.GetText() != "")
        //    tabControl.SetActiveTab(tabControl.GetTabByName('TabC'));    

        //lbDisponiveis.PerformCallback(codigoEvento);
        //lbSelecionados.PerformCallback(codigoEvento);
    }
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0)
        onClick_btnCancelar();
}

// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------
function novaReuniao(idProjeto)
{    
    
    var url = '../../Reunioes/reunioes1_popup.aspx';
    url += '?idProjeto=' + idProjeto;
    url += '&CE=-1';
    url += '&RO=N';
    url += '&TO=Incluir';
    url += '&MOD=PRJ';

    var altura = window.top.innerHeight - 130;

    var largura = window.top.innerWidth - 50;
    popup = window.top.showModal(url, traducao.reunioes_reuni_es, largura, altura, atualizaPosPopup, null);

}
function editaReuniao(codigoEvento) {
    var url = '../../Reunioes/reunioes1_popup.aspx';
    url += '?idProjeto=' + hfGeral.Get("CodigoProjetoAtual");
    url += '&CE=' + codigoEvento;
    url += '&RO=N';
    url += '&TO=Editar';
    url += '&MOD=PRJ';


    var altura = window.top.innerHeight - 130;

    var largura = window.top.innerWidth - 50;
    popup = window.top.showModal(url, traducao.reunioes_reuni_es, largura, altura, atualizaPosPopup, null);
}

function visualizaReuniao(codigoEvento) {
    var url = '../../Reunioes/reunioes1_popup.aspx';
    url += '?idProjeto=' + hfGeral.Get("CodigoProjetoAtual");
    url += '&CE=' + codigoEvento;
    url += '&RO=S';
    url += '&TO=Editar';
    url += '&MOD=PRJ';
    var altura = window.top.innerHeight - 130;
    var largura = window.top.innerWidth - 50;
    popup = window.top.showModal(url, traducao.reunioes_reuni_es, largura, altura, atualizaPosPopup, null);
}

function atualizaPosPopup() {
    gvDados.Refresh();
}

function desabilitaHabilitaComponentes()
{
    //var tipoOperacaoSel = TipoOperacao; hfGeral.Get("TipoOperacao");
    var OperacaoSel = hfGeral.Get("TipoOperacao").toString();
	var BoolEnabled = (OperacaoSel == "Editar" || OperacaoSel == "Incluir") ? true : false;
	
   if("Incluir" == OperacaoSel || "Consultar" == OperacaoSel)
        btnEnviarPauta.SetVisible(false);
   else
        btnEnviarPauta.SetVisible(true);
        
	/*if("Incluir" == OperacaoSel)
	    btnNovaReuniao.SetVisible(false);
    else
        btnNovaReuniao.SetVisible(true);*/
        
    txtAssunto.SetEnabled(BoolEnabled);
    txtHoraInicio.SetEnabled(BoolEnabled);
    txtHoraTermino.SetEnabled(BoolEnabled);
    txtHoraInicioAta.SetEnabled(BoolEnabled);
    txtHoraTerminoAta.SetEnabled(BoolEnabled);

    memoLocal.clientEnabled = BoolEnabled;
    memoPauta.clientEnabled = BoolEnabled;
    memoAta.clientEnabled = BoolEnabled;

    memoLocal.SetEnabled(BoolEnabled);
    memoPauta.SetEnabled(BoolEnabled);
    memoAta.SetEnabled(BoolEnabled);


    ddlResponsavelEvento.SetEnabled(BoolEnabled);
    ddlTipoEvento.SetEnabled(BoolEnabled);
    ddlInicioPrevisto.SetEnabled(BoolEnabled);
    ddlTerminoPrevisto.SetEnabled(BoolEnabled);
    ddlInicioReal.SetEnabled(BoolEnabled);
    ddlTerminoReal.SetEnabled(BoolEnabled);
    
    lbDisponiveis.SetEnabled(BoolEnabled);
    lbSelecionados.SetEnabled(BoolEnabled);
    lbGrupos.SetEnabled(BoolEnabled);
    
    
    if("Consultar" == OperacaoSel)
    {
        btnADD.SetEnabled(false);
        btnADDTodos.SetEnabled(false);
        btnRMV.SetEnabled(false);
        btnRMVTodos.SetEnabled(false);
    }
}

function podeMudarAba(s, e)
{
    if (e.tab.index <=1)
        return false;


    if (hfGeral.Get("CodigoEvento") == "-1" || hfGeral.Get("CodigoEvento") == "")
    {
        window.top.mostraMensagem(traducao.reunioes_para_ter_acesso_a_op__o + " \"" + e.tab.GetText() + "\" " + traducao.reunioes___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + tabControl.GetTab(0).GetText() + "\"", traducao.reunioes_atencao, true, false, null);
        return true;
    }
    
    if(!window.TipoOperacao)
        TipoOperacao = "Consultar";
    
    else if (e.tab.name=="TabD")
       getValoresPlanoAcao();
    
    return false;
}

function getValoresPlanoAcao()
{
    var codigoEvento = hfGeral.Get("CodigoEvento");
    if (window.hfGeralToDoList)
    {
        hfGeral.Set("TipoOperacao", TipoOperacao);
        hfGeral.Set("codigoObjetoAssociado", codigoEvento );
        hfGeralToDoList.Set("codigoObjetoAssociado", codigoEvento );
        gvToDoList.PerformCallback("Popular");
    }
    else
        window.top.mostraMensagem(traducao.reunioes_n_o_foi_poss_vel_encontrar_o_componente_todolist, 'Atencao', true, false, null);
}

//------------------------------------------------------------funções relacionadas com a ListBox
var delimitador = ";";

function UpdateButtons() {
    try {

        btnADD.SetEnabled(window.TipoOperacao && TipoOperacao != "" && TipoOperacao != "Consultar" && lbDisponiveis.GetSelectedItem() != null);
        btnADDTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "" && TipoOperacao != "Consultar" && lbDisponiveis.GetItemCount() > 0);
        btnRMV.SetEnabled(window.TipoOperacao && TipoOperacao != "" && TipoOperacao != "Consultar" && lbSelecionados.GetSelectedItem() != null);
        btnRMVTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "" && TipoOperacao != "Consultar" && lbSelecionados.GetItemCount() > 0);
        capturaCodigosInteressados();
    } catch (e) { }
}

function UpdateButtonsPopup() {
    var somenteLeitura = hfGeral.Get("SomenteLeitura").toString();

    btnADD.SetEnabled(somenteLeitura != "S" && lbDisponiveis.GetSelectedItem() != null);
    btnADDTodos.SetEnabled(somenteLeitura != "S" && lbDisponiveis.GetItemCount() > 0);
    btnRMV.SetEnabled(somenteLeitura != "S" && lbSelecionados.GetSelectedItem() != null);
    btnRMVTodos.SetEnabled(somenteLeitura != "S" && lbSelecionados.GetItemCount() > 0);
    capturaCodigosInteressados();
}



function capturaCodigosInteressados()
{
    var CodigosProjetosSelecionados = "";
    for (var i = 0; i < lbSelecionados.GetItemCount(); i++) 
    {
        CodigosProjetosSelecionados += lbSelecionados.GetItem(i).value + delimitador;
    }
    hfGeral.Set("CodigosSelecionados", CodigosProjetosSelecionados);
}
//--------------------***********

function verificarDadosAta()
{
    var mensagemError = "";
    var retorno = true;
    var numError = 0;
    
    //------------Obtendo data e hora actual
    var momentoActual = new Date();
    var horaActual    = momentoActual.getHours() + ":" + momentoActual.getMinutes();
	var arrayHoraAgora = horaActual.split(':');
	var meuDataAtual  = (momentoActual.getMonth() + 1) + "/" + momentoActual.getDate() + "/" + momentoActual.getFullYear();
	var dataHoje 	  = Date.parse(meuDataAtual);
    
    var dataInicioReal = new Date(ddlInicioReal.GetValue());
    var dataInicioRealP = (dataInicioReal.getMonth() + 1) + "/" + dataInicioReal.getDate() + "/" + dataInicioReal.getFullYear();
    var dataInicioRealC = Date.parse(dataInicioRealP);
    
    var dataTerminoReal = new Date(ddlTerminoReal.GetValue());
    var dataTerminoRealP = (dataTerminoReal.getMonth() + 1) + "/" + dataTerminoReal.getDate() + "/" + dataTerminoReal.getFullYear();
    var dataTerminoRealC = Date.parse(dataTerminoRealP);
    
	var arrayMomentoInicio = txtHoraInicioAta.GetText().split(':');
	var arrayMomentoFinal  = txtHoraTerminoAta.GetText().split(':');
	//------------- ***
	//---- Datas para comparação ****
	var dataInicioComparaP = (momentoActual.getMonth() + 1) + "/" + momentoActual.getDate() + "/" + (momentoActual.getFullYear() - 5);
	var dataInicioComparaMostra = momentoActual.getDate() + "/" + (momentoActual.getMonth()) + "/" + (momentoActual.getFullYear() - 5);
	var dataInicioComparaC = Date.parse(dataInicioComparaP);

	var dataTerminoCompara = new Date(ddlTerminoReal.GetValue());
	var dataTerminoComparaP = (dataTerminoCompara.getMonth() + 1) + "/" + dataTerminoCompara.getDate() + "/" + dataTerminoCompara.getFullYear();
	var dataTerminoComparaMostra = (dataTerminoCompara.getMonth() + 1) + "/" + dataTerminoCompara.getDate() + "/" + dataTerminoCompara.getFullYear();
	var dataTerminoComparaC = Date.parse(dataTerminoComparaP);

    //------***********
    
    if(ddlInicioReal.GetValue() == null)
    {
        mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_real_da_reuni_o_deve_ser_informada_ + "\n";
        retorno = false;
    }

    if(ddlTerminoReal.GetValue() == null)
    {
        mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_t_rmino_real_da_reuni_o_deve_ser_informada_ + "\n";
        retorno = false;
    }
    
    if(dataHoje < dataInicioRealC)
    {
        mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_real_da_reuni_o_n_o_pode_ser_maior_que_a_data_atual_ + "\n";
        retorno = false;
    }
    
    if(dataHoje < dataTerminoRealC)
    {
        mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_t_rmino_real_da_reuni_o_n_o_pode_ser_maior_que_a_data_atual_ + "\n";
        retorno = false;
    }
   
    if(dataInicioRealC > dataTerminoRealC)
    {
        mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_real_da_reuni_o_n_o_pode_ser_maior_que_a_data_de_t_rmino_real_ + "\n";
        retorno = false;
    }
    
    
    
    
    if(dataInicioRealP == dataTerminoRealP)
    {        
        if(arrayMomentoFinal[0] < arrayMomentoInicio[0])
        {
            mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_real_indicada_n_o_pode_ser_superior___hora_de_t_rmino_real__horas__ + "\n";
            retorno = false;
        }
        if(arrayMomentoFinal[0] == arrayMomentoInicio[0])
        {
            if(arrayMomentoFinal[1] < arrayMomentoInicio[1])
            {
                mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_real_indicada_n_o_pode_ser_superior___hora_atual__minutos__ + "\n";
                retorno = false;
            }
        }


    }
    
    if(dataTerminoRealP == dataHoje)
    {
        if(arrayHoraAgora[0] < arrayMomentoInicio[0])
        {
            mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_real_indicada_n_o_pode_ser_superior___hora_atual__horas__ + "\n";
            retorno = false;

        }
        if(arrayHoraAgora[0] == arrayMomentoInicio[0])
        {
            if(arrayHoraAgora[1] < arrayMomentoInicio[1])
            {
                mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_real_indicada_n_o_pode_ser_superior___hora_atual__minutos__ + "\n";
                retorno = false;
            }
        }

        if(arrayHoraAgora[0] < arrayMomentoFinal[0])
        {
            mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_t_rmino_real_indicada_n_o_pode_ser_superior___hora_atual__horas__ + "\n";
            retorno = false;

        }
        if(arrayHoraAgora[0] == arrayMomentoFinal[0])
        {
            if(arrayHoraAgora[1] < arrayMomentoFinal[1])
            {
                mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_t_rmino_real_indicada_n_o_pode_ser_superior___hora_atual__minutos__ + "\n";
                retorno = false;
            }
        }
    }
        
    if(window.memoAta)
    {
        if(memoAta.GetHtml() == "")
        {
            mensagemError += ++numError + ") " + traducao.reunioes_a_ata_da_reuni_o_deve_ser_informada_ + "\n";
            retorno = false;
        }
    }
    
	if (!retorno)
	    window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);

	return retorno;
}


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
	var dataHoje = Date.parse(meuDataAtual);

	var data

	
	var dataInicioComparaP = (momentoActual.getMonth()+1) + "/" + momentoActual.getDate() + "/" + (momentoActual.getFullYear() - 5);
	var dataInicioComparaMostra = momentoActual.getDate() + "/" + (momentoActual.getMonth()) + "/" + (momentoActual.getFullYear() - 5);
	var dataInicioComparaC = Date.parse(dataInicioComparaP);

	var dataTerminoCompara = new Date(ddlInicioPrevisto.GetValue());
	var dataTerminoComparaP = (dataTerminoCompara.getMonth()+1) + "/" + dataTerminoCompara.getDate() + "/" + dataTerminoCompara.getFullYear();
	var dataTerminoComparaMostra = (dataTerminoCompara.getMonth()+1) + "/" + dataTerminoCompara.getDate() + "/" + dataTerminoCompara.getFullYear();
	var dataTerminoComparaC = Date.parse(dataTerminoComparaP);

    //------------- ***
    
    if(txtAssunto.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.reunioes_o_assunto_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if (ddlResponsavelEvento.GetText() == "" || ddlResponsavelEvento.GetText() === ddlResponsavelEvento.GetValue())
    {
        mensagemError += ++numError + ") " + traducao.reunioes_o_respons_vel_pela_reuni_o_deve_ser_informado_ + "\n";
        ddlResponsavelEvento.SetValue(null);
        retorno = false;
    }

    if(ddlTipoEvento.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.reunioes_o_tipo_de_reuni_o_deve_ser_informado_ + "\n";
        retorno = false;
    }

    if (ddlInicioPrevisto.GetText() == "" || ddlInicioPrevisto.GetDate() == null) {
        mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_da_reuni_o_deve_ser_informada_ + "\n";
        retorno = false;
    } else {

        if (ddlInicioPrevisto.GetDate() < dataInicioComparaC) {
            mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_da_reuni_o_n_o_pode_ser_inferior_a + " [" + dataInicioComparaMostra + "]!\n";
            retorno = false;
        }
    }


    if (ddlTerminoPrevisto.GetText() == "" || ddlTerminoPrevisto.GetDate() == null)
    {
        mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_t_rmino_da_reuni_o_deve_ser_informada_ + "\n";
        retorno = false;
    } else {

        if (ddlTerminoPrevisto.GetDate() < dataInicioComparaC && ddlInicioPrevisto.GetDate() >= dataInicioComparaC) {
            mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_t_rmino_da_reuni_o_n_o_pode_ser_inferior_a + " [" + dataTerminoComparaMostra + "]!\n";
            retorno = false;
        }
    }

    if(memoLocal.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.reunioes_a_descri__o_do_local_da_reuni_o_deve_ser_informada_ + "\n";
        retorno = false;
    }

    if(window.memoPauta)
    {
        if(memoPauta.GetHtml() == "")
        {
            mensagemError += ++numError + ") " + traducao.reunioes_a_pauta_da_reuni_o_deve_ser_informada_ + "\n";
            retorno = false;
        }
    }

    //----------- PAUTA
    //---coletando dados da reunion

    var dataInicioPrevisto = new Date(ddlInicioPrevisto.GetValue());
    var dataInicioPrevistoP = (dataInicioPrevisto.getMonth() + 1) + "/" + dataInicioPrevisto.getDate() + "/" + dataInicioPrevisto.getFullYear();
    var dataInicioPrevistoC = Date.parse(dataInicioPrevistoP);
    
    var dataTerminoPrevisto = new Date(ddlTerminoPrevisto.GetValue());
    var dataTerminoPrevistoP = (dataTerminoPrevisto.getMonth() + 1) + "/" + dataTerminoPrevisto.getDate() + "/" + dataTerminoPrevisto.getFullYear();
    var dataTerminoPrevistoC = Date.parse(dataTerminoPrevistoP);

	var arrayHoraPrevistoInicio = txtHoraInicio.GetText().split(':');
	var arrayHoraPrevistoFinal = txtHoraTermino.GetText().split(':');


	

    if(dataInicioPrevistoP == dataTerminoPrevistoP)
    {
        if(arrayHoraPrevistoInicio[0] > arrayHoraPrevistoFinal[0])
        {
            mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_indicada_na_reuni_o_n_o_pode_ser_superior___hora_de_t_rmino__horas__ + "\n";
            retorno = false;
        } 

        if(arrayHoraPrevistoInicio[0] == arrayHoraPrevistoFinal[0])
        {
            if(arrayHoraPrevistoInicio[1] > arrayHoraPrevistoFinal[1])
            {
                mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_indicada_na_reuni_o_n_o_pode_ser_superior___hora_de_t_rmino__minutos__ + "\n";
                retorno = false;
            }
        }
    }
    else if(dataInicioPrevistoC > dataTerminoPrevistoC)
    {
        mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_inicio_indicada_na_reuni_o_n_o_pode_ser_maior___data_de_t_rmino_ + "\n";
        retorno = false;
    }

    if (!retorno) {
        window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);
        return false;
    }

    //----------- ATA
    //---coletando dados da ATA
    if (ddlInicioReal.GetValue() != null || ddlTerminoReal.GetValue() != null || memoAta.GetHtml() != "") {
        retorno = verificarDadosAta();
    }
    //----------- ***

	return retorno;
}

function verificaEnvioPauta()
{
    //var tipoOperacaoSel = TipoOperacao; hfGeral.Get("TipoOperacao");
    var OperacaoSel = hfGeral.Get("TipoOperacao").toString();
    
    if(OperacaoSel == "Editar")
    {
        if(tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1)
	    {
            var dataInicioPrevisto = new Date(ddlInicioPrevisto.GetValue());
            var dataHoje = new Date();
            	
	        if(dataHoje > dataInicioPrevisto)
	        {
                btnEnviarPauta.SetEnabled(false);
                var buttonElement = btnEnviarPauta.GetMainElement();
                buttonElement.title = 'O envio da pauta só é permitido para reuniões com data futura.';
	        }
	        else
	        {
                btnEnviarPauta.SetEnabled(true);
                var buttonElement1 = btnEnviarPauta.GetMainElement();
                buttonElement1.title = '';
	        }
	    }
	    else
	    {
            btnEnviarPauta.SetEnabled(true);
            var buttonElement1 = btnEnviarPauta.GetMainElement();
            buttonElement1.title = '';
	    }
    }
}

function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}

function mostraDivAtualizando(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    //setTimeout('fechaTelaEdicao();', 10);
}

function abrejanelaImpressaoReuniao(codigoProjeto, codigoEvento, moduloSistema) {
    var url = '../Reunioes/imprimeAtaDeReuniao.aspx?';

    url += 'CP=' + codigoProjeto;
    url += '&CE=' + codigoEvento;
    //url += '&MOD=' + moduloSistema;
   var w = screen.width;
   var h = screen.height;
   var left = (w - (w - 300)) / 2;
   var top = (h - (h - 300)) / 2;
   window.open(url, 'frmRelReuniao', 'menubar=no,titlebar=no,status=yes,scrollbars=yes,resizable=yes,width=' + (w - 300) + ',height=' + (h - 300) + ',left=' + left + ',top=' + top + '', false);
    //window.open(url, 'Ocupantes', 'menubar=no;titlebar=no;scrollbars=yes', true);
}

function buscaNoArray(arr, obj) 
{
    var retorno = false;
    var i = 0;
    for (i = 0; i < arr.length; i++) {
        if (arr[i] == obj) {
            retorno = true;
        }
    }
    return retorno;
}

var popup = null;

function abreExecucao(valor) 
{
    w = screen.width;
    h = screen.height;
    var altura = h - 290;
    var largura = w - 290;
    //                               '           0;                1;
    // s.GetRowValues(e.visibleIndex,'CodigoEvento;DescricaoResumida;CodigoResponsavelEvento;InicioPrevisto;inicioPrevistoData;inicioPrevistoHora;TerminoPrevisto;TerminoPrevistoData;TerminoPrevistoHora;InicioReal;InicioRealData;InicioRealHora;TerminoReal;TerminoRealData;TerminoRealHora;CodigoTipoAssociacao;CodigoObjetoAssociado;LocalEvento;Pauta;ResumoEvento;CodigoTipoEvento;CodigoObjetoAssociado;',abreExecucao);
    var tipoReuniao = gvDados.cp_tipoReuniao;
    if (popup != null && !popup.closed) {
        if (confirm(traducao.reunioes_j__existe_uma_reuni_o_aberta_para_execu__o__ao_abrir_uma_nova_janela_todos_os_dados_ser_o_perdidos__deseja_continuar_)) {
            var url = '../../Reunioes/reunioes1_popup.aspx';
            url += '?idProjeto=' + valor[1];
            url += '&CR=' + valor[0];
            url += '&RO=' + somenteLeitura;
            url += '&ALT=' + altura;
            url += '&LARG=' + largura;
            url += '&TO=' + hfGeral.Get("TipoOperacao").toString();
            popup = window.top.showModal(url, traducao.reunioes_reuni_es, largura, altura, atualizaPosPopup, null);
            if (popup != null)
                popup.focus();
        }
    }
    else {
        var url = '../../Reunioes/reunioes1_popup.aspx';
        url += '?idProjeto=' + valor[1];
        url += '&CR=' + valor[0];
        url += '&RO=' + somenteLeitura;
        url += '&ALT=' + altura;
        url += '&LARG=' + largura;
        url += '&TO=' + hfGeral.Get("TipoOperacao").toString();
        //popup = window.open(url, 'frameReuniao', 'scrollbars=no,toolbar=no,location=no,directories=no,status=no, menubar=no,resizable=no,copyhistory=no,height=' + altura + ',width=' + largura + ',top=' + meio1 + ',left=' + meio2 + '');
        popup = window.top.showModal(url, traducao.reunioes_reuni_es, largura, altura, atualizaPosPopup, null);
        if (popup != null)
            popup.focus();
    }
}

function excluiEvento(valor) {
    callbackExcluir.PerformCallback(valor);
}

function confirmaClickEnviarPauta() {
    capturaCodigosInteressados();
    if (btnEnviarPauta.cp_EditaMensagem == 'S') {
        if (tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1) {
            if (verificarDadosPreenchidos()) {
                pcMensagemPauta.SetHeaderText(traducao.reunioes_envio_de_pauta)
                heEncabecadoAta.SetHtml(btnEnviarPauta.cp_MensagemPauta);
                pcMensagemPauta.Show();
            }
        }
        else {
            if (verificarDadosAta()) {
                pcMensagemPauta.SetHeaderText(traducao.reunioes_envio_de_ata)
                heEncabecadoAta.SetHtml(btnEnviarPauta.cp_MensagemAta);
                pcMensagemPauta.Show();
            }
        }
    }
    else {
        if (tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1) {
            if (verificarDadosPreenchidos()) {
                tipoEnvio = "EnviarPauta";
                pnCallback.PerformCallback(tipoEnvio);
            }
        }
        else {
            if (verificarDadosAta()) {
                tipoEnvio = "EnviarAta";
                pnCallback.PerformCallback(tipoEnvio);
            }
        }
    }
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 40;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}