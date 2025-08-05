
<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="carteirasDeProjetos.aspx.cs" Inherits="_Projetos_Administracao_carteirasDeProjetos"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px" s>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Carteiras" ClientInstanceName="lblTitulo">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <!-- ASPxCALLBACKPANEL principal: pnCallback -->
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <!-- ASPxGRidVIEW: gvDados -->
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
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
                            <!--############################################@ INÍCIO @ PopUP - Carteiras ############################################-->
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoCarteira"
                                AutoGenerateColumns="False" ID="gvDados"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback" Width="100%" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnHtmlRowPrepared="gvDados_HtmlRowPrepared"
                                Style="text-align: left">
                                <ClientSideEvents CustomButtonClick="function(s, e) {
    //gvDados.SetFocusedRowIndex(e.visibleIndex);
btnSalvar1.SetVisible(true);   

 if(e.buttonID == &quot;btnIncluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDadosNovaCarteira);
     }
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
        pcDadosNovaCarteira.SetHeaderText('Editar Carteira');
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDadosNovaCarteira);
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDadosNovaCarteira);
     }
     else if(e.buttonID == &quot;AdicionarProjeto&quot;)
     {
                                    //InserirNovaCarteira(gvDados);
        OnGridFocusedRowChangedProjetos(gvDados, true);
                                    pcDadosProjeto.Show();
                                    //Desabilita Componentes para inserção de Projeto apenas.
                                    txtNomeCarteira.SetEnabled(false);
                                    memDescricaoCarteira.SetEnabled(false);
                                    checkAtivo.SetEnabled(false);
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {     
        pcDadosNovaCarteira.SetHeaderText('Visualizar Carteira');
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar1.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		pcDadosNovaCarteira.Show();

        LimpaCamposFormularioExclusaoProjeto();
     }
	else if(e.buttonID == 'btnPermissoesCustom')
	{
	    OnGridFocusedRowChangedPopup(gvDados);
	}
}"></ClientSideEvents>

                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                </SettingsCommandButton>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="A&#231;&#245;es" VisibleIndex="0"
                                        Width="160px">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Text="Inserir" Visibility="Invisible">
                                                <Image AlternateText="Inserir" Url="~/imagens/botoes/incluirReg02.png">
                                                </Image>
                                                <Styles>
                                                    <Style Width="21px">
                                                    </Style>
                                                </Styles>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Retirar da carteira">
                                                <Image AlternateText="Retirar da carteira" Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                                <Image AlternateText="Detalhe" Url="~/imagens/botoes/pFormulario.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnPermissoesCustom" Text="Administrar Permissões da Carteira">
                                                <Image Url="~/imagens/Perfis/Perfil_Permissoes.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxtv:GridViewCommandColumnCustomButton ID="AdicionarProjeto" Text="Associar Projeto à Carteira">
                                                <Image AlternateText="Associar Projeto à Carteira" IconID="spreadsheet_chartdatalabels_left_svg_16x16" ToolTip="Associar Projeto à Carteira" Url="~/imagens/botoes/AssociarProjetoCarteira.png">
                                                </Image>
                                            </dxtv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
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
                                    <dxwgv:GridViewDataTextColumn Caption="Carteira" FieldName="NomeCarteira" Name="NomeCarteira"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="CodigoCarteira" FieldName="CodigoCarteira"
                                        Name="CodigoCarteira" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoCarteira" Name="DescricaoCarteira"
                                        VisibleIndex="3">
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="CodigoEntidade" FieldName="CodigoEntidade"
                                        Name="CodigoEntidade" VisibleIndex="4" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IniciaisCarteiraControladaSistema" ShowInCustomizationForm="True"
                                        VisibleIndex="5" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoObjeto" ShowInCustomizationForm="True"
                                        VisibleIndex="6" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaCarteiraAtiva" ShowInCustomizationForm="True"
                                        VisibleIndex="7" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Controlada Sistema" FieldName="ControladaSistema"
                                        Name="ControladaSistema" ShowInCustomizationForm="True" VisibleIndex="8" Width="135px">
                                        <EditCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                        </EditCellStyle>
                                        <FilterCellStyle HorizontalAlign="Center">
                                        </FilterCellStyle>
                                        <EditFormCaptionStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                        </EditFormCaptionStyle>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                        </CellStyle>
                                        <FooterCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                        </FooterCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" ShowFooter="True" ShowGroupPanel="True"></Settings>
                                <Styles>
                                    <EmptyDataRow BackColor="#EEEEDD" ForeColor="Black">
                                    </EmptyDataRow>
                                </Styles>
                                <Templates>
                                    <FooterRow>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="grid-legendas-cor grid-legendas-cor-controlado-sistema"><span></span></td>
                                                    <td class="grid-legendas-label grid-legendas-label-controlado-sistema">
                                                        <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                            Text="<%# Resources.traducao.carteirasDeProjetos_carteiras_controladas_pelo_sistema %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-cor grid-legendas-cor-inativo"><span></span></td>
                                                    <td class="grid-legendas-label grid-legendas-label-inativo">
                                                        <span><%# definelegenda %></span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                            <dxtv:ASPxCallback ID="callbackSalvaselecao" runat="server" ClientInstanceName="callbackSalvaselecao" OnCallback="callbackSalvaselecao_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
	gvProjetos.Refresh();
window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3000); pcCheck.Hide();
}" />
                            </dxtv:ASPxCallback>
                            <!--############################################@ FIM @ PopUP - Carteiras ############################################-->

                            <!--############################################@ INÍCIO @ PopUP - Projetos da Carteira ############################################-->
                            <dxtv:ASPxPopupControl runat="server" ClientInstanceName="pcDadosProjeto" HeaderText="Projetos da Carteira"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ID="pcDadosProjeto" AllowDragging="True" CloseAction="None"
                                Width="960px" Text="Projetos da Carteira" Modal="True">




                                <ClientSideEvents Shown="function(s, e) {
        gvProjetos.GotoPage(0);
}"
                                    Closing="function(s, e) {
    gvProjetos.ClearFilter();  
}" />
                                <ContentStyle>
                                    <Paddings Padding="8px"></Paddings>
                                </ContentStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <!-- table -->
                                        <table class="headerGrid" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td class="auto-style2">
                                                                    <dxe:ASPxLabel ID="lblCarteira" runat="server" ClientInstanceName="lblCarteira"
                                                                        Text="Carteira:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <!-- ASPxROUNDPANEL -->
                                                                    <%--              <dxe:ASPxTextBox ID="txtNomeCarteira" runat="server" ClientInstanceName="txtNomeCarteira"
                                                                        MaxLength="150" Width="100%" ClientReadOnly="True">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>--%>
                                                                    <dxe:ASPxMemo ID="txtNomeCarteira" runat="server" ClientInstanceName="txtNomeCarteira"
                                                                        Height="60px" Width="600px" MaxLength="500">
                                                                        <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                            <border bordercolor="#E0E0E0"></border>
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                        Text="Descrição:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="memDescricaoCarteira" runat="server" ClientInstanceName="memDescricaoCarteira"
                                                                        Height="60px" Width="700px" MaxLength="500">
                                                                        <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                            <border bordercolor="#E0E0E0"></border>
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxMemo>
                                                                    <dxe:ASPxLabel ID="lblContadorMemo" runat="server" ClientInstanceName="lblContadorMemo"
                                                                        Font-Bold="True" ForeColor="#999999">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                    <dxe:ASPxCheckBox ID="checkAtivo" runat="server" ClientInstanceName="checkAtivo"
                                                        Text="Carteira de Projetos Ativa?">
                                                    </dxe:ASPxCheckBox>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvProjetos" KeyFieldName="CodigoProjeto" Width="100%" OnCustomCallback="gvProjetos_CustomCallback1">
                                                        <SettingsPager PageSize="20">
                                                        </SettingsPager>
                                                        <Settings VerticalScrollableHeight="275" VerticalScrollBarMode="Auto" ShowGroupButtons="False" />

                                                        <SettingsBehavior SelectionStoringMode="PerformanceOptimized" />

                                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                                        <Columns>
                                                            <dxtv:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0" Width="90px" ButtonRenderMode="Image">
                                                                <CustomButtons>
                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnExcluirProjeto">
                                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                        </Image>
                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>
                                                                <HeaderTemplate>
                                                                    <table>
                                                                        <tr style="width: 100%;">
                                                                            <td align="center">
                                                                                <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menuProjeto_Init">
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
                                                                            <td align="center">
                                                                                <dxtv:ASPxMenu ID="menuExcluir" runat="server" BackColor="Transparent" ClientInstanceName="menuExcluir" ItemSpacing="5px" OnInit="menuProjeto_Init" OnLoad="menuExcluir_Load">
                                                                                    <Paddings Padding="0px" />
                                                                                    <Items>
                                                                                        <dxtv:MenuItem Name="btnIncluir" ToolTip="Excluir" Text="">
                                                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG" AlternateText="Excluir" ToolTip="Excluir">
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
                                                                                        <dxtv:MenuItem ClientVisible="False" Name="btnLayout" Text="" ToolTip="Layout">
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
                                                            <dxtv:GridViewDataTextColumn Caption="Iniciativa" FieldName="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="1" Width="485px">
                                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn FieldName="Selecionado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5" Caption="Status Projeto">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn FieldName="CodigoStatusProjeto" ShowInCustomizationForm="True" VisibleIndex="2" Caption="Status Iniciativa">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn Caption="Tipo Iniciativa" FieldName="CodigoTipoProjeto" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn Caption="Ano Orçamento" FieldName="AnoOrcamentoProjeto" ShowInCustomizationForm="True" VisibleIndex="3" Visible="False">
                                                            </dxtv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <ClientSideEvents CustomButtonClick="function(s, e) {
 if(e.buttonID == &quot;btnExcluirProjeto&quot;)
     {
               callbackSalvaselecao.PerformCallback(s.GetRowKey(s.GetFocusedRowIndex()));
     }
}" />
                                                    </dxtv:ASPxGridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px"></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table class="formulario-botoes">
                                                        <tbody>
                                                            <tr>
                                                                <td class="auto-style3">&nbsp;</td>
                                                                <td class="formulario-botao" style="height: 27px">
                                                                    <dxe:ASPxButton ID="btnCancelar" runat="server" ClientInstanceName="btnCancelar"
                                                                        Text="Fechar" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
      pcDadosProjeto.Hide();
	
}" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxtv:ASPxPopupControl>
                            <!--############################################@ FIM @ PopUP - Projetos da Carteira ############################################-->

                            <!--############################################@ INÍCIO @ PopUP - Adicionar Projetos Disponíveis ############################################-->
                            <dxtv:ASPxCallback ID="callbackCheck" runat="server" OnCallback="callbackCheck_Callback" ClientInstanceName="callbackCheck">
                                <ClientSideEvents EndCallback="function(s, e) {
gvCheck.Refresh(); pcCheck.Hide();

gvProjetos.Refresh();
window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3000); pcCheck.Hide(); ASPxLoadingExcluirComboboxProjeto.Hide(); pcDadosProjeto.Show();
}" />
                            </dxtv:ASPxCallback>
                            <dxtv:ASPxCallback ID="callbackFechar2" runat="server" OnCallback="callbackrFechar_Callback2" ClientInstanceName="callbackFechar2">
                            </dxtv:ASPxCallback>

                            <dxtv:ASPxPopupControl ID="pcCheck" runat="server" ClientInstanceName="pcCheck" Width="960px" AllowDragging="True" CloseAction="None" ShowCloseButton="False" HeaderText="Adicionar Iniciativas" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Text="Disponíveis" Modal="True">
                                <ContentCollection>
                                    <dxtv:PopupControlContentControl runat="server">

                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxGridView ID="gvCheck" runat="server" ClientInstanceName="gvCheck" Width="100%" AutoGenerateColumns="False" KeyFieldName="CodigoProjeto" OnCustomCallback="gvCheck_CustomCallback">
                                                        <Settings VerticalScrollableHeight="275" VerticalScrollBarMode="Auto" />
                                                        <SettingsDataSecurity AllowDelete="False" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                                        <Columns>
                                                            <dxtv:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="50px" ShowSelectButton="True">
                                                            </dxtv:GridViewCommandColumn>
                                                            <dxtv:GridViewDataTextColumn Caption="Iniciativa" FieldName="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="1" Width="485px">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn Caption="Status Iniciativa" FieldName="CodigoStatusProjeto" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn Caption="Tipo Iniciativa" FieldName="CodigoTipoProjeto" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn Caption="Ano Orçamento" FieldName="AnoOrcamentoProjeto" ShowInCustomizationForm="True" VisibleIndex="3" Visible="False">
                                                            </dxtv:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dxtv:ASPxGridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table class="formulario-botoes">
                                                        <tbody>
                                                            <tr>
                                                                <td class="formulario-botao">
                                                                    <dxtv:ASPxButton ID="btnInserirCheck" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1" Text="Inserir Iniciativas" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
   
            ASPxLoadingExcluirComboboxProjeto.Show(); callbackCheck.PerformCallback(); pcDadosProjeto.Show();
	    
}" />
                                                                    </dxtv:ASPxButton>
                                                                </td>

                                                                <td class="formulario-botao">
                                                                    <dxtv:ASPxButton ID="ASPxButton2" runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="100px" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {              
                                                                                                                             ASPxLoadingExcluirComboboxProjeto.Show(); 
                                                                                                                             pcCheck.Hide();
                                                                                                                             gvCheck.PerformCallback('limpar'); 
                                                                                                                             pcDadosProjeto.Show(); 
                                                                                                                             ASPxLoadingExcluirComboboxProjeto.Hide();

                                                                                                                }" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxtv:PopupControlContentControl>
                                </ContentCollection>
                            </dxtv:ASPxPopupControl>
                            <!--############################################@ FIM @ PopUP - Adicionar Projetos Disponíveis ############################################-->

                            <!--############################################@ INÍCIO @ PopUP - Excluir Projetos Associados ############################################-->
                            <dxtv:ASPxCallback ID="callbackExcluirCheck" runat="server" OnCallback="callbackExcluirCheck_Callback1" ClientInstanceName="callbackExcluirCheck" OnLoad="callbackExcluirCheck_Load">
                                <ClientSideEvents EndCallback="function(s, e) {
gvExcluirCheck.Refresh(); pcExcluirCheck.Hide();

gvProjetos.Refresh();
window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3000); pcExcluirCheck.Hide(); gvProjetos.Refresh(); ASPxLoadingExcluirComboboxProjeto.Hide(); pcDadosProjeto.Show(); 
}" />
                            </dxtv:ASPxCallback>
                            <dxtv:ASPxCallback ID="callbackExcluirFechar" runat="server" OnCallback="callbackExcluirFechar_Callback1" ClientInstanceName="callbackExcluirFechar">
                            </dxtv:ASPxCallback>
                            <dxtv:ASPxPopupControl ID="pcExcluirCheck" runat="server" ClientInstanceName="pcExcluirCheck" Width="960px" AllowDragging="True" CloseAction="none" ShowCloseButton="False" HeaderText="Excluir Projetos Associados" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Text="Projetos Associados" Modal="True">
                                <ContentCollection>
                                    <dxtv:PopupControlContentControl runat="server">

                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxGridView ID="gvExcluirCheck" runat="server" ClientInstanceName="gvExcluirCheck" Width="100%" AutoGenerateColumns="False" KeyFieldName="CodigoProjeto" OnCustomCallback="gvExcluirCheck_CustomCallback">
                                                        <Settings VerticalScrollableHeight="275" VerticalScrollBarMode="Auto" />
                                                        <SettingsDataSecurity AllowDelete="False" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                                        <Columns>
                                                            <dxtv:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="40px" ShowSelectButton="True">
                                                            </dxtv:GridViewCommandColumn>
                                                            <dxtv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="1" Width="75%">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                            </dxtv:GridViewDataTextColumn>
                                                            <dxtv:GridViewDataTextColumn Caption="Status Projeto" FieldName="CodigoStatusProjeto" ShowInCustomizationForm="True" VisibleIndex="3" Width="25%">
                                                            </dxtv:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dxtv:ASPxGridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table class="formulario-botoes">
                                                        <tbody>
                                                            <tr>
                                                                <td class="formulario-botao">
                                                                    <dxtv:ASPxButton ID="btnExcluirCheck" runat="server" AutoPostBack="False" ClientInstanceName="btnExcluir1" Text="Excluir Projetos" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
   
            ASPxLoadingExcluirComboboxProjeto.Show(); callbackExcluirCheck.PerformCallback(); pcDadosProjeto.Show(); 
	    
}" />
                                                                    </dxtv:ASPxButton>
                                                                </td>

                                                                <td class="formulario-botao">
                                                                     <dxtv:ASPxButton ID="ASPxButton3" runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="100px" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {              
                                                                                                                             ASPxLoadingExcluirComboboxProjeto.Show(); 
                                                                                                                             pcExcluirCheck.Hide();
                                                                                                                             gvExcluirCheck.PerformCallback('limpar'); 
                                                                                                                             pcDadosProjeto.Show(); 
                                                                                                                             ASPxLoadingExcluirComboboxProjeto.Hide();

                                                                                                                }" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxtv:PopupControlContentControl>
                                </ContentCollection>
                            </dxtv:ASPxPopupControl>
                            <!--############################################@ FIM @ PopUP - Excluir Projetos Associados ############################################-->

                            <!--############################################@ INÍCIO @ PopUP - Incluir Carteira ############################################-->
                            <dxtv:ASPxPopupControl CloseAction="None" ID="pcDadosNovaCarteira" runat="server" AllowDragging="True" ClientInstanceName="pcDadosNovaCarteira" HeaderText="Incluir Carteira" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="960px" Modal="True">
                                <ClientSideEvents Closing="function(s, e) {
    gvProjetos.ClearFilter();
}"
                                    Shown="function(s, e) {
        gvProjetos.GotoPage(0);
}" />
                                <ContentStyle>
                                    <Paddings Padding="8px" />
                                </ContentStyle>
                                <ContentCollection>
                                    <dxtv:PopupControlContentControl runat="server">
                                        <!-- table -->
                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxLabel ID="lblCarteira0" runat="server" ClientInstanceName="lblCarteira" Text="Carteira:">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <!-- ASPxROUNDPANEL -->
                                                                    <dxtv:ASPxTextBox ID="txtNomeCarteiraADD" runat="server" ClientInstanceName="txtNomeCarteiraADD" MaxLength="150" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxtv:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Text="Descrição:">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxMemo ID="memDescricaoCarteiraADD" runat="server" ClientInstanceName="memDescricaoCarteiraADD" Height="60px" Width="100%" MaxLength="500">
                                                                        <DisabledStyle BackColor="#E0E0E0" Border-BorderColor="#E0E0E0" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxtv:ASPxMemo>
                                                                    <dxtv:ASPxLabel ID="lblContadorMemo0" runat="server" ClientInstanceName="lblContadorMemo" Font-Bold="True" ForeColor="#999999">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                    <dxtv:ASPxCheckBox ID="checkAtivoADD" runat="server" CheckState="Unchecked" ClientInstanceName="checkAtivoADD" Text="Carteira de Projetos Ativa?">
                                                    </dxtv:ASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table class="formulario-botoes">
                                                        <tbody>
                                                            <tr>
                                                                <td class="auto-style3">
                                                                    <dxtv:ASPxButton ID="btnSalvar0" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
         if (window.onClick_btnSalvar)
	     onClick_btnSalvar();
}" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                                <td class="formulario-botao" style="height: 27px">
                                                                    <dxtv:ASPxButton ID="btnCancelar0" runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       pcDadosNovaCarteira.Hide(); onClick_btnCancelar();
	
}" />

                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxtv:PopupControlContentControl>
                                </ContentCollection>
                            </dxtv:ASPxPopupControl>
                            <!--############################################@ FIM @ PopUP - Incluir Carteira ############################################-->
                            <dxcp:ASPxLoadingPanel ID="ASPxLoadingExcluirComboboxProjeto" ClientInstanceName="ASPxLoadingExcluirComboboxProjeto" runat="server"></dxcp:ASPxLoadingPanel>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
    var erroAoSalvar = hfGeral.Get(&quot;ErroSalvar&quot;);
    if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.carteirasDeProjetos_registro_inclu_do_com_sucesso, 'sucesso', false, false, null);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.carteirasDeProjetos_registro_alterado_com_sucesso_, 'sucesso', false, false, null);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.carteirasDeProjetos_registro_exclu_do_com_sucesso_, 'sucesso', false, false, null);
    else if(erroAoSalvar != '' &amp;&amp; erroAoSalvar != undefined)
	{
		window.top.mostraMensagem(traducao.carteirasDeProjetos_erro_ + erroAoSalvar, 'erro', true, false, null);
	}
    onClick_btnCancelar();

}"
                        Init="function(s, e) {
	if (hfGeral.Contains(&quot;Selecionados&quot;) == false)
        {
            hfGeral.Set(&quot;Selecionados&quot;, &quot;&quot;);
        }
}"></ClientSideEvents>
                    <Border BorderStyle="None" />
                </dxcp:ASPxCallbackPanel>
            </td>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style2 {
            height: 14px;
        }

        .auto-style3 {
            height: 27px;
        }
    </style>
</asp:Content>
