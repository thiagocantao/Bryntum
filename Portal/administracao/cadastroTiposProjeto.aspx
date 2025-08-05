<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="cadastroTiposProjeto.aspx.cs" Inherits="administracao_cadastroTiposProjetos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Tipos de Projetos">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="ConteudoPrincipal">
        <table>
            <tr>
                <td>
                    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
                        Landscape="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
                        ExportEmptyDetailGrid="True" PreserveGroupRowStates="False">
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
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ValueType="System.String" ClientInstanceName="ddlExporta"
                                        ID="ddlExporta" ClientVisible="False">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set('tipoArquivo', s.GetValue());
}"></ClientSideEvents>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td style="padding-right: 3px; padding-left: 3px">
                                    <dxcp:ASPxCallbackPanel runat="server"
                                        ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent2" runat="server">
                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                    Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao" ClientVisible="False">
                                                </dxe:ASPxImage>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>
                                </td>
                                <td style="padding-left: 3px; width: 75px">
                                    <dxe:ASPxButton runat="server" Text="Exportar"
                                        ID="Aspxbutton1" OnClick="btnExcel_Click" ClientVisible="False">
                                        <ClientSideEvents Click="function(s, e) 
{
	if(gvDados.pageRowCount == 0)
	{
		window.top.mostraMensagem(&quot;N&#227;o h&#225; Nenhuma informa&#231;&#227;o para exportar.&quot;, 'Atencao', true, false, null);
		e.processOnServer = false;	
	}
}"></ClientSideEvents>
                                        <Paddings Padding="0px"></Paddings>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent1" runat="server">
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoTipoProjeto"
                                    AutoGenerateColumns="False" Width="100%"
                                    ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback"
                                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	
OnGridFocusedRowChanged(s, true);
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
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
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
                                                <%--<%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>--%>
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
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoProjeto" Caption="CodigoTipoProjeto"
                                            VisibleIndex="1" Visible="False">
                                            <Settings AllowAutoFilter="False" />
                                            <Settings AllowAutoFilter="False"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Descrição Tipo de Projeto"
                                            FieldName="TipoProjeto" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Controlado pelo Sistema" FieldName="IndicaControladoSistema"
                                            ShowInCustomizationForm="True" VisibleIndex="10">
                                            <Settings AllowAutoFilter="True" AllowDragDrop="False"
                                                AllowHeaderFilter="True" />
                                            <Settings AllowDragDrop="False" AllowAutoFilter="True" AllowHeaderFilter="True"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Tipo Projeto" FieldName="IndicaTipoProjeto"
                                            ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                                            <Settings AllowAutoFilter="False" />
                                            <Settings AllowAutoFilter="False"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Código Tipo Associação" FieldName="CodigoTipoAssociacao"
                                            ShowInCustomizationForm="True" VisibleIndex="7" Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Tipo de Associação" FieldName="DescricaoTipoAssociacao"
                                            ShowInCustomizationForm="True" VisibleIndex="8" Width="250px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="CodigoCalendarioPadrao" FieldName="CodigoCalendarioPadrao"
                                            ShowInCustomizationForm="True" VisibleIndex="3" Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Calendário Padrão" ShowInCustomizationForm="True"
                                            VisibleIndex="4" FieldName="DescricaoCalendario">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Tolerância Atraso Projeto" FieldName="ToleranciaInicioLBProjeto"
                                            ShowInCustomizationForm="True" VisibleIndex="5" Width="170px">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True"
                                        AutoExpandAllGroups="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <Settings VerticalScrollBarMode="Visible" ShowFooter="True"></Settings>
                                    <SettingsText></SettingsText>
                                    <Templates>
                                        <FooterRow>
                                            <table class="grid-legendas">
                                                <tbody>
                                                    <tr>
                                                        <td class="grid-legendas-cor grid-legendas-cor-controlado-sistema">
                                                            <span></span>
                                                        </td>
                                                        <td class="grid-legendas-label grid-legendas-label-controlado-sistema">
                                                            <span>
                                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, cadastroTiposProjeto_controlados_pelo_sistema %>" /></span>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </FooterRow>
                                    </Templates>
                                </dxwgv:ASPxGridView>
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                    HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                    ShowCloseButton="False" Width="485px" ID="pcDados">
                                    <ContentStyle>
                                        <Paddings Padding="5px"></Paddings>
                                    </ContentStyle>
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                        Text="Tipo de Projeto*:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtTipoDeProjeto" runat="server" ClientInstanceName="txtTipoDeProjeto"
                                                                        MaxLength="30" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Calendário Base*:"
                                                                        ID="ASPxLabel2">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="488px"
                                                                        ID="ddlCalendarioBase"
                                                                        ClientInstanceName="ddlCalendarioBase">
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                        Text="Tolerância para Atraso no Início do Projeto (em dias):">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxSpinEdit ID="txtAtraso" runat="server" ClientInstanceName="txtAtraso" Number="0">
                                                                        <SpinButtons ClientVisible="False" ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                        </dxtv:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table cellpadding="0" cellspacing="0" class="formulario-botoes">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                            Text="Salvar" Width="100px">
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
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                                PaddingTop="0px" />
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
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.cadastroTiposProjeto_tipo_de_projeto_inclu_do_com_sucesso_);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.cadastroTiposProjeto_tipo_de_projeto_alterado_com_sucesso_);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.cadastroTiposProjeto_tipo_de_projeto_exclu_do_com_sucesso_);
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
