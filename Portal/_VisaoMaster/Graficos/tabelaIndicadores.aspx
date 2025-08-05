<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tabelaIndicadores.aspx.cs" Inherits="_VisaoMaster_Graficos_tabelaIndicadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            height: 10px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                            Width="100%"
                            OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                            OnHtmlRowPrepared="gvDados_HtmlRowPrepared"
                            KeyFieldName="CodigoCronogramaProjeto;CodigoTarefa"
                            OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                            OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                            <ClientSideEvents CustomButtonClick="function(s, e) {
	var currentRow = e.visibleIndex;
    pJustificativa.PerformCallback(s.GetRowKey(currentRow));
}" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0"
                                    Width="100px">
                                    <CustomButtons>
                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnJustificativa"
                                            Text="Anotações">
                                            <Image ToolTip="Anotações" Url="~/imagens/botoes/comentarios.PNG">
                                            </Image>
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <HeaderTemplate>
                                        <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr runat="server">
                                                <td runat="server" style="padding: 3px" valign="top">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxMenu ID="menu0" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
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
                                                                                <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                                    <Image Url="~/imagens/menuExportacao/html.png">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                                <dxtv:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                    <Image Url="~/imagens/menuExportacao/iconoCSV.png">
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
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Marco Físico" VisibleIndex="1"
                                    FieldName="NomeTarefa">
                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Previsão Término"
                                    FieldName="PrevisaoTermino" VisibleIndex="2" Width="125px">
                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                    </PropertiesDateEdit>
                                    <Settings ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Término Reprog."
                                    FieldName="TerminoReprogramado" VisibleIndex="5" Width="125px">
                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                    </PropertiesDateEdit>
                                    <Settings ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Dias Atraso" FieldName="DiasAtraso"
                                    VisibleIndex="4" Width="85px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Término Real" FieldName="TerminoReal"
                                    VisibleIndex="3" Width="125px">
                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                    </PropertiesDateEdit>
                                    <Settings ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Concluído" FieldName="Concluido"
                                    VisibleIndex="6" Width="95px">
                                    <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCor"
                                    VisibleIndex="7" Width="75px" Name="StatusCor">
                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' /&gt;">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="IndicaPossuiAnotacao" Visible="False"
                                    VisibleIndex="9">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Recursos" FieldName="Recursos"
                                    VisibleIndex="8" Width="190px">
                                    <Settings AutoFilterCondition="Contains" />
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False"
                                VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                            <Styles>
                                <HeaderFilterItem>
                                </HeaderFilterItem>
                                <FilterRowMenu>
                                </FilterRowMenu>
                            </Styles>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                            Text="Fechar" Width="90px">
                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                            <Paddings Padding="0px" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxpc:ASPxPopupControl ID="pJustificativa" runat="server"
                HeaderText="Anotações" Width="800px"
                ClientInstanceName="pJustificativa"
                OnWindowCallback="pJustificativa_WindowCallback" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ClientSideEvents EndCallback="function(s, e) {
	s.Show();
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                        Text="Marco Físico:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxTextBox ID="txtTarefa" runat="server" ClientEnabled="False"
                                        Width="100%"
                                        ClientInstanceName="txtTarefa">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2"></td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                        Text="Anotações:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="width: 100%;" runat="server" id="spanJustificativa"></span></td>
                            </tr>
                            <tr>
                                <td class="style2"></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False"
                                        Text="Fechar" Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	pJustificativa.Hide();
}" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
                ID="ASPxGridViewExporter1"
                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                <Styles>
                    <Default></Default>

                    <Header></Header>

                    <Cell></Cell>

                    <GroupFooter Font-Bold="True"></GroupFooter>

                    <Title Font-Bold="True"></Title>
                </Styles>
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
