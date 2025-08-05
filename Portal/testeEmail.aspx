<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="testeEmail.aspx.cs" Inherits="administracao_testeEmail" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript">
        function enviaEmail()
        {
            callBackEnviar.PerformCallback();
        }
    </script>
    <table>
        <tr>
            <td style="width: 10px; height: 10px"></td>
            <td style="height: 10px"></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width: 360px">
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                            Text="Servidor SMTP:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 90px">
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                            Text="Porta:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 360px">
                                        <dxe:ASPxTextBox ID="txtServidor" runat="server" 
                                            Width="350px">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td style="width: 90px">
                                        <dxe:ASPxSpinEdit ID="txtPorta" runat="server" 
                                            NumberType="Integer" Width="77px">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="ckSSL" runat="server" 
                                            Text="SSL">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width: 360px">
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                            Text="Usuário:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                            Text="Senha:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 360px">
                                        <dxe:ASPxTextBox ID="txtUsuario" runat="server"  Width="350px">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtSenha" runat="server"  Width="77px">
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width: 360px">
                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                            Text="Remetente:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                            Text="Destinatário:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 360px">
                                        <dxe:ASPxTextBox ID="txtRemetenteEmail" runat="server" 
                                            Width="350px">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtDestinatario" runat="server"  Width="350px">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px"></td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="lblTipoServidorEnvioEmail" runat="server" 
                                            Text="Tipo do servidor que enviará o e-mail." Font-Bold="True">
                                        </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px"></td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width: 170px">
                                        <dxe:ASPxButton ID="btnEnviar" runat="server" AutoPostBack="False"
                                            Text="Enviar" Width="148px">
                                            <Paddings Padding="0px" />
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	enviaEmail();
}" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                                            Text="Voltar Configurações" Width="148px" OnClick="ASPxButton1_Click">
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="callBackEnviar" runat="server" ClientInstanceName="callBackEnviar" OnCallback="callBackEnviar_Callback1">
        <ClientSideEvents EndCallback="function(s, e) {
	window.top.mostraMensagem(s.cp_Msg, 'Atencao', true, false, null);
}" />
    </dxcb:ASPxCallback>
</asp:Content>

