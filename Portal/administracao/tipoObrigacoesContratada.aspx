<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="tipoObrigacoesContratada.aspx.cs" Inherits="administracao_tipoObrigacoesContratada" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif); height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Tipos de Obrigações Contratada">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 10px; height: 10px;"></td>
            <td style="height: 10px"></td>
            <td style="height: 10px; width: 10px">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>


                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%"
                    ClientInstanceName="pnCallback" OnCallback="pnCallback_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Tipo de Obrigação Contratada incluída com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Tipo de Obrigação Contratada alterada com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Tipo de Obrigação Contratada excluída com sucesso!&quot;);
}" />
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="gvDados"
                                KeyFieldName="CodigoTipoObrigacoesContratada"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize" Width="100%">
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
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"
                                    FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" />
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                        VisibleIndex="0" Width="100px">
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
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout"
                                                                            Name="btnRestaurarLayout">
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
                                    <dxwgv:GridViewDataTextColumn Caption="Codigo" FieldName="CodigoTipoObrigacoesContratada"
                                        Name="CodigoTipoObrigacoesContratada" ShowInCustomizationForm="True" Visible="False"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Descrição"
                                        FieldName="DescricaoTipoObrigacoesContratada" ShowInCustomizationForm="True"
                                        VisibleIndex="2" Name="DescricaoTipoObrigacoesContratada">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" />
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="620px">
                                <ContentStyle>
                                    <Paddings Padding="5px" />
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True" />
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                        Text="Descrição:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtDescricaoTipoObrigacoes" runat="server"
                                                        ClientInstanceName="txtDescricaoTipoObrigacoes"
                                                        MaxLength="50" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px"></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                                        ClientInstanceName="btnSalvar"
                                                                        Text="Salvar" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                        <Paddings Padding="0px" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td style="width: 10px"></td>
                                                                <td>
                                                                    <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                                        ClientInstanceName="btnFechar"
                                                                        Text="Fechar" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                        <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                            PaddingRight="0px" PaddingTop="0px" />
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
                            <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server"
                                ClientInstanceName="pcUsuarioIncluido"
                                HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
                                Width="270px">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td align="center" style=""></td>
                                                    <td align="center" rowspan="3" style="width: 70px">
                                                        <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar"
                                                            ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server"
                                                            ClientInstanceName="lblAcaoGravacao">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1"
                                runat="server" GridViewID="gvDados"
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

                </dxcp:ASPxCallbackPanel>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

