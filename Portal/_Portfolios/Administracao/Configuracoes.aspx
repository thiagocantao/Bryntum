<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="Configuracoes.aspx.cs" Inherits="_Portfolios_Administracao_Configuracoes" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-left: 10px; height: 26px" valign="middle" align="left">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server"
                    ClientInstanceName="lblTituloTela" Font-Bold="True"
                    Text="Configurações de Portfólio">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>

    <div id="ConteudoPrincipal">
        <table>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%" ClientInstanceName="pnCallback" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
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
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="Ano" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	//OnGridFocusedRowChanged(s);
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
OnGridFocusedRowChanged(gvDados, true);		
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
}"></ClientSideEvents>
                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                        <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                    </SettingsCommandButton>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="110px" VisibleIndex="0">

                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                    <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
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
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Ano" Width="60px" Caption="Ano" VisibleIndex="2">
                                            <PropertiesTextEdit>

                                                <ClientSideEvents Validation="function(s, e) {
	if (isNaN(Ano) || Ano.length&lt;4 || parseFloat(Ano)&lt;1900){
		e.isValid = false;
		e.errorText = 'Ano inv&#225;lido';
	} else {
		e.isValid = true;
	}
}"></ClientSideEvents>

                                            </PropertiesTextEdit>

                                            <EditFormSettings ColumnSpan="2" Visible="True" CaptionLocation="Top"></EditFormSettings>

                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Editável?"
                                            FieldName="IndicaAnoPeriodoEditavel" ShowInCustomizationForm="True"
                                            VisibleIndex="2" Width="70px">
                                            <EditFormSettings CaptionLocation="Top" Visible="True" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Periodicidade"
                                            FieldName="IndicaTipoDetalheEdicao" ShowInCustomizationForm="True"
                                            VisibleIndex="4">
                                            <EditFormSettings CaptionLocation="Top" Visible="True" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="AnoAtivo" Caption="Ano Ativo?" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="MetaEditavel" Caption="Meta Edit&#225;vel?" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ResultadoEditavel" Caption="Resultado Edit&#225;vel?" VisibleIndex="6"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaAnoAtivo" Caption="indicaAnoAtivo" Visible="False" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaMetaEditavel" Caption="IndicaMetaEditavel" Visible="False" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaResultadoEditavel" Caption="indicaResultadoEditavel" Visible="False" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn FieldName="CodigoPeriodicidade" Caption="CodigoPeriodicidade" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11"></dxtv:GridViewDataTextColumn>
                                    </Columns>

                                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                    <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>

                                    <SettingsText></SettingsText>
                                </dxwgv:ASPxGridView>
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="387px" ID="pcDados">
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 69px">
                                                                            <dxe:ASPxLabel runat="server" Text="Ano:" ClientInstanceName="lblAno" ID="lblAno"></dxe:ASPxLabel>

                                                                        </td>
                                                                        <td style="width: 65px">
                                                                            <dxe:ASPxLabel runat="server" Text="Editável:"
                                                                                ClientInstanceName="lblEditavel"
                                                                                ID="lblEditavel">
                                                                            </dxe:ASPxLabel>

                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Periodicidade:" ClientInstanceName="lblTipoEdicao" ID="lblTipoEdicao"></dxe:ASPxLabel>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 69px">
                                                                            <dxe:ASPxTextBox runat="server" Width="60px" MaxLength="4" ClientInstanceName="txtAno" ID="txtAno">
                                                                                <ClientSideEvents Validation="function(s, e) {
	var Ano = txtAno.GetText();
	if (isNaN(Ano) || Ano.length&lt;4 || parseFloat(Ano)&lt;1900){
		e.isValid = false;
		e.errorText = 'Ano inv&#225;lido';
	} else {
		e.isValid = true;
	}
}"></ClientSideEvents>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxTextBox>

                                                                        </td>
                                                                        <td style="width: 85px">
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="80px" ClientInstanceName="ddlEditavel" ID="ddlEditavel">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
                                                                                </Items>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxComboBox>

                                                                        </td>

                                                                        <td>
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlTipoEdicao" ID="ddlTipoEdicao">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Na Periodicidade" Value="P"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="Total" Value="A"></dxe:ListEditItem>
                                                                                </Items>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxComboBox>

                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxrp:ASPxRoundPanel runat="server" HeaderText="Metas de Projetos" ID="ASPxRoundPanel1">
                                                                <PanelCollection>
                                                                    <dxp:PanelContent runat="server">
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <table style="width: 340px" cellspacing="0" cellpadding="0" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 100px; height: 16px" align="left">
                                                                                                        <dxe:ASPxLabel runat="server" Text="Ano Ativo:" ClientInstanceName="lblAnoAtivo" ID="lblAnoAtivo"></dxe:ASPxLabel>


                                                                                                    </td>
                                                                                                    <td style="width: 10px; height: 16px"></td>
                                                                                                    <td style="width: 100px; height: 16px" align="left">
                                                                                                        <dxe:ASPxLabel runat="server" Text="Meta Edit&#225;vel:" ClientInstanceName="lblMetaEditavel" ID="lblMetaEditavel"></dxe:ASPxLabel>


                                                                                                    </td>
                                                                                                    <td style="width: 10px; height: 16px"></td>
                                                                                                    <td style="height: 16px" align="left">
                                                                                                        <dxe:ASPxLabel runat="server" Text="Resultado Edit&#225;vel:" ClientInstanceName="lblResultadoEditavel" ID="lblResultadoEditavel"></dxe:ASPxLabel>


                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlAnoAtivo" ID="ddlAnoAtivo">
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                                                                                <dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
                                                                                                            </Items>

                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>


                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlMetaEditavel" ID="ddlMetaEditavel">
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                                                                                <dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
                                                                                                            </Items>

                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>


                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td style="width: 120px">
                                                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlResultadoEditavel" ID="ddlResultadoEditavel">
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                                                                                <dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
                                                                                                            </Items>

                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>


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
                                                            </dxrp:ASPxRoundPanel>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table style="text-align: right" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar"
                                                                                Width="90px" ID="btnSalvar"
                                                                                AutoPostBack="False">
                                                                                <ClientSideEvents Click="function(s, e) {
if (window.onClick_btnSalvar)
	    onClick_btnSalvar();	
}"></ClientSideEvents>

                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>

                                                                        </td>
                                                                        <td style="width: 10px"></td>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();	
}"></ClientSideEvents>

                                                                                <Paddings Padding="0px"></Paddings>
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
                                <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server"
                                    ClientInstanceName="pcUsuarioIncluido"
                                    HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
                                    Width="270px">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server"
                                            SupportsDisabledAttribute="True">
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
                            </dxp:PanelContent>
                        </PanelCollection>

                        <ClientSideEvents EndCallback="function(s, e) {
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.Configuracoes_registro_inclu_do_com_sucesso, 'sucesso', false, false, null);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.Configuracoes_registro_alterado_com_sucesso, 'sucesso', false, false, null);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.Configuracoes_registro_exclu_do_com_sucesso, 'sucesso', false, false, null);
	else
	{
        var erro = s.cp_erro;
	    window.top.mostraMensagem(traducao.Configuracoes_erro__ + erro, 'erro', true, false, null);
	}
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                    &nbsp;&nbsp;
                </td>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
