<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="cadastroEntidades.aspx.cs" Inherits="administracao_cadastroEntidades"
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
                             <div id="divGrid" style="visibility:visible;padding-top:5px">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoUnidadeNegocio"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnHtmlRowPrepared="grid_HtmlRowPrepared"
                                OnAfterPerformCallback="grid_AfterPerformCallback" OnCustomCallback="gvDados_CustomCallback"
                                OnCustomErrorText="gvDados_CustomErrorText">
                                <ClientSideEvents BeginCallback="function(s,e){window.top.lpAguardeMasterPage.Show();;
                                                                                                                                                        }" />
                                <ClientSideEvents FocusedRowChanged="function(s, e) {OnGridFocusedRowChanged(s);}"
                                    CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     btnSalvar1.SetVisible(true);
      if(e.buttonID == 'btnNovo')
     {
        hfGeral.Set('NomeArquivo', '');
        onClickBarraNavegacao('Incluir', gvDados, pcDados);
        TipoOperacao = 'Incluir';
		hfGeral.Set('TipoOperacao', TipoOperacao);
     }
     if(e.buttonID == 'btnEditar')
     {
        hfGeral.Set('NomeArquivo', '');
		onClickBarraNavegacao('Editar', gvDados, pcDados);
		TipoOperacao = 'Editar';
		hfGeral.Set('TipoOperacao', TipoOperacao);
     }
     else if(e.buttonID == 'btnExcluir')
     {
                               hfGeral.Set('NomeArquivo', '');
        &nbsp;                     window.top.mostraMensagem(traducao.cadastroEntidades_deseja_realmente_excluir_o_registro_, 'confirmacao', true, true, excluiRegistro);
    }
     else if(e.buttonID == 'btnDetalhesCustom')
     {	
                                hfGeral.Set('NomeArquivo', '');
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar1.SetVisible(false);
		TipoOperacao = 'Consultar';
		hfGeral.Set('TipoOperacao', TipoOperacao);
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
"
EndCallback="function(s, e) {	
    window.top.lpAguardeMasterPage.Hide();
    if (s.cp_StatusSalvar == &quot;0&quot; &amp;&amp; s.cp_OperacaoOk == &quot;&quot;){
            window.top.mostraMensagem(s.cp_ErroSalvar, 'atencao', true, false, null);
        }
        else
        {
            var definicaoEntidade = hfGeral.Contains(&quot;definicaoEntidade&quot;) ? hfGeral.Get(&quot;definicaoEntidade&quot;).toString() : &quot;Entidade&quot;;

            if (&quot;Incluir&quot; == s.cp_OperacaoOk)
                mostraDivSalvoPublicado(definicaoEntidade + ' ' + traducao.cadastroEntidades_inclu_da_com_sucesso);
            else if (&quot;Editar&quot; == s.cp_OperacaoOk)
                mostraDivSalvoPublicado(traducao.cadastroEntidades_dados_gravados_com_sucesso);
            else if (&quot;Excluir&quot; == s.cp_OperacaoOk)
                mostraDivSalvoPublicado(definicaoEntidade + ' ' + traducao.cadastroEntidades_exclu_da_com_sucesso);
        }
}"  
Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}" ></ClientSideEvents>

                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                </SettingsCommandButton>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
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
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnCompartilharCustom" Text="Administrar Permissões da Entidade">
                                                <Image ToolTip="Administrar Permissões da Entidade" Url="~/imagens/Perfis/Perfil_Permissoes.png">
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
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio" Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeNegocio" Width="110px" Caption="Sigla"
                                        VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUnidadeNegocio" Caption="Entidade" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SiglaUF" Width="55px" Caption="UF" VisibleIndex="4"
                                        Name="SiglaUF">
                                        <Settings AllowDragDrop="True" AllowGroup="True" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioGerente" Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaUnidadeNegocioAtiva" Name="IndicaUnidadeNegocioAtiva"
                                        Visible="False" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Observacoes" Visible="False" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Email" Width="310px" Caption="Email do Respons&#225;vel"
                                        VisibleIndex="10">
                                        <Settings AllowDragDrop="True" AllowGroup="True" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TelefoneContato1" Visible="False" VisibleIndex="9">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaUnidadeNegocioAtiva" Name="IndicaUnidadeNegocioAtiva"
                                        Width="70px" Caption="Ativo" VisibleIndex="11">
                                        <Settings AllowDragDrop="True" AllowGroup="True" />
                                        <DataItemTemplate>
                                            <%# (Eval("IndicaUnidadeNegocioAtiva").ToString() == "S") ? Resources.traducao.sim : Resources.traducao.nao%>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoReservado" Caption="C&#243;digo Reservado"
                                        Visible="False" VisibleIndex="14">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Latitude" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="12">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Longitude" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="13">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings ShowFooter="True" VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>
                                <Templates>
                                    <FooterRow>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="grid-legendas-cor grid-legendas-cor-inativo"><span></span></td>
                                                    <td class="grid-legendas-label grid-legendas-label-inativo">
                                                        <span>
                                                            <%# SetIniciaisMaiusculas(Resources.traducao.unidadesNegocio_inativas) + " " + hfGeral.Get("definicaoEntidadePlural").ToString() %>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </FooterRow>
                                </Templates>
                                <Styles>
                                    <Footer>
                                    </Footer>
                                </Styles>
                            </dxwgv:ASPxGridView>
                            </div>
                                 <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" PopupVerticalOffset="30" ShowCloseButton="False"
                                Width="760px" ID="pcDados">
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
                                                                    <td style="width: 165px; height: 62px" id="Td2" valign="top">
                                                                        <div id="divLogoImagem">
                                                                        <dx:ASPxLoadingPanel ID="lpLogoImagem" runat="server" ClientInstanceName="lpLogoImagem" ContainerElementID="divLogoImagem" Modal="False" ><Image Width="20px" Height="20px" /></dx:ASPxLoadingPanel>
                                                                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnLogo" ID="pnLogo" OnCallback="pnLogo_Callback">
                                                                            <ClientSideEvents BeginCallback="function(s,e){lpLogoImagem.Show();}" />
                                                                            <ClientSideEvents EndCallback="function(s, e) {lpLogoImagem.Hide();}" />
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
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: 5px"></td>
                                                                    <td valign="top">
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 80px">
                                                                                        <dxe:ASPxLabel runat="server" Text="Sigla:" ClientInstanceName="lblSigla"
                                                                                            ID="lblSigla">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td style="width: 5px"></td>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel runat="server" Text="Nome:" ClientInstanceName="lblNomeEntidade"
                                                                                            ID="lblNomeEntidade">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="10" ClientInstanceName="txtSigla"
                                                                                            ID="txtSigla">
                                                                                            <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtEntidade"
                                                                                            ID="txtEntidade">
                                                                                            <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 161px">
                                                                                        <dxe:ASPxLabel runat="server" Text="C&#243;digo Reservado:"
                                                                                            ID="ASPxLabel1">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td style="width: 200px">
                                                                                        <dxe:ASPxLabel runat="server" Text="UF:" ClientInstanceName="lblUF"
                                                                                            ID="lblUF">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 161px">
                                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 133px">
                                                                                                        <dxe:ASPxTextBox runat="server" Width="95%" MaxLength="20" ClientInstanceName="txtCodigoReservado"
                                                                                                            ID="txtCodigoReservado">
                                                                                                            <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ToolTip="C&#243;digo utilizado para interface com outros sistemas da institui&#231;&#227;o"
                                                                                                            Width="18px" Height="18px" ClientInstanceName="imgAjuda" Cursor="pointer" ID="imgAjuda">
                                                                                                            <ClientSideEvents Click="function(s, e) {
	pcAjuda.Show();
}"></ClientSideEvents>
                                                                                                        </dxe:ASPxImage>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" TextField="SiglaUF" ValueField="SiglaUF"
                                                                                            Width="100%" ClientInstanceName="comboUF"
                                                                                            ID="comboUF">
                                                                                            <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxComboBox>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <dxe:ASPxCheckBox runat="server" Text="Entidade Ativa?" ClientInstanceName="ckbEntidadAtiva"
                                                                                            ID="ckbEntidadAtiva">
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
                                                    <td align="center" style="padding-top: 5px">
                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="Administrador da Entidade" Width="100%"
                                                            ClientInstanceName="rpAdministrador" ID="rpAdministrador">
                                                            <ContentPaddings PaddingLeft="5px" PaddingRight="5px"></ContentPaddings>
                                                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxLabel runat="server" Text="Email do Respons&#225;vel:" ClientInstanceName="lblEmailAdministrador"
                                                                                                                        ID="lblEmailAdministrador">
                                                                                                                    </dxe:ASPxLabel>
                                                                                                                </td>
                                                                                                                <td style="width: 95px"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="150" ClientInstanceName="txtEmail"
                                                                                                                        ID="txtEmail">
                                                                                                                        <ClientSideEvents KeyUp="function(s, e) {
	var tipoOperacaoAdm = hfGeral.Get(&quot;TipoOperacao&quot;);
	
	if(s.GetText().length &gt; 0)
		hlkVerificarEmail.SetEnabled(true);
	else
		hlkVerificarEmail.SetEnabled(false);

	if(&quot;Editar&quot; == tipoOperacaoAdm)
	{
			hlkVerificarEmail.SetEnabled(true);
			btnSalvar.SetEnabled(false);
	}
}"></ClientSideEvents>
                                                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Email Incorreto"
                                                                                                                            SetFocusOnError="True" ValidationGroup="MKE">
                                                                                                                            <RegularExpression ErrorText="" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                                                                                            <RequiredField IsRequired="True" ErrorText=""></RequiredField>
                                                                                                                        </ValidationSettings>
                                                                                                                        <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                                                                        </DisabledStyle>
                                                                                                                    </dxe:ASPxTextBox>
                                                                                                                </td>
                                                                                                                <td style="width: 95px;" align="center">
                                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnVerificarEmail" ClientEnabled="False"
                                                                                                                        Text="..." Font-Bold="True" ToolTip="Clique para validar o email e prosseguir com o cadastro."
                                                                                                                        ID="btnVerificarEmail" Visible="False">
                                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(verificarEmailUsuarioNovo())
	{
		var estadoTela = hfGeral.Get(&quot;TipoOperacao&quot;);
		if('Editar' == estadoTela)
			hfEmailAdministrador.PerformCallback('verificarEditar');
		else if ('Incluir' == estadoTela)
			hfEmailAdministrador.PerformCallback('verificar');
	}
}"></ClientSideEvents>
                                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                                    </dxe:ASPxButton>
                                                                                                                    <dxe:ASPxHyperLink ID="hlkVerificarEmail" runat="server" ClientEnabled="False" ClientInstanceName="hlkVerificarEmail"
                                                                                                                        Cursor="pointer" Font-Underline="True" ForeColor="Blue"
                                                                                                                        Text="Verificar email...">
                                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(verificarEmailUsuarioNovo())
	{
        window.top.lpAguardeMasterPage.Show();
		var estadoTela = hfGeral.Get(&quot;TipoOperacao&quot;);
		if('Editar' == estadoTela)
			hfEmailAdministrador.PerformCallback('verificarEditar');
		else if ('Incluir' == estadoTela)
			hfEmailAdministrador.PerformCallback('verificar');
	}
}" />
                                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(verificarEmailUsuarioNovo())
	{
        window.top.lpAguardeMasterPage.Show();
		var estadoTela = hfGeral.Get(&quot;TipoOperacao&quot;);
		if(&#39;Editar&#39; == estadoTela)
			hfEmailAdministrador.PerformCallback(&#39;verificarEditar&#39;);
		else if (&#39;Incluir&#39; == estadoTela)
			hfEmailAdministrador.PerformCallback(&#39;verificar&#39;);
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
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfEmailAdministrador" ID="hfEmailAdministrador"
                                                                                        OnCustomCallback="hfEmailAdministrador_CustomCallback">
                                                                                        <ClientSideEvents EndCallback="function(s, e) {
	                                                                                        window.top.lpAguardeMasterPage.Hide();
                                                                                            trataResultadoVerificacaoEmail(s,e);
}"></ClientSideEvents>
                                                                                    </dxhf:ASPxHiddenField>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackFormulario" 
                                                                                        Width="100%" ID="pnCallbackFormulario" OnCallback="pnCallbackFormulario_Callback">
                                                                                        <PanelCollection>
                                                                                            <dxp:PanelContent runat="server">
                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <dxe:ASPxLabel runat="server" Text="Nome:" ClientInstanceName="lblNomeAdministrador"
                                                                                                                                    ID="lblNomeAdministrador">
                                                                                                                                </dxe:ASPxLabel>
                                                                                                                            </td>
                                                                                                                            <td style="width: 150px">
                                                                                                                                <dxtv:ASPxLabel ID="lblTelefoneAdministrador" runat="server" ClientInstanceName="lblTelefoneAdministrador" Text="Telefone:">
                                                                                                                                </dxtv:ASPxLabel>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-right: 5px">
                                                                                                                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="60" ClientInstanceName="txtNome"
                                                                                                                                    ID="txtNome">
                                                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                                                                                                        <RequiredField IsRequired="True" ErrorText=""></RequiredField>
                                                                                                                                    </ValidationSettings>
                                                                                                                                    <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxe:ASPxTextBox>
                                                                                                                                </td>
                                                                                                                            <td>
                                                                                                                                <dxtv:ASPxTextBox ID="txtFone" runat="server" ClientInstanceName="txtFone" MaxLength="14" Width="100%">
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
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </dxp:PanelContent>
                                                                                        </PanelCollection>
                                                                                    </dxcp:ASPxCallbackPanel>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxrp:ASPxRoundPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Observa&#231;&#245;es:"
                                                            ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo runat="server" Rows="4" Width="100%" ClientInstanceName="memObservacoes"
                                                            ID="memObservacoes">
                                                            <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                        <dxe:ASPxLabel ID="lblContadorMemo" runat="server" ClientInstanceName="lblContadorMemo"
                                                            Font-Bold="True" ForeColor="#999999">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 32px">
                                                                        <dxuc:ASPxUploadControl runat="server" ClientInstanceName="LogoUpload" FileUploadMode="OnPageLoad"
                                                                            CancelButtonHorizontalPosition="Left" Width="100%" BackColor="White"
                                                                            ForeColor="Black" ID="LogoUpload" OnFileUploadComplete="LogoUpload_FileUploadComplete" NullText="Escolher Logomarca da Entidade: (157px X 40px)">
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
                                                                            <NullTextStyle ForeColor="Black">
                                                                            </NullTextStyle>
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
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100px"
                                                                            ID="btnSalvar" AutoPostBack="False">
                                                                            <ClientSideEvents Click="function(s, e) 
{
    var msgErro = validaCamposFormulario();
    if(msgErro != '')
    {
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);
    } 
    else
    {
                  e.processOnServer = false;
                  window.top.lpAguardeMasterPage.Show();
                  gvDados.PerformCallback(TipoOperacao);
    }
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100px"
                                                                            ID="btnFechar">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       LimpaCamposFormulario();
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
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
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
                                                            ClientInstanceName="lblMsgIncluirEntidadAtual" Width="100%"
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
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioExcluido" HeaderText="Reativar Usuario?"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="440px" ID="pcUsuarioExcluido">
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 100%;" align="center">
                                                        <dxe:ASPxLabel runat="server" Text="Este email &#233; de um usu&#225;rio que foi exclu&#237;do do sistema. Deseja reativar o usu&#225;rio?"
                                                            ClientInstanceName="lblMsgUsuarioEliminado" Width="100%"
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
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcAjuda"
                                CloseAction="CloseButton" HeaderText="Ajuda" PopupElementID="imgAjuda" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" Width="293px" Font-Size="10pt"
                                ID="pcAjuda">
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        Código utilizado para interface com outros sistemas da instituição.
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {	
	
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
                <dxhf:ASPxHiddenField ClientInstanceName="hfLogo" ID="hfLogo" runat="server">
                </dxhf:ASPxHiddenField>
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
                            <table style="width: 100%; padding-top: 3px;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 100%">
                                        <dxe:ASPxButton ID="btnConfirmaCoordenadas" runat="server" AutoPostBack="False" ClientInstanceName="btnConfirmaCoordenadas"
                                            Text="Salvar" Width="100px">
                                            <ClientSideEvents Click="function(s, e) 
{
    var codigoUnidade = hfGeral.Get(&quot;CodigoUnidade&quot;);
    var strCallback = codigoUnidade + ';' + txtLatitude.GetText() + ';' + txtLongitude.GetText();
    callback1.PerformCallback(strCallback);
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td align="right" style="width: 105px">
                                        <dxe:ASPxButton ID="btnFecharConfirmaCoordenadas" runat="server" AutoPostBack="False"
                                            ClientInstanceName="btnFecharConfirmaCoordenadas"
                                            Text="Fechar" Width="100px">
                                            <ClientSideEvents Click="function(s, e) 
{
	pcLatitudeLongitude.Hide();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxcb:ASPxCallback ID="callback1" runat="server" ClientInstanceName="callback1" OnCallback="callback1_Callback" >
                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Sucesso == 'S')
	{
		mostraDivSalvoPublicado(traducao.cadastroEntidades_coordenadas_salvas_com_sucesso);
		
		initialize();
	}
	else
	{
		var erro = s.cp_Erro;		
		
		mostraDivSalvoPublicado(traducao.cadastroEntidades_erro_ao_salvar_ + erro);
	}
}" />
                </dxcb:ASPxCallback>
            </td>
        </tr>
    </table>
</asp:Content>
