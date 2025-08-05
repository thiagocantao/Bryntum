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
        mensagemErro_ValidaCamposFormulario += "\n" + num + " - " + traducao.TemasMapa_o_mapa_deve_ser_informado;
    }
    if (ddlPerspectiva.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n" + num + " - " + traducao.TemasMapa_a_perspectiva_deve_ser_informada;
    }

    if (txtTituloTema.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n" + num + " - " + traducao.TemasMapa_o_t_tulo_do_tema_deve_ser_informada;
    }
    if (txtDescricaoTema.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n" + num + " - " + traducao.TemasMapa_a_descri__o_do_tema_deve_ser_informada;
    }
    

        

    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{    
    ddlMapaEstrategico.SetEnabled(  TipoOperacao != "Consultar");
    ddlPerspectiva.SetEnabled(      TipoOperacao != "Consultar");
    ddlMapaEstrategico.SetEnabled(  TipoOperacao != "Editar");
    ddlPerspectiva.SetEnabled(      TipoOperacao != "Editar");
    txtTituloTema.SetEnabled(             TipoOperacao != "Consultar");
    txtDescricaoTema.SetEnabled(TipoOperacao != "Consultar");
    ddlResponsavel.SetEnabled(      TipoOperacao != "Consultar");
    //ddlResponsavel.SetEnabled(      TipoOperacao != "Editar");
    
    if(window.heGlossario)
        heGlossario.SetEnabled(TipoOperacao != "Consultar");
}

function LimpaCamposFormulario()
{
    txtTituloTema.SetText(null);
    txtDescricaoTema.SetText(null);
    //ddlMapaEstrategico.SetText("");
    //ddlPerspectiva.SetText("");
    ddlResponsavel.SetText("");
    if(window.heGlossario)
        heGlossario.SetHtml("");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoObjetoEstrategia;CodigoMapaEstrategico;CodigoVersaoMapaEstrategico;TituloObjetoEstrategia;GlossarioObjeto;CodigoResponsavelObjeto;CodigoObjetoEstrategiaSuperior;TituloMapaEstrategico;ResponsavelObjeto;TituloObjetoEstrategia;DescricaoObjetoEstrategia;', MontaCamposFormulario);
        //gridDadosIndicador.PerformCallback();  
    }
}

function MontaCamposFormulario(values)
{
    /* Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    
    CodigoObjetivoEstrategico       0;
    CodigoMapaEstrategico           1;
    CodigoVersaoMapaEstrategico     2;
    TituloObjetoEstrategia          3;
    GlossarioObjeto                 4;
    CodigoResponsavelObjeto         5;
    CodigoObjetoEstrategiaSuperior  6;
    TituloMapaEstrategico           7;
    ResponsavelObjeto               8;
    */
    var separadorChave = ';';
    LimpaCamposFormulario();

    if(values)
    {
        var CodigoObjetivo          = values[0];
        var CodigoMapa              = values[1];
        var CodigoVersao            = values[2];
        var TituloObjetoEstrategia  = values[3];
        var GlossarioObjeto         = values[4];
        var CodigoResponsavel       = values[5];
        var CodigoObjetoSuperior    = values[6];
        var TituloMapaEstrategico = values[7];
        var nomeResp = values[8];
        var DescricaoObjetoEstrategia = values[10];

        hfGeral.Set("chave", CodigoObjetivo + separadorChave + CodigoMapa + separadorChave + CodigoVersao);
        
        ddlMapaEstrategico.SetText(TituloMapaEstrategico    != null ? TituloMapaEstrategico : "");
        ddlPerspectiva.SetValue(    CodigoObjetoSuperior    != null ? CodigoObjetoSuperior  : "");
        ddlResponsavel.SetValue(    CodigoResponsavel       != null ? CodigoResponsavel     : "");
        (CodigoResponsavel == null ? ddlResponsavel.SetText("") : ddlResponsavel.SetText(nomeResp));
        txtTituloTema.SetText(TituloObjetoEstrategia != null ? TituloObjetoEstrategia : "");
        txtDescricaoTema.SetText(DescricaoObjetoEstrategia != null ? DescricaoObjetoEstrategia : "");
        
        if(window.heGlossario)
            heGlossario.SetHtml(GlossarioObjeto != null ?  GlossarioObjeto : "");

        var parametro = CodigoMapa + separadorChave + CodigoObjetoSuperior;
        //ddlPerspectiva.PerformCallback(parametro);
        callbackPerspectiva.PerformCallback(parametro);
    }
}

//------------------------------------------------------------ DIV salvar
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUC'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}
//------------------------------------------------------------ fim DIV salvar

/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function onGridFocusedRowChangedPopup(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoObjetoEstrategia;TituloObjetoEstrategia;TituloMapaEstrategico;', getDadosPopup); //getKeyGvDados);
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
    window.top.showModal("../../InteressadosObjeto.aspx?ITO=TM&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa + '&AlturaGrid=' + (window_height - 110), traducao.TemasMapa_temas, window_width, window_height, '', null); 
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