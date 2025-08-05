<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoCorporativa_07.aspx.cs" Inherits="_VisaoMaster_Graficos_visaoCorporativa_07" %>

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

        window.parent.idPaginaAtual = 7;
        window.parent.imgAnterior.SetVisible(true);
        window.parent.imgProximo.SetVisible(true);

        window.parent.imgAnterior.SetEnabled(true);
        window.parent.imgProximo.SetEnabled(true);

        window.parent.urlAnterior = '6'
        window.parent.urlProximo = ''

        window.parent.lblTitulo.SetText('Painel de Gerenciamento - Sítio Diques');
        window.parent.imgFotos.SetVisible(true);
        window.parent.imgGrafico.SetVisible(true);
        window.parent.btnDownLoad.SetVisible(false);
        window.parent.btnEAP.SetVisible(false);
        window.parent.verificaLabelImagensVisivel();
        
        window.parent.iniciaisAssociacao = 'Diques';
        window.parent.nomeSitio = 'Sítio Diques';

        function getAjuda() {
            return hfAjuda.Get("TextoAjuda");
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
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico1 %>"
                                    width="<%=larguraTela2 %>"></iframe>
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
                            <td align="center">
                    <iframe id="tdNumeros" frameborder="0" height="<%=alturaNumeros %>" scrolling="no" src="numeros_006.aspx?CR=Diques"
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
