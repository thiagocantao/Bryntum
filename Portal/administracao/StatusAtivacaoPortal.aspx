<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatusAtivacaoPortal.aspx.cs" Inherits="administracao_StatusAtivacaoPortal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="auto-style1" style="text-align: center; padding-left: 10px; padding-right: 10px">
            <tr>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Licenças Adquiridas:">
                    </dxcp:ASPxLabel>
                </td>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel3" runat="server" Text="Licenças Utilizadas:">
                    </dxcp:ASPxLabel>
                </td>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Licenças Disponíveis">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #000000">
                    <dxcp:ASPxLabel ID="lblLicencasAdquiridas" runat="server" ClientInstanceName="lblLicencasAdquiridas">
                    </dxcp:ASPxLabel>
                </td>
                <td style="border: 1px solid #000000">
                    <dxcp:ASPxLabel ID="lblLicencasUtilizadas" runat="server" ClientInstanceName="lblLicencasUtilizadas">
                    </dxcp:ASPxLabel>
                </td>
                <td style="border: 1px solid #000000">
                    <dxcp:ASPxLabel ID="lblLicencasDisponiveis" runat="server" ClientInstanceName="lblLicencasDisponiveis">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
        </table>
        <table align="right" style="padding-top: 10px">
            <tr>
                <td>
                    <dxcp:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100px">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                    </dxcp:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
