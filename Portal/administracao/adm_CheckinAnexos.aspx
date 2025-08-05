<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="adm_CheckinAnexos.aspx.cs" Inherits="administracao_adm_CheckinAnexos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px"
                valign="middle">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Desbloqueio de Anexos">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td id="ConteudoPrincipal">

                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                    KeyFieldName="CodigoAnexo"
                    Width="99%" OnCustomCallback="gvDados_CustomCallback">
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
     if(e.buttonID == &quot;btnCheckin&quot;)
     {
          var sim = confirm(traducao.adm_CheckinAnexos_ao_fazer_o_desbloqueio_as_atualiza__es_pendentes_ser_o_perdidas__deseja_realmente_desbloquear_o_anexo_);
          if(true == sim)
	      {
				gvDados.PerformCallback();
          }
          e.processOnServer = false;
     }	
}
" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="50px"
                            Caption=" ">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnCheckin" Text="Desbloquear">
                                    <Image Url="~/imagens/botoes/btnDesbloquear.png" />
                                </dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
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
                        <dxwgv:GridViewDataTextColumn Caption="Anexo" FieldName="Nome" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Bloqueio Feito Por"
                            FieldName="NomeUsuarioCheckout" VisibleIndex="2" Width="220px"
                            Name="NomeUsuarioCheckout">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Bloqueio Feito Em"
                            FieldName="dataCheckOut" VisibleIndex="3" Width="160px"
                            Name="dataCheckOut">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                    </Columns>
                    <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True" />
                </dxwgv:ASPxGridView>

                <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1"
                    OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                    <Styles>
                        <Default></Default>

                        <Header></Header>

                        <Cell></Cell>

                        <GroupFooter Font-Bold="True"></GroupFooter>

                        <Title Font-Bold="True"></Title>
                    </Styles>
                </dxwgv:ASPxGridViewExporter>

            </td>
        </tr>
    </table>
</asp:Content>


