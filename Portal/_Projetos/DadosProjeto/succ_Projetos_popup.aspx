<%@ Page Language="C#" AutoEventWireup="true" CodeFile="succ_Projetos_popup.aspx.cs" Inherits="_Projetos_DadosProjeto_succ_Projetos_popup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .Tabela {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                        <dxcp:ASPxPageControl ID="pgControl" runat="server" ActiveTabIndex="0" ClientInstanceName="pgControl" Width="100%">
                            <TabPages>
                                <dxtv:TabPage Text="Principal">
                                    <TabStyle >
                                    </TabStyle>
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="dv01" runat="server">
                                                <table width="99%">
                                                    <tr>
                                                        <td>
                                                            <table class="Tabela">
                                                                <tr>
                                                                    <td width="25%">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel33" runat="server" Font-Bold="False"  Text="IJ:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td width="25%">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="False"  Text="Assinatura:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td width="25%">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="False"  Text="Início Vigência:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td width="50%">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel29" runat="server" Font-Bold="False"  Text="Término Vigência:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="txtIJ" runat="server" ClientInstanceName="txtIJ"  Width="100%" ReadOnly="True">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="dtAssinatura" runat="server" ClientInstanceName="dtAssinatura" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="dtInicioVigencia" runat="server" ClientInstanceName="dtInicioVigencia" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxTextBox ID="dtTerminoVigencia" runat="server" ClientInstanceName="dtTerminoVigencia" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel30" runat="server" Font-Bold="False"  Text="Fornecedor:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxTextBox ID="txtFornecedor" runat="server" ClientInstanceName="txtFornecedor"  Width="100%" ReadOnly="True">
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                            </dxtv:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel343" runat="server" Font-Bold="False"  Text="Tipo:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td width="33%">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel35" runat="server" Font-Bold="False"  Text="Natureza:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td width="33%">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel36" runat="server" Font-Bold="False"  Text="Valor Contrato:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="34%">
                                                                        <dxtv:ASPxTextBox ID="txtTipo" runat="server" ClientInstanceName="txtTipo" DisplayFormatString="c"  Width="100%" ReadOnly="True">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxTextBox ID="txtNatureza" runat="server" ClientInstanceName="txtNatureza"  Width="100%" ReadOnly="True">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxSpinEdit ID="spValorContrato" runat="server" ClientInstanceName="spValorContrato" DisplayFormatString="{0:c2}" Number="0" Width="100%" ReadOnly="True">
                                                                            <SpinButtons ClientVisible="False">
                                                                            </SpinButtons>
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel37" runat="server" Font-Bold="False"  Text="Gestor:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxTextBox ID="txtGestor" runat="server" ClientInstanceName="txtGestor"  MaxLength="255" Width="100%" ReadOnly="True">
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                            </dxtv:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel38" runat="server" Font-Bold="False"  Text="Objeto:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxMemo ID="memoObjeto" runat="server" ClientInstanceName="memoObjeto" Rows="5" Width="100%" ReadOnly="True">
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                            </dxtv:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Text="Previsão Orçamentária">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <table class="Tabela">
                                                <tr>
                                                    <td>
                                                        <table class="Tabela">
                                                            <tr>
                                                                <td width="25%">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel381" runat="server" Font-Bold="False"  Text="IJ:">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                                <td width="25%">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel382" runat="server" Font-Bold="False"  Text="Assinatura:">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                                <td width="25%">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel383" runat="server" Font-Bold="False"  Text="Início Vigência:">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                                <td width="50%">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel384" runat="server" Font-Bold="False"  Text="Término Vigência:">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px">
                                                                    <dxtv:ASPxTextBox ID="txtIJ3" runat="server" ClientInstanceName="txtIJ"  ReadOnly="True" Width="100%">
                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </ReadOnlyStyle>
                                                                    </dxtv:ASPxTextBox>
                                                                </td>
                                                                <td style="padding-right: 10px">
                                                                    <dxtv:ASPxTextBox ID="dtAssinatura3" runat="server" ClientInstanceName="dtAssinatura" ReadOnly="True" Width="100%">
                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </ReadOnlyStyle>
                                                                    </dxtv:ASPxTextBox>
                                                                </td>
                                                                <td style="padding-right: 10px">
                                                                    <dxtv:ASPxTextBox ID="dtInicioVigencia3" runat="server" ClientInstanceName="dtInicioVigencia" ReadOnly="True" Width="100%">
                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </ReadOnlyStyle>
                                                                    </dxtv:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxtv:ASPxTextBox ID="dtTerminoVigencia3" runat="server" ClientInstanceName="dtTerminoVigencia" ReadOnly="True" Width="100%">
                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </ReadOnlyStyle>
                                                                    </dxtv:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxLabel ID="ASPxLabel385" runat="server" Font-Bold="False"  Text="Previsão por Trimestre:">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxGridView ID="gvPrevisaoOrcamentaria0" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPrevisaoOrcamentaria0"  Width="100%" KeyFieldName="ExercicioPrevisao;ClassificacaoOrcamentaria" OnDetailRowExpandedChanged="gvPrevisaoOrcamentaria0_DetailRowExpandedChanged">
                                                            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                                            <Templates>
                                                                <DetailRow>
                                                                    <table cellpadding="0" cellspacing="0" class="Tabela">
                                                                        <tr>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel386" runat="server" Font-Bold="True"  Text="Previsão Por Trimestre">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxtv:ASPxGridView ID="gvTrimestrePrevisaoOrcamentaria" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvTrimestrePrevisaoOrcamentaria"  KeyFieldName="TrimestrePrevisao" OnBeforePerformDataSelect="gvTrimestrePrevisaoOrcamentaria_BeforePerformDataSelect" OnLoad="gvTrimestrePrevisaoOrcamentaria_Load" Width="100%">
                                                                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                    </SettingsPager>
                                                                                    <Settings HorizontalScrollBarMode="Visible" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" />
                                                                                    <SettingsBehavior AllowGroup="False" AllowSort="False" />
                                                                                    <Columns>
                                                                                        <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="#" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="50px">
                                                                                            <HeaderTemplate>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td align="center">
                                                                                                            <dxtv:ASPxMenu ID="menuPrevisaoOrc5" runat="server" BackColor="Transparent" ClientInstanceName="menuPrevisaoOrc" ItemSpacing="5px" OnInit="menuPrevisaoOrc_Init" OnItemClick="menuPrevisaoOrc_ItemClick">
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
                                                                                                            </dxtv:ASPxMenu>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </HeaderTemplate>
                                                                                        </dxtv:GridViewCommandColumn>
                                                                                        <dxtv:GridViewDataTextColumn Caption="Trimestre" FieldName="TrimestrePrevisao" ShowInCustomizationForm="True" VisibleIndex="1" Width="70px">
                                                                                        </dxtv:GridViewDataTextColumn>
                                                                                        <dxtv:GridViewDataTextColumn Caption="Valor Previsto" FieldName="ValorPrevisto" ShowInCustomizationForm="True" VisibleIndex="5" Width="100%">
                                                                                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                                            </PropertiesTextEdit>
                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                        </dxtv:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                        <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorPrevisto" ShowInColumn="Valor Previsto" ShowInGroupFooterColumn="Valor Previsto" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                    </TotalSummary>
                                                                                    <Styles>
                                                                                        <CommandColumnItem>
                                                                                            <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                                                        </CommandColumnItem>
                                                                                    </Styles>
                                                                                </dxtv:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DetailRow>
                                                            </Templates>
                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <Settings HorizontalScrollBarMode="Visible" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" />
                                                            <SettingsBehavior AllowSort="False" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="#" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="30px">
                                                                    <HeaderTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <dxtv:ASPxMenu ID="menuPrevisaoOrc4" runat="server" BackColor="Transparent" ClientInstanceName="menuPrevisaoOrc" ItemSpacing="5px" OnInit="menuPrevisaoOrc_Init" OnItemClick="menuPrevisaoOrc_ItemClick">
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
                                                                                    </dxtv:ASPxMenu>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Exercício" FieldName="ExercicioPrevisao" ShowInCustomizationForm="True" VisibleIndex="1" Width="70px">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Classificação" FieldName="ClassificacaoOrcamentaria" ShowInCustomizationForm="True" VisibleIndex="3" Width="100%">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Valor Total" FieldName="ValorTotal" ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                    </PropertiesTextEdit>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                </dxtv:GridViewDataTextColumn>
                                                            </Columns>
                                                            <TotalSummary>
                                                                <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorPrevisto" ShowInColumn="Valor Previsto" ShowInGroupFooterColumn="Valor Previsto" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                            </TotalSummary>
                                                            <Styles>
                                                                <CommandColumnItem>
                                                                    <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                                </CommandColumnItem>
                                                            </Styles>
                                                        </dxtv:ASPxGridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Text="Dotações">
                                    <TabStyle >
                                    </TabStyle>
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="dv03" runat="server">
                                                <table width="99%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel364" runat="server" Font-Bold="False"  Text="IJ:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel365" runat="server" Font-Bold="False"  Text="Assinatura:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel366" runat="server" Font-Bold="False"  Text="Início Vigência:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxLabel ID="ASPxLabel367" runat="server" Font-Bold="False"  Text="Término Vigência:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="txtIJ1" runat="server" ClientInstanceName="txtIJ"  ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>

                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="dtAssinatura1" runat="server" ClientInstanceName="dtAssinatura" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>

                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="dtInicioVigencia1" runat="server" ClientInstanceName="dtInicioVigencia" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>

                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxTextBox ID="dtTerminoVigencia1" runat="server" ClientInstanceName="dtTerminoVigencia" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel379" runat="server" Font-Bold="False"  Text="Dotações e Itens de Despesa:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxGridView ID="gvDotacoes" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDotacoes"  KeyFieldName="IdItemDespesaFicha" OnDetailRowExpandedChanged="gvExercicioDotacoes_DetailRowExpandedChanged" Width="100%">
                                                                <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                                                <Templates>
                                                                    <DetailRow>
                                                                        <table cellpadding="0" cellspacing="0" class="Tabela">
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxLabel ID="ASPxLabel365" runat="server" Font-Bold="True"  Text="Empenhos e Pagamentos">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxGridView ID="gvTrimestreDotacoes" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvTrimestreDotacoes"  KeyFieldName="Trimestre" OnBeforePerformDataSelect="gvTrimestreDotacoes_BeforePerformDataSelect" OnLoad="gvTrimestreDotacoes_Load" Width="100%">
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <Settings HorizontalScrollBarMode="Visible" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollableHeight="110" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" />
                                                                                        <SettingsBehavior AllowGroup="False" AllowSort="False" />
                                                                                        <Columns>
                                                                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="#" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="50px">
                                                                                                <HeaderTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <dxtv:ASPxMenu ID="menuPrevisaoOrc1" runat="server" BackColor="Transparent" ClientInstanceName="menuPrevisaoOrc" ItemSpacing="5px" OnInit="menuPrevisaoOrc_Init" OnItemClick="menuPrevisaoOrc_ItemClick">
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
                                                                                                                </dxtv:ASPxMenu>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                            </dxtv:GridViewCommandColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Trimestre" FieldName="Trimestre" ShowInCustomizationForm="True" VisibleIndex="1" Width="70px">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Valor Empenhado" FieldName="ValorEmpenhado" ShowInCustomizationForm="True" VisibleIndex="3" Width="100%">
                                                                                                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                                                </PropertiesTextEdit>
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Valor Liquidado" FieldName="ValorLiquidado" ShowInCustomizationForm="True" VisibleIndex="4" Width="100%">
                                                                                                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                                                </PropertiesTextEdit>
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Valor Autorizado" FieldName="ValorAutorizado" ShowInCustomizationForm="True" VisibleIndex="2" Width="100%">
                                                                                                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                                                </PropertiesTextEdit>
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Valor Pago" FieldName="ValorPago" VisibleIndex="5" Width="100%">
                                                                                                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                                                </PropertiesTextEdit>
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <TotalSummary>
                                                                                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorAutorizado" ShowInColumn="Valor Autorizado" ShowInGroupFooterColumn="Valor Autorizado" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorEmpenhado" ShowInColumn="Valor Empenhado" ShowInGroupFooterColumn="Valor Empenhado" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorLiquidado" ShowInColumn="Valor Liquidado" ShowInGroupFooterColumn="Valor Liquidado" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorPago" ShowInColumn="Valor Pago" ShowInGroupFooterColumn="Valor Pago" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                        </TotalSummary>
                                                                                        <Styles>
                                                                                            <CommandColumnItem>
                                                                                                <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                                                            </CommandColumnItem>
                                                                                        </Styles>
                                                                                    </dxtv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 20px">
                                                                                    <dxtv:ASPxLabel ID="ASPxLabel366" runat="server" Font-Bold="True"  Text="Demandas CCG">
                                                                                    </dxtv:ASPxLabel>
                                                                                    <dxtv:ASPxGridView ID="gvItensDeDespesa" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvItensDeDespesa"  KeyFieldName="CodigoControleCCG" OnBeforePerformDataSelect="gvItensDeDespesa_BeforePerformDataSelect" OnLoad="gvItensDeDespesa_Load1" Width="100%">
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <Settings HorizontalScrollBarMode="Visible" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollableHeight="110" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" />
                                                                                        <SettingsBehavior AllowGroup="False" AllowSort="False" />
                                                                                        <Columns>
                                                                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="#" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="50px">
                                                                                                <HeaderTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <dxtv:ASPxMenu ID="menuPrevisaoOrc2" runat="server" BackColor="Transparent" ClientInstanceName="menuPrevisaoOrc" ItemSpacing="5px" OnInit="menuPrevisaoOrc_Init" OnItemClick="menuPrevisaoOrc_ItemClick">
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
                                                                                                                </dxtv:ASPxMenu>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                            </dxtv:GridViewCommandColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Código" FieldName="CodigoControleCCG" ReadOnly="True" VisibleIndex="1" Width="100px">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Título" FieldName="TituloCCG" VisibleIndex="2" Width="100%">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <TotalSummary>
                                                                                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorAutorizado" ShowInColumn="Valor Autorizado" ShowInGroupFooterColumn="Valor Autorizado" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorEmpenhado" ShowInColumn="Valor Empenhado" ShowInGroupFooterColumn="Valor Empenhado" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorLiquidado" ShowInColumn="Valor Liquidado" ShowInGroupFooterColumn="Valor Liquidado" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorPago" ShowInColumn="Valor Pago" ShowInGroupFooterColumn="Valor Pago" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                                        </TotalSummary>
                                                                                        <Styles>
                                                                                            <CommandColumnItem>
                                                                                                <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                                                            </CommandColumnItem>
                                                                                        </Styles>
                                                                                    </dxtv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </DetailRow>
                                                                </Templates>
                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                </SettingsPager>
                                                                <Settings HorizontalScrollBarMode="Visible" ShowFilterRow="True" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" />
                                                                <SettingsBehavior AllowSort="False" AllowSelectSingleRowOnly="True" />

<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>
                                                                <Columns>
                                                                    <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="#" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="50px">
                                                                        <HeaderTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxtv:ASPxMenu ID="menu_Dotacoes" runat="server" BackColor="Transparent" ClientInstanceName="menu_Dotacoes" ItemSpacing="5px" OnInit="menu_Dotacoes_Init" OnItemClick="menu_Dotacoes_ItemClick">
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
                                                                                        </dxtv:ASPxMenu>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                    </dxtv:GridViewCommandColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Dotação" FieldName="IdDotacaoParcela" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1" Width="100%">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Item de despesa" FieldName="IdItemDespesaFicha" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2" Width="100%">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Dotação" FieldName="Dotacao" ShowInCustomizationForm="True" VisibleIndex="4" Width="220px">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Exercício" FieldName="Exercicio" ShowInCustomizationForm="True" VisibleIndex="3" Width="70px">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Item Despesa" FieldName="ItemDespesa" ShowInCustomizationForm="True" VisibleIndex="5" Width="100%">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Valor Parcela" FieldName="ValorParcela" ShowInCustomizationForm="True" VisibleIndex="6" Width="200px">
                                                                        <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                        </PropertiesTextEdit>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn FieldName="IdDotacaoParcela" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <TotalSummary>
                                                                    <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorParcela" ShowInColumn="Valor Parcela" ShowInGroupFooterColumn="Valor Parcela" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                </TotalSummary>
                                                                <Styles>
                                                                    <CommandColumnItem>
                                                                        <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                                    </CommandColumnItem>
                                                                </Styles>
                                                            </dxtv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Text="Convênios">
                                    <TabStyle >
                                    </TabStyle>
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="dv04" runat="server">
                                                <table cellpadding="0" cellspacing="0" class="Tabela">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel372" runat="server" Font-Bold="False"  Text="IJ:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel373" runat="server" Font-Bold="False"  Text="Assinatura:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel374" runat="server" Font-Bold="False"  Text="Início Vigência:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxLabel ID="ASPxLabel375" runat="server" Font-Bold="False"  Text="Término Vigência:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="txtIJConvenios" runat="server" ClientInstanceName="txtIJConvenios"  ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="dtAssinaturaConvenio" runat="server" ClientInstanceName="dtAssinaturaConvenio" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="dtInicioVigenciaConvenio" runat="server" ClientInstanceName="dtInicioVigenciaConvenio" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxTextBox ID="dtTerminoVigenciaConvenio" runat="server" ClientInstanceName="dtTerminoVigenciaConvenio" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <dxtv:ASPxLabel ID="ASPxLabel376" runat="server" Font-Bold="False"  Text="Partícipes:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxGridView ID="gvParticipesConvenio" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvParticipesConvenio"  Width="100%">
                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                </SettingsPager>
                                                                <Settings HorizontalScrollBarMode="Visible" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" />
                                                                <SettingsBehavior AllowSort="False" />

<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>
                                                                <Columns>
                                                                    <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="#" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="30px">
                                                                        <HeaderTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxtv:ASPxMenu ID="menuPrevisaoOrc2" runat="server" BackColor="Transparent" ClientInstanceName="menuPrevisaoOrc" ItemSpacing="5px" OnInit="menuPrevisaoOrc_Init" OnItemClick="menuPrevisaoOrc_ItemClick">
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
                                                                                        </dxtv:ASPxMenu>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                    </dxtv:GridViewCommandColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Tipo" FieldName="TipoParticipe" ShowInCustomizationForm="True" VisibleIndex="1" Width="200px">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Órgão" FieldName="Orgao" ShowInCustomizationForm="True" VisibleIndex="2" Width="100%">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <Styles>
                                                                    <CommandColumnItem>
                                                                        <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                                    </CommandColumnItem>
                                                                </Styles>
                                                            </dxtv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <dxtv:ASPxLabel ID="ASPxLabel377" runat="server" Font-Bold="False"  Text="Plano de Aplicação:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxGridView ID="gvPlanoAplicacaoConvenio" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPlanoAplicacaoConvenio"  Width="100%">
                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                </SettingsPager>
                                                                <Settings HorizontalScrollBarMode="Visible" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" />
                                                                <SettingsBehavior AllowSort="False" />

<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>
                                                                <Columns>
                                                                    <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="#" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="30px">
                                                                        <HeaderTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxtv:ASPxMenu ID="menuPrevisaoOrc3" runat="server" BackColor="Transparent" ClientInstanceName="menuPrevisaoOrc" ItemSpacing="5px" OnInit="menuPrevisaoOrc_Init" OnItemClick="menuPrevisaoOrc_ItemClick">
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
                                                                                        </dxtv:ASPxMenu>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                    </dxtv:GridViewCommandColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Natureza" FieldName="NaturezaDespesa" ShowInCustomizationForm="True" VisibleIndex="1" Width="100%">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Fonte" FieldName="Fonte" ShowInCustomizationForm="True" VisibleIndex="2" Width="100%">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Valor Empenhado" FieldName="ValorEmpenhado" ShowInCustomizationForm="True" VisibleIndex="4" Width="140px">
                                                                        <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                        </PropertiesTextEdit>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Valor Liquidado" FieldName="ValorLiquidado" ShowInCustomizationForm="True" VisibleIndex="5" Width="140px">
                                                                        <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                        </PropertiesTextEdit>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Valor Pago" FieldName="ValorPago" ShowInCustomizationForm="True" VisibleIndex="6" Width="140px">
                                                                        <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                                                        </PropertiesTextEdit>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                    </dxtv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <TotalSummary>
                                                                    <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorEmpenhado" ShowInColumn="Valor Empenhado" ShowInGroupFooterColumn="Valor Empenhado" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                    <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorLiquidado" ShowInColumn="Valor Liquidado" ShowInGroupFooterColumn="Valor Liquidado" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                    <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorPago" ShowInColumn="Valor Pago" ShowInGroupFooterColumn="Valor Pago" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                </TotalSummary>
                                                                <Styles>
                                                                    <CommandColumnItem>
                                                                        <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                                    </CommandColumnItem>
                                                                </Styles>
                                                            </dxtv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Text="Projetos">
                                    <TabStyle >
                                    </TabStyle>
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <div id="dv05" runat="server">
                                                <table width="99%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel368" runat="server" Font-Bold="False"  Text="IJ:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel369" runat="server" Font-Bold="False"  Text="Assinatura:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel370" runat="server" Font-Bold="False"  Text="Início Vigência:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxLabel ID="ASPxLabel371" runat="server" Font-Bold="False"  Text="Término Vigência:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="txtIJ2" runat="server" ClientInstanceName="txtIJ"  ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>

                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="dtAssinatura2" runat="server" ClientInstanceName="dtAssinatura" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>

                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="dtInicioVigencia2" runat="server" ClientInstanceName="dtInicioVigencia" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>

                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxTextBox ID="dtTerminoVigencia2" runat="server" ClientInstanceName="dtTerminoVigencia" ReadOnly="True" Width="100%">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxtv:ASPxTextBox>

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel378" runat="server" Font-Bold="False"  Text="Projetos Associados ao Instrumento Jurídico:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvProjetos"  KeyFieldName="CodigoProjeto" Width="100%">
                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                </SettingsPager>
                                                                <Settings HorizontalScrollBarMode="Visible" ShowFilterRow="True" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" />
                                                                <SettingsBehavior AllowSort="False" />

<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>
                                                                <Columns>
                                                                    <dxtv:GridViewDataTextColumn Caption="Projeto Associado" FieldName="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="2" Width="100%">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Valor Projeto" FieldName="ValorProjeto" ShowInCustomizationForm="True" VisibleIndex="3" Width="175px">
                                                                        <PropertiesTextEdit DisplayFormatString="c2">
                                                                        </PropertiesTextEdit>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="% Projeto" FieldName="PercentualProjeto" ShowInCustomizationForm="True" VisibleIndex="4" Width="180px">
                                                                        <PropertiesTextEdit DisplayFormatString="{0:n2}%">
                                                                        </PropertiesTextEdit>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="CodigoProjeto" FieldName="CodigoProjeto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewCommandColumn Caption="#" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="50px">
                                                                        <HeaderTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxtv:ASPxMenu ID="menu_gvProjeto" runat="server" BackColor="Transparent" ClientInstanceName="menu_gvProjeto" ItemSpacing="5px" OnInit="menu_gvProjeto_Init" OnItemClick="menu_gvProjeto_ItemClick">
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
                                                                                        </dxtv:ASPxMenu>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                    </dxtv:GridViewCommandColumn>
                                                                </Columns>
                                                                <TotalSummary>
                                                                    <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorProjeto" ShowInColumn="Valor Projeto" ShowInGroupFooterColumn="Valor Projeto" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                                                                    <dxtv:ASPxSummaryItem DisplayFormat="{0:n2}%" FieldName="PercentualProjeto" ShowInColumn="% Projeto" ShowInGroupFooterColumn="% Projeto" SummaryType="Sum" ValueDisplayFormat="{0:n2}%" />
                                                                </TotalSummary>
                                                                <Styles>
                                                                    <CommandColumnItem>
                                                                        <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                                    </CommandColumnItem>
                                                                </Styles>
                                                            </dxtv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                            </TabPages>
                        </dxcp:ASPxPageControl>
        <div style="width: 100%; text-align: right;">
                                    <dxe:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"
                                        Text="Fechar" AutoPostBack="False" Width="110px">
                                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                    </dxe:ASPxButton>
      </div>
            <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvPrevisaoOrcamentaria" ID="gvExporter" OnRenderBrick="gvExporter_RenderBrick">
                <Styles>
                    <Default >
                    </Default>
                    <Header Font-Bold="True" >
                    </Header>
                    <Cell >
                    </Cell>
                    <GroupFooter Font-Bold="True" >
                    </GroupFooter>
                    <GroupRow Font-Bold="True" >
                    </GroupRow>
                    <Title Font-Bold="True" ></Title>
                </Styles>
            </dxcp:ASPxGridViewExporter>
    </form>
</body>
</html>
