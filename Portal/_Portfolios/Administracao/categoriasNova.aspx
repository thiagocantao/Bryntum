<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="categoriasNova.aspx.cs" Inherits="_Portfolios_Administracao_categorias" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoCategoria" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
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
	 else if(e.buttonID == &quot;btnMatriz&quot;)
     {	
		gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoCategoria', abreMatrizCategoria);
     }	
}" Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 90;
       s.SetHeight(sHeight);
}"></ClientSideEvents>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="col_comando" Width="130px" VisibleIndex="0" ShowClearFilterButton="true">
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
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnMatriz"
                                                Text="Editar Matriz de Priorização">
                                                <Image Url="~/imagens/botoes/matriz_ahp1.png"></Image>
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
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout"
                                                                            Name="btnRestaurarLayout">
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
                                    <dxwgv:GridViewDataTextColumn FieldName="SiglaCategoria" Name="SiglaCategoria" Width="80px" Caption="Sigla" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoCategoria" Name="col_DescricaoCategoria" Caption="Categoria" VisibleIndex="3" SortOrder="Ascending"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoCategoria" Name="col_CodigoCategoria" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
                                </Columns>

                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

                                <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                                <Settings VerticalScrollBarMode="Visible"></Settings>
                                <Paddings PaddingTop="5px" />
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Dados da Categoria" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupVerticalOffset="40" ShowCloseButton="False" Width="860px" ID="pcDados">
                                <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"
                                    Shown="function(s, e) {
}"></ClientSideEvents>

                                <CloseButtonImage Width="17px"></CloseButtonImage>

                                <SizeGripImage Width="12px"></SizeGripImage>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table class="formulario" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        <table class="formulario-colunas" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td style="padding: 2px;">
                                                                    <dxe:ASPxLabel runat="server" Text="Categoria:" ID="ASPxLabel1"></dxe:ASPxLabel>


                                                                </td>
                                                                <td></td>
                                                                <td style="width: 80px">
                                                                    <dxe:ASPxLabel runat="server" Text="Sigla:" ClientInstanceName="lblSigla" ID="lblSigla"></dxe:ASPxLabel>


                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="60" ClientInstanceName="txtCategoria" ID="txtCategoria">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                    </dxe:ASPxTextBox>


                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="5" ClientInstanceName="txtSigla" ID="txtSigla">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                    </dxe:ASPxTextBox>


                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table class="formulario-botoes" id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px" ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    if (window.onClick_btnSalvar)
	                    onClick_btnSalvar();
                }"></ClientSideEvents>

                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>



                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
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

                    <SettingsLoadingPanel Text=" "></SettingsLoadingPanel>

                    <ClientSideEvents EndCallback="function(s, e) {
         if(s.cp_MSG != &quot;&quot;)
         {
                     window.top.mostraMensagem(s.cp_MSG, 'erro', true, false, null);
         }
        else
        {
                     if(&quot;Incluir&quot; == s.cp_OperacaoOk)
                             window.top.mostraMensagem(traducao.categoriasNova_categoria_inclu_da_com_sucesso, 'sucesso', false, false, null);
	    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
	             window.top.mostraMensagem(traducao.categoriasNova_categoria_alterada_com_sucesso, 'sucesso', false, false, null);	
	    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
	            window.top.mostraMensagem(traducao.categoriasNova_categoria_exclu_da_com_sucesso, 'sucesso', false, false, null);
      onEnd_pnCallback();  
     }
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>
