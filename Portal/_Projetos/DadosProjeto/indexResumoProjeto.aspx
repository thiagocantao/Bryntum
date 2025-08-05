<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="indexResumoProjeto.aspx.cs"
    Inherits="_Projetos_DadosProjeto_indexResumoProjeto" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgMensagens"
                                ImageUrl="~/imagens/botoes/btnPDF.png"
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
       if(s.cp_result != '')
      {
                  window.top.mostraMensagem(s.cp_result, 'sucesso', false, false, null);
      }
      else if(s.cp_Erro != '')
     {
                  window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
      }
      framePrincipal.window.location.reload();
}"
                                    BeginCallback="function(s, e) {
	lpAtualizando.Show();
}" />
                            </dxcb:ASPxCallback>
                        </td>
                        <td style="width: 275px;" id="tdFinanceiro">
                            <dxp:ASPxPanel ID="pnFinanceiro" runat="server"
                                ClientInstanceName="pnFinanceiro" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblFinanceiro" runat="server" EnableViewState="False"
                                                        Text="Financeiro:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="padding-right: 10px; width: 100px">
                                                    <dxe:ASPxComboBox ID="ddlFinanceiro" runat="server"
                                                        ClientInstanceName="ddlFinanceiro" EnableViewState="False"
                                                        SelectedIndex="1" Width="100%">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Total" Value="T" />
                                                            <dxe:ListEditItem Text="Ano Atual" Value="A" Selected="True" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"
                                                        EnableViewState="False" Text="Selecionar">
                                                        <ClientSideEvents Click="function(s, e) 
{
	var url = s.cp_URL + '&amp;Financeiro=' + ddlFinanceiro.GetValue();
	sp_Tela.GetPane(1).SetContentUrl(url, false);
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </td>
                        <td style="width: 25px">
                            <table cellpadding="0" cellspacing="0" class="auto-style2">
                                <tr>
                                    <td style="padding-right: 5px">
                                        <dxe:ASPxImage ID="imgRedirecionar" runat="server" ImageUrl="~/imagens/left-arrow.png" ToolTip="Acessar o projeto ágil do sprint." ClientVisible="False" Height="20px" Width="20px">
                                            <ClientSideEvents Click="function(s, e) {
                                        //window.top.mostraMensagem(traducao.indexResumoProjeto_deseja_ir_para_o_projeto_agil_, 'confirmacao', true, true, redirecionaProjeto);
                                    redirecionaProjeto();
}" />
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxImage ID="imgAtualizar" runat="server" ImageUrl="~/imagens/atualizar.PNG" ToolTip="Atualizar o Projeto">
                                            <ClientSideEvents Click="function(s, e) {
                                        window.top.mostraMensagem(traducao.indexResumoProjeto_deseja_atualizar_o_projeto_, 'confirmacao', true, true, atualizaProjeto);
}" />
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 25px; padding-right: 13px">
                            <dxcp:ASPxCallbackPanel ID="cbImagemAjuda" ClientInstanceName="cbImagemAjuda" runat="server" Width="100%" OnCallback="cbImagemAjuda_Callback" Height="16px">
                                <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="" />
                                <PanelCollection>
                                    <dxcp:PanelContent runat="server">
                                        <dxe:ASPxImage ID="imgAjudaGlossarioTipoProjeto" ClientInstanceName="imgAjudaGlossarioTipoProjeto" runat="server" ImageUrl="~/imagens/ajuda.png" ToolTip="Ajuda sobre este tópico" OnInit="imgAjudaGlossarioTipoProjeto_Init">
                                            <ClientSideEvents Click="function(s, e) {
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
                                        </dxe:ASPxImage>
                                    </dxcp:PanelContent>
                                </PanelCollection>
                            </dxcp:ASPxCallbackPanel>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        var urlStatus = '';

    </script>
    <script language="javascript" type="text/javascript">
        function reziseDiv() {
            if (window.parent.sp_Tela) {
                pnDiv.SetWidth('100%');
            }
        }
</script>

    <style>
        html #sp_Tela_0_CC{
	        width:260px !important;
        }
    </style>


    <div id="divGrid" style="visibility: hidden">
    <dx:ASPxSplitter ID="sp_Tela" runat="server" ClientInstanceName="sp_Tela" Height="100%" Width="100%" SeparatorSize="10px" SaveStateToCookies="True">
        <Panes>
            <dx:SplitterPane MaxSize="300px" MinSize="200px" ScrollBars="Auto"
                Size="300px" ShowCollapseBackwardButton="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxcp:ASPxCallbackPanel ID="callbackMenu" runat="server"
                            ClientInstanceName="callbackMenu" OnCallback="callbackMenu_Callback" Width="100%">
                            <SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>

                            <Paddings Padding="0px" />
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dxnb:ASPxNavBar ID="nvbMenuProjeto" runat="server"
                                        ClientInstanceName="nvbMenuProjeto"
                                        GroupSpacing="3px" Width="100%">
                                        <ClientSideEvents 
                                            HeaderClick="function(s, e) 
{
 /*var textoGrupo = e.group.GetText();
lblTituloTela.SetText(&quot;Resumo Projeto - &quot; + textoGrupo);*/
}"
                                            ItemClick="function(s, e) 
{
	ddlFinanceiro.SetValue('A');
    textoItem = nvbMenuProjeto.GetItemText(e.item.group.index,e.item.index);
	var tituloTela = '';
	var Thiswidth=screen.width - 80;
    var Thisheight=screen.height - 240;
    
    urlStatus = nvbMenuProjeto.GetItemNavigateUrl(e.item.group.index,e.item.index);
	
    if(urlStatus.indexOf('ResumoProjeto.aspx') != -1)
        pnFinanceiro.SetVisible(true);
    else
        pnFinanceiro.SetVisible(false);

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
				myArguments.param2 = ' (VISUALIZAÇÃO) ';
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
		else
			pnCarregarClick.Show();
	}

	lblTituloTela.SetText(tituloTela);
    var urlDoItem = e.item.GetNavigateUrl();
    if(urlDoItem.indexOf('propostaDeIniciativa_009') != -1)
    {
        pnCarregarClick.Hide();
    } 
cbImagemAjuda.PerformCallback(urlDoItem);		
}"
                                            Init="function(s, e) {
	                                        AdjustSize();
                                            urlStatus = nvbMenuProjeto.GetItemNavigateUrl(0,0);
    if(urlStatus.indexOf('ResumoProjeto.aspx') != -1)
        pnFinanceiro.SetVisible(true);
    else
        pnFinanceiro.SetVisible(false);
}" />
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
AdjustSize(); 
}"
            PaneExpanded="function(s, e) {
	//if(framePrincipal.reziseDiv)
	//	framePrincipal.reziseDiv();
}"
            PaneResizeCompleted="function(s, e) {
	//if(framePrincipal.reziseDiv)
	//	framePrincipal.reziseDiv();
}"
            PaneContentUrlLoaded="function(s, e) {
	pnCarregarClick.Hide();
}" 
                                    Init="function(s, e) 
                                                    {
                                                    AdjustSize();
                                                    document.getElementById('divGrid').style.visibility = '';
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
    <dxcp:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" ClientInstanceName="popup" CloseAction="CloseButton" CloseAnimationType="Fade" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" OnWindowCallback="popup_WindowCallback">
        <Windows>
            <dxtv:PopupWindow HeaderText="Consultas" Name="winGerenciarConsultas" Width="600px" Modal="True">
                <ContentCollection>
                    <dxtv:PopupControlContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dxtv:ASPxGridView ID="gvConsultas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvConsultas" DataSourceID="sdsConsultas" KeyFieldName="CodigoListaUsuario" Width="100%" OnCellEditorInitialize="gvConsultas_CellEditorInitialize" OnCommandButtonInitialize="gvConsultas_CommandButtonInitialize">
                                        <Columns>
                                            <dxtv:GridViewDataTextColumn Caption="Lista" FieldName="NomeListaUsuario" ShowInCustomizationForm="True" VisibleIndex="2" Width="230px">
                                                <PropertiesTextEdit MaxLength="255">
                                                </PropertiesTextEdit>
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoListaUsuario" ReadOnly="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                <EditFormSettings Visible="False" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataCheckColumn Caption="Padrão" FieldName="IndicaListaPadrao" ShowInCustomizationForm="True" VisibleIndex="3" Width="130px">
                                                <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                </PropertiesCheckEdit>
                                            </dxtv:GridViewDataCheckColumn>
                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="   " ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="130px">
                                            </dxtv:GridViewCommandColumn>
                                            <dxtv:GridViewCommandColumn Caption="  " ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="45px">
                                            </dxtv:GridViewCommandColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                                        <SettingsEditing Mode="Inline">
                                        </SettingsEditing>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                                        <SettingsText ConfirmDelete="Deseja excluir a consulta?" />
                                        <SettingsCommandButton>
                                            <UpdateButton RenderMode="Image" Text="Salvar">
                                                <Image AlternateText="Salvar" ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                </Image>
                                            </UpdateButton>
                                            <CancelButton RenderMode="Image" Text="Cancelar">
                                                <Image AlternateText="Cancelar" ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                </Image>
                                            </CancelButton>
                                            <EditButton RenderMode="Image" Text="Editar">
                                                <Image AlternateText="Editar" ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </EditButton>
                                            <DeleteButton RenderMode="Image" Text="Excluir">
                                                <Image AlternateText="Excluir" ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </DeleteButton>
                                        </SettingsCommandButton>
                                    </dxtv:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="margin: 10px 0 5px auto;">
                                        <tr>
                                            <td>
                                                <dxtv:ASPxButton ID="btnConfirmar_SelecaoConsultas" runat="server" ClientInstanceName="btnConfirmar_SelecaoConsultas" Text="Confirmar" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
    	var keys = gvConsultas.GetSelectedKeysOnPage();
   	 if(keys.length &gt; 0){
        		CarregarConsulta(keys[0]);
	   	 popup.HideWindow(winGerenciarConsultas);
   	 }
	else{
		window.top.mostraMensagem(traducao.index_nenhuma_consulta_foi_selecionada, 'atencao', true, false, null);
	}
}" />
                                                </dxtv:ASPxButton>
                                            </td>
                                            <td>
                                                <dxtv:ASPxButton ID="btnCancelar_SelecaoConsultas" runat="server" ClientInstanceName="btnCancelar_SelecaoConsultas" Text="Cancelar" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	popup.HideWindow(winGerenciarConsultas);
}" />
                                                </dxtv:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxtv:PopupControlContentControl>
                </ContentCollection>
            </dxtv:PopupWindow>
            <dxtv:PopupWindow HeaderText="Salvar como" Name="winSalvarComo" Width="400px" Modal="True">
                <ContentCollection>
                    <dxtv:PopupControlContentControl runat="server">
                        <table>
                            <tr>
                                <td>

                                    <dxtv:ASPxTextBox ID="txtNomeConsulta" runat="server" ClientInstanceName="txtNomeConsulta" Width="350px">
                                    </dxtv:ASPxTextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="margin: 10px 0 5px auto;">
                                        <tr>
                                            <td>
                                                <dxtv:ASPxButton ID="btnConfirmar_SalvarComo" runat="server" ClientInstanceName="btnConfirmar_SalvarComo" Text="Confirmar" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	var nomeConsulta = txtNomeConsulta.GetText().replace(/'/g, &quot;''&quot;);
	if(nomeConsulta == null || nomeConsulta.trim() == ''){
		window.top.mostraMensagem(traducao.index_informe_o_nome_da_consluta, 'atencao', true, false, null);
	}
	else{
		SalvarConsultaComo(nomeConsulta);
		popup.HideWindow(winSalvarComo);
	}
}" />
                                                </dxtv:ASPxButton>
                                            </td>
                                            <td>
                                                <dxtv:ASPxButton ID="btnCancelar_SalvarComo" runat="server" ClientInstanceName="btnCancelar_SalvarComo" Text="Cancelar" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	popup.HideWindow(winSalvarComo);
}" />
                                                </dxtv:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxtv:PopupControlContentControl>
                </ContentCollection>
            </dxtv:PopupWindow>
        </Windows>
        <ClientSideEvents Init="function(s, e) {	
        winGerenciarConsultas = popup.GetWindowByName('winGerenciarConsultas');
        winSalvarComo = popup.GetWindowByName('winSalvarComo');
}" />
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
    <asp:SqlDataSource runat="server" DeleteCommand="DELETE FROM [ListaCampoUsuario] WHERE [CodigoListaUsuario] = @CodigoListaUsuario

DELETE FROM [ListaUsuario] WHERE [CodigoListaUsuario] = @CodigoListaUsuario"
        InsertCommand="INSERT INTO [ListaUsuario] 
([NomeListaUsuario], [CodigoUsuario], [CodigoLista], [IndicaListaPadrao]) 
VALUES 
(@NomeListaUsuario, @CodigoUsuario, @CodigoLista, (SELECT (CASE WHEN EXISTS(SELECT 1 FROM [ListaUsuario] WHERE [CodigoUsuario] = @CodigoUsuario AND [CodigoLista] = @CodigoLista) THEN 'N' ELSE 'S' END)))"
        SelectCommand="SELECT [NomeListaUsuario], [IndicaListaPadrao], [CodigoListaUsuario] FROM [ListaUsuario] WHERE (([CodigoUsuario] = @CodigoUsuario) AND ([CodigoLista] = @CodigoLista)) ORDER BY [NomeListaUsuario]"
        UpdateCommand="IF @IndicaListaPadrao = &#39;S&#39;
BEGIN
    UPDATE [ListaUsuario] 
            SET [IndicaListaPadrao] = &#39;N&#39; 
     WHERE [CodigoUsuario] = @CodigoUsuario
          AND [CodigoLista] = @CodigoLista
END

    UPDATE [ListaUsuario] 
            SET [NomeListaUsuario] = @NomeListaUsuario, 
                   [IndicaListaPadrao] = @IndicaListaPadrao 
     WHERE [CodigoListaUsuario] = @CodigoListaUsuario"
        ID="sdsConsultas">
        <DeleteParameters>
            <asp:Parameter Name="CodigoListaUsuario" Type="Int64"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="NomeListaUsuario" Type="String"></asp:Parameter>
            <asp:SessionParameter Name="CodigoUsuario" SessionField="codUsuario" Type="Int32" />
            <asp:SessionParameter Name="CodigoLista" SessionField="codLista" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter SessionField="codUsuario" Name="CodigoUsuario" Type="Int32"></asp:SessionParameter>
            <asp:SessionParameter SessionField="codLista" Name="CodigoLista" Type="Int32"></asp:SessionParameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="NomeListaUsuario"></asp:Parameter>
            <asp:Parameter Name="IndicaListaPadrao"></asp:Parameter>
            <asp:Parameter Name="CodigoListaUsuario"></asp:Parameter>
            <asp:SessionParameter SessionField="codUsuario" Name="CodigoUsuario"></asp:SessionParameter>
            <asp:SessionParameter SessionField="codLista" Name="CodigoLista"></asp:SessionParameter>
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style2 {
            width: 35px;
        }
    </style>
</asp:Content>

