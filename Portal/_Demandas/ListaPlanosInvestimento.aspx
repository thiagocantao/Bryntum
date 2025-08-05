<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="ListaPlanosInvestimento.aspx.cs" Inherits="_Demandas_ListaDemandas"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <style type="text/css">
        .branco {
            background: white;
        }

        .colorido {
            background: #F3B678;
        }

        .style2 {
            width: 10px;
            height: 10px;
        }

        .style3 {
            height: 10px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        if (window.top.lpAguardeMasterPage)
            window.top.lpAguardeMasterPage.Hide();
    </script>
    <table border="0" cellpadding="0" cellspacing="0"
        width: 100%; height: 28px">
        <tr>
            <td align="left" style="padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True" CssClass="titlePage"
                    Font-Overline="False" Font-Strikeout="False"
                    Text="Planos de Investimento"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
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
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoProjeto"
                    AutoGenerateColumns="False" Width="100%" 
                    ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                    OnProcessColumnAutoFilter="gvDados_ProcessColumnAutoFilter" OnCustomCallback="gvDados_CustomCallback">
                    <ClientSideEvents ContextMenu="OnContextMenu" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Ações" FieldName="CodigoProjeto" Name="CodigoProjeto"
                            VisibleIndex="0" Width="40px">
                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" AllowDragDrop="False"
                                AllowGroup="False" AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False"
                                ShowInFilterControl="False" />
                            <Settings AllowDragDrop="False" AllowAutoFilterTextInputTimer="False" AllowAutoFilter="False"
                                ShowFilterRowMenu="False" AllowHeaderFilter="False" ShowInFilterControl="False"
                                AllowSort="False" AllowGroup="False"></Settings>
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <%# getBotaoGraficoFluxo() %>
                                        </td>
                                        <td>
                                            <%# getBotaoInteragirFluxo() %>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                            <HeaderStyle  HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderCaptionTemplate>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                            <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                                <Image IconID="save_save_16x16">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout" Name="btnRestaurarLayout">
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
                            </HeaderCaptionTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Título" VisibleIndex="2" FieldName="TituloProjeto"
                            Name="TituloProjeto" Width="300px">
                            <Settings AllowGroup="False" AutoFilterCondition="Contains" AllowAutoFilter="True" />
                            <DataItemTemplate>
                                <%# getDescricaoObra()%>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Órgão Demandante" VisibleIndex="3" Width="40px"
                            FieldName="SiglaOrgaoDemandante" Name="SiglaOrgaoDemandante">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" Caption="Responsável" Width="200px"
                            FieldName="NomeResponsavel" Name="NomeResponsavel">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="8" Width="180px" FieldName="StatusProjeto"
                            Name="StatusProjeto">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowGroup="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Etapa Atual" VisibleIndex="9" Width="180px"
                            FieldName="EtapaAtualProcesso" Name="EtapaAtualProcesso">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ano" FieldName="Ano" Name="Ano" VisibleIndex="13"
                            Width="80px">
                            <Settings AllowGroup="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AutoExpandAllGroups="True" EnableCustomizationWindow="true">
                    </SettingsBehavior>
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                        ShowHeaderFilterBlankItems="False"></Settings>
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
