<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="_Projetos_Boletim_index" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px;">
                            <dxe:ASPxLabel ID="lblTituloTelaOld" runat="server" 
                                ClientInstanceName="lblTituloTelaOld"  Text="Boletins da Unidade Selecionada">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 180px" valign="top">
                <dxnb:ASPxNavBar ID="nvbMenuProjeto" runat="server" Width="180px" ClientInstanceName="nvbMenuProjeto" GroupSpacing="3px"><Groups>
<dxnb:NavBarGroup Name="opOpcoes" Text="Opções">
<ItemStyle Font-Overline="False" Font-Underline="True"></ItemStyle>
</dxnb:NavBarGroup>
</Groups>

<ClientSideEvents ItemClick="function(s, e) 
{
    var textoItem = nvbMenuProjeto.GetItemText(e.item.group.index,e.item.index);
	lblTituloTela.SetText(traducao.index_relat_rios_de_projetos_da_unidade__ + nvbMenuProjeto.cp_titulo + ' - ' + textoItem);	
}" Init="function(s, e) 
{
    var textoItem = nvbMenuProjeto.GetItemText(0,0);
	lblTituloTela.SetText(traducao.index_relat_rios_de_projetos_da_unidade__ + nvbMenuProjeto.cp_titulo + ' - ' + textoItem);	
}"></ClientSideEvents>

<Paddings Padding="1px"></Paddings>
</dxnb:ASPxNavBar>
            </td>
            <td style="width: 5px">
            </td>
            <td valign="top">
                <dxp:ASPxPanel ID="ASPxPanel1" runat="server">
                    <BorderLeft BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                    <BorderTop BorderStyle="None" />
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <iframe frameborder="0" name="framePrincipal" scrolling="no" src="<%=telaInicial %>"
                                style="height: <%=alturaTabela %>; margin:0" width="100%" marginheight="0" id="frmBoletim"></iframe>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <BorderRight BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                    <BorderBottom BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                    <Paddings Padding="0px" />
                </dxp:ASPxPanel>
                <dxlp:ASPxLoadingPanel ID="lpAtualizando" runat="server" ClientInstanceName="lpAtualizando"
                     Modal="True" Text="Atualizando&hellip;">
                </dxlp:ASPxLoadingPanel>
            </td>
        </tr>
    </table>
    </asp:Content>