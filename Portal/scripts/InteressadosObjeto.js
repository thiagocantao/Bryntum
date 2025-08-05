// JScript File
var TipoOperacao = "Consultar";
var comando = '';

function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallbackDetalhe.PerformCallback(TipoOperacao);
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function AssociarUsuariosNoPerfil() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback("AssociarUsuariosNoPerfil");
    return false;
}

function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function IncluirNovoInteressado()
{
    TipoOperacao = "Incluir";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    LimpaCamposFormulario();
    lbListaPerfisSelecionados.ClearItems();
    lbListaPerfisDisponivel.ClearItems();
    btnSalvar.SetVisible(true);
    pnCallbackDetalhe.PerformCallback("IncluirNovo");
}

function onClickDetalheInteressado(grid, e)
{
    grid.GetRowValues(grid.GetFocusedRowIndex(),'CodigoUsuario;NomeUsuario;HerdaPermissoesObjetoSuperior;Perfis;IniciaisTipoAssociacao', MostraGridPermissaoDetalhe);
}

/*
<summary>
    CodigoUsuario           = 0     HerdaPermissoesObjetoSuperior   = 2     IniciaisTipoAssociacao  = 4
    NomeUsuario             = 1     Perfis                          = 3
</summary>
*/
function MostraGridPermissaoDetalhe(values)
{
        var codigoUsuario   = (values[0] != null ? values[0]  : "");
        var nomeUsuario     = (values[1] != null ? values[1]  : "");
        var herda           = (values[2] != null ? values[2]  : "");
        var perfis          = (values[3] != null ? values[3]  : "");
        var iniciaisTO      = (values[4] != null ? values[4]  : "");
        
		btnSalvar.SetVisible(false);
		TipoOperacao = "Consultar";
		if(window.hfGeral && hfGeral.Contains("TipoOperacao"))            hfGeral.Set("TipoOperacao", TipoOperacao);
        if(window.hfGeral && hfGeral.Contains("CodigoUsuarioPermissao"))  hfGeral.Set("CodigoUsuarioPermissao", codigoUsuario);
        
        lblCaptionInteressado.SetText(nomeUsuario);
        lblCaptionPerfil.SetText(perfis);
        lblCaptionHerda.SetText(herda == 'S' ? traducao.InteressadosObjeto_sim : traducao.InteressadosObjeto_n_o);
        
        if(pcPermissoes.GetVisible())
            CallBackGvPermissoes(iniciaisTO)
        else
		    pcPermissoes.Show();
}

/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function LimpaCamposFormulario()
{
    var tOperacao = ""

    try
    {// Função responsável por preparar os campos do formulário para receber um novo registro
        ddlInteressado.SetText("");
        lbListaPerfisSelecionados.ClearItems();
        lbListaPerfisDisponivel.ClearItems();

        if(window.hfGeral && hfGeral.Contains("CodigoObjetoAssociado"))     hfGeral.Set("CodigoObjetoAssociado", "-1");
        if(window.hfGeral && hfGeral.Contains("CodigoUsuarioPermissao"))    hfGeral.Set("CodigoUsuarioPermissao", "-1");
        if(window.hfGeral && hfGeral.Contains("HerdaPermissoes"))           hfGeral.Set("HerdaPermissoes", "S");
        if (window.hfGeral && hfGeral.Contains("CodigosPerfisSelecionados")) hfGeral.Set("CodigosPerfisSelecionados", "-1");
        
        checkHerdarPermissoes.SetChecked(true);
        if(TipoOperacao != "Incluir") callbackGeral.PerformCallback("CerrarSession");
    }catch(e){}
}

/*-------------------------------------------------
<summary>

</summary>
-------------------------------------------------*/
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(),'CodigoUsuario;NomeUsuario;CodigoObjetoAssociado;CodigoTipoAssociacao;IniciaisTipoAssociacao;HerdaPermissoesObjetoSuperior;Perfis', MontaCamposFormulario);
    }
}

function validaUsuarioNaLista(valores) {
    debugger;
    var nomeUsusario = valores[0];
}

/*-------------------------------------------------
<summary>

CodigoUsuario           = 0     IniciaisTipoAssociacao          = 4
NomeUsuario             = 1     HerdaPermissoesObjetoSuperior   = 5
CodigoObjetoAssociado   = 2     Perfis                          = 6
CodigoTipoAssociacao    = 3

</summary>
-------------------------------------------------*/
function MontaCamposFormulario(values)
{
    try
    {
   
        //------------Obtemgo os valores da grid.
        var codigoUsuario   = (values[0] != null ? values[0]  : "");
        var nomeUsuario     = (values[1] != null ? values[1]  : "");
        var codigoObjetoAssociado   = (values[2] != null ? values[2]  : "-1");
        var iniciaisTipoAssociacao  = (values[4] != null ? values[4]  : "");
        var herda           = (values[5] != null ? values[5]  : "");
        var perfis          = (values[6] != null ? values[6]  : "");
        
        //--------------Seteo a tela con os valores obtenidos
        ddlInteressado.SetText(nomeUsuario);
        checkHerdarPermissoes.SetChecked(herda == 'S' ? true : false);
        
        lblCaptionInteressado.SetText(nomeUsuario);
        lblCaptionPerfil.SetText(perfis);
        lblCaptionHerda.SetText(herda == 'S' ? 'Sim' : 'Não');

        if(window.hfGeral && hfGeral.Contains("TipoOperacao"))
            hfGeral.Set("TipoOperacao", TipoOperacao);
        if(window.hfGeral && hfGeral.Contains("CodigoObjetoAssociado"))
            hfGeral.Set("CodigoObjetoAssociado", codigoObjetoAssociado);
        if(window.hfGeral && hfGeral.Contains("CodigoUsuarioPermissao"))
            hfGeral.Set("CodigoUsuarioPermissao", codigoUsuario);
        if(window.hfGeral && hfGeral.Contains("HerdaPermissoes"))
            hfGeral.Set("HerdaPermissoes", herda);

        if(TipoOperacao != "Consultar")
            lbListaPerfisDisponivel.PerformCallback(codigoUsuario);
       
        lbListaPerfisSelecionados.PerformCallback(codigoUsuario);
      }
      catch(e){/* ups! acontecio um error :( */}
}

function ObtemListaPerfisDisponiveis(codigoUsuario) {
    lbListaPerfisDisponivel.ClearItems();
    lbListaPerfisDisponivel.PerformCallback(codigoUsuario);
}

function MontaCamposPopupUsuarios(valores) {

   
    var CodigoPerfil = valores[0];
    var DescricaoPerfil = valores[1];

    txtNomePerfil.SetText(DescricaoPerfil);

    lbListaUsuariosDisponivel.ClearItems();
    lbListaUsuariosSelecionados.ClearItems();

    lbListaUsuariosDisponivel.PerformCallback(CodigoPerfil);
    pcDadosUsuarios.Show();
}
/*-------------------------------------------------
<summary>
</summary>
-------------------------------------------------*/
function onClick_Cancelar(s, e)
{
    pcDados.Hide();
    callbackGeral.PerformCallback("CerrarSession");
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
        
    var parametroCalbackGvPermissao = iniciaisTO + ";" + hfGeral.Get("CodigoUsuarioPermissao") + ";";
    gvPermissoes.PerformCallback(parametroCalbackGvPermissao);
}

function getIniciaisAssociacaoFromTextoMenu(textoMenu)
{
    /* EN UN ST PR ME PP TM OB IN CT EQ*/
    var retorno = "";

    if (textoMenu == "mnEntidade")          retorno = "EN";
    else if (textoMenu == "mnUnidades")     retorno = "UN";
    else if (textoMenu == "mnEstrategia")   retorno = "ST";
    else if (textoMenu == "mnProjeto")      retorno = "PR";
    else if (textoMenu == "mnMapas")        retorno = "ME";
    else if (textoMenu == "mnPerspectiva")  retorno = "PP";
    else if (textoMenu == "mnTema")         retorno = "TM";
    else if (textoMenu == "mnObjetivo")     retorno = "OB";
    else if (textoMenu == "mnIndicador")    retorno = "IN";
    else if (textoMenu == "mnContrato")    retorno = "CT";
    else if (textoMenu == "mnDemandaComplexa")    retorno = "DC";
    else if (textoMenu == "mnDemandaSimple")    retorno = "DS";
    else if (textoMenu == "mnProcesso") retorno = "PC";
    else if (textoMenu == "mnCarteira") retorno = "CP";
    else if (textoMenu == "mnEquipe") retorno = "EQ";

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
/*
function onClick_CheckConceder(s, e)
{
    if( true == s.GetChecked())
    {
        chkNegar.SetChecked(false);
        chkExtensivel.SetChecked(false);
        //chkIncondicional.SetEnabled(true);
    }
    else
    {
        chkExtensivel.SetChecked(false);
    }
    e.processOnServer = false; // registro no parâmetro que não é para processar nada no server
}

function onClick_CheckExtensivel(s, e)
{
    if( true == s.GetChecked())
    {
        chkNegar.SetChecked(false);
        chkConceder.SetChecked(true);
    }
    e.processOnServer = false; // registro no parâmetro que não é para processar nada no server
}

function onClick_CheckNegar(s, e)
{
    if( true == s.GetChecked())
    {
        chkConceder.SetChecked(false);
        chkExtensivel.SetChecked(false);
    }
    e.processOnServer = false; // registro no parâmetro que não é para processar nada no server
}
*/

function verificarDadosPreenchidos()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    
    if(ddlInteressado.GetValue() == null || ddlInteressado.GetValue() == "")
    {
        mensagemErro_ValidaCamposFormulario = traducao.InteressadosObjeto_o_usuario_para_a_permiss_o_deve_ser_informado_;
        ddlInteressado.Focus();
    }

    return mensagemErro_ValidaCamposFormulario;
}


//function onPopup_pcDados()
//{
//    if(TipoOperacao == "Editar")
//	    callbackGeral.PerformCallback("editarPermissao");
//}


function desabilitaHabilitaComponentes()
{
	var BoolEnabled = hfGeral.Get("TipoOperacao") != "Consultar";
	
    ddlInteressado.SetEnabled(BoolEnabled);
    checkHerdarPermissoes.SetEnabled(BoolEnabled);
    btnSalvarPerfis.SetVisible(BoolEnabled);
    
    if(hfGeral.Get("TipoOperacao") == "Editar")
        ddlInteressado.SetEnabled(false);
}

//------------------------------------------------------------ funções relacionadas com a ListBox
var delimitadorLocal = ";";

function UpdateButtons() 
{
    btnADD.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaPerfisDisponivel.GetSelectedItem() != null);
    btnADDTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaPerfisDisponivel.GetItemCount() > 0 );
    btnRMV.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaPerfisSelecionados.GetSelectedItem() != null);
    btnRMVTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaPerfisSelecionados.GetItemCount() > 0);
    capturaCodigosPerfisSelecionados();
}

function UpdateButtonsUsuarios() {
    btnADDUsuarios.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaUsuariosDisponivel.GetSelectedItem() != null);
    btnADDTodosUsuarios.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaUsuariosDisponivel.GetItemCount() > 0);
    btnRMVUsuarios.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaUsuariosSelecionados.GetSelectedItem() != null);
    btnRMVTodosUsuarios.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbListaUsuariosSelecionados.GetItemCount() > 0);
    capturaCodigosUsuariosSelecionados();
}

function capturaCodigosPerfisSelecionados()
{
    var CodigosPerfisSelecionados = "";
    for (var i = 0; i < lbListaPerfisSelecionados.GetItemCount(); i++) 
    {
        CodigosPerfisSelecionados += lbListaPerfisSelecionados.GetItem(i).value + delimitadorLocal;
    }
    hfGeral.Set("CodigosPerfisSelecionados", CodigosPerfisSelecionados);
    
    if(lbListaPerfisSelecionados.GetItemCount()>0 && (ddlInteressado.GetValue() != null && ddlInteressado.GetText() != "") )
    {
        btnSeleccionarPermissao.SetEnabled(true);
        btnSalvarPerfis.SetEnabled(true);
    }
    else
    {
        btnSeleccionarPermissao.SetEnabled(false);
        btnSalvarPerfis.SetEnabled(false);
    }
}

function capturaCodigosUsuariosSelecionados() {
    var CodigosSelecionados = "";
    for (var i = 0; i < lbListaUsuariosSelecionados.GetItemCount(); i++) {
        CodigosSelecionados += lbListaUsuariosSelecionados.GetItem(i).value + delimitadorLocal;
    }
    hfGeral.Set("CodigosUsuariosSelecionados", CodigosSelecionados);
}

function onValueChanged_lbListaPerfisSelecionados(s, e)
{
    if(s.GetItemCount()>0 && (ddlInteressado.Value != null || ddlInteressado.Value != ""))
    {
        btnSeleccionarPermissao.SetEnabled(true);
        btnSalvarPerfis.SetEnabled(true);
    }
    else
    {
        btnSeleccionarPermissao.SetEnabled(false);
        btnSalvarPerfis.SetEnabled(false);
    }    
}

//------------------------------------------------------------ CHECKBOX PERMISSÕES
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

function clicaRestrincao(valor)
{
    gvDados.PerformCallback(valor);
}

function mostraPopupMensagemGravacao(acao)
{
    window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    onClick_Cancelar();
}

function onClick_Cancelar()
{
    habilitaModoEdicaoBarraNavegacao(false, gvDados);
    pcDados.Hide();
    pcPermissoes.Hide();
    TipoOperacao = "Consultar";
    hfGeral.Set("TipoOperacao", TipoOperacao);
    return true;
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
