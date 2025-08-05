<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_GrupoFluxo.aspx.cs" Inherits="administracao_adm_GrupoFluxo" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
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
                                Text="Cadastro de Grupos de Fluxos" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>

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
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoGrupoFluxo"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        btnSalvar1.SetVisible(true);
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		btnSalvar1.SetVisible(true);
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
        btnSalvar1.SetVisible(false);
        OnGridFocusedRowChanged(gvDados, true);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="110px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                            ClientInstanceName="menu"
                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                            OnInit="menu_Init">
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
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoGrupoFluxo" Name="col_CodigoGrupoFluxo"
                                        Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoGrupoFluxo" Name="col_DescricaoGrupoFluxo"
                                        Caption="Descri&#231;&#227;o" VisibleIndex="2" SortIndex="0"
                                        SortOrder="Ascending">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="OrdemGrupoMenu" Name="col_OrdemGrupoMenu"
                                        Caption="Ordem" VisibleIndex="3" Width="85px">
                                        <Settings AutoFilterCondition="Contains" AllowAutoFilter="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IniciaisGrupoFluxo" Name="col_IniciaisGrupoFluxo"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" Name="col_CodigoEntidade"
                                        Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" AllowGroup="False"></SettingsBehavior>
                                <SettingsPager PageSize="100">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowFooter="True"></Settings>
                                <SettingsText></SettingsText>
                                <Templates>
                                    <FooterRow>
                                        <templates>
                                            <FooterRow>
                                                <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="grid-legendas-cor grid-legendas-cor-controlado-sistema"><span></span></td>
                                                            <td class="grid-legendas-label grid-legendas-label-controlado-sistema">
                                                                <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                                    Text="<%# Resources.traducao.adm_GrupoFluxo_grupos_de_fluxo_controlados_pelo_sistema %>">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </FooterRow>
                                        </templates>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>

                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                ExportEmptyDetailGrid="True" GridViewID="gvDados" Landscape="True"
                                LeftMargin="50" RightMargin="50"
                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                <Styles>
                                    <Default>
                                    </Default>
                                    <Header>
                                    </Header>
                                    <Cell>
                                    </Cell>
                                    <Footer>
                                    </Footer>
                                    <GroupFooter>
                                    </GroupFooter>
                                    <GroupRow>
                                    </GroupRow>
                                    <Title></Title>
                                </Styles>
                            </dxwgv:ASPxGridViewExporter>
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                            </dxhf:ASPxHiddenField>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.adm_GrupoFluxo_grupo_de_fluxo_inclu_do_com_sucesso_, 'sucesso', false, false, null);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.adm_GrupoFluxo_dados_gravados_com_sucesso_, 'sucesso', false, false, null);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.adm_GrupoFluxo_grupo_de_fluxo_exclu_do_com_sucesso_, 'sucesso', false, false, null);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>

        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados"
        CloseAction="None" HeaderText="Detalhes"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowCloseButton="False" Width="700px">
        <ContentStyle>
            <Paddings Padding="5px" />
        </ContentStyle>
        <HeaderStyle Font-Bold="True" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server"
                SupportsDisabledAttribute="True">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td align="left">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="lblNome" runat="server" ClientInstanceName="lblNome"
                                                Text="Descrição:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="width: 90px">
                                            <dxe:ASPxLabel ID="lblNome0" runat="server" ClientInstanceName="lblNome"
                                                Text="Ordem:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtDescricaoGrupoFluxo" runat="server"
                                                ClientInstanceName="txtDescricaoGrupoFluxo"
                                                MaxLength="60" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td style="width: 90px">
                                            <dxe:ASPxSpinEdit ID="txtOrdemGrupoFluxo" runat="server"
                                                ClientInstanceName="txtOrdemGrupoFluxo"
                                                MaxLength="4" MaxValue="1000" Number="0" NumberType="Integer" Width="100%">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <ReadOnlyStyle BackColor="#EBEBEB">
                                                </ReadOnlyStyle>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="formulario-botao">
                                                            <dxe:ASPxButton ID="btnSalvar" runat="server"
                                                                ClientInstanceName="btnSalvar1" AutoPostBack="False" Text="Salvar" Width="90px">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td class="formulario-botao">
                                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                                ClientInstanceName="btnFechar"
                                                                Text="Fechar" Width="90px">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                    PaddingRight="0px" PaddingTop="0px" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>

</asp:Content>
