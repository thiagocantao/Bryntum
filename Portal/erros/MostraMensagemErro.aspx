<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MostraMensagemErro.aspx.cs" Inherits="erros_MostraMensagemErro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function verificaAvancoWorkflow(codigoWorkflow, codigoEtapa, codigoAcao) {
            window.top.mostraMensagem("Um erro ocorrido impede a execução desta ação, verifique na tela!!!", 'erro', true, false, null);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" 
                                ClientInstanceName="lblTituloTela" Font-Bold="True"
                                 Text="Acesso Restrito">
                            </dxe:ASPxLabel>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td> &nbsp</td></tr>
        <tr><td> &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp<dxe:ASPxLabel ID="lblMensagem" runat="server" 
                ClientInstanceName="lblMensagem" Font-Bold="True" Font-Italic="True" 
               >
            </dxe:ASPxLabel>
            </td> </tr>
    </table>
    <br />
    <br />
    <span style="color: #cc0000"><strong>
    &nbsp;- Acesso Não Autorizado!</strong></span>
    </div>
    </form>
</body>
</html>
