<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="ArvoresDoConhecimento.aspx.cs" Inherits="_Projetos_Administracao_LicoesAprendidas_ArvoresDoConhecimento" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
    </dxcp:ASPxLoadingPanel>
    <div style="margin:10px">
        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoElementoArvore"
            AutoGenerateColumns="False" Width="100%"
            ID="gvDados" OnRowInserting="gvDados_RowInserting" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnRowDeleting="gvDados_RowDeleting">
            <ClientSideEvents
                CustomButtonClick="function(s, e) {
      s.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == 'btnEditar')
     {       
                 if(e.buttonID == 'btnEditar')
                 {
                                      lpLoading.Show();
                                     s.GetRowValues( e.visibleIndex, 'CodigoElementoArvore',  
                                      function(codigo)
                                      {
                                                   var sUrl = '../../../../_Projetos/Administracao/LicoesAprendidas/popupArvoresConhecimentoEDT.aspx?CERARV=' + codigo;            
                                                   lpLoading.Hide();
                                                   window.top.showModal(sUrl, 'Arvore Conhecimento', null, null, function(){ gvDados.Refresh();  } );            
                                       });
                   }
      }
     else if(e.buttonID == 'btnExcluir')
     {
               	    var funcObj = { funcaoClickOK: function () { s.DeleteRow(e.visibleIndex); } }
                     window.top.mostraConfirmacao('Excluir a árvore de conhecimento?', function () { funcObj['funcaoClickOK']() }, null);
                
                
     }
}"
                Init="function(s, e) {
    var height = Math.max(0, document.documentElement.clientHeight) - 100;
    gvDados.SetHeight(height);
}" BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
     if(s.cp_textoMsg != undefined &amp;&amp; s.cp_textoMsg != '')
         {          
              var temp = s.cp_textoMsg;
             s.cp_textoMsg = '';
               window.top.mostraMensagem(temp, s.cp_nomeImagem, (s.cp_mostraBtnOK == 'true'), (s.cp_mostraBtnCancelar == 'true'), null, parseInt(s.cp_timeout));
         }
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
                <dxwgv:GridViewDataTextColumn FieldName="DescricaoElementoArvore" Name="IndicaEquipe"
                    Caption="Descrição" VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ValidationSettings CausesValidation="True" ErrorDisplayMode="Text" ErrorText="*">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                    <EditFormSettings Caption="Descrição da Árvore:" CaptionLocation="Top" />
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
            <SettingsPager PageSize="100">
            </SettingsPager>
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
            </SettingsEditing>
            <Settings ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible" ShowGroupedColumns="True"
                ShowFilterRow="True"></Settings>

            <SettingsPopup>
                <EditForm HorizontalAlign="WindowCenter" ShowShadow="True" VerticalAlign="WindowCenter">
                </EditForm>
                <HeaderFilter MinHeight="140px"></HeaderFilter>
            </SettingsPopup>

            <SettingsText GroupPanel="Para agrupar, arraste uma coluna aqui"></SettingsText>
        </dxwgv:ASPxGridView>
    </div>

</asp:Content>


