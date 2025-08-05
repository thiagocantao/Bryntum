<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/novaCdis.master" CodeFile="DadoWebService.aspx.cs" Inherits="administracao_DadoWebService" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
   <div style="padding: 5px">
       <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="idDadoWebService" AutoGenerateColumns="False" Width="100%" ID="gvDados">
        <ClientSideEvents
            CustomButtonClick="function(s, e) {
     s.SetFocusedRowIndex(e.visibleIndex);
     var codigoRegistroIntegracao =  s.GetRowKey(e.visibleIndex);
     var sHeaderTitulo = 'Detalhes - Dados WebService';
     if(e.buttonID == 'btnEditar')
     {
           var sUrl = window.top.pcModal.cp_Path + 'administracao/DadoWebService_popup.aspx?C=' + codigoRegistroIntegracao;
           window.top.showModal(sUrl, sHeaderTitulo, 800, null, atualizaGrid, null);
    }
     else if(e.buttonID == 'btnExcluir')
     {            
            excluirRegistroIntegracao(codigoRegistroIntegracao);
     }
      else if(e.buttonID == 'btnAtualizarDados')
      {
              s.GetRowValues( e.visibleIndex, 'IniciaisDadoWebService', atualizaDadoIntegracao);
     }
}"
            Init="function(s, e) {
var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
s.SetHeight(sHeight);
}"></ClientSideEvents>
        <Columns>
            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                        </Image>
                    </dxwgv:GridViewCommandColumnCustomButton>
                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                        </Image>
                    </dxwgv:GridViewCommandColumnCustomButton>
                    <dxtv:GridViewCommandColumnCustomButton ID="btnAtualizarDados">
                        <Image Url="~/imagens/refresh.png" ToolTip="Atualizar os dados da integração">
                        </Image>
                    </dxtv:GridViewCommandColumnCustomButton>
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
            <dxwgv:GridViewDataTextColumn FieldName="TituloDadoWebService" Name="TituloDadoWebService"
                Caption="Título" VisibleIndex="1">
                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="DescricaoDadoWebService" Name="DescricaoDadoWebService" Caption="Descrição"
                VisibleIndex="2">
                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoAssociacao" Name="DescricaoTipoAssociacao" VisibleIndex="4" Caption="Tipo de Associação" Width="150px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoObjetoAssociado" VisibleIndex="3" Visible="False" Caption="Código Tipo Objeto Associado" Width="250px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="IniciaisDadoWebService" Name="IniciaisDadoWebService"
                Caption="Iniciais" VisibleIndex="5" Width="200px">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="idDadoWebService" FieldName="idDadoWebService" Visible="False" VisibleIndex="6">
            </dxtv:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
        <SettingsPager PageSize="100">
        </SettingsPager>
        <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible" ShowGroupedColumns="True"
            ShowFilterRow="True"></Settings>

        <SettingsPopup>
            <HeaderFilter MinHeight="140px"></HeaderFilter>
        </SettingsPopup>

        <SettingsText GroupPanel="Para agrupar, arraste uma coluna aqui"></SettingsText>
    </dxwgv:ASPxGridView>
   </div>    
    <dxcp:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados">
    </dxcp:ASPxGridViewExporter>
    <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
    </dxcp:ASPxLoadingPanel>
    <dxcp:ASPxCallback ID="callbackExcluiRegistro" runat="server" ClientInstanceName="callbackExcluiRegistro" OnCallback="callbackExcluiRegistro_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
        lpLoading.Hide();  
        if(s.cpErro !== '')
         {
                   window.top. mostraMensagem(s.cpErro, 'erro', true, false, null);
         }
         else if(s.cpSucesso !== '')
         {
            gvDados.Refresh();
            window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3400);

        }
}" />
    </dxcp:ASPxCallback>
    <dxcp:ASPxCallback ID="callbackAtualizaDadoIntegracao" runat="server" ClientInstanceName="callbackAtualizaDadoIntegracao" OnCallback="callbackAtualizaDadoIntegracao_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
        lpLoading.Hide();  
        if(s.cpErro !== '')
         {
                   window.top. mostraMensagem(s.cpErro, 'erro', true, false, null);
         }
         else if(s.cpSucesso !== '')
         {
                 gvDados.Refresh();
                 window.top. mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3400);
        }
}" />
    </dxcp:ASPxCallback>
</asp:Content>
