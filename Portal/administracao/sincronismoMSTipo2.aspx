<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sincronismoMSTipo2.aspx.cs" Inherits="administracao_sincronismoMSTipo2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function executaAcao()
        {
            window.parent.mostraInformacao('A revisão dos cadastros dos recursos na base de dados do MS-Project precisa ser feita no próprio ambiente EPM  da Microsoft. Após atualizar os cadastros, volte a esta tela para verificar se o recurso foi vinculado automaticamente.');
            
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
                            <dxe:ListEditItem Selected="True" Text="Revisar, no MS-Project, os cadastros dos recursos envolvidos"
                                Value="1" />
                        </Items>
                        <Paddings Padding="0px" PaddingLeft="15px" />
                    </dxe:ASPxRadioButtonList>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
