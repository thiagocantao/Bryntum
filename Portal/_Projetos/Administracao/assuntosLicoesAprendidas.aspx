<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="assuntosLicoesAprendidas.aspx.cs" Inherits="_Projetos_Administracao_assuntosLicoesAprendidas" %>

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

                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Assuntos de Lições Aprendidas">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <div id="ConteudoPrincipal">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        OnCallback="pnCallback_Callback" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoAssuntoLicaoAprendida" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
		OnGridFocusedRowChanged(s,true);
}"
                                        CustomButtonClick="function(s, e) 
{
     btnSalvar1.SetVisible(true);
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
		btnSalvar1.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="125px" VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                    <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
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
                                                                            <dxtv:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                </Image>
                                                                            </dxtv:MenuItem>

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
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoAssuntoLicaoAprendida" Caption="C&#243;digo Assunto" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoAssuntoLicaoAprendida"
                                            Caption="Assunto" VisibleIndex="2" SortIndex="0" SortOrder="Ascending">
                                            <Settings AllowSort="True" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaControladoSistema" Caption="Controlado Sistema" Visible="False" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
                                    </Columns>

                                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                    <Settings VerticalScrollBarMode="Visible"></Settings>
                                </dxwgv:ASPxGridView>
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="587px" Height="98px" ID="pcDados">
                                    <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="Assunto:" ID="ASPxLabel1"></dxe:ASPxLabel>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="587px" MaxLength="50" ClientInstanceName="txtAssunto" ID="txtAssunto">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                            </dxe:ASPxTextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar1" Text="Salvar" Width="90px" ID="btnSalvar">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                            </dxe:ASPxButton>

                                                                        </td>
                                                                        <td style="width: 100px" align="right">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="90px" ID="btnCancelar">
                                                                                <ClientSideEvents Click="function(s, e) {
		e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
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
                            </dxp:PanelContent>
                        </PanelCollection>

                        <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	{
		if(&quot;Incluir&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(traducao.assuntosLicoesAprendidas_li__o_aprendida_inclu_da_com_sucesso_ ,'sucesso', false, false, null);
	    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(traducao.assuntosLicoesAprendidas_li__o_aprendida_alterada_com_sucesso_,'sucesso', false, false, null);
	    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(traducao.assuntosLicoesAprendidas_li__o_aprendida_exclu_da_com_sucesso_,'sucesso', false, false, null);		
	}
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
