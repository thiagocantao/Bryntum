<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoCorporativa_02.aspx.cs" Inherits="_Portfolios_visaoCorporativa_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function mostrarNumeros(mostrar)
        {
            if(mostrar)
            {
                document.getElementById('tdNumeros').style.display = 'block';
                imgMostrarNumeros.SetVisible(false);
                
            }
            else
            {
                document.getElementById('tdNumeros').style.display = 'none';
                imgMostrarNumeros.SetVisible(true);
            }
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" enableviewstate="false" runat="server">
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
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico3 %>"
                                    width="<%=larguraTela %>" id="I6" name="I6"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
                    <table>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico2 %>"
                                    width="<%=larguraTelaGrande %>" id="I5" name="I5"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                    <tr>
                                        <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico4 %>"
                                    width="<%=larguraTela %>" id="I7" name="I7"></iframe>
                                        </td>
                                        <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico5 %>"
                                    width="<%=larguraTela %>" id="I8" name="I8"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td align="center">
                    <iframe id="tdNumeros" frameborder="0" height="<%=alturaNumeros %>" scrolling="no" src="<%=numeros1 %>"
                        width="230px"></iframe>
                                <dxe:ASPxImage ID="imgMostrarNumeros" runat="server" ClientInstanceName="imgMostrarNumeros"
                                    ClientVisible="False" CssClass="mao" ImageUrl="~/imagens/mostrarNumeros.png"
                                    ToolTip="Mostrar Números da Instituição">
                                    <ClientSideEvents Click="function(s, e) {
	mostrarNumeros(true);
}" Init="function(s, e) {
	
}" />
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
        </table>
    
    </div>
    <script language="javascript" type="text/javascript">
        mostrarNumeros(imgMostrarNumeros.cp_mostraDefault != 'N');
    </script>
    </form>
</body>
</html>
