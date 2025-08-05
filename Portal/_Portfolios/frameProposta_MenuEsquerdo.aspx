<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameProposta_MenuEsquerdo.aspx.cs" Inherits="_Portfolios_frameProposta_MenuEsquerdo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <title>Menu Propostas</title>
</head>
<body style="margin-left: 0px; margin-top:0px">
    <form id="form1" runat="server">
        <div style="padding-top:5px;padding-left:5px;">
            <dxnb:ASPxNavBar ID="navBarLateral" runat="server" CssFilePath="~/App_Themes/Glass/{0}/styles.css"
                CssPostfix="Glass" GroupSpacing="0px" ImageFolder="~/App_Themes/Glass/{0}/" Width="166px" >
                <CollapseImage Height="16px" Width="16px">
                </CollapseImage>
                <GroupContentStyle>
                    <Paddings PaddingBottom="2px" PaddingLeft="1px" PaddingRight="1px" PaddingTop="2px" />
                </GroupContentStyle>
                <Groups>
                    <dxnb:NavBarGroup Name="Proposta" Text="Proposta" Expanded="true">
                    </dxnb:NavBarGroup>
                </Groups>
                <ExpandImage Height="16px" Width="16px"></ExpandImage>
                <ClientSideEvents ItemClick="function(s, e) {
	parent.mudaTituloSelecao(e.item.GetText());
}" Init="function(s, e) {
	parent.mudaTituloSelecao('Principal');
}" />
            </dxnb:ASPxNavBar>
        </div>
    </form>
</body>
</html>
