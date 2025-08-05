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
        mensagemErro_ValidaCamposFormulario += "\n- " + num + traducao.PerspectivasMapa__o_mapa_deve_ser_informado;
    }
    if (txtPerspectiva.GetText() == "") {
        num = num + 1;
        mensagemErro_ValidaCamposFormulario += "\n- " + num + traducao.PerspectivasMapa__a_descri__o_da_perspectiva_deve_ser_informada;
    }       

    return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{    
    txtPerspectiva.SetEnabled(      TipoOperacao != "Consultar");
    ddlMapaEstrategico.SetEnabled(  TipoOperacao != "Consultar");
    ddlMapaEstrategico.SetEnabled(  TipoOperacao != "Editar");
    ddlResponsavel.SetEnabled(      TipoOperacao != "Consultar");
    if(window.heGlossario)
        heGlossario.SetEnabled(TipoOperacao != "Consultar");
}

function LimpaCamposFormulario()
{
    txtPerspectiva.SetText(null);
    //ddlMapaEstrategico.SetText("");
    ddlResponsavel.SetText("");
    if(window.heGlossario)
        heGlossario.SetHtml("");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoObjetoEstrategia;CodigoMapaEstrategico;CodigoVersaoMapaEstrategico;TituloObjetoEstrategia;GlossarioObjeto;CodigoResponsavelObjeto;TituloMapaEstrategico;ResponsavelObjeto', MontaCamposFormulario);
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
    TituloMapaEstrategico           6;
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
        var TituloMapaEstrategico   = values[6];
        var NomeUsuario             = values[7]; 

        hfGeral.Set("chave", CodigoObjetivo + separadorChave + CodigoMapa + separadorChave + CodigoVersao);
        
        ddlMapaEstrategico.SetText(TituloMapaEstrategico    != null ? TituloMapaEstrategico : "");
        ddlResponsavel.SetValue(CodigoResponsavel != null ? CodigoResponsavel : "");
        ddlResponsavel.SetValue(CodigoResponsavel != null ? CodigoResponsavel : "");
        ddlResponsavel.SetText(NomeUsuario != null ? NomeUsuario : "");

        
        txtPerspectiva.SetText(TituloObjetoEstrategia != null ? TituloObjetoEstrategia : "");
        
        if(window.heGlossario)
            heGlossario.SetHtml(GlossarioObjeto != null ?  GlossarioObjeto : "");
    }
}

//------------------------------------------------------------ DIV salvar
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
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
    var window_width = screen.availWidth - 200;
    var window_height = screen.availHeight - 300;
    var newfeatures     = 'scrollbars=no,resizable=no';
    var window_top      = (screen.height-window_height)/2;
    var window_left = (screen.width - window_width) / 2;
    //  function showModal(sUrl                                                                                                                                   , sHeaderTitulo                         , sWidth      , sHeight      , sFuncaoPosModal, objParam)
    window.top.showModal("../../InteressadosObjeto.aspx?ITO=PP&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa + '&AlturaGrid=' + (window_height - 20), traducao.PerspectivasMapa_perspectivas, window_width, window_height, null, null); 
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 85;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
