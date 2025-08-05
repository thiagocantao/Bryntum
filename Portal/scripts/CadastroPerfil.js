var TipoOperacao = "Consultar";

function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    if(window.hfGeral && hfGeral.Contains("TipoOperacao"))
        TipoOperacao = hfGeral.Get("TipoOperacao");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function DesativarRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao + "Desativar");
    return false;
}

function ReativarRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao + "Reativar");
    return false;
}

function IncluirNovoRegistroPerfil()
{
    tcDados.SetActiveTab(tcDados.GetTab(0));
    TipoOperacao = 'Incluir';
    hfGeral.Set("TipoOperacao", TipoOperacao);
    onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
    desabilitaHabilitaComponentes();
    btnSalvarPerfil.SetVisible(true);
    hfGeral.Set("CodigoPerfil", "-1");
    //tcDados.tabs[1].visible = false;
}

function onClick_Cancelar()
{
    habilitaModoEdicaoBarraNavegacao(false, gvDados);
    pcDados.Hide();
    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    return true;
}

function desabilitaHabilitaComponentes()
{
    ddlTipoObjeto.SetEnabled(TipoOperacao != "Consultar");
    txtNomePerfil.SetEnabled(TipoOperacao != "Consultar");
    mmObservacao.SetEnabled(TipoOperacao != "Consultar");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(),'CodigoPerfil;CodigoEntidade;CodigoTipoAssociacao;IniciaisPerfil;DescricaoPerfil_PT;ObservacaoPerfil;StatusPerfil;DescricaoTipoAssociacao;IniciaisTipoAssociacao', MontaCamposFormulario);
}

function verificaAtivacao(values)
{
    TipoOperacao = 'Excluir';
    if (values[0] == "A")
    {
        var funcObj = { funcaoClickOK: function () { DesativarRegistroSelecionado(); } }
        window.top.mostraConfirmacao(traducao.CadastroPerfil_deseja_realmente_desativar_o_perfil_, function () { funcObj['funcaoClickOK']() }, null);	        
    }
    else
    {
        var funcObj = { funcaoClickOK: function(){ ReativarRegistroSelecionado(); } }
        window.top.mostraConfirmacao(traducao.CadastroPerfil_deseja_realmente_ativar_o_perfil_, function () { funcObj['funcaoClickOK']() }, null);
    }
                    
}

function MontaCamposFormulario(values)
{
    /*
    CodigoPerfil            0       ObservacaoPerfil        5
    CodigoEntidade          1       StatusPerfil            6
    CodigoTipoAssociacao    2       DescricaoTipoAssociacao 7
    IniciaisPerfil          3       IniciaisTipoAssociacao  8
    DescricaoPerfil_PT      4
    */

    LimpaCamposFormulario();
    
    if(values)
    {
        var CodigoPerfil            = values[0] != null ? values[0].toString()  : "-1";
        var CodigoEntidade          = values[1] != null ? values[1].toString()  : "-1";
        var CodigoTipoAssociacao    = values[2] != null ? values[2].toString()  : "-1";
        var IniciaisPerfil          = values[3] != null ? values[3].toString()  : "";
        var DescricaoPerfil_PT      = values[4] != null ? values[4].toString()  : "";
        var ObservacaoPerfil        = values[5] != null ? values[5].toString()  : "";
        var StatusPerfil            = values[6] != null ? values[6].toString()  : "";
        var DescricaoTipoAssociacao = values[7] != null ? values[7].toString()  : "";
        var IniciaisTipoAssociacao  = values[8] != null ? values[8].toString()  : "";
        
     
        if(window.hfGeral && hfGeral.Contains("TipoOperacao"))
            hfGeral.Set("TipoOperacao", TipoOperacao);
        if(window.hfGeral && hfGeral.Contains("CodigoPerfil"))
            hfGeral.Set("CodigoPerfil",CodigoPerfil);
            
        ddlTipoObjeto.SetValue(CodigoTipoAssociacao);
        txtNomePerfil.SetText(DescricaoPerfil_PT);
        mmObservacao.SetText(ObservacaoPerfil);
        mmObservacao.Validate();

        lblCaptionPerfil.SetText(DescricaoTipoAssociacao);
        //btnSalvarPerfil.SetVisible(TipoOperacao != "Consultar"? true:false);
        
       // btnSalvar.SetVisible(TipoOperacao != "Consultar"? true:false);
        btnSalvarPerfil.SetVisible(TipoOperacao != "Consultar"? true:false);
     
        if(TipoOperacao == "Consultar" || TipoOperacao == "")
        {
            ddlTipoObjeto.SetEnabled(false);
            txtNomePerfil.SetEnabled(false);
            mmObservacao.SetEnabled(false);
        }
        if(TipoOperacao == "Editar")
        {
            ddlTipoObjeto.SetEnabled(false);
            txtNomePerfil.SetEnabled(true);
            mmObservacao.SetEnabled(true);
        }
        
        hfGeral.Set("textoItem", IniciaisTipoAssociacao);
        pnCallbackPermissoes.PerformCallback(IniciaisTipoAssociacao);
    }
}

function verificarDadosPreenchidos()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    if (ddlTipoObjeto.GetEnabled() == true && isNaN(ddlTipoObjeto.GetValue()) == true)
    {

        mensagemErro_ValidaCamposFormulario += "\n" + traducao.CadastroPerfil_o_tipo_de_objeto_deve_ser_informado_;
    }

    if(txtNomePerfil.GetText() == "")
    {
        mensagemErro_ValidaCamposFormulario += "\n" + traducao.CadastroPerfil_o_nome_do_perfil_deve_ser_informado_;
    }

    return mensagemErro_ValidaCamposFormulario;
}

/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function LimpaCamposFormulario()
{
    try
    {// Função responsável por preparar os campos do formulário para receber um novo registro
       
        ddlTipoObjeto.SetValue("");
        txtNomePerfil.SetText("");
        mmObservacao.SetText("");
        mmObservacao.Validate();
        lblCaptionPerfil.SetText("");

        if(window.hfGeral && hfGeral.Contains("CodigoPerfil"))
            hfGeral.Set("CodigoPerfil", "-1");
    }
    catch(e){}
}

/*-------------------------------------------------
<summary>
Evento que se dispara ao fazer click no menu nbAssociacao.
Utiliza o nome do item do menu, para enviarlo ao callback da grid gvPermissoes
para recargar las permissões correspondientes.
</summary>
-------------------------------------------------*/
function onClick_MenuPermissao(s, e)
{
    e.processOnServer = false;
    
    var textoItem = getIniciaisAssociacaoFromTextoMenu(e.item.name);
    CallBackGvPermissoes(textoItem);
}

function CallBackGvPermissoes(iniciaisTO)
{
    if(window.hfGeral && window.hfGeral.Contains('itemMenu'))
        hfGeral.Set('itemMenu', iniciaisTO);
        
    gvPermissoes.PerformCallback(iniciaisTO);
}

function getIniciaisAssociacaoFromTextoMenu(textoMenu)
{
    var retorno = "";
    
    if(textoMenu == "mnEntidade")         retorno = "EN";
    else if(textoMenu == "mnUnidades")    retorno = "UN";
    else if(textoMenu == "mnEstrategia")  retorno = "ST";
    else if(textoMenu == "mnProjeto")     retorno = "PR";
    else if(textoMenu == "mnMapas")       retorno = "ME";
    else if(textoMenu == "mnPerspectiva") retorno = "PP";
    else if(textoMenu == "mnTema")        retorno = "TM";
    else if(textoMenu == "mnObjetivo")    retorno = "OB";
    else if(textoMenu == "mnIndicador")   retorno = "IN";
    else if(textoMenu == "mnContrato")    retorno = "CT";
    else if(textoMenu == "mnDemandaComplexa")   retorno = "DC";
    else if(textoMenu == "mnDemandaSimple")     retorno = "DS";
    else if(textoMenu == "mnProcesso")          retorno = "PC";
    //TODO: Alterado Eduardo.Rocha
    else if (textoMenu == "mnCarteiraProjeto") retorno = "CP";
    else if (textoMenu == "mnEquipe") retorno = "EQ";
    else if (textoMenu == "mnRiscosCorporativos") retorno = "R1";
    return retorno;
}


/*-------------------------------------------------
<summary>Manipula os CheckEdit da grid gvPermissao.
Aplica la regla sigente:
- checkEditConceder(true)   entonces checkEditNegar(false)
- checkEditConceder(false)  entonces checkEditNegar(false)
- checkEditExtensivel(true) entonces checkEditConceder(true) e checkEditNegar(false)
- checkEditNegar(true)      entonces checkEditExtensivel(false) e checkEditConceder(false)
</summary>
-------------------------------------------------*/
function clicaConceder(codigo, valor, op)
{
    var checkConcedido = document.getElementById('CheckConcedido');
    var checkExtensivel = document.getElementById('CheckDelegavel');
    var checkNegar = document.getElementById('CheckNegado');
    
    if(op == "C"){ }
    else if(op == "D") { }
    else if(op == "N") { }
        
    callbackConceder.PerformCallback(codigo + ';' + valor + ';' + op);
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function podeMudarAba(s, e)
{
    var codigoPerfil = parseFloat(hfGeral.Get("CodigoPerfil"));
     //btnSalvar.SetVisible(TipoOperacao != "Consultar"? true:false);
     btnSalvarPerfil.SetVisible(TipoOperacao != "Consultar"? true:false);
     //btnCancelar.SetText("Fechar");
    
    if (e.tab.index == 0)
    {
        return false;
    }
        
    if(e.tab.index == 1)
    {
        if (isNaN(codigoPerfil) || codigoPerfil.length <= 0 || parseFloat(codigoPerfil)<0)
        {
            window.top.mostraMensagem(traducao.CadastroPerfil_para_ter_acesso_a + " \"" + e.tab.GetText() + "\", " + traducao.CadastroPerfil___obrigat_rio_salvar_as_informa__es_de + " \"" + tcDados.GetTab(0).GetText() + "\".", 'Atencao', true, false, null);
             return true;
        }
        else
        {
            gvPermissoes.PerformCallback(hfGeral.Get("textoItem"));
        }
    }


   
    if (isNaN(codigoPerfil) || codigoPerfil.length <= 0 || parseFloat(codigoPerfil)<0)
    {
        window.top.mostraMensagem(traducao.CadastroPerfil_para_ter_acesso_a + " \"" + e.tab.GetText() + "\", " + traducao.CadastroPerfil___obrigat_rio_salvar_as_informa__es_de + " \"" + tabControl.GetTab(0).GetText() + "\".", 'Atencao', true, false, null);
        return true;
    }

    if(!window.TipoOperacao)
        TipoOperacao = "Consultar";

    return false;
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/

//--------------------------------------bloco de scripts da copia de perfil

function trataClickBotaoCopiarPerfil(s) {
    s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoPerfil;DescricaoPerfil_PT;CodigoTipoAssociacao', auxTrataClickBotaoCopiarPerfil)

}

function auxTrataClickBotaoCopiarPerfil(valores) {
    if (null != valores) {
        var codigoPerfil = valores[0];
        var nomePerfil = valores[1];
        var codigoTipoAssociacao = valores[2];
        var continuar = true;
        hfStatusCopiaPerfil.Set("CodigoPerfilDestino", codigoPerfil);
        hfStatusCopiaPerfil.Set("NomePerfilDestino", nomePerfil);
        hfStatusCopiaPerfil.Set("CodigoTipoAssociacaoDestino", codigoTipoAssociacao);
        callbackPerfilOrigem.PerformCallback();
        pcCopiaPermissoes.Show();
    }
}

function validaCamposCopiaPerfil() {
    //debugger
    var retorno = true;
    var mensagemAlert = "";

    if (ddlPerfilOrigem.GetValue() == null || ddlPerfilOrigem.GetValue() == -1) {
        mensagemAlert += traducao.CadastroPerfil_informe_o_perfil_que_vai_fornecer_o_conjunto_de_permiss_es_para_a_c_pia + "\n";
        retorno = false;
    }
    else {
        hfStatusCopiaPerfil.Set("CodigoPerfilOrigem", ddlPerfilOrigem.GetValue());
        hfStatusCopiaPerfil.Set("NomePerfilOrigem", ddlPerfilOrigem.GetText());
    }

    if (retorno == false) {
        window.top.mostraMensagem(mensagemAlert, 'erro', true, false, null);
    }
    return retorno;
}



function onClick_btnSalvarCopiaPermissoes() {
    if (validaCamposCopiaPerfil()) {
        var sentenca = traducao.CadastroPerfil_perfil_origem__ + hfStatusCopiaPerfil.Get("NomePerfilOrigem") + '\n' + traducao.CadastroPerfil_perfil_destino__ + hfStatusCopiaPerfil.Get("NomePerfilDestino") + '\n\n' + traducao.CadastroPerfil_confirma_a_c_pia_do_conjunto_de_permiss_es__n_nesta_opera__o_n_o_pode_ser_desfeita_;
        window.top.mostraMensagem(sentenca, 'confirmacao', true, true, executaCopiaPerfil);
    }
    return false;
}

    


function executaCopiaPerfil() {
    hfStatusCopiaPerfil.PerformCallback("Editar");
}

function onClick_btnCancelarCopiaPermissoes() {
    pcCopiaPermissoes.Hide();
    return true;
}

function hfStatusCopiaPerfil_onEndCallback() {
    if (hfStatusCopiaPerfil.Get("StatusSalvar") == "1") {
        if (window.posSalvarComSucessoCopiaPerfil)
            window.posSalvarComSucessoCopiaPerfil();
        gvDados.PerformCallback("");
        onClick_btnCancelarCopiaPerfil();
    }
    else if (hfStatusCopiaPerfil.Get("StatusSalvar") == "0") {
        mensagemErro = hfStatusCopiaPerfil.Get("ErroSalvar");
        mostraPopupMensagemGravacaoCopiaPerfil(mensagemErro);

    }
}

function pcCopiaPermissoes_OnPopup(s, e) {
    // limpa o hidden field com a lista de status
    //hfStatusCopiaFluxo.Clear();
    lblPerfilAlvo.SetText(traducao.CadastroPerfil_perfil_que_vai_receber_o_conjuto_de_permiss_es__ + hfStatusCopiaPerfil.Get("NomePerfilDestino") + ".");
    lblCopia.SetText(traducao.CadastroPerfil_importante__o_perfil_alvo_inicialmente_perde_todas_as_permiss_es_que_tem__e_em_seguida_recebe_as_permiss_es_do_perfil_origem_selecionado_abaixo_);

    ddlPerfilOrigem.SetSelectedIndex(-1);

}

function posSalvarComSucessoCopiaPerfil() {
    mostraPopupMensagemGravacaoCopiaPerfil(traducao.CadastroPerfil_dados_gravados_com_sucesso_);
}

//-------
function mostraPopupMensagemGravacaoCopiaPerfil(acao) {
    window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    onClick_btnCancelarCopiaPermissoes();
}
//fim do bloco da copia
