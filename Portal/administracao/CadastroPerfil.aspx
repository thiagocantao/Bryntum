<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="CadastroPerfil.aspx.cs" Inherits="administracao_CadastroPerfil"
    Title="Portal da Estratégia" %>


<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <script type="text/javascript" language="javascript" src="../scripts/barraNavegacao.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/CadastroPerfil.js"></script>

    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
        OnCallback="pnCallback_Callback" Width="100%">
        <PanelCollection>
            <dxp:PanelContent runat="server">
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoPerfil"
                    AutoGenerateColumns="False" Width="100%"
                    ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                    OnAfterPerformCallback="gvDados_AfterPerformCallback">
                    <ClientSideEvents CustomButtonClick="function(s, e) {
	//OnGridFocusedRowChanged(gvDados, true);
    //gvDados.SetFocusedRowIndex(e.visibleIndex);
    if(e.buttonID == 'btnNovo')
    {
        onClickBarraNavegacao('Incluir', gvDados, pcDados);
    }
    if(e.buttonID == 'btnEditar')
    {
        onClickBarraNavegacao('Editar', gvDados, pcDados);
		tcDados.SetActiveTab(tcDados.GetTab(0));
		btnSalvarPerfil.SetVisible(true);
		callbackGeral.PerformCallback(&quot;CerrarSession&quot;);
    }
    else if(e.buttonID == 'btnExcluir')
    {
        //onClickBarraNavegacao('Excluir', gvDados, pcDados);
        if (gvDados.GetVisibleRowsOnPage() &gt; 0)
        {
			s.GetRowValues(e.visibleIndex, 'StatusPerfil;', verificaAtivacao);				
        }
        else 
            window.top.mostraMensagem(&quot;O m&#233;todo \&quot;ExcluirRegistroSelecionado\&quot; n&#227;o foi implementado!&quot;, 'erro', true, false, null);
    }
    else if(e.buttonID == 'btnDetalheCustom')
    {	
        OnGridFocusedRowChanged(gvDados, true);
		tcDados.SetActiveTab(tcDados.GetTab(0));
        
		btnSalvar.SetVisible(false);
		btnSalvarPerfil.SetVisible(false);

		TipoOperacao = 'Consultar';        
		hfGeral.Set('TipoOperacao', TipoOperacao);
		callbackGeral.PerformCallback(&quot;CerrarSession&quot;);
        pcDados.Show();
    }
     else if ( e.buttonID == 'btnCopiaPerfil' )
	{
        
		e.processOnServer = true;
        trataClickBotaoCopiarPerfil(s);
	}	
}

"></ClientSideEvents>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem SummaryType="Count" FieldName="DescricaoPerfil_PT" DisplayFormat="Total de Perfis: {0}"></dxwgv:ASPxSummaryItem>
                    </TotalSummary>
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="180px" VisibleIndex="0">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnCopiaPerfil" Text="Obter as Permissões de Outro Perfil">
                                    <Image AlternateText="Obter as Permissões de Outro Perfil" Url="~/imagens/botoes/btnDuplicar.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Desativar">
                                    <Image Url="~/imagens/botoes/btnOff.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhes">
                                    <Image Url="~/imagens/botoes/pFormulario.png">
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
                                                                <Image Url="~/imagens/layout/saveLayout.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                <Image Url="~/imagens/layout/restoreLayout.png">
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
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoPerfil" Name="CodigoPerfil" Caption="CodigoPerfil"
                            Visible="False" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoAssociacao" Name="CodigoTipoAssociacao"
                            Caption="CodigoTipoAssociacao" Visible="False" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="IniciaisPerfil" Name="IniciaisPerfil" Caption="IniciaisPerfil"
                            Visible="False" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoAssociacao" GroupIndex="0"
                            SortIndex="0" SortOrder="Ascending" Name="DescricaoTipoAssociacao" Width="150px"
                            Caption="Tipo Associa&#231;&#227;o" VisibleIndex="4">
                            <Settings AllowDragDrop="False"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoPerfil_PT" Name="DescricaoPerfil_PT"
                            Caption="Descri&#231;&#227;o" VisibleIndex="5">
                            <Settings AllowDragDrop="False"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ObservacaoPerfil" Name="ObservacaoPerfil"
                            Caption="ObservacaoPerfil" Visible="False" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="StatusPerfil " Name="StatusPerfil " Caption="StatusPerfil "
                            Visible="False" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="IniciaisTipoAssociacao" Name="IniciaisTipoAssociacao"
                            Caption="IniciaisTipoAssociacao" Visible="False" VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>
                    <SettingsText></SettingsText>
                    <Templates>
                        <FooterRow>
                            <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td class="grid-legendas-cor grid-legendas-cor-controlado-sistema"><span></span></td>
                                        <td class="grid-legendas-label grid-legendas-label-controlado-sistema">
                                            <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                Text="<%# Resources.traducao.CadastroPerfil_controlados_pelo_sistema %>">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </FooterRow>
                    </Templates>
                </dxwgv:ASPxGridView>



            </dxp:PanelContent>
        </PanelCollection>
        <ClientSideEvents EndCallback="function(s, e) {
 
     if (s.cpStatusSalvar =='1')
    {
	                if('Incluir' == s.cp_OperacaoOk)
		{
                                               TipoOperacao = 'Editar';
                                               hfGeral.Set('TipoOperacao', TipoOperacao);
			pnCallbackPermissoes.PerformCallback(s.cp_TipoNovoPerfil);
    	                               window.top.mostraMensagem(traducao.CadastroPerfil_perfil_inclu_do_com_sucesso_, 'sucesso', false, false, null);
                                                lblCaptionPerfil.SetText(ddlTipoObjeto.GetText());
			ddlTipoObjeto.SetEnabled(false);
		}
	                else if('Editar' == s.cp_OperacaoOk)
		{
    	                                window.top.mostraMensagem(traducao.CadastroPerfil_perfil_alterado_com_sucesso_, 'sucesso', false, false, null);
			pnCallbackPermissoes.PerformCallback(s.cp_TipoNovoPerfil);
			lblCaptionPerfil.SetText(ddlTipoObjeto.GetText());
		}
	                else if('EditarPermissao' == s.cp_OperacaoOk)
		{
    	                                window.top.mostraMensagem(traducao.CadastroPerfil_permiss_o_alterado_com_sucesso_, 'sucesso', false, false, null);
			TipoOperacao = 'Editar';
			hfGeral.Set('TipoOperacao', TipoOperacao);
			pcDados.Hide();
		}
    	                callbackGeral.PerformCallback('CerrarSession');
                                //e.processOnServer = false;
	               gvPermissoes.PerformCallback(&quot;VOLTAR&quot;);
              
                              if (window.onClick_btnCancelar)
	              {
                                       onClick_btnCancelar();
		       tcDados.SetActiveTab(tcDados.GetTab(0));
                              }
     }
    else if (s.cpStatusSalvar =='0')
    {
                                mensagemErro = s.cpErroSalvar;
                                if (TipoOperacao == 'Excluir')
                                {
                                            // se existe um tratamento de erro especifico da opçao que está sendo executada
                                            if (window.trataMensagemErro)
                                           {
                                                                 mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
                                           }
                                            else // caso contrário, usa o tratamento padrão
                                           {
                                                            // se for erro de Chave Estrangeira (FK)
                                                           if (mensagemErro.indexOf('REFERENCE') &gt;= 0 )
                                                           {
                                                                       mensagemErro = traducao.CadastroPerfil_o_registro_n_o_pode_ser_exclu_do_pois_est__sendo_utilizado_por_outro;
                                                           }
                                           }
                                   }
                                   window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
		   if('EditarPermissao' == s.cp_OperacaoOk)
			TipoOperacao = 'Editar';
    }
}"></ClientSideEvents>
    </dxcp:ASPxCallbackPanel>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
        HeaderText="Detalhe" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowCloseButton="False" Width="940px" ID="pcDados" PopupVerticalOffset="10">
        <ClientSideEvents PopUp="function(s, e) {
     var largura = Math.max(0, document.documentElement.clientWidth) - 100;
     s.SetWidth(largura);
     s.UpdatePosition();
}" />
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
                                <dxtc:ASPxPageControl runat="server" ActiveTabIndex="1" ClientInstanceName="tcDados"
                                    Width="100%" ID="tcDados">
                                    <TabPages>
                                        <dxtc:TabPage Name="tabI" Text="Identifica&#231;&#227;o">
                                            <ContentCollection>
                                                <dxw:ContentControl runat="server">
                                                    <div id="divIdentificacao">
                                                        <table id="tbIdentificacao" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Tipo de objeto:" ClientInstanceName="lblGerente"
                                                                            ID="lblGerente">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="DescricaoTipoAssociacao"
                                                                            ValueField="CodigoTipoAssociacao" TextFormatString="{1}" Width="100%" ClientInstanceName="ddlTipoObjeto"
                                                                            ForeColor="Black" ID="ddlTipoObjeto">
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetText() != '')
		lblCaptionPerfil.SetText(s.GetText());
}"></ClientSideEvents>
                                                                            <Columns>
                                                                                <dxe:ListBoxColumn FieldName="NivelHierarquicoFilho" Width="10px" Caption="Nivel">
                                                                                </dxe:ListBoxColumn>
                                                                                <dxe:ListBoxColumn FieldName="DescricaoTipoAssociacao" Caption="Nome"></dxe:ListBoxColumn>
                                                                            </Columns>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-top: 5px">&nbsp;<dxe:ASPxLabel runat="server" Text="Nome Perfil:" ClientInstanceName="lblNome"
                                                                        ID="lblNome">
                                                                    </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtNomePerfil"
                                                                            ID="txtNomePerfil">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-top: 5px">
                                                                        <dxe:ASPxLabel runat="server" Text="Observa&#231;&#227;o:" ClientInstanceName="lblObservacao"
                                                                            ID="lblObservacao">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo runat="server" Height="195px" Width="100%" ClientInstanceName="mmObservacao"
                                                                            ID="mmObservacao">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxMemo>
                                                                        <dxe:ASPxLabel ID="lblContadorMemoObservacao" runat="server" ClientInstanceName="lblContadorMemoObservacao"
                                                                            Font-Bold="True" ForeColor="#999999">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </dxw:ContentControl>
                                            </ContentCollection>
                                        </dxtc:TabPage>
                                        <dxtc:TabPage Name="tabP" Text="Permiss&#245;es">
                                            <ContentCollection>
                                                <dxw:ContentControl runat="server">
                                                    <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackPermissoes"
                                                        Width="100%" ID="pnCallbackPermissoes" OnCallback="pnCallbackPermissoes_Callback">
                                                        <ClientSideEvents Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 275;
       s.SetHeight(sHeight);
}" />
                                                        <PanelCollection>
                                                            <dxp:PanelContent runat="server">
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="padding-right: 5px; width: 180px" valign="top">
                                                                                <div id="divNbAssociacao" style="overflow:auto">
                                                                                    <dxnb:ASPxNavBar runat="server" AllowSelectItem="True" ClientInstanceName="nbAssociacao" EnableClientSideAPI="true"
                                                                                        Width="100%" ID="nbAssociacao">
                                                                                        <ClientSideEvents ItemClick="function(s, e) {
	onClick_MenuPermissao(s, e);
}" Init="function(s, e) {
       var sHeight = Math.max(0, document.documentElement.clientHeight) - 275;
       document.getElementById('divNbAssociacao').style.height = sHeight + 'px';
}"></ClientSideEvents>
                                                                                        <Groups>
                                                                                            <dxnb:NavBarGroup Name="gpAssociacao" Text="Tipo Associac&#227;o">
                                                                                            </dxnb:NavBarGroup>
                                                                                        </Groups>
                                                                                        <Paddings PaddingTop="0px"></Paddings>
                                                                                        <ItemStyle Wrap="True"></ItemStyle>
                                                                                    </dxnb:ASPxNavBar>
                                                                                </div>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvPermissoes" KeyFieldName="CodigoPermissao"
                                                                                    AutoGenerateColumns="False" Width="100%"
                                                                                    ID="gvPermissoes" OnHtmlRowPrepared="gvPermissoes_HtmlRowPrepared" OnHtmlRowCreated="gvPermissoes_HtmlRowCreated"
                                                                                    OnCustomCallback="gvPermissoes_CustomCallback" OnDataBinding="gvPermissoes_DataBinding">
                                                                                    <Columns>
                                                                                        <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                            Width="40px">
                                                                                        </dxwgv:GridViewCommandColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoItemPermissao" GroupIndex="0" SortIndex="0"
                                                                                            SortOrder="Ascending" Name="DescricaoItemPermissao" Caption=" " VisibleIndex="1">
                                                                                            <Settings AllowDragDrop="False" AllowGroup="True"></Settings>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoAcaoPermissao" ReadOnly="True"
                                                                                            Name="DescricaoAcaoPermissao" Caption="Permiss&#245;es" VisibleIndex="2" Width="250px">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataCheckColumn FieldName="Concedido" Name="Concedido" Width="75px"
                                                                                            Caption="Conceder" VisibleIndex="3">
                                                                                            <PropertiesCheckEdit ClientInstanceName="chkConceder">
                                                                                            </PropertiesCheckEdit>
                                                                                            <Settings AllowAutoFilter="False" />
                                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                                            <DataItemTemplate>
                                                                                                <%# getCheckBox("CheckConcedido", "Concedido", "C")%>
                                                                                            </DataItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                                        <dxwgv:GridViewDataCheckColumn FieldName="Delegavel" Name="Delegavel" Width="80px"
                                                                                            Caption="Extens&#237;vel" VisibleIndex="4">
                                                                                            <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextUnchecked="N&#227;o" ClientInstanceName="chkExtensivel">
                                                                                            </PropertiesCheckEdit>
                                                                                            <Settings AllowAutoFilter="False" />
                                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                                            <DataItemTemplate>
                                                                                                <%# getCheckBox("CheckDelegavel", "Delegavel", "D")%>
                                                                                            </DataItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                                        <dxwgv:GridViewDataCheckColumn FieldName="Negado" Name="Negado" Width="70px" Caption="Negar"
                                                                                            VisibleIndex="5">
                                                                                            <PropertiesCheckEdit ClientInstanceName="chkNegar">
                                                                                            </PropertiesCheckEdit>
                                                                                            <Settings AllowAutoFilter="False" />
                                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                                            <DataItemTemplate>
                                                                                                <%# getCheckBox("CheckNegado", "Negado", "N")%>
                                                                                            </DataItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                                        <dxwgv:GridViewDataCheckColumn FieldName="Incondicional" Name="Incondicional" Width="75px"
                                                                                            Caption="Incondicional" VisibleIndex="7">
                                                                                            <PropertiesCheckEdit ClientInstanceName="chkIncondicional" EnableClientSideAPI="True">
                                                                                            </PropertiesCheckEdit>
                                                                                            <Settings AllowAutoFilter="False" />
                                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                                            <DataItemTemplate>
                                                                                                <%# getCheckBox("CheckIncondicional", "Incondicional", "I")%>
                                                                                            </DataItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="Herdada" Caption="Herdada" Visible="False"
                                                                                            VisibleIndex="9">
                                                                                            <PropertiesTextEdit ClientInstanceName="txtHerdada">
                                                                                            </PropertiesTextEdit>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="FlagsOrigem" Caption="FlagsOrigem" Visible="False"
                                                                                            VisibleIndex="10">
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="Outorgavel" Name="Outorgavel" Caption="Outorgavel"
                                                                                            Visible="False" VisibleIndex="8">
                                                                                            <Settings AllowAutoFilter="False" />
                                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoPermissao" Name="CodigoPermissao"
                                                                                            Caption="CodigoPermissao" Visible="False" VisibleIndex="6">
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True"></SettingsBehavior>
                                                                                    <ClientSideEvents Init="function(s, e) {
		var sHeight = Math.max(0, document.documentElement.clientHeight) - 275;
       s.SetHeight(sHeight);
}" />
                                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="Inline" EditFormColumnCount="4">
                                                                                    </SettingsEditing>
                                                                                    <SettingsPopup>
                                                                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                            AllowResize="True" />
                                                                                        <HeaderFilter MinHeight="140px">
                                                                                        </HeaderFilter>
                                                                                    </SettingsPopup>
                                                                                    <Settings ShowTitlePanel="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="295"></Settings>
                                                                                    <SettingsLoadingPanel Mode="Disabled"></SettingsLoadingPanel>
                                                                                </dxwgv:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </dxcp:ASPxCallbackPanel>
                                                </dxw:ContentControl>
                                            </ContentCollection>
                                        </dxtc:TabPage>
                                    </TabPages>
                                    <ClientSideEvents ActiveTabChanging="function(s, e) {
    
	e.cancel = podeMudarAba(s, e);
	if (!e.cancel){
        var textoItem = getIniciaisAssociacaoFromTextoMenu(nbAssociacao.GetSelectedItem(0,0).name);
        CallBackGvPermissoes(textoItem);
	}
	
}"></ClientSideEvents>
                                </dxtc:ASPxPageControl>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 59px" align="right">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="padding-left: 10px; padding-bottom: 3px; padding-top: 3px" align="left">&nbsp;<dxe:ASPxLabel runat="server" Text="Perfil de tipo " ClientInstanceName="lblCaptionPerfil"
                                                Font-Bold="False" ForeColor="#404040" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblCaptionPerfil" Font-Bold="True"
                                                    ForeColor="#404040" ID="lblCaptionPerfil">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 100%"></td>
                                        <td>
                                            <dxe:ASPxButton ID="btnSalvarPerfil" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarPerfil"
                                                Text="Salvar" Width="100px">
                                                <ClientSideEvents Click="function(s, e) {
                                                                            debugger
    var mensagemErro = verificarDadosPreenchidos(); 
    alert(mensagemErro);
    if(mensagemErro == '')
    {
	   if (window.SalvarCamposFormulario)
    	{
        	if (!SalvarCamposFormulario())
	        {
    	        //pcDados.Hide();
        	    habilitaModoEdicaoBarraNavegacao(false, gvDados);
				ddlTipoObjeto.SetEnabled(false);
            	return true;
	        }
	    }
    	else
	    {
    	    window.top.mostraMensagem('O método não foi implementado!', 'erro', true, false, null);
	    }
    }
    else
    {
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
        e.processOnServer = false;
    }
}




" />
                                                <ClientSideEvents Click="function(s, e) {
    var mensagemErro = verificarDadosPreenchidos(); 
    if(mensagemErro == &#39;&#39;)
    {
	   if (window.SalvarCamposFormulario)
    	{
        	if (!SalvarCamposFormulario())
	        {
				ddlTipoObjeto.SetEnabled(false);
            	return true;
	        }
	    }
    	else
	    {
    	    window.top.mostraMensagem(traducao.CadastroPerfil_o_m_todo_n_o_foi_implementado_, 'erro', true, false, null);
	    }
    }
    else
    {
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
        e.processOnServer = false;
    }
}




"></ClientSideEvents>
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dxe:ASPxButton ID="btnCancelar" runat="server" CommandArgument="btnCancelar"
                                                Text="Fechar" Width="100px">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	gvPermissoes.PerformCallback(&quot;VOLTAR&quot;);

    if (window.onClick_btnCancelar)
	{
        onClick_btnCancelar();
		tcDados.SetActiveTab(tcDados.GetTab(0));
    }

    //callbackGeral.PerformCallback(&quot;CerrarSession&quot;);
	//callbackGeral.PerformCallback(&quot;VOLTAR&quot;);
}" />
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	gvPermissoes.PerformCallback(&quot;VOLTAR&quot;);

    if (window.onClick_btnCancelar)
	{
        onClick_btnCancelar();
		tcDados.SetActiveTab(tcDados.GetTab(0));
    }

    //callbackGeral.PerformCallback(&quot;CerrarSession&quot;);
	//callbackGeral.PerformCallback(&quot;VOLTAR&quot;);
}"></ClientSideEvents>
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxtv:ASPxPopupControl ID="pcCopiaPermissoes" runat="server" AllowDragging="True"
        AllowResize="True" ClientInstanceName="pcCopiaPermissoes"
        HeaderText="Copia Perfil" Modal="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="721px">
        <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = true;
	pcCopiaPermissoes_OnPopup(s,e);
}"
            Shown="function(s, e) {
 }" />
        <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = true;
	pcCopiaPermissoes_OnPopup(s,e);
}"
            Shown="function(s, e) {
 }"></ClientSideEvents>
        <ContentCollection>
            <dxtv:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <table>
                    <tbody>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%">
                                <dxe:ASPxLabel ID="lblPerfilAlvo" runat="server" ClientInstanceName="lblPerfilAlvo"
                                    Font-Bold="True" Width="100%" CssClass="titleCopiarPerfil">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%">
                                <dxe:ASPxLabel ID="lblCopia" runat="server" ClientInstanceName="lblCopia" CssClass="titleCopiarPerfilDanger"
                                    Text="IMPORTANTE: O perfil alvo inicialmente perde todas as permissões que tem, e em seguida recebe as permissões do perfil origem selecionado abaixo."
                                    Font-Bold="True" Width="100%" ForeColor="#CC3300">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxtv:ASPxRoundPanel ID="rpPerfilOrigem" runat="server"
                                    HeaderText="Selecione o perfil de origem, o qual irá fornecer o conjunto de permissões para  a cópia:"
                                    View="GroupBox" Width="100%" ClientInstanceName="rpPerfilOrigem" HorizontalAlign="Center" ContentHeight="15px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PanelCollection>
                                        <dxtv:PanelContent ID="PanelContent1" runat="server">

                                            <dxtv:ASPxCallbackPanel ID="callbackPerfilOrigem" ClientInstanceName="callbackPerfilOrigem" runat="server" Width="100%" OnCallback="callbackPerfilOrigem_Callback">
                                                <PanelCollection>
                                                    <dxtv:PanelContent runat="server">
                                                        <dxtv:ASPxComboBox ID="ddlPerfilOrigem" runat="server" ClientInstanceName="ddlPerfilOrigem"
                                                            TextFormatString="{0}" ValueType="System.Int32" Width="100%">
                                                            <Columns>
                                                                <dxtv:ListBoxColumn Caption="Código" FieldName="codigo" Width="100px" Visible="False" />
                                                                <dxtv:ListBoxColumn Caption="Perfil" FieldName="descricao" />
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
                            <td align="right" style="padding-top: 10px">
                                <table id="Table3" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr style="height: 35px">
                                            <td>
                                                <dxtv:ASPxButton ID="btnSalvarCopiaPermissoes" runat="server" AutoPostBack="False"
                                                    CausesValidation="False" ClientInstanceName="btnSalvarCopiaPermissoes"
                                                    Text="Salvar" UseSubmitBehavior="False" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvarCopiaPermissoes)
		onClick_btnSalvarCopiaPermissoes();
}" />
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
}" />
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
                <dxtv:ASPxHiddenField ID="hfStatusCopiaPerfil" runat="server" ClientInstanceName="hfStatusCopiaPerfil"
                    OnCustomCallback="hfStatusCopiaPerfil_CustomCallback">
                    <ClientSideEvents EndCallback="function(s, e) {
	hfStatusCopiaPerfil_onEndCallback(s,e);
}"></ClientSideEvents>
                </dxtv:ASPxHiddenField>
            </dxtv:PopupControlContentControl>
        </ContentCollection>
    </dxtv:ASPxPopupControl>
    <asp:SqlDataSource ID="dsPerfilOrigem" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sds" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackGeral" ID="callbackGeral"
        OnCallback="callbackGeral_Callback">
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackConceder" ID="callbackConceder"
        OnCallback="callbackConceder_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	gvPermissoes.PerformCallback('ATL');
}"></ClientSideEvents>
    </dxcb:ASPxCallback>
    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados">
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
    <script type="text/javascript">
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 275;
document.getElementById("divIdentificacao").style.height = sHeight + 'px';
    </script>
</asp:Content>
