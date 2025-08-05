<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfiguracaoMapaEstrategico.aspx.cs"
    Inherits="_Estrategias_mapa_ConfiguracaoMapaEstrategico" MasterPageFile="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript" src="../../scripts/jquery.ultima.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            callback.PerformCallback('carregamapa');
            $(document).on('click', '.divFatorChave', SelecionaFatorChave);
            $(document).on('dblclick', '.divFatorChave', DefineTituloFatorChave);
        });

        function SelecionaFatorChave() {
            $('.divFatorChave').removeClass('selecionado');
            $(this).toggleClass('selecionado');
            //menuMapa.GetItemByName('btnExclusaoFatorChave').SetEnabled(true);
        }

        function DefineTituloFatorChave() {
            var $divFatorChave = $(this);
            popup.ShowWindow(popup.GetWindowByName('winSelecionarFatorChave'));
            var titulo = $divFatorChave.find('.spanTitulo').html();
            txtTituloFatorChave.SetText(titulo);
        }

        function onClickBtnSalvarTituloFatorChave(s, e) {
            var titulo = txtTituloFatorChave.GetText();
            var acao = 'definetitulofc';
            var id = $('.selecionado').attr('id');
            var parameters = (new Array(acao, id, titulo)).join(';');
            callback.PerformCallback(parameters);
        }

        function OnCallbackComplete(s, e) {
            var parameter = e.parameter.split(';')[0];
            if (parameter == 'carregamapa') {
                if (e.result != "") {
                    var result = eval("(" + e.result + ")");
                    var $divConteudo = $("#divConteudo");
                    $divConteudo.height(result.AlturaImagem);
                    $divConteudo.width(result.LarguraImagem);
                    $divConteudo.css("background-image", "url('../../ArquivosTemporarios/" + result.NomeImagemMapa + "')");
                    $divConteudo.append($(result.InnerHtml));
                    $('.divFatorChave')
                        .draggable({
                            stop: function (e, ui) {
                                SalvaAlteracoesFatorChave($(this), 'reposiciona');
                            }
                        })
                        .resizable({
                            stop: function (e, ui) {
                                SalvaAlteracoesFatorChave($(this), 'redimensiona');
                            }
                        });
                }
            }
            else if (parameter == 'exclui') {
                var id = '#fc' + e.result;
                $(id).remove();
            }
            else if (parameter == 'definenovofc') {
                var id = 'fc' + e.result;
                $("#divTemp").attr('id', id);
                $("#" + id)
                    .draggable({
                        stop: function (e, ui) {
                            SalvaAlteracoesFatorChave($(this), 'reposiciona');
                        }
                    })
                    .resizable({
                        stop: function (e, ui) {
                            SalvaAlteracoesFatorChave($(this), 'redimensiona');
                        }
                    });
            }
            else if (parameter == 'definetitulofc') {
                $('.divFatorChave').find('.spanTitulo').html(e.result);
                popup.HideWindow(popup.GetWindowByName('winSelecionarFatorChave'));
            }
        }

        function SalvaAlteracoesFatorChave($divFatorChave, acao) {
            //var acao = 'reposiciona';
            var id = $divFatorChave.attr('id');
            var width = $divFatorChave.width();
            var height = $divFatorChave.height();
            var left = $divFatorChave.cssUnit('left')[0];
            var top = $divFatorChave.cssUnit('top')[0];
            var parameters = (new Array(acao, id, width, height, left, top)).join(';');
            callback.PerformCallback(parameters);
        }

        function OnFileUploadComplete(s, e) {
            var urlImagem = e.callbackData;
            //menuMapa.GetItemByName('btnNovoFatorChave').SetEnabled(true);
            popup.HideWindow(popup.GetWindowByName('winUploadImagemMapa'));
            var img = new Image();
            img.onload = function () {
                $divConteudo = $('#divConteudo');
                $divConteudo.css("background-image", "url('" + e.callbackData + "')");
                $divConteudo.width(this.width);
                $divConteudo.height(this.height);
            }
            img.src = urlImagem;
        }

        function OnItemClick(s, e) {
            var itemName = e.item.name;
            if (itemName == 'btnSelecaoMapa') {
                popup.ShowWindow(popup.GetWindowByName('winUploadImagemMapa'));
            }
            else if (itemName == 'btnNovoFatorChave') {
                menuMapa.GetItemByName('btnSelecaoMapa').SetEnabled(false);
                menuMapa.GetItemByName('btnNovoFatorChave').SetEnabled(false);
                menuMapa.GetItemByName('btnExclusaoFatorChave').SetEnabled(false);
                AcionaEventos(true);
            }
            else if (itemName == 'btnExclusaoFatorChave') {
                var $selecionado = $('.selecionado');
                if ($selecionado.length == 1) {
                    var id = $selecionado.attr('id');
                    var tituloFatorChave = $('.selecionado>.spanTitulo').html();
                    var parameters = (new Array('reposiciona', id)).join(';');

                    var funcObj = {
                        funcaoClickOK: function (id) {
                            var parameters = (new Array('exclui', id)).join(';');
                            callback.PerformCallback(parameters);
                        }
                    }

                    window.top.mostraConfirmacao("Deseja excluir o fator chave '" + tituloFatorChave + "'?", function () { funcObj['funcaoClickOK'](id) }, null);
                }
            }
        }

        function AcionaEventos(eventoAtivos) {
            if (eventoAtivos) {
                $(document).on('click', '#divConteudo', onClickDivConteudo_NovoFatorChave);
                $(document).on('mouseenter', '#divConteudo', onMouseEnterDivConteudo_NovoFatorChave);
                $(document).on('mouseleave', '#divConteudo', onMouseLeaveDivConteudo_NovoFatorChave);
                $(document).on('mousemove', '#divConteudo', onMouseMoveDivConteudo_NovoFatorChave);
            }
            else {
                $(document).off('click', '#divConteudo', onClickDivConteudo_NovoFatorChave);
                $(document).off('mouseenter', '#divConteudo', onMouseEnterDivConteudo_NovoFatorChave);
                $(document).off('mouseleave', '#divConteudo', onMouseLeaveDivConteudo_NovoFatorChave);
                $(document).off('mousemove', '#divConteudo', onMouseMoveDivConteudo_NovoFatorChave);
            }
        }

        function DefinePosicaoDivTemp(x, y) {
            var $dvBarraTopo = $('#dvBarraTopo');
            if ($dvBarraTopo.css('display') != 'none') {
                y -= $dvBarraTopo.height();
            }        
            var $div = $("#divTemp");
            $div.css('left', x);
            $div.css('top', y);
        }

        function onClickDivConteudo_NovoFatorChave(e) {
            menuMapa.GetItemByName('btnSelecaoMapa').SetEnabled(true);
            menuMapa.GetItemByName('btnNovoFatorChave').SetEnabled(true);
            menuMapa.GetItemByName('btnExclusaoFatorChave').SetEnabled(true);
            AcionaEventos(false);
            var x = e.pageX - this.offsetLeft;
            var y = e.pageY - this.offsetTop;
            DefinePosicaoDivTemp(x, y);
            SalvaAlteracoesFatorChave($("#divTemp"), 'definenovofc');
        }

        function onMouseEnterDivConteudo_NovoFatorChave(e) {
            var $div = $("<div id='divTemp' class='divFatorChave'/>");
            var $spanTitulo = $("<span class='spanTitulo'>Fator Chave</span>");
            $div.append($spanTitulo);
            $('#divConteudo').append($div);
        }

        function onMouseMoveDivConteudo_NovoFatorChave(e) {
            var x = e.pageX - this.offsetLeft;
            var y = e.pageY - this.offsetTop;
            DefinePosicaoDivTemp(x, y);
        }

        function onMouseLeaveDivConteudo_NovoFatorChave(e) {
            $("#divTemp").remove();
        }

    </script>
    <link href="../../estilos/jquery.ui.ultima.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        div#divConteudo
        {
            border: 2px solid black;
            background-repeat: no-repeat;
            position: absolute;
        }
        div.divFatorChave
        {
            display: inline-block;
            position: absolute;
            border: 2px dashed blue;
            background-color: White;
            height: 50px;
            width: 150px;
            opacity: 0.4;
            filter: alpha(opacity=40); /*For IE8 and earlier*/
            cursor: pointer;
        }
        span.spanTitulo
        {
            color: White;
            
            font-size: 6pt;
            background-color: Black;
            margin: 2px;
            padding: 2px;
            position: absolute;
            top: 3px;
            left: 3px;
            border-radius: 5px;
        }
        
        .selecionado
        {
            -webkit-box-shadow: 0 0 15px red;
            -moz-box-shadow: 0 0 15px red;
            box-shadow: 0 0 15px red;
        }
    </style>
    <div id="divContainer">
        <dxm:ASPxMenu ID="menuMapa" runat="server" ClientInstanceName="menuMapa">
            <ClientSideEvents ItemClick="OnItemClick" />
            <Items>
                <dxm:MenuItem Text="<%$ Resources:traducao, ConfiguracaoMapaEstrategico_selecionar_imagem_do_mapa %>" Name="btnSelecaoMapa">
                </dxm:MenuItem>
                <dxm:MenuItem BeginGroup="True" Text="<%$ Resources:traducao, ConfiguracaoMapaEstrategico_novo %>" Name="btnNovoFatorChave">
                    <Image Height="20px" Url="~/imagens/botoes/incluirReg02.png" Width="20px">
                    </Image>
                </dxm:MenuItem>
                <dxm:MenuItem Text="<%$ Resources:traducao, ConfiguracaoMapaEstrategico_excluir %>" Name="btnExclusaoFatorChave">
                    <Image Height="20px" Url="~/imagens/botoes/excluirReg02.PNG" UrlDisabled="~/imagens/botoes/excluirRegDes.PNG"
                        Width="20px">
                    </Image>
                </dxm:MenuItem>
            </Items>
        </dxm:ASPxMenu>
        <div id="divConteudo">
        </div>
    </div>
    <dxpc:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="400px">
        <Windows>
            <dxpc:PopupWindow CloseAction="CloseButton" HeaderText="<%$ Resources:traducao, ConfiguracaoMapaEstrategico_selecionar_imagem_do_mapa %>"
                Modal="True" Name="winUploadImagemMapa">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxuc:ASPxUploadControl ID="upload" runat="server" ClientInstanceName="upload" ShowUploadButton="True"
                            UploadMode="Advanced" Width="100%" OnFileUploadComplete="upload_FileUploadComplete"
                            ShowProgressPanel="True">
                            <ValidationSettings AllowedFileExtensions=".jpg">
                            </ValidationSettings>
                            <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                            <BrowseButton Text="<%$ Resources:traducao, ConfiguracaoMapaEstrategico_selecionar %>">
                            </BrowseButton>
                            <AdvancedModeSettings TemporaryFolder="~\ArquivosTemporarios\" />
                        </dxuc:ASPxUploadControl>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:PopupWindow>
            <dxpc:PopupWindow CloseAction="None" HeaderText="Fator Chave" Modal="True" Name="winSelecionarFatorChave"
                ShowCloseButton="True" ShowHeader="False" Width="500px">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <table>
                            <tr>
                                <td>
                                    <span style="font-weight: bold;">Título:</span>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtTituloFatorChave" ClientInstanceName="txtTituloFatorChave"
                                        runat="server" Width="375px">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td style="padding-right: 5px;">
                                    <dxe:ASPxImage ID="imgSalvarTituloFatorChave" Width="20px" Height="20px" runat="server"
                                        ImageUrl="~/imagens/botoes/salvar.PNG" Cursor="pointer">
                                        <ClientSideEvents Click="onClickBtnSalvarTituloFatorChave" />
                                    </dxe:ASPxImage>
                                </td>
                                <td>
                                    <dxe:ASPxImage ID="imgCancelarTituloFatorChave" Width="20px" Height="20px" runat="server"
                                        ImageUrl="~/imagens/botoes/cancelar.PNG" Cursor="pointer">
                                        <ClientSideEvents Click="function(s, e) {
	popup.HideWindow(popup.GetWindowByName('winSelecionarFatorChave'));
}" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:PopupWindow>
        </Windows>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="OnCallbackComplete" />
    </dxcb:ASPxCallback>
</asp:Content>
