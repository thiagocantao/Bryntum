<%@ Page Language="C#" AutoEventWireup="true" CodeFile="opcoes.aspx.cs" Inherits="_Portfolios_Administracao_opcoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Menu Portfolio</title>

    <script type="text/javascript" language="javascript">   
    
    function habilitaItemMenu(nomeItem)
    {
        var item = menuItens.GetItemByName(nomeItem);
        item.SetEnabled(true);
    }
    
    function desabilitaItemMenu(nomeItem)
    {
        var item = menuItens.GetItemByName(nomeItem);
        item.SetEnabled(false);
    }
    
    </script>

</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #e0e0e0 1px solid;
            background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); width: 192px;">
            <tr height="26px">
                <td valign="middle" style="height: 26px;padding-left:10px">
                   <asp:Label ID="lblTitulo" runat="server" Font-Overline="False"
                        Font-Strikeout="False" Text="Gestão Portfolio" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #e4e3e4 1px solid;height:<%=alturaTabela %>; width:192px ">
            <tr>
                <td style="width: 7px; height: 4px" valign="top">
                </td>
                <td style="height: 4px" valign="top">
                </td>
                <td style="width: 7px; height: 4px" valign="top">
                </td>
            </tr>
            <tr>
                <td style="width: 7px" valign="top">
                </td>
                <td valign="top">
                    <dxnb:ASPxNavBar ID="nvbMenuPortfolio" runat="server" AutoCollapse="True"
                        ClientInstanceName="nvbMenuPortfolio"  Width="170px">
<ItemStyle ></ItemStyle>
<Groups>
<dxnb:NavBarGroup Name="gpPortfolio" Text="Gest&#227;o">
<ItemStyle ></ItemStyle>
</dxnb:NavBarGroup>
</Groups>

<Paddings PaddingLeft="5px" PaddingRight="5px"></Paddings>
</dxnb:ASPxNavBar>
                </td>
                <td style="width: 7px" valign="top">
                </td>
            </tr>
        </table>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>

</html>
