<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoDemandas_01.aspx.cs" Inherits="_Portfolios_visaoDemandas_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />   
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                <table>
            <tr>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
                    <table>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico1 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico4 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
                    <table>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico2 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico5 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
                    &nbsp;<iframe frameborder="0" height="<%=alturaTela %>" scrolling="no" src="<%=grafico3 %>"
                                    width="<%=larguraTela %>"></iframe>
                </td>
            </tr>
        </table>
                </td>
            </tr>
            <tr>
                <td style="height: 3px">
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 5px">
                            </td>
                            <td style="width: 20px; background-color: #8eabdb">
                            </td>
                            <td style="width: 125px">
                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                    Text="Demandas Simples">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 20px; background-color: #cebeaa">
                            </td>
                            <td style="width: 145px">
                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                                    Text="Demandas Complexas">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 20px; background-color: #D8D99B">
                            </td>
                            <td style="width: 62px">
                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
                                    Text="Projetos">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 20px; background-color: #8FBC8F">
                            </td>
                            <td style="width: 75px">
                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                    Text="Processos">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 230px">
                                <dxe:ASPxLabel ID="lblLegendaTempo" runat="server" Font-Italic="True"
                                   >
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>       
    
    </div>
    </form>
</body>
</html>
