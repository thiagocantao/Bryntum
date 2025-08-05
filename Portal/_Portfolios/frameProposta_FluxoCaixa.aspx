<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameProposta_FluxoCaixa.aspx.cs" Inherits="_Portfolios_frameProposta_FluxoCaixa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script type="text/javascript">
        var existeConteudoCampoAlterado = false;

        function conteudoCampoAlterado() {
            existeConteudoCampoAlterado = true;
        }
    </script>

</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" id="tbBotoes" runat="server"
                            style="width: 100%">
                            <tr runat="server">
                                <td runat="server" style="padding: 3px" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dxm:ASPxMenu ID="menu0" runat="server" BackColor="Transparent"
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
                                </td>
                            </tr>
                        </table>
                        <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" KeyFieldName="Codigo" Width="100%"
                            OnRowUpdating="grid_RowUpdating" OnHtmlRowCreated="grid_HtmlRowCreated"
                            OnCellEditorInitialize="grid_CellEditorInitialize"
                            OnSummaryDisplayText="grid_SummaryDisplayText">
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"
                                ShowFooter="True" ShowGroupButtons="False" ShowGroupPanel="True"
                                ShowGroupFooter="VisibleAlways" />
                            <SettingsBehavior AllowDragDrop="False" AllowSort="False" />
                            <Styles>
                                <GroupRow BackColor="#EBEBEB" Font-Bold="True">
                                </GroupRow>
                                <Footer Font-Bold="True">
                                </Footer>
                            </Styles>
                        </dxwgv:ASPxGridView>
                        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1"
                            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                            <Styles>
                                <GroupFooter Font-Bold="True"></GroupFooter>
                                <Title Font-Bold="True"></Title>
                            </Styles>
                        </dxwgv:ASPxGridViewExporter>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-top: 5px">
                        <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar"
                            Width="90px" ID="btnFechar"
                            ClientVisible="False">
                            <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    window.top.fechaModal();
}"></ClientSideEvents>
                            <Paddings Padding="0px"></Paddings>
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
