<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testaPlanoAcao.aspx.cs" Inherits="testaPlanoAcao" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxPageControl ID="pcAbas" runat="server" ActiveTabIndex="0" Height="228px" Width="1211px">
            <TabPages>
<dx:TabPage Text="Coisas a Fazer!" Name="tabPageToDoList"><ContentCollection>
<dx:ContentControl runat="server"></dx:ContentControl>
</ContentCollection>
</dx:TabPage>
</TabPages>
        </dx:ASPxPageControl>
    </div>
    </form>
</body>
</html>
