<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_GRID_crs_do_SESCOOP.aspx.cs" Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_GRID_crs_do_SESCOOP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .iniciaisMaiusculas{
            text-transform:capitalize !important
        }
    </style>
    <title></title>
    <script type="text/javascript">
        function LimpaTelaRelTCU()
        {
            gvDados.SetVisible(false);
            menu.GetItemByName("btnExportar").SetVisible(false);
        };
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 5px" valign="top"></td>
                                <td valign="top">
                                    <dxcp:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados">
                                    </dxcp:ASPxGridViewExporter>
                                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                    </dxhf:ASPxHiddenField>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top"></td>
                                <td colspan="1" valign="top">
                                    <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="dxeBinImgCPnlSys">
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Ano" Theme="MaterialCompact">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Trimestre" Theme="MaterialCompact">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-right: 5px">
                                                            <dxcp:ASPxComboBox ID="comboAnos" runat="server" ClientInstanceName="comboAnos" ValueType="System.String" Width="100%" Theme="MaterialCompact">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { LimpaTelaRelTCU(); }" />
                                                            </dxcp:ASPxComboBox>
                                                        </td>
                                                        <td style="padding-right: 5px">
                                                            <dxcp:ASPxComboBox ID="comboTrimestre" runat="server" ClientInstanceName="comboTrimestre" Width="100%" Theme="MaterialCompact">
                                                                <Items>
                                                                    <dxtv:ListEditItem Text="Primeiro" Value="1" />
                                                                    <dxtv:ListEditItem Text="Segundo" Value="2" />
                                                                    <dxtv:ListEditItem Text="Terceiro" Value="3" />
                                                                    <dxtv:ListEditItem Text="Quarto" Value="4" />
                                                                </Items>
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { LimpaTelaRelTCU(); }" />
                                                            </dxcp:ASPxComboBox>
                                                        </td>
                                                        <td>
                                                            <dxcp:ASPxRadioButtonList ID="radioReceitaDespesa" runat="server" RepeatDirection="Horizontal" Width="100%" ClientInstanceName="radioReceitaDespesa" SelectedIndex="0" Theme="MaterialCompact" Height="15px">
                                                                <Paddings Padding="0px" />
                                                                <Items>
                                                                    <dxtv:ListEditItem Text="Receita" Value="R" Selected="True" />
                                                                    <dxtv:ListEditItem Text="Despesa" Value="D" />
                                                                </Items>
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { LimpaTelaRelTCU(); }" />
                                                            </dxcp:ASPxRadioButtonList>
                                                        </td>
                                                        <td align="right">
                                                            <dxcp:ASPxButton ID="btnSelecionar" runat="server" ClientInstanceName="btnSelecionar" Text="Selecionar" Theme="MaterialCompact" CssClass="iniciaisMaiusculas">
                                                            </dxcp:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr id="Tr1" runat="server">
                                            <td id="Td1" runat="server" style="padding: 3px" valign="top">
                                                <table cellpadding="0" cellspacing="0" style="height: 22px">
                                                    <tr>
                                                        <td>
                                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init"
                                                                OnItemClick="menu_ItemClick">
                                                                <Paddings Padding="0px" />
                                                                <Items>
                                                                    <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir" Visible="false">
                                                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                        </Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                        <Items>
                                                                            <dxm:MenuItem Name="btnCSV" Text="CSV" ToolTip="Exportar para CSV">
                                                                                <Image Url="~/imagens/menuExportacao/iconoCSV.png">
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
                                                                    <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout" Visible="false">
                                                                        <Items>
                                                                            <dxm:MenuItem Name="btnSalvarLayout" Text="Salvar" ToolTip="Salvar Layout">
                                                                                <Image IconID="save_save_16x16">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnRestaurarLayout" Text="Restaurar"
                                                                                ToolTip="Restaurar Layout">
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
                                    <div id="divPivotGrid" runat="server" style="width: 100%; overflow: auto">
                                        <dxtv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" KeyFieldName="COD_CONTA" Width="100%">
                                            <Columns>
                                                <dxtv:GridViewDataTextColumn Caption="COD_CONTA" FieldName="COD_CONTA" Name="COD_CONTA" VisibleIndex="0" Width="150px">
                                                    <Settings AllowAutoFilter="True" AllowHeaderFilter="True" ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="False" ShowInFilterControl="False" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="DESC_CONTA" FieldName="DESC_CONTA" Name="DESC_CONTA" VisibleIndex="1" Width="230px">
                                                    <Settings AllowAutoFilter="True" AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="TRANSPOSTO" FieldName="TRANSPOSTO" Name="TRANSPOSTO" VisibleIndex="5" Width="230px">
                                                    <PropertiesTextEdit DisplayFormatString="n2">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="REALIZADO" FieldName="REALIZADO" Name="REALIZADO" VisibleIndex="6" Width="230px">
                                                    <PropertiesTextEdit DisplayFormatString="n2">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="SUPLEMENTADO" FieldName="SUPLEMENTADO" Name="SUPLEMENTADO" VisibleIndex="4" Width="230px">
                                                    <PropertiesTextEdit DisplayFormatString="n2">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="RETIFICADO" FieldName="RETIFICADO" Name="RETIFICADO" VisibleIndex="3" Width="230px">
                                                    <PropertiesTextEdit DisplayFormatString="n2">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowAutoFilter="True" AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="OBS" FieldName="OBS" Name="OBS" VisibleIndex="7" Width="300px">
                                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="ORCADO" FieldName="ORCADO" Name="ORCADO" ShowInCustomizationForm="False" VisibleIndex="2" Width="230px">
                                                    <PropertiesTextEdit DisplayFormatString="n2">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowAutoFilter="True" AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized" />
                                            <ClientSideEvents Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 120;
       s.SetHeight(sHeight);				
}" />
                                            <SettingsPager PageSize="100">
                                            </SettingsPager>
                                            <Settings ShowFooter="False" ShowGroupedColumns="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" ShowFilterRow="True" />

                                            <SettingsPopup>
                                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                                            </SettingsPopup>

                                            <SettingsText GroupPanel="Para agrupar, arraste uma coluna aqui" />
                                        </dxtv:ASPxGridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
