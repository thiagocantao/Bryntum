//========================================================================================================================================
//============================================VARIÁVEIS GLOBAIS UTILIZADAS PELO SISTEMA===================================================
//========================================================================================================================================

/// <param name="infoWindow" type="google.maps.infoWindow">Variável global necessária para que seja possível ao usuário clicar no marcador, ou em qualquer area 
/// do mapa e ser exibida para o mesmo, informações relativas a localidade que o usuário clicou. e ser possível ao desenvolvedor formatar conteúdo no infowindow.   
/// </param>
var infoWindow = new google.maps.InfoWindow();


/// <param name="map" type="google.maps.map">Variável global que armazena o mapa que será manipulado pelo usuário. Esta variável é 
/// global e várias funções a chamam 
/// </param>
var map;

/// <param name="markers" type="Array">Variável que será utilizada para armazenar o conjunto de pontos que o usuário achou
/// Na busca que o mesmo fez. 
/// </param>
var markers = [];

/// <summary>
/// Função que é chamada para inicializar todos os dados do mapa escolhido. Esta função é a primeira a ser chamada, assim que se inicializa
/// o sistema.
/// </summary>
/// <returns type="void"></returns>
function initialize() {
    //debugger
    getXmlDocDaOrigem();
    
}
/// <summary>
/// Devolve um objeto DOM XML contendo o XML original a partir do componente hiddenfield, depois disso ele redireciona para a função que manipula
///o arquivo xml retornado: getXMLDadosGrafico(xmlDoc)
/// </summary>
/// <returns type="void"></returns>
function getXmlDocDaOrigem() {

    var xmlDoc = null;
    //O conteúdo xml deste hiddenfield é gerado no lado do servidor, favor verificar.
    var xml = hfGeral.Get("xmlReferenciamento");

    if ((null != xml) && (xml.length > 0))
        xmlDoc = xmlDocFromString(xml);
    else {
        window.top.mostraMensagem(traducao.geoReferenciamento_o_arquivo_xml_referenciado_n_o___v_lido_para_plotar_os_pontos_no_mapa___ncontate_o_administrador_, 'Atencao', true, false, null);
    }

    getXMLDadosGrafico(xmlDoc);
}

/// <summary>
/// Funcão que efetivamente carrega o arquivo xml, esta função sempre é chamada da função downloadUrl.
/// Esta função caracteriza-se por capturar as propriedades XML do mapa escolhido e montá-lo na tela para o usuário.
/// </summary>
/// <param name="markXML" type="string">Parametro que é um objeto XML contendo os dados do mapa retornados
/// </param>
/// <returns type="void"></returns>
function getXMLDadosGrafico(markXML) {


    var bounds = new google.maps.LatLngBounds();

    var latitudeCentralMapa = Number(markXML.documentElement.getAttribute("lat")); //lat="-15.833753" lng="-48.052957"
    var longitudeCentralMapa = Number(markXML.documentElement.getAttribute("lng")); //lat="-15.833753" lng="-48.052957"
    var mostra_panControl = (markXML.documentElement.getAttribute("mostra_panControl").toString() == "true") ? true : false;
    var mostra_zoomControl = (markXML.documentElement.getAttribute("mostra_zoomControl").toString() == "true") ? true : false;
    var mostra_mapTypeControl = (markXML.documentElement.getAttribute("mostra_mapTypeControl").toString() == "true") ? true : false;
    var mostra_scaleControl = (markXML.documentElement.getAttribute("mostra_scaleControl").toString() == "true") ? true : false;
    var mostra_streetViewControl = (markXML.documentElement.getAttribute("mostra_streetViewControl").toString() == "true") ? true : false;
    var mostra_overviewMapControl = (markXML.documentElement.getAttribute("mostra_overviewMapControl").toString() == "true") ? true : false;
    var zoomInicial = Number(markXML.documentElement.getAttribute("zoom"));
    var mapOptions = {
        zoom: zoomInicial,
        center: new google.maps.LatLng(latitudeCentralMapa, longitudeCentralMapa),
        panControl: mostra_panControl,
        zoomControl: mostra_zoomControl,
        mapTypeControl: mostra_mapTypeControl,
        scaleControl: mostra_scaleControl,
        streetViewControl: mostra_streetViewControl,
        overviewMapControl: mostra_overviewMapControl
    };
    map = new google.maps.Map(document.getElementById("divGoogleMaps"),
                                mapOptions);
    

    var markers = markXML.documentElement.getElementsByTagName("marker"); // <marker id="1000" lat=...
    var pontosPoligono = markXML.documentElement.getElementsByTagName("ponto"); // <marker id="1000" lat=...



    var markerPositions = new Array();
    var markerColors = new Array();


    var latlng;
    var pinColo;
    var arrastavel;
    var titulo;
    var urlDaImagem;
    var pinImage;
    var pinShadow;
    var imageFromUrl;
    var shape;
    


    //laço que percorre todos os marcadores encontrados no xml
    for (var i = 0; i < markers.length; i++) {
        var latlng = new google.maps.LatLng(Number(markers[i].getAttribute("lat")),
                                    Number(markers[i].getAttribute("lng")));
        markerPositions[i] = latlng;
        pinColor = markers[i].getAttribute("cor");
        arrastavel = markers[i].getAttribute("arrastavel");
        arrastavel = (arrastavel == "S");
        titulo = markers[i].getAttribute("title");

        urlDaImagem = markers[i].getAttribute("status");

        pinImage = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + pinColor,
                        new google.maps.Size(21, 34),
                        new google.maps.Point(0, 0),
                        new google.maps.Point(10, 34));
        pinShadow = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_shadow",
                        new google.maps.Size(40, 37),
                        new google.maps.Point(0, 0),
                        new google.maps.Point(12, 35));

        imageFromUrl = {
            url: urlDaImagem, //'imagens/vermelho.gif',
            // Este marcador tem 20px de largura por 32 pixels de altura.
            size: new google.maps.Size(20, 32),
            // A origem da imagem é igual a zero. is 0,0.
            origin: new google.maps.Point(0, 0),
            //A ancoragem da imagem é a de  um ponto nas coordenadas (0,32)
            anchor: new google.maps.Point(0, 32)
        };
        //O formato da imagem do icone é a de um polygono
        shape = {
            coord: [1, 1, 1, 20, 18, 20, 18, 1],
            type: 'poly'
        };


        var markerx = new google.maps.Marker({
            position: latlng,
            map: map,
            icon: pinImage,
            shadow: pinShadow,
            //shape: shape,
            draggable: arrastavel,
            title: titulo
        });
        markerx.setMap(map);
        bounds.extend(latlng);

        google.maps.event.addListener(markerx, 'click', showArrays);

    }
    //comando que faz com que todos os marcadores aparecam no quadrante (visualização) atual do mapa
    map.fitBounds(bounds);
    
    //função chamada pelo arquivo de scripts:
    //scripts/poligono.js
    //plotaPligono(map);

    var poligonoCoordenadas = [];
    //laço que percorre todos os pontos do poligono encontrados no xml
    for (var i = 0; i < pontosPoligono.length; i++) {
        var posicao = new google.maps.LatLng(pontosPoligono[i].getAttribute("latitude").toString().replace(",", "."), pontosPoligono[i].getAttribute("longitude").toString().replace(",", "."));
        poligonoCoordenadas.push(posicao);
    }
    // Construct the polygon.
    poligono = new google.maps.Polygon({
        paths: poligonoCoordenadas,
        strokeColor: '#FF0000',
        strokeOpacity: 0.8,
        strokeWeight: 3,
        fillColor: '#FF0000',
        fillOpacity: 0.35
    });
    poligono.setMap(map);
}



/// <summary>
///Função utilizada para testar a funcionalidade de capturar informações de um evento de clicar em uma posição qualquer do mapa.
/// </summary>
/// <param name="event" type="google.maps.event">variável de evento utilizada pela função para exibir, por exemplo, as coordenadas que um determinado
/// marcador está. Esta variável proporciona outras coisas.
/// </param>
/// <returns type="void"></returns>
function showArrays(event) {
    //debugger

    var conteudoPopup = getHtmlMarcadorPorPosicao(event.latLng);

    infoWindowOpcoes = ({ pixelOffset: new google.maps.Size(0, -30)});

    infoWindow.setContent(conteudoPopup);

    var latitude1 = Number(event.latLng.k);
    var longitude1 = event.latLng.A;

    var latlng = new google.maps.LatLng(latitude1, longitude1);

    infoWindow.setPosition(latlng);
    infoWindow.setOptions(infoWindowOpcoes);
    infoWindow.open(map);
}


function getHtmlMarcadorPorPosicao(posicao) {
    var xmlDoc = null;
    //O conteúdo xml deste hiddenfield é gerado no lado do servidor, favor verificar.
    var xml = hfGeral.Get("xmlReferenciamento");

    if ((null != xml) && (xml.length > 0))
        xmlDoc = xmlDocFromString(xml);
    else {
        window.top.mostraMensagem(traducao.geoReferenciamento_o_arquivo_xml_referenciado_n__o____v__lido_para_plotar_o_mapa__contate_o_administrador_, 'Atencao', true, false, null);
    }

    var markers = xmlDoc.documentElement.getElementsByTagName("marker"); // <marker id="1000" lat=...

    var conteudoPopupRetorno = '';

    for (var i = 0; i < markers.length; i++) {
        if (markers[i].getAttribute("lat") == posicao.k.toString().substring(0, 10)) {
            conteudoPopupRetorno = markers[i].textContent;
        }
    }
    return conteudoPopupRetorno;
}

//========================================================================================================================================
//=====================================================SEÇÃO DE FUNÇÕES XML===============================================================
//========================================================================================================================================

/// <summary>
/// Devolve um objeto DOM XML dado um conteúdo XML dentro de uma string. Devolve NULL em caso de erro.
/// </summary>
/// <param name="str" type="string">Conteúdo do XML.</param>
/// <returns type="DOM XML">Objeto DOM XML contendo o xml passado como parâmetro.</returns>
function xmlDocFromString(str) {

    var xmlDoc;

    if (window.DOMParser) {
        parser = new DOMParser();
        xmlDoc = parser.parseFromString(str, "text/xml");
    }
    else {
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async = "false";
        xmlDoc.loadXML(str);
    }
    return xmlDoc;

}
