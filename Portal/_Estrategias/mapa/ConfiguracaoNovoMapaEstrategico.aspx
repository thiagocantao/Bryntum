<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfiguracaoNovoMapaEstrategico.aspx.cs"
    Inherits="_Estrategias_mapa_ConfiguracaoNovoMapaEstrategico" MasterPageFile="~/novaCdis.master" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    
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
            var titulo = $divFatorChave.find('.spanTitulo').html();
            popup.ShowWindow(popup.GetWindowByName('winSelecionarFatorChave'));
            txtTituloFatorChave.SetText(titulo);
            ddlTema.SetEnabled(true);
            ddlTema.SetValue($divFatorChave.attr('cp_CodigoSuperior'));
        }

        function onClickBtnSalvarTituloFatorChave(s, e) {
            var indice = ddlTema.GetSelectedIndex();
            var titulo = txtTituloFatorChave.GetText();
            if (indice == -1 || titulo == '') {
                window.top.mostraMensagem(traducao.ConfiguracaoNovoMapaEstrategico_informe_o_t_tulo_do_objetivo_e_a_quem_ele_est__associado_, 'atencao', true, false, null);
            }
            else {
                if (ddlTema.GetEnabled()) {
                    popup.HideWindow(popup.GetWindowByName('winSelecionarFatorChave'));
                    AcionaEventos(true);
                }
                else {
                    var acao = 'definetitulofc';
                    var id = $('.selecionado').attr('id');
                    var parameters = (new Array(acao, id, titulo)).join(';');
                    callback.PerformCallback(parameters);
                }
            }
        }

        function OnCallbackComplete(s, e) {

            if (s.cp_MostraLP == "S")
                lblSemImagem.SetVisible(true);
            else
                lblSemImagem.SetVisible(false);

            var parameter = e.parameter.split(';')[0];
            if (parameter == 'carregamapa') {
                if (e.result != "") {
                    var result = eval("(" + e.result + ")");
                    var $divConteudo = $("#divConteudo");
                    $divConteudo.height(s.cpAltura);
                    $divConteudo.width(result.LarguraImagem);
                    //$divConteudo.css("height", "100%");
                    $divConteudo.css("width", "100%");
                    //$divConteudo.css("background-image", "url('../../ArquivosTemporarios/" + result.NomeImagemMapa + "')");
                    $divConteudo.html("<img src=\"../../ArquivosTemporarios/" + result.NomeImagemMapa + "\" style='position: absolute; top: 0; left: 0;' />")
                    $divConteudo.css("overflow", "scroll");
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
                $("#" + id).attr('cp_CodigoSuperior', ddlTema.GetValue());
            }
            else if (parameter == 'definetitulofc') {
                var id = '#' + e.parameter.split(';')[1];
                $(id).find('.spanTitulo').html(e.result);
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
            var codigoSuperior = $divFatorChave.attr('cp_CodigoSuperior');
            var parameters = (new Array(acao, id, width, height, left, top, codigoSuperior)).join(';');
            callback.PerformCallback(parameters);
        }

        function OnFileUploadComplete(s, e) {
            var urlImagem = e.callbackData;
            //menuMapa.GetItemByName('btnNovoFatorChave').SetEnabled(true);
            popup.HideWindow(popup.GetWindowByName('winUploadImagemMapa'));
            var img = new Image();
            img.onload = function () {
                $divConteudo = $('#divConteudo');
                $divConteudo.css("height", "100%");
                $divConteudo.css("width", "100%");
                //$divConteudo.css("background-image", "url('" + e.callbackData + "')");
                $divConteudo.html("<img src=\"../../ArquivosTemporarios/" + result.NomeImagemMapa + "\" style='width: " + result.LarguraImagem + "px; height: " + result.AlturaImagem + "px; position: absolute; top: 0; left: 0;' />")
                $divConteudo.css("overflow", "scroll");
                $divConteudo.width(this.width);
                $divConteudo.height(this.height);
            }
            img.src = urlImagem;
            callback.PerformCallback('carregamapa');
        }

        function OnItemClick(s, e) {
            var itemName = e.item.name;
            if (itemName == 'btnSelecaoMapa') {
                popup.ShowWindow(popup.GetWindowByName('winUploadImagemMapa'));
            }
            else if (itemName == 'btnNovoFatorChave') {
                menuMapa.GetItemByName('btnSelecaoMapa').SetEnabled(true);
                menuMapa.GetItemByName('btnNovoFatorChave').SetEnabled(true);
                menuMapa.GetItemByName('btnExclusaoFatorChave').SetEnabled(true);
                popup.ShowWindow(popup.GetWindowByName('winSelecionarFatorChave'));
                txtTituloFatorChave.SetText(traducao.ConfiguracaoNovoMapaEstrategico_objetivo_estrat_gico);
                ddlTema.SetSelectedIndex(-1);
                ddlTema.SetEnabled(true);
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
                    window.top.mostraConfirmacao(traducao.ConfiguracaoNovoMapaEstrategico_deseja_excluir_o_objetivo_estrat_gico__ + tituloFatorChave + "'?", function () { funcObj['funcaoClickOK'](id) }, null);
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
            //            var $dvBarraTopo = $('#dvBarraTopo');
            //            if ($dvBarraTopo.css('display') != 'none') {
            //                y -= $dvBarraTopo.height();
            //            }
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
            $("#divTemp").attr('cp_CodigoSuperior', ddlTema.GetValue());
            SalvaAlteracoesFatorChave($("#divTemp"), 'definenovofc');
        }

        function onMouseEnterDivConteudo_NovoFatorChave(e) {
            var $div = $("<div id='divTemp' class='divFatorChave'/>");
            var $spanTitulo = $("<span class='spanTitulo'>" + txtTituloFatorChave.GetText() + "</span>");
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
        div#divContainer
        {
            height: 100vh;
        }
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
        <table cellpadding="0" cellspacing="0" class="headerGrid">
            <tr>
                <td>
        <dxm:ASPxMenu ID="menuMapa" runat="server" ClientInstanceName="menuMapa">
            <ClientSideEvents ItemClick="OnItemClick" />
            <Items>
                <dxm:MenuItem Text="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_selecionar_imagem_do_mapa %>" Name="btnSelecaoMapa">
                </dxm:MenuItem>
                <dxm:MenuItem BeginGroup="True" Text="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_novo %>" Name="btnNovoFatorChave">
                    <Image Height="20px" Url="~/imagens/botoes/incluirReg02.png" Width="20px">
                    </Image>
                </dxm:MenuItem>
                <dxm:MenuItem Text="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_excluir %>" Name="btnExclusaoFatorChave">
                    <Image Height="20px" Url="~/imagens/botoes/excluirReg02.PNG" UrlDisabled="~/imagens/botoes/excluirRegDes.PNG"
                        Width="20px">
                    </Image>
                </dxm:MenuItem>
            </Items>
        </dxm:ASPxMenu>
                </td>
                <td>
        <dxcp:ASPxLabel ID="lblSemImagem" runat="server" ClientInstanceName="lblSemImagem" ClientVisible="False"></dxcp:ASPxLabel>
                </td>
            </tr>
        </table>
        <div id="divConteudo" style="width: 100%; height: 100%; overflow: scroll;">
        </div>
    </div>
    <dxpc:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="400px">
        <Windows>
            <dxpc:PopupWindow CloseAction="None" HeaderText="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_objetivo_estrat_gico %>" Modal="True"
                Name="winSelecionarFatorChave" ShowCloseButton="True" ShowHeader="False" Width="500px">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <table>
                            <tr>
                                <td>
                                    <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_associar_objetivo_a_ %>" Font-Bold="True">
                                    </dxcp:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxComboBox ID="ddlTema" runat="server" ClientInstanceName="ddlTema" 
                                        Width="500px" ValueType="System.Int32">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_t_tulo_ %>" Font-Bold="True" 
                                        Font-Size="8">
                                    </dxcp:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxTextBox ID="txtTituloFatorChave" runat="server" ClientInstanceName="txtTituloFatorChave"
                                        Width="100%">
                                    </dxtv:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="margin-left: auto">
                                        <tr>
                                            <td>
                                                <dxtv:ASPxImage ID="imgSalvarTituloFatorChave" runat="server" Cursor="pointer" Height="20px"
                                                    ImageUrl="~/imagens/botoes/salvar.PNG" Width="20px" ToolTip="Salvar">
                                                    <ClientSideEvents Click="onClickBtnSalvarTituloFatorChave" />
                                                </dxtv:ASPxImage>
                                            </td>
                                            <td style="padding-left: 10px">
                                                <dxtv:ASPxImage ID="imgCancelarTituloFatorChave0" runat="server" Cursor="pointer"
                                                    Height="20px" ImageUrl="~/imagens/botoes/cancelar.PNG" Width="20px" ToolTip="Fechar">
                                                    <ClientSideEvents Click="function(s, e) {
	popup.HideWindow(popup.GetWindowByName('winSelecionarFatorChave'));
}" />
                                                </dxtv:ASPxImage>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:PopupWindow>
            <dxpc:PopupWindow CloseAction="CloseButton" HeaderText="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_selecionar_imagem_do_mapa %>"
                Modal="True" Name="winUploadImagemMapa">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxuc:ASPxUploadControl ID="upload" runat="server" ClientInstanceName="upload" ShowUploadButton="True"
                            UploadMode="Advanced" Width="100%" OnFileUploadComplete="upload_FileUploadComplete"
                            ShowProgressPanel="True" ToolTip="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_nenhum_arquivo_selecionado %>">
                            <ValidationSettings AllowedFileExtensions=".jpg">
                            </ValidationSettings>
                            <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
<%--                            <BrowseButton Text="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_selecionar %>">
                            </BrowseButton>--%>
                                <BrowseButton Text="<%$ Resources:traducao, ConfiguracaoNovoMapaEstrategico_selecionar %>">
                                </BrowseButton>
                            <AdvancedModeSettings TemporaryFolder="~\ArquivosTemporarios\">
                                <FileListItemStyle CssClass="pending dxucFileListItem">
                                </FileListItemStyle>
                            </AdvancedModeSettings>
                        </dxuc:ASPxUploadControl>
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
