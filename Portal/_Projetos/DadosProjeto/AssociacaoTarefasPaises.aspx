<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssociacaoTarefasPaises.aspx.cs" Inherits="_Projetos_DadosProjeto_AssociacaoTarefasPaises" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    <link href="../../estilos/custom.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div>

            <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoTarefa" AutoGenerateColumns="False" Width="100%" ID="gvDados" EnableViewState="False" OnCustomCallback="gvDados_CustomCallback" OnAfterPerformCallback="gvDados_AfterPerformCallback">
                <ClientSideEvents
                    CustomButtonClick="function(s, e) 
{      
           // debugger   
               s.SetFocusedRowIndex(e.visibleIndex);
    	e.processOnServer = false;
               if(e.buttonID == 'btnEditar')
               {	
                        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);       
                        onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
               }
   }"
                    BeginCallback="function(s, e) {
	command = e.command;
}"
                    EndCallback="function(s, e) {
	if(command == &quot;CUSTOMCALLBACK&quot;)
               {
                        var sucesso = s.cp_sucesso;
                        var erro = s.cp_erro;
                        if(s.cp_erro == &quot;&quot;)
                        {
                                     window.top.mostraMensagem(sucesso , 'sucesso', false, false, null, 2000);
                                     if (window.onClick_btnCancelar)
                                     {
       	                                onClick_btnCancelar();
                                     }
                        }
                        else
                        {
                                     if(s.cp_erro != &quot;&quot;)
                                     {
                                                  window.top.mostraMensagem(erro , 'erro', true, false, null);
                                      }
                        }
               }
}"></ClientSideEvents>
                <SettingsPager PageSize="100">
                </SettingsPager>
                <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible" AutoFilterCondition="Contains" ShowFilterRowMenuLikeItem="True"></Settings>
                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                <SettingsText CommandClearFilter="Limpar filtro"></SettingsText>
                <Columns>
                    <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="False" Width="50px" ToolTip="Editar os perfis do usu&#225;rio" VisibleIndex="0">
                        <CustomButtons>
                            <dxcp:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                </Image>
                            </dxcp:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                        <HeaderTemplate>
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
                    </dxcp:GridViewCommandColumn>
                    <dxcp:GridViewDataTextColumn FieldName="NomeTarefa" VisibleIndex="3" ShowInCustomizationForm="True" Caption="Tarefa">
                    </dxcp:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="CodigoTarefa" Caption="CodigoTarefa" VisibleIndex="1" Visible="False">
                    </dxcp:GridViewDataTextColumn>
                    <dxtv:GridViewDataSpinEditColumn Caption="Quantidade" FieldName="Quantidade" VisibleIndex="4" Width="85px">
                        <PropertiesSpinEdit DisplayFormatString="{0}" NumberFormat="Custom">
                        </PropertiesSpinEdit>
                        <Settings ShowFilterRowMenu="True" />
                        <CellStyle Font-Size="Larger">
                        </CellStyle>
                    </dxtv:GridViewDataSpinEditColumn>
                    <dxtv:GridViewDataTextColumn Caption="Sequencial" FieldName="SequenciaTarefaCronograma" VisibleIndex="2" Width="85px">
                        <Settings AllowAutoFilter="False" />
                        <EditCellStyle HorizontalAlign="Center">
                        </EditCellStyle>
                    </dxtv:GridViewDataTextColumn>
                </Columns>
            </dxcp:ASPxGridView>

        </div>


        <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="680px" ID="pcDados">

            <ContentStyle>
                <Paddings Padding="5px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">

                    <table cellpadding="0" cellspacing="0" class="auto-style1">
                        <tr>
                            <td>
                                <dxtv:ASPxGridView ID="gvPaises" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPaises" KeyFieldName="CodigoPais" OnCustomCallback="gvPaises_CustomCallback" Width="100%">
                                    <Templates>
                                        <GroupRowContent>
                                            <%# Container.GroupText == "0" ? "Selecionados" : "Disponíveis" %>
                                        </GroupRowContent>
                                    </Templates>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings VerticalScrollableHeight="150" VerticalScrollBarMode="Auto" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <Columns>
                                        <dxtv:GridViewCommandColumn Caption=" " SelectAllCheckboxMode="Page" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="40px">
                                        </dxtv:GridViewCommandColumn>
                                        <dxtv:GridViewDataTextColumn Caption="País" FieldName="NomePais" ShowInCustomizationForm="True" VisibleIndex="2">
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn FieldName="Selecionado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption=" " FieldName="ColunaAgrupamento" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" VisibleIndex="1">
                                        </dxtv:GridViewDataTextColumn>
                                    </Columns>
                                </dxtv:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table id="tblSalvarFechar1" border="0" cellpadding="0" cellspacing="0" class="formulario-colunas">
                                    <tbody>
                                        <tr style="height: 35px">
                                            <td align="right">
                                                <dxtv:ASPxButton ID="btnSalvar1" runat="server" AutoPostBack="False" CausesValidation="False" ClientInstanceName="btnSalvar1" Text="Salvar" UseSubmitBehavior="False" Width="100px">
                                                    <ClientSideEvents Click="function(s, e) 
{
         e.processOnServer = false;
         if (window.onClick_btnSalvar)
        {
                  onClick_btnSalvar();
        }
}" />
                                                    <Paddings Padding="0px" />
                                                </dxtv:ASPxButton>
                                            </td>
                                            <td align="right">
                                                <dxtv:ASPxButton ID="btnFechar1" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100px">
                                                    <ClientSideEvents Click="function(s, e) {
     e.processOnServer = false;
     if (window.onClick_btnCancelar)
    {
               onClick_btnCancelar();
    }
}" />
                                                    <Paddings Padding="0px" />
                                                </dxtv:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>
        <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50" Landscape="True" ExportEmptyDetailGrid="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" MaxColumnWidth="700" PaperKind="A4">
        </dxcp:ASPxGridViewExporter>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
    </form>
</body>
</html>
