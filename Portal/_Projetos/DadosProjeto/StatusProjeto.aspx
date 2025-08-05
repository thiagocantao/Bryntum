<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatusProjeto.aspx.cs" Inherits="_Projetos_DadosProjeto_StatusProjeto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style4 {
            width: 100%;
        }

        .style10 {
            height: 10px;
        }

        .style11 {
            width: 10px;
        }

        .style1 {
            width: 100%;
        }

        .style2 {
            width: 45px;
        }

        .auto-style1 {
            height: 1px;
        }
    </style>
</head>
<body style="margin: 5px">
    <form id="form1" runat="server">
        <div>

            <dxcp:ASPxSplitter ID="pn" runat="server" FullscreenMode="True" Height="100%" Orientation="Vertical" Width="100%">
                <Panes>
                    <dxtv:SplitterPane AllowResize="False" Size="70px" Name="pnGrupo1">
                        <Panes>
                            <dxtv:SplitterPane AllowResize="True">
                                <ContentCollection>
                                    <dxtv:SplitterContentControl runat="server">
                                        <table cellspacing="0" class="dxic-fileManager">
                                            <tr>
                                                <td>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td style="padding-right: 15px;">
                                                                <dxtv:ASPxLabel ID="lblUnidade" runat="server">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxtv:ASPxLabel ID="lblResponsavel" runat="server">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" class="style4">
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0">
                                                                    <tr>
                                                                        <td style="padding-right: 10px; padding-bottom: 5px">
                                                                            <dxtv:ASPxLabel ID="lblInicio" runat="server">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxtv:ASPxLabel ID="lblInicioLB" runat="server">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 10px">
                                                                            <dxtv:ASPxLabel ID="lblTermino" runat="server">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxtv:ASPxLabel ID="lblTerminoLB" runat="server">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <dxtv:ASPxLabel ID="lblAtualizacao" runat="server">
                                                                </dxtv:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxtv:SplitterContentControl>
                                </ContentCollection>
                            </dxtv:SplitterPane>
                            <dxtv:SplitterPane AllowResize="True">
                                <ContentCollection>
                                    <dxtv:SplitterContentControl runat="server">
                                        <table>
                                            <tr>
                                                <td style="width: 33%">
                                                    <table>
                                                        <tr>
                                                            <td align="left">
                                                                <dxtv:ASPxHyperLink ID="hlCronograma" runat="server" ForeColor="#5D7B9D" Target="_self" Text="0 Atrasos no Cronograma">
                                                                </dxtv:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <dxtv:ASPxHyperLink ID="hlEntregas" runat="server" ForeColor="#5D7B9D" Target="_self" Text="0 Entregas Atrasadas">
                                                                </dxtv:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <dxtv:ASPxHyperLink ID="hlToDoList" runat="server" ForeColor="#5D7B9D" Target="_self" Text="0 Atrasos no To Do List">
                                                                </dxtv:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td align="left" style="width: 47%">
                                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                                    <tr>
                                                                        <td class="style2">
                                                                            <dxtv:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/riscos.png">
                                                                            </dxtv:ASPxImage>
                                                                        </td>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="height: 16px">
                                                                                        <dxtv:ASPxHyperLink ID="hlRiscosAtivos" runat="server" ForeColor="#5D7B9D" Target="_self" Text="Riscos Ativos">
                                                                                        </dxtv:ASPxHyperLink>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 7px"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 16px">
                                                                                        <dxtv:ASPxHyperLink ID="hlRiscosEliminados" runat="server" ForeColor="#5D7B9D" Target="_self" Text="Riscos Eliminados">
                                                                                        </dxtv:ASPxHyperLink>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td align="left" style="width: 53%">
                                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                                    <tr>
                                                                        <td class="style2">
                                                                            <dxtv:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/issue.png">
                                                                            </dxtv:ASPxImage>
                                                                        </td>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="height: 16px">
                                                                                        <dxtv:ASPxHyperLink ID="hlProblemasAtivos" runat="server" ForeColor="#5D7B9D" Target="_self" Text="Problemas Ativos">
                                                                                        </dxtv:ASPxHyperLink>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 7px"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 15px">
                                                                                        <dxtv:ASPxHyperLink ID="hlProblemasEliminados" runat="server" ForeColor="#5D7B9D" Target="_self" Text="Problemas Eliminados">
                                                                                        </dxtv:ASPxHyperLink>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxtv:SplitterContentControl>
                                </ContentCollection>
                            </dxtv:SplitterPane>
                        </Panes>
                        <ContentCollection>
                            <dxcp:SplitterContentControl runat="server"></dxcp:SplitterContentControl>
                        </ContentCollection>
                    </dxtv:SplitterPane>
                    <dxtv:SplitterPane Name="pnGrupo2">
                        <Panes>
                            <dxtv:SplitterPane AllowResize="True" Name="pnGrafico01">
                                <ContentCollection>
                                    <dxtv:SplitterContentControl runat="server">
                                    </dxtv:SplitterContentControl>
                                </ContentCollection>
                            </dxtv:SplitterPane>
                            <dxtv:SplitterPane AllowResize="True" Name="pnGrafico02">
                                <ContentCollection>
                                    <dxtv:SplitterContentControl runat="server">
                                    </dxtv:SplitterContentControl>
                                </ContentCollection>
                            </dxtv:SplitterPane>
                        </Panes>
                        <ContentCollection>
                            <dxcp:SplitterContentControl runat="server"></dxcp:SplitterContentControl>
                        </ContentCollection>
                    </dxtv:SplitterPane>
                    <dxtv:SplitterPane Name="pnGrupo3">
                        <Panes>
                            <dxtv:SplitterPane AllowResize="True" Name="pnGrafico04">
                                <ContentCollection>
                                    <dxtv:SplitterContentControl runat="server">
                                    </dxtv:SplitterContentControl>
                                </ContentCollection>
                            </dxtv:SplitterPane>
                            <dxtv:SplitterPane AllowResize="True" Name="pnGrafico05">
                                <ContentCollection>
                                    <dxtv:SplitterContentControl runat="server">
                                    </dxtv:SplitterContentControl>
                                </ContentCollection>
                            </dxtv:SplitterPane>
                        </Panes>
                        <ContentCollection>
                            <dxtv:SplitterContentControl runat="server">
                            </dxtv:SplitterContentControl>
                        </ContentCollection>
                    </dxtv:SplitterPane>
                </Panes>
            </dxcp:ASPxSplitter>

        </div>
        <dxpc:ASPxPopupControl ID="pcEventosAtlPrj" runat="server" ClientInstanceName="pcEventosAtlPrj"
            HeaderText="Atualizações"
            Width="800px" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table cellspacing="1" class="style4">
                        <tr>
                            <td class="style10">
                                <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
                                    style="width: 100%">
                                    <tr runat="server">
                                        <td runat="server" style="padding: 3px" valign="top">
                                            <table>
                                                <tr>
                                                    <td>
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
                                                                <dxm:MenuItem ClientVisible="False" Name="btnLayout" Text="" ToolTip="Layout">
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
                                <dxwgv:ASPxGridView ID="gvEventosAtlPrj" runat="server"
                                    AutoGenerateColumns="False" ClientInstanceName="gvEventosAtlPrj"
                                    KeyFieldName="IDItemAtualizacao"
                                    Width="100%">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="ID Item" FieldName="IDItemAtualizacao"
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Item"
                                            FieldName="DescricaoItemAtualizacao" ShowInCustomizationForm="True"
                                            VisibleIndex="0" Width="115px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Evento"
                                            FieldName="DescricaoUltimaAtualizacaoItem" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Data"
                                            FieldName="DataUltimaAtualizacaoItem" ShowInCustomizationForm="True"
                                            VisibleIndex="2" SortIndex="0" SortOrder="Descending" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Usuário"
                                            FieldName="IDUsuarioUltimaAtualizacao" ShowInCustomizationForm="True"
                                            VisibleIndex="3" Width="200px">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="250" />
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="1" class="style4">
                                    <tr>
                                        <td align="right">&nbsp;</td>
                                        <td class="style11">&nbsp;</td>
                                        <td align="right">
                                            <dxe:ASPxButton ID="btnFecharEventosAtlPrj" runat="server"
                                                Text="Fechar" Width="90px" AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {
	pcEventosAtlPrj.Hide();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvEventosAtlPrj"
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

    </form>
</body>
</html>
