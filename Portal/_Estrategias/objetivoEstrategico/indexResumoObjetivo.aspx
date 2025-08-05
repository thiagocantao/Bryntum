<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="indexResumoObjetivo.aspx.cs"
    Inherits="_Projetos_DadosProjeto_indexResumoProjeto" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="height: 26px">
                        <td valign="middle" style="padding-left: 10px; width: 25px;">
                            <dxe:ASPxImage ID="imgMensagens" runat="server" ClientInstanceName="imgMensagens"
                                ImageUrl="~/imagens/questao.gif" ToolTip="<%$ Resources:traducao, indexResumoObjetivo_incluir_uma_nova_mensagem %>" Width="14px" Cursor="pointer">
                                <ClientSideEvents Click="function(s, e) 
{
	var codOE = hfGeral.Get('hfCodigoObjetivo');
	var nomeOE = hfGeral.Contains('hfNomeObjetivo') ? hfGeral.Get('hfNomeObjetivo'): &quot;&quot;;
      var myObject = new Object();
     myObject.nomeProjeto = nomeOE; 

    window.top.showModal('../../Mensagens/EnvioMensagens.aspx?CO=' + codOE + '&TA=OB', traducao.indexResumoObjetivo_nova_mensagem___ + nomeOE, 950, 510, '', myObject);

}" />
                            </dxe:ASPxImage>
                        </td>
                        <td valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" ToolTip="" runat="server" ClientInstanceName="lblTituloTela"
                                Font-Bold="True"
                                Text="Objetivo Estratégico">
                                <DisabledStyle Font-Size="15px">
                                </DisabledStyle>
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="middle" style="padding-right: 5px;">
                            <dxe:ASPxLabel ID="lblEntidade" runat="server"
                                Text="Unidade de Negócio:" ClientInstanceName="lblEntidade">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="middle" style="padding-right: 4px;">
                            <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                                Width="100%" ValueType="System.Int32"
                                TextFormatString="{0}" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetValue() == s.cp_UNL)
		lblEntidadeDiferente.SetText('');
	else
		lblEntidadeDiferente.SetText('*' + traducao.voc__est__visualizando_as_informa__es_da_entidade + ': ' + s.GetText());
}"
                                    Init="function(s, e) {
	if(s.GetValue() == s.cp_UNL)
		lblEntidadeDiferente.SetText('');
	else
		lblEntidadeDiferente.SetText('*' + traducao.voc__est__visualizando_as_informa__es_da_entidade + ': ' + s.GetText());
}"></ClientSideEvents>
                                <Columns>
                                    <dxe:ListBoxColumn Name="siglaUnidade" Width="160px" Caption="Sigla Unidade"></dxe:ListBoxColumn>
                                    <dxe:ListBoxColumn Name="nomeUnidade" Width="650px" Caption="Nome Unidade"></dxe:ListBoxColumn>
                                </Columns>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">

        var urlStatus = '';

    </script>

    <style>
        html #nvbMenuProjeto_GHE0 {
            width: 100% !important;
        }

        html #nvbMenuProjeto_GHC1 {
            width: 100% !important;
        }
        /*html #sp_Tela_0_CC{
	        width:217px !important;
        }

        html #nvbMenuProjeto{
            width: 173px !important;
        }*/
        /*html #nvbMenuProjeto_GC0{
            width: 195px !important;
        }*/
    </style>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td valign="top">
                <dx:ASPxSplitter ID="sp_Tela" runat="server" ClientInstanceName="sp_Tela"
                    FullscreenMode="True" Height="100%" Width="100%" SeparatorSize="10px">
                    <Panes>
                        <dx:SplitterPane MaxSize="180px" MinSize="100px" ScrollBars="Auto"
                            Size="165px" ShowCollapseBackwardButton="True">
                            <ContentCollection>
                                <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dxcp:ASPxCallbackPanel ID="callbackMenu" runat="server"
                                        ClientInstanceName="callbackMenu">
                                        <Paddings Padding="0px" />
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                <dxnb:ASPxNavBar ID="nvbMenuProjeto" runat="server"
                                                    ClientInstanceName="nvbMenuProjeto"
                                                    GroupSpacing="3px" Width="100%">
                                                    <ClientSideEvents ItemClick="function(s, e) 
{	
    textoItem = nvbMenuProjeto.GetItemText(e.item.group.index,e.item.index);
	var tituloTela = '';
	var Thiswidth=screen.width - 80;
    var Thisheight=screen.height - 240;
    
    urlStatus = nvbMenuProjeto.GetItemNavigateUrl(e.item.group.index,e.item.index);
	    
	if(hfGeral.Contains('hfNomeObjetivo'))
	{
		tituloTela = hfGeral.Get('hfNomeObjetivo').toString() + ' - ' +  textoItem;
		var myArguments = new Object();
   		myArguments.param1 = hfGeral.Get('hfNomeObjetivo').toString();
        		
		pnCarregarClick.Show();
	}

	lblTituloTela.SetText(tituloTela);	
}" />
                                                    <Groups>
                                                        <dxnb:NavBarGroup Name="Moe" Text="Principal">
                                                            <Items>
                                                                <dxnb:NavBarItem Name="Det" Text="Detalhes"></dxnb:NavBarItem>
                                                                <dxnb:NavBarItem Name="Ana" Text="Análises"></dxnb:NavBarItem>
                                                                <dxnb:NavBarItem Name="Tdl" Text="Planos de Ação" Visible="False"></dxnb:NavBarItem>
                                                                <dxnb:NavBarItem Name="Est" Text="Estratégias"></dxnb:NavBarItem>
                                                                <dxnb:NavBarItem Name="Asu" Text="Ações Sugeridas"></dxnb:NavBarItem>
                                                            </Items>
                                                        </dxnb:NavBarGroup>
                                                        <dxnb:NavBarGroup Expanded="False" Name="Com" Text="Comunicação">
                                                            <Items>
                                                                <dxnb:NavBarItem Name="Ane" Text="Anexos"></dxnb:NavBarItem>
                                                                <dxnb:NavBarItem Name="Men" Text="Mensagens"></dxnb:NavBarItem>
                                                            </Items>
                                                        </dxnb:NavBarGroup>
                                                        <dxnb:NavBarGroup Expanded="False" Name="Rel" Text="Relatórios">
                                                            <Items>
                                                                <dxnb:NavBarItem Name="Mel" Text="Análise de Melhores Práticas"></dxnb:NavBarItem>
                                                                <dxnb:NavBarItem Name="AnaliseIni" Text="Análise de Iniciativas" Visible="False"></dxnb:NavBarItem>
                                                                <dxtv:NavBarItem Name="AnInd" Text="Análise de Indicadores" ToolTip="Análise de Indicadores">
                                                                </dxtv:NavBarItem>
                                                                <dxtv:NavBarItem Name="AnFin" Text="Análise Financeira" ToolTip="Análise Financeira">
                                                                </dxtv:NavBarItem>
                                                            </Items>
                                                        </dxnb:NavBarGroup>
                                                    </Groups>
                                                    <Paddings Padding="0px" />
                                                </dxnb:ASPxNavBar>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane ContentUrl="" ContentUrlIFrameName="oe_desktop"
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
	if(oe_desktop.reziseDiv)
		oe_desktop.reziseDiv();
}"
                        PaneExpanded="function(s, e) {
	if(oe_desktop.reziseDiv)
		oe_desktop.reziseDiv();
}"
                        PaneResizeCompleted="function(s, e) {
	if(oe_desktop.reziseDiv)
		oe_desktop.reziseDiv();
}"
                        PaneContentUrlLoaded="function(s, e) {
	pnCarregarClick.Hide();
}" Init="function(s, e) {
s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 130);
}" />
                    <Styles>
                        <Pane>
                            <Paddings Padding="0px"/>
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
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblEntidadeDiferente" runat="server"
                    ClientInstanceName="lblEntidadeDiferente" Font-Bold="True" Font-Italic="True"
                    ForeColor="Red">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <dxlp:ASPxLoadingPanel ID="lpAtualizando" runat="server"
        ClientInstanceName="lpAtualizando"
        Modal="True" Text="Atualizando...">
    </dxlp:ASPxLoadingPanel>
    <dxlp:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server"
        ClientInstanceName="pnCarregarClick" HorizontalAlign="Center"
        HorizontalOffset="40" Text="" VerticalAlign="Middle">
        <Image Url="~/imagens/carregando.gif">
        </Image>
    </dxlp:ASPxLoadingPanel>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
</asp:Content>
