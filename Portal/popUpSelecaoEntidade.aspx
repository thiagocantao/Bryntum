<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popUpSelecaoEntidade.aspx.cs" Inherits="popUpSelecaoEntidade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="./estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style>
        .alert-erro {
            display: block;
            color: #E07569;
            font-size: 14px;
            padding: 5px;
            font-family: 'Roboto', sans-serif;
            font-weight: bold;
            border-radius: 4px;
            margin-top: 5px;
            margin-bottom: 10px;
            padding-top: 5px;
        }
    </style>
    <script src="Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js"></script>
    <script src="/Scripts/dist/browser/signalr.js"></script>
    <script type="text/javascript" language="javascript">
        var connSignalRnewBriskEntidade = null;
        var urlWSnewBriskBase = "";
        var token = "";
        var authorization = "";
        var urlWSnewBriskBase = "";
        var codigoEntidadeLogada = "";

        $(document).ready(function () {

            connSignalRnewBriskEntidade = null;
            urlWSnewBriskBase;
            token = window.top.getTokenNBRFromMemory(null);
            authorization = "Bearer " + token;
            urlWSnewBriskBase = hfSignalR.Get("urlWSnewBriskBase") + "";
            codigoEntidadeLogada = hfSignalR.Get("codigoEntidadeLogada") + "";

            var sHeight = Math.max(0, document.documentElement.clientHeight) - 90;
            divListaEntidades.style.height = sHeight + 'px';
        });

        function realizaConexaoSignalREntidade(token) {

            sessionStorage.setItem('isLogin', 'S');

            const queryParams = `?login=${true}`;

            connSignalRnewBriskEntidade = new signalR.HubConnectionBuilder()
                .withUrl(`${urlWSnewBriskBase}/hubs/notification/${queryParams}`, { accessTokenFactory: async () => token })
                .configureLogging(signalR.LogLevel.Information)
                .build();

            async function start() {
                try {
                    await connSignalRnewBriskEntidade.start();
                    console.log("SignalR Connected on realizaConexaoSignalREntidade().");
                } catch (err) {
                    lpLoading.Hide();
                    console.log(err);
                    setTimeout(start, 5000);
                }
            };

            connSignalRnewBriskEntidade.on("licenseIssue_sce", function (userNameEconnectionId) {
                connSignalRnewBriskEntidade.stop();
                document.getElementById('lblErro').innerText = 'Limite máximo de usuários simultâneos excedido!';
                lpLoading.Hide();
                window.top.connSignalRnewBrisk.start();
            });

            connSignalRnewBriskEntidade.on("SuccessConnected", function (userNameEconnectionId) {
                connSignalRnewBriskEntidade.stop();
                connSignalRnewBriskEntidade = null;
                window.top.connSignalRnewBrisk.start();
                sessionStorage.setItem('taNBR', token);
                sessionStorage.setItem('nbr_justAccessed', true);
                sessionStorage.removeItem('listaNotificacoes');
                var codigoEntidadeSelecionada = hfSignalR.Get("codigoEntidadeSelecionada");
                selecionarEntidadePadrao(codigoEntidadeSelecionada);
            });

            start();

        }

        function obtemTokenAcessoNBPorEntidade(codigoEntidadeSelecionada) {

            if (token == null) {
                document.getElementById('lblErro').innerText = 'Ocorreu um erro ao tentar obter token de acesso a entidade.';
                window.top.connSignalRnewBrisk.start();
                lpLoading.Hide();
                return;
            }

            $.ajax({
                "url": urlWSnewBriskBase + "/api/v1/token-acesso-workspace?codigoWorkspace=" + codigoEntidadeSelecionada,
                "method": "GET",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json; charset=utf-8",
                    "Authorization": authorization,
                    "cod-entidade-contexto": codigoEntidadeLogada
                },
            }).done(function (response) {
                var token = response.data.access_token;
                if (token != null) {
                    conectaSignalREntidade(token);
                }
            }).fail(function () {
                document.getElementById('lblErro').innerText = 'Ocorreu um erro ao tentar obter token de acesso a entidade.';
                window.top.connSignalRnewBrisk.start();
                lpLoading.Hide();
            });
        }

        function conectaSignalREntidade(token) {
            realizaConexaoSignalREntidade(token);
        }

        function selecionarEntidadePadrao(codigoEntidadeSelecionada) {

            var checkBox = ASPxClientControl.GetControlCollection().GetByName("chkEntidadePadrao");

            if (checkBox != null && checkBox.GetChecked()) {

                $.ajax({
                    "url": urlWSnewBriskBase + "/api/v1/alterar-workspace-padrao-usuario?CodigoNovaEntidadePadrao=" + codigoEntidadeSelecionada,
                    "method": "PUT",
                    "timeout": 0,
                    "headers": {
                        "Content-Type": "application/json; charset=utf-8",
                        "Authorization": authorization,
                        "cod-entidade-contexto": codigoEntidadeLogada
                    },
                }).done(function (response) {
                    if (response.success) {
                        window.top.location.href = hfSignalR.Get("linkRedirect");
                        window.top.fechaModal();
                    }
                }).fail(function () {
                    document.getElementById('lblErro').innerText = 'Ocorreu um erro ao tentar selecionar a entidade como padrão.';
                    window.top.connSignalRnewBrisk.start();
                    lpLoading.Hide();
                });
            } else {
                var linkRedirect = hfSignalR.Get("linkRedirect");
                window.top.location.href = linkRedirect;
                window.top.fechaModal();
            }
        }

        function atualizaInformacaoSelecionada(siglaEntidade, codigoEntidadeNegocio) {
            var link = "inicio.aspx?SUN=" + siglaEntidade + "&CE=" + codigoEntidadeNegocio;
            hfSignalR.Set("linkRedirect", link);
            hfSignalR.Set("codigoEntidadeSelecionada", codigoEntidadeNegocio);
        }

        function acessarEntidade() {
            try {
                window.top.connSignalRnewBrisk.stop();

                var codigoEntidadeSelecionada = hfSignalR.Get("codigoEntidadeSelecionada") + "";

                obtemTokenAcessoNBPorEntidade(codigoEntidadeSelecionada);
            } catch (e) {
                document.getElementById('lblErro').innerText = 'Ocorreu um erro ao tentar acessar a entidade.';
                lpLoading.Hide();
            }

        }

    </script>
</head>
<body style="margin: 0px">
    <form id="form1" enableviewstate="false" runat="server">
        <dxhf:ASPxHiddenField ID="hfSignalR" runat="server" ClientInstanceName="hfSignalR" />
        <dx:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading" />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div id="divListaEntidades" style="overflow:auto">
                        <dxcp:ASPxRadioButtonList ID="rbEntidades" runat="server" ClientInstanceName="rbEntidades" Width="100%">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
            var item = s.GetSelectedItem();
            var codigoEntidadeNegocio = item.value;
            var siglaEntidade = hfListaSigla.Get(codigoEntidadeNegocio);
            atualizaInformacaoSelecionada(siglaEntidade, codigoEntidadeNegocio);
        }" />
                        </dxcp:ASPxRadioButtonList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" class="alert-erro" Visible="true" ID="lblErro"></asp:Label>
                </td>
            </tr>
        </table>

        <div style="position:sticky; bottom: 0px; right: 0px; top:0px">
                        <table style="width: 100%">
                            <tr>
                                <td align="left">
                                    <dx:ASPxCheckBox ID="chkEntidadePadrao" ClientInstanceName="chkEntidadePadrao" Text="Tornar esta minha entidade padrão." runat="server" CheckState="Unchecked" BackColor="White" />
                                </td>
                                <td align="right" style="width: 100px">
                                    <dxe:ASPxButton ID="btnAcessar" runat="server" AutoPostBack="False"
                                        Text="Acessar" Width="100%">
                                        <ClientSideEvents Click="function(s, e) {
                                                                        lpLoading.Show()
                                                                        acessarEntidade();
                                                                        }" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                                <td align="right" style="padding-left: 5px" width="100px">
                                    <dxe:ASPxButton ID="btnFechar" runat="server"
                                        Text="Fechar" Width="100%" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
                                                                        window.top.fechaModal();
                                                                        }" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
        <dxcp:ASPxHiddenField ID="hfListaSigla" runat="server" ClientInstanceName="hfListaSigla">
        </dxcp:ASPxHiddenField>
    </form>
</body>
</html>
