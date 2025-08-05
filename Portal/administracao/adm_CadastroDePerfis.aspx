<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/novaCdis.master"
    AutoEventWireup="true" CodeFile="adm_CadastroDePerfis.aspx.cs" Inherits="administracao_adm_CadastroDePerfis"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <!-- table Principal -->
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px;">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server"
                                Text="Perfis de acesso aos fluxos" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!-- table Conteudo -->
    <table border="0" cellpadding="0" cellspacing="0" width="100%">

        <tr>
            <td id="ConteudoPrincipal">

                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoPerfilWf"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnCustomButtonCallback="gvDados_CustomButtonCallback" OnBeforeColumnSortingGrouping="gvDados_BeforeColumnSortingGrouping" OnCustomCallback="gvDados_CustomCallback">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) {
	s.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
	 }
     else if(e.buttonID == &quot;btnEditar&quot;)
     {	
		lpLoading.Show();
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
        s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoPerfilWf;perfil;tipo;indTipoGrupo', MontaCamposFormulario);
	 }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
	 else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();

     }
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" Caption="A&#231;&#227;o"
                                        VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnNovo" Visibility="Invisible" Text="Novo">
                                                <Image Url="~/imagens/botoes/novoReg.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Apagar">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Mostrar detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
                                                            <Paddings Padding="0px" />
                                                            <Items>
                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                    <Items>
                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                            <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                    <Items>
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                            <Image IconID="actions_reset_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                            </Items>
                                                            <ItemStyle Cursor="pointer">
                                                                <HoverStyle>
                                                                    <border borderstyle="None" />
                                                                </HoverStyle>
                                                                <Paddings Padding="0px" />
                                                            </ItemStyle>
                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                <SelectedStyle>
                                                                    <border borderstyle="None" />
                                                                </SelectedStyle>
                                                            </SubMenuItemStyle>
                                                            <Border BorderStyle="None" />
                                                        </dxm:ASPxMenu>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoPerfilWf" Name="CodigoPerfilWf" Visible="False"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="perfil" Name="perfil" Caption="Nome do Perfil"
                                        VisibleIndex="2">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="tipo" Name="tipo" Width="150px" Visible="False"
                                        VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="indTipoGrupo" Name="indTipoGrupo" Width="150px"
                                        Caption="Tipo de Perfil" VisibleIndex="4">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm">
                                </SettingsEditing>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                        AllowResize="True" Width="600px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                </SettingsPopup>
                                <Settings ShowGroupButtons="False" ShowFooter="True" VerticalScrollBarMode="Visible"
                                    ShowGroupPanel="True"></Settings>
                                <SettingsText ConfirmDelete="Excluir perfil ?" PopupEditFormCaption="Perfis Notificados"></SettingsText>
                                <SettingsLoadingPanel Text="" ShowImage="False"></SettingsLoadingPanel>
                                <Templates>
                                    <FooterRow>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="grid-legendas-cor grid-legendas-cor-controlado-sistema"><span></span></td>
                                                    <td class="grid-legendas-label grid-legendas-label-controlado-sistema">
                                                        <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                            Text="<%# Resources.traducao.adm_CadastroDePerfis_perfis_controlados_pelo_sistema %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
         if(s.cpErro !== '')
         {
             window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
         }
         else if(s.cpSucesso !== '')
         {
            pcDados.Hide();
            gvDados.PerformCallback(s.cpCodigoSelecionado);
            window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, null, null, null);
        }
}" />
                </dxcp:ASPxCallback>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" HeaderText="Detalhes"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="715px" ID="pcDados" CloseAction="None">
                                <ClientSideEvents CloseUp="function(s, e) {
	e.processOnServer = false;
	pcDados_OnCloseUp(s,e);
}"
                                    PopUp="function(s, e) {
	e.processOnServer = false;
	pcDados_OnPopup(s,e);
}"></ClientSideEvents>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" border="0" style="width:100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Perfil:" ClientInstanceName="lblNomePerfil"
                                                                            ID="lblNomePerfil">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="width: 160px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtNomePerfil"
                                                                            ID="txtNomePerfil" MaxLength="50">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td valign="top">
                                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblTipoPerfil"
                                                                            ID="lblTipoPerfil">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr id="linhaSelecaoUsuarios">
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="height: 200px">
                                                                        <table cellspacing="0" cellpadding="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 310px; height: 16px">
                                                                                        <dxe:ASPxLabel runat="server" Text="Usu&#225;rios Dispon&#237;veis:" ClientInstanceName="lblPerfisDisponivel"
                                                                                            ID="lblPerfisDisponivel">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td style="width: 60px" align="center"></td>
                                                                                    <td style="width: 310px; height: 16px" align="left">
                                                                                        <dxe:ASPxLabel runat="server" Text="Usu&#225;rios Selecionados:" ClientInstanceName="lblPerfisSelecionado"
                                                                                            ID="lblPerfisSelecionado">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 184px" valign="top">
                                                                                        <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" SelectionMode="CheckColumn" ClientInstanceName="lbItensDisponiveis"
                                                                                            EnableClientSideAPI="True" Width="100%" Height="170px"
                                                                                            ID="lbItensDisponiveis" OnCallback="lbItensDisponiveis_Callback" SelectAllText="Selecionar Tudo" ToolTip="Digite Control  + clique com o botão esquerdo do mouse para selecionar vários ítens..." CallbackPageSize="10" EnableCallbackMode="True">
                                                                                            <ItemStyle Wrap="True"></ItemStyle>
                                                                                            <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                                                            <FilteringSettings EditorNullText="Digite o texto para filtrar" ShowSearchUI="True" EditorNullTextDisplayMode="Unfocused" UseCompactView="False" />
                                                                                            <ClientSideEvents EndCallback="function(s, e) {
	                    setListBoxItemsInMemory(s,'Disp_');
                    }"
                                                                                                SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"
                                                                                                Init="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </ReadOnlyStyle>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxListBox>
                                                                                    </td>
                                                                                    <td style="width: 60px" valign="top" align="center">
                                                                                        <table style="width: 100%; height: 170px" cellspacing="0" cellpadding="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td valign="middle" align="center">
                                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddAll"
                                                                                                            ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="80%" Height="75%"
                                                                                                            ToolTip="Selecionar todos os perfis" ID="btnAddAll">
                                                                                                            <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensDisponiveis,lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                        </dxe:ASPxButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="height: 42px" valign="middle" align="center">
                                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddSel"
                                                                                                            ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="80%" Height="75%"
                                                                                                            ToolTip="Selecionar os perfis marcados"
                                                                                                            ID="btnAddSel">
                                                                                                            <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensDisponiveis, lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                        </dxe:ASPxButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="height: 42px" valign="middle" align="center">
                                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveSel"
                                                                                                            ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="80%" Height="75%"
                                                                                                            ToolTip="Retirar da sele&#231;&#227;o os perfis marcados"
                                                                                                            ID="btnRemoveSel">
                                                                                                            <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                        </dxe:ASPxButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="height: 42px" valign="middle" align="center">
                                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveAll"
                                                                                                            ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="80%" Height="75%"
                                                                                                            ToolTip="Retirar da sele&#231;&#227;o todos perfis"
                                                                                                            ID="btnRemoveAll">
                                                                                                            <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                        </dxe:ASPxButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td style="height: 184px" valign="top" align="right">
                                                                                        <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="4" SelectionMode="Multiple" ClientInstanceName="lbItensSelecionados"
                                                                                            EnableClientSideAPI="True" Width="100%" Height="170px"
                                                                                            ID="lbItensSelecionados" OnCallback="lbItensSelecionados_Callback" SelectAllText="Selecionar Tudo" ToolTip="Digite Control  + clique com o botão esquerdo do mouse para selecionar vários ítens...">
                                                                                            <ItemStyle Wrap="True"></ItemStyle>
                                                                                            <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                                                            <FilteringSettings EditorNullText="Digite o texto para filtrar" ShowSearchUI="True" />
                                                                                            <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Sel_');
	setListBoxItemsInMemory(s,'InDB_');
	habilitaBotoesListBoxes();
	lpLoading.Hide();
	}"
                                                                                                SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </ReadOnlyStyle>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxListBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfUsuarios" ID="hfUsuarios">
                                                                            <ClientSideEvents EndCallback="function(s, e) {
	                        hfUsuarios_onEndCallback();
                        }"></ClientSideEvents>
                                                                        </dxhf:ASPxHiddenField>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table id="Table2" cellspacing="0" cellpadding="0" border="0" class="formulario-botoes">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvar"
                                                                            CausesValidation="False" Text="Salvar" Width="90px"
                                                                            ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	
	var operacao = hfGeral.Get(&quot;TipoOperacao&quot;);
	    hfGeral.Set('StatusSalvar','0');
   callback.PerformCallback(operacao);

}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td align="right" class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="90px" ID="btnFechar">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxtv:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
                            </dxtv:ASPxLoadingPanel>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                <Styles>
                                    <Default>
                                    </Default>
                                    <Header>
                                    </Header>
                                    <Cell>
                                    </Cell>
                                    <GroupFooter Font-Bold="True">
                                    </GroupFooter>
                                    <Title Font-Bold="True"></Title>
                                </Styles>
                            </dxwgv:ASPxGridViewExporter>

            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
</asp:Content>