<%@ Page Language="C#" AutoEventWireup="true" CodeFile="assinaturaMultiplosFluxos.aspx.cs"
    Inherits="_CertificadoDigital_assinaturaMultiplosFluxos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../scripts/jquery.ultima.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript" language='javascript'>
        function continuar(s, e) {
            if (e.result) {
                window.top.mostraMensagem(e.result, 'erro', true, false, null); 
                window.top.retornoModal = 'ERRO';
            }
            else {
                window.top.retornoModal = 'OK';
            }
            fechar();
        }

        function cancelar() {
            window.top.retornoModal = 'CANCELAR';
            fechar();
        }

        function fechar() {
            window.top.fechaModal();
        }

        function encodeDocuments() {
            // temporario
            //cbExecutaAcaoServidor.PerformCallback('assina');
            //return;

            appt = document.applets[0];
            appt.markAllDocuments();
            //appt.markDocument(0);
            appt.setSignDocument(true);
            appt.setEncryptDocument(false);
            appt.signAndSendMarkedDocuments();
            var codigos = "";
            $("applet>param[name^='ID.']").each(function (index, value) {
                codigos += $(this).attr("value") + "|";
            });
            cbExecutaAcaoServidor.PerformCallback('efetiva;' + codigos);
        }

        function configAppletCertificacaoDigital() {
            appt = document.applets[0];
            appt.showConfiguration();
        }

        

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
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
                            <ClientSideEvents Click="function(s, e) {encodeDocuments();}" />
                            <Paddings Padding="0px" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnConfigurar" runat="server" Text="Configurar" AutoPostBack="False"
                            ToolTip="Configura o método para assinatura digital" 
                            Width="95px">
                            <ClientSideEvents Click="function(s, e) {configAppletCertificacaoDigital();}" />
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
    <dx:ASPxCallback ID="cbExecutaAcaoServidor" runat="server" 
        ClientInstanceName="cbExecutaAcaoServidor" 
        oncallback="cbExecutaAcaoServidor_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {	continuar(s, e); }" />
    </dx:ASPxCallback>
    <span id="applet" runat="server"></span>
    </form>
</body>
</html>
