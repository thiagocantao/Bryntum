<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NovoMapaEstrategico.aspx.cs"
    Inherits="_Estrategias_mapa_NovoMapaEstrategico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" src="../../scripts/jquery.ultima.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.timer.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var resolution = ObtemResolucao();
            var w = resolution.width - 25;
            var h = resolution.height - 35;
            carregaMapa(w, h);
        });

        function carregaMapa(w, h) {
            var codigoMapa = hfGeral.Get("cm");
            if (codigoMapa != null) {
                var caminhoFisicoArquivosTemp = hfGeral.Get("caminhoFisicoArquivosTemp");

                var parameters = (new Array(codigoMapa, caminhoFisicoArquivosTemp, w, h)).join(';');
                callback.PerformCallback(parameters);
                //Eventos .divFatorChave
                var onClickDivFatorChave = function () {
                    var cod = $(this).attr('id').substring(2);
                    var perm = $(this).attr('perm');

                    if (perm == "1")
                        window.top.location = "../objetivoEstrategico/indexResumoObjetivo.aspx?cm=" + codigoMapa + "&coe=" + cod;
                };
                var onMouseEnterDivFatorChave = function () {
                    $(this).addClass('divSelecionada');
                };
                var onMouseLeaveDivFatorChave = function () {
                    $(this).removeClass('divSelecionada');
                };
                $(document).on('click', '.divFatorChave', onClickDivFatorChave);
                $(document).on('mouseenter', '.divFatorChave', onMouseEnterDivFatorChave);
                $(document).on('mouseleave', '.divFatorChave', onMouseLeaveDivFatorChave);
                //Eventos .divIndicar
                var onClickDivIndicador = onClickDivFatorChave; /*function () {
                    var cod = $(this).attr('id').substring(2);
                    window.location = "FatorChave.aspx?tabIndex=0&cod=" + cod;
                };*/
                var onMouseEnterDivIndicador = function () {
                    $(document).off('click', '.divFatorChave');
                    $(this).addClass('divSelecionada');
                };
                var onMouseLeaveDivIndicador = function () {
                    $(document).on('click', '.divFatorChave', onClickDivFatorChave);
                    $(this).removeClass('divSelecionada');
                };
                $(document).on('click', '.divFatorChave div', onClickDivIndicador);
                $(document).on('mouseenter', '.divFatorChave div', onMouseEnterDivIndicador);
                $(document).on('mouseleave', '.divFatorChave div', onMouseLeaveDivIndicador);
            }
        }

        function OnCallbackError(s, e) {
            var msgErro = "<div id='divErro'>";
            msgErro += "Não foi possível carregar o mapa<br />";
            msgErro += e.message;
            msgErro += "</div>";
            $("#divContainer").append($(msgErro));

            e.handled = true;
        }

        function OnCallbackComplete(s, e) {
            if (e.parameter == "exportaGestaoEstrategica" || e.parameter == "exportaProjetosEstrategicos") {
                window.location = "../../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=pdf&bInline=False";
            }
            else {
                var result = eval("(" + e.result + ")");
                var caminhoImagemMapa =
                    "url('../../ArquivosTemporarios/" + result.NomeImagemMapa + "')";
                var $divConteudo = $('#divConteudo');

                $divConteudo.width(result.LarguraImagem);
                $divConteudo.height(result.AlturaImagem);
                $divConteudo.css('background-repeat', 'no-repeat');
                $divConteudo.css('background-image', caminhoImagemMapa);
                $divConteudo.css('background-size', '100%');
                $divConteudo.html(result.InnerHtml);
            }
        }

        function OnImageZoomClick(s, e) {
            var $div = $('#dvBarraTopo');
            var urlImage = s.GetImageUrl();
            $div.toggle("blind");
            if (urlImage.indexOf('zoom_in.png') == -1)
                s.SetImageUrl('../../imagens/zoom_in.png');
            else
                s.SetImageUrl('../../imagens/zoom_out.png');
        }

        function ObtemResolucao() {
            var e = window, a = 'inner';
            if (!('innerWidth' in window)) {
                a = 'client';
                e = document.documentElement || document.body;
            }
            return { width: e[a + 'Width'], height: e[a + 'Height'] }
        }

    </script>
    <style type="text/css">
        .divFatorChave {
            display: inline-block;
            position: absolute;
        }

            .divFatorChave > div {
                width: 10px;
                height: 10px;
                top: 3px;
                right: 3px;
                position: absolute;
                border: 1px solid #000000;
                border-radius: 100%;
            }

        div.divSelecionada {
            cursor: pointer; /*opacity: 0.4;
            filter: alpha(opacity=40);  For IE8 and earlier */
        }

        div#divConteudo {
            /*background-image: url('../../imagens/CNI_MAPA2013.png');*/
            background-repeat: no-repeat;
            position: relative;
            display: block;
            top: 10px;
        }

        div#divContainer {
        }

        .divIndicadorBranco {
            background-color: White;
        }

        .divIndicadorVerde {
            background-color: Green;
        }

        .divIndicadorVermelho {
            background-color: Red;
        }

        .divIndicadorAzul {
            background-color: Blue;
        }

        .divIndicadorAmarelo {
            background-color: Yellow;
        }

        .divIndicadorLaranja {
            background-color: Orange;
        }

        .style3 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
                <tr>
                    <td align="left" style="background-image: url('../../imagens/titulo/back_Titulo_Desktop.gif'); height: 26px">
                        <table>
                            <tr>

                                <td align="left" style="height: 19px; width: 350px;" valign="middle">
                                    <table>
                                        <tr>
                                            <td style="padding-right: 10px">
                                                <dxcp:ASPxImage ID="ASPxImage2" runat="server" Cursor="pointer"
                                                    ImageUrl="~/imagens/ZoomOut.png" ShowLoadingImage="True" ToolTip="Menos Zoom">
                                                    <ClientSideEvents Click="function(s, e) {
	       var $divConteudo = $('#divConteudo');

                if($divConteudo.height() &gt; 250)
                {
                    var w = $divConteudo.width() - 50;
                    var h = $divConteudo.height() - 50;

                    carregaMapa(w, h);
                }
     

}" />
                                                </dxcp:ASPxImage>
                                            </td>
                                            <td>
                                                <dxcp:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer"
                                                    ImageUrl="~/imagens/ZoomIn.png" ShowLoadingImage="True"
                                                    ToolTip="Mais Zoom">
                                                    <ClientSideEvents Click="function(s, e) {
	var $divConteudo = $('#divConteudo');
                var w = $divConteudo.width() + 50;
                    var h = $divConteudo.height() + 50;

                    carregaMapa(w, h);

}" />
                                                </dxcp:ASPxImage>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div id="divConteudo" style="margin-left: auto; margin-right: auto; display: block; margin-bottom: 50px;">
            </div>
            <table>
                <tr>
                    <td class="<%=estiloFooter %>" style="padding: 3px">
                        <table cellspacing="0" cellpadding="0" class="grid-legendas" __designer:mapid="4319">
                            <tbody __designer:mapid="4400">
                                <tr __designer:mapid="4401">
                                    <td __designer:mapid="4402">
                                        <img alt="" src="../../imagens/azulMenor.gif" __designer:mapid="4403" height="16" />
                                    </td>
                                    <td style="padding-left: 3px; padding-right: 10px;" __designer:mapid="4404">
                                        <span __designer:mapid="4405">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_meta_superada %>"
                                                ID="Label2" EnableViewState="False"></asp:Label>
                                        </span>
                                    </td>
                                    <td __designer:mapid="4402">
                                        <img alt="" src="../../imagens/verdeMenor.gif" __designer:mapid="4403" height="16" />
                                    </td>
                                    <td style="padding-left: 3px; padding-right: 10px;" __designer:mapid="4404">
                                        <span __designer:mapid="4405">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_satisfat_rio %>"
                                                ID="Label10" EnableViewState="False"></asp:Label>
                                        </span>
                                    </td>
                                    <td __designer:mapid="4407">
                                        <img alt="" src="../../imagens/amareloMenor.gif" __designer:mapid="4408" height="16" />
                                    </td>
                                    <td style="padding-left: 3px; padding-right: 10px;" __designer:mapid="4409">
                                        <span __designer:mapid="440a">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_aten__o %>"
                                                ID="Label11" EnableViewState="False"></asp:Label>
                                        </span>
                                    </td>
                                    <td __designer:mapid="440c">
                                        <img alt="" src="../../imagens/vermelhoMenor.gif" __designer:mapid="440d" height="16" />
                                    </td>
                                    <td style="padding-left: 3px; padding-right: 10px;" __designer:mapid="440e">
                                        <span style="" __designer:mapid="440f">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_cr_tico %>"
                                                ID="Label12" EnableViewState="False"></asp:Label>
                                        </span>
                                    </td>
                                    <td __designer:mapid="4411">
                                        <img style="margin-top: 0px" alt="" src="../../imagens/BrancoMenor.gif" __designer:mapid="4412"
                                            height="16" />
                                    </td>
                                    <td style="padding-left: 3px; padding-right: 10px;" __designer:mapid="4413">
                                        <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_sem_informa__o %>"
                                            ID="Label13" EnableViewState="False"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>

            </table>
            <dxhf:ASPxHiddenField ID="hfGeral" ClientInstanceName="hfGeral" runat="server">
            </dxhf:ASPxHiddenField>
            <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                <ClientSideEvents CallbackComplete="OnCallbackComplete" CallbackError="OnCallbackError" />
            </dxcb:ASPxCallback>
            <asp:SqlDataSource ID="sdsDataSource" runat="server" SelectCommand=" SELECT me.CodigoMapaEstrategico, 
        me.TituloMapaEstrategico 
   FROM MapaEstrategico AS me INNER JOIN 
        UnidadeNegocio AS un ON un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio 
  WHERE (me.IndicaMapaEstrategicoAtivo = 'S') 
    AND (un.CodigoEntidade = @CodigoEntidade)
    AND (me.ImagemMapa IS NOT NULL)
  ORDER BY 
        me.TituloMapaEstrategico">
                <SelectParameters>
                    <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
