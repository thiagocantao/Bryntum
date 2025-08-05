<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MensagensPainelBordo.aspx.cs" Inherits="_Default"
    EnableViewState="False" EnableSessionState="ReadOnly" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <link href="../estilos/custom.css" rel="stylesheet" />
    <title>Mensagens</title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .MailMenuSearchBox {
            height: 30px;
        }

        @media (max-height: 768px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 350px;
            }
            .rolagem-tab-email {
                overflow-y: auto;
                height: 300px;
            }
        }

        @media (min-height: 769px) and (max-height: 800px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 350px;
            }
            .rolagem-tab-email {
                overflow-y: auto;
                height: 300px;
            }
        }

        @media (min-height: 801px) and (max-height: 960px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 400px;
            }
            .rolagem-tab-email {
                overflow-y: auto;
                height: 350px;
            }
        }

        @media (min-height: 961px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 550px;
            }
            .rolagem-tab-email {
                overflow-y: auto;
                height: 500px;
            }
        }
    </style>
    <script type="text/javascript">
        var myObject = null;
        var posExecutar = null;
        var urlModal = "";
        var retornoModal = null;
        function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
            if (parseInt(sHeight) < 535)
                sHeight = parseInt(sHeight) + 20;
            sHeight = 400;

            myObject = objParam;
            posExecutar = sFuncaoPosModal != "" ? sFuncaoPosModal : null;
            objFrmModal = document.getElementById('frmModal');

            pcModal.SetWidth(sWidth);
            objFrmModal.style.width = "100%";
            objFrmModal.style.height = sHeight + "px";
            urlModal = sUrl;
            //setTimeout ('alteraUrlModal();', 0);            
            pcModal.SetHeaderText(sHeaderTitulo);
            pcModal.Show();

        }

        function fechaModal() {
            pcModal.Hide();
        }

        function resetaModal() {
            objFrmModal = document.getElementById('frmModal');
            posExecutar = null;
            objFrmModal.src = "";
            pcModal.SetHeaderText("");
            retornoModal = null;
        }

    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <table id="tbTitulo" runat="server" border="0" cellpadding="0" cellspacing="0" style=" width: 100%">
            <tr style="height: 26px">
                <td align="left" style="padding-left: 10px;">
       <%--             <asp:Label ID="lblTituloTela" runat="server"
                        Text="Mensagens"></asp:Label>--%></td>
            </tr>
        </table>
        <div style="position: fixed !important; top : 0px;">
            <dxm:ASPxMenu ID="MailMenu" runat="server" ClientInstanceName="ClientMailMenu"
                ItemAutoWidth="False"
                ShowAsToolbar="True" Width="100%">
                <ClientSideEvents ItemClick="MailDemo.ClientMailMenu_ItemClick" />
                <Items>
                    <dxm:MenuItem Name="ler" Text="Ler">
                        <Image>
                            <SpriteProperties CssClass="Sprite_BuyNow" />
                        </Image>
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="compose" Text="Nova Mensagem">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Compose" />
                        </Image>
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="reply" Text="Responder">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Reply" />
                        </Image>
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="replyAll" Text="Responder a Todos">
                        <Image>
                            <SpriteProperties CssClass="Sprite_ReplyAll" />
                        </Image>
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="fwd" Text="Encaminhar">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Forward" />
                        </Image>
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Print" Text="Imprimir">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Print" />
                        </Image>
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="SearchBoxItem">
                        <Template>
                            <dxe:ASPxButtonEdit ID="SearchBox" runat="server"
                                ClientInstanceName="ClientSearchBox" CssClass="MailMenuSearchBox"
                                 NullText="<%$ Resources:traducao, procurar %>"  Width="250">
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
                    <dxm:MenuItem Name="ckMostrarApenasNaoLidosItem">
                        <Template>
                            <dxe:ASPxCheckBox ID="ckMostrarApenasNaoLidos" runat="server" Checked="True"
                                CheckState="Checked" ClientInstanceName="ckMostrarApenasNaoLidos"
                                Text="<%# Resources.traducao.mostrar_apenas_n_o_lidas %>">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	 ClientMailPanel.PerformCallback();
}" />
                            </dxe:ASPxCheckBox>
                        </Template>
                    </dxm:MenuItem>
                </Items>
                <BorderBottom BorderWidth="0px" />
            </dxm:ASPxMenu>
            <dxcp:ASPxCallbackPanel ID="MailPanel" runat="server"
                ClientInstanceName="ClientMailPanel" Width="100%"
                OnCallback="MailPanel_Callback">
                <ClientSideEvents BeginCallback="function(s, e) {                                            
   clearTimeout(timedOutMsg);
}" />
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
 
                        <dxwgv:ASPxGridView ID="MailGrid" runat="server" AccessKey="G"
                            AutoGenerateColumns="False" ClientInstanceName="ClientMailGrid"
                            EnableRowsCache="False" EnableViewState="False"
                            KeyboardSupport="True" KeyFieldName="CodigoMensagem"
                            OnCustomCallback="MailGrid_CustomCallback"
                            OnCustomGroupDisplayText="MailGrid_CustomGroupDisplayText"
                            OnHtmlDataCellPrepared="MailGrid_HtmlDataCellPrepared" Width="100%"
                            OnHtmlRowCreated="MailGrid_HtmlRowCreated">
                            <ClientSideEvents BeginCallback="function(s, e) {
	clearTimeout(timedOutMsg);
}"
                                ColumnStartDragging="function(s, e) {
	if (s.cp_Atualizar != 'N') {
		MailDemo.ClientMailGrid_EndCallback(s,e)
	}
}"
                                EndCallback="MailDemo.ClientMailGrid_EndCallback" FocusedRowChanged="MailDemo.ClientMailGrid_FocusedRowChanged"
                                Init="MailDemo.ClientMailGrid_Init" />
                            <ClientSideEvents FocusedRowChanged="MailDemo.ClientMailGrid_FocusedRowChanged" EndCallback="MailDemo.ClientMailGrid_EndCallback"
                                Init="MailDemo.ClientMailGrid_Init"></ClientSideEvents>
                            <Columns>
                                <dxwgv:GridViewDataColumn Caption="De" FieldName="NomeUsuario"
                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="20%">
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn Caption="Assunto" FieldName="Assunto"
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="20%">
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Categoria"
                                    FieldName="DescricaoCategoria" ShowInCustomizationForm="True" VisibleIndex="3"
                                    Width="20%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="DataInclusao"
                                    GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0"
                                    SortOrder="Descending" VisibleIndex="4" Width="20%">
                                    <PropertiesDateEdit DisplayFormatString="g">
                                    </PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Mensagem"
                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Lida" ShowInCustomizationForm="True"
                                    Visible="False" VisibleIndex="9">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="IndicaTipoMensagem"
                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="Prioridade"
                                    ShowInCustomizationForm="True" VisibleIndex="0" Width="25px">
                                    <Settings AllowGroup="False" AllowHeaderFilter="False"
                                        FilterMode="DisplayText" AllowAutoFilter="False" />
                                    <Settings FilterMode="DisplayText" AllowHeaderFilter="False" AllowGroup="False"></Settings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="TipoAssociacao"
                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="40px">
                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/botoes/{0}.png'  /&gt;">
                                    </PropertiesTextEdit>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado"
                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="NomeObjeto"
                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="CodigoCategoria"
                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowClientEventsOnLoad="False" AllowFocusedRow="True"
                                AutoExpandAllGroups="True" EnableRowHotTrack="True" />

                            <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" EnableRowHotTrack="True" AllowClientEventsOnLoad="False"></SettingsBehavior>
                            <SettingsResizing  ColumnResizeMode="NextColumn" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings GridLines="Vertical" ShowGroupedColumns="True" ShowGroupPanel="True"
                                ShowHeaderFilterBlankItems="False"
                                VerticalScrollBarMode="Visible" VerticalScrollableHeight="50"
                                ShowFooter="True" />
                            <SettingsLoadingPanel Mode="Disabled" />

                            <Settings ShowHeaderFilterBlankItems="False" ShowGroupPanel="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="50" ShowStatusBar="Visible" ShowGroupedColumns="True" GridLines="Vertical"></Settings>

                            <SettingsLoadingPanel Mode="Disabled"></SettingsLoadingPanel>

                            <Styles>
                                <Row Cursor="pointer">
                                </Row>
                                <Footer>
                                    <Paddings Padding="0px" />
                                    <Paddings Padding="0px"></Paddings>
                                </Footer>
                            </Styles>
                            <Templates>
                                <FooterRow>
                                    <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="grid-legendas-icone">
                                                    <dxe:ASPxImage ID="ASPxImage1" runat="server"
                                                        ImageUrl="~/imagens/botoes/PR.PNG" Height="16px">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td class="grid-legendas-label grid-legendas-label-icone">
                                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                        Text="<%# Resources.traducao.MensagensPainelBordo_projeto %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td class="grid-legendas-icone">
                                                    <dxe:ASPxImage ID="ASPxImage2" runat="server"
                                                        ImageUrl="~/imagens/botoes/IN.png" Height="16px">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td class="grid-legendas-label grid-legendas-label-icone">
                                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                        Text="<%# Resources.traducao.MensagensPainelBordo_indicador %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td class="grid-legendas-icone">
                                                    <dxe:ASPxImage ID="ASPxImage3" runat="server"
                                                        ImageUrl="~/imagens/botoes/OB.png" Height="16px">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td class="grid-legendas-label grid-legendas-label-icone">
                                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                        Text="<%# Resources.traducao.MensagensPainelBordo_objetivo_estrat_gico %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td class="grid-legendas-icone">
                                                    <dxe:ASPxImage ID="ASPxImage4" runat="server"
                                                        ImageUrl="~/imagens/botoes/EN.png" Height="16px">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td class="grid-legendas-label grid-legendas-label-icone">
                                                    <span><%=definicaoEntidade %></span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </FooterRow>
                            </Templates>
                        </dxwgv:ASPxGridView>

                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>

            <dxpc:ASPxPopupControl ID="MailEditorPopup"
                runat="server" AllowDragging="True"
                ClientInstanceName="ClientMailEditorPopup" CloseAction="CloseButton"
                PopupAnimationType="None"
                HeaderText="Mensagem" Modal="True"
                OnWindowCallback="MailEditorPopup_WindowCallback"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ShowFooter="True" PopupVerticalOffset="-20" Width="700px">
                <ClientSideEvents Closing="function(s, e) {
	lerMsg();
}" />
                <ContentStyle>
                    <Paddings Padding="0px" PaddingLeft="2px" PaddingRight="2px" />
                </ContentStyle>
                <FooterTemplate>
                    <table align="right" class="formulario-botoes" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="formulario-botao">
                                    <dxe:ASPxButton ID="MailSendButton" runat="server" AutoPostBack="False"
                                        ClientInstanceName="ClientMailSendButton"
                                        Text="<%$ Resources:traducao, enviar%>"
                                        CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                        Width="100px">
                                        <ClientSideEvents Click="MailDemo.ClientMailSendButton_Click" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                                <td class="formulario-botao">
                                    <dxe:ASPxButton ID="MailCancelButton" runat="server" AutoPostBack="False"
                                         CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                        Text="<%$ Resources:traducao, fechar%>"
                                        Width="100px">
                                        <ClientSideEvents Click="MailDemo.ClientMailCancelButton_Click" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="clear">
                    </div>
                </FooterTemplate>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl100" runat="server" SupportsDisabledAttribute="True">
                        <div class="rolagem-tab">
                            <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                <tr>
                                    <td style="padding-top: 8px; padding-bottom: 3px">
                                        <dxe:ASPxButton ID="btnPara" runat="server" AutoPostBack="False"
                                            ClientInstanceName="btnPara"
                                            Text="Para..." Width="65px">
                                            <ClientSideEvents Click="function(s, e) {
	abreDestinatariosPopUp();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxMemo ID="txtDestinatarios" runat="server"
                                            ClientInstanceName="txtDestinatarios"
                                            Rows="2" Width="100%" ClientEnabled="False">
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
                                    <td>
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
                                                            <td style="padding:2px 5px 0px 0px;">
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
                                                <td style="padding-left: 5px; width: 140px; padding-top: 12px;">
                                                    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton ID="btnAltaPrioridade" runat="server" AutoPostBack="False"
                                                                    CausesValidation="False" ClientInstanceName="btnAltaPrioridade"
                                                                    GroupName="Prioridade"
                                                                    HorizontalAlign="Left" ImageSpacing="3px" Text="Alta Prioridade" Width="100%">
                                                                    <Image Url="~/imagens/vazio.PNG">
                                                                    </Image>
                                                                    <Paddings Padding="0px" />
                                                                    <BorderBottom BorderStyle="None" />
                                                                    <ClientSideEvents Click="                                                                     
                                                                     function(s, e)
                                                                     {
                                                                        if (marcacaoAlta == 'S') 
                                                                        {
                                                                              s.SetImageUrl('../imagens/vazio.PNG');
                                                                              marcacaoAlta = 'N';
                                                                              s.SetChecked(false);
                                                                        }
                                                                        else 
                                                                        {
                                                                              s.SetImageUrl('../imagens/selecionado.PNG');
                                                                              marcacaoAlta = 'S';
                                                                              s.SetChecked(true);
                                                                        }
                                                                     }" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 4px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxhe:ASPxHtmlEditor ID="MailEditor" runat="server"
                                            ClientInstanceName="ClientMailEditor"
                                            Width="100%">
                                            <ClientSideEvents Init="MailDemo.ClientMailEditor_Init" />

                                            <SettingsDialogs>
                                                <InsertImageDialog>
                                                    <SettingsImageUpload UploadFolder=""></SettingsImageUpload>
                                                </InsertImageDialog>
                                            </SettingsDialogs>

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
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="ASPxPopupControl1" runat="server"
                HeaderText="Mensagem"
                ClientInstanceName="pnMSG" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ShowFooter="True">
                <ClientSideEvents Closing="function(s, e) {
	lerMsg();
}" />
                <FooterStyle HorizontalAlign="Right">
                    <Paddings Padding="3px" />
                </FooterStyle>
                <FooterTemplate>
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td align="right">
                                <table class="formulario-botoes" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                                                    Text="Fechar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	pnMSG.Hide();
}" />
                                                    <Paddings Padding="0px" />
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
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxcp:ASPxCallbackPanel ID="MailMessagePanel" runat="server"
                            ClientInstanceName="ClientMailMessagePanel" Width="800px">
                            <ClientSideEvents EndCallback="function(s, e) {
    
	
}"
                                Init="function(s, e) {
	
}" />
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <div class="rolagem-tab-email" id="divPrint" style="width: 100%;"><%=FormatCurrentMessage() %></div>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
            </dxhf:ASPxHiddenField>

            <%--<div style="z-index: 100000;"></div>--%>

            <dxpc:ASPxPopupControl ID="pcModal" runat="server" SizeGripRtlImage-SpriteProperties-CssClass ClientInstanceName="pcModal"
                HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                        <iframe id="frmModal" name="frmModal" frameborder="0" style="overflow: auto; padding: 0px; margin: 0px;"></iframe>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <ClientSideEvents CloseUp="function(s, e) {
            var retorno = '';
            
            if(retornoModal != null)
            {   
                retorno = retornoModal;
            }
            
            if(posExecutar != null)
               posExecutar(retorno);
                
            resetaModal();
}"
                    PopUp="function(s, e) {
    window.document.getElementById('frmModal').dialogArguments = myObject;
	document.getElementById('frmModal').src = urlModal;
}" />
                <ContentStyle>
                    <Paddings Padding="5px" />
                </ContentStyle>
            </dxpc:ASPxPopupControl>

            <iframe id="frmPrint" name="frmPrint" style="width: 99%; display: none" src="../printText.aspx"></iframe>
        </div>
    </form>
</body>
</html>
