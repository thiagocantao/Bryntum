<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="index_prev.aspx.cs" Inherits="_Processos_Visualizacao_index_prev" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px;">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela" Text="Gestão dinâmica de processos e relatórios">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dx:ASPxSplitter ID="sp_Tela" runat="server" ClientInstanceName="sp_Tela" FullscreenMode="True" Width="100%" SeparatorSize="10px" Height="100%">
        <Panes>
            <dx:SplitterPane MaxSize="180px" MinSize="120px" ScrollBars="Auto" Size="165px" ShowCollapseBackwardButton="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxnb:ASPxNavBar ID="mvbMenu" runat="server" Width="100%" ClientInstanceName="mvbMenu" GroupSpacing="3px" AutoCollapse="True" AllowSelectItem="True">
                            <ClientSideEvents ItemClick="function(s, e) 
{
    lpAtualizando.Show();
    var textoItem = mvbMenu.GetItemText(e.item.group.index,e.item.index);
	lblTituloTela.SetText(lblTituloTela.GetText().split('-')[0] + ' - ' + textoItem);	
	e.processOnServer = false;
}"
                                Init="function(s, e) 
{
    var textoItem = mvbMenu.GetItemText(0,0);
	lblTituloTela.SetText(lblTituloTela.GetText().split('-')[0] + ' - ' + textoItem);	
}"></ClientSideEvents>
                            <Paddings Padding="1px"></Paddings>
                            <GroupHeaderStyle Wrap="True">
                            </GroupHeaderStyle>
                        </dxnb:ASPxNavBar>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ContentUrl="" ContentUrlIFrameName="framePrincipal" ScrollBars="Auto" Name="paneContent">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxp:ASPxPanel ID="ASPxPanel1" runat="server">
                            <BorderLeft BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            <BorderTop BorderStyle="None" />
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                    <iframe frameborder="0" name="framePrincipal" scrolling="no" src="<%=telaInicial %>" style="height: <%=alturaTabela %>; margin: 0" width="100%" marginheight="0" id="frmBoletim"></iframe>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <BorderRight BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            <BorderBottom BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            <Paddings Padding="0px" />
                        </dxp:ASPxPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
        <ClientSideEvents Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);
    height = height - 120;
 	s.SetHeight(height);
}"
            PaneContentUrlLoaded="function(s, e) {
    lpAtualizando.Hide();
}" />
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
            </Pane>
        </Styles>
    </dx:ASPxSplitter>
    <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback">
    </dxcp:ASPxCallback>
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

    <dxlp:ASPxLoadingPanel ID="lpAtualizando" runat="server" ClientInstanceName="lpAtualizando" Modal="True" Text="Carregando" HorizontalAlign="Center" VerticalAlign="Middle">
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
                                        <ClientSideEvents BeginCallback="function(s, e) {
	comando = e.command;
}"
                                            EndCallback="function(s, e) {
            if(comando == 'DELETEROW')
            {
                     callbackBotoes.PerformCallback(comando);  
            }
}" />
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
                                    <dxtv:ASPxCallbackPanel ID="callbackBotoes" runat="server" ClientInstanceName="callbackBotoes" Width="100%" OnCallback="callbackBotoes_Callback">
                                        <PanelCollection>
                                            <dxtv:PanelContent runat="server">
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
		//window.top.mostraMensagem(traducao.index_nenhuma_consulta_foi_selecionada, 'atencao', true, false, null);
        CarregarConsulta(null);
        popup.HideWindow(winGerenciarConsultas);
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
                                            </dxtv:PanelContent>
                                        </PanelCollection>
                                    </dxtv:ASPxCallbackPanel>

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
    <!--
    <script src="../../scripts/jquery.ultima.js" type="text/javascript"></script>
    -->
    <script type="text/javascript">
        var winGerenciarConsultas = null;
        var winSalvarComo = null;

        function ExibeConsultaSalvas(codigoLista, codigoUsuario) {
            var parametro = codigoUsuario + ';' + codigoLista;
            popup.ShowWindow(winGerenciarConsultas);
            popup.PerformWindowCallback(winGerenciarConsultas, parametro);
        }

        function ExibirJanelaSalvarComo() {
            txtNomeConsulta.SetText('');
            popup.ShowWindow(winSalvarComo);
        }

        function SalvarConsultaComo(nomeConsulta) {
            var framePrincipal = window.frames['framePrincipal'];
            if (framePrincipal.SalvarConsultaComo)
                framePrincipal.SalvarConsultaComo(nomeConsulta);
        }

        function CarregarConsulta(codigoListaUsuario) {
            var framePrincipal = window.frames['framePrincipal'];
            if (framePrincipal.CarregarConsulta)
                framePrincipal.CarregarConsulta(codigoListaUsuario);
        }

        if (window.top.lpAguardeMasterPage)
            window.top.lpAguardeMasterPage.Hide();
    </script>
</asp:Content>
