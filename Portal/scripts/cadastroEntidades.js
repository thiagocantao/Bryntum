// JScript File
/// <param name="marcadores" type="Array">Array de marcadores necessários para controlar a quantidade de marcadores qeue podem ser mostrados no mapa
/// </param>
var marcadores = new Array();
var map;
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

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function validateEmail(elementValue)
{      
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(elementValue); 
}
    
function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;
    var eMail  = txtEmail.GetText();
    var definiciaoEntidade = hfGeral.Get("definicaoEntidade");
    if("" == definiciaoEntidade)
        definiciaoEntidade = "Entidade";
    
        //validação da Entidad 
        if(txtSigla.GetText() == "")
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroEntidades_a_sigla_de_ + definiciaoEntidade + traducao.cadastroEntidades__deve_ser_informado_ + "\n";
            
        if(txtEntidade.GetText() == "")
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroEntidades_nome_de_ + definiciaoEntidade + traducao.cadastroEntidades__deve_ser_informado_ + "\n";
        
        //validação do Usuario
        if(txtNome.GetText() == "")
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroEntidades_nome_do_usu_rio_deve_ser_informado_ + "\n";
            
        if(txtEmail.GetText() == "")
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroEntidades_email_do_usu_rio_deve_ser_informado_ + "\n";
        else
            if (!validateEmail(eMail))
                mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.cadastroEntidades_por_favor__forne_a_um_endere_o_de_e_mail_v_lido_ + "\n"; 

    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario()
{
    // Função responsável por preparar os campos do formulário para receber um novo registro
    txtSigla.SetText("");
    txtEntidade.SetText("");
    txtNome.SetText("");
    txtEmail.SetText("");
    txtFone.SetText("");
    txtCodigoReservado.SetText("");
    memObservacoes.SetText("");
    memObservacoes.Validate();
    comboUF.SetSelectedIndex(-1);
    
    ckbEntidadAtiva.SetChecked(true);
    
    hfGeral.Set("hfCodigoUnidadeNegocio", "");
    
    desabilitaHabilitaComponentes();
    
    if(hfGeral.Get("TipoOperacao").toString() == "Incluir")
        pnLogo.PerformCallback('Limpar');

}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoUnidadeNegocio;SiglaUnidadeNegocio;NomeUnidadeNegocio;CodigoUsuarioGerente;SiglaUF;IndicaUnidadeNegocioAtiva;Observacoes;NomeUsuario;Email;TelefoneContato1;CodigoReservado;', MontaCamposFormulario);
}

function MontaCamposFormulario(valores)
{
    LimpaCamposFormulario();
    if(valores)
    {
        //debugger
        var CodigoUnidade = (valores[0] == null) ? "" : valores[0].toString();
        var SiglaUnidadeNegocio = (valores[1] == null) ? "" : valores[1].toString();
        var NomeUnidadeNegocio = (valores[2] == null) ? "" : valores[2].toString();
        var CodigoUsuarioGerente = (valores[3] == null) ? "" : valores[3].toString();
        var SiglaUF = (valores[4] == null) ? "" : valores[4].toString();
        var IndicaUnidadeNegocioAtiva = (valores[5] == null) ? "" : valores[5].toString();
        var Observacoes = (valores[6] == null) ? "" : valores[6].toString();
        var NomeUsuario = (valores[7] == null) ? "" : valores[7].toString();
        var Email = (valores[8] == null) ? "" : valores[8].toString();
        var TelefoneContato1 = (valores[9] == null) ? "" : valores[9].toString();
        var codigoReservado =  (valores[10] == null) ? "" : valores[10].toString();

        txtSigla.SetText(SiglaUnidadeNegocio);
        txtEntidade.SetText(NomeUnidadeNegocio);
        comboUF.SetValue(SiglaUF);
        //comboUF.SetText(SiglaUF);

        if(IndicaUnidadeNegocioAtiva == "S")
            ckbEntidadAtiva.SetChecked(true);
        else
            ckbEntidadAtiva.SetChecked(false);

        txtNome.SetText(NomeUsuario);
        txtEmail.SetText(Email);
        txtFone.SetText(TelefoneContato1);
        memObservacoes.SetText(Observacoes);
        memObservacoes.Validate();
        hfGeral.Set("hfCodigoUnidadeNegocio",CodigoUnidade);    
        hfGeral.Set('NomeArquivo', '');
        txtCodigoReservado.SetText(codigoReservado);
        var unidadeLogada = hfGeral.Get("codigoEntidadeLogada");
        
            txtNome.SetEnabled(false);
            txtFone.SetEnabled(false);
            hfGeral.Set("CodigoUnidade",CodigoUnidade);
        //hfGeral.Set("LogoUnidadeNegocio", (valores[10] == null) ? "" : valores[10].toString());
        pnLogo.PerformCallback();
        
    }
}

/*-------------------------------------------------------------------------------------------
 Setea a tabela tbAcao que contem ao girAcoes, si sera visivel o nao segundo o parametro
 estado:    block = visivel.
            none = nao visivel.
-------------------------------------------------------------------------------------------*/
function logoVisible(estado)
{
    if (document.getElementById('tdImagem')!=null)
        document.getElementById('tdImagem').style.display = estado;
        
    if (document.getElementById('tbUploadLogo')!=null)
        document.getElementById('tbUploadLogo').style.display = estado;
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
    //var tipoOperacaoSel = TipoOperacao; hfGeral.Get("TipoOperacao");
	var BoolEnabled = (TipoOperacao == "Editar" || TipoOperacao == "Incluir") ? true : false;
	
    //Dados Administrador

    if("Incluir" == TipoOperacao)
	{
	    btnSalvar.SetEnabled(true);
	    hlkVerificarEmail.SetVisible(true);
        hlkVerificarEmail.SetEnabled(false);
        txtEmail.SetEnabled(true);
        txtNome.SetEnabled(false);
        txtFone.SetEnabled(false);
    }
	else if("Editar" == TipoOperacao)
	{
	    btnSalvar.SetEnabled(true);
	    hlkVerificarEmail.SetVisible(true);
        hlkVerificarEmail.SetEnabled(false);
        txtEmail.SetEnabled(true);
        txtNome.SetEnabled(true);
        txtFone.SetEnabled(true);

    }
	else
	{
        hlkVerificarEmail.SetVisible(false);
        txtEmail.SetEnabled(false);
        txtNome.SetEnabled(false);
        txtFone.SetEnabled(false);

    }
    

    //Dados Entidade
    txtSigla.SetEnabled(BoolEnabled);
    txtEntidade.SetEnabled(BoolEnabled);
    memObservacoes.SetEnabled(BoolEnabled);
    memObservacoes.Validate();
    ckbEntidadAtiva.SetEnabled(BoolEnabled);
    comboUF.SetEnabled(BoolEnabled);
    hideImagenUp(BoolEnabled);
    txtCodigoReservado.SetEnabled(BoolEnabled);
}

/*-------------------------------------------------------------------
    Función: hideImagenUp(checked)
    Parámetro: true / false.
    retorno: void.
    Descripción: Mudara la visibilidad de la Tablet que contiene el 
                 componente que hace referencia al fileup. Utilizado 
                 para cargar nuevos logos para las entidades.
-------------------------------------------------------------------*/
function hideImagenUp(checked)
{
    //var hide = checked ? "block" : "none";
    //var tab = document.getElementById("tbImagenUp");
    
    //tab.style.display = hide;
}

/*-------------------------------------------------------------------
    Función: mostraDivSalvoPublicado(acao)
    Parámetro: cadena de texto.
    retorno: void.
    Descripción: Muestra un panelControl por un tiempo determinado.
                 Funciona junto a la funcion fechaTelaEdicao(), que
                 se encargara de cerrar el panelControl.
-------------------------------------------------------------------*/
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

/*-------------------------------------------------------------------
    Función: lancarMensagemSistema(mensagem)
    Parámetro: cadena de texto.
    retorno: void.
    Descripción: Muestra un panelControl con el testo enviado como 
                 parámetro. Usado para mostrar mensajes informativos
                 al usuário.
-------------------------------------------------------------------*/
function lancarMensagemSistema(mensagem)
{
    lblMensagemVarios.SetText(mensagem);
    pcMensagemVarios.Show();
}

/*-------------------------------------------------------------------
    Función: verificarEmailUsuarioNovo()
    retorno: boolean.
    Descripción: Verificará (usando comparación regex) la validez del 
                 E-mail digitado en el comboBox txtEmail. Retornara 
                 'true' caso que el E-mail sea correcto, 'false' 
                 caso contrario.
-------------------------------------------------------------------*/
function verificarEmailUsuarioNovo()
{
    var retorno = true;
    var msgEmail = "";
    var eMail  = txtEmail.GetText();
    
	if("" != eMail){
        if (!validateEmail(eMail))
            msgEmail = traducao.cadastroEntidades_por_favor__forne_a_um_endere_o_de_e_mail_v_lido;
    }
	else
		msgEmail = traducao.cadastroEntidades_por_favor__forne_a_um_endere_o_de_e_mail_v_lido;
	
	if("" != msgEmail)
	{
		lancarMensagemSistema(msgEmail);
		retorno = false;
	}
	return retorno;
}

/*-------------------------------------------------------------------
    Función: trataResultadoVerificacaoEmail(s,e)
    retorno: void.
    Descripción: Verificará el resultado de haber testado un E-mail
                 teniendo en cuenta las siguientes condiciones:

  situacaoEmail : 'NO' -> NOVO, email inexistente no banco de dados.
                  'EX' -> EXcluido.
                  'OE' -> ATivo Outra Entidade.    
                  'NE' -> ATivo Nesta Entidade.
                  
                 Según el resultado, llamará a funciones determinadas.
                 Las funciones que se encuentra en relación son las
                 siguientes:
                 simUsuarioNovo() : El E-mail no existe en el banco
                                    de datos.
                 simUsuarioNestaEntidade() : El E-mail existe en la
                                             entidad logada.
-------------------------------------------------------------------*/
function trataResultadoVerificacaoEmail(s,e){
    var estadoTela = hfGeral.Get("TipoOperacao");
    var situacaoEmail = hfEmailAdministrador.Get("emailAdministrador");

    if("Editar" == estadoTela)
    {
        if("NE" == situacaoEmail)
        {
            simUsuarioNestaEntidade();
            btnSalvar.SetEnabled(true);
        }
        else
        {
            var parametroTipo = hfGeral.Contains("definicaoEntidade") == true ? hfGeral.Get("definicaoEntidade").toString() : "Entidade";
            lancarMensagemSistema(traducao.cadastroEntidades_o_e_mail_n_o_existe_na_ + parametroTipo + traducao.cadastroEntidades___cadastre_primero_na_tela_usuario_e_tente_novamente_);
        }
    }
    else if ("Incluir" == estadoTela)
    {
        if("NO" == situacaoEmail)
            simUsuarioNovo();
        else if( "EX" == situacaoEmail)
            pcUsuarioExcluido.Show();
        else if("OE" == situacaoEmail)
            pcEntidadActual.Show();
        else if("NE" == situacaoEmail)
            simUsuarioNestaEntidade();
    }
        
    //pnCallbackFormulario.PerformCallback(situacaoEmail);
}

function simUsuarioNovo(){
    // emailAdministrador = 'NO'.
    txtEmail.SetEnabled(false);
    hlkVerificarEmail.SetEnabled(false);
    pnCallbackFormulario.PerformCallback("NO");
}

function simUsuarioNestaEntidade(){
    // emailAdministrador = 'NE'.
    txtEmail.SetEnabled(false);
    hlkVerificarEmail.SetEnabled(false);
    pnCallbackFormulario.PerformCallback("NE");
}

/*-------------------------------------------------------------------
    Función: SimUsuarioExcluido_OnClick()
    retorno: void.
    Descripción: Setea los componentes relacionado al cadastro de
                 usuario. 
-------------------------------------------------------------------*/
function SimUsuarioExcluido_OnClick(){
    // emailAdministrador = 'EX'.
    pcUsuarioExcluido.Hide();
    txtEmail.SetEnabled(false);
    hlkVerificarEmail.SetEnabled(false);
    pnCallbackFormulario.PerformCallback("EX");
}

/*-------------------------------------------------------------------
    Función: SimUsuarioExcluido_OnClick()
    retorno: void.
    Descripción: Setea los componentes relacionado al cadastro de
                 usuario. 
-------------------------------------------------------------------*/
function SimEntidadAtual_OnClick(){
    // emailAdministrador = 'OE'.
    pcEntidadActual.Hide();
    txtEmail.SetEnabled(false);
    hlkVerificarEmail.SetEnabled(false);
    pnCallbackFormulario.PerformCallback("OE");
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
    var tituloMapa   = "";
   
    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = screen.width - 100;
    var window_height = screen.height - 200;
    var newfeatures     = 'scrollbars=no,resizable=yes';
    var window_top      = (screen.height-window_height)/2;
    var window_left     = (screen.width-window_width)/2;

    window.top.showModal("../_Estrategias/InteressadosObjeto.aspx?ITO=EN&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + tituloMapa + '&AlturaGrid=' + (window_height - 110), traducao.cadastroEntidades_permiss_es, null, null, '', null);
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
        draggable: true,
        map: map
    });
    marcadores.push(marker);

    //define uma variável do tipo request que será utilizada pelo campo txtConfirmaCoordenadas
    //que consistem em buscar um endereço próximo ao ponto que o usuário colocou o marcador, o campo chama-se: txtConfirmaCoordenadas
    var request = {
        location: location,
        radius: 1
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

function excluiRegistro() {
    gvDados.PerformCallback('Excluir');
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