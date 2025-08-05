<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="indicadoresEstrategicos.aspx.cs" Inherits="_Estrategias_wizard_indicadores" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript">
        var pastaImagens = "../../imagens";
    </script>
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div id="divGrid" style="visibility: hidden">
                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoIndicador"
                                                AutoGenerateColumns="False" Width="100%"
                                                ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                                <ClientSideEvents CustomButtonClick="function(s, e) 
{
	e.processOnServer = false;
	gvDados.SetFocusedRowIndex(e.visibleIndex);
	TipoOperacao = '';

    if(e.buttonID == 'btnExcluir')
    {
		TipoOperacao = 'Excluir';
		//onClickBarraNavegacao(TipoOperacao, gvDados, null);
        var func = function(existeDependencia){
			if(existeDependencia == 'S')
				window.top.mostraMensagem(traducao.indicadoresEstrategicos_este_indicador_possui_metas_e_ou_resultados_lan_ados__a_exclus_o_deste_indicador_implicar__na_perda_de_todas_as_informa__es_hist_ricas__confirma_a_exclus_o_, 'confirmacao', true, true, ExcluirRegistroSelecionado);
			else
				window.top.mostraMensagem(traducao.indicadoresEstrategicos_deseja_realmente_excluir_o_registro_, 'confirmacao', true, true, ExcluirRegistroSelecionado);
        };
		 
		s.GetRowValues(e.visibleIndex, 'ExisteDependencia', func);
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
    	     abreDetalhes(s.GetRowKey(e.visibleIndex), 'S');
	    }
	    else
		{
			if(e.buttonID == 'btnNovo')
			{
	    			abreDetalhes(-1, 'N');
	    		}
	    	else if(e.buttonID == 'btnEditar')
		    {
    		    abreDetalhes(s.GetRowKey(e.visibleIndex), 'N');
	    	}

					}
	}
}
" Init="function(s, e) 
                                                    {
                                                    AdjustSize();
                                                    document.getElementById(&quot;divGrid&quot;).style.visibility = &quot;&quot;;
                                                    }"></ClientSideEvents>
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
                                                        Width="100px" Caption="Compartilhado" Visible="False" VisibleIndex="26">
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
                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaIndicadorCompartilhado" Visible="False" VisibleIndex="14">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavel" Visible="False" VisibleIndex="16" Name="CodigoResponsavel">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoReservado" Visible="False" VisibleIndex="29" Caption="Código Reservado">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="PossuiMetaResultado" Visible="False" VisibleIndex="30" Caption="PossuiMetaResultado">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="MapaEstrategico" VisibleIndex="18" Caption="Mapa Estratégico" GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavelResultado" Visible="False" VisibleIndex="25">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavelAtualizacaoIndicador" Visible="False"
                                                        VisibleIndex="24">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="PodeCompartilhar" Visible="False"
                                                        VisibleIndex="22">
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
                                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="grid-legendas-cor grid-legendas-cor-criado-em-outra-unidade"><span></span></td>
                                                                    <td class="grid-legendas-label">
                                                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                            ClientInstanceName="lblDescricaoNaoAtiva" Font-Bold="False"
                                                                            Text="<%# Resources.traducao.indicadoresEstrategicos_indicador_criado_em_outra_entidade %>">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </FooterRow>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
                                                
                                            </div>
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
                                                                            ForeColor="#404040" ID="lblAjuda">
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
                
	        if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		        mostraDivSalvoPublicado(traducao.indicadoresEstrategicos_indicador_exclu__237_do_com_sucesso);
	        else if(&quot;EditarCompartilhar&quot; == s.cp_OperacaoOk)
		        mostraDivSalvoPublicado(traducao.indicadoresEstrategicos_indicador_compartilhado_com_sucesso);
        }
    }
}
"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="dsResponsavel" runat="server" ConnectionString="" SelectCommand=""></asp:SqlDataSource>
</asp:Content>
