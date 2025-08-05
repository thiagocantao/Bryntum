<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="adm_CheckinProjetos.aspx.cs" Inherits="administracao_adm_CheckinProjetos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px" valign="middle">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" style="padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Desbloqueio de Projetos">
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
                    KeyFieldName="CodigoCronogramaProjeto"
                    Width="100%" OnCustomCallback="gvDados_CustomCallback"
                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
     if(e.buttonID == &quot;btnCheckin&quot;)
     {
          var funcObj = { funcaoClickOK: function(s, e){gvDados.PerformCallback(); } }
             window.top.mostraConfirmacao(traducao.adm_CheckinProjetos_ao_fazer_o_desbloqueio_as_atualiza__es_pendentes_ser_o_perdidas__deseja_realmente_desbloquear_o_projeto_,function(){funcObj['funcaoClickOK'](s, e)}, null);
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
                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Bloqueio Feito Por"
                            FieldName="NomeUsuarioCheckoutCronograma" VisibleIndex="3" Width="220px"
                            Name="NomeUsuarioCheckoutCronograma">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Bloqueio Feito Em"
                            FieldName="DataCheckoutCronograma" VisibleIndex="4" Width="160px"
                            Name="DataCheckoutCronograma">
                            <%--<PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Codigo do Projeto" FieldName="CodigoProjeto"
                            VisibleIndex="5" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Codigo Cronograma Projeto" FieldName="CodigoCronogramaProjeto"
                            Visible="False" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Tipo" FieldName="Tipo" VisibleIndex="1" Width="80px">
                        </dxtv:GridViewDataTextColumn>
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


