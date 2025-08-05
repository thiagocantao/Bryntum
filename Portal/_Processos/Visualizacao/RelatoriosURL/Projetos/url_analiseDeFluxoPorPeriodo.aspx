<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_analiseDeFluxoPorPeriodo.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_analiseDeFluxoPorPeriodo"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1 {
            width: 5px;
            height: 2px;
        }
    </style>
</head>
<body style="margin: 10px">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" enableviewstate="false">
            <tr>
                <td class="style1"></td>
                <td align="right">
                    <table>
                        <tr>
                            <td
                                align="right">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Carteira:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-right: 5px;">
                                <dxe:ASPxComboBox ID="cmbCarteiras" ClientInstanceName="cmbCarteiras" runat="server"
                                    Width="300px">
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="right">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                    Text="De:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-right: 5px;">
                                <dxe:ASPxDateEdit ID="dtDe" runat="server" ClientInstanceName="dtDe"
                                    Width="100px">
                                    <CalendarProperties>
                                        <DayHeaderStyle />
                                        <WeekNumberStyle>
                                        </WeekNumberStyle>
                                        <DayStyle />
                                        <DaySelectedStyle>
                                        </DaySelectedStyle>
                                        <DayOtherMonthStyle>
                                        </DayOtherMonthStyle>
                                        <DayWeekendStyle>
                                        </DayWeekendStyle>
                                        <DayOutOfRangeStyle>
                                        </DayOutOfRangeStyle>
                                        <TodayStyle>
                                        </TodayStyle>
                                        <ButtonStyle>
                                        </ButtonStyle>
                                        <HeaderStyle />
                                        <FooterStyle />
                                        <FastNavStyle>
                                        </FastNavStyle>
                                        <FastNavMonthAreaStyle>
                                        </FastNavMonthAreaStyle>
                                        <FastNavYearAreaStyle>
                                        </FastNavYearAreaStyle>
                                        <FastNavMonthStyle>
                                        </FastNavMonthStyle>
                                        <FastNavYearStyle>
                                        </FastNavYearStyle>
                                        <FastNavFooterStyle>
                                        </FastNavFooterStyle>
                                        <Style>
                                        
                                    </Style>
                                    </CalendarProperties>
                                </dxe:ASPxDateEdit>
                            </td>
                            <td style="height: 18px" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                    Text="Até:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-right: 5px;">
                                <dxe:ASPxDateEdit ID="dteAte" runat="server" ClientInstanceName="dteAte"
                                    Width="100px">
                                    <CalendarProperties>
                                        <DayHeaderStyle />
                                        <WeekNumberStyle>
                                        </WeekNumberStyle>
                                        <DayStyle />
                                        <DaySelectedStyle>
                                        </DaySelectedStyle>
                                        <DayOtherMonthStyle>
                                        </DayOtherMonthStyle>
                                        <DayWeekendStyle>
                                        </DayWeekendStyle>
                                        <DayOutOfRangeStyle>
                                        </DayOutOfRangeStyle>
                                        <TodayStyle>
                                        </TodayStyle>
                                        <ButtonStyle>
                                        </ButtonStyle>
                                        <HeaderStyle />
                                        <FooterStyle />
                                        <FastNavStyle>
                                        </FastNavStyle>
                                        <FastNavMonthAreaStyle>
                                        </FastNavMonthAreaStyle>
                                        <FastNavYearAreaStyle>
                                        </FastNavYearAreaStyle>
                                        <FastNavMonthStyle>
                                        </FastNavMonthStyle>
                                        <FastNavYearStyle>
                                        </FastNavYearStyle>
                                        <FastNavFooterStyle>
                                        </FastNavFooterStyle>
                                        <Style>
                                        
                                    </Style>
                                    </CalendarProperties>
                                </dxe:ASPxDateEdit>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                    Width="100px">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoWorkflow;CodigoInstanciaWf"
                        OnDetailRowExpandedChanged="gvDados_DetailRowExpandedChanged" Width="100%" OnDetailRowGetButtonVisibility="gvDados_DetailRowGetButtonVisibility">
                        <ClientSideEvents CustomButtonClick="function(s, e) {
    if(&quot;btnResumo&quot; == e.buttonID)
	{
            s.GetRowValues(e.visibleIndex,&quot;CodigoInstanciaWf;CodigoWorkflow;NomeFluxo&quot;,MontaCamposFormulario);
            e.processOnServer = false;
	}
}" />
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem FieldName="NomeProjeto" ShowInColumn="Projeto" SummaryType="Count"
                                DisplayFormat="Quantidade de registros: {0}" />
                        </TotalSummary>
                        <Columns>
                            <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="70px"
                                Caption=" ">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnResumo"
                                        Text="Imprimr resumo de tramitação do processo">
                                        <Image AlternateText="Resumo do Processo" Url="~/imagens/botoes/btnPDF.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
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
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoCarteira" ReadOnly="True" Visible="False"
                                VisibleIndex="11">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" ReadOnly="True" Visible="False"
                                VisibleIndex="13">
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" VisibleIndex="5"
                                Caption="Projeto" Width="250px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeUnidadeNegocio" VisibleIndex="4"
                                Caption="Unidade" Width="250px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeFluxo" VisibleIndex="6"
                                Caption="Fluxo" Width="220px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="EtapaAtual" VisibleIndex="7"
                                Caption="Etapa atual" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="QtdeAnexos" VisibleIndex="1" Caption="Anexos"
                                Width="55px">
                                <Settings AllowAutoFilter="False" AllowHeaderFilter="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoWorkflow" ReadOnly="True" Visible="False"
                                VisibleIndex="14">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoInstanciaWf" ReadOnly="True" Visible="False"
                                VisibleIndex="16">
                                <EditFormSettings Visible="False" />
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Status" ReadOnly="True" VisibleIndex="2"
                                Width="100px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Responsável pelo Fluxo" FieldName="nomeResponsavelFluxo"
                                VisibleIndex="8" Width="200px">
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Término"
                                FieldName="DataTerminoInstancia" VisibleIndex="10" Width="120px">
                                <PropertiesDateEdit DisplayFormatString="">
                                </PropertiesDateEdit>
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ano Término 1º Etapa" FieldName="AnoInicio"
                                VisibleIndex="12" Width="120px">
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Mes Término 1º Etapa" FieldName="MesInicio"
                                VisibleIndex="15" Width="120px">
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unidade Superior" FieldName="NomeUnidadeNegocioSuperior"
                                VisibleIndex="3" Width="250px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Término 1º Etapa" FieldName="DataInicioInstancia"
                                VisibleIndex="9" Width="150px">
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager PageSize="50">
                        </SettingsPager>
                        <Settings ShowGroupPanel="True" ShowFilterRow="True"
                            VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" ExportMode="All" />
                        <Templates>
                            <DetailRow>
                                <dxwgv:ASPxGridView ID="gvAnexos" runat="server" AutoGenerateColumns="False" DataSourceID="dsAnexos"
                                    KeyFieldName="CodigoAnexo">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoFormulario" VisibleIndex="4" ShowInCustomizationForm="True"
                                            Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoAnexo" VisibleIndex="5" Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="CodigoAnexo" Caption=" ">
                                            <DataItemTemplate>
                                                <dxe:ASPxButton ID="btnDownLoad" runat="server" ClientInstanceName="<%# getNomeBotalDownload() %>"
                                                    AutoPostBack="False" Height="16px" ImageSpacing="0px" OnClick="btnDownLoad_Click"
                                                    ToolTip="Visualizar o arquivo" Width="16px" Wrap="False">
                                                    <Image Url="~/imagens/anexo/download.png" />
                                                    <FocusRectPaddings Padding="0px" />
                                                    <FocusRectBorder BorderColor="Transparent" BorderStyle="None" />
                                                    <Border BorderWidth="0px" />
                                                </dxe:ASPxButton>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoAnexo" VisibleIndex="2" ShowInCustomizationForm="True"
                                            Caption="Descrição">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Nome" VisibleIndex="3" Caption="Nome do arquivo">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" />
                                    <SettingsPager Visible="False" Mode="ShowAllRecords">
                                    </SettingsPager>
                                </dxwgv:ASPxGridView>
                            </DetailRow>
                        </Templates>
                    </dxwgv:ASPxGridView>
                    <asp:SqlDataSource ID="dsAnexos" runat="server" ConnectionString="<%$ ConnectionStrings:dbcdispsConnectionString %>"
                        SelectCommand="
select distinct CodigoFormulario, aa.CodigoAnexo, a.DescricaoAnexo, a.Nome
  from FormulariosInstanciasWorkflows AS fiw inner JOIN
              AnexoAssociacao AS aa ON (aa.CodigoObjetoAssociado = fiw.CodigoFormulario
                                   AND aa.CodigoTipoAssociacao = (select CodigoTipoAssociacao FROM TipoAssociacao WHERE IniciaisTipoAssociacao = 'FO')) inner join
              Anexo a on a.CodigoAnexo = aa.CodigoAnexo                                                                                                      
where fiw.CodigoWorkflow = @CodigoWorkflow
  AND fiw.CodigoInstanciaWf = @CodigoInstanciaWf">
                        <SelectParameters>
                            <asp:Parameter Name="CodigoWorkflow" />
                            <asp:Parameter Name="CodigoInstanciaWf" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <dxwgv:ASPxGridViewExporter ID="gvExporter" runat="server" GridViewID="gvDados"
                        OnRenderBrick="gvExporter_RenderBrick">
                    </dxwgv:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
