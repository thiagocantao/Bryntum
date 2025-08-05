<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoCorporativa_08.aspx.cs" Inherits="_VisaoMaster_Graficos_visaoCorporativa_08" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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

        window.parent.idPaginaAtual = 4;
        window.parent.imgAnterior.SetVisible(true);
        window.parent.imgProximo.SetVisible(true);

        window.parent.imgAnterior.SetEnabled(true);
        window.parent.imgProximo.SetEnabled(true);

        window.parent.urlAnterior = '3'
        window.parent.urlProximo = '5'

        window.parent.lblTitulo.SetText('Visão Global de Orçamento UHE Belo Monte');
        window.parent.imgFotos.SetVisible(false);
        window.parent.imgGrafico.SetVisible(false);
        window.parent.btnDownLoad.SetVisible(false);
        window.parent.btnEAP.SetVisible(false);
        window.parent.verificaLabelImagensVisivel();
        

        function getAjuda() {
            return hfAjuda.Get("TextoAjuda");
        }
    </script>
</head>
<body style="margin:0px; margin-left:5px; margin-right:5px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
                    <table>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela1 %>" scrolling="no" src="<%=grafico1 %>"
                                    width="<%=larguraGrafico1 %>px"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                    <tr>
                                        <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico2 %>"
                                    width="<%=larguraTela %>" id="I1" name="I1"></iframe>
                                        </td>
                                        <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico3 %>"
                                    width="<%=larguraTela %>" id="I2" name="I2"></iframe>
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
                            <td align="center" valign="top">
                    <iframe id="tdNumeros" frameborder="0" height="<%=alturaNumeros %>" scrolling="no" src="<%=urlNumeros %>"
                        width="100%"></iframe>
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
                            <td>
                    <dxhf:ASPxHiddenField ID="hfAjuda" runat="server" ClientInstanceName="hfAjuda">
                    </dxhf:ASPxHiddenField>
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
