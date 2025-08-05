<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="PainelIndicadores.aspx.cs" Inherits="ListaIndicadores"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

        <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <style type="text/css">
        .branco
        {
            background: white;
        }
        .colorido
        {
            background: #F3B678;
        }
        .style2
        {
            width: 10px;
            height: 10px;
        }
        .style3
        {
            height: 10px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        if (window.top.lpAguardeMasterPage)
            window.top.lpAguardeMasterPage.Hide();
    </script>

    <table style=" width: 100%;">
        <tr>
            <td class="style2">
            </td>
            <td class="style3">
            </td>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoIndicador"
                    AutoGenerateColumns="False" Width="100%" 
                    ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                    OnProcessColumnAutoFilter="gvDados_ProcessColumnAutoFilter" OnCustomCallback="gvDados_CustomCallback">
                    <ClientSideEvents ContextMenu="OnContextMenu" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Nome do Indicador" VisibleIndex="1" FieldName="NomeIndicador"
                            Name="NomeIndicador" Width="140px">
                            <Settings AllowGroup="False" AutoFilterCondition="Contains" AllowAutoFilter="True" />
                            <DataItemTemplate>
                                <%# getLinkIndicador()%>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Responsável pelo Indicador" VisibleIndex="2" Width="160px"
                            FieldName="NomeResponsavel" Name="NomeResponsavel">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" Caption="Responsável pela Atualização" Width="160px" Name="NomeResponsavelAtualizacao" FieldName="NomeResponsavelAtualizacao">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Meta Descritiva" VisibleIndex="4" Width="100px" FieldName="Meta"
                            Name="Meta">
                            <Settings AllowGroup="False" AllowAutoFilter="True" AllowDragDrop="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Unidade do Indicador " VisibleIndex="5" Width="120px"
                            FieldName="NomeUnidadeNegocio" Name="NomeUnidadeNegocio">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption=" " FieldName="Desempenho" Name="Desempenho" VisibleIndex="0" Width="140px">
                            <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif' /&gt;">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="True" AllowSort="False"  ShowFilterRowMenu="False" ShowInFilterControl="False" />
                            <HeaderStyle  HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderCaptionTemplate>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
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
                                                            <dxtv:MenuItem Name="btnSalvarLayout" Text="Salvar" ToolTip="Salvar Layout">
                                                                <Image IconID="save_save_16x16">
                                                                </Image>
                                                            </dxtv:MenuItem>
                                                            <dxtv:MenuItem Name="btnRestaurarLayout" Text="Restaurar" ToolTip="Restaurar Layout">
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
                            </HeaderCaptionTemplate>
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Mapa(s) Estratégico(s)" FieldName="MapasEstrategicos" VisibleIndex="6" Width="150px">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Órgão" FieldName="Orgao" VisibleIndex="7" Width="150px">
                        </dxtv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AutoExpandAllGroups="True" EnableCustomizationWindow="true">
                    </SettingsBehavior>
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                        ShowHeaderFilterBlankItems="False" HorizontalScrollBarMode="Auto"></Settings>
                    <SettingsText CommandClearFilter="Limpar filtro" CustomizationWindowCaption="Colunas Disponíveis">
                    </SettingsText>
                    <SettingsPopup>
                        <CustomizationWindow Height="200px" Width="200px" HorizontalAlign="LeftSides" VerticalAlign="TopSides"
                            VerticalOffset="200" />
                    </SettingsPopup>
                    <StylesPopup>
                    <HeaderFilter>
                    <Content ></Content>
                    </HeaderFilter>
                    </StylesPopup>
                </dxwgv:ASPxGridView>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <dxm:ASPxPopupMenu ID="headerMenu" runat="server" ClientInstanceName="headerMenu"
        >
        <Items>
            <dxm:MenuItem Text="Ocultar Coluna" Name="HideColumn">
            </dxm:MenuItem>
            <dxm:MenuItem Text="Mostrar/Ocultar Colunas Disponíveis" Name="ShowHideList">
            </dxm:MenuItem>
        </Items>
        <ClientSideEvents ItemClick="OnItemClick" />
    </dxm:ASPxPopupMenu>
    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
        Landscape="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
        ExportEmptyDetailGrid="True" PreserveGroupRowStates="False">
        <Styles>
            <Default >
            </Default>
            <Header >
            </Header>
            <Cell >
            </Cell>
            <Footer >
            </Footer>
            <GroupFooter >
            </GroupFooter>
            <GroupRow >
            </GroupRow>
            <Title ></Title>
        </Styles>
    </dxwgv:ASPxGridViewExporter>
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxhf:ASPxHiddenField>
</asp:Content>
