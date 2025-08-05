<%@ Page Language="C#" AutoEventWireup="true" CodeFile="an_005.aspx.cs" Inherits="_VisaoAnalista_Graficos_an_005" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <link href="../../estilos/custom_frame.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function abreRiscos(statusParam) {
            if (statusParam == "A")
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?Cor=Vermelho&TipoTela=R', '_top');
            else
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?TipoTela=R', '_top');
        }

        function abreIssues(statusParam) {
            if (statusParam == "A")
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?Cor=Vermelho&TipoTela=Q', '_top');
            else
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?TipoTela=Q', '_top');
        }
        // fazer tratamenmto para clientes que não usam contratos estendidos
        function abreContratos(statusParam, qtdDias, varUsaContratoEstendido) {

            if (varUsaContratoEstendido == "S") {
                if (statusParam == "A")
                    window.top.gotoURL('_Projetos/Administracao/ListaContratosEstendidos.aspx?Vencidos=S&ApenasMeusContratos=S', '_top');
                else
                    window.top.gotoURL('_Projetos/Administracao/ListaContratosEstendidos.aspx?DiasVencimento=' + qtdDias + '&ApenasMeusContratos=S', '_top');
            }
            else {
                if (statusParam == "A")
                    window.top.gotoURL('_Projetos/Administracao/Contratos.aspx?Vencidos=S&ApenasMeusContratos=S', '_top');
                else
                    window.top.gotoURL('_Projetos/Administracao/Contratos.aspx?DiasVencimento=' + qtdDias + '&ApenasMeusContratos=S', '_top');

            }
        }
    </script>
</head>
<body class="body" style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center">
            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho" Width="325px" CornerRadius="1px">
                <ContentPaddings Padding="1px" />
                <HeaderStyle Font-Bold="False"
                    BackColor="#EBEBEB" Height="31px"></HeaderStyle>
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/riscos.png">
                                    </dxe:ASPxImage>
                                </td>
                                <td align="left">
                                    <dxe:ASPxHyperLink ID="lkRiscos" runat="server"
                                        Text="Riscos" ForeColor="Black" EncodeHtml="False">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 50px">
                                    <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/issue.png">
                                    </dxe:ASPxImage>
                                </td>
                                <td align="left">
                                    <dxe:ASPxHyperLink ID="lkIssues" runat="server"
                                        Text="Issues" ForeColor="Black" EncodeHtml="False">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 50px">
                                    <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/contrato.png">
                                    </dxe:ASPxImage>
                                </td>
                                <td align="left">
                                    <dxe:ASPxHyperLink ID="lkContratos" runat="server"
                                        Text="Contratos" ForeColor="Black" EncodeHtml="False">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
                <HeaderTemplate>
                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Gestão">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </HeaderTemplate>
            </dxrp:ASPxRoundPanel>
            &nbsp;
        </div>
    </form>
</body>
</html>
