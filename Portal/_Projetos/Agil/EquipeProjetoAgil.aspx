<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipeProjetoAgil.aspx.cs" Inherits="_Projetos_Agil_EquipeProjetoAgil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var comando;
    </script>
        <style>
    .container {
        position: relative;
    }
   
    .button-container {
        position: absolute;
        top: 0;
        right: 0;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" KeyFieldName="CodigoRecursoProjeto" Width="100%" OnCellEditorInitialize="gvDados_CellEditorInitialize" OnRowDeleted="gvDados_RowDeleted" OnRowDeleting="gvDados_RowDeleting" OnInitNewRow="gvDados_InitNewRow" OnRowUpdating="gvDados_RowUpdating" OnRowInserting="gvDados_RowInserting">
                <ClientSideEvents Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);
    height = height - 45;
 	s.SetHeight(height);
}"
                    CustomButtonClick="function(s, e) {
     if(e.buttonID == 'btnExcluir')
     {
	var funcObj = {
                        funcaoClickOK: function () 
                        {
                               s.DeleteRow(s.GetFocusedRowIndex());
                        }
                    };
                   window.top.mostraConfirmacao(traducao.EquipeProjetoAgil_deseja_realmente_excluir_o_recurso_, function () { funcObj['funcaoClickOK']() }, null);
     }
}"
                    BeginCallback="function(s, e) {
	comando = e.command;
}"
                    EndCallback="function(s, e) 
{
      if(comando == 'UPDATEEDIT' )
      {
            s.Refresh();
       }
       if(comando == 'DELETEROW')
       {
               if(s.cpErro != '')
               {
                      window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
                }
                else
                {
                      if(s.cpSucesso != '')
                      {
                                   window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null);
                                  s.Refresh();
                      }
              }
        }
}" />
                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
                </SettingsEditing>
                <Settings VerticalScrollBarMode="Visible" />
                <SettingsCommandButton RenderMode="Image">
                    <EditButton Text="Editar">
                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                        </Image>
                    </EditButton>
                    <DeleteButton Text="Excluir">
                        <Image Url="~/imagens/botoes/excluirReg02.PNG&quot;">
                        </Image>
                    </DeleteButton>
                </SettingsCommandButton>
                <SettingsPopup>
                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="400px">
                    </EditForm>

                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>
                <SettingsText PopupEditFormCaption="Editar Recurso" />
                <Columns>
                    <dxtv:GridViewDataTextColumn FieldName="CodigoRecursoProjeto" ReadOnly="True" Visible="False" VisibleIndex="3">
                        <EditFormSettings Visible="False" />
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Recurso da Equipe" FieldName="CodigoRecursoCorporativo" Visible="False" VisibleIndex="4">
                        <PropertiesComboBox TextField="NomeRecurso" ValueField="CodigoRecursoCorporativo" ValueType="System.Int32">
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="0" />
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Papel" FieldName="CodigoTipoPapelRecursoProjeto" Visible="False" VisibleIndex="5">
                        <PropertiesComboBox DataSourceID="sdsTipoPapelRecurso" TextField="DescricaoTipoPapelRecurso" ValueField="CodigoTipoPapelRecurso" ValueType="System.Int32">
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="1" />
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewCommandColumn ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="75px">
                        <CustomButtons>
                            <dxtv:GridViewCommandColumnCustomButton ID="btnExcluir">
                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                </Image>
                            </dxtv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td align="center">
                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                            ItemSpacing="5px"  OnInit="menu_Init">
                                            <Paddings Padding="0px" />
                                            <Items>
                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="btnImportar" Text="" ToolTip="Incluir">
                                                    <Image Url="~/imagens/Perfis/PersonalizarPerfilUsuario.png">
                                                    </Image>
                                                </dxm:MenuItem>
                                            </Items>
                                            <ItemStyle Cursor="pointer">
                                                <HoverStyle>
                                                    <border borderstyle="None" />
                                                </HoverStyle>
                                                <Paddings Padding="0px" />
                                            </ItemStyle>
                                            <Border BorderStyle="None" />
                                        </dxm:ASPxMenu>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </dxtv:GridViewCommandColumn>
                    <dxtv:GridViewDataTextColumn Caption="Recurso da Equipe" FieldName="NomeRecurso" VisibleIndex="1">
                        <EditFormSettings Visible="False" />
                        <HeaderStyle Font-Bold="False" />
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Papel" FieldName="DescricaoTipoPapelRecurso" VisibleIndex="2">
                        <EditFormSettings Visible="False" />
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataSpinEditColumn FieldName="PercentualAlocacao" VisibleIndex="6" Caption="Percentual Alocação" Width="180px">
                        <PropertiesSpinEdit NumberFormat="Custom" DisplayFormatString="n0" MaxValue="100" NumberType="Integer" DisplayFormatInEditMode="False">
                            <SpinButtons ClientVisible="False">
                            </SpinButtons>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesSpinEdit>
                        <EditFormSettings Caption="Percentual Alocação" CaptionLocation="Top" Visible="True" />
                    </dxtv:GridViewDataSpinEditColumn>
                </Columns>
            </dxcp:ASPxGridView>
        </div>
        <div class="container">
            <div class="button-container">
                <dxcp:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"
                    Text="Fechar" Width="110px">
                    <ClientSideEvents Click="function(s, e) {
	                                            window.top.fechaModal();
                                            }" />
                </dxcp:ASPxButton>
            </div>
        </div>
        <asp:SqlDataSource ID="sdsTipoPapelRecurso" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [CodigoTipoPapelRecurso], [DescricaoTipoPapelRecurso] FROM [Agil_TipoPapelRecurso]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsRecursoCorporativo" runat="server" ProviderName="System.Data.SqlClient">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
                <asp:Parameter Name="CodigoProjeto" DbType="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <dxcp:ASPxCallback ID="callbackImportar" runat="server" ClientInstanceName="callbackImportar" OnCallback="callbackImportar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
               
       if(s.cpErro !== '')
       {
                window.top.mostraMensagem(s.cpErro , 'erro', true, false, null, null);
       }
       else
       {
                gvDados.Refresh();
                if(s.cpAlerta !== '')
               {                          
                           window.top.mostraMensagem(s.cpAlerta , 'atencao', true, false, null, 3400);
               }
               else if(s.cpSucesso !== '')
               {                          
                           window.top.mostraMensagem(s.cpSucesso , 'sucesso', false, false, null, 3400);
               }
       }
}" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
