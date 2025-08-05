<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoCorporativa_01.aspx.cs" Inherits="_Portfolios_VisaoCorporativa_visaoCorporativa_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
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
        
        function abreCategoriaLink(valor)
        {
            parent.clickLinkCategoria(valor);
        }
    </script>
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
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="vc_001.aspx"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="vc_002.aspx"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5px">
                </td>
                <td align="center" valign="top"  style="width: <%=larguraTabela %>">
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                    <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="vc_003.aspx"
                        width="<%=larguraTabela %>"></iframe>
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                <iframe src="vc_004.aspx" frameBorder="0" width="<%=larguraTela %>" 
                                    scrolling=no height="<%=metadeAlturaTela %>"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="vc_005.aspx" width="<%=larguraGraficoBarras %>"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5px">
                </td>
                <td valign="top">
                    <table cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="center">
                                    <iframe id="tdNumeros" src="vc_006.aspx" frameborder="0" width="100%" scrolling="no"
                                        height="<%=alturaNumeros %>"></iframe>
                                    <dxe:ASPxImage ID="imgMostrarNumeros" runat="server" ToolTip="Mostrar Números do Portfólio"
                                        ImageUrl="~/imagens/mostrarNumeros.png" CssClass="mao" ClientVisible="False"
                                        ClientInstanceName="imgMostrarNumeros">
                                        <ClientSideEvents Click="function(s, e) {
	mostrarNumeros(true);
}" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
