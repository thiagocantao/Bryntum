<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="PeriodoEstrategico.aspx.cs" Inherits="_Estrategias_wizard_PeriodoEstrategico" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td id="ConteudoPrincipal">
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <div id="divGrid" style="visibility: hidden">
                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoUnidadeNegocio" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                                    CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);

     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
        btnSalvar1.SetVisible(true);
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar1.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
" Init="function(s, e) {
    AdjustSize();
    document.getElementById(&quot;divGrid&quot;).style.visibility = &quot;&quot;;
}" EndCallback="function(s, e) {
AdjustSize();	
}"></ClientSideEvents>
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="100px">
                                                        <CustomButtons>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                <Image Url="~/imagens/botoes/editarReg02.PNG" />
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                                <Image Url="~/imagens/botoes/excluirReg02.PNG" />
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                                <Image Url="~/imagens/botoes/pFormulario.PNG" />
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
                                                    <dxwgv:GridViewDataTextColumn Caption="Ano" FieldName="Ano" Name="Ano" VisibleIndex="1">
                                                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Ano Ativo?" FieldName="AnoAtivo" Name="IndicaAnoAtivo"
                                                        VisibleIndex="2">
                                                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Meta Edit&#225;vel?" FieldName="MetaEditavel"
                                                        Name="IndicaMetaEditavel" VisibleIndex="3">
                                                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Resultado Edit&#225;vel?" FieldName="ResultadoEditavel"
                                                        Name="IndicaResultadoEditavel" VisibleIndex="4">
                                                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaAnoAtivo"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaMetaEditavel"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaResultadoEditavel"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Tipo Visualização"
                                                        FieldName="TipoVisualizacao" ShowInCustomizationForm="True" VisibleIndex="5">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaTipoDetalheVisualizacao"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>

                                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                                <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>

                                                <SettingsText></SettingsText>
                                                    <Paddings PaddingTop="10px" />
                                            </dxwgv:ASPxGridView>
                                            </div>                                            
                                        </td>
                                    </tr>
                                </table>
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados"
                                    CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="600px"
                                    ID="pcDados">
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="left">
                                                            <table class="formulario-colunas" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left" style="width: 55px">
                                                                            <dxe:ASPxLabel runat="server" Text="Ano:" ClientInstanceName="lblAno" ID="lblAno"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <%--<td align="left"></td>--%>
                                                                        <td style="width: 100px;" align="left">
                                                                            <dxe:ASPxLabel runat="server" Text="Ano Ativo:" ClientInstanceName="lblAnoAtivo" ID="lblAnoAtivo"></dxe:ASPxLabel>

                                                                        </td>
                                                                        <%--<td style="width: 10px;"></td>--%>
                                                                        <td style="width: 100px;" align="left">
                                                                            <dxe:ASPxLabel runat="server" Text="Meta Edit&#225;vel:" ClientInstanceName="lblMetaEditavel" ID="lblMetaEditavel"></dxe:ASPxLabel>

                                                                        </td>
                                                                        <%--  <td style="width: 10px;"></td>--%>
                                                                        <td align="left" style="width: 115px">
                                                                            <dxe:ASPxLabel runat="server" Text="Resultado Edit&#225;vel:" ClientInstanceName="lblResultadoEditavel" ID="lblResultadoEditavel"></dxe:ASPxLabel>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="width: 55px">
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtAno" ID="txtAno">
                                                                                <MaskSettings Mask="0000" IncludeLiterals="None"></MaskSettings>

                                                                                <ValidationSettings ValidationGroup="MKE" ErrorDisplayMode="None">
                                                                                    <RequiredField IsRequired="True" ErrorText=""></RequiredField>
                                                                                    <ErrorFrameStyle>
                                                                                        <Paddings Padding="0px" />
                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>

                                                                                <DisabledStyle BackColor="#E0E0E0" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <%--<td></td>--%>
                                                                        <td>
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlAnoAtivo" ID="ddlAnoAtivo">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
                                                                                </Items>

                                                                                <DisabledStyle BackColor="#E0E0E0" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxComboBox>

                                                                        </td>
                                                                        <%-- <td></td>--%>
                                                                        <td>
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlMetaEditavel" ID="ddlMetaEditavel">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
                                                                                </Items>

                                                                                <DisabledStyle BackColor="#E0E0E0" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxComboBox>

                                                                        </td>
                                                                        <%--<td></td>--%>
                                                                        <td style="width: 115px">
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlResultadoEditavel" ID="ddlResultadoEditavel">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
                                                                                </Items>

                                                                                <DisabledStyle BackColor="#E0E0E0" ForeColor="Black"></DisabledStyle>
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
                                                        <td style="padding-bottom: 5px;">
                                                            <dxtv:ASPxLabel ID="lblTipoVisualizacao" runat="server" ClientInstanceName="lblTipoVisualizacao" Text="Tipo Visualização:">
                                                            </dxtv:ASPxLabel>
                                                            <dxtv:ASPxComboBox ID="ddlTipoVisualizacao" runat="server" ClientInstanceName="ddlTipoVisualizacao" Width="100%">
                                                                <Items>
                                                                    <dxtv:ListEditItem Text="Total" Value="A" />
                                                                    <dxtv:ListEditItem Text="No período" Value="P" />
                                                                </Items>
                                                                <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar1"
                                                                                Text="Salvar" ValidationGroup="MKE" Width="90px"
                                                                                ID="btnSalvar1">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>

                                                                        </td>
                                                                        <td align="right" style="padding-left: 10px">
                                                                            <dxe:ASPxButton runat="server"
                                                                                ClientInstanceName="btnFechar" Text="Fechar" Width="90px"
                                                                                ID="btnFechar">
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
                                    <ContentStyle>
                                        <Paddings Padding="8px" />
                                    </ContentStyle>
                                </dxpc:ASPxPopupControl>
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
                                                        <td style="height: 10px"></td>
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
                            </dxp:PanelContent>
                        </PanelCollection>

                        <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();

	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Ano inclu&#237;do com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
                <td></td>
            </tr>
        </tbody>
    </table>
</asp:Content>
