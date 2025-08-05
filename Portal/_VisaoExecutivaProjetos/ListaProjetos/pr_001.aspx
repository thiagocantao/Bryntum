<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pr_001.aspx.cs" Inherits="_VisaoExecutivaProjetos_ListaProjetos_pr_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function renderizaGrafico() {

        }

        function abreAnalise(ano, mes, codigoIndicador) {
            parent.callback.PerformCallback(ano + ';' + mes + ';' + codigoIndicador);
        }
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center">

            <table>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="gvProjetos"
                            KeyFieldName="NomeTarefa" Width="96%">
                            <Styles>
                                <Header>
                                </Header>
                                <Footer>
                                    <Paddings Padding="0px" />
                                </Footer>
                            </Styles>
                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                            </SettingsPager>
                            <SettingsText ConfirmDelete="Deseja Realmente Excluir a Associa&#231;&#227;o?" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="40px" Caption=" "
                                    FieldName="Status" Name="col_Status">
                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' /&gt;">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" AllowHeaderFilter="False" />
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
                                <dxwgv:GridViewDataTextColumn Caption="Marco" FieldName="NomeTarefa"
                                    VisibleIndex="1" ExportWidth="400">
                                    <PropertiesTextEdit DisplayFormatString="{0}">
                                    </PropertiesTextEdit>
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Previsto" ExportWidth="100"
                                    FieldName="TerminoPrevisto" VisibleIndex="2" Width="120px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                    <Settings ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Tendência" ExportWidth="100"
                                    FieldName="Termino" VisibleIndex="3" Width="120px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EncodeHtml="False">
                                    </PropertiesDateEdit>
                                    <Settings ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Realizado" ExportWidth="100"
                                    FieldName="TerminoReal" VisibleIndex="4" Width="120px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                    <Settings ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                            </Columns>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="160"
                                ShowFooter="True" />
                            <Templates>
                                <FooterRow>
                                    <table>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/VerdeOK.gif"
                                                    Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="left" style="padding-left: 3px; padding-right: 10px;">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                    Text="Concluído no Prazo">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="left">
                                                <dxe:ASPxImage ID="ASPxImage2" runat="server"
                                                    ImageUrl="~/imagens/VermelhoOK.gif" Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="left" style="padding-left: 3px; padding-right: 10px;">
                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                    Text="Concluído com Atraso">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="left">
                                                <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/Verde.gif"
                                                    Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="left" style="padding-left: 3px; padding-right: 10px;">
                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                    Text="No Prazo/Adiantado">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="left">
                                                <dxe:ASPxImage ID="ASPxImage4" runat="server" ImageUrl="~/imagens/Amarelo.gif"
                                                    Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="left" style="padding-left: 3px; padding-right: 10px;">
                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                    Text="Tendência de Atraso">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="left">
                                                <dxe:ASPxImage ID="ASPxImage5" runat="server" ImageUrl="~/imagens/Vermelho.gif"
                                                    Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="left" style="padding-left: 3px; padding-right: 10px">
                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                    Text="Atrasado">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterRow>
                            </Templates>
                        </dxwgv:ASPxGridView>
                        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
                            LeftMargin="50" RightMargin="50"
                            Landscape="True" ID="ASPxGridViewExporter1"
                            ExportEmptyDetailGrid="True" PreserveGroupRowStates="False"
                            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                            <Styles>
                                <Default>
                                </Default>
                                <Header>
                                </Header>
                                <Cell>
                                </Cell>
                                <Footer>
                                </Footer>
                                <GroupFooter>
                                </GroupFooter>
                                <GroupRow>
                                </GroupRow>
                                <Title></Title>
                            </Styles>
                        </dxwgv:ASPxGridViewExporter>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
