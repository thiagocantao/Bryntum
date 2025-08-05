<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_CadastroWorkflows.aspx.cs" Inherits="administracao_adm_CadastroWorkflows"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <div id="divGrid" style="visibility: hidden; padding-top: 5px">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoFluxo"
                                    AutoGenerateColumns="False" ID="gvDados"
                                    OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnDetailRowExpandedChanged="gvDados_DetailRowExpandedChanged"
                                    Width="100%" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                    KeyboardSupport="True" OnAfterPerformCallback="gvDados_AfterPerformCallback"
                                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                        DetailRowExpanding="function(s, e) {
	// cpDetailRowIndex ser&#225; usada ao se clicar em NovoWorkflow para a linha em quest&#227;o;
	gvDados.cpDetailRowIndex = e.visibleIndex;
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
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar1.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
	 else if ( e.buttonID == 'btnTipoProjeto' )
	{
		e.processOnServer = false;
		pcDadosAssociacao.Show();
	}
}
"
                                        Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="130px" VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Alterar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Visualizar Detalhes">
                                                    <Image Url="~/imagens/botoes/pFormulario.png">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnTipoProjeto" Text="Administrar Tipos de Projetos Relacionados ao Modelo">
                                                    <Image AlternateText="Administrar Tipos de Projetos Relacionados ao Modelo" Url="~/imagens/botoes/permissoes.png">
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
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoFluxo" Caption="C&#243;digo" Visible="False"
                                            VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomeFluxo" Width="250px" Caption="Nome"
                                            VisibleIndex="2">
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Status" Width="150px" Caption="Status" Visible="False"
                                            VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Descricao" Caption="Descri&#231;&#227;o"
                                            VisibleIndex="4">
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Observacao" Visible="False" VisibleIndex="7">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="StatusFluxo" Visible="False" VisibleIndex="8">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="CodigoGrupoFluxo" FieldName="CodigoGrupoFluxo"
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="podeExcluir" FieldName="podeExcluir" ShowInCustomizationForm="True"
                                            Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Iniciais Fluxo" FieldName="IniciaisFluxo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                        </dxtv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" EnableRowHotTrack="True"></SettingsBehavior>
                                    <SettingsResizing  ColumnResizeMode="Control"/>
                                    <SettingsPager PageSize="100">
                                    </SettingsPager>
                                    <SettingsEditing EditFormColumnCount="1">
                                    </SettingsEditing>
                                    <SettingsPopup>
                                        <EditForm Modal="true" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                    </SettingsPopup>
                                    <Settings ShowFooter="True" VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>
                                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                                    <Styles>
                                        <AlternatingRow Enabled="True">
                                        </AlternatingRow>
                                    </Styles>
                                    <Templates>
                                        <DetailRow>
                                            <dxwgv:ASPxGridView ID="gvWorkflows" runat="server"
                                                ClientInstanceName="gvWorkFlows" Width="100%" OnCustomButtonInitialize="gvWorkflows_CustomButtonInitialize"
                                                OnCustomButtonCallback="gvWorkflows_CustomButtonCallback" OnBeforePerformDataSelect="gvWorkflows_BeforePerformDataSelect"
                                                AutoGenerateColumns="False" KeyFieldName="CodigoWorkflow" OnHtmlRowCreated="gvWorkflows_HtmlRowCreated"
                                                OnLoad="gvWorkflows_Load">
                                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                <SettingsPager Mode="ShowAllRecords">
                                                    <AllButton Text="All">
                                                    </AllButton>
                                                    <Summary AllPagesText="{0} - {1} ({2})" Text="{0} de {1}"></Summary>
                                                </SettingsPager>
                                                <SettingsEditing Mode="PopupEditForm">
                                                </SettingsEditing>
                                                <SettingsPopup>
                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                        AllowResize="True" Height="300px" Width="600px" />
                                                </SettingsPopup>
                                                <SettingsText ConfirmDelete="<%$ Resources:traducao, adm_CadastroWorkflows_confirma_exclus_o_desta_vers_o_ %>" EmptyDataRow="<%$ Resources:traducao, adm_CadastroWorkflows__table_cellpadding__0__cellspacing__0__style__width__100_____tr__td_align__center____td__td_style__width__50px____td___tr__tr__td_align__center__nenhuma_vers_o_cadastrada_para_esse_modelo_de_fluxo___td__td_style__width__50px____td___tr___table_ %>"></SettingsText>
                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;btnNovo&quot;)
	{
        trataClickBotaoNovoWorkflow(gvDados);
		e.processOnServer = false;
	}
	else if(e.buttonID == &quot;btnExcluir&quot;)
	{
	    e.processOnServer = confirm(traducao.adm_CadastroWorkflows_confirma_exclus_o_desta_vers_o_);
	}
	else if(e.buttonID == &quot;btnEditar&quot;)
	{ 
        trataClickBotaoEditarWorkflow(s);
		e.processOnServer = false;
	}
     else if ( e.buttonID == 'btnCopiaFluxo' )
	{
        lpLoading.Show();
		e.processOnServer = true;
        trataClickBotaoCopiarWorkflow(s);
	}
}
"></ClientSideEvents>
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" Caption="A&#231;&#245;es"
                                                        ShowClearFilterButton="true" ShowCancelButton="true" ShowUpdateButton="true"
                                                        VisibleIndex="0">
                                                        <CustomButtons>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                                </Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                </Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnCopiaFluxo" Text="Copiar Fluxo">
                                                                <Image AlternateText="Copiar Fluxo" Url="~/imagens/botoes/btnDuplicar.png">
                                                                </Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                        <HeaderTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <dxm:ASPxMenu ID="menu1" runat="server" BackColor="Transparent" ClientInstanceName="menu1"
                                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init1">
                                                                            <Paddings Padding="0px" />
                                                                            <Items>
                                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="<%$ Resources:traducao, adm_CadastroWorkflows_incluir %>">
                                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="<%$ Resources:traducao, adm_CadastroWorkflows_exportar %>">
                                                                                    <Items>
                                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<%$ Resources:traducao, adm_CadastroWorkflows_exportar_para_xls %>">
                                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<%$ Resources:traducao, adm_CadastroWorkflows_exportar_para_pdf %>">
                                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<%$ Resources:traducao, adm_CadastroWorkflows_exportar_para_rtf %>">
                                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="<%$ Resources:traducao, adm_CadastroWorkflows_exportar_para_html %>" ClientVisible="False">
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
                                                                                        <dxm:MenuItem Text="Salvar" ToolTip="<%$ Resources:traducao, adm_CadastroWorkflows_salvar_layout %>">
                                                                                            <Image IconID="save_save_16x16">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="<%$ Resources:traducao, adm_CadastroWorkflows_restaurar_layout %>">
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
                                                    <dxwgv:GridViewDataTextColumn FieldName="VersaoWorkflow" Width="60px" Caption="<%$ Resources:traducao, adm_CadastroWorkflows_vers_o %>"
                                                        VisibleIndex="1">
                                                        <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                            AllowSort="False" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoVersao" Name="DescricaoVersao"
                                                        Caption="<%$ Resources:traducao, adm_CadastroWorkflows_descri__o_da_vers_o %>" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="<%$ Resources:traducao, adm_CadastroWorkflows_criada_em %>" FieldName="DataCriacao" VisibleIndex="3"
                                                        Width="135px">
                                                        <PropertiesDateEdit DisplayFormatString="">
                                                        </PropertiesDateEdit>
                                                        <Settings ShowFilterRowMenu="True" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="<%$ Resources:traducao, adm_CadastroWorkflows_publicada_em %>" FieldName="DataPublicacao" VisibleIndex="4"
                                                        Width="135px">
                                                        <PropertiesDateEdit DisplayFormatString="">
                                                        </PropertiesDateEdit>
                                                        <Settings ShowFilterRowMenu="True" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="<%$ Resources:traducao, adm_CadastroWorkflows_revogada_em %>" FieldName="DataRevogacao" VisibleIndex="5"
                                                        Width="135px">
                                                        <PropertiesDateEdit DisplayFormatString="">
                                                        </PropertiesDateEdit>
                                                        <Settings ShowFilterRowMenu="True" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoWorkflow" Caption="<%$ Resources:traducao, adm_CadastroWorkflows_c_digo %>" VisibleIndex="6"
                                                        Visible="False">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoFluxo" Caption="<%$ Resources:traducao, adm_CadastroWorkflows_c_digo_do_fluxo %>" Visible="False"
                                                        VisibleIndex="7">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings VerticalScrollBarMode="Visible" ShowFooter="True"></Settings>
                                                <Templates>
                                                    <FooterRow>
                                                        <table class="grid-legendas">
                                                            <tr>
                                                                <td class="grid-legendas-cor grid-legendas-cor-versao-em-uso"><span></span></td>
                                                                <td class="grid-legendas-label grid-legendas-label-versao-em-uso">
                                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                        Font-Strikeout="False" Text="<%# Resources.traducao.adm_CadastroWorkflows_vers_o_em_uso %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="grid-legendas-asterisco"><span>*</span></td>
                                                                <td class="grid-legendas-label">
                                                                    <dxe:ASPxLabel ID="lblCopia" runat="server" ClientInstanceName="lblCopia"
                                                                        Text="<%# Resources.traducao.adm_CadastroWorkflows_a_c_pia_de_um_modelo_de_fluxo_s__poder__ser_efetuada_para_um_modelo_de_fluxo_com_vers_o_criada_e_n_o_publicada_ %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </FooterRow>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
                                        </DetailRow>
                                        <FooterRow>
                                            <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td class="grid-legendas-cor grid-legendas-cor-inativo"><span></span></td>
                                                        <td class="grid-legendas-label grid-legendas-label-inativo">
                                                            <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                                Text="<%# Resources.traducao.adm_CadastroWorkflows_modelos_de_fluxos_inativos %>">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </FooterRow>
                                    </Templates>
                                </dxwgv:ASPxGridView>
                            </div>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" HeaderText="Detalhes do Modelo de Fluxo"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                Width="1000px" Height="80px" ID="pcDados"
                                CloseAction="None" CssClass="popup" AllowDragging="True" ShowFooter="True">
                                <ClientSideEvents CloseUp="function(s, e) {
	e.processOnServer = false;
	pcDados_OnCloseUp(s,e);
}"
                                    PopUp="function(s, e) {
	e.processOnServer = false;
    pageControl.SetActiveTabIndex(0);
    memoJsonSchema.SetText('');
    btnCopyToClipboard.SetVisible(false);
	pcDados_OnPopup(s,e);
}" ></ClientSideEvents>
                                <CloseButtonImage Height="16px" Width="17px">
                                </CloseButtonImage>
                                <SizeGripImage Height="12px" Width="12px">
                                </SizeGripImage>
                                <FooterTemplate>
                                    <table style="width: 100%">
<tr>
                                                <td align="right">
                                                    <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="padding-right: 5px">
                                                                    <dxtv:ASPxButton ID="btnSalvar1" runat="server" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
		onClick_btnSalvar();
}" />
                                                                        <Paddings Padding="0px" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <dxtv:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
    //debugger;
    gvProjetosFluxo.PerformCallback(-1);
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                        <Paddings Padding="0px" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
											</table>
                                </FooterTemplate>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" Width="100%" ClientInstanceName="pageControl" OnCallback="pageControl_Callback">
                                                        <ClientSideEvents ActiveTabChanged="function(s,e){
                                                                if(e.tab.name === 'tabIntegracoes'){
                                                                    s.PerformCallback('init');
                                                                    lpAguardeMasterPage.Show();
                                                                }
                                                            }" EndCallback="function(s,e){lpAguardeMasterPage.Hide();}" />
                                                        <TabPages>
                                                            <dxtv:TabPage Text="Detalhes">
                                                                <ContentCollection>
                                                                    <dxtv:ContentControl runat="server">
                                                                        <div id="divDetalhes">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" class="formulario-colunas" width="100%">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" Text="Nome:">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td style="padding-left: 5px">
                                                                                                            <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" Text="Status:">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxTextBox ID="txtNomeFluxo" runat="server" ClientInstanceName="txtNomeFluxo" MaxLength="30" Width="100%">
                                                                                                                <ValidationSettings>
                                                                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                                                    </ErrorImage>
                                                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                                    </ErrorFrameStyle>
                                                                                                                </ValidationSettings>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxtv:ASPxTextBox>
                                                                                                        </td>
                                                                                                        <td style="padding-left: 5px">
                                                                                                            <dxtv:ASPxComboBox ID="ddlStatusFluxo" runat="server" ClientInstanceName="ddlStatusFluxo" Width="100%">
                                                                                                                <Items>
                                                                                                                    <dxtv:ListEditItem Text="Ativo" Value="A" />
                                                                                                                    <dxtv:ListEditItem Text="Desativado" Value="D" />
                                                                                                                </Items>
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
                                                                                        <td>
                                                                                            <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Descrição:">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxTextBox ID="txtDescricao" runat="server" ClientInstanceName="txtDescricao" MaxLength="128" Width="100%">
                                                                                                <ValidationSettings>
                                                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                                    </ErrorImage>
                                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                    </ErrorFrameStyle>
                                                                                                </ValidationSettings>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxtv:ASPxTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                                <tr>
                                                                                                    <td style="width: 100%">
                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel1012" runat="server" Text="Grupo do Fluxo:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="padding-left: 5px; width: 250px;" id="tdTagControle" runat="server">
                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel1013" runat="server" Text="Tag de controle:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 100%">
                                                                                                        <dxtv:ASPxComboBox ID="ddlGrupoFluxo" runat="server" ClientInstanceName="ddlGrupoFluxo" IncrementalFilteringDelay="250" Width="100%">
                                                                                                            <Items>
                                                                                                                <dxtv:ListEditItem Text="Ativo" Value="A" />
                                                                                                                <dxtv:ListEditItem Text="Desativado" Value="D" />
                                                                                                            </Items>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td style="padding-left: 5px; width: 250px;" id="tdTagControle1" runat="server">
                                                                                                        <dxtv:ASPxTextBox ID="txtIniciaisFluxo" runat="server" ClientInstanceName="txtIniciaisFluxo" Width="100%">
                                                                                                            <ValidationSettings>
                                                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                                                </ErrorImage>
                                                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                                </ErrorFrameStyle>
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Observação:">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxMemo ID="txtObservacao" runat="server" ClientInstanceName="txtObservacao" Rows="4" Width="100%">
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxtv:ASPxMemo>
                                                                                        </td>
                                                                                    </tr>

                                                                                </tbody>
                                                                            </table>
                                                                        </div>

                                                                    </dxtv:ContentControl>
                                                                </ContentCollection>
                                                            </dxtv:TabPage>
                                                            <dxtv:TabPage Text="Projetos Relacionados">
                                                                <ContentCollection>
                                                                    <dxtv:ContentControl runat="server">
                                                                        <dxtv:ASPxCallbackPanel ID="pnCallbackgvProjetosFluxo" runat="server" ClientInstanceName="pnCallbackgvProjetosFluxo" OnCallback="pnCallbackgvProjetosFluxo_Callback" Width="100%">
                                                                            <ClientSideEvents EndCallback="function(s, e) {
   onEnd_pnCallbackDisparoFluxo();
}" />
                                                                            <PanelCollection>
                                                                                <dxtv:PanelContent runat="server">
                                                                                    <dxtv:ASPxHiddenField ID="hfGeral1" runat="server" ClientInstanceName="hfGeral1">
                                                                                    </dxtv:ASPxHiddenField>
                                                                                    <dxtv:ASPxGridView ID="gvProjetosFluxo" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvProjetosFluxo" DataSourceID="sdsProjetosFluxo" EnableRowsCache="False" EnableViewState="False" KeyFieldName="CodigoFluxo;CodigoProjeto" OnCustomButtonInitialize="gvProjetosFluxo_CustomButtonInitialize" OnCustomCallback="gvProjetosFluxo_CustomCallback1" Width="100%">
                                                                                        <ClientSideEvents CustomButtonClick="function(s, e) {
	 gvProjetosFluxo.SetFocusedRowIndex(e.visibleIndex);
     //debugger
     if(e.buttonID == &quot;btnEditarDisp&quot;)
     {
		onClickBarraNavegacaoDisparoFluxo(&quot;Editar&quot;, gvProjetosFluxo, pcDadosDisparoFluxoProjetos);
        hfGeral1.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluirDisp&quot;)
     {		
		onClickBarraNavegacaoDisparoFluxo(&quot;Excluir&quot;, gvProjetosFluxo, pcDadosDisparoFluxoProjetos);
     }
     else if(e.buttonID == &quot;btnDetalhesCustomDisp&quot;)
     {	
        OnGridFocusedRowChangedDisparoFluxo(gvProjetosFluxo, true);
	    btnSalvarDisparo.SetVisible(false);
		hfGeral1.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDadosDisparoFluxoProjetos.Show();
     }
}" />
                                                                                        <SettingsPager PageSize="100">
                                                                                        </SettingsPager>
                                                                                        <Settings ShowFilterRow="True" ShowTitlePanel="False" VerticalScrollBarMode="Visible" />
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                                                                                        <SettingsText CommandClearFilter="Limpar filtro" />
                                                                                        <Columns>
                                                                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ShowClearFilterButton="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="100px">
                                                                                                <CustomButtons>
                                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnEditarDisp" Text="Editar">
                                                                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                        </Image>
                                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnExcluirDisp" Text="Desativar disparo automatico">
                                                                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                        </Image>
                                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnDetalhesCustomDisp" Text="Mostrar Detalhes">
                                                                                                        <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                                                                        </Image>
                                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                                </CustomButtons>
                                                                                                <HeaderTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <dxtv:ASPxMenu ID="menu_Disparo" runat="server" BackColor="Transparent" ClientInstanceName="menu_Disparo" ItemSpacing="5px" OnInit="menu_Disparo_Init" OnItemClick="menu_Disparo_ItemClick">
                                                                                                                    <Paddings Padding="0px" />
                                                                                                                    <Items>
                                                                                                                        <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                                                            <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                                                            </Image>
                                                                                                                        </dxtv:MenuItem>
                                                                                                                        <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                                                            <Items>
                                                                                                                                <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                                                                    <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                                                    </Image>
                                                                                                                                </dxtv:MenuItem>
                                                                                                                                <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                                                                    <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                                                    </Image>
                                                                                                                                </dxtv:MenuItem>
                                                                                                                                <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                                                                    <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                                                    </Image>
                                                                                                                                </dxtv:MenuItem>
                                                                                                                                <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                                                                                    <Image Url="~/imagens/menuExportacao/html.png">
                                                                                                                                    </Image>
                                                                                                                                </dxtv:MenuItem>
                                                                                                                                <dxtv:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                                                                    <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                                                                    </Image>
                                                                                                                                </dxtv:MenuItem>
                                                                                                                            </Items>
                                                                                                                            <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                                                            </Image>
                                                                                                                        </dxtv:MenuItem>
                                                                                                                        <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                                                                                            <Items>
                                                                                                                                <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                                                                    <Image IconID="save_save_16x16">
                                                                                                                                    </Image>
                                                                                                                                </dxtv:MenuItem>
                                                                                                                                <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                                                                    <Image IconID="actions_reset_16x16">
                                                                                                                                    </Image>
                                                                                                                                </dxtv:MenuItem>
                                                                                                                            </Items>
                                                                                                                            <Image Url="~/imagens/botoes/layout.png">
                                                                                                                            </Image>
                                                                                                                        </dxtv:MenuItem>
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
                                                                                                                </dxtv:ASPxMenu>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                            </dxtv:GridViewCommandColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Nome do Projeto" FieldName="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="3" Width="300px">
                                                                                                <Settings AllowSort="False" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="CodigoProjeto" FieldName="CodigoProjeto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Última Ativação" FieldName="DataAtivacao" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                                                                <PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy}">
                                                                                                </PropertiesTextEdit>
                                                                                                <Settings AllowSort="False" />
                                                                                                <CellStyle Wrap="True">
                                                                                                </CellStyle>
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Última Desativação" FieldName="DataDesativacao" ShowInCustomizationForm="True" VisibleIndex="5" Width="90px">
                                                                                                <PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy}">
                                                                                                </PropertiesTextEdit>
                                                                                                <Settings AllowSort="False" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Periodicidade" FieldName="DescricaoPeriodicidade_PT" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                                <Settings AllowSort="False" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="CodigoFluxo" FieldName="CodigoFluxo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Próxima Execução" FieldName="DataProximaExecucao" ShowInCustomizationForm="True" VisibleIndex="8">
                                                                                                <PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy}">
                                                                                                </PropertiesTextEdit>
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataComboBoxColumn Caption="Disparo Automático" FieldName="indicaControlado" ShowInCustomizationForm="True" VisibleIndex="9" Width="100px">
                                                                                                <PropertiesComboBox EnableFocusedStyle="False">
                                                                                                    <Items>
                                                                                                        <dxtv:ListEditItem Text="Sim" Value="S" />
                                                                                                        <dxtv:ListEditItem Text="Não" Value="N" />
                                                                                                        <dxtv:ListEditItem />
                                                                                                    </Items>
                                                                                                </PropertiesComboBox>
                                                                                                <HeaderStyle Wrap="True" />
                                                                                                <CellStyle Wrap="True">
                                                                                                </CellStyle>
                                                                                                <FooterCellStyle Wrap="True">
                                                                                                </FooterCellStyle>
                                                                                            </dxtv:GridViewDataComboBoxColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Primeira Execução" FieldName="DataPrimeiraExecucao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                                                <PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy}">
                                                                                                </PropertiesTextEdit>
                                                                                                <CellStyle Wrap="True">
                                                                                                </CellStyle>
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Codigo usuario ativação" FieldName="IdentificadorUsuarioAtivacao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Usuario Ativação" FieldName="UsuarioAtivacao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Codigo Usuario Desativação" FieldName="IdentificadorUsuarioDesativacao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Nome Usuario Desativação" FieldName="UsuarioDesativacao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Codigo Periodicidade" FieldName="CodigoPeriodicidade" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="existeRegistro " FieldName="existeRegistro " ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <Styles>
                                                                                            <Header Wrap="True">
                                                                                            </Header>
                                                                                            <Footer Wrap="True">
                                                                                            </Footer>
                                                                                        </Styles>
                                                                                    </dxtv:ASPxGridView>
                                                                                </dxtv:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxtv:ASPxCallbackPanel>

                                                                    </dxtv:ContentControl>
                                                                </ContentCollection>
                                                            </dxtv:TabPage>
                                                            <dxtv:TabPage Text="Integrações" Name="tabIntegracoes" Visible="False">
                                                                <ContentCollection>
                                                                    <dxtv:ContentControl runat="server">
                                                                        <table class="headerGrid">
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxCheckBox ID="cbPermitirAcionamentoExterno" runat="server" CheckState="Unchecked" ClientInstanceName="cbPermitirAcionamentoExterno" Text="Permitir acionamento externo">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
callbackIntegracoes.PerformCallback('permitir-acionamento-exteno');
}" />
                                                                                                </dxtv:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxtv:ASPxButton ID="btnGerarEsquema" runat="server" AutoPostBack="False" ClientInstanceName="btnGerarEsquema" Text="Gerar esquema">
                                                                                                    <ClientSideEvents Click="function(s, e) {
	callbackIntegracoes.PerformCallback('gerar-esquema');
}" />
                                                                                                </dxtv:ASPxButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxCallback ID="callbackIntegracoes" runat="server" ClientInstanceName="callbackIntegracoes" OnCallback="callbackIntegracoes_Callback">
                                                                                        <ClientSideEvents BeginCallback="function(s, e){
    lpAguardeMasterPage.Show();
}" CallbackComplete="function(s, e) {
    if(s.cp_Erro){
        window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
    }
    else{
	    if(e.parameter === 'gerar-esquema'){
		    memoJsonSchema.SetText(e.result);
            btnCopyToClipboard.SetVisible(true);
            window.top.mostraMensagem('Esquema gerado com sucesso!', 'sucesso', false, false, null);
        }
        else {
            var permitir = cbPermitirAcionamentoExterno.GetChecked();
            btnGerarEsquema.SetEnabled(permitir);
            if(!permitir){
                memoJsonSchema.SetText('');
                btnCopyToClipboard.SetVisible(false);
            }
            lpAguardeMasterPage.Hide();
        }
    }
}" />
                                                                                    </dxtv:ASPxCallback>
                                                                                    <dxtv:ASPxMemo ID="memoJsonSchema" runat="server" ClientInstanceName="memoJsonSchema" ClientReadOnly="True" Rows="20" Width="100%">
                                                                                        <ClientSideEvents Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight) - 365;
            s.SetHeight(height);
}" />
                                                                                        <ReadOnlyStyle BackColor="#EBEBEB">
                                                                                        </ReadOnlyStyle>
                                                                                    </dxtv:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr><td>
                                                                                <table style="margin-left:auto; position: relative; top: -40px; right: 15px;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxButton ID="btnCopyToClipboard" ClientInstanceName="btnCopyToClipboard" ClientVisible="false" runat="server" AutoPostBack="false" Text="Copiar esquema">
                                                                                                <ClientSideEvents Click="function(s,e){
    navigator.clipboard.writeText(memoJsonSchema.GetText()).then(() => window.top.mostraMensagem('Esquema copiado!!', 'sucesso', false, false, null));
}" />
                                                                                            </dxtv:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td></tr>
                                                                        </table>
                                                                    </dxtv:ContentControl>
                                                                </ContentCollection>
                                                            </dxtv:TabPage>
                                                        </TabPages>
                                                    </dxtv:ASPxPageControl>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxtv:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
                            </dxtv:ASPxLoadingPanel>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
                            <dxpc:ASPxPopupControl ID="pcDadosAssociacao" runat="server" ClientInstanceName="pcDadosAssociacao"
                                HeaderText="Tipos de Projetos Relacionados ao Modelo de Fluxo"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="721px" CssClass="popup" ShowFooter="True" PopupVerticalOffset="15">
                                <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = false;
	pcDadosAssociacao_OnPopup(s,e);
}" />
                                <FooterTemplate>
                                                                                   
					<table style="width: 100%">							
												<tr>
                                                    <td align="right">
                                                        <table id="Table2" border="0" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxButton ID="btnSalvarAssociacao" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                            ClientInstanceName="btnSalvarAssociacao" Text="Salvar" UseSubmitBehavior="False"
                                                                            Width="90px" Theme="MaterialCompact">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvarAssociacao)
		onClick_btnSalvarAssociacao();
}" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btnFecharAssociacao" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharAssociacao"
                                                                            Text="Fechar" Width="90px" Theme="MaterialCompact">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelarAssociacao)
       onClick_btnCancelarAssociacao();
}" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
												</table>
											
                                </FooterTemplate>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel1011" runat="server"
                                                            Text="Modelo:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtNomeFluxoAssociado" runat="server" ClientInstanceName="txtNomeFluxoAssociado"
                                                            MaxLength="30" ReadOnly="True" Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel105" runat="server"
                                                                            Text="Tipos de Projetos Relacionados ao Modelo:" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxwgv:ASPxGridView ID="gvTiposProjetos" runat="server" AutoGenerateColumns="False"
                                                                            ClientInstanceName="gvTiposProjetos" KeyFieldName="CodigoTipoProjeto"
                                                                            OnCellEditorInitialize="gvTiposProjetos_CellEditorInitialize" OnCustomCallback="gvTiposProjetos_CustomCallback"
                                                                            OnRowDeleting="gvTiposProjetos_RowDeleting" OnRowInserting="gvTiposProjetos_RowInserting"
                                                                            OnRowUpdating="gvTiposProjetos_RowUpdating" Width="100%">
                                                                            <ClientSideEvents EndCallback="function(s, e) {	
	gvTipoProjetos_onEndCallback(s,e);
	
}"
                                                                                FocusedRowChanged="function(s, e) {	
	e.processOnServer = false;
	gvTiposProjetos_FocusedRowChanged(s,e);
}" />
                                                                            <Columns>
                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                    Width="80px" ShowDeleteButton="True" ShowEditButton="True">
                                                                                    <HeaderTemplate>
                                                                                        <img src="../imagens/botoes/incluirReg02.png" onclick="gvTiposProjetos.AddNewRow();" />
                                                                                    </HeaderTemplate>
                                                                                </dxwgv:GridViewCommandColumn>
                                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Tipo de projeto" FieldName="CodigoTipoProjeto"
                                                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                                                    <PropertiesComboBox ValueType="System.Int32" Width="120px">
                                                                                        <ItemStyle />
                                                                                        <DropDownButton ToolTip="Selecione o tipo de projeto a relacionar com o modelo de fluxo em questão.">
                                                                                        </DropDownButton>
                                                                                        <ValidationSettings SetFocusOnError="True">
                                                                                            <RequiredField ErrorText="Escolher o tipo de projeto a relacionar com o modelo de fluxo."
                                                                                                IsRequired="True"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                        <Style>
                                                                                            
                                                                                        </Style>
                                                                                    </PropertiesComboBox>
                                                                                    <EditFormSettings Caption="Associar a:" CaptionLocation="Top" Visible="True" VisibleIndex="0" />
                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                <dxwgv:GridViewDataTextColumn Caption="Tipo" FieldName="TipoProjeto" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1" Width="150px">
                                                                                    <PropertiesTextEdit Width="180px">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditFormSettings CaptionLocation="Top" Visible="False" VisibleIndex="0" />
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                <dxwgv:GridViewDataTextColumn Caption="Nome no Menu" FieldName="TextoOpcaoFluxo" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3" Width="200px">
                                                                                    <PropertiesTextEdit MaxLength="40" Width="150px">
                                                                                        <ValidationSettings SetFocusOnError="True">
                                                                                            <RequiredField ErrorText="Informar o nome a ser mostrado como opção de acesso ao modelo de fluxo para o tipo de projeto em questão."
                                                                                                IsRequired="True"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                        <Style>
                                                                                            
                                                                                        </Style>
                                                                                    </PropertiesTextEdit>
                                                                                    <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="1" Caption="Nome no Menu" />
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Ocorrência" FieldName="TipoOcorrencia"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                    <PropertiesComboBox Width="80px">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Text="1 vez" Value="U"></dxe:ListEditItem>
                                                                                            <dxe:ListEditItem Text="N Vezes" Value="N"></dxe:ListEditItem>
                                                                                        </Items>
                                                                                        <DropDownButton ToolTip="Selecione o tipo de ocorrência do modelo para o tipo de projeto em questão">
                                                                                        </DropDownButton>
                                                                                        <ValidationSettings>
                                                                                            <RequiredField ErrorText="Escolher o tipo de ocorrência do modelo para o tipo de projeto em questão."
                                                                                                IsRequired="True"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                        <Style>
                                                                                            
                                                                                        </Style>
                                                                                    </PropertiesComboBox>
                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" VisibleIndex="2" />
                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                <dxwgv:GridViewDataTextColumn Caption="RegistroNovo" FieldName="RegistroNovo" ShowInCustomizationForm="True"
                                                                                    Visible="False" VisibleIndex="5">
                                                                                    <EditFormSettings Visible="False" />
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False"
                                                                                AllowSort="False" ConfirmDelete="True" />
                                                                            <SettingsPager PageSize="4">
                                                                            </SettingsPager>
                                                                            <SettingsEditing EditFormColumnCount="4" Mode="PopupEditForm" />
                                                                            <SettingsPopup>
                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                    AllowResize="True" HorizontalOffset="30" Width="400px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                            </SettingsPopup>
                                                                            <SettingsText CommandCancel="Cancelar" CommandNew="Novo" CommandUpdate="Salvar" ConfirmDelete="Retirar a associação do modelo para o este tipo de projeto?"
                                                                                EmptyDataRow="Nenhum tipo de projeto está relacionado a este modelo de fluxo"
                                                                                PopupEditFormCaption="Tipo de Projeto associado ao modelo de fluxo" />
                                                                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="150"></Settings>
                                                                        </dxwgv:ASPxGridView>
                                                                        <dxhf:ASPxHiddenField ID="hfStatus" runat="server" ClientInstanceName="hfStatus"
                                                                            OnCustomCallback="hfStatus_CustomCallback">
                                                                            <ClientSideEvents EndCallback="function(s, e) {
	hfStatus_onEndCallback(s,e);
}" />
                                                                        </dxhf:ASPxHiddenField>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Seleção de Status para o Tipo:  " Width="180px">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 5px; height: 16px"></td>
                                                                    <td style="height: 16px">
                                                                        <dxe:ASPxLabel ID="lblSelecaoStatus" runat="server" ClientInstanceName="lblSelecaoStatus"
                                                                            Font-Bold="True" Font-Italic="True" Height="15px">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 330px">
                                                                        <dxe:ASPxLabel ID="ASPxLabel106" runat="server"
                                                                            Text="Status Disponíveis:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 60px"></td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel107" runat="server"
                                                                            Text="Status Selecionados:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxListBox ID="lbDisponiveisStatus" runat="server" ClientInstanceName="lbDisponiveisStatus"
                                                                            EnableClientSideAPI="True" EnableSynchronization="False" EncodeHtml="False"
                                                                            Height="130px" OnCallback="lbDisponiveisStatus_Callback" Rows="3"
                                                                            Width="100%">
                                                                            <FilteringSettings EditorNullText="Filtrar texto" />
                                                                            <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Disp_');
}"
                                                                                SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}" />
                                                                            <ValidationSettings>
                                                                                <ErrorImage Width="14px">
                                                                                </ErrorImage>
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxListBox>
                                                                    </td>
                                                                    <td align="center">
                                                                        <table>
                                                                            <tbody>                                                                                
                                                                                <tr>
                                                                                    <td style="height: 28px">
                                                                                        <dxe:ASPxButton ID="btnAddAll" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                            ClientInstanceName="btnAddAll" EncodeHtml="False" Text="&gt;&gt;"
                                                                                            ToolTip="Selecionar todos os usuarios" Width="55px" Font-Size="9pt" Height="15px">
                                                                                            <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbDisponiveisStatus,lbSelecionadosStatus);
	setListBoxItemsInMemory(lbDisponiveisStatus,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosStatus,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 28px">
                                                                                        <dxe:ASPxButton ID="btnAddSel" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                            ClientInstanceName="btnAddSel" EncodeHtml="False" Text="&gt;"
                                                                                            ToolTip="Selecionar os status marcados" Width="55px" Font-Size="9pt" Height="15px">
                                                                                            <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbDisponiveisStatus, lbSelecionadosStatus);
	setListBoxItemsInMemory(lbDisponiveisStatus,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosStatus,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 28px">
                                                                                        <dxe:ASPxButton ID="btnRemoveSel" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                            ClientInstanceName="btnRemoveSel" EncodeHtml="False" Text="&lt;"
                                                                                            ToolTip="Retirar da seleção os status marcados" Width="55px" Font-Size="9pt" Height="15px">
                                                                                            <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbSelecionadosStatus, lbDisponiveisStatus);
	setListBoxItemsInMemory(lbDisponiveisStatus,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosStatus,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 28px">
                                                                                        <dxe:ASPxButton ID="btnRemoveAll" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                            ClientInstanceName="btnRemoveAll" EncodeHtml="False" Text="&lt;&lt;"
                                                                                            ToolTip="Retirar da seleção todos os status" Width="55px" Font-Size="9pt" Height="15px">
                                                                                            <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbSelecionadosStatus, lbDisponiveisStatus);
	setListBoxItemsInMemory(lbDisponiveisStatus,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosStatus,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxListBox ID="lbSelecionadosStatus" runat="server" ClientInstanceName="lbSelecionadosStatus"
                                                                            EnableClientSideAPI="True" EnableSynchronization="False" EncodeHtml="False"
                                                                            Height="130px" OnCallback="lbSelecionadosStatus_Callback" Rows="4"
                                                                            Width="100%">
                                                                            <FilteringSettings EditorNullText="Filtrar texto" />
                                                                            <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Sel_');
	habilitaBotoesListBoxes();
}"
                                                                                SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}" />
                                                                            <ValidationSettings>
                                                                                <ErrorImage Width="14px">
                                                                                </ErrorImage>
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxListBox>
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
                            <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                CloseAction="None" ClientInstanceName="pcDadosDisparoFluxoProjetos" HeaderText="Detalhe"
                                ShowCloseButton="False" Width="702px" ID="pcDadosDisparoFluxoProjetos"
                                Height="122px" AllowDragging="True" Modal="True">
                                <ClientSideEvents Init="function(s, e) {
 
}"
                                    Shown="function(s, e) {
   	LimpaCamposDisparoFluxo();
}" />
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <table cellspacing="0" cellpadding="0" class="formulario" width="100%" border="0">
                                            <tbody>
                                                <%--             <tr>
                            <td style="height: 10px">
                                <dxe:ASPxLabel ID="lblProjeto" runat="server" ClientInstanceName="lblProjeto"
                                    Text="Projeto:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" 
                                    ClientInstanceName="txtNomeProjeto"  
                                    MaxLength="250" Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>--%>
                                                <%--                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width: 250px">
                                            <dxe:ASPxLabel ID="lblDataAtivacao" runat="server" 
                                                ClientInstanceName="lblDataAtivacao"
                                                Text="Data Ativação:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="lblUsuarioAtivacao" runat="server" 
                                                ClientInstanceName="lblUsuarioAtivacao"
                                                Text="Usuário Ativação:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtDataAtivacao" runat="server" ClientInstanceName="txtDataAtivacao"
                                                 MaxLength="250" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td style="width: 250px; padding-right: 5px">
                                            <dxe:ASPxTextBox ID="txtNomeUsuarioAtivacao" runat="server" ClientInstanceName="txtNomeUsuarioAtivacao"
                                                 MaxLength="250" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width: 250px">
                                            <dxe:ASPxLabel ID="lblDataDesativacao" runat="server" 
                                                ClientInstanceName="lblDataDesativacao"
                                                Text="Data Desativação:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="lblUsuarioDesativacao" runat="server" 
                                                ClientInstanceName="lblUsuarioDesativacao"
                                                Text="Usuário Desativação:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtDataDesativacao" runat="server" ClientInstanceName="txtDataDesativacao"
                                                 MaxLength="250" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td style="width: 250px; padding-right: 5px">
                                            <dxe:ASPxTextBox ID="txtUsuarioDesativacao" runat="server" ClientInstanceName="txtUsuarioDesativacao"
                                                 MaxLength="50" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="ckbIndicaControlado" runat="server" CheckState="Unchecked"
                                                            ClientInstanceName="ckbIndicaControlado"
                                                            Text="O fluxo será disparado automaticamente?" ValueChecked="S" ValueType="System.String"
                                                            ValueUnchecked="N">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	LimpaCamposDisparoFluxo();
}" />
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table class="formulario-colunas" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="lblPrimeiraExecucao" runat="server" ClientInstanceName="lblPrimeiraExecucao"
                                                                        Text="Data Primeira Execução:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 250px">
                                                                    <dxe:ASPxLabel ID="lblPeriodicidade" runat="server" ClientInstanceName="lblPeriodicidade"
                                                                        Text="Periodicidade:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="display: none;">
                                                                    <dxe:ASPxLabel ID="lblProximaExecucao" runat="server" ClientInstanceName="lblProximaExecucao"
                                                                        Text="Data Próxima Execução:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; padding-right: 5px">
                                                                    <dxe:ASPxDateEdit ID="dteInicio" runat="server" ClientInstanceName="dteInicio" EditFormat="Custom"
                                                                        Width="100%" DisplayFormatString="dd/MM/yyyy"
                                                                        EditFormatString="dd/MM/yyyy">
                                                                        <ClientSideEvents DateChanged="function(s, e) {
	//onClickAlteraDisparo(s,e);
}"
                                                                            Init="function(s, e) {
	LimpaCamposDisparoFluxo();
}" />
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmbPeriodicidade" runat="server" ClientInstanceName="cmbPeriodicidade"
                                                                        EnableCallbackMode="True" Width="100%" TextField="DescricaoPeriodicidade"
                                                                        TextFormatString="{0}" ValueField="CodigoPeriodicidade" ValueType="System.Int32">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
   	//onClickAlteraDisparo(s,e);
    hfGeral1.Set(&quot;codigoPeriodicidade&quot;, s.GetValue().toString());
}" />
                                                                        <Columns>
                                                                            <dxe:ListBoxColumn Caption="Periodicidade" FieldName="DescricaoPeriodicidade_PT" />
                                                                            <dxe:ListBoxColumn Caption="Intervalo Dias" FieldName="IntervaloDias" Visible="False" />
                                                                        </Columns>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td style="padding-right: 5px; display: none;">
                                                                    <dxe:ASPxTextBox ID="txtProximaExecucao" runat="server" ClientInstanceName="txtProximaExecucao"
                                                                        MaxLength="250" Width="100%" ClientEnabled="False"
                                                                        DisplayFormatString="dd/MM/yyyy">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
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
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarDisparo"
                                                                            Text="Salvar" Width="100px" ID="btnSalvarDisparo">
                                                                            <ClientSideEvents Click="function(s, e) {
//debugger	
if (window.onClick_btnSalvarDisparo)
	      onClick_btnSalvarDisparo();
}"></ClientSideEvents>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharDisparo"
                                                                            Text="Fechar" Width="100px" ID="btnFecharDisparo">
                                                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer = false;
    if (window.onClick_btnFecharDisparo)
       onClick_btnFecharDisparo();
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
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfTela" ID="hfTela">
                            </dxhf:ASPxHiddenField>
                            <dxtv:ASPxPopupControl ID="pcCopiaFluxo" runat="server" AllowDragging="True" AllowResize="True"
                                ClientInstanceName="pcCopiaFluxo" HeaderText="Copia Fluxo"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="721px" CssClass="popup">
                                <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = true;
	pcCopiaFluxo_OnPopup(s,e);
}"
                                    Shown="function(s, e) {
 }" />
                                <ContentCollection>
                                    <dxtv:PopupControlContentControl runat="server">
                                        <table class="formulario" cellpadding="0" cellspacing="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="lblCopia" runat="server" ClientInstanceName="lblCopia"
                                                            Text="A cópia de um modelo de fluxo só poderá ser efetuada para um modelo de fluxo com versão criada e não publicada."
                                                            Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <dxtv:ASPxRoundPanel ID="rpWorkflowDestino" runat="server"
                                                            HeaderText="Selecione o fluxo de destino, para o qual se deseja efetuar a cópia:"
                                                            View="GroupBox" Width="100%" ClientInstanceName="rpWorkflowDestino">
                                                            <PanelCollection>
                                                                <dxtv:PanelContent runat="server">
                                                                    <dxtv:ASPxCallbackPanel ID="pn_ddlFluxoDestino" ClientInstanceName="pn_ddlFluxoDestino" runat="server" Width="100%" OnCallback="pn_ddlFluxoDestino_Callback">
                                                                        <ClientSideEvents  EndCallback="function(s, e) {
                                                                            lpLoading.Hide();
                                                                            
                                                                            }" />
                                                                        <PanelCollection>
                                                                            <dxtv:PanelContent runat="server">                                                                                
                                                                                <dxtv:ASPxComboBox ID="ddlFluxoDestino" runat="server" ClientInstanceName="ddlFluxoDestino"
                                                                                    ValueType="System.Int32"
                                                                                    Width="100%" Native="True">
                                                                                    <Columns>
                                                                                        <dxtv:ListBoxColumn Caption="Código" FieldName="codigo" Width="100px" Visible="False" />
                                                                                        <dxtv:ListBoxColumn Caption="Fluxo - Versão" FieldName="descricao" Width="300px" />
                                                                                    </Columns>
                                                                                </dxtv:ASPxComboBox>
                                                                            </dxtv:PanelContent>
                                                                        </PanelCollection>
                                                                    </dxtv:ASPxCallbackPanel>
                                                                </dxtv:PanelContent>
                                                            </PanelCollection>
                                                        </dxtv:ASPxRoundPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table class="formulario-botoes" id="Table3" border="0" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxtv:ASPxButton ID="btnSalvarCopiaFluxo" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                            ClientInstanceName="btnSalvarCopiaFluxo" Text="Salvar" UseSubmitBehavior="False"
                                                                            Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvarCopiaFluxo)
		onClick_btnSalvarCopiaFluxo();
}" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxtv:ASPxButton ID="btnFecharCopiaFluxo" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharCopiaFluxo"
                                                                            Text="Fechar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelarCopiaFluxo)
       onClick_btnCancelarCopiaFluxo();
}" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxtv:ASPxHiddenField ID="hfStatusCopiaFluxo" runat="server" ClientInstanceName="hfStatusCopiaFluxo"
                                            OnCustomCallback="hfStatusCopiaFluxo_CustomCallback">
                                            <ClientSideEvents EndCallback="function(s, e) {
	hfStatusCopiaFluxo_onEndCallback(s,e);
}" />
                                        </dxtv:ASPxHiddenField>
                                    </dxtv:PopupControlContentControl>
                                </ContentCollection>
                            </dxtv:ASPxPopupControl>
                            <asp:SqlDataSource ID="sdsProjetosFluxo" runat="server" DeleteCommand="UPDATE [dbo].[FluxosProjetoDisparoAutomatico]
                                                                       SET [DataDesativacao] = GETDATE()
                                                                          ,[IdentificadorUsuarioDesativacao] = @IdentificadorUsuarioDesativacao
                                                                     WHERE [CodigoFluxo] = @CodigoFluxo and [CodigoProjeto] = @CodigoProjeto"
                                InsertCommand="INSERT INTO [dbo].[FluxosProjetoDisparoAutomatico]
                                               ([CodigoFluxo]
                                               ,[CodigoProjeto]
                                               ,[DataAtivacao]
                                               ,[IdentificadorUsuarioAtivacao]
                                               ,[DataDesativacao]
                                               ,[IdentificadorUsuarioDesativacao]
                                               ,[DataPrimeiraExecucao]
                                               ,[CodigoPeriodicidade]
                                               ,[DataProximaExecucao]
                                               ,[IndicaControlado])
                                         VALUES
                                               (@CodigoFluxo
                                               ,@CodigoProjeto
                                               ,GETDATE()
                                               ,@IdentificadorUsuarioAtivacao
                                               ,null
                                               ,null
                                               ,@DataPrimeiraExecucao
                                               ,@Periodicidade
                                               ,@DataProximaExecucao
                                               ,@IndicaControlado)"
                                SelectCommand="SELECT fp.[CodigoFluxo]
                                              ,fp.[CodigoProjeto]
                                              ,p.NomeProjeto
                                              ,da.[DataAtivacao]
                                              ,da.[IdentificadorUsuarioAtivacao]
                                              ,da.[DataDesativacao]
                                              ,da.[IdentificadorUsuarioDesativacao]
                                              ,da.[DataPrimeiraExecucao]
                                              ,da.[CodigoPeriodicidade]
                                              ,da.[DataProximaExecucao]
                                              ,ISNULL(indicaControlado ,'Não') indicaControlado 
                                              ,at.NomeUsuario as UsuarioAtivacao
                                              ,de.NomeUsuario as UsuarioDesativacao
                                              ,tp.DescricaoPeriodicidade_PT
                                              ,CASE WHEN (da.[IdentificadorUsuarioAtivacao] IS NULL ) THEN 'N' ELSE 'S' END AS existeRegistro 
                                          FROM [dbo].[FluxosProjeto] fp INNER JOIN 
                                               [dbo].[projeto] p on p.CodigoProjeto = fp.codigoProjeto left join
                                               [dbo].[FluxosProjetoDisparoAutomatico] da on da.CodigoFluxo = fp.CodigoFluxo and da.CodigoProjeto = fp.CodigoProjeto left join
                                               [dbo].[Usuario] at on at.CodigoUsuario = da.IdentificadorUsuarioAtivacao left join 
                                               [dbo].[Usuario] de on de.CodigoUsuario = da.IdentificadorUsuarioDesativacao left join
                                               [dbo].[TipoPeriodicidade] tp on tp.CodigoPeriodicidade = da.codigoPeriodicidade
                                       where (fp.[CodigoFluxo] = @CodigoFluxo) AND (@CodigoFluxo &lt;&gt; -1)">
                                <DeleteParameters>
                                    <asp:Parameter Name="CodigoFluxo" Type="Int32" />
                                    <asp:Parameter Name="CodigoProjeto" Type="Int32" />
                                    <asp:Parameter Name="IdentificadorUsuarioDesativacao" Type="Int32" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="CodigoFluxo" Type="Int32" />
                                    <asp:Parameter Name="CodigoProjeto" Type="Int32" />
                                    <asp:Parameter Name="IdentificadorUsuarioAtivacao" Type="Int32" />
                                    <asp:Parameter Name="DataPrimeiraExecucao" Type="DateTime" />
                                    <asp:Parameter Name="Periodicidade" Type="Int32" />
                                    <asp:Parameter Name="DataProximaExecucao" Type="DateTime" />
                                    <asp:Parameter Name="IndicaControlado" />
                                </InsertParameters>
                                <SelectParameters>
                                    <asp:SessionParameter Name="CodigoFluxo" SessionField="ssCodigoFluxoSelecionado"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="" align="center"></td>
                                                    <td style="width: 70px" align="center" rowspan="3">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                            ClientInstanceName="imgSalvar" ID="imgSalvar">
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
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();

	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.adm_CadastroWorkflows__modelo_de_fluxo_inclu_do_com_sucesso__);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.adm_CadastroWorkflows__dados_gravados_com_sucesso__);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.adm_CadastroWorkflows__modelo_de_fluxo_exclu_do_com_sucesso__);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">

        function redirecionaPagina(codigoFluxo, codigoWorkflow) {
            window.top.gotoURL('administracao/adm_edicaoWorkflows.aspx?_NF=AreaTrabalho_&CWF=' + codigoWorkflow + '&CF=' + codigoFluxo, '_parent');
        }

        function trataClickBotaoEditarWorkflow(s) {
            s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoFluxo;CodigoWorkflow;DataPublicacao', auxTrataClickBotaoEditarWorkflow)

        }

        function auxTrataClickBotaoEditarWorkflow(valores) {
            if (null != valores) {
                var codigoFluxo = valores[0];
                var codigoWorkflow = valores[1];
                var dataPublicacao = valores[2];
                var continuar = true;

                if ((null != dataPublicacao) && ('' != dataPublicacao))
                    continuar = confirm(traducao.adm_CadastroWorkflows_esta_vers_o_j__est__publicada__sua_edi__o_ir__gerar_uma_nova_vers_o__continuar_);

                if (true == continuar)
                    redirecionaPagina(codigoFluxo, codigoWorkflow);
            }
        }

        function trataClickBotaoNovoWorkflow(s) {
            // cpDetailRowIndex é criada no evento DetailRowExpanding
            if (s.cpDetailRowIndex != null)
                s.GetRowValues(s.cpDetailRowIndex, 'CodigoFluxo', auxTrataClickBotaoNovoWorkflow)
        }

        function auxTrataClickBotaoNovoWorkflow(codigoFluxo) {
            if (null != codigoFluxo)
                redirecionaPagina(codigoFluxo, '0');
        }


        function trataClickBotaoCopiarWorkflow(s) {
            s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoFluxo;CodigoWorkflow;DataPublicacao', auxTrataClickBotaoCopiarWorkflow)

        }

        function auxTrataClickBotaoCopiarWorkflow(valores) {
            if (null != valores) {
                var codigoFluxo = valores[0];
                var codigoWorkflow = valores[1];

                hfStatusCopiaFluxo.Set("CodigoWorkflow", codigoWorkflow);
                hfStatusCopiaFluxo.Set("CodigoFluxo", codigoFluxo);                
                pn_ddlFluxoDestino.PerformCallback((codigoWorkflow == null ? -1 : codigoWorkflow) + '|' + codigoFluxo);
                pcCopiaFluxo.Show();
            }
        }


    </script>
</asp:Content>
