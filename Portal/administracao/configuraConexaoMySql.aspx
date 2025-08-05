<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="configuraConexaoMySql.aspx.cs" Inherits="administracao_configuraConexaoMySql" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="height: 26px; padding-left: 20px; background-image: url('../imagens/titulo/back_Titulo_Desktop.gif');"
                valign="middle">
                <dxe:ASPxLabel ID="lblTitulo" runat="server" Text="Conexão com o MySql (LDAP)" >
                </dxe:ASPxLabel>
            </td>
            <td style="height: 26px; width: 10px;" valign="middle">
                &nbsp;
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <table>
                    <tr>
                        <td width="165">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Servidor:">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="165">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Porta:">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="165">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Banco:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td width="165">
                            <dxe:ASPxTextBox ID="txtServidor" runat="server" Width="150px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="" SetFocusOnError="True">
                                    <RequiredField ErrorText="Preenchimento obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="165">
                            <dxe:ASPxTextBox ID="txtPorta" runat="server" Width="150px" Text="3306">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="" SetFocusOnError="True">
                                    <RequiredField ErrorText="Preenchimento obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="165">
                            <dxe:ASPxTextBox ID="txtBanco" runat="server" Width="150px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="" SetFocusOnError="True">
                                    <RequiredField ErrorText="Preenchimento obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="165">
                            &nbsp;
                        </td>
                        <td width="165">
                            &nbsp;
                        </td>
                        <td width="165">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="165">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Usuário:">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="165">
                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Senha:">
                            </dxe:ASPxLabel>
                        </td>
                        <td rowspan="2" valign="bottom" width="165">
                            <dxe:ASPxCheckBox ID="cbSalvar" runat="server" Checked="True" CheckState="Checked"
                                Text="Salvar após o teste">
                            </dxe:ASPxCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="165">
                            <dxe:ASPxTextBox ID="txtUsuario" runat="server" Width="150px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="" SetFocusOnError="True">
                                    <RequiredField ErrorText="Preenchimento obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="165">
                            <dxe:ASPxTextBox ID="txtSenha" runat="server" Width="150px" Password="True">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="" SetFocusOnError="True">
                                    <RequiredField ErrorText="Preenchimento obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="165">
                            &nbsp;
                        </td>
                        <td width="165">
                            &nbsp;
                        </td>
                        <td width="165">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <dxe:ASPxButton ID="btnTestarConexao" runat="server" Text="Testar conexão" Width="150px"
                    OnClick="btnTestarConexao_Click">
                </dxe:ASPxButton>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <dxe:ASPxLabel ID="lblResultadoTesteConexao" runat="server" Font-Bold="True">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
</asp:Content>
