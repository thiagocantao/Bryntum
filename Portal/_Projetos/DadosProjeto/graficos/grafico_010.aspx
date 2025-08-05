<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_010.aspx.cs" Inherits="grafico_010" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 45px;
        }
        .style3
        {
            height: 10px;
        }
        .style4
        {
            height: 11px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="border-style: solid; border-width: 1px; border-color: #CCCCCC; width:100%; height: 65px;">
                        <tr>
                            <td align="left" style="width: 50%">
                                <table cellpadding="0" cellspacing="0" class="style1">
                                    <tr>
                                        <td class="style2">
                                            <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/riscos.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="height: 16px">
                                                        <dxe:ASPxHyperLink ID="hlRiscosAtivos" runat="server" ForeColor="#5D7B9D" Target="_parent" Text="Riscos Ativos">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 16px">
                                                        <dxe:ASPxHyperLink ID="hlRiscosEliminados" runat="server" ForeColor="#5D7B9D" Target="_parent" Text="Riscos Eliminados">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 2%">
                            </td>
                            <td align="left" style="width: 48%">
                                <table cellpadding="0" cellspacing="0" class="style1">
                                    <tr>
                                        <td class="style2">
                                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/issue.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="height: 16px">
                                                        <dxe:ASPxHyperLink ID="hlProblemasAtivos" runat="server" ForeColor="#5D7B9D" Target="_parent" Text="Problemas Ativos">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        <dxe:ASPxHyperLink ID="hlProblemasEliminados" runat="server" 
                                                             ForeColor="#5D7B9D" Target="_parent" 
                                                            Text="Problemas Eliminados">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>                
    </form>
</body>
</html>
