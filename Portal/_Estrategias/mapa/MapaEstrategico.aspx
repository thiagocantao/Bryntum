<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapaEstrategico.aspx.cs"
    Inherits="_Estrategias_mapa_MapaEstrategico" MasterPageFile="~/novaCdis.master" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript" src="../../scripts/jquery.ultima.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery.timer.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //Se houver um mapa selecionado...
            if (cmbMapas.GetSelectedIndex() > -1) {
                var codigoMapa = cmbMapas.GetValue();
                var caminhoFisicoArquivosTemp = hfGeral.Get("caminhoFisicoArquivosTemp");
                var resolution = ObtemResolucao();
                var w = resolution.width - 25;
                var h = resolution.height - 35;
                var parameters = (new Array(codigoMapa, caminhoFisicoArquivosTemp, w, h)).join(';');
                callback.PerformCallback(parameters);
                //Eventos .divFatorChave
                var onClickDivFatorChave = function () {
                    var cod = $(this).attr('id').substring(2);
                    window.location = "FatorChave.aspx?tabIndex=1&cod=" + cod;
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
                var onClickDivIndicador = function () {
                    var cod = $(this).attr('id').substring(2);
                    window.location = "FatorChave.aspx?tabIndex=0&cod=" + cod;
                };
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
        });

        function OnCallbackError(s, e) {
            var msgErro = "<div id='divErro'>";
            msgErro += "Não foi possível carregar o mapa <span style='font-weight:bold'>'" + cmbMapas.GetText() + "'.</span> <br />";
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
                $divConteudo.append(result.InnerHtml);
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
                left: 3px;
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
            position: absolute;
            display: block;
            top: 30px;
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

        .style2 {
            height: 19px;
            width: 223px;
        }
    </style>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="background-image: url('../../imagens/titulo/back_Titulo_Desktop.gif'); height: 26px">
                <table>
                    <tr>
                        <td align="left" style="padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" style="height: 19px; width: 350px;" valign="middle">
                            <div id="divContainer">
                                <div id="divOpcoes" style="display: block;">
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Mapa: ">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox ID="cmbMapas" ClientInstanceName="cmbMapas" runat="server" ValueType="System.Int32"
                                                    AutoPostBack="True" DataSourceID="sdsDataSource" IncrementalFilteringMode="Contains"
                                                    TextField="TituloMapaEstrategico" ValueField="CodigoMapaEstrategico" Width="250px"
                                                    OnDataBound="cmbMapas_DataBound">
                                                </dxe:ASPxComboBox>

                                            </td>
                                            <td>
                                                <dxe:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer"
                                                    ToolTip="Relatório de Gestão Estratégica" Height="20px"
                                                    ImageUrl="~/imagens/mapaEstrategico/proj_estrategicos1.png" Width="20px">
                                                    <ClientSideEvents Click="function(s, e) {
	callback.PerformCallback(&quot;exportaGestaoEstrategica&quot;);
}" />
                                                </dxe:ASPxImage>
                                            </td>
                                            <td style="margin-left: 40px">
                                                <dxe:ASPxImage ID="ASPxImage2" runat="server" Cursor="pointer"
                                                    ToolTip="Relatório de Projetos Estratégicos"
                                                    ImageUrl="~/imagens/mapaEstrategico/relProjetosEstrategicos.png">
                                                    <ClientSideEvents Click="function(s, e) {
	callback.PerformCallback(&quot;exportaProjetosEstrategicos&quot;);
}" />
                                                </dxe:ASPxImage>
                                            </td>
                                            <td style="margin-left: 40px">
                                                <dxe:ASPxImage ID="ASPxImage3" runat="server" Cursor="pointer"
                                                    ToolTip="Projetos do Mapa Estratégico"
                                                    ImageUrl="~/imagens/mapaEstrategico/projeto.PNG">
                                                    <ClientSideEvents Click="function(s, e) {
	var codigoMapa = cmbMapas.GetValue() == null ? -1 : cmbMapas.GetValue();
	window.open('./ListaProjetosMapa.aspx?CM=' + codigoMapa, '_self');
}" />
                                                </dxe:ASPxImage>
                                            </td>
                                            <td>
                                                <dxe:ASPxImage ID="imgZoom" runat="server" ClientInstanceName="imgZoom" Height="20px"
                                                    ImageUrl="~/imagens/zoom_in.png" Width="20px" Cursor="pointer">
                                                    <ClientSideEvents Click="OnImageZoomClick" />
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divConteudo">
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
</asp:Content>
