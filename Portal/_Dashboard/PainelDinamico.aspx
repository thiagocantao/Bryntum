<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="PainelDinamico.aspx.cs" Inherits="_Dashboard_PainelDinamico" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../estilos/jquery.ui.ultima.css" rel="stylesheet" />
    <style type="text/css">
        #conteudo {
            height: 100%;
        }

        .itemContainer {
            display: inline-block;
            padding: 3px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

            .itemContainer > iframe {
                border: none;
            }
    </style>
    <script type="text/javascript" src="../scripts/jquery.ultima.js"></script>
    <script type="text/javascript" src="../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript">

        $(document).ready(readyFn);

        function readyFn(jQuery) {
            var objetoParametros = eval("(" + callbackPanel.cpParametros + ")");
            callbackPanel.SetVisible(false);
            defineAlturaPainel();
            var $divConteudo = $('#conteudo');
            var linhas = objetoParametros.Linhas;
            var colunas = objetoParametros.Colunas;
            var alturaTotal = $divConteudo.height();
            var larguraTotal = $divConteudo.width();
            for (var i = 0; i < linhas * colunas; i++) {
                var $div = $("<div></div>");
                $div.addClass('itemContainer');
                $div.css('height', (100 / linhas) + '%');
                $div.css('width', (100 / colunas) + '%');
                //if (i == 0) $div.resizable({ handles: 'e, w' });;
                var url = objetoParametros.Urls[i];
                var $iframe = $("<iframe id='frame_" + i + "'></iframe>");
                var largura = Math.floor($divConteudo.parent().width() / colunas)-10;
                var altura = Math.floor($divConteudo.parent().height() / linhas)-25;
                if (url.indexOf('?') == -1)
                    url += "?Altura=" + altura + "&Largura=" + largura;
                else
                    url += "&Altura=" + altura + "&Largura=" + largura;
                $iframe.attr('src', url);
                $iframe.css('height', '100%');
                $iframe.css('width', '100%');
                $iframe.appendTo($div);
                $divConteudo.append($div);
            }
            callbackPanel.SetVisible(true);
        }

        function defineAlturaPainel() {
            var height = Math.max(0, document.documentElement.clientHeight);
            height = height - 135;
            callbackPanel.SetHeight(height);
        }

        function callbackPanel_Init(s, e) {
        }

        function imgConfiguracoes_Click(s, e) {
            AbrirJanelaConfiguracoes();
        }

        function AbrirJanelaConfiguracoes() {
            var window = popup.GetWindowByName("winConfiguracoes");
            popup.ShowWindow(window);
        }

        function FecharJanelaConfiguracoes() {
            var window = popup.GetWindowByName("winConfiguracoes");
            popup.HideWindow(window);
        }

        function btnSalvarParametros_Click(s, e) {
            var parametros = "";
            var qtdeLinhas = gvParametros.cpQuantidadeLinhas;
            for (var i = 0; i < qtdeLinhas; i++) {
                var codigoTipoParametro = gvParametros.GetRowKey(i);
                var valor = ASPxClientComboBox.Cast("cmb_" + i).GetValue();
                parametros += codigoTipoParametro + "=" + valor + ";";
            }
            callback.PerformCallback(parametros);
        }

        function btnCancelarParametros_Click(s, e) {
            FecharJanelaConfiguracoes();
        }

        function callback_CallbackComplete(s, e) {
            if (e.result) {
                window.top.mostraMensagem(e.result, 'erro', true, false, null); 
            }
            else {
                FecharJanelaConfiguracoes();
                document.location.reload(true);
                /*$('iframe').each(function (index) {
                    var src = $(this).attr('src');
                    $(this).attr('src', src);
                });*/
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);">
            <tr height="26px">
                <td valign="middle" style="padding-left: 10px">
                   <table>
            <tr>
                <td>
                     <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True" 
                        Font-Overline="False" Font-Strikeout="False" Text="Painel Pessoal"
                        EnableViewState="False"></asp:Label>
                </td>
                <td>
                    <dxcp:ASPxImage ID="imgConfiguracoes" ClientInstanceName="imgConfiguracoes" runat="server" ShowLoadingImage="true"
                        ToolTip="Configurações" Cursor="pointer">
                        <ClientSideEvents Click="imgConfiguracoes_Click" />
                        <EmptyImage IconID="setup_properties_16x16">
                        </EmptyImage>
                    </dxcp:ASPxImage>
                </td>
            </tr>
        </table>
                </td>
                <td align="left" valign="middle"></td>
            </tr>
        </table>
    <dxcp:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server" Width="100%" OnCustomJSProperties="callbackPanel_CustomJSProperties">
        <ClientSideEvents Init="callbackPanel_Init" />
        <PanelCollection>
            <dxcp:PanelContent runat="server">
                <div id="conteudo"></div>
            </dxcp:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <dxcp:ASPxPopupControl ID="popup" ClientInstanceName="popup" runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" >
        <Windows>
            <dxtv:PopupWindow CloseAction="CloseButton" HeaderText="Configurações" Modal="True" Name="winConfiguracoes" Width="600px">
                <ContentCollection>
                    <dxtv:PopupControlContentControl runat="server">
                        <dxtv:ASPxGridView ID="gvParametros" runat="server" AutoGenerateColumns="False" DataSourceID="dsParametros" KeyFieldName="CodigoTipoParametro" Width="100%" OnCustomJSProperties="gvParametros_CustomJSProperties" >
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings ShowColumnHeaders="False" />
                            <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                            <Columns>
                                <dxtv:GridViewDataTextColumn FieldName="CodigoTipoParametro" ReadOnly="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                    <EditFormSettings Visible="False" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn FieldName="DescricaoTipoParametro" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Caption="Parâmetro" Width="50%">
                                    <EditFormSettings Visible="False" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn FieldName="Parametro" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                    <EditFormSettings Visible="False" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataComboBoxColumn FieldName="Valor" ShowInCustomizationForm="True" VisibleIndex="3" Caption="Valor">
                                    <DataItemTemplate>
                                        <dxtv:ASPxComboBox ID="cmb" runat="server" ClientInstanceName="cmb" DataSourceID="dsPortlet" OnInit="cmb_Init" TextField="TituloPortlet" Value='<%# Bind("Valor") %>' ValueField="CodigoPortlet" ValueType="System.Int32" Width="100%" >
                                        </dxtv:ASPxComboBox>
                                    </DataItemTemplate>
                                </dxtv:GridViewDataComboBoxColumn>
                            </Columns>
                            <%--<ClientSideEvents BatchEditStartEditing="function(s, e){ cmb.PerformCallback(e.visibleIndex); }" />--%>
                        </dxtv:ASPxGridView>
                        <asp:SqlDataSource ID="dsParametros" runat="server" SelectCommand=" SELECT tpu.CodigoTipoParametro, 
        tpu.DescricaoTipoParametro,
        tpu.Parametro,
        CAST(pu.Valor AS INT) AS Valor
   FROM TipoParametroUsuario AS tpu LEFT JOIN
        ParametroUsuario AS pu ON pu.CodigoTipoParametro = tpu.CodigoTipoParametro
  WHERE pu.CodigoUsuario = @CodigoUsuario
    AND tpu.Parametro IN ('PrimeiroPortletPainelPessoal','SegundoPortletPainelPessoal','TerceiroPortletPainelPessoal','QuartoPortletPainelPessoal','QuintoPortletPainelPessoal','SextoPortletPainelPessoal','NumeroColunasPainelPessoal')">
                            <SelectParameters>
                                <asp:SessionParameter Name="CodigoUsuario" SessionField="CodigoUsuario" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="dsPortlet" runat="server" SelectCommand=" SELECT CAST(p.CodigoPortlet AS INT) AS CodigoPortlet,
        p.TituloPortlet
   FROM Portlet AS p
  WHERE p.IndicaPorletPessoal = 'S'"></asp:SqlDataSource>
                        <div>
                            <table style="margin-left: auto;">
                                <tr>
                                    <td style="padding: 10px; padding-right: 0px">
                                        <dxcp:ASPxButton ID="btnSalvarParametros" ClientInstanceName="btnSalvarParametros" AutoPostBack="false" runat="server" Text="Salvar"  Width="100px">
                                            <ClientSideEvents Click="btnSalvarParametros_Click" />
                                            <Paddings Padding="0px" />
                                        </dxcp:ASPxButton>
                                    </td>
                                    <td style="padding: 10px; padding-right: 0px">
                                        <dxcp:ASPxButton ID="btnCancelarParametros" ClientInstanceName="btnCancelarParametros" AutoPostBack="false" runat="server" Text="Cancelar"  Width="100px">
                                            <ClientSideEvents Click="btnCancelarParametros_Click" />
                                            <Paddings Padding="0px" />
                                        </dxcp:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxtv:PopupControlContentControl>
                </ContentCollection>
            </dxtv:PopupWindow>
        </Windows>
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server"></dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
    <dxcp:ASPxCallback ID="callback" ClientInstanceName="callback" runat="server" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="callback_CallbackComplete" />
    </dxcp:ASPxCallback>
</asp:Content>

