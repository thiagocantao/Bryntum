<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="avisos.aspx.cs" Inherits="espacoTrabalho_avisos" Title="Portal da Estratégia" %>


<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False"
                    Text="Avisos"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 10px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 14px">
            </td>
            <td style="height: 14px">
                &nbsp;<dxnc:ASPxNewsControl ID="ASPxNewsControl1" runat="server" Width="400px">
                </dxnc:ASPxNewsControl>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

