<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupChaveAcesso.aspx.cs" Inherits="administracao_popupChaveAcesso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 654px;
        }

        .auto-style2 {
            width: 100%;
        }

        #btnCopiar {
            font-family: FontAwesome;
            border-style: solid;
            height: 35px;
            border-width: thin;
            border-color: darkgray;
            border-radius: 2px;
            display: none;
            width: 25px;
        }
    </style>
    <link href="../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../scripts/clipboard.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <li style="font-family: verdana; font-size: 9pt;padding-bottom:5px">As chaves de acesso são geradas para permitir o acionamento direto de API's do sistema.</li>
                    <li style="font-family: verdana; font-size: 9pt;padding-bottom:5px">Para gerar uma nova chave para o usuário, clique no botão "Gerar Chave".</li>
                    <li style="font-family: verdana; font-size: 9pt;padding-bottom:5px">Ao fechar esta tela, a chave gerada não será mais apresentada. Portanto, é importante anotar a chave para uso posterior.</li>
                    <li style="font-family: verdana; font-size: 9pt;padding-bottom:10px">O clique no ícone ao final do campo "Client Secret" copia a chave para a área de transferência.</li>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <dxcp:ASPxLabel ID="ASPxLabel3" runat="server" Text="Usuário:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxcp:ASPxTextBox ID="txtDescricaoUsuario" runat="server" ClientInstanceName="txtDescricaoUsuario" Width="100%" ReadOnly="True">
                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                        </ReadOnlyStyle>
                    </dxcp:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Client ID:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxcp:ASPxTextBox ID="txtClientID" runat="server" ClientInstanceName="txtClientID" Width="100%" ReadOnly="True">
                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                        </ReadOnlyStyle>
                    </dxcp:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Client Secret:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="1" class="auto-style2">
                        <tr>
                            <td style="width: 100%">
                                <dxcp:ASPxCallbackPanel ID="callbackGeraChave" runat="server" Width="100%" ClientInstanceName="callbackGeraChave" OnCallback="callbackGeraChave_Callback1">
                                    <ClientSideEvents EndCallback="function(s, e) {
	var objetoBotaoCopiar = document.getElementById('btnCopiar');
               objetoBotaoCopiar.style.display = 'block';
}" />
                                    <PanelCollection>
                                        <dxcp:PanelContent runat="server">
                                            <dxcp:ASPxTextBox ID="txtClientSecret" runat="server" ClientInstanceName="txtClientSecret" Width="100%" ReadOnly="True">
                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </ReadOnlyStyle>
                                            </dxcp:ASPxTextBox>
                                        </dxcp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                            <td>
                                <button id="btnCopiar" type="button" title="Copiar as chaves Client_ID e Client_secret" data-clipboard-text="Just because you can doesn't mean you should — clipboard.js">
                                    &#xf0c5</button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 10px">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="padding-right: 5px">
                                <dxcp:ASPxButton ID="btnGerarChave" runat="server" Text="Gerar Chave" ClientInstanceName="btnGerarChave" Width="100px" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
	callbackGeraChave.PerformCallback();
}" />
                                </dxcp:ASPxButton>
                            </td>
                            <td>
                                <dxcp:ASPxButton ID="btnFechar" runat="server" Text="Fechar" ClientInstanceName="btnFechar" Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                </dxcp:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">

        document.querySelector('#btnCopiar').addEventListener('click', function (e) {

            var objetoBotaoCopiar = document.getElementById('btnCopiar');
            var strAreaTransferencia = "Client_ID: " + txtClientID.GetText() + '\n';
            strAreaTransferencia += "Client_secret: " + txtClientSecret.GetText();


            objetoBotaoCopiar.setAttribute('data-clipboard-text', strAreaTransferencia);
        });

        var objetoCopia = new ClipboardJS('#btnCopiar');
        objetoCopia.on('success', function (e) {
            window.top.mostraMensagem('Chaves copiadas para a área de transferência!', 'sucesso', false, false, null, 2000);
            e.clearSelection();
        });

        objetoCopia.on('error', function (e) {
            //console.error('Action:', e.action);
            //console.error('Trigger:', e.trigger);
        });



    </script>
</body>
</html>
