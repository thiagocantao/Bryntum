// JScript File


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

function validaCamposFormulario()
{   // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var num = 0;
    if (ddlMapaEstrategico.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n- " + num + traducao.ObjetivosEstrategicosMapa__o_mapa_deve_ser_informado;
    }


    if (ddlTema.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n- " + num + traducao.ObjetivosEstrategicosMapa__a_descri__o_do_tema_deve_ser_informado;
        
    }

    if (txtTituloObjetivo.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n- " + num + traducao.ObjetivosEstrategicosMapa__o_t_tulo_do_objetivo_estrat_gico_deve_ser_informado;
    }

    if (txtDescricaoObjetivo.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n- " + num + traducao.ObjetivosEstrategicosMapa__a_descri__o_do_objetivo_estrat_gico_deve_ser_informado;
    }

    if (ddlInicioValidade.GetValue() != null && ddlTerminoValidade.GetValue() != null)
    {
        if(ddlInicioValidade.GetValue() > ddlTerminoValidade.GetValue())
        {
            num = num + 1;
            mensagemErro_ValidaCamposFormulario += "\n- " + num + traducao.ObjetivosEstrategicosMapa__a_data_de_in_cio_da_validade_n_o_pode_ser_maior_que_a_data_de_t_rmino;
        }
    }

    if (ddlResponsavel.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n- " + num + traducao.ObjetivosEstrategicosMapa__o_respons_vel_deve_ser_informado;
    }

    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{    
    ddlMapaEstrategico.SetEnabled(TipoOperacao != "Consultar");
    ddlTema.SetEnabled(TipoOperacao != "Consultar");//habilitado em edição, inclusão
    ddlMapaEstrategico.SetEnabled(TipoOperacao != "Editar");
    //ddlTema.SetEnabled(      TipoOperacao != "Editar");//habilitado em consulta, inclusão
    txtTituloObjetivo.SetEnabled(TipoOperacao != "Consultar");
    txtDescricaoObjetivo.SetEnabled(TipoOperacao != "Consultar");
    txtClassificacaoObjetivo.SetEnabled(TipoOperacao != "Consultar");
    ddlInicioValidade.SetEnabled(TipoOperacao != "Consultar");
    ddlTerminoValidade.SetEnabled(TipoOperacao != "Consultar");

    ddlResponsavel.SetEnabled(TipoOperacao != "Consultar");
    //ddlResponsavel.SetEnabled(      TipoOperacao != "Editar");
    
    if(window.heGlossario)
        heGlossario.SetEnabled(TipoOperacao != "Consultar");
}

function LimpaCamposFormulario()
{
    ddlTema.SetValue(null);
    txtTituloObjetivo.SetText(null);
    txtDescricaoObjetivo.SetText(null);
    hfGeral.Set('idTema', '-1');
    ddlResponsavel.SetText("");
    txtClassificacaoObjetivo.SetText("");
    ddlInicioValidade.SetValue(null);
    ddlTerminoValidade.SetValue(null);
    if(window.heGlossario)
        heGlossario.SetHtml("");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoObjetoEstrategia;CodigoMapaEstrategico;CodigoVersaoMapaEstrategico;DescricaoObjetoEstrategia;GlossarioObjeto;CodigoResponsavelObjeto;CodigoObjetoEstrategiaSuperior;TituloMapaEstrategico;ResponsavelObjeto;TituloObjetoEstrategia;ClassificacaoObjetivo;InicioValidade;TerminoValidade', MontaCamposFormulario);
        //gridDadosIndicador.PerformCallback();  
    }
}

function MontaCamposFormulario(values)
{
    /* Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    
    CodigoObjetivoEstrategico       0;
    CodigoMapaEstrategico           1;
    CodigoVersaoMapaEstrategico     2;
    DescricaoObjetoEstrategia       3;
    GlossarioObjeto                 4;
    CodigoResponsavelObjeto         5;
    CodigoObjetoEstrategiaSuperior  6;
    TituloMapaEstrategico           7;
    ResponsavelObjeto               8
    */
    var separadorChave = ';';
    LimpaCamposFormulario();

    if(values)
    {
        var CodigoObjetivo          = values[0];
        var CodigoMapa              = values[1];
        var CodigoVersao            = values[2];
        var DescricaoObjetoEstrategia = values[3];//DescricaoObjetoEstrategia
        var GlossarioObjeto         = values[4];
        var CodigoResponsavel       = values[5];
        var CodigoObjetoSuperior    = values[6];
        var TituloMapaEstrategico = values[7];
        var nomeResp = values[8];
        var TituloObjetoEstrategia = values[9]; //tituloObjetoEstrategia
        var ClassificacaoObjetivo = values[10];
        var InicioValidade = values[11];
        var TerminoValidade = values[12];

        hfGeral.Set("chave", CodigoObjetivo + separadorChave + CodigoMapa + separadorChave + CodigoVersao);
        
        ddlMapaEstrategico.SetText(TituloMapaEstrategico    != null ? TituloMapaEstrategico : "");
        ddlTema.SetValue(CodigoObjetoSuperior               != null ? CodigoObjetoSuperior   : "");
        ddlResponsavel.SetValue(    CodigoResponsavel       != null ? CodigoResponsavel     : "");
        (CodigoResponsavel == null ? ddlResponsavel.SetText("") : ddlResponsavel.SetText(nomeResp));
        txtTituloObjetivo.SetText(TituloObjetoEstrategia != null ? TituloObjetoEstrategia : "");
        txtDescricaoObjetivo.SetText(DescricaoObjetoEstrategia != null ? DescricaoObjetoEstrategia : "");
        txtClassificacaoObjetivo.SetText(ClassificacaoObjetivo != null ? ClassificacaoObjetivo : "");
        ddlInicioValidade.SetValue(InicioValidade);
        ddlTerminoValidade.SetValue(TerminoValidade);

        if(window.heGlossario)
            heGlossario.SetHtml(GlossarioObjeto != null ?  GlossarioObjeto : "");
        hfGeral.Set('idTema', CodigoObjetoSuperior);
        var parametro = CodigoMapa + separadorChave + CodigoObjetoSuperior;
        //ddlTema.PerformCallback(parametro);
        callbackTema.PerformCallback(parametro);
    }
}

//------------------------------------------------------------ DIV salvar
function mostraDivSalvoPublicado(acao)
{
    if (acao.toString().indexOf('suc') > -1)
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}


/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function onGridFocusedRowChangedPopup(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoObjetoEstrategia;DescricaoObjetoEstrategia;TituloMapaEstrategico;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor)
{
    var idObjeto     = (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var tituloMapa   = (valor[2] != null ? valor[2] : "");
   
    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width    = 900;
    var window_height   = 590;
    var newfeatures     = 'scrollbars=no,resizable=no';
    var window_top      = (screen.height-window_height)/2;
    var window_left     = (screen.width-window_width)/2;
    window.top.showModal("../../InteressadosObjeto.aspx?ITO=OB&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa + '&AlturaGrid=' + (window_height - 110), traducao.ObjetivosEstrategicosMapa_objetivos_estrat_gicos, window_width, window_height, '', null); 
	
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 90;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}