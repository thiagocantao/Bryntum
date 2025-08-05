<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_011.aspx.cs" Inherits="grafico_011" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="border-style: solid; border-width: 1px; border-color: #CCCCCC; width:100%; height: 65px;">
                        <tr>
                            <td align="left" style="width: 50%; ">
                                <table>
                                    <tr>
                                        <td style="width: 30px; height: 16px;" align="center">
                                            <dxe:ASPxImage ID="imgRA" runat="server" ImageUrl="~/imagens/riscos.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="height: 16px">
                                            <dxe:ASPxHyperLink ID="hlRiscosAtivos" runat="server" 
                                                ForeColor="#5D7B9D" Target="_parent" Text="Riscos Ativos">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 2%; ">
                            </td>
                            <td align="left" style="width: 48%; ">
                                <table>
                                    <tr>
                                        <td style="width: 30px; height: 16px;" align="center">
                                            <dxe:ASPxImage ID="imgPA" runat="server" ImageUrl="~/imagens/issue.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="height: 16px">
                                            <dxe:ASPxHyperLink ID="hlProblemasAtivos" runat="server" 
                                                ForeColor="#5D7B9D" Target="_parent" Text="Problemas Ativos">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
    </form>
</body>
</html>
