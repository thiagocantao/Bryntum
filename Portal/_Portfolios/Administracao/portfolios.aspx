<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="portfolios.aspx.cs"
    Inherits="_Portfolios_Administracao_portfolios" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Manutenção de Portfólio" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="ConteudoPrincipal" <%--style="padding:7px 7px 2px 7px"--%>>
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            OnCallback="pnCallback_Callback" Width="100%">
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoPortfolio" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                        <ClientSideEvents CustomButtonClick="function(s, e) {
	//debugger
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
}"></ClientSideEvents>

                        <SettingsCommandButton>
                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                        </SettingsCommandButton>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="130px" VisibleIndex="0" ShowClearFilterButton="true">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                        <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                        <Image Url="~/imagens/botoes/pFormulario.png"></Image>
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
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoPortfolio" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoPortfolio" Caption="Portf&#243;lio" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="UsuarioGerente" Width="190px" Caption="Gerente" VisibleIndex="6"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="PortfolioSuperior" Width="190px" Caption="Portf&#243;lio Superior" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoPortfolioSuperior" Visible="False" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioGerente" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoStatusPortfolio" Visible="False" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Status" Visible="False" VisibleIndex="8"></dxwgv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn Caption="CodigoCarteiraAssociada" FieldName="CodigoCarteiraAssociada" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn Caption="Carteira Associada" FieldName="NomeCarteira" ShowInCustomizationForm="True" VisibleIndex="10">
                            </dxtv:GridViewDataTextColumn>
                        </Columns>

                        <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                        <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>

                        <Styles>
                            <TitlePanel BackColor="White" Font-Bold="False" ForeColor="Red"></TitlePanel>
                        </Styles>
                    </dxwgv:ASPxGridView>
                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                </dxp:PanelContent>
            </PanelCollection>

            <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
	{
		if(&quot;Incluir&quot; == s.cp_OperacaoOk)
        {
            window.top.mostraMensagem(traducao.portfolios_portf_lio_inclu_do_com_sucesso_, 'sucesso', false, false, null);
            pcDados.Hide();
        }			
	    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
        {
            window.top.mostraMensagem(traducao.portfolios_portf_lio_alterado_com_sucesso_, 'sucesso', false, false, null);
            pcDados.Hide();
        }			
		else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		{
            window.top.mostraMensagem(traducao.portfolios_portf_lio_exclu_do_com_sucesso, 'sucesso', false, false, null);
		}
		else if(hfGeral.Contains(&quot;ErroSalvar&quot;) &amp;&amp; hfGeral.Get(&quot;ErroSalvar&quot;).toString() != &quot;&quot;)
		{
			window.top.mostraMensagem(trataMensagemErro(s.cp_OperacaoErro,hfGeral.Get(&quot;ErroSalvar&quot;).toString()), 'erro', true, false, null);
		}

	}
	hfGeral.Set(&quot;ErroSalvar&quot;,&quot;&quot;);
	s.cp_OperacaoOk = &quot;&quot;;
}
"></ClientSideEvents>
        </dxcp:ASPxCallbackPanel>
    </div>



    <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ExportSelectedRowsOnly="false" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        <Styles>
            <Default></Default>

            <Header></Header>

            <Cell></Cell>

            <GroupFooter Font-Bold="True"></GroupFooter>

            <Title Font-Bold="True"></Title>
        </Styles>
    </dxcp:ASPxGridViewExporter>
    <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
    <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcMensagemGravacao" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False" Width="270px" ID="pcMensagemGravacao">
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td align="center" style=""></td>
                            <td align="center" rowspan="3" style="width: 70px">
                                <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxcp:ASPxImage>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxcp:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxcp:ASPxLabel>

                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
    <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ID="pcDados" Font-Bold="True">
        <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"
            CloseUp="function(s, e) {
	ddlGerente.SetValue(null);
	ddlGerente.SetText(&quot;&quot;);	
	ddlGerente.PerformCallback();
}"></ClientSideEvents>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxp:ASPxPanel runat="server" ClientInstanceName="pnFormulario" Width="100%" ID="pnFormulario">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <table class="formulario" style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="formulario-label">
                                                            <dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o do Portf&#243;lio:" Font-Bold="False" ID="ASPxLabel1"></dxe:ASPxLabel>


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="760px" MaxLength="50" ClientInstanceName="txtPortfolio" ID="txtPortfolio">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                            </dxe:ASPxTextBox>


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="formulario-colunas" cellspacing="0" cellpadding="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="formulario-label">
                                                                            <dxe:ASPxLabel runat="server" Text="Gerente:" Font-Bold="False" ID="ASPxLabel2"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="formulario-label">
                                                                            <dxe:ASPxLabel runat="server" Text="Status do Portf&#243;lio:" Font-Bold="False" ID="ASPxLabel3"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="formulario-label">
                                                                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="False" Text="Carteira:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxComboBox runat="server" Width="100%"
                                                                                IncrementalFilteringMode="Contains" ValueType="System.String"
                                                                                TextFormatString="{0}" ClientInstanceName="ddlGerente"
                                                                                ID="ddlGerente"
                                                                                DropDownStyle="DropDown" EnableCallbackMode="True"
                                                                                OnItemRequestedByValue="ddlGerente_ItemRequestedByValue"
                                                                                OnItemsRequestedByFilterCondition="ddlGerente_ItemsRequestedByFilterCondition">
                                                                                <Columns>
                                                                                    <dxe:ListBoxColumn FieldName="NomeUsuario" Width="300px" Caption="Nome"></dxe:ListBoxColumn>
                                                                                    <dxe:ListBoxColumn FieldName="EMail" Width="200px" Caption="Email"></dxe:ListBoxColumn>
                                                                                </Columns>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxComboBox>


                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlStatus" ID="ddlStatus">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxComboBox>


                                                                        </td>
                                                                        <td>
                                                                            <dxtv:ASPxComboBox ID="ddlCarteira" Width="100%" runat="server" ClientInstanceName="ddlCarteira">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="formulario-label">
                                                            <dxe:ASPxLabel runat="server" Text="Portf&#243;lio Superior:"
                                                                Font-Bold="False" ID="ASPxLabel4" Width="348px">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxCallbackPanel ID="callbackPortfolioSuperior" runat="server" ClientInstanceName="callbackPortfolioSuperior" OnCallback="callbackPortfolioSuperior_Callback" Width="100%">
                                                                <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" />
                                                                <ClientSideEvents EndCallback="function(s, e) {    
        ddlPortfolioSuperior.SetEnabled(s.cp_habilita == &quot;S&quot;);
  }" />
                                                                <PanelCollection>
                                                                    <dxtv:PanelContent runat="server">
                                                                        <dxtv:ASPxComboBox ID="ddlPortfolioSuperior" runat="server" ClientInstanceName="ddlPortfolioSuperior" Width="760px">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxComboBox>
                                                                    </dxtv:PanelContent>
                                                                </PanelCollection>
                                                            </dxtv:ASPxCallbackPanel>

                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100%" ID="btnSalvar">
                                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}
"></ClientSideEvents>
                                                </dxe:ASPxButton>

                                            </td>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100%" ID="btnFechar">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}
"></ClientSideEvents>
                                                </dxe:ASPxButton>

                                            </td>
                                            <%-- <td align="right"></td>--%>
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

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>


