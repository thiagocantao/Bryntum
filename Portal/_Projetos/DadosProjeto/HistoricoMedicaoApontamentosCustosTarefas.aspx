<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoricoMedicaoApontamentosCustosTarefas.aspx.cs"
    Inherits="_Projetos_DadosProjeto_HistoricoMedicaoApontamentosCustosTarefas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
            GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        </dxwgv:ASPxGridViewExporter>
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            Width="100%" OnCallback="pnCallback_Callback">
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                    </dxhf:ASPxHiddenField>

                    <div id="divGrid" style="visibility:hidden">
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="gvDados"
                            KeyFieldName="CodigoBoletim" OnCustomCallback="gvDados_CustomCallback" OnHtmlRowPrepared="gvDados_HtmlRowPrepared"
                            Width="100%">
                            <ClientSideEvents CustomButtonClick="function(s, e) 
                                                                                        {
                                                                                            onClick_CustomButtomGrid(s, e);
                                                                                        }
                                                                                        "                                
                                Init="function(s, e) { 
                                        var height = Math.max(0, document.documentElement.clientHeight) - 25;
                                        gvDados.SetHeight(height); 
                                        document.getElementById('divGrid').style.visibility = '';
                                                                                }" />

                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <SettingsLoadingPanel Mode="Disabled" ShowImage="False" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0"
                                    Width="130px">
                                    <DataItemTemplate>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td align="center" valign="middle">
                                                        <dxe:ASPxButton ID="btnExcluir" runat="server" AutoPostBack="False" DisabledStyle-BackColor="Red"
                                                            BackColor="Transparent" ClientInstanceName="btnExcluir" Height="14px"
                                                            ImageSpacing="0px" OnLoad="Button_Load" ToolTip="Excluir boletim" Width="14px"
                                                            Wrap="False">
                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                            </Image>
                                                            <FocusRectPaddings Padding="0px" />
                                                            <FocusRectBorder BorderColor="Transparent" BorderStyle="None" />
                                                            <Border BorderStyle="None" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        <dxe:ASPxButton ID="btnDownLoad" runat="server" BackColor="Transparent"
                                                            Height="14px" ImageSpacing="0px" OnLoad="Button_Load" OnClick="ASPxButton1_Click"
                                                            ToolTip="Visualizar boletim" Width="14px" Wrap="False">
                                                            <Image Url="~/imagens/botoes/btnPDF.png">
                                                            </Image>
                                                            <FocusRectPaddings Padding="0px" />
                                                            <FocusRectBorder BorderColor="Transparent" BorderStyle="None" />
                                                            <Border BorderStyle="None" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </DataItemTemplate>
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td align="center">
                                                    <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                        ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init"
                                                        OnItemClick="menu_ItemClick">
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
                                                                    <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML"
                                                                        ToolTip="Exportar para HTML">
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
                                                            <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
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
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="#" FieldName="SequenciaBoletimProjeto"
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="10%">
                                    <Settings ShowFilterRowMenu="True" FilterMode="DisplayText" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Referência" FieldName="DescricaoPeriodo"
                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="25%">
                                    <Settings ShowFilterRowMenu="True" FilterMode="DisplayText" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Gerado em" FieldName="DataGeracaoBoletim"
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="25%">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                                    </PropertiesDateEdit>
                                    <Settings ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Gerado por" FieldName="NomeUsuarioGeracao"
                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="30%">
                                    <Settings ShowFilterRowMenu="True" FilterMode="DisplayText" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Excluído"
                                    FieldName="IndicaBoletimExcluido" ShowInCustomizationForm="True"
                                    VisibleIndex="6" Width="15%">
                                    <Settings ShowFilterRowMenu="True" FilterMode="DisplayText" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="CodigoBoletim"
                                    Name="CodigoBoletim" ShowInCustomizationForm="True" Visible="False"
                                    VisibleIndex="7">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="MesPeriodoBoletim"
                                    Name="MesPeriodoBoletim" ShowInCustomizationForm="True" Visible="False"
                                    VisibleIndex="8">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioGeracao"
                                    Name="CodigoUsuarioGeracao" ShowInCustomizationForm="True" Visible="False"
                                    VisibleIndex="9">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False"
                                AutoExpandAllGroups="True" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings ShowFilterRow="True" ShowGroupPanel="True"
                                VerticalScrollBarMode="Visible" />
                            <Settings ShowGroupPanel="True" ShowFooter="False" VerticalScrollBarMode="Visible"></Settings>
                        </dxwgv:ASPxGridView>
                    </div>

                    <dxpc:ASPxPopupControl ID="pcSelecaoModeloStatusReport" runat="server" AllowDragging="True"
                        ClientInstanceName="pcSelecaoModeloStatusReport" CloseAction="None"
                        HeaderText="Gerar Relatório de Histórico Medição Atribuição" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="300px" Height="180px" ShowFooter="True">
                        <FooterTemplate>
                            <div id="divContainer" style="display: flex; flex-direction: row-reverse">
                                <div style="padding-left: 5px">
                                    <dxtv:ASPxButton ID="btnCancelar_MSR" runat="server" AutoPostBack="False" Text="Fechar" Theme="MaterialCompact">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(onClick_btnCancelar_MSR)
		onClick_btnCancelar_MSR();
}" />
                                        <Paddings Padding="0px" />
                                    </dxtv:ASPxButton>
                                </div>
                                <div>
                                    <dxtv:ASPxButton ID="btnSalvar_MSR" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Gerar Relatório" Theme="MaterialCompact">
                                        <ClientSideEvents Click="function(s, e) {
                                                                                                    var mensagemValidacao = ValidaPreenchimento();
                                                                                                    if(mensagemValidacao == ''){
                                                                                                      e.processOnServer = false;
	                                                                                                  onClick_btnSalvar_MSR();
                                                                                                    }else{
                                                                                                        window.top.mostraMensagem(mensagemValidacao, 'atencao', true, false, null);
                                                                                                    }

                                                                                                                }" />
                                    </dxtv:ASPxButton>
                                </div>
                            </div>
                        </FooterTemplate>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                Text="Mês:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="width: 100px">
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                Text="Ano:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlMes" runat="server" ClientInstanceName="ddlMes"
                                                ValueType="System.Int32" Width="95%" DropDownRows="4">
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dx:ASPxSpinEdit ID="txtAno" runat="server" ClientInstanceName="txtAno" MaxLength="4" MaxValue="3000" NumberType="Integer" Width="100px" Style="margin-left: 0px" OnLoad="txtAno_Load">
                                            </dx:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                </table>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                    </dxpc:ASPxPopupControl>
                    <!-- ASPxPOPUPCONTROL : Dados salvos -->
                </dxp:PanelContent>
            </PanelCollection>
            <ClientSideEvents BeginCallback="function(s,e){window.top.lpAguardeMasterPage.Show(); }" 
                    EndCallback="function(s, e) {
                                                            window.top.lpAguardeMasterPage.Hide();
	                                                        local_onEnd_pnCallback(s,e);
                                                        }" />          
        </dxcp:ASPxCallbackPanel>
    </form>
</body>
</html>
