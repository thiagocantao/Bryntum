<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forumMenu.aspx.cs" Inherits="espacoTrabalho_forumMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
</head>
<body style="margin-left: 0px; margin-top:0px">
    <form id="form1" runat="server">
    <div>
            <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
                    width: 205px">
                    <tr>
                        <td style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                            height: 26px; padding-left: 10px;">
                            &nbsp;<dxe:aspxlabel id="lblTituloTela" runat="server" clientinstancename="lblTituloTela"
                                font-bold="True"  text="Comunicação"></dxe:aspxlabel>
                        </td>
                    </tr>
                </table>            
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #e4e3e4 1px solid;height:<%=alturaTabela %>; width:205px ">
            <tr>
                <td style="width: 7px" valign="top">
                </td>
                <td valign="top" style="width: 205px; padding-top: 5px;" align="center">
                    <dxnb:ASPxNavBar ID="nvbMenuForuns" runat="server" AutoCollapse="True"
                        ClientInstanceName="nvbMenuForuns" >
<ClientSideEvents ItemClick="function(s, e) {
	//lblTituloTela.SetText(e.item.GetText());
}"></ClientSideEvents>

<ItemStyle ></ItemStyle>
<Groups>
<dxnb:NavBarGroup Name="gpForuns" Text="Ambiente de Discuss&#227;o">
<ItemStyle ></ItemStyle>
</dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="gpAvisos" Text="Avisos"><Items>
<dxnb:NavBarItem Name="Neg" Text="Neg&#243;cio"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Mis" Text="Miss&#227;o"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Vis" Text="Vis&#227;o"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Cre" Text="Cren&#231;as e Valores"></dxnb:NavBarItem>
</Items>

<HeaderStyle ></HeaderStyle>

<ItemStyle ></ItemStyle>
</dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="gpBiblioteca" Text="Biblioteca">
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
    </div>
    </form>
</body>
</html>
