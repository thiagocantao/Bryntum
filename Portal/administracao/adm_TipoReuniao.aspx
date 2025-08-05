<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="adm_TipoReuniao.aspx.cs" Inherits="administracao_adm_TipoReuniao" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Tipos de Reunião">
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
                    OnCallback="pnCallback_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                                KeyFieldName="CodigoTipoEvento" AutoGenerateColumns="False" Width="100%"
                                                ID="gvDados"
                                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                                <ClientSideEvents FocusedRowChanged="function(s, e) {
		OnGridFocusedRowChanged(s,true);
}"
                                                    CustomButtonClick="function(s, e) 
{
    btnSalvar1.SetVisible(true); 
    gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == 'btnNovo')
     {
        onClickBarraNavegacao('Incluir', gvDados, pcDados);
		hfGeral.Set('TipoOperacao', 'Incluir');
		TipoOperacao = 'Incluir';
     }
     if(e.buttonID == 'btnEditar')
     {
		onClickBarraNavegacao('Editar', gvDados, pcDados);
		hfGeral.Set('TipoOperacao', 'Editar');
		TipoOperacao = 'Editar';
     }
     else if(e.buttonID == 'btnExcluir')
     {
		onClickBarraNavegacao('Excluir', gvDados, pcDados);
     }
     else if(e.buttonID == 'btnDetalhesCustom')
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar1.SetVisible(false);
		hfGeral.Set('TipoOperacao', 'Consultar');
		TipoOperacao = 'Consultar';
		pcDados.Show();
     }	
}
"></ClientSideEvents>
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
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
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoEvento" Caption="C&#243;digo Tipo Evento" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoEvento" Caption="Tipo de Reuni&#227;o" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoModuloSistema" Caption="M&#243;dulo" Visible="False" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" Caption="Entidade" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
                                                </Columns>

                                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                                <Settings VerticalScrollBarMode="Visible"></Settings>
                                            </dxwgv:ASPxGridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="587px" Height="200px" ID="pcDados">
                                <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>

                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o:" ID="ASPxLabel1"></dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="60" ClientInstanceName="txtTipoReuniao" ID="txtTipoReuniao">
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                        </dxe:ASPxTextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Associa&#231;&#227;o:" ID="ASPxLabel3"></dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlModuloSistema" ID="ddlModuloSistema">
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                        </dxe:ASPxComboBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100px" ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                        </dxe:ASPxButton>

                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="100px" ID="btnCancelar">
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
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
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

                    <ClientSideEvents EndCallback="function(s, e)
{
	onEnd_pnCallback();

	//if (window.onEnd_pnCallback)
	//{

	if(hfGeral.Contains('StatusSalvar'))
    {
    	var status = hfGeral.Get('StatusSalvar');
        if(status != '1')
        {
        	//var mensagem = hfGeral.Get('ErroSalvar');
			//mostraPopupMensagemGravacao(mensagem );
		}
		else
		{
			if('Incluir' == s.cp_OperacaoOk)
				window.top.mostraMensagem(traducao.adm_TipoReuniao_tipo_de_reuni_o_inclu_do_com_sucesso_, 'sucesso', false, false, null);
			else if('Editar' == s.cp_OperacaoOk)
				window.top.mostraMensagem(traducao.adm_TipoReuniao_tipo_de_reuni_o_alterado_com_sucesso_, 'sucesso', false, false, null);
		}
	}

	//}
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>


