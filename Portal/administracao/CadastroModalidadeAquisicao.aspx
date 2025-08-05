<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="CadastroModalidadeAquisicao.aspx.cs" Inherits="administracao_CadastroModalidadeAquisicao" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif); height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
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
            <td style="padding: 10px; padding-bottom: 0px">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	var erro = s.cp_ErroSalvar;
	if(erro != &quot;&quot; &amp;&amp; erro != undefined &amp;&amp; erro != &quot;undefined&quot;)
	{
		window.top.mostraMensagem(erro, 'erro', true, false, null);	
	}
	else
	{
		if(&quot;Incluir&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(&quot;Modalidade de Aquisição Incluída com Sucesso!&quot;, 'sucesso', false, false, null);
    	else if(&quot;Editar&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(&quot;Modalidade de Aquisição Alterada com Sucesso!&quot;, 'sucesso', false, false, null);
    	else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(&quot;Modalidade de Aquisição Excluída com Sucesso!&quot;, 'sucesso', false, false, null);
	}
}" />
                    <PanelCollection>
                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="Codigo"
                                AutoGenerateColumns="False" EnableRowsCache="False" Width="100%"
                                ID="gvDados" EnableViewState="False"
                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(window.pcDados &amp;&amp; pcDados.GetVisible())
		OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) 
{
	OnClick_CustomButtons(s, e);
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ToolTip="Editar os perfis do usu&#225;rio"
                                        VisibleIndex="0" Width="130px">
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
                                    <dxwgv:GridViewDataTextColumn Caption="Codigo" FieldName="Codigo" VisibleIndex="1"
                                        Visible="False">
                                        <Settings AutoFilterCondition="Contains" />
                                        <FilterCellStyle>
                                        </FilterCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="Descricao" VisibleIndex="2"
                                        Name="Descricao" SortIndex="0" SortOrder="Ascending">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="CodigoEntidade" FieldName="CodigoEntidade"
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager PageSize="100">
                                </SettingsPager>
                                <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>
                            </dxwgv:ASPxGridView>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
    </dxwgv:ASPxGridViewExporter>
    <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        CloseAction="None" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False"
        Width="620px" ID="pcDados">
        <ContentStyle>
            <Paddings Padding="5px"></Paddings>
        </ContentStyle>
        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxLabel runat="server" Text="Descrição:"
                                ID="ASPxLabel1">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtDescricaoModalidadeAquisicao"
                                ID="txtDescricaoModalidadeAquisicao">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                Text="Salvar" Width="90px"
                                                ID="btnSalvar">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                <Paddings Padding="0px"></Paddings>
                                            </dxe:ASPxButton>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                Text="Fechar" Width="90px" ID="btnFechar">
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
</asp:Content>
