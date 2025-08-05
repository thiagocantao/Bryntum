<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="agrupamentoContratoMaster_NE_OS.aspx.cs" Inherits="_Projetos_DadosProjeto_agrupamentoContratoMaster_NE_OS" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Atualização de Contratos de Grande Porte" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    
    <div style="position:relative;padding-top:5px;padding-left:5px;padding-bottom:5px;padding-right:5px">
    <dxtv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
        ClientInstanceName="gvDados" EnableViewState="False" 
        KeyFieldName="CodigoContratoEspecial" 
        OnCustomErrorText="gvDados_CustomErrorText" Width="100%" 
        OnCustomCallback="gvDados_CustomCallback" 
            oncustombuttoninitialize="gvDados_CustomButtonInitialize">
        <ClientSideEvents CustomButtonClick="function(s, e) {
gvDados.SetFocusedRowIndex(e.visibleIndex);
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
}" EndCallback="function(s, e) {

if(comando == &quot;CUSTOMCALLBACK&quot;)
{
if(&quot;Incluir&quot; == s.cp_OperacaoOk)
   	{
window.top.mostraMensagem(&quot;Registro incluído com sucesso!&quot;, 'sucesso', false, false, null);
//onEnd_pnCallback();
pcDados.Hide();
gvDados.SetVisible(true);
   	}	
    	else if(&quot;Editar&quot; == s.cp_OperacaoOk)
	{
		window.top.mostraMensagem(&quot;Registro alterado com sucesso!&quot;, 'sucesso', false, false, null);
}    
else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
{
window.top.mostraMensagem(&quot;Registro excluído com sucesso!&quot;, 'sucesso', false, false, null);
}
else if (s.cp_ErroSalvar != null ||s.cp_ErroSalvar != null != undefined)
{
               window.top.mostraMensagem(s.cp_ErroSalvar, 'erro', true, false, null);
}
   }    
}
" BeginCallback="function(s, e) {
comando = e.command;
}" />
        <Columns>
            <dxtv:GridViewDataTextColumn Caption="Contrato" FieldName="DescricaoContrato" 
                Name="col_DescricaoContrato" ShowInCustomizationForm="True" VisibleIndex="2" 
                Width="350px" SortIndex="0" SortOrder="Ascending">
                <Settings AutoFilterCondition="Contains" />
                <FilterCellStyle >
                </FilterCellStyle>
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataSpinEditColumn Caption="Valor Contratado (I0)" 
                FieldName="ValorContratado" Name="col_ValorContratado" 
                ShowInCustomizationForm="True" VisibleIndex="3">
                <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" 
                    NumberFormat="Currency">
                    <SpinButtons ClientVisible="False">
                    </SpinButtons>
                </PropertiesSpinEdit>
                <Settings AllowAutoFilter="False" />
                <HeaderStyle Wrap="True" HorizontalAlign="Right" />
            </dxtv:GridViewDataSpinEditColumn>
            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" 
                VisibleIndex="0" Width="100px">
                <CustomButtons>
                    <dxtv:GridViewCommandColumnCustomButton ID="btnEditar">
                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                        </Image>
                    </dxtv:GridViewCommandColumnCustomButton>
                    <dxtv:GridViewCommandColumnCustomButton ID="btnExcluir">
                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                        </Image>
                    </dxtv:GridViewCommandColumnCustomButton>
                    <dxtv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom">
                        <Image Url="~/imagens/botoes/pFormulario.PNG">
                        </Image>
                    </dxtv:GridViewCommandColumnCustomButton>
                </CustomButtons>
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td align="center">
                                <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" 
                                    ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" 
                                    OnItemClick="menu_ItemClick">
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
                                                <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" 
                                                    ToolTip="Exportar para HTML">
                                                    <Image Url="~/imagens/menuExportacao/html.png">
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
                                        <Border BorderStyle="None" />
                                    </HoverStyle>
                                    <Paddings Padding="0px" />
                                    </ItemStyle>
                                    <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                        <SelectedStyle>
                                            <Border BorderStyle="None" />
                                        </SelectedStyle>
                                    </SubMenuItemStyle>
                                    <Border BorderStyle="None" />
                                </dxtv:ASPxMenu>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
            </dxtv:GridViewCommandColumn>
            <dxtv:GridViewDataSpinEditColumn Caption="Valor Contratado (Atual)" 
                FieldName="ValorContratadoReaj" Name="col_ValorContratadoReaj" 
                ShowInCustomizationForm="True" VisibleIndex="4">
                <PropertiesSpinEdit DisplayFormatString="c" NumberFormat="Currency">
                    <SpinButtons ClientVisible="False">
                    </SpinButtons>
                </PropertiesSpinEdit>
                <Settings AllowAutoFilter="False" />
                <HeaderStyle Wrap="True" HorizontalAlign="Right" />
            </dxtv:GridViewDataSpinEditColumn>
            <dxtv:GridViewDataSpinEditColumn Caption="Valor Realizado (I0)" 
                FieldName="ValorRealizado" Name="col_ValorRealizado" 
                ShowInCustomizationForm="True" VisibleIndex="5">
                <PropertiesSpinEdit DisplayFormatString="c" NumberFormat="Currency">
                    <SpinButtons ClientVisible="False">
                    </SpinButtons>
                </PropertiesSpinEdit>
                <Settings AllowAutoFilter="False" />
                <HeaderStyle Wrap="True" HorizontalAlign="Right" />
            </dxtv:GridViewDataSpinEditColumn>
            <dxtv:GridViewDataSpinEditColumn Caption="Valor Realizado (Atual)" 
                FieldName="ValorRealizadoReaj" Name="col_ValorRealizadoReaj" 
                ShowInCustomizationForm="True" VisibleIndex="6">
                <PropertiesSpinEdit DisplayFormatString="c" NumberFormat="Currency">
                    <SpinButtons ClientVisible="False">
                    </SpinButtons>
                </PropertiesSpinEdit>
                <Settings AllowAutoFilter="False" />
                <HeaderStyle Wrap="True" HorizontalAlign="Right" />
            </dxtv:GridViewDataSpinEditColumn>
            <dxtv:GridViewDataTextColumn Caption="CodigoContratoEspecial" 
                FieldName="CodigoContratoEspecial" Name="col_CodigoContratoEspecial" 
                ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                <FilterCellStyle >
                </FilterCellStyle>
            </dxtv:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" 
            ConfirmDelete="True" />
        <SettingsPager PageSize="100">
        </SettingsPager>
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible" />
        <SettingsText CommandClearFilter="Limpar filtro" />
        <SettingsPopup>
            <EditForm AllowResize="True" HorizontalAlign="WindowCenter" 
                VerticalAlign="WindowCenter" />
        </SettingsPopup>
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" 
            AllowInsert="False" />
    </dxtv:ASPxGridView>
    </div>
    
    <dxtv:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" 
        CloseAction="None"  HeaderText="Detalhe" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        ShowCloseButton="False" Width="520px">
        <ContentStyle>
            <Paddings Padding="5px" />
        </ContentStyle>
        <HeaderStyle Font-Bold="True" />
        <ContentCollection>
            <dxtv:PopupControlContentControl runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <table ID="tbIdentificacao" border="0" cellpadding="0" cellspacing="0" 
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxLabel ID="lblNome" runat="server" ClientInstanceName="lblNome" 
                                                     Text="Contrato:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxTextBox ID="txtDescricaoContrato" runat="server" 
                                                    ClientInstanceName="txtDescricaoContrato"  
                                                    MaxLength="100" Width="100%">
                                                    <validationsettings causesvalidation="True" display="None" 
                                                        errordisplaymode="Text" errortext="*" setfocusonerror="True">
                                                        <requiredfield isrequired="True" />
                                                    </validationsettings>
                                                    <disabledstyle backcolor="#EBEBEB" forecolor="Black">
                                                    </disabledstyle>
                                                </dxtv:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="PADDING-TOP: 5px">
                                                <table>
                                                    <tr>
                                                        <td style="width: 260px">
                                                            <dxtv:ASPxLabel ID="lblNome0" runat="server" ClientInstanceName="lblNome" 
                                                                 Text="Valor Contratado (I0):">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="lblNome1" runat="server" ClientInstanceName="lblNome" 
                                                                 Text="Valor Contratado (Atual):">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 260px; padding-right: 5px">
                                                            <dxtv:ASPxCallbackPanel ID="pncb_txtValorContratado" runat="server" 
                                                                ClientInstanceName="pncb_txtValorContratado" 
                                                                OnCallback="pncb_txtValorContratado_Callback"  
                                                                 Width="100%">
                                                                <SettingsLoadingPanel Enabled="False" ShowImage="False" />
                                                                <PanelCollection>
                                                                    <dxtv:PanelContent runat="server">
                                                                        <dxtv:ASPxSpinEdit ID="txtValorContratado" runat="server" 
                                                                            ClientInstanceName="txtValorContratado" DecimalPlaces="2" 
                                                                            DisplayFormatString="c" EnableClientSideAPI="True" 
                                                                            Number="0" Width="100%">
                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <ValidationSettings CausesValidation="True" Display="None" 
                                                                                ErrorDisplayMode="ImageWithTooltip" ErrorText="*">
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxSpinEdit>
                                                                    </dxtv:PanelContent>
                                                                </PanelCollection>
                                                            </dxtv:ASPxCallbackPanel>
                                                        </td>
                                                        <td>
                                                            <dxtv:ASPxCallbackPanel ID="pncb_txtValorContratadoReaj" runat="server" 
                                                                ClientInstanceName="pncb_txtValorContratadoReaj" 
                                                                OnCallback="pncb_txtValorContratadoReaj_Callback"  
                                                                 Width="100%">
                                                                <SettingsLoadingPanel Enabled="False" ShowImage="False" />
                                                                <PanelCollection>
                                                                    <dxtv:PanelContent runat="server">
                                                                        <dxtv:ASPxSpinEdit ID="txtValorContratadoReaj" runat="server" 
                                                                            ClientInstanceName="txtValorContratadoReaj" DecimalPlaces="2" 
                                                                            DisplayFormatString="c" EnableClientSideAPI="True" 
                                                                            Number="0" Width="100%">
                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxSpinEdit>
                                                                    </dxtv:PanelContent>
                                                                </PanelCollection>
                                                            </dxtv:ASPxCallbackPanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <table>
                                                    <tr>
                                                        <td style="width: 260px">
                                                            <dxtv:ASPxLabel ID="lblNome2" runat="server" ClientInstanceName="lblNome" 
                                                                 Text="Valor Realizado (I0):">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="lblNome3" runat="server" ClientInstanceName="lblNome" 
                                                                 Text="Valor Realizado (Atual):">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 260px; padding-right: 5px">
                                                            <dxtv:ASPxCallbackPanel ID="pncb_txtValorRealizado" runat="server" 
                                                                ClientInstanceName="pncb_txtValorRealizado" 
                                                                OnCallback="pncb_txtValorRealizado_Callback"  
                                                                 Width="100%">
                                                                <SettingsLoadingPanel Enabled="False" ShowImage="False" />
                                                                <PanelCollection>
                                                                    <dxtv:PanelContent runat="server">
                                                                        <dxtv:ASPxSpinEdit ID="txtValorRealizado" runat="server" 
                                                                            ClientInstanceName="txtValorRealizado" DecimalPlaces="2" 
                                                                            DisplayFormatString="c" EnableClientSideAPI="True" 
                                                                            Number="0" Width="100%">
                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxSpinEdit>
                                                                    </dxtv:PanelContent>
                                                                </PanelCollection>
                                                            </dxtv:ASPxCallbackPanel>
                                                        </td>
                                                        <td>
                                                            <dxtv:ASPxCallbackPanel ID="pncb_txtValorRealizadoReaj" runat="server" 
                                                                ClientInstanceName="pncb_txtValorRealizadoReaj" 
                                                                OnCallback="pncb_txtValorRealizadoReaj_Callback"  
                                                                 Width="100%">
                                                                <SettingsLoadingPanel Enabled="False" ShowImage="False" />
                                                                <PanelCollection>
                                                                    <dxtv:PanelContent runat="server">
                                                                        <dxtv:ASPxSpinEdit ID="txtValorRealizadoReaj" runat="server" 
                                                                            ClientInstanceName="txtValorRealizadoReaj" DecimalPlaces="2" 
                                                                            DisplayFormatString="c" EnableClientSideAPI="True" 
                                                                            Number="0" Width="100%">
                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxSpinEdit>
                                                                    </dxtv:PanelContent>
                                                                </PanelCollection>
                                                            </dxtv:ASPxCallbackPanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-top: 10px;">
                                <table>
                                    <tr>
                                        <td width="100%">
                                        </td>
                                        <td>
                                            <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" 
                                                ClientInstanceName="btnSalvar"  
                                                Text="Salvar" Width="100px">
                                                <clientsideevents click="function(s, e) {
e.processOnServer = false;  
  var mensagemErro = validaCamposFormulario(); 
    if(mensagemErro == '')
    {
	   if (window.SalvarCamposFormulario)
    	{
		onClick_btnSalvar();
        	}    	
    }
    else
    {
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
        e.processOnServer = false;
    }
}




" />
                                            </dxtv:ASPxButton>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dxtv:ASPxButton ID="btnCancelar" runat="server" CommandArgument="btnCancelar" 
                                                 Text="Fechar" Width="100px">
                                                <clientsideevents click="function(s, e) {
   e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();

}" />
                                            </dxtv:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxtv:PopupControlContentControl>
        </ContentCollection>
    </dxtv:ASPxPopupControl>            
    <dxtv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
        PaperKind="A4">
    </dxtv:ASPxGridViewExporter>
    <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxtv:ASPxHiddenField>
</asp:Content>
