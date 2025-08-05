<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mt_002.aspx.cs" Inherits="mt_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        function renderizaGrafico() {
            vchart.render(document.getElementById('chartdiv'));
        }

        function abreAnalise(ano, mes, codigoIndicador, codigoUnidadeNegocio) {
            parent.callback.PerformCallback(ano + ';' + mes + ';' + codigoIndicador + ';' + codigoUnidadeNegocio);
        }
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center">

            <table cellpadding="0" cellspacing="0" style="width: 95%">
                <tr>
                    <td>
                        <div id="chartdiv" align="center">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>

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

                        <dxwgv:ASPxGridView ID="gridProjetos" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="gridProjetos" KeyFieldName="CodigoProjeto" Width="100%">
                            <Styles>
                                <FocusedRow BackColor="Transparent">
                                </FocusedRow>
                            </Styles>
                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                            </SettingsPager>
                            <SettingsText ConfirmDelete="Deseja Realmente Excluir a Associa&#231;&#227;o?" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="50px">
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
                                <dxwgv:GridViewDataTextColumn Caption="Iniciativa" FieldName="NomeProjeto"
                                    VisibleIndex="1">
                                    <PropertiesTextEdit DisplayFormatString="{0}">
                                    </PropertiesTextEdit>
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                    <DataItemTemplate>
                                        <%# "<a href='../../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + Eval("CodigoProjeto") + "&NomeProjeto=" + Eval("NomeProjeto") + "' target='_top'>" + Eval("NomeProjeto") + "</a>"%>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Respons&#225;vel" FieldName="Responsavel"
                                    VisibleIndex="2" Width="190px">
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="%" FieldName="Concluido" VisibleIndex="3"
                                    Width="70px">
                                    <PropertiesTextEdit DisplayFormatString="{0:p0}" EncodeHtml="False">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="Status" VisibleIndex="4"
                                    Width="50px">
                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' style='border-width:0px;' /&gt;">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="110" />
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxMemo ID="txtAnalise" runat="server" ClientEnabled="False"
                            Rows="4" Width="100%" OnTextChanged="txtAnalise_TextChanged">
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <dxe:ASPxLabel ID="lblAnalise" runat="server" Font-Italic="True">
                        </dxe:ASPxLabel>
                        &nbsp;</td>
                </tr>
            </table>


            <script type="text/javascript"> 
                getGrafico('<%=grafico_swf %>', "grafico001", '100%', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
            </script>
        </div>
    </form>
</body>
</html>
