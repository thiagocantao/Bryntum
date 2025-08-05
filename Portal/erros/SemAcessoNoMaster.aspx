<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SemAcessoNoMaster.aspx.cs" Inherits="erros_SemAcessoNoMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                        <td style="width: 25px" align="center">
                            &nbsp;</td>
                        <td style="width: 5px">
                            &nbsp;</td>
                        <td>
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                                 Text="Acesso Restrito">
                            </dxe:ASPxLabel>
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 25px">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <span style="color: #cc0000"><strong>
    &nbsp;- Acesso Não Autorizado!</strong></span>
    </div>
    </form>
</body>
</html>
