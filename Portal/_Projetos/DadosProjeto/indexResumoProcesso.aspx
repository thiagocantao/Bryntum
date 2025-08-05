<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="indexResumoProcesso.aspx.cs"
    Inherits="_Projetos_DadosProjeto_indexResumoProcesso" Title="Portal da Estratégia" %>
    <%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td style="width: 25px" align="center">
                <dxe:ASPxImage ID="imgMensagens" runat="server" ClientInstanceName="imgMensagens" ImageUrl="~/imagens/questao.gif"
                    Width="14px" ToolTip="Incluir uma nova mensagem">
                    <ClientSideEvents Click="function(s, e) 
{
	abreTelaNovaMensagem();	
}" />
                </dxe:ASPxImage>
                        </td>
                        <td style="width: 5px">
                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgMensagens" ImageUrl="~/imagens/botoes/btnPDF.png"
                    ToolTip="Resumo do projeto selecionado no formato PDF" ClientVisible="False">
                                <ClientSideEvents Click="function(s, e) 
{
	var codProj = hfGeral.Get('hfCodigoProjeto');
	var nomeProj = hfGeral.Get('hfNomeProjeto');


     var myObject = new Object();
     myObject.nomeProjeto = nomeProj; 


	
}" />
                            </dxe:ASPxImage>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" 
                                ClientInstanceName="lblTituloTela" Font-Bold="True"
                                 Text="Resumo Projeto">
                            </dxe:ASPxLabel>
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                </dxhf:ASPxHiddenField>
                            <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
	lpAtualizando.Hide();
	if(s.cp_status == 'ok')
       window.top.mostraMensagem(s.cp_result, 'sucesso', false, false, null);
    else
       window.top.mostraMensagem(s.cp_result, 'erro', true, false, null);
	frmDadosProjeto.window.location.reload();
}" BeginCallback="function(s, e) {
	lpAtualizando.Show();
}" />
                            </dxcb:ASPxCallback>
                        </td>
                        <td style="width: 25px">
                            <dxe:ASPxImage ID="imgAtualizar" runat="server" ImageUrl="~/imagens/atualizar.PNG"  ToolTip="Atualizar o Projeto">
                                <ClientSideEvents Click="function(s, e) {
	confirmacao = confirm(&quot;Deseja Atualizar o Projeto?&quot;);
	if(confirmacao)
		callback.PerformCallback();
}" />
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 180px" valign="top">
                <dxnb:ASPxNavBar ID="nvbMenuProjeto" runat="server" Width="180px"  ClientInstanceName="nvbMenuProjeto" GroupSpacing="3px"><Groups>
<dxnb:NavBarGroup Name="opPrincipal" Text="Principal">
<ItemStyle Font-Overline="False" Font-Underline="True"></ItemStyle>
</dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opTempoEscopo" Text="Tempo e Escopo">
<ItemStyle Font-Underline="True"></ItemStyle>
</dxnb:NavBarGroup>
                    <dxnb:NavBarGroup Expanded="False" Name="opMenuFinanceiro" Text="Financeiro">
                    </dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opRiscosQuestos" Text="Riscos e Quest&#245;es"></dxnb:NavBarGroup>
                    <dxnb:NavBarGroup Expanded="False" Name="opContratos" Text="Contratos">
                    </dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opTarefas" Text="Tarefas"></dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opComunicacao" Text="Comunica&#231;&#227;o"></dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opFormularios" Text="Formul&#225;rios"></dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opFluxos" Text="A&#231;&#245;es"></dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opIndicadores" Text="Metas"></dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opParametros" Text="Par&#226;metros"></dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="opStatusReport" Text="Status Report"></dxnb:NavBarGroup>
</Groups>

<ClientSideEvents ItemClick="function(s, e) 
{
    var textoItem = nvbMenuProjeto.GetItemText(e.item.group.index,e.item.index);
	var tituloTela = '';
	var Thiswidth=screen.width - 80;
    var Thisheight=screen.height - 240;
	if(hfGeral.Contains('hfNomeProjeto'))
	{
		tituloTela = hfGeral.Get('hfNomeProjeto').toString() + ' - ' +  textoItem;
		var myArguments = new Object();
   		myArguments.param1 = hfGeral.Get('hfNomeProjeto').toString();

		if('Visualizar EAP' == textoItem)
		{
			if(window.hfGeral &amp;&amp; hfGeral.Contains('urlEAPvisualica'))
			{
				var urlItem = hfGeral.Get('urlEAPvisualica');
				myArguments.param2 = ' (VISUALIZA&#199;&#195;O) ';
				window.top.showModal(urlItem + '&amp;Altura=' + Thisheight, 'Visualização', Thiswidth, Thisheight, '', myArguments);
			}
		}
		else if('Editar EAP' == textoItem)
		{
			if(window.hfGeral &amp;&amp; hfGeral.Contains('urlEAPedicao'))
			{
				var urlItem = hfGeral.Get('urlEAPedicao');
				myArguments.param2 = '';
				window.top.showModal(urlItem + '&amp;Altura=' + Thisheight, 'Edição', Thiswidth, Thisheight, funcaoPosModal, myArguments);
			}
		}
	}

	lblTituloTela.SetText(tituloTela);	
}" ExpandedChanged="function(s, e) 
{

}" HeaderClick="function(s, e) 
{
 /*var textoGrupo = e.group.GetText();
lblTituloTela.SetText(&quot;Resumo Projeto - &quot; + textoGrupo);*/
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
                                style="height: <%=alturaTabela %>; margin:0" width="100%" marginheight="0" id="frmDadosProjeto"></iframe>
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


