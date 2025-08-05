<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="adm_CheckinFormularios.aspx.cs" Inherits="administracao_adm_CheckinFormularios" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript">
        function desbloquearFormulario() {
            gvDados.PerformCallback();
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif); height: 26px"
                valign="middle">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" style="padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Desbloqueio de Formulários">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="padding-left: 10px; padding-top: 10px; padding-right: 10px;">

                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                    KeyFieldName="CodigoFormulario"
                    Width="100%" OnCustomCallback="gvDados_CustomCallback"
                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
     if(e.buttonID == &quot;btnCheckin&quot;)
     {
            e.processOnServer = false;
            var mensagem = &quot;Ao fazer o desbloqueio as atualizações pendentes serão perdidas. Deseja realmente desbloquear o formulário?&quot;;
            window.top.mostraMensagem(mensagem, 'confirmacao', true, true, desbloquearFormulario);
     }	
}
"
                        EndCallback="function(s, e) {
       if(s.cp_Sucesso != &quot;&quot;)
       {
                 window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
       }
       else
       {
                 if(s.cp_Erro != &quot;&quot;)
                {
                         window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
                 }
       }
}" />
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
                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Bloqueio Feito Por"
                            FieldName="NomeUsuario" VisibleIndex="3" Width="220px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Bloqueio Feito Em"
                            FieldName="DataCheckOut" VisibleIndex="4" Width="160px"
                            Name="DataCheckout">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                            <Settings ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Formulário" FieldName="DescricaoFormulario"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
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


