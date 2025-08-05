<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/novaCdis.master" CodeFile="adm_AssociacaoDeCR.aspx.cs" Inherits="administracao_adm_AssociacaoDeCR" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>


<%@ Register Assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <dxcp:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" PaperKind="A4">
    </dxcp:ASPxGridViewExporter>
    <div style="padding-top: 5px; padding-right: 5px; padding-left: 10px">

        <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoProjeto" ClientInstanceName="gvDados" Width="100%" ID="gvDados" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnCustomCallback="gvDados_CustomCallback">
            <ClientSideEvents CustomButtonClick="function(s, e) {
       s.SetFocusedRowIndex(e.visibleIndex);
       if(e.buttonID == 'btnEditar')
       {
               s.GetRowValues( e.visibleIndex, &quot;CodigoProjeto;Ano;NomeProjeto&quot;,  mostraPopupSelecao);
       }
}"
                Init="function(s, e) {
var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
s.SetHeight(sHeight);
}"></ClientSideEvents>
            <SettingsPager PageSize="100"></SettingsPager>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowGroupedColumns="True" VerticalScrollBarMode="Visible"></Settings>
            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
            <SettingsPopup>
                <HeaderFilter MinHeight="140px"></HeaderFilter>
            </SettingsPopup>
            <SettingsText GroupPanel="Para agrupar, arraste uma coluna aqui"></SettingsText>
            <Columns>
                <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" ShowInCustomizationForm="True" Width="75px" VisibleIndex="0">
                    <CustomButtons>
                        <dxcp:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                        </dxcp:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td align="center">
                                    <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                        ClientInstanceName="menu"
                                        ItemSpacing="5px" OnItemClick="menu_ItemClick1"
                                        OnInit="menu_Init1" ClientVisible="false">
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

                                    <dxcp:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/imagens/menuExportacao/xls.png" Width="16" Height="16">
                                        <ClientSideEvents Click="function(s,e){ webDocumentViewer.ExportTo('xls');}" />
                                    </dxcp:ASPxImage>

                                </td>
                            </tr>
                        </table>

                    </HeaderTemplate>
                </dxcp:GridViewCommandColumn>
                <dxcp:GridViewDataTextColumn FieldName="NomeProjeto" ShowInCustomizationForm="True" Caption="Nome" VisibleIndex="1">
                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                </dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="ListaCarteiras" ShowInCustomizationForm="True" Caption="Carteiras" VisibleIndex="2" Name="colunaCarteiras">
                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                </dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="StatusProjeto" ShowInCustomizationForm="True" Width="250px" Caption="Status" VisibleIndex="3">
                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                </dxcp:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" VisibleIndex="4" Visible="False">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Ano" FieldName="Ano" Visible="False" VisibleIndex="5">
                </dxtv:GridViewDataTextColumn>
            </Columns>
        </dxcp:ASPxGridView>
        <div id="container-viewer" style="display:;">
            <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server" ClientInstanceName="webDocumentViewer" OnLoad="ASPxWebDocumentViewer1_Load">
            </dx:ASPxWebDocumentViewer>
        </div>
    </div>
</asp:Content>
