<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="reuniaoTecnicaPlanejamento.aspx.cs" Inherits="Reunioes_reuniaoTecnicaPlanejamento"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTitulo"
                    Font-Bold="True"
                    EnableViewState="False">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>


    <table style="width: 100%">
        <tr>
            <td style="padding: 5px">
                <dxe:ASPxLabel ID="lblUnidade" runat="server"
                    Text="Unidade:" EnableViewState="False">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="padding: 0px 5px 5px 5px">
                <dxe:ASPxTextBox ID="txtUnidade" runat="server" ClientEnabled="False"
                    Width="100%" EnableViewState="False">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td id="tdLista" align="left" style="padding: 0px 5px 0px 5px">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback" EnableViewState="False">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoEvento"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" EnableViewState="False" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback"
                                OnHeaderFilterFillItems="gvDados_HeaderFilterFillItems"
                                OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) {
    //gvDados.SetFocusedRowIndex(e.visibleIndex);
    btnSalvar1.SetVisible(true); 
    if(e.buttonID == &quot;btnPlan&quot;)
     {
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		desabilitaHabilitaComponentes();
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {	
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		desabilitaHabilitaComponentes();
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar1.SetVisible(false);
		pcDados.Show();
     }
     else if(e.buttonID == &quot;btnReal&quot;)
     {	
		somenteLeituraExecucao = 'N';
&nbsp;                               s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
                                s.GetSelectedFieldValues( &quot;CodigoEvento;&quot;, abreExecucao);
     }
     else if(e.buttonID == &quot;btnDetalheReal&quot;)
     {	
		somenteLeituraExecucao = 'S';
		s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
                                s.GetSelectedFieldValues( &quot;CodigoEvento;&quot;, abreExecucao);
     }	
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="A&#231;&#227;o" Name="A&#231;&#227;o"
                                        VisibleIndex="0" Width="130px">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnPlan" Text="Editar Planejamento">
                                                <Image Url="~/imagens/planejamentoReuniao.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnReal" Text="Editar Realiza&#231;&#227;o">
                                                <Image Url="~/imagens/realizacaoReuniao.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                                <Image Url="~/imagens/botoes/pFormulario.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheReal" Text="Detalhe">
                                                <Image Url="~/imagens/botoes/pFormulario.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
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
                                    <dxwgv:GridViewDataTextColumn Caption="Assunto" FieldName="DescricaoResumida" Name="DescricaoResumida"
                                        VisibleIndex="1" Width="300px">
                                        <Settings AllowHeaderFilter="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="Início Previsto"
                                        FieldName="InicioPrevisto" Name="InicioPrevisto"
                                        VisibleIndex="2" Width="130px" SortIndex="1" SortOrder="Descending">
                                        <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, geral_formato_data_hora_curta_csharp %>">
                                        </PropertiesDateEdit>
                                        <Settings ShowFilterRowMenu="True" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Quando" FieldName="Quando" Name="Quando" VisibleIndex="4"
                                        Width="135px">
                                        <PropertiesTextEdit DisplayFormatString="<%$ Resources:traducao, geral_formato_data_hora_curta_csharp %>">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="True" AllowHeaderFilter="False" />
                                        <FilterCellStyle>
                                            <Paddings Padding="0px" />
                                        </FilterCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavelEvento" Name="CodigoResponsavelEvento"
                                        Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="inicioPrevistoData" Name="inicioPrevistoData"
                                        Visible="False" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="inicioPrevistoHora" Name="inicioPrevistoHora"
                                        Visible="False" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevisto" Name="TerminoPrevisto"
                                        Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevistoData" Name="TerminoPrevistoData"
                                        Visible="False" VisibleIndex="9">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevistoHora" Name="TerminoPrevistoHora"
                                        Visible="False" VisibleIndex="10">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InicioReal" Name="InicioReal" Visible="False"
                                        VisibleIndex="11">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InicioRealData" Name="InicioRealData" Visible="False"
                                        VisibleIndex="12">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InicioRealHora" Name="InicioRealHora" Visible="False"
                                        VisibleIndex="13">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoReal" Name="TerminoReal" Visible="False"
                                        VisibleIndex="14">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="Realização" FieldName="TerminoRealData"
                                        Name="TerminoRealData" ShowInCustomizationForm="True" VisibleIndex="3"
                                        Width="130px" SortIndex="0" SortOrder="Descending">
                                        <PropertiesDateEdit DisplayFormatString="">
                                        </PropertiesDateEdit>
                                        <Settings ShowFilterRowMenu="True" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoRealHora" Name="TerminoRealHora"
                                        Visible="False" VisibleIndex="15">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoAssociacao" Name="CodigoTipoAssociacao"
                                        Visible="False" VisibleIndex="16">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="CodigoObjetoAssociado"
                                        Visible="False" VisibleIndex="17">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="LocalEvento" Name="LocalEvento" Visible="False"
                                        VisibleIndex="18">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Pauta" Name="Pauta" Visible="False"
                                        VisibleIndex="19">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="ResumoEvento" Name="ResumoEvento" Visible="False"
                                        VisibleIndex="20">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoEvento" Name="CodigoTipoEvento"
                                        Visible="False" VisibleIndex="21">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="CodigoObjetoAssociado"
                                        Visible="False" VisibleIndex="22">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaAtrasada"
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="PermissaoEditarAtaEntidade"
                                        FieldName="PermissaoEditarAtaEntidade" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="23" Width="190px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="PermissaoEditarAtaResponsavel"
                                        FieldName="PermissaoEditarAtaResponsavel" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="24" Width="190px">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                                    ShowFooter="True" ShowHeaderFilterBlankItems="False"></Settings>
                                <SettingsAdaptivity AdaptiveColumnPosition="None">
                                </SettingsAdaptivity>
                                <Templates>
                                    <FooterRow>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td class="grid-legendas-cor grid-legendas-cor-atrasado">
                                                    <span></span>
                                                </td>
                                                <td class="grid-legendas-label grid-legendas-label-atrasado">
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                        Text="<%$ Resources:traducao, reuni_es_passadas_n_o_finalizadas %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td class="grid-legendas-asterisco">
                                                    <span>*</span>
                                                </td>
                                                <td class="grid-legendas-label grid-legendas-label-asterisco">
                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                        Text="<%$ Resources:traducao, para_editar_a_realiza__o_da_reuni_o___necess_rio_que_o_seu_navegador_aceite_a_abertura_de_pop_ups_ %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {	
	callbackEnviaPauta.PerformCallback(s.cp_PodeEnviarAta);
                 if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.reuniaoTecnicaPlanejamento_reuni_o_exclu_da_com_sucesso_, 'sucesso', false, false, null);
	else
	{
		if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		{
		             hfGeral.Set(&quot;TipoOperacao&quot;, 'Editar');
		            onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
                                            if(s.cp_PodeEnviarAta == 'S')
                                            {
                                                        window.top.mostraMensagem(traducao.reuniaoTecnicaPlanejamento_deseja_enviar_a_pauta_da_reuni_o_aos_participantes_, 'confirmacao', true, true, enviaPautaDeReuniao);
                                            }
                                 }
	                 else if(&quot;Editar&quot; == s.cp_OperacaoOk)
                                 {
                                            if(s.cp_PodeEnviarAta == 'S')
                                            {
                                                        window.top.mostraMensagem(traducao.reuniaoTecnicaPlanejamento_deseja_enviar_a_pauta_da_reuni_o_aos_participantes_, 'confirmacao', true, true, enviaPautaDeReuniao);
                                            }
	                             window.top.mostraMensagem(traducao.reuniaoTecnicaPlanejamento_dados_gravados_com_sucesso_, 'sucesso', false, false, null);
                                 }
	                else if(&quot;EnviarPauta&quot; == s.cp_OperacaoOk)
		        window.top.mostraMensagem(traducao.reuniaoTecnicaPlanejamento_pauta_enviada_com_sucesso_aos_participantes_, 'sucesso', false, false, null);
	                else if(&quot;EnviarAta&quot; == s.cp_OperacaoOk)
		        window.top.mostraMensagem(traducao.reuniaoTecnicaPlanejamento_ata_enviada_com_sucesso_aos_participantes_, 'sucesso', false, false, null);
	                else if(&quot;Erro&quot; == s.cp_OperacaoOk)
		     window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);		
	}
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>

    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
        ShowHeader="False" Width="270px" ID="pcUsuarioIncluido"
        EnableViewState="False">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="" align="center"></td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                    ClientInstanceName="imgSalvar" ID="imgSalvar" EnableViewState="False">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                    ID="lblAcaoGravacao">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcMensagemPauta"
        CloseAction="None" HeaderText="Envio de pauta" PopupAction="None" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="700px" ID="pcMensagemPauta"
        EnableViewState="False">
        <ClientSideEvents Closing="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('TabA')); 
}"></ClientSideEvents>
        <ContentStyle>
            <Paddings Padding="3px" PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px"></Paddings>
        </ContentStyle>
        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" Text="<%$ Resources:traducao, reuniaoTecnicaPlanejamento_texto_de_apresenta__o_ %>"
                                                    ID="Label1"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="heEncabecadoAta" Width="98%"
                                                    Height="120px" ID="heEncabecadoAta">
                                                    <Toolbars>
                                                        <dxhe:HtmlEditorToolbar>
                                                            <Items>
                                                                <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                    <Items>
                                                                        <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                    </Items>
                                                                </dxhe:ToolbarParagraphFormattingEdit>
                                                                <dxhe:ToolbarFontNameEdit>
                                                                    <Items>
                                                                        <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                                    </Items>
                                                                </dxhe:ToolbarFontNameEdit>
                                                                <dxhe:ToolbarFontSizeEdit>
                                                                    <Items>
                                                                        <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                                        <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                                    </Items>
                                                                </dxhe:ToolbarFontSizeEdit>
                                                                <dxhe:ToolbarBoldButton BeginGroup="True">
                                                                </dxhe:ToolbarBoldButton>
                                                                <dxhe:ToolbarItalicButton>
                                                                </dxhe:ToolbarItalicButton>
                                                                <dxhe:ToolbarUnderlineButton>
                                                                </dxhe:ToolbarUnderlineButton>
                                                                <dxhe:ToolbarStrikethroughButton>
                                                                </dxhe:ToolbarStrikethroughButton>
                                                                <dxhe:ToolbarJustifyLeftButton BeginGroup="True">
                                                                </dxhe:ToolbarJustifyLeftButton>
                                                                <dxhe:ToolbarJustifyCenterButton>
                                                                </dxhe:ToolbarJustifyCenterButton>
                                                                <dxhe:ToolbarJustifyRightButton>
                                                                </dxhe:ToolbarJustifyRightButton>
                                                                <dxhe:ToolbarJustifyFullButton>
                                                                </dxhe:ToolbarJustifyFullButton>
                                                                <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                                </dxhe:ToolbarBackColorButton>
                                                                <dxhe:ToolbarFontColorButton>
                                                                </dxhe:ToolbarFontColorButton>
                                                            </Items>
                                                        </dxhe:HtmlEditorToolbar>
                                                    </Toolbars>
                                                    <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                </dxhe:ASPxHtmlEditor>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 10px" align="right">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnEnviarEncabecadoPauta"
                                                                    Text="Enviar" ValidationGroup="MKE" Width="100px"
                                                                    ID="btnEnviarEncabecadoPauta" EnableViewState="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;

		capturaCodigosInteressados();

		if(verificarDadosPreenchidos())
		{
			tipoEnvio = &quot;EnviarPauta&quot;;
			pnCallback.PerformCallback(tipoEnvio);
		}
		
}"></ClientSideEvents>
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td></td>
                                                            <td style="width: 90px">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelarEncabecadoPauta"
                                                                    Text="Fechar" Width="100%" ID="btnCancelarEncabecadoPauta"
                                                                    EnableViewState="False">
                                                                    <ClientSideEvents Click="function(s, e) {	
	pcMensagemPauta.Hide();
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
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
        CloseAction="None" HeaderText="Reuni&#227;o da Unidade" PopupAction="None" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="790px" ID="pcDados" Height="400px" PopupVerticalOffset="10"
        EnableViewState="False">
        <ClientSideEvents Closing="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('TabA')); 
            }"></ClientSideEvents>
        <ContentStyle>
            <Paddings Padding="3px" PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px"></Paddings>
        </ContentStyle>
        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxp:ASPxPanel runat="server" Width="100%" ID="pnFormulario" Style="overflow: auto">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl"
                                                Width="100%" ID="tabControl">
                                                <TabPages>
                                                    <dxtc:TabPage Name="TabA" Text="Reuni&#227;o">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <div id="divReuniao" style="height:380px;overflow-y:auto">
                                                                <table class="formulario" style="overflow-y:visible" cellspacing="0" cellpadding="0" width="98%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <table class="formulario-colunas" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="formulario-label">
                                                                                                <asp:Label runat="server" Text="<%$ Resources:traducao, reuniaoTecnicaPlanejamento_assunto_ %>" ID="lblAssunto"></asp:Label>
                                                                                            </td>
                                                                                            <td class="formulario-label">
                                                                                                <asp:Label runat="server" Text="<%$ Resources:traducao, reuniaoTecnicaPlanejamento_respons_vel_ %>" ID="lblResponsavel"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtAssunto"
                                                                                                    ID="txtAssunto">
                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                                        <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.Int32"
                                                                                                    Width="100%" ClientInstanceName="ddlResponsavelEvento"
                                                                                                    ID="ddlResponsavelEvento" CallbackPageSize="80" DropDownRows="10"
                                                                                                    DropDownStyle="DropDown" EnableCallbackMode="True"
                                                                                                    OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                                                                                    OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                                                                                    TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario">
                                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//lbDisponiveis.PerformCallback();
}"></ClientSideEvents>
                                                                                                    <Columns>
                                                                                                        <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario"></dxe:ListBoxColumn>
                                                                                                        <dxe:ListBoxColumn Caption="Email" FieldName="EMail"></dxe:ListBoxColumn>
                                                                                                    </Columns>
                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                                        <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxComboBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table class="formulario-colunas" cellspacing="0" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="formulario-label" style="width: 50%">
                                                                                                <asp:Label runat="server"
                                                                                                    Text="<%$ Resources:traducao, reuniaoTecnicaPlanejamento_tipo_de_reuni_o_ %>" ID="lblTipoEventos">
                                                                                                </asp:Label>
                                                                                            </td>
                                                                                            <td class="formulario-label" colspan="2">
                                                                                                <asp:Label runat="server"
                                                                                                    Text="<%$ Resources:traducao, reuniaoTecnicaPlanejamento_in_cio_ %>" ID="lblInicio">
                                                                                                </asp:Label>
                                                                                            </td>
                                                                                            <td class="formulario-label" colspan="2">
                                                                                                <asp:Label runat="server"
                                                                                                    Text="<%$ Resources:traducao, reuniaoTecnicaPlanejamento_t_rmino_ %>" ID="lblTermino">
                                                                                                </asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 50%">
                                                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlTipoEvento"
                                                                                                    ID="ddlTipoEvento">
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxComboBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>"
                                                                                                    EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" ClientInstanceName="ddlInicioPrevisto"
                                                                                                    ID="ddlInicioPrevisto">
                                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">


                                                                                                        <DayHeaderStyle></DayHeaderStyle>
                                                                                                        <DayStyle></DayStyle>
                                                                                                        <DayWeekendStyle>
                                                                                                        </DayWeekendStyle>
                                                                                                        <Style></Style>
                                                                                                    </CalendarProperties>
                                                                                                    <ClientSideEvents DateChanged="function(s, e) {
	ddlTerminoPrevisto.SetDate(s.GetValue());
	calendar = ddlTerminoPrevisto.GetCalendar();
  	if ( calendar )
    	calendar.minDate = new Date(s.GetValue());
}"></ClientSideEvents>
                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxDateEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxTextBox runat="server" Width="60px" ClientInstanceName="txtHoraInicio"
                                                                                                    ID="txtHoraInicio">
                                                                                                    <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>"
                                                                                                    EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" ClientInstanceName="ddlTerminoPrevisto"
                                                                                                    ID="ddlTerminoPrevisto">
                                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                        <DayHeaderStyle></DayHeaderStyle>
                                                                                                        <DayStyle></DayStyle>
                                                                                                        <DaySelectedStyle>
                                                                                                        </DaySelectedStyle>
                                                                                                        <DayOtherMonthStyle>
                                                                                                        </DayOtherMonthStyle>
                                                                                                        <DayWeekendStyle>
                                                                                                        </DayWeekendStyle>
                                                                                                        <DayOutOfRangeStyle>
                                                                                                        </DayOutOfRangeStyle>
                                                                                                        <ButtonStyle>
                                                                                                        </ButtonStyle>
                                                                                                        <HeaderStyle></HeaderStyle>
                                                                                                    </CalendarProperties>
                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxDateEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxTextBox runat="server" Width="60px" ClientInstanceName="txtHoraTermino"
                                                                                                    ID="txtHoraTermino">
                                                                                                    <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="formulario-label">
                                                                                <asp:Label runat="server" Text="<%$ Resources:traducao, reuniaoTecnicaPlanejamento_local_ %>" ID="lblLocal"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="memoLocal"
                                                                                    ID="memoLocal">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                        <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="formulario-label">
                                                                                <asp:Label runat="server" Text="<%$ Resources:traducao, reuniaoTecnicaPlanejamento_pauta_ %>" ID="lblPauta"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="memoPauta" Width="775px"
                                                                                    Height="180px" ID="memoPauta" Theme="MaterialCompact">

<SettingsHtmlEditing>
<PasteFiltering Attributes="class"></PasteFiltering>
</SettingsHtmlEditing>

                                                                                    <Styles>
                                                                                        <ContentArea>
                                                                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                                                        </ContentArea>
                                                                                    </Styles>
                                                                                    <Toolbars>
                                                                                        <dxhe:HtmlEditorToolbar>
                                                                                            <Items>
                                                                                                <dxhe:ToolbarCutButton>
                                                                                                </dxhe:ToolbarCutButton>
                                                                                                <dxhe:ToolbarCopyButton>
                                                                                                </dxhe:ToolbarCopyButton>
                                                                                                <dxhe:ToolbarPasteButton>
                                                                                                </dxhe:ToolbarPasteButton>
                                                                                                <dxhe:ToolbarPasteFromWordButton>
                                                                                                </dxhe:ToolbarPasteFromWordButton>
                                                                                                <dxhe:ToolbarUndoButton BeginGroup="True">
                                                                                                </dxhe:ToolbarUndoButton>
                                                                                                <dxhe:ToolbarRedoButton>
                                                                                                </dxhe:ToolbarRedoButton>
                                                                                                <dxhe:ToolbarRemoveFormatButton BeginGroup="True">
                                                                                                </dxhe:ToolbarRemoveFormatButton>
                                                                                                <dxhe:ToolbarSuperscriptButton BeginGroup="True">
                                                                                                </dxhe:ToolbarSuperscriptButton>
                                                                                                <dxhe:ToolbarSubscriptButton>
                                                                                                </dxhe:ToolbarSubscriptButton>
                                                                                                <dxhe:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                                                </dxhe:ToolbarInsertOrderedListButton>
                                                                                                <dxhe:ToolbarInsertUnorderedListButton>
                                                                                                </dxhe:ToolbarInsertUnorderedListButton>
                                                                                                <dxhe:ToolbarIndentButton BeginGroup="True">
                                                                                                </dxhe:ToolbarIndentButton>
                                                                                                <dxhe:ToolbarOutdentButton>
                                                                                                </dxhe:ToolbarOutdentButton>
                                                                                                <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                                                </dxhe:ToolbarInsertLinkDialogButton>
                                                                                                <dxhe:ToolbarUnlinkButton>
                                                                                                </dxhe:ToolbarUnlinkButton>
                                                                                                <dxhe:ToolbarInsertImageDialogButton Visible="false">
                                                                                                </dxhe:ToolbarInsertImageDialogButton>
                                                                                                <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                                                    <Items>
                                                                                                        <dxhe:ToolbarInsertTableDialogButton ViewStyle="ImageAndText" BeginGroup="True">
                                                                                                        </dxhe:ToolbarInsertTableDialogButton>
                                                                                                        <dxhe:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                                                        </dxhe:ToolbarTablePropertiesDialogButton>
                                                                                                        <dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                                        </dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                                        <dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                                        </dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                                        <dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                                        </dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                                        <dxhe:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                                                        </dxhe:ToolbarInsertTableRowAboveButton>
                                                                                                        <dxhe:ToolbarInsertTableRowBelowButton>
                                                                                                        </dxhe:ToolbarInsertTableRowBelowButton>
                                                                                                        <dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                                        </dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                                        <dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                                        </dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                                        <dxhe:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                                                        </dxhe:ToolbarSplitTableCellHorizontallyButton>
                                                                                                        <dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                                        </dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                                        <dxhe:ToolbarMergeTableCellRightButton>
                                                                                                        </dxhe:ToolbarMergeTableCellRightButton>
                                                                                                        <dxhe:ToolbarMergeTableCellDownButton>
                                                                                                        </dxhe:ToolbarMergeTableCellDownButton>
                                                                                                        <dxhe:ToolbarDeleteTableButton BeginGroup="True">
                                                                                                        </dxhe:ToolbarDeleteTableButton>
                                                                                                        <dxhe:ToolbarDeleteTableRowButton>
                                                                                                        </dxhe:ToolbarDeleteTableRowButton>
                                                                                                        <dxhe:ToolbarDeleteTableColumnButton>
                                                                                                        </dxhe:ToolbarDeleteTableColumnButton>
                                                                                                    </Items>
                                                                                                </dxhe:ToolbarTableOperationsDropDownButton>
                                                                                                <dxhe:ToolbarFullscreenButton>
                                                                                                </dxhe:ToolbarFullscreenButton>
                                                                                            </Items>
                                                                                        </dxhe:HtmlEditorToolbar>
                                                                                        <dxhe:HtmlEditorToolbar>
                                                                                            <Items>
                                                                                                <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                                                    <Items>
                                                                                                        <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                                                    </Items>
                                                                                                </dxhe:ToolbarParagraphFormattingEdit>
                                                                                                <dxhe:ToolbarFontNameEdit>
                                                                                                    <Items>
                                                                                                        <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                                                                    </Items>
                                                                                                </dxhe:ToolbarFontNameEdit>
                                                                                                <dxhe:ToolbarFontSizeEdit>
                                                                                                    <Items>
                                                                                                        <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                                                                        <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                                                                    </Items>
                                                                                                </dxhe:ToolbarFontSizeEdit>
                                                                                                <dxhe:ToolbarBoldButton BeginGroup="True">
                                                                                                </dxhe:ToolbarBoldButton>
                                                                                                <dxhe:ToolbarItalicButton>
                                                                                                </dxhe:ToolbarItalicButton>
                                                                                                <dxhe:ToolbarUnderlineButton>
                                                                                                </dxhe:ToolbarUnderlineButton>
                                                                                                <dxhe:ToolbarStrikethroughButton>
                                                                                                </dxhe:ToolbarStrikethroughButton>
                                                                                                <dxhe:ToolbarJustifyLeftButton BeginGroup="True">
                                                                                                </dxhe:ToolbarJustifyLeftButton>
                                                                                                <dxhe:ToolbarJustifyCenterButton>
                                                                                                </dxhe:ToolbarJustifyCenterButton>
                                                                                                <dxhe:ToolbarJustifyRightButton>
                                                                                                </dxhe:ToolbarJustifyRightButton>
                                                                                                <dxhe:ToolbarJustifyFullButton>
                                                                                                </dxhe:ToolbarJustifyFullButton>
                                                                                                <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                                                                </dxhe:ToolbarBackColorButton>
                                                                                                <dxhe:ToolbarFontColorButton>
                                                                                                </dxhe:ToolbarFontColorButton>
                                                                                            </Items>
                                                                                        </dxhe:HtmlEditorToolbar>
                                                                                    </Toolbars>
                                                                                    <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                                                </dxhe:ASPxHtmlEditor>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                                    </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Name="TabB" Text="Participantes">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                               <div id="divParticipante" style="height:380px;overflow-y:auto">
                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="formulario-label" style="width: 45%;">
                                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblSelecionado"
                                                                                    Text="Disponíveis:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 10%;"></td>
                                                                            <td class="formulario-label" style="width: 45%;">
                                                                                <dxe:ASPxLabel runat="server" Text="Selecionados:" ClientInstanceName="lblSelecionado"
                                                                                    ID="lblSelecionado">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" style="width: 45%;">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxListBox ID="lbDisponiveis" runat="server" ClientInstanceName="lbDisponiveis" EnableClientSideAPI="True" EnableSelectAll="False" EncodeHtml="False" Height="120px" OnCallback="lbDisponiveis_Callback" Rows="10" SelectionMode="CheckColumn" Width="100%">
                                                                                                    <SettingsLoadingPanel Text="" />
                                                                                                    <FilteringSettings ShowSearchUI="True" />
                                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}" />
                                                                                                    <ValidationSettings>
                                                                                                        <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" Width="14px">
                                                                                                        </ErrorImage>
                                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                        </ErrorFrameStyle>
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxListBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" ClientInstanceName="lblSelecionado" Text="Grupos:">
                                                                                                </dxtv:ASPxLabel>
                                                                                                <dxtv:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                                                                                                    <ClientSideEvents BeginCallback="function(s, e) {
	mostraDivAtualizando(traducao.reuniaoTecnicaPlanejamento_atualizando___); 
}"
                                                                                                        EndCallback="function(s, e) 
{
	var delimitador = &quot;¥&quot;;
  	var listaCodigos = s.cp_ListaCodigos;
  	var arrayItens = listaCodigos.split(';');

    //arrayItens.sort();
    var array3 = new Array();

    for (i = 0; i &lt; arrayItens.length; i++)
    {
        temp = arrayItens[i].split(delimitador);
        if((temp[0] != null) &amp;&amp; (temp[1] != null))
        {
           array3.push(temp[1]);
        }
    }
    //lbDisponiveis.BeginUpdate(); 
    lbDisponiveis.UnselectAll();
    lbDisponiveis.SelectValues(array3);
    //lbDisponiveis.EndUpdate();    

    UpdateButtons();    
    setTimeout('fechaTelaEdicao();', 10);
}" />
                                                                                                </dxtv:ASPxCallback>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxListBox ID="lbGrupos" runat="server" ClientInstanceName="lbGrupos" EnableClientSideAPI="True" EncodeHtml="False" Height="120px" Rows="10" SelectionMode="CheckColumn" Width="100%">
                                                                                                    <SettingsLoadingPanel Text="" />
                                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
    callback.PerformCallback(s.GetValue());
}"
                                                                                                        ValueChanged="function(s, e) {
	callback.PerformCallback(s.GetValue());
}" />
                                                                                                    <ValidationSettings>
                                                                                                        <ErrorImage Height="14px" Url="~/App_Themes/MaterialCompact/Editors/edtError.png" Width="14px">
                                                                                                        </ErrorImage>
                                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                        </ErrorFrameStyle>
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxListBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>&nbsp; </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td style="width: 10%;" align="center">
                                                                                <table cellspacing="2" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodos"
                                                                                                    ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="60px" Height="25px"
                                                                                                    Font-Bold="True" ID="btnADDTodos">
                                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveTodosItens(lbDisponiveis,lbSelecionados);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>
                                                                                                </dxe:ASPxButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADD" ClientEnabled="False"
                                                                                                    Text="&gt;" EncodeHtml="False" Width="60px" Height="25px" Font-Bold="True"
                                                                                                    ID="btnADD">
                                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbDisponiveis, lbSelecionados);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>
                                                                                                </dxe:ASPxButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMV" ClientEnabled="False"
                                                                                                    Text="&lt;" EncodeHtml="False" Width="60px" Height="25px" Font-Bold="True"
                                                                                                    ID="btnRMV">
                                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbSelecionados, lbDisponiveis);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>
                                                                                                </dxe:ASPxButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodos"
                                                                                                    ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="60px" Height="25px"
                                                                                                    Font-Bold="True" ID="btnRMVTodos">
                                                                                                    <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
	lb_moveTodosItens(lbSelecionados, lbDisponiveis);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>
                                                                                                </dxe:ASPxButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td valign="top" style="width: 45%;">
                                                                                <dxe:ASPxListBox runat="server" EnableSynchronization="True" EncodeHtml="False" Rows="10"
                                                                                    SelectionMode="CheckColumn"
                                                                                    ClientInstanceName="lbSelecionados" EnableClientSideAPI="True" Width="100%" Height="120px"
                                                                                    ID="lbSelecionados" OnCallback="lbSelecionados_Callback" EnableSelectAll="False">

                                                                                    <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                                                    <FilteringSettings ShowSearchUI="True" />
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>
                                                                                    <ValidationSettings>
                                                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                        </ErrorImage>
                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                        </ErrorFrameStyle>
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxListBox>
                                                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfRiscosSelecionados" ID="hfRiscosSelecionados">
                                                                                </dxhf:ASPxHiddenField>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                   </div>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Name="tabProjetos" Text="Projetos">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvProjetos" KeyFieldName="Codigo"
                                                                    AutoGenerateColumns="False" Width="100%"
                                                                    ID="gvProjetos" OnAfterPerformCallback="gvProjetos_AfterPerformCallback" OnCustomCallback="gvProjetos_CustomCallback">
                                                                    <Columns>
                                                                        <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" Width="5%" Caption=" " VisibleIndex="0">
                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            <HeaderTemplate>
                                                                                <input type="checkbox" onclick="gvProjetos.SelectAllRowsOnPage(this.checked);" title="<%# Resources.traducao.reuniaoTecnicaPlanejamento_marcar_desmarcar_todos %>" />
                                                                            </HeaderTemplate>
                                                                        </dxwgv:GridViewCommandColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Desempenho" Width="5%" Caption=" " VisibleIndex="1">
                                                                            <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif' /&gt;">
                                                                            </PropertiesTextEdit>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Descricao" Width="90%" Caption="Projetos"
                                                                            VisibleIndex="2">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                    </SettingsPager>
                                                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="260"></Settings>
                                                                    <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                                                    <Styles>
                                                                        <SelectedRow BackColor="Transparent" ForeColor="Black">
                                                                        </SelectedRow>
                                                                    </Styles>
                                                                </dxwgv:ASPxGridView>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Name="tabAnexos" Text="Anexos">
                                                        <ContentCollection>
                                                            <dxw:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                                                                <iframe id="frmAnexosReuniao" frameborder="0" scrolling="yes" allowtransparency="False"
                                                                    height="380" width="100%"></iframe>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                </TabPages>
                                                <ClientSideEvents ActiveTabChanged="function(s, e) {
	// se for a aba 'Participantes', atualiza a disponibilidade dos botões de 'seleção'
    if(s.GetActiveTab().index == 1)
    {	    
    	UpdateButtons();
    }

}"
                                                    ActiveTabChanging="function(s, e) {
	e.cancel = podeMudarAba(s, e)
}"></ClientSideEvents>
                                                <ContentStyle>
                                                    <Paddings PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px"></Paddings>
                                                </ContentStyle>
                                            </dxtc:ASPxPageControl>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table class="formulario-botoes" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxCallbackPanel ID="callbackEnviaPauta" runat="server" ClientInstanceName="callbackEnviaPauta" OnCallback="callbackEnviaPauta_Callback">
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <dxtv:ASPxButton ID="btnEnviarPauta" runat="server" AutoPostBack="False" ClientInstanceName="btnEnviarPauta" EnableViewState="False" Text="Enviar Pauta" ValidationGroup="MKE" Width="110px">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;
	var podeEditar = btnEnviarPauta.cp_EditaMensagem;
	if(podeEditar == 'N')
	{
                                window.top.mostraMensagem(traducao.reuniaoTecnicaPlanejamento_a_reuni_o_ser__salva__deseja_continuar_, 'confirmacao', true, true, SalvaEEnviaPauta);
	}
	else
	{
		pcMensagemPauta.Show();
	}
}" />
                                                                <Paddings Padding="1px" />
                                                            </dxtv:ASPxButton>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxCallbackPanel>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1"
                                                    Text="Salvar" ValidationGroup="MKE" Width="110px"
                                                    ID="btnSalvar" EnableViewState="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	capturaCodigosInteressados();
	if(verificarDadosPreenchidos())
		if (window.onClick_btnSalvar)
	    	onClick_btnSalvar();
	else
		return false;
}"></ClientSideEvents>
                                                    <Paddings Padding="1px" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <%-- <td>
                                                                    </td>--%>
                                            <td style="width: 90px">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar2"
                                                    Text="Fechar" Width="110px" ID="btnFechar2"
                                                    EnableViewState="False">
                                                    <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
	desabilitaHabilitaComponentes();
    memoPauta.SetHtml('');
}"></ClientSideEvents>
                                                    <Paddings Padding="1px" />
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
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxhf:ASPxHiddenField>
    <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
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
</asp:Content>
