<%@ Page Language="C#" MasterPageFile="~/novaCDIS.master" AutoEventWireup="true" CodeFile="PainelMensagens.aspx.cs" Inherits="_Default"
    EnableViewState="False" EnableSessionState="ReadOnly" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="height: 26px; padding-left: 10px;" valign="middle">
                <table cellpadding="0" cellspacing="0" class="headerGrid">
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                                Text="Mensagens">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" style="padding-right: 10px">
                            <dxe:ASPxCheckBox ID="ckTodasEntidades" runat="server" AutoPostBack="True"
                                ClientInstanceName="ckTodasEntidades"
                                OnCheckedChanged="ckTodasEntidades_CheckedChanged"
                                Text="Mostrar Minhas Mensagens de Todas as Entidades" OnInit="ckTodasEntidades_Init">
                            </dxe:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dx:ASPxSplitter ID="LayoutSplitter" runat="server" FullscreenMode="True"
        Width="100%" Height="700px" Orientation="Vertical" SaveStateToCookies="True"
        EnableViewState="False">
        <Panes>
            <dx:SplitterPane AllowResize="False" ShowSeparatorImage="False">
                <Separator Visible="False"></Separator>
                <Panes>
                    <dx:SplitterPane MaxSize="500px" MinSize="150px"
                        ShowCollapseBackwardButton="True" Size="300px">
                        <Panes>
                            <dx:SplitterPane Name="SidePane" ScrollBars="Auto">
                                <PaneStyle>
                                    <BorderBottom BorderWidth="1px" />
                                    <Paddings Padding="0px"></Paddings>

                                    <BorderBottom BorderWidth="1px"></BorderBottom>
                                </PaneStyle>
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxcp:ASPxCallbackPanel ID="pnMenu" runat="server" ClientInstanceName="pnMenu"
                                            OnCallback="pnMenu_Callback"
                                            EnableViewState="False">
                                            <SettingsLoadingPanel Enabled="False"></SettingsLoadingPanel>
                                            <ClientSideEvents EndCallback="function(s, e) {
                var podeExcluir = s.cp_PodeExcluir;
                var mensagemExclusao = s.cp_MensagemExclusao;
                if(mensagemExclusao == &quot;&quot;)
                {
                              pcNovaPasta.Hide();
	               if(s.cp_SelecionaPastaMailDemo != '')
	              {
		            hfGeral.Set(&quot;CodigoPastaSelecionada&quot;, s.cp_SelecionaPasta);
		            ClientMailPanel.PerformCallback();
	               }

                }
                else
                {
                               e.processOnServer = false; 
                               if(podeExcluir == &quot;True&quot;)
                                {
                                            window.top.mostraMensagem(mensagemExclusao, 'sucesso', false, false, null);
                                 }
                                 else
                                {
                                            if(podeExcluir == &quot;False&quot;)
                                            {
                                                       window.top.mostraMensagem(mensagemExclusao, 'atencao', true, false, null);
                                             }
                               }
                }
                s.cp_PodeExcluir = &quot;&quot;;
                s.cp_MensagemExclusao = &quot;&quot;;
}"></ClientSideEvents>
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                    <dxtv:ASPxTreeView ID="MailTree" runat="server" AllowSelectNode="True"
                                                        ClientInstanceName="ClientMailTree"
                                                        EnableViewState="False">
                                                        <ClientSideEvents NodeClick="MailDemo.ClientMailTree_NodeClick" Init="treeView_OnInit"></ClientSideEvents>
                                                        <Styles>
                                                            <NodeImage>
                                                                <Paddings PaddingRight="5px"></Paddings>
                                                            </NodeImage>
                                                            <Disabled ForeColor="Black">
                                                            </Disabled>
                                                        </Styles>
                                                        <Nodes>
                                                            <dxtv:TreeViewNode AllowCheck="False" ClientEnabled="False" Expanded="True"
                                                                Name="nome" Text="Usuário">
                                                                <Image Height="25px" Width="25px">
                                                                    <SpriteProperties CssClass="Sprite_Person"></SpriteProperties>
                                                                </Image>
                                                                <ImageStyle>
                                                                    <Paddings Padding="0px" />
                                                                </ImageStyle>
                                                                <Nodes>
                                                                    <dxtv:TreeViewNode Expanded="True" Name="E" Text="<%$ Resources:traducao,caixa_de_entrada %>">
                                                                        <Image Height="22px" Width="22px">
                                                                            <SpriteProperties CssClass="Sprite_Inbox"></SpriteProperties>
                                                                        </Image>
                                                                    </dxtv:TreeViewNode>
                                                                    <dxtv:TreeViewNode Name="S" Text="<%$ Resources:traducao, itens_enviados %>">
                                                                        <Image Height="22px" Width="22px">
                                                                            <SpriteProperties CssClass="Sprite_SentItems"></SpriteProperties>
                                                                        </Image>
                                                                    </dxtv:TreeViewNode>
                                                                </Nodes>
                                                            </dxtv:TreeViewNode>
                                                        </Nodes>
                                                    </dxtv:ASPxTreeView>
                                                    <dxm:ASPxPopupMenu ID="popupMenuMover" runat="server"
                                                        ClientInstanceName="popupMenuMover" CloseAction="MouseOut">
                                                        <ClientSideEvents ItemClick="popupMenuMover_OnItemClick" PopUp="function(s, e) {
	var tipo = ClientMailGrid.cp_TipoPastaSelecionada;
	s.GetItemByName(&quot;E&quot;).SetVisible(tipo == 'E');
	s.GetItemByName(&quot;S&quot;).SetVisible(tipo == 'S');
	s.GetItemByName(&quot;categorizar&quot;).SetVisible(tipo == 'S');
}"></ClientSideEvents>
                                                        <Items>
                                                            <dxm:MenuItem Name="mover" Text="Mover Para">
                                                                <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Name="categorizar" Text="Categorizar">
                                                                <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                            </dxm:MenuItem>
                                                        </Items>
                                                        <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                        <DisabledStyle ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxm:ASPxPopupMenu>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxCallbackPanel>
                                        <dxm:ASPxPopupMenu ID="popupMenu" runat="server" ClientInstanceName="popupMenu"
                                            ItemSpacing="3px">
                                            <ClientSideEvents ItemClick="popupMenu_OnItemClick" PopUp="popupMenu_OnPopUp"></ClientSideEvents>
                                            <Items>
                                                <dxm:MenuItem Name="opcao" Text="Nova Pasta">
                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                    </Image>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="excluir" Text="Excluir Pasta">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </dxm:MenuItem>
                                            </Items>
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                        </dxm:ASPxPopupMenu>
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
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>
                        </Panes>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl3" runat="server">
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane Name="MainPane">
                        <PaneStyle>
                            <Paddings Padding="0px"></Paddings>

                            <border borderwidth="0px"></border>
                        </PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                                <dxm:ASPxMenu ID="MailMenu" runat="server" ClientInstanceName="ClientMailMenu"
                                    ItemAutoWidth="False"
                                    ShowAsToolbar="True" Width="100%">
                                    <ClientSideEvents ItemClick="MailDemo.ClientMailMenu_ItemClick"></ClientSideEvents>
                                    <Items>
                                        <dxm:MenuItem Name="compose" Text="Nova Mensagem">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Compose"></SpriteProperties>
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="reply" Text="Responder">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Reply"></SpriteProperties>
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="replyAll" Text="Responder a Todos">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_ReplyAll"></SpriteProperties>
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="fwd" Text="Encaminhar">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Forward"></SpriteProperties>
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="Print" Text="Imprimir">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Print"></SpriteProperties>
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="SearchBoxItem">
                                            <Template>
                                                <dxe:ASPxButtonEdit ID="SearchBox" runat="server"
                                                    ClientInstanceName="ClientSearchBox" CssClass="MailMenuSearchBox"
                                                    NullText="<%$ Resources:traducao, procurar %>" Width="250">
                                                    <ClientSideEvents KeyPress="MailDemo.ClientSearchBox_KeyPress"
                                                        TextChanged="MailDemo.ClientSearchBox_TextChanged" />
                                                    <Buttons>
                                                        <dxe:EditButton>
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Search"
                                                                    HottrackedCssClass="Sprite_SearchHover" PressedCssClass="Sprite_SearchHover" />
                                                            </Image>
                                                        </dxe:EditButton>
                                                    </Buttons>
                                                    <ButtonStyle CssClass="MailMenuSearchBoxButton" />
                                                </dxe:ASPxButtonEdit>
                                            </Template>
                                        </dxm:MenuItem>
                                    </Items>
                                    <BorderBottom BorderWidth="0px"></BorderBottom>
                                </dxm:ASPxMenu>
                                <dxcp:ASPxCallbackPanel ID="MailPanel" runat="server"
                                    ClientInstanceName="ClientMailPanel"
                                    OnCallback="MailPanel_Callback">
                                    <ClientSideEvents BeginCallback="function(s, e) {
	clearTimeout(timedOutMsg);
}"></ClientSideEvents>
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSplitter ID="MailSplitter" runat="server"
                                                ClientInstanceName="ClientMailSplitter" Orientation="Vertical"
                                                SaveStateToCookies="True" Width="100%" EnableViewState="False">
                                                <Panes>
                                                    <dx:SplitterPane Name="GridPane" AutoHeight="false">
                                                        <PaneStyle>
                                                            <Paddings Padding="0px"></Paddings>

                                                            <border borderwidth="0px"></border>
                                                        </PaneStyle>
                                                        <ContentCollection>
                                                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dxwgv:ASPxGridView ID="MailGrid" runat="server" AccessKey="G"
                                                                    AutoGenerateColumns="False" ClientInstanceName="ClientMailGrid"
                                                                    EnableRowsCache="False"
                                                                    KeyboardSupport="True" KeyFieldName="CodigoMensagem"
                                                                    OnCustomCallback="MailGrid_CustomCallback"
                                                                    OnCustomColumnDisplayText="MailGrid_CustomColumnDisplayText"
                                                                    OnCustomGroupDisplayText="MailGrid_CustomGroupDisplayText"
                                                                    OnHtmlRowCreated="MailGrid_HtmlRowCreated" Width="100%"
                                                                    EnableViewState="False"
                                                                    OnHtmlDataCellPrepared="MailGrid_HtmlDataCellPrepared">
                                                                    <ClientSideEvents BeginCallback="function(s, e) {
	clearTimeout(timedOutMsg);
}"
                                                                        ColumnStartDragging="function(s, e) {
	if (s.cp_Atualizar != 'N') {
		MailDemo.ClientMailGrid_EndCallback(s,e)
	}
}"
                                                                        ContextMenu="function(s, e) {
	if(e.objectType == 'row')
	{ 
		 hfGeral.Set('CodigoMensagemMover', s.GetRowKey(e.index));
         popupMenuMover.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
	}
}"
                                                                        EndCallback="MailDemo.ClientMailGrid_EndCallback" FocusedRowChanged="MailDemo.ClientMailGrid_FocusedRowChanged"
                                                                        Init="MailDemo.ClientMailGrid_Init" />
                                                                    <Columns>
                                                                        <dxtv:GridViewCommandColumn ShowClearFilterButton="True" Caption=" " ShowInCustomizationForm="True" VisibleIndex="0" Width="5%">
                                                                        </dxtv:GridViewCommandColumn>
                                                                        <dxwgv:GridViewDataColumn Caption="De" FieldName="NomeUsuario"
                                                                            ShowInCustomizationForm="True" VisibleIndex="2" Width="25%">
                                                                        </dxwgv:GridViewDataColumn>
                                                                        <dxwgv:GridViewDataColumn Caption="Assunto" FieldName="Assunto" MinWidth="450"
                                                                            ShowInCustomizationForm="True" VisibleIndex="3" Width="25%">
                                                                        </dxwgv:GridViewDataColumn>
                                                                        <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="DataInclusao" ShowInCustomizationForm="True" VisibleIndex="4" Width="20%" SortIndex="0" SortOrder="Ascending">
                                                                            <PropertiesDateEdit DisplayFormatString="g">
                                                                            </PropertiesDateEdit>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                        </dxwgv:GridViewDataDateColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Mensagem"
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Lida" ShowInCustomizationForm="True"
                                                                            Visible="False" VisibleIndex="10">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaTipoMensagem"
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn Caption=" " FieldName="Prioridade"
                                                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="5%">
                                                                            <Settings AllowGroup="False" AllowHeaderFilter="False"
                                                                                FilterMode="DisplayText" AllowAutoFilter="False" AllowDragDrop="False"
                                                                                AllowSort="False" />
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
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn Caption="Categoria" FieldName="DescricaoCategoria"
                                                                            ShowInCustomizationForm="True" VisibleIndex="5" MinWidth="10" Width="15%">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn Caption=" " FieldName="TipoAssociacao"
                                                                            ShowInCustomizationForm="True" VisibleIndex="6" Width="5%">
                                                                            <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/botoes/{0}.png'  /&gt;">
                                                                            </PropertiesTextEdit>
                                                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False"
                                                                                AllowSort="False" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado"
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeObjeto"
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoCategoria"
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <SettingsBehavior AllowClientEventsOnLoad="False" AllowFocusedRow="True"
                                                                        AutoExpandAllGroups="True" EnableRowHotTrack="True" />
                                                                    <SettingsResizing  ColumnResizeMode="NextColumn"/>
                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                    </SettingsPager>
                                                                    <Settings GridLines="Vertical" ShowGroupedColumns="True" ShowGroupPanel="True"
                                                                        VerticalScrollBarMode="Visible" VerticalScrollableHeight="50"
                                                                        ShowStatusBar="Visible" ShowHeaderFilterBlankItems="False" ShowFilterRow="True" HorizontalScrollBarMode="Auto" />
                                                                    <SettingsLoadingPanel Mode="Disabled" />

                                                                    <Styles>
                                                                        <Row Cursor="pointer">
                                                                        </Row>
                                                                        <Footer>
                                                                            <Paddings Padding="0px" />
                                                                        </Footer>
                                                                    </Styles>
                                                                    <Templates>
                                                                        <StatusBar>
                                                                            <table>
                                                                            </table>
                                                                        </StatusBar>
                                                                    </Templates>
                                                                </dxwgv:ASPxGridView>
                                                            </dx:SplitterContentControl>
                                                        </ContentCollection>
                                                    </dx:SplitterPane>
                                                    <dx:SplitterPane Name="MessagePane" ScrollBars="Auto" AutoHeight="false">
                                                        <PaneStyle>
                                                            <Paddings Padding="0px"></Paddings>

                                                            <border borderwidth="0px"></border>
                                                        </PaneStyle>
                                                        <ContentCollection>
                                                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dxcp:ASPxCallbackPanel ID="MailMessagePanel" runat="server"
                                                                    ClientInstanceName="ClientMailMessagePanel"
                                                                    OnCallback="MailMessagePanel_Callback">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
    
	timedOutMsg = setTimeout('lerMsg();', 5000);
}"
                                                                        Init="function(s, e) {
	timedOutMsg = setTimeout('lerMsg();', 5000);
}" />

                                                                    <PanelCollection>
                                                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                            <div id="divPrint"><%=FormatCurrentMessage() %></div>
                                                                        </dxp:PanelContent>
                                                                    </PanelCollection>
                                                                </dxcp:ASPxCallbackPanel>
                                                            </dx:SplitterContentControl>
                                                        </ContentCollection>
                                                    </dx:SplitterPane>
                                                </Panes>
                                                <ClientSideEvents PaneResized="MailDemo.ClientMailSplitter_PaneResized"></ClientSideEvents>
                                            </dx:ASPxSplitter>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <Separator Visible="False" />
                <PaneStyle>
                    <BorderBottom BorderWidth="0px"></BorderBottom>
                </PaneStyle>
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl6" runat="server" SupportsDisabledAttribute="True"></dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
    <dxpc:ASPxPopupControl ID="MailEditorPopup"
        runat="server" AllowDragging="True"
        ClientInstanceName="ClientMailEditorPopup" CloseAction="CloseButton"
        PopupAnimationType="None"
        HeaderText="Mensagem" Modal="True"
        OnWindowCallback="MailEditorPopup_WindowCallback"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowFooter="True" Width="1000px">
        <ContentStyle>
            <Paddings Padding="0px" PaddingLeft="2px" PaddingRight="2px" />
        </ContentStyle>
        <FooterTemplate>
            <dxe:ASPxButton ID="MailCancelButton" runat="server" AutoPostBack="False"
                CssClass="MailPopupButton" Text="<%$ Resources:traducao, fechar %>"
                Width="100px" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                <ClientSideEvents Click="MailDemo.ClientMailCancelButton_Click" />
                <Paddings Padding="0px" />
            </dxe:ASPxButton>
            <dxe:ASPxButton ID="MailSendButton" runat="server" AutoPostBack="False"
                ClientInstanceName="ClientMailSendButton" CssClass="MailPopupButton"
                Text="<%$ Resources:traducao,enviar %>" Width="100px" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                <ClientSideEvents Click="MailDemo.ClientMailSendButton_Click" />
                <Paddings Padding="0px" />
            </dxe:ASPxButton>
            <div class="clear">
            </div>
        </FooterTemplate>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <table cellpadding="0" cellspacing="0" class="headerGrid">
                    <tr>
                        <td style="padding-top: 8px; padding-bottom: 3px">
                            <dxe:ASPxButton ID="btnPara" runat="server" AutoPostBack="False"
                                ClientInstanceName="btnPara"
                                Text="Para..." Width="65px">
                                <ClientSideEvents Click="function(s, e) {
	abreDestinatarios();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxMemo ID="txtDestinatarios" runat="server"
                                ClientInstanceName="txtDestinatarios"
                                Rows="5" Width="100%" ClientEnabled="False">
                                <ClientSideEvents KeyDown="function(s, e) {                                                                       
	if(e.htmlEvent.keyCode != 46 &amp;&amp; e.htmlEvent.keyCode != 8)
		ASPxClientUtils.PreventEvent(e.htmlEvent);
}" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="#333333">
                                </DisabledStyle>
                            </dxe:ASPxMemo>
                            <dxhf:ASPxHiddenField ID="hfDestinatarios" runat="server"
                                ClientInstanceName="hfDestinatarios">
                            </dxhf:ASPxHiddenField>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                            <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                        Text="Assunto:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 200px">
                                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                        Text="Categoria:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px">
                                                    <dxe:ASPxTextBox ID="txtAssunto" runat="server" ClientInstanceName="txtAssunto"
                                                        Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td style="width: 200px">
                                                    <dxe:ASPxComboBox ID="ddlCategoria" runat="server"
                                                        ClientInstanceName="ddlCategoria"
                                                        IncrementalFilteringMode="Contains" Width="100%">
                                                    </dxe:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 140px; vertical-align: bottom; padding-left: 3px !important;">
                                        <table cellpadding="0" cellspacing="0" class="headerGrid" style="width: 100%; margin-bottom: 2px !important;">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton ID="btnAltaPrioridade" runat="server" CssClass="spacingBTNChek"
                                                        Text="Alta Prioridade" Width="100%" AutoPostBack="False"
                                                        ClientInstanceName="btnAltaPrioridade" HorizontalAlign="Left"
                                                        ImageSpacing="4px" CausesValidation="False" GroupName="Prioridade">
                                                        <Image IconID="iconBtnAltaPrioridade" SpriteProperties-CssClass="teste123" Url="~/imagens/vazio.PNG">
                                                        </Image>
                                                        <Paddings Padding="0px" />
                                                        <BorderBottom BorderStyle="None" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                            <script type="text/javascript">
                                                function verificaMarcacaoAlta() {
                                                    if (marcacaoAlta == 'S') {
                                                        btnAltaPrioridade.SetChecked(false);
                                                        $('#btnAltaPrioridadeImg').attr("src", "../imagens/vazio.PNG");
                                                        marcacaoAlta = 'N';
                                                    }
                                                    else {
                                                        $('#btnAltaPrioridadeImg').attr("src", "../imagens/selecionado.PNG");
                                                        marcacaoAlta = 'S';
                                                        btnAltaPrioridade.SetChecked(true);
                                                    }
                                                }
                                            </script>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <dxhe:ASPxHtmlEditor ID="MailEditor" runat="server"
                                ClientInstanceName="ClientMailEditor"
                                Height="300px" Width="100%">
                                <ClientSideEvents Init="MailDemo.ClientMailEditor_Init" />
                                <StylesToolbars>
                                    <Toolbar>
                                        <Paddings Padding="0px" />
                                    </Toolbar>
                                    <ToolbarItem>
                                        <Paddings Padding="4px" />
                                    </ToolbarItem>
                                </StylesToolbars>
                                <Toolbars>
                                    <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
                                        <Items>
                                            <dxhe:ToolbarCutButton>
                                            </dxhe:ToolbarCutButton>
                                            <dxhe:ToolbarCopyButton>
                                            </dxhe:ToolbarCopyButton>
                                            <dxhe:ToolbarPasteButton>
                                            </dxhe:ToolbarPasteButton>
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
                                            <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                <Items>
                                                    <dxhe:ToolbarInsertTableDialogButton BeginGroup="True">
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
                                            <dxhe:ToolbarBackColorButton BeginGroup="True">
                                            </dxhe:ToolbarBackColorButton>
                                            <dxhe:ToolbarFontColorButton>
                                            </dxhe:ToolbarFontColorButton>
                                            <dxhe:ToolbarFullscreenButton>
                                            </dxhe:ToolbarFullscreenButton>
                                        </Items>
                                    </dxhe:HtmlEditorToolbar>

                                </Toolbars>
                                <Settings AllowHtmlView="False" AllowPreview="False"
                                    AllowInsertDirectImageUrls="False" />
                                <SettingsHtmlEditing EnterMode="BR" />
                                <Border BorderWidth="0px" />
                            </dxhe:ASPxHtmlEditor>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcNovaPasta" runat="server"
        ClientInstanceName="pcNovaPasta"
        HeaderText="Nova Pasta" Modal="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" Width="700px">
        <ClientSideEvents Closing="function(s, e) {
	hfGeral.Set('IndicaEntradaSaida', '');
    txtNomePasta.SetText('');
}"
            Shown="function(s, e) {
	txtNomePasta.SetFocus();
}" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                Text="Nome da Pasta:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxTextBox ID="txtNomePasta" runat="server"
                                ClientInstanceName="txtNomePasta"
                                MaxLength="100" Width="100%">
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                            ClientInstanceName="btnSalvar"
                                            Text="Salvar" Width="90px">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(txtNomePasta.GetText() == '')
		window.top.mostraMensagem(traducao.PainelMensagens_a_pasta_deve_possuir_um_nome_, 'Atencao', true, false, null);
	else
	    pnMenu.PerformCallback(hfGeral.Get('IndicaEntradaSaida'));
        }" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td></td>
                                    <td>
                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                            ClientInstanceName="btnFechar"
                                            Text="Fechar" Width="90px">
                                            <ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
						pcNovaPasta.Hide();
                    }" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <iframe id="frmPrint" name="frmPrint" style="width: 100%;" src="../printText.aspx"></iframe>
</asp:Content>
