<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="FrmListaPendenciasWfSGDA.aspx.cs" Inherits="espacoTrabalho_FrmListaPendenciasWfSGDA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">

        <table>
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
                        ID="gvDados">
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


	
    	if (e.buttonID == 'btnGrafico')
	{
		url = window.top.pcModal.cp_Path + '_Portfolios/GraficoProcessoInterno.aspx?CW=' + codigoWorkflow + '&amp;CI=' + codigoInstanciaWf + '&amp;CF=' + codigoFluxo + '&amp;CP=' + -1 + '&Altura=' + (altura - 190) + '&Largura=' + (largura - 50);
		titulo = 'Gráfico';
	 }
	else
	{
		url = window.top.pcModal.cp_Path + 'wfEngineInterno.aspx?CW=' + codigoWorkflow + '&amp;CI=' + codigoInstanciaWf + '&amp;CE=' + codigoEtapa + '&amp;CS=' + ocorrencia + '&Altura=' + (altura - 190) + '&Largura=' + (largura - 50);
		titulo = 'Etapa';
	}

	window.top.showModal2(url + '&Popup=S', titulo, largura, altura, atualizaGrid, null);
}"
                            EndCallback="function(s, e) {
	
}"
                            ContextMenu="grid_ContextMenu
"></ClientSideEvents>
                        <Columns>
                            <dxcp:GridViewCommandColumn ButtonRenderMode="Image"
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
                            <dxcp:GridViewDataTextColumn FieldName="NomeFluxo"
                                Name="colTipoFluxo" Width="250px" Caption="Tipo de Fluxo" VisibleIndex="2"
                                GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                                <Settings AllowHeaderFilter="True"  />
                                <HeaderStyle></HeaderStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeInstanciaWf" Name="colNomeFluxo"
                                Caption="Nome do Fluxo" VisibleIndex="3" Width="300px">
                                <PropertiesTextEdit DisplayFormatString="{0}"></PropertiesTextEdit>

                                <Settings AllowAutoFilter="True" ShowFilterRowMenu="False"
                                    AllowHeaderFilter="False" AutoFilterCondition="Contains"></Settings>

                                <HeaderStyle></HeaderStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeEtapaAnterior" Width="225px" Caption="Etapa Anterior"
                                VisibleIndex="5" Visible="False">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="True"
                                    AutoFilterCondition="Contains" ></Settings>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeEtapaAtual" Width="190px" Caption="Etapa Atual"
                                VisibleIndex="6">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="True"
                                    AutoFilterCondition="Contains" ></Settings>

                                <HeaderStyle></HeaderStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeProjeto" VisibleIndex="4"
                                Caption="Descrição" Width="280px">

                                <Settings AllowAutoFilter="True" AllowHeaderFilter="False"
                                    AutoFilterCondition="Contains" ShowFilterRowMenu="False" />

                            </dxcp:GridViewDataTextColumn>
                            <dxtv:GridViewBandColumn Caption="Sub Etapas" VisibleIndex="8">
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <dxtv:GridViewDataImageColumn Caption="Envio das Placas de Reconhecimento" FieldName="colEnvioPlacasReconhecimento" VisibleIndex="3" Width="225px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dxtv:GridViewDataImageColumn>
                                    <dxtv:GridViewDataImageColumn Caption=" Providências de Viagem" FieldName="colDespesasViagem" VisibleIndex="1" Width="150px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dxtv:GridViewDataImageColumn>
                                    <dxtv:GridViewDataImageColumn Caption="Envio de Material de Apoio" FieldName="colMaterialApoio" VisibleIndex="2" Width="175px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dxtv:GridViewDataImageColumn>
                                </Columns>
                            </dxtv:GridViewBandColumn>
                            <dxtv:GridViewDataTextColumn Caption="Prazo" VisibleIndex="7" FieldName="Prazo" Width="80px">
                                <PropertiesTextEdit DisplayFormatString="d">
                                </PropertiesTextEdit>
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn Caption="Protocolo" FieldName="NumeroProtocolo" VisibleIndex="1" Width="75px">
                            </dxtv:GridViewDataTextColumn>
                        </Columns>

                        <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True"
                            EnableCustomizationWindow="True"></SettingsBehavior>
                        <SettingsResizing ColumnResizeMode="Control" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                        <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" ShowGroupPanel="True"
                            VerticalScrollBarMode="Auto" ShowStatusBar="Visible"
                            HorizontalScrollBarMode="Auto"></Settings>

                        <SettingsCommandButton>
                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                        </SettingsCommandButton>

                        <SettingsPopup>
                            <HeaderFilter Width="200px" Height="350px"></HeaderFilter>
                        </SettingsPopup>

                        <FormatConditions>
                            <dxtv:GridViewFormatConditionHighlight Expression="Today() &gt; GetDate([Prazo])" FieldName="Prazo" Format="Custom" ShowInColumn="Prazo">
                                <CellStyle ForeColor="Red">
                                </CellStyle>
                            </dxtv:GridViewFormatConditionHighlight>
                        </FormatConditions>

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
                        ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" Landscape="True" PaperKind="A4">
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
                    <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxtv:ASPxHiddenField>

                </td>
                <td style="width: 10px; height: 10px;"></td>
            </tr>
        </table>
    </form>
</body>
</html>

