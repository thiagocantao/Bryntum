<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="cadastroGrupoIndicador.aspx.cs" Inherits="administracao_cadastroGrupoIndicador" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <!-- TABLA CONTEUDO -->
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Grupo de Indicadores">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="height: 10px" align="right">
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td style="width: 205px">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ClientInstanceName="ddlExporta"
                                                    ID="ddlExporta">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}"></ClientSideEvents>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td style="padding-left: 2px">
                                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnImage" Width="23px"
                                                    Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
                                                    <PanelCollection>
                                                        <dxp:PanelContent runat="server">
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                                Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao">
                                                            </dxe:ASPxImage>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td>
                                <dxe:ASPxButton runat="server" Text="Exportar" 
                                    ID="Aspxbutton1" OnClick="btnExcel_Click">
                                    <Paddings Padding="0px"></Paddings>
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="height: 10px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick" PaperKind="A4" PreserveGroupRowStates="True"
                                PrintSelectCheckBox="True">
                            </dxwgv:ASPxGridViewExporter>
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoGrupoIndicador"
                                AutoGenerateColumns="False" Width="100%" 
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                                OnHtmlRowPrepared="gvDados_HtmlRowPrepared1">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
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
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderTemplate>
                                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoGrupoIndicador" Name="CodigoGrupoIndicador"
                                        Caption="Código" VisibleIndex="1" Width="30px" Visible="False">
                                        <Settings AutoFilterCondition="Contains" AllowAutoFilter="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="DescricaoGrupoIndicador"
                                        ShowInCustomizationForm="True" VisibleIndex="2" Name="DescricaoGrupoIndicador">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Operacional?" FieldName="IndicaGrupoOperacional"
                                        ShowInCustomizationForm="True" VisibleIndex="3" Width="90px" Name="IndicaGrupoOperacional">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Estratégico?" FieldName="IndicaGrupoEstrategico"
                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="90px" Name="IndicaGrupoEstrategico">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Controlado Pelo sistema" FieldName="IniciaisGrupoControladoSistema"
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="6" Name="IniciaisGrupoControladoSistema">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowFooter="True">
                                </Settings>
                                <SettingsText></SettingsText>
                                <Templates>
                                    <FooterRow>
                                        <table cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td style="border-right: green 2px solid; border-top: green 2px solid; border-left: green 2px solid;
                                                        width: 15px; border-bottom: green 2px solid; background-color: #DDFFCC">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                            Font-Size="7pt" Text="Controlados pelo sistema." Font-Bold="False">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="632px"  ID="pcDados">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                        Text="Descrição do Grupo:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtDescricaoGrupo" runat="server" ClientInstanceName="txtDescricaoGrupo"
                                                         MaxLength="250" Width="100%" Text="123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 ">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px">
                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxCheckBox ID="ckbIndicaGrupoOperacional" runat="server" CheckState="Unchecked"
                                                                    Text="Disponível para Indicadores de Projetos" 
                                                                    ClientInstanceName="ckbIndicaGrupoOperacional">
                                                                    <Border BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxCheckBox>
                                                            </td>
                                                            <td align="right">
                                                                <dxe:ASPxCheckBox ID="ckbIndicaGrupoEstrategico" runat="server" CheckState="Unchecked"
                                                                    Text="Disponível para Indicadores da Estratégia" 
                                                                    ClientInstanceName="ckbIndicaGrupoEstrategico">
                                                                    <Border BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                        Text="Salvar" Width="100px"  ID="btnSalvar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                        <Paddings Padding="0px"></Paddings>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                        Text="Fechar" Width="90px"  ID="btnFechar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                        <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                        </Paddings>
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
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
                                window.top.mostraMensagem(&quot;Grupo de indicador incluído com sucesso!&quot;, 'sucesso', false, false, null);
               else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Grupo de indicador alterado com sucesso!&quot;, 'sucesso', false, false, null);
               else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Grupo de indicador excluído com sucesso!&quot;, 'sucesso', false, false, null);

      onClick_btnCancelar();
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
