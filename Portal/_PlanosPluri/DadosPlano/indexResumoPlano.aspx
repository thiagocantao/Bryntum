<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="indexResumoPlano.aspx.cs"
    Inherits="_Projetos_DadosProjeto_indexResumoProjeto" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script language="javascript" type="text/javascript">
    var urlStatus = '';

</script>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td style="width: 25px" align="center">
                            &nbsp;</td>
                        <td style="width: 5px">
                            &nbsp;</td>
                        <td>
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" 
                                ClientInstanceName="lblTituloTela" Font-Bold="True"
                                 Text="Resumo Projeto">
                            </dxe:ASPxLabel>
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                </dxhf:ASPxHiddenField>
                        </td>
                        <td style="width: 25px;padding-right:13px">
                                                    <dxcp:ASPxCallbackPanel ID="cbImagemAjuda" ClientInstanceName="cbImagemAjuda" runat="server" Width="100%" OnCallback="cbImagemAjuda_Callback" Height="16px"        >
                                                        <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="" />
                                                        <PanelCollection>
<dxcp:PanelContent runat="server"><dxe:ASPxImage ID="imgAjudaGlossarioTipoProjeto" ClientInstanceName="imgAjudaGlossarioTipoProjeto" runat="server" ImageUrl="~/imagens/ajuda.png"  ToolTip="Ajuda sobre este tópico" OnInit="imgAjudaGlossarioTipoProjeto_Init">
                                <ClientSideEvents Click="function(s, e) {
//debugger
var codigoGlossario = s.cp_CodigoGlossario;
var url = &quot;../../HelpFuncionalidade.aspx?CG=&quot; + codigoGlossario;
var diferencaAltura = (window.outerHeight - window.innerHeight);
var diferencaLargura =  (window.outerWidth - window.innerWidth);
if(diferencaLargura == 0)
{
diferencaLargura = 50;
}
var altura = (window.innerHeight - (diferencaAltura * 1.03));
var largura = (window.innerWidth - (diferencaLargura * 1.04));
window.top.showModal(url , 'Ajuda', largura, altura, null, null);
}" />
                            </dxe:ASPxImage></dxcp:PanelContent>
</PanelCollection>
                                                    </dxcp:ASPxCallbackPanel>
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dx:ASPxSplitter ID="sp_Tela" runat="server" ClientInstanceName="sp_Tela" 
        FullscreenMode="True" Height="100%" Width="100%" SeparatorSize="10px">
        <Panes>
            <dx:SplitterPane MaxSize="180px" MinSize="100px" ScrollBars="Auto" 
                Size="165px" ShowCollapseBackwardButton="True">
                <ContentCollection>
<dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
    <dxcp:ASPxCallbackPanel ID="callbackMenu" runat="server" 
        ClientInstanceName="callbackMenu" OnCallback="callbackMenu_Callback" 
         >
<SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>

        <Paddings Padding="0px" />
        <PanelCollection>
            <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dxnb:ASPxNavBar ID="nvbMenuProjeto" runat="server" 
                    ClientInstanceName="nvbMenuProjeto"  
                    GroupSpacing="3px" Width="100%">
                    <ClientSideEvents ExpandedChanged="function(s, e) 
{

}" HeaderClick="function(s, e) 
{
 /*var textoGrupo = e.group.GetText();
lblTituloTela.SetText(&quot;Plano Plurianual - &quot; + textoGrupo);*/
}" ItemClick="function(s, e) 
{
    textoItem = nvbMenuProjeto.GetItemText(e.item.group.index,e.item.index);
	var tituloTela = '';
	var Thiswidth=screen.width - 80;
    var Thisheight=screen.height - 240;
    
    urlStatus = nvbMenuProjeto.GetItemNavigateUrl(e.item.group.index,e.item.index);
	

	if(hfGeral.Contains('hfNomePlano'))
	{
		tituloTela = hfGeral.Get('hfNomePlano').toString() + ' - ' +  textoItem;
		var myArguments = new Object();
   		myArguments.param1 = hfGeral.Get('hfNomePlano').toString();
        pnCarregarClick.Show();
	}

	lblTituloTela.SetText(tituloTela);
    var urlDoItem = e.item.GetNavigateUrl();
    
    cbImagemAjuda.PerformCallback(urlDoItem);		
}"  />
                    <Groups>
                        <dxnb:NavBarGroup Name="opPrincipal" Text="Principal">
                            <ItemStyle Font-Overline="False" Font-Underline="True" />
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Expanded="False" Name="opTempoEscopo" Text="Tempo e Escopo">
                            <ItemStyle Font-Underline="True" />
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Expanded="False" Name="opMenuFinanceiro" Text="Financeiro">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Name="opRiscosQuestos" Text="Riscos e Questões">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Name="opContratos" Text="Contratos">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Expanded="False" Name="opTarefas" Text="Tarefas">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Expanded="False" Name="opComunicacao" Text="Comunicação">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Expanded="False" Name="opFormularios" Text="Formulários">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Name="opFluxos" Text="Ações">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Name="opIndicadores" Text="Metas">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Expanded="False" Name="opParametros" Text="Parâmetros">
                        </dxnb:NavBarGroup>
                        <dxnb:NavBarGroup Expanded="False" Name="opStatusReport" Text="Status Report">
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
            <dx:SplitterPane ContentUrl="" ContentUrlIFrameName="framePrincipal" 
                ScrollBars="Auto">
                <ContentCollection>
<dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
    <dxp:ASPxPanel ID="ASPxPanel1" runat="server">
        <Paddings Padding="0px" />
        <PanelCollection>
            <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                               
            </dxp:PanelContent>
        </PanelCollection>
        <BorderLeft BorderStyle="None"  />
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
}" PaneExpanded="function(s, e) {
	if(framePrincipal.reziseDiv)
		framePrincipal.reziseDiv();
}" PaneResizeCompleted="function(s, e) {
	if(framePrincipal.reziseDiv)
		framePrincipal.reziseDiv();
}" PaneContentUrlLoaded="function(s, e) {
	pnCarregarClick.Hide();
}" />
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
    </asp:Content>
