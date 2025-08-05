<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupAssociaItensBacklog.aspx.cs" Inherits="_Projetos_DadosProjeto_popupAssociaItensBacklog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2 {
            width: 100%;
        }
         .btn{
            text-transform: capitalize !important;
        }
    </style>
    <script type="text/javascript"> 
        var comando;
        function funcaoCallbackSalvar() {
            tlDados.PerformCallback('marcar');
        }

        function funcaoCallbackFechar() {
            window.top.fechaModalComFooter();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="3" cellspacing="3" class="style2">
            <tr>
                <td>
                    <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlDados" Width="100%" KeyFieldName="CodigoItem" ParentFieldName="CodigoItemSuperior" OnCustomCallback="tlDados_CustomCallback">
                        <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" EnableCustomizationWindow="True" ProcessFocusedNodeChangedOnServer="True" />
                        <Columns>
                            <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoItem" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="1" AllowAutoFilter="True">
                            </dxwtle:TreeListTextColumn>
                            <dxwtle:TreeListTextColumn AllowAutoFilter="True" AutoFilterCondition="Contains" Caption="Título do Item" FieldName="TituloItem" ShowInCustomizationForm="False" ShowInFilterControl="Default" VisibleIndex="2" Width="400px" SortIndex="0" SortOrder="Ascending" Visible="False">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dxwtle:TreeListTextColumn>
                            <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="Sprint" ShowInCustomizationForm="False" ShowInFilterControl="Default" VisibleIndex="7" Caption="Sprint Associada" Visible="False">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dxwtle:TreeListTextColumn>
                            <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="Importancia" ShowInCustomizationForm="False" ShowInFilterControl="Default" VisibleIndex="3" Width="130px" Caption="Importância">
                                <HeaderStyle Wrap="True" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwtle:TreeListTextColumn>
                            <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Esforço (h)" FieldName="EsforcoPrevisto" ShowInCustomizationForm="False" ShowInFilterControl="Default" VisibleIndex="5" Width="140px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwtle:TreeListTextColumn>
                            <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="CodigoIteracao" FieldName="CodigoIteracao" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="8">
                            </dxwtle:TreeListTextColumn>
                            <dxwtle:TreeListTextColumn AutoFilterCondition="Contains" Caption="Título do Item" Name="colSelecaoIB" ShowInCustomizationForm="False" ShowInFilterControl="True" VisibleIndex="0" Width="100%" AllowAutoFilter="True" AllowHeaderFilter="True" ShowFilterRowMenu="True">
                                <DataCellTemplate>
                                    <dxtv:ASPxCheckBox ID="cbSelecaoIB" runat="server" OnInit="cbSelecaoIB_Init" Text="Título do Item ">
                                    </dxtv:ASPxCheckBox>
                                </DataCellTemplate>
                            </dxwtle:TreeListTextColumn>
                        </Columns>
                        <Settings SuppressOuterGridLines="True" VerticalScrollBarMode="Visible" ShowFilterRow="True" HorizontalScrollBarMode="Visible" />
                        <SettingsBehavior AllowAutoFilter="True" AllowDragDrop="False" AllowHeaderFilter="True" FocusNodeOnExpandButtonClick="False" FocusNodeOnLoad="False"></SettingsBehavior>                        
                        <SettingsPager PageSize="30">
                        </SettingsPager>
                        <SettingsCustomizationWindow PopupHorizontalAlign="RightSides" PopupVerticalAlign="BottomSides"></SettingsCustomizationWindow>

<SettingsPopupEditForm VerticalOffset="-1"></SettingsPopupEditForm>

                        <SettingsText CommandDelete="Excluir" ConfirmDelete="Deseja realmente excluir o item de backlog?" />

<SettingsPopup>
<EditForm VerticalOffset="-1"></EditForm>
</SettingsPopup>

                        <Styles>
                            <Cell Wrap="True">
                            </Cell>
                        </Styles>
                        <ClientSideEvents BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
//alert(comando);       
 if(comando == 'CustomCallback')
        {
                 if(s.cpErro == '')
                {
                          if(s.cpSucesso != '')
                         {
                                    window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 2500);
                         }
                }
               else
              {
                         if(s.cpErro != '')
                         {
                                     window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
                         }
              }
         }
}" Init="function(s, e) {
	s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 30);
}" />
                    </dxwtl:ASPxTreeList>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
