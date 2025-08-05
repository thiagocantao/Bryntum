// JScript File
var frmAnexosContrato = '';
var atualizarURLAnexos = '';

// ---- Provavelmente não será necessário alterar as duas próximas funções
function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

//function validaCamposFormulario()
//{
//    // Esta função tem que retornar uma string.
//    // "" se todas as validações estiverem OK
//    // "<erro>" indicando o que deve ser corrigido
//    mensagemErro_ValidaCamposFormulario = "";
//    
//    if (txtRisco.GetText() == "")
//    {
//        pcAbas.SetActiveTab(pcAbas.GetTab(0)); 
//        mensagemErro_ValidaCamposFormulario = "O campo nome deve ser informado.";
//        txtRisco.Focus();
//    }
//    else if (ddlTipo.GetText() == "")
//    {
//        pcAbas.SetActiveTab(pcAbas.GetTab(0)); 
//        mensagemErro_ValidaCamposFormulario = "O tipo deve ser informado.";
//        ddlTipo.Focus();
//    }
//    else if (ddlProbabilidade.GetText() == "")
//    {
//        pcAbas.SetActiveTab(pcAbas.GetTab(0));     
//        mensagemErro_ValidaCamposFormulario = "A probabilidade deve ser informada.";
//        ddlProbabilidade.Focus();
//    }
//    else if (ddlImpacto.GetText() == "")
//    {
//        pcAbas.SetActiveTab(pcAbas.GetTab(0));     
//        mensagemErro_ValidaCamposFormulario = "O impacto deve ser informado.";
//        ddlImpacto.Focus();
//    }
//    else if (txtResponsavel.GetText() == "")
//    {
//        pcAbas.SetActiveTab(pcAbas.GetTab(0));     
//        mensagemErro_ValidaCamposFormulario = "O responsável deve ser informado.";
//        txtResponsavel.Focus();
//    }
//    else if (window.txtConsequencia)
//    {
//        if (txtConsequencia.GetText().length > 2000)
//        {
//            pcAbas.SetActiveTab(pcAbas.GetTab(1));     
//            mensagemErro_ValidaCamposFormulario = "A quantidade de caracteres no campo \"consequência\" está maior que o máximo permitido.";
//            txtConsequencia.Focus();
//        }
//        else if (txtEstrategiaTratamento.GetText().length > 2000)
//        {
//            pcAbas.SetActiveTab(pcAbas.GetTab(1));     
//            mensagemErro_ValidaCamposFormulario = "A quantidade de caracteres no campo \"Estratégia para tratamento\" está maior que o máximo permitido";
//            txtEstrategiaTratamento.Focus();
//        }
//    }
//    
//    return mensagemErro_ValidaCamposFormulario;
//}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    hfGeral.Set("CodigoRiscoQuestao", "");
    hfGeral.Set("codigoRiscoQuestaoComentario","");
    
    try
    {
    txtRisco.SetText("");
    txtDescricao.SetText("");
    txtDescricao.Validate();

    ddlTipo.SetSelectedIndex(0);
    ddlProbabilidade.SetSelectedIndex(-1);
    ddlImpacto.SetSelectedIndex(-1);
    hfGeral.Set("lovCodigoResponsavel", "");
    txtResponsavel.SetText(""); 
    txtIncluidoEmPor.SetText(""); 
    txtPublicadoEmPor.SetText(""); 
    ddeLimiteEliminacao.SetText(""); 
    txtStatus.SetText(""); 
    txtDataElimincaoCancelado.SetText("");     
    if (window.txtConsequencia)
    {
        txtConsequencia.SetText("");
        txtConsequencia.Validate();

        txtEstrategiaTratamento.SetText("");
        txtEstrategiaTratamento.Validate(); 
    }
    
    // atualiza a imagem referente á severidade do risco/questão
    atualizaImagemSeveridade();
   // desabilitaHabilitaComponentes();
   }catch(e){}
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoRiscoQuestao;DescricaoRiscoQuestao;DetalheRiscoQuestao;CodigoTipoRiscoQuestao;ProbabilidadePrioridade;ImpactoUrgencia;CodigoUsuarioResponsavel;NomeUsuarioResponsavel;IncluidoEmPor;PublicaoEmPor;ConsequenciaRiscoQuestao;TratamentoRiscoQuestao;DataLimiteResolucao;DescricaoStatusRiscoQuestao;DataEliminacaoCancelamento;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
//    if (window.TipoOperacao &&  TipoOperacao=="Incluir")
//        return;
    if(values)
    {
        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        LimpaCamposFormulario();

        var idiomaEPortugues = hfGeral.Get("idiomaEPortugues");
        var inclui = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao") != "Incluir");
        btnRelatorioRisco.SetClientVisible(inclui && idiomaEPortugues);
        
        try
        {
            hfGeral.Set("CodigoRiscoQuestao", values[0] != null ? values[0]: "" );
            txtRisco.SetText(values[1] != null ? values[1] : "");
            txtDescricao.SetText(values[2] != null ? values[2] : "");
            txtDescricao.Validate();
            ddlTipo.SetValue(values[3] != null ? values[3] : 0);
            ddlProbabilidade.SetSelectedIndex(values[4] != null ? getIndexProbabilidadeImpacto(values[4]) : -1);
            ddlImpacto.SetSelectedIndex(values[5] != null ? getIndexProbabilidadeImpacto(values[5]) : -1);
            hfGeral.Set("lovCodigoResponsavel", values[6] != null ? values[6]: "" );
            txtResponsavel.SetText(values[7] != null ? values[7]: "" ); 
            txtIncluidoEmPor.SetText(values[8] != null ? values[8]: "" ); 
            txtPublicadoEmPor.SetText(values[9] != null ? values[9]: "" ); 
            ddeLimiteEliminacao.SetText(values[12] != null ? values[12]: "" ); 
            txtStatus.SetText(values[13] != null ? values[13]: "" ); 
            
            var readOnly = (TipoOperacao == "Consultar") ? 'S' : 'N';

            atualizarURLAnexos = 'S';

            frmAnexosContrato = './frameEspacoTrabalho_BibliotecaInterno.aspx?TA=RQ&ID=' + values[0] + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=420';

            if ( null == values[14])
                txtDataElimincaoCancelado.SetText(null);
            else
            {
                var dt = new Date(values[14]);
                var mes = dt.getMonth() + 1;
                
                if(mes<10)
                    mes = '0' + mes;
                    
                var strDate = dt.getDate() + '/' + mes +  '/' + dt.getFullYear();
                txtDataElimincaoCancelado.SetText(strDate);
            }
            hfGeral.Set("CodigoProjeto", values[15] != null ? values[15]: "-1" );

            if (window.txtConsequencia)
            {
                txtConsequencia.SetText(values[10] != null ? values[10]: "" );
                txtConsequencia.Validate();
                
                txtEstrategiaTratamento.SetText(values[11] != null ? values[11] : "");
                txtEstrategiaTratamento.Validate(); 
            }
            if (pcAbas.GetActiveTab().name == "tabPageToDoList")
                getValoresPlanoAcao();
            if (pcAbas.GetActiveTab().name == "tabPageComentario")
                getValoresComentarios(); 
            if (e.tab.name == 'tabPageAnexo') 
                getValoresAnexos();   
        
            // atualiza a imagem referente á severidade do risco/questão
            atualizaImagemSeveridade();
        }
        catch(e){}
    }
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    if (TipoOperacao == "Editar")
    {
        btnRelatorioRisco.SetClientVisible(true);
        window.top.mostraMensagem(traducao.meusProblemas_as_informa__es_foram_salvas_com_sucesso_, 'Atencao', false, false, null);
    }
}

// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------

function podeMudarAba(s, e)
{

 

    if (e.tab.name=='tabPageTratamento')
        verificaVisibilidadeBotoes();

    if (e.tab.index <=1)
        return false;
        
    if (hfGeral.Get("CodigoRiscoQuestao") == "")
    {
        window.top.mostraMensagem(traducao.meusProblemas_para_ter_acesso_a_op__o___ + e.tab.GetText() + "\" " + traducao.meusProblemas___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + pcAbas.GetTab(0).GetText() + "\"", 'Atencao', true, false, null);
        return true;
    }

    if(!window.TipoOperacao)
        TipoOperacao = "Consultar";
    
    // apenas para a aba ToDoList
    if (e.tab.name=='tabPageToDoList')
        getValoresPlanoAcao();
    else if (e.tab.name=='tabPageComentario')
        getValoresComentarios();
    else if (e.tab.name == 'tabPageAnexo')
        getValoresAnexos();
    
    return false;
}

function getValoresComentarios()
{
    hfGeral.Set("TipoOperacao", TipoOperacao);
    hfGeral.Set('codigoRiscoQuestaoComentario', hfGeral.Get("CodigoRiscoQuestao") );
    gvComentarios.PerformCallback('Popular'+hfGeral.Get("CodigoRiscoQuestao") );
}

function getValoresPlanoAcao()
{
    if (window.hfGeralToDoList)
    {
        hfGeral.Set("TipoOperacao", TipoOperacao);
        hfGeralToDoList.Set('codigoObjetoAssociado', hfGeral.Get("CodigoRiscoQuestao") );
        hfGeral.Set("codigoObjetoAssociado", hfGeral.Get("CodigoRiscoQuestao") );
        gvToDoList.PerformCallback('Popular');
    }
    else
        window.top.mostraMensagem(traducao.meusProblemas_n_o_foi_poss_vel_encontrar_o_componente_todolist, 'Atencao', true, false, null);
}

function desabilitaHabilitaComponentes()
{
    ddlTipo.SetEnabled(false);
    ddlProbabilidade.SetEnabled(false);
    ddlImpacto.SetEnabled(false);
    ddeLimiteEliminacao.SetEnabled(false);
}

/// <summary>
/// Função para atualizar, no popupControl da tela, a imagem referente à severidade do risco ou da questão
/// </summary>
/// <remarks>
/// Criada a partir da regra existente na função de banco de dados [f_GetCorRiscoQuestao]
/// by Géter - 05/04/2010
/// </remarks>
/// <returns type="void"></returns>
function atualizaImagemSeveridade()
{
    var probabilidade, impacto, tipoRisco, tipoRQ, status;
    var severidade = null;
    var corRisco;
    
    var aba = pcAbas.GetTab(0); 
    if ( "Risco" == aba.GetText() )
        tipoRQ = "R";
    else
        tipoRQ = "Q";
        
    if ( "R" == tipoRQ )
        tipoRisco = ddlTipo.GetValue();
        
    status = txtStatus.GetText();
    if ( null == status )
        status = 'Ativo';
    
    probabilidade = getValueProbabilidadeImpacto(ddlProbabilidade.GetSelectedIndex());
    impacto = getValueProbabilidadeImpacto(ddlImpacto.GetSelectedIndex());
    
    if( (probabilidade>0) && (impacto>0) )
    {
        severidade = probabilidade * impacto;
        
        if (  ("R" == tipoRQ) && (2 == tipoRisco) && ((severidade < 3) || (severidade>4))  )
        {
            if (severidade<3)
                severidade = 9;
            else
                severidade = 1;
        } // if (  ("R" == tipoRQ) && ...
        
        // se o risco ainda está ativo
        if ( ("ativo" == status.toLowerCase()) && ("" == txtDataElimincaoCancelado.GetText()) )
        {
            // se tiver data limite para resolução
            if ( "" != ddeLimiteEliminacao.GetText() )
            {
                // var dtAux = new Date();
                // var dtHoje = new Date(dtAux.getFullYear(), dtAux.getMonth(), dtAux.getDay(), 0, 0, 0, 0);
                var dtHoje = new Date();
                dtHoje.setHours(0,0,0,0);
                var dtLimiteRisco = ddeLimiteEliminacao.GetDate();
                if (dtHoje.toDateString() == dtLimiteRisco.toDateString())
                    severidade = 3
                else if (dtHoje.valueOf() > dtLimiteRisco.valueOf())
                    severidade = 9
            } // if ( "" != txtDataElimincaoCancelado.GetText() )
        } // if ( ("ativo" == status.toLowerCase()) && ...
        
        if (severidade < 3)
            corRisco = 'Verde';
        else if (severidade < 6)
            corRisco = 'Amarelo';
        else 
            corRisco = 'Vermelho';
            
    } // if( (probabilidade>0) && (impacto>0) )
    else
        corRisco = 'Branco';
        
    imgSeveridade.SetImageUrl('../imagens/' + corRisco + '.gif');
}

/// <summary>
/// Função para criar um objeto para ser usada nas funções que irão dar títulos aos botões
/// </summary>
/// <remarks>
///  Função criada pelo fato de que não foi possível passar um string por referência.
/// </remarks>
function myStr()
{
    this.value = "";
}

/// <summary>
/// Função para ajustar a visibilidade dos botões de eliminação e cancelamento de risco, conforme o 
/// usuário que estiver logado.
/// </summary>
/// <remarks>
/// </remarks>
/// <returns type="void"></returns>
function verificaVisibilidadeBotoes()
{
    TipoOperacao = hfGeral.Get("TipoOperacao");
    var somenteLeitura = TipoOperacao == "Consultar";
    var podeSalvar = gvDados.cpPodeEditar;
    var podeEliminar = true;
    if (somenteLeitura) {
        podeSalvar = false;
        podeEliminar = false;
    }
    else {
        var dataEliminacao = txtDataElimincaoCancelado.GetText();
        var publicadoPor = txtPublicadoEmPor.GetText();
        podeSalvar = podeSalvar && ((null == dataEliminacao) || ("" == dataEliminacao));
        podeEliminar = podeEliminar && ((null != publicadoPor) && ("" != publicadoPor));
    }
    btnSalvar.SetVisible(podeSalvar);
    btnSalvar2.SetVisible(podeSalvar);

    if (podeEliminar) {
        var nomeItem = new myStr();
        var pronomeItem = new myStr();
        var acaoTraduzida = new myStr();
        var nomeAcao = new myStr();
        var pronomeAcao = new myStr();

        obtemTextosAcao("Eliminar", acaoTraduzida, nomeAcao, pronomeAcao, nomeItem, pronomeItem);
        btnEliminar.SetText(acaoTraduzida.value + " " + nomeItem.value);
        btnEliminar.GetMainElement().title = acaoTraduzida.value + " " + pronomeItem.value + " " + nomeItem.value;

        obtemTextosAcao(traducao.meusProblemas_cancelar, acaoTraduzida, nomeAcao, pronomeAcao, nomeItem, pronomeItem);
        btnCancelar.SetText(acaoTraduzida.value);
        btnCancelar.GetMainElement().title = acaoTraduzida.value + " " + pronomeItem.value + " " + nomeItem.value;
    }

    btnEliminar.SetVisible(podeEliminar);
    btnCancelar.SetVisible(podeEliminar);
}

/// <summary>
/// Função para tratar o click do botão 'Eliminar' da aba tratamento do risco/questão
/// </summary>
/// <remarks>
/// </remarks>
/// <returns type="void"></returns>
function onBtnAcaoEliminar_Click()
{
    mostraPopupAcao("Eliminar");
}

/// <summary>
/// Função para tratar o click do botão 'Cancelar' da aba tratamento do risco/questão
/// </summary>
/// <remarks>
/// </remarks>
/// <returns type="void"></returns>
function onBtnAcaoCancelar_Click()
{
    mostraPopupAcao("Cancelar");
}

function mostraPopupAcao(acao)
{
    var nomeItem        = new myStr();
    var pronomeItem     = new myStr();
    var acaoTraduzida   = new myStr();
    var nomeAcao        = new myStr();
    var pronomeAcao     = new myStr();
    
    hfAcao.Set("Acao", acao);
    obtemTextosAcao(acao, acaoTraduzida, nomeAcao, pronomeAcao, nomeItem, pronomeItem);
    
    pcComentarioAcao.SetHeaderText(nomeAcao.value);
    lblComentarioAcao.SetText(traducao.meusProblemas_coment__rio_sobre_ + " " + nomeAcao.value + " " + pronomeItem.value + " " + nomeItem.value + ":");
    mmComentarioAcao.SetText(null);
    mmComentarioAcao.Validate();   
    pcComentarioAcao.Show();
}

/// <summary>
/// Obtém textos de uma ação.
/// </summary>
/// <remarks>
/// Dada um ação, esta função devolve os textos referente à ação para serem usadas na personalização dos botões
/// </remarks>
/// <returns type="void"></returns>
function obtemTextosAcao(acao, acaoTraduzida, nomeAcao, pronomeAcao, nomeItem, pronomeItem)
{
    var aba = pcAbas.GetTab(0);
    var tipoRQ;
    //Tratamento de Pronome para Masculino e Feminino em PT e em inglês utiliza-se o "The"
    pronomeAcao.value = traducao.meusProblemas_a;//hfGeral.Get("artigo")
    acaoTraduzida.value = acao;

    nomeItem.value = aba.GetText();
    if ("Risco" == nomeItem.value)
        tipoRQ = "R";
    else
        tipoRQ = "Q";

    if ("Questão" == nomeItem.value)
        pronomeItem.value = traducao.meusProblemas_a;
    else
        pronomeItem.value = traducao.meusProblemas_o;

    if ("Eliminar" == acao) {
        if ("Q" == tipoRQ) {
            acaoTraduzida.value = traducao.meusProblemas_resolver;
            nomeAcao.value = traducao.meusProblemas_resolu____o;
        }
        else
            nomeAcao.value = traducao.meusProblemas_elimina____o;
    }
    else if ("Cancelar" == acao) {
        pronomeAcao.value = traducao.meusProblemas_o;
        nomeAcao.value = traducao.meusProblemas_cancelamento;
    }
    else if ("Excluir" == acao)
        nomeAcao.value = traducao.meusProblemas_exclus__o;

    return;
}


/// <summary>
/// Função para tratar o click do botão de ação do popup de comentário de ação 
/// usuário que estiver logado.
/// </summary>
/// <remarks>
/// </remarks>
/// <returns type="void"></returns>
function onBtnAcao_pcComentarioAcao_Click()
{
    var comment = mmComentarioAcao.GetText();
    if ( (null == comment) || ("" == comment) )
        window.top.mostraMensagem(traducao.meusProblemas_por_favor__informe_o_coment_rio_, 'Atencao', true, false, null);
    else
    {
        var acao = hfAcao.Get("Acao");
        if( null != acao )
            pnCallback.PerformCallback(acao); // o endCallBack tratará de fechar a tela ou mostrar o erro
    }
}


/// <summary>
/// Função chamada ao final do calback da ação 'eliminar risco' ou ...
/// </summary>
/// <remarks>
/// </remarks>
/// <returns type="void"></returns>
function onPnCallBack_EndCallback_Acao(s,e)
{
    var statusAcao = null;
    var msg = "";
        
    if (hfAcao.Contains("StatusAcao"))
        statusAcao = hfAcao.Get("StatusAcao");

    // se tudo certo na ação, desabilita         
    if ("OK" == statusAcao)
    {
        pcComentarioAcao.Hide()
        onClick_btnCancelar();
    } // if ( "OK" == statusAcao )
    else if ("NOK" == statusAcao)
    {
        msg = hfAcao.Get("MensagemErro");
        
        if (null == msg)
            msg = traducao.meusProblemas_falha_ao_atualizar_as_informa__es_;
            
        window.top.mostraMensagem(msg, 'Atencao', true, false, null);
        // pcComentarioAcao.Hide()  -- NÃO fechar a tela se ocorrer erro
    } // else if ("NOK" == statusAcao)
    
    hfAcao.Set("Acao", "");
    hfAcao.Set("StatusAcao", "");
}

/// <summary>
/// Função para impedir que se digite além do tamanho de um campo. 
/// </summary>
/// <remarks>
/// Chamada no OnKeyUp de controles que não tenham a propriedade [MaxLength], como é o caso de 
/// controles do tipo Memo.
/// </remarks>
/// <param name="s" type="object">Controle no qual está ocorrendo o evento, cujo tamanho do 
///  texto será controlado</param>
/// <param name="e" type="object">Dados do evento DevExpress. Deverá conter um objeto htmlEvent</param>
/// <param name="tam" type="int">Tamanho máximo do campo.</param>
/// <returns type="void"></returns>
function validaTamanhoTexto(s, e, tam)
{
	var text = s.GetText();
	
	if (null != text)
	{
		if (text.length >= tam)
		{
			e.htmlEvent.returnValue = false;
			text = text.substring(0,tam);
			s.SetText(text);
        }
	}
}

function getIndexProbabilidadeImpacto(valorBD)
{
    var index = 0;
    if (valorBD==3)
        index = 1;
    else if (valorBD==1)
        index = 3
    else 
        index = 2;
        
    return index - 1;
}

function getValueProbabilidadeImpacto(index)
{
    var valorBD;
    
    switch(index) {
        case 0: valorBD = 3; break;
        case 1: valorBD = 2; break;
        case 2: valorBD = 1; break;
        default: valorBD = 0; break;
    }
    return valorBD;
}

function getValoresAnexos() {
    if (atualizarURLAnexos != 'N') {
        atualizarURLAnexos = 'N';
        document.getElementById('frmAnexos').src = frmAnexosContrato;
    }
}

function setMaxLength(textAreaElement, length) {
    textAreaElement.maxlength = length;
    ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
    ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
}

function onKeyUpOrChange(evt) {
    processTextAreaText(ASPxClientUtils.GetEventSource(evt));
}

function processTextAreaText(textAreaElement) {
    var maxLength = textAreaElement.maxlength;
    var text = textAreaElement.value;
    var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
    if (maxLength != 0 && text.length > maxLength)
        textAreaElement.value = text.substr(0, maxLength);
    else {
        document.getElementById('spC').innerText = text.length;
    }
}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

