<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="ListaProcessosGrupo.aspx.cs"
    Inherits="_Projetos_DadosProjeto_indexResumoProjeto" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
<script type="text/javascript">
function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 130;
    sp_Tela.SetHeight(height);
    nvbMenuFluxos.SetHeight(height - 35);
    document.getElementById('divGrid').style.visibility = ''
}
</script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr style="height: 26px;">
                        <td valign="middle" style="padding-left: 10px">
                            <table class="auto-style1">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="lblTituloTela" runat="server"
                                            Text="Fluxos de Processos">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">

                                        <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server"
                                            ClientInstanceName="txtPesquisa"
                                            NullText="Pesquisar por palavra chave em TODOS os fluxos..." Width="350px" Height="25px">
                                            <ClientSideEvents ButtonClick="function(s, e) {
	
	e.processOnServer = false; 
framePrincipal.location.href = 'listaProcessosInterno.aspx?Filtro=' + s.GetText();
}"
                                                KeyPress="function(s, e) {
	if(e.htmlEvent.keyCode == 13)
	{
		e.processOnServer = false; 
framePrincipal.location.href = 'listaProcessosInterno.aspx?Filtro=' + s.GetText();
ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
	}
}
" />
                                            <Buttons>
                                                <dxe:EditButton>
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Search"
                                                            HottrackedCssClass="Sprite_SearchHover" PressedCssClass="Sprite_SearchHover" />
                                                    </Image>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ButtonStyle CssClass="MailMenuSearchBoxButton">
                                                <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                            </ButtonStyle>
                                            <Paddings PaddingBottom="0px" PaddingTop="0px" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divGrid" style="visibility: hidden">
    <dx:ASPxSplitter ID="sp_Tela" runat="server" ClientInstanceName="sp_Tela"
        FullscreenMode="True" Height="100%" Width="100%" SeparatorSize="10px">
        <Panes>
            <dx:SplitterPane MaxSize="280px" MinSize="150px" ScrollBars="Auto"
                Size="265px" ShowCollapseBackwardButton="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxcp:ASPxCallbackPanel ID="callbackMenu" runat="server"
                            ClientInstanceName="callbackMenu" Width="100%">
                            <Paddings Padding="0px" />
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dxnb:ASPxNavBar ID="nvbMenuFluxos" runat="server"
                                        ClientInstanceName="nvbMenuFluxos"
                                        GroupSpacing="3px" Width="100%">
                                        <ClientSideEvents ItemClick="function(s, e) {
	txtPesquisa.SetText('');
}" />
                                        <Paddings Padding="0px" />
                                    </dxnb:ASPxNavBar>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ContentUrl="" Name="frameWf" ContentUrlIFrameName="framePrincipal"
                ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxp:ASPxPanel ID="ASPxPanel1" runat="server">
                            <Paddings Padding="0px" />
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                </dxp:PanelContent>
                            </PanelCollection>
                            <BorderLeft BorderStyle="None" />
                            <BorderTop BorderStyle="None" />
                            <BorderRight BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                        </dxp:ASPxPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
        <ClientSideEvents PaneCollapsed="function(s, e) {
	if(framePrincipal.reziseDiv)
		framePrincipal.reziseDiv();
}"
            PaneExpanded="function(s, e) {
	if(framePrincipal.reziseDiv)
		framePrincipal.reziseDiv();
}"
            PaneResizeCompleted="function(s, e) {
	if(framePrincipal.reziseDiv)
		framePrincipal.reziseDiv();
}" Init="function(s, e) {
	                                        AdjustSize();
}"/>
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
            </Pane>
        </Styles>
        <Images>
            <HorizontalSeparatorButton Height="8px" Width="8px">
            </HorizontalSeparatorButton>
            <VerticalCollapseBackwardButton Url="~/imagens/colapse.png"
                UrlHottracked="~/imagens/colapse.png">
            </VerticalCollapseBackwardButton>
            <VerticalCollapseForwardButton Url="~/imagens/expand.png"
                UrlHottracked="~/imagens/expand.png">
            </VerticalCollapseForwardButton>
            <HorizontalCollapseBackwardButton Height="8px" Width="8px">
            </HorizontalCollapseBackwardButton>
            <HorizontalCollapseForwardButton Height="8px" Width="8px">
            </HorizontalCollapseForwardButton>
        </Images>
    </dx:ASPxSplitter>
        </div>
</asp:Content>
