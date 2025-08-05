<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoCorporativa_01.aspx.cs" Inherits="_VisaoNE_VisaoCorporativa_visaoCorporativa_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function mostrarNumeros(mostrar) {
            if (mostrar) {
                document.getElementById('tdNumeros').style.display = 'block';
                imgMostrarNumeros.SetVisible(false);

            }
            else {
                document.getElementById('tdNumeros').style.display = 'none';
                imgMostrarNumeros.SetVisible(true);
            }
        }
    </script>
</head>
<body style="margin: 0px">
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
                                    <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico2 %>"
                                        width="<%=larguraTela %>"></iframe>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="center" style="width: <%=larguraTela2 %>">
                        <table>
                            <tr>
                                <td>
                                    <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico3 %>"
                                        width="<%=larguraTela2 %>"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico4 %>"
                                        width="<%=larguraTela2 %>"></iframe>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table>
                            <tr>
                                <td align="center">
                                    <iframe id="tdNumeros" frameborder="0" height="<%=alturaNumeros %>" scrolling="auto" src="numeros_001.aspx"
                                        width="300px"></iframe>
                                    <dxe:ASPxImage ID="imgMostrarNumeros" runat="server" ClientInstanceName="imgMostrarNumeros"
                                        ClientVisible="False" CssClass="mao" ImageUrl="~/imagens/mostrarNumeros.png"
                                        ToolTip="Mostrar Números dos Contratos">
                                        <ClientSideEvents Click="function(s, e) {
	mostrarNumeros(true);
}" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
