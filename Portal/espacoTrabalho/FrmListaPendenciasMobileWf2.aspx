<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/novaCdis.master" CodeFile="FrmListaPendenciasMobileWf2.aspx.cs" Inherits="espacoTrabalho_FrmListaPendenciasMobileWf2" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="Server">
    <!--
    <link rel="stylesheet" type="text/css" href="../Content/Paper/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../Content/FontAwesome/font-awesome.min.css" />
    <script type="text/javascript" src="../scripts/bootstrap.min.js"></script>
     <script type="text/javascript" src="../scripts/xpull.js"></script>
    <script type="text/javascript" src="../scripts/jquery-3.1.1.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Content/mobile/frmListaPendenciasMobile.css" />
    <link rel="stylesheet" type="text/css" href="../Content/highlightjs.default.css" />
    <link rel="stylesheet" type="text/css" href="../Content/site.css" />
    <link rel="stylesheet" type="text/css" href="../Content/Paper/site.css"/>
    <link rel="stylesheet" type="text/css" href="../Content/mobile/xpull.css" />
    -->
    <title></title>
     
    <script type="text/javascript">
        // jquery document ready
        $(function () {

            // Init xpull plugin for demo
            $('#divXpull').xpull({
                pullThreshold: 70,
                maxPullThreshold: 70,
                'callback': function () {
                    gvDados.PerformCallback("");
                }
            });

        });
        function displayMessage(evt) {//recibe el evento desde el padre
            //veryfica que accion tomar
            //en este caso le estoy pidiendo el tamaño 
            //del documento para adaptar mi iframe
            if (evt.data.type == 'origin') {
                var data = {
                    type: "FullScreen",//para identificar el evento en la aplicacion
                    value: false //tamaño del documento
                };
                localStorage.setItem("originMsg", evt.origin);
                //se envia el evento a la aplicacion
                parent.window.postMessage(data, evt.origin);
            }

            
        }

        //escucha el evento
        if (window.addEventListener) {
            window.removeEventListener("message", displayMessage, false);
            window.addEventListener("message", displayMessage, false);
        }
        else {
            window.attachEvent("onmessage", displayMessage);
        }


        var doProcessClick;
        var visibleIndex;
        function ProcessClick() {
            if (doProcessClick) {
                alert("Here is the CardClick action in the " + visibleIndex.toString() + "-th card");
            }
        }
    </script>
    <script type="text/javascript">

        function UpdateGridHeight(){
            sampleGrid.SetHeight(0);
            var containerHeight = ASPxClientUtils.GetDocumentClientHeight();
            if(document.body.scrollHeight > containerHeight)
                containerHeight = document.body.scrollHeight;
            sampleGrid.SetHeight(containerHeight);
        }


        var refreshinterval = 540;
        var starttime;
        var nowtime;
        var reloadseconds = 0;
        var secondssinceloaded = 0;



        function starttime() {
            starttime = new Date();
            starttime = starttime.getTime();
            countdown();
        }

        function countdown() {
            nowtime = new Date();
            nowtime = nowtime.getTime();
            secondssinceloaded = (nowtime - starttime) / 1000;
            reloadseconds = Math.round(refreshinterval - secondssinceloaded);
            if (refreshinterval >= secondssinceloaded) {
                var timer = setTimeout("countdown()", 10);

            }
            else {
                clearTimeout(timer);
                callbackSession.PerformCallback();
            }
        }
        window.onload = starttime;



        function gotoURL(url, target) {
            if (pcModal.cp_Path != null && pcModal.cp_Path != "" && pcModal.cp_Path != 'undefined') {
                var fakeLink = document.createElement("a");

                fakeLink.target = target;

                if (typeof (fakeLink.click) == 'undefined')
                    location.href = pcModal.cp_Path + url; // sends referrer in FF, not in IE 
                else {
                    fakeLink.href = pcModal.cp_Path + url;
                    document.body.appendChild(fakeLink);
                    fakeLink.click(); // click() method defined in IE only 
                }
            }

        }


        function atualizaTela() {
            window.location.reload();
        }

        function mostraSobre(caminho) {
            showModal(caminho, "Sobre", 532, 330, "", null);
        }
    </script>

    <style>

        #rastroPrincipal {
            display: none;
        }

        .dxflGroup_MaterialCompact > tbody > tr:first-child {
            
            color : black;
            font-weight: normal;
            text-decoration : none;
        }

        .dxflGroup_MaterialCompact > tbody > tr:first-child > td:last-child {
            color : black !important;
            font-weight: bold;
        }

                .dxflGroup_MaterialCompact > tbody > tr:first-child > td:last-child:hover {
            color : black !important;
            font-weight: bold;
            text-decoration : underline;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
<body style="margin: 0px">
    <div id="form1" runat="server">

        <div id="divXpull" class="mi-class">
            <div class="xpull">
                <div class="xpull__start-msg">
                    <div class="xpull__start-msg-text" style="width: 100%; text-align: center;">Arraste para Baixo e Solte para Recarregar a Lista</div>
                    <div class="xpull__arrow"></div>
                </div>
            </div>
            <dx:ASPxCardView ID="gvDados" CssClass="MY-CLASSS" Width="100%" runat="server" ClientInstanceName="gvDados"  
                     KeyFieldName="CodigoWorkflow;CodigoInstanciaWf;CodigoFluxo;OcorrenciaAtual;CodigoEtapaAtual" >
                <ClientSideEvents 
                    CardClick="function(s, e) {
                        var chaves = s.GetCardKey(e.visibleIndex).split('|');
                        var codigoWorkflow = chaves[0];
                        var codigoInstanciaWf = chaves[1];
                        var codigoFluxo = chaves[2];
                        var ocorrencia = chaves[3];
                        var codigoEtapa = chaves[4];
                        var url = '';
                        var titulo = '';
                        var largura = screen.width - 10;

                        url = '../wfEngineInternoMobile2.aspx?CW=' + codigoWorkflow + '&amp;CI=' + codigoInstanciaWf + '&amp;CE=' + codigoEtapa + '&amp;CS=' + ocorrencia + '&amp;Largura=' + largura;
                        titulo = 'Etapa';

                        window.location.href = url;
                    }" />
                <SettingsPager NumericButtonCount="1" Mode="EndlessPaging" EndlessPagingMode="OnScroll">
                        <SettingsTableLayout ColumnCount="1" RowsPerPage="15" />
                </SettingsPager>
                <Columns>
                    <dx:CardViewColumn BatchEditModifiedCellStyle-CssClass="alert-danger" FieldName="NomeProjeto" Caption="Descrição" />                    
                    <dx:CardViewColumn FieldName="NomeEtapaAtual" Caption="Etapa Atual" />
                    <dx:CardViewColumn FieldName="NomeUnidadeNegocio" Caption="Unidade de Negócio" />
                    <dx:CardViewColumn FieldName="NomeFluxo" Caption="Tipo de Fluxo" Name="col_NomeFluxo" />
                    <dx:CardViewColumn FieldName="UsuarioCriacaoInstancia" Caption="Solicitante" />
                </Columns>
                <SettingsSearchPanel Visible="true" />
            </dx:ASPxCardView>
        </div>
</div>
</body>
</asp:Content>
