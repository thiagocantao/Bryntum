<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="mostraFormulario.aspx.cs"
    Inherits="formularios_mostraFormulario" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"  %>    

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
<asp:Panel ID="pnExterno" runat="server">
    </asp:Panel>
    <table>
        <tr>
            <td align="center">
                <asp:HiddenField ID="hfCodigoFormularioMaster" runat="server" />
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                    <tr style="height: 10px">
                        <td style="width: 2px">
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2px">
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td style="width: 80px">
                                        <dxe:ASPxButton ID="btnSalvar1" runat="server" Text="Salvar" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" OnClick="btnSalvar1_Click">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btnCancelar1" runat="server" Text="Cancelar" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" OnClick="btnCancelar1_Click">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2px">
                        </td>
                        <td align="right" >
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2px">
                        </td>
                        <td>
                            <asp:Panel ID="pnCampos" runat="server" Style="width: 99%; border-right: #a9a9a9 1px dashed;
                                border-top: #a9a9a9 1px dashed; border-left: #a9a9a9 1px dashed; border-bottom: #a9a9a9 1px dashed;
                                text-align: left;" Width="99%">
                                &nbsp;<dxe:ASPxLabel ID="lblNomeFormulario" runat="server" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                    CssPostfix="Aqua" Font-Bold="True" Text="Nome do Formulário">
                                </dxe:ASPxLabel>
                                &nbsp;&nbsp;<br />
                                &nbsp;<dxe:ASPxLabel ID="lblDescricaoFormulario" runat="server" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                    CssPostfix="Aqua" Text="Descrição do Formulário">
                                </dxe:ASPxLabel>
                                &nbsp;<br />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2px">
                        </td>
                        <td align="right" >
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2px">
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td style="width: 80px">
                                        <dxe:ASPxButton ID="btnSalvar2" runat="server" Text="Salvar" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" OnClick="btnSalvar1_Click">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btnCancelar2" runat="server" Text="Cancelar" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" OnClick="btnCancelar1_Click">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
</asp:Content>
