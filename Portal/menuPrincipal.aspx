<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menuPrincipal.aspx.cs" Inherits="menuPrincipal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="./estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">

.dxnbControl 
{
	font: 9pt Tahoma;
	color: black;
	background-color: white;
}
.dxnbControl td.dxnbCtrl
{
    padding: 11px;
}
.dxnbGroupHeader, .dxnbGroupHeaderCollapsed
{
    text-align: left;
}
.dxnbGroupHeader
{
	font: bold 9pt Tahoma;
	color: black;
	background-color: #E0E0E0;
	border: solid 1px #A8A8A8;
	padding: 4px 10px 4px 10px;
}
.dxnbGroupHeader table.dxnb
{
	font: bold 9pt Tahoma;
	color: black;
}
.dxnbGroupHeader td.dxnb
{
	white-space: nowrap;
}
.dxnbGroupContent
{
	font: 9pt Tahoma;
	color: #1E3695;
	border: solid 1px #A8A8A8;
	padding: 5px 5px 5px 5px;
}
.dxnbItem, .dxnbItemHover, .dxnbItemSelected,
.dxnbBulletItem, .dxnbBulletItemHover, .dxnbBulletItemSelected
{
    text-align: left;
}
.dxnbItem
{
	padding-top: 4px;
	padding-right: 5px;
	padding-bottom: 5px;
	padding-left: 5px;
}
.dxnbItem, .dxnbLargeItem, .dxnbBulletItem
{
	font: 9pt Tahoma;
	color: #1E3695;
}
.dxnbGroupSpacing,
.dxnbItemSpacing
{
	width: 100%;
	height: 1px;
}
    </style>
</head>
<body style="margin:0px" >
    <form id="form1" runat="server">
    <div align="center">
    
    <table cellspacing="0" cellpadding="0"><TBODY><tr><td valign="top">
        <dxnb:ASPxNavBar runat="server" AllowExpanding="False" ItemLinkMode="TextOnly" 
            ShowExpandButtons="False" SyncSelectionMode="None" GroupSpacing="15px" 
            EncodeHtml="False" Width="320px" Height="450px" 
            ID="nav001" EnableViewState="False" Target="_top">
<ClientSideEvents ItemClick="function(s, e) {
	
}"></ClientSideEvents>

<GroupHeaderStyle Font-Bold="True" ></GroupHeaderStyle>

<GroupContentStyle>
<Border BorderStyle="None"></Border>
</GroupContentStyle>
</dxnb:ASPxNavBar>

 </td><td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px">
            <dxnb:ASPxNavBar runat="server" 
                AllowExpanding="False" ItemLinkMode="TextOnly" ShowExpandButtons="False" 
                SyncSelectionMode="None" GroupSpacing="15px" EncodeHtml="False" Width="320px" 
                Height="450px"  ID="nav002" 
                EnableViewState="False" Target="_top">
<GroupHeaderStyle Font-Bold="True" ></GroupHeaderStyle>

<GroupContentStyle>
<Border BorderStyle="None"></Border>
</GroupContentStyle>
</dxnb:ASPxNavBar>

 </td><td valign="top"><dxnb:ASPxNavBar runat="server" AllowExpanding="False" 
                ItemLinkMode="TextOnly" ShowExpandButtons="False" SyncSelectionMode="None" 
                GroupSpacing="15px" EncodeHtml="False" Width="320px" Height="450px" 
                 ID="nav003" EnableViewState="False" 
                Target="_top">
<GroupHeaderStyle Font-Bold="True" ></GroupHeaderStyle>

<GroupContentStyle BackColor="Transparent">
<Border BorderStyle="None"></Border>
</GroupContentStyle>
</dxnb:ASPxNavBar>

 </td></tr></TBODY></table>
    
    </div>
    </form>
</body>
</html>
