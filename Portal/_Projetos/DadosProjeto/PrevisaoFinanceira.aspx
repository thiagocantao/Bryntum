<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrevisaoFinanceira.aspx.cs" Inherits="_Portfolios_frameProposta_FluxoCaixa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script type="text/javascript">
        var existeConteudoCampoAlterado = false;

        function conteudoCampoAlterado() 
        {
            existeConteudoCampoAlterado = true;
        }

        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight) - 60;
            grid.SetHeight(height);
        }

        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
    </script> 

    </head>
<body style="margin:0px; font-size:8pt; font-family:Verdana">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 10px; height: 10px;">
                </td>
                <td style="height: 10px">
                </td>
                <td style="width: 10px; height: 10px;">
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td>
                            <table cellpadding="0" cellspacing="0" ID="tbBotoes" runat="server" 
                                style="width:100%" >
                                <tr runat="server">
                                    <td runat="server" style="padding: 3px" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <dxm:ASPxMenu ID="menu0" runat="server" BackColor="Transparent" 
                                                        ClientInstanceName="menu" ItemSpacing="5px" oninit="menu_Init" 
                                                        onitemclick="menu_ItemClick">
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
                                                            <Border BorderStyle="None" />
                                                        </HoverStyle>
                                                        <Paddings Padding="0px" />
                                                        </ItemStyle>
                                                        <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                            <SelectedStyle>
                                                                <Border BorderStyle="None" />
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
                     <div id="divGrid" style="visibility:hidden">
                    <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" Font-Names="Verdana"
                        Font-Size="8pt" KeyFieldName="_CodigoConta;CodigoPessoaParticipe" Width="100%" OnHtmlRowCreated="grid_HtmlRowCreated" 
                        onsummarydisplaytext="grid_SummaryDisplayText" OnBatchUpdate="grid_BatchUpdate" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared">
                        <ClientSideEvents BatchEditStartEditing="function(s, e) {
	 indexAtual = e.visibleIndex;
	if(hfGeral.Get(e.focusedColumn.fieldName + '_' + e.visibleIndex) == 'N')
		e.cancel = true;
}" Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" 
                            ShowFooter="True" ShowGroupButtons="False" ShowGroupPanel="True" 
                            ShowGroupFooter="VisibleAlways" />
                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" />

<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>

                        <Styles>
                            <GroupRow BackColor="#EBEBEB" Font-Bold="True">
                            </GroupRow>
                            <Footer Font-Bold="True">
                            </Footer>
                        </Styles>
                    </dxwgv:ASPxGridView>
                         </div>

    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1" 
                                onrenderbrick="ASPxGridViewExporter1_RenderBrick">
<Styles>
<Default Font-Names="Verdana" Font-Size="8pt"></Default>

<Header Font-Names="Verdana" Font-Size="9pt"></Header>

<Cell Font-Names="Verdana" Font-Size="8pt"></Cell>

<GroupFooter Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></GroupFooter>

<Title Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></Title>
</Styles>
</dxwgv:ASPxGridViewExporter>

    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>

                </td>
                <td style="width: 10px">
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;</td>
                <td align="right" style="padding-top: 5px">
                            <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" 
                                Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnFechar" 
                                ClientVisible="False">
<ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    window.top.fechaModal();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>





                </td>
                <td style="width: 10px">
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
