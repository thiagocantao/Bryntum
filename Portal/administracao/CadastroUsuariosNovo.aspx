<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="CadastroUsuariosNovo.aspx.cs" Inherits="administracao_CadastroUsuariosNovo"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <style type="text/css">
        .wrapDataText {
            word-break: break-all;
        }

        .auto-style2 {
            height: 14px;
        }
    </style>
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <div id="divGrid" style="visibility: hidden; padding-top: 5px">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoUsuario"
                                    AutoGenerateColumns="False" DataSourceID="dsDados" Width="100%"
                                    ID="gvDados" EnableViewState="False" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                    OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                    OnCustomErrorText="gvDados_CustomErrorText" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
OnClick_CustomButtons(s, e);
}"
                                        FocusedRowChanged="function(s, e) {
                                        
	OnGridFocusedRowChanged(gvDados);
}"
                                        Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ToolTip="Editar os perfis do usu&#225;rio"
                                            VisibleIndex="0" Width="180px" ShowInCustomizationForm="False">
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
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnCopiaPermissoes" Text="Copiar Permissões">
                                                    <Image AlternateText="Copiar Permissões" Url="~/imagens/botoes/btnDuplicar.png">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarPerfis" Text="Visualizar Perfis do Usu&#225;rio">
                                                    <Image AlternateText="Perfis do usu&#225;rio" Url="~/imagens/perfis/PersonalizarPerfilUsuario.png">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxtv:GridViewCommandColumnCustomButton ID="btnConfig" Text="<%$ Resources:traducao,editar_prefer_ncias_do_usu_rio %>">
                                                    <Image Url="~/imagens/botoes/btnConfig1.png">
                                                    </Image>
                                                </dxtv:GridViewCommandColumnCustomButton>
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
                                        <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="NomeUsuario" VisibleIndex="1"
                                            ShowInCustomizationForm="False">
                                            <Settings AutoFilterCondition="Contains" />
                                            <FilterCellStyle>
                                            </FilterCellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Conta para Autenticação" FieldName="ContaWindows"
                                            VisibleIndex="2" Width="180px" ShowInCustomizationForm="False">
                                            <CellStyle CssClass="wrapDataText" Wrap="True"></CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="E-mail" FieldName="EMail"
                                            VisibleIndex="3" ShowInCustomizationForm="False" Width="250px">
                                            <CellStyle CssClass="wrapDataText" Wrap="True"></CellStyle>
                                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="False" ShowInFilterControl="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Telefone" FieldName="TelefoneContato1" VisibleIndex="4"
                                            Width="130px" ShowInCustomizationForm="False">
                                            <Settings AllowAutoFilter="False" ShowFilterRowMenu="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Celular" FieldName="TelefoneContato2" Visible="False"
                                            VisibleIndex="5" Width="90px" ShowInCustomizationForm="False">
                                            <Settings AllowAutoFilter="False" ShowFilterRowMenu="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Ativo" FieldName="IndicaUsuarioAtivoUnidadeNegocio"
                                            Name="IndicaUsuarioAtivoUnidadeNegocio" Visible="False" VisibleIndex="6" Width="50px">
                                            <Settings AllowAutoFilter="False" ShowFilterRowMenu="False" />
                                            <DataItemTemplate>
                                                <%# (Eval("IndicaUsuarioAtivoUnidadeNegocio").ToString() == "S") ? "Sim" : "Não" %>
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Codigo Usuario" FieldName="CodigoUsuario"
                                            Name="col_CodigoUsuario" Visible="False" VisibleIndex="7"
                                            ShowInCustomizationForm="False">
                                            <Settings AllowAutoFilter="False" ShowFilterRowMenu="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="CodUnidResp" FieldName="CodigoEntidadeResponsavelUsuario"
                                            Visible="False" VisibleIndex="8" ShowInCustomizationForm="False">
                                            <Settings AllowAutoFilter="False" ShowFilterRowMenu="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="TipoAutenticacao" ShowInCustomizationForm="False"
                                            Visible="False" VisibleIndex="9">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn FieldName="CPF" ShowInCustomizationForm="False" VisibleIndex="10"
                                            Width="150px" Caption="CPF" ToolTip="Para filtrar, não digite ponto e nem traço, apenas dígitos!">
                                            <PropertiesTextEdit>
                                                <MaskSettings IncludeLiterals="None" Mask="999,999,999-99" />
                                            </PropertiesTextEdit>
                                        </dxtv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                    <SettingsPager PageSize="100">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>

                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                        <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                    </SettingsCommandButton>

                                    <SettingsPopup>
                                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                                    </SettingsPopup>

                                    <SettingsText CommandClearFilter="Limpar filtro"></SettingsText>
                                    <Templates>
                                        <FooterRow>
                                            <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td class="grid-legendas-cor grid-legendas-cor-inativo">
                                                            <span></span>
                                                        </td>
                                                        <td class="grid-legendas-label grid-legendas-label-inativo">
                                                            <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                                Text="<%# Resources.traducao.CadastroUsuariosNovo_usu_rios_inativos %>">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="display: none; padding-right: 10px; text-align: right;">
                                                            <dxcp:ASPxImage ID="imgStatusAtivacao" ClientInstanceName="imgStatusAtivacao" Width="25px" Height="25px" Cursor="pointer" ImageUrl="~/imagens/contrato_Pagamento.png" runat="server" OnInit="imgStatusAtivacao_Init" ShowLoadingImage="false">
                                                                <ClientSideEvents Click="function(s, e) {
	                                                                                              var window_width = window.innerWidth - (window.innerWidth * 0.50);
                                                                                                  var window_height = window.innerHeight - (window.innerHeight * 0.85);
                                                                                                  var url = 'StatusAtivacaoPortal.aspx';
                                                                                                  window.top.showModal(url, 'Gestão de Licenças de Uso', window_width, window_height, '', null);
                                                                                }" />
                                                            </dxcp:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </FooterRow>
                                    </Templates>
                                </dxwgv:ASPxGridView>
                            </div>
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ForeColor="#99ff99" ShowCloseButton="False" Width="680px"
                                ID="pcDados">
                                <ClientSideEvents CloseUp="function(s, e) {
    hlkVerificarEmail.SetEnabled(false);
    emailAlterado = false;
                                    
	OnGridFocusedRowChanged(gvDados);
}"
                                    PopUp="function(s, e) {
	showPcDado(s, e);
}"
                                    Shown="function(s, e) {
                          
	emailAlterado = false;
    hlkVerificarEmail.SetEnabled(false);
    if(TipoOperacao != &quot;Incluir&quot; && TipoOperacao.length != 0)
		OnGridFocusedRowChanged(gvDados);	
	showPcDado(s, e);
}"
                                    Init="function(s, e) {

}"></ClientSideEvents>
                                <ContentStyle>
                                    <Paddings Padding="5px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Email:" ClientInstanceName="lblEmail"
                                                                            ID="lblEmail">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 85%">
                                                                        <dxe:ASPxTextBox runat="server" MaxLength="150" ClientInstanceName="txtEmail"
                                                                            ID="txtEmail" Width="100%">
                                                                            <ClientSideEvents KeyUp="function(s, e) {
	var estado = (s.GetText().length &gt; 0) &amp;&amp; (validateEmail(s.GetText()));

	hlkVerificarEmail.SetEnabled(estado);
}"
                                                                                TextChanged="function(s, e) {
	emailAlterado = true;
    hlkVerificarEmail.SetEnabled(true);
}"></ClientSideEvents>
                                                                            <DisabledStyle BackColor="#EBEBEB">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-left: 10px;">
                                                                        <dxe:ASPxHyperLink runat="server" Text="Verificar email" ClientInstanceName="hlkVerificarEmail"
                                                                            ClientEnabled="False" Cursor="pointer" Font-Underline="True"
                                                                            ForeColor="Blue" ID="hlkVerificarEmail">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(verificarConfirmacaoEmailUsuarioNovo()){
        emailAlterado = false;  
		hfNovoEmailUsuario.PerformCallback('verificar');
    }
}"></ClientSideEvents>
                                                                            <DisabledStyle ForeColor="Silver">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxHyperLink>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfNovoEmailUsuario" ID="hfNovoEmailUsuario"
                                                            OnCustomCallback="hfNovoEmailUsuario_CustomCallback1">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	
                                                                
   // Permite a inclusao de um email invalido
    if(hfConfirmacaoEmailUsuario.Get('confirmacaoEmailInvalido') && hfNovoEmailUsuario.Get('emailVerificadoUsuarioNovo').indexOf('I') == 0 ){
                statusComponentesAposVerificacaoEmail();
                trataResultadoVerificacaoEmail(s,e);                                                          
                                                                
      }else{
        // função para interagir com o usuario sobre o
	    // resultado da verificação do email
        trataResultadoVerificacaoEmail(s,e);
        hlkVerificarEmail.SetEnabled(false);
    }
}"></ClientSideEvents>
                                                        </dxhf:ASPxHiddenField>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxCallbackPanel ID="pnCallbackFormulario" runat="server" ClientInstanceName="pnCallbackFormulario" OnCallback="pnCallbackFormulario_Callback" Width="100%">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	onEnd_pnCallbackFormulario(s, e);
}" />
                                                            <PanelCollection>
                                                                <dxtv:PanelContent runat="server">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                        <tr>
                                                                                            <td class="auto-style2">
                                                                                                <dxtv:ASPxLabel ID="lblNome" runat="server" ClientInstanceName="lblNome" Text="Nome:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxTextBox ID="txtNomeUsuario" runat="server" ClientEnabled="False" ClientInstanceName="txtNomeUsuario" MaxLength="60" Width="100%">
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
                                                                                    <table width="100%">
                                                                                        <tr>
                                                                                            <td style="width: 125px">
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxLabel ID="lblCPF" runat="server" ClientInstanceName="lblCPF" Text="CPF:">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td><span id="spanHelp" runat="server" style="cursor: pointer">
                                                                                                            <dxtv:ASPxCallbackPanel ID="pnHelp" runat="server" ClientInstanceName="pnHelp" ClientVisible="False" OnCallback="pnHelp_Callback">
                                                                                                                <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" />
                                                                                                                <ClientSideEvents EndCallback="function(s, e) {
var visivel = s.cp_Visivel;
s.SetVisible(visivel == 'S');	
}" />
                                                                                                                <SettingsCollapsing AnimationType="None">
                                                                                                                </SettingsCollapsing>
                                                                                                                <Paddings Padding="0px" />
                                                                                                                <PanelCollection>
                                                                                                                    <dxtv:PanelContent runat="server">
                                                                                                                        <dxtv:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/ajuda.png">
                                                                                                                        </dxtv:ASPxImage>
                                                                                                                    </dxtv:PanelContent>
                                                                                                                </PanelCollection>
                                                                                                            </dxtv:ASPxCallbackPanel>
                                                                                                            </span></td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td style="width: 135px">
                                                                                                <dxtv:ASPxLabel ID="lblTelefono1" runat="server" ClientInstanceName="lblTelefono1" Text="Telefone 1:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 135px">
                                                                                                <dxtv:ASPxLabel ID="lblTelefono2" runat="server" ClientInstanceName="lblTelefono2" Text="Telefone 2:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 140px">&nbsp;</td>
                                                                                            <td>
                                                                                                <dxtv:ASPxLabel ID="lblPerfil" runat="server" ClientInstanceName="lblPerfil" Text="Perfil:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="padding-right: 5px">
                                                                                                <dxtv:ASPxCallbackPanel ID="pnCPF" runat="server" ClientInstanceName="pnCPF" OnCallback="pnCPF_Callback" Width="100%">
                                                                                                    <SettingsLoadingPanel Enabled="False" ShowImage="False" />
                                                                                                    <ClientSideEvents EndCallback="function(s, e) {
var podeEditar = s.cp_PodeEditarCPF;
txtCPF.SetEnabled(podeEditar == &quot;S&quot;);

}" />
                                                                                                    <SettingsCollapsing AnimationType="None">
                                                                                                    </SettingsCollapsing>
                                                                                                    <Paddings Padding="0px" />
                                                                                                    <PanelCollection>
                                                                                                        <dxtv:PanelContent runat="server">
                                                                                                            <dxtv:ASPxTextBox ID="txtCPF" runat="server" ClientInstanceName="txtCPF" MaxLength="16" Width="100%">
                                                                                                                <ClientSideEvents KeyUp="function(s, e) {
          var textoCampo = s.GetText().replace((/\./g) ,&quot;&quot;).replace((/\-/g) ,&quot;&quot;).replace((/\_/g) ,&quot;&quot;); 
           if(textoCampo.length == 11)
          {
                    callbackVerificaSeExisteCPF.PerformCallback(s.GetText() + '|' + hfGeral.Get(&quot;CodigoUsuarioAtual&quot;));
           }	
}" />
                                                                                                                <MaskSettings IncludeLiterals="None" Mask="999,999,999-99" />
                                                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                                                </ValidationSettings>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxtv:ASPxTextBox>
                                                                                                        </dxtv:PanelContent>
                                                                                                    </PanelCollection>
                                                                                                </dxtv:ASPxCallbackPanel>
                                                                                            </td>
                                                                                            <td style="padding-right: 5px">
                                                                                                <dxtv:ASPxTextBox ID="txtTelefoneContato1" runat="server" ClientEnabled="False" ClientInstanceName="txtTelefoneContato1" MaxLength="15" Width="100%">
                                                                                                    <ClientSideEvents KeyPress="function(s, e) {
impedirQueLetrasSejamDigitadasNoCampo(s,e);
}" LostFocus="function(s, e) {
configurarMascaraDeTelefoneAoSairDoCampo(s, e);	
}" />
                                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxTextBox>
                                                                                            </td>
                                                                                            <td style="padding-right: 5px">
                                                                                                <dxtv:ASPxTextBox ID="txtTelefoneContato2" runat="server" ClientEnabled="False" ClientInstanceName="txtTelefoneContato2" MaxLength="15" Width="100%">
                                                                                                    <ClientSideEvents KeyPress="function(s, e) {
impedirQueLetrasSejamDigitadasNoCampo(s,e);
}" LostFocus="function(s, e) {
configurarMascaraDeTelefoneAoSairDoCampo(s, e);
}" />
                                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxTextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxtv:ASPxCheckBox ID="ckbAtivo" runat="server" Checked="True" CheckState="Checked" ClientInstanceName="ckbAtivo" Text="Usuário Ativo">
                                                                                                </dxtv:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxtv:ASPxComboBox ID="cmbPerfil" runat="server" ClientEnabled="False" ClientInstanceName="cmbPerfil" Width="100%">
                                                                                                    <ClientSideEvents EndCallback="function(s, e) {
	//txtLogin.SetEnabled(cmbTipoAutentica.GetSelectedIndex()==0);
	if(cmbTipoAutentica.GetValue() == &quot;AI&quot; || cmbTipoAutentica.GetValue() == &quot;AP&quot;)
		txtLogin.SetEnabled(true);
	else
		txtLogin.SetEnabled(false);
}" Init="function(s, e) {
	if(cmbTipoAutentica.GetValue() == &quot;AI&quot; || cmbTipoAutentica.GetValue() == &quot;AP&quot;)
		txtLogin.SetEnabled(true);
	else
		txtLogin.SetEnabled(false);
}" SelectedIndexChanged="function(s, e)
{
	if(!cmbTipoAutentica.GetEnabled())
		return;
	//txtLogin.SetEnabled(cmbTipoAutentica.GetSelectedIndex() == 0);
	if(cmbTipoAutentica.GetValue() == &quot;AI&quot; || cmbTipoAutentica.GetValue() == &quot;AP&quot;)
	{
		txtLogin.SetText(&quot;&quot;);
		txtLogin.SetEnabled(true);
	}
	if(cmbTipoAutentica.GetValue() == &quot;AS&quot;)
	{
		txtLogin.SetText(&quot;&quot;);
		txtLogin.SetEnabled(false);
	}
}" />
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxComboBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table border="0" cellpadding="0" cellspacing="3" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 290px">
                                                                                                    <dxtv:ASPxLabel ID="lblTipoAuten" runat="server" ClientInstanceName="lblTipoAuten" Text="Tipo de Autenticação:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxLabel ID="lblConta" runat="server" ClientInstanceName="lblConta" Text="Conta para autenticação:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-right: 5px">
                                                                                                    <dxtv:ASPxComboBox ID="cmbTipoAutentica" runat="server" ClientEnabled="False" ClientInstanceName="cmbTipoAutentica" Width="100%">
                                                                                                        <ClientSideEvents EndCallback="function(s, e) {
	//txtLogin.SetEnabled(cmbTipoAutentica.GetSelectedIndex()==0);
	if(cmbTipoAutentica.GetValue() == &quot;AI&quot; || cmbTipoAutentica.GetValue() == &quot;AP&quot;)
		txtLogin.SetEnabled(true);
	else
		txtLogin.SetEnabled(false);
}" Init="function(s, e) {
	if(cmbTipoAutentica.GetValue() == &quot;AI&quot; || cmbTipoAutentica.GetValue() == &quot;AP&quot;)
		txtLogin.SetEnabled(true);
	else
		txtLogin.SetEnabled(false);
}" SelectedIndexChanged="function(s, e)
{
	//txtLogin.SetEnabled(cmbTipoAutentica.GetSelectedIndex() == 0);
	if(cmbTipoAutentica.GetValue() == &quot;AI&quot; || cmbTipoAutentica.GetValue() == &quot;AP&quot;)
	{
		txtLogin.SetText(&quot;&quot;);
		txtLogin.SetEnabled(true);
	}
	if(cmbTipoAutentica.GetValue() == &quot;AS&quot;)
	{
		txtLogin.SetText(&quot;&quot;);
		txtLogin.SetEnabled(false);
	}
}" />
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxTextBox ID="txtLogin" runat="server" ClientEnabled="False" ClientInstanceName="txtLogin" MaxLength="100" ToolTip="Conta de Acesso à Rede do Usuário" Width="100%">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxLabel ID="lblObservacao" runat="server" ClientInstanceName="lblObservacao" Text="Observações:">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxMemo ID="memObservacoes" runat="server" ClientEnabled="False" ClientInstanceName="memObservacoes" Height="100px" Width="100%">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxtv:ASPxMemo>
                                                                                    <dxtv:ASPxLabel ID="lblContadorMemo" runat="server" ClientInstanceName="lblContadorMemo" Font-Bold="True" Font-Size="7pt" ForeColor="#999999">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxtv:PanelContent>
                                                            </PanelCollection>
                                                        </dxtv:ASPxCallbackPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <div style="position:absolute;bottom:0px;right:5px">
                                                            <table class="formulario-botoes" id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="left" width="310">
                                                                        <dxe:ASPxLabel ID="lblMensagemOutraEntidade" runat="server" ClientInstanceName="lblMensagemOutraEntidade"
                                                                            ClientVisible="False" Font-Bold="True" Font-Italic="True"
                                                                            Font-Size="7pt" ForeColor="Red" Text="*Usuário cadastrado em outra Entidade">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td class="formulario-botao" id="tdRedefinirSenha">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRedefinirSenha"
                                                                            Text="Redefinir Senha" Width="120px" ID="btnRedefinirSenha">
                                                                            <ClientSideEvents Click="function(s, e) {
	if(confirm(traducao.CadastroUsuariosNovo_deseja_redefinir_a_senha_do_usu_rio_selecionado_))
	{
		callbackSenha.PerformCallback();
	}
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                            ClientInstanceName="btnSalvar1" Text="<%$ Resources:traducao, CadastroUsuariosNovo_salvar %>"
                                                                            UseSubmitBehavior="False" Width="100px">
                                                                            <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
    if (emailAlterado)
       window.top.mostraMensagem(traducao.CadastroUsuariosNovo_e_mail_n_o_foi_verificado_, 'Atencao', true, false, null);
    else 
	   onClick_SalvarUsuario(s, e);
}" />
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="100px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    emailAlterado = false;
	hfGeral.Set(&quot;RowFocusChanged&quot;, &quot;1&quot;);
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        </div>
                                                        
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxtv:ASPxPopupControl ID="pcCopiaPermissoes" runat="server" AllowDragging="True"
                                AllowResize="True" ClientInstanceName="pcCopiaPermissoes"
                                HeaderText="<%$ Resources:traducao,copia_permiss_es %>" Modal="True" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="721px" CssClass="popup">
                                <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = true;
	pcCopiaPermissoes_OnPopup(s,e);
}" />
                                <ContentCollection>
                                    <dxtv:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <table style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="100%">
                                                        <dxe:ASPxLabel ID="lblUsuarioAlvo" runat="server" ClientInstanceName="lblUsuarioAlvo"
                                                            Font-Size="7pt" Font-Bold="True" Width="100%" CssClass="titleCopiarPerfil">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="100%">
                                                        <dxe:ASPxLabel ID="lblCopia" runat="server" ClientInstanceName="lblCopia"
                                                            Font-Bold="True" Width="100%" CssClass="titleCopiarPerfilDanger">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxRoundPanel ID="rpUsuarioOrigem" runat="server"
                                                            HeaderText="Selecione o usuário de origem, o qual irá fornecer o conjunto de permissões para  a cópia:"
                                                            View="GroupBox" Width="100%" ClientInstanceName="rpUsuarioOrigem" HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <PanelCollection>
                                                                <dxtv:PanelContent ID="PanelContent1" runat="server">
                                                                    <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.String"
                                                                        Width="100%" ClientInstanceName="ddlUsuarioOrigem"
                                                                        ID="ddlUsuarioOrigem" TextField="NomeUsuario" ValueField="CodigoUsuario" EnableCallbackMode="True"
                                                                        OnItemRequestedByValue="ddlUsuarioOrigem_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlUsuarioOrigem_ItemsRequestedByFilterCondition"
                                                                        PopupHorizontalAlign="NotSet" OnCallback="ddlUsuarioOrigem_Callback">
                                                                        <Columns>
                                                                            <dxe:ListBoxColumn Width="300px" Caption="Nome"></dxe:ListBoxColumn>
                                                                            <dxe:ListBoxColumn Width="200px" Caption="Email"></dxe:ListBoxColumn>
                                                                        </Columns>
                                                                        <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                                                            ErrorText="*">
                                                                            <RequiredField IsRequired="True" />
                                                                            <RequiredField IsRequired="True"></RequiredField>
                                                                        </ValidationSettings>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxComboBox>
                                                                </dxtv:PanelContent>
                                                            </PanelCollection>
                                                        </dxtv:ASPxRoundPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding-top: 10px">
                                                        <table id="Table3" border="0" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr style="height: 35px">
                                                                    <td>
                                                                        <dxtv:ASPxButton ID="btnSalvarCopiaPermissoes" runat="server" AutoPostBack="False"
                                                                            CausesValidation="False" ClientInstanceName="btnSalvarCopiaPermissoes"
                                                                            Text="<%$ Resources:traducao, CadastroUsuariosNovo_salvar %>" UseSubmitBehavior="False" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvarCopiaPermissoes)
		onClick_btnSalvarCopiaPermissoes();
}"></ClientSideEvents>
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                    <td align="right">&nbsp;
                                                                    </td>
                                                                    <td align="right">
                                                                        <dxtv:ASPxButton ID="btnFecharCopiaPermissoes" runat="server" AutoPostBack="False"
                                                                            ClientInstanceName="btnFecharCopiaPermissoes" Text="Fechar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelarCopiaPermissoes)
       onClick_btnCancelarCopiaPermissoes();
}"></ClientSideEvents>
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxtv:ASPxHiddenField ID="hfStatusCopiaPermissoes" runat="server" ClientInstanceName="hfStatusCopiaPermissoes"
                                            OnCustomCallback="hfStatusCopiaPermissoes_CustomCallback">
                                            <ClientSideEvents EndCallback="function(s, e) {
	hfStatusCopiaPermissoes_onEndCallback(s,e);
}" />
                                        </dxtv:ASPxHiddenField>
                                    </dxtv:PopupControlContentControl>
                                </ContentCollection>
                            </dxtv:ASPxPopupControl>
                            <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackSenha" ID="callbackSenha"
                                OnCallback="callbackSenha_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
       if(s.cp_MSG != '')
      {
              window.top.mostraMensagem(s.cp_MSG, 'sucesso', false, false, null, 3000);
      }
       else if(s.cp_Erro != '')
      {
             window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null, 3000);
      }
}"></ClientSideEvents>
                            </dxcb:ASPxCallback>
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" ExportEmptyDetailGrid="True"
                                GridViewID="gvDados" Landscape="True" LeftMargin="50" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
                                RightMargin="50">
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
                            <dxtv:ASPxPopupControl ID="popupHelpUsuario" runat="server" ClientInstanceName="popupHelpUsuario"
                                HeaderText="" Modal="True" PopupElementID="spanHelp"
                                PopupHorizontalAlign="Center" PopupHorizontalOffset="100" PopupVerticalAlign="Middle"
                                PopupVerticalOffset="-60" Width="460px">
                                <HeaderImage Url="~/imagens/ajuda.png">
                                </HeaderImage>
                                <ContentCollection>
                                    <dxtv:PopupControlContentControl runat="server">
                                        Não é possível alterar o C.P.F. do usuário, pois o mesmo possui vínculo de assinaturas
                                                        digitais de formulários dinamicos em seu nome.
                                    </dxtv:PopupControlContentControl>
                                </ContentCollection>
                            </dxtv:ASPxPopupControl>
                            <dxtv:ASPxGridViewExporter ID="gridViewExporterImpedimento" runat="server" ExportSelectedRowsOnly="false" ExportEmptyDetailGrid="True" GridViewID="gvImpedimentos" Landscape="True" LeftMargin="50" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" RightMargin="50">
                            </dxtv:ASPxGridViewExporter>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcEntidadActual" HeaderText="Incluir usu&#225;rio na Entidade Atual?"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="440px" ID="pcEntidadActual">
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 100%;" align="center">
                                                        <dxe:ASPxLabel runat="server" Text="Aten&#231;&#227;o! Este email &#233; de um usu&#225;rio de outra entidade. Deseja incluir o usu&#225;rio nesta entidade tamb&#233;m?"
                                                            ClientInstanceName="lblMsgIncluirEntidadAtual"
                                                            ID="lblMsgIncluirEntidadAtual">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table style="text-align: center" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSim" Text="Sim" Width="80px"
                                                                            ID="btnSim">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	SimEntidadAtual_OnClick();
//	SalvarCamposEntidadeAtual();
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnNao" Text="N&#227;o" Width="80px"
                                                                            ID="btnNao">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcEntidadActual.Hide();
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
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
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioExcluido" HeaderText="Re-incluir usu&#225;rio?"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="440px" ID="pcUsuarioExcluido">
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 100%;" align="center">
                                                        <dxe:ASPxLabel runat="server" Text="Este e-mail &#233; de um usu&#225;rio que foi exclu&#237;do anteriormente. Deseja re-incluir este usu&#225;rio?"
                                                            ClientInstanceName="lblMsgUsuarioEliminado"
                                                            ID="lblMsgUsuarioEliminado">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table style="text-align: center" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSimUsuarioExcluido"
                                                                            Text="Sim" Width="80px" ID="btnSimUsuarioExcluido">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	SimUsuarioExcluido_OnClick();
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnNaoUsuarioExcluido"
                                                                            Text="N&#227;o" Width="80px" ID="btnNaoUsuarioExcluido">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcUsuarioExcluido.Hide();
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
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
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemConfirmacaoEmail" HeaderText="Mensagem do Sistema!"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="520px" ID="pcMensagemConfirmacaoEmail">
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 100%;" align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblMensagemConfirmacaoEmail"
                                                            ID="lblMensagemConfirmacaoEmail">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnConfirmacao"
                                                            Text="OK" Width="90px" ID="btnConfirmacao">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;   
	pcMensagemConfirmacaoEmail.Hide();
    
    hfConfirmacaoEmailUsuario.Set('confirmacaoEmailInvalido', true);
    hfNovoEmailUsuario.PerformCallback();

}"></ClientSideEvents>
                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxe:ASPxButton>

                                                        <dxe:ASPxButton ID="btnFecharPopUp" runat="server" ClientInstanceName="btnFecharPopUp" Text="Fechar" Width="100px">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    emailAlterado = false; 
	pcMensagemConfirmacaoEmail.Hide();
}" />
                                                        </dxe:ASPxButton>
                                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfConfirmacaoEmailUsuario" ID="hfConfirmacaoEmailUsuario"
                                                            OnCustomCallback="hfConfirmacaoEmailUsuario_CustomCallback1">
                                                            <ClientSideEvents EndCallback="function(s, e) {
                                                                
    hfNovoEmailUsuario.PerformCallback();
	// função que permite a inclusao de um email invalido
	
}"></ClientSideEvents>
                                                        </dxhf:ASPxHiddenField>

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
                                                    <td style="width: 100%;" align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblMensagemVarios"
                                                            ID="lblMensagemVarios">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
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
                            <dxtv:ASPxCallbackPanel ID="callbackTelaImpedimento" runat="server" ClientInstanceName="callbackTelaImpedimento" OnCallback="callbackTelaImpedimento_Callback1">
                                <ClientSideEvents EndCallback="function(s, e) 
{
	if(s.cpErro != '')
                {
                       window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
                }
                else
                {
                       if(s.cpSucesso != '')
                       {
                           pcMensagemGravacao.Hide();    
                           ckbAtivo.SetChecked(false);
                                    
                            window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3000); 
             onClick_SalvarUsuario(s, e);    
                       }
                }
}
" />
                                <PanelCollection>
                                    <dxtv:PanelContent runat="server">
                                        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" CloseAction="None"
                                            EnableClientSideAPI="True" HeaderText="Mensagem" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowShadow="False"
                                            Width="721px" ID="pcMensagemGravacao" AllowDragging="True" Modal="True">
                                            <HeaderImage Url="~/imagens/alertAmarelho.png">
                                            </HeaderImage>
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

                                                                                                    <SettingsPopup>
                                                                                                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                                                    </SettingsPopup>
                                                                                                    <Columns>
                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUnidade" Name="NomeUnidade" Width="40%"
                                                                                                            Caption="Item" VisibleIndex="0">
                                                                                                            <HeaderTemplate>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td align="center">
                                                                                                                            <dxtv:ASPxMenu ID="menu_impedimento" runat="server" BackColor="Transparent" ClientInstanceName="menu_impedimento" ItemSpacing="5px" OnInit="menu_impedimento_Init" OnItemClick="menu_impedimento_ItemClick">
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
                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="codImpedimento" Name="codImpedimento" Caption="Impedimento"
                                                                                                            Visible="False" VisibleIndex="1">
                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                    </Columns>
                                                                                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                                    </SettingsPager>
                                                                                                    <Settings VerticalScrollBarMode="Visible"></Settings>
                                                                                                </dxwgv:ASPxGridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table id="tbColores" cellpadding="0" width="100%" border="0">
                                                                                                    <tbody>
                                                                                                        <tr id="corUN">
                                                                                                            <td style="border-right: green 2px solid; border-top: green 2px solid; border-left: green 2px solid; width: 30px; border-bottom: green 2px solid; background-color: #98fb98">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Unidades relacionadas ao usu&#225;rio." ClientInstanceName="lblDescricaoCorUnidade"
                                                                                                                    ID="lblDescricaoCorUnidade">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr id="corPR">
                                                                                                            <td style="border-right: blue 2px solid; border-top: blue 2px solid; border-left: blue 2px solid; width: 30px; border-bottom: blue 2px solid; background-color: #b0c4de">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Projetos ou Programas relacionados ao usu&#225;rio."
                                                                                                                    ID="ASPxLabel2">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr id="corIN">
                                                                                                            <td style="border-right: gray 2px solid; border-top: gray 2px solid; border-left: gray 2px solid; width: 30px; border-bottom: gray 2px solid; background-color: #dcdcdc">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Indicadores relacionados ao usu&#225;rio."
                                                                                                                    ID="ASPxLabel3">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr style="display: none" id="corIO">
                                                                                                            <td style="border-right: fuchsia 2px solid; border-top: fuchsia 2px solid; border-left: fuchsia 2px solid; width: 30px; border-bottom: fuchsia 2px solid; background-color: #d8bfd8">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Indicadores Operacionales relacionados ao usu&#225;rio."
                                                                                                                    ID="ASPxLabel4">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr id="corDE">
                                                                                                            <td style="border-right: yellow 2px solid; border-top: yellow 2px solid; border-left: yellow 2px solid; width: 30px; border-bottom: yellow 2px solid; background-color: #eee8aa">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Demandas relacionadas ao usu&#225;rio."
                                                                                                                    ID="ASPxLabel5">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr id="corTD">
                                                                                                            <td style="border-right: yellow 2px solid; border-top: yellow 2px solid; border-left: yellow 2px solid; width: 30px; border-bottom: yellow 2px solid; background-color: #CCCC00; border-color: #996633;">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Tarefas TodoList relacionadas ao usu&#225;rio."
                                                                                                                    ID="ASPxLabel1">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td style="width: 120px">
                                                                                                            <dxtv:ASPxButton ID="btnSimTelaImpedimento" runat="server" AutoPostBack="False" CausesValidation="False" ClientInstanceName="btnSimTelaImpedimento" Text="Sim" UseSubmitBehavior="False" Width="100%">
                                                                                                                <ClientSideEvents Click="function(s, e) {
	callbackTelaImpedimento.PerformCallback();
}" />
                                                                                                            </dxtv:ASPxButton>
                                                                                                        </td>
                                                                                                        <td style="width: 120px; padding-left: 5px">
                                                                                                            <dxtv:ASPxButton ID="btnNaoTelaImpedimento" runat="server" AutoPostBack="False" CausesValidation="False" ClientInstanceName="btnNaoTelaImpedimento" Text="Não" UseSubmitBehavior="False" Width="100%">
                                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcMensagemGravacao.Hide();
}" />
                                                                                                            </dxtv:ASPxButton>
                                                                                                        </td>

                                                                                                    </tr>
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
                                    </dxtv:PanelContent>
                                </PanelCollection>
                            </dxtv:ASPxCallbackPanel>

                        </dxp:PanelContent>
                    </PanelCollection>
                    <SettingsLoadingPanel Text=" "></SettingsLoadingPanel>
                    <ClientSideEvents EndCallback="function(s, e) {
	onEnd_pnCallbackLocal(s, e);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <dxtv:ASPxCallback ID="callbackVerificaSeExisteCPF" runat="server" ClientInstanceName="callbackVerificaSeExisteCPF" OnCallback="callbackVerificaSeExisteCPF_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
mensagemCPFJaExiste = s.cp_atencao;
}" />
    </dxtv:ASPxCallback>
    <asp:SqlDataSource ID="dsDados" runat="server" SelectCommand="SELECT CodigoUsuario, NomeUsuario, ContaWindows, TipoAutenticacao, SenhaAcessoAutenticacaoSistema, EMail, ResourceUID, TelefoneContato1, TelefoneContato2, Observacoes, DataInclusao, CodigoUsuarioInclusao, DataUltimaAlteracao, CodigoUsuarioUltimaAlteracao, DataExclusao, CodigoUsuarioExclusao, IDEstiloVisual, CodigoEntidadeResponsavelUsuario FROM Usuario"></asp:SqlDataSource>
</asp:Content>
