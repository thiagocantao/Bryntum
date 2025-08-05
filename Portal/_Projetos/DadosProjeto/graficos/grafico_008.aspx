<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_008.aspx.cs" Inherits="grafico_008" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
       
                    <table cellpadding="0" cellspacing="0" style="border-style: solid; border-width: 1px; border-color: #CCCCCC; width:100%; height: 65px;">
                        <tr>
                            <td align="left" style="height: 3px">
                            </td>
                            <td align="left" style="width: 5px; height: 3px">
                            </td>
                            <td align="left" style="height: 3px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="hlCronograma" runat="server" 
                                    ForeColor="#5D7B9D" Text="0 Atrasos no Cronograma" Target="_parent">
                                </dxe:ASPxHyperLink>
                            </td>
                            <td align="left" style="width: 5px">
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="hlToDoList" runat="server" 
                                    ForeColor="#5D7B9D" Text="0 Atrasos no To Do List" Target="_parent">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 10px">
                            </td>
                            <td align="left" style="width: 5px; height: 10px">
                            </td>
                            <td align="left" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="hlMensagens" runat="server" 
                                    ForeColor="#5D7B9D" Text="0 Mensagens N&#227;o Respondidas" Target="_parent">
                                </dxe:ASPxHyperLink>
                            </td>
                            <td align="left" style="width: 5px">
                            </td>
                            <td align="left">
                                <asp:Label ID="lblAtualizacao" runat="server" EnableViewState="False"
                                   >--/--/----</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 3px">
                            </td>
                            <td align="left" style="width: 5px; height: 3px">
                            </td>
                            <td align="left" style="height: 3px">
                            </td>
                        </tr>
                    </table>
             
    </form>
</body>
</html>
