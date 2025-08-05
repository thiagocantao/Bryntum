<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="grupoParticipantesEvento.aspx.cs" Inherits="administracao_grupoParticipantesEvento" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px;">
                            <span id="Span1" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server"
                                Text="Grupos de Participantes de Eventos" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                KeyFieldName="CodigoGrupoParticipantes" AutoGenerateColumns="False" Width="100%"
                                ID="gvDados"
                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
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
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="180px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Mostrar Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
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
                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                            ClientVisible="False">
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
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoGrupoParticipantes" Name="CodigoGrupoParticipantes"
                                        Caption="Codigo Grupo Participantes" VisibleIndex="1" Visible="False">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Nome do Grupo"
                                        FieldName="DescricaoGrupoParticipantes" ShowInCustomizationForm="True"
                                        VisibleIndex="2">
                                        <PropertiesTextEdit>
                                            <Style>
            </Style>
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                        <EditFormSettings Caption="Nome do Grupo" CaptionLocation="Top" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="CodigoEntidade"
                                        FieldName="CodigoEntidade" ShowInCustomizationForm="True" Visible="False"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>

                                <SettingsBehavior AllowFocusedRow="True" AllowGroup="False"></SettingsBehavior>

                                <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                                <SettingsEditing Mode="PopupEditForm" />

                                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>

                                <SettingsText></SettingsText>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="700px"
                                ID="pcDados">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>

                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                        Text="Nome do Grupo:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtNomeGrupo" runat="server" ClientInstanceName="txtNomeGrupo"
                                                        MaxLength="64" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 15px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxGridView ID="gvParticipantesEventos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvParticipantesEventos" KeyFieldName="CodigoUsuario" OnCustomCallback="gvParticipantesEventos_CustomCallback" Width="100%">
                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                    </SettingsPager>
                                                                    <Settings VerticalScrollableHeight="275" VerticalScrollBarMode="Auto" />
                                                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                                    <Columns>
                                                                        <dxtv:GridViewCommandColumn Caption=" " SelectAllCheckboxMode="Page" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="40px">
                                                                        </dxtv:GridViewCommandColumn>
                                                                        <dxtv:GridViewDataTextColumn Caption="Participantes" FieldName="NomeUsuario" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                                        </dxtv:GridViewDataTextColumn>
                                                                        <dxtv:GridViewDataTextColumn FieldName="Selecionado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                        </dxtv:GridViewDataTextColumn>
                                                                        <dxtv:GridViewDataTextColumn Caption=" " FieldName="ColunaAgrupamento" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" VisibleIndex="2">
                                                                        </dxtv:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <Templates>
                                                                        <GroupRowContent>
                                                                            <%# Container.GroupText == "0" ? "Selecionados" : "Disponíveis" %>
                                                                        </GroupRowContent>
                                                                    </Templates>
                                                                </dxtv:ASPxGridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td class="formulario-botao">
                                                                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                                        ClientInstanceName="btnSalvar"
                                                                        Text="Salvar" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                        <Paddings Padding="0px" />
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                                        <Paddings Padding="0px"></Paddings>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td class="formulario-botao">
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

                                                                        <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                                    </dxe:ASPxButton>

                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="" align="center"></td>
                                                    <td style="width: 70px" align="center" rowspan="3">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

























                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>

























                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                        </dxp:PanelContent>
                    </PanelCollection>

                    <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.grupoParticipantesEvento_grupo_de_participantes_inclu_do_com_sucesso_);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.grupoParticipantesEvento_grupo_de_participantes_alterado_com_sucesso_);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.grupoParticipantesEvento_grupo_de_participantes_exclu_do_com_sucesso_);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>

    </table>
</asp:Content>

