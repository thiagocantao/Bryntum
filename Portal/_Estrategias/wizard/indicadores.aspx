<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="indicadores.aspx.cs" Inherits="_Estrategias_wizard_indicadores" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript">
        var pastaImagens = "../../imagens";
    </script>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); width: 100%">
            <tr style="height: 26px">
                <td style="height: 26px" valign="middle">&nbsp;
                    <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                        Font-Overline="False" Font-Strikeout="False" Text="Indicadores"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="padding-right: 10px; padding-left: 10px; padding-top: 10px">
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoIndicador"
                                                    AutoGenerateColumns="False" Width="100%"
                                                    ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                                    OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomCallback="gvDados_CustomCallback">
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                                        CustomButtonClick="function(s, e) 
{
	e.processOnServer = false;
	gvDados.SetFocusedRowIndex(e.visibleIndex);
	TipoOperacao = '';

    if(e.buttonID == 'btnExcluir')
    {
		TipoOperacao = 'Excluir';
		onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
    }
	else if(e.buttonID == 'btnResponsavel')
	{
       s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
	   s.GetSelectedFieldValues('NomeUsuarioResponsavel;CodigoIndicador;CodigoReservado;NomeIndicador;CodigoResponsavelAtualizacaoIndicador;NomeUsuarioResponsavelResultado;CodigoResponsavel', alteraResponsavelCombo);
		
	}
	else if(e.buttonID == 'btnCompartilhar')
	{
		//hfUnidades.Clear();
	    //OnGridFocusedRowChangedCompartilhamento(gvDados, true);
		//pcCompartilhar.Show();
        if(s.GetRowKey(e.visibleIndex) != null)
            abreCompartilhamentoIndicador(s.GetRowKey(e.visibleIndex));
	}
	else if(e.buttonID == 'btnPermissoesCustom')
	{
	    OnGridFocusedRowChangedPopup(gvDados);
	}
	else
	{
    	if(e.buttonID == 'btnDetalhesCustom')
	    {	
    	    TipoOperacao = 'Consultar';
			hfGeral.Set('TipoOperacao', TipoOperacao);
			btnSalvar.SetVisible(false);

			OnGridFocusedRowChanged(gvDados, true);
			desabilitaHabilitaComponentes();
			pcDados.Show();
	    }
	    else
		{
			if(e.buttonID == 'btnNovo')
			{
	    		TipoOperacao = 'Incluir';
		    	hfGeral.Set('TipoOperacao', TipoOperacao);
	    	}
	    	else if(e.buttonID == 'btnEditar')
		    {
    		    TipoOperacao = 'Editar';
        		hfGeral.Set('TipoOperacao', TipoOperacao);
	    	}

			onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
			desabilitaHabilitaComponentes();
		}
	}
}
"></ClientSideEvents>
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="195px" VisibleIndex="0">
                                                            <CustomButtons>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                                    <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnCompartilhar" Text="Compartilhar">
                                                                    <Image Url="~/imagens/compartilhar.PNG">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnResponsavel" Text="Editar Responsável">
                                                                    <Image Url="~/imagens/TrocaResponsavel.png">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnPermissoesCustom" Text="Alterar Permissões">
                                                                    <Image Url="~/imagens/Perfis/Perfil_Permissoes.png">
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
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Name="CodigoIndicador"
                                                            Visible="False" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Caption="Nome Indicador"
                                                            VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" />
                                                            <Settings AllowHeaderFilter="False"></Settings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" ShowInCustomizationForm="True"
                                                            Name="NomeUsuarioResponsavel" Width="200px" Caption="Respons&#225;vel pelo Indicador"
                                                            VisibleIndex="12">
                                                            <Settings AllowHeaderFilter="False"></Settings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavelResultado" ShowInCustomizationForm="True"
                                                            Name="NomeUsuarioResponsavelResultado" Width="200px" Caption="Respons&#225;vel pela Atualização"
                                                            VisibleIndex="13">
                                                            <Settings AllowHeaderFilter="False"></Settings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaUnidadeCriadoraIndicador" Name="IndicaUnidadeCriadoraIndicador"
                                                            Width="100px" Caption="Compartilhado" Visible="False" VisibleIndex="25">
                                                            <DataItemTemplate>
                                                                <%# (Eval("IndicaUnidadeCriadoraIndicador").ToString() == "S") ? "Sim" : "Não"%>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUnidadeNegocio" Width="250px" Caption="Nome Unidade Negócio"
                                                            VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False"></Settings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeMedida" Visible="False" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="GlossarioIndicador" Visible="False" VisibleIndex="6">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False" VisibleIndex="7">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Polaridade" Visible="False" VisibleIndex="2">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="FormulaIndicador" Visible="False" VisibleIndex="8">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaIndicadorCompartilhado" Visible="False"
                                                            VisibleIndex="13">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoPeriodicidadeCalculo" Visible="False"
                                                            VisibleIndex="9">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="FonteIndicador" Visible="False" VisibleIndex="14">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavel" Name="CodigoResponsavel"
                                                            Visible="False" VisibleIndex="15">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="FormulaFormatoCliente" Visible="False" VisibleIndex="16">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="IndicadorResultante" Visible="False" VisibleIndex="10">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoFuncaoAgrupamentoMeta" Name="CodigoFuncaoAgrupamentoMeta"
                                                            Visible="False" VisibleIndex="11">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaCriterio" Visible="False" VisibleIndex="26">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="LimiteAlertaEdicaoIndicador" Visible="False"
                                                            VisibleIndex="27">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoReservado" Caption="C&#243;digo Reservado"
                                                            Visible="False" VisibleIndex="28">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="PossuiMetaResultado" Caption="PossuiMetaResultado"
                                                            Visible="False" VisibleIndex="29">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Mapa Estratégico" FieldName="MapaEstrategico"
                                                            GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending"
                                                            VisibleIndex="17">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavelResultado" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="24">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavelAtualizacaoIndicador" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="23">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="22">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="PodeCompartilhar" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="21">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="DataInicioValidadeMeta" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="18">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="DataTerminoValidadeMeta" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="19">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaAcompanhamentoMetaVigencia" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="20">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True" AutoExpandAllGroups="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Settings ShowFooter="True" VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                                        ShowGroupPanel="True" ShowHeaderFilterBlankItems="False" ShowHeaderFilterButton="True"></Settings>
                                                    <SettingsText></SettingsText>
                                                    <Templates>
                                                        <FooterRow>
                                                            <table>
                                                                <tr>
                                                                    <td style="border: 1px solid #808080; width: 10px; background-color: #ddffcc">&nbsp;
                                                                    </td>
                                                                    <td style="padding-left: 3px; padding-right: 10px;">
                                                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                                            Font-Bold="False" Text="Indicador criado em outra entidade">
                                                                        </dxe:ASPxLabel>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterRow>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcResponsavel2"
                                                    CloseAction="None" HeaderText="Novo Respons&#225;vel" Modal="True" PopupHorizontalAlign="WindowCenter"
                                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="600px" Font-Bold="True"
                                                    ID="pcResponsavel">
                                                    <ClientSideEvents Closing="function(s, e) {
	ddlResponsavel.SetText('');
	txtCodigoReservadoNovoResp.SetText('');
}"></ClientSideEvents>
                                                    <ContentStyle>
                                                        <Paddings Padding="5px"></Paddings>
                                                    </ContentStyle>
                                                    <ContentCollection>
                                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Responsável pelo Indicador:" ClientInstanceName="lblResponsavel"
                                                                                Font-Bold="False" ID="lblResponsavel">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackResponsavel"
                                                                                Width="100%" ID="pnCallbackResponsavel" OnCallback="pnCallbackResponsavel_Callback">
                                                                                <ClientSideEvents EndCallback="function(s, e) {
        if(s.cp_sucesso != '')
       {
               pcResponsavel2.Hide();
                window.top.mostraMensagem(s.cp_sucesso, 'sucesso', false, false, null);
                gvDados.PerformCallback();
       }
       else if(s.cp_erro != '')
      {
                window.top.mostraMensagem(s.cp_erro , 'erro', true, false, null);
      }
}"></ClientSideEvents>
                                                                                <PanelCollection>
                                                                                    <dxp:PanelContent runat="server">
                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.Int32"
                                                                                                            TextFormatString="{0}" Width="100%" ClientInstanceName="ddlResponsavel" Font-Bold="False"
                                                                                                            ID="ddlResponsavel" DropDownStyle="DropDown"
                                                                                                            EnableCallbackMode="True" OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                                                                                            OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition">
                                                                                                            <Columns>
                                                                                                                <dxe:ListBoxColumn FieldName="NomeUsuario" Width="300px" Caption="Nome"></dxe:ListBoxColumn>
                                                                                                                <dxe:ListBoxColumn FieldName="EMail" Width="200px" Caption="Email"></dxe:ListBoxColumn>
                                                                                                            </Columns>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="height: 10px"></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxLabel ID="lblResponsavelIndicador1" runat="server"
                                                                                                            Text="Responsável pela Atualização do Resultado:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxComboBox ID="ddlResponsavelResultados2" runat="server" ClientInstanceName="ddlResponsavelResultados2"
                                                                                                            DropDownStyle="DropDown" EnableCallbackMode="True" Font-Bold="False"
                                                                                                            IncrementalFilteringMode="Contains" OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                                                                                            OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                                                                                            TextFormatString="{0}" ValueType="System.Int32" Width="100%">
                                                                                                            <Columns>
                                                                                                                <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="300px" />
                                                                                                                <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="200px" />
                                                                                                            </Columns>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="height: 10px">&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="height: 10px">
                                                                                                        <dxe:ASPxLabel ID="lblCodigoResponsavelNovoResp" runat="server" ClientInstanceName="lblCodigoResponsavelNovoResp"
                                                                                                            Font-Bold="False" Text="Código Reservado:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" ClientInstanceName="lblAjuda" Font-Bold="True"
                                                                                                            Font-Italic="False" ForeColor="Black" Text="*">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxTextBox ID="txtCodigoReservadoNovoResp" runat="server" ClientInstanceName="txtCodigoReservadoNovoResp"
                                                                                                            Width="100%" MaxLength="256">
                                                                                                        </dxe:ASPxTextBox>
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
                                                                        <td style="padding-bottom: 4px; padding-top: 4px">
                                                                            <dxe:ASPxLabel runat="server" Text="(*) - C&#243;digo utilizado para interface com outros sistemas da institui&#231;&#227;o."
                                                                                ClientInstanceName="lblAjuda" Font-Bold="False" Font-Italic="False"
                                                                                Font-Size="7pt" ForeColor="#404040" ID="lblAjuda">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 90px">
                                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvarResponsavel" Text="Salvar"
                                                                                                Width="100%" ID="btnSalvarResponsavel">
                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pnCallbackResponsavel.PerformCallback('salvar');
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px" />
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td style="width: 90px">
                                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnFecharResponsavel" Text="Fechar"
                                                                                                Width="100%" ID="btnFecharResponsavel">
                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcResponsavel2.Hide();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px" />
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
                                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                                    CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="730px"
                                                    ID="pcDados">
                                                    <ClientSideEvents Closing="function(s, e) {
	LimpaCamposFormulario();	
	TabControl.SetActiveTab(TabControl.GetTabByName('TabDetalhe'));
	ddlResponsavelIndicador.SetValue(null);
	ddlResponsavelIndicador.SetText(&quot;&quot;);	
	ddlResponsavelIndicador.PerformCallback();
}"
                                                        Init="function(s, e) {
	TabControl.ActiveTabIndex = 0;
}"
                                                        CloseUp="function(s, e) {
	gridDadosIndicador.PerformCallback(&quot;Incluir&quot;);
}"></ClientSideEvents>
                                                    <ContentStyle>
                                                        <Paddings Padding="5px" PaddingTop="1px"></Paddings>
                                                    </ContentStyle>
                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                    <ContentCollection>
                                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxtc:ASPxPageControl runat="server" ActiveTabIndex="2" ClientInstanceName="TabControl"
                                                                                Width="710px" ID="TabControl">
                                                                                <TabPages>
                                                                                    <dxtc:TabPage Name="TabDetalhe" Text="Detalhes">
                                                                                        <ContentCollection>
                                                                                            <dxw:ContentControl runat="server">
                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 156px">
                                                                                                                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" ClientInstanceName="lblIndicador" Text="Código Reservado:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                            <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" ClientInstanceName="lblAjuda" Font-Bold="True" Font-Italic="False" ForeColor="Black" Text="*">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 400px">
                                                                                                                            <dxtv:ASPxLabel ID="lblIndicador" runat="server" ClientInstanceName="lblIndicador" Text="Indicador:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td>
                                                                                                                            <dxtv:ASPxLabel ID="lblAgrupamentoDaMeta" runat="server" ClientInstanceName="lblAgrupamentoDaMeta" Text=" Agrupamento da Meta:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 156px">
                                                                                                                            <dxtv:ASPxTextBox ID="txtCodigoReservado" runat="server" ClientInstanceName="txtCodigoReservado" MaxLength="256" Width="95%">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxTextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 400px">
                                                                                                                            <dxtv:ASPxTextBox ID="txtIndicador" runat="server" ClientInstanceName="txtIndicador" MaxLength="99" Width="100%">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxTextBox>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td>
                                                                                                                            <dxtv:ASPxComboBox ID="cmbAgrupamentoDaMeta" runat="server" ClientInstanceName="cmbAgrupamentoDaMeta" ValueType="System.Int32" Width="100%">
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
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 120px">
                                                                                                                            <dxtv:ASPxLabel ID="lblUnidadeMedida" runat="server" Text="Unidade de Medida:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td style="width: 120px">
                                                                                                                            <dxtv:ASPxLabel ID="lblCasasDecimais" runat="server" Text="Casas Decimais:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td style="width: 205px">
                                                                                                                            <dxtv:ASPxLabel ID="lblPolaridade" runat="server" Text="Polaridade:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td style="width: 112px">
                                                                                                                            <dxtv:ASPxLabel ID="lblPeriodicidade" runat="server" Text="Periodicidade:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 120px">
                                                                                                                            <dxtv:ASPxComboBox ID="ddlUnidadeMedida" runat="server" ClientInstanceName="ddlUnidadeMedida" ValueType="System.Int32" Width="100%">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxComboBox>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td style="width: 120px">
                                                                                                                            <dxtv:ASPxComboBox ID="ddlCasasDecimais" runat="server" ClientInstanceName="ddlCasasDecimais" SelectedIndex="0" ValueType="System.Int32" Width="100%">
                                                                                                                                <Items>
                                                                                                                                    <dxtv:ListEditItem Selected="True" Text="0 (Zero)" Value="0" />
                                                                                                                                    <dxtv:ListEditItem Text="1 (Um)" Value="1" />
                                                                                                                                    <dxtv:ListEditItem Text="2 (Duas)" Value="2" />
                                                                                                                                    <dxtv:ListEditItem Text="3 (Três)" Value="3" />
                                                                                                                                    <dxtv:ListEditItem Text="4 (Quatro)" Value="4" />
                                                                                                                                    <dxtv:ListEditItem Text="5 (Cinco)" Value="5" />
                                                                                                                                    <dxtv:ListEditItem Text="6 (Seis)" Value="6" />
                                                                                                                                </Items>
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxComboBox>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td>
                                                                                                                            <dxtv:ASPxComboBox ID="ddlPolaridade" runat="server" ClientInstanceName="ddlPolaridade" SelectedIndex="0" Width="100%">
                                                                                                                                <Items>
                                                                                                                                    <dxtv:ListEditItem Selected="True" Text="Quanto maior o valor, MELHOR" Value="POS" />
                                                                                                                                    <dxtv:ListEditItem Text="Quanto maior o valor, PIOR" Value="NEG" />
                                                                                                                                </Items>
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxComboBox>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td style="width: 112px">
                                                                                                                            <dxtv:ASPxComboBox ID="ddlPeriodicidade" runat="server" ClientInstanceName="ddlPeriodicidade" ValueType="System.Int32" Width="100%">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxComboBox>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td>
                                                                                                                            <dxtv:ASPxCheckBox ID="cbCheckResultante" runat="server" CheckState="Unchecked" ClientInstanceName="cbCheckResultante" Text="Resultante?">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxCheckBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 10px">
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <dxtv:ASPxLabel ID="lblFonte" runat="server" Text="Fonte:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10px;"></td>
                                                                                                                        <td style="width: 160px;">
                                                                                                                            <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" Text="Critério:">
                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <dxtv:ASPxTextBox ID="txtFonte" runat="server" ClientInstanceName="txtFonte" MaxLength="59" Width="100%">
                                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxTextBox>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td style="width: 160px">
                                                                                                                            <dxtv:ASPxRadioButtonList ID="rbCriterio" runat="server" ClientInstanceName="rbCriterio" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%">
                                                                                                                                <Paddings Padding="0px" />
                                                                                                                                <Items>
                                                                                                                                    <dxtv:ListEditItem Selected="True" Text="Status" Value="S" />
                                                                                                                                    <dxtv:ListEditItem Text="Acumulado" Value="A" />
                                                                                                                                </Items>
                                                                                                                                <DisabledStyle ForeColor="Black">
                                                                                                                                </DisabledStyle>
                                                                                                                            </dxtv:ASPxRadioButtonList>
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
                                                                                                        <td>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxtv:ASPxLabel ID="lblUnidade" runat="server" ClientInstanceName="lblUnidade" Text="Unidade de Negócio:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="width: 210px">
                                                                                                                        <dxtv:ASPxLabel ID="lblResponsavelIndicador" runat="server" Text="Responsável pelo Indicador:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="width: 244px">
                                                                                                                        <dxtv:ASPxLabel ID="lblResponsavelIndicador0" runat="server" Text="Responsável Atualização do Resultado:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxtv:ASPxComboBox ID="ddlUnidadeNegocio" runat="server" ClientInstanceName="ddlUnidadeNegocio" TextFormatString="{1}" ValueType="System.Int32" Width="100%">
                                                                                                                            <Columns>
                                                                                                                                <dxtv:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio" Width="140px" />
                                                                                                                                <dxtv:ListBoxColumn Caption="Nome" FieldName="NomeUnidadeNegocio" Width="300px" />
                                                                                                                            </Columns>
                                                                                                                            <SettingsLoadingPanel Text="Carregando;" />
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-right: 10px; width: 210px;">
                                                                                                                        <dxtv:ASPxComboBox ID="ddlResponsavelIndicador" runat="server" ClientInstanceName="ddlResponsavelIndicador" EnableCallbackMode="True" OnItemRequestedByValue="ddlResponsavelIndicador_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavelIndicador_ItemsRequestedByFilterCondition" ValueType="System.Int32" Width="100%">
                                                                                                                            <Columns>
                                                                                                                                <dxtv:ListBoxColumn Caption="Nome" Width="300px" />
                                                                                                                                <dxtv:ListBoxColumn Caption="Email" Width="200px" />
                                                                                                                            </Columns>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 244px">
                                                                                                                        <dxtv:ASPxComboBox ID="ddlResponsavelResultado" runat="server" ClientInstanceName="ddlResponsavelResultado" EnableCallbackMode="True" OnItemRequestedByValue="ddlResponsavelIndicador_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavelIndicador_ItemsRequestedByFilterCondition" ValueType="System.Int32" Width="100%">
                                                                                                                            <Columns>
                                                                                                                                <dxtv:ListBoxColumn Caption="Nome" Width="300px" />
                                                                                                                                <dxtv:ListBoxColumn Caption="Email" Width="200px" />
                                                                                                                            </Columns>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 10px">&nbsp; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td style="width: 125px">
                                                                                                                        <dxtv:ASPxLabel ID="lblInicioVigencia" runat="server" Text="Início Vigência:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="width: 125px">
                                                                                                                        <dxtv:ASPxLabel ID="lblInicioVigencia0" runat="server" Text="Término Vigência:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>&nbsp; </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 125px; padding-right: 10px">
                                                                                                                        <dxtv:ASPxDateEdit ID="ddlInicioVigencia" runat="server" ClientInstanceName="ddlInicioVigencia" DisplayFormatString="{0:dd/MM/yyyy}" Width="100%">
                                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	verificaVigencia();
}" />
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxDateEdit>
                                                                                                                    </td>
                                                                                                                    <td style="width: 125px; padding-right: 10px">
                                                                                                                        <dxtv:ASPxDateEdit ID="ddlTerminoVigencia" runat="server" ClientInstanceName="ddlTerminoVigencia" DisplayFormatString="{0:dd/MM/yyyy}" Width="100%">
                                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	verificaVigencia();
}" />
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxDateEdit>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxtv:ASPxCheckBox ID="cbVigencia" runat="server" CheckState="Unchecked" ClientInstanceName="cbVigencia" Text="Acompanhar as metas do indicador somente no período de vigência">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 10px">&nbsp; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td id="Td1">
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <fieldset style="clip: rect(auto auto auto auto)">
                                                                                                                                <legend style="color: black">O Indicador deverá ser atualizado em quantos dias após o fechamento do período?</legend>
                                                                                                                                <dxtv:ASPxTextBox ID="txtLimite" runat="server" ClientInstanceName="txtLimite" HorizontalAlign="Right" Width="80px">
                                                                                                                                    <MaskSettings Mask="&lt;1..999&gt;" PromptChar=" " />
                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxtv:ASPxTextBox>
                                                                                                                            </fieldset>
                                                                                                                            &nbsp;&nbsp; </td>
                                                                                                                        <td align="right" style="50">
                                                                                                                            <dxtv:ASPxButton ID="btnFaixaDeTolerancia" runat="server" ClientInstanceName="btnFaixaDeTolerancia">
                                                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcFaixaDeTolerancia.Show();
}" />
                                                                                                                                <Image ToolTip="Editar Faixa de Tolerância" Url="~/imagens/botoes/FaixaDeTolerancia.png">
                                                                                                                                </Image>
                                                                                                                                <Paddings Padding="0px" />
                                                                                                                            </dxtv:ASPxButton>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxLabel ID="lblGlossario" runat="server" Text="Glossário:">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxhe:ASPxHtmlEditor ID="heGlossario" runat="server" ClientInstanceName="heGlossario" Height="159px" Width="100%">
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
                                                                                                                            <dxhe:ToolbarInsertImageDialogButton>
                                                                                                                            </dxhe:ToolbarInsertImageDialogButton>
                                                                                                                            <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False">
                                                                                                                            </dxhe:ToolbarCheckSpellingButton>
                                                                                                                            <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                                                                                <Items>
                                                                                                                                    <dxhe:ToolbarInsertTableDialogButton BeginGroup="True" Text="Insert Table..." ToolTip="Insert Table...">
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
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Normal" Value="p" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Address" Value="address" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                                                                                </Items>
                                                                                                                            </dxhe:ToolbarParagraphFormattingEdit>
                                                                                                                            <dxhe:ToolbarFontNameEdit>
                                                                                                                                <Items>
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                                                                                </Items>
                                                                                                                            </dxhe:ToolbarFontNameEdit>
                                                                                                                            <dxhe:ToolbarFontSizeEdit>
                                                                                                                                <Items>
                                                                                                                                    <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                                                                                                    <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7" />
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
                                                                                                                <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                                                                <StylesToolbars>
                                                                                                                    <BarDockControl Wrap="True">
                                                                                                                    </BarDockControl>
                                                                                                                    <Toolbar>
                                                                                                                        <SeparatorPaddings Padding="0px" PaddingLeft="1px" />
                                                                                                                    </Toolbar>
                                                                                                                    <ToolbarItem>
                                                                                                                        <Paddings PaddingLeft="1px" PaddingRight="1px" />
                                                                                                                    </ToolbarItem>
                                                                                                                </StylesToolbars>
                                                                                                            </dxhe:ASPxHtmlEditor>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding-top: 3px">
                                                                                                            <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" ClientInstanceName="lblAjuda" Font-Bold="False" Font-Italic="False" ForeColor="#404040" Text="(*) - Código utilizado para interface com outros sistemas da instituição.">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>

                                                                                                </table>
                                                                                            </dxw:ContentControl>
                                                                                        </ContentCollection>
                                                                                    </dxtc:TabPage>
                                                                                    <dxtc:TabPage Name="TabFT" Text="Faixa de Toler&#226;ncia" Visible="False">
                                                                                        <ContentCollection>
                                                                                            <dxw:ContentControl runat="server">
                                                                                            </dxw:ContentControl>
                                                                                        </ContentCollection>
                                                                                    </dxtc:TabPage>
                                                                                    <dxtc:TabPage Name="TabDado" Text="Componentes da F&#243;rmula">
                                                                                        <ContentCollection>
                                                                                            <dxw:ContentControl runat="server">
                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblCaptionIndicador"
                                                                                                                    ForeColor="Gray" ID="lblCaptionIndicador">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gridDadosIndicador" KeyFieldName="CodigoDado"
                                                                                                                    AutoGenerateColumns="False" Width="100%"
                                                                                                                    ID="gridDadosIndicador" OnCustomCallback="gridDadosIndicador_CustomCallback"
                                                                                                                    OnCancelRowEditing="gridDadosIndicador_CancelRowEditing" OnRowInserting="gridDadosIndicador_RowInserting"
                                                                                                                    OnCellEditorInitialize="gridDadosIndicador_CellEditorInitialize" OnRowUpdating="gridDadosIndicador_RowUpdating"
                                                                                                                    OnRowDeleting="gridDadosIndicador_RowDeleting" OnAfterPerformCallback="gridDadosIndicador_AfterPerformCallback"
                                                                                                                    EnableRowsCache="False" EnableViewState="False">
                                                                                                                    <ClientSideEvents EndCallback="function(s, e) {
	var foiDadoExcluido = s.cp_DadoExcluido;

	if(foiDadoExcluido != null &amp;&amp; foiDadoExcluido == 'S')
		btnFechar.SetEnabled(false);
}"></ClientSideEvents>
                                                                                                                    <Columns>
                                                                                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="80px" Caption="A&#231;&#245;es"
                                                                                                                            ShowEditButton="true" ShowDeleteButton="true" VisibleIndex="0">
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
                                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="Sequencia" ReadOnly="True" Width="20px"
                                                                                                                            Caption=" #" VisibleIndex="1">
                                                                                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                                                            </CellStyle>
                                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoDado" Width="35px" Caption="Tipo"
                                                                                                                            VisibleIndex="2">
                                                                                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                                                                                            <DataItemTemplate>
                                                                                                                                <%# (int.Parse(Eval("CodigoDado").ToString()) > 0) ? "<img src='../../imagens/dado.png' />" : "<img src='../../imagens/indicadoresMenor.png' />" %>
                                                                                                                            </DataItemTemplate>
                                                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                                                            </CellStyle>
                                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoDado" Width="300px" Caption="A (Componente)"
                                                                                                                            VisibleIndex="4">
                                                                                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoDado" Width="280px" Caption="A (Componente)"
                                                                                                                            Visible="False" VisibleIndex="3">
                                                                                                                            <PropertiesComboBox ImageUrlField="UrlTipoComponente" TextField="DescricaoDado" ValueField="CodigoDado"
                                                                                                                                ValueType="System.Int32" Width="280px">
                                                                                                                                <ClientSideEvents Init="function(s, e) {
	hfGeral.Set(&quot;NovoCodigoDado&quot;, s.GetValue());
}"
                                                                                                                                    SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;NovoCodigoDado&quot;, s.GetValue());
}"
                                                                                                                                    Validation="function(s, e) {	
    if(s.GetValue() == null)
	{
		e.isValid = false;
		e.errorText = 'Campo Obrigatório!';
	}
}"></ClientSideEvents>
                                                                                                                                <ValidationSettings CausesValidation="True" Display="Dynamic"
                                                                                                                                    ValidationGroup="MKE">
                                                                                                                                    <RequiredField IsRequired="True" />
                                                                                                                                </ValidationSettings>
                                                                                                                            </PropertiesComboBox>
                                                                                                                            <EditFormSettings Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                                                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoFuncaoDadoIndicador" Width="100px"
                                                                                                                            Caption="B (Fun&#231;&#227;o)" VisibleIndex="5">
                                                                                                                            <PropertiesComboBox DataSourceID="dsFuncao" TextField="NomeFuncao" ValueField="CodigoFuncao"
                                                                                                                                ValueType="System.Int32">
                                                                                                                                <ClientSideEvents Init="function(s, e) {
	hfGeral.Set(&quot;NovoCodigoFuncao&quot;, s.GetValue());
}"
                                                                                                                                    SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;NovoCodigoFuncao&quot;, s.GetValue());
}"></ClientSideEvents>
                                                                                                                            </PropertiesComboBox>
                                                                                                                            <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                                                                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="valorDado" Width="66px" Caption="C (Valor)"
                                                                                                                            VisibleIndex="6">
                                                                                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                                                                                            <DataItemTemplate>
                                                                                                                                <dxe:ASPxTextBox ID="txtValorDado" runat="server" ClientInstanceName="txtValorDado"
                                                                                                                                    Width="60px" HorizontalAlign="Right">
                                                                                                                                </dxe:ASPxTextBox>
                                                                                                                            </DataItemTemplate>
                                                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                                                            </CellStyle>
                                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Visible="False" VisibleIndex="7">
                                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoDado" Visible="False" VisibleIndex="8">
                                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeFuncao" Visible="False" VisibleIndex="9">
                                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                                    </Columns>
                                                                                                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>
                                                                                                                    <SettingsPager Mode="ShowAllRecords" PageSize="4" Visible="False">
                                                                                                                    </SettingsPager>
                                                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                                                    </SettingsEditing>
                                                                                                                    <SettingsPopup>
                                                                                                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                                            AllowResize="True" Width="400px" />
                                                                                                                    </SettingsPopup>
                                                                                                                    <Settings ShowGroupButtons="False" ShowFooter="True" VerticalScrollBarMode="Visible"
                                                                                                                        VerticalScrollableHeight="361"></Settings>
                                                                                                                    <SettingsText ConfirmDelete="Confirmar exclus&#227;o?" PopupEditFormCaption="Componentes"></SettingsText>
                                                                                                                    <Styles>
                                                                                                                        <Table BackColor="White">
                                                                                                                        </Table>
                                                                                                                        <EmptyDataRow BackColor="White">
                                                                                                                        </EmptyDataRow>
                                                                                                                        <TitlePanel BackColor="White">
                                                                                                                        </TitlePanel>
                                                                                                                    </Styles>
                                                                                                                    <StylesPopup>
                                                                                                                        <EditForm>
                                                                                                                            <MainArea HorizontalAlign="Left"></MainArea>
                                                                                                                        </EditForm>
                                                                                                                    </StylesPopup>
                                                                                                                    <Templates>
                                                                                                                        <FooterRow>
                                                                                                                            <table cellspacing="0" cellpadding="0">
                                                                                                                                <tbody>
                                                                                                                                    <tr>
                                                                                                                                        <td align="left">
                                                                                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/dado.png" ID="ASPxImage1" Height="16px">
                                                                                                                                            </dxe:ASPxImage>
                                                                                                                                        </td>
                                                                                                                                        <td style="padding-left: 3px; padding-right: 10px;" align="left">
                                                                                                                                            <dxe:ASPxLabel runat="server" Text="Dado" ID="ASPxLabel3">
                                                                                                                                            </dxe:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                        <td align="left">
                                                                                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/indicadoresMenor.png" ID="ASPxImage2"
                                                                                                                                                Height="16px">
                                                                                                                                            </dxe:ASPxImage>
                                                                                                                                        </td>
                                                                                                                                        <td align="left" style="padding-left: 3px; padding-right: 10px">
                                                                                                                                            <dxe:ASPxLabel runat="server" Text="Indicador"
                                                                                                                                                ID="ASPxLabel4">
                                                                                                                                            </dxe:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </tbody>
                                                                                                                            </table>
                                                                                                                        </FooterRow>
                                                                                                                    </Templates>
                                                                                                                </dxwgv:ASPxGridView>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <table cellspacing="0" cellpadding="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td style="width: 5px" id="tdDetalhes">&nbsp;
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                                                    <tbody>
                                                                                                                                        <tr>
                                                                                                                                            <td align="left">
                                                                                                                                                <dxe:ASPxLabel runat="server" Text="F&#243;rmula " ClientInstanceName="lblFormula"
                                                                                                                                                    ID="lblFormula">
                                                                                                                                                </dxe:ASPxLabel>
                                                                                                                                                <dxe:ASPxLabel runat="server" Text="  exemplo (0,2 * C1 + C2) :" ClientInstanceName="lblExemplo2"
                                                                                                                                                    Font-Italic="True" ForeColor="Teal" ID="lblExemplo2">
                                                                                                                                                </dxe:ASPxLabel>
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 42px"></td>
                                                                                                                                            <td style="width: 80px"></td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="999" ClientInstanceName="txtFormulaIndicador"
                                                                                                                                                    ID="txtFormulaIndicador">
                                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                                    </DisabledStyle>
                                                                                                                                                </dxe:ASPxTextBox>
                                                                                                                                            </td>
                                                                                                                                            <td align="center" style="padding-left: 3px; padding-right: 3px;">
                                                                                                                                                <dxe:ASPxButton runat="server" Text="=" ID="btnEqual"
                                                                                                                                                    Width="100%">
                                                                                                                                                    <ClientSideEvents Click="function(s, e) {
	                executaFormula();
	                e.processOnServer = false;
                }"></ClientSideEvents>
                                                                                                                                                    <Paddings Padding="0px" />
                                                                                                                                                    <Paddings Padding="0px"></Paddings>
                                                                                                                                                </dxe:ASPxButton>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Center" ClientInstanceName="txtResultadoFormula"
                                                                                                                                                    ID="txtResultadoFormula">
                                                                                                                                                </dxe:ASPxTextBox>
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
                                                                                                            <td style="height: 10px"></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="popupNovoDado" HeaderText="Novo Dado"
                                                                                                                    Modal="True" PopupElementID="popupDado" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                                                                                                    ShowCloseButton="False" Width="600px" Height="124px"
                                                                                                                    ID="popupNovoDado">
                                                                                                                    <ClientSideEvents Shown="function(s, e) {
        txtNome.SetText(&quot;&quot;);
    }"></ClientSideEvents>
                                                                                                                    <ContentStyle>
                                                                                                                        <Paddings Padding="5px"></Paddings>
                                                                                                                    </ContentStyle>
                                                                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                                                    <ContentCollection>
                                                                                                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                                                                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                                                                <tbody>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <dxe:ASPxLabel runat="server" Text="Nome do Dado:" CssPostfix="Aqua" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                                                                                                                                ClientInstanceName="lblNome" ID="lblNome">
                                                                                                                                            </dxe:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtNome"
                                                                                                                                                ID="txtNome">
                                                                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="PPR">
                                                                                                                                                    <ErrorImage Height="14px">
                                                                                                                                                    </ErrorImage>
                                                                                                                                                    <RequiredField IsRequired="True" ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                                                                                </ValidationSettings>
                                                                                                                                                <DisabledStyle ForeColor="Black">
                                                                                                                                                </DisabledStyle>
                                                                                                                                            </dxe:ASPxTextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="height: 10px"></td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td align="right">
                                                                                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                                                                                <tbody>
                                                                                                                                                    <tr>
                                                                                                                                                        <td>
                                                                                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarPP"
                                                                                                                                                                Text="Salvar" ValidationGroup="PPR" CssPostfix="Glass" CssFilePath="~/App_Themes/Glass/{0}/styles.css"
                                                                                                                                                                Width="90px" ID="btnSalvarNovoDado">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
                    if(txtNome.GetValue() != &quot;&quot;) 
                    {	
                        insereNovoDado();
                        popupNovoDado.Hide();
                     }
                }"></ClientSideEvents>
                                                                                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                                                                                            </dxe:ASPxButton>
                                                                                                                                                        </td>
                                                                                                                                                        <td></td>
                                                                                                                                                        <td>
                                                                                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelaPP"
                                                                                                                                                                Text="Cancelar" CssPostfix="Glass" CssFilePath="~/App_Themes/Glass/{0}/styles.css"
                                                                                                                                                                Width="90px" ID="btnCancelaNovoDado">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) { popupNovoDado.Hide(); }"></ClientSideEvents>
                                                                                                                                                                <Paddings Padding="0px" />
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
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </dxw:ContentControl>
                                                                                        </ContentCollection>
                                                                                    </dxtc:TabPage>
                                                                                </TabPages>
                                                                                <ClientSideEvents ActiveTabChanged="function(s, e) {
	var tab = TabControl.GetActiveTab();

	if(TipoOperacao == 'Incluir')
	{
	    if(e.tab.name=='TabDado')
	    {
	        TabControl.SetActiveTab(TabControl.tabs[0]);
        }
    }
	pcDados.AdjustSize();
}"
                                                                                    ActiveTabChanging="function(s, e) {
	pcDados.AdjustSize();
}"
                                                                                    TabClick="function(s, e) {
	pcDados.AdjustSize();
}"
                                                                                    Init="function(s, e) {
	pcDados.AdjustSize();
}"></ClientSideEvents>
                                                                                <ContentStyle>
                                                                                    <Paddings Padding="3px" PaddingLeft="3px" PaddingRight="3px"></Paddings>
                                                                                </ContentStyle>
                                                                            </dxtc:ASPxPageControl>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE"
                                                                                                CssPostfix="Aqua" CssFilePath="~/App_Themes/Aqua/{0}/styles.css" Width="90px"
                                                                                                ID="btnSalvar">
                                                                                                <ClientSideEvents Click="function(s, e) {	
	var valido = true;
	
	if(hfGeral.Get('TipoOperacao').toString() == 'Incluir')
		valido = true;
	else
	{
	if(parseFloat(txtD1.GetValue().toString().replace(',', '.')) &gt; parseFloat(txtA1.GetValue().toString().replace(',', '.')))
	{
		txtA1.SetIsValid(false);
		txtA1.Validate();
		valido = false;
	}

	if(parseFloat(txtD2.GetValue().toString().replace(',', '.')) &gt; parseFloat(txtA2.GetValue().toString().replace(',', '.')))
	{
		txtA2.SetIsValid(false);
		txtA2.Validate();
		valido = false;
	}

	if(parseFloat(txtD3.GetValue().toString().replace(',', '.')) &gt; parseFloat(txtA3.GetValue().toString().replace(',', '.')))
	{
		txtA3.SetIsValid(false);
		txtA3.Validate();
		valido = false;
	}

	if((ddlC4.GetValue() != 'S') &amp;&amp; (parseFloat(txtD4.GetValue().toString().replace(',', '.')) &gt; parseFloat(txtA4.GetValue().toString().replace(',', '.'))))
	{
		txtA4.SetIsValid(false);
		txtA4.Validate();
		valido = false;
	}
	}
	
    if(valido == true)
	{   
		onClick_btnSalvar();
	}
	else
	{
		window.top.mostraMensagem('Faixa de Toler&#226;ncia Inv&#225;lida', 'atencao', true, false, null);	
	}
	e.processOnServer = false;
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px" />
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td align="right"></td>
                                                                                        <td align="right">
                                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" CssPostfix="Aqua"
                                                                                                CssFilePath="~/App_Themes/Aqua/{0}/styles.css" Width="90px"
                                                                                                ID="btnFechar">
                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px" />
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
                                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcFaixaDeTolerancia" HeaderText="Faixa de Toler&#226;ncia"
                                                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                                    ShowCloseButton="False" ID="pcFaixaDeTolerancia">
                                                    <ClientSideEvents Shown="function(s, e) {
	if (hfGeral.Get(&quot;TipoOperacao&quot;).toString() == 'Editar')
    	btnSalvarFaixaDeTolerancia.SetVisible(true);
	else
		btnSalvarFaixaDeTolerancia.SetVisible(false);

	pnCallbackFaixaTolerancia.PerformCallback();
}"></ClientSideEvents>
                                                    <ContentStyle HorizontalAlign="Left">
                                                    </ContentStyle>
                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                    <ContentCollection>
                                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackFaixaTolerancia"
                                                                                Width="100%" ID="pnCallbackFaixaTolerancia" OnCallback="pnCallbackFaixaTolerancia_Callback">
                                                                                <ClientSideEvents EndCallback="function(s, e) {
	//desabilitaHabilitaComponentes();
	//pcFaixaDeTolerancia.Show();
	if(s.cp_FallaEditada == &quot;OK&quot;)
	{
		mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
		pcFaixaDeTolerancia.Hide();
	}
}"></ClientSideEvents>
                                                                                <PanelCollection>
                                                                                    <dxp:PanelContent runat="server">
                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td valign="top" align="left">
                                                                                                        <table cellspacing="0" cellpadding="0">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxLabel runat="server" Text="Cor:" ID="lblCor">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxLabel runat="server" Text="De:" ID="lblDe">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxLabel runat="server" Text="At&#233;:"
                                                                                                                            ID="lblAte">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.String" Width="95px"
                                                                                                                            ClientInstanceName="ddlC1" ClientEnabled="False" BackColor="#EBEBEB"
                                                                                                                            ID="ddlC1">
                                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD1, txtA1, null);
	txtD1.SetFocus();
}"
                                                                                                                                Init="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD1, txtA1, null);
}"></ClientSideEvents>
                                                                                                                            <Items>
                                                                                                                                <dxe:ListEditItem Text="Vermelho" Value="Vermelho" Selected="True"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Amarelo" Value="Amarelo"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Verde" Value="Verde"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Azul" Value="Azul"></dxe:ListEditItem>
                                                                                                                            </Items>
                                                                                                                            <DisabledStyle ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtD1"
                                                                                                                            ID="txtD1">
                                                                                                                            <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtA1"
                                                                                                                            ID="txtA1">
                                                                                                                            <ClientSideEvents TextChanged="function(s, e) {
	if(ddlC2.GetValue() != 'S' &amp;&amp; parseFloat(s.GetValue().toString().replace(',', '.')) != 0)
		txtD2.SetText((parseFloat(s.GetValue().toString().replace(',', '.')) + 0.01).toString().replace('.', ','));
}"
                                                                                                                                Validation="function(s, e) {
	if(parseFloat(txtD1.GetValue()) &gt; parseFloat(s.GetValue()))
		e.isValid = false;
}"></ClientSideEvents>
                                                                                                                            <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Valor da Faixa Superior Menor que o Valor da Faixa Inferior">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxComboBox runat="server" SelectedIndex="1" ValueType="System.String" Width="95px"
                                                                                                                            ClientInstanceName="ddlC2" ClientEnabled="False" BackColor="#EBEBEB"
                                                                                                                            ID="ddlC2">
                                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(ddlC1.GetValue() == 'S')
	{
		s.SetValue('S');
		window.top.mostraMensagem('Escolha a Faixa de Toler&#226;ncia Acima para Continuar!', 'atencao', true, false, null);	
	}
	else
	{
		verificaCamposFaixaTolerancia(s, txtD2, txtA2, txtA1);
		txtD2.SetFocus();
	}
}"
                                                                                                                                Init="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD2, txtA2, txtA1);
}"></ClientSideEvents>
                                                                                                                            <Items>
                                                                                                                                <dxe:ListEditItem Text="Vermelho" Value="Vermelho"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Amarelo" Value="Amarelo" Selected="True"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Verde" Value="Verde"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Azul" Value="Azul"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Sem Faixa" Value="S"></dxe:ListEditItem>
                                                                                                                            </Items>
                                                                                                                            <DisabledStyle ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtD2"
                                                                                                                            ID="txtD2">
                                                                                                                            <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtA2"
                                                                                                                            ID="txtA2">
                                                                                                                            <ClientSideEvents TextChanged="function(s, e) {
	if(ddlC3.GetValue() != 'S' &amp;&amp; parseFloat(s.GetValue().toString().replace(',', '.')) != 0)
		txtD3.SetText((parseFloat(s.GetValue().toString().replace(',', '.')) + 0.01).toString().replace('.', ','));
}"
                                                                                                                                Validation="function(s, e) {
	if(parseFloat(txtD2.GetValue()) &gt; parseFloat(s.GetValue()))
		e.isValid = false;
}"></ClientSideEvents>
                                                                                                                            <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Valor da Faixa Superior Menor que o Valor da Faixa Inferior"
                                                                                                                                ValidationGroup="MKE">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxComboBox runat="server" SelectedIndex="2" ValueType="System.String" Width="95px"
                                                                                                                            ClientInstanceName="ddlC3" ClientEnabled="False" BackColor="#EBEBEB"
                                                                                                                            ID="ddlC3">
                                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(ddlC1.GetValue() == 'S' || ddlC2.GetValue() == 'S')
	{
		s.SetValue('S');
		window.top.mostraMensagem('Escolha as Faixas de Toler&#226;ncia Acima para Continuar!', 'atencao', true, false, null);		
	}
	else
	{
		verificaCamposFaixaTolerancia(s, txtD3, txtA3, txtA2);
		txtD1.SetFocus();
	}
}"
                                                                                                                                Init="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD3, txtA3, txtA2);
}"></ClientSideEvents>
                                                                                                                            <Items>
                                                                                                                                <dxe:ListEditItem Text="Vermelho" Value="Vermelho"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Amarelo" Value="Amarelo"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Verde" Value="Verde" Selected="True"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Azul" Value="Azul"></dxe:ListEditItem>
                                                                                                                            </Items>
                                                                                                                            <DisabledStyle ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtD3"
                                                                                                                            ID="txtD3">
                                                                                                                            <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtA3"
                                                                                                                            ID="txtA3">
                                                                                                                            <ClientSideEvents TextChanged="function(s, e) {
	if(ddlC4.GetValue() != &quot;S&quot; &amp;&amp; parseFloat(s.GetValue().toString().replace(',', '.')) != 0)
		txtD4.SetText((parseFloat(s.GetValue().toString().replace(',', '.')) + 0.01).toString().replace('.', ','));
}"
                                                                                                                                Validation="function(s, e) {
	if(parseFloat(txtD3.GetValue()) &gt; parseFloat(s.GetValue()))
		e.isValid = false;
}"></ClientSideEvents>
                                                                                                                            <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Valor da Faixa Superior Menor que o Valor da Faixa Inferior">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxComboBox runat="server" SelectedIndex="4" ValueType="System.String" Width="95px"
                                                                                                                            ClientInstanceName="ddlC4" ID="ddlC4">
                                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(ddlC1.GetValue() == 'S' || ddlC2.GetValue() == 'S' || ddlC3.GetValue() == 'S')
	{
		s.SetValue('S');
		window.top.mostraMensagem('Escolha as Faixas de Toler&#226;ncia Acima para Continuar!', 'atencao', true, false, null);		
	}
	else
	{
		verificaCamposFaixaTolerancia(s, txtD4, txtA4, txtA3);
		txtD1.SetFocus();
	}
}"
                                                                                                                                Init="function(s, e) {
	verificaCamposFaixaTolerancia(s, txtD4, txtA4, txtA3);
}"></ClientSideEvents>
                                                                                                                            <Items>
                                                                                                                                <dxe:ListEditItem Text="Vermelho" Value="Vermelho"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Amarelo" Value="Amarelo"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Verde" Value="Verde"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Azul" Value="Azul"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Sem Faixa" Value="S" Selected="True"></dxe:ListEditItem>
                                                                                                                            </Items>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtD4"
                                                                                                                            ID="txtD4">
                                                                                                                            <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td style="padding-left: 10px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="90px" HorizontalAlign="Right" ClientInstanceName="txtA4"
                                                                                                                            ID="txtA4">
                                                                                                                            <ClientSideEvents Validation="function(s, e) {
	if(parseFloat(txtD4.GetValue()) &gt; parseFloat(s.GetValue()))
		e.isValid = false;
}"></ClientSideEvents>
                                                                                                                            <MaskSettings Mask="&lt;0..9999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Valor da Faixa Superior Menor que o Valor da Faixa Inferior">
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
                                                                                                    <td style="padding-top: 10px" valign="top">
                                                                                                        <dxe:ASPxLabel runat="server" Text="(*) " Font-Bold="True" Font-Italic="False"
                                                                                                            Font-Size="7pt" ForeColor="#404040" ID="ASPxLabel2">
                                                                                                        </dxe:ASPxLabel>
                                                                                                        <dxe:ASPxLabel runat="server" Text="O desempenho considera o resultado do indicador em rela&#231;&#227;o &#224; sua meta"
                                                                                                            Font-Bold="False" Font-Italic="False" ForeColor="#404040"
                                                                                                            ID="ASPxLabel10">
                                                                                                        </dxe:ASPxLabel>
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
                                                                        <td style="padding-top: 10px" align="right">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvarFaixaDeTolerancia" Text="Salvar"
                                                                                                Width="90px" ID="btnSalvarFaixaDeTolerancia">
                                                                                                <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
	var valido = true;

	if(parseFloat(txtD1.GetValue().toString().replace(',', '.')) &gt; parseFloat(txtA1.GetValue().toString().replace(',', '.')))
	{
		txtA1.SetIsValid(false);
		txtA1.Validate();
		valido = false;
	}
	if(parseFloat(txtD2.GetValue().toString().replace(',', '.')) &gt; parseFloat(txtA2.GetValue().toString().replace(',', '.')))
	{
		txtA2.SetIsValid(false);
		txtA2.Validate();
		valido = false;
	}
	if(parseFloat(txtD3.GetValue().toString().replace(',', '.')) &gt; parseFloat(txtA3.GetValue().toString().replace(',', '.')))
	{
		txtA3.SetIsValid(false);
		txtA3.Validate();
		valido = false;
	}
	if((ddlC4.GetValue() != 'S') &amp;&amp; (parseFloat(txtD4.GetValue().toString().replace(',', '.')) &gt; parseFloat(txtA4.GetValue().toString().replace(',', '.'))))
	{
		txtA4.SetIsValid(false);
		txtA4.Validate();
		valido = false;
	}
	
    if(valido == true)
		pnCallbackFaixaTolerancia.PerformCallback('Salvar');
	else
		window.top.mostraMensagem('Faixa de Toler&#226;ncia Inv&#225;lida', 'atencao', true, false, null);	
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px" />
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px" align="right">
                                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"
                                                                                                ID="ASPxButton3">
                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcFaixaDeTolerancia.Hide();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px" />
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
                                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" CloseAction="None"
                                                    EnableClientSideAPI="True" HeaderText="Mensagem" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowShadow="False"
                                                    Width="721px" ID="pcMensagemGravacao">
                                                    <HeaderImage Url="~/imagens/alertAmarelho.png">
                                                    </HeaderImage>
                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                    <ContentCollection>
                                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl6" runat="server">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="lblAtencao" runat="server" ClientInstanceName="lblAtencao" Font-Bold="True"
                                                                                Text="Atenção:">
                                                                            </dxe:ASPxLabel>
                                                                            &nbsp;<dxe:ASPxLabel ID="lblMensagemError" runat="server" ClientInstanceName="lblMensagemError"
                                                                                Text="ASPxLabel">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dxpc:PopupControlContentControl>
                                                    </ContentCollection>
                                                </dxpc:ASPxPopupControl>
                                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcAjudaNovoResp"
                                                    CloseAction="CloseButton" HeaderText="Ajuda" Modal="True" PopupElementID="imgAjudaNovoResp"
                                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="293px"
                                                    ID="pcAjudaNovoResp">
                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                    <ContentCollection>
                                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                                                            Código utilizado para interface com outros sistemas da instituição.
                                                        </dxpc:PopupControlContentControl>
                                                    </ContentCollection>
                                                </dxpc:ASPxPopupControl>
                                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                                    ShowCloseButton="False" ShowHeader="False" Width="270px"
                                                    ID="pcUsuarioIncluido">
                                                    <ContentCollection>
                                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl9" runat="server">
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
                                                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dxpc:PopupControlContentControl>
                                                    </ContentCollection>
                                                </dxpc:ASPxPopupControl>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
    if ( hfGeral.Contains(&quot;StatusSalvar&quot;) )
    {
        var status = hfGeral.Get(&quot;StatusSalvar&quot;);
        if (status != &quot;1&quot;)
        {
            var mensagem = hfGeral.Get(&quot;ErroSalvar&quot;);
            window.top.mostraMensagem(mensagem, 'erro', true, false, null);
        }
        else
        {
            if (window.onEnd_pnCallback)
                onEnd_pnCallback();
                
	        if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		        mostraDivSalvoPublicado(&quot;Indicador inclu&#237;do com sucesso!&quot;);
            else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		        mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
	        else if(&quot;EditarCompartilhar&quot; == s.cp_OperacaoOk)
		        mostraDivSalvoPublicado(&quot;Indicador compartilhado com sucesso!&quot;);

            pcResponsavel2.Hide();
        }
    }
}
"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:SqlDataSource ID="dsFuncao" runat="server" ConnectionString="" SelectCommand="SELECT CodigoFuncao, NomeFuncao FROM TipoFuncaoDado UNION SELECT 0 AS Expr1, 'STATUS' AS Expr2 ORDER BY NomeFuncao"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsResponsavel" runat="server" ConnectionString="" SelectCommand=""></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
</asp:Content>
