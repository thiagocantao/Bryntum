// JScript File
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

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    hfGeral.Set("hfCodigoUnidadeNegocio", "");
    hfGeral.Set("lovCodigoResponsavel", "");
    ddlTipoRecurso.SetValue("");
    chkAtivo.SetChecked(true);
    ddlResponsavel.SetText("");
    ddlGrupo.SetText("");
    ddlUnidadeNegocio.SetValue("");
    txtValorHora.SetText("");
    txtValorUso.SetText("");
    txtUnidadeMedida.SetText("");
    ddlDisponibilidadeInicio.SetValue(null);
    ddlDisponibilidadeTermino.SetValue(null);
    memoAnotacoes.SetText("");
    memoAnotacoes.Validate();
    txtResp.SetText("");
    desabilitaHabilitaComponentes();
    callbackLimpaSessao.PerformCallback();
    chkGenerico.SetChecked(false);
    chkEquipe.SetChecked(false);

}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetSelectedFieldValues('CodigoRecursoCorporativo;CodigoTipoRecurso;IndicaRecursoAtivo;CodigoUsuario;CodigoGrupoRecurso;CodigoUnidadeNegocio;CustoHora;CustoUso;InicioDisponibilidadeRecurso;TerminoDisponibilidadeRecurso;Anotacoes;NomeUsuario;NomeRecursoCorporativo;CodigoEntidade;UnidadeMedidaRecurso;Generico;Equipe;NomeUnidadeNegocio', MontaCamposFormulario);
}

function trataMensagemErro(TipoOperacao1, mensagemErro1)
{
    return mensagemErro1;
}
function MontaCamposFormulario(valores)
{
    var OperacaoSel = (window.TipoOperacao) ? TipoOperacao : "Consultar";
/*
  0- CodigoRecursoCorporativo;  6- CustoHora;                       12- NomeRecursoCorporativo;
  1- CodigoTipoRecurso;         7- CustoUso;                        13- CodigoEntidade
  2- IndicaRecursoAtivo;        8- InicioDisponibilidadeRecurso;    14- UnidadeMedidaRecurso
  3- CodigoUsuario;             9- TerminoDisponibilidadeRecurso;   15- Generico
  4- CodigoGrupoRecurso;        10-Anotacoes                        16- Equipe 
  5- CodigoUnidadeNegocio;      11-NomeUsuario
*/

    desabilitaHabilitaComponentes();

    var values = valores[0];

    if("Incluir" != OperacaoSel)
    {
        var CodigoRecurso = (values[0] == null) ? "" : values[0].toString();
        hfGeral.Set("hfCodigoUnidadeNegocio",CodigoRecurso);



        (values[1] == null ? ddlTipoRecurso.SetText("")  : ddlTipoRecurso.SetValue(values[1]));
        if("Financeiro" == ddlTipoRecurso.GetText())
        {
            txtValorHora.SetEnabled(false);
            txtValorUso.SetEnabled(false);
            ddlDisponibilidadeInicio.SetEnabled(false);
            ddlDisponibilidadeTermino.SetEnabled(false);
            ddlResponsavel.SetVisible(false);
            txtResp.SetVisible(true);
            ddlResponsavel.SetEnabled(false);
            txtResp.SetEnabled("Consultar" == OperacaoSel ? false : true);
            txtUnidadeMedida.SetEnabled(false);
            lpLoading.Hide();
        }
        if("Equipamento" == ddlTipoRecurso.GetText())
        {
            ddlResponsavel.SetVisible(false);
            txtResp.SetVisible(true);
            ddlResponsavel.SetEnabled(false);
            txtResp.SetEnabled("Consultar" == OperacaoSel ? false : true);
            lpLoading.Hide();
        }
        /*if ("Editar" == OperacaoSel)
	    {
            //txtResp.SetEnabled(false);*/
        if ("Pessoa" == ddlTipoRecurso.GetText()) {
            ddlResponsavel.SetEnabled(false);
            txtUnidadeMedida.SetEnabled(false);
            txtResp.SetEnabled("Consultar" == OperacaoSel ? false : true);
            lpLoading.Hide();
        } 
        
        var IndicaRecursoAtivo = (values[2] == null) ? "" : values[2].toString();
        if(IndicaRecursoAtivo == "S")
            chkAtivo.SetChecked(true);
        else
            chkAtivo.SetChecked(false);

        var IndicaRecursoGenerico = (values[15] == null) ? "" : values[15].toString();
        if (IndicaRecursoGenerico == "S"){
            chkGenerico.SetChecked(true);
            ddlResponsavel.SetVisible(false);
            txtResp.SetVisible(true);
         }
            
        else
            chkGenerico.SetChecked(false);


        var IndicaEquipe = (values[16] == null) ? "" : values[16].toString();
        if (IndicaEquipe == "S")
            chkEquipe.SetChecked(true);
        else
            chkEquipe.SetChecked(false);

            
        var CodigoUsuario = (values[3] == null) ? "" : values[3].toString();

        ddlResponsavel.SetText((values[12] != null ? values[12]  : ""));
        txtResp.SetText((values[12] != null ? values[12]  : ""));
        (values[4] == null ? ddlGrupo.SetText("")  : ddlGrupo.SetValue(values[4]));
        (values[4] == null ? hfCodigoGrupo.Set("hfCodigoGrupo", "") : hfCodigoGrupo.Set("hfCodigoGrupo", values[4]));

        if (values[17] != null) {
            ddlUnidadeNegocio.SetText(values[17].toString());
        }
        ddlUnidadeNegocio.SetValue(values[5]);

        txtValorHora.SetText((values[6] != null ? values[6].toString().replace(".", ",") : ""));
        txtValorUso.SetText((values[7] != null ? values[7].toString().replace(".", ",") : ""));
        txtUnidadeMedida.SetText((values[14] != null ? values[14].toString() : ""));
        (values[8] == null ? ddlDisponibilidadeInicio.SetText("")  : ddlDisponibilidadeInicio.SetValue(values[8]));
        (values[9] == null ? ddlDisponibilidadeTermino.SetText("")  : ddlDisponibilidadeTermino.SetValue(values[9]));
        memoAnotacoes.SetText((values[10] != null ? values[10]  : ""));
        memoAnotacoes.Validate();
        hfGeral.Set("lovCodigoResponsavel", CodigoUsuario);
                
        pn_ddlGrupo.PerformCallback(values[1] == null ? "-1|" + OperacaoSel : values[1] + "|" + OperacaoSel);
    }
}

/*-------------------------------------------------------------------------------------------
    Función: desabilitaHabilitaComponentes()
    retorno: void.
    Descripción: Mudara la propiedad Enabled de los componentes que hacen referencia a los
                 datos del Usuario. Dependera del estado de la tela, si esta Incluyendo,
                 Editando o Default (Consultando).
-------------------------------------------------------------------------------------------*/
function desabilitaHabilitaComponentes()
{
    var OperacaoSel = hfGeral.Contains("TipoOperacao") ? hfGeral.Get("TipoOperacao").toString():"";
	
	if ("Incluir" == OperacaoSel || "Editar" == OperacaoSel)
	{
	    ddlTipoRecurso.SetEnabled(true);
        chkAtivo.SetEnabled(true);
        ddlResponsavel.SetEnabled(true);
        ddlGrupo.SetEnabled(true);
        ddlUnidadeNegocio.SetEnabled(true);
        txtValorHora.SetEnabled(true);
        txtValorUso.SetEnabled(true);
        ddlDisponibilidadeInicio.SetEnabled(true);
        ddlDisponibilidadeTermino.SetEnabled(true);
        memoAnotacoes.SetEnabled(true);
        txtResp.SetEnabled(true);
        txtUnidadeMedida.SetEnabled(true);
        chkGenerico.SetEnabled(false);
        chkEquipe.SetEnabled(false);
	}
	if ("Editar" == OperacaoSel)
	{
	    ddlTipoRecurso.SetEnabled(false);
	}
	else if ("Consultar" == OperacaoSel)
	{
	    ddlTipoRecurso.SetEnabled(false);
        chkAtivo.SetEnabled(false);
        ddlResponsavel.SetEnabled(false);
        ddlGrupo.SetEnabled(false);
        ddlUnidadeNegocio.SetEnabled(false);
        txtValorHora.SetEnabled(false);
        txtValorUso.SetEnabled(false);
        ddlDisponibilidadeInicio.SetEnabled(false);
        ddlDisponibilidadeTermino.SetEnabled(false);
        memoAnotacoes.SetEnabled(false);
        txtResp.SetEnabled(false);
        txtUnidadeMedida.SetEnabled(false);
        chkGenerico.SetEnabled(false);
        chkEquipe.SetEnabled(false);
    }
//    if ("Incluir" == OperacaoSel) {
//        trataSelecaoGenerico();
//        trataSelecaoEquipe();
//    }
}

function trataSelecaoGenerico() {
    if (chkGenerico.GetChecked()) {
        // desabilita combo de usuario e habilita txt quando for pessoa 
        if ("Pessoa" == ddlTipoRecurso.GetText()) {
            ddlResponsavel.SetEnabled(false);
            ddlResponsavel.SetVisible(false);
            ddlResponsavel.SetText("");

            txtResp.SetVisible(true);
            txtResp.SetEnabled(true);
            txtResp.SetText("");
        }
        // desabilita o check equipe
        chkEquipe.SetEnabled(false);
        chkEquipe.SetChecked(false);

    } else {
        if ("Pessoa" == ddlTipoRecurso.GetText()) {
            ddlResponsavel.SetEnabled(true);
            ddlResponsavel.SetVisible(true);
            ddlResponsavel.SetText("");

            txtResp.SetVisible(false);
            txtResp.SetEnabled(true);
            txtResp.SetText("");
            chkEquipe.SetEnabled(true);
            chkEquipe.SetChecked(false);
        }
    }


}

function trataSelecaoEquipe() {
    if (chkEquipe.GetChecked()) {
        // desabilita combo de usuario e habilita txt quando for pessoa 
        if ("Pessoa" == ddlTipoRecurso.GetText()) {
            ddlResponsavel.SetEnabled(false);
            ddlResponsavel.SetVisible(false);
            ddlResponsavel.SetText("");

            txtResp.SetVisible(true);
            txtResp.SetEnabled(true);
            txtResp.SetText("");
        }
        // desabilita o check generico
        chkGenerico.SetEnabled(false);
        chkGenerico.SetChecked(false);

    } else {
        if ("Pessoa" == ddlTipoRecurso.GetText()) {
            ddlResponsavel.SetEnabled(true);
            ddlResponsavel.SetVisible(true);
            ddlResponsavel.SetText("");

            txtResp.SetVisible(false);
            txtResp.SetEnabled(false);
            txtResp.SetText("");
            chkGenerico.SetEnabled(true);
            chkGenerico.SetChecked(false);
        }
    }
}

function OnGridFocusedRowChangedPopup(grid) {
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoRecursoCorporativo;NomeRecursoCorporativo;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor) {
    var idObjeto = (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var tituloMapa = "";

    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = screen.width - 100;
    var window_height = screen.height - 200;
    var newfeatures = 'scrollbars=no,resizable=no';
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;
    window.top.showModal("../_Estrategias/InteressadosObjeto.aspx?ITO=EQ&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa + '&AlturaGrid=' + (window_height - 110), traducao.RecursosCorporativos_membros_da_equipe, null, null, '', null);
}


function onEndLocal_pnCallback()
{

        if(window.onEnd_pnCallback)
        {
            if(pnCallback.cp_Pesquisa == "0")//se o callback não foi terminado por causa de uma pesquisa feita
            {
                onEnd_pnCallback();
            }  
               
        }  
}

function verificarDadosPreenchidos()
{
    var mensagemError = "";
    var retorno = true;
    var numError = 0;

    var dataAtual = new Date();
    var quatroAnosAtras = dataAtual.getFullYear() - 4;
    var dataInicioReal = new Date(ddlDisponibilidadeInicio.GetValue());
    var dataInicioRealP = (dataInicioReal.getMonth() + 1) + "/" + dataInicioReal.getDate() + "/" + dataInicioReal.getFullYear();
    var dataInicioRealC = Date.parse(dataInicioRealP);
    
    var dataTerminoReal = new Date(ddlDisponibilidadeTermino.GetValue());
    var dataTerminoRealP = (dataTerminoReal.getMonth() + 1) + "/" + dataTerminoReal.getDate() + "/" + dataTerminoReal.getFullYear();
    var dataTerminoRealC = Date.parse(dataTerminoRealP);

    if ((ddlDisponibilidadeInicio.GetValue() != null) && (ddlDisponibilidadeTermino.GetValue() == null)) {
        if (dataInicioReal.getFullYear() < 2010) {
            mensagemError += ++numError + ") " + traducao.RecursosCorporativos_o_in_cio_da_disponibilidade_n_o_pode_ser_anterior_a_ + quatroAnosAtras + "!\n";
            retorno = false;
        }
    }

    if ((ddlDisponibilidadeInicio.GetValue() == null) && (ddlDisponibilidadeTermino.GetValue() != null)) {
       if (dataTerminoReal.getFullYear() < 2010) {
           mensagemError += ++numError + ") " + traducao.RecursosCorporativos_o_t_rmino_da_disponibilidade_n_o_pode_ser_anterior_a_ + quatroAnosAtras + "!\n";
            retorno = false;
        }
    }

    if ((ddlDisponibilidadeInicio.GetValue() != null) && (ddlDisponibilidadeTermino.GetValue() != null)) {
        if (dataInicioRealC > dataTerminoRealC) {
            mensagemError += ++numError + ") " + traducao.RecursosCorporativos_a_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n";
            retorno = false;
        }
        if (dataInicioReal.getFullYear() < 2010) {
            mensagemError += ++numError + ") " + traducao.RecursosCorporativos_o_in_cio_da_disponibilidade_n_o_pode_ser_anterior_a_ + quatroAnosAtras + "!\n";
            retorno = false;
        }
        if (dataTerminoReal.getFullYear() < 2010) {
            mensagemError += ++numError + ") " + traducao.RecursosCorporativos_o_t_rmino_da_disponibilidade_n_o_pode_ser_anterior_a_ + quatroAnosAtras + "!\n";
            retorno = false;
        }
    }

    if(ddlTipoRecurso.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.RecursosCorporativos_tipo_de_recurso_deve_ser_informado_ + "\n";
            retorno = false;
    }
    if (ddlTipoRecurso.GetText() == "Pessoa") {
        if (ddlUnidadeNegocio.GetValue() == null) {
            mensagemError += ++numError + ") " + traducao.RecursosCorporativos_unidade_neg_cio_deve_ser_informada_ + "\n";
            retorno = false;
        }
        if (ddlResponsavel.GetValue() == -1 && (!chkGenerico.GetChecked() && !chkEquipe.GetChecked())) {
            mensagemError += ++numError + ") " + traducao.RecursosCorporativos_pessoa_deve_ser_informada_ + "\n";
            retorno = false;
        }
        if (txtResp.GetText() == "" && (chkGenerico.GetChecked() || chkEquipe.GetChecked())) {
            mensagemError += ++numError + ") " + traducao.RecursosCorporativos_pessoa_deve_ser_informada_ + "\n";
            retorno = false;
        }
    }
   
    if(ddlTipoRecurso.GetText() == "Equipamento" && txtResp.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.RecursosCorporativos_equipamento_deve_ser_informado_ + "\n";
            retorno = false;
    }
    if(ddlTipoRecurso.GetText() == "Financeiro" && txtResp.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.RecursosCorporativos_recurso_financeiro_deve_ser_informado_ + "\n";
            retorno = false;
    }
    if(ddlGrupo.GetText() == "")
    {
        mensagemError += ++numError + ") " + traducao.RecursosCorporativos_grupo_deve_ser_informado_ + "\n";
            retorno = false;
    }

//    if(txtValorHora.GetText() == "")
//    {
//            mensagemError += ++numError + ") O Valor/hora deve ser informado!\n";
//            retorno = false;
//    }

//    if(txtValorUso.GetText() == "")
//    {
//            mensagemError += ++numError + ") O Valor/uso deve ser informado!\n";
//            retorno = false;
//    }
    
	if (!retorno)
	    window.top.mostraMensagem(mensagemError, 'atencao', true, false, null);

	return retorno;
}

function posSalvarComSucesso()
{
    if("Incluir" == TipoOperacao)
        window.top.mostraMensagem(traducao.RecursosCorporativos_recurso_inclu_do_com_sucesso_, 'sucesso', false, false, null);
    else if("Editar" == TipoOperacao)
    {
        window.top.mostraMensagem(traducao.RecursosCorporativos_dados_gravados_com_sucesso_, 'sucesso', false, false, null);
    }
    else if("Excluir" == TipoOperacao)
        window.top.mostraMensagem(traducao.RecursosCorporativos_recurso_exclu_do_com_sucesso_, 'sucesso', false, false, null);

    fechaEdicao();
}

/*-------------------------------------------------------------------------------------------
    Función: mostraDivSalvoPublicado(acao) - fechaTelaEdicao()
    retorno: void.
    Descripción: Muestra DIV con mensaje de resultado da operação con o banco de dados.
-------------------------------------------------------------------------------------------*/
function fechaEdicao()
{
    hfGeral.Set("RowFocusChanged","1");
    onClick_btnCancelar();
}

function abreNovoReg()
{
    btnSalvar1.SetVisible(true);
    TipoOperacao = 'Incluir';
    onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
}

/*-------------------------------------------------------------------------------------------
    Función: abreEdicao(codigoCalendario)
    retorno: void.
    Descripción: Muestra o formulario para editar o calendario do recurso coorporativo.
-------------------------------------------------------------------------------------------*/
function abreEdicao(parametros)
{
    var sWidth = Math.max(0, document.documentElement.clientWidth) - 300;

    window.top.showModalComFooter('frm_Calendarios.aspx?RC=S&' + parametros, traducao.RecursosCorporativos_calend_rio_do_recurso, sWidth, null, '', null);
}

function onBtnCalendario_Click(grid, e)
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoCalendario;NomeRecursoCorporativo;CodigoRecursoCorporativo', preparaEdicao);
}

function preparaEdicao(valores)
{
    var parametroCal = "";
    if( null != valores)
    {
        parametroCal = "CR=" + valores[2] + "&CC=" + valores[0] + "&Editar=N";
        abreEdicao(parametroCal);
    }
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 100;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
