<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="criteriosSelecaoNova.aspx.cs" Inherits="_Portfolios_Administracao_criteriosSelecaoNova"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoCriterioSelecao"
                                PreviewFieldName="CodigoCriterioSelecao" AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) 
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
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
        desabilitaHabilitaComponentes();
		pcDados.Show();
     }	
}" Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
s.SetHeight(sHeight);
}"></ClientSideEvents>

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
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
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
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoCriterioSelecao" Caption="Descri&#231;&#227;o"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoCriterioSelecao" Caption="CodigoCriterioSelecao"
                                        Visible="False" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible"></Settings>
                                <Paddings PaddingTop="5px" />
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ID="pcDados" Width="960px">
                                <CloseButtonImage Width="17px">
                                </CloseButtonImage>
                                <SizeGripImage Width="12px">
                                </SizeGripImage>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        <dxp:ASPxPanel runat="server" Width="100%" ID="pnFormulario" Style="overflow: auto">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Crit&#233;rio:"
                                                                                        ID="ASPxLabel1">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="60" ClientInstanceName="txtCriterio"
                                                                                        ID="txtCriterio">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gridDescricao" KeyFieldName="CodigoOpcaoCriterioSelecao"
                                                                                        AutoGenerateColumns="False" Width="100%"
                                                                                        ID="gridDescricao" OnInitNewRow="gridDescricao_InitNewRow" OnRowUpdating="gridDescricao_RowUpdating"
                                                                                        OnRowInserting="gridDescricao_RowInserting" OnRowDeleting="gridDescricao_RowDeleting"
                                                                                        OnCustomCallback="gridDescricao_CustomCallback">
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" Caption="Ac&#245;es"
                                                                                                VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true"
                                                                                                ShowUpdateButton="true">
                                                                                                <HeaderTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <dxm:ASPxMenu ID="menu1" runat="server" BackColor="Transparent" ClientInstanceName="menu1"
                                                                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick1" OnInit="menu_Init1">
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
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoOpcaoCriterioSelecao" ReadOnly="True"
                                                                                                Width="50px" Caption="Item" VisibleIndex="1">
                                                                                                <PropertiesTextEdit>
                                                                                                    <ReadOnlyStyle BackColor="#E8E8E8" ForeColor="Black">
                                                                                                    </ReadOnlyStyle>
                                                                                                    <ValidationSettings ErrorText="Dado obrigat&#243;rio!" ErrorDisplayMode="None">
                                                                                                        <RequiredField ErrorText="Dado obrigat&#243;rio!"></RequiredField>
                                                                                                    </ValidationSettings>
                                                                                                    <Style></Style>
                                                                                                </PropertiesTextEdit>
                                                                                                <Settings AllowSort="False" />
                                                                                                <Settings AllowSort="False"></Settings>
                                                                                                <EditFormSettings VisibleIndex="0" CaptionLocation="Top" Caption="Item:"></EditFormSettings>
                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                <CellStyle HorizontalAlign="Center">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoOpcaoCriterioSelecao" Caption="Op&#231;&#227;o"
                                                                                                VisibleIndex="2" Width="175px">
                                                                                                <PropertiesTextEdit Width="480px" MaxLength="250">
                                                                                                    <ValidationSettings CausesValidation="True" EnableCustomValidation="True" ErrorText="Dado obrigat&#243;rio!"
                                                                                                        ErrorDisplayMode="None">
                                                                                                        <RequiredField IsRequired="True" ErrorText="Dado obrigat&#243;rio!"></RequiredField>
                                                                                                    </ValidationSettings>
                                                                                                    <Style></Style>
                                                                                                </PropertiesTextEdit>
                                                                                                <Settings AllowSort="False" />
                                                                                                <Settings AllowSort="False"></Settings>
                                                                                                <EditFormSettings VisibleIndex="1" CaptionLocation="Top" Caption="Op&#231;&#227;o:"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataMemoColumn FieldName="DescricaoEstendidaOpcao" Name="DescricaoEstendidaOpcao"
                                                                                                Caption="Descri&#231;&#227;o" VisibleIndex="3">
                                                                                                <PropertiesMemoEdit Rows="5" Width="100%">
                                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                                    </ValidationSettings>
                                                                                                    <Style></Style>
                                                                                                </PropertiesMemoEdit>
                                                                                                <Settings AllowSort="False" />
                                                                                                <Settings AllowSort="False"></Settings>
                                                                                                <EditFormSettings ColumnSpan="3" RowSpan="5" VisibleIndex="3" CaptionLocation="Top"
                                                                                                    Caption="Descri&#231;&#227;o:"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataMemoColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="ValorOpcaoCriterioSelecao" Width="120px"
                                                                                                Caption="Valor" VisibleIndex="4">
                                                                                                <PropertiesTextEdit Width="120px">
                                                                                                    <ValidationSettings CausesValidation="True" EnableCustomValidation="True" ErrorText="Dado obrigat&#243;rio!"
                                                                                                        ErrorDisplayMode="None">
                                                                                                        <RegularExpression ErrorText="Express&#227;o inv&#225;lida!" ValidationExpression="\d+"></RegularExpression>
                                                                                                        <RequiredField IsRequired="True" ErrorText="Dado obrigat&#243;rio!"></RequiredField>
                                                                                                    </ValidationSettings>
                                                                                                    <Style></Style>
                                                                                                </PropertiesTextEdit>
                                                                                                <Settings AllowSort="False" />
                                                                                                <Settings AllowSort="False"></Settings>
                                                                                                <EditFormSettings VisibleIndex="2" CaptionLocation="Top" Caption="Valor:"></EditFormSettings>
                                                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                                                <CellStyle HorizontalAlign="Right">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowSort="False" AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3">
                                                                                        </SettingsEditing>
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="600px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                                        </SettingsPopup>
                                                                                        <Settings ShowFooter="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="250"></Settings>
                                                                                        <SettingsText Title=" &amp;nbsp;" ConfirmDelete="Excluir esta op&#231;&#227;o?" PopupEditFormCaption="Edi&#231;ao/Novo"></SettingsText>
                                                                                        <Styles>
                                                                                            <TitlePanel BackColor="White" ForeColor="Black">
                                                                                            </TitlePanel>
                                                                                        </Styles>
                                                                                        <%-- <Templates>
                                                                                            <FooterRow>
                                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="left">
                                                                                                            <dxe:ASPxLabel ID="lblDescricaoTotal" runat="server" ClientInstanceName="lblDescricaoTotal"
                                                                                                                Font-Bold="False"  Text="<%$ Resources:traducao, criteriosSelecaoNova_total_de_crit_rios__ %>">
                                                                                                            </dxe:ASPxLabel>
                                                                                                            <%# gridDescricao.VisibleRowCount %>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </FooterRow>
                                                                                        </Templates>--%>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 48px" align="right">
                                                        <table class="formulario-botoes" id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                            Text="Salvar" Width="90px" ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="90px" ID="btnFechar">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
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
                        </dxp:PanelContent>
                    </PanelCollection>
                    <SettingsLoadingPanel Text=" "></SettingsLoadingPanel>
                    <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>
