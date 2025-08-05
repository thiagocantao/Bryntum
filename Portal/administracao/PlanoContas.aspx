<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="PlanoContas.aspx.cs" Inherits="administracao_PlanoContas" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table>
        <tr>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoConta"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                OnCustomErrorText="gvDados_CustomErrorText">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
//debugger	
OnGridFocusedRowChanged(s, true);
}"
                                    CustomButtonClick="function(s, e) 
{
     //debugger
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
}
"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
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
                                                <Image Url="~/imagens/botoes/pFormulario.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderTemplate>
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
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxtv:GridViewDataTextColumn Caption="Conta" Name="col_DescricaoConta" ShowInCustomizationForm="True"
                                        VisibleIndex="3" FieldName="DescricaoConta">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"
                                            AllowGroup="False" />
                                    </dxtv:GridViewDataTextColumn>
                                    <dxtv:GridViewDataTextColumn Caption="Conta Superior" Name="col_DescricaoContaSuperior"
                                        ShowInCustomizationForm="True" VisibleIndex="2"
                                        FieldName="DescricaoContaSuperior" Width="300px">
                                        <Settings AllowAutoFilter="False" AllowGroup="True" />
                                    </dxtv:GridViewDataTextColumn>
                                    <dxtv:GridViewDataTextColumn Caption="Código" FieldName="CodigoReservadoGrupoConta"
                                        Name="col_CodigoReservadoGrupoConta" ShowInCustomizationForm="True"
                                        VisibleIndex="5" Width="120px">
                                        <Settings AllowAutoFilter="False" AllowGroup="False" />
                                    </dxtv:GridViewDataTextColumn>
                                    <dxtv:GridViewDataCheckColumn Caption="Analítica"
                                        Name="col_IndicaContaAnalitica" ShowInCustomizationForm="True"
                                        VisibleIndex="6" Width="65px" FieldName="IndicaContaAnalitica"
                                        Visible="False">
                                        <Settings AllowAutoFilter="False" />
                                    </dxtv:GridViewDataCheckColumn>
                                    <dxtv:GridViewDataTextColumn Caption="codigoConta" FieldName="CodigoConta"
                                        Name="col_CodigoConta" ShowInCustomizationForm="True" Visible="False"
                                        VisibleIndex="7">
                                    </dxtv:GridViewDataTextColumn>
                                    <dxtv:GridViewDataTextColumn Caption="CodigoContaSuperior"
                                        FieldName="CodigoContaSuperior" Name="col_CodigoContaSuperior"
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                    </dxtv:GridViewDataTextColumn>
                                    <dxtv:GridViewDataComboBoxColumn Caption="Tipo" FieldName="TipoConta"
                                        Name="col_TipoConta" ShowInCustomizationForm="True" VisibleIndex="4"
                                        Width="100px">
                                        <PropertiesComboBox>
                                            <Items>
                                                <dxtv:ListEditItem Text="Receita" Value="RC" />
                                                <dxtv:ListEditItem Text="Investimento" Value="IV" />
                                                <dxtv:ListEditItem Text="Custo Fixo" Value="CF" />
                                                <dxtv:ListEditItem Text="Custo Variável" Value="CV" />
                                            </Items>
                                        </PropertiesComboBox>
                                        <Settings AllowAutoFilter="False" AllowGroup="True" />
                                    </dxtv:GridViewDataComboBoxColumn>
                                    <dxtv:GridViewDataComboBoxColumn Caption="E/S" FieldName="EntradaSaida"
                                        Name="col_EntradaSaida" ShowInCustomizationForm="True" VisibleIndex="1"
                                        Width="65px">
                                        <PropertiesComboBox>
                                            <Items>
                                                <dxtv:ListEditItem Text="Saída" Value="S" />
                                                <dxtv:ListEditItem Text="Entrada" Value="E" />
                                            </Items>
                                        </PropertiesComboBox>
                                        <Settings AllowAutoFilter="False" AutoFilterCondition="Contains"
                                            AllowGroup="True" />
                                    </dxtv:GridViewDataComboBoxColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                    ShowGroupPanel="True" ShowGroupedColumns="True"></Settings>
                                <SettingsText></SettingsText>
                            </dxwgv:ASPxGridView>

                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
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
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                            ID="lblAcaoGravacao">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxtv:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True"
                                ClientInstanceName="pcDados" CloseAction="None"
                                HeaderText="Detalhes" Height="184px"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="489px">
                                <ContentStyle>
                                    <Paddings Padding="5px" />
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True" />
                                <ContentCollection>
                                    <dxtv:PopupControlContentControl runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="width: 170px">
                                                                                        <dxtv:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                                            Text="Tipo:">
                                                                                        </dxtv:ASPxLabel>
                                                                                    </td>
                                                                                    <td>&nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 170px; padding-right: 5px">
                                                                                        <dxtv:ASPxRadioButtonList ID="rblEntradaSaida" runat="server"
                                                                                            ClientInstanceName="rblEntradaSaida"
                                                                                            RepeatDirection="Horizontal" Width="100%">
                                                                                            <Paddings Padding="0px" />
                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
   //debugger
   ddlTipoConta.PerformCallback(s.GetSelectedIndex());
}"
                                                                                                Init="function(s, e) {
	 ddlTipoConta.PerformCallback(s.GetSelectedIndex());
}" />
                                                                                            <Items>
                                                                                                <dxtv:ListEditItem Text="Entrada" Value="E" />
                                                                                                <dxtv:ListEditItem Text="Saída" Value="S" />
                                                                                            </Items>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxtv:ASPxRadioButtonList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxtv:ASPxCheckBox ID="ckbContaAnalitica" runat="server" CheckState="Unchecked"
                                                                                            ClientInstanceName="ckbContaAnalitica"
                                                                                            Text="Conta Analítica?" ValueChecked="S" ValueType="System.String"
                                                                                            ValueUnchecked="N">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxtv:ASPxCheckBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxtv:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                Text="Descrição:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxtv:ASPxTextBox ID="txtDescricaoConta" runat="server"
                                                                                ClientInstanceName="txtDescricaoConta"
                                                                                MaxLength="70" Width="100%">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="width: 235px">
                                                                                        <dxtv:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                                            Text="Codigo Reservado:">
                                                                                        </dxtv:ASPxLabel>
                                                                                        <span id="spanhelp">
                                                                                            <dxtv:ASPxImage ID="imgAjudaCodigoReservado" runat="server"
                                                                                                ClientInstanceName="imgAjudaCodigoReservado" ImageUrl="~/imagens/ajuda.png"
                                                                                                ShowLoadingImage="True" Cursor="pointer">
                                                                                            </dxtv:ASPxImage>
                                                                                        </span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxtv:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                                            Text="Tipo:">
                                                                                        </dxtv:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 235px; padding-right: 5px">
                                                                                        <dxtv:ASPxTextBox ID="txtCodigoReservado" runat="server"
                                                                                            ClientInstanceName="txtCodigoReservado"
                                                                                            MaxLength="30" Width="100%">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxtv:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxtv:ASPxCallbackPanel ID="callbackDll" runat="server" ClientInstanceName="callbackDll" OnCallback="callbackDll_Callback" Width="100%">
                                                                                            <ClientSideEvents EndCallback="function(s, e) {
//debugger
   if(ddlContaSuperior.cpIndiceSelecionado)
	{
		ddlContaSuperior.SetSelectedIndex(ddlContaSuperior.cpIndiceSelecionado);		
	}	
}" />
                                                                                            <PanelCollection>
                                                                                                <dxtv:PanelContent runat="server">
                                                                                                    <dxtv:ASPxComboBox ID="ddlTipoConta" runat="server" ClientInstanceName="ddlTipoConta" SelectedIndex="0" Width="100%">
                                                                                                        <ClientSideEvents EndCallback="function(s, e) {
	callbackDll.PerformCallback(ddlContaSuperior.GetSelectedIndex());
}" />
                                                                                                        <Items>
                                                                                                            <dxtv:ListEditItem Selected="True" Text="Receita" Value="RC" />
                                                                                                            <dxtv:ListEditItem Text="Investimento" Value="IV" />
                                                                                                            <dxtv:ListEditItem Text="Custo Fixo" Value="CF" />
                                                                                                            <dxtv:ListEditItem Text="Custo Variável" Value="CV" />
                                                                                                        </Items>
                                                                                                        <Paddings Padding="0px" />
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </dxtv:PanelContent>
                                                                                            </PanelCollection>
                                                                                        </dxtv:ASPxCallbackPanel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 5px;">
                                                                <dxtv:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                    Text="Conta Superior:">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxComboBox ID="ddlContaSuperior" runat="server"
                                                                    ClientInstanceName="ddlContaSuperior"
                                                                    Width="100%">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	//debugger
   if(ddlContaSuperior.cpIndiceSelecionado)
	{
		ddlContaSuperior.SetSelectedIndex(ddlContaSuperior.cpIndiceSelecionado);		
	}
}" />
                                                                    <Paddings Padding="0px" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="padding-bottom: 3px; padding-top: 3px">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                                        ClientInstanceName="btnSalvar"
                                                                        Text="Salvar" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
   e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                        <Paddings Padding="0px" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                                <td align="right">
                                                                    <dxtv:ASPxButton ID="btnFechar0" runat="server" AutoPostBack="False"
                                                                        ClientInstanceName="btnFechar"
                                                                        Text="Fechar" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	//e.processOnServer = false;
	//abrePopUp('Conta', 'Cancelar');
    if(window.onClick_btnCancelar)
     onClick_btnCancelar();

}" />
                                                                        <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                            PaddingRight="0px" PaddingTop="0px" />
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
                            <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral"
                                ID="hfGeral">
                            </dxcp:ASPxHiddenField>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) 
{
	//debugger
                if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Conta incluída com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Conta alterada com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Conta excluída com sucesso!&quot;);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="popupHelpUsuario" runat="server" ClientInstanceName="popupHelpUsuario"
        Width="460px" HeaderText="" PopupElementID="spanhelp"
        PopupHorizontalAlign="Center" PopupHorizontalOffset="100" PopupVerticalAlign="Middle" PopupVerticalOffset="-60"
        Modal="True">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                Código utilizado para integrar com outros softwares.
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderImage Url="~/imagens/ajuda.png"></HeaderImage>
    </dxpc:ASPxPopupControl>
    <dxtv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
    </dxtv:ASPxGridViewExporter>
</asp:Content>
