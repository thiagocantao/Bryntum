<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard_02.aspx.cs" Inherits="espacoTrabalho_VisaoCorporativa_dashboard_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
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
        
        function abreCategoriaLink(valor)
        {
            window.top.gotoURL('_Portfolios/visaoCorporativa.aspx?CC=' + valor, '_top');
        }
    </script>
</head>
<body class="body" style="margin:0px">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="width: 100px">
        <table>
            <tr>
                <td valign="top" align="center" style="width: <%=larguraTela %>; ">
                                <iframe frameborder="0" height="<%=alturaTela %>" scrolling="no" src="<%=telaEstrategia %>"
                                    width="<%=larguraTela %>" id="frmEstrategia"></iframe>
                </td>
                <td valign="top" align="center" style="width: <%=larguraTela %>; ">                                
                    <table>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=telaPortfolio01 %>"
                                    width="<%=larguraTela %>" id="frmPortfolio"></iframe></td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=telaPortfolio02 %>"
                                    width="<%=larguraTela %>" id="frmPortfolio2"></iframe></td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="center" style="width: <%=larguraTela %>; ">
                    <table>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=telaOrcamento01 %>"
                                    width="<%=larguraTela %>" id="frmOrcamento01"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=telaOrcamento02 %>"
                                    width="<%=larguraTela %>" id="frmOrcamento02"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="left">
                    <table cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td>
                                    <iframe src="<%=telaProjetos01 %>" frameborder="0" width="<%=larguraTela %>" scrolling="no"
                                        height="<%=metadeAlturaTela %>" id="frmProjetos01"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <iframe src="<%=telaProjetos02 %>" frameborder="0" width="<%=larguraTela %>" scrolling="no"
                                        height="<%=metadeAlturaTela %>" id="frmProjetos02"></iframe>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 459px">
                                    <tbody>
                                        <tr>
                                            <td style="width: 61px">
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblDescricaoConcluido"
                                                     Text="Satisfatório">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                            <td>
                                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/verdeMenor.gif">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 46px">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblDescricaoPendiente"
                                                     Text="Atenção">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                            <td>
                                                <dxe:ASPxImage ID="ASPxImage5" runat="server" ImageUrl="~/imagens/amareloMenor.gif">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 36px">
                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" ClientInstanceName="lblDescricaoAtrazadas"
                                                     Text="Crítico">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                            <td style="width: 18px">
                                                <dxe:ASPxImage ID="ASPxImage6" runat="server" ImageUrl="~/imagens/vermelhoMenor.gif">
                                                </dxe:ASPxImage>
                                                </td>
                                            <td>
                                                1K = 1.000 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;1M = 1.000.000 &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
