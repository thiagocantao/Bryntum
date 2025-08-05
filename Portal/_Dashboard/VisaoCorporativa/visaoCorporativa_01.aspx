<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoCorporativa_01.aspx.cs" Inherits="_Dashboard_VisaoCorporativa_visaoCorporativa_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />  
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script> 
    <link href="../../estilos/custom_frame.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
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
                    <table>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico3 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico6 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" style="display: none">
                    <table>
                        <tr>
                            <td>
                                            <table cellpadding="0" cellspacing="0" style="width: 230px; ">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 95px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                                        Text="1K = 1.000">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                        Text="1M = 1.000.000">
                                                                    </dxe:ASPxLabel>
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
                                                    <td style="display: none">
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%; ">
                                                            <tr>
                                                                <td style="width: 95px">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 20px; background-color: <%=corPrevisto %>">
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                                                    Text="Previsto">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 20px; background-color: <%=corReal %>">
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                                                    Text="Realizado">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
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
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%; ">
                                                            <tr>
                                                                <td style="width: 95px">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 20px; background-color: <%=corSatisfatorio %>">
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                                                    Text="Satisfatório">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 75px">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 20px; background-color: <%=corAtencao %>">
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                                                                    Text="Atenção">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 20px; background-color: <%=corCritico %>">
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                                                                    Text="Crítico">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
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
