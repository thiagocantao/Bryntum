<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="tipoTarefaTimesheet.aspx.cs" Inherits="_Projetos_Administracao_tipoTarefaTimesheet" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px" valign="middle">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" style="height: 19px" valign="middle">
                            &nbsp; &nbsp;
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Tipos de Atividades" 
                                ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback" Width="100%"><PanelCollection>
<dxp:PanelContent runat="server">
            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoTipoTarefaTimeSheet" AutoGenerateColumns="False" Width="99%"  ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
<ClientSideEvents FocusedRowChanged="function(s, e) {
		OnGridFocusedRowChanged(s,true);
}" CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     btnSalvar1.SetVisible(true);
	 if(e.buttonID == &quot;btnAssociaProjetos&quot;)
	{
        onBtnEditarTipoTarefaTimeSheet_Click(s,e);
	}
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar1.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"></ClientSideEvents>
<Columns>
    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="135px">
        <CustomButtons>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnAssociaProjetos" Text="Associar Tipo de Atividade a Projetos">
                <Image Url="~/imagens/compartilhar.PNG" />
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                <Image Url="~/imagens/botoes/editarReg02.PNG" />
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                <Image Url="~/imagens/botoes/excluirReg02.PNG" />
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                <Image Url="~/imagens/botoes/pFormulario.PNG" />
            </dxwgv:GridViewCommandColumnCustomButton>
        </CustomButtons>
        <HeaderTemplate>
                                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        </HeaderTemplate>
    </dxwgv:GridViewCommandColumn>
    <dxwgv:GridViewDataTextColumn Caption="C&#243;digo Tipo Tarefa" FieldName="CodigoTipoTarefaTimeSheet"
        Visible="False" VisibleIndex="1">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Tipo de Atividade" FieldName="DescricaoTipoTarefaTimeSheet"
        VisibleIndex="2">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Codigo Entidade" FieldName="CodigoEntidade"
        Visible="False" VisibleIndex="3">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>
</dxwgv:ASPxGridView>
 
                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                </dxhf:ASPxHiddenField>
 
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	{
	   //debugger
       if(&quot;Incluir&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(&quot;Tipo de Atividade Incluída com Sucesso!&quot;, 'sucesso', false, false, null);
	    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(&quot;Tipo de Atividade Alterada com Sucesso!&quot;, 'sucesso', false, false, null);
        onEnd_pnCallback();	
	}
}"></ClientSideEvents>
</dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Tipo de Atividade" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="587px" Height="98px"  ID="pcDados">
<ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>
<ContentCollection>
<dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr><td style="height: 16px"><dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o do Tipo de Atividade:"  ID="ASPxLabel1"></dxe:ASPxLabel>

 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="587px" MaxLength="100" ClientInstanceName="txtDescricaoTipoTarefa"  ID="txtDescricaoTipoTarefa">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>

 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align=right><table cellspacing="0" cellpadding="0" border="0"><tbody><tr><td align=right><dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar1" Text="Salvar" Width="90px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
</dxe:ASPxButton>

 </td><td style="WIDTH: 100px" align=right><dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="90px"  ID="btnCancelar">
<ClientSideEvents Click="function(s, e) {
		e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
</dxe:ASPxButton>

 </td></tr></tbody></table></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcCompartilharAssuntos" runat="server" AllowDragging="True"
        ClientInstanceName="pcCompartilharAssuntos" CloseAction="None"
        HeaderText="Compartilhar Tipo de Atividade com Projetos" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="650px">
        <ContentStyle>
            <Paddings Padding="5px" />
        </ContentStyle>
        <HeaderStyle Font-Bold="False" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 648px;">
                    <tbody>
                        <tr>
                            <td style="height: 16px">
                                <dxe:ASPxLabel ID="ASPxLabel1011" runat="server"
                                      Text="Tipo de Atividade Selecionado:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxTextBox ID="txtDescricaoTipoTarefa_pc" runat="server" ClientInstanceName="txtDescricaoTipoTarefa_pc"
                                    
                                    MaxLength="30" ReadOnly="True" Width="100%">
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ReadOnlyStyle BackColor="WhiteSmoke">
                                    </ReadOnlyStyle>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 649px">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 295px">
                                                <dxe:ASPxLabel ID="ASPxLabel106" runat="server"
                                                     Text="Projetos Dispon&#237;veis:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                            <td align="left" style="width: 295px">
                                                <dxe:ASPxLabel ID="ASPxLabel107" runat="server"  Text="Projetos Selecionados:">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxListBox ID="lbItensDisponiveis" runat="server" ClientInstanceName="lbItensDisponiveis"
                                                     EnableClientSideAPI="True"
                                                    EncodeHtml="False"  Height="150px" ImageFolder="~/App_Themes/Aqua/{0}/"
                                                     Rows="3" SelectionMode="Multiple" Width="100%" OnCallback="lbItensDisponiveis_Callback">
                                                    <ItemStyle BackColor="White" Wrap="True">
                                                        <SelectedStyle BackColor="#FFE4AC">
                                                        </SelectedStyle>
                                                    </ItemStyle>
                                                    <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Disp_');
}" SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}" />
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" Width="14px" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <DisabledStyle ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxListBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="height: 28px">
                                                                <dxe:ASPxButton ID="btnAddAll" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                    ClientInstanceName="btnAddAll" EncodeHtml="False" Font-Bold="True" 
                                                                    Height="25px" Text="&gt;&gt;" ToolTip="Selecionar todas as unidades" Width="40px">
                                                                    <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensDisponiveis,lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 28px">
                                                                <dxe:ASPxButton ID="btnAddSel" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                    ClientInstanceName="btnAddSel" EncodeHtml="False" Font-Bold="True" 
                                                                    Height="25px" Text="&gt;" ToolTip="Selecionar as unidades marcadas" Width="40px">
                                                                    <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensDisponiveis, lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 28px">
                                                                <dxe:ASPxButton ID="btnRemoveSel" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                    ClientInstanceName="btnRemoveSel" EncodeHtml="False" Font-Bold="True" 
                                                                    Height="25px" Text="&lt;" ToolTip="Retirar da sele&#231;&#227;o as unidades marcadas"
                                                                    Width="40px">
                                                                    <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 28px">
                                                                <dxe:ASPxButton ID="btnRemoveAll" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                    ClientInstanceName="btnRemoveAll" EncodeHtml="False" Font-Bold="True" 
                                                                    Height="25px" Text="&lt;&lt;" ToolTip="Retirar da sele&#231;&#227;o todas as unidades"
                                                                    Width="40px">
                                                                    <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dxe:ASPxListBox ID="lbItensSelecionados" runat="server" ClientInstanceName="lbItensSelecionados"
                                                     EnableClientSideAPI="True"
                                                    EncodeHtml="False"  Height="150px" ImageFolder="~/App_Themes/Aqua/{0}/"
                                                     Rows="4" SelectionMode="Multiple" Width="100%" OnCallback="lbItensSelecionados_Callback">
                                                    <ItemStyle BackColor="White" Wrap="True">
                                                        <SelectedStyle BackColor="#FFE4AC">
                                                        </SelectedStyle>
                                                    </ItemStyle>
                                                    <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Sel_');
	setListBoxItemsInMemory(s,'InDB_');
	habilitaBotoesListBoxes();
}" SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}" />
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" Width="14px" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <DisabledStyle ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxListBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dxhf:ASPxHiddenField ID="hfCompartilhaAssuntos" runat="server" ClientInstanceName="hfCompartilhaAssuntos"
                                    OnCustomCallback="hfCompartilhaAssuntos_CustomCallback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	hfCompartilhaAssuntos_onEndCallback();
}" />
                                </dxhf:ASPxHiddenField>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table id="tblSalvarFechar" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr style="height: 35px">
                                            <td>
                                                <dxe:ASPxButton ID="btnSalvarCompartilhar" runat="server" AutoPostBack="False" CausesValidation="False"
                                                    ClientInstanceName="btnSalvarCompartilhar"  Text="Salvar" UseSubmitBehavior="False"
                                                    Width="100px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcCompartilharAssuntos_onBtnSalvar();
}" />
                                                    <Paddings Padding="0px" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="right">
                                                <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                    
                                                    Text="Fechar" Width="100px">
                                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
    pcCompartilharAssuntos.Hide();
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                    <Paddings Padding="0px" />
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
</asp:Content>
