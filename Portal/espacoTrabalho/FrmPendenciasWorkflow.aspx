<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="FrmPendenciasWorkflow.aspx.cs" Inherits="espacoTrabalho_PendenciasWorkflow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">

        <table width="100%">
            <tr>
                <td style="width: 10px; height: 10px"></td>
                <td style="height: 10px"></td>
                <td style="width: 10px; height: 10px;"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>

                    <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados"
                        KeyFieldName="CodigoWorkflow;CodigoInstanciaWf;CodigoFluxo;OcorrenciaAtual;CodigoEtapaAtual"
                        AutoGenerateColumns="False" Width="100%"
                        ID="gvDados" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                        <ClientSideEvents CustomButtonClick="function(s, e) {
	var chaves = s.GetRowKey(e.visibleIndex).split('|');
	var codigoWorkflow = chaves[0];
	var codigoInstanciaWf = chaves[1];
	var codigoFluxo = chaves[2];
	var ocorrencia = chaves[3];
	var codigoEtapa = chaves[4];
	var url = '';
	var titulo = '';
	var largura = screen.width - 10;
	var altura  =  screen.height - 175;


	ddlAtual = ASPxClientControl.GetControlCollection().GetByName('ddl_' + codigoInstanciaWf);

    	if (e.buttonID == 'btnGrafico')
	{
		url = window.top.pcModal.cp_Path + '_Portfolios/GraficoProcessoInterno.aspx?CW=' + codigoWorkflow + '&amp;CI=' + codigoInstanciaWf + '&amp;CF=' + codigoFluxo + '&amp;CP=-1&amp;Altura=' + (altura - 190) + '&amp;Largura=' + (largura - 50) + '&amp;Popup=S';
		titulo = 'Gráfico';
	 }
	else
	{
		url = window.top.pcModal.cp_Path + 'wfEngineInterno.aspx?CW=' + codigoWorkflow + '&amp;CI=' + codigoInstanciaWf + '&amp;CE=' + codigoEtapa + '&amp;CS=' + ocorrencia + '&amp;Altura=' + (altura - 190) + '&amp;Largura=' + (largura - 50);
		titulo = 'Etapa';
	}

	abreTela(url, titulo, largura, altura);
}"
                            EndCallback="function(s, e) {
	ASPxClientEdit.ValidateGroup('MKE', true);
}"
                            ContextMenu="grid_ContextMenu
"></ClientSideEvents>
                        <Columns>
                            <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                Name="A&#231;&#245;es" Width="80px" Caption=" " VisibleIndex="0">
                                <CustomButtons>
                                    <dxcp:GridViewCommandColumnCustomButton ID="btnGrafico" Text="Visualizar Gr&#225;fico">
                                        <Image Url="~/imagens/botoes/fluxos.PNG"></Image>
                                    </dxcp:GridViewCommandColumnCustomButton>
                                    <dxcp:GridViewCommandColumnCustomButton ID="btnInteragir" Text="Interagir">
                                        <Image AlternateText="Interagir" Url="~/imagens/botoes/interagir.png"></Image>
                                    </dxcp:GridViewCommandColumnCustomButton>
                                </CustomButtons>

                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                <HeaderCaptionTemplate>
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


                                </HeaderCaptionTemplate>
                            </dxcp:GridViewCommandColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeFluxo" ShowInCustomizationForm="True"
                                Name="colTipoFluxo" Width="220px" Caption="Tipo de Fluxo" VisibleIndex="1"
                                GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                                <Settings AllowHeaderFilter="True"  />
                                <HeaderStyle></HeaderStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeInstanciaWf"
                                ShowInCustomizationForm="True" Name="colNomeFluxo"
                                Caption="Nome do Fluxo" VisibleIndex="2" Width="280px">
                                <PropertiesTextEdit DisplayFormatString="{0}"></PropertiesTextEdit>

                                <Settings AllowAutoFilter="True" ShowFilterRowMenu="False"
                                    AllowHeaderFilter="False" AutoFilterCondition="Contains"></Settings>

                                <HeaderStyle></HeaderStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeEtapaAnterior"
                                ShowInCustomizationForm="True" Width="200px" Caption="Etapa Anterior"
                                VisibleIndex="4" Visible="False">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="True"
                                    AutoFilterCondition="Contains" ></Settings>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeEtapaAtual"
                                ShowInCustomizationForm="True" Width="190px" Caption="Etapa Atual"
                                VisibleIndex="6">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="True"
                                    AutoFilterCondition="Contains" ></Settings>

                                <HeaderStyle></HeaderStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="UsuarioCriacaoInstancia"
                                ShowInCustomizationForm="True" Width="180px" Caption="Solicitante"
                                VisibleIndex="9">
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains"></Settings>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeUnidadeNegocio"
                                ShowInCustomizationForm="True" VisibleIndex="16"
                                Caption="Unidade de Negócio" Width="160px">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="True"
                                    AutoFilterCondition="Contains" />
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="PossuiPendenciaInteracao"
                                ShowInCustomizationForm="True" Name="Acao"
                                VisibleIndex="15" Caption="Ação" Width="220px">
                                <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                    AllowDragDrop="False" AllowFilterBySearchPanel="False" AllowGroup="False"
                                    AllowHeaderFilter="False" AllowSort="False" />
                                <DataItemTemplate>
                                    <dxtv:ASPxComboBox ID="ddlAcao" runat="server" ClientInstanceName="ddlAcao"
                                        ValueType="System.String" Width="100%">
                                    </dxtv:ASPxComboBox>
                                </DataItemTemplate>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeProjeto" VisibleIndex="3"
                                Caption="Nome do Projeto" Width="280px">

                                <Settings AllowAutoFilter="True" AllowHeaderFilter="False"
                                    AutoFilterCondition="Contains" ShowFilterRowMenu="False" />

                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="PossuiNotificacao"
                                VisibleIndex="17" Caption="Notificação" Width="90px">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="False"
                                    AutoFilterCondition="Contains" ShowFilterRowMenu="False" />
                            </dxcp:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn Caption="Atraso" VisibleIndex="18"
                                Width="90px" FieldName="IndicaAtraso">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="False"
                                    AutoFilterCondition="Contains" ShowFilterRowMenu="False" />
                            </dxtv:GridViewDataTextColumn>
                        </Columns>

                        <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True"
                            EnableCustomizationWindow="True"></SettingsBehavior>
                        <SettingsResizing ColumnResizeMode="Control"/>
                        <SettingsPager PageSize="100" AlwaysShowPager="True"></SettingsPager>

                        <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" ShowGroupPanel="True"
                            VerticalScrollBarMode="Auto" ShowStatusBar="Visible"
                            HorizontalScrollBarMode="Auto"></Settings>

                        <SettingsPopup>
                            <HeaderFilter Width="200px" Height="350px"></HeaderFilter>
                        </SettingsPopup>

                        <Styles>
                            <Header></Header>

                            <EmptyDataRow></EmptyDataRow>

                            <GroupPanel></GroupPanel>

                            <HeaderPanel></HeaderPanel>

                            <CommandColumn HorizontalAlign="Center"></CommandColumn>

                            <CommandColumnItem>
                                <Paddings Padding="1px"></Paddings>
                            </CommandColumnItem>

                            <StatusBar Font-Bold="True"
                                Font-Italic="True">
                            </StatusBar>

                            <HeaderFilterItem></HeaderFilterItem>
                        </Styles>

                        <StylesEditors>
                            <ButtonEdit></ButtonEdit>
                        </StylesEditors>
                        <Templates>
                            <StatusBar>
                                <%# getRowCount() %>
                            </StatusBar>
                        </Templates>
                    </dxcp:ASPxGridView>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 10px; height: 10px;"></td>
                <td style="height: 10px">

                    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
                        ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                        <Styles>
                            <Default></Default>

                            <Header></Header>

                            <Cell></Cell>

                            <GroupFooter Font-Bold="True"></GroupFooter>

                            <Title Font-Bold="True"></Title>
                        </Styles>
                    </dxwgv:ASPxGridViewExporter>

                    <dxm:ASPxPopupMenu ID="popupMenu" runat="server" ClientInstanceName="popupMenu">
                    </dxm:ASPxPopupMenu>
                    <dxm:ASPxPopupMenu ID="pmColumnMenu" runat="server" ClientInstanceName="pmColumnMenu">
                        <Items>
                            <dxm:MenuItem Name="cmdShowCustomization" Text="Selecionar campos">
                            </dxm:MenuItem>
                        </Items>
                        <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'cmdShowCustomization')
        gvDados.ShowCustomizationWindow();
}" />
                    </dxm:ASPxPopupMenu>

                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server"
                        ClientInstanceName="callbackSalvar" ClientVisible="False"
                        OnCallback="pnCallback_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Status == 'OK')
	{
		window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
		gvDados.PerformCallback();
	}
	else
		window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);
}
" />
                        <PanelCollection>
                            <dxcp:PanelContent runat="server">
                                <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                </dxtv:ASPxHiddenField>
                            </dxcp:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>

                </td>
                <td style="width: 10px; height: 10px;"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="right">
                    <dxcp:ASPxButton ID="ASPxButton3" runat="server"
                        Text="Publicar" Width="100px" AutoPostBack="False"
                        ValidationGroup="MKE">
                        <ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('MKE', true) == false)
		window.top.mostraMensagem('Existem pendências!', 'atencao', true, false, null);
	else
	{
		if(confirm('Confirma a execução das ações?'))
			callbackSalvar.PerformCallback();
	}
}" />
                        <Paddings Padding="0px" />
                    </dxcp:ASPxButton>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>

