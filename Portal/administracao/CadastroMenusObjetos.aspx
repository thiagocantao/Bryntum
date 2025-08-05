<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroMenusObjetos.aspx.cs" Inherits="administracao_CadastroMenusObjetos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <!-- TABLA CONTEUDO -->
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="left" valign="middle" style="padding-left: 10px; width: 80%;">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Menus de Projetos/Processos/Programas">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="center" style="width: 20%; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>

                        <td align="right" valign="middle">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="Tipo:"
                                            ID="ASPxLabel2">
                                        </dxe:ASPxLabel>


                                    </td>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlTipoProjeto" runat="server" ClientInstanceName="ddlTipoProjeto"
                                            Width="350px" TextField="TipoProjeto"
                                            ValueField="CodigoTipoProjeto">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.SetFocusedRowIndex(-1);
	ddlTipoProjetoSelectedIndexChanged();

}" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>


        <tr>
            <td></td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                KeyFieldName="CodigoGrupoMenuTipoProjeto" AutoGenerateColumns="False" Width="100%"
                                ID="gvDados"
                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s,true);
}"
                                    CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
		pcItemsMenus.Hide();
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		pcItemsMenus.Hide();
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		pcItemsMenus.Hide();
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		pcItemsMenus.Hide();
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
     else if(e.buttonID == &quot;btnNovoMenu&quot;)
     {        
		pcDados.Hide();
		pcItemsMenus.Show();        
     }
}
"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="95px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnNovoMenu"
                                                Text="Alterar Opções de Menu">
                                                <Image Url="../imagens/botoes/NovoMenu.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
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
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoGrupoMenuTipoProjeto" Name="DescricaoGrupoMenuTipoProjeto"
                                        Caption="Grupo" VisibleIndex="1">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Ordem" ShowInCustomizationForm="True"
                                        VisibleIndex="2" Width="150px" FieldName="SequenciaGrupoMenuTipoProjeto">
                                        <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                            AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Ativo" ShowInCustomizationForm="True"
                                        VisibleIndex="4" Width="70px" FieldName="IndicaGrupoVisivel"
                                        Visible="False">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="CodigoGrupoMenuTipoProjeto"
                                        FieldName="CodigoGrupoMenuTipoProjeto" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IniciaisGrupoMenu"
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>

                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                                <Settings VerticalScrollBarMode="Visible" ShowFooter="True"></Settings>

                                <SettingsText></SettingsText>
                                <Templates>
                                    <FooterRow>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="width: 16px; height: 16px; border-radius: 50%; background-color: #914800">&nbsp;
                                                    </td>
                                                    <td style="padding-left: 3px; padding-right: 10px">
                                                        <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server"
                                                            ClientInstanceName="lblDescricaoNaoAtiva" Font-Bold="False"
                                                            Text="Grupos Inativos.">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="400px"
                                ID="pcDados">
                                <ClientSideEvents CloseUp="function(s, e) {
	ddlTipoProjeto.SetEnabled(true);
}"
                                    PopUp="function(s, e) {
	ddlTipoProjeto.SetEnabled(false);
}" />
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>

                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table>
                                            <tr>
                                                <td style="height: 15px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                    Text="Grupo:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 60px">
                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                    Text="Ordem:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 55px">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-right: 10px">
                                                                <dxe:ASPxTextBox ID="txtGrupo" runat="server" ClientInstanceName="txtGrupo"
                                                                    Height="16px" MaxLength="25"
                                                                    Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td style="padding-right: 10px; width: 60px;">
                                                                <dxe:ASPxSpinEdit ID="txtOrdem" runat="server" ClientInstanceName="txtOrdem"
                                                                    MaxValue="100" MinValue="1" Number="0"
                                                                    NumberType="Integer" Width="100%">
                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                    </SpinButtons>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxSpinEdit>
                                                            </td>
                                                            <td style="width: 55px">
                                                                <dxe:ASPxCheckBox ID="ckGrupoAtivo" runat="server" CheckState="Unchecked"
                                                                    ClientInstanceName="ckGrupoAtivo"
                                                                    Text="Ativo" Width="100%">
                                                                    <DisabledStyle ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                        ClientInstanceName="btnSalvar" Text="Salvar" Width="90px"
                                                                        ID="btnSalvar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                                        <Paddings Padding="0px"></Paddings>
                                                                    </dxe:ASPxButton>

                                                                </td>
                                                                <td style="width: 10px"></td>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
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
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxpc:ASPxPopupControl ID="pcItemsMenus" runat="server"
                                ClientInstanceName="pcItemsMenus" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" Width="840px" AllowDragging="True"
                                HeaderText="Opções de Menu" CloseAction="CloseButton">
                                <ClientSideEvents CloseUp="function(s, e) {
		ddlTipoProjeto.SetEnabled(true);
}"
                                    PopUp="function(s, e) {
	ddlTipoProjeto.SetEnabled(false);
}" />
                                <ContentStyle>
                                    <Paddings PaddingBottom="10px" />
                                </ContentStyle>
                                <FooterStyle>
                                    <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                        PaddingTop="0px" />
                                </FooterStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server"
                                        SupportsDisabledAttribute="True">
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    <dxwgv:ASPxGridView ID="gvMenuTipoProjeto" runat="server"
                                                        AutoGenerateColumns="False" ClientInstanceName="gvMenuTipoProjeto"
                                                        KeyFieldName="CodigoItemMenu"
                                                        OnAfterPerformCallback="gvMenuTipoProjeto_AfterPerformCallback"
                                                        Width="100%"
                                                        OnCellEditorInitialize="gvMenuTipoProjeto_CellEditorInitialize"
                                                        OnRowInserting="gvMenuTipoProjeto_RowInserting"
                                                        OnRowUpdating="gvMenuTipoProjeto_RowUpdating"
                                                        OnRowDeleting="gvMenuTipoProjeto_RowDeleting"
                                                        OnCustomCallback="gvMenuTipoProjeto_CustomCallback"
                                                        OnCommandButtonInitialize="gvMenuTipoProjeto_CommandButtonInitialize" OnCustomJSProperties="gvMenuTipoProjeto_CustomJSProperties">
                                                        <ClientSideEvents CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		//onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
     else if(e.buttonID == &quot;btnNovoMenu&quot;)
     {		
		pcItemsMenus.Show();
     }
}
"
                                                            FocusedRowChanged="function(s, e) {
	//OnGridFocusedRowChanged(s);
}"
                                                            BeginCallback="function(s, e) {
	hfGeral.Set('command', e.command);
}"
                                                            EndCallback="function(s, e) {
	if(hfGeral.Get('command') == 'UPDATEEDIT'){
		if(gvMenuTipoProjeto.cp_msgErro){
			window.top.mostraMensagem(gvMenuTipoProjeto.cp_msgErro, 'Atencao', true, false, null);     
        }
	}
}" />
                                                        <Columns>
                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" Width="70px" Caption=" " ShowEditButton="true" ShowDeleteButton="true">
                                                                <HeaderTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <dxm:ASPxMenu ID="menu1" runat="server" BackColor="Transparent"
                                                                                    ClientInstanceName="menu1"
                                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                                                    OnInit="menu_Init1">
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
                                                            <dxwgv:GridViewDataTextColumn
                                                                FieldName="CodigoGrupoMenu" ShowInCustomizationForm="True" Visible="False"
                                                                VisibleIndex="5">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn
                                                                FieldName="CodigoItemMenu" ShowInCustomizationForm="True" Visible="False"
                                                                VisibleIndex="3">
                                                                <PropertiesTextEdit>
                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                    </ValidationSettings>
                                                                </PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn
                                                                FieldName="DescricaoOpcaoMenu" ShowInCustomizationForm="True"
                                                                VisibleIndex="2" Caption="Título do Menu">
                                                                <PropertiesTextEdit MaxLength="30">
                                                                    <ValidationSettings CausesValidation="True" ErrorDisplayMode="Text"
                                                                        ErrorText="">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <Style>
                                            </Style>
                                                                </PropertiesTextEdit>
                                                                <EditFormSettings CaptionLocation="Top" Caption="Título do Menu:" />
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Ordem"
                                                                FieldName="SequenciaItemMenuGrupo" ShowInCustomizationForm="True"
                                                                VisibleIndex="4" Width="70px">
                                                                <PropertiesSpinEdit DisplayFormatString="g" MaxLength="4" MaxValue="1000"
                                                                    NumberType="Integer" Width="70px">
                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                    </SpinButtons>
                                                                    <ValidationSettings CausesValidation="True" ErrorDisplayMode="Text"
                                                                        ErrorText="">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <Style>
                                            </Style>
                                                                </PropertiesSpinEdit>
                                                                <EditFormSettings CaptionLocation="Top" Caption="Ordem:" />
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Opção de Menu"
                                                                FieldName="DescricaoItemMenuObjeto" Name="CodigoGrupoMenu"
                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="250px">
                                                                <PropertiesComboBox TextField="DescricaoGrupoMenuTipoProjeto"
                                                                    ValueField="CodigoGrupoMenu" ValueType="System.Int32">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text"
                                                                        ErrorText="" ValidationGroup="MKE">
                                                                        <ErrorFrameStyle>
                                                                            <border bordercolor="Red" borderstyle="Solid" borderwidth="1px" />
                                                                        </ErrorFrameStyle>
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <Style>
                                            </Style>
                                                                </PropertiesComboBox>
                                                                <EditFormSettings CaptionLocation="Top" Caption="Opção de Menu:"
                                                                    ColumnSpan="2" />
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowFocusedRow="True"
                                                            ConfirmDelete="True" />
                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                        </SettingsPager>
                                                        <SettingsEditing Mode="PopupEditForm" />
                                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="320" />
                                                        <SettingsText PopupEditFormCaption="Opção de Menu" />
                                                        <SettingsPopup>
                                                            <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter"
                                                                Width="550px" />
                                                        </SettingsPopup>
                                                        <Styles>
                                                            <EditForm>
                                                            </EditForm>
                                                            <EditFormCell>
                                                            </EditFormCell>
                                                        </Styles>
                                                    </dxwgv:ASPxGridView>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <dxe:ASPxButton ID="btnFechar2" runat="server" ClientInstanceName="btnFechar2"
                                                                    HorizontalAlign="Center"
                                                                    Text="Fechar" Width="90px" AutoPostBack="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	pcItemsMenus.Hide();
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
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="" align="center"></td>
                                                    <td style="width: 70px" align="center" rowspan="3">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

























                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>

























                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
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
                        </dxp:PanelContent>
                    </PanelCollection>

                    <ClientSideEvents EndCallback="function(s, e) 
{
	if(s.cp_Msg != &quot;&quot;)
		mostraDivSalvoPublicado(s.cp_Msg);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td></td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

    </table>
</asp:Content>
