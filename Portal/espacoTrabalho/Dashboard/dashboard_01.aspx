<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard_01.aspx.cs" Inherits="espacoTrabalho_VisaoCorporativa_dashboard_01" %>

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
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
                                <iframe frameborder="0" height="<%=alturaTela %>" scrolling="no" src="<%=telaEstrategia %>"
                                    width="<%=larguraTela %>" id="frmEstrategia"></iframe>
                </td>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
                                <iframe frameborder="0" height="<%=alturaTela %>" scrolling="no" src="<%=telaPortfolio %>"
                                    width="<%=larguraTela %>" id="frmPortfolio"></iframe>
                </td>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
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
    </div>
    </form>
</body>
</html>
