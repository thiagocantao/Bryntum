<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizacaoDashboardPainelDinamico.aspx.cs" Inherits="_Dashboard_VisualizacaoDashboardPainelDinamico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0;">
    <form id="form1" runat="server">
        <div>
            <dxrp:ASPxRoundPanel ID="roundPanel" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho"
                Height="215px" Width="210px" CornerRadius="1px">
                <ContentPaddings Padding="1px" />
                <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB" Height="31px"></HeaderStyle>
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <iframe id="frameDashboard" runat="server" style="width: 100%; height: 100%; border: none;"></iframe>
                    </dxp:PanelContent>
                </PanelCollection>
                <HeaderTemplate>
                    <table cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <dxe:ASPxLabel ID="lblTitulo" runat="server" Text="Minhas Metas" OnInit="lblTitulo_Init">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </HeaderTemplate>
            </dxrp:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
