<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_Dinamica.aspx.cs" Inherits="espacoTrabalho_vc_Dinamica" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxe:ASPxButton ID="btnNovo" runat="server" Text="Novo" Width="85px" AutoPostBack="False">
            <ClientSideEvents Click="function(s, e) {
	pCall.PerformCallback();
}" />
        </dxe:ASPxButton>
        <dxcp:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="459px" ClientInstanceName="pCall" OnCallback="ASPxCallbackPanel1_Callback">
            <PanelCollection>
                <dxp:PanelContent runat="server"><div runat="server" id="divNovo">
                    <dxp:ASPxPanel ID="ASPxPanel1" runat="server" Width="439px">
                        <panelcollection>
<dxp:PanelContent runat="server"></dxp:PanelContent>
</panelcollection>
                    </dxp:ASPxPanel>
                </div></dxp:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>        
        <table>
            <tr>
                <td id="tdFrame" align="center">
        <dxpc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True"
            AllowResize="True" Height="246px" ShowOnPageLoad="True" Width="350px" CloseAction="CloseButton" RenderIFrameForPopupElements="True" ContentUrl="../_Projetos/VisaoCorporativa/vc_008.aspx" ShowSizeGrip="True" PopupElementID="tdFrame" PopupAnimationType="None" EnableHotTrack="False" EnableViewState="False" HeaderText="Gráfico 1" ShowCloseButton="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                   </dxpc:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings Padding="1px" />
            </ContentStyle>
            <ClientSideEvents AfterResizing="function(s, e) {
	s.RefreshWindowContentUrl();
}" />
        </dxpc:ASPxPopupControl>
                </td>
                <td id="tdFrame2" align="center">
                    <dxpc:ASPxPopupControl ID="ASPxPopupControl2" runat="server" AllowDragging="True"
            AllowResize="True" Height="246px" ShowOnPageLoad="True" Width="350px" CloseAction="CloseButton" RenderIFrameForPopupElements="True" ContentUrl="../_Projetos/VisaoCorporativa/vc_008.aspx" ShowSizeGrip="True" PopupElementID="tdFrame2" ResizingMode="Postponed" EnableViewState="False" HeaderText="Gráfico 2" ShowCloseButton="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                   </dxpc:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings Padding="1px" />
            </ContentStyle>
            <ClientSideEvents Resize="function(s, e) {
	s.RefreshWindowContentUrl();
}" />
        </dxpc:ASPxPopupControl></td>
                <td id="tdFrame3" align="center">
                    <dxpc:ASPxPopupControl ID="ASPxPopupControl3" runat="server" AllowDragging="True"
            AllowResize="True" Height="246px" ShowOnPageLoad="True" Width="350px" CloseAction="CloseButton" RenderIFrameForPopupElements="True" ContentUrl="../_Projetos/VisaoCorporativa/vc_008.aspx" ShowSizeGrip="True" PopupElementID="tdFrame3" ResizingMode="Postponed" EnableViewState="False" HeaderText="Gráfico 3" ShowCloseButton="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                   </dxpc:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings Padding="1px" />
            </ContentStyle>
            <ClientSideEvents Resize="function(s, e) {
	s.RefreshWindowContentUrl();
}" />
        </dxpc:ASPxPopupControl></td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
