<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InteressadosObjeto.aspx.cs"
    Inherits="_Estrategias_InteressadosObjeto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></!--script -->
    <title>Lista de Interessados</title>
    <script language="javascript" type="text/javascript"> </script>
    <link href="../estilos/custom.css" rel="stylesheet" />



    <link href="../estilos/custom.css" rel="stylesheet" />
    <style type="text/css">
        @media (max-height: 768px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 200px;
            }
        }

        @media (min-height: 769px) and (max-height: 800px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 200px;
            }
        }

        @media (min-height: 801px) and (max-height: 960px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 300px;
            }
        }

        @media (min-height: 961px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 600px;
            }
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>

            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" Width="100%" OnCallback="pnCallback_Callback">
                            <ClientSideEvents EndCallback="function(s, e) {

lpLoading.Hide();
    if (hfGeral.Get('StatusSalvar') == '1')
    {
        if (TipoOperacao == 'Incluir')
        {
                
                 TipoOperacao = 'Editar';
            hfGeral.Set('TipoOperacao', TipoOperacao);
        }
	    if('Incluir' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao(traducao.InteressadosObjeto_perfil_inclu_do_com_sucesso_);
	    else if('Editar' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao(traducao.InteressadosObjeto_perfil_alterado_com_sucesso_);
		else if('Excluir' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao(traducao.InteressadosObjeto_perfil_excluido_com_sucesso_);
	    else if('IncluirPermissao' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao(traducao.InteressadosObjeto_permiss_o_inclu_do_com_sucesso_);
	    else if('EditarPermissao' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao(traducao.InteressadosObjeto_permiss_o_alterado_com_sucesso_);
                  else if('AssociarUsuariosNoPerfil' == s.cp_OperacaoOk)
                   {
                           mostraPopupMensagemGravacao('Associação do usuário ao perfil feita com sucesso!');
                           pcDadosUsuarios.Hide();
                   }
	    callbackGeral.PerformCallback('CerrarSession');
		onClick_Cancelar(); //onClick_btnCancelar();
    }
    else if (hfGeral.Get('StatusSalvar') == '0')
    {
        mensagemErro = hfGeral.Get('ErroSalvar');
        
        if (TipoOperacao == 'Excluir')
        {
            // se existe um tratamento de erro especifico da opçao que está sendo executada
            if (window.trataMensagemErro)
                mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
            else // caso contrário, usa o tratamento padrão
            {
                // se for erro de Chave Estrangeira (FK)
                if (mensagemErro.indexOf('REFERENCE') &gt;= 0 )
                    mensagemErro = traducao.InteressadosObjeto_o_registro_n__227_o_pode_ser_exclu__237_do_pois_est__225__sendo_utilizado_por_outro;
            }
        }
        window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
    }
}
"
                                BeginCallback="function(s, e) {
	        	lpLoading.Show();
}" />
                            <PanelCollection>
                                <dxcp:PanelContent runat="server">
                                    <dxcp:ASPxPageControl ID="pcAbas" runat="server" ActiveTabIndex="0" Width="100%" ClientInstanceName="pcAbas">
                                        <TabPages>
                                            <dxtv:TabPage Text="Interessados">
                                                <ContentCollection>
                                                    <dxtv:ContentControl runat="server">
                                                        <div id="divGrid" style="visibility: visible">
                                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" OnPageIndexChanged="gvDados_PageIndexChanging" 
                                                                KeyFieldName="CodigoObjetoAssociado;CodigoTipoAssociacao;CodigoUsuario"
                                                                AutoGenerateColumns="False" Width="100%"
                                                                ID="gvDados" OnCustomCallback="gvDados_CustomCallback"
                                                                OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                                                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                                                OnDataBound="gvDados_DataBound">
                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
     LimpaCamposFormulario();
     if(e.buttonID == 'btnEditarCustom')		onClickBarraNavegacao('Editar', gvDados, pcDados);
     else if(e.buttonID == 'btnDetalheCustom')	onClickDetalheInteressado(s,e);
     else if(e.buttonID == 'btnExcluirCustom')	onClickBarraNavegacao('Excluir', gvDados, pcDados);
}"
                                                                    Init="function(s, e) 
                                                    {
                                                        var height = Math.max(0, document.documentElement.clientHeight) - 75;
                                                        if(btnFechar.GetVisible() == false)
                                                      {
                                                                s.SetHeight(height);
                                                      }
                                                      else
                                                      {
                                                               height = height - 75;
                                                               s.SetHeight(height);
                                                      }
                                                        
                                                        document.getElementById('divGrid').style.visibility = '';
                                                    }"></ClientSideEvents>
                                                                <TotalSummary>
                                                                    <dxwgv:ASPxSummaryItem SummaryType="Count" FieldName="NomeUsuario" DisplayFormat="Total interessados: {0}"></dxwgv:ASPxSummaryItem>
                                                                </TotalSummary>

                                                                <SettingsPopup>
                                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                </SettingsPopup>
                                                                <Columns>
                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="A&#231;&#227;o" Width="100px" Caption="A&#231;&#227;o" VisibleIndex="0">
                                                                        <CustomButtons>
                                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                                                <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                                                <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                                                                <Image Url="~/imagens/botoes/pFormulario.png"></Image>
                                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                                        </CustomButtons>

                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                        <HeaderTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                                            ClientInstanceName="menu" ItemSpacing="5px" OnItemClick="menu_ItemClick"
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
                                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" Caption="Interessado" VisibleIndex="1">
                                                                        <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="Perfis" Name="Perfis" Caption="Perfis" VisibleIndex="2">
                                                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuario" Name="CodigoUsuario" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="IniciaisTipoAssociacao" Name="IniciaisTipoAssociacao" Caption="IniciaisTipoAssociacao" Visible="False" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="HerdaPermissoesObjetoSuperior" Name="HerdaPermissoesObjetoSuperior" Caption="HerdaPermissoesObjetoSuperior" Visible="False" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaPermissoesPersonalizadas"
                                                                        Name="IndicaPermissoesPersonalizadas" Width="30px" VisibleIndex="6"
                                                                        Caption=" ">
                                                                        <Settings AllowAutoFilter="False"></Settings>
                                                                        <DataItemTemplate>
                                                                            <%# (Eval("IndicaPermissoesPersonalizadas").ToString().Equals("N") ? "<img title='' alt='' style='border:0px' src='../imagens/vazioMenor.gif'/>" : "<img title='Permissões Personalizadas' alt='' style='border:0px' src='../imagens/Perfis/Editado.png'/>")%>
                                                                        </DataItemTemplate>

                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoAssociacao" Name="CodigoTipoAssociacao" Caption="CodigoTipoAssociacao" Visible="False" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaPermissoesPersonalizadas" Name="IndicaPermissoesPersonalizadas" Visible="False" VisibleIndex="8"></dxwgv:GridViewDataTextColumn>
                                                                </Columns>

                                                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                                                <SettingsPager PageSize="100">
                                                                </SettingsPager>

                                                                <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>

                                                                <SettingsText></SettingsText>

                                                                <Styles>
                                                                    <TitlePanel HorizontalAlign="Left"></TitlePanel>
                                                                </Styles>

                                                                <Templates>
                                                                    <FooterRow>
                                                                        <table class="" cellspacing="0" cellpadding="0" width="100%">
                                                                            <tr>
                                                                                <td class="grid-legendas-icone">
                                                                                    <dxe:ASPxImage ID="ASPxImage2" runat="server"
                                                                                        ImageUrl="~/imagens/Perfis/Editado.png">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                                <td class="grid-legendas-label">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:traducao, permiss_o_personalizada %>">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td class="grid-legendas-asterisco">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="*">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td class="grid-legendas-label">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:traducao, permiss_es_herdadas_em_n_vel_superior %>">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </FooterRow>
                                                                </Templates>
                                                            </dxwgv:ASPxGridView>
                                                        </div>

                                                    </dxtv:ContentControl>
                                                </ContentCollection>
                                            </dxtv:TabPage>
                                            <dxtv:TabPage Text="Perfis">
                                                <ContentCollection>
                                                    <dxtv:ContentControl runat="server">
                                                        <dxtv:ASPxGridView ID="gvPerfisAtribuiveis" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPerfisAtribuiveis" KeyFieldName="CodigoPerfil" Width="100%">
                                                            <ClientSideEvents CustomButtonClick="function(s, e) {
	s.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnEditar&quot;)
     {	
		//lpLoading.Show();
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
        s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoPerfil;NomePerfil', MontaCamposPopupUsuarios);
	 }
     }"
                                                                FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                                                Init="function(s, e) {
	                                                        var height = Math.max(0, document.documentElement.clientHeight) - 75;
                                                        if(btnFechar.GetVisible() == false)
                                                      {
                                                                s.SetHeight(height);
                                                      }
                                                      else
                                                      {
                                                               height = height - 75;
                                                               s.SetHeight(height);
                                                      }


}" />
                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm">
                                                            </SettingsEditing>
                                                            <Settings ShowGroupButtons="False" ShowGroupPanel="True" VerticalScrollBarMode="Visible" />
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ConfirmDelete="True" />
                                                            <SettingsPopup>
                                                                <EditForm AllowResize="True" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="600px">
                                                                </EditForm>
                                                                <HeaderFilter MinHeight="140px">
                                                                </HeaderFilter>
                                                            </SettingsPopup>
                                                            <SettingsLoadingPanel ShowImage="False" Text="" />
                                                            <SettingsText ConfirmDelete="Excluir perfil ?" PopupEditFormCaption="Perfis Notificados" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" Caption="Ação" ShowInCustomizationForm="True" VisibleIndex="0" Width="5%">
                                                                    <CustomButtons>
                                                                        <dxtv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                            </Image>
                                                                        </dxtv:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <HeaderTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <dxtv:ASPxMenu ID="menu0" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu0_Init" OnItemClick="menu0_ItemClick">
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
                                                                <dxtv:GridViewDataTextColumn Caption="Perfil" FieldName="NomePerfil" Name="perfil" ShowInCustomizationForm="True" VisibleIndex="1" Width="75%">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <CellStyle HorizontalAlign="Left">
                                                                    </CellStyle>
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="DescricaoPerfil" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="IdentificacaoImagemPerfil" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="CorFundoImagemPerfil" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="IniciaisPerfil" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="ordemApresentacaoPerfil" ShowInCustomizationForm="True" VisibleIndex="6" Visible="False">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="CodigoPerfil" ShowInCustomizationForm="True" VisibleIndex="7" Visible="False">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Usuários" ShowInCustomizationForm="True" VisibleIndex="8" Width="20%" FieldName="QuantidadeUsuarios">
                                                                </dxtv:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dxtv:ASPxGridView>
                                                    </dxtv:ContentControl>
                                                </ContentCollection>
                                            </dxtv:TabPage>
                                        </TabPages>
                                    </dxcp:ASPxPageControl>
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>

                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px">
                        <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td class="formulario-botao">

                                        <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Fechar" Width="100px"
                                            ID="btnFechar"
                                            ClientInstanceName="btnFechar" ClientVisible="False">
                                            <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if(window.parent.fechaModal){
		LimpaCamposFormulario();
        window.parent.fechaModal();
    }
	else if(window.top.fechaModal){
		LimpaCamposFormulario();
        window.top.fechaModal();
    }
}"></ClientSideEvents>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackGeral" ID="callbackGeral" OnCallback="callbackGeral_Callback">            
            </dxcb:ASPxCallback>


            <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackConceder" ID="callbackConceder" OnCallback="callbackConceder_Callback">
                <ClientSideEvents EndCallback="function(s, e) {
	gvPermissoes.PerformCallback('ATL');
}"></ClientSideEvents>
            </dxcb:ASPxCallback>
            <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                ExportEmptyDetailGrid="True" GridViewID="gvDados" Landscape="True"
                LeftMargin="50" RightMargin="50"
                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            </dxwgv:ASPxGridViewExporter>
            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server"
                ExportEmptyDetailGrid="True" GridViewID="gvPerfisAtribuiveis" Landscape="True"
                LeftMargin="50" RightMargin="50"
                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            </dxwgv:ASPxGridViewExporter>
        </div>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhe" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="850px" Height="400px" ID="pcDados">
            <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
	ddlInteressado.SetText('');
}"></ClientSideEvents>

            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackDetalhe" Width="100%" ID="pnCallbackDetalhe" OnCallback="pnCallbackDetalhe_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	if(TipoOperacao == 'Incluir')
		pcDados.Show();
}"></ClientSideEvents>
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table id="tbDetalhe" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="Interessado:" ClientInstanceName="lblGerente" ID="lblGerente"></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                            <dxe:ASPxComboBox runat="server" ValueType="System.Int32"
                                                                TextField="NomeUsuario" ValueField="CodigoUsuario" TextFormatString="{0}"
                                                                Width="100%" ClientInstanceName="ddlInteressado" ForeColor="Black" ID="ddlInteressado"
                                                                IncrementalFilteringMode="Contains" EnableCallbackMode="True"
                                                                DropDownStyle="DropDown" >
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e){
    var codigoInteressado = s.GetValue();
    if(isNaN(codigoInteressado)) return;
    if(codigoInteressado != null)
    {
        if(window.hfGeral &amp;&amp; hfGeral.Contains('CodigoUsuarioPermissao'))
            hfGeral.Set('CodigoUsuarioPermissao', codigoInteressado);
            
        if(lbListaPerfisSelecionados.GetItemCount()&gt;0)
        {
            btnSeleccionarPermissao.SetEnabled(true);
            btnSalvarPerfis.SetEnabled(true);
        }
        else
        {
            btnSeleccionarPermissao.SetEnabled(false);
            btnSalvarPerfis.SetEnabled(false);
        }
        ObtemListaPerfisDisponiveis(codigoInteressado);
    }
}"
                                                            KeyDown="function(s, e) 
                                                                    { 
                                                                        if(e.htmlEvent.keyCode == 13) 
                                                                        { 
                                                                            ASPxClientUtils.PreventEventAndBubble(e.htmlEvent); 
                                                                            e.ProcessOnServer = false;
                                                                        } 
                                                                    }"></ClientSideEvents>
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="NomeUsuario" Width="60%" Caption="Nome"></dxe:ListBoxColumn>
                                                                    <dxe:ListBoxColumn FieldName="Email" Width="40%" Caption="E-mail"></dxe:ListBoxColumn>
                                                                </Columns>

                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                            </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 5px">
                                                <table id="Table2" cellspacing="0" cellpadding="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 350px">
                                                                <dxe:ASPxLabel runat="server" Text="Perfis dispon&#237;veis:" ClientInstanceName="lblProjetosDisponivel" ID="lblProjetosDisponivel">
                                                                    <ClientSideEvents Init="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>
                                                                </dxe:ASPxLabel>


                                                            </td>
                                                            <td valign="top" align="center"></td>
                                                            <td style="width: 350px">
                                                                <dxe:ASPxLabel runat="server" Text="Perfis selecionados:" ClientInstanceName="lblProjetosSelecionados" ID="lblProjetosSelecionados"></dxe:ASPxLabel>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" SelectionMode="CheckColumn" FilteringSettings-ShowSearchUI="true" TextField="DescricaoPerfil" ValueField="CodigoPerfil" ClientInstanceName="lbListaPerfisDisponivel" EnableClientSideAPI="True" Width="100%" ID="lbListaPerfisDisponivel" OnCallback="lbListaPerfisDisponivel_Callback" Height="200px" EnableViewState="false" EnableSynchronization="False">
                                                                    <FilteringSettings EditorNullText="Filtrar texto" />
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	UpdateButtons();
}
"
                                                                        SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>

                                                                    <ValidationSettings>
                                                                        <ErrorImage Width="14px"></ErrorImage>
                                                                    </ValidationSettings>
                                                                </dxe:ASPxListBox>
                                                            </td>
                                                            <td valign="middle" align="center">
                                                                <table class="formulario-lista-botoes" cellspacing="0" cellpadding="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodos" ClientEnabled="False" Text="&gt;&gt;" Width="40px" ID="btnADDTodos">
                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveTodosItens(lbListaPerfisDisponivel, lbListaPerfisSelecionados);
	UpdateButtons();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADD" ClientEnabled="False" Text="&gt;" Width="40px" ID="btnADD">
                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbListaPerfisDisponivel, lbListaPerfisSelecionados);
	UpdateButtons();	
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMV" ClientEnabled="False" Text="&lt;" Width="40px" ID="btnRMV">
                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbListaPerfisSelecionados, lbListaPerfisDisponivel);
	UpdateButtons();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodos" ClientEnabled="False" Text="&lt;&lt;" Width="40px" ID="btnRMVTodos">
                                                                                    <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
	lb_moveTodosItens(lbListaPerfisSelecionados, lbListaPerfisDisponivel);
	UpdateButtons();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <dxe:ASPxListBox runat="server" EncodeHtml="False" SelectionMode="CheckColumn" Rows="4" FilteringSettings-ShowSearchUI="true" TextField="DescricaoPerfil" ValueField="CodigoPerfil" ClientInstanceName="lbListaPerfisSelecionados" EnableClientSideAPI="True" Width="100%" ID="lbListaPerfisSelecionados" OnCallback="lbListaPerfisSelecionados_Callback" Height="200px" EnableViewState="false" EnableSynchronization="False">
                                                                    <FilteringSettings EditorNullText="Filtrar texto" />
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	UpdateButtons();
}"
                                                                        SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>

                                                                    <ValidationSettings>
                                                                        <ErrorImage Width="14px"></ErrorImage>
                                                                    </ValidationSettings>
                                                                </dxe:ASPxListBox>


                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 5px">
                                                <dxe:ASPxCheckBox runat="server" Text="Herdar permiss&#245;es do objeto superior?" ClientInstanceName="checkHerdarPermissoes" ID="checkHerdarPermissoes"></dxe:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="formulario-botao">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btnSeleccionarPermissao" Text="Personalizar"
                                                                    ID="btnSeleccionarPermissao"
                                                                    Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	
	if(lblCaptionInteressado.GetText() == '')
		lblCaptionInteressado.SetText(ddlInteressado.GetText());
	if(lblCaptionPerfil.GetText() == '')
	    lblCaptionPerfil.SetText(lbListaPerfisSelecionados.mainElement.innerText.toString());
	if(lblCaptionHerda.GetText() == '')
	    lblCaptionHerda.SetText(checkHerdarPermissoes.GetChecked() ? 'Sim' : 'N&#227;o');
	if(window.hfGeral &amp;&amp; hfGeral.Contains(&quot;HerdaPermissoes&quot;))
		hfGeral.Set('HerdaPermissoes', checkHerdarPermissoes.GetChecked() ? 'S' : 'N');

	pcPermissoes.Show();
}"></ClientSideEvents>
                                                                </dxe:ASPxButton>


                                                            </td>
                                                            <td class="formulario-botao">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btnSalvarPerfis" Text="Salvar" ID="btnSalvarPerfis" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {
    var mensagemErro = verificarDadosPreenchidos(); 
  if(mensagemErro == '')
    {
	   if (window.SalvarCamposFormulario)
    	{
			if(window.hfGeral &amp;&amp; hfGeral.Contains(&quot;HerdaPermissoes&quot;))
				hfGeral.Set('HerdaPermissoes', checkHerdarPermissoes.GetChecked() ? 'S' : 'N');

            if (SalvarCamposFormulario())
	        {
                                                                        
                    pcDados.Hide();
        	    habilitaModoEdicaoBarraNavegacao(false, gvDados);
            	return true;
	        }
    	}
    	else
	    {
    	    window.top.mostraMensagem(traducao.InteressadosObjeto_o_m__233_todo_n__227_o_foi_implementado_, 'atencao', true, false, null);
	    }
    }
    else
    {
                                                                        alert('retorno clique botao');
        window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
        e.processOnServer = false;
    }
}




"></ClientSideEvents>
                                                                </dxe:ASPxButton>


                                                            </td>
                                                            <td class="formulario-botao">
                                                                <dxe:ASPxButton runat="server"
                                                                    CommandArgument="btnCancelar" Text="Fechar" Width="100px" ID="btnCancelar">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_Cancelar)
       onClick_Cancelar();
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
                    </dxcp:ASPxCallbackPanel>

                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDadosUsuarios" CloseAction="None" HeaderText="Detalhe" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="850px" Height="400px" ID="pcDadosUsuarios" Modal="True">
            <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
	ddlInteressado.SetText('');
}"></ClientSideEvents>

            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackDetalhe_perfil" Width="100%" ID="pnCallbackDetalhe_perfil">
                        <ClientSideEvents EndCallback="function(s, e) {
	if(TipoOperacao == 'Incluir')
		pcDados.Show();
}"></ClientSideEvents>
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table id="tbDetalhe" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxLabel ID="lblNomePerfil" runat="server" ClientInstanceName="lblNomePerfil" Text="Perfil:">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxTextBox ID="txtNomePerfil" runat="server" ClientInstanceName="txtNomePerfil" MaxLength="50" Width="100%" ReadOnly="True">
                                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </ReadOnlyStyle>
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
                                            <td style="padding-top: 5px">
                                                <table id="Table2" cellspacing="0" cellpadding="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 350px">
                                                                <dxe:ASPxLabel runat="server" Text="Usuários disponíveis:" ClientInstanceName="lblProjetosDisponivel" ID="ASPxLabel5">
                                                                    <ClientSideEvents Init="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>
                                                                </dxe:ASPxLabel>


                                                            </td>
                                                            <td valign="top" align="center"></td>
                                                            <td style="width: 350px">
                                                                <dxe:ASPxLabel runat="server" Text="Usuários com o perfil:" ClientInstanceName="lblProjetosSelecionados" ID="ASPxLabel6"></dxe:ASPxLabel>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" SelectionMode="CheckColumn" FilteringSettings-ShowSearchUI="true" TextField="NomeApresentacaoUsuario" ValueField="CodigoUsuario" ClientInstanceName="lbListaUsuariosDisponivel" EnableClientSideAPI="True" Width="100%" ID="lbListaUsuariosDisponivel" OnCallback="lbListaUsuariosDisponivel_Callback" Height="200px" EnableViewState="false" EnableSynchronization="False" ValueType="System.Int32">
                                                                    <FilteringSettings EditorNullText="Filtrar texto" />
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	//UpdateButtonsUsuarios();
    lbListaUsuariosSelecionados.PerformCallback(s.cpCodigoPerfil);
}
"
                                                                        SelectedIndexChanged="function(s, e) {
	UpdateButtonsUsuarios();
}"></ClientSideEvents>

                                                                    <ValidationSettings>
                                                                        <ErrorImage Width="14px"></ErrorImage>
                                                                    </ValidationSettings>
                                                                </dxe:ASPxListBox>
                                                            </td>
                                                            <td valign="middle" align="center">
                                                                <table class="formulario-lista-botoes" cellspacing="0" cellpadding="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodosUsuarios" ClientEnabled="False" Text="&gt;&gt;" Width="40px" ID="btnADDTodosUsuarios">
                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;

                                                                                        

	lb_moveTodosItens(lbListaUsuariosDisponivel, lbListaUsuariosSelecionados);
	UpdateButtonsUsuarios();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDUsuarios" ClientEnabled="False" Text="&gt;" Width="40px" ID="btnADDUsuarios">
                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbListaUsuariosDisponivel, lbListaUsuariosSelecionados);
	UpdateButtonsUsuarios();	
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVUsuarios" ClientEnabled="False" Text="&lt;" Width="40px" ID="btnRMVUsuarios">
                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbListaUsuariosSelecionados, lbListaUsuariosDisponivel);
	UpdateButtonsUsuarios();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodosUsuarios" ClientEnabled="False" Text="&lt;&lt;" Width="40px" ID="btnRMVTodosUsuarios">
                                                                                    <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
	lb_moveTodosItens(lbListaUsuariosSelecionados, lbListaUsuariosDisponivel);
	UpdateButtonsUsuarios();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <dxe:ASPxListBox runat="server" EncodeHtml="False" SelectionMode="CheckColumn" Rows="4" FilteringSettings-ShowSearchUI="true" TextField="NomeApresentacaoUsuario" ValueField="CodigoUsuario" ClientInstanceName="lbListaUsuariosSelecionados" EnableClientSideAPI="True" Width="100%" ID="lbListaUsuariosSelecionados" OnCallback="lbListaUsuariosSelecionados_Callback" Height="200px" EnableViewState="false" EnableSynchronization="False" ValueType="System.Int32">
                                                                    <FilteringSettings EditorNullText="Filtrar texto" />
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	UpdateButtonsUsuarios();
}"
                                                                        SelectedIndexChanged="function(s, e) {
	UpdateButtonsUsuarios();
}"></ClientSideEvents>

                                                                    <ValidationSettings>
                                                                        <ErrorImage Width="14px"></ErrorImage>
                                                                    </ValidationSettings>
                                                                </dxe:ASPxListBox>


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
                                                            <td class="formulario-botao">&nbsp;</td>
                                                            <td class="formulario-botao">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btnSalvarUsuarios" Text="Salvar" ID="btnSalvarUsuarios" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {	AssociarUsuariosNoPerfil();   } "></ClientSideEvents>
                                                                </dxe:ASPxButton>


                                                            </td>
                                                            <td class="formulario-botao">
                                                                <dxe:ASPxButton runat="server"
                                                                    CommandArgument="btnCancelarUsuarios" Text="Fechar" Width="100px" ID="btnCancelarUsuarios" ClientInstanceName="btnCancelarUsuarios">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
pcDadosUsuarios.Hide();
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
                    </dxcp:ASPxCallbackPanel>

                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcPermissoes"
            CloseAction="None" HeaderText="Permiss&#245;es" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="False" Width="810px"
            ID="pcPermissoes" AllowDragging="True">
            <ClientSideEvents PopUp="function(s, e) {
    var group = nbAssociacao.GetActiveGroup();
    if (group != null)
    {
        var item = group.GetItem(0);
        if (item != null)
        {
	        var textoItem = item.name;
	        nbAssociacao.SetSelectedItem(item);

            CallBackGvPermissoes(getIniciaisAssociacaoFromTextoMenu(textoItem));
        } // if (item != null)
    } // if (group != null)
}"
                Shown="function(s, e) {
	s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 120);
                s.SetWidth(Math.max(0, document.documentElement.clientWidth) - 100);

            s.UpdatePosition();
}"></ClientSideEvents>

            <ContentStyle>
                <Paddings PaddingLeft="5px" PaddingTop="5px" PaddingRight="5px" PaddingBottom="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackPermissoes" Width="100%" ID="pnCallbackPermissoes" OnCallback="pnCallbackPermissoes_Callback">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="padding-right: 5px; width: 180px" valign="top">
                                                                <div class="rolagem-tab">
                                                                    <dxnb:ASPxNavBar runat="server" AllowSelectItem="True" AutoCollapse="True" ClientInstanceName="nbAssociacao" Width="100%" ID="nbAssociacao">
                                                                        <ClientSideEvents ItemClick="function(s, e) {
	onClick_MenuPermissao(s, e);
}"></ClientSideEvents>
                                                                        <Groups>
                                                                            <dxnb:NavBarGroup Name="gpAssociacao" Text="Tipo Associac&#227;o"></dxnb:NavBarGroup>
                                                                        </Groups>

                                                                        <Paddings PaddingTop="0px"></Paddings>

                                                                        <ItemStyle Wrap="True"></ItemStyle>
                                                                    </dxnb:ASPxNavBar>
                                                                </div>


                                                            </td>
                                                            <td valign="top">
                                                                <!-- GRIDVIEW gvPermissoes -->
                                                                <dxwgv:ASPxGridView runat="server"
                                                                    ClientInstanceName="gvPermissoes" KeyFieldName="CodigoPermissao"
                                                                    AutoGenerateColumns="False" Width="100%"
                                                                    ID="gvPermissoes" OnCustomCallback="gvPermissoes_CustomCallback"
                                                                    OnHtmlRowPrepared="gvPermissoes_HtmlRowPrepared"
                                                                    OnAfterPerformCallback="gvPermissoes_AfterPerformCallback">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	//if(s.cp_RecarregaGvPermissao == &quot;OK&quot;)
	//	gvPermissoes.PerformCallback('ATL');
}"
                                                                        Init="function(s, e) {
	s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 175);
}"></ClientSideEvents>
                                                                    <Columns>
                                                                        <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True"
                                                                            VisibleIndex="0" Width="40px">
                                                                        </dxwgv:GridViewCommandColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="imagemIcone" Name="imagemIcone"
                                                                            Width="30px" Caption="Inf" VisibleIndex="1">
                                                                            <Settings AllowAutoFilter="False" />
                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                            <DataItemTemplate>
                                                                                <%# Eval("imagemIcono").ToString() != "" ? string.Format("<img title='' alt='' style='border:0px' src='../imagens/Perfis/{0}'/>", Eval("imagemIcono").ToString()) : "<img title='' alt='' style='border:0px' src='../imagens/vazioMenor.gif'/>"%>
                                                                            </DataItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoItemPermissao" GroupIndex="0"
                                                                            SortIndex="0" SortOrder="Ascending" Name="DescricaoItemPermissao" Caption=" "
                                                                            VisibleIndex="2">
                                                                            <Settings AllowDragDrop="False" AllowGroup="True" AllowAutoFilter="False"></Settings>

                                                                            <GroupFooterCellStyle Font-Italic="True"></GroupFooterCellStyle>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoAcaoPermissao" ReadOnly="True"
                                                                            Name="DescricaoAcaoPermissao" Caption="Permiss&#245;es" VisibleIndex="3" Width="150px">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataCheckColumn FieldName="Concedido" Name="Concedido" Width="80px"
                                                                            Caption="Conceder" VisibleIndex="4">
                                                                            <PropertiesCheckEdit ClientInstanceName="chkConceder"></PropertiesCheckEdit>
                                                                            <Settings AllowAutoFilter="False" />

                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                            <DataItemTemplate>
                                                                                <%# getCheckBox("CheckConcedido", "Concedido", "C")%>
                                                                            </DataItemTemplate>

                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                        <dxwgv:GridViewDataCheckColumn FieldName="Delegavel" Name="Delegavel" Width="80px"
                                                                            Caption="Extens&#237;vel" VisibleIndex="5">
                                                                            <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextUnchecked="N&#227;o" ClientInstanceName="chkExtensivel"></PropertiesCheckEdit>
                                                                            <Settings AllowAutoFilter="False" />

                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                            <DataItemTemplate>
                                                                                <%# getCheckBox("CheckDelegavel", "Delegavel", "D")%>
                                                                            </DataItemTemplate>

                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                        <dxwgv:GridViewDataCheckColumn FieldName="Negado" Name="Negado" Width="70px"
                                                                            Caption="Negar" VisibleIndex="6">
                                                                            <PropertiesCheckEdit ClientInstanceName="chkNegar"></PropertiesCheckEdit>
                                                                            <Settings AllowAutoFilter="False" />

                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                            <DataItemTemplate>
                                                                                <%# getCheckBox("CheckNegado", "Negado", "N")%>
                                                                            </DataItemTemplate>

                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                        <dxwgv:GridViewDataCheckColumn FieldName="Incondicional" Name="Incondicional"
                                                                            Width="105px" Caption="Incondicional" VisibleIndex="8">
                                                                            <PropertiesCheckEdit ClientInstanceName="chkIncondicional" EnableClientSideAPI="True"></PropertiesCheckEdit>
                                                                            <Settings AllowAutoFilter="False" />

                                                                            <Settings AllowAutoFilter="False"></Settings>
                                                                            <DataItemTemplate>
                                                                                <%# getCheckBox("CheckIncondicional", "Incondicional", "I")%>
                                                                            </DataItemTemplate>

                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Herdada" Caption="Herdada" Visible="False"
                                                                            VisibleIndex="10">
                                                                            <PropertiesTextEdit ClientInstanceName="txtHerdada"></PropertiesTextEdit>

                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="FlagsOrigem" Caption="FlagsOrigem"
                                                                            Visible="False" VisibleIndex="11">
                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Outorgavel" Name="Outorgavel"
                                                                            Caption="Outorgavel" Visible="False" VisibleIndex="9">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoPermissao" Name="CodigoPermissao"
                                                                            Caption="CodigoPermissao" Visible="False" VisibleIndex="7">
                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="ReadOnly" Name="ReadOnly" Visible="False"
                                                                            VisibleIndex="12">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="imagemReadOnly" Name="imagemReadOnly"
                                                                            Visible="False" VisibleIndex="13">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="IncondicionalBloqueado"
                                                                            Name="IncondicionalBloqueado" Caption="IncondicionalBloqueado" Visible="False"
                                                                            VisibleIndex="14">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                    </Columns>

                                                                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                                                    <Settings ShowFooter="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="300"></Settings>

                                                                    <SettingsText></SettingsText>

                                                                    <Styles>
                                                                        <Disabled BackColor="#EBEBEB" ForeColor="Black"></Disabled>
                                                                    </Styles>

                                                                    <Templates>
                                                                        <FooterRow>
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/Perfis/Editado.png"></dxe:ASPxImage>
                                                                                        </td>
                                                                                        <td style="padding-right: 10px; padding-left: 3px">
                                                                                            <dxe:ASPxLabel ID="lblAjudaPersonalizada" runat="server" Text="<%$ Resources:traducao, permiss_o_personalizada %>"></dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="border-right: green 2px solid; border-top: green 2px solid; border-left: green 2px solid; width: 10px; border-bottom: green 2px solid; background-color: #ddffcc; border-width: 1px; border-color: #808080;">&nbsp;</td>
                                                                                        <td style="padding-right: 10px; padding-left: 3px">
                                                                                            <dxe:ASPxLabel ID="lblAjudaBloqueada" runat="server" Text="<%$ Resources:traducao, permiss_o_n_o_edit_vel %>"></dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/Perfis/Perfil_Herdada.png"></dxe:ASPxImage>
                                                                                        </td>
                                                                                        <td style="padding-right: 10px; padding-left: 3px">
                                                                                            <dxe:ASPxLabel ID="lblAjudaHerdada" runat="server" Text="<%$ Resources:traducao, permiss_o_herdada %>"></dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </FooterRow>
                                                                    </Templates>
                                                                </dxwgv:ASPxGridView>


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
                                <td style="padding-top: 5px" align="right">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="padding-left: 10px; padding-bottom: 0px" align="left">
                                                    <dxe:ASPxLabel runat="server" Text="Interessado: " ClientInstanceName="lblInteressado" Font-Bold="False" ForeColor="#404040" ID="lblInteressado"></dxe:ASPxLabel>

                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblCaptionInteressado" Font-Bold="True" ForeColor="#404040" ID="lblCaptionInteressado"></dxe:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; padding-bottom: 0px" align="left">
                                                    <dxe:ASPxLabel runat="server" Text="Perfis: " ClientInstanceName="lblPerfis" Font-Bold="False" ForeColor="#404040" ID="lblPerfis"></dxe:ASPxLabel>

                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblCaptionPerfil" Font-Bold="True" ForeColor="#404040" ID="lblCaptionPerfil"></dxe:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; padding-bottom: 0px" align="left">
                                                    <dxe:ASPxLabel runat="server" Text="Herdar permiss&#245;es do objeto superior?:" ClientInstanceName="lblHerda" Font-Bold="False" ForeColor="#404040" ID="lblHerda"></dxe:ASPxLabel>

                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblCaptionHerda" Font-Bold="True" ForeColor="#404040" ID="lblCaptionHerda"></dxe:ASPxLabel>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table cellspacing="0" cellpadding="4" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px" ID="btnSalvar">
                                                        <ClientSideEvents Click="function(s, e){
	if (window.SalvarCamposFormulario)
    {
		TipoOperacao += 'Permissao';
        if (SalvarCamposFormulario())
	    {
    		pcPermissoes.Hide();
        	habilitaModoEdicaoBarraNavegacao(false, gvDados);
            return true;
		}
	}
    else
	{
    	window.top.mostraMensagem(traducao.InteressadosObjeto_o_m__233_todo_n__227_o_foi_implementado_, 'atencao', true, false, null);
	}
}"></ClientSideEvents>
                                                    </dxe:ASPxButton>

                                                </td>
                                                <td class="formulario-botao">
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                        CommandArgument="btnVoltar" Text="Fechar" Width="100px" ID="btnVoltar">
                                                        <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    pcPermissoes.Hide();
    
    gvPermissoes.PerformCallback('VOLTAR');
    
    //if(window.hfGeral &amp;&amp; hfGeral.Contains('CodigoUsuarioPermissao'))
    //{
        //lbListaPerfisDisponivel.PerformCallback(hfGeral.Get('CodigoUsuarioPermissao'));
        //lbListaPerfisSelecionados.PerformCallback(hfGeral.Get('CodigoUsuarioPermissao'));
    //}
        
    //callbackGeral.PerformCallback('CerrarSession');
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
        <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
        </dxcp:ASPxLoadingPanel>
    </form>
</body>
</html>
