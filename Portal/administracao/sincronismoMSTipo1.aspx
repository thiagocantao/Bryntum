<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sincronismoMSTipo1.aspx.cs" Inherits="administracao_sincronismoMSTipo1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function executaAcao()
        {
            if(rbOpcao.GetValue() == "1")
            {
                window.parent.mostraInformacao('A atualização do email do recurso na base de dados do MS-Project precisa ser feita no próprio ambiente EPM  da Microsoft. Após atualizar o email, volte a esta tela para verificar se o recurso foi vinculado automaticamente.');
            }
            else
            {
                window.parent.atualizaEmailSistema();
            }
        }
    </script>
    
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxRadioButtonList ID="rbOpcao" runat="server" ClientInstanceName="rbOpcao"
                         SelectedIndex="0">
                        <Items>
                            <dxe:ListEditItem Selected="True" Text="Informar um email para o recurso no MS-Project (recomendado)."
                                Value="1" />
                            <dxe:ListEditItem Text="Manter o recurso no MS-Project sem email e registrar email apenas no Sistema"
                                Value="2" />
                        </Items>
                        <Paddings Padding="0px" PaddingLeft="15px" />
                        <Border BorderStyle="None" />
                    </dxe:ASPxRadioButtonList>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
