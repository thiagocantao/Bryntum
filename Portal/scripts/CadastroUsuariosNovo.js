// JScript File
var mensagemErro = "";
var EsRecursoCorporativo = "";
var mensagemCPFJaExiste = "";
//======================================
//      AÇÕES COM O BOTÕES.
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function SalvarCamposFormulario() {
   
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    hfGeral.Set("RowFocusChanged", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function ExcluirRegistroSelecionado() {
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    hfGeral.Set("RowFocusChanged", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}


//======================================
//      AÇÕES DE VALIDAÇÃO.
//--------------------------------------

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function validateEmail(elementValue) {
    var emailPattern = /^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]{2,})*?(\.[a-z]{2,6})?)$/;
    return emailPattern.test(elementValue);
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function validaCamposFormulario(confirmacaoEmailInvalido=false) {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 1;
    var eMail = txtEmail.GetText();
    var bIncluir = (TipoOperacao == "Incluir");
    var cpf = txtCPF.GetValue();

    if (eMail != "" && !confirmacaoEmailInvalido) {
        if (!validateEmail(eMail)) {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.CadastroUsuariosNovo_por_favor__forne_a_um_endere_o_de_e_mail_v_lido_ + "\n";
        }
    }

    if ("" == txtNomeUsuario.GetText()) {
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.CadastroUsuariosNovo_o_nome_do_usu_rio_deve_ser_informado_ + "\n";
    }
    if (cpf != null && cpf.length > 0) {
        if (valida_cpf(cpf) == false) {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.CadastroUsuariosNovo_cpf_inv_lido_ + "\n";
        }
        else if (mensagemCPFJaExiste != '')
        {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + mensagemCPFJaExiste + "\n";
        }
    }
    if ("AI" == cmbTipoAutentica.GetValue()) {
        if (txtLogin.GetText() == "") {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.CadastroUsuariosNovo_o_nome_do_login_deve_ser_informado__utilize_a_formata__o_de_acordo_com_a_configura__o_do_seu_servidor + "\n";
        }
    }

    if ("AP" == cmbTipoAutentica.GetValue()) {
        if (txtLogin.GetText() == "") {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.CadastroUsuariosNovo_o_nome_do_login_deve_ser_informado__utilize_a_formata__o_de_acordo_com_a_configura__o_do_seu_servidor + "\n";
        }
    }

    if (bIncluir) {
        if ("" == cmbPerfil.GetText()) {
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.CadastroUsuariosNovo_o_perfil_deve_ser_informado_ + "\n";
        }
    }

    return mensagemErro_ValidaCamposFormulario;
}

function validarPermissoesUsuario() { }



//======================================
//      AÇÕES DE COMPONENTES DA TELA.
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function LimpaCamposFormulario() {
    // Função responsável por preparar os campos do formulário para receber um novo registro

    // passa null para verficaPermissao p/ que os campos fiquem habilitados
    verificaPermissaoEdicaoRegistro(null, true);
    hfGeral.Set("emailAnterior", "");
    txtNomeUsuario.SetText("");
    txtEmail.SetText("");
    txtTelefoneContato1.SetText("");
    txtTelefoneContato2.SetText("");
    txtLogin.SetText("");
    cmbTipoAutentica.SetValue("AI");
    cmbPerfil.SetText("");
    memObservacoes.SetText("");
    memObservacoes.Validate();
    ckbAtivo.SetChecked(true);
    txtCPF.SetValue("");
    desabilitaHabilitaComponentes();
    hfGeral.Set("CodigoUsuarioAtual", -1);
}

/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function verificaPermissaoEdicaoRegistro(codigoEntidadeResponsavel, bPopup) {
    var bPodeEditar = true;
    lblMensagemOutraEntidade.SetVisible(false);
    if ((null != codigoEntidadeResponsavel) && hfGeral.Contains("CodigoEntidadeAtual")) {
        // se a entidade responsável pelo usuário não a entidade atual, não pode editar.
        if (hfGeral.Get("CodigoEntidadeAtual") != codigoEntidadeResponsavel) {
            lblMensagemOutraEntidade.SetVisible(true);
            bPodeEditar = false;
        }
    }
    // imgBtnExcluir é o botão editar da barra de navegação.
    if (bPodeEditar) {
        try { imgBtnExcluir.SetImageUrl(PathImagesUrl + '/pExcluir.png'); } catch (e) { }
        try { imgBtnExcluir.SetEnabled(true); } catch (e) { }
    }
    else {
        try { imgBtnExcluir.SetImageUrl(PathImagesUrl + '/pExcluirDisabled.png'); } catch (e) { }
        try { imgBtnExcluir.SetEnabled(false); } catch (e) { }
    }

    // se estiver mostrando o popup
    if (bPopup) {
        var bConsulta = (TipoOperacao == "Consultar");
        var bIncluir = (TipoOperacao == "Incluir");
        bPodeEditar = bPodeEditar && !bConsulta;

        txtNomeUsuario.SetEnabled(bPodeEditar);
        txtLogin.SetEnabled(bPodeEditar);
        txtTelefoneContato1.SetEnabled(bPodeEditar);
        txtTelefoneContato2.SetEnabled(bPodeEditar);
        cmbTipoAutentica.SetEnabled(bPodeEditar);


        memObservacoes.SetEnabled(bPodeEditar);
        btnRedefinirSenha.SetVisible(bPodeEditar);


        // ativar e desativar será a única coisa permitida independentemente da unidade responsavel
        ckbAtivo.SetEnabled(!bConsulta);

        if (bPodeEditar)
            habilitaLogim();
        else
            txtLogin.SetEnabled(false);

        if (bIncluir) {
            cmbPerfil.SetEnabled(true);
            cmbPerfil.SetVisible(true);
            lblPerfil.SetVisible(true);
        }
        else {
            cmbPerfil.SetVisible(false);
            lblPerfil.SetVisible(false);
        }

    }
}



//======================================
//      AÇÕES COM A GRID.
//--------------------------------------


/*-------------------------------------------------
<summary></summary>
-------------------------------------------------*/
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    
    var rowFocus = "1";
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    // rowFocusChanged é desabilitado nos processamentos de gravação
    if (hfGeral.Contains("RowFocusChanged"))
        rowFocus = hfGeral.Get("RowFocusChanged");
    //grid.Refresh();
    if (window.pcDados && pcDados.GetVisible() == true)
        grid.GetSelectedFieldValues('NomeUsuario;ContaWindows;TipoAutenticacao;EMail;TelefoneContato1;TelefoneContato2;Observacoes;IndicaUsuarioAtivoUnidadeNegocio;CodigoUsuario;CodigoEntidadeResponsavelUsuario;CPF', MontaCamposFormulario);
    else
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoEntidadeResponsavelUsuario', verificaPermissaoEdicaoRegistro);

}


/*-------------------------------------------------
<summary>Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente.</summary>
-------------------------------------------------*/
function MontaCamposFormulario(values) {
    
    LimpaCamposFormulario();
    if (values.length > 0) {
        var celda = document.getElementById("tdRedefinirSenha");
        txtNomeUsuario.SetText(values[0][0].toString());

        if (values[0][1] != null)
            txtLogin.SetText(values[0][1].toString());
        else
            txtLogin.SetText("");

        cmbTipoAutentica.SetValue(values[0][2].toString());
        txtEmail.SetText(values[0][3].toString());
        hfGeral.Set("emailAnterior", values[0][3].toString());

        if (values[0][4] != null)
            txtTelefoneContato1.SetText(values[0][4].toString());

        if (values[0][5] != null)
            txtTelefoneContato2.SetText(values[0][5].toString());

        if (values[0][6] != null) {
            memObservacoes.SetText(values[0][6]);
            memObservacoes.Validate();
        }


        if (values[0][7] != null)
            ckbAtivo.SetChecked(values[0][7].toString() == "S");

        //*********************************************************
        hfGeral.Set("CodigoUsuarioAtual", values[0][8]);

        //habilitaLogim();
        var estado = "";

        if (window.hfGeral)
            var estado = hfGeral.Get("TipoOperacao");

        if (estado == "Consultar") {
            btnSalvar1.SetVisible(false);
            btnRedefinirSenha.SetVisible(false);
        }

        verificaPermissaoEdicaoRegistro(values[0][9], true);

        if (estado == "Editar") {
            hfGeral.Set("AutenticacaoLoad", values[0][2].toString());
            if (values[0][2].toString() == "AS")
                btnRedefinirSenha.SetVisible(true);
            else
                btnRedefinirSenha.SetVisible(false);
        }
        else
            btnRedefinirSenha.SetVisible(false);

        txtCPF.SetValue(values[0][10]);
        pnCPF.PerformCallback(values[0][8] + "|" + estado + "|" + values[0][9]);
        pnHelp.PerformCallback(values[0][8] + "|" + estado + "|" + values[0][9]);
    }
    else {
        hfGeral.Set("CodigoUsuarioAtual", -1);
    }
}



function salvarPermissoesUsuario() {
    hfGeral.Set("StatusSalvar", "0");
    hfGeral.Set("RowFocusChanged", "0");
    pnCallback.PerformCallback("editarPermissoes");
    return false;
}




// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso() {
    // se já incluiu alguma opção, feche a tela após salvar
    //    if (gridDescricao.GetVisibleRowsOnPage() > 0 )
    //        onClick_btnCancelar();    
}

// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------

function SalvarCamposEntidadeAtual() {
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack

    hfGeral.Set("StatusSalvar", "0");
    hfGeral.Set("RowFocusChanged", "0");
    pnCallback.PerformCallback("IncluirEntidadeAtual");
    return false;
}



//--****


function habilitaLogim() {
    if (TipoOperacao == "Editar") {
        if (cmbTipoAutentica.GetValue() == "AS")//autenticação pelo sistema (e-mail)
        {
            txtLogin.SetEnabled(false);
        }
        else
            txtLogin.SetEnabled(true);
    }
    else {
        if (TipoOperacao == "Incluir")//Se o tipo for Incluir, na função LimpaCamposFormulario() já vem marcado AI
        {
            txtLogin.SetEnabled(true);
        }
        else
            txtLogin.SetEnabled(false);
    }

    pnCallbackFormulario.cp_SituacaoEmail;
}



//======================================
//      AÇÕES DE CONTROLE DOS COMPONENTE DA TELA.
//--------------------------------------



function desabilitaHabilitaComponentes() {
    //var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;
    
    if ("Incluir" == TipoOperacao) {
        //btnVerificarEmail.SetVisible(true);
        hlkVerificarEmail.SetVisible(true);
        hlkVerificarEmail.SetEnabled(false);
        txtEmail.SetEnabled(true);

        txtNomeUsuario.SetEnabled(false);
        txtCPF.SetEnabled(false);
        txtLogin.SetEnabled(false);
        txtTelefoneContato1.SetEnabled(false);
        txtTelefoneContato2.SetEnabled(false);
        cmbTipoAutentica.SetEnabled(false);
        cmbPerfil.SetEnabled(false);
        lblPerfil.SetVisible(true);
        memObservacoes.SetEnabled(false);

    }
    else if ("Editar" == TipoOperacao) {
        //btnVerificarEmail.SetVisible(false);
        hlkVerificarEmail.SetVisible(true);
        txtEmail.SetEnabled(true);
        txtNomeUsuario.SetEnabled(true);
        txtLogin.SetEnabled(true);
        txtTelefoneContato1.SetEnabled(true);
        txtTelefoneContato2.SetEnabled(true);
        cmbTipoAutentica.SetEnabled(true);
        cmbPerfil.SetVisible(false);
        lblPerfil.SetVisible(false);
        memObservacoes.SetEnabled(true);
    }
    else {
        //btnVerificarEmail.SetVisible(false);
        hlkVerificarEmail.SetVisible(false);
        txtEmail.SetEnabled(false);

        txtNomeUsuario.SetEnabled(false);
        //txtCPF.SetEnabled(false);
        txtLogin.SetEnabled(false);
        txtTelefoneContato1.SetEnabled(false);
        txtTelefoneContato2.SetEnabled(false);
        cmbTipoAutentica.SetEnabled(false);
        cmbPerfil.SetVisible(false);
        lblPerfil.SetVisible(false);
        memObservacoes.SetEnabled(false);
    }
}

function statusComponentesAposVerificacaoEmail() {
    emailAlterado = false;
    txtEmail.SetEnabled(false);
    txtNomeUsuario.SetEnabled(true);
    txtCPF.SetEnabled(true);
    txtTelefoneContato1.SetEnabled(true);
    txtTelefoneContato2.SetEnabled(true);
    cmbPerfil.SetEnabled(true);
    ckbAtivo.SetEnabled(true);
    cmbTipoAutentica.SetEnabled(true);
    txtLogin.SetEnabled(true);
    memObservacoes.SetEnabled(true);
    hlkVerificarEmail.SetEnabled(true);
}

/*-------------------------------------------------
<summary>
setea o conteudo del label do popup 'pcMensagemVarios', apos 
lança o evento Show().
</summary>
<return></return>
<Parameters>
mensagem: texto a ser mostrado.
</Parameters>
-------------------------------------------------*/
function verificarEmailUsuarioNovo() {
    var retorno = true;
    var msgEmail = "";
    var eMail = txtEmail.GetText();

    if ("" != eMail) {
        if (!validateEmail(eMail))
            msgEmail = traducao.CadastroUsuariosNovo_por_favor__forne_a_um_endere_o_de_e_mail_v_lido;
    }
    else
        msgEmail = traducao.CadastroUsuariosNovo_por_favor__forne_a_um_endere_o_de_e_mail_v_lido;

    if ("" != msgEmail) {
        lancarMensagemSistema(msgEmail);
        retorno = false;
    }
    
    return retorno;
}

/*-------------------------------------------------
<summary>
setea o conteudo del label do popup 'pcMensagemVarios', apos 
lança o evento Show().
</summary>
<return></return>
<Parameters>
mensagem: texto a ser mostrado.
</Parameters>
-------------------------------------------------*/
function verificarConfirmacaoEmailUsuarioNovo() {
    var retorno = true;
    var msgEmail = "";
    var eMail = txtEmail.GetText();
    
    if ("" != eMail) {
        if (!validateEmail(eMail)) {
            msgEmail = traducao.CadastroUsuariosNovo_confirmar_um_endere_o_de_e_mail_v_lido;
        }
    }
    else
        msgEmail = traducao.CadastroUsuariosNovo_por_favor__forne_a_um_endere_o_de_e_mail_v_lido;

    if ("" != msgEmail) {
        if ("Editar" == hfGeral.Get("TipoOperacao"))
            hfNovoEmailUsuario.Set("emailVerificadoUsuarioNovo", "ALTE");
        else
            hfNovoEmailUsuario.Set("emailVerificadoUsuarioNovo", "I");
        hfNovoEmailUsuario.Set("tipoConfirmacao", "");
        lblMensagemConfirmacaoEmail.SetText(msgEmail);
        pcMensagemConfirmacaoEmail.Show();
        retorno = false;
    }

    return retorno;
}

/*-------------------------------------------------
<summary>
Ao inserir um novo usuário, o primeiro dado que se digita é o email. 
Uma função se encarregasse de analisar o email digitado com a base de dados.
Dois estados são considerados, se o email não existe na 
base (em nenhuma situação como ativo, não ativo, excluído, etc), ou se 
existe o email (em todas as situações como ativo, não ativo, excluído, etc).
Se existe terá, segundo sua existência, os seguintes parâmetros:

EXOE: existe em outra entidade.
EXNE: existe nesta entidade.
ATOE: ativo em outra entidade.
ATNE: ativo nesta entidade.
I: inexistente.

Segundo o estado, tendra situações de resultado diferentes.
</summary>
<return></return>
<Parameters>
s: Componente HiddenField 'hfNovoEmailUsuario'.
e: Propiedade HiddenField 'hfNovoEmailUsuario'.
</Parameters>
-------------------------------------------------*/
function trataResultadoVerificacaoEmail(s, e) {
    var situacaoEmail = hfNovoEmailUsuario.Get("emailVerificadoUsuarioNovo");
    var confirmaAtivar;
    hfNovoEmailUsuario.Set("tipoConfirmacao", "");
   
    // encontrado email, porém de um usuário
    if (("EXOE" == situacaoEmail || "EXNE" == situacaoEmail))
        pcUsuarioExcluido.Show();
    else if ("ATOE" == situacaoEmail) //encontrado email de um usuário ATivo em outra entidade
        pcEntidadActual.Show();
    else if ("ATNE" == situacaoEmail) //encontrado email de um usuário ATivo na entidade em questão
        lancarMensagemSistema(traducao.CadastroUsuariosNovo_aten__o__j__existe_um_usu_rio_com_este_email_nesta_unidade__n_o___permitido_incluir_outro_usu_rio_com_este_email_);
    else if ("I" == situacaoEmail) // email não existente
        SimUsuarioNovo();
    else if ("ALTE" == situacaoEmail) // Alteração de Email
        SimUsuarioAlterar();
}

/*-------------------------------------------------
<summary>
setea o conteudo del label do popup 'pcMensagemVarios', apos 
lança o evento Show().
</summary>
<return></return>
<Parameters>
mensagem: texto a ser mostrado.
</Parameters>
-------------------------------------------------*/
function lancarMensagemSistema(mensagem) {
    lblMensagemVarios.SetText(mensagem);
    pcMensagemVarios.Show();
}

function SimUsuarioNovo() {
    txtEmail.SetEnabled(false);
    //btnVerificarEmail.SetEnabled(false);
    hlkVerificarEmail.SetEnabled(true);
    hfNovoEmailUsuario.Set("tipoConfirmacao", "nuevoUsuario");
    pnCallbackFormulario.PerformCallback("nuevoUsuario");
}

function SimUsuarioAlterar() {
    txtEmail.SetEnabled(false);
    //btnVerificarEmail.SetEnabled(false);
    hlkVerificarEmail.SetEnabled(false);
    hfNovoEmailUsuario.Set("tipoConfirmacao", "alterarUsuario");
    pnCallbackFormulario.PerformCallback("alterarUsuario");
}

function SimUsuarioExcluido_OnClick() {
    pcUsuarioExcluido.Hide();
    txtEmail.SetEnabled(false);
    //btnVerificarEmail.SetVisible(false);
    hlkVerificarEmail.SetVisible(false);
    hfNovoEmailUsuario.Set("tipoConfirmacao", "ativarUsuario");
    pnCallbackFormulario.PerformCallback("ativarUsuario");
}

function SimEntidadAtual_OnClick() {
    pcEntidadActual.Hide();
    txtEmail.SetEnabled(false);
    //btnVerificarEmail.SetVisible(false);
    hlkVerificarEmail.SetVisible(false);
    hfNovoEmailUsuario.Set("tipoConfirmacao", "adicionarUnidade");
    pnCallbackFormulario.PerformCallback("adicionarUnidade");
}

function showPcDado(s, e) {
    //    var celda = document.getElementById("tdRedefinirSenha");
    //    var autenticacao = cmbTipoAutentica.GetValue();
    //    var estado;
    //    
    //    if(autenticacao == "AS")
    //        btnRedefinirSenha.SetVisible(false); //celda.style.display = "none";
    //    else
    //        btnRedefinirSenha.SetVisible(true); //celda.style.display = "";
    //        
    var sWidth = Math.max(0, document.documentElement.clientWidth) - 300;
    var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
    s.SetWidth(sWidth);
    s.SetHeight(sHeight);
    s.UpdatePosition();
    if (window.hfGeral) {
        if ("Incluir" == hfGeral.Get("TipoOperacao"))
            btnRedefinirSenha.SetVisible(false); //celda.style.display = "none";    
    }
}

/*-------------------------------------------------
<summary>
CusttomButtons do GridView 'gvDados', ao fazer click, segundo cual, fazera uma ação.
</summary>
<return></return>
<Parameters>
s: Componente GridView.
e: Propiedade do GridView.
</Parameters>
-------------------------------------------------*/
function OnClick_CustomButtons(s, e) {
    gvDados.SetFocusedRowIndex(e.visibleIndex);
    btnSalvar1.SetVisible(true);
    e.processOnServer = false;
    
    if (e.buttonID == "btnEditar") {
        TipoOperacao = "Editar";
        hfGeral.Set("TipoOperacao", TipoOperacao);
        //OnGridFocusedRowChanged(gvDados);
        onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
    }
    else if (e.buttonID == "btnExcluir") {
        onClickBarraNavegacao("Excluir", gvDados, pcDados);
    }
    else if (e.buttonID == "btnDetalhesCustom") {
        TipoOperacao = "Consultar";
        hfGeral.Set("TipoOperacao", TipoOperacao);
        OnGridFocusedRowChanged(gvDados);
        //Ocultar botones.
        btnSalvar1.SetVisible(false);
        btnRedefinirSenha.SetVisible(false);

        pcDados.Show();
    }
    else if (e.buttonID == "btnEditarPerfis") {
        OnGridFocusedRowChangedPopup(gvDados);
    }
    else if (e.buttonID == 'btnCopiaPermissoes') {
        //	    e.processOnServer = false;
        //	    pcCopiaPermissoes.Show();
        e.processOnServer = true;
        trataClickBotaoCopiarPermissoes(s);
    }
    else if (e.buttonID == "btnConfig") {
        OnGridFocusedRowChangedConfig(gvDados);
    }
}

/*-------------------------------------------------
<summary>
Função que verifica o usuário logado e o usuário editado, se ambos são iguais, 
e se se tenta modificar a propriedade de 'usuário ativo', notificasse com um alerta, 
para queo usuário editor tenha consciência da modificação de tal propriedade.

Resulta que se o usuário se edita como não ativo, não terá acesso à entidade na que 
se está editando.
</summary>
<return></return>
<Parameters>
s: Componente aspxButton.
e: Propiedade do aspxButton.
</Parameters>
-------------------------------------------------*/
function onClick_SalvarUsuario(s, e) {
    
    var acao = hfGeral.Get("TipoOperacao");
    var idUsuarioLogado = hfGeral.Get('idUsuarioLogado');
    var usuarioAtual = hfGeral.Get('CodigoUsuarioAtual');  
    var confirmacaoEmailInvalido = hfConfirmacaoEmailUsuario.Get("confirmacaoEmailInvalido");

    if (!ckbAtivo.GetValue() && acao == 'Editar') {
        if (idUsuarioLogado == usuarioAtual) {
            if (confirm(traducao.CadastroUsuariosNovo_aten__o__voc__est__desativando_o_seu_pr_prio_usu_rio__se_continuar__n_o_poder__mais_acessar_esta_entidade_at__que_outro_usu_rio_o_ative_novamente__deseja_continuar_)) {
                if (window.onClick_btnSalvar1)
                    onClick_btnSalvar1(confirmacaoEmailInvalido);
            }
        }
        else {
            if (window.onClick_btnSalvar1) {

                onClick_btnSalvar1(confirmacaoEmailInvalido);
            }
        }
    }
    else {
        if (window.onClick_btnSalvar1)
            onClick_btnSalvar1(confirmacaoEmailInvalido);
    }
}

function onEnd_pnCallbackLocal(s, e) {
    
    TipoOperacao = hfGeral.Get("TipoOperacao");
    lpAguardeMasterPage.Hide();
    EsRecursoCorporativo = s.cp_EsRecursoCorporativo;
    if (s.cp_Mostrar == "S")
        pcEntidadActual.Show();
    else {
        pcEntidadActual.Hide();

        if ("Incluir" == s.cp_OperacaoOk) mostraDivSalvoPublicado(traducao.CadastroUsuariosNovo_usuario_inclu_do_com_sucesso_);
        else if ("Editar" == s.cp_OperacaoOk) mostraDivSalvoPublicado(traducao.CadastroUsuariosNovo_dados_gravados_com_sucesso_);
        else if ("Excluir" == s.cp_OperacaoOk) mostraDivSalvoPublicado(traducao.CadastroUsuariosNovo_usuario_exclu_do_com_sucesso_);
        else if ("IncluirEntidadeAtual" == s.cp_OperacaoOk) mostraDivSalvoPublicado(traducao.CadastroUsuariosNovo_usuario_inclu_do_com_sucesso_);

        if (hfGeral.Contains("StatusSalvar")) {
            var status = hfGeral.Get("StatusSalvar");
            if (status != "1") {
                var mensagem = hfGeral.Get("ErroSalvar");
                lblMensagemError.SetText(mensagem);
                pnlImpedimentos.SetVisible(true);


                if (s.cp_EsRecursoCorporativo == "OK") {
                    document.getElementById("corUN").style.display = "none";
                    document.getElementById("corPR").style.display = "none";
                    document.getElementById("corIN").style.display = "none";
                    document.getElementById("corIO").style.display = "none";
                    document.getElementById("corDE").style.display = "none";
                    document.getElementById("corTD").style.display = "none";

                    var arrayDeLegendas = s.cp_LegendasEnvolvidas.split('|');
                    var i = 0;
                    for (i = 0; i < arrayDeLegendas.length; i++)
                    {
                        document.getElementById("cor" + arrayDeLegendas[i]).style.display = "block";
                    }


                    if (TipoOperacao == 'Excluir') {
                        btnNaoTelaImpedimento.SetVisible(true);
                        btnSimTelaImpedimento.SetVisible(false);
                        btnNaoTelaImpedimento.SetText("Fechar");
                    }
                    else {
                        btnNaoTelaImpedimento.SetText("Não");
                        btnNaoTelaImpedimento.SetVisible(true);
                        btnSimTelaImpedimento.SetVisible(true);
                        btnSimTelaImpedimento.SetText("Sim");
                    }

                    gvImpedimentos.SetVisible(gvImpedimentos.pageRowCount > 0);
                    desabilitaHabilitaComponentes();
                    pcMensagemGravacao.Show();

                }
                else if (s.cp_EsSuperUsuario == "OK") {
                    document.getElementById("corUN").style.display = "none";
                    document.getElementById("corPR").style.display = "none";
                    document.getElementById("corIN").style.display = "none";
                    document.getElementById("corIO").style.display = "none";
                    document.getElementById("corDE").style.display = "none";
                    document.getElementById("corTD").style.display = "none";

                    var arrayDeLegendas1 = s.cp_LegendasEnvolvidas.split('|');
                    var i = 0;
                    for (i = 0; i < arrayDeLegendas1.length; i++) {
                        document.getElementById("cor" + arrayDeLegendas1[i]).style.display = "block";
                    }

                    btnNaoTelaImpedimento.SetVisible(true);
                    btnSimTelaImpedimento.SetVisible(false);
                    btnNaoTelaImpedimento.SetText("Fechar");

                    gvImpedimentos.SetVisible(gvImpedimentos.pageRowCount > 0);
                    desabilitaHabilitaComponentes();
                    pcMensagemGravacao.Show();

                }
                else if (s.cp_IndicaCpfInvalido == 'S')
                {
                    document.getElementById("corUN").style.display = "none";
                    document.getElementById("corPR").style.display = "none";
                    document.getElementById("corIN").style.display = "none";
                    document.getElementById("corIO").style.display = "none";
                    document.getElementById("corDE").style.display = "none";
                    document.getElementById("corTD").style.display = "none";
                    btnNaoTelaImpedimento.SetVisible(true);
                    btnSimTelaImpedimento.SetVisible(false);
                    btnNaoTelaImpedimento.SetText("Fechar");

                    gvImpedimentos.SetVisible(gvImpedimentos.pageRowCount > 0);
                    desabilitaHabilitaComponentes();
                    pcMensagemGravacao.Show();
                }
                else {
                    //-------
                    if (s.cp_corUN != "S") document.getElementById("corUN").style.display = "none";
                    if (s.cp_corPR != "S") document.getElementById("corPR").style.display = "none";
                    if (s.cp_corIN != "S") document.getElementById("corIN").style.display = "none";
                    //if(s.cp_corIO != "S") document.getElementById("corIO").style.display = "none";
                    if (s.cp_corDE != "S") document.getElementById("corDE").style.display = "none";
                    if (s.cp_corTD != "S") document.getElementById("corTD").style.display = "none";
                    //-------

                    
                    if (gvImpedimentos.pageRowCount <= 0 && s.cp_HabilitaSimNao == "S") {
                        btnNaoTelaImpedimento.SetVisible(true);
                        btnNaoTelaImpedimento.SetText("Não");
                        btnSimTelaImpedimento.SetVisible(true);
                        btnSimTelaImpedimento.SetText("Sim");

                        gvImpedimentos.SetVisible(gvImpedimentos.pageRowCount > 0);
                        desabilitaHabilitaComponentes();
                        pcMensagemGravacao.Show();
                    }
                    else {
                        gvImpedimentos.SetVisible(true);
                        btnNaoTelaImpedimento.SetVisible(true);
                        btnSimTelaImpedimento.SetVisible(false);

                        btnNaoTelaImpedimento.SetText("Fechar");

                        gvImpedimentos.SetVisible(gvImpedimentos.pageRowCount > 0);
                        statusComponentesAposVerificacaoEmail();
                        pcMensagemGravacao.Show();
                    }

                }
               
            }
            else {
                if (window.onEnd_pnCallback)
                    onEnd_pnCallback();
            }
        }
    }

    e.processOnServer = false;
}

function mostraDivSalvoPublicado(acao) {
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null, 3000);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar()
    // oculta também a tela de perfis, caso esteja visível.
    //pcPerfis.Hide();
}

function onEnd_CallbackGvImpedimentos(s, e) {

}

function onEnd_pnCallbackFormulario(s, e) {
    if (s.cp_SituacaoEmail == "adicionarUnidade")
        txtLogin.SetEnabled(false);
}


/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function OnGridFocusedRowChangedPopup(grid) {
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoUsuario;NomeUsuario;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor) {
    var idUsuario = (valor[0] != null ? valor[0] : "-1");
    var nomeUsuario = (valor[1] != null ? valor[1] : "");

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = screen.width - 100;
    var window_height = Math.max(0, document.documentElement.clientHeight) - 130;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;
    window.top.showModal("ListagemPerfisUsuario.aspx?CU=" + idUsuario + "&AlturaGrid=" + (window_height - 280), traducao.CadastroUsuariosNovo_perfis, window_width, window_height, '', null);
}


//--------------------------------------bloco de scripts da copia de permissoes
function trataClickBotaoCopiarPermissoes(s) {
    s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoUsuario;NomeUsuario', auxTrataClickBotaoCopiarPermissoes)

}

function auxTrataClickBotaoCopiarPermissoes(valores) {
    if (null != valores) {
        var codigoUsuario = valores[0];
        var nomeUsuario = valores[1];
        var continuar = true;
        hfStatusCopiaPermissoes.Set("CodigoUsuarioDestino", codigoUsuario);
        hfStatusCopiaPermissoes.Set("NomeUsuarioDestino", nomeUsuario);
        ddlUsuarioOrigem.PerformCallback();
        pcCopiaPermissoes.Show();
    }
}



function validaCamposCopiaPermissao() {
    //debugger
    var retorno = true;
    var mensagemAlert = "";

    if (ddlUsuarioOrigem.GetValue() == null || ddlUsuarioOrigem.GetValue() == -1) {
        mensagemAlert += traducao.CadastroUsuariosNovo_informe_o_usu_rio_que_vai_fornecer_o_conjunto_de_permiss_es_para_a_c_pia + "\n";
        retorno = false;
    }
    else {
        hfStatusCopiaPermissoes.Set("CodigoUsuarioOrigem", ddlUsuarioOrigem.GetValue());
        hfStatusCopiaPermissoes.Set("NomeUsuarioOrigem", ddlUsuarioOrigem.GetText());
    }

    if (retorno == false) {
        window.top.mostraMensagem(mensagemAlert, 'atencao', true, false, null);
    }
    return retorno;
}



function onClick_btnSalvarCopiaPermissoes() {
    if (validaCamposCopiaPermissao()) {
        if (confirm(traducao.CadastroUsuariosNovo_usu_rio_origem__ + hfStatusCopiaPermissoes.Get("NomeUsuarioOrigem") + '\n' + traducao.CadastroUsuariosNovo_usu_rio_destino__ + hfStatusCopiaPermissoes.Get("NomeUsuarioDestino") + '\n\n' + traducao.CadastroUsuariosNovo_confirma_a_c_pia_do_conjunto_de_permiss_es_ + '\n\n' + traducao.CadastroUsuariosNovo_esta_opera__o_n_o_pode_ser_desfeita_)) {
            hfStatusCopiaPermissoes.PerformCallback("Editar");
        }
    }

    return false;
}


function onClick_btnCancelarCopiaPermissoes() {
    pcCopiaPermissoes.Hide();
    return true;
}

function hfStatusCopiaPermissoes_onEndCallback() {
    var msgErro = hfStatusCopiaPermissoes.Get("ErroSalvar");
    if (hfStatusCopiaPermissoes.Get("StatusSalvar") == "1")
    {
        if (trim(msgErro) != "") {
            window.top.mostraMensagem(msgErro, 'sucesso', false, false, null, 3000);
            pcCopiaPermissoes.Hide();
        }
        //gvDados.PerformCallback("");
    }
    else if (hfStatusCopiaPermissoes.Get("StatusSalvar") == "0")
    {
        if (trim(msgErro) != "") {
            window.top.mostraMensagem(msgErro, 'erro', true, false, null);
        }
    }
}

function pcCopiaPermissoes_OnPopup(s, e) {
    // limpa o hidden field com a lista de status
    //hfStatusCopiaFluxo.Clear(); 
    lblUsuarioAlvo.SetText(traducao.CadastroUsuariosNovo_usu_rio_que_vai_receber_o_conjuto_de_permiss_es__ + hfStatusCopiaPermissoes.Get("NomeUsuarioDestino") + ".");
    lblCopia.SetText(traducao.CadastroUsuariosNovo_importante__o_usu_rio_acima_inicialmente_perder__todas_as_permiss_es_que_tem_exceto_as_que_ele___respons_vel__em_seguida_as_permiss_es_do_usu_rio_origem_selecionado_abaixo_exceto_as_que_ele___respons_vel_ser_o_atribu_das_a_ele_);
    ddlUsuarioOrigem.SetSelectedIndex(-1);

}

//-------
function mostraPopupMensagemGravacaoCopiaPermissao(acao) {
    window.top.mostraMensagem(acao, 'sucesso', false, false, null, 3000);
    setTimeout('fechaTelaEdicaoCopiaPermissao();', 4500);
}

function fechaTelaEdicaoCopiaPermissao() {
    onClick_btnCancelarCopiaPermissoes();
}

//fim do bloco da copia

function OnGridFocusedRowChangedConfig(grid) {
    
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoUsuario;NomeUsuario;', getDadosPopupConfig); //getKeyGvDados);
}

function getDadosPopupConfig(valor) {
    var idUsuario = (valor[0] != null ? valor[0] : "-1");
    var nomeUsuario = (valor[1] != null ? valor[1] : "");

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = screen.width - 100;
    var window_height = screen.height - 300;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;
    window.top.showModal("adm_ConfiguracaoPessoais_Popup.aspx?US=" + idUsuario, traducao.CadastroUsuariosNovo_prefer_ncias, window_width, window_height, '', null);
}

function valida_cpf(cpf) {
    var numeros, digitos, soma, i, resultado, digitos_iguais;
    digitos_iguais = 1;
    if (cpf.length < 11)
        return false;
    for (i = 0; i < cpf.length - 1; i++)
        if (cpf.charAt(i) != cpf.charAt(i + 1)) {
            digitos_iguais = 0;
            break;
        }
    if (!digitos_iguais) {
        numeros = cpf.substring(0, 9);
        digitos = cpf.substring(9);
        soma = 0;
        for (i = 10; i > 1; i--)
            soma += numeros.charAt(10 - i) * i;
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;
        numeros = cpf.substring(0, 10);
        soma = 0;
        for (i = 11; i > 1; i--)
            soma += numeros.charAt(11 - i) * i;
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;
        return true;
    }
    else
        return false;
}

function configurarMascaraDeTelefoneAoSairDoCampo(s, e) {
    var valor = s.GetText();
    valor = valor.replace(/\D/g, "");

    if (/^(\d{11})/.test(valor) == true) {
        valor = valor.replace(/^(\d{2})(\d{5})(\d{4})/, "($1) $2-$3");
    }
    else if (/^(\d{10})/.test(valor) == true) {
        valor = valor.replace(/^(\d{2})(\d{4})(\d{4})/, "($1) $2-$3");
    }
    else if (/^(\d{9})/.test(valor) == true) {
        valor = valor.replace(/^(\d{5})(\d{4})/, "$1-$2");
    }
    else if (/^(\d{8})/.test(valor) == true) {
        valor = valor.replace(/^(\d{4})(\d{4})/, "$1-$2");
    }
    else if (/^(\d{7})/.test(valor) == true) {
        valor = valor.replace(/^(\d{4})(\d{3})/, "$1-$2");
    }
    else if (/^(\d{6})/.test(valor) == true) {
        valor = valor.replace(/^(\d{4})(\d{2})/, "$1-$2");
    }
    else if (/^(\d{5})/.test(valor) == true) {
        valor = valor.replace(/^(\d{4})(\d)/, "$1-$2");
    }
    s.SetText(valor);
}

function impedirQueLetrasSejamDigitadasNoCampo(s, e) {
    //A faixa de valores de 48 até 57 correspondem aos números de 0 a 9 no codigo UNICODE. 
    var key = e.htmlEvent.keyCode;
    if (key < 48 || key > 57) {
        e.htmlEvent.returnValue = false;
    }
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 95;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}


function onClick_btnSalvar1(confirmacaoEmailInvalido=false) {
    if (window.validaCamposFormulario) {
        if (validaCamposFormulario(confirmacaoEmailInvalido)) {    
            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'atencao', true, false, null);
            return false;
        }
    }

    if (window.SalvarCamposFormulario) {
        if (window.top.lpAguardeMasterPage.GetVisible() == false) {
            window.top.lpAguardeMasterPage.Show();
            setTimeout('SalvarCamposFormulario();habilitaModoEdicaoBarraNavegacao(false, gvDados);', 1000);
        }
    }
    else {
        window.top.mostraMensagem(traducao.barraNavegacao_o_m_todo_n_o_foi_implementado_, 'atencao', true, false, null);
    }
}