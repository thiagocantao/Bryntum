<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ie_003.aspx.cs" Inherits="_VisaoIntegrante_Graficos_ie_003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

</script>
</head>
<body class="body" style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 10px; height: 4px;"></td>
                    <td style="height: 4px"></td>
                </tr>
                <tr>
                    <td style="width: 10px"></td>
                    <td align="left">
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                            KeyFieldName="CodigoAtribuicao"
                            PreviewFieldName="CodigoAtribuicao" Width="100%" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                            <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AutoExpandAllGroups="True" />
                            <Styles>
                                <FocusedRow BackColor="Transparent" ForeColor="Black">
                                </FocusedRow>
                            </Styles>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" SummaryType="Sum" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" SummaryType="Sum" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" SummaryType="Sum" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" SummaryType="Sum" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" SummaryType="Sum" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" SummaryType="Sum" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsEditing Mode="PopupEditForm" />
                            <SettingsText HeaderFilterShowAll="Todos" HeaderFilterShowBlanks="Vazios" HeaderFilterShowNonBlanks="N&#227;o Vazios" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="TarefaCritica" Name="TarefaCritica"
                                    VisibleIndex="0" Width="40px">
                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif'/&gt;"
                                        EncodeHtml="False">
                                    </PropertiesTextEdit>
                                    <Settings AllowDragDrop="False" AllowGroup="False" AllowAutoFilter="False" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
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
                                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="NomeProjeto" GroupIndex="0"
                                    ReadOnly="True" SortIndex="0" SortOrder="Ascending" VisibleIndex="1" Width="300px">
                                    <Settings AllowGroup="True" />
                                    <DataItemTemplate>
                                        <%# string.Format("<table><tr><td><img src='../../imagens/{0}.png' /></td><td>&nbsp;{1}</td></tr></table>", Eval("IndicaToDoList").ToString().Trim() == "N" ? "projeto" : "toDoList"
                                                                                                                                    , Eval("NomeProjeto").ToString()) %>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Tarefa" FieldName="NomeTarefa" VisibleIndex="1"
                                    Width="350px">
                                    <Settings AllowGroup="False" />
                                    <DataItemTemplate>
                                        <%# Eval("TarefaAtrasada").ToString() == "Sim" ? "<a style='Color:red'>" + Eval("NomeTarefa").ToString() + "</a>" : Eval("NomeTarefa").ToString()%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Conclu&#237;do" FieldName="PercentualConcluido"
                                    Name="PercConcluido" ReadOnly="True" VisibleIndex="2" Width="100px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n0}%">
                                        <ValidationSettings ErrorDisplayMode="None">
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <Settings AllowGroup="False" AllowAutoFilter="False" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Início Prev." FieldName="Inicio" Name="InicioReal"
                                    ReadOnly="True" VisibleIndex="3" Width="115px">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings ErrorDisplayMode="None">
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <Settings AllowGroup="False" ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Término Prev." FieldName="Termino"
                                    Name="TerminoReal" ReadOnly="True" VisibleIndex="4" Width="115px">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings ErrorDisplayMode="None">
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <Settings AllowGroup="False" ShowFilterRowMenu="True" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Anota&#231;&#245;es" FieldName="Anotacoes"
                                    Name="Anotacoes" ReadOnly="True" VisibleIndex="5" Width="600px">
                                    <Settings AllowGroup="False" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="IndicaToDoList" Visible="False" VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="True" VerticalScrollBarMode="Visible"
                                ShowHeaderFilterBlankItems="False" HorizontalScrollBarMode="Visible" />
                            <Templates>
                                <FooterRow>
                                    <table class="grid-legendas" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="grid-legendas-icone">
                                                <dxe:ASPxImage ID="ASPxImage1" runat="server"
                                                    ImageUrl="~/imagens/tarefaCritica.gif" Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td class="grid-legendas-label">
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                    Text="<%# Resources.traducao.ie_003_cr_ticas %>">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td class="grid-legendas-cor grid-legendas-cor-atrasado"><span></span></td>
                                            <td class="grid-legendas-label grid-legendas-label-atrasado">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                    Text="<%# Resources.traducao.ie_003_atrasadas %>">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td class="grid-legendas-icone">
                                                <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/projeto.PNG"
                                                    Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td class="grid-legendas-label">
                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                    Text="<%# Resources.traducao.ie_003_projeto %>">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td class="grid-legendas-icone">
                                                <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/toDoList.PNG"
                                                    Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td class="grid-legendas-label">
                                                <span>
                                                    <%=tituloToDoList %>
                                                </span>
                                            </td>
                                            <td class="grid-legendas-asterisco">
                                                <span>*</span>
                                            </td>
                                            <td class="grid-legendas-label">
                                                <%# getInfoLegenda() %>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterRow>
                                <GroupRowContent>
                                    <%# string.Format("<table><tr><td><img src='../../imagens/{0}.png' /></td><td>&nbsp;{1}</td></tr></table>", Eval("IndicaToDoList").ToString().Trim() == "N" ? "projeto" : "toDoList"
                                                                                                                                    , Eval("NomeProjeto").ToString()) %>
                                </GroupRowContent>
                            </Templates>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
            Landscape="True" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
