// JScript File
/// <param name="marcadores" type="Array">Array de marcadores necessários para controlar a quantidade de marcadores qeue podem ser mostrados no mapa
/// </param>
var marcadores = new Array();
var map;

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

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;

    if (txtNome.GetText() == "")
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.unidadesNegocio_campo_nome_deve_ser_informado_ + " \n";

    if (txtSigla.GetText() == "")
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.unidadesNegocio_campo_sigla_deve_ser_informado_ + " \n";

    if (ddlUnidadeSuperior.GetSelectedIndex() == -1)
        mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.unidadesNegocio_campo_unidade_superior_deve_ser_informado_ + " \n";

        return mensagemErro_ValidaCamposFormulario;
}

function desabilitaHabilitaComponentes()
{
	var BoolEnabled = hfGeral.Get("TipoOperacao") != "Consultar";

	txtNome.SetEnabled(BoolEnabled);
	ddlUnidadeSuperior.SetEnabled(BoolEnabled);
    txtSigla.SetEnabled(BoolEnabled);
    txtCodigo.SetEnabled(BoolEnabled);
    checkUnidade.SetEnabled(BoolEnabled);
    ddlGerente.SetEnabled(BoolEnabled);
    txtObservacoes.SetEnabled(BoolEnabled);

}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtNome.SetText("");
    txtSigla.SetText("");
    txtCodigo.SetText("");
    txtObservacoes.SetText("");
    txtObservacoes.Validate();
    ddlUnidadeSuperior.SetSelectedIndex(-1);
    ddlUnidadeSuperior.SetText("");
    ddlGerente.SetValue(null);
    
    checkUnidade.SetChecked(true);
    
    if(hfGeral.Get("TipoOperacao").toString() == "Incluir")
        pnLogo.PerformCallback('Limpar');
    
    if("Incluir"==TipoOperacao)
        pnCallbackUnidadeSuperior.PerformCallback("IncluirNovo");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);
    //não esquecer de colocar a latitude e a longitude na função:  grid.GetRowValues
    //Recebe os valores dos campos de acordo com a linha selecionada. O resultado é passado para a função montaCampo
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetSelectedFieldValues('CodigoUnidadeNegocio;NomeUnidadeNegocio;SiglaUnidadeNegocio;IndicaUnidadeNegocioAtiva;Observacoes;CodigoUnidadeNegocioSuperior;CodigoUsuarioGerente;NivelHierarquicoUnidade;CodigoReservado;Latitude;Longitude;NomeUsuarioGerente', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{ 
    LimpaCamposFormulario();

    if(valores.length > 0)
    {
        var codigoUnidadeNegocio  = valores[0][0];
        var nomeUnidadeNegocio = valores[0][1];
        var siglaUnidadeNegecio = valores[0][2];
        var indicaUnidadeNegocioAtiva = valores[0][3];
        var observacoes = valores[0][4];
        var codigoUnidadeSuperior = valores[0][5];
        var codigoUsuarioGerente = valores[0][6];
        var nivelHierarquicoUnidade = valores[0][7];
        var codigoReservado = valores[0][8];
        var latitude = valores[0][9];
        var longitude = valores[0][10];
        var nomeGerente = valores[0][11];
        hfGeral.Set("CodigoUnidade",codigoUnidadeNegocio);
        
        txtNome.SetText(nomeUnidadeNegocio);
        txtSigla.SetText(siglaUnidadeNegecio);
        txtCodigo.SetText(codigoReservado);

        if (codigoUsuarioGerente == null) {
            ddlGerente.SetValue(null);
        }
        else {
            ddlGerente.SetValue(codigoUsuarioGerente);
            ddlGerente.SetText(nomeGerente);
        }
           
        txtObservacoes.SetText(observacoes);
        txtObservacoes.Validate();

        if(indicaUnidadeNegocioAtiva == 'S')
            checkUnidade.SetValue(true);
        else
            checkUnidade.SetValue(false);

        pnCallbackUnidadeSuperior.PerformCallback(codigoUnidadeSuperior);
        
        pnLogo.PerformCallback();     
    }
}

function logoVisible(estado)
{
    if (document.getElementById('tdImagem')!=null)
        document.getElementById('tdImagem').style.display = estado;
        
    if (document.getElementById('tbUploadLogo')!=null)
        document.getElementById('tbUploadLogo').style.display = estado;
} 

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    // se já incluiu alguma opção, feche a tela após salvar
    if (gvDados.GetVisibleRowsOnPage() > 0 )
        onClick_btnCancelar();    
}

// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------

//-----------
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);

    fechaTelaEdicao();
}

function fechaTelaEdicao()
{
    onClick_btnCancelar();
}


/*-------------------------------------------------
<summary>
colocar los campos em branco o limpos, en momento de inserir un nuevo dado.
</summary>
-------------------------------------------------*/
function OnGridFocusedRowChangedPopup(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoUnidadeNegocio;NomeUnidadeNegocio;', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor)
{
    var idObjeto     =  (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var tituloMapa = "";

    var isIE = /*@cc_on!@*/false || !!document.documentMode;
    if (isIE)
        tituloObjeto = encodeURI(tituloObjeto);
   
    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = screen.width - 100;
    var window_height = screen.height - 200;
    var newfeatures     = 'scrollbars=no,resizable=no';
    var window_top      = (screen.height-window_height)/2;
    var window_left     = (screen.width-window_width)/2;
    window.top.showModal("../_Estrategias/InteressadosObjeto.aspx?ITO=UN&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa + '&AlturaGrid=' + (window_height - 110), traducao.unidadesNegocio_permiss_es, window_width, window_height, '', null);
}

function onEnd_pnCallbackLocal(s, e)
{
	var defUnidade = hfGeral.Contains("definicaoUnidade") ? hfGeral.Get("definicaoUnidade").toString() : "Unidade";

	if("Incluir" == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(defUnidade + traducao.unidadesNegocio__inclu_da_com_sucesso_);
    else if("Editar" == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.unidadesNegocio_dados_gravados_com_sucesso_);
    else if("Excluir" == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(defUnidade + traducao.unidadesNegocio__exclu_da_com_sucesso_);
		
    if(hfGeral.Contains("StatusSalvar"))
    {
        var status = hfGeral.Get("StatusSalvar");
        if(status != "1")
        {
            if (hfGeral.Contains("ErroSalvar") && hfGeral.Get("ErroSalvar") != "") {
                
                var mensagem = hfGeral.Get("ErroSalvar");
                pnlImpedimentos.SetVisible(true);
                lblMensagemError.SetText(mensagem);

                //-------
                if (s.cp_corRC != "S") document.getElementById("corRC").style.display = "none";
                if (s.cp_corUN != "S") document.getElementById("corUN").style.display = "none";
                if (s.cp_corME != "S") document.getElementById("corME").style.display = "none";
                if (s.cp_corPP != "S") document.getElementById("corPP").style.display = "none";
                if (s.cp_corDE != "S") document.getElementById("corDE").style.display = "none";
                if (s.cp_corCO != "S") document.getElementById("corCO").style.display = "none";
                //-------

                if (gvImpedimentos.pageRowCount <= 0)
                    gvImpedimentos.SetVisible(false);
                else
                    gvImpedimentos.SetVisible(true);

                desabilitaHabilitaComponentes();
                pcMensagemGravacao.Show();

            }
            
        }
        else
        {
            if (window.onEnd_pnCallback)
                onEnd_pnCallback();
        }
    }
}

function onClick_LinkVerificarDisponibilidad(s, e)
{
	e.processOnServer = false;
	if(camposDisponibilidad()) hfNovoNomeUnidade.PerformCallback('verificar');
	else lancarMensagemSistema(traducao.unidadesNegocio_aten__o__campos_vazios__os_campos_nome_e_sigla_devem_ser_informados_para_verificar_disponibilidade_);
}

function camposDisponibilidad()
{
    var disponible = true;
    
    if(txtNome.GetText() == "" || txtSigla.GetText() == "") disponible = false;
    
    return disponible;
}

/*-------------------------------------------------
<summary>
Ao inserir uma nova entidade, ela deve asegurar não possui o mesmo nome e
a mesma sigla que uma ja cadastrada e nao excluidam uma veis pesquiçaõ 
no banco de dados se observa dois situações:

N: não existe nome no banco de dados.
E: existe o mesmo nome e possui data exclusão null.

Segundo o estado, tendra situações de resultado diferentes.
</summary>
<return></return>
<Parameters>
s: Componente HiddenField 'hfNovoNomeUnidade'.
e: Propiedade HiddenField 'hfNovoNomeUnidade'.
</Parameters>
-------------------------------------------------*/

function trataResultadoVerificacaoNomeEntidade(s,e)
{
    var situacaoNome = hfNovoNomeUnidade.Get("NomeVerificarNovaUnidade");
    var situacaoSigla = hfNovoNomeUnidade.Get("SiglaVerificarNovaUnidade");
    
    var situacionCodigoReservado = hfNovoNomeUnidade.Get("CodigoVerificarNovaUnidade");
    var mensagem1 = traducao.unidadesNegocio_erro_ao_incluir_a_unidade_de_neg_cio_;
    var mensagem2 = "";
    var statusErro = 0;
    
    if(situacaoNome == "E")
    {
        mensagem1 += "<br> - " + traducao.unidadesNegocio_j__existe_uma_unidade_de_neg_cio_com_o_nome_informado_;
        statusErro = 1;
    }
    if(situacaoNome == "E")
    {
        mensagem1 += "<br> - " + traducao.unidadesNegocio_j__existe_uma_unidade_de_neg_cio_com_a_sigla_informada_;
        statusErro = 1;
    }
    if(situacaoNome == "E")
    {
        mensagem1 += "<br> - " + traducao.unidadesNegocio_j__existe_uma_unidade_de_neg_cio_com_o_c_digo_reservado_informado_;
        statusErro = 1;
    }

    if(statusErro == 1) 
        lancarMensagemSistema(mensagem1);
    else
    {
        if (window.onClick_btnSalvar)
	        onClick_btnSalvar();
    }
}



/*-------------------------------------------------
<summary>setea o conteudo del label do popup 'pcMensagemVarios', apos lança o evento Show().</summary>
<Parameters>mensagem: texto a ser mostrado.</Parameters>
-------------------------------------------------*/
function lancarMensagemSistema(mensagem1, mensagem2)
{
    lblMensagemVarios.SetText(mensagem1);
    lblMensagemVarios2.SetText(mensagem2);
    pcMensagemVarios.Show();
}

function onClick_btnSalvarUnidade(s, e)
{
    e.processOnServer = false;
    var acao = hfGeral.Get("TipoOperacao");
    
    if(acao == "Incluir")
    {
        if(camposDisponibilidad()) hfNovoNomeUnidade.PerformCallback('verificarNovo');
        else 
        {   //lancarMensagemSistema("Atenção! campos vacios. Os campos Nome e Sigla devem ser informados para verificar disponibilidade.");
            if (window.onClick_btnSalvar)
	            onClick_btnSalvar();
        }
    }
    else if(acao == "Editar")
    {
        if(camposDisponibilidad()) hfNovoNomeUnidade.PerformCallback('verificarEditar');
        else 
        {   //lancarMensagemSistema("Atenção! campos vacios. Os campos Nome e Sigla devem ser informados para verificar disponibilidade.");
            if (window.onClick_btnSalvar)
	            onClick_btnSalvar();
        }
    }
}

function onClick_CustomIncluirUnidade()
{
    btnSalvar1.SetVisible(true);
    onClickBarraNavegacao("Incluir", gvDados, pcDados);
    TipoOperacao = "Incluir";
}

//=======================================================================================================================================
//==============================================SEÇÃO DE MANIPULAÇÃO DO MAPA DO GOOGLE MAPS==============================================
//=======================================================================================================================================


///<summary>Inicializa o mapa que será utilizado para que o usuário escolha as coordenadas
///da unidade selecionada.
///</summary>
///<returns type="void"></returns>
function initialize() {


    var w = ASPxClientUtils.GetDocumentClientWidth() - 12;
    var h = ASPxClientUtils.GetDocumentClientHeight() - 10;

    var widthEfetivo = parseInt(w - (0.4 * w));
    var heightEfetivo = parseInt(h - (0.5 * h));


    document.getElementById('divGoogleMaps').style.cssText = "width:" + widthEfetivo + "px;height:" + heightEfetivo + "px; overflow: hidden; position: relative; background-color: rgb(229, 227, 223);";

    /// <param name="latitudeInicialParametroBD" type="decimal">No caso de não haver coordenadas definidas ou se o usuário estiver ainda incluindo a primeira coordenada
    /// então uma coordenada de algum parametro é utilizada.
    /// </param>
    var latitudeInicialParametroBD = -15.794083;

    /// <param name="longitudeInicialParametroBD" type="decimal">No caso de não haver coordenadas definidas ou se o usuário estiver ainda incluindo a primeira coordenada
    /// então uma coordenada de algum parametro é utilizada.
    /// </param>
    var longitudeInicialParametroBD = -47.882541;

    /// <param name="textoEnderecoInicialParametroBD" type="decimal">No caso de não haver coordenadas definidas ou se o usuário estiver ainda incluindo a primeira coordenada
    /// então uma coordenada de algum parametro é utilizada.
    /// </param>
    var textoEnderecoInicialParametroBD = "";

    var latitudeInicial = (txtLatitude.GetText() == "" || txtLatitude.GetText() == "0") ? latitudeInicialParametroBD : txtLatitude.GetText();
    var longitudeInicial = (txtLongitude.GetText() == "" || txtLongitude.GetText() == "0") ? longitudeInicialParametroBD : txtLongitude.GetText();
    
    var mapOptions = {
        zoom: 8,
        center: new google.maps.LatLng(latitudeInicial, longitudeInicial)
    };
    map = new google.maps.Map(document.getElementById('divGoogleMaps'),
      mapOptions);

    //Comando necessário para que o popup control se alinhe novamente as configurações definidas pelo usuário
    pcLatitudeLongitude.UpdatePosition();

    //inicialmente o marcador inicial é mostrado no mapa.
    deleteMarkers();
    addMarker(new google.maps.LatLng(latitudeInicial, longitudeInicial));

    //O evento de capturar as coordenadas com um clique é adicionado no ambito do mapa
    google.maps.event.addListener(map, 'click', capturarCoordenadas);

}

///<summary>Função chamada toda vez que o usuário faz um clique no mapa</summary>
/// <param name="evento" type="google.maps.event">Variável do tipo evento em que é possível acessar em qual coordenada que o usuário clicou no mapa.
/// Este evento captura a coordenada, escreve a coordenada no campo de texto do canto superior da tela e executa a rotina de adicionar o
/// marcador.
/// </param>
///<returns type="void"></returns>
function capturarCoordenadas(evento) {
    var retorno;
    retorno = evento.latLng.lat() + "," + evento.latLng.lng();
    
    txtLatitude.SetText(evento.latLng.lat());
    txtLongitude.SetText(evento.latLng.lng());
    
    //txtConfirmaCoordenadas.SetText(retorno);
    deleteMarkers();
    addMarker(evento.latLng);

}

///<summary>Função chamada quando o usuário inicia o arrasto do marcador, esta função coloca o marcador na pilha (variável do tipo array)
///Faz uma busca ao redor do ponto que o usuário escolheu em um raio de 1 (km??)
/// e adiciona um 'ouvidor' de eventos no marcador em que se acabou de inserir. 
///</summary>
/// <param name="location" type="google.maps.LatLng">Parametro que vem com as coordenadas na qual o marcador deverá ser
/// colocado no mapa
/// </param>
///<returns type="void"></returns>
function addMarker(location) {
    var marker = new google.maps.Marker({
        position: location,
        draggable:true,
        map: map
    });
    marcadores.push(marker);

    //define uma variável do tipo request que será utilizada pelo campo txtConfirmaCoordenadas
    //que consistem em buscar um endereço próximo ao ponto que o usuário colocou o marcador, o campo chama-se: txtConfirmaCoordenadas
    var request = {
        location: location,
        radius: 2
    };
    //define uma variável do tipo serviços do google
    //que vai disparar um evento para procurar áreas ao redor do ponto onde o usuário clicou
    var service = new google.maps.places.PlacesService(map);
    service.nearbySearch(request, buscaEnderecosAoRedor);

    google.maps.event.addListener(marker, 'drag', arrastarMarcador);
    google.maps.event.addListener(marker, 'dragend', arrastarMarcadorFim);
}

///<summary>Função chamada quando o usuário inicia o arrasto do marcador. Esta função faz uma busca de um (km??)
///ao redor do ponto que o usuário esta arrastando o marcador</summary>
/// <param name="evento" type="string">Parãmetro que dá acesso às propriedades do evento
/// </param>
///<returns type="void"></returns>
function arrastarMarcador(evento) {
    
    //define uma variável do tipo request que será utilizada pelo campo txtConfirmaCoordenadas
    //que consistem em buscar um endereço próximo ao ponto que o usuário colocou o marcador, o campo chama-se: txtConfirmaCoordenadas
    var request = {
        location: evento.latLng,
        radius: 1
    };
    //define uma variável do tipo serviços do google
    //que vai disparar um evento para procurar áreas ao redor do ponto onde o usuário clicou
    var service = new google.maps.places.PlacesService(map);
    service.nearbySearch(request, buscaEnderecosAoRedor);
    
    var retorno;
    //Formata as coordenadas e escreve no campo texto.
    retorno = evento.latLng.lat() + "," + evento.latLng.lng();
    //txtConfirmaCoordenadas.SetText(retorno);

    txtLatitude.SetText(evento.latLng.lat());
    txtLongitude.SetText(evento.latLng.lng());

}


///<summary>Função chamada quando o usuário arrasta um marcador de um lugar para outro, utilizando o mouse e em seguida, solta.
///</summary>
/// <param name="evento" type="string">Parãmetro que dá acesso às propriedades do evento
/// </param>
///<returns type="void"></returns>
function arrastarMarcadorFim(evento) {

    var request = {
        location: evento.latLng,
        radius: 1
    };
    var service = new google.maps.places.PlacesService(map);
    service.nearbySearch(request, buscaEnderecosAoRedor);

    var retorno;
    retorno = evento.latLng.lat() + "," + evento.latLng.lng();
    //txtConfirmaCoordenadas.SetText(retorno);

    txtLatitude.SetText(evento.latLng.lat());
    txtLongitude.SetText(evento.latLng.lng());
}

///<summary>Função chamada a partir de um evento de clique no mapa, quando o usuário clica no mapa, o endereço mais próximo é mostrado
///no campo: 'txtEnderecoMaisProximo'.
///</summary>
/// <param name="results" type="string">Parametro que vem com a lista de resultados buscados pela função de serviços do google
/// </param>
/// <param name="status" type="string">Parametro que vem com o código do status do serviço, 
/// </param>
///<returns type="void"></returns>
function buscaEnderecosAoRedor(results, status) {
    if (status == google.maps.places.PlacesServiceStatus.OK) {
        if (results.length > 0) {
            if (results[0].name.toString().indexOf("?") != 0) {
                txtEnderecoMaisProximo.SetText(results[0].name);
            }
            else {
                txtEnderecoMaisProximo.SetText("Brasília");
            }
        }
    }
}


///<summary>Coloca todos os marcadores no vetor, apontando para o mapa no array.</summary>
/// <param name="map" type="string">Parametro que vem com o mapa carregado no sistema 
/// </param>
///<returns type="void"></returns>
function setAllMap(map) {
    for (var i = 0; i < marcadores.length; i++) {
        marcadores[i].setMap(map);
    }
}

///<summary>Apenas remove os marcadores do mapa, mas os mesmos ainda ficam armazenados no array</summary>
///<returns type="void"></returns>
function clearMarkers() {
    setAllMap(null);
}


///<summary>Mostra qualquer marcador que esteja no array</summary>
///<returns type="void"></returns>
function showMarkers() {
    setAllMap(map);
}

///<summary> Exclui todos os marcadores do Array.</summary>
///<returns type="void"></returns>
function deleteMarkers() {
    clearMarkers();
    marcadores = [];
}

function OnGridFocusedRowChangedGoogleMaps(grid) {
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoUnidadeNegocio;Latitude;Longitude;', getDadosPopupGoogleMaps); //getKeyGvDados);
}

function getDadosPopupGoogleMaps(dados) {

    var latitudeInicialParametroBD = -15.794083;
    var longitudeInicialParametroBD = -47.882541;

    txtLatitude.SetText(dados[1] != null ? dados[1] : latitudeInicialParametroBD);
    txtLongitude.SetText(dados[2] != null ? dados[2] : longitudeInicialParametroBD);
    txtEnderecoMaisProximo.SetText("Brasília");
    hfGeral.Set("CodigoUnidade", dados[0]);
    pcLatitudeLongitude.Show();
       
    //callback1.PerformCallback(strCallback);
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