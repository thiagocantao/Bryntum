<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="unidadesNegocio.aspx.cs" Inherits="administracao_unidadesNegocio"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <div id="divGrid" style="visibility:hidden;padding-top:5px">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoUnidadeNegocio"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnCellEditorInitialize="gvDados_CellEditorInitialize">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	                    OnGridFocusedRowChanged(s);
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
     else if(e.buttonID == 'btnEditar')
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
	else if(e.buttonID == 'btnCompartilharCustom')
	{
		OnGridFocusedRowChangedPopup(gvDados);
	}
	else if(e.buttonID == 'btnGoogleMaps')
	{
		OnGridFocusedRowChangedGoogleMaps(gvDados);
	}
}
"  Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>

                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                </SettingsCommandButton>
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
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnCompartilharCustom" Text="Administrar Permissões da Unidade">
                                                <Image ToolTip="Administrar Permissões da Unidade" Url="~/imagens/Perfis/Perfil_Permissoes.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnGoogleMaps" Text="Visualizar Mapa"
                                                Visibility="Invisible">
                                                <Image Url="~/imagens/botoes/mapa.png" ToolTip="Visualizar Mapa">
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
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUnidadeNegocio" Name="col_NomeUnidade"
                                        Caption="Nome Unidade" VisibleIndex="1">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeNegocio" Name="col_SiglaUnidade"
                                        Width="100px" Caption="Sigla" VisibleIndex="2">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUnidadeNegocioSuperior" Name="col_UnidadeSuperior"
                                        Caption="Unidade Superior" VisibleIndex="9">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Observacoes" Visible="False" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocioSuperior" Name="CodigoUnidadeNegocioSuperior"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioGerente" Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NivelHierarquicoUnidade" Visible="False"
                                        VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoReservado" Visible="False" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio" Name="CodigoUnidadeNegocio"
                                        Width="40px" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaUnidadeNegocioAtiva" Name="IndicaUnidadeNegocioAtiva"
                                        Width="50px" Caption="Ativo" Visible="False" VisibleIndex="10">
                                        <DataItemTemplate>
                                            <%# (Eval("IndicaUnidadeNegocioAtiva").ToString() == "S") ? "Sim" : "Não"%>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Gerente" FieldName="NomeUsuarioGerente" ShowInCustomizationForm="True"
                                        VisibleIndex="11">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Latitude" FieldName="Latitude" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="12">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Longitude" FieldName="Longitude" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="13">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                <SettingsPager PageSize="100">
                                </SettingsPager>
                                <Settings ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible"
                                    ShowFilterRow="True"></Settings>
                                <Templates>
                                    <FooterRow>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="grid-legendas-cor grid-legendas-cor-inativo">
                                                        <span></span>
                                                    </td>
                                                    <td class="grid-legendas-label grid-legendas-label-inativo">
                                                        <span>
                                                            <%# definicionLenda %>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                            </div>
                                <!-- PCDADOS -->
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                HeaderText="Detalhe" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="850px" ID="pcDados">
                                <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"
                                    CloseUp="function(s, e) {
	LimpaCamposFormulario();
}"></ClientSideEvents>
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfNovoNomeUnidade" ID="hfNovoNomeUnidade"
                                                            OnCustomCallback="hfNovoNomeUnidade_CustomCallback">
                                                            <ClientSideEvents EndCallback="function(s, e) {
trataResultadoVerificacaoNomeEntidade(s, e);
}"></ClientSideEvents>
                                                        </dxhf:ASPxHiddenField>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td id="Td2" style="width: 165px; height: 62px" valign="top">
                                                                        <dxcp:ASPxCallbackPanel ID="pnLogo" runat="server" ClientInstanceName="pnLogo" OnCallback="pnLogo_Callback">
                                                                            <PanelCollection>
                                                                                <dxp:PanelContent runat="server">
                                                                                    <dxe:ASPxBinaryImage runat="server" Width="155px" Height="75px" ClientInstanceName="imageLogo"
                                                                                        ID="imageLogo">
                                                                                        <EmptyImage Height="55px" Width="155px" Url="../imagens/sin_logo.gif">
                                                                                        </EmptyImage>
                                                                                    </dxe:ASPxBinaryImage>
                                                                                </dxp:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxcp:ASPxCallbackPanel>
                                                                    </td>
                                                                    <td style="width: 5px"></td>
                                                                    <td valign="top">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="lblNome" runat="server" ClientInstanceName="lblNome"
                                                                                            Text="Nome:*">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtNome"
                                                                                            ID="txtNome">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 161px; height: 5px"></td>
                                                                                    <td style="width: 10px; height: 5px"></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 161px">
                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, unidadesNegocio_sigla__ %>" ClientInstanceName="lblSigla"
                                                                                            ID="lblSigla">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td style="width: 200px">&nbsp;<dxe:ASPxLabel runat="server" Text="C&#243;digo:" ClientInstanceName="lblCodigo"
                                                                                        ID="lblCodigo">
                                                                                    </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 161px">
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="10" ClientInstanceName="txtSigla"
                                                                                            ID="txtSigla">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="20" ClientInstanceName="txtCodigo"
                                                                                            ID="txtCodigo">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <dxe:ASPxCheckBox runat="server" Text="Unidade Ativa?" ClientInstanceName="checkUnidade"
                                                                                            ID="checkUnidade">
                                                                                        </dxe:ASPxCheckBox>
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

                                                <tr>
                                                    <td style="height: 10px">&nbsp;
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Gerente:" ClientInstanceName="lblGerente"
                                                            ID="lblGerente">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="ddlGerente" runat="server" ClientInstanceName="ddlGerente"
                                                            DropDownStyle="DropDown" EnableCallbackMode="True"
                                                            IncrementalFilteringMode="Contains" OnItemRequestedByValue="ddlGerenteProjeto_ItemRequestedByValue"
                                                            OnItemsRequestedByFilterCondition="ddlGerenteProjeto_ItemsRequestedByFilterCondition"
                                                            TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario" Width="100%">
                                                            <Columns>
                                                                <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                                                                <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="300px" />
                                                            </Columns>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackUnidadeSuperior"
                                                            Width="100%" ID="pnCallbackUnidadeSuperior" OnCallback="pnCallbackUnidadeSuperior_Callback">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, unidadesNegocio_unidade_superior__ %>" ClientInstanceName="lblUnidadeSuperior"
                                                                                        ID="lblUnidadeSuperior">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="StartsWith" ValueType="System.Int32"
                                                                                        TextFormatString="{1}" Width="100%" ClientInstanceName="ddlUnidadeSuperior"
                                                                                        ID="ddlUnidadeSuperior">
                                                                                        <Columns>
                                                                                            <dxe:ListBoxColumn FieldName="SiglaUnidadeNegocio" Width="20%" Caption="Sigla"></dxe:ListBoxColumn>
                                                                                            <dxe:ListBoxColumn FieldName="NomeUnidadeNegocio" Width="80%" Caption="Unidade">
                                                                                            </dxe:ListBoxColumn>
                                                                                        </Columns>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxcp:ASPxCallbackPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Observa&#231;&#245;es:" ClientInstanceName="lblObservacoes"
                                                            ID="lblObservacoes">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo runat="server" Rows="5" Width="100%" ClientInstanceName="txtObservacoes"
                                                            ID="txtObservacoes">
                                                            <Border BorderStyle="Solid" />
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                        <dxe:ASPxLabel ID="lblContadorMemo" runat="server" ClientInstanceName="lblContadorMemo"
                                                            Font-Bold="True" ForeColor="#999999">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>
                                                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="lblLogoEntidad" runat="server" ClientInstanceName="lblLogoEntidad"
                                                                            Text="Escolher Logomarca da Unidade:">
                                                                        </dxe:ASPxLabel>
                                                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                            ForeColor="Green" Text=" (157px X 40px)">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 32px">
                                                                        <dxuc:ASPxUploadControl runat="server" ClientInstanceName="LogoUpload" FileUploadMode="OnPageLoad"
                                                                            CancelButtonHorizontalPosition="Left" Width="100%" BackColor="White"
                                                                            ForeColor="Black" ID="LogoUpload" OnFileUploadComplete="LogoUpload_FileUploadComplete">
                                                                            <ValidationSettings AllowedFileExtensions=".jpeg, .pjpeg, .gif, .png, .bmp, .jpg, .tif"
                                                                                NotAllowedFileExtensionErrorText="Esse tipo de conte&#250;do n&#227;o &#233; permitido"
                                                                                MaxFileSize="4000000"
                                                                                MaxFileSizeErrorText="Tamanho do ficheiro excede o tamanho m&#225;ximo permitido"
                                                                                GeneralErrorText="Upload do arquivo falhar devido a um erro externo que n&#227;o se relaciona com a funcionalidade de Componente">
                                                                            </ValidationSettings>
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                                
hfGeral.Set(&quot;NomeArquivo&quot;, e.callbackData);
                                                                                if(e.callbackData != '')
pnLogo.PerformCallback('SE');
}"
                                                                                TextChanged="function(s, e) {
LogoUpload.UploadFile();
}"></ClientSideEvents>
                                                                            <UploadButton ImagePosition="Right" Text="Carregar  Logo ">
                                                                            </UploadButton>
                                                                            <AdvancedModeSettings>
                                                                                <FileListItemStyle CssClass="pending dxucFileListItem">
                                                                                </FileListItemStyle>
                                                                            </AdvancedModeSettings>
                                                                            <DisabledStyle ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxuc:ASPxUploadControl>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1"
                                                                            Text="Salvar" Width="100px" ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
  onClick_btnSalvarUnidade(s, e);
}"></ClientSideEvents>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="100px" ID="btnFechar">
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
                            <!-- FIM DE PCDADOS -->
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
                                                        <dxe:ASPxLabel runat="server" EncodeHtml="False" ClientInstanceName="lblAcaoGravacao"
                                                            ID="lblAcaoGravacao">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" CloseAction="None"
                                EnableClientSideAPI="True" HeaderText="Mensagem" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowShadow="False"
                                Width="721px" ID="pcMensagemGravacao" CssClass="popup">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" class="formulario" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Aten&#231;&#227;o:" ClientInstanceName="lblAtencao"
                                                            Font-Bold="True" ID="lblAtencao">
                                                        </dxe:ASPxLabel>
                                                        &nbsp;<dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblMensagemError"
                                                            ID="lblMensagemError">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxp:ASPxPanel runat="server" EnableClientSideAPI="True" ClientInstanceName="pnlImpedimentos"
                                                            Width="100%" ID="pnlImpedimentos">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvImpedimentos" AutoGenerateColumns="False"
                                                                                        Width="100%" ID="gvImpedimentos" OnHtmlRowPrepared="gvImpedimentos_HtmlRowPrepared">
                                                                                        <ClientSideEvents EndCallback="function(s, e) {
	
	onEnd_CallbackGvImpedimentos(s, e);
}"></ClientSideEvents>

                                                                                        <SettingsCommandButton>
                                                                                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                                                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                                                        </SettingsCommandButton>
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUnidade" Name="NomeUnidade" Width="40%"
                                                                                                Caption="Item" VisibleIndex="0">
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="codImpedimento" Name="codImpedimento" Caption="Impedimento"
                                                                                                Visible="False" VisibleIndex="1">
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <Settings VerticalScrollableHeight="150" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>
                                                                                        <Templates>
                                                                                            <FooterRow>
                                                                                                <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td class="grid-legendas-cor grid-legendas-cor-recursos-corporativos-relacionados">
                                                                                                                <span></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label grid-legendas-label-recursos-corporativos-relacionados">
                                                                                                                <span>
                                                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, unidadesNegocio_recursos_corporativos_relacionados %>" /></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-cor grid-legendas-cor-unidades-relacionadas">
                                                                                                                <span></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label grid-legendas-label-unidades-relacionadas">
                                                                                                                <span>
                                                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, unidadesNegocio_unidades_relacionadas %>" /></span>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="grid-legendas-cor grid-legendas-cor-mapas-estrategicos-relacionados">
                                                                                                                <span></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label grid-legendas-label-mapas-estrategicos-relacionados">
                                                                                                                <span>
                                                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, unidadesNegocio_mapas_estrat_gicos_relacionados %>" /></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-cor grid-legendas-cor-projetos-programas-relacionados">
                                                                                                                <span></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label grid-legendas-label-projetos-programas-relacionados">
                                                                                                                <span>
                                                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, unidadesNegocio_projetos_ou_programas_relacionados %>" /></span>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="grid-legendas-cor grid-legendas-cor-demandas-relacionadas">
                                                                                                                <span></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label grid-legendas-label-demandas-relacionadas">
                                                                                                                <span>
                                                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, unidadesNegocio_demandas_relacionadas %>" /></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-cor grid-legendas-cor-contratos-relacionados">
                                                                                                                <span></span>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label grid-legendas-label-contratos-relacionados">
                                                                                                                <span>
                                                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, unidadesNegocio_contratos_relacionados %>" /></span>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </FooterRow>
                                                                                        </Templates>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none;">
                                                                                <td>
                                                                                    <!-- TABLA REFERENÇÃS CORES -->
                                                                                    <table id="tbColores" width="100%">
                                                                                        <tbody>
                                                                                            <tr id="corRC">
                                                                                                <td style="width: 30px; background-color: #6a5acd"></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Recursos Corporativos relacionados." ClientInstanceName="lblDescricaoCorReCor"
                                                                                                        ID="lblDescricaoCorReCor">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="corUN">
                                                                                                <td style="width: 30px; background-color: #98fb98"></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Unidades relacionadas." ClientInstanceName="lblDescricaoCorUnidade"
                                                                                                        ID="lblDescricaoCorUnidade">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="corME">
                                                                                                <td style="width: 30px; background-color: #b0c4de"></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Mapas Estrategicos relacionados." ClientInstanceName="lblDescricaoCorMaEst"
                                                                                                        ID="lblDescricaoCorMaEst">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="corPP">
                                                                                                <td style="width: 30px; background-color: #dcdcdc"></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Projetos ou Programas relacionados." ClientInstanceName="lblDescricaoCorProjeto"
                                                                                                        ID="lblDescricaoCorProjeto">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="corDE">
                                                                                                <td style="width: 30px; background-color: #d8bfd8"></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Demandas relacionadas." ClientInstanceName="lblDescricaoCorDemanda"
                                                                                                        ID="lblDescricaoCorDemanda">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="corCO">
                                                                                                <td style="width: 30px; background-color: #eee8aa"></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Contratos relacionados." ClientInstanceName="lblDescricaoCorContrato"
                                                                                                        ID="lblDescricaoCorContrato">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="formulario-botao">
                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False"
                                                                                                        Text="Fechar" Width="100px" ID="ASPxButton1">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcMensagemGravacao.Hide();
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
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemVarios" HeaderText="Mensagem do Sistema!"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="440px" ID="pcMensagemVarios">
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblMensagemVarios"
                                                            ID="lblMensagemVarios">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblMensagemVarios2"
                                                            ID="lblMensagemVarios2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAceitarMensagem"
                                                            Text="Ok" Width="90px" ID="btnAceitarMensagem">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcMensagemVarios.Hide();
}"></ClientSideEvents>
                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxe:ASPxButton>
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
    onEnd_pnCallbackLocal(s, e);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="pcLatitudeLongitude" runat="server" AllowDragging="True"
        ClientInstanceName="pcLatitudeLongitude" CloseAction="CloseButton" HeaderText="Ajuda - Coordenadas do Google"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowSizeGrip="True">
        <ClientSideEvents Shown="function(s, e) {
setTimeout(function(){}, 1500);	
initialize();
}" />
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblUnidadeSuperior0" runat="server" ClientInstanceName="lblUnidadeSuperior"
                                                        Text="Latitude:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblUnidadeSuperior1" runat="server" ClientInstanceName="lblUnidadeSuperior"
                                                        Text="Longitude:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtLatitude" runat="server" ClientInstanceName="txtLatitude"
                                                        MaxLength="100" Text="0" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtLongitude" runat="server" ClientInstanceName="txtLongitude"
                                                        MaxLength="100" Text="0" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 3px; padding-bottom: 3px">
                                        <dxe:ASPxTextBox ID="txtEnderecoMaisProximo" runat="server" ClientInstanceName="txtEnderecoMaisProximo"
                                            Width="100%">
                                            <ClientSideEvents TextChanged="function(s, e) {
	btnConfirmaCoordenadas.SetEnabled(true);
}" />
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="divGoogleMaps">
                </div>
                <table align="right" class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td class="formulario-botao">
                                <dxe:ASPxButton ID="btnConfirmaCoordenadas" runat="server" AutoPostBack="False" ClientInstanceName="btnConfirmaCoordenadas"
                                    Text="Salvar" Width="100px">
                                    <ClientSideEvents Click="function(s, e) 
{
var codigoUnidade = hfGeral.Get(&quot;CodigoUnidade&quot;);
var strCallback = codigoUnidade + ';' + txtLatitude.GetText() + ';' + txtLongitude.GetText();
callback1.PerformCallback(strCallback);
}" />

                                </dxe:ASPxButton>
                            </td>
                            <td class="formulario-botao">
                                <dxe:ASPxButton ID="btnFecharConfirmaCoordenadas" runat="server" AutoPostBack="False"
                                    ClientInstanceName="btnFecharConfirmaCoordenadas"
                                    Text="Fechar" Width="100px">
                                    <ClientSideEvents Click="function(s, e) 
{
pcLatitudeLongitude.Hide();
}" />

                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfCodigo" ID="hfCodigo">
    </dxhf:ASPxHiddenField>
    <dxcb:ASPxCallback ID="callback1" runat="server" ClientInstanceName="callback1" OnCallback="callback1_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Sucesso == 'S')
	{
		mostraDivSalvoPublicado(&quot;Coordenadas salvas com sucesso&quot;);
		
		initialize();
	}
	else
	{
		var erro = s.cp_Erro;		
		
		mostraDivSalvoPublicado(&quot;Erro ao salvar: &quot; + erro);
	}
}" />
    </dxcb:ASPxCallback>
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfNivelHierarquicoUnidade"
        ID="hfNivelHierarquicoUnidade">
    </dxhf:ASPxHiddenField>
    <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
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
</asp:Content>
