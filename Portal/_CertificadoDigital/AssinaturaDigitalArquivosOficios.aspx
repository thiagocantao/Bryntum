<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssinaturaDigitalArquivosOficios.aspx.cs" Inherits="_CertificadoDigital_AssinaturaDigitalArquivosOficios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function onBtnContinuar_Click(s, e) {
            loadingPanel.Show();
            callback.PerformCallback('assinar');
        }

        function cancelar() {
            window.top.retornoModal = 'CANCELAR';
            fechar();
        }

        function fechar() {
            window.top.fechaModal();
        }

        function onCallbackComplete(s, e) {
            var result = e.result;
            if (result == '') {
                loadingPanel.Hide();
                window.top.retornoModal = 'OK';
                fechar();
            }
            else if(isNumeric(result)){
                var intervalo = s.cpIntervaloVerificacaoStatusAssinatura;
                var func = function () {
                    s.PerformCallback(e.result);
                };
                setTimeout(func, intervalo);
            }
            else {
                loadingPanel.Hide();
                window.top.mostraMensagem(result, 'atencao', true, false, null);
            }
        }

        function isNumeric(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="text-align: justify; height: 250px;">
                <dx:ASPxLabel ID="lblTextoInformacao" runat="server" Text="Texto explicando ao usuário que ação de mudança da etapa exige que os formulários sejam assinados de forma digital. Texto explicando ao usuário que ação de mudança da etapa exige que os formulários sejam assinados de forma digital."
                     ClientInstanceName="lblTextoInformacao">
                </dx:ASPxLabel>
                <p>
                </p>
            </div>
            <div>
                <table style="margin-left: auto;">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnContinuar" runat="server" Text="Continuar" AutoPostBack="False"
                                 Width="95px">
                                <ClientSideEvents Click="onBtnContinuar_Click" />
                                <Paddings Padding="0px" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="False"
                                 Width="95px">
                                <ClientSideEvents Click="function(s, e) {cancelar();}" />
                                <Paddings Padding="0px" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <dxcp:ASPxLoadingPanel ID="loadingPanel" runat="server" ClientInstanceName="loadingPanel" Text="Aguardando assinatura&amp;hellip;"  Modal="True">
        </dxcp:ASPxLoadingPanel>
        <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback" OnCustomJSProperties="callback_CustomJSProperties">
            <ClientSideEvents CallbackComplete="onCallbackComplete" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
