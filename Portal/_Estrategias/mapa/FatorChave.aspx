<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="FatorChave.aspx.cs" Inherits="_Estrategias_mapa_FatorChave" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
<table>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False"
                                Font-Strikeout="False"></asp:Label></td>
                        <td style="width: 5px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    <div style="padding:2px">
    
        <dxtc:ASPxPageControl ID="tabControl" runat="server" ActiveTabIndex="0" 
            ClientInstanceName="tabControl"  
            Width="100%">
            <TabPages>
                <dxtc:TabPage Name="tbMacrometa" Text="Macrometa">
                    <ContentCollection>
                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="tbQuadroSintese" Text="Quadro Síntese">
                    <ContentCollection>
                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
            </TabPages>
            <ContentStyle>
                <Paddings Padding="0px" />
            </ContentStyle>
        </dxtc:ASPxPageControl>
    
    </div>
</asp:Content>
