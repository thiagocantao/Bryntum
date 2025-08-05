<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="ListaPlanos.aspx.cs" Inherits="_PlanosPluri_ListaPlanos" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <div enableviewstate="false">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);">
            <tr height="26px">
                <td valign="middle" style="padding-left: 10px">
                    <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                        Font-Overline="False" Font-Strikeout="False" Text="Gestão de Planos Plurianuais"
                        EnableViewState="False"></asp:Label>
                </td>
                <td align="left" valign="middle"></td>
            </tr>
        </table>
    </div>
    <table cellspacing="0" class="auto-style1">
        <tr>
            <td style="padding: 5px; padding-bottom: 0px">
                <dxcp:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"  Width="100%" KeyFieldName="CodigoPlano" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                    <ClientSideEvents CustomButtonClick="function(s, e) {
	abreEdicaoPlano(s.GetRowKey(e.visibleIndex));
}" />
                    <Settings VerticalScrollBarMode="Auto" />
                    <Columns>
                        <dxtv:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="70px" ButtonRenderMode="Image">
                             <CustomButtons>
                                 <dxtv:GridViewCommandColumnCustomButton ID="btnEditar">
                                     <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                     </Image>
                                 </dxtv:GridViewCommandColumnCustomButton>
                             </CustomButtons>
                             <HeaderTemplate>
                                <table>
    <tr>
        <td align="center">
            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" 
                                                            ClientInstanceName="menu" 
                ItemSpacing="5px" onitemclick="menu_ItemClick" 
                                                            oninit="menu_Init">
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
                            </HeaderTemplate>
                        </dxtv:GridViewCommandColumn>
                        <dxtv:GridViewDataTextColumn Caption="Plano Plurianual" VisibleIndex="1" FieldName="NomePlano">
                           
                            <DataItemTemplate>
                               <%# getLinkPlano() %>
                            </DataItemTemplate>
                           
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Status do Plano" VisibleIndex="2" Width="250px" FieldName="TipoStatus">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Unidade de Negócio" VisibleIndex="3" Width="350px" FieldName="NomeUnidadeNegocio">
                        </dxtv:GridViewDataTextColumn>                        
                    </Columns>
                </dxcp:ASPxGridView>

    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" 
        ID="ASPxGridViewExporter1">
<Styles>
<Default ></Default>

<Header ></Header>

<Cell ></Cell>

<GroupFooter Font-Bold="True" ></GroupFooter>

<Title Font-Bold="True" ></Title>
</Styles>
</dxwgv:ASPxGridViewExporter>

                <script language="javascript" type="text/javascript">
                    gvDados.SetHeight(window.innerHeight - 160);
                </script>
            </td>
        </tr>
    </table>
</asp:Content>

