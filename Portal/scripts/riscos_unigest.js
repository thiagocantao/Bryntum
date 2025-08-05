
var frmAnexosContrato = '';
var atualizarURLAnexos = '';

// ==============================================================
// Script para a tela "DadosProjeto_riscos.aspx"
// ==============================================================

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

function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var conta = 1;
    if (txtRisco.GetText() == "")
    {
        pcAbas.SetActiveTab(pcAbas.GetTab(0)); 
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_o_campo_ + lblRiscoQuestao.GetText().toString().replace(':', '') + traducao.riscos_unigest__deve_ser_informado_ + "\n";
        txtRisco.Focus();
    }
    if (ddlTipo.GetText() == "")
    {
        pcAbas.SetActiveTab(pcAbas.GetTab(0)); 
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_o_tipo_deve_ser_informado_ + "\n";
        ddlTipo.Focus();
    }
    if (ddlProbabilidade.GetText() == "")
    {
        pcAbas.SetActiveTab(pcAbas.GetTab(0));     
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_a_ + lblProbabilidadeUrgencia.GetText().toString().replace(':', '') + traducao.riscos_unigest__deve_ser_informada_ + "\n";
        ddlProbabilidade.Focus();
    }
    if (ddlImpacto.GetText() == "")
    {
        pcAbas.SetActiveTab(pcAbas.GetTab(0));
        if(lblImpactoPrioridade.GetText() == "Impacto:")     
            mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_o_ + lblImpactoPrioridade.GetText().toString().replace(':', '') + traducao.riscos_unigest__deve_ser_informado_ + "\n";
        else
            mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_a_ + lblImpactoPrioridade.GetText().toString().replace(':', '') + traducao.riscos_unigest__deve_ser_informada_ + "\n";
        ddlImpacto.Focus();
    }
    if (ddlResponsavel.GetText() == "")
    {
        pcAbas.SetActiveTab(pcAbas.GetTab(0));     
        mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_o_respons_vel_deve_ser_informado_ + "\n";
        ddlResponsavel.Focus();
    }
    if (window.txtConsequencia)
    {
        if (txtConsequencia.GetText().length > 2000)
        {
            pcAbas.SetActiveTab(pcAbas.GetTab(1));     
            mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_a_quantidade_de_caracteres_no_campo___consequ_ncia___est__maior_que_o_m_ximo_permitido_ + "\n";
            txtConsequencia.Focus();
        }
        else if (txtEstrategiaTratamento.GetText().length > 2000)
        {
            pcAbas.SetActiveTab(pcAbas.GetTab(1));     
            mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_a_quantidade_de_caracteres_no_campo___estrat_gia_para_tratamento___est__maior_que_o_m_ximo_permitido_ + "\n";
            txtEstrategiaTratamento.Focus();
        }
    }

    if (checkAfetaMeta.GetChecked() == true) {
        if (ddlMetas.GetValue() == null) {
            mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_a_meta_afetada_deve_ser_informada_ + "\n";
        }
    }

    if (checkAfetaOrcamento.GetChecked() == true) {

        if (ddlTipoOrcamento.GetValue() == null) {
            mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_o_tipo_de_varia__o_deve_ser_informado_ + "\n";
        }
        if (spnValorOrcamento.GetValue() == null) {
            mensagemErro_ValidaCamposFormulario += (conta++) + ") " + traducao.riscos_unigest_o_valor_da_varia__o_deve_ser_informado_ + "\n";
        }
    }
    
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario() {

    btnTransforma.SetVisible(false);

    // Função responsável por preparar os campos do formulário para receber um novo registro
    hfGeral.Set("CodigoRiscoQuestao", "");
    hfGeral.Set("codigoRiscoQuestaoComentario","");

    txtRisco.SetText("");

    txtDescricao.SetText("");


    ddlTipo.SetSelectedIndex(0);
    ddlProbabilidade.SetSelectedIndex(-1);
    ddlImpacto.SetSelectedIndex(-1);
    ddlResponsavel.SetSelectedIndex(-1);
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

    ddlRiscoAssociado.SetValue(null);
    spnCusto.SetValue(null);
    ddlTipoRespostaRisco.SetValue(null);

    checkAfetaOrcamento.SetChecked(false);
    lblTipoOrcamento.SetVisible(false);
    ddlTipoOrcamento.SetVisible(false);
    lblVariacaoOrcamento.SetVisible(false);
    spnValorOrcamento.SetVisible(false);


    checkAfetaMeta.SetChecked(false);
    ddlMetas.SetVisible(false);
    lblMetasAtivasProjeto.SetVisible(false);


    ddlMetas.SetValue(null);
    ddlTipoOrcamento.SetValue(null);
    spnValorOrcamento.SetValue(null);

    pnRiscoAssociadoSuperior.PerformCallback('-1|-1|Incluir');
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        //                                                             0;                    1;                  2;                     3;                      4               5;                       6;                    7;             8;            9;                      10;                    11;                 12;                         13;                        14;                     15;               16;                        17;                           18;               19;                20;               21;                 22;
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoRiscoQuestao;DescricaoRiscoQuestao;DetalheRiscoQuestao;CodigoTipoRiscoQuestao;ProbabilidadePrioridade;ImpactoUrgencia;CodigoUsuarioResponsavel;NomeUsuarioResponsavel;IncluidoEmPor;PublicaoEmPor;ConsequenciaRiscoQuestao;TratamentoRiscoQuestao;DataLimiteResolucao;DescricaoStatusRiscoQuestao;DataEliminacaoCancelamento;CodigoTipoRespostaRisco;CustoRiscoQuestao;CodigoRiscoQuestaoSuperior;DescricaoRiscoQuestaoSuperior;TipoVariacaoCusto;ValorVariacaoCusto;CodigoMetaAfetada;DescricaoMetaAfetada', MontaCamposFormulario);
}

function MontaCamposFormulario(values) {
   // debugger
    btnTransforma.SetVisible(false);

    if (window.TipoOperacao && TipoOperacao == "Incluir")
        return;

    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    //LimpaCamposFormulario();
    var inclui = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao") != "Incluir");
    btnRelatorioRisco.SetClientVisible(inclui);
    hfGeral.Set("CodigoRiscoQuestao", values[0] != null ? values[0] : "");
    txtRisco.SetText(values[1] != null ? values[1] : "");

    txtDescricao.SetText(values[2] != null ? values[2] : "");
    txtDescricao.Validate();

    ddlTipo.SetValue(values[3] != null ? values[3] : 0);
    ddlProbabilidade.SetSelectedIndex(values[4] != null ? getIndexProbabilidadeImpacto(values[4]) : -1);
    ddlImpacto.SetSelectedIndex(values[5] != null ? getIndexProbabilidadeImpacto(values[5]) : -1);
    hfGeral.Set("lovCodigoResponsavel", values[6] != null ? values[6] : "");
    ddlResponsavel.SetValue(values[6] != null ? values[6] : null);
    ddlResponsavel.SetText(values[7] != null ? values[7] : null);
    txtIncluidoEmPor.SetText(values[8] != null ? values[8] : "");
    txtPublicadoEmPor.SetText(values[9] != null ? values[9] : "");
    ddeLimiteEliminacao.SetText(values[12] != null ? values[12] : "");
    txtStatus.SetText(values[13] != null ? values[13] : "");

var DescricaoMetaAfetada = values[22] != null ? values[22] : "";

    var readOnly = (!(gvDados.cpPodeEditar) || TipoOperacao == "Consultar") ? 'S' : 'N';

    atualizarURLAnexos = 'S';

    frmAnexosContrato = '../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=RQ&ID=' + values[0] + '&RO=' + readOnly + '&TO=' + TipoOperacao + '&ALT=235';


    if (null == values[14])
        txtDataElimincaoCancelado.SetText(null);
    else {
        var dt = new Date(values[14]);
        var mes = dt.getMonth() + 1;

        if (mes < 10)
            mes = '0' + mes;

        var strDate = dt.getDate() + '/' + mes + '/' + dt.getFullYear();
        txtDataElimincaoCancelado.SetText(strDate);
    }

    if (window.txtConsequencia) {
        txtConsequencia.SetText(values[10] != null ? values[10] : "");
        txtConsequencia.Validate();


        txtEstrategiaTratamento.SetText(values[11] != null ? values[11] : "");
        txtEstrategiaTratamento.Validate();
    }

    if (pcAbas.GetActiveTab().name == "tabPageToDoList")
        getValoresPlanoAcao();
    if (pcAbas.GetActiveTab().name == "tabPageComentario")
        getValoresComentarios();
    if (pcAbas.GetActiveTab().name == "tabPageAnexo")
        getValoresAnexos();

    // atualiza a imagem referente á severidade do risco/questão
    atualizaImagemSeveridade();

    //mostra ou esconde o botão de transformar
    var aba = pcAbas.GetTab(0);
    if ("Risco" == aba.GetText()) {

        if ((window.TipoOperacao) && ("Editar" == TipoOperacao)) {

            var dataEliminacao = txtDataElimincaoCancelado.GetText();
            if ((null == dataEliminacao) || ("" == dataEliminacao)) {

                if (getPolaridadeFromComboBox(ddlTipo.GetSelectedIndex()) != "Positiva") {
                    btnTransforma.SetVisible(true);
                }
            }
        }
    }
    //CodigoTipoRespostaRisco;CustoRiscoQuestao;CodigoRiscoQuestaoSuperior 16 17 18
    var CodigoTipoRespostaRisco = values[15] != null ? values[15] : "";
    var CustoRiscoQuestao = values[16] != null ? values[16] : 0;
    var CodigoRiscoQuestaoSuperior = values[17] != null ? values[17] : "";
    var DescricaoRiscoQuestaoSuperior = (values[18] != null ? values[18] : "");
    ddlRiscoAssociado.SetValue(CodigoRiscoQuestaoSuperior);
    ddlRiscoAssociado.SetText(DescricaoRiscoQuestaoSuperior);

    spnCusto.SetValue(CustoRiscoQuestao);
    ddlTipoRespostaRisco.SetValue(CodigoTipoRespostaRisco);
    pnRiscoAssociadoSuperior.PerformCallback(hfGeral.Get("CodigoRiscoQuestao") + '|' + CodigoRiscoQuestaoSuperior + '|' + TipoOperacao);

    var TipoVariacaoCusto = values[19] != null ? values[19] : null;
    var ValorVariacaoCusto = values[20] != null ? values[20] : null;
    var CodigoMetaAfetada = values[21] != null ? values[21] : null;
    
    ddlTipoOrcamento.SetValue(TipoVariacaoCusto);
    spnValorOrcamento.SetValue(ValorVariacaoCusto);
    
    checkAfetaOrcamento.SetChecked((TipoVariacaoCusto != null) && (ValorVariacaoCusto != null));
    ddlTipoOrcamento.SetVisible(checkAfetaOrcamento.GetChecked() == true);
    spnValorOrcamento.SetVisible(checkAfetaOrcamento.GetChecked() == true);

    lblTipoOrcamento.SetVisible(checkAfetaOrcamento.GetChecked() == true);
    ddlTipoOrcamento.SetVisible(checkAfetaOrcamento.GetChecked() == true);
    lblVariacaoOrcamento.SetVisible(checkAfetaOrcamento.GetChecked() == true);
    spnValorOrcamento.SetVisible(checkAfetaOrcamento.GetChecked() == true);  
    
    checkAfetaMeta.SetChecked(CodigoMetaAfetada != null);
    
    ddlMetas.SetVisible(CodigoMetaAfetada != null);
    ddlMetas.SetValue(CodigoMetaAfetada);
    ddlMetas.SetText(DescricaoMetaAfetada);

    lblMetasAtivasProjeto.SetVisible(CodigoMetaAfetada != null);   
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

function getPolaridadeFromComboBox(index) {

    var polaridade = "";
    if (index > -1) {
        var item = ddlTipo.GetItem(index);
        if (item != null) {
            polaridade = item.GetColumnText("PolaridadeExtenso");
        }
    }

    return polaridade == null ? "" : polaridade;
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    if (TipoOperacao == "Editar")
    {
        btnRelatorioRisco.SetClientVisible(true);
        window.top.mostraMensagem(traducao.riscos_unigest_as_informa__es_foram_salvas_com_sucesso_, 'sucesso', false, false, null);

    }

    OnGridFocusedRowChanged(gvDados, true);
        
    //onClick_btnCancelar();    
}


// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------

function podeMudarAba(s, e)
{
    if (e.tab.name=='tabPageTratamento')
    {
        verificaVisibilidadeBotoes();
    }
    
    if (e.tab.index <=1)
        return false;
        
    if (hfGeral.Get("CodigoRiscoQuestao") == "")
    {
        window.top.mostraMensagem(traducao.riscos_unigest_para_ter_acesso_a_op__o___ + e.tab.GetText() + "\" " + traducao.riscos_unigest___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + pcAbas.GetTab(0).GetText() + "\"", 'Atencao', true, false, null);
        return true;
    }

    if(!window.TipoOperacao)
        TipoOperacao = "Consultar";
    
    // apenas para a aba ToDoList
    if (e.tab.name=='tabPageToDoList')
    {
        getValoresPlanoAcao();
    }
    else if (e.tab.name=='tabPageComentario')
    {
        getValoresComentarios();
    }
    else if (e.tab.name == 'tabPageAnexo') {
        getValoresAnexos();
    }       
    
    return false;
}

function getValoresAnexos() {
    if (atualizarURLAnexos != 'N') {
        atualizarURLAnexos = 'N';
        document.getElementById('frmAnexos').src = frmAnexosContrato;
    }
}

function getValoresComentarios()
{
   // if (hfGeral.Get('codigoRiscoQuestaoComentario') != hfGeral.Get("CodigoRiscoQuestao") || TipoOperacao=="Incluir" || TipoOperacao=="Editar" )
   // {
        hfGeral.Set("TipoOperacao", TipoOperacao);
        hfGeral.Set('codigoRiscoQuestaoComentario', hfGeral.Get("CodigoRiscoQuestao") );
        gvComentarios1.PerformCallback('Popular'+hfGeral.Get("CodigoRiscoQuestao") );
   // }
}

function getValoresPlanoAcao()
{
    if (window.hfGeralToDoList)
    {
        //if(!window.TipoOperacao)
        //    TipoOperacao = "Consultar";
            
       // if (hfGeralToDoList.Get('codigoObjetoAssociado') != hfGeral.Get("CodigoRiscoQuestao") || TipoOperacao=="Incluir" || TipoOperacao=="Editar" )
       // {
            hfGeral.Set("TipoOperacao", TipoOperacao);
            hfGeralToDoList.Set('codigoObjetoAssociado', hfGeral.Get("CodigoRiscoQuestao") );
            hfGeral.Set("codigoObjetoAssociado", hfGeral.Get("CodigoRiscoQuestao") );
            gvToDoList.PerformCallback('Popular');
       // }
    }
    else
        window.top.mostraMensagem(traducao.riscos_unigest_n_o_foi_poss_vel_encontrar_o_componente_todolist, 'Atencao', true, false, null);

}

function desabilitaHabilitaComponentes()
{
    var BoolEnabled = (hfGeral.Get("TipoOperacao") != "Consultar") && gvDados.cpPodeEditar;
	
	ddeLimiteEliminacao.SetEnabled(BoolEnabled);	
	//txtResponsavel.SetEnabled(BoolEnabled);
	txtRisco.SetEnabled(BoolEnabled);
	txtDescricao.SetEnabled(BoolEnabled);
	try{
	    txtConsequencia.SetEnabled(BoolEnabled);
	    txtEstrategiaTratamento.SetEnabled(BoolEnabled);
	}catch(e){};
	
	ddlTipo.SetEnabled(BoolEnabled);
	ddlProbabilidade.SetEnabled(BoolEnabled);
	ddlImpacto.SetEnabled(BoolEnabled);
	ddlResponsavel.SetEnabled(BoolEnabled);
	
	imgSeveridade.SetEnabled(BoolEnabled);
	

	ddlRiscoAssociado.SetEnabled(BoolEnabled);
	spnCusto.SetEnabled(BoolEnabled);
	ddlTipoRespostaRisco.SetEnabled(BoolEnabled);

    checkAfetaMeta.SetEnabled(BoolEnabled);
    ddlMetas.SetEnabled(BoolEnabled);

    checkAfetaOrcamento.SetEnabled(BoolEnabled);
    ddlTipoOrcamento.SetEnabled(BoolEnabled);
    spnValorOrcamento.SetEnabled(BoolEnabled);

//	(window.lblCantCarater3) ? lblCantCarater3.SetVisible(BoolEnabled) : true;
//	(window.lblDe2503) ? lblDe2503.SetVisible(BoolEnabled) : true;
//	
//    (window.lblCantCarater1) ? lblCantCarater1.SetVisible(BoolEnabled) : true;
//    (window.lblDe2501) ? lblDe2501.SetVisible(BoolEnabled) : true;
//    
//    (window.lblCantCarater2) ? lblCantCarater2.SetVisible(BoolEnabled) : true; 
//    (window.lblDe2502) ? lblDe2502.SetVisible(BoolEnabled) : true;
}

/// <summary>
/// Função para atualizar, no popupControl da tela, a imagem referente à severidade do risco ou da questão
/// </summary>
/// <remarks>
/// Criada a partir da regra existente na função de banco de dados [f_GetCorRiscoQuestao]
/// by Géter - 05/04/2010, revista em 27/04/2015 para contemplar o conceito de polaridade do risco/questão
/// </remarks>
/// <returns type="void"></returns>
function atualizaImagemSeveridade()
{
    var probabilidade, impacto, polaridade, tipoRQ, status;
    var severidade = null;
    var corRisco;
    
    var aba = pcAbas.GetTab(0); 
    if ( "Risco" == aba.GetText() )
        tipoRQ = "R";
    else
        tipoRQ = "Q";

    status = txtStatus.GetText();
    if ( null == status )
        status = 'Ativo';
    
    probabilidade = getValueProbabilidadeImpacto(ddlProbabilidade.GetSelectedIndex());
    impacto = getValueProbabilidadeImpacto(ddlImpacto.GetSelectedIndex());
    if ("R" == tipoRQ) {
        polaridade = getPolaridadeFromComboBox(ddlTipo.GetSelectedIndex());
    }
        
    if( (probabilidade>0) && (impacto>0) )
    {
        severidade = probabilidade * impacto;
        
        if (  ("R" == tipoRQ) && ("Positiva" == polaridade) && ((severidade < 3) || (severidade>4))  )
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
        
    imgSeveridade.SetImageUrl('../../imagens/' + corRisco + '.gif');
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
    
    var mostrar;
    var codigoUsuarioLogado = null, codigoResponsavelRisco = null, codigoGerenteProjeto = null;
    var showBtnSalvar;
    
    var dataEliminacao = txtDataElimincaoCancelado.GetText();
    var TipoOpr = hfGeral.Get("TipoOperacao");
    if (TipoOpr)
    {
        if (TipoOpr == "Consultar" || !(gvDados.cpPodeEditar)) {
            showBtnSalvar = false;
        }
        else
            showBtnSalvar = ((null == dataEliminacao) || ("" == dataEliminacao));
    }
    else 
        showBtnSalvar = false;
                
    btnSalvar.SetVisible(showBtnSalvar);
    btnSalvar2.SetVisible(showBtnSalvar);
        
    if ( (showBtnSalvar) &&  (TipoOpr=="Editar") )
    {
        var publicadoPor   = txtPublicadoEmPor.GetText();
        
        // o(a) risco/questão tem que publicado e ainda não resolvido/eliminado/cancelado 
        if(  (null != publicadoPor) && ("" != publicadoPor) )
        {
        
            if ( hfGeral.Contains("codigoUsuarioLogado") )
                codigoUsuarioLogado = hfGeral.Get("codigoUsuarioLogado");
                
            if ( hfGeral.Contains("lovCodigoResponsavel") )
                codigoResponsavelRisco = hfGeral.Get("lovCodigoResponsavel");
                
            if ( hfGeral.Contains("codigoGerenteProjeto") )
                codigoGerenteProjeto = hfGeral.Get("codigoGerenteProjeto");
                
            if (null != codigoUsuarioLogado)
            {
                if ( (codigoUsuarioLogado == codigoResponsavelRisco) /*|| (codigoUsuarioLogado == codigoGerenteProjeto)*/ )
                    mostrar = true;
            }
        } // if( (null == dataEliminacao) || ...
    } // if (window.TipoOperacao &&  TipoOperacao=="Editar")
    else
        mostrar = false;
    
    if (true == mostrar) {
        btnEliminar.SetVisible(true);
        btnCancelar.SetVisible(true);
    }
    else {
        btnEliminar.SetVisible(gvDados.cp_podeEliminar);
        btnCancelar.SetVisible(gvDados.cp_podeCancelar);
    }

    var nomeItem = new myStr();
    var pronomeItem = new myStr();
    var acaoTraduzida = new myStr();
    var nomeAcao = new myStr();
    var pronomeAcao = new myStr();

    if (btnEliminar.GetVisible()) {
        obtemTextosAcao("Eliminar", acaoTraduzida, nomeAcao, pronomeAcao, nomeItem, pronomeItem);
        btnEliminar.SetText(acaoTraduzida.value + ' ' + nomeItem.value);
        btnEliminar.GetMainElement().title = acaoTraduzida.value + " " + pronomeItem.value + " " + nomeItem.value;
    }
    if (btnCancelar.GetVisible()) {
        obtemTextosAcao("Cancelar", acaoTraduzida, nomeAcao, pronomeAcao, nomeItem, pronomeItem);
        btnCancelar.SetText(acaoTraduzida.value + ' ' + nomeItem.value);
        btnCancelar.GetMainElement().title = acaoTraduzida.value + " " + pronomeItem.value + " " + nomeItem.value;
    }
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
    
    pcComentarioAcao.SetHeaderText(nomeAcao.value + " de " + nomeItem.value);
    lblComentarioAcao.SetText(traducao.riscos_unigest_coment_rio_sobre_ + pronomeAcao.value + " " + nomeAcao.value + " d" + pronomeItem.value + " " + nomeItem.value + ":");
    mmComentarioAcao.SetText(null);
    
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
    pronomeAcao.value = "a";
    acaoTraduzida.value = acao;
    
    nomeItem.value = aba.GetText();
    if ( "Risco" == nomeItem.value )
        tipoRQ = "R";
    else
        tipoRQ = "Q";
        
    if ( "Questão" == nomeItem.value )
        pronomeItem.value = "a";
    else
        pronomeItem.value = "o";
        
    if ( "Eliminar" == acao )
    {
        if ( "Q" == tipoRQ )
        {
            acaoTraduzida.value = "Resolver";
            nomeAcao.value = "Resolução";
        }
        else
            nomeAcao.value = "Eliminação";
    }
    else if ("Cancelar" == acao)
    {
        pronomeAcao.value = "o";
        nomeAcao.value = "Cancelamento";
    }
    else if ("Excluir" == acao)
        nomeAcao.value = "Exclusão ";
        
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
        window.top.mostraMensagem(traducao.riscos_unigest_por_favor__informe_o_coment_rio_, 'Atencao', true, false, null);
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
        pcComentarioAcao.Hide();
        onClick_btnCancelar();
    } // if ( "OK" == statusAcao )
    else if ("NOK" == statusAcao)
    {
        msg = hfAcao.Get("MensagemErro");
        
        if (null == msg)
            msg = traducao.riscos_unigest_falha_ao_atualizar_as_informa__es_;
            
        window.top.mostraMensagem(msg, 'Atencao', true, false, null);
        // pcComentarioAcao.Hide()  -- NÃO fechar a tela se ocorrer erro
    } // else if ("NOK" == statusAcao)

//    if (pnCallback.cp_FechaEdicao == "S")
//        pcDados.Hide();

    hfAcao.Set("Acao", "");
    hfAcao.Set("StatusAcao", "");
}

function onClick_btnTransforma() {
    var lblQuestao = hfGeral.Get("labelQuestao");
    var texto = traducao.riscos_unigest_confirma_convers_o_do_risco_em_ + lblQuestao + ' ? ';

    window.top.mostraMensagem(texto, 'confirmacao', true, true, converteRisco);
}

function converteRisco() {
    pnCallback.PerformCallback('Transformar');
    onClick_btnCancelar();
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

}

function createEventHandler(funcName) {
    return new Function("event", funcName + "(event);");
}

