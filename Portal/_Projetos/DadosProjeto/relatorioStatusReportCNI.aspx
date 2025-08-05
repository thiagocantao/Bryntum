<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relatorioStatusReportCNI.aspx.cs" Inherits="_Projetos_DadosProjeto_relatorioStatusReportCNI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 100%;
            border-style: solid;
            border-width: 1px;
            background-color: #000000;
        }

        .campo-label {
            font-family: 'Roboto Regular', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif !important;
            font-size: 12px;
            color: #484848;
        }

        #tbFiltro {
            padding-bottom: 5px;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="auto-style1">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" id="tbFiltro">
                        <tr>
                            <td align="left" style="width: 14.28%">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblInstituicao" runat="server" EnableViewState="False"
                                                Text="Entidade:" CssClass="campo-label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlInstituicao" runat="server"
                                                ClientInstanceName="ddlInstituicao"
                                                EnableViewState="False" TextFormatString="{0}"
                                                CallbackPageSize="50" Width="100%">
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td id="td_FiltroGerente" align="left" style="width: 14.28%">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblIArea" runat="server" EnableViewState="False"
                                                Text="Área:" CssClass="campo-label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>

                                            <dxe:ASPxComboBox ID="ddlArea" runat="server"
                                                ClientInstanceName="ddlArea" ValueType="System.Int32"
                                                EnableViewState="False"
                                                IncrementalFilteringMode="Contains" TextFormatString="{0}"
                                                CallbackPageSize="50" EnableCallbackMode="True" Width="100%">
                                            </dxe:ASPxComboBox>


                                        </td>

                                    </tr>
                                </table>
                            </td>
                            <td style="padding-right: 5px; width: 14.28%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblConsultor" runat="server"
                                                Text="Projeto:" EnableViewState="False" CssClass="campo-label"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlConsultor" runat="server"
                                                ClientInstanceName="ddlConsultor"
                                                EnableViewState="False" TextFormatString="{0}"
                                                CallbackPageSize="50" EnableCallbackMode="True" Width="100%">
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="padding-right: 5px; width: 14.28%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPeriodo" runat="server"
                                                Text="Período de:" EnableViewState="False" CssClass="campo-label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxDateEdit ID="periodoDe" runat="server" ClientInstanceName="periodoDe" Width="100%">
                                                <ClientSideEvents DateChanged="function(s, e) {
	periodoAte.SetMinDate(s.GetValue());
}" />
                                            </dxcp:ASPxDateEdit>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="padding-right: 5px; width: 14.28%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPeriodo0" runat="server"
                                                Text="até:" EnableViewState="False" CssClass="campo-label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxDateEdit ID="periodoAte" runat="server" ClientInstanceName="periodoAte" Width="100%">
                                            </dxcp:ASPxDateEdit>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="padding-right: 5px; width: 14.28%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server"
                                                Text="Status do Projeto:" EnableViewState="False" CssClass="campo-label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxComboBox ID="ddlStatusProjeto" runat="server" ClientInstanceName="ddlStatusProjeto" Width="100%" SelectedIndex="0">
                                                <Items>
                                                    <dxtv:ListEditItem Selected="True" Text="Em Execução e em Planejamento" Value="Exec_Plan" />
                                                    <dxtv:ListEditItem Text="Cancelado" Value="Cancelado" />
                                                    <dxtv:ListEditItem Text="Em Execução" Value="Em Execução" />
                                                    <dxtv:ListEditItem Text="Em Planejamento" Value="Em Planejamento" />
                                                    <dxtv:ListEditItem Text="Encerrado" Value="Encerrado" />
                                                </Items>
                                            </dxcp:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 14.28%" valign="bottom">
                                <dxcp:ASPxButton ID="btnPesquisar" runat="server" AutoPostBack="False" ClientInstanceName="btnPesquisar" Text="Pesquisar" Width="100%">
                                    <ClientSideEvents Click="function(s, e) {
  //lpLoading.Show();	
  gvDados.PerformCallback();
}" />
                                </dxcp:ASPxButton>
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="padding-top: 10px">
                <td>
                    <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" Width="100%" ID="gvDados" EnableViewState="False" OnCustomCallback="gvDados_CustomCallback" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                        <ClientSideEvents EndCallback="function(s, e) {
	lpLoading.Hide();
}" />
                        <Templates>
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" class="auto-style2">
                                    <tr>
                                        <td>
                                            <dxtv:ASPxTextBox ID="txtTituloGrid" runat="server" BackColor="Black" ClientInstanceName="txtTituloGrid" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" ReadOnly="True" Width="100%">
                                            </dxtv:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsPager PageSize="100" AlwaysShowPager="True"></SettingsPager>

                        <Settings VerticalScrollBarMode="Visible" ShowTitlePanel="True"></Settings>

                        <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image"
                                VisibleIndex="0" Width="40px" ShowInCustomizationForm="False">
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
                            <dxcp:GridViewDataTextColumn FieldName="NomeProjeto" ShowInCustomizationForm="False" Caption="PROJETO" VisibleIndex="4" Name="colunaNomeProjeto">
                                <Settings AllowAutoFilter="False" AllowCellMerge="True"></Settings>
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeGerenteProjeto" ShowInCustomizationForm="False" Caption="RESPONSÁVEL" VisibleIndex="5" Name="colunaNomeGerenteProjeto">
                                <Settings AllowAutoFilter="False" AllowCellMerge="True"></Settings>
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="DataInicioTarefa" ShowInCustomizationForm="False" Caption="DATA DA DEMANDA" VisibleIndex="6" Name="colunaDataInicioTarefa">
                                <PropertiesTextEdit DisplayFormatString="dd/MMM">
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False" ShowFilterRowMenu="False"></Settings>
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" Wrap="True" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="NomeTarefa" ShowInCustomizationForm="True" Name="colunaNomeTarefa" Caption="DEMANDA" VisibleIndex="7">
                                <Settings AllowAutoFilter="False" ShowFilterRowMenu="False"></Settings>
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="RecursosAlocadosTarefa" ShowInCustomizationForm="False" Name="colunaRecursosAlocadosTarefa" Caption="ONDE ESTÁ" VisibleIndex="8">
                                <Settings AllowAutoFilter="False" ShowFilterRowMenu="False"></Settings>
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="DataTerminoTarefa" ShowInCustomizationForm="False" Caption="PREVISÃO DE ENTREGA" VisibleIndex="9" Name="celulaDataTerminoTarefa">
                                <Settings AllowAutoFilter="False" ShowFilterRowMenu="False"></Settings>
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" Wrap="True" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxcp:GridViewDataTextColumn>
                            <dxtv:GridViewDataDateColumn Caption="ENTRADA DO JOB" FieldName="DataBriefing" ShowInCustomizationForm="False" VisibleIndex="2" Name="colunaDataBriefing">
                                <PropertiesDateEdit DisplayFormatString="dd/MMM/yyyy">
                                </PropertiesDateEdit>
                                <Settings AllowAutoFilter="False" AllowCellMerge="True" />
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" Wrap="True" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxtv:GridViewDataDateColumn>
                            <dxtv:GridViewDataComboBoxColumn Caption="ÁREA" FieldName="UnidadeDemandante" ShowInCustomizationForm="False" VisibleIndex="1" Name="colunaUnidadeDemandante">
                                <Settings AllowAutoFilter="False" AllowCellMerge="True" />
                                <FilterCellStyle>
                                </FilterCellStyle>
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxtv:GridViewDataComboBoxColumn>
                            <dxtv:GridViewDataTextColumn Caption="CodigoProjeto" FieldName="CodigoProjeto" Visible="False" VisibleIndex="10">
                                <HeaderStyle BackColor="#CCCCCC" />
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn Caption="DIAS ÚTEIS CORRIDOS DESDE O ÚLTIMO RETORNO" FieldName="DiasUteisCorridos" Name="colunaDiasUteisCorridos" VisibleIndex="11">
                                <Settings AllowAutoFilter="False" ShowFilterRowMenu="False" />
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" Wrap="True" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn Caption="STATUS DO PROJETO" FieldName="StatusBriefing" Name="colunaStatusBriefing" VisibleIndex="3">
                               <Settings AllowCellMerge="True" />
                                <CellStyle Wrap="True" HorizontalAlign="Center">
                                </CellStyle>
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" Wrap="True" />
                                <Settings AllowAutoFilter="False" ShowFilterRowMenu="False" />
                            </dxtv:GridViewDataTextColumn>
                        </Columns>
                    </dxcp:ASPxGridView>
                </td>
            </tr>
        </table>
        <dxtv:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading" Font-Size="10pt" Height="73px" HorizontalAlign="Center" Text="Carregando&amp;hellip;" VerticalAlign="Middle" Width="180px">
        </dxtv:ASPxLoadingPanel>
        <dxcp:ASPxCallback ID="cbExportacao" runat="server" ClientInstanceName="cbExportacao" OnCallback="cbExportacao_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
               lpLoading.Hide();
	window.location = '../../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=pdf&amp;amp;bInline=false';
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxGridViewExporter ID="gvExporter" runat="server" GridViewID="gvDados" Landscape="True" PaperKind="A4" ExportEmptyDetailGrid="True" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default>
                </Default>
                <Header Font-Bold="True" BackColor="#D1D1D1" ForeColor="Black">
                </Header>
                <Cell>
                </Cell>
            </Styles>
        </dxcp:ASPxGridViewExporter>
    </form>
</body>
</html>
